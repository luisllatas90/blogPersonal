Imports System.Collections.Generic

Partial Class Alumni_frmOfertaLaboral
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Dim cod_user As Integer = 0
    Dim codigo_tfu, codigo_test As String
    Public nro As Integer = 0
    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum
    'Datos
    Dim md_oferta As d_Oferta
    Dim md_funciones As d_Funciones
    Dim md_departamento As d_Departamento
    Dim md_sector As d_Sector
    Dim md_CarreraProfesional As d_CarreraProfesional
    Dim md_DetalleOfertaCarrera As d_DetalleOfertaCarrera
    Dim md_EgresadoAlumni As d_EgresadoAlumni
    Dim md_Personal As d_Personal
    Dim md_EnviarCorreo As d_EnvioCorreosMasivo
    Dim md_Empresa As d_Empresa

    'Entidades
    Dim me_oferta As e_Oferta
    Dim me_sector As e_Sector
    Dim me_departamento As e_Departamento
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_DetalleOfertaCarrera As e_DetalleOfertaCarrera
    Dim me_EgresadoAlumni As e_EgresadoAlumni
    Dim me_personal As e_Personal
    Dim me_EnviarCorreo As e_EnvioCorreosMasivo
    Dim me_Empresa As e_Empresa

    Dim memory As New System.IO.MemoryStream
    Dim bytes As Byte()

#End Region

#Region "Metodos"
    Private Sub InicializarControles()
        AddHandler btnListarOfertas.ServerClick, AddressOf btnListarOfertas_Click
        AddHandler btnNuevaOferta.ServerClick, AddressOf btnNuevaOferta_Click
    End Sub
    Private Sub CargarGrillaOfertas(ByVal fechaIniReg As String, ByVal fechaFinReg As String)
        me_oferta = New e_Oferta : md_oferta = New d_Oferta : md_funciones = New d_Funciones

        me_oferta.fechaIniReg = Me.txtFechIni.Text.Trim
        me_oferta.fechaFinReg = Me.txtFechFin.Text.Trim

        Dim dtOfertas As New Data.DataTable
        dtOfertas = md_oferta.LitarOferta(me_oferta)
        Me.gvListarOfertas.DataSource = dtOfertas
        Me.gvListarOfertas.DataBind()
        'dtOfertas.Dispose()

        Call md_funciones.AgregarHearders(gvListarOfertas)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)


    End Sub
    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
            Me.udpScripts.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mt_cargarCombosManuales()
        '***** Tipo Trabajo
        Me.ddlTipoTrabajo.Items.Clear()
        Me.ddlTipoTrabajo.Items.Add("[--SELECCIONE--]")
        Me.ddlTipoTrabajo.Items.Add("TIEMPO COMPLETO")
        Me.ddlTipoTrabajo.Items.Add("MEDIO TIEMPO")
        Me.ddlTipoTrabajo.Items.Add("POR HORAS")
        Me.ddlTipoTrabajo.Items.Add("BECAS/PRACTICAS")
        Me.ddlTipoTrabajo.Items.Add("DESDE CASA")
        '****** Tipo Oferta
        Me.ddlTipoOferta.Items.Clear()
        Me.ddlTipoOferta.Items.Add("[--SELECCIONE--]")
        Me.ddlTipoOferta.Items.Add("PRACTICAS PRE PROFESIONALES")
        Me.ddlTipoOferta.Items.Add("PRACTICAS PROFESIONALES")
        Me.ddlTipoOferta.Items.Add("OFERTA LABORAL PROFESIONAL")
        Me.ddlTipoOferta.Items.Add("OFERTA LABORAL ESTUDIANTE")
    End Sub
    Private Sub mt_cargarComboEstadoAlta()
        '***** Estado de la oferta
        Me.ddListEstado.Items.Clear()
        Me.ddListEstado.Items.Add("[--SELECCIONE--]")
        Me.ddListEstado.Items.Add("ACTIVA")
        Me.ddListEstado.Items.Add("POR REVISAR")
        'Me.ddListEstado.Items.Add("DAR ALTA")
        Me.ddListEstado.Items.Add("RECHAZADA")
        'Me.ddListEstado.Items.Add("DESACTIVADA")
    End Sub
    Private Sub mt_cargarComboDepartamento()
        md_funciones = New d_Funciones : md_departamento = New d_Departamento : me_departamento = New e_Departamento
        Try
            Dim dt As New Data.DataTable
            dt = md_departamento.BuscaDepartamento

            Call md_funciones.CargarCombo(Me.ddlDepartamento, dt, "codigo_Dep", "nombre_Dep", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Sub mt_cargarComboSector()
        md_funciones = New d_Funciones : md_sector = New d_Sector : me_sector = New e_Sector

        Try
            Dim dt As New Data.DataTable
            dt = md_sector.BuscaSector

            Call md_funciones.CargarCombo(Me.ddlSector, dt, "codigo_sec", "nombre_sec", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Sub mt_CargarFormOferta(ByVal codigo_ofe As String)
        Try
            mt_cargarCombosManuales()
            mt_cargarComboDepartamento()
            mt_cargarComboSector()

            Me.chkMostrarCorreo.Checked = True
            Me.rbPostCorreo.Checked = True


            If codigo_ofe <> "0" Then
                me_oferta = New e_Oferta : md_oferta = New d_Oferta
                Dim dtOferta As New Data.DataTable
                Me.txtCodigo_ofe.Text = codigo_ofe

                me_oferta.codigo_ofe = codigo_ofe

                dtOferta = md_oferta.ListaOfertaByCodOfe(me_oferta)
                If dtOferta.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado." & me_oferta.codigo_ofe, MessageType.warning) : Exit Sub

                With dtOferta.Rows(0)
                    Me.txtDescripcion.Text = .Item("descripcion_ofe").ToString
                    Me.txtTitulo.Text = .Item("titulo_ofe")
                    Me.txtFechIniPub.Text = .Item("fechaInicioAnuncio").ToString
                    Me.txtFechFinPub.Text = .Item("fechaFinAnuncio").ToString
                    Me.txtRequisitos.Text = .Item("requisitos_ofe")
                    Me.txtCorreo.Text = .Item("correocontacto_ofe")
                    Me.txtContacto.Text = .Item("contacto_ofe")
                    Me.txtLugar.Text = .Item("lugar_ofe")
                    Me.txtTelefono.Text = .Item("telefonocontacto_ofe")
                    Me.txtEmpresa.Text = .Item("nombrePro")
                    Me.ddlTipoTrabajo.Text = .Item("tipotrabajo_ofe")
                    Me.txtWebOfe.Text = .Item("web_ofe")
                    Me.hf_codigo_emp.Value = .Item("codigo_emp")
                    Me.hf_idPro.Value = .Item("idPro")
                    Me.hf_estadoOfe.Value = .Item("estado_ofe")
                    'combo de tipo de oferta laboral
                    Select Case dtOferta.Rows(0).Item("tipo_oferta").ToString.Trim
                        Case "PR"
                            Me.ddlTipoOferta.SelectedIndex = 1
                        Case "PP"
                            Me.ddlTipoOferta.SelectedIndex = 2
                        Case "OL"
                            Me.ddlTipoOferta.SelectedIndex = 3
                        Case "OE"
                            Me.ddlTipoOferta.SelectedIndex = 4
                    End Select
                    Me.ddlDepartamento.Text = .Item("codigo_dep")
                    Me.ddlSector.Text = .Item("codigo_sec")
                    'chkMostrar.Checked = .Item("visible_ofe")
                    Me.rbPostCorreo.Checked = False
                    Me.rbPostWeb.Checked = False
                    Me.rbPostCorreo.Checked = IIf(.Item("modopostular_ofe").ToString = "C", True, False)
                    Me.rbPostWeb.Checked = IIf(.Item("modopostular_ofe").ToString = "W", True, False)
                    Me.chkMostrarCorreo.Checked = IIf(.Item("mostrarcorreocontacto").ToString = "S", True, False)
                    Me.txtDescBanner.Text = .Item("desc_banner")
                    'Me.txt()
                End With
            Else
                Me.txtCodigo_ofe.Text = codigo_ofe
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarModalAlta(ByVal codigo_ofe As String)
        Try
            If Me.txtCodOfMod.Text <> "0" Then
                me_oferta = New e_Oferta : md_oferta = New d_Oferta
                Dim dtOferta As New Data.DataTable
                Me.txtCodOfeAlta.Text = codigo_ofe

                With me_oferta
                    .codigo_ofe = codigo_ofe
                End With

                dtOferta = md_oferta.ListaOfertaByCodOfe(me_oferta)
                If dtOferta.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Exit Sub

                With dtOferta.Rows(0)
                    Me.txtDescOfeAlta.Text = .Item("descripcion_ofe").ToString
                    Me.txtOfeLabAlta.Text = .Item("titulo_ofe")
                    Me.txtReqOfeAlta.Text = .Item("requisitos_ofe")
                    'combo de estado de oferta laboral
                    Select Case dtOferta.Rows(0).Item("estado_ofe").ToString.Trim
                        Case "A"
                            Me.ddListEstado.SelectedIndex = 1
                        Case "P"
                            Me.ddListEstado.SelectedIndex = 2
                        Case "L"
                            Me.ddListEstado.SelectedIndex = 3
                        Case "R"
                            Me.ddListEstado.SelectedIndex = 4
                        Case "D"
                            Me.ddListEstado.SelectedIndex = 5
                    End Select
                End With
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboProfesional()
        Try
            md_funciones = New d_Funciones : md_CarreraProfesional = New d_CarreraProfesional : me_CarreraProfesional = New e_CarreraProfesional

            Dim dtCarreraProfesional As New Data.DataTable
            me_CarreraProfesional.codigo_per = cod_user
            me_CarreraProfesional.codigo_tfu = codigo_tfu
            me_CarreraProfesional.codigo_test = codigo_test
            dtCarreraProfesional = md_CarreraProfesional.ListarCarreraProfesionalByAcceso(me_CarreraProfesional)
            Call md_funciones.CargarCombo(Me.ddlCarrrera, dtCarreraProfesional, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")

            dtCarreraProfesional.Dispose()

        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Sub
    Private Sub mt_CargarGrillaCarreras(ByVal codigo_ofe As String)
        Try
            me_DetalleOfertaCarrera = New e_DetalleOfertaCarrera : md_DetalleOfertaCarrera = New d_DetalleOfertaCarrera
            Dim dt As New Data.DataTable
            me_DetalleOfertaCarrera.codigo_ofe = codigo_ofe
            me_DetalleOfertaCarrera.codigo_cpf = "0"
            dt = md_DetalleOfertaCarrera.ListarDetalleOferta(me_DetalleOfertaCarrera)
            Me.gvCarreras.DataSource = dt
            Me.gvCarreras.DataBind()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub
    Private Sub mt_DeleteCarrera(ByVal codigo_ofe As String, ByVal codigo_cpf As String)
        me_DetalleOfertaCarrera = New e_DetalleOfertaCarrera : md_DetalleOfertaCarrera = New d_DetalleOfertaCarrera
        Dim dt As New Data.DataTable
        Try
            me_DetalleOfertaCarrera.codigo_ofe = codigo_ofe
            me_DetalleOfertaCarrera.codigo_cpf = codigo_cpf
            dt = md_DetalleOfertaCarrera.EliminarItemDetOferta(me_DetalleOfertaCarrera)
            mt_CargarGrillaCarreras(Me.txtCodOfMod.Text)
            udpModal.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_traeOferta(ByVal codigo_ofe As String)
        md_oferta = New d_Oferta : me_oferta = New e_Oferta
        me_oferta.codigo_ofe = codigo_ofe
        Dim dt As New Data.DataTable
        'Me.txtCodOfeMod.Text = codigo_ofe
        dt = md_oferta.ListaOfertaByCodOfe(me_oferta)
        With dt.Rows(0)
            Me.txtCodigo_ofeMod.Text = .Item("codigo_ofe")
            Me.txtTitOfeEmail.Text = .Item("titulo_ofe")
            Me.txtFechIniPubEmail.Text = .Item("fechaInicioAnuncio")
            Me.txtFechFinPubEmail.Text = .Item("fechaFinAnuncio")
        End With
    End Sub
    Private Sub mt_CargarComboCarreraModal(ByVal codigo_ofe As String)
        Try
            md_funciones = New d_Funciones : md_CarreraProfesional = New d_CarreraProfesional : me_CarreraProfesional = New e_CarreraProfesional
            Dim dt As New Data.DataTable
            'me_CarreraProfesional.c = cod_user
            'me_CarreraProfesional.codigo_tfu = codigo_tfu
            'me_CarreraProfesional.codigo_test = codigo_test
            dt = md_CarreraProfesional.ListarCarreraByCodOfe(codigo_ofe)
            Call md_funciones.CargarCombo(ddlCarrProfEmail, dt, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Response.Write(ex)
        End Try
    End Sub
    Private Sub mt_CargarDatosEgre()
        Try

            md_funciones = New d_Funciones : md_EgresadoAlumni = New d_EgresadoAlumni : me_EgresadoAlumni = New e_EgresadoAlumni
            Dim dt As New Data.DataTable

            If Me.gvEmailEgr.Rows.Count > 0 Then Me.gvEmailEgr.DataSource = Nothing : Me.gvEmailEgr.DataBind()

            With me_EgresadoAlumni
                .operacion = "LIS"
                .codigo_cpf = Me.ddlCarrProfEmail.SelectedValue.ToString
            End With

            dt = md_EgresadoAlumni.ListarEgresadoAlumni(me_EgresadoAlumni)
            Me.gvEmailEgr.DataSource = dt
            Me.gvEmailEgr.DataBind()

            'Me.txtCodOfeMod.Text = ddlCarrProfEmail.SelectedValue.ToString

            Call md_funciones.AgregarHearders(gvEmailEgr)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaEgre", "formatoGrillaEgre();", True)

            Me.udpFiltros.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Function mt_EnviarCorreo2(ByVal codigo_ofe As String) As Boolean

        Dim correosEnviados As Integer = 0
        Dim correosNoEnviados As Integer = 0
        Dim sincorreo As Integer = 0
        Dim StrCuentas As String = ""
        Dim para As String = ""
        Dim Mensaje As String = ""
        Dim ls_asunto As String = String.Empty

        ''Traer la oferta
        me_oferta = New e_Oferta : md_oferta = New d_Oferta
        Dim dtOferta As New Data.DataTable
        me_oferta.codigo_ofe = codigo_ofe

        dtOferta = md_oferta.ListaOfertaByCodOfe(me_oferta)

        With dtOferta.Rows(0)
            ls_asunto = "USAT ALUMNI - OFERTA LABORAL: " & .Item("titulo_ofe").ToString
            'Mensaje = "<br>" & .Item("titulo_ofe").ToString & "<br>"
            Mensaje = Mensaje & "<p>Tenemos la siguiente oferta para ti:&nbsp;</p>"
            Mensaje = Mensaje & "<p style='text-align: center;'><span style='background-color: #e33439; color: #fff; display: inline-block; padding: 3px 10px; font-weight: bold; border-radius: 5px;'>" & .Item("titulo_ofe").ToString & "</span>&nbsp;</p>"
            Mensaje = Mensaje & "<p style='text-align: left;'>Por encargo de nuestro cliente:&nbsp;<strong>" & .Item("nombrePro").ToString & "</strong></p>"
            Mensaje = Mensaje & "<p style='text-align: left;'>" & .Item("descripcion_ofe").ToString & "</p><p style='text-align: left;'>&nbsp;</p><p>&nbsp;</p>"
            Mensaje = Mensaje & "<p>&nbsp;</p>"
            Mensaje = Mensaje & "<p style='text-align: left;'>Ingresa a tu campus virtual de egresado en la opci&oacute;n Ofertas Laborales&gt;&gt;Consultas Ofertas.</p>"
        End With

        Try
            If Mensaje <> "" Then
                md_funciones = New d_Funciones : me_personal = New e_Personal : md_Personal = New d_Personal : md_EnviarCorreo = New d_EnvioCorreosMasivo
                Dim dt As New Data.DataTable

                me_personal.codigo_per = cod_user
                dt = md_Personal.ObtenerFirmaAlumni(me_personal)

                If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información para la firma del correo.", MessageType.warning) : Return False

                'Obtenemos los datos de la firma del correo
                Dim ln_seleccionados As Integer = 0
                Dim ln_correosEnviados As Integer = 0
                Dim ln_correosNoValidos As Integer = 0
                Dim ln_sinCorreo As Integer = 0
                Dim ls_cuentas As String = String.Empty
                Dim ls_para As String = String.Empty
                Dim ls_mensaje As String = String.Empty
                Dim ls_nombrePer As String = String.Empty

                'Dim ls_replyTo As String = "alumni@usat.edu.pe"
                'Dim ls_replyTo As String = "olluen@usat.edu.pe"

                Dim ls_replyTo As String = g_VariablesGlobales.CorreoAlumni
                If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba

                Dim ls_ruta As String = String.Empty
                Dim ls_MensajeFinal As String = String.Empty
                'Dim ls_nombreArchivo As String = String.Empty
                'Dim ls_fileName As String = String.Empty

                'nombre para la firma
                ls_nombrePer = dt.Rows(0).Item("nombreper").ToString

                'COORDINADOR DE ALUMNI
                'If Request.QueryString("ctf") = 145 Then ls_replyTo = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
                'If Session("cod_ctf") = 145 Then ls_replyTo = "olluen@usat.edu.pe"

                'COORDINADOR DE ALUMNI
                If Session("cod_ctf") = g_VariablesGlobales.TipoFuncionCoordinadorAlumni Then ls_replyTo = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
                If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba

                'Enviamos                
                If Me.gvEmailEgr.Rows.Count > 0 Then
                    For Each Fila As GridViewRow In Me.gvEmailEgr.Rows
                        If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                            ln_seleccionados += 1

                            If md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString) OrElse _
                                md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString) Then

                                ls_mensaje = "<font face='Trebuchet MS'>"
                                'ls_mensaje &= IIf(Fila.Cells(7).Text = "F", "Estimada ", "Estimado")
                                ls_mensaje &= "Estimado(a) "
                                'ls_mensaje &= Fila.Cells(4).Text & ":<br /><br />"
                                ls_mensaje &= Fila.Cells(2).Text & ":<br /><br />"
                                ls_mensaje &= Mensaje
                                ls_mensaje &= mt_ObtenerFirmaMensaje(ls_nombrePer)
                                ls_mensaje &= "</font>"


                                'Call mt_ShowMessage(ls_mensaje, MessageType.error)

                                If md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString) Then
                                    ls_para = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString
                                Else
                                    ls_para = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString
                                End If

                                If ConfigurationManager.AppSettings("CorreoUsatActivo") = 0 Then ls_para = g_VariablesGlobales.CorreoPrueba

                                'Enviar correo masivo
                                me_EnviarCorreo = md_EnviarCorreo.GetEnvioCorreosMasivo(0)
                                dt = New Data.DataTable

                                With me_EnviarCorreo
                                    .operacion = "I"
                                    .cod_user = cod_user
                                    .tipoCodigoEnvio_ecm = "codigo_pso"
                                    .codigoEnvio_ecm = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("codigo_Pso").ToString
                                    .asunto = ls_asunto
                                    .correo_destino = ls_para
                                    .cuerpo = ls_mensaje
                                    .codigo_apl = 47
                                    .correo_respuesta = ls_replyTo
                                    .archivo_adjunto = ls_ruta
                                End With
                                dt = md_EnviarCorreo.RegistrarEnvioCorreosMasivo(me_EnviarCorreo)

                                ln_correosEnviados += 1

                                'Insertar en bitacora
                                me_EnviarCorreo = md_EnviarCorreo.GetEnvioCorreosMasivo(0)
                                With me_EnviarCorreo
                                    .fecha_envio = Date.Now
                                    .codigoEnvio_ecm = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("codigo_Pso").ToString
                                    .correo_destino = ls_para
                                    .asunto = ls_asunto
                                    ''aqui un pequeño atajo para relacionarlo el envio a la persona y a la oferta
                                    .archivo_adjunto = codigo_ofe
                                End With

                                md_EnviarCorreo.RegistrarBitacoraEnvio(me_EnviarCorreo)

                            Else
                                If String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                                String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString) Then
                                    ln_sinCorreo += 1
                                Else
                                    ln_correosNoValidos += 1
                                End If

                                ls_cuentas &= "<tr><td></td><td>" & _
                                    Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString & "</td><td>" & _
                                    Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString & "</td></tr>"
                            End If
                        End If
                    Next
                    ls_MensajeFinal = "Correos Enviados: " & correosEnviados.ToString & "  de  " & nro & " destinatario(s) seleccionado(s).Usuarios sin correo registrado: " & sincorreo & ". Usuarios con correos inválidos: " & correosNoEnviados

                End If
                Return True
            End If
            'EnviarMensajeNotificacion(StrCuentas, txtMensaje.Value, txtAsunto.Text, Me.FileUpload1.FileName, nro, correosEnviados, correosNoEnviados, sincorreo, nombreper)
        Catch ex As Exception
            'Response.Write(ex)
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            'Me.lblMensajeFormulario.Text = ex.Message & " " & ex.StackTrace.ToString
            'Me.lblMensajeFormulario.Text = "Correos Enviados: " & correosEnviados.ToString & "  de  " & nro & " destinatario(s) seleccionado(s). Usuarios sin correo registrado: " & sincorreo
        End Try
    End Function

    Private Function mt_EnviarCorreo(ByVal codigo_ofe As String) As Boolean
        Try
            me_personal = New e_Personal : md_EnviarCorreo = New d_EnvioCorreosMasivo : md_Personal = New d_Personal
            md_funciones = New d_Funciones
            Dim dt As New Data.DataTable

            me_personal.codigo_per = cod_user
            me_personal.codigo_tfu = Session("cod_ctf")

            dt = md_Personal.ObtenerFirmaAlumni(me_personal)

            If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información para la firma del correo.", MessageType.warning) : Return False

            'Obtenemos los datos de la firma del correo
            Dim ln_seleccionados As Integer = 0
            Dim ln_correosEnviados As Integer = 0
            Dim ln_correosNoValidos As Integer = 0
            Dim ln_sinCorreo As Integer = 0
            Dim ls_cuentas As String = String.Empty
            Dim ls_para As String = String.Empty
            Dim ls_mensaje As String = String.Empty
            Dim ls_cuerpoMensaje As String = String.Empty
            Dim ls_asunto As String = String.Empty
            Dim ls_nombrePer As String = String.Empty
            Dim ls_replyTo As String = g_VariablesGlobales.CorreoAlumni
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba
            Dim ls_ruta As String = String.Empty
            Dim ls_nombreArchivo As String = String.Empty
            Dim ls_FirmaMensaje As String = String.Empty

            ls_nombrePer = dt.Rows(0).Item("nombreper").ToString
            me_personal.nombres_per = ls_nombrePer

            'Obtener firma del mensaje
            ls_FirmaMensaje = md_funciones.FirmaMensajeAlumni(me_personal)

            'COORDINADOR DE ALUMNI
            If Session("cod_ctf") = g_VariablesGlobales.TipoFuncionCoordinadorAlumni Then ls_replyTo = dt.Rows(0).Item("usuario_per").ToString + "@usat.edu.pe"
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_replyTo = g_VariablesGlobales.CorreoPrueba

            'OBTENER DATOS DE LA OFERTA            
            Dim dtOferta As New Data.DataTable
            me_oferta = New e_Oferta : md_oferta = New d_Oferta

            me_oferta.codigo_ofe = codigo_ofe
            dtOferta = md_oferta.ListaOfertaByCodOfe(me_oferta)

            If dtOferta.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información de la oferta laboral.", MessageType.warning) : Return False

            With dtOferta.Rows(0)
                ls_asunto = "USAT ALUMNI - OFERTA LABORAL: " & .Item("titulo_ofe").ToString
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p>La Coordinación de Alumni te invita a postular a la siguiente oferta laboral:&nbsp;</p>"
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p style='text-align: center;'><span style='background-color: #e33439; color: #fff; display: inline-block; padding: 3px 10px; font-weight: bold; border-radius: 5px;'>" & .Item("titulo_ofe").ToString & "</span>&nbsp;</p>"
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p style='text-align: left;'>Recuerda que primero deberás ingresar a tu campus virtual como egresado y actualizar tus datos.</p>"
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p style='text-align: left;'>En caso hayas olvidado tu clave, envía un correo a alumni.usat@gmail.com para hacerte llegar una nueva.</p>"
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p>&nbsp;</p>"
                ls_cuerpoMensaje = ls_cuerpoMensaje & "<p style='text-align: left;'>¡Suerte!</p>"
            End With

            If Me.gvEmailEgr.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.gvEmailEgr.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        ln_seleccionados += 1

                        If (Not String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                            md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim)) OrElse _
                            (Not String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString.Trim) AndAlso _
                             md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString)) Then

                            ls_mensaje = g_VariablesGlobales.AbrirFormatoCorreoAlumni
                            ls_mensaje &= "Estimado(a) "
                            ls_mensaje &= Fila.Cells(2).Text & ":<br /><br />"
                            ls_mensaje &= ls_cuerpoMensaje
                            ls_mensaje &= ls_FirmaMensaje
                            ls_mensaje &= g_VariablesGlobales.CerrarFormatoCorreoAlumni

                            If Not String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                                md_funciones.ValidarEmail(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString) Then
                                ls_para = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString
                            Else
                                ls_para = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString
                            End If

                            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_para = g_VariablesGlobales.CorreoPrueba

                            'Enviar correo masivo
                            me_EnviarCorreo = md_EnviarCorreo.GetEnvioCorreosMasivo(0)
                            dt = New Data.DataTable

                            With me_EnviarCorreo
                                .operacion = "I"
                                .cod_user = cod_user
                                .tipoCodigoEnvio_ecm = "codigo_pso"
                                .codigoEnvio_ecm = Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("codigo_Pso").ToString
                                .codigo_apl = g_VariablesGlobales.CodigoAplicacionAlumni
                                .correo_destino = ls_para
                                .correo_respuesta = ls_replyTo
                                .asunto = ls_asunto
                                .cuerpo = ls_mensaje
                                .archivo_adjunto = ls_ruta
                            End With

                            dt = md_EnviarCorreo.RegistrarEnvioCorreosMasivo(me_EnviarCorreo)

                            ln_correosEnviados += 1

                        Else
                            If String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString.Trim) AndAlso _
                                String.IsNullOrEmpty(Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString) Then
                                ln_sinCorreo += 1
                            Else
                                ln_correosNoValidos += 1
                            End If

                            ls_cuentas &= "<tr><td>" & Fila.Cells(4).Text & " " & Fila.Cells(3).Text & "</td><td>" & _
                                            Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_personal").ToString & "</td><td>" & _
                                            Me.gvEmailEgr.DataKeys(Fila.RowIndex).Item("correo_profesional").ToString & "</td></tr>"
                        End If
                    End If
                Next
            End If

            Call mt_EnviarMensajeNotificacion(ls_cuentas, ls_cuerpoMensaje, ls_asunto, ls_nombreArchivo, ln_seleccionados, ln_correosEnviados, ln_correosNoValidos, ln_sinCorreo, ls_nombrePer)

            Dim ls_mensajeAlerta As String = String.Empty
            ls_mensajeAlerta = "Correos Enviados: " & ln_correosEnviados.ToString & "  de  " & ln_seleccionados.ToString & " destinatario(s) seleccionado(s).Usuarios sin correo registrado: " & ln_sinCorreo.ToString & ".  Usuarios con correos inválidos: " & ln_correosNoValidos.ToString

            Call mt_ShowMessage(ls_mensajeAlerta, MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EnviarMensajeNotificacion(ByVal ls_cuentas As String, ByVal ls_mensaje As String, ByVal ls_asunto As String, ByVal ls_adjunto As String, ByVal ln_seleccionados As Integer, _
                                                  ByVal ln_correosEnviados As Integer, ByVal ln_correosNoValidos As Integer, ByVal ln_sinCorreo As Integer, ByVal ls_nombrePer As String) As Boolean
        Try
            me_CarreraProfesional = New e_CarreraProfesional : md_EnviarCorreo = New d_EnvioCorreosMasivo : md_CarreraProfesional = New d_CarreraProfesional
            me_EnviarCorreo = md_EnviarCorreo.GetEnvioCorreosMasivo(0)
            Dim dt As Data.DataTable

            Dim ls_mensajeEnviar As String = String.Empty

            'Listar coordinadores por escuela
            dt = New Data.DataTable

            With me_CarreraProfesional
                .operacion = "PER"
                .codigo_per = cod_user
            End With

            dt = md_CarreraProfesional.ListarCarreraProfesional(me_CarreraProfesional)

            Dim ls_escuela As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If String.IsNullOrEmpty(ls_escuela) Then
                    ls_escuela = dt.Rows(i).Item("nombre_Cpf").ToString
                Else
                    ls_escuela = ls_escuela + ", " + dt.Rows(i).Item("nombre_Cpf").ToString
                End If
            Next

            ls_mensajeEnviar &= g_VariablesGlobales.AbrirFormatoCorreoAlumni
            ls_mensajeEnviar &= "<b>Notificación de Envío de Correo</b><hr /><br />"
            ls_mensajeEnviar &= "<b>Fecha: </b>" & Now.Date & "<br />"
            ls_mensajeEnviar &= "<b>Asunto: </b>" & ls_asunto & "<br />"
            ls_mensajeEnviar &= "<b>Mensaje: </b>" & ls_mensaje & "<br />"
            ls_mensajeEnviar &= "<b>Adjunto: </b>" & ls_adjunto & "<br /><br />"
            ls_mensajeEnviar &= "<b>Total Destinatarios: </b>" & ln_seleccionados.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Destinatarios sin correo registrado: </b>" & ln_sinCorreo.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mensajes Enviados: </b>" & ln_correosEnviados.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mensajes Fallidos: </b>" & ln_correosNoValidos.ToString & "<br />"
            ls_mensajeEnviar &= "<b>Mail enviado por: </b>" & ls_nombrePer & "<br />"
            ls_mensajeEnviar &= "<b>Escuela: </b>" & ls_escuela & "<br />"

            If Not String.IsNullOrEmpty(ls_cuentas) Then
                ls_mensajeEnviar &= "<br /><b>Detalle de correos fallidos: </b>"
                ls_mensajeEnviar &= "<br /><br /><table border=""1"" style=""border:1px solid black;""><tr><th>Nombres Apellidos</th><th>Correo Personal</th><th>Correo Profesional</th></tr>"
                ls_mensajeEnviar &= ls_cuentas
                ls_mensajeEnviar &= "</table>"
            End If

            ls_mensajeEnviar &= "<br /><hr /><br />"
            ls_mensajeEnviar &= "<b>CampusVirtual USAT</b>"
            ls_mensajeEnviar &= g_VariablesGlobales.CerrarFormatoCorreoAlumni

            Dim ls_para As String = g_VariablesGlobales.CorreoCoordinadorAlumni
            If Not (ConfigurationManager.AppSettings("CorreoUsatActivo") = 1) Then ls_para = g_VariablesGlobales.CorreoPrueba

            'Enviar correo masivo
            dt = New Data.DataTable

            With me_EnviarCorreo
                .operacion = "I"
                .cod_user = cod_user
                .tipoCodigoEnvio_ecm = "codigo_pso"
                .codigoEnvio_ecm = g_VariablesGlobales.PersonalCoordinadorAlumni
                .codigo_apl = g_VariablesGlobales.CodigoAplicacionAlumni
                .correo_destino = ls_para
                .asunto = "Módulo de Alumni USAT - Notificación de Envío de Correo"
                .cuerpo = ls_mensajeEnviar
            End With

            dt = md_EnviarCorreo.RegistrarEnvioCorreosMasivo(me_EnviarCorreo)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_LimpiarControles()
        'Me.ddlDepartamento.SelectedIndex = 0
        Me.ddlTipoTrabajo.SelectedIndex = 0
        'Me.ddlSector.SelectedIndex = 0
        Me.txtEmpresa.Text = String.Empty
        Me.txtCodigo_ofe.Text = ""
        Me.txtTitulo.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.txtRequisitos.Text = String.Empty
        Me.txtContacto.Text = String.Empty
        Me.txtCorreo.Text = String.Empty
        Me.txtTelefono.Text = String.Empty
        Me.txtLugar.Text = String.Empty
        Me.txtFechIniPub.Text = String.Empty
        Me.txtFechFinPub.Text = String.Empty
        Me.txtWebOfe.Text = String.Empty
        Me.rbPostWeb.Checked = True
        Me.chkMostrarCorreo.Checked = True
        Me.hdCodigo_ofe.Value = String.Empty

    End Sub
    Private Sub mt_ListarEmpresas()
        Try
            me_Empresa = New e_Empresa : md_Empresa = New d_Empresa : md_funciones = New d_Funciones
            Dim dt As New Data.DataTable

            If Me.grwListaEmpresa.Rows.Count > 0 Then Me.grwListaEmpresa.DataSource = Nothing : Me.grwListaEmpresa.DataBind()

            With me_Empresa
                .operacion = "GEN"
                .nombreComercial_emp = Me.txtNombreComercialFiltro.Text.Trim
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            Me.grwListaEmpresa.DataSource = dt

            Me.grwListaEmpresa.DataBind()

            Call md_funciones.AgregarHearders(grwListaEmpresa)

            'Call mt_UpdatePanel("ListaEmpresa")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per") 'Request.QueryString("id")
            codigo_tfu = Request.QueryString("ctf") '1
            codigo_test = "2" 'Request.QueryString("mod")

            'cod_user = "684" 'Request.QueryString("id")
            'codigo_tfu = "1" '1
            'codigo_test = "2" 'Request.QueryString("mod")


            InicializarControles()

            If IsPostBack = False Then

                Dim PrimerDiaDelMes As String
                'UltimoDiaDelMes = DateSerial(Year(dtmFecha), Month(dtmFecha) + 1, 0)
                PrimerDiaDelMes = DateSerial(Year(DateTime.Now), Month(DateTime.Now), 1)
                'Me.txtPrueba.Text = PrimerDiaDelMes
                txtFechIni.Text = PrimerDiaDelMes '"01/10/2019" DateTime.Now.ToString("dd/MM/yyyy")
                txtFechFin.Text = DateTime.Now.ToString("dd/MM/yyyy")
                Call CargarGrillaOfertas(Me.txtFechIni.Text, Me.txtFechFin.Text)
                Me.updListOfertas.Update()

            End If



        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFechaIni", "udpFechaIni();", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFechaFin", "udpFechaFin();", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFechaIniMod", "udpFechaIniMod();", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFechaFinModal", "udpFechaFinModal();", True)
    End Sub
    Protected Sub gvListarOfertas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListarOfertas.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Dim codigo_ofe As String

            codigo_ofe = Me.gvListarOfertas.DataKeys(index).Values("codigo_ofe")
            Select Case e.CommandName
                Case "EditOfe"
                    mt_CargarFormOferta(codigo_ofe)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
                    Me.udpRegistro.Update()
                Case "AddCarr"
                    mt_CargarComboProfesional()
                    mt_CargarGrillaCarreras(codigo_ofe)
                    Me.txtCodOfMod.Text = codigo_ofe
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModal", "OpenModal('');", True)
                    Me.udpModal.Update()
                Case "GenBanner"
                    Session("codigo_ofe") = codigo_ofe
                    Response.Redirect("~/Alumni/frmGenBannerSes.aspx")
                    'Response.Redirect("~/frmGenBannerSes.aspx")
                    'Me.udpPanelBanner.Update()
                Case "enviaEmail"
                    mt_traeOferta(codigo_ofe)
                    mt_CargarComboCarreraModal(codigo_ofe)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('envio-tab');", True)
                    Me.udpEnviarMail.Update()
                Case "AddCarr"
                    mt_CargarComboProfesional()
                    mt_CargarGrillaCarreras(codigo_ofe)
                    Me.txtCodOfMod.Text = codigo_ofe
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModal", "OpenModal('');", True)                    'Me.udpRegistro.Update()

                Case "darAlta"
                    mt_cargarComboEstadoAlta()
                    mt_CargarModalAlta(codigo_ofe)
                    Me.txtCodOfeAlta.Text = codigo_ofe
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModalAlta", "OpenModalAlta();", True)
                    udpModalAlta.Update()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub gvCarreras_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCarreras.RowCommand
        Try

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Dim codigo_cpf As String

            codigo_cpf = Me.gvCarreras.DataKeys(index).Values("codigo_cpf")

            Select Case e.CommandName
                Case "DeleteCarr"
                    mt_DeleteCarrera(Me.txtCodOfMod.Text, codigo_cpf)
                    mt_CargarGrillaCarreras(Me.txtCodOfMod.Text)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModal", "OpenModal('');", True)
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnGuardarOferta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarOferta.Click
        Dim vFechIniPub As Date
        Dim vFechFinPub As Date
        Try
            me_oferta = New e_Oferta : md_oferta = New d_Oferta

            Dim dtInserta As New Data.DataTable

            Dim ls_tipoOferta As String = ""

            Select Case ddlTipoOferta.SelectedItem.ToString
                Case "PRACTICAS PRE PROFESIONALES"
                    ls_tipoOferta = "PR"
                Case "PRACTICAS PROFESIONALES"
                    ls_tipoOferta = "PP"
                Case "OFERTA LABORAL PROFESIONAL"
                    ls_tipoOferta = "OL"
                Case "OFERTA LABORAL ESTUDIANTE"
                    ls_tipoOferta = "OE"
            End Select

            With me_oferta

                .codigo_ofe = Me.txtCodigo_ofe.Text
                .idPro = 0 '---Me.hf_idPro.Value
                .codigo_dep = Me.ddlDepartamento.SelectedValue
                .titulo_ofe = Me.txtTitulo.Text
                .descripcion_ofe = Me.txtDescripcion.Text
                .requisitos_ofe = Me.txtRequisitos.Text
                .contacto_ofe = Me.txtContacto.Text
                .correocontacto_ofe = Me.txtCorreo.Text
                .telefonocontacto_ofe = Me.txtTelefono.Text
                .lugar_ofe = Me.txtLugar.Text
                .tipotrabajo_ofe = Me.ddlTipoTrabajo.Text
                .duracion_ofe = "00 DIAS"
                .fechaInicioAnuncio = Me.txtFechIniPub.Text
                .fechaFinAnuncio = Me.txtFechFinPub.Text
                .codigo_sec = Me.ddlSector.SelectedValue
                .visible_ofe = True

                If Me.hf_codigo_emp.Value = "0" Then
                    .estado_ofe = Me.hf_estadoOfe.Value
                Else
                    .estado_ofe = "P"
                End If

                .web_ofe = Me.txtWebOfe.Text
                .modopostular_ofe = IIf(Me.rbPostCorreo.Checked, "C", "W")
                .mostrarcorreocontacto = IIf(Me.chkMostrarCorreo.Checked, "S", "N")
                .tipo_oferta = ls_tipoOferta
                .desc_banner = Me.txtDescBanner.Text
                .codigo_emp = Me.hf_codigo_emp.Value
            End With

            If Me.txtTitulo.Text = "" Then
                mt_ShowMessage("Debe ingresar el título de la oferta.", MessageType.warning)
                Me.udpRegistro.Update()
                Me.txtTitulo.Focus()

            ElseIf Me.hf_codigo_emp.Value = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe seleccionar al menos una empresa", MessageType.warning)
                Me.txtDescripcion.Focus()

            ElseIf Me.txtDescripcion.Text = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe ingresar la descripción de la oferta.", MessageType.warning)
                Me.txtDescripcion.Focus()

            ElseIf Me.txtRequisitos.Text = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe ingresar los requisitos de la oferta.", MessageType.warning)
                Me.txtRequisitos.Focus()
            ElseIf Me.ddlDepartamento.SelectedIndex = 0 Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe seleccionar el departamento de la oferta.", MessageType.warning)
                Me.ddlDepartamento.Focus()
                'ElseIf Me.txtLugar.Text = "" Then
                '    Me.udpRegistro.Update()
                '    mt_ShowMessage("Debe seleccionar el lugar de la oferta.", MessageType.warning)
                '    Me.txtRequisitos.Focus()
            ElseIf Me.ddlTipoOferta.SelectedIndex = 0 Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe seleccionar el tipo de oferta.", MessageType.warning)
                Me.ddlTipoOferta.Focus()
            ElseIf Me.ddlSector.SelectedIndex = 0 Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe seleccionar el sector de la oferta.", MessageType.warning)
                Me.ddlSector.Focus()
            ElseIf Me.txtContacto.Text = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe ingresar el contacto de la oferta.", MessageType.warning)
                Me.txtContacto.Focus()
                ' no se validará el teléfonno del contacto de la oferta
                'ElseIf Me.txtTelefono.Text = "" Then
                '    Me.udpRegistro.Update()
                '    mt_ShowMessage("Debe ingresar el teléfono del contacto de la oferta.", MessageType.warning)
                '    Me.txtTelefono.Focus()
            ElseIf Me.ddlTipoTrabajo.SelectedIndex = 0 Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Debe seleccionar el tipo de trabajo la oferta.", MessageType.warning)
                Me.ddlSector.Focus()
                'ElseIf Me.txtCorreo.Text = "" Then
                '    Me.udpRegistro.Update()
                '    mt_ShowMessage("Debe ingresar el correo de la oferta.", MessageType.warning)
                '    Me.txtCorreo.Focus()
            ElseIf Me.txtCorreo.Text <> "" And email_bien_escrito(Me.txtCorreo.Text) = False Then
                Me.udpRegistro.Update()
                mt_ShowMessage("El correo esta mal escrito.", MessageType.warning)
                Me.txtCorreo.Focus()
            ElseIf Me.rbPostWeb.Checked = True And Me.txtWebOfe.Text = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Si la postulación es mediante web, debe ingresar la dirección de la página", MessageType.warning)
                Me.txtWebOfe.Focus()
            ElseIf Me.rbPostCorreo.Checked = True And Me.txtCorreo.Text = "" Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Si la postulación es mediante correo, debe ingresar el correo", MessageType.warning)
                Me.txtCorreo.Focus()
            ElseIf Not DateTime.TryParse(Me.txtFechIniPub.Text, vFechIniPub) Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Ingrese fecha de inicio de publicación, valida", MessageType.warning)
                Me.txtCorreo.Focus()
            ElseIf Not DateTime.TryParse(Me.txtFechFinPub.Text, vFechFinPub) Then
                Me.udpRegistro.Update()
                mt_ShowMessage("Ingrese fecha de fin de publicación, valida", MessageType.warning)
                Me.txtCorreo.Focus()
            ElseIf CDate(vFechFinPub) < CDate(vFechIniPub) Then
                Me.udpRegistro.Update()
                mt_ShowMessage("La fecha de fin de publicación no puede ser menor que la de inicio", MessageType.warning)
                Me.txtCorreo.Focus()

                'ElseIf Me.txtDescripcion.Text = "" Then
                '    Me.udpRegistro.Update()
                '    mt_ShowMessage("Debe ingresar la descripción de la oferta.", MessageType.warning)
                '    Me.txtDescripcion.Focus()

            Else
                Me.udpRegistro.Update()
                dtInserta = md_oferta.InsertaUpdateOferta(me_oferta)
                mt_ShowMessage("Se registró la oferta con éxito", MessageType.success)
                mt_LimpiarControles()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
                CargarGrillaOfertas(Me.txtFechIni.Text, Me.txtFechFin.Text)
                Me.updListOfertas.Update()

            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnGuardarAltaOfe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAltaOfe.Click
        'artificio para actualizar la pagina y que reconozca los comandos
        Me.udpScripts.Update()
        '---------------------------------------------------------------
        Try

            me_oferta = New e_Oferta : md_oferta = New d_Oferta : me_DetalleOfertaCarrera = New e_DetalleOfertaCarrera : md_DetalleOfertaCarrera = New d_DetalleOfertaCarrera

            Dim ls_estadoOfe As String = ""

            Select Case ddListEstado.SelectedItem.ToString
                Case "ACTIVA"
                    ls_estadoOfe = "A"
                Case "POR REVISAR"
                    ls_estadoOfe = "P"
                Case "RECHAZADA"
                    ls_estadoOfe = "R"
            End Select

            If ls_estadoOfe = "" Then mt_ShowMessage("Debe seleccionar un estado", MessageType.warning) : Me.ddListEstado.Focus() : Exit Sub



            With me_oferta
                .codigo_ofe = Me.txtCodOfeAlta.Text
                .estado_ofe = ls_estadoOfe
            End With

            '--- valido si se le ha adicionado carrera profesional a la oferta
            If ls_estadoOfe = "A" Then
                Dim dtVerifica As New Data.DataTable
                me_DetalleOfertaCarrera.codigo_ofe = Me.txtCodOfeAlta.Text
                me_DetalleOfertaCarrera.codigo_cpf = "0"
                dtVerifica = md_DetalleOfertaCarrera.ListarDetalleOferta(me_DetalleOfertaCarrera)
                If dtVerifica.Rows.Count = 0 Then mt_ShowMessage("Debe asignar carrera profesional a la oferta", MessageType.warning) : Me.ddlCarrrera.Focus() : Exit Sub
            End If

            Dim dtEstado As New Data.DataTable

            dtEstado = md_oferta.ActualizarEstadoOferta(me_oferta)
            mt_ShowMessage("Se actualizó el estado de la oferta", MessageType.success)

            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "noValidaModAlta", "noValidaModAlta('SE ACTUALIZÓ EL ESTADO LA OFERTA');", True)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub lbAddCarr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddCarr.Click
        me_DetalleOfertaCarrera = New e_DetalleOfertaCarrera : md_DetalleOfertaCarrera = New d_DetalleOfertaCarrera
        Dim dt As New Data.DataTable
        Dim dtValida As New Data.DataTable
        Try
            If Me.ddlCarrrera.SelectedValue = "" Then
                udpScripts.Update()
                Call mt_ShowMessage("Debe seleccionar una carrera", MessageType.warning)

            Else
                me_DetalleOfertaCarrera.codigo_ofe = Me.txtCodOfMod.Text
                me_DetalleOfertaCarrera.codigo_cpf = Me.ddlCarrrera.SelectedValue
                me_DetalleOfertaCarrera.accion = "VER"


                dtValida = md_DetalleOfertaCarrera.ListarDetalleOferta(me_DetalleOfertaCarrera)

                If dtValida.Rows.Count > 0 Then mt_ShowMessage("Ya se ha agregado esta carrera profesional", MessageType.warning) : Me.ddlCarrrera.Focus() : Exit Sub

                dt = md_DetalleOfertaCarrera.InsertarDetalleOferta(me_DetalleOfertaCarrera)
                mt_CargarGrillaCarreras(Me.txtCodOfMod.Text)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "borraValidaModal", "borraValidaModal();", True)
                udpModal.Update()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Protected Sub btnListarEgre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarEgre.Click
        Try
            If Me.ddlCarrProfEmail.SelectedValue = "" Then
                mt_ShowMessage("Debe seleccionar alguna carrera profesional", MessageType.warning)
                Exit Sub
            End If
            mt_CargarDatosEgre()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mostrarBotonTodos", "mostrarBotonTodos();", True)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnSalirListEgre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirListEgre.Click
        Try
            Me.gvEmailEgr.DataSource = ""
            Me.gvEmailEgr.DataBind()
            Me.udpEnviarMail.Update()
            'Me.updListOfertas.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnEnviarEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarEmail.Click
        If validaCheckActivo() Then
            If mt_EnviarCorreo(Me.txtCodigo_ofeMod.Text) Then
                Me.udpEnviarMail.Update()
                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "noValida", "noValida('SE ENVIO EL CORREO');", True)
                mt_ShowMessage("Se envio la oferta a los correos seleccionados..", MessageType.success)
                Call btnListarEgre_Click(Nothing, Nothing)
            End If

        Else
            Me.udpScripts.Update()
            mt_ShowMessage("Debe seleccionar al menos un egresado..", MessageType.warning)
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "valida", "valida('DEBE SELECCIONAR AL MENOS UN EGRESADO');", True)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('envio-tab');", True)
            'Me.udpEnviarMail.Update()
            Call btnListarEgre_Click(Nothing, Nothing)
            'Me.lblMensajeFormulario.Text = "Debe seleccionar al menos un egresado"
        End If
    End Sub
    Protected Sub lbModOlBuscaEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbModOlBuscaEmp.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModalBuscaEmp", "OpenModalBuscaEmp();", True)
        Me.udpFiltrosEmpresa.Update()
    End Sub
    Protected Sub btnListarEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarEmpresa.Click
        Try
            Call mt_ListarEmpresas()
            Me.udpListaEmpresa.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub grwListaEmpresa_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaEmpresa.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Seleccionar"

                    Me.hf_codigo_emp.Value = Me.grwListaEmpresa.DataKeys(index).Values("codigo_emp")
                    Me.txtEmpresa.Text = Me.grwListaEmpresa.DataKeys(index).Values("nombreComercial_emp")

                    'Call mt_UpdatePanel("InformacionLaboral")
                    'Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalBuscaEmp", "closeModalBuscaEmp();", True)
                    Me.udpRegistro.Update()
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    'Eventos delegados
    Private Sub btnListarOfertas_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.txtFechIni.Text = "" Or Me.txtFechFin.Text = "" Then
            mt_ShowMessage("Debe ingresar las fechas", MessageType.warning)
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "valida", "valida('DEBE INGRESAR FECHAS');", True)
        Else
            CargarGrillaOfertas(Me.txtFechIni.Text.Trim, Me.txtFechFin.Text.Trim)
            Me.updListOfertas.Update()
        End If

        'Me.updCabecera.Update()
    End Sub
    Private Sub btnNuevaOferta_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_CargarFormOferta("0")

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
        'Me.udpModal.Update()
        Me.udpRegistro.Update()
    End Sub
    Protected Sub btnXModalAlta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnXModalAlta.Click

        Me.udpScripts.Update()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalAlta", "closeModalAlta();", True)
        CargarGrillaOfertas(Me.txtFechIni.Text, Me.txtFechFin.Text)
        Me.updListOfertas.Update()
        'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

    End Sub
    Protected Sub lbRetorno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbRetorno.Click
        mt_LimpiarControles()
        'Me.udpScripts.Update()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "borraValidaModal()", "borraValidaModal();", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
        CargarGrillaOfertas(Me.txtFechIni.Text, Me.txtFechFin.Text)
        Me.updListOfertas.Update()
    End Sub

   
#End Region

#Region "Funciones"

    Private Function validaCheckActivo() As Boolean
        Try
            If Me.gvEmailEgr.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.gvEmailEgr.Rows
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked Then
                        Return True
                        Exit For
                    End If
                Next
            End If

            Return False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_ObtenerFirmaMensaje(ByVal nombre As String) As String
        Try
            me_CarreraProfesional = New e_CarreraProfesional : md_CarreraProfesional = New d_CarreraProfesional
            Dim dt As Data.DataTable
            Dim ls_firma As String = String.Empty

            'Listar escuelas por coordinador
            dt = New Data.DataTable

            With me_CarreraProfesional
                .operacion = "PER"
                .codigo_per = cod_user
            End With

            dt = md_CarreraProfesional.ListarCarreraProfesional(me_CarreraProfesional)

            Dim ls_escuela As String = String.Empty

            For i As Integer = 0 To dt.Rows.Count - 1
                If String.IsNullOrEmpty(ls_escuela) Then
                    ls_escuela = dt.Rows(i).Item("nombre_Cpf").ToString
                Else
                    ls_escuela = ls_escuela + ", " + dt.Rows(i).Item("nombre_Cpf").ToString
                End If
            Next

            'Listar los celulares del personal
            me_personal = New e_Personal : md_Personal = New d_Personal
            dt = New Data.DataTable

            me_personal.codigo_per = cod_user

            dt = md_Personal.ObtenerCelular(me_personal)

            Dim ls_celular As String = String.Empty
            If dt.Rows.Count > 0 Then ls_celular = dt.Rows(0).Item("celular_Per").ToString

            Dim ls_direccion As String = "Av. San Josemaría Escrivá N°855. Chiclayo - Perú"
            Dim ls_telefono As String = "T: (074) 606200. Anexo: 1239 - C: " & ls_celular
            Dim ls_paginaWeb As String = "www.usat.edu.pe / http://www.facebook.com/usat.peru"

            ls_firma = "<br /><br />---------------------------------------<br />"
            ls_firma &= nombre & "<br />"

            Select Case CInt(Session("cod_ctf"))
                Case 1
                    ls_firma &= "Administrador del Sistema  - " & ls_escuela & "<br />"
                Case 90
                    ls_firma &= "Dirección de Alumni - " & ls_escuela & "<br />"
                Case 145
                    ls_firma &= "Coordinación de Alumni - " & ls_escuela & "<br />"
            End Select

            ls_firma &= ls_direccion & "<br />"
            ls_firma &= ls_telefono & "<br />"
            ls_firma &= ls_paginaWeb & "<br />"

            ls_firma &= "<div>Síguenos en :</div>"
            ls_firma &= mt_ObtenerLogo()

            Return ls_firma
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Private Function mt_ObtenerLogo() As String
        Try
            Dim ls_logo As String = String.Empty
            Dim ls_estiloLogo As String = " background-color: #5B74A8;  border-color: #29447E #29447E #1A356E;    border-image: none;    border-style: solid;    border-width: 1px;    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 #8A9CC2 inset;    color: #FFFFFF;    cursor: pointer;    display: inline-block;    font: bold 20px verdana,arial,sans-serif;    margin: 0;    overflow: visible;    padding: 0.1em 0.5em 0.1em;    position: relative;    text-align: center;    text-decoration: none;white-space: nowrap;z-index: 1;"
            ls_logo = "<a href='https://www.facebook.com/usatalumni' style='" & ls_estiloLogo & " ' ><b>f</b></a>"

            Return ls_logo
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            Return String.Empty
        End Try
    End Function

    Function style() As String
        Dim stylestr As String
        stylestr = " background-color: #5B74A8;  border-color: #29447E #29447E #1A356E;    border-image: none;    border-style: solid;    border-width: 1px;    box-shadow: 0 1px 0 rgba(0, 0, 0, 0.1), 0 1px 0 #8A9CC2 inset;    color: #FFFFFF;    cursor: pointer;    display: inline-block;    font: bold 20px verdana,arial,sans-serif;    margin: 0;    overflow: visible;    padding: 0.1em 0.5em 0.1em;    position: relative;    text-align: center;    text-decoration: none;white-space: nowrap;z-index: 1;"
        'stylestr = "width: 32px; height: 32px;	background-repeat: no-repeat;	background-position: center center;	text-indent: -900em;	text-decoration: none;	line-height: 100%;	white-space: nowrap;	display: inline-block;	position: relative;	vertical-align: middle;	margin: 0 2px 5px 0;	/* default button color */	background-color: #ececec;	border: solid 1px #b8b8b9;	/* default box shadow */	-webkit-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	-moz-box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	box-shadow: inset 0 1px 0 rgba(255,255,255,.3), 0 1px 0 rgba(0,0,0,.1);	/* default border radius */	-webkit-border-radius: 5px;	-moz-border-radius: 5px;	border-radius: 5px;background-color: #4d7de1;	border-color: #294c89;	color: #fff; background-image: url('https://intranet.usat.edu.pe/autoevaluacion/dyandroid/white_facebook.png');"
        Return stylestr
    End Function

    Private Function email_bien_escrito(ByVal email As String) As Boolean

        Dim expresion As String
        expresion = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

        If Regex.IsMatch(email, expresion) Then

            If Regex.Replace(email, expresion, String.Empty).Length = 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function



#End Region

   
    Protected Sub lbResolPrueba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbResolPrueba.Click


        Dim arreglo As New Dictionary(Of String, String)
        Dim nombreArchivo As String = "InformeDeAsesor"
        Dim codigoDatos As String = "6980"

        arreglo.Add("nombreArchivo", nombreArchivo)
        arreglo.Add("sesionUsuario", Session("perlogin").ToString)
        '-----------------                
        arreglo.Add("codigo_tes", codigoDatos)


        clsDocumentacion.PrevioDocumentopdf("", arreglo, memory)
        Dim bytes() As Byte = memory.ToArray
        memory.Close()

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-length", bytes.Length.ToString())
        Response.BinaryWrite(bytes)
    End Sub
End Class
