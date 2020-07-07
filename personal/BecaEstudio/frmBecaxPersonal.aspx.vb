Partial Class BecaEstudio_frmBecaxPersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                If verificarTipoPersonal() Then
                    CargarCombos()
                    CargarRegistros()
                    ' EstadoConvenio()
                    pnlBusqueda.Visible = True
                    'pnlPrincipal.Visible = False
                    gvLista.SelectedIndex = -1
                    Me.txtBuscarAlumno.Text = ""

                    If HiddenField.Value = 0 Then
                        btnEnviar.Visible = False
                        chkAcepto.enabled = True
                    Else
                        btnEnviar.Visible = True
                    End If
                    chkAcepto.enabled = False
                    Me.panelTipoPersonal.Visible = False
                Else
                    Me.panelTipoPersonal.Visible = True
                End If
                Me.txtBuscarAlumno.Focus()
            End If
            If Not verificarCronograma() Then
                Me.btnBuscar.Enabled = False
                Me.txtBuscarAlumno.Enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Function verificarCronograma() As Boolean

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("Beca_ConsultarCronogramaSolicitud")
        obj.CerrarConexion()

        Me.LblCronograma.Text = "Solicitud de Becas disponible desde " & dt.Rows(0).Item("desde") & " al " & dt.Rows(0).Item("hasta")

        If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
            Return True
        End If
        If dt.Rows(0).Item("estado") = "N" Then
            Return False
        Else
            Return True
        End If


    End Function
    Private Function verificarTipoPersonal() As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_TipoPersonal", CInt(Request.QueryString("id")))
            If tb.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Sub CargarRegistros()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_ListaBecaSolicitudPersonal", CInt(Request.QueryString("id")), 0)
            If tb.Rows.Count > 0 Then
                Me.gvListaBecas.DataSource = tb
            Else
                Me.gvListaBecas.DataSource = Nothing
            End If


            If Not Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
                Me.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")

        'Me.ddlCiclo.DataSource = tb
        'Me.ddlCiclo.DataTextField = "descripcion_cac"
        'Me.ddlCiclo.DataValueField = "codigo_cac"
        'Me.ddlCiclo.DataBind()
        'If Session("Beca_codigo_cac") IsNot Nothing Then
        '    Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        'End If

        'tb = obj.TraerDataTable("BECA_ListaTipoConvenio")
        'Me.ddlConvenio.DataSource = tb
        'Me.ddlConvenio.DataTextField = "descripcion_Mno"
        'Me.ddlConvenio.DataValueField = "codigo_Mno"
        'Me.ddlConvenio.DataBind()

        'tb = obj.TraerDataTable("Beca_ListaCobertura")
        'Me.ddlCobertura.DataSource = tb
        'Me.ddlCobertura.DataTextField = "porcentaje_bco"
        'Me.ddlCobertura.DataValueField = "codigo_bco"
        'Me.ddlCobertura.DataBind()

        'tb = obj.TraerDataTable("Beca_ListaTipoBecaConvenio")
        'Me.ddlTipoBeca.DataSource = tb
        'Me.ddlTipoBeca.DataTextField = "descripcion_bec"
        'Me.ddlTipoBeca.DataValueField = "codigo_bec"
        'Me.ddlTipoBeca.DataBind()

        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub lbkBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbkBuscar.Click
        Try
            pnlBusqueda.Visible = True
            pnlPrincipal.Visible = False
            gvLista.SelectedIndex = -1
            Me.txtBuscarAlumno.Text = ""

            If HiddenField.Value = 0 Then
                btnEnviar.Visible = False
                chkAcepto.enabled = True
            Else
                btnEnviar.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub BuscarAlumno()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ' Si es direc.pensiones busca cualquier alumno
            If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
                dt = obj.TraerDataTable("[Beca_ListaBuscarAlumnos]", txtBuscarAlumno.Text.Trim, ddlTipo.SelectedValue)
            Else
                dt = obj.TraerDataTable("[Beca_ListaBuscarAlumnosPersonal]", txtBuscarAlumno.Text.Trim, ddlTipo.SelectedValue, CInt(Request.QueryString("id")))
            End If

            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                gvLista.DataSource = dt
                gvLista.DataBind()
                Me.Label1.Visible = True
            Else
                gvLista.DataSource = Nothing
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            'Me.btnAgregar.Enabled = True
            BuscarAlumno()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                    e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                    e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvLista','Select$" & e.Row.RowIndex & "');")
                    e.Row.Style.Add("cursor", "hand")
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLista.SelectedIndexChanged
        Try
            HiddenField.Value = gvLista.SelectedRow.Cells(0).Text
            'Response.Write(HiddenField.Value)
            If HiddenField.Value > 0 Then
                btnEnviar.Visible = True
                'btnEnviar.Text = "  Enviar"
                'btnEnviar.CssClass = "enviarvac"
                gvLista.SelectedRow.Cells(1).ForeColor = Drawing.Color.Blue
                'Me.lblInstrucciones.Text = "Ud. desea generar una vacante para el trabajador => <b><font color='blue'>" & gvVacantes.SelectedRow.Cells(1).Text & "</font></b><br>Pulse el botón <b><u>Enviar</u><b>."
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            'pnlBusqueda.Visible = False
            Me.gvLista.Visible = False
            pnlPrincipal.Visible = True
            Me.pnlDatosAlumno.Visible = True
            gvLista.SelectedIndex = -1
            If HiddenField.Value > 0 Then
                DatosAlumno()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub DatosAlumno()
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("Beca_DatosAlumno", Me.HiddenField.Value, "D")
            If tb.Rows.Count > 0 Then
                lblAlumno.Text = tb.Rows(0).Item("alumno").ToString
                lblCodigoUniv.Text = tb.Rows(0).Item("codigoUniver_Alu").ToString
                lblcicloingreso.Text = tb.Rows(0).Item("cicloIng_Alu").ToString
                lblEscuelaprofesional.Text = tb.Rows(0).Item("nombre_Cpf").ToString
                lblPlanEstudios.Text = tb.Rows(0).Item("descripcion_Pes").ToString

                '::: Llamamos a ver si cumple con los requisitos :::
                Requisitos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub Requisitos()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Beca_ConsultarRequisitoAlumnoPersonal", Me.HiddenField.Value)
            obj.CerrarConexion()

            Dim htmlTb As String = ""
            Dim img As String = ""
            Dim pass As Integer = 1
            Dim f As Byte = 0

            If dt.Rows.Count Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    htmlTb = htmlTb & "<tr> "
                    htmlTb = htmlTb & "<td class=""filarequisito"">" & dt.Rows(i).Item("descripcion_req") & "</td>"
                    htmlTb = htmlTb & "<td class=""filarequisito"">" & IIf(dt.Rows(i).Item("valord_bsr").ToString = "1", "Sí", dt.Rows(i).Item("valord_bsr").ToString) & "</td>"
                    htmlTb = htmlTb & "<td class=""filarequisito"">" & dt.Rows(i).Item("valora_bsr").ToString & "</td>"
                    img = IIf(dt.Rows(i).Item("cumple_bsr").ToString = "1", "check.png", "exclamation.png")
                    htmlTb = htmlTb & "<td class=""cell cell5""><img src=""images/" & img & """</td>"
                    htmlTb = htmlTb & "</tr>"
                    pass = pass * dt.Rows(i).Item("cumple_bsr")
                Next

                tb.InnerHtml = htmlTb

                'pass = 1
                If pass Then
                    chkAcepto.enabled = True
                Else
                    chkAcepto.enabled = False
                End If

                'Agregado para Direc.Pensiones, puede solicitar beca sin importar requisitos. 10.03.14
                If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
                    chkAcepto.enabled = True
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
            'Requisitos()
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If verificarCronograma() Then

                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim valoresdevueltos(1) As Integer
                Dim intConvenio As Integer = 0
                Dim pagina As String
                pagina = "frmBecaxPersonal.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf")
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                'Aqui las validadciones'
                '==================================
                '::Insercion ::'
                tb = obj.TraerDataTable("Beca_RegistrarSolicitudPersonal", Me.HiddenField.Value, CInt(Request.QueryString("id")))
                If tb.Rows(0).Item("rpta") = -1 Then
                    Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('Ocurrio un error al tratar de registrar la solicitud.');", True)
                    Exit Sub
                Else
                    If tb.Rows(0).Item("rpta") = 999999 Then
                        Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('El alumno ya fue asignado a una beca, favor de verificar.');", True)
                    Else
                        chkAcepto.enabled = False
                        If Not Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
                            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.');", True)
                        Else
                            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('La solicitud fue registrada correctamente.');location.href='" & pagina & "'", True)
                        End If
                    End If
                End If
            End If

            ':: Listamos todas los registrso ::
            'CargarRegistros()            
            If Not Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "1" Then
                CargarRegistros()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaBecas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvListaBecas.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If gvListaBecas.DataKeys.Item(e.RowIndex).Values(1).ToString <> "aceptado" Then
            obj.Ejecutar("BECA_SolicitudPersonalEliminar", Me.gvListaBecas.DataKeys.Item(e.RowIndex).Values(0))
            chkAcepto.enabled = True
        Else
            Me.ClientScript.RegisterStartupScript(Me.GetType, "Faltan Datos", "alert('No se puede eliminar un solicitud aceptada o rechazada.');", True)
        End If
        'CargarRegistros()
        obj.CerrarConexion()
        obj = Nothing


    End Sub

    'Protected Sub ddlTipoBeca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoBeca.SelectedIndexChanged
    '    Try
    '        EstadoConvenio()
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    'Private Sub EstadoConvenio()
    '    Try
    '        If ddlTipoBeca.SelectedValue = 4 Then
    '            Me.lblConvenio.Visible = False
    '            Me.ddlConvenio.Visible = False
    '        Else
    '            Me.lblConvenio.Visible = True
    '            Me.ddlConvenio.Visible = True
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub chkAcepto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAcepto.CheckedChanged
        Me.btnAgregar.enabled = chkAcepto.checked
    End Sub
End Class
