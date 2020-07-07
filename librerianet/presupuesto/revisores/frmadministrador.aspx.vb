
Partial Class frmadministrador
    Inherits System.Web.UI.Page
    Dim PrecioUnitario As Double
    Dim Cantidad As Integer
    Dim SubTotal As Double
    Dim PrecioUnitarioE As Double
    Dim CantidadE As Integer
    Dim SubTotalE As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objPresup As New ClsPresupuesto
            Dim objAprob As New clsAprobarPresupuesto
            Dim objlista As New ClsFunciones

            objlista.CargarListas(dpProceso, objPresup.ConsultarProcesoContable(), "codigo_ejp", "descripcion_ejp")
            objlista.CargarListas(Me.dpEstado, objPresup.ObtenerListaEstados(1, 0), "codigo_epr", "descripcion_epr", "--Todos--")
            objlista.CargarListas(Me.dpEstadoPresupuesto, objPresup.ObtenerListaEstados(2, 2), "codigo_epr", "descripcion_epr")

            objlista = Nothing
            objPresup = Nothing

            'Desactivar
            Me.cmdIniciar.Attributes.Add("disabled", "true")
            Me.txtTechoIngresos.Attributes.Add("onkeypress", "validarnumero()")
            Me.txtTechoEgresos.Attributes.Add("onkeypress", "validarnumero()")
            Me.chkHabilitarEstado.Attributes.Add("onclick", "HabilitarAcciones(this,document.getElementById('dpEstadoPresupuesto'))")
            Me.chkHabilitarIngreso.Attributes.Add("onclick", "HabilitarAcciones(this,document.getElementById('txtTechoIngresos'))")
            Me.chkHabilitarEgreso.Attributes.Add("onclick", "HabilitarAcciones(this,document.getElementById('txtTechoEgresos'))")
            Me.chkHabilitarComentario.Attributes.Add("onclick", "HabilitarAcciones(this,document.getElementById('txtComentarios'))")
            'Me.PanelDetalle.Attributes.Add("Style", "visibility:hidden")
        End If
    End Sub

    Protected Sub grwPresupuestos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPresupuestos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim chk As CheckBox = CType(e.Row.FindControl("chkElegir"), CheckBox)
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            Select Case Me.dpEstado.SelectedValue
                Case 1, 3 'Formulacion y Reformulación, debe volver a "INICIAR REVISION"
                    chk.Visible = True
                    chk.Attributes.Add("OnClick", "HabilitarEnvio(this,document.getElementById('cmdIniciar'))")
                Case 2 'Revisión, debe pasar directo a habilitar botón "REVISAR PRESUPUESTO"
                    chk.Visible = True
                    chk.Attributes.Add("OnClick", "HabilitarEnvio(this,document.getElementById('cmdRevisar'))")
                Case Else
                    chk.Visible = False
            End Select
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            'Diferencia de Techo ingresos
            e.Row.Cells(7).Text = FormatNumber(CDbl(fila("totalIngresos")) - CDbl(fila("techoIngresos")), 2)
            e.Row.Cells(10).Text = FormatNumber(CDbl(fila("totalEgresos")) - CDbl(fila("techoEgresos")), 2)
        End If
        If e.Row.RowType = DataControlRowType.Header Then
            Select Case Me.dpEstado.SelectedValue
                Case 1
                    CType(e.Row.FindControl("chkHeader"), CheckBox).Attributes.Add("onclick", "MarcarTodo(this.checked, cmdIniciar)")
                    CType(e.Row.FindControl("chkHeader"), CheckBox).Visible = True
                Case 2
                    CType(e.Row.FindControl("chkHeader"), CheckBox).Attributes.Add("onclick", "MarcarTodo(this.checked, cmdRevisar)")
                    CType(e.Row.FindControl("chkHeader"), CheckBox).Visible = True
                Case Else
                    CType(e.Row.FindControl("chkHeader"), CheckBox).Visible = False

            End Select
        End If
    End Sub
    Protected Sub cmdIniciar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdIniciar.Click
        'Cambiar de estado a los presupuestos Marcados.
        Dim id As String = Request.QueryString("id")
        If Me.grwPresupuestos.Rows.Count > 0 Then

            Dim Fila As GridViewRow
            Dim obj As New clsAprobarPresupuesto
            Try
                For Each Fila In Me.grwPresupuestos.Rows
                    Dim chk As CheckBox = Fila.FindControl("chkElegir")
                    If chk.Checked = True Then
                        obj.RevisarPresupuesto(Me.grwPresupuestos.DataKeys.Item(Fila.RowIndex).Value, 2, -1, -1, "", id)
                    End If
                Next
                obj = Nothing
                'Cambiar de estado
                Me.dpEstado.SelectedValue = 2
                Me.grwPresupuestos.DataBind()
                Me.cmdRevisar.Enabled = Me.grwPresupuestos.Rows.Count > 0
            Catch ex As Exception
                obj = Nothing
                Me.cmdRevisar.Enabled = False
            End Try
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim Fila As GridViewRow
        Dim obj As New clsAprobarPresupuesto
        Dim id As String = Request.QueryString("id")
        'Try
        For Each Fila In Me.grwPresupuestos.Rows
            Dim chk As CheckBox = Fila.FindControl("chkElegir")
            If chk.Checked = True And (Me.chkHabilitarEstado.Checked = True Or _
                                Me.chkHabilitarIngreso.Checked = True Or _
                                Me.chkHabilitarEgreso.Checked = True Or _
                                Me.chkHabilitarComentario.Checked = True) Then
                Dim TechoIngresos, TechoEgresos As Double
                Dim Estado, Comentarios As String

                Estado = IIf(Me.chkHabilitarEstado.Checked = False, -1, Me.dpEstadoPresupuesto.SelectedValue)
                TechoIngresos = IIf(Me.chkHabilitarIngreso.Checked = False, -1, Me.txtTechoIngresos.Text.Trim)
                TechoEgresos = IIf(Me.chkHabilitarEgreso.Checked = False, -1, Me.txtTechoEgresos.Text.Trim)
                Comentarios = IIf(Me.chkHabilitarComentario.Checked = False, "", Me.txtComentarios.Text.Trim)

                obj.RevisarPresupuesto(Me.grwPresupuestos.DataKeys.Item(Fila.RowIndex).Value, Estado, TechoIngresos, TechoEgresos, Comentarios, id)
            End If
        Next
        obj = Nothing

        'Limpiar valores y actualizar GRID
        Me.txtTechoIngresos.Text = ""
        Me.txtTechoEgresos.Text = ""
        Me.grwPresupuestos.DataBind()

        'Catch ex As Exception
        '    obj = Nothing
        '    'Me.cmdRevisar.Enabled = False
        'End Try
    End Sub
    Protected Sub lnkCentroCostos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDetalle.Click
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim codigo_pto As Integer = Convert.ToInt32(Me.grwPresupuestos.DataKeys(gvr.RowIndex).Values("codigo_Pto"))
        Dim codigo_cco As Integer = Convert.ToInt32(Me.grwPresupuestos.DataKeys(gvr.RowIndex).Values("codigo_cco"))

        'Cargar Datos del Presupuesto
        Me.lblcentrocosto.Text = CType(Me.grwPresupuestos.Rows(gvr.RowIndex).Cells(0).FindControl("lnkCentroCostos"), LinkButton).Text
        Me.lbldirector.Text = Me.grwPresupuestos.Rows(gvr.RowIndex).Cells(3).Text
        Me.lblestado.Text = Me.grwPresupuestos.Rows(gvr.RowIndex).Cells(4).Text
        Me.lblTechoIngresos.Text = Me.grwPresupuestos.Rows(gvr.RowIndex).Cells(6).Text
        Me.lblTechoEgresos.Text = Me.grwPresupuestos.Rows(gvr.RowIndex).Cells(9).Text

        Dim obj As New ClsPresupuesto

        'Cargar Observaciones
        Me.grwobservaciones.DataSource = obj.ConsultarDatosPresupuesto(2, codigo_pto)
        Me.grwobservaciones.DataBind()

        'Cargar Ingresos
        Me.grwDetallePresupuestoIngresos.DataSource = obj.ConsultarDetallePresupuesto(Me.dpProceso.SelectedValue, codigo_cco, "I")
        Me.grwDetallePresupuestoIngresos.DataBind()

        'Cargar Egresos
        Me.grwDetallePresupuestoEgresos.DataSource = obj.ConsultarDetallePresupuesto(Me.dpProceso.SelectedValue, codigo_cco, "E")
        Me.grwDetallePresupuestoEgresos.DataBind()

        obj = Nothing

        Me.mpeDetalle.Show()
    End Sub
    Protected Sub grwDetallePresupuestoIngresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwDetallePresupuestoIngresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Text = FormatNumber(e.Row.Cells(5).Text, 2)
            e.Row.Cells(6).Text = FormatNumber(e.Row.Cells(6).Text, 2)
            e.Row.Cells(7).Text = FormatNumber(e.Row.Cells(7).Text, 2)

            PrecioUnitario += CDbl(fila.Item("Precio Unit."))
            Cantidad += fila.Item("Cantidad")
            SubTotal += CDbl(fila.Item("subTotal"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = "TOTAL"
            e.Row.Cells(5).Text = FormatNumber(PrecioUnitario, 2)
            e.Row.Cells(6).Text = FormatNumber(Cantidad, 2)
            e.Row.Cells(7).Text = FormatNumber(SubTotal, 2)
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            PrecioUnitario = 0
            Cantidad = 0
            SubTotal = 0
        End If
    End Sub
    Protected Sub grwDetallePresupuestoEgresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwDetallePresupuestoEgresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Text = FormatNumber(e.Row.Cells(5).Text, 2)
            e.Row.Cells(6).Text = FormatNumber(e.Row.Cells(6).Text, 2)
            e.Row.Cells(7).Text = FormatNumber(e.Row.Cells(7).Text, 2)

            PrecioUnitarioE += CDbl(fila.Item("Precio Unit."))
            CantidadE += fila.Item("Cantidad")
            SubTotalE += CDbl(fila.Item("subTotal"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(4).Text = "TOTAL"
            e.Row.Cells(5).Text = FormatNumber(PrecioUnitarioE, 2)
            e.Row.Cells(6).Text = FormatNumber(CantidadE, 2)
            e.Row.Cells(7).Text = FormatNumber(SubTotalE, 2)
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            PrecioUnitarioE = 0
            CantidadE = 0
            SubTotalE = 0
        End If
    End Sub
End Class
