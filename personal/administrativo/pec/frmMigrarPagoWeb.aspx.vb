Imports System.Globalization
Imports System.Data
Partial Class administrativo_pec_frmMigrarPagoWeb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        If (IsPostBack = False) Then
            CargaCentroCostos()
            CargarUsuario()
        End If
    End Sub

    Private Sub CargaCentroCostos()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddpCentroCostos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If ddpCentroCostos.SelectedIndex < 1 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Importante", "alert('Debe de seleccionar un Centro de Costos.');", True)
            Exit Sub
        End If
        CargarPagos()
    End Sub

    Private Sub CargarPagos()
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            grwListaPagos.DataSource = obj.TraerDataTable("CAJ_ConsultarPagoWeb", Me.ddpCentroCostos.SelectedValue, txtFecha.Text.ToUpper, "")
            grwListaPagos.DataBind()
            obj.CerrarConexion()
            If grwListaPagos.Rows.Count > 0 Then
                btnMigrar.Visible = True
            Else
                btnMigrar.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargarUsuario()
        Dim objPre As New ClsPresupuesto
        Dim dt As New DataTable
        dt = objPre.ObtenerDatosUsuario(Request.QueryString("id"))
        If dt.Rows.Count > 0 Then
            hfUsuReg.Value = dt.Rows(0).Item("usuario_per").Replace("USAT\", "")
        End If
    End Sub

    Protected Sub btnMigrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMigrar.Click
        If ValidarSeleccionados() = 0 Then Exit Sub
        MigrarPagoWeb()
    End Sub

    Private Sub MigrarPagoWeb()
        Dim chk As CheckBox
        Dim hf_ID_pagoweb As HiddenField
        Dim numLiq As String
        Dim Codigo_cin As Integer
        Dim dtDatosPago As DataTable
        Dim obj As New ClsConectarDatos
        Dim ValoresDevueltos(0) As Object
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            numLiq = obj.TraerDataTable("ConsultarParametroCaja", "ST").Rows(0).Item("ultimoNroLiquidCaja_Par").ToString.PadLeft(12, "0")
            obj.CerrarConexion()

            obj.IniciarTransaccion()
            For Each row As GridViewRow In grwListaPagos.Rows
                chk = row.FindControl("chkSeleccionar")
                '--- Si la fila ha sido seleccionada
                If chk.Checked Then
                    hf_ID_pagoweb = row.FindControl("hf_ID_pagoweb")
                    dtDatosPago = obj.TraerDataTable("CAJ_ConsultarDeudaPagoWeb", hf_ID_pagoweb.Value)
                    obj.Ejecutar("AgregarCajaIngreso_v1", CObj(dtDatosPago.Rows(0).Item("codigo_Tdo")), "", Now.ToShortDateString, dtDatosPago.Rows(0).Item("tipoResp_Deu").ToString() _
                                 , CObj(dtDatosPago.Rows(0).Item("codigo_Alu")), CObj(dtDatosPago.Rows(0).Item("codigo_Per")) _
                                 , CObj(dtDatosPago.Rows(0).Item("codigo_Pot")), dtDatosPago.Rows(0).Item("participante").ToString() _
                                 , "R", dtDatosPago.Rows(0).Item("pago_Deu"), "S/. ", numLiq, 0, CObj(DBNull.Value), "0", 0, 1, dtDatosPago.Rows(0).Item("usuario_per") _
                                 , dtDatosPago.Rows(0).Item("codigo_per_reg"), 2, System.Net.Dns.GetHostName(), 1, 1.0, 0).copyto(ValoresDevueltos, 0)
                    Codigo_cin = ValoresDevueltos(0)
                    obj.Ejecutar("CAJ_ActualizarPagoWeb", hf_ID_pagoweb.Value, Codigo_cin)
                    obj.Ejecutar("AgregarDetalleCajaIngreso_v1", Codigo_cin, 1, dtDatosPago.Rows(0).Item("codigo_Sco") _
                                 , dtDatosPago.Rows(0).Item("pago_Deu"), 1, CDbl(dtDatosPago.Rows(0).Item("pago_Deu")) * 1 _
                                 , "CampusVirtual Módulo de Eventos - Pago Web Fecha: " & dtDatosPago.Rows(0).Item("fecha_Pago").ToString() _
                                 , CObj(dtDatosPago.Rows(0).Item("codigo_Cac")), 0, dtDatosPago.Rows(0).Item("codigo_Deu").ToString() _
                                 , 0, 1, 1.0)
                End If
            Next
            obj.TerminarTransaccion()
            ClientScript.RegisterStartupScript(Me.GetType, "Importante", "alert('Se realizó la migración exitosamente.');", True)
            CargarPagos()
        Catch ex As Exception
            obj.AbortarTransaccion()
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function ValidarSeleccionados() As Integer
        Dim count As Integer = 0
        Dim chk As CheckBox
        For Each row As GridViewRow In grwListaPagos.Rows
            chk = row.FindControl("chkSeleccionar")
            If chk.Checked Then
                count = count + 1
            End If
        Next
        Return count
    End Function

    Protected Sub ddpCentroCostos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddpCentroCostos.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        Dim dt As DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_BuscaCCOPermisoPagoWeb", Me.ddpCentroCostos.SelectedValue)
            obj.CerrarConexion()
            If CBool(dt.Rows(0).Item("permiso")) Then
                tbBuscar.Visible = True
                tbMensaje.Visible = False
            Else
                tbBuscar.Visible = False
                tbMensaje.Visible = True
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
