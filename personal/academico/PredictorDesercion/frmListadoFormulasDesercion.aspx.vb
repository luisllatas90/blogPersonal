
Partial Class academico_PredictorDiserccion_frmListadoFormulasDiserccion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtSession.value = Session("perlogin")
    End Sub
End Class
