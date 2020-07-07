
Partial Class administrativo_activofijo_registroActivo_v3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                If (Request.QueryString("Ped") <> "") Then
                    Dim cod_ped As Integer
                    Dim cod_art As String
                    Dim cantidad As String
                    Dim egreso As String
                    Dim cod_user As Integer

                    cod_ped = Request.QueryString("Ped")
                    Me.param1.Value = cod_ped

                    cod_art = Request.QueryString("Art").ToString
                    Me.param2.Value = cod_art

                    cantidad = Request.QueryString("Cant").ToString
                    Me.param3.Value = cantidad

                    egreso = Request.QueryString("Egr")
                    Me.hdIEgreso.Value = egreso
                    'Me.hdIEgreso.Value = 1091932

                    'cod_user = Request.QueryString("id")
                    'Me.hdUser.Value = cod_user

                    'Response.Write("RW: " & Request.QueryString("Art").ToString)

                End If
            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            End Try
        End If
    End Sub

    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function

    Function decode(ByVal str As String) As String
        Return System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(str))
    End Function

End Class
