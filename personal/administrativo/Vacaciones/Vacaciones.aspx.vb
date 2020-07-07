
Partial Class administrativo_Vacaciones_Vacaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_per") = "" Then
            'Response.Write("Sesion Finalizada")
            Response.Redirect("../../ErrorSistema.aspx")
        End If

        'If (Request.Headers("referer") Is Nothing) Then
        '    Response.Redirect("../../ErrorSistema.aspx")
        'Else
        '    If Request.Headers("referer").ToLower.Contains("intranet.usat.edu.pe/campusvirtual/personal") = False Then
        '        Response.Redirect("../../ErrorSistema.aspx")
        '    End If
        'End If

        'Response.Write(Request.UserHostAddress)
        'Response.Write("<br>")
        'Response.Write(System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName)
        'Response.Write("<br>")
        'Response.Write(Session("perlogin").ToString().Trim)
        'If "http://" & HttpContext.Current.Request.Url.Host.Contains("intranet.usat.edu.pe/campusvirtual/personal") Then
        'Response.Redirect("../../ErrorSistema.aspx")
        'End If

    End Sub
End Class
