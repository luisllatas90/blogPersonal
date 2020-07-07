
Partial Class aulavirtual_descargararchivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Ruta As String

        If Request.QueryString("accion") = "D" Then

            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            obj.IniciarTransaccion()
            Ruta = obj.Ejecutar("DI_DescargarTarea", Request.QueryString("idtareausuario"), Session("idusuario2"), Session("idvisita2"), "--", Session("idcursovirtual2"), 0)
            obj.TerminarTransaccion()
            obj = Nothing
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            Response.Redirect("../../../archivoscv/" & Ruta)
        End If

        If Request.QueryString("accion") = "E" Then
            'Eliminar de la base de datos
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CMUSATConnectionString").ConnectionString)
            obj.IniciarTransaccion()
            Ruta = obj.Ejecutar("DI_EliminarTrabajoUsuario", Request.QueryString("idtareausuario"), "")
            obj.TerminarTransaccion()
            obj = Nothing

            'Eliminar el archivo
            Ruta = "T:\documentos aula virtual\archivoscv\" & Ruta
            If FileIO.FileSystem.FileExists(Ruta) = True Then
                Kill(Ruta)
            End If
            Response.Redirect("admintareas.aspx?vez=2")

        End If

    End Sub
End Class
