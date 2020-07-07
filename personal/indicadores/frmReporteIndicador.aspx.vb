
Partial Class indicadores_frmReporteIndicador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If PreviousPage IsNot Nothing Then
            Dim id As Integer = PreviousPage.Nombre
            lblNombre.Text = id
            'https://intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_GraficoBarrasUnIndicador

        End If

    End Sub

End Class
