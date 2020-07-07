
Partial Class logistica_frmVerAdjuntos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objL As New ClsLogistica
        'Me.frame1.Attributes.Add("src", "form.aspx?id=" & Request.QueryString("id"))
        Dim codigo_rco As String = objL.EncrytedString64(Request.QueryString("id"))
        Session("id_per") = Request.QueryString("p")
        Session("perlogin") = Request.QueryString("l")
        Me.cod_rco.Value = codigo_rco
        Response.Write(Session("perlogin"))
    End Sub
End Class
