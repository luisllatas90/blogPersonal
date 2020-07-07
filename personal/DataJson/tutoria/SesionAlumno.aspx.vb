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
                    Dim cod_per As Integer = 0
                    Dim codigo_stu As Integer = objT.DecrytedString64(Request("hdcodSE"))
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    Dim cod_ctf As Integer = Session("ctf")
                    cod_per = Session("id_per")
                    ListaSesionesAlumnos("L", codigo_stu, 0, codigo_cac, cod_per, cod_ctf)
                Case "Individual"
                    Dim codigo_stu As Integer = 0
                    Dim codigo_tua As Integer = 0
                    Dim codigo_tc As Integer = 0
                    If Request("stu") <> "" Then codigo_stu = objT.DecrytedString64(Request("stu"))
                    If Request("hdcod") <> "" Then codigo_tua = objT.DecrytedString64(Request("hdcod"))
                    If Request("tc") <> "" Then codigo_tc = objT.DecrytedString64(Request("tc"))
                    ListaSesionesAlumnosIndividual("L", codigo_stu, codigo_tua, codigo_tc)
                Case "Registrar"
                    Dim alumnos() As Object = serializer.DeserializeObject(Request("array"))
                    Dim sesiones() As Object = serializer.DeserializeObject(Request("array1"))
                    Dim estado As Integer = 1
                    Dim cod_per As Integer = Session("id_per")

                    RegistrarSesionAlumno(estado, cod_per, alumnos, sesiones)
                    'Dim detalles() As Object = serializer.DeserializeObject(Request("array"))

                    ' RegistrarSesion (

                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcodSE"))
                    Dim cod_per As Integer = Session("id_per")
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    'ListaSesiones("L", k, cod_per, codigo_cac, 0, 0, "", "")
                Case "Modificar"
                    Dim detalles() As Object = serializer.DeserializeObject(Request("array"))
                    f = objT.DecrytedString64(Request("hdcodS"))
                    Dim cod_per As Integer = Session("id_per")
                    Dim cod_ctf As Integer = Session("ctf")
                    Dim tipo As Integer = Request("tipo")
                    If tipo = 1 Then
                        ModificarSesionAlumno(f, 1, cod_ctf, cod_per, detalles)
                    End If
                    If tipo = 2 Then

                        Dim asistencia As String = "P"
                        Dim problemas_stua As String = ""
                        'If Request("chkAsistir") = "" Then
                        '    asistencia = "F"
                        'Else
                        '    asistencia = "P"
                        'End If
                        'Dim asistencia As String = Request("chkAsistir")
                        Dim accion_stua As String = Request("txtAccion")
                        Dim codigo_tat As Integer = objT.DecrytedString64(Request("cboActividad"))
                        Dim codigo_tre As Integer = objT.DecrytedString64(Request("cboResultado"))
                        Dim codigo_nrt As Integer = objT.DecrytedString64(Request("cboRiesgo"))
                        Dim codigo_etu As Integer = objT.DecrytedString64(Request("cboEstado"))
                        Dim descripcionInc_stua As String = Request("txtIncidencia")
                        Dim comentarioTutor_stua As String = Request("txtComentario")
                        Dim fechaEjecucion_stua As String = Request("dtpFechaF")

                        For i As Integer = 0 To detalles.Length - 1
                            problemas_stua = problemas_stua + objT.DecrytedString64(detalles(i).Item("d")) + "/"
                        Next

                        ModificarSesionAlumnoIndividual(f, 1, asistencia, "", accion_stua, codigo_tat, codigo_tre, problemas_stua, codigo_nrt, codigo_etu, descripcionInc_stua, comentarioTutor_stua, fechaEjecucion_stua, cod_per)
                    End If
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    'k = objT.DecrytedString64(Request("cod"))
                    EliminaSesionAlumno(k)
                    'ListaTutores("ESC", 0, 0)

            End Select
            'Data.Add("s", Request("hdcodSE"))
            'Data.Add("d", objT.DecrytedString64(Request("hdcodSE")))
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)

        Catch ex As Exception
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

    Private Sub ListaSesionesAlumnos(ByVal tipo As String, ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_cac As Integer, Optional ByVal codigo_per As Integer = 0, Optional ByVal codigo_ctf As Integer = 0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb, tb1 As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaSesionesAlumnos(tipo, codigo_stu, codigo_tua, codigo_per, codigo_ctf)
            tb1 = obj.ListaSesiones("L", 0, codigo_stu, 0, codigo_cac, 0, 0, "", "")
            If tb1.Rows.Count > 0 Then
                Dim data1 As New Dictionary(Of String, Object)()
                data1.Add("cStu", obj.EncrytedString64(tb1.Rows(0).Item("codigo_stu")))
                data1.Add("cTis", obj.EncrytedString64(tb1.Rows(0).Item("codigo_tis")))
                data1.Add("ccCarr", obj.EncrytedString64(tb1.Rows(0).Item("codigo_cpf")))
                data1.Add("ccCur", obj.EncrytedString64(tb1.Rows(0).Item("codigo_cur")))
                data1.Add("cFecha", tb1.Rows(0).Item("fecha"))
                data1.Add("cDTis", tb1.Rows(0).Item("descripcion_tis"))
                data1.Add("cDstu", tb1.Rows(0).Item("descripcion_stu"))
                data1.Add("cHini", tb1.Rows(0).Item("horaInicio_stu"))
                data1.Add("cHfin", tb1.Rows(0).Item("horaFin_stu"))
                data1.Add("cCarr", tb1.Rows(0).Item("nombre_cpf"))
                data1.Add("cCur", tb1.Rows(0).Item("nombre_cur"))

                list.Add(data1)

                If tb1.Rows(0).Item("total") > 0 Then

                    If tb.Rows.Count > 0 Then
                        For i As Integer = 0 To tb.Rows.Count - 1
                            Dim data As New Dictionary(Of String, Object)()

                            'If i = 0 Then data.Add("sw", True)
                            Dim cAct As String = ""
                            Dim cRes As String = ""
                            Dim cNri As String = ""
                            Dim cEtu As String = ""
                            If tb.Rows(i).Item("codigo_tat") <> 0 Then cAct = obj.EncrytedString64(tb.Rows(i).Item("codigo_tat"))
                            If tb.Rows(i).Item("codigo_tre") <> 0 Then cRes = obj.EncrytedString64(tb.Rows(i).Item("codigo_tre"))
                            If tb.Rows(i).Item("codigo_nrt") <> 0 Then cNri = obj.EncrytedString64(tb.Rows(i).Item("codigo_nrt"))
                            If tb.Rows(i).Item("codigo_etu") <> 0 Then cEtu = obj.EncrytedString64(tb.Rows(i).Item("codigo_etu"))

                            data.Add("cTua", obj.EncrytedString64(tb.Rows(i).Item("codigo_tua")))
                            data.Add("cSA", obj.EncrytedString64(tb.Rows(i).Item("codigo_stua")))
                            data.Add("cAlumno", tb.Rows(i).Item("Alumno"))
                            data.Add("cTiene_asistencia", tb.Rows(i).Item("tiene_asistencia"))
                            data.Add("cAsistencia_stu", tb.Rows(i).Item("asistencia_stu"))
                            data.Add("cObservacion_stu", tb.Rows(i).Item("observacion_stu"))
                            data.Add("cAct", cAct)
                            data.Add("cRes", cRes)
                            data.Add("cNri", cNri)
                            data.Add("cEtu", cEtu)
                            data.Add("cActividad", tb.Rows(i).Item("descripcion_tat"))
                            data.Add("cIncidencia", tb.Rows(i).Item("incidencia_stua"))
                            data.Add("cComent", tb.Rows(i).Item("comentarioTutor_snp"))
                            data.Add("cAcc", tb.Rows(i).Item("accion_stua"))
                            data.Add("cEstado", tb.Rows(i).Item("descripcion_etu"))
                            data.Add("cFechaEj", tb.Rows(i).Item("fechaEjecucion_snp"))
                            data.Add("cResultado", tb.Rows(i).Item("descripcion_tre"))
                            data.Add("cRiesgo", tb.Rows(i).Item("descripcion_nrt"))

                            Dim problema As String = tb.Rows(i).Item("problemas_stua")
                            Dim problemas() As String = problema.Split("/")
                            Dim prob As String = ""
                            If tb.Rows(i).Item("problemas_stua") <> "" Then
                                For index As Integer = 0 To problemas.Length - 1
                                    If problemas(index) <> "" Then prob = prob + obj.EncrytedString64(problemas(index)) + "/"
                                Next

                            End If

                            data.Add("cPro", prob)
                            'data.Add("cPros", tb.Rows(i).Item("problemas_stua"))
                            'data.Add("cPro1", problemas(0))
                            'data.Add("cPro2", problemas(1))
                            'data.Add("cPro3", problemas(2))
                            'data.Add("cPro2w", problemas.Length)
                            list.Add(data)
                        Next
                    End If
                End If
            End If


            'data2.Add("tb1.Rows.Count ", codigo_stu)
            'data2.Add("tb.Rows.Count ", codigo_cac)
            ''data2.Add("codigo_stu", codigo_stu)
            '' ''data2.Add("codigo_tua", codigo_tua)
            ' '' ''data2.Add("codigocpf", codigocpf)
            ' '' ''data2.Add("codigo_cai", codigo_cai)
            'list.Add(data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            
            data2.Add("tipo", tipo)
            data2.Add("codigo", codigo_stu)
            data2.Add("rpta", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListaSesionesAlumnosIndividual(ByVal tipo As String, ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaSesionesAlumnosIndividual(tipo, codigo_stu, codigo_tua, codigo_tc)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()

                    'If i = 0 Then data.Add("sw", True)

                    data.Add("cStu", obj.EncrytedString64(tb.Rows(i).Item("codigo_stu")))
                    data.Add("cTua", obj.EncrytedString64(tb.Rows(i).Item("codigo_tua")))
                    data.Add("cSA", obj.EncrytedString64(tb.Rows(i).Item("codigo_stua")))
                    data.Add("cTis", obj.EncrytedString64(tb.Rows(i).Item("codigo_tis")))
                    data.Add("cFecha", tb.Rows(i).Item("fecha"))
                    data.Add("cDTis", tb.Rows(i).Item("descripcion_tis"))
                    data.Add("cDstu", tb.Rows(i).Item("descripcion_stu"))
                    data.Add("cHini", tb.Rows(i).Item("horaInicio_stu"))
                    data.Add("cHfin", tb.Rows(i).Item("horaFin_stu"))
                    data.Add("cAlumno", tb.Rows(i).Item("Alumno"))
                    data.Add("cTiene_asistencia", tb.Rows(i).Item("tiene_asistencia"))
                    data.Add("cAsistencia_stu", tb.Rows(i).Item("asistencia_stu"))
                    data.Add("cObservacion_stu", tb.Rows(i).Item("observacion_stu"))
                    data.Add("cAct", obj.EncrytedString64(tb.Rows(i).Item("codigo_tat")))
                    data.Add("cRes", obj.EncrytedString64(tb.Rows(i).Item("codigo_tre")))
                    data.Add("cNri", obj.EncrytedString64(tb.Rows(i).Item("codigo_nrt")))
                    data.Add("cEtu", obj.EncrytedString64(tb.Rows(i).Item("codigo_etu")))
                    data.Add("cActividad", tb.Rows(i).Item("descripcion_tat"))
                    data.Add("cIncidencia", tb.Rows(i).Item("incidencia_stua"))
                    data.Add("cComent", tb.Rows(i).Item("comentarioTutor_snp"))
                    data.Add("cAcc", tb.Rows(i).Item("accion_stua"))
                    data.Add("cEstado", tb.Rows(i).Item("descripcion_etu"))
                    data.Add("cFechaEj", tb.Rows(i).Item("fechaEjecucion_snp"))
                    data.Add("cResultado", tb.Rows(i).Item("descripcion_tre"))
                    data.Add("cRiesgo", tb.Rows(i).Item("descripcion_nrt"))
                    data.Add("cPros", tb.Rows(i).Item("problemas_stua"))

                    'data.Add("cPros", tb.Rows(i).Item("problemas_stua"))
                    'data.Add("cPro1", problemas(0))
                    'data.Add("cPro2", problemas(1))
                    'data.Add("cPro3", problemas(2))
                    'data.Add("cPro2w", problemas.Length)
                    list.Add(data)
                Next
            End If

            'data2.Add("sp", tipo & " " & codigo_stu & " " & codigo_tua & " " & codigo_tc)
            'data2.Add("codigo", codigo_stu)
            'data2.Add("codigo_tua", codigo_tua)
            ''data2.Add("codigocpf", codigocpf)
            ''data2.Add("codigo_cai", codigo_cai)
            'list.Add(data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            'data2.Add("tipo", tipo)
            'data2.Add("codigo", codigo)
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
    Private Sub RegistrarSesionAlumno(ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal detalle() As Object, ByVal sesiones() As Object)
        Dim obj As New clsTutoria
        Dim Data2 As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            For i As Integer = 0 To sesiones.Length - 1
                Dim dt1 As New Data.DataTable

                For y As Integer = 0 To detalle.Length - 1

                    Dim Data As New Dictionary(Of String, Object)()
                    dt1 = obj.ActualizarSesiónAlumno(obj.DecrytedString64(sesiones(i).Item("stu")), obj.DecrytedString64(detalle(y).Item("hdc")), 0, estado, "", "", usuario_reg)
                    Data.Add("rpta", dt1.Rows(0).Item("Respuesta"))
                    Data.Add("msje", dt1.Rows(0).Item("Mensaje").ToString)
                    Data.Add("c", dt1.Rows(0).Item("Total").ToString)
                    'Data.Add("aux", sesiones.Length)
                    'Data.Add("aux2", detalle.Length)
                    list.Add(Data)
                Next

            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - REG")
            Data2.Add("s", detalle)
            Data2.Add("msje", ex.Message)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    'Private Sub RegistrarSesion(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_stu As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal detalle() As Object)
    '    Dim obj As New clsTutoria
    '    Dim Data2 As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        Dim dt As New Data.DataTable
    '        Dim cod, rpta As Integer
    '        dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, fecha_stu, horaInicio_stu, horaFin_stu, estado, usuario_reg)
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
    'Private Sub RegistrarSesionMasiva(ByVal codigo_stu As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_desde As String, ByVal fecha_hasta As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal semana As Integer, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal detalle() As Object, ByVal dias() As Object)
    '    Dim obj As New clsTutoria
    '    Dim SecondDate, hasta, aux As Date
    '    Dim IntervalType, IntervalType1 As DateInterval
    '    Dim Months, dia As Double
    '    Dim cont As Integer
    '    Dim Data2 As New Dictionary(Of String, Object)()
    '    Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '    Dim JSONresult As String = ""
    '    Dim list As New List(Of Dictionary(Of String, Object))()
    '    Try

    '        IntervalType = DateInterval.WeekOfYear   ' Specifies semana as interval.
    '        IntervalType1 = DateInterval.Day   ' Specifies dia as interval.
    '        SecondDate = CDate(fecha_desde)
    '        hasta = CDate(fecha_hasta)
    '        Months = Val(1)
    '        dia = Val(1)

    '        cont = 0
    '        While SecondDate <= fecha_hasta
    '            'Response.Write()

    '            aux = SecondDate
    '            For i As Integer = 1 To 7

    '                aux = DateAdd(IntervalType1, dia, aux)
    '                If aux <= fecha_hasta Then
    '                    For y As Integer = 0 To dias.Length - 1
    '                        If Weekday(aux, Microsoft.VisualBasic.FirstDayOfWeek.Monday) = dias(y).Item("dy") Then
    '                            'Response.Write(aux)
    '                            Dim dt As New Data.DataTable
    '                            Dim cod, rpta As Integer
    '                            dt = obj.ActualizarSesión(codigo_stu, usuario_reg, codigo_cac, codigo_tis, descripcion_stu, aux, horaInicio_stu, horaFin_stu, estado, usuario_reg)
    '                            rpta = CInt(dt.Rows(0).Item("Respuesta"))
    '                            cod = CInt(dt.Rows(0).Item("COD"))
    '                            If rpta = 1 Then
    '                                For z As Integer = 0 To detalle.Length - 1
    '                                    Dim dt1 As New Data.DataTable
    '                                    Dim Data As New Dictionary(Of String, Object)()
    '                                    dt1 = obj.ActualizarSesiónAlumno(cod, obj.DecrytedString64(detalle(z).Item("hdc")), 0, estado, "", "", usuario_reg)
    '                                    'dt1 = obj.ActualizarSesión(codigo_stu, obj.DecrytedString64(detalle(z).Item("hdc")), codigo_tis, descripcion_stu, aux, horaInicio_stu, horaFin_stu, estado, usuario_reg)
    '                                    Data.Add("rpta", dt1.Rows(0).Item("Respuesta"))
    '                                    Data.Add("msje", dt1.Rows(0).Item("Mensaje").ToString)
    '                                    list.Add(Data)

    '                                    If dt.Rows(0).Item("Respuesta") = 1 Then
    '                                        cont = cont + 1
    '                                    End If
    '                                Next
    '                            End If


    '                        End If
    '                    Next
    '                End If

    '            Next
    '            SecondDate = DateAdd(IntervalType, Months, SecondDate)

    '        End While

    '        If cont = 0 Then
    '            Data2.Add("rpta", 0)
    '            Data2.Add("msje", "Seleccione los días de la semana que se encuentren dentro del intervalo de fechas de sesión seleccionado")
    '            list.Add(Data2)
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
    Private Sub ModificarSesionAlumno(ByVal codigo_stu As Integer, ByVal estado As Integer, ByVal codigo_ctf As Integer, ByVal usuario_reg As Integer, ByVal detalle() As Object)
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
            dt2 = obj.ResetSesionesAlumno(codigo_stu, codigo_ctf, usuario_reg)

            If dt2.Rows(0).Item("Respuesta") = 1 Then
                For i As Integer = 0 To detalle.Length - 1
                    Dim Data As New Dictionary(Of String, Object)()
                    dt = obj.ModificarSesionAlumno(obj.DecrytedString64(detalle(i).Item("sa")), "P", "", estado, usuario_reg)
                    Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
                    Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
                    list.Add(Data)
                Next
            End If

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - MOD")
            Data2.Add("msje", ex.Message)
            Data2.Add("codigo_stu", codigo_stu)
            Data2.Add("sa", obj.DecrytedString64(detalle(0).Item("sa")))
            Data2.Add("usuario_reg", usuario_reg)
            list.Add(Data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
    Private Sub ModificarSesionAlumnoIndividual(ByVal codigo_stua As Integer, ByVal estado As Integer, ByVal asistencia As String, ByVal obs As String, ByVal accion_stua As String, ByVal codigo_tat As Integer, ByVal codigo_tre As Integer, ByVal problemas_stua As String, ByVal codigo_nrt As Integer, ByVal codigo_etu As Integer, ByVal descripcionInc_stua As String, ByVal comentarioTutor_stua As String, ByVal fechaEjecucion_stua As String, ByVal usuario_reg As Integer)
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

            Dim Data As New Dictionary(Of String, Object)()
            dt = obj.ActualizarSesiónAlumnoIndividual(0, 0, codigo_stua, estado, asistencia, obs, accion_stua, codigo_tat, codigo_tre, problemas_stua, codigo_nrt, codigo_etu, descripcionInc_stua, comentarioTutor_stua, fechaEjecucion_stua, usuario_reg)
            Data.Add("rpta", dt.Rows(0).Item("Respuesta"))
            Data.Add("msje", dt.Rows(0).Item("Mensaje").ToString)
            list.Add(Data)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Data2.Add("rpta", "0 - MOD")
            Data2.Add("msje", ex.Message)
            Data2.Add("usuario_reg", usuario_reg)
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


    Private Sub EliminaSesionAlumno(ByVal cod As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarSesionAlumno(cod)
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
