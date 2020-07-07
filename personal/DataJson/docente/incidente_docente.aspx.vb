Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Xml

Partial Class scriptJSON_docente_incidente_docente
    Inherits System.Web.UI.Page
    'Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
    'Dim usuario_session As String = usuario_session_(1)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim tipo As String = ""
            'Dim k As String = Request("k")
            Dim obj As New ClsCRM
            Dim arr As New List(Of String)
            Dim ope As String = ""
            Dim lst As Boolean = False
            Dim action As String = ""
            Dim codpk As Integer = 0
            Dim codigo_per As String = Session("id_per").ToString
            For Each name As String In Request.Form.AllKeys
                Dim value As String = Request.Form(name)

                If name = "process" Then lst = True
                If name = "hdo" Then
                    arr.Add(obj.DecrytedString64(value))
                Else
                    arr.Add(value)
                End If
                ' Data.Add(name, value)
            Next

            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
            '0: hdo
            '1: hdc
            '2: cboSemestreR
            '3: cboDacadR
            '4: cboDocenteR
            '5: txtasunto
            '6: txtdetalle

            'If Not Request.Form("process").ToString Is Nothing Then
            If lst Then
                action = obj.DecrytedString64(Request.Form("process").ToString)
            Else
                action = obj.DecrytedString64(Request.Form("hdo").ToString())
            End If

            'Data.Add("hdo", action)
            'Data.Add("hdc", arr(1).ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)



            Select Case action
                Case "Listar"

                    'If arr(1).ToString = "" Then
                    '    Data.Add("sw", False)
                    '    Data.Add("msje", "Seleccione Semestre Academico")
                    '    Data.Add("obj", "cboSemestre")
                    '    list.Add(Data)
                    '    JSONresult = serializer.Serialize(list)
                    '    Response.Write(JSONresult)
                    '    'ElseIf arr(2).ToString = "" Then
                    '    '    Data.Add("sw", False)
                    '    '    Data.Add("msje", "Seleccione Departamento Academico")
                    '    '    Data.Add("obj", "cboDacad")
                    '    '    list.Add(Data)
                    '    '    JSONresult = serializer.Serialize(list)
                    '    '    Response.Write(JSONresult)
                    '    'ElseIf arr(3).ToString = "" Then
                    '    '    Data.Add("sw", False)
                    '    '    Data.Add("msje", "Seleccione Docente")
                    '    '    Data.Add("obj", "cboDocente")
                    '    '    list.Add(Data)
                    '    '    JSONresult = serializer.Serialize(list)
                    '    '    Response.Write(JSONresult)
                    'Else
                    '    'arr(2) = "0"
                    '    'arr(3) = "0"
                    '    If arr(1).ToString = "" Then arr(1) = "0"
                    '    If arr(2).ToString = "" Then arr(2) = "0"
                    '    If arr(3).ToString = "" Then arr(3) = "0"
                    '    'Data.Add("sw", False)
                    '    'Data.Add("arr2", arr(2).ToString)
                    '    'Data.Add("arr3", arr(3).ToString)
                    '    'list.Add(Data)
                    '    'JSONresult = serializer.Serialize(list)
                    '    'Response.Write(JSONresult)
                    '    ListarIncidenteDocente("L", 0, arr)
                    'End If


                    If arr(1).ToString = "" Then arr(1) = "0"
                    If arr(2).ToString = "" Then arr(2) = "0"
                    If arr(3).ToString = "" Then arr(3) = "0"



                    ListarIncidenteDocente("L", 0, arr, codigo_per)

                Case "Reg"

                    If arr(2).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Seleccione Semestre Academico")
                        Data.Add("obj", "cboSemestreR")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)
                    ElseIf arr(3).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Seleccione Departamento Academico")
                        Data.Add("obj", "cboDacadR")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)
                    ElseIf arr(4).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Seleccione Docente")
                        Data.Add("obj", "cboDocenteR")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)
                    ElseIf arr(5).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Ingrese Asunto")
                        Data.Add("obj", "txtasunto")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)
                    ElseIf arr(6).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Ingrese detalle de la incidencia")
                        Data.Add("obj", "txtdetalle")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)
                    Else
                        'If arr(0).ToString = "Reg" Then
                        '    tipo = "I"
                        'ElseIf arr(1).ToString = "Mod" Then
                        '    tipo = "A"
                        'End If
                        If arr(1).ToString = "" Then
                            codpk = 0
                            tipo = "I"
                        Else
                            codpk = CInt(arr(1).ToString)
                            tipo = "A"
                        End If
                        RegistrarIncidenteDocente(tipo, codpk, codigo_per, arr)
                    End If

                Case "Mod"
                    If arr(1).ToString = "" Then
                        Data.Add("sw", False)
                        Data.Add("msje", "Seleccione Incidencia")
                        Data.Add("obj", "btnListar")
                        list.Add(Data)
                        JSONresult = serializer.Serialize(list)
                        Response.Write(JSONresult)

                    Else
                        If arr(1).ToString <> "" Then
                            codpk = CInt(arr(1).ToString)
                        End If

                        'Data.Add("hdo", action)
                        'Data.Add("hdc", arr(1).ToString)
                        'Data.Add("hdpk", CInt(codpk.ToString))
                        'list.Add(Data)
                        'JSONresult = serializer.Serialize(list)
                        'Response.Write(JSONresult)


                        BuscarIncidenteDocente("L", codpk)
                    End If
                Case "Upload"
                    SubirArchivo()

                Case "ListarFiles"
                    If arr(1).ToString <> "" Then
                        codpk = CInt(arr(1).ToString)
                    End If
                    ListarIncidenteDocenteFiles("1", codpk)

                Case "Download"
                    DescargarArchivo()

            End Select

            'Data.Add("msje", Request)
            'JSONresult = serializer.Serialize(sr)
            'Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", False)
            Data.Add("msje", ex.InnerException.ToString)
            JSONresult = serializer.Serialize(Data)
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
    Sub DescargarArchivo()
        Try
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)

            'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim JSONresult As String = ""
            'Dim Data As New Dictionary(Of String, Object)()
            'Data.Add("msje", usuario_session)
            'JSONresult = serializer.Serialize(Data)
            'Response.Write(JSONresult)

            Dim IdArchivo As String = Request.Form("hdc")
            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 4, IdArchivo)
            obj.CerrarConexion()

            Dim resultData As New List(Of Dictionary(Of String, Object))()
            ' Response.Write(IdArchivo)
            list.Add("IdArchivo", IdArchivo)
            list.Add("Usuario", usuario_session)
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin").ToString)
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


    Sub SubirArchivo()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        ' Dim List As New List(Of Dictionary(Of String, Object))()
        Dim Data As New Dictionary(Of String, Object)()


        Try
            Dim usuario_session_ As String() = Session("perlogin").ToString.Split(New Char() {"\"c})
            Dim usuario_session As String = usuario_session_(1)
            Dim post As HttpPostedFile = HttpContext.Current.Request.Files("UploadedImage")
            Dim codigo_Dren As String = Request.Form("hdc")
            Dim NroRend As String = Request.Form("hdn")
            Dim Fecha As String = "10-04-2017"
            Dim Usuario As String = usuario_session
            Dim Input(post.ContentLength) As Byte

            Dim b As New BinaryReader(post.InputStream)
            Dim binData As Byte() = b.ReadBytes(post.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)



            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(post.FileName))
            list.Add("Nombre", System.IO.Path.GetFileName(post.FileName))
            list.Add("TransaccionId", codigo_Dren)
            list.Add("TablaId", "4")
            list.Add("NroOperacion", NroRend)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "DSISTEMAS09")
            list.Add("Ip", "10.10.1.5")
            list.Add("param8", Usuario)
            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            ' Response.Write(result)
            Data.Add("msje", result)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub RegistrarIncidenteDocente(ByVal tipo As String, ByVal cod As Integer, ByVal codper As String, ByVal arr As List(Of String))
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.incidentes_docente_reg", tipo, cod, CInt(arr(2)), CInt(arr(3)), CInt(arr(4)), arr(5), arr(6), "", 1, CInt(codper))
            obj.CerrarConexion()
            'data.Add("sp", "dbo.incidentes_docente_reg" & " " & tipo & " " & cod & " " & arr(2).ToString & " " & arr(3).ToString & " " & arr(4).ToString & " " & arr(5).ToString & " " & arr(6).ToString & " " & "" & " " & "1" & " " & codper.ToString)
            'data.Add("sp", "dbo.incidentes_docente_reg " & arr(0).ToString & " " & codper)

            If tb.Rows.Count = 1 Then
                If CInt(tb.Rows(0).Item("cod").ToString()) > 0 Then
                    data.Add("sw", True)
                    data.Add("cCod", tb.Rows(0).Item("cod"))
                    data.Add("sNum", tb.Rows(0).Item("num"))
                    data.Add("msje", "Se registr&oacute; satisfactoriamente la incidencia")
                Else
                    data.Add("sw", False)
                    data.Add("msje", "Ha ocurrido problemas al registrar incidencia")
                End If
            Else
                data.Add("sw", False)
                data.Add("msje", "Ha ocurrido problemas al registrar incidencia")
            End If

            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarIncidenteDocenteFiles(ByVal tipo As String, ByVal cod As Integer)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", tipo, 4, cod)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If i = 0 Then data.Add("sw", True)
                    data.Add("cCod", tb.Rows(i).Item("IdArchivo"))
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

    Private Sub ListarIncidenteDocente(ByVal tipo As String, ByVal cod As Integer, ByVal arr As List(Of String), ByVal codigo_per As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.incidentes_docente_listar", tipo, cod, CInt(arr(1)), CInt(arr(2)), CInt(arr(3)), CInt(codigo_per))
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If i = 0 Then data.Add("sw", True)
                    data.Add("cCod", tb.Rows(i).Item("codigo_incd"))
                    data.Add("cCiclo", tb.Rows(i).Item("codigo_cac"))
                    data.Add("nCiclo", tb.Rows(i).Item("semestre"))
                    data.Add("cDpto", tb.Rows(i).Item("codigo_dac"))
                    data.Add("nDpto", tb.Rows(i).Item("depacademico"))
                    data.Add("cPer", tb.Rows(i).Item("codigo_per"))
                    data.Add("nPer", tb.Rows(i).Item("apellidoPat_Per").ToString & " " & tb.Rows(i).Item("apellidoMat_Per").ToString & " " & tb.Rows(i).Item("nombres_Per").ToString)
                    data.Add("nAsunto", tb.Rows(i).Item("asunto"))
                    data.Add("nAsuntoext", tb.Rows(i).Item("asunto"))
                    data.Add("nDetalle", tb.Rows(i).Item("detalle"))
                    data.Add("nDetalleext", tb.Rows(i).Item("detalle"))
                    data.Add("nAdjunto", tb.Rows(i).Item("adjunto"))
                    data.Add("nCodigo", tb.Rows(i).Item("numero"))
                    If tb.Rows(i).Item("activo") = 1 Then
                        data.Add("est", True)
                    Else
                        data.Add("est", False)
                    End If
                    data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BuscarIncidenteDocente(ByVal tipo As String, ByVal cod As Integer)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.incidentes_docente_listar", tipo, cod)
            obj.CerrarConexion()

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    If i = 0 Then data.Add("sw", True)
                    data.Add("cCod", tb.Rows(i).Item("codigo_incd"))
                    data.Add("cCiclo", tb.Rows(i).Item("codigo_cac"))
                    data.Add("nCiclo", tb.Rows(i).Item("semestre"))
                    data.Add("cDpto", tb.Rows(i).Item("codigo_dac"))
                    data.Add("nDpto", tb.Rows(i).Item("depacademico"))
                    data.Add("cPer", tb.Rows(i).Item("codigo_per"))
                    data.Add("nPer", tb.Rows(i).Item("apellidoPat_Per").ToString & " " & tb.Rows(i).Item("apellidoMat_Per").ToString & " " & tb.Rows(i).Item("nombres_Per").ToString)
                    data.Add("nAsunto", tb.Rows(i).Item("asunto"))
                    data.Add("nDetalle", tb.Rows(i).Item("detalle"))
                    data.Add("nAdjunto", tb.Rows(i).Item("adjunto"))
                    data.Add("nCodigo", tb.Rows(i).Item("numero"))
                    If tb.Rows(i).Item("activo") = 1 Then
                        data.Add("est", True)
                    Else
                        data.Add("est", False)
                    End If

                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

End Class
