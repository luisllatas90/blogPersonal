
Partial Class TestNET
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If Session("id_per") = "" Then
            '    Response.Redirect("../sinacceso.html")
            'End If
            Dim ObjCnx As New ClsConectarDatos
            Dim Datos As New Data.DataTable
            ObjCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            ObjCnx.AbrirConexion()

            Datos = ObjCnx.TraerDataTable("InsertarAceptacionReglamento", 1, Request.QueryString("u"), Request.ServerVariables.Item("REMOTE_ADDR").ToString, Request.ServerVariables.Item("REMOTE_ADDR").ToString)
            ObjCnx.CerrarConexion()
            ObjCnx = Nothing
            Dim script As String = "window.close();"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, True)
        Catch ex As Exception
            'Response.Write("Error: " & ex.Message)

            Dim script As String = "window.close();"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewindows", script, True)
        End Try
    End Sub
End Class
