Imports System
Imports System.IO
Imports System.Data
Imports System.Web.UI
Partial Class academico_rptListaPagosDeuda
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("al") IsNot Nothing) Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Me.gvPagoVsDeuda.DataSource = obj.TraerDataTable("CAJ_EstadoCuentaAlumno", Request.QueryString("al"))
            Me.gvPagoVsDeuda.DataBind()
        Else
            Response.Write("No se encontró al alumno")
        End If        
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        'Dim Page As Page = New Page()
        'Dim form As HtmlForm = New HtmlForm()
        'Me.gvPagoVsDeuda.EnableViewState = False
        'Page.EnableEventValidation = False
        'Page.DesignerInitialize()
        'Page.Controls.Add(form)
        'form.Controls.Add(Me.gvPagoVsDeuda)
        'Page.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=rptPagosDeudas" & ".xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default
        'Response.Write(sb.ToString())
        'Response.End()
        Try
            Response.Clear()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment;filename=rptPagosDeudas" & ".xls")
            Response.Charset = ""
            Response.ContentType = "application/vnd.ms-excel"
            Dim sWriter As New StringWriter()
            Dim hWriter As New HtmlTextWriter(sWriter)
            gvPagoVsDeuda.RenderControl(hWriter)
            Response.Output.Write(sWriter.ToString())
            Response.Flush()
            Response.End()
        Catch ex As Exception
            Response.Write(ex.ToString())
        End Try
    End Sub
End Class
