
Partial Class librerianet_aulavirtual_vistapaginaweb
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Dim Datos As Data.DataTable
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)
        Dim iddocumento As Integer = 0
        iddocumento = Request.QueryString("iddocumento")
        Datos = Obj.TraerDataTable("ConsultarDocumento", "3", iddocumento, "", "", "")
        Obj = Nothing
        If Datos.Rows.Count > 0 Then
            Me.lbltitulo.Text = Datos.Rows(0).Item("titulodocumento").ToString
            Me.lblContenido.Text = Datos.Rows(0).Item("paginaweb").ToString
            Me.Title = Me.lbltitulo.Text
        End If
        Datos.Dispose()
        Obj = Nothing
    End Sub
End Class
