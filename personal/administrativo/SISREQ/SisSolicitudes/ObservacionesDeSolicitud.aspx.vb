
Partial Class SisSolicitudes_ObservacionesDeSolicitud
    Inherits System.Web.UI.Page


    Protected Sub GvObservaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvObservaciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            Select Case CInt(Fila.Item("instancia_Eob").ToString)
                Case 1 : e.Row.Cells(3).Text = "Director de Escuela"
                Case 2 : e.Row.Cells(3).Text = "Director Académico"
                Case 3 : e.Row.Cells(3).Text = "Administrador General"
            End Select
            Select Case CInt(Fila.Item("estado_eob").ToString)
                Case 0 : e.Row.Cells(4).Text = "Sin revisar"
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red
                Case 1 : e.Row.Cells(4).Text = "Revisado"
                    e.Row.Cells(4).ForeColor = Drawing.Color.Green
            End Select

        End If
    End Sub

End Class
