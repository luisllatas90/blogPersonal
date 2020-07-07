
Partial Class planillaQuinta_frmConsultarEjecutadoPlanilla
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            'Cargar datos de Proceso presupuestal
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If
        End If
        Me.lblNroRegistros.Text = gvResultado.Rows.Count.ToString & " Registros"
        Me.lblNroRegistros.ForeColor = Drawing.Color.Blue
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReportePlanillaEjecutado", gvResultado, "Reporte de Ejecutado: Planilla", "Sistema de Presupuesto - Campus Virtual USAT")
    End Sub
End Class
