
Partial Class frmprocesar
    Inherits System.Web.UI.Page
    Dim cn As New clsaccesodatos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim accion As String = "", identificador As String = ""
        accion = Request.QueryString("accion")
        identificador = Request.QueryString("codigo")

        Select Case accion.ToLower
            Case "anularinformeprograma"
                Dim rpta As Integer, mensaje As String
                cn.abrirconexiontrans()
                cn.ejecutar("dbo.spAnularinformeprograma", False, rpta, mensaje, identificador, 0, "")
                If rpta <= 0 Then
                    cn.cancelarconexiontrans()

                Else
                    cn.cerrarconexiontrans()
                End If
                Response.Write("<script>alert('" & mensaje & " ');window.opener.location.href=window.opener.location.href;window.close();</script>")
        End Select



    End Sub
End Class
