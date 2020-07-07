Imports System.Collections.Generic
Imports System.Net

Partial Class administrativo_pec_test_frmActualizarDatosPostulante
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private mo_Cnx As New ClsConectarDatos
    Private mo_RepoAdmision As New ClsAdmision
    Public ms_CodigoPaiPeru As String = "156"

    Private ms_CodigoTfu As String = ""
    Private ms_CodigoCco As String = ""
    Private ms_CodigoPer As String = ""
    Private ms_CodigoTest As String = ""
    Private ms_CodigoTestAlt As String = ""
    Private ms_CodigoAlu As String = "0"
    Private ms_DefaultCodigoIed As String = "15928" 'ESTUDIANTE EXTRANJERO
    Private ms_CodigoIedAplicacion As String = "15873"
    Private ms_Accion As String = ""
    Private ms_CorreoDefault As String = "ninguno@usat.edu.pe"
    Private ms_NombreEve As String = "OFICINA ADMISIÓN"
    Private mb_SoloEdicionDatos As Boolean = False
    Private ms_CodigoUniversitario As String = ""

    'Envio de correo
    'Private md_EnvioCorreosMasivo As New d_EnvioCorreosMasivo
    'Private me_EnvioCorreosMasivo As e_EnvioCorreosMasivo

    'Variables / Entorno
    Private ms_CodigoOri As String = mo_RepoAdmision.ObtenerVariableGlobal("codigoOficinaInformes")
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            'If Session("id_per") = "" Or Request.QueryString("id") = "" Then
            '    Response.Redirect("../../../sinacceso.html")
            'End If

            mo_Cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ms_CodigoTest = Request.QueryString("mod") : ms_CodigoTest = IIf(ms_CodigoTest = "-1", 0, ms_CodigoTest)
            ms_CodigoTestAlt = IIf(ms_CodigoTest = "1", "2", ms_CodigoTest)

            ms_CodigoTfu = Request.QueryString("ctf")
            ms_CodigoCco = Request.QueryString("cco")
            ms_CodigoPer = Request.QueryString("id")
            ms_Accion = Request.QueryString("accion")
            mb_SoloEdicionDatos = IIf(Not String.IsNullOrEmpty(Request.QueryString("nv")), Request.QueryString("nv"), False)

            If Not IsPostBack Then
                LimpiarDatosOrig()

                If Not String.IsNullOrEmpty(Request.QueryString("alu")) Then
                    ViewState("codigo_alu") = Request.QueryString("alu")
                    ViewState("editando") = (ViewState("codigo_alu") <> "0")
                Else
                    BuscarDeudasPendientes(txtNroDocIdentidad.Text.Trim, ms_CodigoCco)
                End If

                If mb_SoloEdicionDatos Then
                    '23/04/2019 andy.diaz: Solo quito las pestañas, los campos del formulario deben estar ocultos pero con la información cargada
                    liDatosPostulacion.Visible = False ': divDatosPostulacion.Visible = False
                    liInformacionEducativa.Visible = False ': divDatosPostulacion.Visible = False
                End If
            End If
            ms_CodigoAlu = ViewState("codigo_alu")

            HabilitarDeshabilitarControles(ms_CodigoAlu)

            rbtInstPublica.Enabled = False
            rbtInstPrivada.Enabled = False

            rbtColegioAplicacionNo.Enabled = False
            rbtColegioAplicacionSi.Enabled = False

            divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
            udpMensajeServidorParametros.Update()

            grwCoincidencias.Attributes.Item("data-mostrar") = "false"
            udpCoincidencias.Update()
            RefrescarGrillaCoincidencias()

            With respuestaPostback.Attributes
                .Item("data-rpta") = ""
                .Item("data-msg") = ""
                .Item("data-control") = ""
            End With
            udpMensajeServidorBody.Update()

            MostrarOcultarBotonesModal()

            If Not IsPostBack Then
                CargarCombos()

                frmDatosPostulante.Attributes.Item("data-alu") = ms_CodigoAlu
                divNivelacion.Visible = IIf(ms_CodigoAlu <> "0", True, False)

                chkNivCompetencia1.Enabled = False
                txtCalifNivCompetencia1.Enabled = False
                chkNivCompetencia2.Enabled = False
                txtCalifNivCompetencia2.Enabled = False
                chkNivCompetencia3.Enabled = False
                txtCalifNivCompetencia3.Enabled = False
                chkNivCompetencia4.Enabled = False
                txtCalifNivCompetencia4.Enabled = False

                cmbCarreraProfesional.Attributes.Item("data-live-search") = True
                cmbPaisInstitucion.Attributes.Item("data-live-search") = True
                cmbPaisNacimiento.Attributes.Item("data-live-search") = True
                cmbInstitucionEducativa.Attributes.Item("data-live-search") = True
                frmDatosPostulante.Attributes.Item("data-edit") = IIf(ms_CodigoAlu = "0", False, True)

                AsignarValoresFormulario(ms_CodigoAlu)
                HabilitarDeshabilitarControlesPersona(False)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub LimpiarDatosOrig()
        ViewState.Remove("origPrecioCredito")
        ViewState.Remove("newPrecioCredito")
        ViewState.Remove("origNombreCpf")
        ViewState.Remove("origNombreMin")
        ViewState.Remove("origNombreIed")
    End Sub

    Private Sub HabilitarDeshabilitarControles(ByVal ls_CodigoAlu As String)
        If ls_CodigoAlu = "0" Then
            cmbCicloIngreso.Enabled = True
        Else
            cmbCicloIngreso.Enabled = False
        End If
    End Sub

    Protected Sub lnkObtenerDatos_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkObtenerDatos.ServerClick
        Dim lo_ValidacionDNI As Dictionary(Of String, String) = ValidarDNI()
        If lo_ValidacionDNI.Item("rpta") = 0 Then
            GenerarMensajeServidor("Alerta", lo_ValidacionDNI.Item("rpta"), lo_ValidacionDNI.Item("msg"), lo_ValidacionDNI.Item("control"))
            ViewState("datosPersonalesCargados") = False
            'MostrarOcultarBotonesModal()
            LimpiarControlesPersona()
            Exit Sub
        End If

        Dim ls_NumDoc As String = txtNroDocIdentidad.Text.Trim
        Dim lo_ValidacionSituacionAcademica As Dictionary(Of String, String) = mo_RepoAdmision.ConsultarSituacionAcademicaPersona(ls_NumDoc)
        If lo_ValidacionSituacionAcademica.Item("rpta") <> 1 Then
            GenerarMensajeServidor("Error", lo_ValidacionSituacionAcademica.Item("rpta"), lo_ValidacionSituacionAcademica.Item("msg"))
            ViewState("datosPersonalesCargados") = False
            'MostrarOcultarBotonesModal()
            HabilitarDeshabilitarControlesPersona(False)
            LimpiarControlesPersona()
            Exit Sub
        End If

        ObtenerDatosPersonales("DNIU", ls_NumDoc, ls_NumDoc)
        udpDatosPersonales.Update()
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
            Dim ls_NumDoc As String = txtNroDocIdentidad.Text.Trim
            Dim ls_Nombres As String = txtNombres.Text.Trim
            ObtenerDatosPersonales("APEE", ls_Valor, ls_NumDoc, ls_ApePaterno, ls_ApeMaterno)
        End If
        udpDatosPersonales.Update()
    End Sub

    Protected Sub btnMensajeAceptarNuevaPersona_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMensajeAceptarNuevaPersona.ServerClick
        HabilitarDeshabilitarControlesPersona(True)
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

    Protected Sub chkNoTieneCorreo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNoTieneCorreo.CheckedChanged
        If chkNoTieneCorreo.Checked Then
            txtEmail.Text = ms_CorreoDefault
            ViewState("txtEmail:Enabled") = txtEmail.Enabled.ToString
            txtEmail.Enabled = False

            txtEmailAlternativo.Text = ""
            txtEmailAlternativo.Enabled = False
        Else
            txtEmail.Text = ""
            txtEmail.Enabled = Boolean.Parse(ViewState("txtEmail:Enabled"))

            txtEmailAlternativo.Enabled = True
        End If

        hddEmailCoincidente.Value = "0"
        hddEmailVerificado.Value = "0"

        udpEmail.Update()
    End Sub

    Protected Sub cmbCicloIngreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCicloIngreso.SelectedIndexChanged
        lr_CargarComboModalidadIngreso(ms_CodigoTest, cmbCicloIngreso.SelectedValue, cmbCarreraProfesional.SelectedValue)
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        lr_CargarComboModalidadIngreso(ms_CodigoTest, cmbCicloIngreso.SelectedValue, cmbCarreraProfesional.SelectedValue)
        If IsPostBack Then
            cmbModalidadIngreso.Enabled = True
        End If

        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
    End Sub

    Protected Sub cmbModalidadIngreso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbModalidadIngreso.SelectedIndexChanged
        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
        VisibilidadFechaPrimeraMatricula()
    End Sub

    Protected Sub cmbPaisNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPaisNacimiento.SelectedIndexChanged
        lr_CargarComboDepartamento(cmbDptoNacimiento, cmbPaisNacimiento.SelectedValue)
        cmbDptoNacimiento_SelectedIndexChanged(cmbDptoNacimiento, New EventArgs)
        udpLugarNacimiento.Update()
    End Sub

    Protected Sub cmbDptoNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoNacimiento.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvNacimiento, cmbDptoNacimiento.SelectedValue)
        cmbPrvNacimiento_SelectedIndexChanged(cmbPrvNacimiento, New EventArgs)
        udpLugarNacimiento.Update()
    End Sub

    Protected Sub cmbPrvNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvNacimiento.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstNacimiento, cmbPrvNacimiento.SelectedValue)
        udpLugarNacimiento.Update()
    End Sub

    Protected Sub cmbDptoActual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoActual.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvActual, cmbDptoActual.SelectedValue)
        cmbPrvActual_SelectedIndexChanged(cmbPrvActual, New EventArgs)
        udpDireccionActual.Update()
    End Sub

    Protected Sub cmbPrvActual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvActual.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstActual, cmbPrvActual.SelectedValue)
        udpDireccionActual.Update()
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Dim dtCorreos As Data.DataTable = mo_RepoAdmision.BuscarEmailInteresadoCoincidente(txtEmail.Text.Trim)
        Dim coincidente As Boolean = False
        Dim verificado As Boolean = False

        For Each _row As Data.DataRow In dtCorreos.Rows
            coincidente = True
            If _row.Item("verificado_emi") IsNot DBNull.Value Then
                If DirectCast(_row.Item("verificado_emi"), Boolean) Then
                    verificado = _row.Item("verificado_emi")
                    Exit For
                End If
            End If
        Next
        hddEmailCoincidente.Value = IIf(coincidente, "1", "0")
        hddEmailVerificado.Value = IIf(verificado, "1", "0")

        udpEmail.Update()
    End Sub

    Protected Sub grwDeudas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwDeudas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim colIndex As Integer = mo_RepoAdmision.GetColumnIndexByName(e.Row, "fechaVencimiento_Deu")
            Dim fechaVencimiento As Date = _cellsRow.Item(colIndex).Text
            If Date.Now > fechaVencimiento.Date Then
                e.Row.CssClass = "vencida"
            End If
        End If
    End Sub

    Protected Sub cmbPaisInstitucion_SelectedIntexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPaisInstitucion.SelectedIndexChanged
        lr_CargarComboDepartamento(cmbDptoInstitucion, cmbPaisInstitucion.SelectedValue)
        cmbDptoInstitucion_SelectedIndexChanged(cmbDptoInstitucion, New EventArgs)

        Dim lb_EstudioExtranjero As Boolean = (cmbPaisInstitucion.SelectedValue <> ms_CodigoPaiPeru)
        If lb_EstudioExtranjero Then
            lr_CargarComboInstitucionEducativaPorId(ms_DefaultCodigoIed)
            cmbInstitucionEducativa.SelectedValue = ms_DefaultCodigoIed
        End If
        cmbDptoInstitucion.Enabled = Not lb_EstudioExtranjero
        cmbPrvInstitucion.Enabled = Not lb_EstudioExtranjero
        cmbDstInstitucion.Enabled = Not lb_EstudioExtranjero
        cmbInstitucionEducativa.Enabled = Not lb_EstudioExtranjero

        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub cmbDptoInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoInstitucion.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvInstitucion, cmbDptoInstitucion.SelectedValue)
        cmbPrvInstitucion_SelectedIndexChanged(cmbPrvInstitucion, New EventArgs)
        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub cmbPrvInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvInstitucion.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstInstitucion, cmbPrvInstitucion.SelectedValue)
        cmbDstInstitucion_SelectedIndexChanged(cmbDstInstitucion, New EventArgs)
        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub cmbDstInstitucion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDstInstitucion.SelectedIndexChanged
        lr_CargarComboInstitucionEducativa(cmbDstInstitucion.SelectedValue)
        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub cmbInstitucionEducativa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInstitucionEducativa.SelectedIndexChanged
        lr_DatosAdicionalesInstitucion(cmbInstitucionEducativa.SelectedValue)
        udpInstEducDatosAdicionales.Update()

        ObtenerCalculosPension(cmbCarreraProfesional.SelectedValue, cmbInstitucionEducativa.SelectedValue, cmbModalidadIngreso.SelectedValue)
    End Sub

    Protected Sub cmbDptoPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoPadre.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvPadre, cmbDptoPadre.SelectedValue)
        cmbPrvPadre_SelectedIndexChanged(cmbPrvPadre, New EventArgs)
        udpDireccionPadre.Update()
    End Sub

    Protected Sub cmbPrvPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvPadre.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstPadre, cmbPrvPadre.SelectedValue)
        udpDireccionPadre.Update()
    End Sub

    Protected Sub cmbDptoMadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoMadre.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvMadre, cmbDptoMadre.SelectedValue)
        cmbPrvMadre_SelectedIndexChanged(cmbPrvMadre, New EventArgs)
        udpDireccionMadre.Update()
    End Sub

    Protected Sub cmbPrvMadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvMadre.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstMadre, cmbPrvMadre.SelectedValue)
        udpDireccionMadre.Update()
    End Sub

    Protected Sub cmbDptoApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDptoApoderado.SelectedIndexChanged
        lr_CargarComboProvincia(cmbPrvApoderado, cmbDptoApoderado.SelectedValue)
        cmbPrvApoderado_SelectedIndexChanged(cmbPrvApoderado, New EventArgs)
        udpDireccionApoderado.Update()
    End Sub

    Protected Sub cmbPrvApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrvApoderado.SelectedIndexChanged
        lr_CargarComboDistrito(cmbDstApoderado, cmbPrvApoderado.SelectedValue)
        udpDireccionApoderado.Update()
    End Sub

    Protected Sub rbtResPagApoderadoSi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtResPagApoderadoSi.CheckedChanged
        If rbtResPagApoderadoSi.Checked Then
            rbtResPagPadreSi.Checked = False
            rbtResPagPadreNo.Checked = True
            rbtResPagMadreSi.Checked = False
            rbtResPagMadreNo.Checked = True

            rbtResPagPadreSi.Enabled = False
            rbtResPagPadreNo.Enabled = False
            rbtResPagMadreSi.Enabled = False
            rbtResPagMadreNo.Enabled = False

            udpDatosFamiliares.Update()
        End If
    End Sub

    Protected Sub rbtResPagApoderadoNo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtResPagApoderadoNo.CheckedChanged
        If rbtResPagApoderadoNo.Checked Then
            rbtResPagPadreSi.Enabled = True
            rbtResPagPadreNo.Enabled = True
            rbtResPagMadreSi.Enabled = True
            rbtResPagMadreNo.Enabled = True
            udpDatosFamiliares.Update()
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Guardar()
    End Sub

    Protected Sub btnSubmitAndContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitAndContinue.Click
        Guardar()
    End Sub
#End Region

#Region "Metodos"
    Private Function ConsultarSituacionAcademicaPersona(ByVal ls_NumeroDocIdentidad As String) As Dictionary(Of String, String)
        Return mo_RepoAdmision.ConsultarSituacionAcademicaPersona(ls_NumeroDocIdentidad)
    End Function

    Private Sub HabilitarDeshabilitarControlesPersona(ByVal lb_Estado As Boolean)
        Dim lb_DatosCargados As Boolean = ViewState("datosPersonalesCargados")

        cmbTipoDocIdentidad.Enabled = Not lb_DatosCargados
        txtNroDocIdentidad.Enabled = Not lb_DatosCargados
        txtApellidoPaterno.Enabled = lb_Estado Or lb_DatosCargados
        txtApellidoMaterno.Enabled = lb_Estado Or lb_DatosCargados
        txtNombres.Enabled = lb_Estado Or lb_DatosCargados
        dtpFecNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbGenero.Enabled = lb_Estado Or lb_DatosCargados
        txtNumTelefono.Enabled = lb_Estado Or lb_DatosCargados
        txtNumCelular.Enabled = lb_Estado Or lb_DatosCargados
        cmbOperadorCelular.Enabled = lb_Estado Or lb_DatosCargados
        txtEmail.Enabled = lb_Estado Or lb_DatosCargados
        chkNoTieneCorreo.Enabled = lb_Estado Or lb_DatosCargados
        cmbPaisNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbDptoNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbPrvNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbDstNacimiento.Enabled = lb_Estado Or lb_DatosCargados
        cmbDptoActual.Enabled = lb_Estado Or lb_DatosCargados
        cmbPrvActual.Enabled = lb_Estado Or lb_DatosCargados
        cmbDstActual.Enabled = lb_Estado Or lb_DatosCargados
        txtDireccion.Enabled = lb_Estado Or lb_DatosCargados

        chkDiscFisica.Enabled = lb_Estado Or lb_DatosCargados
        chkDiscSensorial.Enabled = lb_Estado Or lb_DatosCargados
        chkDiscAuditiva.Enabled = lb_Estado Or lb_DatosCargados
        chkDiscVisual.Enabled = lb_Estado Or lb_DatosCargados
        chkDiscIntelectual.Enabled = lb_Estado Or lb_DatosCargados
        txtDiscOtra.Enabled = lb_Estado Or lb_DatosCargados

        cmbReligion.Enabled = lb_Estado Or lb_DatosCargados
        chkSacBautismo.Enabled = lb_Estado Or lb_DatosCargados
        chkSacEucaristia.Enabled = lb_Estado Or lb_DatosCargados
        chkSacConfirmacion.Enabled = lb_Estado Or lb_DatosCargados
        chkSacMatrimonio.Enabled = lb_Estado Or lb_DatosCargados
        chkSacOrdenSacerdotal.Enabled = lb_Estado Or lb_DatosCargados

        If lb_Estado Then
            LimpiarControlesPersona()
        End If

        udpDatosPersonales.Update()
    End Sub

    Private Sub LimpiarControlesPersona()
        txtApellidoPaterno.Text = ""
        txtApellidoMaterno.Text = ""
        txtNombres.Text = ""
        dtpFecNacimiento.Text = ""
        cmbGenero.SelectedValue = "-1"
        txtNumCelular.Text = ""
        txtNumTelefono.Text = ""
        txtEmail.Text = ""
        chkNoTieneCorreo.Checked = False

        cmbDptoNacimiento.SelectedValue = "-1"
        cmbPrvNacimiento.SelectedValue = "-1"
        cmbDstNacimiento.SelectedValue = "-1"
        cmbDptoActual.SelectedValue = "-1"
        cmbPrvActual.SelectedValue = "-1"
        cmbDstActual.SelectedValue = "-1"
        txtDireccion.Text = ""

        udpDatosPersonales.Update()
    End Sub

    Private Sub RefrescarGrillaCoincidencias()
        For Each _Row As GridViewRow In grwCoincidencias.Rows
            grwCoincidencias_RowDataBound(grwCoincidencias, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub CargarCombos()
        'Cargo los combos
        Dim lo_DtCicloIngreso As Data.DataTable = mo_RepoAdmision.ListarProcesoAdmisionV3()
        ClsFunciones.LlenarListas(cmbCicloIngreso, lo_DtCicloIngreso, "codigo_Cac", "descripcion_Cac", "-- Seleccione --")
        'For Each _Row As Data.DataRow In lo_DtCicloIngreso.Rows
        '    cmbCicloIngreso.SelectedValue = _Row.Item("codigo_Cac")
        '    Exit For
        'Next

        ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional(ms_CodigoTestAlt), "codigo_Cpf", "nombre_Cpf", "-- Seleccione --")
        lr_CargarComboModalidadIngreso(ms_CodigoTest, "", "")

        Dim lo_DtTipoDocIdent As Data.DataTable = mo_RepoAdmision.ListarTipoDocumentoIdentidad("PG", "")
        ClsFunciones.LlenarListas(cmbTipoDocIdentidad, lo_DtTipoDocIdent, "siglas_doci", "nombre_doci")
        cmbTipoDocIdentidad.SelectedValue = "DNI"

        lr_CargarComboPais(cmbPaisNacimiento)
        cmbPaisNacimiento.SelectedValue = ms_CodigoPaiPeru

        lr_CargarComboPais(cmbPaisInstitucion)
        cmbPaisInstitucion.SelectedValue = ms_CodigoPaiPeru

        lr_CargarComboDepartamento(cmbDptoNacimiento, ms_CodigoPaiPeru)
        cmbDptoNacimiento_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamento(cmbDptoActual, ms_CodigoPaiPeru)
        cmbDptoActual_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamento(cmbDptoInstitucion, ms_CodigoPaiPeru)
        cmbDptoInstitucion_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamento(cmbDptoPadre, ms_CodigoPaiPeru)
        cmbDptoPadre_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamento(cmbDptoMadre, ms_CodigoPaiPeru)
        cmbDptoMadre_SelectedIndexChanged(Nothing, Nothing)

        lr_CargarComboDepartamento(cmbDptoApoderado, ms_CodigoPaiPeru)
        cmbDptoApoderado_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub lr_CargarComboModalidadIngreso(ByVal ls_CodigoTest As String, ByVal ls_CodigoCac As String, ByVal ls_CodigoCpf As String)
        Dim lo_DtModalidad As New Data.DataTable
        If Not mb_SoloEdicionDatos Then
            Dim ls_Tipo As String = "77" 'Parametro tipo en procedimiento almacenado
            lo_DtModalidad = mo_RepoAdmision.ListarModalidadIngreso(ls_Tipo, ls_CodigoTest, ls_CodigoCac, ls_CodigoCpf)
        Else
            Dim ls_Tipo As String = "NV"
            lo_DtModalidad = mo_RepoAdmision.ListarModalidadIngresoPorTipo(ls_Tipo)
        End If

        ClsFunciones.LlenarListas(cmbModalidadIngreso, lo_DtModalidad, "codigo_Min", "nombre_Min", "-- Seleccione --")
        udpModalidadIngreso.Update()
    End Sub

    Private Sub ObtenerDatosPersonales(ByVal ls_Tipo As String, ByVal ls_Valor As String, Optional ByVal ls_NumDoc As String = "", Optional ByVal ls_ApePat As String = "", Optional ByVal ls_ApeMat As String = "", Optional ByVal ls_Nombre As String = "")
        Dim lo_DtRespuesta As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales(ls_Tipo, ls_Valor, ls_NumDoc, ls_ApePat, ls_ApeMat, ls_Nombre)
        If lo_DtRespuesta.Rows.Count > 0 Then
            BuscarDeudasPendientes(txtNroDocIdentidad.Text.Trim, ms_CodigoCco)

            If lo_DtRespuesta.Rows.Count = 1 Then
                Dim lo_DrPersona As Data.DataRow = lo_DtRespuesta.Rows(0)
                If Not Boolean.Parse(lo_DrPersona.Item("nrodocidentConfirmado")) Then
                    GenerarMensajeServidor("Advertencia", "0", "Se ha encontrado un interesado con el DNI: " & ls_NumDoc & ", sin embargo, el DNI aún no ha sido confirmado. Para continuar con la inscripción debe confirmar el DNI en el módulo CRM > Movimientos > Inscripción Interesado")
                    Exit Sub
                End If

                txtNroDocIdentidad.Text = lo_DrPersona.Item("nroDocIdent")
                txtApellidoPaterno.Text = lo_DrPersona.Item("apellidoPaterno")
                txtApellidoMaterno.Text = lo_DrPersona.Item("apellidoMaterno")
                txtNombres.Text = lo_DrPersona.Item("nombres")
                dtpFecNacimiento.Text = lo_DrPersona.Item("fechaNacimiento")

                If lo_DrPersona.Item("sexo").ToString.Trim <> "" Then
                    cmbGenero.SelectedValue = lo_DrPersona.Item("sexo")
                End If

                txtNumCelular.Text = lo_DrPersona.Item("telefonoCelular")
                txtNumTelefono.Text = lo_DrPersona.Item("telefonoFijo")
                txtEmail.Text = lo_DrPersona.Item("emailPrincipal")
                cmbDptoNacimiento.SelectedValue = lo_DrPersona.Item("codigoDepNac") : cmbDptoNacimiento_SelectedIndexChanged(Nothing, Nothing)
                cmbPrvNacimiento.SelectedValue = lo_DrPersona.Item("codigoProNac") : cmbPrvNacimiento_SelectedIndexChanged(Nothing, Nothing)
                cmbDstNacimiento.SelectedValue = lo_DrPersona.Item("codigoDisNac")
                cmbDptoActual.SelectedValue = lo_DrPersona.Item("codigoDep") : cmbDptoActual_SelectedIndexChanged(Nothing, Nothing)
                cmbPrvActual.SelectedValue = lo_DrPersona.Item("codigoPro") : cmbPrvActual_SelectedIndexChanged(Nothing, Nothing)
                cmbDstActual.SelectedValue = lo_DrPersona.Item("codigoDis")
                txtDireccion.Text = lo_DrPersona.Item("direccion")
                cmbDptoInstitucion.SelectedValue = lo_DrPersona.Item("codigoDepInstEdu") : cmbDptoInstitucion_SelectedIndexChanged(Nothing, Nothing)
                cmbPrvInstitucion.SelectedValue = lo_DrPersona.Item("codigoProInstEdu") : cmbPrvInstitucion_SelectedIndexChanged(Nothing, Nothing)
                cmbDstInstitucion.SelectedValue = lo_DrPersona.Item("codigoDisInstEdu") : cmbDstInstitucion_SelectedIndexChanged(Nothing, Nothing)
                cmbInstitucionEducativa.SelectedValue = IIf(cmbInstitucionEducativa.Items.FindByValue(lo_DrPersona.Item("codigoIed")) IsNot Nothing, lo_DrPersona.Item("codigoIed"), "-1") : cmbInstitucionEducativa_SelectedIndexChanged(Nothing, Nothing)

                'Datos del padre
                txtDocIdentPadre.Text = lo_DrPersona.Item("numeroDocIdentPadre")
                txtApellidoPaternoPadre.Text = lo_DrPersona.Item("apellidoPaternoPadre")
                txtApellidoMaternoPadre.Text = lo_DrPersona.Item("apellidoMaternoPadre")
                txtNombresPadre.Text = lo_DrPersona.Item("nombresPadre")
                dtpFecNacPadre.Text = lo_DrPersona.Item("fechaNacimientoPadre")
                txtNumTelPadre.Text = lo_DrPersona.Item("telefonoFijoPadre")
                txtNumCelPadre.Text = lo_DrPersona.Item("telefonoCelularPadre")
                cmbOpeCelPadre.SelectedValue = lo_DrPersona.Item("operadorCelularPadre")
                txtEmailPadre.Text = lo_DrPersona.Item("emailPadre")
                rbtResPagPadreNo.Checked = IIf(lo_DrPersona.Item("indRespPagoPadre") = "0", True, False)
                rbtResPagPadreSi.Checked = IIf(lo_DrPersona.Item("indRespPagoPadre") = "1", True, False)
                cmbDptoPadre.SelectedValue = lo_DrPersona.Item("codigoDepPadre") : cmbDptoPadre_SelectedIndexChanged(cmbDptoPadre, EventArgs.Empty)
                cmbPrvPadre.SelectedValue = lo_DrPersona.Item("codigoProPadre") : cmbPrvPadre_SelectedIndexChanged(cmbPrvPadre, EventArgs.Empty)
                cmbDstPadre.SelectedValue = lo_DrPersona.Item("codigoDisPadre")
                txtDirPadre.Text = lo_DrPersona.Item("direccionPadre")

                'Datos de la madre
                txtDocIdentMadre.Text = lo_DrPersona.Item("numeroDocIdentMadre")
                txtApellidoPaternoMadre.Text = lo_DrPersona.Item("apellidoPaternoMadre")
                txtApellidoMaternoMadre.Text = lo_DrPersona.Item("apellidoMaternoMadre")
                txtNombresMadre.Text = lo_DrPersona.Item("nombresMadre")
                dtpFecNacMadre.Text = lo_DrPersona.Item("fechaNacimientoMadre")
                txtNumTelMadre.Text = lo_DrPersona.Item("telefonoFijoMadre")
                txtNumCelMadre.Text = lo_DrPersona.Item("telefonoCelularMadre")
                cmbOpeCelMadre.SelectedValue = lo_DrPersona.Item("operadorCelularMadre")
                txtEmailMadre.Text = lo_DrPersona.Item("emailMadre")
                rbtResPagMadreNo.Checked = IIf(lo_DrPersona.Item("indRespPagoMadre") = "0", True, False)
                rbtResPagMadreSi.Checked = IIf(lo_DrPersona.Item("indRespPagoMadre") = "1", True, False)
                cmbDptoMadre.SelectedValue = lo_DrPersona.Item("codigoDepMadre") : cmbDptoMadre_SelectedIndexChanged(cmbDptoMadre, EventArgs.Empty)
                cmbPrvMadre.SelectedValue = lo_DrPersona.Item("codigoProMadre") : cmbPrvMadre_SelectedIndexChanged(cmbPrvMadre, EventArgs.Empty)
                cmbDstMadre.SelectedValue = lo_DrPersona.Item("codigoDisMadre")
                txtDirMadre.Text = lo_DrPersona.Item("direccionMadre")

                'Datos del apoderado
                txtDocIdentApoderado.Text = lo_DrPersona.Item("numeroDocIdentApod")
                txtApellidoPaternoApoderado.Text = lo_DrPersona.Item("apellidoPaternoApod")
                txtApellidoMaternoApoderado.Text = lo_DrPersona.Item("apellidoMaternoApod")
                txtNombresApoderado.Text = lo_DrPersona.Item("nombresApod")
                dtpFecNacApoderado.Text = lo_DrPersona.Item("fechaNacimientoApod")
                txtNumTelApoderado.Text = lo_DrPersona.Item("telefonoFijoApod")
                txtNumCelApoderado.Text = lo_DrPersona.Item("telefonoCelularApod")
                cmbOpeCelApoderado.SelectedValue = lo_DrPersona.Item("operadorCelularApod")
                txtEmailApoderado.Text = lo_DrPersona.Item("emailApod")
                rbtResPagApoderadoNo.Checked = IIf(lo_DrPersona.Item("indRespPagoApod") = "0", True, False)
                rbtResPagApoderadoSi.Checked = IIf(lo_DrPersona.Item("indRespPagoApod") = "1", True, False)
                cmbDptoApoderado.SelectedValue = lo_DrPersona.Item("codigoDepApod") : cmbDptoApoderado_SelectedIndexChanged(cmbDptoApoderado, EventArgs.Empty)
                cmbPrvApoderado.SelectedValue = lo_DrPersona.Item("codigoProApod") : cmbPrvApoderado_SelectedIndexChanged(cmbPrvApoderado, EventArgs.Empty)
                cmbDstApoderado.SelectedValue = lo_DrPersona.Item("codigoDisApod")
                txtDirApoderado.Text = lo_DrPersona.Item("direccionApod")

                ViewState("datosPersonalesCargados") = True
                HabilitarDeshabilitarControlesPersona(False)
                'udpDatosPersonales.Update()
                udpDatosFamiliares.Update()
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
        End If
    End Sub

    Private Sub ObtenerCalculosPension(ByVal ls_codigoCpf As String, ByVal ls_codigoIed As String, ByVal ls_codigoMin As String)
        If ls_codigoCpf <> "-1" AndAlso ls_codigoIed <> "-1" AndAlso ls_codigoMin <> "-1" Then
            rowCostos.Visible = True
            Dim lo_DtRespuesta As Data.DataTable = mo_RepoAdmision.CalcularCategoriaEstudiante(ls_codigoCpf, ls_codigoIed, ls_codigoMin)
            If lo_DtRespuesta.Rows.Count > 0 Then
                Dim lo_Row As Data.DataRow = lo_DtRespuesta.Rows(0)
                spnCostoCredito.InnerText = "S/" & Decimal.Parse(lo_Row.Item("preciocredito")).ToString("N2")
                spnCostoMes.InnerText = "S/" & Decimal.Parse(lo_Row.Item("costomes")).ToString("N2")
                spnCostoCiclo.InnerText = "S/" & Decimal.Parse(lo_Row.Item("costociclo")).ToString("N2")
                rowCostos.Attributes.Remove("data-oculto")

                ViewState("newPrecioCredito") = Decimal.Parse(lo_Row.Item("preciocredito")).ToString("N2")
            End If
        Else
            rowCostos.Visible = False
        End If
        udpCostos.Update()
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

    Private Sub btnSeleccionarPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim button As HtmlButton = DirectCast(sender, HtmlButton)
        Dim ls_DNI As String = button.Attributes("data-dni")
        ObtenerDatosPersonales("DNIU", ls_DNI, ls_DNI)
        udpDatosPersonales.Update()
    End Sub

    Protected Sub lr_CargarComboPais(ByVal lo_Combo As DropDownList)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListarPaises(), "codigo_Pai", "nombre_Pai", "-- Seleccione --")
    End Sub

    Protected Sub lr_CargarComboDepartamento(ByVal lo_Combo As DropDownList, ByVal ls_CodigoPai As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaDepartamentos(ls_CodigoPai), "codigo_Dep", "nombre_Dep", "-- Seleccione --")
    End Sub

    Protected Sub lr_CargarComboProvincia(ByVal lo_Combo As DropDownList, ByVal ls_CodigoDep As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaProvincias(ls_CodigoDep), "codigo_Pro", "nombre_Pro", "-- Seleccione --")
    End Sub

    Protected Sub lr_CargarComboDistrito(ByVal lo_Combo As DropDownList, ByVal ls_CodigoDep As String)
        ClsFunciones.LlenarListas(lo_Combo, mo_RepoAdmision.ListaDistritos(ls_CodigoDep), "codigo_Dis", "nombre_Dis", "-- Seleccione --")
    End Sub

    Protected Sub lr_CargarComboInstitucionEducativa(ByVal ls_CodigoDis As String)
        Dim lo_DsInstitucionEducativa As Data.DataTable = mo_RepoAdmision.ListarInstitucionEducativa("DIS", ls_CodigoDis)
        ClsFunciones.LlenarListas(cmbInstitucionEducativa, lo_DsInstitucionEducativa, "codigo_Ied", "nombre_Ied", "-- Seleccione --")
        lr_ConfigurarOptionsIed(lo_DsInstitucionEducativa)
    End Sub

    Protected Sub lr_CargarComboInstitucionEducativaPorId(ByVal ls_CodigoIed As String)
        Dim lo_DsInstitucionEducativa As Data.DataTable = mo_RepoAdmision.ListarInstitucionEducativaPorId(ls_CodigoIed)
        ClsFunciones.LlenarListas(cmbInstitucionEducativa, lo_DsInstitucionEducativa, "codigo_Ied", "nombre_Ied", "-- Seleccione --")
        lr_ConfigurarOptionsIed(lo_DsInstitucionEducativa)
    End Sub

    Private Sub lr_ConfigurarOptionsIed(ByVal lo_DS As Data.DataTable)
        Dim dr() As System.Data.DataRow
        For Each item As ListItem In cmbInstitucionEducativa.Items
            dr = lo_DS.Select("codigo_Ied='" & item.Value & "'")
            If dr.Length > 0 Then
                item.Attributes.Add("data-tokens", dr(0).Item("Nombre_ied").ToString)
                Dim ls_Direccion As String = ""
                If Not String.IsNullOrEmpty(dr(0).Item("Direccion_ied").ToString) Then
                    ls_Direccion = " | " & dr(0).Item("Direccion_ied").ToString
                End If
                item.Attributes.Add("data-subtext", ls_Direccion)
            End If
        Next
    End Sub

    Private Sub lr_DatosAdicionalesInstitucion(ByVal ls_CodigoIed As String)
        rbtInstPublica.Checked = False
        rbtInstPrivada.Checked = False
        If ls_CodigoIed <> "-1" Then
            Dim ls_Tipo As String = "GES"
            Dim ls_Texto As String = ""
            Dim lo_InstitucionEducativa As Data.DataRow = mo_RepoAdmision.ObtenerInstitucionEducativa(ls_Tipo, ls_Texto, ls_CodigoIed)
            Select Case lo_InstitucionEducativa.Item("Gestion_ied")
                Case "Pública"
                    rbtInstPublica.Checked = True
                    rbtInstPrivada.Checked = False
                Case "Privada"
                    rbtInstPublica.Checked = False
                    rbtInstPrivada.Checked = True
            End Select
        End If

        rbtColegioAplicacionNo.Checked = IIf(ls_CodigoIed = ms_CodigoIedAplicacion, False, True)
        rbtColegioAplicacionSi.Checked = IIf(ls_CodigoIed = ms_CodigoIedAplicacion, True, False)
    End Sub

    Private Sub AsignarValoresFormulario(ByVal ls_CodigoAlu As String)
        Try
            Dim ls_TipoConsulta As String = "C"
            Dim lo_DtInscripcion As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(ls_CodigoAlu, ls_TipoConsulta)

            If lo_DtInscripcion.Rows.Count > 0 Then
                Dim lo_Row As Data.DataRow = lo_DtInscripcion.Rows(0)
                cmbCicloIngreso.SelectedValue = lo_Row.Item("codigo_cac")
                cmbCarreraProfesional.SelectedValue = lo_Row.Item("tempcodigo_cpf") : cmbCarreraProfesional_SelectedIndexChanged(cmbCarreraProfesional, EventArgs.Empty)
                cmbModalidadIngreso.SelectedValue = lo_Row.Item("codigo_min")
                chkNivCompetencia1.Checked = lo_Row.Item("nivelacionCompetencia1")
                txtCalifNivCompetencia1.Text = Decimal.Parse(lo_Row.Item("calificacionNivelacionCompetencia1")).ToString("N2")
                chkNivCompetencia2.Checked = lo_Row.Item("nivelacionCompetencia2")
                txtCalifNivCompetencia2.Text = Decimal.Parse(lo_Row.Item("calificacionNivelacionCompetencia2")).ToString("N2")
                chkNivCompetencia3.Checked = lo_Row.Item("nivelacionCompetencia3")
                txtCalifNivCompetencia3.Text = Decimal.Parse(lo_Row.Item("calificacionNivelacionCompetencia3")).ToString("N2")
                chkNivCompetencia4.Checked = lo_Row.Item("nivelacionCompetencia4")
                txtCalifNivCompetencia4.Text = Decimal.Parse(lo_Row.Item("calificacionNivelacionCompetencia4")).ToString("N2")
                cmbTipoDocIdentidad.SelectedValue = lo_Row.Item("tipoDocIdent_Alu")
                txtNroDocIdentidad.Text = lo_Row.Item("nroDocIdent_Alu")
                txtApellidoPaterno.Text = lo_Row.Item("apellidoPat_Alu")
                txtApellidoMaterno.Text = lo_Row.Item("apellidoMat_Alu")
                txtNombres.Text = lo_Row.Item("nombres_Alu")
                dtpFecNacimiento.Text = lo_Row.Item("fechaNacimiento_Alu")

                If lo_Row.Item("sexo_Alu").ToString.Trim <> "" Then
                    cmbGenero.SelectedValue = lo_Row.Item("sexo_Alu")
                End If

                txtNumTelefono.Text = lo_Row.Item("telefonoCasa_Dal")
                txtNumCelular.Text = lo_Row.Item("telefonoMovil_Dal")
                cmbOperadorCelular.SelectedValue = lo_Row.Item("OperadorMovil_Dal")
                txtEmail.Text = lo_Row.Item("eMail_Alu")
                txtEmailAlternativo.Text = lo_Row.Item("eMail2_Alu")
                chkDiscFisica.Checked = lo_Row.Item("discapacidades_Dal").ToString.Contains("F")
                chkDiscSensorial.Checked = lo_Row.Item("discapacidades_Dal").ToString.Contains("S")
                chkDiscAuditiva.Checked = lo_Row.Item("discapacidades_Dal").ToString.Contains("A")
                chkDiscVisual.Checked = lo_Row.Item("discapacidades_Dal").ToString.Contains("V")
                chkDiscIntelectual.Checked = lo_Row.Item("discapacidades_Dal").ToString.Contains("I")
                txtDiscOtra.Text = lo_Row.Item("otraDiscapacidad_Dal")
                cmbReligion.SelectedValue = lo_Row.Item("religion_Dal")
                chkSacBautismo.Checked = lo_Row.Item("bautismo")
                chkSacEucaristia.Checked = lo_Row.Item("eucaristia")
                chkSacConfirmacion.Checked = lo_Row.Item("confirmacion")
                chkSacMatrimonio.Checked = lo_Row.Item("matrimonio")
                chkSacOrdenSacerdotal.Checked = lo_Row.Item("ordensacerdotal")
                cmbPaisNacimiento.SelectedValue = lo_Row.Item("codigo_PaiNac") : cmbPaisNacimiento_SelectedIndexChanged(cmbPaisNacimiento, EventArgs.Empty)
                cmbDptoNacimiento.SelectedValue = lo_Row.Item("codigo_DepNac") : cmbDptoNacimiento_SelectedIndexChanged(cmbDptoNacimiento, EventArgs.Empty)
                cmbPrvNacimiento.SelectedValue = lo_Row.Item("codigo_ProNac") : cmbPrvNacimiento_SelectedIndexChanged(cmbPrvNacimiento, EventArgs.Empty)
                cmbDstNacimiento.SelectedValue = lo_Row.Item("codigo_DisNac")
                cmbDptoActual.SelectedValue = lo_Row.Item("codigo_Dep") : cmbDptoActual_SelectedIndexChanged(cmbDptoActual, EventArgs.Empty)
                cmbPrvActual.SelectedValue = lo_Row.Item("codigo_Pro") : cmbPrvActual_SelectedIndexChanged(cmbPrvActual, EventArgs.Empty)
                cmbDstActual.SelectedValue = lo_Row.Item("codigo_Dis")
                txtDireccion.Text = lo_Row.Item("direccion_Pso")

                txtAnioPromocion.Text = lo_Row.Item("añoEgresoSec_Dal").ToString.Trim
                txtSeccionPromocion.Text = lo_Row.Item("seccionEgreso_Dal").ToString.Trim
                rbtMerito1ro.Checked = False : rbtMerito2do.Checked = False : rbtMeritoNinguno.Checked = False
                Select Case lo_Row.Item("ordenMerito_Dal")
                    Case "0"
                        rbtMeritoNinguno.Checked = True
                    Case "1"
                        rbtMerito1ro.Checked = True
                    Case "2"
                        rbtMerito2do.Checked = True
                End Select
                dtpFechaPrimeraMatricula.Text = lo_Row.Item("fechaPrimeraMatricula")
                cmbNivelIngles.SelectedValue = lo_Row.Item("nivelIngles_Dal")
                txtOtrosDatosIngles.Text = lo_Row.Item("otrosDatosIngles_Dal")
                cmbDptoInstitucion.SelectedValue = lo_Row.Item("codigo_DepInstEdu") : cmbDptoInstitucion_SelectedIndexChanged(cmbDptoInstitucion, EventArgs.Empty)
                cmbPrvInstitucion.SelectedValue = lo_Row.Item("codigo_ProInstEdu") : cmbPrvInstitucion_SelectedIndexChanged(cmbPrvInstitucion, EventArgs.Empty)
                cmbDstInstitucion.SelectedValue = lo_Row.Item("codigo_DisInstEdu") : cmbDstInstitucion_SelectedIndexChanged(cmbDstInstitucion, EventArgs.Empty)
                cmbInstitucionEducativa.SelectedValue = lo_Row.Item("codigo_ied") : cmbInstitucionEducativa_SelectedIndexChanged(cmbInstitucionEducativa, EventArgs.Empty)
                rbtColegioAplicacionNo.Checked = Not Boolean.Parse(lo_Row.Item("colegioAplicacion_Alu"))
                rbtColegioAplicacionSi.Checked = Boolean.Parse(lo_Row.Item("colegioAplicacion_Alu"))

                txtDocIdentPadre.Text = lo_Row.Item("numeroDocIdentPadre_fam")
                txtApellidoPaternoPadre.Text = lo_Row.Item("apellidoPaternoPadre_fam")
                txtApellidoMaternoPadre.Text = lo_Row.Item("apellidoMaternoPadre_fam")
                txtNombresPadre.Text = lo_Row.Item("nombresPadre_fam")
                dtpFecNacPadre.Text = lo_Row.Item("fechaNacimientoPadre_fam")
                txtNumTelPadre.Text = lo_Row.Item("telefonoFijoPadre_fam")
                txtNumCelPadre.Text = lo_Row.Item("telefonoCelularPadre_fam")
                cmbOpeCelPadre.SelectedValue = lo_Row.Item("operadorCelularPadre_fam")
                txtEmailPadre.Text = lo_Row.Item("emailPadre_fam")
                rbtResPagPadreNo.Checked = IIf(lo_Row.Item("indRespPagoPadre_fam") = "0", True, False)
                rbtResPagPadreSi.Checked = IIf(lo_Row.Item("indRespPagoPadre_fam") = "1", True, False)
                cmbDptoPadre.SelectedValue = lo_Row.Item("codigo_DepPadre") : cmbDptoPadre_SelectedIndexChanged(cmbDptoPadre, EventArgs.Empty)
                cmbPrvPadre.SelectedValue = lo_Row.Item("codigo_ProPadre") : cmbPrvPadre_SelectedIndexChanged(cmbPrvPadre, EventArgs.Empty)
                cmbDstPadre.SelectedValue = lo_Row.Item("codigo_DisPadre")
                txtDirPadre.Text = lo_Row.Item("direccionPadre_fam")

                txtDocIdentMadre.Text = lo_Row.Item("numeroDocIdentMadre_fam")
                txtApellidoPaternoMadre.Text = lo_Row.Item("apellidoPaternoMadre_fam")
                txtApellidoMaternoMadre.Text = lo_Row.Item("apellidoMaternoMadre_fam")
                txtNombresMadre.Text = lo_Row.Item("nombresMadre_fam")
                dtpFecNacMadre.Text = lo_Row.Item("fechaNacimientoMadre_fam")
                txtNumTelMadre.Text = lo_Row.Item("telefonoFijoMadre_fam")
                txtNumCelMadre.Text = lo_Row.Item("telefonoCelularMadre_fam")
                cmbOpeCelMadre.SelectedValue = lo_Row.Item("operadorCelularMadre_fam")
                txtEmailMadre.Text = lo_Row.Item("emailMadre_fam")
                rbtResPagMadreNo.Checked = IIf(lo_Row.Item("indRespPagoMadre_fam") = "0", True, False)
                rbtResPagMadreSi.Checked = IIf(lo_Row.Item("indRespPagoMadre_fam") = "1", True, False)
                cmbDptoMadre.SelectedValue = lo_Row.Item("codigo_DepMadre") : cmbDptoMadre_SelectedIndexChanged(cmbDptoMadre, EventArgs.Empty)
                cmbPrvMadre.SelectedValue = lo_Row.Item("codigo_ProMadre") : cmbPrvMadre_SelectedIndexChanged(cmbPrvMadre, EventArgs.Empty)
                cmbDstMadre.SelectedValue = lo_Row.Item("codigo_DisMadre")
                txtDirMadre.Text = lo_Row.Item("direccionMadre_fam")

                txtDocIdentApoderado.Text = lo_Row.Item("numeroDocIdentApod_fam")
                txtApellidoPaternoApoderado.Text = lo_Row.Item("apellidoPaternoApod_fam")
                txtApellidoMaternoApoderado.Text = lo_Row.Item("apellidoMaternoApod_fam")
                txtNombresApoderado.Text = lo_Row.Item("nombresApod_fam")
                dtpFecNacApoderado.Text = lo_Row.Item("fechaNacimientoApod_fam")
                txtNumTelApoderado.Text = lo_Row.Item("telefonoFijoApod_fam")
                txtNumCelApoderado.Text = lo_Row.Item("telefonoCelularApod_fam")
                cmbOpeCelApoderado.SelectedValue = lo_Row.Item("operadorCelularApod_fam")
                txtEmailApoderado.Text = lo_Row.Item("emailApod_fam")
                rbtResPagApoderadoNo.Checked = IIf(lo_Row.Item("indRespPagoApod_fam") = "0", True, False)
                rbtResPagApoderadoSi.Checked = IIf(lo_Row.Item("indRespPagoApod_fam") = "1", True, False)
                cmbDptoApoderado.SelectedValue = lo_Row.Item("codigo_DepApod") : cmbDptoApoderado_SelectedIndexChanged(cmbDptoApoderado, EventArgs.Empty)
                cmbPrvApoderado.SelectedValue = lo_Row.Item("codigo_ProApod") : cmbPrvApoderado_SelectedIndexChanged(cmbPrvApoderado, EventArgs.Empty)
                cmbDstApoderado.SelectedValue = lo_Row.Item("codigo_DisApod")
                txtDirApoderado.Text = lo_Row.Item("direccionApod_fam")

                'Si existen datos de apoderado muestro el collapse
                chkInfApoderado.Checked = (Not String.IsNullOrEmpty(txtDocIdentApoderado.Text) _
                                           OrElse Not String.IsNullOrEmpty(txtApellidoPaternoApoderado.Text) _
                                           OrElse Not String.IsNullOrEmpty(txtApellidoMaternoApoderado.Text) _
                                           OrElse Not String.IsNullOrEmpty(txtNombresApoderado.Text))

                ViewState("datosPersonalesCargados") = True
            End If

            VisibilidadFechaPrimeraMatricula()

            ls_TipoConsulta = "F"
            Dim lo_DtDatosAlumno As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(ls_CodigoAlu, ls_TipoConsulta)
            If lo_DtDatosAlumno.Rows.Count > 0 Then
                ViewState("origPrecioCredito") = lo_DtDatosAlumno.Rows(0).Item("precioCreditoAct_Alu")
                ViewState("origNombreCpf") = lo_DtDatosAlumno.Rows(0).Item("nombre_cpf")
                ViewState("origNombreMin") = lo_DtDatosAlumno.Rows(0).Item("nombre_min")
                ViewState("origNombreIed") = lo_DtDatosAlumno.Rows(0).Item("Nombre_ied")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Guardar()
        Try
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            If lo_Validacion.Item("rpta") = 1 Then
                mo_Cnx.IniciarTransaccion()

                Dim tipoDocIdent_Pso As String = cmbTipoDocIdentidad.SelectedValue
                Dim numeroDocIdent_Pso As String = UCase(txtNroDocIdentidad.Text.Trim)
                Dim apellidoPaterno_Pso As String = UCase(txtApellidoPaterno.Text.Trim)
                Dim apellidoMaterno_Pso As String = UCase(txtApellidoMaterno.Text.Trim)
                Dim nombres_Pso As String = UCase(txtNombres.Text.Trim)
                Dim fechaNacimiento_Pso As Date = #1/1/1900#
                If Not String.IsNullOrEmpty(dtpFecNacimiento.Text.Trim) Then
                    fechaNacimiento_Pso = Date.Parse(dtpFecNacimiento.Text.Trim)
                End If
                Dim sexo_Pso As String = cmbGenero.SelectedValue
                Dim emailPrincipal_Pso As String = UCase(txtEmail.Text.Trim)
                Dim emailAlternativo_Pso As String = UCase(txtEmailAlternativo.Text.Trim)
                Dim direccion_Pso As String = UCase(txtDireccion.Text.Trim)
                Dim codigo_Dis As String = cmbDstActual.SelectedValue
                Dim telefonoFijo_Pso As String = UCase(txtNumTelefono.Text.Trim)
                Dim telefonoCelular_Pso As String = UCase(txtNumCelular.Text.Trim)
                Dim estadoCivil_Pso As String = ""
                Dim nroRuc_Pso As String = ""
                Dim codigo_Pai As String = cmbPaisNacimiento.SelectedValue

                Dim password_Alu As String = mo_RepoAdmision.GeneraClave(apellidoPaterno_Pso, nombres_Pso)
                Dim paisNac_Dal As String = cmbPaisNacimiento.SelectedValue
                Dim distritoNac_Dal As String = cmbDstNacimiento.SelectedValue
                Dim operadorMovil_Dal As String = cmbOperadorCelular.SelectedValue

                Dim discapacidades_Dal As String = ""
                If chkDiscFisica.Checked Then
                    If Not String.IsNullOrEmpty(discapacidades_Dal.Trim) Then discapacidades_Dal += ","
                    discapacidades_Dal += "F"
                End If
                If chkDiscSensorial.Checked Then
                    If Not String.IsNullOrEmpty(discapacidades_Dal.Trim) Then discapacidades_Dal += ","
                    discapacidades_Dal += "S"
                End If
                If chkDiscAuditiva.Checked Then
                    If Not String.IsNullOrEmpty(discapacidades_Dal.Trim) Then discapacidades_Dal += ","
                    discapacidades_Dal += "A"
                End If
                If chkDiscVisual.Checked Then
                    If Not String.IsNullOrEmpty(discapacidades_Dal.Trim) Then discapacidades_Dal += ","
                    discapacidades_Dal += "V"
                End If
                If chkDiscIntelectual.Checked Then
                    If Not String.IsNullOrEmpty(discapacidades_Dal.Trim) Then discapacidades_Dal += ","
                    discapacidades_Dal += "I"
                End If
                Dim otraDiscapacidad_Dal As String = UCase(txtDiscOtra.Text.Trim)

                Dim religion_Dal As String = cmbReligion.SelectedValue
                Dim bautismo As Boolean = chkSacBautismo.Checked
                Dim eucaristia As Boolean = chkSacEucaristia.Checked
                Dim confirmacion As Boolean = chkSacConfirmacion.Checked
                Dim matrimonio As Boolean = chkSacMatrimonio.Checked
                Dim ordensacerdotal As Boolean = chkSacOrdenSacerdotal.Checked

                '===========================================================================
                'Información de institución educativa
                '===========================================================================
                Dim codigo_Ied As Object = IIf(cmbInstitucionEducativa.SelectedValue <> "-1", cmbInstitucionEducativa.SelectedValue, DBNull.Value)
                Dim anioEgresoSec_Dal As String = UCase(txtAnioPromocion.Text.Trim)
                Dim seccionEgreso_Dal As String = UCase(txtSeccionPromocion.Text.Trim)
                Dim ordenMerito_Dal As Integer = 0
                If rbtMerito1ro.Checked Then ordenMerito_Dal = 1
                If rbtMerito2do.Checked Then ordenMerito_Dal = 2
                Dim colegioAplicacion_Alu As Boolean = rbtColegioAplicacionSi.Checked

                Dim fechaPrimeraMatricula As Object = DBNull.Value
                If Not String.IsNullOrEmpty(dtpFechaPrimeraMatricula.Text.Trim) Then
                    fechaPrimeraMatricula = Date.Parse(dtpFechaPrimeraMatricula.Text.Trim)
                End If

                Dim nivelIngles As String = cmbNivelIngles.SelectedValue
                Dim otrosDatosIngles As String = UCase(txtOtrosDatosIngles.Text.Trim)

                '===========================================================================
                'Información de padre, madre y apoderado
                '===========================================================================
                Dim numeroDocIdentPadre_fam As String = UCase(txtDocIdentPadre.Text.Trim)
                Dim apellidoPaternoPadre_fam As String = UCase(txtApellidoPaternoPadre.Text.Trim)
                Dim apellidoMaternoPadre_fam As String = UCase(txtApellidoMaternoPadre.Text.Trim)
                Dim nombresPadre_fam As String = UCase(txtNombresPadre.Text.Trim)
                Dim direccionPadre_fam As String = UCase(txtDirPadre.Text.Trim)
                Dim codigoPadre_Dis As Integer = Convert.ToInt32(cmbDstPadre.SelectedValue)
                Dim telefonoFijoPadre_fam As String = txtNumTelPadre.Text.Trim
                Dim telefonoCelularPadre_fam As String = txtNumCelPadre.Text.Trim
                Dim operadorCelularPadre_fam As String = cmbOpeCelPadre.SelectedValue
                Dim emailPadre_fam As String = UCase(txtEmailPadre.Text.Trim)
                Dim fechaNacimientoPadre_fam As Object = DBNull.Value
                If Not String.IsNullOrEmpty(dtpFecNacPadre.Text.Trim) Then
                    fechaNacimientoPadre_fam = Date.Parse(dtpFecNacPadre.Text.Trim)
                End If
                Dim indRespPagoFam_Dal As Boolean = IIf(rbtResPagPadreSi.Checked, True, False)

                Dim numeroDocIdentMadre_fam As String = UCase(txtDocIdentMadre.Text.Trim)
                Dim apellidoPaternoMadre_fam As String = UCase(txtApellidoPaternoMadre.Text.Trim)
                Dim apellidoMaternoMadre_fam As String = UCase(txtApellidoMaternoMadre.Text.Trim)
                Dim nombresMadre_fam As String = UCase(txtNombresMadre.Text.Trim)
                Dim direccionMadre_fam As String = UCase(txtDirMadre.Text.Trim)
                Dim codigoMadre_Dis As Integer = Convert.ToInt32(cmbDstMadre.SelectedValue)
                Dim telefonoFijoMadre_fam As String = txtNumTelMadre.Text.Trim
                Dim telefonoCelularMadre_fam As String = txtNumCelMadre.Text.Trim
                Dim operadorCelularMadre_fam As String = cmbOpeCelMadre.SelectedValue
                Dim emailMadre_fam As String = UCase(txtEmailMadre.Text.Trim)
                Dim fechaNacimientoMadre_fam As Object = DBNull.Value
                If Not String.IsNullOrEmpty(dtpFecNacMadre.Text.Trim) Then
                    fechaNacimientoMadre_fam = Date.Parse(dtpFecNacMadre.Text.Trim)
                End If
                Dim indRespPagoMadre_fam As Boolean = IIf(rbtResPagMadreSi.Checked, True, False)

                Dim numeroDocIdentApod_fam As String = UCase(txtDocIdentApoderado.Text.Trim)
                Dim apellidoPaternoApod_fam As String = UCase(txtApellidoPaternoApoderado.Text.Trim)
                Dim apellidoMaternoApod_fam As String = UCase(txtApellidoMaternoApoderado.Text.Trim)
                Dim nombresApod_fam As String = UCase(txtNombresApoderado.Text.Trim)
                Dim direccionApod_fam As String = UCase(txtDirApoderado.Text.Trim)
                Dim codigoApod_Dis As Integer = cmbDstApoderado.SelectedValue
                Dim telefonoFijoApod_fam As String = txtNumTelApoderado.Text.Trim
                Dim telefonoCelularApod_fam As String = txtNumCelApoderado.Text.Trim
                Dim operadorCelularApod_fam As String = cmbOpeCelApoderado.SelectedValue
                Dim emailApod_fam As String = UCase(txtEmailApoderado.Text.Trim)
                Dim fechaNacimientoApod_fam As Object = DBNull.Value
                If Not String.IsNullOrEmpty(dtpFecNacApoderado.Text.Trim) Then
                    fechaNacimientoApod_fam = Date.Parse(dtpFecNacApoderado.Text.Trim)
                End If
                Dim indRespPagoApod_fam As Boolean = IIf(rbtResPagApoderadoSi.Checked, True, False)

                Dim codigo_Cco As String = ms_CodigoCco
                Dim codigo_Cac As String = cmbCicloIngreso.SelectedValue
                Dim codigo_Cpf As Object = IIf(cmbCarreraProfesional.SelectedValue <> "-1", cmbCarreraProfesional.SelectedValue, DBNull.Value)
                Dim codigo_Min As String = cmbModalidadIngreso.SelectedValue
                Dim usuario_reg As String = Request.QueryString("id")
                Dim verificado_emi As Boolean = (hddEmailVerificado.Value = "1")

                Dim lo_Salida As New Object()
                Dim lo_Resultado As New Dictionary(Of String, String)
                Dim lb_Inscrito As Boolean = True
                Dim lb_CambioPrecio As Boolean = False

                If ms_CodigoAlu = "0" Then
                    'Si es un nuevo alumno, primero tengo que realizar el proceso de inscripción
                    Dim codigo_doci As String = ""
                    Dim lo_DtTipoDocIdent As Data.DataTable = mo_RepoAdmision.ListarTipoDocumentoIdentidad("PG", "")
                    For Each _Row As Data.DataRow In lo_DtTipoDocIdent.Rows
                        If _Row.Item("siglas_doci") = tipoDocIdent_Pso Then
                            codigo_doci = _Row.Item("codigo_doci")
                            Exit For
                        End If
                    Next

                    Dim descripcion_Cac As String = cmbCicloIngreso.SelectedItem.Text.Trim
                    Dim grado_int As String = IIf(chkEgresado.Checked, "E", "Q") 'Este formulario solo es para inscripciones, por lo tanto el alumno o es egresado o va a dar examen de 5to
                    Dim codigo_Dep As String = cmbDptoActual.SelectedValue
                    Dim codigo_Pro As String = cmbPrvActual.SelectedValue
                    Dim estado_int As String = "1"
                    Dim centroLabores As String = ""
                    Dim cargoActual As String = ""
                    Dim codigo_tpar As String = "1" 'PENDIENTE!! Por ahora solo para ESTUDIANTE USAT, luego debería agregar algún combo TipoParticipante para otros casos
                    Dim numerodoc_fin As String = ""
                    Dim apepaterno_fin As String = ""
                    Dim apematerno_fin As String = ""
                    Dim nombres_fin As String = ""
                    Dim celular_fin As String = ""
                    Dim email_fin As String = ""
                    Dim valida_deuda As String = "0" 'No valido que el alumno tenga deudas
                    Dim genera_cargo As Boolean = False 'No genera cargo automáticamente
                    Dim origenInscripcion_Alu As String = "P" 'PRESENCIAL

                    lo_Salida = mo_Cnx.Ejecutar("WS_InscripcionInteresado" _
                        , ms_CodigoOri _
                        , ms_NombreEve _
                        , descripcion_Cac _
                        , codigo_doci _
                        , numeroDocIdent_Pso _
                        , apellidoPaterno_Pso _
                        , apellidoMaterno_Pso _
                        , nombres_Pso _
                        , fechaNacimiento_Pso _
                        , sexo_Pso _
                        , grado_int _
                        , codigo_Ied _
                        , codigo_Cpf _
                        , estado_int _
                        , telefonoFijo_Pso _
                        , telefonoCelular_Pso _
                        , emailPrincipal_Pso _
                        , codigo_Dep _
                        , codigo_Pro _
                        , codigo_Dis _
                        , direccion_Pso _
                        , ms_CodigoCco _
                        , codigo_Cac _
                        , codigo_Min _
                        , ms_CodigoTest _
                        , emailAlternativo_Pso _
                        , estadoCivil_Pso _
                        , password_Alu _
                        , nroRuc_Pso _
                        , centroLabores _
                        , cargoActual _
                        , codigo_tpar _
                        , numerodoc_fin _
                        , apepaterno_fin _
                        , apematerno_fin _
                        , nombres_fin _
                        , celular_fin _
                        , email_fin _
                        , valida_deuda _
                        , genera_cargo _
                        , usuario_reg _
                        , ms_Accion _
                        , verificado_emi _
                        , origenInscripcion_Alu _
                        , "0" _
                        , "" _
                        , "0" _
                    )
                Else
                    Dim ipusureg As String = Request.UserHostAddress

                    lo_Salida = mo_Cnx.Ejecutar("ADM_MantenimientoInscripcionAlumno", _
                        ms_CodigoAlu, _
                        tipoDocIdent_Pso, _
                        numeroDocIdent_Pso, _
                        apellidoPaterno_Pso, _
                        apellidoMaterno_Pso, _
                        nombres_Pso, _
                        fechaNacimiento_Pso, _
                        sexo_Pso, _
                        codigo_Ied, _
                        codigo_Cpf, _
                        telefonoFijo_Pso, _
                        telefonoCelular_Pso, _
                        operadorMovil_Dal, _
                        discapacidades_Dal, _
                        otraDiscapacidad_Dal, _
                        religion_Dal, _
                        bautismo, _
                        eucaristia, _
                        confirmacion, _
                        matrimonio, _
                        ordensacerdotal, _
                        emailPrincipal_Pso, _
                        emailAlternativo_Pso, _
                        distritoNac_Dal, _
                        codigo_Dis, _
                        direccion_Pso, _
                        anioEgresoSec_Dal, _
                        seccionEgreso_Dal, _
                        ordenMerito_Dal, _
                        colegioAplicacion_Alu, _
                        fechaPrimeraMatricula, _
                        nivelIngles, _
                        otrosDatosIngles, _
                        numeroDocIdentPadre_fam, _
                        apellidoPaternoPadre_fam, _
                        apellidoMaternoPadre_fam, _
                        nombresPadre_fam, _
                        fechaNacimientoPadre_fam, _
                        direccionPadre_fam, _
                        codigoPadre_Dis, _
                        telefonoFijoPadre_fam, _
                        telefonoCelularPadre_fam, _
                        operadorCelularPadre_fam, _
                        emailPadre_fam, _
                        indRespPagoFam_Dal, _
                        numeroDocIdentMadre_fam, _
                        apellidoPaternoMadre_fam, _
                        apellidoMaternoMadre_fam, _
                        nombresMadre_fam, _
                        fechaNacimientoMadre_fam, _
                        direccionMadre_fam, _
                        codigoMadre_Dis, _
                        telefonoFijoMadre_fam, _
                        telefonoCelularMadre_fam, _
                        operadorCelularMadre_fam, _
                        emailMadre_fam, _
                        indRespPagoMadre_fam, _
                        numeroDocIdentApod_fam, _
                        apellidoPaternoApod_fam, _
                        apellidoMaternoApod_fam, _
                        nombresApod_fam, _
                        fechaNacimientoApod_fam, _
                        direccionApod_fam, _
                        codigoApod_Dis, _
                        telefonoFijoApod_fam, _
                        telefonoCelularApod_fam, _
                        operadorCelularApod_fam, _
                        emailApod_fam, _
                        indRespPagoApod_fam, _
                        codigo_Min, _
                        mb_SoloEdicionDatos, _
                        ipusureg, _
                        usuario_reg, _
                        "0", _
                        "", _
                        "0")

                    If ViewState("editando") AndAlso lo_Salida(0) = "1" AndAlso ViewState("newPrecioCredito") <> ViewState("origPrecioCredito") Then
                        lb_CambioPrecio = True
                    End If

                    If lo_Salida(0) = "1" AndAlso Not ViewState("editando") Then
                        lr_ProcesoMarketing("INSCRIPCIÓN") '19/07/2019: Se envían los datos de registro al script de marketing
                    End If
                End If

                lo_Resultado.Add("rpta", lo_Salida(0))
                lo_Resultado.Add("msg", lo_Salida(1))
                ViewState("codigo_alu") = lo_Salida(2)

                mo_Cnx.TerminarTransaccion()

                If lb_CambioPrecio Then
                    'Consulto en base de datos para certificar si se ha realizado un cambio de precio crédito
                    Dim codigoAlu As Integer = lo_Salida(2)
                    Dim lo_DtResutado As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(codigoAlu, "F")

                    If lo_DtResutado.Rows.Count > 0 Then
                        Dim precioCredito As Decimal = lo_DtResutado.Rows(0).Item("precioCreditoAct_Alu")
                        If precioCredito <> ViewState("origPrecioCredito") Then
                            EnviarCorreoPensiones(ViewState("codigo_alu"))
                        End If
                    End If
                End If

                Dim lo_DtPersona As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales("DNIU", numeroDocIdent_Pso, numeroDocIdent_Pso)
                If lo_DtPersona.Rows.Count > 0 Then
                    divMdlMenServParametros.Attributes.Item("data-codigo-pso") = lo_DtPersona.Rows(0).Item("codigoPso")
                End If
                divMdlMenServParametros.Attributes.Item("data-codigo-alu") = ViewState("codigo_alu")

                GenerarMensajeServidor("Respuesta", lo_Resultado.Item("rpta"), lo_Resultado.Item("msg"))
            Else
                GenerarMensajeServidor("Advertencia", lo_Validacion.Item("rpta"), lo_Validacion.Item("msg"), lo_Validacion.Item("control"))
            End If
        Catch ex As Exception
            mo_Cnx.AbortarTransaccion()
            lblErrorMessage.Text = ex.Message
            GenerarMensajeServidor("Error", "-1", "Ha ocurrido un error en el servidor")
        End Try
    End Sub

    Private Sub EnviarCorreoPensiones(ByVal codigoAlu As Integer)
        Dim tipoConsulta As String = "CO"
        Dim dt As Data.DataTable = mo_RepoAdmision.ConsultarDatosAlumno(tipoConsulta, codigoAlu)

        Dim tipo As String = "TO"
        Dim codigoPer As String = Request.QueryString("id")
        Dim usuario As String = ""
        Dim correoUsuario As String = ""
        Dim correoPensiones As String = ""

        Dim dtDatosUsuario As Data.DataTable = mo_RepoAdmision.ConsultarDatosUsuario(tipo, codigoPer)
        If dtDatosUsuario.Rows.Count > 0 Then
            usuario = dtDatosUsuario.Rows(0).Item("usuario_per")
            correoUsuario = dtDatosUsuario.Rows(0).Item("email_Per")
        End If

        Dim dtDatosPensiones As Data.DataTable = mo_RepoAdmision.ConsultarDatosUsuario(tipo, ClsAdmision.CodigoJefaPensiones)
        If dtDatosPensiones.Rows.Count > 0 Then
            correoPensiones = dtDatosPensiones.Rows(0).Item("email_Per")
        End If

        If dt.Rows.Count > 0 Then
            Dim codigoUniversitario As String = dt.Rows(0).Item("codigoUniver_Alu")
            Dim postulante As String = dt.Rows(0).Item("alumno")

            Dim mensaje As String = "<font face='Trebuchet MS'>"
            mensaje &= "<div style=""font-size: 14px;"">"
            mensaje &= "<p>Estimado(a):</p>"
            mensaje &= "<p>Se ha generado un cambio en el precio crédito del postulante " & postulante & " con código " & codigoUniversitario
            mensaje &= "<ul>"
            mensaje &= "<li>S/." & ViewState("origPrecioCredito") & " a S/." & ViewState("newPrecioCredito") & "</li>"
            mensaje &= "</ul>"
            mensaje &= "Este cambio se ha realizado desde el módulo de: GESTIÓN DE ADMISIÓN Y ESCUELA PRE > Movimientos > Inscripción Pregrado Regular > Editar"

            If Not String.IsNullOrEmpty(usuario) Then
                mensaje &= "<p><b>Usuario:</b> " & usuario & "</p>"
            End If
            mensaje &= "<table style=""font-family: Calibri, Helvetica, Arial, sans-serif; font-size: 14px; border-collapse: collapse;"">"
            mensaje &= "<tr>"
            mensaje &= "<th colspan=""2"" style=""background-color: #C9D583"">Datos originales</th>"
            mensaje &= "<tr/>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Carrera: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & ViewState("origNombreCpf") & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Modalidad: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & ViewState("origNombreMin") & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Colegio: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & ViewState("origNombreIed") & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "</table>"

            mensaje &= "<br>"

            mensaje &= "<table style=""min-width = 500px; font-family: Calibri, Helvetica, Arial, sans-serif; font-size: 14px; border-collapse: collapse;"">"
            mensaje &= "<tr>"
            mensaje &= "<th colspan=""2"" style=""background-color: #56D1CA"">Datos actuales</th>"
            mensaje &= "<tr/>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Carrera: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & cmbCarreraProfesional.SelectedItem.Text & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Modalidad: </th>"
            mensaje &= "<td style=""min-width: 350px;"">&nbsp;&nbsp;" & cmbModalidadIngreso.SelectedItem.Text & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "<tr>"
            mensaje &= "<th style=""text-align: right"">Colegio: </th>"
            mensaje &= "<td style=""min-width: 200px;"">&nbsp;&nbsp;" & cmbInstitucionEducativa.SelectedItem.Text & "&nbsp;&nbsp;</td>"
            mensaje &= "</tr>"
            mensaje &= "</table>"

            mensaje &= "<p>Atentamente.<br>Campus Virtual USAT</p>"
            mensaje &= "</div>"
            mensaje &= "</font>"

            mo_Cnx.AbrirConexion()
            dt = mo_Cnx.TraerDataTable("InsertaEnvioCorreosMasivo", ClsAdmision.CodigoJefaPensiones, "Cambio de precio crédito", correoPensiones, mensaje, 32, ClsAdmision.CorreoServiciosTI, "")
            dt = mo_Cnx.TraerDataTable("InsertaEnvioCorreosMasivo", codigoPer, "Cambio de precio crédito", correoUsuario, mensaje, 32, ClsAdmision.CorreoServiciosTI, "")
            mo_Cnx.CerrarConexion()

            'TODAVÍA NO EXISTE EN PROD
            'me_EnvioCorreosMasivo = md_EnvioCorreosMasivo.GetEnvioCorreosMasivo(0)
            'With me_EnvioCorreosMasivo
            '    .operacion = "I"
            '    .cod_user = Request.QueryString("id")
            '    .tipoCodigoEnvio_ecm = "codigo_per"
            '    .codigoEnvio_ecm = ClsAdmision.JefaPensiones
            '    .codigo_apl = 32 'ADMISIÓN Y ESC PRE
            '    .correo_destino = ClsAdmision.CorreoJefaPensiones
            '    .correo_respuesta = ClsAdmision.CorreoServiciosTI
            '    .asunto = "Cambio de precio crédito"
            '    .cuerpo = mensaje
            'End With
            'dt = md_EnvioCorreosMasivo.RegistrarEnvioCorreosMasivo(me_EnvioCorreosMasivo) 
        End If
    End Sub

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ls_Rpta As String, ByVal ls_Msg As String, Optional ByVal ls_Control As String = "")
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        With respuestaPostback.Attributes
            .Item("data-rpta") = ls_Rpta
            .Item("data-msg") = ls_Msg
            .Item("data-control") = ls_Control
        End With
        udpMensajeServidorBody.Update()
    End Sub

    Private Function ValidarDNI() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        Dim lb_Errores As Boolean = False

        If String.IsNullOrEmpty(txtNroDocIdentidad.Text.Trim) Then
            If Not lb_Errores Then
                lo_Validacion.Item("rpta") = 0
                lo_Validacion.Item("msg") = "Debe asignar un número de identidad"
                lo_Validacion.Item("control") = "txtDNI"
                lb_Errores = True
            End If
            txtNroDocIdentidad.Attributes.Item("data-error") = "true"
        Else
            txtNroDocIdentidad.Attributes.Item("data-error") = "false"
        End If

        If cmbTipoDocIdentidad.SelectedValue = "DNI" Then
            Dim ls_FormatDNI As String = "^\d{8}$"
            If Not Regex.IsMatch(txtNroDocIdentidad.Text.Trim, ls_FormatDNI) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "El DNI ingresado no es correcto"
                    lo_Validacion.Item("control") = "txtDNI"
                    lb_Errores = True
                End If
                txtNroDocIdentidad.Attributes.Item("data-error") = "true"
            Else
                txtNroDocIdentidad.Attributes.Item("data-error") = "false"
            End If
        End If

        Return lo_Validacion
    End Function

    Private Function ValidarFormulario() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        If Not mb_SoloEdicionDatos AndAlso cmbCarreraProfesional.SelectedValue = "-1" Then 'No se valida en edición de datos, ya que hay alumnos antiguos que no tienen codigo_cpf
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe seleccionar una carrera profesional"
            lo_Validacion.Item("control") = "cmbCarreraProfesional"
            Return lo_Validacion
        End If

        If String.IsNullOrEmpty(txtNroDocIdentidad.Text.Trim) Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe asignar un número de DNI"
            lo_Validacion.Item("control") = "txtNroDocIdentidad"
            Return lo_Validacion
        End If

        If cmbModalidadIngreso.SelectedValue = "-1" Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "Debe seleccionar una modalidad de ingreso"
            lo_Validacion.Item("control") = "cmbModalidadIngreso"
            Return lo_Validacion
        End If

        'Datos Personales

        ''Datos del Padre
        'Dim lb_DniPadre As Boolean = Not String.IsNullOrEmpty(txtDocIdentPadre.Text.Trim)
        'If lb_DniPadre Then
        '    If cmbDstPadre.SelectedValue = "-1" Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Para registrar el DNI del padre debe indicar el seleccionar su distrito de nacimiento"
        '        lo_Validacion.Item("control") = "cmbModalidadIngreso"
        '        Return lo_Validacion
        '    End If
        'End If

        ''Datos de la madre
        'Dim lb_DniMadre As Boolean = Not String.IsNullOrEmpty(txtDocIdentMadre.Text.Trim)
        'If Not lb_DniMadre Then
        '    If Not String.IsNullOrEmpty(txtApellidoPaternoMadre.Text.Trim) _
        '        OrElse Not String.IsNullOrEmpty(txtApellidoMaternoMadre.Text.Trim) _
        '        OrElse Not String.IsNullOrEmpty(txtNombresMadre.Text.Trim) _
        '        OrElse cmbDstMadre.SelectedValue <> "-1" Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Si va a registrar los datos de la madre, debe ingresar su DNI"
        '        lo_Validacion.Item("control") = "txtDocIdentMadre"
        '        Return lo_Validacion
        '    End If
        'End If

        ''Datos del apoderado
        'Dim lb_DniApoderado As Boolean = Not String.IsNullOrEmpty(txtDocIdentApoderado.Text.Trim)
        'If Not lb_DniApoderado Then
        '    If Not String.IsNullOrEmpty(txtApellidoPaternoApoderado.Text.Trim) _
        '        OrElse Not String.IsNullOrEmpty(txtApellidoMaternoApoderado.Text.Trim) _
        '        OrElse Not String.IsNullOrEmpty(txtNombresApoderado.Text.Trim) _
        '        OrElse cmbDstApoderado.SelectedValue <> "-1" Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Si va a registrar los datos del apoderado, debe ingresar su DNI"
        '        lo_Validacion.Item("control") = "txtDocIdentApoderado"
        '        Return lo_Validacion
        '    End If
        'Else
        '    If String.IsNullOrEmpty(txtApellidoPaternoApoderado.Text.Trim) Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "No ha ingresado el apellido paterno del Apoderado"
        '        lo_Validacion.Item("control") = "txtApellidoPaternoApoderado"
        '        Return lo_Validacion
        '    End If

        '    If String.IsNullOrEmpty(txtApellidoMaternoApoderado.Text.Trim) Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "No ha ingresado el apellido materno del Apoderado"
        '        lo_Validacion.Item("control") = "txtApellidoMaternoApoderado"
        '        Return lo_Validacion
        '    End If

        '    If String.IsNullOrEmpty(txtNombresApoderado.Text.Trim) Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "No ha ingresado los nombres de Apoderado"
        '        lo_Validacion.Item("control") = "txtNombres"
        '        Return lo_Validacion
        '    End If

        '    If cmbDstApoderado.SelectedValue = "-1" Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Si va a registrar los datos del apoderado, debe ingresar su distrito"
        '        lo_Validacion.Item("control") = "cmbDstApoderado"
        '        Return lo_Validacion
        '    End If
        'End If

        If Not String.IsNullOrEmpty(txtDocIdentApoderado.Text.Trim) AndAlso txtDocIdentApoderado.Text.Trim = txtDocIdentPadre.Text.Trim Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "El DNI del apoderado no puede ser el mismo que el DNI del padre"
            lo_Validacion.Item("control") = "txtDocIdentApoderado"
            Return lo_Validacion
        End If

        If Not String.IsNullOrEmpty(txtDocIdentApoderado.Text.Trim) AndAlso txtDocIdentApoderado.Text.Trim = txtDocIdentMadre.Text.Trim Then
            lo_Validacion.Item("rpta") = 0
            lo_Validacion.Item("msg") = "El DNI del apoderado no puede ser el mismo que el DNI de la madre"
            lo_Validacion.Item("control") = "txtDocIdentApoderado"
        End If

        Return lo_Validacion
    End Function

    Private Sub lr_ProcesoMarketing(ByVal ls_Tipo As String)
        Try
            Using _client As New Net.WebClient
                Dim lo_Credentials As New NetworkCredential("marketing", "USAT2015")
                _client.Credentials = lo_Credentials
                Dim lo_ReqParam As New Specialized.NameValueCollection
                lo_ReqParam.Add("dniApoderado", txtDocIdentApoderado.Text.Trim)
                lo_ReqParam.Add("apePatApoderado", txtApellidoPaternoApoderado.Text.Trim)
                lo_ReqParam.Add("apeMatApoderado", txtApellidoMaternoApoderado.Text.Trim)
                lo_ReqParam.Add("nombresApoderado", txtNombresApoderado.Text.Trim)
                lo_ReqParam.Add("numCelApoderado", txtNumCelApoderado.Text.Trim)
                lo_ReqParam.Add("emailApoderado", txtEmailApoderado.Text.Trim)
                lo_ReqParam.Add("dni", txtNroDocIdentidad.Text.Trim)
                lo_ReqParam.Add("apellidoPaterno", txtApellidoPaterno.Text.Trim)
                lo_ReqParam.Add("apellidoMaterno", txtApellidoMaterno.Text.Trim)
                lo_ReqParam.Add("nombres", txtNombres.Text.Trim)
                lo_ReqParam.Add("numCelular", txtNumCelular.Text.Trim)
                lo_ReqParam.Add("numFijo", txtNumTelefono.Text.Trim)
                lo_ReqParam.Add("email", txtEmail.Text.Trim)
                lo_ReqParam.Add("direccion", txtDireccion.Text.Trim)
                lo_ReqParam.Add("departamento", cmbDptoActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("provincia", cmbPrvActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("distrito", cmbDstActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("fecNacimiento", dtpFecNacimiento.Text.Trim)
                lo_ReqParam.Add("sexo", cmbGenero.SelectedItem.Text.Trim)
                lo_ReqParam.Add("anioEstudio", IIf(chkEgresado.Checked, "E", "Q"))
                lo_ReqParam.Add("centroLabores", "")
                lo_ReqParam.Add("cargo", "")
                lo_ReqParam.Add("ruc", "")
                lo_ReqParam.Add("departamentoInstEduc", cmbDptoInstitucion.SelectedItem.Text.Trim)
                lo_ReqParam.Add("institucionEducativa", cmbInstitucionEducativa.SelectedItem.Text.Trim)
                lo_ReqParam.Add("carreraProfesional", cmbCarreraProfesional.SelectedItem.Text.Trim)
                lo_ReqParam.Add("consultas", "")
                lo_ReqParam.Add("tipo", ls_Tipo)
                lo_ReqParam.Add("campoAdicional1", "")
                lo_ReqParam.Add("campoAdicional2", "")
                lo_ReqParam.Add("campoAdicional3", "INSCRIPCIÓN")
                lo_ReqParam.Add("campoAdicional4", "PRESENCIAL")
                lo_ReqParam.Add("campoAdicional5", "OFICINA INFORMES") 'Origen CRM, a petición de Salvador
                lo_ReqParam.Add("campoAdicional6", "")
                Dim lo_ResponseBytes As Byte() = _client.UploadValues("http://www.tuproyectodevida.pe/autorespuesta/auto_prueba.php", "POST", lo_ReqParam)
                Dim lo_ResponseBody As String = (New Text.UTF8Encoding).GetString(lo_ResponseBytes)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BuscarDeudasPendientes(ByVal ls_DNI As String, ByVal ls_CodigoCco As String)
        Dim ls_CodigoPso As String = ""
        Dim lo_DtDeudas As New Data.DataTable

        Dim lo_DtPersona As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales("DNIE", ls_DNI, ls_DNI)
        If lo_DtPersona.Rows.Count > 0 Then
            ls_CodigoPso = lo_DtPersona.Rows(0).Item("codigoPso")

            If Not String.IsNullOrEmpty(ls_CodigoPso) Then
                'andy.diaz  04/07/2019  Ahora muestro siempre las deudas pendientes, vigentes o vencidas
                'lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorPersonaCentroCosto(ls_CodigoPso, ls_CodigoCco)
                'If lo_DtDeudas.Rows.Count = 0 Then
                '    Dim ln_codigoTest As Integer = 1 'PREGRADO
                '    lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorPersonaTipoEstudio(ls_CodigoPso, ln_codigoTest)
                'End If

                Dim ln_codigoTest As Integer = 1 'PREGRADO
                lo_DtDeudas = mo_RepoAdmision.ConsultarDeudasPorPersonaTipoEstudio(ls_CodigoPso, ln_codigoTest)
            End If
        End If

        If lo_DtDeudas.Rows.Count > 0 Then
            divDeudas.Attributes.Item("data-mostrar") = "true"
        Else
            divDeudas.Attributes.Item("data-mostrar") = "false"
        End If
        grwDeudas.DataSource = lo_DtDeudas
        grwDeudas.DataBind()

        udpDeudas.Update()
    End Sub

    Private Sub VisibilidadFechaPrimeraMatricula()
        If cmbModalidadIngreso.SelectedValue = "2" Then 'TRASLADO EXTERNO
            divFechaPrimeraMatricula.Visible = True
        Else
            divFechaPrimeraMatricula.Visible = False
            dtpFechaPrimeraMatricula.Text = ""
        End If
        udpFechaPrimeraMatricula.Update()
    End Sub
#End Region
    
End Class
