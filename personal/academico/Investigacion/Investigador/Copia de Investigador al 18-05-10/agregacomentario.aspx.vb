
Partial Class Investigador_agregacomentario
    Inherits System.Web.UI.Page

    
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjComentar As New Investigacion
        'response.write(Session("codigo_per"))
        If ObjComentar.AgregarObservaciones("1", Me.TxtAsunto.Text, Me.TxtObservacion.Text, Session("codigo_per"), CInt(Request.QueryString("codigo_inv"))) = 1 Then
            Response.Write("<script>window.opener.location.reload(); window.close();</script>")
        Else
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Ocurrio un error al insertar los datos"
        End If
    End Sub
End Class
