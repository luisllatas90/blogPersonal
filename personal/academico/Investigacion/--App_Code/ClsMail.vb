Imports Microsoft.VisualBasic

Public Class ClsMail
    Protected _mServidor As String
    Protected _mPuerto As Integer
    Protected _mUser As String
    Protected _mPass As String

    Sub New()
        _mServidor = "172.16.1.7" '"mail1.usat.edu.pe" "192.168.2.4" 
        _mPuerto = 25
    End Sub

    Sub New(ByVal Servidor As String, ByVal Puerto As Integer)
        Me.New()
        _mServidor = Servidor
        _mPuerto = Puerto
    End Sub

    Sub New(ByVal Servidor As String, ByVal puerto As Integer, ByVal Usuario As String, ByVal Password As String)
        Me.New()
        Me.Servidor = Servidor
        Me.Puerto = puerto
        Me.User = Usuario
        Me.Password = Password
    End Sub

    'Nombre del Servidor
    Public WriteOnly Property Servidor() As String
        Set(ByVal Value As String)
            _mServidor = Value
        End Set
    End Property

    'Nombre del Usuario
    Public WriteOnly Property User() As String
        Set(ByVal value As String)
            _mUser = value
        End Set
    End Property

    'Nombre de la contraseña
    Public WriteOnly Property Password() As String
        Set(ByVal value As String)
            _mPass = value
        End Set
    End Property

    'Numero del Puerto
    Public WriteOnly Property Puerto() As Integer
        Set(ByVal value As Integer)
            _mPuerto = value
        End Set
    End Property

    Public Function EnviarMail(ByVal De As String, ByVal nombreenvia As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean) As Boolean
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De, nombreenvia)
        correo.To.Add(Para)
        correo.Subject = Asunto
        correo.Body = Mensaje
        correo.IsBodyHtml = HTML
        correo.Priority = System.Net.Mail.MailPriority.High

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Port = _mPuerto
        smtp.Host = _mServidor
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.EnableSsl = False

        If Len(_mUser) <> 0 And Len(_mPass) <> 0 Then
            smtp.Credentials = New System.Net.NetworkCredential(_mUser, _mPass)
        End If
        Try
            smtp.Send(correo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EnviarMailVarios(ByVal De As String, ByVal Para As String, ByVal asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean) As Boolean
        Dim Destinos() As String
        Destinos = Split(Para, ";")
        Dim correo As New System.Net.Mail.MailMessage()

        correo.From = New System.Net.Mail.MailAddress(De)
        For i As Integer = 0 To Destinos.Length - 1
            correo.To.Add(Trim(Destinos(i)))
        Next

        correo.Subject = asunto
        correo.Body = Mensaje
        correo.IsBodyHtml = HTML
        correo.Priority = System.Net.Mail.MailPriority.High

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Port = _mPuerto
        smtp.Host = _mServidor
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.EnableSsl = False

        If Len(_mUser) <> 0 And Len(_mPass) <> 0 Then
            smtp.Credentials = New System.Net.NetworkCredential(_mUser, _mPass)
        End If

        Try
            smtp.Send(correo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function MensajeInvestigacion(ByVal tipo As String, ByVal nombredestino As String, ByVal nombreautor As String, ByVal nombreinvestigacion As String) As String

        Dim Mensaje As String
        Mensaje = ""
        Select Case tipo
            Case 1 ' Para una nueva investigación
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Ha recibido una nueva "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong>, para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 2
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El perfil de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 3
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Proyecto de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido ENVIADO para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 4
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El perfil de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 5 ' Para una nueva investigación
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Ha recibido una nueva "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong>, para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 6
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Proyecto de investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO Y EMITIDO SU DECRETO</B> por la Dirección de Investigación"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 7
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Se han registrado nuevos <b>AVANCES</b> de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 8
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Investigaciones pendientes de revisi&oacute;n al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>Se ha registrado un nuevo <b>INFORME</b> de la "
                Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> para su revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 9
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Informe de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 10
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>El Informe de la investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>APROBADO</B> por el Director de Investigación "
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para continuar con el proceso.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 11
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>DESAPROBADA</B> por la Dirección de Investigación"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 12
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>DESAPROBADA</B> por su Director de Departamento"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

            Case 13
                Mensaje = "<html><head><meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' /><title>Mail Investigaciones</title><style type='text/css'>"
                Mensaje = Mensaje + ".Estilo1 {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;}</style></head><body><table width='100%' border='0'>"
                Mensaje = Mensaje + "<tr><td colspan='3' bgcolor='#006699'>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Sr.(a).(ita): <strong>" & nombredestino.ToUpper & "</strong></span></td>"
                Mensaje = Mensaje + "<td>&nbsp;</td><td>&nbsp;</td></tr><tr><td><span class='Estilo1'>Revisión de Investigación al " & Now.ToLongDateString & "</span></td><td>&nbsp;</td><td>&nbsp;</td>"
                Mensaje = Mensaje + "</tr><tr><td colspan='3'>&nbsp;</td></tr><tr><td colspan='3'><div align='justify'><span class='Estilo1'>La investigación que a registrado en el SISTEMA DE INVESTIGACIONES - USAT ha sido <B>OBSERVADA</B>"
                Mensaje = Mensaje + ", verifique en el sistema de INVESTIGACIONES para mas detalle.</span></div></td>"
                'Mensaje = Mensaje + "investigaci&oacute;n <strong>&quot;" & nombreinvestigacion.ToUpper & "&quot;</strong> de <strong>&quot;" & nombreautor.ToUpper & "&quot;</strong> ha sido MODIFICADA para su  revisi&oacute;n, s&iacute;rvase darle la atenci&oacute;n respectiva.</span></div></td>"
                Mensaje = Mensaje + "</tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td colspan='3'><hr></td></tr><tr><td colspan='3'>&nbsp;</td></tr><tr>"
                Mensaje = Mensaje + "<td colspan='3'><span class='Estilo1'>Sistema de Investigaciones <br>Campus Virtual USAT </span></td></tr></table><span class='Estilo1'><br><br></span><br><br><br><br></body></html>"

        End Select
        Return Mensaje

    End Function

End Class