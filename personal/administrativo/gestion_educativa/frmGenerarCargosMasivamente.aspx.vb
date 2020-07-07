Imports ClsPensiones
Imports System.IO
Imports System.Xml
Imports System.Data.OleDb
Imports System.Data
Imports System.Collections.Generic

Partial Class frmGenerarCargosMasivamente
    Inherits System.Web.UI.Page

#Region "Propiedades"
    'ENTIDADES
    Private me_Cpc As New e_PenConfiguracionProgramacionCargo
    Private me_CicloAcacemico As New e_PenCicloAcademico

    'DATOS
    Private md_Pensiones As New d_Pensiones
    Private md_Cpc As New d_PenConfiguracionProgramacionCargo
    Private md_CicloAcademico As New d_PenCicloAcademico
    Private md_TipoEstudio As New d_PenTipoEstudio
    Private md_CarreraProfesional As New d_PenCarreraProfesional
    Private md_ServicioConcepto As New d_PenServicioConcepto
    Private md_CentroCosto As New d_PenCentroCosto
    Private md_ProgramacionCargoMasivo As New d_PenProgramacionCargoMasivo
    Private md_DatosCargoMasivo As New d_PenDatosCargoMasivo

    'OTRAS VARIABLES
    Private ms_SharedFilesPath As String = ConfigurationManager.AppSettings("SharedFiles")
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("id_per") = "684" 'PENDIENTE BORRAR!
        If Not IsPostBack Then
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            mt_Init()
        Else
            If Session("id_per") = "" Or Request.QueryString("id") = "" Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Se ha perdido la sesión")
            End If

            mt_LimpiarToastServidor()
            mt_LimpiarMensajeServidor()
            mt_LimpiarModalAlumnos()
            mt_RefreshGridView()
        End If
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        Try
            mt_Listar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvProgramacion_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvProgramacion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ls_codigoPcm As String = grvProgramacion.DataKeys(e.Row.RowIndex).Values.Item("codigo_pcm")
                Dim ln_Index As Integer = e.Row.RowIndex + 1
                Dim ln_Columnas As Integer = grvProgramacion.Columns.Count
                _cellsRow(0).Text = ln_Index

                Dim ls_Programacion As String = ""

                Dim lo_Dr As Data.DataRowView = e.Row.DataItem
                If lo_Dr IsNot Nothing Then
                    Select Case lo_Dr.Item("tipoConfiguracion_pcm")
                        Case "M"
                            lo_Dr.Item("tipoConfiguracion_pcm") = "MANUAL"
                        Case "P"
                            lo_Dr.Item("tipoConfiguracion_pcm") = "PREDETERMINADA"
                    End Select

                    Select Case lo_Dr.Item("tipoEjecucion_pcm")
                        Case "M"
                            lo_Dr.Item("tipoEjecucion_pcm") = "MANUAL"
                        Case "U"
                            lo_Dr.Item("tipoEjecucion_pcm") = "PROGRAMADA ÚNICA"
                        Case "P"
                            lo_Dr.Item("tipoEjecucion_pcm") = "PROGRAMADA PERIÓDICA"
                    End Select


                    Dim ls_FechaHoraInicio As String = lo_Dr.Item("fechaHoraInicio_pcm")
                    If ls_FechaHoraInicio.Trim <> "" Then
                        ls_FechaHoraInicio = CDate(ls_FechaHoraInicio).ToString("dd/MM/yyyy HH:mm")
                    End If
                    Dim ls_FechaHoraFin As String = lo_Dr.Item("fechaHoraFin_pcm").ToString
                    If Not String.IsNullOrEmpty(ls_FechaHoraFin) Then
                        ls_FechaHoraFin = " - " & CDate(ls_FechaHoraFin).ToString("dd/MM/yyyy HH:mm")
                    End If

                    ls_Programacion = ls_FechaHoraInicio & ls_FechaHoraFin

                    e.Row.DataBind()
                End If

                _cellsRow(4).Text = ls_Programacion

                'Editar
                Dim lo_btnEditar As New HtmlButton()
                With lo_btnEditar
                    .ID = "btnEditar" & ln_Index
                    .Attributes.Add("data-pcm", ls_codigoPcm)
                    .Attributes.Add("class", "btn btn-primary btn-sm")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Editar")
                    .InnerHtml = "<i class='fa fa-edit'></i>"
                    AddHandler .ServerClick, AddressOf btnEditar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEditar)

                'Ejecutar operación manualmente
                Dim lo_btnFakeEjecutar As New HtmlButton
                With lo_btnFakeEjecutar
                    .ID = "btnFakeEjecutar" & ln_Index
                    .Attributes.Add("data-pcm", ls_codigoPcm)
                    .Attributes.Add("class", "btn btn-success btn-sm")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Ejecutar")
                    .InnerHtml = "<i class='fa fa-angle-double-right'></i>"
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnFakeEjecutar)

                Dim lo_btnEjecutar As New HtmlButton
                With lo_btnEjecutar
                    .ID = "btnEjecutar" & ln_Index
                    .Attributes.Add("data-pcm", ls_codigoPcm)
                    .Attributes.Add("class", "btn btn-light btn-sm d-none")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Ejecutar")
                    .InnerHtml = "<i class='fa fa-angle-double-right'></i>"
                    AddHandler .ServerClick, AddressOf btnEjecutar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEjecutar)
            End If

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnNuevo_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.ServerClick
        Try
            mt_SeleccionarTab("GENERAL", "M")
            mt_CargarForm(0)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtConfiguracionPredefinida_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtConfiguracionPredefinida.ServerChange
        Try
            mt_SeleccionarTipoConfiguracion(rbtConfiguracionPredefinida.Value)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtConfiguracionManual_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtConfiguracionManual.ServerChange
        Try
            mt_SeleccionarTipoConfiguracion(rbtConfiguracionManual.Value)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnMostrarAlumnos_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMostrarAlumnos.ServerClick
        Try
            mt_ListarAlumnos()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", 0, ex.Message)
        End Try
    End Sub

    Protected Sub grvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAlumnos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim index As Integer = e.Row.RowIndex + 1
                _cellsRow(0).Text = index
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Advertencia", 0, ex.Message)
        End Try
    End Sub

    Protected Sub cmbTipoDeuda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoDeuda.SelectedIndexChanged
        Try
            mt_ListarSco()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub cmbServicioConcepto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServicioConcepto.SelectedIndexChanged
        Try
            mt_ListarCco()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtEjecManual_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtEjecManual.ServerChange
        Try
            mt_SeleccionarTipoProcesamiento(rbtEjecManual.Value)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtProgUnaVez_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtProgUnaVez.ServerChange
        Try
            mt_SeleccionarTipoProcesamiento(rbtProgUnaVez.Value)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtProgPeriodico_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtProgPeriodico.ServerChange
        Try
            mt_SeleccionarTipoProcesamiento(rbtProgPeriodico.Value)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtTipoManual_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTipoManual.ServerChange
        Try
            mt_SeleccionarTipoDeuda(True)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub rbtTipoConfigurado_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTipoConfigurado.ServerChange
        Try
            mt_SeleccionarTipoDeuda(False)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.ServerClick
        Try
            mt_Guardar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick
        Try
            hddTipoVista.Value = "L"
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"
    Private Sub mt_Init()
        Try
            mt_ListarCpc()
            mt_ListarCac()
            mt_ListarSco()
            'mt_SeleccionarTipoDeuda(True)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim lo_ProgramacionCargoMasivo As New e_PenProgramacionCargoMasivo
            With lo_ProgramacionCargoMasivo
                .operacion = "GEN"
                .descripcion_pcm = "%" & txtFiltroDescripcion.Text.Trim & "%"
                .codigo_cpc = 0
            End With

            Dim lo_Dt As Data.DataTable = md_ProgramacionCargoMasivo.ListarProgramacionCargoMasivo(lo_ProgramacionCargoMasivo)
            grvProgramacion.DataSource = lo_Dt
            grvProgramacion.DataBind()

            divGrvProgramacion.Visible = lo_Dt.Rows.Count > 0
            udpGrvProgramacion.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub mt_RefreshGridView()
        Try
            For Each _Row As GridViewRow In grvProgramacion.Rows
                grvProgramacion_RowDataBound(grvProgramacion, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Eventos delegados
    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ln_CodigoPcm As Integer = button.Attributes("data-pcm")
            mt_CargarForm(ln_CodigoPcm)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub btnEjecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ln_CodigoPcm As Integer = button.Attributes("data-pcm")
            mt_Ejecutar(ln_CodigoPcm)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    '---------------------------------------------------------------------------------

    Private Sub mt_CargarForm(ByVal ln_CodigoPcm As Integer)
        Try
            hddCod.Value = ln_CodigoPcm
            hddCodD.Value = 0
            udpParams.Update()

            mt_LimpiarForm("PCM")
            mt_LimpiarForm("DCM")

            If ln_CodigoPcm <> 0 Then
                Dim lo_Pcm As New e_PenProgramacionCargoMasivo
                With lo_Pcm
                    .operacion = "GEN"
                    .codigo_pcm = ln_CodigoPcm
                End With

                Dim dt_Pcm As Data.DataTable = md_ProgramacionCargoMasivo.ListarProgramacionCargoMasivo(lo_Pcm)
                If dt_Pcm.Rows.Count = 0 Then
                    mt_GenerarMensajeServidor("Advertencia", 0, "No se ha encontrado programación con ID: " & ln_CodigoPcm)
                    Exit Sub
                End If

                Dim lo_Dcm As New e_PenDatosCargoMasivo
                With lo_Dcm
                    .operacion = "GEN"
                    .codigo_pcm = ln_CodigoPcm
                End With
                Dim dt_Dcm As Data.DataTable = md_DatosCargoMasivo.ListarDatosCargoMasivo(lo_Dcm)

                Dim drPcm As Data.DataRow = dt_Pcm.Rows(0)
                With drPcm
                    Dim tipoConfiguracion_pcm As String = .Item("tipoConfiguracion_pcm")
                    Dim tipoEjecucion_pcm As String = .Item("tipoEjecucion_pcm")
                    Dim codigo_cpc As Integer = .Item("codigo_cpc")
                    Dim periodicidad_pcm As Integer = .Item("periodicidad_pcm")

                    'Datos generales
                    If tipoConfiguracion_pcm = "P" Then
                        rbtConfiguracionPredefinida.Checked = True : rbtConfiguracionPredefinida_ServerChange(Nothing, Nothing)
                        rbtConfiguracionManual.Checked = False
                    Else
                        rbtConfiguracionPredefinida.Checked = False
                        rbtConfiguracionManual.Checked = True : rbtConfiguracionManual_ServerChange(Nothing, Nothing)
                    End If
                    cmbTipoConfiguracion.SelectedValue = IIf(codigo_cpc <> 0, codigo_cpc, -1)
                    cmbCicloAcademico.SelectedValue = drPcm.Item("codigo_cac")
                    txtDescripcion.Text = .Item("descripcion_pcm")

                    'Datos de programación
                    Select Case tipoEjecucion_pcm
                        Case "M"
                            rbtEjecManual.Checked = True : rbtEjecManual_ServerChange(Nothing, Nothing)
                            rbtProgUnaVez.Checked = False
                            rbtProgPeriodico.Checked = False
                        Case "U"
                            rbtEjecManual.Checked = False
                            rbtProgUnaVez.Checked = True : rbtProgUnaVez_ServerChange(Nothing, Nothing)
                            rbtProgPeriodico.Checked = False
                        Case "P"
                            rbtEjecManual.Checked = False
                            rbtProgUnaVez.Checked = False
                            rbtProgPeriodico.Checked = True : rbtProgPeriodico_ServerChange(Nothing, Nothing)
                    End Select
                    txtFechaHoraInicioProg.Value = .Item("fechaHoraInicio_pcm")
                    txtFechaHoraFinProg.Value = .Item("fechaHoraFin_pcm")
                    cmbEjecutarCada.SelectedValue = IIf(periodicidad_pcm <> 0, periodicidad_pcm, -1)

                    'Datos del filtro de alumnos
                    hddTipoSeleccionAlumnos.Value = IIf(Not String.IsNullOrEmpty(.Item("tipoSeleccion_pcm").ToString.Trim), .Item("tipoSeleccion_pcm"), "M")
                    hddFiltrosAlumno.Value = .Item("filtrosAlumno_pcm")
                    If hddFiltrosAlumno.Value.Trim <> "" Then
                        Dim la_Filtros As String() = hddFiltrosAlumno.Value.Split("|")
                        For Each f As String In la_Filtros
                            Dim la_filtro As String() = f.Split("=")
                            Dim ls_Key As String = la_filtro(0)
                            Dim ls_Value As String = la_filtro(1)
                            If ls_Key = "codigoUniver_Alu" Then
                                hddCodUnivAlumnos.Value = ls_Value
                            End If
                        Next
                    End If
                End With

                'Datos del cargo
                If dt_Dcm.Rows.Count > 0 Then
                    Dim dr_Dcm As Data.DataRow = dt_Dcm.Rows(0) 'El formulario actualmente solo muestra un cargo
                    With dr_Dcm
                        hddCodD.Value = .Item("codigo_dcm")
                        Dim tipo_dcm As String = .Item("tipo_dcm")
                        Dim configuracion_dcm As String = IIf(.Item("configuracion_dcm").trim() <> "", .Item("configuracion_dcm"), -1)
                        Dim codigo_sco As String = IIf(.Item("codigo_sco") <> "0", .Item("codigo_sco"), -1)
                        Dim codigo_cco As String = IIf(.Item("codigo_cco") <> "0", .Item("codigo_cco"), -1)

                        Select Case tipo_dcm
                            Case "C"
                                rbtTipoConfigurado.Checked = True : rbtTipoConfigurado_ServerChange(Nothing, Nothing)
                                rbtTipoManual.Checked = False
                            Case "M"
                                rbtTipoConfigurado.Checked = False
                                rbtTipoManual.Checked = True : rbtTipoManual_ServerChange(Nothing, Nothing)
                        End Select
                        cmbTipoDeuda.SelectedValue = configuracion_dcm
                        cmbServicioConcepto.SelectedValue = codigo_sco : cmbServicioConcepto_SelectedIndexChanged(Nothing, Nothing)
                        cmbCentroCosto.SelectedValue = codigo_cco
                        txtImporte.Text = .Item("importe_dcm")
                        txtFechaVencimiento.Value = .Item("fechaVencimiento_dcm")
                        txtObservacion.Text = .Item("observacion_dcm")
                    End With
                Else
                    mt_LimpiarForm("DCM")
                End If
            End If

            ifrmFiltrosAlumno.Attributes("src") = "frmFiltrarAlumnos.aspx?tipo=I" _
                                                    & "&filtros=" & hddFiltrosAlumno.Value _
                                                    & "&excluirControles=ExcluirCarreraProfesional"

            udpMantenimiento.Update()
            mt_SeleccionarTab("GENERAL", "M")

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Ejecutar(ByVal ln_CodigoPcm As Integer)
        Try
            Dim ln_CodUsuario As Integer = Session("id_per")
            Dim ls_TipoEjecucionHpc As String = "M"
            Dim lo_Salida As Dictionary(Of String, String) = md_ProgramacionCargoMasivo.EjecutarProgramacionCargo(ln_CodigoPcm, ls_TipoEjecucionHpc, ln_CodUsuario)
            mt_GenerarToastServidor(lo_Salida.Item("rpta"), lo_Salida.Item("msg"))
            If lo_Salida.Item("rpta") = "1" Then
                mt_Listar()
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarForm(ByVal tipo As String)
        Try
            Select Case tipo
                Case "PCM"
                    'Datos generales
                    rbtConfiguracionPredefinida.Checked = True : rbtConfiguracionPredefinida_ServerChange(Nothing, Nothing)
                    cmbTipoConfiguracion.SelectedValue = -1
                    rbtConfiguracionManual.Checked = False
                    cmbCicloAcademico.SelectedValue = -1
                    txtDescripcion.Text = ""
                    'Datos de programación
                    rbtEjecManual.Checked = True : rbtEjecManual_ServerChange(Nothing, Nothing)
                    rbtProgUnaVez.Checked = False
                    rbtProgPeriodico.Checked = False
                    txtFechaHoraInicioProg.Value = ""
                    txtFechaHoraFinProg.Value = ""
                    cmbEjecutarCada.SelectedValue = -1
                    'Datos del filtro de alumnos
                    hddTipoSeleccionAlumnos.Value = "M"
                    hddFiltrosAlumno.Value = ""
                    hddCodUnivAlumnos.Value = ""
                Case "DCM"
                    'Datos del cargo
                    rbtTipoConfigurado.Checked = False
                    rbtTipoManual.Checked = True : rbtTipoManual_ServerChange(Nothing, Nothing)
                    cmbServicioConcepto.SelectedValue = -1
                    cmbCentroCosto.SelectedValue = -1
                    txtImporte.Text = ""
                    txtFechaVencimiento.Value = ""
                    txtObservacion.Text = ""
            End Select
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_SeleccionarTab(ByVal tipo As String, ByVal valor As String)
        Select Case tipo
            Case "GENERAL"
                hddTipoVista.Value = valor
            Case "ALUMNOS"
                hddTipoSeleccionAlumnos.Value = valor
        End Select
        udpParams.Update()
    End Sub

    Private Sub mt_ListarCpc()
        Try
            Dim lo_Dt As New Data.DataTable

            With me_Cpc
                .operacion = "GEN"
            End With
            lo_Dt = md_Cpc.Listar(me_Cpc)

            ClsFunciones.LlenarListas(cmbTipoConfiguracion, lo_Dt, "codigo_cpc", "nombre_cpc", "-- SELECCIONE --")
            lo_Dt.Dispose()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarCac()
        Try
            Dim lo_Dt As New Data.DataTable

            With me_CicloAcacemico
                .operacion = "GEN"
                .tipo_Cac = "N"
            End With
            lo_Dt = md_CicloAcademico.Listar(me_CicloAcacemico)

            ClsFunciones.LlenarListas(cmbCicloAcademico, lo_Dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            lo_Dt.Dispose()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarSco()
        Try
            Dim lo_Dt As New Data.DataTable
            Dim ls_Tipo As String = ""
            If rbtTipoConfigurado.Checked Then
                Select Case cmbTipoDeuda.SelectedValue
                    Case "M"
                        ls_Tipo = "MATRICULA"
                    Case "P"
                        ls_Tipo = "PENSION"
                End Select

            End If

            Dim le_ServicioConcepto As New e_PenServicioConcepto
            With le_ServicioConcepto 'PENDIENTE!
                .operacion = "DE"
                .param = ls_Tipo
            End With
            lo_Dt = md_ServicioConcepto.ConsultarServicioConcepto(le_ServicioConcepto)

            ClsFunciones.LlenarListas(cmbServicioConcepto, lo_Dt, "codigo_sco", "descripcion_sco", "-- SELECCIONE --")
            cmbServicioConcepto_SelectedIndexChanged(Nothing, Nothing)
            udpServicioConcepto.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarCco()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_CentroCosto As New e_PenCentroCosto
            With le_CentroCosto
                .operacion = "XSCO"
                .codigoSco = cmbServicioConcepto.SelectedValue
            End With
            lo_Dt = md_CentroCosto.ConsultarCentroCosto(le_CentroCosto)

            ClsFunciones.LlenarListas(cmbCentroCosto, lo_Dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
            udpCentroCosto.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_SeleccionarTipoConfiguracion(ByVal tipoConfiguracion As String)
        Try
            Select Case tipoConfiguracion
                Case "P"
                    cmbTipoConfiguracion.Enabled = True
                    divConfigManual.Visible = False
                Case "M"
                    cmbTipoConfiguracion.Enabled = False : cmbTipoConfiguracion.SelectedValue = "-1"
                    divConfigManual.Visible = True
            End Select
            udpTipoConfiguracion.Update()
            udpConfigManual.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarAlumnos()
        Try
            Dim configPredefinida As Boolean = rbtConfiguracionPredefinida.Checked

            Dim codigoCpc As Integer = cmbTipoConfiguracion.SelectedValue
            Dim codigoCac As String = cmbCicloAcademico.SelectedValue

            If configPredefinida AndAlso codigoCpc = -1 Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe seleccionar una configuración")
                Exit Sub
            End If

            If configPredefinida AndAlso codigoCac = -1 Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe seleccionar un ciclo académico")
                Exit Sub
            End If

            Dim lo_DtAlumnos As New Data.DataTable
            Dim filtrosAlumnoCpc As String = ""

            Dim descrCicloActual As String = IIf(codigoCac = -1, "", cmbCicloAcademico.SelectedItem.Text.Trim)

            If configPredefinida Then
                With me_Cpc
                    .operacion = "GEN"
                    .codigo_cpc = codigoCpc
                End With
                Dim lo_DtCpc As Data.DataTable = md_Cpc.Listar(me_Cpc)

                If lo_DtCpc.Rows.Count > 0 Then
                    filtrosAlumnoCpc = lo_DtCpc.Rows(0).Item("filtrosAlumno_cpc")
                    filtrosAlumnoCpc = filtrosAlumnoCpc & "|descrCicloActual=" & descrCicloActual
                End If
            Else
                If hddTipoSeleccionAlumnos.Value = "M" Then
                    If hddFiltrosAlumno.Value = "" Then
                        mt_GenerarMensajeServidor("Advertencia", 0, "No se ha seleccionado ningún filtro")
                        Exit Sub
                    End If
                    filtrosAlumnoCpc = hddFiltrosAlumno.Value
                Else
                    If hddCodUnivAlumnos.Value.Trim = "" Then
                        mt_GenerarMensajeServidor("Advertencia", 0, "No se ha cargado a ningún alumno")
                        Exit Sub
                    End If
                    filtrosAlumnoCpc = "codigoUniver_Alu=" & hddCodUnivAlumnos.Value
                End If
            End If

            With me_Cpc
                .operacion = ""
                .codigo_cpc = ""
                .filtrosAlumno_cpc = filtrosAlumnoCpc
            End With
            lo_DtAlumnos = md_Cpc.FiltrarAlumnos(me_Cpc)

            hCantidadAlumnos.InnerHtml = "Cantidad de alumnos encontrados: " & lo_DtAlumnos.Rows.Count

            grvAlumnos.DataSource = lo_DtAlumnos
            grvAlumnos.DataBind()
            udpAlumnos.Update()

            spnAlumnoTitulo.InnerHtml = "Lista de Alumnos"
            divAlumnoParametros.Attributes.Item("data-mostrar") = "true"
            udpAlumnos.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarAlumnos()
        Try
            If fluArchivoAlumnos.HasFile Then
                Dim ls_FileName As String = Path.GetFileName(fluArchivoAlumnos.PostedFile.FileName)
                Dim ls_Extension As String = Path.GetExtension(fluArchivoAlumnos.PostedFile.FileName)
                Dim ls_FolderPath As String = ""
                Dim ls_FilePath As String = Server.MapPath(ls_FolderPath + ls_FileName)

                fluArchivoAlumnos.SaveAs(ls_FilePath)

                Dim conStr As String = ""

                Select Case ls_Extension
                    Case ".xls"
                        conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString()
                        Exit Select
                    Case ".xlsx"
                        conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString()
                        Exit Select
                End Select

                conStr = String.Format(conStr, ls_FilePath, False)

                Dim connExcel As New OleDbConnection(conStr)
                Dim cmdExcel As New OleDbCommand()
                Dim oda As New OleDbDataAdapter()
                Dim dt As New DataTable()

                cmdExcel.Connection = connExcel

                'Get the name of First Sheet
                connExcel.Open()

                Dim dtExcelSchema As DataTable
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                connExcel.Close()

                'Read Data from First Sheet
                connExcel.Open()
                cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
                oda.SelectCommand = cmdExcel
                oda.Fill(dt)
                connExcel.Close()
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarArchivoAlumnos()
        Dim ls_RutaArchivo As String = ""
        Dim ls_NombreFinal As String = ""
        Dim ln_IdArchivoCompartido As Integer
        Dim ls_Extension As String = ""
        Dim ln_IdTabla As Integer = 15 'PENDIENTE! MODIFICAR CUANDO SE AGREGE EL REGISTRO A LA TABLA
        Dim ln_IdTransaccion As Integer = 0 ' ID DE TABLA RELACIONADA (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
        Dim ln_NroOperacion As Integer = 0 ' ID DE TABLA RELACIONADA OPCIONAL (EN ESTE CASO NO TENEMOS UN SOLO REGISTRO SINO VARIOS SE CONSIDERA 0)
        Dim lo_Archivo As HttpPostedFile = HttpContext.Current.Request.Files(fluArchivoAlumnos.ID)
        Dim ln_CodigoPer As Integer = Request.QueryString("id")
        Dim lb_Resultado As Boolean = mt_SubirArchivo(ln_IdTabla, 0, lo_Archivo, 0)

        If lb_Resultado Then
            Dim dta As New Data.DataTable
            dta = md_Pensiones.ObtenerUltimoIDArchivoCompartido(ln_IdTabla, ln_IdTransaccion, ln_NroOperacion)
            ls_RutaArchivo = dta.Rows(0).Item("ruta").ToString
            ls_NombreFinal = dta.Rows(0).Item("NombreArchivo").ToString
            ln_IdArchivoCompartido = dta.Rows(0).Item("idarchivo")
            ls_Extension = dta.Rows(0).Item("Extension")

            'Obtenemos Respuesta de MigraciÃ³n en una tabla
            Dim lo_Resultado As New Dictionary(Of String, String)
            'lo_Resultado = mo_RepoAdmision.CargarExcelNotas(ls_RutaArchivo, ln_CodigoCco, ln_IdArchivoCompartido, activarEstado, ln_CodigoPer)

            mt_GenerarMensajeServidor("Respuesta", lo_Resultado.Item("rpta"), lo_Resultado.Item("msg"))

        End If
    End Sub

    Private Sub mt_SeleccionarTipoProcesamiento(ByVal tipoEjecucion As String)
        Try
            Select Case tipoEjecucion
                Case "M"
                    txtFechaHoraInicioProg.Attributes.Item("disabled") = "disabled"
                    txtFechaHoraInicioProg.Value = ""
                    txtFechaHoraFinProg.Attributes.Item("disabled") = "disabled"
                    txtFechaHoraFinProg.Value = ""
                    cmbEjecutarCada.SelectedValue = "-1"
                    cmbEjecutarCada.Enabled = False
                Case "U"
                    txtFechaHoraInicioProg.Attributes.Remove("disabled")
                    txtFechaHoraFinProg.Attributes.Item("disabled") = "disabled"
                    txtFechaHoraFinProg.Value = ""
                    cmbEjecutarCada.SelectedValue = "-1"
                    cmbEjecutarCada.Enabled = False
                Case "P"
                    txtFechaHoraInicioProg.Attributes.Remove("disabled")
                    txtFechaHoraFinProg.Attributes.Remove("disabled")
                    cmbEjecutarCada.Enabled = True
            End Select

            udpProgramacion.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_SeleccionarTipoDeuda(ByVal tipoManual As Boolean)
        Try
            cmbServicioConcepto.Enabled = tipoManual
            cmbCentroCosto.Enabled = tipoManual

            If Not tipoManual Then
                cmbServicioConcepto.SelectedIndex = 0
                cmbServicioConcepto_SelectedIndexChanged(Nothing, Nothing)
            End If

            udpServicioCentroCosto.Update()

            cmbTipoDeuda.Enabled = Not tipoManual
            udpTipoDeuda.Update()

            mt_ListarSco()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Function mt_SubirArchivo(ByVal ls_IdTabla As Integer, ByVal ls_NroTransaccion As String, ByVal lo_Archivo As HttpPostedFile, ByVal tipo As String) As Boolean
        Try
            Dim ls_NroOperacion As String = ""
            Dim id_tablaProviene As String = ls_NroTransaccion

            Dim ld_Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim ls_Usuario As String = Session("perlogin").ToString
            Dim lo_Input(lo_Archivo.ContentLength) As Byte

            Dim lo_Br As New BinaryReader(lo_Archivo.InputStream)
            Dim lo_BinData As Byte() = lo_Br.ReadBytes(lo_Archivo.InputStream.Length)

            Dim lo_WsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)

            Dim ls_NombreArchivo As String = System.IO.Path.GetFileName(lo_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            list.Add("Fecha", ld_Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(lo_Archivo.FileName))
            list.Add("Nombre", ls_NombreArchivo)
            list.Add("TransaccionId", id_tablaProviene)
            list.Add("TablaId", ls_IdTabla)
            list.Add("NroOperacion", ls_NroOperacion)
            list.Add("Archivo", System.Convert.ToBase64String(lo_BinData, 0, lo_BinData.Length))
            list.Add("Usuario", ls_Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", ls_Usuario)

            Dim envelope As String = lo_WsCloud.SoapEnvelope(list)
            Dim lo_RespuestaSOAP As New XmlDocument
            lo_RespuestaSOAP.LoadXml(lo_WsCloud.PeticionRequestSoap(ms_SharedFilesPath, envelope, "http://usat.edu.pe/UploadFile", ls_Usuario))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable)
            lo_Namespace.AddNamespace("ns", "http://usat.edu.pe")

            Dim ls_RutaNodos As String = "//ns:UploadFileResponse/ns:UploadFileResult"
            Dim ls_Status As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos & "/ns:Status", lo_Namespace).InnerText
            Dim ls_Code As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos & "/ns:StatusBody/ns:Code ", lo_Namespace).InnerText

            If ls_Status = "OK" And ls_Code = "0" Then
                Return True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            Throw ex
        End Try

        Return False

    End Function

    Private Function mt_ValidarForm() As Boolean
        Try
            If rbtConfiguracionPredefinida.Checked Then
                If cmbTipoConfiguracion.SelectedValue = "-1" Then
                    mt_GenerarToastServidor(0, "No ha seleccionado una configuración", "cmbTipoConfiguracion")
                    Return False
                End If
            End If

            If cmbCicloAcademico.SelectedValue = "-1" Then
                mt_GenerarToastServidor(0, "Debe selecconar un ciclo académico", "cmbCicloAcademico")
                Return False
            End If

            If txtDescripcion.Text.Trim = "" Then
                mt_GenerarToastServidor(0, "Debe ingresar una descripción", "txtDescripcion")
                Return False
            End If

            Dim ld_FechaHoraInicioProg As Date, ld_FechaHoraFinProg As Date
            If rbtProgUnaVez.Checked OrElse rbtProgPeriodico.Checked Then
                If txtFechaHoraInicioProg.Value.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresaro la fecha de inicio de ejecución", "txtFechaHoraInicioProg")
                    Return False
                End If

                If Not DateTime.TryParse(txtFechaHoraInicioProg.Value.Trim, ld_FechaHoraInicioProg) Then
                    mt_GenerarToastServidor(0, "Debe ingresar una fecha de inicio válida", "txtFechaHoraInicioProg")
                    Return False
                End If
            End If

            If rbtProgPeriodico.Checked Then
                If txtFechaHoraFinProg.Value.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresaro la fecha de fin de ejecución", "txtFechaHoraFinProg")
                    Return False
                End If

                If Not DateTime.TryParse(txtFechaHoraFinProg.Value.Trim, ld_FechaHoraFinProg) Then
                    mt_GenerarToastServidor(0, "Debe ingresar una fecha de fin válida", "txtFechaHoraFinProg")
                    Return False
                End If

                If ld_FechaHoraFinProg <= ld_FechaHoraInicioProg Then
                    mt_GenerarToastServidor(0, "La fecha de fin debe ser mayor a la fecha de inicio", "txtFechaHoraFinProg")
                    Return False
                End If

                If cmbEjecutarCada.SelectedValue = "-1" Then
                    mt_GenerarToastServidor(0, "Debe seleccionar la periodicidad de ejecución", "cmbEjecutarCada")
                    Return False
                End If
            End If

            If hddTipoSeleccionAlumnos.Value = "M" Then
                If hddFiltrosAlumno.Value.Trim = "" Then
                    mt_GenerarToastServidor(0, "No ha realizado ningún filtro")
                    Return False
                End If
            End If

            If hddTipoSeleccionAlumnos.Value = "I" Then
                If hddCodUnivAlumnos.Value.Trim = "" Then
                    mt_GenerarToastServidor(0, "No ha importado a ningún alumno", "fluArchivoAlumnos")
                    Return False
                End If
            End If

            If rbtConfiguracionManual.Checked Then
                If rbtTipoConfigurado.Checked Then
                    If cmbTipoDeuda.SelectedValue = "-1" Then
                        mt_GenerarToastServidor(0, "Debe seleccionar una configuración para generar el cargo", "cmbTipoDeuda")
                        Return False
                    End If
                End If

                If rbtTipoManual.Checked Then
                    If cmbServicioConcepto.SelectedValue = "-1" Then
                        mt_GenerarToastServidor(0, "Debe seleccionar el concepto del cargo", "cmbServicioConcepto")
                        Return False
                    End If

                    If cmbCentroCosto.SelectedValue = "-1" Then
                        mt_GenerarToastServidor(0, "Debe seleccionar el centro de costo del cargo", "cmbCentroCosto")
                        Return False
                    End If
                End If

                If txtImporte.Text.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresar el importe del cargo a generar", "txtImporte")
                    Return False
                End If

                Dim importe As Double = 0.0
                If Not Double.TryParse(txtImporte.Text.Trim, importe) Then
                    mt_GenerarToastServidor(0, "El importe ingresado no es válido", "txtImporte")
                    Return False
                End If

                If txtFechaVencimiento.Value.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresar una fecha de vencimiento del cargo a generar", "txtFechaVencimiento")
                    Return False
                End If

                If txtObservacion.Text.Trim = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresar una observación de vencimiento del cargo a generar", "txtObservacion")
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
            Return False
        End Try
    End Function

    Private Sub mt_Guardar()
        Try
            If mt_ValidarForm() Then
                Dim codigo_pcm As Integer = hddCod.Value
                Dim tipoConfiguracion_pcm As String = IIf(rbtConfiguracionPredefinida.Checked, rbtConfiguracionPredefinida.Value, rbtConfiguracionManual.Value)
                Dim codigo_cac As String = cmbCicloAcademico.SelectedValue
                Dim descripcion_pcm As String = txtDescripcion.Text
                Dim tipoEjecucion_pcm As String = IIf(rbtEjecManual.Checked, rbtEjecManual.Value, IIf(rbtProgUnaVez.Checked, rbtProgUnaVez.Value, rbtProgPeriodico.Value))
                Dim codigo_cpc As Object = IIf(cmbTipoConfiguracion.SelectedValue <> "-1", cmbTipoConfiguracion.SelectedValue, DBNull.Value)
                If tipoConfiguracion_pcm <> "P" Then
                    codigo_cpc = DBNull.Value 'Me aseguro que el codigo se guarde solo si el tipo es programado
                End If
                Dim fechaHoraInicio_pcm As Object = txtFechaHoraInicioProg.Value
                If fechaHoraInicio_pcm = "" Then
                    fechaHoraInicio_pcm = DBNull.Value
                End If
                Dim fechaHoraFin_pcm As Object = txtFechaHoraFinProg.Value
                If fechaHoraFin_pcm = "" Then
                    fechaHoraFin_pcm = DBNull.Value
                End If
                Dim periodicidad_pcm As Integer = IIf(cmbEjecutarCada.SelectedValue <> "" AndAlso cmbEjecutarCada.SelectedValue <> "-1", cmbEjecutarCada.SelectedValue, 0)

                Dim tipoSeleccion_pcm As String = hddTipoSeleccionAlumnos.Value
                If rbtConfiguracionPredefinida.Checked Then
                    tipoSeleccion_pcm = ""
                End If

                Dim filtrosAlumno_pcm As String = ""
                Select Case tipoSeleccion_pcm
                    Case "M"
                        filtrosAlumno_pcm = hddFiltrosAlumno.Value
                    Case "I"
                        filtrosAlumno_pcm = "codigoUniver_Alu=" & hddCodUnivAlumnos.Value
                End Select
                Dim cod_usuario As String = Session("id_per")

                Dim lo_Pcm As New e_PenProgramacionCargoMasivo
                With lo_Pcm
                    .operacion = "I"
                    .codigo_pcm = codigo_pcm
                    .tipoConfiguracion_pcm = tipoConfiguracion_pcm
                    .codigo_cac = codigo_cac
                    .descripcion_pcm = descripcion_pcm
                    .tipoEjecucion_pcm = tipoEjecucion_pcm
                    .codigo_cpc = codigo_cpc
                    .fechaHoraInicio_pcm = fechaHoraInicio_pcm
                    .fechaHoraFin_pcm = fechaHoraFin_pcm
                    .periodicidad_pcm = periodicidad_pcm
                    .tipoSeleccion_pcm = tipoSeleccion_pcm
                    .filtrosAlumno_pcm = filtrosAlumno_pcm
                    .habilitado_pcm = True
                    .cod_usuario = cod_usuario
                End With

                'Datos de la deuda
                Dim lo_Dcm As New e_PenDatosCargoMasivo
                Dim codigo_dcm As Integer = hddCodD.Value

                With lo_Dcm
                    .codigo_dcm = codigo_dcm
                    .cod_usuario = cod_usuario
                End With


                If tipoConfiguracion_pcm = "M" Then
                    Dim tipo_dcm As String = IIf(rbtTipoConfigurado.Checked, rbtTipoConfigurado.Value, rbtTipoManual.Value)
                    Dim importe_dcm As Decimal = txtImporte.Text
                    Dim configuracion_dcm As String = IIf(cmbTipoDeuda.SelectedValue <> "-1", cmbTipoDeuda.SelectedValue, "")
                    Dim fechaVencimiento_dcm As String = txtFechaVencimiento.Value
                    Dim observacion_dcm As String = txtObservacion.Text
                    Dim codigo_sco As Integer = IIf(cmbServicioConcepto.SelectedValue <> "-1", cmbServicioConcepto.SelectedValue, 0)
                    Dim codigo_cco As Integer = IIf(cmbCentroCosto.SelectedValue <> "-1", cmbCentroCosto.SelectedValue, 0)

                    With lo_Dcm
                        .operacion = "I"
                        .tipo_dcm = tipo_dcm
                        .configuracion_dcm = configuracion_dcm
                        .importe_dcm = importe_dcm
                        .fechaVencimiento_dcm = fechaVencimiento_dcm
                        .observacion_dcm = observacion_dcm
                        .codigo_sco = codigo_sco
                        .codigo_cco = codigo_cco
                    End With
                Else
                    lo_Dcm.operacion = "D"
                End If

                Dim lo_Salida As Dictionary(Of String, String) = md_ProgramacionCargoMasivo.GuardarProgramacion(lo_Pcm, lo_Dcm)
                mt_GenerarToastServidor(lo_Salida.Item("rpta"), lo_Salida.Item("msg"))

                If lo_Salida.Item("rpta") = "1" Then
                    mt_Listar()
                    mt_SeleccionarTab("GENERAL", "L")
                End If

            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String, Optional ByVal control As String = "")
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg & "|control=" & control
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarToastServidor()
        Try
            hddParamsToastr.Value = ""
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String)
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "true"
            divMenServParametros.Attributes.Item("data-rpta") = ln_Rpta
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ls_Titulo
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ls_Mensaje
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarMensajeServidor()
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "false"
            divMenServParametros.Attributes.Item("data-rpta") = ""
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ""
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ""
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarModalAlumnos()
        Try
            divAlumnoParametros.Attributes.Item("data-mostrar") = "false"
            udpAlumnos.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
