Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class Telefono
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
                    f = objCRM.DecrytedString64(Request("hdcodiT"))
                    ListaTelefonos("L", k, f)
                Case "Registrar"
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiT"))
                    Dim tipo As String = Request("cboTipoTelefono")
                    Dim numero As String = Request("txtnumeroTel")
                    Dim detalle As String = Request("txtdetalleTel")
                    Dim cod_per As Integer = Session("id_per")
                    Dim pertenencia As String = Request("rbtPertenencia")
                    Dim vigencia As Integer
                    If Request("chkVigenciaT") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    RegistrarTelefono(k, codigo_int, tipo, numero, detalle, vigencia, cod_per, pertenencia)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod_T"))
                    ListaTelefonos("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod_T"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiT"))
                    Dim tipo As String = Request("cboTipoTelefono")
                    Dim numero As String = Request("txtnumeroTel")
                    Dim detalle As String = Request("txtdetalleTel")
                    Dim cod_per As Integer = Session("id_per")
                    Dim pertenencia As String = Request("rbtPertenencia")
                    Dim vigencia As Integer
                    If Request("chkVigenciaT") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    RegistrarTelefono(k, codigo_int, tipo, numero, detalle, vigencia, cod_per, pertenencia)
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

    Private Sub ListaTelefonos(ByVal tipo As String, ByVal codigo_tel As Integer, ByVal codigo_interesado As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            'Dim cn As New clsaccesodatos
            tb = obj.ListaTelefonos(tipo, codigo_tel, codigo_interesado)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_tei")))
                    data.Add("tip", tb.Rows(i).Item("tipotel_tei"))

                    data.Add("nro", tb.Rows(i).Item("numero_tei"))
                    data.Add("det", tb.Rows(i).Item("detalle_tei"))
                    data.Add("vig", tb.Rows(i).Item("vigencia_tei"))
                    data.Add("fec", tb.Rows(i).Item("fecha_Reg"))
                    data.Add("prt", tb.Rows(i).Item("pertenencia_tei"))
                    data.Add("nprt", tb.Rows(i).Item("nombrePertenencia_tei"))
                    'If tipo = "E" Then
                    '    data.Add("cDetalle", tb.Rows(i).Item("descripcion_con"))
                    '    data.Add("cTes", obj.EncrytedString64(tb.Rows(i).Item("codigo_test")))
                    '    data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                    'End If

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
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("rpta", "0 - REG")
            data1.Add("msje", ex.Message)
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarTelefono(ByVal cod As Integer, ByVal codigo_int As Integer, ByVal tipo As String, ByVal numero As String, ByVal detalle As String, ByVal vigencia As Integer, ByVal user_reg As Integer, ByVal pertenencia As String)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.ActualizarTelefono(cod, codigo_int, tipo, numero, detalle, vigencia, user_reg, pertenencia)
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

    'Private Sub ModificarConvocatoria(ByVal cod As Integer, ByVal codigo_test As Integer, ByVal codigo_cac As Integer, ByVal nombre As String, ByVal detalle As String, ByVal fecini As String, ByVal fecfin As String, ByVal estado As Integer, ByVal user_reg As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        'For i As Integer = 0 To arr.Count - 1
    '        '    Data.Add(i, arr(i))
    '        'Next
    '        'list.Add(Data)
    '        Dim dt As New Data.DataTable
    '        dt = obj.ActualizarConvocatoria(cod, codigo_test, codigo_cac, nombre, detalle, fecini, fecfin, estado, user_reg)
    '        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data.Add("rpta", "0 - MOD")
    '        Data.Add("msje", ex.Message)
    '        list.Add(Data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    'Private Sub EliminarConvocatoria(ByVal cod As Integer)
    '    Dim obj As New ClsCRM
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        dt = obj.EliminarConvocatoria(cod)
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

End Class
