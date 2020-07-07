Imports iTextSharp.text
Imports System.IO
Imports iTextSharp.text.pdf

Partial Class administrativo_pec_test_frmInscritos
    Inherits System.Web.UI.Page

#Region "Variables de clase"
    Private mo_Cnx As New ClsConectarDatos
    Private mo_RepoAdmision As New ClsAdmision

    Private ms_CodigoTfu As String = ""
    Private ms_CodigoPer As String = ""
    Private ms_CodigoTest As String = ""

    Public mn_FilasPostu As Integer = 0
    Public mn_FilasPorPaginaPostu As Integer = 0
    Public mn_PaginaPostu As Integer = 1
    Public mn_RangoPaginasPostu As Integer = 5 'Máxima cantidad de botones a la derecha e izquierda en el paginador
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ms_CodigoTfu = Request.QueryString("ctf")
        ms_CodigoPer = Request.QueryString("id")
        ms_CodigoTest = Request.QueryString("mod")

        urlMod.Value = ms_CodigoTest
        urlId.Value = ms_CodigoPer
        urlCtf.Value = ms_CodigoTfu

        InicializarControles()

        If Not IsPostBack Then
            ViewState.Item("dtPostulantes") = Nothing
            CargarCombos()
        Else
            RefrescarGrillaPostulantes()
            'RefrescarGrillaIngresantes()

            'Obtengo de una vez la cantidad de filas para no estar haciendo varias llamadas
            mn_FilasPostu = mo_RepoAdmision.ContarPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, Me.cmbEstadoAlumno.SelectedValue)
            mn_FilasPorPaginaPostu = cmbFilasPorPaginaPostu.SelectedValue
            mn_PaginaPostu = IIf(ViewState.Item("PaginaPostu") IsNot Nothing, ViewState.Item("PaginaPostu"), 1)
            GenerarPaginador(mn_RangoPaginasPostu)
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ViewState.Item("PaginaPostu") = mn_PaginaPostu
    End Sub

    Protected Sub cmbCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCentroCosto.SelectedIndexChanged
        EstadoBotonesAccion()
        udpFiltrosInteresados.Update()

        If cmbCentroCosto.SelectedValue <> "-1" Then
            mn_PaginaPostu = 1
            CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, mn_PaginaPostu, mn_FilasPorPaginaPostu)
            udpPostulantes.Update()
        Else
            LimpiarGrillaPostulantes()
        End If

        LimpiarGrillaIngresantes()
        GenerarPaginador(mn_RangoPaginasPostu)
    End Sub

    Protected Sub cmbEstadoAlumno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstadoAlumno.SelectedIndexChanged
        mn_PaginaPostu = 1
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Protected Sub cmbFilasPorPaginaPostu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFilasPorPaginaPostu.SelectedIndexChanged
        mn_PaginaPostu = 1
        mn_FilasPorPaginaPostu = cmbFilasPorPaginaPostu.SelectedValue
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Protected Sub grwPostulantes_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwPostulantes.DataBound
        ViewState.Item("dtPostulantes") = grwPostulantes.DataSource
    End Sub

    Protected Sub grwPostulantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPostulantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ls_codigoPso As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("codigo_pso")
            Dim ls_codigoAlu As String = grwPostulantes.DataKeys(e.Row.RowIndex).Values.Item("cli")
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwPostulantes.Columns.Count

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
            _cellsRow(ln_Columnas - 6).Controls.Add(lo_btnInfo)

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
            _cellsRow(ln_Columnas - 5).Controls.Add(lo_btnEditar)

            'Generar Convenio
            Dim lo_btnGenerarConvenio As New HtmlButton
            With lo_btnGenerarConvenio
                .ID = "btnGenerarConvenio" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-success btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-money-bill-alt'></i>"
                AddHandler .ServerClick, AddressOf btnGenerarConvenio_Click
            End With
            _cellsRow(ln_Columnas - 4).Controls.Add(lo_btnGenerarConvenio)

            'Financiar en cuotas
            Dim lo_btnFinanciar As New HtmlButton
            With lo_btnFinanciar
                .ID = "btnFinanciar" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("data-pso", ls_codigoPso)
                .Attributes.Add("class", "btn btn-secondary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-dollar-sign'></i>"
                AddHandler .ServerClick, AddressOf btnFinanciar_Click
            End With
            _cellsRow(ln_Columnas - 3).Controls.Add(lo_btnFinanciar)

            'Imprimir ficha
            Dim lo_btnImprimir As New HtmlButton
            With lo_btnImprimir
                .ID = "btnImprimirFichaPecIngresante" & ln_Index
                .Attributes.Add("data-alu", ls_codigoAlu)
                .Attributes.Add("class", "btn btn-info btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-print'></i>"
            End With
            _cellsRow(ln_Columnas - 2).Controls.Add(lo_btnImprimir)

            'Reporte convenio
            Dim lo_BtnConvenio As New HtmlButton
            Dim ls_CodigoCco As String = cmbCentroCosto.SelectedValue
            Dim ls_Path As String = "/reportServer/?/PRIVADOS/PENSIONES/PEN_DeudasxCco&codigo_cco=" & ls_CodigoCco & "&codigo_pso=" & ls_codigoPso
            With lo_BtnConvenio
                .ID = "btnImprimirConvenio" & ln_Index
                .Attributes.Add("class", "btn btn-danger btn-sm")
                .InnerHtml = "<i class='fa fa-file-pdf'></i>"
                .Attributes.Item("onClick") = "window.open('" & ls_Path & "')"
            End With
            _cellsRow(ln_Columnas - 1).Controls.Add(lo_BtnConvenio)

            grwPostulantes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    'Eventos delegados
    Private Sub btnInfoPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoPso As String = button.Attributes("data-pso")
        CargarGrillaMovimientosPostulante(ls_CodigoPso, cmbCentroCosto.SelectedValue)
        udpMovimientosPostulante.Update()
    End Sub

    Public Sub btnPagePostulantes_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        mn_PaginaPostu = button.Attributes("data-pagina")
        CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, mn_PaginaPostu, mn_FilasPorPaginaPostu)
        udpPostulantes.Update()
    End Sub

    Private Sub btnListarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbCentroCosto.SelectedValue <> "-1" Then
            mn_PaginaPostu = 1
            CargarGrillaPostulantes(cmbCentroCosto.SelectedValue, txtFiltroDNI.Text.Trim, cmbEstadoAlumno.SelectedValue, mn_PaginaPostu, mn_FilasPorPaginaPostu)
            udpPostulantes.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Private Sub btnRegistrarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cmbCentroCosto.SelectedValue <> "-1" Then
            ifrmInscripcionPostulante.Attributes("src") = "frmInscripcionInteresadoAdmision.aspx?modal=true&id=" & ms_CodigoPer & "&cco=" & cmbCentroCosto.SelectedValue & "&test=" & ms_CodigoTest & "&accion=N"
            udpInscripcionPostulante.Update()
            udpMensajeServidorBody.Update()
        Else
            GenerarMensajeServidor("Advertencia", 0, "Primero debe seleccionar un centro de costos")
        End If
    End Sub

    Private Sub btnEditarPostulante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        'ifrmMantenimientoPostulante.Attributes("src") = "frmActualizarDatosPostulante.aspx?modal=true&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&cco=" & cmbCentroCosto.SelectedValue & "&alu=" & ls_CodigoAlu
        ifrmMantenimientoPostulante.Attributes("src") = "frmInscripcionInteresadoAdmision.aspx?modal=true&mod=" & ms_CodigoTest & "&id=" & ms_CodigoPer & "&ctf=" & ms_CodigoTfu & "&cco=" & cmbCentroCosto.SelectedValue & "&alu=" & ls_CodigoAlu & "&test=" & ms_CodigoTest & "&accion=M"
        udpMantenimientoPostulante.Update()
    End Sub

    Private Sub btnGenerarConvenio_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        ifrmGenerarConvenio.Attributes("src") = "frmGenerarConvenio.aspx?modal=true" & "&id=" & ms_CodigoPer & "&alu=" & ls_CodigoAlu
        udpGenerarConvenio.Update()
    End Sub

    Private Sub btnFinanciar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_CodigoAlu As String = button.Attributes("data-alu")
        Dim ls_TipoCparc As String = "CONVENIO"
        Dim lo_DtDeudas As Data.DataTable = mo_RepoAdmision.ConsultarDeudasPorAlumnoTipoParticipante(ls_CodigoAlu, ls_TipoCparc)

        If lo_DtDeudas.Rows.Count > 0 Then
            GenerarMensajeServidor("Advertencia", 0, "Ya se ha generado el cronograma de pagos.")
        Else
            Dim ls_CodigoPso As String = button.Attributes("data-pso")
            ifrmFinanciamiento.Attributes("src") = "frmFinanciarInscripcion.aspx?modal=true" & "&id=" & ms_CodigoPer & "&alu=" & ls_CodigoAlu & "&cco=" & cmbCentroCosto.SelectedValue & "&ctf=" & ms_CodigoTfu & "&pso=" & ls_CodigoPso & "&test=" & ms_CodigoTest
            udpFinanciamiento.Update()
        End If
    End Sub

    Private Sub btnListarIngresante_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CargarGrillaIngresantes()
        udpIngresantes.Update()
    End Sub

    Protected Sub grwIngresantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwIngresantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Index As Integer = e.Row.RowIndex + 1

            _cellsRow(0).Text = ln_Index

            grwIngresantes.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub InicializarControles()
        AddHandler btnListarPostulante.ServerClick, AddressOf btnListarPostulante_Click
        AddHandler btnRegistrarPostulante.ServerClick, AddressOf btnRegistrarPostulante_Click

        mensajeServer.InnerHtml = ""
        udpMensajeServidorBody.Update()

        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        divMdlMenServParametros.Attributes.Item("data-rpta") = 0
        udpMensajeServidorParametros.Update()
    End Sub

    Private Sub EstadoBotonesAccion()
        If cmbCentroCosto.SelectedValue <> "-1" Then
            btnListarPostulante.Attributes.Remove("disabled")
            btnRegistrarPostulante.Attributes.Remove("disabled")
            btnExportarInteresados.Attributes.Remove("disabled")

            btnListarIngresante.Attributes.Remove("disabled")
            btnExportarIngresantes.Attributes.Remove("disabled")
        Else
            btnListarPostulante.Attributes.Item("disabled") = "disabled"
            btnRegistrarPostulante.Attributes.Item("disabled") = "disabled"
            btnExportarInteresados.Attributes.Item("disabled") = "disabled"

            btnListarIngresante.Attributes.Item("disabled") = "disabled"
            btnExportarIngresantes.Attributes.Item("disabled") = "disabled"
        End If
        udpBotonesInteresados.Update()
        udpBotonesIngresantes.Update()
        udpFiltrosInteresados.Update()
    End Sub

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String)
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        divMdlMenServParametros.Attributes.Item("data-rpta") = ln_Rpta
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        mensajeServer.InnerHtml = ls_Mensaje
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub GenerarPaginador(ByVal ln_RangoPaginas As Integer)
        pgrPostulantes.Controls.Clear()

        Dim lo_DtPostulantes As Data.DataTable = ViewState.Item("dtPostulantes")
        If lo_DtPostulantes IsNot Nothing AndAlso lo_DtPostulantes.Rows.Count > 0 Then
            rowPagination.Visible = True

            Dim ln_Desde As Integer = ((mn_PaginaPostu - 1) * mn_FilasPorPaginaPostu) + 1
            Dim ln_Hasta As Integer = Math.Min(IIf(mn_FilasPorPaginaPostu > 0, mn_PaginaPostu * mn_FilasPorPaginaPostu, mn_FilasPostu), mn_FilasPostu)
            lblPaginacion.InnerHtml = "Mostrando registros: " & ln_Desde & " - " & ln_Hasta & " de " & mn_FilasPostu

            If cmbCentroCosto.SelectedValue <> "-1" AndAlso mn_FilasPorPaginaPostu > 0 Then
                Dim ln_Paginas As Integer = Math.Ceiling(mn_FilasPostu / mn_FilasPorPaginaPostu)

                'Página de Inicio
                Dim lo_liPageStart As New HtmlGenericControl("li")
                lo_liPageStart.Attributes.Item("class") = "page-item"
                Dim lo_btnPageStart As New HtmlButton
                With lo_btnPageStart
                    .ID = "btnPagePostuStart"
                    .Attributes.Item("type") = "button"
                    .Attributes.Item("class") = "page-link"
                    .Attributes.Item("data-pagina") = 1
                    .InnerHtml = "<span aria-hidden='true'>&laquo;</span><span class='sr-only'>Inicio</span>"
                End With
                AddHandler lo_btnPageStart.ServerClick, AddressOf btnPagePostulantes_Click
                If mn_PaginaPostu = 1 Then
                    lo_liPageStart.Attributes.Item("class") = lo_liPageStart.Attributes.Item("class") & " disabled"
                End If
                lo_liPageStart.Controls.Add(lo_btnPageStart)
                pgrPostulantes.Controls.Add(lo_liPageStart)

                'Páginas
                Dim lb_Separador As Boolean = False
                For i As Integer = 1 To ln_Paginas
                    If i <= (mn_PaginaPostu + ln_RangoPaginas) AndAlso i >= (mn_PaginaPostu - ln_RangoPaginas) Then
                        Dim lo_liPage As New HtmlGenericControl("li")
                        lo_liPage.Attributes.Item("class") = "page-item"
                        Dim lo_btnPage As New HtmlButton
                        With lo_btnPage
                            .ID = "btnPagePostu" & i
                            .Attributes.Item("type") = "button"
                            .Attributes.Item("class") = "page-link"
                            .Attributes.Item("data-pagina") = i
                            .InnerHtml = i
                        End With

                        If i = mn_PaginaPostu Then
                            lo_liPage.Attributes.Item("class") = lo_liPage.Attributes.Item("class") & " active disabled"
                        End If

                        AddHandler lo_btnPage.ServerClick, AddressOf btnPagePostulantes_Click
                        lo_liPage.Controls.Add(lo_btnPage)
                        pgrPostulantes.Controls.Add(lo_liPage)

                        lb_Separador = False
                    Else
                        If Not lb_Separador Then
                            Dim lo_liSeparador As New HtmlGenericControl("li")
                            lo_liSeparador.Attributes.Item("class") = "page-item disabled"
                            Dim lo_btnPageSeparador As New HtmlButton
                            With lo_btnPageSeparador
                                .ID = "btnPagePostuSeparador" & i
                                .Attributes.Item("type") = "button"
                                .Attributes.Item("class") = "page-link"
                                .InnerHtml = "..."
                            End With
                            lo_liSeparador.Controls.Add(lo_btnPageSeparador)
                            pgrPostulantes.Controls.Add(lo_liSeparador)

                            lb_Separador = True
                        End If
                    End If
                Next

                'Página Final
                Dim lo_liPageEnd As New HtmlGenericControl("li")
                lo_liPageEnd.Attributes.Item("class") = "page-item"
                Dim lo_btnPageEnd As New HtmlButton
                With lo_btnPageEnd
                    .ID = "btnPagePostuEnd"
                    .Attributes.Item("type") = "button"
                    .Attributes.Item("class") = "page-link"
                    .Attributes.Item("data-pagina") = ln_Paginas
                    .InnerHtml = "<span aria-hidden='true'>&raquo;</span><span class='sr-only'>Fin</span>"
                End With
                AddHandler lo_btnPageEnd.ServerClick, AddressOf btnPagePostulantes_Click
                If mn_PaginaPostu = ln_Paginas Then
                    lo_liPageEnd.Attributes.Item("class") = lo_liPageEnd.Attributes.Item("class") & " disabled"
                End If
                lo_liPageEnd.Controls.Add(lo_btnPageEnd)
                pgrPostulantes.Controls.Add(lo_liPageEnd)
            End If
        Else
            rowPagination.Visible = False
        End If
        udpPgrPostulantes.Update()
    End Sub

    Private Sub CargarCombos()
        ClsFunciones.LlenarListas(cmbCentroCosto, mo_RepoAdmision.ListarCentroCosto(ms_CodigoTfu, ms_CodigoPer, ms_CodigoTest), "codigo_Cco", "Nombre", "-- Seleccione --")
        cmbCentroCosto_SelectedIndexChanged(Nothing, Nothing)

        'ClsFunciones.LlenarListas(cmbCicloAcademico, mo_RepoAdmision.ListarCicloAcademico(), "cicloIng_Alu", "cicloIng_Alu", "-- Todos --  ")
        'ClsFunciones.LlenarListas(cmbModalidadIngreso, mo_RepoAdmision.ListarModalidadIngresoPorTipo("TO"), "codigo_Min", "nombre_Min", "-- Todos --  ")
        'ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional("2"), "codigo_cpf", "nombre_cpf", "-- Todos --")
    End Sub

    Private Sub CargarGrillaPostulantes(ByVal ln_CodigoCco As Integer, ByVal ls_FiltroDNI As String, ByVal ls_EstadoAlumno As String, ByVal ln_Pagina As Integer, ByVal ln_FilasPorPagina As Integer)
        Dim lo_DtPostulantes As New Data.DataTable
        If ln_CodigoCco > -1 Then
            'grwPostulantes.DataSource = mo_RepoAdmision.ListarPostulante(ln_CodigoCco, ls_EstadoAlumno, "")
            lo_DtPostulantes = mo_RepoAdmision.ListarPostulante(ln_CodigoCco, ls_FiltroDNI, ls_EstadoAlumno, ln_Pagina, ln_FilasPorPagina)
            grwPostulantes.DataSource = lo_DtPostulantes
            grwPostulantes.DataBind()

            GenerarPaginador(mn_RangoPaginasPostu)
        End If
        EstadoBotonesAccion()
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

    Private Sub CargarGrillaIngresantes()
        Dim ln_CodigoCco As Integer = cmbCentroCosto.SelectedValue
        If ln_CodigoCco > -1 Then
            'Dim ls_CodigoCac As String = cmbCicloAcademico.SelectedValue
            'Dim ln_CodigoMin As Integer = cmbModalidadIngreso.SelectedValue
            'grwIngresantes.DataSource = mo_RepoAdmision.ListarIngresante(ls_CodigoCac, ln_CodigoCco, ln_CodigoMin, 0)
            grwIngresantes.DataBind()
        End If
    End Sub

    Private Sub LimpiarGrillaPostulantes()
        grwPostulantes.DataSource = Nothing
        grwPostulantes.DataBind()
        udpPostulantes.Update()
    End Sub

    Private Sub LimpiarGrillaIngresantes()
        grwIngresantes.DataSource = Nothing
        grwIngresantes.DataBind()
        udpIngresantes.Update()
    End Sub
#End Region

End Class
