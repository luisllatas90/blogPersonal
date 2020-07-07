
'*********************************************************************************
'CV - USAT
'Archivo: clsRequerimientos.vb
'Autora: Helen Reyes 
'Fecha de creación: 15/02/08
'Última fecha de actualización:16/02/08
'Observaciones:
'*********************************************************************************

Imports Microsoft.VisualBasic
Public Class clsRequerimientos
    'consulta solicitudes que tienen asignados un equipo o no
    Public Function ObtieneConsultaSolicitudes(ByVal existencia As Int16, ByVal campo As Int16, ByVal valor As Integer, ByVal asignado As Int16) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            If asignado = 0 Then
                Return ObjCnx.TraerDataTable("paReq_SolicitudesConEquipoSinResponsable", existencia, campo, valor)
            ElseIf asignado = 1 Then
                Return ObjCnx.TraerDataTable("paReq_SolicitudesConEquipoConResponsable", existencia, campo, valor)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    '---------------------Para consultas/solicitudes pendientes------------------
    'Consulta solicitudes asignadas a un responsable
    Public Function obtieneSolicitudPorResponsable(ByVal cod_per As Int32) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Return ObjCnx.TraerDataTable("paReq_ConsultarListaSolicitud", cod_per)
        Catch ex As Exception
            Return Nothing
        Finally
            ObjCnx = Nothing
        End Try
    End Function
    'consulta solicitudes por responsable de cronograma para ser administradas
    Public Function obtieneSolicitudesPorResponsableDeCronograma(ByVal cod_per As Int32, ByVal campo As Int16, ByVal valor As Int16) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Return ObjCnx.TraerDataTable("paReq_SolicitudPorResponsable", cod_per, campo, valor)
        Catch ex As Exception
            Return Nothing
        Finally
            ObjCnx = Nothing
        End Try
    End Function

    'consultar solicitudes pendientes para asignar cronograma
    Public Function ObtieneSolicitudesParaAsignarCronograma(ByVal cod_per As Int32, ByVal tabla As Int32, ByVal campo As Int32) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Return ObjCnx.TraerDataTable("paReq_ConsultarSolicitudAsignar", cod_per, tabla, campo)
        Catch ex As Exception
            Return Nothing
        Finally
            ObjCnx = Nothing
        End Try
    End Function

    'consultar solicitudes a reprogramar y administrar
    Public Function ObtieneSolicitudesAReprogramar(ByVal cod_per As Int32, ByVal tipo As Int32, ByVal campo As Int16, ByVal valor As Int16) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Select Case tipo
                Case 1
                    Return ObjCnx.TraerDataTable("paReq_ConsultarSolicitudIniciadas", cod_per, campo, valor)
                Case 2
                    Return ObjCnx.TraerDataTable("paReq_ConsultarSolicitudNoIniciadas", cod_per, campo, valor)
                Case 3
                    Return ObjCnx.TraerDataTable("paReq_ConsultarSolicitudVencidas", cod_per, campo, valor)
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            Return Nothing
        Finally
            ObjCnx = Nothing
        End Try
    End Function

    'consultar solicitud a Administrar----
    Public Function ObtieneSolicitudesParaAdministrar(ByVal id_sol As Int32, ByVal cod_per As Int32, ByVal tipo As Char) As Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Return ObjCnx.TraerDataTable("paReq_ConsultarSolicitudParaAdministrar", id_sol, cod_per, tipo)
        Catch ex As Exception
            Return Nothing
        Finally
            ObjCnx = Nothing
        End Try
    End Function

    Public Sub InsertaEstadoSolicitud(ByVal id_sol As Int32, ByVal id_est As Int32, ByVal cod_per As Int32, ByVal observacion As String)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("paReq_ActualizarEstadoSolicitud", id_sol, id_est, cod_per, observacion)
        ObjCnx = Nothing
    End Sub
    '-----o-----
End Class
