
Partial Class proponente_comentarios
    Inherits System.Web.UI.Page

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("nuevocomentario.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))
    End Sub
End Class
