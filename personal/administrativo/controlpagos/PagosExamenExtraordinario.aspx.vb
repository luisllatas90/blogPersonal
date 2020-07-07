
Partial Class administrativo_controlpagos_PagosExamenExtraordinario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCnx As New ClsConectarDatos
            Dim objFun As New ClsFunciones
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            objFun.CargarListas(cboCiclo, objCnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_Cac", "descripcion_Cac")
            objCnx.CerrarConexion()
        End If
    End Sub

    Protected Sub cboCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCiclo.SelectedIndexChanged
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        gvwPagos.DataSource = objCnx.TraerDataTable("CAJ_ConsultarPagosExamenExtraordinario", Me.cboCiclo.SelectedValue)
        gvwPagos.DataBind()
        objCnx.CerrarConexion()
    End Sub
End Class
