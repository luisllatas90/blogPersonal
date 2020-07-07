
Partial Class DirectorInvestigacion_agregacomentario
    Inherits System.Web.UI.Page


    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        If Request.QueryString("modo") = "obs" Then
            Dim ObjComentar As New Investigacion
            If ObjComentar.AgregarObservaciones("1", Me.TxtAsunto.Text, Me.TxtObservacion.Text, Session("codigo_per"), CInt(Request.QueryString("codigo_inv"))) = 1 Then
                ObjComentar.CambiarestadoInvestigacion(Request.QueryString("codigo_inv"), 2, Request.QueryString("id"))
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            Else
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error al insertar los datos"
            End If
            ObjComentar = Nothing
        Else
            Dim ObjComentar As New Investigacion
            If ObjComentar.AgregarObservaciones("1", Me.TxtAsunto.Text, Me.TxtObservacion.Text, Session("codigo_per"), CInt(Request.QueryString("codigo_inv"))) = 1 Then
                Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            Else
                Me.LblMensaje.ForeColor = Drawing.Color.Red
                Me.LblMensaje.Text = "Ocurrio un error al insertar los datos"
            End If
            ObjComentar = Nothing
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("modo") = "obs" Then
            Me.LblTitulo.Text = "Observar una Investigación"
            Me.TxtAsunto.Text = "OBSERVACIÓN :"
        Else
            Me.LblTitulo.Text = "Comentar una Investigación"
        End If
    End Sub
End Class
