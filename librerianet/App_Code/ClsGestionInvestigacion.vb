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
    Public Function ListarConcurso(ByVal tipo As String, ByVal codigo_con As Integer, ByVal texto As String, ByVal estado As String, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ListarConcurso", tipo, codigo_con, texto, estado, id, ctf)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarConcurso(ByVal codigo_con As Integer, ByVal titulo As String, ByVal descripcion As String, ByVal fechaini As String, ByVal fechafin As String, ByVal fechafinevaluacion As String, ByVal fecharesultados As String, ByVal tipo As Integer, ByVal id As Integer, ByVal ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarConcurso", codigo_con, titulo, descripcion, fechaini, fechafin, fechafinevaluacion, fecharesultados, tipo, id, ctf)
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
    Public Function EliminarConcurso(ByVal codigo As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_EliminarConcurso", codigo)
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

    Public Function ActualizarEquipoPostulacion(ByVal codigo_pos As Integer, ByVal codigo_inv As Integer, ByVal codigo_alu As Integer, ByVal codigo_rol As Integer, ByVal dedicacion As Integer, ByVal id As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_ActualizarEquipoPostulacion", codigo_pos, codigo_inv, codigo_alu, codigo_rol, dedicacion, id)
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


    Public Function ListaRolInvestigador() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("INV_listarRolInvestigador")
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

End Class
