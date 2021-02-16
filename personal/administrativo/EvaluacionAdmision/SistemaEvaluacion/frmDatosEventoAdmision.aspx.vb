Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class frmDatosEventoAdmision
    Inherits System.Web.UI.Page

#Region "Declaración de variables"
    Private oeVacantesEvento As e_VacantesEvento, odVacantesEvento As d_VacantesEvento
    Private oeDatosEventoAdmision As e_DatosEventoAdmision, odDatosEventoAdmision As d_DatosEventoAdmision
    Private dtCicloAcademico As Data.DataTable
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()
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

                Dim cmbCicloAcademico As DropDownList = e.Row.FindControl("cmbCicloAcademico")
                mt_InitComboCicloAcademico(cmbCicloAcademico, "-- Seleccione --")

                Dim hddCac As HiddenField = e.Row.FindControl("hddCac")
                Dim codigoCac As Integer = IIf(hddCac.Value <> 0, hddCac.Value, -1)
                cmbCicloAcademico.SelectedValue = codigoCac

                Dim txtFechaEvento As TextBox = e.Row.FindControl("txtFechaEvento")
                txtFechaEvento.Text = e.Row.DataItem("fechaEvento_dea")
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
            If e.CommandName = "Editar" Then
                Dim btnEditar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")

                Dim spnCicloAcademico As HtmlGenericControl = row.FindControl("spnCicloAcademico")
                Dim cmbCicloAcademico As DropDownList = row.FindControl("cmbCicloAcademico")

                btnEditar.Visible = False
                btnGuardar.Visible = True
                btnCancelar.Visible = True

                spnCicloAcademico.Visible = False
                cmbCicloAcademico.Visible = True
                cmbCicloAcademico.Enabled = True

                cmbCicloAcademico.Attributes.Item("data-old-value") = cmbCicloAcademico.SelectedValue
            End If

            If e.CommandName = "Guardar" Then
                Dim btnGuardar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardar.NamingContainer

                Dim btnEditar As LinkButton = row.FindControl("btnEditar")
                Dim btnCancelar As LinkButton = row.FindControl("btnCancelar")

                Dim spnCicloAcademico As HtmlGenericControl = row.FindControl("spnCicloAcademico")
                Dim cmbCicloAcademico As DropDownList = row.FindControl("cmbCicloAcademico")
                Dim hddDea As HiddenField = row.FindControl("hddDea")
                Dim hddCac As HiddenField = row.FindControl("hddCac")

                If cmbCicloAcademico.SelectedValue = -1 Then
                    mt_GenerarToastServidor(0, "Debe seleccionar un semestre académico")
                    Exit Sub
                End If

                Dim codigoDea As Integer = hddDea.Value
                Dim codigoCco As Integer = DirectCast(row.FindControl("hddCco"), HiddenField).Value
                Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue

                oeDatosEventoAdmision = New e_DatosEventoAdmision : odDatosEventoAdmision = New d_DatosEventoAdmision
                With oeDatosEventoAdmision
                    ._operacion = "UCA"
                    ._codigo_dea = codigoDea
                    ._codigo_cco = codigoCco
                    ._codigo_cac = codigoCac
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odDatosEventoAdmision.fc_IUD(oeDatosEventoAdmision)
                If rpta.Item("rpta") = "1" Then
                    btnEditar.Visible = True
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False

                    spnCicloAcademico.InnerText = cmbCicloAcademico.SelectedItem.Text
                    spnCicloAcademico.Visible = True
                    cmbCicloAcademico.Visible = False
                    cmbCicloAcademico.Enabled = False

                    hddDea.Value = rpta.Item("cod")
                    hddCac.Value = codigoCac
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "Cancelar" Then
                Dim btnCancelar As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelar.NamingContainer

                Dim btnGuardar As LinkButton = row.FindControl("btnGuardar")
                Dim btnEditar As LinkButton = row.FindControl("btnEditar")

                Dim spnCicloAcademico As HtmlGenericControl = row.FindControl("spnCicloAcademico")
                Dim cmbCicloAcademico As DropDownList = row.FindControl("cmbCicloAcademico")

                btnEditar.Visible = True
                btnGuardar.Visible = False
                btnCancelar.Visible = False

                spnCicloAcademico.Visible = True
                cmbCicloAcademico.Visible = False
                cmbCicloAcademico.Enabled = False

                cmbCicloAcademico.SelectedValue = cmbCicloAcademico.Attributes.Item("data-old-value")
            End If

            If e.CommandName = "EditarFecha" Then
                Dim btnEditarFecha As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnEditarFecha.NamingContainer

                Dim btnGuardarFecha As LinkButton = row.FindControl("btnGuardarFecha")
                Dim btnCancelarFecha As LinkButton = row.FindControl("btnCancelarFecha")

                Dim spnFechaEvento As HtmlGenericControl = row.FindControl("spnFechaEvento")
                Dim txtFechaEvento As TextBox = row.FindControl("txtFechaEvento")

                btnEditarFecha.Visible = False
                btnGuardarFecha.Visible = True
                btnCancelarFecha.Visible = True

                spnFechaEvento.Visible = False
                txtFechaEvento.Visible = True
                txtFechaEvento.Enabled = True

                txtFechaEvento.Attributes.Item("data-old-value") = txtFechaEvento.Text
            End If

            If e.CommandName = "GuardarFecha" Then
                Dim btnGuardarFecha As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnGuardarFecha.NamingContainer

                Dim btnEditarFecha As LinkButton = row.FindControl("btnEditarFecha")
                Dim btnCancelarFecha As LinkButton = row.FindControl("btnCancelarFecha")

                Dim spnFechaEvento As HtmlGenericControl = row.FindControl("spnFechaEvento")
                Dim txtFechaEvento As TextBox = row.FindControl("txtFechaEvento")
                Dim hddDea As HiddenField = row.FindControl("hddDea")
                Dim hddCac As HiddenField = row.FindControl("hddCac")

                If txtFechaEvento.Text.Trim() = "" Then
                    mt_GenerarToastServidor(0, "Debe ingresar una fecha correcta")
                    Exit Sub
                End If

                Dim codigoCac As Integer = hddCac.Value
                If codigoCac = 0 Then
                    mt_GenerarToastServidor(0, "Primero debe guardar el semestre académico del evento")
                    Exit Sub
                End If

                Dim codigoDea As Integer = hddDea.Value
                Dim codigoCco As Integer = DirectCast(row.FindControl("hddCco"), HiddenField).Value
                Dim fechaEvento As Date = txtFechaEvento.Text

                oeDatosEventoAdmision = New e_DatosEventoAdmision : odDatosEventoAdmision = New d_DatosEventoAdmision
                With oeDatosEventoAdmision
                    ._operacion = "UFE"
                    ._codigo_dea = codigoDea
                    ._codigo_cco = codigoCco
                    ._codigo_cac = codigoCac
                    ._fechaEvento_dea = fechaEvento
                    ._cod_usuario = Request.QueryString("id")
                End With

                Dim rpta As Dictionary(Of String, String) = odDatosEventoAdmision.fc_IUD(oeDatosEventoAdmision)
                If rpta.Item("rpta") = "1" Then
                    btnEditarFecha.Visible = True
                    btnGuardarFecha.Visible = False
                    btnCancelarFecha.Visible = False

                    spnFechaEvento.InnerText = txtFechaEvento.Text
                    spnFechaEvento.Visible = True
                    txtFechaEvento.Visible = False
                    txtFechaEvento.Enabled = False

                    hddDea.Value = rpta.Item("cod")
                End If
                mt_GenerarToastServidor(rpta.Item("rpta"), rpta.Item("msg"))
            End If

            If e.CommandName = "CancelarFecha" Then
                Dim btnCancelarFecha As LinkButton = e.CommandSource
                Dim row As GridViewRow = btnCancelarFecha.NamingContainer

                Dim btnGuardarFecha As LinkButton = row.FindControl("btnGuardarFecha")
                Dim btnEditarFecha As LinkButton = row.FindControl("btnEditarFecha")

                Dim spnFechaEvento As HtmlGenericControl = row.FindControl("spnFechaEvento")
                Dim txtFechaEvento As TextBox = row.FindControl("txtFechaEvento")

                btnEditarFecha.Visible = True
                btnGuardarFecha.Visible = False
                btnCancelarFecha.Visible = False

                spnFechaEvento.Visible = True
                txtFechaEvento.Visible = False
                txtFechaEvento.Enabled = False

                txtFechaEvento.Text = txtFechaEvento.Attributes.Item("data-old-value")
            End If

            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        divGrvList.Visible = False

        odVacantesEvento = New d_VacantesEvento
        Dim dt As Data.DataTable = odVacantesEvento.fc_ListarCicloAcademico()
        mt_InitComboCicloAcademico(cmbFiltroCicloAcademico, "-- TODOS --", dt)
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

    Private Sub mt_Listar()
        Try
            odVacantesEvento = New d_VacantesEvento

            'Primero cargo la data de los ciclos académicos para cargar los combos de cada fila
            dtCicloAcademico = odVacantesEvento.fc_ListarCicloAcademico()

            Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
            If codigoCac = -1 Then codigoCac = 0

            Dim codUsuario As Integer = Request.QueryString("id")
            Dim dt As Data.DataTable = odVacantesEvento.fc_ListarCentroCostos("DEA", codigoCac, codUsuario)
            grvList.DataSource = dt
            grvList.DataBind()

            divGrvList.Visible = dt.Rows.Count > 0
            udpGrvList.Update()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_InitComboCicloAcademico(ByVal cmb As DropDownList, ByVal defaultText As String, Optional ByVal dt As Data.DataTable = Nothing)
        Try
            If dt Is Nothing Then dt = dtCicloAcademico
            oeDatosEventoAdmision = New e_DatosEventoAdmision : odDatosEventoAdmision = New d_DatosEventoAdmision
            ClsGlobales.mt_LlenarListas(cmb, dt, "codigo_cac", "descripcion_cac", defaultText)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'OTROS MÉTODOS
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
