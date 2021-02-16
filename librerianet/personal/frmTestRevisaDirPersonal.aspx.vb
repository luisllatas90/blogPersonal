'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class frmTestRevisaDirPersonal
    Inherits System.Web.UI.Page

    Dim codigo_per As Integer
    Dim codigo_pel As Integer
    Dim totalminutos As Integer
    Dim totalminutoslbl As Integer

    'Agreado 01/12/2011
    Dim vSemana As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'función que devuelve el codigo_pel vigente
            Dim obj As New clsPersonal
            codigo_pel = obj.ConsultarPeridoLaborable
            If ddlPersonal.SelectedIndex > -1 Then
                codigo_per = ddlPersonal.SelectedValue
            End If

            If Not IsPostBack Then
                MostrarOpciones(False)  'agregado 23/11/11
                cargarControles()
                CargaCentroCostosHP()

                If ddlPersonal.SelectedIndex > -1 Then
                    codigo_per = ddlPersonal.SelectedValue
                    consultarDatosGenerales()
                End If
                '---------------------------------------------------------------------------------------------------------
                'Verfica si hay trabajadores, 
                'en que sus horas registradas de tesis no coinciden con las horas de tesis que tienen alumnos matriculados
                MostrarEnvioCorreoTesis()
                '---------------------------------------------------------------------------------------------------------
                'Cargar Horas para el refrigerio de la tabla HorarioGeneral.
                CargarHorasRefrigerio()
                VerificarPeriodoLaborable() 'para mostrar el mensaje del horario administrativo.
                DeshabilitarControles()

                'Agregado xDguevara 05.11.2012
                cargaPersonal()
            End If

            lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
            If Trim(lblObservacion.Text) <> "" Then
                lblObservacion.Text = "Obs. " & lblObservacion.Text
            End If


            'CargaLeyenda()
            Me.CargarLeyenda()

            '02/11/2011
            Me.btnRefrescar.Attributes.Add("onclick", "location.reload();")
            Hr.Visible = False

            EstadoControlesInciales(False)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadoControlesInciales(ByVal vEstado As Boolean)
        Try
            btnActivaEnfermeria.Enabled = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarEnvioCorreoTesis()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal

            dts = obj.HorasTesisDiferentes()
            If dts.Rows.Count > 0 Then
                'Me.img_Email.Visible = True
                Me.btnTesis.Visible = True
            Else
                'Me.img_Email.Visible = False
                Me.btnTesis.Visible = False
            End If
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

    Private Sub CargaCentroCostosHP()
        'Carga CC del personal que tiene registrado horario personal
        Dim dts As New Data.DataTable
        Dim obj As New clsPersonal
        dts = obj.ConsultarCentroCostosHP()

        ddlCentroCostosHP.DataSource = dts
        ddlCentroCostosHP.DataTextField = "descripcion_Cco"
        ddlCentroCostosHP.DataValueField = "codigo_cco"
        ddlCentroCostosHP.DataBind()


        '***Agregado 09.06.2014 *************
        ddlCentroCostosHP.SelectedIndex = 1
        '************************************
    End Sub
    Private Sub cargaPersonal()
        Dim idPer As Integer
        Dim estado_hop As String
        idPer = Request.QueryString("id")
        ' idPer = 684
        Me.txtId.Text = idPer
        estado_hop = ddlFiltroEstadoHorario.SelectedValue
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        'Filtra personal por estado_hop y centrocostos        
        dts = obj.ConsultarPersonalDirectorPersonal_v2(estado_hop, ddlCentroCostosHP.SelectedValue)
        ddlPersonal.DataSource = dts
        ddlPersonal.DataTextField = "personal"
        ddlPersonal.DataValueField = "codigo_per"
        ddlPersonal.DataBind()
    End Sub

    Private Sub verificarDedicacion()
        'Verificar el tipo de dedicacion para validar horario de refrigerio        
        If lblDedicacion.Text = "TIEMPO COMPLETO" Or lblDedicacion.Text = "MEDIO TIEMPO" Or lblDedicacion.Text = "DEDICACION EXCLUSIVA" Or lblDedicacion.Text = "TIEMPO PARCIAL > 20 HRS." Then
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
        'Cargamos el combo tipo de actividad 03/08/11
        CargaTipoActividad()
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

            ElseIf ddlTipoActividad.SelectedValue = 5 Or ddlTipoActividad.SelectedValue = 24 Then '5 Investigacion   24 Gestión en Investigación   --Mneciosup 15/04/2019
                CargarDepartamento()
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)
            ElseIf ddlTipoActividad.SelectedValue = 23 Then '23 formación
                CargarDepartamento()
                HabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 1 Then  '1 Administrativo Institucional                
                DeshabilitarEsFacuDep()
                CargaCentroCostos(1, 0, "ApoyoInstu", codigo_pel)
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

            ElseIf ddlTipoActividad.SelectedValue = 8 Then '8 Horas Asistenciales Clinica USAT
                CargaCentroCostos(8, 0, "", codigo_pel)
                DeshabilitarEsFacuDep()
                Call MostrarOpciones(False)

            ElseIf ddlTipoActividad.SelectedValue = 16 Then '16 Centro Pre
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
        '13/08/2014 Tania Reyes Flores TRF Se agregó vigencia, fecha inicio y fin del contrato 
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        Dim fechaIniMostrar As Date 'TRF
        Dim fechaFinMostrar As Date 'TRF

        'Recuperar ciclo vigente actual        
        Dim codigo_Cac As Integer = obj.ConsultarCicloAcademicoVigente()

        'Recupera el total de horas de asesoria de tesis
        If codigo_Cac <> 0 Then
            'If (obj.ConsultarHorasAsesoria(codigo_per, codigo_Cac)) > 0 Then
            '    lblHorasAsesoria.Text = obj.ConsultarHorasAsesoria(codigo_per, codigo_Cac)
            'Else
            '    lblHorasAsesoria.Text = 0
            'End If   

            If (obj.ConsultarHorasAsesoria_V2(codigo_per, codigo_Cac)) > 0 Then
                '#### Modificacion de horas de tesis, asigandas segun cantidad de tesis a cargo y que el alumno se encuentre matriculado ####
                lblHorasAsesoria.Text = obj.ConsultarHorasAsesoria_V2(codigo_per, codigo_Cac)
            Else
                lblHorasAsesoria.Text = 0
            End If
        End If


        dts = obj.ConsultarDatosPersonales(codigo_per)
        Me.lblCeco.Text = dts.Rows(0).Item("descripcion_cco").ToString
        Me.lblNombre.Text = dts.Rows(0).Item("paterno").ToString & " " & dts.Rows(0).Item("materno").ToString & " " & dts.Rows(0).Item("nombres").ToString
        Me.lblTipo.Text = dts.Rows(0).Item("descripcion_Tpe").ToString
        Me.lblDedicacion.Text = dts.Rows(0).Item("descripcion_ded").ToString
        verificarDedicacion()

        If dts.Rows(0).Item("descripcion_ded").ToString.Trim = "TIEMPO PARCIAL < 20 HRS." Then
            DeshabilitarHorarioRefrigerio()
        End If

        Me.txtHoras.Text = dts.Rows(0).Item("horas").ToString

        AsignarEstadoHorario()

        Me.lblFechaIngreso.Text = dts.Rows(0).Item("fechaIni_Per").ToString

        If Not IsDBNull(dts.Rows(0).Item("foto")) Then
            If dts.Rows(0).Item("foto").ToString <> "" Then
                'Me.imgFoto.ImageUrl = "http://www.usat.edu.pe/campusvirtual/personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
                Me.imgFoto.ImageUrl = "../../personal/imgpersonal/" & dts.Rows(0).Item("foto").ToString
            Else
                Me.imgFoto.BackColor = Drawing.Color.Red
                imgFoto.AlternateText = "ATENCIÓN:    Suba su foto en el módulo de HOJA DE VIDA"
                imgFoto.ForeColor = Drawing.Color.White
                Me.imgFoto.ImageUrl = ""
            End If
        Else
            Me.imgFoto.BackColor = Drawing.Color.Red
            imgFoto.AlternateText = "ATENCIÓN:    Suba su foto en el módulo de HOJA DE VIDA"
            imgFoto.ForeColor = Drawing.Color.White
            Me.imgFoto.ImageUrl = ""
        End If

        'Si ha sido enviado al director no puede modificar datos del horario
        'If dts.Rows(0).Item("envioDirPersonal_Per").ToString = "True" Then
        '    DeshabilitarControles()
        'Else
        '    HabilitarControles()
        'End If
        'TRF

        '** Modificado por daguevara **, debido a que generaba error **'
        dts = obj.ConsultarDatosContrato(codigo_per)
        If dts.Rows.Count > 0 Then
            If dts.Rows(0).Item("vigencia_Ctr").ToString = "False" Then
                Me.lblEstado.ForeColor = Drawing.Color.Red
                Me.lblEstado.Text = "No Vigente"
            Else
                Me.lblEstado.ForeColor = Drawing.Color.Green
                Me.lblEstado.Text = "Vigente"
            End If
            fechaIniMostrar = dts.Rows(0).Item("fechaini_ctr").ToString()
            fechaFinMostrar = dts.Rows(0).Item("fechafin_ctr").ToString
            Me.lblFechaInicio.Text = fechaIniMostrar.Date
            Me.lblFechaFin.Text = fechaFinMostrar.Date
        End If

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
        'gvEditHorario.Columns(0).Visible = False    'ocultamos la columna codigo del grid
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
        obj.AsignarEstadoHorario(codigo_per, "P")
        consultarVistaHorario()

        'agregado 01/12/2011
        ConsultarHorasLectivas()

        consultarListaHorario()
        consultarTotalHorasSemanas()
        lblMensaje.Text = ""
        CompararHorasSemanales()
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

            'agregado 01/12/2011
            ConsultarHorasLectivas()


            consultarListaHorario()
            consultarTotalHorasSemanas()
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

            'agregado 01/12/2011
            ConsultarHorasLectivas()

            consultarListaHorario()
            consultarTotalHorasSemanas()
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

            'agregado 01/12/2012
            ConsultarHorasLectivas()

            consultarListaHorario()
            consultarTotalHorasSemanas()
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

            'agreado 01/12/2011
            ConsultarHorasLectivas()

            consultarListaHorario()
            consultarTotalHorasSemanas()
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

                'agregado 01/12/2011
                ConsultarHorasLectivas()

                consultarListaHorario()
                consultarTotalHorasSemanas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Function validaciones() As Boolean
        Try
            Dim obj As New clsPersonal
            Dim cc As Integer
            Dim esfacudep As Integer
            Dim vMensaje As String = ""

            Dim sw As Byte = 0

            'Valida que la hora de inicio y fin sean correctas.
            If ddlHoraInicio.Text >= ddlHoraFin.Text Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La hora de inicio debe ser menor que la hora fin, favor de corregir');", True)
                ddlHoraInicio.Focus()
                Exit Function
            End If

            'Valida que haya seleccionado un tipo de actividad.
            If ddlTipoActividad.Text = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el tipo de actividad.');", True)
                ddlTipoActividad.Focus()
                Exit Function
            End If

            'Seleccione Escuela  - Facultad - Departamento
            If ddlEsFacuDep.Enabled = True And ddlEsFacuDep.Text <> "" Then
                If ddlEsFacuDep.Text = 0 Then
                    Dim mensaje As String = "Seleccione " + lblEsFacuDep.Text
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & mensaje & "');", True)
                    Exit Function
                End If
            End If

            'Seleccionar centro de costo
            If ddlCentroCostos.Visible = True Then
                If ddlCentroCostos.SelectedValue = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el centro de costos.');", True)
                    ddlCentroCostos.Focus()
                    Exit Function
                End If
            End If

            'Para el tipo de actividad - Practicas Externas se le solicita ingresar el nombre de centro donde hace las practicas
            If ddlTipoActividad.SelectedValue = 7 Then
                If txtObservacion.Text = "" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar el nombre del centro de prácticas.');", True)
                    lblDescripcion.Text = "Centro de Práticas"
                    txtObservacion.Focus()
                    Exit Function
                End If
            Else
                lblDescripcion.Text = "Observación"
            End If


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
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Ud. no puede registrar este tipo de actividad, debido a que no ha importado o no cuenta con Horas Lectivas, verifique.');", True)
                    btnImportarHorarioAcademico.Focus()
                    Exit Function
                End If


                ''1. Cuando no ha registrado minutos para Gestion Academica
                If min_GestionAcademicaReg = 0 And min_MitadHrsLectivas > 0 Then
                    If tot_GestionAcademica > min_MitadHrsLectivas Then
                        vMensaje = vMensaje & "Ud. puede registrar como máximo [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ] para este tipo de actividad."
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & vMensaje & "');", True)
                        Exit Function
                    End If
                End If

                '2. Cuando el registro de horas es similar al permitido.
                If min_GestionAcademicaReg = min_MitadHrsLectivas Then
                    vMensaje = vMensaje & "Ud.  ya completo el máximo de horas permitidas para esta actividad [ " & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]"
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & vMensaje & "');", True)
                    Exit Function
                End If


                '3. Cuando el trabajador ya tiene registrado, horas de gestion academica.
                If (min_GestionAcademicaReg > 0 And min_MitadHrsLectivas > 0) And (min_GestionAcademicaReg <> min_MitadHrsLectivas) Then
                    If tot_GestionAcademica > min_MitadHrsLectivas Then
                        min_Faltantes = min_MitadHrsLectivas - min_GestionAcademicaReg
                        vMensaje = vMensaje & "Ud.  sólo puede registrar [ " & (min_Faltantes \ 60).ToString + " Hrs " & CType((min_Faltantes Mod 60), Integer).ToString & "Min ] de las  [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]" & " permitidas para este tipo de actividad."
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & vMensaje & "');", True)
                        Exit Function
                    End If
                End If
            End If
            '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            Dim abreviatura As String = obj.Consultarabreviatura_td(ddlTipoActividad.SelectedValue)
            '##########################-- VALIDACION PARA EL RANGO DE HORAS --###################################################
            '--------------------------------------------------------------------------------------------------------------------'
            'Mostrar una alerta si las horas elegidas se encuentran fuera del limite 07:00- 17:00 para todos,excepto para CC. Salud.
            'Ojo: es solo para informar al usuario, la insercion en la bd se realiza aparte
            If (obj.VerificarLimiteHoras("", ddlHoraInicio.Text, ddlHoraFin.Text, codigo_per, 0, abreviatura, 0, 0, "", "", 0, 0, 0, txtObservacion.Text.Trim, 0, "n")) = True Then
                vMensaje = vMensaje & "Las Actividades [Horas Lectivas/Asesoría de Tesis/ Practica Externa /Centro Pre] pueden estar fuera del Horario Administrativo, por favor verifique. "
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & vMensaje & "');", True)
                Exit Function
            End If

            If vMensaje = "" Then

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

                Dim mensajeAlerta As String = ""
                mensajeAlerta = obj.ValidarCrucesFinal(codigo_per, codigo_pel, ddlSemana.SelectedValue, ddlHoraInicio.Text, ddlHoraFin.Text, ddlDia.SelectedValue, abreviatura, cc, Me.txtObservacion.Text)
                lblMensaje.Text = mensajeAlerta

                obj.AsignarEstadoHorario(codigo_per, "P")
                consultarVistaHorario()

                'agregado 01/12/2011
                ConsultarHorasLectivas()
                consultarListaHorario()
                CompararHorasSemanales()
                consultarTotalHorasSemanas()
            End If

            sw = 1
            If (sw = 1) Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If validaciones() = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Se encontro un problema en el registro, favor de intentar nuevamente.');", True)
                Exit Sub
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('El registro fue guardado correctamente.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
    '    Try
    '        Dim obj As New clsPersonal
    '        Dim cc As Integer
    '        Dim esfacudep As Integer
    '        Dim vMensaje As String = ""

    '        If ddlHoraInicio.Text >= ddlHoraFin.Text Then
    '            vMensaje = "La hora de inicio debe ser menor que la hora fin."
    '            Dim myscript As String = "alert('" & vMensaje & "')"
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '            Exit Sub
    '        End If

    '        If ddlTipoActividad.Text = 0 Then
    '            vMensaje = vMensaje & "Seleccione el tipo de actividad."
    '            Dim myscript As String = "alert('" & vMensaje & "')"
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '            Exit Sub
    '        End If


    '        If ddlEsFacuDep.Enabled = True And ddlEsFacuDep.Text <> "" Then
    '            If ddlEsFacuDep.Text = 0 Then
    '                vMensaje = vMensaje & "Seleccione " & lblEsFacuDep.Text & ". "
    '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                Exit Sub
    '            End If
    '        End If

    '        If ddlCentroCostos.Visible = True Then
    '            If ddlCentroCostos.SelectedValue = 0 Then
    '                vMensaje = vMensaje & "Seleccione el centro de costos. "
    '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                Exit Sub
    '            End If
    '        End If

    '        '7 Practica Externa
    '        If ddlTipoActividad.SelectedValue = 7 Then
    '            'Validar observacion obligatoria
    '            If txtObservacion.Text = "" Then
    '                vMensaje = vMensaje & "Especificar centro de prácticas. "
    '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                Exit Sub

    '                'ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar el nombre del centro de prácticas.');", True)
    '                'lblDescripcion.Text = "Centro de Práticas"
    '                'lblDescripcion.ForeColor = Drawing.Color.Red
    '                'txtObservacion.Focus()
    '                'Exit Sub
    '            End If
    '        End If

    '        '++++++++++++++++++++++++++++++++++++++++++++++++++ x dguevara 01/12/2011 ++++++++++++++++++++++++++++++++++++++++++++++++++

    '        '## Validamos 01/12/2011: Recuperamos la mitad de las horas lectivas, que servira para el registro del tipo de actividad Gestion Academico.

    '        ' Entra, siempre y cuando el tipo de actividad sea Gestion Academica
    '        If ddlTipoActividad.SelectedValue = 11 Then
    '            Dim dts As New Data.DataTable
    '            Dim min_MitadHrsLectivas As Decimal
    '            Dim min_GestionAcademicaReg As Decimal
    '            Dim tot_GestionAcademica As Decimal
    '            Dim min_Faltantes As Decimal

    '            dts = obj.HoraGestionAcademicaSegunLectivas(codigo_per, codigo_pel, ddlSemana.SelectedValue)

    '            min_MitadHrsLectivas = dts.Rows(0).Item("Mitad_HorasLectivas")
    '            min_GestionAcademicaReg = dts.Rows(0).Item("GestionAcademicaReg")

    '            'Asignamos los valores de las horas a dato de tipo fecha/hora para poder hacer las operaciones x dguevara
    '            Dim min_PorRegistrar As Decimal
    '            Dim vHoraInicio As DateTime = ddlHoraInicio.Text
    '            Dim vHoraFin As DateTime = ddlHoraFin.Text
    '            min_PorRegistrar = DateDiff(DateInterval.Minute, vHoraInicio, vHoraFin)

    '            tot_GestionAcademica = min_PorRegistrar + min_GestionAcademicaReg


    '            ''0. Cuando no tiene o no ha importado horas de lectivas
    '            If min_MitadHrsLectivas = 0 Then
    '                vMensaje = vMensaje & "Ud. no puede registrar este tipo de actividad, debido a que no ha importado o no cuenta con Horas Lectivas, verifique."
    '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                Exit Sub
    '            End If


    '            ''1. Cuando no ha registrado minutos para Gestion Academica
    '            If min_GestionAcademicaReg = 0 And min_MitadHrsLectivas > 0 Then
    '                If tot_GestionAcademica > min_MitadHrsLectivas Then
    '                    vMensaje = vMensaje & "Ud. puede registrar como máximo [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ] para este tipo de actividad."
    '                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                    Exit Sub
    '                End If
    '            End If

    '            '2. Cuando el registro de horas es similar al permitido.
    '            If min_GestionAcademicaReg = min_MitadHrsLectivas Then
    '                vMensaje = vMensaje & "Ud.  ya completo el máximo de horas permitidas para esta actividad [ " & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]"
    '                Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                Exit Sub
    '            End If


    '            '3. Cuando el trabajador ya tiene registrado, horas de gestion academica.
    '            If (min_GestionAcademicaReg > 0 And min_MitadHrsLectivas > 0) And (min_GestionAcademicaReg <> min_MitadHrsLectivas) Then
    '                If tot_GestionAcademica > min_MitadHrsLectivas Then
    '                    min_Faltantes = min_MitadHrsLectivas - min_GestionAcademicaReg
    '                    vMensaje = vMensaje & "Ud.  sólo puede registrar [ " & (min_Faltantes \ 60).ToString + " Hrs " & CType((min_Faltantes Mod 60), Integer).ToString & "Min ] de las  [" & (CType(min_MitadHrsLectivas \ 60, Integer)).ToString + " Hrs " & CType((min_MitadHrsLectivas Mod 60), Integer).ToString & " Min ]" & " permitidas para este tipo de actividad."
    '                    Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '                    Exit Sub
    '                End If
    '            End If
    '        End If

    '        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    '        Dim abreviatura As String = obj.Consultarabreviatura_td(ddlTipoActividad.SelectedValue)

    '        '##########################-- VALIDACION PARA EL RANGO DE HORAS --###################################################
    '        '--------------------------------------------------------------------------------------------------------------------'
    '        'Mostrar una alerta si las horas elegidas se encuentran fuera del limite 07:00- 17:00 para todos,excepto para CC. Salud.
    '        'Ojo: es solo para informar al usuario, la insercion en la bd se realiza aparte

    '        If (obj.VerificarLimiteHoras("", ddlHoraInicio.Text, ddlHoraFin.Text, codigo_per, 0, abreviatura, 0, 0, "", "", 0, 0, 0, txtObservacion.Text.Trim, 0, "n")) = True Then
    '            vMensaje = vMensaje & "Las Actividades [Horas Lectivas/Asesoría de Tesis/ Practica Externa /Centro Pre] pueden estar fuera del Horario Administrativo, por favor verifique. "
    '            Dim myscript As String = "alert('" & vMensaje.Trim & "')"
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
    '            Exit Sub
    '        End If

    '        If vMensaje = "" Then

    '            'Obtener el centro costos de acuerdo al tipo de actividad
    '            If ddlTipoActividad.SelectedIndex <> -1 Then
    '                cc = ddlCentroCostos.SelectedValue
    '            End If

    '            If ddlEsFacuDep.Visible = False Then
    '                esfacudep = 0
    '            Else
    '                esfacudep = ddlEsFacuDep.SelectedValue
    '            End If

    '            'Verificar cruce de horarios                        
    '            'Obtener abreviatura

    '            Dim mensajeAlerta As String = ""

    '            mensajeAlerta = obj.ValidarCrucesFinal(codigo_per, codigo_pel, ddlSemana.SelectedValue, ddlHoraInicio.Text, ddlHoraFin.Text, ddlDia.SelectedValue, abreviatura, cc, Me.txtObservacion.Text)
    '            lblMensaje.Text = mensajeAlerta
    '            obj.AsignarEstadoHorario(codigo_per, "P")
    '            consultarVistaHorario()

    '            'agregado 01/12/2011
    '            ConsultarHorasLectivas()

    '            consultarListaHorario()
    '            CompararHorasSemanales()
    '            consultarTotalHorasSemanas()
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

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

        'agregado 01/12/2011
        ConsultarHorasLectivas()
        ModificarHrsLectivas(vSemana)
    End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Try
            Dim obj As New clsPersonal
            Dim dtshorariogeneral As New Data.DataTable
            dtshorariogeneral = obj.ConsultarHorarioGeneral()
            Dim dtspreenvio As New Data.DataTable
            Dim vmensaje As String = ""


            If ddlCentroCostosHP.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar un centro de Costo.');", True)
                Exit Sub
            End If

            If ddlPersonal.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Por favor seleccione un trabajador.');", True)
                Exit Sub
            End If

            If ddlEstadoHorario.SelectedValue = "CALIFICAR" Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Por favor seleccione una opción para Calificar el Horario.');", True)
                ddlEstadoHorario.Focus()
                Exit Sub
            End If


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

            '---------------
            'Observado
            If ddlEstadoHorario.SelectedValue = "O" Then
                If txtObservacionHorario.Text = "" And ddlEstadoHorario.SelectedValue = "O" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Por favor ingrese un comentario sobre el horario observado.');", True)
                    txtObservacionHorario.Focus()
                    Exit Sub
                End If

                'Modifica estadohorario
                obj.EnviarHorarioDirector(codigo_per, 0, ddlEstadoHorario.SelectedValue, Request.QueryString("id"))
                'Modifica en DatosPersonal: horarioObservado_Per = 1, ultimaobservacion y envia mail

                'Envia correo al trabajador
                obj.ObservarHorario(codigo_per, txtObservacionHorario.Text, _
                                    Me.lblNombre.Text, txtId.Text, _
                                    ddlEstadoHorario.SelectedValue, Request.QueryString("id"))

                'Guarda en bitacora
                obj.registrarBitacoraHorario(codigo_per, codigo_pel, txtId.Text)
                obj.RegistrarBitacoraObservacionHorario(codigo_per, codigo_pel, txtObservacionHorario.Text)

                'Actualiza el lbl de la ultima observacion
                lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
                AsignarEstadoHorario()
                'Se actualiza el grid de historico de envios
                ConsultarListaCambiosHorarios()
                ddlPersonal_SelectedIndexChanged(sender, e)
                Exit Sub
            End If

            'Conforme
            If ddlEstadoHorario.SelectedValue = "C" Then
                Dim horasdiarias As Integer
                'Valida limite de horas diarias = 12            
                For j As Integer = 1 To 6
                    Select Case j
                        Case 1
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "LU")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Lunes excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                        Case 2
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MA")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Martes excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                        Case 3
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "MI")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Miércoles excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                        Case 4
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "JU")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Jueves excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                        Case 5
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "VI")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Viernes excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                        Case 6
                            horasdiarias = obj.CalcularTotalHorasDiarias(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue, "SA")
                            If horasdiarias > 14 Then       'Cambiado a 14 por indicaciones de Hugo Saavedra (estaba en 12)
                                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La carga horaria del día Sábado excede el límite permitido (12 horas), por favor verifique.');", True)
                                Exit Sub
                            End If
                    End Select
                Next

                'Recorre las horas inicio y fin, por dia
                For i As Integer = 0 To dtshorariogeneral.Rows.Count - 1
                    Dim cantidadregistros As Integer
                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "LU")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If

                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "MA")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If

                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "MI")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If

                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "JU")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If

                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "VI")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If

                    cantidadregistros = obj.PreenvioHorarioPersonal(codigo_per, codigo_pel, ddlSemana.SelectedValue, dtshorariogeneral.Rows(i).Item("horaInicio"), dtshorariogeneral.Rows(i).Item("horaFin"), "SA")
                    If cantidadregistros > 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuración del horario presenta cruces, por favor verifique para poder finalizar y enviar.');", True)
                        Exit Sub
                    End If
                Next

                'Devuelve el total de registros del horario semestral, el total de registros de
                'horario semanal y un valor que indica si existen ambos
                Dim dtsSemanaSemestre As New Data.DataTable
                dtsSemanaSemestre = obj.ValidarHorarioSemestralySemanal(codigo_pel, codigo_per)

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
                        'lblMensaje.Text = "No puede registrar horario semestral y semanal, por favor verifique."
                        'lblMensaje.Visible = True
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No puede registrar horario semestral y semanal, por favor verifique.');", True)
                        Exit Sub
                    Else
                        If horariossemanal > 0 Then
                            'Verificar que haya registrado en todas las semanas                        
                            If lblHorasMensuales.Text <> totalHorasMensuales Then
                                lblMensaje.Text = "Debe registrar un horario de " & txtHoras.Text & " horas para cada semana, por favor verifique."
                                lblMensaje.Visible = True
                                Exit Sub
                            End If
                        End If
                    End If
                    ddlPersonal_SelectedIndexChanged(sender, e)
                End If

                'Validar registro horario
                Dim TotalRegistrosSemanal As Integer = dtsSemanaSemestre.Rows(0).Item("HorarioSemanal")
                Dim TotalRegistrosSemestral As Integer = dtsSemanaSemestre.Rows(0).Item("HorarioSemestre")

                If TotalRegistrosSemanal = 0 And TotalRegistrosSemestral = 0 Then
                    'lblMensaje.Text = "Debe registrar un horario, favor de verificar."
                    'lblMensaje.Visible = True
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Debe registrar un horario, favor de verificar.');", True)
                    Exit Sub
                End If

                'Validar registro horas tesis
                Dim resultado As String
                resultado = obj.ValidarRegistroHorasAsTesis(codigo_pel, codigo_per, lblHorasAsesoria.Text)
                If resultado <> "" Then
                    'Dim myscript As String = "alert('" & resultado & "')"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert(' " & resultado & "');", True)
                    Exit Sub
                End If


                'Validar seleccion de refrigerio
                'No se validara porque hay trabajadores a TC sin refrigerio (Ej. biblioteca)
                'If btnRefrigerio1.Enabled = True Then
                '    Dim existerefrigerio As Boolean = obj.ConsultarHorarioRefrigerio(codigo_per, codigo_pel)
                '    If existerefrigerio = False Then
                '        lblMensaje.Text = "Debe seleccionar un refrigerio, favor de verificar."
                '        lblMensaje.Visible = True
                '        Exit Sub
                '    End If
                'End If


                '---------------------------------------------------------------------------------------------
                'Agregado xD 29.03.2012 : Validar que las HorasSemanales = HoraSemanalesregistradas
                'txtHoras
                'lblHorasSemanales

                If CType(txtHoras.Text, Decimal) <> CType(lblHorasSemanales.Text, Decimal) Then
                    'Dim rpt As String = "Las horas semanales no coinciden con la distribución, favor de verificar"
                    'If rpt <> "" Then
                    'Dim myscript As String = "alert('" & rpt & "')"
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Las horas semanales asignadas por el área de personal no coinciden con la distribución de su horario, favor de verificar');", True)
                    obj.EnviarAlertaHorarios(codigo_per, lblNombre.Text, txtId.Text)
                    Exit Sub
                End If
            End If
            '---------------------------------------------------------------------------------------------


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
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Las horas semanales no coinciden con la distribución, favor de verificar');", True)
            Else
                'lblMensaje.Text = "Horario enviado satisfactoriamente"
                'lblMensaje.ForeColor = Drawing.Color.Blue
                'lblMensaje.Visible = True
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Horario enviado satisfactoriamente');", True)

                'Envia Horario.   
                'Modifica en DatosPersonal: totalhoras, envioDirPersonal_Per = 1, ultimaobservacion = "", fecha
                obj.EnviarHorarioDirector(codigo_per, CInt(Me.txtHoras.Text), ddlEstadoHorario.SelectedValue, Request.QueryString("id"))
                obj.registrarBitacoraHorario(codigo_per, codigo_pel, txtId.Text)

                '#############    Enviar mail de conformidad #######################################################
                obj.AprobarHorario(codigo_per, lblNombre.Text, txtId.Text)
                '###################################################################################################

                'Actualiza el lbl de la ultima observacion
                lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
                AsignarEstadoHorario()
                'Se actualiza el grid de historico de envios
                ConsultarListaCambiosHorarios()

                If ddlPersonal.SelectedValue <> 0 Then
                    '#### Mostrar estados: EnvioDirector_per / EnvioDirPersonal_per ####
                    EstadosEnvioHorario(ddlPersonal.SelectedValue)
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub AsignarEstadoHorario()
        Dim obj As New clsPersonal
        Dim dtsEstadoHorario As New Data.DataTable
        dtsEstadoHorario = obj.ConsultarEstadoHorario(codigo_per)
        Dim estadohorario As String = dtsEstadoHorario.Rows(0).Item("estado_hop").ToString

        If Not IsDBNull(estadohorario) Then
            Select Case estadohorario
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
        End If
    End Sub

    'Private Sub DeshabilitarControles()

    '    Dim i As Integer
    '    For i = 0 To gvEditHorario.Rows.Count - 1
    '        gvEditHorario.Rows(i).Cells(7).Enabled = False
    '    Next i

    '    txtHoras.Enabled = False
    '    btnBorrar.Enabled = False
    '    btnCopiarHorarioAdministrativo.Enabled = False
    '    btnImportarHorarioAcademico.Enabled = False
    '    btnAceptar.Enabled = False
    '    btnEnviar.Enabled = False
    '    btnConsideraciones.Enabled = False

    '    DeshabilitarControlesRefrigerio()

    '    ddlSemana.Enabled = False
    '    ddlEsFacuDep.Enabled = False
    '    ddlHoraFin.Enabled = False
    '    ddlHoraInicio.Enabled = False
    '    ddlDiaRefrigerio.Enabled = False
    '    ddlRefrigerioInicio.Enabled = False
    '    ddlCentroCostos.Enabled = False
    '    ddlDia.Enabled = False
    '    ddlTipoActividad.Enabled = False

    '    txtEncEscuela.Enabled = False
    '    txtObservacion.Enabled = False
    '    txtResEscuela.Enabled = False
    'End Sub


    'Private Sub HabilitarControles()

    '    Dim i As Integer
    '    For i = 0 To gvEditHorario.Rows.Count - 1
    '        gvEditHorario.Rows(i).Cells(0).Enabled = True
    '    Next i

    '    txtHoras.Enabled = True
    '    btnBorrar.Enabled = True
    '    btnCopiarHorarioAdministrativo.Enabled = True
    '    btnImportarHorarioAcademico.Enabled = True
    '    btnAceptar.Enabled = True
    '    btnEnviar.Enabled = True
    '    btnConsideraciones.Enabled = True

    '    HabilitarControlesRefrigerio()        

    '    ddlSemana.Enabled = True
    '    ddlEsFacuDep.Enabled = True
    '    ddlHoraFin.Enabled = True
    '    ddlHoraInicio.Enabled = True
    '    ddlDiaRefrigerio.Enabled = True
    '    ddlRefrigerioInicio.Enabled = True
    '    ddlCentroCostos.Enabled = True
    '    ddlDia.Enabled = True
    '    ddlTipoActividad.Enabled = True

    '    txtEncEscuela.Enabled = True
    '    txtObservacion.Enabled = True
    '    txtResEscuela.Enabled = True
    'End Sub

    Private Sub consultarVistaHorario()
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable

        dts = obj.ConsultarVistaHorario_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)
        gvVistaHorario.DataSource = dts
        gvVistaHorario.DataBind()

        ConsultarHorasTesisRegistradas()
        lblHorasSemanales.Text = obj.ConsultarTotalHorasSemana_v2(codigo_per, codigo_pel, Me.ddlSemana.SelectedValue)
        lblHorasMensuales.Text = obj.TotalHorasMes_v2(codigo_per, codigo_pel)
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
                Case "PE"
                    dts = obj.ConsultaTipoActividadPorAbreviatura("PE")
                    Dim color As String = dts.Rows(0).Item("color_td")
                    e.Row.Cells(i).BackColor = System.Drawing.ColorTranslator.FromHtml(color)
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

    'Private Sub CargaLeyenda()
    '    Dim obj As New clsPersonal
    '    Dim dts As New Data.DataTable

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("A")
    '    lblA.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("D")
    '    lblD.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblD.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("T")
    '    lblT.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblT.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("I")
    '    lblI.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblI.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("U")
    '    lblU.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblU.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("P")
    '    lblP.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("H")
    '    lblH.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblH.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("C")
    '    lblC.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblC.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("GR")
    '    lblGR.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblGR.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("CP")
    '    lblCP.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblCP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    dts = obj.ConsultaTipoActividadPorAbreviatura("CA")
    '    lblCA.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    lblCA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)


    '    '==================================================================================================================
    '    '------------------------------------------------------Anulados ---------------------------------------------------
    '    '==================================================================================================================

    '    'dts = obj.ConsultaTipoActividadPorAbreviatura("G")
    '    'lblG.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    'lblG.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    'dts = obj.ConsultaTipoActividadPorAbreviatura("F")
    '    'lblF.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    'lblF.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    'dts = obj.ConsultaTipoActividadPorAbreviatura("E")
    '    'lblE.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    'lblE.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    'dts = obj.ConsultaTipoActividadPorAbreviatura("GA")
    '    'lblGA.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    'lblGA.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    'dts = obj.ConsultaTipoActividadPorAbreviatura("GP")
    '    'lblGP.Text = dts.Rows(0).Item("descripcion_td").ToString
    '    'lblGP.BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td").ToString)

    '    '==================================================================================================================

    'End Sub


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

        'agregado
        ConsultarHorasLectivas()
    End Sub


    Protected Sub btnImportarHorarioAcademico_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportarHorarioAcademico.Click
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            Dim codigo_Cac As Integer = obj.ConsultarCicloAcademicoVigente()
            obj.ActualizarHorarioAcademico_Personal(codigo_per, codigo_pel, codigo_Cac, ddlSemana.SelectedValue)

            Response.Write("ddlSemana.SelectedValue: " + ddlSemana.SelectedValue.ToString)


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
                        'Verifica que si hay cruce de horarios de docencia con otros tipos.
                        mensajeCruceHorarios = obj.VerificarCruceHorarios(horainicio_adm, horafin_adm, horainicio_doc, horafin_doc, codigo_per, codigo_pel, dia, semana)
                        lblMensaje.Text = lblMensaje.Text + mensajeCruceHorarios
                    End If
                Next
            Else
                lblMensaje.Text = "Para el periodo actual, no posee carga académica. "
            End If

            consultarListaHorario()
            consultarVistaHorario()
            CompararHorasSemanales()
            consultarTotalHorasSemanas()

            'agregado 01/12/2012
            ConsultarHorasLectivas()

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
        ConsultarHorasLectivas()
        vSemana = ddlSemana.SelectedValue
        ModificarHrsLectivas(vSemana)
    End Sub

    Private Sub RangoFechasSemanas()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            Dim dtsMesVigente As New Data.DataTable
            dtsMesVigente = obj.MesVigente(codigo_pel, 0, "C")

            If dtsMesVigente.Rows.Count > 0 And dtsMesVigente.Rows(0).Item("mes_sec").ToString <> "No Definido" Then
                dts = obj.RangoFechasSemanasBitacora(ddlSemana.SelectedValue, dtsMesVigente.Rows(0).Item("mes_sec").ToString, codigo_pel)
                Dim FechaIni As String = dts.Rows(0).Item("desde_sec").ToString
                Dim FechaFin As String = dts.Rows(0).Item("hasta_sec").ToString

                lblFechas.Visible = True
                lblFechas.ForeColor = Drawing.Color.Blue
                lblFechas.Text = FechaIni & " hasta " & FechaFin
            Else
                lblFechas.Visible = True
                lblFechas.ForeColor = Drawing.Color.Red
                lblFechas.Text = "MES VIGENTE NO DEFINIDO"

                'Dim myscript As String = "alert('Por favor configure el mes viegente en el calendario computable.')"
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "myscript", myscript, True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Protected Sub ddlEstadoHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoHorario.SelectedIndexChanged
        If ddlEstadoHorario.SelectedValue = "O" Then
            lblObservacionHorario.Visible = True
            txtObservacionHorario.Visible = True
            btnEnviar.Text = "Observar"
        Else
            lblObservacionHorario.Visible = False
            txtObservacionHorario.Visible = False
            btnEnviar.Text = "Finalizar y Enviar"
        End If
    End Sub

    Protected Sub ddlPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersonal.SelectedIndexChanged
        Try
            If ddlPersonal.SelectedValue >= 1 Then
                HabilitarControles()
                LimpiarFormulario()
                Dim dtsOtros As New Data.DataTable
                Dim dtsSemanas As New Data.DataTable
                Dim obj As New clsPersonal

                codigo_per = ddlPersonal.SelectedValue

                'Pruebas ------------------------------
                'Response.Write(ddlPersonal.SelectedValue)
                '--------------------------------------

                '************************************************************************
                '***Requerimiento solicitado por rtimana : 09.06.2014 ***'
                If codigo_per > 0 Then
                    'dtsOtros = obj.ConsultarOtrosDatos("0", codigo_per)
                    'If dtsOtros.Rows.Count > 0 Then
                    '    '**A PLAZO INDETERMINADO - D.LEG. 728**'
                    '    If dtsOtros.Rows(0).Item("codigo_ctra").ToString = "01" Then
                    '        pnlContrato.Visible = False
                    '        lbltipocontrato.Text = dtsOtros.Rows(0).Item("nombre_ctra").ToString
                    '    Else
                    '        pnlContrato.Visible = True
                    '        dtsOtros = obj.ConsultarOtrosDatos("1", codigo_per)
                    '        If dtsOtros.Rows.Count > 0 Then
                    '            '**comentado **
                    '            'lblContrato.Text = dtsOtros.Rows(0).Item("Fechas").ToString
                    '            lbltipocontrato.Text = dtsOtros.Rows(0).Item("tipocontrato").ToString
                    '        Else
                    '            lblContrato.Text = "No registrado "
                    '            lbltipocontrato.Text = "-"
                    '        End If
                    '    End If
                    'End If

                    dtsOtros = obj.ConsultarOtrosDatos("2", codigo_per)
                    If dtsOtros.Rows.Count > 0 Then
                        If dtsOtros.Rows(0).Item("codigo_Cup") > 0 Then
                            Me.lblCarga.Text = "Sí"
                        Else
                            Me.lblCarga.Text = "No"
                        End If
                    Else
                        Me.lblCarga.Text = "No"
                    End If
                End If

                '************************************************************************



                Me.ddlSemana.Enabled = obj.EsCCSalud(codigo_per)

                'Carga el dropdownlist de las semanas
                dtsSemanas = obj.ConsultarTotalSemanas(codigo_pel)
                'Incluye semestre
                If dtsSemanas.Rows.Count > 0 Then
                    ddlSemana.DataSource = dtsSemanas
                    ddlSemana.DataTextField = "Semana"
                    ddlSemana.DataValueField = "numeroSemana_sec"
                    ddlSemana.DataBind()
                End If

                'Si su horario es semestral, apunta a "Semestral"
                If ddlSemana.Enabled = False Then
                    'ddlSemana.SelectedValue = 0
                    If (ddlSemana.Items.Count > 0) Then
                        ddlSemana.SelectedIndex = 0
                    End If
                Else
                    '-----------------------------------------------------------------------
                    'Este codigo, apunta a la semana segun la fecha del servidor.
                    If dtsSemanas.Rows.Count > 0 Then
                        Dim dtsSem As New Data.DataTable
                        dtsSem = obj.MuestraSemanaActual(codigo_per)
                        'Response.Write(dtsSem.Rows(0).Item("numeroSemana_sec"))
                        Dim vS As Integer = dtsSem.Rows(0).Item("numeroSemana_sec")
                        If vS <> -1 Then
                            ddlSemana.SelectedValue = vS
                        End If
                    End If
                    '-----------------------------------------------------------------------
                End If

                'Reporte de horas tesis
                Dim ObjCnx As New ClsConectarDatos
                Dim DatosHorasTesis As Data.DataTable
                ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                ObjCnx.AbrirConexion()
                DatosHorasTesis = ObjCnx.TraerDataTable("PER_ConsultarPersonalHorasTesis", "TE", codigo_pel, codigo_per)
                ObjCnx.CerrarConexion()


                If DatosHorasTesis.Rows.Count > 0 Then
                    Dim id_per As Integer
                    id_per = Request.QueryString("id")
                    btnReporteHorasTesis.Enabled = True
                    'btnReporteHorasTesis.Attributes.Add("OnClick", "javascript:AbrirPopUp('http://www.usat.edu.pe/rptusat/?/privados/personal/PER_DocentesAvancesTesis&id=" & id_per.ToString & "&codigo_pel=" & codigo_pel.ToString & "&codigo_per=" & codigo_per.ToString & "',500,800,1,1,1,0);return false;")
                    btnReporteHorasTesis.Attributes.Add("OnClick", "javascript:AbrirPopUp('//intranet.usat.edu.pe/rptusat/?/privados/personal/PER_DocentesAvancesTesis&id=" & id_per.ToString & "&codigo_pel=" & codigo_pel.ToString & "&codigo_per=" & codigo_per.ToString & "',500,800,1,1,1,0);return false;")
                Else
                    btnReporteHorasTesis.Enabled = False
                End If

                consultarVistaHorario()
                'agregado  01/12/2011
                ConsultarHorasLectivas()

                consultarListaHorario()

                consultarTotalHorasSemanas()


                ConsultarListaCambiosHorarios()



                consultarDatosGenerales()


                CompararHorasSemanales()


                RangoFechasSemanas()

                'Consulta la ultima observacion
                lblObservacion.Text = obj.ConsultarObservacion(codigo_per)
                If Trim(lblObservacion.Text) <> "" Then
                    lblObservacion.Text = "Obs. " & lblObservacion.Text
                End If
            End If

            If ddlPersonal.SelectedValue = 0 Then
                DeshabilitarControles()
            End If

            '########################################
            'agregado 
            vSemana = ddlSemana.SelectedValue
            '########################################

            '----## 
            ModificarHrsLectivas(vSemana)
            'Habilitamos la opcion para que el trabajador pueda modificar sus horas lectivas-------
            'If (lblEstadoHorario.Text = "Pendiente" Or lblEstadoHorario.Text = "Observado") Then
            'Hr.Visible = True
            'Else
            'Hr.Visible = False
            'End If
            '--------------------------------------------------------------------------------------

            VerificaCiclo()

            '#### Mostrar estados: EnvioDirector_per / EnvioDirPersonal_per ####
            EstadosEnvioHorario(codigo_per)
            CargarLeyenda()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarLeyenda()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.ListaActividadesLeyenda()
            If dts.Rows.Count > 0 Then
                gvLeyenda.DataSource = dts
                gvLeyenda.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ModificarHrsLectivas(ByVal vSemana As Integer)
        Dim obj As New clsPersonal
        Dim HoraMinuto As String = obj.ConsultarHorasCargaAcademica(codigo_per, codigo_pel)

        'Response.Write(HoraMinuto)
        'Response.Write("-")
        'Response.Write(lblTotalHorasLectivas.Text)

        If (lblTotalHorasLectivas.Text <> HoraMinuto Or lblEstadoHorario.Text = "Pendiente" Or lblEstadoHorario.Text = "Observado") Then
            Hr.Visible = True
            Hr.InnerHtml = "<a href='frmModificarHorasLectivas.aspx?codigo_pel=" & codigo_pel & "&codigo_per=" & codigo_per & "&Semana=" & vSemana & "&KeepThis=true&TB_iframe=true&height=350&width=570&modal=true' title='Clic Aqui Modifcar ' class='thickbox'>MODIFICAR HRAS LECTIVAS<a/>"
        End If
    End Sub


    Private Sub LimpiarFormulario()
        Try
            ddlDiaRefrigerio.SelectedValue = "LU"
            ddlRefrigerioInicio.SelectedValue = "--Seleccione--"
            ddlDia.SelectedValue = "LU"
            ddlHoraInicio.SelectedValue = "05:00"
            ddlHoraFin.SelectedValue = "05:00"
            ddlTipoActividad.SelectedValue = 0
            ddlEsFacuDep.SelectedValue = 0
            ddlCentroCostos.SelectedValue = 0
            txtEncEscuela.Text = ""
            txtResEscuela.Text = ""
            txtObservacion.Text = ""
            lblMensaje.Text = ""
            lblObservacion.Text = ""
            txtObservacionHorario.Text = ""
            lblObservacionHorario.Visible = False
            txtObservacionHorario.Visible = False
            ddlEstadoHorario.SelectedValue = "CALIFICAR"
            btnEnviar.Text = "Finalizar y Enviar"

            'limpia datos:
            lblCarga.Text = ""
            lbltipocontrato.Text = ""
            lblCarga.Text = ""
            lblEstado.Text = ""
            lblFechaInicio.Text = ""
            lblFechaFin.Text = ""


        Catch ex As Exception
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

    Protected Sub btnActivarBiblioteca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivarBiblioteca.Click
        Dim obj As New clsPersonal
        obj.HabilitarModificarHorarioBiblioteca()
    End Sub

    Protected Sub btnActivarCCSalud_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivarCCSalud.Click
        Dim obj As New clsPersonal
        obj.HabilitarModificarHorarioPersonalSalud("CC")
    End Sub

    Protected Sub cmdActivarCIS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActivarCIS.Click
        Dim obj As New clsPersonal
        obj.HabilitarModificarHorarioPersonalSalud("CI")
    End Sub

    Protected Sub cmdActivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdActivar.Click
        Dim obj As New clsPersonal
        Dim dts As New Data.DataTable
        If ddlPersonal.SelectedIndex > -1 Then
            dts = obj.HabilitarModificarHorarioPersonal(codigo_per, "P", Request.QueryString("id"))
            ddlPersonal_SelectedIndexChanged(sender, e)
        End If
    End Sub


    Protected Sub ddlCentroCostosHP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCentroCostosHP.SelectedIndexChanged

        'Response.Write(ddlCentroCostosHP.SelectedValue)

        cargaPersonal()
        'Limpiar grids
        gvEditHorario.DataSource = Nothing
        gvEditHorario.DataBind()
        gvVistaHorario.DataSource = Nothing
        gvVistaHorario.DataBind()
        gvListaCambios.DataSource = Nothing
        gvListaCambios.DataBind()

        lblNombre.Text = ""
        lblCeco.Text = ""
        lblDedicacion.Text = ""
        lblFechaIngreso.Text = ""
        lblTipo.Text = ""
        txtHoras.Text = ""
        lblHorasAsesoria.Text = ""
        lblEstadoHorario.Text = ""
        imgFoto.ImageUrl = ""
        lblHorasSemanales.Text = ""
        lblFechas.Text = ""
        lblHorasMensuales.Text = ""

    End Sub


    Protected Sub ddlFiltroEstadoHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFiltroEstadoHorario.SelectedIndexChanged
        'Response.Write(ddlFiltroEstadoHorario.SelectedValue)
        If ddlCentroCostosHP.SelectedValue > -1 Then
            cargaPersonal()
        End If
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

        'btnAceptar.Enabled = False
        btnAgregar.Enabled = False

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

    Private Sub HabilitarControles()

        Dim i As Integer
        For i = 0 To gvEditHorario.Rows.Count - 1
            gvEditHorario.Rows(i).Cells(7).Enabled = True
        Next i

        txtHoras.Enabled = True
        btnBorrar.Enabled = True
        btnCopiarHorarioAdministrativo.Enabled = True
        btnImportarHorarioAcademico.Enabled = True

        'btnAceptar.Enabled = True
        btnAgregar.Enabled = True

        btnEnviar.Enabled = True
        btnConsideraciones.Enabled = True

        DeshabilitarControlesRefrigerio()

        ddlSemana.Enabled = True
        ddlEsFacuDep.Enabled = True
        ddlHoraFin.Enabled = True
        ddlHoraInicio.Enabled = True
        ddlDiaRefrigerio.Enabled = True
        ddlRefrigerioInicio.Enabled = True
        ddlCentroCostos.Enabled = True
        ddlDia.Enabled = True
        ddlTipoActividad.Enabled = True
        txtEncEscuela.Enabled = True
        txtObservacion.Enabled = True
        txtResEscuela.Enabled = True
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

    Protected Sub chkActivarBiblioteca_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActivarBiblioteca.CheckedChanged

        If chkActivarBiblioteca.Checked = True Then btnActivarBiblioteca.Enabled = True
        If chkActivarBiblioteca.Checked = False Then btnActivarBiblioteca.Enabled = False
    End Sub

    Protected Sub chkActivarCCSalud_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActivarCCSalud.CheckedChanged
        If chkActivarCCSalud.Checked = True Then btnActivarCCSalud.Enabled = True
        If chkActivarCCSalud.Checked = False Then btnActivarCCSalud.Enabled = False
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

    Private Sub HabilitarEsFacuDepCargaAcademica(ByVal vEstado As Boolean)
        ddlEsFacuDep.Visible = vEstado
        ddlEsFacuDep.Enabled = vEstado
    End Sub

    Private Sub MostrarOpciones(ByVal vEstado As Boolean)
        'Me.lblEsFacuDep.Visible = Not vEstado
        rdbDepartamento.Visible = vEstado
        rdbEscuela.Visible = vEstado
        rdbFacultad.Visible = vEstado
    End Sub


    'codigo para habilitar o deshabilitar los refrigerios dependiendo del ciclo 02/01/2012
    Private Sub VerificaCiclo()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            dts = obj.VerificaCiclo()

            If dts.Rows(0).Item("Rpta").ToString = "S" Then
                Me.btnRefrigerio.Enabled = False
                Me.btnRefrigerio1.Enabled = False
                Me.btnRefrigerio2.Enabled = False
                Me.btnRefrigerio3.Enabled = False
                Me.btnRefrigerio4.Enabled = False

                ddlDiaRefrigerio.Enabled = False
                ddlRefrigerioInicio.Enabled = False
            Else
                Me.btnRefrigerio.Enabled = True
                Me.btnRefrigerio1.Enabled = True
                Me.btnRefrigerio2.Enabled = True
                Me.btnRefrigerio3.Enabled = True
                Me.btnRefrigerio4.Enabled = True
                ddlRefrigerioInicio.Enabled = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EstadosEnvioHorario(ByVal codigo_per As Integer)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsPersonal
            dts = obj.VerificarEstadoEnvioHorario(codigo_per)

            If dts.Rows(0).Item("envioDirector_Per") = 0 Then
                lblEnvioDirector_per.ForeColor = Drawing.Color.Green
                Me.lblEnvioDirector_per.Text = "Habilitado"
            Else
                lblEnvioDirector_per.ForeColor = Drawing.Color.Red
                Me.lblEnvioDirector_per.Text = "Bloqueado"
            End If


            If dts.Rows(0).Item("envioDirPersonal_Per") = 0 Then
                Me.lblEnvioDirPersonal_per.ForeColor = Drawing.Color.Red
                lblEnvioDirPersonal_per.Text = "No"
            Else
                lblEnvioDirPersonal_per.ForeColor = Drawing.Color.Green
                lblEnvioDirPersonal_per.Text = "Sí"
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvLeyenda_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLeyenda.RowDataBound
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable

            If e.Row.RowType = DataControlRowType.DataRow Then
                Select Case e.Row.Cells(2).Text
                    Case "T"    'Asesoría de Tesis
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "CA" 'Carga Académico - Administrativa
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "CP" 'Centro Pre
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "C"    'Cooperación interinstitucional
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "A"    'Gestión Apoyo Institucional
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "P"    'Supervisión de Prácticas Pre Profesionales
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "H"    'Horas asistenciales Clínica Usat
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "D"    'Horas Lectivas
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "I"    'Investigación e Innovación
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "R"    'Refrigerio
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "GR"   'Responsabilidad Social
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "U"    'Tutoría al Estudiante
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "PE"   'Prácticas Externas
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "G"   'Gestión Académica
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))
                    Case "FP"   'Formación Permanente
                        dts = obj.ColorActividad(e.Row.Cells(2).Text)
                        e.Row.Cells(2).BackColor = System.Drawing.ColorTranslator.FromHtml(dts.Rows(0).Item("color_td"))

                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub chkActivaEnfermeria_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActivaEnfermeria.CheckedChanged
        If chkActivaEnfermeria.Checked = True Then btnActivaEnfermeria.Enabled = True
        If chkActivaEnfermeria.Checked = False Then btnActivaEnfermeria.Enabled = False
    End Sub

    Protected Sub btnActivaEnfermeria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivaEnfermeria.Click
        Dim obj As New clsPersonal
        obj.HabilitarModificarHorarioPersonalSalud("EN")
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

End Class
