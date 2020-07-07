﻿Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class sistema_Permisos_frmPermisoAccionFuncion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_PermisoAccionFuncion As New d_PermisoAccionFuncion
    Dim md_TipoFuncion As New d_TipoFuncion
    Dim md_Personal As New d_Personal
    Dim md_Aplicacion As New d_Aplicacion
    Dim md_PermisoAccion As New d_PermisoAccion

    'ENTIDADES
    Dim me_PermisoAccionFuncion As e_PermisoAccionFuncion
    Dim me_TipoFuncion As e_TipoFuncion
    Dim me_Personal As e_Personal
    Dim me_Aplicacion As e_Aplicacion
    Dim me_PermisoAccion As e_PermisoAccion

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim codigo_apl As Integer = 0
    Dim codigo_tfu As Integer = 0
    Dim nombre_formulario As String = String.Empty

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
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_apl = Request.QueryString("apl")
            codigo_tfu = Request.QueryString("ctf")
            nombre_formulario = Me.Form.Name

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarComboTipoFuncion()
                Call mt_CargarComboPersonal()                
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            If Not fu_ValidarCargarDatos() Then Exit Sub

            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            If Not mt_VerificarPermiso("AGREGAR", False) Then Exit Sub

            If Not fu_ValidarCargarDatos() Then Exit Sub

            Session("frmPermisoAccionFuncion-codigo_paf") = 0
            Session("frmPermisoAccionFuncion-codigo_tfu") = IIf(String.IsNullOrEmpty(Me.cmbPerfilFiltro.SelectedValue), 0, Me.cmbPerfilFiltro.SelectedValue)
            Session("frmPermisoAccionFuncion-codigo_per") = IIf(String.IsNullOrEmpty(Me.cmbUsuarioFiltro.SelectedValue), 0, Me.cmbUsuarioFiltro.SelectedValue)

            Call mt_CargarComboAplicacion()

            Call mt_LimpiarControles("Registro")

            Me.cmbPerfil.SelectedValue = CInt(Session("frmPermisoAccionFuncion-codigo_tfu"))
            Me.cmbUsuario.SelectedValue = CInt(Session("frmPermisoAccionFuncion-codigo_per"))

            Call mt_UpdatePanel("Registro")

            Call mt_CargarPermisos()

            Call mt_FlujoTabs("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaAsignado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaAsignado.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmPermisoAccionFuncion-codigo_paf") = Me.grwListaAsignado.DataKeys(index).Values("codigo_paf").ToString

            Select Case e.CommandName
                Case "Eliminar"
                    If Not mt_VerificarPermiso("ELIMINAR", False) Then Exit Sub

                    If Not mt_EliminarAsignacion(CInt(Session("frmPermisoAccionFuncion-codigo_paf"))) Then Exit Sub

                    Call btnListar_Click(Nothing, Nothing)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbPerfilFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPerfilFiltro.SelectedIndexChanged
        Try
            Me.cmbUsuarioFiltro.SelectedValue = String.Empty

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbUsuarioFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUsuarioFiltro.SelectedIndexChanged
        Try
            Me.cmbPerfilFiltro.SelectedValue = String.Empty

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTemporalidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTemporalidad.SelectedIndexChanged
        Try
            Me.txtFechaInicio.Text = String.Empty
            Me.txtFechaFin.Text = String.Empty

            If Me.cmbTemporalidad.SelectedValue.Equals("T") Then
                Me.txtFechaInicio.Enabled = True
                Me.txtFechaFin.Enabled = True
            Else
                Me.txtFechaInicio.Enabled = False
                Me.txtFechaFin.Enabled = False
            End If

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbAplicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAplicacion.SelectedIndexChanged
        Try
            Call mt_CargarPermisos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaNoAsignado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaNoAsignado.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmPermisoAccionFuncion-codigo_pea") = Me.grwListaNoAsignado.DataKeys(index).Values("codigo_pea").ToString

            Select Case e.CommandName
                Case "Asignar"
                    If Not mt_AsignarPermiso(CInt(Session("frmPermisoAccionFuncion-codigo_pea"))) Then Exit Sub

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

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

                Case "ListaAsignado"
                    Me.udpListaAsignado.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaAsignado", "formatoGrillaAsignado();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "ListaNoAsignado"
                    Me.udpListaNoAsignado.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaNoAsignado", "formatoGrillaNoAsignado();", True)

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

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_VerificarPermiso(ByVal accion_permiso As String, Optional ByVal mostrar_permiso As Boolean = False) As Boolean
        Try
            Dim ls_mensaje As String = String.Empty

            Select Case accion_permiso
                Case "AGREGAR", "ELIMINAR"
                    ls_mensaje = g_Seguridad.VerificarPermiso(codigo_apl, codigo_tfu, cod_user, nombre_formulario, _
                                                               accion_permiso, mostrar_permiso)
            End Select

            If Not String.IsNullOrEmpty(ls_mensaje) Then mt_ShowMessage(ls_mensaje, MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.cmbAplicacion.SelectedValue = String.Empty
                    Me.cmbPerfil.SelectedValue = "0"
                    Me.cmbUsuario.SelectedValue = "0"
                    Me.cmbPerfil.Enabled = False
                    Me.cmbUsuario.Enabled = False
                    Me.cmbTemporalidad.SelectedValue = String.Empty
                    Me.txtFechaInicio.Text = String.Empty
                    Me.txtFechaFin.Text = String.Empty
                    Me.txtFechaInicio.Enabled = False
                    Me.txtFechaFin.Enabled = False
                    Me.chkVerificarRestriccion.Checked = True                    
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmPermisoAccionFuncion-codigo_paf") = Nothing
            Session("frmPermisoAccionFuncion-codigo_tfu") = Nothing
            Session("frmPermisoAccionFuncion-codigo_per") = Nothing
            Session("frmPermisoAccionFuncion-codigo_pea") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipoFuncion()
        Try
            Dim dt As New DataTable : me_TipoFuncion = New e_TipoFuncion

            With me_TipoFuncion
                .operacion = "GEN"
            End With
            dt = md_TipoFuncion.ListarTipoFuncion(me_TipoFuncion)

            Call md_Funciones.CargarCombo(Me.cmbPerfilFiltro, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbPerfil, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboPersonal()
        Try
            Dim dt As New DataTable : me_Personal = New e_Personal

            With me_Personal
                .operacion = "GEN"
            End With
            dt = md_Personal.ListarPersonal(me_Personal)

            Call md_Funciones.CargarCombo(Me.cmbUsuarioFiltro, dt, "codigo_per", "nombres", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbUsuario, dt, "codigo_per", "nombres", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboAplicacion()
        Try
            Dim dt As New DataTable : me_Aplicacion = New e_Aplicacion

            With me_Aplicacion
                .operacion = IIf(CInt(Session("frmPermisoAccionFuncion-codigo_tfu")) = 0, "PER", "TFU")
                .codigo_tfu = IIf(CInt(Session("frmPermisoAccionFuncion-codigo_tfu")) = 0, String.Empty, CInt(Session("frmPermisoAccionFuncion-codigo_tfu")))
                .codigo_per = IIf(CInt(Session("frmPermisoAccionFuncion-codigo_per")) = 0, String.Empty, CInt(Session("frmPermisoAccionFuncion-codigo_per")))
            End With
            dt = md_Aplicacion.ListarAplicacion(me_Aplicacion)

            Call md_Funciones.CargarCombo(Me.cmbAplicacion, dt, "codigo_apl", "descripcion_apl", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_PermisoAccionFuncion = New e_PermisoAccionFuncion

            If Me.grwListaAsignado.Rows.Count > 0 Then Me.grwListaAsignado.DataSource = Nothing : Me.grwListaAsignado.DataBind()

            With me_PermisoAccionFuncion
                .operacion = "ASG"
                .codigo_tfu = Me.cmbPerfilFiltro.SelectedValue
                .codigo_per = Me.cmbUsuarioFiltro.SelectedValue
            End With

            dt = md_PermisoAccionFuncion.ListarPermisoAccionFuncion(me_PermisoAccionFuncion)

            Me.grwListaAsignado.DataSource = dt
            Me.grwListaAsignado.DataBind()

            Call md_Funciones.AgregarHearders(grwListaAsignado)

            Call mt_UpdatePanel("ListaAsignado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarPermisos()
        Try
            Dim dt As New DataTable : me_PermisoAccion = New e_PermisoAccion

            If Me.grwListaNoAsignado.Rows.Count > 0 Then Me.grwListaNoAsignado.DataSource = Nothing : Me.grwListaNoAsignado.DataBind()

            With me_PermisoAccion
                .operacion = "APL"
                .codigo_apl = IIf(String.IsNullOrEmpty(Me.cmbAplicacion.SelectedValue), 0, Me.cmbAplicacion.SelectedValue)
            End With

            dt = md_PermisoAccion.ListarPermisoAccion(me_PermisoAccion)

            Me.grwListaNoAsignado.DataSource = dt
            Me.grwListaNoAsignado.DataBind()

            Call md_Funciones.AgregarHearders(grwListaNoAsignado)

            Call mt_UpdatePanel("ListaNoAsignado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDatos() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbPerfilFiltro.SelectedValue) AndAlso String.IsNullOrEmpty(Me.cmbUsuarioFiltro.SelectedValue) Then mt_ShowMessage("Debe seleccionar un criterio de asignación.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_AsignarPermiso(ByVal codigo_pea As Integer) As Boolean
        Try
            If Not fu_ValidarAsignarPermiso(codigo_pea) Then Return False

            me_PermisoAccionFuncion = md_PermisoAccionFuncion.GetPermisoAccionFuncion(0)

            With me_PermisoAccionFuncion
                .operacion = "I"
                .codigo_apl = Me.cmbAplicacion.SelectedValue
                .codigo_tfu = Me.cmbPerfil.SelectedValue
                .codigo_per = Me.cmbUsuario.SelectedValue
                .codigo_pea = codigo_pea
                .temporalidad_paf = Me.cmbTemporalidad.SelectedValue
                If Me.cmbTemporalidad.SelectedValue.Equals("T") Then
                    .fechaInicio_paf = CDate(Me.txtFechaInicio.Text)
                    .fechaFin_paf = CDate(Me.txtFechaFin.Text)
                End If
                .verificarRestriccion_paf = IIf(Me.chkVerificarRestriccion.Checked, "S", "N")
                .cod_user = cod_user
            End With

            md_PermisoAccionFuncion.RegistrarPermisoAccionFuncion(me_PermisoAccionFuncion)

            Call mt_ShowMessage("¡El permiso se asignó exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarAsignarPermiso(ByVal codigo_pea As Integer) As Boolean
        Try
            If codigo_pea <= 0 Then mt_ShowMessage("Debe seleccionar un permiso.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbAplicacion.SelectedValue) Then mt_ShowMessage("Debe seleccionar una aplicación.", MessageType.warning) : Me.cmbAplicacion.Focus() : Return False
            If CInt(Me.cmbPerfil.SelectedValue) = 0 AndAlso CInt(Me.cmbUsuario.SelectedValue) = 0 Then mt_ShowMessage("Debe seleccionar un perfil o usuario.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbTemporalidad.SelectedValue) Then mt_ShowMessage("Debe seleccionar una temporalidad.", MessageType.warning) : Me.cmbTemporalidad.Focus() : Return False

            If Me.cmbTemporalidad.SelectedValue.Equals("T") Then
                If String.IsNullOrEmpty(Me.txtFechaInicio.Text) Then mt_ShowMessage("Debe ingresar una fecha de inicio.", MessageType.warning) : Me.txtFechaInicio.Focus() : Return False
                If String.IsNullOrEmpty(Me.txtFechaFin.Text) Then mt_ShowMessage("Debe ingresar una fecha de fin.", MessageType.warning) : Me.txtFechaFin.Focus() : Return False
                If CDate(Me.txtFechaInicio.Text) > CDate(Me.txtFechaFin.Text) Then mt_ShowMessage("Debe ingresar una fecha de inicio válida.", MessageType.warning) : Me.txtFechaInicio.Focus() : Return False
            End If

            'Validar registro anterior
            Dim dt As New DataTable : me_PermisoAccionFuncion = New e_PermisoAccionFuncion

            With me_PermisoAccionFuncion
                .operacion = "VAL"
                .codigo_apl = Me.cmbAplicacion.SelectedValue
                .codigo_tfu = Me.cmbPerfil.SelectedValue
                .codigo_per = Me.cmbUsuario.SelectedValue
                .codigo_pea = codigo_pea
                .temporalidad_paf = Me.cmbTemporalidad.SelectedValue
            End With

            dt = md_PermisoAccionFuncion.ListarPermisoAccionFuncion(me_PermisoAccionFuncion)

            If dt.Rows.Count > 0 Then
                If Me.cmbTemporalidad.SelectedValue.Equals("T") Then
                    mt_ShowMessage("Existe una asignación temporal vigente de este permiso.", MessageType.warning) : Return False
                Else
                    mt_ShowMessage("El permiso ya se encuentra asignado.", MessageType.warning) : Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarAsignacion(ByVal codigo_paf As Integer) As Boolean
        Try
            me_PermisoAccionFuncion = md_PermisoAccionFuncion.GetPermisoAccionFuncion(codigo_paf)

            With me_PermisoAccionFuncion
                .operacion = "D"
                .codigo_paf = codigo_paf
                .cod_user = cod_user
            End With

            md_PermisoAccionFuncion.RegistrarPermisoAccionFuncion(me_PermisoAccionFuncion)

            mt_ShowMessage("¡La asignación se elimino exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
