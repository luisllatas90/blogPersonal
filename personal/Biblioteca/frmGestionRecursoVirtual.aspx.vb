Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class Biblioteca_frmGestionRecursoVirtual
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_Categoria As e_Categoria
    Dim me_BibliotecaVirtual As e_BibliotecaVirtual
    Dim me_RecursoVirtual As e_RecursoVirtual
    Dim me_RecursoVirtualDetalle As e_RecursoVirtualDetalle
    Dim me_RecursoVirtualEnlace As e_RecursoVirtualEnlace
    Dim me_ArchivoCompartido As e_ArchivoCompartido
    Dim me_ArchivoCompartidoDetalle As e_ArchivoCompartidoDetalle

    'DATOS
    Dim md_Categoria As New d_Categoria
    Dim md_BibliotecaVirtual As New d_BibliotecaVirtual
    Dim md_Funciones As New d_Funciones
    Dim md_RecursoVirtual As New d_RecursoVirtual
    Dim md_RecursoVirtualDetalle As New d_RecursoVirtualDetalle
    Dim md_RecursoVirtualEnlace As New d_RecursoVirtualEnlace
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_ArchivoCompartidoDetalle As New d_ArchivoCompartidoDetalle

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
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarComboTipoRepositorio()
                Call mt_CargarComboDisciplina()
                Call mt_CargarBibliotecaVirtual()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbContarVisita_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbContarVisita.SelectedIndexChanged
        Try
            Me.cmbBiblioteca.SelectedValue = "0"

            If Me.cmbContarVisita.SelectedValue = "S" Then
                Me.cmbBiblioteca.Enabled = True
            Else
                Me.cmbBiblioteca.Enabled = False
            End If

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbContarVisitaEnlace_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbContarVisitaEnlace.SelectedIndexChanged
        Try
            Me.cmbBibliotecaEnlace.SelectedValue = "0"
            Me.cmbTipoEnlace.SelectedValue = String.Empty

            If Me.cmbContarVisitaEnlace.SelectedValue = "S" Then
                Me.cmbBibliotecaEnlace.Enabled = True
                Me.cmbTipoEnlace.Enabled = False
            Else
                Me.cmbBibliotecaEnlace.Enabled = False
                Me.cmbTipoEnlace.Enabled = True
            End If

            Call cmbTipoEnlace_SelectedIndexChanged(Nothing, Nothing)

            Call mt_UpdatePanel("RegistroEnlace")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoEnlace_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEnlace.SelectedIndexChanged
        Try
            Me.txtLinkEnlace.Text = String.Empty
            Me.txtArchivoEnlace.Dispose()

            If Me.cmbTipoEnlace.SelectedValue = "L" Then
                Me.txtLinkEnlace.ReadOnly = False
                Me.txtArchivoEnlace.Enabled = False

            ElseIf Me.cmbTipoEnlace.SelectedValue = "D" Then
                Me.txtLinkEnlace.ReadOnly = True
                Me.txtArchivoEnlace.Enabled = True

            Else
                Me.txtLinkEnlace.ReadOnly = True
                Me.txtArchivoEnlace.Enabled = False
            End If

            Call mt_UpdatePanel("RegistroEnlace")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmGestionRecursoVirtual-codigo_rvi") = 0
            Call mt_LimpiarControles("Registro")

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

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not mt_RegistrarRecursoVirtual() Then
                Call mt_FlujoTabs("Registro")
                Exit Sub
            End If

            Call mt_CargarDatos()

            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmGestionRecursoVirtual-codigo_rvi") = Me.grwLista.DataKeys(index).Values("codigo_rvi").ToString
            Session("frmGestionRecursoVirtual-nombre_rvi") = Me.grwLista.DataKeys(index).Values("nombre_rvi").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_CargarFormularioRegistro(Session("frmGestionRecursoVirtual-codigo_rvi")) Then Exit Sub

                    Call mt_UpdatePanel("Registro")
                    Call mt_UpdatePanel("Logo")

                    Call mt_FlujoTabs("Registro")

                Case "Detalle"
                    Call mt_LimpiarControles("Detalle")
                    Call mt_HabilitarControl("Detalle", False)
                    Session("frmGestionRecursoVirtual-codigo_rvd") = 0
                    Me.divTituloDetalle.InnerText = "Nuevo Detalle"
                    Me.txtRecursoDetalle.Text = Session("frmGestionRecursoVirtual-nombre_rvi")

                    Call mt_UpdatePanel("RegistroDetalle")

                    Call mt_CargarDatosDetalle(Session("frmGestionRecursoVirtual-codigo_rvi"))

                    Call mt_FlujoTabs("Detalle")

                Case "Inactivar"
                    Call mt_InactivarRecursoVirtual()

                    Call btnListar_Click(Nothing, Nothing)

            End Select
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

    Protected Sub btnNuevoDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoDetalle.Click
        Try
            Call mt_LimpiarControles("Detalle")
            Call mt_HabilitarControl("Detalle", True)
            Session("frmGestionRecursoVirtual-codigo_rvd") = 0
            Me.divTituloDetalle.InnerText = "Nuevo Detalle"

            Call mt_UpdatePanel("RegistroDetalle")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirDetalle.Click
        Try
            Call mt_CargarDatos()

            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaDetalle_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaDetalle.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmGestionRecursoVirtual-codigo_rvd") = Me.grwListaDetalle.DataKeys(index).Values("codigo_rvd").ToString
            Session("frmGestionRecursoVirtual-titulo_rvd") = Me.grwListaDetalle.DataKeys(index).Values("titulo_rvd").ToString

            Select Case e.CommandName
                Case "Editar"                    
                    If Not mt_CargarFormularioDetalle(Session("frmGestionRecursoVirtual-codigo_rvd")) Then Exit Sub

                    Call mt_UpdatePanel("RegistroDetalle")

                Case "Enlace"
                    Call mt_LimpiarControles("Enlace")
                    Call mt_HabilitarControl("Enlace", False)
                    Session("frmGestionRecursoVirtual-codigo_rve") = 0
                    Me.divTituloEnlace.InnerText = "Nuevo Enlace"
                    Me.txtRecursoEnlace.Text = Session("frmGestionRecursoVirtual-nombre_rvi")
                    Me.txtDetalleEnlace.Text = Session("frmGestionRecursoVirtual-titulo_rvd")

                    Call mt_UpdatePanel("RegistroEnlace")

                    Call mt_CargarDatosEnlace(Session("frmGestionRecursoVirtual-codigo_rvd"))

                    Call mt_FlujoTabs("Enlace")

                Case "Eliminar"
                    Call mt_EliminarRecursoVirtualDetalle()

                    Call mt_CargarDatosDetalle(Session("frmGestionRecursoVirtual-codigo_rvi"))

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarDetalle.Click
        Try
            If mt_RegistrarRecursoVirtualDetalle() Then
                Call mt_LimpiarControles("Detalle")
                Call mt_HabilitarControl("Detalle", False)
                Session("frmGestionRecursoVirtual-codigo_rvd") = 0
                Me.divTituloDetalle.InnerText = "Nuevo Detalle"

                Call mt_UpdatePanel("RegistroDetalle")
            End If

            Call mt_CargarDatosDetalle(Session("frmGestionRecursoVirtual-codigo_rvi"))
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevoEnlace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoEnlace.Click
        Try
            Call mt_LimpiarControles("Enlace")
            Call mt_HabilitarControl("Enlace", True)
            Session("frmGestionRecursoVirtual-codigo_rve") = 0
            Me.divTituloEnlace.InnerText = "Nuevo Enlace"

            Call mt_UpdatePanel("RegistroEnlace")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirEnlace_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirEnlace.Click
        Try
            Call mt_CargarDatosDetalle(Session("frmGestionRecursoVirtual-codigo_rvi"))

            Call mt_FlujoTabs("Detalle")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaEnlace_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaEnlace.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmGestionRecursoVirtual-codigo_rve") = Me.grwListaEnlace.DataKeys(index).Values("codigo_rve").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_CargarFormularioEnlace(Session("frmGestionRecursoVirtual-codigo_rve")) Then Exit Sub

                    Call mt_UpdatePanel("RegistroEnlace")

                Case "Archivo"
                    Call mt_DescargarArchivoEnlace(Session("frmGestionRecursoVirtual-codigo_rve"))

                Case "Link"
                    Call mt_AbrirLinkEnlace(Session("frmGestionRecursoVirtual-codigo_rve"))

                Case "Eliminar"
                    Call mt_EliminarRecursoVirtualEnlace()

                    Call mt_CargarDatosEnlace(Session("frmGestionRecursoVirtual-codigo_rvd"))

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaEnlace_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaEnlace.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim IdArchivosCompartidos As String = e.Row.DataItem("IdArchivosCompartidos").ToString

                Dim btnArchivo As LinkButton
                btnArchivo = e.Row.Cells(6).FindControl("btnArchivo")

                Dim btnLink As LinkButton
                btnLink = e.Row.Cells(6).FindControl("btnLink")

                If String.IsNullOrEmpty(IdArchivosCompartidos) OrElse IdArchivosCompartidos = 0 Then
                    btnArchivo.Style.Add("display", "none")
                Else
                    btnLink.Style.Add("display", "none")
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarEnlace_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Not mt_RegistrarRecursoVirtualEnlace() Then
                Call mt_CargarDatosEnlace(Session("frmGestionRecursoVirtual-codigo_rvd"))
                Call mt_FlujoTabs("Enlace")
                Exit Sub
            End If

            Call mt_LimpiarControles("Enlace")
            Call mt_HabilitarControl("Enlace", False)
            Session("frmGestionRecursoVirtual-codigo_rve") = 0
            Me.divTituloEnlace.InnerText = "Nuevo Enlace"

            Call mt_UpdatePanel("RegistroEnlace")

            Call mt_CargarDatosEnlace(Session("frmGestionRecursoVirtual-codigo_rvd"))

            Call mt_FlujoTabs("Enlace")
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

                Case "Logo"
                    Me.udpLogo.Update()

                Case "RegistroDetalle"
                    Me.udpRegistroDetalle.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroDetalleUpdate", "udpRegistroDetalleUpdate();", True)

                Case "ListaDetalle"
                    Me.udpListaDetalle.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaDetalleUpdate", "udpListaDetalleUpdate();", True)

                Case "RegistroEnlace"
                    Me.udpRegistroEnlace.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroEnlaceUpdate", "udpRegistroEnlaceUpdate();", True)

                Case "ListaEnlace"
                    Me.udpListaEnlace.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaEnlaceUpdate", "udpListaEnlaceUpdate();", True)

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

                Case "Detalle"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('detalle-tab');", True)

                Case "Enlace"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('enlace-tab');", True)

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

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmGestionRecursoVirtual-codigo_rvi") = Nothing
            Session("frmGestionRecursoVirtual-codigo_rvd") = Nothing
            Session("frmGestionRecursoVirtual-codigo_rve") = Nothing

            Session("frmGestionRecursoVirtual-nombre_rvi") = Nothing
            Session("frmGestionRecursoVirtual-titulo_rvd") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.cmbTipoRepositorio.SelectedValue = String.Empty
                    Me.cmbDisciplina.SelectedValue = String.Empty
                    Me.txtNombre.Text = String.Empty
                    Me.cmbEstado.SelectedValue = String.Empty
                    Me.txtOrden.Text = String.Empty
                    Me.cmbContarVisita.SelectedValue = String.Empty
                    cmbContarVisita_SelectedIndexChanged(Nothing, Nothing)
                    Me.cmbBiblioteca.SelectedValue = "0"
                    Me.cmbAcceso.SelectedValue = String.Empty
                    Me.txtLogo.Text = String.Empty
                    Me.imgLogo.ImageUrl = g_VariablesGlobales.RutaLogos + "sinimagen.png"
                    Me.txtArchivoLogo.Dispose()

                Case "Detalle"
                    Me.txtTituloDetalle.Text = String.Empty
                    Me.txtCuerpoDetalle.Value = String.Empty
                    Me.cmbAccesoDetalle.SelectedValue = String.Empty
                    Me.txtOrdenDetalle.Text = String.Empty

                Case "Enlace"
                    Me.txtDescripcionEnlace.Text = String.Empty
                    Me.cmbAccesoEnlace.SelectedValue = String.Empty
                    Me.txtOrdenEnlace.Text = String.Empty
                    Me.cmbContarVisitaEnlace.SelectedValue = String.Empty
                    cmbContarVisitaEnlace_SelectedIndexChanged(Nothing, Nothing)
                    Me.cmbBibliotecaEnlace.SelectedValue = 0
                    Me.cmbTipoEnlace.SelectedValue = String.Empty
                    Me.txtLinkEnlace.Text = String.Empty
                    Me.txtArchivoEnlace.Dispose()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_HabilitarControl(ByVal ls_tab As String, ByVal lb_habilitar As Boolean)
        Try
            Select Case ls_tab
                Case "Detalle"
                    Me.txtTituloDetalle.ReadOnly = Not lb_habilitar
                    'Me.txtCuerpoDetalle.Disabled = Not lb_habilitar
                    If lb_habilitar Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "divCuerpoDetalleReadonly", "divCuerpoDetalleReadonly('N');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "divCuerpoDetalleReadonly", "divCuerpoDetalleReadonly('S');", True)
                    End If
                    Me.cmbAccesoDetalle.Enabled = lb_habilitar
                    Me.txtOrdenDetalle.ReadOnly = Not lb_habilitar

                    Me.btnNuevoDetalle.Visible = Not lb_habilitar
                    Me.btnGuardarDetalle.Visible = lb_habilitar

                Case "Enlace"
                    Me.txtDescripcionEnlace.ReadOnly = Not lb_habilitar
                    Me.cmbAccesoEnlace.Enabled = lb_habilitar
                    Me.txtOrdenEnlace.ReadOnly = Not lb_habilitar
                    Me.cmbContarVisitaEnlace.Enabled = lb_habilitar
                    If Not lb_habilitar Then
                        Me.cmbBibliotecaEnlace.Enabled = lb_habilitar
                        Me.cmbTipoEnlace.Enabled = lb_habilitar
                        Me.txtLinkEnlace.ReadOnly = Not lb_habilitar
                        Me.txtArchivoEnlace.Enabled = lb_habilitar
                    End If

                    Me.btnNuevoEnlace.Visible = Not lb_habilitar
                    Me.btnGuardarEnlace.Visible = lb_habilitar

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipoRepositorio()
        Try
            Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "TIPO_REPOSITORIO"
            End With
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbTipoRepositorio, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbTipoRepositorioFiltro, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDisciplina()
        Try
            Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "DISCIPLINA_REPOSITORIO"
            End With
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbDisciplina, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbDisciplinaFiltro, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarBibliotecaVirtual()
        Try
            Dim dt As New Data.DataTable : me_BibliotecaVirtual = New e_BibliotecaVirtual

            With me_BibliotecaVirtual
                .operacion = "GEN"                
            End With
            dt = md_BibliotecaVirtual.ListarBibliotecaVirtual(me_BibliotecaVirtual)

            Call md_Funciones.CargarCombo(Me.cmbBiblioteca, dt, "codigo_biv", "nombre_biv", True, "[-- SELECCIONE --]", "0")
            Call md_Funciones.CargarCombo(Me.cmbBibliotecaEnlace, dt, "codigo_biv", "nombre_biv", True, "[-- SELECCIONE --]", "0")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_RecursoVirtual = New e_RecursoVirtual

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_RecursoVirtual
                .operacion = "GEN"
                .tipoRepo_rvi = Me.cmbTipoRepositorioFiltro.SelectedValue
                .disciplinaRepo_rvi = Me.cmbDisciplinaFiltro.SelectedValue                
            End With

            dt = md_RecursoVirtual.ListarRecursoVirtual(me_RecursoVirtual)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_rvi As Integer) As Boolean
        Try
            me_RecursoVirtual = md_RecursoVirtual.GetRecursoVirtual(codigo_rvi)

            If me_RecursoVirtual.codigo_rvi = 0 Then Return False

            Call mt_LimpiarControles("Registro")

            With me_RecursoVirtual
                Me.cmbTipoRepositorio.SelectedValue = .tipoRepo_rvi
                Me.cmbDisciplina.SelectedValue = .disciplinaRepo_rvi
                Me.txtNombre.Text = .nombre_rvi
                Me.cmbEstado.SelectedValue = .estado_rvi
                Me.txtOrden.Text = .orden_rvi
                Me.cmbContarVisita.SelectedValue = .contarVisita_rvi
                Call cmbContarVisita_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbBiblioteca.SelectedValue = .codigo_biv
                Me.cmbAcceso.SelectedValue = .acceso_rvi

                'Obtener el logo del Shared Files
                If String.IsNullOrEmpty(.IdArchivosCompartidos) OrElse .IdArchivosCompartidos = 0 Then Return True

                'Obtener los datos del archivo compartido
                me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(.IdArchivosCompartidos)
                Dim ls_extensiones As String = ".png .jpg .jpeg"

                'Comprobar que el archivo compartido tenga la extencion de imagen
                If Not ls_extensiones.Contains(me_ArchivoCompartido.extencion.ToLower) Then Return True

                me_ArchivoCompartido.usuario_act = Session("perlogin")
                me_ArchivoCompartido.ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")

                Dim archivo As Byte() = md_ArchivoCompartido.ObtenerArchivoCompartido(me_ArchivoCompartido)

                Dim ms As New IO.MemoryStream(CType(archivo, Byte()))

                me_ArchivoCompartido.content_type = md_Funciones.ObtenerContentType(me_ArchivoCompartido.extencion)

                Me.imgLogo.ImageUrl = "data:" + me_ArchivoCompartido.content_type + ";base64," + Convert.ToBase64String(ms.ToArray(), 0, ms.ToArray().Length)

                Me.txtLogo.Text = .logo_rvi
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarRecursoVirtual() As Boolean
        Try
            If Not fu_ValidarRegistrarRecursoVirtual() Then Return False

            Dim dt As New Data.DataTable : me_RecursoVirtual = md_RecursoVirtual.GetRecursoVirtual(Session("frmGestionRecursoVirtual-codigo_rvi"))

            me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)
            Dim ls_logo As String = String.Empty

            'Archivo adjunto File Shared
            If Me.txtArchivoLogo.HasFile Then
                dt = New Data.DataTable

                With me_ArchivoCompartido
                    .fecha = Date.Now.ToString("dd/MM/yyyy")
                    .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                    .nombre_archivo = Me.txtArchivoLogo.FileName
                    .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("ZWWHAZU474") 'BIB_RecursoVirtual
                    .usuario_reg = Session("perlogin").ToString
                    .cod_user = cod_user
                End With

                'Realizar la carga del archivo compartido
                dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartido, Me.txtArchivoLogo.PostedFile)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo adjunto.", MessageType.error) : Return False

                'Obtener el id y ruta del archivo compartido
                With me_ArchivoCompartido
                    .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString
                    ls_logo = .nombre_archivo
                End With
            End If

            With me_RecursoVirtual
                .operacion = "I"
                .cod_user = cod_user
                .tipoRepo_rvi = Me.cmbTipoRepositorio.SelectedValue
                .disciplinaRepo_rvi = Me.cmbDisciplina.SelectedValue
                .nombre_rvi = Me.txtNombre.Text.Trim
                .estado_rvi = Me.cmbEstado.SelectedValue
                .orden_rvi = Me.txtOrden.Text
                .contarVisita_rvi = Me.cmbContarVisita.SelectedValue
                .codigo_biv = Me.cmbBiblioteca.SelectedValue
                .acceso_rvi = Me.cmbAcceso.SelectedValue
                If Not String.IsNullOrEmpty(ls_logo) Then .logo_rvi = ls_logo
                If me_ArchivoCompartido.id_archivos_compartidos > 0 Then .IdArchivosCompartidos = me_ArchivoCompartido.id_archivos_compartidos
            End With

            dt = New Data.DataTable
            dt = md_RecursoVirtual.RegistrarRecursoVirtual(me_RecursoVirtual)

            'Insertar el detalle del archivo compartido
            If dt.Rows.Count > 0 AndAlso Me.txtArchivoLogo.HasFile Then
                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                With me_ArchivoCompartidoDetalle
                    .operacion = "I"
                    .tabla_acd = "BIB_RecursoVirtual"
                    .codigoTabla_acd = dt.Rows(0).Item("codigo_rvi")
                End With

                me_ArchivoCompartido.detalles.Add(me_ArchivoCompartidoDetalle)

                'Registrar en la tabla detalle del archivo compartido
                md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartido)
            End If

            Call mt_ShowMessage("¡El registro se realizó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarRecursoVirtual() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbTipoRepositorio.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbDisciplina.SelectedValue) Then mt_ShowMessage("Debe seleccionar una disciplina.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbEstado.SelectedValue) Then mt_ShowMessage("Debe seleccionar un estado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtOrden.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de orden.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbContarVisita.SelectedValue) Then mt_ShowMessage("Debe indicar si se contarán o no las visitas.", MessageType.warning) : Return False
            If Me.cmbContarVisita.SelectedValue = "S" AndAlso (String.IsNullOrEmpty(Me.cmbBiblioteca.SelectedValue) OrElse Me.cmbBiblioteca.SelectedValue = 0) Then mt_ShowMessage("Debe seleccionar una biblioteca.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbAcceso.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de acceso.", MessageType.warning) : Return False

            If Me.txtArchivoLogo.HasFile Then
                Dim ls_extensiones As String = ".png .jpeg .jpg"
                If Not ls_extensiones.Contains(System.IO.Path.GetExtension(Me.txtArchivoLogo.FileName).ToString().Trim.ToLower) Then mt_ShowMessage("Debe ingresar un archivo con extensión .png o .jpg o .jpeg .", MessageType.warning) : Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_InactivarRecursoVirtual() As Boolean
        Try
            me_RecursoVirtual = md_RecursoVirtual.GetRecursoVirtual(Session("frmGestionRecursoVirtual-codigo_rvi"))

            With me_RecursoVirtual
                .operacion = "U"
                .cod_user = cod_user
                .estado_rvi = "I"
            End With

            md_RecursoVirtual.RegistrarRecursoVirtual(me_RecursoVirtual)

            Call mt_ShowMessage("¡El registro se inactivó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarDatosDetalle(ByVal codigo_rvi As Integer)
        Try
            Dim dt As New DataTable : me_RecursoVirtualDetalle = New e_RecursoVirtualDetalle

            If Me.grwListaDetalle.Rows.Count > 0 Then Me.grwListaDetalle.DataSource = Nothing : Me.grwListaDetalle.DataBind()

            With me_RecursoVirtualDetalle
                .operacion = "GEN"
                .codigo_rvi = codigo_rvi
            End With

            dt = md_RecursoVirtualDetalle.ListarRecursoVirtualDetalle(me_RecursoVirtualDetalle)

            Me.grwListaDetalle.DataSource = dt
            Me.grwListaDetalle.DataBind()

            Call md_Funciones.AgregarHearders(grwListaDetalle)

            Call mt_UpdatePanel("ListaDetalle")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioDetalle(ByVal codigo_rvd As Integer) As Boolean
        Try
            me_RecursoVirtualDetalle = md_RecursoVirtualDetalle.GetRecursoVirtualDetalle(codigo_rvd)

            If me_RecursoVirtualDetalle.codigo_rvd = 0 Then Return False

            Call mt_LimpiarControles("Detalle")
            Call mt_HabilitarControl("Detalle", True)
            Me.divTituloDetalle.InnerText = "Editar Detalle"

            With me_RecursoVirtualDetalle
                Me.txtTituloDetalle.Text = .titulo_rvd
                'Me.txtCuerpoDetalle.InnerText = .cuerpo_rvd
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "setCodigoCuerpoDetalle", "setCodigoCuerpoDetalle('" & fu_EliminarSaltosLinea(.cuerpo_rvd, "") & "');", True)
                Me.cmbAccesoDetalle.SelectedValue = .acceso_rvd
                Me.txtOrdenDetalle.Text = .orden_rvd
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarRecursoVirtualDetalle() As Boolean
        Try
            If Not fu_ValidarRegistrarRecursoVirtualDetalle() Then Return False

            Dim dt As New Data.DataTable : me_RecursoVirtualDetalle = md_RecursoVirtualDetalle.GetRecursoVirtualDetalle(Session("frmGestionRecursoVirtual-codigo_rvd"))

            With me_RecursoVirtualDetalle
                .operacion = "I"
                .cod_user = cod_user
                .codigo_rvi = Session("frmGestionRecursoVirtual-codigo_rvi")
                .titulo_rvd = Me.txtTituloDetalle.Text.Trim
                .cuerpo_rvd = HttpUtility.UrlDecode(Me.txtCuerpoDetalle.Value)
                .acceso_rvd = Me.cmbAccesoDetalle.SelectedValue
                .orden_rvd = Me.txtOrdenDetalle.Text.Trim
            End With

            dt = md_RecursoVirtualDetalle.RegistrarRecursoVirtualDetalle(me_RecursoVirtualDetalle)

            Call mt_ShowMessage("¡El registro se realizó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarRecursoVirtualDetalle() As Boolean
        Try
            Dim ls_CuerpoDetalle As String = HttpUtility.UrlDecode(Me.txtCuerpoDetalle.Value)

            If String.IsNullOrEmpty(Me.txtTituloDetalle.Text.Trim) Then mt_ShowMessage("Debe ingresar un título.", MessageType.warning) : Return False            
            If String.IsNullOrEmpty(ls_CuerpoDetalle.Trim) OrElse ls_CuerpoDetalle.Trim = "<p>&nbsp;</p>" OrElse ls_CuerpoDetalle.Trim = "<p><br></p>" Then mt_ShowMessage("Debe ingresar un cuerpo.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbAccesoDetalle.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de acceso.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtOrdenDetalle.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de orden.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarRecursoVirtualDetalle() As Boolean
        Try
            me_RecursoVirtualDetalle = md_RecursoVirtualDetalle.GetRecursoVirtualDetalle(Session("frmGestionRecursoVirtual-codigo_rvd"))

            With me_RecursoVirtualDetalle
                .operacion = "D"
                .cod_user = cod_user
            End With

            md_RecursoVirtualDetalle.RegistrarRecursoVirtualDetalle(me_RecursoVirtualDetalle)

            Call mt_ShowMessage("¡El registro se eliminó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarDatosEnlace(ByVal codigo_rvd As Integer)
        Try
            Dim dt As New DataTable : me_RecursoVirtualEnlace = New e_RecursoVirtualEnlace

            If Me.grwListaEnlace.Rows.Count > 0 Then Me.grwListaEnlace.DataSource = Nothing : Me.grwListaEnlace.DataBind()

            With me_RecursoVirtualEnlace
                .operacion = "GEN"
                .codigo_rvd = codigo_rvd
            End With

            dt = md_RecursoVirtualEnlace.ListarRecursoVirtualEnlace(me_RecursoVirtualEnlace)

            Me.grwListaEnlace.DataSource = dt
            Me.grwListaEnlace.DataBind()

            Call md_Funciones.AgregarHearders(grwListaEnlace)

            Call mt_UpdatePanel("ListaEnlace")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioEnlace(ByVal codigo_rve As Integer) As Boolean
        Try
            me_RecursoVirtualEnlace = md_RecursoVirtualEnlace.GetRecursoVirtualEnlace(codigo_rve)

            If me_RecursoVirtualEnlace.codigo_rve = 0 Then Return False

            Call mt_LimpiarControles("Enlace")
            Call mt_HabilitarControl("Enlace", True)
            Me.divTituloEnlace.InnerText = "Editar Enlace"

            With me_RecursoVirtualEnlace
                Me.txtDescripcionEnlace.Text = .descripcion_rve
                Me.cmbAccesoEnlace.SelectedValue = .acceso_rve
                Me.txtOrdenEnlace.Text = .orden_rve
                Me.cmbContarVisitaEnlace.SelectedValue = .contarVisita_rve
                Call cmbContarVisitaEnlace_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbBibliotecaEnlace.SelectedValue = .codigo_biv
                If .codigo_biv > 0 Then Return True

                If .IdArchivosCompartidos > 0 Then
                    Me.cmbTipoEnlace.SelectedValue = "D"
                    Call cmbTipoEnlace_SelectedIndexChanged(Nothing, Nothing)
                Else
                    Me.cmbTipoEnlace.SelectedValue = "L"
                    Call cmbTipoEnlace_SelectedIndexChanged(Nothing, Nothing)
                    Me.txtLinkEnlace.Text = .enlace_rve
                End If
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarRecursoVirtualEnlace() As Boolean
        Try
            If Not fu_ValidarRegistrarRecursoVirtualEnlace() Then Return False

            Dim dt As New Data.DataTable : me_RecursoVirtualEnlace = md_RecursoVirtualEnlace.GetRecursoVirtualEnlace(Session("frmGestionRecursoVirtual-codigo_rve"))

            If Me.cmbTipoEnlace.SelectedValue = "D" AndAlso Not Me.txtArchivoEnlace.HasFile AndAlso _
                (String.IsNullOrEmpty(me_RecursoVirtualEnlace.IdArchivosCompartidos) OrElse me_RecursoVirtualEnlace.IdArchivosCompartidos = 0) Then

                mt_ShowMessage("Debe seleccionar un archivo.", MessageType.warning)
                Return False
            End If

            me_ArchivoCompartido = md_ArchivoCompartido.GetArchivoCompartido(0)
            Dim ls_archivo As String = String.Empty

            If Me.cmbTipoEnlace.SelectedValue = "D" Then
                'Archivo adjunto File Shared
                If Me.txtArchivoEnlace.HasFile Then
                    dt = New Data.DataTable

                    With me_ArchivoCompartido
                        .fecha = Date.Now.ToString("dd/MM/yyyy")
                        .ruta_archivo = ConfigurationManager.AppSettings("SharedFiles")
                        .nombre_archivo = Me.txtArchivoEnlace.FileName
                        .id_tabla = md_ArchivoCompartido.ObtenerIdTabla("SGN9UYLA23") 'BIB_RecursoVirtualEnlace
                        .usuario_reg = Session("perlogin").ToString
                        .cod_user = cod_user
                    End With

                    'Realizar la carga del archivo compartido
                    dt = md_ArchivoCompartido.CargarArchivoCompartido(me_ArchivoCompartido, Me.txtArchivoEnlace.PostedFile)

                    If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha podido cargar el archivo adjunto.", MessageType.error) : Return False

                    'Obtener el id y ruta del archivo compartido
                    With me_ArchivoCompartido
                        .id_archivos_compartidos = dt.Rows(0).Item("IdArchivosCompartidos").ToString
                        ls_archivo = .nombre_archivo
                    End With
                End If
            End If

            With me_RecursoVirtualEnlace
                .operacion = "I"
                .cod_user = cod_user
                .codigo_rvd = Session("frmGestionRecursoVirtual-codigo_rvd")
                .descripcion_rve = Me.txtDescripcionEnlace.Text.Trim
                .contarVisita_rve = Me.cmbContarVisitaEnlace.SelectedValue
                .codigo_biv = Me.cmbBibliotecaEnlace.SelectedValue
                .acceso_rve = Me.cmbAccesoEnlace.SelectedValue
                .orden_rve = Me.txtOrdenEnlace.Text.Trim

                If Me.cmbContarVisitaEnlace.SelectedValue = "S" Then
                    .enlace_rve = String.Empty
                    .IdArchivosCompartidos = 0
                Else
                    If Me.cmbTipoEnlace.SelectedValue = "L" Then
                        .enlace_rve = Me.txtLinkEnlace.Text.Trim
                        .IdArchivosCompartidos = 0
                    ElseIf Me.cmbTipoEnlace.SelectedValue = "D" Then
                        .enlace_rve = String.Empty
                        If me_ArchivoCompartido.id_archivos_compartidos > 0 Then .IdArchivosCompartidos = me_ArchivoCompartido.id_archivos_compartidos
                    End If
                End If
            End With

            dt = New Data.DataTable
            dt = md_RecursoVirtualEnlace.RegistrarRecursoVirtualEnlace(me_RecursoVirtualEnlace)

            'Insertar el detalle del archivo compartido
            If dt.Rows.Count > 0 AndAlso Me.txtArchivoLogo.HasFile Then
                me_ArchivoCompartidoDetalle = md_ArchivoCompartidoDetalle.GetArchivoCompartidoDetalle(0)

                With me_ArchivoCompartidoDetalle
                    .operacion = "I"
                    .tabla_acd = "BIB_RecursoVirtualEnlace"
                    .codigoTabla_acd = dt.Rows(0).Item("codigo_rve")
                End With

                me_ArchivoCompartido.detalles.Add(me_ArchivoCompartidoDetalle)

                'Registrar en la tabla detalle del archivo compartido
                md_ArchivoCompartidoDetalle.RegistrarArchivoCompartidoDetalle(me_ArchivoCompartido)
            End If

            Call mt_ShowMessage("¡El registro se realizó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarRecursoVirtualEnlace() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtDescripcionEnlace.Text.Trim) Then mt_ShowMessage("Debe ingresar una descripción.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbAccesoEnlace.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de acceso.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtOrdenEnlace.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de orden.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbContarVisitaEnlace.SelectedValue) Then mt_ShowMessage("Debe indicar si se contarán o no las visitas.", MessageType.warning) : Return False
            If Me.cmbContarVisitaEnlace.SelectedValue = "S" AndAlso (String.IsNullOrEmpty(Me.cmbBibliotecaEnlace.SelectedValue) OrElse Me.cmbBibliotecaEnlace.SelectedValue = 0) Then mt_ShowMessage("Debe seleccionar una biblioteca.", MessageType.warning) : Return False
            If Me.cmbContarVisitaEnlace.SelectedValue = "N" Then
                If String.IsNullOrEmpty(Me.cmbTipoEnlace.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de enlace.", MessageType.warning) : Return False
                If Me.cmbContarVisitaEnlace.SelectedValue = "L" AndAlso String.IsNullOrEmpty(Me.txtLinkEnlace.Text.Trim) Then mt_ShowMessage("Debe ingresar un link.", MessageType.warning) : Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_DescargarArchivoEnlace(ByVal codigo_rve As Integer)
        Try
            me_RecursoVirtualEnlace = md_RecursoVirtualEnlace.GetRecursoVirtualEnlace(codigo_rve)

            With me_RecursoVirtualEnlace
                If .codigo_rve = 0 Then mt_ShowMessage("El registro no ha sido encontrado.", MessageType.warning) : Exit Sub
                If String.IsNullOrEmpty(.IdArchivosCompartidos) OrElse .IdArchivosCompartidos = 0 Then mt_ShowMessage("El enlace no presenta archivo asociado.", MessageType.warning) : Exit Sub

                Me.udpScripts.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../frmDescargarArchivoCompartido.aspx?Id=" & .IdArchivosCompartidos & "');", True)
            End With
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_AbrirLinkEnlace(ByVal codigo_rve As Integer)
        Try
            me_RecursoVirtualEnlace = md_RecursoVirtualEnlace.GetRecursoVirtualEnlace(codigo_rve)

            With me_RecursoVirtualEnlace
                If .codigo_rve = 0 Then mt_ShowMessage("El registro no ha sido encontrado.", MessageType.warning) : Exit Sub

                Me.udpScripts.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('" & me_RecursoVirtualEnlace.enlace & "');", True)
            End With
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_EliminarRecursoVirtualEnlace() As Boolean
        Try
            me_RecursoVirtualEnlace = md_RecursoVirtualEnlace.GetRecursoVirtualEnlace(Session("frmGestionRecursoVirtual-codigo_rve"))

            With me_RecursoVirtualEnlace
                .operacion = "D"
                .cod_user = cod_user
            End With

            md_RecursoVirtualEnlace.RegistrarRecursoVirtualEnlace(me_RecursoVirtualEnlace)

            Call mt_ShowMessage("¡El registro se eliminó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_EliminarSaltosLinea(ByVal texto As String, ByVal caracter_reemplazar As String) As String
        Try
            Return Replace(Replace(texto, Chr(10), caracter_reemplazar), Chr(13), caracter_reemplazar)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
