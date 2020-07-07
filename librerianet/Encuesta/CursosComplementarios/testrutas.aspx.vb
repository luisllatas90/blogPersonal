
Partial Class Encuesta_CursosComplementarios_testrutas
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('https://intranet.usat.edu.pe/campusvirtual/');", True)
        'ClientScript.RegisterStartupScript(Me.GetType, "direccionar", "location.href('../../../');", True)
    End Sub

End Class
