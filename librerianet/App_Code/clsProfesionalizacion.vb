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
    Public Function ConsultarCursosInvitadosAlumno(ByVal codigo_alu As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_ListaCursosInvitadoAlumno", codigo_alu)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function EjecutaProcesoNivelacion(ByVal codigo_alu As Integer, ByVal codigo_cup As Integer, ByVal codigo_dma As Integer, ByVal monto As Decimal) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_MatriculaGeneraDeudaProfesionalizacionNivelacion", codigo_alu, codigo_cup, codigo_dma, monto)
        cnx.CerrarConexion()
        Return dts
    End Function

    'Public Function GuardaEnvioCorreos(ByVal codigo_alu As Integer, ByVal codigo_cup As Integer, ByVal codigo_dma As Integer, ByVal monto As Decimal) As Data.DataTable
    '    Dim dts As New Data.DataTable
    '    cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '    cnx.AbrirConexion()
    '    dts = cnx.TraerDataTable("", codigo_alu, codigo_cup, codigo_dma, monto)
    '    cnx.CerrarConexion()
    '    Return dts
    'End Function

    Public Function RechazaProcesoNivelacion(ByVal codigo_alu As Integer, ByVal codigo_cup As Integer, ByVal codigo_dma As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("spPRO_RechazaProcesoNivelacion", codigo_alu, codigo_cup, codigo_dma)
        cnx.CerrarConexion()
        Return dts
    End Function

End Class
