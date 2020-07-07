Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Xml

Partial Class DataJson_Poa_Movimientos_POA
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        If Session("id_per") = "" Then
            Data.Add("msje", "ErrorSession")
            Data.Add("lnk", "../../../sinacceso.html")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Else
            Try

                Dim tipo As String = ""
                'Dim k As String = Request("k")
                Dim obj As New ClsCRM
                Dim arr As New List(Of String)
                Dim codigo_per As String = Session("id_per")
                Dim action As String = ""

                Dim codigo_acp As Integer
                action = Request.Form("action").ToString()

                Select Case action
                    Case "Upload"
                        codigo_acp = obj.DecrytedString64(Request.Form("cod_acp"))
                        SubirArchivo(codigo_acp)
                    Case "Cerrar"
                        codigo_acp = obj.DecrytedString64(Request.Form("cod_acp"))
                        CerrarProyecto(codigo_acp)
                    Case "Ver"
                        codigo_acp = obj.DecrytedString64(Request.Form("cod_acp"))
                        ListarProyectosFiles(codigo_acp)
                    Case "Download"
                        DescargarArchivo()
                End Select
            Catch ex As Exception
                Data.Add("msje", False)
                Data.Add("msje", ex.InnerException.ToString)
                JSONresult = serializer.Serialize(Data)
                Response.Write(JSONresult)
            End Try
        End If
    End Sub


    Sub SubirArchivo(ByVal codigo_acp As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        ' Dim List As New List(Of Dictionary(Of String, Object))()
        Dim Data As New Dictionary(Of String, Object)()
        Dim list1 As New List(Of Dictionary(Of String, Object))()

        Try
            Dim obj As New ClsCRM
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")

            'Dim NroRend As String = Request.Form("hdn")
            Dim Fecha As String = DateTime.Now.ToString("dd/MM/yyyy")
            Dim Input(post.ContentLength) As Byte
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)
            Dim Usuario As String = usuario_session

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo_acp)
            list.Add("TablaId", "5")
            list.Add("NroOperacion", codigo_acp)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "DSISTEMAS13")
            'list.Add("Ip", "10.10.1.5")
            list.Add("Ip", "")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            ' Response.Write(result)
            'Data.Add("msje", result)
            Data.Add("msje", "OK")
            list1.Add(Data)
            JSONresult = serializer.Serialize(list1)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", ex.Message)
            list1.Add(Data)
            JSONresult = serializer.Serialize(list1)
            Response.Write(JSONresult)
        End Try

    End Sub

    Sub DescargarArchivo()
        Try
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)
            Dim crm As New ClsCRM
            Dim IdArchivo As String = crm.DecrytedString64(Request.Form("IdArchivo"))

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 5, IdArchivo)
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", usuario_session)
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
            Dim imagen As String = ResultFile(result)
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen)
                Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                resultData.Add(Data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarProyectosFiles(ByVal cod As Integer)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim crm As New ClsCRM
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 1, 5, cod)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If i = 0 Then
                        data.Add("sw", True)
                        data.Add("msje", "OK")
                    End If
                    data.Add("cCod", crm.EncrytedString64(tb.Rows(i).Item("IdArchivo")))
                    data.Add("nArchivo", tb.Rows(i).Item("NombreArchivo"))
                    data.Add("nExtension", ExtensionArchivo(tb.Rows(i).Item("Extencion").ToString))
                    data.Add("nExt", tb.Rows(i).Item("Extencion").ToString)
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim data As New Dictionary(Of String, Object)()
            data.Add("sw", True)
            data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub CerrarProyecto(ByVal codigo_acp As Integer)
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Try
            Dim objcrm As New ClsCRM
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("[dbo].[POA_CerrarProyecto]", codigo_acp)
            obj.CerrarConexion()

            data.Add("msje", "Proyecto Cerrado Correctamente")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            'data.Add("msje", ex.Message)
            data.Add("msje", "Proyecto No se Pudo Cerrar Correctamente")
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try

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

    Private Function ExtensionArchivo(ByVal ext As String) As String
        Dim extencion As String = ""
        Select Case ext.Trim.ToString
            Case ".txt"
                'extencion = "text/plain"
                extencion = "fa fa-file-text-o"
            Case ".doc"
            Case ".docx"

                'extencion = "application/ms-word"
                extencion = "fa fa-file-word-o"
            Case ".xls"
            Case ".xlsx"
                'extencion = "application/vnd.ms-excel"
                extencion = "fa fa-file-excel-o"
            Case ".gif"
                'extencion = "image/gif"
                extencion = "fa fa-file-image-o"
            Case ".jpg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".jpeg"
                'extencion = "image/jpeg"
                extencion = "fa fa-file-image-o"
            Case ".bmp"
                'extencion = "image/bmp"
                extencion = "fa fa-file-image-o"
            Case ".wav"
                'extencion = "audio/wav"
                extencion = "fa fa-file-audio-o"
            Case ".ppt"
                'extencion = "application/mspowerpoint"
                extencion = "fa fa-file-powerpoint-o"
            Case ".dwg"
                'extencion = "image/vnd.dwg"
                extencion = "fa fa-file-code-o"
            Case ".zip"
                extencion = "fa fa-file-archive-o"
            Case ".rar"
                extencion = "fa fa-file-archive-o"
            Case ".pdf"
                extencion = "fa fa-file-pdf-o"
            Case Else
                'extencion = "application/octet-stream"
                extencion = ""
        End Select
        Return extencion
    End Function


End Class
