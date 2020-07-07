
Partial Class administrativo_propuestas2_proponente_seguimientoAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("id") Is Nothing) Then
            Response.Redirect("../../../ErrorSistema.aspx")            
        Else
            Me.HdUsuario.Value = Request.QueryString("id")
        End If
    End Sub

    Protected Sub dgvPropuestas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgvPropuestas.PageIndexChanging
        dgvPropuestas.PageIndex = e.NewPageIndex
        Buscar()
        'dgvPropuestas.SelectedIndex = e.NewPageIndex
    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
        End If
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged
        Me.txtelegido.Value = ""
    End Sub

    Protected Sub BtnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscar.Click
        Buscar()
    End Sub

    Private Sub Buscar()
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = ObjCnx.TraerDataTable("PRP_ConsultarPropuestaAprobada", Me.txtPersonal.Text, Me.ddlInstanciaPropuesta.SelectedValue)
            Me.dgvPropuestas.DataSource = dt
            Me.dgvPropuestas.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged
        
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles dgvPropuestas.SelectedIndexChanging
        'dgvPropuestas.PageIndex = e.NewSelectedIndex
        'dgvPropuestas.DataBind()
    End Sub
End Class
