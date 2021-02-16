Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data

Partial Class operaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCRM As New ClsCRM
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
                Case "TipoEstudio"
                    ListarTipoEstudio(k, 0, f)
                Case "CicloAcademico"
                    ListarCicloAcademico(k, f)

                    '//root:1, k:"C", f:'R'
                Case "Convocatoria"
                    If f <> "%" And f <> "L" And f <> "R" Then
                        f = objCRM.DecrytedString64(Request("f"))
                    End If

                    ListarConvocatoria(k, 0, f)

                Case "CentroCosto"
                    Dim ecrCodigoTest As String = Request("codigoTest")
                    Dim codigoTest As Integer = -2

                    If Not String.IsNullOrEmpty(ecrCodigoTest) Then
                        codigoTest = objCRM.DecrytedString64(ecrCodigoTest)
                    End If

                    If codigoTest = 2 Then
                        codigoTest = 1 'ESCUELA PRE, El tipo de estudios para Inscripción Pregrado es Escuela Pre
                    End If
                    ListarCentroCosto(codigoTest)

                Case "Evento"
                    If f <> "%" Then
                        f = objCRM.DecrytedString64(Request("f"))
                    End If
                    Dim cod_con As String = Request("cod_con")
                    If cod_con <> "%" Then
                        cod_con = objCRM.DecrytedString64(Request("cod_con"))
                    End If
                    ListarEvento(k, 0, f, cod_con)
                Case "TipoDocumento"
                    ListarTipoDocumento()
                Case "Region"
                    ListaRegion(k, f)
                Case "Provincia"
                    ListaProvincia(k, objCRM.DecrytedString64(f))
                Case "Distrito"
                    ListaDistrito(k, objCRM.DecrytedString64(f))
                Case "ActividadPOA"
                    Call ListarActividadPOA(k, 0, f)
                Case "Motivo"
                    ListaMotivo(k, 0)
                Case "TipoComunicacion"
                    ListaTipoComunicacion(k, 0)
                Case "CarreraProfesional"
                    If k <> "0" And k <> "" Then
                        k = objCRM.DecrytedString64(k)
                    End If
                    If f <> "0" And f <> "" Then
                        f = objCRM.DecrytedString64(f)
                    End If
                    ListaCarreraProfesional(Request("tipo"), k, f)
                Case "SituacionInteresado"
                    Dim h As String = Request("h")
                    If f <> "0" And h <> "" Then
                        h = objCRM.DecrytedString64(h)
                    End If
                    SituacionInteresado(k, f, h)
                Case "EstadoComunicacion"
                    Dim g As String = ""
                    ListaEstadoComunicacion(k, f, g)
                    '
                    'Case "depacad"
                    '    f = CInt(Request("f"))
                    '    ListarDepartamentoAcademico(0, "")
                    'Case "depdoc"
                    '    f = CInt(Request("f"))
                    '    ListarDepartamentoAcademicoDocente("1", k, f)

                    'Case "CCOxPxV"
                    '    Dim tf As String = objCRM.DecrytedString64(Request("f"))
                    '    ListarCentroCostoPorPermisoPorVisibilidad(tf, objCRM.DecrytedString64(k))
                    'Case "CCOevent"
                    '    Dim cco As String = objCRM.DecrytedString64(Request("f"))
                    '    BuscarEvento(k, cco)
                    'Case "moding"
                    '    Dim modu As String = objCRM.DecrytedString64(Request("mod"))
                    '    ListarModalidadIngreso("7", modu)
                    'Case "ubi"
                Case "Origen"
                    ListarOrigen()
                Case "InstitucionEducativa"
                    Dim tipo As String = Request("tipo")
                    Dim codigo As Integer = Request("codigo")
                    Dim soloSecundaria As Boolean = Request("soloSecundaria")
                    ListaInstitucionEducativa(tipo, codigo, soloSecundaria)
                Case "InstitucionEducativaPorConvocatoria"
                    Dim tipo As String = Request("tipo")
                    Dim codigo As Integer = Request("codigo")
                    Dim soloSecundaria As Boolean = Request("soloSecundaria")
                    Dim codigoCon As Integer = objCRM.DecrytedString64(Request("codigoCon"))
                    ConsultarInstitucionEducativaInteresadoPorConvocatoria(tipo, codigo, soloSecundaria, codigoCon)
                Case "RequisitoAdmision"
                    Dim codigoTest As String = "0"
                    Dim codigoMin As String = "0"

                    If Not String.IsNullOrEmpty(Request("codigoTest")) Then
                        codigoTest = objCRM.DecrytedString64(Request("codigoTest"))
                        If codigoTest = "2" Then
                            codigoTest = "1" 'ESCUELA PRE -> PREGRADO
                        End If
                    End If
                    If Not String.IsNullOrEmpty(Request("codigoMin")) Then
                        codigoMin = objCRM.DecrytedString64(Request("codigoMin"))
                    End If
                    ListarRequisitosAdmision(codigoTest, codigoMin)
                Case "GradosPorTipoEstudio"
                    Dim codigoTest As String = objCRM.DecrytedString64(Request("codigoTest"))
                    ListarGradosPorTipoEstudio(codigoTest)
            End Select

        Catch ex As Exception
            Data.Add("msje", ex.Message)
            JSONresult = serializer.Serialize(Data)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub TiposOperacion()
        Try
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim obj As New ClsCRM

            Dim data As New Dictionary(Of String, Object)()
            data.Add("ValSes", obj.EncrytedString64("ValidaSession"))
            data.Add("lst", obj.EncrytedString64("Listar")) ' Listar
            data.Add("reg", obj.EncrytedString64("Registrar")) ' Registrar
            data.Add("edi", obj.EncrytedString64("Editar")) ' Modificar
            data.Add("mod", obj.EncrytedString64("Modificar")) ' Modificar
            data.Add("eli", obj.EncrytedString64("Eliminar")) ' Eliminar
            data.Add("btnd", obj.EncrytedString64("BuscaxTipoyNumDoc")) ' Busqueda por tipo y num de docuemnto interesado
            data.Add("bcon", obj.EncrytedString64("BuscaCoincidencia")) ' Busqueda por apellidos y nombres de interesado
            data.Add("scon", obj.EncrytedString64("SeleccionCoincidencia")) 'Seleccionar Coincidencia
            data.Add("bie", obj.EncrytedString64("BuscaInstitucionEducativa")) 'Buscar IE por Codigo departamento
            data.Add("vdup", obj.EncrytedString64("ValidaDuplicado")) 'Verifica que no este registrado por tipodoc y numdoc
            data.Add("ins", obj.EncrytedString64("InscribirInteresado")) ' Inscribi Interesado en Evento
            data.Add("pint", obj.EncrytedString64("PerfilInteresado"))
            data.Add("Idsint", obj.EncrytedString64("IdSessionInteresado"))
            data.Add("cfi", obj.EncrytedString64("CargaFiltros"))
            data.Add("lpr", obj.EncrytedString64("ListaPrioridad"))
            data.Add("lrp", obj.EncrytedString64("ListaRespuestaPregunta"))
            data.Add("rxp", obj.EncrytedString64("RespuestasxPregunta"))
            data.Add("mrp", obj.EncrytedString64("MoverRespuestaDePregunta"))
            data.Add("test", obj.EncrytedString64("TipoEstudio"))
            data.Add("conv", obj.EncrytedString64("Convocatoria"))
            data.Add("ori", obj.EncrytedString64("Origen")) 'Origen_CRM
            data.Add("reecom", obj.EncrytedString64("ReenviarComunicacion")) 'Reenvia una comunicación programada
            data.Add("lstxint", obj.EncrytedString64("ListarPorInteresado"))
            data.Add("vldem", obj.EncrytedString64("ValidarEmail"))
            data.Add("mrk", obj.EncrytedString64("ProcesoMarketing"))
            data.Add("reqing", obj.EncrytedString64("RequisitosIngresante"))
            data.Add("exp", obj.EncrytedString64("ExportarInteresados"))
            data.Add("dcp", obj.EncrytedString64("DatosCarreraProfesional"))
            data.Add("rqa", obj.EncrytedString64("RequisitoAdmision"))
            JSONresult = serializer.Serialize(data)
            Response.Write(JSONresult)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ListarTipoEstudio(ByVal opcion As String, ByVal codigo As Integer, ByVal f As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoEstudio(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_test")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_test"))
                list.Add(data)
            Next
            If f = 1 Then
                Dim data1 As New Dictionary(Of String, Object)()
                data1.Add("cod", obj.EncrytedString64("T"))
                data1.Add("nombre", "TODOS")
                list.Add(data1)
            End If
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarCicloAcademico(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaCicloAcademico(opcion, codigo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarConvocatoria(ByVal opcion As String, ByVal codigo As String, ByVal f As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        If f = "L" Or f = "R" Then
            dt = obj.ListaConvocatorias(opcion, codigo, "%")
        Else
            dt = obj.ListaConvocatorias(opcion, codigo, f)
        End If

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                list.Add(data)
            Next
            If f = "L" Or f <> "R" Then
                Dim data1 As New Dictionary(Of String, Object)()
                data1.Add("cod", obj.EncrytedString64("T"))
                data1.Add("nombre", "TODOS")
                list.Add(data1)
            End If

        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarCentroCosto(ByVal codigoTest As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        Dim codigoPer As Integer = Session("id_per")
        dt = obj.ListarCentroCosto(1, codigoPer, codigoTest)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cco")))
                data.Add("nombre", dt.Rows(i).Item("Nombre"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListarEvento(ByVal opcion As String, ByVal codigo As String, ByVal f As String, ByVal cod_conv As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaEventos(opcion, codigo, f, cod_conv)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                list.Add(data)
            Next
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("cod", obj.EncrytedString64("T"))
            data1.Add("nombre", "TODOS")
            list.Add(data1)
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
            'Dim data1 As New Dictionary(Of String, Object)()
            'data1.Add("cod", obj.EncrytedString64("T"))
            'data1.Add("nombre", "TODOS")
            'list.Add(data1)
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

    Private Sub ListarActividadPOA(ByVal opcion As String, ByVal codigo As String, ByVal f As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaActividadPOA(opcion, codigo, f)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
                list.Add(data)
            Next
            Dim data1 As New Dictionary(Of String, Object)()
            data1.Add("cod", obj.EncrytedString64("T"))
            data1.Add("nombre", "TODOS")
            list.Add(data1)
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaMotivo(ByVal opcion As String, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaMotivo(opcion, param1)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo")))
                data.Add("nombre", dt.Rows(i).Item("descripcion"))
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

    Private Sub ListaTipoComunicacion(ByVal opcion As String, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaTipoComunicacion(opcion, param1)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_tcom")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_tcom"))
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

    Private Sub ListaCarreraProfesional(ByVal tipo As String, ByVal codigo_eve As Integer, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaCarrerasxEvento(tipo, codigo_eve, param1, "")

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                data.Add("nombre", dt.Rows(i).Item("nombre_cpf"))
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

    Private Sub SituacionInteresado(ByVal opcion As String, ByVal codigoSin As String, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaSituacionInteresado(opcion, codigoSin, param1)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_sin")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_sin"))
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
            data.Add("link", "https://intranet.usat.edu.pe/campusvirtual/sinacceso.html")
        End If

        data.Item("msje") = True
        data.Item("link") = ""

        list.Add(data)
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaEstadoComunicacion(ByVal opcion As String, ByVal param1 As String, ByVal param2 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaEstadoComunicacion(opcion, param1, param2)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_ecom")))
                data.Add("nombre", dt.Rows(i).Item("descripcion_ecom"))
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
    '        Dim objCRM As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarInformacionParaEvento", tipo, modulo, 0, 0)
    '        obj.CerrarConexion()

    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Dim data As New Dictionary(Of String, Object)()
    '            data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_min")))
    '            data.Add("nombre", tb.Rows(i).Item("nombre_min"))
    '            list.Add(data)
    '        Next
    '        objCRM = Nothing
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
    '        Dim objCRM As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarInformacionParaEvento", tipo, modulo, 0, 0)
    '        obj.CerrarConexion()

    '        For i As Integer = 0 To tb.Rows.Count - 1
    '            Dim data As New Dictionary(Of String, Object)()
    '            data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_min")))
    '            data.Add("nombre", tb.Rows(i).Item("nombre_min"))
    '            list.Add(data)
    '        Next
    '        objCRM = Nothing
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
    '        Dim objCRM As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos

    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarEventos", tipo, cco, 0)
    '        obj.CerrarConexion()

    '        Dim data As New Dictionary(Of String, Object)()
    '        'data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
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

    '        objCRM = Nothing
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
    '        Dim objCRM As New ClsCRM
    '        Dim obj As New ClsConectarDatos
    '        Dim tb As New Data.DataTable
    '        Dim cn As New clsaccesodatos


    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()
    '        tb = obj.TraerDataTable("dbo.EVE_ConsultarCentroCostoXPermisosXVisibilidad", tipo, Session("id_per"), "", param, 1)
    '        obj.CerrarConexion()

    '        If tb.Rows.Count > 0 Then
    '            For i As Integer = 0 To tb.Rows.Count - 1
    '                Dim data As New Dictionary(Of String, Object)()
    '                data.Add("cod", objCRM.EncrytedString64(tb.Rows(i).Item("codigo_Cco")))
    '                data.Add("nombre", tb.Rows(i).Item("Nombre"))
    '                list.Add(data)
    '            Next
    '        End If

    '        objCRM = Nothing
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

    Private Sub ListarOrigen()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsCRM
        Dim dt As New Data.DataTable
        dt = obj.ListaOrigen()

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Item("cod") = obj.EncrytedString64(dt.Rows(i).Item("codigo_ori"))
                data.Item("nombre") = dt.Rows(i).Item("nombre_ori")
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ListaInstitucionEducativa(ByVal tipo As String, ByVal codigo As String, ByVal soloSecundaria As Boolean)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListaInstitucionEducativa(tipo, codigo, soloSecundaria)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_ied")))
                    data.Add("nom", tb.Rows(i).Item("Nombre_ied"))
                    'data.Add("dir", tb.Rows(i).Item("Direccion_ied"))
                    data.Add("dep", tb.Rows(i).Item("nombre_Dep"))
                    data.Add("pro", tb.Rows(i).Item("nombre_Pro"))
                    data.Add("dis", tb.Rows(i).Item("nombre_Dis"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ConsultarInstitucionEducativaInteresadoPorConvocatoria(ByVal tipo As String, ByVal codigo As String, ByVal soloSecundaria As Boolean, ByVal codigoCon As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ConsultarInstitucionEducativaInteresadoPorConvocatoria(tipo, codigo, soloSecundaria, codigoCon)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_ied")))
                    data.Add("nom", tb.Rows(i).Item("Nombre_ied"))
                    'data.Add("dir", tb.Rows(i).Item("Direccion_ied"))
                    data.Add("dep", tb.Rows(i).Item("nombre_Dep"))
                    data.Add("pro", tb.Rows(i).Item("nombre_Pro"))
                    data.Add("dis", tb.Rows(i).Item("nombre_Dis"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarRequisitosAdmision(ByVal codigoTest As String, ByVal codigoMin As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM
            Dim tb As New Data.DataTable
            tb = obj.ListarRequisitosAdmision(codigoTest, codigoMin)

            If tb.Rows.Count > 0 Then
                For i As Integer = 0 To tb.Rows.Count - 1
                    Dim data As New Dictionary(Of String, Object)()
                    'If i = 0 Then data.Add("sw", True)
                    data.Add("cod", obj.EncrytedString64(tb.Rows(i).Item("codigo_req")))
                    data.Add("des", tb.Rows(i).Item("descripcion_req"))
                    list.Add(data)
                Next
            End If
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub

    Private Sub ListarGradosPorTipoEstudio(ByVal codigoTest As Integer)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim obj As New ClsCRM

            Dim pregrado(1) As Integer
            pregrado(0) = 1
            pregrado(1) = 2

            Dim i As Integer = IIf(Array.IndexOf(pregrado, codigoTest) <> -1, 0, 3)
            Dim grados As New List(Of Dictionary(Of String, String))

            Dim tercero As New Dictionary(Of String, String)
            tercero.Item("cod") = "T" : tercero.Item("nombre") = "TERCERO"
            grados.Add(tercero)

            Dim cuarto As New Dictionary(Of String, String)
            cuarto.Item("cod") = "C" : cuarto.Item("nombre") = "CUARTO"
            grados.Add(cuarto)

            Dim quinto As New Dictionary(Of String, String)
            quinto.Item("cod") = "Q" : quinto.Item("nombre") = "QUINTO"
            grados.Add(quinto)

            Dim egresado As New Dictionary(Of String, String)
            egresado.Item("cod") = "E" : egresado.Item("nombre") = "EGRESADO"
            grados.Add(egresado)

            Dim universitario As New Dictionary(Of String, String)
            universitario.Item("cod") = "U" : universitario.Item("nombre") = "UNIVERSITARIO"
            grados.Add(universitario)

            For index As Integer = i To grados.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                'If i = 0 Then data.Add("sw", True)
                data.Add("cod", grados(index).Item("cod"))
                data.Add("nombre", grados(index).Item("nombre"))
                list.Add(data)
            Next

            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        Catch ex As Exception
            Dim data As New Dictionary(Of String, Object)()
            data.Add("msje", ex.Message)
            data.Add("rpta", "0 - Servidor")
            list.Add(data)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
        End Try
    End Sub
End Class
