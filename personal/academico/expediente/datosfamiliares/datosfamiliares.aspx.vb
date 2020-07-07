
Partial Class _Default

    Inherits System.Web.UI.Page
    Dim id As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("act") = "M" Then
            Page.RegisterStartupScript("CANCEL", "<SCRIPT>alert('Se actualizaron los datos satisfactoriamente')</SCRIPT>")
        End If
        If Request.QueryString("act") = "N" Then
            Page.RegisterStartupScript("CANCEL", "<SCRIPT>alert('Se Registraron los datos satisfactoriamente')</SCRIPT>")
        End If
        id = Request.QueryString("id")
        CargarGrid(id)
    End Sub
    Private Sub CargarGrid(ByVal codigo_per As Integer)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos, EstadoCuenta As New Data.DataTable
        datos = Obj.TraerDataTable("FAM_ConsultarDatosFamilia", "PE", codigo_per)

        dgvDatos.DataSource = datos
        dgvDatos.DataBind()

        lblCantidad.Text = CStr(dgvDatos.Rows.Count) & " familiares (hijos / cónyuge / Tutorados) registrados."

    End Sub

    Protected Sub dgvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            ' e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            'e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            e.Row.Cells(10).Text = "<a href='datosfamiliar.aspx?&id=" & id & "&tipo=M&field=" & fila.Row("codigo_dhab").ToString & "'><IMG src='../../../../images/editar.gif' border=0 alt='Modificar Datos' ></a>"
        End If
    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Response.Redirect("datosfamiliar.aspx?tipo=N&id=" & Request.QueryString("id"))

    End Sub
End Class
