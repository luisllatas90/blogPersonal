Imports Microsoft.VisualBasic

Public Class ClsMail
    Protected _mServidor As String
    Protected _mPuerto As Integer
    Protected _mUser As String
    Protected _mPass As String

    Sub New()
        '_mServidor = "172.16.1.7" '"192.168.2.4" "172.16.1.7"
        _mServidor = "10.10.1.25" '"192.168.2.4" "172.16.1.7"
        _mPuerto = 2526
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

    Public Function EnviarMail(ByVal De As String, ByVal nombreenvia As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean, Optional ByVal concopia As String = "", Optional ByVal replyTo As String = "") As Boolean
        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De, nombreenvia)
        correo.To.Add(Para)
        If replyTo.Trim <> "" Then
            correo.ReplyTo = New System.Net.Mail.MailAddress(replyTo)
        End If

        If concopia.trim <> "" And concopia.Trim <> ";" Then
            Dim Destinos() As String
            Destinos = Split(concopia, ";")

            For i As Integer = 0 To Destinos.Length - 1
                correo.CC.Add(Trim(Destinos(i)))
            Next
        End If

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
            smtp.UseDefaultCredentials = True
        Else
            smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "XCR176BUF515")
        End If

        'Try
        smtp.Send(correo)
        Return True
        'Catch ex As Exception
        '    Return False
        'End Try
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

    Public Function EnviarMailVariosV2(ByVal De As String, ByVal Para As String, ByVal asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean) As Boolean
        Try
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
                smtp.UseDefaultCredentials = True
            Else
                smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "XCR176BUF515")
            End If


            smtp.Send(correo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EnviarMailVariosV3(ByVal De As String, ByVal Para As String, ByVal asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean) As String
        Try
            Dim Destinos() As String
            Destinos = Split(Para, ";")
            Dim correo As New System.Net.Mail.MailMessage()

            correo.From = New System.Net.Mail.MailAddress(De)
            For i As Integer = 0 To Destinos.Length - 1
                If Trim(Destinos(i)) <> "" Then
                    correo.To.Add(Trim(Destinos(i)))
                End If
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
                smtp.UseDefaultCredentials = True
            Else
                smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "XCR176BUF515")
            End If

            smtp.Send(correo)

            Return "1,Correo enviado"
        Catch ex As Exception
            Return Err.Number & "," & Err.Description
        End Try
    End Function


    Public Function EnviarPDFMail(ByVal De As String, ByVal nombreEnvia As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean, ByVal nfile As String, ByVal file As System.IO.MemoryStream, Optional ByVal concopia As String = "", Optional ByVal replyTo As String = "") As Boolean
        Try
            Dim correo As New System.Net.Mail.MailMessage()
            correo.From = New System.Net.Mail.MailAddress(De, nombreEnvia)
            correo.To.Add(Para)

            If replyTo.Trim <> "" Then
                correo.ReplyTo = New System.Net.Mail.MailAddress(replyTo)
            End If

            If concopia.Trim <> "" And concopia.Trim <> ";" Then
                Dim Destinos() As String
                Destinos = Split(concopia, ";")
                For i As Integer = 0 To Destinos.Length - 1
                    correo.Bcc.Add(Trim(Destinos(i)))
                Next
            End If

            correo.Subject = Asunto
            correo.Body = Mensaje
            correo.IsBodyHtml = HTML
            correo.Priority = System.Net.Mail.MailPriority.High
            correo.Attachments.Add(New System.Net.Mail.Attachment(file, nfile & ".pdf"))

            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.Port = _mPuerto
            smtp.Host = _mServidor
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.EnableSsl = False

            If Len(_mUser) <> 0 And Len(_mPass) <> 0 Then
                smtp.UseDefaultCredentials = True
            Else
                smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "XCR176BUF515")
            End If

            smtp.Send(correo)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ' Enviar Correo con Copia Oculta
    Public Function EnviarMailCCO(ByVal De As String, ByVal nombreenvia As String, ByVal Para As String, ByVal Asunto As String, ByVal Mensaje As String, ByVal HTML As Boolean, Optional ByVal concopia As String = "", Optional ByVal replyTo As String = "") As Boolean

        Dim correo As New System.Net.Mail.MailMessage()
        correo.From = New System.Net.Mail.MailAddress(De, nombreenvia)
        correo.To.Add(Para)
        If replyTo.Trim <> "" Then
            correo.ReplyTo = New System.Net.Mail.MailAddress(replyTo)
        End If

        If concopia.trim <> "" And concopia.Trim <> ";" Then
            Dim Destinos() As String
            Destinos = Split(concopia, ";")

            For i As Integer = 0 To Destinos.Length - 1
                correo.Bcc.Add(Trim(Destinos(i)))
            Next
        End If

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
            smtp.UseDefaultCredentials = True
        Else
            smtp.Credentials = New System.Net.NetworkCredential("USAT\campusvirtual", "XCR176BUF515")
        End If

        'Try
        smtp.Send(correo)
        Return True
        'Catch ex As Exception
        '    Return False
        'End Try
    End Function

End Class

