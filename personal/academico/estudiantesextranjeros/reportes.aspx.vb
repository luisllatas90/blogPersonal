
Partial Class librerianet_estudiantesextranjeros_reportes
    Inherits System.Web.UI.Page

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        gridview1.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Me.TextBox1.Text = year(now())
    End Sub

   
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment; filename=relacionesinternacionales.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Dim sw As New System.IO.StringWriter
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)
        gridview1.rendercontrol(hw)
        Response.Write(sw.ToString)
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As System.Web.UI.Control)

    End Sub



End Class
