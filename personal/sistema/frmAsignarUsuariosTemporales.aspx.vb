Imports ClsMantenimientoSistemas
Imports System.Collections.Generic

Partial Class sistema_frmAsignarUsuariosTemporales
    Inherits System.Web.UI.Page

#Region "Declaración de Variables"
    Private oeCambioUsuarioAcceso As e_CambioUsuarioAcceso, odCambioUsuarioAcceso As d_CambioUsuarioAcceso
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_Init()
            End If
            mt_LimpiarParametros()

            If String.IsNullOrEmpty(Request.QueryString("id").Trim) Then
                mt_GenerarMensajeServidor("Error", -1, "Se ha perdido la sesión de usuario, por favor vuelva a ingresar")
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

    Protected Sub grvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvList.RowCommand
        Try
            Dim codigo As Integer = e.CommandArgument

            Select Case e.CommandName
                Case "Editar"
                    mt_CargarForm(codigo)
                    mt_SeleccionarTab("M")
                Case "Activar"
                    mt_Vigencia(codigo, True)
                Case "Desactivar"
                    mt_Vigencia(codigo, False)
                Case "Eliminar"
                    mt_Eliminar(codigo)
            End Select
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvList.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim codigoCop As String = grvList.DataKeys(e.Row.RowIndex).Values.Item("codigo_cua")
                Dim index As Integer = e.Row.RowIndex + 1
                _cellsRow(0).Text = index
            End If
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

    Protected Sub chkHabilitarHasta_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHabilitarHasta.ServerChange
        Try
            If chkHabilitarHasta.Checked Then
                dtpFechaHasta.Disabled = False
                dtpFechaHasta.Value = dtpFechaHasta.Attributes.Item("data-old")
                dtpHoraHasta.Disabled = False
                dtpHoraHasta.Value = dtpHoraHasta.Attributes.Item("data-old")
            Else
                dtpFechaHasta.Disabled = True
                dtpFechaHasta.Value = ""
                dtpHoraHasta.Disabled = True
                dtpHoraHasta.Value = ""
            End If
            udpFechaHasta.Update()

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
            mt_SeleccionarTab("L")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    'LISTA
    Private Sub mt_Init()
        Try
            mt_InitFiltroComboRegistradoPor()
            chkFiltroVigente.Checked = True
            divGrvList.Visible = False
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitFiltroComboRegistradoPor()
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso
            ClsGlobales.mt_LlenarListas(cmbFiltroRegistradoPor, odCambioUsuarioAcceso.fc_ListarPersonal(), "codigo_per", "nombres", "-- TODOS --")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso

            Dim codigoPerReg As Integer = cmbFiltroRegistradoPor.SelectedValue
            If codigoPerReg = -1 Then codigoPerReg = 0

            With oeCambioUsuarioAcceso
                .tipoConsulta = "LR"
                .codigoCua = ""
                .codigoPerReg = codigoPerReg
                .usuarioOrig = "%" & txtFiltroUsuarioOrig.Text.Trim & "%"
                .usuarioTemp = "%" & txtFiltroUsuarioTemp.Text.Trim & "%"
                .vigenteCua = IIf(chkFiltroVigente.Checked, True, DBNull.Value)
            End With

            Dim dt As Data.DataTable = odCambioUsuarioAcceso.fc_Listar(oeCambioUsuarioAcceso)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub


    'MANTENIMIENTO
    Private Sub mt_InitComboUsuarioReal()
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso
            ClsGlobales.mt_LlenarListas(cmbUsuarioReal, odCambioUsuarioAcceso.fc_ListarPersonal(), "codigo_per", "nombres", "-- Seleccione --")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_InitComboUsuarioTemporal()
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso
            ClsGlobales.mt_LlenarListas(cmbUsuarioTemporal, odCambioUsuarioAcceso.fc_ListarPersonal(), "codigo_per", "nombres", "-- Seleccione --")
            udpMantenimiento.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarForm(ByVal codigo As Integer)
        Try
            hddCod.Value = codigo
            udpParams.Update()

            mt_InitComboUsuarioReal()
            mt_InitComboUsuarioTemporal()

            mt_LimpiarForm()

            If codigo <> 0 Then
                oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso
                With oeCambioUsuarioAcceso
                    .codigoCua = codigo
                End With

                Dim dt As Data.DataTable = odCambioUsuarioAcceso.fc_Listar(oeCambioUsuarioAcceso)
                If dt.Rows.Count > 0 Then
                    cmbUsuarioReal.SelectedValue = dt.Rows(0).Item("codigo_per_orig")
                    cmbUsuarioTemporal.SelectedValue = dt.Rows(0).Item("codigo_per_temp")
                    'FECHA
                    Dim fechaHoraHasta As String = dt.Rows(0).Item("fecha_hasta_cua")
                    Dim fechaHasta As String = "", horaHasta As String = ""
                    If Not String.IsNullOrEmpty(fechaHoraHasta) Then
                        fechaHasta = CDate(fechaHoraHasta).ToString("dd/MM/yyyy")
                        horaHasta = CDate(fechaHoraHasta).ToString("HH:mm")
                        chkHabilitarHasta.Checked = True
                    Else
                        chkHabilitarHasta.Checked = False
                    End If
                    chkHabilitarHasta_ServerChange(Nothing, Nothing)
                    dtpFechaHasta.Value = fechaHasta
                    dtpFechaHasta.Attributes.Item("data-old") = fechaHasta
                    dtpHoraHasta.Value = horaHasta
                    dtpHoraHasta.Attributes.Item("data-old") = horaHasta
                End If
            End If
            udpMantenimiento.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarForm(Optional ByVal limpiarNroResolucion As Boolean = True)
        Try
            cmbUsuarioReal.SelectedValue = -1
            cmbUsuarioTemporal.SelectedValue = -1
            dtpFechaHasta.Value = ""
            dtpHoraHasta.Value = ""
            chkHabilitarHasta.Checked = False
            chkHabilitarHasta_ServerChange(Nothing, Nothing)

            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Guardar()
        Try
            If Not fc_ValidarDatosForm() Then
                Exit Sub
            End If

            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso

            Dim codUsuario As String = Request.QueryString("id")
            Dim fechaHasta As String = dtpFechaHasta.Value.Trim
            Dim horaHasta As String = dtpHoraHasta.Value.Trim
            Dim fechaHoraHasta As Object = fc_FechaHoraHasta()
            If String.IsNullOrEmpty(fechaHoraHasta) Then
                fechaHoraHasta = DBNull.Value
            End If

            'If Not String.IsNullOrEmpty(fechaHasta) Then
            '    If String.IsNullOrEmpty(horaHasta) Then
            '        horaHasta = "23:59"
            '    End If

            '    fechaHoraHasta = fechaHasta & " " & horaHasta & ":59" 'Segundos
            'End If

            With oeCambioUsuarioAcceso
                .operacion = "I"
                .codigoCua = hddCod.Value
                .codigoPerOrig = cmbUsuarioReal.SelectedValue
                .codigoPerTemp = cmbUsuarioTemporal.SelectedValue
                .fechaDesdeCua = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                .fechaHastaCua = fechaHoraHasta
                .vigenteCua = True
                .codUsuario = codUsuario
            End With

            Dim rpta As Dictionary(Of String, String) = odCambioUsuarioAcceso.fc_IUD(oeCambioUsuarioAcceso)

            If rpta.Item("rpta") = "1" Then
                mt_Listar()
                mt_SeleccionarTab("L")
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function fc_FechaHoraHasta() As String
        Dim fechaHasta As String = dtpFechaHasta.Value.Trim
        Dim horaHasta As String = dtpHoraHasta.Value.Trim
        Dim fechaHoraHasta As String = ""

        If Not String.IsNullOrEmpty(fechaHasta) Then
            If String.IsNullOrEmpty(horaHasta) Then
                horaHasta = "23:59"
            End If
            fechaHoraHasta = fechaHasta & " " & horaHasta & ":59" 'Segundos
        End If
        Return fechaHoraHasta
    End Function

    Private Function fc_ValidarDatosForm() As Boolean
        Try
            If cmbUsuarioReal.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar al usuario real")
                Return False
            End If

            If cmbUsuarioTemporal.SelectedValue = -1 Then
                mt_GenerarToastServidor(0, "Debe seleccionar al usuario temporal")
                Return False
            End If

            If chkHabilitarHasta.Checked Then
                Dim fechaHoraHasta As String = fc_FechaHoraHasta()
                Dim dateFechaHoraHasta As Date
                If Not Date.TryParse(fechaHoraHasta, dateFechaHoraHasta) Then
                    mt_GenerarToastServidor(0, "La fecha ingresada no tiene un formato válido")
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
            Return False
        End Try
    End Function

    Private Sub mt_Vigencia(ByVal codigoCua As Integer, ByVal vigenteCua As Boolean)
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso

            With oeCambioUsuarioAcceso
                .operacion = "V"
                .codigoCua = codigoCua
                .vigenteCua = vigenteCua
            End With

            Dim rpta As Dictionary(Of String, String) = odCambioUsuarioAcceso.fc_IUD(oeCambioUsuarioAcceso)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
            End If
            mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Eliminar(ByVal codigoCua As Integer)
        Try
            oeCambioUsuarioAcceso = New e_CambioUsuarioAcceso : odCambioUsuarioAcceso = New d_CambioUsuarioAcceso

            With oeCambioUsuarioAcceso
                .operacion = "D"
                .codigoCua = codigoCua
            End With

            Dim rpta As Dictionary(Of String, String) = odCambioUsuarioAcceso.fc_IUD(oeCambioUsuarioAcceso)
            If rpta.Item("rpta") = "1" Then
                mt_Listar()
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
