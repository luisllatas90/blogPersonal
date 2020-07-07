
Partial Class MKTE_FrmEventoUSAT
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGrabar.Click


        Dim con As New MKTConexionBD

        Try

            Dim Evento As String = "USAThurday 2013 - v1"
            Dim Escuela As String = ddlEscuela.Text.ToUpper
            Dim Nombres As String = txtNombres.Text.Trim.ToUpper
            Dim Apellidos As String = txtApellidos.Text.Trim.ToUpper
            Dim Email As String = txtEmail.Text.Trim.ToUpper
            Dim Direccion As String = txtDireccion.Text.Trim.ToUpper
            Dim TelefonoFijo As String = txtTelefFijo.Text.Trim.ToUpper
            Dim TelefonoCelular As String = txtTelefCelular.Text.Trim.ToUpper
            Dim GradoEstudios As String = ddlGradoEstudios.Text.ToUpper
            Dim ComoSeEnteroEvento As String = ddlMedioComunicacion.Text.ToUpper


            Dim sql As String
            sql = "INSERT INTO MKT_Evento (Evento,Escuela,Nombres,Apellidos,Email,Direccion,TelefonoFijo,TelefonoCelular,GradoEstudios,ComoSeEnteroEvento) VALUES ('" & Evento & "','" & Escuela & "','" & Nombres & "','" & Apellidos & "','" & Email & "','" & Direccion & "','" & TelefonoFijo & "','" & TelefonoCelular & "','" & GradoEstudios & "','" & ComoSeEnteroEvento & "')"
            con.conectarEjecutar(sql)
            lblMensaje.Text = "Participación Registrada Exitosamente!"

            'Enviar Email
            Dim correo As New MKTEmail
            Dim mensaje As String
            Dim para As String

            mensaje = "Estimado Director(a) <br>La siguiente persona ha seleccionado a su escuela para el : " & Evento & " <br><br><br>" & Evento & " <br>" & "Escuela: " & Escuela & "<br>" & "Nombres: " & Nombres & "<br>" & "Apellidos: " & Apellidos & "<br>" & "Email: " & Email & "<br>" & "Direccion: " & Direccion & "<br>" & "Telf. Fijo: " & TelefonoFijo & "<br>" & "Telef. Celular: " & TelefonoCelular & "<br>" & "Grado Estudios: " & GradoEstudios & "<br>" & "Se entero por: " & ComoSeEnteroEvento & "<br>"

            para = obtenerEmailDelDirector(ddlEscuela.SelectedIndex)

            correo.EnviarMail(para, "Inscripcion " & Evento & ": " & Apellidos & "  " & Nombres & " - " & Escuela, mensaje, True, "hzelada@usat.edu.pe", "")


        Catch ex As Exception
            lblMensaje.Text = "Error: " & ex.Message

        End Try



    End Sub

    Public Function obtenerEmailDelDirector(ByVal idEsc As Integer) As String
        Dim email As String = ""

        Select Case idEsc

            Case 1 ' Administracion
                email = "jmundaca@usat.edu.pe"

            Case 2 'Adm. Hotelera 
                email = ""

            Case 3 ' Contabilidad
                email = "jgarces@usat.edu.pe"

            Case 4 'Economia
                email = "jpenalillo@usat.edu.pe"

            Case 5 'Derecho
                email = "lheras@usat.edu.pe"

            Case 6   ' Comunicacion
                email = "rchullen@usat.edu.pe"

            Case 7   'Educ. Primaria
                email = "lgalvez@usat.edu.pe"

            Case 8   'Educ. Secundaria
                email = "lgalvez@usat.edu.pe"

            Case 9   'Filos y Teologia
                email = "lgalvez@usat.edu.pe"

            Case 10  'Arquitectura
                email = "gecheandia@usat.edu.pe"

            Case 11   'Sistemas
                email = "hzelada@usat.edu.pe"

            Case 12  'Industrial
                email = "ssalazar@usat.edu.pe"

            Case 13  'Civil
                email = "wgarcia@usat.edu.pe"

            Case 14  'Mecania
                email = "hmundaca@usat.edu.pe"

            Case 15  'Naval
                email = "mzaliotkina@usat.edu.pe"

            Case 16 'Medicina
                email = "sgayoso@usat.edu.pe"

            Case 17 'Enfermeria
                email = "rdiaz@usat.edu.pe"

            Case 18  'Odontologia
                email = "jjulca@usat.edu.pe"

            Case 19 'Psicologia
                email = "mescuza@usat.edu.pe"

            Case Else
                email = "hdavila@usat.edu.pe"



        End Select

        Return email


    End Function

End Class
