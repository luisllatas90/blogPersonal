Imports System.Globalization

Partial Class logistica_frmBusquedaSubasta
    Inherits System.Web.UI.Page

#Region "Métodos y funciones del Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","

            If Not IsPostBack Then
                CargarCategoria()
                LlenarSubasta()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImgBuscarSubasta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarSubasta.Click
        LlenarSubasta()
    End Sub

    Protected Sub gvSubastas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSubastas.PageIndexChanging
        gvSubastas.PageIndex = e.NewPageIndex
        LlenarSubasta()
    End Sub

    Protected Sub gvSubastas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSubastas.SelectedIndexChanged
        Try
            hfCodSubasta.Value = Me.gvSubastas.SelectedRow.Cells(0).Text
            pnlDetalle.CssClass = ""
            CargarProveedor(Convert.ToInt32(hfCodSubasta.Value))
            CargarArticulosCantidad(Convert.ToInt32(hfCodSubasta.Value))
            CargarPedidos(Convert.ToInt32(hfCodSubasta.Value))
            CargarDetalle(Me.gvSubastas.SelectedRow.Cells(2).Text, Me.gvSubastas.SelectedRow.Cells(1).Text, "Del " + Me.gvSubastas.SelectedRow.Cells(3).Text + " al " + Me.gvSubastas.SelectedRow.Cells(4).Text)
            CargarMensajes()
            dlMensajes.DataSource = Nothing
            dlMensajes.DataBind()
            CargarMensajes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvSubastas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSubastas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvSubastas','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, _
          ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i As Integer
        'Make the selected menu item reflect the correct imageurl
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/seArticulosSubasta.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/seProveedoresParticapantes.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/sePedidosIncluidos.JPG"
                ElseIf i = 3 Then
                    Menu1.Items(i).ImageUrl = "../images/seNegociacionProveedor.JPG"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/unsArticulosSubasta.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/unsProveedoresParticapantes.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/unsPedidosIncluidos.JPG"
                ElseIf i = 3 Then
                    Menu1.Items(i).ImageUrl = "../images/unsNegociacionProveedor.JPG"
                End If
            End If
        Next
    End Sub

    Protected Sub gvArticulos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArticulos.RowDataBound
        Try
            Dim chkPrecioBase As CheckBox
            chkPrecioBase = gvSubastas.SelectedRow.FindControl("chkPrecioBase")
            If chkPrecioBase.Checked Then
                e.Row.Cells(4).Visible = True
            Else
                e.Row.Cells(4).Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregarMensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarMensaje.Click
        LimpiarPanelMensaje()
        CargarMensajes()
        pnlMensaje.Visible = True
        btnAgregarMensaje.Visible = False
    End Sub

    Protected Sub ibtnGuardarMensaje_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGuardarMensaje.Click
        Try
            Dim objlog As New ClsLogistica
            objlog.AbrirTransaccionCnx()
            objlog.AgregarNegociacionSubasta(Convert.ToInt32(hfCodSubasta.Value), Convert.ToInt32(ddlProveedorSubasta.SelectedValue()), txtMensaje.Text, txtAsunto.Text, Convert.ToInt32(Request.QueryString("id")), "per")
            objlog.CerrarTransaccionCnx()
            pnlMensaje.Visible = False
            btnAgregarMensaje.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnEliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEliminar.Click
        pnlMensaje.Visible = False
        btnAgregarMensaje.Visible = True
    End Sub

    Protected Sub ddlProveedorMensaje_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProveedorMensaje.SelectedIndexChanged
        CargarMensajes()
    End Sub

    Protected Sub ibtnNuevaSubasta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnNuevaSubasta.Click
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmNuevaSubastaInversa.aspx?id=" & Request.QueryString("id") & "';", True)
    End Sub

    Protected Sub ibtnModificar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ibtnModificar As ImageButton
        ibtnModificar = sender
        Dim row As GridViewRow
        row = ibtnModificar.NamingContainer
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmNuevaSubastaInversa.aspx?id=" & Request.QueryString("id") & "&codSubasta=" & row.Cells.Item(0).Text & "';", True)
    End Sub

#End Region

#Region "Métodos de Usuario"

    Private Sub CargarCategoria()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarCategoria()
        objfun.CargarListas(ddlListadoCategoria, dts, "codCategoria", "desCategoria", "<< Seleccione >>")
    End Sub

    Private Sub CargarMensajes()
        Try
            Dim objlog As New ClsLogistica
            Dim dtMensajes As Data.DataTable
            dtMensajes = objlog.ConsultarMensajes(hfCodSubasta.Value, ddlProveedorMensaje.SelectedValue, Request.QueryString("id"))
            dlMensajes.DataSource = dtMensajes
            dlMensajes.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LlenarSubasta()
        Try
            Dim objlog As New ClsLogistica
            Dim dtSubasta As New Data.DataTable
            dtSubasta = objlog.ConsultarSusbastaInversa(0, IIf(ddlListadoCategoria.SelectedIndex = 0, 0, ddlListadoCategoria.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, 0, "", chkTodos.Checked)
            gvSubastas.DataSource = dtSubasta
            gvSubastas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarProveedor(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim objfun As New ClsFunciones
            Dim dtSubasta As New Data.DataTable
            dtSubasta = objlog.ConsultarSusbastaProveedor(codSubasta)
            gvProveedores.DataSource = dtSubasta
            gvProveedores.DataBind()
            objfun.CargarListas(ddlProveedorSubasta, dtSubasta, "idPro", "nombrePro", "<<Seleccione>>")
            objfun.CargarListas(ddlProveedorMensaje, dtSubasta, "idPro", "nombrePro", "<<Todos>>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarArticulosCantidad(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim dtArticulo As New Data.DataTable
            dtArticulo = objlog.ConsultarSusbastaArticuloCantidad(codSubasta)
            gvArticulos.DataSource = dtArticulo
            gvArticulos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarPedidos(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim dtPedidos As New Data.DataTable
            dtPedidos = objlog.ConsultarSusbastaPedido(codSubasta)
            gvPedidos.DataSource = dtPedidos
            gvPedidos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDetalle(ByVal fecRegistro As String, ByVal desCategoria As String, ByVal desVigencia As String)
        lblRegistroTab1.Text = fecRegistro
        lblRegistroTab2.Text = fecRegistro
        lblRegistroTab3.Text = fecRegistro
        lblRegistroTab4.Text = fecRegistro
        lblCategoriaTab1.Text = desCategoria
        lblCategoriaTab2.Text = desCategoria
        lblCategoriaTab3.Text = desCategoria
        lblCategoriaTab4.Text = desCategoria
        lblVigenciaTab1.Text = desVigencia
        lblVigenciaTab2.Text = desVigencia
        lblVigenciaTab3.Text = desVigencia
        lblVigenciaTab4.Text = desVigencia
    End Sub

    Private Sub LimpiarPanelMensaje()
        ddlProveedorSubasta.SelectedIndex = 0
        txtAsunto.Text = ""
        txtMensaje.Text = ""
    End Sub

#End Region

End Class
