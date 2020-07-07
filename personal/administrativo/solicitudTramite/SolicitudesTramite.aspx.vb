﻿Partial Class _SolicitudesTramite
    Inherits System.Web.UI.Page
    Dim tip_Per As Integer
    Dim anio, mes As Integer
    Dim estado As String
    Dim j As Integer
    Dim respuesta As String
    Dim cod_ST As Integer
    Dim limite_vac As Integer = 15 '05-07-19 Para definir el día límite del mes para solicitar Vacaciones


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        If Not IsPostBack Then

            'Me.hdctf.Value = CInt(Request.QueryString("ctf"))

            If Request.QueryString("anio") <> "" Then
                Me.ddlAño.SelectedValue = Request.QueryString("anio")
            End If
            If Request.QueryString("mes") <> "" Then
                Me.ddlMes.SelectedValue = Request.QueryString("mes")
            End If
            If Request.QueryString("estado") <> "" Then
                Me.ddlEstado.SelectedValue = Request.QueryString("estado")
            End If
            If Request.QueryString("respuesta") <> "" Then
                respuesta = Request.QueryString("respuesta")
            End If

            Me.divConfirmaEliminar.Visible = False
            Me.divConfirmaEnviar.Visible = False

            lblMensaje.Visible = False 'Se añadió

            ConsultarAño() '02/01/2020

            ConsultarSolicitudes()

            valida_respuesta()

        End If
    End Sub

    Private Sub valida_respuesta()
        If respuesta = "G" Then
            Me.lblMensaje0.Text = "*NOTA : La Solicitud de Trámite se ha Guardado Correctamente"
        ElseIf respuesta = "M" Then
            Me.lblMensaje0.Text = "*NOTA : La Solicitud de Trámite se ha Modificado Correctamente"
        ElseIf respuesta = "GE" Then
            Me.lblMensaje0.Text = "*NOTA : La Solicitud de Trámite se ha Guardado y Enviado correctamente"
        ElseIf respuesta = "C" Then
            Me.lblMensaje0.Text = "** NOTA : Operación CANCELADA"
        ElseIf respuesta = "NG" Then
            Me.lblMensaje0.Text = "*Nota : No se Pudo Guardar la Solicitud de Trámite"
        ElseIf respuesta = "NE" Then
            Me.lblMensaje0.Text = "*NOTA: Error de Envío de la Solicitud de Trámite"
        End If
        Me.lblMensaje.Text = ""
    End Sub

    Public Sub ConsultarSolicitudes()

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
            dt = obj.TraerDataTable("ListaSolicitudTramite", anio, mes, CInt(Session("id_per")), estado)

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                'Me.celdaGrid.Visible = True
                'Me.celdaGrid.InnerHtml = "** AVISO :  NO EXISTEN SOLICITUDES CON LOS PARÁMETROS SELECCIONADOS"
                lblMensaje.Visible = True
                Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"

                Me.btnEditar.Enabled = False
                Me.btnEliminar.Enabled = False
                Me.btnEnviar.Enabled = False

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

    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        ConsultarSolicitudes()
    End Sub

    Protected Sub cboMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        ConsultarSolicitudes()
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        ConsultarSolicitudes()
    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'Dim chk As CheckBox = CType(e.Row.FindControl("CheckBox1"), CheckBox)
            'chk.Visible = False
            'e.Row.Cells(1).Text = e.Row.RowIndex + 1

            Dim cod As Integer
            cod = fila.Row("codigo_ST")

            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            'If e.Row.Cells(2).Text <> "Permiso" Then
            '    FormatDateTime(CDate(e.Row.Cells(4).Text), DateFormat.LongDate)
            'End If

            'e.Row.Cells(4).Style("FormatDateTime") = IIf(fila.Row("Tipo_Solicitud") <> "Permiso", DateFormat.ShortDate, DateFormat.GeneralDate)

            If e.Row.Cells(3).Text = "Urgente" Then
                e.Row.Cells(3).Font.Bold = False
                e.Row.Cells(3).ForeColor = System.Drawing.Color.Blue
                'e.Row.Cells(3).Attributes.Add("HyperLink", "../SolicitudTramite.aspx")
            End If

            ' ''Para Mostrar Evaluador
            'Dim dt As New Data.DataTable
            'Dim obj As New ClsConectarDatos

            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()
            'dt = obj.TraerDataTable("EvaluadorSolicitudTramite", cod_ST)
            'obj.CerrarConexion()
            'e.Row.Cells(10).Text = IIf(fila.Row("codigoPer_EST") = "", "AREA DE PERSONAL", dt.Rows(0).Item("Evaluador"))

            e.Row.Cells(12).ForeColor = IIf(fila.Row("Estado") = "Generado", Drawing.Color.Red, Drawing.Color.Blue)
            e.Row.Cells(12).Font.Underline = IIf(fila.Row("Estado") <> "Generado", True, False)
            Dim pagina As String = "<a href='SolicitudTramite.aspx?cod=" & cod & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "'>" & e.Row.Cells(12).Text & "</a>"
            'e.Row.Cells(11).Text = IIf(fila.Row("Estado") <> "Generado", "<a href='SolicitudTramite.aspx?cod=" + cod + "'>" + e.Row.Cells(11).Text + "</a>", e.Row.Cells(11).Text)
            e.Row.Cells(12).Text = IIf(fila.Row("Estado") <> "Generado", pagina, e.Row.Cells(12).Text)

            If fila.Row("Estado") <> "Generado" Then
                e.Row.Cells(0).Text = ""
            Else
                j = j + 1
            End If

            If j >= 1 Then
                Me.btnEditar.Enabled = True
                Me.btnEliminar.Enabled = True
                Me.btnEnviar.Enabled = True
            ElseIf j = 0 Then
                Me.btnEditar.Enabled = False
                Me.btnEliminar.Enabled = False
                Me.btnEnviar.Enabled = False
            End If

            'e.Row.Cells(11).Text = "<a href='www.google.com.pe'>Estado</a>"
            'If e.Row.Cells(10).Text = "Enviado" Then
            '    e.Row.Cells(10).Font.Bold = True
            '    e.Row.Cells(10).Font.Underline = True
            '    'e.Row.FindControl("CheckBox1").Visible = False
            'Else
            '    e.Row.Cells(10).Font.Bold = True
            '    e.Row.Cells(10).Font.Underline = True
            '    e.Row.Cells(10).ForeColor = System.Drawing.Color.Blue
            '    'e.Row.Enabled = False
            '    'e.Row.Cells(0).FindControl("CheckBox1").Visible = False
            'End If
            'CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
        End If
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.btnEditar.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") '& "&cco=" & Me.cboCecos.SelectedValue
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Response.Redirect("SolicitudTramite.aspx")
    End Sub

    Private Sub registrar_filtros()

        'Session("Filtromes") = Me.ddlMes.SelectedValue
        'Session("Filtroanio") = Me.ddlAño.SelectedValue
        'Session("Filtroestado") = Me.ddlEstado.SelectedValue
        anio = Me.ddlAño.SelectedValue
        mes = Me.ddlMes.SelectedValue
        estado = Me.ddlEstado.SelectedValue

    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditar.Click

        registrar_filtros()
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
            ConsultarSolicitudes()
            limpiar_mensajes()
            Exit Sub
        End If
        '--Hasta acá------------------------------------------------------------------
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
                        'Response.Write(gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado"))
                        Dim cod As Integer
                        cod = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Generado" Then
                            'Response.Write(cod)
                            Response.Redirect("SolicitudTramite.aspx?cod=" & cod & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado)
                            'Response.Write("SolicitudTramite.aspx?cod=" & cod)
                        Else
                            Me.lblMensaje0.Text = "* NOTA : LA SOLICITUD DE TRÁMITE YA SE HA ENVIADO Y NO SE PUEDE MODIFICAR"
                        End If
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                vMensaje = "*Aviso : Debe seleccionar un registro de la lista"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                ConsultarSolicitudes()
                limpiar_mensajes()
                Exit Sub
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click

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
            ConsultarSolicitudes()
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
                        Me.divListado.Visible = False
                        Me.divConfirmaEnviar.Visible = True
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                vMensaje = "*Aviso : Debe seleccionar un registro de la lista"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                ConsultarSolicitudes()
                limpiar_mensajes()
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteEnviar()

        Dim rptta As Integer
        Dim vMensaje As String = ""
        Dim Fila As GridViewRow

        Try
            'Dim obj As New ClsConectarDatos
            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        cod_ST = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")
                        'gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Enviado" Then
                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Generado" Then
                            '--------------Se añade 05-07 desde acá para evitar Envio de Vacaciones con fecha vencida al mes-----------------------
                            Dim Fecha_Ini As Date
                            Fecha_Ini = gvCarga.DataKeys.Item(Fila.RowIndex).Values("Fecha_Ini")
                            Dim Fecha_Fin As Date
                            Fecha_Fin = gvCarga.DataKeys.Item(Fila.RowIndex).Values("Fecha_Fin")
                            If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Tipo_Solicitud") = "Vacaciones" And Day(Today) > limite_vac And (Month(Fecha_Ini) = Month(Today) Or Month(Fecha_Fin) = Month(Today)) Then
                                vMensaje = "**Aviso : No puede solicitar vacaciones durante este mes (del " & Fecha_Ini & " al " & Fecha_Fin & ") por sobrepasar el día " & limite_vac & " establecido. Consulte con el Área de Personal"
                                Dim myscript As String = "alert('" & vMensaje & "')"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                                Me.lblMensaje0.Text = "** AVISO :  No se ha Enviado la Solicitud de Trámite"
                                Me.lblMensaje.Text = ""
                                Exit Sub
                            End If
                            '---------------------------------- 05-07 hasta acá----------------------------------

                            'obj.CerrarConexion()
                            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                            rptta = ObjCnx.TraerValor("CreaEvaluacionSolicitudTramite", CInt(Session("id_per")), cod_ST, "D") 'Se agregó CInt(session). Se crea el registro en la tabla EvaluaciónSolicitudTramite para el Director

                            If rptta = 0 Then
                                vMensaje = "**Aviso : No existe un Director asignado a su Centro de Costo. Consulte con el Área de Personal"
                                Dim myscript As String = "alert('" & vMensaje & "')"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                                Me.lblMensaje0.Text = "** AVISO :  No se ha Enviado la Solicitud de Trámite"
                                Me.lblMensaje.Text = ""
                                Exit Sub
                            ElseIf rptta = -1 Then
                                vMensaje = "**Aviso : Existe más de un Director asignado a su Centro de Costo. Por favor Consulte con el Área de Personal"
                                Dim myscript As String = "alert('" & vMensaje & "')"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                                Me.lblMensaje0.Text = "** AVISO :  No se ha Enviado la Solicitud de Trámite"
                                Me.lblMensaje.Text = ""
                                Exit Sub
                            ElseIf rptta = -2 Then 'Este aviso es solo para Director de Área que solicitan un trámite
                                vMensaje = "**Aviso : Su dependencia superior no tiene un Director asignado. Por favor Consulte con el Área de Personal"
                                Dim myscript As String = "alert('" & vMensaje & "')"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                                Me.lblMensaje0.Text = "** AVISO :  No se ha Enviado la Solicitud de Trámite"
                                Me.lblMensaje.Text = ""
                                Exit Sub
                            Else
                                ObjCnx.Ejecutar("EnviarSolicitudTramite", cod_ST) 'Procedimiento para Enviar al Director  
                                Me.lblMensaje0.Text = "** AVISO :  La Solicitud de Trámite se ha Enviado Correctamente"
                                Me.lblMensaje.Text = ""
                                'Se añadió Enviar Correo, no estaba
                                EnviaCorreo(rptta, "E")
                            End If
                        Else
                            Me.lblMensaje0.Text = "** NOTA :  La Solicitud de Trámite ya se ha Enviado"
                            Me.lblMensaje.Text = ""
                        End If

                    Else
                        x = x + 1
                    End If

                End If
            Next

            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "** NOTA : DEBE SELECCIONAR UN REGISTRO DE LA LISTA"
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarSolicitudes()

    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click

        Dim vMensaje As String
        Dim Fila As GridViewRow
        Dim y As Integer
        Try
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        Me.divListado.Visible = False
                        Me.divConfirmaEliminar.Visible = True
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                vMensaje = "*Aviso : Debe seleccionar un registro de la lista"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                ConsultarSolicitudes()
                limpiar_mensajes()
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteEliminar()
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
                        obj.Ejecutar("EliminaSolicitudTramite", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST"))
                        Me.lblMensaje0.Text = "** AVISO :  LA(S) SOLICITUD(ES) SE HAN ELIMINADO CORRECTAMENTE"
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "** NOTA : DEBE SELECCIONAR UN REGISTRO DE LA LISTA"
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarSolicitudes()

    End Sub

    'Private Sub CargaMeses()
    '    Me.ddlMes.Items.Add("TODOS")
    '    Me.ddlMes.Items.Add("Enero")
    '    Me.ddlMes.Items.Add("Febrero")
    '    Me.ddlMes.Items.Add("Marzo")
    '    Me.ddlMes.Items.Add("Abril")
    '    Me.ddlMes.Items.Add("Mayo")
    '    Me.ddlMes.Items.Add("Junio")
    '    Me.ddlMes.Items.Add("Julio")
    '    Me.ddlMes.Items.Add("Agosto")
    '    Me.ddlMes.Items.Add("Setiembre")
    '    Me.ddlMes.Items.Add("Octubre")
    '    Me.ddlMes.Items.Add("Noviembre")
    '    Me.ddlMes.Items.Add("Diciembre")
    'End Sub

    Protected Sub btnConfirmarEnviarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaEnviarSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaEnviar.Visible = False
        parteEnviar()
    End Sub

    Protected Sub btnConfirmarEnviarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaEnviarNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaEnviar.Visible = False
        ConsultarSolicitudes()
        respuesta = "C"
        valida_respuesta()
    End Sub

    Protected Sub btnConfirmarEliminarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarEliminarSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaEliminar.Visible = False
        parteEliminar()
    End Sub

    Protected Sub btnConfirmarEliminarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarEliminarNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaEliminar.Visible = False
        ConsultarSolicitudes()
        respuesta = "C"
        valida_respuesta()
    End Sub

    Protected Sub gvCarga_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvCarga.SelectedIndexChanging
        Me.btnEnviar.Enabled = True
        Me.btnEditar.Enabled = True
        Me.btnEliminar.Enabled = True
    End Sub

    Private Sub limpiar_mensajes()
        lblMensaje0.text = ""
        lblMensaje.text = ""
    End Sub

    Private Function EnviaCorreo(ByVal cod_EST As Integer, ByVal tipo As String) As Boolean
        Dim strMensaje As String
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim EmailDestino As String = ""
        Dim colaborador, solicitud, evaluador As String

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaDatosColaborador", CInt(Session("id_per")))
            obj.CerrarConexion()

            colaborador = dt.Rows(0).Item("Colaborador")

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", cod_EST)
            obj.CerrarConexion()

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = dt.Rows(0).Item("email")  'CORREO DE PRODUCCIÓN / Se cambió: email_Per a email                   
                End If
            Else
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = "cgastelo@usat.edu.pe"     'Correo para Desarrollo
                End If
            End If

            If dt.rows(0).item("codigo_TST") < 3 Then
                solicitud = "Solicitud de Licencia"
            ElseIf dt.rows(0).item("codigo_TST") = 4 Then
                solicitud = "Solicitud de Permiso"
            ElseIf dt.rows(0).item("codigo_TST") = 3 Then 'Se añadió
                solicitud = "Solicitud de Vacaciones"
            End If

            evaluador = dt.Rows(0).Item("Nombre_Evaluador")

            'Correo 
            If tipo = "E" Then
                strMensaje = "Estimado(a) " & evaluador & ": <br/>"
                strMensaje = strMensaje & "Responsable de " & dt.Rows(0).Item("CeCo_Evaluador") & "<br/><br/>"
                strMensaje = strMensaje & "El(la) colaborador(a): " & colaborador
                strMensaje = strMensaje & " acaba de enviar una  " & solicitud & " para su revisión.<br/>"
                strMensaje = strMensaje & "Favor de ingresar al Campus Virtual y realizar la evaluación correspondiente.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            cls = Nothing
            obj = Nothing

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try
    End Function

End Class
