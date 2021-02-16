Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_CarreraProfesional
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
                    f = objCRM.DecrytedString64(Request("hdcodiCP"))
                    ListaCarrera("L", k, f)
                Case "Registrar"
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiCP"))
                    Dim codigo_cpf As Integer = objCRM.DecrytedString64(Request("cboCarrera"))
                    Dim codigo_eve As Integer = objCRM.DecrytedString64(Request("hdcodeve"))
                    Dim detalle As String = Request("txtdetalleCpf")
                    Dim cod_per As Integer = Session("id_per")
                    Dim prioridad As Integer = Request("cboPrioridad")
                    'If Request("chkVigenciaCpf") = "" Then
                    '    vigencia = 0
                    'Else
                    '    vigencia = 1
                    'End If
                    RegistrarCarrera(k, codigo_int, codigo_cpf, codigo_eve, detalle, prioridad, cod_per)
                Case "Editar"
                    k = objCRM.DecrytedString64(Request("hdcod_CP"))
                    ListaCarrera("E", k, f)
                Case "Modificar"
                    k = objCRM.DecrytedString64(Request("hdcod_CP"))
                    Dim codigo_int As Integer = objCRM.DecrytedString64(Request("hdcodiCP"))
                    Dim codigo_cpf As Integer = objCRM.DecrytedString64(Request("cboCarrera"))
                    Dim codigo_eve As Integer = objCRM.DecrytedString64(Request("hdcodeve"))
                    Dim detalle As String = Request("txtdetalleCpf")
                    Dim cod_per As Integer = Session("id_per")
                    Dim prioridad As Integer = Request("cboPrioridad")
                    'Dim vigencia As Integer
                    'If Request("chkVigenciaT") = "" Then
                    '    vigencia = 0
                    'Else
                    '    vigencia = 1
                    'End If
                    ModificarConvocatoria(k, codigo_int, codigo_cpf, codigo_eve, detalle, prioridad, cod_per)
                Case "Eliminar"
                    k = objCRM.DecrytedString64(Request("hdcod"))
                    EliminarCarrera(k)
                Case "ListaPrioridad"
                    If Request("hdcod") <> "0" Then
                        k = objCRM.DecrytedString64(Request("hdcod"))
                    End If
                    f = objCRM.DecrytedString64(Request("hdcodiCP"))
                    ListaPrioridad(k, f)
                Case "DatosCarreraProfesional"
                    Dim codigo_cpf As Integer = objCRM.DecrytedString64(Request("codigoCpf"))
                    ListarDatosCarreraProfesional(codigo_cpf)

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

    Private Sub ListaCarrera(ByVal tipo As String, ByVal codigo_cpi As Integer, ByVal codigo_interesado As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            'Dim cn As New clsaccesodatos
            tb = obj.ListaCarrera(tipo, codigo_cpi, codigo_interesado)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_cpi")))
                    data.Add("cpf", obj.EncrytedString64(tb.Rows(i).Item("codigo_cpf")))
                    data.Add("ncpf", tb.Rows(i).Item("nombre_cpf"))
                    data.Add("eve", tb.Rows(i).Item("nombre_eve"))
                    data.Add("test", tb.Rows(i).Item("descripcion_test"))
                    data.Add("pri", tb.Rows(i).Item("prioridad_cpi"))
                    data.Add("fec", tb.Rows(i).Item("fecha_Reg"))

                    data.Add("det", tb.Rows(i).Item("detalle_cpi"))


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
            data1.Add("rpta", "0 - LIS")
            data1.Add("msje", ex.Message)
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaPrioridad(ByVal codigo_cpi As Integer, ByVal codigo_interesado As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            'Dim cn As New clsaccesodatos
            tb = obj.ListaPrioridad(codigo_cpi, codigo_interesado)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", tb.Rows(i).Item("valor"))
                    data.Add("nom", tb.Rows(i).Item("prioridad"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("rpta", "0 - LISPRI")
            data1.Add("msje", ex.Message)
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarCarrera(ByVal cod As Integer, ByVal codigo_int As Integer, ByVal codigo_cpf As Integer, ByVal codigo_eve As Integer, ByVal detalle As String, ByVal prioridad As Integer, ByVal user_reg As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.ActualizarCarrera(cod, codigo_int, codigo_cpf, codigo_eve, detalle, prioridad, user_reg)
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

    Private Sub ModificarConvocatoria(ByVal cod As Integer, ByVal codigo_int As Integer, ByVal codigo_cpf As Integer, ByVal codigo_eve As Integer, ByVal detalle As String, ByVal prioridad As Integer, ByVal user_reg As Integer)
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
            dt = obj.ActualizarCarrera(cod, codigo_int, codigo_cpf, codigo_eve, detalle, prioridad, user_reg)
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

    Private Sub EliminarCarrera(ByVal cod As Integer)
        Dim obj As New ClsCRM
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarCarrera(cod)
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

    Private Sub ListarDatosCarreraProfesional(ByVal codigo_cpf As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim obj As New ClsCRM
        Dim tb As New Data.DataTable
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            tb = obj.ListarDatosCarreraProfesional(tipoConsulta:="GEN", codigoCpf:=codigo_cpf)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Item("codigoCpf") = tb.Rows(i).Item("codigo_cpf").ToString
                    data.Item("videoUrlDcp") = tb.Rows(i).Item("videoUrl_dcp").ToString
                    data.Item("brochureUrlDcp") = tb.Rows(i).Item("brochureUrl_dcp").ToString
                    data.Item("temarioUrlDcp") = tb.Rows(i).Item("temarioUrl_dcp").ToString
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("rpta", "0 - LISPRI")
            data1.Add("msje", ex.Message)
            list.Add(data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
End Class
