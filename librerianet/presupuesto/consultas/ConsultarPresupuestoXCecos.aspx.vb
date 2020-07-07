
Partial Class presupuesto_consultas_ConsultarPresupuestoXCecos
    Inherits System.Web.UI.Page
    Dim total, totalIng, totalEgr As Double

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        Dim datos As New Data.DataTable
        Dim Celda As New Data.DataColumn
        total = 0
        totalIng = 0
        totalEgr = 0
        datos = objPre.ConsultarPresupuestoPorOpciones(cboOpcion.SelectedValue, cboProceso.SelectedValue, cboCentroCostos.SelectedValue)
        If datos.Rows.Count > 0 Then
            gvDetalle.DataSource = datos
        Else
            gvDetalle.DataSource = Nothing
        End If
        gvDetalle.DataBind()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objPre = New ClsPresupuesto
            objFun = New ClsFunciones

            'If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 6 Then
            objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id")), "codigo_cco", "descripcion_cco", "-- Todos --")
            'Else
            '    objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("CP", Request.QueryString("id")), "codigo_cco", "descripcion_cco")
            'End If
            objFun.CargarListas(cboProceso, objPre.ConsultarProcesoContable(), "codigo_ejp", "descripcion_ejp")
            total = 0
            totalIng = 0
            totalEgr = 0
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        cmdConsultar_Click(sender, e)
        Axls("ReportePresupuesto", gvDetalle, "Reporte: " & cboOpcion.SelectedItem.Text, "Sistema de Presupuesto - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        gvDetalle.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        gvDetalle.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(gvDetalle, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        Select Case cboOpcion.SelectedValue
            Case 6
                ResumenGeneralXCentroCostos(sender, e)
            Case 7
                ResumenMensualXCentroCostos(sender, e)
        End Select
    End Sub

    Private Sub ResumenGeneralXCentroCostos(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        total = 0
        Dim celda As New TableCell
        e.Row.Cells.Add(celda)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Text = "Centro de costos"
            e.Row.Cells(2).Text = "Cod. Presu."
            e.Row.Cells(3).Text = "Desc. Prog. Presupuestal"
            For i As Int16 = 4 To 16
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "<BR> (S/.)"
            Next
            e.Row.Cells(16).Text = "Sub Total <BR> (S/.)"
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Center
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right

            For i As Int16 = 4 To 15
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
                total = total + e.Row.Cells(i).Text
            Next
            e.Row.Cells(16).Text = total
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(16).Text = FormatNumber(e.Row.Cells(16).Text, 2)
            If e.Row.Cells(1).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(16).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(16).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 15
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 16
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(16).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(16).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(16).Font.Bold = True
        End If
    End Sub

    Private Sub ResumenMensualXCentroCostos(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Text = "Centro de Costos"
            e.Row.Cells(3).Text = "Número Cta"
            e.Row.Cells(4).Text = "Cuenta Contable"
            e.Row.Cells(6).Text = "Cod. Item"
            e.Row.Cells(7).Text = "Desc. Item"
            e.Row.Cells(8).Text = "Desc. Detallada"
            e.Row.Cells(10).Text = "P.U."
            e.Row.Cells(12).Text = "Sub Total"
            For i As Int16 = 12 To 24
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "<BR> (S/.)"
            Next
            For i As Int16 = 0 To 24
                e.Row.Cells(i).VerticalAlign = VerticalAlign.Top
            Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Center

            For i As Int16 = 10 To 24
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
                total = total + e.Row.Cells(i).Text
            Next

            If e.Row.Cells(2).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(12).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(12).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 10
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 11
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(11).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(11).Font.Bold = True
            e.Row.Cells(12).Visible = False
        End If
    End Sub
End Class
