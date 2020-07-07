Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Xml
Imports system.web.script

Partial Class DataJson_Logistica_Movimientos_Logistica
    Inherits System.Web.UI.Page

    Dim objL As New ClsLogistica

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim codigo2 As Integer
        Dim codigo1 As String = ""
        Dim idTabla As Integer
        Dim action As String = ""
        Try
            If Session("id_per") <> "" Then
                Dim list As New List(Of Dictionary(Of String, Object))()
                Dim tipo As String = ""
                Dim arr As New List(Of String)
                
                Dim codigo_per As String = Session("id_per")

                If Request.Form("cod1") <> "" Then
                    codigo1 = objL.DecrytedString64(Request.Form("cod1"))
                End If

                If Request.Form("cod2") <> "" Then
                    codigo2 = CInt(objL.DecrytedString64(Request.Form("cod2")))
                End If

                If Request.Form("idTabla") <> "" Then
                    idTabla = Request.Form("idTabla")
                End If
                action = Request.Form("action").ToString()

                Select Case action
                    Case "Upload"
                        SubirArchivo(codigo1, codigo2, idTabla)
                    Case "Cerrar"
                        CerrarProyecto(codigo1)
                    Case "Ver"
                        ListarProyectosFiles(codigo1, idTabla)
                    Case "Download"
                        DescargarArchivo()
                        'Data.Add("sw", True)
                        'Data.Add("session", Session("id_per"))
                        'Data.Add("cod1", codigo1)
                        'Data.Add("cod2", codigo2)
                        'Data.Add("idTabla", idTabla)
                        'Data.Add("action", action)
                        'Data.Add("archivo", Request.Form("cod_Arch"))
                        'JSONresult = serializer.Serialize(Data)
                        'Response.Write(JSONresult)
                End Select

            Else
                Response.Redirect("../../../personal")
            End If
        Catch ex As Exception
            Data.Add("sw", False)
            Data.Add("msje", ex.Message)
            Data.Add("session", Session("id_per"))
            Data.Add("cod1", objL.DecrytedString64(Request.Form("cod1")))
            Data.Add("cod2", objL.DecrytedString64(Request.Form("cod2")))
            Data.Add("idTabla", Request.Form("idTabla"))
            Data.Add("action", Request.Form("action").ToString())
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
            Throw ex
        End Try
    End Sub

    Sub SubirArchivo(ByVal codigo1 As Integer, ByVal codigo2 As Integer, ByVal idTabla As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        ' Dim List As New List(Of Dictionary(Of String, Object))()
        Dim Data As New Dictionary(Of String, Object)()

        Dim Fecha As String = Format(Now(), "dd/MM/yyyy").ToString

        Try
            Dim obj As New ClsLogistica
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")

            'Dim NroRend As String = Request.Form("hdn")
            Dim Input(post.ContentLength) As Byte
            'Data.Add("msje", Session("perlogin"))
            'JSONresult = serializer.Serialize(Data)
            'Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            'Dim usuario_session As String = usuario_session_(1)
            Dim Usuario As String = Session("perlogin")

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo1.ToString)
            list.Add("TablaId", idTabla.ToString)
            list.Add("NroOperacion", codigo2.ToString)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "DSISTEMAS13")
            'list.Add("Ip", "10.10.1.5")
            list.Add("Ip", "")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", ex.Message)
            Data.Add("fecha", Session("perlogin"))
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try

    End Sub

    Sub DescargarArchivo()
        Dim tb As New Data.DataTable
        Try
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)
            'Dim IdArchivo As Integer = objL.DecrytedString64(Request.Form("cod_Arch"))
            Dim IdArchivo As String = Request.Form("cod_Arch")

            'Dim wsCloud As New ClsArchivosCompartidos
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 6, IdArchivo, "TE2S9PT6VW")
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            Dim rpta As New Dictionary(Of String, Object)()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", "USAT\ESAAVEDRA")
            list.Add("Token", "TE2S9PT6VW")
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            Dim imagen As String = fc_ResultFile(result)
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
            If resultData.Count > 0 Then
                rpta.Add("tipo", "success")
                rpta.Add("msje", "Se ha descargado el archivo " & resultData.Item(0).Item("Nombre"))
            Else
                rpta.Add("tipo", "error")
                rpta.Add("msje", "No existe el archivo")
            End If
            resultData.Add(rpta)

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", "error-SERV")
            'Data.Add("tb", tb.Rows.Count)
            'Data.Add("Usuario", usuario_session)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarProyectosFiles(ByVal cod As String, ByVal idTabla As Integer)
        Dim data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            'data.Add("Step", "Inicio")
            'list.Add(data)

            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("USP_listarArchivosCompartidos2", cod)
            obj.CerrarConexion()

            'data.Add("codigo", cod)
            'list.Add(data)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    If i = 0 Then data.Add("sw", True)
                    data.Add("cCod", tb.Rows(i).Item("IdArchivosCompartidos"))
                    data.Add("nArchivo", tb.Rows(i).Item("NombreArchivo"))
                    data.Add("nExtension", ExtensionArchivo(tb.Rows(i).Item("Extencion").ToString))
                    data.Add("nExt", tb.Rows(i).Item("Extencion").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            'Dim list As New List(Of Dictionary(Of String, Object))()
            'Dim data As New Dictionary(Of String, Object)()
            'data.Add("sw", True)
            'data.Add("msje", ex.Message)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
            Throw ex
        End Try
    End Sub

    Sub CerrarProyecto(ByVal codigo_acp As Integer)
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Try
            Dim objcrm As New ClsLogistica
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("[dbo].[POA_CerrarProyecto]", codigo_acp)
            obj.CerrarConexion()

            data.Add("msje", "Proyecto Cerrado Correctamente")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'data.Add("msje", ex.Message)
            data.Add("msje", "Proyecto No se Pudo Cerrar Correctamente")
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try

    End Sub

    Function ResultFile(ByVal cadXml As String) As String
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        Return res.InnerText
        '   Response.Write("dd" + res.InnerText)
    End Function

    Private Function ExtensionArchivo(ByVal ext As String) As String
        Dim extencion As String = ""
        Select Case ext.Trim.ToString
            Case ".txt"
                'extencion = "text/plain"
                extencion = "fa fa-file-text-o"
            Case ".doc"
            Case ".docx"

                'extencion = "application/ms-word"
                extencion = "fa fa-file-word-o"
            Case ".xls"
            Case ".xlsx"
                'extencion = "application/vnd.ms-excel"
                extencion = "fa fa-file-excel-o"
            Case ".gif"
                'extencion = "image/gif"
                extencion = "fa fa-file-image-o"
            Case ".jpg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".jpeg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".png"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".bmp"
                'extencion = "image/bmp"
                extencion = "fa fa-file-image-o"
            Case ".wav"
                'extencion = "audio/wav"
                extencion = "fa fa-file-audio-o"
            Case ".ppt"
                'extencion = "application/mspowerpoint"
                extencion = "fa fa-file-powerpoint-o"
            Case ".dwg"
                'extencion = "image/vnd.dwg"
                extencion = "fa fa-file-code-o"
            Case ".zip"
                extencion = "fa fa-file-archive-o"
            Case ".rar"
                extencion = "fa fa-file-archive-o"
            Case ".pdf"
                extencion = "fa fa-file-pdf-o"
            Case Else
                'extencion = "application/octet-stream"
                extencion = ""
        End Select
        Return extencion
    End Function

    ''' <summary>
    ''' 20181207 ENevado
    ''' </summary>
    ''' <param name="cadXml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As XmlNamespaceManager
            Dim xml As XmlDocument = New XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
