Imports Microsoft.VisualBasic
Imports System.Data
Imports System
Imports System.Configuration

Public Class clsTutoria
    Private cnx As New ClsConectarDatos

    Public Function ListaTutores(ByVal tipo As String, ByVal codigo_cac As String, ByVal codigo_tc As String, ByVal codigo_ctf As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaTutores", tipo, codigo_cac, codigo_tc, codigo_ctf)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaPersonal(ByVal tipo As String, ByVal codigo_per As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaPersonal", tipo, codigo_per)
        cnx.CerrarConexion()
        Return dt
    End Function

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
    Public Function ActualizarTutor(ByVal codigo_tc As Integer, ByVal codigo_cac As Integer, ByVal codigo_per As Integer, ByVal fecha_ini As String, ByVal fecha_fin As String, ByVal estado As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTutor", codigo_tc, codigo_cac, codigo_per, fecha_ini, fecha_fin, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarTutor(ByVal codigo_tc As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTutor", codigo_tc)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function PoblacionObjetivo(ByVal tipo As String, ByVal codigo_cac As Integer, ByVal categoria As String, ByVal escuela As String, ByVal codigo_caI As Integer, ByVal riesgo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_PoblacionObjetivo", tipo, codigo_cac, categoria, escuela, codigo_caI, riesgo)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function RegistrarTutorAlumno(ByVal codigo_tc As Integer, ByVal codigo_alu As Integer, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal categoria As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_RegistrarTutorAlumno", codigo_tc, codigo_alu, estado, usuario_reg, categoria)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function RegistrarTutorAlumnoFiltros(ByVal codigo_tc As Integer, ByVal usuario_reg As Integer, ByVal codigo_cac As Integer, ByVal categoria As String, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal riesgo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_RegistrarTutorAlumnoMasivo", codigo_tc, usuario_reg, codigo_cac, categoria, codigo_cpf, codigo_cai, riesgo)
        cnx.CerrarConexion()
        Return dts
    End Function
    'Public Function ListaCicloAcademico(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("ConsultarCicloAcademico", tipo, codigo)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function
    Public Function EliminarTutorAlumno(ByVal codigo_tua As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTutorAlumno", codigo_tua)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AtenderTutorAlumno(ByVal codigo_tua As Integer, ByVal estado As Boolean) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_AtenderTutorAlumno", codigo_tua, estado)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListaTutorados(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_cac As Integer, ByVal codigo_ctf As Integer, Optional ByVal tipo_ses As Integer = 0, Optional ByVal codigo_cpf As Integer = 0, Optional ByVal codigo_cai As Integer = 0, Optional ByVal categoria As String = "") As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaTutorados", tipo, codigo, codigo_cac, tipo_ses, codigo_cpf, codigo_cai, categoria, codigo_ctf)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaTipoEvaluacion(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaTipoEvaluacion", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaVariableTipoEvaluacion(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaVariable_TipoEval", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function RegistrarEvaluacionEntrada(ByVal codigo_tua As Integer, ByVal estado_eval As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_RegistrarEvaluacionEntrada", codigo_tua, estado_eval, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function RegistrarDetalleEvaluacionEntrada(ByVal codigo_alu As Integer, ByVal codigo_vt As Integer, ByVal puntaje As Double, ByVal estado_deva As Integer, ByVal usuario_reg As Integer, ByVal codigo_ov As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_RegistrarDetalleEvaluacionEntrada", codigo_alu, codigo_vt, puntaje, estado_deva, usuario_reg, codigo_ov)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ModificarDetalleEvaluacionEntrada(ByVal codigo_tua As Integer, ByVal codigo_vt As Integer, ByVal puntaje As Double, ByVal estado_deva As Integer, ByVal usuario_reg As Integer, ByVal codigo_ov As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ModificarDetalleEvaluacionEntrada", codigo_tua, codigo_vt, puntaje, estado_deva, usuario_reg, codigo_ov)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaCicloAcademico(ByVal tipo As String, ByVal codigo As String, ByVal codigo_ctf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ListaCicloAcademico", tipo, codigo, codigo_ctf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaDetalleEvaluacion(ByVal tipo As String, ByVal codigo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ListaDetalleEvaluacion", tipo, codigo)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function InactivarDetalleEvaluacion(ByVal codigo_eval As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_InactivarDetalleEvaluacion", codigo_eval, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaTipoSesion(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_ctf As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaTipoSesion", tipo, codigo, codigo_ctf)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ActualizarSesión(ByVal codigo_stu As Integer, ByVal codigo_per As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal descripcion_stu As String, ByVal fecha_stu As String, ByVal horaInicio_stu As String, ByVal horaFin_stu As String, ByVal estado As Integer, ByVal usuario_reg As Integer, ByVal codigo_cur As Integer, ByVal codigo_cpf As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarSesion", codigo_stu, codigo_per, codigo_cac, codigo_tis, descripcion_stu, fecha_stu, horaInicio_stu, horaFin_stu, estado, usuario_reg, codigo_cur, codigo_cpf)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarSesiónAlumno(ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_stua As Integer, ByVal estado As Integer, ByVal asistencia As String, ByVal obs As String, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarSesionAlumno", codigo_stu, codigo_tua, codigo_stua, estado, asistencia, obs, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarSesiónAlumnoIndividual(ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_stua As Integer, ByVal estado As Integer, ByVal asistencia As String, ByVal obs As String, ByVal accion_stua As String, ByVal codigo_tat As Integer, ByVal codigo_tre As Integer, ByVal problemas_stua As String, ByVal codigo_nrt As Integer, ByVal codigo_etu As Integer, ByVal descripcionInc_stua As String, ByVal comentarioTutor_stua As String, ByVal fechaEjecucion_stua As String, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarSesionAlumnoIndividual", codigo_stu, codigo_tua, codigo_stua, estado, asistencia, obs, accion_stua, codigo_tat, codigo_tre, problemas_stua, codigo_nrt, codigo_etu, descripcionInc_stua, comentarioTutor_stua, fechaEjecucion_stua, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaSesiones(ByVal tipo As String, ByVal codigo_ctf As Integer, ByVal codigo_stu As Integer, ByVal codigo_per As Integer, ByVal codigo_cac As Integer, ByVal codigo_tis As Integer, ByVal codigo_tua As Integer, ByVal tipo_rango As String, ByVal rango As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaSesiones", tipo, codigo_ctf, codigo_stu, codigo_per, codigo_cac, codigo_tis, codigo_tua, tipo_rango, rango)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaSesionesAlumnos(ByVal tipo As String, ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_per As Integer, ByVal codigo_ctf As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaSesionesAlumnoTutoria", tipo, codigo_stu, codigo_tua, codigo_per, codigo_ctf)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaSesionesAlumnosIndividual(ByVal tipo As String, ByVal codigo_stu As Integer, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaSesionesIndividual", tipo, codigo_stu, codigo_tua, codigo_tc)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ModificarSesionAlumno(ByVal codigo_stua As Integer, ByVal asistencia_stua As String, ByVal observacion_stua As String, ByVal estado As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ModificarSesionAlumno", codigo_stua, asistencia_stua, observacion_stua, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ResetSesionesAlumno(ByVal codigo_stu As Integer, ByVal codigo_per As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ResetSesionesAlumno", codigo_stu, codigo_per, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarSesionTutor(ByVal codigo_stu As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarSesionTutor", codigo_stu)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarSesionAlumno(ByVal codigo_stua As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarSesionAlumno", codigo_stua)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaCursos(ByVal tipo As String, ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaCursos", tipo, codigo_cac, codigo_cpf)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaAux(ByVal tipo As String, ByVal codigo_cac As Integer, ByVal codigo_tc As Integer, ByVal codigo_cpf As Integer, ByVal codigo_cai As Integer, ByVal categoria As String) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_LISTAAUX", tipo, codigo_cac, codigo_tc, codigo_cpf, codigo_cai, categoria)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaTipoActividad(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_TipoActividadTutoria", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function

    Public Function ListaEstado(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaEstadoTutoria", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaTipoResultado(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaTipoResultadoTutoria", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaNivelRiesgo(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaNivelRiesgoTutoria", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaTipoProblema(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_TipoProblemaTutoria", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaAsistenciasMoodle(ByVal tipo As String, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_spAsistenciasMoodle", tipo, codigo_tua, codigo_tc, codigo_cac)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaNotasMoodle(ByVal tipo As String, ByVal codigo_tua As Integer, ByVal codigo_tc As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_spNotasMoodle", tipo, codigo_tua, codigo_tc, codigo_cac)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ActualizarTipoSesion(ByVal codigo_tis As Integer, ByVal descripcion_tis As String, ByVal estado As Integer, ByVal individual As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTipoSesion", codigo_tis, descripcion_tis, estado, individual, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarTipoSesion(ByVal codigo_tis As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTipoSesion", codigo_tis, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarTipoProblema(ByVal codigo_tpr As Integer, ByVal descripcion_tpr As String, ByVal estado As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTipoProblema", codigo_tpr, descripcion_tpr, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarTipoProblema(ByVal codigo_tpr As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTipoProblema", codigo_tpr, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarTipoResultado(ByVal codigo_tre As Integer, ByVal descripcion_tre As String, ByVal estado As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTipoResultado", codigo_tre, descripcion_tre, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarTipoResultado(ByVal codigo_tre As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTipoResultado", codigo_tre, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ActualizarTipoActividad(ByVal codigo_tat As Integer, ByVal descripcion_tat As String, ByVal estado As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTipoActividad", codigo_tat, descripcion_tat, estado, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarTipoActividad(ByVal codigo_tat As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTipoActividad", codigo_tat, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ActualizarTipoEvaluacion(ByVal codigo_te As Integer, ByVal descripcion_te As String, ByVal estado As Integer, ByVal aplica_suma As Integer, ByVal aplica_peso As Integer, ByVal aplica_promedio As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarTipoEvaluacion", codigo_te, descripcion_te, estado, aplica_suma, aplica_peso, aplica_promedio, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarTipoEvaluacion(ByVal codigo_te As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarTipoEvaluacion", codigo_te, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaVariableTipoEvaluacion(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_te As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaVariableTipoEvaluacion", tipo, codigo, codigo_te)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ListaVariableEvaluacion(ByVal tipo As String, ByVal codigo As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaVariableEvaluacion", tipo, codigo)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ActualizarVariableTipoEvaluacion(ByVal codigo_vt As Integer, ByVal codigo_ve As Integer, ByVal codigo_te As Integer, ByVal peso As Double, ByVal total As Double, ByVal estado As Integer, ByVal obligatorio As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarVariableTipoEvaluacion", codigo_vt, codigo_ve, codigo_te, peso, total, estado, obligatorio, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function EliminarVariableTipoEvaluacion(ByVal codigo_te As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_EliminarVariableTipoEvaluacion", codigo_te, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaOpcionVariableEvaluacion(ByVal tipo As String, ByVal codigo As Integer, ByVal codigo_ve As Integer) As Data.DataTable
        Dim dt As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dt = cnx.TraerDataTable("TUT_ListaOpcionVariableEvaluacion", tipo, codigo, codigo_ve)
        cnx.CerrarConexion()
        Return dt
    End Function
    Public Function ActualizarVariableEvaluacion(ByVal codigo_ve As Integer, ByVal descripcion_ve As String, ByVal estado As Integer, ByVal tipo As String, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_ActualizarVariableEvaluacion", codigo_ve, descripcion_ve, estado, tipo, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ListaDatosAlumno(ByVal codigo_alu As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TUT_DatosAlumno", codigo_alu)
        cnx.CerrarConexion()
        Return dts
    End Function
End Class
