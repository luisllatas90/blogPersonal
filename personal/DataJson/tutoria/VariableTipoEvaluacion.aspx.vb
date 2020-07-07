Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_tutoria_VariableTipoEvaluacion
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

                    f = objT.DecrytedString64(Request("hdcod"))
                    'f = objT.DecrytedString64(Request("cboConvocatoria"))
                    ListaVariableTipoEvaluacion("L", 0, f)
                Case "Registrar"

                    f = objT.DecrytedString64(Request("hdcod"))
                    Dim codigo_ve As String = objT.DecrytedString64(Request("cboVariable"))
                    Dim peso As String = Request("puntaje")
                    Dim estado As Integer
                   
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If

                    RegistrarVariableTipoEvaluacion(0, codigo_ve, f, peso, 0, estado, 1, Session("id_per"))
                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    ListaVariableTipoEvaluacion("E", k, 0)
                Case "Modificar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    Dim suma As Integer = 0, peso As Integer = 0, promedio As Integer = 0
                    Dim nombre As String = Request("txtDescripcion")
                    Dim estado As Integer
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If
                    Dim aplica As String = Request("cboAplica")
                    If aplica = "suma" Then suma = 1
                    If aplica = "peso" Then peso = 1
                    If aplica = "prom" Then promedio = 1
                    'RegistrarTipoEvaluacion(k, nombre, estado, suma, peso, promedio, Session("id_per"))
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    EliminarVariableTipoEvaluacion(k, Session("id_per"))
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

    Private Sub ListaVariableTipoEvaluacion(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_te As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            tb = obj.ListaVariableTipoEvaluacion(tipo, codigo, codigo_te)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("codigo", codigo)
                    data.Add("codigo_te", codigo_te)
                    data.Add("cCodte", obj.EncrytedString64(tb.Rows(i).Item("codigo_te")))
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_vt")))
                    data.Add("cCodve", obj.EncrytedString64(tb.Rows(i).Item("codigo_ve")))
                    data.Add("cVar", tb.Rows(i).Item("descripcion_ve"))
                    data.Add("cPeso", tb.Rows(i).Item("peso_vt"))
                    data.Add("cTotal", tb.Rows(i).Item("total_vt"))

                    If tipo = "E" Then
                        data.Add("cEstado", tb.Rows(i).Item("estado_vt"))
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

    Private Sub RegistrarVariableTipoEvaluacion(ByVal codigo_vt As Integer, ByVal codigo_ve As Integer, ByVal codigo_te As Integer, ByVal peso As Double, ByVal total As Double, ByVal estado As Integer, ByVal obligatorio As Integer, ByVal usuario_reg As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarVariableTipoEvaluacion(codigo_vt, codigo_ve, codigo_te, peso, total, estado, obligatorio, usuario_reg)
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

    Private Sub EliminarVariableTipoEvaluacion(ByVal cod As Integer, ByVal usuario_reg As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarVariableTipoEvaluacion(cod, usuario_reg)
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
