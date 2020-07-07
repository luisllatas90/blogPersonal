Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class ClsMatriculaPregradoNivelacion
    Public Function ListarCentroCosto( _
        ByVal codigoPer As Integer _
        , ByVal codigoTfu As Integer _
        , ByVal codigoTest As Integer _
        , ByVal antiguosCco As String _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("EVE_BuscaCcoId", codigoPer, codigoTfu, codigoTest, antiguosCco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarPlanEstudio( _
        ByVal tipo As String _
        , ByVal param1 As String _
        , ByVal param2 As String _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPlanEstudio", tipo, param1, param2)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCicloAcademico( _
        ByVal tipo As String _
        , ByVal param1 As String _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarCicloAcademico", tipo, param1)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCarreraProfesional( _
        ByVal codigoTest As Integer _
        , ByVal codigoStest As Integer _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("WS_ConsultarCarrerasProfesionales", codigoTest, codigoStest)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarGrupoHorario( _
        ByVal codigoPes As Integer _
        , ByVal codigoCac As Integer _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("MAT_ListarGruposMatriculaNivelacion", codigoPes, codigoCac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarCursoProgramado( _
        ByVal codigoPes As Integer _
        , ByVal codigoCac As Integer _
        , ByVal nombreGrupoHorario As String _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("MAT_ListarCursosNivelacionPorGrupo", codigoPes, codigoCac, nombreGrupoHorario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAlumnosParaCursoProgramado( _
        ByVal codigoPes As Integer _
        , ByVal codigoCac As Integer _
        , ByVal codigoCpf As Integer _
        , ByVal codigosCur As String _
        , ByVal nombreGrupoHorario As String _
        , ByVal tempPagoMatricula As Integer _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        Dim pagoMatricula As Object = IIf(tempPagoMatricula = "-1", DBNull.Value, tempPagoMatricula)
        dts = cnx.TraerDataTable("MAT_ListarAlumnosParaCursoProgramado", codigoPes, codigoCac, codigoCpf, codigosCur, nombreGrupoHorario, pagoMatricula)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function BuscarCursoProgramadoNivelacion( _
        ByVal codigoPes As Integer _
        , ByVal codigoCac As Integer _
        , ByVal codigoCur As String _
        , ByVal nombreGrupoHorario As String _
    ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("MAT_BuscarCursoProgramadoNivelacion", codigoPes, codigoCac, codigoCur, nombreGrupoHorario)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarMatriculaWeb( _
        ByVal codigoAlu As Integer _
        , ByVal codigoPes As Integer _
        , ByVal codigoCac As Integer _
        , ByVal codigosCup As String _
        , ByVal arrVD As String _
        , ByVal usuario As String _
    ) As Dictionary(Of String, String)
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()

        Dim resultado As New Dictionary(Of String, String)

        Dim modo As String = "A"
        Dim estadoMat As String = "P"
        Dim observacionMat As String = "Matricula por MPE"
        Dim tipomatriculaDma As String = "N"
        Dim estadoDma As String = "M"
        Dim tipoActualizacion As String = "MAT"
        Dim equipoBit As String = "PC"
        Dim codigoMar As Object = DBNull.Value
        Dim observacionMar As Object = DBNull.Value
        Dim nroPartesDeu As Integer = 1
        Dim tipoCondicion As String = "M"
        Dim motivoCondicion As String = "NIVELACIÓN"
        Dim mensaje As Object = DBNull.Value

        Try
            Dim salida As Object() = cnx.Ejecutar("AgregarMatriculaWeb_v1_2016_AUTO" _
                , modo _
                , codigoAlu _
                , codigoPes _
                , codigoCac _
                , estadoMat _
                , observacionMat _
                , codigosCup _
                , arrVD _
                , tipomatriculaDma _
                , estadoDma _
                , tipoActualizacion _
                , usuario _
                , equipoBit _
                , codigoMar _
                , observacionMar _
                , nroPartesDeu _
                , tipoCondicion _
                , motivoCondicion _
                , mensaje _
            )
            resultado.Item("rpta") = "1"
            resultado.Item("msg") = "Matrícula registrada correctamente"

            cnx.TerminarTransaccion()

        Catch ex As Exception
            resultado.Item("rpta") = "-1"
            resultado.Item("msg") = ex.Message
            cnx.AbortarTransaccion()
        End Try

        Return resultado
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="codigoPes"></param>
    ''' <param name="codigoCac"></param>
    ''' <param name="codigoCur"></param>
    ''' <param name="codigo_alu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function BuscarCursoProgramadoMatriculado( _
         ByVal codigoPes As Integer _
         , ByVal codigoCac As Integer _
         , ByVal codigoCur As String _
         , ByVal codigo_alu As Integer _
     ) As Data.DataTable
        Dim dts As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()

        dts = cnx.TraerDataTable("ADM_BuscarCursoProgramadoMatriculado", codigoPes, codigoCac, codigoCur, codigo_alu)
        cnx.CerrarConexion()
        Return dts
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="codigo_Alu"></param>
    ''' <param name="codigosCup"></param>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RetirarMatriculaCurso( _
        ByVal codigo_Alu As Integer, _
        ByVal codigosCup As String, _
        ByVal usuario As String _
    ) As System.Data.DataTable

        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.IniciarTransaccion()

        Dim resultado As New System.Data.DataTable
        Try

            resultado = cnx.TraerDataTable("ADM_RetirarMatriculaCurso", _
                          codigo_Alu, _
                          codigosCup, _
                          usuario _
                          )

            'resultado.Item("rpta") = "1"
            'resultado.Item("msg") = "Se generado el Retiro correctamente"

            cnx.TerminarTransaccion()

        Catch ex As Exception
            'resultado.Item("rpta") = "-1"
            'resultado.Item("msg") = ex.Message
            cnx.AbortarTransaccion()
        End Try
        Return resultado

    End Function

End Class
