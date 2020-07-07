﻿Imports System.IO
Imports System.Security.Cryptography

Partial Class _SolicitudesTramiteDirector
    Inherits System.Web.UI.Page
    Dim ctf As Integer
    Dim mes As Integer
    Dim anio, estado, prioridad, tipo_tram As String
    Dim j As Integer
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        Session("codigo_ctf") = Request.QueryString("ctf")

        If Not IsPostBack Then

            If Request.QueryString("anio") <> "" Then
                Me.ddlAño.SelectedValue = Request.QueryString("anio")
            End If
            If Request.QueryString("mes") <> "" Then
                Me.ddlMes.SelectedValue = Request.QueryString("mes")
            End If
            If Request.QueryString("estado") <> "" Then
                Me.ddlEstado.SelectedValue = Request.QueryString("estado")
            End If
            If Request.QueryString("prioridad") <> "" Then
                Me.ddlPrioridad.SelectedValue = Request.QueryString("prioridad")
            End If

            ConsultarTipoSolicitud()

            If Request.QueryString("tipo_tram") <> "" Then
                Me.ddlTipoSolicitud.SelectedValue = Request.QueryString("tipo_tram")
            End If

            If Request.QueryString("respuesta") <> "" Then
                respuesta = Request.QueryString("respuesta")
            End If

            Me.divConfirmaRechazar.Visible = False

            lblMensaje.Visible = False 'Se añadió

            ConsultarAño() '02/01/2020

            ConsultarListaSolicitudes()

            valida_respuesta()

        End If
    End Sub

    Private Sub valida_respuesta()

        If respuesta = "A" Then
            Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha Aprobado y Enviado correctamente a Personal"
        ElseIf respuesta = "R" Then
            Me.lblMensaje0.Text = "** AVISO :  La solicitud se ha Rechazado correctamente"
        ElseIf respuesta = "C" Then
            Me.lblMensaje0.Text = "** NOTA : Operación Cancelada"
        End If

    End Sub

    Public Sub ConsultarTipoSolicitud()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaTipoSolicitudTramite", "T")
            obj.CerrarConexion()

            Me.ddlTipoSolicitud.DataTextField = "nombre_TST"
            Me.ddlTipoSolicitud.DataValueField = "codigo_TST"
            Me.ddlTipoSolicitud.DataSource = dt
            Me.ddlTipoSolicitud.DataBind()

            'Me.ddlTipoSolicitud.Items.Add("TODOS")            
            'Me.ddlTipoSolicitud.SelectedIndex = 3 'Permisos

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los Tipos de Solicitud Trámite"
        End Try
    End Sub

    Public Sub ConsultarListaSolicitudes()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""

        registrar_filtros()

        j = 0

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ListaEvaluacionSolicitudTramite", anio, mes, CInt(Session("id_per")), estado, prioridad, tipo_tram)

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                'Me.celdaGrid.Visible = True
                'Me.celdaGrid.InnerHtml = "** AVISO :  No Existen Solicitudes con los parámetros seleccionados"
                lblMensaje.Visible = True 'Se añadió
                Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"

                Me.btnEvaluar.Enabled = False 'Se añadió
                Me.btnRechazar.Enabled = False 'Se añadió

            End If
            obj.CerrarConexion()

            'Me.gvCarga.DataSource = Nothing
            'Me.gvCarga.DataBind()
            'Me.celdaGrid.Visible = True

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Public Sub ConsultarAño()

        Dim i As Integer

        Try
            For i = 2018 To year(System.DateTime.Today)

                Me.ddlAño.Items.Add(i)
                Me.ddlAño.text = i

            Next

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los años"
        End Try

    End Sub

    Private Sub registrar_filtros()

        anio = Me.ddlAño.SelectedValue
        mes = Me.ddlMes.SelectedValue
        estado = Me.ddlEstado.SelectedValue
        prioridad = Me.ddlPrioridad.SelectedValue
        tipo_tram = Me.ddlTipoSolicitud.SelectedValue

    End Sub

    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboPrioridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrioridad.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub ddlTipoSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'Dim chk As CheckBox = CType(e.Row.FindControl("CheckBox1"), CheckBox)
            'chk.Visible = False
            'e.Row.Cells(1).Text = e.Row.RowIndex + 1

            Dim cod, codi As Integer
            cod = fila.Row("codigo_ST")
            codi = fila.Row("codigo_EST")

            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            If e.Row.Cells(3).Text = "Urgente" Then
                e.Row.Cells(3).Font.Bold = False
                e.Row.Cells(3).ForeColor = System.Drawing.Color.Blue
                'e.Row.Cells(3).Attributes.Add("HyperLink", "../SolicitudTramite.aspx")
            End If

            e.Row.Cells(14).ForeColor = IIf(fila.Row("Estado") = "Pendiente", Drawing.Color.Red, Drawing.Color.Blue)
            e.Row.Cells(14).Font.Underline = IIf(fila.Row("Estado") <> "Pendiente", True, False)
            Dim pagina As String = "<a href='AprobacionSolicitudTramite.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "'>" & e.Row.Cells(14).Text & "</a>"
            'e.Row.Cells(11).Text = IIf(fila.Row("Estado") <> "Generado", "<a href='SolicitudTramite.aspx?cod=" + cod + "'>" + e.Row.Cells(11).Text + "</a>", e.Row.Cells(11).Text)
            e.Row.Cells(14).Text = IIf(fila.Row("Estado") <> "Pendiente", pagina, e.Row.Cells(14).Text)

            If fila.Row("Estado") <> "Pendiente" Then
                e.Row.Cells(0).Text = ""
            Else
                j = j + 1
            End If

            If j >= 1 Then
                Me.btnEvaluar.Enabled = True
                Me.btnRechazar.Enabled = True
            ElseIf j = 0 Then
                Me.btnEvaluar.Enabled = False
                Me.btnRechazar.Enabled = False
            End If

        End If
    End Sub

    Private Sub CargaMeses()
        Me.ddlMes.Items.Add("TODOS")
        Me.ddlMes.Items.Add("Enero")
        Me.ddlMes.Items.Add("Febrero")
        Me.ddlMes.Items.Add("Marzo")
        Me.ddlMes.Items.Add("Abril")
        Me.ddlMes.Items.Add("Mayo")
        Me.ddlMes.Items.Add("Junio")
        Me.ddlMes.Items.Add("Julio")
        Me.ddlMes.Items.Add("Agosto")
        Me.ddlMes.Items.Add("Setiembre")
        Me.ddlMes.Items.Add("Octubre")
        Me.ddlMes.Items.Add("Noviembre")
        Me.ddlMes.Items.Add("Diciembre")
    End Sub

    Protected Sub btnEvaluar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEvaluar.Click

        Dim vMensaje As String
        Dim Fila As GridViewRow
        Dim y As Integer
        '--Se añadió para ver si hay más de un registro seleccionado de la lista---------
        For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
            Fila = Me.gvCarga.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    y = y + 1
                End If
            End If
        Next

        If y > 1 Then
            'Limpia los checks seleccionados
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = False
                    End If
                End If
            Next
            'Muestra un mensaje
            vMensaje = "*Atención! Solo puede seleccionar un registro de la lista"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            ConsultarListaSolicitudes()
            limpiar_mensajes()
            Exit Sub
        End If
        '--Hasta acá------------------------------------------------------------------

        Try
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        parteEvaluar()
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                vMensaje = "*Aviso : Debe seleccionar un registro de la lista"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                ConsultarListaSolicitudes()
                limpiar_mensajes()
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteEvaluar()

        ctf = Session("codigo_ctf") 'ctf Director de Área

        Dim Fila As GridViewRow
        Try
            Dim x As Integer
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        Dim cod, codi As Integer
                        cod = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")
                        codi = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST")

                        registrar_filtros()

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Pendiente" Then 'Enviado por colaborador                        
                            Response.Redirect("AprobacionSolicitudTramite.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram)
                        Else
                            Me.lblMensaje0.Text = "** NOTA :  La Solicitud de Trámite ya se ha enviado a Personal"
                        End If
                    Else
                        x = x + 1
                    End If
                End If
            Next

            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "** NOTA : Debe seleccionar un registro de la lista"
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarListaSolicitudes()

    End Sub

    Protected Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click

        Dim vMensaje As String = ""
        Dim Fila As GridViewRow
        Dim y As Integer
        '--Se añadió para ver si hay más de un registro seleccionado de la lista---------
        For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
            Fila = Me.gvCarga.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    y = y + 1
                End If
            End If
        Next
        If y > 1 Then
            'Limpia los checks seleccionados
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = False
                    End If
                End If
            Next
            vMensaje = "*Atención! Solo puede seleccionar un registro de la lista"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            ConsultarListaSolicitudes()
            limpiar_mensajes()
            Exit Sub
        End If
        '--Hasta acá------------------------------------------------------------
        Try
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        'Se añade que no permita Rechazar si es una solicitud de Vacaciones
                        'Response.Write(gvCarga.DataKeys.Item(Fila.RowIndex).Values("Tipo_Solicitud"))
                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Tipo_Solicitud") = "Vacaciones" Then
                            vMensaje = "*Aviso : No puede rechazar directamente una solicitud de Vacaciones. Debe Evaluar y registrar el Motivo de Rechazo."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Exit Sub
                        End If
                        '------------------------------------------------------------------
                        Me.divListado.Visible = False
                        Me.divConfirmaRechazar.Visible = True
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                vMensaje = "*Aviso : Debe seleccionar un registro de la lista"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                ConsultarListaSolicitudes()
                limpiar_mensajes()
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteRechazar()

        Dim Fila As GridViewRow
        Try

            Dim x As Integer
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        Dim cod_ST, cod_Per, cod_EST As Integer 'Se añade 16/07
                        cod_ST = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST") 'Se añade 16/07
                        cod_Per = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_Per") 'Se añade 17/07
                        cod_EST = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST") 'Se añade 17/07
                        obj.Ejecutar("RechazaSolicitudTramite", cod_ST, cod_EST, CInt(Session("id_per")), "")
                        Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha Rechazado correctamente"

                        '----Se añade 16/07 envía correo para Rechazo de Solicitudes------------
                        Dim envio As Integer
                        Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                        Dim dt1 As New Data.DataTable
                        envio = ObjCnx1.TraerValor("PER_ConsultaEnvioCorreoSolicitudTramite", cod_ST, "D", "RE") '"RE" de Rechazo
                        If envio = 0 Then
                            obj.AbrirConexion()
                            obj.Ejecutar("PER_EnviaCorreoSolicitudTramite", cod_ST, "D", "RE") '"RE" de Rechazo
                            obj.CerrarConexion()
                            EnviaCorreo(cod_Per, cod_EST, "R") 'Letra R(rechazo). Envía correo de Rechazo
                        End If
                        '-----------------------------------------------------------------------
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "** NOTA : Debe seleccionar un registro de la lista"
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarListaSolicitudes()

    End Sub

    Protected Sub btnConfirmaRechazarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRechazarSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaRechazar.Visible = False
        parteRechazar()
    End Sub

    Protected Sub btnConfirmaRechazarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRechazarNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaRechazar.Visible = False
        ConsultarListaSolicitudes()
        respuesta = "C"
        valida_respuesta()
    End Sub

    Private Sub limpiar_mensajes()
        lblMensaje0.text = ""
        lblMensaje.text = ""
    End Sub

    Private Function EnviaCorreo(ByVal codigo_Per As Integer, ByVal cod_EST As Integer, ByVal tipo As String) As Boolean

        Dim strMensaje As String
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim EmailDestino As String = ""
        Dim colaborador, solicitud As String
        Dim valorSol As Integer

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaDatosColaborador", codigo_Per)
            obj.CerrarConexion()

            colaborador = dt.Rows(0).Item("Colaborador")

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = dt.Rows(0).Item("email")
                End If
            Else
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = "cgastelo@usat.edu.pe"
                End If
            End If

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", cod_EST)
            obj.CerrarConexion()

            valorSol = dt.rows(0).item("codigo_ST") 'número de la solicitud

            If dt.rows(0).item("codigo_TST") < 3 Then
                solicitud = "Solicitud de Licencia"
            ElseIf dt.rows(0).item("codigo_TST") = 4 Then
                solicitud = "Solicitud de Permiso por Horas"
            ElseIf dt.rows(0).item("codigo_TST") = 3 Then 'Se añadió
                solicitud = "Solicitud de Vacaciones"
            End If

            If tipo = "R" Then 'Para Rechazo
                strMensaje = "Estimado colaborador (a) " & colaborador & ": <br/><br/>"
                strMensaje = strMensaje & "El presente mensaje es para comunicarle que su " & solicitud & ", N° " & valorSol & ", ha sido Rechazada por su Jefe inmediato.<br/>"
                strMensaje = strMensaje & "Puede revisar el detalle de la respuesta en su Campus Virtual. "
                strMensaje = strMensaje & "Si tiene consultas, por favor comuníquese con su superior.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Rechazo de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            cls = Nothing
            obj = Nothing

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Function

End Class