
Partial Class logistica_frmActualizacionArticulo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarArticulos(0, txtDescripcionArt.Text)
                CargarCategoria()
                CargarRubro()
                CargarClase()
                CargarUnidad()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarArticulos(ByVal idArt As Integer, ByVal descripcionArt As String)
        Dim clsLog As New ClsLogistica
        gvArticulo.DataSource = clsLog.ConsultarArticulo(idArt, descripcionArt)
        gvArticulo.DataBind()
    End Sub

    Protected Sub ImgBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscar.Click
        CargarArticulos(0, txtDescripcionArt.Text)
    End Sub

    Protected Sub gvArticulo_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvArticulo.PageIndexChanging
        gvArticulo.PageIndex = e.NewPageIndex
        CargarArticulos(0, txtDescripcionArt.Text)
    End Sub

    Protected Sub gvArticulo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvArticulo.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvArticulo','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvArticulo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvArticulo.SelectedIndexChanged
        pnlActualizacion.Visible = True
        pnlBusqueda.Visible = False
        Dim hfArticulo As HiddenField
        hfArticulo = gvArticulo.SelectedRow.FindControl("hfidArt")
        hfIdArticulo.Value = hfArticulo.Value
        CargarArticulo()
    End Sub

    Protected Sub ibtnRegresar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnRegresar.Click
        txtDescripcionArt.Text = ""
        CargarArticulos(0, "")
        pnlActualizacion.Visible = False
        pnlBusqueda.Visible = True
    End Sub

    Private Sub CargarCategoria()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarCategoria()
        objfun.CargarListas(ddlCategoria, dts, "codCategoria", "desCategoria", "<< Seleccione >>")
    End Sub

    Private Sub CargarRubro()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarRubro(0, "")
        objfun.CargarListas(ddlRubro, dts, "idRub", "descripcionRub", "<< Seleccione >>")
    End Sub

    Private Sub CargarClase()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarClase(0, "")
        objfun.CargarListas(ddlClase, dts, "idcls", "descripcioncls", "<< Seleccione >>")
    End Sub

    Private Sub CargarUnidad()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarUnidad(0, "")
        objfun.CargarListas(ddlUnidad, dts, "iduni", "descripcionuni", "<< Seleccione >>")
    End Sub

    Private Sub CargarArticulo()
        Dim dtArticulo As Data.DataTable
        Dim objLog As New ClsLogistica
        dtArticulo = objLog.ConsultarArticulo(hfIdArticulo.Value, "")
        txtDescripcion.Text = dtArticulo.Rows(0).Item("descripcionArt").ToString
        txtDescripcionRes.Text = dtArticulo.Rows(0).Item("descripcionResumidaArt").ToString
        ddlUnidad.SelectedValue = dtArticulo.Rows(0).Item("iduni").ToString
        ddlClase.SelectedValue = dtArticulo.Rows(0).Item("idcls").ToString
        ddlRubro.SelectedValue = dtArticulo.Rows(0).Item("idRub").ToString
        ddlCategoria.SelectedValue = dtArticulo.Rows(0).Item("codCategoria").ToString
    End Sub

    Private Sub MostrarMensaje(ByVal msg As String)
        Dim sbMensaje As New StringBuilder()
        sbMensaje.Append("<script type='text/javascript'>")
        sbMensaje.AppendFormat("alert('{0}');", msg)
        sbMensaje.Append("</script>")
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "mensaje", sbMensaje.ToString())
    End Sub

    Protected Sub ibtnGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGuardar.Click
        Try
            Dim objLog As New ClsLogistica
            objLog.AbrirTransaccionCnx()
            objLog.ActualizarArticulo(hfIdArticulo.Value, txtDescripcion.Text, txtDescripcionRes.Text, ddlUnidad.SelectedValue, ddlClase.SelectedValue, ddlRubro.SelectedValue, ddlCategoria.SelectedValue)
            objLog.CerrarTransaccionCnx()
            txtDescripcionArt.Text = ""
            CargarArticulos(0, "")
            MostrarMensaje("Se actualizó el artículo correctamente.")
            pnlActualizacion.Visible = False
            pnlBusqueda.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
