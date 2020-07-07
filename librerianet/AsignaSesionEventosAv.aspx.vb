
Partial Class AsignaSesionEventosAv
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session.Clear()
            Session.Add("codigo_alu", Request.QueryString("y").ToString)
            Response.Redirect("academico/calendarioaulavirtual/eventosaulavirtual.aspx")
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class
