Imports System.Globalization
Partial Class administrativo_pec_frmgeneracioncargos
    Inherits System.Web.UI.Page

    Dim ValorScript As String = "$(document).ready(function() { jQuery(function($) { "
    Public CodigoItemExamAdmision As Integer = 751
    Public CodigoItemEscuelaPre As Integer = 15

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
                '169:STAND(MARKETING)   - Autorizado por Esaavedra 20.11.2018

                If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 19 Or Request.QueryString("ctf") = 124 _
                    Or Request.QueryString("ctf") = 161 Or Request.QueryString("ctf") = 18 Or Request.QueryString("ctf") = 162 _
                    Or Request.QueryString("ctf") = 163 Or Request.QueryString("ctf") = 20 Or Request.QueryString("ctf") = 169 _
                    Or Request.QueryString("ctf") = 7 Then
                    Me.lblRecargo.Visible = True
                    Me.lblDescuento.Visible = True
                    Me.TxtRecargo.Visible = True
                    Me.TxtDescuento.Visible = True
                Else
                    Me.lblRecargo.Visible = False
                    Me.lblDescuento.Visible = False
                    Me.TxtRecargo.Visible = False
                    Me.TxtDescuento.Visible = False
                End If

                Me.TxtPrecio.Attributes.Add("OnKeyDown", "return false;")
                Me.TxtTotalPagar.Attributes.Add("OnKeyDown", "return false;")
                Me.TxtSaldoFinanciar.Attributes.Add("OnKeyDown", "return false;")

                Me.TxtRecargo.Attributes.Add("onkeypress", "validarnumero()")
                Me.TxtDescuento.Attributes.Add("onkeypress", "validarnumero()")
                Me.CmdGuardar.Attributes.Add("OnClick", "javascript:if (confirm('Desea Guardar los Cambios') == false) return false;")
                Me.CmdGuardarSinCon.Attributes.Add("OnClick", "javascript:if (confirm('Desea Guardar los Cambios') == false) return false;")
                Call LlenaCombos()

                ' ----------------------------------------------------------------------
                ' Datos con Respecto al Centro de Costos.
                ' ----------------------------------------------------------------------
                Dim Obj As New ClsConectarDatos
                Dim codigo_cco As Integer
                Dim Codigo_pso As Integer
                Dim tcl As String
                Dim cli As Integer
                Dim DatosCentroCosto As New Data.DataTable
                Dim DatosItems As New Data.DataTable
                Dim DatosPersona As New Data.DataTable
                Dim tbl, tblActivo As New Data.DataTable
                Dim tblbeneficios As New Data.DataTable
                Dim tblmodalidad As New Data.DataTable

                codigo_cco = Request.QueryString("cco")
                Codigo_pso = Request.QueryString("pso")
                tcl = Request.QueryString("tcl")
                cli = Request.QueryString("cli")
                Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                Obj.AbrirConexion()

                DatosCentroCosto = Obj.TraerDataTable("EVE_ConsultarEventos", 0, codigo_cco, "")
                DatosItems = Obj.TraerDataTable("EVE_ConsultarServicioPorCeco", codigo_cco)
                DatosPersona = Obj.TraerDataTable("PERSON_ConsultarClientes", Codigo_pso)


                Me.LblCodigoCCO.Text = DatosCentroCosto.Rows(0).Item("codigo_cco")
                Me.LblNombreCCO.Text = DatosCentroCosto.Rows(0).Item("nombre_dev")
                Me.TxtCuotaInicial.Text = IIf(DatosCentroCosto.Rows(0).Item("montocuotainicial_dev").Equals(System.DBNull.Value), "", DatosCentroCosto.Rows(0).Item("montocuotainicial_dev").ToString)
                TxtCuotaInicial.Text = TxtCuotaInicial.Text.Replace(",", ".")
                ClsFunciones.LlenarListas(Me.DDLItem, DatosItems, "codigo_sco", "descripcion_sco", "-- Seleccione Item --")

                'Agregado por mvillavicencio. 

                'Dia de pago y mes, son por defecto la fecha actual
                DDLFormaPago.SelectedValue = Format(Day(Now), "00")
                DDLInicioPrimCuota.SelectedValue = Format(Month(Now), "00")

                RbFijas.Checked = False
                RbPerioFijo.Checked = False
                RbVariables.Checked = True
                RbPerioVaria.Checked = True

                If DatosItems.Rows.Count = 1 Then
                    Me.DDLItem.SelectedValue = DatosItems.Rows(0).Item("codigo_sco").ToString
                    LlenaDatosItem(DatosItems.Rows(0).Item("codigo_sco"))
                End If

                ClsFunciones.LlenarListas(Me.DDLPersona, DatosPersona, "PK_Alternativo", "DatosPersona", "-- Seleccione Persona --")
                'Me.DDLPersona.SelectedValue = tcl & "-" & cli.ToString

                tblActivo = Obj.TraerDataTable("EVE_RetornaAlumnoActivo", Codigo_pso)
                If (tblActivo.Rows.Count > 0) Then
                    Me.DDLPersona.SelectedValue = tblActivo.Rows(0).Item("codigo_alu").ToString
                Else
                    Me.DDLPersona.SelectedIndex = -1
                End If


                'Me.RangeFechaVcto.MinimumValue = Now.ToShortDateString
                'Me.RangeValFechaVencimiento.MinimumValue = Now.ToShortDateString

                '=============================================================================
                'Agregado por mvillavicencio 19-09-12
                '=============================================================================

                lblAlumno.Text = DatosPersona.Rows(0).Item("nombres")
                'Por defecto fecha actual

                TxtFecVctoInicial.Text = Now

                'Por defecto el item INSCRIPCIONES  CURSOS Y OTROS-ESTUDIANTES
                tbl = Obj.TraerDataTable("EVE_ConsultarServicioPorNombre", "INSCRIPCIONES  CURSOS Y OTROS-ESTUDIANTES", codigo_cco)
                If tbl.Rows.Count > 0 Then
                    DDLItem.SelectedValue = tbl.Rows(0).Item("codigo_Sco")
                    LlenaDatosItem(tbl.Rows(0).Item("codigo_Sco"))
                End If

                'Buscar beneficios
                tblbeneficios = Obj.TraerDataTable("EPRE_ConsultarBeneficiosPostulacionAlumno", Request.QueryString("cli"))
                tblmodalidad = Obj.TraerDataTable("ConsultarModalidadIngreso", "NM", "CONVENIOS")

                'Cambios en datos del Cargo
                For i As Integer = 0 To tblbeneficios.Rows.Count() - 1
                    If tblbeneficios.Rows(i).Item("descripcion_bp") = "Beca - Pre" Or _
                        tblbeneficios.Rows(i).Item("descripcion_bp") = "Examen Gratuito" Or _
                        (Request.QueryString("codigo_min") = tblmodalidad.Rows(0).Item("codigo_Min") And _
                         tblbeneficios.Rows(i).Item("descripcion_bp") = "Ingreso Directo") Then

                        'Modificar Cargos
                        TxtDescuento.Text = TxtPrecio.Text
                    End If
                Next

                'Por defecto check de una sola cuota marcado
                chkUnaCuota.Checked = True
                LlenarDatosCuota() 'ejecutar accion del check marcado
                '=============================================================================
                Obj.CerrarConexion()
            End If


            If Me.DDLNumCuotas.SelectedIndex <> -1 Then
                CalculaCuotas(Now, Me.DDLNumCuotas.SelectedValue, Me.TxtSaldoFinanciar.Text, RbFijas.Checked, Me.RbPerioFijo.Checked)

            End If

            If Request.QueryString("mod") = 1 Then 'e-pre
                Me.PanAgrupagorConvenio.Enabled = True
                Me.DDLPersona.Enabled = False
            Else
                Me.PanAgrupagorConvenio.Enabled = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

    Private Sub LlenaCombos()
        Dim fecha As DateTime
        Dim con As Int16 = 0

        Me.DDLInicioPrimCuota.Items.Clear()
        fecha = "01/" & Now.Month & "/" & Now.Year
        For i As Int16 = Now.Month To Now.Month + 17
            con = con + 1
            Me.DDLInicioPrimCuota.Items.Add(Meses(fecha.Month) & " - " & fecha.Year)
            Me.DDLInicioPrimCuota.Items(con - 1).Value = fecha
            fecha = DateAdd(DateInterval.Month, 1, fecha)
        Next
    End Sub

    Private Sub LlenaCombosCuotas(ByVal numcuotas As Integer)
        Me.DDLNumCuotas.Items.Clear()
        Me.DDLNumCuotas.Items.Add("-- Seleccione Cuotas --")
        Me.DDLNumCuotas.Items(0).Value = -1

        If numcuotas > 0 Then 'agregado por mvillavicencio. Salia error cuando num cuotas = 0 10/08/12
            For i As Int16 = 1 To numcuotas
                Me.DDLNumCuotas.Items.Add(i)
            Next
        End If

        Me.DDLNumCuotas.SelectedValue = 1
    End Sub

    Protected Sub CmdCalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCalcular.Click
        CalculaCuotas(Now, Me.DDLNumCuotas.SelectedValue, Me.TxtSaldoFinanciar.Text, RbFijas.Checked, Me.RbPerioFijo.Checked)
    End Sub

    Private Sub CalculaCuotas(ByVal fecha As Date, ByVal Numcuotas As Integer, ByVal Total As Double, ByVal CuotaFija As Boolean, ByVal PerFijo As Boolean)

        TblCuotas.Rows.Clear()
        'Response.Write("start: ")
        'Response.Write(DDLNumCuotas.SelectedValue)
        'Response.Write("end: ")

        If chkUnaCuota.Checked = True Then
            CmdGuardar.Enabled = True
            Exit Sub
        Else
            If Me.DDLNumCuotas.SelectedValue = -1 Then
                Exit Sub
            End If
        End If

        Me.lblMensaje.Text = ""
        Dim MontoMensual As Double
        Dim Pago As Double
        Dim fechaini As Date

        fechaini = Now.ToShortDateString
        Pago = 0
        MontoMensual = FormatNumber((Total / Numcuotas), 2)
        fechaini = Me.DDLFormaPago.SelectedValue & Right(Me.DDLInicioPrimCuota.SelectedValue, 8)

        If PerFijo = True Then
            If DateDiff(DateInterval.Day, fechaini, CDate(Now.ToShortDateString)) > 0 Then
                Me.lblMensaje.Text = "La fecha de Inicio de 1era Cuota no debe ser menor a la actual"
                Me.lblMensaje.ForeColor = Drawing.Color.Red
                Me.TblCuotas.Rows.Clear()
                Exit Sub
            End If

            If DateDiff(DateInterval.Day, fechaini, CDate(Me.TxtFecVctoInicial.Text)) >= 0 Then
                Me.lblMensaje.Text = "La Fecha de Pago de 1era Cuota no debe ser menor o igual a la de la Cuota Inicial"
                Me.lblMensaje.ForeColor = Drawing.Color.Red
                Me.TblCuotas.Rows.Clear()
                Exit Sub
            End If

        End If

        Dim FilaCabecera As New TableRow
        Dim Celda1 As New TableCell
        Dim Celda2 As New TableCell
        Dim Celda3 As New TableCell

        Celda2.Text = "Fec. Vcto"
        Celda3.Text = "Monto"
        FilaCabecera.Cells.Add(Celda1)
        FilaCabecera.Cells.Add(Celda2)
        FilaCabecera.Cells.Add(Celda3)
        Me.TblCuotas.Rows.Add(FilaCabecera)


        For i As Integer = 1 To Numcuotas
            Dim CajaFechas As New TextBox
            Dim CajaMontos As New TextBox
            Dim Filas As New TableRow

            Dim Col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Col3 As New TableCell

            Dim ValiReq_Fecha As New RequiredFieldValidator
            Dim ValiReq_Fecha2 As New RangeValidator
            Dim ValiRerMonto As New RequiredFieldValidator
            Dim ValiRerMonto2 As New RangeValidator

            CajaFechas.ID = "FechaCuota_" & i.ToString
            CajaFechas.Width = 80
            CajaMontos.Width = 70
            CajaMontos.ID = "Monto_" & i.ToString
            CajaFechas.SkinID = "CajaTextoObligatorio"
            CajaMontos.SkinID = "CajaTextoObligatorio"

            ' --------------------------------------------------------
            ' Colocando los Validadores
            ' --------------------------------------------------------
            ValiReq_Fecha.ControlToValidate = CajaFechas.ClientID
            ValiReq_Fecha.ErrorMessage = "Ingrese una Fecha en cuota " & i.ToString
            ValiReq_Fecha.Text = "*"
            ValiReq_Fecha.ValidationGroup = "Guardar"
            ValiReq_Fecha.SetFocusOnError = True

            ValiRerMonto.ControlToValidate = CajaMontos.ClientID
            ValiRerMonto.ErrorMessage = "Ingrese un monto correcto en cuota " & i.ToString
            ValiRerMonto.Text = "*"
            ValiRerMonto.ValidationGroup = "Guardar"
            ValiRerMonto.SetFocusOnError = True

            ValiReq_Fecha2.ControlToValidate = CajaFechas.ClientID
            ValiReq_Fecha2.ErrorMessage = "Fecha de cuota " & i.ToString & " Incorrecta"
            ValiReq_Fecha2.MinimumValue = "01/08/2010"
            ValiReq_Fecha2.MaximumValue = "01/01/2050"
            ValiReq_Fecha2.Type = ValidationDataType.Date
            ValiReq_Fecha2.Text = "*"
            ValiReq_Fecha2.ValidationGroup = "Guardar"
            ValiReq_Fecha2.SetFocusOnError = True

            ValiRerMonto2.ControlToValidate = CajaMontos.ClientID
            ValiRerMonto2.ErrorMessage = "Monto de cuota " & i.ToString & " incorrecto"
            ValiRerMonto2.Text = "*"
            ValiRerMonto2.MinimumValue = 0
            ValiRerMonto2.MaximumValue = 100000
            ValiRerMonto2.Type = ValidationDataType.Double
            ValiRerMonto2.ValidationGroup = "Guardar"
            ValiRerMonto2.SetFocusOnError = True


            ' -------------------------------------------------------------
            ' Colocando los datos de Fecha, y Cuotas
            ' -------------------------------------------------------------
            If CuotaFija = True Then
                If (i = Numcuotas) Then
                    CajaMontos.Text = FormatNumber(Total - Pago, 2, TriState.False, TriState.False, TriState.False) 'FormatNumber(Total - Pago, 2)
                Else
                    CajaMontos.Text = FormatNumber(MontoMensual, 2, TriState.False, TriState.False, TriState.False) 'MontoMensual 
                End If
                CajaMontos.Attributes.Add("OnKeyPress", "javascript:return false;")
                Pago = Pago + MontoMensual
            End If

            If PerFijo = True Then
                If fechaini.Month <> 2 Then
                    fechaini = Me.DDLFormaPago.SelectedValue & "/" & fechaini.Month & "/" & fechaini.Year
                End If
                CajaFechas.Text = fechaini
                CajaFechas.Attributes.Add("OnKeyPress", "javascript:return false;")
                fechaini = DateAdd(DateInterval.Month, 1, fechaini)
            End If

            ' ---------------------------------------------------------------------------
            ' Agregado por mvillavicencio 09/05/2012
            ' Poniendo la fecha actual como fecha de vencimiento, y el total, como monto cuota
            'Cuando el periodo y monto son variables, y la cuota es una,
            ' --------------------------------------------------------------------------
            If Numcuotas = 1 And RbPerioVaria.Checked = True And RbVariables.Checked = True Then
                fechaini = Now
                CajaFechas.Text = fechaini
                CajaFechas.Attributes.Add("OnKeyPress", "javascript:return false;")
                CajaMontos.Text = Total
                CajaMontos.Attributes.Add("OnKeyPress", "javascript:return false;")
                'TxtFecVctoInicial.Text = DateTime.Now.AddDays(-1)
                If TxtFecVctoInicial.Text = "" Then
                    TxtFecVctoInicial.Text = DateTime.Now
                End If
            End If

            CajaMontos.Attributes.Add("OnKeyUp", "javascript:sumarcuotas()")
            Col1.Text = i.ToString
            Col2.Controls.Add(CajaFechas)
            Col2.Controls.Add(ValiReq_Fecha)
            Col2.Controls.Add(ValiReq_Fecha2)
            Col3.Controls.Add(CajaMontos)
            Col3.Controls.Add(ValiRerMonto)
            Col3.Controls.Add(ValiRerMonto2)


            Filas.Cells.Add(Col1)
            Filas.Cells.Add(Col2)
            Filas.Cells.Add(Col3)
            Me.TblCuotas.Rows.Add(Filas)
            ValorScript = ValorScript & GeneraMascara(CajaFechas.ClientID)
        Next

        ' ---------------------------------------------------------------------------
        ' Agregando una Fila de total para sumar todas las cuotas, validacion previa
        ' si existen cuotas asignadas
        ' --------------------------------------------------------------------------
        If Numcuotas > 0 Then
            Dim TxtTotal As New TextBox
            Dim ColF1 As New TableCell
            Dim ColF2 As New TableCell
            Dim colF3 As New TableCell
            Dim FilaTotal As New TableRow

            ColF2.Text = "Total"
            TxtTotal.ID = "TxtTotal"
            TxtTotal.Attributes.Add("OnKeyPress", "javascript:return false;")
            TxtTotal.SkinID = "CajaTextoSinMarco"

            'Agregando que cuando sea cuota variable y num de cuota 1, ponga el total
            If CuotaFija = True Or (CuotaFija = False And DDLNumCuotas.SelectedValue = 1) Then
                TxtTotal.Text = Total
            Else
                TxtTotal.Text = "0.00"
            End If

            colF3.Controls.Add(TxtTotal)
            FilaTotal.Controls.Add(ColF1)
            FilaTotal.Controls.Add(ColF2)
            FilaTotal.Controls.Add(colF3)

            Me.TblCuotas.Rows.Add(FilaTotal)
            Me.HddNumCuotas.Value = Numcuotas

            ClientScript.RegisterStartupScript(Me.GetType, "CargaMascara", "<script type='text/javascript' language='javascript'>" & ValorScript + " }) ; }) " & "</script>", False)
            Me.CmdGuardar.Enabled = True
        End If

    End Sub

    Private Function GeneraMascara(ByVal nombrecontrol As String) As String
        Return "$('#" & nombrecontrol & "').mask('99/99/9999');"

    End Function

    Private Function Meses(ByVal Mes As Integer) As String
        Dim MesRetorno As String
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

    Private Sub LlenaDatosItem(ByVal ValorSeleccionado As Integer)

        Try
            Dim ObjDatosItem As New ClsConectarDatos
            Dim DatosItem As New Data.DataTable
            Dim DatosDeudas As New Data.DataTable

            ObjDatosItem.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            ObjDatosItem.AbrirConexion()
            DatosItem = ObjDatosItem.TraerDataTable("EVE_ConsultarDatosServicio", Request.QueryString("cco"), ValorSeleccionado)
            DatosDeudas = ObjDatosItem.TraerDataTable("EVE_consultarDeudas", Request.QueryString("pso"), Request.QueryString("cco"), ValorSeleccionado)
            ObjDatosItem.CerrarConexion()

            If DatosDeudas.Rows.Count = 0 Then
                Me.Panel3.Visible = False
            Else
                Me.Panel3.Visible = True
                Me.DgvDeudas.DataSource = DatosDeudas
                Me.DgvDeudas.DataBind()
            End If

            Me.TxtPrecio.Text = ""
            Me.TxtDescuento.Text = ""
            Me.TxtRecargo.Text = ""
            Me.TxtTotalPagar.Text = ""

            If DatosItem.Rows.Count > 0 Then                
                Me.TabFormaPago.enabled = True

                With DatosItem
                    Dim Precio As Double

                    If .Rows(0).Item("precio_sco").Equals(System.DBNull.Value) = True Then
                        Precio = 0
                    Else
                        Precio = .Rows(0).Item("precio_sco").ToString
                    End If

                    '' **************** VALIDACIÓN CÁLCULO DE PRECIO ESCUELA PRE ******************************

                    '------ Hcano : Comentado, Precio de Examen de Admision Reemplazado por valor devuelto de las funciones.

                    'If Request.QueryString("mod") = 1 And Request.QueryString("tcl") = "E" Then
                    '    Dim ArrayDevuelto(1) As Object
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

                    Me.TxtPrecio.Text = IIf(Precio = 0, "0.00", Precio)
                    Me.TxtTotalPagar.Text = IIf(Precio = 0, "0.00", Precio)
                    Me.TxtTotalPagarMuestra.Text = IIf(Precio = 0, "0.00", Precio)


                    Me.TxtDescuento.Text = "0.00"
                    Me.TxtRecargo.Text = "0.00"
                    Me.TxtSaldoFinanciar.Text = Val(Me.TxtTotalPagar.Text) - Val(Me.TxtCuotaInicial.Text)

                    If CBool(.Rows(0).Item("generaMora_sco")) = True Then
                        Me.TxtFechaVencimiento.Text = CDate(.Rows(0).Item("fechaVencimiento_sco")).ToShortDateString
                        Me.TxtFechaVencimiento.Enabled = True
                    Else
                        Me.TxtFechaVencimiento.Text = Now.ToShortDateString
                        Me.TxtFechaVencimiento.Enabled = False
                    End If

                    If CBool(.Rows(0).Item("agrupaPension_Sco")) = False Then
                        Me.DDLAgruparPension.Enabled = False
                        Me.ValidaAgrupa.Enabled = False

                        Me.DDLNumPartes.Items.Clear()
                        Me.DDLNumPartes.Items.Add("-- Seleccione Cuotas --")
                        Me.DDLNumPartes.Items(0).Value = -1
                        For i As Int16 = 1 To 1
                            Me.DDLNumPartes.Items.Add(i)
                        Next
                        Me.DDLNumPartes.SelectedValue = 1
                    Else
                        Me.DDLNumPartes.Items.Clear()
                        Me.DDLNumPartes.Items.Add("-- Seleccione Cuotas --")
                        Me.DDLNumPartes.Items(0).Value = -1
                        For i As Int16 = 1 To 4
                            Me.DDLNumPartes.Items.Add(i)
                        Next
                        Me.DDLAgruparPension.Enabled = True
                        Me.ValidaAgrupa.Enabled = True

                    End If

                    Call LlenaCombosCuotas(.Rows(0).Item("nroPartes_sco"))

                    'Agregado por mvillavicencio 10/08/12
                    txtCuotaUnica.Text = TxtTotalPagar.Text

                End With

                If Request.QueryString("mod") = 1 Then 'E-pre
                    Me.DDLAgruparPension.SelectedIndex = 0
                End If
            Else
                Me.TabFormaPago.enabled = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub DDLItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLItem.SelectedIndexChanged
        Try
            Call LlenaDatosItem(Me.DDLItem.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Try
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

            tipoResp_Deu = Left(Me.DDLPersona.SelectedValue, 1)
            codigo_Pk = CInt(Mid(Me.DDLPersona.SelectedValue, 3))
            codigo_sco = Me.DDLItem.SelectedValue
            Observacion_deu = Me.TxtObservacion.Text.Trim
            Moneda_Deu = "S"
            codigo_cco = Request.QueryString("cco")
            codigo_pso = Request.QueryString("pso")
            If Me.TxtRecargo.Text = "" Then
                recargo_deu = 0
            Else
                recargo_deu = CDbl(Me.TxtRecargo.Text)
            End If
            If Me.TxtDescuento.Text = "" Then
                descuento_deu = 0
            Else
                descuento_deu = CDbl(Me.TxtDescuento.Text)
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

                Montototal_deu = CDbl(Me.TxtTotalPagar.Text)
                fechaVencimiento_Deu = TxtFecVctoInicial.Text
                nropartes_deu = 1
                Fechainicio_deu = CDate(Now.ToShortDateString)

                Dim ObjGuardaDeuda As New ClsConectarDatos
                ObjGuardaDeuda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                ObjGuardaDeuda.AbrirConexion()

                ObjGuardaDeuda.Ejecutar("EVE_AgregarDeuda", tipoResp_Deu, codigo_Pk, codigo_sco, Observacion_deu, _
                                        Montototal_deu, Moneda_Deu, fechaVencimiento_Deu, codigo_cco, nropartes_deu, _
                                        Fechainicio_deu, codigo_pso, recargo_deu, descuento_deu, codigoPerRegistro_deu, 0)

                ObjGuardaDeuda.CerrarConexion()
                'Verificar el módulo si es EPRE y enviar email
                EnviarMensajePensiones()

                If Request.QueryString("mod") = 1 Then 'Cuando sea Pre, enviar a la lista de inscritos
                    ClientScript.RegisterStartupScript(Me.GetType, "Ok3", "alert('Información Guardada Correctamente'); location.href='lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString & "';", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "Ok4", "alert('Información Guardada Correctamente'); location.href='frmgeneracioncargos.aspx?" & Page.Request.QueryString.ToString & "'", True)
                End If

            Else
                If Me.TblCuotas.Rows.Count = 0 Then
                    Me.lblMensaje.Text = "No se han generado cuotas, no se guardó la información"
                    Me.lblMensaje.ForeColor = Drawing.Color.Red
                    Exit Sub
                End If

                If Me.TxtSaldoFinanciar.Text <> CType(Me.TblCuotas.Rows(Me.TblCuotas.Rows.Count - 1).Cells(2).Controls.Item(0), TextBox).Text Then
                    Me.lblMensaje.Text = "El Saldo a Financiar debe ser igual a la suma de las cuotas, no se guardó la información"
                    Me.lblMensaje.ForeColor = Drawing.Color.Red
                    Exit Sub
                End If

                NumCuotas = TblCuotas.Rows.Count - 2
                If Me.TxtCuotaInicial.Text = "" Then
                    MontoInicial = 0.0
                Else
                    MontoInicial = CDbl(Me.TxtCuotaInicial.Text)
                End If

                FecVctoInicial = Me.TxtFecVctoInicial.Text
                NroCuotas = Me.DDLNumCuotas.SelectedValue
                Montofijo = IIf(Me.RbFijas.Checked = True, "F", "V")
                tipoperiodo = IIf(Me.RbPerioFijo.Checked = True, "F", "V")
                diadepago = CInt(Me.DDLFormaPago.SelectedValue)

                Dim ObjDeudaConv As New ClsConectarDatos
                ObjDeudaConv.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                Try
                    Dim ValorDevuelveConvenio(1) As Integer
                    Dim ValorDevuelveDeuda(1) As Integer

                    ObjDeudaConv.IniciarTransaccion()
                    ObjDeudaConv.Ejecutar("AgregarConvenioPago", CDate(Now.ToShortDateString), "P", tipoResp_Deu, codigo_Pk, "", "", "", "", _
                                          Montofijo, tipoperiodo, diadepago, NumCuotas, "S", 0.0, System.DBNull.Value, _
                                          codigoPerRegistro_deu, Observacion_deu, 0).CopyTo(ValorDevuelveConvenio, 0)
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
                            If Me.TxtCuotaInicial.Text <> "" AndAlso Val(Me.TxtCuotaInicial.Text) > 0 Then
                                MontoDeuda = CDbl(Me.TxtCuotaInicial.Text)
                                FecVencDeuda = CDate(Me.TxtFecVctoInicial.Text)
                                FecIniDeuda = CDate("01/" & Mid(CDate(Me.TxtFecVctoInicial.Text).ToShortDateString, 4))

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

                    If Request.QueryString("mod") = 1 Then 'Cuando sea Pre, enviar a la lista de inscritos
                        ClientScript.RegisterStartupScript(Me.GetType, "Ok", "if (confirm('Información guardada correctamente, ¿imprimir Convenio?')==true) {location.href='FrmImprimirConvenio.aspx?codigo_cpa=" & ValorDevuelveConvenio(0) & "&" & Page.Request.QueryString.ToString & " '} else {location.href='lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString & "' }", True)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "Ok", "if (confirm('Información guardada correctamente, ¿imprimir Convenio?')==true) {location.href='FrmImprimirConvenio.aspx?codigo_cpa=" & ValorDevuelveConvenio(0) & "&" & Page.Request.QueryString.ToString & " '} else {location.href='frmgeneracioncargos.aspx?" & Page.Request.QueryString.ToString & "' }", True)
                    End If



                    'YPEREZ LLAMAR IMPRESION
                    Me.btnImprimir.navigateurl = "frmExportaFichaPostulacion.aspx?pso=" & codigo_pso & "&cli=" & Request.QueryString("cli") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=false"


                Catch ex As Exception
                    ObjDeudaConv.AbortarTransaccion()
                    lblMensaje.Text = "Ocurrió un error al guardar los datos,intentelo nuevamente." & ex.Message
                End Try

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CmdGuardarSinCon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardarSinCon.Click

        'Me.LblMensajeSinCon.Text = ""
        'If IsDate(Me.TxtFechaVencimiento.Text) = False Then
        '    Me.LblMensajeSinCon.Text = "Fecha de Vencimiento Incorrecta"
        '    Me.LblMensajeSinCon.ForeColor = Drawing.Color.Red
        '    Exit Sub
        'End If

        Try
            Dim tipoResp_Deu As String
            Dim codigo_Pk As Integer
            Dim codigo_sco As Integer
            Dim Observacion_deu As String
            Dim Montototal_deu As Double
            Dim Moneda_Deu As String
            Dim fechaVencimiento_Deu As DateTime
            Dim codigo_cco As Integer
            Dim nropartes_deu As Integer
            Dim Fechainicio_deu As DateTime
            Dim codigo_pso As Integer
            Dim recargo_deu As String
            Dim descuento_deu As Double
            Dim codigoPerRegistro_deu As Integer

            tipoResp_Deu = Left(Me.DDLPersona.SelectedValue, 1)
            codigo_Pk = CInt(Mid(Me.DDLPersona.SelectedValue, 3))
            codigo_sco = Me.DDLItem.SelectedValue
            Observacion_deu = Me.TxtObservacion.Text.Trim
            Montototal_deu = CDbl(Me.TxtTotalPagar.Text)
            Moneda_Deu = "S"
            fechaVencimiento_Deu = Me.TxtFechaVencimiento.Text
            codigo_cco = Request.QueryString("cco")
            nropartes_deu = Me.DDLNumPartes.SelectedValue
            If Me.DDLAgruparPension.Enabled = False Then
                Fechainicio_deu = CDate(Now.ToShortDateString)
            Else
                Fechainicio_deu = CDate(Me.DDLAgruparPension.SelectedValue & "-" & Now.Year)
            End If
            codigo_pso = Request.QueryString("pso")
            If Me.TxtRecargo.Text = "" Then
                recargo_deu = 0
            Else
                recargo_deu = CDbl(Me.TxtRecargo.Text)
            End If
            If Me.TxtDescuento.Text = "" Then
                descuento_deu = 0
            Else
                descuento_deu = CDbl(Me.TxtDescuento.Text)
            End If
            codigoPerRegistro_deu = Request.QueryString("id")

            Dim ObjGuardaDeuda As New ClsConectarDatos
            ObjGuardaDeuda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjGuardaDeuda.AbrirConexion()

            ObjGuardaDeuda.Ejecutar("EVE_AgregarDeuda", tipoResp_Deu, codigo_Pk, codigo_sco, Observacion_deu, _
                                    Montototal_deu, Moneda_Deu, fechaVencimiento_Deu, codigo_cco, nropartes_deu, _
                                    Fechainicio_deu, codigo_pso, recargo_deu, descuento_deu, codigoPerRegistro_deu, 0)
            ObjGuardaDeuda.CerrarConexion()
            'Verificar el módulo si es EPRE y enviar email
            EnviarMensajePensiones()

            If Request.QueryString("mod") = 1 Then 'Cuando sea Pre, enviar a la lista de inscritos
                ClientScript.RegisterStartupScript(Me.GetType, "Ok", "alert('Información Guardada Correctamente');location.href='lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString & "'", True)
                'ClientScript.RegisterStartupScript(Me.GetType, "Ok", "alert('Información Guardada Correctamente');location.href='frmpersona.aspx?" & Page.Request.QueryString.ToString & "'", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Ok2", "alert('Información Guardada Correctamente'); location.href='frmgeneracioncargos.aspx?" & Page.Request.QueryString.ToString & "'", True)
            End If

        Catch ex As Exception
            lblMensaje.Text = "Ocurrió un error al guardar los datos,intentelo nuevamente." & ex.Message
        End Try

    End Sub
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click, cmdCancelar1.Click, cmdCancelar0.Click
        Response.Redirect("lstinscritoseventocargo.aspx?" & Page.Request.QueryString.ToString)
    End Sub

    'Public Sub EnviarMensajePensiones()
    '    If Request.QueryString("tcl") = "E" And Request.QueryString("mod") = 1 Then
    '        Dim ObjGuardaDeuda As New ClsConectarDatos
    '        Dim tblMensaje As Data.DataTable
    '        ObjGuardaDeuda.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
    '        ObjGuardaDeuda.AbrirConexion()
    '        tblMensaje = ObjGuardaDeuda.TraerDataTable("PRE_ObtenerDatosParaCorreo", Request.QueryString("cli"))
    '        ObjGuardaDeuda.CerrarConexion()
    '        ObjGuardaDeuda = Nothing

    '        If tblMensaje.Rows.Count > 0 Then
    '            If tblMensaje.Rows(0).Item("EnviarCorreo") = 1 Then
    '                Dim ObjMailNet As New ClsMail
    '                Dim Mensaje, Correo, AsuntoCorreo, ConCopiaA As String

    '                'Correo = "jdanjanovic@usat.edu.pe"
    '                'ConCopiaA = "hreyes@usat.edu.pe;hzelada@usat.edu.pe;vataboada@usat.edu.pe"
    '                Correo = "gchunga@usat.edu.pe"
    '                ConCopiaA = "hzelada@usat.edu.pe"
    '                AsuntoCorreo = tblMensaje.Rows(0).Item("asunto")
    '                Mensaje = tblMensaje.Rows(0).Item("mensaje")

    '                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Módulo de Gestión Eventos", Correo, AsuntoCorreo, Mensaje, True, ConCopiaA)
    '                ObjMailNet = Nothing

    '            End If
    '        End If
    '    End If
    'End Sub


    'Por mvillavicencio 10/08/12
    Protected Sub chkUnaCuota_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUnaCuota.CheckedChanged
        LlenarDatosCuota()
    End Sub

    Private Sub LlenarDatosCuota()
        Try
            Dim sbMensaje As New StringBuilder
            sbMensaje.Append("<script type='text/javascript'>")
            sbMensaje.AppendFormat("sumatotalpagar()")
            sbMensaje.Append("</script>")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "mensaje", sbMensaje.ToString, False)

            If chkUnaCuota.Checked = True Then
                lblCuotainicial.Text = "Cuota única"
                lblFechaVctoInicial.Text = "&#160;Fecha Vcto Cuota Única&#160;"
                trsaldofinanciar.Visible = False
                trnrocuotas.Visible = False
                trmontocuota.Visible = False
                trPeriodo.Visible = False
                trCalcularCuotas.Visible = False
                PanCuotas.Visible = False
                TblCuotas.Visible = False
                txtCuotaUnica.Visible = True
                txtCuotaUnica.Text = Me.TxtTotalPagar.Text
                TxtCuotaInicial.Visible = False
            Else
                lblCuotainicial.Text = "Cuota inicial"
                lblFechaVctoInicial.Text = "&#160;Fecha Vcto Cuota Inicial&#160;"
                trsaldofinanciar.Visible = True
                trnrocuotas.Visible = True
                trmontocuota.Visible = True
                trPeriodo.Visible = True
                trCalcularCuotas.Visible = True
                PanCuotas.Visible = True
                TblCuotas.Visible = True
                txtCuotaUnica.Visible = False
                TxtCuotaInicial.Visible = True
                CmdGuardar.Enabled = False

                CalculaCuotas(Now, Me.DDLNumCuotas.SelectedValue, Me.TxtSaldoFinanciar.Text, RbFijas.Checked, Me.RbPerioFijo.Checked)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
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

            dts = Obj.TraerDataTable("EPRE_ConsultarBeneficiosPostulacionAlumno", Request.QueryString("cli"))

            If dts.Rows.Count() > 0 Then

                '--------------------------------------------------------------------------------------------------'
                'Email a Direccion de Pensiones, notificandole que se han asignado beneficios al estudiante
                '--------------------------------------------------------------------------------------------------'
                Dim dt As New Data.DataTable
                dt = Obj.TraerDataTable("CEC_ConsultarDirectorCentroCostos", "area de pensiones")
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
                        ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Admisión", dt.Rows(i).Item("email").ToString, "Beneficio de postulación USAT", para & mensaje, True)
                    Next
                End If

            End If
            Obj.CerrarConexion()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


End Class
