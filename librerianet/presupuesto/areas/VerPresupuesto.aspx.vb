
Partial Class presupuesto_areas_VerPresupuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPre As ClsPresupuesto
            Dim datos As Data.DataTable
            objPre = New ClsPresupuesto
            datos = New Data.DataTable

            CargarDatosPresupuesto()
            CargarObservacionesPresupuesto()
            CargarIngresosPresupuesto()
            CargarEgresosPresupuesto()
            CargarGridMeses()
            lblIngMenosEgr.Text = FormatNumber(CDbl(lblTotalIngresos.Text) - CDbl(lblTotalEgresos.Text), 2)

            objPre = Nothing
        End If
    End Sub

    Protected Sub gvDetalleEjecucion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleEjecucion.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim CajaTexto As New TextBox
            Dim validador As New CompareValidator
            CajaTexto.ID = "valor2"
            CajaTexto.Text = ""
            CajaTexto.Width = 100
            CajaTexto.Attributes.Add("onBlur", "calcularsubtotal(this, " & Me.txtTotal.ID & ", " & Me.txtRegistro.ID & "); calcularvalores(" & txtTotal.ID & ", " & txtRegistro.ID & ");")
            CajaTexto.Attributes.Add("onKeyPress", "validarnumero()")
            validador.ToolTip = "Debe ingresar un valor válido"
            validador.ErrorMessage = "Debe ingresar un valor válido (>0)"
            validador.Text = "*"
            validador.ControlToValidate = CajaTexto.ID
            validador.ValidationGroup = "Guardar"
            validador.Operator = ValidationCompareOperator.GreaterThanEqual
            validador.ValueToCompare = 0
            e.Row.Cells(1).Controls.Add(CajaTexto)
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Controls.Add(validador)
        End If
    End Sub

    Private Sub CargarGridMeses()
        Dim Tabla As Data.DataTable
        Tabla = New Data.DataTable
        Tabla.Columns.Add(New Data.DataColumn("mes", GetType(String)))
        'Tabla.Columns.Add(New Data.DataColumn("registro", GetType(String)))

        AgregarFilaGrid(Tabla, "Enero")
        AgregarFilaGrid(Tabla, "Febrero")
        AgregarFilaGrid(Tabla, "Marzo")
        AgregarFilaGrid(Tabla, "Abril")
        AgregarFilaGrid(Tabla, "Mayo")
        AgregarFilaGrid(Tabla, "Junio")
        AgregarFilaGrid(Tabla, "Julio")
        AgregarFilaGrid(Tabla, "Agosto")
        AgregarFilaGrid(Tabla, "Setiembre")
        AgregarFilaGrid(Tabla, "Octubre")
        AgregarFilaGrid(Tabla, "Noviembre")
        AgregarFilaGrid(Tabla, "Diciembre")

        gvDetalleEjecucion.DataSource = Tabla
        gvDetalleEjecucion.DataBind()

    End Sub

    Private Sub AgregarFilaGrid(ByRef tabla As Data.DataTable, ByVal texto As String)
        Dim fila As Data.DataRow
        fila = Tabla.NewRow()
        'Dim txt As New TextBox
        'txt.Text = ""
        fila("mes") = texto
        'fila("registro").
        tabla.Rows.Add(fila)
    End Sub

    Private Sub CargarDatosPresupuesto()
        Dim objPre As ClsPresupuesto
        Dim datos As Data.DataTable
        objPre = New ClsPresupuesto
        datos = New Data.DataTable
        datos = objPre.ConsultarDatosPresupuesto(1, Request.QueryString("field"))
        If datos.Rows.Count > 0 Then
            With datos
                lblCodigo.Text = .Rows(0).Item("codigo_Pto")
                lblCecos.Text = .Rows(0).Item("descripcion_Cco")
                lblProceso.Text = .Rows(0).Item("descripcion_pct")
                lblFechaIni.Text = .Rows(0).Item("fechaInicio_Pto")
                lblFechaFin.Text = .Rows(0).Item("fechaFin_Pto")
                lblObservacion.Text = .Rows(0).Item("observacion_Pto")
                lblEstado.Text = .Rows(0).Item("descripcion_Epr")
                hddHabilitado.Value = .Rows(0).Item("habilitado_Pto")
                cmdNuevoEgr.Enabled = hddHabilitado.Value
                cmdNuevoIng.Enabled = hddHabilitado.Value
            End With
        End If
        datos.Dispose()
        objPre = Nothing
    End Sub

    Private Sub CargarObservacionesPresupuesto()
        Dim objPre As ClsPresupuesto
        Dim datos As Data.DataTable
        objPre = New ClsPresupuesto
        datos = New Data.DataTable
        datos = objPre.ConsultarDatosPresupuesto(2, Request.QueryString("field"))
        With datos
            If .Rows.Count > 0 Then
                gvobservaciones.DataSource = datos
            Else
                gvobservaciones.DataSource = Nothing
            End If
            gvobservaciones.DataBind()
        End With
        datos.Dispose()
        objPre = Nothing
    End Sub

    Private Sub CargarIngresosPresupuesto()
        Dim objPre As ClsPresupuesto
        Dim datos As Data.DataTable
        objPre = New ClsPresupuesto
        datos = New Data.DataTable
        datos = objPre.ConsultarDatosPresupuesto(3, Request.QueryString("field"))
        With datos
            If .Rows.Count > 0 Then
                gvIngresos.DataSource = datos
                lblTechoIngreso.Text = .Rows(0).Item("TotalTecho")
                lblTotalIngresos.Text = .Rows(0).Item("TotalPresupuestado")
            Else
                gvIngresos.DataSource = Nothing
            End If
            gvIngresos.DataBind()
        End With
        datos.Dispose()
        objPre = Nothing
    End Sub

    Private Sub CargarEgresosPresupuesto()
        Dim objPre As ClsPresupuesto
        Dim datos As Data.DataTable
        objPre = New ClsPresupuesto
        datos = New Data.DataTable
        datos = objPre.ConsultarDatosPresupuesto(4, Request.QueryString("field"))
        With datos
            If .Rows.Count > 0 Then
                gvEgresos.DataSource = datos
                lblTechoEgreso.Text = .Rows(0).Item("TotalTecho")
                lblTotalEgresos.Text = .Rows(0).Item("TotalPresupuestado")
            Else
                gvEgresos.DataSource = Nothing
            End If
            gvEgresos.DataBind()
        End With
        datos.Dispose()
        objPre = Nothing
    End Sub

    Protected Sub gvEgresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEgresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(8).Enabled = hddHabilitado.Value
            e.Row.Cells(9).Enabled = hddHabilitado.Value
            e.Row.Cells(10).Enabled = hddHabilitado.Value

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvEgresos','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
            '-------------------
            'ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"
            Dim ctrlEliminar As ImageButton = CType(e.Row.Cells(9).Controls(0), ImageButton)
            Dim ctrlEditar As ImageButton = CType(e.Row.Cells(8).Controls(0), ImageButton)
            Dim ctrl As ImageButton = CType(e.Row.Cells(10).Controls(1), ImageButton)
            If hddHabilitado.Value = True Then
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar.gif"
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = False
                e.Row.Cells(10).Visible = True
            Else
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar_d.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                ctrl.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
            End If
        End If
    End Sub

    Protected Sub gvIngresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIngresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(8).Enabled = hddHabilitado.Value
            e.Row.Cells(9).Enabled = hddHabilitado.Value
            e.Row.Cells(10).Enabled = hddHabilitado.Value

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvIngresos','Select$" & e.Row.RowIndex & "');")

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")

            Dim ctrlEliminar As ImageButton = CType(e.Row.Cells(9).Controls(0), ImageButton)
            Dim ctrlEditar As ImageButton = CType(e.Row.Cells(8).Controls(0), ImageButton)
            Dim ctrl As ImageButton = CType(e.Row.Cells(10).Controls(1), ImageButton)
            'ctrlEliminar.OnClientClick = "return confirm('¿Esta seguro de eliminar este registro?');"
            If hddHabilitado.Value = True Then
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar.gif"
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = False
                e.Row.Cells(10).Visible = True
            Else
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar_d.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                ctrl.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                e.Row.Cells(8).Visible = True
                e.Row.Cells(9).Visible = True
                e.Row.Cells(10).Visible = True
            End If
        End If
    End Sub

    Protected Sub gvIngresos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvIngresos.RowDeleting
        EliminarItem(gvIngresos.DataKeys.Item(e.RowIndex).Value, True)
        CargarIngresosPresupuesto()
        e.Cancel = True
    End Sub

    Protected Sub gvEgresos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEgresos.RowDeleting
        EliminarItem(gvEgresos.DataKeys.Item(e.RowIndex).Value, True)
        CargarEgresosPresupuesto()
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

    Protected Sub gvEgresos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvEgresos.RowEditing
        'Response.Redirect("EditarItem.aspx?field=" & gvEgresos.DataKeys.Item(e.NewEditIndex).Value & "&tipo=E&id=" & Request.QueryString("id") & "&codigo_pto=" & Request.QueryString("field") & "&techo=" & lblTechoEgreso.Text & "&total=" & lblTotalEgresos.Text & "&opcion=E")
        Dim codigo_dpr As Int64 = gvEgresos.DataKeys.Item(e.NewEditIndex).Value
        EditarItem(codigo_dpr)
        Me.mpeEditarItem.show()
        e.Cancel = True
    End Sub

    Protected Sub gvIngresos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvIngresos.RowEditing
        'Response.Redirect("EditarItem.aspx?field=" & gvIngresos.DataKeys.Item(e.NewEditIndex).Value & "&tipo=I&id=" & Request.QueryString("id") & "&codigo_pto=" & Request.QueryString("field") & "&techo=" & lblTechoIngreso.Text & "&total=" & lblTotalIngresos.Text & "&opcion=E")
        Dim codigo_dpr As Int64 = gvIngresos.DataKeys.Item(e.NewEditIndex).Value
        EditarItem(codigo_dpr)
        Me.mpeEditarItem.show()
        e.Cancel = True
    End Sub

    Protected Sub gvEgresos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEgresos.SelectedIndexChanged
        If hddHabilitado.Value = False Then
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('El proceso está bloqueado por haber finalizado la fecha limite');", True)
        End If
    End Sub

    Protected Sub cmdNuevoIng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoIng.Click
        Response.Redirect("AgregarItem.aspx?tipo=I&id=" & Request.QueryString("id") & "&codigo_pto=" & Request.QueryString("field") & "&techo=" & lblTechoIngreso.Text & "&total=" & lblTotalIngresos.Text & "&opcion=N")
    End Sub

    Protected Sub cmdNuevoEgr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevoEgr.Click
        Response.Redirect("AgregarItem.aspx?tipo=E&id=" & Request.QueryString("id") & "&codigo_pto=" & Request.QueryString("field") & "&techo=" & lblTechoEgreso.Text & "&total=" & lblTotalEgresos.Text & "&opcion=N")
    End Sub

    Protected Sub EditarItem(ByVal codigo_dpr As Int64)
        Dim datos As Data.DataTable
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto

        datos = objPre.ConsultarDetalleEjecucion(codigo_dpr)
        If datos.Rows.Count > 0 Then
            Me.lblCodigoo.Text = HttpUtility.HtmlDecode(datos.Rows(0).Item("CodItem").ToString)
            Me.lblConcepto.Text = HttpUtility.HtmlDecode(datos.Rows(0).Item("ConCepto").ToString)
            Me.txtDetalle.Text = HttpUtility.HtmlDecode(datos.Rows(0).Item("Detalle").ToString)
            Me.lblUnidad.Text = HttpUtility.HtmlDecode(datos.Rows(0).Item("Unidad").ToString)
            Me.hddcodigocon.Value = datos.Rows(0).Item("codconcepto").ToString
            Me.hddtipocon.Value = datos.Rows(0).Item("tipoconcepto").ToString
            Me.hddiduni.Value = datos.Rows(0).Item("iduni").ToString
            Me.hddtipo.Value = datos.Rows(0).Item("tipo").ToString
            Me.hddcodigoDpr.Value = codigo_dpr

            If datos.Rows(0).Item("Indicocantidades") = True Then
                AsignarCantidadyPrecio("C", "precio_Dej", "cantidad", "cantidad_Dej", datos, "Precio")
                'rblRegistro.SelectedValue = "C"
                'Me.txtRegistro.Text = datos.Rows(0).Item("Precio_Dej")
                'Me.txtTotal.Text = datos.Rows(0).Item("cantidad")

                'For i = 0 To datos.Rows.Count - 1
                '    For j = 1 To Me.gvDetalleEjecucion.Rows.Count
                '        If datos.Rows(i).Item("descripcion_Dej").ToString.Trim = Me.gvDetalleEjecucion.Rows(j - 1).Cells(0).Text.Trim Then
                '            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
                '            ControlTexto.Text = datos.Rows(i).Item("cantidad_Dej")
                '        End If
                '    Next
                'Next
            Else
                AsignarCantidadyPrecio("P", "cantidad_Dej", "preunitario", "precio_Dej", datos, "Cantidad")
                'rblRegistro.SelectedValue = "P"
                'Me.txtRegistro.Text = datos.Rows(0).Item("cantidad_Dej")
                'Me.txtTotal.Text = datos.Rows(0).Item("preunitario")

                'For i = 0 To datos.Rows.Count - 1
                '    For j = 1 To Me.gvDetalleEjecucion.Rows.Count
                '        If datos.Rows(i).Item("descripcion_Dej").ToString.Trim = Me.gvDetalleEjecucion.Rows(j - 1).Cells(0).Text.Trim Then
                '            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
                '            ControlTexto.Text = datos.Rows(i).Item("precio_Dej")
                '        End If
                '    Next
                'Next
            End If
        End If
        objPre = Nothing
    End Sub

    Private Sub AsignarCantidadyPrecio(ByVal indicocantidad As String, ByVal registro As String, _
                                       ByVal total As String, ByVal cajatexto As String, _
                                       ByRef datos As Data.DataTable, ByVal textoregistro As String)
        Dim ControlTexto As TextBox
        rblRegistro.SelectedValue = indicocantidad
        Me.txtRegistro.Text = datos.Rows(0).Item(registro)
        Me.txtTotal.Text = datos.Rows(0).Item(total)

        For i As Int16 = 0 To datos.Rows.Count - 1
            For j As Int16 = 1 To Me.gvDetalleEjecucion.Rows.Count
                If datos.Rows(i).Item("descripcion_Dej").ToString.Trim = Me.gvDetalleEjecucion.Rows(j - 1).Cells(0).Text.Trim Then
                    ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
                    ControlTexto.Text = datos.Rows(i).Item(cajatexto)
                End If
            Next
        Next
        lblRegistro.Text = textoregistro
    End Sub

    'Protected Sub cmdTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTotal.Click
    '    Dim ControlTexto As TextBox
    '    Dim total As Double = 0
    '    For j As Int16 = 1 To Me.gvDetalleEjecucion.Rows.Count
    '        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
    '        If ControlTexto.Text.Trim = "" Then
    '            total = total + 0.0
    '        Else
    '            total = total + CDbl(ControlTexto.Text)
    '        End If
    '    Next
    'End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim precio, cantidad, techo, total As Double
        Dim indicocantidad As Int16 = IIf(rblRegistro.SelectedValue = "C", 1, 0)

        If indicocantidad = 1 Then
            precio = CDbl(txtRegistro.Text)
            cantidad = CDbl(txtTotal.Text)
        Else
            precio = CDbl(txtTotal.Text)
            cantidad = CDbl(txtRegistro.Text)
        End If

        If hddtipo.Value.Trim = "I" Then
            techo = CDbl(lblTechoIngreso.Text)
            total = CDbl(lblTotalIngresos.Text)
            EliminarItem(hddcodigoDpr.Value, False)
        Else
            techo = CDbl(lblTechoEgreso.Text)
            total = CDbl(lblTotalEgresos.Text)
            EliminarItem(hddcodigoDpr.Value, False)
        End If

        If (techo > 0) Then
            If ((total + (precio * cantidad)) <= techo) Then
                GrabarPresupuesto(precio, cantidad, indicocantidad)
            End If
        Else
            GrabarPresupuesto(precio, cantidad, indicocantidad)
        End If
        CargarIngresosPresupuesto()
        CargarEgresosPresupuesto()
    End Sub

    Private Sub GrabarPresupuesto(ByVal precio As Double, ByVal cantidad As Double, ByVal indicocantidad As Int16)
        Dim tipo As String = hddtipocon.Value
        Dim iduni As Int64 = hddiduni.Value
        Dim idper As Int64 = Request.QueryString("id")
        Dim codigo As Int64 = hddcodigocon.Value
        Dim rpta1 As Int64
        Dim rpta2, sw As Int16
        Dim ControlTexto As TextBox
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        tipo = tipo.Trim
        sw = 0
        Try
            'ObjCnx.IniciarTransaccion()
            '*** GRABA LA CABECERA DE PRESUPUESTO SEGÚN EL TIPO ***
            If tipo = "A" Then ' ARTICULO
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("field")), codigo, 0, 0, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, hddtipo.Value, idper, indicocantidad, rpta1)
            ElseIf tipo = "R" Then ' RUBRO
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("field")), 0, codigo, 0, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, hddtipo.Value, idper, indicocantidad, rpta1)
            ElseIf tipo = "P" Then ' PLANILLA
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("field")), 0, 0, codigo, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, hddtipo.Value, idper, indicocantidad, rpta1)
            End If
            '*** GRABA EL DETALLE DE PRESUPUESTO ***
            If rpta1 > 0 Then
                For i As Int16 = 1 To gvDetalleEjecucion.Rows.Count
                    If sw = 0 Then
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(i).Controls(1).Controls(0)
                        If indicocantidad = 1 Then
                            cantidad = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                        Else
                            precio = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                        End If

                        rpta2 = ObjCnx.Ejecutar("PRESU_AgregarDetalleEjecucion", rpta1, gvDetalleEjecucion.Rows(i - 1).Cells(0).Text, precio, cantidad, rpta2)
                        If rpta2 = 0 Then
                            sw = 1
                        End If
                    End If

                Next
                If sw = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Se registraron los datos correctamente')", True)
                    'ObjCnx.TerminarTransaccion()
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('El proceso no está habilitado, no se pudo registrar los datos de presupuesto')", True)
                    'ObjCnx.AbortarTransaccion()
                End If

            ElseIf rpta1 = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('El proceso no está habilitado, no se pudo registrar los datos de presupuesto')", True)
                'ObjCnx.AbortarTransaccion()
                
            End If
            ObjCnx = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al grabar los datos')", True)
            'ObjCnx.AbortarTransaccion()
        End Try
    End Sub

    Protected Sub rblRegistro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblRegistro.SelectedIndexChanged
        If rblRegistro.SelectedValue = "C" Then
            lblRegistro.Text = "Importe:"
            txtRegistro.Enabled = True
            txtRegistro.Text = ""
            Me.gvDetalleEjecucion.HeaderRow.Cells(1).Text = "Cantidad"
        Else
            lblRegistro.Text = "Cantidad:"
            txtRegistro.Enabled = False
            txtRegistro.Text = 1
            Me.gvDetalleEjecucion.HeaderRow.Cells(1).Text = "Importe"
        End If
    End Sub

    Protected Sub gvIngresos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvIngresos.SelectedIndexChanged

    End Sub

    Protected Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click

    End Sub
End Class
