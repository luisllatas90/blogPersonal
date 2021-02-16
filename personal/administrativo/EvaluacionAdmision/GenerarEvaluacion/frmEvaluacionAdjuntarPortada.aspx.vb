Imports System
Imports System.Collections.Generic
'Imports System.Xml
Imports System.IO
Imports System.Diagnostics

Imports iTextSharp.text
Imports iTextSharp.text.html
Imports iTextSharp.text.pdf
Imports iTextSharp.text.xml
'Imports iTextSharp.tool.xml
'Imports iTextSharp.text.html.simpleparser


Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsGenerarEvaluacion
Imports ClsBancoPreguntas

Partial Class GenerarEvaluacion_frmEvaluacionAdjuntarPortada
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oePreguntas As e_PreguntaEvaluacion, odPreguntas As d_PreguntaEvaluacion
    Private oeAlternativas As e_AlternativaEvaluacion, odAlternativas As d_AlternativaEvaluacion
    Private odGeneral As d_VacantesEvento

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    Public cod_user As Integer = 684
    Private idTabla As Integer = 35 ' Cambio

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
                'mt_CargarCentroCosto()
                mt_CargarCicloAcademico()
                Session("delArchivo") = False
                Me.btnfuEvaluacion.Attributes.Add("onClick", "document.getElementById('" + Me.fuEvaluacion.ClientID + "').click();")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            'mt_CargarComboFiltroCentroCosto()
            mt_CargarCentroCosto()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroCicloAcademico.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Semestre Académico !", MessageType.Warning) : Exit Sub
            If Me.cmbCco.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvEvaluacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvEvaluacion.RowCommand
        Dim index As Integer = -1, codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim nombre_evl As String = "", nombre_archivo As String = ""
        Dim dt As Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_evl = Me.grvEvaluacion.DataKeys(index).Values("codigo_evl")
                codigo_tev = Me.grvEvaluacion.DataKeys(index).Values("codigo_tev")
                nombre_evl = Me.grvEvaluacion.DataKeys(index).Values("nombre_evl")
                Select Case e.CommandName
                    Case "Adjuntar"
                        Session("adm_codigo_evl") = codigo_evl
                        nombre_archivo = fc_ObtenerNombreArchivo(Session("adm_codigo_evl"), idTabla, "VWCJ2L63GR")
                        If nombre_archivo = "" Then
                            Me.spnFile.InnerText = "No se eligió ningun archivo"
                        Else
                            Me.spnFile.InnerText = nombre_archivo
                        End If
                        mt_MostrarTabs(1)
                    Case "Mostrar"
                        Session("adm_codigo_evl") = codigo_evl
                        oePreguntas = New e_PreguntaEvaluacion : odPreguntas = New d_PreguntaEvaluacion
                        With oePreguntas
                            .tipoConsulta = "EVL" : .codigoInd = codigo_evl : .codigoNcp = 0 : .cantidadPrv = 0
                        End With
                        dt = odPreguntas.fc_Listar(oePreguntas)
                        Session("adm_dtPreguntas") = dt
                        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
                        With oeAlternativas
                            .tipoConsulta = "EVL" : .codigoPrv = codigo_evl
                        End With
                        dt = odAlternativas.fc_Listar(oeAlternativas)
                        Session("adm_dtAlternativas") = dt
                        mt_GenerarEvlPdf(nombre_evl)
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
                    mt_MostrarTabs(0)
                    btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage("Se subió correctamente el archivo", MessageType.Success)
                Else
                    mt_ShowMessage("Ocurrio un error en el proceso", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        Finally
            Me.spnFile.InnerText = ""
            Session("delArchivo") = False
            Me.btnfuEvaluacion.Disabled = False
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

    Private Sub mt_CargarCentroCosto()
        'odGeneral = New d_VacantesEvento
        'mt_LlenarListas(Me.cmbCco, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbCco, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        With oeEvaluacion
            ._tipoOpe = "1" : ._codigo_cco = Me.cmbCco.SelectedValue
        End With
        dt = odEvaluacion.fc_Listar(oeEvaluacion)
        Me.grvEvaluacion.DataSource = dt
        Me.grvEvaluacion.DataBind()
        If Me.grvEvaluacion.Rows.Count > 0 Then mt_AgruparFilas(Me.grvEvaluacion.Rows, 0, 2)
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Private Sub mt_GenerarEvlPdf(ByVal nombre_evl As String)
        Dim documento As New Document(PageSize.A4, 2.0, 2.0, 2.5, 2.5)
        Dim msExamen As New MemoryStream
        Dim msPdf As New MemoryStream
        Dim writer As PdfWriter
        Dim parrafo As New Paragraph
        Dim dtPrg, dtAlt As Data.DataTable
        Dim dvAlt As Data.DataView
        Dim textohtml As String
        Dim nro As Integer = 0
        dtPrg = CType(Session("adm_dtPreguntas"), Data.DataTable)
        If dtPrg.Rows.Count > 0 Then
            writer = PdfWriter.GetInstance(documento, msExamen)
            textohtml = ""
            For Each rowPrg As Data.DataRow In dtPrg.Rows
                nro += 1
                textohtml += nro & ". " & ClsGlobales.fc_DesencriptaTexto64(rowPrg.Item("texto_prv"))
                dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
                If dtAlt.Rows.Count > 0 Then
                    dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & rowPrg.Item("codigo_prv"), "", Data.DataViewRowState.CurrentRows)
                    dtAlt = dvAlt.ToTable
                    If dtAlt.Rows.Count > 0 Then
                        For Each rowAlt As Data.DataRow In dtAlt.Rows
                            textohtml += ClsGlobales.fc_DesencriptaTexto64(rowAlt.Item("texto_ale"))
                        Next
                    End If
                End If
            Next
            'Dim se As New StringReader(textohtml)
            Dim rootPath As String = Server.MapPath("~")
            Dim customfont As BaseFont
            customfont = BaseFont.CreateFont(rootPath & "\administrativo\EvaluacionAdmision\font\Belwel.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
            iTextSharp.text.FontFactory.Register(rootPath & "\administrativo\EvaluacionAdmision\font\Belwel.ttf", "Belwe")
            Dim fontbold As New Font(customfont, 12.5, Font.BOLD)
            Dim style As New iTextSharp.text.html.simpleparser.StyleSheet
            style.LoadTagStyle(HtmlTags.P, HtmlTags.SIZE, "12.5pt")
            style.LoadTagStyle(HtmlTags.P, HtmlTags.FACE, "Belwe")
            style.LoadTagStyle(HtmlTags.P, HtmlTags.LEADING, "15")
            documento.Open()

            'Dim tags As HTMLTagProcessors = New HTMLTagProcessors()
            Dim tags As New iTextSharp.text.html.simpleparser.HTMLTagProcessors()
            tags(HtmlTags.IMG) = New CustomImageHTMLTagProcessor()

            Dim tablePdf = New PdfPTable(1)
            Dim providers As New Dictionary(Of String, Object)
            'Dim providers As New Object
            'Dim reader As TextReader
            'reader = New StringReader(textohtml)
            'Dim parsedHtmlElements As List(Of IElement)
            Using reader = New StringReader(textohtml)
                'With reader
                'Dim clsHTMLWorker As New HTMLWorker(documento)
                'Dim _count = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(reader, style, tags, Nothing).Count
                'tablePdf.AddCell(_count)
                Dim parsedHtmlElements = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(reader, style, tags, providers)
                ''parsedHtmlElements = clsHTMLWorker.ParseToList(reader, style, tags, providers)
                ''parsedHtmlElements = New HTMLWorker.ParseToList(reader, style, tags, Nothing)
                For Each htmlElement As List(Of IElement) In parsedHtmlElements
                    Dim pdfCell As PdfPCell = New PdfPCell
                    pdfCell.Border = 0
                    pdfCell.AddElement(htmlElement)
                    tablePdf.AddCell(pdfCell)
                    '    'tablePdf.AddCell("XD")
                Next
                ''End With
            End Using

            'Dim tags = New HTMLTagProcessors()
            'tags(HtmlTags.IMG) = New CustomImageHTMLTagProcessor()

            'Dim tablePdf = New PdfPTable(1)
            'Using reader = New StringReader(textohtml)
            '    Dim parsedHtmlElements = iTextSharp.text.html.simpleparser.HTMLWorker.ParseToList(reader, style, tags, Nothing)
            '    For Each htmlElement As List(Of IElement) In parsedHtmlElements
            '        Dim pdfCell As PdfPCell = New PdfPCell
            '        pdfCell.Border = 0
            '        pdfCell.AddElement(htmlElement)
            '        tablePdf.AddCell(pdfCell)
            '    Next
            'End Using

            documento.Add(tablePdf)

            documento.NewPage()
            documento.Close()

            Dim bytes_exam() As Byte = msExamen.ToArray
            msExamen.Close()

            Dim bytes_port() As Byte = fc_ObtenerArchivo(Session("adm_codigo_evl"), idTabla, "VWCJ2L63GR", 0)


            mt_CopiarPdf(bytes_port, bytes_exam, msPdf)
            'Dim bytes_pdf() As Byte = memory_pdf.ToArray
            'memory_pdf.Close()

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=" & nombre_evl.Replace(" ", "_") & ".pdf")
            Response.ContentType = "application/pdf"
            Response.Buffer = True
            Response.OutputStream.Write(msPdf.GetBuffer(), 0, msPdf.GetBuffer().Length)
            Response.OutputStream.Flush()
            Response.End()
        Else
            mt_ShowMessage("No se han encontrado preguntas asignadas a esta evaluación.", MessageType.Error)
        End If
    End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_port() As Byte, ByVal bytes_exam() As Byte, ByVal memory As System.IO.Stream)

        Dim pdfFile_exam As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_port As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter

        pdfFile_exam = New iTextSharp.text.pdf.PdfReader(bytes_exam)
        pdfFile_port = New iTextSharp.text.pdf.PdfReader(bytes_port)

        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()


        For x As Integer = 1 To pdfFile_port.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_port, x))
        Next

        pCopy.FreeReader(pdfFile_port)
        pdfFile_port.Close()

        For y As Integer = 1 To pdfFile_exam.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_exam, y))
        Next

        pCopy.FreeReader(pdfFile_exam)
        pdfFile_exam.Close()

        pCopy.Close()
        doc.Close()
    End Sub

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

    Private Function fc_ObtenerArchivo(ByVal idTransa As Long, ByVal idTabla As Integer, ByVal token As String, ByVal IdArchivo As Long) As Byte()
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = "USAT\esaavedra"
            Dim bytes() As Byte = Nothing

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, idTransa, token)
            obj.CerrarConexion()

            If tb.Rows.Count = 0 Then
                'Throw New Exception("¡ Archivo no encontrado !")
                obj.AbrirConexion()
                'tbDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", idTransa, -1, -1, -1, -1)
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 3, idTabla, IdArchivo, token)
                If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

                obj.CerrarConexion()
            End If

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                bytes = Convert.FromBase64String(imagen)
            End If
            'Response.Write(envelope)

            Return bytes

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function fc_ObtenerNombreArchivo(ByVal idTransa As Long, ByVal idTabla As Integer, ByVal token As String) As String
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim documento As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, idTransa, token)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            documento = dt.Rows(0).Item("NombreArchivo").ToString '& "." & dt.Rows(0).Item("Extencion").ToString
        End If
        Return documento
    End Function

#End Region

#Region "Class"

    Public Class CustomImageHTMLTagProcessor
        Implements iTextSharp.text.html.simpleparser.IHTMLTagProcessor

        'Public Sub EndElement(ByVal worker As iTextSharp.text.html.simpleparser.HTMLWorker, ByVal tag As String) Implements iTextSharp.text.html.simpleparser.IHTMLTagProcessor.EndElement
        'End Sub

        'Public Sub StartElement(ByVal worker As iTextSharp.text.html.simpleparser.HTMLWorker, ByVal tag As String, ByVal attrs As IDictionary(Of String, String)) Implements iTextSharp.text.html.simpleparser.IHTMLTagProcessor.StartElement
        '    Dim image As Image
        '    Dim src = attrs("src")
        '    Dim _with As Single = 0, _hight As Single = 0
        '    If src.StartsWith("data:image/") Then
        '        Dim base64Data = src.Substring(src.IndexOf(",") + 1)
        '        Dim imagedata = Convert.FromBase64String(base64Data)
        '        image = image.GetInstance(imagedata)
        '    Else
        '        image = image.GetInstance(src)
        '    End If
        '    _with = image.ScaledWidth
        '    _hight = image.ScaledHeight
        '    If _with > 600 Then
        '        If _hight > 700 Then
        '            image.ScalePercent(35)
        '        Else
        '            image.ScalePercent(75)
        '        End If
        '    Else
        '        image.ScalePercent(90)
        '    End If
        '    image.Alignment = 1
        '    worker.UpdateChain(tag, attrs)
        '    worker.ProcessImage(image, attrs)
        '    worker.UpdateChain(tag)
        'End Sub

        Public Sub EndElement(ByVal worker As iTextSharp.text.html.simpleparser.HTMLWorker, ByVal tag As String) Implements iTextSharp.text.html.simpleparser.IHTMLTagProcessor.EndElement
        End Sub

        Public Sub StartElement(ByVal worker As iTextSharp.text.html.simpleparser.HTMLWorker, ByVal tag As String, ByVal attrs As System.Collections.Generic.IDictionary(Of String, String)) Implements iTextSharp.text.html.simpleparser.IHTMLTagProcessor.StartElement
            Dim image As Image
            Dim src = attrs("src")
            Dim _with As Single = 0, _hight As Single = 0
            If src.StartsWith("data:image/") Then
                Dim base64Data = src.Substring(src.IndexOf(",") + 1)
                Dim imagedata = Convert.FromBase64String(base64Data)
                image = image.GetInstance(imagedata)
            Else
                image = image.GetInstance(src)
            End If
            _with = image.ScaledWidth
            _hight = image.ScaledHeight
            If _with > 600 Then
                If _hight > 700 Then
                    image.ScalePercent(35)
                Else
                    image.ScalePercent(75)
                End If
            Else
                image.ScalePercent(90)
            End If
            image.Alignment = 1
            worker.UpdateChain(tag, attrs)
            worker.ProcessImage(image, attrs)
            worker.UpdateChain(tag)
        End Sub
    End Class

#End Region

End Class
