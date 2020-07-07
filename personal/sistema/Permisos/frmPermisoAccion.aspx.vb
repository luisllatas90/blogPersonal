﻿Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class sistema_Permisos_frmPermisoAccion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_PermisoAccion As New d_PermisoAccion

    'ENTIDADES
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
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccion.SelectedIndexChanged
        Try
            Me.txtAccion.Text = String.Empty

            If String.IsNullOrEmpty(Me.cmbAccion.SelectedValue) Then Me.txtAccion.ReadOnly = False Else Me.txtAccion.ReadOnly = True

            Call mt_UpdatePanel("Registro")
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            If Not mt_VerificarPermiso("AGREGAR", False) Then Exit Sub

            Session("frmPermisoAccion-codigo_pea") = 0

            Call mt_LimpiarControles("Registro")

            Call cmbAccion_SelectedIndexChanged(Nothing, Nothing)

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmPermisoAccion-codigo_pea") = Me.grwLista.DataKeys(index).Values("codigo_pea").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_VerificarPermiso("EDITAR", False) Then Exit Sub

                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    If Not mt_CargarFormularioRegistro(CInt(Session("frmPermisoAccion-codigo_pea"))) Then Exit Sub

                    Call cmbAccion_SelectedIndexChanged(Nothing, Nothing)

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")
                Case "Eliminar"
                    If Not mt_VerificarPermiso("ELIMINAR", False) Then Exit Sub

                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    If Not mt_EliminarPermiso(CInt(Session("frmPermisoAccion-codigo_pea"))) Then Exit Sub

                    Call btnListar_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarPermiso(CInt(Session("frmPermisoAccion-codigo_pea"))) Then
                Call btnListar_Click(Nothing, Nothing)
                Call mt_FlujoTabs("Listado")
                Exit Sub
            End If
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

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

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
                Case "AGREGAR", "EDITAR", "ELIMINAR"
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
                    Me.txtNombre.Text = String.Empty
                    Me.cmbAccion.SelectedValue = String.Empty
                    Me.txtAccion.Text = String.Empty
                    Me.txtFormulario.Text = String.Empty
                    Me.txtDescripcion.Text = String.Empty

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmPermisoAccion-codigo_pea") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_PermisoAccion = New e_PermisoAccion

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_PermisoAccion
                .operacion = "GEN"
                .nombreFormulario_pea = Me.txtNombreFiltro.Text.Trim.ToUpper                
            End With

            dt = md_PermisoAccion.ListarPermisoAccion(me_PermisoAccion)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_pea As Integer) As Boolean
        Try           
            me_PermisoAccion = md_PermisoAccion.GetPermisoAccion(codigo_pea)
            Dim ls_Acciones As String = "AGREGAR EDITAR ELIMINAR CONSULTAR"

            If me_PermisoAccion.codigo_pea = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")

            With me_PermisoAccion
                Me.txtFormulario.Text = .formulario_pea.Trim
                Me.txtNombre.Text = .nombreFormulario_pea.Trim.ToUpper

                If ls_Acciones.Contains(.accion_pea.Trim.ToUpper) Then
                    Me.cmbAccion.SelectedValue = .accion_pea.Trim.ToUpper
                    Me.txtAccion.Text = String.Empty
                Else
                    Me.cmbAccion.SelectedValue = String.Empty
                    Me.txtAccion.Text = .accion_pea.Trim.ToUpper
                End If

                Me.txtDescripcion.Text = .descripcion_pea.Trim.ToUpper
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            If Session("frmPermisoAccion-codigo_pea") Is Nothing OrElse String.IsNullOrEmpty(Session("frmPermisoAccion-codigo_pea")) Then mt_ShowMessage("El código de permiso no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarPermiso(ByVal codigo_pea As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarPermiso() Then Return False

            me_PermisoAccion = md_PermisoAccion.GetPermisoAccion(codigo_pea)

            With me_PermisoAccion
                .operacion = "I"
                .cod_user = cod_user
                .nombreFormulario_pea = Me.txtNombre.Text.Trim.ToUpper
                .formulario_pea = Me.txtFormulario.Text.Trim.ToUpper
                .accion_pea = IIf(String.IsNullOrEmpty(Me.cmbAccion.SelectedValue), Me.txtAccion.Text.Trim.ToUpper, Me.cmbAccion.SelectedValue)
                .descripcion_pea = Me.txtDescripcion.Text.Trim.ToUpper
            End With

            md_PermisoAccion.RegistrarPermisoAccion(me_PermisoAccion)

            Call mt_ShowMessage("¡El permiso se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarPermiso() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtFormulario.Text.Trim) Then mt_ShowMessage("Debe ingresar un formulario.", MessageType.warning) : Me.txtFormulario.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("Debe ingresar un título de formulario.", MessageType.warning) : Me.txtNombre.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbAccion.SelectedValue) AndAlso String.IsNullOrEmpty(Me.txtAccion.Text.Trim) Then mt_ShowMessage("Debe seleccionar o ingresar una acción a restringir.", MessageType.warning) : Me.txtAccion.Focus() : Return False

            'Validar que no existan registros activos con el mismo nombre

            Dim dt As New DataTable : me_PermisoAccion = New e_PermisoAccion

            With me_PermisoAccion
                .operacion = "VAL"
                .codigo_pea = CInt(Session("frmPermisoAccion-codigo_pea"))
                .formulario_pea = Me.txtFormulario.Text.Trim.ToUpper
                .accion_pea = IIf(String.IsNullOrEmpty(Me.cmbAccion.SelectedValue), Me.txtAccion.Text.Trim.ToUpper, Me.cmbAccion.SelectedValue)
            End With

            dt = md_PermisoAccion.ListarPermisoAccion(me_PermisoAccion)

            If dt.Rows.Count > 0 Then mt_ShowMessage("El permiso ya se encuentra registrado.", MessageType.warning) : Me.txtNombre.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarPermiso(ByVal codigo_pea As Integer) As Boolean
        Try
            me_PermisoAccion = md_PermisoAccion.GetPermisoAccion(codigo_pea)

            With me_PermisoAccion
                .operacion = "D"
                .codigo_pea = codigo_pea
                .cod_user = cod_user
            End With

            md_PermisoAccion.RegistrarPermisoAccion(me_PermisoAccion)

            mt_ShowMessage("¡El permiso se elimino exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
