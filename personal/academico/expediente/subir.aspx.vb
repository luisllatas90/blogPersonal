Imports System.IO
Partial Class subir
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdEnviar.Click
        Dim ruta As String = "T:\documentos investigaciones\" & Request.QueryString("codigo_cac") & "\" & Request.QueryString("codigo_cup")
        Dim ruta2 As String = "T:\\documentos investigaciones\\" & Request.QueryString("codigo_cac") & "\\" & Request.QueryString("codigo_cup")
        Dim Carpeta As New System.IO.DirectoryInfo(ruta)
        If Carpeta.Exists = False Then : Carpeta.Create() : End If

        Dim ArchivosSubidos As HttpFileCollection = Request.Files
        Dim rutaarchivo As String
        Dim i As Integer = 0
        Dim Codigo As Integer
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Obj.IniciarTransaccion()
            Do Until i = ArchivosSubidos.Count
                Dim MiArchivoSubido As HttpPostedFile = ArchivosSubidos(i)
                If (MiArchivoSubido.ContentLength > 0) Then
                    Codigo = Obj.Ejecutar("INVALU_InsertarInvestigacionTemporal", Request.QueryString("codigo_cup"), Request.QueryString("codigo_cac"), Request.QueryString("codigo_per"), 0)
                    MiArchivoSubido.SaveAs(ruta2 & "\\investigacion" & Codigo.ToString & Path.GetExtension(MiArchivoSubido.FileName))
                    rutaarchivo = Request.QueryString("codigo_Cac") & "\" & Request.QueryString("codigo_cup") & "\" & "investigacion" & Codigo.ToString & Path.GetExtension(MiArchivoSubido.FileName)
                    Obj.Ejecutar("INVALU_ActualizarRutaTemporal", Codigo, rutaarchivo)
                End If
                i = i + 1
            Loop
            Obj.TerminarTransaccion()
            Response.Write("<script>window.opener.location.reload(); window.close();</script>")
        Catch ex As Exception
	    response.write(ex.message)	
            Obj.AbortarTransaccion()
        End Try
    End Sub

End Class
