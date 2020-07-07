
Partial Class academico_matricula_administrar_BloqueosMatricula
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objCnx As New ClsConectarDatos
        Dim codigo_alu, codigo_cac As Integer

        codigo_cac = Request.QueryString("cac")
        codigo_alu = Request.QueryString("alu")
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        Me.gvBloqueos.DataSource = objCnx.TraerDataTable("VerificarAccesoMatriculaEstudiante_V2", "ASE", codigo_alu, codigo_cac)
        objCnx.CerrarConexion()
        Me.gvBloqueos.DataBind()

    End Sub

 
    Protected Sub imgCerrar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgCerrar.Click
        Page.RegisterStartupScript("ok", "<script>self.parent.tb_remove();window.close();</script>")
        'window.parent.location.reload();
    End Sub

End Class
