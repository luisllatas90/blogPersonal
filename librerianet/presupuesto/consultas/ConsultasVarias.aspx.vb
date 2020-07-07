
Partial Class presupuesto_consultas_ConsultasVarias
    Inherits System.Web.UI.Page
    Dim total, totalIng, totalEgr As Double

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
        total = 0
        totalIng = 0
        totalEgr = 0

    End Sub


    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        Select Case cboOpcion.SelectedValue
            Case 1
                ResumenTotalXProgramaPresupuestal(sender, e)
            Case 2
                ResumenMensualXProgramaPresupuestal(sender, e)
            Case 3
                ResumenTotalXCuentaContable(sender, e)
            Case 4
                ResumenMensualXCuentaContable(sender, e)
            Case 5
                ResumenDetallado(sender, e)
        End Select
    End Sub

    Private Sub ResumenDetallado(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Text = "Centro de Costos"
            e.Row.Cells(3).Text = "Número Cta"
            e.Row.Cells(4).Text = "Cuenta Contable"
            e.Row.Cells(5).Text = "Cod. Presu."
            e.Row.Cells(6).Text = "Desc. de Presupuesto"
            e.Row.Cells(8).Text = "Cod. Item"
            e.Row.Cells(9).Text = "Desc. Estandar"
            e.Row.Cells(10).Text = "Desc. Detallada"
            e.Row.Cells(12).Text = "P. U."
            e.Row.Cells(13).Text = "Cantidad"
            e.Row.Cells(14).Text = "Sub Total"
            For i As Int16 = 14 To 26
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "<BR> (S/.)"
            Next
            'For i As Int16 = 12 To 26
            '    e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
            '    e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
            'Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).Visible = False
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Center
            For i As Int16 = 12 To 26
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
            Next

            If e.Row.Cells(0).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(14).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(14).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 12
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 13
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(13).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(13).Font.Bold = True
            e.Row.Cells(14).Visible = False
        End If
    End Sub

    Private Sub ResumenTotalXCuentaContable(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = "Número Cta"
            e.Row.Cells(2).Text = "Cuenta Contable"
            e.Row.Cells(3).Text = "Total (S/.)"

        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = FormatNumber(e.Row.Cells(3).Text, 2)
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            If e.Row.Cells(0).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(3).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(3).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 2
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 3
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(3).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Font.Bold = True
        End If
    End Sub

    Private Sub ResumenMensualXCuentaContable(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim celda As New TableCell
        e.Row.Cells.Add(celda)
        total = 0
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = "Número Cta"
            e.Row.Cells(2).Text = "Cuenta Contable"
            e.Row.Cells(15).Text = "Total (S/.)"
            e.Row.Cells(15).Font.Bold = True
            For i As Int16 = 3 To 14
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "<BR> (S/.)"
            Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            For i As Int16 = 3 To 14
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
                total = total + e.Row.Cells(i).Text
            Next
            e.Row.Cells(15).Text = total
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(15).Text = FormatNumber(e.Row.Cells(15).Text, 2)
            If e.Row.Cells(0).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(15).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(15).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 14
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 15
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(15).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(15).Font.Bold = True
        End If
    End Sub

    Private Sub ResumenTotalXProgramaPresupuestal(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = "Codigo Prog. Pres"
            e.Row.Cells(2).Text = "Desc. Programa Presupuestal"
            e.Row.Cells(3).Text = "Total (S/.)"
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(3).Text = FormatNumber(e.Row.Cells(3).Text, 2)
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            If e.Row.Cells(0).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(3).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(3).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 2
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 3
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(3).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(3).Font.Bold = True
        End If
    End Sub

    Private Sub ResumenMensualXProgramaPresupuestal(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Dim celda As New TableCell
        e.Row.Cells.Add(celda)
        total = 0
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Text = "Codigo Prog. Pres"
            e.Row.Cells(2).Text = "Desc. Programa Presupuestal"
            e.Row.Cells(15).Text = "Total (S/.)"
            e.Row.Cells(15).Font.Bold = True
            For i As Int16 = 3 To 14
                e.Row.Cells(i).Text = e.Row.Cells(i).Text & "<BR> (S/.)"
            Next
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Center
            For i As Int16 = 3 To 14
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
                total = total + e.Row.Cells(i).Text ' Suma Enero a Diciembre
            Next
            e.Row.Cells(15).Text = total
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(15).Text = FormatNumber(e.Row.Cells(15).Text, 2)
            If e.Row.Cells(0).Text = "I" Then ' I: Ingresos
                totalIng = totalIng + e.Row.Cells(15).Text
            Else ' E: egresos
                totalEgr = totalEgr + e.Row.Cells(15).Text
            End If
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            For i As Int16 = 1 To 14
                e.Row.Cells(i).Visible = False
            Next
            e.Row.Cells(0).ColumnSpan = 15
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Font.Bold = True
            e.Row.Cells(0).Text = HttpUtility.HtmlDecode("Total de Ingresos (S/.)<br> Total de Egresos (S/.)<br> Diferencia (S/.)")
            e.Row.Cells(15).Text = HttpUtility.HtmlDecode(FormatNumber(totalIng, 2) & "<br>" & FormatNumber(totalEgr, 2) & "<br>" & FormatNumber(totalIng - totalEgr, 2))
            e.Row.Cells(15).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(15).Font.Bold = True
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


End Class
