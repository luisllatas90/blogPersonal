Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsProfesionalizacion
    Private cnx As New ClsConectarDatos

    Public Sub AbrirTransaccionCnx()
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()
    End Sub

    Public Sub CerrarTransaccionCnx()
        cnx.TerminarTransaccion()
    End Sub

    Public Sub CancelarTransaccionCnx()
        cnx.AbortarTransaccion()
    End Sub

    Public Function ConsultarDatosConsolidado(ByVal codigo_per As Integer, _
                                              ByVal codigo_tfu As Integer, _
                                              ByVal codigo_cco As Integer, _
                                              ByVal estado As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_ConsolidadoProgramaProfesionalizacion", codigo_per, codigo_tfu, codigo_cco, estado)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarProgramasConfigurados(ByVal codigo_tfu As Integer, ByVal codigo_test As Integer, ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'Consulta las carreras de profesionalizacion de un usuario.
        dts = cnx.TraerDataTable("spPRO_UsuariosConsulaProfesionalizacion", codigo_tfu, codigo_test, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaAlumnosPorCursoPrograma(ByVal curso As String, _
                                                 ByVal centrocosto As String, _
                                                 ByVal tipo As String, _
                                                 ByVal estado As Integer, _
                                                 ByVal ceco As Integer, _
                                                 ByVal accion As String, _
                                                 ByVal codigo_cup As Integer, _
                                                 ByVal codigo_cco As Integer, _
                                                 ByVal codigo_cac As Integer, _
                                                 ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_DetalleAlumnosProgramaProfesionalizacion", curso, centrocosto, tipo, estado, ceco, accion, codigo_cup, codigo_cco, codigo_cac, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function MostrarDatosPreMatricula(ByVal codigo_cup As Integer, ByVal codigo_cac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("spPRO_DatosMatriculaProfesionalizacion", codigo_cup, codigo_cac)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultaCentroCosto(ByVal codigo_ceco As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_DatosProfesionalizacion", codigo_ceco)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class
