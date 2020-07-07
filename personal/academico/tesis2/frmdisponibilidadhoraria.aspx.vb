
Partial Class SysTesisInv_frmdisponibilidadhoraria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtFechaInicio.Text = Date.Today
        Me.txtFechaFin.Text = DateAdd(DateInterval.Day, 90, Date.Today)
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

    End Sub

    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
        e.Cell.VerticalAlign = VerticalAlign.Top

        If e.Day.DayNumberText = "24" Then
            e.Cell.Controls.Add(New LiteralControl("<p>Asesoría de Tesis</p>"))
            e.Cell.BorderColor = Drawing.Color.Red
            e.Cell.BorderStyle = BorderStyle.Solid
            e.Cell.BorderWidth = 2
            e.Cell.ToolTip = "Asesoría en Oficina"
            e.Cell.Attributes.Add("onClick", "alert('**********ASESORÍA DE TESIS********** \nTema: Definir título de investigación \nLugar: Oficina\nHora: 03:00 p.m.');return(false)")
        End If
    End Sub
End Class
