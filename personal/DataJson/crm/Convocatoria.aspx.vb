Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_Convocatoria
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
                    f = objCRM.DecrytedString64(Request("cboTipoEstudio"))
                    ListaConvocatorias("L", k, f)
                Case "Registrar"
                    Dim codigo_test As Integer = objCRM.DecrytedString64(Request("cboTipoEstudioR"))
                    Dim codigo_cac As Integer = objCRM.DecrytedString64(Request("cboCicloAcademicoR"))
                    Dim nombre As String = Request("txtnombre")
                    Dim detalle As String = Request("txtdetalle")
                    Dim fecini As String = Request("txtfecini")
                    Dim fecfin As String = Request("txtfecfin")
                    Dim cod_per As Integer = Session("id_per")
                    'Dim estado As Integer
                    'If Request("chkestado") = "" Then
                    '    estado = 0
                    'Else
                    'estado = 1
                    'End If
                    RegistrarConvocatoria(k, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, cod_per)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    ListaConvocatorias("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    Dim codigo_test As Integer = objCRM.DecrytedString64(Request("cboTipoEstudioR"))
                    Dim codigo_cac As Integer = objCRM.DecrytedString64(Request("cboCicloAcademicoR"))
                    Dim nombre As String = Request("txtnombre")
                    Dim detalle As String = Request("txtdetalle")
                    Dim fecini As String = Request("txtfecini")
                    Dim fecfin As String = Request("txtfecfin")
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer
                    'If Request("chkestado") = "" Then
                    '    estado = 0
                    'Else
                    'estado = 1
                    'End If
                    RegistrarConvocatoria(k, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, cod_per)
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    EliminarConvocatoria(k)
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

    Private Sub ListaConvocatorias(ByVal tipo As String, ByVal codigo As Integer, ByVal cod_test As String)
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""

            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaConvocatorias(tipo, codigo, cod_test)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_con")))
                    data.Add("cNombre", tb.Rows(i).Item("nombre_con"))

                    data.Add("cTest", tb.Rows(i).Item("descripcion_test"))
                    data.Add("cFecini", tb.Rows(i).Item("fecini_con"))
                    data.Add("cFecFin", tb.Rows(i).Item("fecfin_con"))
                    data.Add("cEstado", tb.Rows(i).Item("estado_con"))

                    If tipo = "E" Then
                        data.Add("cDetalle", tb.Rows(i).Item("descripcion_con"))
                        data.Add("cTes", obj.EncrytedString64(tb.Rows(i).Item("codigo_test")))
                        data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                    End If

                    'If tb.Rows(i).Item("activo") = 1 Then
                    '    data.Add("est", True)
                    'Else
                    '    data.Add("est", False)
                    'End If
                    'data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RegistrarConvocatoria(ByVal cod As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            dt = obj.ActualizarConvocatoria(cod, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, user_reg)
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

    Private Sub ModificarConvocatoria(ByVal cod As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable
            dt = obj.ActualizarConvocatoria(cod, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, user_reg)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - MOD")
            Data.Add("msje", ex.Message)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub EliminarConvocatoria(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarConvocatoria(cod)
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
