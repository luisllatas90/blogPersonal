﻿
Partial Class academico_horarios_frmPrestamoAlquilerAmbiente
    Inherits System.Web.UI.Page
    Public Hcodigo_cup As Integer = 381553 'Real
    Public Acodigo_cup As Integer = 381740 'ReaL
    Public Hcodigo_pes As Integer = 198
    Public Hcodigo_cac As Integer = 52
    Public Errores As String
    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Me.CheckBox1.Visible = False
        Me.CheckBox2.Visible = False
        Me.pnlRegistrar.Visible = True
        Me.pnlConsultar.Visible = False
        Me.btnRegistrar.Visible = False
        Me.lblPaso.Text = "- Primer Paso: Registro de Horario"
        Me.lblMsj.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../../sinacceso.html")
        End If
        
        If Not Page.IsPostBack Then
            Me.hdtfu.Value = CInt(Request.QueryString("ctf"))
            txtDesde.Value = Today
            txtHasta.Value = Today
            consultarTipos()
            Me.pnlRegistrar.Visible = False
            CargarFechas()
            chkVarias_CheckedChanged(sender, e)
            Me.ddlInicioMes.SelectedIndex = Today.Month - 1
            Me.ddlFinMes.SelectedIndex = Today.Month - 1
            Me.ddlInicioDia.SelectedIndex = Today.Day - 1
            Me.ddlFinDia.SelectedIndex = Day(DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)) - 1 'Today.Day + 1

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlCap.DataSource = obj.TraerDataTable("HorarioPE_ConsultarCapacidad")
            Me.ddlCap.DataTextField = "capacidad_amb"
            Me.ddlCap.DataValueField = "capacidad_amb"
            Me.ddlCap.DataBind()
            obj.CerrarConexion()
            obj = Nothing

            cargarDiaVariasSesiones()
            cargarFechaLimite()
            consultarHorarios()

            ConsultarTipoEstudio()
            Me.ddlTipoEstudio.SelectedValue = 2
            ConsultarCarreras()
        End If
        'Response.Write("LOAD:" & CInt(Me.CheckBox1.Checked) & "</br>")
        'Response.Write("LOAD:" & CInt(Me.CheckBox2.Checked) & "</br>")

    End Sub

    Sub cargarFechaLimite()
        Dim obj As New ClsConectarDatos()
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.lblLimite.Text = "Fecha Límite: " & obj.TraerDataTable("HORARIOPE_ConsultarAmbienteConfig", 1).Rows(0)(1).ToString
        obj.CerrarConexion()
    End Sub
    Sub consultarHorarios()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gridHorario.DataSource = obj.TraerDataTable("HorarioPE_ConsultarSol", CInt(Session("id_per")), IIf((Me.CheckBox1.Checked), 1, 0), IIf((Me.CheckBox2.Checked), 1, 0))
        obj.CerrarConexion()
        Me.gridHorario.DataBind()
        obj = Nothing
        Me.btnExportar.Enabled = Me.gridHorario.Rows.Count
        'Response.Write("QUERY:" & IIf((Me.CheckBox1.Checked), 1, 0) & "</br>")
        'Response.Write("QUERY:" & IIf((Me.CheckBox2.Checked), 1, 0) & "</br>")
    End Sub

    Sub consultarTipos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlTipSolicitud.DataSource = obj.TraerDataTable("HorarioPE_ListarTipoSolicitud")
        obj.CerrarConexion()
        obj = Nothing
        Me.ddlTipSolicitud.DataTextField = "nombre_ambts"
        Me.ddlTipSolicitud.DataValueField = "codigo_ambts"
        Me.ddlTipSolicitud.DataBind()
    End Sub
    Sub ConsultarTipoEstudio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        Me.ddlTipoEstudio.DataSource = obj.TraerDataTable("HorarioPE_ListarTipoEstudio")
        obj.CerrarConexion()

        Me.ddlTipoEstudio.DataTextField = "descripcion_test"
        Me.ddlTipoEstudio.DataValueField = "codigo_test"
        Me.ddlTipoEstudio.DataBind()

        obj = Nothing
    End Sub
    Sub ConsultarCarreras()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        Me.ddlCarreras.DataSource = obj.TraerDataTable("HorarioPe_ListarCarrerasxTipo", Me.ddlTipoEstudio.selectedvalue)
        obj.CerrarConexion()

        Me.ddlCarreras.DataTextField = "nombre_Cpf"
        Me.ddlCarreras.DataValueField = "codigo_Cpf"
        Me.ddlCarreras.DataBind()

        obj = Nothing

    End Sub
    Sub CargarFechas()
        Dim item As String
        For i As Integer = 7 To 23
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlInicioHora.SelectedIndex = 0
        Me.ddlFinHora.SelectedIndex = 1

        For i As Integer = 0 To 59
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlFinHora.SelectedIndex = 21


        For i As Integer = 1 To 31
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i

        For i As Integer = Today.Year To Today.Year + 1
            item = i.ToString
            Me.ddlInicioAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
            'Me.ddlSelAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
    End Sub
    Sub cargarDiaVariasSesiones()
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim fechaInicio As New Date
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text
        'fechaInicio = New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        Me.ddlDiaSelPer.SelectedValue = Replace((WeekdayName(Weekday(fechaInicio))).Substring(0, 2).ToUpper, "Á", "A")
    End Sub

    Protected Sub chkVarias_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVarias.CheckedChanged
        Me.ddlFinDia.Enabled = chkVarias.Checked
        Me.ddlFinMes.Enabled = chkVarias.Checked
        Me.ddlFinAño.Enabled = chkVarias.Checked
        Me.ddlDiaSelPer.Enabled = chkVarias.Checked
        cargarDiaVariasSesiones()
        Me.txtHasta.disabled = Not chkVarias.Checked
    End Sub

    Protected Sub gridHorario_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridHorario.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If (e.CommandName = "LimpiarAmbiente") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_LimpiarAmbiente", gridHorario.DataKeys(index).Values("codigo_lho"), 1)
            consultarHorarios()
            obj.CerrarConexion()
            obj = Nothing
        End If

        If (e.CommandName = "SolicitarAmbiente") Then
            Session("h_mod") = 0
            Session("h_id") = Session("id_per")
            Session("h_ctf") = 0
            Session("h_codigolho") = gridHorario.DataKeys(index).Values("codigo_lho")          
            Me.pnlRegistrar.Visible = False
            Me.pnlConsultar.Visible = False
            'Response.Redirect("frmSolicitarAmbienteH.aspx")
            Response.Redirect("frmSolicitarAmbienteH.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        End If
    End Sub

    Protected Sub gridHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gridHorario.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("HorarioPE_EliminarLH", gridHorario.DataKeys(e.RowIndex).Values("codigo_lho"))
        consultarHorarios()
        obj.CerrarConexion()
        obj = Nothing
        e.Cancel = True
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.lblPaso.Text = ""
        Me.pnlRegistrar.Visible = False
        Me.pnlConsultar.Visible = True
        Me.btnRegistrar.Visible = True
        Me.CheckBox1.Visible = True
        Me.CheckBox2.Visible = True
        Me.lblMsj.Text = ""

    End Sub

    Protected Sub btnRegistrarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPers.Click
        Me.lblMsj.Text = ""
        Me.lblPaso.Text = ""

    
        If Me.ddlTipSolicitud.SelectedValue = 0 Then
            Me.lblMsj.Text = "Debe seleccionar un Motivo de Solicitud"
            Me.ddlTipSolicitud.Focus()
            Exit Sub
        End If



        If Me.txtDescripcion.Text.Trim = "" And Me.ddlTipSolicitud.selectedvalue <> 10 Then
            Me.lblMsj.Text = "Debe escribir una descripción de la actividad"
            Me.txtDescripcion.Focus()
            Exit Sub
        End If

        If Me.ddlTipSolicitud.selectedvalue = 10 Then
            If Me.txtBachilleres.text.trim = "" Then
                Me.lblMsj.Text = "Debe indicar el nombre del bachiller"
                Me.txtBachilleres.Focus()
                Exit Sub
            End If

        End If
        If Me.chkRequerimieto.Checked Then
            If Me.txtRequerimiento.Text.Trim = "" Then
                Me.lblMsj.Text = "Debe indicar requerimiento"
                Me.txtRequerimiento.Focus()
                Exit Sub
            End If

        End If

    


        Dim fechaInicio As New Date
        Dim fechaFin As New Date
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim día As String
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text

        If nombre_hor = horaFin Then
            Me.lblMsj.Text = "Hora de Inicio y Fin de la actividad no pueden ser iguales"
            Exit Sub
        End If

        fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Month, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Day, CDate(txtHasta.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)
        día = Me.ddlDiaSelPer.SelectedValue

        If ValidarFecha(fechaInicio, fechaFin) Then

            '##Validar día domingo
            If WeekdayName(Weekday(fechaInicio)) = "domingo" Then
                Me.pnlPregunta.Visible = True
                Me.pnlRegistrar.Visible = False
                Me.lblActividad.Text = Me.txtDescripcion.Text
                Me.lblFecha.Text = Me.txtDesde.Value
                Exit Sub
            End If
            RegistrarHorario()
            btnCancelar_Click(sender, e)

        End If



        '
        ''
    End Sub
    Public Function ValidarFecha(ByVal fechaInicio As Date, ByVal fechaFin As Date) As Boolean
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Me.lblMsj.Text = ""

        ' 0. hora correctas
        If (CDate(fechaInicio.ToString("H:mm")) > CDate(fechaFin.ToString("H:mm"))) And Me.chkVarias.Checked = False Then
            Me.lblMsj.Text = "No se puede registrar: Verifique las horas"
            Return False
        End If

        '1. Fuera de Fecha
        If hdtfu.Value <> 138 Then
            If CDate(fechaInicio.ToString("dd/MM/yyyy")) < CDate(Today) Then
                Me.lblMsj.Text = "No puede registrar solicitudes en días anteriores al día de hoy"
                Return False
            End If
        End If

        '2. Fecha Límite
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HORARIOPE_ConsultarAmbienteConfig", 1)
        Dim FechaLimite As Date
        FechaLimite = CDate(tb.Rows(0)(1))
        obj.CerrarConexion()
        If CDate(fechaInicio.ToString("dd/MM/yyyy")) > CDate(FechaLimite.ToString("dd/MM/yyyy")) And chkAudi.Checked = False Then
            Me.lblMsj.Text = "Solo pueden registrarse solicitudes hasta el " & FechaLimite
            Return False
        End If


        '5. Registrar solicitudes con 2 días hábiles anticipados (sin contar sabados,domingo, feriados)
        'No restringir para perfil: COORDINADOR DE DIRECCIÓN ACADÉMICA (mfernandez, elsa hernandez y martha tesen, eurpeque)


        If hdtfu.Value <> 138 And hdtfu.Value <> 238 Then
            'If Session("id_per") <> 1415 And Session("id_per") <> 679 And Session("id_per") <> 475 Then
            Dim fechaMin As Date = CDate(Date.Now.ToString("dd/MM/yyyy"))
            Dim fechaActual As Date = CDate(Date.Now.ToString("dd/MM/yyyy"))
            fechaMin = fechaMin.AddDays(1)
            Dim log As String = ""
            Dim nroDias = 0
            Do
                obj.AbrirConexion()
                tb = obj.TraerDataTable("HorarioPE_ConsultarFeriado", CInt(DatePart(DateInterval.Day, CDate(fechaMin))), CInt(DatePart(DateInterval.Month, CDate(fechaMin))))
                obj.CerrarConexion()
                If tb.Rows.Count Then

                ElseIf (WeekdayName(Weekday(fechaMin)) = "domingo" Or WeekdayName(Weekday(fechaMin)) = "sábado") Then

                Else
                    nroDias += 1
                End If

                If nroDias <= 1 Then
                    fechaMin = fechaMin.AddDays(1)
                End If
            Loop While nroDias < 1


            If Not CDate(fechaInicio.ToString("dd/MM/yyyy")) >= CDate(fechaMin.ToString("dd/MM/yyyy")) Then
                Me.lblMsj.Text = "La solicitudes deben registrarse con un mínimo de 1 día hábil anticipado "
                Return False
            End If

        End If


        '3. Feriados
        obj.AbrirConexion()
        tb = obj.TraerDataTable("HorarioPE_ConsultarFeriado", CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))))
        obj.CerrarConexion()
        If tb.Rows.Count Then
            Me.lblMsj.Text = "No puede registrar solicitud para un Día No Laborable"
            Return False
        End If

        '4. No registrar domingos: Valido para varias solicitudes
        If WeekdayName(Weekday(fechaInicio)) = "domingo" And Me.chkVarias.Checked Then
            Return False
        End If



        Return True
    End Function
    Sub RegistrarHorario()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString       
        Dim día As String
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim usu As Integer = CInt(Session("id_per"))
      
        día = Me.ddlDiaSelPer.SelectedValue
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text

        Dim fechaInicio As New Date
        Dim fechaFin As New Date

        If chkAlquiler.Checked Then
            Hcodigo_cup = Acodigo_cup
        End If

        Dim codigo_Lho As Integer = 0


        Dim nroAmbientes As Integer = Me.ddlNro.SelectedValue
        For i As Integer = 1 To nroAmbientes
            fechaInicio = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
            fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Month, CDate(txtHasta.Value))), CInt(DatePart(DateInterval.Day, CDate(txtHasta.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)

            If chkVarias.Checked Then
                If fechaInicio < fechaFin Then
                    Do
                        tb = New Data.DataTable
                        If WeekdayName(Weekday(fechaInicio)) = Me.ddlDiaSelPer.SelectedItem.Text.ToLower Then

                            If ValidarFecha(fechaInicio, fechaFin) Then
                                'obj.AbrirConexion()
                                obj.IniciarTransaccion()
                                Dim fechaInicioG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaInicio.Hour, fechaInicio.Minute, 0)
                                Dim fechaFinG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaFin.Hour, fechaFin.Minute, 0)
                                tb = obj.TraerDataTable("HorarioPE_Registrar", día, Hcodigo_cup, nombre_hor, horaFin, usu, fechaInicioG, fechaFinG, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value) & IIf(Me.ddlTipSolicitud.SelectedValue = 10, Me.ddlCarreras.SelectedItem.Text & " / " & Me.txtBachilleres.Text.Trim.ToUpper, ""), Me.ddlCap.SelectedValue, Me.ddlTipSolicitud.SelectedValue, CInt(Me.chkAudi.Checked))

                                If Me.chkRequerimieto.Checked Then
                                    codigo_Lho = tb.Rows(0).Item(0)
                                    obj.Ejecutar("Lho_RequerimientoAmbienteRegistro", codigo_Lho, CInt(Session("id_per")), Me.txtRequerimiento.Text.Trim)
                                End If

                                obj.TerminarTransaccion()
                                'obj.CerrarConexion()
                            Else
                                Me.lblMsj.Visible = False
                                Me.lblMsj.Text = Me.lblMsj.Text & "// No se puede registrar: " & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & " : " & Errores
                            End If
                        End If
                        fechaInicio = fechaInicio.AddDays(1)
                    Loop While fechaInicio <= fechaFin
                    Me.lblMsj.Visible = True
                Else
                    Response.Write("Fecha Fin debe ser mayor a Fecha Inicio")
                    Exit Sub
                End If
            Else
                fechaFin = New Date(CInt(DatePart(DateInterval.Year, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Month, CDate(txtDesde.Value))), CInt(DatePart(DateInterval.Day, CDate(txtDesde.Value))), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)
                'obj.AbrirConexion()
                If ValidarFecha(fechaInicio, fechaFin) Then
                    'obj.AbrirConexion()
                    obj.IniciarTransaccion()
                    Dim culture As New System.Globalization.CultureInfo("es-ES")
                    día = (culture.DateTimeFormat.GetDayName(fechaInicio.DayOfWeek)).ToString.Substring(0, 2).ToUpper
                    día = Replace(día, "Á", "A")
                    tb = obj.TraerDataTable("HorarioPE_Registrar", día, Hcodigo_cup, nombre_hor, horaFin, usu, fechaInicio, fechaFin, 1, IIf(Me.txtDescripcion.Text.Length > 0, Me.txtDescripcion.Text.Trim, DBNull.Value) & IIf(Me.ddlTipSolicitud.SelectedValue = 10, Me.ddlCarreras.SelectedItem.Text & " / " & Me.txtBachilleres.Text.Trim.ToUpper, ""), Me.ddlCap.SelectedValue, Me.ddlTipSolicitud.SelectedValue, CInt(Me.chkAudi.Checked))
                    If Me.chkRequerimieto.Checked Then
                        codigo_Lho = tb.Rows(0).Item(0)
                        obj.Ejecutar("Lho_RequerimientoAmbienteRegistro", codigo_Lho, CInt(Session("id_per")), Me.txtRequerimiento.Text.Trim)
                    End If
                    obj.TerminarTransaccion()
                    'obj.CerrarConexion()
                Else

                    Me.lblMsj.Text = "No se puede registrar: " & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & " : " & Errores
                End If
            End If
        Next

        If tb.Rows.Count And (Me.chkVarias.Checked = False) Then
            Me.lblMsj.Text = "Se registró el horario: " & fechaInicio.ToString.Substring(0, 10) & " de " & nombre_hor & " a " & horaFin
            'Redireccionar a Solicitar
            Session("h_codigo_cup") = Hcodigo_cup
            Session("h_nombre_cur") = "Préstamo de Ambiente"
            Session("h_mod") = 0
            Session("h_id") = Session("id_per")
            Session("h_ctf") = 0
            Session("h_codigolho") = tb.Rows(0).Item(0)
            Me.pnlRegistrar.Visible = False
            Me.pnlConsultar.Visible = False
            Response.Redirect("frmSolicitarAmbienteH.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        End If
        
        tb.Dispose()
        obj = Nothing
        'consultarHorarios()
    End Sub

    Private Function EnviarCorreoReqAudiovisuales(ByVal codigo_Lho As Integer) As Boolean
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim strMensaje As String = ""

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim De, Asunto As String
        Dim EmailDestino As String = ""
        Dim EmailDestino2 As String = ""
        Dim EmailVarios As String = ""
        Dim rpta As Boolean = False
        Dim rptaEmail As String = ""
        Dim dta As Integer = 0

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Lho_DatosReqAudiovisuales", codigo_Lho)
            obj.CerrarConexion()

            'EmailVarios = "audiovisuales@usat.edu.pe;campusvirtual@usat.edu.pe"
            EmailVarios = "epena@usat.edu.pe;fatima.vasquez@usat.edu.pe"



            De = "campusvirtual@usat.edu.pe"
            Asunto = "Solicitud de Ambiente"

            strMensaje = "Estimado(a): <br/><br/>"
            strMensaje = strMensaje & "El proceso solicitud de ambiente presente el siguiente requerimiento: <br>"
            strMensaje = strMensaje & "<br><br>"
            strMensaje = strMensaje & "<br><br>"


            'rpta = cls.EnviarMailVariosV2(De, EmailVarios, Asunto, strMensaje, True)
            rpta = cls.EnviarMailVariosV3(De, EmailVarios, Asunto, strMensaje, True)





            'RegistroBitacoraCorreo(De, EmailVarios, Asunto, strMensaje, rpta, dft, dta, codigoEmail, msgEmail)

            cls = Nothing
            obj = Nothing
            Return rpta
        Catch ex As Exception

            Return rpta
        End Try
    End Function

    Protected Sub gridHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridHorario.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(7).Text = "1" Then
                e.Row.Cells(7).Text = "<img src='images/star.png' title='Ambiente preferencial'>"
           
            ElseIf e.Row.Cells(7).Text = "0" And e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(7).Text = "-"
            ElseIf e.Row.Cells(7).Text = "0" Then
                e.Row.Cells(7).Text = "<img src='images/door.png' title='Ambiente normal'>"
            End If

            e.Row.Cells(8).Font.Bold = True
            e.Row.Cells(3).Font.Bold = True

            If e.Row.Cells(8).Text = "Sin ambiente solicitado" Then
                e.Row.Cells(11).Text = "-"
            End If

            If e.Row.Cells(9).Text = "Pendiente" Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Green
            Else
                e.Row.Cells(9).ForeColor = Drawing.Color.Blue
            End If

            If e.Row.Cells(9).Text = "Pendiente" And e.Row.Cells(8).Text <> "Sin ambiente solicitado" Then
                e.Row.Cells(10).Text = "-"
            End If
            If e.Row.Cells(9).Text = "Asignado" Then
                e.Row.Cells(10).Text = "-"
                e.Row.Cells(11).Text = "-"
                e.Row.Cells(12).Text = "-"
            End If
           

            If CDate(e.Row.Cells(2).Text) < CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.Gray
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Finalizado"
                e.Row.Cells(10).Text = "-"
                e.Row.Cells(11).Text = "-"
            End If
            If CDate(e.Row.Cells(2).Text) = CDate(Today) Then
                e.Row.Cells(9).ForeColor = Drawing.Color.IndianRed
                e.Row.Cells(9).Text = e.Row.Cells(9).Text & " - Hoy"

            End If
        End If

    End Sub


   
    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        Me.pnlPregunta.Visible = False
        RegistrarHorario()
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.pnlPregunta.Visible = False
        Me.pnlRegistrar.Visible = True
        Me.txtDesde.Focus()
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click        
        Response.Redirect("frmPrestamoAlquilerAmbienteExportar.aspx?id=" & Session("id_per") & "&val1=" & IIf((Me.CheckBox1.Checked), 1, 0).ToString & "&val2=" & IIf((Me.CheckBox2.Checked), 1, 0).ToString)


    End Sub

 
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        consultarHorarios()
    End Sub

    Protected Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        consultarHorarios()
    End Sub

    Protected Sub ddlTipSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipSolicitud.SelectedIndexChanged
        Try
            If ddlTipSolicitud.selectedvalue = 10 Then 'Si es sustentacion

                Me.lblBachilleres.visible = True
                Me.txtbachilleres.visible = True
                Me.lbldescripcion.visible = False
                Me.txtDescripcion.visible = False

                Me.ddlTipoEstudio.visible = True
                Me.ddlCarreras.visible = True

                lblTipoEstudio.visible = True
                lblCarreras.visible = True

                Me.ddlTipoEstudio.focus()
            Else

                Me.lblBachilleres.visible = False
                Me.txtbachilleres.visible = False
                Me.lbldescripcion.visible = True
                Me.txtDescripcion.visible = True

                Me.ddlTipoEstudio.visible = False
                Me.ddlCarreras.visible = False

                Me.txtDescripcion.focus()

                lblTipoEstudio.visible = False
                lblCarreras.visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCarreras_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreras.SelectedIndexChanged

    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        ConsultarCarreras()
    End Sub

    Protected Sub chkRequerimieto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRequerimieto.CheckedChanged
        If Me.chkRequerimieto.Checked Then
            Me.txtRequerimiento.Visible = True
            lblReqInfoEmail1.Visible = True
            lblReqInfoEmail2.Visible = True
        Else
            Me.txtRequerimiento.Visible = False
            lblReqInfoEmail1.Visible = False
            lblReqInfoEmail2.Visible = False
        End If
    End Sub
End Class
