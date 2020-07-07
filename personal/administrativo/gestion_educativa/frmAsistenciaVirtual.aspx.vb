Imports ClsPreGrado

Partial Class administrativo_gestion_educativa_frmAsistenciaVirtual
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private md_CicloAcademico As New d_PgCicloAcademico
    Private md_CarreraProfesional As New d_PgCarreraProfesional
    Private md_DepartamentoAcademico As New d_PgDepartamentoAcademico
    Private md_Funciones As New d_PgFunciones
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
            mt_CargarFiltros()
        Else
            mt_LimpiarMensajeServidor()
        End If
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        mt_Listar()
    End Sub

    Protected Sub grvAsistencia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvAsistencia.RowDataBound
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
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        divAsistencia.Visible = False
        Session.Remove("data")
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

    Private Sub mt_CargarFiltros()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_CicloAcademico As New e_PgCicloAcademico
            With le_CicloAcademico
                .operacion = "GEN"
                .tipoCac = ""
            End With
            lo_Dt = md_CicloAcademico.Listar(le_CicloAcademico)
            ClsFunciones.LlenarListas(cmbCicloAcademico, lo_Dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")

            Dim codigoTfu As String = Request.QueryString("ctf")
            Dim codigoPer As String = Request.QueryString("id")
            Dim le_CarreraProfesional As New e_PgCarreraProfesional
            With le_CarreraProfesional
                .codigoTest = "2"
                .codigoTfu = codigoTfu
                .codigoPer = codigoPer
            End With
            lo_Dt = md_CarreraProfesional.ConsultarCarreraProfesional(le_CarreraProfesional)
            ClsFunciones.LlenarListas(cmbCarreraProfesional, lo_Dt, "codigo_cpf", "nombre_cpf")
            'cmbCarreraProfesional.SelectedIndex = 0

            Dim le_DepartamentoAcademico As New e_PgDepartamentoAcademico
            With le_DepartamentoAcademico
                .operacion = ""
                .codigoTfu = codigoTfu
                .codigoPer = codigoPer
            End With

            lo_Dt = md_DepartamentoAcademico.DepartamentoPersonalFuncion(le_DepartamentoAcademico)
            If lo_Dt.Rows.Count > 0 Then
                For Each _row As Data.DataRow In lo_Dt.Rows
                    If _row.Item("codigo_dac") = "0" Then
                        _row.Item("descripcion_dac") = "TODOS"
                        Exit For
                    End If
                Next
                ClsFunciones.LlenarListas(cmbDepartamentoAcademico, lo_Dt, "codigo_dac", "descripcion_dac")
            End If

            lo_Dt.Dispose()

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
            Dim codigoCac As String = cmbCicloAcademico.SelectedValue
            Dim codigoCpf As String = cmbCarreraProfesional.SelectedValue
            Dim codigoDac As String = cmbDepartamentoAcademico.SelectedValue
            Dim fechaDesde As String = dtpFechaDesde.Text.Trim
            Dim fechaHasta As String = dtpFechaHasta.Text.Trim

            If codigoCac = -1 Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe seleccionar un ciclo académico")
                Exit Sub
            End If

            If fechaDesde.Trim = "" Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar la fecha 'desde'")
                Exit Sub
            End If

            If fechaHasta.Trim = "" Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe ingresar la fecha 'hasta'")
                Exit Sub
            End If

            'If codigoCpf = -1 Then
            '    codigoCpf = 0
            'End If

            If codigoDac = "" Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Debe seleccionar un departamento académico")
                Exit Sub
            End If

            Dim lo_Dt As Data.DataTable = md_Funciones.AsistenciaVirtual(codigoCac, codigoCpf, codigoDac, fechaDesde, fechaHasta)
            Session("data") = lo_Dt
            grvAsistencia.DataSource = lo_Dt
            grvAsistencia.DataBind()

            divAsistencia.Visible = lo_Dt.Rows.Count > 0
            udpAsistencia.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

End Class
