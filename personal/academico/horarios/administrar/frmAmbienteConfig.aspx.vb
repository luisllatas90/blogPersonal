
Partial Class academico_horarios_administrar_frmAmbienteConfig
    Inherits System.Web.UI.Page

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("HORARIOPE_RegistrarAmbienteConfig", 1, Me.txtDesde.Value)
        obj.CerrarConexion()
        obj = Nothing
        consultarFecha()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            consultarFecha()
        End If
        
    End Sub
    Sub consultarFecha()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.lblFecha.Text = obj.TraerDataTable("HORARIOPE_ConsultarAmbienteConfig", 1).Rows(0).Item(1)
        Me.txtDesde.Value = Me.lblFecha.Text
        obj.CerrarConexion()
        obj = Nothing
    End Sub
End Class
