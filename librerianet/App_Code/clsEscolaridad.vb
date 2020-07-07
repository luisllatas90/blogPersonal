Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsEscolaridad
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

    Public Function ListaDerechoHabientes(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListaDerechoHabientes", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function


    '** Para cargar los combos de la admin de escolaridad **'
    Public Function CargarCombosEscolaridad(ByVal tipo As String, ByVal parm1 As Integer, ByVal parm2 As Integer, ByVal parm3 As Integer, ByVal parm4 As Integer, ByVal parm5 As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_CargarCombosEscolaridad", tipo, parm1, parm2, parm3, parm4, parm5)
        cnx.CerrarConexion()
        Return dts
    End Function

    '** Carga todas las solicitudes de un trabajador **'
    Public Function ListaRegistrosEscolaridad(ByVal codigo_per As Integer, _
                                              ByVal desde As Date, ByVal hasta As Date, _
                                              ByVal estado As String, ByVal codigo_ded As Integer, ByVal codigo_tpe As Integer, ByVal anio As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListaRegistrosEscolaridad", codigo_per, desde, hasta, estado, codigo_ded, codigo_tpe, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaRegistrosEscolaridad_exportar(ByVal codigo_per As Integer, _
                                              ByVal desde As Date, ByVal hasta As Date, _
                                              ByVal estado As String, ByVal codigo_ded As Integer, ByVal codigo_tpe As Integer, ByVal anio As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListaRegistrosEscolaridad_expotar", codigo_per, desde, hasta, estado, codigo_ded, codigo_tpe, anio)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListaDocumentosAdjuntos(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListaDocumentosAdjuntos", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function CargarDatosPersonales(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_CargarDatosPersonales", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarNivelEscolaridad() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListaNivelEscolaridad")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ListarHijos(ByVal codigo_per As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListarHijos", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ListarGrados(ByVal codigo_niv As Integer) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("ES_ListarGrados", codigo_niv)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function AgregarSolicitud(ByVal codigo_dhab As Integer, _
                                     ByVal codigo_niv As Integer, _
                                     ByVal txtEstudios As String, _
                                     ByVal codigo_gra As Integer, _
                                     ByVal chkApli As Boolean, _
                                     ByVal Documentos As String) As Integer

        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("ES_RegistrarSolicitudEscolaridad", codigo_dhab, codigo_niv, txtEstudios, codigo_gra, chkApli, Documentos, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function


    Public Function CalificarSolicitud(ByVal codigo_se As Integer) As Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("ES_CalificarEscolaridad", codigo_se)
        cnx.CerrarConexion()
        Return codigo_se
    End Function

End Class
