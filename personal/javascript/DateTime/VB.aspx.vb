
Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim dob As DateTime = DateTime.Parse(Request.Form(TextBox1.UniqueID))
    End Sub
End Class
