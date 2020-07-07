Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GradosYTitulos_Especialidad
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case obj.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim opcion As String = "L"
                    Dim codigo_test As String = obj.DecrytedString64(Request("test"))
                    Dim codigo_cpf As String = obj.DecrytedString64(Request("cpf"))
                    Dim vigencia As String = Request("vig")
                    ConsultarEspecialidad(opcion, codigo_test, codigo_cpf, vigencia)
                Case "Registrar"
                    Dim codigo As Integer = Request("hdcod")
                    Dim codigo_pes As Integer = obj.DecrytedString64(Request("cboPlanEstudios"))
                    Dim descripcion As String = Request("txtespecialidad")
                    Dim abreviatura As String = Request("txtabreviatura")
                    Dim vigencia As String
                    If Request("chkvigencia") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Registrar(codigo, codigo_pes, descripcion, abreviatura, vigencia, Session("id_per"))
                Case "Editar"
                    k = obj.DecrytedString64(Request("cod")) ' codigo de Denominacion
                    ConsultarEspecialidad("E", k, "", "")
                Case "Modificar"
                    Dim codigo As Integer = obj.DecrytedString64(Request("hdcod"))
                    Dim codigo_pes As Integer = obj.DecrytedString64(Request("cboPlanEstudios"))
                    Dim descripcion As String = Request("txtespecialidad")
                    Dim abreviatura As String = Request("txtabreviatura")
                    Dim vigencia As String
                    If Request("chkvigencia") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Registrar(codigo, codigo_pes, descripcion, abreviatura, vigencia, Session("id_per"))
                Case "Eliminar"
                    '    k = objCRM.DecrytedString64(Request("hdcod"))
                    '    EliminarConvocatoria(k)

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

    Private Sub ConsultarEspecialidad(ByVal opcion As String, ByVal param1 As String, ByVal param2 As String, ByVal vigencia As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarEspecialidad(opcion, param1, param2, vigencia)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod_es", obj.EncrytedString64(dt.Rows(i).Item("codigo_esp")))
                data.Add("nom_es", dt.Rows(i).Item("descripcion_esp"))
                data.Add("abr_es", dt.Rows(i).Item("abreviatura_esp"))
                data.Add("test", obj.EncrytedString64(dt.Rows(i).Item("codigo_test")))
                data.Add("cod_cp", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                data.Add("cod_pes", obj.EncrytedString64(dt.Rows(i).Item("codigo_pes")))
                data.Add("nom_pes", dt.Rows(i).Item("descripcion_pes"))
                data.Add("vig", dt.Rows(i).Item("vigencia_esp"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub Registrar(ByVal cod As Integer, ByVal codigo_pes As Integer, ByVal descripcion As String, ByVal abreviatura As String, ByVal vigencia As Integer, ByVal user_reg As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarEspecialidad(cod, codigo_pes, descripcion, abreviatura, vigencia, user_reg)
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

