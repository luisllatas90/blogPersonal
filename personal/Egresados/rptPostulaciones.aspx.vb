
Partial Class Egresados_rptPostulaciones
    Inherits System.Web.UI.Page

    Sub CargarData()
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        dt = New Data.DataTable
        dt = obj.TraerDataTable("ALUMNI_ReportePostulaciones")
        Me.gwData.datasource = dt
        Me.gwData.databind()
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gwData)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=CampusVirtualAlumni_ReportePostulaciones" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        CargarData()
    End Sub
End Class
