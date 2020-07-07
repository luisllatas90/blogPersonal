
Partial Class aulavirtual_detalletareausuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Tabla As Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("DI_ConsultarAdminTareas", 2, 0, 0, 0, Request.QueryString("idtareausuario"))

        If Tabla.Rows.Count > 0 Then
            Me.DataList1.DataSource = Tabla
            Me.DataList1.DataBind()
        End If

        ObjDatos = Nothing
    End Sub
End Class
