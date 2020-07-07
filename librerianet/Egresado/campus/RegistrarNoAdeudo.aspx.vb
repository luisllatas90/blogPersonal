
Partial Class noadeudo_registrarnoadeudo
    Inherits System.Web.UI.Page
    Dim codigo_per As Integer
    Dim ctf As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cmdConsultarDatos_Click()            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'codigo_per = Request.QueryString("id")
        'ctf = Request.QueryString("ctf")
    End Sub


    Sub CargarRevisiones(ByVal codigo As Integer)
        Dim obj As New ClsNoAdeudos
        Dim dt As Data.DataTable
        dt = obj.ConsultarRevisiones(codigo)
        gvRevision.Visible = True
        Me.nro.Text = "(" & dt.Rows.Count.ToString & " de 7) Revisiones"
        gvRevision.DataSource = dt
        gvRevision.DataBind()
    End Sub


    Sub cmdConsultarDatos_Click()
        Try
            Dim Tbl As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Tbl = obj.TraerDataTable("ALUMNI_ConsultarNoAdeudo", CInt(Session("codigo_alu")))
            obj.CerrarConexion()


            If Tbl.Rows.Count > 0 Then
                Me.lblCodigoUniversitario.Text = Tbl.Rows(0).Item("codigouniver_alu")
                Me.lblApellidosNombres.Text = Tbl.Rows(0).Item("alumno")
                Me.lblEscuelaProfesional.Text = Tbl.Rows(0).Item("nombre_cpf")
                Me.lblCicloIngreso.Text = Tbl.Rows(0).Item("cicloing_alu")
                Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")
                Me.txtcodigo_alu.Text = Tbl.Rows(0).Item("codigo_alu")
                lblMensaje.Visible = False
                ''Cargar la Foto

                If (Tbl.Rows(0).Item("foto_Ega").ToString.Trim <> "") Then
                    FotoAlumno.ImageUrl = "../fotos/" & Tbl.Rows(0).Item("foto_Ega")
                Else
                    FotoAlumno.ImageUrl = "../archivos/no_disponible.jpg"
                End If

                cmdAceptar.Enabled = True

                If CInt(Tbl.Rows(0).Item("cade")) > 0 Then
                    CargarRevisiones(CInt(Tbl.Rows(0).Item("cade")))
                    cmdAceptar.Enabled = False
                    Me.txtCorreo.Enabled = False
                    Me.lblMensaje.Visible = True
                    Me.lblMensaje.Text = "*Ya has solicitado una constancia de no adeudos"
                End If

            Else
                lblMensaje.Visible = True
                Me.lblMensaje.Text = "*"
                limpiarDatos()
                Me.FotoAlumno.Visible = False
                cmdAceptar.Enabled = False
            End If
            Tbl.Dispose()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

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

    'Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
    '    Dim objAdeudo As New ClsNoAdeudos
    '    Dim ncorreos As Integer

    '    Dim objMail As New ClsMail
    '    Dim remitente As String
    '    Dim nombreRemitente As String
    '    Dim asunto As String
    '    Dim mensaje As String
    '    Dim destinatario As String
    '    Dim emailRemite As String


    '    lblMensaje.Text = objAdeudo.RegistrarNoAdeudo(txtcodigo_alu.Text, txtCorreo.Text, codigo_per)
    '    lblMensaje.Visible = True
    '    cmdAceptar.Enabled = False
    '    cmdCancelar.Enabled = False

    '    If Left(lblMensaje.Text, 2) <> "**" Then
    '        '*-* Enviar correo a todas las instancias indicando que tienen una nueva solicitud por tramitar
    '        remitente = objAdeudo.ListaRevisiores.Rows(0).Item("email").ToString()
    '        nombreRemitente = objAdeudo.ListaRevisiores.Rows(0).Item("revisor").ToString()
    '        emailRemite = objAdeudo.ListaRevisiores.Rows(0).Item("email").ToString()

    '        Dim numcorreos As Integer = objAdeudo.ListaRevisiores.Rows.Count
    '        asunto = "CONSTANCIA DE NO ADEUDOS Alumno: " & Me.lblCodigoUniversitario.Text & ". " & Me.lblApellidosNombres.Text
    '        mensaje = "<font face=arial>Por favor, sírvase a revisar la <b>" & asunto & "</b>.<br>"
    '        mensaje = mensaje & "<br>Atentamente" & "<br><br><font size=2>"
    '        mensaje = mensaje & nombreRemitente & "<br>"
    '        mensaje = mensaje & "Secretaría de DIRECCION ACADÉMICA - USAT</font><font color=blue><br>"
    '        mensaje = mensaje & emailRemite & "</font></font>"
    '        For ncorreos = 1 To numcorreos - 1
    '            destinatario = objAdeudo.ListaRevisiores.Rows(ncorreos).Item("email").ToString()
    '            destinatario = "yperez@usat.edu.pe"
    '            objMail.EnviarMail("campusvirtual@usat.edu.pe", nombreRemitente, destinatario, asunto, mensaje, True, "", emailRemite)
    '        Next
    '        '------------------------------------------------------------------------            

    '        'Enviar mail de aviso a ehernandez
    '        'destinatario = "ehernandez@usat.edu.pe"
    '        destinatario = "yperez@usat.edu.pe"
    '        emailRemite = "campusvirtual@usat.edu.pe"
    '        asunto = "CONSTANCA DE NO ADEUDOS - Alumni USAT"
    '        mensaje = "Estimada (Sra) Elsa Hernández:</br></br>"
    '        mensaje &= "Se ha registrado una solicitud de constancia de no adeudos desde el CAMPUS ALUMNI - USAT</br></br>"
    '        mensaje &= "Código Univ.: " & Me.lblCodigoUniversitario.Text & "</br>"
    '        mensaje &= "Estudiante.: " & Me.lblApellidosNombres.Text & "</br></br>"
    '        mensaje &= "Atentamente,</br>"
    '        mensaje &= "<b>CAMPUS ALUMNI - USAT</b>"
    '        objMail.EnviarMail("campusvirtual@usat.edu.pe", "Campus ALUMNI - USAT", destinatario, asunto, mensaje, True, "", emailRemite)
    '        cmdConsultarDatos_Click()
    '    End If
    '    objMail = Nothing
    'End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        '  Me.txtCodigoUniversitario.Text = ""
        limpiarDatos()
    End Sub
End Class
