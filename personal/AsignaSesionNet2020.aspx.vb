
Partial Class academico_AsignaSesionNet
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.RemoveAll()
        If (Request.QueryString("per") IsNot Nothing) Then            
            Session.Add("id_per", Request.QueryString("per"))
            Session.Add("perlogin", Request.QueryString("perlogin"))
            Session.Add("nombreper", Request.QueryString("nombre"))



            Response.Redirect("../libreriaNet/AsignaSesionesPer2020.aspx?per=" & Request.QueryString("per") & "&t=" & Now.Millisecond & "&log=" & Request.QueryString("perlogin"))
            'Response.Redirect("listaaplicaciones.asp?sw=1")        
        End If
        Response.Redirect("../")
    End Sub
End Class
