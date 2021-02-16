Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System

Public Class ClsProcesamientoResultados

#Region "Capa de Entidades"

    Public Class e_EvaluacionAlumno
        Private _tipoConsulta As String
        Public Property tipoConsulta() As String
            Get
                Return _tipoConsulta
            End Get
            Set(ByVal value As String)
                _tipoConsulta = value
            End Set
        End Property

        Private _operacion As String
        Public Property operacion() As String
            Get
                Return _operacion
            End Get
            Set(ByVal value As String)
                _operacion = value
            End Set
        End Property

        Private _codigoElu As String
        Public Property codigoElu() As String
            Get
                Return _codigoElu
            End Get
            Set(ByVal value As String)
                _codigoElu = value
            End Set
        End Property

        Private _codigoEvl As Integer
        Public Property codigoEvl() As Integer
            Get
                Return _codigoEvl
            End Get
            Set(ByVal value As Integer)
                _codigoEvl = value
            End Set
        End Property

        Private _codigoAlu As Integer
        Public Property codigoAlu() As Integer
            Get
                Return _codigoAlu
            End Get
            Set(ByVal value As Integer)
                _codigoAlu = value
            End Set
        End Property

        Private _notaElu As Decimal
        Public Property notaElu() As Decimal
            Get
                Return _notaElu
            End Get
            Set(ByVal value As Decimal)
                _notaElu = value
            End Set
        End Property

        Private _estadoNotaElu As String
        Public Property estadoNotaElu() As String
            Get
                Return _estadoNotaElu
            End Get
            Set(ByVal value As String)
                _estadoNotaElu = value
            End Set
        End Property

        Private _condicionIngresoElu As String
        Public Property condicionIngresoElu() As String
            Get
                Return _condicionIngresoElu
            End Get
            Set(ByVal value As String)
                _condicionIngresoElu = value
            End Set
        End Property

        Private _estadoVerificacionElu As String
        Public Property estadoVerificacionElu() As String
            Get
                Return _estadoVerificacionElu
            End Get
            Set(ByVal value As String)
                _estadoVerificacionElu = value
            End Set
        End Property

        Private _observacionElu As String
        Public Property observacionElu() As String
            Get
                Return _observacionElu
            End Get
            Set(ByVal value As String)
                _observacionElu = value
            End Set
        End Property

        Private _codigoPerReg As Integer
        Public Property codigoPerReg() As Integer
            Get
                Return _codigoPerReg
            End Get
            Set(ByVal value As Integer)
                _codigoPerReg = value
            End Set
        End Property

        Private _fechaReg As Object
        Public Property fechaReg() As Object
            Get
                Return _fechaReg
            End Get
            Set(ByVal value As Object)
                _fechaReg = value
            End Set
        End Property

        Private _codigoPerAct As Integer
        Public Property codigoPerAct() As Integer
            Get
                Return _codigoPerAct
            End Get
            Set(ByVal value As Integer)
                _codigoPerAct = value
            End Set
        End Property

        Private _fechaAct As Object
        Public Property fechaAct() As Object
            Get
                Return _fechaAct
            End Get
            Set(ByVal value As Object)
                _fechaAct = value
            End Set
        End Property

        Private _codUsuario As Integer
        Public Property codUsuario() As Integer
            Get
                Return _codUsuario
            End Get
            Set(ByVal value As Integer)
                _codUsuario = value
            End Set
        End Property

        Private _ruta As String
        Public Property ruta() As String
            Get
                Return _ruta
            End Get
            Set(ByVal value As String)
                _ruta = value
            End Set
        End Property

        Private _codigoCco As Integer
        Public Property codigoCco() As Integer
            Get
                Return _codigoCco
            End Get
            Set(ByVal value As Integer)
                _codigoCco = value
            End Set
        End Property

        Private _codigoCpf As Integer
        Public Property codigoCpf() As Integer
            Get
                Return _codigoCpf
            End Get
            Set(ByVal value As Integer)
                _codigoCpf = value
            End Set
        End Property

        Private _codigoMin As Integer
        Public Property codigoMin() As Integer
            Get
                Return _codigoMin
            End Get
            Set(ByVal value As Integer)
                _codigoMin = value
            End Set
        End Property

        Private _idArchivoCompartido As Integer
        Public Property idArchivoCompartido() As Integer
            Get
                Return _idArchivoCompartido
            End Get
            Set(ByVal value As Integer)
                _idArchivoCompartido = value
            End Set
        End Property

        Private _respuesta_elu As String
        Public Property respuesta_elu() As String
            Get
                Return _respuesta_elu
            End Get
            Set(ByVal value As String)
                _respuesta_elu = value
            End Set
        End Property

        Public leRespuesta As List(Of e_EvaluacionAlumno_Respuesta)

        Private _puntaje_elu As Decimal
        Public Property puntaje_elu() As Decimal
            Get
                Return _puntaje_elu
            End Get
            Set(ByVal value As Decimal)
                _puntaje_elu = value
            End Set
        End Property

        Private _notaFinal_elu As Decimal
        Public Property notaFinal_elu() As Decimal
            Get
                Return _notaFinal_elu
            End Get
            Set(ByVal value As Decimal)
                _notaFinal_elu = value
            End Set
        End Property

        Private _puntajeFinal_elu As Decimal
        Public Property puntajeFinal_elu() As Decimal
            Get
                Return _puntajeFinal_elu
            End Get
            Set(ByVal value As Decimal)
                _puntajeFinal_elu = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            operacion = "I"
            codigoElu = ""
            codigoEvl = 0
            codigoAlu = 0
            notaElu = 0.0
            estadoNotaElu = ""
            condicionIngresoElu = ""
            estadoVerificacionElu = ""
            observacionElu = ""
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
            ruta = ""
            codigoCco = 0
            codigoCpf = 0
            codigoMin = 0
            idArchivoCompartido = -1
            respuesta_elu = ""
            leRespuesta = New List(Of e_EvaluacionAlumno_Respuesta)
            puntaje_elu = 0.0
            notaFinal_elu = 0.0
            puntajeFinal_elu = 0.0
        End Sub
    End Class

    Public Class e_EvaluacionAlumno_Respuesta

        Private _tipoConsulta As String
        Public Property tipoConsulta() As String
            Get
                Return _tipoConsulta
            End Get
            Set(ByVal value As String)
                _tipoConsulta = value
            End Set
        End Property

        Private _codigoEar As String
        Public Property codigoEar() As String
            Get
                Return _codigoEar
            End Get
            Set(ByVal value As String)
                _codigoEar = value
            End Set
        End Property

        Private _codigoElu As String
        Public Property codigoElu() As String
            Get
                Return _codigoElu
            End Get
            Set(ByVal value As String)
                _codigoElu = value
            End Set
        End Property

        Private _codigoEvd As String
        Public Property codigoEvd() As String
            Get
                Return _codigoEvd
            End Get
            Set(ByVal value As String)
                _codigoEvd = value
            End Set
        End Property

        Private _codigoAle As String
        Public Property codigoAle() As String
            Get
                Return _codigoAle
            End Get
            Set(ByVal value As String)
                _codigoAle = value
            End Set
        End Property

        Private _respuestaEar As String
        Public Property respuesta_ear() As String
            Get
                Return _respuestaEar
            End Get
            Set(ByVal value As String)
                _respuestaEar = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            _respuestaEar = ""
            _codigoEar = 0
            _codigoElu = 0
            _codigoEvd = 0
            _codigoAle = 0
        End Sub

    End Class

    Public Class e_CierreResultadosAdmision
        Private _tipoConsulta As String
        Public Property tipoConsulta() As String
            Get
                Return _tipoConsulta
            End Get
            Set(ByVal value As String)
                _tipoConsulta = value
            End Set
        End Property

        Private _operacion As String
        Public Property operacion() As String
            Get
                Return _operacion
            End Get
            Set(ByVal value As String)
                _operacion = value
            End Set
        End Property

        Private _codigoCra As String
        Public Property codigoCra() As String
            Get
                Return _codigoCra
            End Get
            Set(ByVal value As String)
                _codigoCra = value
            End Set
        End Property

        Private _codigoCac As Integer
        Public Property codigoCac() As Integer
            Get
                Return _codigoCac
            End Get
            Set(ByVal value As Integer)
                _codigoCac = value
            End Set
        End Property

        Private _codigoMin As Integer
        Public Property codigoMin() As Integer
            Get
                Return _codigoMin
            End Get
            Set(ByVal value As Integer)
                _codigoMin = value
            End Set
        End Property

        Private _codigoCpf As Integer
        Public Property codigoCpf() As Integer
            Get
                Return _codigoCpf
            End Get
            Set(ByVal value As Integer)
                _codigoCpf = value
            End Set
        End Property

        Private _codigoCco As Integer
        Public Property codigoCco() As Integer
            Get
                Return _codigoCco
            End Get
            Set(ByVal value As Integer)
                _codigoCco = value
            End Set
        End Property

        Private _codigoPerReg As Integer
        Public Property codigoPerReg() As Integer
            Get
                Return _codigoPerReg
            End Get
            Set(ByVal value As Integer)
                _codigoPerReg = value
            End Set
        End Property

        Private _fechaReg As Object
        Public Property fechaReg() As Object
            Get
                Return _fechaReg
            End Get
            Set(ByVal value As Object)
                _fechaReg = value
            End Set
        End Property

        Private _codigoPerAct As Integer
        Public Property codigoPerAct() As Integer
            Get
                Return _codigoPerAct
            End Get
            Set(ByVal value As Integer)
                _codigoPerAct = value
            End Set
        End Property

        Private _fechaAct As Object
        Public Property fechaAct() As Object
            Get
                Return _fechaAct
            End Get
            Set(ByVal value As Object)
                _fechaAct = value
            End Set
        End Property

        Private _codUsuario As Integer
        Public Property codUsuario() As Integer
            Get
                Return _codUsuario
            End Get
            Set(ByVal value As Integer)
                _codUsuario = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            operacion = "I"
            codigoCra = ""
            codigoCac = 0
            codigoMin = 0
            codigoCpf = 0
            codigoCco = 0
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
        End Sub
    End Class

#End Region

#Region "Capa de Datos"

    Public Class d_EvaluacionAlumno

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_CargarExcelNotas(ByVal obj As e_EvaluacionAlumno) As Dictionary(Of String, String)
            Dim lo_Resultado As New Dictionary(Of String, String)
            Try
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ToString
                cnx.AbrirConexion()
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_ProcesarResultados_Test", _
                                                         obj.codigoEvl, _
                                                         obj.ruta, _
                                                         obj.idArchivoCompartido, _
                                                         obj.codigoPerReg, _
                                                         0, _
                                                         "")
                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
            Catch ex As Exception
                lo_Resultado.Item("rpta") = "-1"
                lo_Resultado.Item("msg") = ex.Message
            End Try
            Return lo_Resultado
        End Function

        Public Function fc_Listar(ByVal obj As e_EvaluacionAlumno) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Alumno_Listar" _
                                            , .tipoConsulta _
                                            , .codigoElu _
                                            , .codigoEvl _
                                            , .codigoAlu _
                                            , .notaElu _
                                            , .estadoNotaElu _
                                            , .condicionIngresoElu _
                                            , .estadoVerificacionElu _
                                            , .observacionElu _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            , .respuesta_elu _
                                            , .puntaje_elu _
                                            , .notaFinal_elu _
                                            , .puntajeFinal_elu _
                                            , .codigoCco _
                                            , .codigoCpf _
                                            , .codigoMin _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_EvaluacionAlumno) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_Evaluacion_Alumno_IUD", _
                                        .operacion _
                                        , .codigoElu _
                                        , .codigoEvl _
                                        , .codigoAlu _
                                        , .notaElu _
                                        , .estadoNotaElu _
                                        , .condicionIngresoElu _
                                        , .estadoVerificacionElu _
                                        , .observacionElu _
                                        , .respuesta_elu _
                                        , .puntaje_elu _
                                        , .notaFinal_elu _
                                        , .puntajeFinal_elu _
                                        , .codUsuario, _
                                        "0", "", "0")
                End With

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

        Public Function fc_InsertarMasivo(ByVal obj As e_EvaluacionAlumno) As System.Data.DataTable
            Try
                Dim odRespuesta As New d_EvaluacionAlumno_Respuesta
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    For Each rpta As e_EvaluacionAlumno_Respuesta In .leRespuesta
                        dt = odRespuesta.fc_InsertarMasivo(rpta)
                    Next

                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_CalcularNotasEvaluacion(ByVal codigoEvl As Integer, ByVal codigoCco As Integer, ByVal codigoMin As Integer, _
                                                   ByVal codigoCpf As Integer, ByVal codUsuario As Integer) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_CalcularNotasEvaluacion", _
                                                         codigoEvl _
                                                         , codigoCco _
                                                         , codigoMin _
                                                         , codigoCpf _
                                                         , codUsuario _
                                                         , "0", "")

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function fc_ProcesarCondicionesIngreso(ByVal codigoCco As Integer, ByVal codigoCac As Integer, ByVal codigoCpf As Integer, _
                                                     ByVal codigoMin As Integer, ByVal notaMin As Decimal, ByVal cantidadAccesitarios As Integer, _
                                                     ByVal codUsuario As Integer) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_ProcesarCondicionesIngreso", _
                                                         codigoCco _
                                                         , codigoCac _
                                                         , codigoCpf _
                                                         , codigoMin _
                                                         , notaMin _
                                                         , cantidadAccesitarios _
                                                         , codUsuario _
                                                         , "0", "")

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        Public Function fc_ConfirmarEvaluaciones(ByVal le As List(Of e_EvaluacionAlumno)) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                For Each obj As e_EvaluacionAlumno In le
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Alumno_Conformidad", obj.codigoElu, obj.estadoVerificacionElu, obj.observacionElu, obj.codUsuario)
                Next
                cnx.CerrarConexion()
                Return dt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function fc_ProcesarResultadoEvaluacion(ByVal codigoEvl As Integer, ByVal codUsuario As Integer) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_ProcesarResultadosEvaluacion", _
                                                         codigoEvl _
                                                         , codUsuario _
                                                         , "0", "")

                cnx.TerminarTransaccion()

                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_EvaluacionAlumno_Respuesta

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_EvaluacionAlumno_Respuesta) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Alumno_Respuesta_Listar" _
                                            , .tipoConsulta _
                                            , .codigoEar _
                                            , .codigoElu _
                                            , .codigoEvd _
                                            , .codigoAle _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_InsertarMasivo(ByVal obj As e_EvaluacionAlumno_Respuesta) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_Evaluacion_Alumno_Respuesta_InsertarMasivo", .codigoElu, .respuesta_ear)
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

    End Class

    Public Class d_CierreResultadosAdmision
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_CierreResultadosAdmision) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_CierreResultadosAdmision_Listar" _
                                            , .tipoConsulta _
                                            , .codigoCra _
                                            , .codigoCac _
                                            , .codigoMin _
                                            , .codigoCpf _
                                            , .codigoCco _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_CierreResultadosAdmision) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_CierreResultadosAdmision_IUD", _
                                        .operacion, _
                                        .codigoCra, _
                                        .codigoCac, _
                                        .codigoMin, _
                                        .codigoCpf, _
                                        .codigoCco, _
                                        .codUsuario, _
                                        "0", "", "0")
                End With

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

        Public Function fc_ConfirmarResultadosAdmision(ByVal obj As e_CierreResultadosAdmision) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_ConfirmarResultadosAdmision", _
                                        .codigoCac, _
                                        .codigoMin, _
                                        .codigoCpf, _
                                        .codigoCco, _
                                        .codUsuario, _
                                        "0", "", "0")
                End With

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
    End Class

#End Region

End Class
