
Partial Class Odontologia_Docente_frmAcceso
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then            
            Session.Clear()
            Session.RemoveAll()

            Me.txtUsuario.Attributes.Add("placeholder", "Usuario")
            Me.txtUsuario.Attributes.Add("required", "required")

            Me.txtClave.Attributes.Add("placeholder", "Clave")
            Me.txtClave.Attributes.Add("required", "required")
        End If
    End Sub

    Protected Sub btnIngresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("ODO_VerificaAccesoPersonal", Me.txtUsuario.Text, Me.txtClave.Text)
            If (dt.Rows.Count > 0) Then
                Session("codper_odo") = dt.Rows(0).Item("codigo_per")
                Session("nomper_odo") = dt.Rows(0).Item("NombreTrabajador")
                Response.Redirect("frmDocenteAprobacion.aspx")
            Else
                ShowMessage("Usuario o clave incorrecta", MessageType.Error)
            End If
        Catch ex As Exception
            ShowMessage("Error al intentar logearse: " + ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub lnkClave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkClave.Click
        Dim cls As New ClsMail
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Dim strCorreo As String = ""
        Dim strMensaje As String = ""
        Dim strNombre As String = ""
        Try
            If Me.txtUsuario.Text = "" Then
                ShowMessage("Ingresar usuario", MessageType.Error)
                Exit Sub
            End If

            dt = obj.TraerDataTable("ODO_RetornaClaveUsuarioCefo", Me.txtUsuario.Text.ToUpper.Trim.Replace("USAT\", ""))

            If dt.Rows.Count = 0 Then
                ShowMessage("No se encontró el usuario", MessageType.Error)
            Else
                If dt.Rows(0).Item("estado_Per") = 0 Then
                    ShowMessage("Se encuentra inactivo, por favor coordinar con el área de personal", MessageType.Error)
                Else
                    If dt.Rows(0).Item("usuario_per").ToString.Trim.Length <> 0 Then
                        If dt.Rows(0).Item("clave_Per").ToString.Trim.Length <> 0 Then
                            strCorreo = dt.Rows(0).Item("usuario_per") & "@usat.edu.pe"
                            strNombre = dt.Rows(0).Item("nombres_Per")
                            strMensaje = "Estimado(a) " & strNombre & " su clave de acceso es: " & dt.Rows(0).Item("clave_Per").ToString
                            cls.EnviarMail("campusvirtual@usat.edu.pe", "Campus Virtual", strCorreo, "Recuperar Clave CEFO", strMensaje, False)
                            ShowMessage("Clave enviada al correo " & strCorreo, MessageType.Success)
                        Else
                            ShowMessage("No tiene clave registrada, coordinar con Desarrollo de Sistemas", MessageType.Error)
                        End If                       
                    Else
                        ShowMessage("No se encontró un correo asociado a su usuario", MessageType.Error)
                    End If
                End If
            End If
        Catch ex As Exception
            ShowMessage("Error al intentar logearse: " + ex.Message, MessageType.Error)
        End Try
    End Sub
End Class
