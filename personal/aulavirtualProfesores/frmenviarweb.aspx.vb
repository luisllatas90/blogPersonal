
Partial Class aulavirtual_frmenviarweb
    Inherits System.Web.UI.Page
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim archivo, usuario, icursovirtual, ruta, idvisita, fileExtension As String
        Dim idtarea, idtareausuario As Integer
        Dim fileOk As Boolean

        '#########################################################################
        'Subir el archivo a la carpeta, y verificar si es la segunda versión
        '#########################################################################
        ruta = "C:\Aplicaciones WEB\Proyecto Campus Virtual\campusvirtual\cursos\usat\"
        'ruta = "D:\documentos aula virtual\archivoscv\" & icursovirtual.ToString & "\tareas\"
        'Response.Write(ruta)
        If Me.FileArchivo.HasFile = True Then
            fileExtension = System.IO.Path.GetExtension(FileArchivo.FileName).ToLower()

            '##########################################################################
            'Aki colocamos el array de los archivos NO permitidos
            '##########################################################################
            Dim allowedExtensions As String() = {".exe", ".dat", ".sys", ".dll", ".js", ".asp", ".php", ".aspx"}
            For i As Integer = 0 To allowedExtensions.Length - 1
                If fileExtension <> allowedExtensions(i) Then
                    fileOk = True
                Else
                    Me.LblMensaje.Text = "Archivo no permitido"
                    Exit Sub
                End If
            Next
            If fileOk = True Then
                Me.LblMensaje.Text = ""
                '######################################################################
                ' Capturamos el nombre de usuario
                '######################################################################

                Try
                    '###########################################################################
                    ' Guardamos el archivo y luego agregamos el documento en la base de datos
                    '###########################################################################
                    FileArchivo.PostedFile.SaveAs(ruta & FileArchivo.FileName.ToLower())

                    Response.Write("<script>alert('Gracias por registrar su archivo');top.window.close()</script>")

                Catch ex As Exception

                    Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos. Intente mas tarde." & Chr(13) & ex.Message
                End Try
            End If
        Else
            Me.LblMensaje.Text = "No ha subido ningun archivo"
        End If
    End Sub
End Class