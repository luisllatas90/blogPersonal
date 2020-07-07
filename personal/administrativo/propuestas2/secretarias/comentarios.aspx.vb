
Partial Class proponente_comentarios
    Inherits System.Web.UI.Page




    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("datospropuesta.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&id_rec=" & Request.QueryString("id_rec"))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblPropuesta.Text = Request.QueryString("nombre_prp")
    End Sub
End Class
