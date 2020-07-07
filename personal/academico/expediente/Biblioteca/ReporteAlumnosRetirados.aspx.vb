
Partial Class Biblioteca_ReporteAlumnosRetirados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCnx As New ClsConectarDatos
            objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objCnx.AbrirConexion()
            ClsFunciones.LlenarListas(Me.cboCicloAcad, objCnx.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.cboCicloAcad.SelectedIndex = 1
            objCnx.CerrarConexion()
            cmdConsultar_Click(sender, e)
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        gvDatos.DataSource = objCnx.TraerDataTable("SOL_ConsultarRetitosYReservas", cboCicloAcad.SelectedItem.Text)
        objCnx.CerrarConexion()
        gvDatos.DataBind()
    End Sub
End Class
