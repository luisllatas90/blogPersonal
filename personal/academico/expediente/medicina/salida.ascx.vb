
Partial Class medicina_salidas
    Inherits System.Web.UI.UserControl

    Public Property CodigoDMA() As Integer
        Get
            Return Me.HidenCodAlu.Value
        End Get
        Set(ByVal value As Integer)
            Me.HidenCodAlu.Value = value
        End Set
    End Property


    Public Property Numero() As String
        Get
            Return Me.LblNum.Text
        End Get
        Set(ByVal value As String)
            Me.LblNum.Text = value
        End Set
    End Property

    Public Property Condicion() As String
        Get
            Return Me.LblCondicion.Text
        End Get
        Set(ByVal value As String)
            Me.LblCondicion.Text = value
        End Set
    End Property

    Public WriteOnly Property CondicionColor() As Drawing.Color
        Set(ByVal value As Drawing.Color)
            Me.LblCondicion.ForeColor = value
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

    Public Property Asistio() As Boolean
        Get
            Return Me.TxtObservaciones.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.TxtObservaciones.Enabled = value
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



    Public Property HoraIngreso() As String
        Get
            Return Me.LblIngreso.Text
        End Get
        Set(ByVal value As String)
            Me.LblIngreso.Text = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.ChkAsistio.Attributes.Add("OnClick", "habilita(this)")
        'Me.TxtFinalHora.Attributes.Add("OnKeyPress", "numeros();")
        'Me.TxtFinalMin.Attributes.Add("OnKeyPress", "numeros();")
        'Me.TxtObservaciones.Enabled = False
    End Sub
End Class
