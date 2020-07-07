﻿Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_frmPublicarSilabo
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    'Private idTabla As Integer = 22

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
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If IsPostBack = False Then
                Session("gc_dtCursoProg") = Nothing
                mt_CargarSemestre()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarTipoEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            If cboCarPro.Items.Count > 0 Then Me.cboCarPro.SelectedIndex = 0
            If cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboTipoEstudio.SelectedValue, cod_user)
            If cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedIndex = 0
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarPro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarPro.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboCarPro.SelectedValue)
            If Me.gvAsignatura.Rows.Count > 0 Then Me.gvAsignatura.DataSource = Nothing : Me.gvAsignatura.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEstudio.SelectedIndexChanged
        Try
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), Me.cboPlanEstudio.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
        Me.gvAsignatura.DataBind()
        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub btnDescargar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'btnListar_Click(Nothing, Nothing)
            'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim clsPdf As New clsGenerarPDF
        Dim memory As New System.IO.MemoryStream
        Dim memory_pdf As New System.IO.MemoryStream
        Dim index, codigo_cup, codigo_cur, codigo_pes, codigo_cac, codigo_dis As Integer
        Dim obj As New ClsConectarDatos
        Try
            index = CInt(e.CommandArgument)
            codigo_cup = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_cup"))
            Select Case e.CommandName
                Case "Ver"
                    clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                    clsPdf.anexo = Server.MapPath(".") & "/ManualTutoria.pdf"
                    clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory, True)
                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()

                    Dim idTransa As Long
                    idTransa = CLng(Me.gvAsignatura.DataKeys(index).Values("IdArchivo_Anexo"))

                    'Throw New Exception(idArchivo2)

                    Dim bytes_anexo() As Byte = fc_ObtenerArchivo(idTransa, 24, "SU3WMBVV4W")

                    mt_CopiarPdf(bytes, bytes_anexo, memory_pdf)
                    Dim bytes_pdf() As Byte = memory_pdf.ToArray
                    memory_pdf.Close()

                    Response.Clear()
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-length", bytes_pdf.Length.ToString())
                    Response.BinaryWrite(bytes_pdf)
                    'OnClientClick="return confirm('¿Desea visualizar el silabo?');"

                Case "Publicar"
                    Dim codigo_sib As Integer
                    Dim dt As New Data.DataTable
                    Dim respuesta, nombre_silabo, curso, grupo, semestre As String
                    respuesta = "" : nombre_silabo = ""
                    curso = Me.gvAsignatura.DataKeys(index).Values("nombre_Cur")
                    grupo = Me.gvAsignatura.DataKeys(index).Values("grupoHor_Cup")
                    semestre = Me.gvAsignatura.DataKeys(index).Values("descripcion_Cac")
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                        obj.IniciarTransaccion()
                        dt = obj.TraerDataTable("SilaboCurso_insertar", codigo_cup, 0, Date.Now.Date, 1, cod_user)
                        If dt.Rows.Count = 0 Then Throw New Exception("¡ No se pudo realizar la publicación del sílabo")
                        codigo_sib = CInt(dt.Rows(0).Item(0).ToString)
                        
                        'Data.Add("Step", idtabla & "-" & codigo_sib & "-" & CStr(codigo_cup) & ".pdf")
                        clsPdf.fuente = Server.MapPath(".") & "/segoeui.ttf"
                        clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory)
                        nombre_silabo = "Silabo " & semestre & " " & curso & " " & grupo & ".pdf"
                        'mt_ShowMessage(nombre_silabo, MessageType.Warning)
                        respuesta = fc_SubirArchivo(22, codigo_sib, codigo_cup, memory, nombre_silabo)
                        memory.Close()
                        obj.Ejecutar("SilaboCurso_actualizar", codigo_sib, codigo_cup, 0, Date.Now.Date, 1, cod_user)
                        obj.TerminarTransaccion()
                        mt_ShowMessage("¡ Se ha publicado el silabo correctamente !", MessageType.Success)
                        'mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoEstudio.SelectedValue)
                        cboPlanEstudio_SelectedIndexChanged(Nothing, Nothing)
                    Else
                        Throw New Exception("Inicie Session")
                    End If
                    'Data.Add("Status", "OK")
                    'Data.Add("Message", respuesta)
                    'list.Add(Data)
                    'JSONresult = serializer.Serialize(list)
                    'Response.Write(JSONresult)
                Case "Descargar"
                    Dim idArchivo As Long
                    idArchivo = CLng(Me.gvAsignatura.DataKeys(index).Values("IdArchivo"))
                    If idArchivo = 0 Then Throw New Exception("¡ Este silabo no se encuentra disponible !")
                    'Page.RegisterStartupScript("BusyBox", "<script>fc_ocultarBusy();</script>")
                    'Response.Write("<script>fc_OcultarBussy();</script>")
                    mt_DescargarArchivo(idArchivo, 22, "YAXVXFQACX")
                    'RegisterStartupScript("", "<script>fc_OcultarBussy();</script>")
                    'Page.ClientScript.RegisterStartupScript(Me.GetType, "script", "return fc_OcultarBussy()")
                    'btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage("¡ Se ha descargado el silabo correctamente !", MessageType.Success)
                Case "Observar"
                    Dim dtDis As New Data.DataTable
                    codigo_cac = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Cac"))
                    codigo_cur = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Cur"))
                    codigo_pes = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Pes"))
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    obj.AbrirConexion()
                    dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, codigo_pes, codigo_cur, codigo_cac, -1)
                    If dtDis.Rows.Count > 0 Then
                        codigo_dis = dtDis.Rows(0).Item(0)
                        Session("gc_codigo_dis") = codigo_dis
                        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
                    End If
                    obj.CerrarConexion()
            End Select
        Catch ex As Exception
            memory.Close()
            memory_pdf.Close()
            If e.CommandName = "Publicar" Then obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
            'Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim codigo_dis As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            codigo_dis = Session("gc_codigo_dis")
            obj.AbrirConexion()
            obj.Ejecutar("DiseñoAsignatura_observar", codigo_dis, Me.txtObservacion.Text, cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("¡ Se ha registrado la observación al sílabo !", MessageType.Success)
            cboPlanEstudio_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAsignatura.RowDataBound
        Try
            Dim nro_fecha, nro_sesion As String
            Dim peso_res As Double = 0
            nro_fecha = Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("nro_fecha")
            nro_sesion = Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("nro_sesion")
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Cells(6).Text = nro_fecha & " / " & nro_sesion
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarTipoEstudio(ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "PS", codigo_cac)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboTipoEstudio, dt, "codigo_test", "descripcion_test")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal codigo_test As Integer, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'cod_user = 648
            If cod_ctf = 1 Or cod_ctf = 232 Then user = -1
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV3", "PS", codigo_cac, codigo_test, user)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarPro, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPlanEstudio", "PS", codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_pes As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar", "PS", -1, codigo_pes, -1, codigo_cac)
            obj.CerrarConexion()
            Session("gc_dtCursoProg") = dt
            'Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
            Me.gvAsignatura.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_DescargarArchivo(ByVal IdArchivo As Long, ByVal idTabla As Integer, ByVal token As String)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontre el archivo !")

            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", token)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)

            If tb.Rows.Count > 0 Then
                Dim extencion As String
                extencion = tb.Rows(0).Item("Extencion")
                Select Case tb.Rows(0).Item("Extencion")
                    Case ".txt"
                        extencion = "text/plain"
                    Case ".doc"
                        extencion = "application/ms-word"
                    Case ".xls"
                        extencion = "application/vnd.ms-excel"
                    Case ".gif"
                        extencion = "image/gif"
                    Case ".jpg"
                    Case ".jpeg"
                    Case "jpeg"
                        extencion = "image/jpeg"
                    Case "png"
                        extencion = "image/png"
                    Case ".bmp"
                        extencion = "image/bmp"
                    Case ".wav"
                        extencion = "audio/wav"
                    Case ".ppt"
                        extencion = "application/mspowerpoint"
                    Case ".dwg"
                        extencion = "image/vnd.dwg"
                    Case ".pdf"
                        extencion = "application/pdf"
                    Case Else
                        extencion = "application/octet-stream"
                End Select

                Dim bytes As Byte() = Convert.FromBase64String(imagen)
                Response.Clear()
                Response.Buffer = False
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = extencion
                Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo").ToString)
                Response.AppendHeader("Content-Length", bytes.Length.ToString())
                Response.BinaryWrite(bytes)
                Response.End()
            End If
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CopiarPdf(ByVal bytes_pdf() As Byte, ByVal bytes_anexo() As Byte, ByVal memory As System.IO.Stream)
        Dim pdfFile_anexo As iTextSharp.text.pdf.PdfReader
        Dim pdfFile_silabo As iTextSharp.text.pdf.PdfReader
        Dim doc As iTextSharp.text.Document
        Dim pCopy As iTextSharp.text.pdf.PdfWriter
        'Dim msOutput As MemoryStream = New MemoryStream()
        pdfFile_anexo = New iTextSharp.text.pdf.PdfReader(bytes_anexo)
        pdfFile_silabo = New iTextSharp.text.pdf.PdfReader(bytes_pdf)
        doc = New iTextSharp.text.Document()
        pCopy = New iTextSharp.text.pdf.PdfSmartCopy(doc, memory)
        doc.Open()

        'For k As Integer = 0 To files.Count - 1
        'pdfFile = New iTextSharp.text.pdf.PdfReader(Server.MapPath(".") & "/ManualTutoria.pdf")

        For j As Integer = 1 To pdfFile_silabo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_silabo, j))
        Next

        pCopy.FreeReader(pdfFile_silabo)
        pdfFile_silabo.Close()

        For i As Integer = 1 To pdfFile_anexo.NumberOfPages
            CType(pCopy, iTextSharp.text.pdf.PdfSmartCopy).AddPage(pCopy.GetImportedPage(pdfFile_anexo, i))
        Next

        pCopy.FreeReader(pdfFile_anexo)
        'Next

        pdfFile_anexo.Close()

        pCopy.Close()
        doc.Close()
    End Sub

#End Region

#Region "Funciones"

    Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal nro_operacion As String, ByVal file As System.IO.MemoryStream, ByVal name As String) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString

            Dim binData As Byte() = file.ToArray
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = name.Replace("&", "_").Replace("'", "_").Replace("*", "_")

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", ".pdf")
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", nro_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)

            Return result

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

    Private Function fc_ObtenerArchivo(ByVal idTransa As Long, ByVal idTabla As Integer, ByVal token As String) As Byte()
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")
            Dim bytes() As Byte = Nothing

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, idTransa, token)
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontre el archivo !")

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

#End Region

End Class
