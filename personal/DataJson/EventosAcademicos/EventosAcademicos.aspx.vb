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
Partial Class DataJson_EventosAcademicos_EventosAcademicos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")

        'Response.Write(Request("id"))
        Select Case FPost
            Case "cecos" : CentroCostos()
            Case "rubro" : Rubro()
            Case "Post" : Grabar()

        End Select
    End Sub
    Sub CentroCostos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.Datatable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        tb = obj.TraerDataTable("dbo.EVE_BuscaCcoId", Request.Form("Id"), Request.Form("Ctf"), Request.Form("Mod"), "N")
        obj.CerrarConexion()
        ' Response.Write(tb.Rows.Count)
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("codigo_Cco"))
            Data.Add("Label", tb.Rows(i).Item("descripcion_Cco"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Sub Rubro()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        Dim id As Integer = Request.Form("id")
        tb = obj.TraerDataTable("dbo.LOG_ConsultarCentroCostosServicioConcepto_V2", "", id)
        obj.CerrarConexion()

        '' Response.Write(tb.Rows.Count)
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Value", tb.Rows(i).Item("codigo_Sco"))
            Data.Add("Label", tb.Rows(i).Item("descripcion_Sco"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Sub Grabar()
        Dim TipDoc As String = Request.Form("TipoDoc")
        Dim NumDoc As String = Request.Form("NumDoc")
        Dim ApellPat As String = Request.Form("ApellPaterno")
        Dim ApellMat As String = Request.Form("ApellMaterno")
        Dim Nombres As String = Request.Form("Nombres")
        Dim Codigo_ceco As Integer = Request.Form("Codigo_Ceco")
        Dim Codigo_Sco As Integer = Request.Form("Codigo_Sco")
        Dim FecNac As DateTime
        If Request.Form("FecNac") = "undefined" Then
            FecNac = Request.Form("FecNac")
        Else
            FecNac = Now.Date
        End If


        Dim Email As String = Request.Form("Email")
        Dim Importe As Decimal = Request.Form("Importe")
        Dim FecVence As DateTime = Request.Form("FecVence")

        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.CONEII_REGISTRAALUMNOS", TipDoc, NumDoc, ApellPat, ApellMat, Nombres, FecNac, Email, Importe, FecVence, Codigo_ceco, Codigo_Sco)
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
