
Partial Class administrativo_pec_frmConsultaDatosProfesionalizacion
    Inherits System.Web.UI.Page

    Protected Sub lnkDatosEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosEvento.Click
        Try
            EnviarAPagina("..\..\..\librerianet\academico\historialcc.aspx?id=" & Request.QueryString("id"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Try
            Me.fradetalle.Attributes("src") = pagina
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lnkDatosEvento_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPreInscripcion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPreInscripcion.Click
        Try
            EnviarAPagina("..\..\..\librerianet\academico\admincuentaper.aspx?id=" & Request.QueryString("id"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
