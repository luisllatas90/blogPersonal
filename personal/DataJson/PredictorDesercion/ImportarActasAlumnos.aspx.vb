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
Partial Class DataJson_PredictorDesercion_ImportarActasAlumnos
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FPost As String = Request.Form("Funcion")

        'Response.Write(Request("id"))
        Select Case FPost

            Case "Post" : Grabar()

        End Select
    End Sub

    Sub Grabar()
        Dim CODIGO As String = Request.Form("CODIGO")
        Dim MAT3 As Decimal = Request.Form("MAT3")
        Dim MAT4 As Decimal = Request.Form("MAT4")
        Dim MAT5 As Decimal = Request.Form("MAT5")
        Dim COM3 As Decimal = Request.Form("COM3")
        Dim COM4 As Decimal = Request.Form("COM4")
        Dim COM5 As Decimal = Request.Form("COM5")
        Dim CTA3 As Decimal = Request.Form("CTA3")
        Dim CTA4 As Decimal = Request.Form("CTA4")
        Dim CTA5 As Decimal = Request.Form("CTA5")
        Dim PROMEDIO As Decimal = Request.Form("PROMEDIO")
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim cif As New ClsCRM
        '   Response.Write(Request.Form("id"))

        tb = obj.TraerDataTable("dbo.PRED_USPACTUALIZANOTAINGRESO", CODIGO, PROMEDIO, MAT3, MAT4, MAT5, COM3, COM4, COM5, CTA3, CTA4, CTA5)
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
