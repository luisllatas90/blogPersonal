
Partial Class librerianet_biblioteca_BibliotecarioOnLine
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.txtdni.Attributes.add("onKeyPress", "validarnumero()")
        End If
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim objMail As New clsMail
        Dim conCopiaA, asuntoCorreo, mensaje As String
        Try
            conCopiaA = "rsortiz@usat.edu.pe; mguerrero@usat.edu.pe"
            asuntoCorreo = "Bibliotecario en línea"
            mensaje = "<table width='100%' style='font-family: Arial, Helvetica, sans-serif;font-size=9pt'>" & _
                      "<tr><td w><strong>Nombres y apellidos:</strong></td>" & _
                      "<td>" & txtNombres.text & "</td></tr>" & _
                      "<tr><td><strong>DNI:</strong></td>" & _
                      "<td>" & txtdni.text & "</td></tr>" & _
                      "<tr><td><strong>Correo Electrónico:</strong></td>" & _
                      "<td>" & txtEmail.text & "</td></tr>" & _
                                  "<tr><td><strong>Tipo de usuario:</strong></td>" & _
                      "<td>" & cbotipousuario.selecteditem.text & "</td></tr>" & _
                      "<tr><td><strong>Tema de consulta:</strong></td>" & _
                      "<td>" & txttemaconsulta.text & "</td></tr>" & _
                      "<tr><td><b>Su consulta:</b></td>" & _
                                  "<td>" & txtconsulta.text & "</td></tr>" & _
                      "</table>"
            mensaje = mensaje & "</br> <font color='blue' style='font-family: Arial, Helvetica, sans-serif;font-size=8pt'>__________________________</br>" & "Campus Virtual " & Date.Now.ToShortDateString & "</font>"
            objMail.EnviarMail("campusvirtual@usat.edu.pe", "El Bibliotecario en línea", " bibliotecarioenlinea@usat.edu.pe", asuntoCorreo, mensaje, True, ConCopiaA)
            ClientScript.RegisterStartupScript(Me.GetType, "ok", "alert('se envió correctamente los datos')", True)
            cmdLimpiar_Click(sender, e)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al enviar el correo, vuelva a intentarlo')", True)
        End Try
    End Sub

    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        Me.txtNombres.text = ""
        Me.txtdni.text = ""
        Me.txtemail.text = ""
        Me.txttemaconsulta.text = ""
        Me.txtconsulta.text = ""
        Me.cbotipousuario.selectedvalue = 0
    End Sub
End Class
