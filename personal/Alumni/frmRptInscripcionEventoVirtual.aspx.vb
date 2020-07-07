
Partial Class administrativo_gestion_educativa_frmRptInscripcionEventoVirtual
    Inherits System.Web.UI.Page

#Region "Propiedades"
    ' Datos
    private md_EventoVirtual as new d_EventoVirtual
    Private md_TipoParticipante As New d_TipoParticipante
    Private md_InscripcionEventoVirtual As New d_InscripcionEventoVirtual
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
        End If
        mt_LimpiarToastServidor()
        mt_LimpiarMensajeServidor()
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        Try
            Dim ln_CodigoEvi As Integer = IIf(cmbEventoVirtual.SelectedValue <> -1, cmbEventoVirtual.SelectedValue, 0)

            Dim ls_FechaDesde As String = dtpFechaDesde.Text.Trim
            Dim ls_FechaHasta As String = dtpFechaHasta.Text.Trim

            If Not String.IsNullOrEmpty(ls_FechaDesde) AndAlso Not IsDate(ls_FechaDesde) Then
                mt_GenerarToastServidor("0", "La fecha de inicio no es válida", "dtpFechaDesde")
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(ls_FechaHasta) AndAlso Not IsDate(ls_FechaHasta) Then
                mt_GenerarToastServidor("0", "La fecha de fin no es válida", "dtpFechaHasta")
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(ls_FechaDesde) AndAlso Not String.IsNullOrEmpty(ls_FechaHasta) _
                AndAlso Date.Parse(ls_FechaDesde) > Date.Parse(ls_FechaHasta) Then
                mt_GenerarToastServidor("0", "La fecha de inicio no puede ser mayor a la fecha de fin", "dtpFechaDesde")
                Exit Sub
            End If

            Dim lo_FechaDesde As Object = IIf(Not String.IsNullOrEmpty(ls_FechaDesde), ls_FechaDesde, DBNull.Value)
            Dim lo_FechaHasta As Object = IIf(Not String.IsNullOrEmpty(ls_FechaHasta), ls_FechaHasta, DBNull.Value)

            mt_CargarGrillaInscripcionEvento(ln_CodigoEvi, lo_FechaDesde, lo_FechaHasta)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Protected Sub grvInscripcionEvento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvInscripcionEvento.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ls_codigoIev As String = grvInscripcionEvento.DataKeys(e.Row.RowIndex).Values.Item("codigo_iev")
                Dim ln_Index As Integer = e.Row.RowIndex + 1
                Dim ln_Columnas As Integer = grvInscripcionEvento.Columns.Count
                _cellsRow(0).Text = ln_Index

                Dim ls_EstaTrabajando As String = "", ls_ObtenerConstancia As String=""

                Dim lo_Dr As Data.DataRowView = e.Row.DataItem
                If lo_Dr IsNot Nothing Then
                    ls_EstaTrabajando = IIf(lo_Dr.Item("estaTrabajando_iev"), "SI", "NO")

                    Select Case lo_Dr.Item("medioIngresoLaboral_iev")
                        Case "A"
                            lo_Dr.Item("medioIngresoLaboral_iev") = "ALUMNI"
                        Case "E"
                            lo_Dr.Item("medioIngresoLaboral_iev") = "EXTERNA"
                    End Select

                    ls_ObtenerConstancia = IIf(lo_Dr.Item("obtenerConstancia_iev") = "1", "SI", "NO")

                    Select Case lo_Dr.Item("medioInscripcion_iev")
                        Case "CV"
                            lo_Dr.Item("medioInscripcion_iev") = "CAMPUS VIRTUAL"
                        Case "WEB"
                            lo_Dr.Item("medioInscripcion_iev") = "WEB"
                    End Select

                    Dim ls_FechaHoraReg As String = lo_Dr.Item("fechaHoraReg_iev")
                    If ls_FechaHoraReg.Trim <> "" Then
                        ls_FechaHoraReg = CDate(ls_FechaHoraReg).ToString("dd/MM/yyyy HH:mm")
                    End If
                    lo_Dr.Item("fechaHoraReg_iev") = ls_FechaHoraReg

                    e.Row.DataBind()
                End If

                _cellsRow(6).Text = ls_EstaTrabajando
                _cellsRow(10).Text = ls_ObtenerConstancia
            End If

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    private sub mt_Init()
        Try
            mt_CargarComboEventoVirtual()
            divGrvInscripcionEvento.Visible = False
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarComboEventoVirtual()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_EventoVirtual As New e_EventoVirtual
            With le_EventoVirtual
                .operacion = "GEN"
            End With
            lo_Dt = md_EventoVirtual.ListarEventoVirtual(le_EventoVirtual)
            ClsFunciones.LlenarListas(cmbEventoVirtual, lo_Dt, "codigo_evi", "nombre_evi", "-- TODOS --")

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarGrillaInscripcionEvento(ByVal ln_CodigoEvi As Integer, ByVal lo_FechaDesde As Object, ByVal lo_FechaHasta As Object)
        Try
            Dim le_InscripcionEventoVirtual As New e_InscripcionEventoVirtual
            With le_InscripcionEventoVirtual
                .operacion = "DET"
                .codigo_evi = ln_CodigoEvi
                .fechaDesde = lo_FechaDesde
                .fechaHasta = lo_FechaHasta
            End With
            Dim lo_Dt As Data.DataTable = md_InscripcionEventoVirtual.ListarInscripcionEventoVirtual(le_InscripcionEventoVirtual)
            grvInscripcionEvento.DataSource = lo_Dt
            grvInscripcionEvento.DataBind()

            divGrvInscripcionEvento.Visible = lo_Dt.Rows.Count > 0
            udpGrvInscripcionEvento.Update()

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
#End Region
End Class