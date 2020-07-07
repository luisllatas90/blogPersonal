
Partial Class DirectorInvestigacion_administrarunidadesinvestigacion
    Inherits System.Web.UI.Page

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CmdAgregar.Attributes.Add("OnClick", "AbrirPopUp('frmagregarunidadinvestigacion.aspx','325','650'); return false;")
    End Sub

    Protected Sub GridView1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting
        Dim ObjInv As New Investigacion
        ObjInv.ModificarUnidadInvestigacion(1, e.Keys.Item(0))
        ObjInv = Nothing
        e.Cancel = True
        Me.GridView1.DataBind()
    End Sub

    
End Class
