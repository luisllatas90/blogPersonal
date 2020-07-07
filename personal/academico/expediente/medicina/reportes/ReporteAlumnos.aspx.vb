
Partial Class medicina_reportes_ReporteAlumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Codigo_cup As Integer
        Codigo_cup = Request.QueryString("codigo_cup")

        If Not IsPostBack Then
            Dim tblCurso As Data.DataTable
            Dim Alumnos As New Data.DataTable
            Dim Codigo_alu As Integer
            Dim Contador As Integer

            tblCurso = obj.TraerDataTable("ConsultarCursoProgramado", 10, Codigo_cup, 0, 0, 0)
            If tblCurso.Rows.Count > 0 Then
                Me.lblcurso.Text = tblCurso.Rows(0).Item("nombre_cur") & " - Grupo (" & tblCurso.Rows(0).Item("grupohor_cup") & ")"
                Me.lblInicio.Text = tblCurso.Rows(0).Item("fechainicio_cup")
                Me.lblFin.Text = tblCurso.Rows(0).Item("fechafin_cup")
            End If

            Codigo_alu = 0
            Contador = 1

            Alumnos = obj.TraerDataTable("MED_ConsultarAlumnosCurso", 0, Codigo_cup)
            gvAlumnos.DataSource = Alumnos
            gvAlumnos.DataBind()
            
        End If

    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment;filename=Asistencias.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()
    End Sub

    Public Function HTML() As String
        Dim page1 As New Page
        Dim form1 As New HtmlForm
        Dim lbl As New Label
        lbl.Text = "<br><br>"

        'Me.TblAsistencia.EnableViewState = False
        page1.EnableViewState = False

        page1.Controls.Add(form1)
        form1.Controls.Add(Me.lblcurso)
        form1.Controls.Add(lbl)
        form1.Controls.Add(lbl)
        'form1.Controls.Add(Me.TblAsistencia)
        form1.Controls.Add(Me.gvAlumnos)
        form1.Controls.Add(lbl)


        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='2' color='#121212'>Listado de alumnos matriculados - USAT<br>Actualizado al :" & Date.Now() & "</font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function

    Protected Sub gvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

End Class
