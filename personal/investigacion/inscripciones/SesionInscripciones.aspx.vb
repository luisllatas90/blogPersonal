
Partial Class academico_SesionInscripciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        'Response.Write("id=" & Request.QueryString("id"))

        If (Request.QueryString("id") IsNot Nothing) Then
            Session.Add("codigo_Usu", Request.QueryString("id"))
        End If
        Response.Redirect("frmRegInscripcionKermesPersonal.aspx?cco=" & Request.QueryString("cco"))

    End Sub
End Class
