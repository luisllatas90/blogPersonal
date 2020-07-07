
Partial Class rendimientoacadprogramas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Cargar el ciclo académico
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpPlanEstudio, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 2, request.querystring("ctf"), request.querystring("id"), 0, 0), "codigo_pes", "descripcion_pes", "--Seleccione--")

            obj = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 4, Me.dpPlanEstudio.SelectedValue, Me.dpVersion.SelectedValue, 0, 0)
        Me.GridView1.DataBind()
        obj = Nothing
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")


        End If
    End Sub

    Protected Sub dpPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPlanEstudio.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpVersion, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 3, Me.dpPlanEstudio.SelectedValue, 0, 0, 0), "edicionProgramaEspecial_Alu", "edicionProgramaEspecial_Alu", "--Seleccione--")
        obj = Nothing
        cmdBuscar.Enabled = Me.dpVersion.Items.Count > 1
        cmdExportar.Enabled = Me.dpVersion.Items.Count > 1
    End Sub

    Protected Sub lnkAprobados_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPopUp.Click
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim id As Integer = Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Values("codigo_cur"))

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.lblCurso.Text = Me.GridView1.DataKeys(gvr.RowIndex).Values("nombre_cur")
        Me.lblProfesor.Text = Me.GridView1.DataKeys(gvr.RowIndex).Values("docente")

        Me.GridView2.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 5, "A", Me.dpVersion.SelectedValue, Me.dpPlanEstudio.SelectedValue, id)
        Me.GridView2.DataBind()
        obj = Nothing


        Me.mpeFicha.Show()

    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub lnkDesaprobados_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim id As Integer = Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Values("codigo_cur"))

        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.lblCurso.Text = Me.GridView1.DataKeys(gvr.RowIndex).Values("nombre_cur")
        Me.lblProfesor.Text = Me.GridView1.DataKeys(gvr.RowIndex).Values("docente")

        Me.GridView2.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 5, "D", Me.dpVersion.SelectedValue, Me.dpPlanEstudio.SelectedValue, id)
        Me.GridView2.DataBind()
        obj = Nothing

        Me.mpeFicha.Show()
    End Sub

    Protected Sub cmdExportarLista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportarLista.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView2.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView2)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=listaestudiantes_Grupo_" & Me.dpVersion.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=RendimientoAcademico_Grupo_" & Me.dpVersion.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
