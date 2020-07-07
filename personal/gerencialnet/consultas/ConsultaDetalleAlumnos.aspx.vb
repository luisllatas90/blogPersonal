
Partial Class consultas_ConsultaDetalleAlumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
            Dim DT As New Data.DataTable
            ClsFunciones.LlenarListas(Me.ddlCarrera, Obj.TraerDataTable("ConsultarCarreraProfesional", "IV", ""), "codigo_cpf", "Nombre_cpf", "--- Todas ---")
            ClsFunciones.LlenarListas(Me.ddlCicloMat, Obj.TraerDataTable("ConsultarCicloAcademico", "U5", ""), "codigo_cac", "descripcion_cac", "--- Todas ---")
            ClsFunciones.LlenarListas(Me.ddlCicloIng, Obj.TraerDataTable("ConsultarCicloAcademico", "U5", ""), "descripcion_cac", "descripcion_cac", "--- Todas ---")
            ClsFunciones.LlenarListas(Me.ddlDepartamento, Obj.TraerDataTable("ConsultarLugares", "2", "156", ""), "codigo_Dep", "nombre_dep", "-Departamento-")
            ClsFunciones.LlenarListas(Me.ddlProvincia, DT, "", "", "-Provincia-")
            ClsFunciones.LlenarListas(Me.ddlDistrito, DT, "", "", "-Distrito-")
            ClsFunciones.LlenarListas(Me.ddlColegio, DT, "", "", "--- Todos los Colegios ---")
        End If
        Me.GridView1.DataBind()
    End Sub

    Protected Sub ddlDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamento.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        ClsFunciones.LlenarListas(Me.ddlProvincia, Obj.TraerDataTable("ConsultarLugares", "3", Me.ddlDepartamento.SelectedValue, ""), "codigo_Pro", "nombre_pro", "-Provincia-")
        ClsFunciones.LlenarListas(Me.ddlDistrito, Obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvincia.SelectedValue, ""), "codigo_dis", "nombre_dis", "-Distrito-")
        ClsFunciones.LlenarListas(Me.ddlColegio, Obj.TraerDataTable("ConsultarLugares", "5", Me.ddlDistrito.SelectedValue, ""), "codigo_col", "nombre_col", "--- Todos los Colegios ---")
    End Sub

    Protected Sub ddlProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvincia.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        ClsFunciones.LlenarListas(Me.ddlDistrito, Obj.TraerDataTable("ConsultarLugares", "4", Me.ddlProvincia.SelectedValue, ""), "codigo_dis", "nombre_dis", "-Distrito-")
        ClsFunciones.LlenarListas(Me.ddlColegio, Obj.TraerDataTable("ConsultarLugares", "5", Me.ddlDistrito.SelectedValue, ""), "codigo_col", "nombre_col", "--- Todos los Colegios ---")
    End Sub

    Protected Sub ddlDistrito_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDistrito.SelectedIndexChanged
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        ClsFunciones.LlenarListas(Me.ddlColegio, Obj.TraerDataTable("ConsultarLugares", "5", Me.ddlDistrito.SelectedValue, ""), "codigo_col", "nombre_col", "--- Todos los Colegios ---")
    End Sub

    Protected Sub CmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdConsultar.Click
    
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteEstudiantes_.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub
  
    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim Grid As New GridView
        Dim Obj As New clsReportes

        Grid.DataSource = Obj.ConsultaDatosDetalladoAlumnos("PR", Me.ddlCicloMat.SelectedValue, Me.ddlCicloIng.SelectedValue, Me.ddlsexo.SelectedValue, Me.ddlColegio.SelectedValue, _
                            Me.ddlCarrera.SelectedValue, Me.ddlCondicion.SelectedValue, "", Me.txtNombre.Text)
        Grid.DataBind()
        Grid.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False
   
        'Form2.Controls.Add(Me.LblTitulo)
        Form2.Controls.Add(Grid)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls()
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        'Dim pagina As Page = New Page
        'Dim form = New HtmlForm
        'Me.GridView1.EnableViewState = False
        'pagina.EnableEventValidation = False
        'pagina.DesignerInitialize()
        'pagina.Controls.Add(form)
        'form.Controls.Add(GridView1)
        'pagina.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default
        'Response.Write(sb.ToString())
        'Response.End()

    End Sub
End Class
