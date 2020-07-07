﻿Imports System.IO
Imports System.Security.Cryptography
Partial Class _SolicitudesTramitePorDirector
    Inherits System.Web.UI.Page
    Dim ctf As Integer
    Dim mes As Integer
    Dim anio, estado, prioridad, tipo_tram, responsable, trabajador As String
    Dim j As Integer
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        Session("codigo_ctf") = Request.QueryString("ctf")

        If Not IsPostBack Then

            'Response.Write("anio: " & Request.QueryString("anio"))

            ConsultarAño() '02/01/2020

            If Request.QueryString("anio") <> "" Then
                Me.ddlAño.SelectedValue = Request.QueryString("anio")
                anio = Me.ddlAño.SelectedValue
            Else
                anio = Me.ddlAño.SelectedValue
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

            ConsultarDirectores()

            If Request.QueryString("responsable") <> "" Then
                Me.ddlResponsableArea.SelectedValue = Request.QueryString("responsable")
            Else
                Me.ddlResponsableArea.SelectedValue = "0"
            End If

            If Request.QueryString("trabajador") <> "" Then
                Me.txtTrabajador.Text = Request.QueryString("trabajador")
            End If

            If Request.QueryString("respuesta") <> "" Then
                respuesta = Request.QueryString("respuesta")
            End If

            Me.divConfirmaRechazar.Visible = False

            lblMensaje.Visible = False 'Se añadió

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

            dt = obj.TraerDataTable("ListaTipoSolicitudTramite", "T") 'T incluye el valor Todos
            obj.CerrarConexion()

            Me.ddlTipoSolicitud.DataTextField = "nombre_TST"
            Me.ddlTipoSolicitud.DataValueField = "codigo_TST"
            Me.ddlTipoSolicitud.DataSource = dt
            Me.ddlTipoSolicitud.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los Tipos de Solicitud Trámite"
        End Try
    End Sub

    Public Sub ConsultarDirectores()
        Try

            Dim obj As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlResponsableArea.Items.Clear()
            Me.ddlResponsableArea.DataBind()
            objFun.CargarListas(Me.ddlResponsableArea, obj.TraerDataTable("ResponsablesAreasPOA", anio), "codigo_Per", "Responsable")

            obj.CerrarConexion()
            obj = Nothing
            objFun = Nothing

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los Directores"
        End Try
    End Sub

    Public Sub ConsultarListaSolicitudes()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""
        Me.lblMensaje.Text = ""

        registrar_filtros()

        j = 0

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ListaEvaluacionSolicitudTramite", anio, mes, responsable, estado, prioridad, tipo_tram, Trim(Me.txtTrabajador.Text))

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

        If Me.ddlResponsableArea.SelectedValue = "0" Then
            responsable = 0
        Else
            responsable = Me.ddlResponsableArea.SelectedValue
        End If

        trabajador = Trim(Me.txtTrabajador.Text)

    End Sub

    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub cboMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub cboPrioridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrioridad.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub ddlTipoSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub ddlResponsableArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlResponsableArea.SelectedIndexChanged
        ConsultarListaSolicitudes()
        Me.lblMensaje0.Text = ""
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

            e.Row.Cells(15).ForeColor = IIf(fila.Row("Estado") = "Pendiente", Drawing.Color.Red, Drawing.Color.Blue)
            e.Row.Cells(15).Font.Underline = IIf(fila.Row("Estado") <> "Pendiente", True, False)
            Dim pagina As String = "<a href='AprobacionSolicitudTramite.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&responsable=" & responsable & "&trabajador=" & trabajador & "'>" & e.Row.Cells(15).Text & "</a>"
            e.Row.Cells(15).Text = IIf(fila.Row("Estado") <> "Pendiente", pagina, e.Row.Cells(15).Text)

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

    Protected Sub Unnamed1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click, btnBuscar.Click

        Busqueda()

    End Sub

    Private Sub Busqueda()

        Me.lblMensaje0.Text = ""

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""

        If Trim(Me.txtTrabajador.Text) = "" Then
            Me.lblMensaje0.Text = "** Aviso: No ha ingresado el APELLIDO o NOMBRE del trabajador"
            ConsultarListaSolicitudes()
            Exit Sub
        End If

        'Response.Write(Trim(Me.txtTrabajador.Text))
        registrar_filtros()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListaEvaluacionSolicitudTramite", anio, mes, responsable, estado, prioridad, tipo_tram, Trim(Me.txtTrabajador.Text))

        If tb.Rows.Count Then
            Me.gvCarga.DataSource = tb
            'Me.lblLista.Text = "B" 'De búsqueda
        Else
            Me.gvCarga.DataSource = Nothing
            Me.gvCarga.DataBind()
            lblMensaje.Visible = True 'Se añadió
            Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"
            'Me.lblLista.Text = "L" 'Lista general
        End If

        Me.gvCarga.DataBind()
        obj.CerrarConexion()
        obj = Nothing

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
                            Response.Redirect("AprobacionSolicitudTramite.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&responsable=" & responsable)
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
                        obj.Ejecutar("RechazaSolicitudTramite", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST"), gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST"), CInt(Session("id_per")), "")
                        Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha Rechazado correctamente"
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

    Protected Sub btnExporta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExporta.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvCarga.EnableViewState = False
        gvCarga.Visible = True
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvCarga)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"

        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        gvCarga.Visible = False
    End Sub

    Private Sub limpiar_mensajes()
        lblMensaje0.text = ""
        lblMensaje.text = ""
    End Sub

End Class
