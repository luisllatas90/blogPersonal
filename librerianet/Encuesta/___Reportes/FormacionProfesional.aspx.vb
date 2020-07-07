
Partial Class Encuesta_Reportes_FormacionProfesional
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
        Response.Write(ClsFunciones.HTML(Me.GvFormacion, "Matriz de Acreditación Universitaria: Formación Profesional - Estudiantes - al ", "Encuesta de Acreditación Universitaria - Campus virtual USAT"))
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LblTotal.Text = "Se encontraron " & GvFormacion.Rows.Count & " registros"
        End If
    End Sub

    Protected Sub GvFormacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvFormacion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            With fila.Row
                e.Row.Cells(2).Text = Mid(.Item("Est. Enseñanza").ToString, 2, .Item("Est. Enseñanza").ToString.Length)
                e.Row.Cells(3).Text = Mid(.Item("Est. Capacidad").ToString, 2, .Item("Est. Capacidad").ToString.Length)
                e.Row.Cells(4).Text = Mid(.Item("Silabus").ToString, 2, .Item("Silabus").ToString.Length)
                e.Row.Cells(5).Text = Mid(.Item("Eval. Aprendizaje").ToString, 2, .Item("Eval. Aprendizaje").ToString.Length)
                e.Row.Cells(6).Text = Mid(.Item("Beneficios").ToString, 2, .Item("Beneficios").ToString.Length)
                e.Row.Cells(7).Text = Mid(.Item("Ayuda").ToString, 2, .Item("Ayuda").ToString.Length)
                e.Row.Cells(8).Text = Mid(.Item("Tutoria").ToString, 2, .Item("Tutoria").ToString.Length)
            End With
        End If
    End Sub
End Class
