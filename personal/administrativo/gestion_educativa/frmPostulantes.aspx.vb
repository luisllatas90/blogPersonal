Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf
Imports System.Collections.Generic

Partial Class administrativo_pec_test_frmPostulantes
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Private mo_Cnx As New ClsConectarDatos
    Private mo_RepoAdmision As New ClsAdmision
    Private mo_Evento As New clsEvento

    Private ms_CodigoTfu As String = ""
    Private ms_CodigoPer As String = ""
    Private ms_CodigoTest As String = ""
    Private ms_AccionInscripcion As String = "N"

    Public mn_FilasPostu As Integer = 0
    Public mn_FilasPorPaginaPostu As Integer = 0
    Public mn_PaginaPostu As Integer = 1
    Public mn_RangoPaginasPostu As Integer = 5 'Máxima cantidad de botones a la derecha e izquierda en el paginador
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If

        ms_CodigoTest = Request.QueryString("mod")
        ms_CodigoPer = Request.QueryString("id")
        ms_CodigoTfu = Request.QueryString("ctf")

        urlMod.Value = ms_CodigoTest
        urlId.Value = ms_CodigoPer
        urlCtf.Value = ms_CodigoTfu

        InicializarControles()
        LimpiarMensajeServidor()
        LimpiarMensajeValServ()

        If Not IsPostBack Then
            CargarCombos()
            ViewState.Item("dtPostulantes") = Nothing
            ViewState.Item("dtIngresantes") = Nothing

            'andy.diaz 03/07/2019 -> Oculto el botón de inscripción simple
            btnRegistrarPostulante.Visible = False
        Else
            RefrescarGrillaPostulantes()
            RefrescarGrillaIngresantes()

            'Obtengo de una vez la cantidad de filas para no estar haciendo varias llamadas
            mn_FilasPostu = mo_RepoAdmision.ContarPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked)
            mn_FilasPorPaginaPostu = cmbFilasPorPaginaPostu.SelectedValue
            mn_PaginaPostu = IIf(ViewState.Item("PaginaPostu") IsNot Nothing, ViewState.Item("PaginaPostu"), 1)
            GenerarPaginador(mn_RangoPaginasPostu)

            Dim eventTarget As String = IIf(Me.Request("__EVENTTARGET") IsNot Nothing, Me.Request("__EVENTTARGET"), "")
            If eventTarget = "udpGenerarCargo" Then
                CargarIframeGenerarCargo()
            End If

            'Métodos llamados por AJAX
            Dim ls_Method As String = Request.Form("method")
            If Not String.IsNullOrEmpty(ls_Method) Then
                Select Case ls_Method
                    Case "ExportarInteresados"
                        ExportarInteresados()
                    Case "ExportarIngresantes"
                        ExportarIngresantes()
                End Select
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ViewState.Item("PaginaPostu") = mn_PaginaPostu
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        EstadoBotonesAccion()
        LimpiarFiltros()
        udpFiltrosIngresantes.Update()

        If cmbCentroCosto.SelectedValue <> "-1" Then
            mn_PaginaPostu = 1
            CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
            CargarDatosEvento(cmbCentroCosto.SelectedValue)
            udpPostulantes.Update()
        Else
            LimpiarGrillaPostulantes()
            LimpiarDatosEvento()
        End If

        LimpiarGrillaIngresantes()
        GenerarPaginador(mn_RangoPaginasPostu)
    End Sub

    Private Sub LimpiarFiltros()
        'Postulante
        txtFiltroDNI.Text = ""

        'Ingresante
        cmbCicloAcademico.SelectedValue = "-1"
        cmbModalidadIngreso.SelectedValue = "-1"
        cmbCarreraProfesional.SelectedValue = "-1"
    End Sub

    Protected Sub chkMostrarSinDeuda_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMostrarSinDeuda.ServerChange
        mn_PaginaPostu = 1
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Protected Sub cmbEstadoAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstadoAlumno.SelectedIndexChanged
        mn_PaginaPostu = 1
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Protected Sub cmbFilasPorPaginaPostu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFilasPorPaginaPostu.SelectedIndexChanged
        mn_PaginaPostu = 1
        mn_FilasPorPaginaPostu = cmbFilasPorPaginaPostu.SelectedValue
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Protected Sub grwPostulantes_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwPostulantes.DataBound
        ViewState.Item("dtPostulantes") = grwPostulantes.DataSource
    End Sub

    Protected Sub grwPostulantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPostulantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ls_codigoPso As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("codigo_pso")
            Dim ls_codigoAlu As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("cli")
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwPostulantes.Columns.Count

            _cellsRow(0).Text = ln_Index

            'Ver movimientos
            Dim lo_btnInfo As New HtmlButton()
            With lo_btnInfo
                .ID = "btnInfoPostulante" & ln_Index
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-secondary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-search-plus'></i>"
                AddHandler .ServerClick, AddressOf btnInfoPostulante_Click
            End With
            _cellsRow(ln_Columnas - 7).Controls.Add(lo_btnInfo)

            'Editar postulante
            Dim lo_btnEditar As New HtmlButton
            With lo_btnEditar
                .ID = "btnEditarPostulante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-edit'></i>"
                AddHandler .ServerClick, AddressOf btnEditarPostulante_Click
            End With
            _cellsRow(ln_Columnas - 6).Controls.Add(lo_btnEditar)

            'Imprimir ficha
            Dim lo_btnImprimir As New HtmlButton
            With lo_btnImprimir
                .ID = "btnImprimirFichaIngresante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-print'></i>"
            End With
            _cellsRow(ln_Columnas - 5).Controls.Add(lo_btnImprimir)

            'Requisitos
            Dim lo_btnRequisitos As New HtmlButton
            With lo_btnRequisitos
                .ID = "btnRequisitosAdmision" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-check-square'></i>"
                AddHandler .ServerClick, AddressOf btnRequisitosAdmision_Click
            End With
            _cellsRow(ln_Columnas - 4).Controls.Add(lo_btnRequisitos)

            'Reporte convenio
            Dim lo_BtnConvenio As New HtmlButton
            Dim ls_CodigoCco As String = cmbCentroCosto.SelectedValue
            Dim ls_Dominio As String = mo_RepoAdmision.ObtenerVariableGlobal("parhReportServer")
            Dim ls_Path As String = ls_Dominio & "PRIVADOS/PENSIONES/PEN_DeudasxCco&codigo_cco=" & ls_CodigoCco & "&codigo_pso=" & ls_codigoPso
            With lo_BtnConvenio
                .ID = "btnImprimirConvenio" & ln_Index
                .Attributes.Add("class", "btn btn-dark btn-sm")
                .Attributes.Add("type", "button")
                .Attributes.Item("onClick") = "window.open('" & ls_Path & "')"
                .InnerHtml = "<i class='fa fa-file-pdf'></i>"
            End With
            _cellsRow(ln_Columnas - 3).Controls.Add(lo_BtnConvenio)

            'Generar Cargo 
            Dim lo_btnGenerarCargo As New HtmlButton
            With lo_btnGenerarCargo
                .ID = "btnGenerarCargo" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-coins'></i>"
                AddHandler .ServerClick, AddressOf btnGenerarCargo_Click
            End With
            _cellsRow(ln_Columnas - 2).Controls.Add(lo_btnGenerarCargo)

            'Anular Cargo 
            Dim lo_btnAnularCargo As New HtmlButton
            With lo_btnAnularCargo
                .ID = "btnAnularCargo" & ln_Index
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-danger btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-minus-circle'></i>"
                AddHandler .ServerClick, AddressOf btnAnularCargo_Click
            End With
            _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnAnularCargo)

            grwPostulantes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    'Eventos delegados
    Private Sub btnInfoPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoPso As String = button.Attributes("data-pso")
        CargarGrillaMovimientosPostulante(ls_CodigoPso, cmbCentroCosto.SelectedValue)
        udpMovimientosPostulante.Update()
    End Sub

    Public Sub btnPagePostulantes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        mn_PaginaPostu = button.Attributes("data-pagina")
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Private Sub btnListarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbCentroCosto.SelectedValue <> "-1" Then
            mn_PaginaPostu = 1
            CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, chkMostrarSinDeuda.Checked, mn_PaginaPostu, mn_FilasPorPaginaPostu)
            udpPostulantes.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Private Sub btnRegistrarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbCentroCosto.SelectedValue <> "-1" Then
            ifrmInscripcionPostulante.Attributes("src") = "frmInscripcionInteresadoAdmision.aspx?modal=true&id=" & ms_CodigoPer & "&cco=" & cmbCentroCosto.SelectedValue & "&test=" & ms_CodigoTest & "&accion=N"
            udpInscripcionPostulante.Update()
            udpMensajeServidorBody.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Protected Sub btnRegistrarPostulanteCompleto_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPostulanteCompleto.ServerClick
        If cmbCentroCosto.SelectedValue <> "-1" Then
            ifrmMantenimientoPostulante.Attributes("src") = "frmActualizarDatosPostulante.aspx?modal=true&mod=" & ms_CodigoTest & "&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&cco=" & cmbCentroCosto.SelectedValue & "&accion=N&alu=0"
            udpMantenimientoPostulante.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Private Sub btnEditarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        ifrmMantenimientoPostulante.Attributes("src") = "frmActualizarDatosPostulante.aspx?modal=true&mod=" & ms_CodigoTest & "&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&cco=" & cmbCentroCosto.SelectedValue & "&alu=" & ls_CodigoAlu
        udpMantenimientoPostulante.Update()
    End Sub

    Private Sub btnGenerarCargo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        Dim lb_ValidaDeuda As Boolean = True
        Dim ln_UsuarioReg As Integer = Request.QueryString("id")

        Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.GenerarDeudaInscripcionPreGrado(ls_CodigoAlu, lb_ValidaDeuda, ln_UsuarioReg)

        Dim ls_Titulo As String = IIf(lo_Respuesta.Item("rpta") <> "1", "Advertencia", "Respuesta")
        GenerarMensajeServidor(ls_Titulo, lo_Respuesta.Item("rpta"), lo_Respuesta.Item("msg"))
    End Sub

    Private Sub btnAnularCargo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoPso As String = button.Attributes("data-pso")
        ifrmAnularCargo.Attributes("src") = "frmSolicitarAnulacion.aspx?modal=true&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&pso=" & ls_CodigoPso & "&cco=" & cmbCentroCosto.SelectedValue
        udpAnularCargo.Update()
    End Sub

    Private Sub btnEnviarCorreoAdmision_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodUniversitario As String = button.Attributes("data-cod-uni")
        Dim ls_CodigoCco As String = button.Attributes("data-cco")
        Dim blnResultado As Boolean = False

        blnResultado = mo_Evento.EnviaClavesAlumno(ls_CodUniversitario, ls_CodigoCco)
        If blnResultado Then
            GenerarMensajeServidor("Respuesta", 1, "Correo enviado correctamente")
        Else
            GenerarMensajeServidor("Advertencia", 0, "No se pudo enviar el correo electronico al alumno")
        End If
    End Sub

    Private Sub btnRequisitosAdmision_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        ifrmRequisitosAdmision.Attributes("src") = "frmRequisitosAdmision.aspx?modal=true" & "&id=" & ms_CodigoPer & "&alu=" & ls_CodigoAlu
        udpRequisitosAdmision.Update()
    End Sub

    Private Sub btnImprimirConvenio_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoCco As String = button.Attributes("data-cco")
        Dim ls_CodigoPso As String = button.Attributes("data-pso")
        Dim ls_Dominio As String = mo_RepoAdmision.ObtenerVariableGlobal("parhReportServer")
        Response.Redirect(ls_Dominio & "PRIVADOS/PENSIONES/PEN_DeudasxCco&codigo_cco=" & ls_CodigoCco & "&codigo_pso=" & ls_CodigoPso)
    End Sub

    Private Sub btnListarIngresante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbCentroCosto.SelectedValue <> "-1" Then
            CargarGrillaIngresantes()
            udpIngresantes.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Protected Sub grwIngresantes_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwIngresantes.DataBound
        ViewState.Item("dtIngresantes") = grwIngresantes.DataSource
    End Sub

    Protected Sub grwIngresantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwIngresantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ls_codigoAlu As String = grwIngresantes.DataKeys(e.Row.RowIndex).Values.Item("codigo_Alu")
            Dim ls_codUniversitario As String = grwIngresantes.DataKeys(e.Row.RowIndex).Values.Item("codUniversitario")
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwIngresantes.Columns.Count

            _cellsRow(0).Text = ln_Index

            'Editar postulante
            Dim lo_btnEditar As New HtmlButton
            With lo_btnEditar
                .ID = "btnEditarPostulante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-edit'></i>"
                AddHandler .ServerClick, AddressOf btnEditarPostulante_Click
            End With
            _cellsRow(ln_Columnas - 4).Controls.Add(lo_btnEditar)

            'Correo
            Dim lo_btnEnviarCorreo As New HtmlButton
            With lo_btnEnviarCorreo
                .ID = "btnEnviarCorreoAdmision" & ln_Index
                .Attributes.Add("data-cod-uni", ls_codUniversitario)
                .Attributes.Add("data-cco", cmbCentroCosto.SelectedValue)
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-envelope'></i>"
                AddHandler .ServerClick, AddressOf btnEnviarCorreoAdmision_Click
            End With
            _cellsRow(ln_Columnas - 3).Controls.Add(lo_btnEnviarCorreo)

            'Imprimir ficha
            Dim lo_btnImprimir As New HtmlButton
            With lo_btnImprimir
                .ID = "btnImprimirFichaIngresante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-print'></i>"
            End With
            _cellsRow(ln_Columnas - 2).Controls.Add(lo_btnImprimir)

            'Requisitos
            Dim lo_btnRequisitos As New HtmlButton
            With lo_btnRequisitos
                .ID = "btnRequisitosAdmision" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-dark btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-check-square'></i>"
                AddHandler .ServerClick, AddressOf btnRequisitosAdmision_Click
            End With
            _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnRequisitos)

            grwIngresantes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnAnularCargosEvento_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnularCargosEvento.ServerClick
        Try
            Dim codigoCco As Integer = cmbCentroCosto.SelectedValue
            If codigoCco = -1 Then
                GenerarMensajeServidor("Error", -1, "Seleccione un centro de costso")
                Exit Sub
            End If

            Dim rpta As Integer = 0 'warning
            Dim mensaje As String = "", advertencias As String = ""

            'Validar si hay algún ingresante
            Dim tipoConsulta As String = "COD"
            Dim dtIngresantes As Data.DataTable = mo_RepoAdmision.ListarIngresantesPorCentroCosto(tipoConsulta, codigoCco)
            If dtIngresantes.Rows.Count = 0 Then
                advertencias = "<li>No se ha encontrado ningún ingresante en este centro de costos. La operación afectará a todos los inscritos.</li>"
            End If

            If advertencias <> "" Then
                advertencias = "Advertencia: <br><ul>" & advertencias & "</ul>"
            End If

            mensaje = mensaje & advertencias
            mensaje = mensaje & "Se procederá a anular todos los cargos <b>pendientes</b> generados por concepto de inscripción para este centro de costo. "

            Dim procOperacion As String = "C" 'CONTAR
            Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.AnularCargosPendientesPorCentroCostos(procOperacion, codigoCco)
            If lo_Respuesta.Item("msg") <> "" Then
                mensaje = mensaje & "<br><b><i>" & lo_Respuesta.Item("msg") & "</i></b>"
            End If

            mensaje = mensaje & "<br><br><b>¿Está seguro de continuar con la operación?</b>"

            MostrarValidacionServidor("Confirmar operación", rpta, mensaje, "anularCargos")
        Catch ex As Exception
            GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnInactivarInscritosEvento_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInactivarInscritosEvento.ServerClick
        Try
            Dim codigoCco As Integer = cmbCentroCosto.SelectedValue
            If codigoCco = -1 Then
                GenerarMensajeServidor("Error", -1, "Seleccione un centro de costso")
                Exit Sub
            End If

            Dim rpta As Integer = 0 'warning
            Dim mensaje As String = "", advertencias As String = ""

            'Validar si hay algún ingresante
            Dim tipoConsulta As String = "COD"
            Dim dtIngresantes As Data.DataTable = mo_RepoAdmision.ListarIngresantesPorCentroCosto(tipoConsulta, codigoCco)
            If dtIngresantes.Rows.Count = 0 Then
                advertencias = "<li>No se ha encontrado ningún ingresante en este centro de costos. La operación afectará a todos los inscritos.</li>"
            End If

            If advertencias <> "" Then
                advertencias = "Advertencia: <br><ul>" & advertencias & "</ul>"
            End If

            mensaje = mensaje & advertencias
            mensaje = mensaje & "Se procederá a inactivar a todos los postulantes que no hayan ingresado, los accesitarios segirán activos y si no logran ingresar deberán ser inactivados individualmente. "

            Dim procOperacion As String = "C" 'CONTAR
            Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.InactivarNoIngresantesPorCentroCostos(procOperacion, codigoCco)
            If lo_Respuesta.Item("msg") <> "" Then
                mensaje = mensaje & "<br><b><i>" & lo_Respuesta.Item("msg") & "</i></b>"
            End If

            mensaje = mensaje & "<br><br><b>¿Está seguro de continuar con la operación?</b>"

            MostrarValidacionServidor("Confirmar operación", rpta, mensaje, "inactivarNoIngresantes")
        Catch ex As Exception
            GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnValServProcesar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValServProcesar.ServerClick
        Try
            Dim lo_Respuesta As Dictionary(Of String, String)
            Dim operacion As String = divValServParametros.Attributes.Item("data-operacion")
            Dim codigoCco As String = cmbCentroCosto.SelectedValue
            Dim procOperacion As String = ""

            Select Case operacion
                Case "anularCargos"
                    procOperacion = "A" 'ANULAR
                    lo_Respuesta = mo_RepoAdmision.AnularCargosPendientesPorCentroCostos(procOperacion, codigoCco)
                    DevolverRespuestaValServ(lo_Respuesta.Item("rpta"), lo_Respuesta.Item("msg"))
                Case "inactivarNoIngresantes"
                    procOperacion = "I" 'INACTIVAR
                    lo_Respuesta = mo_RepoAdmision.InactivarNoIngresantesPorCentroCostos(procOperacion, codigoCco)
                    DevolverRespuestaValServ(lo_Respuesta.Item("rpta"), lo_Respuesta.Item("msg"))
            End Select
        Catch ex As Exception
            GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub


#End Region

#Region "Métodos"
    Private Sub InicializarControles()
        AddHandler btnListarPostulante.ServerClick, AddressOf btnListarPostulante_Click
        AddHandler btnRegistrarPostulante.ServerClick, AddressOf btnRegistrarPostulante_Click
        AddHandler btnListarIngresante.ServerClick, AddressOf btnListarIngresante_Click
    End Sub

    Private Sub LimpiarMensajeServidor()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = ""
        udpMensajeServidorParametros.Update()
    End Sub

    Private Sub LimpiarMensajeValServ()
        divValServParametros.Attributes.Item("data-mostrar") = ""
        divValServParametros.Attributes.Item("data-rpta") = "0"
        udpValServ.Update()
    End Sub

    Private Sub DevolverRespuestaValServ(ByVal rpta As Integer, ByVal msg As String)
        divValServParametros.Attributes.Item("data-rpta") = rpta
        divValServParametros.Attributes.Item("data-msg") = msg
        udpValServ.Update()
    End Sub

    Private Sub EstadoBotonesAccion()
        If cmbCentroCosto.SelectedValue <> "-1" Then
            btnListarPostulante.Attributes.Remove("disabled")
            btnRegistrarPostulante.Attributes.Remove("disabled")
            btnRegistrarPostulanteCompleto.Attributes.Remove("disabled")

            btnListarIngresante.Attributes.Remove("disabled")
        Else
            btnListarPostulante.Attributes.Item("disabled") = "disabled"
            btnRegistrarPostulante.Attributes.Item("disabled") = "disabled"
            btnRegistrarPostulanteCompleto.Attributes.Item("disabled") = "disabled"
            btnExportarInteresados.Attributes.Item("disabled") = "disabled"

            btnListarIngresante.Attributes.Item("disabled") = "disabled"
            btnExportarIngresantes.Attributes.Item("disabled") = "disabled"
        End If
        udpFiltrosPostulantes.Update()
    End Sub

    Private Sub AtributosExportarInteresados()
        Dim ls_CodigoCco As String = IIf(cmbCentroCosto.SelectedValue = "-1", "0", cmbCentroCosto.SelectedValue)
        Dim ls_ParametroBusqueda As String = txtFiltroDNI.Text.Trim
        Dim ls_Estado As String = IIf(cmbEstadoAlumno.SelectedValue = "-1", "0", cmbEstadoAlumno.SelectedValue)
        Dim lb_MostrarSinDeuda As Boolean = chkMostrarSinDeuda.Checked
        Dim ls_Dominio As String = mo_RepoAdmision.ObtenerVariableGlobal("parhReportServer")
        Dim ls_Path As String = ls_Dominio & "PRIVADOS/ACADEMICO/ACAD_ListarInscritosPreUniv&tipo_consulta=R" & "&codigo_cco=" & ls_CodigoCco & "&parametro_busqueda=" & ls_ParametroBusqueda & "&estado=" & ls_Estado & "&mostrar_sin_deuda=" & lb_MostrarSinDeuda
        btnExportarInteresados.Attributes.Item("data-path") = ls_Path
        udpBotonesPostulante.Update()
    End Sub

    Private Sub AtributosExportarIngresantes()
        Dim ls_CicloIngr As String = IIf(cmbCicloAcademico.SelectedValue = "-1", "%", cmbCicloAcademico.SelectedValue)
        Dim ls_CodigoCco As String = IIf(cmbCentroCosto.SelectedValue = "-1", "0", cmbCentroCosto.SelectedValue)
        Dim ls_CodigoMin As String = IIf(cmbModalidadIngreso.SelectedValue = "-1", "0", cmbModalidadIngreso.SelectedValue)
        Dim ls_DNI As String = ""
        Dim ls_CodigoUniver As String = ""
        Dim ls_Nombres As String = ""
        Dim ls_EstadoPost As String = "I"
        Dim ls_CodigoTest As String = "0"
        Dim ls_CodigoAlu As String = "0"
        Dim ls_Categorizado As String = "%"
        Dim ls_Impresion As String = "%"
        Dim ls_CodigoCpf As String = "0"
        Dim ls_Letra As String = ""
        Dim ls_DeudaMatricula As String = "0"

        Dim ls_Dominio As String = mo_RepoAdmision.ObtenerVariableGlobal("parhReportServer")
        Dim ls_Path As String = ls_Dominio & "PRIVADOS/ACADEMICO/ACAD_ListarIngresantesPreUniv"
        ls_Path &= "&cicloingr=" & ls_CicloIngr
        ls_Path &= "&codigo_cco=" & ls_CodigoCco
        ls_Path &= "&codigo_min=" & ls_CodigoMin
        ls_Path &= "&dni=" & ls_DNI
        ls_Path &= "&codigouniver=" & ls_CodigoUniver
        ls_Path &= "&nombres=" & ls_Nombres
        ls_Path &= "&estadopost=" & ls_EstadoPost
        ls_Path &= "&codigo_test=" & ls_CodigoTest
        ls_Path &= "&codigo_alu=" & ls_CodigoAlu
        ls_Path &= "&categorizado=" & ls_Categorizado
        ls_Path &= "&impresion=" & ls_Impresion
        ls_Path &= "&codigo_cpf=" & ls_CodigoCpf
        ls_Path &= "&letra=" & ls_Letra
        ls_Path &= "&deuda_matricula=" & ls_DeudaMatricula
        btnExportarIngresantes.Attributes.Item("data-path") = ls_Path
        udpBotonesIngresante.Update()
    End Sub

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String)
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        divMdlMenServParametros.Attributes.Item("data-rpta") = ln_Rpta
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        mensajeServer.InnerHtml = ls_Mensaje
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub MostrarValidacionServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String, ByVal ls_Operacion As String)
        Try
            divValServParametros.Attributes.Item("data-mostrar") = "true"
            divValServParametros.Attributes.Item("data-rpta") = ln_Rpta
            divValServParametros.Attributes.Item("data-operacion") = ls_Operacion

            spnValServTitulo.InnerHtml = ls_Titulo
            divValServMensaje.InnerHtml = ls_Mensaje

            udpValServ.Update()
        Catch ex As Exception
            GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub GenerarPaginador(ByVal ln_RangoPaginas As Integer)
        pgrPostulantes.Controls.Clear()

        Dim lo_DtPostulantes As Data.DataTable = ViewState.Item("dtPostulantes")
        If lo_DtPostulantes IsNot Nothing AndAlso lo_DtPostulantes.Rows.Count > 0 Then
            rowPagination.Visible = True

            Dim ln_Desde As Integer = ((mn_PaginaPostu - 1) * mn_FilasPorPaginaPostu) + 1
            Dim ln_Hasta As Integer = Math.Min(IIf(mn_FilasPorPaginaPostu > 0, mn_PaginaPostu * mn_FilasPorPaginaPostu, mn_FilasPostu), mn_FilasPostu)
            lblPaginacion.InnerHtml = "Mostrando registros: " & ln_Desde & " - " & ln_Hasta & " de " & mn_FilasPostu

            If cmbCentroCosto.SelectedValue <> "-1" AndAlso mn_FilasPorPaginaPostu > 0 Then
                Dim ln_Paginas As Integer = Math.Ceiling(mn_FilasPostu / mn_FilasPorPaginaPostu)

                'Página de Inicio
                Dim lo_liPageStart As New HtmlGenericControl("li")
                lo_liPageStart.Attributes.Item("class") = "page-item"
                Dim lo_btnPageStart As New HtmlButton
                With lo_btnPageStart
                    .ID = "btnPagePostuStart"
                    .Attributes.Item("type") = "button"
                    .Attributes.Item("class") = "page-link"
                    .Attributes.Item("data-pagina") = 1
                    .InnerHtml = "<span aria-hidden='true'>&laquo;</span><span class='sr-only'>Inicio</span>"
                End With
                AddHandler lo_btnPageStart.ServerClick, AddressOf btnPagePostulantes_Click
                If mn_PaginaPostu = 1 Then
                    lo_liPageStart.Attributes.Item("class") = lo_liPageStart.Attributes.Item("class") & " disabled"
                End If
                lo_liPageStart.Controls.Add(lo_btnPageStart)
                pgrPostulantes.Controls.Add(lo_liPageStart)

                'Páginas
                Dim lb_Separador As Boolean = False
                For i As Integer = 1 To ln_Paginas
                    If i <= (mn_PaginaPostu + ln_RangoPaginas) AndAlso i >= (mn_PaginaPostu - ln_RangoPaginas) Then
                        Dim lo_liPage As New HtmlGenericControl("li")
                        lo_liPage.Attributes.Item("class") = "page-item"
                        Dim lo_btnPage As New HtmlButton
                        With lo_btnPage
                            .ID = "btnPagePostu" & i
                            .Attributes.Item("type") = "button"
                            .Attributes.Item("class") = "page-link"
                            .Attributes.Item("data-pagina") = i
                            .InnerHtml = i
                        End With

                        If i = mn_PaginaPostu Then
                            lo_liPage.Attributes.Item("class") = lo_liPage.Attributes.Item("class") & " active disabled"
                        End If

                        AddHandler lo_btnPage.ServerClick, AddressOf btnPagePostulantes_Click
                        lo_liPage.Controls.Add(lo_btnPage)
                        pgrPostulantes.Controls.Add(lo_liPage)

                        lb_Separador = False
                    Else
                        If Not lb_Separador Then
                            Dim lo_liSeparador As New HtmlGenericControl("li")
                            lo_liSeparador.Attributes.Item("class") = "page-item disabled"
                            Dim lo_btnPageSeparador As New HtmlButton
                            With lo_btnPageSeparador
                                .ID = "btnPagePostuSeparador" & i
                                .Attributes.Item("type") = "button"
                                .Attributes.Item("class") = "page-link"
                                .InnerHtml = "..."
                            End With
                            lo_liSeparador.Controls.Add(lo_btnPageSeparador)
                            pgrPostulantes.Controls.Add(lo_liSeparador)

                            lb_Separador = True
                        End If
                    End If
                Next

                'Página Final
                Dim lo_liPageEnd As New HtmlGenericControl("li")
                lo_liPageEnd.Attributes.Item("class") = "page-item"
                Dim lo_btnPageEnd As New HtmlButton
                With lo_btnPageEnd
                    .ID = "btnPagePostuEnd"
                    .Attributes.Item("type") = "button"
                    .Attributes.Item("class") = "page-link"
                    .Attributes.Item("data-pagina") = ln_Paginas
                    .InnerHtml = "<span aria-hidden='true'>&raquo;</span><span class='sr-only'>Fin</span>"
                End With
                AddHandler lo_btnPageEnd.ServerClick, AddressOf btnPagePostulantes_Click
                If mn_PaginaPostu = ln_Paginas Then
                    lo_liPageEnd.Attributes.Item("class") = lo_liPageEnd.Attributes.Item("class") & " disabled"
                End If
                lo_liPageEnd.Controls.Add(lo_btnPageEnd)
                pgrPostulantes.Controls.Add(lo_liPageEnd)
            End If
        Else
            rowPagination.Visible = False
        End If
        udpPgrPostulantes.Update()
    End Sub

    Private Sub CargarCombos()
        ClsFunciones.LlenarListas(cmbCentroCosto, mo_RepoAdmision.ListarCentroCosto(ms_CodigoTfu, ms_CodigoPer, ms_CodigoTest), "codigo_Cco", "Nombre", "-- Seleccione --")
        cmbCentroCosto_SelectedIndexChanged(Nothing, Nothing)

        ClsFunciones.LlenarListas(cmbCicloAcademico, mo_RepoAdmision.ListarCicloAcademico(), "cicloIng_Alu", "cicloIng_Alu", "-- Todos --  ")
        ClsFunciones.LlenarListas(cmbModalidadIngreso, mo_RepoAdmision.ListarModalidadIngresoPorTipo("TO"), "codigo_Min", "nombre_Min", "-- Todos --  ")
        ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional("2"), "codigo_cpf", "nombre_cpf", "-- Todos --")
    End Sub

    Private Sub CargarDatosEvento(ByVal codigoCco As Integer)
        Try
            Dim tipo As String = "2", param2 As String = "0"
            Dim dt As Data.DataTable = mo_RepoAdmision.ConsultarEventos(tipo, codigoCco, param2)

            If dt.Rows.Count > 0 Then
                tdNombreCorto.InnerHtml = "<b>" & dt.Rows(0).Item("nombre_dev") & "</b>"
                tdNroResolucion.InnerHtml = "<b>" & dt.Rows(0).Item("nroresolucion_dev") & "</b>"
                tdCoordinadorGeneral.InnerHtml = "<b>" & dt.Rows(0).Item("coordinador") & "</b>"
                tdCoordinadorApoyo.InnerHtml = "<b>" & dt.Rows(0).Item("apoyo") & "</b>"
                tdFechaInicioPropuesta.InnerHtml = "<b>" & dt.Rows(0).Item("fechainiciopropuesta_dev") & "</b>"
                tdFechaFinPropuesta.InnerHtml = "<b>" & dt.Rows(0).Item("fechafinpropuesta_dev") & "</b>"

                tdMetaParticipantes.InnerHtml = "<b>" & dt.Rows(0).Item("nroparticipantes_dev") & "</b>"
                tdPrecios.InnerHtml = "Contado: <b>S/." & dt.Rows(0).Item("preciounitcontado_dev") & "</b><br>" & _
                                        "Financiado: <b>S/." & dt.Rows(0).Item("preciounitfinanciado_dev") & "</b><br>" & _
                                        "Cuota Inicial: <b>S/." & dt.Rows(0).Item("montocuotainicial_dev") & "</b><br>" & _
                                        "Nro de Cuotas: <b>" & dt.Rows(0).Item("nrocuotas_dev") & "</b><br>"
                tdDescuentos.InnerHtml = "Personal USAT: <b>" & dt.Rows(0).Item("porcentajedescpersonalusat_dev") & " %</b><br>" & _
                                        "Alumno USAT: <b>" & dt.Rows(0).Item("porcentajedescalumnousat_dev") & " %</b><br>" & _
                                        "Corporativo: <b>" & dt.Rows(0).Item("porcentajedesccorportativo_dev") & " %</b><br>" & _
                                        "Egresado USAT: <b>" & dt.Rows(0).Item("porcentajedescegresado_dev") & " %</b><br>"

                tdGestionaNotas.InnerHtml = "<b>" & dt.Rows(0).Item("horarios_dev") & "</b>"
                tdHorarios.InnerHtml = "<b>" & dt.Rows(0).Item("horarios_dev") & "</b>"
                tdObservaciones.InnerHtml = "<b>" & dt.Rows(0).Item("horarios_dev") & "</b>"

                udpDatosEvento.Update()

                btnAnularCargosEvento.Attributes.Remove("disabled")
                btnInactivarInscritosEvento.Attributes.Remove("disabled")
                udpOpcionesEvento.Update()
            End If
        Catch ex As Exception
            GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub LimpiarDatosEvento()
        tdNombreCorto.InnerHtml = ""
        tdNroResolucion.InnerHtml = ""
        tdCoordinadorGeneral.InnerHtml = ""
        tdCoordinadorApoyo.InnerHtml = ""
        tdFechaInicioPropuesta.InnerHtml = ""
        tdFechaFinPropuesta.InnerHtml = ""
        tdMetaParticipantes.InnerHtml = ""
        tdPrecios.InnerHtml = ""
        tdDescuentos.InnerHtml = ""
        tdGestionaNotas.InnerHtml = ""
        tdHorarios.InnerHtml = ""
        tdObservaciones.InnerHtml = ""
        udpDatosEvento.Update()

        btnAnularCargosEvento.Attributes.Item("disabled") = "disabled"
        btnInactivarInscritosEvento.Attributes.Item("disabled") = "disabled"
        udpOpcionesEvento.Update()
    End Sub

    Private Sub CargarGrillaPostulantes(ByVal ln_CodigoCco As Integer, ByVal ls_DNI As String, ByVal ls_EstadoAlumno As String, ByVal lb_MostrarSinDeuda As Boolean, ByVal ln_Pagina As Integer, ByVal ln_FilasPorPagina As Integer)
        Dim lo_DtPostulantes As New Data.DataTable
        If ln_CodigoCco > -1 Then
            lo_DtPostulantes = mo_RepoAdmision.ListarPostulante(ln_CodigoCco, ls_DNI, ls_EstadoAlumno, lb_MostrarSinDeuda, ln_Pagina, ln_FilasPorPagina)
            grwPostulantes.DataSource = lo_DtPostulantes
            grwPostulantes.DataBind()

            GenerarPaginador(mn_RangoPaginasPostu)
        End If

        If lo_DtPostulantes.Rows.Count > 0 Then
            btnExportarInteresados.Attributes.Remove("disabled")
        Else
            btnExportarInteresados.Attributes.Item("disabled") = "disabled"
        End If
        udpBotonesPostulante.Update()

        AtributosExportarInteresados()
    End Sub

    Private Sub CargarGrillaMovimientosPostulante(ByVal ls_CodigoPso As String, ByVal ls_CodigoCco As String)
        Dim lo_DtMovimientosPostulante As Data.DataTable = mo_RepoAdmision.ListarMovimientosPostulante(ls_CodigoPso, ls_CodigoCco)
        grwMovimientosPostulante.DataSource = lo_DtMovimientosPostulante
        grwMovimientosPostulante.DataBind()

        divSinMovimientos.Visible = (lo_DtMovimientosPostulante.Rows.Count = 0)
        udpSinMovimientos.Update()
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefrescarGrillaPostulantes()
        For Each _Row As GridViewRow In grwPostulantes.Rows
            grwPostulantes_RowDataBound(grwPostulantes, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub CargarGrillaIngresantes()
        Dim lo_DtIngresantes As New Data.DataTable
        Dim ln_CodigoCco As Integer = cmbCentroCosto.SelectedValue
        If ln_CodigoCco > -1 Then
            Dim ls_CodigoCac As String = cmbCicloAcademico.SelectedValue
            Dim ln_CodigoMin As Integer = cmbModalidadIngreso.SelectedValue
            Dim ln_CodigoCpf As Integer = cmbCarreraProfesional.SelectedValue

            lo_DtIngresantes = mo_RepoAdmision.ListarIngresante(ls_CodigoCac, ln_CodigoCco, ln_CodigoMin, ln_CodigoCpf)
            grwIngresantes.DataSource = lo_DtIngresantes
            grwIngresantes.DataBind()
        End If

        If lo_DtIngresantes.Rows.Count > 0 Then
            btnExportarIngresantes.Attributes.Remove("disabled")
        Else
            btnExportarIngresantes.Attributes.Item("disabled") = "disabled"
        End If
        udpBotonesIngresante.Update()

        AtributosExportarIngresantes()
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefrescarGrillaIngresantes()
        For Each _Row As GridViewRow In grwIngresantes.Rows
            grwIngresantes_RowDataBound(grwIngresantes, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub LimpiarGrillaPostulantes()
        grwPostulantes.DataSource = Nothing
        grwPostulantes.DataBind()
        udpPostulantes.Update()
    End Sub

    Private Sub LimpiarGrillaIngresantes()
        grwIngresantes.DataSource = Nothing
        grwIngresantes.DataBind()
        udpIngresantes.Update()
    End Sub

    Private Sub ExportarInteresados()
        If Request.Cookies("fileDownload") Is Nothing Then
            Dim aCookie As New HttpCookie("fileDownload")
            aCookie.Value = "true"
            aCookie.Path = "/"
            Response.Cookies.Add(aCookie)
        End If

        Dim lo_DtPostulantes As Data.DataTable = ViewState.Item("dtPostulantes")

        If lo_DtPostulantes IsNot Nothing AndAlso lo_DtPostulantes.Rows.Count > 0 Then
            grwPostulantes.DataSource = lo_DtPostulantes
            grwPostulantes.DataBind()

            Dim ln_Columns As Integer = grwPostulantes.Columns.Count
            grwPostulantes.GridLines = GridLines.Both
            grwPostulantes.Columns(ln_Columns - 1).Visible = False
            grwPostulantes.Columns(ln_Columns - 2).Visible = False
            grwPostulantes.Columns(ln_Columns - 3).Visible = False
            grwPostulantes.Columns(ln_Columns - 4).Visible = False
            grwPostulantes.Columns(ln_Columns - 5).Visible = False

            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()

            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(grwPostulantes)
            Page.RenderControl(htw)

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=Inscritos.xls")
            Response.ContentType = "text/xml"
            Response.Write(sb.ToString())
            Response.End()
        End If
    End Sub

    Private Sub ExportarIngresantes()
        If Request.Cookies("fileDownload") Is Nothing Then
            Dim aCookie As New HttpCookie("fileDownload")
            aCookie.Value = "true"
            aCookie.Path = "/"
            Response.Cookies.Add(aCookie)
        End If

        Dim lo_DtIngresantes As Data.DataTable = ViewState.Item("dtIngresantes")

        If lo_DtIngresantes IsNot Nothing AndAlso lo_DtIngresantes.Rows.Count > 0 Then
            grwIngresantes.DataSource = lo_DtIngresantes
            grwIngresantes.DataBind()

            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()

            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(grwIngresantes)
            Page.RenderControl(htw)

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment; filename=Ingresantes.xls")
            Response.ContentType = "text/xml"
            Response.Write(sb.ToString())
            Response.End()
        End If
    End Sub

    Private Sub CargarIframeGenerarCargo()
        Dim eventArgument As String = IIf(Me.Request("__EVENTARGUMENT") IsNot Nothing, Me.Request("__EVENTARGUMENT"), "")
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer
        Dim valores As Dictionary(Of String, Object) = serializer.DeserializeObject(eventArgument)
        Dim codigoAlu As Integer = valores.Item("codigoAlu")
        Dim codigoPso As Integer = valores.Item("codigoPso")
        ifrmGenerarCargo.Attributes("src") = "frmFinanciarInscripcion.aspx?modal=true" & "&id=" & ms_CodigoPer & "&alu=" & codigoAlu & "&cco=" & cmbCentroCosto.SelectedValue & "&ctf=" & ms_CodigoTfu & "&pso=" & codigoPso & "&test=" & ms_CodigoTest
        udpGenerarCargo.Update()
    End Sub
#End Region

End Class
