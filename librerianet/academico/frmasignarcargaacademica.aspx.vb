
Partial Class academico_frmasignarcargaacademica
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_per As Integer
            Dim codigo_tfu As Int16
            codigo_per = 471
            codigo_tfu = 1

            ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            ClsFunciones.LlenarListas(Me.dpDpto, obj.TraerDataTable("ConsultarCentroCosto", "AC", codigo_per), "codigo_dac", "descripcion_cco", IIf(codigo_tfu = 1, "--TODOS--", ""))
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
       
    End Sub

    Protected Sub dpDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpDpto.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim RS As Data.DataTable
        RS = obj.TraerDataTable("ConsultarCargaAcademicaDpto", 1, Me.dpDpto.SelectedValue, Me.dpCiclo.SelectedValue, 0)
        ClsFunciones.LlenarListas(Me.dpCurso, RS, "codigo_cur", "nombre_cur")
    End Sub
End Class
