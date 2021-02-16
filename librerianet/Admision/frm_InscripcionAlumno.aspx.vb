Imports System.Xml
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Imports System.Net

Partial Class frm_InscripcionAlumno
    Inherits System.Web.UI.Page

#Region "Variables globales"
    Private ReadOnly mo_SOAP As New ClsAdmisionSOAP
    Private mo_VariablesGlobales As New Dictionary(Of String, String)
    Private serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()

    Private ReadOnly mn_CodigoPaiPeru As Integer = 156
    Private ReadOnly mn_CodigoDepLambayeque As Integer = 13
    Private ReadOnly mn_CodigoDepNoDefinido As Integer = 26
    Private ReadOnly mn_CodigoProNoDefinido As Integer = 1
    Private ReadOnly mn_CodigoDisNoDefinido As Integer = 1

    Private ReadOnly mn_CodigoTest As Integer = 1 'ESCUELA PRE
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LimpiarVariablesSession()

                Dim encodedModalidades As String = IIf(Request.Form("modalidades") IsNot Nothing, Request.Form("modalidades"), "")
                Dim decodedModalides As String = ""
                If Not String.IsNullOrEmpty(encodedModalidades) Then
                    decodedModalides = DesencriptaTexto(encodedModalidades)
                End If
                ViewState("modalidades") = decodedModalides
                ViewState("nombreEve") = IIf(Request.Form("nombreEve") IsNot Nothing, Request.Form("nombreEve"), "")
                ViewState("descripcionCac") = IIf(Request.Form("descripcionCac") IsNot Nothing, Request.Form("descripcionCac"), "")
                ViewState("incluirCodigosCpf") = IIf(Request.Form("incluirCodigosCpf") IsNot Nothing, Request.Form("incluirCodigosCpf"), "")
                ViewState("excluirCodigosCpf") = IIf(Request.Form("excluirCodigosCpf") IsNot Nothing, Request.Form("excluirCodigosCpf"), "")

                Dim encodedMsgExitoInscripcion As String = IIf(Request.Form("msgExitoInscripcion") IsNot Nothing, Request.Form("msgExitoInscripcion"), "")
                Dim decodedMsgExitoInscripcion As String = ""
                If Not String.IsNullOrEmpty(encodedMsgExitoInscripcion) Then
                    decodedMsgExitoInscripcion = DesencriptaTexto(encodedMsgExitoInscripcion)
                End If
                ViewState("msgExitoInscripcion") = decodedMsgExitoInscripcion

                rbtNacionalidadPeruana.Checked = True
                rbtNacionalidadPeruana.Attributes.Item("data-checked") = rbtNacionalidadPeruana.Checked.ToString.ToLower
                cmbPaisNacimiento.Enabled = Not rbtNacionalidadPeruana.Checked
                ViewState("nacionalidadPeruana") = rbtNacionalidadPeruana.Checked

                hddEmailCoincidente.Value = "0"
                hddEmailVerificado.Value = "0"

                divContainerModalidades.Visible = False
                txtOtroMerito.Visible = False

                hddPathFicha.Value = ObtenerVariableGlobal("fichaInscripcionUrl")

                txtToken.Enabled = False
                txtCelularToken.Enabled = False

                CargarCombos()
                MostrarOcultarOrdenMerito()
            End If

            LimpiarMensajeServidor()
            LimpiarMensajeToastr()
            LimpiarMensajeConfirmacion()
        Catch ex As Exception
            Response.Write(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub MostrarTiempoRestantePago()
        Dim fechaLimitePagoInscripcion As DateTime = DateTime.Parse(ViewState("fechaHoraLimitePago"))
        Dim diasRestantes As Integer = DateDiff(DateInterval.Day, Date.Now(), fechaLimitePagoInscripcion)
        Dim horasRestantes As Integer = DateDiff(DateInterval.Hour, Date.Now(), fechaLimitePagoInscripcion)
        Dim minutosRestantes As Integer = DateDiff(DateInterval.Minute, Date.Now(), fechaLimitePagoInscripcion)

        If diasRestantes > 1 Then
            spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence en 48 horas</u>, caso contrario tendrás que volver a inscribirte. Ten en cuenta que si no realizas el pago hasta un día antes del examen, no podrás rendir la evaluación"
            Exit Sub
        Else
            spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence el " & fechaLimitePagoInscripcion & "</u>, caso contrario tendrás que volver a inscribirte. Ten en cuenta que si no realizas el pago hasta un día antes del examen, no podrás rendir la evaluación"
            Exit Sub
        End If

        'If diasRestantes > 1 Then
        '    spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence en 48 horas</u>, caso contrario tendrás que volver a inscribirte. Ten en cuenta que si no realizas el pago hasta un día antes del examen, no podrás rendir la evaluación"
        '    Exit Sub
        'End If

        'If horasRestantes > 1 Then
        '    spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence en " & horasRestantes & " horas</u>, caso contrario tendrás que volver a inscribirte. Ten en cuenta que si no realizas el pago hasta un día antes del examen, no podrás rendir la evaluación"
        '    Exit Sub
        'End If

        'If minutosRestantes > 1 Then
        '    spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence en " & minutosRestantes & " minutos</u>, caso contrario tendrás que volver a inscribirte. Ten en cuenta que si no realizas el pago hasta un día antes del examen, no podrás rendir la evaluación"
        '    Exit Sub
        'End If

        'If diasRestantes > 1 OrElse horasRestantes > 1 OrElse minutosRestantes > 1 Then
        '    spnMensajeTiempoPagoRegular.InnerHtml = "El plazo para realizar el pago <u>vence en 48 horas</u>, pero <u>si si la inscripción lo haces en el último día y hora de inscripción, el plazo para pagar es de una hora</u>. Cabe resaltar, que si no realizas el pago respectivo no podrás participar de las capacitaciones y tampoco podrás rendir el examen."
        '    Exit Sub
        'End If

        spnMensajeTiempoPagoRegular.InnerHtml = "Se ha vencido el plazo para realizar el pago"
        Exit Sub
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        divContainerModalidades.Visible = (cmbCarreraProfesional.SelectedValue <> "-1")
        MostrarOcultarModalidades(cmbCarreraProfesional.SelectedValue)
        udpModalidades.Update()
    End Sub

    Protected Sub btnModExamenAdmision_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModExamenAdmision.Click
        AlmacenarDatosModalidad("examenAdmision")
    End Sub

    Protected Sub btnModTestDahc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModTestDahc.Click
        AlmacenarDatosModalidad("testDahc")
    End Sub

    Protected Sub btnModEscuelaPreUsat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModEscuelaPreUsat.Click
        AlmacenarDatosModalidad("escuelaPre")
    End Sub

    Protected Sub btnModPrimerosPuestos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModPrimerosPuestos.Click
        AlmacenarDatosModalidad("primerosPuestos")
    End Sub

    Protected Sub btnModTalentoMedicina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModTalentoMedicina.Click
        AlmacenarDatosModalidad("talentoMedicina")
    End Sub

    Protected Sub btnFakeNacionalidadPeruana_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFakeNacionalidadPeruana.Click
        ViewState("nacionalidadPeruana") = True
        CargarComboDepartamento(cmbDepNacimiento, Not ViewState("nacionalidadPeruana"), "DEPARTAMENTO")
        cmbDepNacimiento_SelectedIndexChanged(Nothing, Nothing)
        udpDepNacimiento.Update()

        divContainerNacimiento.Visible = ViewState("nacionalidadPeruana")
        udpContainerNacimiento.Update()

        cmbPaisNacimiento.SelectedValue = "-1"
        udpPaisNacimiento.Update()
    End Sub

    Protected Sub btnFakeNacionalidadExtranjera_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFakeNacionalidadExtranjera.Click
        ViewState("nacionalidadPeruana") = False
        CargarComboDepartamento(cmbDepNacimiento, Not ViewState("nacionalidadPeruana"), "DEPARTAMENTO")
        cmbDepNacimiento_SelectedIndexChanged(Nothing, Nothing)
        udpDepNacimiento.Update()

        divContainerNacimiento.Visible = ViewState("nacionalidadPeruana")
        udpContainerNacimiento.Update()
    End Sub

    Protected Sub txtNroDocIdentidad_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNroDocIdentidad.TextChanged
        Dim numeroDocIdentidad As String = txtNroDocIdentidad.Text.Trim
        If numeroDocIdentidad = "" Then Exit Sub

        Dim rpta As Integer = ConsultarDeudasPersona(numeroDocIdentidad)

        If rpta = 1 Then
            rpta = ConsultarSituacionAcademicaPersona(numeroDocIdentidad)
            If rpta = 1 Then
                rpta = ConsultarInscripcionPersona(numeroDocIdentidad)
                If rpta = 1 Then 'andy.diaz 10/09/2020
                    'Verifico si ya tiene inscripción preferente
                    If ViewState("validarPrimeraInscripcionPreferente") Then
                        rpta = ConsultarInscripcionPreferentePersona(numeroDocIdentidad)
                    End If
                End If '\andy.diaz 10/09/2020
            End If
        End If

        udpNroDocIdentidad.Update()
    End Sub

    Protected Sub cmbDepNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepNacimiento.SelectedIndexChanged
        Dim ln_CodigoDep As Integer = cmbDepNacimiento.SelectedValue
        Dim lb_NoDefinido As Boolean = Not ViewState("nacionalidadPeruana")

        CargarComboProvincia(cmbProNacimiento, ln_CodigoDep, lb_NoDefinido, "PROVINCIA")
        cmbProNacimiento_SelectedIndexChanged(Nothing, Nothing)
        udpProNacimiento.Update()
    End Sub

    Protected Sub cmbProNacimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProNacimiento.SelectedIndexChanged
        Dim ln_CodigoPro As Integer = cmbProNacimiento.SelectedValue
        Dim lb_NoDefinido As Boolean = Not ViewState("nacionalidadPeruana")

        CargarComboDistrito(cmbDisNacimiento, cmbProNacimiento.SelectedValue, lb_NoDefinido, "DISTRITO")
        udpDisNacimiento.Update()
    End Sub

    Protected Sub cmbDepActual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepActual.SelectedIndexChanged
        Dim ln_CodigoDep As Integer = cmbDepActual.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboProvincia(cmbProActual, ln_CodigoDep, lb_NoDefinido, "PROVINCIA")
        cmbProActual_SelectedIndexChanged(Nothing, Nothing)
        udpProActual.Update()
    End Sub

    Protected Sub cmbProActual_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProActual.SelectedIndexChanged
        Dim ln_CodigoPro As Integer = cmbProActual.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboDistrito(cmbDisActual, ln_CodigoPro, lb_NoDefinido, "DISTRITO")
        udpDisActual.Update()
    End Sub

    Protected Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        lr_BuscarEmailCoincidente(txtEmail.Text.Trim)
    End Sub

    Protected Sub cmbDepInstitucionEducativa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepInstitucionEducativa.SelectedIndexChanged
        Dim ls_Tipo As String = "DEP"
        Dim ln_CodigoDep As Integer = cmbDepInstitucionEducativa.SelectedValue
        CargarComboInstitucionEducativa(cmbInstitucionEducativa, ls_Tipo, ln_CodigoDep, "NOMBRE DEL COLEGIO")
        udpInstitucionEducativa.Update()
    End Sub

    Protected Sub cmbCondicionEstudiante_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCondicionEstudiante.SelectedIndexChanged
        MostrarOcultarOrdenMerito()
    End Sub

    Protected Sub cmbOrdenMerito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOrdenMerito.SelectedIndexChanged
        MostrarOcultarMensajePuesto()
    End Sub

    Protected Sub cmbDepPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepPadre.SelectedIndexChanged
        Dim ln_CodigoDep As Integer = cmbDepPadre.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboProvincia(cmbProPadre, ln_CodigoDep, lb_NoDefinido, "PROVINCIA")
        cmbProPadre_SelectedIndexChanged(Nothing, Nothing)
        udpProPadre.Update()
    End Sub

    Protected Sub cmbProPadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProPadre.SelectedIndexChanged
        Dim ln_CodigoPro As Integer = cmbProPadre.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboDistrito(cmbDisPadre, ln_CodigoPro, lb_NoDefinido, "DISTRITO")
        udpDisPadre.Update()
    End Sub

    Protected Sub cmbDepMadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepMadre.SelectedIndexChanged
        Dim ln_CodigoDep As Integer = cmbDepMadre.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboProvincia(cmbProMadre, ln_CodigoDep, lb_NoDefinido, "PROVINCIA")
        cmbProMadre_SelectedIndexChanged(Nothing, Nothing)
        udpProMadre.Update()
    End Sub

    Protected Sub cmbProMadre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProMadre.SelectedIndexChanged
        Dim ln_CodigoPro As Integer = cmbProMadre.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboDistrito(cmbDisMadre, ln_CodigoPro, lb_NoDefinido, "DISTRITO")
        udpDisMadre.Update()
    End Sub

    Protected Sub cmbDepApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepApoderado.SelectedIndexChanged
        Dim ln_CodigoDep As Integer = cmbDepApoderado.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboProvincia(cmbProApoderado, ln_CodigoDep, lb_NoDefinido, "PROVINCIA")
        cmbProApoderado_SelectedIndexChanged(Nothing, Nothing)
        udpProApoderado.Update()
    End Sub

    Protected Sub cmbProApoderado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProApoderado.SelectedIndexChanged
        Dim ln_CodigoPro As Integer = cmbProApoderado.SelectedValue
        Dim lb_NoDefinido As Boolean = False

        CargarComboDistrito(cmbDisApoderado, ln_CodigoPro, lb_NoDefinido, "DISTRITO")
        udpDisApoderado.Update()
    End Sub

    Protected Sub grwRequisitosAdmision_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwRequisitosAdmision.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            Dim ln_Columnas As Integer = grwRequisitosAdmision.Columns.Count

            _cellsRow(0).Text = ln_Index
            grwRequisitosAdmision.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnGenerarToken_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarToken.Click
        GenerarToken()
    End Sub

    Protected Sub btnConfirmarInscripcion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmarInscripcion.Click
        ValidarToken()
    End Sub

#End Region

#Region "Métodos"
    Public Sub New()
        mo_VariablesGlobales = New Dictionary(Of String, String)

        'Origen WEB USAT
        mo_VariablesGlobales.Item("codigoOrigenWeb") = "4"

        'Ruta Servicio
        mo_VariablesGlobales.Item("servicioUrl") = ConfigurationManager.AppSettings("RutaCampusLocal") & "WSUSAT/WSUSAT.asmx"

        'Ruta Ficha Inscripcion
        'mo_VariablesGlobales.Item("fichaInscripcionUrl") = ConfigurationManager.AppSettings("RutaCampus") & "personal/administrativo/gestion_educativa/frmFichaInscripcionPDF.aspx"
        mo_VariablesGlobales.Item("fichaInscripcionUrl") = ConfigurationManager.AppSettings("RutaCampus") & "librerianet/Admision/frmFichaInscripcionPDF.aspx" 'andy.diaz   18/03/2020
    End Sub

    Public Function ObtenerVariableGlobal(ByVal cadena As String) As Object
        Return mo_VariablesGlobales.Item(cadena)
    End Function

    Private Sub LimpiarVariablesSession()
        ViewState.Remove("modalidades")
        ViewState.Remove("nombreEve")
        ViewState.Remove("descripcionCac")
        ViewState.Remove("preferente")
        ViewState.Remove("codigosIed")
        ViewState.Remove("codigosCpf")
        ViewState.Remove("fechaHoraLimiteInscripcion")
        ViewState.Remove("fechaHoraLimitePago")
        ViewState.Remove("msgExitoInscripcion")
        ViewState.Remove("nacionalidadPeruana")
        ViewState.Remove("tokenValidado")
        ViewState.Remove("esEscuelaPre")
        ViewState.Remove("esEspecial")
        ViewState.Remove("esRegular")
    End Sub

    Private Sub AlmacenarDatosModalidad(ByVal nombreEvento As String)
        Dim modalidades As Dictionary(Of String, Object) = serializer.DeserializeObject(ViewState("modalidades"))
        For Each _kvp As KeyValuePair(Of String, Object) In modalidades
            If _kvp.Key = nombreEvento Then
                Dim modalidad As Dictionary(Of String, Object) = modalidades.Item(_kvp.Key)
                ViewState("codigoMin") = modalidad.Item("codigoMin")
                ViewState("tokenDev") = modalidad.Item("tokenDev")
                ViewState("codigosIed") = modalidad.Item("codigosIed")
                ViewState("preferente") = modalidad.Item("preferente")
                cmbDepInstitucionEducativa_SelectedIndexChanged(Nothing, Nothing) 'andy.diaz 24/08/2020 Vuelvo a cargar las instituciones, en el caso que se quieram mostrar solo preferentes

                If modalidad.ContainsKey("mostrarQuinto") Then
                    ViewState("mostrarQuinto") = Boolean.Parse(modalidad.Item("mostrarQuinto").ToString)
                Else
                    ViewState("mostrarQuinto") = True
                End If

                If modalidad.ContainsKey("mostrarEgresado") Then
                    ViewState("mostrarEgresado") = Boolean.Parse(modalidad.Item("mostrarEgresado").ToString)
                Else
                    ViewState("mostrarEgresado") = True
                End If

                'andy.diaz 10/09/2020
                If modalidad.ContainsKey("validarPrimeraInscripcionPreferente") Then
                    ViewState("validarPrimeraInscripcionPreferente") = Boolean.Parse(modalidad.Item("validarPrimeraInscripcionPreferente").ToString)
                Else
                    ViewState("validarPrimeraInscripcionPreferente") = False
                End If
                '---

                ViewState("fechaHoraLimiteInscripcion") = modalidad.Item("fechaHoraLimiteInscripcion")
                ViewState("fechaHoraLimitePago") = modalidad.Item("fechaHoraLimitePago")

                'andy.diaz 16/12/2020
                Dim esEscuelaPre As Boolean = False
                Dim esEspecial As Boolean = False
                Dim esRegular As Boolean = False

                Select Case modalidad.Item("codigoMin")
                    Case 4 'ESCUELA PRE
                        esEscuelaPre = True
                    Case 33, 43, 44, 45, 49 'EVALUACIÓN PREFERENTE, BECA 18, BECA SOCIOECONÓMICA, PRIMEROS PUESTOS
                        esEspecial = True
                    Case Else
                        esRegular = True
                End Select

                ViewState("esEscuelaPre") = esEscuelaPre
                ViewState("esEspecial") = esEspecial
                ViewState("esRegular") = esRegular

                pTextoRegular.Visible = esRegular
                spnMensajeTiempoPagoRegular.Visible = esRegular OrElse esEspecial
                pTextoAdvertenciaHoraExamen.Visible = esRegular OrElse esEspecial
                lblCondicion1Regular.Visible = esRegular
                lblCondicion1Especial.Visible = esEspecial


                pTextoEscuelaPre.Visible = esEscuelaPre
                spnMensajeTiempoPagoEscuelaPre.Visible = esEscuelaPre
                lblCondicion1EscuelaPre.Visible = esEscuelaPre
                '--------

                Exit For
            End If
        Next

        CargarComboCondicionEstudiante(mostrarQuinto:=ViewState("mostrarQuinto"), mostrarEgresado:=ViewState("mostrarEgresado"))
        udpCondicionEstudiante.Update()

        ConsultarPrecioInscripcion(ViewState("tokenDev"))
        ConsultarRequisitosAdmision(ViewState("codigoMin"))
        MostrarTiempoRestantePago()

        'andy.diaz 10/09/2020
        txtNroDocIdentidad.Text = ""
        udpNroDocIdentidad.Update()
    End Sub

    Private Sub MostrarOcultarModalidades(ByVal codigoCpf As Integer)
        Try
            alertProcesoConcluido.Visible = False
            Dim modalidades As Dictionary(Of String, Object) = serializer.DeserializeObject(ViewState("modalidades"))
            For Each _kvp As KeyValuePair(Of String, Object) In modalidades
                Dim modalidad As Dictionary(Of String, Object) = modalidades.Item(_kvp.Key)

                Dim controlId As String = modalidad.Item("controlId")
                Dim boton As LinkButton = udpModalidades.FindControl(controlId)

                If codigoCpf = -1 Then
                    boton.Visible = False
                    Continue For
                End If

                'Habilitado / Deshabilitado
                If Not Boolean.Parse(modalidad.Item("habilitado")) Then
                    boton.Visible = False
                    Continue For
                End If

                Dim lblBoton As HtmlGenericControl = boton.FindControl(controlId.Replace("btn", "spn"))
                lblBoton.InnerText = modalidad.Item("textoBoton")

                'INCLUIR: Si se envía el parámetro incluirCodigosCpf se muestra el boton solo si incluirCodigosCpf contiene el codigoCpf enviado
                Dim incluirCodigosCpf As String = modalidad.Item("incluirCodigosCpf")
                Dim aIncluirCodigosCpf() As String = incluirCodigosCpf.Split(",")
                Dim lIncluirCodigosCpf As List(Of String) = New List(Of String)(aIncluirCodigosCpf)

                If Not String.IsNullOrEmpty(incluirCodigosCpf) Then
                    If Not lIncluirCodigosCpf.Contains(codigoCpf) Then
                        boton.Visible = False
                        Continue For
                    End If
                End If

                'EXCLUIR: Si se envía el parámetro incluirCodigosCpf se muestra el boton solo si excluirCodigosCpf no contiene el codigoCpf enviado
                Dim excluirCodigosCpf As String = modalidad.Item("excluirCodigosCpf")
                Dim aExcluirCodigosCpf() As String = excluirCodigosCpf.Split(",")
                Dim lExcluirCodigosCpf As List(Of String) = New List(Of String)(aExcluirCodigosCpf)

                If lExcluirCodigosCpf.Contains(codigoCpf) Then
                    boton.Visible = False
                    Continue For
                End If

                'Fecha límite de inscripción
                Dim fechaHoraLimiteInscripcion As DateTime = modalidad.Item("fechaHoraLimiteInscripcion")
                If fechaHoraLimiteInscripcion < DateTime.Now Then
                    boton.Visible = False
                    If Boolean.Parse(modalidad.Item("habilitado")) Then
                        alertProcesoConcluido.Visible = True
                    End If
                    Continue For
                End If

                boton.Visible = True
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarCombos()
        CargarComboTipoDocumentoIdentidad("PG", "")
        cmbTipoDocIdentidad.SelectedValue = 1 'DNI

        CargarComboCarreraProfesional(cmbCarreraProfesional, "ELIGE TU CARRERA")
        CargarComboPais(cmbPaisNacimiento, lb_QuitarPeru:=True, ls_Default:="OTRA")
        CargarComboDepartamento(cmbDepNacimiento, Not ViewState("nacionalidadPeruana"), "DEPARTAMENTO")
        cmbDepNacimiento_SelectedIndexChanged(Nothing, Nothing)

        CargarComboDepartamento(cmbDepActual, False, "DEPARTAMENTO")
        cmbDepActual.SelectedValue = mn_CodigoDepLambayeque
        cmbDepActual_SelectedIndexChanged(Nothing, Nothing)

        CargarComboDepartamento(cmbDepInstitucionEducativa, False, "¿DÓNDE ESTÁ UBICADO TU COLEGIO?")
        cmbDepInstitucionEducativa.SelectedValue = mn_CodigoDepLambayeque
        cmbDepInstitucionEducativa_SelectedIndexChanged(Nothing, Nothing)

        CargarComboDepartamento(cmbDepPadre, False, "DEPARTAMENTO")
        cmbDepPadre_SelectedIndexChanged(Nothing, Nothing)

        CargarComboDepartamento(cmbDepMadre, False, "DEPARTAMENTO")
        cmbDepMadre_SelectedIndexChanged(Nothing, Nothing)

        CargarComboDepartamento(cmbDepApoderado, False, "DEPARTAMENTO")
        cmbDepApoderado_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub CargarComboTipoDocumentoIdentidad(ByVal ls_Tipo As String, ByVal ls_Param As String, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String)
        lo_Datos.Item("tipo") = ls_Tipo
        lo_Datos.Item("param") = ls_Param

        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarTipoDocumentoIdentidad", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarTipoDocumentoIdentidadResponse/ns:ListarTipoDocumentoIdentidadResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsTipoDocumentoIdentidad As New Data.DataTable
        lo_DsTipoDocumentoIdentidad.Columns.Add("Valor")
        lo_DsTipoDocumentoIdentidad.Columns.Add("Nombre")

        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsTipoDocumentoIdentidad.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next

        If String.IsNullOrEmpty(ls_Default) Then
            ClsFunciones.LlenarListas(cmbTipoDocIdentidad, lo_DsTipoDocumentoIdentidad, "Valor", "Nombre")
        Else
            ClsFunciones.LlenarListas(cmbTipoDocIdentidad, lo_DsTipoDocumentoIdentidad, "Valor", "Nombre", ls_Default)
        End If
    End Sub

    Private Sub CargarComboCarreraProfesional(ByVal cmbCombo As DropDownList, Optional ByVal ls_Default As String = "")
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarCarreraProfesional"))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarCarreraProfesionalResponse/ns:ListarCarreraProfesionalResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsCarreraProfesional As New Data.DataTable
        lo_DsCarreraProfesional.Columns.Add("Valor")
        lo_DsCarreraProfesional.Columns.Add("Nombre")

        'Filtro por codigosCpf
        Dim incluirCodigosCpf As String = IIf(ViewState("incluirCodigosCpf") IsNot Nothing, ViewState("incluirCodigosCpf"), "")
        Dim ll_IncluirCodigosCpf As New List(Of String)(incluirCodigosCpf.Split(","))

        Dim excluirCodigosCpf As String = IIf(ViewState("excluirCodigosCpf") IsNot Nothing, ViewState("excluirCodigosCpf"), "")
        Dim ll_ExcluirCodigosCpf As New List(Of String)(excluirCodigosCpf.Split(","))

        For Each _Nodo As XmlNode In lst_Nodo
            Dim codigoCpf As Integer = _Nodo.Item("Valor").InnerText
            If Not String.IsNullOrEmpty(incluirCodigosCpf) AndAlso Not ll_IncluirCodigosCpf.Contains(codigoCpf) Then
                Continue For
            End If

            If Not String.IsNullOrEmpty(excluirCodigosCpf) AndAlso ll_ExcluirCodigosCpf.Contains(codigoCpf) Then
                Continue For
            End If

            Dim ls_NombreCarrera As String = _Nodo.Item("Nombre").InnerText.ToString
            lo_DsCarreraProfesional.Rows.Add(codigoCpf, ls_NombreCarrera)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsCarreraProfesional, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub CargarComboPais(ByVal cmbCombo As DropDownList, Optional ByVal ls_Default As String = "", Optional ByVal lb_QuitarPeru As Boolean = False)
        Dim lo_Datos As New Dictionary(Of String, String)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarPais", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarPaisResponse/ns:ListarPaisResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsPais As New Data.DataTable
        lo_DsPais.Columns.Add("Valor")
        lo_DsPais.Columns.Add("Nombre")

        For Each _Nodo As XmlNode In lst_Nodo
            If lb_QuitarPeru AndAlso _Nodo.Item("Valor").InnerText = mn_CodigoPaiPeru Then Continue For
            lo_DsPais.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString.ToUpper)
        Next
        Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
        ClsFunciones.LlenarListas(cmbCombo, lo_DsPais, "Valor", "Nombre", ls_Seleccione)
    End Sub

    Private Sub CargarComboDepartamento(ByVal cmbCombo As DropDownList, Optional ByVal lb_NoDefinido As Boolean = False, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String)
        lo_Datos.Add("codigoPai", mn_CodigoPaiPeru)
        lo_Datos.Add("mostrarNoDefinido", lb_NoDefinido.ToString.ToLower)

        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarDepartamento", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarDepartamentoResponse/ns:ListarDepartamentoResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsDepartamento As New Data.DataTable
        lo_DsDepartamento.Columns.Add("Valor")
        lo_DsDepartamento.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            If lb_NoDefinido AndAlso _Nodo.Item("Valor").InnerText <> mn_CodigoDepNoDefinido Then Continue For
            lo_DsDepartamento.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString.ToUpper)
        Next

        If Not lb_NoDefinido Then
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbCombo, lo_DsDepartamento, "Valor", "Nombre", ls_Seleccione)
        Else
            ClsFunciones.LlenarListas(cmbCombo, lo_DsDepartamento, "Valor", "Nombre")
            cmbCombo.SelectedValue = mn_CodigoDepNoDefinido
        End If
    End Sub

    Private Sub CargarComboProvincia(ByVal cmbCombo As DropDownList, ByVal ls_CodigoDep As String, Optional ByVal lb_NoDefinido As Boolean = False, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoDep", ls_CodigoDep)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarProvincia", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarProvinciaResponse/ns:ListarProvinciaResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsProvincia As New Data.DataTable
        lo_DsProvincia.Columns.Add("Valor")
        lo_DsProvincia.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            If lb_NoDefinido AndAlso _Nodo.Item("Valor").InnerText <> mn_CodigoProNoDefinido Then Continue For
            lo_DsProvincia.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString.ToUpper)
        Next

        If Not lb_NoDefinido Then
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbCombo, lo_DsProvincia, "Valor", "Nombre", ls_Seleccione)
        Else
            ClsFunciones.LlenarListas(cmbCombo, lo_DsProvincia, "Valor", "Nombre")
            cmbCombo.SelectedValue = mn_CodigoProNoDefinido
        End If
    End Sub

    Private Sub CargarComboDistrito(ByVal cmbCombo As DropDownList, ByVal ls_CodigoPro As String, Optional ByVal lb_NoDefinido As Boolean = False, Optional ByVal ls_Default As String = "")
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoPro", ls_CodigoPro)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarDistrito", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ListarDistritoResponse/ns:ListarDistritoResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsDistrito As New Data.DataTable
        lo_DsDistrito.Columns.Add("Valor")
        lo_DsDistrito.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            If lb_NoDefinido AndAlso _Nodo.Item("Valor").InnerText <> mn_CodigoDisNoDefinido Then Continue For
            lo_DsDistrito.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString.ToUpper)
        Next

        If Not lb_NoDefinido Then
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbCombo, lo_DsDistrito, "Valor", "Nombre", ls_Seleccione)
        Else
            ClsFunciones.LlenarListas(cmbCombo, lo_DsDistrito, "Valor", "Nombre")
            If lb_NoDefinido Then cmbCombo.SelectedValue = mn_CodigoDisNoDefinido
        End If
    End Sub

    Private Sub CargarComboInstitucionEducativa(ByVal cmbCombo As DropDownList, ByVal ls_Tipo As String, ByVal ls_Codigo As String, Optional ByVal ls_Default As String = "")
        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("tipo", ls_Tipo) : lo_Datos.Add("codigo", ls_Codigo)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ListarInstitucionEducativa", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ListarInstitucionEducativaResponse/ns:ListarInstitucionEducativaResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            Dim lo_DsInstitucionEducativa As New Data.DataTable
            lo_DsInstitucionEducativa.Columns.Add("Valor")
            lo_DsInstitucionEducativa.Columns.Add("Nombre")
            lo_DsInstitucionEducativa.Columns.Add("Direccion")
            lo_DsInstitucionEducativa.Columns.Add("Distrito")

            'Filtro por codigosIed y por convenio
            Dim ls_CodigosIed As String = IIf(ViewState("codigosIed") IsNot Nothing, ViewState("codigosIed"), "")
            Dim lo_CodigosIed() As String = ls_CodigosIed.Split(",")
            For Each _Nodo As XmlNode In lst_Nodo
                Dim lb_Agregar As Boolean = False
                For i As Integer = 0 To lo_CodigosIed.Length - 1
                    If String.IsNullOrEmpty(ViewState("codigosIed")) OrElse _Nodo.Item("Valor").InnerText.ToString.Trim = lo_CodigosIed(i).Trim Then
                        lb_Agregar = True
                        Exit For
                    End If
                Next
                If ViewState("preferente") = "1" AndAlso _Nodo.Item("Adicional3").InnerText.ToString.Trim <> ViewState("preferente") Then
                    lb_Agregar = False
                End If

                If lb_Agregar Then
                    lo_DsInstitucionEducativa.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString, _Nodo.Item("Adicional").InnerText.ToString, _Nodo.Item("Adicional2").InnerText.ToString)
                End If
            Next
            Dim ls_Seleccione As String = IIf(String.IsNullOrEmpty(ls_Default), "-- Seleccione --", ls_Default)
            ClsFunciones.LlenarListas(cmbCombo, lo_DsInstitucionEducativa, "Valor", "Nombre", ls_Seleccione)

            Dim dr() As System.Data.DataRow
            For Each item As ListItem In cmbInstitucionEducativa.Items
                dr = lo_DsInstitucionEducativa.Select("Valor='" & item.Value & "'")
                If dr.Length > 0 Then
                    Dim lo_DrInstEducativa As Data.DataRow = dr(0)
                    item.Attributes.Add("data-tokens", lo_DrInstEducativa.Item("Nombre").ToString)
                    Dim ls_Subtext As String = ""
                    If Not String.IsNullOrEmpty(lo_DrInstEducativa.Item("Direccion")) Then
                        ls_Subtext &= " | " & lo_DrInstEducativa.Item("Direccion")
                    End If
                    If Not String.IsNullOrEmpty(lo_DrInstEducativa.Item("Distrito")) Then
                        ls_Subtext &= " - <b>" & lo_DrInstEducativa.Item("Distrito") & "</b>"
                    End If
                    item.Attributes.Add("data-subtext", ls_Subtext)
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboCondicionEstudiante(Optional ByVal mostrarQuinto As Boolean = True, Optional ByVal mostrarEgresado As Boolean = True)
        Dim lo_DtCondicionEstudiante As New Data.DataTable
        lo_DtCondicionEstudiante.Columns.Add("Valor")
        lo_DtCondicionEstudiante.Columns.Add("Nombre")

        If mostrarQuinto Then
            lo_DtCondicionEstudiante.Rows.Add("Q", "QUINTO DE SECUNDARIA")
        End If

        If mostrarEgresado Then
            lo_DtCondicionEstudiante.Rows.Add("E", "EGRESADO")
        End If

        ClsFunciones.LlenarListas(cmbCondicionEstudiante, lo_DtCondicionEstudiante, "Valor", "Nombre", "CONDICIÓN")
    End Sub

    Private Sub MostrarOcultarOrdenMerito()
        If cmbCondicionEstudiante.SelectedValue = "E" Then
            divDatosCondicion.Visible = True
        Else
            divDatosCondicion.Visible = False
        End If
        udpDatosCondicion.Update()
    End Sub

    Private Function ConsultarDeudasPersona(ByVal ls_NumeroDocIdentidad As String) As Integer
        Dim rpta As Integer = 1

        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("numeroDocIdentidad", ls_NumeroDocIdentidad)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ConsultarDeudasPersona", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ConsultarDeudasPersonaResponse/ns:ConsultarDeudasPersonaResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            If lst_Nodo.Count > 0 Then
                rpta = -1
                GenerarMensajeServidor("Ya te encuentras inscrito!", -1, "Tu inscripción sigue activa, acércate a pagar en <b>agentes BCP o BBVA</b>")
            End If
        Catch ex As Exception
            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try

        Return rpta
    End Function

    Private Function ConsultarSituacionAcademicaPersona(ByVal ls_NumeroDocIdentidad As String) As Integer
        Dim rpta As Integer = 1

        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("numeroDocIdentidad", ls_NumeroDocIdentidad)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ConsultarSituacionAcademicaPersona", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ConsultarSituacionAcademicaPersonaResponse/ns:ConsultarSituacionAcademicaPersonaResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            rpta = lst_Nodo(0).Item("Valor").InnerText
            If rpta <> 1 Then
                Dim msg As String = lst_Nodo(0).Item("Nombre").InnerText
                GenerarMensajeServidor("Error de validación", -1, msg)
            End If
        Catch ex As Exception
            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try

        Return rpta
    End Function

    'andy.diaz 10/09/2020
    Private Function ConsultarInscripcionPreferentePersona(ByVal ls_NumeroDocIdentidad As String) As Integer
        Dim rpta As Integer = 1

        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("numeroDocIdentidad", ls_NumeroDocIdentidad)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ConsultarInscripcionPreferentePersona", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ConsultarInscripcionPreferentePersonaResponse/ns:ConsultarInscripcionPreferentePersonaResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            rpta = lst_Nodo(0).Item("Valor").InnerText
            If rpta <> 1 Then
                Dim msg As String = "Hola, tu inscripción por esta modalidad NO PROCEDE porque ya hiciste uso del primer examen SIN COSTO. Por favor, intenta por la Modalidad Test DAHC o escríbenos a informes.admision@usat.edu.pe o contáctate al WhatsApp 978724097, detallando tu DNI, nombres, colegio, carrera y asunto, para que se te asigne una asesora de servicio y te pueda ayudar. 😊"
                GenerarMensajeServidor("Mensaje de validación", -1, msg)
            End If
        Catch ex As Exception
            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try

        Return rpta
    End Function
    '\andy.diaz 10/09/2020

    Private Function ConsultarInscripcionPersona(ByVal numeroDocIdentidad As String) As Integer
        Dim rpta As Integer = 1

        Try
            Dim lo_Params As New Dictionary(Of String, String)
            With lo_Params
                .Add("tipoConsulta", "GEN")
                .Add("codigoPso", "0")
                .Add("nroDocIdent", numeroDocIdentidad)
                .Add("codigoCac", "0")
                .Add("descripcionCac", ViewState("descripcionCac"))
                .Add("codigoTest", "2")
                .Add("codigoCco", "0")
                .Add("codigoMin", "-1")
            End With

            Dim ls_Method As String = "ConsultarInscripcionPersona"

            Dim lo_RespuestaSOAP As New XmlDocument
            lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), ls_Method, lo_Params))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:" & ls_Method & "Response/ns:" & ls_Method & "Result"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Result As List(Of Dictionary(Of String, Object)) = lo_Serializer.Deserialize(Of List(Of Dictionary(Of String, Object)))(ls_Respuesta)

            If lo_Result.Count > 0 Then
                If lo_Result(0).Item("codigo_Pso") <> "-1" Then
                    rpta = -1

                    Dim ls_Advertencia As String = "Usted ya cuenta con inscripción en este proceso de admisión, por favor, póngase en contacto con el área de admisión para continuar con el proceso."
                    ls_Advertencia &= "<ul>"
                    For Each item As Dictionary(Of String, Object) In lo_Result
                        ls_Advertencia &= "<li>"
                        ls_Advertencia &= "Evento: " & item.Item("descripcion_Cco") & "<br>Modalidad: " & item.Item("nombre_Min")
                        ls_Advertencia &= "</li>"
                    Next
                    ls_Advertencia &= "</ul>"

                    GenerarMensajeServidor("Advertencia", -1, ls_Advertencia)
                End If
            End If

        Catch ex As Exception
            rpta = -1
            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try

        Return rpta
    End Function

    Private Sub ConsultarPrecioInscripcion(ByVal ln_TokenDev As String)
        Try
            Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("tokenDev", ln_TokenDev)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ConsultarPrecioInscripcion", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:ConsultarPrecioInscripcionResponse/ns:ConsultarPrecioInscripcionResult/ns:e_ListItem"
            Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

            Dim precio As Decimal = 0.0
            For Each _Nodo As XmlNode In lst_Nodo
                precio = _Nodo.Item("Valor").InnerText
                spnPrecioInscripcion.InnerHtml = "S/ " & precio
                Exit For
            Next

            If precio > 0 Then
                divPasoUno.Visible = True
                divNroUno.InnerHtml = "1"
                divNroDos.InnerHtml = "2"
                divNroTres.InnerHtml = "3"
                ' prfPasoDos.InnerHtml = "Requisitos obligatorios para rendir el EXAMEN de ADMISIÓN que debes enviar escaneado al correo informes.admision@usat.edu.pe y luego presentar los originales en Oficina de Admisión:"
                spnCondicionPago.InnerHtml = "REALIZAR EL PAGO POR DERECHO DE EVALUACIÓN DE ADMISION Y"
            Else
                divPasoUno.Visible = False
                ' prfPasoDos.InnerHtml = "Debes presentar los siguientes requisitos en la oficina de admisión - Campus USAT"
                divNroDos.InnerHtml = "1"
                divNroTres.InnerHtml = "2"
                spnCondicionPago.InnerHtml = ""
            End If

            Dim ls_PasoDos As String = ""
            If ViewState("esEscuelaPre") Then
                ls_PasoDos = "Obligatorio, adjuntar los requisitos legibles de postulación en formato JPG, antes de la fecha de inicio de la PRE USAT. El enlace para adjuntar los documentos es "
                ls_PasoDos &= "<a href='http://www.tuproyectodevida.pe/documentos/' target='_blank'>www.tuproyectodevida.pe/documentos/</a><br>"

            ElseIf ViewState("esEspecial") Then
                ls_PasoDos = "Obligatorio revisar los requisitos según la modalidad por la que postulas en "
                ls_PasoDos &= "<a href='http://www.tuproyectodevida.pe/modalidades/' target='_blank'>http://www.tuproyectodevida.pe/modalidades/</a>, "
                ls_PasoDos &= "luego debes adjuntarlos en formato JPG (alta resolución) en el siguiente enlace "
                ls_PasoDos &= "<a href='http://www.tuproyectodevida.pe/documentos/' target='_blank'>http://www.tuproyectodevida.pe/documentos/</a>, "
                ls_PasoDos &= "busca la fecha en la que postulas, lo seleccionas y adjuntar tus requisitos.<br>"

            ElseIf ViewState("esRegular") Then
                ls_PasoDos = "Obligatorio, adjuntar los requisitos legibles de postulación en formato JPG, antes de la fecha de postulación, según la modalidad por la que postulas. El enlace para adjuntar los documentos es "
                ls_PasoDos &= "<a href='http://www.tuproyectodevida.pe/documentos/' target='_blank'>www.tuproyectodevida.pe/documentos/</a><br>"
                ls_PasoDos &= "Los postulantes por la Modalidad de Traslado Externo y Modalidad de Graduado y Titulado, deben contactarse con su Asistente de Servicio USAT.<br>"
            End If
            prfPasoDos.InnerHtml = ls_PasoDos

            udpInstrucciones.Update()
        Catch ex As Exception
            scriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alertMessage", "alert('" & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub MostrarOcultarMensajePuesto()
        'If cmbOrdenMerito.SelectedValue = 1 OrElse cmbOrdenMerito.SelectedValue = 2 Then
        '    spnMensajeAnulacionCargo.Visible = True
        'Else
        '    spnMensajeAnulacionCargo.Visible = False
        'End If
        'udpInstrucciones.Update()
    End Sub

    Private Sub ConsultarRequisitosAdmision(ByVal ln_CodigoMin As Integer)
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("codigoMin", ln_CodigoMin)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "ConsultarRequisitosAdmision", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:ConsultarRequisitosAdmisionResponse/ns:ConsultarRequisitosAdmisionResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim lo_DsRequisitoAdmision As New Data.DataTable
        lo_DsRequisitoAdmision.Columns.Add("Valor")
        lo_DsRequisitoAdmision.Columns.Add("Nombre")
        For Each _Nodo As XmlNode In lst_Nodo
            lo_DsRequisitoAdmision.Rows.Add(_Nodo.Item("Valor").InnerText.ToString, _Nodo.Item("Nombre").InnerText.ToString)
        Next

        grwRequisitosAdmision.DataSource = lo_DsRequisitoAdmision
        grwRequisitosAdmision.DataBind()

        udpInstrucciones.Update()
    End Sub

    Private Function ValidarInscripcion() As Dictionary(Of String, String)
        Dim lb_EncuentraError As Boolean = False
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Item("rpta") = "1"
        lo_Validacion.Item("msg") = "Sin errores"
        lo_Validacion.Item("control") = ""

        If Not lb_EncuentraError AndAlso (ViewState("tokenValidado") Is Nothing OrElse Not ViewState("tokenValidado")) Then
            lo_Validacion.Item("msg") = "El token de inscripción no ha sido validado"
            lb_EncuentraError = True
        End If

        'FECHA LÍMITE DE INSCRIPCIÓN
        Dim ls_FechaHoraLimiteInscripcion As String = ViewState("fechaHoraLimiteInscripcion")
        If Not lb_EncuentraError AndAlso Not String.IsNullOrEmpty(ls_FechaHoraLimiteInscripcion) Then
            Dim ld_FechaHoraLimiteInscripcion As DateTime = DateTime.Parse(ls_FechaHoraLimiteInscripcion)
            If ld_FechaHoraLimiteInscripcion < Date.Now() Then
                lo_Validacion.Item("msg") = "Se ha superado la fecha límite de inscripción"
                lb_EncuentraError = True
            End If
        End If

        'MODALIDAD Y CARRERA
        If Not lb_EncuentraError AndAlso cmbCarreraProfesional.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar una carrera profesional"
            lo_Validacion.Item("control") = "cmbCarreraProfesional"
            lb_EncuentraError = True
        End If

        'DATOS PERSONALES
        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtNroDocIdentidad.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar su número de documento de identidad"
            lo_Validacion.Item("control") = "txtNroDocIdentidad"
            lb_EncuentraError = True
        End If

        If Not ViewState("nacionalidadPeruana") Then
            If lb_EncuentraError AndAlso cmbPaisNacimiento.SelectedValue = "-1" Then
                lo_Validacion.Item("msg") = "Debe seleccionar un país de nacimiento"
                lo_Validacion.Item("control") = "cmbPaisNacimiento"
                lb_EncuentraError = True
            End If
        Else
            If Not lb_EncuentraError AndAlso cmbDepNacimiento.SelectedValue = "-1" Then
                lo_Validacion.Item("msg") = "Debe seleccionar su departamento de nacimiento"
                lo_Validacion.Item("control") = "cmbDepNacimiento"
                lb_EncuentraError = True
            End If

            If Not lb_EncuentraError AndAlso cmbProNacimiento.SelectedValue = "-1" Then
                lo_Validacion.Item("msg") = "Debe seleccionar su provincia de nacimiento"
                lo_Validacion.Item("control") = "cmbProNacimiento"
                lb_EncuentraError = True
            End If

            If Not lb_EncuentraError AndAlso cmbDisNacimiento.SelectedValue = "-1" Then
                lo_Validacion.Item("msg") = "Debe seleccionar su distrito de nacimiento"
                lo_Validacion.Item("control") = "cmbDisNacimiento"
                lb_EncuentraError = True
            End If
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(dtpFecNacimiento.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar su fecha de nacimiento"
            lo_Validacion.Item("control") = "dtpFecNacimiento"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso Not IsDate(dtpFecNacimiento.Text.Trim) Then
            lo_Validacion.Item("msg") = "El formato de la fecha de nacimiento no es correcto"
            lo_Validacion.Item("control") = "dtpFecNacimiento"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtApellidoPaterno.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar su apellido paterno"
            lo_Validacion.Item("control") = "txtApellidoPaterno"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtApellidoMaterno.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar su apellido materno"
            lo_Validacion.Item("control") = "txtApellidoMaterno"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtNombres.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar sus nombres"
            lo_Validacion.Item("control") = "txtNombres"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtDireccionActual.Text) Then
            lo_Validacion.Item("msg") = "Debe ingresar su dirección"
            lo_Validacion.Item("control") = "txtDireccionActual"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso cmbDepActual.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar su departamento actual"
            lo_Validacion.Item("control") = "cmbDepNacimiento"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso cmbProActual.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar su provincia actual"
            lo_Validacion.Item("control") = "cmbProActual"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso cmbDisActual.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar su distrito actual"
            lo_Validacion.Item("control") = "cmbDisActual"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso String.IsNullOrEmpty(txtCelular.Text) Then
            lo_Validacion.Item("msg") = "Debe seleccionar su número de celular"
            lo_Validacion.Item("control") = "txtCelular"
            lb_EncuentraError = True
        End If

        'DATOS ACADÉMICOS
        If Not lb_EncuentraError AndAlso cmbDepInstitucionEducativa.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar el departamento de su colegio"
            lo_Validacion.Item("control") = "cmbDepInstitucionEducativa"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso cmbInstitucionEducativa.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar su colegio"
            lo_Validacion.Item("control") = "cmbInstitucionEducativa"
            lb_EncuentraError = True
        End If

        If Not lb_EncuentraError AndAlso cmbCondicionEstudiante.SelectedValue = "-1" Then
            lo_Validacion.Item("msg") = "Debe seleccionar su condición de colegio"
            lo_Validacion.Item("control") = "cmbCondicionEstudiante"
            lb_EncuentraError = True
        End If

        'DATOS FAMILIARES
        If Not lb_EncuentraError AndAlso Not (chkRespPagoPadre.Checked OrElse chkRespPagoMadre.Checked OrElse chkRespPagoApoderado.Checked) Then
            lo_Validacion.Item("msg") = "Debe seleccionar al menos un responsable de pago"
            lo_Validacion.Item("control") = "chkRespPagoPadre"
            lb_EncuentraError = True
        End If

        If lb_EncuentraError Then lo_Validacion.Item("rpta") = "0"

        Return lo_Validacion
    End Function

    'Function GeneraClave(ByVal ls_ApePaterno As String, ByVal ls_Nombres As String) As String
    '    Randomize()
    '    Return Right(UCase(ls_ApePaterno), 1) & _
    '        Left(UCase(ls_Nombres), 1) & _
    '        CInt(Rnd() * 4) & CInt(Rnd() * 5) & CInt(Rnd() * 9) & CInt(Rnd() * 7)
    'End Function

    'andy.diaz 14/09/2020 - Utilizo la función EliminarTildes
    Function GeneraClave(ByVal ls_ApePaterno As String, ByVal ls_Nombres As String) As String
        Randomize()
        Return Right(UCase(EliminarTildes(ls_ApePaterno)), 1) & _
            Left(UCase(EliminarTildes(ls_Nombres)), 1) & _
            CInt(Rnd() * 4) & CInt(Rnd() * 5) & CInt(Rnd() * 9) & CInt(Rnd() * 7)
    End Function

    'andy.diaz 14/09/2020
    Public Function EliminarTildes(ByVal accentedStr As String) As String
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)
        Return System.Text.Encoding.UTF8.GetString(tempBytes)
    End Function

    Public Function EncriptaTexto(ByVal base64Decoded As String) As String
        Dim base64Encoded As String = ""
        Try
            Dim data As Byte()
            data = System.Text.UTF8Encoding.UTF8.GetBytes(base64Decoded)
            base64Encoded = System.Convert.ToBase64String(data)
        Catch ex As Exception

        End Try
        Return base64Encoded
    End Function

    Public Function DesencriptaTexto(ByVal base64Encoded As String) As String
        Dim base64Decoded As String = ""
        Try
            Dim data() As Byte
            data = System.Convert.FromBase64String(base64Encoded)
            base64Decoded = System.Text.UTF8Encoding.UTF8.GetString(data)
        Catch ex As Exception

        End Try
        Return base64Decoded
    End Function

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ls_Rpta As String, ByVal ls_Msg As String, Optional ByVal ls_Control As String = "")
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        With divRespuestaPostback.Attributes
            .Item("data-rpta") = ls_Rpta
            .Item("data-msg") = ls_Msg
            .Item("data-control") = ls_Control
        End With
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub GenerarMensajeConfirmacion(ByVal ls_Msg As String)
        divMdlMenConfParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeConfirmacionParametros.Update()

        divRespuestaConfirmacion.Attributes.Item("data-msg") = ls_Msg
        udpMensajeConfirmacionBody.Update()
    End Sub

    Private Sub GenerarMensajeToastr(ByVal rpta As String, ByVal msg As String)
        divMensajesToastr.Attributes.Item("data-mostrar") = "true"
        divMensajesToastr.Attributes.Item("data-rpta") = rpta
        divMensajesToastr.Attributes.Item("data-msg") = msg
        udpMensajesToastr.Update()
    End Sub

    Private Sub LimpiarMensajeToastr()
        divMensajesToastr.Attributes.Item("data-mostrar") = "false"
        divMensajesToastr.Attributes.Item("data-rpta") = ""
        divMensajesToastr.Attributes.Item("data-msg") = ""
        udpMensajesToastr.Update()
    End Sub

    Private Sub CargarDataFichaInscripcion(ByVal ls_CodigoAlu As String)
        divDataFichaInscripcion.Attributes.Item("data-mostrar") = "true"
        divDataFichaInscripcion.Attributes.Item("data-alu") = ls_CodigoAlu
        udpDataFichaInscripcion.Update()
    End Sub

    Private Sub LimpiarMensajeServidor()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()

        divRespuestaPostback.Attributes.Item("data-rpta") = ""
        divRespuestaPostback.Attributes.Item("data-msg") = ""
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub LimpiarMensajeConfirmacion()
        divMdlMenConfParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeConfirmacionParametros.Update()
    End Sub

    Private Sub lr_BuscarEmailCoincidente(ByVal email As String)
        Dim lo_Datos As New Dictionary(Of String, String) : lo_Datos.Add("email", email)
        Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "BuscarEmailCoincidente", lo_Datos))

        Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
        Dim ls_RutaNodos As String = "//ns:BuscarEmailCoincidenteResponse/ns:BuscarEmailCoincidenteResult/ns:e_ListItem"
        Dim lst_Nodo As XmlNodeList = lo_RespuestaSOAP.DocumentElement.SelectNodes(ls_RutaNodos, lo_Namespace)

        Dim coincide As Boolean = False
        Dim verificado As Boolean = False

        For Each _nodo As XmlNode In lst_Nodo
            coincide = True
            If Not String.IsNullOrEmpty(_nodo.Item("Valor").InnerText) Then
                verificado = _nodo.Item("Valor").InnerText
                If verificado Then
                    Exit For
                End If
            End If
        Next
        hddEmailCoincidente.Value = IIf(coincide, "1", "0")
        hddEmailVerificado.Value = IIf(verificado, "1", "0")

        udpVerificacionEmail.Update()
    End Sub

    Private Sub GenerarToken()
        Try
            'Si se modifica el número de celular en token, actualizo el original
            txtCelular.Text = txtCelularToken.Text.Trim()
            udpCelular.Update()

            Dim celularCet As String = txtCelular.Text.Trim
            If String.IsNullOrEmpty(celularCet) Then
                GenerarMensajeToastr("0", "Debe ingresar su número celular")
                Exit Sub
            End If

            Dim nombre As String = txtNombres.Text.Trim.ToUpper
            Dim operacionCet As String = "INSCRIPCIÓN WEB"
            Dim tipoCet As String = "A"

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("nombre", nombre)
                .Add("celularCet", celularCet)
                .Add("operacionCet", operacionCet)
                .Add("tipoCet", tipoCet)
            End With

            Dim lo_RespuestaSOAP As New XmlDocument
            lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "GenerarCelularTokenInscripcion", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:GenerarCelularTokenInscripcionResponse/ns:GenerarCelularTokenInscripcionResult"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_Respuesta)

            If lo_Dict.Item("rpta") = "1" Then
                ViewState("tokenValidado") = True

                txtToken.Enabled = True
                txtToken.Text = ""
                udpToken.Update()

                lr_ProcesoMarketingToken(lo_Dict.Item("token"))
            End If
            GenerarMensajeToastr(lo_Dict.Item("rpta"), lo_Dict.Item("msg"))

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ValidarToken()
        Try
            Dim celularCet As String = txtCelular.Text.Trim
            Dim tipoCet As String = "A"
            Dim tokenCet As String = txtToken.Text.Trim.ToUpper

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("celularCet", celularCet)
                .Add("tipoCet", tipoCet)
                .Add("tokenCet", tokenCet)
            End With

            Dim lo_RespuestaSOAP As New XmlDocument
            lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "CompararCelularTokenInscripcion", lo_Datos))

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:CompararCelularTokenInscripcionResponse/ns:CompararCelularTokenInscripcionResult"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_Respuesta)

            If lo_Dict.Item("rpta") = "1" Then
                txtToken.Enabled = False
                txtToken.Text = ""

                InscribirAlumno()
            Else
                GenerarMensajeToastr(lo_Dict.Item("rpta"), lo_Dict.Item("msg"))

                btnConfirmarInscripcion.Attributes.Item("class") = ""
                udpConfirmarInscripcion.Update()
            End If

            udpToken.Update()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub InscribirAlumno()
        Dim ls_RespuestaServicio As String = ""

        Try
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarInscripcion()
            If lo_Validacion.Item("rpta") <> "1" Then
                GenerarMensajeServidor("Error de validación", lo_Validacion.Item("rpta"), lo_Validacion.Item("msg"))
                Exit Sub
            End If

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Item("codigoTest") = mn_CodigoTest
                .Item("tokenDev") = ViewState("tokenDev")
                .Item("codigoMin") = ViewState("codigoMin")
                .Item("descripcionCac") = ViewState("descripcionCac")
                .Item("codigoOri") = ObtenerVariableGlobal("codigoOrigenWeb")
                .Item("nombreEve") = ViewState("nombreEve")
                'Datos personales
                .Item("codigoDoci") = cmbTipoDocIdentidad.SelectedValue
                .Item("numeroDocIdent") = txtNroDocIdentidad.Text.Trim.ToUpper
                .Item("apellidoPaterno") = txtApellidoPaterno.Text.Trim.ToUpper
                .Item("apellidoMaterno") = txtApellidoMaterno.Text.Trim.ToUpper
                .Item("nombres") = txtNombres.Text.Trim.ToUpper
                .Item("fechaNacimiento") = dtpFecNacimiento.Text.Trim
                .Item("sexo") = IIf(rbtSexoMasculino.Checked, "M", "F")
                .Item("telefonoFijo") = txtTelefono.Text.Trim
                .Item("telefonoCelular") = txtCelular.Text.Trim

                Dim discapacidades As String = ""
                For Each _item As ListItem In cmbDiscapacidad.Items
                    If _item.Selected Then
                        discapacidades &= _item.Value + ","
                    End If
                Next
                .Item("discapacidades") = discapacidades

                .Item("otraDiscapacidad") = txtOtraDiscapacidad.Text.Trim.ToUpper
                .Item("emailPrincipal") = txtEmail.Text.Trim.ToUpper
                .Item("codigoNacPai") = IIf(ViewState("nacionalidadPeruana"), mn_CodigoPaiPeru, cmbPaisNacimiento.SelectedValue)
                .Item("codigoNacDis") = cmbDisNacimiento.SelectedValue
                .Item("codigoDis") = cmbDisActual.SelectedValue
                .Item("direccion") = txtDireccionActual.Text.Trim.ToUpper
                .Item("password") = GeneraClave(txtApellidoPaterno.Text.Trim, txtNombres.Text.Trim)

                'Datos académicos
                .Item("codigoIed") = cmbInstitucionEducativa.SelectedValue
                .Item("codigoCpf") = cmbCarreraProfesional.SelectedValue
                .Item("grado") = cmbCondicionEstudiante.SelectedValue
                .Item("anioEgresoSec") = txtAnioEgreso.Text.Trim

                .Item("ordenMerito") = "0"
                If .Item("grado") = "E" Then
                    If cmbOrdenMerito.SelectedValue <> "-1" Then
                        .Item("ordenMerito") = cmbOrdenMerito.SelectedValue
                    Else
                        If Not String.IsNullOrEmpty(txtOtroMerito.Text) Then
                            .Item("ordenMerito") = Integer.Parse(txtOtroMerito.Text.Trim)
                        End If
                    End If
                End If

                'Datos del padre
                .Item("numeroDocIdentPadre") = txtDniPadre.Text.Trim
                .Item("apellidoPaternoPadre") = txtApellidoPaternoPadre.Text.Trim.ToUpper
                .Item("apellidoMaternoPadre") = txtApellidoMaternoPadre.Text.Trim.ToUpper
                .Item("nombresPadre") = txtNombresPadre.Text.Trim.ToUpper
                .Item("fechaNacimientoPadre") = dtpFecNacPadre.Text.Trim.ToUpper
                .Item("direccionPadre") = txtDireccionPadre.Text.Trim.ToUpper
                .Item("codigoPadreDis") = cmbDisPadre.SelectedValue
                .Item("telefonoFijoPadre") = txtTelefonoPadre.Text.Trim
                .Item("telefonoCelularPadre") = txtCelularPadre.Text.Trim
                .Item("emailPadre") = txtEmailPadre.Text.Trim.ToUpper
                .Item("indRespPagoPadre") = chkRespPagoPadre.Checked.ToString.ToLower

                'Datos de la madre
                .Item("numeroDocIdentMadre") = txtDniMadre.Text.Trim
                .Item("apellidoPaternoMadre") = txtApellidoPaternoMadre.Text.Trim.ToUpper
                .Item("apellidoMaternoMadre") = txtApellidoMaternoMadre.Text.Trim.ToUpper
                .Item("nombresMadre") = txtNombresMadre.Text.Trim.ToUpper
                .Item("fechaNacimientoMadre") = dtpFecNacMadre.Text.Trim.ToUpper
                .Item("direccionMadre") = txtDireccionMadre.Text.Trim.ToUpper
                .Item("codigoMadreDis") = cmbDisMadre.SelectedValue
                .Item("telefonoFijoMadre") = txtTelefonoMadre.Text.Trim
                .Item("telefonoCelularMadre") = txtCelularMadre.Text.Trim
                .Item("emailMadre") = txtEmailMadre.Text.Trim.ToUpper
                .Item("indRespPagoMadre") = chkRespPagoMadre.Checked.ToString.ToLower

                'Datos del apoderado
                .Item("numeroDocIdentApod") = txtDniApoderado.Text.Trim
                .Item("apellidoPaternoApod") = txtApellidoPaternoApoderado.Text.Trim.ToUpper
                .Item("apellidoMaternoApod") = txtApellidoMaternoApoderado.Text.Trim.ToUpper
                .Item("nombresApod") = txtNombresApoderado.Text.Trim.ToUpper
                .Item("fechaNacimientoApod") = dtpFecNacApoderado.Text.Trim.ToUpper
                .Item("direccionApod") = txtDireccionApoderado.Text.Trim.ToUpper
                .Item("codigoApodDis") = cmbDisApoderado.SelectedValue
                .Item("telefonoFijoApod") = txtTelefonoApoderado.Text.Trim
                .Item("telefonoCelularApod") = txtCelularApoderado.Text.Trim
                .Item("emailApod") = txtEmailApoderado.Text.Trim.ToUpper
                .Item("indRespPagoApod") = chkRespPagoApoderado.Checked.ToString.ToLower

                'Otros datos
                .Item("validaDeuda") = "true"
                .Item("usuarioReg") = 4877
                .Item("verificadoEmi") = hddEmailVerificado.Value
                .Item("origenInscripcionAlu") = "W" 'WEB
            End With

            ls_RespuestaServicio = mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), "InscribirAlumno", lo_Datos)
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(ls_RespuestaServicio)

            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:InscribirAlumnoResponse /ns:InscribirAlumnoResult"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Dict As Dictionary(Of String, String) = lo_Serializer.Deserialize(Of Dictionary(Of String, String))(ls_Respuesta)

            If lo_Dict.Item("rpta") = "1" Then
                CargarDataFichaInscripcion(lo_Dict.Item("cod"))
                GenerarMensajeConfirmacion(ViewState("msgExitoInscripcion"))

                Dim ls_Email As String = txtEmail.Text.Trim
                Dim ln_CodigoAlu As Integer = lo_Dict.Item("cod")
                EnviarEmailAlumno(ViewState("codigoMin"), ln_CodigoAlu)

                lr_ProcesoMarketing("INSCRIPCIÓN")

                LimpiarVariablesSession()
            Else
                GenerarMensajeServidor("Respuesta del servidor", lo_Dict.Item("rpta"), lo_Dict.Item("msg"))
            End If

        Catch ex As Exception
            GenerarMensajeServidor("Error", "-1", ls_RespuestaServicio)
        End Try

    End Sub

    Public Function EnviarEmailAlumno(ByVal codigoMin As Integer, ByVal ls_CodigoAlu As Integer) As Boolean
        Try
            Dim ls_Correo As String = txtEmail.Text.Trim.ToUpper
            If String.IsNullOrEmpty(ls_Correo) Then
                Return False
            End If

            Dim Mail As New ClsMail
            Dim ls_RutaFichaInscripcion As String = ObtenerVariableGlobal("fichaInscripcionUrl") & "?alu=" & ls_CodigoAlu

            ' Examen de admisión: 1
            ' Evaluación Test Dahc: 8
            ' Escuela Pre Universitaria USAT: 4
            ' Traslado externo: 2
            ' Graduados y titulados: 6
            ' Deportistas destacados: 38
            ' Personas con discapacidad: 39
            ' Bachillerato internacional: 48
            Dim modalidadesCorreo1() As Integer = {1, 8, 4, 2, 6, 38, 39, 48}

            Dim ls_Asunto As String = ""
            Dim ls_Mensaje As String = ""
            Dim ls_Nombre As String = txtNombres.Text.Trim.ToUpper

            If Array.IndexOf(modalidadesCorreo1, codigoMin) >= 0 Then
                ls_Asunto = "Admisión USAT – Confirmación de inscripción a Examen de Admisión"

                ls_Mensaje = "<div style='font-family: Calibri, sans-serif; font-size: 14px'>"
                ls_Mensaje &= "Hola " & ls_Nombre & ": <br/>"
                ls_Mensaje &= "<p>Recibe el saludo de la Dirección de Admisión y Marketing, a la vez, te confirmamos que ya estás inscrito para postular al EXAMEN de ADMISIÓN.<p>"
                ls_Mensaje &= "<p>Solo recalcar, que debes enviar los requisitos de admisión según la modalidad por la que postulas y luego acercarte a cualquier agente o banco del BCP o BBVA para pagar por tu derecho de examen, también lo puedes pagar por aplicativo móvil de los bancos mencionados, el <b>código viene a ser el Nº de DNI del postulante.</b><p>"
                ls_Mensaje &= "<p>También, debes <u><b>leer la directiva y descargar la ficha de inscripción</b></u> que adjunto para hacerla firmar por tu apoderado, luego enviar junto con los requisitos de postulación al siguiente enlace <a href='http://www.tuproyectodevida.pe/documentos'>www.tuproyectodevida.pe/documentos</a><p>"
                ls_Mensaje &= "<p>Enlace para descargar tu Ficha de inscripción de Admisión: <a href='" & ls_RutaFichaInscripcion & "'>Descargar</a><p>"
                ls_Mensaje &= "<p>En otro correo, te detallaremos el proceso de admisión que debes seguir.<p>"
                ls_Mensaje &= "Atentamente <br/>"
                ls_Mensaje &= "Dirección de Admisión y Marketing <br/>"
                ls_Mensaje &= "Universidad Católica Santo Toribio de Mogrovejo <br/>"
                ls_Mensaje &= "</div>"
            Else
                ls_Asunto = "Admisión USAT – Confirmación de inscripción a Examen de Admisión"

                ls_Mensaje = "<div style='font-family: Calibri, sans-serif; font-size: 14px'>"
                ls_Mensaje &= "Hola " & ls_Nombre & ": <br/>"
                ls_Mensaje &= "<p>Recibe el saludo de la Dirección de Admisión y Marketing, a la vez, te confirmamos que ya estás inscrito para postular al EXAMEN de ADMISIÓN.<p>"
                ls_Mensaje &= "<p>Solo recalcar, que debes enviar los requisitos de admisión según la modalidad por la que postulas.</b><p>"
                ls_Mensaje &= "<p>También, debes <u><b>leer la directiva y descargar la ficha de inscripción</b></u> que adjunto para hacerla firmar por tu apoderado, luego enviar junto con los requisitos de postulación al siguiente enlace <a href='http://www.tuproyectodevida.pe/documentos'>www.tuproyectodevida.pe/documentos</a><p>"
                ls_Mensaje &= "<p>Enlace para descargar tu Ficha de inscripción de Admisión: <a href='" & ls_RutaFichaInscripcion & "'>Descargar</a><p>"
                ls_Mensaje &= "<p>En otro correo, te detallaremos el proceso de admisión que debes seguir.<p>"
                ls_Mensaje &= "Atentamente <br/>"
                ls_Mensaje &= "Dirección de Admisión y Marketing <br/>"
                ls_Mensaje &= "Universidad Católica Santo Toribio de Mogrovejo <br/>"
                ls_Mensaje &= "</div>"
            End If

            Return Mail.EnviarMail("campusvirtual@usat.edu.pe", "Admisión USAT", ls_Correo, ls_Asunto, ls_Mensaje, True, "", "informes.admision@usat.edu.pe")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub lr_ProcesoMarketing(ByVal ls_Tipo As String)
        Try
            'Obtengo datos adicionales de la institución educativa
            Dim departamentoIed As String = ""
            Dim provinciaIed As String = ""
            Dim distritoIed As String = ""

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("tipoConsulta", "GEN")
                .Add("codigoIed", cmbInstitucionEducativa.SelectedValue)
            End With

            Dim ls_NombreNodo As String = "ConsultarInstitucionEducativa"
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), ls_NombreNodo, lo_Datos))
            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:" & ls_NombreNodo & "Response /ns:" & ls_NombreNodo & "Result"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Result As List(Of Dictionary(Of String, Object)) = lo_Serializer.Deserialize(Of List(Of Dictionary(Of String, Object)))(ls_Respuesta)

            If lo_Result.Count > 0 Then
                If lo_Result(0).Item("codigo_ied") <> "-1" Then
                    departamentoIed = lo_Result(0).Item("nombre_dep")
                    provinciaIed = lo_Result(0).Item("nombre_pro")
                    distritoIed = lo_Result(0).Item("nombre_dis")
                End If
            End If
            '----------------

            Using _client As New Net.WebClient
                Dim lo_Credentials As New NetworkCredential("marketing", "USAT2015")
                _client.Credentials = lo_Credentials
                Dim lo_ReqParam As New Specialized.NameValueCollection
                lo_ReqParam.Add("dniApoderado", txtDniApoderado.Text.Trim)
                lo_ReqParam.Add("apePatApoderado", txtApellidoPaternoApoderado.Text.Trim)
                lo_ReqParam.Add("apeMatApoderado", txtApellidoMaternoApoderado.Text.Trim)
                lo_ReqParam.Add("numCelApoderado", txtCelularApoderado.Text.Trim)
                lo_ReqParam.Add("emailApoderado", txtEmailApoderado.Text.Trim)
                lo_ReqParam.Add("dni", txtNroDocIdentidad.Text.Trim)
                lo_ReqParam.Add("apellidoPaterno", txtApellidoPaterno.Text.Trim)
                lo_ReqParam.Add("apellidoMaterno", txtApellidoMaterno.Text.Trim)
                lo_ReqParam.Add("nombres", txtNombres.Text.Trim)
                lo_ReqParam.Add("numCelular", txtCelular.Text.Trim)
                lo_ReqParam.Add("numFijo", txtTelefono.Text.Trim)
                lo_ReqParam.Add("email", txtEmail.Text.Trim)
                lo_ReqParam.Add("direccion", txtDireccionActual.Text.Trim)
                lo_ReqParam.Add("departamento", cmbDepActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("provincia", cmbProActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("distrito", cmbDisActual.SelectedItem.Text.Trim)
                lo_ReqParam.Add("fecNacimiento", dtpFecNacimiento.Text.Trim)
                lo_ReqParam.Add("sexo", IIf(rbtSexoMasculino.Checked, "MASCULINO", "FEMENINO"))
                lo_ReqParam.Add("anioEstudio", cmbCondicionEstudiante.SelectedValue)
                lo_ReqParam.Add("centroLabores", "")
                lo_ReqParam.Add("cargo", "")
                lo_ReqParam.Add("ruc", "")
                lo_ReqParam.Add("departamentoInstEduc", departamentoIed)
                lo_ReqParam.Add("provinciaInstEduc", provinciaIed)
                lo_ReqParam.Add("distritoInstEduc", distritoIed)
                lo_ReqParam.Add("institucionEducativa", cmbInstitucionEducativa.SelectedItem.Text.Trim)
                lo_ReqParam.Add("carreraProfesional", cmbCarreraProfesional.SelectedItem.Text.Trim)
                lo_ReqParam.Add("consultas", "")
                lo_ReqParam.Add("tipo", ls_Tipo)
                lo_ReqParam.Add("campoAdicional1", "")
                lo_ReqParam.Add("campoAdicional2", "")
                lo_ReqParam.Add("campoAdicional3", "INSCRIPCIÓN")
                lo_ReqParam.Add("campoAdicional4", "ONLINE")
                lo_ReqParam.Add("campoAdicional5", "WEB USAT")
                lo_ReqParam.Add("campoAdicional6", "")
                Dim lo_ResponseBytes As Byte() = _client.UploadValues("http://www.tuproyectodevida.pe/autorespuesta/auto_prueba.php", "POST", lo_ReqParam)
                Dim lo_ResponseBody As String = (New Text.UTF8Encoding).GetString(lo_ResponseBytes)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lr_ProcesoMarketingToken(ByVal ls_Token As String)
        Try
            'Obtengo datos adicionales de la institución educativa
            Dim departamentoIed As String = ""
            Dim provinciaIed As String = ""
            Dim distritoIed As String = ""

            Dim lo_Datos As New Dictionary(Of String, String)
            With lo_Datos
                .Add("tipoConsulta", "GEN")
                .Add("codigoIed", cmbInstitucionEducativa.SelectedValue)
            End With

            Dim ls_NombreNodo As String = "ConsultarInstitucionEducativa"
            Dim lo_RespuestaSOAP As New XmlDocument : lo_RespuestaSOAP.LoadXml(mo_SOAP.lr_RealizarPeticionSOAP(ObtenerVariableGlobal("servicioUrl"), ls_NombreNodo, lo_Datos))
            Dim lo_Namespace As XmlNamespaceManager = New XmlNamespaceManager(lo_RespuestaSOAP.NameTable) : lo_Namespace.AddNamespace("ns", "http://tempuri.org/")
            Dim ls_RutaNodos As String = "//ns:" & ls_NombreNodo & "Response /ns:" & ls_NombreNodo & "Result"
            Dim ls_Respuesta As String = lo_RespuestaSOAP.DocumentElement.SelectSingleNode(ls_RutaNodos, lo_Namespace).InnerText

            Dim lo_Serializer As New JavaScriptSerializer()
            Dim lo_Result As List(Of Dictionary(Of String, Object)) = lo_Serializer.Deserialize(Of List(Of Dictionary(Of String, Object)))(ls_Respuesta)

            If lo_Result.Count > 0 Then
                If lo_Result(0).Item("codigo_ied") <> "-1" Then
                    departamentoIed = lo_Result(0).Item("nombre_dep")
                    provinciaIed = lo_Result(0).Item("nombre_pro")
                    distritoIed = lo_Result(0).Item("nombre_dis")
                End If
            End If
            '----------------

            Dim lo_ReqParam As New Specialized.NameValueCollection
            With lo_ReqParam
                .Add("celular", txtCelular.Text.Trim)
                .Add("codigo", ls_Token)
                .Add("dni", txtNroDocIdentidad.Text.Trim)
                .Add("nombres", txtNombres.Text.Trim.ToUpper)
                .Add("apellidos", txtApellidoMaterno.Text.Trim.ToUpper & " " & txtApellidoPaterno.Text.Trim.ToUpper)
                .Add("carrera", cmbCarreraProfesional.SelectedItem.Text.Trim.ToUpper)
                .Add("colegio", cmbInstitucionEducativa.SelectedItem.Text.Trim.ToUpper)
                .Add("departamento", departamentoIed.ToUpper)
                .Add("provincia", provinciaIed.ToUpper)
                .Add("distrito", distritoIed.ToUpper)
            End With

            Using _client As New Net.WebClient
                Dim lo_Credentials As New NetworkCredential("marketing", "USAT2015")
                _client.Credentials = lo_Credentials
                Dim lo_ResponseBytes As Byte() = _client.UploadValues("http://www.tuproyectodevida.pe/autorespuesta/codigovalidacion.php", "POST", lo_ReqParam)
                Dim lo_ResponseBody As String = (New Text.UTF8Encoding).GetString(lo_ResponseBytes)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class
