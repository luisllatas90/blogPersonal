
Partial Class librerianet_estudiantesextranjeros_Notificar
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ObjCnx As New ClsConectarDatos
        Dim Datos As Data.DataTable
        ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        ObjCnx.AbrirConexion()
        ObjCnx.Ejecutar("dbo.EXT_EstadoAlumnoExtranjero", Me.GridView1.DataKeys.Item(Me.GridView1.SelectedIndex).Values(0), Me.DropDownList1.text)
        ObjCnx.CerrarConexion()
        Me.GridView1.databind()
    End Sub
End Class
