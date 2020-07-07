﻿
Partial Class logistica_Default
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.cboInstancia.SelectedValue = 2

        If Not IsPostBack Then
            Dim log As New ClsLogistica
            Dim fun As New ClsFunciones
            fun.CargarListas(cboInstancia, log.ConsultarInstancias("TO"), "codigo_Ipl", "nombreInstancia_Ipl")
            fun.CargarListas(cboPersonalDerivar, log.ConsultarPersonalDerivacion(), "codigo_Per", "personal")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim objlog As New ClsLogistica
            Dim datos As New Data.DataTable
            'Cargar datos de usuario
            datos = objpre.ObtenerDatosUsuario(Request.QueryString("id"))
            'If datos.Rows.Count > 0 Then
            '    'lblUsuario.Text = datos.Rows(0).Item("usuario")
            '    'lblCargo.Text = datos.Rows(0).Item("Cargo")
            '    'lblCentroCostos.Text = datos.Rows(0).Item("CentroCostos")
            'End If
            datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione<<")
            objfun.CargarListas(Me.cboCentroCostos, datos, "codigo_Cco", "descripcion_Cco", "TODOS")
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If
            datos.Dispose()
            'Cargar datos de programa presuspuestal
            objfun.CargarListas(cboProgramaPresu, objpre.ObtenerListaProgramaPresupuestal(), "codigo_ppr", "descripcion_ppr", ">> Seleccione <<")
            objfun.CargarListas(Me.cboEstado, objlog.ConsultarEstados("PE"), "codigo_Eped", "descripcionEstado_Eped")
            'If Request.QueryString("Tipo") = "E" Then
            '    'BloquearCabecera(False)
            'Else
            '    'BloquearCabecera(True)
            'End If
            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
            txtCantidad.Attributes.Add("onKeyPress", "validarnumero()")
            Panel1.Visible = False
            Panel3.Visible = False
            'MostrarBusquedaCeCos(False)
            Me.TxtFechaEsperada.Text = FormatDateTime(Now, DateFormat.ShortDate)
        End If
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.cmdEnviar.Visible = True
        Else
            Me.cmdEnviar.Visible = False
        End If
        ConsultarPedidos()
    End Sub
    Protected Sub gvPedidos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPedidos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvPedidos','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub
    Protected Sub gvPedidos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPedidos.SelectedIndexChanged
        Dim log As New ClsLogistica
        Me.txtPedido.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(0).Text)
        Me.cboEstado.SelectedItem.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text)
        ConsultarDatosPedido(Me.txtPedido.Text)
        pnlDistribuir.Visible = False
        FinalizarIngresoDetalle()
        cmdCalificar.Enabled = True
        limpiarCalificacion()
        If log.ActivarDerivacion(Me.txtPedido.Text) = "S" Then
            rbOpciones.Items(3).Enabled = True
        Else
            rbOpciones.Items(3).Enabled = False
        End If
        If Me.cboInstancia.SelectedValue = 1 Then
            UpdatePanel1.Visible = True
            UpdatePanel2.Visible = True
            Me.cmdGuardar.Visible = True
        Else
            UpdatePanel1.Visible = False
            UpdatePanel2.Visible = False
            Me.cmdGuardar.Visible = False
        End If
    End Sub
    Protected Sub lnkDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatos.Click
        If Me.txtPedido.Text <> "" Then
            ConsultarDatosPedido(Me.txtPedido.Text)
        End If
    End Sub
    Private Sub CargarDetalle()
        Dim objLog As New ClsLogistica
        gvDetallePedido.DataSource = objLog.ConsultarDetalleEjecutado(Me.txtPedido.Text)
        gvDetallePedido.DataBind()
        ' INICIO - JR
        Dim datos As New Data.DataTable
        datos = objLog.ConsultarAlmacen(Me.txtPedido.Text)
        Me.txtAlmacen.Text = datos.rows(0).item("nombre_alm").tostring
        ' FIN - JR
        objLog = Nothing
    End Sub
    Private Sub ConsultarDatosPedido(ByVal codigo_ped As Integer)
        Me.pnlDatos.Visible = True
        Me.pnlRevision.Visible = False
        CargarDetalle()

    End Sub
    Private Sub ConsultarRevisionesPedido(ByVal codigo_ped As Integer)
        Dim objLog As New ClsLogistica
        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = True
        gvRevisiones.DataSource = objLog.ConsultarRevisiones(codigo_ped)
        gvEstados.DataSource = objLog.ConsultarEstadosPedido(codigo_ped)
        gvObservaciones.DataSource = objLog.ConsultarObservacionesPedido(codigo_ped)
        gvRevisiones.DataBind()
        gvEstados.DataBind()
        gvObservaciones.DataBind()
    End Sub

    Protected Sub lnkRevisiones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRevisiones.Click
        If Me.txtPedido.Text <> "" Then
            ConsultarRevisionesPedido(Me.txtPedido.Text)
        End If
    End Sub

    Protected Sub cboInstancia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInstancia.SelectedIndexChanged
        txtPedido.Text = ""
        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = False
    End Sub
    Private Sub FinalizarIngresoDetalle()
        Dim objLog As New ClsLogistica
        Dim totalDetalle As Double
        totalDetalle = 0
        LimpiarDetalle()
        CargarDetalle()
        totalDetalle = objLog.ConsultarTotalDetalle(Me.txtPedido.Text)
        If totalDetalle = 0 Then
            Me.lblTitTotal.Visible = False
            Me.lblTotalDetalle.Visible = False
            Me.lblTotalDetalle.Text = "0.00"
        Else
            Me.lblTitTotal.Visible = True
            Me.lblTotalDetalle.Visible = True
            Me.lblTotalDetalle.Text = FormatNumber(totalDetalle, 2)
        End If
        limpiarDistribucion()
        pnlDistribuir.Visible = False
        objLog = Nothing
    End Sub
    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        If Me.txtPedido.Text <> "" Then
            Dim log As New ClsLogistica
            log.CalificarPedido(Me.txtPedido.Text, Request.QueryString("id"), "C", "")
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosSolicitante.aspx?id=" & Request.QueryString("id") & "';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione un pedido de la lista para enviar');", True)
        End If
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


    Protected Sub gvDetallePedido_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetallePedido.RowDataBound
        If e.Row.Cells(8).Text = "Rechazado" Then
            e.Row.ForeColor = Drawing.Color.Red
            e.Row.Font.Strikeout = True
        End If
        If e.Row.Cells(8).Text = "Atendido" Then
            e.Row.ForeColor = Drawing.Color.Blue
        End If
        If e.Row.Cells(8).Text = "Por despachar" Or e.Row.Cells(8).Text.Substring(0, 5).ToString = "En al" Then
            e.Row.ForeColor = Drawing.Color.Green
        End If
        If e.Row.Cells(8).Text.Substring(0, 6).ToString = "Tesore" Then
            e.Row.ForeColor = Drawing.Color.Orange
        End If

    End Sub
    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        'EliminarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
        DenegarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
    End Sub
    Protected Sub gvDetallePedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetallePedido.SelectedIndexChanged
        ConsultarDistribucion()
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.lblCeCoDist.Visible = True
            Me.lblModoDistribucion.Visible = True
            Me.cboCecosEjecucion.Visible = True
            Me.txtCantidadDistribucion.Visible = True
            Me.cmdDistribuir.Visible = True
        Else
            Me.lblCeCoDist.Visible = False
            Me.lblModoDistribucion.Visible = False
            Me.cboCecosEjecucion.Visible = False
            Me.txtCantidadDistribucion.Visible = False
            Me.cmdDistribuir.Visible = False
        End If
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
    Private Sub EliminarItemDetalle(ByVal codigo_dpe As Integer)
        Dim objLog As New ClsLogistica
        objLog.ElminarItemDetalle(codigo_dpe)
        objLog = Nothing
        CargarDetalle()
        objLog = Nothing
        FinalizarIngresoDetalle()
    End Sub
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
            objLog.AbrirTransaccionCnx()
            If txtPedido.Text = "" Then
                datos = ObjPre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    codigo_per = datos.Rows(0).Item("codigo_per")
                    codigo_cco = datos.Rows(0).Item("codigo_cco")
                End If
                'Registrar la cabecera del pedido sólo la primera vez
                Me.txtPedido.Text = objLog.AgregarPedido(codigo_per, codigo_cco, 1, 0, CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue)
            End If
            ' Registrar el detalle del pedido
            objLog.AgregarDetallePedido(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, Me.cboProgramaPresu.SelectedValue, codDetPresup, "")
            objLog.CerrarTransaccionCnx()
            objLog = Nothing
            FinalizarIngresoDetalle()
            pnlDistribuir.Visible = False
            If txtPedido.Text <> "" Then
                cmdEnviar.Visible = True
            End If
        Catch ex As Exception
            objLog.CancelarTransaccionCnx()
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        End Try
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        Me.pnlPresupuesto.Visible = True
        ConsultarPresupuesto()
        LimpiarDetalle()
        Me.txtDetPresup.Text = ""
        Panel1.Visible = False
    End Sub
    Private Sub ConsultarPresupuesto()
        Dim objcon As ClsLogistica
        objcon = New ClsLogistica
        Dim DatosIng, datosEgr As New Data.DataTable
        datosEgr = objcon.ConsultarDetallePresupuesto(Me.cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, "E")
        gvPresupuesto.DataSource = datosEgr
        gvPresupuesto.DataBind()
        If datosEgr.Rows.Count = 0 Then Me.pnlPresupuesto.Visible = False
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

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged

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
    Protected Sub gvPresupuesto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPresupuesto.SelectedIndexChanged
        LimpiarDetalle()
        txtDetPresup.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(0)
        cboProgramaPresu.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(1)
        txtCodItem.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(2)
        Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(1).Text)
        Me.txtComentarioReq.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(2).Text)
        Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(3).Text)
        Me.txtCantidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(4).Text)
    End Sub
    Private Sub EliminarDistribucionItemDetalle(ByVal codigo_ecc As Integer)
        Dim objLog As New ClsLogistica
        objLog.EliminarDistribucionItemDetalle(codigo_ecc)
        objLog = Nothing
        ConsultarDistribucion()
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
    Protected Sub gvDistribucion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDistribucion.RowDataBound
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.gvDistribucion.Columns(4).Visible = True
            Me.gvDistribucion.Columns(5).Visible = True
        Else
            Me.gvDistribucion.Columns(4).Visible = False
            Me.gvDistribucion.Columns(5).Visible = False

        End If
    End Sub

    Protected Sub gvDistribucion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDistribucion.RowDeleting
        'EliminarDistribucionItemDetalle(Me.gvDistribucion.DataKeys.Item(e.RowIndex).Values(0))
    End Sub
    Protected Sub gvDistribucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistribucion.SelectedIndexChanged
        Me.txtCantidadDistribucion.Text = HttpUtility.HtmlDecode(gvDistribucion.SelectedRow.Cells(1).Text)
        Me.cboCecosEjecucion.SelectedValue = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(1)
        Me.txtCodigo_Ecc.Text = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(0)
    End Sub
    Protected Sub cboVeredicto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboVeredicto.SelectedIndexChanged
        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = True
        Me.txtPedido.Text = ""
        cmdCalificar.Enabled = False
        limpiarCalificacion()
    End Sub
    Private Sub limpiarCalificacion()
        rbOpciones.SelectedIndex = -1
        Me.txtObservacion.Text = ""
        pnlDerivar.Visible = False
    End Sub
    Protected Sub cmdCalificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalificar.Click
        If Me.txtPedido.Text <> "" Then
            Dim log As New ClsLogistica
            If Me.rbOpciones.SelectedValue = "D" Then
                log.DerivarPedido(Me.txtPedido.Text, Me.cboPersonalDerivar.SelectedValue, Request.QueryString("id"), _
                                Me.txtObservacion.Text)
            End If
            log.CalificarPedido(Me.txtPedido.Text, Request.QueryString("id"), rbOpciones.SelectedValue, Me.txtObservacion.Text)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosRevisor.aspx?id=" & Request.QueryString("id") & "';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione un pedido de la lista para enviar');", True)
        End If
    End Sub
    Protected Sub rbOpciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbOpciones.SelectedIndexChanged
        If rbOpciones.SelectedValue = "D" Then
            pnlDerivar.Visible = True
        Else
            pnlDerivar.Visible = False
        End If
    End Sub
    Private Sub DenegarItemDetalle(ByVal codigo_dpe As Integer)
        Dim objLog As New ClsLogistica
        objLog.DenegarItemDetalle(codigo_dpe)
        objLog = Nothing
        CargarDetalle()
        objLog = Nothing
        FinalizarIngresoDetalle()
    End Sub
    Private Sub ConsultarPedidos()
        Dim objcon As ClsLogistica
        objcon = New ClsLogistica
        Dim datos As New Data.DataTable
        Dim pedido, trabajador As String
        If Me.txtIdPedido.Text.Trim = "" Then
            pedido = -1
        Else
            pedido = Me.txtIdPedido.Text
        End If

        If Me.txtTrabajador.Text.Trim = "" Then
            trabajador = "%"
        Else
            trabajador = Me.txtTrabajador.Text.Trim
        End If

        datos = objcon.ConsultarPedidosSolicitante(pedido, Request.QueryString("id"), trabajador)
        gvPedidos.DataSource = datos
        gvPedidos.DataBind()

    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        ConsultarPedidos()
    End Sub
End Class

