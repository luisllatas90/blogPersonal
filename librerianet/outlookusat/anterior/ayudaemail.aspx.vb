
Partial Class librerianet_outlookusat_ayudaemail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            tbl = obj.TraerDataTable("ConsultarEmailUSAT", 0, Request.Form("id"))
            If tbl.Rows.Count > 0 Then
                Me.lblusuario.Text = tbl.Rows(0).Item("email")
                Me.lblClave.Text = tbl.Rows(0).Item("password")
            Else
                Me.lblMensaje.Text = "Por el momento no se le ha habilitado su cuenta de correo USAT. Favor de comunicarse al email: computo@usat.edu.pe"
            End If
            tbl.Dispose()
            obj = Nothing
        End If
    End Sub
End Class
