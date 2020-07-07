
Partial Class Biblioteca_LIBROSPEDIDOS
    Inherits System.Web.UI.Page
    'Dim Dtb_Reporte As New Data.DataTable

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
        ConsultarLibrosPedidos(grid)

        If ChkFechas.Checked = True Then
            writer1.Write("<strong>" & Me.LblTitulo.Text & " [" & Me.CboVer.SelectedItem.Text & "] DEL " & Me.txtFinicio.Text & " AL " & Me.txtFfin.Text & "</strong><br> Módulo Biblioteca - Campus Virtual - USAT")
        Else
            writer1.Write("<strong>" & Me.LblTitulo.Text & " [" & Me.CboVer.SelectedItem.Text & "] A LA FECHA " & Date.Now.ToShortDateString & "</strong><br> Módulo Biblioteca - Campus Virtual - USAT")
        End If

        grid.EnableViewState = False
        page1.EnableViewState = False
        page1.EnableEventValidation = False
        grid.HeaderStyle.BackColor = Drawing.Color.LightBlue

        page1.Controls.Add(form1)
        form1.Controls.Add(grid)

        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='1' color='#121212'><br>Sistema de Biblioteca - Desarrollo de Sistemas <br>Actualizado al :" & Date.Now() & "</font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function


    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        ConsultarLibrosPedidos(GvLibros)
    End Sub

    Private Sub ConsultarLibrosPedidos(ByVal grid As GridView)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)

        Dim Dtb_Reporte As New Data.DataTable
        If ChkFechas.Checked = True Then
            Dtb_Reporte = obj.TraerDataTable("BI_LibrosMasConsultadosXFecha", CboVer.SelectedValue, txtTextoBus.Text, txtFinicio.Text, txtFfin.Text, TxtCantidad.Text)
        Else
            Dtb_Reporte = obj.TraerDataTable("BI_LibrosMasConsultados", CboVer.SelectedValue, txtTextoBus.Text, TxtCantidad.Text)
        End If
        If Dtb_Reporte.Rows.Count > 0 Then
            Me.LblTotal.Text = Dtb_Reporte.Rows.Count & " registros"
            grid.DataSource = Dtb_Reporte
            grid.DataBind()
        Else
            Me.LblTotal.Text = "0 registros"
            grid.DataSource = Nothing
            grid.DataBind()
        End If

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.txtTextoBus.Enabled = False
            Me.txtTextoBus.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3")
            Me.TxtCantidad.Visible = False
            Me.LblSigno.Visible = False
        End If
    End Sub

    Protected Sub CmbConsultar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboConsultar.SelectedIndexChanged
        If Me.CboConsultar.SelectedValue = 0 Then
            Me.txtTextoBus.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3")
            Me.txtTextoBus.Enabled = False
        Else
            Me.txtTextoBus.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFBF")
            Me.txtTextoBus.Enabled = True
        End If
    End Sub

    Protected Sub GvLibros_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GvLibros.PageIndexChanging
        Dim grilla As GridView = CType(sender, GridView)
        With grilla
            .PageIndex = e.NewPageIndex()
        End With
        ConsultarLibrosPedidos(Me.GvLibros)
    End Sub

    Protected Sub GvLibros_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvLibros.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim dewey As String = ""
            fila = e.Row.DataItem
            With fila.Row
                If .Item("abreviatura_pre").ToString <> "" Then
                    dewey = dewey + .Item("abreviatura_pre").ToString + "/"
                End If
                dewey = dewey + .Item("codigoingreso").ToString + .Item("abreviatura_post").ToString + " " + _
                    .Item("tomovolumen").ToString + " "
                If .Item("abreviatura_ejm").ToString <> "" Then
                    dewey = dewey + "ej. " + .Item("abreviatura_ejm").ToString
                End If
            End With
            e.Row.Cells(1).Text = dewey
        End If
    End Sub

    Protected Sub CboVer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboVer.SelectedIndexChanged
        TxtCantidad.Text = ""
        If Me.CboVer.SelectedValue = "TO" Then
            Me.TxtCantidad.Visible = False
            Me.LblSigno.Visible = False
        Else
            Me.TxtCantidad.Visible = True
            Me.LblSigno.Visible = True
            If Me.CboVer.SelectedValue = "PR" Then
                LblSigno.Text = " >= "
            ElseIf Me.CboVer.SelectedValue = "NP" Then
                LblSigno.Text = " <= "
            End If
        End If
        
    End Sub

End Class
