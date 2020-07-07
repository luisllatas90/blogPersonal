Partial Class presupuesto_areas_AgregarItem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objFun = New ClsFunciones
            objPre = New ClsPresupuesto
            objFun.CargarListas(cboClase, objPre.ObtenerListaClase("DES", ""), "codigo_Cla", "descripcion_Cla", "-- Todos --")
            LimpiarTodos()
            cboClase_SelectedIndexChanged(sender, e)
            Me.gvItems.HeaderRow.Cells(5).Visible = False
            Me.gvItems.HeaderRow.Cells(6).Visible = False
            Me.gvItems.HeaderRow.Cells(7).Visible = False
            CargarGridMeses()
            Me.txtTotal.Text = 0
            objPre = Nothing
            objFun = Nothing
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

    Private Sub LimpiarTodos()
        LimpiarLinea()
        LimpiarFamilia()
        LimpiarSubFamilia()
    End Sub

    Private Sub LimpiarLinea()
        CboLinea.Items.Clear()
        CboLinea.Items.Add("-- Todos --")
        CboLinea.Items(0).Value = -1
    End Sub

    Private Sub LimpiarFamilia()
        CboFamilia.Items.Clear()
        CboFamilia.Items.Add("-- Todos --")
        CboFamilia.Items(0).Value = -1
    End Sub

    Private Sub LimpiarSubFamilia()
        cboSubFamilia.Items.Clear()
        cboSubFamilia.Items.Add("-- Todos --")
        cboSubFamilia.Items(0).Value = -1
    End Sub

    Protected Sub cboClase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClase.SelectedIndexChanged
        If cboClase.SelectedValue = -1 Then
            LimpiarTodos()
        Else
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objFun = New ClsFunciones
            objPre = New ClsPresupuesto
            objFun.CargarListas(CboLinea, objPre.ObtenerListaLinea("XCLA", cboClase.SelectedValue.ToString), "codigo_Lin", "descripcion_Lin", "-- Todos --")
        End If
    End Sub

    Protected Sub CboLinea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboLinea.SelectedIndexChanged
        If CboLinea.SelectedValue = -1 Then
            LimpiarFamilia()
            LimpiarSubFamilia()
        Else
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objFun = New ClsFunciones
            objPre = New ClsPresupuesto
            objFun.CargarListas(CboFamilia, objPre.ObtenerListaFamilia("XCLALIN", cboClase.SelectedValue.ToString, CboLinea.SelectedValue.ToString), "codigo_Fam", "descripcion_Fam", "-- Todos --")
        End If
    End Sub

    Protected Sub CboFamilia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboFamilia.SelectedIndexChanged
        If CboFamilia.SelectedValue = -1 Then
            LimpiarSubFamilia()
        Else
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objFun = New ClsFunciones
            objPre = New ClsPresupuesto
            objFun.CargarListas(cboSubFamilia, objPre.ObtenerListaSubFamilia("XCLALINFAM", cboClase.SelectedValue.ToString, CboLinea.SelectedValue.ToString, CboFamilia.SelectedValue.ToString), "codigo_Sfa", "descripcion_Sfa", "-- Todos --")
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        'Dim objPre As ClsPresupuesto
        'objPre = New ClsPresupuesto
        'Response.Write(Request.QueryString("tipo") & " " & cboClase.SelectedValue & " " & CboLinea.SelectedValue & " " & CboFamilia.SelectedValue & " " & cboSubFamilia.SelectedValue & " " & txtConcepto.Text)
        'gvItems.DataSource = objPre.ConsultarConceptos(Request.QueryString("tipo"), cboClase.SelectedValue, CboLinea.SelectedValue, CboFamilia.SelectedValue, cboSubFamilia.SelectedValue, txtConcepto.Text)
        'gvItems.DataBind()
        'Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'gvItems.DataSource = ObjCnx.TraerDataTable("PRESU_ConsultarConceptoIngreso", -2, -2, -2, -2, "")
        gvItems.DataBind()
    End Sub


    Protected Sub gvItems_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItems.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            'fila = e.Row.DataItem
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvItems','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvItems.SelectedIndexChanged
        Dim ControlTexto As TextBox
        Dim tipo As String = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(1).ToString.Trim
        With gvItems
            Me.lblCodigo.Text = HttpUtility.HtmlDecode(.SelectedRow.Cells(1).Text)
            Me.lblConcepto.Text = HttpUtility.HtmlDecode(.SelectedRow.Cells(2).Text)
            Me.lblUnidad.Text = HttpUtility.HtmlDecode(.SelectedRow.Cells(3).Text)
            Me.txtRegistro.Text = HttpUtility.HtmlDecode(.SelectedRow.Cells(4).Text)
        End With
        For j As Int16 = 1 To Me.gvDetalleEjecucion.Rows.Count

            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
            ControlTexto.Text = ""
        Next

    End Sub

    Protected Sub rblRegistro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblRegistro.SelectedIndexChanged
        If rblRegistro.SelectedValue = "C" Then
            lblRegistro.Text = "Precio Unitario:"
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
            validador.Operator = ValidationCompareOperator.GreaterThanEqual
            validador.ValidationGroup = "Guardar"
            validador.ValueToCompare = 0
            e.Row.Cells(1).Controls.Add(CajaTexto)
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).Controls.Add(validador)
        End If
    End Sub

    Protected Sub cmdTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTotal.Click
        Dim ControlTexto As TextBox
        Dim total As Double = 0
        For j As Int16 = 1 To Me.gvDetalleEjecucion.Rows.Count
            'For i As Int16 = 0 To Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls.Count - 1
            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(j).Controls(1).Controls(0)
            If ControlTexto.Text.Trim = "" Then
                total = total + 0.0
            Else
                total = total + CDbl(ControlTexto.Text)
            End If
            'Next
        Next
        txtTotal.Text = total
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim precio, cantidad As Double
        Dim indicocantidad As Int16 = IIf(rblRegistro.SelectedValue = "C", 1, 0)
        If Request.QueryString("opcion") = "N" Then
            If indicocantidad = 1 Then
                precio = CDbl(txtRegistro.Text)
                cantidad = CDbl(txtTotal.Text)
            Else
                precio = CDbl(txtTotal.Text)
                cantidad = CDbl(txtRegistro.Text)
            End If
            If (CDbl(Request.QueryString("techo")) > 0) Then
                If ((CDbl(Request.QueryString("total")) + (precio * cantidad)) <= CDbl(Request.QueryString("techo"))) Then
                    GrabarPresupuesto(precio, cantidad, indicocantidad)
                End If
            Else
                GrabarPresupuesto(precio, cantidad, indicocantidad)
            End If
        End If
    End Sub

    Private Sub GrabarPresupuesto(ByVal precio As Double, ByVal cantidad As Double, ByVal indicocantidad As Int16)
        Dim tipo As String = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(1).ToString
        Dim iduni As Int64 = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(2).ToString
        Dim idper As Int64 = Request.QueryString("id")
        Dim codigo As Int64 = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString
        Dim rpta1 As Int64
        Dim rpta2, sw As Int16
        Dim ControlTexto As TextBox
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        tipo = tipo.Trim
        sw = 0
        Try
            ObjCnx.IniciarTransaccion()
            '*** GRABA LA CABECERA DE PRESUPUESTO SEGÚN EL TIPO ***
            If tipo = "A" Then ' ARTICULO
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("codigo_pto")), codigo, 0, 0, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, Request.QueryString("tipo"), idper, indicocantidad, rpta1)
            ElseIf tipo = "R" Then ' RUBRO
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("codigo_pto")), 0, codigo, 0, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, Request.QueryString("tipo"), idper, indicocantidad, rpta1)
            ElseIf tipo = "P" Then ' PLANILLA
                rpta1 = ObjCnx.Ejecutar("PRESU_AgregarDetallePresupuesto", CInt(Request.QueryString("codigo_pto")), 0, 0, codigo, txtDetalle.Text, iduni, _
                                                 precio, cantidad, 1, Request.QueryString("tipo"), idper, indicocantidad, rpta1)
            End If

            If rpta1 > 0 Then
                For i As Int16 = 1 To gvDetalleEjecucion.Rows.Count

                    If sw = 0 Then
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(i).Controls(1).Controls(0)
                        If indicocantidad = 1 Then
                            cantidad = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                        Else
                            precio = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                        End If
                        'Response.Write(rpta1 & ", " & gvDetalleEjecucion.Rows(i - 1).Cells(0).Text & ", " & precio & ", " & cantidad & " = ")
                        rpta2 = ObjCnx.Ejecutar("PRESU_AgregarDetalleEjecucion", rpta1, gvDetalleEjecucion.Rows(i - 1).Cells(0).Text, precio, cantidad, rpta2)
                        If rpta2 = 0 Then
                            sw = 1
                        End If
                    End If

                Next
                If sw = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Se registraron los datos correctamente')", True)
                    ObjCnx.TerminarTransaccion()
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('El proceso no está habilitado, no se pudo registrar los datos de presupuesto')", True)
                    ObjCnx.AbortarTransaccion()
                End If

            ElseIf rpta1 = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "no habilitado", "alert('El proceso no está habilitado, no se pudo registrar los datos de presupuesto')", True)
                ObjCnx.AbortarTransaccion()
            End If
        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al grabar los datos')", True)

        End Try
    End Sub

    Protected Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click
        Response.Redirect("VerPresupuesto.aspx?field=" & Request.QueryString("codigo_pto") & "&id=" & Request.QueryString("id"))
    End Sub
End Class
