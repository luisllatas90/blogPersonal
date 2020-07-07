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
                    Dim codigo_tis As Integer = 0
                    Dim codigo_tua As Integer = 0
                    Dim tipo_rango As String = ""
                    Dim rango As String = ""
                    Dim cod_per As Integer = Session("id_per")
                    Dim cod_ctf As Integer = Session("ctf")
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    Dim tipo As String = Request("tipo")
                    If Request("cboTipoS") <> "" Then codigo_tis = objT.DecrytedString64(Request("cboTipoS"))
                    If Request("cboAlumno") <> "" Then codigo_tua = objT.DecrytedString64(Request("cboAlumno"))
                    tipo_rango = Request("cboRango")
                    If tipo_rango = "M" Then
                        rango = Request("cboMes")
                    End If
                    If tipo_rango = "D" Then
                        rango = Request("dtpDia")
                    End If
                    If tipo <> "L" And tipo <> "LS" Then
                        tipo_rango = ""
                        rango = ""
                        If Request("k") = "" Then
                            cod_per = 0
                        Else
                            cod_per = objT.DecrytedString64(Request("k"))
                        End If
                    End If
                    If tipo = "LS" Then
                        If Request("k") = "" Then
                            cod_per = 0
                        Else
                            cod_per = objT.DecrytedString64(Request("k"))
                        End If
                    End If
                    ListaSesiones(tipo, cod_ctf, 0, cod_per, codigo_cac, codigo_tis, codigo_tua, tipo_rango, rango)
                Case "Registrar"
                    Dim codigo_cur As Integer = 0
                    Dim codigo_cpf As Integer = 0
                    Dim tipo As Integer = Request("tipo")
                    f = objT.DecrytedString64(Request("cboTipo"))
                    Dim alumnos() As Object = serializer.DeserializeObject(Request("array"))
                    Dim descripcion As String = Request("lblDescripcion")
                    Dim estado As Integer = 1
                    Dim cod_per As Integer = Session("id_per")
                    Dim cod_ctf As Integer = Session("ctf")
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    If Request("cboCurso") <> "" Then codigo_cur = objT.DecrytedString64(Request("cboCurso"))
                    If Request("cboCarrera") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboCarrera"))

                    If tipo = 1 Then
                        Dim fecha As String = Request("dtpFecha")
                        Dim horaInicio As String = Request("cboHoraD").PadLeft(2, "0") & ":" & Request("cboMinutoD").PadLeft(2, "0")
                        Dim horaFin As String = Request("cboHoraA").PadLeft(2, "0") & ":" & Request("cboMinutoA").PadLeft(2, "0")
                        'If f = 4 Then
                        '    RegistrarSesionAlumno(0, codigo_cac, f, descripcion, fecha, horaInicio, horaFin, estado, cod_per, alumnos)
                        'Else
                        RegistrarSesion(0, codigo_cac, f, descripcion, fecha, horaInicio, horaFin, estado, cod_per, codigo_cur, codigo_cpf)
                        'End If
                    End If
                    If tipo = 2 Then
                        Dim dias() As Object = serializer.DeserializeObject(Request("array1"))
                        Dim horaInicio As String = Request("cboHoraDV").PadLeft(2, "0") & ":" & Request("cboMinutoDV").PadLeft(2, "0")
                        Dim horaFin As String = Request("cboHoraAV").PadLeft(2, "0") & ":" & Request("cboMinutoAV").PadLeft(2, "0")
                        Dim desde As String = Request("dtpDesde")
                        Dim hasta As String = Request("dtpHasta")
                        Dim semana As Integer = CInt(Request("cboSemana"))
                        RegistrarSesionMasiva(0, codigo_cac, f, descripcion, desde, hasta, horaInicio, horaFin, semana, estado, cod_per, dias, codigo_cur, codigo_cpf)
                    End If
                    'Dim detalles() As Object = serializer.DeserializeObject(Request("array"))

                    ' RegistrarSesion (

                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcodSE"))
                    Dim cod_per As Integer = Session("id_per")
                    Dim cod_ctf As Integer = Session("ctf")
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    ListaSesiones("L", cod_ctf, k, cod_per, codigo_cac, 0, 0, "", "")
                Case "Modificar"
                    f = objT.DecrytedString64(Request("hdcodS"))
                    Dim codigo_cur As String = 0
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    Dim codigo_tis As String = objT.DecrytedString64(Request("cboTipo"))
                    Dim codigo_cpf As String = objT.DecrytedString64(Request("cboCarrera"))
                    If Request("cboCurso") <> "" Then codigo_cur = objT.DecrytedString64(Request("cboCurso"))
                    Dim descripcion As String = Request("lblDescripcion")
                    Dim cod_per As Integer = Session("id_per")
                    Dim fecha As String = Request("dtpFecha")
                    Dim horaInicio As String = Request("cboHoraDM").PadLeft(2, "0") & ":" & Request("cboMinutoDM").PadLeft(2, "0")
                    Dim horaFin As String = Request("cboHoraAM").PadLeft(2, "0") & ":" & Request("cboMinutoAM").PadLeft(2, "0")
                    ActualizarSesion(f, codigo_cac, codigo_tis, descripcion, fecha, horaInicio, horaFin, 1, cod_per, codigo_cpf, codigo_cur)
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    'k = objT.DecrytedString64(Request("cod"))
                    EliminaSesionTutor(k)
                Case "ListarEscuela"
                    'ListaTutores("ESC", 0, 0)

            End Select
            'Data.Add("array", detalles(0).Item("eval"))
        Catch ex As Exception

            'Data.Add("action", objT.DecrytedString64(Request("action")))
            'Data.Add("codigo_cac", objT.DecrytedString64(Request("cboCicloAcad")))
            'Data.Add("codigo_tis", objT.DecrytedString64(Request("cboTipo")))
            'Data.Add("descripcion", Request("lblDescripcion"))
            'Data.Add("fecha", Request("dtpFecha"))
            'Data.Add("horaInicio", Request("dtpFecha"))
            'Data.Add("horaFin", Request("dtpFecha"))
            'Data.Add("f", objT.DecrytedString64(Request("hdcodS")))
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

    Private Sub ListaSesiones(ByVal tipo As String, ByVal codigo_ctf As Integer, ByVal codigo_stu As Integer, ByVal codigo_per As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal codigo_tua As Integer, ByVal tipo_rango As String, ByVal rango As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb, tb1 As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaSesiones(tipo, codigo_ctf, codigo_stu, codigo_per, codigo_cac, codigo_tis, codigo_tua, tipo_rango, rango)
            tb1 = obj.ListaTutores("L", codigo_cac, codigo_per, 0)

            If tb1.Rows.Count > 0 Or (tipo = "L" Or tipo = "LS") Then
                If tipo <> "L" And tipo <> "LS" Then
                    Dim data1 As New Dictionary(Of String, Object)()
                    data1.Add("cDtc", tb1.Rows(0).Item("nombreTutor"))
                    list.Add(data1)
                End If

                If tb.Rows.Count > 0 Then
                    For i As Integer = 0 To tb.Rows.Count - 1
                        Dim data As New Dictionary(Of String, Object)()

                        'If i = 0 Then data.Add("sw", True)

                        data.Add("cTc", obj.EncrytedString64(tb.Rows(i).Item("codigo_tc")))
                        data.Add("cStu", obj.EncrytedString64(tb.Rows(i).Item("codigo_stu")))
                        data.Add("cTis", obj.EncrytedString64(tb.Rows(i).Item("codigo_tis")))
                        data.Add("cFecha", tb.Rows(i).Item("fecha"))
                        data.Add("cDTis", tb.Rows(i).Item("descripcion_tis"))
                        data.Add("cDstu", tb.Rows(i).Item("descripcion_stu"))
                        data.Add("cHini", tb.Rows(i).Item("horaInicio_stu"))
                        data.Add("cHfin", tb.Rows(i).Item("horaFin_stu"))
                        data.Add("cCarr", tb.Rows(i).Item("nombre_cpf"))
                        data.Add("cCur", tb.Rows(i).Item("nombre_cur"))
                        data.Add("cMod", tb.Rows(i).Item("modificados"))
                        data.Add("cTotal", tb.Rows(i).Item("total"))
                        data.Add("cInd", tb.Rows(i).Item("individual"))
                        data.Add("cPre", tb.Rows(i).Item("presente"))
                        If tipo = "LTUA" Then
                            data.Add("cDtc", tb.Rows(i).Item("tutor"))
                            data.Add("cAsis", tb.Rows(i).Item("asistencia_stu"))
                        End If

                        list.Add(data)
                    Next
                End If
            End If



            'data2.Add("tipo", tipo)
            'data2.Add("tb1.Rows.Count", tb1.Rows.Count)
            'data2.Add("tb.Rows.Count", tb.Rows.Count)
            ''data2.Add("codigocpf", codigocpf)
            ''data2.Add("codigo_cai", codigo_cai)
            'list.Add(data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            'data2.Add("tipo", tipo)
            data2.Add("sp", tipo & " " & codigo_ctf & " " & codigo_stu & " " & codigo_per & " " & codigo_cac & " " & codigo_tis & " " & codigo_tua & " " & tipo_rango & " " & rango)
            data2.Add("rpta", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    'Private Sub RegistrarEvaluacionEntrada(ByVal codtua As Integer, ByVal estado As Integer, ByVal user_reg As Integer)
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
    '        dt = obj.RegistrarEvaluacionEntrada(codtua, estado, user_reg)
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
    'Private Sub RegistrarSesionAlumno(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_stu As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal detalle() As Object)
    '    Dim obj As New clsTutoria
    '    Dim Data2 As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        Dim cod, rpta As Integer
    '        dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, fecha_stu, horaInicio_stu, horaFin_stu, estado, usuario_reg, 0)
    '        rpta = CInt(dt.Rows(0).Item("Respuesta"))
    '        cod = CInt(dt.Rows(0).Item("COD"))
    '        If rpta = 1 Then
    '            For i As Integer = 0 To detalle.Length - 1
    '                Dim dt1 As New Data.DataTable
    '                Dim Data As New Dictionary(Of String, Object)()
    '                dt1 = obj.ActualizarSesiónAlumno(cod, obj.DecrytedString64(detalle(i).Item("hdc")), 0, estado, "", "", usuario_reg)
    '                Data.Add("rpta", dt1.Rows(0).Item("Respuesta"))
    '                Data.Add("msje", dt1.Rows(0).Item("Mensaje").ToString)
    '                list.Add(Data)
    '            Next
    '        End If

    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data2.Add("rpta", "0 - REG")
    '        Data2.Add("s", detalle)
    '        Data2.Add("msje", ex.Message)
    '        list.Add(Data2)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub
    Private Sub RegistrarSesion(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_stu As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal codigo_cur As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New clsTutoria
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            Dim Data As New Dictionary(Of String, Object)()
            dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, fecha_stu, horaInicio_stu, horaFin_stu, estado, usuario_reg, codigo_cur, codigo_cpf)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("COD")))
            list.Add(Data)


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
    Private Sub ActualizarSesion(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_stu As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal codigo_cpf As Integer, Optional ByVal codigo_cur As Integer = 0)
        Dim obj As New clsTutoria
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim Data As New Dictionary(Of String, Object)()
            Dim dt As New Data.DataTable
            dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, fecha_stu, horaInicio_stu, horaFin_stu, estado, usuario_reg, codigo_cur, codigo_cpf)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

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
    Private Sub RegistrarSesionMasiva(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_desde As String, ByVal fecha_hasta As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal semana As Integer, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal dias() As Object, ByVal codigo_cur As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New clsTutoria
        Dim SecondDate, hasta, aux As Date
        Dim IntervalType, IntervalType1 As DateInterval
        Dim Months, dia As Double
        Dim cont As Integer
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            IntervalType = DateInterval.WeekOfYear   ' Specifies semana as interval.
            IntervalType1 = DateInterval.Day   ' Specifies dia as interval.
            SecondDate = CDate(fecha_desde)
            hasta = CDate(fecha_hasta)
            Months = Val(1)
            dia = Val(1)

            cont = 0
            While SecondDate <= fecha_hasta
                'Response.Write()

                aux = SecondDate
                For i As Integer = 1 To 7

                    aux = DateAdd(IntervalType1, dia, aux)
                    If aux <= fecha_hasta Then
                        For y As Integer = 0 To dias.Length - 1
                            If Weekday(aux, Microsoft.VisualBasic.FirstDayOfWeek.Monday) = dias(y).Item("dy") Then
                                'Response.Write(aux)
                                Dim Data As New Dictionary(Of String, Object)()
                                Dim dt As New Data.DataTable
                                Dim cod, rpta As Integer
                                dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, aux, horaInicio_stu, horaFin_stu, estado, usuario_reg, codigo_cur, codigo_cpf)
                                'rpta = CInt(dt.Rows(0).Item("Respuesta"))
                                'cod = CInt(dt.Rows(0).Item("COD"))
                                'If rpta = 1 Then
                                '    For z As Integer = 0 To detalle.Length - 1
                                '        Dim dt1 As New Data.DataTable
                                '        dt1 = obj.ActualizarSesiónAlumno(cod, obj.DecrytedString64(detalle(z).Item("hdc")), 0, estado, "", "", usuario_reg)
                                '        'dt1 = obj.ActualizarSesión(codigo_stu, obj.DecrytedString64(detalle(z).Item("hdc")), codigo_tis, descripcion_stu, aux, horaInicio_stu, horaFin_stu, estado, usuario_reg)
                                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                                Data.Add("cod", obj.EncrytedString64(dt.Rows(0).Item("COD").ToString))
                                list.Add(Data)

                                '        If dt.Rows(0).Item("Respuesta") = 1 Then
                                cont = cont + 1
                                '        End If
                                '    Next
                                'End If


                            End If
                        Next
                    End If

                Next
                SecondDate = DateAdd(IntervalType, Months, SecondDate)

            End While

            If cont = 0 Then
                Data2.Add("rpta", 0)
                Data2.Add("msje", "Seleccione los días de la semana que se encuentren dentro del intervalo de fechas de sesión seleccionado")
                list.Add(Data2)
            End If

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
    'Private Sub ModificarDetalleEval(ByVal codigo_tua As Integer, ByVal estado As Integer, ByVal user_reg As Integer, ByVal detalle() As Object)
    '    Dim obj As New clsTutoria
    '    Dim Data2 As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try
    '        'For i As Integer = 0 To arr.Count - 1
    '        '    Data.Add(i, arr(i))
    '        'Next
    '        'list.Add(Data)
    '        Dim dt, dt2 As New Data.DataTable
    '        dt2 = obj.InactivarDetalleEvaluacion(codigo_tua, user_reg)

    '        If dt2.Rows(0).Item("Respuesta") = 1 Then
    '            For i As Integer = 0 To detalle.Length - 1
    '                Dim Data As New Dictionary(Of String, Object)()
    '                dt = obj.ModificarDetalleEvaluacionEntrada(codigo_tua, obj.DecrytedString64(detalle(i).Item("item")), detalle(i).Item("resultado"), estado, user_reg)
    '                Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
    '                Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
    '                list.Add(Data)
    '            Next
    '        End If



    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    Catch ex As Exception
    '        Data2.Add("rpta", "0 - REG")
    '        Data2.Add("msje", ex.Message)
    '        list.Add(Data2)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub
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



    Private Sub EliminaSesionTutor(ByVal cod As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarSesionTutor(cod)
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
