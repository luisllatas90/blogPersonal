
Partial Class academico_estudiante_FrmVerSeparacionDetalle
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../ErrorSistema.aspx")
        End If

        If IsPostBack = False Then
            If Request.QueryString("p") Is Nothing Then
                Me.gvLista.DataSource = Nothing
                Me.gvLista.DataBind()
            Else
                Try
                    Dim cls As New ClsConectarDatos
                    cls.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    Dim dt As New Data.DataTable

                    If IsNumeric(Request.QueryString("p")) = True Then
                        cls.AbrirConexion()
                        dt = cls.TraerDataTable("SEP_VerDetallexAlumno", Request.QueryString("p"))
                        cls.CerrarConexion()
                        Me.gvLista.DataSource = dt
                        Me.gvLista.DataBind()
                    Else
                        Me.gvLista.DataSource = Nothing
                        Me.gvLista.DataBind()
                    End If
                Catch ex As Exception
                    Response.Write("Error: ")
                End Try                
            End If
        End If
    End Sub
End Class
