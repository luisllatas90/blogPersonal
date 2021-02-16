Imports System.Xml
Imports System.IO
Imports ExcelDataReader

Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsGenerarEvaluacion
Imports ClsProcesamientoResultados
Imports ClsBancoPreguntas
Imports System.Collections.Generic

Partial Class frmProcesamientoResultados
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oeEvalDetalle As e_EvaluacionDetalle, odEvalDetalle As d_EvaluacionDetalle
    Private oeEvalAlumno As e_EvaluacionAlumno, odEvalAlumno As d_EvaluacionAlumno
    Private oeEvalAluRpta As e_EvaluacionAlumno_Respuesta, odEvalAluRpta As d_EvaluacionAlumno_Respuesta
    Private oeAlternativas As e_AlternativaEvaluacion, odAlternativas As d_AlternativaEvaluacion
    Private oeTipoEvaluacion As e_TipoEvaluacion, odTipoEvaluacion As d_TipoEvaluacion
    Private odGeneral As d_VacantesEvento
    Private mo_RepoAdmision As New ClsAdmision

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Public cod_user As Integer = 684
    Private idTabla As Integer = mo_RepoAdmision.ObtenerVariableGlobal("codigoTablaArchivo")
    Private orden_col As Integer = 3

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
            If Me.cmbFiltroCentroCostos.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvResultados_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim codigo_alu As Integer = -1, codigo_prv As Integer = -1
        Dim correct As Boolean = False, virtual_evl As Boolean = False
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As Data.DataTable
            Dim dv As Data.DataView
            dt = CType(Session("adm_dtPreguntas"), Data.DataTable)
            dv = New Data.DataView(dt, "", "nro_item", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            codigo_alu = Me.grvResultados.DataKeys(e.Row.RowIndex).Values("codigo_alu")
            virtual_evl = Session("adm_virtual_evl")
            For i As Integer = 0 To dt.Rows.Count - 1
                If virtual_evl Then
                    e.Row.Cells(orden_col + i).Text = fc_ObtenerRespuesta(codigo_alu, i)
                    If e.Row.Cells(orden_col + i).Text = "1" Then
                        e.Row.Cells(orden_col + i).BackColor = Drawing.Color.lightgreen
                    Else
                        e.Row.Cells(orden_col + i).BackColor = Drawing.Color.Coral
                    End If
                Else
                    codigo_prv = dt.Rows(i).Item("codigo_prv").ToString
                    e.Row.Cells(orden_col + i).Text = fc_ObtenerRespuesta(codigo_alu, i)
                    correct = fc_ObtenerRptaCorrecta(codigo_prv, fc_ObtenerOrdenAlt(e.Row.Cells(orden_col + i).Text))
                    If Not correct Then
                        e.Row.Cells(orden_col + i).BackColor = Drawing.Color.Coral
                    Else
                        e.Row.Cells(orden_col + i).BackColor = Drawing.Color.lightgreen
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub grvEvaluaciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvEvaluaciones.RowCommand
        Dim index As Integer = -1, codigo_cco As Integer = -1, codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim nombre_evl As String = "", nombre_archivo As String = ""
        Dim virtual_evl As Boolean = False
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_evl = Me.grvEvaluaciones.DataKeys(index).Values("codigo_evl")
                codigo_tev = Me.grvEvaluaciones.DataKeys(index).Values("codigo_tev")
                codigo_cco = Me.grvEvaluaciones.DataKeys(index).Values("codigo_cco")
                nombre_evl = Me.grvEvaluaciones.DataKeys(index).Values("nombre_evl")
                nombre_archivo = Me.grvEvaluaciones.DataKeys(index).Values("NombreArchivo")
                virtual_evl = Me.grvEvaluaciones.DataKeys(index).Values("virtual_evl")
                Select Case e.CommandName
                    Case "Adjuntar"
                        Me.cmbCentroCostos.SelectedValue = codigo_cco
                        Me.cmbTipoEvaluacion.SelectedValue = codigo_tev
                        Me.txtEvaluacion.Text = nombre_evl
                        Me.spnFile.InnerText = nombre_archivo
                        Session("adm_codigo_evl") = codigo_evl
                        Session("adm_codigo_tev") = codigo_tev
                        Session("adm_virtual_evl") = virtual_evl
                        mt_CargarRespuestas()
                        mt_MostrarTabs(1)
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Session("adm_dt_Preguntas") = Nothing
        Me.spnFile.InnerText = ""
        Session("delArchivo") = False
        Me.btnfuEvaluacion.Disabled = False
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim xmlTest As XmlDocument = New XmlDocument
            Dim xmlNode As XmlNodeList
            Dim xmlResponse As String = ""
            'Dim lb_Resultado As Boolean
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
                    oeEvalAlumno = New e_EvaluacionAlumno : odEvalAlumno = New d_EvaluacionAlumno
                    oeEvalAlumno.codigoEvl = Session("adm_codigo_evl")
                    oeEvalAlumno.codigoPerReg = cod_user
                    oeEvalAlumno.idArchivoCompartido = ln_IdArchivoCompartido
                    oeEvalAlumno.ruta = ls_RutaArchivo
                    Dim lo_Resultado As New Dictionary(Of String, String)
                    lo_Resultado = odEvalAlumno.fc_CargarExcelNotas(oeEvalAlumno)
                    If lo_Resultado.Item("rpta") <> "1" Then
                        'mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Error)
                        mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Error)
                    Else
                        'mt_MostrarTabs(0)
                        mt_CargarRespuestas()
                        mt_MostrarTabs2(1)
                        'btnListar_Click(Nothing, Nothing)
                        mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Success)
                    End If
                    'mt_ShowMessage(xmlResponse, MessageType.Success)
                Else
                    mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Success)
                End If
            Else
                mt_MostrarTabs2(1)
            End If

        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        Finally
            Me.spnFile.InnerText = ""
            Session("delArchivo") = False
            Me.btnfuEvaluacion.Disabled = False
        End Try
    End Sub

    Protected Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt, dtRpta As Data.DataTable
        Dim dv As Data.DataView
        Dim cadenaRpta As String = "", respuesta_ear As String = ""
        Dim codigo_elu As Integer = -1, codigo_evd As Integer = -1, codigo_alt As Integer = -1, codigo_prv As Integer = -1
        Dim orden_evd As Integer = -1, orden_ale As Integer = -1, correcta_ear As Integer = -1
        Dim virtual_evl As Boolean = False
        Try
            dt = CType(Session("adm_dtPreguntas"), Data.DataTable)
            virtual_evl = Session("adm_virtual_evl")
            If virtual_evl Then
                dv = New Data.DataView(dt, "codigo_ind<>-1", "nro_item", Data.DataViewRowState.CurrentRows)
            Else
                dv = New Data.DataView(dt, "codigo_prv<>-1", "nro_item", Data.DataViewRowState.CurrentRows)
            End If
            dt = dv.ToTable
            oeEvalAlumno = New e_EvaluacionAlumno : odEvalAlumno = New d_EvaluacionAlumno
            For x As Integer = 0 To Me.grvResultados.Rows.Count - 1
                cadenaRpta = ""
                codigo_elu = Me.grvResultados.DataKeys(x).Values("codigo_elu")
                For y As Integer = 0 To dt.Rows.Count - 1
                    codigo_prv = dt.Rows(y).Item("codigo_prv")
                    codigo_evd = dt.Rows(y).Item("codigo_evd")
                    orden_evd = dt.Rows(y).Item("nro_item")
                    respuesta_ear = Me.grvResultados.Rows(x).Cells(orden_col + orden_evd - 1).Text
                    orden_ale = fc_ObtenerOrdenAlt(respuesta_ear)
                    codigo_alt = fc_ObtenerCodigoAlt(codigo_prv, orden_ale)
                    If virtual_evl Then
                        If Me.grvResultados.Rows(x).Cells(orden_col + orden_evd - 1).Text = "1" Then
                            correcta_ear = 1
                        Else
                            correcta_ear = 0
                        End If
                    Else
                        If fc_ObtenerRptaCorrecta(codigo_prv, orden_ale) Then
                            correcta_ear = 1
                        Else
                            correcta_ear = 0
                        End If
                    End If
                    If cadenaRpta.Length > 0 Then cadenaRpta &= ","
                    cadenaRpta &= "(" & codigo_elu & "," & codigo_evd & "," & codigo_alt & "," & orden_evd & "," & orden_ale & ",'" & respuesta_ear & "'," & correcta_ear & "," & cod_user & ",GETDATE(),1)"
                Next
                oeEvalAluRpta = New e_EvaluacionAlumno_Respuesta
                oeEvalAluRpta.codigoElu = codigo_elu
                oeEvalAluRpta.respuesta_ear = cadenaRpta
                oeEvalAlumno.leRespuesta.Add(oeEvalAluRpta)
            Next
            dtRpta = odEvalAlumno.fc_InsertarMasivo(oeEvalAlumno)
            If dtRpta.Rows.Count > 0 Then
                Dim lo_Resultado As New Dictionary(Of String, String)
                lo_Resultado = odEvalAlumno.fc_ProcesarResultadoEvaluacion(Session("adm_codigo_evl"), cod_user)
                If lo_Resultado.Item("rpta") = "-1" Then
                    mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Error)
                Else
                    mt_MostrarTabs(0)
                    btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Success)
                End If
            Else
                mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Error)
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
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link disabled"
                Me.navmantenimientotab.Attributes("aria-selected") = "false"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade"
            Case 1
                Me.navlistatab.Attributes("class") = "nav-item nav-link disabled"
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
                Me.navdatosgeneralestab.Attributes("class") = "nav-item nav-link active"
                Me.navdatosgeneralestab.Attributes("aria-selected") = "true"
                Me.navdatosgenerales.Attributes("class") = "tab-pane fade show active"
                Me.navrespuestastab.Attributes("class") = "nav-item nav-link"
                Me.navrespuestastab.Attributes("aria-selected") = "false"
                Me.navrespuestas.Attributes("class") = "tab-pane fade"
            Case 1
                Me.navdatosgeneralestab.Attributes("class") = "nav-item nav-link"
                Me.navdatosgeneralestab.Attributes("aria-selected") = "false"
                Me.navdatosgenerales.Attributes("class") = "tab-pane fade"
                Me.navrespuestastab.Attributes("class") = "nav-item nav-link active"
                Me.navrespuestastab.Attributes("aria-selected") = "true"
                Me.navrespuestas.Attributes("class") = "tab-pane fade show active"
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
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbFiltroCentroCostos, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarCentroCosto()
        odGeneral = New d_VacantesEvento
        'mt_LlenarListas(Me.cmbFiltroCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarTipoEvaluacion()
        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
        mt_LlenarListas(Me.cmbTipoEvaluacion, odTipoEvaluacion.fc_Listar(oeTipoEvaluacion), "codigo_tev", "nombre_tev", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        With oeEvaluacion
            ._tipoOpe = "3" : ._codigo_cco = Me.cmbFiltroCentroCostos.SelectedValue
        End With
        dt = odEvaluacion.fc_Listar(oeEvaluacion)
        Me.grvEvaluaciones.DataSource = dt
        Me.grvEvaluaciones.DataBind()
        If Me.grvEvaluaciones.Rows.Count > 0 Then mt_AgruparFilas(Me.grvEvaluaciones.Rows, 0, 2)
    End Sub

    Private Sub mt_CargarRespuestas()
        Dim codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim dtAlu, dtPreg, dtAlt As Data.DataTable
        codigo_evl = Session("adm_codigo_evl")
        codigo_tev = Session("adm_codigo_tev")
        oeEvalAlumno = New e_EvaluacionAlumno : odEvalAlumno = New d_EvaluacionAlumno
        oeEvalAlumno.tipoConsulta = "EV" : oeEvalAlumno.codigoEvl = codigo_evl
        dtAlu = odEvalAlumno.fc_Listar(oeEvalAlumno)
        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
        oeEvalDetalle._tipoOpe = iif(Session("adm_virtual_evl"), "3", "1") '20210203-ENevado
        oeEvalDetalle._orden_evd = codigo_tev
        oeEvalDetalle._codigo_evl = codigo_evl
        dtPreg = odEvalDetalle.fc_Listar(oeEvalDetalle)
        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
        With oeAlternativas
            .tipoConsulta = "EVL" : .codigoPrv = codigo_evl
        End With
        dtAlt = odAlternativas.fc_Listar(oeAlternativas)
        Session("adm_dtAlternativas") = dtAlt
        Session("adm_dtPreguntas") = dtPreg
        Session("adm_dtRespuestas") = dtAlu
        mt_CrearColumns()
        Me.grvResultados.DataSource = dtAlu
        Me.grvResultados.DataBind()
    End Sub

    Private Sub mt_CrearColumns()
        Dim dtCol As Data.DataTable
        Dim dvCol As Data.DataView
        Dim tfield As New TemplateField()
        If Me.grvResultados.Columns.Count > orden_col Then
            'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
            For i As Integer = (orden_col + 1) To Me.grvResultados.Columns.Count
                Me.grvResultados.Columns.RemoveAt(Me.grvResultados.Columns.Count - 1)
            Next
        End If
        dtCol = CType(Session("adm_dtPreguntas"), Data.DataTable)
        dvCol = New Data.DataView(dtCol, "", "nro_item", Data.DataViewRowState.CurrentRows)
        dtCol = dvCol.ToTable
        If dtCol.Rows.Count = 0 Then
            tfield = New TemplateField()
            tfield.HeaderText = ""
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.grvResultados.Columns.Add(tfield)
        Else
            For x As Integer = 0 To dtCol.Rows.Count - 1
                tfield = New TemplateField()
                tfield.HeaderText = dtCol.Rows(x).Item("nro_item").ToString.ToUpper
                'tfield.HeaderText = (x + 1)
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.grvResultados.Columns.Add(tfield)
            Next
        End If
        '' Crear Column para operaciones
        'tfield = New TemplateField()
        'tfield.HeaderText = "OPERACIONES"
        'tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        'Me.grvResultados.Columns.Add(tfield)
    End Sub

    'Private Sub mt_LeerExcel(ByVal filePath As String)
    '    Dim stream As FileStream = File.Open(filePath, FileMode.Open, FileAccess.Read)

    '    '1. Reading from a binary Excel file ('97-2003 format; *.xls)
    '    Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(stream)
    '    '...
    '    '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
    '    Dim excelReader2 As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
    '    '...
    '    '3. DataSet - The result of each spreadsheet will be created in the result.Tables
    '    Dim result As Data.DataSet = excelReader.AsDataSet()
    '    '...
    '    '4. DataSet - Create column names from first row
    '    'excelReader.IsFirstRowAsColumnNames = True
    '    Dim result2 As Data.DataSet = excelReader.AsDataSet()

    '    '5. Data Reader methods
    '    While excelReader.Read()
    '        'excelReader.GetInt32(0);
    '    End While

    '    '6. Free resources (IExcelDataReader is IDisposable)
    '    excelReader.Close()
    'End Sub

    'Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
    '    If totalColumns = 0 Then Return
    '    Dim i As Integer, count As Integer = 1
    '    Dim lst As ArrayList = New ArrayList()
    '    lst.Add(gridViewRows(0))
    '    Dim ctrl As TableCell
    '    ctrl = gridViewRows(0).Cells(startIndex)
    '    For i = 1 To gridViewRows.Count - 1
    '        Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
    '        If ctrl.Text = nextTbCell.Text Then
    '            count += 1
    '            nextTbCell.Visible = False
    '            lst.Add(gridViewRows(i))
    '        Else
    '            If count > 1 Then
    '                ctrl.RowSpan = count
    '                ctrl.VerticalAlign = VerticalAlign.Middle
    '                mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
    '            End If
    '            count = 1
    '            lst.Clear()
    '            ctrl = gridViewRows(i).Cells(startIndex)
    '            lst.Add(gridViewRows(i))
    '        End If
    '    Next
    '    If count > 1 Then
    '        ctrl.RowSpan = count
    '        ctrl.VerticalAlign = VerticalAlign.Middle
    '        mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
    '    End If
    '    count = 1
    '    lst.Clear()
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

    Private Function fc_ObtenerRespuesta(ByVal codigo_alu As Integer, ByVal item As Integer, Optional ByVal virtual As Boolean = False) As String
        Dim rpta As String = "-"
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Dim rpta_aux As String()
        dt = CType(Session("adm_dtRespuestas"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_alu= " & codigo_alu, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                rpta_aux = dt.Rows(0).Item("respuesta_elu").ToString.Split("|")
                If rpta_aux.Length > item Then
                    If virtual Then
                        If IsNumeric(rpta_aux(item)) Then
                            rpta = CInt(rpta_aux(item))
                        Else
                            rpta = rpta_aux(item)
                        End If
                    Else
                        rpta = rpta_aux(item)
                    End If
                End If
            End If
        End If
        Return rpta
    End Function

    Private Function fc_ObtenerOrdenAlt(ByVal respuesta_ear As String) As Integer
        Dim orden As Integer = -1
        Select Case respuesta_ear
            Case "A" : orden = 1
            Case "B" : orden = 2
            Case "C" : orden = 3
            Case "D" : orden = 4
            Case "E" : orden = 5
        End Select
        Return orden
    End Function

    Private Function fc_ObtenerCodigoAlt(ByVal codigo_prv As Integer, ByVal orden_ale As Integer) As Integer
        Dim cod As Integer = -1
        Dim dtAlt As New Data.DataTable
        Dim dvAlt As Data.DataView
        dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
        If dtAlt.Rows.Count > 0 Then
            dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & codigo_prv & " AND orden_ale = " & orden_ale, "", Data.DataViewRowState.CurrentRows)
            dtAlt = dvAlt.ToTable
            If dtAlt.Rows.Count > 0 Then
                cod = dtAlt.Rows(0).Item("codigo_ale")
            End If
        End If
        Return cod
    End Function

    Private Function fc_ObtenerRptaCorrecta(ByVal codigo_prv As Integer, ByVal orden_ale As Integer) As Boolean
        Dim rpta As Boolean = False
        Dim dtAlt As New Data.DataTable
        Dim dvAlt As Data.DataView
        dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
        If dtAlt.Rows.Count > 0 Then
            dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & codigo_prv & " AND orden_ale = " & orden_ale, "", Data.DataViewRowState.CurrentRows)
            dtAlt = dvAlt.ToTable
            If dtAlt.Rows.Count > 0 Then
                rpta = dtAlt.Rows(0).Item("correcta_ale")
            End If
        End If
        Return rpta
    End Function

#End Region


End Class
