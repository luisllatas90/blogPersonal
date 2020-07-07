
Partial Class lstdirectorio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            Dim mensajetodos As String = "--Seleccione--"
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim modulo As Int16 = Request.QueryString("mod")

            'tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, codigo_tfu, codigo_usu)
            tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, 1, codigo_usu)

            ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", mensajetodos)
            ClsFunciones.LlenarListas(Me.dpciclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac", "-Ninguno-")

            'Cargar combos
            tbl.Dispose()
            obj = Nothing
            'Me.cmdExportar.Enabled = Me.GridView1.Rows.Count > 0
        End If
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")
            'CType(e.Row.FindControl("cmdVer"), Button).Attributes.Add("OnClick", "AbrirPopUp('frmcambiardatosalumno.aspx?c=" & fila.Row("codigouniver_alu") & "&x=" & fila.Row("codigo_alu").ToString & "&id=" & ID & "','550','650','yes','yes');return(false);")
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
        Response.AddHeader("Content-Disposition", "attachment;filename=directorioestudiantes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As New Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_ConsultaDirectorio", Me.dpEscuela.SelectedValue, Me.dpciclo.SelectedValue)
            Me.GridView1.DataSource = dt
            Me.GridView1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
