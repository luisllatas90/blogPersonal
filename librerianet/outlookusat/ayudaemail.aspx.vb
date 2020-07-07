
Partial Class librerianet_outlookusat_ayudaemail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Response.Write("Session: " & Session("codigo_alu"))
            If (Session("codigo_alu") Is Nothing) Then
                Response.Redirect("../msgError.aspx")
            End If

            If IsPostBack = False Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                Dim tbl As Data.DataTable
                Dim dt As New Data.DataTable

                dt = obj.TraerDataTable("ACAD_RetornaCodUniversitario", Session("codigo_alu"))                

                If (dt.Rows.Count > 0) Then
                    'Response.Write(dt.Rows(0).Item("codigoUniver_Alu").ToString.Trim & "-")
                    tbl = obj.TraerDataTable("ConsultarEmailUSAT", 0, dt.Rows(0).Item("codigoUniver_Alu").ToString.Trim)

                    If tbl.Rows.Count > 0 Then
                        Me.Panel1.Visible = True
                        Me.lblusuario.Text = tbl.Rows(0).Item("email")
                        Me.lblClave.Text = tbl.Rows(0).Item("password")
                        Me.lblMensaje.Visible = False
                    ElseIf tbl.Rows.Count = 0 Then                        
                        Me.Panel1.Visible = False
                        Me.lblMensaje.Visible = True
                        Me.lblMensaje.Text = "Por el momento no se le ha habilitado su cuenta de correo USAT. Solicitelo vía email a: computo@usat.edu.pe"
                    End If
                    tbl.Dispose()
                    obj = Nothing
                End If
            End If
        Catch ex As Exception
            Response.Write("Error al cargar los datos: " & ex.Message)
        End Try
    End Sub
End Class
