
Partial Class TestEnvio
    Inherits System.Web.UI.Page

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        '=============== ENVIAR CORREO ===============================
        Dim objemail As New ClsMail
        Dim dt As New Data.DataTable
        Dim cuerpo, receptor, AsuntoCorreo As String
        cuerpo = "<html> "
        cuerpo = cuerpo & "<head>"
        cuerpo = cuerpo & "<title></title>"
        cuerpo = cuerpo & "<style>"
        cuerpo = cuerpo & ".FontType{font-family: calibri; font-size:12px; }"
        cuerpo = cuerpo & "</style>"
        cuerpo = cuerpo & "</head>"
        cuerpo = cuerpo & "<body>"
        cuerpo = cuerpo & "<table border=0 cellpadding=0 class=""FontType"">"
        cuerpo = cuerpo & "<tr><td colspan=2 ><b>Estimado(a):</b></td></tr>"
        cuerpo = cuerpo & "<tr><td colspan=2></br></br>Saludos Cordiales</td></tr>"
        cuerpo = cuerpo & "</table>"
        cuerpo = cuerpo & "</body>"
        cuerpo = cuerpo & "</html>"

        
            receptor = "hcano@usat.edu.pe"
        

        AsuntoCorreo = "[Evaluación de Programa/Proyecto]"
        objemail.EnviarMail("campusvirtual@usat.edu.pe", "Evaluación de Programa/Proyecto", receptor, AsuntoCorreo, cuerpo, True)
    End Sub
End Class
