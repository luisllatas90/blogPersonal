Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmMantenimientoRequisitosAdmision
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim mo_Admision As New ClsAdmision
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("id_per") = "" Then 'Or Request.QueryString("id") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If

            If Not IsPostBack Then
                mt_CargarComboTipoEstudio(cmbFiltroTipoEstudio, udpFiltroTipoEstudio)
            Else
                limpiarValoresMensaje()
                RefrescarGrillaPostulantes()
            End If
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub cmbFiltroTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroTipoEstudio.SelectedIndexChanged
        Try
            mt_CargarComboModalidad(cmbFiltroModalidad, cmbFiltroTipoEstudio, udpFiltroModalidad, True)
            mt_CargarGrillaRequisitosAdmision()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub cmbFiltroModalidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroModalidad.SelectedIndexChanged
        Try
            mt_CargarGrillaRequisitosAdmision()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub grwRequisitosAdmision_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwRequisitosAdmision.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim ls_codigoReq As String = grwRequisitosAdmision.DataKeys(e.Row.RowIndex).Values.Item("codigo_req")
                Dim ln_Index As Integer = e.Row.RowIndex + 1
                Dim ln_Columnas As Integer = grwRequisitosAdmision.Columns.Count

                _cellsRow(0).Text = ln_Index

                'Editar
                Dim lo_btnEditar As New HtmlButton()
                With lo_btnEditar
                    .ID = "btnEditar" & ln_Index
                    .Attributes.Add("data-req", ls_codigoReq)
                    .Attributes.Add("class", "btn btn-primary btn-sm")
                    .Attributes.Add("type", "button")
                    .InnerHtml = "<i class='fa fa-edit'></i>"
                    AddHandler .ServerClick, AddressOf btnEditar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEditar)

                'Eliminar
                Dim lo_btnEliminar As New HtmlButton()
                With lo_btnEliminar
                    .ID = "btnEliminar" & ln_Index
                    .Attributes.Add("data-req", ls_codigoReq)
                    .Attributes.Add("class", "btn btn-danger btn-sm")
                    .Attributes.Add("type", "button")
                    .InnerHtml = "<i class='fa fa-trash'></i>"
                    AddHandler .ServerClick, AddressOf btnEliminar_Click
                End With
                _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnEliminar)

                grwRequisitosAdmision.HeaderRow.TableSection = TableRowSection.TableHeader
            End If
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub btnNuevo_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.ServerClick
        Try
            mt_CargarComboTipoEstudio(cmbTipoEstudio, udpTipoEstudio)
            mt_AsignarValoresFormulario(0)
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub cmbTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEstudio.SelectedIndexChanged
        Try
            mt_CargarComboModalidad(cmbModalidad, cmbTipoEstudio, udpModalidad, False)
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Protected Sub btnGuardar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.ServerClick
        Try
            If mt_ValidarFormulario() Then
                Dim lo_Respuesta As Dictionary(Of String, String)
                Dim codigoReq As Integer = hddCod.Value
                Dim descripcionReq As String = txtDescripcion.Value
                Dim codigosMin As String = ""
                Dim codUsuario As Integer = Session("id_per")

                For Each _Item As ListItem In cmbModalidad.Items
                    If _Item.Selected Then
                        If codigosMin.Length > 0 Then codigosMin &= ","
                        codigosMin &= _Item.Value
                    End If
                Next

                lo_Respuesta = mo_Admision.GuardarRequisitoAdmision(codigoReq, descripcionReq, codigosMin, 0)
                hddRpta.Value = lo_Respuesta.Item("rpta")
                hddMsg.Value = lo_Respuesta.Item("msg")
                hddCod.Value = "0"
                udpHidden.Update()

                If hddRpta.Value = "1" Then
                    mt_CargarGrillaRequisitosAdmision()
                End If
            End If
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub limpiarValoresMensaje()
        Try
            hddRpta.Value = ""
            hddMsg.Value = ""
            udpHidden.Update()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    'Eventos delegados
    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoReq As String = button.Attributes("data-req")

            mt_CargarComboTipoEstudio(cmbTipoEstudio, udpTipoEstudio)
            mt_AsignarValoresFormulario(ls_CodigoReq)
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoReq As String = button.Attributes("data-req")
            mt_EliminarRequisito(ls_CodigoReq)
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_CargarComboTipoEstudio(ByVal combo As Object, ByVal panel As UpdatePanel)
        Try
            Dim ls_Tipo As String = "GEN"
            Dim ls_CodigoTest As String = "1,2,3,4,5,7,8,10"
            ClsFunciones.LlenarListas(combo, mo_Admision.TipoEstudioListar(ls_Tipo, ls_CodigoTest), "codigo_test", "descripcion_test", "-- Seleccione --")
            panel.Update()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub mt_CargarComboModalidad(ByVal combo As Object, ByVal comboTipo As DropDownList, ByVal panel As UpdatePanel, ByVal agregarSeleccione As Boolean)
        Try
            Dim ls_Tipo As String = "CT"
            Dim ls_Param As String = comboTipo.SelectedValue
            If agregarSeleccione Then
                ClsFunciones.LlenarListas(combo, mo_Admision.ListarModalidadIngresoPorTipo(ls_Tipo, ls_Param), "codigo_Min", "nombre_Min", "-- Seleccione --")
            Else
                ClsFunciones.LlenarListas(combo, mo_Admision.ListarModalidadIngresoPorTipo(ls_Tipo, ls_Param), "codigo_Min", "nombre_Min")
            End If
            panel.Update()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub mt_CargarGrillaRequisitosAdmision()
        Try
            Dim ln_CodigoMin As Integer = IIf(cmbFiltroModalidad.SelectedValue <> "", cmbFiltroModalidad.SelectedValue, 0)
            Dim dtRequisitosAdmision As Data.DataTable = mo_Admision.ConsultarRequisitosAdmisionPorModalidad(ln_CodigoMin)
            grwRequisitosAdmision.DataSource = dtRequisitosAdmision
            grwRequisitosAdmision.DataBind()
            udpGrwRequisitosAdmision.Update()
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub mt_MostrarMensaje(ByVal msg As String, ByVal rpta As Integer)
        Try
            hddRpta.Value = rpta
            hddMsg.Value = msg
            udpHidden.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AsignarValoresFormulario(ByVal codigoReq As Integer)
        Try
            If codigoReq = 0 Then
                mt_LimpiarControles()
            Else
                Dim ls_TipoConsulta As String = "GEN"

                cmbTipoEstudio.SelectedValue = cmbFiltroTipoEstudio.SelectedValue
                cmbTipoEstudio_SelectedIndexChanged(Nothing, Nothing)

                Dim dtRequisito As Data.DataTable = mo_Admision.RequisitoListar(ls_TipoConsulta, codigoReq:=codigoReq)
                If dtRequisito.Rows.Count > 0 Then
                    txtDescripcion.Value = dtRequisito.Rows(0).Item("descripcion_req")
                End If

                Dim dtRequisitoModalidad As Data.DataTable = mo_Admision.RequisitoModalidadListar(ls_TipoConsulta, codigoReq:=codigoReq)
                For Each dr As Data.DataRow In dtRequisitoModalidad.Rows
                    For Each _Item As ListItem In cmbModalidad.Items
                        If _Item.Value = dr.Item("codigo_min") Then
                            _Item.Selected = True
                        End If
                    Next
                Next
            End If
            udpMantenimiento.Update()

            hddCod.Value = codigoReq
            udpHidden.Update()

        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            cmbTipoEstudio.SelectedValue = cmbFiltroTipoEstudio.SelectedValue

            If cmbTipoEstudio.SelectedValue <> "-1" Then
                mt_CargarComboModalidad(cmbModalidad, cmbTipoEstudio, udpModalidad, False)

                For Each _Item As ListItem In cmbModalidad.Items
                    If _Item.Value = cmbFiltroModalidad.SelectedValue Then
                        _Item.Selected = True
                    Else
                        _Item.Selected = False
                    End If
                Next
            End If

            txtDescripcion.Value = ""
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Function mt_ValidarFormulario() As Boolean
        Try
            If txtDescripcion.Value.Trim = "" Then
                mt_MostrarMensaje("Debe ingresar un nombre de requisito", 0)
                Return False
            End If
            If cmbTipoEstudio.SelectedValue = "-1" Then
                mt_MostrarMensaje("Debe seleccionar un tipo de estudio", 0)
                Return False
            End If
            If cmbModalidad.SelectedValue = "" Then
                mt_MostrarMensaje("Debe seleccionar una modalidad", 0)
                Return False
            End If
            Return True
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Function

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefrescarGrillaPostulantes()
        Try
            For Each _Row As GridViewRow In grwRequisitosAdmision.Rows
                grwRequisitosAdmision_RowDataBound(grwRequisitosAdmision, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub

    Private Sub mt_EliminarRequisito(ByVal codigoReq As Integer)
        Try
            Dim lo_Respuesta As Dictionary(Of String, String)
            Dim ls_Operacion As String = "D"
            Dim codUsuario As Integer = Session("id_per")
            lo_Respuesta = mo_Admision.RequisitoIUD(ls_Operacion, codigoReq:=codigoReq, codUsuario:=codUsuario)

            hddRpta.Value = lo_Respuesta.Item("rpta")
            hddMsg.Value = lo_Respuesta.Item("msg")
            udpHidden.Update()

            If hddRpta.Value = "1" Then
                mt_CargarGrillaRequisitosAdmision()
            End If

        Catch ex As Exception
            mt_MostrarMensaje(ex.Message, -1)
        End Try
    End Sub
#End Region

End Class
