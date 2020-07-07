Imports Microsoft.Reporting.WebForms

Partial Class Prueba_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ReportViewer1.ProcessingMode = ProcessingMode.Remote
        ReportViewer1.ServerReport.ReportServerCredentials = New Net.NetworkCredential("usuario", "clave", "dominio")
        ReportViewer1.ServerReport.ReportServerUrl = New Uri("http://192.168.0.1/ReportServer/")
        ReportViewer1.ServerReport.ReportPath = "/autonarudzba/listanarudzbi"
        ReportViewer1.ServerReport.Refresh()
        'rptViewer.ServerReport.SetParameters(param)
        'rptViewer.ShowParameterPrompts = false;
    End Sub
End Class
