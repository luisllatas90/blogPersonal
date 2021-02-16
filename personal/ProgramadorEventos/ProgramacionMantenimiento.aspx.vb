Imports System.Net
Imports System.Net.Mail
Imports System.Collections.Generic
Imports System.Data

Partial Class ProgramacionMantenimiento
    Inherits System.Web.UI.Page
    Private cod As String = "0"
    Private con As String = "0"
    Private evt As String = "0"
    Private des As String = ""
    Private cat As String = ""
    Private cpf As String = "0"
    Private C As ClsConectarDatos

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSATIMPORT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            cod = Request.Params("cod").Trim()
            con = Request.Params("con").Trim()
            evt = Request.Params("evt").Trim()
            des = Request.Params("des").Trim()
            cat = Request.Params("cat").Trim()

            If Request.Params("cpf") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Request.Params("cpf")) Then
                cpf = Request.Params("cpf")
            End If

            If String.IsNullOrEmpty(cod) Then
                cod = "0"
            End If

            If String.IsNullOrEmpty(evt) Then
                ifrmSeleccionarInteresados.Attributes.Item("src") = "FiltrarInteresados.aspx?tipo=M&codigoPro=" & cod
                pageHeader.Visible = False
            End If

            liSeleccionarInteresados.Visible = (cat = "M")
            liInteresados.Visible = False
            'liInteresados.Visible = (cat <> "I" AndAlso cod <> "0")

            If cat = "P" Then
                hddFiltros.Value = "codigo_con=" & con & "|codigo_eve=" & evt
            End If

            If cat <> "I" Then
                divDiasFechaRegistro.Attributes.Item("class") = divDiasFechaRegistro.Attributes.Item("class") + " d-none"
            End If

            If cat = "I" Then
                cboTipoProgramacion.Attributes.Item("disabled") = "disabled"
                divFrecuenciaGeneral.Attributes.Item("disabled") = "disabled"
            Else
                cboTipoProgramacion.Attributes.Remove("disabled")
                divFrecuenciaGeneral.Attributes.Remove("disabled")
            End If

            If Not IsPostBack Then
                Me.lblEvento.InnerText = des

                If Not cod.Equals("0") Then
                    frmProgramacion.Attributes.Item("data-edit") = True
                    divMsgPrueba.Visible = False
                    Call CargarDatos()
                Else
                    frmProgramacion.Attributes.Item("data-edit") = False
                    Me.txtDescripcion.Focus()
                End If
            Else
                Call LimpiarFormulario()
            End If
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.ToString() & "');</script>")
        Finally
            LimpiarMensajeServidor()
        End Try
    End Sub

    Private Sub CargarDatos()
        Try
            Dim hraIni, hraFin, frec, diaSem, diaMes, ord, sem, frecHra, horaMin As String
            Dim varsProg() As String
            Dim dt As New System.Data.DataTable("Programacion")

            C.AbrirConexion()
            dt = C.TraerDataTable("PRO_DatosProgramacionEvento", cod)

            Me.txtCodigo.Value = cod
            Me.txtCategoria.Value = cat
            Me.txtDescripcion.Value = dt.Rows(0).Item("descripcion_pro").ToString
            Me.cboTipoMensaje.Value = dt.Rows(0).Item("tipoMensaje_pro").ToString
            Me.txtID.Value = dt.Rows(0).Item("mensajeTemplate_pro").ToString

            varsProg = dt.Rows(0).Item("variablesMensajeTemplate_pro").ToString.Split(",")
            For Each varProg As String In varsProg
                For Each _item As ListItem In cboVariablesProgramacion.Items
                    If _item.Value = varProg Then
                        _item.Selected = True
                    End If
                Next
            Next
            Me.cboTipoProgramacion.Value = dt.Rows(0).Item("tipoProgramacion_pro").ToString
            Me.dtpFechaInicio.Value = dt.Rows(0).Item("fechaInicio_pro").ToString
            Me.dtpFechaFin.Value = dt.Rows(0).Item("fechaFin_pro").ToString

            hraIni = dt.Rows(0).Item("horaInicio_pro").ToString
            hraFin = dt.Rows(0).Item("horaFin_pro").ToString

            Me.cboTipoFrecuencia.Value = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("tipoFrecuencia_pro").ToString), "D", dt.Rows(0).Item("tipoFrecuencia_pro").ToString)
            Me.txtFrecuenciaDiaSemana.Value = dt.Rows(0).Item("frecuenciaDiaSemana_pro").ToString
            Me.txtNumeroDias.Value = dt.Rows(0).Item("diasFecRegistro_pro").ToString

            frec = IIf(String.IsNullOrEmpty(dt.Rows(0).Item("frecuencia_pro").ToString), "1", dt.Rows(0).Item("frecuencia_pro").ToString)
            diaSem = dt.Rows(0).Item("frecuenciaDiaSemana_pro").ToString
            diaMes = dt.Rows(0).Item("frecuenciaDiaMes_pro").ToString
            ord = dt.Rows(0).Item("frecuenciaOrdinal_pro").ToString
            sem = dt.Rows(0).Item("frecuenciaSemanal_pro").ToString

            frecHra = dt.Rows(0).Item("frecuenciaHora_pro").ToString
            horaMin = dt.Rows(0).Item("frecuenciaTiempo_pro").ToString

            dt.Dispose()
            C.CerrarConexion()

            'If cat <> "I" Then
            '    ListarInteresadosPorProgramacion()
            'End If

            ClientScript.RegisterStartupScript(Me.GetType, "PopUp", "<script>setValuesJS('" & hraIni & "', '" & hraFin & "', '" & frec & "', '" & diaSem & "', '" & diaMes & "', '" & ord & "', '" & sem & "', '" & frecHra & "', '" & horaMin & "');</script>")
        Catch ex As Exception
            Response.Write("<script>alert('" & ex.ToString() & "');</script>")
        End Try
    End Sub

    Private Sub ListarInteresadosPorProgramacion()
        Try
            Dim dt As New System.Data.DataTable("Interesados")

            C.AbrirConexion()
            dt = C.TraerDataTable("PRO_ListarInteresadosPorProgramacion", cod)
            grwInteresados.DataSource = dt
            grwInteresados.DataBind()

            udpInteresados.Update()
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.ToString & "')", True)
        End Try
    End Sub

    Protected Sub btnMensajePrueba_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMensajePrueba.ServerClick
        Try
            Dim tipoMensaje As String = Request("cboTipoMensaje").ToString()
            Dim mensajeTemplate As String = Request("txtID").ToString().Trim
            Dim variablesMensaje As String = IIf(String.IsNullOrEmpty(Request("cboVariablesProgramacion")), "", Request("cboVariablesProgramacion")).ToString
            Dim destinatario As String = Request("txtDestinatarioPrueba").ToString().Trim
            Dim urlAPI As String = ""

            Select Case tipoMensaje
                Case "S"
                    If String.IsNullOrEmpty(mensajeTemplate) Then
                        GenerarMensajeServidor("Alerta", "0", "Debe ingresar un mensaje")
                        Exit Sub
                    End If

                    If String.IsNullOrEmpty(destinatario) Then
                        GenerarMensajeServidor("Alerta", "0", "Debe ingresar un destinatario")
                        Exit Sub
                    End If

                    If Not Regex.IsMatch(destinatario, "^[0-9 ]+$") Then
                        GenerarMensajeServidor("Alerta", "0", "Solo se permiten números en el destinatario")
                        Exit Sub
                    End If

                    urlAPI = "https://api.infobip.com/sms/1/text/single"
                    Using client As New WebClient()
                        Dim base64Decoded As String = "Hzapatac:Lqevsl2@2@" 'andy.diaz 05/10/2020
                        Dim data As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(base64Decoded)
                        Dim base64Encoded As String = System.Convert.ToBase64String(data)

                        client.Headers(HttpRequestHeader.ContentType) = "application/json"
                        client.Headers(HttpRequestHeader.Authorization) = "Basic " + base64Encoded

                        Dim finalMensaje As String = mensajeTemplate
                        Dim aVariables As String() = variablesMensaje.Split(",")
                        For Each _var As String In aVariables
                            finalMensaje = finalMensaje.Replace("{{" + _var + "}}", _var)
                        Next
                        Dim sBody As String = "{""to"": ""51" + destinatario + """, ""text"": """ + finalMensaje + """}"
                        Dim body As Byte() = Encoding.UTF8.GetBytes(sBody)
                        Dim result As Byte() = client.UploadData(urlAPI, "POST", body)
                        Dim resultContent As String = Encoding.UTF8.GetString(result, 0, result.Length)

                        GenerarMensajeServidor("Mensaje", "1", "Se envió el mensaje correctameente")
                    End Using

                Case "M"
                    If String.IsNullOrEmpty(mensajeTemplate) Then
                        GenerarMensajeServidor("Alerta", "0", "Debe ingresar un template")
                        Exit Sub
                    End If

                    If String.IsNullOrEmpty(destinatario) Then
                        GenerarMensajeServidor("Alerta", "0", "Debe ingresar un email")
                        Exit Sub
                    End If

                    Try
                        Dim addr As MailAddress = New System.Net.Mail.MailAddress(destinatario)
                        If addr.Address <> destinatario Then
                            GenerarMensajeServidor("Alerta", "0", "El formato del email no es válido")
                            Exit Sub
                        End If
                    Catch ex As Exception
                        GenerarMensajeServidor("Alerta", "0", "El formato del email no es válido")
                        Exit Sub
                    End Try

                    urlAPI = "https://api.sendgrid.com/v3/mail/send"
                    Using client As New WebClient()
                        client.Headers(HttpRequestHeader.ContentType) = "application/json"
                        client.Headers(HttpRequestHeader.Authorization) = "Bearer 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"

                        Dim dynamicData As String = ""
                        Dim aVariables As String() = variablesMensaje.Split(",")
                        For Each _var As String In aVariables
                            dynamicData &= """" + _var + """: """ + _var + """, "
                        Next
                        dynamicData = "{" + dynamicData + "}"
                        Dim sBody As String = "{""personalizations"": [{""to"": [{""email"": """ + destinatario + """}],""dynamic_template_data"": " + dynamicData + "}],""from"": {""email"": ""informacion@usat.edu.pe""},""template_id"": """ + mensajeTemplate + """,}"
                        Dim body As Byte() = Encoding.UTF8.GetBytes(sBody)
                        Dim result As Byte() = client.UploadData(urlAPI, "POST", body)
                        Dim resultContent As String = Encoding.UTF8.GetString(result, 0, result.Length)

                        GenerarMensajeServidor("Mensaje", "1", "Se envió el mensaje correctameente")
                    End Using
            End Select

        Catch ex As Exception
            GenerarMensajeServidor("Error", "-1", ex.Message)
        End Try
    End Sub

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ls_Rpta As String, ByVal ls_Msg As String, Optional ByVal ls_Control As String = "")
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        With divRespuestaPostback.Attributes
            .Item("data-rpta") = ls_Rpta
            .Item("data-msg") = ls_Msg
            .Item("data-control") = ls_Control
        End With
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub LimpiarMensajeServidor()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Try
            Dim codigo_eve As Object = IIf(Not String.IsNullOrEmpty(evt), evt, DBNull.Value)
            Dim valid As Dictionary(Of String, String) = Validar()

            If valid.Item("rpta") = 1 Then
                Dim p As Object() = {DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                                     DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                                     DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                                     DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                                     DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, _
                                     DBNull.Value, DBNull.Value, DBNull.Value}

                p(0) = cod
                p(1) = 4818 '@codigo_cco
                p(2) = codigo_eve
                p(3) = 684
                p(4) = Request("txtDescripcion").ToString()
                p(5) = cat
                p(6) = Request("cboTipoMensaje").ToString()
                p(7) = Request("txtID").ToString()

                p(8) = IIf(Request("cboVariablesProgramacion") IsNot Nothing, Request("cboVariablesProgramacion"), "").ToString()
                p(9) = Request("cboTipoProgramacion").ToString()

                If Request("cboTipoProgramacion").ToString().Equals("P") Then
                    p(10) = Request("txtFechaInicio").ToString() 'dtpFechaInicio
                    p(11) = IIf(Not String.IsNullOrEmpty(Request("txtFechaFin").ToString()), Request("txtFechaFin").ToString(), p(11)) 'dtpFechaFin
                    p(12) = IIf(rbtFrecuenciaA1.Checked, Request("txtHoraFrecuencia").ToString(), p(12)) 'dtpHoraFrecuencia
                    p(12) = IIf(rbtFrecuenciaA2.Checked, Request("txtHoraIniDia").ToString(), p(12)) 'dtpHoraIniDia
                    p(13) = IIf(rbtFrecuenciaA2.Checked, Request("txtHoraFinDia").ToString(), p(13)) 'dtpHoraFinDia
                    p(14) = Request("cboTipoFrecuencia").ToString()

                    If p(14).Equals("D") Or p(14).Equals("S") Then
                        p(15) = Request("txtFrecuencia").ToString() 'spnFrecuencia
                    ElseIf p(14).Equals("M") And rbtFrecuenciaDiaMes.Checked Then
                        p(15) = Request("txtFrecuenciaMes").ToString() 'spnFrecuenciaMes
                    ElseIf p(14).Equals("M") And rbtOrdinal.Checked Then
                        p(15) = Request("txtMes").ToString() 'spnMes
                    End If

                    p(16) = IIf(p(14).Equals("S"), Request("txtFrecuenciaDiaSemana").ToString(), p(16))
                    p(17) = IIf(p(14).Equals("M") And rbtFrecuenciaDiaMes.Checked, Request("txtFrecuenciaDiaMes").ToString(), p(17)) 'spnFrecuenciaDiaMes
                    p(18) = IIf(p(14).Equals("M") And rbtOrdinal.Checked, Request("cboOrdinal").ToString(), p(18))
                    p(19) = IIf(p(14).Equals("M") And rbtOrdinal.Checked, Request("cboDiaSemana").ToString(), p(19))
                    p(20) = IIf(rbtFrecuenciaA2.Checked, Request("cboFrecuenciaTiempo").ToString(), p(20))
                    p(21) = IIf(rbtFrecuenciaA2.Checked, Request("txtFrecuenciaHora").ToString(), p(21)) 'spnFrecuenciaHora
                Else
                    p(10) = Request("txtFechaInicio").ToString() 'dtpFechaInicio
                    p(12) = Request("txtHoraInicio").ToString() 'dtpHoraInicio
                End If

                'p(22) = hddCodigosInt.Value
                p(24) = "A"

                p(22) = cpf 'codigo_cpf

                Dim numeroDias As Integer = 0
                If Request("txtNumeroDias") IsNot Nothing Then
                    If Not String.IsNullOrEmpty(Request("txtNumeroDias")) Then
                        numeroDias = Request("txtNumeroDias")
                    End If
                End If
                p(23) = numeroDias 'numero_dias

                'FILTROS
                p(25) = hddFiltros.Value

                C.AbrirConexion()

                Dim oSalida As Object() = C.Ejecutar("PRO_ProgramarComunicacion", p(0), p(1), p(2), p(3), p(4), p(5), _
                                                     p(6), p(7), p(8), p(9), p(10), p(11), p(12), p(13), p(14), p(15), _
                                                     p(16), p(17), p(18), p(19), p(20), p(21), p(22), p(23), p(24), _
                                                     p(25), p(26), p(27))
                C.CerrarConexion()

                GenerarMensajeServidor("Mensaje", oSalida(0).ToString, oSalida(1).ToString)
            Else
                GenerarMensajeServidor("Advertencia", valid.Item("rpta"), valid.Item("msg"))
            End If
        Catch ex As Exception
            GenerarMensajeServidor("Error", "-1", ex.Message)
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        Me.txtCodigo.Value = ""
        Me.txtDescripcion.Value = "" : Me.cboTipoMensaje.SelectedIndex = 0
        Me.txtID.Value = "" : Me.cboTipoProgramacion.SelectedIndex = 0
        Me.cboTipoFrecuencia.SelectedIndex = 0 : Me.cboOrdinal.SelectedIndex = 0
        Me.cboDiaSemana.SelectedIndex = 0 : Me.cboFrecuenciaTiempo.SelectedIndex = 0
        Me.dtpFechaInicio.Value = "" : Me.dtpFechaFin.Value = ""
        Me.chkFechaFin.Checked = False
    End Sub

    Private Function Validar() As Dictionary(Of String, String)
        Dim valid As New Dictionary(Of String, String)
        Dim err As Boolean = False
        valid.Add("rpta", 1)
        valid.Add("msg", "")
        valid.Add("control", "")

        'VALIDAR CABECERA
        If String.IsNullOrEmpty(Request("txtDescripcion")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar una descripción"
                valid.Item("control") = "txtDescripcion"
                err = True
            End If
            txtDescripcion.Attributes.Item("data-error") = "true"
        Else
            txtDescripcion.Attributes.Item("data-error") = "false"
        End If

        If String.IsNullOrEmpty(Request("txtID")) Then
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar " & IIf(Request("cboTipoMensaje").Trim().Equals("M"), "ID del Template", "texto del mensaje")
                valid.Item("control") = "txtID"
                err = True
            End If
            txtID.Attributes.Item("data-error") = "true"
        Else
            txtID.Attributes.Item("data-error") = "false"
        End If

        If Request("cboTipoProgramacion").Trim.Equals("P") Then 'Si el tipo de programación es Periódica
            'VALIDAR FRECUENCIA
            If Request("cboTipoFrecuencia").Trim().Equals("D") Or Request("cboTipoFrecuencia").Trim().Equals("S") Then
                If String.IsNullOrEmpty(Request("txtFrecuencia")) Then 'spnFrecuencia
                    If Not err Then
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe ingresar con qué frecuencia se ejecuta la tarea"
                        valid.Item("control") = "spnFrecuencia"
                        err = True
                    End If
                End If
            End If

            If Request("cboTipoFrecuencia").Trim().Equals("S") Then
                If String.IsNullOrEmpty(Request("txtFrecuenciaDiaSemana")) Then 'cboFrecuenciaDiaSemana
                    If Not err Then
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe ingresar los días de la semana en que se ejecuta la tarea"
                        valid.Item("control") = "cboFrecuenciaDiaSemana"
                        err = True
                    End If
                End If
            End If

            If Request("cboTipoFrecuencia").Trim().Equals("M") Then
                If rbtFrecuenciaDiaMes.Checked Then
                    If String.IsNullOrEmpty(Request("txtFrecuenciaDiaMes")) Then 'spnFrecuenciaDiaMes
                        If Not err Then
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Debe ingresar el día del mes en que se ejecuta la tarea"
                            valid.Item("control") = "spnFrecuenciaDiaMes"
                            err = True
                        End If
                    End If

                    If String.IsNullOrEmpty(Request("txtFrecuenciaMes")) Then 'spnFrecuenciaMes
                        If Not err Then
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Debe ingresar la frecuencia del mes en que se ejecuta la tarea"
                            valid.Item("control") = "spnFrecuenciaMes"
                            err = True
                        End If
                    End If
                End If

                If rbtOrdinal.Checked Then
                    If String.IsNullOrEmpty(Request("txtMes")) Then 'spnMes
                        If Not err Then
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Debe ingresar cada qué mes se ejecuta la tarea"
                            valid.Item("control") = "spnMes"
                            err = True
                        End If
                    End If
                End If
            End If

            'VALIDAR FRECUENCIA DIARIA
            If Me.rbtFrecuenciaA1.Checked And String.IsNullOrEmpty(Request("txtHoraFrecuencia")) Then 'dtpHoraFrecuencia
                If Not err Then
                    valid.Item("rpta") = 0
                    valid.Item("msg") = "Debe ingresar la hora en que se ejecuta por única vez la tarea"
                    valid.Item("control") = "dtpHoraFrecuencia"
                    err = True
                End If
            End If

            If Me.rbtFrecuenciaA2.Checked Then
                If String.IsNullOrEmpty(Request("txtFrecuenciaHora")) Then 'spnFrecuenciaHora
                    If Not err Then
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe ingresar cada qué hora sucede la tarea"
                        valid.Item("control") = "spnFrecuenciaHora"
                        err = True
                    End If
                End If

                If Not err Then
                    If String.IsNullOrEmpty(Request("txtHoraIniDia")) Then 'dtpHoraIniDia
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe seleccionar la hora en que inicia la tarea"
                        valid.Item("control") = "dtpHoraIniDia"
                        err = True
                    Else
                        Try
                            Dim hora As New DateTime
                            hora = CDate(Request("txtHoraIniDia")) 'dtpHoraIniDia
                        Catch ex As Exception
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Debe seleccionar la hora en que inicia la tarea"
                            valid.Item("control") = "dtpHoraIniDia"
                            err = True
                        End Try
                    End If
                End If

                If Not err Then
                    If String.IsNullOrEmpty(Request("txtHoraFinDia")) Then 'dtpHoraFinDia
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe seleccionar la hora en que termina la tarea"
                        valid.Item("control") = "dtpHoraFinDia"
                        err = True
                    Else
                        Try
                            Dim hora As New DateTime
                            hora = CDate(Request("txtHoraFinDia"))
                        Catch ex As Exception
                            valid.Item("rpta") = 0
                            valid.Item("msg") = "Debe seleccionar la hora en que termina la tarea"
                            valid.Item("control") = "dtpHoraFinDia"
                            err = True
                        End Try
                    End If
                End If
            End If
        Else 'Si el tipo de programación es por Única Vez
            If Not err Then
                If String.IsNullOrEmpty(Request("txtHoraInicio")) Then 'dtpHoraInicio
                    valid.Item("rpta") = 0
                    valid.Item("msg") = "Debe ingresar una hora de envío válida"
                    valid.Item("control") = "dtpHoraInicio"
                    err = True
                Else
                    Try
                        Dim hora As New DateTime
                        hora = CDate(Request("txtHoraInicio")) 'dtpHoraInicio
                    Catch ex As Exception
                        valid.Item("rpta") = 0
                        valid.Item("msg") = "Debe ingresar una hora de envío válida"
                        valid.Item("control") = "dtpHoraInicio"
                        err = True
                    End Try
                End If
            End If
        End If

        'VALIDAR DURACIÓN
        If String.IsNullOrEmpty(Request("txtFechaInicio")) Then 'dtpFechaInicio
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar la fecha de inicio de la programación"
                valid.Item("control") = "dtpFechaInicio"
                err = True
            End If
            dtpFechaInicio.Attributes.Item("data-error") = "true"
        Else
            dtpFechaInicio.Attributes.Item("data-error") = "false"
        End If

        If chkFechaFin.Checked And String.IsNullOrEmpty(Request("txtFechaFin")) Then 'dtpFechaFin
            If Not err Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "Debe ingresar la fecha de término de la programación"
                valid.Item("control") = "dtpFechaFin"
                err = True
            End If
            dtpFechaFin.Attributes.Item("data-error") = "true"
        Else
            dtpFechaFin.Attributes.Item("data-error") = "false"
        End If

        If Not err And chkFechaFin.Checked Then
            Dim desde As Date = CDate(Request("txtFechaInicio")) 'dtpFechaInicio
            Dim hasta As Date = CDate(Request("txtFechaFin")) 'dtpFechaFin
            If hasta < desde Then
                valid.Item("rpta") = 0
                valid.Item("msg") = "La fecha de término de la programación no puede ser menor a la fecha inicial"
                valid.Item("control") = "dtpFechaFin"
                err = True

                dtpFechaFin.Attributes.Item("data-error") = "true"
            Else
                dtpFechaFin.Attributes.Item("data-error") = "false"
            End If
        End If

        Return valid
    End Function

    Protected Sub grwInteresados_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwInteresados.DataBound
        grwInteresados.Columns(5).Visible = (cboTipoMensaje.Value = "S")
        grwInteresados.Columns(6).Visible = (cboTipoMensaje.Value = "M")
    End Sub

    Protected Sub grwInteresados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwInteresados.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ln_Index As Integer = e.Row.RowIndex + 1

                _cellsRow(0).Text = ln_Index
                grwInteresados.HeaderRow.TableSection = TableRowSection.TableHeader

                Dim dr As DataRowView = e.Row.DataItem
                If dr.Item("verificado_emi") IsNot DBNull.Value Then
                    If Not dr.Item("verificado_emi") Then
                        e.Row.Cells(6).Attributes.Item("style") = "color: #e65a5e;"
                    End If
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try
        
    End Sub
End Class
