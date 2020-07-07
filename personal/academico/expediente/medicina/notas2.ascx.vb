
Partial Class medicina_notas2
    Inherits System.Web.UI.UserControl
    Public Property Numero() As String
        Get
            Return Me.LblNum.Text
        End Get
        Set(ByVal value As String)
            Me.LblNum.Text = value
        End Set
    End Property

    Public Property CodigoDMA() As Integer
        Get
            Return Me.HidenCodAlu.Value
        End Get
        Set(ByVal value As Integer)
            Me.HidenCodAlu.Value = value
        End Set
    End Property

    Public Property Nombres() As String
        Get
            Return Me.LblAlumno.Text
        End Get
        Set(ByVal value As String)
            Me.LblAlumno.Text = value
        End Set
    End Property

    Public WriteOnly Property ToolNOtas() As String
        Set(ByVal value As String)
            Me.TxtNota.ToolTip = value
        End Set
    End Property

    Public WriteOnly Property ToolNotas2() As String
        Set(ByVal value As String)
            Me.TxtNota2.ToolTip = value
        End Set
    End Property

    Public Property Notas() As String
        Get
            Return Me.TxtNota.Text
        End Get
        Set(ByVal value As String)
            Me.TxtNota.Text = value
        End Set
    End Property

    Public Property Notas2() As String
        Get
            Return Me.TxtNota2.Text
        End Get
        Set(ByVal value As String)
            Me.TxtNota2.Text = value
        End Set
    End Property

    Public Property observaciones() As String
        Get
            Return Me.TxtObservaciones.Text
        End Get
        Set(ByVal value As String)
            Me.TxtObservaciones.Text = value
        End Set
    End Property

    Public Property ColorFila() As String
        Get
            Return Me.fila.BgColor
        End Get
        Set(ByVal value As String)
            Me.fila.BgColor = value
        End Set
    End Property

    Public Property ActivaControles() As Boolean
        Get
            Return Me.TxtNota.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.TxtNota.Enabled = value
            Me.TxtNota2.Enabled = value
            Me.TxtObservaciones.Enabled = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TxtNota.Attributes.Add("OnKeyPress", "numeros()")
        Me.TxtObservaciones.Enabled = False
    End Sub
End Class
