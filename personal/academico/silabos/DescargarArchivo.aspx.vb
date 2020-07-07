Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization

Partial Class academico_silabos_DescargarArchivo
    Inherits System.Web.UI.Page
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("perlogin") Is Nothing Then
            Try


                Dim IdArchivo As String = Request("Id")
                Dim idTabla As Integer = 22
                Dim token As String = "YAXVXFQACX"
                Dim wsCloud As New ClsArchivosCompartidosV2
                Dim list As New Dictionary(Of String, String)
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim usuario As String = Session("perlogin")

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, IdArchivo, token)
                obj.CerrarConexion()
                If tb.Rows.Count = 0 Then Throw New Exception("¡ Archivo no encontrado !")

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
                    Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()
                End If
            Catch ex As Exception
                Response.Write(ex.Message & " -- " & ex.StackTrace & "<br>")
            End Try
        Else
            Response.Write("<alert>La sessión ha finalizado</alert>")
        End If

    End Sub
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
End Class
