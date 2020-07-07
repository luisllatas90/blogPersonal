Imports System.Globalization

Partial Class logistica_frmAprobacionSubasta
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
            pnlDetalle.CssClass = ""
            hfCodSubasta.Value = Me.gvSubastas.SelectedRow.Cells(0).Text
            CargarProveedor(Convert.ToInt32(hfCodSubasta.Value))
            CargarArticulosCantidad(Convert.ToInt32(hfCodSubasta.Value))
            CargarPedidos(Convert.ToInt32(hfCodSubasta.Value))
            lblFecInicioTab4.Text = Me.gvSubastas.SelectedRow.Cells(3).Text
            lblFecFinTab4.Text = Me.gvSubastas.SelectedRow.Cells(4).Text
            CargarDetalle(Me.gvSubastas.SelectedRow.Cells(2).Text, Me.gvSubastas.SelectedRow.Cells(1).Text, "Del " + Me.gvSubastas.SelectedRow.Cells(3).Text + " al " + Me.gvSubastas.SelectedRow.Cells(4).Text)
            If gvSubastas.SelectedRow.Cells(7).Text = "Aprobado" Then
                btnAprobar.Enabled = False
                btnAprobar.Text = "Aprobado"
            Else
                btnAprobar.Enabled = True
                btnAprobar.Text = "Aprobar Subasta"
            End If
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
        For i = 0 To Menu1.Items.Count - 1
            If i = e.Item.Value Then
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/seArticulosSubasta.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/seProveedoresParticapantes.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/sePedidosIncluidos.JPG"
                ElseIf i = 3 Then
                    Menu1.Items(i).ImageUrl = "../images/seAprobarSubasta.JPG"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/unsArticulosSubasta.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/unsProveedoresParticapantes.JPG"
                ElseIf i = 2 Then
                    Menu1.Items(i).ImageUrl = "../images/unsPedidosIncluidos.JPG"
                ElseIf i = 3 Then
                    Menu1.Items(i).ImageUrl = "../images/unsAprobarSubasta.JPG"
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

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Try
            Dim objlog As New ClsLogistica
            objlog.AbrirTransaccionCnx()
            objlog.ActualizarSubastaInversa(Convert.ToInt32(hfCodSubasta.Value))
            objlog.CerrarTransaccionCnx()
            EnviarMensaje()
            LlenarSubasta()
            btnAprobar.Enabled = False
            btnAprobar.Text = "Aprobado"
            pnlDetalle.CssClass = "hidden"
            MostrarMensaje("Se ha aprobado la Subasta Inversa.")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos y funciones de Usuario"

    Private Sub MostrarMensaje(ByVal msg As String)
        Dim sbMensaje As New StringBuilder()
        sbMensaje.Append("<script type='text/javascript'>")
        sbMensaje.AppendFormat("alert('{0}');", msg)
        sbMensaje.Append("</script>")
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "mensaje", sbMensaje.ToString())
    End Sub

    Private Sub EnviarMensaje()
        Dim ObjMailNet As New ClsMail
        Dim mensaje As String = GenerarMensaje()
        Dim hfEmailPro As HiddenField
        Dim para As String = ""
        For Each row As GridViewRow In gvProveedores.Rows
            hfEmailPro = row.FindControl("hfEmailPro")
            If hfEmailPro.Value <> "" Then
                para = "</br>" & row.Cells.Item(1).Text
                ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Subasta Inversa - Logística", "gcastillo@usat.edu.pe", "Registro de nueva Subasta Inversa", para & mensaje, True)
            End If
        Next
        'ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Subasta Inversa - Logística", "gcastillo@usat.edu.pe", "Registro de nueva Subasta Inversa", para & mensaje, True)
    End Sub

    Private Function GenerarMensaje() As String
        Dim strMensaje As String
        strMensaje = "</br>Se ha aprobado una nueva subasta con los siguientes Artículos: </br></br>"
        strMensaje = strMensaje & "<table border=1 width=100%><tr><td><b>Artículo</b></td><td><b>Cantidad</b></td><td><b>Total</b></td></tr>"
        For Each row As GridViewRow In gvArticulos.Rows
            strMensaje = strMensaje & "<tr><td>" & row.Cells.Item(1).Text & "</td><td>" & row.Cells.Item(2).Text & "</td><td>" & row.Cells.Item(3).Text & "</td></tr>"
        Next
        strMensaje = strMensaje & "</table>"
        Return strMensaje
    End Function

    Private Sub CargarCategoria()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarCategoria()
        objfun.CargarListas(ddlListadoCategoria, dts, "codCategoria", "desCategoria", "<< Seleccione >>")
    End Sub

    Private Sub LlenarSubasta()
        Try
            Dim objlog As New ClsLogistica
            Dim dtSubasta As New Data.DataTable
            dtSubasta = objlog.ConsultarSusbastaInversa(0, IIf(ddlListadoCategoria.SelectedIndex = 0, 0, ddlListadoCategoria.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, 0, "R", 0)
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
        lblCantArticulos.Text = gvArticulos.Rows.Count
        lblProveedores.Text = gvProveedores.Rows.Count
    End Sub

#End Region

End Class
