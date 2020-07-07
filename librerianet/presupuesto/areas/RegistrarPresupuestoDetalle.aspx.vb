
Partial Class presupuesto_areas_RegistrarPresupuestoDetalle
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable
            'Cargar datos de usuario
            datos = objpre.ObtenerDatosUsuario(Request.QueryString("id"))
            If datos.Rows.Count > 0 Then
                lblUsuario.Text = datos.Rows(0).Item("usuario")
                lblCargo.Text = datos.Rows(0).Item("Cargo")
                lblCentroCostos.Text = datos.Rows(0).Item("CentroCostos")
            End If

            'Cargar datos de Proceso presupuestal
            If Request.QueryString("ctf") = "1" Then  'Si es administrador puede ver todos los procesos
                datos = objPre.ConsultarProcesoContable()
            Else
                Me.cboPeriodoPresu.Enabled = False
                datos = objpre.ObtenerListaProcesos() 'Solo ve el proceso activo actualmente
            End If



            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If
            datos.Dispose()

            'Cargar datos de programa presuspuestal
            objfun.CargarListas(cboProgramaPresu, objpre.ObtenerListaProgramaPresupuestal(), "codigo_ppr", "descripcion_ppr", ">> Seleccione <<")

            'Cargar Datos Centro Costos
            'datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            datos = objpre.ObtenerListaCentroCostosNuevoPresupuesto("1", 523, Request.QueryString("id"))
            'Session("datosCecos") = datos
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")
            'Dim keys(1) As Data.DataColumn
            'keys(0) = Session("datosCecos").Columns("codigo_Cco")
            'Session("datosCecos").PrimaryKey = keys
            'If cboCecos.SelectedValue > 0 Then
            '    Dim fila As Data.DataRow = Session("datosCecos").Rows.Find(cboCecos.SelectedValue)
            '    txtCecos.Text = fila.Item(1).ToString
            'End If
            datos.Dispose()

            Me.txtPrecioUnit.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & ");")
            txtCantidadAnual.Text = FormatNumber(0, 2)
            txtImporteAnual.Text = FormatNumber(0, 2)
            CargarGridMeses("Cantidad")

            If Request.QueryString("Tipo") = "E" Then ' E: editar presupuesto 
                CargarDatosEditarDetalle()
                BloquearCabecera(False)
            Else
                BloquearCabecera(True)
            End If

            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
            Panel1.Visible = False
            Panel3.Visible = False
            MostrarBusquedaCeCos(False)
        End If
    End Sub

    Private Sub BloquearCabecera(ByVal valor As Boolean)
        Me.txtComentario.Enabled = valor
        Me.cboCecos.Enabled = valor
        Me.txtCecos.Enabled = valor
        'Me.cboProgramaPresu.Enabled = valor
        'Me.cboPeriodoPresu.Enabled = valor
        Me.rblPrioridad.Enabled = valor
    End Sub

    Private Sub CargarDatosEditarDetalle()
        Dim ObjPre As New ClsPresupuesto
        Dim datos As New Data.DataTable
        datos = ObjPre.ObtenerDetallePresupuesto(Request.QueryString("field"))
        With datos.Rows(0)
            If datos.Rows.Count > 0 Then
                cboProgramaPresu.SelectedValue = .Item("codigo_ppr")
                cboCecos.SelectedValue = .Item("codigo_cco")
                txtComentarioReq.Text = .Item("comentario")
                rblPrioridad.SelectedValue = .Item("prioridad")
                rblMovimiento.SelectedValue = .Item("Tipo")
                txtConcepto.Text = .Item("Desestandar")
                txtComentarioReq.Text = .Item("detdescripcion")
                rblSubPrioridad.SelectedValue = .Item("subprioridad_dpr")
                lblUnidad.Text = .Item("unidad")
                txtPrecioUnit.Text = .Item("preunitario")
                txtCantidadAnual.Text = .Item("cantidad")
                txtImporteAnual.Text = FormatNumber(.Item("subtotal"), 2)
                txtComentario.Text = .Item("comentario")
                Me.cboPeriodoPresu.SelectedValue = .Item("codigo_ejp")
                'response.write(.Item("codigo_ejp") & "- " & Me.cboPeriodoPresu.SelectedValue)
                Dim ControlTexto As New TextBox
                Dim mes As String
                If .Item("indicoCantidades") = True Then
                    For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                        Select Case i
                            Case 1 : mes = "canene"
                            Case 2 : mes = "canfeb"
                            Case 3 : mes = "canmar"
                            Case 4 : mes = "canabr"
                            Case 5 : mes = "canmay"
                            Case 6 : mes = "canjun"
                            Case 7 : mes = "canjul"
                            Case 8 : mes = "canago"
                            Case 9 : mes = "cansep"
                            Case 10 : mes = "canoct"
                            Case 11 : mes = "cannov"
                            Case 12 : mes = "candic"
                        End Select
                        ControlTexto.Text = .Item(mes)
                    Next
                Else
                    For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                        Select Case i
                            Case 1 : mes = "preene"
                            Case 2 : mes = "prefeb"
                            Case 3 : mes = "premar"
                            Case 4 : mes = "preabr"
                            Case 5 : mes = "premay"
                            Case 6 : mes = "prejun"
                            Case 7 : mes = "prejul"
                            Case 8 : mes = "preago"
                            Case 9 : mes = "presep"
                            Case 10 : mes = "preoct"
                            Case 11 : mes = "prenov"
                            Case 12 : mes = "predic"
                        End Select
                        ControlTexto.Text = .Item(mes)
                    Next
                End If
            End If
        End With

        BuscarItems()
        gvItems.SelectedIndex = 0
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        SeleccionarItem()
    End Sub

    Private Sub CargarGridMeses(ByVal Texto As String)
        Dim Tabla As Data.DataTable
        Tabla = New Data.DataTable
        Tabla.Columns.Add(New Data.DataColumn("cantidad", GetType(String)))
        AgregarFilaGrid(Tabla, "Valores") 'Texto)
        gvDetalleEjecucion.DataSource = Tabla
        gvDetalleEjecucion.DataBind()
    End Sub

    Private Sub AgregarFilaGrid(ByRef tabla As Data.DataTable, ByVal texto As String)
        Dim fila As Data.DataRow
        fila = Tabla.NewRow()
        fila("cantidad") = texto
        tabla.Rows.Add(fila)
    End Sub

    Protected Sub gvDetalleEjecucion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleEjecucion.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i As Int16 = 1 To 12
                Dim CajaTexto As New TextBox
                Dim validador As New CompareValidator
                CajaTexto.ID = "valor" & i
                CajaTexto.Text = "0"
                CajaTexto.Width = 50
                CajaTexto.Attributes.Add("onKeyPress", "validarnumero()")
                validador.ToolTip = "Debe ingresar un valor válido"
                validador.ErrorMessage = "Debe ingresar un valor válido (>0)"
                validador.Text = "*"
                validador.ControlToValidate = CajaTexto.ID
                validador.Operator = ValidationCompareOperator.GreaterThanEqual
                validador.ValidationGroup = "Guardar"
                validador.ValueToCompare = 0

                e.Row.Cells(i).Controls.Add(CajaTexto)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
                e.Row.Cells(i).Controls.Add(validador)

                CajaTexto.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & ");")
            Next
        End If
    End Sub

    'Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
    '    Dim fila As Data.DataRow = Session("datosCecos").Rows.Find(cboCecos.SelectedValue)
    '    txtCecos.Text = fila.Item(1).ToString
    'End Sub

    Protected Sub ImgBuscarItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarItems.Click
        BuscarItems()
    End Sub

    Private Sub BuscarItems()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvItems.DataSource = objPre.ConsultarConceptos(rblMovimiento.SelectedValue, -1, -1, -1, -1, txtConcepto.Text)
        gvItems.DataBind()
        objPre = Nothing
        Panel1.Visible = True
    End Sub

    Protected Sub gvItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvItems.SelectedIndexChanged
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(1).Text)
        Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(3).Text)
        Me.lblUnidad.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(2).Text)
        gvDetalleEjecucion.Dispose()
        SeleccionarItem()
        'CargarGridMeses()
    End Sub

    Private Sub SeleccionarItem()
        If Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3).ToString.Trim() = true Then
            lblValores.Text = "Cantidad"
            'CargarGridMeses("Cantidad")
            lblTexto.Text = "Precio Unitario (S/.)"
        Else
            'gvItems.Rows(0).Cells(0).Text = "Importe"
            lblValores.Text = "Importe"
            CargarGridMeses("Importe")
            lblTexto.Text = "Cantidad"
        End If
        gvItems.Dispose()
        Panel1.Visible = False
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjPre As New ClsPresupuesto
        Dim codigoArt, codigoRub, codigoPlla As Int64
        Dim iduni, rptaCab As Integer
	Dim indicoCant as Byte
        Dim tipo As String = ""
        Dim CantidadTotal As Decimal
        Dim rpta, OK As Byte

        'Try
            If txtCodItem.Text <> "" And cboCecos.Visible = True Then
                cmdGuardar.Enabled = False
                codigoArt = 0
                codigoPlla = 0
                codigoRub = 0
                AsignarDatos(tipo, indicoCant, iduni, codigoRub, codigoArt, codigoPlla, CantidadTotal)
                VerificarTechosPresupuestales(rpta)

                If rpta = 1 Then
                    ObjPre.AbrirTransaccionCnx()

                    If Request.QueryString("tipo") = "E" Then
                        OK = ObjPre.EliminarDetallePresupuesto(CInt(Request.QueryString("field")), CInt(Request.QueryString("id")))
                        hddForzar.Value = 1
                    Else
                        OK = 1
                    End If

                    If OK = 1 Then 'Edicion, Nuevo

                        '*** Guarda cabecera de presupuesto ***
                        rptaCab = ObjPre.AgregarPresupuesto(cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, cboProgramaPresu.SelectedValue, _
                                                            txtComentario.Text, rblPrioridad.SelectedValue, CInt(codigoArt), CInt(codigoRub), CInt(codigoPlla), _
                                                            txtComentarioReq.Text, iduni, CDec(txtPrecioUnit.Text), CantidadTotal, 1, _
                                                            rblMovimiento.SelectedValue, Request.QueryString("id"), indicoCant, rblSubPrioridad.SelectedValue, hddForzar.Value)

                        '*** Muestra mensaje de retorno ***
                    Dim rptaDet As Integer
                        Select Case rptaCab
                            Case 0
                                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos');", True)
                            Case -1
                                ClientScript.RegisterStartupScript(Me.GetType, "Periodo no activo", "alert('El Ejercicio presupuestal esta cerrado actualmente, no se puede registrar el presupuesto');", True)
                            Case -2
                                ClientScript.RegisterStartupScript(Me.GetType, "Proceso no disponible", "alert('El estado actual del presupuesto, no permite que sea modificado');", True)
                            Case -3
                                ClientScript.RegisterStartupScript(Me.GetType, "Existente", "divConfirmar.style.visibility='visible'", True)
                                ObjPre.CancelarTransaccionCnx()
                                Exit Sub
                            Case Else
                                If rptaCab > 0 Then
                                    Dim ControlTexto As New TextBox
                                    Dim CantidadMes, PrecioMes As Decimal
                                    Dim sw1, sw2 As Int16
                                    sw1 = sw2 = 0
                                    Dim Meses() As String = New String() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SETIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

                                    For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                                        If Cbool(indicoCant) = true Then
                                            CantidadMes = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                                            rptaDet = ObjPre.AgregarDetalleEjecucion(rptaCab, Meses(i - 1), CDec(txtPrecioUnit.Text), CDec(CantidadMes))
                                        Else
                                            PrecioMes = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                                            rptaDet = ObjPre.AgregarDetalleEjecucion(rptaCab, Meses(i - 1), CDec(PrecioMes), 1)
                                        End If

                                        sw1 += rptaDet

                                    Next
                                    If sw1 = 0 Then
                                        ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('El proceso no está disponible');", True)
                                    ElseIf sw1 > 0 Then
                                        ObjPre.CerrarTransaccionCnx()
                                        ClientScript.RegisterStartupScript(Me.GetType, "Correcto", "alert('Se registraron correctamente los datos');", True)
                                    End If
                                End If
                        End Select
                    End If
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('No se pudo editar el registro');", True)
                End If
                CargarGridMeses("Cantidad")
                LimpiarDetalle()
                cmdGuardar.Enabled = True
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('Debe seleccionar un item existente, si desea crear un nuevo item comuniquese con Cesar Cama');", True)
            End If
        'Catch ex As Exception
        '    ObjPre.CancelarTransaccionCnx()
         '   Response.Write(ex.Message)
        '    ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        'End Try
    End Sub

    Private Sub VerificarTechosPresupuestales(ByRef rpta As Byte)
        Dim datos As New Data.DataTable
        Dim techo, total As Decimal
        Dim ObjPre As New ClsPresupuesto
        datos = ObjPre.ObtenerTechosPresupuestales(cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue)
        If datos.Rows.Count > 0 Then
            If rblMovimiento.SelectedValue = "I" Then
                techo = datos.Rows(0).Item("techoIngresos_pto")
                total = datos.Rows(0).Item("totalIngresos_Pto")
            Else
                techo = datos.Rows(0).Item("techoEgresos_pto")
                total = datos.Rows(0).Item("totalEgresos_pto")
            End If
            If techo > 0 Then
                If ((total + txtImporteAnual.Text) <= techo) Then
                    rpta = 1

                Else
                    If Request.QueryString("tipo") = "E" Then
                        ClientScript.RegisterStartupScript(Me.GetType, "TechoExedido", "alert('No se puede editar debido a que usted excede el techo presupuestal asignado');", True)
                    End If
                End If
            Else
                rpta = 1
            End If
        Else
            rpta = 1
        End If
        ObjPre = Nothing

    End Sub

    Private Sub LimpiarDetalle()
        rblMovimiento.ClearSelection()
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        lblUnidad.Text = ""
        txtCantidadAnual.Text = 0
        txtComentarioReq.Text = ""
        txtImporteAnual.Text = 0
        rblSubPrioridad.ClearSelection()
    End Sub

    Private Sub AsignarDatos(ByRef tipo As String, ByRef indicoCant As Byte, ByRef iduni As Int16, _
                             ByRef codigorub As Int64, ByRef codigoart As Int64, ByRef codigoPlla As Int64, _
                             ByRef CantidadTotal As Decimal)
        tipo = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(1).ToString.Trim()
        indicoCant =Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3)
        iduni = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(2).ToString.Trim
        If tipo = "R" Then
            codigorub = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        ElseIf tipo = "A" Then
            codigoart = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        ElseIf tipo = "P" Then
            codigoPlla = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        End If
        If indicoCant = 1 Then
            CantidadTotal = CalcularCantidadTotal()
        Else
            CantidadTotal = 1
        End If
        txtCantidadAnual.Text = CantidadTotal
        txtImporteAnual.Text = CantidadTotal * txtPrecioUnit.Text
    End Sub

    Private Function CalcularCantidadTotal() As Decimal
        Dim ControlTexto As New TextBox
        Dim CantidadTotal As Decimal = 0
        For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
            CantidadTotal += CDec(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
        Next
        Return CantidadTotal
    End Function



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
        lblUnidad.Text = ""
        txtCantidadAnual.Text = 0
        txtComentarioReq.Text = ""
        txtImporteAnual.Text = 0
        rblSubPrioridad.ClearSelection()
        CargarGridMeses("Cantidad")
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Response.Redirect("ModificarPresupuesto_V2.aspx?id=" & Request.QueryString("id") & "&cecos=" & Request.QueryString("cecos") & "&ppr=" & Request.QueryString("ppr") & "&pto=" & Request.QueryString("pto") & "&habilitado=" & Request.QueryString("habilitado") & "&ctf=" & request.QueryString("ctf"))
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
    End Sub

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        '        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisosNuevoPresupuesto(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
    End Sub
End Class
