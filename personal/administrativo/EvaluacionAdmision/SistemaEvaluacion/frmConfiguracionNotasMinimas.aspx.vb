Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class SistemaEvaluacion_frmConfiguracionNotasMinimas
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeConfiguracionNotaMinima As e_ConfiguracionNotaMinima, odConfiguracionNotaMinima As d_ConfiguracionNotaMinima
    Private oeConfiguracionNotaMinimaCompetencia As e_ConfiguracionNotaMinimaCompetencia, odConfiguracionNotaMinimaCompetencia As d_ConfiguracionNotaMinimaCompetencia
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
            mt_CargarComboFiltroCentroCosto()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnValoresPorDefecto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValoresPorDefecto.Click
        Try
            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            If codigoCac = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un semestre académico")
                Exit Sub
            End If

            Dim codigoCco As Integer = cmbFiltroCentroCosto.SelectedValue
            If codigoCco = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un centro de costos")
                Exit Sub
            End If

            Dim codigoCpf As Integer = cmbFiltroCarreraProfesional.SelectedValue
            If codigoCpf = -1 Then codigoCpf = 0

            mt_CargarFormNotasPorDefecto(codigoCco, codigoCpf)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfNotasPorDefecto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfNotasPorDefecto.Click
        Try
            mt_GuardarNotasPorDefecto()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            hddCac.Value = cmbFiltroCicloAcademico.SelectedValue
            hddCco.Value = cmbFiltroCentroCosto.SelectedValue
            hddCpf.Value = cmbFiltroCarreraProfesional.SelectedValue
            mt_Listar()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvList.DataBound
        Try
            Dim values As Dictionary(Of String, Dictionary(Of String, Object)) = fn_RetornaEstructuraRowspan("facultad")

            With values.Item("facultad")
                .Item(KEY_INDEX_COL) = 0
            End With
            mt_AgruparFilasCustom(grvList, values)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvList.RowCommand
        Try
            'NOTAS MÍNIMAS
            If e.CommandName = "Editar" Then
                Dim btnEditar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")

                btnEditar.Visible = False
                btnGuardar.Visible = True
                btnCancelar.Visible = True
                txtNotaMinima.Enabled = True

                btnEditar.Attributes.Item("data-old-value") = txtNotaMinima.Text.Trim
            End If

            If e.CommandName = "Guardar" Then
                Dim btnGuardar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardar.NamingContainer

                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")
                Dim hddCnm As HiddenField = row.FindControl("hddCnm")

                Dim notaMinima As Decimal
                If Not Decimal.TryParse(txtNotaMinima.Text.Trim, notaMinima) Then
                    mt_GenerarToastServidor(0, "El valor ingresado debe ser un número")
                    Exit Sub
                End If

                Dim codigoCnm As Integer = hddCnm.Value
                Dim codigoCpf As Integer = DirectCast(row.FindControl("hddCpf"), HiddenField).Value
                Dim codigoCco As Integer = DirectCast(row.FindControl("hddCco"), HiddenField).Value

                oeConfiguracionNotaMinima = New e_ConfiguracionNotaMinima : odConfiguracionNotaMinima = New d_ConfiguracionNotaMinima
                With oeConfiguracionNotaMinima
                    ._operacion = "UNM"
                    ._codigo_cnm = codigoCnm
                    ._codigo_cpf = codigoCpf
                    ._codigo_cco = codigoCco
                    ._nota_min_cnm = notaMinima
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odConfiguracionNotaMinima.fc_IUD(oeConfiguracionNotaMinima)
                If rpta.Item("rpta") = "1" Then
                    btnEditar.Visible = True
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False
                    txtNotaMinima.Enabled = False

                    mt_Listar()
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "Cancelar" Then
                Dim btnCancelar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")

                btnEditar.Visible = True
                btnGuardar.Visible = False
                btnCancelar.Visible = False
                txtNotaMinima.Enabled = False

                txtNotaMinima.Text = btnEditar.Attributes.Item("data-old-value")
            End If

            'NOTAS POR COMPETENCIA
            If e.CommandName = "Competencias" Then
                Dim btnEditarCompetencias As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditarCompetencias.NamingContainer

                Dim hddCnm As HiddenField = row.FindControl("hddCnm")
                Dim hddCpf As HiddenField = row.FindControl("hddCpf")
                mt_CargarFormNotaMinimaCompetencias(hddCnm.Value, hddCpf.Value)

                mt_MostrarModal("NMPC")
            End If

            udpGrvList.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells

                Dim hddCnm As HiddenField = e.Row.FindControl("hddCnm")
                Dim btnEditarCompetencias As LinkButton = e.Row.FindControl("btnEditarCompetencias")
                btnEditarCompetencias.Visible = (hddCnm.Value <> 0)

                Dim hddCco As HiddenField = e.Row.FindControl("hddCco")
                hddCco.Value = cmbFiltroCentroCosto.SelectedValue

                Dim dtNotasPorCompetencia As New Data.DataTable
                dtNotasPorCompetencia.Columns.Add("i", GetType(Integer))
                dtNotasPorCompetencia.Columns.Add("competencia", GetType(String))
                dtNotasPorCompetencia.Columns.Add("nota", GetType(Decimal))

                Dim dr As Data.DataRowView = e.Row.DataItem
                Dim notasPorCompetencia As String = dr.Item("notasPorCompetencia").ToString.Trim
                If notasPorCompetencia <> "" Then
                    Dim competencias() As String = notasPorCompetencia.Split("|")
                    Dim valores() As String
                    For i As Integer = 0 To competencias.Length - 1
                        valores = competencias(i).Split("=")
                        dtNotasPorCompetencia.Rows.Add(i + 1, valores(0), valores(1))
                    Next

                    Dim rptNotasPorCompetencia As Repeater = e.Row.FindControl("rptNotasPorCompetencia")
                    rptNotasPorCompetencia.DataSource = dtNotasPorCompetencia
                    rptNotasPorCompetencia.DataBind()
                End If
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'NOTAS POR COMPETENCIA
    Protected Sub grvNotaMinimaCompetencias_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvNotaMinimaCompetencias.RowCommand
        Try
            If e.CommandName = "Editar" Then
                Dim btnEditar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")

                btnEditar.Visible = False
                btnGuardar.Visible = True
                btnCancelar.Visible = True
                txtNotaMinima.Enabled = True

                btnEditar.Attributes.Item("data-old-value") = txtNotaMinima.Text.Trim
            End If

            If e.CommandName = "Guardar" Then
                Dim btnGuardar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardar.NamingContainer

                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")
                Dim hddCnc As HiddenField = row.FindControl("hddCnc")

                Dim notaMinima As Decimal
                If Not Decimal.TryParse(txtNotaMinima.Text.Trim, notaMinima) Then
                    mt_GenerarToastServidor(0, "El valor ingresado debe ser un número")
                    Exit Sub
                End If

                Dim codigoCnc As Integer = hddCnc.Value
                Dim codigoCom As Integer = DirectCast(row.FindControl("hddCom"), HiddenField).Value

                oeConfiguracionNotaMinimaCompetencia = New e_ConfiguracionNotaMinimaCompetencia : odConfiguracionNotaMinimaCompetencia = New d_ConfiguracionNotaMinimaCompetencia
                With oeConfiguracionNotaMinimaCompetencia
                    ._operacion = "I"
                    ._codigo_cnc = codigoCnc
                    ._codigo_cnm = hddCod.Value
                    ._codigo_com = codigoCom
                    ._nota_min_cnc = notaMinima
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odConfiguracionNotaMinimaCompetencia.fc_IUD(oeConfiguracionNotaMinimaCompetencia)
                If rpta.Item("rpta") = "1" Then
                    btnEditar.Visible = True
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False
                    txtNotaMinima.Enabled = False
                    hddCnc.Value = rpta.Item("cod")

                    mt_Listar()
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "Cancelar" Then
                Dim btnCancelar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim txtNotaMinima As TextBox = row.FindControl("txtNotaMinima")

                btnEditar.Visible = True
                btnGuardar.Visible = False
                btnCancelar.Visible = False
                txtNotaMinima.Enabled = False

                txtNotaMinima.Text = btnEditar.Attributes.Item("data-old-value")
            End If

            udpGrvNotaMinimaCompetencia.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvNotaMinimaCompetencias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvNotaMinimaCompetencias.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim cellsRow As TableCellCollection = e.Row.Cells
                cellsRow(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos"
    'LISTA
    Private Sub mt_Init()
        Try
            divGrvList.Visible = False

            mt_CargarComboFiltroCicloAcademico()
            mt_CargarComboFiltroCarreraProfesional()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
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

    Private Sub mt_CargarComboFiltroCentroCosto()
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

    Private Sub mt_CargarComboFiltroCarreraProfesional()
        Try
            ClsGlobales.mt_LlenarListas(cmbFiltroCarreraProfesional, ClsGlobales.fc_ListarCarreraProfesional(), "codigo_cpf", "nombre_cpf", "TODOS")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim codigoCac As Integer = hddCac.Value
            Dim codigoCco As Integer = hddCco.Value
            Dim codigoCpf As Integer = hddCpf.Value
            If codigoCpf = -1 Then codigoCpf = 0

            btnValoresPorDefecto.Visible = False

            If codigoCac = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un semestre académico")
                Exit Sub
            End If

            If codigoCco = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un centro de costos")
                Exit Sub
            End If

            btnValoresPorDefecto.Visible = True
            udpFiltros.Update()

            oeConfiguracionNotaMinima = New e_ConfiguracionNotaMinima : odConfiguracionNotaMinima = New d_ConfiguracionNotaMinima
            With oeConfiguracionNotaMinima
                ._tipoConsulta = "LC" 'Listar Carreras
                ._codigo_test = 2 'Pregado
                ._codigo_cpf = codigoCpf
                ._codigo_cco = codigoCco
            End With

            Dim dt As Data.DataTable = odConfiguracionNotaMinima.fc_Listar(oeConfiguracionNotaMinima)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarFormNotasPorDefecto(ByVal codigoCco As Integer, ByVal codigoCpf As Integer)
        Try
            hddCco.Value = codigoCco
            hddCpf.Value = codigoCpf

            Dim carrera As String = ""
            If codigoCpf = 0 Then
                carrera = "TODOS"
            Else
                Dim dtCarreraProfesional As Data.DataTable = ClsGlobales.fc_ListarCarreraProfesional("CO", codigoCpf)
                If dtCarreraProfesional.Rows.Count > 0 Then
                    carrera = dtCarreraProfesional.Rows(0).Item("nombre_Cpf")
                End If
            End If

            txtCarreraProfesional.Text = carrera
            txtNota.Text = ""
            txtNotaNivelacion.Text = ""
            chkConfNotasPorDefecto.Checked = False

            udpNotasPorDefecto.Update()

            mt_MostrarModal("NPD")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GuardarNotasPorDefecto()
        Try
            oeConfiguracionNotaMinima = New e_ConfiguracionNotaMinima : odConfiguracionNotaMinima = New d_ConfiguracionNotaMinima

            Dim nota As Decimal, notaNivelacion As Decimal

            Dim sNota As String = txtNota.Text.Trim
            Dim sNotaNivelacion As String = txtNotaNivelacion.Text.Trim

            If String.IsNullOrEmpty(sNota) Then
                mt_GenerarToastServidor(0, "Debe indicar la nóta mínima de ingreso")
                Exit Sub
            End If

            If String.IsNullOrEmpty(sNotaNivelacion) Then
                mt_GenerarToastServidor(0, "Debe indicar la nóta mínima para nivelación")
                Exit Sub
            End If

            If Not Decimal.TryParse(sNota, nota) Then
                mt_GenerarToastServidor(0, "La nota ingresada debe ser un número")
                Exit Sub
            End If

            If Not Decimal.TryParse(sNotaNivelacion, notaNivelacion) Then
                mt_GenerarToastServidor(0, "La nota para nivelación ingresada debe ser un número")
                Exit Sub
            End If

            If Not chkConfNotasPorDefecto.Checked Then
                mt_GenerarToastServidor(0, "Debe marcar el check de conformidad")
                Exit Sub
            End If

            If nota > 20 OrElse nota < 0 Then
                mt_GenerarToastServidor(0, "La nota debe ser un número comprendido entre 0 y 20")
                Exit Sub
            End If

            If notaNivelacion > 20 OrElse notaNivelacion < 0 Then
                mt_GenerarToastServidor(0, "La nota de nivelación debe ser un número comprendido entre 0 y 20")
                Exit Sub
            End If

            With oeConfiguracionNotaMinima
                ._codigo_test = 2
                ._codigo_cco = hddCco.Value
                ._codigo_cpf = hddCpf.Value
                ._nota_min_cnm = nota
                ._nota_min_competencia = notaNivelacion
                ._cod_usuario = Request.QueryString("id")
            End With

            Dim rpta As Dictionary(Of String, String) = odConfiguracionNotaMinima.fc_AsignarNotasMinimasPorDefecto(oeConfiguracionNotaMinima)
            If rpta.Item("rpta") = "1" Then
                mt_OcultarModal("NPD")
                mt_Listar()
            End If

            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
        Catch ex As Exception
            mt_GenerarToastServidor(-1, ex.Message)
        End Try
    End Sub

    'NOTAS POR COMPETENCIA
    Private Sub mt_CargarFormNotaMinimaCompetencias(ByVal codigoCnm As Integer, ByVal codigoCpf As Integer)
        Try
            oeConfiguracionNotaMinimaCompetencia = New e_ConfiguracionNotaMinimaCompetencia : odConfiguracionNotaMinimaCompetencia = New d_ConfiguracionNotaMinimaCompetencia
            With oeConfiguracionNotaMinimaCompetencia
                ._tipoConsulta = "LC"
                ._codigo_cnm = codigoCnm
                ._codigo_cpf = codigoCpf
            End With

            Dim dt As Data.DataTable = odConfiguracionNotaMinimaCompetencia.fc_Listar(oeConfiguracionNotaMinimaCompetencia)
            grvNotaMinimaCompetencias.DataSource = dt
            grvNotaMinimaCompetencias.DataBind()
            divCeroElementos.Visible = dt.Rows.Count = 0
            udpNotaMinimaCompetencias.Update()

            hddCod.Value = codigoCnm

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
