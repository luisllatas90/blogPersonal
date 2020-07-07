
Partial Class presupuesto_areas_RegistrarPresupuestoDetalle
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim objlog As New ClsLogistica
            Dim datos As New Data.DataTable
            'Cargar datos de usuario
            datos = objpre.ObtenerDatosUsuario(Request.QueryString("id"))
            If datos.Rows.Count > 0 Then
                lblUsuario.Text = datos.Rows(0).Item("usuario")
                lblCargo.Text = datos.Rows(0).Item("Cargo")
                lblCentroCostos.Text = datos.Rows(0).Item("CentroCostos")
            End If
            datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If
            datos.Dispose()
            'Cargar datos de programa presuspuestal
            objfun.CargarListas(cboProgramaPresu, objpre.ObtenerListaProgramaPresupuestal(), "codigo_ppr", "descripcion_ppr", ">> Seleccione <<")
            objfun.CargarListas(Me.cboEstado, objlog.ConsultarEstados("PE"), "codigo_Eped", "descripcionEstado_Eped")
            If Request.QueryString("Tipo") = "E" Then
                BloquearCabecera(False)
            Else
                BloquearCabecera(True)
            End If
            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
            txtCantidad.Attributes.Add("onKeyPress", "validarnumero()")
            Panel1.Visible = False
            Panel3.Visible = False
            MostrarBusquedaCeCos(False)
            Me.TxtFechaEsperada.Text = FormatDateTime(Now, DateFormat.ShortDate)
        End If
    End Sub

    Private Sub BloquearCabecera(ByVal valor As Boolean)
        Me.cboCecos.Enabled = valor
        Me.txtCecos.Enabled = valor
    End Sub

    Private Sub AgregarFilaGrid(ByRef tabla As Data.DataTable, ByVal texto As String)
        Dim fila As Data.DataRow
        fila = tabla.NewRow()
        fila("cantidad") = texto
        tabla.Rows.Add(fila)
    End Sub

    Protected Sub ImgBuscarItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarItems.Click
        BuscarItems()
        Me.txtDetPresup.Text = ""
        pnlPresupuesto.Visible = False
    End Sub

    Private Sub BuscarItems()
        Dim objPre As ClsLogistica
        objPre = New ClsLogistica
        gvItems.DataSource = objPre.ConsultarConceptos(rblMovimiento.SelectedValue, -1, -1, -1, -1, txtConcepto.Text)
        gvItems.DataBind()
        objPre = Nothing
        Panel1.Visible = True
    End Sub

    Protected Sub gvItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvItems.SelectedIndexChanged
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(1).Text)
        Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(3).Text)
        SeleccionarItem()
    End Sub
    Private Sub SeleccionarItem()
        If Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3) = True Then
            lblValores.Text = "Cantidad"
            lblTexto.Text = "Precio Unitario (S/.)"
        Else
            lblValores.Text = "Importe"
            lblTexto.Text = "Cantidad"
        End If
        gvItems.Dispose()
        Panel1.Visible = False
    End Sub
    Private Sub limpiarDistribucion()
        lblTotalItem.Text = "0.00"
        lblDistribuidoItem.Text = "0.00"
        lblPorDistribuir.Text = "0.00"
        lblNombreItem.Text = ""
        Me.gvDistribucion.DataSource = Nothing
        gvDistribucion.DataBind()
        Me.txtCodigo_Ecc.Text = ""
        Me.txtCantidadDistribucion.Text = ""
    End Sub
    Private Function sumarDistribucion() As Double
        Dim i As Integer
        Dim suma As Double
        suma = 0
        For i = 0 To gvDistribucion.Rows.Count - 1
            suma = suma + gvDistribucion.Rows(i).Cells(1).Text
        Next
        sumarDistribucion = suma
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjPre As New ClsPresupuesto
        Dim tipo As String = ""
        Dim objLog As New ClsLogistica
        Dim datos As New Data.DataTable
        Dim codigo_per As Integer
        Dim codigo_cco As Integer
        Dim codDetPresup As Integer
        Try

            If Me.txtDetPresup.Text = "" Then
                codDetPresup = 0
            Else
                codDetPresup = Me.txtDetPresup.Text
            End If

	    if txtCodItem.Text.toString = "" or Me.txtConcepto.Text.tostring="" or Me.txtPrecioUnit.Text = "" then
		ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Por favor complete los datos correctamente. Haga clic en el [ícono de búsqueda] y luego selecciónelo.')", True)
		exit sub
	    End if


            objLog.AbrirTransaccionCnx()
            If txtPedido.Text = "" Then
                datos = ObjPre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    codigo_per = datos.Rows(0).Item("codigo_per")
                    codigo_cco = datos.Rows(0).Item("codigo_cco")
                End If
                'Registrar la cabecera del pedido sólo la primera vez
                Me.txtPedido.Text = objLog.AgregarPedido(codigo_per, Me.cboCecos.SelectedValue, 1, 0, CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue)
            End If
            ' Registrar el detalle del pedido
            objLog.AgregarDetallePedido(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, Me.cboProgramaPresu.SelectedValue, codDetPresup)
            objLog.CerrarTransaccionCnx()
            objLog = Nothing
            FinalizarIngresoDetalle()
            pnlDistribuir.Visible = False
            ConsultarDatosPresupuesto()
            If txtPedido.Text <> "" Then
                cmdEnviar.Visible = True
                Me.cboCecos.Enabled = False
                lnkBusquedaAvanzada.Enabled = False
            End If
        Catch ex As Exception
            objLog.CancelarTransaccionCnx()
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        End Try
    End Sub
    Private Sub CargarDetalle()
        Dim objLog As New ClsLogistica
        gvDetallePedido.DataSource = objLog.ConsultarDetalle(Me.txtPedido.Text)
        gvDetallePedido.DataBind()
        objLog = Nothing
    End Sub
    Private Sub FinalizarIngresoDetalle()
        Dim objLog As New ClsLogistica
        Dim totalDetalle As Double
        totalDetalle = 0
        LimpiarDetalle()
        CargarDetalle()
        totalDetalle = objLog.ConsultarTotalDetalle(Me.txtPedido.Text)
        If totalDetalle = 0 Then
            Me.lblTitLista.Visible = False
            Me.lblTitTotal.Visible = False
            Me.lblTotalDetalle.Visible = False
            Me.lblTotalDetalle.Text = "0.00"

        Else
            Me.lblTitLista.Visible = True
            Me.lblTitTotal.Visible = True
            Me.lblTotalDetalle.Visible = True
            Me.lblTotalDetalle.Text = FormatNumber(totalDetalle, 2)
        End If
        limpiarDistribucion()
        pnlDistribuir.Visible = False
        objLog = Nothing
    End Sub
    Private Sub EliminarItemDetalle(ByVal codigo_dpe As Integer)
        Dim objLog As New ClsLogistica
        objLog.ElminarItemDetalle(codigo_dpe)
        objLog = Nothing
        CargarDetalle()
        objLog = Nothing
        FinalizarIngresoDetalle()
    End Sub
    Private Sub EliminarDistribucionItemDetalle(ByVal codigo_ecc As Integer)
        Dim objLog As New ClsLogistica
        objLog.EliminarDistribucionItemDetalle(codigo_ecc)
        objLog = Nothing
        ConsultarDistribucion()
    End Sub
  
    Private Sub LimpiarDetalle()
        Me.cboProgramaPresu.SelectedIndex = -1
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        txtCantidad.Text = ""
        txtComentarioReq.Text = ""
        txtDetPresup.Text = ""
    End Sub


    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click, cmdCerrar.Click
        hddForzar.Value = 0
        ClientScript.RegisterStartupScript(Me.GetType, "Existente", "javascript:divConfirmar.style.visibility='hidden'", True)
    End Sub

    Protected Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        hddForzar.Value = 1
        cmdGuardar_Click(sender, e)
    End Sub

    Protected Sub rblMovimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblMovimiento.SelectedIndexChanged
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        txtComentarioReq.Text = ""

    End Sub

    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
        Panel3.Visible = (valor)
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
    End Sub

    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        EliminarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
    End Sub


    Protected Sub gvDetallePedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetallePedido.SelectedIndexChanged
        ConsultarDistribucion()
    End Sub
    Private Sub ConsultarDistribucion()
        Dim objLog As New ClsLogistica
        Dim objpre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
        objfun.CargarListas(cboCecosEjecucion, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
        datos.Dispose()
        limpiarDistribucion()
        txtCodigoDetalle.Text = gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(0)
        lblNombreItem.Text = HttpUtility.HtmlDecode(gvDetallePedido.SelectedRow.Cells(0).Text)
        Me.gvDistribucion.DataSource = objLog.ConsultarDetalleItem(gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(0))
        Me.gvDistribucion.DataBind()
        pnlDistribuir.Visible = True
        If gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values("modoDistribucion_Dpe") = "P" Then
            lblModoDistribucion.Text = "Porcentaje(%)"
            lblTotalItem.Text = "100.00%"
            lblDistribuidoItem.Text = FormatNumber(sumarDistribucion(), 2) & "%"
            lblPorDistribuir.Text = FormatNumber(100 - sumarDistribucion(), 2) & "%"
        Else
            lblModoDistribucion.Text = "Cantidad"
            lblTotalItem.Text = HttpUtility.HtmlDecode(gvDetallePedido.SelectedRow.Cells(3).Text)
            lblDistribuidoItem.Text = FormatNumber(sumarDistribucion(), 2)
            lblPorDistribuir.Text = FormatNumber(lblTotalItem.Text - sumarDistribucion(), 2)
        End If
        objLog = Nothing
    End Sub

    Protected Sub gvDistribucion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDistribucion.RowDeleting
        EliminarDistribucionItemDetalle(Me.gvDistribucion.DataKeys.Item(e.RowIndex).Values(0))
    End Sub

    Protected Sub gvDistribucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistribucion.SelectedIndexChanged
        Me.txtCantidadDistribucion.Text = HttpUtility.HtmlDecode(gvDistribucion.SelectedRow.Cells(1).Text)
        Me.cboCecosEjecucion.SelectedValue = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(1)
        Me.txtCodigo_Ecc.Text = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(0)
    End Sub


    Protected Sub cmdDistribuir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDistribuir.Click
        Dim objLog As New ClsLogistica
        Dim rpta As String
        Dim codigo_ecc As Integer
        If Me.txtCodigo_Ecc.Text = "" Then
            codigo_ecc = -1
        Else
            codigo_ecc = Me.txtCodigo_Ecc.Text
        End If
        objLog.AbrirTransaccionCnx()
        rpta = objLog.ActualizarDistribucionItem(codigo_ecc, Me.cboCecosEjecucion.SelectedValue, Me.txtCantidadDistribucion.Text, txtCodigoDetalle.Text)
        objLog.CerrarTransaccionCnx()
        If rpta <> "OK" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & rpta & "');", True)
        End If
        ConsultarDistribucion()
    End Sub

    Private Sub ConsultarPresupuesto()
        Dim objcon As ClsLogistica
        objcon = New ClsLogistica
        Dim DatosIng, datosEgr As New Data.DataTable
        datosEgr = objcon.ConsultarDetallePresupuesto(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, "E")
        gvPresupuesto.DataSource = datosEgr
        gvPresupuesto.DataBind()
        If datosEgr.Rows.Count = 0 Then
            pnlListaPresup.Visible = False
            pnlPresupuesto.Visible = False
        Else
            pnlListaPresup.Visible = True
            pnlPresupuesto.Visible = True
        End If
    End Sub

    Protected Sub gvPresupuesto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPresupuesto.SelectedIndexChanged
        If gvPresupuesto.SelectedRow.Cells(5).Text > 0 Then
            LimpiarDetalle()
            txtDetPresup.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(0)
            cboProgramaPresu.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(1)
            txtCodItem.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(2)
            Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(1).Text)
            Me.txtComentarioReq.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(2).Text)
            Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(3).Text)
            Me.txtCantidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
        End If

    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        ConsultarDatosPresupuesto()
    End Sub
    Private Sub ConsultarDatosPresupuesto()
        Me.pnlPresupuesto.Visible = True
        ConsultarPresupuesto()
        LimpiarDetalle()
        Me.txtDetPresup.Text = ""
        Panel1.Visible = False
    End Sub
    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        If gvDetallePedido.Rows.Count > 0 Then
            If Me.txtpedido.text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
                Exit Sub
            End If
            Dim log As New ClsLogistica
            log.CalificarPedido(txtPedido.Text, Request.QueryString("id"), "C", "")
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmNuevoPedido.aspx?id=" & Request.QueryString("id") & "';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El detalle del pedido no cuenta con Items Registrados');", True)
        End If
    End Sub
End Class
