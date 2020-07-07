
Partial Class buscarestudiante
    Inherits System.Web.UI.Page

    Protected Sub cmdAnadir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAnadir.Click
        Dim script As String

        script = "<script>window.close();window.opener.ElegirAsesor('" & Me.ListBox1.SelectedItem.Text & "')</script>"
        'codigo = "<script>window.close();window.opener.elegir('" & Me.GridCliente.SelectedRow.Cells(0).Text & "','" & GridCliente.SelectedRow.Cells(1).Text & "')</script>"
        Page.RegisterStartupScript("refrescarAutor", script)
    End Sub
End Class
