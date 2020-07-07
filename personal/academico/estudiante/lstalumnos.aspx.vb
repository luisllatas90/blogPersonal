
Partial Class academico_estudiante_lstalumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscarAlumno", Me.txtBuscar.Text)
            obj.CerrarConexion()

            Me.gvEstudiante.DataSource = dt
            Me.gvEstudiante.DataBind()

        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class
