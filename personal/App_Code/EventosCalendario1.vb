Imports Microsoft.VisualBasic

Public Class EventosCalendario1
    Private m_id As Integer
    Public Property id() As Integer
        Get
            Return m_id
        End Get
        Set(ByVal value As Integer)
            m_id = value
        End Set
    End Property

    Private m_title As String
    Public Property title() As String
        Get
            Return m_title
        End Get
        Set(ByVal value As String)
            m_title = value
        End Set
    End Property

    Private m_description As String
    Public Property description() As String
        Get
            Return m_description
        End Get
        Set(ByVal value As String)
            m_description = value
        End Set
    End Property

    Private m_start As Long

    Public Property start() As Long
        Get
            Return m_start
        End Get
        Set(ByVal value As Long)
            m_start = value
        End Set
    End Property

    Private m_end As Long
    Public Property [end]() As Long
        Get
            Return m_end
        End Get
        Set(ByVal value As Long)
            m_end = value
        End Set
    End Property

    Private m_background As String
    Public Property background() As String
        Get
            Return m_background
        End Get
        Set(ByVal value As String)
            m_background = value
        End Set
    End Property

    Private m_color As String
    Public Property color() As String
        Get
            Return m_color
        End Get
        Set(ByVal value As String)
            m_color = value
        End Set
    End Property
End Class
