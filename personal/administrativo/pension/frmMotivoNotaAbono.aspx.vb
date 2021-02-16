Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class Pensiones_frmMotivoNotaAbono
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"    
    'ENTIDADES
    Dim me_MotivoNotaAbono As e_MotivoNotaAbono
    Dim me_GrupoMotivoAbono As e_GrupoMotivoAbono

    'DATOS
    Dim md_MotivoNotaAbono As New d_MotivoNotaAbono
    Dim md_GrupoMotivoAbono As New d_GrupoMotivoAbono
    Dim md_Funciones As New d_Funciones

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
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")            

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()
                Call mt_CargarComboGrupo()                
            End If
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmMotivoNotaAbono-codigo_mno") = 0

            Call mt_LimpiarControles("Registro")

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmMotivoNotaAbono-codigo_mno") = Me.grwLista.DataKeys(index).Values("codigo_mno").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_CargarFormularioRegistro(CInt(Session("frmMotivoNotaAbono-codigo_mno"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

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

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarMotivo(CInt(Session("frmMotivoNotaAbono-codigo_mno"))) Then
                Call btnListar_Click(Nothing, Nothing)
                Call mt_FlujoTabs("Listado")
                Exit Sub
            End If
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

    Private Sub mt_CargarComboGrupo()
        Try
            Dim dt As New Data.DataTable : me_GrupoMotivoAbono = New e_GrupoMotivoAbono

            With me_GrupoMotivoAbono
                .operacion = "GEN"
            End With
            dt = md_GrupoMotivoAbono.ListarGrupoMotivoAbono(me_GrupoMotivoAbono)

            Call md_Funciones.CargarCombo(Me.cmbGrupo, dt, "codigo_gmn", "nombre_gmn", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbGrupoFiltro, dt, "codigo_gmn", "nombre_gmn", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.cmbGrupo.SelectedValue = String.Empty
                    Me.txtDescripcion.Text = String.Empty
                    Me.chkConvenioBeca.Checked = False
                    Me.chkSolicitudAnulacion.Checked = False
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmMotivoNotaAbono-codigo_mno") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_MotivoNotaAbono = New e_MotivoNotaAbono

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_MotivoNotaAbono
                .operacion = "GEN"
                .codigo_gmn = cmbGrupoFiltro.SelectedValue
            End With

            dt = md_MotivoNotaAbono.ListarMotivoNotaAbono(me_MotivoNotaAbono)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_mno As Integer) As Boolean
        Try
            me_MotivoNotaAbono = md_MotivoNotaAbono.GetMotivoNotaAbono(codigo_mno)

            If me_MotivoNotaAbono.codigo_mno = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")

            With me_MotivoNotaAbono
                Me.txtDescripcion.Text = .descripcion_mno
                Me.cmbGrupo.SelectedValue = .codigo_gmn
                Me.chkConvenioBeca.Checked = .conveniobeca
                Me.chkSolicitudAnulacion.Checked = .solicitudAnulacion
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            If Session("frmMotivoNotaAbono-codigo_mno") Is Nothing OrElse String.IsNullOrEmpty(Session("frmMotivoNotaAbono-codigo_mno")) Then mt_ShowMessage("El código de motivo de nota de abono no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarMotivo(ByVal codigo_mno As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarMotivo() Then Return False

            me_MotivoNotaAbono = md_MotivoNotaAbono.GetMotivoNotaAbono(codigo_mno)

            With me_MotivoNotaAbono
                .operacion = "I"                
                .descripcion_mno = Me.txtDescripcion.Text.Trim.ToUpper
                .codigo_gmn = Me.cmbGrupo.SelectedValue
                .conveniobeca = Me.chkConvenioBeca.Checked
                .solicitudAnulacion = Me.chkSolicitudAnulacion.Checked
            End With

            md_MotivoNotaAbono.RegistrarMotivoNotaAbono(me_MotivoNotaAbono)

            Call mt_ShowMessage("¡El motivo de nota de abono se registró exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarMotivo() As Boolean
        Try
            If String.IsNullOrEmpty(Me.txtDescripcion.Text.Trim) Then mt_ShowMessage("Debe ingresar una descripción.", MessageType.warning) : Me.txtDescripcion.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbGrupo.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un grupo.", MessageType.warning) : Me.cmbGrupo.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
