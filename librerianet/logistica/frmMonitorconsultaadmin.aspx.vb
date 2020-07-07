
Partial Class librerianet_logistica_frmMonitorconsultaadmin
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.cboInstancia.SelectedValue = 2

        If Not IsPostBack Then
            Dim log As New ClsLogistica
            Dim fun As New ClsFunciones
            fun.CargarListas(cboInstancia, log.ConsultarInstancias("TO"), "codigo_Ipl", "nombreInstancia_Ipl")
            fun.CargarListas(cboPersonalDerivar, log.ConsultarPersonalDerivacion(), "codigo_Per", "personal")
            fun.CargarListas(cboDerivarCon, log.ConsultarPersonaResponsableDeAprobacion(), "Codigo", "Persona")
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

            'gvCabOrden.DataSource = log.BuscarOrden("44049")
            'gvCabOrden.DataBind()
        End If
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.cmdEnviar.Visible = True
        Else
            Me.cmdEnviar.Visible = False
        End If
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

        gvCabOrden.DataBind()

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
        gvAtenciones.DataSource = objLog.ConsultarAtencionesPedido(codigo_ped)
        gvRevisiones.DataBind()
        gvEstados.DataBind()
        gvObservaciones.DataBind()
        gvAtenciones.DataBind()
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
        'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" + e.Row.Cells(9).Text + "');", True)

        'If e.Row.Cells(9).Text = "Rechazado" Then
        '    e.Row.ForeColor = Drawing.Color.Red
        '    e.Row.Font.Strikeout = True
        'End If
        'If e.Row.Cells(9).Text = "Atendido" Then
        '    e.Row.ForeColor = Drawing.Color.Blue
        'End If
        'If e.Row.Cells(9).Text = "Por despachar" Or e.Row.Cells(9).Text.Substring(0, 5).ToString = "En al" Then
        '    e.Row.ForeColor = Drawing.Color.Green
        'End If
        'If e.Row.Cells(9).Text.Substring(0, 6).ToString = "Tesore" Then
        '    e.Row.ForeColor = Drawing.Color.Orange
        'End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk As LinkButton = CType(e.Row.Cells(9).FindControl("hlnkOrden"), LinkButton)

            If lnk.Text = "Rechazado" Then
                e.Row.ForeColor = Drawing.Color.Red
                e.Row.Font.Strikeout = True
                lnk.Style("color") = "Red"
                lnk.Style("text-decoration") = "line-through"
            End If
            If lnk.Text = "Atendido" Then
                e.Row.ForeColor = Drawing.Color.Blue
                lnk.Style("color") = "Blue"
            End If
            If lnk.Text = "Por despachar" Or lnk.Text.Substring(0, 5).ToString = "En al" Then
                e.Row.ForeColor = Drawing.Color.Green
                lnk.Style("color") = "Green"
            End If
            If lnk.Text.Substring(0, 6).ToString = "Tesore" Then
                e.Row.ForeColor = Drawing.Color.Orange
                lnk.Style("color") = "Orange"
            End If

            If lnk.Text = "En orden de compra" Or lnk.Text = "En orden de servicio" Then
                lnk.Style("text-decoration") = "underline"
            Else
                lnk.Style("cursor") = "text"
            End If
        End If

        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 158 Then 'adminstrador del sistema puede transferir a compras/despacho/tesorería
            e.Row.Cells(11).Enabled = True
            e.Row.Cells(12).Enabled = True
            e.Row.Cells(13).Enabled = True
        Else
            If Request.QueryString("ctf") = 123 Then 'tesorería tiene activo el menú de transferir a tesorería
                e.Row.Cells(11).Enabled = True
            Else
                e.Row.Cells(11).Enabled = False
            End If

            e.Row.Cells(12).Enabled = False
            e.Row.Cells(13).Enabled = False
        End If

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        '    e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        '    e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDetallePedido','Select$" & e.Row.RowIndex & "');")
        '    e.Row.Style.Add("cursor", "hand")
        'End If
    End Sub

    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        Dim ecc As Integer
        Dim estado As String
        Dim objLog As New ClsLogistica

        ecc = gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0)
        estado = gvDetallePedido.DataKeys.Item(e.RowIndex).Values(3)
        If estado = "En compra" Or estado = "Tesorería" Or estado = "Por despachar" Then
            If estado = "Por despachar" Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido a que el ítem ya se encuentra Por Despachar');", True)
            Else
                objLog.CambiarItemDespachar(ecc)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido que el ítem del pedido se encuentra: " & estado & "');", True)
        End If

        ConsultarDatosPedido(Me.txtPedido.Text)

    End Sub

    Protected Sub gvDetallePedido_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetallePedido.RowEditing
        Dim ecc As Integer
        Dim estado As String
        Dim objLog As New ClsLogistica

        ecc = gvDetallePedido.DataKeys.Item(e.NewEditIndex).Values(0)
        estado = gvDetallePedido.DataKeys.Item(e.NewEditIndex).Values(3)
        If estado = "Por despachar" Or estado = "Tesorería" Or estado = "En compra" Then
            If estado = "En compra" Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido a que el ítem ya se encuentra en Compras');", True)
            Else
                objLog.CambiarItemCompras(ecc, Me.txtPedido.Text, Request.QueryString("id"))
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido que el ítem del pedido se encuentra: " & estado & "');", True)
        End If

        ConsultarDatosPedido(Me.txtPedido.Text)
    End Sub

    Protected Sub gvDetallePedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetallePedido.SelectedIndexChanged
        '-----------------Aqui me quede 14.02.201 - modificado el 17.02.2014 por fcastillo---------

        Dim ecc As Integer
        Dim estado As String
        Dim objLog As New ClsLogistica
        Dim personalTesoreria As Integer

        personalTesoreria = Request.QueryString("id")
        Response.Write(personalTesoreria)
        ecc = gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(0)
        estado = gvDetallePedido.DataKeys.Item(gvDetallePedido.SelectedIndex).Values(3)
        ' validamos los estados de los ítems solicitados
        If personalTesoreria = 499 Then 'Para que la señora rocio coronado pueda jalar a tesorería los pedidos de aquellos usuarios que no aprueban vía campus
            objLog.CambiarItemTesoreria(ecc)
        Else
            If estado = "En compra" Or estado = "Por despachar" Or estado = "Tesorería" Then
                If estado = "Tesorería" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido a que ítem ya se encuentra en Tesorería');", True)
                Else
                    objLog.CambiarItemTesoreria(ecc)
                End If
            Else

                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido que el ítem del pedido se encuentra: " & estado & "');", True)
            End If
        End If
        ConsultarDatosPedido(Me.txtPedido.Text)


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
        Dim pedido, trabajador, item As String
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
        If txtDescripcionItem.Text.Trim = "" Then
            item = "%"
        Else
            item = txtDescripcionItem.Text.Trim
        End If
        'If Me.chkPorItem.Checked = True Then
        'datos = objcon.ConsultarPedidos(txtDescripcionItem.Text)
        'Else
        datos = objcon.ConsultarPedidosPorItem(pedido, Me.cboCentroCostos.SelectedValue, trabajador, txtDescripcionItem.Text)
        'End If
        gvPedidos.DataSource = datos
        gvPedidos.DataBind()

    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        ConsultarPedidos()
        Me.gvDetallePedido.DataBind()
        Me.gvRevisiones.DataBind()
        Me.gvEstados.DataBind()
        Me.gvCabOrden.DataBind()
        Me.txtPedido.Text = ""
    End Sub

    Protected Sub chkPorItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPorItem.CheckedChanged
        If Me.chkPorItem.Checked = True Then
            Me.txtIdPedido.Text = ""
            Me.txtIdPedido.Enabled = False
            Me.txtTrabajador.Text = ""
            Me.txtTrabajador.Enabled = False
            Me.cboCentroCostos.SelectedIndex = -1
            Me.cboCentroCostos.Enabled = False
            Me.txtDescripcionItem.Text = ""
            Me.txtDescripcionItem.Enabled = True
        Else
            Me.txtIdPedido.Text = ""
            Me.txtIdPedido.Enabled = True
            Me.txtTrabajador.Text = ""
            Me.txtTrabajador.Enabled = True
            Me.cboCentroCostos.SelectedIndex = -1
            Me.cboCentroCostos.Enabled = True
            Me.txtDescripcionItem.Text = ""
            Me.txtDescripcionItem.Enabled = False
        End If
    End Sub

    Protected Sub cmdDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDerivar.Click
        Dim estado As String
        Dim objLog As New ClsLogistica
        If gvPedidos.Rows.Count > 0 Then
            estado = Me.gvPedidos.SelectedRow.Cells(6).Text
            'Response.Write(estado)
            If estado = "Generado" Then
                objLog.derivarAprobacionPedido(Me.cboDerivarCon.SelectedValue, CInt(Me.txtPedido.Text))
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('para poder derivar el pedido su estado debe ser: Generado');", True)
            End If
        End If

    End Sub

    Protected Sub hlnkOrden_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim objPre As New ClsLogistica
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim codigo_ecc As String
        codigo_ecc = lnk.CommandArgument

        If lnk.Text <> "Atendido" And lnk.Text <> "En almacén" And lnk.Text <> "Por despachar" Then
            gvCabOrden.DataSource = objPre.BuscarOrden(codigo_ecc)
            gvCabOrden.DataBind()
        Else
            gvCabOrden.DataBind()
        End If
        
    End Sub
End Class
