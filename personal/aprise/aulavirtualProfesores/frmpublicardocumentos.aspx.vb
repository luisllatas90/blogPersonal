Imports System.IO
Partial Class frmpublicardocumentos
    Inherits System.Web.UI.Page

    Protected Sub CmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdEnviar.Click
        Dim usuario, icursovirtual As String
        Dim ArchivosSubidos As HttpFileCollection = Request.Files
        Dim ruta, archivo, fileExtension As String
        Dim i As Integer = 0
        Dim valordevuelto, refcodigo_ccv, refiddocumento As Integer

        usuario = request.querystring("usuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        refcodigo_ccv = Request.QueryString("refcodigo_ccv")
        refiddocumento = Request.QueryString("refiddocumento")

        ruta = "T:\documentos aula virtual\archivoscv\" & icursovirtual.ToString & "\documentos\"
        'ruta = "C:\Inetpub\wwwroot\campusvirtual\archivoscv\" & icursovirtual.ToString & "\documentos\"

        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Try
            ObjCMDUSAT.IniciarTransaccion()
            Do Until i = ArchivosSubidos.Count
                Dim MiArchivoSubido As HttpPostedFile = ArchivosSubidos(i)
                If (MiArchivoSubido.ContentLength > 0) Then
                    fileExtension = Path.GetExtension(MiArchivoSubido.FileName).ToLower()

                    '######################################################################
                    ' Capturamos el nombre de usuario
                    '######################################################################
                    archivo = Replace(usuario, "\", "")
                    If Left(archivo, 4) = "USAT" Then
                        archivo = Right(archivo, Len(archivo) - 4)
                    End If
                    archivo = archivo & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & fileExtension
                    Try
                        '###########################################################################
                        ' Guardamos el archivo y luego agregamos el documento en la base de datos
                        '###########################################################################

                        valordevuelto = ObjCMDUSAT.Ejecutar("DI_AgregarVariosDocumentos", archivo, MiArchivoSubido.FileName, usuario, icursovirtual, refcodigo_ccv, refiddocumento, 0)

                        If valordevuelto > 0 Then
                            MiArchivoSubido.SaveAs(ruta & archivo)
                        Else
                            ObjCMDUSAT.AbortarTransaccion()
                            Me.lblMensaje.Text = "No se pueden subir los archivos"
                            Exit Sub
                        End If

                    Catch Env As Exception
                        ObjCMDUSAT.AbortarTransaccion()
                        Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos. " & Env.Message
                        ObjCMDUSAT = Nothing
                        Exit Sub
                    End Try

                End If
                i = i + 1
            Loop
            ObjCMDUSAT.TerminarTransaccion()
            'Response.Write("<script>window.opener.location.reload(); window.close();</script>")
            'response.write "ok"
        Catch ex As Exception
            ObjCMDUSAT.AbortarTransaccion()
            Me.LblMensaje.Text = "Ocurrió un Error al Publicar los documentos. " & ex.Message
            ObjCMDUSAT = Nothing
        End Try
    End Sub
End Class
