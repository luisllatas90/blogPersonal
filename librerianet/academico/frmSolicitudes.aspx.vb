
Partial Class academico_frmSolicitudes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Request.QueryString("id") IsNot Nothing) Then
                Dim DatSolicitudes As New Data.DataTable
                Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                DatSolicitudes = Obj.TraerDataTable("ALUMNI_ConsultaSolicitudes", Request.QueryString("id"))
                If DatSolicitudes.Rows.Count > 0 Then
                    Me.gvDatos.DataSource = DatSolicitudes
                    Me.gvDatos.SelectedIndex = -1
                    Me.lblMensaje.Text = ""
                Else
                    Me.lblMensaje.Text = "No se encontraron solicitudes"
                End If
                Me.gvDatos.DataBind()
            End If            
        Catch ex As Exception
            Me.lblMensaje.Text = "Error: " & ex.Message
        End Try
    End Sub
End Class
