
Partial Class noadeudo_formatoImpresion
    Inherits System.Web.UI.Page
    Dim per As Integer
    Dim ctf As Integer
    Dim ade As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim dts As New Data.DataTable
        Dim obj As New ClsNoAdeudos

        per = Request.QueryString("id")
        ctf = Request.QueryString("ctf")
        ade = Request.QueryString("ade")

        dts = obj.ConsultarNoAdeudos("I", ade)

        Me.lblSolicitud.Text = dts.Rows(0).Item("Nro")
        Me.lblFecha.Text = dts.Rows(0).Item("Fecha")
        Me.lblEstudiante.Text = dts.Rows(0).Item("Alumno")
        Me.lblEscuela.Text = dts.Rows(0).Item("Escuela")
        Me.lblRevisiones.Text = dts.Rows(0).Item("Revisiones")

        mostrarRevision(ade)


    End Sub
    Private Sub mostrarRevision(ByVal codigo_cade As Integer)
        Dim obj As New ClsNoAdeudos
        gvRevisiones.Visible = True
        gvRevisiones.DataSource = obj.ConsultarRevisiones(codigo_cade)
        gvRevisiones.DataBind()
    End Sub

    Protected Sub cmdCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCerrar.Click
        Response.Redirect("consultarnoadeudo.aspx?id=" & per & "&ctf=" & ctf)
    End Sub

    Protected Sub cmdImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click


    End Sub
End Class
