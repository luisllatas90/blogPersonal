Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class DataJson_crm_Acuerdo
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objCRM.DecrytedString64(Request("action"))
                Case "Listar"
                    'f = objCRM.DecrytedString64(Request("cboConvocatoria"))
                    Dim codigo_com As Integer = objCRM.DecrytedString64(Request("codigo_com"))
                    ListaAcuerdo("L", codigo_com)

                Case "Registrar"
                    Dim codigo_com As Integer = objCRM.DecrytedString64(Request("hdcod"))
                    Dim detalle_acu As String = Request("txtdetalle_acu")
                    Dim fecha_acu As String = Request("txtFecha_acu")
                    Dim hora_acu As String = Request("txtHora_acu") 'Adicionado por @jquepuy | 07ENE2019
                    Dim estado_acu As Integer = 1
                    RegistrarAcuerdo(k, codigo_com, detalle_acu, fecha_acu, hora_acu, estado_acu, Session("id_per"))

                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdID"))
                    ListaAcuerdo("E", k)

                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdID"))
                    Dim codigo_com As Integer = 0
                    Dim detalle_acu As String = Request("txtdetalle_acu")
                    Dim fecha_acu As String = Request("txtFecha_acu")
                    Dim hora_acu As String = Request("txtHora_acu") 'Adicionado por @jquepuy | 07ENE2019
                    Dim estado_acu As Integer = 1
                    RegistrarAcuerdo(k, codigo_com, detalle_acu, fecha_acu, hora_acu, estado_acu, Session("id_per"))

                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdID"))
                    EliminarAcuerdo(k)
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

    Private Sub ListaAcuerdo(ByVal tipo As String, ByVal codigo As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaAcuerdo(tipo, codigo)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_acu")))
                    data.Add("cFecha", tb.Rows(i).Item("fecha_acu"))
                    data.Add("cHora", tb.Rows(i).Item("hora_acu")) 'Adicionado por @jquepuy | 07ENE2019
                    data.Add("cDetalle", tb.Rows(i).Item("detalle_acu"))
                    If tipo = "L" Then
                        data.Add("cUsuario", tb.Rows(i).Item("usuario_per"))
                    End If
                    'If tipo = "E" Then
                    '    data.Add("cTip", obj.EncrytedString64(tb.Rows(i).Item("codigo_tcom")))
                    '    data.Add("cMot", obj.EncrytedString64(tb.Rows(i).Item("codigo_mot")))
                    'End If
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

    Private Sub RegistrarAcuerdo(ByVal cod As Integer, ByVal codigo_com As Integer, ByVal detalle_acu As String, ByVal fecha_acu As String, _
                                 ByVal hora_acu As String, ByVal estado_acu As Integer, ByVal user_reg As Integer)


        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarAcuerdo(cod, codigo_com, detalle_acu, fecha_acu, hora_acu, estado_acu, user_reg)
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

    'Private Sub RegistrarMotivo(ByVal cod As Integer, ByVal nombre As String, ByVal estado As Integer, ByVal user_reg As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarMotivo(cod, nombre, estado, user_reg)
    '        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data.Add("rpta", "0 - REG")
    '        Data.Add("msje", ex.Message)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    Private Sub EliminarAcuerdo(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarAcuerdo(cod)
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
