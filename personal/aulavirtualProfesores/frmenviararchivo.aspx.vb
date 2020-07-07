
Partial Class aulavirtual_frmenviararchivo
    Inherits System.Web.UI.Page
    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim archivo, usuario, icursovirtual, ruta, idvisita, fileExtension As String
        Dim idtarea, idtareausuario, refidtareausuario As Integer
        Dim fileOk As Boolean

        usuario = Session("idusuario2")
        idtarea = Session("idtarea2")
        idtareausuario = Request.QueryString("idtareausuario")
        icursovirtual = Session("idcursovirtual2")
        idvisita = Session("idvisita2")
        refidtareausuario = Request.QueryString("refidtareausuario")

        If Session("idusuario2") = "" Then
            Response.Write("<h3>Ingrese denuevo al Aula Virtual y publique su trabajo</h3>")
            Exit Sub
        End If


        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        If Request.QueryString("accion") = "agregartareausuario" Then

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
                        ObjCMDUSAT.Ejecutar("DI_AgregarTareaUsuario", idtarea, usuario, archivo, idtareausuario, Me.TxtComentario.Text, icursovirtual)

                        ObjCMDUSAT.TerminarTransaccion()
                        ObjCMDUSAT = Nothing

                        Response.Write("<script>window.opener.location.reload();window.close()</script>")

                    Catch ex As Exception

                        ObjCMDUSAT.AbortarTransaccion()
                        Me.LblMensaje.Text = "Ocurrió un Error al Registrar los datos. Intente mas tarde." & Chr(13) & ex.Message
                        ObjCMDUSAT = Nothing
                    End Try
                End If
            Else
                Me.LblMensaje.Text = "No ha subido ningun archivo"
            End If
        Else
            Try
                archivo = ""
                '###########################################################################
                ' Guardamos el archivo y luego agregamos el documento en la base de datos
                '###########################################################################
                ObjCMDUSAT.IniciarTransaccion()
                ObjCMDUSAT.Ejecutar("DI_AgregarTareaUsuario", idtarea, usuario, archivo, idtareausuario, Me.TxtComentario.Text, icursovirtual)
                ObjCMDUSAT.TerminarTransaccion()
                ObjCMDUSAT = Nothing

                Response.Write("<script>window.opener.location.reload();window.close()</script>")

            Catch ex As Exception

                ObjCMDUSAT.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
                ObjCMDUSAT = Nothing
            End Try
        End If
        ObjCMDUSAT = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            If Request.QueryString("accion") <> "agregartareausuario" Then
                Me.FileArchivo.Visible = False
                Me.LblUbicacion.Visible = False
                Me.ValidarSubir.EnableClientScript = False
                Me.ValidarSubir.Enabled = False
            End If
        End If
    End Sub
End Class
