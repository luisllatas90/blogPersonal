Imports ClsSistemaEvaluacion
Imports ClsTomarEvaluacion
Imports System.Collections.Generic

Partial Class TomarEvaluacion_frmRegistroAsistencia
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeAsistenciaEvaluacion As e_AsistenciaEvaluacion, odAsistenciaEvaluacion As d_AsistenciaEvaluacion
    Private oeIncidenciaEvaluacion As e_IncidenciaEvaluacion, odIncidenciaEvaluacion As d_IncidenciaEvaluacion
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
        End If
        mt_LimpiarParametros()
    End Sub

    'LISTA
    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboFiltroCentroCostos()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        Try
            mt_Listar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells
                cellsRow(1).Text = e.Row.RowIndex + 1

                Dim hddAsignado As HiddenField = e.Row.FindControl("hddAsignado")
                Dim hddAsistencia As HiddenField = e.Row.FindControl("hddAsistencia")
                Dim hddCerrado As HiddenField = e.Row.FindControl("hddCerrado")
                If hddAsignado.Value = 0 Or hddAsistencia.Value = 0 Or hddCerrado.Value <> 0 Then
                    Dim divActivarCierre As HtmlGenericControl = e.Row.FindControl("divActivarCierre")
                    divActivarCierre.Visible = False
                End If
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvList.RowCommand
        Try
            Select Case e.CommandName
                Case "Asistencia"
                    Dim codigo As Integer = e.CommandArgument
                    Dim hddAsignado As HiddenField = DirectCast(e.CommandSource, LinkButton).Parent.FindControl("hddAsignado")
                    If hddAsignado.Value = 0 Then
                        mt_GenerarToastServidor(0, "No hay ningún alumno asignado a este grupo")
                    Else
                        Dim hddCerrado As HiddenField = DirectCast(e.CommandSource, LinkButton).Parent.FindControl("hddCerrado")
                        mt_CargarForm(codigo, hddCerrado.Value)
                        mt_SeleccionarTab("M")
                    End If
                Case "Incidencia"
                    Dim codigo As Integer = e.CommandArgument
                    mt_CargarFormRegistroIncidencia(codigo)
                Case "VisualizarIncidencias"
                    Dim codigo As Integer = e.CommandArgument
                    mt_CargarFormVisualizarIncidencias(codigo)
            End Select
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCerrarAsistencia_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarAsistencia.ServerClick
        Try
            Dim gruposSeleccionados As String = ""
            For Each row As GridViewRow In grvList.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSeleccionarGrupo As HtmlInputCheckBox = row.FindControl("chkSeleccionarGrupo")
                    If chkSeleccionarGrupo.Checked Then
                        gruposSeleccionados += "<li>" + row.Cells(3).Text + "</li>"
                    End If
                End If
            Next

            If gruposSeleccionados.Trim = "" Then
                mt_GenerarToastServidor(0, "No ha seleccionado ningún grupo para realizar el cierre")
                Exit Sub
            End If
            txtFechaCierreAsistencias.Text = ""
            spnGrupos.InnerHtml = "<ul>" + gruposSeleccionados + "</ul>"
            udpCerrarAsistencias.Update()

            mt_MostrarModal("CA")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfCerrarAsistencias_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfCerrarAsistencias.ServerClick
        Try
            oeAsistenciaEvaluacion = New e_AsistenciaEvaluacion : odAsistenciaEvaluacion = New d_AsistenciaEvaluacion

            Dim fechaCierre As String = txtFechaCierreAsistencias.Text.Trim
            If fechaCierre = "" Then
                mt_GenerarToastServidor(0, "Debe indicar la fecha de cierre")
                Exit Sub
            End If

            Dim resultado As New Dictionary(Of String, String)
            For Each row As GridViewRow In grvList.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkSeleccionarGrupo As HtmlInputCheckBox = row.FindControl("chkSeleccionarGrupo")
                    If chkSeleccionarGrupo.Checked Then
                        With oeAsistenciaEvaluacion
                            .operacion = "C" 'Cerrar
                            .codigoAse = 0
                            .codigoGru = DirectCast(row.FindControl("hddCodigoGru"), HiddenField).Value
                            .fechaCierreAse = txtFechaCierreAsistencias.Text.Trim
                            .codUsuario = Request.QueryString("id")
                        End With

                        resultado = odAsistenciaEvaluacion.fc_IUD(oeAsistenciaEvaluacion)
                        If resultado.Item("rpta") <> "1" Then
                            mt_GenerarToastServidor(resultado.Item("rpta"), resultado.Item("msg"))
                            Exit Sub
                        End If
                    End If
                End If
            Next

            mt_OcultarModal("CA")

            mt_Listar()
            mt_GenerarToastServidor(1, "Operación realizada correctamente")

        Catch ex As Exception
            mt_GenerarToastServidor(-1, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfRegistrarIncidencia_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfRegistrarIncidencia.ServerClick
        Try
            oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion

            Dim incidencia As String = txtIncidencia.Text.Trim
            If incidencia = "" Then
                mt_GenerarToastServidor(0, "Debe indicar el motivo de la incidencia")
                Exit Sub
            End If

            With oeIncidenciaEvaluacion
                .operacion = "I"
                .codigoIne = 0
                .codigoGru = hddCod.Value
                .descripcionIne = incidencia
                .codUsuario = Request.QueryString("id")
            End With

            Dim resultado As Dictionary(Of String, String) = odIncidenciaEvaluacion.fc_IUD(oeIncidenciaEvaluacion)
            If resultado.Item("rpta") = "1" Then
                mt_OcultarModal("RI")
            End If
            mt_GenerarToastServidor(resultado.Item("rpta"), resultado.Item("msg"))

        Catch ex As Exception
            mt_GenerarToastServidor(-1, ex.Message)
        End Try
    End Sub

    'MANTENIMIENTO
    Protected Sub btnCancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick
        Try
            mt_SeleccionarTab("L")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvAsistencia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAsistencia.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells
                cellsRow(0).Text = e.Row.RowIndex + 1

                Dim data As Data.DataRowView = e.Row.DataItem

                Dim hddFechaCierre As HiddenField = e.Row.FindControl("hddFechaCierre")
                Dim cerrado As Boolean = hddFechaCierre.Value.Trim <> ""

                Dim divActivarEdicion As HtmlGenericControl = e.Row.FindControl("divActivarEdicion")

                Dim btnNoAsistio As HtmlGenericControl = e.Row.FindControl("btnNoAsistio")
                Dim btnAsistio As HtmlGenericControl = e.Row.FindControl("btnAsistio")
                Dim btnTardanza As HtmlGenericControl = e.Row.FindControl("btnTardanza")

                Dim rbtNoAsistio As HtmlInputRadioButton = e.Row.FindControl("rbtNoAsistio")
                Dim rbtAsistio As HtmlInputRadioButton = e.Row.FindControl("rbtAsistio")
                Dim rbtTardanza As HtmlInputRadioButton = e.Row.FindControl("rbtTardanza")

                Dim hddSaldo As HiddenField = e.Row.FindControl("hddSaldo")
                Dim saldo As Decimal = hddSaldo.Value
                If cerrado OrElse saldo > 0 Then
                    btnNoAsistio.Attributes.Item("class") = btnNoAsistio.Attributes.Item("class") & " disabled"
                    btnAsistio.Attributes.Item("class") = btnAsistio.Attributes.Item("class") & " disabled"
                    btnTardanza.Attributes.Item("class") = btnTardanza.Attributes.Item("class") & " disabled"
                End If

                If cerrado OrElse Request.QueryString("ctf") <> "175" OrElse saldo = 0 Then divActivarEdicion.Visible = False 'PENDIENTE!

                Select Case data.Item("estadoasistencia_ase").ToString.Trim
                    Case "N"
                        btnNoAsistio.Attributes.Item("class") = btnNoAsistio.Attributes.Item("class") & " active"
                        rbtNoAsistio.Checked = True
                    Case "A"
                        btnAsistio.Attributes.Item("class") = btnAsistio.Attributes.Item("class") & " active"
                        rbtAsistio.Checked = True
                    Case "T"
                        btnTardanza.Attributes.Item("class") = btnTardanza.Attributes.Item("class") & " active"
                        rbtTardanza.Checked = True
                End Select

                grvAsistencia.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
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
#End Region

#Region "Métodos"
    'LISTA
    Private Sub mt_Init()
        divGrvList.Visible = False
        mt_CargarComboFiltroCicloAcademico()
    End Sub

    Private Sub mt_CargarComboFiltroCicloAcademico()
        Try
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCicloAcademico()
            ClsGlobales.mt_LlenarListas(cmbFiltroCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            cmbFiltroCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboFiltroCentroCostos()
        Try
            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            Dim codUsuario As Integer = Request.QueryString("id")
            Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, codUsuario)
            ClsGlobales.mt_LlenarListas(cmbFiltroCentroCosto, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim codigoCco As Integer = cmbFiltroCentroCosto.SelectedValue
            Dim codigoGru As Integer = -1

            'Validaciones
            If cmbFiltroCicloAcademico.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un ciclo académico")
                Exit Sub
            End If
            If codigoCco = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un centro de costos")
                Exit Sub
            End If

            oeAsistenciaEvaluacion = New e_AsistenciaEvaluacion : odAsistenciaEvaluacion = New d_AsistenciaEvaluacion
            With oeAsistenciaEvaluacion
                .tipoConsulta = "LT"
                .codigoCco = codigoCco
                .codigoTge = 0
            End With

            Dim dt As Data.DataTable = odAsistenciaEvaluacion.fc_Listar(oeAsistenciaEvaluacion)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarFormRegistroIncidencia(ByVal codigoGru As Integer)
        Try
            oeAsistenciaEvaluacion = New e_AsistenciaEvaluacion : odAsistenciaEvaluacion = New d_AsistenciaEvaluacion
            With oeAsistenciaEvaluacion
                .tipoConsulta = "LT"
                .codigoAse = 0
                .codigoGru = codigoGru
            End With

            Dim dt As Data.DataTable = odAsistenciaEvaluacion.fc_Listar(oeAsistenciaEvaluacion)
            If dt.Rows.Count > 0 Then
                txtGrupoAdmision.Text = dt.Rows(0).Item("nombre_amb")
            Else
                mt_GenerarToastServidor(0, "No se ha encontrado grupo con código " & codigoGru)
                Exit Sub
            End If

            hddCod.Value = codigoGru
            txtIncidencia.Text = ""

            udpRegistrarIncidencia.Update()

            mt_MostrarModal("RI")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarFormVisualizarIncidencias(ByVal codigoGru As Integer)
        Try
            oeIncidenciaEvaluacion = New e_IncidenciaEvaluacion : odIncidenciaEvaluacion = New d_IncidenciaEvaluacion
            With oeIncidenciaEvaluacion
                .tipoConsulta = "GEN"
                .codigoGru = codigoGru
            End With

            Dim dt As Data.DataTable = odIncidenciaEvaluacion.fc_Listar(oeIncidenciaEvaluacion)
            rptIncidencia.DataSource = dt
            rptIncidencia.DataBind()
            udpVisualizarIncidencias.Update()

            mt_MostrarModal("VI")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'MANTENIMIENTO
    Private Sub mt_CargarForm(ByVal codigo As Integer, ByVal cerrado As Boolean)
        Try
            hddCod.Value = codigo
            udpParams.Update()

            If codigo <> 0 Then
                oeAsistenciaEvaluacion = New e_AsistenciaEvaluacion : odAsistenciaEvaluacion = New d_AsistenciaEvaluacion
                With oeAsistenciaEvaluacion
                    .tipoConsulta = "REGAS"
                    .codigoGru = codigo
                End With

                Dim dt As Data.DataTable = odAsistenciaEvaluacion.fc_Listar(oeAsistenciaEvaluacion)
                grvAsistencia.DataSource = dt
                grvAsistencia.DataBind()
            End If
            spnGrupoCerrado.Visible = cerrado
            btnFakeGuardar.Disabled = cerrado

            udpMantenimiento.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Guardar()
        Try
            odAsistenciaEvaluacion = New d_AsistenciaEvaluacion
            Dim codUsuario As Integer = Request.QueryString("id")

            Dim lstAsistenciaEvaluacion As New List(Of e_AsistenciaEvaluacion)

            For Each row As GridViewRow In grvAsistencia.Rows
                oeAsistenciaEvaluacion = New e_AsistenciaEvaluacion
                With oeAsistenciaEvaluacion
                    .operacion = "I"
                    .codigoAse = 0
                    .codigoGru = hddCod.Value
                    .codUsuario = codUsuario
                End With

                If row.RowType = DataControlRowType.DataRow Then
                    Dim rbtNoAsistio As HtmlInputRadioButton = row.FindControl("rbtNoAsistio")
                    Dim rbtAsistio As HtmlInputRadioButton = row.FindControl("rbtAsistio")
                    Dim rbtTardanza As HtmlInputRadioButton = row.FindControl("rbtTardanza")

                    Dim hddCodAlu As HiddenField = row.FindControl("hddCodAlu")

                    Dim estadoAsistencia As String = ""
                    If rbtNoAsistio.Checked Then estadoAsistencia = "N"
                    If rbtAsistio.Checked Then estadoAsistencia = "A"
                    If rbtTardanza.Checked Then estadoAsistencia = "T"

                    If estadoAsistencia = "" Then
                        mt_GenerarToastServidor(0, "Debe marcar la asistencia para todos los alumnos")
                    End If

                    With oeAsistenciaEvaluacion
                        .codigoAlu = hddCodAlu.Value
                        .estadoAsistenciaAse = estadoAsistencia
                    End With

                    lstAsistenciaEvaluacion.Add(oeAsistenciaEvaluacion)
                End If
            Next

            Dim rpta As Dictionary(Of String, String) = odAsistenciaEvaluacion.fc_MasivoIUD(lstAsistenciaEvaluacion)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
                mt_SeleccionarTab("L")
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'OTROS MÉTODOS
    Private Sub mt_MostrarModal(ByVal tipo As String)
        Try
            hddTipoModalAccion.Value = tipo
            hddMostrarModalAccion.Value = "true"
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_OcultarModal(ByVal tipo As String)
        Try
            hddTipoModalAccion.Value = tipo
            hddOcultarModalAccion.Value = "true"
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""

        'Parámetros para mensajes desde el servidor
        hddMenServMostrar.Value = "false"
        hddMenServRpta.Value = ""
        hddMenServTitulo.Value = ""
        hddMenServMensaje.Value = ""

        'Acciones adicionales
        hddTipoModalAccion.Value = ""
        hddMostrarModalAccion.Value = "false"
        hddOcultarModalAccion.Value = "false"

        udpParams.Update()
    End Sub

    Private Sub mt_SeleccionarTab(ByVal tipo As String)
        Try
            hddTipoVista.Value = tipo
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String)
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarMensajeServidor(ByVal titulo As String, ByVal rpta As Integer, ByVal mensaje As String)
        Try
            hddMenServMostrar.Value = "true"
            hddMenServRpta.Value = rpta
            hddMenServTitulo.Value = titulo
            hddMenServMensaje.Value = mensaje

            udpParams.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

End Class
