Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization

Partial Class administrativo_tramite_DescargarArchivo
    Inherits System.Web.UI.Page

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("perlogin") Is Nothing Then
            Try
                Dim IdArchivo As String = Request("Id")
                Dim token As String = "FHAIWBVE36"
                ' Response.Write(IdArchivo)
                Dim wsCloud As New ClsArchivosCompartidosV2
                Dim list As New Dictionary(Of String, String)

                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim cn As New clsaccesodatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 1, IdArchivo, token)
                obj.CerrarConexion()

                list.Add("IdArchivo", IdArchivo)
                list.Add("Usuario", Session("perlogin"))
                list.Add("Token", token)
                Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))
                Dim imagen As String = wsCloud.ResultFile(result)
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

                    ' Response.Write(extencion)

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
            Catch ex As Exception
                Response.Write(ex.Message & " -- " & ex.StackTrace & "<br>")
            End Try
        Else
            Response.Write("<alert>La sessión ha finalizado</alert>")
        End If

    End Sub
End Class
