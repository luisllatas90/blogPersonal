
Partial Class noadeudo_registrarnoadeudo
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer
    Dim ctf As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        codigo_per = Request.QueryString("id")
        ctf = Request.QueryString("ctf")
    End Sub

    Protected Sub cmdConsultarDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultarDatos.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable
        Dim objAdeudo As New ClsNoAdeudos

        Tbl = objAdeudo.ConsultarDatosAlumno(Me.txtCodigoUniversitario.Text)
        If Tbl.Rows.Count > 0 Then
            Me.lblCodigoUniversitario.Text = Tbl.Rows(0).Item("codigouniver_alu")
            Me.lblApellidosNombres.Text = Tbl.Rows(0).Item("alumno")
            Me.lblEscuelaProfesional.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblCicloIngreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")
            Me.txtcodigo_alu.Text = Tbl.Rows(0).Item("codigo_alu")
            lblMensaje.visible = False
            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            Me.FotoAlumno.Visible = True
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
            ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing
            cmdAceptar.enabled = True
        Else
            lblMensaje.visible = True
            Me.lblMensaje.Text = "* El estudiante no existe en la Base de datos"
            limpiarDatos()
            Me.FotoAlumno.Visible = False
            cmdAceptar.enabled = False
        End If
        Tbl.Dispose()
        Obj = Nothing
    End Sub
    Private Sub limpiarDatos()
        Me.lblCodigoUniversitario.Text = ""
        Me.lblApellidosNombres.Text = ""
        Me.lblEscuelaProfesional.Text = ""
        Me.lblCicloIngreso.Text = ""
        Me.lblPlan.Text = ""
        Me.txtcodigo_alu.Text = ""
        Me.txtCorreo.Text = ""
        Me.FotoAlumno.Visible = False
    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Dim objAdeudo As New ClsNoAdeudos
        Dim ncorreos As Integer

        Dim objMail As New ClsMail
        Dim remitente As String
        Dim nombreRemitente As String
        Dim asunto As String
        Dim mensaje As String
        Dim destinatario As String
        'Dim emailRemite As String
        Dim cod_per As Integer '12-03-2020

        lblMensaje.Text = objAdeudo.RegistrarNoAdeudo(txtcodigo_alu.Text, txtCorreo.Text, codigo_per)
        lblMensaje.Visible = True
        cmdAceptar.Enabled = False
        cmdCancelar.Enabled = False

        If Left(lblMensaje.Text, 2) <> "**" Then
            '*-* Enviar correo a todas las instancias indicando que tienen una nueva solicitud por tramitar
            remitente = objAdeudo.ListaRevisiores.Rows(0).Item("email").ToString()
            nombreRemitente = objAdeudo.ListaRevisiores.Rows(0).Item("revisor").ToString()
            'emailRemite = objAdeudo.ListaRevisiores.Rows(0).Item("email").ToString() '13-03 SE REPITE, igual que remitente

            Dim numcorreos As Integer = objAdeudo.ListaRevisiores.Rows.Count
            asunto = "CONSTANCIA DE NO ADEUDOS Alumno: " & Me.lblCodigoUniversitario.Text & ". " & Me.lblApellidosNombres.Text
            mensaje = "<font face=arial>Por favor, sírvase a revisar la <b>" & asunto & "</b>.<br>"
            mensaje = mensaje & "<br>Atentamente" & "<br><br><font size=2>"
            mensaje = mensaje & nombreRemitente & "<br>"
            'mensaje = mensaje & "CAMPUS VIRTUAL - USAT</font><font color=blue><br>"
            mensaje = mensaje & remitente & "</font></font>"

            For ncorreos = 0 To numcorreos - 1 '13-03 antes decía de 1 a -1 pero salian 6 envios y son 7                      

                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 1 Then
                    destinatario = objAdeudo.ListaRevisiores.Rows(ncorreos).Item("email").ToString()
                    cod_per = objAdeudo.ListaRevisiores.Rows(ncorreos).Item("codigo_per").ToString() '12-03-2020
                Else
                    destinatario = "cgastelo@usat.edu.pe"  'CORREO DE PRUEBA
                    cod_per = objAdeudo.ListaRevisiores.Rows(ncorreos).Item("codigo_per").ToString() '12-03-2020
                End If

                '12-03 Se cambia la clase por Envio de Correo Masivo 
                'objMail.EnviarMail("campusvirtual@usat.edu.pe", nombreRemitente, destinatario, asunto, mensaje, True, "", emailRemite)

                '12-03 No se considera el remitente, va vacío
                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ObjCnx.Ejecutar("ALUMNI_EnvioCorreosMasivoIUD", "I", codigo_per, 0, "codigo_per", cod_per, 46, destinatario, "", asunto, mensaje, "", CDate(TODAY), 0, 0)

            Next
            '------------------------------------------------------------------------
            Response.Redirect("consultarnoadeudo.aspx?id=" & codigo_per & "&ctf=" & ctf)
        End If

    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.txtCodigoUniversitario.Text = ""
        limpiarDatos()
    End Sub
End Class
