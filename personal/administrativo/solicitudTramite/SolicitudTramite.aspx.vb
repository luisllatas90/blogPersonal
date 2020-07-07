Imports System.Collections.Generic
Imports System.IO
Imports System.Xml

Partial Class _SolicitudTramite
    Inherits System.Web.UI.Page
    Dim cod_ST As Integer
    Dim anio, mes As Integer
    Dim estado As String
    Dim respuesta As String
    Dim valida As Integer
    Dim tamano, extension As Integer
    Dim guarda_envia As Integer = 0
    Dim dPend As Integer 'se añade 28-03
    'Dim alt_Desde, alt_Hasta As Date
    'Dim fecha_ini As Date
    Dim fecha_cumple As Date
    'Dim vale_check As Integer
    Dim limite_vac As Integer = 15 '07/01/2020 /  29/11-19 Para definir el día límite del mes para solicitar Vacaciones

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If Not IsPostBack Then

            lblFechaIni.Visible = False
            lblAuxPend.Visible = False
            lblCorreo.Visible = False

            cod_ST = Request.QueryString("cod")

            'Me.divConfirmaGuardar.Visible = False
            'Me.divConfirmaGuadarEnviar.Visible = False

            carga_horas()

            Me.txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Me.txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy")

            Me.txtDesde.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
            Me.txtHasta.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)

            'calcula_dias() 'Se bloquea el 05-07

            ConsultarTipoSolicitud()

            Me.chkCumple.STYLE.ITEM("float") = "left"
            Me.ftchkCumple.STYLE.ITEM("margin-top") = "3px"
            Me.ftchkCumple.STYLE.ITEM("margin-left") = "3px"

            ftchkCumple.visible = False
            lblCumple.visible = False 'se añade 10-04
            chkCumple.visible = False

            ConsultarSolicitudTramite()

            ConsultarAdjuntos()

            If Me.lblEstado.Text = "Rechazado" Or Me.lblEstado.Text = "Enviado" Or Me.lblEstado.Text = "Aprobado Director" Or Me.lblEstado.Text = "Aprobado Personal" Then
                desactiva_controles()
                lblAvisoTipoSol.Visible = False '11/09
                lblMotivoDescansoMedico.visible = False
                lblVacaciones.visible = False 'Se añadió
            Else
                activa_controles()
                lblAvisoSubida.Text = ""
                valida_aviso()
            End If

            'valida_cumple() 'se añade 09-05-19
            verifica_dias() '20/08

            lblDesde.text = Me.txtDesde.Text
            lblHasta.text = Me.txtHasta.Text

            'guarda_envia = 0 'Se añade

            divSaldo.Visible = False

            Dim objcrm As New ClsCRM
            'Me.btnAdjunto.Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & objcrm.EncrytedString64(Trim(Me.lblNumero_Tramite.Text)) & "')")
            'Me.btnAdjunto.Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & objcrm.EncrytedString64(Trim(Me.lblNumero_Tramite.Text)) & "')")
            'Me.btnAdjunto.Attributes.Add("OnClick", "javascript:ModalAdjuntar2('" & Me.lblNumero_Tramite.Text & "')")

            lblCorreo.text = "N" ' se añade para validar el envio de correo

            If cod_ST = 0 Then '11/09
                oculta_capacitacion()
            End If

        End If

    End Sub

    Public Sub ConsultarSolicitudTramite()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaSolicitudTramite", cod_ST)
            obj.CerrarConexion()

            'Me.lblNumero_Tramite.Font.Italic = IIf(cod_ST <> "", True, False)
            Me.lblNumero_Tramite.Text = dt.Rows(0).Item("codigo_ST")
            Me.lblNumero_Tramite1.Text = dt.Rows(0).Item("codigo_ST")

            Me.lblEstado.Text = dt.Rows(0).Item("Estado")

            If Me.lblEstado.Text <> "Nuevo" Then 'Se añade 02-07
                lblNumDias.text = "Días Solicitados :"
            End If

            If dt.Rows(0).Item("Estado") <> "Generado" Then
                Me.btnGuardar.Enabled = False
                Me.btnGuardarEnviar.Enabled = False
            End If

            If dt.Rows(0).Item("Estado") = "Enviado" Then
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("fechaEnvio_ST"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de "
                'Me.files.Enabled = False ---Se bloqueó
            ElseIf dt.Rows(0).Item("Estado") = "Generado" Then 'Si es Generado es porque se va a Editar
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("fechaActualizacion_ST"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Actualización de "
            Else 'Para Aprobados
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de "
                'Me.files.Enabled = False  ---Se bloqueó
            End If

            If dt.Rows(0).Item("Prioridad") = "Urgente" Then
                Me.ddlPrioridad.ForeColor = Drawing.Color.OrangeRed
                Me.ddlPrioridad.SelectedIndex = 1 'Urgente
            Else
                Me.ddlPrioridad.SelectedIndex = 0 'Normal
            End If

            Me.ddlTipoSolicitud.SelectedValue = dt.Rows(0).Item("codigo_TST")
            If dt.Rows(0).Item("codigo_TST") = 2 Then 'Licencia C/Goce
                ConsultarClasificacionSolicitudTramite() '17/09

                If dt.Rows(0).Item("codigo_CST") <= 2 Then 'Otros o Descanso Médico
                    'ddlTipoLicencia.Visible = True '17/09 Siempre tiene q mostrarse
                    ddlTipoLicencia.SelectedValue = dt.Rows(0).Item("codigo_CST") '17/09
                    oculta_parcial() '17/09
                    If dt.Rows(0).Item("codigo_CST") = 2 Then 'Descanso médico
                        Me.lblAvisoTipoSol.Text = "*Debe entregar el sustento original dentro de las próximas 48 hrs. a su jefe inmediato o a la Asistenta Social"
                        Me.lblMotivoDescansoMedico.Text = "*Debe especificar el motivo de salud"
                    End If
                Else
                    ConsultarLicenciaCapacitacion() '02/08 Si es Licencia Capacitación
                End If

            Else
                oculta_capacitacion()
                ddlTipoLicencia.SelectedValue = 0 '23/09
            End If

            Me.txtmotivo.Text = dt.Rows(0).Item("motivo")

            Me.txtDesde.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortDate)
            Me.txtHasta.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortDate)
            calcula_dias()

            If dt.Rows(0).Item("codigo_TST") = 3 Then 'Vacaciones
                ConsultaVacaciones()
            End If
            If dt.Rows(0).Item("codigo_TST") = 4 Then 'Permiso
                'Me.ddlHoraInicio.Text = CDate(dt.Rows(0).Item("fechahoraInicio_ST")).ToString("MM\/dd\/yyyy")
                'Me.ddlHoraFin.Text = CDate(dt.Rows(0).Item("fechahoraFin_ST")).ToString("HH:mm")
                Me.ddlHoraInicio.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                Me.ddlHoraFin.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                calcula_horas()
                chkCumple.Visible = True 'se añade 10-04-19
                ftchkCumple.Visible = True 'se añade 12-04-19

                valida_cumple()

                If Month(fecha_cumple) = Month(CDate(Me.txtDesde.Text)) And Day(fecha_cumple) = Day(CDate(Me.txtDesde.Text)) Then
                    lblchkCump.Text = "1"
                    chkCumple.Checked = True
                    desactiva_controles()
                    Me.txtmotivo.Enabled = False
                    lblCumple.Visible = True
                Else
                    lblCumple.Visible = False
                End If

            End If

            valida_TipoSolicitud()

            Me.txtObservacion.Text = dt.Rows(0).Item("Observacion_Director") 'Se añadió
            Me.txtObservacionPersonal.Text = dt.Rows(0).Item("Observacion_Personal") 'Se añadió

        Catch ex As Exception
            'Me.lblMensaje0.Text = "Error al cargar la Solicitud de Trámite"
        End Try

    End Sub

    Public Sub ConsultarTipoSolicitud()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaTipoSolicitudTramite", "")
            obj.CerrarConexion()

            Me.ddlTipoSolicitud.DataTextField = "nombre_TST"
            Me.ddlTipoSolicitud.DataValueField = "codigo_TST"
            Me.ddlTipoSolicitud.DataSource = dt
            Me.ddlTipoSolicitud.DataBind()

            valida_TipoSolicitud()

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los Tipos de Solicitud Trámite"
        End Try
    End Sub

    Public Sub ConsultarClasificacionSolicitudTramite()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaClasificacionSolicitudTramite", 2) 'Clasificacion de Licencias C/Goce
            obj.CerrarConexion()

            Me.ddlTipoLicencia.DataTextField = "Nombre"
            Me.ddlTipoLicencia.DataValueField = "codigo_CST"
            Me.ddlTipoLicencia.DataSource = dt
            Me.ddlTipoLicencia.DataBind()

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los Tipos de Solicitud Trámite"
        End Try
    End Sub

    Public Sub ConsultarTipoMotivoCapacitacion()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaTipoMotivoSolicitudTramite", Me.ddlTipoLicencia.selectedvalue)
            obj.CerrarConexion()

            Me.ddlMotivoLicencia.DataTextField = "Nombre"
            Me.ddlMotivoLicencia.DataValueField = "codigo_TMS"
            Me.ddlMotivoLicencia.DataSource = dt
            Me.ddlMotivoLicencia.DataBind()

            Me.ddlMotivoLicencia.Items.Add("Seleccione..") 'Se añade
            Me.ddlMotivoLicencia.text = "Seleccione.."

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los Tipos de Motivos por Capacitación"
        End Try

    End Sub

    'Public Sub ConsultarDetalleMotivoCapacitacion()

    '    Try
    '        Dim dt As New Data.DataTable
    '        Dim obj As New ClsConectarDatos
    '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        obj.AbrirConexion()

    '        dt = obj.TraerDataTable("ListaDetalleMotivoSolicitudTramite", Me.ddlMotivoLicencia.selectedvalue)
    '        obj.CerrarConexion()

    '        Me.ddlDetalleLicencia.DataTextField = "Nombre"
    '        Me.ddlDetalleLicencia.DataValueField = "codigo_DMS"
    '        Me.ddlDetalleLicencia.DataSource = dt
    '        Me.ddlDetalleLicencia.DataBind()

    '        Me.ddlDetalleLicencia.Items.Add("Seleccione..") 'Se añade
    '        Me.ddlDetalleLicencia.text = "Seleccione.."

    '    Catch ex As Exception
    '        Me.lblMensaje0.Text = "Error al cargar los Detalle de los Motivos por Capacitación"
    '    End Try

    'End Sub

    Public Sub ConsultarTipoActividad()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaTipoActividadCapacitaciones")
            obj.CerrarConexion()

            Me.ddlTipoActividad.DataTextField = "Nombre"
            Me.ddlTipoActividad.DataValueField = "codigo_TAC"
            Me.ddlTipoActividad.DataSource = dt
            Me.ddlTipoActividad.DataBind()

            Me.ddlTipoActividad.Items.Add("Seleccione..") 'Se añade
            Me.ddlTipoActividad.text = "Seleccione.."

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los Tipos de Actividad para Capacitaciones"
        End Try

    End Sub

    Public Sub ConsultarPais()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ConsultarPais", "T", "")
            obj.CerrarConexion()

            Me.ddlPais.DataTextField = "nombre_Pai"
            Me.ddlPais.DataValueField = "codigo_Pai"
            Me.ddlPais.DataSource = dt
            Me.ddlPais.DataBind()

            Me.ddlPais.Items.Add("Seleccione..") 'Se añade
            Me.ddlPais.text = "Seleccione.."

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los países"
        End Try

    End Sub

    Private Sub ConsultarLicenciaCapacitacion()
        Try
            'Para obtener el campo de observacion del registro:
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            'Response.Write(cod_ST)

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaLicenciaCapacitacion", cod_ST)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                'Response.Write(dt.Rows(0).Item("codigo_TAC"))

                Me.lblAvisoTipoSol.Text = "*Nota: Por favor complete todos los campos solicitados sobre la Licencia por Capacitación"

                muestra_capacitacion()
                lblMotivoLicencia.Visible = True
                ddlMotivoLicencia.Visible = True
                'ddlDetalleLicencia.Visible = True

                ConsultarClasificacionSolicitudTramite()
                ddlTipoLicencia.SelectedValue = 3 'Capacitación

                ConsultarTipoMotivoCapacitacion()
                ddlMotivoLicencia.SelectedValue = dt.Rows(0).Item("codigo_TMS")

                'ConsultarDetalleMotivoCapacitacion()
                'ddlDetalleLicencia.SelectedValue = dt.Rows(0).Item("codigo_DMS")

                'If dt.Rows(0).Item("codigo_TAC") = 0 Then
                '    ddlTipoActividad.Visible = False
                'Else
                '    ddlTipoActividad.Visible = True
                '    ConsultarTipoActividad()
                '    ddlTipoActividad.SelectedValue = dt.Rows(0).Item("codigo_TAC")
                'End If
                lblPapel.Visible = True '19/09
                ddlTipoActividad.Visible = True
                ConsultarTipoActividad()
                ddlTipoActividad.SelectedValue = dt.Rows(0).Item("codigo_TAC")

                ConsultarPais()
                ddlPais.SelectedValue = dt.Rows(0).Item("codigo_Pai")

                txtActividad.Text = dt.Rows(0).Item("actividad_DLC")
                txtCiudad.Text = dt.Rows(0).Item("ciudad_DLC")
                txtInstitucion.Text = dt.Rows(0).Item("institucion_DLC")

                Me.txtmotivo.ForeColor = Drawing.Color.DarkRed  '22/08

            Else
                oculta_capacitacion()
            End If

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Evaluación de Solicitud de Trámite"
        End Try

    End Sub

    Private Sub ConsultaVacaciones()

        'Se añade para obtener el número de días como saldo de vacaciones
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("spPla_ProvisionVacacionesTotalAnual", CInt(Session("id_per")), DateTime.Now.ToString("dd/MM/yyyy"))
        obj.CerrarConexion()

        lblDPendi.text = dt.Rows(0).Item("DiasPendientes")

        Dim cadena As String
        Dim position As Integer

        cadena = Str(trim(lblDPendi.text)) '02/07/19 : 107, 112 días

        If cadena.Length > 2 Then 'quiere decir que puede se decimal
            position = cadena.IndexOf(".")
            If position > 0 Then
                If Val(cadena) < 1 Then
                    lblDPendi.Text = "0"
                Else
                    lblDPendi.Text = cadena.Substring(0, position)
                End If
            Else
                'position = 3
                'lblAuxPend.Text = cadena.Substring(0, position)
                'lblDPendi.text = lblAuxPend.Text
                'dPend = val(lblDPendi.text) 'se añade 28-03
            End If
        End If

        valida_mes_registro() 'Vacaciones

        calcula_dias()

        If Me.lblNumero_Tramite.Text = "Nuevo" Then
            oculta_dias() 'Se añade el 04-07 : cuando seleccione Vacaciones no se debe mostrar los días
        End If

        'valida_pendientes() ' SE AÑADE, se bloquea 30-05-19

        saldo_dias()
        'Se añadió, a la fecha final se suma los días pendientes    
        'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)

    End Sub

    Private Sub verifica_dias() 'se crea 20/08 para no permitir enviar una solicitud guardada de fecha anterior

        If lblEstado.Text = "Nuevo" Or lblEstado.Text = "Generado" Then
            If Today > CDate(txtDesde.Text) Then
                Me.txtDesde.Text = Today
            End If

            If Today > CDate(txtHasta.Text) Then
                Me.txtHasta.Text = Today
            End If
        End If

    End Sub

    Private Sub valida_mes_registro()

        If Day(Today) > limite_vac And Month(CDate(Me.txtDesde.Text)) = Month(Today) Then '22/08/19 // Cambió de > 15 por limite_vac el 01/07/2019, Solicitado por CCHAVEZ
            If Me.lblEstado.Text = "Nuevo" Or Me.lblEstado.Text = "Generado" Then '05-07 Se añade Generado y cambia a lblEstado
                Me.txtDesde.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 1)
                Me.txtHasta.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 1)
                calcula_dias()
            End If
        End If

    End Sub

    Public Sub calcula_dias()
        Me.lblNumDias.visible = True
        Me.lblNum_dias.visible = True
        Me.lblNum_dias.Text = Str(DateDiff("d", Me.txtDesde.Text, Me.txtHasta.Text) + 1)
    End Sub

    Private Sub oculta_dias()
        Me.lblNumDias.visible = True
        Me.lblNum_dias.visible = True
        Me.lblNum_dias.Text = "--"
    End Sub

    Private Sub saldo_dias()

        Dim saldo As Integer

        saldo = Val(lblDPendi.Text) - Val(lblNum_dias.Text) 'Saldo de días de Vacaciones
        lblSPend.Text = saldo

        If Val(lblSPend.Text) < 0 Then
            lblSPend.ForeColor = Drawing.Color.OrangeRed 'Rojo
        Else
            lblSPend.ForeColor = Drawing.Color.DarkSlateBlue
        End If

    End Sub

    Private Sub valida_pendientes()

        Dim dt1 As New Data.DataTable
        Dim obj1 As New ClsConectarDatos
        obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj1.AbrirConexion()
        dt1 = obj1.TraerDataTable("spPla_ConsultarDatosPersonal", "TO", CInt(Session("id_per"))) 'se añade 27-03-19
        obj1.CerrarConexion()

        'fecha_ini = dt1.Rows(0).Item("fechaini_Per")
        lblFechaIni.text = dt1.Rows(0).Item("fechaini_Per") 'se añade

        'Response.Write(lblFechaIni.text)

        If Year(CDate(txtDesde.Text)) = Year(DateTime.Now.ToString("dd/MM/yyyy")) Then
            If Month(CDate(txtDesde.Text)) < Month(CDate(lblFechaIni.text)) Then

                If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then
                    'lblAuxPend.Text = STR(val(lblDPendi.text) - 30)

                ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then

                    If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
                        'lblAuxPend.Text = STR(val(lblDPendi.text) - 30)
                    ElseIf DAY(CDate(txtHasta.Text)) >= DAY(CDate(lblFechaIni.text)) Then
                        lblDPendi.Text = STR(val(lblDPendi.text) + 30)
                    End If

                ElseIf Month(CDate(txtHasta.Text)) > Month(CDate(lblFechaIni.text)) Then

                    lblDPendi.Text = STR(val(lblDPendi.text) + 30)

                End If

            ElseIf Month(CDate(txtDesde.Text)) = Month(CDate(lblFechaIni.text)) Then

                If DAY(CDate(txtDesde.Text)) < DAY(CDate(lblFechaIni.text)) Then

                    If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then
                        'lblAuxPend.Text = STR(val(lblDPendi.text) - 30)

                    ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then
                        If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
                            'lblAuxPend.Text = STR(val(lblDPendi.text) - 30)
                        Else
                            lblDPendi.Text = STR(val(lblDPendi.text) + 30)
                        End If
                    End If

                ElseIf DAY(CDate(txtDesde.Text)) >= DAY(CDate(lblFechaIni.text)) Then
                    lblDPendi.Text = STR(val(lblDPendi.text) + 30)
                End If

            ElseIf Month(CDate(txtDesde.Text)) > Month(CDate(lblFechaIni.text)) Then

                lblDPendi.Text = STR(val(lblDPendi.text) + 30)

            End If
        End If

        'actualiza_pendiente()

    End Sub

    'Private Sub actualiza_pendiente()

    '    If (VAL(lblAuxPend.Text) = val(lblDPendi.Text)) Or VAL(lblAuxPend.Text) = (val(lblDPendi.Text) + 30) Then
    '        lblDPendi.Text = lblAuxPend.Text
    '    End If

    'End Sub

    Private Sub valida_aviso()

        If Me.ddlTipoSolicitud.SelectedValue = 2 Then 'Licencia C/G
            Me.lblAvisoTipoSol.Visible = True '11/09
            Me.lblMotivoDescansoMedico.Visible = True
        Else
            Me.lblAvisoTipoSol.Visible = False '11/09
            Me.lblMotivoDescansoMedico.Visible = False
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Se añadió: Vacaciones
            lblVacaciones.visible = True
            Me.lbladjunto1.Visible = False
            Me.lbladjunto2.Visible = False
        Else
            lblVacaciones.visible = False

            If chkCumple.checked = True Then 'se añade 11-04
                Me.lbladjunto1.Visible = False
                Me.lbladjunto2.Visible = False
            Else
                Me.lbladjunto1.Visible = True
                Me.lbladjunto2.Visible = True
            End If

        End If

    End Sub

    Public Sub ConsultarAdjuntos()
        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        Me.celdaGrid.Visible = True
        Me.celdaGrid.InnerHtml = ""

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaAdjuntoTramite", cod_ST)

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()
                hddEliminar.Value = True
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Me.celdaGrid.Visible = True
                If ddlTipoSolicitud.selectedvalue <> 3 Then 'Se añadió: diferente de Vacaciones
                    Me.celdaGrid.InnerHtml = "*Aviso: No existen Archivos Adjuntos relacionados"
                End If

                hddEliminar.Value = False

                Me.lblAdjuntos1.Visible = False 'Se añadió

            End If
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje0.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try
    End Sub

    Private Sub registrar_filtros()
        anio = Request.QueryString("anio")
        mes = Request.QueryString("mes")
        estado = Request.QueryString("estado")
    End Sub

    'Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
    '    Me.divFormulario.Visible = False
    '    Me.divConfirmaGuadar.Visible = True
    'End Sub

    Private Sub parteGuardar()

        valida_sesion()
        Dim vMensaje As String = ""

        Dim motivo As String '09/08/19
        motivo = Trim(Me.txtmotivo.Text)

        'Se añade 17-01-2019 para validar que los días de vacaciones pedido no sea mayor a los pendientes
        If Me.ddlTipoSolicitud.SelectedValue = 3 Then
            'Me.txtmotivo.Text = "" '09/08/19  
            'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)        
            If Val(lblNum_dias.Text) - Val(lblDPendi.Text) >= 1 Then
                vMensaje = "*Aviso : No dispone de esta cantidad de días (" & Val(lblNum_dias.Text) & ") para gozar como Vacaciones. Consulte con el Área de Personal."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.btnConfirmarGuardarSI.enabled = True 'Se añadió
                'Me.txtDesde.Text = FormatDateTime(lblDesde.text, DateFormat.ShortDate)
                'Me.txtHasta.Text = FormatDateTime(lblHasta.text, DateFormat.ShortDate)
                valida_mes_registro()
                saldo_dias()
                Exit Sub
            End If
        End If

        consulta_cumpleaños() '09/08/19

        lblMensaje0.text = ""

        Dim Fecha_Hora_Ini As DateTime
        Dim Fecha_Hora_Fin As DateTime
        Dim Param, rptta As Integer
        Dim tipo_motivo, tipo_actividad, pais As Integer '27/07/19
        Dim actividad, institucion, ciudad As String '27/07/19

        Param = Me.ddlTipoSolicitud.SelectedValue

        If Param = 4 Then 'Permiso
            'Dim vHoraInicio As DateTime = ddlHoraInicio.Text
            'Dim vHoraFin As DateTime = ddlHoraFin.Text
            Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + ddlHoraInicio.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + ddlHoraFin.Text)

            If chkCumple.checked = True Then '---------------------  Se Inserta desde Acá 12/04/19  ----------------------------------------------------------
                '--------- 20/05/2019 Se añade Validación si el Colaborador es Admninistrativo o Docente a Tiempo Completo -----------------------------------
                Dim tipoPer As Integer
                Dim dt2 As New Data.DataTable
                Dim obj2 As New ClsConectarDatos
                obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj2.AbrirConexion()
                dt2 = obj2.TraerDataTable("spPla_ConsultarPersonal", "CP", CInt(Session("id_per")))

                tipoPer = dt2.Rows(0).Item("codigo_Tpe")

                If (tipoPer = 1 Or tipoPer = 2 Or tipoPer = 3) And dt2.Rows(0).Item("codigo_Ded") = 1 Then ' Dedicacion=1(TiempoCompleto), TipoPersonal=1,2,3(Docente/Administrativo/Servicios)
                    '------------------ hasta aquí primer parte 20/05/2019  -----------------------------------------------------------------------------------           
                    Dim hora_ingreso, hora_salida As String '09/08/19
                    Dim dt1 As New Data.DataTable
                    Dim obj1 As New ClsConectarDatos
                    obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj1.AbrirConexion()
                    dt1 = obj1.TraerDataTable("PER_ConsultarCumpleDia", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                    obj1.CerrarConexion() '09/08/19
                    If dt1.Rows.Count > 0 Then
                        'si la fecha del cumple es un dia laboral para el colaborador
                        Dim ObjCnx3 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                        Dim dt3 As New Data.DataTable
                        dt3 = ObjCnx3.TraerDataTable("ConsultaHoraIngresoSalida", CInt(Session("id_per")), CDate(Me.txtDesde.Text))                        

                        hora_ingreso = FormatDateTime(dt3.Rows(0).Item("hora_ingreso"), DateFormat.ShortTime)
                        hora_salida = FormatDateTime(dt3.Rows(0).Item("hora_salida"), DateFormat.ShortTime) 'Se añaden las horas de ingreso y salida

                        Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + hora_ingreso) 'Por Cumple se considera Día Completo
                        Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + hora_salida) 'Por Cumple se considera Día Completo
                    Else
                        '---------09/08 Se añade segundo filtro--------------------------------------------------------------------
                        Dim dt4 As New Data.DataTable
                        Dim obj4 As New ClsConectarDatos
                        obj4.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj4.AbrirConexion()
                        dt4 = obj4.TraerDataTable("PER_ConsultaDiaLaboral", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                        obj4.CerrarConexion()
                        If dt4.Rows.Count > 0 Then
                            'si la fecha del cumple es un dia laboral para el colaborador
                            hora_ingreso = FormatDateTime(dt4.Rows(0).Item("hora_ingreso"), DateFormat.ShortTime)
                            hora_salida = FormatDateTime(dt4.Rows(0).Item("hora_salida"), DateFormat.ShortTime) 'Se añaden las horas de ingreso y salida

                            Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + hora_ingreso) 'Por Cumple se considera Día Completo
                            Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + hora_salida) 'Por Cumple se considera Día Completo
                            '---------09/08 hasta acá------------------------------------------------------------------------------
                        Else
                            'Dim vMensaje As String = ""
                            vMensaje = "**Aviso : En el día de su cumpleaños No cuenta con Horario Laboral. No podrá acceder a este beneficio en el presente año."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.btnGuardar.Visible = False '25-06
                            Me.btnGuardarEnviar.Visible = False '25-06
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: Este día no es laborable para Usted."
                            Exit Sub
                        End If


                    End If

                    '----------------- desde acá 2da parte 20/05/2019 -------------------------------------------------------------------------------------------
                Else
                    obj2.CerrarConexion()
                    vMensaje = "**Aviso : Usted No cuenta con Horario Laboral a Tiempo Completo o no es Docente, Administrativo o de Servicios; por lo tanto No puede solicitar este tipo de permiso."
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    desactiva_controles() 'Se añadió
                    Me.celdaGrid.Visible = True
                    Me.celdaGrid.InnerHtml = "**Aviso : Usted no goza de este beneficio"
                    Exit Sub
                End If
                '--------------------- hasta acá la 2da parte 20/05/2019 ---------------------------------------------------------------------------------------

            End If '--------------------------------Hasta Acá--------------------------------------------------------------------------------------------

        Else '------------------Si es otro tipo de Solicitud diferente de Permisos x Horas------------------------------------------------------------------

            valida_cumple()
            Dim dia, mes As Integer
            dia = Day(fecha_cumple)
            mes = Month(fecha_cumple)

            If (Me.txtDesde.Text = Me.txtHasta.Text) Then 'Si es Un solo Dia solicitado

                If dia = Day(CDate(Me.txtDesde.Text)) And mes = Month(CDate(Me.txtDesde.Text)) Then 'Si ese día es el de Cumpleaños
                    Dim dt1 As New Data.DataTable
                    Dim obj1 As New ClsConectarDatos
                    obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj1.AbrirConexion()
                    dt1 = obj1.TraerDataTable("PER_ConsultarCumpleDia", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                    obj1.CerrarConexion() '09/08/19
                    '------------------Si es el día de su Cumpleaños-----------------------------------------------------------------------------------------
                    If dt1.Rows.Count > 0 Then
                        vMensaje = "**Aviso : El permiso por Cumpleaños es del Tipo: Permisos por Horas. Por favor seleccione correctamente."
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                        desactiva_controles() 'Se añadió
                        Me.celdaGrid.Visible = True
                        Me.celdaGrid.InnerHtml = "*Aviso: El permiso por Cumpleaños es Por Horas."
                        Exit Sub
                    Else
                        '---------09/08 Se añade segundo filtro--------------------------------------------------------------------
                        Dim dt4 As New Data.DataTable
                        Dim obj4 As New ClsConectarDatos
                        obj4.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj4.AbrirConexion()
                        dt4 = obj4.TraerDataTable("PER_ConsultaDiaLaboral", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                        obj4.CerrarConexion()
                        If dt4.Rows.Count > 0 Then
                            vMensaje = "**Aviso : El permiso por Cumpleaños es del Tipo: Permisos por Horas. Por favor seleccione correctamente."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: El permiso por Cumpleaños es Por Horas."
                            Exit Sub
                            '---------09/08 hasta acá------------------------------------------------------------------------------
                        Else
                            vMensaje = "**Aviso : En el día de su cumpleaños No cuenta con Horario Laboral. No podrá acceder a este beneficio en el presente año."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.btnGuardar.Visible = False '25-06
                            Me.btnGuardarEnviar.Visible = False '25-06
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: Este día no es laborable para Usted."
                            Exit Sub
                        End If
                    End If

                End If

            End If

                Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
                Fecha_Hora_Fin = CDate(Me.txtHasta.Text)
        End If

        If Param = 2 Then 'Licencias C/Goce
            If ddlTipoLicencia.SelectedValue = 3 Then 'Si es por Capacitación
                'Response.Write("Capacitacion")
                'If ddlMotivoLicencia.SelectedValue = 2 Then 'Si es Experiencias de Intercambio 
                '    If ddlTipoActividad.Text = "Seleccione.." Then 'y no selecciona tipoActividad
                '        vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE ACTIVIDAD a realizar en el evento de capacitación"
                '        Dim myscript As String = "alert('" & vMensaje & "')"
                '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                '        Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                '        Exit Sub
                '    Else
                '        tipo_actividad = ddlTipoActividad.SelectedValue
                '    End If
                'Else 'Si es Movilidad docente o Por Estudios, el tipo_actividad es vacío
                '    tipo_actividad = 0
                'End If
                If ddlTipoActividad.Text = "Seleccione.." Then 'y no selecciona tipoActividad
                    vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE ACTIVIDAD a realizar en el evento de capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    tipo_actividad = ddlTipoActividad.SelectedValue
                End If

                If ddlMotivoLicencia.Text = "Seleccione.." Then
                    vMensaje = "* ATENCIÓN: Debe seleccionar el MOTIVO de la actividad que realizará"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    tipo_motivo = ddlMotivoLicencia.SelectedValue
                End If

                'If ddlDetalleLicencia.Text = "Seleccione.." Then
                '    vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE EVENTO de la actividad que realizará"
                '    Dim myscript As String = "alert('" & vMensaje & "')"
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                '    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                '    Exit Sub
                'Else
                '    detalle_motivo = ddlDetalleLicencia.SelectedValue
                'End If

                If Trim(txtCiudad.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar la CIUDAD donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If Trim(txtInstitucion.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar la INSTITUCION donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If Trim(txtActividad.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar el NOMBRE DE LA ACTIVIDAD que se realizará"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If ddlPais.Text = "Seleccione.." Then
                    vMensaje = "* ATENCIÓN: Debe indicar el PAIS donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    pais = ddlPais.SelectedValue
                End If

                actividad = Trim(txtActividad.Text)
                institucion = Trim(txtInstitucion.Text)
                ciudad = Trim(txtCiudad.Text)
            End If
        End If

        If Me.ddlTipoSolicitud.SelectedIndex < 0 Then
            vMensaje = "* ATENCIÓN: Debe seleccionar un Tipo de Solicitud de Trámite"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
            Exit Sub
        End If

        If Param <> 3 Then '15/08 Si motivo está en blanco y es diferente de Vacaciones
            'Response.Write(ddlTipoSolicitud.SelectedValue)
            If Trim(txtmotivo.Text) = "" Then
                vMensaje = "* ATENCIÓN: Debe indicar el Motivo de la Solicitud de Trámite"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                Exit Sub
            End If
        End If

        Try
            'Dim obj As New ClsConectarDatos
            'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable
            'obj.AbrirConexion()

            If Me.lblEstado.Text = "Nuevo" Then
                'Dim dt As New Data.DataTable

                dt = ObjCnx.TraerDataTable("ComparaSolicitudTramite", CInt(Session("id_per")), Fecha_Hora_Ini, Fecha_Hora_Fin, Param, 0)
                If dt.Rows.Count > 0 Then
                    vMensaje = "*Aviso : Ud. ya tiene una o más Solicitudes de Trámite en el rango de Fechas solicitadas"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If
                'obj.Ejecutar("GuardarSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, CInt(Session("id_per")), "G")
                'Se considera el cod_ST a crear
                If ddlTipoSolicitud.SelectedValue = 2 Then
                    cod_ST = ObjCnx.TraerValor("GuardarSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, CInt(Session("id_per")), Me.ddlTipoLicencia.SelectedValue, "G") '17/09 se añade ddlTipoLicencia
                Else
                    cod_ST = ObjCnx.TraerValor("GuardarSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, CInt(Session("id_per")), 0, "G") '17/09 se añade ddlTipoLicencia

                End If

                If cod_ST = 0 Then
                    respuesta = "NG"
                    Me.lblMensaje0.Text = "*Nota : No se Pudo Guardar la Solicitud de Trámite"

                ElseIf cod_ST > 0 Then

                    If Param = 2 Then 'Licencia C/Goce y Capacitaciones '16/08

                        If ddlTipoLicencia.SelectedValue = 3 Then
                            ObjCnx.Ejecutar("GuardarLicenciaCapacitacion", cod_ST, actividad, pais, ciudad, institucion, tipo_motivo, tipo_actividad)
                        End If
                    End If

                    If Me.files.HasFile Then
                        GuardarArchivos(cod_ST)
                        ObjCnx.Ejecutar("InsertaArchivosSolicitudTramite", cod_ST, CInt(Session("id_per")))
                    End If
                    respuesta = "G"
                    '....Se añadió....
                    If tamano = 1 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque han excedido el peso límite permitido. Se aceptan archivos de hasta 2MB."
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha Guardado pero los archivos No se adjuntaron."
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    End If
                    If extension = 4 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque no son del tipo permitido. Se aceptan archivos de los tipos .jpg, .pdf y .rar"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha Guardado pero los archivos No se adjuntaron."
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    End If
                    '....hasta aquí....
                End If
                registrar_filtros()
                Response.Redirect("SolicitudesTramite.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&respuesta=" & respuesta)

            Else
                cod_ST = Request.QueryString("cod")
                'Dim dt As New Data.DataTable
                dt = ObjCnx.TraerDataTable("ComparaSolicitudTramite", CInt(Session("id_per")), Fecha_Hora_Ini, Fecha_Hora_Fin, Param, cod_ST)
                If dt.Rows.Count > 0 Then
                    vMensaje = "*Aviso : Existen Solicitudes de Trámite en las Fechas/Horas solicitadas"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If ddlTipoSolicitud.SelectedValue = 2 Then
                    rptta = ObjCnx.TraerValor("ActualizaSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, Me.ddlTipoLicencia.SelectedValue, "G", cod_ST) '03/10
                Else
                    rptta = ObjCnx.TraerValor("ActualizaSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, 0, "G", cod_ST) '03/10
                End If

                If rptta = 1 Then

                    If Param = 2 Then 'Si es Licencia C/Goce y es Capacitación '16/08
                        If ddlTipoLicencia.SelectedValue = 3 Then
                            ObjCnx.Ejecutar("GuardarLicenciaCapacitacion", cod_ST, actividad, pais, ciudad, institucion, tipo_motivo, tipo_actividad)
                        End If
                    End If

                    If Me.files.HasFile Then
                        GuardarArchivos(cod_ST)
                        ObjCnx.Ejecutar("InsertaArchivosSolicitudTramite", cod_ST, CInt(Session("id_per"))) 'Inserta en la tabla ArchivosSolictudTramite
                    End If
                    respuesta = "M"
                    '....Se añadió....
                    If tamano = 1 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque han excedido el peso límite permitido. Se aceptan archivos de hasta 2MB."
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos()  'Se añadió
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha modificado pero los archivos No se adjuntaron."
                        Exit Sub
                    End If
                    If extension = 4 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque no son del tipo permitido. Se aceptan archivos de los tipos .jpg, .pdf y .rar"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos() 'Se añadió
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha modificado pero los archivos No se adjuntaron."
                        Exit Sub
                    End If
                    '....hasta aquí....
                Else
                    respuesta = "NM"
                    Me.lblMensaje0.Text = "*Nota : No se Pudo Actualizar la Solicitud de Trámite"
                End If

                registrar_filtros()
                Response.Redirect("SolicitudesTramite.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&respuesta=" & respuesta)

            End If
            'obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        'ConsultarSolicitudTramite()

    End Sub

    'Protected Sub btnConfirmarGuardarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarGuardarNO.Click
    '    Me.divFormulario.Visible = True
    '    Me.divConfirmaGuardar.Visible = False
    'End Sub

    Protected Sub btnConfirmarGuardarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarGuardarSI.Click

        'Me.btnConfirmarGuardarSI.enabled = False 'Se añadió para evitar clickear varias veces//Se comenta porque no funciona en rapidez
        Me.divFormulario.Visible = True
        parteGuardar()
        'Me.divConfirmaGuadar.Visible = False

    End Sub

    'Protected Sub btnGuardarEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarEnviar.Click
    '    Me.divFormulario.Visible = False
    '    Me.divConfirmaGuadarEnviar.Visible = True
    'End Sub

    Private Sub parteGuardarEnviar()

        If guarda_envia = 1 Then
            Exit Sub
        End If

        valida_sesion()
        Dim vMensaje As String = ""

        'Se añade 17-01-2019 para validar que los días de vacaciones pedido no sea mayor a los pendientes
        If Me.ddlTipoSolicitud.SelectedValue = 3 Then
            'Me.txtmotivo.Text = "" '09/08/19  
            'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)        
            If Val(lblNum_dias.Text) - Val(lblDPendi.Text) >= 1 Then
                vMensaje = "*Aviso : No dispone de esta cantidad de días (" & Val(lblNum_dias.Text) & ") para gozar como Vacaciones. Consulte con el Área de Personal."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.btnConfirmarGuardarSI.enabled = True 'Se añadió
                'Me.txtDesde.Text = FormatDateTime(lblDesde.text, DateFormat.ShortDate)
                'Me.txtHasta.Text = FormatDateTime(lblHasta.text, DateFormat.ShortDate)
                valida_mes_registro()
                saldo_dias()
                'Response.Write(Val(lblNum_dias.Text))
                'Response.Write(Val(lblDPendi.Text))
                Exit Sub
            End If
        End If

        lblMensaje0.text = ""

        consulta_cumpleaños() '09/08/19

        Dim Fecha_Hora_Ini As DateTime
        Dim Fecha_Hora_Fin As DateTime
        Dim Param, rptta As Integer
        Dim tipo_motivo, tipo_actividad, pais As Integer '02/08/19
        Dim actividad, institucion, ciudad As String '02/08/19

        Param = Me.ddlTipoSolicitud.SelectedValue

        If Param = 4 Then 'Permisos
            'Dim vHoraInicio As DateTime = ddlHoraInicio.Text
            'Dim vHoraFin As DateTime = ddlHoraFin.Text
            Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + ddlHoraInicio.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + ddlHoraFin.Text)

            If chkCumple.checked = True Then '---------------------- Se Inserta desde Acá 12/04/19  ------------------
                '--------- 20/05/2019 Se añade Validación si el Colaborador es Admninistrativo o Docente a Tiempo Completo -----------------------------------
                Dim tipoPer As Integer
                Dim dt2 As New Data.DataTable
                Dim obj2 As New ClsConectarDatos
                obj2.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj2.AbrirConexion()
                dt2 = obj2.TraerDataTable("spPla_ConsultarPersonal", "CP", CInt(Session("id_per")))

                tipoPer = dt2.Rows(0).Item("codigo_Tpe")

                If (tipoPer = 1 Or tipoPer = 2 Or tipoPer = 3) And dt2.Rows(0).Item("codigo_Ded") = 1 Then ' Dedicacion=1(TiempoCompleto), TipoPersonal=1,2,3(Docente/Administrativo/Servicios) '18/02/2020
                    '------------------ hasta aquí primer parte 20/05/2019  -----------------------------------------------------------------------------------           
                    Dim hora_ingreso, hora_salida As String '09/08/19
                    Dim dt1 As New Data.DataTable
                    Dim obj1 As New ClsConectarDatos
                    obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj1.AbrirConexion()
                    dt1 = obj1.TraerDataTable("PER_ConsultarCumpleDia", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                    obj1.CerrarConexion()
                    If dt1.Rows.Count > 0 Then
                        'si la fecha del cumple es un dia laboral para el colaborador
                        Dim ObjCnx3 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                        Dim dt3 As New Data.DataTable
                        dt3 = ObjCnx3.TraerDataTable("ConsultaHoraIngresoSalida", CInt(Session("id_per")), CDate(Me.txtDesde.Text))

                        hora_ingreso = FormatDateTime(dt3.Rows(0).Item("hora_ingreso"), DateFormat.ShortTime)
                        hora_salida = FormatDateTime(dt3.Rows(0).Item("hora_salida"), DateFormat.ShortTime) 'Se añaden las horas de ingreso y salida

                        Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + hora_ingreso) 'Por Cumple se considera Día Completo
                        Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + hora_salida) 'Por Cumple se considera Día Completo
                    Else
                        '---------09/08 Se añade segundo filtro--------------------------------------------------------------------
                        Dim dt4 As New Data.DataTable
                        Dim obj4 As New ClsConectarDatos
                        obj4.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj4.AbrirConexion()
                        dt4 = obj4.TraerDataTable("PER_ConsultaDiaLaboral", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                        obj4.CerrarConexion()
                        If dt4.Rows.Count > 0 Then
                            'si la fecha del cumple es un dia laboral para el colaborador
                            hora_ingreso = FormatDateTime(dt4.Rows(0).Item("hora_ingreso"), DateFormat.ShortTime)
                            hora_salida = FormatDateTime(dt4.Rows(0).Item("hora_salida"), DateFormat.ShortTime) 'Se añaden las horas de ingreso y salida

                            Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + hora_ingreso) 'Por Cumple se considera Día Completo
                            Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + hora_salida) 'Por Cumple se considera Día Completo
                            '---------09/08 hasta acá------------------------------------------------------------------------------
                        Else
                            'Dim vMensaje As String = ""
                            vMensaje = "**Aviso : En el día de su cumpleaños No cuenta con Horario Laboral. No podrá acceder a este beneficio en el presente año."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.btnGuardar.Visible = False '25-06
                            Me.btnGuardarEnviar.Visible = False '25-06
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: Este día no es laborable para Usted."
                            Exit Sub
                        End If
                    End If

                    '----------------- desde acá 2da parte 20/05/2019 -------------------------------------------------------------------------------------------
                Else
                    obj2.CerrarConexion()
                    vMensaje = "**Aviso : Usted No cuenta con Horario Laboral a Tiempo Completo o no es Docente, Administrativo o de Servicios; por lo tanto No puede solicitar este tipo de permiso." ' 18/02/2020
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    desactiva_controles() 'Se añadió
                    Me.celdaGrid.Visible = True
                    Me.celdaGrid.InnerHtml = "**Aviso : Usted no goza de este beneficio"
                    Exit Sub
                End If
                '--------------------- hasta acá la 2da parte 20/05/2019 ---------------------------------------------------------------------------------------

            End If '--------------------------------Hasta Acá------------------------------------------------------

        Else
            Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text)
        End If

        If Param = 2 Then 'Licencias C/Goce
            If ddlTipoLicencia.SelectedValue = 3 Then 'Si es por Capacitación

                'If ddlMotivoLicencia.SelectedValue = 2 Then 'Si es Experiencias de Intercambio 
                '    If ddlTipoActividad.Text = "Seleccione.." Then 'y no selecciona tipoActividad
                '        vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE ACTIVIDAD a realizar en el evento de capacitación"
                '        Dim myscript As String = "alert('" & vMensaje & "')"
                '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                '        Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                '        Exit Sub
                '    Else
                '        tipo_actividad = ddlTipoActividad.SelectedValue
                '    End If
                'Else 'Si es Movilidad docente o Por Estudios, el tipo_actividad es vacío
                '    tipo_actividad = 0
                'End If
                If ddlTipoActividad.Text = "Seleccione.." Then 'y no selecciona tipoActividad
                    vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE ACTIVIDAD a realizar en el evento de capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    tipo_actividad = ddlTipoActividad.SelectedValue
                End If

                If ddlMotivoLicencia.Text = "Seleccione.." Then
                    vMensaje = "* ATENCIÓN: Debe seleccionar el MOTIVO de la actividad que realizará"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    tipo_motivo = ddlMotivoLicencia.SelectedValue
                End If

                'If ddlDetalleLicencia.Text = "Seleccione.." Then
                '    vMensaje = "* ATENCIÓN: Debe seleccionar el TIPO DE EVENTO de la actividad que realizará"
                '    Dim myscript As String = "alert('" & vMensaje & "')"
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                '    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                '    Exit Sub
                'Else
                '    detalle_motivo = ddlDetalleLicencia.SelectedValue
                'End If

                If Trim(txtCiudad.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar la CIUDAD donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If Trim(txtInstitucion.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar la INSTITUCION donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If Trim(txtActividad.Text) = "" Then
                    vMensaje = "* ATENCIÓN: Debe indicar el NOMBRE DE LA ACTIVIDAD que se realizará"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                If ddlPais.Text = "Seleccione.." Then
                    vMensaje = "* ATENCIÓN: Debe indicar el PAIS donde se llevará a cabo la capacitación"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                Else
                    pais = ddlPais.SelectedValue
                End If

                actividad = Trim(txtActividad.Text)
                institucion = Trim(txtInstitucion.Text)
                ciudad = Trim(txtCiudad.Text)

            End If
        End If

        If Me.ddlTipoSolicitud.SelectedIndex < 0 Then
            vMensaje = "*Aviso : Atención: Debe seleccionar un Tipo de Solicitud de Trámite"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
            Exit Sub
        End If

        If Param <> 3 Then  '15/08
            If Trim(txtmotivo.Text) = "" Then
                vMensaje = "* ATENCIÓN: Debe indicar el Motivo de la Solicitud de Trámite"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                Exit Sub
            End If
        End If

        Try

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable

            cod_ST = Request.QueryString("cod")

            If cod_ST = 0 Then '----------------------Para Nuevas Solicitudes---------------------------------------------------------------
                dt = ObjCnx.TraerDataTable("ComparaSolicitudTramite", CInt(Session("id_per")), Fecha_Hora_Ini, Fecha_Hora_Fin, Param, 0)
                If dt.Rows.Count > 0 Then
                    vMensaje = "*Aviso : Ud. ya tiene una o más Solicitudes de Trámite en el rango de Fechas solicitadas"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                'Se considera el cod_ST a crear
                If ddlTipoSolicitud.SelectedValue = 2 Then
                    cod_ST = ObjCnx.TraerValor("GuardarSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, CInt(Session("id_per")), Me.ddlTipoLicencia.SelectedValue, "G")
                Else
                    cod_ST = ObjCnx.TraerValor("GuardarSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, CInt(Session("id_per")), 0, "G") '17/09 se añade ddlTipoLicencia
                End If


                If cod_ST > 0 Then
                    '02/08 Para nuevas solicitudes                
                    If Param = 2 Then 'Licencia C/Goce y Capacitaciones '16/08                        
                        If ddlTipoLicencia.SelectedValue = 3 Then
                            ObjCnx.Ejecutar("GuardarLicenciaCapacitacion", cod_ST, actividad, pais, ciudad, institucion, tipo_motivo, tipo_actividad)
                        End If
                    End If
                    If Me.files.HasFile Then
                        GuardarArchivos(cod_ST)
                        ObjCnx.Ejecutar("InsertaArchivosSolicitudTramite", cod_ST, CInt(Session("id_per")))
                    End If
                    respuesta = "G"
                    '....Se añadió....
                    If tamano = 1 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque han excedido el peso límite permitido. Se aceptan archivos de hasta 2MB."
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite No se envió porque los archivos no se adjuntaron." 'Se añadió. No se envió porque no adjuntó
                        Exit Sub
                    End If
                    If extension = 4 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque no son del tipo permitido. Se aceptan archivos de los tipos .jpg, .pdf y .rar"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite No se envió porque los archivos no se adjuntaron."
                        Exit Sub
                    End If
                    '....hasta aquí....no se envía si entra acá

                    rptta = ObjCnx.TraerValor("CreaEvaluacionSolicitudTramite", CInt(Session("id_per")), cod_ST, "D") 'Se agregó CInt(session)

                    If rptta = 0 Then
                        vMensaje = "*Aviso : No existe un Director asignado a su Centro de Costo. Consultar con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    ElseIf rptta = -1 Then
                        vMensaje = "*Aviso : Existe más de un Director asignado a su Centro de Costo. Por favor Consultar con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    ElseIf rptta = -2 Then  'Este aviso es solo para Director de Área que solicitan un trámite
                        vMensaje = "Aviso : Su dependencia superior no tiene un Director asignado. Por favor Consulte con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    Else
                        'Si entra en esta parte ya se creó el registro de Director en tabla: EvaluacionSolicitudTramite
                        ObjCnx.Ejecutar("EnviarSolicitudTramite", cod_ST) 'Procedimiento para Enviar al Director  
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha Guardado y Enviado Correctamente"
                        respuesta = "GE"

                        If lblCorreo.Text = "N" Then 'se añade 22/04/19
                            'Se añadió Enviar Correo
                            EnviaCorreo(rptta, "E")
                            lblCorreo.Text = "S" 'Se coloca 'S' de Sí
                        End If

                    End If
                Else
                    respuesta = "NE"
                    Me.lblMensaje0.Text = "*Nota : No se Pudo Guardar ni Enviar la Solicitud de Trámite"
                    'Si no guarda la solicitud por error al guardar, envía este mensaje a la página anterior como respuesta
                End If

            Else '------------------Para Solicitudes que ya Existen----------------------------------------------------------------
                guarda_envia = 1 'Se añade
                'Response.Write(guarda_envia)
                'Me.btnConfirmarGuardarSI.enabled = False 'Se añadió para evitar clickear varias veces//Se anula porque igual no se desactiva

                dt = ObjCnx.TraerDataTable("ComparaSolicitudTramite", CInt(Session("id_per")), Fecha_Hora_Ini, Fecha_Hora_Fin, Param, cod_ST)
                If dt.Rows.Count > 0 Then
                    vMensaje = "*Aviso : Existen Solicitudes de Trámite en las Fechas/Horas solicitadas"
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                    Exit Sub
                End If

                Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

                If ddlTipoSolicitud.SelectedValue = 2 Then
                    rptta = ObjCnx1.TraerValor("ActualizaSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, Me.ddlTipoLicencia.SelectedValue, "G", cod_ST) '03/10
                Else
                    rptta = ObjCnx1.TraerValor("ActualizaSolicitudTramite", Me.ddlTipoSolicitud.SelectedValue, Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtmotivo.Text), Me.ddlPrioridad.SelectedValue, 0, "G", cod_ST) '03/10
                End If

                If rptta = 1 Then

                    If Param = 2 Then 'Licencia C/Goce y Capacitaciones '16/08
                        Response.Write(Param)
                        If ddlTipoLicencia.SelectedValue = 3 Then
                            ObjCnx.Ejecutar("GuardarLicenciaCapacitacion", cod_ST, actividad, pais, ciudad, institucion, tipo_motivo, tipo_actividad)
                        End If
                    End If

                    If Me.files.HasFile Then
                        GuardarArchivos(cod_ST)
                        ObjCnx1.Ejecutar("InsertaArchivosSolicitudTramite", cod_ST, CInt(Session("id_per"))) 'Inserta en la tabla ArchivosSolictudTramite
                    End If

                    '....Se añadió....
                    If tamano = 1 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque han excedido el peso límite permitido. Se aceptan archivos de hasta 2MB."
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos() 'Se añadió
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Modificado la Solicitud de Trámite pero No se ha Enviado porque los archivos no se adjuntaron." 'Se añadió. No se envió porque no adjuntó
                        Exit Sub
                    End If
                    If extension = 4 Then
                        vMensaje = "*Aviso! Los archivos no se han podido guardar porque no son del tipo permitido. Se aceptan archivos de los tipos .jpg, .pdf y .rar"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        desactiva_controles() 'Se añadió
                        ConsultarAdjuntos() 'Se añadió
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Modificado la Solicitud de Trámite pero No se ha Enviado porque los archivos no se adjuntaron."
                        Exit Sub
                    End If
                    '....hasta aquí....

                    rptta = ObjCnx.TraerValor("CreaEvaluacionSolicitudTramite", CInt(Session("id_per")), cod_ST, "D") 'Se agregó CInt(session)

                    If rptta = 0 Then
                        vMensaje = "*Aviso : No existe un Director asignado a su Centro de Costo. Consultar con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    ElseIf rptta = -1 Then
                        vMensaje = "*Aviso : Existe más de un Director asignado a su Centro de Costo. Por favor Consultar con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    ElseIf rptta = -2 Then  'Este aviso es solo para Director de Área que solicitan un trámite
                        vMensaje = "*Aviso : Su dependencia superior no tiene un Director asignado. Por favor Consulte con el Área de Personal"
                        Dim myscript As String = "alert('" & vMensaje & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Me.lblMensaje0.Text = "** AVISO :  Se ha Guardado su Solicitud de Trámite pero No se ha Enviado"
                        respuesta = "NE"
                        Me.btnCancelar.Enabled = True
                        'ConsultarSolicitudTramite()
                        desactiva_controles()
                        ConsultarAdjuntos() 'Se añadió
                        Exit Sub
                    Else
                        'Si entra en esta parte ya se creó el registro de Director en tabla: EvaluacionSolicitudTramite
                        ObjCnx.Ejecutar("EnviarSolicitudTramite", cod_ST) 'Procedimiento para Enviar al Director  
                        Me.lblMensaje0.Text = "*Nota : La Solicitud de Trámite se ha Guardado y Enviado Correctamente"
                        respuesta = "GE"

                        If lblCorreo.Text = "N" Then 'se añade 22/04/19
                            'Se añadió Enviar Correo
                            EnviaCorreo(rptta, "E")
                            lblCorreo.Text = "S" 'Se coloca 'S' de Sí
                        End If

                    End If
                Else
                    respuesta = "NE"
                    Me.lblMensaje0.Text = "*Nota : No se Pudo Guardar ni Enviar la Solicitud de Trámite"
                    'Si no guarda la solicitud por error al actualizar, envía este mensaje a la página anterior como respuesta
                End If
            End If
            '-------Enviar a Director-------------------------------------------------------------------------------
            desactiva_controles()
            registrar_filtros()
            Response.Redirect("SolicitudesTramite.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&respuesta=" & respuesta)

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        'desactiva_controles()

    End Sub

    'Protected Sub btnConfirmarGuardarEnviarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarGuardarEnviarNO.Click
    '    Me.divFormulario.Visible = True
    '    Me.divConfirmaGuardarEnviar.Visible = False
    'End Sub
    Protected Sub btnConfirmarGuardarEnviarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarGuardarEnviarSI.Click

        'Me.btnConfirmarGuardarEnviarSI.enabled = False 'Se añadió para evitar clickear varias veces PERO NO SE OCULTA RAPIDAMENTE// Se comenta
        'Me.divConfirmaGuadarEnviar.Visible = False

        Me.divFormulario.Visible = True 'Se cambió el orden, al final a ver q pasa...
        parteGuardarEnviar()

    End Sub

    Public Sub valida_TipoSolicitud()

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones

            Me.lblSaldoDias.visible = True
            Me.lblSPend.visible = True
            lblMotivo.visible = False
            txtMotivo.visible = False

            lblDiasPend.visible = True
            lblDPendi.visible = True
            lblpendientes.visible = True

            lblAdjuntos.visible = False
            files.visible = False
            btnSubirAdjunto.visible = False
        Else
            Me.lblSaldoDias.visible = False
            Me.lblSPend.visible = False
            lblMotivo.visible = True
            txtMotivo.visible = True

            lblDiasPend.visible = False
            lblDPendi.visible = False
            lblpendientes.visible = False

            lblAdjuntos.visible = True
            files.visible = True

            If Me.lblEstado.Text = "Nuevo" Or Me.lblEstado.Text = "Generado" Then 'Se añadió
                btnSubirAdjunto.visible = False
            Else
                btnSubirAdjunto.visible = True
            End If

        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Permisos
            Me.lblHoraFin.Visible = True
            Me.lblHoraInicio.Visible = True
            Me.lblHoras.Visible = True
            Me.ddlHoraInicio.Visible = True
            Me.ddlHoraFin.Visible = True
            Me.lblTotalHoras.Visible = True

            Me.txtDesde.Enabled = True
            Me.txtHasta.Text = Me.txtDesde.Text
            Me.txtHasta.Enabled = False

            If chkCumple.checked = True Then 'se añade 11-04
                Me.lblHoraFin.Visible = False
                Me.lblHoraInicio.Visible = False
                Me.lblHoras.Visible = False
                Me.ddlHoraInicio.Visible = False
                Me.ddlHoraFin.Visible = False
                Me.lblTotalHoras.Visible = False
                lblAdjuntos.visible = False
                files.Visible = False
                Me.btnSubirAdjunto.Visible = False '13/08
                Me.txtmotivo.enabled = False
                Me.btnGuardar.visible = True '25-06
                Me.btnGuardarEnviar.Visible = True '25-06
            End If

            Me.lblNumDias.visible = False
            Me.lblNum_dias.Text = "0"
            Me.lblNum_dias.visible = False
        Else
            Me.lblTotalHoras.Visible = False
            Me.lblHoraInicio.Visible = False
            Me.lblHoraFin.Visible = False
            Me.lblHoras.Visible = False
            Me.ddlHoraInicio.Visible = False
            Me.ddlHoraFin.Visible = False

            Me.txtDesde.Enabled = True
            Me.txtHasta.Enabled = True

            Me.lblNumDias.visible = True 'Se añadió
            Me.lblNum_dias.visible = True 'Se añadió

        End If

    End Sub

    Protected Sub ddlTipoSolicitud_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.TextChanged

        If Me.ddlTipoSolicitud.SelectedValue <> 4 Then
            calcula_dias()
            chkCumple.visible = False 'se añade 10-04-19
            ftchkCumple.visible = False
            lblCumple.visible = False
            If chkCumple.checked = True Then
                'txtmotivo.text = ""
                lblchkCump.text = "1"
            End If
        Else
            chkCumple.visible = True 'se añade 10-04-19
            ftchkCumple.Visible = True

            If chkCumple.checked = True Then
                valida_cumple()
                cumple_si()
            Else
                Me.txtDesde.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
                Me.txtHasta.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
                cumple_no()
            End If

            Me.txtmotivo.Text = "" '16/08

        End If

        consulta_cumpleaños() '09/08/19

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones
            valida_mes_registro() '05-07 Se añade
            ConsultaVacaciones()
            oculta_dias() '05-07
            Me.txtmotivo.Text = "" '15/08 se limpia
        End If

        lblDesde.text = Me.txtDesde.Text 'Se añadió
        lblHasta.text = Me.txtHasta.Text

        valida_aviso()
        valida_TipoSolicitud()
        lista_adjuntos()

    End Sub

    Private Sub consulta_cumpleaños()

        If txtDesde.Text = txtHasta.Text Then '-------------------Se añade: si es un solo dia que es igual a su cumpleaños
            valida_cumple()
            If Month(fecha_cumple) = Month(CDate(Me.txtDesde.Text)) And Day(fecha_cumple) = Day(CDate(Me.txtDesde.Text)) Then
                lblCumple.Visible = True
                desactiva_controles()
                Me.lblHoraInicio.Visible = False
                Me.ddlHoraInicio.Visible = False
                Me.lblHoraFin.Visible = False
                Me.ddlHoraFin.Visible = False
                Me.lblHoras.Visible = False
                Me.lblTotalHoras.Visible = False
                Me.txtmotivo.Text = "Permiso por cumpleaños / Todo el día."
                Me.txtmotivo.ForeColor = Drawing.Color.DarkRed '15/08

                txtDesde.Enabled = True
                Me.ddlTipoSolicitud.Enabled = True
                Me.ddlTipoSolicitud.SelectedValue = 4

                chkCumple.Visible = True
                chkCumple.Checked = True
                ftchkCumple.Visible = True
                lblVacaciones.Visible = False
                lblAvisoTipoSol.Visible = False '11/09
                lblMotivoDescansoMedico.Visible = False
            Else
                cumple_no() 'se añade 13/08
                chkCumple.Checked = False 'se añade 13/08
            End If
        End If '-----------------------------------------------------------------------------------------------------------

    End Sub

    Protected Sub cboPrioridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrioridad.SelectedIndexChanged
        If Me.ddlPrioridad.SelectedIndex = 1 Then 'Urgente
            Me.ddlPrioridad.ForeColor = Drawing.Color.OrangeRed
        Else 'Normal
            Me.ddlPrioridad.ForeColor = Drawing.Color.Black
        End If
        lista_adjuntos()
    End Sub

    Public Sub calcula_horas()

        Try
            'Calcula diferencia de hora y minutos
            Dim cadena As String
            Dim tot_horas As String
            Dim tot_min As Decimal
            Dim dif_hora As String
            Dim position As Integer

            cadena = Trim(Str(DateDiff("n", CDate(Me.ddlHoraInicio.Text), CDate(Me.ddlHoraFin.Text)) / 60)) 'Ej: 2.25 horas, 11 horas, 0.5 h

            If cadena = ".5" Then
                cadena = "0.5"
            End If

            If cadena.Length > 2 Then

                position = cadena.IndexOf(".")

                If Val(cadena) < 1 Then
                    tot_horas = "0"
                Else
                    tot_horas = cadena.Substring(0, position)
                End If

                dif_hora = cadena.Substring(position + 1)

                If val(dif_hora) > 0 Then
                    If dif_hora.Length >= 3 Then
                        dif_hora = dif_hora.Substring(0, 3)
                        tot_min = dif_hora * 60 * 0.001
                    ElseIf dif_hora.Length = 2 Then
                        tot_min = dif_hora * 60 * 0.01
                    ElseIf dif_hora.Length = 1 Then
                        tot_min = dif_hora * 60 * 0.1
                    End If
                    'tot_min = tot_min.ToString("0.##")
                    Me.lblTotalHoras.Text = tot_horas + " h, " + Trim(Str(tot_min)) + " m "
                End If

            ElseIf cadena.Length <= 2 Then
                Me.lblTotalHoras.Text = cadena + " h"
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.stackTrace)
        End Try

    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            'Aquí abajo desactivo la opción Eliminar archivo solo en dos casos
            If Me.lblEstado.Text = "Rechazado" Or Me.lblEstado.Text = "Aprobado Personal" Then
                Me.gvcarga.columns(4).visible = False
            End If

            Dim myLink As HyperLink = New HyperLink()
            myLink.NavigateUrl = "javascript:void(0)"
            myLink.Text = "Descargar"
            myLink.CssClass = "btn btn-xs btn-orange"
            myLink.Attributes.Add("onclick", "DescargarArchivo('" & Me.gvCarga.DataKeys(e.Row.RowIndex).Values("ID").ToString & "','" & Me.gvCarga.DataKeys(e.Row.RowIndex).Values("token").ToString & "')")

            e.Row.Cells(3).Controls.Add(myLink)
            'CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

        End If

    End Sub

    Protected Sub gvCarga_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCarga.RowDeleting

        lblMensaje0.text = ""

        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim mensaje As String

            If hddEliminar.Value = True Then
                mensaje = obj.Ejecutar("EliminaAdjuntoTramite", gvCarga.DataKeys(e.RowIndex).Values("codigo_ast"), CInt(Session("id_per")))
                'Me.divMensaje.InnerHtml = Me.divMensaje.InnerHtml + "<span style='color:green'><i class='ion-checkmark-round'></i> " + "Archivo eliminado.." + " </span><br>"
                lista_adjuntos()

                Me.celdaGrid.Visible = True
                Me.celdaGrid.InnerHtml = "*Aviso : Archivo eliminado correctamente.."
                Me.lblMensaje0.Text = ""

                obj = Nothing
                e.Cancel = True
                'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarArchivo", "alert('Eliminado: " & mensaje & "');", True)
            Else
                'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarProgramacion2", "alert('No puede eliminar el curso programado porque la fecha de programación académica ha concluido');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Sub valida_cumple()

        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("ConsultaDatosColaborador", CInt(Session("id_per")))
        fecha_cumple = dt.Rows(0).Item("fechaNacimiento_Per")
        obj.CerrarConexion()

    End Sub

    Protected Sub txtDesde_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesde.TextChanged

        'Response.Write(lblFechaIni.text)
        lblMensaje0.text = ""

        Dim vMensaje As String = ""
        If CDate(Me.txtDesde.Text) < Today Then 'Si la fecha de inicio es menor a la fecha de Hoy
            vMensaje = "*Aviso : La fecha de inicio debe ser mayor o igual a la fecha actual"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            'Si es vacaciones 
            If Me.ddlTipoSolicitud.SelectedValue = 3 And Day(Today) > limite_vac And Month(CDate(Me.txtDesde.Text)) = Month(Today) Then '01-07-19 Cambió de > 15 por limite_vac, a pedido por CCHAVEZ/ Si pide para el mismo mes y es más del día 15
                Me.txtDesde.Text = FormatDateTime(lblDesde.text, DateFormat.ShortDate)
                'Se añadió, a la fecha inicial se suma los días pendientes    
                'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)
                'Me.txtHasta.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 1)
                calcula_dias()
                saldo_dias()
                Exit Sub
            End If
            Me.txtDesde.Text = Today
            Exit Sub '20/08
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then

            Me.txtHasta.Text = Me.txtDesde.Text

            consulta_cumpleaños() 'se añade 13/08

            'valida_cumple() '----------------------------se añade 10-04-19----------------------
            'If month(fecha_cumple) = Month(CDate(Me.txtDesde.Text)) And Day(fecha_cumple) = Day(CDate(Me.txtDesde.Text)) Then
            '    lblCumple.visible = True
            '    chkCumple.visible = True
            '    chkCumple.checked = True
            '    ftchkCumple.visible = True
            '    desactiva_controles()
            '    Me.lblHoraInicio.visible = False
            '    Me.ddlHoraInicio.visible = False
            '    Me.lblHoraFin.visible = False
            '    Me.ddlHoraFin.visible = False
            '    Me.lblHoras.visible = False
            '    Me.lblTotalHoras.visible = False
            '    Me.txtmotivo.text = "Permiso por cumpleaños / Todo el día."

            '    txtDesde.enabled = True
            '    Me.ddlTipoSolicitud.enabled = True
            '    Me.ddlTipoSolicitud.selectedvalue = 4 'permisos x horas
            '    lblVacaciones.visible = False
            '    lblAvisoTipoSol.visible = False
            '    lblMotivoDescansoMedico.visible = False
            'Else
            '    cumple_no()
            '    chkCumple.Checked = False

            '    'If Me.ddlTipoSolicitud.SelectedValue = 4 Then
            '    '    chkCumple.visible = True
            '    '    ftchkCumple.visible = True
            '    'Else
            '    '    chkCumple.visible = False
            '    '    ftchkCumple.visible = False
            '    'End If
            'End If '--------------------------------------------------------------------------

        ElseIf Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Si es Vacaciones
            If Day(Today) > limite_vac And Month(CDate(Me.txtDesde.Text)) = Month(Today) Then '01-07-19 Cambió de > 15 por limite_vac, Solicitado por CCHAVEZ // Si pide para el mismo mes y es más del día 15
                Dim fecha_nueva As Date
                fecha_nueva = CDate(Me.txtDesde.Text)
                vMensaje = "*Aviso : No puede solicitar vacaciones durante este mes (desde " & fecha_nueva & ") por sobrepasar el día " & limite_vac & " establecido. Puede realizar su consulta con Dirección de Personal."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.txtDesde.Text = FormatDateTime(lblDesde.text, DateFormat.ShortDate)
                'Se añadió, a la fecha inicial se suma los días pendientes    
                'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)
                calcula_dias()
                saldo_dias()
                Exit Sub
            End If

            'If Year(CDate(txtDesde.Text)) = Year(DateTime.Now.ToString("dd/MM/yyyy")) Then
            '    If Month(CDate(txtDesde.Text)) < Month(CDate(lblFechaIni.text)) Then
            '        If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then

            '            lblDPendi.Text = STR(val(lblDPendi.text) - 30)

            '        ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then
            '            If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
            '                lblDPendi.Text = STR(val(lblDPendi.text) - 30)
            '            Else
            '                'lblDPendi.Text = STR(val(lblDPendi.text) + 30)
            '            End If
            '        ElseIf Month(CDate(txtHasta.Text)) > Month(CDate(lblFechaIni.text)) Then
            '            'lblDPendi.Text = STR(val(lblDPendi.text) + 30)
            '        End If
            '    ElseIf Month(CDate(txtDesde.Text)) = Month(CDate(lblFechaIni.text)) Then
            '        If DAY(CDate(txtDesde.Text)) < DAY(CDate(lblFechaIni.text)) Then

            '            If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then
            '                lblDPendi.Text = STR(val(lblDPendi.text) - 30)

            '            ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then
            '                If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
            '                    lblDPendi.Text = STR(val(lblDPendi.text) - 30)
            '                Else
            '                    lblDPendi.Text = STR(val(lblAuxPend.Text) + 30)
            '                End If
            '            End If
            '        End If
            '    ElseIf Month(CDate(txtDesde.Text)) > Month(CDate(lblFechaIni.text)) Then
            '        ' lblDPendi.Text = STR(val(lblDPendi.text) + 30)
            '    End If
            'End If

            'actualiza_pendiente()

            calcula_dias() 'Se añadió
        Else

            consulta_cumpleaños() '09/08/19

            calcula_dias()
            valida_cumple()

            If Me.txtDesde.Text = txtHasta.Text Then
                If Month(fecha_cumple) = Month(CDate(Me.txtDesde.Text)) And Day(fecha_cumple) = Day(CDate(Me.txtDesde.Text)) Then
                    Dim dt1 As New Data.DataTable
                    Dim obj1 As New ClsConectarDatos
                    obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj1.AbrirConexion()
                    dt1 = obj1.TraerDataTable("PER_ConsultarCumpleDia", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                    '------------------Si es el día de su Cumpleaños-------------------
                    If dt1.Rows.Count > 0 Then
                        'lblCumple.Visible = True
                        'chkCumple.Visible = True
                        'chkCumple.Checked = True
                        'ftchkCumple.Visible = True
                        'desactiva_controles()

                        'Me.txtmotivo.Text = "Permiso por cumpleaños / Todo el día."

                        'txtDesde.Enabled = True
                        'Me.ddlTipoSolicitud.Enabled = True
                        'Me.ddlTipoSolicitud.SelectedValue = 4 'permisos x horas
                        'valida_TipoSolicitud() ' SE AÑADE
                        'lblVacaciones.Visible = False
                        'lblAvisoTipoSol.Visible = False
                        'lblMotivoDescansoMedico.Visible = False

                        Me.celdaGrid.Visible = True
                        Me.celdaGrid.InnerHtml = "*Aviso: La solicitud de Cumpleaños es Permiso por Horas."
                    Else
                        '---------09/08 Se añade segundo filtro--------------------------------------------------------------------
                        Dim dt4 As New Data.DataTable
                        Dim obj4 As New ClsConectarDatos
                        obj4.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj4.AbrirConexion()
                        dt4 = obj4.TraerDataTable("PER_ConsultaDiaLaboral", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                        obj4.CerrarConexion()
                        If dt4.Rows.Count > 0 Then
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: La solicitud de Cumpleaños es Permiso por Horas."
                        Else '-------------09/0819 Hasta acá se añadió--------------------------------------------------------------
                            vMensaje = "**Aviso : En el día de su cumpleaños No cuenta con Horario Laboral. No podrá acceder a este beneficio en el presente año."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.btnGuardar.Visible = False '25-06
                            Me.btnGuardarEnviar.Visible = False '25-06
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: Este día no es laborable para Usted."
                            Exit Sub
                        End If
                    End If
                    obj1.CerrarConexion()
                End If
            End If
        End If

        If CDate(Me.txtDesde.Text) > CDate(Me.txtHasta.Text) Then
            Me.txtHasta.Text = Me.txtDesde.Text 'se añade el cambio 30-05-19
            Me.lblHasta.Text = Me.txtHasta.Text ' 30-05-19
            calcula_dias() ' 30-05-19
        End If

        lista_adjuntos()

        'valida_pendientes() 'se añade 29-03

        saldo_dias()

        lblDesde.text = Me.txtDesde.Text

    End Sub

    Protected Sub txtHasta_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHasta.TextChanged

        'Response.Write(lblFechaIni.text)

        lblMensaje0.text = ""

        Dim vMensaje As String = ""
        If CDate(Me.txtHasta.Text) < Today Then 'Si la fecha de inicio es menor a la fecha de Hoy
            vMensaje = "*Aviso : La fecha de fin debe ser mayor o igual a la fecha actual"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.txtHasta.Text = Today
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'se añade 13/08

            consulta_cumpleaños() 'se añade 13/08

        ElseIf Me.ddlTipoSolicitud.SelectedValue = 3 Then '------Si es Vacaciones---------------

            If Day(Today) > limite_vac And Month(CDate(Me.txtHasta.Text)) = Month(Today) Then 'Cambió de > 15 por limite_vac, Solicitado por CCHAVEZ // Si pide para el mismo mes y es más del día 15
                Dim fecha_nueva As Date
                fecha_nueva = CDate(Me.txtHasta.Text)
                vMensaje = "*Aviso : No puede solicitar vacaciones durante este mes (al " & fecha_nueva & ") por sobrepasar el día " & limite_vac & " establecido. Puede realizar su consulta con Dirección de Personal."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                'Me.txtDesde.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 1)
                'Se añadió, a la fecha final se suma los días pendientes    
                'Me.txtHasta.Text = FormatDateTime(CDate(Me.txtDesde.Text).AddDays(Val(lblDPendi.Text) - 1), DateFormat.ShortDate)
                Me.txtHasta.Text = FormatDateTime(lblHasta.Text, DateFormat.ShortDate)
                calcula_dias()
                saldo_dias()
                Exit Sub
            End If

            'If Year(CDate(txtDesde.Text)) = Year(DateTime.Now.ToString("dd/MM/yyyy")) Then

            '    If Month(CDate(txtDesde.Text)) < Month(CDate(lblFechaIni.text)) Then
            '        If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then

            '            lblDPendi.Text = STR(val(lblDPendi.text) - 30)

            '        ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then

            '            If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
            '                lblDPendi.Text = STR(val(lblDPendi.text) - 30)
            '            Else
            '                'lblAuxPend.Text = STR(val(lblDPendi.text) + 30)
            '            End If

            '        ElseIf Month(CDate(txtHasta.Text)) > Month(CDate(lblFechaIni.text)) Then

            '            'lblAuxPend.Text = STR(val(lblDPendi.text) + 30)
            '        End If

            '    ElseIf Month(CDate(txtDesde.Text)) = Month(CDate(lblFechaIni.text)) Then
            '        If DAY(CDate(txtDesde.Text)) < DAY(CDate(lblFechaIni.text)) Then

            '            If Month(CDate(txtHasta.Text)) < Month(CDate(lblFechaIni.text)) Then
            '                lblDPendi.Text = STR(val(lblDPendi.text) - 30)

            '            ElseIf Month(CDate(txtHasta.Text)) = Month(CDate(lblFechaIni.text)) Then

            '                If DAY(CDate(txtHasta.Text)) < DAY(CDate(lblFechaIni.text)) Then
            '                    lblDPendi.Text = STR(val(lblDPendi.text) - 30)
            '                Else
            '                    lblDPendi.Text = STR(val(lblAuxPend.Text) + 30)
            '                End If

            '            End If
            '        End If
            '    ElseIf Month(CDate(txtDesde.Text)) > Month(CDate(lblFechaIni.text)) Then
            '        'lblAuxPend.Text = STR(val(lblDPendi.text) + 30)
            '    End If
            'End If

            'actualiza_pendiente()

        Else '----------Se añade 10-05-2019--------------------Para otros tipo de Solicitud

            consulta_cumpleaños() '09/08/19

            calcula_dias() '09/08/19
            valida_cumple()

            If Me.txtDesde.Text = txtHasta.Text Then
                If Month(fecha_cumple) = Month(CDate(Me.txtDesde.Text)) And Day(fecha_cumple) = Day(CDate(Me.txtDesde.Text)) Then
                    Dim dt1 As New Data.DataTable
                    Dim obj1 As New ClsConectarDatos
                    obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj1.AbrirConexion()
                    dt1 = obj1.TraerDataTable("PER_ConsultarCumpleDia", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                    '------------------Si es el día de su Cumpleaños-------------------
                    If dt1.Rows.Count > 0 Then
                        'lblCumple.visible = True
                        'chkCumple.visible = True
                        'chkCumple.checked = True
                        'ftchkCumple.visible = True
                        'desactiva_controles()

                        'Me.txtmotivo.text = "Permiso por cumpleaños / Todo el día."

                        'txtDesde.enabled = True
                        'Me.ddlTipoSolicitud.enabled = True
                        'Me.ddlTipoSolicitud.selectedvalue = 4 'permisos x horas
                        'valida_TipoSolicitud() ' SE AÑADE
                        'lblVacaciones.visible = False
                        'lblAvisoTipoSol.visible = False
                        'lblMotivoDescansoMedico.visible = False

                        Me.celdaGrid.Visible = True
                        Me.celdaGrid.InnerHtml = "*Aviso: La solicitud de Cumpleaños es Permiso por Horas."
                    Else
                        '---------09/08 Se añade segundo filtro--------------------------------------------------------------------
                        Dim dt4 As New Data.DataTable
                        Dim obj4 As New ClsConectarDatos
                        obj4.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                        obj4.AbrirConexion()
                        dt4 = obj4.TraerDataTable("PER_ConsultaDiaLaboral", CInt(Session("id_per")), CDate(Me.txtDesde.Text))
                        obj4.CerrarConexion()
                        If dt4.Rows.Count > 0 Then
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: La solicitud de Cumpleaños es Permiso por Horas."
                        Else '-------------09/0819 Hasta acá se añadió--------------------------------------
                            vMensaje = "**Aviso : En el día de su cumpleaños No cuenta con Horario Laboral. No podrá acceder a este beneficio en el presente año."
                            Dim myscript As String = "alert('" & vMensaje & "')"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                            Me.btnConfirmarGuardarSI.Enabled = True 'Se añadió
                            desactiva_controles() 'Se añadió
                            Me.btnGuardar.Visible = False '25-06
                            Me.btnGuardarEnviar.Visible = False '25-06
                            Me.celdaGrid.Visible = True
                            Me.celdaGrid.InnerHtml = "*Aviso: Este día no es laborable para Usted."
                            Exit Sub
                        End If
                    End If
                    obj1.CerrarConexion()
                End If
            End If
            '--------------------------------------------------
        End If

        If CDate(Me.txtDesde.Text) > CDate(Me.txtHasta.Text) Then
            vMensaje = "*Aviso : La fecha final debe ser mayor o igual que la fecha de inicio."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.txtHasta.Text = FormatDateTime(lblHasta.text, DateFormat.ShortDate)
            calcula_dias()
            lista_adjuntos()
            saldo_dias()
            Exit Sub
        End If

        calcula_dias()
        lista_adjuntos()

        'valida_pendientes() 'se añade 29-03

        saldo_dias()

        lblHasta.text = Me.txtHasta.Text

    End Sub

    Protected Sub HoraInicio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraInicio.TextChanged

        lblMensaje0.text = ""

        Dim vMensaje As String = ""

        If trim(Me.ddlHoraFin.Text) = "05:00" Then 'Se añade Validación

            Me.ddlHoraFin.Text = Me.ddlHoraInicio.Text
        Else
            If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
                vMensaje = "*Aviso : La hora final debe ser mayor que la hora de inicio."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.ddlHoraFin.Text = Me.ddlHoraInicio.Text
                calcula_horas()
                lista_adjuntos()
                Exit Sub
            End If
        End If
        
        calcula_horas()
        lista_adjuntos()

    End Sub

    Protected Sub HoraFin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraFin.TextChanged

        lblMensaje0.text = ""

        Dim vMensaje As String = ""
        If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
            vMensaje = "*Aviso : La hora de inicio debe ser menor que la hora final."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.ddlHoraFin.Text = Me.ddlHoraInicio.Text
            calcula_horas()
            lista_adjuntos()
            Exit Sub
        End If
        calcula_horas()
        lista_adjuntos()

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        registrar_filtros()
        respuesta = "C"
        Response.Redirect("SolicitudesTramite.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&respuesta=" & respuesta)
    End Sub

    Private Sub desactiva_controles()

        Me.ddlPrioridad.Enabled = False
        Me.ddlTipoSolicitud.Enabled = False
        Me.txtDesde.Enabled = False
        Me.txtHasta.Enabled = False
        Me.txtmotivo.Enabled = False
        Me.ddlHoraInicio.Enabled = False
        Me.ddlHoraFin.Enabled = False

        Me.ddlTipoLicencia.Enabled = False
        Me.ddlMotivoLicencia.Enabled = False
        'Me.ddlDetalleLicencia.Enabled = False
        Me.ddlTipoActividad.Enabled = False
        Me.ddlPais.Enabled = False

        Me.txtCiudad.Enabled = False
        Me.txtActividad.Enabled = False
        Me.txtInstitucion.Enabled = False

        Me.btnGuardar.visible = False
        Me.btnGuardarEnviar.Visible = False

        'Si es Rechazado o Aprobado Personal ya no pueden adjuntar archivos
        If Me.lblEstado.Text = "Rechazado" Or Me.lblEstado.Text = "Aprobado Personal" Then
            Me.lblAdjuntos.visible = False
            Me.files.Visible = False 'Se bloqueó
            btnSubirAdjunto.visible = False

            If ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones
                lblAvisoSubida.Text = ""
            Else
                If chkCumple.Checked = True Then '13/08 Se añade esta línea
                    lblAvisoSubida.Text = ""
                End If
                lblAvisoSubida.Text = "*Nota: Usted ya no puede subir más archivos."
            End If
            'chkCumple.enabled = False 'Se añade
        Else
            If chkCumple.Checked = True Then '13/08 Se añade esta línea
                lblAvisoSubida.Text = "**Aviso: En Permiso por Cumpleaños no es necesario archivos adjuntos"
            Else
                If ddlTipoSolicitud.SelectedValue = 3 Then
                    lblAvisoSubida.Text = "**Aviso: En Vacaciones no es necesario archivos adjuntos"
                End If
                lblAvisoSubida.Text = "**Aviso: Todavía puede subir archivos"
            End If

        End If

        Me.lbladjunto1.Visible = False
        Me.lbladjunto2.Visible = False

        If Me.lblEstado.Text = "Enviado" Then
            Me.lblObservaDirector.Visible = False
            Me.lblObservaPersonal.Visible = False
            Me.txtObservacion.Visible = False
            Me.txtObservacionPersonal.Visible = False

            If chkCumple.Checked = True Then '13/08
                btnSubirAdjunto.Visible = False 'Se añadió
            End If
        End If

        If Me.lblEstado.Text = "Nuevo" Or Me.lblEstado.Text = "Generado" Then
            Me.lblAdjuntos.Visible = False 'Se añadió  
            Me.files.Visible = False 'Se añadió
            'lblAvisoSubida.visible = False  '13/08 se bloquea
            btnSubirAdjunto.Visible = False 'Se añadió

            'If chkCumple.Checked = True Then  'se añade // 18/02/2020 Se bloquea línea
            Me.lblAdjuntos.Visible = False 'Se añadió  13/08
            Me.btnGuardar.Visible = True
            Me.btnGuardarEnviar.Visible = True
            Me.lbladjunto1.Visible = False
            Me.lbladjunto2.Visible = False
            'chkCumple.Enabled = False '09/08/19 ---- 13/08 se bloquea
            'End If ' 18/02/2020 Se bloquea línea

        Else
            If chkCumple.Checked = False Then
                chkCumple.Visible = False
                ftchkCumple.Visible = False
            Else
                chkCumple.Visible = True
                ftchkCumple.Visible = True
                chkCumple.Enabled = False '09/08/19
            End If

        End If

    End Sub

    Private Sub activa_controles()

        Me.ddlPrioridad.Enabled = True
        Me.ddlTipoSolicitud.Enabled = True
        Me.txtDesde.Enabled = True
        Me.lblAdjuntos.Visible = True 'Se añadió  13/08
        Me.files.Visible = True   '13/08

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Si es Vacaciones
            Me.lblSaldoDias.visible = True
            Me.lblSPend.visible = True

            lblDiasPend.visible = True
            lblDPendi.visible = True
            lblpendientes.visible = True

            Me.lbladjunto1.visible = False
            Me.lbladjunto2.Visible = False

            Me.lblAdjuntos.Visible = False  'Se añadió  20/08
            Me.files.Visible = False  '20/08

        Else
            Me.lblSaldoDias.visible = False
            Me.lblSPend.visible = False

            lblDiasPend.visible = False
            lblDPendi.visible = False
            lblpendientes.visible = False

            Me.lbladjunto1.visible = True
            Me.lbladjunto2.Visible = True

            Me.lblMotivo.visible = True
            Me.txtmotivo.visible = True
            Me.txtmotivo.Enabled = True

        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Si es Permisos x Horas
            Me.txtHasta.Enabled = False
            Me.ddlHoraInicio.Enabled = True
            Me.ddlHoraFin.Enabled = True

            If chkCumple.checked = True Then
                Me.txtmotivo.enabled = False
                Me.lblAdjuntos.Visible = False 'Se añadió
                Me.files.Visible = False  '20/08
            Else
                Me.txtmotivo.enabled = True
                Me.lblAdjuntos.Visible = True 'Se añadió
            End If
        Else
            Me.txtHasta.Enabled = True
        End If

        Me.btnGuardar.visible = True
        Me.btnGuardarEnviar.visible = True 

        Me.txtFechaEstado.Visible = True 'Se añadió

        Me.lblObservaDirector.Visible = False
        Me.lblObservaPersonal.Visible = False
        Me.txtObservacion.Visible = False
        Me.txtObservacionPersonal.Visible = False

        If Me.lblEstado.Text = "Nuevo" Then
            txtFechaEstado.Visible = False
        End If

        lblAvisoSubida.Text = "" '13/08

        btnSubirAdjunto.Visible = False

    End Sub

    Private Sub carga_horas()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultarHorasControl()

        ddlHoraInicio.DataSource = dts
        ddlHoraInicio.DataTextField = "hora"
        ddlHoraInicio.DataValueField = "hora"
        ddlHoraInicio.DataBind()

        ddlHoraFin.DataSource = dts
        ddlHoraFin.DataTextField = "hora"
        ddlHoraFin.DataValueField = "hora"
        ddlHoraFin.DataBind()
    End Sub

    Private Sub lista_adjuntos()
        cod_ST = Request.QueryString("cod")
        ConsultarAdjuntos() 'Se añade para q no se pierda el botón de la lista del grid
    End Sub

    Private Sub valida_sesion()

        'If CInt(Session("id_per")) = Nothing Then
        '    Me.lblMensaje0.Text = "*ATENCIÓN : SU SESIÓN HA CADUCADO"
        'End If

        'Me.gvCarga.DataSource = Nothing
        'Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

    End Sub

    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles")
    'Private ruta As String = "http://serverdev/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' RUTA de Serverdev
    'Private ruta As String = "http://serverqa/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' RUTA de Serverqa
    'Private ruta As String = "http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx" ' RUTA de Producción

    Function SubirArchivo(ByVal id_tabla As Integer, ByVal nro_transaccion As String, ByVal ArchivoaSubir As HttpPostedFile, ByVal tipo As String, ByVal usuario_per As Integer) As String
        Try
            Dim codigo_tablaArchivo As String = id_tabla ' ID de tablaArchivo
            Dim archivo As HttpPostedFile = ArchivoaSubir
            Dim nro_operacion As String = ""
            Dim id_tablaProviene As String = nro_transaccion

            Dim Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim Usuario As String = Session("perlogin").ToString
            Dim Input(archivo.ContentLength) As Byte

            Dim b As New BinaryReader(archivo.InputStream)
            Dim binData As Byte() = b.ReadBytes(archivo.InputStream.Length)
            Dim base64 = System.Convert.ToBase64String(binData)

            Dim wsCloud As New ClsArchivosCompartidos
            Dim list As New Dictionary(Of String, String)

            Dim nombre_archivo As String = System.IO.Path.GetFileName(archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            list.Add("Fecha", Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(archivo.FileName))
            list.Add("Nombre", nombre_archivo)
            'list.Add("Nombre", Regex.Replace(System.IO.Path.GetFileName(post.FileName), "[^0-9A-Za-z._ ]", "_")) 'se cambia 25-04-19
            list.Add("TransaccionId", id_tablaProviene)
            list.Add("TablaId", codigo_tablaArchivo)
            list.Add("NroOperacion", nro_operacion)
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)
            Return result
        Catch ex As Exception
            Dim Data1 As New Dictionary(Of String, Object)()
            Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim JSONresult As String = ""
            Dim list As New List(Of Dictionary(Of String, Object))()
            Data1.Add("rpta", "1 - SUBIR ARCHIVO")
            Data1.Add("msje", ex.Message)
            list.Add(Data1)
            JSONresult = serializer.Serialize(list)
            Response.Write(JSONresult)
            Return JSONresult
        End Try
    End Function

    Protected Sub GuardarArchivos(ByVal codigo_ST As Integer)
        'Me.divMensaje.InnerHtml = ""
        'Me.divMensaje.Attributes.Remove("Class")
        extension = 0
        tamano = 0
        Response.Write("<script type='javascript'>alert('GUARDAR ARCHIVO')</script>")
        Dim idtabla As Integer = 11 '11 ServerDev
        Dim codigo_per As Integer = Session("id_per")
        Try
            If Me.files.HasFile Then
                Response.Write("<script type='javascript'>alert('ENCONTRO ARCHIVO')</script>")
                'permite .jpg, .jpeg, .pdf o .rar
                Dim ExtensionesPermitidas As String() = {".jpg", ".jpeg", ".pdf", ".rar"}
                'Dim valida As Integer = 0
                valida = 0
                Dim Archivos As HttpFileCollection = Request.Files

                For j As Integer = 0 To Archivos.Count - 1
                    extension = 0
                    For i As Integer = 0 To ExtensionesPermitidas.Length - 1
                        If Path.GetExtension(Archivos(j).FileName) = ExtensionesPermitidas(i) Then
                            'Response.Write(Path.GetExtension(Archivos(j).FileName) + " - " + ExtensionesPermitidas(i) + "<br>")
                            valida = valida + 1
                        Else 'Se añadió
                            extension = extension + 1
                        End If
                    Next
                    If Archivos(j).ContentLength > 2097152 Then '2MB= 2048 KB=2097152 B
                        tamano = 1
                        'El archivo no es del tamano permitido
                        Exit Sub
                    End If
                    If extension = ExtensionesPermitidas.Length Then
                        'El archivo no es de ningún tipo de extensión permitida
                        Exit Sub
                    End If
                Next

                If valida = Archivos.Count Then 'si todos los archivos tienen formato y peso permitido subimos los archivos, sino NO.
                    Response.Write("<script type='javascript'>alert('PASO VALIDACION')</script>")

                    Dim respuesta As String = ""
                    For i As Integer = 0 To Archivos.Count - 1
                        respuesta = SubirArchivo(idtabla, codigo_ST, Archivos(i), 0, codigo_per)
                        If respuesta.Contains("Registro procesado correctamente") Then
                            'Me.divMensaje.InnerHtml = Me.divMensaje.InnerHtml + "<span style='color:green'><i class='ion-checkmark-round'></i> Archivo Adjuntado Correctamente : " + Archivos(i).FileName.ToString + " </span><br>"
                        Else
                            'Me.divMensaje.InnerHtml = Me.divMensaje.InnerHtml + "<span style='color:red'><i class='ion-close-round'></i> No se Pudo Adjuntar Archivo : " + Archivos(i).FileName.ToString + " ,Verifique Extensión y Tamaño máximo(2MB). </span><br> "
                        End If
                    Next
                    'Me.divMensaje.Attributes.Add("Class", "alert alert-success")
                Else
                    'Me.divMensaje.InnerHtml = "Solo se aceptan tipos de archivo con extension '.jpg','.jpeg','.pdf','.rar'"
                    'Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
                End If
            Else
                'Me.divMensaje.InnerHtml = "Seleccione al menos un archivo para Adjuntar."
                'Me.divMensaje.Attributes.Add("Class", "alert alert-danger")
            End If
        Catch ex As Exception			
			Response.Write("<script type='javascript'>alert('" & ex.message & "')</script>")
            'Me.divMensaje.InnerHtml = "Los Archivos no se pudieron Subir: " & ex.Message
            'Me.divMensaje.Attributes.Add("Class", "alert alert-danger")

        End Try
    End Sub

    Protected Sub btnSubirAdjunto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubirAdjunto.Click

        cod_ST = lblNumero_Tramite.text

        If Me.files.HasFile Then

            GuardarArchivos(cod_ST)

            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("InsertaArchivosSolicitudTramite", cod_ST, CInt(Session("id_per")))

            Me.lblMensaje0.Text = "*Nota : El o los archivos se han guardado correctamente"

        Else
            Dim vMensaje As String = ""
            vMensaje = "¡Aviso! No ha seleccionado ningún archivo. Debe seleccionar archivos de hasta 2MB y de tipo: .jpg, .pdf o .rar."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)

        End If

        lblMensaje0.text = ""

        ConsultarAdjuntos()

    End Sub

    'Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
    '    Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    'End Sub

    Private Function EnviaCorreo(ByVal cod_EST As Integer, ByVal tipo As String) As Boolean
        Dim strMensaje As String
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim EmailDestino As String = ""
        Dim colaborador, solicitud, evaluador As String

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaDatosColaborador", CInt(Session("id_per")))
            obj.CerrarConexion()

            colaborador = dt.Rows(0).Item("Colaborador")

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", cod_EST)
            obj.CerrarConexion()

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = dt.Rows(0).Item("email")  'CORREO DE PRODUCCIÓN / Se cambió: email_Per a email                   
                End If
            Else
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = "cgastelo@usat.edu.pe"     'Correo para Desarrollo
                End If
            End If

            If dt.rows(0).item("codigo_TST") < 3 Then
                solicitud = "Solicitud de Licencia"
            ElseIf dt.rows(0).item("codigo_TST") = 4 Then
                solicitud = "Solicitud de Permiso"
            ElseIf dt.rows(0).item("codigo_TST") = 3 Then 'Se añadió
                solicitud = "Solicitud de Vacaciones"
            End If

            evaluador = dt.Rows(0).Item("Nombre_Evaluador")

            'Correo 
            If tipo = "E" Then
                strMensaje = "Estimado(a) " & evaluador & ": <br/>"
                strMensaje = strMensaje & "Responsable de " & dt.Rows(0).Item("CeCo_Evaluador") & "<br/><br/>"
                strMensaje = strMensaje & "El(la) colaborador(a): " & colaborador
                strMensaje = strMensaje & " acaba de enviar una  " & solicitud & " para su revisión.<br/>"
                strMensaje = strMensaje & "Favor de ingresar al Campus Virtual y realizar la evaluación correspondiente.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            cls = Nothing
            obj = Nothing

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Function

    Private Sub cumple_si()

        Dim dia, mes As Integer
        dia = Day(fecha_cumple)
        mes = Month(fecha_cumple)
        Me.txtDesde.text = DateSerial(Now.Date.Year, mes, dia) 'se ubica en el dia del cumple del presente año
        Me.txtHasta.text = DateSerial(Now.Date.Year, mes, dia) 'se ubica en el dia del cumple del presente año

        lblCumple.visible = True
        desactiva_controles()
        Me.lblHoraInicio.visible = False
        Me.ddlHoraInicio.visible = False
        Me.lblHoraFin.visible = False
        Me.ddlHoraFin.visible = False
        Me.lblHoras.visible = False
        Me.lblTotalHoras.visible = False
        Me.txtmotivo.Text = "Permiso por cumpleaños / Todo el día."
        Me.txtmotivo.ForeColor = Drawing.Color.DarkRed  '15/08

        Me.ddlTipoSolicitud.enabled = True
        txtDesde.enabled = True

        lblchkCump.text = "1"

    End Sub

    Private Sub cumple_no()

        lblCumple.visible = False
        activa_controles()

        'Me.txtmotivo.text = Me.lblmotivo_text.text  '15/08 se bloquea

        If Me.txtmotivo.Text = "Permiso por cumpleaños / Todo el día." Then  '16/08
            Me.txtmotivo.Text = ""
        End If

        lblchkCump.Text = "0"

        If ddlTipoSolicitud.SelectedValue = 4 Then 'se añade if 20/08
            Me.txtmotivo.Enabled = True
            Me.lblHoraInicio.Visible = True
            Me.ddlHoraInicio.Visible = True
            Me.lblHoraFin.Visible = True
            Me.ddlHoraFin.Visible = True
            Me.lblHoras.Visible = True
            Me.lblTotalHoras.Visible = True
        End If

    End Sub

    Protected Sub chkCumple_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCumple.CheckedChanged

        If chkCumple.checked = True Then
            valida_cumple()
            'Me.lblmotivo_text.text = trim(txtmotivo.text)   '15/08 se bloquea
            cumple_si()

        Else
            Me.txtDesde.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
            Me.txtHasta.Text = DateSerial(Now.Date.Year, Now.Month, Now.Day)
            cumple_no()
            Me.txtmotivo.Text = ""   '15/08
        End If

    End Sub

    Protected Sub ddlTipoLicencia_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoLicencia.TextChanged

        If ddlTipoLicencia.selectedvalue = 3 Then 'Capacitación

            lblMotivoLicencia.Visible = True
            Me.ddlMotivoLicencia.Visible = True
            Me.ddlMotivoLicencia.Enabled = True
            ConsultarTipoMotivoCapacitacion()

            'Me.ddlDetalleLicencia.Enabled = True
            lblPapel.Visible = True '19/09
            Me.ddlTipoActividad.Visible = True
            ConsultarTipoActividad()

            Me.divActividad.Visible = True
            Me.txtActividad.Enabled = True
            Me.txtInstitucion.Enabled = True

            divPais.Visible = True
            ConsultarPais()
            Me.ddlPais.Enabled = True
            Me.ddlPais.SelectedValue = 156 '17/09 Perú por defecto
            Me.txtCiudad.Enabled = True

            Me.lblAvisoTipoSol.Text = "*Nota: Por favor complete todos los campos solicitados sobre la Licencia por Capacitación"
            Me.lblMotivoDescansoMedico.Text = ""
            txtmotivo.Text = "**Escriba AQUI en forma detallada el motivo para solicitar la Licencia por Capacitación**"
            txtmotivo.ForeColor = Drawing.Color.DarkRed

        ElseIf ddlTipoLicencia.SelectedValue = 2 Then 'Si es Descanso Médico
            Me.lblAvisoTipoSol.Text = "*Debe entregar el sustento original dentro de las próximas 48 hrs. a su jefe inmediato o a la Asistenta Social"
            Me.lblMotivoDescansoMedico.Text = "*Debe especificar el motivo de salud"
            oculta_parcial()
            If lblEstado.Text = "Nuevo" Then '16/09
                txtmotivo.Text = ""
            End If

        ElseIf ddlTipoLicencia.SelectedValue = 1 Then 'Otros, para otros tipo de Licencia c/goce
            Me.lblAvisoTipoSol.Text = ""
            Me.lblMotivoDescansoMedico.Text = ""
            oculta_parcial()
            'txtmotivo.Text = ""
        End If

    End Sub

    Private Sub oculta_capacitacion()

        divTipoLicencia.Visible = False
        divActividad.Visible = False
        divPais.Visible = False

        'lblAvisoCiudad.visible = True
    End Sub

    Private Sub oculta_parcial()
        'oculta_capacitacion()
        divTipoLicencia.Visible = True
        divActividad.Visible = False
        divPais.Visible = False

        lblMotivoLicencia.Visible = False
        ddlMotivoLicencia.Visible = False
        'ddlDetalleLicencia.Visible = False
        lblPapel.Visible = False '19/09
        ddlTipoActividad.Visible = False
    End Sub

    Private Sub muestra_capacitacion()

        divTipoLicencia.Visible = True
        ddlTipoLicencia.Enabled = True '13/08
        lblMotivoLicencia.Visible = False
        ddlMotivoLicencia.Visible = False
        'ddlDetalleLicencia.Visible = False
        lblPapel.Visible = False '19/09
        Me.ddlTipoActividad.Visible = False

    End Sub

    Protected Sub ddlTipoSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.SelectedIndexChanged

        If Me.ddlTipoSolicitud.SelectedValue = 2 Then 'Licencia C/Goce

            If chkCumple.Checked = True Then 'se añade 15/08
                oculta_capacitacion()
            Else
                muestra_capacitacion()
                ConsultarClasificacionSolicitudTramite()
            End If
            
        Else
            oculta_capacitacion()

        End If

    End Sub

    Protected Sub ddlMotivoLicencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMotivoLicencia.SelectedIndexChanged

        'Me.ddlDetalleLicencia.visible = True
        'ConsultarDetalleMotivoCapacitacion()

        'If ddlMotivoLicencia.SelectedValue = 2 Then 'Experiencias de Intercambio
        '    Me.ddlTipoActividad.visible = True
        '    ConsultarTipoActividad()
        'Else
        '    Me.ddlTipoActividad.visible = False
        'End If

    End Sub

End Class
