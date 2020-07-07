﻿Partial Class Alumni_frmEgresado
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_Categoria As New d_Categoria
    Dim md_TipoEstudio As New d_TipoEstudio
    Dim md_Facultad As New d_Facultad
    Dim md_CarreraProfesional As New d_CarreraProfesional
    Dim md_Anio As New d_Anio
    Dim md_EgresadoAlumni As New d_EgresadoAlumni
    Dim md_Empresa As New d_Empresa
    Dim md_EnvioCorreosMasivo As New d_EnvioCorreosMasivo
    Dim md_Personal As New d_Personal
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_ArchivoCompartidoDetalle As New d_ArchivoCompartidoDetalle

    'ENTIDADES
    Dim me_Categoria As e_Categoria
    Dim me_TipoEstudio As e_TipoEstudio
    Dim me_Facultad As e_Facultad
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_Anio As e_Anio
    Dim me_EgresadoAlumni As e_EgresadoAlumni
    Dim me_Empresa As e_Empresa
    Dim me_EnvioCorreosMasivo As e_EnvioCorreosMasivo
    Dim me_Personal As e_Personal
    Dim me_ArchivoCompartido As e_ArchivoCompartido
    Dim me_ArchivoCompartidoDetalle As e_ArchivoCompartidoDetalle

    'VARIABLES    
    Dim cod_user As Integer = 0    

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
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            
            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Session("frmEgresado-cod_ctf") = Request.QueryString("ctf")
                Call mt_CargarComboNivelFiltro()
                Me.cmbNivelFiltro.SelectedValue = g_VariablesGlobales.NivelEstudioPreGrado
                Call mt_CargarComboModalidadFiltro()
                Call mt_CargarComboFacultadFiltro()
                Call mt_CargarComboCarreraFiltro()
                Call mt_CargarComboAniosFiltro()
                Call mt_CargarComboSexoFiltro()
                Call mt_CargarComboTelefonico()

                Call mt_LimpiarControles()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbNivelFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNivelFiltro.SelectedIndexChanged
        Try
            Call mt_CargarComboModalidadFiltro()
            Call mt_CargarComboCarreraFiltro()

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbFacultadFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFacultadFiltro.SelectedIndexChanged
        Try
            Call mt_CargarComboCarreraFiltro()

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbModalidadFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModalidadFiltro.SelectedIndexChanged
        Try
            Call mt_CargarComboCarreraFiltro()

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub chkLaboraActual_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            mt_EstadoLaboraActual()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try            
            mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirMensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirMensaje.Click
        Try            
            Call mt_UpdatePanel("SalirMensaje")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmEgresado-codigo_ega") = Me.grwLista.DataKeys(index).Values("codigo_ega")
            Session("frmEgresado-codigo_pso") = Me.grwLista.DataKeys(index).Values("codigo_Pso")

            Select Case e.CommandName
                Case "Editar"
                    Call mt_CargarFormularioRegistro(Session("frmEgresado-codigo_ega"))

                    If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Me.txtNombreComercialFiltro.Text = String.Empty

            Call mt_UpdatePanel("FiltrosEmpresa")

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('listaEmpresa-tab');", True)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevaEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevaEmpresa.Click
        Try
            Call mt_LimpiarControlesRegistro()

            Call mt_UpdatePanel("RegistroEmpresa")

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('registroEmpresa-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirEmpresa.Click
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('listaEmpresa-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListarEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarEmpresa.Click
        Try
            Call mt_ListarEmpresas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarEmpresa.Click
        Try
            If Not mt_RegistrarEmpresa() Then Exit Sub

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('listaEmpresa-tab');", True)

            Call mt_ListarEmpresas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaEmpresa_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaEmpresa.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Seleccionar"
                    Me.txtCodigoEmp.Value = Me.grwListaEmpresa.DataKeys(index).Values("codigo_emp")
                    Me.txtCentroLaboral.Text = Me.grwListaEmpresa.DataKeys(index).Values("nombreComercial_emp")

                    call mt_UpdatePanel("InformacionLaboral")

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal();", True)
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If Not mt_RegistrarEgresado() Then Exit Sub

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            If Not mt_CargarFormularioEnviar() Then Exit Sub

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('envio-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEnviarMensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not mt_EnviarMensaje() Then
                Call mt_UpdatePanel("Envio")

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('envio-tab');", True)
            End If

            Call btnListar_Click(Nothing, Nothing)

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos();", True)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
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
                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "FiltrosEmpresa"
                    Me.udpFiltrosEmpresa.Update()

                Case "ListaEmpresa"
                    Me.udpListaEmpresa.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaEmpresaUpdate", "udpListaEmpresaUpdate();", True)

                Case "RegistroEmpresa"
                    Me.udpRegistroEmpresa.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroEmpresaUpdate", "udpRegistroEmpresaUpdate();", True)

                Case "InformacionLaboral"
                    Me.udpInformacionLaboral.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpInformacionLaboralUpdate", "udpInformacionLaboralUpdate();", True)

                Case "Envio"
                    Me.udpEnvio.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpEnvioUpdate", "udpEnvioUpdate();", True)

                Case "SalirMensaje"
                    Me.udpLista.Update()
                    Call md_Funciones.AgregarHearders(grwLista)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            'Información de Egresado
            Me.txtNombre.Text = String.Empty
            Me.txtNivel.Text = String.Empty
            Me.txtModalidad.Text = String.Empty
            Me.txtFacultad.Text = String.Empty
            Me.txtCarrera.Text = String.Empty
            Me.txtAnioEgreso.Text = String.Empty
            Me.txtAnioBachiller.Text = String.Empty
            Me.txtAnioTitulo.Text = String.Empty

            Me.txtNombre.ReadOnly = True
            Me.txtNivel.ReadOnly = True
            Me.txtModalidad.ReadOnly = True
            Me.txtFacultad.ReadOnly = True
            Me.txtCarrera.ReadOnly = True
            Me.txtAnioEgreso.ReadOnly = True
            Me.txtAnioBachiller.ReadOnly = True
            Me.txtAnioTitulo.ReadOnly = True

            'Información de Contacto del Egresado
            Me.txtCorreoProfesional.Text = String.Empty
            Me.txtCorreoPersonal.Text = String.Empty
            Me.cmbTelefonoEgresado.SelectedValue = String.Empty
            Me.txtTelefonoEgresado.Text = String.Empty
            Me.txtCelular01Egresado.Text = String.Empty
            Me.txtCelular02Egresado.Text = String.Empty

            'Información Laboral
            Me.chkLaboraActual.Checked = False
            Me.txtCodigoEmp.Value = String.Empty
            Me.txtCentroLaboral.Text = String.Empty
            Me.txtCargo.Text = String.Empty
            Me.txtCorreoEmpresa.Text = String.Empty
            Me.txtCelularEmpresa.Text = String.Empty
            Me.cmbTelefonoEmpresa.SelectedValue = String.Empty
            Me.txtTelefonoEmpresa.Text = String.Empty

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesEnvio()
        Try
            'Envio de Mensajes
            Me.lblDestinatarios.Text = String.Empty
            Me.txtAsunto.Text = String.Empty            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesRegistro()
        Try
            Me.txtNombreComercial.Text = String.Empty
            Me.txtCorreoRegEmpresa.Text = String.Empty
            Me.txtCelRegEmpresa.Text = String.Empty
            Me.cmbTelRegEmpresa.SelectedValue = String.Empty
            Me.txtTelRegEmpresa.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmEgresado-cod_ctf") = Nothing
            Session("frmEgresado-codigo_ega") = Nothing
            Session("frmEgresado-codigo_pso") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboNivelFiltro()
        Try
            me_Categoria = New e_Categoria
            Dim dt As New Data.DataTable

            me_Categoria.operacion = "GEN"
            me_Categoria.grupo_cat = "NIVEL_ESTUDIO"
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbNivelFiltro, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboModalidadFiltro()
        Try
            me_TipoEstudio = New e_TipoEstudio
            Dim dt As New Data.DataTable

            me_TipoEstudio.operacion = "MOD"

            If Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPreGrado Then
                me_TipoEstudio.codigo_test = g_VariablesGlobales.CodigoTestPreGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostGrado Then
                me_TipoEstudio.codigo_test = g_VariablesGlobales.CodigoTestPostGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostTitulo Then
                me_TipoEstudio.codigo_test = g_VariablesGlobales.CodigoTestPostTitulo
            Else
                me_TipoEstudio.codigo_test = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostTitulo
            End If

            dt = md_TipoEstudio.ListarTipoEstudio(me_TipoEstudio)

            Call md_Funciones.CargarCombo(Me.cmbModalidadFiltro, dt, "codigo_test", "descripcion_test", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboFacultadFiltro()
        Try
            me_Facultad = New e_Facultad
            Dim dt As New Data.DataTable

            If Request.QueryString("ctf") = 145 Then me_Facultad.operacion = "COO" Else me_Facultad.operacion = "GEN"
            me_Facultad.codigo_per = Request.QueryString("ID")
            dt = md_Facultad.ListarFacultad(me_Facultad)

            Call md_Funciones.CargarCombo(Me.cmbFacultadFiltro, dt, "codigo_Fac", "nombre_Fac", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCarreraFiltro()
        Try
            me_CarreraProfesional = New e_CarreraProfesional
            Dim dt As New Data.DataTable

            If Request.QueryString("ctf") = 145 Then me_CarreraProfesional.operacion = "PER" Else me_CarreraProfesional.operacion = "GEN"
            me_CarreraProfesional.codigo_Fac = cmbFacultadFiltro.SelectedValue
            me_CarreraProfesional.modalidad = cmbModalidadFiltro.SelectedValue
            me_CarreraProfesional.codigo_per = Request.QueryString("ID")

            If Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPreGrado Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPreGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostGrado Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPostGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostTitulo Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPostTitulo
            Else
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostTitulo
            End If

            dt = md_CarreraProfesional.ListarCarreraProfesional(me_CarreraProfesional)

            Call md_Funciones.CargarCombo(Me.cmbCarreraFiltro, dt, "codigo_Cpf", "nombre_Cpf", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboAniosFiltro()
        Try
            me_Anio = New e_Anio
            Dim dt As New Data.DataTable

            me_Anio.operacion = "GEN"
            me_Anio.anio_inicio = "2004"
            dt = md_Anio.ListarAnio(me_Anio)

            Call md_Funciones.CargarCombo(Me.cmbAnioEgresoFiltro, dt, "anio", "anio", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbAnioBachillerFiltro, dt, "anio", "anio", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbAnioTituloFiltro, dt, "anio", "anio", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSexoFiltro()
        Try
            Dim dt As Data.DataTable = md_Funciones.ObtenerDataTable("SEXO")

            Call md_Funciones.CargarCombo(Me.cmbSexoFiltro, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTelefonico()
        Try
            Dim dt As Data.DataTable = md_Funciones.ObtenerDataTable("CODIGO_TELEFONICO")

            Call md_Funciones.CargarCombo(Me.cmbTelefonoEgresado, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbTelefonoEmpresa, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbTelRegEmpresa, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_EstadoLaboraActual()
        Try
            'Bloquear Controles
            Me.txtCentroLaboral.ReadOnly = True
            Me.txtCargo.ReadOnly = Not chkLaboraActual.Checked
            Me.txtCorreoEmpresa.ReadOnly = Not chkLaboraActual.Checked
            Me.txtCelularEmpresa.ReadOnly = Not chkLaboraActual.Checked
            Me.cmbTelefonoEmpresa.Enabled = chkLaboraActual.Checked
            Me.txtTelefonoEmpresa.ReadOnly = Not chkLaboraActual.Checked
            Me.btnBuscar.Enabled = chkLaboraActual.Checked

            'Limpiar Controles
            Me.txtCentroLaboral.Text = String.Empty
            Me.txtCodigoEmp.Value = "0"
            Me.txtCargo.Text = String.Empty
            Me.txtCorreoEmpresa.Text = String.Empty
            Me.txtCelularEmpresa.Text = String.Empty
            Me.cmbTelefonoEmpresa.SelectedValue = String.Empty
            Me.txtTelefonoEmpresa.Text = String.Empty

            Me.udpInformacionLaboral.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpInformacionLaboralUpdate", "udpInformacionLaboralUpdate();", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            me_EgresadoAlumni = New e_EgresadoAlumni
            Dim dt As New Data.DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_EgresadoAlumni
                .operacion = "LIS"

                If Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPreGrado Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado
                ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostGrado Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPostGrado
                ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostTitulo Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPostTitulo
                Else
                    .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostTitulo
                End If

                .modalidad_ega = Me.cmbModalidadFiltro.Text.Trim
                .codigo_fac = Me.cmbFacultadFiltro.Text.Trim
                .codigo_cpf = Me.cmbCarreraFiltro.Text.Trim
                .sexo_ega = Me.cmbSexoFiltro.Text.Trim
                .anio_egreso = Me.cmbAnioEgresoFiltro.Text.Trim
                .anio_bachiller = Me.cmbAnioBachillerFiltro.Text.Trim
                .anio_titulo = Me.cmbAnioTituloFiltro.Text.Trim
                .nombre_ega = Me.txtNombreFiltro.Text.Trim
            End With

            dt = md_EgresadoAlumni.ListarEgresadoAlumni(me_EgresadoAlumni)
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarFormularioRegistro(ByVal codigo_ega As Integer)
        Try
            me_EgresadoAlumni = New e_EgresadoAlumni
            Dim dt As New Data.DataTable

            With me_EgresadoAlumni
                .operacion = "LIS"
                .codigo_ega = codigo_ega
            End With

            dt = md_EgresadoAlumni.ListarEgresadoAlumni(me_EgresadoAlumni)

            If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Exit Sub

            Call mt_LimpiarControles()

            With dt.Rows(0)
                'Información de Egresado
                Me.txtNombre.Text = .Item("apellidos").ToString & " " & .Item("nombres").ToString
                Me.txtNivel.Text = .Item("nivel").ToString
                Me.txtModalidad.Text = .Item("modalidad").ToString
                Me.txtFacultad.Text = .Item("facultad").ToString
                Me.txtCarrera.Text = .Item("escuela_profesional").ToString
                Me.txtAnioEgreso.Text = .Item("anio_egreso").ToString
                Me.txtAnioBachiller.Text = .Item("anio_bachiller").ToString
                Me.txtAnioTitulo.Text = .Item("anio_titulo").ToString

                'Información de Contacto del Egresado
                Me.txtCorreoProfesional.Text = .Item("correo_profesional").ToString
                Me.txtCorreoPersonal.Text = .Item("correo_personal").ToString
                Me.cmbTelefonoEgresado.SelectedValue = .Item("pre_tel_fijo").ToString
                Me.txtTelefonoEgresado.Text = .Item("tel_fijo").ToString
                Me.txtCelular01Egresado.Text = .Item("celular1").ToString
                Me.txtCelular02Egresado.Text = .Item("celular2").ToString

                'Información Laboral
                If .Item("actual_labora").ToString.Trim = "S" Then Me.chkLaboraActual.Checked = True Else Me.chkLaboraActual.Checked = False
                Call mt_EstadoLaboraActual()
                Me.txtCodigoEmp.Value = .Item("codigo_emp").ToString
                Me.txtCentroLaboral.Text = .Item("nombre_emp").ToString
                Me.txtCargo.Text = .Item("cargo_actual").ToString
                Me.txtCorreoEmpresa.Text = .Item("correo_emp").ToString
                Me.txtCelularEmpresa.Text = .Item("celular_emp").ToString
                Me.cmbTelefonoEmpresa.SelectedValue = .Item("pre_tel_emp").ToString
                Me.txtTelefonoEmpresa.Text = .Item("tel_emp").ToString
            End With
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ListarEmpresas()
        Try
            me_Empresa = New e_Empresa
            Dim dt As New Data.DataTable

            If Me.grwListaEmpresa.Rows.Count > 0 Then Me.grwListaEmpresa.DataSource = Nothing : Me.grwListaEmpresa.DataBind()

            With me_Empresa
                .operacion = "GEN"
                .nombreComercial_emp = Me.txtNombreComercialFiltro.Text.Trim
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            Me.grwListaEmpresa.DataSource = dt

            Me.grwListaEmpresa.DataBind()

            Call md_Funciones.AgregarHearders(grwListaEmpresa)

            call mt_UpdatePanel("ListaEmpresa")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarEmpresa() As Boolean
        Try
            If Not fu_ValidarRegistrarEmpresa() Then Return False

            me_Empresa = md_Empresa.GetEmpresa(0)

            With me_Empresa
                .operacion = "I"
                .cod_user = cod_user
                .nombreComercial_emp = Me.txtNombreComercial.Text.Trim
                .codigoEstado_cat = g_VariablesGlobales.EstadoEmpresaGenerado
                .externo_emp = "N"
                .codigoExterno_emp = "0"
                .correo_emp = Me.txtCorreoRegEmpresa.Text.Trim
                .prefijoTel_emp = Me.cmbTelRegEmpresa.SelectedValue.Trim
                .telefono_emp = Me.txtTelRegEmpresa.Text.Trim
                .accesoCampus_emp = "N"
            End With

            md_Empresa.RegistrarEmpresa(me_Empresa)

            Call mt_ShowMessage("¡La empresa se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarEmpresa() As Boolean
        Try
            Dim dt As Data.DataTable

            If String.IsNullOrEmpty(Me.txtNombreComercial.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre comercial.", MessageType.warning) : Me.txtNombreComercial.Focus() : Return False

            'Consultar si existe una empresa registrada con este nombre comercial
            dt = New Data.DataTable : me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "VAL"
                .nombreComercial_emp = Me.txtNombreComercial.Text.Trim
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)

            If dt.Rows.Count > 0 Then mt_ShowMessage("Existe una empresa registrada con este nombre comercial.", MessageType.warning) : Me.txtNombreComercial.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarEgresado() As Boolean
        Try
            If Not fu_ValidarRegistrarEgresado() Then Return False

            me_EgresadoAlumni = New e_EgresadoAlumni

            With me_EgresadoAlumni
                .operacion = "U"
                .cod_user = cod_user
                .codigo_ega = Session("frmEgresado-codigo_ega")
                .codigo_pso = Session("frmEgresado-codigo_pso")
                .emailPrincipal_pso = Me.txtCorreoPersonal.Text.Trim
                .emailAlternativo_pso = Me.txtCorreoProfesional.Text.Trim
                .prefijoTelefono_pso = Me.cmbTelefonoEgresado.SelectedValue
                .telefonoFijo_pso = Me.txtTelefonoEgresado.Text.Trim
                .telefonoCelular_pso = Me.txtCelular01Egresado.Text.Trim
                .telefonoCelular2_pso = Me.txtCelular02Egresado.Text.Trim
                If Me.chkLaboraActual.Checked Then .actualmenteLabora_ega = "S" Else .actualmenteLabora_ega = "N"
                .codigo_emp = Me.txtCodigoEmp.Value
                .empresaLabora_ega = Me.txtCentroLaboral.Text.Trim
                .cargoActual_ega = Me.txtCargo.Text.Trim
                .prefijoTelEmp_ega = Me.cmbTelefonoEmpresa.SelectedValue
                .telefonoEmp_ega = Me.txtTelefonoEmpresa.Text.Trim
                .correoEmp_ega = Me.txtCorreoEmpresa.Text.Trim
                .celularEmp_ega = Me.txtCelularEmpresa.Text.Trim
            End With

            md_EgresadoAlumni.RegistrarEgresadoAlumni(me_EgresadoAlumni)

            Call mt_ShowMessage("¡La información del egresado se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarEgresado() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtCorreoPersonal.Text.Trim) OrElse Not md_Funciones.ValidarEmail(Me.txtCorreoPersonal.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo personal válido.", MessageType.warning) : Me.txtCorreoPersonal.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.txtCorreoProfesional.Text.Trim) AndAlso Not md_Funciones.ValidarEmail(Me.txtCorreoProfesional.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo profesional válido.", MessageType.warning) : Me.txtCorreoProfesional.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.txtTelefonoEgresado.Text.Trim) AndAlso String.IsNullOrEmpty(Me.cmbTelefonoEgresado.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar el código de ciudad al que pertenece el número de teléfono.", MessageType.warning) : Return False            

            If Me.chkLaboraActual.Checked Then
                If String.IsNullOrEmpty(Me.txtCodigoEmp.Value) OrElse Me.txtCodigoEmp.Value = "0" OrElse String.IsNullOrEmpty(Me.txtCentroLaboral.Text.Trim) Then mt_ShowMessage("Debe seleccionar una empresa de la lista.", MessageType.warning) : Me.btnBuscar.Focus() : Return False
                If String.IsNullOrEmpty(Me.txtCargo.Text.Trim) Then mt_ShowMessage("Debe ingresar un cargo.", MessageType.warning) : Me.txtCargo.Focus() : Return False
                If Not String.IsNullOrEmpty(Me.txtTelefonoEmpresa.Text.Trim) AndAlso String.IsNullOrEmpty(Me.cmbTelefonoEmpresa.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar el código de ciudad al que pertenece el número de teléfono.", MessageType.warning) : Return False
                If Not String.IsNullOrEmpty(Me.txtCorreoEmpresa.Text.Trim) AndAlso Not md_Funciones.ValidarEmail(Me.txtCorreoEmpresa.Text.Trim) Then mt_ShowMessage("Debe ingresar un correo de empresa válido.", MessageType.warning) : Me.txtCorreoEmpresa.Focus() : Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioEnviar() As Boolean
        Try
            If Not fu_ValidarSeleccionEgresado() Then Return False

            Call mt_LimpiarControlesEnvio()

            Dim ls_Destinatarios As String = String.Empty
            Dim ln_Destinatarios As Integer = 0

            If Me.grwLista.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwLista.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        ls_Destinatarios &= Fila.Cells(3).Text & ", " & Fila.Cells(4).Text & " | "
                        ln_Destinatarios += 1
                    End If
                Next
            End If

            Me.lblDestinatarios.Text = ln_Destinatarios.ToString & " Destinatario(s)."

            Me.lblDestinatarios.Attributes.Add("title", Server.HtmlDecode(ls_Destinatarios))

            Call mt_UpdatePanel("Envio")

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarSeleccionEgresado() As Boolean
        Try            
            If Me.grwLista.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwLista.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        Return True
                        Exit For
                    End If
                Next
            End If

            Call mt_UpdatePanel("SalirMensaje")

            Call mt_ShowMessage("Debe seleccionar al menos un egresado.", MessageType.warning)

            Return False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarMensaje() As Boolean
        Try
            If Not fu_ValidarEnviarMensaje() Then Return False

            me_Personal = New e_Personal
            Dim dt As New Data.DataTable

            me_Personal.codigo_per = cod_user
            me_Personal.codigo_tfu = Session("frmEgresado-cod_ctf")

            dt = md_Personal.ObtenerFirmaAlumni(me_Personal)

            If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información para la firma del correo.", MessageType.warning) : Return False

            'Obtenemos los datos de la firma del correo
            Dim ln_seleccionados As Integer = 0
            Dim ln_correosEnviados As Integer = 0
            Dim ln_correosNoValidos As Integer = 0
            Dim ln_sinCorreo As Integer = 0
            Dim ls_cuentas As String = String.Empty
            Dim ls_para As String = String.Empty
            Dim ls_mensaje As String = String.Empty
            Dim ls_nombrePer As String = String.Empty
            Dim ls_replyTo As String = g_VariablesGlobales.CorreoAlumni
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba
            Dim ls_ruta As String = String.Empty
            Dim ls_nombreArchivo As String = String.Empty
            Dim ls_FirmaMensaje As String = String.Empty

            ls_nombrePer = dt.Rows(0).Item("nombreper").ToString
            me_Personal.nombres_per = ls_nombrePer

            'Obtener firma del mensaje
            ls_FirmaMensaje = md_Funciones.FirmaMensajeAlumni(me_Personal)

            'COORDINADOR DE ALUMNI
            If Session("frmEgresado-cod_ctf") = g_VariablesGlobales.TipoFuncionCoordinadorAlumni Then ls_replyTo = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba

            'Archivo adjunto File Shared
            If Me.txtArchivo.HasFile Then
                dt = New Data.DataTable
                me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)

                With me_ArchivoCompartido
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivo.FileName
                    .id_tabla = g_VariablesGlobales.TablaArchivoCorreosMasivo
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                End With

                'Realizar la cargar del archivo compartido
                dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartido, Me.txtArchivo.PostedFile)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo adjunto.", MessageType.error) : Return False

                'Obtener el id y ruta del archivo compartido
                With me_ArchivoCompartido
                    .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString
                    .ruta_archivo = dt.Rows(0).Item("RutaArchivo").ToString

                    ls_ruta = .ruta_archivo
                    ls_nombreArchivo = .nombre_archivo
                End With                
            End If

            If Me.grwLista.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwLista.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        ln_seleccionados += 1

                        If (Not String.IsNullOrEmpty(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                            md_Funciones.ValidarEmail(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim)) OrElse _
                            (Not String.IsNullOrEmpty(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString.Trim) AndAlso _
                             md_Funciones.ValidarEmail(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString)) Then

                            ls_mensaje = g_VariablesGlobales.AbrirFormatoCorreoAlumni
                            ls_mensaje &= IIf(Fila.Cells(7).Text = "F", "Estimada ", "Estimado ")
                            ls_mensaje &= Fila.Cells(4).Text & ":<br /><br />"
                            ls_mensaje &= HttpUtility.UrlDecode(Me.txtMensaje.Value)
                            ls_mensaje &= ls_FirmaMensaje
                            ls_mensaje &= g_VariablesGlobales.CerrarFormatoCorreoAlumni

                            If Not String.IsNullOrEmpty(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                                md_Funciones.ValidarEmail(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString) Then
                                ls_para = Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString
                            Else
                                ls_para = Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString
                            End If

                            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_para = g_VariablesGlobales.CorreoPrueba

                            'Enviar correo masivo
                            me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
                            dt = New Data.DataTable

                            With me_EnvioCorreosMasivo
                                .operacion = "I"
                                .cod_user = cod_user
                                .tipoCodigoEnvio_ecm = "codigo_pso"
                                .codigoEnvio_ecm = Me.grwLista.DataKeys(Fila.RowIndex).Item("codigo_Pso").ToString
                                .codigo_apl = g_VariablesGlobales.CodigoAplicacionAlumni
                                .correo_destino = ls_para
                                .correo_respuesta = ls_replyTo
                                .asunto = Me.txtAsunto.Text.Trim
                                .cuerpo = ls_mensaje
                                .archivo_adjunto = ls_ruta
                            End With

                            dt = md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo)

                            ln_correosEnviados += 1

                            'Insertar el detalle del archivo compartido
                            If dt.Rows.Count > 0 AndAlso Me.txtArchivo.HasFile Then
                                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                                With me_ArchivoCompartidoDetalle
                                    .operacion = "I"
                                    .tabla_acd = "EnvioCorreosMasivo"
                                    .codigoTabla_acd = dt.Rows(0).Item("codigo_ecm")
                                End With

                                me_ArchivoCompartido.detalles.Add(me_ArchivoCompartidoDetalle)
                            End If

                            ''Insertar en bitacora
                            'me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
                            'With me_EnvioCorreosMasivo
                            '    .fecha_envio = Date.Now
                            '    .codigoEnvio_ecm = Me.grwLista.DataKeys(Fila.RowIndex).Item("codigo_Pso").ToString
                            '    .correo_destino = ls_para
                            '    .asunto = Me.txtAsunto.Text.Trim
                            '    .cuerpo = HttpUtility.UrlDecode(Me.txtMensaje.Value)
                            '    .archivo_adjunto = ls_nombreArchivo
                            'End With

                            'md_EnvioCorreosMasivo.RegistrarBitacoraEnvio(me_EnvioCorreosMasivo)

                        Else
                            If String.IsNullOrEmpty(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                                String.IsNullOrEmpty(Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString) Then
                                ln_sinCorreo += 1
                            Else
                                ln_correosNoValidos += 1
                            End If

                            ls_cuentas &= "<tr><td>" & Fila.Cells(4).Text & " " & Fila.Cells(3).Text & "</td><td>" & _
                                            Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_personal").ToString & "</td><td>" & _
                                            Me.grwLista.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString & "</td></tr>"

                        End If

                    End If
                Next

                'Guardar los detalles del archivo compartido shared files
                If Me.txtArchivo.HasFile Then md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartido)
            End If

            Call mt_EnviarMensajeNotificacion(ls_cuentas, HttpUtility.UrlDecode(Me.txtMensaje.Value), Me.txtAsunto.Text.Trim, ls_nombreArchivo, ln_seleccionados, ln_correosEnviados, ln_correosNoValidos, ln_sinCorreo, ls_nombrePer)

            Dim ls_mensajeAlerta As String = String.Empty
            ls_mensajeAlerta = "Correos Enviados: " & ln_correosEnviados.ToString & "  de  " & ln_seleccionados.ToString & " destinatario(s) seleccionado(s).Usuarios sin correo registrado: " & ln_sinCorreo.ToString & ".  Usuarios con correos inválidos: " & ln_correosNoValidos.ToString

            Call mt_ShowMessage(ls_mensajeAlerta, MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarEnviarMensaje() As Boolean
        Try
            Dim ls_Mensaje As String = HttpUtility.UrlDecode(Me.txtMensaje.Value)

            If String.IsNullOrEmpty(Me.txtAsunto.Text.Trim) Then mt_ShowMessage("Debe ingresar el asunto del mensaje.", MessageType.warning) : Me.txtAsunto.Focus() : Return False
            If String.IsNullOrEmpty(ls_Mensaje.Trim) OrElse ls_Mensaje.Trim = "<p>&nbsp;</p>" Then mt_ShowMessage("Debe ingresar un mensaje.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarMensajeNotificacion(ByVal ls_cuentas As String, ByVal ls_mensaje As String, ByVal ls_asunto As String, ByVal ls_adjunto As String, ByVal ln_seleccionados As Integer, _
                                                  ByVal ln_correosEnviados As Integer, ByVal ln_correosNoValidos As Integer, ByVal ln_sinCorreo As Integer, ByVal ls_nombrePer As String) As Boolean
        Try
            me_CarreraProfesional = New e_CarreraProfesional
            me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
            Dim dt As Data.DataTable

            Dim ls_mensajeEnviar As String = String.Empty

            'Listar coordinadores por escuela
            dt = New Data.DataTable

            With me_CarreraProfesional
                .operacion = "PER"
                .codigo_per = cod_user
            End With

            dt = md_CarreraProfesional.ListarCarreraProfesional(me_CarreraProfesional)

            Dim ls_escuela As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If String.IsNullOrEmpty(ls_escuela) Then
                    ls_escuela = dt.Rows(i).Item("nombre_Cpf").ToString
                Else
                    ls_escuela = ls_escuela + ", " + dt.Rows(i).Item("nombre_Cpf").ToString
                End If
            Next

            ls_mensajeEnviar &= g_VariablesGlobales.AbrirFormatoCorreoAlumni
            ls_mensajeEnviar &= "<b>Notificación de Envío de Correo</b><hr /><br />"
            ls_mensajeEnviar &= "<b>Fecha: </b>" & Now.Date & "<br />"
            ls_mensajeEnviar &= "<b>Asunto: </b>" & ls_asunto & "<br />"
            ls_mensajeEnviar &= "<b>Mensaje: </b>" & ls_mensaje & "<br />"
            ls_mensajeEnviar &= "<b>Adjunto: </b>" & ls_adjunto & "<br /><br />"
            ls_mensajeEnviar &= "<b>Total Destinatarios: </b>" & ln_seleccionados.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Destinatarios sin correo registrado: </b>" & ln_sinCorreo.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mensajes Enviados: </b>" & ln_correosEnviados.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mensajes Fallidos: </b>" & ln_correosNoValidos.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mail enviado por: </b>" & ls_nombrePer & "<br />"
            ls_mensajeEnviar &= "<b>Escuela: </b>" & ls_escuela & "<br />"

            If Not String.IsNullOrEmpty(ls_cuentas) Then
                ls_mensajeEnviar &= "<br /><b>Detalle de correos fallidos: </b>"
                ls_mensajeEnviar &= "<br /><br /><table border=""1"" style=""border:1px solid black;""><tr><th>Nombres Apellidos</th><th>Correo Personal</th><th>Correo Profesional</th></tr>"
                ls_mensajeEnviar &= ls_cuentas
                ls_mensajeEnviar &= "</table>"
            End If

            ls_mensajeEnviar &= "<br /><hr /><br />"
            ls_mensajeEnviar &= "<b>CampusVirtual USAT</b>"
            ls_mensajeEnviar &= g_VariablesGlobales.CerrarFormatoCorreoAlumni

            Dim ls_para As String = g_VariablesGlobales.CorreoCoordinadorAlumni
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_para = g_VariablesGlobales.CorreoPrueba

            'Enviar correo masivo
            dt = New Data.DataTable

            With me_EnvioCorreosMasivo
                .operacion = "I"
                .cod_user = cod_user
                .tipoCodigoEnvio_ecm = "codigo_pso"
                .codigoEnvio_ecm = g_VariablesGlobales.PersonalCoordinadorAlumni
                .codigo_apl = g_VariablesGlobales.CodigoAplicacionAlumni
                .correo_destino = ls_para
                .asunto = "Módulo de Alumni USAT - Notificación de Envío de Correo"                
                .cuerpo = ls_mensajeEnviar                
            End With

            dt = md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
