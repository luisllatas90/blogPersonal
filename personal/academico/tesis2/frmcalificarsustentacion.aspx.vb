
Partial Class frmcalificarsustentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.Label2.Text = Date.Now
            Me.Label1.Text = "001"
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Response.Write("<script>window.opener.location.reload();window.close()</script>")

    End Sub
End Class
