Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualBasic
Imports System
Imports System.Security.AccessControl
Partial Class DataJson_Sunedo_GestionDocumetacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        ' Response.Write("sdfsd" + FPost)
        Select Case FPost
            Case "Indicador"
                ListarIndicador()
            Case "Medio"
                ListarMedioVerificacion()
            Case "Periodo"
                ListarPeriodo()
            Case "Responsable"
                ListarResponsable()
            Case "PeriodoContable"
                PeriodoContable()
            Case "Registrar"
                RegistrarDocumento()
            Case "Version"
                ListarMatrizConocimiento()
            Case "TipDoc"
                TipoDocumento()
            Case "VersionDoc"

                ListarVersiones()
            Case "DescargarArchivo"
                DescargarArchivo()
            Case "Delete"
                DeletFile()
        End Select

    End Sub
    Sub ListarIndicador()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.REP_USPINDICADOR_LIST")
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Id"))
            Data.Add("Label", tb.Rows(i).Item("Nombre"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarMedioVerificacion()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim Codigo_ind As Integer = Request.Form("Id")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.REP_USPRepMedioVerificacion_LIST", Codigo_ind)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Id"))
            Data.Add("Label", tb.Rows(i).Item("Nombre"))
            ' Data.Add("NombreFre", tb.Rows(i).Item("Frecuencia"))
            'Data.Add("CodigoFre", tb.Rows(i).Item("codigo_fre"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarPeriodo()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigo_ver As Integer = Request.Form("Id")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRE_USPPERIODO_LIST", codigo_ver)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Id"))
            Data.Add("Label", tb.Rows(i).Item("Nombre"))
            Data.Add("NombreFre", tb.Rows(i).Item("Frecuencia"))
            Data.Add("CodigoFre", tb.Rows(i).Item("CodigoFre"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarResponsable()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigo_ver As Integer = Request.Form("Id")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.REP_USPRESPONSABLE_LIST", codigo_ver)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Id"))
            Data.Add("Label", tb.Rows(i).Item("Responsable"))  
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub TipoDocumento()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigo_doc As Integer = Request.Form("Id")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.REP_USPDocumento_LIST", codigo_doc)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Codigo"))
            Data.Add("Label", tb.Rows(i).Item("Nombre"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub PeriodoContable()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("dbo.CONTA_PROCESOCONTABLE")
        obj.CerrarConexion()
        ' Response.Write(tb.Rows.Count)
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("codigo_pct"))
            Data.Add("Label", tb.Rows(i).Item("descripcion_pct"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub RegistrarDocumento()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
        Dim CodigoDoc As Integer = Request.Form("CodigoDoc")
        Dim CodigoPer As Integer = Request.Form("CodigoPer")
        Dim CodigoPct As Integer = Request.Form("CodigoPct")
        Dim CodigoPeri As Integer = Request.Form("CodigoPeri")
        Dim NombreArchivo As String = System.IO.Path.GetFileName(post.FileName)
        Dim Usuario As String = Session("perlogin")
        Dim Input(post.ContentLength) As Byte

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' Response.Write(CodigoRend)
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.REP_USPRepDocumentoVersion_ins", CodigoDoc, NombreArchivo, "1", CodigoPeri, CodigoPer, CodigoPct)
        obj.CerrarConexion()
        Dim status As String
        Dim codigo As Integer
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            status = tb.Rows(i).Item("Status")
            codigo = tb.Rows(i).Item("Code")
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            List.Add(Data)
        Next


        If status = "OK" Then
            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos

            Dim listData As New Dictionary(Of String, String)
            listData.Add("Fecha", "27/10/2017")
            listData.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            listData.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            listData.Add("TransaccionId", codigo)
            listData.Add("TablaId", "8")
            listData.Add("NroOperacion", codigo)
            listData.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            listData.Add("Usuario", Usuario)
            listData.Add("Equipo", "dsistemas09")
            listData.Add("Ip", "")
            listData.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(listData)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)

            ' Response.Write(envelope)

            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Usuario)
        End If

        ' Dim b As New BinaryReader(post.InputStream)
        '  Dim by() As Byte = b.ReadByte(post.ContentLength)

        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub ListarMatrizConocimiento()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim CodigoInd As Integer = Request.Form("CodigoInd")
        Dim CodigoPct As Integer = Request.Form("CodigoPct")
        Dim codigo_per As Integer = Session("id_per")
        Dim fecha_fac As String = Request.Form("Fecha")
        Dim ctf As Integer = Request.Form("ctf")

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.REP_USPVERSIONDOCUMENTO_LIST", CodigoInd, CodigoPct, codigo_per, ctf)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()


        For Each row As Data.DataRow In tb.Rows
            Dim Data As New Dictionary(Of String, Object)()
            For Each col As Data.DataColumn In tb.Columns
                Data.Add(col.ColumnName, row(col))
            Next
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarVersiones()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigos As String = Request.Form("codigos")
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()


        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM

        tb = obj.TraerDataTable("dbo.REP_USPVERSIONES_LIST", codigos)
        obj.CerrarConexion()


        For Each row As Data.DataRow In tb.Rows
            Dim Data As New Dictionary(Of String, Object)()
            For Each col As Data.DataColumn In tb.Columns
                Data.Add(col.ColumnName, row(col))
            Next
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'FechaVar
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

 
    Private Sub DescargarArchivo()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim resultData As New List(Of Dictionary(Of String, Object))()

        If Not Session("perlogin") Is Nothing Then
            Dim cif As New ClsCRM
            Dim IdArchivo As String
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)
            For Each s As String In Request.Form.AllKeys
                IdArchivo = Request.Form(s)
                '    Response.Write(s)
            Next
            'Response.Write(Request.Form("Codigo"))
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 8, IdArchivo)
            obj.CerrarConexion()
            Dim Usuario As String = Session("perlogin")

            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)'produccion
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)'Qa
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario) 'Desarrollo
            Dim imagen As String = ResultFile(result)
            Dim NombreAr As String
            Dim extencion As String
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "OK")
                Data.Add("Message", "Descargando archivos")
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                Data.Add("Rep", "/Repositorio/files/")

                NombreAr = tb.Rows(i).Item("NombreArchivo")
                extencion = tb.Rows(i).Item("Extencion")
                Data.Add("Host", "http://serverdev/campusvirtual/ArchivosCompartidos/files/" & NombreAr)
                '  Data.Add("Host", "../../../ArchivosCompartidos/files/" & NombreAr)

                resultData.Add(Data)
            Next

            Dim file As String = Server.MapPath("../Repositorio/files/" & NombreAr )
            Dim tempBytes As Byte() = Convert.FromBase64String(imagen)
            Using fs As New FileStream(file, FileMode.Create)
                fs.Write(tempBytes, 0, tempBytes.Length)
                fs.Close()
            End Using

            '  Response.Write(file)
        Else
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir al Menu Principal/Tesoreria/Movimiento/Rendicion de gastos/Rendiciones Pendientes </h4>")
            Data.Add("File", "")
            Data.Add("Nombre", "")
            Data.Add("Extencion", "")
            Data.Add("Host", "")
            resultData.Add(Data)
        End If
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(resultData)
        Response.Write(JSONresult)
    End Sub
    Sub DeletFile()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim resultData As New List(Of Dictionary(Of String, Object))()
        Dim fullFilePath As String = Server.MapPath("../Repositorio/files/" & Request.Form("name"))
        System.IO.File.Delete(fullFilePath)
        Dim Data As New Dictionary(Of String, Object)()

        If System.IO.File.Exists(fullFilePath) Then
            Data.Add("Status", "ERROR")
            Data.Add("Message", "No se elimino el registro")
        Else
            Data.Add("Status", "OK")
            Data.Add("Message", "Registro Eliminado")
        End If
        resultData.Add(Data)
        serializer.MaxJsonLength = Int32.MaxValue
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
