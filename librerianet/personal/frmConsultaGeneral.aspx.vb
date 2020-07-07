
Partial Class frmConsultaGeneral
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ConsultaGeneral()
    End Sub
    Private Sub ConsultaGeneral()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarHorariosGeneral()
        Me.gvConsultaGeneral.DataSource = dts
        Me.gvConsultaGeneral.DataBind()
    End Sub
End Class
