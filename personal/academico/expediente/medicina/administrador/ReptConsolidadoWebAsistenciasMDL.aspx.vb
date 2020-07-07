Imports Microsoft.Reporting.WebForms
Imports System.Collections.Generic

Partial Class ReptConsolidadoWebAsistenciasMDL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.CmdSalir.Attributes.Add("OnClick", "javascript:history.back();")
        End If

        If Request.QueryString("codal") = "" Then
            Me.ReptConsolidadoWebAsistenciasMDL.Visible = False
        Else
            Me.ReptConsolidadoWebAsistenciasMDL.ServerReport.ReportPath = "/PRIVADOS/ACADEMICO/ACAD_RepAsistConsoliAlumnoMDL"
            Dim paramList As New Generic.List(Of ReportParameter)
            paramList.Add(New ReportParameter("codigo_alu", Request.QueryString("codal"), False))
            paramList.Add(New ReportParameter("codigo_cac", Request.QueryString("cac"), False))
            Me.ReptConsolidadoWebAsistenciasMDL.ServerReport.SetParameters(paramList)
        End If
    End Sub
End Class
