
Partial Class Crm_FrmListaTutor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("ctf") = Request("ctf")
        ' Me.ct.Value = Session("ctf")
    End Sub
End Class
