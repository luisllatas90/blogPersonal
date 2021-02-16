Imports System.Net
Imports System.IO
Imports System.Drawing

Partial Class Alumni_frmEmpresa
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_Categoria As New d_Categoria
    Dim md_Departamento As New d_Departamento
    Dim md_Provincia As New d_Provincia
    Dim md_Distrito As New d_Distrito
    Dim md_Empresa As New d_Empresa
    Dim md_InformacionContacto As New d_InformacionContacto
    Dim md_EnvioCorreosMasivo As New d_EnvioCorreosMasivo
    Dim md_Personal As New d_Personal
    Dim md_CarreraProfesional As New d_CarreraProfesional
    Dim md_Sector As New d_Sector
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_ArchivoCompartidoDetalle As New d_ArchivoCompartidoDetalle

    'ENTIDADES
    Dim me_Categoria As e_Categoria
    Dim me_Departamento As e_Departamento
    Dim me_Provincia As e_Provincia
    Dim me_Distrito As e_Distrito
    Dim me_Empresa As e_Empresa
    Dim me_InformacionContacto As e_InformacionContacto
    Dim me_Personal As e_Personal
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_EnvioCorreosMasivo As e_EnvioCorreosMasivo
    Dim me_ArchivoCompartido As e_ArchivoCompartido
    Dim me_ArchivoCompartidoDetalle As e_ArchivoCompartidoDetalle

    'VARIABLES    
    Dim cod_user As Integer = 0
    Dim miCookie As New CookieContainer

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("https://intranet.usat.edu.pe/campusvirtual/sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Session("frmEmpresa-cod_ctf") = Request.QueryString("ctf")
                Call mt_CargarComboDepartamento()
                Call mt_CargarComboEstadoFiltro()
                Call mt_CargarComboTipo()
                Call mt_CargarComboSector()
                Call mt_CargarComboTelefono()
                Call mt_CargarComboDenominacionContacto()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmEmpresa-codigo_emp") = 0
            Call mt_LimpiarControles()

            Call mt_UpdatePanel("Registro")
            Call mt_UpdatePanel("Logo")

            Call mt_FlujoTabs("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try            
            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        Try
            Call mt_CargarComboProvincia()            

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        Try
            Call mt_CargarComboDistrito()

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnBuscarEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarEmpresa.Click
        Try
            Me.txtRazonSocial.Text = String.Empty
            Me.txtNombreComercial.Text = String.Empty
            Me.txtDireccion.Text = String.Empty

            If Not fu_ValidarConsultarCaptcha() Then Exit Sub

            Call mt_ConsultarCaptcha()
            
            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not mt_RegistrarEmpresa() Then
                Call mt_FlujoTabs("Registro")
                Exit Sub
            End If

            Call mt_CargarDatos()

            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmEmpresa-codigo_emp") = Me.grwLista.DataKeys(index).Values("codigo_emp").ToString
            Session("frmEmpresa-idPro") = Me.grwLista.DataKeys(index).Values("idPro").ToString

            If Not fu_ValidarSeleccionEmpresa(Session("frmEmpresa-codigo_emp"), e.CommandName) Then Exit Sub

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_CargarFormularioRegistro(Session("frmEmpresa-codigo_emp")) Then Exit Sub

                    Call mt_UpdatePanel("Registro")
                    Call mt_UpdatePanel("Logo")

                    Call mt_FlujoTabs("Registro")

                Case "GestionarContacto"
                    Call mt_CargarDatosContacto(Session("frmEmpresa-codigo_emp"))

                    Call mt_FlujoTabs("Contactos")

                Case "AccesoCampus"
                    If Not mt_CargarFormularioAcceso(Session("frmEmpresa-codigo_emp")) Then Exit Sub

                    Call mt_UpdatePanel("AccesoCampus")

                    Call mt_FlujoModal("AccesoCampus", "open")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirContacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirContacto.Click
        Try
            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevoContacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoContacto.Click
        Try
            Session("frmEmpresa-codigo_inc") = 0

            Call mt_LimpiarControlesContacto()

            Call mt_UpdatePanel("RegistroContacto")

            Call mt_FlujoModal("RegistrarContacto", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarContacto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarContacto.Click
        Try
            If Not mt_RegistrarContacto() Then Exit Sub

            Call mt_CargarDatosContacto(Session("frmEmpresa-codigo_emp"))

            Call mt_FlujoModal("RegistrarContacto", "close")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwContactos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwContactos.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmEmpresa-codigo_inc") = Me.grwContactos.DataKeys(index).Values("codigo_inc").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_CargarFormularioRegistroContacto(Session("frmEmpresa-codigo_inc")) Then Exit Sub

                    Call mt_UpdatePanel("RegistroContacto")

                    Call mt_FlujoModal("RegistrarContacto", "open")

                Case "Eliminar"
                    If Not mt_EliminarContacto(Session("frmEmpresa-codigo_inc")) Then Exit Sub

                    Call mt_CargarDatosContacto(Session("frmEmpresa-codigo_emp"))
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEnviarAcceso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarAcceso.Click
        Try
            If Not mt_EnviarAccesoCampus() Then Exit Sub

            Call mt_CargarDatos()

            Call mt_FlujoModal("AccesoCampus", "close")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogo.Click
        Try
            Call mt_FlujoModal("Logo", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Registro"                    
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

                Case "Contactos"
                    Me.udpContactos.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpContactosUpdate", "udpContactosUpdate();", True)

                Case "RegistroContacto"
                    Me.udpRegistroContacto.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroContactoUpdate", "udpRegistroContactoUpdate();", True)

                Case "AccesoCampus"
                    Me.udpAccesoCampus.Update()

                Case "Logo"
                    Me.udpLogo.Update()
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try            
            Select Case ls_tab
                Case "Registro"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)

                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "Contactos"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('contactos-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoModal(ByVal ls_modal As String, ByVal ls_accion As String)
        Try
            Select Case ls_accion
                Case "open"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & ls_modal & "');", True)

                Case "close"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal('" & ls_modal & "');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            Me.cmbDepartamento.SelectedValue = "0"
            Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
            Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
            Me.txtRuc.Text = String.Empty
            Me.cmbEstado.SelectedValue = g_VariablesGlobales.EstadoEmpresaAprobado
            Me.txtRazonSocial.Text = String.Empty
            Me.txtNombreComercial.Text = String.Empty
            Me.txtAbreviatura.Text = String.Empty
            Me.cmbTipoEmpresa.SelectedValue = "0"
            Me.cmbSector.SelectedValue = "0"
            Me.txtDireccion.Text = String.Empty
            Me.txtDireccionWeb.Text = String.Empty
            Me.txtCorreo.Text = String.Empty
            Me.cmbTelefono.SelectedValue = String.Empty
            Me.txtTelefono.Text = String.Empty
            Me.txtCelular.Text = String.Empty
            Me.txtLogo.Text = String.Empty
            Me.imgLogo.ImageUrl = g_VariablesGlobales.RutaLogos + "sinimagen.png"
            Me.chkAccesoCampus.Checked = False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesContacto()
        Try
            Me.cmbDenominacionContacto.SelectedValue = String.Empty
            Me.txtNombresContacto.Text = String.Empty
            Me.txtApellidosContacto.Text = String.Empty
            Me.txtCargoContacto.Text = String.Empty
            Me.txtCorreoContacto1.Text = String.Empty
            Me.txtCorreoContacto2.Text = String.Empty
            Me.cmbTelefonoContacto.SelectedValue = String.Empty
            Me.txtTelefonoContacto.Text = String.Empty
            Me.txtCelularContacto.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesAcceso()
        Try
            Me.txtRucAcceso.Text = String.Empty
            Me.txtNombreComercial.Text = String.Empty
            Me.txtCorreoAcceso.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmEmpresa-cod_ctf") = Nothing
            Session("frmEmpresa-codigo_emp") = Nothing
            Session("frmEmpresa-idPro") = Nothing
            Session("frmEmpresa-codigo_inc") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboEstadoFiltro()
        Try
            Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "ESTADO_EMPRESA"
            End With
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbEstadoFiltro, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbEstado, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipo()
        Try
            Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "TIPO_EMPRESA"
            End With
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbTipoEmpresa, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSector()
        Try
            Dim dt As New Data.DataTable

            dt = md_Sector.BuscaSector()

            Call md_Funciones.CargarCombo(Me.cmbSector, dt, "codigo_sec", "nombre_sec", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDepartamento()
        Try
            Dim dt As New Data.DataTable : me_Departamento = New e_Departamento

            With me_Departamento
                .operacion = "GEN"
                .codigo_pai = "156"
            End With
            dt = md_Departamento.ListarDepartamento(me_Departamento)

            Call md_Funciones.CargarCombo(Me.cmbDepartamento, dt, "codigo_Dep", "nombre_Dep", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboProvincia()
        Try
            Dim dt As New Data.DataTable : me_Provincia = New e_Provincia

            With me_Provincia
                .operacion = "GEN"
                If Not String.IsNullOrEmpty(Me.cmbDepartamento.SelectedValue) Then .codigo_dep = Me.cmbDepartamento.SelectedValue
            End With
            dt = md_Provincia.ListarProvincia(me_Provincia)

            Call md_Funciones.CargarCombo(Me.cmbProvincia, dt, "codigo_Pro", "nombre_Pro", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDistrito()
        Try
            Dim dt As New Data.DataTable : me_Distrito = New e_Distrito

            With me_Distrito
                .operacion = "GEN"                
                If Not String.IsNullOrEmpty(Me.cmbProvincia.SelectedValue) Then .codigo_pro = Me.cmbProvincia.SelectedValue
            End With
            dt = md_Distrito.ListarDistrito(me_Distrito)

            Call md_Funciones.CargarCombo(Me.cmbDistrito, dt, "codigo_Dis", "nombre_Dis", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTelefono()
        Try            
            Dim dt As Data.DataTable = md_Funciones.ObtenerDataTable("CODIGO_TELEFONICO")

            Call md_Funciones.CargarCombo(Me.cmbTelefono, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbTelefonoContacto, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDenominacionContacto()
        Try
            Dim dt As Data.DataTable = md_Funciones.ObtenerDataTable("DENOMINACION_PERSONA")

            Call md_Funciones.CargarCombo(Me.cmbDenominacionContacto, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ConsultarCaptcha()
        Try
            Dim urlSunat As String = String.Format("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=random", False)
            Dim enlaceSunat As HttpWebRequest = WebRequest.Create(urlSunat)
            enlaceSunat.CookieContainer = Me.miCookie
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            enlaceSunat.Credentials = CredentialCache.DefaultCredentials
            Dim respuesta_web As WebResponse = enlaceSunat.GetResponse()

            If CType(respuesta_web, HttpWebResponse).StatusCode = HttpStatusCode.OK Then
                Dim myStream As Stream = respuesta_web.GetResponseStream
                Dim myStreamReader As New StreamReader(myStream)
                Dim xDat As String = myStreamReader.ReadToEnd.ToString.Trim

                If InStr(xDat, "consultado no es v�lido") > 0 Then
                    Call mt_ShowMessage("El número RUC ingresado no existe en la base de datos de la SUNAT o existe error en la página proporcionada.", MessageType.error)
                    Exit Sub
                End If

                xDat = Replace(xDat, "          ", " ")
                xDat = Replace(xDat, "         ", " ")
                xDat = Replace(xDat, "        ", " ")
                xDat = Replace(xDat, "       ", " ")
                xDat = Replace(xDat, "      ", " ")
                xDat = Replace(xDat, "     ", " ")
                xDat = Replace(xDat, "    ", " ")
                xDat = Replace(xDat, "   ", " ")
                xDat = Replace(xDat, "  ", " ")
                xDat = Replace(Replace(xDat, Chr(10), " "), Chr(13), "")
                xDat = Replace(xDat, "&Ntilde;", "Ñ")
                xDat = Replace(xDat, "&ntilde;", "ñ")
                xDat = Replace(xDat, "&Oacute;", "Ó")
                xDat = Replace(xDat, "&oacute;", "ó")
                xDat = Replace(xDat, "&Uacute;", "Ú")
                xDat = Replace(xDat, "&uacute;", "ú")
                xDat = Replace(xDat, "&Aacute;", "Á")
                xDat = Replace(xDat, "&aacute;", "á")
                xDat = Replace(xDat, "&Eacute;", "É")
                xDat = Replace(xDat, "&eacute;", "é")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "�", "Ñ")

                fu_ObtenerDatosSunat(txtRuc.Text.Trim, xDat)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ObtenerDatosSunat(ByVal str_ruc As String, ByVal str_captcha As String) As Boolean
        Try
            Dim urlSunat As String = String.Format("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" & str_ruc & "&numRnd=" & str_captcha, False)
            Dim enlaceSunat As HttpWebRequest = WebRequest.Create(urlSunat)
            enlaceSunat.CookieContainer = Me.miCookie
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
            enlaceSunat.Credentials = CredentialCache.DefaultCredentials
            Dim respuesta_web As WebResponse = enlaceSunat.GetResponse

            If CType(respuesta_web, HttpWebResponse).StatusCode = HttpStatusCode.OK Then
                Dim myStream As Stream = respuesta_web.GetResponseStream
                Dim myStreamReader As New StreamReader(myStream)
                Dim xDat As String = myStreamReader.ReadToEnd.ToString.Trim

                If InStr(xDat, "N&uacute;mero de RUC:") = 0 Then
                    Call mt_ShowMessage("El número RUC ingresado no existe en la base de datos de la SUNAT.", MessageType.error)
                    Return False
                End If

                Dim Dict As Object
                Dict = CreateObject("Scripting.Dictionary")
                Dim xTabla() As String
                Dim i As Integer

                xDat = Replace(xDat, "          ", " ")
                xDat = Replace(xDat, "         ", " ")
                xDat = Replace(xDat, "        ", " ")
                xDat = Replace(xDat, "       ", " ")
                xDat = Replace(xDat, "      ", " ")
                xDat = Replace(xDat, "     ", " ")
                xDat = Replace(xDat, "    ", " ")
                xDat = Replace(xDat, "   ", " ")
                xDat = Replace(xDat, "  ", " ")
                xDat = Replace(Replace(xDat, Chr(10), " "), Chr(13), "")
                xDat = Replace(xDat, "&Ntilde;", "Ñ")
                xDat = Replace(xDat, "&ntilde;", "ñ")
                xDat = Replace(xDat, "&Oacute;", "Ó")
                xDat = Replace(xDat, "&oacute;", "ó")
                xDat = Replace(xDat, "&Uacute;", "Ú")
                xDat = Replace(xDat, "&uacute;", "ú")
                xDat = Replace(xDat, "&Aacute;", "Á")
                xDat = Replace(xDat, "&aacute;", "á")
                xDat = Replace(xDat, "&Eacute;", "É")
                xDat = Replace(xDat, "&eacute;", "é")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "&Iacute;", "Í")
                xDat = Replace(xDat, "�", "Ñ")
                xTabla = Split(Trim(xDat), "<tr>")

                If UBound(xTabla) = 0 Then
                    Call mt_ShowMessage("El código captcha ingresado es incorrecto.", MessageType.warning)
                    Return False
                End If

                Dim cont As Integer = 1
                For i = 1 To 17
                    Dim Pos1, pos2 As Integer
                    Dim Cadena() As String

                    If Not Mid$(Trim(xTabla(i)), 1, 3) = "-->" Then
                        Cadena = Split(xTabla(i), "</td>")
                        Pos1 = InStrRev(Trim(Cadena(0)), ">")
                        pos2 = InStrRev(Trim(Cadena(1)), ">", Len(Trim(Cadena(1))) - 1)

                        Dict.Add(Trim(Mid$(Trim(Cadena(0)), Pos1 + 1, Len(Trim(Cadena(0))) - Pos1)), Replace(Trim(Mid$(Trim(Cadena(1)), pos2 + 1, Len(Trim(Cadena(1))) - pos2)), "</table>", ""))
                        cont = cont + 1
                    End If
                Next

                Dim nombre1 As String = "", nombre2 As String = "", nombre3 As String = "", nombre4 As String = ""
                nombre1 = Trim(Split(Dict.Item("Número de RUC:"), "-")(1))
                Me.txtRazonSocial.Text = nombre1
                Me.txtNombreComercial.Text = nombre1

                If Split(Dict.Item("Número de RUC:"), "-").Length > 2 Then
                    nombre2 = Trim(Split(Dict.Item("Número de RUC:"), "-")(2))
                    Me.txtRazonSocial.Text = nombre1 + "-" + nombre2
                    Me.txtNombreComercial.Text = nombre1 + "-" + nombre2
                End If

                If Split(Dict.Item("Número de RUC:"), "-").Length > 3 Then
                    nombre3 = Trim(Split(Dict.Item("Número de RUC:"), "-")(3))
                    Me.txtRazonSocial.Text = nombre1 + "-" + nombre2 + "-" + nombre3
                    Me.txtNombreComercial.Text = nombre1 + "-" + nombre2 + "-" + nombre3
                End If

                If Split(Dict.Item("Número de RUC:"), "-").Length > 4 Then
                    nombre4 = Trim(Split(Dict.Item("Número de RUC:"), "-")(4))
                    Me.txtRazonSocial.Text = nombre1 + "-" + nombre2 + "-" + nombre3 + "-" + nombre4
                    Me.txtNombreComercial.Text = nombre1 + "-" + nombre2 + "-" + nombre3 + "-" + nombre4
                End If

                If Not (Dict.Item("Dirección del Domicilio Fiscal:").ToString = "-") Then 'Persona Natural, no muestra Departamento, Provincia, Distrito
                    Dim xDireccion = Split(Dict.Item("Dirección del Domicilio Fiscal:"), " - ")
                    Dim xDis As String = Trim(Split(Dict.Item("Dirección del Domicilio Fiscal:"), " - ")(UBound(xDireccion)))
                    Dim xProv As String = Trim(Split(Dict.Item("Dirección del Domicilio Fiscal:"), " - ")(UBound(xDireccion) - 1))

                    Dim n As Integer
                    Dim ls_cadena = Split(Dict.Item("Dirección del Domicilio Fiscal:"), " - ")
                    Dim ls_direccion As String = String.Empty

                    For n = 0 To UBound(ls_cadena, 1)
                        ls_direccion = ls_cadena(0)
                    Next

                    Dim Palabras = Split(ls_direccion, " ")
                    Dim UltimoElemento = UBound(Palabras)
                    Dim UltimaPalabra = Palabras(UltimoElemento)

                    Dim Direccion As String = String.Empty

                    For x As Integer = 1 To UltimoElemento - 1
                        Direccion = Direccion & " " & Palabras(x)
                    Next

                    Me.txtDireccion.Text = Direccion

                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarConsultarCaptcha() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtRuc.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de RUC.", MessageType.warning) : Me.txtRuc.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarEmpresa() As Boolean
        Try
            If Not fu_ValidarRegistrarEmpresa() Then Return False

            Dim dt As New Data.DataTable : me_Empresa = md_Empresa.GetEmpresa(Session("frmEmpresa-codigo_emp"))

            me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)
            Dim ls_logo As String = String.Empty            

            'Archivo adjunto File Shared            
            If Me.txtArchivo.HasFile Then
                dt = New Data.DataTable                

                With me_ArchivoCompartido
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivo.FileName
                    .id_tabla = g_VariablesGlobales.TablaArchivoEmpresaLogos
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                End With

                'Realizar la carga del archivo compartido
                dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartido, Me.txtArchivo.PostedFile)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo adjunto.", MessageType.error) : Return False

                'Obtener el id y ruta del archivo compartido
                With me_ArchivoCompartido
                    .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString                    
                    ls_logo = .nombre_archivo
                End With
            End If

            With me_Empresa
                .operacion = "I"
                .cod_user = cod_user
                .ruc_emp = Me.txtRuc.Text.Trim
                .nombreComercial_emp = Me.txtNombreComercial.Text.Trim
                .razonSocial_emp = Me.txtRazonSocial.Text.Trim
                .abreviatura_emp = Me.txtAbreviatura.Text.Trim
                .codigoTipo_cat = Me.cmbTipoEmpresa.SelectedValue
                .codigo_sec = Me.cmbSector.SelectedValue
                .codigo_dep = Me.cmbDepartamento.SelectedValue
                .codigo_pro = Me.cmbProvincia.SelectedValue
                .codigo_dis = Me.cmbDistrito.SelectedValue
                .direccion_emp = Me.txtDireccion.Text.Trim                
                .correo_emp = Me.txtCorreo.Text.Trim
                .direccionWeb_emp = Me.txtDireccionWeb.Text.Trim
                .prefijoTel_emp = Me.cmbTelefono.SelectedValue
                .telefono_emp = Me.txtTelefono.Text.Trim
                .celular_emp = Me.txtCelular.Text.Trim
                .codigoEstado_cat = Me.cmbEstado.SelectedValue
                .accesoCampus_emp = IIf(Me.chkAccesoCampus.Checked, "S", "N")
                If Not String.IsNullOrEmpty(ls_logo) Then .logo_emp = ls_logo                
                If me_ArchivoCompartido.id_archivos_compartidos > 0 Then .id_archivos_compartidos = me_ArchivoCompartido.id_archivos_compartidos
            End With

            dt = New Data.DataTable
            dt = md_Empresa.RegistrarEmpresa(me_Empresa)

            'Insertar el detalle del archivo compartido
            If dt.Rows.Count > 0 AndAlso Me.txtArchivo.HasFile Then
                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                With me_ArchivoCompartidoDetalle
                    .operacion = "I"
                    .tabla_acd = "ALUMNI_Empresa"
                    .codigoTabla_acd = dt.Rows(0).Item("codigo_emp")
                End With

                me_ArchivoCompartido.detalles.Add(me_ArchivoCompartidoDetalle)

                'Registrar en la tabla detalle del archivo compartido
                md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartido)
            End If

            Call mt_ShowMessage("¡La empresa se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarEmpresa() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtRuc.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de RUC.", MessageType.warning) : Me.txtRuc.Focus() : Return False
            'If Not md_Funciones.ValidarRucSunat(Me.txtRuc.Text.Trim) Then mt_ShowMessage("El RUC ingresado no se encuentra registrado en SUNAT.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNombreComercial.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre comercial.", MessageType.warning) : Me.txtNombreComercial.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.txtTelefono.Text.Trim) AndAlso String.IsNullOrEmpty(Me.cmbTelefono.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar el código de ciudad al que pertenece el número de teléfono.", MessageType.warning) : Return False
            If Not String.IsNullOrEmpty(Me.txtCorreo.Text.Trim) AndAlso Not md_Funciones.ValidarEmail(Me.txtCorreo.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo de empresa válido.", MessageType.warning) : Me.txtCorreo.Focus() : Return False

            If Me.txtArchivo.HasFile Then
                Dim ls_extensiones As String = ".png .jpeg .jpg"
                If Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtArchivo.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe ingresar un archivo con extensión .png o .jpg o .jpeg .", MessageType.warning) : Return False
            End If

            Dim dt As New Data.DataTable

            'Verificar que el número de RUC no lo tenga otra empresa
            me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "VAL"
                .codigo_emp = Session("frmEmpresa-codigo_emp")
                .ruc_emp = Me.txtRuc.Text.Trim
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            If dt.Rows.Count > 0 Then mt_ShowMessage("Existe una empresa registrada con este número de RUC.", MessageType.warning) : Me.txtRuc.Focus() : Return False

            'Verificar que el nombre comercial no lo tenga otra empresa
            me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "VAL"
                .codigo_emp = Session("frmEmpresa-codigo_emp")
                .nombreComercial_emp = Me.txtNombreComercial.Text.Trim
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            If dt.Rows.Count > 0 Then mt_ShowMessage("Existe una empresa registrada con este nombre comercial.", MessageType.warning) : Me.txtNombreComercial.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New Data.DataTable : me_Empresa = New e_Empresa

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_Empresa
                .operacion = "GEN"
                .nombreComercial_emp = Me.txtNombreFiltro.Text.Trim
                .codigoEstado_cat = Me.cmbEstadoFiltro.SelectedValue
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_emp As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "GEN"
                .codigo_emp = codigo_emp
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles()

            With dt.Rows(0)
                Me.txtRuc.Text = .Item("ruc_emp").ToString
                Me.cmbEstado.SelectedValue = .Item("codigoEstado_cat").ToString
                Me.txtNombreComercial.Text = .Item("nombreComercial_emp").ToString
                Me.txtRazonSocial.Text = .Item("razonSocial_emp").ToString
                Me.txtAbreviatura.Text = .Item("abreviatura_emp").ToString
                Me.cmbTipoEmpresa.SelectedValue = .Item("codigoTipo_cat").ToString
                Me.cmbSector.SelectedValue = .Item("codigo_sec").ToString
                Me.cmbDepartamento.SelectedValue = .Item("codigo_dep").ToString
                Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbProvincia.SelectedValue = .Item("codigo_pro").ToString
                Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbDistrito.SelectedValue = .Item("codigo_dis").ToString
                Me.txtDireccion.Text = .Item("direccion_emp").ToString
                Me.txtDireccionWeb.Text = .Item("direccionWeb_emp").ToString
                Me.txtCorreo.Text = .Item("correo_emp").ToString
                Me.cmbTelefono.SelectedValue = .Item("prefijoTel_emp").ToString
                Me.txtTelefono.Text = .Item("telefono_emp").ToString
                Me.txtCelular.Text = .Item("celular_emp").ToString
                If .Item("accesoCampus_emp").ToString.Trim = "S" Then chkAccesoCampus.Checked = True Else chkAccesoCampus.Checked = False

                'Obtener el logo del Shared Files

                If String.IsNullOrEmpty(.Item("IdArchivosCompartidos").ToString.Trim) Then Return True
                Dim ln_ArchivoCompartido As Integer = Integer.Parse(.Item("IdArchivosCompartidos").ToString.Trim)
                If ln_ArchivoCompartido = 0 Then Return True

                'Obtener los datos del archivo compartido
                me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(ln_ArchivoCompartido)
                Dim ls_extensiones As String = ".png .jpg .jpeg"

                'Comprobar que el archivo compartido tenga la extencion de imagen
                If Not ls_extensiones.Contains(me_ArchivoCompartido.extencion.ToLower) Then Return True

                me_ArchivoCompartido.usuario_act = Session("perlogin")
                me_ArchivoCompartido.ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")

                Dim archivo As Byte() = md_ArchivoCompartido.ObtenerArchivoCompartido(me_ArchivoCompartido)

                Dim ms As New IO.MemoryStream(CType(archivo, Byte()))

                me_ArchivoCompartido.content_type = md_Funciones.ObtenerContentType(me_ArchivoCompartido.extencion)

                Me.imgLogo.ImageUrl = "data:" + me_ArchivoCompartido.content_type + ";base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length)

                Me.txtLogo.Text = .Item("logo_emp").ToString                
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarDatosContacto(ByVal codigo_emp As Integer)
        Try
            Dim dt As New Data.DataTable : me_InformacionContacto = New e_InformacionContacto

            If Me.grwContactos.Rows.Count > 0 Then Me.grwContactos.DataSource = Nothing : Me.grwContactos.DataBind()

            With me_InformacionContacto
                .operacion = "GEN"
                .codigo_emp = codigo_emp
            End With

            dt = md_InformacionContacto.ListarInformacionContacto(me_InformacionContacto)

            Me.grwContactos.DataSource = dt
            Me.grwContactos.DataBind()

            Call md_Funciones.AgregarHearders(grwContactos)

            Call mt_UpdatePanel("Contactos")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarContacto() As Boolean
        Try
            If Not fu_ValidarRegistrarContacto() Then Return False

            me_InformacionContacto = md_InformacionContacto.GetInformacionContacto(Session("frmEmpresa-codigo_inc"))

            With me_InformacionContacto
                .operacion = "I"
                .cod_user = cod_user
                .codigo_inc = Session("frmEmpresa-codigo_inc")
                .codigo_emp = Session("frmEmpresa-codigo_emp")
                .idPro = Session("frmEmpresa-idPro")
                .denominacion_inc = Me.cmbDenominacionContacto.SelectedValue
                .apellidos_inc = Me.txtApellidosContacto.Text.Trim
                .nombres_inc = Me.txtNombresContacto.Text.Trim
                .cargo_inc = Me.txtCargoContacto.Text.Trim
                .prefijoTel_inc = Me.cmbTelefonoContacto.SelectedValue
                .telefono_inc = Me.txtTelefonoContacto.Text.Trim
                .celular_inc = Me.txtCelularContacto.Text.Trim
                .correo01_inc = Me.txtCorreoContacto1.Text.Trim
                .correo02_inc = Me.txtCorreoContacto2.Text.Trim                                
            End With

            md_InformacionContacto.RegistrarInformacionContacto(me_InformacionContacto)

            Call mt_ShowMessage("¡El contacto se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarContacto() As Boolean
        Try
            If Session("frmEmpresa-codigo_emp") Is Nothing OrElse Session("frmEmpresa-codigo_emp") = 0 Then mt_ShowMessage("El código de la empresa no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbDenominacionContacto.SelectedValue) Then mt_ShowMessage("Debe seleccionar una denominación.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNombresContacto.Text.Trim) Then mt_ShowMessage("Debe ingresar el nombre del contacto.", MessageType.warning) : Me.txtNombresContacto.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtApellidosContacto.Text.Trim) Then mt_ShowMessage("Debe ingresar el apellido del contacto.", MessageType.warning) : Me.txtApellidosContacto.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.txtTelefonoContacto.Text.Trim) AndAlso String.IsNullOrEmpty(Me.cmbTelefonoContacto.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar el código de ciudad al que pertenece el número de teléfono.", MessageType.warning) : Return False
            If Not String.IsNullOrEmpty(Me.txtCorreoContacto1.Text.Trim) AndAlso Not md_Funciones.ValidarEmail(Me.txtCorreoContacto1.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo 1 de contacto válido.", MessageType.warning) : Me.txtCorreoContacto1.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.txtCorreoContacto2.Text.Trim) AndAlso Not md_Funciones.ValidarEmail(Me.txtCorreoContacto2.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo 2 de contacto válido.", MessageType.warning) : Me.txtCorreoContacto2.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioRegistroContacto(ByVal codigo_inc As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_InformacionContacto = New e_InformacionContacto

            With me_InformacionContacto
                .operacion = "GEN"
                .codigo_inc = codigo_inc
            End With

            dt = md_InformacionContacto.ListarInformacionContacto(me_InformacionContacto)
            If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControlesContacto()

            With dt.Rows(0)
                Me.cmbDenominacionContacto.SelectedValue = .Item("denominacion_inc").ToString
                Me.txtNombresContacto.Text = .Item("nombres_inc").ToString
                Me.txtApellidosContacto.Text = .Item("apellidos_inc").ToString
                Me.txtCargoContacto.Text = .Item("cargo_inc").ToString
                Me.txtCorreoContacto1.Text = .Item("correo01_inc").ToString
                Me.txtCorreoContacto2.Text = .Item("correo02_inc").ToString
                Me.cmbTelefonoContacto.SelectedValue = .Item("prefijoTel_inc").ToString
                Me.txtTelefonoContacto.Text = .Item("telefono_inc").ToString
                Me.txtCelularContacto.Text = .Item("celular_inc").ToString
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarContacto(ByVal codigo_inc As Integer) As Boolean
        Try
            me_InformacionContacto = md_InformacionContacto.GetInformacionContacto(codigo_inc)

            With me_InformacionContacto
                .operacion = "D"
                .cod_user = cod_user
                .codigo_inc = codigo_inc                
            End With

            md_InformacionContacto.RegistrarInformacionContacto(me_InformacionContacto)

            Call mt_ShowMessage("¡El contacto se elimino exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarSeleccionEmpresa(ByVal codigo_emp As String, ByVal ls_accion As String) As Boolean
        Try            
            me_Empresa = md_Empresa.GetEmpresa(codigo_emp)

            Select Case ls_accion
                Case "Editar"
                    If me_Empresa.codigoEstado_cat = g_VariablesGlobales.EstadoEmpresaRechazado Then mt_ShowMessage("La empresa se encuentra en estado RECHAZADO.", MessageType.warning) : Return False

                Case "GestionarContacto"
                    If me_Empresa.codigoEstado_cat = g_VariablesGlobales.EstadoEmpresaRechazado Then mt_ShowMessage("La empresa se encuentra en estado RECHAZADO.", MessageType.warning) : Return False

                Case "AccesoCampus"
                    If me_Empresa.accesoCampus_emp <> "S" Then mt_ShowMessage("La empresa debe tener acceso a campus.", MessageType.warning) : Return False
                    If me_Empresa.codigoEstado_cat <> g_VariablesGlobales.EstadoEmpresaAlta Then mt_ShowMessage("La empresa debe tener el estado ALTA.", MessageType.warning) : Return False

            End Select

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioAcceso(ByVal codigo_emp As String) As Boolean
        Try
            me_Empresa = md_Empresa.GetEmpresa(codigo_emp)

            If me_Empresa.codigo_emp = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControlesAcceso()

            Me.txtRucAcceso.Text = me_Empresa.ruc_emp
            Me.txtNombreComercialAcceso.Text = me_Empresa.nombreComercial_emp
            Me.txtCorreoAcceso.Text = me_Empresa.correo_emp

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarAccesoCampus() As Boolean
        Try
            If Not fu_ValidarEnviarAccesoCampus() Then Return False

            Dim dt As New Data.DataTable : me_Personal = New e_Personal

            me_Personal.codigo_per = cod_user
            me_Personal.codigo_tfu = Session("frmEmpresa-cod_ctf")

            dt = md_Personal.ObtenerFirmaAlumni(me_Personal)

            If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información para la firma del correo.", MessageType.warning) : Return False

            'Variables a utilizar
            Dim ls_nombrePer As String = String.Empty
            Dim ls_replyTo As String = g_VariablesGlobales.CorreoAlumni
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba
            Dim ls_mensaje As String = String.Empty
            Dim ls_password As String = md_Funciones.GenerarPassword(10)            
            Dim ls_para As String = String.Empty
            Dim ls_asunto As String = "Credenciales de Acceso al Campus Empresa - USAT"
            Dim ls_cuerpo As String = String.Empty
            Dim ls_FirmaMensaje As String = String.Empty

            'Obtenemos los datos de la firma del correo
            ls_nombrePer = dt.Rows(0).Item("nombreper").ToString
            me_Personal.nombres_per = ls_nombrePer

            'Obtener firma del mensaje
            ls_FirmaMensaje = md_Funciones.FirmaMensajeAlumni(me_Personal)

            'COORDINADOR DE ALUMNI
            If Session("frmEmpresa-cod_ctf") = g_VariablesGlobales.TipoFuncionCoordinadorAlumni Then ls_replyTo = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba

            'Armamos el mensaje de correo electronico
            ls_cuerpo &= "<p><strong><span style='text-decoration: underline;'>Estimados " & Me.txtNombreComercialAcceso.Text.Trim & "</span></strong></p>"
            ls_cuerpo &= "<p>Las credenciales para acceder a su campus de empresa son las siguientes:</p>"
            ls_cuerpo &= "<p>Usuario: <strong>" & Me.txtRucAcceso.Text.Trim & "</strong></p>"
            ls_cuerpo &= "<p>Contrase&ntilde;a: <strong>" & ls_password & "</strong></p>"
            ls_cuerpo &= "<p>Es recomendable cambiar la contrase&ntilde;a predeterminada.</p>"
            ls_cuerpo &= "<p><a href='" & g_VariablesGlobales.RutaEmpresaLogin & "'>Ingrese aquí</a></p>"

            ls_mensaje = g_VariablesGlobales.AbrirFormatoCorreoAlumni
            ls_mensaje &= ls_cuerpo
            ls_mensaje &= ls_FirmaMensaje
            ls_mensaje &= g_VariablesGlobales.CerrarFormatoCorreoAlumni

            'Obtenemos el correo de destino
            ls_para = Me.txtCorreoAcceso.Text.Trim
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_para = g_VariablesGlobales.CorreoPrueba

            'Enviar correo masivo
            me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
            dt = New Data.DataTable

            With me_EnvioCorreosMasivo
                .operacion = "I"
                .cod_user = cod_user
                .tipoCodigoEnvio_ecm = "codigo_emp"
                .codigoEnvio_ecm = Session("frmEmpresa-codigo_emp")
                .codigo_apl = g_VariablesGlobales.CodigoAplicacionAlumni
                .correo_destino = ls_para
                .correo_respuesta = ls_replyTo
                .asunto = ls_asunto                
                .cuerpo = ls_mensaje                                
            End With

            dt = md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo)

            ''Insertar en bitacora
            'me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
            'With me_EnvioCorreosMasivo
            '    .fecha_envio = Date.Now
            '    .codigoEnvio_ecm = 0
            '    .correo_destino = ls_para
            '    .asunto = ls_asunto
            '    .cuerpo = ls_cuerpo
            '    .archivo_adjunto = String.Empty
            'End With

            'md_EnvioCorreosMasivo.RegistrarBitacoraEnvio(me_EnvioCorreosMasivo)

            'Si todo se ha realizado correctamente, actualizamos la contraseña de la empresa
            me_Empresa = md_Empresa.GetEmpresa(0)

            With me_Empresa
                .operacion = "A"
                .cod_user = cod_user
                .codigo_emp = Session("frmEmpresa-codigo_emp")
                .password_emp = md_Funciones.EncrytedString64(ls_password)
                .accesoCampus_emp = "S"
            End With

            md_Empresa.RegistrarEmpresa(me_Empresa)

            Call mt_ShowMessage("¡Los datos de acceso a campus empresa se registraron exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarEnviarAccesoCampus() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtRucAcceso.Text.Trim) Then mt_ShowMessage("La empresa debe contar con un número de RUC.", MessageType.warning) : Me.txtRucAcceso.Focus() : Return False            
            If String.IsNullOrEmpty(Me.txtCorreoAcceso.Text.Trim) OrElse Not md_Funciones.ValidarEmail(Me.txtCorreoAcceso.Text.Trim) Then mt_ShowMessage("La empresa debe contar con una cuenta de correo válida. ", MessageType.warning) : Me.txtCorreoAcceso.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
