Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmConfigurarCentroCostoParticipante
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private mo_RepoAdmision As New ClsAdmision
    Private ms_CodigoTfu As String = ""
    Private ms_CodigoPer As String = ""
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & codigoCco & "')", True)
        ms_CodigoTfu = Request.QueryString("ctf")
        ms_CodigoPer = Request.QueryString("id")

        If Not IsPostBack Then
            CargarCombos()
        End If
    End Sub

    Protected Sub cmbTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEstudio.SelectedIndexChanged
        CargarComboCentroCosto()
        cmbCentroCosto.Enabled = (cmbTipoEstudio.SelectedValue <> "-1")
        udpCentroCosto.Update()
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        CargarListaServicios()
    End Sub

    Protected Sub btnListarServicios_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarServicios.ServerClick
        CargarListaServicios()
    End Sub

    Protected Sub grwServicioConcepto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwServicioConcepto.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim codigoScc As String = grwServicioConcepto.DataKeys(e.Row.RowIndex).Values.Item("codigo_Scc")
            Dim ln_Columnas As Integer = grwServicioConcepto.Columns.Count
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            _cellsRow(0).Text = ln_Index

            Dim rowData As Data.DataRowView = TryCast(e.Row.DataItem, Data.DataRowView)

            Dim _CheckBox As CheckBox = e.Row.FindControl("chkServicioConcepto")
            _CheckBox.InputAttributes.Item("class") = "custom-control-input"
            _CheckBox.InputAttributes.Item("data-codigo_scc") = codigoScc
            _CheckBox.Checked = rowData.Item("principal_sco")
        End If
    End Sub

    Public Sub btnDetalleConvenio_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim codigoCparc As String = button.Attributes("data-cparc")
        Dim codigoScc As String = button.Attributes("data-scc")
        ifrmDetalleConvenio.Attributes("src") = "frmDetalleConvenio.aspx?modal=true&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&cparc=" & codigoCparc & "&scc=" & codigoScc
        udpDetalleConvenio.Update()
    End Sub

    Public Sub chkServicioConcepto_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim columns As Integer = grwServicioConcepto.Columns.Count
        Dim checkbox As CheckBox = TryCast(sender, CheckBox)
        Dim codigoScc As String = checkbox.InputAttributes.Item("data-codigo_scc")

        'Actualizo en base de datos
        Dim lo_Resultado As Dictionary(Of String, String) = mo_RepoAdmision.CambiarPrincipalServicioCentroCosto(codigoScc, checkbox.Checked)

        If checkbox.Checked Then
            For Each _Row As GridViewRow In grwServicioConcepto.Rows
                Dim _checkBox As CheckBox = _Row.FindControl("chkServicioConcepto")
                Dim _codigoScc As String = _checkBox.InputAttributes.Item("data-codigo_scc")
                If codigoScc <> _codigoScc Then
                    Dim update As Boolean = False
                    If _checkBox.Checked Then
                        update = True
                    End If
                    _checkBox.Checked = False
                    If update Then
                        chkServicioConcepto_CheckedChanged(_checkBox, New EventArgs)
                    End If
                End If
            Next
        End If
        udpServicioConcepto.Update()
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        CargarComboCentroCosto()
    End Sub

    Private Sub CargarComboCentroCosto()
        Dim codigoTest As String = cmbTipoEstudio.SelectedValue
        Dim dtTipoEstudio As New Data.DataTable
        If codigoTest <> "-1" Then
            dtTipoEstudio = mo_RepoAdmision.ListarCentroCosto(ms_CodigoTfu, ms_CodigoPer, codigoTest)
        End If
        ClsFunciones.LlenarListas(cmbCentroCosto, dtTipoEstudio, "codigo_Cco", "Nombre", "-- Seleccione --")
        cmbCentroCosto_SelectedIndexChanged(Nothing, Nothing)
        udpCentroCosto.Update()
    End Sub

    Private Sub CargarListaServicios()
        Dim dtServicioCentroCosto As Data.DataTable
        Dim codigoCco As String = cmbCentroCosto.SelectedValue

        If codigoCco <> "-1" Then
            Dim tipo As String = "DET" 'Detallado
            dtServicioCentroCosto = mo_RepoAdmision.ConsultarServicioPorCeco(codigoCco, tipo)
        Else
            dtServicioCentroCosto = New Data.DataTable
        End If
        grwServicioConcepto.DataSource = dtServicioCentroCosto
        grwServicioConcepto.DataBind()
        udpServicioConcepto.Update()
    End Sub
#End Region
End Class
