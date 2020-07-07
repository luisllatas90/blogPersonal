Imports Microsoft.Reporting.WebForms
Imports System.Collections.Generic

Partial Class administrativo_pec_FrmImprimirConvenio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.CmdSalir.Attributes.Add("OnClick", "javascript:location.href='frmgeneracioncargos.aspx?" & Page.Request.QueryString.ToString & "'")
            If Request.QueryString("tcl") = "" Or Request.QueryString("codigo_cpa") = "" Then
                Me.RptConvenio.Visible = False
            Else
                Me.RptConvenio.ServerReport.ReportPath = "/PRIVADOS/PENSIONES/PEN_ConvenioPago"

                Dim paramList As New Generic.List(Of ReportParameter)
                paramList.Add(New ReportParameter("tcl", Request.QueryString("tcl"), False))
				paramList.Add(New ReportParameter("ctf", 0, False))
                paramList.Add(New ReportParameter("codigo_cpa", Request.QueryString("codigo_cpa"), False))
                paramList.Add(New ReportParameter("usuario", Request.ServerVariables("LOGON_USER"), False))
                Me.RptConvenio.ServerReport.SetParameters(paramList)
            End If
        End If
    End Sub

    
End Class
