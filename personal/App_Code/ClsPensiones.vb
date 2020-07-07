Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class ClsPensiones
#Region "ENTIDADES"

    Public Class e_PenAlumno
        Public codigoAlu As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            codigoAlu = ""
        End Sub
    End Class

    Public Class e_PenCicloAcademico
        Public operacion As String
        Public codigo_Cac As String
        Public descripcion_Cac As String
        Public tipo_Cac As String
        Public fechaIni_Cac As Object
        Public fechaFin_Cac As Object
        Public numeroDoc_Cac As String
        Public fechaDoc_Cac As Object
        Public vigencia_Cac As Boolean
        Public notaMinima_Cac As Decimal
        Public moraDiaria_Cac As Decimal
        Public vigenciaaux_cac As Boolean
        Public FechaIniClases_cac As Object
        Public FechaFinClases_cac As Object
        Public admision_cac As Boolean
        Public admisionaux_cac As Boolean

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigo_Cac = ""
            descripcion_Cac = ""
            tipo_Cac = ""
            fechaIni_Cac = DBNull.Value
            fechaFin_Cac = DBNull.Value
            numeroDoc_Cac = ""
            fechaDoc_Cac = DBNull.Value
            vigencia_Cac = False
            notaMinima_Cac = 0.0
            moraDiaria_Cac = 0.0
            vigenciaaux_cac = False
            FechaIniClases_cac = DBNull.Value
            FechaFinClases_cac = DBNull.Value
            admision_cac = False
            admisionaux_cac = False
        End Sub
    End Class

    Public Class e_PenConfiguracionProgramacionCargo
        Public operacion As String
        Public codigo_cpc As String
        Public nombre_cpc As String
        Public procedimiento_cpc As String
        Public filtrosAlumno_cpc As String
        Public otrosParametros_cpc As String
        Public estado_cpc As String
        Public usuarioReg_cpc As Integer
        Public fechaHoraReg_cpc As Object
        Public usuarioMod_cpc As Integer
        Public fechaHoraMod_cpc As Object

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigo_cpc = ""
            nombre_cpc = ""
            procedimiento_cpc = ""
            filtrosAlumno_cpc = ""
            otrosParametros_cpc = ""
            estado_cpc = "A"
            usuarioReg_cpc = 0
            fechaHoraReg_cpc = DBNull.Value
            usuarioMod_cpc = 0
            fechaHoraMod_cpc = DBNull.Value
        End Sub
    End Class

    Public Class e_PenTipoEstudio
        Public operacion As String
        Public codigoTest As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoTest = ""
        End Sub
    End Class

    Public Class e_PenCarreraProfesional
        Public operacion As String
        Public codigoCpf As String
        Public codigoTest As String
        Public nombreCpf As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoCpf = ""
            codigoTest = ""
            nombreCpf = ""
        End Sub
    End Class

    Public Class e_PenServicioConcepto
        Public operacion As String
        Public param As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            param = ""
        End Sub
    End Class

    Public Class e_PenCentroCosto
        Public operacion As String
        Public codigoSco As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoSco = ""
        End Sub
    End Class

    Public Class e_PenProgramacionCargoMasivo
        Public operacion As String
        Public codigo_pcm As String
        Public tipoConfiguracion_pcm As String
        Public codigo_cac As Integer
        Public descripcion_pcm As String
        Public tipoEjecucion_pcm As String
        Public codigo_cpc As Object
        Public fechaHoraInicio_pcm As Object
        Public fechaHoraFin_pcm As Object
        Public periodicidad_pcm As Integer
        Public tipoSeleccion_pcm As String
        Public filtrosAlumno_pcm As String
        Public habilitado_pcm As Object
        Public estado_pcm As String
        Public cod_usuario As Integer
        Public fechaHoraReg_pcm As Object
        Public fechaHoraMod_pcm As Object

        Public Sub New()
            Inicializar()
        End Sub

        Private Sub Inicializar()
            operacion = ""
            codigo_pcm = ""
            tipoConfiguracion_pcm = ""
            codigo_cac = 0
            descripcion_pcm = ""
            tipoEjecucion_pcm = ""
            codigo_cpc = 0
            fechaHoraInicio_pcm = DBNull.Value
            fechaHoraFin_pcm = DBNull.Value
            periodicidad_pcm = 0
            tipoSeleccion_pcm = ""
            filtrosAlumno_pcm = ""
            habilitado_pcm = DBNull.Value
            estado_pcm = "A"
            cod_usuario = 0
            fechaHoraReg_pcm = DBNull.Value
            fechaHoraMod_pcm = DBNull.Value
        End Sub
    End Class

    Public Class e_PenDatosCargoMasivo
        Public operacion As String
        Public codigo_dcm As String
        Public tipo_dcm As String
        Public configuracion_dcm As String
        Public importe_dcm As Decimal
        Public fechaVencimiento_dcm As String
        Public observacion_dcm As String
        Public codigo_sco As Integer
        Public codigo_cco As Integer
        Public codigo_pcm As Integer
        Public estado_dcm As String
        Public cod_usuario As Integer
        Public fechaHoraReg_dcm As Object
        Public fechaHoraMod_dcm As Object

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigo_dcm = ""
            tipo_dcm = ""
            configuracion_dcm = ""
            importe_dcm = 0
            fechaVencimiento_dcm = ""
            observacion_dcm = ""
            codigo_sco = 0
            codigo_cco = 0
            codigo_pcm = 0
            estado_dcm = ""
            cod_usuario = 0
            fechaHoraReg_dcm = DBNull.Value
            fechaHoraMod_dcm = DBNull.Value
        End Sub
    End Class

    Public Class e_PenTipoProcesoCargo
        Public operacion As String
        Public codigo_tpc As String
        Public nombre_tpc As String
        Public filtros_tpc As String
        Public formulario_tpc As String
        Public procedimiento_tpc As String
        Public estado_tpc As String
        Public usuarioReg_tpc As Integer
        Public fechaHoraReg_tpc As Object
        Public usuarioMod_tpc As Integer
        Public fechaHoraMod_tpc As Object

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigo_tpc = ""
            nombre_tpc = ""
            filtros_tpc = ""
            formulario_tpc = ""
            procedimiento_tpc = ""
            estado_tpc = ""
            usuarioReg_tpc = 0
            fechaHoraReg_tpc = DBNull.Value
            usuarioMod_tpc = 0
            fechaHoraMod_tpc = DBNull.Value
        End Sub
    End Class

#End Region

#Region "DATOS"
    Public Class d_PenTipoProcesoCargo
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal lo_Dcm As e_PenTipoProcesoCargo) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PEN_TipoProcesoCargo_Listar", _
                                        lo_Dcm.operacion, _
                                        lo_Dcm.codigo_tpc, _
                                        lo_Dcm.nombre_tpc, _
                                        lo_Dcm.filtros_tpc, _
                                        lo_Dcm.formulario_tpc, _
                                        lo_Dcm.procedimiento_tpc, _
                                        lo_Dcm.estado_tpc, _
                                        lo_Dcm.usuarioReg_tpc, _
                                        lo_Dcm.fechaHoraReg_tpc, _
                                        lo_Dcm.usuarioMod_tpc, _
                                        lo_Dcm.fechaHoraMod_tpc)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PenDatosCargoMasivo
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function ListarDatosCargoMasivo(ByVal lo_Dcm As e_PenDatosCargoMasivo) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PEN_DatosCargoMasivo_Listar", _
                                        lo_Dcm.operacion, _
                                        lo_Dcm.codigo_dcm, _
                                        lo_Dcm.tipo_dcm, _
                                        lo_Dcm.configuracion_dcm, _
                                        lo_Dcm.importe_dcm, _
                                        lo_Dcm.fechaVencimiento_dcm, _
                                        lo_Dcm.observacion_dcm, _
                                        lo_Dcm.codigo_sco, _
                                        lo_Dcm.codigo_cco, _
                                        lo_Dcm.codigo_pcm, _
                                        lo_Dcm.estado_dcm, _
                                        lo_Dcm.cod_usuario, _
                                        lo_Dcm.fechaHoraReg_dcm, _
                                        lo_Dcm.cod_usuario, _
                                        lo_Dcm.fechaHoraMod_dcm)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        'Public Function DatosCargoMasivoIUD(ByVal lo_Dcm As e_PenDatosCargoMasivo) As Dictionary(Of String, String)
        '    Try
        '        Dim lo_Resultado As New Dictionary(Of String, String)
        '        cnx = New ClsConectarDatos : dt = New Data.DataTable
        '        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        '        cnx.IniciarTransaccion()

        '        'Ejecutar Procedimiento
        '        Dim lo_Salida As Object() = cnx.Ejecutar("PEN_DatosCargoMasivo_IUD", _
        '                                lo_Dcm.operacion, _
        '                                lo_Dcm.codigo_pcm, _
        '                                lo_Dcm.tipo_dcm, _
        '                                lo_Dcm.importe_dcm, _
        '                                lo_Dcm.fechaVencimiento_dcm, _
        '                                lo_Dcm.observacion_dcm, _
        '                                lo_Dcm.codigo_sco, _
        '                                lo_Dcm.codigo_cco, _
        '                                lo_Dcm.codigo_pcm, _
        '                                lo_Dcm.estado_dcm, _
        '                                lo_Dcm.cod_usuario, _
        '                                "0", "", "0")
        '        cnx.TerminarTransaccion()

        '        lo_Resultado.Item("rpta") = lo_Salida(0)
        '        lo_Resultado.Item("msg") = lo_Salida(1)
        '        lo_Resultado.Item("cod") = lo_Salida(2)

        '        Return lo_Resultado
        '    Catch ex As Exception
        '        cnx.AbortarTransaccion()
        '        Throw ex
        '    End Try
        'End Function
    End Class

    Public Class d_PenProgramacionCargoMasivo
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function GuardarProgramacion(ByVal lo_Pcm As e_PenProgramacionCargoMasivo, ByVal lo_Dcm As e_PenDatosCargoMasivo) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                Dim md_DatosCargoMasivo As New d_PenDatosCargoMasivo

                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                Dim lo_Salida As Object() = cnx.Ejecutar("PEN_ProgramacionCargoMasivo_IUD", _
                                        lo_Pcm.operacion, _
                                        lo_Pcm.codigo_pcm, _
                                        lo_Pcm.tipoConfiguracion_pcm, _
                                        lo_Pcm.codigo_cac, _
                                        lo_Pcm.descripcion_pcm, _
                                        lo_Pcm.tipoEjecucion_pcm, _
                                        lo_Pcm.codigo_cpc, _
                                        lo_Pcm.fechaHoraInicio_pcm, _
                                        lo_Pcm.fechaHoraFin_pcm, _
                                        lo_Pcm.periodicidad_pcm, _
                                        lo_Pcm.tipoSeleccion_pcm, _
                                        lo_Pcm.filtrosAlumno_pcm, _
                                        lo_Pcm.habilitado_pcm, _
                                        lo_Pcm.estado_pcm, _
                                        lo_Pcm.cod_usuario, _
                                        "0", "", "0")
                lo_Dcm.codigo_pcm = lo_Salida(2)

                lo_Salida = cnx.Ejecutar("PEN_DatosCargoMasivo_IUD", _
                                        lo_Dcm.operacion, _
                                        lo_Dcm.codigo_dcm, _
                                        lo_Dcm.tipo_dcm, _
                                        lo_Dcm.configuracion_dcm, _
                                        lo_Dcm.importe_dcm, _
                                        lo_Dcm.fechaVencimiento_dcm, _
                                        lo_Dcm.observacion_dcm, _
                                        lo_Dcm.codigo_sco, _
                                        lo_Dcm.codigo_cco, _
                                        lo_Dcm.codigo_pcm, _
                                        lo_Dcm.estado_dcm, _
                                        lo_Dcm.cod_usuario, _
                                        "0", "", "0")

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function EjecutarProgramacionCargo(ByVal ln_CodigoPcm As Integer, ByVal ls_TipoEjecucionHpc As String, ByVal ln_CodUsuario As Integer) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                Dim md_DatosCargoMasivo As New d_PenDatosCargoMasivo

                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                Dim lo_Salida As Object() = cnx.Ejecutar("PEN_EjecutarProgramacionCargo", ln_CodigoPcm, ls_TipoEjecucionHpc, ln_CodUsuario, "0", "", "0")

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
                lo_Resultado.Item("cod") = lo_Salida(2)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function ListarProgramacionCargoMasivo(ByVal lo_Pcm As e_PenProgramacionCargoMasivo) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PEN_ProgramacionCargoMasivo_Listar", _
                                        lo_Pcm.operacion, _
                                        lo_Pcm.codigo_pcm, _
                                        lo_Pcm.tipoConfiguracion_pcm, _
                                        lo_Pcm.codigo_cac, _
                                        lo_Pcm.descripcion_pcm, _
                                        lo_Pcm.tipoEjecucion_pcm, _
                                        lo_Pcm.codigo_cpc, _
                                        lo_Pcm.fechaHoraInicio_pcm, _
                                        lo_Pcm.fechaHoraFin_pcm, _
                                        lo_Pcm.periodicidad_pcm, _
                                        lo_Pcm.tipoSeleccion_pcm, _
                                        lo_Pcm.filtrosAlumno_pcm, _
                                        lo_Pcm.habilitado_pcm, _
                                        lo_Pcm.estado_pcm, _
                                        lo_Pcm.cod_usuario, _
                                        lo_Pcm.fechaHoraReg_pcm, _
                                        lo_Pcm.cod_usuario, _
                                        lo_Pcm.fechaHoraMod_pcm)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        'Public Function ProgramacionCargoMasivoIUD(ByVal le_Pcm As e_PenProgramacionCargoMasivo) As Dictionary(Of String, String)
        '    Try
        '        Dim lo_Resultado As New Dictionary(Of String, String)
        '        cnx = New ClsConectarDatos : dt = New Data.DataTable
        '        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        '        cnx.IniciarTransaccion()

        '        'Ejecutar Procedimiento
        '        Dim lo_Salida As Object() = cnx.Ejecutar("PEN_ProgramacionCargoMasivo_IUD", _
        '                                le_Pcm.operacion, _
        '                                le_Pcm.codigo_pcm, _
        '                                le_Pcm.tipoConfiguracion_pcm, _
        '                                le_Pcm.codigo_cac, _
        '                                le_Pcm.descripcion_pcm, _
        '                                le_Pcm.tipoEjecucion_pcm, _
        '                                le_Pcm.fechaHoraInicio_pcm, _
        '                                le_Pcm.fechaHoraFin_pcm, _
        '                                le_Pcm.periodicidad_pcm, _
        '                                le_Pcm.filtrosAlumno_pcm, _
        '                                le_Pcm.estado_pcm, _
        '                                le_Pcm.cod_usuario, _
        '                                "0", "", "0")
        '        cnx.TerminarTransaccion()

        '        lo_Resultado.Item("rpta") = lo_Salida(0)
        '        lo_Resultado.Item("msg") = lo_Salida(1)
        '        lo_Resultado.Item("cod") = lo_Salida(2)

        '        Return lo_Resultado
        '    Catch ex As Exception
        '        cnx.AbortarTransaccion()
        '        Throw ex
        '    End Try
        'End Function
    End Class

    Public Class d_PenCentroCosto
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function ConsultarCentroCosto(ByVal le_Cco As e_PenCentroCosto) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ADM_ConsultarCentroCosto", _
                                        le_Cco.operacion, _
                                        le_Cco.codigoSco)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PenServicioConcepto
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function ConsultarServicioConcepto(ByVal le_Sco As e_PenServicioConcepto) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PRESU_ConsultarServicioConcepto", _
                                        le_Sco.operacion, _
                                        le_Sco.param)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PenCarreraProfesional
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal le_CarreraProfesional As e_PenCarreraProfesional) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ACAD_BuscaEscuelaProfesional", _
                                        le_CarreraProfesional.codigoCpf, _
                                        le_CarreraProfesional.codigoTest, _
                                        le_CarreraProfesional.nombreCpf)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PenTipoEstudio
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal le_PenTipoEstudio As e_PenTipoEstudio) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ACAD_ConsultarTipoEstudio", _
                                        le_PenTipoEstudio.operacion, _
                                        le_PenTipoEstudio.codigoTest)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PenCicloAcademico
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal le_CicloAcademico As e_PenCicloAcademico) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()
                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ADM_CicloAcademico_Listar", le_CicloAcademico.operacion, _
                                        le_CicloAcademico.codigo_Cac, _
                                        le_CicloAcademico.descripcion_Cac, _
                                        le_CicloAcademico.tipo_Cac, _
                                        le_CicloAcademico.fechaIni_Cac, _
                                        le_CicloAcademico.fechaFin_Cac, _
                                        le_CicloAcademico.numeroDoc_Cac, _
                                        le_CicloAcademico.fechaDoc_Cac, _
                                        le_CicloAcademico.vigencia_Cac, _
                                        le_CicloAcademico.notaMinima_Cac, _
                                        le_CicloAcademico.moraDiaria_Cac, _
                                        le_CicloAcademico.vigenciaaux_cac, _
                                        le_CicloAcademico.FechaIniClases_cac, _
                                        le_CicloAcademico.FechaFinClases_cac, _
                                        le_CicloAcademico.admision_cac, _
                                        le_CicloAcademico.admisionaux_cac)

                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class dAlumno
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable
    End Class

    Public Class d_PenConfiguracionProgramacionCargo
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal le_Cpc As e_PenConfiguracionProgramacionCargo) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PEN_ConfiguracionProgramacionCargo_Listar", _
                                        le_Cpc.operacion, _
                                        le_Cpc.codigo_cpc, _
                                        le_Cpc.nombre_cpc)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function FiltrarAlumnos(ByVal le_Cpc As e_PenConfiguracionProgramacionCargo) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("PEN_MostrarAlumnosParaGeneracionCargo", le_Cpc.operacion, le_Cpc.codigo_cpc, le_Cpc.filtrosAlumno_cpc)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_Pensiones
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        'OBTENER ULTIMO ID DE ARCHIVO,NOMBRE Y RUTA INSERTARO EN ARCHIVOCOMPARTIDO PARA ACTUALIZAR EN LOS REGISTROS
        Public Function ObtenerUltimoIDArchivoCompartido(ByVal idtabla As Integer, ByVal idtransaccion As String, ByVal nrooperacion As String) As Data.DataTable
            Dim lo_Dts As New Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ObtenerUltimoIDArchvoCompartido", idtabla, idtransaccion, nrooperacion)

                cnx.CerrarConexion()
            Catch ex As Exception
                Throw ex
            End Try

            Return lo_Dts
        End Function

    End Class

#End Region
End Class
