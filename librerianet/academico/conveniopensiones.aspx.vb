
Partial Class conveniopensiones
    Inherits System.Web.UI.Page

    Dim Total_Cuota As Double = 0
    Dim Total_Pago As Double = 0
    Dim Total_Saldo As Double = 0
    Dim Total_Mora As Double = 0

    Dim Total_ImporteDeuda As Double = 0


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)

                If fila.Row("estado_cpa").ToString = "A" Then
                    e.Row.Cells(1).Text = "ANULADO"
                    e.Row.Cells(1).ForeColor = Drawing.Color.Red
                Else
                    e.Row.Cells(1).Text = "CONFORME"
                    e.Row.Cells(1).ForeColor = Drawing.Color.Blue
                End If
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GridView1','Select$" & e.Row.RowIndex.ToString & "')")
                e.Row.Style.Add("Cursor", "Hand")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.LblCodigoUniver.Text = Request.QueryString("codigouniver_alu")
        Me.LblEscuela.Text = Server.HtmlDecode(Request.QueryString("nombre_cpf"))
        Me.LblNombres.Text = Server.HtmlDecode(Request.QueryString("alumno"))

        If Me.GridView1.Rows.Count > 0 Then
            Me.GridView1.SelectedIndex = -1
        End If

    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        'Dim Mora As Double = 0
        Dim SubTotal As Double = 0

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                'Response.Write(CStr(DateDiff(DateInterval.Day, CDate(fila.Row("fechaVencimiento_Dcp")), Now)) & "<br>")

                If DateDiff(DateInterval.Day, CDate(fila.Row("fechaVencimiento_Dcp")), Now) > 0 And CDbl(fila.Row("saldo")) > 0 Then
                    'Mora = CDbl(DateDiff(DateInterval.Day, CDate(fila.Row("fechaVencimiento_Dcp")), Now) * 0.5)
                    'Me.Total_Mora = Total_Mora + Mora
                    'e.Row.Cells(5).Text = FormatNumber(Mora, 2, TriState.True)
                    e.Row.Cells(5).ForeColor = Drawing.Color.Red
                Else
                    'e.Row.Cells(5).Text = "0.00"
                End If


                SubTotal = fila.Row("saldo") + e.Row.Cells(5).Text
                e.Row.Cells(6).Text = FormatNumber(SubTotal, 2, TriState.True)

                Me.Total_Cuota = Total_Cuota + fila.Row("importe_dcp")
                Me.Total_Pago = Total_Pago + fila.Row("pago")
                Me.Total_Saldo = Total_Saldo + fila.Row("saldo")


                Me.LblCuota.Text = FormatNumber(Total_Cuota, 2, TriState.True)
                Me.LblPago.Text = FormatNumber(Total_Pago, 2, TriState.True)
                Me.LblSaldo.Text = FormatNumber(Total_Saldo, 2, TriState.True)
                Me.LblMora.Text = FormatNumber(Total_Mora, 2, TriState.True)
                Me.LblSubTotal.Text = FormatNumber(Me.Total_Saldo + Me.Total_Mora, 2, TriState.True)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView3_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                Me.Total_ImporteDeuda = Total_ImporteDeuda + fila.Row("monto_dcp")

            End If
            Me.LblTotalimporte.Text = FormatNumber(Me.Total_ImporteDeuda, 2, True)
        Catch ex As Exception

        End Try
    End Sub

End Class
