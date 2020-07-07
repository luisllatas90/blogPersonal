
Partial Class Egresados_frmOfertaLaboralV2
    Inherits System.Web.UI.Page
    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        Response.Redirect("frmOfertasLaborales.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
End Class
