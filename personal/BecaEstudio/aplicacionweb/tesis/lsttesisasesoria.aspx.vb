
Partial Class lsttesisasesoria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 0, 0, 0, 0), "codigo_Eti", "nombre_Eti")

            BuscarTesisRegistradas()

        End If
    End Sub
    Private Sub BuscarTesisRegistradas()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis", 3, Me.dpFase.SelectedValue, 1, Request.QueryString("id"))
        Me.GridView1.DataBind()
        obj = Nothing
        Session("codigo_usu2") = Request.QueryString("id")
        Session("codigo_tfu2") = Request.QueryString("ctf")
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_tes").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub dpFase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpFase.SelectedIndexChanged
        BuscarTesisRegistradas()
    End Sub

End Class
