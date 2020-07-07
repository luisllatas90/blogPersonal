
Partial Class reglamentos_FrmReglamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.btnGuardar.Enabled = False
        If (Request.QueryString("id") IsNot Nothing) Then
            Me.btnGuardar.Enabled = True
            Me.calPublicacion.SelectedDate = Date.Today
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try            
            If (ValidaDatos() = True) Then
                Dim strRutaCV As String = ""
                Dim archivo As String
                Dim sw As Byte = 0

                strRutaCV = Server.MapPath("\archivos")
                archivo = GeneraToken()
                archivo = archivo & System.IO.Path.GetExtension(Me.flpArchivo.FileName).ToString

                While (sw = 0)
                    If (ExisteArchivo(archivo) = False) Then
                        sw = 1
                        Me.flpArchivo.PostedFile.SaveAs(strRutaCV & "\" & archivo)
                    End If
                End While


                Response.Write(archivo)
                Response.Write("<br/>" & strRutaCV & "\" & archivo)

            End If
        Catch ex As Exception
            lblMensaje.Text = "Error al guardar datos: " & ex.Message
        End Try
    End Sub

    Private Function ExisteArchivo(ByVal archivo As String) As Boolean
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            obj.AbrirConexion()
            dt = obj.TraerDataTable(archivo)
            obj.CerrarConexion()

            If (dt.Rows.Count > 0) Then
                Return True 'Existe el nombre del archivo
            End If

            Return False    'No existe archivo
        Catch ex As Exception
            Return True
        End Try
    End Function


    Private Function ValidaDatos() As Boolean
        Try
            Dim MegasPermitidos As Integer = 4
            If (Me.txtNombre.Text.Trim = "") Then
                lblMensaje.Text = "Falta ingresar el nombre del reglamento"
                Return False
            End If

            If (Me.txtElaborado.Text.Trim = "") Then
                lblMensaje.Text = "Falta ingresar quien elaboró del reglamento"
                Return False
            End If

            If (Me.flpArchivo.HasFile = False) Then
                lblMensaje.Text = "Falta adjuntar el reglamento"
                Return False
            End If

            If (Me.flpArchivo.FileName.ToLower.EndsWith("pdf") = False) Then
                lblMensaje.Text = "El archivo debe tener extensión PDF"
                Return False
            End If

            If (Me.flpArchivo.FileContent.Length / 1024 > MegasPermitidos * 1024) Then
                lblMensaje.Text = "El archivo debe tener un máximo de " & MegasPermitidos & " MB"
                Return False
            End If

            lblMensaje.Text = ""
            Return True
        Catch ex As Exception
            lblMensaje.Text = "Error al validar los datos: " & ex.Message
            Return False
        End Try
    End Function

    Private Function GeneraToken() As String
        Dim rnd As New Random
        Dim ubicacion As Integer
        Dim strNumeros As String = "0123456789"
        Dim strLetraMin As String = "abcdefghijklmnopqrstuvwxyz"
        Dim strLetraMay As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Token As String = ""
        Dim strCadena As String = ""
        strCadena = strLetraMin & strNumeros & strLetraMay
        While Token.Length < 10
            ubicacion = rnd.Next(0, strCadena.Length)
            If (ubicacion = 62) Then
                Token = Token & strCadena.Substring(ubicacion - 1, 1)
            End If
            If (ubicacion < 62) Then
                Token = Token & strCadena.Substring(ubicacion, 1)
            End If
        End While
        Return Token
    End Function
End Class
