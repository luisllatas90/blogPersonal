
Partial Class AsignaSesionesPer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Try
                Session.RemoveAll()

                If (Request.QueryString("per") IsNot Nothing) Then
                    'Asignamos Sesiones                            
                    Session.Add("id_per", Request.QueryString("per"))
                    Session.Add("perlogin", Request.QueryString("log"))

                    Response.Redirect("../personal/listaaplicaciones.asp?x=1")
                Else
                    Response.Redirect("../")
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub
End Class
