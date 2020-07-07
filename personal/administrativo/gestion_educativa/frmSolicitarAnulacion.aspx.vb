Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmSolicitarAnulacion
    Inherits System.Web.UI.Page

#Region "Variables Globales"
    Private mo_RepoAdmision As New ClsAdmision
    Private mn_CodigoUsu As Integer
    Private mn_CodigoPso As Integer
    Private mn_CodigoCco As Integer
    Private mb_IsModal As Boolean
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mn_CodigoUsu = IIf(Request.QueryString("id") IsNot Nothing, Request.QueryString("id"), 0)
        mn_CodigoPso = IIf(Request.QueryString("pso") IsNot Nothing, Request.QueryString("pso"), 0)
        mn_CodigoCco = IIf(Request.QueryString("cco") IsNot Nothing, Request.QueryString("cco"), 0)
        mb_IsModal = IIf(Request.QueryString("modal") IsNot Nothing, Request.QueryString("modal"), False)

        LimpiarMensajeServidor()

        If Not IsPostBack Then
            ViewState("SeEnviaronSolicitudes") = False
            lblMEnsajeDeuda.Visible = False
            txtSaldo.Value = 0.0

            If mb_IsModal Then
                mainContainer.Attributes.Item("class") &= " modal-container"
            End If

            Dim dtSolicitudesEnviadas As Data.DataTable = mo_RepoAdmision.ConsultarSolicitudAnulacion(mn_CodigoPso, mn_CodigoCco)
            If dtSolicitudesEnviadas.Rows.Count > 0 Then
                divControlesDatos.Visible = False
            End If
            ViewState("SolicitudesEnviadas") = dtSolicitudesEnviadas
            CargarGrillaDeudas(dtSolicitudesEnviadas)
            CargarComboMotivoAnulacion()
        End If
    End Sub

    Protected Sub grwDeudas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwDeudas.RowDataBound
        Dim dtSolicitudesEnviadas As Data.DataTable = ViewState("SolicitudesEnviadas")

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Columnas As Integer = grwDeudas.Columns.Count
            Dim ln_codigoDeu As Integer = grwDeudas.DataKeys(e.Row.RowIndex).Values.Item("codigo_Deu")

            Dim txtCantidad As TextBox = DirectCast(e.Row.FindControl("txtCantidad"), TextBox)
            If txtCantidad.Text = "" Then
                Dim cantidad As Decimal = _cellsRow(4).Text.Trim
                For Each _Row As Data.DataRow In dtSolicitudesEnviadas.Rows
                    If _Row.Item("codigo_Deu").ToString.Trim = ln_codigoDeu Then
                        cantidad = _Row.Item("importe_Dsa").ToString.Trim
                    End If
                Next
                txtCantidad.Text = cantidad
            End If

            Dim dtSolicitudes As Data.DataTable = ViewState("SolicitudesEnviadas")
            Dim codigoDeu As String = grwDeudas.DataKeys(e.Row.RowIndex).Values.Item("codigo_Deu")
            For Each _Row As Data.DataRow In dtSolicitudes.Rows
                If _Row.Item("codigo_Deu") = codigoDeu AndAlso _Row.Item("autorizado_San") = 0 Then
                    ViewState("SeEnviaronSolicitudes") = True

                    txtCantidad.BackColor = System.Drawing.Color.Orange
                    txtCantidad.Enabled = False
                    lblMEnsajeDeuda.InnerHtml = "Deuda tiene solicitud de anulación"
                    lblMEnsajeDeuda.Visible = True
                End If
            Next

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            Dim txtTotal As TextBox = DirectCast(e.Row.FindControl("txtTotal"), TextBox)
            txtTotal.Enabled = False

            Dim total As Decimal = 0.0
            For Each _Row As GridViewRow In grwDeudas.Rows
                Dim txtCantidad As TextBox = DirectCast(_Row.FindControl("txtCantidad"), TextBox)
                total += CType(txtCantidad.Text.Trim, Decimal)
            Next
            txtTotal.Text = total

            If total > 0 AndAlso Not ViewState("SeEnviaronSolicitudes") Then
                txtSaldo.Value = total
                btnAnular.Attributes.Remove("disabled")
                udpBotonesAccion.Update()
            End If
        End If
    End Sub

    Protected Sub btnAnular_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.ServerClick
        Enviar()
    End Sub
#End Region

#Region "Metodos"
    Private Sub CargarGrillaDeudas(Optional ByVal dtSolicitudesEnviadas As Data.DataTable = Nothing)
        Dim dtDeudas As Data.DataTable = mo_RepoAdmision.ConsultarCargosAbonosPersona(mn_CodigoPso, mn_CodigoCco)
        grwDeudas.DataSource = dtDeudas
        grwDeudas.DataBind()
        udpDeudas.Update()

        If dtDeudas.Rows.Count = 0 Then
            divControlesDatos.Visible = False
            lblMEnsajeDeuda.InnerHtml = "Saldo en cero, no hay información para mostrar"
            lblMEnsajeDeuda.Visible = True
        End If
    End Sub

    Private Sub CargarComboMotivoAnulacion()
        Dim dtMotivoAnulacion As Data.DataTable = mo_RepoAdmision.ListarMotivoAnulacion()
        ClsFunciones.LlenarListas(cmbMotivoAnulacion, dtMotivoAnulacion, "codigo_Mno", "descripcion_Mno", "-- Seleccione --")
    End Sub

    Private Sub Enviar()
        Dim dcValidacion As Dictionary(Of String, String) = ValidarForm()
        If dcValidacion.Item("rpta") <> 1 Then
            GenerarMensajeServidor("Advertencia", dcValidacion.Item("rpta"), dcValidacion.Item("msg"))
            Exit Sub
        End If

        Dim total As Decimal = 0.0
        For Each _Row As GridViewRow In grwDeudas.Rows
            Dim txtCantidad As TextBox = DirectCast(_Row.FindControl("txtCantidad"), TextBox)
            If txtCantidad.Text.Trim <> "" Then
                total += CType(txtCantidad.Text.Trim, Decimal)
            End If
        Next

        If total > 0 Then
            'Registrar la solicitud
            Dim codigoMno As Integer = cmbMotivoAnulacion.SelectedValue
            Dim observacion As String = txtObservacion.Text.Trim
            Dim dcRespuesta As Dictionary(Of String, String) = mo_RepoAdmision.RegistrarSoliciturAnulacion(mn_CodigoPso, codigoMno, observacion, total, mn_CodigoUsu, grwDeudas)

            'Registrar el detalle de la solicitud
            If dcRespuesta.Item("rpta") <> "1" Then
                GenerarMensajeServidor("Error", dcRespuesta.Item("rpta"), dcRespuesta.Item("msg"))
            Else
                GenerarMensajeServidor("Respuesta", dcRespuesta.Item("rpta"), dcRespuesta.Item("msg"))
            End If
        End If
    End Sub

    Private Function ValidarForm() As Dictionary(Of String, String)
        Dim conErrores As Boolean = False
        Dim dcValidacion As New Dictionary(Of String, String)
        With dcValidacion
            .Item("rpta") = 1
            .Item("msg") = ""
            .Item("control") = ""
        End With

        If Not conErrores AndAlso cmbMotivoAnulacion.SelectedValue = "-1" Then
            dcValidacion.Item("rpta") = 0
            dcValidacion.Item("msg") = "Debe seleccionar un motivo de anulación"
            dcValidacion.Item("control") = "cmbMotivoAnulacion"
            conErrores = True
        End If

        If Not conErrores AndAlso String.IsNullOrEmpty(txtObservacion.Text.Trim) Then
            dcValidacion.Item("rpta") = 0
            dcValidacion.Item("msg") = "Debe ingresar una observación"
            dcValidacion.Item("control") = "txtObservacion"
            conErrores = True
        End If

        If Not conErrores Then
            Dim suma As Decimal = 0.0
            For Each _Row As GridViewRow In grwDeudas.Rows
                Dim txtCantidad As TextBox = DirectCast(_Row.FindControl("txtCantidad"), TextBox)
                If String.IsNullOrEmpty(txtCantidad.Text.Trim) Then
                    dcValidacion.Item("rpta") = 0
                    dcValidacion.Item("msg") = "No puede ingresar montos vacíos"
                    dcValidacion.Item("control") = txtCantidad.ID
                    Return dcValidacion
                End If

                Dim cantidad As Decimal = CType(txtCantidad.Text.Trim, Decimal)
                If cantidad = 0 Then
                    dcValidacion.Item("rpta") = 0
                    dcValidacion.Item("msg") = "Debe ingresar valores mayores a 0"
                    dcValidacion.Item("control") = txtCantidad.ID
                    Return dcValidacion
                End If
                suma += cantidad
            Next
            Dim saldo As Decimal = txtSaldo.Value.Trim
            If suma > saldo Then
                dcValidacion.Item("rpta") = 0
                dcValidacion.Item("msg") = "El total no puede ser mayor al saldo"
                conErrores = True
            End If
        End If

        Return dcValidacion
    End Function

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ls_Rpta As String, ByVal ls_Msg As String, Optional ByVal ls_Control As String = "")
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        With respuestaPostback.Attributes
            .Item("data-rpta") = ls_Rpta
            .Item("data-msg") = ls_Msg
            .Item("data-control") = ls_Control
        End With
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub LimpiarMensajeServidor()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ""
        udpMensajeServidorHeader.Update()

        With respuestaPostback.Attributes
            .Item("data-rpta") = ""
            .Item("data-msg") = ""
            .Item("data-control") = ""
        End With
        udpMensajeServidorBody.Update()
    End Sub
#End Region
End Class
