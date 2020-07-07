
Partial Class logistica_frmFaltaConformidad
    Inherits System.Web.UI.Page

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Me.txtBuscar.Text.Trim <> "") Then
            CargaPedidos(Me.txtBuscar.Text, Request.QueryString("id"))
        Else
            CargaPedidos(0, Request.QueryString("id"))
        End If
    End Sub

    Private Sub BuscarDetallePedidos(ByVal Pedido As Integer, ByVal Personal As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("LOG_ListaDetallePedidosConformidad", Pedido, 0)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvPedidos.DataSource = dt
            Me.gvPedidos.DataBind()

            Me.lblAviso.Text = ""
        Catch ex As Exception
            Me.lblAviso.Text = ex.Message
            obj = Nothing
        End Try
    End Sub

    Private Sub CargaPedidos(ByVal codigo_Ped As Integer, ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("LOG_ListaPedidosxPersonalSinConfirmar", codigo_Ped, 0)
            obj.CerrarConexion()
            obj = Nothing

            Me.gvCabecera.DataSource = dt
            Me.gvCabecera.DataBind()

            Me.lblAviso.Text = ""
        Catch ex As Exception
            Me.lblAviso.Text = "Error al cargar pedidos: " & ex.Message
            obj = Nothing
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            txtBuscar.Attributes.Add("onKeyPress", "validarnumero()")
            If (Request.QueryString("id") IsNot Nothing) Then
                CargaPedidos(0, Request.QueryString("id"))
            Else
                Me.btnBuscar.Visible = False
                Me.txtBuscar.Visible = False
                Me.btnConfirma.Visible = False
            End If
        End If
    End Sub


    Protected Sub btnConfirma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirma.Click
        Try
            Dim clsLog As New ClsLogistica
            Dim dt_Per As New Data.DataTable
            Dim cnx As New ClsConectarDatos
            Dim correo As String = ""

            If (Me.gvPedidos.Rows.Count > 0) Then                
                cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

                cnx.AbrirConexion()
                dt_Per = cnx.TraerDataTable("LOG_BuscaPersonal", Request.QueryString("id"), "")
                cnx.CerrarConexion()

                If (dt_Per.Rows.Count > 0) Then
                    If (dt_Per.Rows(0).Item("usuario_per").ToString.Trim <> "") Then
                        correo = dt_Per.Rows(0).Item("usuario_per").ToString & "@usat.edu.pe"
                    End If
                End If

                Dim Fila As GridViewRow
                Dim cont As Integer = 0
                For i As Integer = 0 To Me.gvPedidos.Rows.Count - 1
                    'Capturamos las filas que estan activas
                    Fila = Me.gvPedidos.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked
                    If (valor = True) Then
                        clsLog.EnvioSolicitaConfirmacionPedido(Me.gvPedidos.DataKeys(i).Values(1), Me.gvPedidos.Rows(i).Cells(1).Text, Me.gvPedidos.Rows(i).Cells(3).Text, Me.gvPedidos.Rows(i).Cells(4).Text, Me.HdPerCabecera.Value, Me.gvPedidos.Rows(i).Cells(6).Text, correo)
                    End If
                Next

                Me.lblAviso.Text = "Se envio el correo solicitando confirmación."
            Else
                Me.lblAviso.Text = "No existen detalles en el pedido."
            End If
        Catch ex As Exception
            Me.gvPedidos.DataSource = Nothing
            Me.gvPedidos.DataBind()
            Me.lblAviso.Text = "Error: " & ex.Message
        End Try
    End Sub

    Protected Sub gvCabecera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabecera.SelectedIndexChanged
        Me.HdPerCabecera.Value = Me.gvCabecera.SelectedDataKey.Item(0)
        BuscarDetallePedidos(Me.gvCabecera.SelectedRow.Cells(0).Text, Request.QueryString("id"))
    End Sub

    Protected Sub gvCabecera_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCabecera.PageIndexChanging
        Me.gvCabecera.PageIndex = e.NewPageIndex()
        If (Me.txtBuscar.Text.Trim <> "") Then
            CargaPedidos(Me.txtBuscar.Text, Request.QueryString("id"))
        Else
            CargaPedidos(0, Request.QueryString("id"))
        End If
    End Sub

    Protected Sub gvPedidos_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPedidos.DataBound
        Dim Fila As GridViewRow
        For i As Integer = 0 To Me.gvPedidos.Rows.Count - 1
            Fila = Me.gvPedidos.Rows(i)
            If (Me.gvPedidos.Rows(i).Cells(5).Text.Trim = "Atendido") Then
                CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = True                
            Else
                CType(Fila.FindControl("chkElegir"), CheckBox).Enabled = False
            End If
        Next
    End Sub
End Class
