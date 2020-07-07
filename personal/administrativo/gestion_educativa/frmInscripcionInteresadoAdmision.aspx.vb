Imports System.Collections.Generic

Partial Class administrativo_pec_test_frmInscripcionInteresadoAdmision
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private mo_RepoAdmision As New ClsAdmision
    Public ms_CodigoPaiPeru As String = "156"
    Private ms_codigoAlu As String = "0" 'Si este formulario no recibe el código de alumno se generaría un nuevo registro
    Private ms_CodigoTestQuery As String = "0"
    Private ms_codigoTest As String = ""
    Private ms_NombreEve As String = "OFICINA ADMISIÓN"
    Private ms_CodigoCco As String = ""
    Private mb_IsModal As Boolean = False
    Private ms_Accion As String = ""
    Private ms_CorreoDefault As String = "ninguno@usat.edu.pe"
    'Variables / Entorno
    Private ms_CodigoOri As String = mo_RepoAdmision.ObtenerVariableGlobal("codigoOrigenWeb")
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
        '    Response.Redirect("../../../sinacceso.html")
        'End If

        'errorMensaje.InnerHtml = "cco: " & Request.Params("cco") & " - id: " & Request.Params("id")
        mb_IsModal = Request.Params("modal")
        ms_codigoTest = Request.Params("test")
        ms_Accion = Request.Params("accion")

        ms_CodigoTestQuery = IIf(ms_codigoTest = "1", "2", ms_codigoTest)

        If mb_IsModal Then
            botonesAccion.Attributes.Item("class") = "d-none"
        End If

        MostrarOcultarSecciones(ms_codigoTest)

        If Not String.IsNullOrEmpty(Request.QueryString("alu")) Then ms_codigoAlu = Request.QueryString("alu")
        If Not String.IsNullOrEmpty(Request.QueryString("cco")) Then ms_CodigoCco = Request.QueryString("cco")

        If Not IsPostBack Then
            CargarCombos()
            AsignarValoresFormulario(ms_codigoAlu)
            BuscarDeudasPendientes(txtDNI.Text.Trim, ms_CodigoCco)

            ViewState("datosPersonalesCargados") = (Integer.Parse(ms_codigoAlu) > 0)
            HabilitarDeshabilitarControlesPersona(False)
        Else
            ActualizarControlesPostback()
            RefrescarGrillaCoincidencias()
        End If
        MostrarOcultarBotonesModal()
    End Sub

    Protected Sub grwCoincidencias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCoincidencias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwCoincidencias.Columns.Count
            Dim ls_DNI As String = grwCoincidencias.DataKeys(e.Row.RowIndex).Values.Item("nroDocIdent")

            _cellsRow(0).Text = ln_Index

            'Seleccionar persona
            Dim lo_btnSeleccionarPersona As New HtmlButton()
            With lo_btnSeleccionarPersona
                .ID = "btnSeleccionarPersona" & ln_Index
                .Attributes.Add("data-dni", ls_DNI)
                .Attributes.Add("class", "btn btn-primary btn-sm")
                .Attributes.Add("type", "button")
                .InnerHtml = "<i class='fa fa-check-circle'></i>"
                AddHandler .ServerClick, AddressOf btnSeleccionarPersona_Click
            End With
            _cellsRow(ln_Columnas - 1).Controls.Add(lo_btnSeleccionarPersona)

            grwCoincidencias.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub lnkObtenerDatos_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObtenerDatos.ServerClick
        Dim lo_ValidacionDNI As Dictionary(Of String, String) = ValidarDNI()
        If lo_ValidacionDNI.Item("rpta") = 0 Then
            divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
            udpMensajeServidorParametros.Update()

            spnMensajeServidorTitulo.InnerHtml = "Alerta"
            udpMensajeServidorHeader.Update()

            With respuestaPostback.Attributes
                .Item("data-rpta") = lo_ValidacionDNI.Item("rpta")
                .Item("data-msg") = lo_ValidacionDNI.Item("msg")
                .Item("data-control") = lo_ValidacionDNI.Item("control")
            End With
            udpMensajeServidorBody.Update()

            MostrarOcultarBotonesModal()
            LimpiarControlesPersona()
        Else
            Dim ls_NumDoc As String = txtDNI.Text.Trim
            ObtenerDatosPersonales("DNIE", ls_NumDoc, ls_NumDoc)
        End If
        udpForm.Update()
    End Sub

    Protected Sub lnkObtenerDatosPorApellidos_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObtenerDatosPorApellidos.ServerClick
        Dim ls_ApePaterno As String = txtApellidoPaterno.Text.Trim
        Dim ls_ApeMaterno As String = txtApellidoMaterno.Text.Trim

        If String.IsNullOrEmpty(ls_ApePaterno) OrElse String.IsNullOrEmpty(ls_ApeMaterno) Then
            spnMensajeServidorTitulo.InnerHtml = "Alerta"
            udpMensajeServidorHeader.Update()

            divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
            With respuestaPostback.Attributes
                .Item("data-rpta") = "0"
                .Item("data-msg") = "Ingrese ambos apellidos para poder realizar la búsqueda"
                .Item("data-control") = ""
            End With
            udpMensajeServidorBody.Update()
        Else
            Dim ls_Valor As String = ls_ApePaterno & " " & ls_ApeMaterno
            Dim ls_NumDoc As String = txtDNI.Text.Trim
            Dim ls_Nombres As String = txtNombres.Text.Trim
            ObtenerDatosPersonales("APEE", ls_Valor, ls_NumDoc, ls_ApePaterno, ls_ApeMaterno)
        End If
        udpForm.Update()
    End Sub

    Protected Sub btnMensajeAceptarNuevaPersona_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMensajeAceptarNuevaPersona.ServerClick
        HabilitarDeshabilitarControlesPersona(True)
        udpForm.Update()
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        CargarComboProvincia(cmbProvincia, cmbDepartamento.SelectedValue)
        CargarComboDistrito(cmbDistrito, cmbProvincia.SelectedValue)
        udpDireccion.Update()
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        CargarComboDistrito(cmbDistrito, cmbProvincia.SelectedValue)
        udpDireccion.Update()
    End Sub

    Protected Sub cmbDepartamentoInstEduc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamentoInstEduc.SelectedIndexChanged
        CargarComboInstitucionEducativa()
        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub chkNoTieneCorreo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNoTieneCorreo.CheckedChanged
        If chkNoTieneCorreo.Checked Then
            txtEmail.Text = ms_CorreoDefault
            ViewState("txtEmail:Enabled") = txtEmail.Enabled.ToString
            txtEmail.Enabled = False
        Else
            txtEmail.Text = ""
            txtEmail.Enabled = Boolean.Parse(ViewState("txtEmail:Enabled"))
        End If
        udpEmail.Update()
    End Sub

    Protected Sub cmbInstitucionEducativa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInstitucionEducativa.SelectedIndexChanged
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
    End Sub

    Protected Sub cmbAnioEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAnioEstudio.SelectedIndexChanged
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
    End Sub

    Protected Sub cmbCicloIngreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCicloIngreso.SelectedIndexChanged
        CargarComboModalidadIngreso(ms_codigoTest, cmbCicloIngreso.SelectedValue, cmbCarreraProfesional.SelectedValue)
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
        udpModalidad.Update()
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        CargarComboModalidadIngreso(ms_codigoTest, cmbCicloIngreso.SelectedValue, cmbCarreraProfesional.SelectedValue)
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
        udpModalidad.Update()
    End Sub

    Protected Sub cmbModalidadIngreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModalidadIngreso.SelectedIndexChanged
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Guardar()
    End Sub

#End Region

#Region "Metodos"
    Private Sub HabilitarDeshabilitarControlesPersona(ByVal lb_Estado As Boolean)
        Dim lb_DatosCargados As Boolean = ViewState("datosPersonalesCargados")

        txtDNI.Enabled = Not lb_DatosCargados
        txtApellidoPaterno.Enabled = lb_Estado Or lb_DatosCargados
        txtApellidoMaterno.Enabled = lb_Estado Or lb_DatosCargados
        txtNombres.Enabled = lb_Estado Or lb_DatosCargados
        dtpFecNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbSexo.Enabled = lb_Estado Or lb_DatosCargados
        txtNumFijo.Enabled = lb_Estado Or lb_DatosCargados
        txtNumCelular.Enabled = lb_Estado Or lb_DatosCargados
        txtEmail.Enabled = lb_Estado Or lb_DatosCargados
        chkNoTieneCorreo.Enabled = lb_Estado Or lb_DatosCargados
        'Dirección
        cmbDepartamento.Enabled = lb_Estado Or lb_DatosCargados
        cmbProvincia.Enabled = lb_Estado Or lb_DatosCargados
        cmbDistrito.Enabled = lb_Estado Or lb_DatosCargados
        txtDireccion.Enabled = lb_Estado Or lb_DatosCargados
        'Institución educativa
        cmbDepartamentoInstEduc.Enabled = lb_Estado Or lb_DatosCargados
        cmbInstitucionEducativa.Enabled = lb_Estado Or lb_DatosCargados
        cmbAnioEstudio.Enabled = lb_Estado Or lb_DatosCargados
        'Datos laborales
        txtEmail2.Enabled = lb_Estado Or lb_DatosCargados
        cmbEstadoCivil.Enabled = lb_Estado Or lb_DatosCargados
        txtRuc.Enabled = lb_Estado Or lb_DatosCargados
        txtCentroLabores.Enabled = lb_Estado Or lb_DatosCargados
        txtCargoActual.Enabled = lb_Estado Or lb_DatosCargados
        'Inscripción
        cmbCicloIngreso.Enabled = lb_Estado Or lb_DatosCargados
        cmbCarreraProfesional.Enabled = lb_Estado Or lb_DatosCargados
        cmbTipoParticipante.Enabled = lb_Estado Or lb_DatosCargados
        cmbModalidadIngreso.Enabled = lb_Estado Or lb_DatosCargados
        cmbModalidadIngresoEC.Enabled = lb_Estado Or lb_DatosCargados
        If lb_Estado Then
            LimpiarControlesPersona()
        End If
    End Sub

    Private Sub MostrarOcultarSecciones(ByVal ls_CodigoTest As String)
        Select Case ls_CodigoTest
            Case "1"
                divCicloIngreso.Visible = True
                divCarreraProfesional.Visible = True
                divModalidadIngreso.Visible = True
                divEmailAlternativo.Visible = False
                divEstadoCivil.Visible = False
                divRUC.Visible = False
                divInfoEducativa.Visible = True
                divDatosLaborales.Visible = False
                divTipoParticipante.Visible = False
                divModalidadIngresoEC.Visible = False
            Case "6"
                divCicloIngreso.Visible = True
                divCarreraProfesional.Visible = False
                divModalidadIngreso.Visible = False
                divEmailAlternativo.Visible = True
                divEstadoCivil.Visible = True
                divRUC.Visible = True
                divInfoEducativa.Visible = False
                divDatosLaborales.Visible = True
                divTipoParticipante.Visible = True
                divModalidadIngresoEC.Visible = True
        End Select
    End Sub

    Private Sub MostrarOcultarBotonesModal(Optional ByVal ls_Tipo As String = "")
        Select Case ls_Tipo
            Case "NUEVA_PERSONA"
                btnMensajeAceptarNuevaPersona.Visible = True
                btnMensajeCancelarNuevaPersona.Visible = True
                btnMensajeCerrar.Visible = False
            Case Else
                btnMensajeAceptarNuevaPersona.Visible = False
                btnMensajeCancelarNuevaPersona.Visible = False
                btnMensajeCerrar.Visible = True
        End Select
        udpMensajeServidorFooter.Update()
    End Sub

    Private Sub LimpiarControlesPersona()
        'txtApellidoPaterno.Text = ""
        'txtApellidoMaterno.Text = ""
        txtNombres.Text = ""
        dtpFecNacimiento.Text = ""
        cmbSexo.SelectedValue = "-1"
        txtNumCelular.Text = ""
        txtNumFijo.Text = ""
        txtEmail.Text = ""
    End Sub

    Private Sub btnSeleccionarPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_DNI As String = button.Attributes("data-dni")
        ObtenerDatosPersonales("DNIE", ls_DNI, ls_DNI)
        udpForm.Update()
    End Sub

    Private Sub ActualizarControlesPostback()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()

        grwCoincidencias.Attributes.Item("data-mostrar") = "false"
        udpCoincidencias.Update()
    End Sub

    Private Sub RefrescarGrillaCoincidencias()
        For Each _Row As GridViewRow In grwCoincidencias.Rows
            grwCoincidencias_RowDataBound(grwCoincidencias, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub CargarCombos()
        CargarComboDepartamento(cmbDepartamento, ms_CodigoPaiPeru)
        cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)

        Dim lo_DtCicloIngreso As New Data.DataTable
        'If ms_codigoTest = 1 Then
        '    lo_DtCicloIngreso = mo_RepoAdmision.ListarProcesoAdmision() 'En ciclo ordinario, lista solo ciclos actuales
        'Else
        '    lo_DtCicloIngreso = mo_RepoAdmision.ListarProcesoAdmisionV3() 'Lista los ciclos de acuerdo a la columna admision_cac
        'End If

        lo_DtCicloIngreso = mo_RepoAdmision.ListarProcesoAdmisionV3() 'Lista los ciclos de acuerdo a la columna admision_cac

        ClsFunciones.LlenarListas(cmbCicloIngreso, lo_DtCicloIngreso, "codigo_Cac", "descripcion_Cac", "-- Seleccione --")
        'For Each _Row As Data.DataRow In lo_DtCicloIngreso.Rows
        '    cmbCicloIngreso.SelectedValue = _Row.Item("codigo_Cac")
        '    Exit For
        'Next
        cmbCicloIngreso_SelectedIndexChanged(Nothing, Nothing)

        CargarComboModalidadIngreso(ms_codigoTest, "", "")

        CargarComboDepartamento(cmbDepartamentoInstEduc, ms_CodigoPaiPeru)
        cmbDepartamentoInstEduc_SelectedIndexChanged(Nothing, Nothing)

        ClsFunciones.LlenarListas(cmbTipoParticipante, mo_RepoAdmision.ListarTipoParticipante(ms_CodigoCco), "codigo", "descripcion", "-- Seleccione --")

        Dim ls_CodigoTestPreGrado As String = "2"
        ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional(ls_CodigoTestPreGrado), "codigo_Cpf", "nombre_Cpf", "-- Seleccione --")
    End Sub

    Protected Sub CargarComboDepartamento(ByVal lo_Combo As DropDownList, ByVal ls_CodigoPai As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaDepartamentos(ls_CodigoPai), "codigo_Dep", "nombre_Dep", "-- Seleccione --")
    End Sub

    Protected Sub CargarComboProvincia(ByVal lo_Combo As DropDownList, ByVal ls_CodigoDep As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaProvincias(ls_CodigoDep), "codigo_Pro", "nombre_Pro", "-- Seleccione --")
    End Sub

    Protected Sub CargarComboDistrito(ByVal lo_Combo As DropDownList, ByVal ls_CodigoDep As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaDistritos(ls_CodigoDep), "codigo_Dis", "nombre_Dis", "-- Seleccione --")
    End Sub

    Private Sub CargarComboModalidadIngreso(ByVal ls_CodigoTest As String, ByVal ls_CodigoCac As String, ByVal ls_CodigoPpf As String)
        If ls_CodigoTest = "1" Then
            ClsFunciones.LlenarListas(cmbModalidadIngreso, mo_RepoAdmision.ListarModalidadIngreso("77", ls_CodigoTest, ls_CodigoCac, ls_CodigoPpf), "codigo_Min", "nombre_Min", "-- Seleccione --")
        Else
            ClsFunciones.LlenarListas(cmbModalidadIngresoEC, mo_RepoAdmision.ListarModalidadIngreso("7", ls_CodigoTest, 0, 0), "codigo_Min", "nombre_Min", "-- Seleccione --")
        End If
    End Sub

    Private Sub CargarComboInstitucionEducativa()
        Try
            Dim lo_DataSource As Data.DataTable = mo_RepoAdmision.ListarInstitucionEducativa("DEP", cmbDepartamentoInstEduc.SelectedValue)
            ClsFunciones.LlenarListas(cmbInstitucionEducativa, lo_DataSource, "codigo_Ied", "nombre_Ied", "-- Seleccione --")

            Dim dr() As System.Data.DataRow
            For Each item As ListItem In cmbInstitucionEducativa.Items
                dr = lo_DataSource.Select("codigo_Ied='" & item.Value & "'")
                If dr.Length > 0 Then
                    item.Attributes.Add("data-tokens", dr(0).Item("Nombre_ied").ToString)
                    item.Attributes.Add("data-subtext", dr(0).Item("Direccion_ied").ToString)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ObtenerDatosPersonales(ByVal ls_Tipo As String, ByVal ls_Valor As String, Optional ByVal ls_NumDoc As String = "", Optional ByVal ls_ApePat As String = "", Optional ByVal ls_ApeMat As String = "", Optional ByVal ls_Nombre As String = "")
        'If Not String.IsNullOrEmpty(ls_Valor) Then
        Dim lo_DtRespuesta As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales(ls_Tipo, ls_Valor, ls_NumDoc, ls_ApePat, ls_ApeMat, ls_Nombre)
        If lo_DtRespuesta.Rows.Count > 0 Then
            If lo_DtRespuesta.Rows.Count = 1 Then
                Dim lo_DrPersona As Data.DataRow = lo_DtRespuesta.Rows(0)
                txtDNI.Text = lo_DrPersona.Item("nroDocIdent")
                txtApellidoPaterno.Text = lo_DrPersona.Item("apellidoPaterno")
                txtApellidoMaterno.Text = lo_DrPersona.Item("apellidoMaterno")
                txtNombres.Text = lo_DrPersona.Item("nombres")
                dtpFecNacimiento.Text = lo_DrPersona.Item("fechaNacimiento")
                cmbSexo.SelectedValue = lo_DrPersona.Item("sexo")
                txtNumCelular.Text = lo_DrPersona.Item("telefonoCelular")
                txtNumFijo.Text = lo_DrPersona.Item("telefonoFijo")
                txtEmail.Text = lo_DrPersona.Item("emailPrincipal")
                cmbDepartamento.SelectedValue = lo_DrPersona.Item("codigoDep") : cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
                cmbProvincia.SelectedValue = lo_DrPersona.Item("codigoPro") : cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
                cmbDistrito.SelectedValue = lo_DrPersona.Item("codigoDis")
                txtDireccion.Text = lo_DrPersona.Item("direccion")
                cmbDepartamentoInstEduc.SelectedValue = lo_DrPersona.Item("codigoDep") : cmbDepartamentoInstEduc_SelectedIndexChanged(Nothing, Nothing)
                Dim ln_CodigoIed As Integer
                If Integer.TryParse(lo_DrPersona.Item("codigoIed"), ln_CodigoIed) Then
                    If ln_CodigoIed > 0 Then
                        cmbInstitucionEducativa.SelectedValue = ln_CodigoIed
                    End If
                End If
                cmbAnioEstudio.SelectedValue = lo_DrPersona.Item("anioEstudios")
                Dim ln_CodigoCpf As Integer
                If Integer.TryParse(lo_DrPersona.Item("codigoCpf"), ln_CodigoCpf) Then
                    If ln_CodigoCpf > 0 Then
                        cmbCarreraProfesional.SelectedValue = ln_CodigoCpf : cmbCarreraProfesional_SelectedIndexChanged(Nothing, Nothing)
                    End If
                End If
                BuscarDeudasPendientes(txtDNI.Text.Trim, ms_CodigoCco)
                ViewState("datosPersonalesCargados") = True
                HabilitarDeshabilitarControlesPersona(False)
                udpForm.Update()
            Else
                'Cargar estos datos en un datagrid
                grwCoincidencias.DataSource = lo_DtRespuesta
                grwCoincidencias.DataBind()
                grwCoincidencias.Attributes.Item("data-mostrar") = "true"
                udpCoincidencias.Update()
            End If
        Else
            divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
            With respuestaPostback.Attributes
                .Item("data-rpta") = 0
                .Item("data-msg") = "Es una PERSONA NUEVA, ¿Desea continuar con el registro?"
            End With
            MostrarOcultarBotonesModal("NUEVA_PERSONA")
            udpMensajeServidorBody.Update()

            'divEventoCRM.Visible = True
            'udpDivEventoCRM.Update()
        End If
        'End If
    End Sub

    Private Sub BuscarDeudasPendientes(ByVal ls_DNI As String, ByVal ls_CodigoCco As String)
        Dim ls_CodigoPso As String = ""
        Dim lo_DtDeudas As New Data.DataTable

        Dim lo_DtPersona As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales("DNIE", ls_DNI, ls_DNI)
        If lo_DtPersona.Rows.Count > 0 Then
            ls_CodigoPso = lo_DtPersona.Rows(0).Item("codigoPso")

            If Not String.IsNullOrEmpty(ls_CodigoPso) Then
                lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorPersonaCentroCosto(ls_CodigoPso, ls_CodigoCco)
                If lo_DtDeudas.Rows.Count = 0 Then
                    Dim ln_codigoTest As Integer = 2 'PREGRADO
                    lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorPersonaTipoEstudio(ls_CodigoPso, ln_codigoTest)
                End If
            End If
        End If

        If lo_DtDeudas.Rows.Count > 0 Then
            divDeudas.Attributes.Item("data-mostrar") = "true"
        Else
            divDeudas.Attributes.Item("data-mostrar") = "false"
        End If
        grwDeudas.DataSource = lo_DtDeudas
        grwDeudas.DataBind()
    End Sub

    Private Sub AsignarValoresFormulario(ByVal ls_CodigoAlu As String)
        Try
            Dim lo_DrCentroCosto As Data.DataRow = mo_RepoAdmision.ObtenerCentroCosto(ms_CodigoCco)
            lblCentroCosto.InnerHtml = lo_DrCentroCosto.Item("descripcion_Cco")

            If ls_CodigoAlu <> 0 Then
                Dim ls_TipoConsulta As String = "I"
                Dim lo_DtInscripcion As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(ls_CodigoAlu, ls_TipoConsulta)

                If lo_DtInscripcion.Rows.Count > 0 Then
                    Dim lo_Row As Data.DataRow = lo_DtInscripcion.Rows(0)
                    txtDNI.Text = lo_Row.Item("numeroDocIdent_Pso")
                    txtApellidoPaterno.Text = lo_Row.Item("apellidoPaterno_Pso")
                    txtApellidoMaterno.Text = lo_Row.Item("apellidoMaterno_Pso")
                    txtNombres.Text = lo_Row.Item("nombres_Pso")
                    dtpFecNacimiento.Text = lo_Row.Item("fechaNacimiento_Pso")
                    cmbSexo.SelectedValue = lo_Row.Item("sexo_Pso")
                    txtNumCelular.Text = lo_Row.Item("telefonoCelular_Pso")
                    txtNumFijo.Text = lo_Row.Item("telefonoFijo_Pso")
                    txtEmail.Text = lo_Row.Item("emailPrincipal_Pso")
                    cmbDepartamento.SelectedValue = lo_Row.Item("codigo_Dep") : cmbDepartamento_SelectedIndexChanged(cmbDepartamento, EventArgs.Empty)
                    cmbProvincia.SelectedValue = lo_Row.Item("codigo_Pro") : cmbProvincia_SelectedIndexChanged(cmbProvincia, EventArgs.Empty)
                    cmbDistrito.SelectedValue = lo_Row.Item("codigo_Dis")
                    txtDireccion.Text = lo_Row.Item("direccion_Pso")
                    cmbDepartamentoInstEduc.SelectedValue = lo_Row.Item("codigo_DepIe") : cmbDepartamentoInstEduc_SelectedIndexChanged(cmbDepartamentoInstEduc, EventArgs.Empty)
                    cmbInstitucionEducativa.SelectedValue = lo_Row.Item("codigo_ied")
                    cmbCicloIngreso.SelectedValue = lo_Row.Item("codigo_Cac") : cmbCicloIngreso_SelectedIndexChanged(cmbCicloIngreso, EventArgs.Empty)
                    cmbCarreraProfesional.SelectedValue = lo_Row.Item("tempcodigo_cpf") : cmbCarreraProfesional_SelectedIndexChanged(cmbCarreraProfesional, EventArgs.Empty)
                    cmbTipoParticipante.SelectedValue = lo_Row.Item("codigo_tpar")
                    If ms_codigoTest = "1" Then
                        cmbModalidadIngreso.SelectedValue = lo_Row.Item("codigo_Min")
                    Else
                        cmbModalidadIngresoEC.SelectedValue = lo_Row.Item("codigo_Min")
                    End If
                    cmbEstadoCivil.SelectedValue = lo_Row.Item("estadoCivil_Dal")
                    txtCargoActual.Text = lo_Row.Item("cargoActual_Dal")
                    txtCentroLabores.Text = lo_Row.Item("centroTrabajo_Dal")
                    txtRuc.Text = lo_Row.Item("nroRuc_Pso")
                    txtEmail2.Text = lo_Row.Item("emailAlternativo_Pso")
                    errorMensaje.InnerHtml = lo_Row.Item("codigo_DepIe")
                Else
                    txtDNI.Text = ""
                    txtApellidoPaterno.Text = ""
                    txtApellidoMaterno.Text = ""
                    txtNombres.Text = ""
                    dtpFecNacimiento.Text = ""
                    cmbSexo.SelectedValue = ""
                    txtNumCelular.Text = ""
                    txtNumFijo.Text = ""
                    txtEmail.Text = ""
                    cmbDepartamento.SelectedValue = "" : cmbDepartamento_SelectedIndexChanged(cmbDepartamento, EventArgs.Empty)
                    cmbProvincia.SelectedValue = "" : cmbProvincia_SelectedIndexChanged(cmbProvincia, EventArgs.Empty)
                    cmbDistrito.SelectedValue = ""
                    txtDireccion.Text = ""
                    cmbDepartamentoInstEduc.SelectedValue = "" : cmbDepartamentoInstEduc_SelectedIndexChanged(cmbDepartamentoInstEduc, EventArgs.Empty)
                    cmbInstitucionEducativa.SelectedValue = ""
                    cmbCicloIngreso.SelectedValue = "" : cmbCicloIngreso_SelectedIndexChanged(cmbCicloIngreso, EventArgs.Empty)
                    cmbCarreraProfesional.SelectedValue = "" : cmbCarreraProfesional_SelectedIndexChanged(cmbCarreraProfesional, EventArgs.Empty)
                    cmbModalidadIngreso.SelectedValue = ""
                    cmbEstadoCivil.SelectedValue = ""
                    txtCargoActual.Text = ""
                    txtCentroLabores.Text = ""
                    txtRuc.Text = ""
                    txtEmail2.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ObtenerCalculosPension(ByVal ls_codigoCpf As String, ByVal ls_codigoIed As String, ByVal ls_codigoMin As String)
        rowCostos.Attributes.Add("data-oculto", True)
        If ls_codigoIed <> "-1" AndAlso ls_codigoCpf <> "-1" AndAlso ls_codigoMin <> "-1" _
            AndAlso (cmbAnioEstudio.SelectedValue = "E" OrElse cmbAnioEstudio.SelectedValue = "Q") Then

            Dim lo_DtRespuesta As Data.DataTable = mo_RepoAdmision.CalcularCategoriaEstudiante(ls_codigoCpf, ls_codigoIed, ls_codigoMin)
            If lo_DtRespuesta.Rows.Count > 0 Then
                Dim lo_Row As Data.DataRow = lo_DtRespuesta.Rows(0)
                spnCostoCredito.InnerText = "S/" & Decimal.Parse(lo_Row.Item("preciocredito")).ToString("N2")
                spnCostoMes.InnerText = "S/" & Decimal.Parse(lo_Row.Item("costomes")).ToString("N2")
                spnCostoCiclo.InnerText = "S/" & Decimal.Parse(lo_Row.Item("costociclo")).ToString("N2")
                rowCostos.Attributes.Remove("data-oculto")
            End If
        End If
        udpCostos.Update()
    End Sub

    Private Function ValidarDNI() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        Dim lb_Errores As Boolean = False

        If String.IsNullOrEmpty(txtDNI.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe asignar un número de DNI"
                lo_Validacion.Item("control") = "txtDNI"
                lb_Errores = True
            End If
            txtDNI.Attributes.Item("data-error") = "true"
        Else
            txtDNI.Attributes.Item("data-error") = "false"
        End If

        Dim ls_FormatDNI As String = "^\d{8}$"
        If Not Regex.IsMatch(txtDNI.Text.Trim, ls_FormatDNI) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = "0"
                lo_Validacion.Item("msg") = "El DNI ingresado no es correcto"
                lo_Validacion.Item("control") = "txtDNI"
                lb_Errores = True
            End If
            txtDNI.Attributes.Item("data-error") = "true"
        Else
            txtDNI.Attributes.Item("data-error") = "false"
        End If

        Return lo_Validacion
    End Function

    Private Function ValidarFormulario() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        Dim lb_Errores As Boolean = False

        lo_Validacion = ValidarDNI()
        If lo_Validacion.Item("rpta") = 0 Then
            lb_Errores = True
        End If

        If String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe ingresar su apellido paterno"
                lo_Validacion.Item("control") = "txtApellidoPaterno"
                lb_Errores = True
            End If
            txtApellidoPaterno.Attributes.Item("data-error") = "true"
        Else
            txtApellidoPaterno.Attributes.Item("data-error") = "false"
        End If

        If String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe ingresar su apellido materno"
                lo_Validacion.Item("control") = "txtApellidoMaterno"
                lb_Errores = True
            End If
            txtApellidoMaterno.Attributes.Item("data-error") = "true"
        Else
            txtApellidoMaterno.Attributes.Item("data-error") = "false"
        End If

        If String.IsNullOrEmpty(txtNombres.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe ingresar sus nombres"
                lo_Validacion.Item("control") = "txtNombres"
                lb_Errores = True
            End If
            txtNombres.Attributes.Item("data-error") = "true"
        Else
            txtNombres.Attributes.Item("data-error") = "false"
        End If

        If String.IsNullOrEmpty(txtEmail.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe ingresar un correo electrónico"
                lo_Validacion.Item("control") = "txtEmail"
                lb_Errores = True
            End If
            txtEmail.Attributes.Item("data-error") = "true"
            Return lo_Validacion
        Else
            txtEmail.Attributes.Item("data-error") = "false"
        End If

        Dim ls_FormatEmail As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        If Not Regex.IsMatch(txtEmail.Text.Trim, ls_FormatEmail) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = "0"
                lo_Validacion.Item("msg") = "El correo electrónico no es válido"
                lo_Validacion.Item("control") = "txtEmail"
                lb_Errores = True
            End If
            txtEmail.Attributes.Item("data-error") = "true"
        Else
            txtEmail.Attributes.Item("data-error") = "false"
        End If

        If cmbDistrito.SelectedValue = "-1" Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = "0"
                lo_Validacion.Item("msg") = "Debe seleccionar un distrito"
                lo_Validacion.Item("control") = "cmbDistrito"
                lb_Errores = True
            End If
            cmbDistrito.Attributes.Item("data-error") = "true"
        Else
            cmbDistrito.Attributes.Item("data-error") = "false"
        End If

        If String.IsNullOrEmpty(txtDireccion.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe ingresar una dirección"
                lo_Validacion.Item("control") = "txtDireccion"
                lb_Errores = True
            End If
            txtDireccion.Attributes.Item("data-error") = "true"
        Else
            txtDireccion.Attributes.Item("data-error") = "false"
        End If

        If ms_codigoTest = "1" Then 'Escuela PRE
            If cmbAnioEstudio.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar un año de estudio"
                    lo_Validacion.Item("control") = "cmbAnioEstudio"
                    lb_Errores = True
                End If
                cmbAnioEstudio.Attributes.Item("data-error") = "true"
            Else
                cmbAnioEstudio.Attributes.Item("data-error") = "false"
            End If

            If cmbCicloIngreso.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar un proceso de admisión"
                    lo_Validacion.Item("control") = "cmbCicloIngreso"
                    lb_Errores = True
                End If
                cmbCicloIngreso.Attributes.Item("data-error") = "true"
            Else
                cmbCicloIngreso.Attributes.Item("data-error") = "false"
            End If

            'If cmbEventoCRM.SelectedValue = "-1" Then
            '    If Not lb_Errores Then
            '        lo_Validacion.Item("rpta") = "0"
            '        lo_Validacion.Item("msg") = "Debe seleccionar un evento CRM"
            '        lo_Validacion.Item("control") = "cmbEventoCRM"
            '        lb_Errores = True
            '    End If
            '    cmbEventoCRM.Attributes.Item("data-error") = "true"
            'Else
            '    cmbEventoCRM.Attributes.Item("data-error") = "false"
            'End If

            If cmbModalidadIngreso.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar una modalidad"
                    lo_Validacion.Item("control") = "cmbModalidadIngreso"
                    lb_Errores = True
                End If
                cmbModalidadIngreso.Attributes.Item("data-error") = "true"
            Else
                cmbModalidadIngreso.Attributes.Item("data-error") = "false"
            End If
        End If

        If ms_codigoTest = "6" Then 'Educación continua
            If cmbEstadoCivil.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar estado civil"
                    lo_Validacion.Item("control") = "cmbEstadoCivil"
                    lb_Errores = True
                End If
                cmbEstadoCivil.Attributes.Item("data-error") = "true"
            Else
                cmbEstadoCivil.Attributes.Item("data-error") = "false"
            End If

            If Not String.IsNullOrEmpty(txtEmail2.Text.Trim) Then
                If Not Regex.IsMatch(txtEmail2.Text.Trim, ls_FormatEmail) Then
                    If Not lb_Errores Then
                        lo_Validacion.Item("rpta") = "0"
                        lo_Validacion.Item("msg") = "El correo electrónico no es válido"
                        lo_Validacion.Item("control") = "txtEmail2"
                        lb_Errores = True
                    End If
                    txtEmail2.Attributes.Item("data-error") = "true"
                Else
                    txtEmail2.Attributes.Item("data-error") = "false"
                End If
            End If

            If Not String.IsNullOrEmpty(txtRuc.Text.Trim) Then
                Dim ls_FormatRUC As String = "^\d{11}$"
                If Not Regex.IsMatch(txtRuc.Text.Trim, ls_FormatRUC) Then
                    If Not lb_Errores Then
                        lo_Validacion.Item("rpta") = "0"
                        lo_Validacion.Item("msg") = "El RUC ingresado no es correcto"
                        lo_Validacion.Item("control") = "txtRuc"
                        lb_Errores = True
                    End If
                    txtRuc.Attributes.Item("data-error") = "true"
                Else
                    txtRuc.Attributes.Item("data-error") = "false"
                End If
            End If

            If String.IsNullOrEmpty(txtCentroLabores.Text.Trim) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Debe ingresar un centro de labores"
                    lo_Validacion.Item("control") = "txtCentroLabores"
                    lb_Errores = True
                End If
                txtCentroLabores.Attributes.Item("data-error") = "true"
            Else
                txtCentroLabores.Attributes.Item("data-error") = "false"
            End If

            If String.IsNullOrEmpty(txtCargoActual.Text.Trim) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Debe ingresar un cargo actual"
                    lo_Validacion.Item("control") = "txtCargoActual"
                    lb_Errores = True
                End If
                txtCargoActual.Attributes.Item("data-error") = "true"
            Else
                txtCargoActual.Attributes.Item("data-error") = "false"
            End If

            If cmbModalidadIngresoEC.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar una modalidad"
                    lo_Validacion.Item("control") = "cmbModalidadIngreso"
                    lb_Errores = True
                End If
                cmbModalidadIngresoEC.Attributes.Item("data-error") = "true"
            Else
                cmbModalidadIngresoEC.Attributes.Item("data-error") = "false"
            End If
        End If

        Return lo_Validacion
    End Function

    Private Sub Guardar()
        Try
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            If lo_Validacion.Item("rpta") = 1 Then
                'Dim ls_NombreEve As String = IIf(cmbEventoCRM.SelectedValue = "-1", "", cmbEventoCRM.Text)
                Dim ls_DescripcionCac As String = "" 'Lo obtengo en el procedimiento, a partir del codigo_cac
                Dim ls_TipoDoc As String = "1"
                Dim ls_NroDoc As String = UCase(txtDNI.Text.Trim)
                Dim ls_ApePaterno As String = UCase(txtApellidoPaterno.Text.Trim)
                Dim ls_ApeMaterno As String = UCase(txtApellidoMaterno.Text.Trim)
                Dim ls_Nombres As String = UCase(txtNombres.Text.Trim)
                Dim ls_FechaNacimiento As String = dtpFecNacimiento.Text.Trim
                Dim ls_Sexo As String = cmbSexo.SelectedValue
                Dim ls_Grado As String = IIf(ms_codigoTest = "1", cmbAnioEstudio.SelectedValue, "P")
                Dim ls_CodigoIed As String = IIf(cmbInstitucionEducativa.SelectedValue = "-1", 0, cmbInstitucionEducativa.SelectedValue)
                Dim ls_CodigoCpf As String = IIf(cmbCarreraProfesional.SelectedValue = "-1", 0, cmbCarreraProfesional.SelectedValue)
                Dim ls_Estado As String = "1"
                Dim ls_Telefono As String = txtNumFijo.Text.Trim
                Dim ls_Celular As String = txtNumCelular.Text.Trim
                Dim ls_Email As String = UCase(txtEmail.Text.Trim)
                Dim ls_CodigoDep As String = cmbDepartamento.SelectedValue
                Dim ls_CodigoPro As String = cmbProvincia.SelectedValue
                Dim ls_CodigoDis As String = cmbDistrito.SelectedValue
                Dim ls_Direccion As String = UCase(txtDireccion.Text.Trim)
                Dim ls_CodigoCac As String = cmbCicloIngreso.SelectedValue
                Dim ls_CodigoMin As String = IIf(ms_codigoTest = "1", cmbModalidadIngreso.SelectedValue, cmbModalidadIngresoEC.SelectedValue)
                Dim ls_CodigoTest As String = ms_codigoTest '"1" 'Escuela PRE
                Dim ls_UsuarioReg As String = Request.QueryString("id")
                Dim ls_email2 As String = txtEmail2.Text
                Dim ls_EstadoCivil As String = cmbEstadoCivil.SelectedValue
                Dim ls_PasswordAlu As String = mo_RepoAdmision.GeneraClave(ls_ApePaterno, ls_Nombres)
                Dim ls_NroRuc As String = txtRuc.Text
                Dim ls_CentroLabores As String = txtCentroLabores.Text
                Dim ls_CargoActual As String = txtCargoActual.Text
                Dim ls_CodigoTpar As String = cmbTipoParticipante.SelectedValue
                Dim ls_NumeroDocFin As String = ""
                Dim ls_ApepaternoFin As String = ""
                Dim ls_ApematernoFin As String = ""
                Dim ls_NombresFin As String = ""
                Dim ls_CelularFin As String = ""
                Dim ls_EmailFin As String = ""
                Dim ls_ValidaDeuda As String = "0"
                Dim lb_GeneraCargo As Boolean = False
                Dim ls_accion As String = ms_Accion

                'Dim lo_DtEventoCRM As Data.DataTable = mo_RepoAdmision.BuscarEventoCRM(ls_NroDoc, ms_CodigoCco, ls_CodigoCac)
                Dim lo_DtEventoCRM As Data.DataTable = mo_RepoAdmision.BuscarEventoCRM(ls_NroDoc, "0", "0") 'PENDIENTE!: Por ahora estoy enviando ceros ya que no hay interesados con centros de costo
                If lo_DtEventoCRM.Rows.Count > 0 Then
                    ms_NombreEve = lo_DtEventoCRM.Rows(0).Item("nombre_eve")
                End If

                Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.GuardarInscripcion( _
                    ms_CodigoOri, _
                    ms_NombreEve, _
                    ls_DescripcionCac, _
                    ls_TipoDoc, _
                    ls_NroDoc, _
                    ls_ApePaterno, _
                    ls_ApeMaterno, _
                    ls_Nombres, _
                    ls_FechaNacimiento, _
                    ls_Sexo, _
                    ls_Grado, _
                    ls_CodigoIed, _
                    ls_CodigoCpf, _
                    ls_Estado, _
                    ls_Telefono, _
                    ls_Celular, _
                    ls_Email, _
                    ls_CodigoDep, _
                    ls_CodigoPro, _
                    ls_CodigoDis, _
                    ls_Direccion, _
                    ms_CodigoCco, _
                    ls_CodigoCac, _
                    ls_CodigoMin, _
                    ls_CodigoTest, _
                    ls_email2, _
                    ls_EstadoCivil, _
                    ls_PasswordAlu, _
                    ls_NroRuc, _
                    ls_CentroLabores, _
                    ls_CargoActual, _
                    ls_CodigoTpar, _
                    ls_NumeroDocFin, _
                    ls_ApepaternoFin, _
                    ls_ApematernoFin, _
                    ls_NombresFin, _
                    ls_CelularFin, _
                    ls_EmailFin, _
                    ls_ValidaDeuda, _
                    lb_GeneraCargo, _
                    ls_UsuarioReg, _
                    ls_accion _
                )

                Dim lo_DtPersona As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales("DNIE", ls_NroDoc, ls_NroDoc)
                If lo_DtPersona.Rows.Count > 0 Then
                    divMdlMenServParametros.Attributes.Item("data-codigo-pso") = lo_DtPersona.Rows(0).Item("codigoPso")
                End If
                divMdlMenServParametros.Attributes.Item("data-codigo-alu") = lo_Respuesta.Item("cod")

                divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
                With respuestaPostback.Attributes
                    .Item("data-rpta") = lo_Respuesta.Item("rpta")
                    .Item("data-msg") = lo_Respuesta.Item("msg")
                End With
            Else
                divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
                With respuestaPostback.Attributes
                    .Item("data-rpta") = lo_Validacion.Item("rpta")
                    .Item("data-msg") = lo_Validacion.Item("msg")
                    .Item("data-control") = lo_Validacion.Item("control")
                End With
            End If
        Catch ex As Exception
            divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
            With respuestaPostback.Attributes
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        Finally
            udpForm.Update()

            udpMensajeServidorParametros.Update()

            spnMensajeServidorTitulo.InnerHtml = "Alerta"
            udpMensajeServidorHeader.Update()
            udpMensajeServidorBody.Update()

            MostrarOcultarBotonesModal()
        End Try
    End Sub
#End Region
End Class
