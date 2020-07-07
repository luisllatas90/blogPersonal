Imports System.Data
Imports System.Data.OleDb

Partial Class EnviaCorreoPE_EnviaCorreoPE
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim enviamail As New ClsMail
            Dim mensaje, asunto, A, De As String
            mensaje = ""
            asunto = "Nuevo inscrito [III Jornada Internacional de Enfermeria]"
            De = "mneciosup@usat.edu.pe"
            A = "mneciosup@usat.edu.pe"
            mensaje = "1 nuevo inscrito a la fecha " & Date.Now.ToShortDateString & "<br><br>"

            CrearMensaje(mensaje)
            enviamail.EnviarMail(De, A, asunto, mensaje, True)
            ClientScript.RegisterStartupScript(Me.GetType, "correcto", "alert('Se insertaron correctamente los datos');window.close();", True)
        End If
    End Sub
    Private Sub CrearMensaje(ByRef Mensaje As String)
        Dim Cnx As New OleDb.OleDbConnection
        Dim Cmd As New OleDb.OleDbCommand
        Dim Tabla As OleDb.OleDbDataReader

        Cnx.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" & Server.MapPath("base.mdb")
        Cnx.Open()

        Cmd.Connection = Cnx
        Cmd.CommandText = "SELECT TOP 1 *FROM interesados where idinteresado=" & Request.QueryString("id")
        Tabla = Cmd.ExecuteReader

        While Tabla.Read
            'Response.Write(Tabla.GetString(2) & "<BR>")
            With Tabla
                FormatoMensaje(Mensaje)
                Mensaje = Mensaje & "<table border=0>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "<H5>III Jornada Internacional de Enfermeria</H5>"
                Mensaje = Mensaje & "************************ FICHA DE INSCRIPCIÓN ************************<br>"
                Mensaje = Mensaje & "<b>Información Personal</b><br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "Nombres y Apellidos: " & .GetString(1).ToString & " " & .GetString(2) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Dirección: " & .GetString(3) & " "
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Distrito: " & .GetString(4) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td>"
                Mensaje = Mensaje & "Teléfono Domicilio: " & .GetString(5) & "  "
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Teléfono Celular: " & .GetString(6) & "&nbsp;&nbsp;&nbsp; "
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "E-mail: " & .GetString(7) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td>"
                Mensaje = Mensaje & "Fecha de Nacimiento: " & .GetString(8) & "  "
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Sexo: " & .GetString(9) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Doc. de Identidad: " & .GetString(10) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "<b>Formación Académica - Profesional</b><br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "<table border=1 cellpadding=0 cellspacing=0>"
                Mensaje = Mensaje & "<tr><td>"
                Mensaje = Mensaje & "Estudios <br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Institución <br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Desde - Hasta <br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Título o Grado Obtenido<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td>"
                Mensaje = Mensaje & .GetString(11) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(12) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(13) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(14) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td>"
                Mensaje = Mensaje & .GetString(15) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(16) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(17) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & .GetString(18) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "</table>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "<b>Información Laboral</b><br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "Centro Laboral: " & .GetString(19) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Cargo: " & .GetString(20) & ""
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Área: " & .GetString(21) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Dirección: " & .GetString(22) & ""
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Distrito: " & .GetString(23) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Teléfono: " & .GetString(24) & ""
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Fax: " & .GetString(25) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "A Quien Reporta: " & .GetString(26) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "E-mail - Jefe: " & .GetString(27) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "<b>Datos de Facturación</b><br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=3>"
                Mensaje = Mensaje & "Doc de Facturación: " & .GetString(28) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Ruc: " & .GetString(29) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Facturar A: " & .GetString(30) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Dirección: " & .GetString(31) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Forma de pago: " & .GetString(32) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "<tr><td colspan=2>"
                Mensaje = Mensaje & "Cuotas de crédito: " & .GetString(33) & "<br>"
                Mensaje = Mensaje & "</td><td>"
                Mensaje = Mensaje & "Referencias: " & .GetString(34) & "<br>"
                Mensaje = Mensaje & "</td></tr>"
                Mensaje = Mensaje & "</table"
                Mensaje = Mensaje & "Fecha de Inscripción: " & .GetString(35) & "<br>"
            End With
        End While
        Tabla.Close()
        Tabla = Nothing
        Cnx.Close()
        Cnx.Dispose()
    End Sub

    Private Sub FormatoMensaje(ByRef Mensaje As String)
        Mensaje = Mensaje & "<style type=text/css>" & Chr(13)
        Mensaje = Mensaje & "<!--" & Chr(13)
        Mensaje = Mensaje & "body,td,th {" & Chr(13)
        Mensaje = Mensaje & "font-family: Verdana, Arial, Helvetica, sans-serif;" & Chr(13)
        Mensaje = Mensaje & "    font-size: 12px;" & Chr(13)
        Mensaje = Mensaje & "}" & Chr(13)
        Mensaje = Mensaje & ".Estilo1 {color: #FFFFFF}" & Chr(13)
        Mensaje = Mensaje & "-->" & Chr(13)
        Mensaje = Mensaje & "</style>"
    End Sub
End Class
