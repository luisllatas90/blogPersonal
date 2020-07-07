﻿Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class operaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGradosyTitulos
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
                Case "ConsultarFacultad"
                    ListarFacultad("GT", "")
                Case "ConsultarActoAcad"
                    ListarActoAcademico("GYT", "")
                Case "ConsultarEspecialidad"
                    Dim cod_pes As Integer = obj.DecrytedString64(Request("cod"))
                    Dim vigencia As String = Request("vig")
                    ListarEspecialidades("EXP", cod_pes, vigencia, "")
                Case "ConsultarGrado"
                    Dim cod_cpf As Integer = obj.DecrytedString64(Request("cod"))
                    Dim vigencia As String = Request("vig")
                    ListarGrados("GYT", cod_cpf, vigencia)
                Case "ConsultarTipoEstudio"
                    Dim parametro As Integer = Request("param")
                    ListarTipoEstudio("GT", parametro)
                Case "ConsultarPlanEstudio"
                    Dim codigo_Cpf As Integer = obj.DecrytedString64(Request("param"))
                    ListarPlanEstudios(codigo_Cpf)
                Case "ConsultarCarreraProf"
                    Dim parametro As Integer = obj.DecrytedString64(Request("param"))
                    ConsultarCarrerasxTest("GT", parametro)
                Case "ConsultarPersonal"
                    Dim parametro As String = Request("param")
                    ConsultarPersonal("GT", parametro)
                Case "ConsultarCargo"
                    Dim parametro As String = Request("param")
                    ConsultarCargo("GT", parametro)
                Case "ConsultarPrefijo"
                    Dim parametro As String = Request("param")
                    ConsultarPrefijo("GYT", parametro)
                Case "ConsultarAutoridad"
                    Dim param1 As String = obj.DecrytedString64(Request("param1"))
                    Dim param2 As String = obj.DecrytedString64(Request("param2"))
                    Dim param3 As String = Request("param3")
                    ConsultarAutoridad("GYT", param1, param2, param3)
                Case "ConsultarGrupoEgresado"
                    ListarGrupoEgresado("GYT", "")
                Case "ConsultarTipoDenominacion"
                    ConsultarTipoDenominacion("GYT", "")
                Case "ConsultarCargosxTest"
                    Dim codigo_test As Integer = obj.DecrytedString64(Request("param"))
                    ConsultarCargosxTest(codigo_test)
            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
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

    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsGradosyTitulos

            Dim data As New Dictionary(Of String, Object)()
            data.Add("ValSes", obj.EncrytedString64("ValidaSession"))
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("reg", obj.EncrytedString64("Registrar")) ' Registrar
            data.Add("edi", obj.EncrytedString64("Editar")) ' Modificar
            data.Add("mod", obj.EncrytedString64("Modificar")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eliminar")) ' Eliminar
            data.Add("pcc", obj.EncrytedString64("ListaPreEgresadosConConsejo")) ' Listar PreEgresados Con COnsejo por Codigo de S. COnsejo
            data.Add("psc", obj.EncrytedString64("ListaPreEgresadosSinConsejo")) ' Listar PreEgresados Sin COnsejo (Pendientes)
            data.Add("ba", obj.EncrytedString64("BuscarAlumno")) ' Buscar Alumno
            data.Add("ca", obj.EncrytedString64("ConsultarAlumno")) ' Consultar Alumno
            data.Add("caut", obj.EncrytedString64("ConsultarAutoridad"))
            data.Add("mov", obj.EncrytedString64("Mover"))
            data.Add("cer", obj.EncrytedString64("ConsultaExpedientesResolucion"))
            data.Add("acr", obj.EncrytedString64("ActualizarResolucion"))
            data.Add("ceo", obj.EncrytedString64("ConsultaExpedientesOficio"))
            data.Add("aco", obj.EncrytedString64("ActualizarOficio"))
            data.Add("lstden", obj.EncrytedString64("ListarDenominacion")) ' Listar
            data.Add("lee", obj.EncrytedString64("ListaExpedientesEntregar")) ' Lista Diplomas Entregar
            data.Add("edip", obj.EncrytedString64("EntregaDiploma")) ' Actualiza Entrega de Diplomas
            data.Add("adc", obj.EncrytedString64("ActualizarDatosContacto")) ' Actualiza Entrega de Diplomas
            data.Add("lsc", obj.EncrytedString64("ListaSesionCorrelativos")) ' Lista Sesiones Vigentes para generar correlativos
            data.Add("lec", obj.EncrytedString64("ListaEgresadoAsignaCorrelativo")) ' Lista para Asignar correlativos Egresado
            data.Add("ocd", obj.EncrytedString64("ObtenerCorrelativos")) ' Obtener Los siguientes Correlativos Respectivos
            data.Add("gce", obj.EncrytedString64("GenerarCorrelativos")) ' Generar correlativos Egresado
            data.Add("qce", obj.EncrytedString64("QuitarCorrelativos")) ' Quitar correlativos Egresado
            data.Add("leg", obj.EncrytedString64("ListaEgresadoAsignaGrupo")) ' Lista para Asignar correlativos Egresado
            data.Add("age", obj.EncrytedString64("ActualizarGrupo")) ' ACtualizar Grupo de Egresados
            data.Add("cfa", obj.EncrytedString64("ConsultarFechaActo")) ' ConsultaFecha de Acto Academico Original solo para Duplicados
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarFacultad(ByVal tipo As String, ByVal param As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarFacultad(tipo, param)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_fac")))
                data.Add("nombre", dt.Rows(i).Item("nombre_fac"))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarActoAcademico(ByVal tipo As String, ByVal param As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarActoAcademico(tipo, param)

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
    Private Sub ListarGrupoEgresado(ByVal tipo As String, ByVal param As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarGrupoEgresado(tipo, param)

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
    Private Sub ListarEspecialidades(ByVal tipo As String, ByVal cod_pes As Integer, ByVal vigencia As String, ByVal param3 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarEspecialidades(tipo, cod_pes, vigencia, param3)

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

    Private Sub ListarGrados(ByVal tipo As String, ByVal cod_cpf As Integer, ByVal vigencia As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarGrados(tipo, cod_cpf, vigencia)

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

    Private Sub ListarTipoEstudio(ByVal tipo As String, ByVal parametro As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarTipoEstudio(tipo, parametro)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_test")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_test"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarPlanEstudios(ByVal codigo_cpf As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarPlanEstudioxCarrera(codigo_cpf)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_pes")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_pes"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarCarrerasxTest(ByVal tipo As String, ByVal parametro As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarCarrerasxTest(tipo, parametro)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                data.Add("nombre", dt.Rows(i).Item("nombre_cpf"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarPersonal(ByVal tipo As String, ByVal parametro As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPersonal(tipo, parametro)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_per")))
                data.Add("nombre", dt.Rows(i).Item("personal"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarCargo(ByVal tipo As String, ByVal parametro As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarCargo(tipo, parametro)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cgo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_Cgo"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarPrefijo(ByVal tipo As String, ByVal parametro As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPrefijo(tipo, parametro)

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


    Private Sub ConsultarAutoridad(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarAutoridad(tipo, param1, param2, param3)

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

    Private Sub ConsultarTipoDenominacion(ByVal tipo As String, ByVal parametro As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarTipoDenominacion(tipo, parametro)

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


    Private Sub ConsultarCargosxTest(ByVal codigo_Test As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarCargosxTest(codigo_Test)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Cgo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_cgo"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub









    Private Sub ListarTipoDocumento()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoDocumento()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_doci")))
                data.Add("nombre", dt.Rows(i).Item("nombre_doci"))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaRegion(ByVal opcion As String, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaDepartamentos()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Dep")))
                data.Add("nombre", dt.Rows(i).Item("nombre_Dep"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListaProvincia(ByVal opcion As String, ByVal cod_dep As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaProvincias(cod_dep)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Pro")))
                data.Add("nombre", dt.Rows(i).Item("nombre_Pro"))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaDistrito(ByVal opcion As String, ByVal cod_prov As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaDistritos(cod_prov)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Dis")))
                data.Add("nombre", dt.Rows(i).Item("nombre_Dis"))
                list.Add(data)
            Next
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

   
    
End Class
