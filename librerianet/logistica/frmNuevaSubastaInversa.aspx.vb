Imports System.Web.UI.WebControls
Imports System.Globalization

Partial Class librerianet_logistica_frmNuevaSubastaInversa
    Inherits System.Web.UI.Page

#Region "Métodos de Usuario"

    Private Sub CargarUsuario()
        Dim objpre As New ClsPresupuesto
        Dim datos As New Data.DataTable
        datos = objpre.ObtenerDatosUsuario(hfidUsuario.Value)
        If datos.Rows.Count > 0 Then
            lblUsuario.Text = datos.Rows(0).Item("usuario")
            lblUsuarioCargo.Text = datos.Rows(0).Item("Cargo")
        End If
    End Sub

    Private Sub CargarCategoria()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarCategoria()
        objfun.CargarListas(ddlListadoCategoria, objlog.ConsultarCategoria, "codCategoria", "desCategoria", "<< Seleccione >>")
    End Sub

    Private Sub CreaDatatableDetalleSubasta()
        If Session("dtdSubasta") Is Nothing Then
            Dim dtdSubasta As New Data.DataTable
            dtdSubasta.Columns.Add("idArt", GetType(String))
            dtdSubasta.Columns.Add("descripcionArt", GetType(String))
            dtdSubasta.Columns.Add("cantidad", GetType(String))
            dtdSubasta.Columns.Add("total", GetType(String))
            dtdSubasta.Columns.Add("precioBase", GetType(String))
            Session("dtdSubasta") = dtdSubasta
        End If
    End Sub

    Private Sub LimpiarDetalleSubasta()
        pnlDetalle.Visible = True
        Dim dtdSubasta As New Data.DataTable
        dtdSubasta = Session("dtdSubasta")
        dtdSubasta.Rows.Clear()
        Session("dtdSubasta") = dtdSubasta
        gvDetalleSubasta.DataSource = dtdSubasta
        gvDetalleSubasta.DataBind()
        pnlDetalleInferior.Visible = False
    End Sub

    Private Sub AgregarFilaDetalleSubasta(ByVal idArt As String, ByVal descripcionArt As String, ByVal cantidadComprar As String, ByVal totalComprar As String)
        'If Session("dtdSubasta") Then
        Dim dtdSubasta As New Data.DataTable
        dtdSubasta = Session("dtdSubasta")
        For Each row As Data.DataRow In dtdSubasta.Rows
            If row("idArt") = idArt Then
                row("cantidad") = Convert.ToDecimal(cantidadComprar) + Convert.ToDecimal(row("cantidad"))
                row("total") = Convert.ToDecimal(totalComprar) + Convert.ToDecimal(row("total"))
                gvDetalleSubasta.DataSource = dtdSubasta
                gvDetalleSubasta.DataBind()
                Exit Sub
            End If
        Next
        Dim myrow As Data.DataRow
        myrow = dtdSubasta.NewRow
        myrow("idArt") = idArt
        myrow("descripcionArt") = descripcionArt
        myrow("cantidad") = cantidadComprar
        myrow("total") = totalComprar
        myrow("precioBase") = 0
        dtdSubasta.Rows.Add(myrow)
        gvDetalleSubasta.DataSource = dtdSubasta
        gvDetalleSubasta.DataBind()
        'End If
    End Sub

    Private Sub QuitarFilaDetalleSubasta(ByVal idArt As String, ByVal cantidadComprar As String, ByVal totalComprar As String)
        Dim dtdSubasta As New Data.DataTable
        dtdSubasta = Session("dtdSubasta")
        For Each row As Data.DataRow In dtdSubasta.Rows
            If row("idArt") = idArt Then
                row("cantidad") = Convert.ToDecimal(row("cantidad")) - Convert.ToDecimal(cantidadComprar)
                row("total") = Convert.ToDecimal(row("total")) - Convert.ToDecimal(totalComprar)
                If Convert.ToDecimal(row("cantidad")) = 0 And Convert.ToDecimal(row("total")) = 0 Then
                    dtdSubasta.Rows.Remove(row)
                End If
                Session("dtdSubasta") = dtdSubasta
                gvDetalleSubasta.DataSource = dtdSubasta
                gvDetalleSubasta.DataBind()
                Exit Sub
            End If
        Next
    End Sub

    Private Sub ObtenerPedidosSeleccionados()
        Dim count As Integer = 0
        Dim checkbox As CheckBox
        For Each row As GridViewRow In gvArticulos.Rows
            checkbox = row.FindControl("chkSeleccionar")
            If checkbox.Checked Then
                count = count + 1
            End If
        Next
    End Sub

    Private Sub CargarSubasta()
        Try
            Dim dtSubasta As New Data.DataTable
            Dim objLog As New ClsLogistica
            dtSubasta = objLog.ConsultarSusbastaInversa(hfcodSubasta.Value, 0, "", "", 0, "", False)
            txtFechaInicio.Text = dtSubasta.Rows(0).Item("fecInicio").ToString()
            txtFechaFin.Text = dtSubasta.Rows(0).Item("fecFin").ToString()
            txtDescripcion.Text = dtSubasta.Rows(0).Item("descripcion").ToString()
            chkMostrarMejorOferta.Checked = dtSubasta.Rows(0).Item("mejorOferta").ToString()
            chkMostrarPrecioBase.Checked = dtSubasta.Rows(0).Item("precioBase").ToString()
            ddlListadoCategoria.SelectedValue = dtSubasta.Rows(0).Item("codCategoria").ToString()
            hfidUsuario.Value = dtSubasta.Rows(0).Item("codigo_Per").ToString()
            hfcodCategoria.Value = dtSubasta.Rows(0).Item("codCategoria").ToString()
            CargarDetalleSubasta()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDetalleSubasta()
        Dim objLog As New ClsLogistica
        Dim dtPedido, dtProveedor As New Data.DataTable
        dtPedido = objLog.ConsultarSubastaPedidoComprar(ddlListadoCategoria.SelectedValue, hfcodSubasta.Value)
        dtProveedor = objLog.ConsultarProveedorSubasta(ddlListadoCategoria.SelectedValue, hfcodSubasta.Value)
        gvArticulos.DataSource = dtPedido
        gvProveedores.DataSource = dtProveedor

        gvArticulos.DataBind()
        gvProveedores.DataBind()
        CargarArticulosCantidad(hfcodSubasta.Value)
        pnlDetalle.Visible = True
        pnlDetalleInferior.Visible = True
    End Sub

    Private Sub CargarArticulosCantidad(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim dtArticulo As New Data.DataTable
            dtArticulo = objlog.ConsultarSusbastaArticuloCantidad(codSubasta)
            gvDetalleSubasta.DataSource = dtArticulo
            gvDetalleSubasta.DataBind()
            Session("dtdSubasta") = dtArticulo
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

#Region "Métodos del Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","

            If Not IsPostBack Then
                CargarCategoria()
                CreaDatatableDetalleSubasta()
                If Request.QueryString("codSubasta") <> Nothing Then
                    hfcodSubasta.Value = Request.QueryString("codSubasta")
                    CargarSubasta()
                Else
                    hfidUsuario.Value = Request.QueryString("id")
                End If
                CargarUsuario()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlListadoCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlListadoCategoria.SelectedIndexChanged
        If hfcodCategoria.Value = ddlListadoCategoria.SelectedValue Then
            CargarDetalleSubasta()
        Else
            Dim objlog As New ClsLogistica
            Dim dtPedido, dtProveedor As New Data.DataTable

            dtPedido = objlog.ConsultarArticulosComprar(ddlListadoCategoria.SelectedValue)
            dtProveedor = objlog.ConsultarProveedorCategoria(ddlListadoCategoria.SelectedValue)

            gvArticulos.DataSource = dtPedido
            gvProveedores.DataSource = dtProveedor

            gvArticulos.DataBind()
            gvProveedores.DataBind()
            LimpiarDetalleSubasta()
        End If
    End Sub

    Protected Sub chkSeleccionar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim checkbox As CheckBox
            checkbox = sender
            If checkbox.Checked Then
                pnlDetalleInferior.Visible = True
                Dim row As GridViewRow
                row = checkbox.NamingContainer
                AgregarFilaDetalleSubasta(row.Cells.Item(1).Text, row.Cells.Item(2).Text, row.Cells.Item(5).Text, row.Cells.Item(5).Text)
            Else
                Dim row As GridViewRow
                row = checkbox.NamingContainer
                QuitarFilaDetalleSubasta(row.Cells.Item(1).Text, row.Cells.Item(5).Text, row.Cells.Item(5).Text)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvDetalleSubasta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleSubasta.RowDataBound
        If chkMostrarPrecioBase.Checked Then
            e.Row.Cells(4).Visible = True
        Else
            e.Row.Cells(4).Visible = False
        End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtTotalComprar, txtPrecioBase As TextBox
            txtTotalComprar = e.Row.FindControl("txtTotalComprar")
            txtPrecioBase = e.Row.FindControl("txtPrecioBase")

            txtTotalComprar.Attributes.Add("onKeyPress", "validarnumero()")
            txtPrecioBase.Attributes.Add("onKeyPress", "validarnumero()")

        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If CDate(txtFechaFin.Text) < CDate(txtFechaInicio.Text) Then
            ClientScript.RegisterStartupScript(Me.GetType, "Incorrecto", "alert('El intervalo de fechas es incorrecto.')", True)
            Exit Sub
        End If
        If gvDetalleSubasta.Rows.Count = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Faltan ingresar artículos al detalle de la Subasta Inversa.')", True)
            Exit Sub
        End If
        Dim objLog As New ClsLogistica
        Dim codSubasta As Integer
        Dim cantidad, totalComprar, precioBase As Double
        Dim checkbox As CheckBox
        Dim txtTotalComprar, txtPrecioBase As TextBox
        Try
            objLog.AbrirTransaccionCnx()
            If Request.QueryString("codSubasta") <> Nothing Then
                codSubasta = hfcodSubasta.Value
                objLog.ActualizarTablasSubastaInversa(codSubasta, Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), chkMostrarPrecioBase.Checked, chkMostrarMejorOferta.Checked, ddlListadoCategoria.SelectedValue, txtDescripcion.Text)
            Else
                codSubasta = objLog.AgregarSubastaInversa(0, ddlListadoCategoria.SelectedValue, Convert.ToDateTime(txtFechaInicio.Text), Convert.ToDateTime(txtFechaFin.Text), txtDescripcion.Text, Request.QueryString("id"), chkMostrarPrecioBase.Checked, chkMostrarMejorOferta.Checked)
            End If
            For Each row As GridViewRow In gvProveedores.Rows
                checkbox = row.FindControl("chkSeleccionar")
                If checkbox.Checked Then
                    objLog.AgregarProveedoresSubasta(codSubasta, Convert.ToInt32(row.Cells.Item(0).Text), True)
                    For Each rowDet As GridViewRow In gvDetalleSubasta.Rows
                        txtTotalComprar = rowDet.FindControl("txtTotalComprar")
                        txtPrecioBase = rowDet.FindControl("txtPrecioBase")
                        totalComprar = Convert.ToDouble(txtTotalComprar.Text)
                        precioBase = Convert.ToDouble(IIf(txtPrecioBase.Text = "", 0, txtPrecioBase.Text))
                        objLog.AgregarSubastaProveedorOferta(codSubasta, Convert.ToInt32(rowDet.Cells.Item(0).Text), Convert.ToInt32(row.Cells.Item(0).Text), totalComprar, IIf(chkMostrarPrecioBase.Checked, precioBase, 0), 0)
                    Next
                End If
            Next
            For Each row As GridViewRow In gvArticulos.Rows
                checkbox = row.FindControl("chkSeleccionar")
                If checkbox.Checked Then
                    objLog.AgregarPedidosSubasta(codSubasta, Convert.ToInt32(row.Cells.Item(0).Text), Convert.ToInt32(row.Cells.Item(1).Text))
                End If
            Next
            For Each row As GridViewRow In gvDetalleSubasta.Rows
                txtTotalComprar = row.FindControl("txtTotalComprar")
                txtPrecioBase = row.FindControl("txtPrecioBase")
                cantidad = Convert.ToDouble(row.Cells.Item(2).Text)
                totalComprar = Convert.ToDouble(txtTotalComprar.Text)
                precioBase = Convert.ToDouble(IIf(txtPrecioBase.Text = "", 0, txtPrecioBase.Text))
                objLog.AgregarArticulosSubasta(codSubasta, Convert.ToInt32(row.Cells.Item(0).Text), cantidad, totalComprar, IIf(chkMostrarPrecioBase.Checked, precioBase, 0))
            Next
            objLog.CerrarTransaccionCnx()
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se guardaron los datos correctamente.');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmNuevaSubastaInversa.aspx?id=" & Request.QueryString("id") & "';", True)
        Catch ex As Exception
            objLog.CancelarTransaccionCnx()
            Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Ocurrió un error al procesar los datos: -" & ex.Message & "')", True)
        End Try
    End Sub

    Protected Sub chkMostrarPrecioBase_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMostrarPrecioBase.CheckedChanged
        Dim dtdSubasta As New Data.DataTable
        dtdSubasta = Session("dtdSubasta")
        gvDetalleSubasta.DataSource = dtdSubasta
        gvDetalleSubasta.DataBind()
    End Sub

#End Region

End Class
