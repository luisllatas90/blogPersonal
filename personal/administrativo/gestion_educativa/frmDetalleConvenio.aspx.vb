Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmDetalleConvenio
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private mo_RepoAdmision As New ClsAdmision
    Public mn_CodigoCparc As Integer
    Public mn_CodigoScc As Integer
    Public mn_CodigoCco As Integer
    Public mb_Modal As Boolean
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('Record Inserted Successfully')", True)
        mn_CodigoCparc = IIf(Request.QueryString("cparc") IsNot Nothing, Request.QueryString("cparc"), 0)
        mn_CodigoScc = IIf(Request.QueryString("scc") IsNot Nothing, Request.QueryString("scc"), 0)
        mn_CodigoCco = IIf(Request.QueryString("cco") IsNot Nothing, Request.QueryString("cco"), 0)
        mb_Modal = IIf(Request.QueryString("modal") IsNot Nothing, Request.QueryString("modal"), 0)

        Dim cssClassBotonesAccion As String = IIf(mb_Modal, "d-none", "")
        divBotonesAccion.Attributes.Item("class") = cssClassBotonesAccion

        If Not IsPostBack Then
            LimpiarSession()
            CargarCombosFormulario()
            AsignarValoresFormulario()
            BindGrwCuotas()
        Else
            LimpiarValores()
        End If
    End Sub

    Private Sub LimpiarSession()
        Session.Remove("cuotas")
        Session.Remove("cuotasEliminadas")
    End Sub

    Private Sub LimpiarValores()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()
    End Sub

    Protected Sub btnGenerarCuotas_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarCuotas.ServerClick
        GenerarCuotas(txtCuotas.Text)
    End Sub

    Public Sub btnEditarCuota_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('Record Inserted Successfully')", True)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim index As String = button.Attributes("data-index")
        grwCuotas.EditIndex = index
        BindGrwCuotas()
    End Sub

    Protected Sub grwCuotas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCuotas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim nroColumnas As Integer = e.Row.Cells.Count
            Dim lnkEditar As LinkButton = e.Row.Cells(nroColumnas - 1).FindControl("btnEditar")
            Dim lnkActualizar As LinkButton = e.Row.Cells(nroColumnas - 1).FindControl("btnActualizar")
            Dim lnkCancelar As LinkButton = e.Row.Cells(nroColumnas - 1).FindControl("btnCancelar")

            lnkEditar.Visible = Not (grwCuotas.EditIndex = e.Row.RowIndex)
            lnkActualizar.Visible = (grwCuotas.EditIndex = e.Row.RowIndex)
            lnkCancelar.Visible = (grwCuotas.EditIndex = e.Row.RowIndex)
        End If
    End Sub

    Protected Sub grwCuotas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grwCuotas.RowEditing
        grwCuotas.EditIndex = e.NewEditIndex
        BindGrwCuotas()
    End Sub

    Protected Sub grwCuotas_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grwCuotas.RowUpdating
        Dim row As GridViewRow = grwCuotas.Rows(e.RowIndex)
        Dim nroCuota As Integer = CType(row.FindControl("txtNroCuota"), TextBox).Text.Trim
        Dim fechaVencimiento As String = CType(row.FindControl("txtFechaVencimiento"), TextBox).Text.Trim
        Dim monto As String = CType(row.FindControl("txtMonto"), TextBox).Text.Trim

        Dim validacion As Dictionary(Of String, String) = ValidarCuota(fechaVencimiento, monto)
        If validacion.Item("rpta") = "1" Then
            ActualizarCuota(e.RowIndex, nroCuota, fechaVencimiento, monto)
            grwCuotas.EditIndex = -1
            BindGrwCuotas()
        Else
            GenerarMensajeServidor("Advertencia", validacion.Item("rpta"), validacion.Item("msg"))
        End If
    End Sub

    Private Sub GenerarMensajeServidor(ByVal titulo As String, ByVal rpta As Integer, ByVal mensaje As String)
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = titulo
        udpMensajeServidorHeader.Update()

        divRespuestaPostback.Attributes.Item("data-rpta") = rpta
        divRespuestaPostback.Attributes.Item("data-msg") = mensaje
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub ActualizarCuota(ByVal index As Integer _
        , ByVal nroCuota As Integer _
        , ByVal fechaVencimiento As DateTime _
        , ByVal monto As Decimal _
    )
        Dim dtCuotas As Data.DataTable = Session("cuotas")
        Dim drCuota As Data.DataRow = dtCuotas.Rows(index)
        drCuota.Item("nro") = nroCuota
        drCuota.Item("fechaVencimiento") = fechaVencimiento.ToString("dd/MM/yyyy")
        drCuota.Item("monto") = monto
    End Sub

    Protected Sub grwCuotas_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grwCuotas.RowCancelingEdit
        grwCuotas.EditIndex = -1
        BindGrwCuotas()
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Guardar()
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarCombosFormulario()
        ClsFunciones.LlenarListas(cmbTipoParticipante, mo_RepoAdmision.ListarTipoParticipante(), "codigo_tpar", "descripcion_tpar", "-- Seleccione --")
    End Sub

    Private Sub AsignarValoresFormulario()
        Dim dtValoresFormulario As Data.DataTable = mo_RepoAdmision.ConsultarConvenioParticipante(mn_CodigoCparc, mn_CodigoScc)
        If dtValoresFormulario.Rows.Count > 0 Then
            Dim drValoresFormulario As Data.DataRow = dtValoresFormulario.Rows(0)
            spnCentroCosto.InnerHtml = drValoresFormulario.Item("descripcion_Cco").ToString.Trim
            spnServicioConcepto.InnerHtml = drValoresFormulario.Item("descripcion_Sco").ToString.Trim
            txtPrecio.Text = drValoresFormulario.Item("precio_Sco").ToString.Trim
            txtCuotas.Text = drValoresFormulario.Item("nroPartes_Sco").ToString.Trim
            cmbTipoParticipante.SelectedValue = drValoresFormulario.Item("codigo_tpar").ToString.Trim
            txtOperacion.Text = drValoresFormulario.Item("tipo_cparc").ToString.Trim

            Dim generar As Boolean = False
            If mn_CodigoCparc = 0 Then
                generar = True
            Else
                Dim dtCpard As Data.DataTable = mo_RepoAdmision.ListarCentroCostosParticipanteD(codigoCparc:=mn_CodigoCparc)
                If dtCpard.Rows.Count > 0 Then
                    CargarCuotas(dtCpard)
                Else
                    generar = True
                End If
            End If

            If generar Then
                txtFechaPrimeraCuota.Text = DateTime.Now.ToString("dd/MM/yyyy")
                GenerarCuotas(txtCuotas.Text)
            End If
        End If
    End Sub

    Private Sub GenerarCuotas(ByVal cuotas As Integer)
        grwCuotas.EditIndex = -1

        Dim cuotasEliminadas As List(Of Integer) = IIf(Session("cuotasEliminadas") IsNot Nothing, Session("cuotasEliminadas"), New List(Of Integer))
        Dim dtCuotasSession As Data.DataTable = DirectCast(Session("cuotas"), Data.DataTable)
        If dtCuotasSession IsNot Nothing Then
            For i As Integer = 0 To dtCuotasSession.Rows.Count - 1
                Dim drCuota As Data.DataRow = dtCuotasSession.Rows(i)
                If drCuota.Item("cpard") <> 0 Then
                    cuotasEliminadas.Add(drCuota.Item("cpard"))
                End If
            Next
            Session("cuotasEliminadas") = cuotasEliminadas
        End If

        Dim dtCuotas As New Data.DataTable
        dtCuotas.Columns.Add("cpard")
        dtCuotas.Columns.Add("nro")
        dtCuotas.Columns.Add("fechaVencimiento")
        dtCuotas.Columns.Add("monto")

        Dim precio As Decimal = txtPrecio.Text.Trim
        Dim cuota As Decimal = Math.Round(precio / cuotas, 2)
        Dim sum As Decimal = 0.0
        Dim fechaCuota As DateTime = DateTime.Parse(txtFechaPrimeraCuota.Text.Trim)

        For i As Integer = 1 To cuotas
            Dim drFila As Data.DataRow = dtCuotas.NewRow()
            drFila.Item("cpard") = 0
            drFila.Item("nro") = i
            drFila.Item("fechaVencimiento") = fechaCuota.AddMonths(i - 1).ToString("dd/MM/yyyy")
            cuota = IIf(i = cuotas, precio - sum, cuota)
            drFila.Item("monto") = cuota
            sum += cuota
            dtCuotas.Rows.Add(drFila)

            fechaCuota.AddMonths(1)
        Next
        Session("cuotas") = dtCuotas
        BindGrwCuotas()
    End Sub

    Private Sub CargarCuotas(ByVal dtCpard As Data.DataTable)
        Dim dtCuotas As New Data.DataTable
        dtCuotas.Columns.Add("cpard")
        dtCuotas.Columns.Add("nro")
        dtCuotas.Columns.Add("fechaVencimiento")
        dtCuotas.Columns.Add("monto")

        For i As Integer = 0 To dtCpard.Rows.Count - 1
            Dim drFilaCpard As Data.DataRow = dtCpard.Rows(i)
            If i = 0 Then
                txtFechaPrimeraCuota.Text = DateTime.Parse(drFilaCpard.Item("fechavenc_cpard")).ToString("dd/MM/yyyy")
            End If

            Dim drFilaCuota As Data.DataRow = dtCuotas.NewRow()
            drFilaCuota.Item("cpard") = drFilaCpard.Item("codigo_cpard")
            drFilaCuota.Item("nro") = drFilaCpard.Item("nrocuota_cpard")
            drFilaCuota.Item("fechaVencimiento") = DateTime.Parse(drFilaCpard.Item("fechavenc_cpard")).ToString("dd/MM/yyyy")
            drFilaCuota.Item("monto") = drFilaCpard.Item("monto_cpard")
            dtCuotas.Rows.Add(drFilaCuota)
        Next
        Session("cuotas") = dtCuotas
        BindGrwCuotas()
    End Sub

    Private Sub BindGrwCuotas()
        grwCuotas.DataSource = Session("cuotas")
        grwCuotas.DataBind()
        udpCuotas.Update()
    End Sub

    Private Sub Guardar()

        Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

        If lo_Validacion.Item("rpta") = 1 Then
            Dim codigoTpar As Integer = cmbTipoParticipante.SelectedValue
            Dim montoCparc As Decimal = txtPrecio.Text.Trim
            Dim cuotasCparc As Integer = txtCuotas.Text.Trim
            Dim tipoCparc As String = UCase(txtOperacion.Text.Trim)

            Dim dtCentroCostosParticipanteD As New Data.DataTable

            Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.GuardarCentroCostosParticipanteC( _
                mn_CodigoCparc _
                , mn_CodigoCco _
                , mn_CodigoScc _
                , codigoTpar _
                , montoCparc _
                , cuotasCparc _
                , tipoCparc _
            )

            If lo_Respuesta.Item("rpta") = "1" Then
                Dim dtCuotas As Data.DataTable = Session("cuotas")
                mn_CodigoCparc = lo_Respuesta.Item("cod")

                For i As Integer = 0 To dtCuotas.Rows.Count - 1
                    Dim drCuota As Data.DataRow = dtCuotas.Rows(i)
                    Dim codigoCpard As Integer = drCuota.Item("cpard")
                    Dim nrocuotaCpard As Integer = drCuota.Item("nro")
                    Dim fechavencCpard As Date = drCuota.Item("fechaVencimiento")
                    Dim montoCpard As Decimal = drCuota.Item("monto")

                    mo_RepoAdmision.GuardarCentroCostosParticipanteD( _
                        codigoCpard _
                        , mn_CodigoCparc _
                        , nrocuotaCpard _
                        , fechavencCpard _
                        , montoCpard _
                    )
                Next

                'Elimino las cuotas que se hayan sobreescrito
                Dim cuotasEliminadas As List(Of Integer) = Session("cuotasEliminadas")
                If cuotasEliminadas IsNot Nothing Then
                    For Each codigoCpard As Integer In cuotasEliminadas
                        mo_RepoAdmision.EliminarCentroCostosParticipanteD(codigoCpard)
                    Next
                End If
            End If

            divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
            With divRespuestaPostback.Attributes
                .Item("data-rpta") = lo_Respuesta.Item("rpta")
                .Item("data-msg") = lo_Respuesta.Item("msg")
            End With

        Else
            GenerarMensajeServidor("Advertencia", lo_Validacion.Item("rpta"), lo_Validacion.Item("msg"))
        End If
        udpMensajeServidorParametros.Update()
        udpMensajeServidorHeader.Update()
        udpMensajeServidorBody.Update()
    End Sub

    Private Function ValidarFormulario() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        Dim lb_Errores As Boolean = False

        If Not lb_Errores AndAlso String.IsNullOrEmpty(txtPrecio.Text.Trim) Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe ingresar un precio"
            lb_Errores = True
        End If

        If Not lb_Errores AndAlso String.IsNullOrEmpty(txtCuotas.Text.Trim) Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe ingresar un número de cuotas"
            lb_Errores = True
        End If

        If Not lb_Errores AndAlso cmbTipoParticipante.SelectedValue = "-1" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe seleccionar un tipo de participante"
            lb_Errores = True
        End If

        If Not lb_Errores AndAlso String.IsNullOrEmpty(txtOperacion.Text.Trim) Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe ingresar una operación"
            lb_Errores = True
        End If

        'Valido las cuotas
        If Not lb_Errores Then
            Dim precio As Decimal = txtPrecio.Text.Trim
            Dim dtCuotas As Data.DataTable = Session("cuotas")
            Dim precioCuotas As Decimal = 0
            For i As Integer = 0 To dtCuotas.Rows.Count - 1
                Dim drCuota As Data.DataRow = dtCuotas.Rows(i)
                precioCuotas += Decimal.Parse(drCuota.Item("monto"))
            Next

            If precioCuotas <> precio Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "La suma de las cuotas no coincide con el monto total"
                lb_Errores = True
            End If
        End If

        Return lo_Validacion
    End Function

    Private Function ValidarCuota(ByVal fechaVencimiento As String, ByVal monto As String) As Dictionary(Of String, String)
        Dim validacion As New Dictionary(Of String, String)
        validacion.Add("rpta", 1)
        validacion.Add("msg", "")

        Dim conErrores As Boolean = False

        Dim lb As DateTime
        If Not conErrores AndAlso Not DateTime.TryParse(fechaVencimiento, lb) Then
            validacion.Item("rpta") = 0
            validacion.Item("msg") = "Todas las cuotas deben tener una fecha válida"
            conErrores = True
        End If

        If Not conErrores AndAlso String.IsNullOrEmpty(monto) Then
            validacion.Item("rpta") = 0
            validacion.Item("msg") = "Todas las cuotas deben un asignado monto"
            conErrores = True
        End If

        Return validacion
    End Function
#End Region

End Class
