Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class frmVacantesEvento
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeVacantesEvento As e_VacantesEvento, odVacantesEvento As d_VacantesEvento
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
        Else
            mt_RefreshGridView()
        End If
        mt_LimpiarParametros()
    End Sub

    Protected Sub grvList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grvList.DataBound
        Try
            ' Dim values As Dictionary(Of String, Dictionary(Of String, Object)) = fn_RetornaEstructuraRowspan("carrera", "modalidad")
            ' With values.Item("carrera")
            '     .Item(KEY_INDEX_COL) = 1
            ' End With
            ' With values.Item("modalidad")
            '     .Item(KEY_INDEX_COL) = 2
            ' End With
            ' mt_AgruparFilasCustom(grvList, values)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvList.RowCommand
        Try
            If e.CommandName = "Editar" Then
                Dim btnEditar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtVacantes As TextBox = row.FindControl("txtVacantes")

                btnEditar.Visible = False
                btnGuardar.Visible = True
                btnCancelar.Visible = True
                txtVacantes.Enabled = True

                btnEditar.Attributes.Item("data-old-value") = txtVacantes.Text.Trim
            End If

            If e.CommandName = "Guardar" Then
                Dim btnGuardar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardar.NamingContainer

                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")
                Dim txtVacantes As TextBox = row.FindControl("txtVacantes")
                Dim hddVae As HiddenField = row.FindControl("hddVae")

                Dim vacantesVae As Integer
                If Not Integer.TryParse(txtVacantes.Text.Trim, vacantesVae) Then
                    mt_GenerarToastServidor(0, "El valor ingresado debe ser un número entero")
                    Exit Sub
                End If

                Dim codigoVae As Integer = hddVae.Value
                Dim codigoCco As Integer = DirectCast(row.FindControl("hddCco"), HiddenField).Value
                Dim codigoVac As Integer = DirectCast(row.FindControl("hddVac"), HiddenField).Value

                oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
                With oeVacantesEvento
                    ._operacion = "UV"
                    ._codigo_vae = codigoVae
                    ._codigo_cco = codigoCco
                    ._codigo_vac = codigoVac
                    ._cantidad_vae = vacantesVae
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odVacantesEvento.fc_IUD(oeVacantesEvento)
                If rpta.Item("rpta") = "1" Then
                    btnEditar.Visible = True
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False
                    txtVacantes.Enabled = False
                    hddVae.Value = rpta.Item("cod")
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "Cancelar" Then
                Dim btnCancelar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim txtVacantes As TextBox = row.FindControl("txtVacantes")

                btnEditar.Visible = True
                btnGuardar.Visible = False
                btnCancelar.Visible = False
                txtVacantes.Enabled = False

                txtVacantes.Text = btnEditar.Attributes.Item("data-old-value")
            End If

            If e.CommandName = "EditarAccesitarios" Then
                Dim btnEditarAccesitarios As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditarAccesitarios.NamingContainer

                Dim btnGuardarAccesitarios As LinkButton = row.FindControl("btnGuardarAccesitarios")
                Dim btnCancelarAccesitarios As LinkButton = row.FindControl("btnCancelarAccesitarios")
                Dim txtAccesitarios As TextBox = row.FindControl("txtAccesitarios")

                btnEditarAccesitarios.Visible = False
                btnGuardarAccesitarios.Visible = True
                btnCancelarAccesitarios.Visible = True
                txtAccesitarios.Enabled = True

                btnEditarAccesitarios.Attributes.Item("data-old-value") = txtAccesitarios.Text.Trim
            End If

            If e.CommandName = "GuardarAccesitarios" Then
                Dim btnGuardarAccesitarios As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardarAccesitarios.NamingContainer

                Dim btnEditarAccesitarios As LinkButton = row.FindControl("btnEditarAccesitarios")
                Dim btnCancelarAccesitarios As LinkButton = row.FindControl("btnCancelarAccesitarios")
                Dim txtAccesitarios As TextBox = row.FindControl("txtAccesitarios")
                Dim hddVae As HiddenField = row.FindControl("hddVae")

                Dim accesitariosVae As Integer
                If Not Integer.TryParse(txtAccesitarios.Text.Trim, accesitariosVae) Then
                    mt_GenerarToastServidor(0, "El valor ingresado debe ser un número entero")
                    Exit Sub
                End If

                Dim codigoVae As Integer = hddVae.Value
                Dim codigoCco As Integer = DirectCast(row.FindControl("hddCco"), HiddenField).Value
                Dim codigoVac As Integer = DirectCast(row.FindControl("hddVac"), HiddenField).Value

                oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
                With oeVacantesEvento
                    ._operacion = "UA"
                    ._codigo_vae = codigoVae
                    ._codigo_cco = codigoCco
                    ._codigo_vac = codigoVac
                    ._cantidad_accesitarios_vae = accesitariosVae
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odVacantesEvento.fc_IUD(oeVacantesEvento)
                If rpta.Item("rpta") = "1" Then
                    btnEditarAccesitarios.Visible = True
                    btnGuardarAccesitarios.Visible = False
                    btnCancelarAccesitarios.Visible = False
                    txtAccesitarios.Enabled = False
                    hddVae.Value = rpta.Item("cod")
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "CancelarAccesitarios" Then
                Dim btnCancelarAccesitarios As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelarAccesitarios.NamingContainer

                Dim btnGuardarAccesitarios As LinkButton = row.FindControl("btnGuardarAccesitarios")
                Dim btnEditarAccesitarios As LinkButton = row.FindControl("btnEditarAccesitarios")
                Dim txtAccesitarios As TextBox = row.FindControl("txtAccesitarios")

                btnEditarAccesitarios.Visible = True
                btnGuardarAccesitarios.Visible = False
                btnCancelarAccesitarios.Visible = False
                txtAccesitarios.Enabled = False

                txtAccesitarios.Text = btnEditarAccesitarios.Attributes.Item("data-old-value")
            End If

            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim codigo As String = grvList.DataKeys(e.Row.RowIndex).Values.Item("codigo_vae")
                Dim index As Integer = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_InitComboFiltroCentroCostos()
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

    'Eventos delegados
    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("data-cod")


        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        divGrvList.Visible = False

        mt_InitComboFiltroCicloAcademico()
        mt_InitComboFiltroCarreraProfesional()
        mt_InitComboFiltroModalidadIngreso()
        mt_InitComboFiltroCentroCostos()
    End Sub

    Private Sub mt_LimpiarParametros()
        Try
            hddTipoVista.Value = ""
            hddParamsToastr.Value = ""

            'Parámetros para mensajes desde el servidor
            hddMenServMostrar.Value = "false"
            hddMenServRpta.Value = ""
            hddMenServTitulo.Value = ""
            hddMenServMensaje.Value = ""

            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboFiltroCicloAcademico()
        Try
            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
            ClsGlobales.mt_LlenarListas(cmbFiltroCicloAcademico, odVacantesEvento.fc_ListarCicloAcademico(), "codigo_cac", "descripcion_cac", "-- Seleccione --")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_InitComboFiltroCarreraProfesional()
        Try
            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
            ClsGlobales.mt_LlenarListas(cmbFiltroCarreraProfesional, odVacantesEvento.fc_ListarCarreraProfesional(), "codigo_cpf", "nombre_cpf", "TODOS")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_InitComboFiltroModalidadIngreso()
        Try
            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
            ClsGlobales.mt_LlenarListas(cmbFiltroModalidadIngreso, odVacantesEvento.fc_ListarModalidadIngreso(), "codigo_min", "nombre_Min", "-- Seleccione --")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboFiltroCentroCostos()
        Try
            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento

            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            If codigoCac = -1 Then
                codigoCac = 0
            End If

            'PENDIENTE!: En vez de 684 debe ir el id de sesión
            'Dim codUsuario As Integer = Request.QueryString("id")
            Dim codUsuario As Integer = 684
            ClsGlobales.mt_LlenarListas(cmbFiltroCentroCostos, odVacantesEvento.fc_ListarCentroCostos("GEN", codigoCac, codUsuario), "codigo_cco", "descripcion_cco", "TODOS")
            udpFiltros.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            If codigoCac = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar un semestre académico")
                Exit Sub
            End If

            Dim codigoMin As Integer = cmbFiltroModalidadIngreso.SelectedValue
            If codigoMin = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar una modalidad")
                Exit Sub
            End If

            Dim codigoCpf As Integer = cmbFiltroCarreraProfesional.SelectedValue
            Dim codigoCco As Integer = cmbFiltroCentroCostos.SelectedValue

            If codigoCpf = -1 Then codigoCpf = 0
            If codigoMin = -1 Then codigoMin = 0
            If codigoCco = -1 Then codigoCco = 0

            oeVacantesEvento = New e_VacantesEvento : odVacantesEvento = New d_VacantesEvento
            With oeVacantesEvento
                ._tipoConsulta = "VAC" 'Datos personales y cargo
                ._codigo_cac = codigoCac
                ._codigo_cpf = codigoCpf
                ._codigo_min = codigoMin
                ._codigo_cco = codigoCco
            End With

            Dim dt As Data.DataTable = odVacantesEvento.fc_Listar(oeVacantesEvento)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub mt_RefreshGridView()
        Try
            For Each _Row As GridViewRow In grvList.Rows
                grvList_RowDataBound(grvList, New GridViewRowEventArgs(_Row))
            Next
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
