Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Web
Public Class ClsArchivosCompartidosV2
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="image"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SoapEnvelopeAlumni(ByVal image As String) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
            .AppendLine("<soap:Body>")
            .AppendLine("<ExistsStudentPicture  xmlns=""http://usat.edu.pe"">")
            .AppendLine("<image>" & image & "</image>")
            .AppendLine("</ExistsStudentPicture >")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SoapEnvelope(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
            .AppendLine("<soap:Body>")
            .AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<UploadFileInqRq>")
            .AppendLine("<Fecha>" & Data("Fecha").ToString() & "</Fecha>")
            .AppendLine("<Extencion>" & Data("Extencion").ToString & "</Extencion>")
            .AppendLine("<Nombre>" & Data("Nombre").ToString & "</Nombre>")
            .AppendLine("<TransaccionId>" & Data("TransaccionId").ToString & "</TransaccionId>")
            .AppendLine("<TablaId>" & CType(Data("TablaId").ToString, Integer) & "</TablaId>")
            .AppendLine("<NroOperacion>" & Data("NroOperacion").ToString & "</NroOperacion>")
            .AppendLine("<Archivo>" & Data("Archivo").ToString & "</Archivo>")
            .AppendLine("<Usuario>" & Data("Usuario").ToString & "</Usuario>")
            .AppendLine("<Equipo>" & Data("Equipo").ToString & "</Equipo>")
            .AppendLine("<Ip>" & Data("Ip").ToString & "</Ip>")
            .AppendLine("<param8>" & Data("param8").ToString & "</param8>")
            .AppendLine("</UploadFileInqRq>")
            .AppendLine("</UploadFile>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function
    ''' <summary>
    ''' envelope para descargar archisvo. esta funcion te un caracter general. cualquier cambio sin la respectiva configuracion en el servicio web wb
    ''' no se podria efectuar ningun tipo de descarga de documentos digitales.
    ''' </summary>
    ''' <param name="Data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SoapEnvelopeDescarga(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
            .AppendLine("<soap:Body>")
            .AppendLine("<DownloadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<IdArchivo>" & Data("IdArchivo") & "</IdArchivo>")
            .AppendLine("<param>" & Data("Usuario").ToString() & "</param>")
            .AppendLine("<param1>" & Data("Token").ToString() & "</param1>")
            .AppendLine("<param2></param2>")
            .AppendLine("</DownloadFile>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function
    ''' <summary>
    ''' Envelope  para generar la hoja de remuneraciones digital
    ''' </summary>
    ''' <param name="Data">Diccionario de datos que son eviados atraves del servicio web</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SoapEnvelopeGenerarBoleta(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
            .AppendLine("<soap:Body>")
            .AppendLine("<GenerateTicket  xmlns=""http://usat.edu.pe"">")
            .AppendLine("<param1>" & Data("CodigoPer").ToString() & "</param1>")
            .AppendLine("<param2>" & Data("CodigoPlla").ToString() & "</param2>")
            .AppendLine("<param3>" & Data("TipoPlla").ToString() & "</param3>")
            .AppendLine("<param4>" & Data("Mes").ToString() & "</param4>")
            .AppendLine("<param5>" & Data("Periodo").ToString() & "</param5>")
            .AppendLine("<param6>" & Data("TablaId").ToString() & "</param6>")
            .AppendLine("<param7>" & Data("Fecha").ToString() & "</param7>")
            .AppendLine("<param8>" & Data("Usuario").ToString() & "</param8>")
            .AppendLine("</GenerateTicket>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function
    ''' <summary>
    ''' 'Cabecera de paticion SOAP para genera la hota semestral de ct
    ''' </summary>
    ''' <param name="Data">Diccionado de datos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SoapEnvelopeGenerarCTS(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
            .AppendLine("<soap:Body>")
            .AppendLine("<GenerateCTS  xmlns=""http://usat.edu.pe"">")
            .AppendLine("<Semestre>" & Data("Semestre").ToString() & "</Semestre>")
            .AppendLine("<Periodo>" & Data("Periodo").ToString() & "</Periodo>")
            .AppendLine("<ImpTc>" & Data("ImpTc").ToString() & "</ImpTc>")
            .AppendLine("<CodigoPer>" & Data("CodigoPer").ToString() & "</CodigoPer>")
            .AppendLine("<Fecha>" & Data("Fecha").ToString() & "</Fecha>")
            .AppendLine("<TablaId>" & Data("TablaId").ToString() & "</TablaId>")
            .AppendLine("</GenerateCTS>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function
    'Public Function SoapEnvelopeGenerarBoleta(ByVal Data As Dictionary(Of String, String)) As String
    '    Dim envelope As New StringBuilder
    '    With envelope
    '        .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
    '        .AppendLine("<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">")
    '        .AppendLine("<soap:Body>")
    '        .AppendLine("<GenerateTicket  xmlns=""http://usat.edu.pe"">")
    '        .AppendLine("<CodigoPer>" & Data("CodigoPer").ToString() & "</CodigoPer>")
    '        .AppendLine("<CodigoPlla>" & Data("CodigoPlla").ToString() & "</CodigoPlla>")
    '        .AppendLine("<TipoPlla>" & Data("TipoPlla").ToString() & "</TipoPlla>")
    '        .AppendLine("<Mes>" & Data("Mes").ToString() & "</Mes>")
    '        .AppendLine("<Periodo>" & Data("Periodo").ToString() & "</Periodo>")
    '        .AppendLine("<TablaId>" & Data("TablaId").ToString() & "</TablaId>")
    '        .AppendLine("<Fecha>" & Data("Fecha").ToString() & "</Fecha>")
    '        .AppendLine("</GenerateTicket>")
    '        .AppendLine("</soap:Body>")
    '        .AppendLine("</soap:Envelope>")
    '    End With
    '    Return envelope.ToString()
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="UrlService">
    ''' Url del servicio web en donde se encuentra alogado en el caso del server real debe ir http://localhost/
    ''' el caso de server dev http://serverdev/  server QA http://serverqa/
    ''' </param>
    ''' <param name="envelope"></param>
    ''' <param name="SoapAction">el actio del servicio web va depender de la accion que estan realizadon</param>
    ''' <param name="QVUSER">usuario que esta autenticado.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PeticionRequestSoap(ByVal UrlService As String, ByVal envelope As String, ByVal SoapAction As String, ByVal QVUSER As String) As String
        Try
            Dim webRequest As HttpWebRequest = CType(System.Net.WebRequest.Create(UrlService), HttpWebRequest)
            Dim mycredentialCache As CredentialCache = New CredentialCache()
            Dim credentials As NetworkCredential = New NetworkCredential("esaavedra", "www.usat.edu.pe", "usat.edu.pe")
            mycredentialCache.Add(New Uri(UrlService), "Basic", credentials)
            Dim oCookies As New CookieContainer

            webRequest.Headers.Clear()
            webRequest.ProtocolVersion = HttpVersion.Version11
            webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
            'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36" ' "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
            webRequest.ContentType = "text/xml; charset=UTF-8" '"txt/xml;charset=""UTF-8""";
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
            webRequest.Headers.Add("QVUSER", QVUSER)
            webRequest.Headers.Add("SOAPAction", SoapAction)
            webRequest.Method = "POST"
            webRequest.CookieContainer = oCookies
            webRequest.Credentials = CredentialCache.DefaultCredentials
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
            Dim encoding As New ASCIIEncoding()

            Dim bytes() As Byte = encoding.GetBytes(envelope)
            webRequest.ContentLength = bytes.Length

            Using responseStream As Stream = webRequest.GetRequestStream()
                If responseStream IsNot Nothing Then
                    Using streamreader As New StreamWriter(responseStream)
                        streamreader.Write(envelope)
                    End Using
                End If
            End Using

            Dim responseResult As String = ""
            Using response As WebResponse = webRequest.GetResponse()
                Using responseStream As Stream = response.GetResponseStream()
                    If responseStream IsNot Nothing Then
                        Using streamreader As New StreamReader(responseStream)
                            responseResult = streamreader.ReadToEnd()
                        End Using
                    End If
                End Using
            End Using
            Return responseResult
            'Return "OK"
        Catch ex As Exception
            Return ex.Message & "---" & ex.StackTrace
        End Try
    End Function
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
End Class