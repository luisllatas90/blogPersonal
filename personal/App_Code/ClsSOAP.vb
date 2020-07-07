Imports Microsoft.VisualBasic
Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Collections.Generic

Public Class ClsSOAP
    Private ms_AccionURL As String = "http://tempuri.org/"

    Public Function lr_RealizarPeticionSOAP(ByVal ls_ServicioURL As String, ByVal ls_Accion As String, Optional ByVal lo_Datos As Dictionary(Of String, String) = Nothing) As String
        Try
            Dim webRequest As HttpWebRequest = CType(System.Net.WebRequest.Create(ls_ServicioURL), HttpWebRequest)
            Dim oCookies As New CookieContainer

            webRequest.Headers.Clear()
            webRequest.ProtocolVersion = HttpVersion.Version11
            webRequest.Accept = "application/xml, text/xml, */*; q=0.01"
            'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.111 Safari/537.36" ' "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chorme/47.0.2526.106 safari/537.36";
            webRequest.ContentType = "text/xml; charset=UTF-8" '"txt/xml;charset=""UTF-8""";
            webRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate")
            webRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "es-ES,es;q=0.8")
            webRequest.Headers.Add("SOAPAction", ms_AccionURL & ls_Accion)
            webRequest.Method = "POST"
            webRequest.CookieContainer = oCookies
            webRequest.AutomaticDecompression = DecompressionMethods.Deflate Or DecompressionMethods.GZip
            Dim encoding As UTF8Encoding = New UTF8Encoding()

            Dim ls_Envelope As String = lr_GenerarEnvelopeSOAP(ls_Accion, lo_Datos)
            Dim bytes() As Byte = encoding.GetBytes(ls_Envelope)
            webRequest.ContentLength = bytes.Length

            Using responseStream As Stream = webRequest.GetRequestStream()
                If responseStream IsNot Nothing Then
                    Using streamreader As New StreamWriter(responseStream)
                        streamreader.Write(ls_Envelope)
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

    Private Function lr_GenerarHeaderSOAP() As String
        Dim lo_SoapHeader As New StringBuilder
        Dim ls_User As String = "user"
        Dim ls_Password As String = "P@ssw0rdSe4vic3"

        With lo_SoapHeader
            .AppendLine("<b_Credenciales xmlns=""http://tempuri.org/"">")
            .AppendLine("<Username>" & ls_User & "</Username>")
            .AppendLine("<Password>" & ls_Password & "</Password>")
            .AppendLine("</b_Credenciales>")
        End With
        Return lo_SoapHeader.ToString
    End Function

    Private Function lr_GenerarBodySOAP(ByVal lo_Datos As Dictionary(Of String, String)) As String
        Dim lo_SoapBody As New StringBuilder
        If lo_Datos IsNot Nothing Then
            For Each _kv As KeyValuePair(Of String, String) In lo_Datos
                lo_SoapBody.AppendLine("<" & _kv.Key & ">" & _kv.Value & "</" & _kv.Key & ">")
            Next
        End If
        Return lo_SoapBody.ToString
    End Function

    Private Function lr_GenerarEnvelopeSOAP(ByVal ls_Accion As String, Optional ByVal lo_Datos As Dictionary(Of String, String) = Nothing) As String
        Dim lo_SoapHeader As String = lr_GenerarHeaderSOAP()

        Dim lo_SoapBody As New StringBuilder
        lo_SoapBody.AppendLine(lr_GenerarBodySOAP(lo_Datos))

        Select Case ls_Accion
            Case "ValidarEmail"
                With lo_SoapBody
                    .Insert(0, "<ValidarEmail xmlns=""http://tempuri.org/"">")
                    .AppendLine("</ValidarEmail>")
                End With
        End Select

        Return lr_GenerarTemplateSOAP(lo_SoapHeader, lo_SoapBody)
    End Function

    Private Function lr_GenerarTemplateSOAP(ByVal ls_SoapHeader As String, ByVal ls_SoapBody As StringBuilder) As String
        Dim lo_SoapBuilder As New StringBuilder
        With lo_SoapBuilder
            .AppendLine("<?xml version=""1.0"" encoding=""utf-8""?>")
            .AppendLine("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>")
            .AppendLine("<soap:Header>")
            .Append(ls_SoapHeader)
            .AppendLine("</soap:Header>")
            .AppendLine("<soap:Body>")
            .Append(ls_SoapBody)
            .AppendLine("</soap:Body>")
            .AppendLine("</soap:Envelope>")
        End With
        Return lo_SoapBuilder.ToString
    End Function

    Function lr_RespuestaToDataTable(ByVal cadXml As String, ByVal tag As String) As Data.DataTable
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
            End If
        End If

        Dim i As Integer = 0

        Dim dt As New Data.DataTable
        dt.Columns.Add("cod")
        dt.Columns.Add("des")
        For i = 0 To param.ChildNodes.Count - 1
            dt.Rows.Add(param.ChildNodes(i).Item("valor").InnerText.ToString, param.ChildNodes(i).Item("descripcion").InnerText.ToString)
        Next

        Return dt
    End Function
End Class
