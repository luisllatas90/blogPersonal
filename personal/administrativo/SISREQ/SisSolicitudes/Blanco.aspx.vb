
Partial Class SisSolicitudes_Blanco
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim AsuntoCorreo, Correo, concopia, mensaje As String
        Try
            Dim ObjMailNet As New ClsEnviaMail
            ObjMailNet.ConsultarEnvioMail(Me.txtCod.Text)
            Response.Write(Me.txtCod.Text & "...SE ENVIARON CORRECTAMENTE LOS DATOS")
            ' Catch ex As Exception
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("OCURRIÓ UN ERROR AL PROCESAR LOS DATOS")
        End Try

    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim ObjMailNet As New ClsEnviaMail
            ObjMailNet.EnviaMailEvaluacionRegistro(Me.txtCod0.Text)
            Response.Write(Me.txtCod0.Text & "...SE ENVIARON CORRECTAMENTE LOS DATOS")
            ' Catch ex As Exception
        Catch ex As Exception
            Response.Write(ex.Message)
            Response.Write("OCURRIÓ UN ERROR AL PROCESAR LOS DATOS")
        End Try
    End Sub
End Class
