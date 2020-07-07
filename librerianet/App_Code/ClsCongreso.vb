Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Net
Imports System.IO
Imports System.Xml

Public Class ClsCongreso
    Public Function SoapEnvelope(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<Inscripcion xmlns=""http://tempuri.org/"">")
            .AppendLine("<token_cco>" & Data("token_cco").ToString() & "</token_cco>")
            .AppendLine("<codigo_eve>" & Data("codigo_eve").ToString() & "</codigo_eve>")
            .AppendLine("<tipo_doc>" & Data("tipo_doc").ToString & "</tipo_doc>")
            .AppendLine("<nro_doc>" & Data("nro_doc").ToString & "</nro_doc>")
            .AppendLine("<apepat>" & Data("apepat").ToString & "</apepat>")
            .AppendLine("<apemat>" & Data("apemat").ToString & "</apemat>")
            .AppendLine("<nombre>" & Data("nombre").ToString & "</nombre>")
            .AppendLine("<fecha_nac>" & Data("fecha_nac").ToString & "</fecha_nac>")
            .AppendLine("<sexo>" & Data("sexo").ToString & "</sexo>")
            .AppendLine("<email>" & Data("email").ToString & "</email>")
            .AppendLine("<universidad>" & Data("universidad").ToString & "</universidad>")
            .AppendLine("<tipo_participante>" & Data("tipo_participante").ToString & "</tipo_participante>")
            .AppendLine("<telefono>" & Data("telefono").ToString & "</telefono>")
            .AppendLine("<requiere_factura>" & Data("requiere_factura").ToString & "</requiere_factura>")
            .AppendLine("</Inscripcion>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

    Public Function SoapEnvelopeVacio() As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<Universidades xmlns=""http://tempuri.org/"" />")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

    Public Function SoapEnvelopeToken(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<VerCentroCosto xmlns=""http://tempuri.org/"">")
            .AppendLine("<token_cco>" & Data("token_cco").ToString & "</token_cco>")
            .AppendLine("</VerCentroCosto>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

    Public Function SoapEnvelopeTokenP(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<TipoParticipantes xmlns=""http://tempuri.org/"">")
            .AppendLine("<token_cco>" & Data("token_cco").ToString & "</token_cco>")
            .AppendLine("</TipoParticipantes>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

    Public Function SoapEnvelopeTokenC(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<ParametrosTipoParticipante xmlns=""http://tempuri.org/"">")
            .AppendLine("<token_cco>" & Data("token_cco").ToString & "</token_cco>")
            .AppendLine("<codigo_tpar>" & Data("codigo_tpar").ToString & "</codigo_tpar>")
            .AppendLine("</ParametrosTipoParticipante>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

    Public Function PeticionRequestSoap(ByVal UrlService As String, ByVal envelope As String, ByVal SoapAction As String, ByVal QVUSER As String) As String

        Try
            Dim webRequest As HttpWebRequest = CType(System.Net.WebRequest.Create(UrlService), HttpWebRequest)

            'Dim mycredentialCache As CredentialCache = New CredentialCache()
            'Dim credentials As NetworkCredential = New NetworkCredential("serverdev\esaavedra", "serverdev", "usat.edu.pe")
            'mycredentialCache.Add(New Uri(UrlService), "Basic", credentials)

            Dim oCookies As New CookieContainer

            webRequest.Headers.Clear()
            webRequest.ProtocolVersion = HttpVersion.Version11
            webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
            'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36" ' "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
            webRequest.ContentType = "text/xml; charset=UTF-8" '"txt/xml;charset=""UTF-8""";
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
            'webRequest.Headers.Add("QVUSER", QVUSER)
            webRequest.Headers.Add("SOAPAction", SoapAction)
            webRequest.Method = "POST"
            webRequest.CookieContainer = oCookies
            'webRequest.Credentials = CredentialCache.DefaultCredentials
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
            'Dim encoding As New ASCIIEncoding()
            Dim encoding As UTF8Encoding = New UTF8Encoding()

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
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Public Function PeticionRequestSoap2(ByVal UrlService As String, ByVal envelope As String, ByVal SoapAction As String, ByVal QVUSER As String) As String

        Try
            Dim webRequest As HttpWebRequest = CType(System.Net.WebRequest.Create(UrlService), HttpWebRequest)

            'Dim mycredentialCache As CredentialCache = New CredentialCache()
            'Dim credentials As NetworkCredential = New NetworkCredential("serverdev\esaavedra", "serverdev", "usat.edu.pe")
            'mycredentialCache.Add(New Uri(UrlService), "Basic", credentials)

            Dim oCookies As New CookieContainer

            webRequest.Headers.Clear()
            webRequest.ProtocolVersion = HttpVersion.Version11
            webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
            'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36" ' "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
            webRequest.ContentType = "text/xml; charset=UTF-8" '"txt/xml;charset=""UTF-8""";
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
            'webRequest.Headers.Add("QVUSER", QVUSER)
            webRequest.Headers.Add("SOAPAction", SoapAction)
            webRequest.Method = "POST"
            webRequest.CookieContainer = oCookies
            'webRequest.Credentials = CredentialCache.DefaultCredentials
            'Dim encoding As New ASCIIEncoding()
            Dim encoding As UTF8Encoding = New UTF8Encoding()

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
        Catch ex As Exception
            Return ex.Message
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

    Function ResultFileTabla(ByVal cadXml As String, ByVal tag As String) As Data.DataTable
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        Dim NodeList As XmlNodeList
        Dim nombreSpace As XmlNamespaceManager
        nombreSpace = New XmlNamespaceManager(xml.NameTable)
        nombreSpace.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        NodeList = xml.DocumentElement.SelectNodes("/soap:Envelope/soap:Body", nombreSpace)
        Dim param As XmlElement = Nothing
        If (NodeList(0).HasChildNodes) Then
            param = NodeList(0).ChildNodes(0)
            If (NodeList(0).ChildNodes(0).HasChildNodes) Then
                param = NodeList(0).ChildNodes(0).ChildNodes(0)
                'If (NodeList(0).ChildNodes(0).HasChildNodes) Then
                '    param = NodeList(0).ChildNodes(0).ChildNodes(0).ChildNodes(0)
                'End If
            End If
        End If

        Dim i As Integer = 0

        Dim dt As New Data.DataTable
        dt.Columns.Add("cod")
        dt.Columns.Add("des")
        dt.Columns.Add("val1")
        dt.Columns.Add("val2")
        dt.Columns.Add("val3")
        dt.Columns.Add("val4")
        dt.Columns.Add("val5")
        For i = 0 To param.ChildNodes.Count - 1
            dt.Rows.Add(param.ChildNodes(i).Item("valor").InnerText.ToString, param.ChildNodes(i).Item("descripcion").InnerText.ToString, param.ChildNodes(i).Item("valor1").InnerText.ToString, param.ChildNodes(i).Item("valor2").InnerText.ToString, param.ChildNodes(i).Item("valor3").InnerText.ToString, param.ChildNodes(i).Item("valor4").InnerText.ToString, param.ChildNodes(i).Item("valor5").InnerText.ToString)
        Next

        Return dt
    End Function
    '///********* Olluen 28/11/2019 procedimiento para Alumni
    Public Function SoapEnvelopeAlumni(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Body>")
            '.AppendLine("<UploadFile xmlns=""http://usat.edu.pe"">")
            .AppendLine("<InscripcionAlumni xmlns=""http://tempuri.org/"">")
            .AppendLine("<token_cco>" & Data("token_cco").ToString() & "</token_cco>")
            .AppendLine("<codigo_eve>" & Data("codigo_eve").ToString() & "</codigo_eve>")
            .AppendLine("<tipo_doc>" & Data("tipo_doc").ToString & "</tipo_doc>")
            .AppendLine("<nro_doc>" & Data("nro_doc").ToString & "</nro_doc>")
            .AppendLine("<apepat>" & Data("apepat").ToString & "</apepat>")
            .AppendLine("<apemat>" & Data("apemat").ToString & "</apemat>")
            .AppendLine("<nombre>" & Data("nombre").ToString & "</nombre>")
            .AppendLine("<fecha_nac>" & Data("fecha_nac").ToString & "</fecha_nac>")
            .AppendLine("<sexo>" & Data("sexo").ToString & "</sexo>")
            .AppendLine("<email>" & Data("email").ToString & "</email>")
            .AppendLine("<universidad>" & Data("universidad").ToString & "</universidad>")
            .AppendLine("<tipo_participante>" & Data("tipo_participante").ToString & "</tipo_participante>")
            .AppendLine("<telefono>" & Data("telefono").ToString & "</telefono>")
            .AppendLine("<requiere_factura>" & Data("requiere_factura").ToString & "</requiere_factura>")
            .AppendLine("<indica_labora>" & Data("indica_labora").ToString & "</indica_labora>")

            .AppendLine("<empresa>" & Data("empresa").ToString & "</empresa>")
            .AppendLine("<cargo>" & Data("cargo").ToString & "</cargo>")
            .AppendLine("<dirEmp>" & Data("dirEmp").ToString & "</dirEmp>")
            .AppendLine("<telefEmp>" & Data("telefEmp").ToString & "</telefEmp>")
            .AppendLine("<emailEmp>" & Data("emailEmp").ToString & "</emailEmp>")
            .AppendLine("<indLabMod>" & Data("indLabMod").ToString & "</indLabMod>")

            .AppendLine("</InscripcionAlumni>")
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return envelope.ToString()
    End Function

End Class
