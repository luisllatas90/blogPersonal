
Partial Class personal_menuSup
    Inherits System.Web.UI.Page

    Public Sub btnEjecutar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEjecutar.Click
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        Dim datos As New Data.DataTable


        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        datos = obj.TraerDataTable("PRESU_ConsultarParametros", txtSQL.text.trim)
        gvdatos.DataSource = datos
        gvdatos.DataBind()
        datos.Dispose()

        obj.CerrarConexion()

    End Sub
End Class
