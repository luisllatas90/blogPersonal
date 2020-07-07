Imports System.Data.OleDb

Partial Class frmimportarcolegiado
    Inherits System.Web.UI.Page
    Dim Ruta As String = Server.MapPath("../formatos/")
    Dim archivo As String = "tmpUsr" & Now.Year & ".xls"
    Protected Sub cmdSubir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSubir.Click
        If FileUpload.HasFile = True Then
            Try
                If System.IO.Path.GetExtension(FileUpload.FileName).ToLower() <> ".xls" Then
                    Me.lblmensaje.Text = "* Sólo se permiten archivos en formato Microsoft Excel (*.xls)"
                Else
                    Me.LnkFormato.Visible = False
                    FileUpload.PostedFile.SaveAs(Ruta & archivo)
                    lblmensaje.Text = "Archivo: " & _
                         FileUpload.FileName & "   |    " & _
                         "Tamaño: " & (FileUpload.PostedFile.ContentLength / 1024) & " kb<br>"

                    ImportarUsuarios()

                End If
            Catch ex As Exception
                lblmensaje.Text = "Error: " & ex.Message.ToString
                Me.LnkFormato.Visible = True
            End Try
        Else
            Me.grwUsuarios.Visible = False
            Me.LnkFormato.Visible = True
        End If
    End Sub

    Private Sub ImportarUsuarios()

        Dim strCnx As New OleDbConnection( _
            "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & Server.MapPath("../formatos/" & archivo) & ";" & _
            "Extended Properties=""Excel 8.0;HDR=Yes""")

        Try
            strCnx.Open()

            Dim DBCommand As New OleDbCommand("SELECT * FROM [Hoja1$]", strCnx)
            Dim tblXLS As OleDbDataReader = DBCommand.ExecuteReader()

            grwUsuarios.DataSource = tblXLS
            grwUsuarios.DataBind()
            Me.cmdCopiar.Visible = False

            tblXLS.Close()
            strCnx.Close()
            'Eliminar archivo subido temporalmente
            If FileIO.FileSystem.FileExists(Ruta & archivo) = True Then
                Kill(Ruta & archivo)
            End If

            'Habilitar Boton de OK
            Me.cmdCopiar.Visible = Me.grwUsuarios.Rows.Count > 0
        Catch ex As Exception
            strCnx.Close()
            strCnx = Nothing
            lblmensaje.Text = ex.Message
        End Try
    End Sub
    Protected Sub cmdCopiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCopiar.Click

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CnxBDICAL").ConnectionString)
        Try
            If Me.grwUsuarios.Rows.Count > 0 Then
                Dim ical, paterno, materno, nombres, dni, sexo, domicilio, telefono, celular, clave, estado As String
                'Desactivar validación de fileUpload
                Me.ValidarSubir.EnableClientScript = False
                Me.ValidarSubir.Enabled = False

                obj.IniciarTransaccion()
                For Each Fila As GridViewRow In Me.grwUsuarios.Rows

                    ical = Server.HtmlDecode(Fila.Cells(0).Text.Trim)
                    paterno = Server.HtmlDecode(UCase(Fila.Cells(1).Text.Trim))
                    materno = Server.HtmlDecode(UCase(Fila.Cells(2).Text.Trim))
                    nombres = Server.HtmlDecode(UCase(Fila.Cells(3).Text.Trim))
                    domicilio = Server.HtmlDecode(UCase(Fila.Cells(4).Text.Trim))
                    telefono = Server.HtmlDecode(Fila.Cells(5).Text.Trim)
                    celular = Server.HtmlDecode(Fila.Cells(6).Text.Trim)
                    dni = Server.HtmlDecode(Fila.Cells(7).Text.Trim)
                    sexo = Server.HtmlDecode(Fila.Cells(8).Text.Trim)
                    clave = ical 'ClsFunciones.GeneraClave(6)
                    estado = Server.HtmlDecode(Fila.Cells(9).Text.Trim)

                    If Server.HtmlDecode(Fila.Cells(0).Text.Trim.ToString) <> "" Then
                        obj.Ejecutar("AgregarColegiadoCAL", ical, paterno, materno, nombres, dni, sexo, domicilio, telefono, celular, clave, Session("codigo_usu"))
                    End If
                Next
                obj.TerminarTransaccion()
                obj = Nothing
                Me.grwUsuarios.DataBind()
                Page.RegisterStartupScript("GrabarUserXLS", "<script>alert('* Se han guardado los datos correctamente');location.href='frmimportarcolegiado.aspx'</script>")
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing
            Me.lblmensaje.Text = ex.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("activa") = False Then
            Response.Redirect("../tiempofinalizado.aspx")
        End If
    End Sub
End Class
