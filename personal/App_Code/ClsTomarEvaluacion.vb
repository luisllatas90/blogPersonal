Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System

Public Class ClsTomarEvaluacion
#Region "Capa de Entidades"
    Public Class e_AsistenciaEvaluacion
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

        Private _codigoAse As String
        Public Property codigoAse() As String
            Get
                Return _codigoAse
            End Get
            Set(ByVal value As String)
                _codigoAse = value
            End Set
        End Property

        Private _codigoGru As Integer
        Public Property codigoGru() As Integer
            Get
                Return _codigoGru
            End Get
            Set(ByVal value As Integer)
                _codigoGru = value
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

        Private _estadoAsistenciaAse As String
        Public Property estadoAsistenciaAse() As String
            Get
                Return _estadoAsistenciaAse
            End Get
            Set(ByVal value As String)
                _estadoAsistenciaAse = value
            End Set
        End Property

        Private _fechaCierreAse As Object
        Public Property fechaCierreAse() As Object
            Get
                Return _fechaCierreAse
            End Get
            Set(ByVal value As Object)
                _fechaCierreAse = value
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

        Private _codigoCco As Integer
        Public Property codigoCco() As Integer
            Get
                Return _codigoCco
            End Get
            Set(ByVal value As Integer)
                _codigoCco = value
            End Set
        End Property

        Private _codigoTge As Integer
        Public Property codigoTge() As Integer
            Get
                Return _codigoTge
            End Get
            Set(ByVal value As Integer)
                _codigoTge = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            operacion = "I"
            codigoAse = ""
            codigoGru = 0
            codigoAlu = 0
            estadoAsistenciaAse = ""
            fechaCierreAse = DBNull.Value
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
            codigoCco = 0
            codigoTge = 0
        End Sub
    End Class

    Public Class e_IncidenciaEvaluacion
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

        Private _codigoIne As String
        Public Property codigoIne() As String
            Get
                Return _codigoIne
            End Get
            Set(ByVal value As String)
                _codigoIne = value
            End Set
        End Property

        Private _codigoGru As Integer
        Public Property codigoGru() As Integer
            Get
                Return _codigoGru
            End Get
            Set(ByVal value As Integer)
                _codigoGru = value
            End Set
        End Property

        Private _descripcionIne As String
        Public Property descripcionIne() As String
            Get
                Return _descripcionIne
            End Get
            Set(ByVal value As String)
                _descripcionIne = value
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

        'Adicionales
        Private _codigoCco As Integer
        Public Property codigoCco() As Integer
            Get
                Return _codigoCco
            End Get
            Set(ByVal value As Integer)
                _codigoCco = value
            End Set
        End Property

        Public Sub New()
            tipoConsulta = "GEN"
            operacion = "I"
            codigoIne = ""
            codigoGru = 0
            descripcionIne = ""
            codigoPerReg = 0
            fechaReg = DBNull.Value
            codigoPerAct = 0
            fechaAct = DBNull.Value
            codUsuario = 0
            codigoCco = 0
        End Sub

    End Class

#End Region

#Region "Capa de Datos"
    Public Class d_AsistenciaEvaluacion
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_AsistenciaEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_AsistenciaEvaluacion_Listar" _
                                            , .tipoConsulta _
                                            , .codigoAse _
                                            , .codigoGru _
                                            , .codigoAlu _
                                            , .estadoAsistenciaAse _
                                            , .fechaCierreAse _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            , .codigoCco _
                                            , .codigoTge _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_AsistenciaEvaluacion) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_AsistenciaEvaluacion_IUD", _
                                        .operacion _
                                        , .codigoAse _
                                        , .codigoGru _
                                        , .codigoAlu _
                                        , .estadoAsistenciaAse _
                                        , .fechaCierreAse _
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

        Public Function fc_MasivoIUD(ByVal lst As List(Of e_AsistenciaEvaluacion)) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                For Each obj As e_AsistenciaEvaluacion In lst
                    Dim lo_Salida As Object()
                    With obj
                        lo_Salida = cnx.Ejecutar("ADM_AsistenciaEvaluacion_IUD", _
                                            .operacion _
                                            , .codigoAse _
                                            , .codigoGru _
                                            , .codigoAlu _
                                            , .estadoAsistenciaAse _
                                            , .fechaCierreAse _
                                            , .codUsuario _
                                            , "0", "", "0")
                    End With

                    lo_Resultado.Item("rpta") = lo_Salida(0)
                    lo_Resultado.Item("msg") = lo_Salida(1)
                    lo_Resultado.Item("cod") = lo_Salida(2)

                    If lo_Resultado.Item("rpta") <> "1" Then
                        Throw New Exception(lo_Resultado.Item("msg"))
                    End If
                Next

                cnx.TerminarTransaccion()

                Return lo_Resultado
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function

        'Funciones adicionales
        Public Function fc_ListarGrupoAdmisionVirtual(ByVal tipoOperacion As String, ByVal codigoGru As Integer, ByVal codigoCco As String, ByVal codigoTge As Integer) As Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                dt = cnx.TraerDataTable("ADM_GrupoAdmisionVirtual_Listar", tipoOperacion, codigoGru, codigoCco, codigoTge)
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_IncidenciaEvaluacion
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_IncidenciaEvaluacion) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("ADM_IncidenciaEvaluacion_Listar" _
                                            , .tipoConsulta _
                                            , .codigoIne _
                                            , .codigoGru _
                                            , .descripcionIne _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            , .codigoCco _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_IncidenciaEvaluacion) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("ADM_IncidenciaEvaluacion_IUD", _
                                        .operacion _
                                        , .codigoIne _
                                        , .codigoGru _
                                        , .descripcionIne _
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
#End Region
End Class
