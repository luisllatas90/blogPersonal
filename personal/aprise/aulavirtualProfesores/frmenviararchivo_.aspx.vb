
Partial Class aulavirtual_frmenviararchivo
    Inherits System.Web.UI.Page
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim archivo, usuario, icursovirtual, ruta, idvisita, fileExtension As String
        Dim idtarea, idtareausuario, refidtareausuario As Integer
        Dim fileOk As Boolean


        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        idtarea = Request.QueryString("idtarea")
        idtareausuario = Request.QueryString("idtareausuario")
        icursovirtual = Request.QueryString("idcursovirtual")
        idvisita = Request.QueryString("idvisita")
        refidtareausuario = Request.QueryString("refidtareausuario")

        '#########################################################################
        'Subir el archivo a la carpeta, y verificar si es la segunda versión
        '#########################################################################
        ruta = "T:\documentos aula virtual\archivoscv\" & icursovirtual.ToString & "\tareas\"
        'ruta = "C:\Inetpub\wwwroot\campusvirtual\archivoscv\" & icursovirtual.ToString & "\tareas\"

        If Me.FileArchivo.HasFile = True Then
		Dim carpetaDestino As New System.IO.DirectoryInfo(ruta)
            	If carpetaDestino.Exists = False Then carpetaDestino.Create()

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
                archivo = Replace(usuario, "\", "")
                If Left(archivo, 4) = "USAT" Then
                    archivo = Right(archivo, Len(archivo) - 4)
                End If
                archivo = archivo & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & fileExtension
                Try
                    '###########################################################################
                    ' Guardamos el archivo y luego agregamos el documento en la base de datos
                    '###########################################################################
                    FileArchivo.PostedFile.SaveAs(ruta & archivo)
                    ObjCMDUSAT.IniciarTransaccion()
                    'ObjCMDUSAT.Ejecutar("DI_AgregarTareaUsuario", idtarea, usuario, archivo, idtareausuario, refidtareausuario, 0, Me.TxtComentario.Text, idvisita, icursovirtual)
		    ObjCMDUSAT.Ejecutar("AgregarTareaUsuario", idtarea, usuario, archivo, idtareausuario, refidtareausuario, 0, Me.TxtComentario.Text, idvisita, icursovirtual)
                    ObjCMDUSAT.TerminarTransaccion()
                    ObjCMDUSAT = Nothing

                    If idtareausuario = 0 Then
                        Response.Write("<script>window.opener.location.reload();window.close()</script>")
                    Else
                        Response.Write("<script>window.close()</script>")
                    End If

                Catch ex As Exception

                    ObjCMDUSAT.AbortarTransaccion()
                    Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos. Intente mas tarde." & Chr(13) & ex.Message
                    ObjCMDUSAT = Nothing
                End Try
            End If
        Else
            Me.LblMensaje.Text = "No ha subido ningun archivo"
        End If

    End Sub
End Class
