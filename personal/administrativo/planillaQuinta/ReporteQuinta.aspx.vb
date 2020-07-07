
Partial Class librerianet_planillaQuinta_ReporteQuintaTotal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPlla As New clsPlanilla
            Dim objFun As New ClsFunciones
            ScriptManager.GetCurrent(Me.Page).RegisterPostBackControl(cmdExportar)
            objFun.CargarListas(Me.cboPlanilla, objPlla.ConsultarPlanilla, "codigo_plla", "PLanilla")
            objPlla = Nothing
            objFun = Nothing
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Me.gvQuintas.DataSourceID = objQuintas.ID
        Me.gvQuintas.DataBind()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        cmdConsultar_Click(sender, e)
        Axls("ReporteQuintas" & Date.Now.Day.ToString & Date.Now.Month.ToString & Date.Now.Year.ToString, gvQuintas, "REPORTE DE QUINTAS", "Sistema de Quinta Especial - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#FF9900")
        grid.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub
End Class
