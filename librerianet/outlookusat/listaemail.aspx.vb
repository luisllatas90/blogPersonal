
Partial Class librerianet_outlookusat_listaemail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable = obj.TraerDataTable("ConsultarCicloAcademico", "CV", 1)

            Me.lblCiclo.Text = tbl.Rows(0).Item("descripcion_cac")
            Me.hdCodigo_cac.Value = tbl.Rows(0).Item("codigo_cac")
            tbl.Dispose()
            obj = Nothing
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
