
Partial Class medicina_alumno
    Inherits System.Web.UI.UserControl

    Public Property Numero() As String
        Get
            Return Me.LblNum.Text
        End Get
        Set(ByVal value As String)
            Me.LblNum.Text = value
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
            Return Me.ChkAsistio.Checked
        End Get
        Set(ByVal value As Boolean)
            Me.ChkAsistio.Checked = value
        End Set
    End Property

    Public Property Ingreso_Hora() As String
        Get
            Return Me.TxtInicioHora.Text
        End Get
        Set(ByVal value As String)
            Me.TxtInicioHora.Text = value
        End Set
    End Property

    Public Property Ingreso_Min() As String
        Get
            Return Me.TxtInicioMin.Text
        End Get
        Set(ByVal value As String)
            Me.TxtInicioMin.Text = value
        End Set
    End Property

    Public Property Salida_Hora() As String
        Get
            Return Me.TxtFinalHora.Text
        End Get
        Set(ByVal value As String)
            Me.TxtFinalHora.Text = value
        End Set
    End Property

    Public Property Salida_min() As String
        Get
            Return Me.TxtFinalMin.Text
        End Get
        Set(ByVal value As String)
            Me.TxtFinalMin.Text = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ChkAsistio.Attributes.Add("OnClick", "habilita(this)")
        Me.TxtInicioHora.Attributes.Add("OnKeyPress", "numeros();")
        Me.TxtInicioMin.Attributes.Add("OnKeyPress", "numeros();")
        Me.TxtFinalHora.Attributes.Add("OnKeyPress", "numeros();")
        Me.TxtFinalMin.Attributes.Add("OnKeyPress", "numeros();")
        Me.TxtInicioHora.Enabled = False
        Me.TxtInicioMin.Enabled = False
        Me.TxtFinalHora.Enabled = False
        Me.TxtFinalMin.Enabled = False
        Me.TxtObservaciones.Enabled = False
    End Sub
End Class
