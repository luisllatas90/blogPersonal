Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System

Public Class ClsBancoPreguntas
#Region "Capa de Entidades"
    Public Class e_ComisionPermanente
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

        Private _codigoCop As String
        Public Property codigoCop() As String
            Get
                Return _codigoCop
            End Get
            Set(ByVal value As String)
                _codigoCop = value
            End Set
        End Property

        Private _codigoPer As Integer
        Public Property codigoPer() As Integer
            Get
                Return _codigoPer
            End Get
            Set(ByVal value As Integer)
                _codigoPer = value
            End Set
        End Property

        Private _codigoCcm As Integer
        Public Property codigoCcm() As Integer
            Get
                Return _codigoCcm
            End Get
            Set(ByVal value As Integer)
                _codigoCcm = value
            End Set
        End Property

        Private _nroResolucionCop As String
        Public Property nroResolucionCop() As String
            Get
                Return _nroResolucionCop
            End Get
            Set(ByVal value As String)
                _nroResolucionCop = value
            End Set
        End Property

        Private _vigenteCop As Object
        Public Property vigenteCop() As Object
            Get
                Return _vigenteCop
            End Get
            Set(ByVal value As Object)
                _vigenteCop = value
            End Set
        End Property

        Private _codigosCom As Object
        Public Property codigosCom() As Object
            Get
                Return _codigosCom
            End Get
            Set(ByVal value As Object)
                _codigosCom = value
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
            codigoCop = ""
            codigoPer = 0
            codigoCcm = 0
            nroResolucionCop = ""
            vigenteCop = DBNull.Value
            codigosCom = DBNull.Value
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
        End Sub
    End Class

    Public Class e_CargoComision
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

        Private _codigoCcm As String
        Public Property codigoCcm() As String
            Get
                Return _codigoCcm
            End Get
            Set(ByVal value As String)
                _codigoCcm = value
            End Set
        End Property

        Private _nombreCcm As String
        Public Property nombreCcm() As String
            Get
                Return _nombreCcm
            End Get
            Set(ByVal value As String)
                _nombreCcm = value
            End Set
        End Property

        Private _descripcionCcm As String
        Public Property descripcionCcm() As String
            Get
                Return _descripcionCcm
            End Get
            Set(ByVal value As String)
                _descripcionCcm = value
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
            codigoCcm = ""
            nombreCcm = ""
            descripcionCcm = ""
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
        End Sub
    End Class

    Public Class e_ComisionPermanenteCompetencia
        Private _tipoConsulta As String
        Public Property tipoConsulta() As String
            Get
                Return _tipoConsulta
            End Get
            Set(ByVal value As String)
                _tipoConsulta = value
            End Set
        End Property

        Private _codigoCpc As String
        Public Property codigoCpc() As String
            Get
                Return _codigoCpc
            End Get
            Set(ByVal value As String)
                _codigoCpc = value
            End Set
        End Property

        Private _codigoCop As Integer
        Public Property codigoCop() As Integer
            Get
                Return _codigoCop
            End Get
            Set(ByVal value As Integer)
                _codigoCop = value
            End Set
        End Property

        Private _codigoCom As Integer
        Public Property codigoCom() As Integer
            Get
                Return _codigoCom
            End Get
            Set(ByVal value As Integer)
                _codigoCom = value
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

        Public Sub New()
            tipoConsulta = "GEN"
            codigoCpc = ""
            codigoCop = 0
            codigoCom = 0
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
        End Sub

    End Class

    Public Class e_NivelComplejidadPregunta
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

        Private _codigoNcp As String
        Public Property codigoNcp() As String
            Get
                Return _codigoNcp
            End Get
            Set(ByVal value As String)
                _codigoNcp = value
            End Set
        End Property

        Private _nombreNcp As String
        Public Property nombreNcp() As String
            Get
                Return _nombreNcp
            End Get
            Set(ByVal value As String)
                _nombreNcp = value
            End Set
        End Property

        Private _abreviaturaNcp As String
        Public Property abreviaturaNcp() As String
            Get
                Return _abreviaturaNcp
            End Get
            Set(ByVal value As String)
                _abreviaturaNcp = value
            End Set
        End Property

        Private _descripcionNcp As String
        Public Property descripcionNcp() As String
            Get
                Return _descripcionNcp
            End Get
            Set(ByVal value As String)
                _descripcionNcp = value
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
            codigoNcp = ""
            nombreNcp = ""
            abreviaturaNcp = ""
            descripcionNcp = ""
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
        End Sub
    End Class

    Public Class e_PreguntaEvaluacion
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

        Private _codigoPrv As String
        Public Property codigoPrv() As String
            Get
                Return _codigoPrv
            End Get
            Set(ByVal value As String)
                _codigoPrv = value
            End Set
        End Property

        Private _codigoInd As Object
        Public Property codigoInd() As Object
            Get
                Return _codigoInd
            End Get
            Set(ByVal value As Object)
                _codigoInd = value
            End Set
        End Property

        Private _codigoNcp As Object
        Public Property codigoNcp() As Object
            Get
                Return _codigoNcp
            End Get
            Set(ByVal value As Object)
                _codigoNcp = value
            End Set
        End Property

        Private _tipoPrv As String
        Public Property tipoPrv() As String
            Get
                Return _tipoPrv
            End Get
            Set(ByVal value As String)
                _tipoPrv = value
            End Set
        End Property

        Private _textoPrv As Object
        Public Property textoPrv() As Object
            Get
                Return _textoPrv
            End Get
            Set(ByVal value As Object)
                _textoPrv = value
            End Set
        End Property

        Private _codigoRaizPrv As Integer
        Public Property codigoRaizPrv() As Integer
            Get
                Return _codigoRaizPrv
            End Get
            Set(ByVal value As Integer)
                _codigoRaizPrv = value
            End Set
        End Property

        Private _cantidadPrv As Integer
        Public Property cantidadPrv() As Integer
            Get
                Return _cantidadPrv
            End Get
            Set(ByVal value As Integer)
                _cantidadPrv = value
            End Set
        End Property

        Private _identificadorPrv As String
        Public Property identificadorPrv() As String
            Get
                Return _identificadorPrv
            End Get
            Set(ByVal value As String)
                _identificadorPrv = value
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

        'Datos adicionales
        Private _codigoCom As Integer
        Public Property codigoCom() As Integer
            Get
                Return _codigoCom
            End Get
            Set(ByVal value As Integer)
                _codigoCom = value
            End Set
        End Property

        Private _alternativas As List(Of e_AlternativaEvaluacion)
        Public Property alternativas() As List(Of e_AlternativaEvaluacion)
            Get
                Return _alternativas
            End Get
            Set(ByVal value As List(Of e_AlternativaEvaluacion))
                _alternativas = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            operacion = "I"
            codigoPrv = ""
            codigoInd = DBNull.Value
            codigoNcp = DBNull.Value
            tipoPrv = ""
            textoPrv = ""
            codigoRaizPrv = 0
            cantidadPrv = 1
            identificadorPrv = ""
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
            codigoCom = 0
            alternativas = New List(Of e_AlternativaEvaluacion)
        End Sub
    End Class

    Public Class e_AlternativaEvaluacion
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

        Private _codigoAle As String
        Public Property codigoAle() As String
            Get
                Return _codigoAle
            End Get
            Set(ByVal value As String)
                _codigoAle = value
            End Set
        End Property

        Private _codigoPrv As Integer
        Public Property codigoPrv() As Integer
            Get
                Return _codigoPrv
            End Get
            Set(ByVal value As Integer)
                _codigoPrv = value
            End Set
        End Property

        Private _ordenAle As Integer
        Public Property ordenAle() As Integer
            Get
                Return _ordenAle
            End Get
            Set(ByVal value As Integer)
                _ordenAle = value
            End Set
        End Property

        Private _textoAle As Object
        Public Property textoAle() As Object
            Get
                Return _textoAle
            End Get
            Set(ByVal value As Object)
                _textoAle = value
            End Set
        End Property

        Private _correctaAle As Object
        Public Property correctaAle() As Object
            Get
                Return _correctaAle
            End Get
            Set(ByVal value As Object)
                _correctaAle = value
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
            codigoAle = ""
            codigoPrv = 0
            ordenAle = 0
            textoAle = ""
            correctaAle = DBNull.Value
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
        End Sub
    End Class
#End Region

#Region "Capa de Datos"
    Public Class d_CargoComision
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_CargoComision) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_CargoComision_Listar" _
                                            , .tipoConsulta _
                                            , .codigoCcm _
                                            , .nombreCcm _
                                            , .descripcionCcm _
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

        Public Function fc_IUD(ByVal obj As e_CargoComision) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_CargoComision_IUD", _
                                        .operacion, _
                                        .codigoCcm, _
                                        .nombreCcm, _
                                        .descripcionCcm, _
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

    Public Class d_ComisionPermanente
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ComisionPermanente) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ComisionPermanente_Listar" _
                                            , .tipoConsulta _
                                            , .codigoCop _
                                            , .codigoPer _
                                            , .codigoCcm _
                                            , .nroResolucionCop _
                                            , .vigenteCop _
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

        Public Function fc_IUD(ByVal obj As e_ComisionPermanente) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_ComisionPermanente_IUD", _
                                        .operacion, _
                                        .codigoCop, _
                                        .codigoPer, _
                                        .codigoCcm, _
                                        .nroResolucionCop, _
                                        .vigenteCop, _
                                        .codigosCom, _
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

        'Otras funciones
        Public Function fc_ListarPersonal() As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("CONF_PersonalListar")
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_ComisionPermanenteCompetencia
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_ComisionPermanenteCompetencia) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_ComisionPermanente_Competencia_Listar" _
                                            , .tipoConsulta _
                                            , .codigoCpc _
                                            , .codigoCop _
                                            , .codigoCom _
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
    End Class

    Public Class d_NivelComplejidadPregunta
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_NivelComplejidadPregunta) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_NivelComplejidadPregunta_Listar" _
                                            , .tipoConsulta _
                                            , .codigoNcp _
                                            , .nombreNcp _
                                            , .abreviaturaNcp _
                                            , .descripcionNcp _
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

        Public Function fc_IUD(ByVal obj As e_NivelComplejidadPregunta) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_NivelComplejidadPregunta_IUD" _
                                        , .operacion _
                                        , .codigoNcp _
                                        , .nombreNcp _
                                        , .abreviaturaNcp _
                                        , .descripcionNcp _
                                        , .codUsuario _
                                        , "0", "", "0")
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

    Public Class d_PreguntaEvaluacion
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_PreguntaEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_PreguntaEvaluacion_Listar" _
                                            , .tipoConsulta _
                                            , .codigoPrv _
                                            , .codigoInd _
                                            , .codigoNcp _
                                            , .tipoPrv _
                                            , .textoPrv _
                                            , .codigoRaizPrv _
                                            , .cantidadPrv _
                                            , .identificadorPrv _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            , .codigoCom _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_PreguntaEvaluacion) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_PreguntaEvaluacion_IUD", _
                                        .operacion _
                                        , .codigoPrv _
                                        , .codigoInd _
                                        , .codigoNcp _
                                        , .tipoPrv _
                                        , .textoPrv _
                                        , .codigoRaizPrv _
                                        , .cantidadPrv _
                                        , .identificadorPrv _
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

        Public Function fc_GuardarMultiple(ByVal lstPreg As List(Of e_PreguntaEvaluacion), Optional ByVal pregEnun As e_PreguntaEvaluacion = Nothing) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                Dim lo_Salida As New Object()

                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                '1. Verifico si existe un enunciado (pregunta agrupada)
                If pregEnun IsNot Nothing Then
                    With pregEnun
                        lo_Salida = cnx.EjecutarConTypes("ADM_PreguntaEvaluacion_IUD", "dbo.ADM_ChunkVarcharType", _
                                            .operacion _
                                            , .codigoPrv _
                                            , .codigoInd _
                                            , .codigoNcp _
                                            , .tipoPrv _
                                            , .textoPrv _
                                            , .codigoRaizPrv _
                                            , .cantidadPrv _
                                            , .identificadorPrv _
                                            , .codUsuario, _
                                            "0", "", "0")

                        lo_Resultado.Item("rpta") = lo_Salida(0)
                        lo_Resultado.Item("msg") = lo_Salida(1)
                        lo_Resultado.Item("cod") = lo_Salida(2)

                        If lo_Resultado.Item("rpta") <> "1" Then
                            Throw New Exception(lo_Resultado.Item("msg"))
                        Else
                            For Each preg As e_PreguntaEvaluacion In lstPreg
                                preg.codigoRaizPrv = lo_Resultado.Item("cod")
                            Next
                        End If
                    End With
                End If

                '2. Guardo las preguntas
                For Each preg As e_PreguntaEvaluacion In lstPreg
                    With preg
                        lo_Salida = cnx.EjecutarConTypes("ADM_PreguntaEvaluacion_IUD", "dbo.ADM_ChunkVarcharType", _
                                            .operacion _
                                            , .codigoPrv _
                                            , .codigoInd _
                                            , .codigoNcp _
                                            , .tipoPrv _
                                            , .textoPrv _
                                            , .codigoRaizPrv _
                                            , .cantidadPrv _
                                            , .identificadorPrv _
                                            , .codUsuario, _
                                            "0", "", "0")
                    End With
                    lo_Resultado.Item("rpta") = lo_Salida(0)
                    lo_Resultado.Item("msg") = lo_Salida(1)
                    lo_Resultado.Item("cod") = lo_Salida(2)

                    If lo_Resultado.Item("rpta") <> "1" Then
                        Throw New Exception(lo_Resultado.Item("msg"))
                    End If

                    '3. Guardo las alternativas
                    Dim codigoPrv As Integer = lo_Resultado.Item("cod")
                    For Each ale As e_AlternativaEvaluacion In preg.alternativas
                        With ale
                            .codigoPrv = codigoPrv
                            lo_Salida = cnx.EjecutarConTypes("ADM_AlternativaEvaluacion_IUD", "dbo.ADM_ChunkVarcharType", _
                                                .operacion _
                                                , .codigoAle _
                                                , .codigoPrv _
                                                , .ordenAle _
                                                , .textoAle _
                                                , .correctaAle _
                                                , .codUsuario, _
                                                "0", "", "0")
                        End With
                        lo_Resultado.Item("rpta") = lo_Salida(0)
                        lo_Resultado.Item("msg") = lo_Salida(1)
                        lo_Resultado.Item("cod") = lo_Salida(2)

                        If lo_Resultado.Item("rpta") <> "1" Then
                            Throw New Exception(lo_Resultado.Item("msg"))
                        End If
                    Next
                Next

                cnx.TerminarTransaccion()
                Return lo_Resultado

            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        'Otras funciones
        Public Function fc_ListarPersonal() As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("CONF_PersonalListar")
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_AlternativaEvaluacion
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_AlternativaEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_AlternativaEvaluacion_Listar" _
                                            , .tipoConsulta _
                                            , .codigoAle _
                                            , .codigoPrv _
                                            , .ordenAle _
                                            , .textoAle _
                                            , .correctaAle _
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

        Public Function fc_IUD(ByVal obj As e_AlternativaEvaluacion) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_AlternativaEvaluacion_IUD", _
                                        .operacion _
                                        , .codigoAle _
                                        , .codigoPrv _
                                        , .ordenAle _
                                        , .textoAle _
                                        , .correctaAle _
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
    End Class

#End Region
End Class
