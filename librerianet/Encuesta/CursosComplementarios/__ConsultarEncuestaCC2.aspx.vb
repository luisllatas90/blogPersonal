
Partial Class Encuesta_CursosComplementarios_ConsultarEncuestaCC2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        gvCursosComplementarios.DataSource = obj.TraerDataTable("ECC_ConsultarEncuestasCC2")
        obj.CerrarConexion()
        gvCursosComplementarios.DataBind()
        lblTotal.Text = gvCursosComplementarios.Rows.Count.ToString & " registros"
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReporteEncuestaCC", gvCursosComplementarios, "Reporte: Encuesta Cursos Complementarios: Curso Básico Fonética", "Sistema de Encuestas - Campus Virtual USAT")
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


End Class
