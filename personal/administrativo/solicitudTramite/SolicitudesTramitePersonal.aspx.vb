Imports System.IO
Imports System.Security.Cryptography

Partial Class _SolicitudesTramitePersonal
    Inherits System.Web.UI.Page
    Dim ctf As Integer
    Dim mes, j, y As Integer
    Dim anio, estado, prioridad, tip_sol As String
    Dim fecha_ini, fecha_fin As Date
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        Session("codigo_ctf") = Request.QueryString("ctf")

        If Not IsPostBack Then

            'Se añadió los filtros de fecha:
            Me.txtFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Me.txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy")

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
            If Request.QueryString("tip_sol") <> "" Then
                Me.ddlTipoSolicitud.SelectedValue = Request.QueryString("tip_sol")
            End If

            If Request.QueryString("fecha_ini") <> "" Then
                Me.txtFechaInicio.Text = Request.QueryString("fecha_ini")
            Else
                Me.txtFechaInicio.Text = DateSerial(Year(Now.Date), Now.Month, Now.Day - 1)
            End If
            If Request.QueryString("fecha_fin") <> "" Then
                Me.txtFechaFin.Text = Request.QueryString("fecha_fin")
            Else
                Me.txtFechaFin.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
            End If

            If Request.QueryString("respuesta") <> "" Then
                respuesta = Request.QueryString("respuesta")
            End If

            If CInt(Session("codigo_ctf")) = 198 Then 'TipoFuncion: CONTROL LICENCIAS Y PERMISOS. EN Produccion es 198
                Me.lblTipo_Solic.Text = "Licencias y Permisos por Horas"
                Me.lblTipo_Solic1.Text = "Licencias y Permisos por Horas"
                Me.ddlTipoSolicitud.Items.FindByValue("V").Enabled = False 'Se añadió 07-02-19
            ElseIf CInt(Session("codigo_ctf")) = 199 Then 'TipoFuncion: CONTROL PERMISOS. EN Produccion es 199
                Me.lblTipo_Solic.Text = "Permisos por Horas"
                Me.lblTipo_Solic1.Text = "Permisos por Horas"
                Me.ddlTipoSolicitud.Visible = False 'Se oculta porque solo tiene acceso a permisos
            ElseIf CInt(Session("codigo_ctf")) = 137 Or CInt(Session("codigo_ctf")) = 141 Then 'TipoFuncion: SUPERVISOR DE PERSONAL, DIRECTOR DE PERSONAL
                Me.lblTipo_Solic.Text = "Licencias, Permisos por Horas y Vacaciones" 'Se cambia título
                Me.lblTipo_Solic1.Text = "Licencias, Permisos por Horas y Vacaciones" 'Se cambia título
            End If

            Me.divConfirmaRechazar.Visible = False
            Me.divConfirmaCancelar.Visible = False

            lblMensaje.Visible = False 'Se añadió

            j = 0
            y = 0

            valida_respuesta()
            'Me.btnCancelar.Visible = False
            'valida_estado()

            ConsultarAño() '02/01/2020

            ConsultarListaSolicitudesPersonal()

        End If

    End Sub

    Private Sub valida_respuesta()

        If respuesta = "A" Then
            Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha aprobado y se ha creado el registro de permiso correctamente"""
        ElseIf respuesta = "R" Then
            Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha Rechazado correctamente"
        ElseIf respuesta = "C" Then
            Me.lblMensaje0.Text = "** NOTA : Operación Cancelada"
        ElseIf respuesta = "GO" Then
            Me.lblMensaje0.Text = "** NOTA : Se ha Guardado la Observación del registro correctamente"
        End If

    End Sub

    Private Sub valida_estado()
                                   
        If Me.ddlEstado.SelectedValue = "AP" Then
            Me.btnAprobar.Enabled = False
            Me.btnRechazar.Enabled = False
            Me.btnCancelar.Visible = True
            btnAnadirObservacion.enabled = True 'se añade la observacion cuando ya se aprobaron las solicitudes

        ElseIf Me.ddlEstado.SelectedValue = "AD" Then 'Pendientes

            Me.btnAprobar.Enabled = True
            Me.btnRechazar.Enabled = True
            Me.btnCancelar.Visible = False

            If ddlTipoSolicitud.selectedvalue = "V" Then 'Se Añade por vacaciones
                Me.btnAprobar.Enabled = False
                Me.btnAprobar.visible = False
            Else
                Me.btnAprobar.Enabled = True
                Me.btnAprobar.visible = True
            End If

        ElseIf Me.ddlEstado.SelectedValue = "TO" Then 'Todos

            If ddlTipoSolicitud.selectedvalue = "V" Then 'Se Añade por vacaciones
                Me.btnAprobar.Enabled = False
                Me.btnAprobar.visible = False
                Me.btnCancelar.Visible = False
            End If

        ElseIf Me.ddlEstado.SelectedValue = "RE" Then
            Me.btnAprobar.Enabled = False
            Me.btnRechazar.Enabled = False
            Me.btnCancelar.Visible = False
        End If

    End Sub

    Public Sub ConsultarListaSolicitudesPersonal()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""

        registrar_filtros()
        j = 0
        y = 0
        ctf = Session("codigo_ctf")

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaSolicitudTramitePersonal", anio, mes, ctf, estado, prioridad, tip_sol, fecha_ini, fecha_fin)

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()

                'Response.Write(j & y)
                valida_estado() 'Se añade 04-03-19

                If j >= 1 Then
                    btnAnadirObservacion.enabled = True 'se añade, si hay más de uno Aprobado personal

                    If ddlTipoSolicitud.selectedvalue = "V" Then 'Se Añade por vacaciones
                        Me.btnCancelar.Visible = False 'es cuando ya se aprobó y se revertirá la aprobación
                    Else
                        Me.btnCancelar.Visible = True
                        Me.btnCancelar.Enabled = True
                    End If
                Else
                    btnAnadirObservacion.enabled = False
                End If

                If y >= 1 Then 'Los pendientes
                    If ddlTipoSolicitud.selectedvalue = "V" Then 'Se Añade por vacaciones
                        Me.btnAprobar.visible = False
                    Else
                        Me.btnAprobar.Enabled = True
                        Me.btnAprobar.visible = True
                    End If
                    
                    Me.btnRechazar.Enabled = True
                    Me.btnRechazar.visible = True
                End If

            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                'Me.celdaGrid.Visible = True
                'Me.celdaGrid.InnerHtml = "** AVISO :  No Existen Solicitudes con los parámetros seleccionados"
                Me.lblMensaje.Visible = True 'Se añadió
                Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"

                Me.btnAprobar.Enabled = False 'Se añadió
                Me.btnRechazar.Enabled = False 'Se añadió
                Me.btnCancelar.Enabled = False 'Se añadió
                Me.btnAnadirObservacion.enabled = False

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
        tip_sol = Me.ddlTipoSolicitud.SelectedValue 'L o P : Licencias o Permisos
        fecha_ini = CDate(Me.txtFechaInicio.text) 'Se añadió
        fecha_fin = CDate(Me.txtFechaFin.text) 'Se añadió

    End Sub

    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""
        ConsultarListaSolicitudesPersonal()
    End Sub

    Protected Sub cboMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""

        ConsultarListaSolicitudesPersonal()

    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged

        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""

        ConsultarListaSolicitudesPersonal()

    End Sub

    Protected Sub cboPrioridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrioridad.SelectedIndexChanged
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""

        ConsultarListaSolicitudesPersonal()
    End Sub

    Protected Sub cboTipoSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.SelectedIndexChanged

        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""

        ConsultarListaSolicitudesPersonal()

        'If ddlTipoSolicitud.selectedvalue = "V" Then 'Se añade para no evaluar las Vacaciones ya que se hace desde el Sistema de Planillas
        '    btnAprobar.visible = False
        '    btnRechazar.visible = False
        '    btnCancelar.visible = False
        'Else
        '    btnAprobar.visible = True
        '    btnRechazar.visible = True
        '    btnCancelar.visible = True
        'End If

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

            If e.Row.Cells(4).Text = "Urgente" Then
                e.Row.Cells(4).Font.Bold = False
                e.Row.Cells(4).ForeColor = System.Drawing.Color.Blue
                'e.Row.Cells(4).Attributes.Add("HyperLink", "../SolicitudTramite.aspx")
            End If

            e.Row.Cells(15).ForeColor = IIf(fila.Row("estado") = "Pendiente", Drawing.Color.Red, Drawing.Color.Blue)
            e.Row.Cells(15).Font.Underline = IIf(fila.Row("estado") <> "Pendiente", True, False)
            Dim pagina As String = "<a href='AprobacionSolicitudTramitePersonal.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin & "'>" & e.Row.Cells(15).Text & "</a>"
            'e.Row.Cells(11).Text = IIf(fila.Row("Estado") <> "Generado", "<a href='SolicitudTramite.aspx?cod=" + cod + "'>" + e.Row.Cells(11).Text + "</a>", e.Row.Cells(11).Text)
            e.Row.Cells(15).Text = IIf(fila.Row("estado") <> "Pendiente", pagina, e.Row.Cells(15).Text)

            If fila.Row("estado") <> "Pendiente" And fila.Row("estado") <> "Aprobado Personal" Then
                e.Row.Cells(0).Text = ""
            End If
            If fila.Row("estado") = "Aprobado Personal" Then
                j = j + 1
            End If
            If fila.Row("estado") = "Pendiente" Then
                y = y + 1
            End If

        End If

    End Sub

    Private Sub verifica_check()

        Dim vMensaje As String
        Dim Fila As GridViewRow
        Dim x As Integer
        x = 0
        For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
            Fila = Me.gvCarga.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    x = x + 1
                End If
            End If
        Next

        If x > 1 Then
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
            Exit Sub
        End If
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click

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
            ConsultarListaSolicitudesPersonal()
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

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Pendiente" Then
                            'parteAprobar()
                            ctf = Session("codigo_ctf") 'ctf 

                            Dim cod, codi As Integer
                            cod = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")
                            codi = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST")

                            registrar_filtros()

                            Response.Redirect("AprobacionSolicitudTramitePersonal.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin)

                        Else
                            Me.lblMensaje0.Text = "** NOTA :  La Solicitud de Trámite ya se ha Evaluado"
                            ConsultarListaSolicitudesPersonal()
                            Exit Sub

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
                ConsultarListaSolicitudesPersonal()
                limpiar_mensajes()
                Exit Sub
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteAprobar()

        ctf = Session("codigo_ctf") 'ctf 

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

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Pendiente" Then 'Lo pendiente es lo Enviado por Director                        
                            Response.Redirect("AprobacionSolicitudTramitePersonal.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad)
                        Else
                            Me.lblMensaje0.Text = "** NOTA :  La Solicitud de Trámite ya se ha Evaluado"
                            ConsultarListaSolicitudesPersonal()
                            Exit Sub
                        End If
                    Else
                        x = x + 1
                    End If
                End If
            Next

            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "** NOTA : Debe Seleccionar un registro de la lista"
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarListaSolicitudesPersonal()

    End Sub

    Protected Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click

        Dim vMensaje As String
        Dim Fila As GridViewRow
        Dim y As Integer
        '--Se añadió para ver si hay más de un registro seleccionado------------
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
            ConsultarListaSolicitudesPersonal()
            limpiar_mensajes()
            Exit Sub
        End If
        '--Hasta acá-----------------------------------------------------

        Try
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Pendiente" Then
                            Me.divListado.Visible = False
                            Me.divConfirmaRechazar.Visible = True
                        Else
                            If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Tipo_Solicitud") = "Vacaciones" Then
                                Me.lblMensaje0.Text = "** NOTA : No puede Rechazar la Solicitud de Vacaciones, ya se ha Evaluado'"
                            Else
                                Me.lblMensaje0.Text = "** NOTA : No puede Rechazar, La Solicitud ya se ha Evaluado. Si desea Eliminarla puede cancelarla con el botón: 'Cancelar Solicitud'"
                            End If
                            ConsultarListaSolicitudesPersonal()
                            Exit Sub
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
                ConsultarListaSolicitudesPersonal()
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
                        Dim cod_per, codi, cod As Integer
                        cod_per = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_Per")
                        codi = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST")
                        cod = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")
                        obj.Ejecutar("RechazaSolicitudTramite", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST"), gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST"), CInt(Session("id_per")), Trim(Me.txtObservacionPer.Text))
                        Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha Rechazado correctamente"

                        Dim envio As Integer
                        Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                        Dim dt1 As New Data.DataTable
                        envio = ObjCnx1.TraerValor("PER_ConsultaEnvioCorreoSolicitudTramite", cod, "P", "RE") '16/07/19 Se añade "RE"
                        If envio = 0 Then 'se añade 22/04/19
                            obj.AbrirConexion()
                            obj.Ejecutar("PER_EnviaCorreoSolicitudTramite", cod, "P", "RE") '16/07 Se añade "RE"/Se añadió 13/05/2019 para confirmar el envio de correo
                            obj.CerrarConexion()
                            'Se añade correo de Confirmación de RECHAZO
                            EnviaCorreo(cod_per, codi, "R") '----Se añadió el envío de correo para todo Tipo Solicitud
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

        ConsultarListaSolicitudesPersonal()

    End Sub

    Protected Sub btnConfirmaRechazarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRechazarSI.Click

        If Me.txtObservacionPer.Text = "" Then
            Dim vMensaje As String = ""
            vMensaje = "* ATENCIÓN: Debe indicar el motivo de rechazo en la Observación"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            btnConfirmaRechazarSI.enabled = True 'Se añadió
            Exit Sub
        Else
            Me.divListado.Visible = True
            Me.divConfirmaRechazar.Visible = False
            parteRechazar()
        End If

    End Sub

    Protected Sub btnConfirmaRechazarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaRechazarNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaRechazar.Visible = False
        ConsultarListaSolicitudesPersonal()
        respuesta = "C"
        valida_respuesta()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        Dim vMensaje As String
        Dim Fila As GridViewRow
        Dim y As Integer
        '--Se añadió para ver si hay más de un registro seleccionado------------
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
            ConsultarListaSolicitudesPersonal()
            limpiar_mensajes()
            Exit Sub
        End If
        '--Hasta acá-----------------------------------------------------

        Try
            Dim x As Integer
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Aprobado Personal" Then
                            Me.divListado.Visible = False
                            Me.divConfirmaCancelar.Visible = True
                        Else
                            Me.lblMensaje0.Text = "** NOTA : No puede Cancelar la Solicitud porque No ha sido Aprobada"
                            ConsultarListaSolicitudesPersonal()
                            Exit Sub
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
                ConsultarListaSolicitudesPersonal()
                limpiar_mensajes()
                Exit Sub
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub parteCancelar()

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
                        obj.Ejecutar("CancelaSolicitudTramite", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST"))
                        Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha CANCELADO y se ha Eliminado el registro del Permiso correspondiente"
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

        ConsultarListaSolicitudesPersonal()

    End Sub

    Protected Sub btnConfirmaCancelarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaCancelarSI.Click
        Me.divListado.Visible = True
        Me.divConfirmaCancelar.Visible = False
        parteCancelar()
    End Sub

    Protected Sub btnConfirmaCancelarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmaCancelarNO.Click
        Me.divListado.Visible = True
        Me.divConfirmaCancelar.Visible = False
        ConsultarListaSolicitudesPersonal()
        respuesta = "C"
        valida_respuesta()
    End Sub

    Protected Sub txtFechaInicio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFechaInicio.TextChanged
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""
        ConsultarListaSolicitudesPersonal()
        'valida_estado() 'se añade
    End Sub

    Protected Sub txtFechaFin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFechaFin.TextChanged
        Me.lblMensaje0.Text = ""
        Me.lblMensaje.Text = ""
        ConsultarListaSolicitudesPersonal()
        'valida_estado() 'se añade
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

    Protected Sub btnAnadirObservacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnadirObservacion.Click

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
            ConsultarListaSolicitudesPersonal()
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

                        If gvCarga.DataKeys.Item(Fila.RowIndex).Values("Estado") = "Aprobado Personal" Then

                            ctf = Session("codigo_ctf") 'ctf 

                            Dim cod, codi As Integer
                            cod = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_ST")
                            codi = gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_EST")

                            registrar_filtros()

                            Response.Redirect("AprobacionSolicitudTramitePersonal.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin)

                        Else
                            Me.lblMensaje0.Text = "** NOTA :  La Solicitud de Trámite está Pendiente de Revisión. La observación debe añadirse después de su Aprobación"
                            ConsultarListaSolicitudesPersonal()
                            Exit Sub

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
                ConsultarListaSolicitudesPersonal()
                limpiar_mensajes()
                Exit Sub
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

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

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then 'Se cambia 16/07
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = dt.Rows(0).Item("email") 'Se cambio: email_Per a email
                End If
            Else
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = "cgastelo@usat.edu.pe" 'Correo de Desarrollo
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

            'Correo 
            If tipo = "R" Then 'Para Rechazo

                strMensaje = "Estimado colaborador (a) " & colaborador & ": <br/><br/>"
                strMensaje = strMensaje & "El presente mensaje es para comunicarle que su " & solicitud & ", N° " & valorSol & ", ha sido Rechazada por el área de Personal.<br/>"
                strMensaje = strMensaje & "Puede revisar el detalle de la respuesta en su Campus Virtual. "
                strMensaje = strMensaje & "Si tiene consultas, por favor comuníquese con el área mencionada.<br/><br/>"
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
