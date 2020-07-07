
Partial Class logistica_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If Not IsPostBack Then
            Dim log As New ClsLogistica
            Dim fun As New ClsFunciones
            fun.CargarListas(cboInstancia, log.ConsultarInstancias("TO"), "codigo_Ipl", "nombreInstancia_Ipl")
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim objlog As New ClsLogistica
            Dim datos As New Data.DataTable
            'Cargar datos de usuario
            datos = objpre.ObtenerDatosUsuario(Request.QueryString("id"))
            If datos.Rows.Count > 0 Then
                'lblUsuario.Text = datos.Rows(0).Item("usuario")
                'lblCargo.Text = datos.Rows(0).Item("Cargo")
                'lblCentroCostos.Text = datos.Rows(0).Item("CentroCostos")
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
                'BloquearCabecera(False)
            Else
                'BloquearCabecera(True)
            End If
            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
            txtCantidad.Attributes.Add("onKeyPress", "validarnumero()")
            Panel1.Visible = False
            Panel3.Visible = False
            'MostrarBusquedaCeCos(False)
            Me.TxtFechaEsperada.Text = FormatDateTime(Now, DateFormat.ShortDate)

            fuCargarArchivo.Attributes.Add("onkeypress", "return false;")
            fuCargarArchivo.Attributes.Add("onkeydown", "return false;")
            fuCargarArchivo.Attributes.Add("onkeyup", "return false;")
            fuCargarArchivo.Attributes.Add("onpaste", "return false;")

            cmdEnviar.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(cmdEnviar, Nothing) + ";")

        End If
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.cmdEnviar.Visible = True
            cmdEliminar.Visible = True
        Else
            Me.cmdEnviar.Visible = False
            cmdEliminar.Visible = False
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
        Try
            'Me.txtPedido.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(0).Text)
            Me.txtPedido.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(0).Text)
            Me.cboEstado.SelectedItem.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text)

            Dim sw As Boolean = False
            For i As Integer = 0 To Me.cboCecos.Items.Count - 1
                If (Me.cboCecos.Items(i).Value = Me.gvPedidos.SelectedDataKey.Item("codigo_cco")) Then
                    Me.cboCecos.SelectedIndex = i
                    sw = True
                End If
            Next

            If (sw = False) Then
                Me.cboCecos.SelectedIndex = 0
            End If

            ConsultarDatosPedido(Me.txtPedido.Text)
            pnlDistribuir.Visible = False
            FinalizarIngresoDetalle()
            If Me.cboInstancia.SelectedValue = 1 Then
                UpdatePanel1.Visible = True
                UpdatePanel2.Visible = True
                Me.cmdGuardar.Visible = True
            Else
                UpdatePanel1.Visible = False
                UpdatePanel2.Visible = False
                Me.cmdGuardar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatos.Click
        If Me.txtPedido.Text <> "" Then
            ConsultarDatosPedido(Me.txtPedido.Text)
        End If
    End Sub
    Private Sub CargarDetalle()
        Dim objLog As New ClsLogistica
        gvDetallePedido.DataSource = objLog.ConsultarDetalle(Me.txtPedido.Text)
        gvDetallePedido.DataBind()
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
        Dim estado As String
        If Me.txtPedido.Text <> "" Then
            estado = Me.gvPedidos.SelectedRow.Cells(5).Text
            If gvDetallePedido.Rows.Count > 0 Then
                If estado = "Rechazado" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El pedido no se puede enviar porque se ecuentra como: Rechazado');", True)
                Else
                    Dim log As New ClsLogistica
                    log.CalificarPedido(Me.txtPedido.Text, Request.QueryString("id"), "C", "")
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se envió el pedido satisfactoriamente');", True)
                    ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosSolicitante.aspx?id=" & Request.QueryString("id") & "';", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El detalle del pedido no cuenta con Items Registrados');", True)
            End If
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
        txtEspecificaciones.Text = ""
        Especificaciones.Visible = False
        hlAdjunto.Enabled = False
        hlAdjunto.Target = ""
        hlAdjunto.Text = ""
    End Sub


    Protected Sub gvDetallePedido_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetallePedido.RowDataBound
        If Me.cboInstancia.SelectedValue = 1 Then
            Me.gvDetallePedido.Columns(8).Visible = True
            '   e.Row.Cells(9).Text = "Distribuir"
            Me.gvDetallePedido.Columns(10).Visible = True
        Else
            '   e.Row.Cells(9).Text = "Distribución"
            Me.gvDetallePedido.Columns(8).Visible = False
            Me.gvDetallePedido.Columns(10).Visible = False
        End If
        If e.Row.Cells(7).Text = "Rechazado" Then
            e.Row.ForeColor = Drawing.Color.Red
            e.Row.Font.Strikeout = True
        End If
    End Sub

    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        txtCodigoDpe.Text = ""
        EliminarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
        Me.gvPedidos.DataBind()

    End Sub

    Protected Sub gvDetallePedido_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetallePedido.RowEditing
        Dim estado As String
        Dim delimiter As Char = "|"
        Dim substrings() As String
        Try

            estado = Me.gvPedidos.SelectedRow.Cells(5).Text
            'Response.Write(estado)
            If estado = "Rechazado" Then
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('El item no se puede editar porque el Pedido se ecuentra como: Rechazado');", True)
            Else
                Me.txtCodigoDpe.Text = Me.gvDetallePedido.DataKeys.Item(e.NewEditIndex).Values(0)
                Dim dt As New Data.DataTable
                Dim objLog As New ClsLogistica
                dt = objLog.ConsultarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.NewEditIndex).Values(0))
                Me.cboCecos.SelectedValue = dt.Rows(0).Item("codigoPrincipal_Cco")
                Me.txtComentarioReq.Text = dt.Rows(0).Item("observacion_Dpe")
                Me.txtPrecioUnit.Text = dt.Rows(0).Item("precioReferencial_Dpe")
                Me.txtCantidad.Text = dt.Rows(0).Item("cantidad_Dpe")
                Me.TxtFechaEsperada.Text = dt.Rows(0).Item("fechaEsperada_Dpe")
                Me.cboProgramaPresu.Text = dt.Rows(0).Item("codigo_Ppr")
                Me.txtCodItem.Text = dt.Rows(0).Item("idArt")
                Me.txtConcepto.Text = dt.Rows(0).Item("descripcionArt")
                rblModoDistribucion.SelectedValue = dt.Rows(0).Item("modoDistribucion_Dpe")
                txtDetPresup.Text = dt.Rows(0).Item("codigo_Dpr")
                substrings = dt.Rows(0).Item("especificacion_Dpe").ToString.Split(delimiter)
                txtEspecificaciones.Text = substrings(0)

                If substrings(1) <> "" Then
                    Especificaciones.Visible = True
                    hlAdjunto.Enabled = True
                    hlAdjunto.Text = substrings(1)
                    hlAdjunto.NavigateUrl = "archivos/" & substrings(1)

                    If System.IO.Path.GetExtension(substrings(1)) <> ".doc" And System.IO.Path.GetExtension(substrings(1)) <> ".xls" And System.IO.Path.GetExtension(substrings(1)) <> ".docx" And System.IO.Path.GetExtension(substrings(1)) <> ".xlsx" Then
                        hlAdjunto.Target = "_blank"
                    End If

                End If

            End If

        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub

    Protected Sub gvDetallePedido_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetallePedido.SelectedIndexChanged
        ConsultarDistribucion()
        txtCodigoDpe.Text = ""
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
        Dim rpta As Integer
        rpta = objLog.ElminarItemDetalle(codigo_dpe)
        objLog = Nothing

        If rpta = 2 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede eliminar el Ítem ya que posee una Solicitud de Anticipo relacionada.');", True)
        End If

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
        Dim ruta As String
        Try

            If Me.txtDetPresup.Text = "" Then
                codDetPresup = 0
            Else
                codDetPresup = Me.txtDetPresup.Text
            End If

            If (Me.cboCecos.SelectedValue = -1) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatosCco", "alert('Debe seleccionar un centro de costos')", True)
                Exit Sub
            End If

            If txtCodItem.Text.ToString = "" Or Me.txtConcepto.Text.ToString = "" Or Me.txtPrecioUnit.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Por favor complete los datos correctamente. Haga clic en el [ícono de búsqueda] y luego selecciónelo.')", True)
                Exit Sub
            End If

            If Me.txtCantidad.Text <= 0 Or Me.txtCantidad.Text = "" Then
                ClientScript.RegisterStartupScript(Me.GetType, "CorregirDatos", "alert('La cantidad a solicitar debe ser mayor a 0.')", True)
                Exit Sub
            End If

            'If Me.hdDisponible.Value > 0 Then
            '    If (CDbl(Me.txtPrecioUnit.Text) * CDbl(Me.txtCantidad.Text)) > CDbl(Me.hdDisponible.Value) Then
            '        ClientScript.RegisterStartupScript(Me.GetType, "CorregirDatos", "alert('El PRECIO X CANTIDAD a SOLICITAR no debe superar lo DISPONIBLE presupuestado: S/. " & Me.hdDisponible.Value.ToString & "')", True)
            '        Exit Sub
            '    End If
            'End If

            If fuCargarArchivo.PostedFile.ContentLength > 4000000 Then
                ClientScript.RegisterStartupScript(Me.GetType, "CorregirDatos", "alert('El archivo adjunto supera el peso máximo: 4MB')", True)
                Exit Sub
            End If


            objLog.AbrirTransaccionCnx()

            If hlAdjunto.Text <> "" And fuCargarArchivo.FileName = "" Then
                ruta = hlAdjunto.Text
            Else
                ruta = SubirArchivo()
            End If

            If txtPedido.Text = "" Then
                datos = ObjPre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    codigo_per = datos.Rows(0).Item("codigo_per")
                    codigo_cco = datos.Rows(0).Item("codigo_cco")
                End If
                'Registrar la cabecera del pedido sólo la primera vez
                Me.txtPedido.Text = objLog.AgregarPedido(codigo_per, codigo_cco, 1, 0, CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue)
            End If

            Me.txtEspecificaciones.Text &= IIf(Trim(ruta) = "", "", "|" & ruta)
            ' Registrar el detalle del pedido
            If Me.txtCodigoDpe.Text = "" Then
                objLog.AgregarDetallePedido(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, Me.cboProgramaPresu.SelectedValue, codDetPresup, Me.txtEspecificaciones.Text)
            Else
                objLog.ActualizarDetallePedido(CInt(Me.txtPedido.Text), CInt(txtCodItem.Text), CInt(cboCecos.SelectedValue), CDbl(Me.txtPrecioUnit.Text), CDbl(Me.txtCantidad.Text), Me.txtComentarioReq.Text, Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, CInt(Me.cboProgramaPresu.SelectedValue), CInt(codDetPresup), CInt(Me.txtCodigoDpe.Text), Me.txtEspecificaciones.Text)
            End If
            objLog.CerrarTransaccionCnx()
            objLog = Nothing
            FinalizarIngresoDetalle()
            pnlDistribuir.Visible = False
            DatosPresupuesto()
            Me.gvPedidos.DataBind()
            If txtPedido.Text <> "" Then
                cmdEnviar.Visible = True
            End If

        Catch ex As Exception
            objLog.CancelarTransaccionCnx()
            Response.Write(ex.Message)
            ' Response.Write(Me.txtCodigoDpe.Text)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

    Protected Function SubirArchivo() As String
        Dim nombre As String
        nombre = ""
        Try
            If fuCargarArchivo.FileName <> "" And fuCargarArchivo.PostedFile.ContentLength < 4000000 Then
                If System.IO.File.Exists(MapPath("archivos/" & fuCargarArchivo.FileName.ToString)) Then
                    fuCargarArchivo.SaveAs(MapPath("archivos/" & System.IO.Path.GetFileNameWithoutExtension(fuCargarArchivo.FileName) & "(1)" & System.IO.Path.GetExtension(fuCargarArchivo.FileName)))
                    nombre = System.IO.Path.GetFileNameWithoutExtension(fuCargarArchivo.FileName) & "(1)" & System.IO.Path.GetExtension(fuCargarArchivo.FileName)

                Else
                    fuCargarArchivo.SaveAs(MapPath("archivos/" & fuCargarArchivo.FileName.ToString))
                    nombre = fuCargarArchivo.FileName.ToString

                End If
                
            End If
            Return nombre
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        DatosPresupuesto()
    End Sub
    Private Sub DatosPresupuesto()
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
        If gvPresupuesto.SelectedRow.Cells(5).Text > 0 Then
            txtDetPresup.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(0)
            cboProgramaPresu.SelectedValue = gvPresupuesto.DataKeys.Item(gvPresupuesto.SelectedIndex).Values(1)
            txtCodItem.Text = Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(2)
            Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(1).Text)
            Me.txtComentarioReq.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(2).Text)
            Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(3).Text)
            Me.txtCantidad.Text = HttpUtility.HtmlDecode(gvPresupuesto.SelectedRow.Cells(5).Text)
        End If
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
        EliminarDistribucionItemDetalle(Me.gvDistribucion.DataKeys.Item(e.RowIndex).Values(0))
    End Sub

    Protected Sub gvDistribucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDistribucion.SelectedIndexChanged
        Me.txtCantidadDistribucion.Text = HttpUtility.HtmlDecode(gvDistribucion.SelectedRow.Cells(1).Text)
        Me.cboCecosEjecucion.SelectedValue = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(1)
        Me.txtCodigo_Ecc.Text = gvDistribucion.DataKeys(gvDistribucion.SelectedIndex).Values(0)
    End Sub

    Protected Sub cmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click
        If Me.txtPedido.Text <> "" Then
            Dim log As New ClsLogistica
            log.ElmininarPedido(Me.txtPedido.Text)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se elminó el pedido satisfactoriamente');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosSolicitante.aspx?id=" & Request.QueryString("id") & "';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione un pedido de la lista para eliminar');", True)
        End If
    End Sub
    'treyes 05/07/2016 se deshabilita a solicitud de Carlos Monja
    'Protected Sub cmdClonar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClonar.Click
    '    Try
    '        If Me.txtPedido.Text <> "" Then
    '            Dim log As New ClsLogistica
    '            log.ClonarPedido(Me.txtPedido.Text)
    '            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se duplicó el pedido satisfactoriamente');", True)
    '            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosSolicitante.aspx?id=" & Request.QueryString("id") & "';", True)
    '        Else
    '            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione un pedido de la lista para duplicar');", True)
    '        End If
    '    Catch ex As Exception
    '        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Error: " & ex.Message & "');", True)
    '    End Try        
    'End Sub

End Class
