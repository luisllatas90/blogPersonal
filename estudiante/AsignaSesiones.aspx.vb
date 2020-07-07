
Partial Class AsignaSesiones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        Try
            'Limpiamos en caso que exista otra sesión                        
            Session.Clear()

            'Asignamos Sesiones            
            Session.Add("codigo_cac", Request.QueryString("x").ToString)
            Session.Add("codigo_alu", Request.QueryString("y").ToString)

            'Creamos sesiones en la aplicacion libreriaNet
            If (Request.QueryString("z") IsNot Nothing) Then
                Response.Redirect("../librerianet/AsignaSesiones.aspx?x=" & Session("codigo_cac") & "&y=" & Session("codigo_alu") & "&z=" & Request.QueryString("z").ToString)
            Else
                'Response.Write("AsignaSesiones")
                Response.Redirect("../librerianet/AsignaSesiones.aspx?x=" & Session("codigo_cac") & "&y=" & Session("codigo_alu"))
            End If

        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class
