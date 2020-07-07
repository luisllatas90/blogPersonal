Imports System.Globalization

Partial Class logistica_frmEvaluacionPropuesta
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

    Protected Sub gvSubastas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvSubastas.PageIndexChanging
        gvSubastas.PageIndex = e.NewPageIndex
        LlenarSubasta()
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, _
      ByVal e As MenuEventArgs) Handles Menu1.MenuItemClick
        MultiView1.ActiveViewIndex = Int32.Parse(e.Item.Value)
        Dim i As Integer
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/seMontoArticulo.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/seOfertaProveedor.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/seNegociacion.JPG"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/unsMontoArticulo.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/unsOfertaProveedor.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/unsNegociacion.JPG"
                End If
            End If
        Next
    End Sub

    Protected Sub gvSubastas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvSubastas.SelectedIndexChanged
        Try
            hfCodSubasta.Value = Me.gvSubastas.SelectedRow.Cells(0).Text
            CargarDetalle(Me.gvSubastas.SelectedRow.Cells(2).Text, Me.gvSubastas.SelectedRow.Cells(1).Text, "Del " + Me.gvSubastas.SelectedRow.Cells(3).Text + " al " + Me.gvSubastas.SelectedRow.Cells(4).Text)
            CargarArticulosProveedor()
            CargarOfertaResumen()
            CargarProveedor(hfCodSubasta.Value)
            CargarMensajes()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvArticulos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArticulos.RowDataBound
        Try
            Dim chkPrecioBase As CheckBox
            chkPrecioBase = gvSubastas.SelectedRow.FindControl("chkPrecioBase")
            If chkPrecioBase.Checked Then
                e.Row.Cells(3).Visible = True
            Else
                e.Row.Cells(3).Visible = False
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

    Protected Sub ibtnVer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ibtnVer As ImageButton
            ibtnVer = sender
            Dim objLog As New ClsLogistica
            Dim row As GridViewRow
            row = ibtnVer.NamingContainer
            ibtnGrabar.Visible = False
            gvOfertaResumen.Visible = False
            btnRegresar.Visible = True
            gvDocumentos.Visible = True
            gvDocumentos.DataSource = objLog.ConsultarSusbastaDocumento(hfCodSubasta.Value, row.Cells.Item(0).Text)
            gvDocumentos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        ibtnGrabar.Visible = True
        gvOfertaResumen.Visible = True
        btnRegresar.Visible = False
        gvDocumentos.Visible = False
    End Sub

    Protected Sub gvOfertaResumen_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOfertaResumen.RowDataBound
        Dim hfIdPro As HiddenField = gvSubastas.SelectedRow.FindControl("hfIdPro")
        If hfFilas.Value = 1 Or hfIdPro.Value <> "0" Then
            e.Row.Cells(7).Visible = False
        Else
            e.Row.Cells(7).Visible = True
        End If
    End Sub

    Protected Sub ibtnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGrabar.Click
        ObtenerSeleccion()
    End Sub

    Protected Sub gvOfertaResumen_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvOfertaResumen.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim output As Literal = CType(e.Row.FindControl("rbtnMarkup"), Literal)
            output.Text = String.Format("<input type=radio name=Grupo id=RowSelector{0} value={0} />", e.Row.RowIndex)
        End If
    End Sub

#End Region

#Region "Métodos y funcionesde Usuario"

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
            dtSubasta = objlog.ConsultarSusbastaInversa(0, IIf(ddlListadoCategoria.SelectedIndex = 0, 0, ddlListadoCategoria.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, 0, "A", 0)
            gvSubastas.DataSource = dtSubasta
            gvSubastas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDetalle(ByVal fecRegistro As String, ByVal desCategoria As String, ByVal desVigencia As String)
        lblRegistroTab1.Text = fecRegistro
        lblRegistroTab2.Text = fecRegistro
        lblRegistroTab3.Text = fecRegistro
        lblCategoriaTab1.Text = desCategoria
        lblCategoriaTab2.Text = desCategoria
        lblCategoriaTab3.Text = desCategoria
        lblVigenciaTab1.Text = desVigencia
        lblVigenciaTab2.Text = desVigencia
        lblVigenciaTab3.Text = desVigencia
        btnRegresar.Visible = False
        gvDocumentos.Visible = False
        gvOfertaResumen.Visible = True
    End Sub

    Private Sub CargarArticulosProveedor()
        Try
            Dim objLog As New ClsLogistica
            gvArticulos.DataSource = objLog.ConsultarSusbastaProveedorOferta(hfCodSubasta.Value, 0)
            gvArticulos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarOfertaResumen()
        Try
            Dim objLog As New ClsLogistica
            Dim dt As Data.DataTable
            dt = objLog.ConsultarSusbastaOfertaResumen(hfCodSubasta.Value)
            hfFilas.Value = dt.Rows.Count
            gvOfertaResumen.DataSource = dt
            gvOfertaResumen.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarPanelMensaje()
        ddlProveedorSubasta.SelectedIndex = 0
        txtAsunto.Text = ""
        txtMensaje.Text = ""
    End Sub

    Private Sub CargarProveedor(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim objfun As New ClsFunciones
            Dim dtSubasta As New Data.DataTable
            dtSubasta = objlog.ConsultarSusbastaProveedor(codSubasta)
            objfun.CargarListas(ddlProveedorSubasta, dtSubasta, "idPro", "nombrePro", "<<Seleccione>>")
            objfun.CargarListas(ddlProveedorMensaje, dtSubasta, "idPro", "nombrePro", "<<Todos>>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ObtenerSeleccion()
        Try
            Dim idPro As Integer
            If hfFilas.Value = 1 Then
                idPro = gvOfertaResumen.Rows(0).Cells(0).Text
            Else
                idPro = gvOfertaResumen.Rows(Convert.ToInt32(Request.Form("Grupo"))).Cells(0).Text
            End If
            Dim clsLog As New ClsLogistica
            clsLog.AbrirTransaccionCnx()
            clsLog.ActualizarSubastaInversa(hfCodSubasta.Value, idPro)
            clsLog.CerrarTransaccionCnx()
            LlenarSubasta()
            CargarOfertaResumen()
            MostrarMensaje("Se ha elegido como ganador al proveedor " & gvOfertaResumen.Rows(Convert.ToInt32(Request.Form("Grupo"))).Cells(1).Text)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarMensaje(ByVal msg As String)
        Dim sbMensaje As New StringBuilder()
        sbMensaje.Append("<script type='text/javascript'>")
        sbMensaje.AppendFormat("alert('{0}');", msg)
        sbMensaje.Append("</script>")
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "mensaje", sbMensaje.ToString())
    End Sub

#End Region

End Class
