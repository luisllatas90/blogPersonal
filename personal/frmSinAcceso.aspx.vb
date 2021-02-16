
Partial Class frmSinAcceso
    Inherits System.Web.UI.Page

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Request.QueryString("msj")) Then
            Call mt_RedirigirLogin("La sesión ha expirado, debe iniciar nuevamente sesión para volver a realizar la operación.", MessageType.warning)
        Else
            Call mt_RedirigirLogin(Request.QueryString("msj").ToString, MessageType.warning)
        End If
    End Sub

    Private Sub mt_RedirigirLogin(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "redirigirLogin", "redirigirLogin('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try            
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
