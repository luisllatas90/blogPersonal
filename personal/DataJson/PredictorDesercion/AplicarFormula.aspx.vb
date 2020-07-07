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
Partial Class DataJson_PredictorDesercion_AplicarFormula
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")
        Select Case FPost
            Case "Alumnos"
                ListarAlumnos()
            Case "Detalle"
                ListarDetalleAlumno()
        End Select
    End Sub

    Sub ListarAlumnos()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        Dim codigo As String = Request.Form("Codigo")
        Dim CodigoFml As String = Request.Form("CodigoFml")
        Dim Fecha As String = Request.Form("Fecha")
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRED_ANALISISPROBABILIDAD_BY_LIST", codigo, CodigoFml, Fecha)
        obj.CerrarConexion()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Id", tb.Rows(i).Item("Id"))
            Data.Add("Codigo", tb.Rows(i).Item("Codigo"))
            Data.Add("Alumno", tb.Rows(i).Item("Alumno"))
            Data.Add("Probabilidad", tb.Rows(i).Item("Probabilidad"))
            Data.Add("Estado", tb.Rows(i).Item("Estado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        serializer.MaxJsonLength = Int32.MaxValue
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Sub ListarDetalleAlumno()
        Dim op As Integer = 1
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        Dim codigo_alu As Integer = Request.Form("Codigo")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))
        tb = obj.TraerDataTable("dbo.PRED_USPDETALLEMATRIZALUMNO_BY_LIST", codigo_alu)
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
End Class
