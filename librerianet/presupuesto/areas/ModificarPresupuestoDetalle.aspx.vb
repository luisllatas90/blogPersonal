
Partial Class presupuesto_areas_ModificarPresupuestoDetalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            'Cargar datos de Proceso presupuestal
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            'Cargar datos de unidad presuspuestal
            datos = objpre.ObtenerListaCentroCostos("H", 523)
            objfun.CargarListas(cboUnidadPresu, datos, "codigo_cco", "descripcion_cco", "Todos")
            datos.Dispose()

            cboAreaPresu.Items.Add("Todos")
            cboAreaPresu.Items.Item(0).Value = -1
        End If
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAreaPresu.SelectedIndexChanged
        Dim fila As Data.DataRow = Session("datosCecos").Rows.Find(cboAreaPresu.SelectedValue)
        txtCecos.Text = fila.Item(1).ToString
    End Sub

    Protected Sub cboUnidadPresu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUnidadPresu.SelectedIndexChanged
        Dim objpre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable

        'Cargar Datos Centro Costos = Area presupuestal
        Session("datosCecos") = objpre.ObtenerListaCentroCostos("H", cboUnidadPresu.SelectedValue)
        objfun.CargarListas(cboAreaPresu, Session("datosCecos"), "codigo_Cco", "descripcion_Cco", "Todos")
        Dim keys(1) As Data.DataColumn
        keys(0) = Session("datosCecos").Columns("codigo_Cco")
        Session("datosCecos").PrimaryKey = keys
        Dim fila As Data.DataRow = Session("datosCecos").Rows.Find(cboAreaPresu.SelectedValue)
        txtCecos.Text = fila.Item(1).ToString
        datos.Dispose()
    End Sub

    Protected Sub gvCabecera_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabecera.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvCabecera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabecera.SelectedIndexChanged
        hddCodigo_pto.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        hddCodigo_ppr.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        hddHabilitado.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(2)
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'For i As Int16 = 7 To 21
            '    e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
            '    e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
            'Next

            Dim ctrlEliminar As ImageButton = CType(e.Row.Cells(28).Controls(0), ImageButton)
            Dim ctrlEditar As ImageButton = CType(e.Row.Cells(27).Controls(0), ImageButton)
            Dim ctrl As ImageButton = CType(e.Row.Cells(29).Controls(1), ImageButton)
            e.Row.Cells(27).Enabled = hddHabilitado.Value
            e.Row.Cells(28).Enabled = hddHabilitado.Value
            e.Row.Cells(29).Enabled = hddHabilitado.Value

            If hddHabilitado.Value = True Then
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar.gif"
                e.Row.Cells(27).Visible = True
                e.Row.Cells(28).Visible = False
                e.Row.Cells(29).Visible = True
            Else
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar_d.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                ctrl.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                e.Row.Cells(27).Visible = True
                e.Row.Cells(28).Visible = True
                e.Row.Cells(29).Visible = False
            End If
        End If
    End Sub

    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting
        EliminarItem(gvDetalle.DataKeys.Item(e.RowIndex).Value, True)
        gvCabecera_SelectedIndexChanged(sender, e)
        e.Cancel = True
    End Sub

   
    Private Sub EliminarItem(ByRef codigo_dpr As Int64, ByVal mostrarmsj As Boolean)
        Dim objPre As ClsPresupuesto
        Dim rpta As String
        objPre = New ClsPresupuesto
        rpta = objPre.EliminarDetallePresupuesto(codigo_dpr, Request.QueryString("id"))
        If mostrarmsj = True Then
            If rpta = "0" Then
                ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo eliminar, debido a que el proceso no está habilitado');", True)
            ElseIf rpta = "1" Then
                ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('Se eliminaron correctamente los datos');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('" & rpta & "');", True)
            End If
        End If
        objPre = Nothing
    End Sub

    Protected Sub gvDetalle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetalle.RowEditing

        Response.Redirect("RegistrarPresupuestoDetalle.aspx?field=" & gvDetalle.DataKeys.Item(e.NewEditIndex).Value & "&tipo=E&id=" & Request.QueryString("id"))
        e.Cancel = False
    End Sub


    Protected Sub gvDetalle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalle.SelectedIndexChanged

    End Sub
End Class
