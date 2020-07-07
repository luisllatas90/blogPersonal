
Partial Class AsignaSesionesEncuestaDD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Limpiamos en caso que exista otra sesión                        
            Session.Clear()

            'Asignamos Sesiones            

            Session.Add("EAD_codigocup", Request.QueryString("w").ToString)
            Session.Add("codigo_cac", Request.QueryString("x").ToString)
            Session.Add("codigo_alu", Request.QueryString("y").ToString)
            'Creamos sesiones en la aplicacion libreriaNet
            Response.Redirect("../librerianet/Encuesta/EvaluacionAlumnoDocente/AsignaSesionesEncuestaDD.aspx?w=" & Session("EAD_codigocup") & "&x=" & Session("codigo_cac") & "&y=" & Session("codigo_alu"))
            'Response.Write(Session("codigo_alu"))
            'Response.Write(Session("EAD_codigocup"))


        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class

