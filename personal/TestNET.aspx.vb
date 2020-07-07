
Partial Class TestNET
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()
            Datos = ObjCnx.TraerDataTable("presu_consultarprocesocontable", "1")
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing

            Response.Write("TODO OK <br/>")

            Dim objpre As New ClsPresupuesto
            objpre.ConsultarProcesoContable()
            Response.Write("CLASE TODO OK <br/>")
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try
    End Sub
End Class
