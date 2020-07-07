Imports Microsoft.VisualBasic

Public Class ClsRespuestaCaptcha
    Private _success As String
    Private _challenge_ts As String
    Private _hostname As String
    Private _error As String

    Public Property success() As String
        Get
            Return _success
        End Get
        Set(ByVal value As String)
            _success = value
        End Set
    End Property

    Public Property challenge_ts() As String
        Get
            Return _challenge_ts
        End Get
        Set(ByVal value As String)
            _challenge_ts = value
        End Set
    End Property
    Public Property hostname() As String
        Get
            Return _hostname
        End Get
        Set(ByVal value As String)
            _hostname = value
        End Set
    End Property
    Public Property errorCaptcha() As String
        Get
            Return _error
        End Get
        Set(ByVal value As String)
            _error = value
        End Set
    End Property
End Class
