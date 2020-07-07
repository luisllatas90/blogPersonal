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
                    Dim tipo As String
                    Dim tipo_ses As Integer = 0
                    Dim codigo_cpf As Integer = 0
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcad"))
                    If Request("cboTipo") <> "" Then tipo_ses = objT.DecrytedString64(Request("cboTipo"))
                    If Request("cboCarrera") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboCarrera"))
                    If Request("cboCarreraP") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboCarreraP")) ' soluciona el problema de mcubas q no filtra por carrera
                    tipo = Request("tipo")
                    If tipo = "LT" Then
                        cod = objT.DecrytedString64(Request("hdcod"))
                    Else
                        cod = Session("id_per")
                    End If

                    Dim codigo_ctf As Integer = Session("ctf")
                    ListaTutorados(tipo, cod, codigo_cac, codigo_ctf, tipo_ses, codigo_cpf)
                Case "Registrar"

                    k = objT.DecrytedString64(Request("hdcod"))
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer = 1
                    If Request("tipo") = 1 Then
                        Dim codigo_alu As Integer = objT.DecrytedString64(Request("cod"))
                        Dim categoria As String = Request("cat")
                        RegistrarTutorAlumno(k, codigo_alu, estado, cod_per, categoria)
                    ElseIf Request("tipo") = 2 Then
                        Dim categoria As String = "-1"
                        Dim codigo_cpf As Integer = 0
                        Dim codigo_cai As Integer = 0
                        Dim riesgo As String = ""
                        f = objT.DecrytedString64(Request("cbocicloAcad"))
                        If Request("cboCategoria") <> "" Then categoria = objT.DecrytedString64(Request("cboCategoria"))
                        If Request("cboEscuela") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboEscuela"))
                        If Request("cboIng") <> "" Then codigo_cai = objT.DecrytedString64(Request("cboIng"))
                        If Request("cboRiesgo") <> "" Then riesgo = objT.DecrytedString64(Request("cboRiesgo"))
                        RegistrarTutorAlumnoFiltros(k, cod_per, f, categoria, codigo_cpf, codigo_cai, riesgo)
                    End If
                Case "Editar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    'ListaTutores("E", 0, k)
                Case "Modificar"
                    k = objT.DecrytedString64(Request("hdcod"))
                    Dim codigo_cac As Integer = objT.DecrytedString64(Request("cboCicloAcadM"))
                    Dim codigo_per As Integer = objT.DecrytedString64(Request("cboPersonal"))
                    Dim fecini As String = Request("txtfecini")
                    Dim fecfin As String = Request("txtfecfin")
                    Dim cod_per As Integer = Session("id_per")
                    Dim estado As Integer
                    If Request("chkestado") = "" Then
                        estado = 0
                    Else
                        estado = 1
                    End If

                    'RegistrarTutor(k, codigo_cac, codigo_per, fecini, fecfin, estado, cod_per)
                Case "Eliminar"
                    k = objT.DecrytedString64(Request("cod"))
                    'k = objT.DecrytedString64(Request("cod"))
                    Dim tipo As Integer = Request("tipo")
                    If tipo = 1 Then EliminaTutorAlumno(k)
                Case "AtenderTutorado"
                    k = objT.DecrytedString64(Request("c"))
                    f = CInt(Request("est"))
                    AtenderTutorAlumno(k, f)
                Case "ListarEscuela"
                    'ListaTutores("ESC", 0, 0)

            End Select


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

    Private Sub ListaTutorados(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_cac As Integer, ByVal codigo_ctf As Integer, Optional ByVal tipo_ses As Integer = 0, Optional ByVal codigo_cpf As Integer = 0)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim data2 As New Dictionary(Of String, Object)()
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim obj As New clsTutoria
            Dim tb As New Data.DataTable
            'Dim cn As New clsaccesodatos
            tb = obj.ListaTutorados(tipo, CInt(codigo), CInt(codigo_cac), codigo_ctf, tipo_ses, codigo_cpf)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()

                    'If i = 0 Then data.Add("sw", True)
                    'If tipo = "A" Then
                    data.Add("cTA", obj.EncrytedString64(tb.Rows(i).Item("codigo_tua")))
                    data.Add("cCac", obj.EncrytedString64(tb.Rows(i).Item("codigo_Cac")))
                    data.Add("cCaI", obj.EncrytedString64(tb.Rows(i).Item("codigo_CaI")))
                    data.Add("cAlu", obj.EncrytedString64(tb.Rows(i).Item("codigo_Alu")))
                    data.Add("cCpf", obj.EncrytedString64(tb.Rows(i).Item("codigo_cpf")))
                    data.Add("cEva", iif(tb.Rows(i).Item("codigo_eva") = 0, "0", obj.EncrytedString64(tb.Rows(i).Item("codigo_eva"))))

                    data.Add("cCodU", tb.Rows(i).Item("codigoUniver_Alu"))
                    data.Add("cAlumno", tb.Rows(i).Item("alumno"))
                    data.Add("cCat", tb.Rows(i).Item("categoria"))
                    data.Add("cAbrev", tb.Rows(i).Item("abreviatura_Cpf"))
                    data.Add("cCarrera", tb.Rows(i).Item("nombre_Cpf"))


                    data.Add("cAtendido", tb.Rows(i).Item("atendido"))




                    If tipo = "LP" Then
                        data.Add("cAsistM", tb.Rows(i).Item("asistencias"))
                        data.Add("cNotasM", tb.Rows(i).Item("notas"))
                        data.Add("cSes", tb.Rows(i).Item("sesiones"))
                        data.Add("cTC", obj.EncrytedString64(tb.Rows(i).Item("codigo_tc")))
                        data.Add("cTutor", tb.Rows(i).Item("tutor"))
                        data.Add("cREval", tb.Rows(i).Item("riesgoEval"))
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

            'data2.Add("CODIGO", codigo)

            'list.Add(data2)

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data2.Add("tipo", tipo)
            data2.Add("codigo", codigo)
            data2.Add("codigo_cac", codigo_cac)
            data2.Add("rpta", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub RegistrarTutorAlumno(ByVal cod As Integer, ByVal codigo_alu As Integer, ByVal estado As Integer, ByVal user_reg As Integer, ByVal cat As String)
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
            dt = obj.RegistrarTutorAlumno(cod, codigo_alu, estado, user_reg, cat)
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
    Private Sub RegistrarTutorAlumnoFiltros(ByVal cod As Integer, ByVal user_reg As Integer, ByVal codigo_cac As Integer, ByVal cat As String, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal riesgo As String)
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
            dt = obj.RegistrarTutorAlumnoFiltros(cod, user_reg, codigo_cac, cat, codigo_cpf, codigo_cai, riesgo)
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

    Private Sub EliminaTutorAlumno(ByVal cod As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            dt = obj.EliminarTutorAlumno(cod)
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cod">15/05/2018 EPENA</param>
    ''' <param name="estado">Click en Checkbox para atender a tutorado</param>
    ''' <remarks></remarks>

    Private Sub AtenderTutorAlumno(ByVal cod As Integer, ByVal estado As Integer)
        Dim obj As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try

            Dim dt As New Data.DataTable
            Dim est As Boolean
            If estado = 1 Then est = True Else est = False

            dt = obj.AtenderTutorAlumno(cod, est)
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
