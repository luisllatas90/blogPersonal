Partial Class DescargarArchivo
    Inherits System.Web.UI.Page

    Private idTabla As Integer = 22
    Private token As String = "YAXVXFQACX"
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id As String = Request.QueryString("id")

            Call mt_download(id)
        End If
    End Sub

    Private Sub mt_download(ByVal id As String)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Generic.Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim tb As New Data.DataTable
            Dim usuario As String = "USAT\ESAAVEDRA" 'Session("perlogin")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("SilaboCurso_listar", -1, id, 1)
            obj.CerrarConexion()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                obj.AbrirConexion()
                tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, idTabla, dt.Rows(0).Item("codigo_sil"), token)
                obj.CerrarConexion()

                If tb IsNot Nothing AndAlso tb.Rows.Count > 0 Then
                    list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
                    list.Add("Usuario", usuario)
                    list.Add("Token", token)
                    Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                    Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
                    Dim imagen As String = fc_ResultFile(result)
                    Dim bytes As Byte() = Convert.FromBase64String(imagen)

                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" + tb.Rows(0).Item("NombreArchivo").ToString)
                    Response.AppendHeader("Content-Length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()
                    'Response.Write(envelope)
                Else
                    Response.Write("<script type='text/JavaScript'>alert('El archivo del sílabo no existe en el servidor'); window.close();</script>")
                End If
            Else
                Response.Write("<script type='text/JavaScript'>alert('No existe silabo para este curso'); window.close();</script>")
            End If
        Catch ex As Exception
            Response.Write("<script type='text/JavaScript'>alert('" & ex.Message & " -- " & ex.StackTrace & "'); window.close();</script>")
        End Try
    End Sub

    Private Function fc_ResultFile(ByVal cadXml As String) As String
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
