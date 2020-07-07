
Partial Class frmHorario
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer
    Dim codigo_pel As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        codigo_per = Request.QueryString("id")
        ' codigo_per = 47

        'función que devuelva el codigo_pel vigente
        'codigo_pel = fn
        codigo_pel = 13

        If Not IsPostBack Then
            cargarControles()
            consultarVistaHorario()
            consultarListaHorario()
            consultarDatosGenerales()
        End If
    End Sub

    Private Sub cargarControles()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        Dim dtsEscuela As New Data.DataTable
        dts = obj.ConsultarHorasControl()
        ddlHoraInicio.DataSource = dts
        ddlHoraInicio.DataTextField = "hora"
        ddlHoraInicio.DataValueField = "hora"
        ddlHoraInicio.DataBind()
        ddlHoraFin.DataSource = dts
        ddlHoraFin.DataTextField = "hora"
        ddlHoraFin.DataValueField = "hora"
        ddlHoraFin.DataBind()

        dtsEscuela = obj.ConsultarCarreraProfesional()
        ddlEscuela.DataSource = dtsEscuela
        ddlEscuela.DataTextField = "nombre_cpf"
        ddlEscuela.DataValueField = "codigo_cpf"
        ddlEscuela.DataBind()
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
        If dts.Rows(0).Item("foto").ToString <> "" Then
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            Me.imgFoto.ImageUrl = "https://intranet.usat.edu.pe/campusvirtual/personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
        Else
            Me.imgFoto.BackColor = Drawing.Color.Red
            imgFoto.AlternateText = "ATENCIÓN:    Suba su foto en el módulo de HOJA DE VIDA"
            imgFoto.ForeColor = Drawing.Color.White
        End If

        
        'Si ha sido enviado al director no puede modificar datos del horario
        if dts.Rows(0).Item("envioDirector_Per").toString= "True" then
            me.btnEnviar.enabled=false
            me.btnAceptar.enabled=false
            dim i as integer
            for i = 0 to gvEditHorario.Rows.count-1
                gvEditHorario.Rows(i).Cells(0).enabled=false
            next i
            lblHoras.enabled=False
        end if

    End Sub
    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarVistaHorario(codigo_per, codigo_pel)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        lblHorasSemanales.text =  obj.ConsultarTotalHorasSemana(codigo_per,codigo_pel)
    End Sub
    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaHorario(codigo_per, codigo_pel)
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim obj As New clsPersonal
        obj.AgregarHorarioPersonal(ddlDia.SelectedValue, ddlHoraInicio.SelectedValue, ddlHoraFin.SelectedValue, codigo_per, 1, ddlTipo.SelectedValue, codigo_pel, ddlEscuela.SelectedValue, Me.txtEncEscuela.Text, Me.txtResEscuela.Text)
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
        End Select

    End Sub

Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
    dim obj as new clsPersonal
    if lblHorasSemanales.text <> lblHoras.text  then
        me.lblmensaje.text ="Las horas semanales no coinciden con la distribución, favor de verificar"
        me.lblmensaje.visible=true
    else
        me.lblmensaje.text ="Horario enviado satisfactoriamente"
        me.lblmensaje.forecolor=Drawing.Color.Blue
        me.lblmensaje.visible=true
        me.btnEnviar.enabled=false
        me.btnAceptar.enabled=false
        dim i as integer
        for i = 0 to gvEditHorario.Rows.count-1
            gvEditHorario.Rows(i).Cells(0).enabled=false
        next i
        obj.EnviarHorarioPersonal(codigo_per, cint(me.lblhoras.text))
        lblHoras.enabled=False
    end if

End Sub
End Class
