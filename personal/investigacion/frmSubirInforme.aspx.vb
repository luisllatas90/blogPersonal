
Partial Class investigacion_frmAgregarEditarComite
    Inherits System.Web.UI.Page
    Dim xid As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btbGuardar.Click
        xid = CInt(Request.QueryString("xid"))
        If Me.FileUpload1.HasFile Then
            Dim obj As New clsInvestigacion
            Dim exec As Integer = 0
            obj.AbrirTransaccionCnx()
            Dim filePath As String
            Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileUpload1.FileName).ToString
            filePath = Server.MapPath("../../filesInvestigacion/" & xid)
            Dim carpeta As New System.IO.DirectoryInfo(filePath)
            If carpeta.Exists = False Then
                carpeta.Create()
            End If
            FileUpload1.PostedFile.SaveAs(filePath & archivo)
            obj.RegistrarInforme(xid, filePath & archivo)
            obj.CerrarTransaccionCnx()
            obj = Nothing
            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "parent.jQuery.fancybox.close();", True)
            'ClientScript.RegisterStartupScript(Me.GetType, "nextpage", "parent.location.reload();", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Seleccione el archivo referente al informe de investigación');", True)
        End If

        
    End Sub
End Class
