
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Response.Redirect("FrmListaTipoActividad.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmListaTipoActividad.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
End Class
