
Partial Class AvanceTesis
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpTipo, obj.TraerDataTable("TES_ConsultarAvanceTesis", 0, 0, 0, 0), "codigo_TATes", "descripcion_TaTes")
            Me.TxtComentario.Attributes.Add("onKeyUp", "ContarTextArea(TxtComentario,1000,lblcontador)")
        End If
        Me.lblFecha.Text = Now
        
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
                ' Crear la ruta si no existe
                '==================================
                ruta = "T:\documentos aula virtual\archivoscv\tesis\" & codigo_tes & "\"
                Dim Carpeta As New System.IO.DirectoryInfo(ruta)
                If Carpeta.Exists = False Then
                    Carpeta.Create()
                End If

                '==================================
                ' Verificar publicación de archivo
                '==================================
                If Me.FileArchivo.HasFile = True And Me.dpTipo.SelectedValue <> 2 Then
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
