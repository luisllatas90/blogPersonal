Imports Microsoft.VisualBasic

Public Class ClsEnviaMail
    Public Sub ConsultarEnvioMail(ByVal codigo_sol As Int32, Optional ByVal foto As String = "")
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dim Dat_Evaluador, Dat_EvaluadorAux As New Data.DataTable

        Dat_Evaluador = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 1, codigo_sol)
        Mensaje = ""
        FormatoMensaje(Mensaje)
        Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
        Mensaje = Mensaje & "<tr>"
        Mensaje = Mensaje & "    <td>&nbsp;</td>"
        Mensaje = Mensaje & "</tr>"
        Mensaje = Mensaje & "<tr>"

        'Envia mail a los evaluadores
        If Dat_Evaluador.Rows.Count > 0 Then
            Mensaje = Mensaje & "    <td colspan='2'><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Select Case Dat_Evaluador.Rows(0).Item("NIVEL_EVA")
                Case 1
                    Mensaje = Mensaje & "    <td colspan='2'>Sr(a)(ta). Coordinador Académico <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
                Case 2
                    Mensaje = Mensaje & "    <td colspan='2'>Sr. Director Académico <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
                Case 3
                    Mensaje = Mensaje & "    <td colspan='2'>Sr. Admistrador General <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            End Select

            Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Evaluador.Rows(0).Item("numero_sol") & "</b> del estudiante:"
            Mensaje &= "</td>"
            Mensaje &= "<tr><td><img src='" & foto & "' width='80' height='100'/></td>"
            Mensaje &= "<td>"
            Mensaje &= "Nombres y Apellidos: <br/><b>" & Dat_Evaluador.Rows(0).Item("alumno") & "</b></br>"
            Mensaje = Mensaje & "<br/> Código Universitario:</br> <b>" & Dat_Evaluador.Rows(0).Item("codigouniver_alu") & "</b></br></td></tr>"

            Mensaje = Mensaje & "<tr><td  colspan='2'> Ha sido registrada el día <b>" & Dat_Evaluador.Rows(0).Item("fecha_sol") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br></td> "

            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td colspan='2'><hr></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr> " & Chr(13)
            Mensaje = Mensaje & "    <td colspan='2' ><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>"
            Mensaje = Mensaje & "</table>" & Chr(13)

            AsuntoCorreo = "1 Solicitud Pendiente"
            Correo = Dat_Evaluador.Rows(0).Item("correo").ToString
            'Correo = "clluen@usat.edu.pe" ' comentar
            'Con copia a
            ConCopiaA = ""
            Dat_EvaluadorAux = ObjCnx.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 1, Dat_Evaluador.Rows(0).Item("codigo_cco"), 0)
            If Dat_EvaluadorAux.Rows.Count > 0 Then
                For i As Int16 = 0 To Dat_EvaluadorAux.Rows.Count - 1
                    ConCopiaA = Dat_EvaluadorAux.Rows(i).Item("correo") & ";" & ConCopiaA
                Next
                ConCopiaA = Left(ConCopiaA, Len(ConCopiaA) - 1)
            End If

            'Cambio 22.06.2017 - YPEREZ 22.06.2017 Verificar si solicitud es de retiro de semestre: codigo_tas =2 para enviar mail también a la Director de Tutoría.
            If Dat_Evaluador.Rows(0).Item("esRetiro").ToString = "1" Then
                If ConCopiaA <> "" Then
                    ConCopiaA = ConCopiaA & ";mescuza@usat.edu.pe;yperez@usat.edu.pe"
                Else
                    ConCopiaA = "mescuza@usat.edu.pe;yperez@usat.edu.pe"
                End If

            End If


            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
            ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "EVAL", Dat_Evaluador.Rows(0).Item("codigo_eva"))
        End If
        AvisarAccion(codigo_sol)

    End Sub



    Public Sub ConsultarEnvioMailPP(ByVal codigo_sol As Int32, Optional ByVal foto As String = "")
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dim Dat_Evaluador, Dat_EvaluadorAux As New Data.DataTable

        Dat_Evaluador = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 1, codigo_sol)
        Mensaje = ""
        FormatoMensaje(Mensaje)
        Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
        Mensaje = Mensaje & "<tr>"
        Mensaje = Mensaje & "    <td>&nbsp;</td>"
        Mensaje = Mensaje & "</tr>"
        Mensaje = Mensaje & "<tr>"

        'Envia mail a los evaluadores
        If Dat_Evaluador.Rows.Count > 0 Then
            Mensaje = Mensaje & "    <td colspan='2'><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Select Case Dat_Evaluador.Rows(0).Item("NIVEL_EVA")
                Case 1
                    Mensaje = Mensaje & "    <td colspan='2'>Sr(a)(ta). Coordinador Académico <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
                Case 2
                    Mensaje = Mensaje & "    <td colspan='2'>Sr. Director Académico <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
                Case 3
                    Mensaje = Mensaje & "    <td colspan='2'>Sr. Gerente General <b>" & Dat_Evaluador.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            End Select

            Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Evaluador.Rows(0).Item("numero_sol") & "</b> del estudiante:"
            Mensaje &= "</td>"
            Mensaje &= "<tr><td><img src='" & foto & "' width='80' height='100'/></td>"
            Mensaje &= "<td>"
            Mensaje &= "Nombres y Apellidos: <br/><b>" & Dat_Evaluador.Rows(0).Item("alumno") & "</b></br>"
            Mensaje = Mensaje & "<br/> Código Universitario:</br> <b>" & Dat_Evaluador.Rows(0).Item("codigouniver_alu") & "</b></br></td></tr>"

            Mensaje = Mensaje & "<tr><td  colspan='2'> Ha sido registrada el día <b>" & Dat_Evaluador.Rows(0).Item("fecha_sol") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br></td> "

            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td colspan='2'><hr></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr> " & Chr(13)
            Mensaje = Mensaje & "    <td colspan='2' ><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>"
            Mensaje = Mensaje & "</table>" & Chr(13)

            AsuntoCorreo = "1 Solicitud Pendiente"
            Correo = Dat_Evaluador.Rows(0).Item("correo").ToString

            'Con copia a
            ConCopiaA = ""
            'ConCopiaA = "lrossmorrey@usat.edu.pe" & ";" & ConCopiaA
            ConCopiaA = "madrianzen@usat.edu.pe" & ";" & ConCopiaA

            ConCopiaA = Left(ConCopiaA, Len(ConCopiaA) - 1)
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
            ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "EVAL", Dat_Evaluador.Rows(0).Item("codigo_eva"))
        End If
        AvisarAccion(codigo_sol)

    End Sub



    Private Sub FormatoMensaje(ByRef Mensaje As String)
        Mensaje = Mensaje & "<style type=text/css>" & Chr(13)
        Mensaje = Mensaje & "<!--" & Chr(13)
        Mensaje = Mensaje & "body,td,th {" & Chr(13)
        Mensaje = Mensaje & "font-family: Verdana, Arial, Helvetica, sans-serif;" & Chr(13)
        Mensaje = Mensaje & "    font-size: 12px;" & Chr(13)
        Mensaje = Mensaje & "}" & Chr(13)
        Mensaje = Mensaje & ".Estilo1 {color: #FFFFFF}" & Chr(13)
        Mensaje = Mensaje & "-->" & Chr(13)
        Mensaje = Mensaje & "</style>"
    End Sub

    Private Sub EnviarMailCaja(ByVal codigo_sol As Int32)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopia As String
        Dim Dat_Accion As New Data.DataTable
        Mensaje = ""
        ConCopia = ""
        FormatoMensaje(Mensaje)
        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 5, codigo_sol)
        If Dat_Accion.Rows.Count > 0 Then
            If CInt(Dat_Accion.Rows(0).Item("aprobado")) = 1 Then
                Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td>&nbsp;</td>"
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para generar cargo al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td> <b>Srta. Luviana Montalvo M.</b></br></br></br>"
                Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Accion.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Accion.Rows(0).Item("alumno") & "</b>"
                Mensaje = Mensaje & "    con código universitario <b>" & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</b> de la Carrera Profesional <b>" & Dat_Accion.Rows(0).Item("nombre_cpf") & "</b>"
                Mensaje = Mensaje & "    ha sido aprobada por el Director de escuela, puede proceder con el cargo del <b>" & Dat_Accion.Rows(0).Item("descripcion_tas") & ": " & Dat_Accion.Rows(0).Item("descripcion") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br> "
                Mensaje = Mensaje & "    </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr> " & Chr(13)
                Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "</table>" & Chr(13)
                Correo = "fseclen@usat.edu.pe"
                'Correo = "hreyes@usat.edu.pe"

                If CInt(Dat_Accion.Rows(0).Item("codigo_tas")) = 10 Then ' exam. extraordinario
                    AsuntoCorreo = "[Módulo de solicitudes " & Dat_Accion.Rows(0).Item("numero_sol") & "] EXAM. EXTRAORDINARIO - " & Dat_Accion.Rows(0).Item("alumno") & " - " & Dat_Accion.Rows(0).Item("descripcion")
                Else 'If CInt(Dat_Accion.Rows(0).Item("codigo_tas")) = 16 Then
                    AsuntoCorreo = "[Módulo de solicitudes " & Dat_Accion.Rows(0).Item("numero_sol") & "] TRASLADO INTERNO - " & Dat_Accion.Rows(0).Item("alumno") & " - " & Dat_Accion.Rows(0).Item("descripcion")
                End If
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True)
                ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SCAJA", codigo_sol)
            End If
        End If
        Dat_Accion.Dispose()
    End Sub

    Public Sub EnviaMailEvaluacionRegistro(ByVal codigo_sol As Int32)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dim Dat_Accion As New Data.DataTable
        Dim j As Int16
        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 2, codigo_sol)
        If CInt(Dat_Accion.Rows(0).Item("aprobadas")) = CInt(Dat_Accion.Rows(0).Item("evaluacion")) Then
            Mensaje = ""
            FormatoMensaje(Mensaje)
            Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
            Mensaje = Mensaje & "<tr>"
            Mensaje = Mensaje & "    <td>&nbsp;</td>"
            Mensaje = Mensaje & "</tr>"
            Mensaje = Mensaje & "<tr>"

            Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td><p> <b>Srs. de Evaluación y registro</b></br></br>"
            Mensaje = Mensaje & "    <b>DATOS DE LA SOLICITUD</b></br></br>"
            Mensaje = Mensaje & "    Fecha de solicitud: " & Dat_Accion.Rows(0).Item("fecha_sol") & "</br>"
            Mensaje = Mensaje & "    Fecha de aprobación: " & Date.Now.Date.ToShortDateString & "</br>"
            Mensaje = Mensaje & "    Número de solicitud: " & Dat_Accion.Rows(0).Item("numero_sol") & "</br>"
            Mensaje = Mensaje & "    Estudiante: " & Dat_Accion.Rows(0).Item("alumno") & "</br>"
            Mensaje = Mensaje & "    Código universitario: " & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</br>"
            Mensaje = Mensaje & "    Carrera profesional: " & Dat_Accion.Rows(0).Item("nombre_cpf") & "</br></br></p>"

            Dim ArrDescripcion() As String
            '::::::::::ASUNTOS::::::::::
            Mensaje = Mensaje & "<b> ASUNTO(S): " & Dat_Accion.Rows(0).Item("descripcion_tas").ToString.ToUpper & "</b>: </br>"
            'For i = 0 To UBound(ArrDescripcion) 'Para recorrer varios elementos de un array, pero como aqui son dos lo hacemos de la sgte manera

            If (Dat_Accion.Rows(0).Item("descripcion") Is System.DBNull.Value) = False And InStr(Dat_Accion.Rows(0).Item("descripcion"), " || ") > 0 Then
                ArrDescripcion = Split(Dat_Accion.Rows(0).Item("descripcion"), " || ", , CompareMethod.Text)
                Mensaje = Mensaje & "» " & ArrDescripcion(0).ToString & "</br>"
                Mensaje = Mensaje & "» " & ArrDescripcion(1).ToString & "</br>"
            Else
                Mensaje = Mensaje & "» " & Dat_Accion.Rows(0).Item("descripcion") & "</br>"
            End If
            Mensaje = Mensaje & " <b></br>POR MOTIVO(S):</br></b>"
            Dat_Accion.Dispose()

            '::::::::::MOTIVOS::::::::::
            Dim Dat_Motivo As Data.DataTable
            Dim motivo As String = ""
            Dat_Motivo = ObjCnx.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 3, codigo_sol)
            For j = 0 To Dat_Motivo.Rows.Count - 1
                motivo = "»" & Dat_Motivo.Rows(j).Item("motivo") & "</br> " & motivo
            Next
            Dat_Motivo.Dispose()

            '::::::::::OBSERVACIONES::::::::::
            Dim Dat_Observacion As New Data.DataTable
            Dat_Observacion = ObjCnx.TraerDataTable("SOL_ConsultarObservacionDirector", 1, codigo_sol)
            If Dat_Observacion.Rows.Count > 0 Then
                Mensaje = Mensaje & motivo & ".</br></br>"
                Mensaje = Mensaje & "<b>OBSERVACIONES DEL DIRECTOR DE ESCUELA:</b>"
                Mensaje = Mensaje & "<p>" & Dat_Observacion.Rows(0).Item("observacion_Eva").ToString & "</p>"
            End If
            Mensaje = Mensaje & "</br> Por favor sirvase atenderla. </br></br> "
            Dat_Observacion.Dispose()

            AsuntoCorreo = "1 Solicitud Pendiente"
            Correo = "eurpeque@usat.edu.pe"
            ConCopiaA = "msanchez@usat.edu.pe; hreyes@usat.edu.pe"
            'Correo = "hreyes@usat.edu.pe"
            ConCopiaA = ""
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
            ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SACAD", codigo_sol)
        End If
    End Sub
    Private Sub AvisarAccion(ByRef codigo_sol As Int32)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dim Dat_Accion As New Data.DataTable
        Dim accion, j As Int16
        EnviarMailCaja(codigo_sol)
        Mensaje = ""
        accion = 0
        FormatoMensaje(Mensaje)

        '### Verifica si los asuntos de retiro o agregados procedieron para enviar correo a mvilchez ###
        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 2, codigo_sol)
        If Dat_Accion.Rows.Count > 0 Then
            If CInt(Dat_Accion.Rows(0).Item("aprobadas")) = CInt(Dat_Accion.Rows(0).Item("evaluacion")) Then
                If Dat_Accion.Rows(0).Item("nivelactual_Sol") > 2 Then
                    accion = 0
                Else
                    If Dat_Accion.Rows(0).Item("nivelactual_Sol") = 1 Then
                        accion = 0
                    Else
                        accion = 2
                        Dim Dat_Observacion As New Data.DataTable
                        Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
                        Mensaje = Mensaje & "<tr>"
                        Mensaje = Mensaje & "    <td>&nbsp;</td>"
                        Mensaje = Mensaje & "</tr>"
                        Mensaje = Mensaje & "<tr>"
                        Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
                        Mensaje = Mensaje & "</tr>" & Chr(13)
                        Mensaje = Mensaje & "<tr>" & Chr(13)
                        Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
                        Mensaje = Mensaje & "</tr>" & Chr(13)
                        Mensaje = Mensaje & "<tr>" & Chr(13)
                        Mensaje = Mensaje & "    <td><p> <b>Srs. de Evaluación y registro</b></br></br>"
                        Mensaje = Mensaje & "    <b>DATOS DE LA SOLICITUD</b></br></br>"
                        Mensaje = Mensaje & "    Fecha de solicitud: " & Dat_Accion.Rows(0).Item("fecha_sol") & "</br>"
                        Mensaje = Mensaje & "    Fecha de aprobación: " & Date.Now.Date.ToShortDateString & "</br>"
                        Mensaje = Mensaje & "    Número de solicitud: " & Dat_Accion.Rows(0).Item("numero_sol") & "</br>"
                        Mensaje = Mensaje & "    Estudiante: " & Dat_Accion.Rows(0).Item("alumno") & "</br>"
                        Mensaje = Mensaje & "    Código universitario: " & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</br>"
                        Mensaje = Mensaje & "    Carrera profesional: " & Dat_Accion.Rows(0).Item("nombre_cpf") & "</br></br></p>"

                        Dim ArrDescripcion() As String
                        '::::::::::ASUNTOS::::::::::
                        Mensaje = Mensaje & "<b> ASUNTO(S): " & Dat_Accion.Rows(0).Item("descripcion_tas").ToString.ToUpper & "</b>: </br>"
                        'For i = 0 To UBound(ArrDescripcion) 'Para recorrer varios elementos de un array, pero como aqui son dos lo hacemos de la sgte manera

                        If (Dat_Accion.Rows(0).Item("descripcion") Is System.DBNull.Value) = False And InStr(Dat_Accion.Rows(0).Item("descripcion"), " || ") > 0 Then
                            ArrDescripcion = Split(Dat_Accion.Rows(0).Item("descripcion"), " || ", , CompareMethod.Text)
                            Mensaje = Mensaje & "» " & ArrDescripcion(0).ToString & "</br>"
                            Mensaje = Mensaje & "» " & ArrDescripcion(1).ToString & "</br>"
                        Else
                            Mensaje = Mensaje & "» " & Dat_Accion.Rows(0).Item("descripcion") & "</br>"
                        End If
                        Mensaje = Mensaje & " <b></br>POR MOTIVO(S):</br></b>"

                        '::::::::::MOTIVOS::::::::::
                        Dim Dat_Motivo As Data.DataTable
                        Dim motivo As String = ""
                        Dat_Motivo = ObjCnx.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 3, codigo_sol)
                        For j = 0 To Dat_Motivo.Rows.Count - 1
                            motivo = "»" & Dat_Motivo.Rows(j).Item("motivo") & "</br> " & motivo
                        Next
                        Mensaje = Mensaje & motivo & ".</br></br>"
                        Dat_Motivo.Dispose()
                        
                        '::::::OBSERVACIONES DE LA SOLICITUD:::::::
                        Mensaje = Mensaje & "<b>OBSERVACIONES DEL ESTUDIANTE AGREGADAS A LA SOLICITUD:</b>"
                        Mensaje = Mensaje & "<p>" & Dat_Accion.Rows(0).Item("observaciones_sol").ToString & "</p></br>"
                        
                        '::::::::::OBSERVACIONES::::::::::
                        Dat_Observacion = ObjCnx.TraerDataTable("SOL_ConsultarObservacionDirector", 1, codigo_sol)
                        If Dat_Observacion.Rows.Count > 0 Then
                            Mensaje = Mensaje & "<b>OBSERVACIONES DEL DIRECTOR DE ESCUELA:</b>"
                            Mensaje = Mensaje & "<p>" & Dat_Observacion.Rows(0).Item("observacion_Eva").ToString & "</p>"
                        End If
                        Mensaje = Mensaje & "</br> Por favor sirvase atenderla. </br></br> "
                End If
            End If
        End If
        End If
        Dat_Accion.Dispose()

        '### Verifica el asunto es reserva de matricula y procedio para enviar correo a jdanjanovic ###
        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 3, codigo_sol)
        If Dat_Accion.Rows.Count > 0 Then
            If CInt(Dat_Accion.Rows(0).Item("aprobado")) = 1 Then
                accion = 3
                Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td>&nbsp;</td>"
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td> <b>Sra. DANJANOVIC LEÓN JULIA</b></br></br></br>"
                Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Accion.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Accion.Rows(0).Item("alumno") & "</b>"
                Mensaje = Mensaje & "    con código universitario <b>" & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</b>"
                Mensaje = Mensaje & "    ha sido aprobada puede proceder con la <b>" & Dat_Accion.Rows(0).Item("descripcion_tas") & ": " & Dat_Accion.Rows(0).Item("descripcion") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br> "
                Mensaje = Mensaje & "    </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr> " & Chr(13)
                Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "</table>" & Chr(13)
            End If
        End If

        AsuntoCorreo = "1 Solicitud Aprobada"
        'Correo = "hreyes@usat.edu.pe"
        ConCopiaA = ""
        If accion = 2 Then
            Correo = "eurpeque@usat.edu.pe"
            ConCopiaA = "msanchez@usat.edu.pe; hreyes@usat.edu.pe"
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
            ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SACAD", codigo_sol)
        ElseIf accion = 3 Then
            Correo = "jdanjanovic@usat.edu.pe"
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True)
            ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SRESER", codigo_sol)
        End If

        '### Si es anulación de deuda envia un mensaje a jdanjanovic para que proceda ###
        Dat_Accion.Dispose()
        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 4, codigo_sol)
        If Dat_Accion.Rows.Count > 0 Then
            If CInt(Dat_Accion.Rows(0).Item("aprobado")) = 1 Then
                Mensaje = ""
                FormatoMensaje(Mensaje)
                Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td>&nbsp;</td>"
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td> <b>Sra. DANJANOVIC LEÓN JULIA</b></br></br></br>"
                Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Accion.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Accion.Rows(0).Item("alumno") & "</b>"
                Mensaje = Mensaje & "    con código universitario <b>" & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</b>"
                Mensaje = Mensaje & "    ha sido aprobada puede proceder con la <b>" & Dat_Accion.Rows(0).Item("descripcion_tas") & ": " & Dat_Accion.Rows(0).Item("descripcion") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br> "

                Mensaje = Mensaje & "    </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr> " & Chr(13)
                Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "</table>" & Chr(13)
                AsuntoCorreo = "1 Solicitud Pendiente"

                Correo = "jdanjanovic@usat.edu.pe"
                'Correo = "hreyes@usat.edu.pe"
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True)
                ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SDEUDA", codigo_sol)
            End If
        End If
        Dat_Accion.Dispose()
        Mensaje = ""
    End Sub

    Public Sub EnviarMailObservadas(ByVal id_sol As Int32, ByVal DerivarA As Int32, ByVal Observacion As String)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Dat_Solicitud, Dat_EvaluadorAux As New Data.DataTable
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dat_Solicitud = ObjCnx.TraerDataTable("SOL_ConsultarSolicitudesObservadas", 1, id_sol, DerivarA)
        If Dat_Solicitud.Rows.Count > 0 Then
            Mensaje = ""
            FormatoMensaje(Mensaje)
            Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
            Mensaje = Mensaje & "<tr>"
            Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 <strong>solicitud observada</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            If DerivarA = 1 Then
                Mensaje = Mensaje & "    <td>Sr(a)(ta). Director(a) de Escuela <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            ElseIf DerivarA = 2 Then
                Mensaje = Mensaje & "    <td>Sr. Director Académico <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            ElseIf DerivarA = 3 Then
                Mensaje = Mensaje & "    <td>Sr. Administrador General <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            End If
            Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Solicitud.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Solicitud.Rows(0).Item("alumno") & "</b>"
            Mensaje = Mensaje & "    con código universitario <b>" & Dat_Solicitud.Rows(0).Item("codigouniver_alu") & "</b>"
            Mensaje = Mensaje & "    ha sido OBSERVADA. Por favor sirvase revisar la solicitud en el menú <b>solicitudes observadas</b> para continuar con el proceso.</br> </br> "

            Mensaje = Mensaje & "    </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr> " & Chr(13)
            Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>"
            Mensaje = Mensaje & "</table>" & Chr(13)

            ConCopiaA = ""
            Dat_EvaluadorAux = ObjCnx.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 1, Dat_Solicitud.Rows(0).Item("codigo_cco"), 0)
            If Dat_EvaluadorAux.Rows.Count > 0 Then
                For i As Int16 = 0 To Dat_EvaluadorAux.Rows.Count - 1
                    ConCopiaA = Dat_EvaluadorAux.Rows(i).Item("correo") & ";" & ConCopiaA
                Next
                ConCopiaA = Left(ConCopiaA, Len(ConCopiaA) - 1)
            End If
            AsuntoCorreo = "1 Solicitud Observada"

            Correo = Dat_Solicitud.Rows(0).Item("correo").ToString
            'Correo = "hreyes@usat.edu.pe"
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
        End If
    End Sub

    Public Sub EnvioMailRespuestaModificada(ByVal codigo_sol As Int32, ByVal nivel As Int32)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Dat_Solicitud, Dat_EvaluadorAux As New Data.DataTable
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String
        Dat_Solicitud = ObjCnx.TraerDataTable("SOL_ConsultarSolicitudesObservadas", 2, codigo_sol, nivel)

        If Dat_Solicitud.Rows.Count > 0 Then
            Mensaje = ""
            FormatoMensaje(Mensaje)
            Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
            Mensaje = Mensaje & "<tr>"
            Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 <strong>solicitud observada que fue modificada</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)
            Mensaje = Mensaje & "<tr>" & Chr(13)
            If Dat_Solicitud.Rows(0).Item("nivel_Eva") = 1 Then
                Mensaje = Mensaje & "    <td>Sr(a)(ta). Director(a) de Escuela <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            ElseIf Dat_Solicitud.Rows(0).Item("nivel_Eva") = 2 Then
                Mensaje = Mensaje & "    <td>Sr. Director Académico <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            ElseIf Dat_Solicitud.Rows(0).Item("nivel_Eva") = 3 Then
                Mensaje = Mensaje & "    <td>Sr. Administrador General <b>" & Dat_Solicitud.Rows(0).Item("PERSONA") & "</b></br></br></br>"
            End If
            Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Solicitud.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Solicitud.Rows(0).Item("alumno") & "</b>"
            Mensaje = Mensaje & "    con código universitario <b>" & Dat_Solicitud.Rows(0).Item("codigouniver_alu") & "</b>"
            Mensaje = Mensaje & "    ha sido Modificada. Por favor sirvase revisar la solicitud</b> para continuar con el proceso.</br> </br> "

            Mensaje = Mensaje & "    </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr>" & Chr(13)
            Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
            Mensaje = Mensaje & "</tr>" & Chr(13)

            Mensaje = Mensaje & "<tr> " & Chr(13)
            Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
            Mensaje = Mensaje & "</tr>"
            Mensaje = Mensaje & "</table>" & Chr(13)

            ConCopiaA = ""
            Dat_EvaluadorAux = ObjCnx.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 1, Dat_Solicitud.Rows(0).Item("codigo_cco"), 0)
            If Dat_EvaluadorAux.Rows.Count > 0 Then
                For i As Int16 = 0 To Dat_EvaluadorAux.Rows.Count - 1
                    ConCopiaA = Dat_EvaluadorAux.Rows(i).Item("correo") & ";" & ConCopiaA
                Next
                ConCopiaA = Left(ConCopiaA, Len(ConCopiaA) - 1)
            End If
            AsuntoCorreo = "1 Solicitud Modificada"

            Correo = Dat_Solicitud.Rows(0).Item("correo").ToString
            'Correo = "hreyes@usat.edu.pe"
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
        End If

    End Sub

    Public Sub EnviarMailAnulacion(ByVal codigo_sol As Int32)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim ObjMailNet As New ClsMail
        Dim Dat_Accion As New Data.DataTable
        Dim Mensaje, Correo, AsuntoCorreo As String

        Dat_Accion = ObjCnx.TraerDataTable("SOL_EnviarMailSolicitud", 4, codigo_sol)
        If Dat_Accion.Rows.Count > 0 Then
            If CInt(Dat_Accion.Rows(0).Item("aprobado")) = 1 Then
                Mensaje = ""
                FormatoMensaje(Mensaje)
                Mensaje = Mensaje & "<table width=80% border=0 align=center cellpadding=3 cellspacing=0>" & Chr(13)
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td>&nbsp;</td>"
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "<tr>"
                Mensaje = Mensaje & "    <td><font color=#000000>Hay 1 Solicitud <strong>pendiente</strong> para su revisión al <strong>" & Now.ToShortDateString & "</strong> a las <strong>" & Now.ToShortTimeString & "</strong> horas.</font></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td>&nbsp;</td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)
                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td> <b>Sra. DANJANOVIC LEÓN JULIA</b></br></br></br>"
                Mensaje = Mensaje & "    La solicitud número <b>" & Dat_Accion.Rows(0).Item("numero_sol") & "</b> del estudiante <b>" & Dat_Accion.Rows(0).Item("alumno") & "</b>"
                Mensaje = Mensaje & "    con código universitario <b>" & Dat_Accion.Rows(0).Item("codigouniver_alu") & "</b>"
                Mensaje = Mensaje & "    ha sido aprobada puede proceder con la <b>" & Dat_Accion.Rows(0).Item("descripcion_tas") & ": " & Dat_Accion.Rows(0).Item("descripcion") & "</b>.</br> </br> Por favor sirvase atenderla. </br></br> "

                Mensaje = Mensaje & "    </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr>" & Chr(13)
                Mensaje = Mensaje & "    <td><hr></td>" & Chr(13)
                Mensaje = Mensaje & "</tr>" & Chr(13)

                Mensaje = Mensaje & "<tr> " & Chr(13)
                Mensaje = Mensaje & "    <td><font color=#004080>Sistema de Solicitudes <br> Campus Virtual - USAT</font> </td>" & Chr(13)
                Mensaje = Mensaje & "</tr>"
                Mensaje = Mensaje & "</table>" & Chr(13)

                AsuntoCorreo = "1 Solicitud Pendiente"

                Correo = "jdanjanovic@usat.edu.pe"
                'Correo = "hreyes@usat.edu.pe"
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Sistema de Solicitudes", Correo, AsuntoCorreo, Mensaje, True)
                ObjCnx.Ejecutar("SOL_ActualizarEnviaMailSolicitud", "SDEUDA", codigo_sol)
            End If
        End If
    End Sub

End Class
