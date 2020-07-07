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
Partial Class DataJson_PredictorDiserccion_ImportarDataTutoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")

        'Response.Write(Request("id"))
        Select Case FPost

            Case "Matriz" : Matriz()
            Case "Post" : Grabar()

        End Select
    End Sub
    Sub Matriz()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        Dim id As Integer = Request.Form("id")
        tb = obj.TraerDataTable("dbo.PRED_MATRIZ_BY_LIST")
        obj.CerrarConexion()

        '' Response.Write(tb.Rows.Count)
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("Codigo_mtz"))
            Data.Add("Label", tb.Rows(i).Item("Observacion_mtz"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub Grabar()
        Dim CODIGO As String = Request.Form("CODIGO")
        Dim CICLOINGRESO As String = Request.Form("CICLOINGRESO")
        Dim COGNITIVO As String = Request.Form("COGNITIVO")
        Dim SOCIOAFECTIVO As String = Request.Form("SOCIOAFECTIVO")
        Dim VOCACIONAL As String = Request.Form("VOCACIONAL")
        Dim AUTOEFICACIA As String = Request.Form("AUTOEFICACIA")
        Dim ANSIEDAD As String = Request.Form("ANSIEDAD")
        Dim DEPRESION As String = Request.Form("DEPRESION")     
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPACTUALIZARMATRIZ", CODIGO, CICLOINGRESO, COGNITIVO, SOCIOAFECTIVO, VOCACIONAL, AUTOEFICACIA, ANSIEDAD, DEPRESION)
        obj.CerrarConexion()

        '' Response.Write(tb.Rows.Count)
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", tb.Rows(i).Item("Status"))
            Data.Add("Message", tb.Rows(i).Item("Message"))
            Data.Add("Code", tb.Rows(i).Item("Code"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
End Class
