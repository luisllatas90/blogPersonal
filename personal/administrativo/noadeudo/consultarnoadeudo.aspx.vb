
Partial Class noadeudo_consultarnoadeudo
    Inherits System.Web.UI.Page
    Dim id As Integer
    Dim ctf As Integer
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("registrarnoadeudo.aspx?id=" & id & "&ctf=" & ctf)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        id = Request.QueryString("id")
        ctf = Request.QueryString("ctf")

        If ctf = 1 Or ctf = 25 Then
            cmdFinalizar.Visible = True
            cmdNuevo.Visible = True
            pnlEvaluar.Visible = False
            ddlEstadoDirAcad.Visible = True
            ddlEstadoRevisor.Visible = False
            cmdImprimir.Visible = True

        Else
            cmdNuevo.Visible = False
            cmdFinalizar.Visible = False
            pnlEvaluar.Visible = True
            ddlEstadoDirAcad.Visible = False
            ddlEstadoRevisor.Visible = True
            cmdImprimir.Visible = False
        End If
        cmdEvaluar.Enabled = False
        consultarDetalle()
    End Sub

    Private Sub consultarDetalle()
        Dim obj As New ClsNoAdeudos
        If ctf = 1 Or ctf = 25 Then
            Me.gvdetalle.DataSource = obj.ConsultarNoAdeudos(Me.ddlEstadoDirAcad.SelectedValue, id)
        Else
            Me.gvdetalle.DataSource = obj.ConsultarNoAdeudos(Me.ddlEstadoRevisor.SelectedValue, id)
        End If

        'Response.Write("ADE_ConsultarNoAdeudos '" & Me.ddlEstadoDirAcad.SelectedValue & "'," & id)



        Me.gvdetalle.DataBind()

    End Sub


    Protected Sub ddlEstadoDirAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoDirAcad.SelectedIndexChanged
        'consultarDetalle()
        Refrescar()
    End Sub

    Protected Sub ddlEstadoRevisor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoRevisor.SelectedIndexChanged

        Refrescar()

    End Sub
    Private Sub Refrescar()
        rbConforme.Enabled = False
        rbObservado.Enabled = False
        rbConforme.Checked = False
        rbObservado.Checked = False
        txtObservacion.Text = ""
        cmdEvaluar.Enabled = False
        gvRevision.Visible = False
        Me.lblNroSol.Text = ""
        consultarDetalle()
        Me.cmdImprimir.Enabled = False
    End Sub

    Protected Sub cmdFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFinalizar.Click

        Dim obj As New ClsNoAdeudos
        'Dim objMail As New ClsMail
        Dim remitente As String
        Dim nombreRemitente As String
        Dim asunto As String
        Dim mensaje As String
        Dim destinatario As String
        Dim dtsDatosRemitente As Data.DataTable
        Dim dtsDatosDestinatario As Data.DataTable
        Dim nombreDestinatario As String
        Dim carreraDestinatario As String
        Dim codigo_alu As String  '12-03
        obj.FinalizarNoAdeudo(CInt(Me.lblNroSol.Text))
        consultarDetalle()
        cmdFinalizar.Enabled = False
        '*-* Enviar correo al estudiante indicadno que se ha culminado con el trámite de la solicitud
        ' obteniendo datos del remitente
        dtsDatosRemitente = obj.ConsultarDatosRemitente(id)

        remitente = dtsDatosRemitente.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"

        nombreRemitente = dtsDatosRemitente.Rows(0).Item("paterno").ToString & " " & dtsDatosRemitente.Rows(0).Item("materno").ToString & " " & dtsDatosRemitente.Rows(0).Item("nombres").ToString
        'obteniendo datos del destinatario
        dtsDatosDestinatario = obj.ConsultarNoAdeudos("I", Me.lblNroSol.Text)

        nombreDestinatario = dtsDatosDestinatario.Rows(0).Item("Alumno").ToString
        carreraDestinatario = dtsDatosDestinatario.Rows(0).Item("Escuela").ToString
        codigo_alu = dtsDatosDestinatario.Rows(0).Item("codigo_alu").ToString

        'configurando el mensaje
        asunto = "FINALIZADA LA REVISION DE NO ADEUDOS Solictud Nro. " & Me.lblNroSol.Text
        mensaje = "<font face=arial>Todos los revisores manifiestan que no existen observaciones y <b>se da por finalizado el proceso de revisión de NO ADEUDOS, Solicitud Nro. " & Me.lblNroSol.Text & ". " & nombreDestinatario & " de la escuela de " & carreraDestinatario & "</b>.<br><br>"
        mensaje = mensaje & "Atentamente. <br> <br> Campus Virtual - USAT<br><br>"
        mensaje = mensaje & "<font size=1>Imprima y Guarde este correo como prueba de acreditación para los fines que usted considere relevante <br>"
        mensaje = mensaje & "El correo electrónico expedido por del sistema de NO ADEUDOS de la Universidad Católica Santo Toribio de Mogrovejo está destinado únicamente para los fines correspondientes y cualquier otro uso contraviene las políticas de la institución.</br>"
        mensaje = mensaje & "Toda la información contenida en este mensaje es confidencial y de uso exclusivo de la USAT, su adulteración está prohibida. Si Ud. ha recibido este mensaje por error por favor proceda a eliminarlo y notificar al remitente.</font></font>"

        If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
            destinatario = dtsDatosDestinatario.Rows(0).Item("Correo").ToString
        Else
            destinatario = "cgastelo@usat.edu.pe" 'activar desarrollo
        End If

        '12-03 No se considera el remitente, va vacío
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ObjCnx.Ejecutar("ALUMNI_EnvioCorreosMasivoIUD", "I", id, 0, "codigo_alu", codigo_alu, 46, destinatario, "", asunto, mensaje, "", CDate(TODAY), 0, 0)

        '12-03 Se cambia la clase por Envio de Correo Masivo
        'objMail.EnviarMail("campusvirtual@usat.edu.pe", nombreRemitente, destinatario, asunto, mensaje, True, "", remitente)

        '--------------------------------------------------------
    End Sub

    Protected Sub gvdetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvdetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvdetalle','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvdetalle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvdetalle.SelectedIndexChanged
        cmdFinalizar.Enabled = True
        cmdEvaluar.Enabled = False
        rbObservado.Checked = False
        rbConforme.Checked = False
        Me.lblNroSol.Text = gvdetalle.SelectedRow.Cells(1).Text
        If Me.ddlEstadoRevisor.SelectedValue <> "C" Then
            rbConforme.Enabled = True
            rbObservado.Enabled = True
        End If
        mostrarRevision(Me.lblNroSol.Text)
    End Sub
    Private Sub mostrarRevision(ByVal codigo_cade As Integer)
        Dim obj As New ClsNoAdeudos
        gvRevision.Visible = True
        gvRevision.DataSource = obj.ConsultarRevisiones(codigo_cade)
        gvRevision.DataBind()
        Me.cmdImprimir.Enabled = True
    End Sub

    Protected Sub cmdEvaluar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEvaluar.Click
        Dim evaluacion As String
        Dim obj As New ClsNoAdeudos
        Dim dts As New Data.DataTable

        Dim objMail As New ClsMail
        Dim remitente As String
        Dim nombreRemitente As String
        Dim asunto As String
        Dim mensaje As String
        Dim mailRemite As String
        Dim destinatario As String
        Dim dtsDatosRemitente As Data.DataTable
        Dim dtsDatosDestinatario As Data.DataTable
        Dim observacion As String
        Dim codigo_alu As String  '12-03

        evaluacion = ""
        If rbObservado.Checked = True And Trim(txtObservacion.Text) = "" Then
            Me.lblValidaEvaluacion.Visible = True
            Me.rbObservado.Enabled = True
            Me.rbConforme.Enabled = True
            Me.cmdEvaluar.Enabled = True
        Else
            If rbConforme.Checked = True Then
                evaluacion = "C"
            End If
            If rbObservado.Checked = True Then
                evaluacion = "O"

                '*-* Enviar un correo al estudiante indicando que se ha observado su solicitud
                '    Indicar el área, persona y correo de quien observó

                dtsDatosRemitente = obj.ConsultarDatosRemitente(id)
                mailRemite = dtsDatosRemitente.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                nombreRemitente = dtsDatosRemitente.Rows(0).Item("paterno").ToString & " " & dtsDatosRemitente.Rows(0).Item("materno").ToString & " " & dtsDatosRemitente.Rows(0).Item("nombres").ToString
                observacion = Me.txtObservacion.Text

                'configurando el mensaje
                asunto = "CONSTANCIA NO ADEUDO OBSERVADA: Solictud Nro. " & Me.lblNroSol.Text
                mensaje = "<FONT FACE=arial> Durante la verificación uno de los revisores ha OBSERVADO El trámite de constancia de No ADEUDOS, Solicitud Nro. " & Me.lblNroSol.Text & " ." & vbCrLf & "</BR>"
                mensaje = mensaje & "REVISOR: <B>" & nombreRemitente & vbCrLf & "</B></BR>"
                mensaje = mensaje & "E-MAIL  : <B><FONT COLOR=BLUE> " & mailRemite & vbCrLf & "</FONT></B></BR>"
                mensaje = mensaje & "OBSERVACIÓN   : <b><FONT COLOR=RED>" & observacion & "</FONT></b>" & vbCrLf & vbCrLf & "</BR>" & "</BR>"

                mensaje = mensaje & "<FONT SIZE=1 > Imprima y Guarde este correo como prueba de acreditación para los fines que usted considere relevante" & vbCrLf & vbCrLf & "</BR>"
                mensaje = mensaje & "El correo electrónico expedido por del sistema de NO ADEUDOS de la Universidad Católica Santo Toribio de Mogrovejo está destinado únicamente para los fines correspondientes y cualquier otro uso contraviene las políticas de la institución." & vbCrLf & "</BR>"
                mensaje = mensaje & "Toda la información contenida en este mensaje es confidencial y de uso exclusivo del remitente y el destinatario, Su divulgación, copia o adulteración están prohibidas y sólo debe ser conocida por la persona a quien se dirige este mensaje. Si Ud. ha recibido este mensaje por error por favor proceda a eliminarlo y notificar al remitente.</FONT></FONT>"
                'obteniendo datos del destinatario
                dtsDatosDestinatario = obj.ConsultarNoAdeudos("I", Me.lblNroSol.Text)
                codigo_alu = dtsDatosDestinatario.Rows(0).Item("codigo_alu").ToString

                If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
                    destinatario = dtsDatosDestinatario.Rows(0).Item("Correo").ToString
                Else
                    destinatario = "cgastelo@usat.edu.pe" 'activar desarrollo
                End If

                'Se cambia la clase por Envio de Correo Masivo 12-03
                'objMail.EnviarMail("campusvirtual@usat.edu.pe", nombreRemitente, destinatario, asunto, mensaje, True, mailRemite, )

                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ObjCnx.Ejecutar("ALUMNI_EnvioCorreosMasivoIUD", "I", id, 0, "codigo_alu", codigo_alu, 46, destinatario, mailRemite, asunto, mensaje, "", CDate(TODAY), 0, 0)

                '--------------------------------------------------------------
            End If
            obj.EvaluarNoAdeudo(evaluacion, Me.txtObservacion.Text, Me.lblNroSol.Text, id)
            Me.lblValidaEvaluacion.Visible = False

            Dim nombreDestinatario As String
            Dim carreraDestinatario As String
            dts = obj.ConsultarUltimaRevision(Me.lblNroSol.Text)
            If dts.Rows(0).Item("Pendientes") = 0 Then ' en caso se la última revision debe enviar e-mails
                '*-* Enviar correo a dirección académica indicando que ya puede dar por finalizada la solicitud.
                dtsDatosRemitente = obj.ConsultarDatosRemitente(id)
                remitente = dtsDatosRemitente.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                nombreRemitente = dtsDatosRemitente.Rows(0).Item("paterno").ToString & " " & dtsDatosRemitente.Rows(0).Item("materno").ToString & " " & dtsDatosRemitente.Rows(0).Item("nombres").ToString
                observacion = Me.txtObservacion.Text
                'obteniendo datos del destinatario
                dtsDatosDestinatario = obj.ConsultarNoAdeudos("I", Me.lblNroSol.Text)
                destinatario = dtsDatosDestinatario.Rows(0).Item("Correo").ToString
                nombreDestinatario = dtsDatosDestinatario.Rows(0).Item("Alumno").ToString
                carreraDestinatario = dtsDatosDestinatario.Rows(0).Item("Escuela").ToString
                codigo_alu = dtsDatosDestinatario.Rows(0).Item("codigo_alu").ToString ' 12-03-2020
                'configurando el mensaje
                asunto = "FINALIZADA LA REVISION DE NO ADEUDOS Solictud Nro. " & Me.lblNroSol.Text
                mensaje = "<font face=arial>Todos los revisores manifiestan que no existen observaciones y <b>se da por finalizado el proceso de revisión de NO ADEUDOS, Solicitud Nro. " & Me.lblNroSol.Text & ". " & nombreDestinatario & " de la escuela de " & carreraDestinatario & "</b>.<br><br>"
                mensaje = mensaje & "Atentamente. <br> <br> Campus Virtual -USAT<br><br>"
                mensaje = mensaje & "<font size=1>Imprima y Guarde este correo como prueba de acreditación para los fines que usted considere relevante <br>"
                mensaje = mensaje & "El correo electrónico expedido por del sistema de NO ADEUDOS de la Universidad Católica Santo Toribio de Mogrovejo está destinado únicamente para los fines correspondientes y cualquier otro uso contraviene las políticas de la institución.</br>"
                mensaje = mensaje & "Toda la información contenida en este mensaje es confidencial y de uso exclusivo de la USAT, su adulteración está prohibida. Si Ud. ha recibido este mensaje por error por favor proceda a eliminarlo y notificar al remitente.</font></font>"
                '  mensaje = mensaje & "<br>" & destinatario & "<br>"
                '  destinatario = "esaavedra@usat.edu.pe"

                If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
                    destinatario = dtsDatosDestinatario.Rows(0).Item("Correo").ToString
                Else
                    destinatario = "cgastelo@usat.edu.pe" 'activar desarrollo
                End If

                'Se cambia la clase por Envio de Correo Masivo 12-03
                'objMail.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual -USAT", "ehernandez@usa.edu.pe", asunto, mensaje, True, "", "ehernandez@usat.edu.pe")

                Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                ObjCnx.Ejecutar("ALUMNI_EnvioCorreosMasivoIUD", "I", id, 0, "codigo_alu", codigo_alu, 46, destinatario, "", asunto, mensaje, "", CDate(TODAY), 0, 0)

            End If
            Refrescar()
        End If
    End Sub

    Protected Sub rbObservado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbObservado.CheckedChanged
        cmdEvaluar.Enabled = True
        Me.txtObservacion.Focus()
    End Sub

    Protected Sub rbConforme_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbConforme.CheckedChanged
        cmdEvaluar.Enabled = True
    End Sub

    Protected Sub gvRevision_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRevision.RowDataBound
        If gvRevision.Rows.Count > 0 Then
            If e.Row.Cells(2).Text = "Observado" Then
                e.Row.ForeColor = Drawing.Color.Red
            End If
            e.Row.Cells(3).Width = 300
        End If
    End Sub

    Protected Sub gvRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvRevision.SelectedIndexChanged

    End Sub

    Protected Sub cmdImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click
        If Me.lblNroSol.Text <> "" Then
            Response.Redirect("formatoImpresion.aspx?id=" & id & "&ctf=" & ctf & "&ade=" & Me.lblNroSol.Text)
        End If
    End Sub
End Class
