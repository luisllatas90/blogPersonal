Imports System.Globalization
Imports System.Data
Partial Class administrativo_pec_frmPagoWebIngreso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        Total = 0
        Me.btnExportar.OnClientClick = "AbrirPopUp('rptListaIngresos.aspx?cco=" & ddpCentroCostos.SelectedValue.ToString & "&operador=" & ddlOperador.SelectedValue.ToString() & "&fecha=" & txtFecha.Text & "','600','800','yes','yes','yes','yes')"
        If (IsPostBack = False) Then
            CargaCentroCostos()
            CargaOperarios()
            CargarUsuario()
        End If
    End Sub

    Private Sub CargaCentroCostos()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddpCentroCostos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione <<")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargaOperarios()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddlOperador, obj.TraerDataTable("CAJ_ConsultaOperadoresPagoWeb"), "codigo_Per", "usuario_per")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If ddpCentroCostos.SelectedIndex < 1 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Importante", "alert('Debe de seleccionar un Centro de Costos.');", True)
            Exit Sub
        End If
        If ddlOperador.SelectedIndex < 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Importante", "alert('Debe de seleccionar el Operador');", True)
            Exit Sub
        End If
        CargarPagos()
    End Sub

    Private Sub CargarPagos()
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            grwListaPagos.DataSource = obj.TraerDataTable("CAJ_ConsultarPagoWebIngreso", Me.ddpCentroCostos.SelectedValue, txtFecha.Text.ToUpper, "", ddlOperador.SelectedValue)
            grwListaPagos.DataBind()
            obj.CerrarConexion()
            If grwListaPagos.Rows.Count > 0 Then
                btnExportar.Visible = True
            Else
                btnExportar.Visible = False
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

    Dim Total As Decimal = 0

    Protected Sub grwListaPagos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPagos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "pago_Deu"))
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Documentos emitidos: " & grwListaPagos.Rows.Count.ToString("d")
            e.Row.Cells(7).Text = "Total: "
            e.Row.Cells(8).Text = Total.ToString()

            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

    End Sub
End Class
