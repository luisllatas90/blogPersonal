Imports System.Web.UI



Partial Class academico_cargalectiva_consultapublica_estimarvacantes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim objFun As New ClsFunciones
        objFun.CargarListas(Me.cboEscProf, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", "2", 1, 684), "codigo_cpf", "nombre_cpf")
        obj.CerrarConexion()
        obj = Nothing
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "selectPlanEstudio", "selectPlanEstudio();", True)
    End Sub


End Class
