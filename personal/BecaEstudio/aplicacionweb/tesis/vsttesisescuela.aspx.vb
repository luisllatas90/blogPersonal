
Partial Class personal_academico_tesis_vsttesisescuela
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

            If Request.QueryString("modo") = "F" Then
                Me.lblEstado.Text = "ESTADO DE TESIS:"
                Me.dpEscuela.Visible = False
                Me.CargarEstados(Request.QueryString("codigo_cpf"))
            Else
                If Request.QueryString("ctf") = 1 Then
                    ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
                Else
                    ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("consultaracceso", "ESC", "", Session("id_per")), "codigo_cpf", "nombre_cpf", "-Seleccione-")
                End If
            End If
            obj = Nothing
            dpFase_SelectedIndexChanged(sender, e)
        End If
    End Sub
    Private Sub CargarEstados(ByVal codigo_cpf As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarTesis", 6, codigo_cpf, 0, 0), "codigo_Eti", "nombre_eti")
        obj = Nothing
    End Sub
    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        CargarEstados(Me.dpEscuela.SelectedValue)
        dpFase_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub dpFase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpFase.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim escuela As String
        If Request.QueryString("modo") = "F" Then
            escuela = Request.QueryString("codigo_cpf")
        Else
            escuela = Me.dpEscuela.SelectedValue
        End If

        If escuela <> -1 Then
            Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis", 2, Me.dpFase.SelectedValue, 1, escuela)
            Me.GridView1.DataBind()
            obj = Nothing
            Me.lblTotal.Text = "TOTAL: " & Me.GridView1.Rows.Count
        End If
        If GridView1.rows.count > 0 Then
            Me.btnExportar.enabled = True
        Else
            Me.btnExportar.enabled = False
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_tes").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteTesis_" & Me.dpEscuela.SelectedItem.Text & "_Etapa_" & Me.dpFase.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub

End Class
