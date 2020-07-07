
Partial Class buscarestudiante
    Inherits System.Web.UI.Page

    Protected Sub cmdAnadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnadir.Click
        Dim script As String

        script = "<script>window.close();window.opener.ElegirAutor('" & Me.ListBox1.SelectedItem.Text & "')</script>"
        Page.RegisterStartupScript("refrescarAutor", script)
    End Sub
End Class
