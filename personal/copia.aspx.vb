
Partial Class copia
    Inherits System.Web.UI.Page



    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        '=============== ENVIAR CORREO ===============================



    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim referer As String = Request.Headers("Referer").ToString()
        Response.Write("--")

        Response.Write(referer)
    End Sub
End Class
