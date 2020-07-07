
Partial Class avisosusat_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim obj As New clsAvisosUsat
        Dim dts As New Data.DataTable
        Dim rpta As Integer

        'Consideraciones:
        '1. La  imagen debe ser de preferencia de 800 an x 600 al para que se muestre centrada
        '2. Las imagenes deben tener el el formato de fecha y con exetencion .jpg
        '----------------------------------------------------------------------------------
        'Solictado: 03.02.2014 :: dguevara ::
        '----------------------------------------------------------------------------------
        '3.Si no encuentra imagen para el dia, el programa de escritorio debe cerrarse.
        '4.Imagen debe mostrarse 5 min. 

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

            'Response.Write("Ruta de la Imagen:" & strRutaFoto)
            'Response.Write("<br>")

            strFoto = (System.IO.File.Exists(strRutaFoto))
            'Response.Write("strFoto" & strFoto.ToString)
            'Response.Write("<br>")
            'Dim estado_avi As Boolean

            Me.hdestado.Value = ""
            If strFoto = True Then
                'Response.Write("Existe la Imagen")
                'Response.Write("<br>")
                Me.Image1.Visible = True
                Me.Image1.ImageUrl = "images/" & aviso.Trim & ".jpg"
                'estado_avi = True
                Me.hdestado.Value = "S"
            Else
                Me.Image1.Visible = False
                Me.hdestado.Value = "N"
                'Response.Write("Nooo Existe la Imagen")
                'Response.Write("<br>")
                'estado_avi = False
            End If
            'rpta = obj.AgregarEstadoImagen("Avisos Usat", estado_avi)
            ''If rpta = -1 Then
            ''    'enviamos un correo de error al insertar los datos.
            ''End If





        End If
    End Sub
End Class
