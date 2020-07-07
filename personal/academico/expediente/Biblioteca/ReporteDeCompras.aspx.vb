
Partial Class Biblioteca_Reporte_de_compras
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            For i As Int16 = Year(Date.Now) To 1990 Step -1
                Me.CboAnio.Items.Add(i)
            Next
            Me.CboAnio.Visible = False
        End If
    End Sub

    Protected Sub CboVer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboVer.SelectedIndexChanged
        If CboVer.SelectedValue = 1 Then
            Me.CboAnio.Visible = True
        Else
            Me.CboAnio.Visible = False
        End If
    End Sub

    Protected Sub CmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdConsultar.Click
        ConsultarCompras(Me.GvwDatos)
    End Sub

    Private Sub ConsultarCompras(ByRef grid As GridView)
        Dim Datos As New Data.DataTable
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
        Datos = Obj.TraerDataTable("BIB_ConsultarCompras", Me.CboVer.SelectedValue, Me.CboAnio.SelectedItem.Text)
        If Datos.Rows.Count > 0 Then
            Me.LblTotal.Text = Datos.Rows.Count & " registros"
            grid.DataSource = Datos
        Else
            Me.LblTotal.Text = "0 registros"
            grid.DataSource = Nothing
        End If
        grid.DataBind()
    End Sub

    Protected Sub GvwDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvwDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            String.Format("0:dd/MM/yyyy", e.Row.Cells(9))
            'e.Row.Cells(9).Text = Format(e.Row.Cells(9), "0:dd/MM/yyyy")
        End If
    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment;filename=reporte.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()
    End Sub

    Public Function HTML() As String
        Dim page1 As New Page
        Dim form1 As New HtmlForm
        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)
        Dim grid As New GridView
        ConsultarCompras(grid)

        grid.EnableViewState = False
        page1.EnableViewState = False
        page1.EnableEventValidation = False

        page1.Controls.Add(form1)
        writer1.Write("<strong>" & Me.LblTitulo.Text & " A LA FECHA " & Date.Now.ToShortDateString & "</strong><br> Módulo Biblioteca - Campus Virtual - USAT")
        grid.HeaderStyle.BackColor = Drawing.Color.LightBlue

        form1.Controls.Add(grid)
        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='1' color='#121212'><br><br><strong>Sistema de Consultas Hemeroteca - Desarrollo de Sistemas <br>Actualizado al: " & Date.Now() & "<strong></font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function
End Class
