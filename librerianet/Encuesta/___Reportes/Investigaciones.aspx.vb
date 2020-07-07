
Partial Class Encuesta_Reportes_Investigaciones
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
                e.Row.Cells(2).Text = IIf(.Item("Participó en Inv.") = 1, "SI", "NO")
                e.Row.Cells(3).Text = IIf(.Item("Participó en Inv.") = 1, .Item("Num. Proyectos").ToString, "0")
                e.Row.Cells(4).Text = IIf(.Item("Participó en Inv.") = 1, .Item("Título Inv.").ToString, "")
                e.Row.Cells(5).Text = IIf(.Item("Participó en Inv.") = 1, Mid(.Item("Modo Participación"), 2, .Item("Modo Participación").ToString.Length), "")
                e.Row.Cells(6).Text = IIf(.Item("Participó en Inv.") = 1, .Item("Año").ToString, "")
                e.Row.Cells(7).Text = IIf(.Item("Participó en Inv.") = 1, .Item("Duración").ToString, "")
                e.Row.Cells(8).Text = IIf(.Item("Participó en Inv.") = 1, .Item("Quien Financió").ToString, "")
                e.Row.Cells(9).Text = IIf(.Item("Participó en Inv.") = 1, Mid(.Item("Medio de Ver.").ToString, 2, .Item("Medio de Ver.").ToString.Length), "")
                e.Row.Cells(10).Text = Mid(.Item("Satisfecho Eval").ToString, 2, .Item("Satisfecho Eval").ToString.Length)
                e.Row.Cells(11).Text = IIf(.Item("Participó E. Difusión") = 1, "SI", "NO")
                e.Row.Cells(12).Text = .Item("Proyecto Difusión")
                e.Row.Cells(13).Text = .Item("Autores Difusión")
                e.Row.Cells(14).Text = IIf(.Item("Participó E. Discusión") = 1, "SI", "NO")
                e.Row.Cells(15).Text = .Item("Proyecto Discusión").ToString
                e.Row.Cells(16).Text = .Item("Autores Discusión").ToString
                e.Row.Cells(17).Text = IIf(.Item("Conoce Prop. Intelectual") = True, "SI", "NO")
            End With
        End If
    End Sub


End Class
