
Partial Class academico_cargalectiva_rpteprofesorcursoshdo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

        
            Dim objFun As New ClsFunciones
            objFun.CargarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            objFun.CargarListas(Me.dpCodigo_dac, obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 0, codigo_usu, 0, 0, 0), "codigo_dac", "nombre_dac")
      
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.dataCursos.DataSource = Nothing
        Dim dtDatos As New Data.DataTable

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDatos = obj.TraerDataTable("ConsultarCursoProgramadoHDO", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_dac.SelectedValue)
        obj.CerrarConexion()
        obj = Nothing

        'Response.Write(dtDatos.Rows.Count)

        If dtDatos.Rows.Count Then
            Me.dataCursos.DataSource = dtDatos
        Else
            Me.dataCursos.DataSource = Nothing
        End If

        Me.dataCursos.DataBind()

    End Sub
End Class
