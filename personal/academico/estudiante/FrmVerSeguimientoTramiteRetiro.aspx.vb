
Partial Class academico_estudiante_FrmVerSeguimientoTramiteRetiro
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
                    Dim dt2 As New Data.DataTable

                    If IsNumeric(Request.QueryString("p")) = True Then

                        cls.AbrirConexion()

                        If Request.QueryString("opc") = 2 Then
                            dt = cls.TraerDataTable("TRL_VerificarTramiteRetiro", Request.QueryString("p"))
                            Me.gvLista.DataSource = dt
                            Me.gvLista.DataBind()
                        ElseIf Request.QueryString("opc") = 1 Then
                            dt2 = cls.TraerDataTable("ACAD_DatosRetiro", Request.QueryString("p"))
                            Me.gvLista2.DataSource = dt2
                            Me.gvLista2.DataBind()
                        End If

                        cls.CerrarConexion()

                    Else
                        Me.gvLista.DataSource = Nothing
                        Me.gvLista.DataBind()

                        Me.gvLista2.DataSource = Nothing
                        Me.gvLista2.DataBind()
                    End If
                Catch ex As Exception
                    Response.Write("Error: ")
                End Try
            End If
        End If
    End Sub
End Class
