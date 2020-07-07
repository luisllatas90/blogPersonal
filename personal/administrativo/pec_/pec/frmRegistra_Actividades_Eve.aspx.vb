
Partial Class administrativo_pec2_frmRegistra_Actividades_Eve
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        iniciar()
    End Sub
    Protected Sub iniciar()
        'Ocurre cuando se inicia el form y cuando se cambia el evento
        If Not IsPostBack Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim tablacecos As New System.Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tablacecos = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod"))
            objfun.CargarListas(DropDownList1, tablacecos, "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing
        End If
    End Sub
End Class
