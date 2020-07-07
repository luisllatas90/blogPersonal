Imports Microsoft.VisualBasic
Imports System.Collections.Generic

#Region "Entidades"

Public Class e_Accion

    Public codigo_acc As Integer
    Public codigo_apl As Integer
    Public codigo_per As Integer
    Public formulario_acc As String
    Public accion_acc As String
    Public codigo_tfu As Integer
    Public tipo_acc As String
    Public estado_acc As Boolean
    Public nombre_acc As String
    Public descripcion_acc As String
    Public codigo_pro As Integer
    Public codigo_men As Integer
    Public token_acc As String

End Class

Public Class e_Accion_Configuracion

    Public codigo_aco As Integer
    Public codigo_acc As Integer
    Public tabla As String
    Public codigo_tabla As Integer
    Public estado_aco As Boolean
    Public codigo_per As Integer

End Class

Public Class e_Notificacion

    Public codigo_not As Integer
    Public codigo_acc As Integer
    Public codigo_apl As Integer = -1
    Public tabla As String
    Public codigo_tabla As Integer = -1
    Public asunto_not As String
    Public descripcion_not As String
    Public codigo_tfu As Integer = -1
    Public estado_not As String
    Public cantidad_not As Integer

    Public codigo_per As Integer
    Public tipo_operacion As String

End Class

Public Class e_UsuarioAplicacionLogeo

    Public codigo_uac As Integer
    Public codigo_per As Integer
    Public codigo_apl As Integer
    Public codigo_tfu As Integer
    Public fecha_reg As Date
    Public hostname_uac As String
    Public ip_uac As String
    Public codigo_men As Integer

End Class

#End Region

#Region "Datos"

Public Class d_Notificacion

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_ListarNotificaciones(ByVal obj As e_Notificacion) As System.Data.DataTable
        Try
            dt = New System.Data.DataTable
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                dt = cnx.TraerDataTable("ALT_Notificacion_listar", .tipo_operacion, .codigo_apl, .codigo_tfu, .codigo_tabla, .codigo_per)
            End With
            cnx.CerrarConexion()
            Return dt
        Catch ex As System.Exception
            Throw ex
        End Try
    End Function

End Class

Public Class d_UsuarioAplicacionLogeo

    Private cnx As ClsConectarDatos
    Private dt As System.Data.DataTable

    Public Function fc_RegistrarUsuarioAplicacionLogeo(ByVal obj As e_UsuarioAplicacionLogeo) As Boolean
        Try
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            cnx.AbrirConexion()
            With obj
                cnx.Ejecutar("ALT_Usuario_Aplicacion_Logeo_Insertar", .codigo_per, .codigo_apl, .codigo_tfu, .hostname_uac, .ip_uac, .codigo_men)
            End With
            cnx.CerrarConexion()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

#End Region
