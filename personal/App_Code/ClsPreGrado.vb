Imports Microsoft.VisualBasic

Public Class ClsPreGrado
#Region "ENTIDADES"
    Public Class e_PgCicloAcademico
        Public operacion As String
        Public codigoCac As String
        Public descripcionCac As String
        Public tipoCac As String
        Public vigenciaCac As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoCac = ""
            descripcionCac = ""
            tipoCac = ""
            vigenciaCac = ""
        End Sub
    End Class

    Public Class e_PgCarreraProfesional
        Public operacion As String
        Public codigoTest As String
        Public codigoTfu As String
        Public codigoPer As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoTest = ""
            codigoTfu = ""
            codigoPer = ""
        End Sub
    End Class

    Public Class e_PgDepartamentoAcademico
        Public operacion As String
        Public codigoPer As String
        Public codigoTfu As String

        Public Sub New()
            Inicializar()
        End Sub

        Public Sub Inicializar()
            operacion = ""
            codigoPer = ""
            codigoTfu = ""
        End Sub
    End Class
#End Region

#Region "DATOS"
    Public Class d_PgDepartamentoAcademico
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function DepartamentoPersonalFuncion(ByVal le_DepartamentoAcademico As e_PgDepartamentoAcademico) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()
                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ACAD_DepartamentoPersonalFuncion", _
                                        le_DepartamentoAcademico.operacion, _
                                        le_DepartamentoAcademico.codigoPer, _
                                        le_DepartamentoAcademico.codigoTfu)

                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PgCarreraProfesional
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function ConsultarCarreraProfesional(ByVal le_CarreraProfesional As e_PgCarreraProfesional) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()

                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("EVE_ConsultarCarreraProfesional", _
                                        le_CarreraProfesional.codigoTest, _
                                        le_CarreraProfesional.codigoTfu, _
                                        le_CarreraProfesional.codigoPer)

                cnx.TerminarTransaccion()
                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PgCicloAcademico
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function Listar(ByVal le_CicloAcademico As e_PgCicloAcademico) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()
                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ACAD_CicloAcademicoListar", le_CicloAcademico.operacion, _
                                        le_CicloAcademico.codigoCac, _
                                        le_CicloAcademico.vigenciaCac, _
                                        le_CicloAcademico.tipoCac)

                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class

    Public Class d_PgFunciones
        Private cnx As ClsConectarDatos
        Private dt As Data.DataTable

        Public Function AsistenciaVirtual(ByVal ln_CodigoCac As Integer, ByVal ln_codigoCpf As Integer, ByVal ln_CodigoDac As Integer, ByVal ls_fechaDesde As String, ByVal ls_fechaHasta As String) As Data.DataTable
            Try
                cnx = New ClsConectarDatos : dt = New Data.DataTable
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.IniciarTransaccion()
                'Ejecutar Procedimiento
                dt = cnx.TraerDataTable("ACAD_AsistenciaVirtual", ln_CodigoCac, ln_codigoCpf, ln_CodigoDac, ls_fechaDesde, ls_fechaHasta)

                Return dt
            Catch ex As Exception
                cnx.AbortarTransaccion()
                Throw ex
            End Try
        End Function
    End Class
#End Region

End Class
