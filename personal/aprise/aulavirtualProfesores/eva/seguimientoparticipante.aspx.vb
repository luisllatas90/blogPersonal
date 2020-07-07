
Partial Class librerianet_aulavirtual_eva_seguimientoparticipante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            Session("codigo_usu2") = Request.QueryString("idusuario")
            Session("idvisita2") = Request.QueryString("idvisita")
            Session("idcursovirtual2") = Request.QueryString("idcursovirtual")

            Me.GridView1.DataSource = obj.TraerDataTable("ConsultarCursoVirtual", "16", Session("idcursovirtual2"), "", "")
            Me.GridView1.DataBind()
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("idusuario").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "VerFichaParticipante(this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
