Imports Microsoft.VisualBasic
Imports System.Net
Imports System.Collections.Generic
Imports System.IO
Imports System.Xml

Public Class ClsDspaceCrisUsat
    Public Function SoapEnvelopeListar(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:sch=""http://4science.github.io/dspace-cris/schemas"">")
            .AppendLine("<soapenv:Header/>")
            .AppendLine("<soapenv:Body>")
            .AppendLine("<sch:NormalAuthQueryRequest>")
            .AppendLine("<sch:Auth>")
            .AppendLine("<sch:Username>usat</sch:Username>")
            .AppendLine("<sch:Password>usat</sch:Password>")
            .AppendLine("</sch:Auth>")
            .AppendLine("<sch:InfoQuery>")
            '.AppendLine("<sch:Query>search.name:'Desarrollo'</sch:Query>") ' Filtro de Consulta SOLR Filtro
            '.AppendLine("<sch:Query>*</sch:Query>") ' Consulta SOLR
            .AppendLine("<sch:Query>" + Data("sql").ToString + "</sch:Query>") ' Consulta SOLR Filtro
            '.AppendLine("<sch:PaginationRows>20</sch:PaginationRows>") ' Filas por Página
            .AppendLine("<sch:PaginationRows>" + Data("FilasPorPagina") + "</sch:PaginationRows>") ' Filas por P'agina
            '.AppendLine("<sch:PaginationStart>0</sch:PaginationStart>") ' Inicio de Pagina
            .AppendLine("<sch:PaginationStart>" + Data("PaginaInicio") + "</sch:PaginationStart>") ' Inicio de Pagina
            .AppendLine("<sch:Projection>")
            '.AppendLine("<sch:orgunitProjection>name</sch:orgunitProjection>") ' Columnas que se consultan que se muestran separados por espacios
            .AppendLine("<sch:" + Data("EntidadSimple").ToString + "Projection>" + Data("Columnas").ToString + "</sch:" + Data("EntidadSimple").ToString + "Projection>") ' Columnas que se consultan que se muestran separados por espacios
            .AppendLine("</sch:Projection>")
            .AppendLine("<sch:Type>")
            '.AppendLine("<sch:type>orgunits</sch:type>") 'Entidad a la que se consulta
            .AppendLine("<sch:type>" + Data("Entidad").ToString + "</sch:type>") ' Entidad a la que se consulta
            .AppendLine("</sch:Type>")
            .AppendLine("<sch:Sort SortOrder='asc'>0</sch:Sort>")
            .AppendLine("</sch:InfoQuery>")
            .AppendLine("</sch:NormalAuthQueryRequest>")
            .AppendLine("</soapenv:Body>")
            .AppendLine("</soapenv:Envelope>")
        End With
        Return envelope.ToString()
    End Function


    Public Function SoapEnvelopeINV(ByVal Data As Dictionary(Of String, String)) As String
        Dim envelope As New StringBuilder
        With envelope
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:sch=""http://4science.github.io/dspace-cris/schemas"">")
            .AppendLine("<soapenv:Header/>")
            .AppendLine("<soapenv:Body>")
            .AppendLine("<sch:NormalAuthQueryRequest>")
            .AppendLine("<sch:Auth>")
            .AppendLine("<sch:Username>hcano</sch:Username>")
            .AppendLine("<sch:Password>hcano</sch:Password>")
            .AppendLine("</sch:Auth>")
            .AppendLine("<sch:InfoQuery>")
            .AppendLine("<sch:Query>*:*</sch:Query>")
            .AppendLine("<sch:PaginationRows>20</sch:PaginationRows>")
            .AppendLine("<sch:PaginationStart>0</sch:PaginationStart>")
            .AppendLine("<sch:Projection>")
            .AppendLine("<sch:researcherProjection>fullName email orcid</sch:researcherProjection>")
            .AppendLine("</sch:Projection>")
            .AppendLine("<sch:Type>")
            .AppendLine("<sch:type>researcherPages</sch:type>")
            .AppendLine("</sch:Type>")
            .AppendLine("</sch:InfoQuery>")
            .AppendLine("</sch:NormalAuthQueryRequest>")
            .AppendLine("</soapenv:Body>")
            .AppendLine("</soapenv:Envelope>")
        End With
        Return envelope.ToString()
    End Function


    Public Function PeticionRequestSoap(ByVal UrlService As String, ByVal envelope As String, ByVal SoapAction As String, ByVal QVUSER As String) As String
        Try
            Dim webRequest As HttpWebRequest = CType(System.Net.WebRequest.Create(UrlService), HttpWebRequest)
            ''Dim mycredentialCache As CredentialCache = New CredentialCache()
            ''Dim credentials As NetworkCredential = New NetworkCredential("hcano", "hcano", "10.10.21.25")
            ''mycredentialCache.Add(New Uri(UrlService), "Basic", credentials)
            ''Dim oCookies As New CookieContainer

            webRequest.Headers.Clear()
            'webRequest.ProtocolVersion = HttpVersion.Version11
            webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
            ''Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36" ' "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
            webRequest.ContentType = "text/xml; charset=UTF-8" '"txt/xml;charset=""UTF-8""";
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
            ''webRequest.Headers.Add("QVUSER", QVUSER)
            'webRequest.Headers.Add("SOAPAction", SoapAction)
            webRequest.Headers.Add("SOAPAction", SoapAction)

            webRequest.Method = "POST"
            'webRequest.CookieContainer = oCookies
            'webRequest.Credentials = CredentialCache.DefaultCredentials
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

            'Dim responseResult As String = ""
            'Using response As WebResponse = webRequest.GetResponse()
            '    Using responseStream As Stream = response.GetResponseStream()
            '        If responseStream IsNot Nothing Then
            '            Using streamreader As New StreamReader(responseStream)
            '                responseResult = streamreader.ReadToEnd()
            '            End Using
            '        End If
            '    End Using
            'End Using

            Dim Response As HttpWebResponse = webRequest.GetResponse
            Dim DataStream As Stream = Response.GetResponseStream()
            Dim reader As New StreamReader(DataStream)
            Return reader.readtoend
        Catch ex As Exception

            Return ex.Message '& "---" & ex.StackTrace

        End Try
    End Function


    Public Function UrlServices(ByVal envelope As String, ByVal UrlService As String) As String
        Dim webRequest As HttpWebRequest
        'webRequest = Net.WebRequest.Create("http://api.neuvoo.com/apisearch?publisher=fe7ca91&q=java&l=buenos+aires%2C+ar&userip=1.2.3.4&useragent=Mozilla/%2F4.0%28Firefox%29&v=2")
        webRequest = Net.WebRequest.Create(UrlService)

        Dim Response__1 As New XmlDocument()

        '_soapEnvelope = envelope
        webRequest.Headers.Clear()
        webRequest.ProtocolVersion = HttpVersion.Version11
        webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
        'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
        webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36"
        ' @"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
        webRequest.ContentType = "text/xml; charset=UTF-8"
        '"txt/xml;charset=\"UTF-8\"";
        webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
        webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
        webRequest.Headers.Add("SOAPAction", "")
        webRequest.Method = "POST"
        webRequest.Credentials = CredentialCache.DefaultCredentials
        webRequest.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
        Dim encoding As New ASCIIEncoding()

        Dim bytes() As Byte = encoding.GetBytes(envelope)
        webRequest.ContentLength = bytes.Length

        Using strem As Stream = webRequest.GetRequestStream()
            strem.Write(bytes, 0, bytes.Length)
            strem.Close()
        End Using

        Dim responseResult As String = ""
        Using response__2 As WebResponse = webRequest.GetResponse()
            Using responseStream As Stream = response__2.GetResponseStream()
                If responseStream IsNot Nothing Then
                    Using streamreader As New StreamReader(responseStream)
                        '  System.Diagnostics.Debug.Write(responseResult);
                        responseResult = streamreader.ReadToEnd()
                    End Using
                End If
            End Using
        End Using

        Return responseResult
    End Function
End Class
