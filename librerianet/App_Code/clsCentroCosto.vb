Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsCentroCosto
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

    Public Function ConsultaCentroCosto(ByVal vDes As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_BuscarCentroCosto", vDes)
        cnx.CerrarConexion()
        Return dts
    End Function

    'No incluye docencia ni refrigerio
    Public Function ConsultarTipoActividad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_BuscarTipoActividad")
        cnx.CerrarConexion()
        Return dts
    End Function

    'No incluye refrigerio
    Public Function ConsultarTipoActividad2() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_BuscarTipoActividad2")
        cnx.CerrarConexion()
        Return dts
    End Function


End Class
