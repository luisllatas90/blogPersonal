
Partial Class frmregsustentacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.txtFechaInicio.Text = Date.Now
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Response.Write("<script>window.opener.location.reload();window.close()</script>")
    End Sub
End Class
