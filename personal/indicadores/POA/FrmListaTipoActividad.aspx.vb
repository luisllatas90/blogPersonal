
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Response.Redirect("FrmMantenimientoTipoActividad.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub
End Class
