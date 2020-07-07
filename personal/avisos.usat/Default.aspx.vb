
Partial Class avisosusat_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Consideraciones:
        '1. La  imagen debe ser de preferencia de 800 an x 600 al para que se muestre centrada
        '2. Las imagenes deben tener el el formato de fecha y con exetencion .jpg

        If Not IsPostBack Then
            'Response.Write(Today.ToString("dd/MM/yyyy"))
            Dim nombreaviso As String = Today.ToString("dd/MM/yyyy").ToString '& ".jpg"
            Dim aviso As String = ""
            Dim TestArray() As String = Split(nombreaviso, "/")
            For i As Integer = 0 To TestArray.Length - 1
                If TestArray(i) <> "" Then
                    aviso = aviso & TestArray(i)
                    'Response.Write(TestArray(i))
                End If
            Next
            'Response.Write(aviso)
            'Response.Write("<br>")

            Dim strFoto As Boolean
            Dim strRutaFoto As String = Server.MapPath("images\" & aviso.Trim & ".jpg")
            strFoto = (System.IO.File.Exists(strRutaFoto))
            'Response.Write(strFoto)
            'Response.Write("<br>")

            If strFoto = True Then
                'Response.Write("Existe la Imagen")
                Me.Image1.Visible = True
                Me.Image1.ImageUrl = "images/" & aviso.Trim & ".jpg"
            Else
                Me.Image1.Visible = False
                'Response.Write("Nooo Existe la Imagen")
            End If
        End If
    End Sub
End Class
