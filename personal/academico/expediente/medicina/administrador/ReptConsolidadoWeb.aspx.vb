Imports Microsoft.Reporting.WebForms
Imports System.Collections.Generic

Partial Class ReptConsolidadoWeb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.CmdSalir.Attributes.Add("OnClick", "javascript:history.back();")
        End If

        If Request.QueryString("codal") = "" Then
            Me.RptConsolidado.Visible = False
        Else
            Me.RptConsolidado.ServerReport.ReportPath = "/PRIVADOS/ACADEMICO/ASIST_NOTAS/ACAD_RepNotasConsoliAlumno"
            Dim paramList As New Generic.List(Of ReportParameter)
            paramList.Add(New ReportParameter("codigo_alu", Request.QueryString("codal"), False))
            paramList.Add(New ReportParameter("codigo_cac", Request.QueryString("cac"), False))
            'paramList.Add(New ReportParameter("codigo_cpa", Request.QueryString("codigo_cpa"), False))
            'paramList.Add(New ReportParameter("usuario", Request.ServerVariables("LOGON_USER"), False))
            Me.RptConsolidado.ServerReport.SetParameters(paramList)
        End If

    End Sub
End Class
