
Partial Class SesionesPOA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("idPto") = "" Then
            Session("idPto") = Request.QueryString("id")
        End If
        If Session("ctfPto") = "" Then
            Session("ctfPto") = Request.QueryString("ctf")
        End If

        If Request.QueryString("opcion") = "EVAL_PTO" Then
            Session("actividadPto") = Request.QueryString("actividadPto")
            Session("nombre_poa") = Request.QueryString("despoa")
            Session("cecoPto") = Request.QueryString("nombreCeco")
            Session("instanciaPto") = Request.QueryString("instancia")
            Session("estadoPto") = Request.QueryString("estado")
            Session("codigoPto") = Request.QueryString("field")

            Response.Redirect("presupuesto/areas/ModificarPresupuesto_V3.aspx?id=" & Session("idPto") & "&ctf=" & Session("ctfPto") & "&op=evaPOA&cb1=" & _
                              Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & _
                              "&cb4=" & Request.QueryString("cb4") & "&acp=" & Request.QueryString("acp"))

            'Response.Redirect("presupuesto/areas/ModificarPresupuesto_V3-HC.aspx?id=" & Session("idPto") & "&ctf=" & Session("ctfPto") & "&op=evaPOA&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4"))
        End If

    End Sub
End Class
