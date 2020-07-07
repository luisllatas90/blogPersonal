Imports Microsoft.VisualBasic

Public Class clsEvento

    Public Function EnviaClavesAlumno(ByVal CodUniversitario As String, _
                                      ByVal Codigo_cco As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_BuscaDatosEmail", Codigo_cco, CodUniversitario)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Dim Mail As New ClsMail
                Dim strMensaje As String = ""
                strMensaje = "Estimado(a) " & dt.Rows(0).Item("Alumno").ToString & "<br/><br/>"
                strMensaje = strMensaje & "Reciba un cordial saludo, nos ponemos en contacto con usted a fin de proporcionale su usuario y clave de acceso a nuestro campus virtual: <br/><br/>"

                '---------------------------------------------------------------------------------------------------------------
                'Fecha: 29.10.2012
                'Usuario: dguevara
                'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                '---------------------------------------------------------------------------------------------------------------
                'strMensaje = strMensaje & "<b>Dirección: </b> <a href='https://intranet.usat.edu.pe/campusvirtual/'>https://intranet.usat.edu.pe/campusvirtual/</a> <br/>" 'andy.diaz 30/10/2018
                'strMensaje = strMensaje & "<b>Dirección: </b> <a href='https://intranet.usat.edu.pe/campusestudiante/'>https://intranet.usat.edu.pe/campusestudiante/</a> <br/>" 'andy.diaz 30/10/2018
                strMensaje = strMensaje & "<b>Dirección: </b> <a href='https://intranet.usat.edu.pe/campusestudiante/'>https://intranet.usat.edu.pe/campusestudiante/</a> <br/>" 'andy.diaz 11/06/2020: Cambio https por http

                strMensaje = strMensaje & "<b>Usuario: </b>" & dt.Rows(0).Item("Usuario").ToString & "<br/>"
                strMensaje = strMensaje & "<b>Clave: </b>" & dt.Rows(0).Item("Clave").ToString & "<br/><br/>"
                strMensaje = strMensaje & "Con estos datos usted podrá acceder a la información correspondiente al " & dt.Rows(0).Item("NombreEvento").ToString & "<br/><br/>"
                strMensaje = strMensaje & "Si tienes alguna consulta o duda con relación a tu inscripción, comunícate con tu Ejecutiva  de Venta<br/><br/>"
                strMensaje = strMensaje & "Atte. <br/><br/>" & "Campus Virtual"

                Dim strCorreo As String = ""
                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                    If (dt.Rows(0).Item("CorreoAlumno").ToString <> "") Then
                        strCorreo = dt.Rows(0).Item("CorreoAlumno").ToString()
                    ElseIf (dt.Rows(0).Item("CorreoAlumno").ToString = "") Then
                        strCorreo = dt.Rows(0).Item("Correo2Alumno").ToString()
                    End If
                Else
                    strCorreo = g_VariablesGlobales.CorreoPrueba
                End If
                
                Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", strCorreo, "[USAT-CV] Envío de Datos de Acceso", strMensaje, True, "", dt.Rows(0).Item("CorreoResponsable").ToString)
                'Mail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual USAT", "csenmache@usat.edu.pe", "[USAT-CV] Envio de Datos de Acceso", strMensaje, True, "", dt.Rows(0).Item("CorreoResponsable").ToString)
                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EnviaFichaInscripcion(ByVal codigoAlu As Integer, ByVal codigoCco As Integer, ByVal codigoPer As Integer) As Boolean
        Try
            Dim dtDatosEnvio As New Data.DataTable
            Dim dtDatosAlumno As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim codigoUniv As String = ""
            dtDatosAlumno = obj.TraerDataTable("ALU_DatosAlumno", codigoAlu)
            If dtDatosAlumno.Rows.Count > 0 Then
                codigoUniv = dtDatosAlumno.Rows(0).Item("codigoUniver_Alu")
            End If

            If Not String.IsNullOrEmpty(codigoUniv) Then
                dtDatosEnvio = obj.TraerDataTable("EVE_BuscaDatosEmail", codigoCco, codigoUniv)
            End If
            obj.CerrarConexion()

            If (dtDatosEnvio.Rows.Count > 0) Then
                Dim md_EnvioCorreosMasivo As New d_EnvioCorreosMasivo
                Dim me_EnvioCorreosMasivo As e_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)

                Dim rutaFicha As String = ConfigurationManager.AppSettings.Item("RutaCampus") & "librerianet/admision/frmExportaFichaPostulacion.aspx?pso=" & dtDatosAlumno.Rows(0).Item("hu") & "&cli=" & codigoAlu

                'Genero el mensaje
                Dim ls_mensaje As String = ""
                ls_mensaje = "Estimado(a) " & dtDatosEnvio.Rows(0).Item("Alumno").ToString & "<br/><br/>"
                ls_mensaje = ls_mensaje & "Reciba un cordial saludo, nos ponemos en contacto con usted a fin de proporcionale un enlace para que pueda descargar su ficha de inscripción: <br/><br/>"
                ls_mensaje = ls_mensaje & "<b>Ficha de Inscripción: </b><a href='" & rutaFicha & "'>Descargar</a><br/><br/>"
                ls_mensaje = ls_mensaje & "Si tienes alguna consulta o duda con relación a tu inscripción, comunícate con tu Ejecutiva  de Venta<br/><br/>"
                ls_mensaje = ls_mensaje & "Atte. <br/><br/>" & "Campus Virtual"

                Dim ls_para As String = ""
                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                    If (dtDatosEnvio.Rows(0).Item("CorreoAlumno").ToString <> "") Then
                        ls_para = dtDatosEnvio.Rows(0).Item("CorreoAlumno").ToString()
                    ElseIf (dtDatosEnvio.Rows(0).Item("CorreoAlumno").ToString = "") Then
                        ls_para = dtDatosEnvio.Rows(0).Item("Correo2Alumno").ToString()
                    End If
                Else
                    ls_para = g_VariablesGlobales.CorreoPrueba
                End If

                Dim ls_replyTo As String = dtDatosEnvio.Rows(0).Item("CorreoResponsable")
                Dim ls_asunto As String = "[USAT-CV] Envío de Ficha de Inscripción"

                With me_EnvioCorreosMasivo
                    .operacion = "I"
                    .cod_user = codigoPer
                    .tipoCodigoEnvio_ecm = "codigo_alu"
                    .codigoEnvio_ecm = codigoAlu
                    .codigo_apl = 37 'POSGRADO
                    .correo_destino = ls_para
                    .correo_respuesta = ls_replyTo
                    .asunto = ls_asunto
                    .cuerpo = ls_mensaje
                End With

                md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo)

                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
