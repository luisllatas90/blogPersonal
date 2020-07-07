Imports Microsoft.VisualBasic

Public Class ClsMail
    Protected _mServidor As String
    Protected _mPuerto As Integer
    Protected _mUser As String
    Protected _mPass As String

    Sub New()
        _mServidor = "mail1.usat.edu.pe"
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

    Public Function EnviarMail(ByVal De As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean) As Boolean
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De)
        correo.To.Add(Para)
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

End Class
