
Partial Class personal_academico_expediente_medicina_reportes_ReporteUtilizacionNotasAsistencias
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim datos As New Data.DataTable
        Dim objCnx As New ClsConectarDatos
        Dim codigo_tfu As Int16

        If Not IsPostBack Then
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        ' ************ Consultar ciclo académico *************
        datos = objCnx.TraerDataTable("ConsultarCicloAcademico", "TO", "")
        If datos.Rows.Count > 0 Then
            ClsFunciones.LlenarListas(Me.cboCicloAcad, datos, "codigo_cac", "descripcion_cac")
            cboCicloAcad.SelectedValue = objCnx.TraerDataTable("ConsultarCicloAcademico", "CV", "1").Rows(0).Item("codigo_cac")
        End If

        ' ************ Consultar facultad *************
        codigo_tfu = Request.QueryString("ctf")
        If codigo_tfu = 1 Or codigo_tfu = 25 Or codigo_tfu = 27 Or codigo_tfu = 35 Then
            ClsFunciones.LlenarListas(Me.cboEscuela, objCnx.TraerDataTable("ConsultarCarreraProfesional", "TC", ""), "codigo_cpf", "nombre_cpf")
        Else
            datos = objCnx.TraerDataTable("ConsultarAccesoRecurso", "3", Request.QueryString("id"), "", "")
            If datos.Rows.Count > 0 Then
                ClsFunciones.LlenarListas(Me.cboEscuela, datos, "codigo_cpf", "Nombre_cpf")
            Else
                cboEscuela.Items.Add(">>No definido<<")
            End If
        End If
        objCnx.CerrarConexion()
	end if

    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objCnx As New ClsConectarDatos
        If Me.cboCicloAcad.SelectedValue <= 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "cicloAcad", "alert('Debe seleccionar un ciclo académico')", True)
            Exit Sub
        End If
        If Me.cboEscuela.SelectedValue <= 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "escuela", "alert('Debe seleccionar una Escuela Profesional')", True)
            Exit Sub
        End If
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        gvNotas.DataSource = objCnx.TraerDataTable("dbo.MED_ConsultarUtilizacionNotasAsistencias", Me.cboEscuela.SelectedValue, Me.cboCicloAcad.SelectedValue)
        gvNotas.DataBind()
        objCnx.CerrarConexion()
    End Sub


    Protected Sub gvNotas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvNotas.SelectedIndexChanged

    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        cmdConsultar_Click(sender, e)
        Axls("ReporteNotasyAsistencias" & Date.Now.Day.ToString & Date.Now.Month.ToString & Date.Now.Year.ToString, gvNotas, "REPORTE DE UTILIZACIÓN DEL MÓDULO NOTAS Y ASISTENCIAS", "Sistema de Notas y Asistencias - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        grid.HeaderRow.BackColor = Drawing.Color.FromName("#FF9900")
        grid.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(grid, titulo, piedepagina))
        Response.End()
    End Sub
End Class
