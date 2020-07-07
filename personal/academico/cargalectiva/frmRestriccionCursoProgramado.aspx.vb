
Partial Class academico_horarios_administrar_frmRestriccionAmbiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.ddlCiclo, obj.TraerDataTable("ListaCicloAcademico"), "codigo_cac", "descripcion_cac", "<<Seleccione>>")

            obj.CerrarConexion()
        End If

    End Sub

   

    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting
    '    e.Command.Parameters("nombre").Value = Me.TextBox1.Text

    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub
End Class
