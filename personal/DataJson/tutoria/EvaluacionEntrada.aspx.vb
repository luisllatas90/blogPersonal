Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_tutoria_TutorAlumno
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objT As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case objT.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim cod As Integer
                    cod = objT.DecrytedString64(Request("codE"))
                    ListaDetalleEvaluacion("L", cod)
                Case "ListarTotales"
                    Dim cod As Integer
                    cod = objT.DecrytedString64(Request("codE"))
                    ListaDetalleEvaluacion("LT", cod)
                Case "Registrar"

                    Dim detalles() As Object = serializer.DeserializeObject(Request("array"))
                    f = objT.DecrytedString64(Request("hdcod"))
                    Dim cod_per As Integer = Session("id_per")
                    RegistrarDetalleEval(f, 1, cod_per, detalles)

                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    'ListaTutores("E", 0, k)
                Case "Modificar"
                    Dim detalles() As Object = serializer.DeserializeObject(Request("array"))
                    f = objT.DecrytedString64(Request("hdcod"))
                    Dim cod_per As Integer = Session("id_per")
                    ModificarDetalleEval(f, 1, cod_per, detalles)
                Case "Eliminar"
                    'k = objT.DecrytedString64(Request("cod"))
                    ''k = objT.DecrytedString64(Request("cod"))
                    'Dim tipo As Integer = Request("tipo")
                    'If tipo = 1 Then EliminaTutorAlumno(k)
                Case "ListarEscuela"
                    'ListaTutores("ESC", 0, 0)

            End Select
            'Data.Add("array", detalles(0).Item("eval"))
        Catch ex As Exception

            'Data.Add("action", objT.DecrytedString64(Request("action")))
            'Data.Add("epr", objT.DecrytedString64(Request("cod")))
            'Data.Add("cac", objT.DecrytedString64(Request("cboCicloAcademicoR")))
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", ex.Message & "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    'Private Sub ListaTutorados(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_cac As Integer)
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim data2 As New Dictionary(Of String, Object)()
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim obj As New clsTutoria
    '        Dim tb As New Data.DataTable
    '        'Dim cn As New clsaccesodatos
    '        tb = obj.ListaTutorados(tipo, codigo, codigo_cac)

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()

    '                'If i = 0 Then data.Add("sw", True)
    '                'If tipo = "A" Then
    '                data.Add("cTA", obj.EncrytedString64(tb.Rows(i).Item("codigo_tua")))
    '                data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
    '                data.Add("cAlu", obj.EncrytedString64(tb.Rows(i).Item("codigo_Alu")))
    '                data.Add("cCodU", tb.Rows(i).Item("codigoUniver_Alu"))
    '                data.Add("cAlumno", tb.Rows(i).Item("alumno"))
    '                data.Add("cCat", tb.Rows(i).Item("categoria"))
    '                data.Add("cAbrev", tb.Rows(i).Item("nombre_Cpf"))
    '                'End If

    '                'If tb.Rows(i).Item("activo") = 1 Then
    '                '    data.Add("est", True)
    '                'Else
    '                '    data.Add("est", False)
    '                'End If
    '                'data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
    '                'End If
    '                'data.Add("eee", "a")
    '                list.Add(data)
    '            Next
    '        End If

    '        'data2.Add("CODIGO", codigo)
    '        'data2.Add("codigotc", codigotc)
    '        'data2.Add("codigocpf", codigocpf)
    '        'data2.Add("codigo_cai", codigo_cai)
    '        'list.Add(data2)

    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        data2.Add("rpta", ex.Message)
    '        list.Add(data2)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    Private Sub ListaDetalleEvaluacion(ByVal tipo As String, ByVal codigo As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaDetalleEvaluacion(tipo, codigo)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()

                    'If i = 0 Then data.Add("sw", True)

                    data.Add("cDet", obj.EncrytedString64(tb.Rows(i).Item("CODIGO_DEVA")))
                    data.Add("cTeva", obj.EncrytedString64(tb.Rows(i).Item("CODIGO_TE")))
                    data.Add("cVt", obj.EncrytedString64(tb.Rows(i).Item("CODIGO_VT")))
                    data.Add("cOpcion", obj.EncrytedString64(tb.Rows(i).Item("CODIGO_ov")))
                    data.Add("cDeva", tb.Rows(i).Item("descripcion_te"))
                    data.Add("cDVar", tb.Rows(i).Item("descripcion_ve"))
                    data.Add("cPuntaje", tb.Rows(i).Item("PUNTAJE"))
                    data.Add("cTipo", tb.Rows(i).Item("TIPO_VAR"))
                    data.Add("cResultado", tb.Rows(i).Item("Resultado"))
                    data.Add("cNrt", obj.EncrytedString64(tb.Rows(i).Item("codigo_nrt")))
                    data.Add("cRiesgo", tb.Rows(i).Item("descripcion_nrt"))

                    If tipo = "LT" Then
                        data.Add("cRiesgoF", tb.Rows(i).Item("riesgofinal"))
                    End If

                    'If tb.Rows(i).Item("activo") = 1 Then
                    '    data.Add("est", True)
                    'Else
                    '    data.Add("est", False)
                    'End If
                    'data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
                    'End If
                    'data.Add("eee", "a")
                    list.Add(data)
                Next
            End If

            'data2.Add("tipo", tipo)
            'data2.Add("codigo", codigo)
            ''data2.Add("codigocpf", codigocpf)
            ''data2.Add("codigo_cai", codigo_cai)
            'list.Add(data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data2.Add("tipo", tipo)
            data2.Add("codigo", codigo)
            data2.Add("rpta", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarEvaluacionEntrada(ByVal codtua As Integer, ByVal estado As Integer, ByVal user_reg As Integer)
        Dim obj As New clsTutoria
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
            dt = obj.RegistrarEvaluacionEntrada(codtua, estado, user_reg)
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
    Private Sub RegistrarDetalleEval(ByVal codigo_alu As Integer, ByVal estado As Integer, ByVal user_reg As Integer, ByVal detalle() As Object)
        Dim obj As New clsTutoria
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt As New Data.DataTable

            For i As Integer = 0 To detalle.Length - 1
                Dim Data As New Dictionary(Of String, Object)()
                Dim opcion As Integer = 0
                If detalle(i).Item("opcion") <> "0" Then opcion = obj.DecrytedString64(detalle(i).Item("opcion"))
                dt = obj.RegistrarDetalleEvaluacionEntrada(codigo_alu, obj.DecrytedString64(detalle(i).Item("item")), detalle(i).Item("resultado"), estado, user_reg, opcion)
                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                list.Add(Data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub ModificarDetalleEval(ByVal codigo_tua As Integer, ByVal estado As Integer, ByVal user_reg As Integer, ByVal detalle() As Object)
        Dim obj As New clsTutoria
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            'For i As Integer = 0 To arr.Count - 1
            '    Data.Add(i, arr(i))
            'Next
            'list.Add(Data)
            Dim dt, dt2 As New Data.DataTable
            dt2 = obj.InactivarDetalleEvaluacion(codigo_tua, user_reg)

            If dt2.Rows(0).Item("Respuesta") = 1 Then
                If detalle.Length > 0 Then
                    For i As Integer = 0 To detalle.Length - 1
                        Dim Data As New Dictionary(Of String, Object)()
                        Dim opcion As Integer = 0
                        If detalle(i).Item("opcion") <> "0" Then opcion = obj.DecrytedString64(detalle(i).Item("opcion"))
                        dt = obj.ModificarDetalleEvaluacionEntrada(codigo_tua, obj.DecrytedString64(detalle(i).Item("item")), detalle(i).Item("resultado"), estado, user_reg, opcion)
                        Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                        Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                        Data.Add("vt", obj.DecrytedString64(detalle(i).Item("item")))
                        Data.Add("ptj", detalle(i).Item("resultado"))
                        Data.Add("opcion", opcion)
                        list.Add(Data)
                    Next
                Else
                    Dim Data As New Dictionary(Of String, Object)()
                    Data.Add("rpta", dt2.Rows(0).Item("Respuesta"))
                    Data.Add("msje", dt2.Rows(0).Item("Mensaje").ToString)
                    list.Add(Data)
                End If
            End If

            Data2.Add("codigo_tua", codigo_tua)
            Data2.Add("user_reg", user_reg)
            list.Add(Data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG")
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    'Private Sub RegistrarTutorAlumnoFiltros(ByVal cod As Integer, ByVal user_reg As Integer, ByVal codigo_cac As Integer, ByVal cat As String, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer)
    '    Dim obj As New clsTutoria
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
    '        dt = obj.RegistrarTutorAlumnoFiltros(cod, user_reg, codigo_cac, cat, codigo_cpf, codigo_cai)
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



    'Private Sub EliminaTutorAlumno(ByVal cod As Integer)
    '    Dim obj As New clsTutoria
    '    Dim Data As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        dt = obj.EliminarTutorAlumno(cod)
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
