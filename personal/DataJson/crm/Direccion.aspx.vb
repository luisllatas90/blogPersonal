Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class Direccion
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
                    f = objCRM.DecrytedString64(Request("hdcodiD"))
                    ListaDirecciones("L", k, f)
                Case "Registrar"
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiD"))
                    Dim codigo_reg As Integer = objCRM.DecrytedString64(Request("cboRegionD"))
                    Dim codigo_prov As Integer = objCRM.DecrytedString64(Request("cboProvinciaD"))
                    Dim codigo_dis As Integer = objCRM.DecrytedString64(Request("cboDistritoD"))
                    Dim direccion As String = Request("txtDireccion")
                    Dim cod_per As Integer = Session("id_per")
                    Dim vigencia As Integer
                    If Request("chkVigenciaD") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    RegistrarDireccion(k, codigo_int, codigo_reg, codigo_prov, codigo_dis, direccion, vigencia, cod_per)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod_D"))
                    ListaDirecciones("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod_D"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiD"))
                    Dim codigo_reg As Integer = objCRM.DecrytedString64(Request("cboRegionD"))
                    Dim codigo_prov As Integer = objCRM.DecrytedString64(Request("cboProvinciaD"))
                    Dim codigo_dis As Integer = objCRM.DecrytedString64(Request("cboDistritoD"))
                    Dim direccion As String = Request("txtDireccion")
                    Dim cod_per As Integer = Session("id_per")
                    Dim vigencia As Integer
                    If Request("chkVigenciaD") = "" Then
                        vigencia = 0
                    Else
                        vigencia = 1
                    End If
                    RegistrarDireccion(k, codigo_int, codigo_reg, codigo_prov, codigo_dis, Direccion, vigencia, cod_per)
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

    Private Sub ListaDirecciones(ByVal tipo As String, ByVal codigo_dir As Integer, ByVal codigo_interesado As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            'Dim cn As New clsaccesodatos
            tb = obj.ListaDirecciones(tipo, codigo_dir, codigo_interesado)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_din")))
                    data.Add("dir", tb.Rows(i).Item("direccion_din"))

                    data.Add("dep", tb.Rows(i).Item("nombre_Dep"))
                    data.Add("pro", tb.Rows(i).Item("nombre_Pro"))
                    data.Add("dis", tb.Rows(i).Item("nombre_Dis"))
                    data.Add("vig", tb.Rows(i).Item("vigencia_din"))
                    data.Add("fec", tb.Rows(i).Item("fecha_Reg"))
                    If tipo = "E" Then
                        If tb.Rows(i).Item("codigo_Dep") <> "0" Then
                            data.Add("cdep", obj.EncrytedString64(tb.Rows(i).Item("codigo_Dep")))
                        Else
                            data.Add("cdep", "")
                        End If

                        If tb.Rows(i).Item("codigo_Pro") <> "0" Then
                            data.Add("cpro", obj.EncrytedString64(tb.Rows(i).Item("codigo_Pro")))
                        Else
                            data.Add("cpro", "")
                        End If

                        If tb.Rows(i).Item("codigo_Dis") <> "0" Then
                            data.Add("cdis", obj.EncrytedString64(tb.Rows(i).Item("codigo_Dis")))
                        Else
                            data.Add("cdis", "")
                        End If

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
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("rpta", "0 - LIST-EDIT")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarDireccion(ByVal cod As Integer, ByVal codigo_int As Integer, ByVal codigo_reg As Integer, ByVal codigo_prov As Integer, ByVal codigo_dis As Integer, ByVal direccion As String, ByVal vigencia As Integer, ByVal user_reg As Integer)
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
            dt = obj.ActualizarDireccion(cod, codigo_int, codigo_reg, codigo_prov, codigo_dis, direccion, vigencia, user_reg)
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
