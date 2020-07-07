
Partial Class Encuesta_Escuela_ConsultaTemasDeCertificacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        gvResultadosEncuesta.DataSource = obj.TraerDataTable("ENC_ConsultarResultadoEncuestaCertificacion")
        gvResultadosEncuesta.DataBind()
        lblTotal.Text = gvResultadosEncuesta.Rows.Count.ToString & " registros"
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReporteEncuestaCertificacion", gvResultadosEncuesta, "Reporte: Encuesta de temas para cursos de certificación", "Sistema de Encuestas - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#FFFF66")
        grid.HeaderRow.ForeColor = Drawing.Color.Black
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub

End Class
