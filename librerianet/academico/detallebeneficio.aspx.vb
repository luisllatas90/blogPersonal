
Partial Class librerianet_academico_detallebeneficio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tbl As Data.DataTable

        tbl = obj.TraerDataTable("ConsultarPonderado", "BE", Request.QueryString("codigo_alu"), 0, 0)

        If tbl.Rows.Count > 0 Then
            Me.lblCodigo.Text = tbl.Rows(0).Item("codigouniver_alu")
            Me.lblapellidos.Text = tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = tbl.Rows(0).Item("nombre_cpf")

            Me.GridView1.DataSource = tbl
            Me.GridView1.DataBind()
        End If
        obj = Nothing
    End Sub
End Class
