Imports ClsTomarEvaluacion
Imports System.Collections.Generic

Partial Class TomarEvaluacion_frmRegistroIncidentes
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeIncidenciaEvaluacion As e_IncidenciaEvaluacion, odIncidenciaEvaluacion As d_IncidenciaEvaluacion
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
        End If
        mt_LimpiarParametros()
    End Sub

    'LISTA
    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboFiltroCentroCostos()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        Try
            mt_Listar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.ServerClick
        Try
            mt_SeleccionarTab("M")
            mt_CargarForm(0)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells
                cellsRow(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvList.RowCommand
        Try
            Select Case e.CommandName
                Case "Editar"
                    Dim codigo As Integer = e.CommandArgument
                    mt_CargarForm(codigo)
                    mt_SeleccionarTab("M")
                Case "Eliminar"
                    Dim codigo As Integer = e.CommandArgument
                    mt_Eliminar(codigo)
            End Select
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'MANTENIMIENTO
    Protected Sub cmbCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboCentroCostos()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        Try
            mt_CargarComboGrupoAdmision()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick
        Try
            mt_SeleccionarTab("L")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.ServerClick
        Try
            mt_Guardar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    'LISTA
    Private Sub mt_Init()
        divGrvList.Visible = False
        mt_CargarComboFiltroCicloAcademico()
    End Sub

    Private Sub mt_CargarComboFiltroCicloAcademico()
        Try
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCicloAcademico()
            ClsGlobales.mt_LlenarListas(cmbFiltroCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            cmbFiltroCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboFiltroCentroCostos()
        Try
            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            Dim codUsuario As Integer = Request.QueryString("id")
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, codUsuario)
            ClsGlobales.mt_LlenarListas(cmbFiltroCentroCosto, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim codigoCco As Integer = cmbFiltroCentroCosto.SelectedValue
            Dim codigoGru As Integer = -1

            'Validaciones
            If cmbFiltroCicloAcademico.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un ciclo académico")
                Exit Sub
            End If
            If codigoCco = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un centro de costos")
                Exit Sub
            End If

            oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion
            With oeIncidenciaEvaluacion
                .tipoConsulta = "LT"
                .codigoCco = cmbFiltroCentroCosto.SelectedValue
            End With

            Dim dt As Data.DataTable = odIncidenciaEvaluacion.fc_Listar(oeIncidenciaEvaluacion)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'MANTENIMIENTO
    Private Sub mt_CargarForm(ByVal codigo As Integer)
        Try
            hddCod.Value = codigo
            udpParams.Update()

            mt_LimpiarForm()

            If codigo <> 0 Then
                oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion
                With oeIncidenciaEvaluacion
                    .tipoConsulta = "EDIT"
                    .codigoIne = codigo
                End With
                Dim dt As Data.DataTable = odIncidenciaEvaluacion.fc_Listar(oeIncidenciaEvaluacion)
                If dt.Rows.Count > 0 Then
                    cmbCicloAcademico.SelectedValue = dt.Rows(0).Item("codigo_cac") : cmbCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
                    cmbCentroCosto.SelectedValue = dt.Rows(0).Item("codigo_cco") : cmbCentroCosto_SelectedIndexChanged(Nothing, Nothing)
                    cmbGrupoAdmision.SelectedValue = dt.Rows(0).Item("codigo_gru")
                    txtIncidencia.Text = dt.Rows(0).Item("descripcion_ine")
                End If
            End If
            udpMantenimiento.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboCicloAcademico()
        Try
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCicloAcademico()
            ClsGlobales.mt_LlenarListas(cmbCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            cmbCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboCentroCostos()
        Try
            Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
            Dim codUsuario As Integer = Request.QueryString("id")
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, codUsuario)
            ClsGlobales.mt_LlenarListas(cmbCentroCosto, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
            cmbCentroCosto_SelectedIndexChanged(Nothing, Nothing)
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboGrupoAdmision()
        Try
            Dim codigoCco As Integer = cmbCentroCosto.SelectedValue
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarGrupoAdmisionVirtual("", 0, codigoCco, 0)
            ClsGlobales.mt_LlenarListas(cmbGrupoAdmision, dt, "codigo_gru", "nombre", "-- SELECCIONE --")
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarForm()
        Try
            mt_CargarComboCicloAcademico()
            txtIncidencia.Text = ""
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Guardar()
        Try
            If Not fc_ValidarDatosForm() Then
                Exit Sub
            End If

            oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion

            With oeIncidenciaEvaluacion
                .operacion = "I"
                .codigoIne = hddCod.Value
                .codigoGru = cmbGrupoAdmision.SelectedValue
                .descripcionIne = txtIncidencia.Text.Trim
                .codUsuario = Request.QueryString("id")
            End With

            Dim rpta As Dictionary(Of String, String) = odIncidenciaEvaluacion.fc_IUD(oeIncidenciaEvaluacion)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
                mt_SeleccionarTab("L")
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_ValidarDatosForm() As Boolean
        If cmbCicloAcademico.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar un semestre académico")
            Return False
        End If

        If cmbCicloAcademico.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar un centro de costo")
            Return False
        End If

        If cmbGrupoAdmision.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar un grupo de admisión")
            Return False
        End If

        If txtIncidencia.Text.Trim = "" Then
            mt_GenerarToastServidor(0, "Debe ingresar la descripción de la incidencia")
            Return False
        End If

        Return True
    End Function

    Private Sub mt_Eliminar(ByVal codigo As String)
        Try
            oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion

            With oeIncidenciaEvaluacion
                .operacion = "D"
                .codigoIne = codigo
            End With

            Dim rpta As Dictionary(Of String, String) = odIncidenciaEvaluacion.fc_IUD(oeIncidenciaEvaluacion)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'OTROS MÉTODOS
    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""

        'Parámetros para mensajes desde el servidor
        hddMenServMostrar.Value = "false"
        hddMenServRpta.Value = ""
        hddMenServTitulo.Value = ""
        hddMenServMensaje.Value = ""

        udpParams.Update()
    End Sub

    Private Sub mt_SeleccionarTab(ByVal tipo As String)
        Try
            hddTipoVista.Value = tipo
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String)
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarMensajeServidor(ByVal titulo As String, ByVal rpta As Integer, ByVal mensaje As String)
        Try
            hddMenServMostrar.Value = "true"
            hddMenServRpta.Value = rpta
            hddMenServTitulo.Value = titulo
            hddMenServMensaje.Value = mensaje

            udpParams.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
