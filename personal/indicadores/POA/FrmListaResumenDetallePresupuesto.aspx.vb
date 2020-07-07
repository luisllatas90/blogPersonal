﻿
Partial Class indicadores_POA_FrmListaEvaluarPresupuesto
    Inherits System.Web.UI.Page
    Dim Ingreso As Decimal = 0
    Dim Egreso As Decimal = 0


    Sub cargarDetalle(ByVal codigo_acp As Integer)
        Dim obj As New clsPlanOperativoAnual
        Dim dttDetalle As New Data.DataTable
        gvDetalle.DataSource = Nothing
        gvDetalle.DataSource = obj.POA_ListarDetalleActividad(codigo_acp)
        gvDetalle.DataBind()
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            Dim codigo_acp As Integer
            Me.nombre_progproy.Text = Request.QueryString("resumen_acp")
            codigo_acp = Request.QueryString("codigo_acp")
            Me.cargarDetalle(codigo_acp)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("FrmListaResumenPresupuesto.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3"))
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Ingreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ingresos"))
            Egreso += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "egresos"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "TOTALES "
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(0).ColumnSpan = 5
            e.Row.Cells(0).ForeColor = Drawing.Color.Blue

            e.Row.Cells(1).Text = "S/. " + FormatNumber(Ingreso.ToString(), 2)
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue

            e.Row.Cells(2).Text = "S/. " + FormatNumber(Egreso.ToString(), 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).ForeColor = Drawing.Color.Blue
            e.Row.Font.Bold = True
            e.Row.Height = 20
            e.Row.Cells(3).Visible = False
            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(6).Visible = False
        End If
    End Sub
End Class
