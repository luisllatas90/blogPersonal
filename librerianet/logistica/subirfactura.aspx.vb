
Partial Class logistica_subirfactura
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


	try
	
        Dim Datos As Data.DataTable
        Dim codigo_original As String
        Dim ObjEncripta As New EncriptaCodigos.clsEncripta

	Dim valor as string
	valor = User.Identity.Name()
	response.write("sssss" & valor.tostring & "ddddd")

       
      
        Catch ex As Exception
            'Response.Write("No se han encontrado coincidencias")
	     Response.Write(ex.Message)
            tblDatos.Visible = False
        End Try
        
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click

        Dim ObjEncripta As New EncriptaCodigos.clsEncripta

        Dim strRuta As String
        Dim strNomArc As String
        Dim strFileExtension As String
        Dim straplicacion As String
        Dim codigo_original As String

        strRuta = "E:\documentos compras\" '& Now.Year.ToString
        straplicacion = "imgdoccompra"
        strFileExtension = System.IO.Path.GetExtension(Me.FileImagen.FileName).ToLower()
        codigo_original = Mid(ObjEncripta.Decodifica(Request.QueryString("codigo_rco")), 4)
        strNomArc = Replace(ObjEncripta.CodificaWeb("069" & codigo_original & Now.Second.ToString).ToString, "/", "")

        Dim ObjGuardar As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Try
            ObjGuardar.IniciarTransaccion()
            FileImagen.PostedFile.SaveAs(strRuta & strNomArc) 'Aqui guardo la imagen
            ObjGuardar.Ejecutar("AdjuntarImagenFactura", codigo_original, straplicacion, strNomArc)
            ObjGuardar.TerminarTransaccion()
            Me.ImgFactura.ImageUrl = "../../" & straplicacion & "/" & strNomArc
            Me.ImgFactura.Attributes.Add("OnClick", "AbrirVentanaMax('../../" & straplicacion & "/" & strNomArc & "')")
            Me.ImgFactura.DataBind()
        Catch ex As Exception
            ObjGuardar.AbortarTransaccion()
        End Try

    End Sub
End Class
