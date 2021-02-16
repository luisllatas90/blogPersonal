Imports ClsProcesamientoResultados
Imports ClsGenerarEvaluacion
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic
Imports System.Drawing

Partial Class ProcesamientoResultados_frmRecalculoResultados
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeEvaluacionAlumno As e_EvaluacionAlumno, odEvaluacionAlumno As d_EvaluacionAlumno
    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oeConfiguracionNotaMinima As e_ConfiguracionNotaMinima, odConfiguracionNotaMinima As d_ConfiguracionNotaMinima
    Private oeVacantesEvento As e_VacantesEvento, odVacantesEvento As d_VacantesEvento
    Private oeCierreResultadosAdmision As e_CierreResultadosAdmision, odCierreResultadosAdmision As d_CierreResultadosAdmision
    Private dtKeys As Dictionary(Of String, Object)
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_Init()
            End If
            mt_LimpiarParametros()

            If String.IsNullOrEmpty(Request.QueryString("id").Trim) Then
                mt_GenerarMensajeServidor("Error", -1, "Se ha perdido la sesión de usuario, por favor vuelva a ingresar")
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'LISTA
    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboFiltroCentroCostos()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            Dim codigoCco As Integer = cmbFiltroCentroCosto.SelectedValue
            Dim codigoMin As Integer = cmbFiltroModalidadIngreso.SelectedValue
            Dim codigoCpf As Integer = cmbFiltroCarreraProfesional.SelectedValue
            mt_Listar(codigoCac, codigoCco, codigoMin, codigoCpf, True)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            For Each kv As KeyValuePair(Of String, Object) In dtKeys
                Dim kvAttr As Dictionary(Of String, Object) = kv.Value
                If kvAttr.ContainsKey("visible") Then
                    e.Row.Cells(kvAttr.Item("pos")).Visible = kvAttr.Item("visible")
                End If

                If e.Row.RowType = DataControlRowType.Header Then
                    If kvAttr.ContainsKey("headerText") Then
                        e.Row.Cells(kvAttr.Item("pos")).Text = kvAttr.Item("headerText")
                    End If
                    If kvAttr.ContainsKey("backColor") Then
                        e.Row.Cells(kvAttr.Item("pos")).BackColor = ColorTranslator.FromHtml(kvAttr.Item("backColor"))
                    End If
                End If

                If e.Row.RowType = DataControlRowType.DataRow Then
                    If kvAttr.ContainsKey("alignment") Then
                        e.Row.Cells(kvAttr.Item("pos")).Attributes.CssStyle.Item("text-align") = kvAttr.Item("alignment")
                    End If
                    If kvAttr.ContainsKey("font-weight") Then
                        e.Row.Cells(kvAttr.Item("pos")).Attributes.CssStyle.Item("font-weight") = kvAttr.Item("font-weight")
                    End If
                End If
            Next

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells
                If cmbFiltroCarreraProfesional.SelectedValue = -1 Then
                    cellsRow(0).Text = "--"
                End If

                Dim condicionIngreso As String = e.Row.DataItem("condicion_ingreso_elu")
                Select Case e.Row.DataItem("condicion_ingreso_elu")
                    Case "I"
                        e.Row.Attributes.Item("class") = "table-success"
                    Case "A"
                        e.Row.Attributes.Item("class") = "table-warning"
                End Select
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcesar.Click
        Try
            mt_Procesar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Try
            mt_Confirmar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    'LISTA
    Private Sub mt_Init()
        Try
            divOperaciones.Visible = False
            divGrvList.Visible = False
            divCeroElementos.Visible = False
            mt_CargarComboFiltroCicloAcademico()
            mt_CargarComboFiltroCarreraProfesional()
            mt_CargarComboFiltroModalidadIngreso()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
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
            'cmbFiltroCentroCosto_SelectedIndexChanged(Nothing, Nothing)
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Private Sub mt_CargarComboFiltroEvaluacion()
    '    Try
    '        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion

    '        Dim codigoCco As Integer = cmbFiltroCentroCosto.SelectedValue
    '        Dim codUsuario As Integer = Request.QueryString("id")
    '        With oeEvaluacion
    '            ._tipoOpe = "1"
    '            ._codigo_cco = codigoCco
    '        End With
    '        Dim dt As Data.DataTable = odEvaluacion.fc_Listar(oeEvaluacion)
    '        ClsGlobales.mt_LlenarListas(cmbFiltroEvaluacion, dt, "codigo_evl", "nombre_evl", "-- SELECCIONE --")
    '        udpFiltros.Update()
    '    Catch ex As Exception
    '        mt_GenerarMensajeServidor("Error", -1, ex.Message)
    '    End Try
    'End Sub

    Private Sub mt_CargarComboFiltroCarreraProfesional()
        Try
            ClsGlobales.mt_LlenarListas(cmbFiltroCarreraProfesional, ClsGlobales.fc_ListarCarreraProfesional(), "codigo_cpf", "nombre_cpf", "TODOS")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboFiltroModalidadIngreso()
        Try
            ClsGlobales.mt_LlenarListas(cmbFiltroModalidadIngreso, ClsGlobales.fc_ListarModalidadIngreso(), "codigo_min", "nombre_Min", "-- SELECCIONE --")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar(ByVal codigoCac As Integer, ByVal codigoCco As Integer, ByVal codigoMin As Integer, ByVal codigoCpf As Integer, ByVal confDivOperaciones As Boolean)
        Try
            'Validaciones
            If codigoCac = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un ciclo académico")
                Exit Sub
            End If
            If codigoCco = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un centro de costos")
                Exit Sub
            End If
            'If codigoEvl = -1 Then
            '    mt_GenerarToastServidor(0, "Debe seleccionar una evaluación")
            '    Exit Sub
            'End If
            If codigoMin = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar una modalidad de ingreso")
                Exit Sub
            End If
            If codigoCpf = -1 Then codigoCpf = 0

            hddCac.Value = codigoCac
            hddCco.Value = codigoCco
            hddMin.Value = codigoMin
            hddCpf.Value = codigoCpf

            oeEvaluacionAlumno = New e_EvaluacionAlumno : odEvaluacionAlumno = New d_EvaluacionAlumno
            With oeEvaluacionAlumno
                .tipoConsulta = "RC"
                '.codigoEvl = codigoEvl
                .codigoCco = codigoCco
                .codigoCpf = codigoCpf
                .codigoMin = codigoMin
            End With

            Dim dt As Data.DataTable = odEvaluacionAlumno.fc_Listar(oeEvaluacionAlumno)
            grvList.DataSource = mt_PivotList(dt)
            grvList.DataBind()

            If confDivOperaciones Then
                divOperaciones.Visible = (dt.Rows.Count > 0)
                divProcesarResultados.Visible = (codigoCpf <> 0)
                divAcciones.Visible = (codigoCpf <> 0)

                If codigoCpf = 0 Then
                    divMensaje.InnerHtml = "Si desea volver a procesar los resultados debe seleccionar un programa específico en el filtro de programas de estudio y volver a listar"
                    divMensaje.Attributes.Item("class") = "alert alert-warning"
                Else
                    'Verifico si ya se ha cerrado la lista de resultados
                    oeCierreResultadosAdmision = New e_CierreResultadosAdmision : odCierreResultadosAdmision = New d_CierreResultadosAdmision
                    With oeCierreResultadosAdmision
                        .tipoConsulta = "GEN"
                        .codigoCac = codigoCac
                        .codigoMin = codigoMin
                        .codigoCpf = codigoCpf
                        .codigoCco = codigoCco
                    End With
                    Dim dtCierre As Data.DataTable = odCierreResultadosAdmision.fc_Listar(oeCierreResultadosAdmision)

                    txtNroAccesitarios.Enabled = (dtCierre.Rows.Count = 0)
                    txtNotaMinima.Enabled = (dtCierre.Rows.Count = 0)
                    divAcciones.Visible = (dtCierre.Rows.Count = 0)

                    If dtCierre.Rows.Count > 0 Then
                        divMensaje.InnerHtml = "Los resultados mostrados en esta lista ya han sido confirmados"
                        divMensaje.Attributes.Item("class") = "alert alert-danger"
                    Else
                        Dim nombreCpf As String = ""
                        Dim dtCpf As Data.DataTable = ClsGlobales.fc_ListarCarreraProfesional("CO", codigoCpf)
                        If dtCpf.Rows.Count > 0 Then
                            nombreCpf = dtCpf.Rows(0).Item("nombre_Cpf")
                        End If

                        Dim nombreMin As String = ""
                        Dim dtMin As Data.DataTable = ClsGlobales.fc_ListarModalidadIngreso("CO", codigoMin)
                        If dtMin.Rows.Count > 0 Then
                            nombreMin = dtMin.Rows(0).Item("nombre_Min")
                        End If

                        divMensaje.InnerHtml = "Al hacer clic en <b>Procesar</b> se volverán a calcular los resultados para el programa: <b>" & nombreCpf & "</b> y modalidad: <b>" & nombreMin & "</b>"
                        divMensaje.Attributes.Item("class") = "alert alert-info"
                    End If
                End If



                'If dtCierre.Rows.Count > 0 Then
                '    divMensaje.InnerHtml = "Los resultados mostrados en esta lista ya han sido confirmados"
                '    divMensaje.Attributes.Item("class") = "alert alert-danger"
                'Else
                '    If codigoCpf = 0 Then
                '        divMensaje.InnerHtml = "Si desea volver a procesar los resultados debe seleccionar un programa específico en el filtro de programas de estudio y volver a listar"
                '        divMensaje.Attributes.Item("class") = "alert alert-warning"
                '    Else
                '        Dim nombreCpf As String = ""
                '        Dim dtCpf As Data.DataTable = ClsGlobales.fc_ListarCarreraProfesional("CO", codigoCpf)
                '        If dtCpf.Rows.Count > 0 Then
                '            nombreCpf = dtCpf.Rows(0).Item("nombre_Cpf")
                '        End If

                '        Dim nombreMin As String = ""
                '        Dim dtMin As Data.DataTable = ClsGlobales.fc_ListarModalidadIngreso("CO", codigoMin)
                '        If dtMin.Rows.Count > 0 Then
                '            nombreMin = dtMin.Rows(0).Item("nombre_Min")
                '        End If

                '        divMensaje.InnerHtml = "Al hacer clic en <b>Procesar</b> se volverán a calcular los resultados para el programa: <b>" & nombreCpf & "</b> y modalidad: <b>" & nombreMin & "</b>"
                '        divMensaje.Attributes.Item("class") = "alert alert-info"
                '    End If
                'End If

                'Configuración de nota mínima
                'If codigoCpf = 0 Then
                '    divMensaje.InnerHtml = "Si desea volver a procesar los resultados debe seleccionar un programa específico en el filtro de programas de estudio y volver a listar"
                '    divMensaje.Attributes.Item("class") = "alert alert-warning"
                '    divProcesarResultados.Visible = False
                'Else
                '    Dim nombreCpf As String = ""
                '    Dim dtCpf As Data.DataTable = ClsGlobales.fc_ListarCarreraProfesional("CO", codigoCpf)
                '    If dtCpf.Rows.Count > 0 Then
                '        nombreCpf = dtCpf.Rows(0).Item("nombre_Cpf")
                '    End If

                '    Dim nombreMin As String = ""
                '    Dim dtMin As Data.DataTable = ClsGlobales.fc_ListarModalidadIngreso("CO", codigoMin)
                '    If dtMin.Rows.Count > 0 Then
                '        nombreMin = dtMin.Rows(0).Item("nombre_Min")
                '    End If

                '    divMensaje.InnerHtml = "Al hacer clic en <b>Procesar</b> se volverán a calcular los resultados para el programa: <b>" & nombreCpf & "</b> y modalidad: <b>" & nombreMin & "</b>"
                '    divMensaje.Attributes.Item("class") = "alert alert-info"
                '    divProcesarResultados.Visible = True
                'End If

                oeConfiguracionNotaMinima = New e_ConfiguracionNotaMinima : odConfiguracionNotaMinima = New d_ConfiguracionNotaMinima
                With oeConfiguracionNotaMinima
                    ._tipoConsulta = "GEN"
                    ._codigo_cpf = codigoCpf
                    ._codigo_cco = codigoCco
                End With
                Dim dtNotaMinima As Data.DataTable = odConfiguracionNotaMinima.fc_Listar(oeConfiguracionNotaMinima)
                If dtNotaMinima.Rows.Count > 0 Then
                    txtNotaMinima.Text = dtNotaMinima.Rows(0).Item("nota_min_cnm")
                Else
                    txtNotaMinima.Text = ""
                End If

                oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
                With oeVacantesEvento
                    ._tipoConsulta = "VAC"
                    ._codigo_cac = codigoCac
                    ._codigo_cco = codigoCco
                    ._codigo_cpf = codigoCpf
                    ._codigo_min = codigoMin
                End With
                Dim dtVae As Data.DataTable = odVacantesEvento.fc_Listar(oeVacantesEvento)
                If dtVae.Rows.Count > 0 Then
                    spnNroVacantes.Visible = True
                    spnNroVacantes.InnerText = dtVae.Rows(0).Item("cantidad_vae").ToString & " vacantes disponibles"
                    txtNroAccesitarios.Text = dtVae.Rows(0).Item("cantidad_accesitarios_vae")
                Else
                    spnNroVacantes.Visible = False
                    spnNroVacantes.InnerText = ""
                    txtNroAccesitarios.Text = ""
                End If

                udpOperaciones.Update()
            End If

            divGrvList.Visible = dt.Rows.Count > 0
            divCeroElementos.Visible = dt.Rows.Count = 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function mt_PivotList(ByVal origDt As Data.DataTable) As Data.DataTable
        Try
            dtKeys = New Dictionary(Of String, Object)
            Dim attrs As New Dictionary(Of String, Object)
            attrs.Item("pos") = 0
            attrs.Item("headerText") = "PUESTO"
            attrs.Item("alignment") = "center"
            dtKeys.Item("puesto") = attrs

            'attrs = New Dictionary(Of String, Object)
            'attrs.Item("pos") = 1
            'attrs.Item("headerText") = "TIPO EVALUACIÓN"
            'dtKeys.Item("nombre_tev") = attrs

            'attrs = New Dictionary(Of String, Object)
            'attrs.Item("pos") = 2
            'attrs.Item("headerText") = "EVALUACIÓN"
            'dtKeys.Item("nombre_evl") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 1
            attrs.Item("headerText") = "CÓDIGO"
            attrs.Item("alignment") = "center"
            dtKeys.Item("codigoUniver_Alu") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 2
            attrs.Item("headerText") = "NRO. DOC. IDENTIDAD"
            attrs.Item("alignment") = "center"
            dtKeys.Item("nroDocIdent_Alu") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 3
            attrs.Item("headerText") = "APELLIDOS Y NOMBRES"
            dtKeys.Item("nombreCompleto") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 4
            attrs.Item("headerText") = "PROGRAMA DE ESTUDIOS"
            attrs.Item("alignment") = "center"
            dtKeys.Item("nombre_Cpf") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 5
            attrs.Item("headerText") = "PUNTAJE 1"
            attrs.Item("alignment") = "center"
            'attrs.Item("backColor") = "#0c66c2"
            dtKeys.Item("puntaje1") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 6
            attrs.Item("headerText") = "NOTA 1"
            attrs.Item("alignment") = "center"
            'attrs.Item("backColor") = "#0c66c2"
            dtKeys.Item("nota1") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 7
            attrs.Item("headerText") = "NOTA FINAL"
            attrs.Item("alignment") = "center"
            attrs.Item("font-weight") = "bold"
            'attrs.Item("backColor") = "#218838"
            dtKeys.Item("notaFinal") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 8
            attrs.Item("headerText") = "ESTADO"
            attrs.Item("alignment") = "center"
            dtKeys.Item("estadoNota") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 9
            attrs.Item("visible") = False
            dtKeys.Item("condicion_ingreso_elu") = attrs

            attrs = New Dictionary(Of String, Object)
            attrs.Item("pos") = 10
            attrs.Item("headerText") = "CONDICIÓN"
            attrs.Item("alignment") = "center"
            dtKeys.Item("condicionIngreso") = attrs

            Dim fixedColIndex As Integer = 6

            Dim newDt As New Data.DataTable
            For Each kv As KeyValuePair(Of String, Object) In dtKeys
                If kv.Key.IndexOf("nota") > -1 OrElse kv.Key.IndexOf("puntaje") > -1 Then
                    newDt.Columns.Add(kv.Key, GetType(Decimal))
                Else
                    newDt.Columns.Add(kv.Key, GetType(String))
                End If
            Next

            Dim tempRow As Data.DataRow
            Dim codigoAlu As Integer = 0, indexEvl As Integer = 0
            For Each row As Data.DataRow In origDt.Rows
                If codigoAlu <> row.Item("codigo_alu") Then
                    indexEvl = 1
                    tempRow = newDt.NewRow()

                    For Each kv As KeyValuePair(Of String, Object) In dtKeys
                        If Not (kv.Key.IndexOf("nota") > -1 OrElse kv.Key.IndexOf("puntaje") > -1) Then
                            tempRow.Item(kv.Key) = row.Item(kv.Key)
                        End If
                    Next
                    tempRow.Item("puntaje1") = row.Item("puntaje_elu")
                    tempRow.Item("nota1") = row.Item("nota_elu")
                    tempRow.Item("notaFinal") = row.Item("notaFinal_elu")
                    newDt.Rows.Add(tempRow)
                Else
                    indexEvl += 1
                    Dim keyColPuntaje As String = "puntaje" & indexEvl
                    Dim keyColNota As String = "nota" & indexEvl
                    If Not newDt.Columns.Contains(keyColPuntaje) Then
                        mt_Ordenar(fixedColIndex)
                        fixedColIndex += 1
                        attrs = New Dictionary(Of String, Object)
                        attrs.Item("pos") = fixedColIndex
                        attrs.Item("headerText") = "PUNTAJE " & indexEvl
                        attrs.Item("alignment") = "center"
                        'attrs.Item("backColor") = "#0c66c2"
                        dtKeys.Item(keyColPuntaje) = attrs
                        newDt.Columns.Add(keyColPuntaje, GetType(Decimal))
                    End If
                    If Not newDt.Columns.Contains(keyColNota) Then
                        mt_Ordenar(fixedColIndex)
                        fixedColIndex += 1
                        attrs = New Dictionary(Of String, Object)
                        attrs.Item("pos") = fixedColIndex
                        attrs.Item("headerText") = "NOTA " & indexEvl
                        attrs.Item("alignment") = "center"
                        'attrs.Item("backColor") = "#0c66c2"
                        dtKeys.Item(keyColNota) = attrs
                        newDt.Columns.Add(keyColNota, GetType(Decimal))
                    End If
                    tempRow.Item(keyColPuntaje) = row.Item("puntaje_elu")
                    tempRow.Item(keyColNota) = row.Item("nota_elu")
                End If
                codigoAlu = row.Item("codigo_alu")
            Next

            Dim cantPuntajes As Integer = 0
            For Each kv As KeyValuePair(Of String, Object) In dtKeys
                Dim kvAttr As Dictionary(Of String, Object) = kv.Value
                newDt.Columns(kv.Key).SetOrdinal(kvAttr.Item("pos"))

                If kv.Key.IndexOf("puntaje") > -1 Then
                    cantPuntajes += 1
                End If
            Next
            If cantPuntajes < 2 Then
                With DirectCast(dtKeys.Item("puntaje1"), Dictionary(Of String, Object))
                    .Item("headerText") = "PUNTAJE"
                End With
                With DirectCast(dtKeys.Item("notaFinal"), Dictionary(Of String, Object))
                    .Item("headerText") = "NOTA"
                End With
                With DirectCast(dtKeys.Item("nota1"), Dictionary(Of String, Object))
                    .Item("visible") = False
                End With
            End If

            Return newDt
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
            Throw ex
        End Try
    End Function

    Private Sub mt_Ordenar(ByVal index As Integer)
        For Each kv As KeyValuePair(Of String, Object) In dtKeys
            Dim kvAttr As Dictionary(Of String, Object) = kv.Value
            If kvAttr.Item("pos") > index Then
                kvAttr.Item("pos") += 1
            End If
        Next
    End Sub

    Private Sub mt_Procesar()
        Try
            odEvaluacionAlumno = New d_EvaluacionAlumno
            oeConfiguracionNotaMinima = New e_ConfiguracionNotaMinima : odConfiguracionNotaMinima = New d_ConfiguracionNotaMinima
            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento

            Dim codigoEvl As Integer = 0
            Dim codigoCac As Integer = hddCac.Value
            Dim codigoCco As Integer = hddCco.Value
            Dim codigoCpf As Integer = hddCpf.Value
            Dim codigoMin As Integer = hddMin.Value

            Dim cantidadAccesitarios As Integer
            If Not Integer.TryParse(txtNroAccesitarios.Text.Trim, cantidadAccesitarios) Then
                mt_GenerarToastServidor(0, "El valor de cantidad de accesitarios debe ser un número")
                Exit Sub
            End If

            If cantidadAccesitarios < 0 Then
                mt_GenerarToastServidor(0, "El valor de cantidad de accesitarios debe ser un número mayor o igual a cero")
                Exit Sub
            End If

            Dim notaMin As Decimal
            If Not Decimal.TryParse(txtNotaMinima.Text.Trim, notaMin) Then
                mt_GenerarToastServidor(0, "El valor de nota mínima debe ser un número")
                Exit Sub
            End If

            'CALCULAR NOTAS
            Dim rpta As Dictionary(Of String, String) = odEvaluacionAlumno.fc_CalcularNotasEvaluacion(codigoEvl, _
                                                                                                      codigoCco, _
                                                                                                      codigoMin, _
                                                                                                      codigoCpf, _
                                                                                                      Request.QueryString("id"))
            If rpta.Item("rpta") <> "1" Then
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
                Exit Sub
            End If

            rpta = odEvaluacionAlumno.fc_ProcesarCondicionesIngreso(codigoCco, _
                                                                    codigoCac, _
                                                                    codigoCpf, _
                                                                    codigoMin, _
                                                                    notaMin, _
                                                                    cantidadAccesitarios, _
                                                                    Request.QueryString("id"))
            If rpta.Item("rpta") <> "1" Then
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
                Exit Sub
            End If

            'ACTUALIZAR NOTA MÍNIMA DE INGRESO
            With oeConfiguracionNotaMinima
                ._operacion = "UNM"
                ._codigo_cnm = 0
                ._codigo_cco = codigoCco
                ._codigo_cpf = codigoCpf
                ._nota_min_cnm = notaMin
                ._cod_usuario = Request.QueryString("id")
            End With
            rpta = odConfiguracionNotaMinima.fc_IUD(oeConfiguracionNotaMinima)

            If rpta.Item("rpta") <> "1" Then
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
                Exit Sub
            End If

            'ACTUALIZAR CANTIDAD DE ACCESITARIOS
            Dim codigoVac As Integer = 0
            With oeVacantesEvento
                ._tipoConsulta = "VAC"
                ._codigo_cac = codigoCac
                ._codigo_cco = codigoCco
                ._codigo_cpf = codigoCpf
                ._codigo_min = codigoMin
                ._cod_usuario = Request.QueryString("id")
            End With
            Dim dtVae As Data.DataTable = odVacantesEvento.fc_Listar(oeVacantesEvento)
            If dtVae.Rows.Count > 0 Then
                codigoVac = dtVae.Rows(0).Item("codigo_Vac")
            End If

            With oeVacantesEvento
                ._operacion = "UA"
                ._codigo_vae = 0
                ._codigo_vac = codigoVac
                ._cantidad_accesitarios_vae = cantidadAccesitarios
            End With
            rpta = odVacantesEvento.fc_IUD(oeVacantesEvento)

            If rpta.Item("rpta") = "1" Then
                mt_Listar(hddCac.Value, hddCco.Value, hddMin.Value, hddCpf.Value, True)
            End If

            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Confirmar()
        Try
            Dim codigoCac As Integer = hddCac.Value
            Dim codigoCco As Integer = hddCco.Value
            Dim codigoCpf As Integer = hddCpf.Value
            Dim codigoMin As Integer = hddMin.Value

            If codigoCac = 0 Then
                mt_GenerarToastServidor(0, "No se ha establecido el semestre académico")
                Exit Sub
            End If

            If codigoCac = 0 Then
                mt_GenerarToastServidor(0, "No se ha establecido el centro de costos")
                Exit Sub
            End If

            If codigoCpf = 0 Then
                mt_GenerarToastServidor(0, "No se ha establecido el programa de estudios")
                Exit Sub
            End If

            If codigoMin = 0 Then
                mt_GenerarToastServidor(0, "No se ha establecido la modalidad de ingreso")
                Exit Sub
            End If

            oeCierreResultadosAdmision = New e_CierreResultadosAdmision : odCierreResultadosAdmision = New d_CierreResultadosAdmision
            With oeCierreResultadosAdmision
                .operacion = "I"
                .codigoCac = codigoCac
                .codigoCco = codigoCco
                .codigoCpf = codigoCpf
                .codigoMin = codigoMin
                .codUsuario = Request.QueryString("id")
            End With
            Dim rpta As Dictionary(Of String, String) = odCierreResultadosAdmision.fc_ConfirmarResultadosAdmision(oeCierreResultadosAdmision)

            If rpta.Item("rpta") = "1" Then
                mt_Listar(hddCac.Value, hddCco.Value, hddMin.Value, hddCpf.Value, True)
            End If

            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'OTROS MÉTODOS
    Private Sub mt_MostrarModal(ByVal tipo As String)
        Try
            hddTipoModalAccion.Value = tipo
            hddMostrarModalAccion.Value = "true"
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_OcultarModal(ByVal tipo As String)
        Try
            hddTipoModalAccion.Value = tipo
            hddOcultarModalAccion.Value = "true"
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""

        'Parámetros para mensajes desde el servidor
        hddMenServMostrar.Value = "false"
        hddMenServRpta.Value = ""
        hddMenServTitulo.Value = ""
        hddMenServMensaje.Value = ""

        'Acciones adicionales
        hddTipoModalAccion.Value = ""
        hddMostrarModalAccion.Value = "false"
        hddOcultarModalAccion.Value = "false"

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
