Imports System.Globalization
Imports System.Data
Partial Class administrativo_pec_frmDeudaPagoWeb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        If (IsPostBack = False) Then
            CargaCentroCostos()
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
        CargarDeudas()
    End Sub

    Private Sub CargarDeudas()
        Dim obj As New ClsConectarDatos
        'Try
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        grwListaDeudas.DataSource = obj.TraerDataTable("CAJ_ConsultarDeudaPendiente", Me.ddpCentroCostos.SelectedValue, txtNombre.Text.ToUpper)
        grwListaDeudas.DataBind()
        obj.CerrarConexion()
        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
    End Sub

    Private Sub CargarTipoDoc(ByVal ddl As DropDownList)
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(ddl, obj.TraerDataTable("CAJ_ConsultaDocumentosPagoWeb"), "codigo_Tdo", "descripcion_Tdo")
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub ibtnEditar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ibtnEditar As ImageButton
        Dim ibtnGuardar As ImageButton
        Dim row As GridViewRow
        Dim txtSaldo As TextBox
        Dim lblSaldo As Label
        Dim ddlTipoDoc As DropDownList
        Dim lblTipoDoc As Label
        ibtnEditar = sender
        row = ibtnEditar.NamingContainer
        txtSaldo = row.FindControl("txtSaldo")
        lblSaldo = row.FindControl("lblSaldo")
        ibtnGuardar = row.FindControl("ibtnGuardar")
        ddlTipoDoc = row.FindControl("ddlTipoDoc")
        lblTipoDoc = row.FindControl("lblTipoDoc")
        txtSaldo.Visible = True
        lblSaldo.Visible = False
        ibtnGuardar.Visible = True
        ibtnEditar.Visible = False
        ddlTipoDoc.Visible = True
        lblTipoDoc.Visible = False
    End Sub

    Protected Sub grwListaDeudas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaDeudas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '-----Saldo
            Dim txtSaldo As TextBox
            txtSaldo = e.Row.FindControl("txtSaldo")
            txtSaldo.Attributes.Add("onKeyPress", "validarnumero()")
            '-----Tipo Doc
            Dim ddlTipoDoc As DropDownList
            ddlTipoDoc = e.Row.FindControl("ddlTipoDoc")
            CargarTipoDoc(ddlTipoDoc)
            '-----lblTipoDoc
            Dim lblTipoDoc As Label
            lblTipoDoc = e.Row.FindControl("lblTipoDoc")
            lblTipoDoc.Text = ddlTipoDoc.SelectedItem.ToString
        End If
    End Sub

    Protected Sub ibtnGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim row As GridViewRow
        Dim ibtnGuardar As ImageButton
        Dim ibtnEditar As ImageButton
        Dim txtSaldo As TextBox
        Dim lblSaldo As Label
        Dim hfCodigo_Deu As HiddenField
        Dim ddlTipoDoc As DropDownList
        ibtnGuardar = sender
        row = ibtnGuardar.NamingContainer
        ibtnEditar = row.FindControl("ibtnEditar")
        txtSaldo = row.FindControl("txtSaldo")
        lblSaldo = row.FindControl("lblSaldo")
        hfCodigo_Deu = row.FindControl("hfCodigo_Deu")
        ddlTipoDoc = row.FindControl("ddlTipoDoc")
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("CAJ_InsertaDeudaPagoWeb", hfCodigo_Deu.Value, txtSaldo.Text, Request.QueryString("id").ToString, ddlTipoDoc.SelectedValue)
        obj.CerrarConexion()
        CargarDeudas()
        'txtSaldo.Visible = False
        'lblSaldo.Visible = True
        'ibtnGuardar.Visible = False
        'ibtnEditar.Visible = True
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
End Class
