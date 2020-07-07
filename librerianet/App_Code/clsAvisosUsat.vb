Imports Microsoft.VisualBasic
Imports System.Data


Public Class clsAvisosUsat

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

    Public Function AgregarEstadoImagen(ByVal decripcion_avi As String, ByVal estado_avi As Boolean) As Integer

        Dim rpta As Integer
        Dim valoresdevueltos(1) As Integer
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("ES_RegistrarEstadoImagenAviso", decripcion_avi, estado_avi, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
    End Function

End Class
