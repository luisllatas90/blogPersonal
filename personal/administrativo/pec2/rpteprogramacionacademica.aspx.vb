
Partial Class rpteprogramacionacademica
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Llenar combos
            'Cargar el ciclo académico
            Dim cls As New ClsFunciones
            Dim obj As New ClsConectarDatos
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim modulo As Int16 = Request.QueryString("mod")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            cls.CargarListas(Me.dpEscuela, obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, codigo_tfu, codigo_usu), "codigo_cpf", "nombre_cpf", "--Seleccione el Programa--")
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
        Me.grwCursosProgramados.DataSource = obj.TraerDataTable("ConsultarDatosProgramaEspecial", 10, Me.dpEscuela.SelectedValue, Me.dpCiclo.SelectedValue, 0, 0)
        Me.grwCursosProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        If Me.grwCursosProgramados.Rows.Count > 0 Then
            Me.lblMensaje.Text = Me.grwCursosProgramados.Rows.Count.ToString & " cursos programados."
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
End Class
