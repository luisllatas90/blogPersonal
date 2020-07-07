Imports System.Collections.Generic

Partial Class Alumni_frmEventoVirtual
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private md_EventoVirtual As New d_EventoVirtual
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
        Else
            mt_LimpiarMensajeServidor()
            mt_RefreshGridView()
        End If
        mt_LimpiarParametros()
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        mt_Listar()
    End Sub

    Protected Sub grvEvento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvEvento.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ls_codigoEvi As String = grvEvento.DataKeys(e.Row.RowIndex).Values.Item("codigo_evi")
                Dim ln_Index As Integer = e.Row.RowIndex + 1
                Dim ln_Columnas As Integer = grvEvento.Columns.Count
                _cellsRow(0).Text = ln_Index

                Dim lo_Dr As Data.DataRowView = e.Row.DataItem
                If lo_Dr IsNot Nothing Then
                    Dim ls_Fecha As String = CDate(lo_Dr.Item("fechaHoraInicio_evi")).ToString("dd/MM/yyyy")
                    Dim ls_HoraInicio As String = CDate(lo_Dr.Item("fechaHoraInicio_evi")).ToString("HH:mm")
                    Dim ls_HoraFin As String = lo_Dr.Item("fechaHoraFin_evi").ToString
                    If Not String.IsNullOrEmpty(ls_HoraFin) Then
                        ls_HoraFin = CDate(ls_HoraFin).ToString("HH:mm")
                    End If

                    _cellsRow(3).Text = ls_Fecha & " " & ls_HoraInicio & " - " & ls_HoraFin

                    Dim ls_Tipo As String = ""
                    Dim ls_Tipos() As String = lo_Dr.Item("tipo_evi").split(",")
                    For Each _tipo As String In ls_Tipos
                        If Not String.IsNullOrEmpty(ls_Tipo) Then
                            ls_Tipo &= ", "
                        End If
                        Select Case _tipo
                            Case "ES"
                                ls_Tipo &= "ESTUDIANTE"
                            Case "EG"
                                ls_Tipo &= "EGRESADO"
                        End Select
                    Next
                    _cellsRow(5).Text = ls_Tipo
                    _cellsRow(6).Text = Codificar(lo_Dr.Item("codigo_evi"))
                End If

                'Editar
                Dim lo_btnEditar As New HtmlButton()
                With lo_btnEditar
                    .ID = "btnEditar" & ln_Index
                    .Attributes.Add("data-evi", ls_codigoEvi)
                    .Attributes.Add("class", "btn btn-info btn-sm")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Editar")
                    .InnerHtml = "<i class='fa fa-edit'></i>"
                    AddHandler .ServerClick, AddressOf btnEditar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEditar)

                'Eliminar
                Dim lo_btnFakeEliminar As New HtmlButton()
                With lo_btnFakeEliminar
                    .ID = "btnFakeEliminar" & ln_Index
                    .Attributes.Add("data-evi", ls_codigoEvi)
                    .Attributes.Add("class", "btn btn-danger btn-sm")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Eliminar")
                    .InnerHtml = "<i class='fa fa-trash-alt'></i>"
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnFakeEliminar)

                Dim lo_btnEliminar As New HtmlButton()
                With lo_btnEliminar
                    .ID = "btnEliminar" & ln_Index
                    .Attributes.Add("data-evi", ls_codigoEvi)
                    .Attributes.Add("class", "btn btn-warning btn-sm d-none")
                    .Attributes.Add("type", "button")
                    .Attributes.Add("title", "Eliminar")
                    .InnerHtml = "<i class='fa fa-trash'></i>"
                    AddHandler .ServerClick, AddressOf btnEliminar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEliminar)

            End If
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub btnRegistrar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.ServerClick
        mt_SeleccionarTab("M")
        mt_CargarForm(0)
    End Sub

    Protected Sub btnManCancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnManCancelar.ServerClick
        hddTipoVista.Value = "L"
        udpParams.Update()
    End Sub

    Protected Sub btnManGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnManGuardar.ServerClick
        mt_Guardar()
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        divEventos.Visible = False
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""
        udpParams.Update()
    End Sub

    Private Function Codificar(ByVal ln_CodigoEvi As Integer) As String
        Try
            Dim base64Encoded As String
            Dim data As Byte()
            data = System.Text.ASCIIEncoding.ASCII.GetBytes(ln_CodigoEvi)
            base64Encoded = System.Convert.ToBase64String(data)
            Return base64Encoded
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
            Throw ex
        End Try
    End Function

    Private Sub mt_SeleccionarTab(ByVal tipo As String)
        Try
            hddTipoVista.Value = tipo
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
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

    Private Sub mt_Listar()
        Try
            Dim lo_EventoVirtual As New e_EventoVirtual
            With lo_EventoVirtual
                .operacion = "GEN"
                .nombre_evi = "%" & txtFiltroNombre.Text.Trim & "%"
                .nombrePonente_evi = "%" & txtFiltroPonente.Text.Trim & "%"
            End With

            Dim lo_Dt As Data.DataTable = md_EventoVirtual.ListarEventoVirtual(lo_EventoVirtual)
            grvEvento.DataSource = lo_Dt
            grvEvento.DataBind()

            divEventos.Visible = lo_Dt.Rows.Count > 0
            udpEventos.Update()

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

    Private Sub mt_SetCodEvi(ByVal codigo_evi As Integer)
        Try
            hddCodEvi.Value = codigo_evi
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Guardar()
        Try
            If Not mf_ValidarDatosForm() Then
                Exit Sub
            End If

            Dim ls_CodUsuario As String = Request.QueryString("id")
            Dim ls_Tipo As String = ""
            For Each _item As ListItem In cmbTipo.Items
                If _item.Selected Then
                    If ls_Tipo <> "" Then
                        ls_Tipo &= ","
                    End If
                    ls_Tipo &= _item.Value
                End If
            Next

            Dim ls_HoraInicio As String = dtpHoraInicio.Value.Trim
            If String.IsNullOrEmpty(ls_HoraInicio) Then
                ls_HoraInicio = "00:00"
            End If
            Dim ls_FechaHoraInicio As String = dtpFecha.Value.Trim & " " & ls_HoraInicio


            Dim ls_HoraFin As Object = dtpHoraFin.Value.Trim
            Dim ls_FechaHoraFin As Object = DBNull.Value
            If Not String.IsNullOrEmpty(ls_HoraFin) Then
                ls_FechaHoraFin = dtpFecha.Value.Trim & " " & ls_HoraFin
            End If

            Dim lo_EventoVirtual As New e_EventoVirtual
            With lo_EventoVirtual
                .operacion = "I"
                .codigo_evi = hddCodEvi.Value
                .nombre_evi = txtNombre.Text.Trim.ToUpper
                .nombrePonente_evi = txtNombrePonente.Text.Trim.ToUpper
                .fechaHoraInicio_evi = ls_FechaHoraInicio
                .fechaHoraFin_evi = ls_FechaHoraFin
                .url_evi = txtUrl.Text.Trim
                .tipo_evi = ls_Tipo
                .cod_usuario = ls_CodUsuario
            End With

            Dim lo_Rpta As Dictionary(Of String, String) = md_EventoVirtual.IUDEventoVirtual(lo_EventoVirtual)

            If lo_Rpta.Item("rpta") = "1" Then
                mt_Listar()
                mt_SeleccionarTab("L")
            End If
            mt_GenerarToastServidor(lo_Rpta.Item("rpta"), lo_Rpta.Item("msg"))

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Function mf_ValidarDatosForm() As Boolean
        If String.IsNullOrEmpty(txtNombre.Text.Trim) Then
            mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar un nombre")
            Return False
        End If

        If String.IsNullOrEmpty(dtpFecha.Value.Trim) Then
            mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar una fecha")
            Return False
        End If

        If String.IsNullOrEmpty(dtpHoraInicio.Value.Trim) Then
            mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar una hora de inicio")
            Return False
        End If

        If String.IsNullOrEmpty(txtUrl.Text.Trim) Then
            mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar un enlace")
            Return False
        End If

        Dim lb_TipoSeleccionado As Boolean = False
        For Each _item As ListItem In cmbTipo.Items
            If _item.Selected Then
                lb_TipoSeleccionado = True
                Exit For
            End If
        Next

        If Not lb_TipoSeleccionado Then
            mt_GenerarMensajeServidor("Advertencia", 0, "Debe seleccionar al menos un tipo de evento")
            Return False
        End If

        Return True
    End Function

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String)
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarForm(ByVal codigo_evi As Integer)
        Try
            mt_SetCodEvi(codigo_evi)

            For Each _item As ListItem In cmbTipo.Items
                _item.Selected = False
            Next

            If codigo_evi = 0 Then
                txtNombre.Text = ""
                txtNombrePonente.Text = ""
                dtpFecha.Value = ""
                dtpHoraInicio.Value = ""
                dtpHoraFin.Value = ""
                txtUrl.Text = ""
            Else
                Dim lo_EventoVirtual As New e_EventoVirtual
                With lo_EventoVirtual
                    .operacion = "GEN"
                    .codigo_evi = codigo_evi
                End With

                Dim lo_Dt As Data.DataTable = md_EventoVirtual.ListarEventoVirtual(lo_EventoVirtual)
                If lo_Dt.Rows.Count > 0 Then
                    txtNombre.Text = lo_Dt.Rows(0).Item("nombre_evi")
                    txtNombrePonente.Text = lo_Dt.Rows(0).Item("nombrePonente_evi")
                    dtpFecha.Value = CDate(lo_Dt.Rows(0).Item("fechaHoraInicio_evi")).ToString("dd/MM/yyyy")
                    dtpHoraInicio.Value = CDate(lo_Dt.Rows(0).Item("fechaHoraInicio_evi")).ToString("HH:mm")

                    Dim ls_HoraFin As String = lo_Dt.Rows(0).Item("fechaHoraFin_evi")
                    If Not String.IsNullOrEmpty(ls_HoraFin) Then
                        ls_HoraFin = CDate(ls_HoraFin).ToString("HH:mm")
                    End If
                    dtpHoraFin.Value = ls_HoraFin

                    txtUrl.Text = lo_Dt.Rows(0).Item("url_evi")

                    Dim ls_Tipos() As String = lo_Dt.Rows(0).Item("tipo_evi").ToString.Split(",")
                    For Each _item As ListItem In cmbTipo.Items
                        If Array.IndexOf(ls_Tipos, _item.Value) > -1 Then
                            _item.Selected = True
                        End If
                    Next
                End If
            End If
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Eliminar(ByVal ls_CodigoEvi As String)
        Try
            Dim ls_CodUsuario As String = Request.QueryString("id")
            Dim lo_EventoVirtual As New e_EventoVirtual
            With lo_EventoVirtual
                .operacion = "D"
                .codigo_evi = ls_CodigoEvi
                .cod_usuario = ls_CodUsuario
            End With
            Dim lo_Rpta As Dictionary(Of String, String) = md_EventoVirtual.IUDEventoVirtual(lo_EventoVirtual)
            mt_GenerarToastServidor(lo_Rpta.Item("rpta"), lo_Rpta.Item("msg"))

            mt_Listar()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub mt_RefreshGridView()
        Try
            For Each _Row As GridViewRow In grvEvento.Rows
                grvEvento_RowDataBound(grvEvento, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Eventos delegados
    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoEvi As String = button.Attributes("data-evi")
            mt_CargarForm(ls_CodigoEvi)
            mt_SeleccionarTab("M")

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoEvi As String = button.Attributes("data-evi")
            mt_ELiminar(ls_CodigoEvi)

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region
    
End Class
