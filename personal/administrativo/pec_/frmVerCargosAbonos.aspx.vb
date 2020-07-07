
Partial Class administrativo_pec_frmVerCargosAbonos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Cargar Datos Centro Costos
            datos = obj.TraerDataTable("EVE_ConsultarCargosAbonosPersona", Request.QueryString("pso"), Request.QueryString("cco"))
            gvResultado.DataSource = datos
            gvResultado.DataBind()

            datos.Dispose()
            obj.CerrarConexion()

        End If
    End Sub
End Class
