
Partial Class logistica_abrirVentana
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim codigo_Rco As String
        Dim ObjCnx As New ClsConectarDatos
        Dim datos As New Data.DataTable
       
        codigo_Rco = Request.QueryString("numero")

        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        datos = ObjCnx.TraerDataTable("LOG_BuscarOrdenCS", "NO", "", "", "", codigo_Rco)
        ObjCnx.CerrarConexion()

        pNumero.Text = datos.Rows(0).Item("descripcion_Tdo") & " N° " & datos.Rows(0).Item("numeroDoc_Rco")
        pProveedor.InnerHtml &= datos.Rows(0).Item("nombrePro")
        lblReferencia.InnerHtml = datos.Rows(0).Item("referencia_Rco")
    End Sub
End Class
