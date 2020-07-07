Partial Class proponente_datospropuesta
    Inherits System.Web.UI.Page

    Dim qtyTotal As Decimal = 0
    Dim grQtyTotal As Decimal = 0
    Dim storid As Integer = 0
    Dim rowIndex As Integer = 1

    Dim subtotal As Decimal = 0
    Dim subtotalIngreso As Decimal = 0

    Dim PorcentajeEgreso As Decimal = 0

    Dim LastTipo As String = String.Empty
    Dim LastActividad As String = String.Empty

    Dim LastTipoIngreso As String = String.Empty
    Dim LastActividadIngreso As String = String.Empty

    Dim CurrentRow As Integer = -1
    Dim CurrentRow2 As Integer = -1

    Dim CurrentRowIngreso As Integer = -1
    Dim CurrentRow2Ingreso As Integer = -1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Request.QueryString("codigo_prp") IsNot Nothing Then
                If Not IsPostBack Then
                    Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                    Dim rsEstados As New Data.DataTable

                    rsEstados = ObjCnx.TraerDataTable("PRP_ConsultarEstadoPropuestas", Request.QueryString("codigo_prp"))
                    Me.lblEstadoFacultad.Text = rsEstados.Rows(0).Item(0)
                    Me.lblEstadoRectorado.Text = rsEstados.Rows(0).Item(1)

                    Dim rsVersiones As New Data.DataTable
                    rsVersiones = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "ES", Request.QueryString("codigo_prp"), 0)
                    ClsFunciones.LlenarListas(Me.ddlversiones, rsVersiones, "version_dap", "version_dap")
                    Call ConsultarDatos()

                    dgv_Presupuesto.Visible = False
                End If
            Else
                Me.Form.Controls.Clear()
                Response.Write("<h3>Seleccione una propuesta.</h3>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarDatos()
        Dim rsVersion As New Data.DataTable

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        rsVersion = ObjCnx.TraerDataTable("CONSULTARVERSIONESPROPUESTA", "DA", Request.QueryString("codigo_prp"), Me.ddlversiones.SelectedValue)

        Me.lblPropuesta.Text = rsVersion.Rows(0).Item("nombre_Prp").ToString
        Me.lblProponente.Text = rsVersion.Rows(0).Item("apellidoPat_Per") & " " & rsVersion.Rows(0).Item("apellidoMat_Per") & " " & rsVersion.Rows(0).Item("Nombres_Per")
        Me.lblArea.Text = rsVersion.Rows(0).Item("descripcion_cco").ToString
        Me.lblTipoPropuesta.Text = rsVersion.Rows(0).Item("Descripcion_Tpr").ToString
        Me.lblInstancia.Text = rsVersion.Rows(0).Item("instancia_Prp").ToString.ToUpper
        Me.lblResumen.Text = rsVersion.Rows(0).Item("beneficios_dap").ToString
        Me.lblImportancia.Text = rsVersion.Rows(0).Item("importancia_dap").ToString
        Me.lblSimbolo.Text = rsVersion.Rows(0).Item("descripcion_tip").ToString
        Me.lblCambio.Text = rsVersion.Rows(0).Item("tipocambio_dap").ToString
        Me.lblSim0.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblSim1.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblSim2.Text = rsVersion.Rows(0).Item("simbolo_moneda").ToString
        Me.lblIngreso.Text = rsVersion.Rows(0).Item("ingreso_dap").ToString
        Me.lblEgreso.Text = rsVersion.Rows(0).Item("egreso_dap").ToString
        Me.lblIngresoMN.Text = FormatNumber(rsVersion.Rows(0).Item("ingresoMN_dap").ToString, 2)
        Me.lblEgresoMN.Text = FormatNumber(rsVersion.Rows(0).Item("egresoMN_dap").ToString, 2)
        Me.lblUtilidad.Text = rsVersion.Rows(0).Item("utilidad_dap").ToString
        Me.lblUtilidadMN.Text = FormatNumber(rsVersion.Rows(0).Item("utilidadMN_dap").ToString, 2)
        Me.HdCodigo_acp.Value = rsVersion.Rows(0).Item("codigo_acp").ToString
        Me.HdCodigo_tpr.Value = rsVersion.Rows(0).Item("codigo_tpr").ToString

        Dim rsDatos As New Data.DataTable
        Dim codigo_dap, codigo_cop, codigo_dip As Integer
        rsDatos = ObjCnx.TraerDataTable("PRP_ConsultarDatosCodigosPropuesta", Request.QueryString("codigo_prp"), Me.ddlversiones.SelectedValue)
        If rsDatos.Rows(0).Item("codigo_dap").ToString <> "" Then
            codigo_dap = rsDatos.Rows(0).Item("codigo_dap")
            codigo_cop = rsDatos.Rows(0).Item("codigo_cop")
            codigo_dip = 0

            Call ListarArchivos(codigo_cop, codigo_dap, codigo_dip)
        End If

        Dim codigo_acp As Integer = 0
        If HdCodigo_acp.Value <> "" Then
            codigo_acp = HdCodigo_acp.Value
        End If

        ''Obtener Margen y Rentabilidad
        Dim dtMargen As New Data.DataTable
        dtMargen = ObjCnx.TraerDataTable("PRP_ObtenerMargen", codigo_acp)

        lblMargen.Text = FormatNumber(CDbl(dtMargen.Rows(0).Item("margen")))
        lblRentabilidad.Text = dtMargen.Rows(0).Item("rentabilidad").ToString
        If lblRentabilidad.Text >= 35 Then
            lblRentabilidad.ForeColor = Drawing.Color.Blue
        Else
            lblRentabilidad.ForeColor = Drawing.Color.Red
        End If

        If Me.HdCodigo_tpr.Value = 3 Or Me.HdCodigo_tpr.Value = 17 Then 'Programas
            imgPresupuesto.Visible = False
            btn_MostrarPresupuesto.Visible = False

             Me.TDMargen.Visible = False
            Me.TDRentabilidad.Visible = False
           
            Me.TDImportes.Attributes.Remove("Style")
            Me.TDImportes.Attributes.Add("Style", "display:none")

            Me.TDPresupuesto.Attributes.Remove("Style")
            Me.TDPresupuesto.Attributes.Add("Style", "display:none")
        Else
            imgPresupuesto.Visible = True
            btn_MostrarPresupuesto.Visible = True

            Me.TDMargen.Visible = True
             Me.TDRentabilidad.Visible = True
            
            Me.TDImportes.Attributes.Remove("Style")
            Me.TDImportes.Attributes.Add("Style", "display:block")

            Me.TDPresupuesto.Attributes.Remove("Style")
            Me.TDPresupuesto.Attributes.Add("Style", "display:block")
        End If
        Call wf_llenarItemPresupuesto()
    End Sub

    Private Sub ListarArchivos(ByVal codigo_cop As String, ByVal codigo_dap As String, ByVal codigo_dip As String)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Datos As Data.DataTable

        If codigo_cop <> "" Then
            Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "TO", codigo_cop)
        Else
            If codigo_dap <> "0" Then
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CP", codigo_dap)
            Else
                Datos = Obj.TraerDataTable("ConsultarArchivosPropuesta", "CI", codigo_dip)
            End If
        End If
        Me.GridView1.DataSource = Datos
        Me.GridView1.DataBind()
        Obj = Nothing
        Datos.Dispose()
        Datos = Nothing
    End Sub

    Protected Sub ddlversiones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlversiones.SelectedIndexChanged
        Call ConsultarDatos()
    End Sub

    Protected Sub imgPresupuesto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPresupuesto.Click
        Try
            dgv_Presupuesto.Visible = True
            dgv_PresupuestoIngreso.Visible = True
            Call wf_llenarItemPresupuesto()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_llenarItemPresupuesto()
        Dim codigo_acp As Integer = 0
        If HdCodigo_acp.Value <> "" Then
            codigo_acp = HdCodigo_acp.Value
        End If

        ''Llenar Grid de Items Presupuestados
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        ''Obtener Margen y Rentabilidad
        Dim dtMargen As New Data.DataTable
        dtMargen = ObjCnx.TraerDataTable("PRP_ObtenerMargen", codigo_acp)
        lblMargen.Text = FormatNumber(CDbl(dtMargen.Rows(0).Item("margen")))

        lblRentabilidad.Text = dtMargen.Rows(0).Item("rentabilidad").ToString
        If lblRentabilidad.Text >= 35 Then
            lblRentabilidad.ForeColor = Drawing.Color.Blue
        Else
            lblRentabilidad.ForeColor = Drawing.Color.Red
        End If

        Dim dtPresupuestoIngreso As New Data.DataTable
        dtPresupuestoIngreso = ObjCnx.TraerDataTable("PRP_ListaItemsPresupuestoIngreso", codigo_acp)
        Me.dgv_PresupuestoIngreso.DataSource = dtPresupuestoIngreso
        Me.dgv_PresupuestoIngreso.DataBind()

        Dim dtPresupuesto As New Data.DataTable
        dtPresupuesto = ObjCnx.TraerDataTable("PRP_ListaItemsPresupuesto", codigo_acp)
        Me.dgv_Presupuesto.DataSource = dtPresupuesto
        Me.dgv_Presupuesto.DataBind()

        If dgv_PresupuestoIngreso.Rows.Count = 0 Then
            lblMsgRentabilidad.Text = "PROYECTO NO PRESENTA RENTABILIDAD"
            lblMsgRentabilidad.Visible = True
            lblMargen.Visible = False
            lblRentabilidad.Visible = False

            lblMsgUtilidad.Text = "NO PRESENTA"
            lblMsgUtilidad.Visible = True
            lblUtilidadMN.Visible = False
        Else
            lblMsgRentabilidad.Visible = False
            lblMargen.Visible = True
            lblRentabilidad.Visible = True

            lblMsgUtilidad.Visible = False
            lblUtilidadMN.Visible = True
        End If
    End Sub

    Protected Sub dgv_Presupuesto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Presupuesto.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Crear Sub Totales en Columna de grid, revisar el evento RowCreated
            storid = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "codigo_dap"))
            Dim tmpTotal As Decimal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "subtotal"))
            qtyTotal += tmpTotal
            grQtyTotal += tmpTotal
            'Fin

            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastTipo = row("tipo") Then
                If (dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    dgv_Presupuesto.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If LastActividad = row("actividad") Then
                    If (dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan = 0) Then
                        dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan = 2
                    Else
                        dgv_Presupuesto.Rows(CurrentRow2).Cells(1).RowSpan += 1
                    End If
                    e.Row.Cells(1).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastActividad = row("actividad").ToString()
                    CurrentRow2 = e.Row.RowIndex
                End If
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastTipo = row("tipo").ToString()
                CurrentRow = e.Row.RowIndex

                e.Row.VerticalAlign = VerticalAlign.Middle
                LastActividad = row("actividad").ToString()
                CurrentRow2 = e.Row.RowIndex
            End If
            subtotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "subtotal"))
            PorcentajeEgreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "porcentaje"))

            If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                e.Row.Cells(7).Visible = False
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTALES "
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).ColumnSpan = 5
            e.Row.Cells(1).ForeColor = Drawing.Color.Red

            e.Row.Cells(2).Text = "S/. " + FormatNumber(subtotal.ToString(), 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).ForeColor = Drawing.Color.Red

            e.Row.Cells(3).Text = FormatNumber(PorcentajeEgreso.ToString(), 2)
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).ForeColor = Drawing.Color.Red

            e.Row.Font.Bold = True
            e.Row.Height = 20
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
            e.Row.Cells(7).Visible = False
            If dgv_PresupuestoIngreso.Rows.Count = 0 Then
                e.Row.Cells(3).Visible = False
            End If
        End If
    End Sub

    Protected Sub dgv_PresupuestoIngreso_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_PresupuestoIngreso.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If LastTipoIngreso = row("tipo") Then
                If (dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan = 0) Then
                    dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan = 2
                Else
                    dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False

                If LastActividadIngreso = row("actividad") Then
                    If (dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(1).RowSpan = 0) Then
                        dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(1).RowSpan = 2
                    Else
                        dgv_PresupuestoIngreso.Rows(CurrentRowIngreso).Cells(1).RowSpan += 1
                    End If
                    e.Row.Cells(1).Visible = False
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    LastActividadIngreso = row("actividad").ToString()
                    CurrentRow2Ingreso = e.Row.RowIndex
                End If
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                LastTipoIngreso = row("tipo").ToString()
                CurrentRowIngreso = e.Row.RowIndex

                e.Row.VerticalAlign = VerticalAlign.Middle
                LastActividadIngreso = row("actividad").ToString()
                CurrentRow2Ingreso = e.Row.RowIndex
            End If
            subtotalIngreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "SUBTOTAL"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTALES "
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).ColumnSpan = 5
            e.Row.Cells(1).ForeColor = Drawing.Color.Red

            e.Row.Cells(2).Text = "S/. " + FormatNumber(subtotalIngreso.ToString(), 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).ForeColor = Drawing.Color.Red

            e.Row.Font.Bold = True
            e.Row.Height = 20
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub

    Protected Sub dgv_Presupuesto_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgv_Presupuesto.RowCreated
        Dim newRow As Boolean = False
        If (storid > 0) AndAlso (DataBinder.Eval(e.Row.DataItem, "codigo_dap") IsNot Nothing) Then
            If storid <> Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "codigo_dap").ToString()) Then
                newRow = True
            End If
        End If
        If (storid > 0) AndAlso (DataBinder.Eval(e.Row.DataItem, "codigo_dap") Is Nothing) Then
            newRow = True
            rowIndex = 0
        End If
        If newRow Then
            Dim GridView1 As GridView = DirectCast(sender, GridView)
            Dim NewTotalRow As New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
            NewTotalRow.Font.Bold = True
            NewTotalRow.ForeColor = Drawing.Color.Blue

            Dim HeaderCell As New TableCell()
            HeaderCell.Text = "Sub Total"
            HeaderCell.HorizontalAlign = HorizontalAlign.Center
            HeaderCell.ColumnSpan = 5

            NewTotalRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = "S/. " + FormatNumber(CDbl(qtyTotal.ToString()))
            HeaderCell.HorizontalAlign = HorizontalAlign.Right

            NewTotalRow.Cells.Add(HeaderCell)
            HeaderCell = New TableCell()
            HeaderCell.Text = ""
            HeaderCell.HorizontalAlign = HorizontalAlign.Right

            NewTotalRow.Cells.Add(HeaderCell)
            GridView1.Controls(0).Controls.AddAt(e.Row.RowIndex + rowIndex, NewTotalRow)
            rowIndex += 1
            qtyTotal = 0
        End If
    End Sub

    Protected Sub btn_MostrarPresupuesto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_MostrarPresupuesto.Click
        Try
            dgv_Presupuesto.Visible = True
            dgv_PresupuestoIngreso.Visible = True
            Call wf_llenarItemPresupuesto()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class