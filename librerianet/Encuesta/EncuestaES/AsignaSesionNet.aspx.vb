
Partial Class AsignaSesionNet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        If (Request.QueryString("x") IsNot Nothing And Request.QueryString("y") IsNot Nothing And Request.QueryString("z") IsNot Nothing) Then
            Session.Add("enc_tipo", Request.QueryString("x"))
            Session.Add("enc_usu", Request.QueryString("y"))
            Response.Redirect(Request.QueryString("z"))
        End If
    End Sub
End Class
