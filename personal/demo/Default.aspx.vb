Imports System.DirectoryServices

Partial Class demo_Default
    Inherits System.Web.UI.Page

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

    Protected Sub btnIngresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        Dim cls As New clsAccesoAD
        If cls.ValidarUsuario(Me.txtUsuario.Text, Me.txtClave.Text) = True Then
            'Response.Redirect("../listaaplicaciones.asp?x=1")
            'Response.Redirect("listaaplicaciones2020.aspx")
            Response.Redirect("../acceder2020.asp")
        Else
            'Response.Write("Error al ingresar")
            mt_ShowMessage("Lo sentimos, Ud. no tiene acceso al Campus Virtual. Para cualquier consulta contáctese con el Área de Personal.", MessageType.error)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write("<u>Datos Probados en producción</u><br/>")
        'Response.Write("Dominio: " & HttpContext.Current.Request.Url.Host & "<br/>")
        'Response.Write("IP: " & HttpContext.Current.Request.UserHostName & "<br/>")
        'Response.Write("Tipo Aut.: " & User.Identity.AuthenticationType & "<br/>")
        'Response.Write("Usuario (USAT\Usuario): " & User.Identity.Name)
    End Sub

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
