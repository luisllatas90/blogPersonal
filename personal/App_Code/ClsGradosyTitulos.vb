
Imports Microsoft.VisualBasic
Imports System.Data

Public Class ClsGradosyTitulos
    Private cnx As New ClsConectarDatos

    Public Function EncrytedString64(ByVal _stringToEncrypt As String) As String
        Dim result As String = ""
        Dim encryted As Byte()
        encryted = System.Text.Encoding.Unicode.GetBytes(_stringToEncrypt)
        result = Convert.ToBase64String(encryted)
        Return result
    End Function

    Public Function DecrytedString64(ByVal _stringToDecrypt As String) As String
        Dim result As String = ""
        Dim decryted As Byte()
        decryted = Convert.FromBase64String(_stringToDecrypt)
        result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.Length)
        Return result
    End Function

    Public Function BuscaAlumno(ByVal opcion As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarAlumno", opcion, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarAlumno(ByVal codigo_alu As Integer, ByVal parametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarAlumno", codigo_alu, parametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarFacultad(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarFacultad", tipo, param)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarActoAcademico(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarActoAcademico", tipo, param)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarEspecialidades(ByVal tipo As String, ByVal cod_pes As Integer, ByVal vigencia As String, ByVal param3 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarEspecialidad", tipo, cod_pes, vigencia, param3)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarGrados(ByVal tipo As String, ByVal cod_cpf As Integer, ByVal vigencia As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarGrados", tipo, cod_cpf, vigencia)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarTipoEstudio(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ACAD_ConsultarTipoEstudio", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPlanEstudioxCarrera(ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("Alumni_ListarPlanEstudiosxCarrera", codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCarrerasxTest(ByVal tipo As String, ByVal parametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCarreraProfesional", tipo, parametro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDenominaciones(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarDenominacion", tipo, param1, param2, param3)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarEspecialidad(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarEspecialidad", tipo, param1, param2, param3)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarDenominacion(ByVal codigo_dgt As Integer, ByVal codigo_cpf As String, ByVal tipo_dgt As Integer, ByVal descripcion As String, _
                                 ByVal vigencia As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarDenominacion", codigo_dgt, codigo_cpf, tipo_dgt, descripcion, vigencia, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEspecialidad(ByVal codigo_esp As Integer, ByVal codigo_pes As String, ByVal descripcion As String, ByVal abreviatura As String, _
                             ByVal vigencia As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarEspecialidad", codigo_esp, codigo_pes, descripcion, abreviatura, vigencia, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonal(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPersonal", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCargo(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCargo", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarTipoDenominacion(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarTipoDenominacion", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPrefijo(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarPrefijo", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarAutoridad(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarAutoridad", tipo, param1, param2, param3)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarConfiguracionCargo(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarConfiguracionCargo", tipo, param1, param2)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarConfiguracionCargo(ByVal codigo_ccp As Integer, ByVal codigo_cgo As Integer, ByVal codigo_pre As Integer, ByVal codigo_per As Integer, ByVal codigo_fac As Integer, _
                                 ByVal orden As Integer, ByVal encargado As String, ByVal vigencia As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarConfiguracionCargo", codigo_ccp, codigo_cgo, codigo_pre, codigo_per, codigo_fac, orden, encargado, vigencia, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ActualizarEgresado(ByVal codigo_egr As Integer, ByVal nro_expediente As String, ByVal codigo_alu As Integer, ByVal codigo_dgt As Integer, ByVal codigo_act As Integer, _
                             ByVal fecha_act As String, ByVal fecha_consejo As String, ByVal nro_res As String, ByVal fecha_res As String, _
                              ByVal codigo_gru As Integer, ByVal nrolibro As String, ByVal nrofolio As String, ByVal nroregistro As String, ByVal codigo_fac As Integer, ByVal codigo_esp As Integer, _
                              ByVal modalidad_estudio As String, ByVal tipo_emision As String, ByVal observaciones As String, ByVal autoridad1 As Integer, ByVal autoridad2 As Integer, ByVal autoridad3 As Integer, _
                              ByVal estado As Integer, ByVal usuario As Integer, ByVal codigo_tes As Integer, ByVal titulo_tesis As String, ByVal nrores_Fac As String, ByVal fechares_fac As String) As Data.DataTable

        ', ByVal fecha_consejo As String, ByVal nro_resolucion As String, ByVal fecha_resolucion As String, ByVal fecha_diploma As String, _
        'ByVal nrolibro As String, ByVal nrofolio As Integer, ByVal codigo_gru As Integer
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'dts = cnx.TraerDataTable("GYT_ActualizarEgresado", codigo_egr, nro_expediente, codigo_alu, codigo_dgt, codigo_act, fecha_act, fecha_consejo, nro_res, fecha_res, codigo_gru, nrolibro, nrofolio, nroregistro, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, autoridad1, autoridad2, autoridad3, estado, usuario)
        dts = cnx.TraerDataTable("GYT_ActualizarEgresado", codigo_egr, nro_expediente, codigo_alu, codigo_dgt, codigo_act, fecha_act, codigo_gru, nrolibro, nrofolio, nroregistro, codigo_fac, codigo_esp, modalidad_estudio, tipo_emision, observaciones, autoridad1, autoridad2, autoridad3, estado, usuario, codigo_tes, titulo_tesis, nrores_Fac, fechares_fac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEgresado(ByVal tipo As String, ByVal codigo As String, ByVal param1 As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarEgresado", tipo, codigo, param1, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaSesionConsejoU(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaSesionConsejoUniversitario", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function Actualizar_sesion(ByVal codigo As Integer, ByVal fecha As String, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarSesion", codigo, fecha, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function MoverAlumno(ByVal codigo As String, ByVal codigo_Sesion As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_MoverEgresadoSesion", tipo, codigo, codigo_Sesion)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarExpResolucion(ByVal tipo As String, ByVal codigo_scu As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaExpedientesResolucion", tipo, codigo_scu, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function Actualizar_Resolucion(ByVal codigo As String, ByVal nro_resol As String, ByVal fecha_resol As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarResolucion", codigo, nro_resol, fecha_resol)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function Actualizar_Oficio(ByVal codigo As String, ByVal nro_oficio As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarOficio", codigo, nro_oficio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDatosAlumno(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaSesionConsejoUniversitario", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarGrupoEgresado(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarGrupoEgresado", tipo, param)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCargosxTest(ByVal codigo_Test As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_CargosxTest", codigo_Test)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarDenominacionesxSesion(ByVal codigo_sesion As Integer, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaDenominacionesxSesionConsejo", codigo_sesion, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    '================================== PARA MANTENIMIENTO DE TESIS BIBLIOTECA =======================================================
    Public Function ListarTesis(ByVal tipo As String, ByVal anio As String, ByVal titulo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarTesis", tipo, anio, titulo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarTesis(ByVal codigo_tes As Integer, ByVal titulo_Tes As String, ByVal url As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarTesis", codigo_tes, titulo_Tes, url, param1)
        cnx.CerrarConexion()
        Return dts
    End Function
    '=========================================================================================================

    Public Function ActualizarEntrega(ByVal codigo As Integer, ByVal entregado As String, ByVal codigo_dta As Integer, ByVal codigo_tfu As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarEntregaDiploma", codigo, entregado, codigo_dta, codigo_tfu, id)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ActualizarDatosContacto(ByVal codigo As String, ByVal apepat As String, ByVal apemat As String, ByVal nombres As String, ByVal email As String, ByVal telefono_movil As String, ByVal telefono_fijo As String, ByVal cod_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarDatosContacto", codigo, apepat, apemat, nombres, email, telefono_movil, telefono_fijo, cod_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEgresadosAsignaCorrelativo(ByVal codigo_scu As String, ByVal codigo_test As String, ByVal codigo_cpf As String, ByVal texto As String, ByVal codigo_tdg As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaEgresadosAsignaCorrelativo", codigo_scu, codigo_test, codigo_cpf, texto, codigo_tdg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ObtenerCorrelativos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ObtenerCorrelativos")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function GenerarCorrelativos(ByVal cod As String, ByVal nro_dip As String, ByVal libro_B As String, ByVal folio_b As String, ByVal libro_t As String, ByVal folio_t As String, ByVal libro_m As String, ByVal folio_m As String, ByVal libro_d As String, ByVal folio_d As String, ByVal libro_s As String, ByVal folio_s As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_GenerarCorrelativos", cod, nro_dip, libro_B, folio_b, libro_t, folio_t, libro_m, folio_m, libro_d, folio_d, libro_s, folio_s)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function QuitarCorrelativos(ByVal cod As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_QuitarCorrelativos", cod)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaEgresadosAsignaGrupo(ByVal codigo_scu As String, ByVal codigo_test As String, ByVal codigo_cpf As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ListaEgresadosAsignaGrupo", codigo_scu, codigo_test, codigo_cpf, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function Actualizar_Grupo(ByVal codigo As String, ByVal cod_grupo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizaGrupoEgresado", codigo, cod_grupo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaFechaActo(ByVal codigo_alu As Integer, ByVal codigo_dgt As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultaActoEgresado", codigo_alu, codigo_dgt)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCarreraxSesion(ByVal codigo_scu As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarCarreraProfesionalxSesion", codigo_Scu)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAlumnosAutorizarTramite(ByVal codigo_scu As String, ByVal codigo_cpf As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarAlumnosAutorizarTramiteTitulo", codigo_scu, codigo_cpf, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AutorizarTramiteTitulo(ByVal codigos_Egr As String, ByVal codigosQuitarCheck As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_AutorizarTramiteTitulo", codigos_Egr, codigosQuitarCheck)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarEgresadoHojaRegistro(ByVal tipo As String, ByVal texto As String, ByVal param1 As String, ByVal param2 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarEgresado", tipo, texto, param1, param2)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarInformarInscripcionSUNEDU(ByVal codigo_sesion As String, ByVal codigo_tdg As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarAlumnosInscripcionSUNEDU", codigo_sesion, codigo_tdg, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEstadoInformarInscripcionSUNEDU(ByVal codigos As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarEstadoEnvioCorreoInscripcionSUNEDU", codigos)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarEntregaDiplomas(ByVal codigo_sesion As String, ByVal codigo_tdg As String, ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ConsultarEntregaDiplomas", codigo_sesion, codigo_tdg, texto)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEstadoEnvioEntregaDiploma(ByVal codigos As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("GYT_ActualizarEstadoEnvioEntregaDiploma", codigos)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEstadosTramite(ByVal tipo As String, ByVal codigo_dta As Integer, ByVal codigo_dre As Integer, ByVal codigo_dft As Integer, ByVal cumple_requisito As Integer, ByVal estado_dft As String, ByVal codigo_per As Integer) As Boolean
        Dim dts As New Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.Ejecutar("TRL_TramiteRequisito_Registrar", tipo, codigo_dta, codigo_dre, codigo_dft, cumple_requisito, estado_dft, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class
