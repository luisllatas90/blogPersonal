
Partial Class GestionInvestigacion_FrmPostulacionConcursoExterno
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If IsPostBack = False Then
                Me.cboEstado.SelectedValue = 1
                ListaConcursos()
                'Me.Scriptmanager1.RegisterStartupScript(Me, Me.GetType, "abrir", "btnConsultar.OnClientClick = BusyBox1.ShowFunctionCall;", True)

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If Me.cboEstado.SelectedValue <> "" Then
            System.Threading.Thread.Sleep(3000)
            ListaConcursos()

        Else
            Response.Write("seleccionar Estado")
        End If
    End Sub

    Private Sub ListaConcursos()
        
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable

        dt = obj.ListarConcurso("L", "1", "", Me.cboEstado.SelectedValue, Session("id_per"), Request.QueryString("ctf"))

        Me.gvConcursos.DataSource = dt
        Me.gvConcursos.DataBind()
        
    End Sub
End Class
