
Partial Class admintareas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("vez") = 1 Then
            Session("idusuario2") = Replace(Request.QueryString("idusuario"), "***", "\")
            Session("idtarea2") = Request.QueryString("idtarea")
            Session("idcursovirtual2") = Request.QueryString("idcursovirtual")
            Session("idvisita2") = Request.QueryString("idvisita")
            Session("codigo_tfu2") = Request.QueryString("codigo_tfu")
            Session("titulotarea") = Request.QueryString("titulotarea")
        End If
        Session("idestadorecurso") = Request.QueryString("idestadorecurso")

        If Session("codigo_tfu2") > 1 Then
            Response.Redirect("trabajosenviados.aspx") '?idestadorecurso=" + Session("idestadorecurso"))
            Exit Sub
        End If


    End Sub
 
    
End Class
