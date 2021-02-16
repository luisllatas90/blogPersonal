Imports System.Net
Imports System.IO
Imports System.Drawing
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "ENTIDADES"

Public Class e_EscalaCalificacionSustentacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"

    Public codigo_ecs As String
    Public eliminado_ecs As Integer

    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_ecs = String.Empty
        eliminado_ecs = -1

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

Public Class e_CompletarDatosSustentacion

#Region "Constructor"

    Public Sub New()
        Inicializar()
    End Sub

#End Region

#Region "Propiedades"
    Public codigo_cds As String
    Public completado As String
    Public codigo_alu As String
    Public codigo_tes As String

    Public cod_user As String
    Public operacion As String

#End Region

#Region "Metodos"

    Private Sub Inicializar()
        codigo_cds = String.Empty
        completado = String.Empty
        codigo_alu = String.Empty
        codigo_tes = String.Empty

        cod_user = String.Empty
        operacion = String.Empty
    End Sub

#End Region

End Class

#End Region

#Region "DATOS"

Public Class d_EscalaCalificacionSustentacion
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable


    Public Function ListarEscalaCalificacionSustentacion(ByVal le_EscalaCalificacionSustentacion As e_EscalaCalificacionSustentacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("SUST_ListaEscalaCalificacionSustentacion", le_EscalaCalificacionSustentacion.operacion, _
                                    le_EscalaCalificacionSustentacion.codigo_ecs, _
                                    le_EscalaCalificacionSustentacion.eliminado_ecs)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

Public Class d_CompletarDatosSustentacion
    Private cnx As ClsConectarDatos
    Private dt As Data.DataTable


    Public Function ListarCompletarDatosSustentacion(ByVal le_CompletarDatosSustentacion As e_CompletarDatosSustentacion) As Data.DataTable
        Try
            cnx = New ClsConectarDatos : dt = New Data.DataTable
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.IniciarTransaccion()

            'Ejecutar Procedimiento
            dt = cnx.TraerDataTable("GYT_CompletarDatosSustentacionListar", le_CompletarDatosSustentacion.operacion, _
                                    le_CompletarDatosSustentacion.codigo_cds, _
                                    le_CompletarDatosSustentacion.completado, _
                                    le_CompletarDatosSustentacion.codigo_alu)

            cnx.TerminarTransaccion()
            Return dt
        Catch ex As Exception
            cnx.AbortarTransaccion()
            Throw ex
        End Try
    End Function

End Class

#End Region