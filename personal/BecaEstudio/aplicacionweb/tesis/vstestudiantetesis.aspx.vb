
Partial Class personal_academico_tesis_vstestudiantetesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            If Request.QueryString("ctf") = 1 Then
                ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
            Else
                ClsFunciones.LlenarListas(Me.dpEscuela, obj.TraerDataTable("consultaracceso", "ESC", "", Session("id_per")), "codigo_cpf", "nombre_cpf", "-Seleccione la Carrera Profesional-")
            End If
            obj = Nothing
            Me.txtCrdAprobados.Attributes.Add("onkeypress", "validarnumero()")
            Me.cmdBuscar.Attributes.Add("onClick", "document.all.form1.style.display='none';document.all.tblmensaje.style.display=''")
        End If
    End Sub
    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        Me.dpPlanEstudio.Items.Clear()
        If Me.dpEscuela.SelectedValue > 0 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.dpPlanEstudio, obj.TraerDataTable("ConsultarPlanEstudio", "CV", 1, Me.dpEscuela.SelectedValue), "codigo_pes", "abreviatura_pes")
            obj = Nothing
            Me.dpPlanEstudio.Visible = True
            Me.dpPlanEstudio.SelectedIndex = 0
            CargarCursos()
        End If
    End Sub
    Private Sub CargarCursos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        ClsFunciones.LlenarListas(Me.dpcurso, obj.TraerDataTable("TES_ConsultarEstudianteCursoTesis", 0, 0, Me.dpPlanEstudio.SelectedValue, 0, 0), "codigo_cur", "nombre_cur", ">>Seleccione la asignatura<<")
        obj = Nothing
        Me.grdEstudiantes.Visible = False
    End Sub

    Protected Sub dpplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpPlanEstudio.SelectedIndexChanged
        CargarCursos()
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Me.dpCurso.SelectedValue > 0 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Me.grdEstudiantes.DataSource = obj.TraerDataTable("TES_ConsultarEstudianteCursoTesis", 1, Me.dpEscuela.SelectedValue, Me.dpPlanEstudio.SelectedValue, Me.dpCurso.SelectedValue, Me.txtCrdAprobados.Text)
            Me.grdEstudiantes.DataBind()
            obj = Nothing
            Me.grdEstudiantes.Visible = True
            If grdEstudiantes.Rows.Count > 0 Then
                Me.lblTotal.Text = grdEstudiantes.Rows.Count & " estudiantes."
                Me.cmdExportar.Visible = True
            End If
        Else
            Me.grdEstudiantes.Visible = False
            Me.cmdExportar.Visible = False
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grdEstudiantes.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grdEstudiantes)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=listaestudiantes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub
End Class
