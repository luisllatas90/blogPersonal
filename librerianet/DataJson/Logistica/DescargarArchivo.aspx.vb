Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Partial Class DataJson_Logistica_DescargarArchivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("perlogin") <> "" Then
                Dim cif As New ClsLogistica
                'Dim IdArchivo As String = cif.DecrytedString64(Request("Id"))
                Dim IdArchivo As String = Request("Id")
                'Dim wsCloud As New ClsArchivosCompartidos
                Dim wsCloud As New ClsArchivosCompartidosV2
                Dim list As New Dictionary(Of String, String)

                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim cn As New clsaccesodatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 6, IdArchivo, "BQ2PQUV34J")
                obj.CerrarConexion()

                list.Add("IdArchivo", IdArchivo)
                list.Add("Usuario", "USAT\ESAAVEDRA")
                list.Add("Token", "BQ2PQUV34J")
                Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))
                Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))
                'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))

                'Dim imagen As String = wsCloud.ResultFile(result)
                Dim imagen As String = fc_ResultFile(result)
                '  Response.Write(tb.Rows(0).Item("NombreArchivo"))

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
                        Case Else
                            extencion = "application/octet-stream"
                    End Select
                    'Response.Write(Session("perlogin"))

                    Dim bytes As Byte() = Convert.FromBase64String(imagen)
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = extencion
                    Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo"))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)

                    Response.End()
                End If
            Else
                Response.Write("<alert>La sessión ha finalizado</alert>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' 20181210 ENevado
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
