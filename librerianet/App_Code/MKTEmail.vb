Public Class MKTEmail

    Protected _mServidor As String
    Protected _mPuerto As Integer
    Protected _mUser As String
    Protected _mPass As String

    '*************** CONFIGURAR ESTO ***********************
    Private usuario As String = "USAT\campusvirtual"
    Private clave As String = "NLH951"
    Private De As String = "campusvirtual@usat.edu.pe"
    Private nombreenvia As String = "Campus Virtual-USAT"
    '*************** CONFIGURAR ESTO ***********************

    Sub New()
        _mServidor = "172.16.1.5"
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

    Public Function EnviarMail(ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean, Optional ByVal concopia As String = "", Optional ByVal replyTo As String = "") As Boolean

        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De, nombreenvia)
        correo.To.Add(Para)
        If replyTo.Trim <> "" Then
            correo.ReplyTo = New System.Net.Mail.MailAddress(replyTo)
        End If

        If concopia.Trim <> "" And concopia.Trim <> ";" Then
            Dim Destinos() As String
            Destinos = Split(concopia, ";")

            For i As Integer = 0 To Destinos.Length - 1
                correo.CC.Add(Trim(Destinos(i)))
            Next
        End If

        correo.Subject = Asunto
        Dim MMS As String = "<hmtl><body> <br> " & Mensaje & " </body></html>"

        correo.Body = MMS
        correo.IsBodyHtml = HTML
        correo.Priority = System.Net.Mail.MailPriority.High

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Port = _mPuerto
        smtp.Host = _mServidor
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.EnableSsl = False

        If Len(_mUser) <> 0 And Len(_mPass) <> 0 Then
            smtp.UseDefaultCredentials = True
        Else
            ''INGRESAR ACA CREDECIANES DE USUARIO Y CLAVE('USAT\USUARIO', PASS)
            smtp.Credentials = New System.Net.NetworkCredential(usuario, clave)
        End If

        'Try
        smtp.Send(correo)
        Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

End Class
