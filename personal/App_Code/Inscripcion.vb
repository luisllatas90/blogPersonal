Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class Inscripcion
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

    Public Function ConsultarInscritos(ByVal ciclo As Integer, ByVal cadena As String) As DataTable

        Try
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            Return cnx.TraerDataTable("AVI_ConsultarVentaEntradaParaEntrega", ciclo, cadena)
            cnx.CerrarConexion()
        Catch ex As Exception
            Return Nothing
        End Try
        cnx = Nothing
    End Function

    Public Sub RegistrarEntrega(ByVal codigo_per As Integer, ByVal codigo_deu As Integer)

        Try
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("AVI_RegistarEntregaEntrada", codigo_per, codigo_deu)
            cnx.CerrarConexion()
        Catch ex As Exception

        End Try
        cnx = Nothing
    End Sub
End Class
