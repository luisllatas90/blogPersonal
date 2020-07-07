Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsAprobarPresupuesto
    Public Function ConsultarEstadoProceso(ByVal codigo_pct As Int16, ByVal codigo_epr As Int16) As DataTable
        Try
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Return ObjCnx.TraerDataTable("PRESU_ConsultarPresupuestosPorProcesoEstado", codigo_pct, codigo_epr)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function RevisarPresupuesto(ByVal codigo_pto As Int16, ByVal estado_pto As Double, ByVal techoingresos_pto As Double, ByVal techoegresos_pto As Double, ByVal observaciones_rpr As String, ByVal codigo_per As Int16) As String
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Try
        Dim rpta As String
        'ObjCnx.IniciarTransaccion()
        rpta = ObjCnx.Ejecutar("PRESU_AgregarRevisionPresupuesto", codigo_pto, estado_pto, techoingresos_pto, techoegresos_pto, observaciones_rpr, codigo_per, 0)
        'ObjCnx.TerminarTransaccion()
        Return rpta.ToString
        'Catch ex As Exception
        '    Return ex.Message
        'End Try
    End Function
End Class
