Imports System.Collections.Generic
Imports System.IO
Imports System.Xml

Partial Class _SolicitudTramitePersonal
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
            cod_EST = Request.QueryString("codi")

            registrar_filtros()

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

            Me.lblColaborador.Text = dt.Rows(0).Item("Colaborador")
            Me.ddlTipoSolicitud.SelectedValue = dt.Rows(0).Item("codigo_TST")
            valida_TipoSolicitud()

            If dt.Rows(0).Item("codigo_TST") = 4 Then 'Permiso por Horas 
                'Me.ddlHoraInicio.Text = CDate(dt.Rows(0).Item("fechahoraInicio_ST")).ToString("MM\/dd\/yyyy")
                'Me.ddlHoraFin.Text = CDate(dt.Rows(0).Item("fechahoraFin_ST")).ToString("HH:mm")
                Me.ddlHoraInicio.Text = FormatDateTime(dt.Rows(0).Item("fechahoraInicio_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                Me.ddlHoraFin.Text = FormatDateTime(dt.Rows(0).Item("fechahoraFin_ST"), DateFormat.ShortTime) 'Extrae solo la Hora
                calcula_horas()
            End If

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

            Me.lblEstado.Text = dt.Rows(0).Item("Estado")
            If dt.Rows(0).Item("Estado") <> "Enviado" Then
                'desactiva_controles()
            End If
            Me.txtMotivo.Text = dt.Rows(0).Item("motivo")

            If dt.Rows(0).Item("Estado") = "Enviado" Then
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("fechaEnvio_ST"), DateFormat.GeneralDate)
                lblObservaPersonal.visible = False
                Me.txtObservacionPersonal.Visible = False
            Else 'Para Aprobados y Rechazados
                Me.txtFechaEstado.Text = FormatDateTime(dt.Rows(0).Item("Fecha_Evaluacion"), DateFormat.GeneralDate)           
                Me.txtObservacion.Text = dt.Rows(0).Item("Observacion_Director") 'Se añadió
                Me.txtObservacionPersonal.Text = dt.Rows(0).Item("Observacion_Personal") 'Se añadió
            End If

            If dt.Rows(0).Item("Prioridad") = "Urgente" Then
                Me.lblprioridad.ForeColor = Drawing.Color.OrangeRed
                Me.lblprioridad.Text = "Urgente"
            Else
                Me.lblprioridad.Text = "Normal"
            End If

        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al cargar la Solicitud de Trámite"
        End Try

        ConsultarEvaluacionSolicitud()

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

            Me.txtObservacion.Text = dt.Rows(0).Item("Observacion")

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
                'position = 3
                'lblDPendi.text = cadena.Substring(0, position)
            End If
        End If

        calcula_dias()

        Dim saldo As Integer
        saldo = Val(lblDPendi.Text) - Val(lblNum_dias.Text) 'Saldo de días de Vacaciones

        lblSPend.Text = saldo
        If saldo < 0 Then
            lblSPend.ForeColor = Drawing.Color.OrangeRed 'Rojo
        Else
            lblSPend.ForeColor = Drawing.Color.DarkSlateBlue
        End If

    End Sub

    Public Sub valida_TipoSolicitud()

        If Me.ddlTipoSolicitud.SelectedValue = 3 Then 'Se añadió Vacaciones
            lblSaldoPend.visible = True
            lblSPend.visible = True
            lblDiasPend.visible = True
            lblDPendi.visible = True
            lblMotivo.visible = False
            txtMotivo.visible = False
        Else
            lblSaldoPend.visible = False
            lblSPend.visible = False
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
                Me.celdaGrid.InnerHtml = "Aviso: No existen Archivos Adjuntos relacionados"

                Me.lblAdjuntos.Visible = False 'Se añadió

            End If
            obj.CerrarConexion()
        Catch ex As Exception
            Me.lblMensaje0.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try
    End Sub

    Public Sub calcula_dias()
        Me.lblNum_dias.Text = Str(DateDiff("d", Me.txtDesde.Text, Me.txtHasta.Text) + 1)
    End Sub

    Public Sub calcula_horas()
        'Calcula diferencia de hora y minutos
        Dim cadena As String
        Dim tot_horas As String
        Dim tot_min As Decimal
        Dim dif_hora As String
        Dim position As Integer

        cadena = Str(DateDiff("n", CDate(Me.ddlHoraInicio.Text), CDate(Me.ddlHoraFin.Text)) / 60) 'Ej: 2.25 horas

        If cadena.Length > 2 Then
            position = cadena.IndexOf(".")
            If Val(cadena) < 1 Then
                tot_horas = "0"
            Else
                tot_horas = cadena.Substring(0, position)
            End If

            dif_hora = cadena.Substring(position + 1)

            If dif_hora.Length = 2 Then
                tot_min = dif_hora * 60 * 0.01
            ElseIf dif_hora.Length = 1 Then
                tot_min = dif_hora * 60 * 0.1
            End If
            'tot_min = tot_min.ToString("0.##")

            Me.lblTotalHoras.Text = tot_horas + " h, " + Trim(Str(tot_min)) + " m "
        Else
            Me.lblTotalHoras.Text = cadena + " h"
        End If

    End Sub

    Private Sub registrar_filtros()
        'Response.Write("responsable: " & Request.QueryString("responsable"))
        anio = Request.QueryString("anio")
        mes = Request.QueryString("mes")
        estado = Request.QueryString("estado")
        prioridad = Request.QueryString("prioridad")
        tipo_tram = Request.QueryString("tipo_tram")
        trabajador = Me.Request.QueryString("trabajador")

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

        registrar_filtros()
        respuesta = "C"
        'Response.Write(respuesta)

        Response.Redirect("SolicitudesTramitePersonalDirector.aspx?anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&trabajador=" & trabajador & "&ctf=" & Me.hdctf.Value & "&respuesta=" & respuesta)

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

    Protected Sub txtObservacion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtObservacion.TextChanged
        cod_ST = Me.lblNumero_Tramite.Text
        If ddlTipoSolicitud.SelectedValue = 3 Then
            lblAdjuntos.visible = False
        Else
            ConsultarAdjuntos() 'Para que no se pierda el boton del grid
        End If
    End Sub

End Class
