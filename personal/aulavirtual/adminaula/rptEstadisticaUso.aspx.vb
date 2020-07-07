
Partial Class aulavirtual_adminaula_rptEstadisticaUso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.QueryString("mod") = 0 Then
        '    If Request.QueryString("ctf") = 1 Or Request.QueryString("id") = "87" Then
        '        If Not IsPostBack Then
        '            ConsultarCicloAcademico()
        '        End If
        '    Else
        '        Me.tabla.Visible = False
        '        Response.Write("Usted no tiene permiso para ver este reporte. Comunicarse con desarrollosistemas@usat.edu.pe")
        '        Exit Sub
        '    End If
        'Else
        If Not IsPostBack Then
            ConsultarCicloAcademico()
            ConsultarEscuelas()
        End If

        'End If
        
    End Sub

    Sub ConsultarCicloAcademico()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("Moodle_ListarCiclos", "S")
            obj.CerrarConexion()
            Me.ddlCiclo.DataTextField = "descripcion_cac"
            Me.ddlCiclo.DataValueField = "codigo_cac"
            Me.ddlCiclo.DataSource = dt
            Me.ddlCiclo.SelectedIndex = 0
            Me.ddlCiclo.DataBind()
        Catch ex As Exception
            Response.Write("Error al cargar los ciclos")
        End Try
    End Sub
    Sub ConsultarEscuelas()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim codigo_per As Integer
            'Inicio Hcano 16-06-17
            If Request.QueryString("mod") = 10 Then
                codigo_per = -1
            Else
                codigo_per = Request.QueryString("id")
            End If

            dt = obj.TraerDataTable("ListarComboEscuelas", codigo_per)
            'dt = obj.TraerDataTable("ListarComboEscuelas", Request.QueryString("id"))
            'Fin Hcano
            obj.CerrarConexion()

            If dt.Rows.Count Then
                Me.ddlCpf.DataTextField = "nombre_Cpf"
                Me.ddlCpf.DataValueField = "codigo_Cpf"
                Me.ddlCpf.DataSource = dt
                Me.ddlCpf.SelectedIndex = 0
                Me.ddlCpf.DataBind()
            Else
                Me.btnConsultar.Enabled = False
                Me.btnExportar.Enabled = False
            End If

        Catch ex As Exception
            Response.Write("Error al cargar los ciclos")
        End Try
    End Sub
    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        dt = obj.TraerDataTable("Moodle_Estadisticas_Consultar", Me.ddlCiclo.SelectedValue, Request.QueryString("mod"), Me.ddlCpf.SelectedValue)
        obj.CerrarConexion()
        'Response.Write(Me.ddlCpf.SelectedValue)
        If dt.Rows.Count Then
            Me.GridView1.DataSource = dt
        Else
            Response.Write("No se encontraron registros")
        End If
        Me.GridView1.DataBind()
     
      
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        If Me.GridView1.Rows.Count Then
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
            Response.AddHeader("Content-Disposition", "attachment;filename=EstadisticasAulaVirtual-" & Me.ddlCiclo.SelectedItem.Text & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Else
            Response.Write("Para exportar el contenido, debe consultar.")
        End If
       
    End Sub
End Class
