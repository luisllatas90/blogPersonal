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

Partial Class DataJson_CertificadoQta_CertificadoQta_ajax
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim codigo_per As String = ""
        Dim clsF As New ClsCRM

        If Request.Form("PersonaId") <> "" Then
            codigo_per = clsF.DesencriptaTexto(Request.Form("PersonaId"))
        End If ' Session("id_per")

        ' codigo_per = Request.Form("PersonaId") ' Session("id_per")

        Dim codigo_dvac As String = ""

        If Request.Form("Param1") <> "" Then
            codigo_dvac = clsF.DesencriptaTexto(Request.Form("Param1"))
        End If

        Dim FPost As String = Request.Form("Funcion")
        Dim Periodo As Integer '= Request.Form("Periodo")

        Try
            Select Case FPost
                'Case "Planilla"
                '    ListarPlanilla(Periodo, CInt(codigo_per))
                'Case "PlanillaCTS"
                '    ListarPlanillaCTS(Periodo, CInt(codigo_per))
                Case "ListarCertQuinta"
                    ListarCertQuinta(CInt(codigo_per))
                Case "Generar"
                    'SubirArchivo(CInt(codigo_per))
                    SubirArchivo(CInt(codigo_dvac), CInt(codigo_per))         'Periodo y codigo_per para los ingresos de Quinta Categoría
                    'Case "GenerarCTS"
                    '    GenerarHojaCts(CInt(codigo_per))
                Case "DescargarArchivo"
                    DescargarArchivo()
                    'Case "Periodo"
                    '    PeriodoContable()
                Case "DatosPersona"
                    DatosPersona()
            End Select

        Catch ex As Exception
            Dim JSONresult As String = ""
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("error msje", ex.Message & " 1--- " & ex.StackTrace)
            Data.Add("PER", Request.Form("PersonaId"))
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    'Sub ListarPlanilla(ByVal Periodo As Integer, ByVal codigo_usu As Integer)
    '    Dim obj As New ClsConectarDatos
    '    Dim tb As New Data.DataTable
    '    Dim cn As New clsaccesodatos
    '    '  codigo_usu = Request.Form("Param01")
    '    ' Response.Write(codigo_usu)
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim JSONresult As String = ""
    '    tb = obj.TraerDataTable("dbo.USP_LISTARPLANILLA_PERSONA", "LI", Periodo, codigo_usu, Session("id_per"))
    '    obj.CerrarConexion()
    '    Dim cif As New ClsCRM

    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    For i As Integer = 0 To tb.Rows.Count - 1

    '        Dim Data As New Dictionary(Of String, Object)()
    '        Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
    '        Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
    '        Data.Add("Periodo", tb.Rows(i).Item("Periodo"))
    '        Data.Add("Mes", tb.Rows(i).Item("Mes"))
    '        Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
    '        Data.Add("TipoPlanilla", tb.Rows(i).Item("TipoPlanilla"))
    '        Data.Add("Estado", tb.Rows(i).Item("Estado"))
    '        Data.Add("FechaPago", tb.Rows(i).Item("FechaPago"))
    '        Data.Add("CodigoTplla", tb.Rows(i).Item("CodigoTplla"))
    '        Data.Add("IdArchivosCompartidos", tb.Rows(i).Item("IdArchivosCompartido"))
    '        Data.Add("BolGenerado", tb.Rows(i).Item("BolGenerado"))
    '        list.Add(Data)
    '    Next
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

    '    JSONresult = serializer.Serialize(list)
    '    Response.Write(JSONresult)
    'End Sub

    'Sub ListarPlanillaCTS(ByVal Periodo As Integer, ByVal codigo_usu As Integer)
    '    Dim obj As New ClsConectarDatos
    '    Dim tb As New Data.DataTable
    '    Dim cn As New clsaccesodatos
    '    '  codigo_usu = Request.Form("Param01")
    '    ' Response.Write(codigo_usu)
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()
    '    Dim JSONresult As String = ""
    '    tb = obj.TraerDataTable("dbo.USP_LISTARPLANILLA_PERSONA", "CT", Periodo, codigo_usu, Session("id_per"))
    '    obj.CerrarConexion()
    '    Dim cif As New ClsCRM

    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    For i As Integer = 0 To tb.Rows.Count - 1

    '        Dim Data As New Dictionary(Of String, Object)()
    '        Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
    '        Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
    '        Data.Add("Periodo", tb.Rows(i).Item("Periodo"))
    '        Data.Add("Mes", tb.Rows(i).Item("Mes"))
    '        Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
    '        Data.Add("TipoPlanilla", tb.Rows(i).Item("TipoPlanilla"))
    '        Data.Add("Estado", tb.Rows(i).Item("Estado"))
    '        Data.Add("FechaPago", tb.Rows(i).Item("FechaPago"))
    '        Data.Add("CodigoTplla", tb.Rows(i).Item("CodigoTplla"))
    '        Data.Add("IdArchivosCompartidos", cif.EncriptaTexto(tb.Rows(i).Item("IdArchivosCompartido")))
    '        Data.Add("BolGenerado", tb.Rows(i).Item("BolGenerado"))
    '        list.Add(Data)
    '    Next
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

    '    JSONresult = serializer.Serialize(list)
    '    Response.Write(JSONresult)
    'End Sub

    Sub ListarCertQuinta(ByVal codigo_usu As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        '  codigo_usu = Request.Form("Param01")
        ' Response.Write(codigo_usu)
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.USP_LISTARCERTIFICADOSQTA_PERSONA", "CT", 0, codigo_usu, Session("id_per"))
        obj.CerrarConexion()
        Dim cif As New ClsCRM

        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1

            Dim Data As New Dictionary(Of String, Object)()
            'Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
            'Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
            'Data.Add("Periodo", tb.Rows(i).Item("Periodo"))
            'Data.Add("Mes", tb.Rows(i).Item("Mes"))
            'Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
            'Data.Add("TipoPlanilla", tb.Rows(i).Item("TipoPlanilla"))
            'Data.Add("Estado", tb.Rows(i).Item("Estado"))
            'Data.Add("FechaPago", tb.Rows(i).Item("FechaPago"))
            'Data.Add("CodigoTplla", tb.Rows(i).Item("CodigoTplla"))
            'Data.Add("IdArchivosCompartidos", cif.EncriptaTexto(tb.Rows(i).Item("IdArchivosCompartido")))
            'Data.Add("BolGenerado", tb.Rows(i).Item("BolGenerado"))
            Data.Add("Codigo", cif.EncriptaTexto(tb.Rows(i).Item("Codigo")))
            Data.Add("FechaIni", tb.Rows(i).Item("FechaIni"))
            Data.Add("FechaFin", tb.Rows(i).Item("FechaFin"))
            'Data.Add("NroDias", tb.Rows(i).Item("NroDias"))
            Data.Add("NroDias", "")
            'Data.Add("NroMemo", tb.Rows(i).Item("NroMemo"))
            Data.Add("NroMemo", "")
            'Data.Add("IdArchivosCompartidos", cif.EncriptaTexto(tb.Rows(i).Item("IdArchivosCompartido")))
            Data.Add("IdArchivosCompartidos", tb.Rows(i).Item("IdArchivosCompartido"))
            Data.Add("VacGenerado", tb.Rows(i).Item("VacGenerado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub PeriodoContable()
        Try

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("dbo.CONTA_PROCESOCONTABLE_V2")
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
        Catch ex As Exception
            Dim JSONresult As String = ""
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("error msje", ex.Message & " --- " & ex.StackTrace)

            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub DatosPersona()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim clsf As New ClsCRM
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'tb = obj.TraerDataTable("dbo.USP_LISTAR_PERSONA_BOLETA")
        tb = obj.TraerDataTable("dbo.USP_LISTAR_PERSONA_CERTIFICADOQTA", "1", Session("id_per"))


        obj.CerrarConexion()
        'Response.Write(tb.Rows.Count)
        'Response.Write(Session("id_per"))

        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", clsf.EncriptaTexto(tb.Rows(i).Item("CodigoPer").ToString))
            'Data.Add("Value", tb.Rows(i).Item("CodigoPer").ToString)
            Data.Add("Label", tb.Rows(i).Item("DatosPersona"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub SubirArchivo(ByVal Codigo_dvac As Integer, ByVal Codigo_per As Integer)
        ' CodigoPer = Request.Form("PersonaId")
        'Response.Write(Request.Form("PersonaId"))
        ' Response.Write(Desencriptar("PERU"))
        ' Dim b As New BinaryReader(post.InputStream)
        '  Dim by() As Byte = b.ReadByte(post.ContentLength)
        Try
            Dim cif As New ClsCRM
            'Dim Codigo_dvac As Integer = cif.DesencriptaTexto(Request.Form("Param1"))
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos

            Dim IpRemoto As String = Request.UserHostAddress.ToString
            Dim HostName As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName.ToString

            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            'obj.AbrirConexion()

            'tb = obj.TraerDataTable("dbo.USP_LISTARVACACIONES_PERSONA", "UN", Codigo_dvac, 0, Session("id_per"))
            'obj.CerrarConexion()

            Dim list As New Dictionary(Of String, String)
            Dim wsCloud As New ClsArchivosCompartidos
            'Response.Write(CodigoPlla)
            'Dim list As New List(Of Dictionary(Of String, String))()
            'list.Add("CodigoPer", CInt(tb.Rows(0)("Codigo_per")))
            list.Add("CodigoPer", CInt(Codigo_per))
            list.Add("CodigoDvac", CInt(Codigo_dvac))
            'list.Add("FechaIni", tb.Rows(0)("FechaIni"))
            'list.Add("FechaFin", tb.Rows(0)("FechaFin"))
            'list.Add("nromemo_dvac", tb.Rows(0)("nromemo_dvac"))
            list.Add("TablaId", 16)                                     'Vacaciones (verificar si el correlativo es este)
            list.Add("Usuario", Session("perlogin").ToString().Trim)
            list.Add("param8", Session("perlogin").ToString().Trim)
            'Response.Write("Codigo_dvac : " & Codigo_dvac.ToString & " CodigoPer: " & tb.Rows(0)("Codigo_per").ToString & " usuario : " & Session("perlogin").ToString().Trim)
            Dim envelope As String = wsCloud.SoapEnvelopeGenerarQuinta(list)
            'Response.Write(envelope)
            'Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateTicket", Session("perlogin"))
            'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateTicket", Session("perlogin"))
            'Dim result As String = wsCloud.PeticionRequestSoap("http://" & HttpContext.Current.Request.Url.Host & "/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateTicket", Session("perlogin"))
            Dim result As String = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateQuinta", Session("perlogin"))

            'Response.Write(result)
            Dim Data As ResultMessage = ResultMessageQuinta(result)

            'Response.Write(Data)

            'Response.Write(cif.EncrytedString64(Data.StatusBody.Code))
            Dim tblRes As New Data.DataTable
            If Data.Status = "OK" Then
                obj.AbrirConexion()
                tblRes = obj.TraerDataTable("dbo.USP_CertificadoQta", 0, CInt(Codigo_per), CInt(Codigo_dvac), Data.StatusBody.Code, "", Session("perlogin").ToString().Trim, HostName, IpRemoto, "R")
                obj.CerrarConexion()

                If tblRes.Rows.Count > 0 Then
                    If tblRes.Rows(0)("Status") = "ERROR" Then
                        Data.Status = tblRes.Rows(0)("Status")
                        Data.StatusBody.Message = tblRes.Rows(0)("Message")
                    End If
                    Data.StatusBody.Code = tblRes.Rows(0)("IdArchivosCompartido")
                End If
            End If
            ''Data.StatusBody.Code = cif.EncrytedString64(Data.StatusBody.Code)
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            JSONresult = serializer.Serialize(Data)
            'JSONresult = serializer.Serialize(envelope)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim JSONresult As String = ""
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("error msje", ex.Message & " SUBIR ARCHIVO--- " & ex.StackTrace)

            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    'Sub GenerarHojaCts(ByVal CodigoPer As Integer)
    '    ' CodigoPer = Request.Form("PersonaId")
    '    'Response.Write(Request.Form("PersonaId"))
    '    ' Response.Write(Desencriptar("PERU"))
    '    ' Dim b As New BinaryReader(post.InputStream)
    '    '  Dim by() As Byte = b.ReadByte(post.ContentLength)
    '    Dim cif As New ClsCRM
    '    Dim CodigoPlla As Integer = cif.DesencriptaTexto(Request.Form("Param1"))
    '    Dim obj As New ClsConectarDatos
    '    Dim tb As New Data.DataTable
    '    Dim cn As New clsaccesodatos
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    obj.AbrirConexion()

    '    tb = obj.TraerDataTable("dbo.USP_LISTARPLANILLA_PERSONA", "UN", CodigoPlla, 0, Session("id_per"))
    '    obj.CerrarConexion()

    '    Dim list As New Dictionary(Of String, String)
    '    Dim wsCloud As New ClsArchivosCompartidos
    '    'Response.Write(CodigoPlla)
    '    ' Dim list As New List(Of Dictionary(Of String, String))()
    '    list.Add("Semestre", tb.Rows(0)("Semestre"))
    '    list.Add("Periodo", tb.Rows(0)("Periodo"))
    '    list.Add("ImpTc", "3.25")
    '    list.Add("CodigoPer", CodigoPer.ToString())
    '    list.Add("TablaId", 7)
    '    list.Add("Fecha", tb.Rows(0)("FechaIni"))
    '    'Response.Write("Codigo_plla : " & CodigoPlla & " CodigoPer: " & CodigoPer & " TipoPlla : " & tb.Rows(0)("CodigoTplla") & " Mes : " & tb.Rows(0)("Mes") & " Periodo : " & tb.Rows(0)("Periodo") & " Fecha : " & tb.Rows(0)("FechaIni"))
    '    Dim envelope As String = wsCloud.SoapEnvelopeGenerarCTS(list)
    '    Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateTicket", Session("perlogin"))
    '    'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateTicket", Session("perlogin"))
    '    'Dim result As String = wsCloud.PeticionRequestSoap("http://" & HttpContext.Current.Request.Url.Host & "/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/GenerateCTS", Session("perlogin"))
    '    Dim Data As ResultMessage = ResultMessageCTS(result)

    '    '   Response.Write(result)
    '    Dim tblRes As New Data.DataTable
    '    If Data.Status = "OK" Then
    '        obj.AbrirConexion()
    '        tblRes = obj.TraerDataTable("dbo.USP_BOLETAPAGOPLANILLA", 0, CodigoPer, CodigoPlla, tb.Rows(0)("Periodo"), tb.Rows(0)("Mes"), Data.StatusBody.Code, "", "", "", "", "R")
    '        obj.CerrarConexion()

    '        If tblRes.Rows.Count > 0 Then
    '            If tblRes.Rows(0)("Status") = "ERROR" Then
    '                Data.Status = tblRes.Rows(0)("Status")
    '                Data.StatusBody.Message = tblRes.Rows(0)("Message")
    '            End If
    '        End If
    '    End If
    '    Data.StatusBody.Code = cif.EncrytedString64(Data.StatusBody.Code)
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    JSONresult = serializer.Serialize(Data)
    '    Response.Write(JSONresult)

    'End Sub

    Private Sub DescargarArchivo2()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim resultData As New List(Of Dictionary(Of String, Object))()
        Try

            If Not Session("perlogin") Is Nothing Then
                Dim IdArchivo As String = ""
                Dim wsCloud As New ClsArchivosCompartidos
                Dim list As New Dictionary(Of String, String)
                Dim Usuario As String = "USAT\"

                'For Each s As String In Request.Form.AllKeys
                '    IdArchivo = Request.Form(s)
                'Next
                Dim cif As New ClsCRM
                'Response.Write(cif.DecrytedString64("NQAyAA=="))
                'Response.Write(cif.DecrytedString64("NQAzAA=="))
                'IdArchivo = cif.DecrytedString64(Request.Form("Param1"))


                list.Add("IdArchivo", "0x01000000F7D0D88E5BD96B01A501A79099C3EA56930B04544C8CDB50")
                list.Add("Usuario", Usuario)
                'list.Add("param1", "BUJMP5SUYJ")
                'list.Add("param2", "D:\demo.xlsx")

                '  Response.Write(Session("perlogin"))
                Dim obj As New ClsConectarDatos
                Dim tb As New Data.DataTable
                Dim cn As New clsaccesodatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tb = obj.TraerDataTable("dbo.USP_LISTARARCHIVOSCOMPARTIDOS", 2, 1, "0x01000000F7D0D88E5BD96B01A501A79099C3EA56930B04544C8CDB50", "BUJMP5SUYJ")
                obj.CerrarConexion()

                'Response.Write(Request.Url.Authority)

                Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                'Dim result As String = wsCloud.PeticionRequestSoap("http://" & HttpContext.Current.Request.Url.Host & "/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))
                'Dim result As String = wsCloud.PeticionRequestSoap("http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                Dim result As String = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Session("perlogin"))
                'Response.Write(result)
                'Dim imagen As String = ResultFile(result)

                'Dim pathTem As String = Server.MapPath("../../administrativo/Tesoreria/Rendiciones/Archivosderendicion") & "/" & IdArchivo + ".PDF"
                '' Dim info As New DirectoryInfo(pathTem)
                'Dim pathperm As String = Server.MapPath("../../administrativo/Tesoreria/Rendiciones/Archivosderendicion/")
                'AddDirectorySecurity(pathperm, Session("perlogin"), FileSystemRights.ReadData, AccessControlType.Allow)
                'AddDirectorySecurity(pathperm, Session("perlogin"), FileSystemRights.Write, AccessControlType.Allow)
                'AddDirectorySecurity(pathperm, Session("perlogin"), FileSystemRights.CreateFiles, AccessControlType.Allow)

                'Dim tempBytes As Byte() = System.Convert.FromBase64String(imagen)
                'Using fs As New FileStream(pathTem, FileMode.Create)
                '    fs.Write(tempBytes, 0, tempBytes.Length)
                '    fs.Close()
                'End Using

                'Dim host As String = "http://" & HttpContext.Current.Request.Url.Host & "/campusvirtual/personal/DataJson/Boletas/Temps/" & IdArchivo + ".PDF"



                'Dim host As String = "" '"http://localhost/campusvirtual/personal/administrativo/Tesoreria/Rendiciones/Archivosderendicion/" & IdArchivo + ".PDF"
                'For i As Integer = 0 To tb.Rows.Count - 1
                '    Dim Data As New Dictionary(Of String, Object)()
                '    Data.Add("Status", "OK")
                '    'Data.Add("File", imagen)
                '    'Data.Add("Nombre", tb.Rows(i).Item("NombreArchivo"))
                '    'Data.Add("Extencion", tb.Rows(i).Item("Extencion"))
                '    'Data.Add("Path", host)
                '    Data.Add("File", "")
                '    Data.Add("Nombre", "")
                '    Data.Add("Extencion", "")
                '    Data.Add("Path", "")
                '    resultData.Add(Data)
                'Next
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("envelope", envelope.ToString)
                Data.Add("result", result.ToString)
                resultData.Add(Data)


            Else
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "ERROR")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir a Menu Principal/Personal/Documentos Virtuales/Vacaciones </h4>")
                Data.Add("File", "")
                Data.Add("Nombre", "")
                Data.Add("Extencion", "")
                Data.Add("Path", "")
                Data.Add("Per", Session("perlogin").ToString)
                resultData.Add(Data)
            End If
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", ex.Message & " " & ex.StackTrace)
            Data.Add("PerLogin", Session("perlogin"))
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub DescargarArchivo()
        Dim JSONresult As String = ""
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim resultData As New List(Of Dictionary(Of String, Object))()
        Try

            If Not Session("perlogin") Is Nothing Then

                Dim IdArchivo As String = ""
                Dim nombreArchivo As String = ""
                Dim tipoArchivo As String = ""
                Dim ruta As String = ""
                Dim rutadwn As String = ""
                Dim cif As New ClsCRM

                IdArchivo = cif.DesencriptaTexto(Request("Param1").ToString) ' "0x01000000812DA2D71C7B31E913DC3FBE74CD59D897160008AB2ADDD5"
                nombreArchivo = ""
                tipoArchivo = ""
                'ruta = Server.MapPath("filesIncidentes/") & nombreArchivo
                'ruta = Server.MapPath("filesIncidentes/") & nombreArchivo

                'rutadwn = "http://serverdev/campusvirtual/CampusVirtualEstudiante/CampusVirtualEstudiante/filesIncidentes/" & nombreArchivo



                Dim wsCloud As New ClsArchivosCompartidosV2
                Dim list As New Dictionary(Of String, String)
                Dim Usuario As String = Session("perlogin")
                list.Add("IdArchivo", IdArchivo)
                list.Add("Usuario", Usuario)
                list.Add("Token", "JHLQ97PAWA")

                Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
                Dim result As String = ""

                'result = wsCloud.PeticionRequestSoap("http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                result = wsCloud.PeticionRequestSoap("http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx", envelope, "http://usat.edu.pe/DownloadFile", Usuario)
                Dim imagen As String = ResultFile(result)

                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("File", imagen.ToString)
                Data.Add("result", result.ToString)
                Data.Add("Param1", IdArchivo)
                Data.Add("Status", "OK")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir a Menu Principal/Personal/Documentos Vituales/Ver Solicitudes de Vacaciones </h4>")
                resultData.Add(Data)


                'INICIO BITACORA 
                Dim IpRemoto As String = Request.UserHostAddress.ToString
                Dim HostName As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName.ToString
                Dim obj As New ClsConectarDatos
                Dim tblBit As New Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tblBit = obj.TraerDataTable("dbo.USP_BITACORABOLETAPAGOPLANILLA", CInt(Session("id_per")), 0, 0, 0, 0, IdArchivo, Usuario, HostName, IpRemoto, "D")
                obj.CerrarConexion()
                'FIN BITACORA



            Else
                Dim Data As New Dictionary(Of String, Object)()
                Data.Add("Status", "ERROR")
                Data.Add("Message", "La sessión del usuario ha finalizado<h4>Ir a Menu Principal/Personal/Documentos Vituales/Ver Solicitudes de Vacaciones </h4>")
                Data.Add("File", "")
                Data.Add("Nombre", "")
                Data.Add("Extencion", "")
                Data.Add("Path", "")

                Data.Add("Per", Session("perlogin").ToString)
                resultData.Add(Data)
            End If
            JSONresult = serializer.Serialize(resultData)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", ex.Message & " " & ex.StackTrace)
            Data.Add("PerLogin", Session("perlogin"))
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try
    End Sub

    ' Adds an ACL entry on the specified directory for the specified account.
    Sub AddDirectorySecurity(ByVal FileName As String, ByVal Account As String, ByVal Rights As FileSystemRights, ByVal ControlType As AccessControlType)
        ' Create a new DirectoryInfoobject.
        Dim dInfo As New DirectoryInfo(FileName)

        ' Get a DirectorySecurity object that represents the 
        ' current security settings.
        Dim dSecurity As DirectorySecurity = dInfo.GetAccessControl()

        'Dim  perms As FileSystemRights,
        ' Add the FileSystemAccessRule to the security settings. 
        dSecurity.AddAccessRule(New FileSystemAccessRule(Account, Rights, ControlType))
        ' dSecurity.AddAccessRule(New FileSystemAccessRule(Account, perms.ReadData, ControlType))
        ' Set the new access settings.
        dInfo.SetAccessControl(dSecurity)

    End Sub

    Function ResultMessage(ByVal cadXml As String) As ResultMessage
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        nsMgr.AddNamespace("xm", "http://usat.edu.pe")
        Dim Code1 As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateTicketResponse/xm:GenerateTicketResult", nsMgr)
        Dim Status As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateTicketResponse/xm:GenerateTicketResult/xm:Status", nsMgr)
        Dim Code As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateTicketResponse/xm:GenerateTicketResult/xm:StatusBody/xm:Code", nsMgr)
        Dim Message As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateTicketResponse/xm:GenerateTicketResult/xm:StatusBody/xm:Message", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        ' Return res.InnerXml '.InnerText
        '   Response.Write("dd" + res.InnerText)
        Dim cif As New ClsCRM
        Dim result As New ResultMessage
        result.Status = Status.InnerXml
        result.StatusBody.Code = Code.InnerXml
        result.StatusBody.Message = Message.InnerXml

        Return result
    End Function
    Function ResultMessageCTS(ByVal cadXml As String) As ResultMessage
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        nsMgr.AddNamespace("xm", "http://usat.edu.pe")
        Dim Code1 As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateCTSResponse/xm:GenerateCTSResult", nsMgr)
        Dim Status As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateCTSResponse/xm:GenerateCTSResult/xm:Status", nsMgr)
        Dim Code As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateCTSResponse/xm:GenerateCTSResult/xm:StatusBody/xm:Code", nsMgr)
        Dim Message As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateCTSResponse/xm:GenerateCTSResult/xm:StatusBody/xm:Message", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        ' Return res.InnerXml '.InnerText
        '   Response.Write("dd" + res.InnerText)
        Dim cif As New ClsCRM
        Dim result As New ResultMessage
        result.Status = Status.InnerXml
        result.StatusBody.Code = Code.InnerXml
        result.StatusBody.Message = Message.InnerXml

        Return result
    End Function

    'Begin 29/01/2019
    Function ResultMessageVacaciones(ByVal cadXml As String) As ResultMessage
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        nsMgr.AddNamespace("xm", "http://usat.edu.pe")
        Dim Code1 As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateVacacionesResponse/xm:GenerateVacacionesResult", nsMgr)
        Dim Status As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateVacacionesResponse/xm:GenerateVacacionesResult/xm:Status", nsMgr)
        Dim Code As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateVacacionesResponse/xm:GenerateVacacionesResult/xm:StatusBody/xm:Code", nsMgr)
        Dim Message As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateVacacionesResponse/xm:GenerateVacacionesResult/xm:StatusBody/xm:Message", nsMgr)
        '  Dim mNombre = xml.ReadElementString("nombre")
        ' Return res.InnerXml '.InnerText
        '   Response.Write("dd" + res.InnerText)
        Dim cif As New ClsCRM
        Dim result As New ResultMessage
        result.Status = Status.InnerXml
        result.StatusBody.Code = Code.InnerXml
        result.StatusBody.Message = Message.InnerXml

        Return result
    End Function
    'End 29/01/2019

    'Begin 28/11/2019
    Function ResultMessageQuinta(ByVal cadXml As String) As ResultMessage
        Dim nsMgr As XmlNamespaceManager
        Dim xml As XmlDocument = New XmlDocument()
        xml.LoadXml(cadXml)
        nsMgr = New XmlNamespaceManager(xml.NameTable)
        nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
        nsMgr.AddNamespace("xm", "http://usat.edu.pe")
        Dim Code1 As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult", nsMgr)
        Dim Status As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:Status", nsMgr)
        Dim Code As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:StatusBody/xm:Code", nsMgr)
        Dim Message As XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body/xm:GenerateQuintaResponse/xm:GenerateQuintaResult/xm:StatusBody/xm:Message", nsMgr)
        ' Dim mNombre = xml.ReadElementString("nombre")
        ' Return res.InnerXml '.InnerText
        ' Response.Write("dd" + res.InnerText)
        Dim cif As New ClsCRM
        Dim result As New ResultMessage
        result.Status = Status.InnerXml
        result.StatusBody.Code = Code.InnerXml
        result.StatusBody.Message = Message.InnerXml

        Return result
    End Function
    'End 28/11/2019

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

    Private Function generarClaveSHA1(ByVal nombre As String) As String
        ' Crear una clave SHA1 como la generada por
        ' FormsAuthentication.HashPasswordForStoringInConfigFile
        ' Adaptada del ejemplo de la ayuda en la descripción de SHA1 (Clase)
        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(nombre)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider
        ' This is one implementation of the abstract class SHA1.
        result = sha.ComputeHash(data)
        '
        ' Convertir los valores en hexadecimal
        ' cuando tiene una cifra hay que rellenarlo con cero
        ' para que siempre ocupen dos dígitos.
        Dim sb As New StringBuilder
        For i As Integer = 0 To result.Length - 1
            If result(i) < 16 Then
                sb.Append("0")
            End If
            sb.Append(result(i).ToString("x"))
        Next
        '
        Return sb.ToString.ToUpper
    End Function
    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
    Public Function Desencriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
End Class

Public Class ResultData
    Dim _status As String
    Dim _code As String
    Dim _message As String
    Public Property Staus() As String
        Get
            Return _code
        End Get
        Set(ByVal value As String)
            _code = value
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

End Class
