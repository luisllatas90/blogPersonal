
Partial Class Encuesta_CursosComplementarios_ConsultarEncuestaCC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        gvCursosComplementarios.DataSource = obj.TraerDataTable("ECC_ConsultarCursosComplementario")
        gvCursosComplementarios.DataBind()
        lblTotal.Text = gvCursosComplementarios.Rows.Count.ToString & " registros"
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReporteEncuestaCC", gvCursosComplementarios, "Reporte: Encuesta Cursos Complementarios", "Sistema de Encuestas - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        grid.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub gvCursosComplementarios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursosComplementarios.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = "'" & e.Row.Cells(0).Text
            For i As Int16 = 3 To 7
                If fila.Row.Item(i) = True Then
                    e.Row.Cells(i).Text = "X"
                Else
                    e.Row.Cells(i).Text = ""
                End If
            Next
            If fila.Row.Item(9) = "S" Then
                e.Row.Cells(9).Text = "Sábado"
            End If
        End If
    End Sub

    Protected Sub gvCursosComplementarios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCursosComplementarios.SelectedIndexChanged

    End Sub
End Class
