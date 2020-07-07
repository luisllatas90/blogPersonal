
Partial Class investigacion_frmTestAjax
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.lblMensaje.Text = "test AJAX"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
