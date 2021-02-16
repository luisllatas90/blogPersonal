Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class GraduacionTitulacion_Deudas_frmConfiguracionInstanciaAdeudo
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES    
    Dim me_ConfiguracionInstanciasAdeudos As e_ConfiguracionInstanciasAdeudos
    Dim me_ServicioConcepto As e_ServicioConcepto
    Dim me_CentroCostos As e_CentroCostos
    Dim me_TipoFuncion As e_TipoFuncion

    'DATOS
    Dim md_ConfiguracionInstanciasAdeudos As New d_ConfiguracionInstanciasAdeudos
    Dim md_ServicioConcepto As New d_ServicioConcepto
    Dim md_CentroCostos As New d_CentroCostos
    Dim md_TipoFuncion As New d_TipoFuncion
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
                Response.Redirect("../../../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()
                mt_CargarComboArea()
                mt_CargarComboCargo()                
                mt_CargarComboEstado()
                mt_CargarComboServicioConcepto()
                mt_CargarComboCentroCostos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbConcepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbConcepto.SelectedIndexChanged
        Try
            Call mt_CargarComboCentroCostos()

            Call mt_UpdatePanel("Registro")
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

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmConfiguracionInstanciaAdeudo-codigo_cia") = 0

            Call mt_LimpiarControles("Registro")
            Me.cmbEstado.SelectedValue = "A"
            Me.cmbCentroCosto.SelectedValue = 0

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmConfiguracionInstanciaAdeudo-codigo_cia") = Me.grwLista.DataKeys(index).Values("codigo_cia").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_CargarFormularioRegistro(Session("frmConfiguracionInstanciaAdeudo-codigo_cia")) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarConfiguracion(CInt(Session("frmConfiguracionInstanciaAdeudo-codigo_cia"))) Then
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

    Private Sub mt_CargarComboArea()
        Try
            Dim dt As New Data.DataTable : Dim le_ConfiguracionInstanciasAdeudos As New e_ConfiguracionInstanciasAdeudos

            With le_ConfiguracionInstanciasAdeudos
                .operacion = "ARE"                
            End With
            dt = md_ConfiguracionInstanciasAdeudos.ListarConfiguracionInstanciasAdeudos(le_ConfiguracionInstanciasAdeudos)

            Call md_Funciones.CargarCombo(Me.cmbAreaFiltro, dt, "codigoArea_cco", "descripcionArea_cco", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbArea, dt, "codigoArea_cco", "descripcionArea_cco", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCargo()
        Try
            Dim dt As New Data.DataTable : me_TipoFuncion = New e_TipoFuncion

            With me_TipoFuncion
                .operacion = "GEN"                
            End With
            dt = md_TipoFuncion.ListarTipoFuncion(me_TipoFuncion)

            Call md_Funciones.CargarCombo(Me.cmbCargoFiltro, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbCargo, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboEstado()
        Try
            Dim dt As New Data.DataTable : md_Funciones = New d_Funciones
            dt = md_Funciones.ObtenerDataTable("ESTADO_CONF_INSTANCIA_ADEUDOS")

            Call md_Funciones.CargarCombo(Me.cmbEstado, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbEstadoFiltro, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboServicioConcepto()
        Try
            Dim dt As New Data.DataTable : me_ServicioConcepto = New e_ServicioConcepto

            With me_ServicioConcepto
                .operacion = "GEN"
                .adeudo_sco = "S"
            End With
            dt = md_ServicioConcepto.ListarServicioConcepto(me_ServicioConcepto)

            Call md_Funciones.CargarCombo(Me.cmbConcepto, dt, "codigo_Sco", "descripcion_Sco", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCentroCostos()
        Try
            Dim dt As New Data.DataTable : me_CentroCostos = New e_CentroCostos

            With me_CentroCostos
                .operacion = "SCC"
                .codigo_sco = IIf(String.IsNullOrEmpty(cmbConcepto.SelectedValue), "0", cmbConcepto.SelectedValue)
            End With
            dt = md_CentroCostos.ListarCentroCostos(me_CentroCostos)

            Call md_Funciones.CargarCombo(Me.cmbCentroCosto, dt, "codigo_Cco", "descripcion_Cco", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.cmbArea.SelectedValue = 0
                    Me.cmbCargo.SelectedValue = 0
                    Me.cmbEstado.SelectedValue = String.Empty
                    Me.cmbConcepto.SelectedValue = 0                    
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmConfiguracionInstanciaAdeudo-codigo_cia") = Nothing
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

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_ConfiguracionInstanciasAdeudos = New e_ConfiguracionInstanciasAdeudos

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_ConfiguracionInstanciasAdeudos
                .operacion = "LIS"
                .codigoArea_cco = cmbAreaFiltro.SelectedValue
                .codigo_tfu = cmbCargoFiltro.SelectedValue
                .estado_cia = cmbEstadoFiltro.SelectedValue
            End With

            dt = md_ConfiguracionInstanciasAdeudos.ListarConfiguracionInstanciasAdeudos(me_ConfiguracionInstanciasAdeudos)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarConfiguracion(ByVal codigo_cia As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarConfiguracion() Then Return False

            me_ConfiguracionInstanciasAdeudos = md_ConfiguracionInstanciasAdeudos.GetConfiguracionInstanciasAdeudos(codigo_cia)

            With me_ConfiguracionInstanciasAdeudos
                .operacion = "I"
                .cod_user = cod_user
                .codigoArea_cco = Me.cmbArea.SelectedValue
                .codigo_tfu = Me.cmbCargo.SelectedValue
                .codigo_sco = Me.cmbConcepto.SelectedValue
                .codigo_cco = Me.cmbCentroCosto.SelectedValue
                .estado_cia = Me.cmbEstado.SelectedValue
            End With

            md_ConfiguracionInstanciasAdeudos.RegistrarConfiguracionInstanciasAdeudos(me_ConfiguracionInstanciasAdeudos)

            Call mt_ShowMessage("¡La configuración se registró exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarConfiguracion() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbArea.SelectedValue) OrElse Me.cmbArea.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un área.", MessageType.warning) : Me.cmbArea.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbCargo.SelectedValue) OrElse Me.cmbCargo.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un cargo.", MessageType.warning) : Me.cmbArea.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbEstado.SelectedValue) Then mt_ShowMessage("Debe seleccionar un estado.", MessageType.warning) : Me.cmbEstado.Focus() : Return False
            If Not String.IsNullOrEmpty(Me.cmbConcepto.SelectedValue) AndAlso Not Me.cmbConcepto.SelectedValue = 0 Then
                If String.IsNullOrEmpty(Me.cmbCentroCosto.SelectedValue) OrElse Me.cmbCentroCosto.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un centro de costo.", MessageType.warning) : Me.cmbCentroCosto.Focus() : Return False
            End If

            Dim dt As New DataTable : Dim le_ConfiguracionInstanciasAdeudos As New e_ConfiguracionInstanciasAdeudos

            With le_ConfiguracionInstanciasAdeudos
                .operacion = "GEN"
                .codigoArea_cco = Me.cmbArea.SelectedValue
                .codigo_tfu = Me.cmbCargo.SelectedValue
            End With

            dt = md_ConfiguracionInstanciasAdeudos.ListarConfiguracionInstanciasAdeudos(le_ConfiguracionInstanciasAdeudos)

            If dt.Rows.Count > 0 Then
                For Each fila As DataRow In dt.Rows
                    If CInt(fila("codigo_cia").ToString) <> CInt(Session("frmConfiguracionInstanciaAdeudo-codigo_cia")) Then
                        mt_ShowMessage("Existe una configuración registrada con esta área y cargo.", MessageType.warning) : Return False
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioRegistro(ByVal codigo_cia As Integer) As Boolean
        Try
            me_ConfiguracionInstanciasAdeudos = md_ConfiguracionInstanciasAdeudos.GetConfiguracionInstanciasAdeudos(codigo_cia)

            If me_ConfiguracionInstanciasAdeudos.codigo_cia = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")

            With me_ConfiguracionInstanciasAdeudos                
                Me.cmbArea.SelectedValue = .codigoArea_cco
                Me.cmbCargo.SelectedValue = .codigo_tfu
                Me.cmbConcepto.SelectedValue = .codigo_sco
                Call cmbConcepto_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbCentroCosto.SelectedValue = .codigo_cco
                Me.cmbEstado.SelectedValue = .estado_cia
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            If Session("frmConfiguracionInstanciaAdeudo-codigo_cia") Is Nothing OrElse String.IsNullOrEmpty(Session("frmConfiguracionInstanciaAdeudo-codigo_cia")) Then mt_ShowMessage("El código de configuración no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
