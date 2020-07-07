
Partial Class AsignaSesiones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Limpiamos sesiones anteriores            
            Session.Clear()            
            'x = cac
            'y = alu
            'z = redir

            'Asignamos Sesiones            
            Session.Add("codigo_cac", Request.QueryString("x").ToString)
            Session.Add("codigo_alu", Request.QueryString("y").ToString)
            If (Request.QueryString("z") Is Nothing) Then
                Response.Redirect("../estudiante/principal.asp")
            Else
                Response.Redirect("../estudiante/" & Request.QueryString("z").ToString)
            End If

        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class
