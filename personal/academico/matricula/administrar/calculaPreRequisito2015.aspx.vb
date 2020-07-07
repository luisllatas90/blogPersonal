
Partial Class academico_matricula_calculaPreRequisito
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try            
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            obj.AbrirConexion()
            'obj.Ejecutar("MAT_ActualizarRequisitosCursoAsesor", Request.QueryString("param1"), Request.QueryString("param2"))
            obj.Ejecutar("MAT_ActualizarPreRequisitos", Request.QueryString("param1"), 0, Request.QueryString("param2"))
            obj.CerrarConexion()
            obj = Nothing            
            Response.Redirect("frmagregarcurso2015.asp?accion=" & Request.QueryString("param4") & "&codigo_pes=" & Request.QueryString("param3"))
        Catch ex As Exception
            Me.lblAviso.Text = "Error al calcular los Pre-Requisitos: " & ex.Message
        End Try
    End Sub
End Class
