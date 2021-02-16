Imports ClsBancoPreguntas
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class BancoPreguntas_frmComisionAdmision
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeComisionPermanente As e_ComisionPermanente, odComisionPermanente As d_ComisionPermanente
    Private oeCargoComision As e_CargoComision, odCargoComision As d_CargoComision
    Private oeCompetenciaAprendizaje As e_CompetenciaAprendizaje, odCompetenciaAprendizaje As d_CompetenciaAprendizaje
    Private oeComisionPermanenteCompetencia As e_ComisionPermanenteCompetencia, odComisionPermanenteCompetencia As d_ComisionPermanenteCompetencia
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

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim codigoCop As String = grvList.DataKeys(e.Row.RowIndex).Values.Item("codigo_cop")
                Dim index As Integer = e.Row.RowIndex + 1
                _cellsRow(0).Text = index

                'Editar
                Dim btnEditar As HtmlButton = e.Row.FindControl("btnEditar")
                With btnEditar
                    .Attributes.Add("data-cod", codigoCop)
                    AddHandler .ServerClick, AddressOf btnEditar_Click
                End With

                'Desactivar
                Dim btnDesactivar As HtmlButton = e.Row.FindControl("btnDesactivar")
                With btnDesactivar
                    .Attributes.Add("data-cod", codigoCop)
                    AddHandler .ServerClick, AddressOf btnDesactivar_Click
                End With

                'Activar
                Dim btnActivar As HtmlButton = e.Row.FindControl("btnActivar")
                With btnActivar
                    .Attributes.Add("data-cod", codigoCop)
                    AddHandler .ServerClick, AddressOf btnActivar_Click
                End With

                'Eliminar
                Dim btnEliminar As HtmlButton = e.Row.FindControl("btnEliminar")
                With btnEliminar
                    .Attributes.Add("data-cod", codigoCop)
                    AddHandler .ServerClick, AddressOf btnEliminar_Click
                End With
            End If
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

    Protected Sub btnAgregar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.ServerClick
        Try
            mt_SeleccionarTab("M")
            mt_CargarForm(0)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.ServerClick
        Try
            mt_SeleccionarTab("L")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.ServerClick
        Try
            mt_Guardar("GS") 'Guardar y salir
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnGuardarContinuar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarContinuar.ServerClick
        Try
            mt_Guardar("GC") 'Guardar y continuar
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Eventos delegados
    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("data-cod")
            mt_CargarForm(codigo)
            mt_SeleccionarTab("M")

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub btnDesactivar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigoCop As String = button.Attributes("data-cod")
            mt_Vigencia(codigoCop, False)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub btnActivar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("data-cod")
            mt_Vigencia(codigo, True)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim codigo As String = button.Attributes("data-cod")
            mt_Eliminar(codigo)

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        divGrvList.Visible = False
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""

        'Parámetros para mensajes desde el servidor
        hddMenServMostrar.Value = "false"
        hddMenServRpta.Value = ""
        hddMenServTitulo.Value = ""
        hddMenServMensaje.Value = ""

        udpParams.Update()
    End Sub

    Private Sub mt_InitComboPersonal()
        Try
            oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente
            ClsGlobales.mt_LlenarListas(cmbPersonal, odComisionPermanente.fc_ListarPersonal(), "codigo_per", "nombres", "-- Seleccione --")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_InitComboCargoComision()
        Try
            oeCargoComision = New e_CargoComision : odCargoComision = New d_CargoComision
            With oeCargoComision
                .operacion = "GEN"
            End With
            ClsGlobales.mt_LlenarListas(cmbCargoComision, odCargoComision.fc_Listar(oeCargoComision), "codigo_ccm", "nombre_ccm", "-- Seleccione --")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_InitComboCompetencias()
        Try
            oeCompetenciaAprendizaje = New e_CompetenciaAprendizaje : odCompetenciaAprendizaje = New d_CompetenciaAprendizaje
            ClsGlobales.mt_LlenarListas(cmbCompetencia, odCompetenciaAprendizaje.fc_Listar(oeCompetenciaAprendizaje), "codigo_com", "nombre_com")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente

            Dim vigenteCop As Object = cmbFiltroEstado.SelectedValue
            Select Case vigenteCop
                Case -1
                    vigenteCop = DBNull.Value
                Case 0
                    vigenteCop = False
                Case 1
                    vigenteCop = True
            End Select

            With oeComisionPermanente
                .tipoConsulta = "PC" 'Datos personales y cargo
                .vigenteCop = vigenteCop
                .nroResolucionCop = "%" & txtFiltroNroResolucion.Text.Trim & "%"
            End With

            Dim dt As Data.DataTable = odComisionPermanente.fc_Listar(oeComisionPermanente)
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

    Private Sub mt_CargarForm(ByVal codigo As Integer)
        Try
            hddCod.Value = codigo
            udpParams.Update()

            mt_InitComboPersonal()
            mt_InitComboCargoComision()
            mt_InitComboCompetencias()

            mt_LimpiarForm()

            btnGuardarContinuar.Visible = (codigo = 0)

            If codigo <> 0 Then
                oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente
                With oeComisionPermanente
                    .codigoCop = codigo
                End With

                Dim dt As Data.DataTable = odComisionPermanente.fc_Listar(oeComisionPermanente)
                If dt.Rows.Count > 0 Then
                    cmbPersonal.SelectedValue = dt.Rows(0).Item("codigo_per")
                    cmbCargoComision.SelectedValue = dt.Rows(0).Item("codigo_ccm")
                    txtNroResolucion.Text = dt.Rows(0).Item("nro_resolucion_cop")
                End If

                'Cargo las competencias relacionadas
                oeComisionPermanenteCompetencia = New e_ComisionPermanenteCompetencia : odComisionPermanenteCompetencia = New d_ComisionPermanenteCompetencia
                With oeComisionPermanenteCompetencia
                    .codigoCop = codigo
                End With
                dt = odComisionPermanenteCompetencia.fc_Listar(oeComisionPermanenteCompetencia)
                For Each item As ListItem In cmbCompetencia.Items
                    For Each row As Data.DataRow In dt.Rows
                        If item.Value = row.Item("codigo_com") Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If
            udpMantenimiento.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarForm(Optional ByVal limpiarNroResolucion As Boolean = True)
        Try
            cmbPersonal.SelectedValue = -1
            cmbCargoComision.SelectedValue = -1
            If limpiarNroResolucion then txtNroResolucion.Text = ""
            For Each _item As ListItem In cmbCompetencia.Items
                _item.Selected = False
            Next
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Guardar(ByVal tipo As String)
        Try
            If Not fc_ValidarDatosForm() Then
                Exit Sub
            End If

            oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente

            With oeComisionPermanente
                .tipoConsulta = "GEN"
                .codigoCop = hddCod.Value
            End With
            Dim dt As Data.DataTable = odComisionPermanente.fc_Listar(oeComisionPermanente)

            Dim vigenteCop As Boolean = True
            If dt.Rows.Count > 0 Then
                With oeComisionPermanente
                    vigenteCop = dt.Rows(0).Item("vigente_cop")
                End With
            End If

            Dim competencias As String = ""
            For Each _item As ListItem In cmbCompetencia.Items
                If _item.Selected Then
                    If competencias <> "" Then
                        competencias &= ","
                    End If
                    competencias &= _item.Value
                End If
            Next

            With oeComisionPermanente
                .operacion = "I"
                .codigoCop = hddCod.Value
                .codigoPer = cmbPersonal.SelectedValue
                .codigoCcm = cmbCargoComision.SelectedValue
                .nroResolucionCop = txtNroResolucion.Text.ToUpper().Trim()
                .vigenteCop = vigenteCop
                .codigosCom = competencias
                .codUsuario = Request.QueryString("id")
            End With

            Dim rpta As Dictionary(Of String, String) = odComisionPermanente.fc_IUD(oeComisionPermanente)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
                Select Case tipo
                    Case "GS" 'Guardar y salir
                        mt_SeleccionarTab("L")
                    Case "GC" 'Guardar y continuar
                        mt_LimpiarForm(limpiarNroResolucion:=False)
                End Select

            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_ValidarDatosForm() As Boolean
        If cmbPersonal.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar una persona")
            Return False
        End If

        If cmbCargoComision.SelectedValue = -1 Then
            mt_GenerarToastServidor(0, "Debe seleccionar un cargo")
            Return False
        End If

        If txtNroResolucion.Text.Trim = "" Then
            mt_GenerarToastServidor(0, "Debe asignar un número de resolución")
            Return False
        End If

        'Dim lb_CompetenciasSeleccionadas As Boolean = False
        'For Each _item As ListItem In cmbCompetencia.Items
        '    If _item.Selected Then
        '        lb_CompetenciasSeleccionadas = True
        '        Exit For
        '    End If
        'Next

        'If Not lb_CompetenciasSeleccionadas Then
        '    mt_GenerarToastServidor(0, "Debe seleccionar al menos una competencia")
        '    Return False
        'End If

        Return True
    End Function

    Private Sub mt_Vigencia(ByVal codigoCop As Integer, ByVal vigenteCop As Boolean)
        Try
            oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente

            With oeComisionPermanente
                .operacion = "V"
                .codigoCop = codigoCop
                .vigenteCop = vigenteCop
            End With

            Dim rpta As Dictionary(Of String, String) = odComisionPermanente.fc_IUD(oeComisionPermanente)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Eliminar(ByVal codigoCop As String)
        Try
            oeComisionPermanente = New e_ComisionPermanente : odComisionPermanente = New d_ComisionPermanente

            With oeComisionPermanente
                .operacion = "D"
                .codigoCop = codigoCop
            End With

            Dim rpta As Dictionary(Of String, String) = odComisionPermanente.fc_IUD(oeComisionPermanente)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
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
