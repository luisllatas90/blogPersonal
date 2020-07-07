
Partial Class vstavancetesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarAsesorias()
        End If
    End Sub
    Private Sub CargarAsesorias()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable
        Dim codigo_tes As Integer
        codigo_tes = Request.QueryString("codigo_tes")

        Me.DataList1.Visible = True
        Me.DataList1.DataSource = obj.TraerDataTable("TES_ConsultarAvanceTesis", 1, codigo_tes, 0, 0)
        Me.DataList1.DataBind()
        obj = Nothing

        Me.lbltitulo.Text = "Registros de Asesoría de Tesis (" & Me.DataList1.Items.Count & ")"
    End Sub
    Protected Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound

        CType(e.Item.FindControl("lblNro"), Label).Text = e.Item.ItemIndex + 1

    End Sub
End Class
