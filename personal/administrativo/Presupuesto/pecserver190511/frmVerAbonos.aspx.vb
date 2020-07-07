
Partial Class administrativo_pec_frmVerAbonos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim datos As New Data.DataTable


            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            datos = obj.TraerDataTable("CAJ_DetalleAbonoDeudas", Request.QueryString("id"))
            gvAbonos.DataSource = datos
            gvAbonos.DataBind()

            datos.Dispose()

            obj.CerrarConexion()

        End If
    End Sub
End Class
