Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Partial Class DataJson_crm_Tutor
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
                    Dim tipo As String = "L"
                    Dim ctf As String = 0
                    f = "0"
                    If Not Request("cboCicloAcad") Is Nothing Then f = objT.DecrytedString64(Request("cboCicloAcad"))
                    If Not Request("hdcod") Is Nothing Then k = objT.DecrytedString64(Request("hdcod"))
                    If Request("tipo") <> "" Then tipo = Request("tipo")
                    If tipo = "LCTF" Then
                        k = Session("id_per")
                        ctf = Session("ctf")
                    End If
                    ListaTutores(tipo, f, k, ctf)
                Case "Registrar"
                    Dim codigo_per As Integer = objT.DecrytedString64(Request("cod"))
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    'Dim nombre As String = Request("txtnombre")
                    'Dim detalle As String = Request("txtdetalle")
                    Dim fecini As String = "01/01/1999"
                    Dim fecfin As String = "01/01/1999"
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer = 1
                    'If Request("chkestado") = "" Then
                    '    estado = 0
                    'Else
                    '    estado = 1
                    'End If
                    RegistrarTutor(k, codigo_cac, codigo_per, fecini, fecfin, estado, cod_per)
                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    ListaTutores("E", 0, k)
                Case "Modificar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcadM"))
                    Dim codigo_per As Integer = objT.DecrytedString64(Request("cboPersonal"))
                    Dim fecini As String = ""
                    Dim fecfin As String = ""
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer
                    If Request("chkEstado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If

                    RegistrarTutor(k, codigo_cac, codigo_per, fecini, fecfin, estado, cod_per)
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    EliminaTutor(k)
                Case "ListarEscuela"
                    'k = ""
                    If Request("cboCategoria") <> "" Then
                        k = objT.DecrytedString64(Request("cboCategoria"))
                    Else
                        k = ""
                    End If
                    'If Not Request("cboCategoria") Is Nothing Then k = objT.DecrytedString64(Request("cboCategoria"))
                    f = objT.DecrytedString64(Request("cboCicloAcad"))
                    PoblacionObjetivo("ESC", f, k, 0, 0, "")
            End Select

            
        Catch ex As Exception

            Data.Add("action", objT.DecrytedString64(Request("action")))
            Data.Add("epr", objT.DecrytedString64(Request("cod")))
            Data.Add("cac", objT.DecrytedString64(Request("cboCicloAcademicoR")))
            Data.Add("idper", Session("id_per"))
            Data.Add("rpta", "0 - LOAD")
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub PoblacionObjetivo(ByVal opcion As String, ByVal codigo_cac As String, ByVal categoria As String, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal riesgo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try

            Dim ejm As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New clsTutoria
            Dim dt, dt2 As New Data.DataTable
            dt = obj.PoblacionObjetivo(opcion, codigo_cac, categoria, codigo_cpf, codigo_cai, riesgo)

            If dt.Rows.Count > 0 Then

                If opcion = "ESC" Then

                    ejm.Add("cCant", CStr(dt.Rows(0).Item("cant")) + " alumnos encontrados")
                    list.Add(ejm)
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim data As New Dictionary(Of String, Object)()
                        data.Add("cCod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                        data.Add("cNombre", dt.Rows(i).Item("nombre_Cpf"))
                        list.Add(data)
                    Next

                End If

            End If

            'ejm.Add("ciclo", codigo_cac)
            'ejm.Add("opc", opcion)
            'list.Add(ejm)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message & "-PORASIG")
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub ListaTutores(ByVal tipo As String, ByVal codigo As Integer, ByVal codigotc As Integer, Optional ByVal codigoctf As Integer = 0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaTutores(tipo, codigo, codigotc, codigoctf)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()

                    'If i = 0 Then data.Add("sw", True)
                    If tipo = "ESC" Then
                        data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cpf")))
                        data.Add("cNombre", tb.Rows(i).Item("nombre_Cpf"))
                        data.Add("cAbrev", tb.Rows(i).Item("abreviatura_Cpf"))
                    ElseIf tipo = "LCTF" Then
                        data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo")))
                        data.Add("nombre", tb.Rows(i).Item("nombre"))
                    ElseIf tipo = "E" Then
                        data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                        data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_tc")))
                        data.Add("cNombre", tb.Rows(i).Item("nombreTutor"))
                        data.Add("cFecini", tb.Rows(i).Item("fechaIni"))
                        data.Add("cFecFin", tb.Rows(i).Item("fechaFin"))
                        data.Add("cEstado", tb.Rows(i).Item("estado_tc"))
                        data.Add("cPer", obj.EncrytedString64(tb.Rows(i).Item("codigo_per")))
                    Else
                        data.Add("cCod", obj.EncrytedString64(tb.Rows(i).Item("codigo_tc")))
                        data.Add("cNombre", tb.Rows(i).Item("nombreTutor"))
                        'data.Add("cTest", tb.Rows(i).Item("descripcion_test"))
                        data.Add("cFecini", tb.Rows(i).Item("fechaIni"))
                        data.Add("cFecFin", tb.Rows(i).Item("fechaFin"))
                        data.Add("cEstado", tb.Rows(i).Item("estado_tc"))
                        data.Add("cPer", obj.EncrytedString64(tb.Rows(i).Item("codigo_per")))
                        data.Add("cSesion", tb.Rows(i).Item("sesiones"))
                        data.Add("cTutorados", tb.Rows(i).Item("tutorados"))

                        'If tipo = "A" Then
                        '    data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                        '    'If tb.Rows(i).Item("CANT") > 0 Then
                        '    For i As Integer = 0 To tb.Rows(i).Item("CANT")
                        '        Dim data2 As New Dictionary(Of String, Object)()
                        '        data2.Add("cAlu", obj.EncrytedString64(tb.Rows(i).Item("codigo_Alu")))
                        '        data2.Add("cCodU", tb.Rows(i).Item("codigoUniver_Alu"))
                        '        data2.Add("cAlumno", tb.Rows(i).Item("alumno"))
                        '        data2.Add("cCat", tb.Rows(i).Item("categoria_tua"))
                        '        data2.Add("cAbrev", tb.Rows(i).Item("abreviatura_Cpf"))
                        '        list.Add(data2)
                        '        'End If
                        '    Next
                        'End If

                        'If tb.Rows(i).Item("activo") = 1 Then
                        '    data.Add("est", True)
                        'Else
                        '    data.Add("est", False)
                        'End If
                        'data.Add("nFiles", tb.Rows(i).Item("canarchivos"))
                    End If
                    'data.Add("eee", "a")
                    list.Add(data)
                Next
            End If

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data2.Add("rpta", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarTutor(ByVal cod As Integer, ByVal codigo_cac As Integer, ByVal codigo_per As Integer, ByVal fecini As String, ByVal fecfin As String, ByVal estado As Integer, ByVal user_reg As Integer)
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
            dt = obj.ActualizarTutor(cod, codigo_cac, codigo_per, fecini, fecfin, estado, user_reg)
            Data.Add("rrr", codigo_per)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            Data.Add("ini", fecini)
            Data.Add("fin", fecfin)
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

    Private Sub EliminaTutor(ByVal cod As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarTutor(cod)
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
