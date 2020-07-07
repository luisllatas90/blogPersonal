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
Partial Class DataJson_PredictorDiserccion_GenerarMatrizCSV
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        Select Case FPost
            Case "CVS"
                ListarMatrizConocimiento()
            Case "Facultad"
                Facultad()
            Case "Escuela"
                EscuelaProfesional()
        End Select
    End Sub
    Sub ListarMatrizConocimiento()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigo_cfp As Integer = Request.Form("Codigo_cpf")
        Dim codigo_fac As Integer = Request.Form("Codigo_fac")
        Dim fecha_fac As String = Request.Form("Fecha")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRED_USPMATRIZCONOCIMIENTO", codigo_fac, codigo_cfp, fecha_fac)
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
    Sub Facultad()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRED_USPFACULTAD_BY_LIST")
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

    Sub EscuelaProfesional()
        Dim codigo_fac As Integer = Request.Form("Codigo")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PREC_USPCARRERAPROFECIONAL_BY_LIST", codigo_fac)
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

End Class

