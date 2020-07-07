
Partial Class AvanceTesis
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
                ClsFunciones.LlenarListas(Me.dpTipo, obj.TraerDataTable("TES_ConsultarAvanceTesis", 0, 0, 0, 0), "codigo_TATes", "descripcion_TaTes")
                Me.TxtComentario.Attributes.Add("onKeyUp", "ContarTextArea(TxtComentario,1000,lblcontador)")
            End If
            Me.lblFecha.Text = Now
            'Publicar Informes en formato PDF
            If Me.dpTipo.SelectedValue = 3 Then
                Me.ValidarTipoArchivo.ErrorMessage = "Sólo se permiten subir archivos en formato PDF"
                Me.ValidarTipoArchivo.ValidationExpression = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$"
                Me.LblUbicacion.Text = "Ubicación del Archivo (5 megas máximo). Sólo en formato PDF"
                Me.ValidarComentario.EnableClientScript = False
                Me.ValidarComentario.Enabled = False
            ElseIf Me.dpTipo.SelectedValue = 2 Then
                'Publicar comentarios
                Me.ValidarComentario.EnableClientScript = True
                Me.ValidarComentario.Enabled = True
                Me.FileArchivo.Visible = False
                Me.LblUbicacion.Visible = False
                Me.ValidarSubir.EnableClientScript = False
                Me.ValidarSubir.Enabled = False
                lnkSugerencias.Visible = False
            Else 'Publicar avances
                Me.LblUbicacion.Text = "Ubicación del Archivo (5 megas máximo). Formato .ZIP, .RAR, .PDF, .DOC"
                Me.ValidarTipoArchivo.ErrorMessage = "Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                Me.ValidarTipoArchivo.ValidationExpression = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$"

                Me.FileArchivo.Visible = True
                Me.LblUbicacion.Visible = True
                Me.ValidarSubir.EnableClientScript = True
                Me.ValidarSubir.Enabled = True
                lnkSugerencias.Visible = True
                Me.ValidarComentario.EnableClientScript = False
                Me.ValidarComentario.Enabled = False
            End If
        Catch ex As Exception
            Me.LblMensaje.Text = "Ocurrió un Error al cargar la página: " & ex.Message
        End Try        
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim asesor, ruta, archivo, extensionarchivo As String
        Dim codigo_tes, codigo_ates As Integer
        Dim ok As Boolean

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ok = False
        asesor = Session("codigo_usu2")
        codigo_ates = Request.QueryString("codigo_ates")
        codigo_tes = Request.QueryString("codigo_tes")

        If Request.QueryString("accion") = "A" Then
            Try


                '==================================
                ' Verificar publicación de archivo
                '==================================
                If Me.FileArchivo.HasFile = True And Me.dpTipo.SelectedValue <> 2 Then

                    '==================================
                    ' Crear la ruta si no existe
                    '==================================
                    ruta = "T:\documentos aula virtual\archivoscv\tesis\" & codigo_tes & "\"
                    Dim Carpeta As New System.IO.DirectoryInfo(ruta)
                    If Carpeta.Exists = False Then
                        Carpeta.Create()
                    End If


                    extensionarchivo = System.IO.Path.GetExtension(FileArchivo.FileName).ToLower()
                Else
                    extensionarchivo = ""
                End If

                '==================================
                ' Guardamos el archivo
                '==================================
                Dim comentarios As String = DBNull.Value.ToString
                If Me.TxtComentario.Text.Trim <> "" Then
                    comentarios = Replace(Me.TxtComentario.Text.Trim, Chr(13), "<br>")
                End If
                obj.IniciarTransaccion()
                codigo_ates = obj.Ejecutar("TES_AgregarAvanceTesis", Me.dpTipo.SelectedValue, codigo_tes, asesor, DBNull.Value, extensionarchivo, comentarios, codigo_ates, Me.txtTitulo.Text.Trim, 0)
                obj.TerminarTransaccion()
                ok = True
                If codigo_ates > 0 And extensionarchivo <> "" Then
                    archivo = codigo_ates & extensionarchivo
                    FileArchivo.PostedFile.SaveAs(ruta & archivo)
                    ok = True
                End If
                If ok = True Then
                    Response.Write("<script>window.opener.location.reload();window.close()</script>")
                End If
                obj = Nothing
            Catch ex As Exception
                obj.AbortarTransaccion()
                Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
                obj = Nothing
            End Try
        End If
    End Sub
End Class
