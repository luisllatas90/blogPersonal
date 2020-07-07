Imports System.Web.Security
Imports System.Data
Imports System.Data.DataTable
Imports System.Collections.Generic
Imports System.Data.DataRow
Imports System.Data.DataColumn
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Diagnostics
Imports System.Xml
Imports System.Xml.Serialization

'Imports iTextSharp.text.pdf
'Imports iTextSharp.text

'Imports cLogica
'Imports cEntidad
'Imports ws = wdsFacElect

Partial Class DataJson_activofijo_processactivofijo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim FPost As String = Request("param0")
            'Response.Write("aaaaaaaaaaaaaaaa: " + FPost)
            Select Case FPost
                Case "dCaracteristica"
                    DelCaracteristica()
                Case "gAddCaracteristica"
                    AddCaracteristica()
                Case "lstCrt"
                    LstCaracteristica()
                Case "dComponente"
                    DelComponente()
                Case "gAddComponente"
                    AddComponente()
                Case "gAddComponenteV2"
                    AddComponentev2()
                Case "lstComponentes"
                    LstComponentes()
                Case "lstArticulos"
                    LstArticulos()
                Case "gAddConfigActivoComponentes"
                    ConfigActivoComponentes()
                Case "gLstConfigActivoCom"
                    lstConfigActivoCom()
                Case "gActualizarConfigActivoComponente"
                    ActualizarActivoComponente()
                Case "lstFamilia"
                    LstFamilia()
                Case "gLstArtCrt"
                    LstArtCrt()
                Case "dActualizarCrtArt"
                    ActualizarCrtArt()
                Case "gLstArticuloComponentes"
                    LstArticuloComponentes()
                Case "gActualizarArtCom"
                    ActualizarArtCom()
                Case "dDetalleArtComponente"
                    EliminarDetalleArtComponente()
                Case "gLstTrasladosMercaderia"
                    LstTrasladosMercaderia()
                Case "gTrasladoMercaderia"
                    ActualizacionTrasladoMercaderia()
                Case "LstCentroCo"
                    LstCentroCo()
                Case "lstPersonal"
                    lstPersonal()
                Case "gLstDetalleTraslado"
                    LstDetalleTraslado()
                Case "gAddDetTraslado"
                    AddDetTraslado()
                Case "gActualizarTraslado"
                    ActualizarTraslado()
                Case "gTrasladoMercaderiaV2"
                    TrasladoMercaderiaV2()
                Case "dEliminarTraslado"
                    EliminarTraslado()
                Case "lstRegistroActivo"
                    lstRegistroActivo()
                Case "gLstArtCrtRegAF"
                    LstArtCrtRegAF()
                Case "gRegistrarDetalleAF"
                    RegistrarDetalleAF()
                Case "gLstArticuloComponentesRegAF"
                    LstArticuloComponentesRegAF()
                Case "LstDetalleRegistroAF"
                    LstDetalleRegistroAF()
                Case "LstDetalleRegistroAF_v2"
                    LstDetalleRegistroAF_v2()
                Case "gClonarDetalleAF"
                    ClonarDetalleAF()
                Case "gActualizarSerieEgreso"
                    ActualizarSerieEgreso()
                Case "lstDetallePedidoAlmDes"
                    lstDetallePedidoAlmDes()
                Case "validaDetalleCantPed"
                    validaDetalleCantPed()
                Case "validaCantidadDisponibleTraslado"
                    validaCantidadDisponibleTraslado()
                Case "lstActivoFijo"
                    mt_lstActivoFijo()
                Case "lstUbicacion"
                    mt_lstUbicacion()
                Case "gGenerarPdf"
                    mt_GenerarPdf()
            End Select
        Catch ex As Exception
            Data.Add("Mensaje Error", ex.Message & " - " & ex.StackTrace)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try


    End Sub
    ' SIIIIIIIIIIIIIIIII
    Sub DelCaracteristica()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoC As Integer = Request("param1")
        Dim Usuario As Integer = Request("hdUser")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("AF_actualizarCaracteristicaAF---" & CodigoC & "--" & "-ELI-" & "USR-")
        tb = obj.TraerDataTable("AF_actualizarCaracteristicaAF", CodigoC, "", 0, "ELI", Usuario)
        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIIIIII
    Sub AddCaracteristica()
        Dim list As New List(Of Dictionary(Of String, Object))()
        'Dim CodigoC As Integer = Request("hdId")
        Dim Caracteristica As String = Request("txtCaracteristica").ToString
        Dim Usuario As Integer = Request("hdUser")
        Dim accion As String = Request("hdtipoA").ToString

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)
        If Request("hdtipoA").ToString = "G" Then
            tb = obj.TraerDataTable("AF_registrarCaracteristicaAF", Caracteristica, Usuario)
        Else
            Dim Estado As Integer = Request("hdEstado")
            Dim Id As Integer = Request("hdId")
            'tb = obj.TraerDataTable("AF_actualizarCaracteristicaAF", CodigoC, "", "ELI", "USR")
            tb = obj.TraerDataTable("AF_actualizarCaracteristicaAF", Id, Caracteristica, Estado, "ACT", Usuario)
        End If

        obj.CerrarConexion()
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

    Sub DelComponente()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoCo As Integer = Request("hdId")
        Dim CodigoCa As Integer = Request("param1")
        Dim Tipo As String = "ELI"


        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("AF_actualizarComponenteAF--" & CodigoCo & "--" & CodigoCa & "-USR-")
        tb = obj.TraerDataTable("AF_actualizarComponenteAF", CodigoCo, CodigoCa, 0, 0, Tipo, "USR")
        obj.CerrarConexion()
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

    Sub AddComponente()
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim Componente As String = Request("txtComponente")
        Dim CodigoCa As Integer = Request("param1")
        Dim Valor As String = Request("txtValor")
        Dim Estado As Integer = Request("hdEstado")
        Dim Usuario As String = "Jseclen"
        Dim accion As String = Request("hdtipoA").ToString
        Dim Tipo As String = "ACT"

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)
        If Request("hdtipoA").ToString = "G" Then
            tb = obj.TraerDataTable("AF_registrarComponenteAF", Componente, CodigoCa, Valor, Estado, Usuario)
        Else
            Dim CodigoCo As Integer = Request("hdId").ToString
            tb = obj.TraerDataTable("AF_actualizarComponenteAF", CodigoCo, CodigoCa, Componente, Valor, Estado, Tipo, Usuario)
        End If

        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIIIIII
    Sub LstCaracteristica()
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_listarCaracteristicaAF")

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_crt", tb.Rows(i).Item("id"))
            Data.Add("d_crt", tb.Rows(i).Item("caracteristica"))
            Data.Add("d_est", tb.Rows(i).Item("estado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIIIIIIII
    Sub AddComponentev2()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Componente As String = Request("txtComponente")
        Dim Estado As Integer = CInt(Request("hdEstado").ToString)
        Dim Usuario As Integer = Session("id_per")
        Dim accion As String = Request("hdtipoA").ToString
        Dim Tipo As String = "ACT"

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)
        If Request("hdtipoA").ToString = "G" Then
            tb = obj.TraerDataTable("AF_registrarComponenteAF", Componente, Estado, Usuario)
        Else
            Dim CodigoCo As Integer = Request("hdId").ToString
            tb = obj.TraerDataTable("AF_actualizarComponenteAF", CodigoCo, Componente, Estado, Tipo, Usuario)
        End If

        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIIIIII
    Sub LstComponentes()
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_listarComponenteAF")

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_com", tb.Rows(i).Item("id"))
            Data.Add("d_com", tb.Rows(i).Item("componente"))
            Data.Add("e_com", tb.Rows(i).Item("estado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIIII
    Sub LstArticulos()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'Response.Write("***" & accion & "/" & Caracteristica)
        tb = obj.TraerDataTable("AF_listarArticulosAF")
        'tb = obj.TraerDataTable("AF_listarCaracteristicaAF")

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_id", tb.Rows(i).Item("id"))
            Data.Add("d_des", tb.Rows(i).Item("descripcion"))
            'Data.Add("d_des", tb.Rows(i).Item("caracteristica"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ConfigActivoComponentes()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_com As Integer = Request("hdCom")
        Dim Codigo_crt As Integer = Request("hdCrt")
        Dim Codigo_art As Integer = Request("hdArt")
        Dim Valor As String = Request("txtValor")
        'Dim accion As String = Request("hdtipoA").ToString

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_registrarConfiguracionCrtCom", Codigo_com, Codigo_crt, Codigo_art, Valor)

        obj.CerrarConexion()
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

    Sub lstConfigActivoCom()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim codigo_art As Integer = Request("hdId")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)
        tb = obj.TraerDataTable("AF_ListarConfiguracionCrtCom", codigo_art)

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_id", tb.Rows(i).Item("id"))
            Data.Add("d_crt", tb.Rows(i).Item("caracteristica"))
            Data.Add("d_com", tb.Rows(i).Item("componente"))
            Data.Add("d_act", tb.Rows(i).Item("activo"))
            Data.Add("d_val", tb.Rows(i).Item("valor"))
            Data.Add("d_est", tb.Rows(i).Item("estado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ActualizarActivoComponente()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Id As Integer = Request("hdId")
        Dim EstadoN As Integer = Request("hdEstado")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("AF_actualizarConfigActivoCom---" & Id & "--" & Valor & "--" & EstadoN)
        If Request("hdtipoA").ToString = "G" Then
            Dim Valor As String = Request("txtValorE")
            tb = obj.TraerDataTable("AF_actualizarConfigActivoCom", Id, Valor, EstadoN, "ACT")
        Else
            tb = obj.TraerDataTable("AF_actualizarConfigActivoCom", Id, "", EstadoN, "ELI")
        End If
        obj.CerrarConexion()
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

    Sub LstFamilia()
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_ListaFamilia")

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_id1", tb.Rows(i).Item("clase"))
            Data.Add("c_id2", tb.Rows(i).Item("linea"))
            Data.Add("c_id3", tb.Rows(i).Item("familia"))
            Data.Add("d_fam", tb.Rows(i).Item("descripcion"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIII
    Sub LstArtCrt()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_Art As Integer = Request("hdArt")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_ListarArticuloCaracteristica", Codigo_Art)

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_chk", tb.Rows(i).Item("chkCrt"))
            Data.Add("c_idO", tb.Rows(i).Item("id_aco"))
            Data.Add("c_idC", tb.Rows(i).Item("id_crt"))
            Data.Add("c_idA", tb.Rows(i).Item("id_Art"))
            Data.Add("d_crt", tb.Rows(i).Item("d_crt"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIII
    Sub ActualizarCrtArt()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_Art As Integer = Request("hdArt")
        Dim Detalle As String = Request("hdDetalle")
        Dim Usuario As Integer = Request("hdUser")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_AsignarCaracteristicaArticulo", Codigo_Art, Detalle, Usuario)

        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIII
    Sub LstArticuloComponentes()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoArt As Integer = Request("hdArt")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("AF_ListarArticuloComponente---" & CodigoArt & "--" & "-ELI-" & "USR-")
        tb = obj.TraerDataTable("AF_ListarArticuloComponente", CodigoArt)
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_chk", tb.Rows(i).Item("chkArt"))
            Data.Add("c_dar", tb.Rows(i).Item("c_dar"))
            Data.Add("c_hart", tb.Rows(i).Item("c_hart"))
            Data.Add("c_part", tb.Rows(i).Item("c_part"))
            Data.Add("d_com", tb.Rows(i).Item("d_com"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIII
    Sub ActualizarArtCom()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigop_Art As Integer = Request("hdArt")
        Dim Codigoh_Art As Integer = Request("hdCom")
        Dim Codigo_user As Integer = Request("hdUser")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & accion & "/" & Caracteristica)

        tb = obj.TraerDataTable("AF_AsignarArticuloComponente", Codigop_Art, Codigoh_Art, Codigo_user)

        obj.CerrarConexion()
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
    ' SIIIIIIIIIIII
    Sub EliminarDetalleArtComponente()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoArt As Integer = Request("hdArt")
        Dim CodigoCom As Integer = Request("hdId")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("AF_EliminarDetalleArticuloComponente", CodigoArt, CodigoCom)
        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIIIIIIIII
    Sub LstTrasladosMercaderia()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim FechaIni As String = Request("txtFechaIni")
        Dim FechaFin As String = Request("txtFechaFin")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        If (FechaIni = "" And FechaFin = "") Then
            tb = obj.TraerDataTable("AF_ListarTrasladosAF")
        Else
            tb = obj.TraerDataTable("AF_ListarTrasladosAF", FechaIni, FechaFin)
        End If
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_id", tb.Rows(i).Item("c_id"))
            Data.Add("d_tip", tb.Rows(i).Item("d_tip"))
            Data.Add("c_per", tb.Rows(i).Item("c_per"))
            Data.Add("d_nom", tb.Rows(i).Item("d_nom"))
            Data.Add("c_cco", tb.Rows(i).Item("c_cco"))
            Data.Add("d_cco", tb.Rows(i).Item("d_cco"))
            Data.Add("d_fec", tb.Rows(i).Item("d_fec"))
            Data.Add("d_obs", tb.Rows(i).Item("d_obs"))
            Data.Add("d_est", tb.Rows(i).Item("d_est"))
            Data.Add("d_mot", tb.Rows(i).Item("d_mot"))
            Data.Add("c_per2", tb.Rows(i).Item("c_per2"))
            Data.Add("d_nom2", tb.Rows(i).Item("d_nom2"))
            Data.Add("c_cco2", tb.Rows(i).Item("c_cco2"))
            Data.Add("d_cco2", tb.Rows(i).Item("d_cco2"))
            Data.Add("d_uba", tb.Rows(i).Item("d_uba"))
            Data.Add("d_nro", tb.Rows(i).Item("d_nro"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ActualizacionTrasladoMercaderia()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoT As Integer = 0
        Dim Tipo As String = Request("dpTipo")
        Dim TipoA As String = Request("hdtipoA")
        Dim Fecha As String = Request("txtFechaTraslado")
        Dim Observacion As String = Request("txtObservacion")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim CodigoP As Integer = 0
        Dim CodigoCco As Integer = 0
        Dim Estado As Integer = 0
        If (TipoA = "ELI") Then
            CodigoT = Request("hdId")
            tb = obj.TraerDataTable("AF_ActualizarTrasladoAF", CodigoT, Tipo, TipoA, Fecha, CodigoP, CodigoCco, Observacion, Estado)
        End If

        If (TipoA = "ACT") Then
            CodigoT = Request("hdId")
            CodigoP = Request("hdPer")
            CodigoCco = Request("hdCco")
            Estado = Request("hdEstado")

            tb = obj.TraerDataTable("AF_ActualizarTrasladoAF", CodigoT, Tipo, TipoA, Fecha, CodigoP, CodigoCco, Observacion, Estado)
        End If

        If (TipoA = "G") Then
            CodigoP = Request("hdPer")
            CodigoCco = Request("hdCco")
            Estado = Request("hdEstado")

            tb = obj.TraerDataTable("AF_RegistrarTraslado", CodigoT, Tipo, Fecha, CodigoP, CodigoCco, Observacion)
        End If

        'Response.Write("..." + CodigoT + "-" + TipoA + "-" + Tipo + "-" + Fecha + "-" + CodigoP + "-" + CodigoCco + "-" + Observacion + "-" + Estado)


        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIII 
    Sub LstCentroCo()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'tb = obj.TraerDataTable("PRP_listaCentroCostos")
            tb = obj.TraerDataTable("CentroCostosConvenio_LISTAR")
            obj.CerrarConexion()
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                'Data.Add("d_id", tb.Rows(i).Item("codigo"))
                'Data.Add("d_des", tb.Rows(i).Item("descripcion"))
                Data.Add("d_id", tb.Rows(i).Item("codigo_cco"))
                Data.Add("d_des", tb.Rows(i).Item("descripcion_cco"))
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)

        Catch ex As Exception
            Dim lstErr As New List(Of Dictionary(Of String, Object))()
            Dim dataErr As New Dictionary(Of String, Object)()
            dataErr.Add("error", ex.Message & " " & ex.StackTrace)
            lstErr.Add(dataErr)
            JSONresult = serializer.Serialize(lstErr)
            Response.Write(JSONresult)
        End Try

    End Sub
    ' SIIIIIIIIIIIIIII
    Sub lstPersonal()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'tb = obj.TraerDataTable("ListaPersonalContrato")
        tb = obj.TraerDataTable("SEG_ListaPersonal")
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_id", tb.Rows(i).Item("codigo_Per"))
            Data.Add("d_des", tb.Rows(i).Item("Trabajador"))
            Data.Add("c_id", tb.Rows(i).Item("codigo_Cco"))
            Data.Add("c_des", tb.Rows(i).Item("descripcion_Cco"))
            Data.Add("u_des", tb.Rows(i).Item("usuario_per"))
            Data.Add("x_id", tb.Rows(i).Item("codigo_Cgo"))
            Data.Add("x_des", tb.Rows(i).Item("descripcion_Cgo"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIIIIIIIIIII
    Sub LstDetalleTraslado()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoT As String = Request("param1")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("AF_ListarDetalleTrasladosAF", CodigoT)
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_dtt", tb.Rows(i).Item("c_dtt"))
            Data.Add("c_dtr", tb.Rows(i).Item("c_dtr"))
            Data.Add("c_per", tb.Rows(i).Item("c_per"))
            Data.Add("d_nom", tb.Rows(i).Item("d_nom"))
            Data.Add("c_art", tb.Rows(i).Item("c_ped"))
            Data.Add("d_art", tb.Rows(i).Item("d_ped"))
            Data.Add("d_cant", tb.Rows(i).Item("d_cant"))
            Data.Add("d_est", tb.Rows(i).Item("d_est"))
            Data.Add("u_nom", tb.Rows(i).Item("u_nom"))
            Data.Add("c_af", tb.Rows(i).Item("c_af"))
            Data.Add("c_uba", tb.Rows(i).Item("c_uba"))
            Data.Add("c_cco", tb.Rows(i).Item("c_cco"))
            Data.Add("d_cco", tb.Rows(i).Item("d_cco"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub AddDetTraslado()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoTra As Integer = Request("hdId")
        Dim CodigoArt As Integer = Request("hdArt")
        Dim Cantidad As Integer = Request("txtCant")
        Dim CodigoPer As Integer = Request("hdPer")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("AF_RegistrarDetTraslado", CodigoTra, CodigoPer, CodigoArt, Cantidad)
        obj.CerrarConexion()
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

    Sub ActualizarTraslado()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoId As Integer = Request("param1")
        Dim CodigoAc As String = Request("hdtipoA")
        Dim CodigoTE As String = Request("hdTE")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        If CodigoTE = "D" Then
            tb = obj.TraerDataTable("AF_ActualizarDetTraslado", CodigoId, CodigoAc)
        Else
            tb = obj.TraerDataTable("AF_ActualizarDetTraslado")
        End If

        obj.CerrarConexion()
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
    ' SIIIIIIIIIIIIIIIIIIII
    Sub TrasladoMercaderiaV2()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Data As New Dictionary(Of String, Object)()

            Dim tipoA As String = Request("hdtipoA")
            Dim fechaT As String = Request("txtFechaTraslado")
            Dim tipoT As String = Request("dpTipo")
            Dim CodigoPer As Integer = Request("hdPer")
            Dim CodigoCco As Integer = Request("hdCco")
            Dim ObservacionT As String = Request("txtObservacion")
            Dim EstadoT As Integer = Request("hdEstado")
            Dim motivoT As String = Request("dpMotivo")

            Dim DetalleA As String = Request("hdDetalleArt")

            Dim DetalleU As String = Request("hdUbi")
            Dim DetalleX As String = Request("hdAsi")
            Dim DetalleC As String = Request("hdCen")

            Dim User As Integer = Request("hdUser")

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim tbDet As New Data.DataTable
            Dim cn As New clsaccesodatos
            Dim codigoT As Integer = 0

            Dim nro_cor As String = ""
            Dim nro_traslado As String = ""

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If (tipoA = "A") Then
                codigoT = Request("hdId")
            Else
                codigoT = 0
            End If

            tb = obj.TraerDataTable("AF_RegistrarTraslado", codigoT, tipoA, fechaT, tipoT, CodigoPer, CodigoCco, ObservacionT, EstadoT, DetalleA, DetalleU, DetalleX, DetalleC, motivoT, User)

            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                Data.Add("Status", tb.Rows(i).Item("Status"))
                Data.Add("Message", tb.Rows(i).Item("Message"))
                Data.Add("Code", tb.Rows(i).Item("Code"))
                nro_cor = tb.Rows(i).Item("Nro")
                nro_traslado = tb.Rows(i).Item("Code")
                list.Add(Data)
            Next
            JSONresult = serializer.Serialize(list)

            If tb.Rows.Count > 0 Then
                obj.AbrirConexion()
                tbDet = obj.TraerDataTable("AF_DetalleTrasladoAF_V2", nro_traslado)
                'tbDet = obj.TraerDataTable("AF_DetalleTrasladoAF_V2", 0)
                obj.AbrirConexion()
            End If

            'Data.Add("DataBase", "OK")

            ' ===================================================================================================================
            ' Autor         :   ENevado
            ' Fecha         :   2018-10-18
            ' Observacion   :   Creacion de Archivo PDF en momoria temporal y Enviar por correo
            ' ===================================================================================================================

            ' 1.- Creacion de Archivo PDF -------------------------------------------------------------------------------------------

            Dim pdfDoc As New iTextSharp.text.Document()
            Dim memory As New MemoryStream
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(7)
            pdfTable.WidthPercentage = 100.0F

            ' Titulo
            Dim titleTab As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FICHA DE RETIRO DEL ACTIVO FIJO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9.0F, iTextSharp.text.Font.BOLD)))
            titleTab.Colspan = 7
            titleTab.HorizontalAlignment = 1
            pdfTable.AddCell(titleTab)

            ' Cabecera
            'Dim srcIcon As String = "http://www.usat.edu.pe/web/wp-content/uploads/2017/08/logo_usat.png"
            'Dim srcIcon As String = "../../../images/logousatvisa.png"
            Dim srcIcon As String = Server.MapPath(".") & "/logo_usat.png"
            'Dim uriIcon As Uri = New Uri(srcIcon)
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(40.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 2
            cellIcon.Rowspan = 2
            pdfTable.AddCell(cellIcon)

            Dim cellCompany As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UNIVERSIDAD CATOLICA SANTO TORIBIO DE MOGROVEJO" & Environment.NewLine & _
                                                      "RUC: 20395492129" & Environment.NewLine & _
                                                      "Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F)))
            cellCompany.Colspan = 5
            cellCompany.Rowspan = 2
            pdfTable.AddCell(cellCompany)

            Dim cellNro As iTextSharp.text.pdf.PdfPCell
            cellNro = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FICHA N° " & Environment.NewLine & _
                                                                                  nro_cor, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            pdfTable.AddCell(cellNro)

            Dim cellDate As iTextSharp.text.pdf.PdfPCell
            cellDate = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA: " & Environment.NewLine _
                                                                                   & Now.Date, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            pdfTable.AddCell(cellDate)

            ' Informacion
            Dim cellInfo As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("INFORMACIÓN GENERAL", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F, iTextSharp.text.Font.BOLD)))
            cellInfo.Colspan = 7
            cellInfo.HorizontalAlignment = 1
            pdfTable.AddCell(cellInfo)

            ' Solicitante
            Dim cellI1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("SOLICITANTE:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI1.Colspan = 1
            pdfTable.AddCell(cellI1)

            Dim cellI1Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Request("txtAsignado").ToString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI1Data.Colspan = 5
            pdfTable.AddCell(cellI1Data)

            ' Otros
            Dim cellOtros As iTextSharp.text.pdf.PdfPCell
            cellOtros = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("OBSERVACION:" & Environment.NewLine & _
                                                    Request("txtObservacion").ToString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellOtros.Colspan = 1
            cellOtros.Rowspan = 10
            pdfTable.AddCell(cellOtros)

            ' Area
            Dim cellI2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("AREA SOLICITANTE:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI2.Colspan = 1
            pdfTable.AddCell(cellI2)

            Dim cellI2Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Request("txtCco").ToString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI2Data.Colspan = 5
            pdfTable.AddCell(cellI2Data)

            ' Ubicacion
            Dim cellI3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UBICACION FISICA:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI3.Colspan = 1
            pdfTable.AddCell(cellI3)

            Dim cellI3Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(Request("txtUbicacion").ToString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI3Data.Colspan = 5
            pdfTable.AddCell(cellI3Data)

            ' Tipo Espacio
            Dim cellI4 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("TIPO DE ESPACIO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI4.Colspan = 1
            pdfTable.AddCell(cellI4)

            Dim cellI4Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI4Data.Colspan = 5
            pdfTable.AddCell(cellI4Data)

            ' Fecha Retiro
            Dim cellI5 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA DE RETIRO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI5.Colspan = 1
            pdfTable.AddCell(cellI5)

            Dim cellI5Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(fechaT, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI5Data.Colspan = 5
            pdfTable.AddCell(cellI5Data)

            ' Hora Retiro
            Dim cellI6 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("HORA DE RETIRO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI6.Colspan = 1
            pdfTable.AddCell(cellI6)

            Dim cellI6Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI6Data.Colspan = 5
            pdfTable.AddCell(cellI6Data)

            ' Fecha Devolucion
            Dim cellI7 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("FECHA DEVOLUCION(*):", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI7.Colspan = 1
            pdfTable.AddCell(cellI7)

            Dim cellI7Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI7Data.Colspan = 5
            pdfTable.AddCell(cellI7Data)

            ' Retiro Interno
            Dim cellI8 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("RETIRO INTERNO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI8.Colspan = 1
            pdfTable.AddCell(cellI8)

            Dim cellI8Data As iTextSharp.text.pdf.PdfPCell
            cellI8Data = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" MANTENIMIENTO ( " & IIf(tipoT = "I" And motivoT = "2", "X", "") & _
                                                     " )        PRESTAMO ( " & IIf(tipoT = "I" And motivoT = "3", "X", "") & _
                                                     " )        REASIGNACION ( " & IIf(tipoT = "I" And motivoT = "1", "X", "") & _
                                                     " ) ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI8Data.Colspan = 5
            pdfTable.AddCell(cellI8Data)

            ' Retiro Externo
            Dim cellI9 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("RETIRO EXTERNO:", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI9.Colspan = 1
            pdfTable.AddCell(cellI9)

            Dim cellI9Data As iTextSharp.text.pdf.PdfPCell
            cellI9Data = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" MANTENIMIENTO ( " & IIf(tipoT = "E" And motivoT = "2", "X", "") & _
                                                     " )        PRESTAMO ( " & IIf(tipoT = "E" And motivoT = "3", "X", "") & _
                                                     " )        REASIGNACION ( " & IIf(tipoT = "E" And motivoT = "1", "X", "") & _
                                                     " ) ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI9Data.Colspan = 5
            pdfTable.AddCell(cellI9Data)

            ' Documento Relacionado
            Dim cellI10 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DOCUMENTO RELACIONADO(**):", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellI10.Colspan = 2
            pdfTable.AddCell(cellI10)

            Dim cellI10Data As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
            cellI10Data.Colspan = 4
            pdfTable.AddCell(cellI10Data)

            ' Notas (*)
            Dim cellNotas As iTextSharp.text.pdf.PdfPCell
            cellNotas = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                    " (*)  SI ES POR ELIMINACIÓN NO CONSIDERAR" & Environment.NewLine & _
                                                    " (**) EN CASO" & Environment.NewLine & _
                                                    " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellNotas.Border = 0
            cellNotas.Colspan = 7
            pdfTable.AddCell(cellNotas)

            ' Descripcion del Bien
            Dim cellDescripcion As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DESCRIPCIÓN DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8.0F, iTextSharp.text.Font.BOLD)))
            cellDescripcion.Colspan = 7
            cellDescripcion.HorizontalAlignment = 1
            pdfTable.AddCell(cellDescripcion)

            ' Codigo del Inventario
            Dim cellDC1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("CODIGO DEL INVENTARIO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC1.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC1)

            ' Descripcion
            Dim cellDC2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("DESCRIPCIÓN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC2.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC2)

            ' Marca
            Dim cellDC3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("MARCA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC3.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC3)

            ' Modelo
            Dim cellDC4 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("MODELO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC4.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC4)

            ' Serie
            Dim cellDC5 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("SERIE", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC5.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC5)

            ' Unidad Medida
            Dim cellDC6 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("UN. MEDIDA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC6.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC6)

            ' Cantidad
            Dim cellDC7 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase("CANTIDAD", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellDC7.HorizontalAlignment = 1
            pdfTable.AddCell(cellDC7)



            ' Detalle Descripcion
            For x As Integer = 0 To 19

                If x > (tbDet.Rows.Count - 1) Then
                    ' Codigo de Inventario
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Descripcion
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Marca
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Modelo
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Serie
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Unidad Medida
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Cantidad
                    pdfTable.AddCell(New iTextSharp.text.Phrase(" ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                Else
                    ' Codigo de Inventario
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("etiqueta_af"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Descripcion
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("descripcionArt"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Marca
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Marca"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Modelo
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Modelo"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Serie
                    pdfTable.AddCell(New iTextSharp.text.Phrase(tbDet.Rows(x).Item("Serie"), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Unidad Medida
                    pdfTable.AddCell(New iTextSharp.text.Phrase("UND.", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                    ' Cantidad
                    pdfTable.AddCell(New iTextSharp.text.Phrase("1", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F)))
                End If

            Next

            ' Salto de Celda
            Dim cellBr As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellBr.Border = 0
            cellBr.Colspan = 7
            pdfTable.AddCell(cellBr)

            ' Intenerario
            Dim cellIntenerario As iTextSharp.text.pdf.PdfPCell
            cellIntenerario = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                          " ITINERARIO (LLENAR SOLO SI SE LLEVARAN BIEN ES POR COMISION)" & Environment.NewLine & _
                                                          " COMISION A:" & Environment.NewLine & _
                                                          " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellIntenerario.Colspan = 7
            pdfTable.AddCell(cellIntenerario)

            ' CeldaNone
            Dim cellNone1 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone1.Border = 0
            pdfTable.AddCell(cellNone1)

            ' Firma Solicitante
            Dim cellFirma1 As iTextSharp.text.pdf.PdfPCell
            cellFirma1 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  ___________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      SOLICITANTE", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma1.Border = 0
            cellFirma1.Colspan = 2
            pdfTable.AddCell(cellFirma1)

            ' CeldaNone
            Dim cellNone2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone2.Border = 0
            pdfTable.AddCell(cellNone2)

            ' Firma Responsable
            Dim cellFirma2 As iTextSharp.text.pdf.PdfPCell
            cellFirma2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                    ________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      RESPONSABLE DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma2.Border = 0
            cellFirma2.Colspan = 2
            pdfTable.AddCell(cellFirma2)

            ' CeldaNone
            Dim cellNone3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(""))
            cellNone3.Border = 0
            pdfTable.AddCell(cellNone3)

            ' Firma Director de Area
            Dim cellFirma3 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  _______________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      DIRECTOR DE AREA", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma3.Border = 0
            cellFirma3.Colspan = 2
            pdfTable.AddCell(cellFirma3)

            ' Firma Director de Area
            Dim cellFirma4 As iTextSharp.text.pdf.PdfPCell
            cellFirma4 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                      _____________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                          RESPONSABLE DEL BIEN", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma4.Border = 0
            cellFirma4.Colspan = 3
            pdfTable.AddCell(cellFirma4)

            ' Firma Comision de Activo Fijo
            Dim cellFirma5 As iTextSharp.text.pdf.PdfPCell
            cellFirma5 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                  __________________________" & Environment.NewLine & _
                                                     " " & Environment.NewLine & _
                                                     "                   COMISION DE ACTIVO FIJO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFirma5.Border = 0
            cellFirma5.Colspan = 2
            pdfTable.AddCell(cellFirma5)

            ' Salto de Celda
            Dim cellBr2 As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & " ", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellBr2.Border = 0
            cellBr2.Colspan = 7
            pdfTable.AddCell(cellBr2)

            ' Pie de Pagina
            Dim cellFoot As iTextSharp.text.pdf.PdfPCell
            cellFoot = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(" " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    " " & Environment.NewLine & _
                                                    "USUARIO: " & Request("txtPersonal").ToString & Environment.NewLine & _
                                                    "FECHA: " & Now.Date & " HORA: " & Now.ToShortTimeString, New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7.0F, iTextSharp.text.Font.BOLD)))
            cellFoot.Border = 0
            cellFoot.Colspan = 7
            pdfTable.AddCell(cellFoot)

            pdfDoc.Add(pdfTable)

            pdfDoc.Close()

            'Data.Add("PDF", "OK")

            Dim bytes() As Byte = memory.ToArray
            memory.Close()

            ' 2.- Creacion de Email para envio de PDF ----------------------------------------------------------------------

            Dim objEmail As New ClsMail
            Dim cuerpo, receptor, AsuntoCorreo, cco As String
            cuerpo = "<html>"
            cuerpo = cuerpo & "<head>"
            cuerpo = cuerpo & "<title></title>"
            cuerpo = cuerpo & "<style>"
            cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
            cuerpo = cuerpo & "</style>"
            cuerpo = cuerpo & "</head>"
            cuerpo = cuerpo & "<body>"
            cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
            cuerpo = cuerpo & "<tr><td colspan=2><b>Estimado(a):</b></td></tr>"
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>El documento de traslado ha sido generado.</td></tr>"
            cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
            cuerpo = cuerpo & "</table>"
            cuerpo = cuerpo & "</body>"
            cuerpo = cuerpo & "</html>"
            'receptor = "enevado@usat.edu.pe"
            receptor = Request("hdEmailUser")
            AsuntoCorreo = "[Documento de Traslado]"

            'cco = "cmonja@usat.edu.pe;csenmache@usat.edu.pe"
            'cco = Request("hdEmailAsig") + "enevado@usat.edu.pe"
            cco = Request("hdEmailAsig") + "cmonja@usat.edu.pe"

            objEmail.EnviarPDFMail("campusvirtual@usat.edu.pe", "Documento de Traslado", receptor, AsuntoCorreo, cuerpo, True, "traslado", New MemoryStream(bytes), cco)

            'Data.Add("Email", "OK")

            'list.Add(Data)

            Response.Write(JSONresult)

        Catch ex As Exception
            Dim lstErr As New List(Of Dictionary(Of String, Object))()
            Dim dataErr As New Dictionary(Of String, Object)()
            'dataErr.Add("Status", "Fail")
            dataErr.Add("error", ex.Message & " " & ex.StackTrace)
            lstErr.Add(dataErr)
            JSONresult = serializer.Serialize(lstErr)
            Response.Write(JSONresult)
        End Try

    End Sub
    ' SIIIIIIIIIIIIIIIIII
    Sub EliminarTraslado()
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Codigo As String = Request("param1")

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tb = obj.TraerDataTable("AF_EliminarTraslado", Codigo)

            obj.CerrarConexion()
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


        Catch ex As Exception
            Response.Write("messaaaaage eliminar" & ex.Message.ToString)
        End Try

    End Sub
    ' SIIIIIIIIIIIIII
    Sub lstRegistroActivo()
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Codigo As String = Request("param1")
            Dim Codigo_art As String = Request("param2")
            Dim Codigo_egr As String = Request("hdIEgreso")
            Dim tipo As String = Request("tipo")

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If (tipo = "P") Then
                tb = obj.TraerDataTable("AF_retornarPedidoAF", Codigo)
            Else
                tb = obj.TraerDataTable("AF_retornarDetallePedidoAF", Codigo, Codigo_art, Codigo_egr)
            End If

            obj.CerrarConexion()
            For i As Integer = 0 To tb.Rows.Count - 1
                Dim Data As New Dictionary(Of String, Object)()
                If (tipo = "P") Then
                    Data.Add("c_ped", tb.Rows(i).Item("c_ped"))
                    Data.Add("c_per", tb.Rows(i).Item("c_per"))
                    Data.Add("c_cco", tb.Rows(i).Item("c_cco"))
                    Data.Add("d_epe", tb.Rows(i).Item("d_epe"))
                    Data.Add("d_imp", tb.Rows(i).Item("d_imp"))
                    Data.Add("d_obs", tb.Rows(i).Item("d_obs"))
                    Data.Add("d_fec", tb.Rows(i).Item("d_fec"))
                    Data.Add("c_ipl", tb.Rows(i).Item("c_ipl"))
                    Data.Add("c_ejp", tb.Rows(i).Item("c_ejp"))
                    Data.Add("c_alm", tb.Rows(i).Item("c_alm"))
                    Data.Add("d_alm", tb.Rows(i).Item("d_alm"))
                    Data.Add("d_per", tb.Rows(i).Item("d_per"))
                    Data.Add("d_cco", tb.Rows(i).Item("d_cco"))
                Else
                    Data.Add("c_dpe", tb.Rows(i).Item("c_dpe"))
                    Data.Add("c_ped", tb.Rows(i).Item("c_ped"))
                    Data.Add("c_art", tb.Rows(i).Item("c_art"))
                    Data.Add("c_cco", tb.Rows(i).Item("c_cco"))
                    Data.Add("d_pre", tb.Rows(i).Item("d_pre"))
                    Data.Add("d_cant", tb.Rows(i).Item("d_cant"))
                    Data.Add("d_obs", tb.Rows(i).Item("d_obs"))
                    Data.Add("d_fec", tb.Rows(i).Item("d_fec"))
                    Data.Add("d_est", tb.Rows(i).Item("d_est"))
                    Data.Add("d_tip", tb.Rows(i).Item("d_tip"))
                    Data.Add("d_mod", tb.Rows(i).Item("d_mod"))
                    Data.Add("c_ppr", tb.Rows(i).Item("c_ppr"))
                    Data.Add("c_dpr", tb.Rows(i).Item("c_dpr"))
                    Data.Add("c_uni", tb.Rows(i).Item("c_uni"))
                    Data.Add("d_uni", tb.Rows(i).Item("d_uni"))
                    Data.Add("d_abr", tb.Rows(i).Item("d_abr"))
                    Data.Add("c_per", tb.Rows(i).Item("c_per"))
                    Data.Add("d_per", tb.Rows(i).Item("d_per"))
                    Data.Add("d_con", tb.Rows(i).Item("d_con"))
                    Data.Add("d_tot", tb.Rows(i).Item("d_tot"))
                    Data.Add("d_dct", tb.Rows(i).Item("d_dct"))
                    Data.Add("d_esp", tb.Rows(i).Item("d_esp"))
                    Data.Add("c_dap", tb.Rows(i).Item("c_dap"))
                    Data.Add("d_art", tb.Rows(i).Item("d_art"))
                    Data.Add("d_cco", tb.Rows(i).Item("d_cco"))
                    Data.Add("nro_comp", tb.Rows(i).Item("nro_comp"))
                    Data.Add("d_filas", tb.Rows(i).Item("d_filas"))
                    Data.Add("c_egr", tb.Rows(i).Item("c_egr"))
                End If
                list.Add(Data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)


        Catch ex As Exception
            Response.Write("message lstRegistroActivo" & ex.Message.ToString)
        End Try

    End Sub
    ' SIIIIIIIIIIIIIII
    Sub LstArtCrtRegAF()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_Art As Integer = Request("hdArt")
        'Dim Codigo_Ser As Integer = Request("hdIEgreso")
        Dim Codigo_Ser As Integer = Request("hdSerie")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("***" & Codigo_Art)

        tb = obj.TraerDataTable("AF_ListarArticuloCaracteristica_registroAF", Codigo_Art, Codigo_Ser)

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_chk", tb.Rows(i).Item("chkCrt"))
            Data.Add("c_idO", tb.Rows(i).Item("id_aco"))
            Data.Add("c_idC", tb.Rows(i).Item("id_crt"))
            Data.Add("c_idA", tb.Rows(i).Item("id_Art"))
            Data.Add("d_crt", tb.Rows(i).Item("d_crt"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            Data.Add("d_val", tb.Rows(i).Item("d_val"))
            Data.Add("d_est", tb.Rows(i).Item("d_est"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIIIIII
    Sub RegistrarDetalleAF()
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim codigo_daf As Integer = 0
            Dim codigo_ped As Integer = Request("txtNroPedido")
            Dim IdEgreso As Integer = Request("hdIdEgresoS")
            Dim NroPedido As Integer = Request("txtNroPedido")
            '$('#hdArt1').val()
            Dim IdArtp As Integer = Request("hdArt1")
            Dim IdArt As Integer = Request("hdArt")

            Dim Serie As String = Request("txtSerie")
            Dim hSerie As String = Request("hdSerie")

            Dim DetCodigo_aco As String = Request("hdDetalleIdCCO")
            'Response.Write("-----------" & DetCodigo_aco)
            Dim DetValor_aco As String = Request("hdDetalleValorCCO")
            'Response.Write("-----------" & DetValor_aco)
            Dim hdAccion As String = Request("hdAccion")
            Dim estado As String = ""

            If (hdAccion = "REG") Then
                estado = "B"
            End If

            'Dim hdUnid As Integer = Request("hdUnid")
            Dim hdUnid As Integer = 1
            Dim hdNumeroEs As Integer = Request("hdNumeroEs")
            Dim Usuario As String = "Usuario"

            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Response.Write("++++++++++++")
            'Response.Write(codigo_daf & "-" & IdEgreso & "-" & codigo_ped & "-" & IdArtp & "-" & IdArt & "-" & hSerie & "-" & DetCodigo_aco & "-" & DetValor_aco & "-" & estado & "-" & hdAccion & "-" & hdNumeroEs & "-" & hdNumeroEs & "-" & Usuario)

            tb = obj.TraerDataTable("AF_registrarDetalleAF", codigo_daf, IdEgreso, codigo_ped, IdArtp, IdArt, hSerie, DetCodigo_aco, DetValor_aco, estado, hdAccion, hdNumeroEs, hdNumeroEs, Usuario)

            obj.CerrarConexion()
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

        Catch ex As Exception
            Response.Write("message registrar Detalle AF:" & ex.Message.ToString)
        End Try

    End Sub
    ' SIIIIIIIIIIIIIII
    Sub LstArticuloComponentesRegAF()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoArt As Integer = Request("hdArt")
        Dim CodigoPed As Integer = Request("txtNroPedido")
        'Dim CodigoSer As Integer = Request("hdIEgreso")
        Dim Serie As String = Request("hdSerieB")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("AF_ListarArticuloComponente---" & CodigoArt & "--" & "-ELI-" & "USR-")
        tb = obj.TraerDataTable("AF_ListarArticuloComponente_RegistrarAF", CodigoArt, CodigoPed, Serie)
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("c_chk", tb.Rows(i).Item("chkArt"))
            Data.Add("c_dar", tb.Rows(i).Item("c_dar"))
            Data.Add("c_hart", tb.Rows(i).Item("c_hart"))
            Data.Add("c_part", tb.Rows(i).Item("c_part"))
            Data.Add("d_com", tb.Rows(i).Item("d_com"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    ' SIIIIIIIIIIIIIII
    Sub LstDetalleRegistroAF()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoPed As Integer = Request("param1")
        Dim CodigoArt As String = Request("param2")
        Dim CodigoEgr As String = Request("hdIEgreso")
        ''Dim CodigoArt As Integer = Request("hd_uArt")
        ''Dim CodigoEgr As Integer = Request("hd_uEgr")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("*****************************")
        tb = obj.TraerDataTable("AF_listadoDetalleRegistroAF", CodigoPed, CodigoArt, CodigoEgr)
        ''tb = obj.TraerDataTable("AF_listadoDetalleRegistroAF_v2", CodigoPed, CodigoArt, CodigoEgr)
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_nes", tb.Rows(i).Item("d_nes"))
            Data.Add("c_art", tb.Rows(i).Item("c_art"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            Data.Add("c_ser", tb.Rows(i).Item("c_ser"))
            Data.Add("d_ser", tb.Rows(i).Item("d_ser"))
            Data.Add("c_egr", tb.Rows(i).Item("c_egr"))
            Data.Add("d_nro", tb.Rows(i).Item("d_nro"))
            Data.Add("c_uni", tb.Rows(i).Item("c_uni"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    ' SIIIIIIIIIIIIIII
    Sub LstDetalleRegistroAF_v2()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim CodigoPed As Integer = Request("param1")
        ''Dim CodigoArt As String = Request("param2")
        ''Dim CodigoEgr As String = Request("hdIEgreso")
        Dim CodigoArt As Integer = Request("hd_uArt")
        Dim CodigoEgr As Integer = Request("hd_uEgr")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("*****************************")
        ''tb = obj.TraerDataTable("AF_listadoDetalleRegistroAF", CodigoPed, CodigoArt, CodigoEgr)
        tb = obj.TraerDataTable("AF_listadoDetalleRegistroAF_v2", CodigoPed, CodigoArt, CodigoEgr)
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_nes", tb.Rows(i).Item("d_nes"))
            Data.Add("c_art", tb.Rows(i).Item("c_art"))
            Data.Add("d_art", tb.Rows(i).Item("d_art"))
            Data.Add("c_ser", tb.Rows(i).Item("c_ser"))
            Data.Add("d_ser", tb.Rows(i).Item("d_ser"))
            Data.Add("c_egr", tb.Rows(i).Item("c_egr"))
            Data.Add("d_nro", tb.Rows(i).Item("d_nro"))
            Data.Add("c_uni", tb.Rows(i).Item("c_uni"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    ' SIIIIIIIIIIIIIIIIII
    Sub ClonarDetalleAF()
        Dim obj As New ClsConectarDatos
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Codigo_ped As Integer = Request("txtNroPedido")
            Dim IdEgreso As Integer = Request("hdIdEgresoS")
            Dim Codigo_art As Integer = Request("hdArt")
            Dim Unid As Integer = Request("hdUnid")
            Dim Serie As String = Request("hdSerie")
            Dim N As Integer = Request("hdCClonar")
            Dim tipo As String = Request("hdAccion")
            Dim Usuario As String = "Usuario"

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            obj.IniciarTransaccion()
            Dim estado As Integer = 0
            For i As Integer = 1 To N - 1
                estado = i + 1
                tb = obj.TraerDataTable("AF_ClonarDetalleAF", Codigo_ped, IdEgreso, Codigo_art, Unid, Serie, estado, "CLO", Usuario)
            Next
            obj.TerminarTransaccion()
            obj.CerrarConexion()

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

        Catch ex As Exception
            Response.Write("CLONACION ERROR:" & ex.Message.ToString)
            obj.AbortarTransaccion()
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "error")
            Data.Add("Message", "Error en la clonación")
            Data.Add("Code", "0")

        End Try

    End Sub

    ' SIIIIIIIIIIIIIIIIII
    Sub ActualizarSerieEgreso()
        Dim obj As New ClsConectarDatos
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Codigo_ped As Integer = Request("txtNroPedido")
            Dim IdEgreso As Integer = Request("hdIdEgresoS")
            Dim Codigo_art As Integer = Request("hdArt")
            Dim Numero As Integer = Request("hdUnid")
            Dim Numero1 As String = Request("hdNumeroEs")
            Dim Serie As String = Request("txtSerie")
            Dim tipo As String = Request("hdAccion")
            Dim Usuario As String = "Usuario"

            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Response.Write("********" & Serie)
            'Response.Write("***AF_ActualizarSerieEgresoAF-" & Codigo_ped & "-" & IdEgreso & "-" & Codigo_art & "-" & Serie & "-" & Numero & "-" & Numero1 & "-" & tipo & "-" & Usuario)
            tb = obj.TraerDataTable("AF_ActualizarSerieEgresoAF", Codigo_ped, IdEgreso, Codigo_art, Serie, Numero, Numero1, tipo, Usuario)
            obj.CerrarConexion()

            For i As Integer = 0 To tb.Rows.Count - 1
                'Response.Write("-----------")
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

        Catch ex As Exception
            'Response.Write("++++++++++")
            'Response.Write("messaaaaage eliminar" & ex.Message.ToString)
            obj.AbortarTransaccion()
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "error")
            Data.Add("Message", "Error en la actualizacion de Serie")
            Data.Add("Code", "0")

        End Try
    End Sub

    ' SIIIIIIIIIIIIIII
    Sub lstDetallePedidoAlmDes()
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Response.Write("pa_listadoDetalleRegistroAF ---" & CodigoPed & "--" & CodigoArt & "--")
        'Response.Write("*****************************")
        tb = obj.TraerDataTable("AF_ListadoDetallePedidoEnAlmDesp")
        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_id", tb.Rows(i).Item("id"))
            Data.Add("d_des", tb.Rows(i).Item("descripcion"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub validaDetalleCantPed()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_Dpe As Integer = Request("param1")
        Dim Cantidad As Integer = Request("param2")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("AF_ValidarDetalleCantPedido", Codigo_Dpe, Cantidad)

        obj.CerrarConexion()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("d_sw", tb.Rows(i).Item("sw"))
            Data.Add("d_cantD", tb.Rows(i).Item("CantDisponible"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub validaCantidadDisponibleTraslado()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim Codigo_Dpe As Integer = Request("param1")
        Dim idArt As Integer = Request("param2")
        Dim Cantidad As Integer = Request("param3")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("AF_validaCantidadDisponibleLogistica", Codigo_Dpe, idArt, Cantidad)

        obj.CerrarConexion()
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

    Sub mt_lstActivoFijo()
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listarActivoFijo")
            obj.CerrarConexion()
            For x As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("d_id", dt.Rows(x).Item("id"))
                data.Add("d_des", dt.Rows(x).Item("descripcion"))
                data.Add("d_eti", dt.Rows(x).Item("etiqueta_af"))
                data.Add("d_nom", dt.Rows(x).Item("descripcionArt"))
                data.Add("d_mar", dt.Rows(x).Item("Marca"))
                data.Add("d_mod", dt.Rows(x).Item("Modelo"))
                data.Add("d_ser", dt.Rows(x).Item("Serie"))
                list.Add(data)
            Next
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
            Dim JSONresult As String = ""
            serializer.MaxJsonLength = Int32.MaxValue
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim data As New Dictionary(Of String, Object)()
            data.Add("error", ex.Message & "   " & ex.StackTrace)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub mt_lstUbicacion()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        Dim JSONresult As String = ""
        Try
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("AF_listadoUbicacionAF", 0)
            obj.CerrarConexion()
            For x As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("d_id", dt.Rows(x).Item("codigo_uba"))
                data.Add("d_des", dt.Rows(x).Item("descripcion_uba"))
                List.Add(data)
            Next
            JSONresult = serializer.Serialize(List)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim lstErr As New List(Of Dictionary(Of String, Object))()
            Dim dataErr As New Dictionary(Of String, Object)()
            dataErr.Add("error", ex.Message & " " & ex.StackTrace)
            JSONresult = serializer.Serialize(lstErr)
            Response.Write(JSONresult)
        End Try
    End Sub

    Sub mt_GenerarPdf()
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer
        'Dim JSONresult As String = ""
        'Try

        '    Dim list As New List(Of Dictionary(Of String, Object))()
        '    Dim Data As New Dictionary(Of String, Object)()

        '    Dim pdfDoc As New Document()
        '    Dim memory As New MemoryStream
        '    Dim pdfWrite As PdfWriter = PdfWriter.GetInstance(pdfDoc, memory)

        '    pdfDoc.Open()

        '    Dim pdfTable = New PdfPTable(7)
        '    pdfTable.WidthPercentage = 100.0F

        '    ' Titulo
        '    Dim titleTab = New PdfPCell(New Phrase("FICHA DE RETIRO DEL ACTIVO FIJO", New Font(Font.FontFamily.HELVETICA, 9.0F, Font.BOLD)))
        '    titleTab.Colspan = 7
        '    titleTab.HorizontalAlignment = 1
        '    pdfTable.AddCell(titleTab)

        '    ' Cabecera
        '    Dim srcIcon As String = "http://www.usat.edu.pe/web/wp-content/uploads/2017/08/logo_usat.png"
        '    Dim uriIcon = New Uri(srcIcon)
        '    Dim usatIcon As Image = iTextSharp.text.Image.GetInstance(uriIcon)
        '    usatIcon.ScalePercent(40.0F)
        '    usatIcon.Alignment = Element.ALIGN_LEFT

        '    Dim cellIcon = New PdfPCell(usatIcon)
        '    cellIcon.HorizontalAlignment = 1
        '    cellIcon.VerticalAlignment = 2
        '    cellIcon.Rowspan = 2
        '    pdfTable.AddCell(cellIcon)

        '    Dim cellCompany = New PdfPCell(New Phrase("UNIVERSIDAD CATOLICA SANTO TORIBIO DE MOGROVEJO" & Environment.NewLine & _
        '                                              "RUC: 20395492129" & Environment.NewLine & _
        '                                              "Av. San Josemaría Escrivá de Balaguer Nº 855 Chiclayo - Perú", New Font(Font.FontFamily.HELVETICA, 8.0F)))
        '    cellCompany.Colspan = 5
        '    cellCompany.Rowspan = 2
        '    pdfTable.AddCell(cellCompany)

        '    Dim cellNro = New PdfPCell(New Phrase("FICHA N° XXX", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    pdfTable.AddCell(cellNro)

        '    Dim cellDate = New PdfPCell(New Phrase("FECHA: " & Now.Date, New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    pdfTable.AddCell(cellDate)

        '    ' Informacion
        '    Dim cellInfo = New PdfPCell(New Phrase("INFORMACIÓN GENERAL", New Font(Font.FontFamily.HELVETICA, 8.0F, Font.BOLD)))
        '    cellInfo.Colspan = 7
        '    cellInfo.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellInfo)

        '    ' Solicitante
        '    Dim cellI1 = New PdfPCell(New Phrase("SOLICITANTE:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI1.Colspan = 1
        '    pdfTable.AddCell(cellI1)

        '    Dim cellI1Data = New PdfPCell(New Phrase(Request("txtPersonal").ToString, New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI1Data.Colspan = 5
        '    pdfTable.AddCell(cellI1Data)

        '    ' Otros
        '    Dim cellOtros = New PdfPCell(New Phrase("OTRO ESPECIFICAR", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellOtros.Colspan = 1
        '    cellOtros.Rowspan = 10
        '    pdfTable.AddCell(cellOtros)

        '    ' Area
        '    Dim cellI2 = New PdfPCell(New Phrase("AREA SOLICITANTE:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI2.Colspan = 1
        '    pdfTable.AddCell(cellI2)

        '    Dim cellI2Data = New PdfPCell(New Phrase(Request("txtCentroCo").ToString, New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI2Data.Colspan = 5
        '    pdfTable.AddCell(cellI2Data)

        '    ' Ubicacion
        '    Dim cellI3 = New PdfPCell(New Phrase("UBICACION FISICA:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI3.Colspan = 1
        '    pdfTable.AddCell(cellI3)

        '    Dim cellI3Data = New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI3Data.Colspan = 5
        '    pdfTable.AddCell(cellI3Data)

        '    ' Tipo Espacio
        '    Dim cellI4 = New PdfPCell(New Phrase("TIPO DE ESPACIO:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI4.Colspan = 1
        '    pdfTable.AddCell(cellI4)

        '    Dim cellI4Data = New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI4Data.Colspan = 5
        '    pdfTable.AddCell(cellI4Data)

        '    ' Fecha Retiro
        '    Dim cellI5 = New PdfPCell(New Phrase("FECHA DE RETIRO:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI5.Colspan = 1
        '    pdfTable.AddCell(cellI5)

        '    Dim cellI5Data = New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI5Data.Colspan = 5
        '    pdfTable.AddCell(cellI5Data)

        '    ' Hora Retiro
        '    Dim cellI6 = New PdfPCell(New Phrase("HORA DE RETIRO:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI6.Colspan = 1
        '    pdfTable.AddCell(cellI6)

        '    Dim cellI6Data = New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI6Data.Colspan = 5
        '    pdfTable.AddCell(cellI6Data)

        '    ' Fecha Devolucion
        '    Dim cellI7 = New PdfPCell(New Phrase("FECHA DEVOLUCION(*):", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI7.Colspan = 1
        '    pdfTable.AddCell(cellI7)

        '    Dim cellI7Data = New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI7Data.Colspan = 5
        '    pdfTable.AddCell(cellI7Data)

        '    ' Retiro Interno
        '    Dim cellI8 = New PdfPCell(New Phrase("RETIRO INTERNO:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI8.Colspan = 1
        '    pdfTable.AddCell(cellI8)

        '    Dim cellI8Data = New PdfPCell(New Phrase(" MANTENIMIENTO ( )        PRESTAMO ( )        REASIGNACION ( ) ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI8Data.Colspan = 5
        '    pdfTable.AddCell(cellI8Data)

        '    ' Retiro Externo
        '    Dim cellI9 = New PdfPCell(New Phrase("RETIRO EXTERNO:", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI9.Colspan = 1
        '    pdfTable.AddCell(cellI9)

        '    Dim cellI9Data = New PdfPCell(New Phrase(" MANTENIMIENTO ( )        PRESTAMO ( )        REASIGNACION ( ) ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI9Data.Colspan = 5
        '    pdfTable.AddCell(cellI9Data)

        '    ' Documento Relacionado
        '    Dim cellI10 = New PdfPCell(New Phrase("DOCUMENTO RELACIONADO(**):", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellI10.Colspan = 2
        '    pdfTable.AddCell(cellI10)

        '    Dim cellI10Data = New PdfPCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    cellI10Data.Colspan = 4
        '    pdfTable.AddCell(cellI10Data)

        '    ' Notas (*)
        '    Dim cellNotas = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                            " (*)  SI ES POR ELIMINACIÓN NO CONSIDERAR" & Environment.NewLine & _
        '                                            " (**) EN CASO" & Environment.NewLine & _
        '                                            " ", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellNotas.Border = 0
        '    cellNotas.Colspan = 7
        '    pdfTable.AddCell(cellNotas)

        '    ' Descripcion del Bien
        '    Dim cellDescripcion = New PdfPCell(New Phrase("DESCRIPCIÓN DEL BIEN", New Font(Font.FontFamily.HELVETICA, 8.0F, Font.BOLD)))
        '    cellDescripcion.Colspan = 7
        '    cellDescripcion.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDescripcion)

        '    ' Codigo del Inventario
        '    Dim cellDC1 = New PdfPCell(New Phrase("CODIGO DEL INVENTARIO", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC1.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC1)

        '    ' Descripcion
        '    Dim cellDC2 = New PdfPCell(New Phrase("DESCRIPCIÓN", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC2.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC2)

        '    ' Marca
        '    Dim cellDC3 = New PdfPCell(New Phrase("MARCA", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC3.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC3)

        '    ' Modelo
        '    Dim cellDC4 = New PdfPCell(New Phrase("MODELO", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC4.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC4)

        '    ' Serie
        '    Dim cellDC5 = New PdfPCell(New Phrase("SERIE", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC5.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC5)

        '    ' Unidad Medida
        '    Dim cellDC6 = New PdfPCell(New Phrase("UN. MEDIDA", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC6.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC6)

        '    ' Cantidad
        '    Dim cellDC7 = New PdfPCell(New Phrase("CANTIDAD", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellDC7.HorizontalAlignment = 1
        '    pdfTable.AddCell(cellDC7)

        '    ' Detalle Descripcion
        '    For x = 0 To 9
        '        ' Codigo de Inventario
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Descripcion
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Marca
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Modelo
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Serie
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Unidad Medida
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '        ' Cantidad
        '        pdfTable.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 7.0F)))
        '    Next

        '    ' Notas (*)
        '    Dim cellBr = New PdfPCell(New Phrase(" " & Environment.NewLine & " ", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellBr.Border = 0
        '    cellBr.Colspan = 7
        '    pdfTable.AddCell(cellBr)

        '    ' Intenerario
        '    Dim cellIntenerario = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                                  " ITINERARIO (LLENAR SOLO SI SE LLEVARAN BIEN ES POR COMISION)" & Environment.NewLine & _
        '                                                  " COMISION A:" & Environment.NewLine & _
        '                                                  " ", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellIntenerario.Colspan = 7
        '    pdfTable.AddCell(cellIntenerario)

        '    ' CeldaNone
        '    Dim cellNone1 = New PdfPCell(New Phrase(""))
        '    cellNone1.Border = 0
        '    pdfTable.AddCell(cellNone1)

        '    ' Firma Solicitante
        '    Dim cellFirma1 = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                  ___________________" & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                      SOLICITANTE", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellFirma1.Border = 0
        '    cellFirma1.Colspan = 2
        '    pdfTable.AddCell(cellFirma1)

        '    ' CeldaNone
        '    Dim cellNone2 = New PdfPCell(New Phrase(""))
        '    cellNone2.Border = 0
        '    pdfTable.AddCell(cellNone2)

        '    ' Firma Responsable
        '    Dim cellFirma2 = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                    ________________________" & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                      RESPONSABLE DEL BIEN", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellFirma2.Border = 0
        '    cellFirma2.Colspan = 2
        '    pdfTable.AddCell(cellFirma2)

        '    ' CeldaNone
        '    Dim cellNone3 = New PdfPCell(New Phrase(""))
        '    cellNone3.Border = 0
        '    pdfTable.AddCell(cellNone3)

        '    ' Firma Director de Area
        '    Dim cellFirma3 = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                  _______________________" & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                      DIRECTOR DE AREA", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellFirma3.Border = 0
        '    cellFirma3.Colspan = 2
        '    pdfTable.AddCell(cellFirma3)

        '    ' Firma Director de Area
        '    Dim cellFirma4 = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                      _____________________________" & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                          RESPONSABLE DEL BIEN", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellFirma4.Border = 0
        '    cellFirma4.Colspan = 3
        '    pdfTable.AddCell(cellFirma4)

        '    ' Firma Comision de Activo Fijo
        '    Dim cellFirma5 = New PdfPCell(New Phrase(" " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                  __________________________" & Environment.NewLine & _
        '                                             " " & Environment.NewLine & _
        '                                             "                   COMISION DE ACTIVO FIJO", New Font(Font.FontFamily.HELVETICA, 7.0F, Font.BOLD)))
        '    cellFirma5.Border = 0
        '    cellFirma5.Colspan = 2
        '    pdfTable.AddCell(cellFirma5)

        '    pdfDoc.Add(pdfTable)

        '    pdfDoc.Close()

        '    'Data.Add("Pdf", "ok")

        '    Dim bytes() As Byte = memory.ToArray
        '    memory.Close()

        '    Dim objEmail As New ClsMail
        '    Dim cuerpo, receptor, AsuntoCorreo As String
        '    cuerpo = "<html>"
        '    cuerpo = cuerpo & "<head>"
        '    cuerpo = cuerpo & "<title></title>"
        '    cuerpo = cuerpo & "<style>"
        '    cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
        '    cuerpo = cuerpo & "</style>"
        '    cuerpo = cuerpo & "</head>"
        '    cuerpo = cuerpo & "<body>"
        '    cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
        '    cuerpo = cuerpo & "<tr><td colspan=2><b>Estimado(a):</b></td></tr>"
        '    cuerpo = cuerpo & "<tr><td colspan=2></br></br>El documento de traslado ha sido generado.</td></tr>"
        '    cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
        '    cuerpo = cuerpo & "</table>"
        '    cuerpo = cuerpo & "</body>"
        '    cuerpo = cuerpo & "</html>"
        '    receptor = "enevado@usat.edu.pe"
        '    AsuntoCorreo = "[Documento de Traslado]"

        '    objEmail.EnviarPDFMail("campusvirtual@usat.edu.pe", "Documento de Traslado", receptor, AsuntoCorreo, cuerpo, True, "traslado", New MemoryStream(bytes))

        '    Data.Add("Status", "success")
        '    Data.Add("Message", "ok")
        '    Data.Add("Code", "1000")
        '    list.Add(Data)

        '    JSONresult = serializer.Serialize(list)
        '    Response.Write(JSONresult)

        'Catch ex As Exception
        '    Dim lstErr As New List(Of Dictionary(Of String, Object))()
        '    Dim dataErr As New Dictionary(Of String, Object)()
        '    dataErr.Add("error", ex.Message)
        '    lstErr.Add(dataErr)
        '    JSONresult = serializer.Serialize(lstErr)
        '    Response.Write(JSONresult)
        'End Try
    End Sub

End Class


