
Partial Class SisSolicitudes_DatosDeSolicitud
    Inherits System.Web.UI.Page

    Protected Sub GvEvaluacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEvaluacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            If CInt(Fila.Item("codigo_res")) = 0 Then 'Verifica si las instancias ya resolvieron la solicitud
                'Verifica si es Julia quien evalua no queda como pendiente por ella tiene la funcion de visualizacion mas no de calificar
                If Fila.Item("codigo_per") = 473 Then '-->Codigo per de Julia Danjanovic
                    e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    e.Row.Cells(2).Text = "-"
                Else
                    e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    e.Row.Cells(2).Text = "Pendiente"
                End If
            Else
                e.Row.Cells(2).ForeColor = Drawing.Color.Green
                e.Row.Cells(2).Text = "Ya dió respuesta"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("codigo_sol").ToString = "" Then
                ActivarControles(False)
            Else
                ActivarControles(True)
            End If
        End If
    End Sub

    Private Sub ActivarControles(ByVal valor As Boolean)
        Me.LblAsuntos.Visible = valor
        Me.LblDatSolicitud.Visible = valor
        Me.LblEstado.Visible = valor
        Me.LblMotivos.Visible = valor
    End Sub
End Class
