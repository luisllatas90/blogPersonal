
Partial Class lstasesoriasestudiante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("activa") = False Then
        'Response.Redirect("../tiempofinalizado.aspx")
        'End If
        If IsPostBack = False Then
            If Session("id") = "" Then
                Session("id") = Request.QueryString("id")
            End If

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            obj.Ejecutar("TES_ActualizarEstadoAvance", DBNull.Value, Session("id"))
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