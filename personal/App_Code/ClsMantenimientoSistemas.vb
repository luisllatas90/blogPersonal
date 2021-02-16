Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class ClsMantenimientoSistemas
#Region "Capa de Entidades"
    Public Class e_CambioUsuarioAcceso
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

        Private _codigoCua As String
        Public Property codigoCua() As String
            Get
                Return _codigoCua
            End Get
            Set(ByVal value As String)
                _codigoCua = value
            End Set
        End Property

        Private _codigoPerOrig As Integer
        Public Property codigoPerOrig() As Integer
            Get
                Return _codigoPerOrig
            End Get
            Set(ByVal value As Integer)
                _codigoPerOrig = value
            End Set
        End Property

        Private _codigoPerTemp As Integer
        Public Property codigoPerTemp() As Integer
            Get
                Return _codigoPerTemp
            End Get
            Set(ByVal value As Integer)
                _codigoPerTemp = value
            End Set
        End Property

        Private _fechaDesdeCua As Object
        Public Property fechaDesdeCua() As Object
            Get
                Return _fechaDesdeCua
            End Get
            Set(ByVal value As Object)
                _fechaDesdeCua = value
            End Set
        End Property

        Private _fechaHastaCua As Object
        Public Property fechaHastaCua() As Object
            Get
                Return _fechaHastaCua
            End Get
            Set(ByVal value As Object)
                _fechaHastaCua = value
            End Set
        End Property

        Private _vigenteCua As Object
        Public Property vigenteCua() As Object
            Get
                Return _vigenteCua
            End Get
            Set(ByVal value As Object)
                _vigenteCua = value
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

        Private _equipoReg As String
        Public Property equipoReg() As String
            Get
                Return _equipoReg
            End Get
            Set(ByVal value As String)
                _equipoReg = value
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

        Private _equipoAct As String
        Public Property equipoAct() As String
            Get
                Return _equipoAct
            End Get
            Set(ByVal value As String)
                _equipoAct = value
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
        Private _usuarioOrig As String
        Public Property usuarioOrig() As String
            Get
                Return _usuarioOrig
            End Get
            Set(ByVal value As String)
                _usuarioOrig = value
            End Set
        End Property

        Private _usuarioTemp As String
        Public Property usuarioTemp() As String
            Get
                Return _usuarioTemp
            End Get
            Set(ByVal value As String)
                _usuarioTemp = value
            End Set
        End Property



        Public Sub New()
            tipoConsulta = "GEN"
            operacion = ""
            codigoCua = 0
            codigoPerOrig = 0
            codigoPerTemp = 0
            fechaDesdeCua = DBNull.Value
            fechaHastaCua = DBNull.Value
            vigenteCua = DBNull.Value
            codigoPerReg = 0
            fechaReg = DBNull.Value
            equipoReg = ""
            codigoPerAct = 0
            fechaAct = DBNull.Value
            equipoAct = ""
            codUsuario = 0
            usuarioOrig = ""
            usuarioTemp = ""
        End Sub
    End Class
#End Region

#Region "Capa de Datos"
    Public Class d_CambioUsuarioAcceso
        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_Listar(ByVal obj As e_CambioUsuarioAcceso) As System.Data.DataTable
            Try
                dt = New System.Data.DataTable
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                With obj
                    dt = cnx.TraerDataTable("MAN_CambioUsuarioAcceso_Listar" _
                                            , .tipoConsulta _
                                            , .codigoCua _
                                            , .codigoPerOrig _
                                            , .codigoPerTemp _
                                            , .fechaDesdeCua _
                                            , .fechaHastaCua _
                                            , .vigenteCua _
                                            , .codigoPerReg _
                                            , .fechaReg _
                                            , .equipoReg _
                                            , .codigoPerAct _
                                            , .fechaAct _
                                            , .equipoAct _
                                            , .usuarioOrig _
                                            , .usuarioTemp _
                                            )
                End With
                cnx.CerrarConexion()
                Return dt
            Catch ex As System.Exception
                Throw ex
            End Try
        End Function

        Public Function fc_IUD(ByVal obj As e_CambioUsuarioAcceso) As Dictionary(Of String, String)
            Try
                Dim lo_Resultado As New Dictionary(Of String, String)
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                Dim lo_Salida As Object()
                With obj
                    lo_Salida = cnx.Ejecutar("MAN_CambioUsuarioAcceso_IUD", _
                                        .operacion, _
                                        .codigoCua, _
                                        .codigoPerOrig, _
                                        .codigoPerTemp, _
                                        .fechaDesdeCua, _
                                        .fechaHastaCua, _
                                        .vigenteCua, _
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
#End Region
End Class
