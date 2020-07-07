
Partial Class rpteresumencargaprofesoresdpto
    Inherits System.Web.UI.Page
    Dim PersonalAnterior As Int32 = -1
    Dim Horas As Int32 = 0
    Dim PrimeraFila As Int32 = -1
    Dim Contador As Int32 = 0
    Dim total As Int32 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim id As String

            id = Request.QueryString("id")

            ClsFunciones.LlenarListas(Me.dpDpto, obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 0, id, 0, 0, 0), "codigo_dac", "nombre_dac", ">>Seleccione el Dpto. Académico<<")
            ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            'Session("codigo_usu2") = Request.QueryString("id")
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.GridView1.DataSource = obj.TraerDataTable("CAR_ConsultarCursosProgramadosXDpto", 2, Me.dpCiclo.SelectedValue, Me.dpDpto.SelectedValue, 0)
        Me.GridView1.DataBind()
        obj = Nothing

        Me.cmdExportar.Visible = Me.GridView1.Rows.Count > 0
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Contador = Contador + 1
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Cells(10).Text = CInt(fila("horasdiarias_ded")) - CInt(fila("totalHoras_Car"))
            'Añadir evento al profesor
            e.Row.Cells(1).Attributes.Add("onClick", "AbrirPopUp('../expediente/consultas/personal.aspx?id=" & Me.GridView1.DataKeys(e.Row.RowIndex).Value & "','500','600','yes','yes','yes')")
            e.Row.Cells(0).Text = Contador
        End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=ConsolidadoResponsabilidadesprofesores" & Me.dpCiclo.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
