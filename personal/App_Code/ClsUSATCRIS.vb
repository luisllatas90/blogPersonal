Imports Microsoft.VisualBasic
Imports System.Data

Public Class ClsUSATCRIS
    Private cnx As New ClsConectarDatos

    Public Function ListarUnidadOrganizacional(ByVal tipo As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("CRIS_ConsultarUnidadOrganizacionalxTipo", tipo)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaDireccionxPlan(ByVal codigo_pla As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_CecosxPEI", codigo_pla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultaResponsablexArea(ByVal codigo_area As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ConsultaResponsablesxArea", codigo_area)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Sub ActualizarLimitePresupuestal(ByVal codigo_asp As Integer, ByVal codigo_poa As Integer, ByVal codigo_cco As Integer, ByVal codigo_tac As Integer, ByVal usuario_reg As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_ActualizarLimitePresupuestal", codigo_asp, codigo_poa, codigo_cco, codigo_tac, usuario_reg)
        cnx.CerrarConexion()
    End Sub

    Public Sub EliminarLimitePresupuestal(ByVal codigo_poa As Integer, ByVal usuario_mod As Integer)
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("POA_EliminarLimitePresupuestal", codigo_poa, usuario_mod)
        cnx.CerrarConexion()
    End Sub

End Class
