'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class frmHorario
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer
    Dim codigo_pel As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        codigo_per = Request.QueryString("id")
        'función que devuelva el codigo_pel vigente
        Dim obj As New clsPersonal
        codigo_pel = obj.ConsultarPeridoLaborable
        If Not IsPostBack Then
            cargarControles()
            consultarDatosGenerales()
        End If
        Me.cboSemana.Enabled = obj.EsCCSalud(codigo_per)
        lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
        If Trim(lblObservacion.Text) <> "" Then
            lblObservacion.Text = "Obs. " & lblObservacion.Text
        End If
        lblFechas.Text = obj.ConsultarRangoFechasSemana(Me.cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
        ConsultarListaCambiosHorarios()
    End Sub

    Private Sub ConsultarListaCambiosHorarios()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaCambiosHorarios(codigo_per, codigo_pel)
        gvListaCambios.DataSource = dts
        gvListaCambios.DataBind()
    End Sub
    Private Sub cargarControles()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        Dim dtsEscuela As New Data.DataTable

        Dim dtsFacultad As New Data.DataTable

        dts = obj.ConsultarHorasControl()
        ddlHoraInicio.DataSource = dts
        ddlHoraInicio.DataTextField = "hora"
        ddlHoraInicio.DataValueField = "hora"
        ddlHoraInicio.DataBind()
        ddlHoraFin.DataSource = dts
        ddlHoraFin.DataTextField = "hora"
        ddlHoraFin.DataValueField = "hora"
        ddlHoraFin.DataBind()
        'cargo la lista de escuelas
        dtsEscuela = obj.ConsultarCarreraProfesional()
        ddlEscuela.DataSource = dtsEscuela
        ddlEscuela.DataTextField = "nombre_cpf"
        ddlEscuela.DataValueField = "codigo_cpf"
        ddlEscuela.DataBind()
        'cargo la lista de facultades
        dtsFacultad = obj.ConsultarFacultad()
        ddlFacultad.DataSource = dtsFacultad
        ddlFacultad.DataTextField = "nombre_fac"
        ddlFacultad.DataValueField = "codigo_fac"
        ddlFacultad.DataBind()

    End Sub
    Private Sub consultarDatosGenerales()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarDatosPersonales(codigo_per)
        Me.lblCeco.Text = dts.Rows(0).Item("descripcion_cco").ToString
        Me.lblNombre.Text = dts.Rows(0).Item("paterno").ToString & " " & dts.Rows(0).Item("materno").ToString & " " & dts.Rows(0).Item("nombres").ToString
        Me.lblTipo.Text = dts.Rows(0).Item("descripcion_Tpe").ToString
        Me.lblDedicacion.Text = dts.Rows(0).Item("descripcion_ded").ToString
        Me.lblHoras.Text = dts.Rows(0).Item("horas").ToString
        Me.lblFechaIngreso.Text = dts.Rows(0).Item("fechaIni_Per").ToString

        If dts.Rows(0).Item("foto").ToString <> "" Then
            'Me.imgFoto.ImageUrl = "http://www.usat.edu.pe/campusvirtual/personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
            Me.imgFoto.ImageUrl = "../../personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
        Else
            Me.imgFoto.BackColor = Drawing.Color.Red
            imgFoto.AlternateText = "ATENCIÓN:    Suba su foto en el módulo de HOJA DE VIDA"
            imgFoto.ForeColor = Drawing.Color.White
        End If

        'Si ha sido enviado al director no puede modificar datos del horario
        if dts.Rows(0).Item("envioDirector_Per").toString= "True" then
            me.btnEnviar.enabled=false
            Me.btnAceptar.Enabled = False
            cmdborrar.Enabled = False
            cmdHorario1.Enabled = False
            cmdHorario2.Enabled = False
            cmdHorario3.Enabled = False
            cmdHorario4.Enabled = False
            dim i as integer
            for i = 0 to gvEditHorario.Rows.count-1
                gvEditHorario.Rows(i).Cells(0).enabled=false
            next i
            lblHoras.Enabled = False
            Me.lblAvisoEnvio.Visible = False
        Else
            Me.lblAvisoEnvio.Visible = True
        End If

    End Sub
    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarVistaHorario(codigo_per, codigo_pel, Me.cboSemana.SelectedValue)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()
        lblHorasSemanales.Text = obj.ConsultarTotalHorasSemana(codigo_per, codigo_pel, Me.cboSemana.SelectedValue)
        lblHorasMensuales.Text = obj.TotalHorasMes(codigo_per, codigo_pel)
    End Sub
    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaHorario(codigo_per, codigo_pel, cboSemana.SelectedValue)
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim obj As New clsPersonal
        obj.AgregarHorarioPersonal(ddlDia.SelectedValue, ddlHoraInicio.SelectedValue, ddlHoraFin.SelectedValue, codigo_per, 1, ddlTipo.SelectedValue, codigo_pel, ddlEscuela.SelectedValue, Me.txtEncEscuela.Text, Me.txtResEscuela.Text, ddlFacultad.SelectedValue, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub

    Protected Sub gvEditHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEditHorario.RowDataBound
        e.Row.Cells(1).Visible = False
    End Sub



    Protected Sub gvEditHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEditHorario.RowDeleting
        Dim obj As New clsPersonal
            obj.EliminarHorarioPersonal(gvEditHorario.Rows(e.RowIndex).Cells(1).Text)
            consultarVistaHorario()
            consultarListaHorario()
    End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        e.Row.Cells(2).Width = 40
        e.Row.Cells(3).Width = 40
        e.Row.Cells(4).Width = 40
        e.Row.Cells(5).Width = 40
        e.Row.Cells(6).Width = 40
        e.Row.Cells(7).Width = 40
        Select Case e.Row.Cells(2).Text
            Case "I"
                e.Row.Cells(2).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(2).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(2).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(2).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(2).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(2).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(2).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(2).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(2).BackColor = Drawing.Color.Lime
        End Select

        Select Case e.Row.Cells(3).Text
            Case "I"
                e.Row.Cells(3).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(3).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(3).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(3).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(3).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(3).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(3).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(3).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(3).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(4).Text
            Case "I"
                e.Row.Cells(4).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(4).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(4).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(4).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(4).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(4).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(4).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(4).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(4).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(5).Text
            Case "I"
                e.Row.Cells(5).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(5).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(5).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(5).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(5).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(5).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(5).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(5).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(5).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(6).Text
            Case "I"
                e.Row.Cells(6).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(6).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(6).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(6).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(6).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(6).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(6).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(6).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(6).BackColor = Drawing.Color.Lime
        End Select
        Select Case e.Row.Cells(7).Text
            Case "I"
                e.Row.Cells(7).BackColor = Drawing.Color.Yellow
            Case "D"
                e.Row.Cells(7).BackColor = Drawing.Color.Green
            Case "E"
                e.Row.Cells(7).BackColor = Drawing.Color.Violet
            Case "A"
                e.Row.Cells(7).BackColor = Drawing.Color.Orange
            Case "P"
                e.Row.Cells(7).BackColor = Drawing.Color.Blue
            Case "T"
                e.Row.Cells(7).BackColor = Drawing.Color.Gray
            Case "F"
                e.Row.Cells(7).BackColor = Drawing.Color.Brown
            Case "G"
                e.Row.Cells(7).BackColor = Drawing.Color.DarkTurquoise
            Case "H"
                e.Row.Cells(7).BackColor = Drawing.Color.Lime
        End Select
        If e.Row.Cells(0).Text = "08:00" Or e.Row.Cells(0).Text = "16:30" Then
            e.Row.Cells(0).ForeColor = Drawing.Color.Blue
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue
        End If



    End Sub

Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
    dim obj as new clsPersonal
        If lblHorasSemanales.text <> lblHoras.Text Then
            Me.lblMensaje.Text = "Las horas semanales no coinciden con la distribución, favor de verificar"
            Me.lblMensaje.Visible = True
        Else
            Me.lblMensaje.Text = "Horario enviado satisfactoriamente"
            Me.lblMensaje.ForeColor = Drawing.Color.Blue
            Me.lblMensaje.Visible = True
            Me.btnEnviar.Enabled = False
            Me.btnAceptar.Enabled = False
            Dim i As Integer
            For i = 0 To gvEditHorario.Rows.Count - 1
                gvEditHorario.Rows(i).Cells(0).Enabled = False
            Next i
            obj.EnviarHorarioPersonal(codigo_per, CInt(Me.lblHoras.Text))
            obj.registrarBitacoraHorario(codigo_per, codigo_pel, codigo_per)
            lblHoras.Enabled = False
            cmdborrar.Enabled = False
            cmdHorario1.Enabled = False
            cmdHorario2.Enabled = False
            cmdHorario3.Enabled = False
            cmdHorario4.Enabled = False
        End If

End Sub

    Protected Sub ddlTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipo.SelectedIndexChanged

        'If ddlTipo.SelectedValue = "F" Then
        'lblEscuela.Visible = False
        'lblFacultad.Visible = True

        'ddlEscuela.Visible = False
        'ddlFacultad.Visible = True
        'ddlEscuela.SelectedValue = 0

        'Else
        lblEscuela.Visible = True
        lblFacultad.Visible = False

        ddlEscuela.Visible = True
        ddlFacultad.Visible = False
        ddlFacultad.SelectedValue = 0
        'End If
    End Sub



    Protected Sub cmdHorario1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorario1.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(1, codigo_per, codigo_pel, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub

    Protected Sub cmdHorario2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorario2.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(2, codigo_per, codigo_pel, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub

    Protected Sub cmdHorario3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorario3.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(3, codigo_per, codigo_pel, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub


    Protected Sub cmdBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBorrar.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(0, codigo_per, codigo_pel, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub

    Protected Sub cmdHorario4_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorario4.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(4, codigo_per, codigo_pel, cboSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
    End Sub
End Class
