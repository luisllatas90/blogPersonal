
Partial Class datosfamiliares_reporte1
    Inherits System.Web.UI.Page

    Protected Sub dlPersonal_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlPersonal.ItemDataBound
        Dim Hijos As BulletedList
        Hijos = CType(e.Item.FindControl("blHijos"), BulletedList)
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    End Sub

    Protected Sub txtConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtConsultar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.dlPersonal.DataSourceID = Nothing
        Me.dlPersonal.DataSource = ObjCnx.TraerDataTable("FAM_ReporteFamiliaFiltros", Left(Me.ddlOrden.Text, 2).ToUpper, Me.ddlArea.SelectedValue, ddlUsat.SelectedValue, ddlMes.SelectedValue, ddlSexo.selectedvalue)
        Me.dlPersonal.DataBind()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "inline;filename=Clientes.xls")
        Response.Charset = ""
        Me.EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHtmlTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        dlPersonal.RenderControl(oHtmlTextWriter)
        Response.Write(oStringWriter.ToString())
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
