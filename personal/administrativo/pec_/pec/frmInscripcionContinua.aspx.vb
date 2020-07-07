
Partial Class administrativo_pec_test_frmInscripcionContinua
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Private mo_Cnx As New ClsConectarDatos
    Private mo_RepoAdmision As New ClsAdmision

    Private ms_CodigoTfu As String = ""
    Private ms_CodigoPer As String = ""
    Private ms_CodigoTest As String = ""
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ms_CodigoTfu = Request.QueryString("ctf")
        ms_CodigoPer = Request.QueryString("id")
        ms_CodigoTest = Request.QueryString("mod")

        AddHandler btnRegistrarPostulante.ServerClick, AddressOf btnRegistrarPostulante_Click

        If Not IsPostBack Then
            CargarCombos()
        Else
            RefrescarGrillaPostulantes()
        End If
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, cmbEstadoAlumno.SelectedValue)
        udpPostulantes.Update()
    End Sub

    Protected Sub cmbEstadoAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstadoAlumno.SelectedIndexChanged
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, cmbEstadoAlumno.SelectedValue)
        udpPostulantes.Update()
    End Sub

    Protected Sub grwPostulantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPostulantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ls_codigoPso As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("codigo_pso")
            Dim ls_codigoAlu As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("cli")
            Dim ln_Index As Integer = e.Row.RowIndex + 1

            _cellsRow(0).Text = ln_Index

            'Ver movimientos
            Dim lo_btnInfo As New HtmlButton()
            With lo_btnInfo
                .ID = "btnInfoPostulante" & ln_Index
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-search-plus'></i>"
                AddHandler .ServerClick, AddressOf btnInfoPostulante_Click
            End With
            _cellsRow(9).Controls.Add(lo_btnInfo)

            'Editar postulante
            Dim lo_btnEditar As New HtmlButton
            With lo_btnEditar
                .ID = "btnEditarPostulante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-edit'></i>"
                AddHandler .ServerClick, AddressOf btnEditarPostulante_Click
            End With
            _cellsRow(10).Controls.Add(lo_btnEditar)

            'Imprimir ficha?
            Dim lo_btnImprimir As New HtmlButton
            With lo_btnImprimir
                .ID = "btnImprimirFichaPostulante" & ln_Index
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-print'></i>"
                'PENDIENTE: Falta añadir el evento
            End With
            _cellsRow(11).Controls.Add(lo_btnImprimir)

            'Requisitos
            Dim lo_btnRequisitos As New HtmlButton
            With lo_btnRequisitos
                .ID = "btnRequisitosPostulante" & ln_Index
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-secondary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-check-square'></i>"
                'PENDIENTE: Falta añadir el evento
            End With
            _cellsRow(12).Controls.Add(lo_btnRequisitos)

            grwPostulantes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    'Eventos delegados
    Private Sub btnInfoPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoPso As String = button.Attributes("data-pso")
        CargarGrillaMovimientosPostulante(ls_CodigoPso, cmbCentroCosto.SelectedValue)
    End Sub

    Private Sub btnRegistrarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ifrmInscripcionPostulante.Attributes("src") = "frmInscripcionInteresadoAdmision.aspx?modal=true&id=" & ms_CodigoPer & "&cco=" & cmbCentroCosto.SelectedValue
        udpInscripcionPostulante.Update()
    End Sub

    Private Sub btnRefrescarGrillaPostulantes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefrescarGrillaPostulantes.ServerClick
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, cmbEstadoAlumno.SelectedValue)
    End Sub

    Private Sub btnEditarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        ifrmMantenimientoPostulante.Attributes("src") = "frmActualizarDatosPostulante.aspx?modal=true&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&alu=" & ls_CodigoAlu
        udpMantenimientoPostulante.Update()
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        ClsFunciones.LlenarListas(cmbCentroCosto, mo_RepoAdmision.ListarCentroCosto(ms_CodigoTfu, ms_CodigoPer, ms_CodigoTest), "codigo_Cco", "Nombre", "-- Seleccione --")
    End Sub

    Private Sub CargarGrillaPostulantes(ByVal ln_CodigoCco As Integer, ByVal ls_EstadoAlumno As String)
        If ln_CodigoCco > -1 Then
            grwPostulantes.DataSource = mo_RepoAdmision.ListarPostulante(ln_CodigoCco, ls_EstadoAlumno, "")
            grwPostulantes.DataBind()
        End If
    End Sub

    Private Sub CargarGrillaMovimientosPostulante(ByVal ls_CodigoPso As String, ByVal ls_CodigoCco As String)
        grwMovimientosPostulante.DataSource = mo_RepoAdmision.ListarMovimientosPostulante(ls_CodigoPso, ls_CodigoCco)
        grwMovimientosPostulante.DataBind()
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefrescarGrillaPostulantes()
        For Each _Row As GridViewRow In grwPostulantes.Rows
            grwPostulantes_RowDataBound(grwPostulantes, New GridViewRowEventArgs(_Row))
        Next
    End Sub
#End Region
End Class
