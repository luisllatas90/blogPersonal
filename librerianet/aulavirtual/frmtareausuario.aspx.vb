
Partial Class aulavirtual_frmtareausuario
    Inherits System.Web.UI.Page


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

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim archivo, usuario, icursovirtual, ruta, idvisita, fileExtension As String
        Dim idtarea, idtareausuario As Integer
        Dim fileOk As Boolean


        Dim ObjCMDUSAT As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        usuario = Request.QueryString("idusuario")
        idtarea = Request.QueryString("idtarea")
        idtareausuario = Request.QueryString("idtareausuario")
        icursovirtual = Request.QueryString("idcursovirtual")

        '#########################################################################
        'Subir el archivo a la carpeta, y verificar si es la segunda versión
        '#########################################################################
        ruta = "T:\documentos aula virtual\archivoscv\" & icursovirtual.ToString & "\tareas\"
        'ruta = "C:\Inetpub\wwwroot\campusvirtual\archivoscv\" & icursovirtual.ToString & "\tareas\"

	If Request.QueryString("accion")="agregartareausuario" then
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
                    ObjCMDUSAT.Ejecutar("DI_AgregarTareaUsuario", idtarea, usuario, archivo, idtareausuario,Me.TxtComentario.Text,icursovirtual)
                    ObjCMDUSAT.TerminarTransaccion()
                    ObjCMDUSAT = Nothing

                    Response.Write("<script>window.opener.location.reload();window.close()</script>")
                Catch ex As Exception

                    ObjCMDUSAT.AbortarTransaccion()
                    Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
                    ObjCMDUSAT = Nothing
                End Try
           End If
	  Else
            Me.LblMensaje.Text = "No ha subido ningun archivo"
          End If
	else
	  Try
		    archivo=""
                    '###########################################################################
                    ' Guardamos el archivo y luego agregamos el documento en la base de datos
                    '###########################################################################
                    ObjCMDUSAT.IniciarTransaccion()
                    ObjCMDUSAT.Ejecutar("DI_AgregarTareaUsuario",idtarea,usuario,archivo,idtareausuario,Me.TxtComentario.Text,icursovirtual)
                    ObjCMDUSAT.TerminarTransaccion()
                    ObjCMDUSAT = Nothing

                    Response.Write("<script>window.opener.location.reload();window.close()</script>")

                Catch ex As Exception

                    ObjCMDUSAT.AbortarTransaccion()
                    Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
                    ObjCMDUSAT = Nothing
            End Try
	end if
    End Sub
End Class
