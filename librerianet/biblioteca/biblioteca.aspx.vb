
Partial Class biblioteca_biblioteca
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjBIB As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
            Dim ObjCombo As New ClsFunciones

            ObjCombo.LlenarListas(Me.DDLProveedor, ObjBIB.TraerDataTable("PED_ConsultarEmpresaProveedora", "TO", 0), "idproveedor", "nombreproveedor")

            ObjCombo = Nothing
            ObjBIB = Nothing
        End If
        
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim ruta As String
        ruta = Server.MapPath("../../catalogobiblioteca/")
        Dim fileextension
        Dim fileOk As Boolean
        fileOk = False

        Try
            If Me.FileSubir.HasFile Then
                fileextension = System.IO.Path.GetExtension(FileSubir.FileName).ToLower()
                Dim allowedExtensions As String() = {".xls"}
                For i As Integer = 0 To allowedExtensions.Length - 1
                    If fileextension = allowedExtensions(i) Then
                        fileOk = True
                    End If
                Next

                If fileOk = True Then
                    FileSubir.PostedFile.SaveAs(ruta & "catalogo.xls")
                    Response.Redirect("../../personal/academico/BIBLIOTECA/pedidos/Biblioteca/procesarcatalogo.asp?idproveedor=" & Me.DDLProveedor.SelectedValue)
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
