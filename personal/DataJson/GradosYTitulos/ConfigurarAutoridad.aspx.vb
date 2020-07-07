Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_GradosYTitulos_ConfigurarAutoridad
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
                    Dim codigo_Cargo As String = obj.DecrytedString64(Request("cod"))
                    Dim vigencia As String = Request("vig")
                    ConsultarConfiguracionCargo(opcion, codigo_Cargo, vigencia)
                Case "Registrar"
                    Dim codigo As Integer = Request("hdcod")
                    Dim codigo_cgo As Integer = obj.DecrytedString64(Request("cboCargoR"))
                    Dim codigo_pre As Integer = obj.DecrytedString64(Request("cboPrefijo"))
                    Dim codigo_per As Integer = obj.DecrytedString64(Request("cboPersonalR"))
                    Dim codigo_fac As Integer = obj.DecrytedString64(Request("cboFacultad"))
                    Dim orden As String = Request("cboOrden")
                    Dim vigencia As String
                    If Request("chkvigencia") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Dim encargado As String
                    If Request("chkEncargado") = "" Then
                        encargado = 0
                    Else
                        encargado = 1
                    End If
                    Registrar(codigo, codigo_cgo, codigo_pre, codigo_per, codigo_fac, orden, encargado, vigencia, Session("id_per"))
                Case "Editar"
                    k = obj.DecrytedString64(Request("cod")) ' codigo de Denominacion
                    ConsultarConfiguracionCargo("E", k, "")
                Case "Modificar"
                    Dim codigo As Integer = obj.DecrytedString64(Request("hdcod"))
                    Dim codigo_cgo As Integer = obj.DecrytedString64(Request("cboCargoR"))
                    Dim codigo_pre As Integer = obj.DecrytedString64(Request("cboPrefijo"))
                    Dim codigo_per As Integer = obj.DecrytedString64(Request("cboPersonalR"))
                    Dim codigo_fac As Integer = obj.DecrytedString64(Request("cboFacultad"))
                    Dim orden As String = Request("cboOrden")
                    Dim vigencia As String
                    If Request("chkvigencia") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    Dim encargado As String
                    If Request("chkEncargado") = "" Then
                        encargado = 0
                    Else
                        encargado = 1
                    End If
                    Registrar(codigo, codigo_cgo, codigo_pre, codigo_per, codigo_fac, orden, encargado, vigencia, Session("id_per"))
                    'Case "Eliminar"
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

    Private Sub ConsultarConfiguracionCargo(ByVal opcion As String, ByVal param1 As String, ByVal param2 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarConfiguracionCargo(opcion, param1, param2)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod_cc", obj.EncrytedString64(dt.Rows(i).Item("codigo_ccp")))
                data.Add("cod_cg", obj.EncrytedString64(dt.Rows(i).Item("codigo_cgo")))
                data.Add("nom_cg", dt.Rows(i).Item("descripcion_cgo"))
                data.Add("cod_pre", obj.EncrytedString64(dt.Rows(i).Item("codigo_pre")))
                data.Add("cod_pe", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                data.Add("nom_pe", dt.Rows(i).Item("Personal"))
                data.Add("cod_fac", obj.EncrytedString64(dt.Rows(i).Item("codigo_Fac")))
                data.Add("nom_fac", dt.Rows(i).Item("nombre_fac"))
                data.Add("encar", dt.Rows(i).Item("encargado_ccp"))
                data.Add("ord", dt.Rows(i).Item("orden_ccp"))
                data.Add("vig", dt.Rows(i).Item("estado_ccp"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub Registrar(ByVal cod As Integer, ByVal codigo_cgo As Integer, ByVal codigo_pre As Integer, ByVal codigo_per As Integer, ByVal codigo_Fac As Integer, ByVal orden As Integer, ByVal encargado As String, ByVal vigencia As Integer, ByVal user_reg As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarConfiguracionCargo(cod, codigo_cgo, codigo_pre, codigo_per, codigo_Fac, orden, encargado, vigencia, user_reg)
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
