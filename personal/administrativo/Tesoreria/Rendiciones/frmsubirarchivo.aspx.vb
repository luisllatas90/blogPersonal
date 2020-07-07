
Partial Class frmsubirarchivo
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos


    Protected Sub cmdcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdcancelar.Click
        Response.Write("<script> window.opener.location.reload(); window.close(); </script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Me.HiddenField1.Value = Me.Request.QueryString("codigo_dren")
        End If
    End Sub

    Protected Sub cmdaceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdaceptar.Click
        Try

            If Me.FileUpload1.FileName = "" Then
                Me.lblmensaje.Text = "Seleccione archivo"
                Exit Sub
            End If

            Dim extension As String = ""
            Dim codigoarchivo As Integer, mensaje As String = ""

            extension = System.IO.Path.GetExtension(Me.FileUpload1.FileName)
            cn.abrirconexion()
            cn.ejecutar("sp_agregararchivo", False, codigoarchivo, mensaje, Me.HiddenField1.Value, Me.txtdescripcion.Text, extension, 0, 0, "")
            cn.cerrarconexion()


            Response.Write(codigoarchivo)

            'Aqui no retorna el codigo insertado en el procedure anterior.
            'Descomentar
            'Me.FileUpload1.SaveAs(Me.Server.MapPath("Archivosderendicion") & "/A" & codigoarchivo & extension)
            'Me.FileUpload1.SaveAs(Me.Server.MapPath("Archivosderendicion") & "\A" & codigoarchivo & extension)

            '=========================================
            'Pruebas Dante:
            Dim filePath As String
            filePath = Server.MapPath("Archivosderendicion")
            'filePath = Server.MapPath("../../../../filesSubastas")
            filePath = filePath & "\A" & codigoarchivo & extension
            FileUpload1.PostedFile.SaveAs(filePath)
            '==========================================

            'Descomentar
            Response.Write("<script> alert('Se ha guardado el archvio correctamente'); window.opener.location.reload(); window.close() </script>")
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
