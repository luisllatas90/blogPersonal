﻿
Partial Class frmanularcita
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Response.Write("<script>window.opener.location.reload();window.close()</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Label1.Text = Date.Now
    End Sub
End Class
