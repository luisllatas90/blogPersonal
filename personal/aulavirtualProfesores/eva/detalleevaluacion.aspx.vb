
Partial Class personal_aulavirtual_lebir_eva_detalleevaluacion
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            Dim idcursovirtual As Integer = Request.QueryString("idcursovirtual")
            Dim idusuario As String = Request.QueryString("idusuario")

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            Dim datos As Data.DataTable

            datos = obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 5, idcursovirtual, idusuario, "")
            If datos.Rows.Count > 0 Then
                Me.GridView1.DataSource = datos
                Me.GridView1.DataBind()
            Else
                Label1.Visible = True
                Label1.Text = "El profesor aún no ha registrado su evaluación"
            End If
            datos = Nothing
            obj = Nothing
        End If
    End Sub
End Class
