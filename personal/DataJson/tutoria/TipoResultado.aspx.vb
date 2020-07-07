Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_tutoria_TipoResultado
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objT As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objT.DecrytedString64(Request("action"))
                Case "Listar"
                    'f = objT.DecrytedString64(Request("cboConvocatoria"))
                    ListaTipoResultado("L", 0)
                Case "Registrar"
                    Dim nombre As String = Request("txtDescripcion")
                    Dim estado As Integer = 0
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If
                    RegistrarTipoResultado(k, nombre, estado, Session("id_per"))
                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    ListaTipoResultado("E", k)
                Case "Modificar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    Dim nombre As String = Request("txtDescripcion")
                    Dim estado As Integer
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If
                    RegistrarTipoResultado(k, nombre, estado, Session("id_per"))
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    EliminarTipoResultado(k, Session("id_per"))
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

    Private Sub ListaTipoResultado(ByVal tipo As String, ByVal codigo As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            tb = obj.ListaTipoResultado(tipo, codigo)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_tre")))
                    data.Add("cDescripcion", tb.Rows(i).Item("descripcion_tre"))

                    If tipo = "E" Then
                        data.Add("cEstado", tb.Rows(i).Item("estado_tre"))
                    End If
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("msje", ex.Message)
            data1.Add("rpta", "0")
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarTipoResultado(ByVal cod As Integer, ByVal nombre As String, ByVal estado As Integer, ByVal user_reg As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarTipoResultado(cod, nombre, estado, user_reg)
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

    Private Sub EliminarTipoResultado(ByVal cod As Integer, ByVal usuario_reg As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarTipoResultado(cod, usuario_reg)
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
