Imports System.Text.StringBuilder
Partial Class logistica_frmCategoria
    Inherits System.Web.UI.Page

#Region "Métodos y funciones del Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCategoria()
        End If
    End Sub

    Protected Sub gvCategoria_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCategoria.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCategoria','Select$" & e.Row.RowIndex & "');")
            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub ImgBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscar.Click
        CargarCategoria()
    End Sub

    Protected Sub ibtnRegresar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnRegresar.Click
        pnlBusqueda.Visible = True
        pnlCategoria.Visible = False
    End Sub

    Protected Sub gvCategoria_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCategoria.PageIndexChanging
        gvCategoria.PageIndex = e.NewPageIndex
        CargarCategoria()
    End Sub

    Protected Sub gvCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCategoria.SelectedIndexChanged
        pnlBusqueda.Visible = False
        pnlCategoria.Visible = True
        Dim hfcodCategoria As HiddenField
        hfcodCategoria = gvCategoria.SelectedRow.FindControl("hfcodCategoria")
        hfCategoria.Value = hfcodCategoria.Value
        CargarVerCategoria()
        hfTransaccion.Value = "M"
    End Sub

    Protected Sub ibtnGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGuardar.Click
        Try
            If hfTransaccion.Value = "M" Then
                ActualizarCategoria()
            ElseIf hfTransaccion.Value = "N" Then
                AgregarCategoria()
            End If
            pnlBusqueda.Visible = True
            pnlCategoria.Visible = False
            CargarCategoria()
            MostrarMensaje("Se han grabado los datos exitosamente.")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnNuevo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnNuevo.Click
        pnlBusqueda.Visible = False
        pnlCategoria.Visible = True
        hfTransaccion.Value = "N"
        txtNDesCategoria.Text = ""
    End Sub

#End Region

#Region "Métodos y funciones de Usuario"

    Private Sub CargarCategoria()
        Try
            Dim objLog As New ClsLogistica
            gvCategoria.DataSource = objLog.ConsultarCategoriaArticuloProveedor(0, txtDesCategoria.Text)
            gvCategoria.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarVerCategoria()
        Try
            Dim dtCategoria As Data.DataTable
            Dim clsLog As New ClsLogistica
            dtCategoria = clsLog.ConsultarCategoriaArticuloProveedor(hfCategoria.Value, "")
            txtNDesCategoria.Text = dtCategoria.Rows(0)("desCategoria").ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ActualizarCategoria()
        Dim clsLog As New ClsLogistica
        clsLog.AbrirTransaccionCnx()
        clsLog.ActualizarCategoriaArticuloProveedor(hfCategoria.Value, txtNDesCategoria.Text,ddlEstado.SelectedValue)
        clsLog.CerrarTransaccionCnx()
    End Sub

    Private Sub AgregarCategoria()
        Dim clsLog As New ClsLogistica
        clsLog.AbrirTransaccionCnx()
        clsLog.AgregarCategoriaArticuloProveedor(txtNDesCategoria.Text, ddlEstado.SelectedValue)
        clsLog.CerrarTransaccionCnx()
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
