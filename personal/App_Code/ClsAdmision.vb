Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data.SqlTypes
Imports System.Net

Public Class ClsAdmision
    Private mo_Cnx As New ClsConectarDatos
    Private mo_VariablesGlobales As New Dictionary(Of String, String)

    Public Sub New()
        mo_VariablesGlobales = New Dictionary(Of String, String)

        'Dominio de ReportServer
        mo_VariablesGlobales.Item("parhReportServer") = ConfigurationManager.AppSettings("RutaReporte")

        'Origen WEB USAT
        mo_VariablesGlobales.Item("codigoOrigenWeb") = "4"

        'Origen OFICINA INFORMES
        mo_VariablesGlobales.Item("codigoOficinaInformes") = "6"

        'Tabla Archivo
        mo_VariablesGlobales.Item("codigoTablaArchivo") = "15"

        'Ruta archivos compartidos
        mo_VariablesGlobales.Item("sharedFilesPath") = ConfigurationManager.AppSettings("SharedFiles")
    End Sub

    Public Function ObtenerVariableGlobal(ByVal cadena As String) As Object
        Return mo_VariablesGlobales.Item(cadena)
    End Function

    Function GeneraClave(ByVal ls_ApePaterno As String, ByVal ls_Nombres As String) As String
        Randomize()
        Return Right(UCase(ls_ApePaterno), 1) & _
            Left(UCase(ls_Nombres), 1) & _
            CInt(Rnd() * 4) & CInt(Rnd() * 5) & CInt(Rnd() * 9) & CInt(Rnd() * 7)
    End Function

    Public Function ListarTipoEstudio(ByVal ls_Tipo As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ln_Codigo As Integer = 0 'El procedimiento pide un código aunque para este caso no se va a utilizar
        lo_Dts = mo_Cnx.TraerDataTable("ACAD_ConsultarTipoEstudio", ls_Tipo, ln_Codigo)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarCentroCosto(ByVal ls_TipoFuncion As String, ByVal ls_CodigoPer As String, ByVal ls_CodigoTest As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarCentroCostosXPermisosXVisibilidad", ls_TipoFuncion, ls_CodigoPer, "", ls_CodigoTest, 1)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ObtenerCentroCosto(ByVal ls_CodigoCco As String) As Data.DataRow
        Try
            Dim lo_Dts As New Data.DataTable
            Dim ls_Tipo As String = "CI"

            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ConsultarCentroCosto", ls_Tipo, ls_CodigoCco)
            If lo_Dts.Rows.Count = 0 Then
                Throw New Exception("No se ha encontrado centro de costo con ID: " & ls_CodigoCco)
            End If
            mo_Cnx.CerrarConexion()
            Return lo_Dts.Rows(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarProcesoAdmision() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarPreSemetreInscripcion")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarProcesoAdmisionV2() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarPreSemetreInscripcion_v2")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarProcesoAdmisionV3() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarPreSemetreInscripcion_v3")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarCarreraProfesional() As Data.DataTable 'Lista todas las carreras
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, 2, 1, 0)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarCarreraProfesional(ByVal ls_CodigoTest As String) As Data.DataTable 'Lista las carreras por tipo de estudio, además omite las carreras eliminadas (eliminado_cpf)
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ACAD_BuscaEscuelaProfesional", 0, ls_CodigoTest, "")

        'Elimino la fila TODOS que devuelve el procedimiento
        For i As Integer = lo_Dts.Rows.Count - 1 To 0 Step -1
            Dim _Row As Data.DataRow = lo_Dts.Rows(i)
            If _Row.Item("codigo_cpf") = "0" Then
                lo_Dts.Rows.Remove(_Row)
                Exit For
            End If
        Next

        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarModalidadIngreso(ByVal ls_Tipo As String, ByVal ls_CodigoTest As String, ByVal ls_CodicoCac As String, ByVal ls_CodigoCpf As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarInformacionParaEvento", ls_Tipo, ls_CodigoTest, ls_CodicoCac, ls_CodigoCpf)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarModalidadIngresoPorTipo(ByVal ls_Tipo As String, Optional ByVal ls_Param As String = "") As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarModalidadIngreso", ls_Tipo, ls_Param)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarModalidadIngreso(ByVal tipo As String, ByVal desc As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarModalidadIngreso", tipo, desc)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function RetornaAlumnoActivo(ByVal codigo_pso As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_RetornaAlumnoActivo", codigo_pso)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDirectorCentroCostos(ByVal area As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CEC_ConsultarDirectorCentroCostos", area)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudasPersonaxCco(ByVal codigo_cco As Integer, ByVal codigo_pso As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CAJ_ConsultarDeudasPersonaxCco", codigo_pso, codigo_cco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarServicioPorNombre(ByVal descripcion_sco As String, ByVal codigo_cco As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarServicioPorNombre", descripcion_sco, codigo_cco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarBeneficiosPostulacionAlumno(ByVal codigo_alu As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EPRE_ConsultarBeneficiosPostulacionAlumno", codigo_alu)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarPaises() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarPais", "T", "")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListaDepartamentos(ByVal ls_CodigoPai As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarLugares", "2", ls_CodigoPai, "")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListaProvincias(ByVal ls_CodigoDep As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarLugares", "3", ls_CodigoDep, "")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListaDistritos(ByVal ls_CodigoProv As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarLugares", "4", ls_CodigoProv, "")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarInstitucionEducativa(ByVal ls_Tipo As String, ByVal ls_Codigo As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()

        Dim lb_SoloSecundaria As Boolean = True
        lo_Dts = mo_Cnx.TraerDataTable("PEC_ConsultarInstitucionesEducativasPorUbicacion", ls_Tipo, ls_Codigo, lb_SoloSecundaria)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarInstitucionEducativaPorId(ByVal ls_CodigoIed As String) As Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()

        Dim lo_Dts As New Data.DataTable
        Dim ls_Tipo As String = "COL"
        Dim ls_Texto As String = ""
        lo_Dts = mo_Cnx.TraerDataTable("PEC_ConsultarInstitucionesEducativas", ls_Tipo, ls_Texto, ls_CodigoIed)

        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ObtenerInstitucionEducativa(ByVal ls_Tipo As String, ByVal ls_Texto As String, ByVal ls_CodigoIed As String) As Data.DataRow
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()

            lo_Dts = mo_Cnx.TraerDataTable("PEC_ConsultarInstitucionesEducativas", ls_Tipo, ls_Texto, ls_CodigoIed)
            If lo_Dts.Rows.Count = 0 Then
                Throw New Exception("No se ha encontrado institución educativa con ID: " & ls_CodigoIed)
            End If

            mo_Cnx.CerrarConexion()
            Return lo_Dts.Rows(0)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarCategoriaPostulacion() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EPRE_ListarCategoriasPostulacion", "%")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarBeneficioPostulacion() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EPRE_ListarBeneficiosPostulacion", "%")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarCicloAcademico() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarCicloAcademico", "CI2", "")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarEventoCRM(ByVal ls_CodigoTest As String, ByVal ls_CodigoCac As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_Opcion As String = "CA" 'Combo para inscripción pre-grado 
        Dim ln_Codigo As Integer = 0 'No se envía código
        Dim ls_Param1 As String = ls_CodigoTest
        Dim ls_Param2 As String = ls_CodigoCac
        lo_Dts = mo_Cnx.TraerDataTable("CRM_ListaEventos", ls_Opcion, ln_Codigo, ls_Param1, ls_Param2)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarTipoParticipante(ByVal ls_CodigoCco As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_TokenCco As String = Nothing
        lo_Dts = mo_Cnx.TraerDataTable("ListaTipoPartipantesCongreso", ls_TokenCco, ls_CodigoCco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarTipoParticipante(Optional ByVal codigoTpar As Integer = 0, _
                                           Optional ByVal descripcionTpar As String = "", _
                                           Optional ByVal ambitoTpar As String = "", _
                                           Optional ByVal usatTpar As Object = Nothing, _
                                           Optional ByVal etiquetaTpar As String = "", _
                                           Optional ByVal universidadNacionalTpar As Object = Nothing) As Data.DataTable

        Dim usatTparDB As Object = IIf(usatTpar <> Nothing, usatTpar, DBNull.Value)
        Dim universidadNacionalTparDB As Object = IIf(universidadNacionalTpar <> Nothing, universidadNacionalTpar, DBNull.Value)

        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ListarTipoParticipante", codigoTpar, descripcionTpar, ambitoTpar, usatTparDB, _
                                       etiquetaTpar, universidadNacionalTparDB)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarTipoEstudioPerfil() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_Tipo As String = "1"
        Dim ls_CodigoTep As String = "0"
        Dim ls_NombreTep As String = ""
        lo_Dts = mo_Cnx.TraerDataTable("TipoEstudioPerfil_Listar", ls_Tipo, ls_CodigoTep, ls_NombreTep)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ContarPostulantes(ByVal ls_codigoCco As Integer, ByVal ls_DNI As String, ByVal ls_Estado As String, ByVal lb_MostrarSinDeuda As Boolean) As Integer
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_TipoConsulta As String = "C" 'Contar
        Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_ListarPostulantes", ls_TipoConsulta, ls_codigoCco, ls_DNI, ls_Estado, lb_MostrarSinDeuda)
        mo_Cnx.CerrarConexion()
        Return lo_Salida(0)
    End Function

    Public Function ListarPostulante(ByVal ls_codigoCco As Integer, ByVal ls_DNI As String, ByVal ls_Estado As String, ByVal lb_MostrarSinDeuda As Boolean, ByVal ln_pagina As Integer, ByVal ln_filasPorPagina As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_TipoConsulta As String = "L" 'Listar
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ListarPostulantes", ls_TipoConsulta, ls_codigoCco, ls_DNI, ls_Estado, lb_MostrarSinDeuda, ln_pagina, ln_filasPorPagina)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarMovimientosPostulante(ByVal ls_CodigoPso As String, ByVal ls_CodigoCco As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarCargosAbonosPersona", ls_CodigoPso, ls_CodigoCco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ObtenerDatosInscripcion(ByVal ls_CodigoAlu As String, ByVal ls_TipoConsulta As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        Dim ls_Tipo As String = "1" 'Según procedimiento: Agregar E-Pre+Encuentra alumno registrado
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarAlumnoInscripcion", ls_CodigoAlu, ls_TipoConsulta)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function CalcularCategoriaEstudiante(ByVal ln_CodigoCpf As Integer, ByVal ln_CodigoIed As Integer, ByVal ln_CodigoMin As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_RetornaCategoriaPension", ln_CodigoCpf, ln_CodigoIed, ln_CodigoMin)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GuardarInscripcion(ByVal codigoOri As String, ByVal nombreEve As String, ByVal descripcionCac As String, ByVal codigoDoci As String, ByVal numerodocInt As String, ByVal apepaternoInt As String, _
                                      ByVal apematernoInt As String, ByVal nombresInt As String, ByVal fechanacimientoInt As String, ByVal sexoPso As String, _
                                      ByVal gradoInt As String, ByVal codigoIed As String, ByVal codigoCpf As String, ByVal estadoInt As String, _
                                      ByVal telNumeroTei As String, ByVal celNumeroTei As String, ByVal descripcionEmi As String, ByVal codigoDep As String, _
                                      ByVal codigoPro As String, ByVal codigoDis As String, ByVal direccionDin As String, ByVal codigoCco As String, _
                                      ByVal codigoCac As String, ByVal codigoMin As String, ByVal codigoTest As String, ByVal email2 As String, _
                                      ByVal estadoCivil As String, ByVal passwordAlu As String, ByVal nroRuc As String, ByVal centroLabores As String, ByVal cargoActual As String, ByVal codigoTpar As String, _
                                      ByVal numerodocFin As String, ByVal apepaternoFin As String, ByVal apematernoFin As String, ByVal nombresFin As String, _
                                      ByVal celularFin As String, ByVal emailFin As String, ByVal validaDeuda As String, ByVal generaCargo As Boolean, ByVal usuarioReg As String, ByVal accion As String) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("WS_InscripcionInteresado", codigoOri, nombreEve, descripcionCac, codigoDoci, numerodocInt, apepaternoInt, apematernoInt, _
                                                         nombresInt, fechanacimientoInt, sexoPso, gradoInt, codigoIed, codigoCpf, estadoInt, telNumeroTei, _
                                                         celNumeroTei, descripcionEmi, codigoDep, codigoPro, codigoDis, direccionDin, codigoCco, _
                                                         codigoCac, codigoMin, codigoTest, email2, estadoCivil, passwordAlu, nroRuc, centroLabores, cargoActual, codigoTpar, _
                                                         numerodocFin, apepaternoFin, apematernoFin, nombresFin, celularFin, emailFin, validaDeuda, generaCargo, usuarioReg, _
                                                         accion, "0", "", "0")
            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)
            lo_Resultado.Item("cod") = lo_Salida(2)
            mo_Cnx.CerrarConexion()
            Return lo_Resultado

        Catch ex As Exception
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
    End Function

    Public Function ListarCuotasConvenio(ByVal codigoAlu As String, ByVal tipoCparc As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ListarCuotasConvenioParticipante", codigoAlu, tipoCparc)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GenerarConvenioParticipante(ByVal codigoAlu As String, ByVal tipoCparc As String) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_GenerarConvenioParticipante", codigoAlu, tipoCparc, "0", "")
            lo_Resultado.Add("rpta", lo_Salida(0))
            lo_Resultado.Add("msg", lo_Salida(1))

            mo_Cnx.CerrarConexion()
            Return lo_Resultado
        Catch ex As Exception
            lo_Resultado.Add("rpta", "-1")
            lo_Resultado.Add("msg", ex.Message)
            Return lo_Resultado
        End Try
    End Function

    Public Function ListarIngresante(ByVal cicloingr As String, ByVal codigo_cco As Integer, ByVal codigo_min As Integer, ByVal codigo_cpf As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()

        cicloingr = IIf(cicloingr = "-1", "%", cicloingr)
        codigo_min = IIf(codigo_min = -1, 0, codigo_min)
        codigo_cpf = IIf(codigo_cpf = -1, 0, codigo_cpf)

        Dim dni As String = ""
        Dim codigouniver As String = ""
        Dim nombres As String = ""
        Dim estadopost As String = "I" 'INGRESANTES
        Dim codigo_test As Integer = 0
        Dim codigo_alu As Integer = 0
        Dim categorizado As String = "%"
        Dim impresion As String = "%"
        Dim letra As String = ""
        Dim deuda_matricula As Integer = 0

        lo_Dts = mo_Cnx.TraerDataTable("EPRE_ListarPostulantesV2", _
                                            cicloingr, _
                                            codigo_cco, _
                                            codigo_min, _
                                            dni, _
                                            codigouniver, _
                                            nombres, _
                                            estadopost, _
                                            codigo_test, _
                                            codigo_alu, _
                                            categorizado, _
                                            impresion, _
                                            codigo_cpf, _
                                            letra, _
                                            deuda_matricula _
                                        )
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ObtenerDatosPersonales(ByVal ls_Tipo As String, ByVal ls_Valor As String, Optional ByVal ls_NumDoc As String = "", Optional ByVal ls_ApePat As String = "", Optional ByVal ls_ApeMat As String = "", Optional ByVal ls_Nombre As String = "") As Data.DataTable
        Dim lo_Dts As New Data.DataTable, lo_DtsTemp As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()

        With lo_Dts.Columns
            .Add("codigoPso")
            .Add("codigoInt")
            .Add("nrodocident")
            .Add("nrodocidentConfirmado")
            .Add("apellidoPaterno")
            .Add("apellidoMaterno")
            .Add("nombres")
            .Add("fechaNacimiento")
            .Add("sexo")
            .Add("telefonoCelular")
            .Add("telefonoFijo")
            .Add("emailPrincipal")
            .Add("codigoDep")
            .Add("codigoPro")
            .Add("codigoDis")
            .Add("codigoDepNac")
            .Add("codigoProNac")
            .Add("codigoDisNac")
            .Add("direccion")
            .Add("codigoDepInstEdu")
            .Add("codigoProInstEdu")
            .Add("codigoDisInstEdu")
            .Add("codigoIed")
            .Add("codigoCpf")
            .Add("anioEstudios")
            'Datos del padre
            .Add("numeroDocIdentPadre")
            .Add("apellidoPaternoPadre")
            .Add("apellidoMaternoPadre")
            .Add("nombresPadre")
            .Add("fechaNacimientoPadre")
            .Add("telefonoFijoPadre")
            .Add("telefonoCelularPadre")
            .Add("operadorCelularPadre")
            .Add("emailPadre")
            .Add("indRespPagoPadre")
            .Add("codigoDepPadre")
            .Add("codigoProPadre")
            .Add("codigoDisPadre")
            .Add("direccionPadre")
            'Datos de la madre
            .Add("numeroDocIdentMadre")
            .Add("apellidoPaternoMadre")
            .Add("apellidoMaternoMadre")
            .Add("nombresMadre")
            .Add("fechaNacimientoMadre")
            .Add("telefonoFijoMadre")
            .Add("telefonoCelularMadre")
            .Add("operadorCelularMadre")
            .Add("emailMadre")
            .Add("indRespPagoMadre")
            .Add("codigoDepMadre")
            .Add("codigoProMadre")
            .Add("codigoDisMadre")
            .Add("direccionMadre")
            'Datos del apoderado
            .Add("numeroDocIdentApod")
            .Add("apellidoPaternoApod")
            .Add("apellidoMaternoApod")
            .Add("nombresApod")
            .Add("fechaNacimientoApod")
            .Add("telefonoFijoApod")
            .Add("telefonoCelularApod")
            .Add("operadorCelularApod")
            .Add("emailApod")
            .Add("indRespPagoApod")
            .Add("codigoDepApod")
            .Add("codigoProApod")
            .Add("codigoDisApod")
            .Add("direccionApod")
        End With

        Dim ln_CodigoInt As Integer = 0, ln_TipoDoc As Integer = 1
        Dim ls_Opcion As String = ""
        Select Case ls_Tipo
            Case "DNIE", "DNIU"
                ls_Opcion = "B"
            Case "APEE"
                ls_Opcion = "APE"
        End Select

        lo_DtsTemp = mo_Cnx.TraerDataTable("PERSON_ConsultarPersona", ls_Tipo, ls_Valor)
        If lo_DtsTemp.Rows.Count > 0 Then
            For Each lo_Row As Data.DataRow In lo_DtsTemp.Rows
                Dim lo_NewRow As Data.DataRow = FilaVaciaDatosPersonales(lo_Dts)
                lo_NewRow.Item("codigoPso") = lo_Row.Item("codigo_Pso").ToString
                lo_NewRow.Item("nroDocIdent") = lo_Row.Item("numeroDocIdent_Pso").ToString
                lo_NewRow.Item("nrodocidentConfirmado") = True
                lo_NewRow.Item("apellidoPaterno") = lo_Row.Item("apellidoPaterno_Pso").ToString
                lo_NewRow.Item("apellidoMaterno") = lo_Row.Item("apellidoMaterno_Pso").ToString
                lo_NewRow.Item("nombres") = lo_Row.Item("nombres_Pso").ToString
                lo_NewRow.Item("fechaNacimiento") = Date.Parse(lo_Row.Item("fechaNacimiento_Pso")).ToString("dd/MM/yyyy")
                lo_NewRow.Item("sexo") = IIf(IsDBNull(lo_Row.Item("sexo_Pso")), "-1", lo_Row.Item("sexo_Pso"))
                lo_NewRow.Item("telefonoCelular") = lo_Row.Item("telefonoCelular_Pso").ToString
                lo_NewRow.Item("telefonoFijo") = lo_Row.Item("telefonoFijo_Pso").ToString
                lo_NewRow.Item("emailPrincipal") = lo_Row.Item("emailPrincipal_Pso").ToString
                lo_NewRow.Item("codigoDep") = IIf(IsDBNull(lo_Row.Item("codigo_dep")), "-1", lo_Row.Item("codigo_dep"))
                lo_NewRow.Item("codigoPro") = IIf(IsDBNull(lo_Row.Item("codigo_pro")), "-1", lo_Row.Item("codigo_pro"))
                lo_NewRow.Item("codigoDis") = IIf(IsDBNull(lo_Row.Item("codigo_dis")), "-1", lo_Row.Item("codigo_dis"))
                lo_NewRow.Item("direccion") = lo_Row.Item("direccion_Pso").ToString

                ls_NumDoc = lo_Row.Item("numeroDocIdent_Pso").ToString
                ls_ApePat = lo_Row.Item("apellidoPaterno_Pso").ToString
                ls_ApeMat = lo_Row.Item("apellidoMaterno_Pso").ToString
                ls_Nombre = lo_Row.Item("nombres_Pso").ToString

                'Busco los datos adicionales en la tabla Interesado_CRM
                lo_DtsTemp = mo_Cnx.TraerDataTable("CRM_BuscarInteresado", ls_Opcion, ln_CodigoInt, ln_TipoDoc, ls_NumDoc, ls_ApePat, ls_ApeMat, ls_Nombre)
                If lo_DtsTemp.Rows.Count > 0 Then
                    lo_Row = lo_DtsTemp.Rows(0)
                    lo_NewRow.Item("nrodocidentConfirmado") = lo_Row.Item("numerodoc_confirmado_int")
                    lo_NewRow.Item("codigoInt") = IIf(IsDBNull(lo_Row.Item("codigo_int")), "-1", lo_Row.Item("codigo_int"))
                    lo_NewRow.Item("codigoIed") = IIf(IsDBNull(lo_Row.Item("codigo_ied")), "-1", lo_Row.Item("codigo_ied"))
                    lo_NewRow.Item("codigoCpf") = IIf(IsDBNull(lo_Row.Item("codigo_cpf")), "-1", lo_Row.Item("codigo_cpf"))
                    lo_NewRow.Item("anioEstudios") = IIf(IsDBNull(lo_Row.Item("Grado_int")), "-1", lo_Row.Item("Grado_int"))
                End If

                'Busco datos adicionales en la vista vstAlumno
                If Not String.IsNullOrEmpty(ls_NumDoc.Trim) Then
                    lo_DtsTemp = mo_Cnx.TraerDataTable("ConsultarAlumno", "DNI", ls_NumDoc)
                    Dim ln_CodigoAlu As Integer = 0

                    If lo_DtsTemp.Rows.Count > 0 Then
                        'Primero busco si hay algún registro para codigo_test = 2 (PREGRADO) ya
                        'en otros tipos de estudio no se registran todos los datos familiares
                        For Each _row As Data.DataRow In lo_DtsTemp.Rows
                            If _row.Item("codigo_test") = "2" Then
                                ln_CodigoAlu = _row.Item("codigo_Alu")
                                'Exit For 'andy.diaz 24/03/2020: Si hubiera más de un alumno me quedo con el último código
                            End If
                        Next

                        If ln_CodigoAlu = 0 Then
                            ln_CodigoAlu = lo_DtsTemp.Rows(lo_DtsTemp.Rows.Count - 1).Item("codigo_Alu")
                        End If
                    End If

                    If ln_CodigoAlu <> 0 Then
                        lo_DtsTemp = mo_Cnx.TraerDataTable("ADM_ConsultarAlumnoInscripcion", ln_CodigoAlu, "C")
                        If lo_DtsTemp.Rows.Count > 0 Then
                            lo_Row = lo_DtsTemp.Rows(0)
                            lo_NewRow.Item("codigoDepNac") = IIf(lo_Row.Item("codigo_DepNac") = "0", "-1", lo_Row.Item("codigo_DepNac"))
                            lo_NewRow.Item("codigoProNac") = IIf(lo_Row.Item("codigo_ProNac") = "0", "-1", lo_Row.Item("codigo_ProNac"))
                            lo_NewRow.Item("codigoDisNac") = IIf(lo_Row.Item("codigo_DisNac") = "0", "-1", lo_Row.Item("codigo_DisNac"))
                            lo_NewRow.Item("codigoDepInstEdu") = IIf(lo_Row.Item("codigo_DepInstEdu") = "0", "-1", lo_Row.Item("codigo_DepInstEdu"))
                            lo_NewRow.Item("codigoProInstEdu") = IIf(lo_Row.Item("codigo_ProInstEdu") = "0", "-1", lo_Row.Item("codigo_ProInstEdu"))
                            lo_NewRow.Item("codigoDisInstEdu") = IIf(lo_Row.Item("codigo_DisInstEdu") = "0", "-1", lo_Row.Item("codigo_DisInstEdu"))
                            lo_NewRow.Item("codigoIed") = IIf(lo_Row.Item("codigo_Ied") = "0", "-1", lo_Row.Item("codigo_Ied"))
                            'Datos del padre
                            lo_NewRow.Item("numeroDocIdentPadre") = lo_Row.Item("numeroDocIdentPadre_fam").ToString
                            lo_NewRow.Item("apellidoPaternoPadre") = lo_Row.Item("apellidoPaternoPadre_fam").ToString
                            lo_NewRow.Item("apellidoMaternoPadre") = lo_Row.Item("apellidoMaternoPadre_fam").ToString
                            lo_NewRow.Item("nombresPadre") = lo_Row.Item("nombresPadre_fam").ToString
                            If IsDate(lo_Row.Item("fechaNacimientoPadre_fam")) Then
                                lo_NewRow.Item("fechaNacimientoPadre") = Date.Parse(lo_Row.Item("fechaNacimientoPadre_fam")).ToString("dd/MM/yyyy")
                            End If
                            lo_NewRow.Item("telefonoFijoPadre") = lo_Row.Item("telefonoFijoPadre_fam").ToString
                            lo_NewRow.Item("telefonoCelularPadre") = lo_Row.Item("telefonoCelularPadre_fam").ToString
                            lo_NewRow.Item("operadorCelularPadre") = lo_Row.Item("operadorCelularPadre_fam").ToString
                            lo_NewRow.Item("indRespPagoPadre") = lo_Row.Item("indRespPagoPadre_fam").ToString
                            lo_NewRow.Item("codigoDepPadre") = IIf(lo_Row.Item("codigo_DepPadre") = "0", "-1", lo_Row.Item("codigo_DepPadre"))
                            lo_NewRow.Item("codigoProPadre") = IIf(lo_Row.Item("codigo_ProPadre") = "0", "-1", lo_Row.Item("codigo_ProPadre"))
                            lo_NewRow.Item("codigoDisPadre") = IIf(lo_Row.Item("codigo_DisPadre") = "0", "-1", lo_Row.Item("codigo_DisPadre"))
                            lo_NewRow.Item("direccionPadre") = lo_Row.Item("direccionPadre_fam").ToString
                            'Datos de la madre
                            lo_NewRow.Item("numeroDocIdentMadre") = lo_Row.Item("numeroDocIdentMadre_fam").ToString
                            lo_NewRow.Item("apellidoPaternoMadre") = lo_Row.Item("apellidoPaternoMadre_fam").ToString
                            lo_NewRow.Item("apellidoMaternoMadre") = lo_Row.Item("apellidoMaternoMadre_fam").ToString
                            lo_NewRow.Item("nombresMadre") = lo_Row.Item("nombresMadre_fam").ToString
                            If IsDate(lo_Row.Item("fechaNacimientoMadre_fam")) Then
                                lo_NewRow.Item("fechaNacimientoMadre") = Date.Parse(lo_Row.Item("fechaNacimientoMadre_fam")).ToString("dd/MM/yyyy")
                            End If
                            lo_NewRow.Item("telefonoFijoMadre") = lo_Row.Item("telefonoFijoMadre_fam").ToString
                            lo_NewRow.Item("telefonoCelularMadre") = lo_Row.Item("telefonoCelularMadre_fam").ToString
                            lo_NewRow.Item("operadorCelularMadre") = lo_Row.Item("operadorCelularMadre_fam").ToString
                            lo_NewRow.Item("indRespPagoMadre") = lo_Row.Item("indRespPagoMadre_fam").ToString
                            lo_NewRow.Item("codigoDepMadre") = IIf(lo_Row.Item("codigo_DepMadre") = "0", "-1", lo_Row.Item("codigo_DepMadre"))
                            lo_NewRow.Item("codigoProMadre") = IIf(lo_Row.Item("codigo_ProMadre") = "0", "-1", lo_Row.Item("codigo_ProMadre"))
                            lo_NewRow.Item("codigoDisMadre") = IIf(lo_Row.Item("codigo_DisMadre") = "0", "-1", lo_Row.Item("codigo_DisMadre"))
                            lo_NewRow.Item("direccionMadre") = lo_Row.Item("direccionMadre_fam").ToString
                            'Datos del apoderado
                            lo_NewRow.Item("numeroDocIdentApod") = lo_Row.Item("numeroDocIdentApod_fam").ToString
                            lo_NewRow.Item("apellidoPaternoApod") = lo_Row.Item("apellidoPaternoApod_fam").ToString
                            lo_NewRow.Item("apellidoMaternoApod") = lo_Row.Item("apellidoMaternoApod_fam").ToString
                            lo_NewRow.Item("nombresApod") = lo_Row.Item("nombresApod_fam").ToString
                            If IsDate(lo_Row.Item("fechaNacimientoApod_fam")) Then
                                lo_NewRow.Item("fechaNacimientoApod") = Date.Parse(lo_Row.Item("fechaNacimientoApod_fam")).ToString("dd/MM/yyyy")
                            End If
                            lo_NewRow.Item("telefonoFijoApod") = lo_Row.Item("telefonoFijoApod_fam").ToString
                            lo_NewRow.Item("telefonoCelularApod") = lo_Row.Item("telefonoCelularApod_fam").ToString
                            lo_NewRow.Item("operadorCelularApod") = lo_Row.Item("operadorCelularApod_fam").ToString
                            lo_NewRow.Item("indRespPagoApod") = lo_Row.Item("indRespPagoApod_fam").ToString
                            lo_NewRow.Item("codigoDepApod") = IIf(lo_Row.Item("codigo_DepApod") = "0", "-1", lo_Row.Item("codigo_DepApod"))
                            lo_NewRow.Item("codigoProApod") = IIf(lo_Row.Item("codigo_ProApod") = "0", "-1", lo_Row.Item("codigo_ProApod"))
                            lo_NewRow.Item("codigoDisApod") = IIf(lo_Row.Item("codigo_DisApod") = "0", "-1", lo_Row.Item("codigo_DisApod"))
                            lo_NewRow.Item("direccionApod") = lo_Row.Item("direccionApod_fam").ToString
                        End If
                    End If
                End If

                lo_Dts.Rows.Add(lo_NewRow)
            Next
        Else
            'No ha encontrado registros en la tabla PERSONA, busco en la tabla Interesado_CRM
            lo_DtsTemp = mo_Cnx.TraerDataTable("CRM_BuscarInteresado", ls_Opcion, ln_CodigoInt, ln_TipoDoc, ls_NumDoc, ls_ApePat, ls_ApeMat, ls_Nombre)
            If lo_DtsTemp.Rows.Count > 0 Then
                For Each lo_Row As Data.DataRow In lo_DtsTemp.Rows
                    Dim lo_NewRow As Data.DataRow = FilaVaciaDatosPersonales(lo_Dts)
                    lo_NewRow.Item("nroDocIdent") = lo_Row.Item("numerodoc_int").ToString
                    lo_NewRow.Item("nrodocidentConfirmado") = lo_Row.Item("numerodoc_confirmado_int")
                    lo_NewRow.Item("apellidoPaterno") = lo_Row.Item("apepaterno_int").ToString
                    lo_NewRow.Item("apellidoMaterno") = lo_Row.Item("apematerno_int").ToString
                    lo_NewRow.Item("nombres") = lo_Row.Item("nombres_int").ToString
                    If IsDate(lo_Row.Item("fecha_nac")) Then
                        lo_NewRow.Item("fechaNacimiento") = Date.Parse(lo_Row.Item("fecha_nac")).ToString("dd/MM/yyyy")
                    End If
                    lo_NewRow.Item("emailPrincipal") = lo_Row.Item("email").ToString
                    lo_NewRow.Item("telefonoCelular") = lo_Row.Item("celular").ToString
                    lo_NewRow.Item("telefonoFijo") = lo_Row.Item("telefono").ToString
                    lo_NewRow.Item("codigoDep") = IIf(IsDBNull(lo_Row.Item("codigo_Dep")), "-1", lo_Row.Item("codigo_Dep"))
                    lo_NewRow.Item("codigoPro") = IIf(IsDBNull(lo_Row.Item("codigo_Pro")), "-1", lo_Row.Item("codigo_Pro"))
                    lo_NewRow.Item("codigoDis") = IIf(IsDBNull(lo_Row.Item("codigo_Dis")), "-1", lo_Row.Item("codigo_Dis"))
                    lo_NewRow.Item("direccion") = lo_Row.Item("direccion").ToString
                    lo_NewRow.Item("codigoIed") = lo_Row.Item("codigo_ied").ToString
                    lo_NewRow.Item("codigoCpf") = IIf(IsDBNull(lo_Row.Item("codigo_Cpf")), "-1", lo_Row.Item("codigo_Cpf"))
                    lo_NewRow.Item("anioEstudios") = IIf(IsDBNull(lo_Row.Item("Grado_int")), "-1", lo_Row.Item("Grado_int"))
                    lo_Dts.Rows.Add(lo_NewRow)
                Next
            End If
        End If

        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Private Function FilaVaciaDatosPersonales(ByVal lo_Dts As Data.DataTable) As Data.DataRow
        Dim lo_NewRow As Data.DataRow = lo_Dts.NewRow
        With lo_NewRow
            .Item("codigoPso") = ""
            .Item("codigoInt") = ""
            .Item("nroDocIdent") = ""
            .Item("apellidoPaterno") = ""
            .Item("apellidoMaterno") = ""
            .Item("nombres") = ""
            .Item("fechaNacimiento") = ""
            .Item("sexo") = "-1"
            .Item("telefonoCelular") = ""
            .Item("telefonoFijo") = ""
            .Item("emailPrincipal") = ""
            .Item("codigoDep") = "-1"
            .Item("codigoPro") = "-1"
            .Item("codigoDis") = "-1"
            .Item("codigoDepNac") = "-1"
            .Item("codigoProNac") = "-1"
            .Item("codigoDisNac") = "-1"
            .Item("direccion") = ""
            .Item("codigoDepInstEdu") = "-1"
            .Item("codigoProInstEdu") = "-1"
            .Item("codigoDisInstEdu") = "-1"
            .Item("codigoIed") = "-1"
            .Item("codigoCpf") = "-1"
            .Item("anioEstudios") = "-1"
            'Datos del padre
            .Item("numeroDocIdentPadre") = ""
            .Item("apellidoPaternoPadre") = ""
            .Item("apellidoMaternoPadre") = ""
            .Item("nombresPadre") = ""
            .Item("fechaNacimientoPadre") = ""
            .Item("telefonoFijoPadre") = ""
            .Item("telefonoCelularPadre") = ""
            .Item("operadorCelularPadre") = ""
            .Item("emailPadre") = ""
            .Item("indRespPagoPadre") = ""
            .Item("codigoDepPadre") = "-1"
            .Item("codigoProPadre") = "-1"
            .Item("codigoDisPadre") = "-1"
            .Item("direccionPadre") = ""
            'Datos de la madre
            .Item("numeroDocIdentMadre") = ""
            .Item("apellidoPaternoMadre") = ""
            .Item("apellidoMaternoMadre") = ""
            .Item("nombresMadre") = ""
            .Item("fechaNacimientoMadre") = ""
            .Item("telefonoFijoMadre") = ""
            .Item("telefonoCelularMadre") = ""
            .Item("operadorCelularMadre") = ""
            .Item("emailMadre") = ""
            .Item("indRespPagoMadre") = ""
            .Item("codigoDepMadre") = "-1"
            .Item("codigoProMadre") = "-1"
            .Item("codigoDisMadre") = "-1"
            .Item("direccionMadre") = ""
            'Datos del apoderado
            .Item("numeroDocIdentApod") = ""
            .Item("apellidoPaternoApod") = ""
            .Item("apellidoMaternoApod") = ""
            .Item("nombresApod") = ""
            .Item("fechaNacimientoApod") = ""
            .Item("telefonoFijoApod") = ""
            .Item("telefonoCelularApod") = ""
            .Item("operadorCelularApod") = ""
            .Item("emailApod") = ""
            .Item("indRespPagoApod") = ""
            .Item("codigoDepApod") = "-1"
            .Item("codigoProApod") = "-1"
            .Item("codigoDisApod") = "-1"
            .Item("direccionApod") = ""
        End With
        Return lo_NewRow
    End Function

    Public Function ListarRequisitosAdmision(ByVal ls_CodigoAlu As String, ByVal lb_Entregados As Boolean) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarRequisitosAdmision", ls_CodigoAlu, lb_Entregados)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GuardarRequisitosAdmision(ByVal codigo_alu As Integer, ByVal requisitos As String, ByVal usuario_reg As Integer) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_ActualizarRequisitosAdmision", codigo_alu, requisitos, usuario_reg)

            lo_Resultado.Add("rpta", lo_Salida(0))
            lo_Resultado.Add("msg", lo_Salida(1))

            mo_Cnx.CerrarConexion()
            Return lo_Resultado

        Catch ex As Exception
            lo_Resultado.Add("rpta", "-1")
            lo_Resultado.Add("msg", ex.Message)
            Return lo_Resultado
        End Try
    End Function

    'OBTENER ULTIMO ID DE ARCHIVO,NOMBRE Y RUTA INSERTARO EN ARCHIVOCOMPARTIDO PARA ACTUALIZAR EN LOS REGISTROS
    Public Function ObtenerUltimoIDArchivoCompartido(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal nrooperacion As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ObtenerUltimoIDArchvoCompartido", idtabla, idtransaccion, nrooperacion)
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return lo_Dts
    End Function

    Public Function CargarExcelNotas(ByVal ruta As String, ByVal codigo_cco As Integer, ByVal idArchivoCompartido As Integer, ByVal activarEstado As Boolean, ByVal codigo_per As Integer) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)

        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
            mo_Cnx.AbrirConexion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_CargarExcelNotas", ruta, codigo_cco, idArchivoCompartido, activarEstado, codigo_per, 0, "")
            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)

            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
        End Try

        Return lo_Resultado
    End Function

    Public Function RetornarAlumnoActivo(ByVal codigoPso As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_RetornaAlumnoActivo", codigoPso)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarAlumno(ByVal codigoPso As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("PERSON_ConsultarClientes", codigoPso)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDatosAlumno(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ConsultarAlumno", tipo, param)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarServicioPorCeco(ByVal codigo_cco As Integer, Optional ByVal tipo As String = "MIN") As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarServicioPorCeco", codigo_cco, tipo)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    'Public Function ConsultarEventos(ByVal codigo_cco As Integer) As Data.DataTable
    '    Dim lo_Dts As New Data.DataTable
    '    mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    mo_Cnx.AbrirConexion()
    '    lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarEventos", codigo_cco)
    '    mo_Cnx.CerrarConexion()
    '    Return lo_Dts
    'End Function

    Public Function ConsultarEventos(ByVal tipo As String, ByVal codigo_cco As String, ByVal param2 As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarEventos", tipo, codigo_cco, param2)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDatosServicio(ByVal codigo_cco As Integer, ByVal valor As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarDatosServicio", codigo_cco, valor)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudas(ByVal codigo_pso As String, ByVal codigo_cco As Integer, ByVal valor As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_consultarDeudas", codigo_pso, codigo_cco, valor)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudasPorPersonaCentroCosto(ByVal codigo_pso As Integer, ByVal codigo_cco As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CAJ_ConsultarDeudasPersonaxCco", codigo_pso, codigo_cco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudasPorPersonaTipoEstudio(ByVal codigo_pso As Integer, ByVal codigo_test As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", codigo_pso, codigo_test)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudasPendientesPersona(ByVal codigo_pso As Integer, ByVal codigo_test As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CAJ_ConsultarDeudasPendientesPersona", codigo_pso, codigo_test)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarDeudasPorAlumnoTipoParticipante(ByVal ls_CodigoAlu As Integer, ByVal ls_TipoCparc As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarDeudasPorAlumnoTipoParticipante", ls_CodigoAlu, ls_TipoCparc)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function BuscarEventoCRM(ByVal ls_NroDocIdentidad As Integer, ByVal ls_CodigoCco As String, ByVal ls_CodigoMin As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CRM_BuscarEventoAdmision", ls_NroDocIdentidad, ls_CodigoCco, ls_CodigoMin)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function BuscarEventoCRMPorNombreYCiclo(ByVal ls_NombreEve As Integer, ByVal ls_DescripcionCac As String, ByVal ls_CodigoTest As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_BuscarEventoCRMPorNombreYCiclo", ls_NombreEve, ls_DescripcionCac, ls_CodigoTest)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function CambiarPrincipalServicioCentroCosto(ByVal ls_CodigoScc As String, ByVal lb_PrincipalSco As Boolean) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_CambiarPrincipalServicioCentroCosto", ls_CodigoScc, lb_PrincipalSco, "0", "", "0")

            lo_Resultado.Add("rpta", lo_Salida(0))
            lo_Resultado.Add("msg", lo_Salida(1))

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
            Return lo_Resultado

        Catch ex As Exception
            lo_Resultado.Add("rpta", "-1")
            lo_Resultado.Add("msg", ex.Message)
            Return lo_Resultado
        End Try
    End Function

    Public Function ConsultarConvenioParticipante(Optional ByVal ln_CodigoCparc As Integer = 0, Optional ByVal ln_CodigoScc As Integer = 0) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarConvenioParticipante", ln_CodigoCparc, ln_CodigoScc)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GuardarCentroCostosParticipanteC( _
        Optional ByVal codigoCparc As Integer = 0 _
        , Optional ByVal codigoCco As Integer = 0 _
        , Optional ByVal codigoScc As Integer = 0 _
        , Optional ByVal codigoTpar As Integer = 0 _
        , Optional ByVal montoCparc As Decimal = 0.0 _
        , Optional ByVal cuotasCparc As Integer = 0 _
        , Optional ByVal tipoCparc As String = "" _
    ) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_GuardarCentroCostosParticipanteC" _
                , codigoCparc _
                , codigoCco _
                , codigoScc _
                , codigoTpar _
                , montoCparc _
                , cuotasCparc _
                , tipoCparc _
                , "0", "", "0")

            lo_Resultado.Add("rpta", lo_Salida(0))
            lo_Resultado.Add("msg", lo_Salida(1))
            lo_Resultado.Add("cod", lo_Salida(2))

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Add("rpta", "-1")
            lo_Resultado.Add("msg", ex.Message)
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function ListarCentroCostosParticipanteD( _
        Optional ByVal codigoCpard As Integer = 0 _
        , Optional ByVal codigoCparc As Integer = 0 _
        , Optional ByVal nroCuotaCpard As Integer = 0 _
        , Optional ByVal fechaVencCpard As Object = Nothing _
        , Optional ByVal montoCpard As Decimal = 0.0 _
    ) As Data.DataTable
        Dim fechaVencCpardDB As Object = IIf(fechaVencCpard <> Nothing, fechaVencCpard, DBNull.Value)

        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("ADM_ListarCentroCostosParticipanteD", _
            codigoCpard _
            , codigoCparc _
            , nroCuotaCpard _
            , fechaVencCpard _
            , montoCpard _
        )
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GuardarCentroCostosParticipanteD( _
        Optional ByVal codigoCpard As Integer = 0 _
        , Optional ByVal codigoCparc As Integer = 0 _
        , Optional ByVal nrocuotaCpard As Integer = 0 _
        , Optional ByVal fechavencCpard As Date = #1/1/1900# _
        , Optional ByVal montoCpard As Decimal = 0.0 _
    ) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_GuardarCentroCostosParticipanteD" _
                , codigoCpard _
                , codigoCparc _
                , nrocuotaCpard _
                , fechavencCpard _
                , montoCpard _
                , "0", "", "0")

            lo_Resultado.Add("rpta", lo_Salida(0))
            lo_Resultado.Add("msg", lo_Salida(1))
            lo_Resultado.Add("cod", lo_Salida(2))

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Add("rpta", "-1")
            lo_Resultado.Add("msg", ex.Message)
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function EliminarCentroCostosParticipanteD(ByVal codigoCpard As Integer) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_EliminarCentroCostosParticipanteD", codigoCpard, "0", "")

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function ConsultarCargosAbonosPersona(ByVal codigoPso As Integer, ByVal codigoCco As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarCargosPersona", codigoPso, codigoCco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarSolicitudAnulacion(ByVal codigoPso As Integer, ByVal codigoCco As Integer) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ConsultarSolicitudAnulacionPorCentroCosto", codigoPso, codigoCco)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ListarMotivoAnulacion() As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("EVE_ListarMotivoAnulacion")
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function RegistrarSoliciturAnulacion(ByVal codigoPso As Integer _
        , ByVal codigoMno As Integer _
        , ByVal observacion As String _
        , ByVal suma As Decimal _
        , ByVal codigoUsu As Integer _
        , Optional ByVal grwDetalle As GridView = Nothing _
    ) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("EVE_RegistrarSolicitudAnulacion" _
                , codigoPso _
                , codigoMno _
                , observacion _
                , suma _
                , codigoUsu _
                , 0 _
            )

            If grwDetalle IsNot Nothing Then
                Dim codigoSan As Integer = lo_Salida(0)
                For Each _Row As GridViewRow In grwDetalle.Rows
                    Dim txtCantidad As TextBox = DirectCast(_Row.FindControl("txtCantidad"), TextBox)
                    If txtCantidad.Text.Trim <> "" Then
                        Dim codigoDeu As Integer = grwDetalle.DataKeys.Item(_Row.RowIndex).Values("codigo_Deu")
                        Dim fechaDeuDsa As Date = CType(_Row.Cells(1).Text.Trim, Date)
                        Dim importeDsa As Decimal = CType(txtCantidad.Text.Trim, Decimal)

                        lo_Salida = mo_Cnx.Ejecutar("EVE_RegistrarDetalleSolicitudAnulacion" _
                            , codigoSan _
                            , codigoDeu _
                            , fechaDeuDsa _
                            , importeDsa _
                            , codigoUsu _
                            , 0 _
                        )
                        If lo_Salida(0) = "0" Then
                            mo_Cnx.AbortarTransaccion()
                            lo_Resultado.Item("rpta") = "-1"
                            lo_Resultado.Item("msg") = "Ocurrió un error al registrar la Solicitud de Anulación. Contáctese con desarrollosistemas@usat.edu.pe"
                            Return lo_Resultado
                        End If
                    End If
                Next
            End If

            lo_Resultado.Item("rpta") = "1"
            lo_Resultado.Item("msg") = "Solicitud enviada corréctamente"
            lo_Resultado.Item("cod") = lo_Salida(0)

            mo_Cnx.TerminarTransaccion()
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function ListarTipoDocumentoIdentidad(ByVal tipo As String, ByVal param As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("CRM_TipoDocumentoIdentidad", tipo, param)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function GenerarDeudaInscripcionPreGrado( _
         ByVal codigoAlu As Integer _
        , ByVal validaDeuda As Boolean _
        , ByVal usuarioReg As Integer _
    ) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_GenerarDeudaInscripcionPreGrado" _
                , codigoAlu _
                , validaDeuda _
                , usuarioReg _
                , "0", "", "0")

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)
            lo_Resultado.Item("cod") = lo_Salida(2)

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function GetColumnIndexByName(ByVal row As GridViewRow, ByVal columnName As String) As Integer
        Dim index As Integer = 0

        For Each _cell As DataControlFieldCell In row.Cells
            If TypeOf _cell.ContainingField Is BoundField Then
                If (DirectCast(_cell.ContainingField, BoundField)).DataField.Equals(columnName) Then
                    Exit For
                End If
            End If
            index += 1
        Next

        Return index
    End Function

    Public Function ConsultarSituacionAcademicaPersona(ByVal numeroDocIdentidad As String) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADP_ConsultarSituacionAcademicaPersona", numeroDocIdentidad, "0", "")

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)

            mo_Cnx.TerminarTransaccion()
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function EnviarParametrosMarketing(ByVal dniApoderado As String, ByVal apePatApoderado As String, ByVal apeMatApoderado As String, _
                                    ByVal nombresApoderado As String, ByVal numCelApoderado As String, ByVal emailApoderado As String, _
                                    ByVal dni As String, ByVal apellidoPaterno As String, ByVal apellidoMaterno As String, _
                                    ByVal nombres As String, ByVal numCelular As String, ByVal numFijo As String, ByVal email As String, _
                                    ByVal direccion As String, ByVal departamento As String, ByVal provincia As String, _
                                    ByVal distrito As String, ByVal fecNacimiento As String, ByVal sexo As String, _
                                    ByVal anioEstudio As String, ByVal centroLabores As String, ByVal cargo As String, _
                                    ByVal ruc As String, ByVal departamentoInstEduc As String, ByVal institucionEducativa As String, _
                                    ByVal carreraProfesional As String, ByVal consultas As String, ByVal tipo As String, _
                                    ByVal campoAdicional1 As String, ByVal campoAdicional2 As String, ByVal campoAdicional3 As String, _
                                    ByVal campoAdicional4 As String, ByVal campoAdicional5 As String, ByVal campoAdicional6 As String) As String
        Try
            Using _client As New Net.WebClient
                Dim lo_Credentials As New NetworkCredential("marketing", "USAT2015")
                _client.Credentials = lo_Credentials
                Dim lo_ReqParam As New Specialized.NameValueCollection
                lo_ReqParam.Item("dniApoderado") = dniApoderado
                lo_ReqParam.Item("apePatApoderado") = apePatApoderado
                lo_ReqParam.Item("apeMatApoderado") = apeMatApoderado
                lo_ReqParam.Item("nombresApoderado") = nombresApoderado
                lo_ReqParam.Item("numCelApoderado") = numCelApoderado
                lo_ReqParam.Item("emailApoderado") = emailApoderado
                lo_ReqParam.Item("dni") = dni
                lo_ReqParam.Item("apellidoPaterno") = apellidoPaterno
                lo_ReqParam.Item("apellidoMaterno") = apellidoMaterno
                lo_ReqParam.Item("nombres") = nombres
                lo_ReqParam.Item("numCelular") = numCelular
                lo_ReqParam.Item("numFijo") = numFijo
                lo_ReqParam.Item("email") = email
                lo_ReqParam.Item("direccion") = direccion
                lo_ReqParam.Item("departamento") = departamento
                lo_ReqParam.Item("provincia") = provincia
                lo_ReqParam.Item("distrito") = distrito
                lo_ReqParam.Item("fecNacimiento") = fecNacimiento
                lo_ReqParam.Item("sexo") = sexo
                lo_ReqParam.Item("anioEstudio") = anioEstudio
                lo_ReqParam.Item("centroLabores") = centroLabores
                lo_ReqParam.Item("cargo") = cargo
                lo_ReqParam.Item("ruc") = ruc
                lo_ReqParam.Item("departamentoInstEduc") = departamentoInstEduc
                lo_ReqParam.Item("institucionEducativa") = institucionEducativa
                lo_ReqParam.Item("carreraProfesional") = carreraProfesional
                lo_ReqParam.Item("consultas") = consultas
                lo_ReqParam.Item("tipo") = tipo
                lo_ReqParam.Item("campoAdicional1") = campoAdicional1
                lo_ReqParam.Item("campoAdicional2") = campoAdicional2
                lo_ReqParam.Item("campoAdicional3") = campoAdicional3
                lo_ReqParam.Item("campoAdicional4") = campoAdicional4
                lo_ReqParam.Item("campoAdicional5") = campoAdicional5
                lo_ReqParam.Item("campoAdicional6") = campoAdicional6

                Dim lo_ResponseBytes As Byte() = _client.UploadValues("http://www.tuproyectodevida.pe/autorespuesta/auto_prueba.php", "POST", lo_ReqParam)
                Dim lo_ResponseBody As String = (New Text.UTF8Encoding).GetString(lo_ResponseBytes)
                Return lo_ResponseBody

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function BuscarEmailInteresadoCoincidente(ByVal email As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("CRM_ListaEMail", "LTXC", "0", email)
            mo_Cnx.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try
        Return lo_Dts
    End Function

    Public Function ValidarEmail(ByVal email As String) As Dictionary(Of String, String)
        Dim lo_Respuesta As New Dictionary(Of String, String)

        Try

            Dim secret As String = "iofafn3rsgczdpvz1isr3"
            Dim path As String = "https://apps.emaillistverify.com/api/verifEmail?secret=" & secret & "&email=" & email

            Dim Tls12 As SecurityProtocolType = 3072
            ServicePointManager.SecurityProtocol = Tls12

            Dim webClient As New WebClient
            Dim result As String = webClient.DownloadString(path)

            If result = "ok" Then
                lo_Respuesta.Item("rpta") = "1"
            Else
                lo_Respuesta.Item("rpta") = "0"
            End If

            Select Case result
                Case "ok"
                    lo_Respuesta.Item("msg") = "La dirección de correo electrónico suministrada ha pasado todas las pruebas de verificación"
                Case "fail"
                    lo_Respuesta.Item("msg") = "La dirección de correo electrónico suministrada ha fallado una o más pruebas"
                Case "unknown"
                    lo_Respuesta.Item("msg") = "La dirección de correo electrónico suministrada no puede ser probada con precisión"
                Case "invalid_syntax", "incorrect"
                    lo_Respuesta.Item("msg") = "No se proporcionó ninguna dirección de correo electrónico en la solicitud o contiene un error de sintaxis"
                Case "key_not_valid"
                    lo_Respuesta.Item("msg") = "No se proporcionó una clave de api en la solicitud o no fue válida"
                Case "missing parameters"
                    lo_Respuesta.Item("msg") = "Faltan parámetros para realizar la validación."
                Case Else
                    lo_Respuesta.Item("msg") = "No se ha podido validar el email"
            End Select

        Catch ex As Exception
            lo_Respuesta.Item("rpta") = "-1"
            lo_Respuesta.Item("msg") = ex.Message
        End Try

        Return lo_Respuesta
    End Function

    Public Function ConsultarDatosUsuario(ByVal tipo As String, ByVal codigoPer As String) As Data.DataTable
        Dim lo_Dts As New Data.DataTable
        mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        mo_Cnx.AbrirConexion()
        lo_Dts = mo_Cnx.TraerDataTable("spPla_ConsultarDatosPersonal", tipo, codigoPer)
        mo_Cnx.CerrarConexion()
        Return lo_Dts
    End Function

    Public Function ConsultarRequisitosAdmisionPorModalidad(ByVal codigoMin As Integer) As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ADM_ConsultarRequisitosAdmisionPorModalidad", codigoMin)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GuardarRequisitoAdmision(ByVal codigoReq As Integer _
        , ByVal descripcionReq As String _
        , ByVal codigosMin As String _
        , ByVal codUsuario As Integer _
        , Optional ByVal grwDetalle As GridView = Nothing _
    ) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_GuardarRequisitoAdmision" _
                , codigoReq _
                , descripcionReq _
                , codigosMin _
                , codUsuario _
                , 0 _
                , "" _
                , 0 _
            )

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)
            lo_Resultado.Item("cod") = lo_Salida(2)

            mo_Cnx.TerminarTransaccion()
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function RequisitoModalidadListar(ByVal tipoConsulta As String, Optional ByVal codigoRmo As String = "", Optional ByVal codigoReq As Integer = 0, _
                                             Optional ByVal codigoMin As Integer = 0, Optional ByVal usuarioReg As Integer = 0) As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ADM_RequisitoModalidad_Listar", tipoConsulta, codigoRmo, codigoReq, codigoMin, usuarioReg)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RequisitoListar(ByVal tipoConsulta As String, Optional ByVal codigoReq As Integer = 0, Optional ByVal descripcionReq As String = "", _
                                    Optional ByVal usuarioReg As Integer = 0) As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ADM_Requisito_Listar", tipoConsulta, codigoReq, descripcionReq, usuarioReg)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ModalidadIngresoListar(ByVal tipoConsulta As String, Optional ByVal codigoMin As Integer = 0, Optional ByVal nombreMin As String = "", _
                                    Optional ByVal abreviaturaMin As String = "") As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ACAD_ModalidadIngreso_Listar", tipoConsulta, codigoMin, nombreMin, abreviaturaMin)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ModalidadIngresoTipoEstudioListar(ByVal tipoConsulta As String, Optional ByVal codigoTest As Integer = 0, Optional ByVal codigoMin As Integer = 0, _
                                    Optional ByVal ingresoDirecto As Boolean = False) As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ACAD_ModalidadIngresoTipoEstudio_Listar", tipoConsulta, codigoTest, codigoMin, ingresoDirecto)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function RequisitoIUD(ByVal operacion As String, _
                                 ByVal codigoReq As Integer, _
                                 ByVal codUsuario As Integer, _
                                 Optional ByVal descripcionReq As String = "") As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)

        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_Requisito_IUD" _
                                                        , operacion _
                                                        , codigoReq _
                                                        , descripcionReq _
                                                        , codUsuario _
                                                        , 0 _
                                                        , "" _
                                                        , 0)

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)
            lo_Resultado.Item("cod") = lo_Salida(2)

            mo_Cnx.TerminarTransaccion()
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function TipoEstudioListar(ByVal tipoConsulta As String, Optional ByVal codigoTest As String = "") As Data.DataTable
        Try
            Dim lo_Dts As New Data.DataTable
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            lo_Dts = mo_Cnx.TraerDataTable("ALUMNI_TipoEstudioListar", tipoConsulta, codigoTest)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ListarIngresantesPorCentroCosto(ByVal tipoConsulta As String, ByVal codigoCco As Integer) As Data.DataTable
        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.AbrirConexion()
            Dim lo_Dts As Data.DataTable = mo_Cnx.TraerDataTable("ADM_ListarIngresantesPorCentroCosto", tipoConsulta, codigoCco)
            mo_Cnx.CerrarConexion()
            Return lo_Dts
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AnularCargosPendientesPorCentroCostos(ByVal operacion As String, ByVal codigoCco As Integer) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)

        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_AnularCargosPendientesPorCentroCostos", operacion, codigoCco)

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)

            mo_Cnx.TerminarTransaccion()
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

    Public Function InactivarNoIngresantesPorCentroCostos(ByVal operacion As String, ByVal codigoCco As Integer) As Dictionary(Of String, String)
        Dim lo_Resultado As New Dictionary(Of String, String)

        Try
            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            mo_Cnx.IniciarTransaccion()

            Dim lo_Salida As Object() = mo_Cnx.Ejecutar("ADM_InactivarNoIngresantesPorCentroCostos", operacion, codigoCco)

            lo_Resultado.Item("rpta") = lo_Salida(0)
            lo_Resultado.Item("msg") = lo_Salida(1)

            mo_Cnx.TerminarTransaccion()
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lo_Resultado.Item("rpta") = "-1"
            lo_Resultado.Item("msg") = ex.Message
            Return lo_Resultado
        End Try
        Return lo_Resultado
    End Function

#Region "VARIABLES_GLOBALES"
    '5531: andy.diaz | 473: Sra. Julia
    Public Shared CodigoJefaPensiones As String = IIf(ConfigurationManager.AppSettings("CorreoUsatActivo") = "0", 5531, 473)
    Public Shared CorreoServiciosTI As String = "servicios.ti@usat.edu.pe"
#End Region
End Class


