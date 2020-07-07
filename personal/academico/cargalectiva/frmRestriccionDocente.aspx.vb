
Partial Class academico_horarios_administrar_frmRestriccionAmbiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlDedicacion, obj.TraerDataTable("H_DedidacionConsultar"), "codigo_ded", "descripcion_ded")
            ClsFunciones.LlenarListas(Me.ddlDepartamento, obj.TraerDataTable("ACAD_DepartamentoPersonalFuncion", "", codigo_usu, codigo_tfu), "codigo_Dac", "descripcion_dac")
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("ListaCicloAcademico"), "codigo_cac", "descripcion_cac")

            obj.CerrarConexion()
        End If

    End Sub

   
  
End Class
