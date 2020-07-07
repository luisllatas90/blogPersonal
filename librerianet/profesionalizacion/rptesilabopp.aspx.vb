
Partial Class rptesilabopp
    Inherits System.Web.UI.Page
    Dim SilabosPublicados As Int16 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Llenar combos
            'Cargar el ciclo académico
            Dim cls As New ClsFunciones
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            cls.CargarListas(Me.dpPlanEstudio, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 2, Request.QueryString("ctf"), Request.QueryString("id"), 0, 0), "codigo_pes", "descripcion_pes", "--Seleccione el Programa--")
            cls.CargarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", "0"), "codigo_cac", "descripcion_cac", "--Seleccione--")
            obj.CerrarConexion()
            obj = Nothing
            cls = Nothing
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.cmdExportar.Visible = False
        Me.lblMensaje.Text = ""
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwCursosProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 10, Me.dpPlanEstudio.SelectedValue, Me.dpCiclo.SelectedValue, 0, 0)
        Me.grwCursosProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        If Me.grwCursosProgramados.Rows.Count > 0 Then
            Me.lblMensaje.Text = "TOTAL: "
            Me.lblMensaje.Text = Me.lblMensaje.Text & "Cursos programados: " & Me.grwCursosProgramados.Rows.Count.ToString
            Me.lblMensaje.Text = Me.lblMensaje.Text & "| Cursos con silabos: " & Me.SilabosPublicados.ToString
            Me.lblMensaje.Text = Me.lblMensaje.Text & "| Cursos sin silabos: " & CInt(Me.grwCursosProgramados.Rows.Count) - CInt(Me.SilabosPublicados)
            Me.cmdExportar.Visible = True
        End If
    End Sub
    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwCursosProgramados.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwCursosProgramados)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=ProgramacionAcademica" & Me.dpCiclo.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub grwCursosProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCursosProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1 'Me.GridView1.DataKeys(e.Row.RowIndex).Value
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            If fila.Row("fechasilabo_Cup").ToString <> "" Then
                e.Row.Cells(10).Text = "<a href=""../../silabos/" & Me.dpCiclo.SelectedItem.Text & "/" & fila("codigo_cup").ToString & ".zip""><img src=""../../images/ext/zip.gif"" ALT=""Ver Silabus"" border=0></a>"
                SilabosPublicados = SilabosPublicados + 1
                'CType(e.Row.FindControl("cmdVer"), Button).Attributes.Add("OnClick", "AbrirPopUp('frmcambiardatosalumno.aspx?c=" & fila.Row("codigouniver_alu") & "&x=" & fila.Row("codigo_alu").ToString & "&id=" & id & "','550','650','yes','yes');return(false);")
            End If
        End If
    End Sub
End Class
