
Partial Class lstasesoriasprofesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("activa") = False Then
        'Response.Redirect("../tiempofinalizado.aspx")
        'End If
        If IsPostBack = False Then
            If Session("id") = "" Then
                Session("id") = request.querystring("id")
                Session("codigo_usu2") = Request.QueryString("id")
            End If
            Me.cmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('frmavancetesis.aspx?accion=A&codigo_tes=" + Request.QueryString("codigo_tes") + "','450','500');return(false)")

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            obj.Ejecutar("TES_ActualizarEstadoAvance", Session("id"), DBNull.Value)
            Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarAvanceTesis", 6, Session("id"), Request.QueryString("codigo_tes"), 0)
            Me.GridView1.DataBind()
            obj = Nothing
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        End If
    End Sub
End Class