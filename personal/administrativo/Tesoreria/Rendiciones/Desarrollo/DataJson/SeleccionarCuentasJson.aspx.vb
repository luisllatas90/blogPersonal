Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Web
Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.Web.Script.Serialization

Partial Class administrativo_Tesoreria_Rendiciones_Desarrollo_DataJson_SeleccionarCuentasJson
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim codigo_per As String
        codigo_per = Session("id_per")
        Dim l_data As String = Request.Form("data")
        Dim l_funcion As String = Request.Form("Funcion")
        Dim l_ObjEntrada As ObjEntrada = New ObjEntrada
        Dim serializer As JavaScriptSerializer = New JavaScriptSerializer()

        Select Case l_funcion
            Case "listarCuentaBancaria"
                ListarCuentaBancaria()
            Case "actualizarCuentaBancaria"
                l_ObjEntrada = serializer.Deserialize(Of ObjEntrada)(l_data)
                ActualizarCuentaBancaria(l_ObjEntrada.nCtaBancariaCodigo)
        End Select


    End Sub

    Private Sub ActualizarCuentaBancaria(ByVal nCtaBancariaCodigo As Integer)
        Try
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim cn As New clsaccesodatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim JSONresult As String = ""
            tb = obj.TraerDataTable("dbo.usp_GesFinanciera_ActualizarEstado_CtasBancarias", nCtaBancariaCodigo)
            obj.CerrarConexion()
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
        Catch ex As Exception
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("Status", "ERROR")
            Data.Add("Message", ex.Message)
            Data.Add("Code", "0")
            list.Add(Data)
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarCuentaBancaria()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim cn As New clsaccesodatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim JSONresult As String = ""
        tb = obj.TraerDataTable("dbo.usp_GesFinanciera_Listar_CtasBancarias")
        obj.CerrarConexion()
        Dim list As New List(Of Dictionary(Of String, Object))()
        For i As Integer = 0 To tb.Rows.Count - 1
            Dim Data As New Dictionary(Of String, Object)()
            Data.Add("nBanCodigo", tb.Rows(i).Item("nBanCodigo"))
            Data.Add("cBanNombre", tb.Rows(i).Item("cBanNombre"))
            Data.Add("nCtaBancariaCodigo", tb.Rows(i).Item("nCtaBancariaCodigo"))
            Data.Add("nCtaBancariaNumCuenta", tb.Rows(i).Item("nCtaBancariaNumCuenta"))
            Data.Add("cCtaBancariaEstado", tb.Rows(i).Item("cCtaBancariaEstado"))
            list.Add(Data)
        Next
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

End Class

Public Class ObjEntrada

    Public nCtaBancariaCodigo As Integer

End Class