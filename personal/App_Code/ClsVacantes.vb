Imports Microsoft.VisualBasic
Imports System.Data
Public Class ClsVacantes
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

    Public Function InsertarVacantes(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_min As Integer, ByVal numero_vac As Integer, ByVal estado As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VAC_InsertarVacantes", codigo_cac, codigo_cpf, codigo_min, numero_vac, estado, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function

    Public Function ConsultarVacantes(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_min As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VAC_ConsultarVacantes", codigo_cac, codigo_cpf, codigo_min)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function actualizarVacantes(ByVal codigo_vac As Integer, ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_min As Integer, ByVal numero_vac As Integer, ByVal estado As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VAC_ActualizarVacantes", codigo_vac, codigo_cac, codigo_cpf, codigo_min, numero_vac, estado, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
    Public Function EliminarVacantes(ByVal codigo_vac As Integer, ByVal usuario As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VAC_EliminarVacantes", codigo_vac, usuario)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
    Public Function ConsultarCantidadIngresantes(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_min As Integer, ByVal numero_vac As Integer) As String
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("VAC_CantidadDeIngresantes", codigo_cac, codigo_cpf, codigo_min, numero_vac)
        cnx.CerrarConexion()
        Return dts.Rows(0).Item("Mensaje").ToString
    End Function
End Class
