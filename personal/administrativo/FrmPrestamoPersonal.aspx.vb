Imports System.Data

Partial Class administrativo_FrmPrestamoPersonal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then            
            mostrartipoplanilla()
            mostrarrubro()
            mostraranio()
            mostrarmes()
            Me.txtFecha.Text = Date.Now.ToString("dd/MM/yyyy")
            Me.HdPersonal.Value = Request.QueryString("id")
            CargaPersonal()
            'Inicializamos Datos del año y mes
            Me.HdAnio.Value = Today.Year
            Me.HdMes.Value = Today.Month


            For j As Integer = 0 To Me.cboMes.Items.Count - 1
                If (Me.cboMes.Items(j).Text = MonthName(Me.HdMes.Value)) Then
                    Me.cboMes.SelectedIndex = j
                    j = Me.cboMes.Items.Count
                End If
            Next            
        End If
    End Sub

    Private Sub CargaPersonal()
        Try
            Dim dt As New DataTable
            Dim objcx As New ClsConectarDatos
            objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            objcx.AbrirConexion()
            dt = objcx.TraerDataTable("LOG_BuscaPersonal", Me.HdPersonal.Value, "")
            objcx.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Dim dtCliente As New DataTable
                objcx.AbrirConexion()
                dtCliente = objcx.TraerDataTable("PER_RetornaTipoCliente", dt.Rows(0).Item("codigo_per"))
                objcx.CerrarConexion()

                If (dtCliente.Rows.Count > 0) Then
                    Me.txtTrabajador.Text = dtCliente.Rows(0).Item("Nombres")
                    Me.HdPersonal.Value = dtCliente.Rows(0).Item("codigo_tcl")
                Else
                    Me.btnGuardar.Enabled = False
                    Me.lblMensaje.Text = "No se encontro al trabajador"
                End If
            Else
                Me.btnGuardar.Enabled = False
                Me.lblMensaje.Text = "No se encontro al trabajador"
            End If
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar al personal: " & ex.Message
        End Try
    End Sub

    Private Sub mostrartipoplanilla()
        Try
            Dim dt As New DataTable
            Dim objcx As New ClsConectarDatos
            objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            objcx.AbrirConexion()
            dt = objcx.TraerDataTable("sp_vertipoplanilla", "TO", "", "", "")
            objcx.CerrarConexion()

            Me.cboTipoPlanilla.DataSource = dt
            Me.cboTipoPlanilla.DataTextField = "descripcion_tplla"
            Me.cboTipoPlanilla.DataValueField = "codigo_tplla"
            Me.cboTipoPlanilla.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los tipo de planilla: " & ex.Message
        End Try        
    End Sub

    Private Sub mostrarrubro() 'solo 2 descuento y / o adelantos
        Try
            Dim dt As New DataTable
            Dim objcx As New ClsConectarDatos
            objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim dtsrubro As New DataSet
            objcx.AbrirConexion()
            dt = objcx.TraerDataTable("sp_verrubro", "PA", "")
            objcx.CerrarConexion()

            Me.cboRubro.DataSource = dt
            Me.cboRubro.DataTextField = "descripcion_rub"
            Me.cboRubro.DataValueField = "codigo_rub"
            Me.cboRubro.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los rubros: " & ex.Message
        End Try
    End Sub

    Private Sub mostraranio()
        Dim i As Integer
        Me.cboAnio.Items.Clear()
        For i = Today.Year To 2050
            Me.cboAnio.Items.Add(Str(i))
        Next
    End Sub

    Private Sub mostrarmes()
        Dim i As Integer
        Me.cbomes.Items.Clear()
        For i = 1 To 12
            Me.cbomes.Items.Add(MonthName(i))
        Next

    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If (validaDatos() = True) Then
            Dim cuotas As Integer
            Dim mesInicio As Integer
            Dim anioInicio As Integer

            cuotas = Integer.Parse(Me.txtCuotas.Text)
            mesInicio = Me.cboMes.SelectedIndex + 1
            anioInicio = Me.cboAnio.SelectedItem.Value
            'Me.txtImporteTotal.Text
            'Me.txtCuotas.Text

            Dim dt As New DataTable()

            dt.Columns.Add(New DataColumn("importe"))
            dt.Columns.Add(New DataColumn("moneda"))
            dt.Columns.Add(New DataColumn("idtipoplanilla"))
            dt.Columns.Add(New DataColumn("tipoplanilla"))
            dt.Columns.Add(New DataColumn("anio"))
            dt.Columns.Add(New DataColumn("mes"))

            Dim dRow As DataRow
            'En caso que haber cargos anteriores
            For i As Integer = 0 To Me.gvCuotas.Rows.Count - 1
                If (Me.gvCuotas.Rows(i).Visible = True) Then
                    dRow = dt.NewRow
                    dRow.Item(0) = FormatNumber(Me.gvCuotas.Rows(i).Cells(0).Text, 2)
                    dRow.Item(1) = Me.gvCuotas.Rows(i).Cells(1).Text
                    dRow.Item(2) = Me.gvCuotas.Rows(i).Cells(2).Text
                    dRow.Item(3) = Me.gvCuotas.Rows(i).Cells(3).Text
                    dRow.Item(4) = Me.gvCuotas.Rows(i).Cells(4).Text
                    dRow.Item(5) = Me.gvCuotas.Rows(i).Cells(5).Text

                    dt.Rows.Add(dRow)
                End If                
            Next

            'Generamos los cargos
            For i As Integer = 1 To cuotas
                dRow = dt.NewRow
                dRow.Item(0) = FormatNumber(Me.txtImporte.Text, 2)
                dRow.Item(1) = "SOLES"
                dRow.Item(2) = Me.cboTipoPlanilla.SelectedItem.Value
                dRow.Item(3) = Me.cboTipoPlanilla.SelectedItem.Text
                dRow.Item(4) = anioInicio
                dRow.Item(5) = MonthName(mesInicio).ToUpper

                mesInicio = mesInicio + 1

                If (mesInicio > 12) Then
                    mesInicio = 1
                    anioInicio = anioInicio + 1
                End If

                dt.Rows.Add(dRow)
                dt.AcceptChanges()
            Next i

            gvCuotas.DataSource = dt
            gvCuotas.DataBind()

            For j As Integer = 0 To Me.cboAnio.Items.Count - 1
                If (Me.cboAnio.Items(j).Text = anioInicio) Then
                    Me.cboAnio.SelectedIndex = j
                    j = Me.cboAnio.Items.Count
                End If
            Next

            For j As Integer = 0 To Me.cboMes.Items.Count - 1
                If (Me.cboMes.Items(j).Text = MonthName(mesInicio)) Then
                    Me.cboMes.SelectedIndex = j
                    j = Me.cboMes.Items.Count
                End If
            Next

            'Recalculamos monto total
            CalculaTotal()

            'Capturamos las ultimas fechas del 
            Me.HdMes.Value = Me.cboMes.SelectedIndex + 1
            Me.HdAnio.Value = Me.cboAnio.SelectedItem.Value

            Me.lblMensaje.Text = ""
        End If
    End Sub

    Private Sub CalculaTotal()
        Dim importeTotal As Double = 0
        For i As Integer = 0 To Me.gvCuotas.Rows.Count - 1
            If (Me.gvCuotas.Rows(i).Visible = True) Then
                Dim monto As Double = Double.Parse(Me.gvCuotas.Rows(i).Cells(0).Text)
                importeTotal = importeTotal + monto
            End If
        Next
        Me.txtImporteTotal.Text = FormatNumber(importeTotal, 2)
    End Sub

    Private Function validaDatos() As Boolean
        If (Me.txtCuotas.Text.Trim.Length = 0) Then
            Me.lblMensaje.Text = "Debe ingresar el numero de cuotas"
            Return False
        ElseIf (Integer.Parse(Me.txtCuotas.Text.Trim) = 0) Then
            Me.lblMensaje.Text = "El numero de cuotas debe ser superior a 0"
            Return False
        End If

        If (Me.txtImporte.Text.Trim.Length = 0) Then
            Me.lblMensaje.Text = "Debe ingresar el importe mensual"
            Return False
        ElseIf (Double.Parse(Me.txtImporte.Text.Trim) = 0) Then
            Me.lblMensaje.Text = "El importe mensual debe ser superior a 0"
            Return False
        End If

        If (Integer.Parse(Me.cboAnio.SelectedItem.Value) < Integer.Parse(Me.HdAnio.Value)) Then
            Me.lblMensaje.Text = "Debe iniciar en un año superior al " & Me.HdAnio.Value
            Return False
        Else
            If (Integer.Parse(Me.cboMes.SelectedIndex + 1) < Integer.Parse(Me.HdMes.Value)) Then
                Me.lblMensaje.Text = "Debe iniciar en un mes superior al " & MonthName(Me.HdMes.Value)
                Return False
            End If
        End If

        Return True
    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (validaFormulario() = True) Then
            Dim codigo_tip As Integer
            Dim obj As New ClsConectarDatos
            Dim nroCuotas As Integer
            Dim codigo_doc, codigo_dpc As Object
            Dim tieneDeuda As Boolean = False
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Try
                codigo_tip = 1  '1. Soles - Tabla: tipo_cambio
                nroCuotas = Me.gvCuotas.Rows.Count

                If (Request.QueryString("estado") = "E") Then
                    tieneDeuda = True
                End If

                obj.IniciarTransaccion()
                codigo_doc = obj.Ejecutar("sp_registrardocumentodeudacobrar_v2", CDate(Me.txtFecha.Text), _
                             "001", "0001", Me.HdPersonal.Value, CDbl(Me.txtImporteTotal.Text), _
                             codigo_tip, 28, nroCuotas, tieneDeuda, 0)

                codigo_dpc = obj.Ejecutar("dbo.sp_generacabecera_prestamoadelanto_v2", _
                                Me.HdPersonal.Value, CDate(Me.txtFecha.Text), _
                                codigo_tip, Me.txtImporteTotal.Text, Me.cboRubro.SelectedValue, _
                                Me.txtSerie.Text, Me.txtNumeracion.Text, 0, "")


                For i As Integer = 0 To Me.gvCuotas.Rows.Count - 1
                    If (Me.gvCuotas.Rows(i).Visible = True) Then
                        obj.Ejecutar("dbo.sp_registradetalledeudacobrar", codigo_dpc(0), _
                                 Me.gvCuotas.Rows(i).Cells(0).Text, codigo_tip, _
                                 "", "", _
                                 Me.gvCuotas.Rows(i).Cells(4).Text, _
                                 RetornaNumeroMes(Me.gvCuotas.Rows(i).Cells(5).Text), _
                                 Me.gvCuotas.Rows(i).Cells(2).Text, _
                                 True, False, "P", 0, False, 0, 0, 0, "")
                    End If                    
                Next

                obj.TerminarTransaccion()
                Me.lblMensaje.Text = "Cargos generados correctamente"
                LimpiaFormulario()
            Catch ex As Exception
                obj.AbortarTransaccion()
                Me.lblMensaje.Text = "No se pudo guardar prestamo: " & ex.Message
            End Try
        End If        
    End Sub

    Private Sub LimpiaFormulario()
        Me.txtCuotas.Text = 1
        Me.txtImporte.Text = 0
        Me.txtImporteTotal.Text = 0

        Me.gvCuotas.DataSource = Nothing
        Me.gvCuotas.DataBind()
    End Sub

    Public Function RetornaNumeroMes(ByVal mes As String) As Integer
        Select Case mes.Trim.ToUpper
            Case "ENERO" : Return 1
            Case "FEBRERO" : Return 2
            Case "MARZO" : Return 3
            Case "ABRIL" : Return 4
            Case "MAYO" : Return 5
            Case "JUNIO" : Return 6
            Case "JULIO" : Return 7
            Case "AGOSTO" : Return 8
            Case "SEPTIEMBRE" : Return 9
            Case "OCTUBRE" : Return 10
            Case "NOVIEMBRE" : Return 11
            Case "DICIEMBRE" : Return 12
        End Select
    End Function

    Public Function RetornaFecha(ByVal mes As Integer, ByVal anio As Integer, ByVal tipo As String) As Date
        If (tipo = "I") Then
            Return DateSerial(anio, mes, 1)
        Else
            Return DateSerial(anio, mes + 1, 0)
        End If
    End Function

    Private Function validaFormulario() As Boolean
        If (Me.txtImporteTotal.Text.Trim.Length = 0) Then
            Me.lblMensaje.Text = "Debe ingresar un importe total"
            Return False
        ElseIf (Me.txtImporteTotal.Text = 0) Then
            Me.lblMensaje.Text = "El importe debe ser superior a 0"
            Return False
        End If

        If (Me.txtTrabajador.Text.Trim.Length = 0) Then
            Me.lblMensaje.Text = "No se encontro el trabajador"
            Return False
        End If

        Dim contador As Integer = 0
        For i As Integer = 0 To Me.gvCuotas.Rows.Count - 1
            If (Me.gvCuotas.Rows(i).Visible = True) Then
                contador = contador + 1
            End If
        Next

        If (contador = 0) Then
            Me.lblMensaje.Text = "No se encontraron detalles"
            Return False
        End If

        Return True
    End Function

    Protected Sub gvCuotas_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvCuotas.RowDeleted

    End Sub

    Protected Sub gvCuotas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCuotas.RowDeleting       
        Me.gvCuotas.Rows(e.RowIndex).Visible = False
        CalculaTotal()
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("frmConsultarIngresosEgresosPersonal.aspx")
    End Sub
End Class
