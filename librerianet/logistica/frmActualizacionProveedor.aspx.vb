Imports AjaxControlToolkit
Imports System.Web.UI.WebControls
Partial Class logistica_frmActualizacionProveedor
    Inherits System.Web.UI.Page

#Region "Métodos y funciones del Formulario"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargarProveedores()
                CargarCategoria()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvProveedor_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvProveedor.PageIndexChanging
        gvProveedor.PageIndex = e.NewPageIndex
        CargarProveedores()
    End Sub

    Protected Sub gvProveedor_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProveedor.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvProveedor','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvProveedor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvProveedor.SelectedIndexChanged
        ddlCategoria.SelectedIndex = 0
        pnlActualizacion.Visible = True
        pnlBusqueda.Visible = False
        Dim hfidPro As HiddenField
        hfidPro = gvProveedor.SelectedRow.FindControl("hfidPro")
        CargarProveedor(hfidPro.Value)
        hfPro.Value = hfidPro.Value
        CargarCategoriaProveedor()
    End Sub

    Protected Sub ImgBuscarSubasta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarSubasta.Click
        CargarProveedores()
    End Sub

    Protected Sub ibtnRegresar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnRegresar.Click
        pnlActualizacion.Visible = False
        pnlBusqueda.Visible = True
    End Sub

    Protected Sub ibtnAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAgregar.Click
        Try
            If ExisteCategoria(ddlCategoria.SelectedValue) Then
                Dim objlog As New ClsLogistica
                objlog.AbrirTransaccionCnx()
                objlog.AgregarCategoriaProveedor(hfPro.Value, ddlCategoria.SelectedValue)
                objlog.CerrarTransaccionCnx()
                CargarCategoriaProveedor()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnEliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim hfCodCategoria As HiddenField
            Dim row As GridViewRow
            Dim ibtnEliminar As ImageButton
            ibtnEliminar = sender
            row = ibtnEliminar.NamingContainer
            hfCodCategoria = row.FindControl("hfcodCategoria")
            Dim objlog As New ClsLogistica
            objlog.EliminarProveedorCategoria(hfPro.Value, hfCodCategoria.Value)
            CargarCategoriaProveedor()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnGuardar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGuardar.Click
        Try
            Dim objLog As New ClsLogistica
            objLog.AbrirTransaccionCnx()
            objLog.ActualizarProveedor(hfPro.Value, txtADireccion.Text, txtATelefono.Text, txtAFax.Text, txtAEmail.Text, rtgRanking.CurrentRating, txtAPassword.Text, chkParticipa.Checked)
            objLog.CerrarTransaccionCnx()
            CargarProveedores()
            pnlActualizacion.Visible = False
            pnlBusqueda.Visible = True
            MostrarMensaje("Se actualizaron los datos del proveedor correctamente. ")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnGenerarUsuario_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGenerarUsuario.Click
        Dim objLog As New ClsLogistica
        Dim passwordPro As String
        objLog.AbrirTransaccionCnx()
        passwordPro = objLog.ActualizarUsuarioProveedor(hfPro.Value)
        objLog.CerrarTransaccionCnx()
        txtAUsuario.Text = txtARUC.Text
        txtAPassword.Text = passwordPro
        EnviarMensaje()
    End Sub

#End Region

#Region "Métodos y funciones de Usuarios"

    Private Sub CargarProveedores()
        Try
            Dim objLog As New ClsLogistica
            gvProveedor.DataSource = objLog.ConsultarProveedor(0, txtRazSoc.Text, txtRuc.Text, 0)
            gvProveedor.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCategoriaProveedor()
        Try
            Dim objLog As New ClsLogistica
            gvCategoriaPrv.DataSource = objLog.ConsultarCategoriaProveedor(hfPro.Value)
            gvCategoriaPrv.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarProveedor(ByVal idPro As Integer)
        Try
            Dim dtProveedor As New Data.DataTable
            Dim objLog As New ClsLogistica
            dtProveedor = objLog.ConsultarProveedor(idPro, "", "", 0)
            txtARUC.Text = dtProveedor.Rows(0)("rucPro").ToString
            txtARazSoc.Text = dtProveedor.Rows(0)("nombrePro").ToString
            txtADireccion.Text = dtProveedor.Rows(0)("direccionPro").ToString
            txtAEmail.Text = dtProveedor.Rows(0)("emailPro").ToString
            txtATelefono.Text = dtProveedor.Rows(0)("telefonoPro").ToString
            txtAFax.Text = dtProveedor.Rows(0)("faxPro").ToString
            txtAUsuario.Text = dtProveedor.Rows(0)("usuario").ToString
            chkParticipa.Checked = dtProveedor.Rows(0)("participa")
            'If txtAUsuario.Text = "" Then
            '    txtAPassword.Enabled = False
            'Else
            '    txtAPassword.Enabled = True
            'End If
            txtAPassword.Text = dtProveedor.Rows(0)("passwordPro").ToString
            rtgRanking.CurrentRating = dtProveedor.Rows(0)("ranking").ToString

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCategoria()
        Dim objfun As New ClsFunciones
        Dim objlog As New ClsLogistica
        Dim dts As New Data.DataTable
        dts = objlog.ConsultarCategoria()
        objfun.CargarListas(ddlCategoria, objlog.ConsultarCategoria, "codCategoria", "desCategoria", "<< Seleccione >>")
    End Sub

    Private Function ExisteCategoria(ByVal codCategoria As Integer) As Boolean
        Dim hfCodCategoria As HiddenField
        For Each row As GridViewRow In gvCategoriaPrv.Rows
            hfCodCategoria = row.FindControl("hfcodCategoria")
            If codCategoria = hfCodCategoria.Value Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub MostrarMensaje(ByVal msg As String)
        Dim sbMensaje As New StringBuilder()
        sbMensaje.Append("<script type='text/javascript'>")
        sbMensaje.AppendFormat("alert('{0}');", msg)
        sbMensaje.Append("</script>")
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "mensaje", sbMensaje.ToString())
    End Sub

    Private Sub EnviarMensaje()
        Dim ObjMailNet As New ClsMail
        Dim mensaje As String = "Estimado usuario " & txtARUC.Text & " - " & txtARazSoc.Text & ": </br> </br>Se ha creado una nueva contraseña:  " & txtAPassword.Text & " </br> </br>Atentamente, </br> </br> Subasta Inversa - Logística"
        If txtAEmail.Text <> "" Then
            ObjMailNet.EnviarMail("campusvirtual@usat.edu.pe", "Subasta Inversa - Logística", "gcastillo@usat.edu.pe", "Nueva contraseña", mensaje, True)
        End If
    End Sub

#End Region

End Class
