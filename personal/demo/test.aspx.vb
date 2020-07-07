
Partial Class demo_test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write("Usuario: " & User.Identity.Name)
        Response.Write("Aut: " & User.Identity.AuthenticationType)

    End Sub
End Class
