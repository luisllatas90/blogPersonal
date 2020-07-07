
Partial Class Investigador_comentarios_investigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.CmdComentar.Attributes.Add("OnClick", "AbrirPopUp('agregacomentario.aspx?codigo_Inv=" & Request.QueryString("codigo_inv") & "','200','520'); return false;")

    End Sub
End Class
