
Partial Class proponente_nuevocomentario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("comentarios.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))

    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click

        Dim codigo_prp, codigo_dap, codigo_dip, codigo_cop, modifica As String
        Dim rsDatos As New Data.DataTable
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim codigo_ipr As Integer

        rsDatos = ObjCnx.TraerDataTable("ConsultarInvolucradoPropuesta", "RK", Request.QueryString("codigo_prp"), Request.QueryString("codigo_per"))

        codigo_ipr = rsDatos.rows(0).item(0).toString
        'Response.Write(codigo_ipr)

        codigo_cop = ObjCnx.TraerValor("RegistraComentario", codigo_ipr, 0, "A", Me.txtAsunto.Text, Me.txtComentario.Text, "O", 0)
        Response.Redirect("comentarios.aspx?codigo_prp=" & Request.QueryString("codigo_prp") & "&codigo_per=" & Request.QueryString("codigo_per"))

    End Sub
End Class
