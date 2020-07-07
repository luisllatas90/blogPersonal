
Partial Class academico_cargalectiva_frmCursoComentarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("id") IsNot Nothing _
                And Request.QueryString("cup") IsNot Nothing) Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim dt As New Data.DataTable
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ENC_ListaComentarioxCursoDocente", Request.QueryString("id"), Request.QueryString("cup"))
            obj.CerrarConexion()
            Me.lblCurso.Text = "Curso: " & Request.QueryString("curso").ToString
            If (dt.Rows.Count = 0) Then
                Me.lblMensaje.Text = "No se encontraron comentarios"
            Else
                Me.lblMensaje.Text = ""
                Me.gvComentarios.DataSource = dt
                Me.gvComentarios.DataBind()
            End If            
        End If
    End Sub
End Class
