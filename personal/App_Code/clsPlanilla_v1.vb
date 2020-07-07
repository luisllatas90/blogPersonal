Imports Microsoft.VisualBasic

Public Class clsPlanilla_v1
    Dim cnx As New ClsConectarDatos

    Public Function ConsultarFuncionQuinta() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarFuncionQuinta")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPlanilla(optional ByVal parametro As Int64 = 0) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarPlanilla", parametro )
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarPersonal(ByVal parametro As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        If parametro = "TO" Then
            dts = cnx.TraerDataTable("ConsultarPersonal", "PRES", "")
        Else
            dts = cnx.TraerDataTable("ConsultarPersonal", "LI", parametro)
        End If
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDistribucionDetallePlanilla(ByVal codigo_plla As Int64, ByVal codigo_per As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("presu_consultardistribuciondetalleplanilla", codigo_plla, codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function



    Public Function DistribucioPlanillaTrabajador(ByVal codigo_plla As Int64, ByVal codigo_cco As Int64, ByVal textoBusqueda As String) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_DistribucionPlanillaTrabajador", codigo_plla, codigo_cco, textoBusqueda)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function RegistrarDistribucionDetallePlanilla(ByVal codigo_plla As Int32, _
                                       ByVal codigo_per As Int32, ByVal codigo_cco As Int64, _
                                       ByVal porcentaje_ddplla As Int64, ByVal usuarioRegistra As Int64, _
                                       ByVal observacion As String) As Int32
        Try
            Dim Rpta As Int32
            Dim valoresdevueltos(1) As Int32
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("PRESU_RegistrarDistribucionDetallePlanilla", codigo_plla, codigo_per, codigo_cco, porcentaje_ddplla, usuarioRegistra, observacion).copyto(valoresdevueltos, 0)
            cnx.CerrarConexion()
            Rpta = valoresdevueltos(0)
            Return Rpta
        Catch ex As Exception
            Return -3
        End Try
    End Function

    Public Function ModificarDistribucionDetallePlanilla(ByVal codigo_ddplla As Int32, _
                                      ByVal codigo_plla As Int64, ByVal codigo_per As Int64, _
                                      ByVal codigo_cco As Int64, ByVal porcentaje_ddplla As Double, _
                                      ByVal usuarioRegistra As Int64, ByVal observacion As String) As Int32
        Try
            Dim Rpta As Int32
            Dim valoresdevueltos(1) As Int32
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("PRESU_ModificarDistribucionDetallePlanilla", codigo_ddplla, codigo_plla, codigo_per, codigo_cco, porcentaje_ddplla, usuarioRegistra, observacion).copyto(valoresdevueltos, 0)
            cnx.CerrarConexion()
            Rpta = valoresdevueltos(0)
            Return Rpta
        Catch ex As Exception
            Return -3
        End Try
    End Function

    Public Function EliminarDistribucionDetallePlanilla(ByVal codigo_ddplla As Int32) As Int32
        Try
            Dim Rpta As Int32
            Dim valoresdevueltos(1) As Int32
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("PRESU_EliminarDistribucionDetallePlanilla", codigo_ddplla).copyto(valoresdevueltos, 0)
            cnx.CerrarConexion()
            Rpta = valoresdevueltos(0)
            Return Rpta
        Catch ex As Exception
            Return -3
        End Try
    End Function

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

    Public Function AgregarAnexoDetallePlanillaEjecutado(ByVal codigo_Adplla As Int32, _
                                         ByVal tipo_Adplla As Int16, ByVal codigo_plla As Int64, _
                                         ByVal codigo_Ejp As Int64, ByVal codigo_per As Int64, _
                                         ByVal codigo_cco As Int64, ByVal importe_Adplla As Double, _
                                         ByVal codigo_ppr As Int16, ByVal codigo_cplla As Int64, _
                                         ByVal codigo_Fqta As Int16, ByVal observacion_Adplla As String, _
                                         ByVal usuarioRegistra As Int64, ByVal estado_Adplla As String, _
                                         ByVal codigo_Dplla As Int16) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        If codigo_Adplla = 0 Then
            cnx.Ejecutar("PRESU_AgregarAnexoDetallePlanillaEjecutado", tipo_Adplla, codigo_plla, codigo_Ejp, codigo_per, codigo_cco, importe_Adplla, codigo_ppr, codigo_cplla, codigo_Fqta, observacion_Adplla, usuarioRegistra, estado_Adplla, codigo_Dplla).copyto(valoresdevueltos, 0)
        Else
            cnx.Ejecutar("PRESU_ModificarAnexoDetallePlanillaEjecutado", codigo_Adplla, tipo_Adplla, codigo_plla, codigo_Ejp, codigo_per, codigo_cco, importe_Adplla, codigo_ppr, codigo_cplla, codigo_Fqta, observacion_Adplla, usuarioRegistra, estado_Adplla, codigo_Dplla).copyto(valoresdevueltos, 0)
        End If
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta

    End Function

    Public Function EliminarAnexoDetallePlanillaEjecutado(ByVal codigo_Adplla As Int32, ByVal codigo_per As Int32) As Int32
        Try
            Dim rpta As Int32
            Dim valoresdevueltos(1) As Int32
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            cnx.AbrirConexion()
            cnx.Ejecutar("PRESU_AnularAnexoDetallePlanillaEjecutado", codigo_Adplla, codigo_per).copyto(valoresdevueltos, 0)
            cnx.CerrarConexion()
            rpta = valoresdevueltos(0)
            Return rpta
        Catch ex As Exception
            Return -3
        End Try
    End Function

    Public Function EnviarDetallePlanillaQuinta(ByVal codigo_plla As Int32, ByVal codigo_cco As Int32, ByVal codigo_per As Int32) As Int32
        'Try
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PRESU_EnviarDetallePlanillaQuinta", codigo_plla, codigo_cco, codigo_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
        'Catch ex As Exception
        '    Return -3
        'End Try
    End Function

    Public Function EnviarTodaPlanillaQuinta(ByVal codigo_plla As Int32, ByVal codigo_per As Int32) As Int32
        'Try
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PRESU_EnviarTodasLasQuintas", codigo_plla, codigo_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
        'Catch ex As Exception
        '    Return -3
        'End Try
    End Function

    Public Function ConsultarQuintaTotal(ByVal codigo_plla As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarPlanillaQuintaTotal", codigo_plla)
        cnx.CerrarConexion()
        Return dts
    End Function
End Class
