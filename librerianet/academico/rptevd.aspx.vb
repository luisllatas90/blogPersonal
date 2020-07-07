
Partial Class rptevd
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16
            Dim codigo_usu As Integer
            Dim mensajetodos As String = "--Seleccione--"

            codigo_tfu = Request.QueryString("ctf")
            codigo_usu = Request.QueryString("id")
            Me.lblMenu.Text = Request.QueryString("menu")

            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                mensajetodos = "--Todos--"
            Else
                tbl = obj.TraerDataTable("consultaracceso", "ESC", "Silabo", codigo_usu)
            End If

            ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", mensajetodos)
            ClsFunciones.LlenarListas(Me.dpciclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")

            'Cargar combos
            tbl.Dispose()
            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Me.GridView1.Columns.Clear()
        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarRendimientoAcademico", Me.dpTipo.SelectedValue, Me.dpciclo.SelectedValue, Me.dpEscuela.SelectedValue)
        Me.GridView1.DataBind()

        obj = Nothing

        Me.cmdExportar.Visible = Me.GridView1.Rows.Count > 0
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim fila As Data.DataRowView
        '    fila = e.Row.DataItem
        '    e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
        '    e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        '    e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        'End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=directorioestudiantes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
