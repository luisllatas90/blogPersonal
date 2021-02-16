Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_Preguntas
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
                    ListaPreguntas("L", 0)
                Case "ListaRespuestaPregunta"
                    k = objCRM.DecrytedString64(Request("cod"))
                    ListaRespuestas("LXP", k, 0, 0)
                Case "RespuestasxPregunta"
                    Dim codigo_eve As Integer = objCRM.DecrytedString64(Request("codeve"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("codint"))
                    Dim codigo_pregunta As Integer = objCRM.DecrytedString64(Request("codpre"))
                    ListaRespuestas("LRP", codigo_pregunta, codigo_int, codigo_eve)
                Case "Registrar"
                    Dim codigo_eve As Integer = objCRM.DecrytedString64(Request("codeve"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("codint"))
                    Dim codigo_respuesta As Integer = objCRM.DecrytedString64(Request("codresp"))
                    Dim nombre_respuesta As String = Request("nombreresp").ToString().Trim()
                    Dim respuesta_otro As String = Request("rptaotro").ToString().Trim()
                    RegistrarRespuesta(codigo_eve, codigo_int, codigo_respuesta, nombre_respuesta, respuesta_otro, Session("id_per"))
                    'Case "Editar"
                    '    k = objCRM.DecrytedString64(Request("hdcod"))
                    '    ListaMotivo("E", k)
                    'Case "Modificar"
                    '    k = objCRM.DecrytedString64(Request("hdcod"))
                    '    Dim nombre As String = Request("txtDescripcion")
                    '    Dim estado As Integer
                    '    If Request("chkestado") = "" Then
                    '        estado = 0
                    '    Else
                    '        estado = 1
                    '    End If
                    '    RegistrarMotivo(k, nombre, estado, Session("id_per"))
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    eliminarRespuesta(k)
                Case "MoverRespuestaDePregunta"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Dim dir As String = Request("dir")
                    MoverRespuestaDePregunta(k, Dir)
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

    Private Sub ListaPreguntas(ByVal tipo As String, ByVal codigo As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListarPreguntas(tipo, codigo)
            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("codigo", obj.EncrytedString64(tb.Rows(i).Item("codigo_pre")))
                    data.Add("descripcion", tb.Rows(i).Item("descripcion_pre"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("msje", ex.Message)
            data1.Add("rpta", "0-PRG")
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaRespuestas(ByVal tipo As String, ByVal codigo_pre As Integer, ByVal codigo_int As Integer, ByVal codigo_eve As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListarRespuestas(tipo, codigo_pre, codigo_int, codigo_eve)
            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("codigo", obj.EncrytedString64(tb.Rows(i).Item("codigo_res")))
                    data.Add("descripcion", tb.Rows(i).Item("descripcion_res"))
                    If tipo = "LRP" Then
                        data.Add("codigoipr", obj.EncrytedString64(tb.Rows(i).Item("codigo_ipr")))
                    End If
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("msje", ex.Message)
            data1.Add("rpta", "0-RPTAS")
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarRespuesta(ByVal codeve As Integer, ByVal codint As Integer, ByVal cod_respuesta As Integer, ByVal nombre_respuesta As String, ByVal respuesta_otro As String, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            If nombre_respuesta.Trim = "OTRO" Then
                If String.IsNullOrEmpty(respuesta_otro) Then
                    Data.Add("rpta", "0")
                    Data.Add("msje", "Si ha seleccionado ""OTRO"" debe ingresar una descripción")
                    list.Add(Data)
                    JSONresult = serializer.Serialize(list)
                    Response.Write(JSONresult)
                    Exit Sub
                End If
            End If

            Dim dt As New Data.DataTable
            dt = obj.RegistrarRespuestaInteresado(codeve, codint, cod_respuesta, respuesta_otro, user_reg)
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

    Private Sub eliminarRespuesta(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarRespuestaDePregunta(cod)
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

    Private Sub MoverRespuestaDePregunta(ByVal cod As Integer, ByVal direccion As String)
        ' cod : Codigo de Tabla InteresadoPreguntaRespuesta_CRM, direccion : Si Sube(S) o Baja (B)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.MoverRespuestaDePregunta(cod, direccion)
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
