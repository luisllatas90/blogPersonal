
Partial Class frmportadawebcurso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Me.hdidcursovirtual.Value = Request.QueryString("idcursovirtual").ToString
            Me.TxtComentario.Attributes.Add("onKeyUp", "ContarTextArea(TxtComentario,8000,lblcontador)")

            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)
            Dim Datos As Data.DataTable
            Datos = Obj.TraerDataTable("ConsultarCursoVirtual", "3", Me.hdidcursovirtual.Value, "", "")
            Obj = Nothing

            Me.TxtComentario.Text = Replace(Datos.Rows(0).Item("web").ToString, "<br>", Chr(13))
            Datos.Dispose()
        End If
    End Sub
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

        Try
            If InStr(Me.TxtComentario.Text, "<script") > 0 Then
                Obj = Nothing
                Me.LblMensaje.Text = "No está permitido contenido JavaScript"
                Exit Sub
            End If

            'Grabar información
            Obj.IniciarTransaccion()
            Obj.Ejecutar("Modificarwebcursovirtual", Me.hdidcursovirtual.Value, Replace(Me.TxtComentario.Text.Trim, Chr(13), "<br>"))
            Obj.TerminarTransaccion()

            Page.RegisterStartupScript("GrabadoWeb", "<script>alert('Se han guardado los datos correctamente');top.window.close();</script>")

            Obj = Nothing
        Catch ex As Exception
            Me.LblMensaje.Text = "Error: " & ex.Message
            Obj = Nothing
        End Try
    End Sub
End Class
