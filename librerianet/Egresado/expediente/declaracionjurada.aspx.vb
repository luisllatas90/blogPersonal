
Partial Class Egresado_expediente_declaracionjurada
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblnombre.Text = Session("nombreEgresado")
    End Sub

    Protected Sub btnAcepta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAcepta.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_InsertaBitacoraUpdateDatos", CInt(Session("codigo_pso")), "DJ")
        obj.CerrarConexion()
        obj = Nothing
        'Response.Redirect("ExperienciaLaboralRegistrar.aspx")
        ClientScript.RegisterStartupScript(Me.GetType, "atras", "javascript:window.history.back();", True)
    End Sub

    Protected Sub btnNoAcepta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNoAcepta.Click
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
        ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
    End Sub
End Class
