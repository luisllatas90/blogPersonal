﻿'treyes
Partial Class presupuesto_areas_RegistrarPresupuestoDetalle_V2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Try
            If Not IsPostBack Then
                Dim objpre As New ClsPresupuesto
                Dim objfun As New ClsFunciones
                Dim datos As New Data.DataTable

                lblTitulo.Text = "Nuevo Presupuesto"

                If Request.QueryString("tipo") = "P" Then
                    hddId.Value = Request.QueryString("idPto")
                Else
                    hddId.Value = Session("idPto")
                End If

                'Cargar Datos de Usuario
                If Request.QueryString("tipo") = "P" Then
                    datos = objpre.ObtenerDatosUsuario(Request.QueryString("idPto"))
                Else
                    datos = objpre.ObtenerDatosUsuario(Session("idPto"))
                End If
                If datos.Rows.Count > 0 Then
                    lblUsuario.Text = datos.Rows(0).Item("usuario") & " ( " & datos.Rows(0).Item("Cargo") & " )"
                End If

                'Cargar Periodo Presupuestal
                If Request.QueryString("tipo") = "P" Then
                    datos = objpre.ObtenerPeriodoPresupuestal(Request.QueryString("ctfPto"))
                Else
                    datos = objpre.ObtenerPeriodoPresupuestal(Session("ctfPto"))
                End If
                If datos.Rows.Count > 0 Then
                    objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
                End If
                datos.Dispose()

                If Request.QueryString("tipo") <> "P" Then
                    cboPeriodoPresu_SelectedIndexChanged(cboPeriodoPresu, New System.EventArgs())
                End If

                txtCantidadAnual.Text = FormatNumber(0, 2)

                If Request.QueryString("tipo") = "E" Then ' E: Editar Presupuesto 
                    lblTitulo.Text = "Presupuesto: " & Session("actividadPto")
                    hddCodigo_Dpr.Value = Session("codigoDprPto")
                    CargarDatosEditarDetalle()
                    BloquearCabecera(False)
                    lnkBusquedaAvanzada.Visible = False
                ElseIf Request.QueryString("tipo") = "N" Then 'N: Nuevo detalle del mismo Presupuesto y Actividad_POA
                    lblTitulo.Text = "Presupuesto: " & Session("actividadPto")
                    hddCodigo_Dpr.Value = Session("codigoDprPto")
                    CargarDatosNuevoDetalle()
                    BloquearCabecera(False)
                    cboActividad.Enabled = False
                    lnkBusquedaAvanzada.Visible = False
                ElseIf Request.QueryString("tipo") = "P" Then 'P: Presupuesto Preliminar, viene del registro de programas y proyectos
                    lblTitulo.Text = "Presupuesto: " & Request.QueryString("actividadPto")
                    CargarDatosPresupuestoPreliminar(Request.QueryString("dapPto"))
                    BloquearCabecera(False)
                    cboActividad.Enabled = False
                    lnkBusquedaAvanzada.Visible = False
                Else
                    BloquearCabecera(True)
                End If

                Panel1.Visible = False
                Panel3.Visible = False
                MostrarBusquedaCeCos(False)
            End If
        Catch ex As Exception
            Response.Write("Error al cargar: " & ex.StackTrace)
        End Try

    End Sub
    Private Sub BloquearCabecera(ByVal valor As Boolean)
        Me.cboPeriodoPresu.Enabled = valor
        Me.cboCecos.Enabled = valor
        Me.txtCecos.Enabled = valor
    End Sub
    Private Sub CargarDatosEditarDetalle()
        Dim ObjPre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        Dim datosA As New Data.DataTable

        datos = ObjPre.ObtenerDetallePresupuesto_V2(hddCodigo_Dpr.Value)
        With datos.Rows(0)
            If datos.Rows.Count > 0 Then

                cboCecos.SelectedValue = .Item("codigo_cco")
                cboPeriodoPresu.SelectedValue = .Item("codigo_ejp")
                'HCANO
                'datosA = ObjPre.ObtenerActividadesPOA(cboCecos.SelectedValue, cboPeriodoPresu.SelectedValue)
                datosA = ObjPre.ObtenerActividadesPOA_v2(cboCecos.SelectedValue, cboPeriodoPresu.SelectedValue)
                '
                objfun.CargarListas(cboActividad, datosA, "codigo_dap", "descripcion_dap")
                cboActividad.SelectedValue = .Item("codigo_dap")
                rblMovimiento.SelectedValue = .Item("Tipo")
                txtConcepto.Text = .Item("Desestandar")
                txtComentarioReq.Text = .Item("detdescripcion")
                lblUnidad.Text = .Item("unidad")
                txtPrecioUnit.Text = .Item("preunitario")
                txtCantidadAnual.Text = .Item("cantidad")
                txtImporteAnual.Text = FormatNumber(.Item("subtotal"), 2)

                VerificarIngresosEgresos()
                gvDetalleEjecucion.Visible = True
                datosA = ObjPre.ObtenerMesesActividadPOA(cboActividad.SelectedValue)
                hddMesIni.Value = datosA.Rows(0).Item("MesIni")
                hddMesFin.Value = datosA.Rows(0).Item("MesFin")
                CargarGridMeses("Cantidad")
                txtPrecioUnit.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & "," & hddMesIni.Value & "," & hddMesFin.Value & ");")
                txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")

                Dim ControlTexto As New TextBox
                Dim mes As String

                If .Item("indicoCantidades") = True Then
                    For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                        Select Case i
                            Case 1 : mes = "canene"
                            Case 2 : mes = "canfeb"
                            Case 3 : mes = "canmar"
                            Case 4 : mes = "canabr"
                            Case 5 : mes = "canmay"
                            Case 6 : mes = "canjun"
                            Case 7 : mes = "canjul"
                            Case 8 : mes = "canago"
                            Case 9 : mes = "cansep"
                            Case 10 : mes = "canoct"
                            Case 11 : mes = "cannov"
                            Case 12 : mes = "candic"
                        End Select
                        ControlTexto.Text = .Item(mes)
                    Next
                Else
                    For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                        ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                        Select Case i
                            Case 1 : mes = "preene"
                            Case 2 : mes = "prefeb"
                            Case 3 : mes = "premar"
                            Case 4 : mes = "preabr"
                            Case 5 : mes = "premay"
                            Case 6 : mes = "prejun"
                            Case 7 : mes = "prejul"
                            Case 8 : mes = "preago"
                            Case 9 : mes = "presep"
                            Case 10 : mes = "preoct"
                            Case 11 : mes = "prenov"
                            Case 12 : mes = "predic"
                        End Select
                        ControlTexto.Text = .Item(mes)
                    Next
                End If
            End If
        End With

        OcultarMesesGrid()

        BuscarItems()
        gvItems.SelectedIndex = 0
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        SeleccionarItem()
    End Sub
    Private Sub CargarDatosNuevoDetalle()
        Dim ObjPre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        Dim datosA As New Data.DataTable

        datos = ObjPre.ObtenerDetallePresupuesto_V2(hddCodigo_Dpr.Value)
        With datos.Rows(0)
            If datos.Rows.Count > 0 Then
                cboCecos.SelectedValue = .Item("codigo_cco")
                cboPeriodoPresu.SelectedValue = .Item("codigo_ejp")
                datosA = ObjPre.ObtenerActividadesPOA(cboCecos.SelectedValue, cboPeriodoPresu.SelectedValue)
                objfun.CargarListas(cboActividad, datosA, "codigo_dap", "descripcion_dap")
                cboActividad.SelectedValue = .Item("codigo_dap")
                VerificarIngresosEgresos()
                gvDetalleEjecucion.Visible = True
                datosA = ObjPre.ObtenerMesesActividadPOA(cboActividad.SelectedValue)
                hddMesIni.Value = datosA.Rows(0).Item("MesIni")
                hddMesFin.Value = datosA.Rows(0).Item("MesFin")
                CargarGridMeses("Cantidad")
                txtPrecioUnit.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & "," & hddMesIni.Value & "," & hddMesFin.Value & ");")
                txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")

                OcultarMesesGrid()

            End If
        End With
    End Sub
    Private Sub CargarDatosPresupuestoPreliminar(ByVal codigo_dap As Integer)
        Dim ObjPre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        Dim datosA As New Data.DataTable

        datos = ObjPre.ObtenerDetalleActividadPOA(codigo_dap)

        If datos.Rows.Count > 0 Then
            objfun.CargarListas(cboCecos, datos, "codigo_cco", "descripcion_cco")
            cboCecos.SelectedValue = datos.Rows(0).Item("codigo_cco")
            cboPeriodoPresu.SelectedValue = datos.Rows(0).Item("codigo_ejp")
            objfun.CargarListas(cboActividad, datos, "codigo_dap", "descripcion_dap")
            cboActividad.SelectedValue = datos.Rows(0).Item("codigo_dap")
            trIngEgr.Visible = False
            gvDetalleEjecucion.Visible = True
            datosA = ObjPre.ObtenerMesesActividadPOA(cboActividad.SelectedValue)
            hddMesIni.Value = datosA.Rows(0).Item("MesIni")
            hddMesFin.Value = datosA.Rows(0).Item("MesFin")
            CargarGridMeses("Cantidad")
            txtPrecioUnit.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & "," & hddMesIni.Value & "," & hddMesFin.Value & ");")
            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")

            OcultarMesesGrid()
            hddCodigo_acp.value = datos.Rows(0).Item("codigo_acp")
            hddCodigo_poa.value = datos.Rows(0).Item("codigo_poa")
        End If

    End Sub
    Private Sub CargarGridMeses(ByVal Texto As String)
        Dim Tabla As Data.DataTable
        Tabla = New Data.DataTable
        Tabla.Columns.Add(New Data.DataColumn("cantidad", GetType(String)))
        AgregarFilaGrid(Tabla, "Valores") 'Texto)
        gvDetalleEjecucion.DataSource = Tabla
        gvDetalleEjecucion.DataBind()
    End Sub
    Private Sub AgregarFilaGrid(ByRef tabla As Data.DataTable, ByVal texto As String)
        Dim fila As Data.DataRow
        fila = tabla.NewRow()
        fila("cantidad") = texto
        tabla.Rows.Add(fila)
    End Sub
    Protected Sub gvDetalleEjecucion_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleEjecucion.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            For i As Int16 = 1 To 12
                Dim CajaTexto As New TextBox
                Dim validador As New CompareValidator
                CajaTexto.ID = "valor" & i
                CajaTexto.Text = "0"
                CajaTexto.Width = 50
                CajaTexto.Attributes.Add("onKeyPress", "validarnumero()")
                validador.ToolTip = "Debe ingresar un valor válido"
                validador.ErrorMessage = "Debe ingresar un valor válido (>0)"
                validador.Text = "*"
                validador.ControlToValidate = CajaTexto.ID
                validador.Operator = ValidationCompareOperator.GreaterThanEqual
                validador.ValidationGroup = "Guardar"
                validador.ValueToCompare = 0

                e.Row.Cells(i).Controls.Add(CajaTexto)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                e.Row.Cells(i).Controls.Add(validador)

                CajaTexto.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & "," & hddMesIni.Value & "," & hddMesFin.Value & ");")
            Next
        End If
    End Sub
    Protected Sub cboPeriodoPresu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodoPresu.SelectedIndexChanged
        Dim objpre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable

        cboCecos.Items.Clear()
        cboCecos.SelectedIndex = -1
        LimpiarCabecera()
        LimpiarDetalle()

        If cboPeriodoPresu.SelectedIndex <> -1 Then
            'hcano
            'datos = objpre.ObtenerCecoActividadPOA(hddId.Value, cboPeriodoPresu.SelectedValue, "")
            If Session("Ident_EvaPoa") = "evaPoa" Then ' 
                datos = objpre.ObtenerCecoActividadPOA_v2(cboPeriodoPresu.SelectedValue, "")
            Else
                datos = objpre.ObtenerCecoActividadPOA(hddId.Value, cboPeriodoPresu.SelectedValue, "")
            End If
            'hcano
            If datos.Rows.Count > 0 Then
                'objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<") ' treyes 28/11/2016 se modifica a solicitud de esaavedra
                objfun.CargarListas(cboCecos, datos, "codigo_Cco", "resumen_acp", ">> Seleccione <<") ' treyes 28/11/2016 se modifica a solicitud de esaavedra

            End If
        End If
        datos.Dispose()
        objpre = Nothing
        objfun = Nothing
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        Dim objPre As New ClsPresupuesto
        Dim objfun As New ClsFunciones
        Dim datos As New Data.DataTable
        Dim iIni, iFin As Integer

        LimpiarCabecera()
        LimpiarDetalle()

        If cboCecos.SelectedIndex <> -1 Then
            datos = objPre.ObtenerActividadesPOA(cboCecos.SelectedValue, cboPeriodoPresu.SelectedValue)
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboActividad, datos, "codigo_dap", "descripcion_dap", ">> Seleccione <<")
            End If
        End If

        If Panel3.Visible = True Then
            Panel3.Visible = False
        End If

        datos.Dispose()
        objPre = Nothing
        objfun = Nothing
    End Sub
    Protected Sub cboActividad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboActividad.SelectedIndexChanged
        Dim objPre As New ClsPresupuesto
        Dim datos As New Data.DataTable

        If cboActividad.SelectedIndex <> -1 Then
            VerificarIngresosEgresos()
            datos = objPre.ObtenerMesesActividadPOA(cboActividad.SelectedValue)
            hddMesIni.Value = datos.Rows(0).Item("MesIni")
            hddMesFin.Value = datos.Rows(0).Item("MesFin")
            CargarGridMeses("Cantidad")
            OcultarMesesGrid()
            gvDetalleEjecucion.Visible = True

            txtPrecioUnit.Attributes.Add("onBlur", "calcularvalores(" & txtImporteAnual.ID & ", " & txtCantidadAnual.ID & ", " & txtPrecioUnit.ID & "," & hddMesIni.Value & "," & hddMesFin.Value & ");")
            txtPrecioUnit.Attributes.Add("onKeyPress", "validarnumero()")
        End If
        
    End Sub
    Private Sub OcultarMesesGrid()
        For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
            gvDetalleEjecucion.Columns(i).Visible = True
            If i < hddMesIni.Value Or i > hddMesFin.Value Then
                gvDetalleEjecucion.Columns(i).Visible = False
            End If
        Next
    End Sub
    Private Sub VerificarIngresosEgresos()
        Dim objPre As New ClsPresupuesto
        Dim datos As New Data.DataTable

        rblMovimiento.Items(0).Enabled = True
        rblMovimiento.Items(1).Enabled = True
        If cboActividad.SelectedIndex <> -1 Then
            datos = objPre.ObtenerIngEgrActividadPOA(cboActividad.SelectedValue)
            If datos.Rows.Count > 0 Then
                trIngEgr.Visible = True
                If datos.Rows(0).Item("ingresos") = 0 Then
                    rblMovimiento.Items(0).Enabled = False
                End If
                If datos.Rows(0).Item("egresos") = 0 Then
                    rblMovimiento.Items(1).Enabled = False
                End If
                lblTopeIng.Text = "S/. " & datos.Rows(0).Item("ingresos")
                lblTopeEgr.Text = "S/. " & datos.Rows(0).Item("egresos")

                If datos.Rows(0).Item("ingresos") = 0 Then
                    lblDisponibleIng.Text = "S/. 0.00"
                    lblObservacionIng.Text = "-"
                Else
                    If datos.Rows(0).Item("disponibleIng") <= 0 Then
                        lblDisponibleIng.Text = "S/. 0.00"
                        If datos.Rows(0).Item("disponibleIng") = 0 Then
                            lblObservacionIng.Text = "Cumplió Meta"
                        Else
                            lblObservacionIng.Text = "Supera Meta en S/." & Math.Abs(datos.Rows(0).Item("disponibleIng"))
                        End If
                    Else
                        lblDisponibleIng.Text = "S/. " & datos.Rows(0).Item("disponibleIng")
                        lblObservacionIng.Text = "-"
                    End If
                End If
                
                If datos.Rows(0).Item("egresos") = 0 Then
                    lblDisponibleEgr.Text = "S/. 0.00"
                    lblObservacionEgr.Text = "-"
                Else
                    If datos.Rows(0).Item("disponibleEgr") <= 0 Then
                        lblDisponibleEgr.Text = "S/. 0.00"
                        If datos.Rows(0).Item("disponibleEgr") = 0 Then
                            lblObservacionEgr.Text = "Cumplió Tope"
                        Else
                            lblObservacionEgr.Text = "Excede Tope en S/." & Math.Abs(datos.Rows(0).Item("disponibleEgr"))
                        End If
                    Else
                        lblDisponibleEgr.Text = "S/. " & datos.Rows(0).Item("disponibleEgr")
                        lblObservacionEgr.Text = "-"
                    End If
                End If
                
            Else
                trIngEgr.Visible = False
            End If
        End If
        objPre = Nothing
    End Sub
    Protected Sub ImgBuscarItems_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarItems.Click
        BuscarItems()
    End Sub
    Private Sub BuscarItems()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvItems.DataSource = objPre.ConsultarConceptos_V2(rblMovimiento.SelectedValue, txtConcepto.Text)
        gvItems.DataBind()
        objPre = Nothing
        Panel1.Visible = True
    End Sub
    Protected Sub gvItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvItems.SelectedIndexChanged
        txtCodItem.Text = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0)
        Me.txtConcepto.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(1).Text)
        Me.txtPrecioUnit.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(3).Text)
        Me.lblUnidad.Text = HttpUtility.HtmlDecode(gvItems.SelectedRow.Cells(2).Text)
        SeleccionarItem()
    End Sub
    Private Sub SeleccionarItem()
        If Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3).ToString.Trim() = True Then
            lblValores.Text = "Cantidad"
            lblTexto.Text = "Precio Unitario (S/.)"
        Else
            lblValores.Text = "Importe"
            lblTexto.Text = "Cantidad"
        End If
        gvItems.Dispose()
        Panel1.Visible = False
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjPre As New ClsPresupuesto
        Dim codigoArt, codigoRub, codigoPlla As Int64
        Dim iduni, rptaCab As Integer
        Dim indicoCant As Byte
        Dim tipo As String = ""
        Dim CantidadTotal As Decimal
        Dim rpta, OK As Byte

        If txtCodItem.Text <> "" And cboCecos.Visible = True Then

            cmdGuardar.Enabled = False
            codigoArt = 0
            codigoPlla = 0
            codigoRub = 0
            rpta = 0
            rptaCab = 0
            indicoCant = 0

            AsignarDatos(tipo, indicoCant, iduni, codigoRub, codigoArt, codigoPlla, CantidadTotal)
            If Request.QueryString("tipo") = "P" Then
                rpta = 1
            Else
                VerificarTechosPresupuestales_V2(rpta)
            End If

            If rpta = 1 Then
                ObjPre.AbrirTransaccionCnx()

                If Request.QueryString("tipo") = "E" Then
                    OK = ObjPre.EliminarDetallePresupuesto_V2(CInt(hddCodigo_Dpr.Value), CInt(hddId.Value))
                    hddForzar.Value = 1
                Else
                    OK = 1
                End If

                If OK = 1 Then 'Edicion, Nuevo
                    '*** Guarda cabecera de presupuesto ***
                    rptaCab = ObjPre.AgregarPresupuesto_V2(cboPeriodoPresu.SelectedValue, cboCecos.SelectedValue, CInt(codigoArt), CInt(codigoRub), CInt(codigoPlla), _
                                                        txtComentarioReq.Text, iduni, CDec(txtPrecioUnit.Text), CantidadTotal, 1, _
                                                        rblMovimiento.SelectedValue, hddId.Value, indicoCant, hddForzar.Value, cboActividad.SelectedValue)

                    '*** Muestra mensaje de retorno ***
                    Dim rptaDet As Integer
                    Select Case rptaCab
                        Case 0
                            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos');", True)
                        Case -1
                            ClientScript.RegisterStartupScript(Me.GetType, "Periodo no activo", "alert('El Ejercicio presupuestal esta cerrado actualmente, no se puede registrar el presupuesto');", True)
                        Case -2
                            ClientScript.RegisterStartupScript(Me.GetType, "Proceso no disponible", "alert('El estado actual del presupuesto, no permite que sea modificado');", True)
                        Case -3
                            ClientScript.RegisterStartupScript(Me.GetType, "Existente", "divConfirmar.style.visibility='visible'", True)
                            ObjPre.CancelarTransaccionCnx()
                            Exit Sub
                        Case Else
                            If rptaCab > 0 Then
                                Dim ControlTexto As New TextBox
                                Dim CantidadMes, PrecioMes As Decimal
                                Dim sw1, sw2 As Int16
                                sw1 = sw2 = 0
                                Dim Meses() As String = New String() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SETIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"}

                                For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
                                    ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
                                    If CBool(indicoCant) = True Then
                                        CantidadMes = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                                        rptaDet = ObjPre.AgregarDetalleEjecucion_V2(rptaCab, Meses(i - 1), CDec(txtPrecioUnit.Text), CDec(CantidadMes))
                                    Else
                                        PrecioMes = CDbl(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
                                        rptaDet = ObjPre.AgregarDetalleEjecucion_V2(rptaCab, Meses(i - 1), CDec(PrecioMes), 1)
                                    End If

                                    sw1 += rptaDet

                                Next
                                If sw1 = 0 Then
                                    ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('El proceso no está disponible');", True)
                                ElseIf sw1 > 0 Then
                                    ObjPre.CerrarTransaccionCnx()
                                    ClientScript.RegisterStartupScript(Me.GetType, "Correcto", "alert('Se registraron correctamente los datos');", True)
                                    'hcano
                                    If Session("Ident_EvaPoa") = "evaPoa" Then
                                        Response.Redirect("ModificarPresupuesto_V3-HC.aspx?op=evaPOA&id=" & Session("idPto") & "&ctf=" & Session("ctfPto") & "&cb1=" & Session("cb1_evapto") & "&cb2=" & Session("cb2_evapto") & "&cb3=" & Session("cb3_evapto") & "&cb4=" & Session("cb4_evapto"))
                                    End If
                                    'hcano
                                End If
                            End If
                    End Select
                End If
            End If
            LimpiarDetalle()
            If Request.QueryString("tipo") <> "P" Then
                VerificarIngresosEgresos()
            End If
            CargarGridMeses("Cantidad")
            OcultarMesesGrid()
            gvDetalleEjecucion.Visible = True
            cmdGuardar.Enabled = True
        Else
            'ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('Debe seleccionar un item existente, si desea crear un nuevo item comuniquese con Cesar Cama');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "No disponible", "alert('Debe seleccionar un item existente, si desea crear un nuevo ítem comuníquese con el área de Finanzas');", True)
        End If
    End Sub
    Private Sub VerificarTechosPresupuestales_V2(ByRef rpta As Byte)
        If rblMovimiento.SelectedValue = "I" Then
            rpta = 1
        Else
            If rblMovimiento.SelectedValue = "E" And Decimal.Parse(txtImporteAnual.Text) <= Decimal.Parse(lblDisponibleEgr.Text.Replace("S/.", "")) Then
                rpta = 1
            Else
                'ClientScript.RegisterStartupScript(Me.GetType, "TopeExedido", "alert('No se puede registrar debido a que se excede el tope presupuestal asignado');", True)
                ClientScript.RegisterStartupScript(Me.GetType, "TopeExedido", "alert('Importe Total excede el tope presupuestal asignado');", True)
                rpta = 1
            End If
        End If
    End Sub
    Private Sub LimpiarCabecera()
        cboActividad.Items.Clear()
        cboActividad.SelectedIndex = -1
        lblDisponibleIng.Text = ""
        lblDisponibleEgr.Text = ""
        lblTopeIng.Text = ""
        lblTopeEgr.Text = ""
        trIngEgr.Visible = False
    End Sub
    Private Sub LimpiarDetalle()
        Dim ControlTexto As New TextBox

        rblMovimiento.ClearSelection()
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        lblUnidad.Text = ""
        txtCantidadAnual.Text = 0
        txtComentarioReq.Text = ""
        txtImporteAnual.Text = 0

        gvDetalleEjecucion.Visible = False
    End Sub
    Private Sub AsignarDatos(ByRef tipo As String, ByRef indicoCant As Byte, ByRef iduni As Int16, _
                             ByRef codigorub As Int64, ByRef codigoart As Int64, ByRef codigoPlla As Int64, _
                             ByRef CantidadTotal As Decimal)

        tipo = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(1).ToString.Trim()
        indicoCant = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(3)
        iduni = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(2).ToString.Trim
        If tipo = "R" Then
            codigorub = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        ElseIf tipo = "A" Then
            codigoart = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        ElseIf tipo = "P" Then
            codigoPlla = Me.gvItems.DataKeys.Item(Me.gvItems.SelectedIndex).Values(0).ToString.Trim
        End If
        If indicoCant = 1 Then
            CantidadTotal = CalcularCantidadTotal()
        Else
            CantidadTotal = 1
        End If

        txtCantidadAnual.Text = CantidadTotal
        txtImporteAnual.Text = CantidadTotal * txtPrecioUnit.Text

    End Sub
    Private Function CalcularCantidadTotal() As Decimal
        Dim ControlTexto As New TextBox
        Dim CantidadTotal As Decimal = 0

        For i As Int16 = 1 To gvDetalleEjecucion.Columns.Count - 1
            ControlTexto = Me.gvDetalleEjecucion.Controls(0).Controls(1).Controls(i).Controls(0)
            CantidadTotal += CDec(IIf(ControlTexto.Text = "", 0, ControlTexto.Text))
        Next
        Return CantidadTotal
    End Function
    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click, cmdCerrar.Click
        hddForzar.Value = 0
        ClientScript.RegisterStartupScript(Me.GetType, "Existente", "javascript:divConfirmar.style.visibility='hidden'", True)
    End Sub
    Protected Sub cmdOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        hddForzar.Value = 1
        cmdGuardar_Click(sender, e)
    End Sub
    Protected Sub rblMovimiento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblMovimiento.SelectedIndexChanged
        txtConcepto.Text = ""
        txtCodItem.Text = ""
        txtPrecioUnit.Text = ""
        lblUnidad.Text = ""
        txtCantidadAnual.Text = 0
        txtComentarioReq.Text = ""
        txtImporteAnual.Text = 0
    End Sub
    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click

        If Request.QueryString("op") = "modPto" Then
            'Hcano 
            'Response.Redirect("ModificarPresupuesto_V3.aspx?op=regPto")
            If Session("Ident_EvaPoa") = "evaPoa" Then
                Response.Redirect("ModificarPresupuesto_V3-HC.aspx?op=evaPOA&id=" & Session("idPto") & "&ctf=" & Session("ctfPto") & "&cb1=" & Session("cb1_evapto") & "&cb2=" & Session("cb2_evapto") & "&cb3=" & Session("cb3_evapto") & "&cb4=" & Session("cb4_evapto"))
            Else
                Response.Redirect("ModificarPresupuesto_V3.aspx?op=regPto")
            End If
            'Hcano

        ElseIf Request.QueryString("op") = "gesPto" Then
            Response.Redirect("GestionarPresupuesto.aspx")
        ElseIf Request.QueryString("tipo") = "P" Then
            Response.Redirect("../../../personal/indicadores/POA/FrmMantenimientoActividadesPOA.aspx?back=pto&id=" & Request.QueryString("idPto") & "&ctf=" & Request.QueryString("ctfPto") & "&codigo_poa=" & Request.QueryString("codigo_poa") & "&codigo_acp=" & Request.QueryString("codigo_acp") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4") & "&tipo_acc=" & Request.QueryString("tipo_acc") & "&index_POa=" & Request.QueryString("index_POa"))
        End If

    End Sub
    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
            Panel3.Visible = False
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub
    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub
    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub
    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvCecos.DataSource = objPre.ObtenerCecoActividadPOA(hddId.Value, cboPeriodoPresu.SelectedValue, txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub
    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        cboCecos_SelectedIndexChanged(cboCecos, New System.EventArgs())
    End Sub
End Class
