
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Response.Write(Request.UrlReferrer)

        For index As Integer = 1 To Request.ServerVariables.Count

            Response.Write(Request.ServerVariables(index - 1) & "|")
        Next

    End Sub
End Class
