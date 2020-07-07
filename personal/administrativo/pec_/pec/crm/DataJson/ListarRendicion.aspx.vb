Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Partial Class administrativo_Tesoreria_Rendiciones_AppRendiciones_DataJson_ListarRendicion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim codigo_per As String
        codigo_per = 30182 ' Session("id_per")
        Dim codigoEgr As Integer = Request.QueryString("Id")
        Dim funcion As String = Request.QueryString("Funcion")

        Dim form As String = Request.Form("id")
        ' Response.Write("Hola:" + form)
        Select Case funcion
            Case "RendDetalle"
                RendirDetalle(codigoEgr)
            Case "RendImportes"
                RendImportes(codigoEgr)
            Case "ListDetalle"
            Case "Grabar"
        End Select

        If Not form Is Nothing Then
            Grabar()
        End If

        '  Response.Write("Hola:"+codigo_per)

       
    End Sub
    Sub RendirDetalle(ByVal codigoEgr As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", Mid(Me.cboestado.Text, 1, 1), "", "", "", "")
        tb = obj.TraerDataTable("dbo.USP_DOCUMENTOS_X_RENDIR_DETALLE", codigoEgr)
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            'Response.Write("----:" + tb.Rows(i).Item("Id"))
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Rubro", tb.Rows(i).Item("Rubro"))
            Data.Add("Importe", tb.Rows(i).Item("Importe"))
            Data.Add("Centrocostos", tb.Rows(i).Item("Centrocostos"))
            Data.Add("EstadoRendicion", tb.Rows(i).Item("EstadoRendicion"))
            Data.Add("Observacion", tb.Rows(i).Item("Observacion"))
            Data.Add("ExigirRendicion", tb.Rows(i).Item("ExigirRendicion"))
            Data.Add("CodigoRend", tb.Rows(i).Item("CodigoRend"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Public Sub RendImportes(ByVal CodigoRend As Integer)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
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
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub Grabar()
        For Each s As String In Request.Form.AllKeys
            Response.Write(s & ": " & Request.Form(s) & "<br />")
        Next
    End Sub
End Class
