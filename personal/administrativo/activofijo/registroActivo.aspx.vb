
Partial Class administrativo_activofijo_registroActivo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'If (Request.QueryString("tipo") <> "") Then
            Dim cod_ped As Integer
            'cod_ped = decode(Request.QueryString("tipo"))
            cod_ped = 108298

            Me.param1.Value = cod_ped

            'End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try

    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

End Class
