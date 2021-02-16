Imports Microsoft.VisualBasic
Imports System.Data
Public Class ClsGestionInvestigacion
    Private cnx As New ClsConectarDatos
    'JR - INICIO
    'Public Function ObtenerDatosPersonal(ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("INV_ListaCursosInvestigacionxDocente", codigo_per, ctf)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    'JR - FIN
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

    Public Function ListaPersonal(ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal texto_busqueda As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaPersonal", codigo_per, ctf, texto_busqueda)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaLineasInvestigacion(ByVal codigo_cpf As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaLineasInvestigacion", codigo_cpf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaTipoAutorProyecto(ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaTipoAutorProyecto", codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarProyecto(ByVal codigo_pro As Integer, ByVal codigo_tin As Integer, ByVal titulo As String, ByVal codigo_lin As Integer, ByVal codigo_dis As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal presupuesto As Decimal, ByVal archivopto As String, ByVal financiamiento As String, ByVal financia_Externo As String, ByVal avance As Decimal, ByVal estado_avance As String, ByVal informe As String, ByVal id As Integer, ByVal ctf As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarProyecto", codigo_pro, codigo_tin, titulo, codigo_lin, codigo_dis, fechaini, fechafin, presupuesto, archivopto, financiamiento, financia_Externo, avance, estado_avance, informe, id, ctf, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarObjetivo(ByVal codigo_obj As Integer, ByVal codigo_pro As Integer, ByVal descripcion As String, ByVal tipo As String, ByVal estado As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarObjetivo", codigo_obj, codigo_pro, descripcion, tipo, estado, id)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarAutorProyecto(ByVal codigo_aut As Integer, ByVal codigo_pro As Integer, ByVal codigo_alu As Integer, ByVal codigo_per As Integer, ByVal codigo_tip As Integer, ByVal estado As Integer, ByVal id As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarAutorProyecto", codigo_aut, codigo_pro, codigo_alu, codigo_per, codigo_tip, estado, id, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarProyecto(ByVal tipo As String, ByVal codigo_pro As Integer, ByVal codigo_per As Integer, ByVal codigo_alu As Integer, ByVal estado As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarProyecto", tipo, codigo_pro, codigo_per, codigo_alu, estado, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObjetivos(ByVal codigo_pro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarObjetivos", codigo_pro)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarAutorProyecto(ByVal codigo_pro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarAutorProyecto", codigo_pro)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarArchivosdeProyecto(ByVal codigo_pro As Integer, ByVal ruta As String, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarArchivosProyecto", codigo_pro, ruta, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarInstanciaEstado(ByVal codigo_pro As Integer, ByVal veredicto As Integer, ByVal observacion As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarInstanciaEstadoProyecto", codigo_pro, veredicto, observacion, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function CargarFiltroEstado(ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_CargaFiltroEstado", id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarObservaciones(ByVal codigo_pro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListaObservaciones", codigo_pro)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarProyecto(ByVal codigo_pro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_EliminarProyecto", codigo_pro)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarConcurso(ByVal tipo As String, ByVal codigo_con As String, ByVal texto As String, ByVal estado As String, ByVal dirigidoa As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarConcurso", tipo, codigo_con, texto, estado, dirigidoa, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarConcurso(ByVal codigo_con As Integer, ByVal titulo As String, ByVal descripcion As String, ByVal ambito As String, ByVal fechaini As String, ByVal fechafin As String, ByVal fechafinevaluacion As String, ByVal fecharesultados As String, ByVal tipo As Integer, ByVal diridigoa As Integer, ByVal innovacion As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarConcurso", codigo_con, titulo, descripcion, ambito, fechaini, fechafin, fechafinevaluacion, fecharesultados, tipo, diridigoa, innovacion, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarArchivosdeConcurso(ByVal codigo_con As Integer, ByVal ruta As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarArchivosConcurso", codigo_con, ruta)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarConcurso(ByVal codigo As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_EliminarConcurso", codigo, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarDepartamentos() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_listarDepartamentos")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarInvestigadorGrupos(ByVal tipo As String, ByVal codigo As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarInvestigadorGrupos", tipo, codigo, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDatosDocenteInvestigador(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultaDatosDocenteInvestigador", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarPostulacion(ByVal codigo_pos As Integer, ByVal codigo_con As Integer, ByVal codigo_per As Integer, ByVal codigo_gru As Integer, ByVal codigo_alu As Integer, ByVal titulo As String, ByVal codigo_doc As Integer, ByVal codigo_linea As Integer, ByVal codigo_dis As Integer, ByVal codigo_region As Integer, ByVal codigo_distrito As Integer, ByVal lugar As String, ByVal resumen As String, ByVal palabras As String, ByVal justificacion As String, ByVal fechaini As String, ByVal fechafin As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarPostulacion", codigo_pos, codigo_con, codigo_per, codigo_gru, codigo_alu, titulo, codigo_doc, codigo_linea, codigo_dis, codigo_region, codigo_distrito, lugar, resumen, palabras, justificacion, fechaini, fechafin, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarObjetivoPostulacion(ByVal codigo_obj As Integer, ByVal codigo_pos As Integer, ByVal descripcion As String, ByVal tipo As String, ByVal estado As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarObjetivoPostulacion", codigo_obj, codigo_pos, descripcion, tipo, estado, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEquipoPostulacion(ByVal codigo_pos As Integer, ByVal codigo_inv As Integer, ByVal codigo_per As Integer, ByVal codigo_alu As Integer, ByVal codigo_rol As Integer, ByVal dedicacion As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarEquipoPostulacion", codigo_pos, codigo_inv, codigo_per, codigo_alu, codigo_rol, dedicacion, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarArchivosdePostulacion(ByVal codigo_pos As Integer, ByVal ruta As String, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarArchivosPostulacion", codigo_pos, ruta, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarPostulacion(ByVal tipo As String, ByVal codigo_pos As Integer, ByVal codigo_con As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarPostulacion", tipo, codigo_pos, codigo_con, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarObjetivosPostulacion(ByVal codigo_pos As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarObjetivosPostulacion", codigo_pos)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarEquipoPostulacion(ByVal codigo_pos As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarEquipoPostulacion", codigo_pos)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarEtapaPostulacion(ByVal codigo_pos As Integer, ByVal etapa As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarEtapaPostulacion", codigo_pos, etapa, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarEvaluadoresPostulacion(ByVal codigo_pos As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarEvaluadoresPostulacion", codigo_pos)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function AsignarEvaluador(ByVal codigo_pos As Integer, ByVal codigo_Eva As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_AsignarEvaluadorExterno", codigo_pos, codigo_Eva, id)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function QuitarEvaluador(ByVal codigo_Eva As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_QuitarEvaluador", codigo_Eva, id)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListarAlumnosTesis(ByVal texto As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarAlumnosTesis", texto)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaRolInvestigador(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_listarRolInvestigador", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaProvincias(ByVal tipo As String, ByVal codigo_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarProvincia", tipo, codigo_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDistritos(ByVal tipo As String, ByVal codigo_prov As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPla_ConsultarDistrito", tipo, codigo_prov)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EnviarMailProyecto(ByVal codigo_pro As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_DatosEmailProyecto", codigo_pro)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarIDArchivoCompartido(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarIDArchivoCompartido", idtabla, idtransaccion, idoperacion)
        cnx.CerrarConexion()
        Return dts
    End Function

    '*******************************************************************************************************************
    '============================================ [ T E S I S ] ========================================================
    '*******************************************************************************************************************

    Public Function ConsultarCicloAcademico(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'dts = cnx.TraerDataTable("ConsultarCicloAcademico", tipo, param1) ' 05/02/2020 Vanessa Castro solicita que tambien figuren los Ciclos 0
        dts = cnx.TraerDataTable("ConsultarCicloAcademico", "TO", param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCarreraProfesionalTesisxDocente(ByVal codigo_Cac As Integer, ByVal etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarCarreraProfesionalTesisxDocente", codigo_Cac, etapa, codigo_per, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCursosTesisxDocente(ByVal codigo_Cac As Integer, ByVal etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal codigo_cpf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarCursosTesisxDocente", codigo_Cac, etapa, codigo_per, ctf, codigo_cpf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarAlumnosxCurso(ByVal codigo_Cup As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarAlumnosxCursoTesis", codigo_Cup)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ConsultarTipoinvestigacion(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarTipoInvestigacion", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarTipoParticipante(ByVal tipo As String, ByVal param1 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarTipoParticipante", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function Consultartesis(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String, ByVal param3 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarTesis", tipo, param1, param2, param3)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarTesis(ByVal codigo_tes As Integer, ByVal codigo_tin As Integer, ByVal titulo As String, ByVal problema As String, ByVal resumen As String, ByVal codigo_lin As Integer, ByVal codigo_dis_ocde As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal codigo_eti As Integer, ByVal codigo_per As Integer, ByVal presupuesto As Decimal, ByVal avance As Decimal, ByVal financiamiento As String, ByVal financiamientousat As String, ByVal financiaexterno As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarTesis", codigo_tes, codigo_tin, titulo, problema, resumen, codigo_lin, codigo_dis_ocde, fechaini, fechafin, codigo_eti, codigo_per, presupuesto, avance, financiamiento, financiamientousat, financiaexterno)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarTesis(ByVal codigo_tes As Integer, ByVal codigo_tin As Integer, ByVal titulo As String, ByVal problema As String, ByVal resumen As String, ByVal codigo_lin As Integer, ByVal codigo_dis_ocde As Integer, ByVal fechaini As String, ByVal fechafin As String, ByVal codigo_eti As Integer, ByVal codigo_per As Integer, ByVal presupuesto As Decimal, ByVal avance As Decimal, ByVal financiamiento As String, ByVal financiamientousat As String, ByVal financiaexterno As String, ByVal FechaSustentacionP As String, ByVal NotaSustentacionP As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarTesis", codigo_tes, codigo_tin, titulo, problema, resumen, codigo_lin, codigo_dis_ocde, fechaini, fechafin, codigo_eti, codigo_per, presupuesto, avance, financiamiento, financiamientousat, financiaexterno, FechaSustentacionP, NotaSustentacionP)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AsignarDatosSustentacionInforme(ByVal codigo_tes As Integer, ByVal nota As String, ByVal fechaActa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_AsignarDatosSustentacionInforme", codigo_tes, nota, fechaActa)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function EnviarTesis(ByVal codigo_tes As Integer, ByVal usuario As Integer, ByVal codigo_alu As Integer, ByVal tipo As Integer, ByVal etapa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_EnviarTesis", codigo_tes, usuario, codigo_alu, tipo, etapa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAutorTesis(ByVal codigo_tes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarAutor", codigo_tes)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarAutorTesis(ByVal codigo_rtes As Integer, ByVal codigo_tes As Integer, ByVal codigo_alu As Integer, ByVal codigo_tpi As Integer, ByVal estado_rtes As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarAutorTesis", codigo_rtes, codigo_tes, codigo_alu, codigo_tpi, estado_rtes, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObjetivosTesis(ByVal codigo_tes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarObjetivos", codigo_tes)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarObjetivoTesis(ByVal codigo_obj As Integer, ByVal codigo_tes As Integer, ByVal descripcion As String, ByVal tipo As String, ByVal estado As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarObjetivoTesis", codigo_obj, codigo_tes, descripcion, tipo, estado, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarParticipantesTesis(ByVal codigo_tes As Integer, ByVal tipo_tpi As String, ByVal abreviatura_etapa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarParticipantesTesis", codigo_tes, tipo_tpi, abreviatura_etapa)
        cnx.CerrarConexion()
        Return dts
    End Function
    ' ---- REGISTRAR ASESOR Y JURADOS
    Public Function ActualizarAsesorTesis(ByVal codigo_jur As Integer, ByVal codigo_tes As Integer, ByVal codigo_per As Integer, ByVal abreviatura_etapa As String, ByVal codigo_tpi As Integer, ByVal estado_jur As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarAsesorTesis", codigo_jur, codigo_tes, codigo_per, abreviatura_etapa, codigo_tpi, estado_jur, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDepartamentosAcademicos(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarDepartamentoAcademico", tipo, param1, param2)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonalxDepartamentoAcademico(ByVal codigo_dac As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_PersonalDepartamentoAcademico", codigo_dac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonalxAdscripcion(ByVal tipo As String, ByVal codigo_dac As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_PersonalAdscripcionDocente", tipo, codigo_dac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarArchivosTesis(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal idoperacion As String, ByVal etapa As String, ByVal usuario_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarIDArchivoCompartidoTesis", idtabla, idtransaccion, idoperacion, etapa, usuario_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RegistrarAsesorTesis(ByVal codigo_tes As Integer, ByVal codigo_per As Integer, ByVal abreviatura_etapa As String, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_RegistrarAsesorTesis", codigo_tes, codigo_per, abreviatura_etapa, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCarreraJurado(ByVal codigo_Cac As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer, ByVal abreviatura_etapa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarCarreraJurado", codigo_Cac, codigo_per, ctf, abreviatura_etapa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarEvaluacionJuradoTesis(ByVal codigo_Cac As Integer, ByVal codigo_per As Integer, ByVal codigo_cpf As Integer, ByVal ctf As Integer, ByVal abreviatura_etapa As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarEvaluacionJuradoTesis", codigo_Cac, codigo_per, codigo_cpf, ctf, abreviatura_etapa)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarRolJurado(ByVal codigo_tes As Integer, ByVal codigo_jur As Integer, ByVal codigo_per As Integer, ByVal codigo_tpi As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarRolJurado", codigo_tes, codigo_jur, codigo_per, codigo_tpi)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AprobarJurado(ByVal codigos_jurado As String, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_AprobarJurado", codigos_jurado, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarDocenteAsesorTesis(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarDocenteAsesorTesis", codigo_cac, abrev_etapa, codigo_per, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAsesorias(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer, ByVal usuario As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarAsesorias", codigo_cac, abrev_etapa, codigo_per, usuario, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CalificacionAsesor(ByVal codigo_tes As Integer, ByVal codigo_cac As Integer, ByVal codigo_rtes As Integer, ByVal codigo_eti As Integer, ByVal porcentaje As String, ByVal nota As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_CalificacionAsesor", codigo_tes, codigo_cac, codigo_rtes, codigo_eti, porcentaje, nota, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarDocenteJuradoTesis(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarDocenteJuradoTesis", codigo_cac, abrev_etapa, codigo_per, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarTesisJurado(ByVal codigo_cac As Integer, ByVal abrev_etapa As String, ByVal codigo_per As Integer, ByVal usuario As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarTesisJurado", codigo_cac, abrev_etapa, codigo_per, usuario, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObservacionesJurado(ByVal codigo_tes As Integer, ByVal codigo_jur As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarObservaciones", codigo_tes, codigo_jur)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarObservacionTesis(ByVal codigo_obs As Integer, ByVal codigo_tes As Integer, ByVal codigo_jur As Integer, ByVal codigo_cac As Integer, ByVal nota As Integer, ByVal tipo_obs As Integer, ByVal descripcion_obs As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_AgregarObservacionTesis", codigo_obs, codigo_tes, codigo_jur, codigo_cac, nota, tipo_obs, descripcion_obs)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarObservacionTesis(ByVal codigo_dot As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_EliminarObservacionTesis", codigo_dot)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarNotaObservacionTesis(ByVal codigo_obs As Integer, ByVal codigo_tes As Integer, ByVal codigo_jur As Integer, ByVal codigo_cac As Integer, ByVal nota As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_AgregarNotaObservacionTesis", codigo_obs, codigo_tes, codigo_jur, codigo_cac, nota)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RegistrarObservacionDocente(ByVal codigo_tes As Integer, ByVal codigo_per As Integer, ByVal codigo_alu As Integer, ByVal descripcion_obs As String, ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_GuardarObservacionDocente", codigo_tes, codigo_per, codigo_alu, descripcion_obs, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarObservacionDocente(ByVal codigo_tes As Integer, ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarObservacionDocente", codigo_tes, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarObservacionDocente(ByVal codigo_odt As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_EliminarObservacionDocente", codigo_odt)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function InsertaEnvioCorreosMasivo(ByVal codigo_odt As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("InsertaEnvioCorreosMasivo", codigo_odt)
        cnx.CerrarConexion()
        Return dts
    End Function
    '=================================== INVESTIGADOR ==============================================
    Public Function ConsultarPersonalInvestigador(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultarPersonalInvestigador", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarInvestigador(ByVal codigo_inv As Integer, ByVal codigo_per As Integer, ByVal codigo_linea As Integer, ByVal codigo_dis As Integer, ByVal regina As String, ByVal dina As String, ByVal orcid As String, ByVal scopusid As String, ByVal reseracherID As String, ByVal usuario_Reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarRegistroInvestigador", codigo_inv, codigo_per, codigo_linea, codigo_dis, regina, dina, orcid, scopusid, reseracherID, usuario_Reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAreasConocimientoOCDE(ByVal codigo As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_listaAreasConocimientoOCDE", codigo, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Function ListarObservacionesInvestigador(ByVal codigo As Integer, ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_VisualizarHistorialGI", codigo, tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarBaseDeDatosRevista() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ConsultarBaseDatosRevista")
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListarAreasOCDExLineaUSAT(ByVal codigo_lin As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarAreasOcdexLineaUsat", codigo_lin)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarLinkInforme(ByVal codigo_tes As Integer, ByVal link As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarLinkInforme", codigo_tes, link)
        cnx.CerrarConexion()
        Return dts
    End Function

    '=============================== SUSTENTACIÓN DE TESIS =============================================================

    Public Function ConsultarTipoEstudio(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ACAD_ConsultarTipoEstudio", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCarrerasxTipoEstudio(ByVal codigo_test As Integer, ByVal id As Integer, ByVal ctf As Integer, ByVal codigo_apl As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultaCarreraxAccesoRecurso", codigo_test, id, ctf, codigo_apl)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarAlumnosSustentacion(ByVal codigo_cpf As Integer, ByVal estado As String, ByVal textoBusqueda As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarAlumnosSustentacion", codigo_cpf, estado, textoBusqueda)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarTesisSustentacion(ByVal codigo_alu As Integer, ByVal codigo_tes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarTesisSustentacion", codigo_alu, codigo_tes)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarJuradosTesisxEtapa(ByVal codigo_Tes As Integer, ByVal codigo_alu As Integer, ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_JuradosTesisxEtapa", codigo_Tes, codigo_alu, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function EliminarJuradoTesis(ByVal codigo_jur As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_EliminarJurado", codigo_jur, id)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function DatosActaSustentacion(ByVal codigo_Tes As Integer, ByVal codigo_alu As Integer, ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_DatosActaSustentacion", codigo_Tes, codigo_alu, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function DatosResolucionJurado(ByVal codigo_Tes As Integer, ByVal codigo_alu As Integer, ByVal abreviatura As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_DatosResolucion", codigo_Tes, codigo_alu, abreviatura)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAmbientes() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarAmbientes")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarEvaluarJuradoSustentacion(ByVal textobusqueda As String, ByVal codigo_cpf As Integer, ByVal usuario As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarEvaluacionJuradoSustentacionTesis", textobusqueda, codigo_cpf, usuario, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarTesisxCodigo(ByVal codigo_Tes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarTesisxCodigo", codigo_Tes)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function GuardarAsesoriaTesisVirtual(ByVal codigo_Tes As Integer, ByVal codigo_rtes As Integer, ByVal codigo_eti As Integer, ByVal observacion As String, ByVal idarchivo As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_GuardarAsesoriaTesisVirtual", codigo_Tes, codigo_rtes, codigo_eti, observacion, idarchivo, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAsesoriaTesisVirtual(ByVal codigo_Tes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ListarAsesoriaTesisVirtual", codigo_Tes)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarIDArchivoCompartidoAsesoria(ByVal idtabla As Integer, ByVal codigo_oat As Integer, ByVal nro_operacion As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarIDArchivoCompartidoAsesoria", idtabla, codigo_oat, nro_operacion)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function GuardarRespuestaAsesoria(ByVal codigo_atv As Integer, ByVal descripcion As String, ByVal codigo_rtes As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_GuardarRespuestaAsesoria", codigo_atv, descripcion, codigo_rtes)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarCompromisoAsesor(ByVal codigo_Rtes As Integer, ByVal codigo_Cac As Integer, ByVal codigo_dot As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ActualizarCompromisoAsesor", codigo_Rtes, codigo_Cac, codigo_dot, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AnularNotaAsesor(ByVal codigo_tes As Integer, ByVal codigo_Cac As Integer, ByVal etapa As String, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_AnularNotaAsesor", codigo_tes, codigo_Cac, etapa, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class
