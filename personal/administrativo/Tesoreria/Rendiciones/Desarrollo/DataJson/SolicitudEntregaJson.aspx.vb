Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.Web.Script.Serialization

Partial Class administrativo_Tesoreria_Rendiciones_Desarrollo_DataJson_SolicitudEntregaJson
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim nPerCodigo As String
        nPerCodigo = Session("id_per")
        Dim l_data As String = Request.Form("data")
        Dim l_funcion As String = Request.Form("Funcion")
        Dim l_ObjEntrada As ObjEntrada = New ObjEntrada
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()
        l_ObjEntrada = serializer.Deserialize(Of ObjEntrada)(l_data)

        Select Case l_funcion
            Case "listarPersonaDatos"
                ListarPersonaDatos(nPerCodigo)
            Case "listarPedidoLogistica"
                ListarPedidoLogistica(l_ObjEntrada.nPedLogCodigo)
            Case "listarDetallePedidoLogistica"
                ListarDetallePedidoLogistica(l_ObjEntrada.nPedLogCodigo)
            Case "listarPersona"
                l_ObjEntrada = serializer.Deserialize(Of ObjEntrada)(l_data)
                ListarPersona(l_ObjEntrada.cPerApellidos)
        End Select

    End Sub

    Private Sub ListarPersonaDatos(ByVal nPerCodigo)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.usp_GesFinanciera_ListarPersonaDatos", nPerCodigo)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("cPerApePaterno", tb.Rows(i).Item("cPerApePaterno"))
            Data.Add("cPerApeMaterno", tb.Rows(i).Item("cPerApeMaterno"))
            Data.Add("cPerNombres", tb.Rows(i).Item("cPerNombres"))
            Data.Add("cPersonaEmail", tb.Rows(i).Item("cPersonaEmail"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarPedidoLogistica(ByVal nPedLogCodigo As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.usp_GesFinanciera_ListarPedidoLogistica", nPedLogCodigo)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("nPedLogCodigo", tb.Rows(i).Item("nPedLogCodigo"))
            Data.Add("fPedLogImporte", String.Format("{0:#,##0.##}", CType(tb.Rows(i).Item("fPedLogImporte"), Decimal)))
            Data.Add("nPedLogEstado", tb.Rows(i).Item("nPedLogEstado"))
            Data.Add("dPedLogFecha", tb.Rows(i).Item("dPedLogFecha"))
            Data.Add("cPerApePaterno", tb.Rows(i).Item("cPerApePaterno"))
            Data.Add("cPerApeMaterno", tb.Rows(i).Item("cPerApeMaterno"))
            Data.Add("cPerNombres", tb.Rows(i).Item("cPerNombres"))
            Data.Add("cCcDescripcion", tb.Rows(i).Item("cCcDescripcion"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarDetallePedidoLogistica(ByVal nPedLogCodigo As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.usp_GesFinanciera_ListarDetallePedidoLogistica", nPedLogCodigo)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("nPedLogDetalleCodigo", tb.Rows(i).Item("nPedLogDetalleCodigo"))
            Data.Add("nPedLogCodigo", tb.Rows(i).Item("nPedLogCodigo"))
            Data.Add("fPedLogImporte", String.Format("{0:#,##0.##}", CType(tb.Rows(i).Item("fPedLogImporte"), Decimal)))
            Data.Add("fPedLogDetalleImporte", String.Format("{0:#,##0.##}", CType(tb.Rows(i).Item("fPedLogDetalleImporte"), Decimal)))
            Data.Add("nArticuloCodigo", tb.Rows(i).Item("nArticuloCodigo"))
            Data.Add("cArticuloDescripcion", tb.Rows(i).Item("cArticuloDescripcion"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarPersona(ByVal cPerApellidos As String)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.usp_GesFinanciera_Listar_Persona", cPerApellidos)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("nPerCodigo", tb.Rows(i).Item("nPerCodigo"))
            Data.Add("cPerApePaterno", tb.Rows(i).Item("cPerApePaterno"))
            Data.Add("cPerApeMaterno", tb.Rows(i).Item("cPerApeMaterno"))
            Data.Add("cPerNombres", tb.Rows(i).Item("cPerNombres"))
            Data.Add("cPerEmail", tb.Rows(i).Item("cPerEmail"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

End Class


Public Class ObjEntrada

    Public nPedLogCodigo As Integer
    Public nDetallePedidoCodigo As Integer
    Public nPerCodigo As Integer
    Public cPerApellidos As String

End Class

