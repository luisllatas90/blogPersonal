
Partial Class separarcita
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
        e.Cell.VerticalAlign = VerticalAlign.Top

        If e.Day.DayNumberText = "24" Then
            e.Cell.Controls.Add(New LiteralControl("<p>Asesoría de Tesis</p>"))
            e.Cell.BorderColor = Drawing.Color.Red
            e.Cell.BorderStyle = BorderStyle.Solid
            e.Cell.BorderWidth = 2
            e.Cell.ToolTip = "Asesoría en Oficina"
            e.Cell.Attributes.Add("onClick", "AbrirCita();return(false)")
        End If
    End Sub
End Class
