
Partial Class indicadores_frmPeriodosImportados
    Inherits System.Web.UI.Page
    Dim Codigo_var As String

    Dim Anterior As String = String.Empty
    Dim PrimeraFila As Int32 = -1
    Dim Contador As Int32 = 0

    Dim Anteriorx As String = String.Empty
    Dim PrimeraFilax As Int32 = -1
    Dim Contadorx As Int32 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Codigo_var = Request.QueryString("codigo_var")
            CargarGridListaPeriodosImportados()
            CargarGridListaNotificaciones()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarGridListaNotificaciones()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Codigo_var = Request.QueryString("codigo_var")
            dts = obj.ListaNotificacionesVariable(Codigo_var)

            If dts.Rows.Count > 0 Then
                gvNotificaciones.DataSource = dts
                gvNotificaciones.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarGridListaPeriodosImportados()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Codigo_var = Request.QueryString("codigo_var")
            'Response.Write(Codigo_var)
            'Response.Write("<br />")

            dts = obj.ListaPeriodosImportadosVariable(Codigo_var)
            'Response.Write(dts.Rows.Count)

            If dts.Rows.Count > 0 Then
                gvListaPeriodosImportados.DataSource = dts
                gvListaPeriodosImportados.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

 
    Protected Sub gvListaPeriodosImportados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaPeriodosImportados.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                Contador = Contador + 1

                If Anterior = fila("Codigo") Then
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = ""
                    Contador = Contador - 1
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    Anterior = fila("Codigo").ToString()
                    PrimeraFila = e.Row.RowIndex
                    'e.Row.Cells(0).Text = Contador
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvNotificaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNotificaciones.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                Contadorx = Contadorx + 1

                If Anteriorx = fila("Codigo") Then
                    e.Row.Cells(0).Text = ""
                    e.Row.Cells(1).Text = ""
                    Contadorx = Contadorx - 1
                Else
                    e.Row.VerticalAlign = VerticalAlign.Middle
                    Anteriorx = fila("Codigo").ToString()
                    PrimeraFilax = e.Row.RowIndex
                    'e.Row.Cells(0).Text = Contador
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
