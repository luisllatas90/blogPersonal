
Partial Class Encuesta_Reportes_BienestarUniversitario
    Inherits System.Web.UI.Page

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteInvestigaciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(ClsFunciones.HTML(Me.GvInvestigacion, "Matriz de Acreditación Universitaria: Investigación - Estudiantes - al ", "Encuesta de Acreditación Universitaria - Campus virtual USAT"))
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LblTotal.Text = "Se encontraron " & GvInvestigacion.Rows.Count & " registros"
        End If
    End Sub

    Protected Sub GvInvestigacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvInvestigacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            With fila.Row
                e.Row.Cells(2).Text = IIf(.Item("Conoce P. Bienestar").ToString = 1, "SI", "NO")
                e.Row.Cells(4).Text = Mid(.Item("Atención Medica").ToString, 2, .Item("Atención Medica").ToString.Length)
                e.Row.Cells(5).Text = Mid(.Item("Psicología").ToString, 2, .Item("Psicología").ToString.Length)
                e.Row.Cells(6).Text = Mid(.Item("Pedagogía").ToString, 2, .Item("Pedagogía").ToString.Length)
                e.Row.Cells(7).Text = Mid(.Item("Asistencia Social").ToString, 2, .Item("Asistencia Social").ToString.Length)
                e.Row.Cells(8).Text = Mid(.Item("Deportes").ToString, 2, .Item("Deportes").ToString.Length)
                e.Row.Cells(9).Text = Mid(.Item("Culturales").ToString, 2, .Item("Culturales").ToString.Length)
                e.Row.Cells(10).Text = Mid(.Item("Biblioteca").ToString, 2, .Item("Biblioteca").ToString.Length)
                e.Row.Cells(11).Text = Mid(.Item("Biblioteca Virtual").ToString, 2, .Item("Biblioteca Virtual").ToString.Length)
            End With
        End If
    End Sub

End Class
