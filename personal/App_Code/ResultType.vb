Imports Microsoft.VisualBasic

Public Class ResultType
    Private _jobkey As String
    Private _jobtitle As String
    Private _company As String
    Private _city As String
    Private _state As String
    Private _country As String
    Private _formattedLocation As String
    Private _source As String
    Private _datetime As String
    Private _snippet As String
    Private _url As String
    Private _logo As String

    Public Property jobkey() As String
        Get
            Return _jobkey
        End Get
        Set(ByVal value As String)
            _jobkey = value
        End Set
    End Property

    Public Property jobtitle() As String
        Get
            Return _jobtitle
        End Get
        Set(ByVal value As String)
            _jobtitle = value
        End Set
    End Property

    Public Property company() As String
        Get
            Return _company
        End Get
        Set(ByVal value As String)
            _company = value
        End Set
    End Property

    Public Property city() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Public Property state() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
        End Set
    End Property

    Public Property country() As String
        Get
            Return _country
        End Get
        Set(ByVal value As String)
            _country = value
        End Set
    End Property

    Public Property formattedLocation() As String
        Get
            Return _formattedLocation
        End Get
        Set(ByVal value As String)
            _formattedLocation = value
        End Set
    End Property

    Public Property source() As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
        End Set
    End Property


    Public Property datetime() As String
        Get
            Return _datetime
        End Get
        Set(ByVal value As String)
            _datetime = value
        End Set
    End Property

    Public Property snippet() As String
        Get
            Return _snippet
        End Get
        Set(ByVal value As String)
            _snippet = value
        End Set
    End Property

    Public Property url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Public Property logo() As String
        Get
            Return _logo
        End Get
        Set(ByVal value As String)
            _logo = value
        End Set
    End Property

End Class
