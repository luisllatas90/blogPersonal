Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class frmConfiguracionEvaluacionesEvento
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeConfigEvalEvento As e_ConfiguracionEvaluacionEvento, odConfigEvalEvento As d_ConfiguracionEvaluacionEvento
    Private oeConfigEvalEventoPeso As e_ConfiguracionEvaluacionEvento_Peso, odConfigEvalEventoPeso As d_ConfiguracionEvaluacionEvento_Peso
    Private oeTipoEvaluacion As e_TipoEvaluacion, odTipoEvaluacion As d_TipoEvaluacion
    Private odGeneral As d_VacantesEvento
    Public cod_user As Integer = 684

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_CargarCicloAcademico()
                mt_CargarCentroCosto()
                mt_CargarProgramaEstudio()
                mt_CargarTipoEvaluacion()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboFiltroCentroCosto()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroCicloAcademico.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Semestre Académico !", MessageType.Warning) : Exit Sub
            If Me.cmbFiltroCentroCostos.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cmbCarreraProfesional.SelectionMode = ListSelectionMode.Multiple 'andy.diaz 27/01/2021
        cmbCarreraProfesional.Enabled = True 'andy.diaz 27/01/2021

        mt_LimpiarControles()
        'mt_CargarDetalle(-1)
        mt_MostrarTabs(1)
        'Me.txtDescripcion.Focus()
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        Dim codigosCpf As String = ""
        For Each _Item As ListItem In Me.cmbCarreraProfesional.Items
            If _Item.Selected AndAlso _Item.Value <> "-1" Then
                If codigosCpf.Length > 0 Then codigosCpf &= ","
                codigosCpf &= _Item.Value
            End If
        Next
        Session("adm_list_cod_cpf") = codigosCpf
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codigo_ceep As Integer = -1, nro_orden_ceep As Integer = -1, codigo_cee As Integer = -1
        Dim cant As Integer = -1
        Dim peso As Decimal = 0, peso_ant As Decimal = 0, peso_tot As Decimal = 0
        Dim dt As New Data.DataTable
        Try
            If Me.cmbCentroCosto.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            If String.IsNullOrEmpty(Session("adm_list_cod_cpf")) Then mt_ShowMessage("¡ Seleccione Programa de Estudio !", MessageType.Warning) : Exit Sub
            If Me.cmbTipoEvaluacion.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Tipo de Evaluación !", MessageType.Warning) : Exit Sub
            If Me.txtCantidad.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Cantidad de Evaluaciones !", MessageType.Warning) : Exit Sub
            oeConfigEvalEvento = New e_ConfiguracionEvaluacionEvento : odConfigEvalEvento = New d_ConfiguracionEvaluacionEvento
            With oeConfigEvalEvento
                ._codigo_cco = Me.cmbCentroCosto.SelectedValue : ._codigo_tev = Me.cmbTipoEvaluacion.SelectedValue
                ._cantidad_cee = Me.txtCantidad.Text.Trim : ._codigo_per = cod_user : ._codigo_cpf = Session("adm_list_cod_cpf")
                ._leInsert = New List(Of e_ConfiguracionEvaluacionEvento_Peso) : ._leEdit = New List(Of e_ConfiguracionEvaluacionEvento_Peso)()
                If ._cantidad_cee > 1 Then
                    For i As Integer = 0 To Me.grvConfigPesos.Rows.Count - 1
                        codigo_ceep = Me.grvConfigPesos.DataKeys(i).Values("codigo_ceep")
                        nro_orden_ceep = Me.grvConfigPesos.DataKeys(i).Values("nro_orden_ceep")
                        peso_ant = Me.grvConfigPesos.DataKeys(i).Values("peso_ceep")
                        Dim txtPeso As TextBox = CType(Me.grvConfigPesos.Rows(i).FindControl("txtPeso"), TextBox)
                        peso = txtPeso.Text
                        peso_tot += peso
                        oeConfigEvalEventoPeso = New e_ConfiguracionEvaluacionEvento_Peso
                        oeConfigEvalEventoPeso._peso_ceep = peso : oeConfigEvalEventoPeso._nro_orden_ceep = nro_orden_ceep : oeConfigEvalEventoPeso._codigo_per = cod_user
                        If codigo_ceep = -1 Then
                            ._leInsert.Add(oeConfigEvalEventoPeso)
                        Else
                            If peso <> peso_ant Then
                                oeConfigEvalEventoPeso._codigo_ceep = codigo_ceep
                                ._leEdit.Add(oeConfigEvalEventoPeso)
                            End If
                        End If
                    Next
                    If peso_tot <> 1 Then
                        mt_ShowMessage("¡ El peso total de las evaluaciones debe ser igual a 1 !", MessageType.Warning) : Exit Sub
                    End If
                End If
            End With
            Dim _refresh As Boolean = False
            If String.IsNullOrEmpty(Session("adm_codigo_cee")) Then
                dt = odConfigEvalEvento.fc_Insertar(oeConfigEvalEvento)
                _refresh = True
            Else
                oeConfigEvalEvento._codigo_cee = Session("adm_codigo_cee")
                oeConfigEvalEvento._codigo_ceep = Session("adm_list_cod_ceep")
                dt = odConfigEvalEvento.fc_Actualizar(oeConfigEvalEvento)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    btnListar_Click(Nothing, Nothing)
                    If _refresh Then
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvConfig_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConfig.RowCommand
        Dim index As Integer = -1, codigo_cee As Integer = -1
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_cee = Me.grvConfig.DataKeys(index).Values("codigo_cee")
                Select Case e.CommandName
                    Case "Editar"
                        cmbCarreraProfesional.SelectionMode = ListSelectionMode.Single 'andy.diaz 27/01/2021
                        cmbCarreraProfesional.Enabled = False 'andy.diaz 27/01/2021

                        mt_LimpiarControles()
                        oeConfigEvalEvento = New e_ConfiguracionEvaluacionEvento : odConfigEvalEvento = New d_ConfiguracionEvaluacionEvento
                        oeConfigEvalEvento._tipoOpe = "1" : oeConfigEvalEvento._codigo_cee = codigo_cee
                        dt = odConfigEvalEvento.fc_Listar(oeConfigEvalEvento)
                        If dt.Rows.Count > 0 Then
                            Me.cmbCentroCosto.SelectedValue = dt.Rows(0).Item(1)
                            Me.cmbTipoEvaluacion.SelectedValue = dt.Rows(0).Item(3)
                            Dim codigosCpf As String = ""
                            For Each _Item As ListItem In Me.cmbCarreraProfesional.Items
                                If _Item.Value = dt.Rows(0).Item(2) Then
                                    _Item.Selected = True
                                    If codigosCpf.Length > 0 Then codigosCpf &= ","
                                    codigosCpf &= _Item.Value
                                    Exit For
                                End If
                            Next
                            Session("adm_list_cod_cpf") = codigosCpf
                            Me.txtCantidad.Text = dt.Rows(0).Item(4)
                            mt_CargarDetalle(codigo_cee)
                            Session("adm_codigo_cee") = codigo_cee
                            mt_MostrarTabs(1)
                            Me.txtCantidad.Focus()
                        End If
                    Case "Eliminar"
                        oeConfigEvalEvento = New e_ConfiguracionEvaluacionEvento : odConfigEvalEvento = New d_ConfiguracionEvaluacionEvento
                        oeConfigEvalEvento._codigo_cee = codigo_cee : oeConfigEvalEvento._codigo_per = cod_user
                        dt = odConfigEvalEvento.fc_Eliminar(oeConfigEvalEvento)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_MostrarTabs(0)
                                btnListar_Click(Nothing, Nothing)
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub txtCantidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCantidad.TextChanged
        Dim dt As New Data.DataTable
        Dim codigosCee As String = ""
        Dim cant As Integer = -1, cant_ant As Integer = -1
        cant_ant = Session("adm_cant_eva")
        If IsNumeric(Me.txtCantidad.Text.Trim) Then
            cant = Me.txtCantidad.Text.Trim
            If cant > 0 Then
                dt = CType(Session("adm_CEE_Peso"), System.Data.DataTable)
                If cant > cant_ant Then
                    For i As Integer = 1 To (cant - cant_ant)
                        dt.Rows.Add(fc_GenerarFila(dt))
                    Next
                Else
                    For j As Integer = cant + 1 To dt.Rows.Count
                        If codigosCee.Length > 0 Then codigosCee &= ","
                        codigosCee &= dt.Rows(dt.Rows.Count - 1).Item(0)
                        dt.Rows.RemoveAt(dt.Rows.Count - 1)
                    Next
                End If
                Me.grvConfigPesos.DataSource = dt
                Me.grvConfigPesos.DataBind()
                Session("adm_CEE_Peso") = dt
                Session("adm_cant_eva") = cant
                Session("adm_list_cod_ceep") = codigosCee
            Else
                Me.txtCantidad.Text = cant_ant
                mt_ShowMessage("¡ Ingrese una Cantidad mayor a cero !", MessageType.Warning)
            End If
        End If
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Dim dvAlerta As New Literal
        Dim cssclss As String
        Select Case type
            Case MessageType.Success
                cssclss = "alert-success"
            Case MessageType.Error
                cssclss = "alert-danger"
            Case MessageType.Warning
                cssclss = "alert-warning"
            Case Else
                cssclss = "alert-info"
        End Select
        dvAlerta.Text = "<div id='alert_div' style='margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;' class='alert " + cssclss + "'>"
        dvAlerta.Text += "  <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>"
        dvAlerta.Text += "  <span>" + Message + "</span>"
        dvAlerta.Text += "</div>"
        Me.divMensaje.Controls.Add(dvAlerta)
    End Sub

    Private Sub mt_MostrarTabs(ByVal idTab As Integer)
        Select Case idTab
            Case 0
                Me.navlistatab.Attributes("class") = "nav-item nav-link active"
                Me.navlistatab.Attributes("aria-selected") = "true"
                Me.navlista.Attributes("class") = "tab-pane fade show active"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link"
                Me.navmantenimientotab.Attributes("aria-selected") = "false"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade"
            Case 1
                Me.navlistatab.Attributes("class") = "nav-item nav-link"
                Me.navlistatab.Attributes("aria-selected") = "false"
                Me.navlista.Attributes("class") = "tab-pane fade"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link active"
                Me.navmantenimientotab.Attributes("aria-selected") = "true"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade show active"
        End Select
    End Sub

    Private Sub mt_CargarCicloAcademico()
        'Try
        Dim dt As Data.DataTable = fc_ListarCicloAcademico()
        mt_LlenarListas(cmbFiltroCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
        cmbFiltroCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarComboFiltroCentroCosto()
        'Try
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbFiltroCentroCostos, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarCentroCosto()
        'Dim dt As New Data.DataTable
        'Dim dv As Data.DataView
        odGeneral = New d_VacantesEvento
        'dt = odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user)
        'dv = New Data.DataView(dt, "", "descripcion_cco", Data.DataViewRowState.CurrentRows)
        'dt = dv.ToTable
        'mt_LlenarListas(Me.cmbFiltroCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbCentroCosto, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        'mt_LlenarListas(Me.cmbCentroCosto, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarProgramaEstudio()
        odGeneral = New d_VacantesEvento
        mt_LlenarListas(Me.cmbCarreraProfesional, odGeneral.fc_ListarCarreraProfesional, "codigo_cpf", "nombre_cpf", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarTipoEvaluacion()
        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
        mt_LlenarListas(Me.cmbTipoEvaluacion, odTipoEvaluacion.fc_Listar(oeTipoEvaluacion), "codigo_tev", "nombre_tev", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeConfigEvalEvento = New e_ConfiguracionEvaluacionEvento : odConfigEvalEvento = New d_ConfiguracionEvaluacionEvento
        With oeConfigEvalEvento
            ._tipoOpe = "2" : ._codigo_cco = Me.cmbFiltroCentroCostos.SelectedValue
        End With
        dt = odConfigEvalEvento.fc_Listar(oeConfigEvalEvento)
        Me.grvConfig.DataSource = dt
        Me.grvConfig.DataBind()
        If Me.grvConfig.Rows.Count > 0 Then mt_AgruparFilas(Me.grvConfig.Rows, 0, 2)
    End Sub

    Private Sub mt_LimpiarControles()
        Session("adm_codigo_cee") = ""
        Session("adm_list_cod_cpf") = ""
        Session("adm_CEE_Peso") = Nothing
        Session("adm_cant_eva") = 1
        Session("adm_list_cod_ceep") = ""
        Me.cmbCentroCosto.SelectedValue = "-1"
        Me.cmbTipoEvaluacion.SelectedValue = "-1"
        Me.txtCantidad.Text = "1"
        For Each _Item As ListItem In Me.cmbCarreraProfesional.Items
            If _Item.Selected Then
                _Item.Selected = False
            End If
        Next
        mt_CargarDetalle(-1)
    End Sub

    Private Sub mt_CargarDetalle(ByVal codigo_cee As Integer)
        Dim dt As New Data.DataTable
        oeConfigEvalEventoPeso = New e_ConfiguracionEvaluacionEvento_Peso : odConfigEvalEventoPeso = New d_ConfiguracionEvaluacionEvento_Peso
        oeConfigEvalEventoPeso._tipoOPe = "1" : oeConfigEvalEventoPeso._codigo_cee = codigo_cee
        dt = odConfigEvalEventoPeso.fc_Listar(oeConfigEvalEventoPeso)
        If dt.Rows.Count = 0 Then
            dt.Rows.Add(fc_GenerarFila(dt))
        End If
        Session("adm_CEE_Peso") = dt
        Session("adm_cant_eva") = dt.Rows.Count
        Me.grvConfigPesos.DataSource = dt
        Me.grvConfigPesos.DataBind()
        'If Me.grvConfigPreguntas.Rows.Count > 0 Then mt_AgruparFilas(Me.grvConfigPreguntas.Rows, 0, 2)
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_GenerarFila(ByVal dt As Data.DataTable) As System.Data.DataRow
        Dim row As System.Data.DataRow
        row = dt.NewRow
        row("codigo_ceep") = -1
        row("codigo_cee") = -1
        row("nro_orden_ceep") = dt.Rows.Count + 1
        row("peso_ceep") = 1.0
        row("estado_ceep") = 1
        row("descripcion_ceep") = "Evaluación N° " & (dt.Rows.Count + 1)
        Return row
    End Function

#End Region

End Class
