Imports System.Net
Imports System.Xml
Imports System.IO
Imports System.Collections.Generic

Partial Class Egresados_frmListaOfertasNeuvoo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            CargaPaises()
        End If
    End Sub

    Sub CargaPaises()
        Dim obj As New ClsConectarDatos
        Dim dtPais As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        ''#Escuela
        dtPais = obj.TraerDataTable("ALUMNI_ListarPaises")
        dpPais.DataSource = dtPais
        dpPais.DataTextField = "nombre"
        dpPais.DataValueField = "codigo"
        dpPais.DataBind()
        dpPais.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Public Function UrlServices(ByVal UrlService As String) As List(Of ResultType)
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

        Dim bytes As Byte() = encoding.GetBytes("")
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

        Response__1.LoadXml(responseResult)
        '   Response__1.Save("d:\data.xml")
        'Dim m_nodelist As XmlNodeList

        Dim __mfx As XmlNode = Response__1.DocumentElement.SelectSingleNode("/response/results")
        Dim catXml As String = __mfx.InnerXml
        Dim Result As New List(Of ResultType)

        For Each node As XmlElement In __mfx
            Dim root As New XmlDocument
            Dim ChilNode As String = node.InnerXml
            Dim __res As New ResultType
            Dim CadXml As String = "<xml>" & ChilNode & "</xml>"
            root.LoadXml(CadXml)

            Dim __jobkey As XmlNode = root.DocumentElement.SelectSingleNode("/xml/jobkey")
            Dim __jobtitle As XmlNode = root.DocumentElement.SelectSingleNode("/xml/jobtitle")
            Dim __company As XmlNode = root.DocumentElement.SelectSingleNode("/xml/company")
            Dim __city As XmlNode = root.DocumentElement.SelectSingleNode("/xml/city")
            Dim __state As XmlNode = root.DocumentElement.SelectSingleNode("/xml/state")
            Dim __country As XmlNode = root.DocumentElement.SelectSingleNode("/xml/country")
            Dim __formattedLocation As XmlNode = root.DocumentElement.SelectSingleNode("/xml/formattedLocation")
            Dim __source As XmlNode = root.DocumentElement.SelectSingleNode("/xml/source")
            Dim __datetime As XmlNode = root.DocumentElement.SelectSingleNode("/xml/date")
            Dim __snippet As XmlNode = root.DocumentElement.SelectSingleNode("/xml/snippet")
            Dim __url As XmlNode = root.DocumentElement.SelectSingleNode("/xml/url")
            Dim __logo As XmlNode = root.DocumentElement.SelectSingleNode("/xml/logo")

            __res.jobkey = __jobkey.InnerText
            __res.jobtitle = __jobtitle.InnerText
            __res.company = __company.InnerText
            __res.city = __city.InnerText
            __res.state = __state.InnerText
            __res.country = __country.InnerText
            __res.formattedLocation = __formattedLocation.InnerText
            __res.source = __source.InnerText
            __res.datetime = __datetime.InnerText.Substring(0, 25)
            __res.snippet = __snippet.InnerText
            __res.url = __url.InnerText
            __res.logo = __logo.InnerText

            Result.Add(__res)
        Next

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        For Each r As ResultType In Result
            '    dgv_listado.Rows.Add(r.jobkey, r.jobtitle, r.company, r.city, r.state, r.country, r.formattedLocation, r.source, r.datetime, r.snippet, r.url, r.logo)
            obj.Ejecutar("ALUMNI_AgregarOfertasAlumni", r.jobkey, r.jobtitle, r.company, r.city, r.state, r.country, r.formattedLocation, r.source, r.datetime, r.snippet, r.url, r.logo)
        Next
        obj.CerrarConexion()
        obj = Nothing

        Return Result
    End Function

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Call wf_buscar()
    End Sub

    Sub wf_buscar()
        '   UrlServices("", "", "") 
        Dim ls_pais As String = dpPais.SelectedValue.ToString()

        Dim ls_titulo As String = txt_tituloTrabajo.Text
        Dim ls_ubicacion As String = txt_ubicacion.Text.Trim

        If ls_ubicacion = "" Then
            If dpPais.SelectedItem.ToString() <> "TODOS" Then
                ls_ubicacion = dpPais.SelectedItem.ToString()
            End If
        Else
            ls_ubicacion = ls_ubicacion & "+" & ls_pais
        End If

        Dim ls_limite As String = "100"
        Dim ls_cadena As String = "http://api.neuvoo.com/apisearch?publisher=fe7ca91&q=" & ls_titulo & "&l=" & ls_ubicacion & "&start=0&limit=" & ls_limite & "&userip=1.2.3.4&useragent=Mozilla/%2F4.0%28Firefox%29&v=2"

        gvwNeuvoo.DataSource = UrlServices(ls_cadena)
        gvwNeuvoo.DataBind()
        gvwNeuvoo.Visible = True

        If gvwNeuvoo.Rows.Count = 0 Then
            lbl_msgbox.Text = "No se encontraron coincidencias para la Oferta Laboral: " & txt_tituloTrabajo.Text.Trim
        Else
            'Response.Write("<script>alert('MAYOR A CERO')</script>")
            lbl_msgbox.Text = ""
        End If
    End Sub

    Protected Sub gvwNeuvoo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwNeuvoo.PageIndexChanging
        gvwNeuvoo.PageIndex = e.NewPageIndex()
        Call wf_buscar()
    End Sub


    Protected Sub gvwNeuvoo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwNeuvoo.SelectedIndexChanged
        Me.Hdjobkey.Value = Me.gvwNeuvoo.DataKeys.Item(Me.gvwNeuvoo.SelectedIndex).Values(0)
        lnkDetalles_Click(sender, e)
        If (gvwNeuvoo.Rows.Count <> 0) Then
            Me.fradetalle.Visible = True
        End If
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina
    End Sub

    Protected Sub lnkDetalles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDetalles.Click
        If (Me.Hdjobkey.Value <> "") Then
             EnviarAPagina("frmListaOfertasNeuvooDetalle.aspx?ofe=" & Me.Hdjobkey.Value)
        End If
    End Sub

    Protected Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If (Me.gvwNeuvoo.SelectedDataKey IsNot Nothing) Then
            Response.Redirect("frmListaOfertasNeuvooModificar.aspx?of=" & Me.gvwNeuvoo.SelectedDataKey.Values(0) & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        Else
            Response.Write("<script>alert('Debe seleccionar una oferta laboral')</script>")
        End If
    End Sub
End Class
