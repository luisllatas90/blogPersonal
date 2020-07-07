Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class operaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objT As New clsTutoria
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = Request("k")
            Dim f As String = Request("f")

            Select Case Request("action")
                Case "ValidaSession"
                    ValidaSession()
                Case "ope"
                    TiposOperacion()
                Case "CicloAcademico"
                    Dim codigo_ctf As Integer = Session("ctf")
                    If k = "LT" Then f = Session("id_per")
                    ListarCicloAcademico(k, f, codigo_ctf)
                Case "CicloAcademicoTO"
                    k = ""
                    Dim codigo_cpf As String = "0"
                    If Request("k") <> "" Then
                        k = objT.DecrytedString64(Request("k"))
                    Else
                        k = "-1"
                    End If
                    If Request("cboEscuela") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboEscuela"))
                    f = objT.DecrytedString64(Request("f"))
                    PoblacionObjetivo("CI", f, k, codigo_cpf, 0)
               
                Case "PER"
                    ListarPersonal("L", 0)
                Case "POB"
                    Dim codigo_cpf As Integer = 0
                    Dim codigo_cai As Integer = 0
                    Dim riesgo As String = ""
                    k = objT.DecrytedString64(Request("cbocicloAcad"))
                    If Request("cboCategoria") <> "" Then
                        f = objT.DecrytedString64(Request("cboCategoria"))
                    Else
                        f = ""
                    End If
                    If Request("cboEscuela") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboEscuela"))
                    If Request("cboIng") <> "" Then codigo_cai = objT.DecrytedString64(Request("cboIng"))
                    Dim tipo As String = Request("tipo")
                    riesgo = objT.DecrytedString64(Request("cboRiesgo"))
                    ListarPorAsignar("PTO", k, f, codigo_cpf, tipo, codigo_cai, riesgo)
                Case "TUT"
                    Dim codigo_cpf As Integer = 0
                    Dim codigo_cai As Integer = 0
                    Dim codigo_tc As Integer = 0
                    Dim cat As String = ""
                    k = objT.DecrytedString64(Request("cboCicloAcad"))
                    If Request("cboTutor") <> "" Then codigo_tc = objT.DecrytedString64(Request("cboTutor"))
                    If Request("cboCarrera") <> "" Then codigo_cpf = objT.DecrytedString64(Request("cboCarrera"))
                    If Request("cboIng") <> "" Then codigo_cai = objT.DecrytedString64(Request("cboIng"))
                    If Request("cboCategoria") <> "" Then cat = objT.DecrytedString64(Request("cboCategoria"))
                    ListarTutoradosFiltros("TF", k, codigo_tc, 0, codigo_cpf, codigo_cai, cat)
                Case "CAT"
                    ListarCategoria()
                Case "RS"
                    ListarComboRiesgoSeparacion()
                Case "TEVAL"
                    ListaTipoEvaluacion("L", 0)
                Case "VTEVAL"
                    k = objT.DecrytedString64(Request("k"))
                    ListaVariableTipoEvaluacion("L", k)
                Case "TACT"
                    k = Request("k")
                    ListaTipoActividad("L", k)
                Case "TEST"
                    k = Request("k")
                    ListaEstado("L", k)
                Case "TRES"
                    k = Request("k")
                    ListaTipoResultado("L", k)
                Case "TNRI"
                    k = Request("k")
                    ListaNivelRiesgo("L", k)
                Case "TPRO"
                    k = Request("k")
                    ListaTipoProblema("L", k)
                Case "TSES"
                    k = Request("k")
                    f = Request("f")
                    Dim cod_ctf As Integer = Session("ctf")
                    ListaTipoSesion(k, f, cod_ctf)
                Case "AM"
                    f = "0"
                    k = objT.DecrytedString64(Request("k"))
                    If Request("f") <> "0" Then f = objT.DecrytedString64(Request("f"))
                    Dim tipo As String = Request("tipo")
                    Dim codigo_cac As String = objT.DecrytedString64(Request("c"))
                    ListaAsistenciasMoodle(tipo, k, f, codigo_cac)
                Case "NM"
                    f = "0"
                    k = objT.DecrytedString64(Request("k"))
                    If Request("f") <> "0" Then f = objT.DecrytedString64(Request("f"))
                    Dim tipo As String = Request("tipo")
                    Dim codigo_cac As String = objT.DecrytedString64(Request("c"))
                    ListaNotasMoodle(tipo, k, f, codigo_cac)
                Case "CUR"
                    k = objT.DecrytedString64(Request("k"))
                    f = objT.DecrytedString64(Request("f"))
                    ListaCursos("L", k, f)
                Case "AUX"
                    Dim codigo_cpf As Integer = 0
                    Dim codigo_cai As Integer = 0
                    Dim codigo_tc As Integer = 0
                    Dim cat As String = ""
                    Dim tipo As String = Request("tipo")
                    k = objT.DecrytedString64(Request("k"))
                    If Request("f") <> "" Then codigo_tc = objT.DecrytedString64(Request("f"))
                    If Request("codigo_cpf") <> "" Then codigo_cpf = objT.DecrytedString64(Request("codigo_cpf"))
                    If Request("codigo_cai") <> "" Then codigo_cai = objT.DecrytedString64(Request("codigo_cai"))
                    If Request("cat") <> "" Then cat = objT.DecrytedString64(Request("cat"))
                    ListaAuxiliares(tipo, k, codigo_tc, codigo_cpf, codigo_cai, cat)
                Case "ALU"
                    k = objT.DecrytedString64(Request("k"))
                    ListaDatosAlumno(k)
                    'Case "depacad"
                    '    f = CInt(Request("f"))
                    '    ListarDepartamentoAcademico(0, "")
                    'Case "depdoc"
                    '    f = CInt(Request("f"))
                    '    ListarDepartamentoAcademicoDocente("1", k, f)

                    'Case "CCOxPxV"
                    '    Dim tf As String = objT.DecrytedString64(Request("f"))
                    '    ListarCentroCostoPorPermisoPorVisibilidad(tf, objT.DecrytedString64(k))
                    'Case "CCOevent"
                    '    Dim cco As String = objT.DecrytedString64(Request("f"))
                    '    BuscarEvento(k, cco)
                    'Case "moding"
                    '    Dim modu As String = objT.DecrytedString64(Request("mod"))
                    '    ListarModalidadIngreso("7", modu)
                    'Case "ubi"
            End Select

            'Data.Add("aq", "-LOAD")
        Catch ex As Exception
            'Data.Add("tipo", Request("tipo"))
            'Data.Add("codigo_cac", objT.DecrytedString64(Request("k")))
            'Data.Add("codigo_tc", objT.DecrytedString64(Request("f")))
            'Data.Add("codigo_cpf", objT.DecrytedString64(Request("codigo_cpf")))
            'Data.Add("codigo_cai", objT.DecrytedString64(Request("codigo_cpf")))
            'Data.Add("cat", objT.DecrytedString64(Request("cat")))
            Data.Add("msje", ex.Message & "-LOAD")
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try

    End Sub


    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New clsTutoria

            Dim data As New Dictionary(Of String, Object)()
            data.Add("ValSes", obj.EncrytedString64("ValidaSession"))
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("reg", obj.EncrytedString64("Registrar")) ' Registrar
            data.Add("edi", obj.EncrytedString64("Editar")) ' Modificar
            data.Add("mod", obj.EncrytedString64("Modificar")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eliminar")) ' Eliminar
            data.Add("btnd", obj.EncrytedString64("BuscaxTipoyNumDoc")) ' Busqueda por tipo y num de docuemnto interesado
            data.Add("bcon", obj.EncrytedString64("BuscaCoincidencia")) ' Busqueda por apellidos y nombres de interesado
            data.Add("scon", obj.EncrytedString64("SeleccionCoincidencia"))
            data.Add("esc", obj.EncrytedString64("ListarEscuela"))
            data.Add("cacto", obj.EncrytedString64("CicloAcademicoTO"))
            data.Add("lstT", obj.EncrytedString64("ListarTotales"))
            data.Add("ind", obj.EncrytedString64("Individual"))
            data.Add("atT", obj.EncrytedString64("AtenderTutorado")) ' Parametro que sirve de operacion para atender a tutorado

            'data.Add("lstFl", obj.EncrytedString64("ListarFiles")) ' Listar Archivos
            'data.Add("upl", obj.EncrytedString64("Upload")) ' Subir Archivos 
            'data.Add("dwl", obj.EncrytedString64("Download")) ' Descargar Archivos

            'Dim dt As New DataTable
            'dt = obj.ListaTipoEstudio("TO", 0)

            'For i As Integer = 0 To dt.Rows.Count - 1
            '    data.Add(dt.Rows(i).Item("abreviatura").ToString.ToLower, obj.EncrytedString64(dt.Rows(i).Item("codigo_test").ToString)) ' Tipos de Estudio
            'Next
            'dt = Nothing

            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    

    Private Sub ListarCicloAcademico(ByVal opcion As String, ByVal codigo As String, ByVal codigo_ctf As Integer)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaCicloAcademico(opcion, codigo, codigo_ctf)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                data.Add("ctf", codigo_ctf)
                data.Add("t", obj.EncrytedString64(dt.Rows(i).Item("codigo_tc")))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListarCicloAcademicoTO(ByVal opcion As String, ByVal codigo As String, ByVal codigo_ctf As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim data2 As New Dictionary(Of String, Object)()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaCicloAcademico(opcion, codigo, codigo_ctf)
        Try
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cac")))
                    data.Add("nombre", dt.Rows(i).Item("descripcion_cac"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            data2.Add("msje", ex.Message)
            list.Add(data2)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub PoblacionObjetivo(ByVal opcion As String, ByVal codigo_cac As String, ByVal categoria As String, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try

            Dim ejm As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New clsTutoria
            Dim dt As New Data.DataTable
            Dim count As String = "0 alumnos encontrados"
            dt = obj.PoblacionObjetivo(opcion, codigo_cac, categoria, codigo_cpf, codigo_cai, "")

            If dt.Rows.Count > 0 Then

                ejm.Add("cCant", CStr(dt.Rows(0).Item("cant")) & " alumnos encontrados")
                list.Add(ejm)
                If opcion = "CI" Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim data As New Dictionary(Of String, Object)()
                        data.Add("cCod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cac")))
                        data.Add("cNombre", dt.Rows(i).Item("descripcion_cac"))
                        list.Add(data)
                    Next

                End If

            End If

            'ejm.Add("ciclo", codigo_cac)
            'ejm.Add("categoria", categoria)
            'ejm.Add("codigo_cpf", codigo_cpf)
            'ejm.Add("codigo_cai", codigo_cai)
            '
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message & "-PORASIG")
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub
   


    Private Sub ListarPersonal(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaPersonal(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))
                data.Add("estado", dt.Rows(i).Item("estado_per"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Private Sub ListaTipoEvaluacion(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEvaluacion(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_te")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_te"))
                data.Add("ope", dt.Rows(i).Item("Operacion"))
                data.Add("usu", dt.Rows(i).Item("usuario"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Private Sub ListaVariableTipoEvaluacion(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaVariableTipoEvaluacion(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("CODIGO_VT")))
                data.Add("nombre", dt.Rows(i).Item("DESCRIPCION_VE"))
                data.Add("codV", obj.EncrytedString64(dt.Rows(i).Item("CODIGO_VE")))
                data.Add("tvar", dt.Rows(i).Item("tipo_var"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Private Sub ListaTipoSesion(ByVal opcion As String, ByVal codigo As String, ByVal codigo_ctf As Integer)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoSesion(opcion, codigo, codigo_ctf)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_tis")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_tis"))
                data.Add("opc", dt.Rows(i).Item("opcion"))

                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Private Sub ListaCursos(ByVal opcion As String, ByVal codigo As String, ByVal codigo_cpf As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaCursos(opcion, codigo, codigo_cpf)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cur")))
                data.Add("nombre", dt.Rows(i).Item("nombre_cur"))
                data.Add("carr", dt.Rows(i).Item("nombre_cpf"))

                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub
    Private Sub ListaAuxiliares(ByVal tipo As String, ByVal codigo_cac As Integer, ByVal codigo_tc As Integer, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal categoria As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaAux(tipo, codigo_cac, codigo_tc, codigo_cpf, codigo_cai, categoria)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("nombre"))

                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Private Sub ListarCategoria()

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable

        dt = obj.PoblacionObjetivo("CAT", 0, "", 0, 0, "")
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cat")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_cat"))

                list.Add(data)
            Next

        End If

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Private Sub ListarComboRiesgoSeparacion()

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria

        Dim data, data2, data3, data4 As New Dictionary(Of String, Object)()
        data.Add("cod", obj.EncrytedString64(""))
        data.Add("nombre", "TODOS")
        list.Add(data)
        data2.Add("cod", obj.EncrytedString64("4"))
        data2.Add("nombre", "CON RIESGO DE SEPARACIÓN (4to ciclo)")
        list.Add(data2)
        data3.Add("cod", obj.EncrytedString64("100"))
        data3.Add("nombre", "CON RIESGO DE SEPARACIÓN")
        list.Add(data3)
        data4.Add("cod", obj.EncrytedString64("0"))
        data4.Add("nombre", "SIN RIESGO DE SEPARACIÓN")
        list.Add(data4)

        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)

    End Sub

    Private Sub ListarPorAsignar(ByVal opcion As String, ByVal codigo_cac As String, ByVal categoria As String, ByVal codigo_cpf As Integer, ByVal tipo As String, ByVal codigo_cai As Integer, ByVal riesgo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""

        Try
            Dim ejm As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New clsTutoria
            Dim dt As New Data.DataTable
            dt = obj.PoblacionObjetivo(opcion, codigo_cac, categoria, codigo_cpf, codigo_cai, riesgo)

            If tipo = "1" Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_pso")))
                    data.Add("alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                    data.Add("cpf", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                    data.Add("nombre", dt.Rows(i).Item("alumno"))
                    data.Add("carrera", dt.Rows(i).Item("nombre_Cpf"))
                    data.Add("categoria", dt.Rows(i).Item("categoria"))
                    data.Add("codU", dt.Rows(i).Item("codigoUniver_Alu"))
                    data.Add("msje", CStr(dt.Rows.Count) & " alumnos encontrados")
                    list.Add(data)
                Next
            End If

            If tipo = "2" Then
                Dim data As New Dictionary(Of String, Object)()
                data.Add("codigo_cai", codigo_cai)
                data.Add("codigo_cpf", codigo_cpf)
                data.Add("categoria", categoria)
                data.Add("msje", CStr(dt.Rows.Count) & " alumnos encontrados")
                list.Add(data)
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
    Private Sub ListarTutoradosFiltros(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_cac As Integer, ByVal tipo_ses As Integer, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal categoria As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try

            Dim ejm As New Dictionary(Of String, Object)()
            Dim list As New List(Of Dictionary(Of String, Object))()
            Dim obj As New clsTutoria
            Dim dt As New Data.DataTable
            dt = obj.ListaTutorados(tipo, codigo, codigo_cac, codigo_cpf, tipo_ses, codigo_cpf, codigo_cai, categoria)

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    data.Add("cTA", obj.EncrytedString64(dt.Rows(i).Item("codigo_tua")))
                    data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_pso")))
                    data.Add("alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                    data.Add("cpf", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                    data.Add("nombre", dt.Rows(i).Item("alumno"))
                    data.Add("carrera", dt.Rows(i).Item("nombre_Cpf"))
                    data.Add("abrev", dt.Rows(i).Item("abreviatura_Cpf"))
                    data.Add("categoria", dt.Rows(i).Item("categoria"))
                    data.Add("codU", dt.Rows(i).Item("codigoUniver_Alu"))
                    data.Add("rg", dt.Rows(i).Item("descripcion_nrt"))
                    list.Add(data)
                Next

            End If

            'ejm.Add("opc", "obj.ListaTutorados(" & tipo & "," & codigo.ToString & "," & codigo_cac.ToString & ",0," & codigo_cpf.ToString & "," & codigo_cai.ToString & "," & categoria & ")")
            'list.Add(ejm)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("sp", "obj.ListaTutorados(" & tipo & "," & codigo.ToString & "," & codigo_cac.ToString & ",0," & codigo_cpf.ToString & "," & codigo_cai.ToString & "," & categoria & ")")
            data.Add("msje", ex.Message & "-PORASIG")
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)
        End Try
    End Sub


    Private Sub ValidaSession()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim data As New Dictionary(Of String, Object)()
        If Session("id_per") <> "" Then
            data.Add("msje", True)
            data.Add("link", "")
        Else
            data.Add("msje", False)
            data.Add("link", "../../../sinacceso.html")
        End If
        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaTipoActividad(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoActividad(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_tat")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_tat"))
                data.Add("est", dt.Rows(i).Item("estado_tat"))
                data.Add("pres", dt.Rows(i).Item("presencial_tat"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
  
    Private Sub ListaEstado(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaEstado(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_etu")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_etu"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaTipoResultado(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoResultado(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_tre")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_tre"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaNivelRiesgo(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaNivelRiesgo(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_nrt")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_nrt"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaTipoProblema(ByVal opcion As String, ByVal codigo As String)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoProblema(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_tpr")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_tpr"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaAsistenciasMoodle(ByVal opcion As String, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer, ByVal codigo_cac As Integer)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaAsistenciasMoodle(opcion, codigo_tua, codigo_tc, codigo_cac)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("nombre", dt.Rows(i).Item("nombre_Cur"))
                data.Add("docente", dt.Rows(i).Item("docente"))
                data.Add("p", dt.Rows(i).Item("P"))
                data.Add("f", dt.Rows(i).Item("F"))
                data.Add("t", dt.Rows(i).Item("Total"))
                data.Add("por", dt.Rows(i).Item("porcentaje"))
                data.Add("sem", dt.Rows(i).Item("semaforo"))
                data.Add("v", dt.Rows(i).Item("veces"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaNotasMoodle(ByVal opcion As String, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer, ByVal codigo_cac As Integer)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaNotasMoodle(opcion, codigo_tua, codigo_tc, codigo_cac)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("nombre", dt.Rows(i).Item("nombre_Cur"))
                data.Add("docente", dt.Rows(i).Item("docente"))
                data.Add("a", dt.Rows(i).Item("A"))
                data.Add("d", dt.Rows(i).Item("D"))
                data.Add("t", dt.Rows(i).Item("Total"))
                data.Add("por", dt.Rows(i).Item("porcentaje"))
                data.Add("sem", dt.Rows(i).Item("semaforo"))
                data.Add("v", dt.Rows(i).Item("veces"))
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaDatosAlumno(ByVal codigo_alu As Integer)

        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New clsTutoria
        Dim dt As New Data.DataTable
        dt = obj.ListaDatosAlumno(codigo_alu)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                Dim foto As String
                Dim obEnc As Object
                obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
                foto = obEnc.CodificaWeb("069" & dt.Rows(i).Item("codigoUniver_Alu"))
                obEnc = Nothing

                data.Add("cod", dt.Rows(i).Item("codigo_Alu"))
                data.Add("codU", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("nombre", dt.Rows(i).Item("ApellidosNombres"))
                data.Add("si", dt.Rows(i).Item("SemIngreso"))
                data.Add("pe", dt.Rows(i).Item("PlanEstudio"))
                data.Add("esc", dt.Rows(i).Item("EscuelaProfesional"))
                data.Add("est", dt.Rows(i).Item("estadoActual_Alu"))
                data.Add("nac", dt.Rows(i).Item("fechaNacimiento_Alu"))
                data.Add("s", dt.Rows(i).Item("sexo_Alu"))
                data.Add("ti", dt.Rows(i).Item("tipoDocIdent_Alu"))
                data.Add("ni", dt.Rows(i).Item("nroDocIdent_Alu"))
                data.Add("em", dt.Rows(i).Item("eMail_Alu"))
                data.Add("em2", dt.Rows(i).Item("email2_Alu"))
                data.Add("ec", dt.Rows(i).Item("estadoCivil_Dal"))
                data.Add("dir", dt.Rows(i).Item("direccion_Dal"))
                data.Add("urb", dt.Rows(i).Item("urbanizacion_Dal"))
                data.Add("dis", dt.Rows(i).Item("nombreDis_Dal"))
                data.Add("pro", dt.Rows(i).Item("nombrePro_Dal"))
                data.Add("tc", dt.Rows(i).Item("telefonoCasa_Dal"))
                data.Add("tm", dt.Rows(i).Item("telefonoMovil_Dal"))
                data.Add("reg", dt.Rows(i).Item("religion_Dal"))
                data.Add("min", dt.Rows(i).Item("nombre_Min"))
                data.Add("estd", dt.Rows(i).Item("estadoDeuda_Alu"))
                data.Add("r", dt.Rows(i).Item("retirado"))
                data.Add("f", "//intranet.usat.edu.pe/imgestudiantes/" & foto)
                list.Add(data)
            Next

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    'Private Sub ListarCicloAcademico(ByVal tipo As String, ByVal param As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()

    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos
    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.ConsultarCicloAcademico", tipo, param)
    '        obj.CerrarConexion()

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()
    '                data.Add("cCiclo", tb.Rows(i).Item("codigo_Cac"))
    '                data.Add("nCiclo", tb.Rows(i).Item("descripcion_Cac"))
    '                list.Add(data)
    '            Next
    '        End If
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub ListarDepartamentoAcademico(ByVal cod As Integer, ByVal param As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()

    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos
    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.ACAD_BuscaDepartamentoAcademico", cod, param)
    '        obj.CerrarConexion()

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()
    '                data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
    '                data.Add("nDac", tb.Rows(i).Item("nombre_Dac"))
    '                data.Add("abrDac", tb.Rows(i).Item("nombre_Dac"))
    '                list.Add(data)
    '            Next
    '        End If
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub ListarDepartamentoAcademicoDocente(ByVal tipo As String, ByVal k As Integer, ByVal f As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()

    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos
    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.ACAD_DocenteporDepAcademico", tipo, k, f)
    '        obj.CerrarConexion()

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()
    '                data.Add("cDac", tb.Rows(i).Item("codigo_Dac"))
    '                data.Add("cPer", tb.Rows(i).Item("codigo_Per"))
    '                data.Add("nPer", tb.Rows(i).Item("Personal"))
    '                list.Add(data)
    '            Next
    '        End If
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub ListarUbigeo(ByVal tipo As String, ByVal modulo As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()
    '        Dim objT As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarInformacionParaEvento", tipo, modulo, 0, 0)
    '        obj.CerrarConexion()

    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Dim data As New Dictionary(Of String, Object)()
    '            data.Add("cod", objT.EncrytedString64(tb.Rows(i).Item("codigo_min")))
    '            data.Add("nombre", tb.Rows(i).Item("nombre_min"))
    '            list.Add(data)
    '        Next
    '        objT = Nothing
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""
    '        Dim list As New List(Of Dictionary(Of String, Object))()


    '        Dim data As New Dictionary(Of String, Object)()
    '        data.Add("error", ex.Message)
    '        list.Add(data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub


    'Private Sub ListarModalidadIngreso(ByVal tipo As String, ByVal modulo As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()
    '        Dim objT As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarInformacionParaEvento", tipo, modulo, 0, 0)
    '        obj.CerrarConexion()

    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Dim data As New Dictionary(Of String, Object)()
    '            data.Add("cod", objT.EncrytedString64(tb.Rows(i).Item("codigo_min")))
    '            data.Add("nombre", tb.Rows(i).Item("nombre_min"))
    '            list.Add(data)
    '        Next
    '        objT = Nothing
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""
    '        Dim list As New List(Of Dictionary(Of String, Object))()


    '        Dim data As New Dictionary(Of String, Object)()
    '        data.Add("error", ex.Message)
    '        list.Add(data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    'Private Sub BuscarEvento(ByVal tipo As String, ByVal cco As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()
    '        Dim objT As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarEventos", tipo, cco, 0)
    '        obj.CerrarConexion()

    '        Dim data As New Dictionary(Of String, Object)()
    '        'data.Add("cod", objT.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
    '        data.Add("nombre", tb.Rows(0).Item("nombre_dev"))
    '        data.Add("nroresolucion", tb.Rows(0).Item("nroresolucion_dev"))
    '        data.Add("coordinador", tb.Rows(0).Item("coordinador"))
    '        data.Add("apoyo", tb.Rows(0).Item("apoyo"))
    '        data.Add("participantes", tb.Rows(0).Item("nroparticipantes_dev"))
    '        data.Add("preunitcont", tb.Rows(0).Item("preciounitcontado_dev"))
    '        data.Add("preunifin", tb.Rows(0).Item("preciounitfinanciado_dev"))
    '        data.Add("montoinicial", tb.Rows(0).Item("montocuotainicial_dev"))
    '        data.Add("cuotas", tb.Rows(0).Item("nrocuotas_dev"))
    '        data.Add("porcdctoper", tb.Rows(0).Item("porcentajedescpersonalusat_dev"))
    '        data.Add("porcdctoalu", tb.Rows(0).Item("porcentajedescalumnousat_dev"))
    '        data.Add("porcdctocorp", tb.Rows(0).Item("porcentajedesccorportativo_dev"))
    '        data.Add("porcdctoegr", tb.Rows(0).Item("porcentajedescegresado_dev"))
    '        If tb.Rows(0).Item("gestionanotas_dev") = True Then
    '            data.Add("gestion", "Si")
    '        Else
    '            data.Add("gestion", "No")
    '        End If

    '        data.Add("horario", tb.Rows(0).Item("horarios_dev"))
    '        data.Add("obs", tb.Rows(0).Item("obs_dev"))
    '        data.Add("feciniprop", CDate(tb.Rows(0).Item("fechainiciopropuesta_dev")).ToShortDateString)
    '        data.Add("fecfinprop", CDate(tb.Rows(0).Item("fechafinpropuesta_dev")).ToShortDateString)
    '        data.Add("escuela", tb.Rows(0).Item("nombre_cpf"))
    '        list.Add(data)

    '        objT = Nothing
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""
    '        Dim list As New List(Of Dictionary(Of String, Object))()


    '        Dim data As New Dictionary(Of String, Object)()
    '        data.Add("error", ex.Message)
    '        list.Add(data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub

    'Private Sub ListarCentroCostoPorPermisoPorVisibilidad(ByVal tipo As String, ByVal param As String)
    '    Try
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""

    '        Dim list As New List(Of Dictionary(Of String, Object))()
    '        Dim objT As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos


    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarCentroCostosXPermisosXVisibilidad", tipo, Session("id_per"), "", param, 1)
    '        obj.CerrarConexion()

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()
    '                data.Add("cod", objT.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
    '                data.Add("nombre", tb.Rows(i).Item("Nombre"))
    '                list.Add(data)
    '            Next
    '        End If

    '        objT = Nothing
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)

    '    Catch ex As Exception
    '        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
    '        Dim JSONresult As String = ""
    '        Dim list As New List(Of Dictionary(Of String, Object))()


    '        Dim data As New Dictionary(Of String, Object)()
    '        data.Add("error", ex.Message)
    '        list.Add(data)
    '        JSONresult = serializer.Serialize(list)
    '        Response.Write(JSONresult)
    '    End Try
    'End Sub
End Class
