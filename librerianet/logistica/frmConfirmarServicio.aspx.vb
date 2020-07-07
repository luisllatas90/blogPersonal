
Partial Class logistica_frmConfirmarServicio
    Inherits System.Web.UI.Page


    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.gvCabOrden.DataSourceID = SqlDataSource5.ID
        Me.gvCabOrden.DataBind()
    End Sub

    Protected Sub gvCabOrden_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabOrden.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabOrden','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub


    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        If cboEstado.SelectedValue = "C" Then
            Me.cmdGuardar.Enabled = False

        Else
            Me.cmdGuardar.Enabled = True
        End If
        Me.txtObservacion.Text = Me.cboEstado.SelectedItem.Text
    End Sub

    Protected Sub rbtVeredicto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVeredicto.SelectedIndexChanged
        If rbtVeredicto.SelectedValue = "X" Then
            pnlDerivar.Visible = True
            cboPersonalDerivar.SelectedIndex = 0
        Else
            pnlDerivar.Visible = False
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim fun As New ClsFunciones
            Dim objLog As New ClsLogistica
            Dim datosDerivar As New Data.DataTable
            datosDerivar = objLog.ConsultarPersonalDerivacion()
            fun.CargarListas(Me.cboPersonalDerivar, datosDerivar, "codigo_Per", "personal")
        End If
    End Sub


    Protected Sub lnkVerRevisiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerRevisiones.Click
        Me.PnlDatosGenerales.Visible = False
        Me.pnlObservaciones.Visible = True
    End Sub

    Protected Sub lnkVerDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkVerDatos.Click
        Me.PnlDatosGenerales.Visible = True
        Me.pnlObservaciones.Visible = False
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objLog As New clsLogistica
        Dim codigo_rco As Int64
        Try
            codigo_rco = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0)
            If Me.rbtVeredicto.SelectedValue = "C" Then
                objLog.InsertarConfirmacionServicio("C", Request.QueryString("id"), Me.txtObservacion.Text, codigo_rco)
            ElseIf Me.rbtVeredicto.SelectedValue = "X" Then
                objLog.InsertarConfirmacionServicio(Me.rbtVeredicto.SelectedValue, Request.QueryString("id"), Me.txtObservacion.Text, codigo_rco)
                objLog.InsertarConfirmacionServicio(Me.rbtVeredicto.SelectedValue, Me.cboPersonalDerivar.SelectedValue, "", codigo_rco)
            End If
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se confirmó la orden de servicio satisfactoriamente');", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al procesar los datos');", True)
        End Try
    End Sub

    Protected Sub gvDetalleCompra_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalleCompra.SelectedIndexChanged
        Dim objLog As New clsLogistica
        Dim datos As New Data.DataTable
        Dim codigo_rco As Int64
        Try
            codigo_rco = gvCabOrden.DataKeys.Item(gvCabOrden.SelectedIndex).Values(0)
            datos = objLog.ConsultarPedidosPorOrdenCompra(codigo_rco)
            gvPedidos.DataSource = datos
            gvPedidos.DataBind()
        Catch ex As Exception
            objLog = Nothing
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al procesar los datos');", True)
        End Try
    End Sub

    Protected Sub gvCabOrden_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabOrden.SelectedIndexChanged
        Me.gvPedidos.DataSource = Nothing
        Me.gvPedidos.DataBind()
    End Sub
End Class
