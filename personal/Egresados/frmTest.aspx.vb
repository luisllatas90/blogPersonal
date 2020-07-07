
Partial Class Egresados_frmTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtcodigo_ofe.Text = Session("codigo_ofe")
    End Sub
End Class
