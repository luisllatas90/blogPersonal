Imports System.Globalization

Partial Class logistica_frmListaConvocatoriaProveedor
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
            CargarArticulosCantidad(Convert.ToInt32(hfCodSubasta.Value))
            CargarDetalle(Me.gvSubastas.SelectedRow.Cells(2).Text, Me.gvSubastas.SelectedRow.Cells(1).Text, "Del " + Me.gvSubastas.SelectedRow.Cells(3).Text + " al " + Me.gvSubastas.SelectedRow.Cells(4).Text)
            cargarMensajes()
            CargarDocumentos()
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
    Protected Sub gvFile_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFile.PageIndexChanging
        gvFile.PageIndex = e.NewPageIndex
        CargarDocumentos()
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
                    Menu1.Items(i).ImageUrl = "../images/seNegociacion.JPG"
                End If
            Else
                If i = 0 Then
                    Menu1.Items(i).ImageUrl = "../images/unsArticulosSubasta.JPG"
                ElseIf i = 1 Then
                    Menu1.Items(i).ImageUrl = "../images/unsNegociacion.JPG"
                End If
            End If
        Next
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

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim txtPrecioXUnidad As TextBox
                txtPrecioXUnidad = e.Row.FindControl("txtPrecioXUnidad")
                txtPrecioXUnidad.Attributes.Add("onKeyPress", "validarnumero()")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregarMensaje_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarMensaje.Click
        pnlMensaje.Visible = True
        btnAgregarMensaje.Visible = False
    End Sub

    Protected Sub ibtnEliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ibtnEliminar As ImageButton
        Dim hfRuta As HiddenField
        Dim row As GridViewRow
        Dim objlog As New ClsLogistica
        ibtnEliminar = sender
        row = ibtnEliminar.NamingContainer
        hfRuta = row.FindControl("hfRuta")
        objlog.EliminarDocumentoSubasta(hfCodSubasta.Value, idPro.Value, hfRuta.Value)
        System.IO.File.Delete(hfRuta.Value)
        CargarDocumentos()
    End Sub

    Protected Sub ibtnEnviar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ibtnEliminar As ImageButton
            Dim hfRuta As HiddenField
            Dim row As GridViewRow
            Dim objlog As New ClsLogistica
            ibtnEliminar = sender
            row = ibtnEliminar.NamingContainer
            hfRuta = row.FindControl("hfRuta")
            objlog.AbrirTransaccionCnx()
            objlog.ActualizarSubastaDocumento(hfCodSubasta.Value, idPro.Value, hfRuta.Value, "E")
            objlog.CerrarTransaccionCnx()
            CargarDocumentos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If FileArchivo.HasFile Then
                Dim filePath As String
                Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileArchivo.FileName).ToString
                filePath = Server.MapPath("../../filesSubastas")
                filePath = filePath & "\" & hfCodSubasta.Value
                Dim carpeta As New System.IO.DirectoryInfo(filePath)
                If carpeta.Exists = False Then
                    carpeta.Create()
                End If
                filePath = filePath & "\" & idPro.Value
                Dim subCarpeta As New System.IO.DirectoryInfo(filePath)
                If subCarpeta.Exists = False Then
                    subCarpeta.Create()
                End If
                FileArchivo.PostedFile.SaveAs(filePath & archivo)
                AgregarDocumento(txtNombreFile.Text, FileArchivo.FileName, filePath & archivo)
                CargarDocumentos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnGuardarMensaje_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGuardarMensaje.Click
        Try
            Dim objlog As New ClsLogistica
            objlog.AbrirTransaccionCnx()
            objlog.AgregarNegociacionSubasta(Convert.ToInt32(hfCodSubasta.Value), idPro.Value, txtMensaje.Text, txtAsunto.Text, 684, "prv")
            objlog.CerrarTransaccionCnx()
            pnlMensaje.Visible = False
            btnAgregarMensaje.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnEliminar_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnEliminar.Click
        pnlMensaje.Visible = False
        btnAgregarMensaje.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim txtPrecioXUnidad As TextBox
            Dim objLog As New ClsLogistica
            objLog.AbrirTransaccionCnx()
            For Each row As GridViewRow In gvArticulos.Rows
                txtPrecioXUnidad = row.FindControl("txtPrecioXUnidad")
                objLog.ActualizaOfertaProveedorSubasta(hfCodSubasta.Value, row.Cells.Item(0).Text, idPro.Value, txtPrecioXUnidad.Text.Replace(".", ","))
            Next
            objLog.CerrarTransaccionCnx()
            CargarArticulosCantidad(Convert.ToInt32(hfCodSubasta.Value))
            MostrarMensaje("Se registró los precios por unidad.")
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
            dtSubasta = objlog.ConsultarSusbastaInversa(0, IIf(ddlListadoCategoria.SelectedIndex = 0, 0, ddlListadoCategoria.SelectedValue), txtFechaInicio.Text, txtFechaFin.Text, idPro.Value, "A", 0)
            gvSubastas.DataSource = dtSubasta
            gvSubastas.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub cargarMensajes()
        Try
            Dim objlog As New ClsLogistica
            Dim dtMensajes As Data.DataTable
            dtMensajes = objlog.ConsultarMensajes(hfCodSubasta.Value, idPro.Value, 684)
            dlMensajes.DataSource = dtMensajes
            dlMensajes.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarArticulosCantidad(ByVal codSubasta As Integer)
        Try
            Dim objlog As New ClsLogistica
            Dim dtArticulo As New Data.DataTable
            dtArticulo = objlog.ConsultarSusbastaProveedorOferta(codSubasta, idPro.Value)
            gvArticulos.DataSource = dtArticulo
            gvArticulos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDetalle(ByVal fecRegistro As String, ByVal desCategoria As String, ByVal desVigencia As String)
        lblRegistro.Text = fecRegistro
        lblCategoria.Text = desCategoria
        lblVigencia.Text = desVigencia
    End Sub

    Private Sub AgregarDocumento(ByVal nombre As String, ByVal nombreArchivo As String, ByVal ruta As String)
        Try
            Dim objlog As New ClsLogistica
            objlog.AbrirTransaccionCnx()
            objlog.AgregarDocumentosSubasta(hfCodSubasta.Value, idPro.Value, nombre, nombreArchivo, ruta)
            objlog.CerrarTransaccionCnx()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDocumentos()
        Try
            Dim objlog As New ClsLogistica
            gvFile.DataSource = objlog.ConsultarSusbastaDocumento(hfCodSubasta.Value, idPro.Value)
            gvFile.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

#End Region

End Class
