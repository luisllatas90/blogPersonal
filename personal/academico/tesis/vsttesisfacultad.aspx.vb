
Partial Class vsttesisfacultad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        If IsPostBack = False Then

            ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 0, 0, 0, 0), "codigo_Eti", "nombre_Eti", "--TODAS-")

        End If
        Me.DataList1.DataSource = obj.TraerDataTable("ConsultarFacultad", "TO", "")
        Me.DataList1.DataBind()
        obj = Nothing
    End Sub
    Protected Sub DataList1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles DataList1.ItemDataBound
        Dim gr As GridView
        gr = CType(e.Item.FindControl("gridView1"), GridView)

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        gr.DataSource = obj.TraerDataTable("TES_ConsultarTesis", "5", Me.DataList1.DataKeys(e.Item.ItemIndex), Me.dpFase.SelectedValue, 0)
        gr.DataBind()
        obj = Nothing
    End Sub
End Class
