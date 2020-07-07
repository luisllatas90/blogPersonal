
Partial Class academico_frmMuestraPedidoOdonto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("codigo_alu") Is Nothing) Then
                Response.Redirect("../ErrorSistema.aspx")
            End If

            If (Request.QueryString("x") IsNot Nothing) Then
                Dim dt As New Data.DataTable
                Dim cls As New ClsConectarDatos
                cls.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
                cls.AbrirConexion()
                dt = cls.TraerDataTable("ODO_BuscaPedidoDeuda", Session("codigo_alu"), Request.QueryString("x"))
                cls.CerrarConexion()

                If (dt.Rows.Count > 0) Then
                    Me.lblFecha.Text = "<b>Fecha Pedido: </b>" & dt.Rows(0).Item("fechaReg_pod") & "   "
                    Me.lblHistoria.Text = "<b>Nro. Historia: </b>" & IIf(dt.Rows(0).Item("nroHistoria_pod") = 0, "No tiene", dt.Rows(0).Item("nroHistoria_pod")) & "   "
                    Me.lblPrecio.Text = "<b>Precio: </b>" & dt.Rows(0).Item("precioTotal_pod")

                    Me.gvDetalle.DataSource = dt
                    Me.gvDetalle.DataBind()
                Else
                    Me.lblMensaje.Text = "No se encontró el pedido"
                End If
            End If
        Catch ex As Exception
            Response.Write("Error al cargar los datos: " & ex.Message)
        End Try
    End Sub
End Class
