'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class detasesoriasprofesor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("id") = False Then
        'Response.Redirect("../../tiempofinalizado.asp")
        'End If
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpTipo, obj.TraerDataTable("TES_ConsultarAvanceTesis", 0, 0, 0, 0), "codigo_TATes", "descripcion_TaTes")

            Me.hdId.Value = Session("id")
            Me.hdCodigo_tes.Value = Request.QueryString("codigo_tes")
            Me.TxtComentario.Attributes.Add("onKeyUp", "ContarTextArea(TxtComentario,1000,lblcontador)")
            If Request.QueryString("estado_ates") = 2 Then
                cmdNuevo.Visible = False
                Me.lblmensajebloqueo.Visible = True
            End If
            CargarDetalleComentarios()
        End If
        ConfigurarTemas()
    End Sub

    Protected Sub cmdResponder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Me.grdComentarios.Visible = False
        Me.grdLista.Visible = False
        Me.cmdNuevo.Visible = False
        Me.Panel1.Visible = True
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        CancelarRespuesta()
    End Sub

    Private Sub CargarDetalleComentarios()
        If Request.QueryString("codigo_ates") <> "" Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable

            tbl = obj.TraerDataTable("TES_ConsultarAvanceTesis", 5, Request.QueryString("codigo_ates"), 0, 0)

            If tbl.Rows.Count > 0 Then
                Me.grdComentarios.DataSource = tbl
                Me.grdComentarios.DataBind()
                Me.grdLista.DataSource = tbl
                Me.grdLista.DataBind()
                obj = Nothing
                tbl = Nothing

            End If
            Me.lblFecha.Text = Now
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ruta, archivo, extensionarchivo As String
        Dim codigo_ates As Integer
        Dim ok As Boolean

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ok = False
        codigo_ates = Request.QueryString("codigo_ates")

        'If Request.QueryString("accion") = "A" Then
        Try
            ''==================================
            

            ''==================================
            '' Verificar publicación de archivo
            ''==================================
            If Me.FileArchivo.HasFile = True And Me.dpTipo.SelectedValue <> 2 Then

                '' Crear la ruta si no existe
                ''==================================
                ruta = "T:\documentos aula virtual\archivoscv\tesis\" & Me.hdCodigo_tes.Value & "\"
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
            obj.Ejecutar("TES_AgregarAvanceTesis", 2, Me.hdCodigo_tes.Value, hdId.Value, DBNull.Value, extensionarchivo, comentarios, codigo_ates, Me.txtTitulo.Text.Trim, 0)
            obj.TerminarTransaccion()
            ok = True
            If codigo_ates > 0 And extensionarchivo <> "" Then
                archivo = codigo_ates & extensionarchivo
                FileArchivo.PostedFile.SaveAs(ruta & archivo)
                ok = True
            End If
            If ok = True Then
                CancelarRespuesta()
            End If
            obj = Nothing
        Catch ex As Exception

            obj.AbortarTransaccion()
            Me.LblMensaje.Text = "Ocurrió un Error al Registrar el archivo. Intente mas tarde." & Chr(13) & ex.Message
            obj = Nothing
        End Try
        'End If
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Response.Redirect("lstasesoriasprofesor.aspx?codigo_tes=" & hdCodigo_tes.Value)
    End Sub

    Protected Sub grdComentarios_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdComentarios.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim ruta As String = ""
            Dim img As Image = e.Row.FindControl("foto")

            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            'Cargar foto del ASESORADO
            If Me.grdComentarios.DataKeys(e.Row.RowIndex).Values("asesorado").ToString <> "0" Then
                Dim obEnc As Object
                obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
                ruta = obEnc.CodificaWeb("069" & Me.grdComentarios.DataKeys(e.Row.RowIndex).Values("asesorado").ToString)
                obEnc = Nothing
                'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
                ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            End If

            'Cargar foto del ASESOR
            If Me.grdComentarios.DataKeys(e.Row.RowIndex).Values("asesor").ToString <> 0 Then
                'ruta = "../../imgpersonal/" & Me.grdComentarios.DataKeys(e.Row.RowIndex).Values("asesor").ToString & ".jpg"
                ruta = "../../images/menus/sinfoto.gif"
            End If
            'Mostrar foto
            img.ImageUrl = ruta

            If CDate(e.Row.Cells(2).Text.ToString).Date.ToShortDateString <> Date.Today.ToShortDateString Then
                e.Row.Cells(3).Text = ""
            Else
                'Mensaje de Eliminar
                e.Row.Cells(3).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar el registro?');")
            End If
        End If
    End Sub
    Protected Sub grdLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim ruta As String = ""
            Dim img As Image = e.Row.FindControl("foto")

            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

        End If
    End Sub

    Protected Sub lnkDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDetalle.Click
        Me.lnkDetalle.CssClass = "pestanaresaltada"
        Me.lnkLista.CssClass = "pestanabloqueada"
        Me.grdLista.Visible = False
        Me.grdComentarios.Visible = True
        Me.Panel1.Visible = False
        Me.cmdNuevo.Visible = True
    End Sub

    Protected Sub lnkLista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLista.Click
        Me.lnkDetalle.CssClass = "pestanabloqueada"
        Me.lnkLista.CssClass = "pestanaresaltada"
        Me.grdComentarios.Visible = False
        Me.grdLista.Visible = True
        Me.Panel1.Visible = False
        Me.cmdNuevo.Visible = True
    End Sub
    Private Sub CancelarRespuesta()
        Me.grdComentarios.Visible = True
        Me.grdLista.Visible = False
        Me.cmdNuevo.Visible = True
        Me.Panel1.Visible = False
        Me.txtTitulo.Text = ""
        Me.TxtComentario.Text = ""
        CargarDetalleComentarios()
    End Sub
    Private Sub ConfigurarTemas()
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
            'lnkSugerencias.Visible = False
        Else 'Publicar avances
            Me.LblUbicacion.Text = "Ubicación del Archivo (5 megas máximo). Formato .ZIP, .RAR, .PDF, .DOC"
            Me.ValidarTipoArchivo.ErrorMessage = "Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
            Me.ValidarTipoArchivo.ValidationExpression = "^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$"

            Me.FileArchivo.Visible = True
            Me.LblUbicacion.Visible = True
            Me.ValidarSubir.EnableClientScript = True
            Me.ValidarSubir.Enabled = True
            'lnkSugerencias.Visible = True
            Me.ValidarComentario.EnableClientScript = False
            Me.ValidarComentario.Enabled = False
        End If
    End Sub
    Protected Sub grdComentarios_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdComentarios.RowDeleting
        'If CDate(Me.grdComentarios.SelectedRow.Cells(2).Text.ToString).Date.ToShortDateString <> Date.Today.ToShortDateString Then
        '    Page.RegisterStartupScript("BloqueoEliminar", "<script>alert('Ud. sólo pueden eliminar asesorias registradas en el día de Hoy: " & Date.Today.ToShortDateString & "')</script>")
        'Else
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        obj.IniciarTransaccion()
        obj.Ejecutar("TES_EliminarAvanceTesis", Me.grdComentarios.DataKeys(e.RowIndex).Values("codigo_ATes"))
        obj.TerminarTransaccion()
        obj = Nothing
        e.Cancel = True

        Dim Ruta As String = Me.grdComentarios.DataKeys(e.RowIndex).Values("ruta_ATes")

        If Ruta <> "" Then
            Ruta = "T:\documentos aula virtual\archivoscv\tesis\" & Ruta
            If FileIO.FileSystem.FileExists(Ruta) = True Then
                Kill(Ruta)
            End If
        End If
        Me.CargarDetalleComentarios()
    End Sub
End Class
