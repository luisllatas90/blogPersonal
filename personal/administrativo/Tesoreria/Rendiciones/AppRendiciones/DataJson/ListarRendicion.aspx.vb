Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization

Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_DataJson_ListarRendicion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim codigo_per As String
        codigo_per = Session("id_per")
        Dim codigoEgr As Integer = Request.QueryString("Id")
        Dim funcion As String = Request.QueryString("Funcion")
        Dim form As String = Request.Form("id")
        ' Response.Write("Hola:" + form)

        Select Case funcion
            Case "RendDetalle"
                RendirDetalle(codigoEgr, codigo_per)
            Case "RendImportes"
                RendImportes(codigoEgr)
            Case "ListDetalle"
                DetalleRendicion(codigoEgr)
            Case "Finalizar"
                FinalizarRendicion(codigo_per)
            Case "Grabar"
        End Select

        ' Response.Write(Request.Form.Count)
        ' Response.Write(Request.Form("Id"))

        Dim FPost As String = Request.Form("Funcion")
        Select Case FPost
            Case "Finalizar"
                FinalizarRendicion(codigo_per)
            Case "Eliminar"
                ElimianrDetalleRendicion(codigo_per)
            Case "UpFile"
                SubirArchivo()
            Case "Adjunto"
                ListarAdjuntos()
            Case "ArchivoCompartido"
                ConsultarArchivoCompartido()
            Case "DescargarArchivo"
                DescargarArchivo()
        End Select
        If Request.Form("Id") = "Grabar" Then
            Grabar(codigo_per)
        End If

        '  Response.Write("Hola:"+codigo_per)


    End Sub
    Sub RendirDetalle(ByVal codigoEgr As Integer, ByVal codigo_usu As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR_DETALLE", codigoEgr, codigo_usu)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            'Response.Write("----:" + tb.Rows(i).Item("Id"))
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Rubro", tb.Rows(i).Item("Rubro"))
            Data.Add("Importe", String.Format("{0:#,##0.##}", CType(tb.Rows(i).Item("Importe"), Decimal)))
            Data.Add("Centrocostos", tb.Rows(i).Item("Centrocostos"))
            Data.Add("EstadoRendicion", tb.Rows(i).Item("EstadoRendicion"))
            Data.Add("Observacion", tb.Rows(i).Item("Observacion"))
            Data.Add("ExigirRendicion", tb.Rows(i).Item("ExigirRendicion"))
            Data.Add("CodigoRend", tb.Rows(i).Item("CodigoRend"))
            Data.Add("NumeracionAnualRend", tb.Rows(i).Item("NumeracionAnualRend"))
            Data.Add("Finalizarend", tb.Rows(i).Item("Finalizarend"))
            Data.Add("FechaRend", tb.Rows(i).Item("FechaRend"))

            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ''' <summary>
    ''' Se Obtene el listado de los comprobantes registrados pertenecientes a una rendicion.
    ''' </summary>
    ''' <param name="codigoDren"></param>
    ''' <remarks></remarks>
    Sub DetalleRendicion(ByVal codigoRend As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_RendDetalleRendicion", codigoRend)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim cif As New ClsCRM
        For i As Integer = 0 To tb.Rows.Count - 1
            'Response.Write("----:" + tb.Rows(i).Item("Id"))
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Codigo", cif.EncrytedString64(tb.Rows(i).Item("Codigo")))
            Data.Add("TipoDoc", tb.Rows(i).Item("TipoDoc"))
            Data.Add("SerieNumero", tb.Rows(i).Item("SerieNumero"))
            Data.Add("Descripcion", tb.Rows(i).Item("Descripcion"))
            Data.Add("Institucion", tb.Rows(i).Item("Institucion"))
            Data.Add("Fecha", tb.Rows(i).Item("Fecha"))
            Data.Add("Importe", tb.Rows(i).Item("Importe"))
            Data.Add("Estado", tb.Rows(i).Item("Estado"))
            Data.Add("Finaliza", tb.Rows(i).Item("Finaliza"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ''' <summary>
    ''' Obtiene los Importes de la rendicion
    ''' </summary>
    ''' <param name="CodigoRend"></param>
    ''' <remarks></remarks>
    Public Sub RendImportes(ByVal CodigoRend As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write(CodigoRend)
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_TOTALES_RENDICION_BY_LIST", CodigoRend)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            'Response.Write("----:" + tb.Rows(i).Item("Id"))
            Dim Data As New Dictionary(Of String, Object)()

            Data.Add("SaldoRendir", tb.Rows(i).Item("SaldoRendir"))
            Data.Add("MontoDevuelto", tb.Rows(i).Item("MontoDevuelto"))
            Data.Add("NumeracionAnualRend", tb.Rows(i).Item("NumeracionAnualRend"))
            Data.Add("Importe", tb.Rows(i).Item("Importe"))
            Data.Add("MontoRendido", tb.Rows(i).Item("MontoRendido"))
            Data.Add("NumeracionAnual", tb.Rows(i).Item("NumeracionAnual"))
            Data.Add("TipDoc", tb.Rows(i).Item("TipDoc"))
            Data.Add("Rubro", tb.Rows(i).Item("Rubro"))
            Data.Add("Moneda", tb.Rows(i).Item("Moneda"))
            Data.Add("FechaEgr", tb.Rows(i).Item("FechaEgr"))
            Data.Add("Usuario", tb.Rows(i).Item("Usuario"))
            Data.Add("Cliente", tb.Rows(i).Item("Cliente"))
            Data.Add("SerieNumero", tb.Rows(i).Item("SerieNumero"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub Grabar(ByVal CodigoUsd As Integer)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            If Not Session("perlogin") Is Nothing Then





                ' Response.Write(Request.Form("OpPersona"))
                Dim NroRendicion As Integer = Request.Form("NroRendicion")
                Dim ruc As String = Request.Form("txtNroRuc")
                Dim Rs As String = Request.Form("txtRazonSocial")
                Dim direccion As String = Request.Form("txtDireccion")
                Dim TipoDoc As Integer = Request.Form("listDocumento")
                Dim serie As String = Request.Form("txtSerie")
                Dim Numero As String = Request.Form("txtNumero")
                Dim Fecha As Date = Request.Form("txtFecha")
                Dim Obs As String = Request.Form("txtObservacion")
                Dim Importe As Decimal = Request.Form("txtImporte")
                Dim CodigoPso As Integer = IIf(String.IsNullOrEmpty(Request.Form("OpPersona")), "0", Request.Form("OpPersona"))

                Dim Distrito As String = Request.Form("Distrito")
                Dim Provincia As String = Request.Form("Provincia")
                Dim Departamento As String = Request.Form("Departamento")
                'For Each s As String In Request.Files.AllKeys
                '    Dim file As HttpPostedFile = Request.Files(s)

                '    Response.Write(s & ": " & file.FileName & "<br />")
                'Next
                ''Dim f As new HttpPostedFile = Request.Files("fileData")

                ' Response.Write(IIF(String.IsNullOrEmpty(Request.Form("OpPersona")), "0", Request.Form("OpPersona")))

                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim cn As New clsaccesodatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ' Response.Write(CodigoRend)
                ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
                tb = obj.TraerDataTable("dbo.UPS_RendDetalleRendicion", NroRendicion, CodigoPso, TipoDoc, serie, Numero, Rs, Fecha, Importe, Obs, CodigoUsd, ruc, direccion, Distrito)
                obj.CerrarConexion()

                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim Data As New Dictionary(Of String, Object)()
                    Data.Add("Status", tb.Rows(i).Item("Status"))
                    Data.Add("Message", tb.Rows(i).Item("Message"))
                    Data.Add("Code", tb.Rows(i).Item("Code"))
                    list.Add(Data)
                Next

            Else
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "ERROR")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir al Menu Principal/Tesoreria/Movimiento/Rendicion de gastos/Rendiciones Pendientes </h4>")
                Data.Add("Code", "504")
                list.Add(Data)
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", ex.Message)
            Data.Add("Code", "0")
            list.Add(Data)
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub SubirArchivoBase64()
        Dim bytes() As Byte = File.ReadAllBytes("")
        Dim wsCloud As New ClsArchivosCompartidos
        Dim list As New Dictionary(Of String, String)
        '  Dim list As New List(Of Dictionary(Of String, String))()
        list.Add("Fecha", "03/03/2017")
        '  list.Add("Extencion", Path.GetExtension(txtRuta.Text))
        list.Add("Nombre", "RENDICIONES")
        list.Add("TransaccionId", "8526")
        list.Add("TablaId", "1")
        list.Add("NroOperacion", "025-939")
        '   list.Add("Archivo", Convert.ToBase64String(Files, 0, Files.Length)
        list.Add("Usuario", "JGUEVARA")
        list.Add("Equipo", "DSISTEMAS09")
        list.Add("Ip", "10.10.1.5")
    End Sub
    Sub FinalizarRendicion(ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write("OKS")
        Dim NroRendicion As String = Request.Form("Id")
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_CERRARRENDICION", codigo_per, NroRendicion)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Sub ElimianrDetalleRendicion(ByVal CodigoUss As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write("OKS")
        Dim cif As New ClsCRM
        Dim CodigoDren As String = cif.DecrytedString64(Request.Form("Id"))
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_QUITARDETALLEREND", CodigoDren, CodigoUss)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarAdjuntos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write("OKS")
        Dim CodigoDren As String = Request.Form("Id")
        ' Response.Write(CodigoDren)
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_RENARCHIVOSRENDICION", CodigoDren)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Imagen", tb.Rows(i).Item("Imagen"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub SubirArchivo()
        Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
        Dim codigo_Dren As String = Request.Form("CodigoDren")
        Dim NroRend As String = Request.Form("CodigoRend")
        Dim Fecha As String = Request.Form("Fecha")
        Dim Usuario As String = Session("perlogin")
        Dim Input(post.ContentLength) As Byte
        ' Dim b As New BinaryReader(post.InputStream)
        '  Dim by() As Byte = b.ReadByte(post.ContentLength)


        Dim b As New BinaryReader(post.InputStream)
        Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
        Dim base64 = System.Convert.ToBase64String(binData)

        Dim wsCloud As New ClsArchivosCompartidos
        Dim list As New Dictionary(Of String, String)
        '  Dim list As New List(Of Dictionary(Of String, String))()
        list.Add("Fecha", Fecha)
        list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
        list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
        list.Add("TransaccionId", codigo_Dren)
        list.Add("TablaId", "1")
        list.Add("NroOperacion", NroRend)
        list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
        list.Add("Usuario", Usuario)
        list.Add("Equipo", "")
        list.Add("Ip", "")
        list.Add("param8", Usuario)

        Dim envelope As String = wsCloud.SoapEnvelope(list)
        Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
        'Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)

        Response.Write(result)
        'Dim list As New List(Of Dictionary(Of String, Object))()
        'If Not post Is Nothing Then
        '    Dim fileSavePath As String = Server.MapPath("../Archivosderendicion")
        '    Dim ruta As String
        '    ruta = fileSavePath & "\" & Request.Files("UploadedImage").FileName
        '    post.SaveAs(fileSavePath)
        '    Dim Data As New Dictionary(Of String, Object)()
        '    Data.Add("Status", "OK")
        '    Data.Add("Message", "Archivo Subido")
        '    Data.Add("Code", ruta)
        '    list.Add(Data)
        'Else
        '    Dim Data As New Dictionary(Of String, Object)()
        '    Data.Add("Status", "ERROR")
        '    Data.Add("Message", "No existe ningun archivo")
        '    Data.Add("Code", "502")
        '    list.Add(Data)
        'End If
        ' Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        ' Dim JSONresult As String = ""
        '  JSONresult = serializer.Serialize(list)
        ' Response.Write(JSONresult)

    End Sub

    Private Sub ConsultarArchivoCompartido()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write("OKS")
        Dim cif As New ClsCRM
        Dim CodigoDren As String = cif.DecrytedString64(Request.Form("Id"))

        If CodigoDren Is Nothing Then
            CodigoDren = 0
        End If

        ' Response.Write(CodigoDren)
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 1, 1, CodigoDren)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("IdArchivo", cif.EncrytedString64(tb.Rows(i).Item("IdArchivo")))
            Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
            Data.Add("RutaArchivo", tb.Rows(i).Item("RutaArchivo"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub DescargarArchivo()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim resultData As New List(Of Dictionary(Of String, Object))()

        If Not Session("perlogin") Is Nothing Then
            Dim cif As New ClsCRM
            Dim IdArchivo As String = String.Empty
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            For Each s As String In Request.Form.AllKeys
                IdArchivo = cif.DecrytedString64(Request.Form(s))
            Next
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 1, IdArchivo)
            obj.CerrarConexion()
            Dim Usuario As String = Session("perlogin")

            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
            Dim imagen As String = ResultFile(result)
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "OK")
                Data.Add("Message", "Descargando archivos")
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
        Else
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir al Menu Principal/Tesoreria/Movimiento/Rendicion de gastos/Rendiciones Pendientes </h4>")
            Data.Add("File", "")
            Data.Add("Nombre", "")
            Data.Add("Extencion", "")
            resultData.Add(Data)
        End If
        JSONresult = serializer.Serialize(resultData)
        Response.Write(JSONresult)
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
End Class
