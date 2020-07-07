Imports System.Data
Imports System.Data.SqlClient

Partial Class Investigador_datos_investigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tabla As DataTable
        Dim objInvestigacion As New Investigacion
        tabla = objInvestigacion.ConsultarInvestigacion(Request.QueryString("codigo_inv"), "12")
        Me.DataList1.DataSource = tabla
        Me.DataList1.DataBind()
        objInvestigacion = Nothing
    End Sub
End Class

