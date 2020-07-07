﻿Partial Class _AprobacionSolicitudTramitePersonal
    Inherits System.Web.UI.Page
    Dim cod_ST As Integer
    Dim mes, categ_permiso, ctf As Integer
    Dim anio, estado, prioridad, tip_sol As String
    Dim fecha_ini, fecha_fin As Date
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        If Not IsPostBack Then
            'Response.Write(Request.QueryString("id")
            Me.hdid.Value = CInt(Session("id_per")) 'Por sesión
            Me.hdctf.Value = CInt(Session("codigo_ctf"))

            cod_ST = Request.QueryString("cod")
            Me.lblcod_EST.Text = Request.QueryString("codi")

            registrar_filtros()

            Me.divConfirmaAprobar.Visible = False
            Me.divConfirmaRechazar.Visible = False

            carga_horas()

            Me.txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy")
            Me.txtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy")

            Me.txtDesde.Text = DateSerial(Now.Date.Year, Now.Month, 1)
            Me.txtHasta.Text = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)

            If tip_sol = "L" Then
                Me.lblTipo_Solic.Text = "Licencia "
                Me.lblTipo_Solic1.text = "Licencia "
            ElseIf tip_sol = "P" Then
                Me.lblTipo_Solic.Text = "Permiso por Horas "
                Me.lblTipo_Solic1.text = "Permiso por Horas "
            ElseIf tip_sol = "V" Then 'Se añadió
                Me.lblTipo_Solic.Text = "Vacaciones "
                Me.lblTipo_Solic1.text = "Vacaciones "
                lblTipoPerm.visible = False
                Me.ddlTipoPermiso.Visible = False
            End If

            'calcula_dias()

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
            dt = obj.TraerDataTable("ConsultaSolicitudTramitePersonal", cod_ST)
            obj.CerrarConexion()

            Me.lblNumero_Tramite.Text = dt.Rows(0).Item("codigo_ST")
            Me.lblNumero_Tramite1.Text = dt.Rows(0).Item("codigo_ST")
            Me.lblcodigo_Per.Text = dt.Rows(0).Item("codigo_Per")

            Me.lblColaborador.Text = dt.Rows(0).Item("Colaborador")
            Me.ddlTipoSolicitud.SelectedValue = dt.Rows(0).Item("codigo_TST")

            If dt.Rows(0).Item("codigo_TST") = 2 Then 'Licencia C/Goce 12/08                
                ConsultarClasificacionSolicitudTramite() '17/09
                If dt.Rows(0).Item("codigo_CST") = 2 Then 'Descanso Médico
                    Me.ddlTipoPermiso.SelectedValue = 32 'Descanso Médico
                    ddlTipoLicencia.SelectedValue = dt.Rows(0).Item("codigo_CST") '17/09
                    oculta_parcial() '17/09
                ElseIf dt.Rows(0).Item("codigo_CST") = 1 Then 'Otros
                    ddlTipoLicencia.SelectedValue = dt.Rows(0).Item("codigo_CST") '17/09
                    oculta_parcial() '17/09
                Else 'Si es 3: Licencia Capacitación
                    ConsultarLicenciaCapacitacion() '02/08
                End If
            Else
                oculta_capacitacion()
            End If

            'valida_TipoSolicitud()

            Me.txtDesde.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortDate)
            Me.txtHasta.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortDate)
            calcula_dias()

            Me.ddlTipoSolicitud.SelectedValue = dt.Rows(0).Item("codigo_TST")

            If Me.ddlTipoSolicitud.SelectedValue < 3 Then 'Licencias
                categ_permiso = 2 'Licencias S/G y C/G
                ConsultarAdjuntos()
                Me.divSaldo.visible = False 'se añade 05-03-19
            ElseIf Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones  se añade 22/02/2019
                ConsultaVacaciones()
                lblAdjuntos.visible = False
            ElseIf Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Permiso por Horas
                categ_permiso = 1 'Permisos
                ConsultarAdjuntos()
                Me.divSaldo.visible = False 'se añade 05-03-19
            End If
            valida_TipoSolicitud() 'se añade 22/02/2019
            ConsultarTipoPermiso()

            Me.lblEstado.Text = dt.Rows(0).Item("Estado")
            If dt.Rows(0).Item("Estado") <> "Aprobado Director" Then 'Rechazados y Aprobados Personal
                desactiva_controles()
            End If

            If dt.Rows(0).Item("Estado") = "Aprobado Director" Then
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Respuesta"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Evaluación de la"
                valida_Goce()
                Me.btnGuardar.visible = False 'se añade 05-03-19
                Me.lblPersonal.Text = dt.Rows(0).Item("Director") 'Se añade 15-03-2019
            ElseIf dt.Rows(0).Item("Estado") = "Aprobado Personal" Then 'Para Aprobados
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de la"
                Me.ddlTipoPermiso.SelectedValue = dt.Rows(0).Item("codigo_Tpp")
                Me.chkConGoce.Checked = dt.Rows(0).Item("conGoce_EST")
                If Me.chkConGoce.Checked = True Then
                    Me.chkConGoce.Text = "Sí"
                Else
                    Me.chkConGoce.Text = "No"
                End If
                Me.lblPersonal.Text = dt.Rows(0).Item("Personal") 'Se añade 15-03-2019
            Else  'Rechazados
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)
                Me.nombre_titulo.Text = "Vista de la"
                Me.btnGuardar.visible = False 'se añade 05-03-19
                Me.lblPersonal.Text = dt.Rows(0).Item("Personal")
                Me.divTipoPermiso.Visible = False '19/08
            End If
            Me.txtMotivo.Text = dt.Rows(0).Item("motivo")
            If dt.Rows(0).Item("Prioridad") = "Urgente" Then
                Me.lblprioridad.ForeColor = Drawing.Color.OrangeRed
                Me.lblprioridad.Text = "Urgente"
            Else
                Me.lblprioridad.Text = "Normal"
            End If

            If dt.Rows(0).Item("Estado") = "Aprobado Director" Then
                Me.txtObservacionPer.Text = dt.Rows(0).Item("motivo")
            ElseIf dt.Rows(0).Item("Estado") = "Aprobado Personal" Then 'se añadió esta parte Si es Aprobado Personal para agregar la observación de Vacaciones
                Me.txtObservacionPer.Text = dt.Rows(0).Item("Observacion_Per")
                If trim(txtObservacionPer.Text) = "" Then
                    Me.txtObservacionPer.enabled = True
                Else
                    Me.txtObservacionPer.enabled = False
                    btnGuardar.visible = False
                End If
            ElseIf dt.Rows(0).Item("Estado") = "Rechazado" Then
                btnGuardar.visible = False
                Me.txtObservacion.Text = dt.Rows(0).Item("Observacion") 'se añade 22-03-19
                Me.txtObservacionPer.Text = dt.Rows(0).Item("Observacion_Per")
            End If

            If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Si Permiso toma las horas guardadas por Vigilancia
                '--Horas Solicitadas
                Me.ddlHoraInicioSol.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortTime)
                Me.ddlHoraFinSol.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortTime)

                '--Horas Reales usadas y grabadas desde Vigilancia
                If dt.Rows(0).Item("fechahoraIniAutorizada_ST") <> "" Then
                    Me.ddlHoraInicio.Visible = False
                    Me.txtHInicio.Text = FormatDateTime(dt.Rows(0).Item("fechahoraIniAutorizada_ST"), DateFormat.ShortTime)
                    Me.txtHInicio.Enabled = False
                Else
                    Me.ddlHoraInicio.Visible = True
                    Me.ddlHoraInicio.ForeColor = Drawing.Color.DarkRed
                    Me.txtHInicio.Visible = False
                End If

                If dt.Rows(0).Item("fechahoraFinAutorizada_ST") <> "" Then
                    'ddlHoraFin.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFinAutorizada_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                    Me.ddlHoraFin.Visible = False
                    Me.txtHFin.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFinAutorizada_ST"), DateFormat.ShortTime)
                    Me.txtHFin.Enabled = False
                Else
                    Me.ddlHoraFin.Visible = True
                    Me.ddlHoraFin.ForeColor = Drawing.Color.DarkRed
                    Me.txtHFin.Visible = False
                End If

                If (Me.txtHInicio.Visible = True And Me.txtHFin.Visible = True) Then
                    calcula_horas()
                End If
                If (Me.ddlHoraInicio.ForeColor <> Drawing.Color.DarkRed And Me.ddlHoraFin.ForeColor <> Drawing.Color.DarkRed) Then
                    calcula_horas()
                End If

            End If

            Me.txtObservacion.Text = dt.Rows(0).Item("Observacion")

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Solicitud de Trámite"
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

            'valida_TipoSolicitud()

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
                'Me.divTipoPermiso.Visible = False '19/08

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
                txtInstitucion.Text = dt.Rows(0).Item("institucion_DLC")
                txtCiudad.Text = dt.Rows(0).Item("ciudad_DLC")

                Me.ddlTipoPermiso.SelectedValue = 39 '19/08 Por Capacitación

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

        'Se añade para obtener el número de días como saldo de vacaciones
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("spPla_ProvisionVacacionesTotalAnual", val(lblcodigo_Per.text), DateTime.Now.ToString("dd/MM/yyyy"))
        obj.CerrarConexion()

        lblDPendi.text = dt.Rows(0).Item("DiasPendientes")

        Dim cadena As String
        Dim position As Integer
        cadena = Str(trim(lblDPendi.text))
        If cadena.Length > 2 Then 'quiere decir que puede se decimal
            position = cadena.IndexOf(".")
            If position > 0 Then
                If Val(cadena) < 1 Then
                    lblDPendi.Text = "0"
                Else
                    lblDPendi.text = cadena.Substring(0, position)
                End If
            Else
                position = 3
                lblDPendi.text = cadena.Substring(0, position)
            End If
        End If

        calcula_dias()

        Dim saldo As Integer
        saldo = Val(lblDPendi.Text) - Val(lblNum_dias.Text) 'Saldo de días de Vacaciones

        'lblSPend.Text = saldo  '29/11 Se oculta
        'If saldo < 0 Then
        '    lblSPend.ForeColor = Drawing.Color.OrangeRed 'Rojo
        'Else
        '    lblSPend.ForeColor = Drawing.Color.DarkSlateBlue
        'End If

    End Sub

    Public Sub valida_TipoSolicitud()

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Se añadió Vacaciones
            divSaldo.visible = True
            divMotivo.visible = False
            lblHoras.visible = False
            lblTotalHoras.visible = False
        Else
            divSaldo.visible = False
            divMotivo.visible = True
        End If

        If Me.ddlTipoSolicitud.SelectedValue = 4 Then 'Permisos por Horas
            Me.lblHoraFin.Visible = True
            Me.lblHoraInicio.Visible = True
            Me.lblHoras.Visible = True
            'Me.ddlHoraInicio.Visible = True
            'Me.ddlHoraFin.Visible = True
            Me.lblTotalHoras.Visible = True
            Me.lblHoraInicioSol.Visible = True
            Me.lblHoraFinSol.Visible = True
            Me.ddlHoraInicioSol.Visible = True
            Me.ddlHoraFinSol.Visible = True
            Me.lblNumDias.Visible = False
            Me.lblNum_dias.Text = "0"
            Me.lblNum_dias.Visible = False

        Else
            Me.lblTotalHoras.Visible = False
            Me.lblHoraInicio.Visible = False
            Me.lblHoraFin.Visible = False
            Me.lblHoras.Visible = False
            Me.ddlHoraInicio.Visible = False
            Me.ddlHoraFin.Visible = False
            Me.lblHoraInicioSol.Visible = False
            Me.lblHoraFinSol.Visible = False
            Me.ddlHoraInicioSol.Visible = False
            Me.ddlHoraFinSol.Visible = False

            Me.lblNumDias.Visible = True
            Me.lblNum_dias.Visible = True

            Me.txtHInicio.Visible = False
            Me.txtHFin.Visible = False

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
                Me.celdaGrid.InnerHtml = "Aviso: No existen Archivos Adjuntos relacionados"

                Me.lblAdjuntos.Visible = False 'Se añadió

            End If
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje0.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try
    End Sub

    Private Sub valida_Goce()

        If Me.ddlTipoSolicitud.SelectedValue = 2 Or Me.ddlTipoSolicitud.SelectedValue = 3 Then
            Me.chkConGoce.Checked = True 'Licencia C/Goce o Vacaciones
            Me.lblConGoce.text = "Sí"
            Me.chkConGoce.Enabled = False
        ElseIf Me.ddlTipoSolicitud.SelectedValue = 1 Then
            Me.chkConGoce.Checked = False  'Licencia S/Goce 
            Me.lblConGoce.Text = "No"
            Me.chkConGoce.Enabled = False
        Else '4 son Permisos a evaluar por Personal
            Me.chkConGoce.Checked = False
            Me.lblConGoce.Text = "No"
        End If

    End Sub

    Protected Sub ddlTipoSolicitud_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.TextChanged

        If Me.ddlTipoSolicitud.SelectedValue <> 4 Then
            calcula_dias()
        End If
        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Vacaciones
            ConsultaVacaciones()
        End If

        valida_TipoSolicitud()

    End Sub

    Public Sub calcula_dias()
        Me.lblNum_dias.Text = Str(DateDiff("d", Me.txtDesde.Text, Me.txtHasta.Text) + 1)
    End Sub

    Public Sub ConsultarTipoPermiso()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("sp_Pla_ConsultarTipoPermiso", "TO", categ_permiso)
            obj.CerrarConexion()

            Me.ddlTipoPermiso.DataTextField = "descripcion_Tpp"
            Me.ddlTipoPermiso.DataValueField = "codigo_Tpp"
            Me.ddlTipoPermiso.DataSource = dt
            Me.ddlTipoPermiso.DataBind()

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar los Tipo de Permiso"
        End Try

    End Sub

    Protected Sub ddlTipoPermiso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoPermiso.TextChanged

        cod_ST = Me.lblNumero_Tramite.Text

        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If

    End Sub

    Public Sub calcula_horas()

        Try
            'Calcula diferencia de hora y minutos
            Dim cadena As String
            Dim tot_horas As String
            Dim tot_min As Decimal
            Dim dif_hora As String
            Dim position As Integer

            If Me.txtHInicio.Visible And Me.txtHFin.Visible = True Then
                cadena = Trim(Str(DateDiff("n", CDate(Me.txtHInicio.Text), CDate(Me.txtHFin.Text)) / 60)) 'Ej: 2.25 horas
            ElseIf Me.ddlHoraInicio.Visible = True And Me.ddlHoraFin.Visible = True Then
                cadena = Trim(Str(DateDiff("n", CDate(Me.ddlHoraInicio.Text), CDate(Me.ddlHoraFin.Text)) / 60)) 'Ej: 2.25 horas
            ElseIf Me.txtHInicio.Visible = True And Me.ddlHoraFin.Visible = True Then 'De lo contrario Personal tiene q seleccionar la hora final del permiso personal
                cadena = Trim(Str(DateDiff("n", CDate(Me.txtHInicio.Text), CDate(Me.ddlHoraFin.Text)) / 60)) 'Ej: 2.25 horas
            ElseIf Me.ddlHoraInicio.Visible = True And Me.txtHFin.Visible = True Then
                cadena = Trim(Str(DateDiff("n", CDate(Me.ddlHoraInicio.Text), CDate(Me.txtHFin.Text)) / 60)) 'Ej: 2.25 horas
            End If

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

                Me.lblTotalHoras.Text = tot_horas + " h, " + Trim(Str(Math.Round(tot_min, 0))) + " m "

            ElseIf cadena.Length <= 2 Then

                Me.lblTotalHoras.Text = cadena + " h"

            End If

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.stackTrace)
        End Try

    End Sub

    Private Sub registrar_filtros()
        anio = Request.QueryString("anio")
        mes = Request.QueryString("mes")
        estado = Request.QueryString("estado")
        prioridad = Request.QueryString("prioridad")
        tip_sol = Request.QueryString("tip_sol")
        fecha_ini = CDate(Request.QueryString("fecha_ini"))
        fecha_fin = CDate(Request.QueryString("fecha_fin"))
        'Response.Write(tip_sol)
    End Sub

    Private Sub verifica_adjunto()
        'Se añade
        cod_ST = Me.lblNumero_Tramite.Text

        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

    Protected Sub ddlHoraInicio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraInicio.TextChanged
        Dim vMensaje As String = ""

        If Me.txtHFin.Visible = True Then
            If CDate(Me.ddlHoraInicio.Text) > CDate(Me.txtHFin.Text) Then
                vMensaje = "Aviso : La hora final debe ser mayor que la hora de inicio."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.ddlHoraInicio.Text = "05:00"
                Me.lblTotalHoras.Text = "--"
                verifica_adjunto() 'se añade
                Exit Sub
            End If
        Else
            If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
                vMensaje = "Aviso : La hora final debe ser mayor que la hora de inicio."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.ddlHoraInicio.Text = Me.ddlHoraFin.Text
                Me.lblTotalHoras.Text = "--"
                verifica_adjunto() 'se añade
                Exit Sub
            End If
        End If

        calcula_horas()

        verifica_adjunto() 'Se añade

    End Sub

    Protected Sub ddlHoraFin_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHoraFin.TextChanged
        Dim vMensaje As String = ""

        If Me.txtHInicio.Visible = True Then
            If CDate(Me.txtHInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
                vMensaje = "Aviso : La hora de inicio debe ser menor que la hora final."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.ddlHoraFin.Text = "05:00"
                Me.lblTotalHoras.Text = "--"
                verifica_adjunto() 'se añade
                Exit Sub
            End If
        Else
            If CDate(Me.ddlHoraInicio.Text) > CDate(Me.ddlHoraFin.Text) Then
                vMensaje = "Aviso : La hora de inicio debe ser menor que la hora final."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Me.ddlHoraFin.Text = Me.ddlHoraInicio.Text
                Me.lblTotalHoras.Text = "--"
                verifica_adjunto() 'se añade
                Exit Sub
            End If
        End If

        calcula_horas()

        verifica_adjunto() 'se añade

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        registrar_filtros()
        respuesta = "C"
        Response.Redirect("SolicitudesTramitePersonal.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)
    End Sub

    Private Sub desactiva_controles()

        Me.ddlTipoPermiso.Enabled = False
        Me.txtObservacionPer.Enabled = False
        Me.chkConGoce.Enabled = False
        Me.ddlHoraInicio.Enabled = False
        Me.ddlHoraFin.Enabled = False

        Me.btnAprobar.visible = False
        Me.btnRechazar.visible = False

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

        Dim obj1 As New clsPersonal
        Dim dts1 As New Data.DataTable

        dts1 = obj1.ConsultarHorasControl()

        ddlHoraInicioSol.DataSource = dts1
        ddlHoraInicioSol.DataTextField = "hora"
        ddlHoraInicioSol.DataValueField = "hora"
        ddlHoraInicioSol.DataBind()

        ddlHoraFinSol.DataSource = dts1
        ddlHoraFinSol.DataTextField = "hora"
        ddlHoraFinSol.DataValueField = "hora"
        ddlHoraFinSol.DataBind()

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

        Dim Param As Integer
        Param = Me.ddlTipoSolicitud.SelectedValue

        If Param = 4 Then
            'If ddlHoraInicio.visible = True Or Me.ddlHoraFin.visible = True Then
            If Me.ddlHoraFin.visible = True And Me.ddlHoraFin.text = "05:00" Then
                Dim vMensaje As String = ""
                vMensaje = "* ATENCIÓN: Debe indicar las horas de Salida y Retorno Reales para el control correspondiente"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)

                cod_ST = Me.lblNumero_Tramite.Text
                If ddlTipoSolicitud.SelectedValue = 3 Then
                    lblAdjuntos.visible = False
                Else
                    ConsultarAdjuntos() 'Para que no se pierda el boton del grid
                End If
                Exit Sub
            End If
            'End If
            If ddlHoraInicio.visible = True And Me.ddlHoraInicio.text = "05:00" Then
                Dim vMensaje As String = ""
                vMensaje = "* ATENCIÓN: Debe indicar las horas de Salida y Retorno Reales para el control correspondiente"
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)

                cod_ST = Me.lblNumero_Tramite.Text
                If ddlTipoSolicitud.SelectedValue = 3 Then
                    lblAdjuntos.visible = False
                Else
                    ConsultarAdjuntos() 'Para que no se pierda el boton del grid
                End If
                Exit Sub
            End If
        End If
        Me.divFormulario.Visible = False
        Me.divConfirmaAprobar.Visible = True
    End Sub

    Private Sub parteAprobar()

        Dim Fecha_Hora_Ini As DateTime
        Dim Fecha_Hora_Fin As DateTime
        Dim Param As Integer

        Param = Me.ddlTipoSolicitud.SelectedValue

        'If Me.txtHInicio.Visible = True Then
        '    If Param = 4 Then 'Si es Permiso graba la hora de Salida y Retorno real
        '        Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + Me.txtHInicio.Text)
        '        Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + Me.txtHFin.Text)
        '    Else
        '        Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
        '        Fecha_Hora_Fin = CDate(Me.txtHasta.Text)
        '    End If
        'Else
        '    If Param = 4 Then 'Si es Permiso graba la hora de Salida y Retorno real
        '        Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + ddlHoraInicio.Text)
        '        Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + ddlHoraFin.Text)
        '    Else
        '        Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
        '        Fecha_Hora_Fin = CDate(Me.txtHasta.Text)
        '    End If
        'End If

        If Param = 4 Then 'Si es Permiso graba la hora de Salida y Retorno real
            If Me.txtHInicio.Visible = True Then
                Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + Me.txtHInicio.Text)
            Else
                Fecha_Hora_Ini = CDate(Me.txtDesde.Text + " " + ddlHoraInicio.Text)
            End If
            If Me.txtHFin.Visible = True Then
                Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + Me.txtHFin.Text)
            Else
                Fecha_Hora_Fin = CDate(Me.txtHasta.Text + " " + ddlHoraFin.Text)
            End If
        Else
            Fecha_Hora_Ini = CDate(Me.txtDesde.Text)
            Fecha_Hora_Fin = CDate(Me.txtHasta.Text)
        End If

        Try
            cod_ST = Request.QueryString("cod")

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Response.Write(Session("codigo_ctf"))
            'Se Aprueba la Solicitud Trámite por Personal:
            obj.Ejecutar("AprobacionSolicitudTramite", Me.lblcod_EST.Text, CInt(Session("id_per")), 1, Trim(Me.txtObservacionPer.Text), ddlTipoPermiso.SelectedValue, Me.chkConGoce.Checked, cod_ST, Fecha_Hora_Ini, Fecha_Hora_Fin, "P", "")
            'Se Registra la solicitud en tabla PermisosPersonal: Se modifica Procedimiento, se añade campo codigo_ST
            obj.Ejecutar("sp_Pla_RegistrarPermisosPersonal", Fecha_Hora_Ini, Fecha_Hora_Fin, Trim(txtObservacionPer.Text), Me.lblcodigo_Per.Text, ddlTipoPermiso.SelectedValue, Me.chkConGoce.Checked, "A", 0, 0, Session("id_per"), cod_ST)
            obj.CerrarConexion()

            Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha aprobado y se ha creado el registro de permiso correctamente"
            respuesta = "A"
            registrar_filtros()
            Response.Redirect("SolicitudesTramitePersonal.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)

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
        If Me.txtObservacionPer.Text = "" Then
            Dim vMensaje As String = ""
            vMensaje = "* ATENCIÓN: Debe indicar el motivo de rechazo en la Observación"
            Dim myscript As String = "alert('" & vMensaje & "')"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            btnConfirmarRechazarSI.enabled = True 'Se añadió
            cod_ST = Me.lblNumero_Tramite.Text
            If ddlTipoSolicitud.SelectedValue = 3 Then
                lblAdjuntos.visible = False
            Else
                ConsultarAdjuntos() 'Para que no se pierda el boton del grid
            End If
            Exit Sub
        Else
            Me.divFormulario.Visible = False
            Me.divConfirmaRechazar.Visible = True
        End If
        
    End Sub

    Private Sub parteRechazar()

        'Dim observacion As String

        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'cod_ST = Request.QueryString("cod")
            'cod_EST = Request.QueryString("codi")

            obj.Ejecutar("RechazaSolicitudTramite", Val(Me.lblNumero_Tramite.Text), Val(Me.lblcod_EST.Text), CInt(Session("id_per")), Trim(Me.txtObservacionPer.Text))
            Me.lblMensaje0.Text = "** Aviso :  La Solicitud se ha rechazado correctamente"
            obj.CerrarConexion()
           
            Dim envio As Integer
            Dim ObjCnx1 As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            envio = ObjCnx1.TraerValor("PER_ConsultaEnvioCorreoSolicitudTramite", Val(Me.lblNumero_Tramite.Text), "P", "RE") '16/07/19 Se añade "RE"
            If envio = 0 Then 'se añade 22/04/19
                obj.AbrirConexion()
                obj.Ejecutar("PER_EnviaCorreoSolicitudTramite", Val(Me.lblNumero_Tramite.Text), "P", "RE") '16/07/19 Se añade "RE"/Se añadió 13/05/2019 para confirmar el envio de correo
                obj.CerrarConexion()
                'Se añade correo de Confirmación de RECHAZO
                EnviaCorreo("R") '----Se añadió el envío de correo para todo Tipo Solicitud
            End If
            respuesta = "R"
            registrar_filtros()
            Response.Redirect("SolicitudesTramitePersonal.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Protected Sub btnConfirmaRechazarSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarRechazarSI.Click
        Me.divFormulario.Visible = True
        Me.divConfirmaRechazar.Visible = False
        parteRechazar()
    End Sub

    Protected Sub btnConfirmarRechazarNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarRechazarNO.Click
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

    Protected Sub chkConGoce_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkConGoce.CheckedChanged
        If Me.chkConGoce.Checked = True Then
            Me.lblConGoce.Text = "Sí"
        Else
            Me.lblConGoce.Text = "No"
        End If
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

    Protected Sub txtObservacionPer_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObservacionPer.TextChanged
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click

        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            'Response.Write(Session("codigo_ctf"))

            obj.Ejecutar("GuardaObservacionVacaciones", trim(txtObservacionPer.text), val(Me.lblcod_EST.Text))

            obj.CerrarConexion()

            Me.lblMensaje0.Text = "** AVISO :  La Solicitud se ha aprobado y se ha creado el registro de permiso correctamente"

            respuesta = "GO" 'GRABA la OBSERVACION de Vacaciones
            registrar_filtros()
            Response.Redirect("SolicitudesTramitePersonal.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tip_sol=" & tip_sol & "&fecha_ini=" & fecha_ini & "&fecha_fin=" & fecha_fin & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Sub

    Private Function EnviaCorreo(ByVal tipo As String) As Boolean

        Dim strMensaje As String
        Dim cls As New ClsMail
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Dim EmailDestino As String = ""
        Dim colaborador, solicitud As String
        Dim valorSol As Integer

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaDatosColaborador", lblcodigo_Per.text)
            obj.CerrarConexion()

            If ConfigurationManager.appsettings("CorreoUsatActivo") = 1 Then 'Se cambia 16/07
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = dt.Rows(0).Item("email") 'Se cambio: email_Per a email
                End If
            Else
                If dt.Rows(0).Item("email").ToString <> "" Then
                    EmailDestino = "cgastelo@usat.edu.pe" 'Correo Desarrollo
                End If
            End If

            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultaEvaluacionSolicitudTramite", lblcod_EST.text)
            obj.CerrarConexion()

            valorSol = dt.rows(0).item("codigo_ST") 'número de la solicitud

            If dt.rows(0).item("codigo_TST") < 3 Then
                solicitud = "Solicitud de Licencia"
            ElseIf dt.rows(0).item("codigo_TST") = 4 Then
                solicitud = "Solicitud de Permiso por Horas"
            ElseIf dt.rows(0).item("codigo_TST") = 3 Then 'Se añadió
                solicitud = "Solicitud de Vacaciones"
            End If

            'fecha_ini = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortDate)
            'fecha_fin = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortDate)

            'Correo 
            If tipo = "R" Then 'Para Rechazo de todo Tipo Solicitud

                strMensaje = "Estimado colaborador (a) " & lblColaborador.text & ": <br/><br/>"
                strMensaje = strMensaje & "El presente mensaje es para comunicarle que su " & solicitud & ", N° " & valorSol & ", ha sido Rechazada por el área de Personal.<br/>"
                strMensaje = strMensaje & "Puede revisar el detalle de la respuesta en su Campus Virtual. "
                strMensaje = strMensaje & "Si tiene consultas, por favor comuníquese con el área mencionada.<br/><br/>"
                strMensaje = strMensaje & "Atte.<br/>"
                strMensaje = strMensaje & "Campus Virtual"
                cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", EmailDestino, "Rechazo de Solicitud de Trámite", strMensaje, True, "", "")
            End If

            cls = Nothing
            obj = Nothing

        Catch ex As Exception
            'ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

    End Function

End Class
