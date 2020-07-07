
Partial Class AsignaSesionNetEncuestaDocente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        If (Request.QueryString("per") IsNot Nothing And Request.QueryString("cup") IsNot Nothing) Then
            Session.Add("id_per", Request.QueryString("per"))
            Session.Add("id_cup", Request.QueryString("cup"))
            Response.Redirect("academico/encuesta/EvaluacionAlumnoDocente/EvaluacionDocente_Docente.aspx")
        End If
    End Sub
End Class
