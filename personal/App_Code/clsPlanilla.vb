Imports Microsoft.VisualBasic

Public Class clsPlanilla
    Dim cnx As New ClsConectarDatos
    '### FUNCIONES DE CONSULTA ### 
    Public Function ConsultarFuncionQuinta() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarFuncionQuinta")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarFuncionQuinta(ByVal codigo_per As Int32, Optional ByVal codigo_tfu As Integer = 0) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarFuncionQuintaPorUsuario", codigo_per, codigo_tfu)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPlanilla(Optional ByVal parametro As Int64 = 0) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarPlanilla", parametro)
        cnx.CerrarConexion()
        Return dts
    End Function


    Public Function ConsultarCentroCostosQuintaParaRevision(ByVal codigo_per As Int32, ByVal codigo_Plla As Int32, ByVal solopendientes As Byte) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarCentroCostosParaRevision", codigo_per, codigo_Plla, solopendientes)
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

    Public Function ConsultarQuintaTotal(ByVal codigo_plla As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarPlanillaQuintaTotal", codigo_plla)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarQuintaTotalPorAcceso(ByVal codigo_plla As Int64, ByVal codigo_per As Int64) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        'response.write codigo_usu
        dts = cnx.TraerDataTable("PRESU_ConsultarPlanillaQuintaTotal_v2", codigo_plla, codigo_per)

        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarQuintaxfacultad(ByVal codigo_plla As Int64, ByVal codigo_per As Int32) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PLA_ConsultaPlanillaQuinta.sql", codigo_plla)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarAccesoQuinta() As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarAccesoQuinta")
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarCentroCostosQuinta(ByVal codigo_per As Int32) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarCentroCostosQuinta", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarDatosCorreoQuinta(ByVal codigo_cco As Int32) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PRESU_ConsultarDatosCorreoParaQuinta", codigo_cco)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarPersonalDelArea(ByVal codigo_per As Int32) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("PER_ConsultarPersonalDirectorDepartamento", codigo_per)
        cnx.CerrarConexion()
        Return dts
    End Function

    Public Function ConsultarHistoricoQuintaPlanillaArea(ByVal codigo_plla As Int32, ByVal codigo_per As Int32) As Data.DataTable
        Dim dts As New Data.DataTable
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        dts = cnx.TraerDataTable("presu_consultarhistoricoquintaplanillaarea", codigo_plla, codigo_per)
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

    '### FUNCIONES DE EJECUCIÓN: INSERTAR, ACTUALIZAR Y ELIMINAR ### 
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

    Public Function AgregarAccesoQuinta(ByVal codigo_cco As Int32, ByVal codigoregistra_per As Int32, ByVal codigoaprueba_per As Int32) As Int32
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("presu_AgregarAccesoQuinta", codigo_cco, codigoregistra_per, codigoaprueba_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
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

    Public Function QuitarAccesoQuinta(ByVal codigo_Aqta As Int32) As String
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("presu_quitaraccesoquinta", codigo_Aqta)
        cnx.CerrarConexion()
        Return "Se eliminó el registro correctamente"
    End Function


    '### FUNCIONES PARA ENVIO Y RETORNO DE INFORMACIÓN ###
    Public Function EnviarDetallePlanillaQuinta(ByVal codigo_plla As Int32, ByVal codigo_cco As Int32, ByVal codigo_per As Int32) As Int32
        'Versión antigua
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

    Public Function RegresarDetallePlanillaQuinta(ByVal codigo_plla As Int32, ByVal codigo_cco As Int32, ByVal codigo_per As Int32) As Int32
        'Versión antigua
        'Try
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PRESU_RegresarDetallePlanillaQuintaBorrador", codigo_plla, codigo_cco, codigo_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
        'Catch ex As Exception
        '    Return -3
        'End Try
    End Function

    Public Function EnviarDetallePlanillaQuintaARevisor(ByVal codigo_plla As Int32, ByVal codigo_cco As Int32, ByVal codigo_per As Int32) As Int32
        'Try
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PRESU_EnviarDetallePlanilLaQuintaARevisor", codigo_plla, codigo_cco, codigo_per, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
        'Catch ex As Exception
        '    Return -3
        'End Try
    End Function

    Public Function EnviarTodaPlanillaQuinta(ByVal codigo_plla As Int32, ByVal codigo_per As Int32, Optional ByVal codigo_ctf As Int32 = 0) As Int32
        'Try
        Dim rpta As Int32
        Dim valoresdevueltos(1) As Int32
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        cnx.AbrirConexion()
        cnx.Ejecutar("PRESU_EnviarTodasLasQuintas", codigo_plla, codigo_per, codigo_ctf, 0).copyto(valoresdevueltos, 0)
        cnx.CerrarConexion()
        rpta = valoresdevueltos(0)
        Return rpta
        'Catch ex As Exception
        '    Return -3
        'End Try
    End Function

    '### Envio de Correo según instancia ###

    Public Sub EnviarCorreo(ByVal codigo_cco As Integer, ByVal tipo As Byte, ByVal Mensaje As String)
        Dim objMail As New ClsMail
        Dim deNombre, deCorreo, paraNombre, paraCorreo, AsuntoCorreo, cecos As String
        Dim datos As New Data.DataTable
        Dim cuerpoMensaje, conCopia As String
        datos = ConsultarDatosCorreoQuinta(codigo_cco)
        deCorreo = "campusvirtual@usat.edu.pe"

        'Antes del cambio 17/05/2019 Mneciosup (Inicio)
        'If tipo = 1 Then
        '    deNombre = datos.Rows(0).Item("PersonaReg").ToString
        '    conCopia = datos.Rows(0).Item("CorreoReg").ToString
        '    paraNombre = datos.Rows(0).Item("PersonaRev").ToString
        '    paraCorreo = datos.Rows(0).Item("CorreoRev").ToString
        '    cecos = datos.Rows(0).Item("Centrocostos").ToString
        '    AsuntoCorreo = "[Mód. Quinta Especial]: Revisar " & cecos
        'ElseIf tipo = 2 Then
        '    deNombre = datos.Rows(0).Item("PersonaRev").ToString
        '    conCopia = datos.Rows(0).Item("CorreoRev").ToString
        '    paraNombre = datos.Rows(0).Item("PersonaReg").ToString
        '    paraCorreo = datos.Rows(0).Item("CorreoReg").ToString
        '    cecos = datos.Rows(0).Item("Centrocostos").ToString
        '    AsuntoCorreo = "[Mód. Quinta Especial]: Corregir " & cecos
        'Else
        '    deNombre = datos.Rows(0).Item("PersonaRev").ToString
        '    conCopia = datos.Rows(0).Item("CorreoRev").ToString
        '    paraNombre = "CASTAÑEDA CASTAÑEDA, ANGEL"
        '    paraCorreo = "acastaneda@usat.edu.pe"

        '    cecos = datos.Rows(0).Item("Centrocostos").ToString
        '    AsuntoCorreo = "[Mód. Quinta Especial]: Planilla aprobada " & cecos
        'End If
        'cuerpoMensaje = ""
        'cuerpoMensaje = cuerpoMensaje & "<table><tr><td>Estimado " & paraNombre & "</td></tr>"
        'cuerpoMensaje = cuerpoMensaje & "<tr><td align='justify'><br> " & Mensaje & " </td></tr>"
        'cuerpoMensaje = cuerpoMensaje & "<tr><td><br>Atte. " & deNombre & " </td></tr>"
        'cuerpoMensaje = cuerpoMensaje & "<tr><td><br><font color=#004080>__________________________________<br>Campus Virtual: Módulo de Quinta Especial</font></td></tr>"
        'cuerpoMensaje = cuerpoMensaje & "<tr><td></td></tr></table>"
        'objMail.EnviarMail(deCorreo, deNombre, paraCorreo, AsuntoCorreo, cuerpoMensaje, True, "hreyes@usat.edu.pe") ' conCopia
        'Antes del cambio 17/05/2019 Mneciosup (Final)

        If tipo = 1 Then
            deNombre = datos.Rows(0).Item("PersonaReg").ToString
            conCopia = datos.Rows(0).Item("CorreoReg").ToString
            paraNombre = datos.Rows(0).Item("PersonaRev").ToString
            paraCorreo = datos.Rows(0).Item("CorreoRev").ToString
            cecos = datos.Rows(0).Item("Centrocostos").ToString
            AsuntoCorreo = "[Mód. Quinta Especial]: Revisar " & cecos

            cuerpoMensaje = ""
            cuerpoMensaje = cuerpoMensaje & "<table><tr><td>Estimado " & paraNombre & "</td></tr>"
            cuerpoMensaje = cuerpoMensaje & "<tr><td align='justify'><br> " & Mensaje & " </td></tr>"
            cuerpoMensaje = cuerpoMensaje & "<tr><td><br>Atte. " & deNombre & " </td></tr>"
            cuerpoMensaje = cuerpoMensaje & "<tr><td><br><font color=#004080>__________________________________<br>Campus Virtual: Módulo de Quinta Especial</font></td></tr>"
            cuerpoMensaje = cuerpoMensaje & "<tr><td></td></tr></table>"
            objMail.EnviarMail(deCorreo, deNombre, paraCorreo, AsuntoCorreo, cuerpoMensaje, True) ' sinCopia    17-05-2019 MNeciosup
        Else
            If tipo = 2 Then
                deNombre = datos.Rows(0).Item("PersonaRev").ToString
                conCopia = datos.Rows(0).Item("CorreoRev").ToString
                paraNombre = datos.Rows(0).Item("PersonaReg").ToString
                paraCorreo = datos.Rows(0).Item("CorreoReg").ToString
                cecos = datos.Rows(0).Item("Centrocostos").ToString
                AsuntoCorreo = "[Mód. Quinta Especial]: Corregir " & cecos

                cuerpoMensaje = ""
                cuerpoMensaje = cuerpoMensaje & "<table><tr><td>Estimado " & paraNombre & "</td></tr>"
                cuerpoMensaje = cuerpoMensaje & "<tr><td align='justify'><br> " & Mensaje & " </td></tr>"
                cuerpoMensaje = cuerpoMensaje & "<tr><td><br>Atte. " & deNombre & " </td></tr>"
                cuerpoMensaje = cuerpoMensaje & "<tr><td><br><font color=#004080>__________________________________<br>Campus Virtual: Módulo de Quinta Especial</font></td></tr>"
                cuerpoMensaje = cuerpoMensaje & "<tr><td></td></tr></table>"
                objMail.EnviarMail(deCorreo, deNombre, paraCorreo, AsuntoCorreo, cuerpoMensaje, True) ' sinCopia    17-05-2019 MNeciosup
            End If
        End If
    End Sub
End Class
