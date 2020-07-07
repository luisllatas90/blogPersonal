Imports ClsPensiones

Partial Class administrativo_gestion_educativa_frmConfigurarCargosMasivos
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private md_PenConfiguracionProgramacionCargo As New d_PenConfiguracionProgramacionCargo
    Private md_PenTipoProcesoCargo As New d_PenTipoProcesoCargo
    Private md_PenCicloAcademico As New d_PenCicloAcademico
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("id_per") = "684" 'PENDIENTE BORRAR!
        If Not IsPostBack Then
            If Not (Session("id_per") <> "" Or Request.QueryString("id") <> "") Then
                Response.Redirect("../../../sinacceso.html")
            End If

            mt_InitFormLista()
        Else
            If Not (Session("id_per") <> "" Or Request.QueryString("id") <> "") Then
                mt_GenerarMensajeServidor("Advertencia", 0, "Se ha perdido la sesión")
            End If

            mt_LimpiarParametros()
            mt_LimpiarMensajeServidor()
            mt_RefreshGridView()
        End If
    End Sub

    Protected Sub grvConfiguracionPredefinidaCargo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvConfiguracionPredefinidaCargo.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ls_codigoCpc As String = grvConfiguracionPredefinidaCargo.DataKeys(e.Row.RowIndex).Values.Item("codigo_cpc")
                Dim ln_Index As Integer = e.Row.RowIndex + 1
                Dim ln_Columnas As Integer = grvConfiguracionPredefinidaCargo.Columns.Count
                _cellsRow(0).Text = ln_Index
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

    Protected Sub btnRegistrar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.ServerClick
        Try
            mt_SeleccionarTab("M")
            mt_CargarForm(0)

            mt_InitFormMantenimiento()

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

    Protected Sub cmbTipoProceso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoProceso.SelectedIndexChanged
        Try
            mt_CargarConfigIframe(cmbTipoProceso.SelectedValue)
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_InitFormLista()
        Try

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitFormMantenimiento()
        Try
            mt_ListarCicloAcademico()
            mt_ListarTipoProcesoCargo()
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarCicloAcademico()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_CicloAcademico As New e_PenCicloAcademico
            With le_CicloAcademico
                .operacion = "GEN"
            End With
            lo_Dt = md_PenCicloAcademico.Listar(le_CicloAcademico)

            ClsFunciones.LlenarListas(cmbCicloAcademico, lo_Dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            lo_Dt.Dispose()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_ListarTipoProcesoCargo()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_TipoProcesoCargo As New e_PenTipoProcesoCargo
            With le_TipoProcesoCargo
                .operacion = "GEN"
            End With
            lo_Dt = md_PenTipoProcesoCargo.Listar(le_TipoProcesoCargo)

            ClsFunciones.LlenarListas(cmbTipoProceso, lo_Dt, "codigo_tpc", "nombre_tpc", "-- SELECCIONE --")
            lo_Dt.Dispose()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarParametros()
        hddTipoVista.Value = ""
        hddParamsToastr.Value = ""
        udpParams.Update()
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

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub mt_RefreshGridView()
        Try
            For Each _Row As GridViewRow In grvConfiguracionPredefinidaCargo.Rows
                grvConfiguracionPredefinidaCargo_RowDataBound(grvConfiguracionPredefinidaCargo, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_Listar()
        Try
            Dim lo_ConfiguracionProgramacionCargo As New e_PenConfiguracionProgramacionCargo
            With lo_ConfiguracionProgramacionCargo
                .operacion = "GEN"
                .nombre_cpc = "%" & txtFiltroNombre.Text.Trim & "%"
            End With

            Dim lo_Dt As Data.DataTable = md_PenConfiguracionProgramacionCargo.Listar(lo_ConfiguracionProgramacionCargo)
            grvConfiguracionPredefinidaCargo.DataSource = lo_Dt
            grvConfiguracionPredefinidaCargo.DataBind()

            divConfiguracionPredefinidaCargo.Visible = lo_Dt.Rows.Count > 0
            udpConfiguracionPredefinidaCargo.Update()

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

    Private Sub mt_CargarForm(ByVal ln_CodigoCpc As Integer)
        Try
            hddCod.Value = ln_CodigoCpc
            udpParams.Update()

            mt_LimpiarForm()

            If ln_CodigoCpc <> 0 Then
                'ifrmFiltrosAlumno.Attributes("src") = "frmFiltrarAlumnos.aspx?tipo=I" _
                '                                    & "&filtros=" & hddFiltrosAlumno.Value _
                '                                    & "&excluirControles=cmbExcluirCarreraProfesional"
            End If
            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_LimpiarForm()
        Try
            ifrmFiltrosAlumno.Attributes("src") = "frmFiltrarAlumnos.aspx?tipo=I&incluirControles=TipoEstudio|CarreraProfesional|ExcluirCarreraProfesional"

            udpMantenimiento.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarConfigIframe(Optional ByVal ln_CodigoTpc As Integer = -1)
        Try
            Dim ls_FormularioTpc As String = ""
            Dim lo_TipoProcesoCargo As New e_PenTipoProcesoCargo
            With lo_TipoProcesoCargo
                .operacion = "GEN"
                .codigo_tpc = ln_CodigoTpc
            End With

            Dim lo_Dt As Data.DataTable = md_PenTipoProcesoCargo.Listar(lo_TipoProcesoCargo)
            If lo_Dt.Rows.Count > 0 Then
                ls_FormularioTpc = lo_Dt.Rows(0).Item("formulario_tpc").ToString
            End If

            ifrmConfigCargos.Attributes("src") = ls_FormularioTpc
            udpIfrmConfigCargos.Update()

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

    Private Sub mt_GenerarToastServidor(ByVal rpta As String, ByVal msg As String)
        Try
            hddParamsToastr.Value = "rpta=" & rpta & "|msg=" & msg
            udpParams.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

#End Region
End Class
