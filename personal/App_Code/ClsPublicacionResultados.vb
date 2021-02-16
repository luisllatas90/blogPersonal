Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Public Class ClsPublicacionResultados

#Region "Capa de Entidades"

    Public Class e_NotificaEstadoAdmision

        Public _tipoOpe As String = ""
        Public _codigo_cco As Integer = 0
        Public _codigo_apl As Integer = 0
        Public _codigo_evl As Integer = 0
        Public _codigo_alu As Integer = 0
        Public _codigo_per As Integer = 0

    End Class

#End Region

#Region "Capa de Datos"

    Public Class d_NotificacionEstadoAdmision

        Private cnx As ClsConectarDatos
        Private dt As System.Data.DataTable

        Public Function fc_GenerarNotificacion(ByVal obj As e_NotificaEstadoAdmision) As Dictionary(Of String, String)
            Dim lo_Resultado As New Dictionary(Of String, String)
            Try
                cnx = New ClsConectarDatos
                cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cnx.AbrirConexion()
                Dim lo_Salida As Object() = cnx.Ejecutar("ADM_Noti_Ingresante", _
                                                         obj._tipoOpe, _
                                                         obj._codigo_cco, _
                                                         obj._codigo_apl, _
                                                         obj._codigo_evl, _
                                                         obj._codigo_alu, _
                                                         obj._codigo_per, _
                                                         0, _
                                                         "")
                lo_Resultado.Item("rpta") = lo_Salida(0)
                lo_Resultado.Item("msg") = lo_Salida(1)
            Catch ex As Exception
                lo_Resultado.Item("rpta") = "-1"
                lo_Resultado.Item("msg") = ex.Message
            End Try
            Return lo_Resultado
        End Function

    End Class

#End Region

End Class
