
Partial Class aulavirtual_ActualizarCursosMoodle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("Moodle_AgregarCursoAlumno", Me.TextBox1.Text)
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        
    End Sub
End Class
