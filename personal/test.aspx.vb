
Partial Class demo_test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Write("Usuario: " & User.Identity.Name)
        Response.Write("<br/>Aut: " & User.Identity.AuthenticationType)
        Response.Write("<br/>Dominio: " & HttpContext.Current.Request.Url.Host)

        Dim cls As New clsAccesoAD
        Response.Write("<br/>Cambio de clave: " & cls.CambiarClave("csenmache", "123456789Cs", "C@rmen1959"))
    End Sub
End Class
