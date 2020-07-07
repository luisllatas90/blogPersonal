Imports System.Collections.Generic

Partial Class administrativo_pec_test_frmFinanciarInscripcion
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private mo_RepoAdmision As New ClsAdmision
    Public ms_CodigoPaiPeru As String = "156"
    Private ms_CodigoAlu As String = "0" 'Si este formulario no recibe el código de alumno se generaría un nuevo registro
    Private ms_CodigoTestPreGrado As String = "2"
    Private ms_codigoTest As String = ""
    Private ms_CodigoCco As String = ""
    Private mb_IsModal As Boolean = False
    Private ms_Accion As String = ""
    Private ms_CodigoPso As String = ""
    Dim ValorScript As String = "$(document).ready(function() { jQuery(function($) { "
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'errorMensaje.InnerHtml = "pso: " & Request.Params("pso")
        mb_IsModal = Request.Params("modal")
        ms_codigoTest = Request.Params("test")
        ms_Accion = Request.Params("accion")
        ms_CodigoPso = Request.Params("pso")

        If mb_IsModal Then
            botonesAccion.Attributes.Item("class") = "d-none"
        End If

        If Not String.IsNullOrEmpty(Request.QueryString("alu")) Then ms_CodigoAlu = Request.QueryString("alu")
        If Not String.IsNullOrEmpty(Request.QueryString("cco")) Then ms_CodigoCco = Request.QueryString("cco")
        If Not String.IsNullOrEmpty(Request.QueryString("test")) Then ms_codigoTest = Request.QueryString("test")
        If Not String.IsNullOrEmpty(Request.QueryString("pso")) Then ms_CodigoPso = Request.QueryString("pso")

        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","

        If Not IsPostBack Then
            ' ------------------------------------------------------
            ' Atributos de Cajas de Texto
            ' ------------------------------------------------------
            '1:  ADMINISTRADOR DEL SISTEMA
            '19: DIRECTOR DE PENSIONES
            '20: ASISTENTE DE PENSIONES
            '18: SECRETARIA DE COORDINACIÓN ACADÉMICA
            '124:COORD. ESC. PRE
            '161:CAJA CFO
            '162:ASISTENTE CONSULTORIO JURÍDICO
            '163:COORD. DE SERVICIOS DE INFORMACIÓN - BIBLIOTECA
            '47:BIBLIOTECA
            If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 19 Or _
                Request.QueryString("ctf") = 124 Or Request.QueryString("ctf") = 161 Or _
                Request.QueryString("ctf") = 18 Or Request.QueryString("ctf") = 162 Or _
                Request.QueryString("ctf") = 163 Or Request.QueryString("ctf") = 20 Or _
                Request.QueryString("ctf") = 47 Then
                cambioprecio.Attributes.Item("class") &= ""
            Else
                cambioprecio.Attributes.Item("class") &= " d-none"
            End If

            txtPrecio.Attributes.Item("readonly") = "readonly"
            txtTotal.Attributes.Item("readonly") = "readonly"

            CargaInicial()
            CargarCombos()
            AsignarValoresFormulario(ms_CodigoAlu)
        End If
    End Sub

    Protected Sub DgvDeudas1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvDeudas1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim colIndex As Integer = mo_RepoAdmision.GetColumnIndexByName(e.Row, "fechaVencimiento_Deu")
            Dim fechaVencimiento As Date = _cellsRow.Item(colIndex).Text
            If Date.Now > fechaVencimiento.Date Then
                e.Row.CssClass = "vencida"
            End If
        End If
    End Sub

    Protected Sub cmbServicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServicio.SelectedIndexChanged
        Try
            LlenaDatosItem(Me.cmbServicio.SelectedValue)
            udpStep1.Update()
            udpDeudas.Update()
            udpStep2.Update()
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        End Try

    End Sub

    Protected Sub btnCalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcular.Click
        Try
            'CalculaCuotas(Now, Me.cmbCuotas.SelectedValue, Me.txtSaldo.Text, cmbTipoCuota.SelectedIndex, cmbTipoPeriodo.SelectedIndex)
            CalculaCuotas(Me.dtpFecVencInicial.Text, Me.cmbCuotas.SelectedValue, Me.txtSaldo.Text, cmbTipoCuota.SelectedIndex, cmbTipoPeriodo.SelectedIndex)
            udpStep2.Update()
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        End Try
    End Sub

    Protected Sub btnRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrar.Click
        Guardar()
    End Sub

#End Region

#Region "Metodos"
    Private Sub CargarCombos()

        Dim DatosItems As New Data.DataTable
        Dim DatosPersona As New Data.DataTable

        DatosItems = mo_RepoAdmision.ConsultarServicioPorCeco(ms_CodigoCco)
        DatosPersona = mo_RepoAdmision.ConsultarAlumno(ms_CodigoPso)
        ClsFunciones.LlenarListas(cmbParticipante, DatosPersona, "PK_Alternativo", "DatosPersona", "-- Seleccione Persona --")
        ClsFunciones.LlenarListas(cmbServicio, DatosItems, "codigo_sco", "descripcion_sco", "-- Seleccione Item --")

        If DatosItems.Rows.Count = 1 Then
            Me.cmbServicio.SelectedValue = DatosItems.Rows(0).Item("codigo_sco").ToString
            LlenaDatosItem(DatosItems.Rows(0).Item("codigo_sco"))
        End If

        lblAlumno.Text = DatosPersona.Rows(0).Item("nombres")

        'Seleccionar último código activo del alumno
        Dim tblActivo As New Data.DataTable
        tblActivo = mo_RepoAdmision.RetornaAlumnoActivo(ms_CodigoPso)
        If (tblActivo.Rows.Count > 0) Then
            'HCano 25-05-18 Inicio
            Dim ultimo_codigo As Integer = 0
            For j As Integer = 0 To tblActivo.Rows.Count - 1
                If ultimo_codigo < tblActivo.Rows(j).Item("codigo_alu") Then
                    ultimo_codigo = tblActivo.Rows(j).Item("codigo_alu")
                End If
            Next
            'HCano 25-05-18 Fin

            'Me.DDLPersona.SelectedValue = tblActivo.Rows(0).Item("codigo_alu").ToString
            For i As Integer = 0 To Me.cmbParticipante.Items.Count - 1
                'HCano 25-05-18 Inicio
                'If (Me.DDLPersona.Items(i).Value = "E-" & tblActivo.Rows(0).Item("codigo_alu").ToString) Then
                If (Me.cmbParticipante.Items(i).Value = "E-" & ultimo_codigo.ToString) Then
                    'HCano 25-05-18 Fin
                    Me.cmbParticipante.SelectedIndex = i
                End If
            Next
        Else
            Me.cmbParticipante.SelectedIndex = -1
        End If

        ' CargarComboDepartamento(cmbDepartamento, ms_CodigoPaiPeru)
        '  cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)

        'ClsFunciones.LlenarListas(cmbCicloIngreso, mo_RepoAdmision.ListarProcesoAdmisionV3(), "codigo_Cac", "descripcion_Cac", "-- Seleccione --")
        'CargarComboModalidadIngreso(ms_codigoTest, "", "")

        'CargarComboDepartamento(cmbDepartamentoInstEduc, ms_CodigoPaiPeru)
        'cmbDepartamentoInstEduc_SelectedIndexChanged(Nothing, Nothing)

        'Dim ls_CodigoTestPreGrado As String = "2"
        'ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_RepoAdmision.ListarCarreraProfesional(ls_CodigoTestPreGrado), "codigo_Cpf", "nombre_Cpf", "-- Seleccione --")
    End Sub

    Private Sub CargaInicial()
        Dim tbl, tblbeneficios, tblmodalidad As New Data.DataTable

        Me.dtpFecVencInicial.Text = FormatDateTime(Now, 2)
        Me.txtCuotaInicial.Text = "0.00"
        Me.txtRecargo.Text = "0.00"
        Me.txtDescuento.Text = "0.00"
        Me.txtTotal.Text = "0.00"
        Me.cmbTipoCuota.SelectedIndex = 2
        Me.cmbTipoPeriodo.SelectedIndex = 2
        Me.cmbDiaPago.SelectedValue = Format(Day(Now), "00")

        'Por defecto el item INSCRIPCIONES  CURSOS Y OTROS-ESTUDIANTES
        tbl = mo_RepoAdmision.ConsultarServicioPorNombre("INSCRIPCIONES  CURSOS Y OTROS-ESTUDIANTES", ms_CodigoCco)
        If tbl.Rows.Count > 0 Then
            Me.cmbServicio.SelectedValue = tbl.Rows(0).Item("codigo_Sco")
            LlenaDatosItem(tbl.Rows(0).Item("codigo_Sco"))
        End If

        'Buscar beneficios
        tblbeneficios = mo_RepoAdmision.ConsultarBeneficiosPostulacionAlumno(ms_CodigoAlu)
        tblmodalidad = mo_RepoAdmision.ConsultarModalidadIngreso("NM", "CONVENIOS")

        'Cambios en datos del Cargo
        For i As Integer = 0 To tblbeneficios.Rows.Count() - 1
            If tblbeneficios.Rows(i).Item("descripcion_bp") = "Beca - Pre" Or _
                tblbeneficios.Rows(i).Item("descripcion_bp") = "Examen Gratuito" Or _
                (Request.QueryString("codigo_min") = tblmodalidad.Rows(0).Item("codigo_Min") And _
                 tblbeneficios.Rows(i).Item("descripcion_bp") = "Ingreso Directo") Then
                'Modificar Cargos
                txtDescuento.Text = txtPrecio.Text
            End If
        Next

        Dim DatosCentroCosto As New Data.DataTable
        DatosCentroCosto = mo_RepoAdmision.ConsultarEventos("0", ms_CodigoCco, "")
        Me.txtCuotaInicial.Text = IIf(DatosCentroCosto.Rows(0).Item("montocuotainicial_dev").Equals(System.DBNull.Value), "", DatosCentroCosto.Rows(0).Item("montocuotainicial_dev").ToString)

        'Mes 1ra cuota
        Dim fecha As DateTime
        Dim con As Int16 = 0

        Me.cmbMesCuota.Items.Clear()
        fecha = "01/" & Now.Month & "/" & Now.Year
        For i As Int16 = Now.Month To Now.Month + 17
            con = con + 1
            Me.cmbMesCuota.Items.Add(Meses(fecha.Month) & " - " & fecha.Year)
            Me.cmbMesCuota.Items(con - 1).Value = fecha
            fecha = DateAdd(DateInterval.Month, 1, fecha)
        Next

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
        'If ls_CodigoTest = "1" Then
        '    ClsFunciones.LlenarListas(cmbModalidadIngreso, mo_RepoAdmision.ListarModalidadIngreso("77", ls_CodigoTest, ls_CodigoCac, ls_CodigoPpf), "codigo_Min", "nombre_Min", "-- Seleccione --")
        'Else
        '    ClsFunciones.LlenarListas(cmbModalidadIngreso1, mo_RepoAdmision.ListarModalidadIngreso("7", ls_CodigoTest, 0, 0), "codigo_Min", "nombre_Min", "-- Seleccione --")
        'End If
    End Sub

    Private Sub LlenaDatosItem(ByVal ValorSeleccionado As Integer)
        Try
            If ValorSeleccionado = "-1" Then
                txtPrecio.Text = "0.00"
                txtRecargo.Text = "0.00"
                txtDescuento.Text = "0.00"
                txtDescuento.Text = "0.00"
                txtTotal.Text = "0.00"
            Else
                Dim DatosItem As New Data.DataTable
                Dim DatosDeudas As New Data.DataTable

                DatosItem = mo_RepoAdmision.ConsultarDatosServicio(ms_CodigoCco, ValorSeleccionado)

                'andy.diaz  04/07/2019  Ahora muestro siempre las deudas pendientes, vigentes o vencidas
                'DatosDeudas = mo_RepoAdmision.ConsultarDeudasPersonaxCco(ms_CodigoCco, ms_CodigoPso)

                'If DatosDeudas.Rows.Count > 0 Then
                '    Me.DgvDeudas1.DataSource = DatosDeudas
                '    Me.DgvDeudas1.DataBind()
                '    Me.divDatosDeudas.Visible = True
                'Else
                '    DatosDeudas = mo_RepoAdmision.ConsultarDeudasPendientesPersona(ms_CodigoPso, ms_codigoTest)
                '    If DatosDeudas.Rows.Count > 0 Then
                '        Me.DgvDeudas1.DataSource = DatosDeudas
                '        Me.DgvDeudas1.DataBind()
                '        Me.divDatosDeudas.Visible = True
                '    Else
                '        DatosDeudas = mo_RepoAdmision.ConsultarDeudas(ms_CodigoPso, ms_CodigoCco, ValorSeleccionado)
                '        If DatosDeudas.Rows.Count > 0 Then
                '            Me.DgvDeudas.DataSource = DatosDeudas
                '            Me.DgvDeudas.DataBind()
                '            Me.divDatosDeudas.Visible = True
                '        End If
                '    End If
                'End If

                DatosDeudas = mo_RepoAdmision.ConsultarDeudasPendientesPersona(ms_CodigoPso, ms_codigoTest)
                Me.DgvDeudas1.DataSource = DatosDeudas
                Me.DgvDeudas1.DataBind()

                If DatosDeudas.Rows.Count > 0 Then
                    Me.divDatosDeudas.Visible = True
                End If

                If DatosItem.Rows.Count > 0 Then
                    ' Me.TabFormaPago.enabled = True

                    With DatosItem
                        Dim Precio As Double

                        If .Rows(0).Item("precio_sco").Equals(System.DBNull.Value) = True Then
                            Precio = 0.0
                        Else
                            Precio = .Rows(0).Item("precio_sco").ToString
                        End If

                        '' **************** VALIDACIÓN CÁLCULO DE PRECIO ESCUELA PRE ******************************

                        '------ Hcano : Comentado, Precio de Examen de Admision Reemplazado por valor devuelto de las funciones.

                        'If Request.QueryString("mod") = 1 And Request.QueryString("tcl") = "E" Then
                        '    Dim ArrayDevuelto(1) As Object---
                        '    Try
                        '        ObjDatosItem.AbrirConexion()
                        '        If Me.DDLItem.SelectedValue = Me.CodigoItemExamAdmision Then
                        '            ObjDatosItem.Ejecutar("CalcularPensionPostulante", Request.QueryString("cli"), 0).CopyTo(ArrayDevuelto, 0)
                        '            Precio = ArrayDevuelto(0)
                        '        Else
                        '            If Me.DDLItem.SelectedValue = Me.CodigoItemEscuelaPre Then
                        '                ObjDatosItem.Ejecutar("CalcularPensionAlumnoPre", Request.QueryString("cli"), 0).CopyTo(ArrayDevuelto, 0)
                        '                Precio = ArrayDevuelto(0)
                        '            End If
                        '        End If
                        '        ObjDatosItem.CerrarConexion()
                        '    Catch ex As Exception
                        '        Precio = 0
                        '        ObjDatosItem.CerrarConexion()
                        '    End Try
                        'End If
                        '------ 
                        '' **************** VALIDACIÓN CÁLCULO DE PRECIO ESCUELA PRE ******************************

                        Me.txtPrecio.Text = IIf(Precio = 0, "0.00", FormatNumber(Precio, 2, TriState.False, TriState.False, TriState.False))
                        Me.txtTotal.Text = IIf(Precio = 0, "0.00", FormatNumber(Precio, 2, TriState.False, TriState.False, TriState.False))
                        'Me.TxtTotalPagarMuestra.Text = IIf(Precio = 0, "0.00", Precio)

                        Me.txtDescuento.Text = "0.00"
                        Me.txtRecargo.Text = "0.00"
                        Me.txtSaldo.Text = FormatNumber(Val(Me.txtTotal.Text) - Val(Me.txtCuotaInicial.Text), 2, TriState.False, TriState.False, TriState.False)

                        'If CBool(.Rows(0).Item("generaMora_sco")) = True Then
                        '    Me.dtpFecVenc.Text = CDate(.Rows(0).Item("fechaVencimiento_sco")).ToShortDateString
                        '    Me.dtpFecVenc.Enabled = True
                        'Else
                        '    Me.dtpFecVenc.Text = Now.ToShortDateString
                        '    Me.dtpFecVenc.Enabled = False
                        'End If

                        Me.dtpFecVenc.Text = CDate(.Rows(0).Item("fechaVencimiento_sco")).ToShortDateString
                        Me.dtpFecVenc.Enabled = True

                        'If CBool(.Rows(0).Item("agrupaPension_Sco")) = False Then
                        '    Me.DDLAgruparPension.Enabled = False
                        '    Me.ValidaAgrupa.Enabled = False

                        '    Me.DDLNumPartes.Items.Clear()
                        '    Me.DDLNumPartes.Items.Add("-- Seleccione Cuotas --")
                        '    Me.DDLNumPartes.Items(0).Value = -1
                        '    For i As Int16 = 1 To 1
                        '        Me.DDLNumPartes.Items.Add(i)
                        '    Next
                        '    Me.DDLNumPartes.SelectedValue = 1
                        'Else
                        '    Me.DDLNumPartes.Items.Clear()
                        '    Me.DDLNumPartes.Items.Add("-- Seleccione Cuotas --")
                        '    Me.DDLNumPartes.Items(0).Value = -1
                        '    For i As Int16 = 1 To 4
                        '        Me.DDLNumPartes.Items.Add(i)
                        '    Next
                        '    Me.DDLAgruparPension.Enabled = True
                        '    Me.ValidaAgrupa.Enabled = True

                        'End If

                        LlenaCombosCuotas(.Rows(0).Item("nroPartes_sco"))

                        'Agregado por mvillavicencio 10/08/12
                        txtCuota.Text = FormatNumber(txtTotal.Text, 2, TriState.False, TriState.False, TriState.False)
                        'CalculaCuotas(Now, Me.cmbCuotas.SelectedValue, Me.txtSaldo.Text, cmbTipoCuota.SelectedIndex, cmbTipoPeriodo.SelectedIndex)
                        CalculaCuotas(Me.dtpFecVencInicial.Text, Me.cmbCuotas.SelectedValue, Me.txtSaldo.Text, cmbTipoCuota.SelectedIndex, cmbTipoPeriodo.SelectedIndex)
                        udpStep2.Update()
                    End With

                    If Request.QueryString("mod") = 1 Then 'E-pre
                        '    Me.DDLAgruparPension.SelectedIndex = 0
                    End If
                Else
                    'Me.TabFormaPago.enabled = False
                End If
            End If
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        End Try

    End Sub

    Private Sub CalculaCuotas(ByVal fecha As Date, ByVal Numcuotas As Integer, ByVal Total As Double, ByVal CuotaFija As Integer, ByVal PerFijo As Integer)

        TblCuotas.Rows.Clear()
        Me.divCuotas.Attributes.Item("class") = "col-sm-6"
        ' TblCuotas.Attributes.Item("class") = "table table-sm"
        'If chkUnaCuota.Checked = True Then
        '    CmdGuardar.Enabled = True
        '    Exit Sub
        'Else
        '    If Me.DDLNumCuotas.SelectedValue = -1 Then
        '        Exit Sub
        '    End If
        'End If

        Me.lblMensaje.Text = ""
        Dim MontoMensual As Double
        Dim Pago As Double
        Dim fechaini As Date

        'fechaini = Now.ToShortDateString
        fechaini = fecha

        Pago = 0
        MontoMensual = FormatNumber((Total / Numcuotas), 2)
        fechaini = Me.cmbDiaPago.SelectedValue & Right(Me.cmbMesCuota.SelectedValue, 8)

        If PerFijo = 1 Then
            If DateDiff(DateInterval.Day, fechaini, CDate(Now.ToShortDateString)) > 0 Then
                Me.lblMensaje.Text = "La fecha de Inicio de 1era Cuota no debe ser menor a la actual"
                Me.lblMensaje.ForeColor = Drawing.Color.Red
                Me.TblCuotas.Rows.Clear()
                Exit Sub
            End If

            If DateDiff(DateInterval.Day, fechaini, CDate(Me.dtpFecVencInicial.Text)) >= 0 Then
                Me.lblMensaje.Text = "La Fecha de Pago de 1era Cuota no debe ser menor o igual a la Fecha de Vencimiento de la Cuota Inicial"
                Me.lblMensaje.ForeColor = Drawing.Color.Red
                Me.TblCuotas.Rows.Clear()
                Exit Sub
            End If

        End If

        Dim FilaCabecera As New TableRow
        Dim Celda1 As New TableCell
        Dim Celda2 As New TableCell
        Dim Celda3 As New TableCell
        Dim lblCelda2 As New Label
        Dim lblCelda3 As New Label

        lblCelda2.Text = "Fecha Vcto"
        'lblCelda2.CssClass = "col-form-label form-control-sm"
        lblCelda2.Font.Size = "10"
        Celda2.Controls.Add(lblCelda2)

        lblCelda3.Text = "Monto"
        ' lblCelda3.CssClass = "col-form-label form-control-sm"
        lblCelda3.Font.Size = "10"
        Celda3.Controls.Add(lblCelda3)

        FilaCabecera.Cells.Add(Celda1)
        FilaCabecera.Cells.Add(Celda2)
        FilaCabecera.Cells.Add(Celda3)

        Me.TblCuotas.Rows.Add(FilaCabecera)

        For i As Integer = 1 To Numcuotas
            Dim CajaFechas As New TextBox
            Dim CajaMontos As New TextBox
            Dim Filas As New TableRow
            Dim lblCelda1 As New Label

            Dim Col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Col3 As New TableCell

            Dim ValiReq_Fecha As New RequiredFieldValidator
            Dim ValiReq_Fecha2 As New RangeValidator
            Dim ValiRerMonto As New RequiredFieldValidator
            Dim ValiRerMonto2 As New RangeValidator

            CajaFechas.ID = "FechaCuota_" & i.ToString
            'CajaFechas.Width = 80
            'CajaMontos.Width = 70
            CajaMontos.ID = "Monto_" & i.ToString

            'CajaFechas.SkinID = "CajaTextoObligatorio"
            'CajaMontos.SkinID = "CajaTextoObligatorio"
            CajaFechas.CssClass = "col-sm-8 form-control form-control-sm"
            CajaMontos.CssClass = "col-sm-8 form-control form-control-sm"
            CajaMontos.Attributes.CssStyle.Add("text-align", "right")

            ' --------------------------------------------------------
            ' Colocando los Validadores
            ' --------------------------------------------------------
            'ValiReq_Fecha.ControlToValidate = CajaFechas.ClientID
            'ValiReq_Fecha.ErrorMessage = "Ingrese una Fecha en cuota " & i.ToString
            'ValiReq_Fecha.Text = "*"
            'ValiReq_Fecha.ValidationGroup = "Guardar"
            'ValiReq_Fecha.SetFocusOnError = True

            'ValiRerMonto.ControlToValidate = CajaMontos.ClientID
            'ValiRerMonto.ErrorMessage = "Ingrese un monto correcto en cuota " & i.ToString
            'ValiRerMonto.Text = "*"
            'ValiRerMonto.ValidationGroup = "Guardar"
            'ValiRerMonto.SetFocusOnError = True

            'ValiReq_Fecha2.ControlToValidate = CajaFechas.ClientID
            'ValiReq_Fecha2.ErrorMessage = "Fecha de cuota " & i.ToString & " Incorrecta"
            'ValiReq_Fecha2.MinimumValue = "01/08/2010"
            'ValiReq_Fecha2.MaximumValue = "01/01/2050"
            'ValiReq_Fecha2.Type = ValidationDataType.Date
            'ValiReq_Fecha2.Text = "*"
            'ValiReq_Fecha2.ValidationGroup = "Guardar"
            'ValiReq_Fecha2.SetFocusOnError = True

            'ValiRerMonto2.ControlToValidate = CajaMontos.ClientID
            'ValiRerMonto2.ErrorMessage = "Monto de cuota " & i.ToString & " incorrecto"
            'ValiRerMonto2.Text = "*"
            'ValiRerMonto2.MinimumValue = 0
            'ValiRerMonto2.MaximumValue = 100000
            'ValiRerMonto2.Type = ValidationDataType.Double
            'ValiRerMonto2.ValidationGroup = "Guardar"
            'ValiRerMonto2.SetFocusOnError = True


            ' -------------------------------------------------------------
            ' Colocando los datos de Fecha, y Cuotas
            ' -------------------------------------------------------------
            If CuotaFija = 1 Then
                If (i = Numcuotas) Then
                    CajaMontos.Text = FormatNumber(Total - Pago, 2, TriState.False, TriState.False, TriState.False) 'FormatNumber(Total - Pago, 2)
                Else
                    CajaMontos.Text = FormatNumber(MontoMensual, 2, TriState.False, TriState.False, TriState.False) 'MontoMensual 
                End If
                'CajaMontos.Attributes.Add("OnKeyPress", "javascript:return validardecimal(event);")
                Pago = Pago + MontoMensual
                CajaMontos.Enabled = False
            End If

            If PerFijo = 1 Then
                If fechaini.Month <> 2 Then
                    fechaini = Me.cmbDiaPago.SelectedValue & "/" & fechaini.Month & "/" & fechaini.Year
                End If
                CajaFechas.Text = FormatDateTime(fechaini, 2)
                'CajaFechas.Attributes.Add("OnKeyPress", "javascript:return false;")
                fechaini = DateAdd(DateInterval.Month, 1, fechaini)
                CajaFechas.Enabled = False
            End If

            ' ---------------------------------------------------------------------------
            ' Agregado por mvillavicencio 09/05/2012
            ' Poniendo la fecha actual como fecha de vencimiento, y el total, como monto cuota
            'Cuando el periodo y monto son variables, y la cuota es una,
            ' --------------------------------------------------------------------------
            If Numcuotas = 1 And cmbTipoPeriodo.SelectedIndex = 2 And cmbTipoCuota.SelectedIndex = 2 Then
                'fechaini = Now
                CajaFechas.Text = FormatDateTime(fechaini, 2)
                'CajaFechas.Attributes.Add("OnKeyPress", "javascript:return false;")
                CajaMontos.Text = FormatNumber(Total, 2, TriState.False, TriState.False, TriState.False)
                'CajaMontos.Attributes.Add("OnKeyPress", "javascript:return validardecimal(event);")
                'TxtFecVctoInicial.Text = DateTime.Now.AddDays(-1)
                If dtpFecVencInicial.Text = "" Then
                    dtpFecVencInicial.Text = FormatDateTime(DateTime.Now, 2)
                End If
                CajaMontos.Enabled = False
                CajaFechas.Enabled = False
            End If
            CajaMontos.Attributes.Add("OnKeyPress", "javascript:return validardecimal(event);")
            CajaMontos.Attributes.Add("OnKeyUp", "javascript:sumarcuotas()")
            CajaMontos.Attributes.Add("FocusOut", "javascript:darformato(this)")
            lblCelda1.Text = i.ToString
            lblCelda1.CssClass = "col-form-label form-control-sm"
            lblCelda1.Font.Size = "10"
            Col1.Controls.Add(lblCelda1)

            Col2.Controls.Add(CajaFechas)
            'Col2.Controls.Add(ValiReq_Fecha)
            'Col2.Controls.Add(ValiReq_Fecha2)
            Col3.Controls.Add(CajaMontos)
            'Col3.Controls.Add(ValiRerMonto)
            'Col3.Controls.Add(ValiRerMonto2)


            Filas.Cells.Add(Col1)
            Filas.Cells.Add(Col2)
            Filas.Cells.Add(Col3)
            Me.TblCuotas.Rows.Add(Filas)
            'ValorScript = ValorScript & GeneraMascara(CajaFechas.ClientID)
        Next

        ' ---------------------------------------------------------------------------
        ' Agregando una Fila de total para sumar todas las cuotas, validacion previa
        ' si existen cuotas asignadas
        ' --------------------------------------------------------------------------
        If Numcuotas > 0 Then
            Dim TxtTotalCuotas As New TextBox
            Dim ColF1 As New TableCell
            Dim ColF2 As New TableCell
            Dim colF3 As New TableCell
            Dim FilaTotal As New TableRow
            Dim lblTotal As New Label

            lblTotal.Text = "Total"
            'lblTotal.CssClass = "col-form-label form-control-sm"
            lblTotal.Font.Size = "10"
            ColF2.Controls.Add(lblTotal)
            TxtTotalCuotas.ID = "TxtTotalCuotas"
            'TxtTotalCuotas.Attributes.Add("OnKeyPress", "javascript:return false;")
            'TxtTotalCuotas.SkinID = "CajaTextoSinMarco"
            TxtTotalCuotas.CssClass = "col-sm-8 form-control form-control-sm"
            TxtTotalCuotas.Enabled = False
            'Agregando que cuando sea cuota variable y num de cuota 1, ponga el total
            If CuotaFija = 1 Or (CuotaFija = 2 And cmbCuotas.SelectedValue = 1) Then
                TxtTotalCuotas.Text = FormatNumber(Total, 2, TriState.False, TriState.False, TriState.False)
            Else
                TxtTotalCuotas.Text = "0.00"
            End If

            colF3.Controls.Add(TxtTotalCuotas)
            TxtTotalCuotas.Attributes.CssStyle.Add("text-align", "right")
            FilaTotal.Controls.Add(ColF1)
            FilaTotal.Controls.Add(ColF2)
            FilaTotal.Controls.Add(colF3)

            Me.TblCuotas.Rows.Add(FilaTotal)
            'Me.HddNumCuotas.Value = Numcuotas

            'ClientScript.RegisterStartupScript(Me.GetType, "CargaMascara", "<script type='text/javascript' language='javascript'>" & ValorScript + " }) ; }) " & "</script>", False)
            'Me.CmdGuardar.Enabled = True
        End If
        Session.Item("TblCuotas") = TblCuotas
    End Sub

    Private Sub LlenaCombosCuotas(ByVal numcuotas As Integer)
        Me.cmbCuotas.Items.Clear()
        Me.cmbCuotas.Items.Add("-- Cuotas --")
        Me.cmbCuotas.Items(0).Value = -1

        If numcuotas > 0 Then 'agregado por mvillavicencio. Salia error cuando num cuotas = 0 10/08/12
            For i As Int16 = 1 To numcuotas
                Me.cmbCuotas.Items.Add(i)
            Next
        End If

        Me.cmbCuotas.SelectedValue = 1
    End Sub

    Private Function Meses(ByVal Mes As Integer) As String
        Dim MesRetorno As String
        MesRetorno = ""
        Select Case Mes
            Case 1 : MesRetorno = "Enero"
            Case 2 : MesRetorno = "Febrero"
            Case 3 : MesRetorno = "Marzo"
            Case 4 : MesRetorno = "Abril"
            Case 5 : MesRetorno = "Mayo"
            Case 6 : MesRetorno = "Junio"
            Case 7 : MesRetorno = "Julio"
            Case 8 : MesRetorno = "Agosto"
            Case 9 : MesRetorno = "Setiembre"
            Case 10 : MesRetorno = "Octubre"
            Case 11 : MesRetorno = "Noviembre"
            Case 12 : MesRetorno = "Diciembre"
        End Select
        Return MesRetorno
    End Function

    'Private Function GeneraMascara(ByVal nombrecontrol As String) As String
    '    Return "$('#" & nombrecontrol & "').mask('99/99/9999');"

    'End Function

    'Private Sub CargarComboInstitucionEducativa()
    '    Try
    '        Dim lo_DataSource As Data.DataTable = mo_RepoAdmision.ListarInstitucionEducativa("DEP", cmbDepartamentoInstEduc.SelectedValue)
    '        ClsFunciones.LlenarListas(cmbInstitucionEducativa, lo_DataSource, "codigo_Ied", "nombre_Ied", "-- Seleccione --")

    '        Dim dr() As System.Data.DataRow
    '        For Each item As ListItem In cmbInstitucionEducativa.Items
    '            dr = lo_DataSource.Select("codigo_Ied='" & item.Value & "'")
    '            If dr.Length > 0 Then
    '                item.Attributes.Add("data-tokens", dr(0).Item("Nombre_ied").ToString)
    '                item.Attributes.Add("data-subtext", dr(0).Item("Direccion_ied").ToString)
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub ObtenerDatosPersonales(ByVal ls_DNI As String)
        If Not String.IsNullOrEmpty(ls_DNI) Then
            Dim ls_Tipo As String = "DNIE" 'Busco por DNI
            Dim lo_DtRespuesta As Data.DataTable = mo_RepoAdmision.ObtenerDatosPersonales(ls_Tipo, ls_DNI)
            If lo_DtRespuesta.Rows.Count > 0 Then
                'Dim lo_DrPersona As Data.DataRow = lo_DtRespuesta.Rows(0)
                'txtApellidoPaterno.Text = lo_DrPersona.Item("apellidoPaterno")
                'txtApellidoMaterno.Text = lo_DrPersona.Item("apellidoMaterno")
                'txtNombres.Text = lo_DrPersona.Item("nombres")
                'dtpFecNacimiento.Text = lo_DrPersona.Item("fechaNacimiento")
                'cmbSexo.SelectedValue = lo_DrPersona.Item("sexo")
                'txtNumCelular.Text = lo_DrPersona.Item("telefonoCelular")
                'txtNumFijo.Text = lo_DrPersona.Item("telefonoFijo")
                'txtEmail.Text = lo_DrPersona.Item("emailPrincipal")
                'cmbDepartamento.SelectedValue = lo_DrPersona.Item("codigoDep") : cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
                'cmbProvincia.SelectedValue = lo_DrPersona.Item("codigoPro") : cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
                'cmbDistrito.SelectedValue = lo_DrPersona.Item("codigoDis")
                'txtDireccion.Text = lo_DrPersona.Item("direccion")
                'cmbDepartamentoInstEduc.SelectedValue = lo_DrPersona.Item("codigoDep") : cmbDepartamentoInstEduc_SelectedIndexChanged(Nothing, Nothing)
                'Dim ln_CodigoIed As Integer
                'If Integer.TryParse(lo_DrPersona.Item("codigoIed"), ln_CodigoIed) Then
                '    If ln_CodigoIed > 0 Then
                '        cmbInstitucionEducativa.SelectedValue = ln_CodigoIed
                '    End If
                'End If
                'Dim ln_CodigoCpf As Integer
                'If Integer.TryParse(lo_DrPersona.Item("codigoCpf"), ln_CodigoCpf) Then
                '    If ln_CodigoCpf > 0 Then
                '        cmbCarreraProfesional.SelectedValue = ln_CodigoCpf : cmbCarreraProfesional_SelectedIndexChanged(Nothing, Nothing)
                '    End If
                'End If
            End If
        End If
    End Sub

    Private Sub AsignarValoresFormulario(ByVal ls_CodigoAlu As String)
        Try
            Dim lo_DrCentroCosto As Data.DataRow = mo_RepoAdmision.ObtenerCentroCosto(ms_CodigoCco)
            lblCentroCosto.InnerHtml = lo_DrCentroCosto.Item("descripcion_Cco")

            'If ls_CodigoAlu <> 0 Then
            '    Dim ls_TipoConsulta As String = "I"
            '    Dim lo_DtInscripcion As Data.DataTable = mo_RepoAdmision.ObtenerDatosInscripcion(ls_CodigoAlu, ls_TipoConsulta)

            '    If lo_DtInscripcion.Rows.Count > 0 Then
            '        Dim lo_Row As Data.DataRow = lo_DtInscripcion.Rows(0)
            '        txtDNI.Text = lo_Row.Item("numeroDocIdent_Pso")
            '        txtApellidoPaterno.Text = lo_Row.Item("apellidoPaterno_Pso")
            '        txtApellidoMaterno.Text = lo_Row.Item("apellidoMaterno_Pso")
            '        txtNombres.Text = lo_Row.Item("nombres_Pso")
            '        dtpFecNacimiento.Text = lo_Row.Item("fechaNacimiento_Pso")
            '        cmbSexo.SelectedValue = lo_Row.Item("sexo_Pso")
            '        txtNumCelular.Text = lo_Row.Item("telefonoCelular_Pso")
            '        txtNumFijo.Text = lo_Row.Item("telefonoFijo_Pso")
            '        txtEmail.Text = lo_Row.Item("emailPrincipal_Pso")
            '        cmbDepartamento.SelectedValue = lo_Row.Item("codigo_Dep") : cmbDepartamento_SelectedIndexChanged(cmbDepartamento, EventArgs.Empty)
            '        cmbProvincia.SelectedValue = lo_Row.Item("codigo_Pro") : cmbProvincia_SelectedIndexChanged(cmbProvincia, EventArgs.Empty)
            '        cmbDistrito.SelectedValue = lo_Row.Item("codigo_Dis")
            '        txtDireccion.Text = lo_Row.Item("direccion_Pso")
            '        cmbDepartamentoInstEduc.SelectedValue = lo_Row.Item("codigo_DepIe") : cmbDepartamentoInstEduc_SelectedIndexChanged(cmbDepartamentoInstEduc, EventArgs.Empty)
            '        cmbInstitucionEducativa.SelectedValue = lo_Row.Item("codigo_ied")
            '        cmbCicloIngreso.SelectedValue = lo_Row.Item("codigo_Cac") : cmbCicloIngreso_SelectedIndexChanged(cmbCicloIngreso, EventArgs.Empty)
            '        cmbCarreraProfesional.SelectedValue = lo_Row.Item("tempcodigo_cpf") : cmbCarreraProfesional_SelectedIndexChanged(cmbCarreraProfesional, EventArgs.Empty)
            '        If ms_codigoTest = "1" Then
            '            cmbModalidadIngreso.SelectedValue = lo_Row.Item("codigo_Min")
            '        Else
            '            cmbModalidadIngreso1.SelectedValue = lo_Row.Item("codigo_Min")
            '        End If
            '        cmbEstadoCivil.SelectedValue = lo_Row.Item("estadoCivil_Dal")
            '        txtCargoActual.Text = lo_Row.Item("cargoActual_Dal")
            '        txtCentroLabores.Text = lo_Row.Item("centroTrabajo_Dal")
            '        txtRuc.Text = lo_Row.Item("nroRuc_Pso")
            '        txtEmail2.Text = lo_Row.Item("emailAlternativo_Pso")
            '        errorMensaje.InnerHtml = lo_Row.Item("codigo_DepIe")
            '    Else
            '        txtDNI.Text = ""
            '        txtApellidoPaterno.Text = ""
            '        txtApellidoMaterno.Text = ""
            '        txtNombres.Text = ""
            '        dtpFecNacimiento.Text = ""
            '        cmbSexo.SelectedValue = ""
            '        txtNumCelular.Text = ""
            '        txtNumFijo.Text = ""
            '        txtEmail.Text = ""
            '        cmbDepartamento.SelectedValue = "" : cmbDepartamento_SelectedIndexChanged(cmbDepartamento, EventArgs.Empty)
            '        cmbProvincia.SelectedValue = "" : cmbProvincia_SelectedIndexChanged(cmbProvincia, EventArgs.Empty)
            '        cmbDistrito.SelectedValue = ""
            '        txtDireccion.Text = ""
            '        cmbDepartamentoInstEduc.SelectedValue = "" : cmbDepartamentoInstEduc_SelectedIndexChanged(cmbDepartamentoInstEduc, EventArgs.Empty)
            '        cmbInstitucionEducativa.SelectedValue = ""
            '        cmbCicloIngreso.SelectedValue = "" : cmbCicloIngreso_SelectedIndexChanged(cmbCicloIngreso, EventArgs.Empty)
            '        cmbCarreraProfesional.SelectedValue = "" : cmbCarreraProfesional_SelectedIndexChanged(cmbCarreraProfesional, EventArgs.Empty)
            '        cmbModalidadIngreso.SelectedValue = ""
            '        cmbEstadoCivil.SelectedValue = ""
            '        txtCargoActual.Text = ""
            '        txtCentroLabores.Text = ""
            '        txtRuc.Text = ""
            '        txtEmail2.Text = ""
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ValidarFormulario() As Dictionary(Of String, String)
        Dim lo_Validacion As New Dictionary(Of String, String)
        lo_Validacion.Add("rpta", 1)
        lo_Validacion.Add("msg", "")
        lo_Validacion.Add("control", "")

        Dim lb_Errores As Boolean = False

        If chkUnaCuota.Checked Then
            If String.IsNullOrEmpty(Me.dtpFecVenc.Text.Trim) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Debe ingresar una fecha de vencimiento para la cuota"
                    lo_Validacion.Item("control") = "dtpFecVenc"
                    lb_Errores = True
                End If
                dtpFecVenc.Attributes.Item("data-error") = "true"
            Else
                dtpFecVenc.Attributes.Item("data-error") = "false"
            End If
        Else
            If String.IsNullOrEmpty(Me.txtCuotaInicial.Text.Trim) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Debe ingresar valor de la cuota inicial"
                    lo_Validacion.Item("control") = "txtCuotaInicial"
                    lb_Errores = True
                End If
                txtCuotaInicial.Attributes.Item("data-error") = "true"
            Else
                txtCuotaInicial.Attributes.Item("data-error") = "false"
            End If

            If String.IsNullOrEmpty(Me.dtpFecVencInicial.Text.Trim) Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = 0
                    lo_Validacion.Item("msg") = "Debe ingresar una fecha de vencimiento para la cuota"
                    lo_Validacion.Item("control") = "dtpFecVencInicial"
                    lb_Errores = True
                End If
                dtpFecVencInicial.Attributes.Item("data-error") = "true"
            Else
                dtpFecVencInicial.Attributes.Item("data-error") = "false"
            End If

            If cmbCuotas.SelectedValue < 0 Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar cantidad de cuotas"
                    lo_Validacion.Item("control") = "cmbCuotas"
                    lb_Errores = True
                End If
                cmbCuotas.Attributes.Item("data-error") = "true"
            Else
                cmbCuotas.Attributes.Item("data-error") = "false"
            End If

            If cmbTipoCuota.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar tipo de cuota"
                    lo_Validacion.Item("control") = "cmbTipoCuota"
                    lb_Errores = True
                End If
                cmbTipoCuota.Attributes.Item("data-error") = "true"
            Else
                cmbTipoCuota.Attributes.Item("data-error") = "false"
            End If

            If cmbTipoPeriodo.SelectedValue = "-1" Then
                If Not lb_Errores Then
                    lo_Validacion.Item("rpta") = "0"
                    lo_Validacion.Item("msg") = "Debe seleccionar tipo de periodo"
                    lo_Validacion.Item("control") = "cmbTipoPeriodo"
                    lb_Errores = True
                End If
                cmbTipoPeriodo.Attributes.Item("data-error") = "true"
            Else
                cmbTipoPeriodo.Attributes.Item("data-error") = "false"
            End If
        End If

        'Dim ls_FormatDNI As String = "^\d{8}$"
        'If Not Regex.IsMatch(txtDNI.Text.Trim, ls_FormatDNI) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = "0"
        '        lo_Validacion.Item("msg") = "El DNI ingresado no es correcto"
        '        lo_Validacion.Item("control") = "txtEmail"
        '        lb_Errores = True
        '    End If
        '    txtDNI.Attributes.Item("data-error") = "true"
        'Else
        '    txtDNI.Attributes.Item("data-error") = "false"
        'End If

        'If String.IsNullOrEmpty(txtApellidoPaterno.Text.Trim) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Debe ingresar su apellido paterno"
        '        lo_Validacion.Item("control") = "txtApellidoPaterno"
        '        lb_Errores = True
        '    End If
        '    txtApellidoPaterno.Attributes.Item("data-error") = "true"
        'Else
        '    txtApellidoPaterno.Attributes.Item("data-error") = "false"
        'End If

        'If String.IsNullOrEmpty(txtApellidoMaterno.Text.Trim) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Debe ingresar su apellido materno"
        '        lo_Validacion.Item("control") = "txtApellidoMaterno"
        '        lb_Errores = True
        '    End If
        '    txtApellidoMaterno.Attributes.Item("data-error") = "true"
        'Else
        '    txtApellidoMaterno.Attributes.Item("data-error") = "false"
        'End If

        'If String.IsNullOrEmpty(txtNombres.Text.Trim) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Debe ingresar sus nombres"
        '        lo_Validacion.Item("control") = "txtNombres"
        '        lb_Errores = True
        '    End If
        '    txtNombres.Attributes.Item("data-error") = "true"
        'Else
        '    txtNombres.Attributes.Item("data-error") = "false"
        'End If

        'If String.IsNullOrEmpty(txtEmail.Text.Trim) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Debe ingresar un correo electrónico"
        '        lo_Validacion.Item("control") = "txtEmail"
        '        lb_Errores = True
        '    End If
        '    txtEmail.Attributes.Item("data-error") = "true"
        '    Return lo_Validacion
        'Else
        '    txtEmail.Attributes.Item("data-error") = "false"
        'End If

        'Dim ls_FormatEmail As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        'If Not Regex.IsMatch(txtEmail.Text.Trim, ls_FormatEmail) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = "0"
        '        lo_Validacion.Item("msg") = "El correo electrónico no es válido"
        '        lo_Validacion.Item("control") = "txtEmail"
        '        lb_Errores = True
        '    End If
        '    txtEmail.Attributes.Item("data-error") = "true"
        'Else
        '    txtEmail.Attributes.Item("data-error") = "false"
        'End If

        'If cmbDistrito.SelectedValue < 0 Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = "0"
        '        lo_Validacion.Item("msg") = "Debe seleccionar un distrito"
        '        lo_Validacion.Item("control") = "cmbDistrito"
        '        lb_Errores = True
        '    End If
        '    cmbDistrito.Attributes.Item("data-error") = "true"
        'Else
        '    cmbDistrito.Attributes.Item("data-error") = "false"
        'End If

        'If String.IsNullOrEmpty(txtDireccion.Text.Trim) Then
        '    If Not lb_Errores Then
        '        lo_Validacion.Item("rpta") = 0
        '        lo_Validacion.Item("msg") = "Debe ingresar una dirección"
        '        lo_Validacion.Item("control") = "txtDireccion"
        '        lb_Errores = True
        '    End If
        '    txtDireccion.Attributes.Item("data-error") = "true"
        'Else
        '    txtDireccion.Attributes.Item("data-error") = "false"
        'End If

        'If ms_codigoTest <> 1 Then
        '    If cmbEstadoCivil.SelectedValue = "-1" Then
        '        If Not lb_Errores Then
        '            lo_Validacion.Item("rpta") = "0"
        '            lo_Validacion.Item("msg") = "Debe seleccionar estado civil"
        '            lo_Validacion.Item("control") = "cmbEstadoCivil"
        '            lb_Errores = True
        '        End If
        '        cmbEstadoCivil.Attributes.Item("data-error") = "true"
        '    Else
        '        cmbEstadoCivil.Attributes.Item("data-error") = "false"
        '    End If

        '    If Not String.IsNullOrEmpty(txtEmail2.Text.Trim) Then
        '        Dim ls_FormatEmail2 As String = "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"
        '        If Not Regex.IsMatch(txtEmail.Text.Trim, ls_FormatEmail2) Then
        '            If Not lb_Errores Then
        '                lo_Validacion.Item("rpta") = "0"
        '                lo_Validacion.Item("msg") = "El correo electrónico no es válido"
        '                lo_Validacion.Item("control") = "txtEmail2"
        '                lb_Errores = True
        '            End If
        '            txtEmail2.Attributes.Item("data-error") = "true"
        '        Else
        '            txtEmail2.Attributes.Item("data-error") = "false"
        '        End If
        '    End If

        '    If Not String.IsNullOrEmpty(txtRuc.Text.Trim) Then
        '        Dim ls_FormatRUC As String = "^\d{11}$"
        '        If Not Regex.IsMatch(txtRuc.Text.Trim, ls_FormatRUC) Then
        '            If Not lb_Errores Then
        '                lo_Validacion.Item("rpta") = "0"
        '                lo_Validacion.Item("msg") = "El RUC ingresado no es correcto"
        '                lo_Validacion.Item("control") = "txtRuc"
        '                lb_Errores = True
        '            End If
        '            txtRuc.Attributes.Item("data-error") = "true"
        '        Else
        '            txtRuc.Attributes.Item("data-error") = "false"
        '        End If
        '    End If

        '    If String.IsNullOrEmpty(txtCentroLabores.Text.Trim) Then
        '        If Not lb_Errores Then
        '            lo_Validacion.Item("rpta") = 0
        '            lo_Validacion.Item("msg") = "Debe ingresar un centro de labores"
        '            lo_Validacion.Item("control") = "txtCentroLabores"
        '            lb_Errores = True
        '        End If
        '        txtCentroLabores.Attributes.Item("data-error") = "true"
        '    Else
        '        txtCentroLabores.Attributes.Item("data-error") = "false"
        '    End If

        '    If String.IsNullOrEmpty(txtCargoActual.Text.Trim) Then
        '        If Not lb_Errores Then
        '            lo_Validacion.Item("rpta") = 0
        '            lo_Validacion.Item("msg") = "Debe ingresar un cargo actual"
        '            lo_Validacion.Item("control") = "txtCargoActual"
        '            lb_Errores = True
        '        End If
        '        txtCargoActual.Attributes.Item("data-error") = "true"
        '    Else
        '        txtCargoActual.Attributes.Item("data-error") = "false"
        '    End If
        'End If

        Return lo_Validacion
    End Function

    Private Sub Guardar()
        Try
            Dim lo_Validacion As Dictionary(Of String, String) = ValidarFormulario()

            If lo_Validacion.Item("rpta") = 1 Then
                Dim tipoResp_Deu As String
                Dim codigo_Pk As Integer
                Dim codigo_sco As Integer
                Dim Observacion_deu As String
                Dim Moneda_Deu As String
                Dim codigo_cco As Integer
                Dim codigo_pso As Integer
                Dim recargo_deu As String
                Dim descuento_deu As Double
                Dim codigoPerRegistro_deu As Integer
                Dim MontoInicial As Double
                Dim FecVctoInicial As Date
                Dim NroCuotas As Integer
                Dim Montofijo As String
                Dim tipoperiodo As String
                Dim diadepago As String
                Dim NumCuotas As Integer

                TblCuotas = CType(Session("TblCuotas"), System.Web.UI.WebControls.Table)
                tipoResp_Deu = Left(Me.cmbParticipante.SelectedValue, 1)
                codigo_Pk = CInt(Mid(Me.cmbParticipante.SelectedValue, 3))
                codigo_sco = Me.cmbServicio.SelectedValue
                Observacion_deu = Me.txtObservacion.Text.Trim
                Moneda_Deu = "S"
                codigo_cco = ms_CodigoCco
                codigo_pso = ms_CodigoPso

                If Me.txtRecargo.Text = "" Then
                    recargo_deu = 0
                Else
                    recargo_deu = CDbl(Me.txtRecargo.Text)
                End If

                If Me.txtDescuento.Text = "" Then
                    descuento_deu = 0
                Else
                    descuento_deu = CDbl(Me.txtDescuento.Text)
                End If

                codigoPerRegistro_deu = Request.QueryString("id")

                '#############################################################################
                'Agregado por mvillavicencio 08/08/2012
                'Si es cuota única
                If chkUnaCuota.Checked = True Then

                    Dim Montototal_deu As Double
                    Dim fechaVencimiento_Deu As DateTime
                    Dim nropartes_deu As Integer
                    Dim Fechainicio_deu As DateTime

                    Montototal_deu = CDbl(Me.txtTotal.Text)
                    'fechaVencimiento_Deu = dtpFecVencInicial.Text
                    fechaVencimiento_Deu = dtpFecVenc.Text
                    nropartes_deu = 1
                    Fechainicio_deu = CDate(Now.ToShortDateString)

                    'Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.AgregarDeuda(tipoResp_Deu, codigo_Pk, codigo_sco, Observacion_deu, Montototal_deu, _
                    '                                                        Moneda_Deu, fechaVencimiento_Deu, ms_CodigoCco, nropartes_deu, Fechainicio_deu, codigo_pso, _
                    '                                                        recargo_deu, descuento_deu, codigoPerRegistro_deu)

                    Dim ObjGuardaDeuda As New ClsConectarDatos
                    ObjGuardaDeuda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    ObjGuardaDeuda.AbrirConexion()

                    ObjGuardaDeuda.Ejecutar("EVE_AgregarDeuda", tipoResp_Deu, codigo_Pk, codigo_sco, Observacion_deu, _
                                            Montototal_deu, Moneda_Deu, fechaVencimiento_Deu, codigo_cco, nropartes_deu, _
                                            Fechainicio_deu, codigo_pso, recargo_deu, descuento_deu, codigoPerRegistro_deu, 0)

                    ObjGuardaDeuda.CerrarConexion()

                    'Verificar el módulo si es EPRE y enviar email
                    EnviarMensajePensiones()

                    'If Request.QueryString("mod") = 1 Then 'Cuando sea Pre, enviar a la lista de inscritos
                    '    ClientScript.RegisterStartupScript(Me.GetType, "Ok3", "alert('Información Guardada Correctamente'); location.href='lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString & "';", True)
                    'Else
                    '    ClientScript.RegisterStartupScript(Me.GetType, "Ok4", "alert('Información Guardada Correctamente'); location.href='frmgeneracioncargos.aspx?" & Page.Request.QueryString.ToString & "'", True)
                    'End If

                Else
                    'If Me.TblCuotas.Rows.Count = 0 Then
                    If TblCuotas.Rows.Count = 0 Then
                        Me.lblMensaje.Text = "No se han generado cuotas, no se guardó la información"
                        Me.lblMensaje.ForeColor = Drawing.Color.Red
                        Me.udpStep2.Update()
                        'With respuestaPostback.Attributes
                        '    .Item("data-ispostback") = True
                        '    .Item("data-rpta") = "-1"
                        '    .Item("data-msg") = "No se han generado cuotas, no se guardó la información"
                        '    ' .Item("data-control") = lo_Validacion.Item("control")
                        'End With
                        Exit Sub
                    End If

                    If Me.txtSaldo.Text <> CType(Me.TblCuotas.Rows(Me.TblCuotas.Rows.Count - 1).Cells(2).Controls.Item(0), TextBox).Text Then
                        Me.lblMensaje.Text = "El Saldo a Financiar debe ser igual a la suma de las cuotas, no se guardó la información"
                        Me.lblMensaje.ForeColor = Drawing.Color.Red
                        Exit Sub
                    End If

                    NumCuotas = TblCuotas.Rows.Count - 2
                    If Me.txtCuotaInicial.Text = "" Then
                        MontoInicial = 0.0
                    Else
                        MontoInicial = CDbl(Me.txtCuotaInicial.Text)
                    End If

                    FecVctoInicial = Me.dtpFecVencInicial.Text
                    NroCuotas = Me.cmbCuotas.SelectedValue
                    Montofijo = cmbTipoCuota.SelectedValue
                    tipoperiodo = cmbTipoPeriodo.SelectedValue
                    diadepago = CInt(Me.cmbDiaPago.SelectedValue)

                    Dim ObjDeudaConv As New ClsConectarDatos
                    ObjDeudaConv.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                    Dim ValorDevuelveConvenio(1) As Integer
                    Dim ValorDevuelveDeuda(1) As Integer
                    'Dim codigo_ref As Integer

                    ObjDeudaConv.IniciarTransaccion()
                    ObjDeudaConv.Ejecutar("AgregarConvenioPago", CDate(Now.ToShortDateString), "P", tipoResp_Deu, codigo_Pk, "", "", "", "", _
                                          Montofijo, tipoperiodo, diadepago, NumCuotas, "S", 0.0, System.DBNull.Value, _
                                          codigoPerRegistro_deu, Observacion_deu, 0).CopyTo(ValorDevuelveConvenio, 0)
                    'Dim lo_Respuesta As Dictionary(Of String, String) = mo_RepoAdmision.AgregarConvenioPago(CDate(Now.ToShortDateString), "P", tipoResp_Deu, codigo_Pk, "", "", "", "", _
                    '                      Montofijo, tipoperiodo, diadepago, NumCuotas, "S", 0.0, codigo_ref, codigoPerRegistro_deu, Observacion_deu) '.CopyTo(ValorDevuelveConvenio, 0)
                    'With respuestaPostback.Attributes
                    '    .Item("data-ispostback") = True
                    '    .Item("data-rpta") = lo_Respuesta.Item("rpta")
                    '    .Item("data-msg") = lo_Respuesta.Item("msg")
                    'End With

                    'ValorDevuelveConvenio = CInt(lo_Respuesta.Item("cod"))
                    If codigo_sco = 30 Then
                        ObjDeudaConv.Ejecutar("PEC_AnularCargoMatricula", 40, codigo_pso, codigoPerRegistro_deu)
                    End If

                    For i As Integer = 0 To Me.TblCuotas.Rows.Count - 2
                        Dim CajaFechaVence As New TextBox
                        Dim CajaMontoCuota As New TextBox
                        Dim MontoDeuda As Double
                        Dim FecVencDeuda As Date
                        Dim FecIniDeuda As Date

                        If i = 0 Then
                            If Me.txtCuotaInicial.Text <> "" AndAlso Val(Me.txtCuotaInicial.Text) > 0 Then
                                MontoDeuda = CDbl(Me.txtCuotaInicial.Text)
                                FecVencDeuda = CDate(Me.dtpFecVencInicial.Text)
                                FecIniDeuda = CDate("01/" & Mid(CDate(Me.dtpFecVencInicial.Text).ToShortDateString, 4))

                                ObjDeudaConv.Ejecutar("EVE_AgregarDeuda", tipoResp_Deu, codigo_Pk, codigo_sco, "CONV :" & CStr(ValorDevuelveConvenio(0)) & " -> Cuota Inicial -- " & Observacion_deu, _
                                     MontoDeuda, Moneda_Deu, FecVencDeuda, codigo_cco, 1, FecIniDeuda, codigo_pso, 0, 0, codigoPerRegistro_deu, 0).CopyTo(ValorDevuelveDeuda, 0)

                                ObjDeudaConv.Ejecutar("AgregarDeudaConvenioPago", ValorDevuelveConvenio(0), ValorDevuelveDeuda(0), MontoDeuda, "G")
                                ObjDeudaConv.Ejecutar("AgregarDetalleConvenioPago", ValorDevuelveConvenio(0), i, FecVencDeuda, MontoDeuda)

                            End If
                        Else
                            CajaFechaVence = CType(Me.TblCuotas.Rows(i).Cells(1).Controls.Item(0), TextBox)
                            CajaMontoCuota = CType(Me.TblCuotas.Rows(i).Cells(2).Controls.Item(0), TextBox)
                            MontoDeuda = CDbl(CajaMontoCuota.Text)
                            FecVencDeuda = CDate(CajaFechaVence.Text)
                            FecIniDeuda = CDate("01/" & Mid(CDate(CajaFechaVence.Text).ToShortDateString, 4))

                            ObjDeudaConv.Ejecutar("EVE_AgregarDeuda", tipoResp_Deu, codigo_Pk, codigo_sco, "CONV :" & CStr(ValorDevuelveConvenio(0)) & " -> Cuota: " & i.ToString & " -- " & Observacion_deu, _
                                         MontoDeuda, Moneda_Deu, FecVencDeuda, codigo_cco, 1, FecIniDeuda, codigo_pso, 0, 0, codigoPerRegistro_deu, 0).CopyTo(ValorDevuelveDeuda, 0)

                            ObjDeudaConv.Ejecutar("AgregarDeudaConvenioPago", ValorDevuelveConvenio(0), ValorDevuelveDeuda(0), MontoDeuda, "G")
                            ObjDeudaConv.Ejecutar("AgregarDetalleConvenioPago", ValorDevuelveConvenio(0), i, FecVencDeuda, MontoDeuda)
                        End If
                    Next

                    ObjDeudaConv.TerminarTransaccion()
                End If
                With respuestaPostback.Attributes
                    .Item("data-ispostback") = True
                    .Item("data-rpta") = "1"
                    .Item("data-msg") = "Información guardada correctamente"
                End With
            Else
                With respuestaPostback.Attributes
                    .Item("data-ispostback") = True
                    .Item("data-rpta") = lo_Validacion.Item("rpta")
                    .Item("data-msg") = lo_Validacion.Item("msg")
                    .Item("data-control") = lo_Validacion.Item("control")
                End With
            End If
        Catch ex As Exception
            With respuestaPostback.Attributes
                .Item("data-ispostback") = True
                .Item("data-rpta") = "-1"
                .Item("data-msg") = "Ha ocurrido un error en el servidor"
            End With
            Throw ex
        Finally
            udpMensajeServidor.Update()
        End Try
    End Sub

    Private Sub EnviarMensajePensiones()
        Try
            Dim ObjMailNet As New ClsMail
            Dim mensaje As String = ""
            Dim para As String = ""
            Dim Obj As New ClsConectarDatos
            Dim dts As New Data.DataTable
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Obj.AbrirConexion()

            dts = mo_RepoAdmision.ConsultarBeneficiosPostulacionAlumno(ms_CodigoAlu)

            If dts.Rows.Count() > 0 Then

                '--------------------------------------------------------------------------------------------------'
                'Email a Direccion de Pensiones, notificandole que se han asignado beneficios al estudiante
                '--------------------------------------------------------------------------------------------------'
                Dim dt As New Data.DataTable
                dt = mo_RepoAdmision.ConsultarDirectorCentroCostos("area de pensiones")
                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        para = "</br><font face='Courier'>" & "Estimado(a): <b>" & dt.Rows(i).Item("nombre_completo").ToString.ToUpper & "</b>"
                        mensaje = "</br></br><P><ALIGN='justify'> Se le comunica que, la Escuela Pre Universitaria ha otorgado al estudiante " & lblAlumno.Text & " el(los) siguiente(s) beneficio(s):" & "</P>"
                        For x As Integer = 0 To dts.Rows.Count() - 1
                            mensaje = mensaje & "</br>* " & dts.Rows(x).Item("descripcion_bp")
                        Next

                        If TxtObservacion.Text <> "" Then
                            mensaje = mensaje & "</br></br><P><ALIGN='justify'> Observación: " & TxtObservacion.Text & "</P>"
                        End If

                        mensaje = mensaje & "</br></br> Atte.<br><br>Campus Virtual - USAT.</font>"
                        'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Admisión", dt.Rows(i).Item("email").ToString, "Beneficio de postulación USAT", para & mensaje, True)
                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Admisión", "fatima.vasquez@usat.edu.pe", "Beneficio de postulación USAT", para & mensaje, True)
                    Next
                End If

            End If
            Obj.CerrarConexion()
        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
#End Region
End Class
