Imports Microsoft.VisualBasic
Imports System.Data
Public Class ClsAdscripcionDocente
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

    Public Function ConsultarPeriodoLaborable(ByVal tipo As String, ByVal codigo_pel As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPeriodosLaborables", tipo, codigo_pel)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDepartamentoAcademico(ByVal tipo As String, ByVal param1 As String, ByVal param2 As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("TES_ConsultarDepartamentoAcademico", tipo, param1, param2)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonal(ByVal tipo As String, ByVal codigo_tpe As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarPersonal", tipo, codigo_tpe)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarAdscriptosPersonal(ByVal periodo As Integer, ByVal codigo_dac As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ConsultarAdscripcionPersonal", periodo, codigo_dac)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function AgregarAdscripcionPersonal(ByVal periodo As Integer, ByVal codigo_dac As Integer, ByVal codigo_per As Integer, ByVal usuario_reg As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("AgregarAdscripcionPersonal", periodo, codigo_dac, codigo_per, usuario_reg)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EliminarAdscripcionPersonal(ByVal codigo_apl As Integer, ByVal usuario As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("EliminarAdscripcionPersonal", codigo_apl, usuario)
        cnx.CerrarConexion()
        Return dts
    End Function
    Public Function ObtenerDepartamentoAcademico(ByVal tipo As String, ByVal codigo_per As Integer, ByVal codigo_dac As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ObtenerDirectorDepartamentoAcademico", tipo, codigo_per, codigo_dac)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class


