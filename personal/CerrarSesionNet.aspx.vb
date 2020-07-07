
Partial Class CerrarSesionNet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        Session.Abandon()
        Response.Redirect("..\librerianet\CerrarSesionNet.aspx")
    End Sub
End Class
