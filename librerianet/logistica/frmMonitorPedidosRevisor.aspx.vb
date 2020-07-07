
Partial Class logistica_Default
    Inherits System.Web.UI.Page
    Dim tipo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim datosCeCo As New Data.DataTable
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Me.cboInstancia.SelectedValue = 2

            If Not IsPostBack Then
                Dim log As New ClsLogistica
                Dim fun As New ClsFunciones
              
                fun.CargarListas(cboInstancia, log.ConsultarInstancias("TO"), "codigo_Ipl", "nombreInstancia_Ipl")
                fun.CargarListas(cboPersonalDerivar, log.ConsultarPersonalDerivacion(), "codigo_Per", "personal")
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

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

                tipo = 1
                llenarGvPedidos()
                cmdCalificar.Attributes.Add("onclick", " this.disabled = true;" + ClientScript.GetPostBackEventReference(cmdCalificar, Nothing) + ";")
            End If
            If Me.cboInstancia.SelectedValue = 1 Then
                Me.cmdEnviar.Visible = True
            Else
                Me.cmdEnviar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try    
    End Sub
    Protected Sub gvPedidos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPedidos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvPedidos','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write("Error en tabla pedidos: " & ex.Message)
        End Try        
    End Sub
    Protected Sub gvPedidos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPedidos.SelectedIndexChanged
        Try
            Dim log As New ClsLogistica
            Dim datosCeCo As New Data.DataTable
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones

            Me.txtPedido.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(0).Text)
            Me.cboEstado.SelectedItem.Text = HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text)
            'If (Me.gvPedidos.SelectedDataKey.Item("codigo_ejp") IsNot Nothing) Then
            'Response.Write(Me.gvPedidos.SelectedDataKey.Item("codigo_ejp"))
            Me.cboPeriodoPresu.SelectedValue = Me.gvPedidos.SelectedDataKey.Item("codigo_ejp")
            'End If


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

            If Request.QueryString("ctf") = 1 Then

                datosCeCo = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
                objfun.CargarListas(ddlCecoDestino, datosCeCo, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")
                pnlCargar.Visible = True
                Dim dtsNoPresu As Data.DataTable
                dtsNoPresu = log.ConsultarImporteNoPresupuestado(CInt(Me.txtPedido.Text))
                lblNoPresupuestado.Text = dtsNoPresu.Rows(0).Item("NoPresup").ToString
                gvPresupuestoTransferir.DataBind()
                If lblNoPresupuestado.Text = 0 Then
                    cmdTransferir.Enabled = False
                Else
                    cmdTransferir.Enabled = True
                End If
            Else
                pnlCargar.Visible = False
            End If
            Me.cmdCalificar.Enabled = True

            If HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text) = "Aprobado" _
            And Request.QueryString("id") = Me.gvPedidos.SelectedDataKey.Item("codigo_per") And Request.QueryString("ctf") <> "156" And Request.QueryString("ctf") <> "158" And Request.QueryString("id") <> 325 Then
                Me.rbOpciones.Enabled = False
            ElseIf HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text) = "Pre Aprobado" _
            And Request.QueryString("id") = Me.gvPedidos.SelectedDataKey.Item("codigo_per") And Request.QueryString("ctf") <> "156" And Request.QueryString("ctf") <> "158" And Request.QueryString("id") <> 325 Then
                Me.rbOpciones.Enabled = False
            Else
                Me.rbOpciones.Enabled = True
            End If


            'Inicio HCano 12-07-2019 
            Dim objLog As New ClsLogistica
            Dim dt As New Data.DataTable
            Dim responsable As String = ""

            dt = objLog.ConsultarRevisiones(Me.gvPedidos.SelectedRow.Cells(0).Text)

            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i).Item("Evaluación") = "Pendiente" Then
                    responsable = dt.Rows(i).Item("Persona")
                    Exit For
                End If
            Next

            Dim dt_Per As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_per As Integer = Request.QueryString("id")
            cnx.AbrirConexion()
            dt_Per = cnx.TraerDataTable("LOG_BuscaPersonal", codigo_per, "")
            cnx.CerrarConexion()

            If responsable = Replace(dt_Per.Rows(0).Item("Nombre"), ",", "") Or Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158 Then '156: Director de Finanzas o 158:Coordinador de Finanzas
                Me.rbOpciones.Visible = True
                Me.cmdCalificar.Visible = True
                Me.txtObservacion.Visible = True
                Me.lblobs.Visible = True
            Else
                Me.rbOpciones.Visible = False
                Me.cmdCalificar.Visible = False
                Me.txtObservacion.Visible = False
                Me.lblobs.Visible = False
            End If

            'Fin HCano 12-07-2019
        Catch ex As Exception
            Response.Write("Error al cargar los pedidos: " & ex.Message)
        End Try

    End Sub
    Protected Sub lnkDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatos.Click
        If Me.txtpedido.Text <> "" Then
            ConsultarDatosPedido(Me.txtpedido.Text)
        End If
    End Sub
    Private Sub CargarDetalle()
        Dim objLog As New ClsLogistica
        gvDetallePedido.DataSource = objLog.ConsultarDetalleEjecutado(Me.txtpedido.Text) ''' se cambio, antes era detalle no ejecutado
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
        If Me.txtpedido.Text <> "" Then
            ConsultarRevisionesPedido(Me.txtpedido.Text)
        End If
    End Sub

    Protected Sub cboInstancia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboInstancia.SelectedIndexChanged
        txtpedido.Text = ""
        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = False
    End Sub
    Private Sub FinalizarIngresoDetalle()
        Dim objLog As New ClsLogistica
        Dim totalDetalle As Double
        totalDetalle = 0
        LimpiarDetalle()
        CargarDetalle()
        totalDetalle = objLog.ConsultarTotalDetalle(Me.txtpedido.Text)
        objLog.actualizarCabeceraPedido(Me.txtpedido.Text, totalDetalle)
        'Me.gvPedidos.DataBind()

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
        If Me.txtpedido.Text <> "" Then
            Dim log As New ClsLogistica
            log.CalificarPedido(Me.txtpedido.Text, Request.QueryString("id"), "C", "")
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

    End Sub
    Protected Sub gvDetallePedido_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetallePedido.RowDeleting
        'EliminarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
        Dim estado As String

        Dim valida As Integer = 0

        'Inicio HCano 12-07-2019 
        Dim objLog As New ClsLogistica
        Dim dt As New Data.DataTable
        Dim responsable As String = ""

        dt = objLog.ConsultarRevisiones(HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(0).Text))

        For i As Integer = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("Evaluación") = "Pendiente" Then
                responsable = dt.Rows(i).Item("Persona")
                Exit For
            End If
        Next

        Dim dt_Per As New Data.DataTable
        Dim cnx As New ClsConectarDatos
        cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim codigo_per As Integer = Request.QueryString("id")
        cnx.AbrirConexion()
        dt_Per = cnx.TraerDataTable("LOG_BuscaPersonal", codigo_per, "")
        cnx.CerrarConexion()

        If responsable = Replace(dt_Per.Rows(0).Item("Nombre"), ",", "") Or Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158 Then '156: Director de Finanzas o 158:Coordinador de Finanzas
            valida = 0
        Else
            valida = 1
        End If

        'If HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text) = "Aprobado" _
        '    And Request.QueryString("id") = Me.gvPedidos.SelectedDataKey.Item("codigo_per") And Request.QueryString("ctf") <> "158" And Request.QueryString("id") <> 325 Then
        '    valida = 1
        'ElseIf HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text) = "Pre Aprobado" _
        'And Request.QueryString("id") = Me.gvPedidos.SelectedDataKey.Item("codigo_per") And Request.QueryString("ctf") <> "158" And Request.QueryString("id") <> 325 Then
        '    valida = 1
        'End If
        If valida = 0 Then
            estado = Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(2)
            If estado = "En compra" Or estado = "Por despachar" Or estado = "Tesorería" Or estado = "Registrado" Then
                DenegarItemDetalle(Me.gvDetallePedido.DataKeys.Item(e.RowIndex).Values(0))
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar esta acción debido que el ítem del pedido se encuentra: " & estado & "');", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede realizar la operación Pedido en estado : " + HttpUtility.HtmlDecode(Me.gvPedidos.SelectedRow.Cells(5).Text) + "');", True)
        End If

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
            If txtpedido.Text = "" Then
                datos = ObjPre.ObtenerDatosUsuario(Request.QueryString("id"))
                If datos.Rows.Count > 0 Then
                    codigo_per = datos.Rows(0).Item("codigo_per")
                    codigo_cco = datos.Rows(0).Item("codigo_cco")
                End If
                'Registrar la cabecera del pedido sólo la primera vez
                Me.txtpedido.Text = objLog.AgregarPedido(codigo_per, codigo_cco, 1, 0, CDate("31/12/2009"), "", Me.cboPeriodoPresu.SelectedValue)
            End If
            ' Registrar el detalle del pedido
            objLog.AgregarDetallePedido(Me.txtPedido.Text, txtCodItem.Text, cboCecos.SelectedValue, Me.txtPrecioUnit.Text, Me.txtCantidad.Text, Me.txtComentarioReq.Text, Me.TxtFechaEsperada.Text, 1, "S", rblModoDistribucion.SelectedValue, Me.cboProgramaPresu.SelectedValue, codDetPresup, "")
            objLog.CerrarTransaccionCnx()
            objLog = Nothing
            FinalizarIngresoDetalle()
            pnlDistribuir.Visible = False
            If txtpedido.Text <> "" Then
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
        llenarGvPedidos()
        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = True
        Me.txtpedido.Text = ""
        cmdCalificar.Enabled = False
        limpiarCalificacion()
    End Sub
    Private Sub limpiarCalificacion()
        rbOpciones.SelectedIndex = -1
        Me.txtObservacion.Text = ""
        pnlDerivar.Visible = False
    End Sub
    Private Function ValidarRbOpciones() As Boolean

        If rbOpciones.SelectedValue <> "C" And Me.txtObservacion.Text = "" Then
            Return False
        End If
        Return True
    End Function
    Protected Sub cmdCalificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCalificar.Click

        Dim valor As Boolean = False
        Dim estadoPedido As String
        Dim log As New ClsLogistica
        Me.cmdCalificar.Enabled = False

        Try
            'Page.Validate("calificar")
           
            If Me.IsValid And ValidarRbOpciones() Then
                'ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Aqui')", True)
                estadoPedido = Me.gvPedidos.SelectedRow.Cells(5).Text

                valor = devolverValor(False) ' - MODIFICADO POR FCASTILLO EL 18.02.2014 PARA VALIDAR LOS ESTADOS DE LOS ITEMS 
                'Response.Write(valor)
                If Me.txtPedido.Text <> "" Then
                    'Response.Write(estadoPedido)
                    If estadoPedido <> "Aprobado" Then
                        If Me.rbOpciones.SelectedValue = "D" Then
                            log.DerivarPedido(Me.txtPedido.Text, Me.cboPersonalDerivar.SelectedValue, Request.QueryString("id"), _
                                              Me.txtObservacion.Text)
                            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
                            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosRevisor.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "';", True)
                        Else
                            log.CalificarPedido(Me.txtPedido.Text, Request.QueryString("id"), rbOpciones.SelectedValue, _
                                                Me.txtObservacion.Text)
                            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
                            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosRevisor.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "';", True)
                        End If
                        'If Me.rbOpciones.SelectedValue = "O" And (Request.QueryString("id") = 771 Or Request.QueryString("id") = 1739) Then '771 es el código del coordinador de presupuesto "rvelasco"
                        '1739 es el código de manuel cuadra
                        If Me.rbOpciones.SelectedValue = "O" And (Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158) Then 'se cambia por el tipo funcion coordinador de finanzas y director de finanzas
                            log.actualizarVeredictoRevisores(CInt(Me.txtPedido.Text), "O", DateTime.Now.ToString("dd/MM/yyyy"))
                        End If
                    Else
                        If valor = False Then
                            If Me.rbOpciones.SelectedValue = "D" Then
                                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede derivar el Pedido debido a que ya se encuentra Aprobado');", True)
                            Else
                                log.CalificarPedido(Me.txtPedido.Text, Request.QueryString("id"), rbOpciones.SelectedValue, Me.txtObservacion.Text)
                                log.calificarDetallePedido(Me.txtPedido.Text, rbOpciones.SelectedValue)
                                ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)
                                ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmMonitorPedidosRevisor.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "';", True)
                            End If
                        Else
                            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('No se puede calificar el Pedido debido a que ya se encuentra comprometido en una o más transacciones');", True)
                        End If
                    End If

                    '---------------------Consultamos si el pedido pertenece al Ceco 620 "Area de Presupuesto"----------------

                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione un pedido de la lista para enviar');", True)
                End If
                End If


        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Public Function devolverValor(ByVal valor As Boolean) As Boolean
        Dim verdadero As Boolean = False
        Dim contador As Integer
        Dim f As Integer
        Dim estado As String

        contador = Me.gvDetallePedido.Rows.Count
        For f = 0 To contador - 1
            estado = Me.gvDetallePedido.Rows(f).Cells(8).Text
            If estado = "Atendido" Or estado = "En orden de compra" Or estado = "En orden de servicio" Or estado = "En almacén" Then
                verdadero = True
            End If
        Next
        Return verdadero

    End Function


    Protected Sub rbOpciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbOpciones.SelectedIndexChanged
        If rbOpciones.SelectedValue = "D" Then
            pnlDerivar.Visible = True
        Else
            pnlDerivar.Visible = False
        End If


        If gvPedidos.SelectedIndex >= 0 Then
            Me.cmdCalificar.Enabled = True
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



    Protected Sub ddlCecoDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCecoDestino.SelectedIndexChanged
        Dim objcon As ClsLogistica
        objcon = New ClsLogistica
        Dim datosEgr As New Data.DataTable
        '        Response.Write(ddlCecoDestino.SelectedValue)
        'comentado por esaavedra 10/10/2011
        'datosEgr = objcon.ConsultarDetallePresupuesto(Me.cboPeriodoPresu.SelectedValue, ddlCecoDestino.SelectedValue, "E")

        datosEgr = objcon.ConsultarDetallePresupuesto(Me.cboPeriodoPresu.SelectedValue, ddlCecoDestino.SelectedValue, "E")
        gvPresupuestoTransferir.DataSource = datosEgr
        gvPresupuestoTransferir.DataBind()
        Me.gvPresupuestoTransferir.SelectedIndex = -1
    End Sub


    Protected Sub cmdTransferir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTransferir.Click
        If gvPresupuestoTransferir.Rows.Count = 0 Then
            Me.lblMensajeSeleccione.Visible = True
        Else

            If gvPresupuestoTransferir.SelectedIndex = -1 Then
                Me.lblMensajeSeleccione.Visible = True
            Else
                ' Response.Write(gvPresupuestoTransferir.SelectedRow.Cells(5).Text)
                If CDbl(gvPresupuestoTransferir.SelectedRow.Cells(5).Text) < CDbl(Me.lblNoPresupuestado.Text) Then
                    Me.lblMensajeNoCubre.Visible = True
                Else
                    'Response.Write("hecho")
                    Dim objLog As New ClsLogistica
                    objLog.EjecutarDeotroPresupuesto(Me.txtpedido.Text, Me.gvPresupuestoTransferir.SelectedRow.Cells(0).Text)
                End If
                Me.lblMensajeSeleccione.Visible = False
            End If
        End If

    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim dtsPedidos As New Data.DataTable
        Dim objLog As New ClsLogistica
       
        Try

            llenarGvPedidos_Buscar()
            tipo = 2
            Me.pnlDatos.Visible = False
            Me.pnlRevision.Visible = True
            Me.txtPedido.Text = ""
            cmdCalificar.Enabled = False
            limpiarCalificacion()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
      

    End Sub

    Protected Sub gvPedidos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPedidos.PageIndexChanging
        gvPedidos.PageIndex = e.NewPageIndex
        Me.gvPedidos.SelectedIndex = -1
        If tipo = 1 Then
            llenarGvPedidos()
        Else
            llenarGvPedidos_Buscar()
        End If

        Me.pnlDatos.Visible = False
        Me.pnlRevision.Visible = True
        Me.txtPedido.Text = ""
        cmdCalificar.Enabled = False
        limpiarCalificacion()
    End Sub

    Protected Sub llenarGvPedidos()
        Dim dtsPedidos As New Data.DataTable
        Dim objLog As New ClsLogistica

        dtsPedidos = objLog.ConsultarPedidos_VE(Request.QueryString("id"), cboInstancia.SelectedValue, cboVeredicto.SelectedValue)

        If dtsPedidos.Rows.Count > 0 Then

            Me.gvPedidos.DataSource = dtsPedidos
            Me.gvPedidos.DataBind()
            Me.lblHayPedidos.Visible = False
        Else
            Me.gvPedidos.DataBind()
            Me.lblHayPedidos.Visible = True
            Me.lblHayPedidos.Text = "No hay registros."
        End If
        tipo = 1
    End Sub
    Protected Sub llenarGvPedidos_Buscar()
        Dim dtsPedidos As New Data.DataTable
        Dim objLog As New ClsLogistica

        Try

            If Me.txtPedidoBuscar.Text = "" Then
                llenarGvPedidos()
                Exit Sub
            End If

            If (IsNumeric(Trim(Me.txtPedidoBuscar.Text))) Then
                dtsPedidos = objLog.ConsultarPedidosSolicitante_V2(Request.QueryString("id"), Me.txtPedidoBuscar.Text, Me.cboVeredicto.SelectedValue, "")
            Else
                dtsPedidos = objLog.ConsultarPedidosSolicitante_V2(Request.QueryString("id"), 0, Me.cboVeredicto.SelectedValue, Me.txtPedidoBuscar.Text)

            End If

            If dtsPedidos.Rows.Count > 0 Then

                Me.gvPedidos.DataSource = dtsPedidos
                Me.gvPedidos.DataBind()
                Me.lblHayPedidos.Visible = False
            Else
                Me.gvPedidos.DataBind()
                Me.lblHayPedidos.Visible = True
                Me.lblHayPedidos.Text = "No hay registros."
            End If
            tipo = 2

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txtObservacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObservacion.TextChanged


        If gvPedidos.SelectedIndex >= 0 Then
            Me.cmdCalificar.Enabled = True
        End If

    End Sub

End Class

