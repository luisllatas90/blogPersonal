Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsGenerarEvaluacion
Imports ClsBancoPreguntas
Imports System.Collections.Generic

Partial Class frmEvaluaciones
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oeEvalDetalle As e_EvaluacionDetalle, odEvalDetalle As d_EvaluacionDetalle
    Private oePreguntas As e_PreguntaEvaluacion, odPreguntas As d_PreguntaEvaluacion
    Private oeAlternativas As e_AlternativaEvaluacion, odAlternativas As d_AlternativaEvaluacion
    Private oeTipoEvaluacion As e_TipoEvaluacion, odTipoEvaluacion As d_TipoEvaluacion
    Private oeTipoEvalIndicador As e_TipoEvaluacion_Indicador, odTipoEvalIndicador As d_TipoEvaluacion_Indicador
    Private oeCompetencia As e_CompetenciaAprendizaje, odCompetencia As d_CompetenciaAprendizaje
    Private odGeneral As d_VacantesEvento
    Private mo_RepoAdmision As New ClsAdmision

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Public cod_user As Integer = 684
    Private idTabla As Integer = 36

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
                mt_CargarTipoEvaluacion()
                Session("delArchivo") = False
                Me.btnfuEvaluacion.Attributes.Add("onClick", "document.getElementById('" + Me.fuEvaluacion.ClientID + "').click();")
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
            If Me.cmbFiltroCentroCosto.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        'mt_CargarDetalle(-1)
        mt_MostrarTabs(1)
        mt_MostrarTabs2(0)
        Me.txtNombre.Focus()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Session("adm_dt_Preguntas") = Nothing
        Me.spnFile.InnerText = ""
        Session("delArchivo") = False
        Me.btnfuEvaluacion.Disabled = False
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt, dtDet As New Data.DataTable, dtEvalDet As Data.DataTable
        Dim existeCompetencia As Boolean, existeDetalle As Boolean
        Try
            If Session("opc") = False Then
                If Me.cmbCentroCosto.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
                If Me.cmbTipoEvaluacion.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Tipo de Evaluacion !", MessageType.Warning) : Exit Sub
                If Me.txtNombre.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Nombre de Evaluación !", MessageType.Warning) : Exit Sub
                oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
                oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle

                dtDet = CType(Session("adm_dtIndPrv"), Data.DataTable)

                'Verifico si hay algun detalle que ya no se encuentra en la grilla, en ese caso elimino
                If Not String.IsNullOrEmpty(Session("adm_codigo_evl")) Then
                    dtEvalDet = CType(Session("adm_dtEvaluacionDetalle"), Data.DataTable)
                    For Each dtOrigDet As Data.DataRow In dtEvalDet.Rows
                        existeCompetencia = False : existeDetalle = False
                        For Each ind As Data.DataRow In dtDet.Rows
                            If dtOrigDet.Item("codigo_com") = ind.Item("codigo_com") Then
                                existeCompetencia = True
                            End If
                            If dtOrigDet.Item("codigo_evd") = ind.Item("codigo_evd") Then
                                existeDetalle = True
                                Exit For
                            End If
                        Next
                        If existeCompetencia AndAlso Not existeDetalle Then
                            oeEvalDetalle._codigo_evd = dtOrigDet.Item("codigo_evd")
                            oeEvalDetalle._codigo_per = cod_user
                            odEvalDetalle.fc_Eliminar(oeEvalDetalle)
                        End If
                    Next
                End If

                With oeEvaluacion
                    ._codigo_cco = Me.cmbCentroCosto.SelectedValue : ._codigo_tev = Me.cmbTipoEvaluacion.SelectedValue
                    ._nombre_evl = Me.txtNombre.Text.Trim : ._codigo_per = cod_user
                    ._virtual_evl = Me.chkvirtual.checked
                    ._leInsert = New List(Of e_EvaluacionDetalle) : ._leEdit = New List(Of e_EvaluacionDetalle)
                    ._leDelete = New List(Of e_EvaluacionDetalle)
                    If dtDet.Rows.Count > 0 Then
                        For Each fila As Data.DataRow In dtDet.Rows
                            oeEvalDetalle = New e_EvaluacionDetalle
                            If fila.Item("codigo_evd") = "-1" Then
                                If fila.Item("estado") = "1" Then
                                    oeEvalDetalle._codigo_prv = fila.Item("codigo_prv")
                                    oeEvalDetalle._orden_evd = fila.Item("orden_evd")
                                    oeEvalDetalle._codigo_per = cod_user
                                    ._leInsert.Add(oeEvalDetalle)
                                End If
                            Else
                                If fila.Item("estado") = "2" Then
                                    oeEvalDetalle._codigo_evd = fila.Item("codigo_evd")
                                    oeEvalDetalle._codigo_prv = fila.Item("codigo_prv")
                                    oeEvalDetalle._orden_evd = fila.Item("orden_evd")
                                    oeEvalDetalle._codigo_per = cod_user
                                    ._leEdit.Add(oeEvalDetalle)
                                Else
                                    If fila.Item("estado") = "0" Then
                                        oeEvalDetalle._codigo_evd = fila.Item("codigo_evd")
                                        oeEvalDetalle._codigo_per = cod_user
                                        ._leDelete.Add(oeEvalDetalle)
                                    End If
                                End If
                            End If
                        Next
                    End If
                End With
                Dim _refresh As Boolean = False
                If String.IsNullOrEmpty(Session("adm_codigo_evl")) Then
                    dt = odEvaluacion.fc_Insertar(oeEvaluacion)
                    _refresh = True
                Else
                    oeEvaluacion._codigo_evl = Session("adm_codigo_evl")
                    dt = odEvaluacion.fc_Actualizar(oeEvaluacion)
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
            Else
                Dim xmlTest As System.Xml.XmlDocument = New System.Xml.XmlDocument
                Dim xmlNode As System.Xml.XmlNodeList
                Dim xmlResponse As String = ""
                If Me.fuEvaluacion.HasFile AndAlso Not Session("delArchivo") Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        xmlTest.LoadXml(fc_SubirArchivo(idTabla, Session("adm_codigo_evl"), Archivos(i)))
                    Next
                    xmlNode = xmlTest.GetElementsByTagName("Message")
                    xmlResponse = xmlNode(0).InnerXml
                    If xmlResponse = "Registro procesado correctamente" Then
                        Dim dta As New Data.DataTable
                        Dim ls_RutaArchivo As String = ""
                        Dim ls_NombreFinal As String = ""
                        Dim ln_IdArchivoCompartido As Integer
                        Dim ls_Extension As String = ""
                        dta = mo_RepoAdmision.ObtenerUltimoIDArchivoCompartido(idTabla, Session("adm_codigo_evl"), 0)
                        ls_RutaArchivo = dta.Rows(0).Item("ruta").ToString
                        ls_NombreFinal = dta.Rows(0).Item("NombreArchivo").ToString
                        ln_IdArchivoCompartido = dta.Rows(0).Item("idarchivo")
                        ls_Extension = dta.Rows(0).Item("Extension")
                        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
                        oeEvalDetalle._codigo_evl = Session("adm_codigo_evl")
                        oeEvalDetalle._codigo_per = cod_user
                        oeEvalDetalle._idArchivo = ln_IdArchivoCompartido
                        oeEvalDetalle._ruta = ls_RutaArchivo
                        Dim lo_Resultado As New Dictionary(Of String, String)
                        lo_Resultado = odEvalDetalle.fc_CargarExcelPreguntas(oeEvalDetalle)
                        If lo_Resultado.Item("rpta") <> "1" Then
                            'mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Error)
                            mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Error)
                        Else
                            mt_MostrarTabs(0)
                            'mt_CargarRespuestas()
                            'mt_MostrarTabs2(1)
                            btnListar_Click(Nothing, Nothing)
                            mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Success)
                        End If
                        'mt_MostrarTabs(0)
                        'btnListar_Click(Nothing, Nothing)
                        'mt_ShowMessage("Se subió correctamente el archivo", MessageType.Success)
                    Else
                        mt_ShowMessage("Ocurrio un error en el proceso", MessageType.Error)
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvEvaluaciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvEvaluaciones.RowCommand
        Dim index As Integer = -1, codigo_evl As Integer = -1, codigo_cco As Integer = -1, codigo_tev As Integer = -1
        Dim virtual_evl As Boolean = False '20201124-ENevado
        Dim nombre_evl As String = ""
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_evl = Me.grvEvaluaciones.DataKeys(index).Values("codigo_evl")
                codigo_cco = Me.grvEvaluaciones.DataKeys(index).Values("codigo_cco")
                codigo_tev = Me.grvEvaluaciones.DataKeys(index).Values("codigo_tev")
                nombre_evl = Me.grvEvaluaciones.DataKeys(index).Values("nombre_evl")
                virtual_evl = Me.grvEvaluaciones.DataKeys(index).Values("virtual_evl") '20201124-ENevado
                Select Case e.CommandName
                    Case "Editar"
                        Session("opc") = False
                        mt_LimpiarControles()
                        Session("adm_codigo_evl") = codigo_evl
                        Session("adm_virtual_evl") = virtual_evl '20210203-ENevado

                        'Guardo en sesión el detalle de la evaluación
                        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
                        oeEvalDetalle._tipoOpe = "4"
                        oeEvalDetalle._codigo_evl = codigo_evl
                        Session("adm_dtEvaluacionDetalle") = odEvalDetalle.fc_Listar(oeEvalDetalle)

                        mt_CargarCompetencias(codigo_tev)
                        Me.cmbCentroCosto.SelectedValue = codigo_cco
                        Me.chkVirtual.checked = virtual_evl '20201124-ENevado
                        Me.cmbTipoEvaluacion.SelectedValue = codigo_tev
                        Me.txtNombre.Text = nombre_evl
                        mt_MostrarTabs(1)
                        mt_MostrarTabs2(0)
                        Me.txtNombre.Focus()
                    Case "Eliminar"
                        Session("opc") = False
                        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
                        oeEvaluacion._codigo_evl = codigo_evl : oeEvaluacion._codigo_per = cod_user
                        dt = odEvaluacion.fc_Eliminar(oeEvaluacion)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_MostrarTabs(0)
                                btnListar_Click(Nothing, Nothing)
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            End If
                        End If
                    Case "Importar"
                        Session("opc") = True
                        Session("adm_codigo_evl") = codigo_evl
                        mt_MostrarTabs(1)
                        mt_MostrarTabs2(2)
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbCompetencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompetencia.SelectedIndexChanged
        Dim dt, dtIndPrv, dtAux As New Data.DataTable
        Dim dv, dvIndProv As Data.DataView
        If Me.cmbCompetencia.SelectedValue <> "-1" Then
            dt = CType(Session("adm_dtIndicador"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(dt, "codigo_com = " & Me.cmbCompetencia.SelectedValue, "nombre_ind", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                dtIndPrv = CType(Session("adm_dtIndPrv"), Data.DataTable)
                For Each fil As Data.DataRow In dt.Rows
                    If fil.Item("codigo_evl") <> "-1" Then
                        dvIndProv = New Data.DataView(dtIndPrv, "codigo_prv = " & fil.Item("codigo_prv"), "", Data.DataViewRowState.CurrentRows)
                        dtAux = dvIndProv.ToTable
                        If dtAux.Rows.Count = 0 Then
                            dtIndPrv.Rows.Add(fil.Item("codigo_evd"), fil.Item("nro_item"), fil.Item("codigo_prv"), fil.Item("codigo_com"), "1")
                        End If
                    End If
                Next
                Session("adm_dtIndPrv") = dtIndPrv
                Me.grvConfig.DataSource = dt
                Me.grvConfig.DataBind()
                If Me.grvConfig.Rows.Count > 0 Then mt_AgruparFilas(Me.grvConfig.Rows, 0, 2)
            End If
        Else
            Me.grvConfig.DataSource = Nothing
            Me.grvConfig.DataBind()
        End If
        mt_MostrarTabs2(1)
    End Sub

    Protected Sub grvConfig_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvConfig.RowCommand
        Dim index As Integer = -1, codigo_ind As Integer = -1, nro_item As Integer = -1, codigo_prv As Integer = -1, codigo_com As Integer = -1
        Dim dtPrg, dtAlt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_ind = Me.grvConfig.DataKeys(index).Values("codigo_ind")
                nro_item = Me.grvConfig.DataKeys(index).Values("nro_item")
                codigo_prv = Me.grvConfig.DataKeys(index).Values("codigo_prv")
                codigo_com = Me.grvConfig.DataKeys(index).Values("codigo_com")
                Select Case e.CommandName
                    Case "Seleccionar"
                        mt_Seleccionar(codigo_ind, nro_item, codigo_prv, codigo_com)
                    Case "Mostrar"
                        oePreguntas = New e_PreguntaEvaluacion : odPreguntas = New d_PreguntaEvaluacion
                        With oePreguntas
                            .tipoConsulta = "GEN" : .codigoInd = codigo_ind : .codigoNcp = 0 : .cantidadPrv = 0
                        End With
                        dtPrg = odPreguntas.fc_Listar(oePreguntas)
                        Session("adm_dtPreguntas") = dtPrg
                        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
                        With oeAlternativas
                            .tipoConsulta = "IND" : .codigoPrv = codigo_ind
                        End With
                        dtAlt = odAlternativas.fc_Listar(oeAlternativas)
                        Session("adm_dtAlternativas") = dtAlt
                        mt_MostrarPregunta(codigo_prv)
                        ClientScript.RegisterStartupScript(Me.GetType(), "Pop", "<script>openModal('pregunta');</script>")
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'Protected Sub rbtTipoPregunta1_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTipoPregunta1.ServerChange
    '    If Me.rbtTipoPregunta1.Checked Then
    '        mt_CargarPreguntas("U")
    '        Me.udpPregunta.Update()
    '    End If

    'End Sub

    'Protected Sub rbtTipoPregunta2_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTipoPregunta2.ServerChange
    '    If Me.rbtTipoPregunta2.Checked Then
    '        mt_CargarPreguntas("A")
    '        Me.udpPregunta.Update()
    '    End If

    'End Sub

    Protected Sub btnAltGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dtIndPrv, dt, dtAux As New Data.DataTable
        Dim dv As Data.DataView
        Dim insert As Boolean = False
        Dim nro_item As Integer = -1
        Dim codigo_com As Integer = -1
        Try
            If Me.hfcodigoprv.Value = "" Then
                mt_Seleccionar(Session("adm_codigo_ind"), Session("adm_nro_item"), Session("adm_codigo_prv"), Session("adm_codigo_com"))
            Else
                dtIndPrv = CType(Session("adm_dtIndPrv"), Data.DataTable)
                nro_item = Session("adm_nro_item")
                codigo_com = Session("adm_codigo_com")
                If dtIndPrv.Rows.Count > 0 Then
                    insert = True
                    For Each fila As Data.DataRow In dtIndPrv.Rows
                        If fila("orden_evd") = nro_item Then
                            If fila("codigo_evd") = -1 Then
                                fila("codigo_prv") = Me.hfcodigoprv.Value
                                fila("codigo_com") = codigo_com
                                fila("estado") = "1"
                                insert = False
                            Else
                                fila("estado") = "0"
                            End If
                        End If
                    Next
                Else
                    insert = True
                End If
                If insert Then
                    dtIndPrv.Rows.Add("-1", Session("adm_nro_item"), Me.hfcodigoprv.Value, codigo_com, "1")
                End If
                dt = CType(Session("adm_dtIndicador"), Data.DataTable)
                If dt.Rows.Count > 0 Then
                    For Each fil As Data.DataRow In dt.Rows
                        If fil.Item("nro_item") = Session("adm_nro_item") Then
                            fil.Item("codigo_prv") = Me.hfcodigoprv.Value
                            fil.Item("identificador_prv") = Me.hfidentificadorprv.Value
                            fil.Item("nombre_ncp") = Me.hfnivel.Value
                            Exit For
                        End If
                    Next
                    dv = New Data.DataView(dt, "codigo_com = " & Me.cmbCompetencia.SelectedValue, "nombre_ind", Data.DataViewRowState.CurrentRows)
                    dtAux = dv.ToTable
                End If
                Session("adm_dtIndPrv") = dtIndPrv
                Session("adm_nro_item") = ""
                Session("adm_dtIndicador") = dt
                Me.grvConfig.DataSource = dtAux
                Me.grvConfig.DataBind()
                If Me.grvConfig.Rows.Count > 0 Then mt_AgruparFilas(Me.grvConfig.Rows, 0, 2)
                Me.hfcodigoprv.Value = ""
                Me.hfidentificadorprv.Value = ""
                Me.hfnivel.Value = ""
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
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

    Private Sub mt_MostrarTabs2(ByVal idTab As Integer)
        Select Case idTab
            Case 0
                mt_ActivarTab(Me.navdatosgeneralestab, Me.navdatosgenerales, True)
                mt_ActivarTab(Me.navpreguntastab, Me.navpreguntas, False)
                mt_ActivarTab(Me.navimportartab, Me.navimportar, False)
            Case 1
                mt_ActivarTab(Me.navdatosgeneralestab, Me.navdatosgenerales, False)
                mt_ActivarTab(Me.navpreguntastab, Me.navpreguntas, True)
                mt_ActivarTab(Me.navimportartab, Me.navimportar, False)
            Case 2
                mt_ActivarTab(Me.navdatosgeneralestab, Me.navdatosgenerales, False)
                mt_ActivarTab(Me.navpreguntastab, Me.navpreguntas, False)
                mt_ActivarTab(Me.navimportartab, Me.navimportar, True)
        End Select
    End Sub

    Private Sub mt_ActivarTab(ByVal tabList As HtmlAnchor, ByVal tab As HtmlGenericControl, ByVal activar As Boolean)
        If activar Then
            tabList.Attributes("class") = "nav-item nav-link active"
            tabList.Attributes("aria-selected") = "true"
            tab.Attributes("class") = "tab-pane fade show active"
        Else
            tabList.Attributes("class") = "nav-item nav-link "
            tabList.Attributes("aria-selected") = "false"
            tab.Attributes("class") = "tab-pane fade"
        End If
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
        mt_LlenarListas(cmbFiltroCentroCosto, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarCentroCosto()
        odGeneral = New d_VacantesEvento
        'mt_LlenarListas(Me.cmbFiltroCentroCosto, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbCentroCosto, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarTipoEvaluacion()
        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
        mt_LlenarListas(Me.cmbTipoEvaluacion, odTipoEvaluacion.fc_Listar(oeTipoEvaluacion), "codigo_tev", "nombre_tev", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        With oeEvaluacion
            ._tipoOpe = "1" : ._codigo_cco = Me.cmbFiltroCentroCosto.SelectedValue
        End With
        dt = odEvaluacion.fc_Listar(oeEvaluacion)
        Me.grvEvaluaciones.DataSource = dt
        Me.grvEvaluaciones.DataBind()
        If Me.grvEvaluaciones.Rows.Count > 0 Then mt_AgruparFilas(Me.grvEvaluaciones.Rows, 0, 2)
    End Sub

    Private Sub mt_LimpiarControles()
        Session("adm_codigo_evl") = ""
        Session("adm_dtIndicador") = Nothing
        Session("adm_dtPreguntas") = Nothing
        Session("adm_dtEvaluacionDetalle") = Nothing
        Session("adm_dtAlternativas") = Nothing
        Session("adm_dtIndPrv") = Nothing
        Session("adm_codigo_ind") = ""
        Session("adm_nro_item") = ""
        mt_CargarCompetencias(-1)
        Me.cmbCentroCosto.SelectedValue = "-1"
        Me.cmbTipoEvaluacion.SelectedValue = "-1"
        Me.txtNombre.Text = ""
        Me.grvConfig.DataSource = Nothing
        Me.grvConfig.DataBind()
        Dim dtIndPrv As New Data.DataTable
        dtIndPrv.Columns.Add("codigo_evd")
        dtIndPrv.Columns.Add("orden_evd")
        dtIndPrv.Columns.Add("codigo_prv")
        dtIndPrv.Columns.Add("codigo_com")
        dtIndPrv.Columns.Add("estado")
        Session("adm_dtIndPrv") = dtIndPrv
    End Sub

    Private Sub mt_CargarCompetencias(ByVal codigo_tev As Integer)
        Dim dt As New Data.DataTable
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        oeCompetencia._tipoOpe = "4" : oeCompetencia._codigo_cmp = codigo_tev
        mt_LlenarListas(Me.cmbCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
        oeEvalDetalle._tipoOpe = iif(Session("adm_virtual_evl"), "3", "1") '20210203-ENevado
        oeEvalDetalle._orden_evd = codigo_tev
        If codigo_tev <> -1 Then oeEvalDetalle._codigo_evl = Session("adm_codigo_evl")
        dt = odEvalDetalle.fc_Listar(oeEvalDetalle)
        Session("adm_dtIndicador") = dt
    End Sub

    Private Sub mt_CargarPreguntas(ByVal tipo As String, ByVal codigo_prv As Integer)
        Dim nro_pregunta As Integer = -1
        Dim dtPrg, dtAlt, dtAltAux As New Data.DataTable
        Dim dvPrg, dvAlt As Data.DataView
        Dim divOl As New Literal
        dtPrg = CType(Session("adm_dtPreguntas"), Data.DataTable)
        If dtPrg.Rows.Count > 0 Then
            dvPrg = New Data.DataView(dtPrg, "tipo_prv = '" & tipo & "'", "", Data.DataViewRowState.CurrentRows)
            dtPrg = dvPrg.ToTable
            If dtPrg.Rows.Count > 0 Then
                nro_pregunta = 0
                divOl.Text += "<div class='panel-group' id='accordion'>"
                For Each row As System.Data.DataRow In dtPrg.Rows
                    nro_pregunta += 1
                    divOl.Text += " <div class='panel panel-default'>"
                    divOl.Text += "     <div class='panel-heading'>"
                    divOl.Text += "         <h4 class='panel-title'>"
                    divOl.Text += "             <div class='custom-control custom-radio custom-control-inline'>"
                    If codigo_prv <> -1 And codigo_prv = row.Item("codigo_prv") Then
                        divOl.Text += "             <input type='radio' id='rbtPregunta" & nro_pregunta & _
                                                    "' name='rbtPregunta' class='custom-control-input radio-prv' data-codigo-prv=" & _
                                                    row.Item("codigo_prv") & " data-ide-prv='" & row.Item("identificador_prv") & "' " & _
                                                    "data-ncp-prv='" & row.Item("nombre_ncp") & "' checked>"
                    Else
                        divOl.Text += "             <input type='radio' id='rbtPregunta" & nro_pregunta & _
                                                    "' name='rbtPregunta' class='custom-control-input radio-prv' data-codigo-prv=" & _
                                                    row.Item("codigo_prv") & " data-ide-prv='" & row.Item("identificador_prv") & "' " & _
                                                    "data-ncp-prv='" & row.Item("nombre_ncp") & "'>"
                    End If
                    divOl.Text += "                 <label class='custom-control-label form-control-sm' for='rbtPregunta" & nro_pregunta & "'>"
                    divOl.Text += "                     <a id='link" & nro_pregunta & "' data-toggle='collapse' data-parent='#accordion' href='#collapse" & nro_pregunta & "'>"
                    divOl.Text += "                         Código Pregunta: " & row.Item("identificador_prv")
                    divOl.Text += "                     </a>"
                    divOl.Text += "                 </label>"
                    divOl.Text += "             </div>"
                    divOl.Text += "         </h4>"
                    divOl.Text += "     </div>"
                    If nro_pregunta = 1 Then
                        divOl.Text += " <div id='collapse" & nro_pregunta & "' class='panel-collapse collapse in'>"
                    Else
                        divOl.Text += " <div id='collapse" & nro_pregunta & "' class='panel-collapse collapse'>"
                    End If
                    divOl.Text += "         <div class='panel-body'>"
                    divOl.Text += "         " & ClsGlobales.fc_DesencriptaTexto64(row.Item("texto_prv"))
                    divOl.Text += "         </div>"

                    dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
                    If dtAlt.Rows.Count > 0 Then
                        dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & row.Item("codigo_prv"), "", Data.DataViewRowState.CurrentRows)
                        dtAltAux = dvAlt.ToTable
                        If dtAltAux.Rows.Count > 0 Then
                            divOl.Text += " <div class='panel-body'>"
                            divOl.Text += "     <ol class='list-group'>"
                            For Each fila As Data.DataRow In dtAltAux.Rows
                                divOl.Text += "     <li class='list-group-item'>"
                                divOl.Text += "     " & ClsGlobales.fc_DesencriptaTexto64(fila.Item("texto_ale"))
                                divOl.Text += "     </li>"
                            Next
                            divOl.Text += "     </ol>"
                            divOl.Text += " </div>"
                        End If
                    End If

                    divOl.Text += "     </div>"
                    divOl.Text += " </div>"
                Next
                divOl.Text += "</div>"
            End If
            Me.divPreguntas.Controls.Add(divOl)
        End If
    End Sub

    Private Sub mt_MostrarPregunta(ByVal codigo_prv As Integer)
        Dim dtPreg, dtAlt As New Data.DataTable
        Dim dvPreg, dvAlt As Data.DataView
        Dim divOl As New Literal
        dtPreg = CType(Session("adm_dtPreguntas"), Data.DataTable)
        If dtPreg.Rows.Count > 0 Then
            dvPreg = New Data.DataView(dtPreg, "codigo_prv = " & codigo_prv, "", Data.DataViewRowState.CurrentRows)
            dtPreg = dvPreg.ToTable
            If dtPreg.Rows.Count > 0 Then
                Dim row As Data.DataRow
                row = dtPreg.Rows(0)
                divOl.Text += " <div class='panel panel-default'>"
                divOl.Text += "     <div class='panel-heading'>"
                divOl.Text += "         <h4 class='panel-title'>"
                divOl.Text += "         " & ClsGlobales.fc_DesencriptaTexto64(row.Item("texto_prv"))
                divOl.Text += "         </h4>"
                divOl.Text += "     </div>"
                dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
                If dtAlt.Rows.Count > 0 Then
                    dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & row.Item("codigo_prv"), "", Data.DataViewRowState.CurrentRows)
                    dtAlt = dvAlt.ToTable
                    If dtAlt.Rows.Count > 0 Then
                        divOl.Text += " <div class='panel-body'>"
                        divOl.Text += "     <ol class='list-group'>"
                        For Each fila As Data.DataRow In dtAlt.Rows
                            divOl.Text += "     <li class='list-group-item'>"
                            divOl.Text += "     " & ClsGlobales.fc_DesencriptaTexto64(fila.Item("texto_ale"))
                            divOl.Text += "     </li>"
                        Next
                        divOl.Text += "     </ol>"
                        divOl.Text += " </div>"
                    End If
                End If
                divOl.Text += " </div>"
                Me.divSelectPregunta.Controls.Add(divOl)
            End If
        End If
    End Sub

    Private Sub mt_Seleccionar(ByVal codigo_ind As Integer, ByVal nro_item As Integer, ByVal codigo_prv As Integer, ByVal codigo_com As Integer)
        Dim dtPrg, dtAlt As New Data.DataTable
        Session("adm_codigo_ind") = codigo_ind
        Session("adm_nro_item") = nro_item
        Session("adm_codigo_prv") = codigo_prv
        Session("adm_codigo_com") = codigo_com
        oePreguntas = New e_PreguntaEvaluacion : odPreguntas = New d_PreguntaEvaluacion
        With oePreguntas
            .tipoConsulta = "GEN" : .codigoInd = codigo_ind : .codigoNcp = 0 : .cantidadPrv = 0
        End With
        dtPrg = odPreguntas.fc_Listar(oePreguntas)
        Session("adm_dtPreguntas") = dtPrg
        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
        With oeAlternativas
            .tipoConsulta = "IND" : .codigoPrv = codigo_ind
        End With
        dtAlt = odAlternativas.fc_Listar(oeAlternativas)
        Session("adm_dtAlternativas") = dtAlt
        If codigo_prv <> -1 Then
            Me.hfcodigoprv.Value = codigo_prv
            Me.txtFiltrPregunta.Value = codigo_prv
        Else
            Me.hfcodigoprv.Value = ""
            Me.hfidentificadorprv.Value = ""
            Me.hfnivel.Value = ""
            Me.txtFiltrPregunta.Value = ""
        End If
        mt_CargarPreguntas("U", codigo_prv)
        ClientScript.RegisterStartupScript(Me.GetType(), "Pop", "<script>openModal('selecccion');</script>")
    End Sub

    'Private Sub mt_CargarPreguntas(ByVal codigo_tev As Integer)
    '    Dim codigo_com As Integer = -1, nro_tab As Integer = -1
    '    Dim divTabs As New Literal
    '    Dim divContents As String = ""
    '    Dim dt As New Data.DataTable
    '    'Session("adm_dt_Preguntas") = dt
    '    oeTipoEvalIndicador = New e_TipoEvaluacion_Indicador : odTipoEvalIndicador = New d_TipoEvaluacion_Indicador
    '    oeTipoEvalIndicador._tipoOpe = "1" : oeTipoEvalIndicador._codigo_tev = codigo_tev
    '    dt = odTipoEvalIndicador.fc_Listar(oeTipoEvalIndicador)
    '    If dt.Rows.Count > 0 Then
    '        divTabs.Text += "<ul>"
    '        nro_tab = 0
    '        For Each row As System.Data.DataRow In dt.Rows
    '            If row.Item("codigo_com") <> codigo_com Then
    '                codigo_com = CInt(row.Item("codigo_com").ToString)
    '                nro_tab += 1
    '                divTabs.Text += "<li id='li" & nro_tab & "' runat='server'>"
    '                divTabs.Text += "   <a href='#step-" & nro_tab & "' data-step='" & nro_tab & "'>"
    '                divTabs.Text += "       <small>" & row.Item("nombre_com").ToString.ToUpper & "</small>"
    '                divTabs.Text += "   </a>"
    '                divTabs.Text += "</li>"
    '                divContents += "<div id='step-" & nro_tab & "' class='tab-pane step-content'>"
    '                divContents += "    <asp:GridView ID='grvConfig" & nro_tab & "' runat='server' AutoGenerateColumns='false' "
    '                divContents += "        CssClass='table table-sm' GridLines='None'>"
    '                divContents += "        <Columns>"
    '                divContents += "            <asp:BoundField DataField='nombre_scom' HeaderText='SUBCOMPETENCIA'></asp:BoundField>"
    '                divContents += "            <asp:BoundField DataField='nombre_ind' HeaderText='INDICADOR' ItemStyle-HorizontalAlign='Center'></asp:BoundField>"
    '                divContents += "        </Columns>"
    '                divContents += "        <HeaderStyle CssClass='thead-dark' />"
    '                divContents += "    </asp:GridView>"
    '                divContents += "</div>"
    '            End If
    '        Next
    '        divTabs.Text += "</ul>"
    '        divTabs.Text += "<div>"
    '        divTabs.Text += divContents
    '        divTabs.Text += "</div>"
    '        Me.smartwizard.Controls.Add(divTabs)
    '        'Me.ulTabs.Controls.Add(divTabs)
    '        'Dim grv As GridView = CType(Me.frmEva.FindControl("grvConfig1"), GridView)
    '        'grv.DataSource = dt
    '        'grv.DataBind()
    '    End If
    'End Sub

#End Region

#Region "Funciones"

    Private Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = "USAT\esaavedra"
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New IO.BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = IO.Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(_Archivo.FileName))
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", "")
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", _Usuario)

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
