
Partial Class academico_horarios_administrar_frmRestriccionAmbiente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                ClsFunciones.LlenarListas(Me.ddlTipo, obj.TraerDataTable("AsignarAmbiente_ListarAmbientes"), "codigo_tam", "descripcion_Tam")
                ClsFunciones.LlenarListas(Me.ddlUbicacion, obj.TraerDataTable("Ambiente_ListarUbicacion"), "codigo_ube", "descripcion_ube")

                Me.ddlTipo.SelectedValue = Session("ddlTipoAmbiente")
                Me.ddlUbicacion.SelectedValue = Session("ddlUbicacion")
                obj.CerrarConexion()
            End If
        Catch ex As Exception

        End Try


    End Sub

   
End Class
