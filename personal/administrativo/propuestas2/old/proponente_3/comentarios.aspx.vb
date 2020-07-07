
Partial Class proponente_comentarios
    Inherits System.Web.UI.Page

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("nuevocomentario.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))
    End Sub


    Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Response.Write("LEIDO")

        ObjCnx.Ejecutar("PRP_RegistrarLecturaComentario", Request.QueryString("codigo_prp"), Me.DataList1.DataKeys(e.Item.ItemIndex), Request.QueryString("codigo_per"))



        '    Me.Label1.Text = Me.DataList1.DataKeys(e.Item.ItemIndex)
    End Sub

    Protected Sub DataList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataList1.SelectedIndexChanged

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
