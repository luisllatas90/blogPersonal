
Partial Class frmboleta
    Inherits System.Web.UI.Page

    Protected Sub cmdcerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcerrar.Click
        Response.Write("window.close()")
    End Sub
End Class
