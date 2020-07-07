
Partial Class presupuesto_areas_RegistrarPresupuesto
    Inherits System.Web.UI.Page

 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objPre = New ClsPresupuesto
            objFun = New ClsFunciones
            
            If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 6 Then
                objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("TO", ""), "codigo_cco", "descripcion_cco")
            Else
                objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("CP", Request.QueryString("id")), "codigo_cco", "descripcion_cco")
            End If

            If Request.QueryString("cco") <> "" Then
                cboCentroCostos.SelectedValue = Request.QueryString("cco")
            End If

            cmdConsultar_Click(sender, e)
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        Dim datos As New Data.DataTable
        datos = objPre.ObtenerListaPresupuesto(Me.cboCentroCostos.SelectedValue)
        If datos.Rows.Count > 0 Then
            gvPresupuesto.DataSource = datos
        Else
            gvPresupuesto.DataSource = Nothing
        End If
        gvPresupuesto.DataBind()
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("agregarpresupuesto.aspx?cecos=" & cboCentroCostos.SelectedItem.Text & "&cco=" & _
                          cboCentroCostos.SelectedValue & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub gvPresupuesto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPresupuesto.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            ' e.Row.Cells(8).Text = "<div style='cursor:hand' onClick=location.href='VerPresupuesto.aspx?field=" & fila.Row.Item("codigo_Pto") & "'><img src='../../images/presupuesto/busca_small.gif'></div>"
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        End If
    End Sub

    Protected Sub gvPresupuesto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPresupuesto.SelectedIndexChanged
        Response.Redirect("VerPresupuesto.aspx?field=" & gvPresupuesto.SelectedValue & "&id=" & Request.QueryString("id"))
    End Sub
End Class
