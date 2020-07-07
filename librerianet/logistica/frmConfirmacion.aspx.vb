
Partial Class logistica_frmConfirmacion
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lblUsuario.Text = Request.ServerVariables("LOGON_USER")
        Me.lblSalida.Text = Request.QueryString("idBol")
        Me.lblObservacion.Text = Request.QueryString("observacion")
        'Me.lblUsuario.Text = "USAT\ESAAVEDRA"
        'Me.lblSalida.Text = "125"
        'Me.lblObservacion.Text = "adasd asda sdad"
    End Sub


    Protected Sub cmdConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConfirmar.Click
        Dim log As New ClsLogistica
        If Me.lblUsuario.Text <> "" And Me.lblSalida.Text <> "" Then
            log.ConfirmarEntrega(Me.lblSalida.Text, Me.lblUsuario.Text)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('No cuenta con datos válidos de confirmación, cierre la ventana y vuelva a ingresar')", True)
        End If
    End Sub
End Class
