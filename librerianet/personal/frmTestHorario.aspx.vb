﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class frmTestHorario
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer
    Dim totalminutos As Integer
    Dim totalminutoslbl As Integer

    Dim vSemana As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim obj As New clsPersonal
            codigo_per = Request.QueryString("id")

            'función que devuelve el codigo_pel vigente
            codigo_pel = obj.ConsultarPeridoLaborable
            ' Response.Write(codigo_pel)
            Me.ddlSemana.Enabled = obj.EsCCSalud(codigo_per)

            'Verifica el estado del trabajador. Activo no muestra el mensaje.
            '=============================================================================
            'Response.Write(obj.obj.FirmoContratoPersonal(codigo_per)obj.FirmoContratoPersonal(codigo_per)(codigo_per))
            lblAlertaContrato.Visible = obj.FirmoContratoPersonal(codigo_per)
            '=============================================================================

            If Not IsPostBack Then
                cargarControles()   'ok                
                'consultarDatosGenerales()      ---Linea Comentada

                CargaLeyenda()      'ok
                consultarVistaHorario() 'ok
                consultarListaHorario() 'ok
                consultarTotalHorasSemanas()    'ok
                CompararHorasSemanales()        'ok

                RangoFechasSemanas()            '-->aqui ta saliendo el error en en server test

                MostrarOpciones(False)  'agregado 23/11/11
                ConsultarHorasLectivas()

                'Cargar Horas para el refrigerio de la tabla HorarioGeneral.
                CargarHorasRefrigerio()
                VerificarPeriodoLaborable() 'para mostrar el mensaje del horario administrativo.

            End If

            consultarDatosGenerales()

            lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
            If Trim(lblObservacion.Text) <> "" Then
                lblObservacion.Text = "Obs. " & lblObservacion.Text
            End If
            'lblFechas.Text = obj.ConsultarRangoFechasSemana(Me.ddlSemana.SelectedValue)        

            ConsultarListaCambiosHorarios()

            '02/11/2011
            Me.btnRefrescar.Attributes.Add("onclick", "location.reload();")
            'Hr.Visible = False


            ModificarHrsLectivas(ddlSemana.SelectedValue)   'OK

            'Habilitamos la opcion para que el trabajador pueda modificar sus horas lectivas-------
            If lblEstadoHorario.Text = "Pendiente" Or lblEstadoHorario.Text = "Observado" Then
                Hr.Visible = True
            Else
                Hr.Visible = False
            End If
            '--------------------------------------------------------------------------------------
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub VerificarPeriodoLaborable()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.VerificaPeriodoLaborable()
            If dts.Rows.Count > 0 Then
                If dts.Rows(0).Item("periodo") = "0" Then 'veranex
                    lblHAdministrativo0.Text = "- Horario administrativo: Lunes - Viernes de 07:00 - 14:00"
                Else
                    lblHAdministrativo0.Text = "- Horario administrativo: Lunes - Viernes de 08:00 - 16:45"
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub EstadoContoles(ByVal vestado As Boolean)
        Try
            btnEnviar.Enabled = vestado
            btnBorrar.Enabled = vestado
            btnCopiarHorarioAdministrativo.Enabled = vestado
            btnRefrigerio1.Enabled = vestado
            btnRefrigerio2.Enabled = vestado
            btnRefrigerio3.Enabled = vestado
            btnRefrigerio4.Enabled = vestado
            ddlDiaRefrigerio.Enabled = vestado
            ddlRefrigerioInicio.Enabled = vestado
            btnRefrigerio.Enabled = vestado
            btnImportarHorarioAcademico.Enabled = vestado
            ddlDia.Enabled = vestado
            ddlHoraInicio.Enabled = vestado
            ddlHoraFin.Enabled = vestado
            ddlTipoActividad.Enabled = vestado
            rdbDepartamento.Enabled = vestado
            rdbFacultad.Enabled = vestado
            rdbEscuela.Enabled = vestado
            ddlEsFacuDep.Enabled = vestado
            ddlCentroCostos.Enabled = vestado
            txtEncEscuela.Enabled = vestado
            txtResEscuela.Enabled = vestado
            txtObservacion.Enabled = vestado
            btnAceptar.Enabled = vestado

            For i As Integer = 0 To gvEditHorario.Rows.Count - 1
                gvEditHorario.Rows(i).Cells(7).Enabled = vestado
            Next i

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarHorasRefrigerio()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ListaHorasRefrigerio()

            If dts.Rows.Count > 0 Then
                ddlRefrigerioInicio.DataSource = dts
                ddlRefrigerioInicio.DataTextField = "horaInicio"
                ddlRefrigerioInicio.DataValueField = "horaInicio"
                ddlRefrigerioInicio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    
    Private Sub verificarDedicacion()
        'Verificar el tipo de dedicacion para validar horario de refrigerio        
        If lblDedicacion.Text = "TIEMPO COMPLETO" Or lblDedicacion.Text = "MEDIO TIEMPO" Or lblDedicacion.Text = "DEDICACION EXCLUSIVA" Or lblDedicacion.Text = "TIEMPO PARCIAL > 20 HRS." Or lblDedicacion.Text = "TIEMPO PARCIAL < 20 HRS." Then
            HabilitarControlesRefrigerio()
        Else
            DeshabilitarControlesRefrigerio()
        End If


    End Sub

    Private Sub DeshabilitarControlesRefrigerio()
        btnRefrigerio1.Enabled = False
        btnRefrigerio2.Enabled = False
        btnRefrigerio3.Enabled = False
        btnRefrigerio4.Enabled = False
        btnRefrigerio.Enabled = False
    End Sub

    Private Sub HabilitarControlesRefrigerio()
        btnRefrigerio1.Enabled = True
        btnRefrigerio2.Enabled = True
        btnRefrigerio3.Enabled = True
        btnRefrigerio4.Enabled = True
        btnRefrigerio.Enabled = True
    End Sub

    Private Sub CargaTipoActividad()
        Dim objCentroCosto As New clsCentroCosto
        Dim dts As New Data.DataTable
        dts = objCentroCosto.ConsultarTipoActividad()
        If dts.Rows.Count > 0 Then
            ddlTipoActividad.DataTextField = "descripcion_td"
            ddlTipoActividad.DataValueField = "codigo_td"
            ddlTipoActividad.DataSource = dts
            ddlTipoActividad.DataBind()
            ddlTipoActividad.SelectedIndex = -1
        End If
    End Sub

    Private Sub cargarControles()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        Dim dtsEscuela As New Data.DataTable
        Dim dtsSemanas As New Data.DataTable
        Dim dtsFacultad As New Data.DataTable

        'Carga las horas para los combos de hora inicio y fin
        dts = obj.ConsultarHorasControl()

        ddlHoraInicio.DataSource = dts
        ddlHoraInicio.DataTextField = "hora"
        ddlHoraInicio.DataValueField = "hora"
        ddlHoraInicio.DataBind()

        ddlHoraFin.DataSource = dts
        ddlHoraFin.DataTextField = "hora"
        ddlHoraFin.DataValueField = "hora"
        ddlHoraFin.DataBind()

        'Cargamos el combo tipo de actividad
        CargaTipoActividad()        

        'Carga el dropdowlist de las semanas
        If ddlSemana.Enabled = True Then            
            dtsSemanas = obj.ConsultarTotalSemanas(codigo_pel)
            'Incluye semestre
            If dtsSemanas.Rows.Count > 0 Then
                ddlSemana.DataSource = dtsSemanas
                ddlSemana.DataTextField = "Semana"
                ddlSemana.DataValueField = "numeroSemana_sec"
                ddlSemana.DataBind()
                'Else
                '    ddlSemana.Items(0).Text = "Semestre"

            End If
        Else
            ddlSemana.Items(0).Text = "Semestral"
        End If
    End Sub

    Private Sub MostrarOpciones(ByVal vEstado As Boolean)
        'Me.lblEsFacuDep.Visible = Not vEstado
        rdbDepartamento.Visible = vEstado
        rdbEscuela.Visible = vEstado
        rdbFacultad.Visible = vEstado
    End Sub

    Protected Sub ddlTipoActividad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoActividad.SelectedIndexChanged

        'función que devuelva el codigo_pel vigente
        Dim obj As New clsPersonal
        codigo_pel = obj.ConsultarPeridoLaborable

        If ddlTipoActividad.SelectedIndex <> -1 Then
            '9 Facultad
            If ddlTipoActividad.SelectedValue = 9 Then
                CargarFacultad()
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 5 Or ddlTipoActividad.SelectedValue = 24 Then '5 Investigacion    24 Gestión en Investigacion  ----15/04/2019 MNeciosup
                CargarDepartamento()
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)
            ElseIf ddlTipoActividad.SelectedValue = 23 Then '23 formación
                CargarDepartamento()
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 1 Then '1 Gestión Apoyo Institucional             
                DeshabilitarEsFacuDep()
                CargaCentroCostos(1, 0, "", codigo_pel)
                Dim dts As New Data.DataTable
                dts = obj.ConsultarDatosPersonales(codigo_per)
                ' Fecha: 2019-01-10    Desarrollador: ENevado ***************************************************************
                Dim objx As New ClsConectarDatos
                objx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                objx.AbrirConexion()
                Dim dtx As New Data.DataTable
                Dim cod As Integer = dts.Rows(0).Item("codigo_CCO")
                dtx = objx.TraerDataTable("PER_ConsultarCentroCostosSeleccionados_v2", 1, 0, "SelecCco", codigo_pel, cod)
                objx.CerrarConexion()
                If dtx.Rows.Count > 0 Then
                    ddlCentroCostos.SelectedValue = dtx.Rows(0).Item("Codigo")
                End If
                ' ***********************************************************************************************************
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 8 Then  '8 Horas Asistenciales Clinica USAT
                CargaCentroCostos(8, 0, "", codigo_pel)
                DeshabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 16 Then  '16 Centro Pre
                CargaCentroCostos(16, 0, "", codigo_pel)
                DeshabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 7 Then '7 Practicas externas
                CargarEscuela(7)
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 17 Then 'Carga Administrativa (Reemplaza a: Apoyo Admin en Facultad/Escuela)
                ddlCentroCostos.Items.Clear()
                ddlEsFacuDep.Items.Clear()
                Call MostrarOpciones(True)
            Else
                CargarEscuela(0)
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)
            End If
        End If

    End Sub

    Private Sub CargaCentroCostos(ByVal vTipoActividad As Integer, ByVal vEsFacuDep As Integer, ByVal xTipo As String, ByVal xPeriodoLaborable As Integer)
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        'Response.Write("vTipoActividad: " & vTipoActividad)
        'Response.Write("<br />")
        'Response.Write("vEsFacuDep: " & vEsFacuDep)
        'Response.Write("<br />")
        'Response.Write("xTipo: " & xTipo)
        'Response.Write("<br />")
        'Response.Write("xPeriodoLaborable: " & xPeriodoLaborable)
        'Response.Write("<br />")
        'Response.Write("codigo_per: " & codigo_per)

        dts = obj.ConsultarCentroCostosSeleccionados_v2(vTipoActividad, vEsFacuDep, xTipo, xPeriodoLaborable, codigo_per)
        ddlCentroCostos.DataSource = dts
        ddlCentroCostos.DataTextField = "Descripcion"
        ddlCentroCostos.DataValueField = "Codigo"
        ddlCentroCostos.DataBind()
    End Sub

    Private Sub DeshabilitarEsFacuDep()
        ddlEsFacuDep.Enabled = False
        ddlEsFacuDep.Visible = False
        lblEsFacuDep.Text = ""
    End Sub

    Private Sub HabilitarEsFacuDep()
        ddlEsFacuDep.Enabled = True
        ddlEsFacuDep.Visible = True
        ddlCentroCostos.DataSource = Nothing
        ddlCentroCostos.DataBind()
        ddlCentroCostos.Items.Clear()
    End Sub

    Private Sub CargarFacultad()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarFacultad()
        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "nombre_fac"
        ddlEsFacuDep.DataValueField = "codigo_fac"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Facultad"
        ddlEsFacuDep.Enabled = True
    End Sub

    Private Sub CargarDepartamento()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarDptoAcademico()
        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "DptoAcademico"
        ddlEsFacuDep.DataValueField = "codigo"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Departamento"
        ddlEsFacuDep.Enabled = True
    End Sub

    Private Sub CargarEscuela(ByVal tipo As Integer)
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        'Practia externa
        If tipo = 7 Then
            dts = obj.ConsultarCarreraProfesionalyCentros()
        Else
            dts = obj.ConsultarCarreraProfesional_v2()
        End If
        ddlEsFacuDep.DataSource = dts
        ddlEsFacuDep.DataTextField = "nombre_cpf"
        ddlEsFacuDep.DataValueField = "codigo_cpf"
        ddlEsFacuDep.DataBind()
        lblEsFacuDep.Text = "Escuela"
        ddlEsFacuDep.Enabled = True
    End Sub

    Private Sub consultarDatosGenerales()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            'Recuperar ciclo vigente actual        
            Dim codigo_Cac As Integer = obj.ConsultarCicloAcademicoVigente()

            'Recupera el total de horas de asesoria de tesis
            If codigo_Cac <> 0 Then
                If (obj.ConsultarHorasAsesoria_V2(codigo_per, codigo_Cac)) > 0 Then
                    '#### Modificacion de horas de tesis, asigandas segun cantidad de tesis a cargo y que el alumno se encuentre matriculado ####
                    lblHorasAsesoria.Text = obj.ConsultarHorasAsesoria_V2(codigo_per, codigo_Cac)
                End If
            End If

            dts = obj.ConsultarDatosPersonales(codigo_per)
            Me.lblCeco.Text = dts.Rows(0).Item("descripcion_cco").ToString
            Me.lblNombre.Text = dts.Rows(0).Item("paterno").ToString & " " & dts.Rows(0).Item("materno").ToString & " " & dts.Rows(0).Item("nombres").ToString
            Me.lblTipo.Text = dts.Rows(0).Item("descripcion_Tpe").ToString
            Me.lblDedicacion.Text = dts.Rows(0).Item("descripcion_ded").ToString
            verificarDedicacion()
            'If dts.Rows(0).Item("descripcion_ded").ToString.Trim = "TIEMPO PARCIAL < 20 HRS." Then
            '    DeshabilitarHorarioRefrigerio()
            'End If
            Me.txtHoras.Text = dts.Rows(0).Item("horas").ToString
            Dim dtsEstadoHorario As New Data.DataTable

            dtsEstadoHorario = obj.ConsultarEstadoHorario(codigo_per)

            MostrarEstadoHorario(dtsEstadoHorario.Rows(0).Item("estado_hop").ToString)

            Me.lblFechaIngreso.Text = dts.Rows(0).Item("fechaIni_Per").ToString

            If dts.Rows(0).Item("foto").ToString <> "" Then
                'Me.imgFoto.ImageUrl = "http://www.usat.edu.pe/campusvirtual/personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
                Me.imgFoto.ImageUrl = "../../personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
            Else
                Me.imgFoto.BackColor = Drawing.Color.Red
                imgFoto.AlternateText = "ATENCIÓN:    Suba su foto en el módulo de HOJA DE VIDA"
                imgFoto.ForeColor = Drawing.Color.White
            End If

            'Si ha sido enviado al director no puede modificar datos del horario
            'Response.Write("envioDirector_Per: " & dts.Rows(0).Item("envioDirector_Per").ToString)

            If dts.Rows(0).Item("envioDirector_Per") = True Then
                EstadoContoles(False)
                'Si el horario ha sido enviado, y tiene estado Pendiente, muestra lblEnviado
                If lblEstadoHorario.Text = "Pendiente" Then
                    OcultarLblEstado()
                End If
            End If

            If (dts.Rows(0).Item("envioDirector_Per") = False And dts.Rows(0).Item("envioDirPersonal_Per") = False And dts.Rows(0).Item("estado_hop").ToString = "C") Then
                obj.ActualizarEstadoHop(codigo_per)
                ConsultaEstadoHop()
            End If

            'Agregado el 26/09/2011
            'Si ha sido enviado por el Director de Area, pero nunca por el trabajador, este puede Finalizar y Enviar pero no modificar
            If dts.Rows(0).Item("envioDirector_Per").ToString = "False" And dts.Rows(0).Item("envioDirPersonal_Per").ToString = "True" Then
                EstadoContoles(False)
                btnEnviar.Enabled = True
            End If
        Catch ex As Exception
            Response.Write("Error al cargar datos personales: " & ex.Message)
        End Try
    End Sub

    Private Sub ConsultaEstadoHop()
        Dim obj As New clsPersonal
        Dim dtsEstadoHorario As New Data.DataTable
        dtsEstadoHorario = obj.ConsultarEstadoHorario(codigo_per)
        MostrarEstadoHorario(dtsEstadoHorario.Rows(0).Item("estado_hop").ToString)
    End Sub

    Private Sub MostrarEstadoHorario(ByVal estado_hop As String)

        Select Case estado_hop
            Case "P"
                lblEstadoHorario.Text = "Pendiente"
                lblEstadoHorario.ForeColor = Drawing.Color.Orange
            Case "O"
                lblEstadoHorario.Text = "Observado"
                lblEstadoHorario.ForeColor = Drawing.Color.Red
            Case "C"
                lblEstadoHorario.Text = "Conforme"
                lblEstadoHorario.ForeColor = Drawing.Color.Green
        End Select

    End Sub


    Private Sub DeshabilitarHorarioRefrigerio()
        btnRefrigerio1.Enabled = False
        btnRefrigerio2.Enabled = False
        btnRefrigerio3.Enabled = False
        btnRefrigerio4.Enabled = False
        btnRefrigerio.Enabled = False
        ddlDiaRefrigerio.Enabled = False
        ddlRefrigerioInicio.Enabled = False
    End Sub


    Protected Sub btnConsideraciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsideraciones.Click
        Dim myscript As String = "window.open('frmConsideraciones.aspx', '', 'toolbar=no, location=no, directories=no, status=no, menubar=no, copyhistory=no, width=100, height=210, top=200, left=850')"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    End Sub


    Private Sub consultarListaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultarListaHorario_v2(codigo_per, codigo_pel, ddlSemana.SelectedValue)        
        gvEditHorario.DataSource = dts
        gvEditHorario.DataBind()
    End Sub

    Protected Sub ddlEsFacuDep_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEsFacuDep.SelectedIndexChanged
        CargaCentroCostos(ddlTipoActividad.SelectedValue, ddlEsFacuDep.SelectedValue, lblEsFacuDep.Text, codigo_pel)
    End Sub

    Protected Sub btnCopiarHorarioAdministrativo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopiarHorarioAdministrativo.Click
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarDatosPersonales(codigo_per)
        ' Fecha: 2019-01-11    Desarrollador: ENevado ***************************************************************
        Dim objx As New ClsConectarDatos
        objx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        objx.AbrirConexion()
        Dim dtx As New Data.DataTable
        Dim cod As Integer = dts.Rows(0).Item("codigo_CCO")
        dtx = objx.TraerDataTable("PER_ConsultarCentroCostosSeleccionados_v2", 1, 0, "SelecCco", codigo_pel, cod)
        objx.CerrarConexion()
        Dim cc As Integer
        If dtx.Rows.Count > 0 Then
            cc = dtx.Rows(0).Item("Codigo")
        Else
            cc = dts.Rows(0).Item("codigo_Cco")
        End If
        ' ************************************************************************************************************        
        obj.AsignarHorarioAdministrativo_v2(codigo_per, codigo_pel, ddlSemana.SelectedValue, cc)
        'P estado Pendiente
        obj.AsignarEstadoHorario(codigo_per,"P")
        consultarVistaHorario()
        consultarListaHorario()
        consultarTotalHorasSemanas()
        lblMensaje.Text = ""
        CompararHorasSemanales()

        ConsultarHorasLectivas()
    End Sub

    Protected Sub btnRefrigerio1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrigerio1.Click

        Dim obj As New clsPersonal
        Dim mensaje As String = ""
        Try
            mensaje = obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "LU", "13:00", "13:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MA", "13:00", "13:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MI", "13:00", "13:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "JU", "13:00", "13:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "VI", "13:00", "13:45")
            lblMensaje.Text = mensaje
            consultarVistaHorario()
            consultarListaHorario()
            consultarTotalHorasSemanas()

            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub btnRefrigerio2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrigerio2.Click
        Dim obj As New clsPersonal
        Dim mensaje As String = ""

        Try
            mensaje = obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "LU", "13:45", "14:30")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MA", "13:45", "14:30")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MI", "13:45", "14:30")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "JU", "13:45", "14:30")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "VI", "13:45", "14:30")
            lblMensaje.Text = mensaje
            consultarVistaHorario()
            consultarListaHorario()
            consultarTotalHorasSemanas()

            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
       
    End Sub

    Protected Sub btnRefrigerio3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrigerio3.Click
        Dim obj As New clsPersonal
        Dim mensaje As String = ""

        Try
            mensaje = obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "LU", "14:00", "14:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MA", "14:00", "14:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MI", "14:00", "14:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "JU", "14:00", "14:45")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "VI", "14:00", "14:45")
            lblMensaje.Text = mensaje
            consultarVistaHorario()
            consultarListaHorario()
            consultarTotalHorasSemanas()

            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
       
    End Sub

    Protected Sub btnRefrigerio4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrigerio4.Click
        Dim obj As New clsPersonal
        Dim mensaje As String = ""

        Try
            mensaje = obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "LU", "14:15", "15:00")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MA", "14:15", "15:00")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MI", "14:15", "15:00")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "JU", "14:15", "15:00")
            mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "VI", "14:15", "15:00")
            lblMensaje.Text = mensaje
            consultarVistaHorario()
            consultarListaHorario()
            consultarTotalHorasSemanas()

            ConsultarHorasLectivas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
       
    End Sub

    Protected Sub ddlRefrigerioInicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRefrigerioInicio.SelectedIndexChanged

        If (ddlRefrigerioInicio.SelectedValue <> "--Seleccione--") Then
            Dim hora As Date
            hora = ddlRefrigerioInicio.SelectedValue
            hora = DateAdd(DateInterval.Minute, 45, hora).ToLongTimeString()
            lblRefrigerioFin.Text = hora.ToString("HH:mm")

        Else
            lblRefrigerioFin.Text = ""
        End If

    End Sub

    Protected Sub btnRefrigerio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrigerio.Click
        Dim obj As New clsPersonal
        Dim mensaje As String = ""
        Try
            If ddlRefrigerioInicio.SelectedValue = "--Seleccione--" Then                
                Dim myscript As String = "alert('Por favor seleccione la hora inicio de su refrigerio')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            Else
                If (ddlDiaRefrigerio.SelectedValue = "TD") Then
                    mensaje = obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "LU", ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                    mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MA", ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                    mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "MI", ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                    mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "JU", ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                    mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, "VI", ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                Else
                    mensaje = mensaje & " " & obj.AsignarRefrigerio(codigo_per, codigo_pel, ddlSemana.SelectedValue, ddlDiaRefrigerio.SelectedValue, ddlRefrigerioInicio.SelectedValue, lblRefrigerioFin.Text)
                End If
                lblMensaje.Text = mensaje
                consultarVistaHorario()
                consultarListaHorario()
                consultarTotalHorasSemanas()

                ConsultarHorasLectivas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarHorasLectivas()
        Try
            Dim obj As New clsPersonal
            Dim HoraMinuto As String = obj.ValidarHorasGestionAcademicaConHorasLectivas(codigo_per, codigo_pel, ddlSemana.SelectedValue)
            lblTotalHorasLectivas.Text = HoraMinuto.Trim.ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ModificarHrsLectivas(ByVal vSemana As Integer)
        Dim obj As New clsPersonal
        Dim HoraMinuto As String = obj.ConsultarHorasCargaAcademica(codigo_per, codigo_pel)

        'Response.Write(vSemana)
        'Response.Write("-")
        'Response.Write(HoraMinuto)
        'Response.Write("-")
        'Response.Write(lblTotalHorasLectivas.Text)

        If lblTotalHorasLectivas.Text <> HoraMinuto Then
            Hr.Visible = True
            Hr.InnerHtml = "<a href='frmModificarHorasLectivas.aspx?codigo_pel=" & codigo_pel & "&codigo_per=" & codigo_per & "&Semana=" & vSemana & "&KeepThis=true&TB_iframe=true&height=350&width=570&modal=true' title='Modificar Horas Lectivas' class='thickbox'>Modificar Hras Lectivas<a/>"
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            Dim obj As New clsPersonal
            Dim cc As Integer
            Dim esfacudep As Integer
            Dim LugarPracticasExternas As String = ""
            Dim vMensaje As String = ""
            Dim SwEHA As Boolean = False        'MNeciosup 29/08/2019

            If ddlHoraInicio.Text >= ddlHoraFin.Text Then
                vMensaje = "La hora de inicio debe ser menor que la hora fin."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlTipoActividad.Text = 0 Then
                vMensaje = vMensaje & "Seleccione el tipo de actividad."
                Dim myscript As String = "alert('" & vMensaje & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If

            If ddlEsFacuDep.Enabled = True And ddlEsFacuDep.Text <> "" Then
                If ddlEsFacuDep.Text = 0 Then
                    vMensaje = vMensaje & "Seleccione " & lblEsFacuDep.Text & ". "
                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Exit Sub
                End If
            End If

            If ddlCentroCostos.Visible = True Then
                If ddlCentroCostos.SelectedValue = 0 Then
                    vMensaje = vMensaje & "Seleccione el centro de costos. "
                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Exit Sub
                End If
            End If

            '7 Practica Externa
            If ddlTipoActividad.SelectedValue = 7 Then
                'Validar observacion obligatoria
                If txtObservacion.Text = "" Then
                    vMensaje = vMensaje & "En el Campo Observación, espercificar el Lugar de las Prácticas Externas."

                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Exit Sub
                Else
                    LugarPracticasExternas = Me.txtObservacion.Text.ToString
                End If

            End If

            ''++++++++ 02/12/2012 x dguevara

            ''## Validamos 01/12/2011: Recuperamos la mitad de las horas lectivas, que servira para el registro del tipo de actividad Gestion Academico.

            '' Entra, siempre y cuando el tipo de actividad sea Gestion Academica
            'If ddlTipoActividad.SelectedValue = 11 Then
            '    Dim dts As New Data.DataTable
            '    dts = obj.HoraGestionAcademicaSegunLectivas(codigo_per, codigo_pel, ddlSemana.SelectedValue)

            '    'Asignamos los valores de las horas a dato de tipo fecha/hora para poder hacer las operaciones x dguevara
            '    Dim vHoraInicio As DateTime = ddlHoraInicio.Text
            '    Dim vHoraFin As DateTime = ddlHoraFin.Text

            '    'que el tipo dia se gestion administrativa
            '    If dts.Rows.Count > 0 Then
            '        If dts.Rows(0).Item("SoloMinuto") > 0 Then
            '            Dim MinutosAgregar As Integer = DateDiff(DateInterval.Minute, vHoraInicio, vHoraFin)
            '            If MinutosAgregar > dts.Rows(0).Item("SoloMinuto") Then
            '                vMensaje = vMensaje & "Ud. puede registrar como máximo " & dts.Rows(0).Item("HoraMinuto").ToString & " para este tipo de actividad."
            '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
            '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            '                Exit Sub
            '            End If
            '        End If
            '    End If

            'End If

            ''+++++++++++++++++++++++++++++

            '++++++++++++++++++++++++++++++++++++++++++++++++++ x dguevara 01/12/2011 ++++++++++++++++++++++++++++++++++++++++++++++++++

            '## Validamos 01/12/2011: Recuperamos la mitad de las horas lectivas, que servira para el registro del tipo de actividad Gestion Academico.

            ' Entra, siempre y cuando el tipo de actividad sea Gestion Academica
            If ddlTipoActividad.SelectedValue = 11 Then
                Dim dts As New Data.DataTable
                Dim min_MitadHrsLectivas As Decimal
                Dim min_GestionAcademicaReg As Decimal
                Dim tot_GestionAcademica As Decimal
                Dim min_Faltantes As Decimal

                dts = obj.HoraGestionAcademicaSegunLectivas(codigo_per, codigo_pel, ddlSemana.SelectedValue)

                min_MitadHrsLectivas = dts.Rows(0).Item("Mitad_HorasLectivas")
                min_GestionAcademicaReg = dts.Rows(0).Item("GestionAcademicaReg")

                'Asignamos los valores de las horas a dato de tipo fecha/hora para poder hacer las operaciones x dguevara
                Dim min_PorRegistrar As Decimal
                Dim vHoraInicio As DateTime = ddlHoraInicio.Text
                Dim vHoraFin As DateTime = ddlHoraFin.Text
                min_PorRegistrar = DateDiff(DateInterval.Minute, vHoraInicio, vHoraFin)

                tot_GestionAcademica = min_PorRegistrar + min_GestionAcademicaReg

                ''0. Cuando no tiene o no ha importado horas de lectivas
                If min_MitadHrsLectivas = 0 Then
                    vMensaje = vMensaje & "Ud. no puede registrar este tipo de actividad, debido a que no ha importado o no cuenta con Horas Lectivas, verifique."
                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Exit Sub
                End If

                ''1. Cuando no ha registrado minutos para Gestion Academica
                If min_GestionAcademicaReg = 0 And min_MitadHrsLectivas > 0 Then
                    If tot_GestionAcademica > min_MitadHrsLectivas Then
                        vMensaje = vMensaje & "Ud. puede registrar como máximo [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ] para este tipo de actividad."
                        Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Exit Sub
                    End If
                End If

                '2. Cuando el registro de horas es similar al permitido.
                If min_GestionAcademicaReg = min_MitadHrsLectivas Then
                    vMensaje = vMensaje & "Ud.  ya completo el máximo de horas permitidas para esta actividad [ " & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]"
                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    Exit Sub
                End If


                '3. Cuando el trabajador ya tiene registrado, horas de gestion academica.
                If (min_GestionAcademicaReg > 0 And min_MitadHrsLectivas > 0) And (min_GestionAcademicaReg <> min_MitadHrsLectivas) Then
                    If tot_GestionAcademica > min_MitadHrsLectivas Then
                        min_Faltantes = min_MitadHrsLectivas - min_GestionAcademicaReg
                        vMensaje = vMensaje & "Ud.  sólo puede registrar [ " & (min_Faltantes \ 60).ToString + " Hrs " & CType((min_Faltantes Mod 60), Integer).ToString & "Min ] de las  [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]" & " permitidas para este tipo de actividad."
                        Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                        Exit Sub
                    End If
                End If
            End If

            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            Dim abreviatura_td As String = obj.Consultarabreviatura_td(ddlTipoActividad.SelectedValue)

            'Mostrar una alerta si las horas elegidas se encuentran fuera del limite 7- 17:00 para todos
            'excepto para CC. Salud
            'Ojo: es solo para informar al usuario, la insercion en la bd se realiza aparte                

            '--------------------------------------------------------------------------------------------------------------------------------------------------------
            'EN ESTE BLOQUE REALIZA LA INSERCION DEL HORARIO ->[HorarioPersonal]
            If (obj.VerificarLimiteHoras("", ddlHoraInicio.Text, ddlHoraFin.Text, codigo_per, 0, abreviatura_td, 0, 0, "", "", 0, 0, 0, LugarPracticasExternas, 0, "n")) = True Then
                vMensaje = vMensaje & "Las Actividades:\n 1.Horas Lectivas\n 2.Asesoría de Tesis (Sólo para tipo: Tiempo Parcial <20 Horas)\n 3.Practica Externa\n 4.Centro Pre\n  pueden estar fuera del Horario Administrativo actual."
                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If
            '--------------------------------------------------------------------------------------------------------------------------------------------------------

            'Verificamos si está Exceptuado del Horario Administrativo
            SwEHA = obj.ConsultarPersonalExceptuadoHorarioAdministrativo(codigo_per)        'MNeciosup 29/08/2019

            If vMensaje = "" Then

                'Inicio -- 10/06/2019 MNeciosup
                'Validamos que si es docente no pueda ingresar horas de <<Carga Academica-Administrativa (17)>> y <<Gestion Apoyo Institucional (1)>>
                'fuera del horario administrativo vigente
                If (lblTipo.Text = "DOCENTE" And (ddlTipoActividad.SelectedValue = 1 Or ddlTipoActividad.SelectedValue = 17) And SwEHA = False) Then
                    Dim hora_IA As DateTime = Mid(lblHAdministrativo0.Text, 46, 5)          'Hora Administrativa de Inicio 
                    Dim hora_FA As DateTime = Mid(lblHAdministrativo0.Text, 54, 5)          'Hora Administrativa de Termino
                    Dim xxhora_ini As DateTime = ddlHoraInicio.Text                         'Hora ingresada de inicio
                    Dim xxhora_fin As DateTime = ddlHoraFin.Text                            'Hora ingresada de termino
                    Dim min_dif1 As Decimal
                    Dim min_dif2 As Decimal

                    min_dif1 = DateDiff(DateInterval.Minute, hora_IA, xxhora_ini)
                    min_dif2 = DateDiff(DateInterval.Minute, hora_FA, xxhora_fin)

                    If min_dif2 > 0 Or min_dif1 < 0 Then        'Cuando están fuera del horario administrativo
                        Dim myscript As String = "alert('Horas ingresadas deben estar dentro del Horario Administrativo')"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)

                        Exit Sub
                    End If
                End If
                ''Fin -- 10/06/2019 MNeciosup

                'Obtener el centro costos de acuerdo al tipo de actividad
                If ddlTipoActividad.SelectedIndex <> -1 Then
                    cc = ddlCentroCostos.SelectedValue
                End If

                If ddlEsFacuDep.Visible = False Then
                    esfacudep = 0
                Else
                    esfacudep = ddlEsFacuDep.SelectedValue
                End If

                'Verificar cruce de horarios                        
                'Obtener abreviatura

                Dim abreviatura As String = obj.Consultarabreviatura_td(ddlTipoActividad.SelectedValue)
                Dim mensajeAlerta As String = ""
                'Response.Write(codigo_per)
                'Response.Write(" - ")
                'Response.Write(codigo_pel)
                'Response.Write(" - ")
                'Response.Write(ddlSemana.SelectedValue)
                'Response.Write(" - ")
                'Response.Write(ddlHoraInicio.Text)
                'Response.Write(" - ")
                'Response.Write(ddlHoraFin.Text)
                'Response.Write(" - ")
                'Response.Write(ddlDia.SelectedValue)
                'Response.Write(" - ")
                'Response.Write(abreviatura)
                'Response.Write(" - ")
                'Response.Write(cc)

                'Devuelve un mensaje de los cruces. Sino, devuelve vacio
                'En el procediemiento ValidarCrucesFinal Realiza la insercion del horario, al dar click en la web en ASIGNAR.
                mensajeAlerta = obj.ValidarCrucesFinal(codigo_per, codigo_pel, ddlSemana.SelectedValue, ddlHoraInicio.Text, ddlHoraFin.Text, ddlDia.SelectedValue, abreviatura, cc, LugarPracticasExternas)
                lblMensaje.Text = mensajeAlerta
                obj.AsignarEstadoHorario(codigo_per, "P")
                consultarVistaHorario()
                consultarListaHorario()
                CompararHorasSemanales()
                consultarTotalHorasSemanas()

                'agreado 02/12/2011
                ConsultarHorasLectivas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


    Protected Sub gvEditHorario_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvEditHorario.RowDeleting
        Try
            Dim obj As New clsPersonal
            obj.EliminarHorarioPersonal(gvEditHorario.Rows(e.RowIndex).Cells(0).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        consultarVistaHorario()
        consultarListaHorario()
        consultarTotalHorasSemanas()

        '02/12/2011
        ConsultarHorasLectivas()  ' muestra la cantidad de horas lectivas
        ModificarHrsLectivas(Me.ddlSemana.SelectedValue)
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            Dim obj As New clsPersonal
            Dim dtshorariogeneral As New Data.DataTable
            dtshorariogeneral = obj.ConsultarHorarioGeneral()
            Dim dtspreenvio As New Data.DataTable

            ' --##  Validacion Temporal Practicas Externas
            Dim dtsPE As New Data.DataTable
            dtsPE = obj.PER_VerificaLugarPracticasExternas(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)

            'Response.Write(dtsPE.Rows.Count)

            If dtsPE.Rows.Count > 0 Then
                For k As Integer = 0 To dtsPE.Rows.Count - 1

                    If dtsPE.Rows(k).Item("observacion_hop").ToString = "" Then
                        'Response.Write("Entra Prueba")
                        lblMensaje.Visible = True
                        lblMensaje.Text = "Para el dia " & dtsPE.Rows(k).Item("diahop").ToString.ToUpper & " No se registro el Lugar para las Practicas Externas, fovor de Voler a registrar la actividad."
                        Exit Sub
                    End If

                Next
            End If
            '-------------------------------------------

            'Dim horasdiarias As Integer
            ''Valida limite de horas diarias = 12            
            'For j As Integer = 1 To 6
            '    Select Case j
            '        Case 1                       
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "LU")                        
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Lunes excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '        Case 2                        
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MA")
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Martes excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '        Case 3
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MI")
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Miércoles excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '        Case 4
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "JU")
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Jueves excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '        Case 5
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "VI")
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Viernes excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '        Case 6
            '            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "SA")
            '            If horasdiarias > 12 Then
            '                lblMensaje.Text = "La carga horaria del día Sábado excede el límite permitido (12 horas), por favor verifique."
            '                lblMensaje.Visible = True
            '                Exit Sub
            '            End If
            '    End Select
            'Next

            Dim horasdiarias As Integer
            'Valida limite de horas diarias = 12            
            Dim minrefrigerio As Integer            ''MNeciosup 21/06/2019

            For j As Integer = 1 To 6
                Select Case j
                    Case 1
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "LU")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Lunes excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "LU")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Lunes, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                    Case 2
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MA")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Martes excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MA")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Martes, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                    Case 3
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MI")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Miércoles excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MI")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Miércoles, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                    Case 4
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "JU")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Jueves excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "JU")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Jueves, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                    Case 5
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "VI")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Viernes excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "VI")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Viernes, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                    Case 6
                        horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "SA")
                        If horasdiarias > 12 Then
                            lblMensaje.Text = "La carga horaria del día Sábado excede el límite permitido (12 horas), por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                        'If horasdiarias >= 8 Then    ''MNeciosup 21/06/2019
                        '    minrefrigerio = obj.CalcularTotalMinutosRefrigerioDiario(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "SA")
                        '    If minrefrigerio < 45 Then
                        '        lblMensaje.Text = "Se debe ingresar 45 min de refrigerio en una Jornada Diaria de 8 hrs a más para el día Sábado, por favor verifique."
                        '        lblMensaje.Visible = True
                        '        Exit Sub
                        '    End If
                        'End If
                End Select
            Next


            'Recorre las horas inicio y fin, por dia
            For i As Integer = 0 To dtshorariogeneral.Rows.Count - 1
                Dim cantidadregistros As Integer
                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "LU")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If

                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "MA")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If

                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "MI")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If

                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "JU")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If

                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "VI")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If

                cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "SA")
                If cantidadregistros > 1 Then
                    lblMensaje.Text = "La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar."
                    lblMensaje.Visible = True
                    Exit Sub
                End If
            Next            

            'Devuelve el total de registros del horario semestral, el total de registros de
            'horario semanal y un valor que indica si existen ambos
            Dim dtsSemanaSemestre As New Data.DataTable
            dtsSemanaSemestre = obj.ValidarHorarioSemestralySemanal(codigo_pel, codigo_per)

            '---------------------------------------------------------------------------------------------------------
            '-----------------------PARA LOS TRABAJADORES QUE SON DE CIENCIAS DE LA SALUD-----------------------------
            '---------------------------------------------------------------------------------------------------------
            'Validar registro semestral o semanal para C. Salud
            If obj.EsCCSalud(codigo_per) = True Then                
                Dim valor As Integer = dtsSemanaSemestre.Rows(0).Item("valor")
                Dim horariossemanal As Integer = dtsSemanaSemestre.Rows(0).Item("HorarioSemanal")
                Dim dtssemanas As New Data.DataTable

                dtssemanas = obj.ConsultarTotalSemanas(codigo_pel)

                'Total de semanas del periodo laborable
                Dim totalSemanas As Integer = dtssemanas.Rows.Count - 1
                'Total de horas mensuales que debe tener
                Dim totalHorasMensuales As Integer = CType(txtHoras.Text, Integer) * totalSemanas

                If valor = 1 Then
                    lblMensaje.Text = "No puede registrar horario semestral y semanal, por favor verifique."
                    lblMensaje.Visible = True
                    Exit Sub
                Else
                    If horariossemanal > 0 Then
                        'Response.Write(lblHorasMensuales.Text)
                        'Response.Write(totalHorasMensuales)
                        'Verificar que haya registrado en todas las semanas                        
                        If CInt(lblHorasSemanales.Text) <> CInt(txtHoras.Text) Then
                            lblMensaje.Text = "Debe registrar un horario de " & txtHoras.Text & " horas para cada semana, por favor verifique."
                            lblMensaje.Visible = True
                            Exit Sub
                        End If
                    End If
                End If
            End If
            '-----------------------------------------------------------------------------------------------------------------------------------------------


            'Validar registro horario
            Dim TotalRegistrosSemanal As Integer = dtsSemanaSemestre.Rows(0).Item("HorarioSemanal")
            Dim TotalRegistrosSemestral As Integer = dtsSemanaSemestre.Rows(0).Item("HorarioSemestre")

            If TotalRegistrosSemanal = 0 And TotalRegistrosSemestral = 0 Then
                lblMensaje.Text = "Debe registrar un horario, favor de verificar."
                lblMensaje.Visible = True
                Exit Sub
            End If

            '---------------------------------------------------------------------------------------------
            'Agregado xD 29.03.2012 : Validar que las HorasSemanales = HoraSemanalesregistradas
            'txtHoras
            'lblHorasSemanales

            If CType(txtHoras.Text, Decimal) <> CType(lblHorasSemanales.Text, Decimal) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Las horas semanales asignadas por el área de personal no coinciden con la distribución de su horario, favor de verificar');", True)
                obj.EnviarAlertaHorarios(codigo_per, lblNombre.Text, Request.QueryString("id"))
                Exit Sub
            End If
            '---------------------------------------------------------------------------------------------

            '##############  Validar registro horas tesis #############################################################
            Dim resultado As String
            resultado = obj.ValidarRegistroHorasAsTesis(codigo_pel, codigo_per, lblHorasAsesoria.Text)
            'resultado = ""
            If resultado <> "" Then
                Dim myscript As String = "alert('" & resultado & "')"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                Exit Sub
            End If
            '########################################################################################################

            'Validar registro total de horas semanales
            Dim TotalHorasRegistradas As Integer

            'Total Horas Registradas en el Semestre
            TotalHorasRegistradas = gvTotalHorasSemanas.Rows(0).Cells(1).Text

            'Si hay registro semanal, obtengo el Total Horas Registradas en alguna de las semanas

            If gvTotalHorasSemanas.Rows.Count > 1 Then
                TotalHorasRegistradas = gvTotalHorasSemanas.Rows(1).Cells(1).Text
            End If

            If CInt(txtHoras.Text) <> TotalHorasRegistradas Then
                'lblMensaje.Text = "Las horas semanales no coinciden con la distribución, favor de verificar"
                'lblMensaje.Visible = True
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Las horas semanales asignadas por el área de personal no coinciden con la distribución de su horario, favor de verificar');", True)
                obj.EnviarAlertaHorarios(codigo_per, lblNombre.Text, Request.QueryString("id"))
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Horario enviado satisfactoriamente');", True)
                lblMensaje.Text = "Horario enviado satisfactoriamente"
                lblMensaje.ForeColor = Drawing.Color.Blue
                lblMensaje.Visible = True

                Me.btnEnviar.Enabled = False
                Me.btnAceptar.Enabled = False

                'Actualiza la tabla [dbo.datospersonal]
                'envioDirector_Per=1 ->
                'horasSemanales_Per - envioDirector_Per=1 - fechaEnvioDirector_Per
                obj.EnviarHorarioPersonal(codigo_per, CInt(Me.txtHoras.Text), Request.QueryString("id"))
                obj.registrarBitacoraHorario(codigo_per, codigo_pel, codigo_per)
                lblObservacion.Text = ""

                'Se deshabilitan los controles.
                DeshabilitarControles()
                'Se actualiza el grid de historico de envios
                ConsultarListaCambiosHorarios()
                OcultarLblEstado()

                If (lblEnviado.Visible = True Or lblEstadoHorario.Text = "Conforme") Then
                    Hr.Visible = False
                    'Hr.InnerHtml = "<a href='frmModificarHorasLectivas.aspx?codigo_pel=" & codigo_pel & "&codigo_per=" & codigo_per & "&Semana=" & vSemana & "&KeepThis=true&TB_iframe=true&height=350&width=570&modal=true' title='Modificar Horas Lectivas' class='thickbox'>Modificar Hras Lectivas<a/>"
                End If

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub DeshabilitarControles()

        Dim i As Integer
        For i = 0 To gvEditHorario.Rows.Count - 1
            gvEditHorario.Rows(i).Cells(7).Enabled = False            
        Next i

        txtHoras.Enabled = False
        btnBorrar.Enabled = False
        btnCopiarHorarioAdministrativo.Enabled = False
        btnImportarHorarioAcademico.Enabled = False
        btnAceptar.Enabled = False
        btnEnviar.Enabled = False
        btnConsideraciones.Enabled = False

        DeshabilitarControlesRefrigerio()

        ddlSemana.Enabled = False
        ddlEsFacuDep.Enabled = False
        ddlHoraFin.Enabled = False
        ddlHoraInicio.Enabled = False
        ddlDiaRefrigerio.Enabled = False
        ddlRefrigerioInicio.Enabled = False
        ddlCentroCostos.Enabled = False
        ddlDia.Enabled = False
        ddlTipoActividad.Enabled = False        
        txtEncEscuela.Enabled = False
        txtObservacion.Enabled = False
        txtResEscuela.Enabled = False
    End Sub

    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarVistaHorario_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        lblHorasSemanales.Text = obj.ConsultarTotalHorasSemana_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)
        lblHorasMensuales.Text = obj.TotalHorasMes_v2(codigo_per, codigo_pel)
        ConsultarHorasTesisRegistradas()

        '02/12/2012
        ConsultarHorasLectivas()
    End Sub

    Protected Sub gvVistaHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvVistaHorario.RowDataBound
        e.Row.Cells(2).Width = 40
        e.Row.Cells(3).Width = 40
        e.Row.Cells(4).Width = 40
        e.Row.Cells(5).Width = 40
        e.Row.Cells(6).Width = 40
        e.Row.Cells(7).Width = 40
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        For i As Integer = 2 To 7
            Select Case e.Row.Cells(i).Text
                Case "A "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("A")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "D "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("D")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "G "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("G")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "T "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("T")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "I "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("I")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "U "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("U")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "P "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("P")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "H "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("H")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "F "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("F")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "E "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("E")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "C "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("C")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "GR"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "GA"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "GP"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                Case "R "
                    dts = obj.ConsultaTipoActividadPorAbreviatura("R")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)

                    'Agregado 22/08/2011
                Case "CP"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    'Agregado 24/11/2011
                Case "CA"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("CA")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
                    'Agregado 13.03.18 esaavedra
                Case "FP"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("FP")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
            End Select
        Next

        If e.Row.Cells(0).Text = "08:00" Or e.Row.Cells(0).Text = "16:30" Then
            e.Row.Cells(0).ForeColor = Drawing.Color.Blue
            e.Row.Cells(1).ForeColor = Drawing.Color.Blue
        End If



    End Sub

    Private Sub CargaLeyenda()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultaTipoActividadPorAbreviatura("A")
        lblA.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("D")
        lblD.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblD.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("T")
        lblT.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblT.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("I")
        lblI.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblI.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("U")
        lblU.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblU.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("P")
        lblP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("H")
        lblH.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblH.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("C")
        lblC.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblC.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
        lblGR.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblGR.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
        lblCP.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblCP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("CA")
        lblCA.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblCA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("PE")
        lblPE.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblPE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        dts = obj.ConsultaTipoActividadPorAbreviatura("FP") ' formación permanente
        lblE.Text = dts.Rows(0).Item("descripcion_td").ToString
        lblE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        '==================================================================================================================
        '------------------------------------------------------Anulados ---------------------------------------------------
        '==================================================================================================================

        'dts = obj.ConsultaTipoActividadPorAbreviatura("G")
        'lblG.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblG.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("F")
        'lblF.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblF.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)


        'dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
        'lblGA.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        'dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
        'lblGP.Text = dts.Rows(0).Item("descripcion_td").ToString
        'lblGP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

        '==================================================================================================================

    End Sub

    Private Sub ConsultarListaCambiosHorarios()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.ConsultarListaCambiosHorarios(codigo_per, codigo_pel)
        gvListaCambios.DataSource = dts
        gvListaCambios.DataBind()
    End Sub

    Protected Sub cmdBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBorrar.Click
        Dim obj As New clsPersonal
        obj.AsignarHorarioAdministrativo(0, codigo_per, codigo_pel, ddlSemana.SelectedValue)
        consultarVistaHorario()
        consultarListaHorario()
        consultarTotalHorasSemanas()
        lblMensaje.Text = ""

        '01/01/2012
        ConsultarHorasLectivas()
    End Sub


    Protected Sub btnImportarHorarioAcademico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportarHorarioAcademico.Click
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            Dim codigo_Cac As Integer = obj.ConsultarCicloAcademicoVigente()
            obj.ActualizarHorarioAcademico_Personal(codigo_per, codigo_pel, codigo_Cac, ddlSemana.SelectedValue)
            dts = obj.ConsultarHorasDocencia(codigo_per, codigo_pel)

            'Tiene carga academica, validar cruces
            If dts.Rows.Count > 0 Then
                For i As Integer = 0 To dts.Rows.Count - 1
                    Dim dia As String = dts.Rows(i).Item("diahop").ToString
                    Dim horainicio_doc As String = dts.Rows(i).Item("horainicio_hop").ToString
                    Dim horafin_doc As String = dts.Rows(i).Item("horafin_hop").ToString
                    Dim dtsadministrativo As New Data.DataTable
                    dtsadministrativo = obj.ConsultarHorasAdministrativo(codigo_per, codigo_pel, dia, horainicio_doc, horafin_doc)

                    If dtsadministrativo.Rows.Count > 0 Then
                        Dim horainicio_adm As String = dtsadministrativo.Rows(0).Item("horainicio_hop").ToString
                        Dim horafin_adm As String = dtsadministrativo.Rows(0).Item("horafin_hop").ToString
                        Dim semana As Integer = ddlSemana.SelectedValue
                        Dim mensajeCruceHorarios As String = ""
                        mensajeCruceHorarios = obj.VerificarCruceHorarios(horainicio_adm, horafin_adm, horainicio_doc, horafin_doc, codigo_per, codigo_pel, dia, semana)
                        lblMensaje.Text = lblMensaje.Text + mensajeCruceHorarios
                    End If
                Next
            Else
                lblMensaje.Text = "Para el periodo actual, no posee carga académica. "
            End If

            consultarListaHorario()
            consultarVistaHorario()

            '01/12/2012
            ConsultarHorasLectivas()

            CompararHorasSemanales()
            consultarTotalHorasSemanas()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CompararHorasSemanales()
        If lblHorasSemanales.Text <> txtHoras.Text Then
            lblHorasSemanales.ForeColor = Drawing.Color.Red                        
            lblHorasSemanales.Width = 50
        End If
    End Sub

    Protected Sub ddlSemana_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemana.SelectedIndexChanged
        RangoFechasSemanas()
        consultarListaHorario()
        consultarVistaHorario()
        ConsultarListaCambiosHorarios()
        consultarTotalHorasSemanas()
        lblMensaje.Text = ""

        'agreado 01/12/2011
        Me.lblsx.Text = ddlSemana.SelectedValue
        ConsultarHorasLectivas()
        ModificarHrsLectivas(Me.lblsx.Text)

    End Sub

    Private Sub RangoFechasSemanas()        
        Try
            Dim dts As New Data.DataTable
            Dim dtsMesVigente As New Data.DataTable
            Dim obj As New clsPersonal
            Dim mes As String
            Dim Semana As Integer

            dtsMesVigente = obj.MesVigente(codigo_pel, 0, "C")
            mes = dtsMesVigente.Rows(0).Item("mes_sec").ToString
            Semana = ddlSemana.SelectedValue
            'Response.Write("semana - " & ddlSemana.SelectedValue)
            'Response.Write("mes - " & mes.ToString
            'Response.Write("semana - " & ddlSemana.SelectedValue)

            If dtsMesVigente.Rows.Count > 0 Then
                dts = obj.RangoFechasSemanasBitacora(Semana, mes, codigo_pel)
                Dim FechaIni As String = dts.Rows(0).Item("desde_sec").ToString
                Dim FechaFin As String = dts.Rows(0).Item("hasta_sec").ToString

                lblFechas.Visible = True
                lblFechas.Text = FechaIni & " hasta " & FechaFin
            Else
                Response.Write("<script>alert('Sugerencia, comuniquese con el administrador del sistema para definir el mes vigente para el periodo laborable actual.')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCambios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCambios.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                Dim mes_vigente As String = e.Row.Cells(1).Text
                Dim fecha As String = e.Row.Cells(2).Text
                Dim hora As String = e.Row.Cells(3).Text
                Dim registradopor As String = e.Row.Cells(4).Text
                e.Row.Cells(5).Text = "<a href='frmVerHorarios.aspx?codigo_pel=" & codigo_pel & "&codigo_per=" & codigo_per & "&registradopor=" & registradopor & "&fecha=" & fecha & "&hora=" & hora & "&mes_vigente=" & mes_vigente & "&KeepThis=true&TB_iframe=true&height=600&width=750&modal=true' title='Ver horarios' class='thickbox'><img src='../../images/resultados.gif' border=0 /><a/>"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub OcultarLblEstado()
        lblEstadoHorario.Visible = False
        lblEnviado.Visible = True
    End Sub

    Private Sub ConsultarHorasTesisRegistradas()
        Dim obj As New clsPersonal
        Dim totalhoras As String = obj.ConsultarHorasAsesoriaTesisHorario(codigo_per, codigo_pel, ddlSemana.SelectedValue)
        Dim horas As Integer = CType(totalhoras.Substring(0, 2).Trim, Integer)

        'Concatenar para mostrar en mensaje
        Dim horasmensaje As String = horas & " horas "

        'Convierte horas en minutos
        horas = horas * 60
        Dim posH As Integer = totalhoras.IndexOf("h")
        Dim posM As Integer = totalhoras.IndexOf("m")
        Dim minutos As Integer = CType(totalhoras.Substring(posH + 1, 2).Trim, Integer)
        totalminutos = horas + minutos

        If lblHorasAsesoria.Text <> "" Then
            totalminutoslbl = CType(lblHorasAsesoria.Text, Integer) * 60
        End If

        If minutos > 0 Then
            horasmensaje = horasmensaje & minutos & " minutos"
        End If
        lblHorasTesis.Text = horasmensaje
    End Sub

    '------------------------- Agregado el 26/09/2011
    Private Sub consultarTotalHorasSemanas()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        dts = obj.PER_CalcularHorasSemanas(codigo_per, codigo_pel)
        gvTotalHorasSemanas.DataSource = dts
        gvTotalHorasSemanas.DataBind()
    End Sub

    Private Sub HabilitarEsFacuDepCargaAcademica(ByVal vEstado As Boolean)
        ddlEsFacuDep.Visible = vEstado
        ddlEsFacuDep.Enabled = vEstado
    End Sub

    Protected Sub rdbDepartamento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbDepartamento.CheckedChanged
        If Me.rdbDepartamento.Checked = True Then
            Call CargarDepartamento()
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

    Protected Sub rdbFacultad_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbFacultad.CheckedChanged
        If Me.rdbFacultad.Checked = True Then
            Call Me.CargarFacultad()
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

    Protected Sub rdbEscuela_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbEscuela.CheckedChanged
        If Me.rdbEscuela.Checked = True Then
            Call CargarEscuela(0)
            HabilitarEsFacuDepCargaAcademica(True)
        End If
    End Sub

End Class
