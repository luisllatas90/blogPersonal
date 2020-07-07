
Partial Class medicina_ingreso
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

    Public Property ActivarIngreso() As Boolean
        Get
            Return Me.ChkAsistio.Enabled
        End Get
        Set(ByVal value As Boolean)
            Me.TxtInicioHora.Enabled = value
            Me.TxtInicioMin.Enabled = value
            Me.ChkAsistio.Enabled = value
            'Me.TxtObservaciones.Enabled = value
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
        Me.TxtInicioHora.Enabled = False
        Me.TxtInicioMin.Enabled = False

    End Sub
    '--------Agregado el dia 13/06/08 - Hreyes----------
    Public Property CheckVisible() As Boolean
        Get
            Return Me.ChkAsistio.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.ChkAsistio.Visible = value
        End Set
    End Property

    Public Property AsistenciaVisible() As Boolean
        Get
            Return Me.LblAsistencia.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.LblAsistencia.Visible = value
        End Set
    End Property

    Public Property Ingreso_Asistencia() As String
        Get
            Return Me.LblAsistencia.Text
        End Get
        Set(ByVal value As String)
            Me.LblAsistencia.Text = value
            Me.LblAsistencia.Font.Bold = True
        End Set
    End Property

    Public Property Color_Asistencia() As Drawing.Color
        Get
            Return Me.LblAsistencia.ForeColor
        End Get
        Set(ByVal value As Drawing.Color)
            Me.LblAsistencia.ForeColor = value
        End Set
    End Property
    '-------------------------------------------------------
End Class
