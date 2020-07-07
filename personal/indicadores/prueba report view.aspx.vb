Imports System.Collections.Generic

'Imports System.Data
Imports Microsoft.Reporting.WebForms

Partial Class indicadores_frmTableroPrincipal
    Inherits System.Web.UI.Page
    Dim usuario As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

               
            End If

            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

  

    Private Sub Cargar()

        'Dim instance As New Microsoft.Reporting.WebForms.ReportParameter("Oficina", "01")
        'Dim instance1 As New Microsoft.Reporting.WebForms.ReportParameter("Fecha", "2004.01.01")
        'Dim instance2 As New Microsoft.Reporting.WebForms.ReportParameter("Ordenadopor", "01")
        '        Dim instance As New Microsoft.Reporting.WebForms.ReportParameter("codigo_ind", ddlIndicadores.SelectedValue)
        '       Dim instance1 As New Microsoft.Reporting.WebForms.ReportParameter("año", ddlPeriodo.SelectedValue)
        Dim instance As New Microsoft.Reporting.WebForms.ReportParameter("codigo_ind", 3)
        Dim instance1 As New Microsoft.Reporting.WebForms.ReportParameter("año", 2011)

        Dim prms(1) As Microsoft.Reporting.WebForms.ReportParameter
        prms(0) = instance
        prms(1) = instance1

        rptGrafico.ServerReport.ReportServerUrl = New Uri("https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/PERSONAL/IND_GraficoBarrasUnIndicador")
        rptGrafico.ServerReport.ReportPath = "/PERSONAL/IND_GraficoBarrasUnIndicador"
        rptGrafico.ServerReport.SetParameters(prms)
        rptGrafico.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote
        'rptGrafico.RefreshReport()

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Cargar()
    End Sub
End Class



