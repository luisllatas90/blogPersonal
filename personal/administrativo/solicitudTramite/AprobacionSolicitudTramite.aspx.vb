﻿'Imports System.Collections.Generic
'Imports System.IO
'Imports System.Xml

Partial Class _AprobacionSolicitudTramite
    Inherits System.Web.UI.Page
    Dim cod_ST, cod_EST As Integer
    Dim mes As Integer
    Dim anio, estado, prioridad, tipo_tram, responsable, trabajador As String
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        If Not IsPostBack Then

            Me.hdid.Value = CInt(Session("id_per")) 'Por sesión
            Me.hdctf.Value = CInt(Session("codigo_ctf"))

            cod_ST = Request.QueryString("cod")
            cod_EST = Request.QueryString("codi") ' 25/06/19 Se activa Nuevamente
            Me.lblcod_EST.Text = cod_EST ' 06/06/19

            registrar_filtros()

            Me.divConfirmaAprobar.Visible = False
            Me.divConfirmaRechazar.Visible = False

            carga_horas()

            Me.txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Me.txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy")

            Me.txtDesde.Text = DateSerial(Now.Date.Year, Now.Month, 1)
            Me.txtHasta.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)

            calcula_dias()

            ConsultarTipoSolicitud()

            ConsultarSolicitudTramite()

            'ConsultarAdjuntos()

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

            Me.lblNumero_Tramite.Text = dt.Rows(0).Item("codigo_ST")
            Me.lblNumero_Tramite1.Text = dt.Rows(0).Item("codigo_ST")

            Me.lblcod_EST.Text = cod_EST

            lblcodigo_Per.text = dt.Rows(0).Item("codigo_Per") 'Se añadió para obtener el codigo_Per del colaborador

            If dt.Rows(0).Item("Prioridad") = "Urgente" Then
                Me.lblprioridad.ForeColor = Drawing.Color.OrangeRed
                Me.lblprioridad.Text = "Urgente"
            Else
                Me.lblprioridad.Text = "Normal"
            End If

            Me.lblColaborador.Text = dt.Rows(0).Item("Colaborador")
            Me.ddlTipoSolicitud.SelectedValue = dt.Rows(0).Item("codigo_TST")

            If dt.Rows(0).Item("codigo_TST") = 2 Then 'Licencia C/Goce 12/08                
                ConsultarClasificacionSolicitudTramite() '17/09

                If dt.Rows(0).Item("codigo_CST") <= 2 Then 'Otros o Descanso Médico
                    'ddlTipoLicencia.Visible = True '17/09 Siempre tiene q mostrarse
                    ddlTipoLicencia.SelectedValue = dt.Rows(0).Item("codigo_CST") '17/09
                    oculta_parcial() '17/09
                Else
                    ConsultarLicenciaCapacitacion() '02/08 Si es Licencia Capacitación
                End If

            Else
                oculta_capacitacion()
            End If

            valida_TipoSolicitud()

            Me.lblEstado.Text = dt.Rows(0).Item("Estado")

            Me.txtDesde.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortDate)
            Me.txtHasta.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortDate)
            calcula_dias()

            If dt.Rows(0).Item("codigo_TST") < 3 Then
                Me.lblTipo_Solic.Text = "Licencia "
                Me.lblTipo_Solic1.text = "Licencia "
                ConsultarAdjuntos()
            ElseIf dt.Rows(0).Item("codigo_TST") = 3 Then 'Se añadió Vacaciones
                Me.lblTipo_Solic.Text = "Vacaciones "
                Me.lblTipo_Solic1.text = "Vacaciones "
                ConsultaVacaciones()
                lblAdjuntos.visible = False
            ElseIf dt.Rows(0).Item("codigo_TST") = 4 Then
                Me.lblTipo_Solic.Text = "Permiso por Horas "
                Me.lblTipo_Solic1.text = "Permiso por Horas "
                ConsultarAdjuntos()
            End If

            If dt.Rows(0).Item("Estado") <> "Enviado" Then
                desactiva_controles() 'Se añade 04-07-19
            End If

            If dt.Rows(0).Item("Estado") = "Enviado" Then
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("fechaEnvio_ST"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Evaluación de "
                Me.lblPersonal.Text = dt.Rows(0).Item("Director") 'se añade 20/03/19
                lblObservaPersonal.visible = False
                Me.txtObservacionPersonal.Visible = False
                ConsultarEvaluacionSolicitud()

            ElseIf dt.Rows(0).Item("Estado") = "Aprobado Director" Then '
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de la "
                Me.txtObservacion.Text = dt.Rows(0).Item("Observacion_Director") 'Se añadió
                Me.lblPersonal.Text = dt.Rows(0).Item("Director") 'se añade 20/03/19

            ElseIf dt.Rows(0).Item("Estado") = "Aprobado Personal" Then
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de la "
                Me.lblPersonal.Text = dt.Rows(0).Item("Personal")
                Me.txtObservacion.Text = dt.Rows(0).Item("Observacion_Director") 'Se añadió             
                Me.txtObservacionPersonal.Text = dt.Rows(0).Item("Observacion_Personal") 'Se añadió
            Else 'Rechazados
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de la "
                Me.txtObservacion.Text = dt.Rows(0).Item("Observacion_Director") 'Se añadió

                If dt.Rows(0).Item("Respuesta_Dir") = 0 Then 'se añade 20/03/2019 Si es 0 está Enviado o Rechazado por Director y se debe mostrar solo Nombre del Director
                    Me.lblPersonal.Text = dt.Rows(0).Item("Director") 'se añade 20/03/19
                Else
                    Me.txtObservacionPersonal.Text = dt.Rows(0).Item("Observacion_Personal") 'Se añadió
                    Me.lblPersonal.Text = dt.Rows(0).Item("Personal")
                End If

            End If

            Me.txtMotivo.Text = dt.Rows(0).Item("motivo")

            If Me.nombre_titulo.Text = "Vista de la " Then
                Me.ddlHoraInicio.Enabled = False
                Me.ddlHoraFin.Enabled = False
            End If

            If dt.Rows(0).Item("codigo_TST") = 4 Then
                'Me.ddlHoraInicio.Text = CDate(dt.Rows(0).Item("fechahoraInicio_ST")).ToString("MM\/dd\/yyyy")
                'Me.ddlHoraFin.Text = CDate(dt.Rows(0).Item("fechahoraFin_ST")).ToString("HH:mm")
                Me.ddlHoraInicio.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                Me.ddlHoraFin.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                calcula_horas()
            End If

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Solicitud de Trámite"
        End Try

    End Sub

    Private Sub ConsultarEvaluacionSolicitud()
        Try
            'Para obtener el campo de observacion del registro:
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", Me.lblcod_EST.Text)
            obj.CerrarConexion()

            If dt.Rows(0).Item("Evaluador") <> CInt(Session("id_per")) Then 'Si Evaluador es diferente al codigo de Sesión lo aprueba Personal, sino es de Director
                'Si es diferente significa que Personal lo aprueba por EL Director
                Me.lblPorDirec.Text = "PD"
                Me.txtObservacion.Text = "Aprobación por Responsable de Área"
            Else
                'Lo está aprobando el Director de Área
                Me.lblPorDirec.Text = "DI"
            End If

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Evaluación de Solicitud de Trámite"
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
            'Me.lblMensaje.Text = "Error al cargar los Tipos de Solicitud Trámite"
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

                lblMotivoLicencia.Visible = True
                ddlMotivoLicencia.Visible = True
                'ddlDetalleLicencia.Visible = True

                ConsultarClasificacionSolicitudTramite()
                ddlTipoLicencia.SelectedValue = 3 'Capacitación
                'ddlTipoLicencia.Text = "Capacitación" ' values:3 pero no llamamos a los valores

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
            Else
                oculta_capacitacion()
            End If

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Evaluación de Solicitud de Trámite"
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

            dt = obj.TraerDataTable("ListaTipoMotivoSolicitudTramite", 3) 'el valor 3 es de Capacitación
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

    Private Sub ConsultaVacaciones()

        'Dim fecha_envio As Date
        'Dim dt1 As New Data.DataTable
        'Dim obj1 As New ClsConectarDatos
        'obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'obj1.AbrirConexion()
        'cod_ST = Request.QueryString("cod")
        'dt1 = obj1.TraerDataTable("ConsultaSolicitudTramite", cod_ST)
        'obj1.CerrarConexion()
        'fecha_envio = FormatDateTime(dt1.Rows(0).Item("fechaEnvio_ST"), DateFormat.GeneralDate)

        'Se añade para obtener el número de días como saldo de vacaciones
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("spPla_ProvisionVacacionesTotalAnual", Val(lblcodigo_Per.Text), DateTime.Now.ToString("dd/MM/yyyy")) '04-07-19 Se cambia "dd/MM/yyyy" por fecha_envio
        obj.CerrarConexion()

        lblDPendi.Text = dt.Rows(0).Item("DiasPendientes")
        'Response.Write(lblDPendi.text)

        Dim cadena As String
        Dim position As Integer
        cadena = Str(Trim(lblDPendi.Text))
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
                'lblDPendi.text = cadena.Substring(0, position)
            End If
        End If

        calcula_dias()

        Dim saldo As Integer
        saldo = Val(lblDPendi.Text) - Val(lblNum_dias.Text) 'Saldo de días de Vacaciones  '25/11 Se cambia

        'lblSPend.Text = saldo   '29/11  Se oculta
        'If saldo < 0 Then
        '    lblSPend.ForeColor = Drawing.Color.OrangeRed 'Rojo
        'Else
        '    lblSPend.ForeColor = Drawing.Color.DarkSlateBlue
        'End If

    End Sub

    Public Sub valida_TipoSolicitud()

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Se añadió Vacaciones
            'lblSaldoPend.visible = True  '29/11  Se oculta
            'lblSPend.visible = True
            lblDiasPend.visible = True
            lblDPendi.visible = True
            lblMotivo.visible = False
            txtMotivo.visible = False
        Else
            'lblSaldoPend.visible = False  '29/11  Se oculta
            'lblSPend.visible = False
            lblDiasPend.visible = False
            lblDPendi.visible = False
            lblMotivo.visible = True
            txtMotivo.visible = True
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Permisos por Horas
            Me.lblHoraFin.Visible = True
            Me.lblHoraInicio.Visible = True
            Me.lblHoras.Visible = True
            Me.lblTotalHoras.Visible = True

            Me.ddlHoraInicio.Visible = True
            Me.ddlHoraFin.Visible = True

            Me.lblNumDias.visible = False
            Me.lblNum_dias.Text = "0"
            Me.lblNum_dias.Visible = False

        Else
            Me.lblTotalHoras.Visible = False
            Me.lblHoraInicio.Visible = False
            Me.lblHoraFin.Visible = False
            Me.lblHoras.Visible = False
            Me.ddlHoraInicio.Visible = False
            Me.ddlHoraFin.Visible = False

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

            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Me.celdaGrid.Visible = True
                Me.celdaGrid.InnerHtml = "*Aviso: No existen Archivos Adjuntos relacionados"

                Me.lblAdjuntos.Visible = False 'Se añadió

            End If
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje0.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try
    End Sub

    Protected Sub ddlTipoSolicitud_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.TextChanged

        If Me.ddlTipoSolicitud.SelectedValue <> 4 Then
            calcula_dias()
        Else
            calcula_horas()
        End If
        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones
            ConsultaVacaciones()
        End If

        valida_TipoSolicitud()

    End Sub

    Public Sub calcula_dias()
        Me.lblNum_dias.Text = Str(DateDiff("d", Me.txtDesde.Text, Me.txtHasta.Text) + 1)
    End Sub

    Public Sub calcula_horas()

        Try
            'Calcula diferencia de hora y minutos
            Dim cadena As String
            Dim tot_horas As String
            Dim tot_min As Decimal
            Dim dif_hora As String
            Dim position As Integer

            cadena = Trim(Str(DateDiff("n", CDate(Me.ddlHoraInicio.Text), CDate(Me.ddlHoraFin.Text)) / 60)) 'Ej: 2.25 horas, 11 horas, 0.5 horas

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

            ElseIf cadena.Length <= 2 Then

                Me.lblTotalHoras.Text = cadena + " h"

            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.stackTrace)
        End Try

    End Sub

    Private Sub registrar_filtros()
        'Response.Write("responsable: " & Request.QueryString("responsable"))
        anio = Request.QueryString("anio")
        mes = Request.QueryString("mes")
        estado = Request.QueryString("estado")
        prioridad = Request.QueryString("prioridad")
        tipo_tram = Request.QueryString("tipo_tram")
        responsable = Request.QueryString("responsable")
        trabajador = Me.Request.QueryString("trabajador")

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        registrar_filtros()
        respuesta = "C"
        'Response.Write(respuesta)
        'En producción el codigo hdctf=198
        If Me.hdctf.Value = 198 Or Me.hdctf.Value = 137 Or Me.hdctf.Value = 141 Then 'Si es sesión de Control Lincencias y Permisos o Supervisor de Personal o Director de Personal
            Response.Redirect("SolicitudesTramitePorDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&responsable=" & responsable & "&trabajador=" & trabajador & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
        Else 'Si es sesión de Director
            Response.Redirect("SolicitudesTramiteDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
        End If

    End Sub

    Private Sub desactiva_controles()

        Me.txtObservacion.Enabled = False

        Me.btnAprobar.Enabled = False
        Me.btnRechazar.Enabled = False

    End Sub

    'Private Sub activa_controles()

    '    Me.ddlTipoSolicitud.Enabled = True
    '    Me.txtDesde.Enabled = True

    '    If Me.ddlTipoSolicitud.SelectedValue = 5 Then
    '        Me.txtHasta.Enabled = False
    '    Else
    '        Me.txtHasta.Enabled = True
    '    End If
    '    Me.txtmotivo.Enabled = True

    '    Me.btnEnviar.Enabled = True

    '    If Me.lblEstado.Text = "Pendiente" Then
    '        Me.btnEnviar.Enabled = True
    '    End If
    '    Me.txtFechaEstado.Visible = True

    '    Me.txtMotivo.Enabled = True
    '    Me.txtObservacion.Enabled = True

    'End Sub
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

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            Dim myLink As HyperLink = New HyperLink()
            myLink.NavigateUrl = "javascript:void(0)"
            myLink.Text = "Descargar"
            myLink.CssClass = "btn btn-xs btn-orange"
            myLink.Attributes.Add("onclick", "DescargarArchivo('" & Me.gvCarga.DataKeys(e.Row.RowIndex).Values("ID").ToString & "','" & Me.gvCarga.DataKeys(e.Row.RowIndex).Values("token").ToString & "')")

            e.Row.Cells(3).Controls.Add(myLink)

        End If

    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Me.divFormulario.Visible = False
        Me.divConfirmaAprobar.Visible = True
    End Sub

    Private Sub parteAprobar()

        Dim Fecha_Hora_Ini As DateTime
        Dim Fecha_Hora_Fin As DateTime
        Dim Param As Integer

        Param = Me.ddlTipoSolicitud.SelectedValue

        If Param = 4 Then 'Permiso
            Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + ddlHoraInicio.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + ddlHoraFin.Text)
        Else
            If Me.hdctf.Value <> 137 Then '05-07-19 Se añade permiso de Aprobación para la Supervisora Personal codigo_ctf=137
                'If Param = 3 And Month(CDate(txtDesde.Text)) = 12 And Month(Today) = 12 And Day(Today) > 3 Then '29/11 Se añade Limite fecha de aprobación: 03-12, cambiar día si se amplia plazo // 04-07 Se añade Validación para aprobación en Julio, fue hasta el 04 julio
                '    Dim vMensaje As String = ""
                '    vMensaje = "* ATENCIÓN: La aprobación de Vacaciones para el mes de diciembre finalizó el 03/12. Cualquier consulta realizarla con Dirección de Personal."
                '    Dim myscript As String = "alert('" & vMensaje & "')"
                '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                '    btnConfirmarRechazarSI.Enabled = True 'Se añadió
                '    desactiva_controles()
                '    Exit Sub
                'End If
                If Param = 3 And Month(CDate(txtDesde.Text)) = Month(Today) And Month(Today) <> 12 And Day(Today) > 15 Then '21/01/2020 se añade opción para todos los meses //  29/11 Se añade Limite fecha de aprobación: 03-12, cambiar día si se amplia plazo // 04-07 Se añade Validación para aprobación en Julio, fue hasta el 04 julio
                    Dim vMensaje As String = ""
                    vMensaje = "* ATENCIÓN: La aprobación de Vacaciones para el presente mes finalizó el día 15. Cualquier consulta realizarla con Dirección de Personal."
                    Dim myscript As String = "alert('" & vMensaje & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    btnConfirmarRechazarSI.Enabled = True 'Se añadió
                    desactiva_controles()
                    Exit Sub
                End If
            End If

            Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text)

        End If

        Try
            cod_ST = Trim(Me.lblNumero_Tramite.Text)
            cod_EST = Me.lblcod_EST.Text

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Response.Write(Me.lblPorDirec.Text)

            obj.Ejecutar("AprobacionSolicitudTramite", val(cod_EST), CInt(Session("id_per")), 1, Trim(Me.txtObservacion.Text), 0, 0, cod_ST, Fecha_Hora_Ini, Fecha_Hora_Fin, "D", trim(Me.lblPorDirec.Text)) '06/06/19 Se cambian cod_ST y cod_EST /Se registra la respuesta
            obj.CerrarConexion()

            'se añade ObjCnx para traer respuesta
            Dim rptta As Integer
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim dt As New Data.DataTable
            'se añadió rptta para traer el valor
            rptta = ObjCnx.TraerValor("CreaEvaluacionSolicitudTramite", Trim(lblcodigo_Per.text), cod_ST, "P") '06/06/19 Se cambia a lblcodigo_Per / Se crea el registro en la tabla EvaluaciónSolicitudTramite para Personal
            'Response.Write(rptta)
            If rptta > 0 Then
                respuesta = "A"
                registrar_filtros()
                Me.lblMensaje0.Text = "** AVISO :  LA SOLICITUD SE HA APROBADO Y ENVIADO CORRECTAMENTE AL ÁREA DE PERSONAL"

                If Me.ddlTipoSolicitud.SelectedValue = 3 Then '----Solo se envía Correo a Personal Si es Vacaciones---- 
                    cod_ST = Request.QueryString("cod")
                    Dim envio As Integer
                    Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                    'Dim dt1 As New Data.DataTable
                    envio = ObjCnx1.TraerValor("PER_ConsultaEnvioCorreoSolicitudTramite", cod_ST, "D", "AP") 'Se añade 16/07 "AP" de Aprobación
                    If envio = 0 Then 'se añade 22/04/19
                        obj.AbrirConexion()
                        obj.Ejecutar("PER_EnviaCorreoSolicitudTramite", cod_ST, "D", "AP") 'Se añadió 13/05/2019 para confirmar el envio de correo a Personal / 16/07 se añade "AP"
                        obj.CerrarConexion()
                        EnviaCorreo("A") '--16/07 Cambia a letra A(aprobacion)/Se añadió el envío de correo del Director de Área a Personal cuando aprueba Vacaciones---
                    End If
                End If

                If Me.hdctf.Value = 198 Or Me.hdctf.Value = 137 Or Me.hdctf.Value = 141 Then 'Si es sesión del perfil: Control Lincencias y Permisos o Supervisor de Personal o Director de Personal
                    Response.Redirect("SolicitudesTramitePorDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&responsable=" & responsable & "&trabajador=" & trabajador & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
                Else 'Si es sesión de Director
                    Response.Redirect("SolicitudesTramiteDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        desactiva_controles()

    End Sub

    Protected Sub btnConfirmarAprobarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarAprobarNO.Click
        Me.divFormulario.Visible = True
        Me.divConfirmaAprobar.Visible = False
        Me.lblMensaje0.Text = "** Nota : Operación Cancelada"
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If

    End Sub

    Protected Sub btnbtnConfirmarAprobarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarAprobarSI.Click
        Me.divFormulario.Visible = True
        Me.divConfirmaAprobar.Visible = False
        parteAprobar()
    End Sub

    Protected Sub btnRechazar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRechazar.Click

        Dim Param As Integer

        Param = Me.ddlTipoSolicitud.SelectedValue

        If Me.hdctf.value <> 137 Then '05-07-19 Se añade permiso de Aprobación para la Supervisora Personal codigo_ctf=137
            If Param = 3 And Month(CDate(txtDesde.text)) = 7 And Day(Today) > 4 Then '04-07 Se añade Validación para aprobación en Julio, fue hasta el 04 julio
                Dim vMensaje As String = ""
                vMensaje = "* ATENCIÓN: El Rechazo de Vacaciones para el mes de julio finalizó el 04 de julio. Puede consultar con Dirección de Personal"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                btnConfirmarRechazarSI.enabled = True 'Se añadió
                desactiva_controles()
                Exit Sub
            End If
        End If

        If Me.txtObservacion.Text = "" Then
            Dim vMensaje As String = ""
            vMensaje = "* ATENCIÓN: Debe indicar el motivo de rechazo en la Observación"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            btnConfirmarRechazarSI.enabled = True 'Se añadió
            Exit Sub
        Else
            Me.divFormulario.Visible = False
            Me.divConfirmaRechazar.Visible = True
        End If

    End Sub

    Private Sub parteRechazar()

        Dim observacion As String

        Try
            'Se añade que la obervación tenga contenido al Rechazar
            observacion = Trim(Me.txtObservacion.Text)

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("RechazaSolicitudTramite", Val(Me.lblNumero_Tramite.Text), Val(Me.lblcod_EST.Text), Me.hdid.Value, observacion)
            Me.lblMensaje0.Text = "** AVISO :  LA SOLICITUD SE HA RECHAZADO CORRECTAMENTE"
            obj.CerrarConexion()

            '----Se añade 16/07 envía correo para Rechazo de Solicitudes------------
            cod_ST = Val(Me.lblNumero_Tramite.Text)
            Dim envio As Integer
            Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'Dim dt1 As New Data.DataTable
            envio = ObjCnx1.TraerValor("PER_ConsultaEnvioCorreoSolicitudTramite", cod_ST, "D", "RE")
            If envio = 0 Then
                obj.AbrirConexion()
                obj.Ejecutar("PER_EnviaCorreoSolicitudTramite", cod_ST, "D", "RE") 'Se añadió 13/05/2019 para confirmar el envio de correo a Personal / 16/07 se añade "A"
                obj.CerrarConexion()
                EnviaCorreo("R") '16/07 Letra R(rechazo)
            End If
            '-----------------------------------------------------------------------

            respuesta = "R"
            registrar_filtros()

            If Me.hdctf.Value = 198 Or Me.hdctf.Value = 137 Or Me.hdctf.Value = 141 Then 'Si es sesión de Control Linceicas y Permisos o Supervisor de Personal o Director de Personal
                Response.Redirect("SolicitudesTramitePorDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&responsable=" & responsable & "&trabajador=" & trabajador & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
            Else 'Si es sesión de Director
                Response.Redirect("SolicitudesTramiteDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Protected Sub btnConfirmaRechazarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarRechazarSI.Click
        Me.divFormulario.Visible = True
        Me.divConfirmaRechazar.Visible = False
        parteRechazar()
    End Sub

    Protected Sub btnConfirmaRechazarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarRechazarNO.Click
        Me.divFormulario.Visible = True
        Me.divConfirmaRechazar.Visible = False
        Me.lblMensaje0.Text = "** Nota : Operación Cancelada"
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

    Protected Sub txtDesde_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesde.TextChanged
        Dim vMensaje As String = ""
        If CDate(Me.txtDesde.Text) < Today Then 'Si la fecha de inicio es menor a la fecha de Hoy
            vMensaje = "Aviso : La fecha de inicio debe ser mayor o igual a la fecha actual"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.txtDesde.Text = Today
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then
            Me.txtHasta.Text = Me.txtDesde.Text
        Else
            If CDate(Me.txtDesde.Text) > CDate(Me.txtHasta.Text) Then
                vMensaje = "Aviso : La fecha de inicio debe ser menor o igual que la fecha final."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.txtHasta.Text = Me.txtDesde.Text
                calcula_dias()
                Exit Sub
            End If
            calcula_dias()
        End If
    End Sub

    Protected Sub txtHasta_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHasta.TextChanged
        Dim vMensaje As String = ""
        If CDate(Me.txtHasta.Text) < Today Then 'Si la fecha de inicio es menor a la fecha de Hoy
            vMensaje = "Aviso : La fecha de fin debe ser mayor o igual a la fecha actual"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.txtHasta.Text = Today
        End If
        If CDate(Me.txtDesde.Text) > CDate(Me.txtHasta.Text) Then
            vMensaje = "Aviso : La fecha final debe ser mayor o igual que la fecha de inicio."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.txtHasta.Text = Me.txtDesde.Text
            calcula_dias()
            Exit Sub
        End If
        calcula_dias()
    End Sub

    Protected Sub HoraInicio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraInicio.TextChanged
        Dim vMensaje As String = ""
        If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
            vMensaje = "Aviso : La hora final debe ser mayor que la hora de inicio."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.ddlHoraInicio.Text = Me.ddlHoraFin.Text
            calcula_horas()
            Exit Sub
        End If
        calcula_horas()
    End Sub

    Protected Sub HoraFin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraFin.TextChanged
        Dim vMensaje As String = ""
        If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
            vMensaje = "Aviso : La hora de inicio debe ser menor que la hora final."
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Me.ddlHoraFin.Text = Me.ddlHoraInicio.Text
            calcula_horas()
            Exit Sub
        End If
        calcula_horas()
    End Sub

    Protected Sub txtObservacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObservacion.TextChanged
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

    Private Function EnviaCorreo(ByVal tipo As String) As Boolean

        Dim strMensaje As String
        Dim cls As New ClsMail

        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            Dim EmailDestino As String = ""
            Dim personal, Ceco, director, Ceco_Ev As String

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EnvioCorreoSolicitudVacaciones")
            obj.CerrarConexion()

            Ceco = dt.Rows(0).Item("Ceco")
            Personal = dt.Rows(0).Item("Personal")

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then 'Se cambia 16/07
                EmailDestino = dt.Rows(0).Item("correo") 'Correo de Producción, de Supervisora de Personal
            Else
                EmailDestino = "cgastelo@usat.edu.pe" 'Correo de Desarrollo
            End If

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", val(Me.lblcod_EST.Text))
            obj.CerrarConexion()
            director = dt.Rows(0).Item("Nombre_Evaluador")
            Ceco_Ev = dt.Rows(0).Item("CeCo_Evaluador")

            Dim solicitud As String
            If dt.rows(0).item("codigo_TST") < 3 Then
                solicitud = "Solicitud de Licencia"
            ElseIf dt.rows(0).item("codigo_TST") = 4 Then
                solicitud = "Solicitud de Permiso por Horas"
            ElseIf dt.rows(0).item("codigo_TST") = 3 Then 'Se añadió
                solicitud = "Solicitud de Vacaciones"
            End If

            If tipo = "A" Then
                strMensaje = "Estimado(a): " & Personal & "<br/>"
                strMensaje = strMensaje & Ceco & "<br/><br/>"
                strMensaje = strMensaje & "Se han aprobado Vacaciones para el colaborador " & lblColaborador.text & ", del área de " & Ceco_Ev
                strMensaje = strMensaje & " (Dir. " & director & "), del " & txtDesde.text & " al " & txtHasta.text & " (" & trim(lblNum_dias.text) & " días).<br/><br/>"
                strMensaje = strMensaje & "Favor de ingresar al Sistema de Planillas y realizar la evaluación correspondiente.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Entrega de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            Dim obj1 As New ClsConectarDatos
            Dim dt1 As New Data.DataTable
            Dim EmailDestino2 As String = ""

            obj1.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj1.AbrirConexion()
            dt1 = obj1.TraerDataTable("ConsultaDatosColaborador", VAL(lblcodigo_Per.text))
            obj1.CerrarConexion()

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then 'Se cambia 16/07
                If dt1.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino2 = dt1.Rows(0).Item("email") 'Se cambio: email_Per a email
                End If
            Else
                If dt1.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino2 = "cgastelo@usat.edu.pe" 'Correo de Desarrollo
                End If
            End If

            If tipo = "R" Then 'Para Rechazo / Se añade 16/07/19
                strMensaje = "Estimado colaborador (a) " & trim(lblColaborador.text) & ": <br/><br/>"
                strMensaje = strMensaje & "El presente mensaje es para comunicarle que su " & solicitud & ", N° " & Val(Me.lblNumero_Tramite.Text) & ", ha sido Rechazada por su Jefe inmediato.<br/>"
                strMensaje = strMensaje & "Puede revisar el detalle de la respuesta en su Campus Virtual. "
                strMensaje = strMensaje & "Si tiene consultas, por favor comuníquese con su superior.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino2, "Rechazo de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            cls = Nothing
            obj = Nothing

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Function

End Class
