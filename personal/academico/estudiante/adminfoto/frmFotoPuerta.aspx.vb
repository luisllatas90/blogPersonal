
Partial Class academico_estudiante_adminfoto_frmFotoPuerta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obEnc As Object
        Dim ruta As String
        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")
        ruta = obEnc.CodificaWeb("069" & Request.QueryString("coduniv"))        
        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
        Image1.ImageUrl = ruta
    End Sub
End Class
