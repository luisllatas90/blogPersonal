﻿Partial Class frmregistrarevento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Try
                Dim objfun As New ClsFunciones
                Dim obj As New ClsConectarDatos
                Dim tbl As Data.DataTable
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                'objfun.CargarListas(Me.cboCecos, obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos_v3", Request.QueryString("ctf"), Request.QueryString("id"), ""), "codigo_cco", "Nombre", ">> Seleccione<<")
                objfun.CargarListas(Me.cboCecos, obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos_v3", Request.QueryString("ctf"), Request.QueryString("id"), "", "NUEVO"), "codigo_cco", "Nombre", ">> Seleccione<<")

                objfun.CargarListas(Me.cbocoordinadorgral, obj.TraerDataTable("ConsultarPersonal", "TPA", Request.QueryString("ctf")), "codigo_per", "Personal", ">> Seleccione<<")
                objfun.CargarListas(Me.cbocoordinadorapoyo, obj.TraerDataTable("ConsultarPersonal", "TPA", Request.QueryString("ctf")), "codigo_per", "Personal", ">> Seleccione<<")
                objfun.CargarListas(Me.dpTipo, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 0, 1, 1, 1), "codigo_test", "descripcion_test", ">> Seleccione<<")

                'Cargar edición
                Me.CargarDatosCbo(Me.cbonrocuotas, 59, False)

                If Request.QueryString("accion") = "M" Then                
                    objfun.CargarListas(Me.cboCecos, obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos_v3", Request.QueryString("ctf"), Request.QueryString("id"), "", "EDITAR"), "codigo_cco", "Nombre", ">> Seleccione<<")
                    tbl = obj.TraerDataTable("EVE_ConsultarEventos", 0, Me.Request.QueryString("cco"), 0)

                    If (tbl.Rows.Count > 0) Then
                        Me.cboCecos.SelectedValue = tbl.Rows(0).Item("codigo_cco")
                        Me.cboCecos.Enabled = False
                        Me.lnkBusquedaAvanzada.Visible = False
                        Me.txtresolucion_dev.Text = tbl.Rows(0).Item("nroresolucion_dev")
                        Me.cbocoordinadorgral.SelectedValue = tbl.Rows(0).Item("coordinadorgral_dev")
                        Me.cbocoordinadorapoyo.SelectedValue = tbl.Rows(0).Item("coordinadorapoyo_dev")
                        Me.txtnroparticipantes.Text = tbl.Rows(0).Item("nroparticipantes_dev")
                        Me.txtpreciounitcontado.Text = tbl.Rows(0).Item("preciounitcontado_dev")
                        Me.txtpreciounitfinanciado.Text = tbl.Rows(0).Item("preciounitfinanciado_dev")
                        Me.txtmontocuotainicial.Text = tbl.Rows(0).Item("montocuotainicial_dev")
                        Me.cbonrocuotas.SelectedValue = tbl.Rows(0).Item("nrocuotas_dev")
                        Me.txtpreciopersonalusat.Text = tbl.Rows(0).Item("porcentajedescpersonalusat_dev")
                        Me.txtprecioalumno.Text = tbl.Rows(0).Item("porcentajedescalumnousat_dev")
                        Me.txtpreciocorportativo.Text = tbl.Rows(0).Item("porcentajedesccorportativo_dev")
                        Me.txtprecioegresado.Text = tbl.Rows(0).Item("porcentajedescegresado_dev") '
                        Me.txtingresostotalesproyectada_dev.Text = tbl.Rows(0).Item("ingresostotalesproyectada_dev")
                        Me.txtegresostotalesproyectada_dev.Text = tbl.Rows(0).Item("egresostotalesproyectada_dev")
                        Me.txtUtilidad.Text = tbl.Rows(0).Item("utilidadproyectada_dev")
                        Me.txtremuneraciontotalcoordinadorgral.Text = tbl.Rows(0).Item("remuneraciontotalcoordinadorgral_dev")
                        Me.txtremuneraciontotalcoordinadorapoyo.Text = tbl.Rows(0).Item("remuneraciontotalcoordinadorapoyo_dev")
                        Me.chkgestionanotas_dev.Checked = tbl.Rows(0).Item("gestionanotas_dev")
                        Me.txtFechaInicio.Text = tbl.Rows(0).Item("fechainiciopropuesta_dev").ToString
                        Me.txtFechaFin.Text = tbl.Rows(0).Item("fechafinpropuesta_dev").ToString
                        Me.txthorarios_dev.Text = tbl.Rows(0).Item("horarios_dev").ToString
                        Me.chkValidarDeuda.Checked = tbl.Rows(0).Item("validarDeuda_dev")

                        '============================================================================'
                        'dguevara: 12.03.2014
                        If tbl.Rows(0).Item("tokengen").ToString.Length > 0 Then
                            'Response.Write("Token generado: <br/>")
                            Me.chkInscripcionViaWeb.Checked = True
                            Me.cmdEnviarInscripcion.Visible = True
                            Me.hd_email_coordinador.Value = tbl.Rows(0).Item("emailcoordinador").ToString
                            Me.hd_email_apoyo.Value = tbl.Rows(0).Item("emailcoordinadorapoyo").ToString
                            Me.hd_token.Value = tbl.Rows(0).Item("tokengen").ToString
                        Else
                            Me.chkInscripcionViaWeb.Checked = False
                            Me.cmdEnviarInscripcion.Visible = False
                            Me.hd_email_coordinador.Value = ""
                            Me.hd_email_apoyo.Value = ""
                            Me.hd_token.Value = ""
                        End If
                        '============================================================================'

                        If tbl.Rows(0).Item("codigo_test").ToString <> "" Then
                            Me.dpTipo.SelectedValue = tbl.Rows(0).Item("codigo_test").ToString
                            objfun.CargarListas(Me.dpSubTipo, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 1, tbl.Rows(0).Item("codigo_test").ToString, 0, 0), "codigo_stest", "descripcion_stest", ">> Seleccione<<")
                            Me.dpSubTipo.SelectedValue = tbl.Rows(0).Item("codigo_stest").ToString
                            'objfun.CargarListas(Me.dpEscuela, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, tbl.Rows(0).Item("codigo_test").ToString, tbl.Rows(0).Item("codigo_stest").ToString, 0), "codigo_cpf", "nombre_cpf")
                            'If dpEscuela.Items.Count = 1 Then
                            ConfigurarEstudios()                            
                            'End If
                            Me.dpEscuela.SelectedValue = tbl.Rows(0).Item("codigo_cpf").ToString
                        End If
                        Me.txtobs.Text = tbl.Rows(0).Item("obs_dev").ToString
                    End If
                    'Asignar acciones a botón "Cancelar"
                    Me.cmdCancelar.UseSubmitBehavior = False
                    Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")

                    'Bloquear si gestiona/no gestiona notas
                    Me.chkgestionanotas_dev.Enabled = False
                    Me.dpTipo.Enabled = False
                    Me.dpSubTipo.Enabled = False
                    Me.dpEscuela.Enabled = False
                Else
                    Me.cmdCancelar.UseSubmitBehavior = True
                    cmdEnviarInscripcion.Visible = False
                End If

                obj.CerrarConexion()
                obj = Nothing
                objfun = Nothing

                'Validar cajas de números.
                Me.txtnroparticipantes.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtremuneraciontotalcoordinadorgral.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtremuneraciontotalcoordinadorapoyo.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtnroparticipantes.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtpreciounitcontado.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtpreciounitfinanciado.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtmontocuotainicial.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtpreciopersonalusat.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtprecioalumno.Attributes.Add("onkeypress", "validarnumero()")
                Me.txtpreciocorportativo.Attributes.Add("onkeypress", "validarnumero()")
            Catch ex As Exception
                Response.Write("Error: " & ex.Message)
            End Try            
        End If
        'Me.txtUtilidad.Text = CDbl(Me.txtegresostotalesproyectada_dev.Text) - CDbl(Me.txtegresostotalesproyectada_dev.Text)
    End Sub
    Private Sub CargarDatosCbo(ByVal cbo As System.Object, ByVal limite As Int16, Optional ByVal MostrarTexto As Boolean = True)
        cbo.items.clear()
        If MostrarTexto = True Then
            cbo.Items.Add("-Seleccione-")
        End If
        'cbo.items.items(0).value = -1
        For i As Integer = 0 To limite
            cbo.Items.Add(i + 1)
            cbo.Items(i).Value = (i + 1).ToString
        Next
    End Sub
    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        Panel3.Visible = False
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'gvCecos.DataSource = obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text.Trim)
        gvCecos.DataSource = obj.TraerDataTable("PRESU_ConsultarCentroCostosXPermisos_V3", Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text.Trim, "NUEVO")
        gvCecos.DataBind()
        obj = Nothing

        If gvCecos.Rows.Count > 0 Then
            Panel3.Visible = True
        End If
    End Sub
    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
        cargarDatosCECO()

    End Sub
    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.cboCecos.Visible = Not (valor)
        Panel3.Visible = (valor)
    End Sub
    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub
    Protected Sub dpTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpTipo.SelectedIndexChanged
        Me.dpSubTipo.Items.Clear()
        Me.dpEscuela.Items.Clear()

        If dpTipo.SelectedValue <> -1 Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(Me.dpSubTipo, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 1, Me.dpTipo.SelectedValue, 0, 0), "codigo_stest", "descripcion_stest", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Me.cmdGuardar.Enabled = False
            'Dim valoresdevueltos(3) As Object
            Dim valoresdevueltos(4) As Object
            Dim gestionanotas As Boolean = False
            Dim escuela As Int16 = 9 'Todas
            Dim codigo_test As Byte = 0
            Dim codigo_stest As Byte = 0

            Me.txtUtilidad.Text = CDbl(Me.txtingresostotalesproyectada_dev.Text) - CDbl(Me.txtegresostotalesproyectada_dev.Text)

            If Me.chkgestionanotas_dev.Checked = True Then
                gestionanotas = True
                codigo_test = Me.dpTipo.SelectedValue
                codigo_stest = Me.dpSubTipo.SelectedValue

                If Me.dpEscuela.SelectedValue.ToString = "" Or Me.dpEscuela.SelectedValue.ToString = "-1" Then
                    Page.RegisterStartupScript("Falta", "<script>alert('Debe especificar el nombre del Estudio')</script>")
                    Exit Sub
                Else
                    escuela = Me.dpEscuela.SelectedValue
                End If
            End If

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            If Request.QueryString("accion") = "M" Then
                obj.Ejecutar("EVE_Modificardatosevento", Request.QueryString("cco"), _
                             Me.txtresolucion_dev.Text.Trim, Me.cbocoordinadorgral.SelectedValue, _
                             Me.cbocoordinadorapoyo.SelectedValue, _
                             Me.txtnroparticipantes.Text, _
                             Me.txtpreciounitcontado.Text, _
                             Me.txtpreciounitfinanciado.Text, _
                             Me.txtmontocuotainicial.Text, _
                             Me.cbonrocuotas.SelectedValue, _
                             Me.txtpreciopersonalusat.Text, _
                             Me.txtprecioalumno.Text, _
                             Me.txtpreciocorportativo.Text, _
                             Me.txtprecioegresado.Text, _
                             Me.txtingresostotalesproyectada_dev.Text, _
                             Me.txtegresostotalesproyectada_dev.Text, _
                             Me.txtUtilidad.Text, _
                             Me.txtremuneraciontotalcoordinadorgral.Text, _
                             Me.txtremuneraciontotalcoordinadorapoyo.Text, gestionanotas, _
                             CDate(Me.txtFechaInicio.Text), _
                             CDate(Me.txtFechaFin.Text), _
                             Me.txthorarios_dev.Text.Trim, escuela, 0, _
                             Me.txtobs.Text.Trim, Me.dpTipo.SelectedValue, _
                             Request.QueryString("id"), chkValidarDeuda.Checked)
            Else
                obj.Ejecutar("EVE_Agregardatosevento", Me.cboCecos.SelectedValue, _
                             Me.txtresolucion_dev.Text.Trim, Me.cbocoordinadorgral.SelectedValue, _
                             Me.cbocoordinadorapoyo.SelectedValue, _
                             Me.txtnroparticipantes.Text, _
                             Me.txtpreciounitcontado.Text, _
                             Me.txtpreciounitfinanciado.Text, _
                             Me.txtmontocuotainicial.Text, _
                             Me.cbonrocuotas.SelectedValue, _
                             Me.txtpreciopersonalusat.Text, _
                             Me.txtprecioalumno.Text, _
                             Me.txtpreciocorportativo.Text, _
                             Me.txtprecioegresado.Text, _
                             Me.txtingresostotalesproyectada_dev.Text, _
                             Me.txtegresostotalesproyectada_dev.Text, _
                             Me.txtUtilidad.Text, _
                             Me.txtremuneraciontotalcoordinadorgral.Text, _
                             Me.txtremuneraciontotalcoordinadorapoyo.Text, gestionanotas, _
                             CDate(Me.txtFechaInicio.Text), _
                             CDate(Me.txtFechaFin.Text), _
                             Me.txthorarios_dev.Text.Trim, escuela, 0, _
                             Me.txtobs.Text.Trim, _
                             codigo_test, codigo_stest, _
                             Request.QueryString("id"), chkValidarDeuda.Checked, 0, "", "", "").copyto(valoresdevueltos, 0)
                'Response.Write(Me.cboCecos.SelectedValue & "<br />" & UCase(Me.txtnombre_dev.Text.Trim) & "<br />" & Me.cboedicion.SelectedValue & "<br />" & Me.txtresolucion_dev.Text.Trim & "<br />" & Me.cbocoordinadorgral.SelectedValue & "<br />" & Me.cbocoordinadorapoyo.SelectedValue & "<br />" & Me.txtnroparticipantes.Text & "<br />" & Me.txtpreciounitcontado.Text & "<br />" & Me.txtpreciounitfinanciado.Text & "<br />" & Me.txtmontocuotainicial.Text & "<br />" & Me.cbonrocuotas.SelectedValue & "<br />" & Me.txtpreciousat.Text & "<br />" & Me.txtprecioalumno.Text & "<br />" & Me.txtpreciocorportativo.Text & "<br />" & Me.txtingresostotalesproyectada_dev.Text & "<br />" & Me.txtegresostotalesproyectada_dev.Text & "<br />" & Me.txtUtilidad.Text & "<br />" & Me.txtremuneraciontotalcoordinadorgral.Text & "<br />" & Me.txtremuneraciontotalcoordinadorapoyo.Text & "<br />" & gestionanotas & "<br />" & CDate(Me.txtFechaInicio.Text) & "<br />" & CDate(Me.txtFechaFin.Text) & "<br />" & Me.txthorarios_dev.Text.Trim & "<br />" & escuela & "<br />" & Me.txtobs.Text.Trim & "<br />" & Request.QueryString("id"))
            End If
            obj.CerrarConexion()
            obj = Nothing

            'Voy a comentar todo esta bloque:
            If Request.QueryString("accion") = "M" Then
                Page.RegisterStartupScript("ok", "<script>alert('Se han registrado los datos correctamente');window.parent.location.reload();self.parent.tb_remove()</script>")
            Else
                If valoresdevueltos(0) = 1 Then
                    'dguevara:11.03.2014
                    '=== ** enviar email a los coordinadores con el formato html para inscripcion web **--
                    If chkInscripcionViaWeb.Checked = True Then
                        EnviaEmailFormatoInscripcion(valoresdevueltos(1), valoresdevueltos(2), valoresdevueltos(3))
                    End If
                    'Asignar los parámetros para ENVIAR correos. Sólo cuando es nuevo evento.
                    If Request.QueryString("id") <> 466 And Request.QueryString("id") <> 471 Then
                        EnviarMensaje(valoresdevueltos(1), valoresdevueltos(2))
                    End If
                    Page.RegisterStartupScript("ok", "<script>alert('Se han registrado los datos correctamente');location.href='lsteventos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "'</script>")
                Else
                    Page.RegisterStartupScript("ok", "<script>alert('Ya existe un evento registrado para este centro de costos');location.href='lsteventos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "'</script>")
                End If
                End If
        Catch ex As Exception
            Page.RegisterStartupScript("Error", "<script>alert('Ocurrió un error al guardar el evento: " & ex.Message & " ')</script>")
        End Try
    End Sub


    Protected Sub cmdEnviarInscripcion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviarInscripcion.Click
        Try
            'Response.Write("hd_email_coordinador : " & Me.hd_email_coordinador.Value)
            'Response.Write("<br/>")
            'Response.Write("hd_email_apoyo : " & Me.hd_email_apoyo.Value)
            'Response.Write("<br/>")
            'Response.Write("token: " & Me.hd_token.Value)
            EnviaEmailFormatoInscripcion(hd_email_coordinador.Value, hd_email_apoyo.Value, hd_token.Value)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub EnviaEmailFormatoInscripcion(ByVal emailResp As String, ByVal emailApy As String, ByVal toke As String)
        Try
            'Email: 
            Dim ObjMailNet As New ClsMail
            Dim Mensaje, AsuntoCorreo, ConCopiaA As String
            Mensaje = "Se adjunta código para la generación de la ficha de pre-inscripción que deberá remitida al administrador del sitio web de la USAT. <br/><br/>"
            Mensaje = Mensaje & "< div style=""text-align:center;"">"
            Mensaje = Mensaje & "<br/>" & "< form id=""form1"" action=""../../../librerianet/Inscripcion/frmpersona_v2.aspx"" method=""post"">"
            Mensaje = Mensaje & "<br/>" & "< input type=""hidden"" name=""mod"" value=""0""/>"
            Mensaje = Mensaje & "<br/>" & "< input type=""hidden"" name=""Evento"" value=""" & toke & """ />"
            Mensaje = Mensaje & "<br/>" & "< input type=""image"" name=""btn"" src=""rutaimagen"" alt=""Preinscribete aquí""/></form>"
            'Response.Write(Mensaje)

            '02.04.2014 : dguevara
            'ConCopiaA = ConCopiaA & ";" & emailResp
            ' ConCopiaA = "clluen@usat.edu.pe" & ";" & emailApy
            AsuntoCorreo = "Inscripción Evento via Web"

            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Gestión Eventos", emailResp, AsuntoCorreo, Mensaje, True, emailApy)

            'Se incluyo en el correo a clluen.
            'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Gestión Eventos", emailResp, AsuntoCorreo, Mensaje, True, ConCopiaA)
            ObjMailNet = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub EnviarMensaje(ByVal emailResp As String, ByVal emailApy As String)
        'Enviar email
        Dim ObjMailNet As New ClsMail
        Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String

        Mensaje = "<h3 style='font-color:blue'>Le informamos que se ha creado un Evento que " & IIf(Me.chkgestionanotas_dev.Checked = True, "gestionará notas ", "no gestionará notas") & "</h3>"
        Mensaje = Mensaje & "<ol><li>Centro de Costos: " & Me.cboCecos.SelectedItem.Text & "</li>"
        Mensaje = Mensaje & "<li>Resolución de aprobación: " & Me.txtresolucion_dev.Text & "</li>"
        Mensaje = Mensaje & "<li>Fecha de inicio: " & Me.txtFechaInicio.Text & "</li>"
        Mensaje = Mensaje & "<li>Fecha fin: " & Me.txtFechaFin.Text & "</li>"
        Mensaje = Mensaje & "<li>Coordinador General: " & Me.cbocoordinadorgral.SelectedItem.Text & "</li>"
        Mensaje = Mensaje & "<li>Coordinador de Apoyo: " & Me.cbocoordinadorapoyo.SelectedItem.Text & "</li></ol>"
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------

        Mensaje = Mensaje & "<p><a href=""../../../personal/administrativo/pec/detalleevento.aspx?cco=" & Me.cboCecos.SelectedValue & """>Ver más detalles</a></p>"

        Correo = "cmonja@usat.edu.pe"
        ConCopiaA = "jdanjanovic@usat.edu.pe;ftuesta@usat.edu.pe;fseclen@usat.edu.pe;esaavedra@usat.edu.pe"

        'Correo = "gchunga@usat.edu.pe"
        'ConCopiaA = "hzelada@usat.edu.pe"

        'PRUEBAS DEL SERVER-TEST
        'Correo = "csenmache@usat.edu.pe"
        'ConCopiaA = "csenmache@usat.edu.pe"

        If emailResp <> "" Then
            ConCopiaA = ConCopiaA & ";" & emailResp
        End If
        If emailApy <> "" Then
            ConCopiaA = ConCopiaA & ";" & emailApy
        End If


        AsuntoCorreo = "Nuevo evento: " & cboCecos.SelectedItem.Text
        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Gestión Eventos", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
        ObjMailNet = Nothing
    End Sub
    Protected Sub dpSubTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpSubTipo.SelectedIndexChanged
        Me.dpEscuela.Items.Clear()

        If dpSubTipo.SelectedValue <> -1 Then
            ConfigurarEstudios()
        End If
    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("lsteventos.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        cargarDatosCECO()
    End Sub
    Private Sub cargarDatosCECO()
        'Consultar Datos del CECO
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        datos = obj.TraerDataTable("PRESU_ConsultarCentroDeCostos", cboCecos.SelectedValue)
        If datos.Rows.Count > 0 Then
            cbocoordinadorgral.SelectedValue = datos.Rows(0).Item("codigo_per")
        Else
            cbocoordinadorgral.SelectedValue = -1
        End If

        obj.CerrarConexion()
        obj = Nothing
        objfun = Nothing

    End Sub

    Private Sub ConfigurarEstudios()
        Me.dpEscuela.Items.Clear()
        Select Case Me.dpTipo.SelectedValue
            Case 1
                'Si es Escuela Pre
                Me.dpEscuela.Items.Add("-Todas las Escuelas--")
                Me.dpEscuela.Items(0).Value = 9                
                'Case 3
                '    'Si es Profesionalización, Cargar todas las Escuelas de PreGrado
                '    'para que elija una de ellas. Enviar tipo 2: Pregrado
                '    Dim obj As New ClsConectarDatos
                '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                '    obj.AbrirConexion()
                '    ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, 2, 1, 0), "codigo_cpf", "nombre_cpf", "--Seleccione--")
                '    obj.CerrarConexion()
                '    obj = Nothing
            Case Else                
                Dim objfun As New ClsFunciones
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ' Response.Write(2 & " -- " & Me.dpTipo.SelectedValue & " -- " & Me.dpSubTipo.SelectedValue & " -- " & 0)
                objfun.CargarListas(Me.dpEscuela, obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 2, Me.dpTipo.SelectedValue, Me.dpSubTipo.SelectedValue, 0), "codigo_cpf", "nombre_cpf", ">> Seleccione<<")
                obj.CerrarConexion()
                obj = Nothing
                objfun = Nothing
        End Select
    End Sub


End Class