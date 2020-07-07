
Partial Class administrativo_pec2_frmTipoActividad
    Inherits System.Web.UI.Page

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim obj As New ClsConectarDatos
        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("EVE_AgregaTipoActividad", Me.txtDescripcion.Text)
            obj.CerrarConexion()
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")
            Me.txtDescripcion.Text = ""
        Catch ex As Exception
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Error al Guardar')</SCRIPT>")
            obj.CerrarConexion()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Me.txtDescripcion.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then

        End If
    End Sub
End Class
