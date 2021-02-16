Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_Categoria
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    Listar("L", k, f)
                Case "Registrar"
                    k = Request("hdcod")
                    Dim nombre As String = Request("txtDescripcion")
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer = 1
                    'If Request("chkestado") = "" Then
                    '    estado = 0
                    'Else
                    '    estado = 1
                    'End If
                    Actualizar(k, nombre, estado, cod_per)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Listar("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Dim nombre As String = Request("txtDescripcion")
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer = 1
                    'If Request("chkestado") = "" Then
                    '    estado = 0
                    'Else
                    '    estado = 1
                    'End If
                    Actualizar(k, nombre, estado, cod_per)
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Eliminar(k)
            End Select

        Catch ex As Exception

            Data.Add("msje", ex.Message)
            Data.Add("rpta", "0 - LOAD")
            Dim list As New List(Of Dictionary(Of String, Object))()
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Listar(ByVal tipo As String, ByVal codigo As String, ByVal param1 As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaEstadoComunicacion(tipo, codigo, param1)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_ecom")))
                    data.Add("nom", tb.Rows(i).Item("descripcion_ecom"))
                    data.Add("est", tb.Rows(i).Item("estado_ecom"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Actualizar(ByVal cod As Integer, ByVal nombre As String, ByVal estado As Integer, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarEstadoComunicacion(cod, nombre, estado, user_reg)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub Eliminar(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.EliminarEstadoComunicacion(cod)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

End Class
