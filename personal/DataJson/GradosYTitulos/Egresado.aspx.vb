
Imports System.IO
Imports System.Web.HttpRequest
Imports System.Collections.Generic
Imports System.Data
Imports EncriptaCodigos
Partial Class DataJson_GradosYTitulos_Egresado
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Try
            Dim k As String = "0" 'Request("k")
            Dim f As String = ""

            Select Case obj.DecrytedString64(Request("action"))
                Case "Listar"
                    Dim cod_test As String
                    Dim texto As String
                    If Request("hdCarrera") = "" Then
                        k = Request("hdCarrera") ' codigo_Cpf
                    Else
                        k = obj.DecrytedString64(Request("hdCarrera")) ' codigo_Cpf
                    End If
                    If Request("hdTest") = "" Then
                        cod_test = Request("hdTest") ' codigo_Cpf
                    Else
                        cod_test = obj.DecrytedString64(Request("hdTest")) ' codigo_Cpf
                    End If
                    texto = Request("txtbuscar")
                    'End If
                    Listar("L", k, cod_test, texto)
                    'Case "ListaPreEgresadosConConsejo"
                    '    Dim var As String()
                    '    Dim codigos As String = ""
                    '    Dim i As Integer
                    '    var = Request("hdcod").Split(",")
                    '    For i = 0 To var.Length - 1
                    '        If i = (var.Length - 1) Then
                    '            codigos += objCRM.DecrytedString64(var(i)).ToString
                    '        Else
                    '            codigos += objCRM.DecrytedString64(var(i)).ToString + ","
                    '        End If
                    '    Next
                    '    k = codigos.ToString
                    '    ListarPreEgresadosConConsejo("PCC", k)
                    'Case "ListaPreEgresadosSinConsejo"
                    '    ListarPreEgresadosConConsejo("PSC", 0)
                Case "BuscarAlumno"
                    Dim opcion As String = "GYT"
                    Dim texto As String = Request("txtbuscar")
                    BuscaAlumno(opcion, texto)
                Case "ConsultarAlumno"
                    Dim cod_alu As Integer = obj.DecrytedString64(Request("cod"))
                    Dim param As String = ""

                    If Request.Form("tipo") <> "" Then
                        param = Request.Form("tipo")
                    End If
                    ConsultarAlumno(cod_alu, param)
                Case "Registrar"
                    Dim codigo As Integer = Request("hdcod")
                    Dim nro_exp As String = Request("txtNroExp")
                    Dim codigo_alu As Integer = obj.DecrytedString64(Request("hdCodigoAlu"))
                    Dim codigo_tes As Integer
                    If Request("hdCodigoTes") <> "0" Then
                        codigo_tes = obj.DecrytedString64(Request("hdCodigoTes"))
                    Else
                        codigo_tes = Request("hdCodigoTes")
                    End If
                    Dim titulo_tes As String = Request("txtTituloTesis")
                    Dim codigo_dgt As Integer = obj.DecrytedString64(Request("CboGrado"))
                    Dim codigo_act As Integer = obj.DecrytedString64(Request("cboActoAcad"))
                    Dim fecha_acto As String = Request("txtFechaActo")
                    Dim fecha_consejo As String = "" 'Request("txtFechaConsejo")
                    Dim nro_Res As String = "" 'Request("txtNroResolucion")
                    Dim fecha_res As String = "" 'Request("txtFechaResolucion")
                    'fecha_consejouniversitario
                    'nro_resolucion
                    'fecha_resolucion
                    'fecha diploma
                    Dim codigo_gru As Integer
                    If Request("cboGrupo") = "" Then
                        codigo_gru = 0
                    Else
                        codigo_gru = obj.DecrytedString64(Request("cboGrupo"))
                    End If
                    Dim nro_libro As String = Request("txtNroLibro")
                    Dim nro_folio As String = Request("txtNroFolio")
                    Dim nro_registro As String = Request("txtRegistro")
                    Dim codigo_fac As Integer = obj.DecrytedString64(Request("cboFacultad"))
                    Dim codigo_esp As Integer = obj.DecrytedString64(Request("cboEspecialidad"))
                    Dim modalidad_estudio As String = Request("cboModEstudio")
                    Dim tipo_emision As String = Request("cboEmisionDiploma")
                    Dim observaciones As String = Request("txtObservaciones")
                    Dim autoridad1 As Integer = obj.DecrytedString64(Request("cboAutoridad1"))
                    Dim autoridad2 As Integer = obj.DecrytedString64(Request("cboAutoridad2"))
                    Dim autoridad3 As Integer = obj.DecrytedString64(Request("cboAutoridad3"))
                    Dim autoridad4 As Integer = IIf(String.IsNullOrEmpty(obj.DecrytedString64(Request("cboAutoridad4"))), 0, obj.DecrytedString64(Request("cboAutoridad4")))
                    Dim estado As Integer = "1"
                    Dim cod_per As Integer = Session("id_per")
                    Dim nroRes_Fac As String = Request("txtNroResolucionFac")
                    Dim fechaRes_Fac As String = Request("txtFechaResolucionF")
                    If fechaRes_Fac = "" Then
                        fechaRes_Fac = "01/01/1900"
                    End If
                    If fecha_acto = "" Then
                        fecha_acto = "01/01/1900"
                    End If
                    Actualizar(codigo, nro_exp, codigo_alu, codigo_dgt, codigo_act, fecha_acto, fecha_consejo, nro_Res, fecha_res, "", codigo_gru, nro_libro, nro_folio, nro_registro, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, autoridad1, autoridad2, autoridad3, estado, cod_per, codigo_tes, titulo_tes, nroRes_Fac, fechaRes_Fac, autoridad4)

                Case "Editar"
                    k = obj.DecrytedString64(Request("hdcod"))
                    Dim param1 As String = ""
                    Listar("E", k, f, param1)
                Case "Modificar"
                    Dim codigo As Integer = obj.DecrytedString64(Request("hdCodEgr"))
                    Dim nro_exp As String = Request("txtNroExp")
                    Dim codigo_alu As Integer = obj.DecrytedString64(Request("hdCodigoAlu"))
                    Dim codigo_tes As Integer
                    If Request("hdCodigoTes") <> "0" Then
                        codigo_tes = obj.DecrytedString64(Request("hdCodigoTes"))
                    Else
                        codigo_tes = Request("hdCodigoTes")
                    End If
                    Dim titulo_tes As String = Request("txtTituloTesis")
                    Dim codigo_dgt As Integer = obj.DecrytedString64(Request("CboGrado"))
                    Dim codigo_act As Integer = obj.DecrytedString64(Request("cboActoAcad"))
                    Dim fecha_acto As String = Request("txtFechaActo")
                    Dim fecha_consejo As String = "" 'Request("txtFechaConsejo")
                    Dim nro_Res As String = "" 'Request("txtNroResolucion")
                    Dim fecha_res As String = "" 'Request("txtFechaResolucion")
                    'fecha diploma
                    Dim codigo_gru As Integer = obj.DecrytedString64(Request("cboGrupo"))
                    Dim nro_libro As String = Request("txtNroLibro")
                    Dim nro_folio As String = Request("txtNroFolio")
                    Dim nro_registro As String = Request("txtRegistro")
                    Dim codigo_fac As Integer = obj.DecrytedString64(Request("cboFacultad"))
                    Dim codigo_esp As Integer = obj.DecrytedString64(Request("cboEspecialidad"))
                    Dim modalidad_estudio As String = Request("cboModEstudio")
                    Dim tipo_emision As String = Request("cboEmisionDiploma")
                    Dim observaciones As String = Request("txtObservaciones")
                    Dim autoridad1 As Integer = obj.DecrytedString64(Request("cboAutoridad1"))
                    Dim autoridad2 As Integer = obj.DecrytedString64(Request("cboAutoridad2"))
                    Dim autoridad3 As Integer = obj.DecrytedString64(Request("cboAutoridad3"))
                    Dim autoridad4 As Integer = IIf(String.IsNullOrEmpty(obj.DecrytedString64(Request("cboAutoridad4"))), 0, obj.DecrytedString64(Request("cboAutoridad4")))
                    Dim estado As Integer = "1"
                    Dim cod_per As Integer = Session("id_per")
                    Dim nroRes_Fac As String = Request("txtNroResolucionFac")
                    Dim fechaRes_Fac As String = Request("txtFechaResolucionF")
                    If fechaRes_Fac = "" Then
                        fechaRes_Fac = "01/01/1900"
                    End If
                    Actualizar(codigo, nro_exp, codigo_alu, codigo_dgt, codigo_act, fecha_acto, fecha_consejo, nro_Res, fecha_res, "", codigo_gru, nro_libro, nro_folio, nro_registro, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, autoridad1, autoridad2, autoridad3, estado, cod_per, codigo_tes, titulo_tes, nroRes_Fac, fechaRes_Fac, autoridad4)


                Case "ConsultarAutoridad"
                    Dim codigo As String = obj.DecrytedString64(Request("param1"))
                    ConsultarAutoridad("CAG", codigo)
                Case "ActualizarResolucion"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += obj.DecrytedString64(var(i)).ToString
                        Else
                            codigos += obj.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    Dim nro_Resol As String = Request("txtNroResolucion")
                    Dim fecha_Resol As String = Request("txtFechaResol")
                    ActualizarResolucion(k, nro_Resol, fecha_Resol)
                Case "ConsultaExpedientesResolucion"
                    Dim tipo As String = Request("estado")
                    Dim codigo_den As String

                    If Request("cod_den") <> "0" Then
                        codigo_den = obj.DecrytedString64(Request("cod_den"))
                    Else
                        codigo_den = Request("cod_den")
                    End If

                    Dim codigo_Scu As String = obj.DecrytedString64(Request("hdcod"))
                    ConsultarExpResolucion(tipo, codigo_Scu, codigo_den)
                Case "ConsultaExpedientesOficio"
                    k = obj.DecrytedString64(Request("hdcod"))
                    f = Request("estado")
                    Dim param1 As String = ""
                    Listar("G", k, f, param1)
                Case "ActualizarOficio"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += obj.DecrytedString64(var(i)).ToString
                        Else
                            codigos += obj.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    Dim nro_oficio As String = Request("txtNroOficio")
                    ActualizarOficio(k, nro_oficio)
                Case "ListaExpedientesEntregar"
                    If Request("hdcod") <> "T" Then
                        k = obj.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    f = Request("estado")
                    Dim txtbuscar As String = Request("txtBusqueda")
                    Listar("LEE", k, f, txtbuscar)
                Case "EntregaDiploma"
                    k = obj.DecrytedString64(Request("hdcod"))
                    f = Request("entregado")
                    Dim cod_per As Integer = Session("id_per")
                    Dim codigo_dta As Integer = Request("cod_dta")
                    Dim codigo_tfu As Integer = Request("cod_tfu")
                    ActualizarEntrega(k, f, codigo_dta, codigo_tfu, cod_per)
                Case "ActualizarDatosContacto"
                    k = obj.DecrytedString64(Request("hdCodigoAluME"))
                    Dim apepat As String = Request("txtapepat")
                    Dim apemat As String = Request("txtapemat")
                    Dim nombres As String = Request("txtnombres")
                    Dim email As String = Request("txtemail")
                    Dim telmov As String = Request("txttelmov")
                    Dim telfijo As String = Request("txttelfijo")
                    Dim cod_per As Integer = Session("id_per")
                    ActualizarDatosContacto(k, apepat, apemat, nombres, email, telmov, telfijo, cod_per)
                Case "ListaEgresadoAsignaCorrelativo"

                    Dim cod_test As String
                    Dim texto As String
                    Dim cod_scu As String
                    Dim codigo_tdg As String
                    If Request("hdscu") = "" Then
                        cod_scu = Request("hdscu") ' sesion consejo
                    Else
                        cod_scu = obj.DecrytedString64(Request("hdscu")) ' sesion consejo
                    End If
                    If Request("hdTest") = "" Then
                        cod_test = Request("hdTest") ' test
                    Else
                        cod_test = obj.DecrytedString64(Request("hdTest")) ' test
                    End If
                    If Request("hdCarrera") = "" Then
                        k = Request("hdCarrera") ' codigo_Cpf
                    Else
                        k = obj.DecrytedString64(Request("hdCarrera")) ' codigo_Cpf
                    End If
                    If Request("hdTipo") = "" Then
                        codigo_tdg = Request("hdTipo") ' test
                    Else
                        codigo_tdg = obj.DecrytedString64(Request("hdTipo")) ' test
                    End If
                    texto = Request("txtbuscar")
                    'End If
                    ListarEgresadoCorrelativo(cod_scu, cod_test, k, texto, codigo_tdg)
                Case "ObtenerCorrelativos"
                    ObetnerCorrelativos()
                Case "GenerarCorrelativos"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += obj.DecrytedString64(var(i)).ToString
                        Else
                            codigos += obj.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    Dim nro_dip As String = Request("nroDiploma")
                    Dim libro_b As String = Request("libro_b")
                    Dim folio_b As String = Request("folio_b")
                    Dim libro_t As String = Request("libro_t")
                    Dim folio_t As String = Request("folio_t")
                    Dim libro_m As String = Request("libro_m")
                    Dim folio_m As String = Request("folio_m")
                    Dim libro_d As String = Request("libro_d")
                    Dim folio_d As String = Request("folio_d")
                    Dim libro_s As String = Request("libro_s")
                    Dim folio_s As String = Request("folio_s")
                    GenerarCorrelativos(k, nro_dip, libro_b, folio_b, libro_t, folio_t, libro_m, folio_m, libro_d, folio_d, libro_s, folio_s)

                Case "QuitarCorrelativos"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += obj.DecrytedString64(var(i)).ToString
                        Else
                            codigos += obj.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    QuitarCorrelativos(k)

                Case "ListaEgresadoAsignaGrupo"
                    Dim cod_test As String
                    Dim texto As String
                    Dim cod_scu As String
                    If Request("hdscu") = "" Then
                        cod_scu = Request("hdscu") ' sesion consejo
                    Else
                        cod_scu = obj.DecrytedString64(Request("hdscu")) ' sesion consejo
                    End If
                    If Request("hdTest") = "" Then
                        cod_test = Request("hdTest") ' test
                    Else
                        cod_test = obj.DecrytedString64(Request("hdTest")) ' test
                    End If
                    If Request("hdCarrera") = "" Then
                        k = Request("hdCarrera") ' codigo_Cpf
                    Else
                        k = obj.DecrytedString64(Request("hdCarrera")) ' codigo_Cpf
                    End If
                    texto = Request("txtbuscar")
                    'End If
                    ListarEgresadoGrupo(cod_scu, cod_test, k, texto)

                Case "ActualizarGrupo"
                    Dim var As String()
                    Dim codigos As String = ""
                    Dim i As Integer
                    var = Request("hdcod").Split(",")
                    For i = 0 To var.Length - 1
                        If i = (var.Length - 1) Then
                            codigos += obj.DecrytedString64(var(i)).ToString
                        Else
                            codigos += obj.DecrytedString64(var(i)).ToString + ","
                        End If
                    Next
                    k = codigos.ToString
                    Dim cod_grupo As String
                    If Request("cboGrupo") <> "" Then
                        cod_grupo = obj.DecrytedString64(Request("cboGrupo"))
                    Else
                        cod_grupo = Request("cboGrupo")
                    End If

                    ActualizarGrupo(k, cod_grupo)
                Case "ConsultarFechaActo"
                    k = obj.DecrytedString64(Request("cod"))
                    f = obj.DecrytedString64(Request("cod_dgt"))
                    ConsultarFechaActo(k, f)


                    '*********************************************************************************
                    '*********************** GESTIÓN DEL EGRESADO ************************************
                    '*********************************************************************************
                    '*********************** Inicio HCANO 19/08/2020 *********************************
                Case "ConsultarTramites"
                    k = Request("estado")
                    f = Request("txtbuscar")
                    ConsultarTramites(k, f)
                Case "ConsultarRequisitos"
                    k = Request("glosa")
                    ConsultarRequisitos(k)
                Case "ObservarTramite"
                    k = obj.DecrytedString64(Request("param1"))
                    Dim textoobservacion As String = Request("param2")
                    Dim requisitosobservados As String = Request("param3").Replace("|:|", "<br>")
                    ObservarTramite(k, 1, "O", textoobservacion, requisitosobservados)

                Case "ExpedientesPendientesCorrelativos"
                    Dim estado As String = Request("estado")
                    Dim cod_test As String
                    Dim texto As String
                    Dim cod_scu As String
                    Dim codigo_tdg As String
                    If Request("hdscu") = "" Then
                        cod_scu = Request("hdscu") ' sesion consejo
                    Else
                        cod_scu = obj.DecrytedString64(Request("hdscu")) ' sesion consejo
                    End If
                    If Request("hdTest") = "" Then
                        cod_test = Request("hdTest") ' test
                    Else
                        cod_test = obj.DecrytedString64(Request("hdTest")) ' test
                    End If
                    If Request("hdCarrera") = "" Then
                        k = Request("hdCarrera") ' codigo_Cpf
                    Else
                        k = obj.DecrytedString64(Request("hdCarrera")) ' codigo_Cpf
                    End If
                    If Request("hdTipo") = "" Then
                        codigo_tdg = Request("hdTipo") ' test
                    Else
                        codigo_tdg = obj.DecrytedString64(Request("hdTipo")) ' test
                    End If
                    texto = Request("txtbuscar")
                    'End If
                    ExpedientesPendientesCorrelativos(estado, cod_scu, cod_test, k, texto, codigo_tdg)
                    'ConsultarRequisitos(k)
                Case "ListarArchivosTramite"
                    k = Request("op")
                    Dim codigo_trl As String = obj.DecrytedString64(Request("trl"))
                    Dim codigo_dta As String = obj.DecrytedString64(Request("dta"))
                    ListarArchivosTramite(k, codigo_trl, codigo_dta)
                    '*********************** Fin HCANO 19/08/2020 *********************************
                    '*********************** OLLUEN 10/09/2020 *********************************
                Case "ConsultarEntregaDiplomas"
                    If Request("hdcod") <> "T" Then
                        k = obj.DecrytedString64(Request("hdcod"))
                    Else
                        k = Request("hdcod")
                    End If
                    f = Request("estado")
                    Dim txtbuscar As String = Request("txtBusqueda")
                    ListarEntregaDiplomas("LEE", k, f, txtbuscar)
                Case "EntregaDiplomaNew"
                    k = obj.DecrytedString64(Request("hdcod"))
                    f = Request("entregado")
                    Dim cod_per As Integer = Session("id_per")
                    Dim codigo_dta As Integer = Request("cod_dta")
                    Dim codigo_tfu As Integer = Request("cod_tfu")
                    'Dim codigo_tfu As Integer = Session("cod_tfu")
                    ActualizarEntregaNew(k, f, codigo_dta, codigo_tfu, cod_per)

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

    Private Sub BuscaAlumno(ByVal opcion As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.BuscaAlumno(opcion, texto)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Alu")))
                data.Add("tipodoc", dt.Rows(i).Item("tipodocident_Alu"))
                data.Add("nrodoc", dt.Rows(i).Item("nrodocident_Alu"))
                data.Add("coduniver", dt.Rows(i).Item("codigouniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("test", dt.Rows(i).Item("descripcion_test"))
                data.Add("cpf", dt.Rows(i).Item("nombre_cpf"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Private Sub ConsultarAlumno(ByVal cod_alu As Integer, ByVal param As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        Dim obEnc As New EncriptaCodigos.clsEncripta

        dt = obj.ConsultarAlumno(cod_alu, param)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Alu")))
                data.Add("coduniver", dt.Rows(i).Item("codigouniver_Alu"))
                data.Add("tipodoc", dt.Rows(i).Item("tipodocident_Alu"))
                data.Add("nrodoc", dt.Rows(i).Item("nrodocident_Alu"))
                data.Add("apepat", dt.Rows(i).Item("ApellidoPat_Alu"))
                data.Add("apemat", dt.Rows(i).Item("ApellidoMat_Alu"))
                data.Add("nom", dt.Rows(i).Item("nombres_Alu"))
                data.Add("cod_test", obj.EncrytedString64(dt.Rows(i).Item("codigo_test")))
                data.Add("test", dt.Rows(i).Item("descripcion_test"))
                data.Add("cod_fac", obj.EncrytedString64(dt.Rows(i).Item("codigo_fac")))
                data.Add("nom_fac", dt.Rows(i).Item("nombre_fac"))
                data.Add("cod_cp", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                data.Add("nom_cp", dt.Rows(i).Item("nombre_cpf"))
                data.Add("cod_pes", obj.EncrytedString64(dt.Rows(i).Item("codigo_pes")))
                data.Add("nom_pes", dt.Rows(i).Item("descripcion_pes"))
                data.Add("nom_tes", dt.Rows(i).Item("titulo_tes"))
                data.Add("cod_tes", obj.EncrytedString64(dt.Rows(i).Item("codigo_tes")))
                data.Add("nom_esp", dt.Rows(i).Item("nombre_Esp"))
                data.Add("foto", obEnc.CodificaWeb("069" & dt.Rows(i).Item("codigouniver_Alu")))
                data.Add("ver_bach", dt.Rows(i).Item("Verifica_Bachiller"))
                data.Add("correo", dt.Rows(i).Item("EMAIL"))
                data.Add("telmov", dt.Rows(i).Item("TELEF_MOV"))
                data.Add("telfijo", dt.Rows(i).Item("TELEF_FIJO"))
                data.Add("fec_acto", dt.Rows(i).Item("fecha_acto"))
                data.Add("tipo_acto", obj.EncrytedString64(dt.Rows(i).Item("tipo_acto")))
                data.Add("fechareso", dt.Rows(i).Item("fecharesofacultad"))
                data.Add("nroreso", dt.Rows(i).Item("nroresofacultad"))
                data.Add("archivoresofac", dt.Rows(i).Item("archivoresofac"))


                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Private Sub Listar(ByVal opcion As String, ByVal codigo As String, ByVal cod_test As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        Dim obEnc As New EncriptaCodigos.clsEncripta
        dt = obj.ListaEgresado(opcion, codigo, cod_test, texto)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("nro_exp", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                data.Add("cod_univer", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("pes", dt.Rows(i).Item("descripcion_Pes"))
                data.Add("est", dt.Rows(i).Item("estado_egr"))
                data.Add("tipo_dip", dt.Rows(i).Item("TipoEmisionDiploma_egr"))
                data.Add("abrev_dip", dt.Rows(i).Item("abreviatura_tdg"))
                data.Add("fecha_reg", dt.Rows(i).Item("fecha_reg"))

                If opcion = "E" Then
                    data.Add("fec_cons", dt.Rows(i).Item("FechaAcuerdoConsUniver"))
                    data.Add("nro_res", dt.Rows(i).Item("NroResolucion_egr"))
                    data.Add("fec_res", dt.Rows(i).Item("FechaResolucion_egr"))
                    data.Add("tipodoc", dt.Rows(i).Item("tipodocident_Alu"))
                    data.Add("nrodoc", dt.Rows(i).Item("nrodocident_Alu"))
                    If dt.Rows(i).Item("codigo_gru") = "0" Then
                        data.Add("cod_gru", "")
                    Else
                        data.Add("cod_gru", obj.EncrytedString64(dt.Rows(i).Item("codigo_gru")))
                    End If
                    data.Add("nro_lib", dt.Rows(i).Item("Nrolibro_egr"))
                    data.Add("nro_fol", dt.Rows(i).Item("Nrofolio_egr"))
                    data.Add("nro_reg", dt.Rows(i).Item("nroRegistro_egr"))
                    data.Add("cod_fac", obj.EncrytedString64(dt.Rows(i).Item("CodigoFac_egr")))
                    data.Add("cod_cp", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                    data.Add("nom_cp", dt.Rows(i).Item("nombre_cpf"))
                    data.Add("cod_pes", obj.EncrytedString64(dt.Rows(i).Item("codigo_Pes")))
                    data.Add("cod_esp", obj.EncrytedString64(dt.Rows(i).Item("codigo_Esp")))
                    data.Add("cod_dgt", obj.EncrytedString64(dt.Rows(i).Item("codigo_dgt")))
                    data.Add("cod_tes", obj.EncrytedString64(dt.Rows(i).Item("codigo_tes")))
                    data.Add("nom_tes", dt.Rows(i).Item("titulo_tes"))
                    data.Add("fec_acto", dt.Rows(i).Item("FechaActoAcademico_egr"))
                    data.Add("cod_acto", obj.EncrytedString64(dt.Rows(i).Item("codigo_act")))
                    data.Add("mod_est", dt.Rows(i).Item("ModEstudio_egr"))
                    data.Add("obs", dt.Rows(i).Item("obervaciones_egr"))
                    data.Add("foto", obEnc.CodificaWeb("069" & dt.Rows(i).Item("codigouniver_Alu")))

                    data.Add("correo", dt.Rows(i).Item("EMAIL"))
                    data.Add("telmov", dt.Rows(i).Item("TELEF_MOV"))
                    data.Add("telfijo", dt.Rows(i).Item("TELEF_FIJO"))

                    data.Add("nrores_Fac", dt.Rows(i).Item("NroResolucionFac_egr"))
                    data.Add("fecres_Fac", dt.Rows(i).Item("FechaResolucionFac_egr"))


                    data.Add("apepat", dt.Rows(i).Item("apellidoPat_Alu"))
                    data.Add("apemat", dt.Rows(i).Item("apellidoMat_Alu"))
                    data.Add("nom", dt.Rows(i).Item("nombres_Alu"))
                    data.Add("archivo", dt.Rows(i).Item("archivo"))
                    data.Add("archivoresofac", dt.Rows(i).Item("archivoresofac"))
                    data.Add("trl", obj.EncrytedString64(dt.Rows(i).Item("codigo_trl")))
                    data.Add("dta", obj.EncrytedString64(dt.Rows(i).Item("codigo_dta")))
                End If
                If opcion = "G" Then
                    data.Add("num_of", dt.Rows(i).Item("NroOficioRemision_egr"))
                End If
                If opcion = "LEE" Then
                    data.Add("entregado", dt.Rows(i).Item("entregado"))
                    data.Add("deno", dt.Rows(i).Item("descripcion_dgt"))
                    data.Add("cod_dta", dt.Rows(i).Item("codigo_dta"))
                End If
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Private Sub ConsultarAutoridad(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarAutoridad(opcion, codigo, "", "")
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", dt.Rows(i).Item("codigo_aeg"))
                data.Add("cod_cgo", obj.EncrytedString64(dt.Rows(i).Item("codigo_cgo")))
                data.Add("cod_ccp", obj.EncrytedString64(dt.Rows(i).Item("codigo_ccp")))
                data.Add("orden", dt.Rows(i).Item("orden_aeg"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ActualizarResolucion(ByVal cod As String, ByVal nro_resolucion As String, ByVal fecha_resol As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.Actualizar_Resolucion(cod, nro_resolucion, fecha_resol)
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


    Private Sub ConsultarExpResolucion(ByVal opcion As String, ByVal codigo As String, ByVal param1 As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarExpResolucion(opcion, codigo, param1)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("Alumno", dt.Rows(i).Item("alumno"))
                data.Add("NroExp", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("nro_res", dt.Rows(i).Item("NroResolucion_egr"))
                data.Add("fec_res", dt.Rows(i).Item("FechaResolucion_egr"))
                data.Add("deno", dt.Rows(i).Item("descripcion_dgt"))
                'data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                'data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ActualizarOficio(ByVal cod As String, ByVal nro_oficio As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.Actualizar_Oficio(cod, nro_oficio)
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

    Private Sub ActualizarEntrega(ByVal cod As String, ByVal entregado As String, ByVal codigo_dta As Integer, ByVal codigo_tfu As Integer, ByVal usuario As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarEntrega(cod, entregado, codigo_dta, codigo_tfu, usuario)
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






    Private Sub ListarPreEgresadosConConsejo(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListaSesionConsejoU(opcion, codigo)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("Alumno", dt.Rows(i).Item("alumno"))
                data.Add("NroExp", dt.Rows(i).Item("NroExpediente_egr"))

                'data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                'data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ListarPreEgresadosSinConsejo(ByVal opcion As String, ByVal codigo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListaSesionConsejoU(opcion, codigo)
        If dt.Rows.Count > 0 Then
            'If codigo = "%" Then
            '    Dim data1 As New Dictionary(Of String, Object)()
            '    data1.Add("cod", obj.EncrytedString64("T"))
            '    data1.Add("nom", "TODOS")
            '    data1.Add("fec", "%")
            '    list.Add(data1)
            'End If
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("Alumno", dt.Rows(i).Item("alumno"))
                data.Add("NroExp", dt.Rows(i).Item("NroExpediente_egr"))

                'data.Add("nom", dt.Rows(i).Item("descripcion_scu"))
                'data.Add("fec", dt.Rows(i).Item("fecha_scu"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub Actualizar(ByVal codigo_egr As Integer, ByVal nro_expediente As String, ByVal codigo_alu As Integer, ByVal codigo_dgt As Integer, ByVal codigo_act As Integer, _
                             ByVal fecha_act As String, ByVal fecha_consejo As String, ByVal nro_resolucion As String, ByVal fecha_resolucion As String, ByVal fecha_diploma As String, _
                              ByVal codigo_gru As Integer, ByVal nrolibro As String, ByVal nrofolio As String, ByVal nroregistro As String, ByVal codigo_fac As Integer, ByVal codigo_esp As Integer, _
                              ByVal modalidad_estudio As String, ByVal tipo_emision As String, ByVal observaciones As String, ByVal autoridad1 As Integer, ByVal autoridad2 As Integer, _
                              ByVal autoridad3 As Integer, ByVal estado As Integer, ByVal usuario As Integer, ByVal codigo_tes As Integer, ByVal titulo_tes As String, ByVal nroResFac As String, ByVal fechaResFac As String, ByVal autoridad4 As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Try
            Dim dt As New Data.DataTable
            'dt = obj.ActualizarEgresado(codigo_egr, nro_expediente, codigo_alu, codigo_dgt, codigo_act, fecha_act, fecha_consejo, nro_resolucion, fecha_resolucion, fecha_diploma, nrolibro, nrofolio, codigo_gru, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, estado, usuario)
            dt = obj.ActualizarEgresado(codigo_egr, nro_expediente, codigo_alu, codigo_dgt, codigo_act, fecha_act, fecha_consejo, nro_resolucion, fecha_resolucion, codigo_gru, nrolibro, nrofolio, nroregistro, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, autoridad1, autoridad2, autoridad3, estado, usuario, codigo_tes, titulo_tes, nroResFac, fechaResFac, autoridad4)
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

    Private Sub ActualizarDatosContacto(ByVal cod As String, ByVal apepat As String, ByVal apemat As String, ByVal nombres As String, ByVal email As String, ByVal telmov As String, ByVal telfijo As String, ByVal cod_per As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarDatosContacto(cod, apepat, apemat, nombres, email, telmov, telfijo, cod_per)
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


    Private Sub ListarEgresadoCorrelativo(ByVal codigo_scu As String, ByVal codigo_test As String, ByVal cod_cpf As String, ByVal texto As String, ByVal codigo_tdg As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        Dim obEnc As New EncriptaCodigos.clsEncripta
        dt = obj.ListaEgresadosAsignaCorrelativo(codigo_scu, codigo_test, cod_cpf, texto, codigo_tdg)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("nro_dip", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("nro_exp", dt.Rows(i).Item("NroDiploma_egr"))
                data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                data.Add("cod_univer", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("nom_cpf", dt.Rows(i).Item("nombre_cpf"))
                data.Add("est", dt.Rows(i).Item("estado_egr"))
                data.Add("tipo_dip", dt.Rows(i).Item("tipo"))
                data.Add("abrev_dip", dt.Rows(i).Item("abreviatura_tdg"))
                data.Add("nro_lib", dt.Rows(i).Item("Nrolibro_egr"))
                data.Add("nro_folio", dt.Rows(i).Item("Nrofolio_egr"))
                data.Add("fec_reg", dt.Rows(i).Item("fecha_reg"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ObetnerCorrelativos()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ObtenerCorrelativos()
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("nro_dip", dt.Rows(i).Item("NRO_DIPLOMA"))
                data.Add("lib_b", dt.Rows(i).Item("LIBRO_B"))
                data.Add("lib_t", dt.Rows(i).Item("LIBRO_T"))
                data.Add("lib_m", dt.Rows(i).Item("LIBRO_M"))
                data.Add("lib_d", dt.Rows(i).Item("LIBRO_D"))
                data.Add("lib_s", dt.Rows(i).Item("LIBRO_S"))
                data.Add("fol_b", dt.Rows(i).Item("FOLIO_B"))
                data.Add("fol_t", dt.Rows(i).Item("FOLIO_T"))
                data.Add("fol_m", dt.Rows(i).Item("FOLIO_M"))
                data.Add("fol_d", dt.Rows(i).Item("FOLIO_D"))
                data.Add("fol_s", dt.Rows(i).Item("FOLIO_S"))
                data.Add("nro_dipb", dt.Rows(i).Item("ULTIMO_BACHILLER"))
                data.Add("nro_dipt", dt.Rows(i).Item("ULTIMO_TITULO"))
                data.Add("nro_dips", dt.Rows(i).Item("ULTIMO_SEGUNDAESP"))
                data.Add("nro_dipm", dt.Rows(i).Item("ULTIMO_MAESTRIA"))
                data.Add("nro_dipd", dt.Rows(i).Item("ULTIMO_DOCTOR"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub GenerarCorrelativos(ByVal cod As String, ByVal nro_dip As String, ByVal libro_B As String, ByVal folio_b As String, ByVal libro_t As String, ByVal folio_t As String, ByVal libro_m As String, ByVal folio_m As String, ByVal libro_d As String, ByVal folio_d As String, ByVal libro_s As String, ByVal folio_s As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.GenerarCorrelativos(cod, nro_dip, libro_B, folio_b, libro_t, folio_t, libro_m, folio_m, libro_d, folio_d, libro_s, folio_s)
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

    Private Sub QuitarCorrelativos(ByVal cod As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.QuitarCorrelativos(cod)
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

    Private Sub ListarEgresadoGrupo(ByVal codigo_scu As String, ByVal codigo_test As String, ByVal cod_cpf As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        Dim obEnc As New EncriptaCodigos.clsEncripta
        dt = obj.ListaEgresadosAsignaGrupo(codigo_scu, codigo_test, cod_cpf, texto)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("nro_dip", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("nro_exp", dt.Rows(i).Item("NroDiploma_egr"))
                data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                data.Add("cod_univer", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("nom_cpf", dt.Rows(i).Item("nombre_cpf"))
                data.Add("est", dt.Rows(i).Item("estado_egr"))
                data.Add("tipo_dip", dt.Rows(i).Item("tipo"))
                data.Add("abrev_dip", dt.Rows(i).Item("abreviatura_tdg"))
                data.Add("nro_lib", dt.Rows(i).Item("Nrolibro_egr"))
                data.Add("nro_folio", dt.Rows(i).Item("Nrofolio_egr"))
                data.Add("fec_reg", dt.Rows(i).Item("fecha_reg"))
                data.Add("grupo", dt.Rows(i).Item("descripcion_gru"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ActualizarGrupo(ByVal cod As String, ByVal cod_grupo As String)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.Actualizar_Grupo(cod, cod_grupo)
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
    ' Consultar Fecha de Acto Academico (solo para Duplicados)
    Private Sub ConsultarFechaActo(ByVal cod_alu As Integer, ByVal codigo_dgt As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ConsultaFechaActo(cod_alu, codigo_dgt)
            Data.Add("fecha", dt.Rows(0).Item("fecha"))
            Data.Add("tipo", obj.EncrytedString64(dt.Rows(0).Item("tipo")))
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


    '*********************************************************************************
    '*********************** GESTIÓN DEL EGRESADO ************************************
    '*********************************************************************************
    '*********************** Inicio HCANO 19/08/2020 *********************************

    Private Sub ConsultarTramites(ByVal estado As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarTramites(estado, texto)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_Alu")))
                data.Add("tipodoc", dt.Rows(i).Item("tipodocident_Alu"))
                data.Add("nrodoc", dt.Rows(i).Item("nrodocident_Alu"))
                data.Add("coduniver", dt.Rows(i).Item("codigouniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("test", dt.Rows(i).Item("descripcion_test"))
                data.Add("cpf", dt.Rows(i).Item("nombre_cpf"))
                data.Add("tip", dt.Rows(i).Item("tipo"))
                data.Add("emi", dt.Rows(i).Item("emision"))
                data.Add("archivo", dt.Rows(i).Item("archivo"))
                data.Add("nro_expediente", dt.Rows(i).Item("glosaCorrelativo_trl"))
                data.Add("estado", dt.Rows(i).Item("estado"))
                data.Add("dta", obj.EncrytedString64(dt.Rows(i).Item("codigo_dta")))
                data.Add("trl", obj.EncrytedString64(dt.Rows(i).Item("codigo_trl")))
                data.Add("fecha_reg", dt.Rows(i).Item("fecha_reg"))

                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ConsultarRequisitos(ByVal glosacorrelativo As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ConsultarRequisitos(glosacorrelativo)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("nombre", dt.Rows(i).Item("nombre_tre"))
                data.Add("detalle", dt.Rows(i).Item("descripcion_resp"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    Private Sub ObservarTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal textoObservacion As String, ByVal requisitos As String)
        Dim objcmp As New List(Of Dictionary(Of String, Object))()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim Data As New Dictionary(Of String, Object)()
        Try
            Dim cmp As New clsComponenteTramiteVirtualCVE

            cmp._codigo_dta = codigo_dta
            'cmp.tipoOperacion = "1"
            cmp.tipoOperacion = tipooperacion
            cmp._codigo_per = Session("id_per")
            cmp._codigo_tfu = 178 'jefatura de grados y titulos
            cmp._estadoFlujo = "O"
            cmp._estadoAprobacion = estadoaprobacion
            cmp._observacionEvaluacion = textoObservacion
            cmp._listaRequisitosObservados = requisitos

            Dim obj As New ClsGradosyTitulos
            Dim dt As New Data.DataTable
            dt = obj.ObtenerSecretariaFacultadApruebaTramite(codigo_dta)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("correo").ToString <> "" Then
                    If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                        cmp._copiarEmailDestinatario = dt.Rows(0).Item("correo").ToString
                    Else
                        cmp._copiarEmailDestinatario = "hcano@usat.edu.pe"
                    End If
                Else
                End If
            End If

            objcmp = cmp.mt_EvaluarTramite()
            'Dim dt As New Data.DataTable
            'dt.Columns.Add("revision")
            'dt.Columns.Add("registros")
            'dt.Columns.Add("email")
            'For Each fila As Dictionary(Of String, Object) In objcmp
            '    dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
            'Next
            JSONresult = serializer.Serialize(objcmp)
            Response.Write(JSONresult)
        Catch ex As Exception

            Data.Add("rpta", "0 - REG")
            Data.Add("msje", ex.Message)
            objcmp.Add(Data)
            JSONresult = serializer.Serialize(objcmp)
            Response.Write(JSONresult)
        End Try

    End Sub

    Private Sub ExpedientesPendientesCorrelativos(ByVal estado As String, ByVal codigo_scu As String, ByVal codigo_test As String, ByVal cod_cpf As String, ByVal texto As String, ByVal codigo_tdg As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ExpedientesPendientesCorrelativos(estado, codigo_scu, codigo_test, cod_cpf, texto, codigo_tdg)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("nro_dip", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("nro_exp", dt.Rows(i).Item("NroDiploma_egr"))
                data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                data.Add("cod_univer", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("nom_cpf", dt.Rows(i).Item("nombre_cpf"))
                data.Add("est", dt.Rows(i).Item("estado_egr"))
                data.Add("tipo_dip", dt.Rows(i).Item("tipo"))
                data.Add("abrev_dip", dt.Rows(i).Item("abreviatura_tdg"))
                data.Add("nro_lib", dt.Rows(i).Item("Nrolibro_egr"))
                data.Add("nro_folio", dt.Rows(i).Item("Nrofolio_egr"))
                data.Add("fec_reg", dt.Rows(i).Item("fecha_reg"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub


    Private Sub ListarArchivosTramite(ByVal opcion As String, ByVal codigo_trl As String, ByVal codigo_dta As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        dt = obj.ListarArchivosTramite(opcion, codigo_trl, codigo_dta)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("tabla", dt.Rows(i).Item("tabla"))
                data.Add("valorcampo", dt.Rows(i).Item("valorcampo"))
                data.Add("observacion", dt.Rows(i).Item("observacion"))
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub

    '************************************ FIN HCANO 19/08/2020 ************************

    '************************************ INICIO OLLUEN 10/09/2020 ************************
    Private Sub ListarEntregaDiplomas(ByVal opcion As String, ByVal codigo As String, ByVal cod_test As String, ByVal texto As String)
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()

        Dim obj As New ClsGradosyTitulos
        Dim dt As New Data.DataTable
        Dim obEnc As New EncriptaCodigos.clsEncripta
        dt = obj.ListarEntregaDiploma(opcion, codigo, cod_test, texto)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim data As New Dictionary(Of String, Object)()
                data.Add("cod", obj.EncrytedString64(dt.Rows(i).Item("codigo_egr")))
                data.Add("nro_exp", dt.Rows(i).Item("NroExpediente_egr"))
                data.Add("cod_alu", obj.EncrytedString64(dt.Rows(i).Item("codigo_alu")))
                data.Add("cod_univer", dt.Rows(i).Item("codigoUniver_Alu"))
                data.Add("alu", dt.Rows(i).Item("Alumno"))
                data.Add("pes", dt.Rows(i).Item("descripcion_Pes"))
                data.Add("est", dt.Rows(i).Item("estado_egr"))
                data.Add("tipo_dip", dt.Rows(i).Item("TipoEmisionDiploma_egr"))
                data.Add("abrev_dip", dt.Rows(i).Item("abreviatura_tdg"))

                If opcion = "E" Then
                    data.Add("fec_cons", dt.Rows(i).Item("FechaAcuerdoConsUniver"))
                    data.Add("nro_res", dt.Rows(i).Item("NroResolucion_egr"))
                    data.Add("fec_res", dt.Rows(i).Item("FechaResolucion_egr"))
                    data.Add("tipodoc", dt.Rows(i).Item("tipodocident_Alu"))
                    data.Add("nrodoc", dt.Rows(i).Item("nrodocident_Alu"))
                    If dt.Rows(i).Item("codigo_gru") = "0" Then
                        data.Add("cod_gru", "")
                    Else
                        data.Add("cod_gru", obj.EncrytedString64(dt.Rows(i).Item("codigo_gru")))
                    End If
                    data.Add("nro_lib", dt.Rows(i).Item("Nrolibro_egr"))
                    data.Add("nro_fol", dt.Rows(i).Item("Nrofolio_egr"))
                    data.Add("nro_reg", dt.Rows(i).Item("nroRegistro_egr"))
                    data.Add("cod_fac", obj.EncrytedString64(dt.Rows(i).Item("CodigoFac_egr")))
                    data.Add("cod_cp", obj.EncrytedString64(dt.Rows(i).Item("codigo_cpf")))
                    data.Add("nom_cp", dt.Rows(i).Item("nombre_cpf"))
                    data.Add("cod_pes", obj.EncrytedString64(dt.Rows(i).Item("codigo_Pes")))
                    data.Add("cod_esp", obj.EncrytedString64(dt.Rows(i).Item("codigo_Esp")))
                    data.Add("cod_dgt", obj.EncrytedString64(dt.Rows(i).Item("codigo_dgt")))
                    data.Add("cod_tes", obj.EncrytedString64(dt.Rows(i).Item("codigo_tes")))
                    data.Add("nom_tes", dt.Rows(i).Item("titulo_tes"))
                    data.Add("fec_acto", dt.Rows(i).Item("FechaActoAcademico_egr"))
                    data.Add("cod_acto", obj.EncrytedString64(dt.Rows(i).Item("codigo_act")))
                    data.Add("mod_est", dt.Rows(i).Item("ModEstudio_egr"))
                    data.Add("obs", dt.Rows(i).Item("obervaciones_egr"))
                    data.Add("foto", obEnc.CodificaWeb("069" & dt.Rows(i).Item("codigouniver_Alu")))

                    data.Add("correo", dt.Rows(i).Item("EMAIL"))
                    data.Add("telmov", dt.Rows(i).Item("TELEF_MOV"))
                    data.Add("telfijo", dt.Rows(i).Item("TELEF_FIJO"))

                    data.Add("nrores_Fac", dt.Rows(i).Item("NroResolucionFac_egr"))
                    data.Add("fecres_Fac", dt.Rows(i).Item("FechaResolucionFac_egr"))


                    data.Add("apepat", dt.Rows(i).Item("apellidoPat_Alu"))
                    data.Add("apemat", dt.Rows(i).Item("apellidoMat_Alu"))
                    data.Add("nom", dt.Rows(i).Item("nombres_Alu"))
                    data.Add("archivo", dt.Rows(i).Item("archivo"))

                End If
                If opcion = "G" Then
                    data.Add("num_of", dt.Rows(i).Item("NroOficioRemision_egr"))
                End If
                If opcion = "LEE" Then
                    data.Add("entregado", dt.Rows(i).Item("entregado"))
                    data.Add("deno", dt.Rows(i).Item("descripcion_dgt"))
                    data.Add("cod_dta", dt.Rows(i).Item("codigo_dta"))
                    data.Add("estado_trl", dt.Rows(i).Item("estado_trl"))
                End If
                list.Add(data)
            Next
        End If
        JSONresult = serializer.Serialize(list)
        Response.Write(JSONresult)
    End Sub
    Private Sub ActualizarEntregaNew(ByVal cod As String, ByVal entregado As String, ByVal codigo_dta As Integer, ByVal codigo_tfu As Integer, ByVal usuario As Integer)
        Dim obj As New ClsGradosyTitulos
        Dim Data As New Dictionary(Of String, Object)()
        Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim JSONresult As String = ""
        Dim list As New List(Of Dictionary(Of String, Object))()
        Try
            Dim dt As New Data.DataTable
            dt = obj.ActualizarEntregaNew(cod, entregado, codigo_dta, codigo_tfu, usuario)
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
    '************************************ FIN OLLUEN 10/09/2020 ************************

End Class
