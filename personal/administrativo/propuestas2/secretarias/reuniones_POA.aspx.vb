
Partial Class secretarias_reuniones
    Inherits System.Web.UI.Page

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("id_Rec").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
        End If
    End Sub

    Protected Sub cmdPresentacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPresentacion.Click
        Response.Redirect("presentacion_intro_POA.aspx?id_rec=" & Me.txtelegido.Value)
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("registrarreunion_POA.aspx?instancia_rec=" & Me.ddlInstanciaRevision.SelectedValue & "&codigo_per=" & Request.QueryString("id"))
    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Response.Redirect("registrarreunion_POA.aspx?instancia_rec=" & Me.ddlInstanciaRevision.SelectedValue & "&id_rec=" & Me.txtelegido.Value & "&codigo_per=" & Request.QueryString("id"))
    End Sub
End Class
