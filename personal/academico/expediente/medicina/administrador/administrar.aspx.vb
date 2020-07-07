'Modificado por hreyes: 30/09/09
'@sistema = Campus Virtual
'@modulo = Registro de notas y asistencias
'@funcion = Listado de profesores que activaron el curso para el módulo de asistencias y notas

Partial Class medicina_administrador_administrar
    Inherits System.Web.UI.Page
    Private Codigo_curso As String = "0"
    Private color As Boolean = True
    Private codigo_sem As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim Obj As New ClsConectarDatos
            Dim codigo_per As Integer = Request.QueryString("id")
            Dim escuela As New data.datatable
            Dim codigo_tfu As Integer = Request.QueryString("ctf")

            '*** Permisos por escuela ***'
            'If codigo_per = 1002 Or codigo_per = 466 And codigo_per = 1290 Then
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Obj.AbrirConexion()
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                ClsFunciones.LlenarListas(Me.DDLEscuela, Obj.TraerDataTable("ConsultarCarreraProfesional", "IV", ""), "codigo_cpf", "nombre_cpf")
            Else
                escuela = Obj.TraerDataTable("ConsultarAcceso", "esc", 0, codigo_per)
                If escuela.rows.count > 0 Then
                    ClsFunciones.LlenarListas(Me.DDLEscuela, escuela, "codigo_cpf", "nombre_cpf")
                Else
                    ClsFunciones.LlenarListas(Me.DDLEscuela, escuela, "codigo_cpf", "nombre_cpf", "- No Definido -")
                End If
            End If

            '*** Llenar Datos de Ciclo Académico ***
            ClsFunciones.LlenarListas(Me.DDLCiclo, Obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "Codigo_cac", "descripcion_cac")
            codigo_sem = Request.QueryString("codigo_sem")
            If codigo_sem = "" Then
                Dim datos As Data.DataTable = Obj.TraerDataTable("ConsultarCicloAcademico", "CV", 1)
                Me.DDLCiclo.SelectedValue = datos.Rows(0).Item("codigo_cac").ToString
            Else
                Me.DDLCiclo.SelectedValue = codigo_sem
            End If
            Obj.CerrarConexion()

        End If
    End Sub


    Protected Sub DDLEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLEscuela.SelectedIndexChanged
        Me.GridCursos.DataBind()
    End Sub

    Protected Sub DDLCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCiclo.SelectedIndexChanged
        Me.GridCursos.DataBind()
    End Sub

    Protected Sub GridCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridCursos.RowDataBound
        Dim obj As New ClsConectarDatos
        Dim datos As Data.DataTable
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem

            e.Row.Attributes.Add("id", "fila" & Fila.Row("codigo_cup").ToString)

            e.Row.Cells(3).Text = Fila.Item("nombre_cur").ToString.Replace("<br>", " - ")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            datos = obj.TraerDataTable("MED_ConsultarSylabus", "SI", Fila.Row("codigo_cup").ToString)
            obj.CerrarConexion()
            If datos.Rows.Count >= 1 Then
                e.Row.Cells(9).Text = "Activo"
                e.Row.Cells(9).ForeColor = Drawing.ColorTranslator.FromHtml("#0000FF")
                e.Row.Attributes.Add("OnClick", "ResaltarfilaDetalle_net('',this,'');ActivaControles(this);")
            Else
                e.Row.Cells(9).Text = "Inactivo"
                e.Row.Cells(9).ForeColor = Drawing.ColorTranslator.FromHtml("#FF0000")
            End If

            If Codigo_curso = "0" Then
                Codigo_curso = Fila.Item("identificador_cur").ToString
            End If

            If Codigo_curso <> Fila.Item("identificador_cur").ToString Then
                Codigo_curso = Fila.Item("identificador_cur").ToString
                If color = True Then
                    'e.Row.BackColor = Drawing.Color.FromArgb(240, 240, 213)
                    color = Not color
                    e.Row.Cells(1).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(2).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(3).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(4).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(5).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(6).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(7).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(8).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(9).Style.Add("BORDER-TOP", "#660000 1px solid")
                Else
                    'e.Row.BackColor = Drawing.Color.FromArgb(255, 255, 255)
                    color = Not color
                    e.Row.Cells(1).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(2).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(3).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(4).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(5).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(6).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(7).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(8).Style.Add("BORDER-TOP", "#660000 1px solid")
                    e.Row.Cells(9).Style.Add("BORDER-TOP", "#660000 1px solid")
                End If
            End If
        End If
    End Sub

    Protected Sub CmdActividades_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdActividades.Click
        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("actividades.aspx?codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

    Protected Sub CmdEvaluaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdEvaluaciones.Click

        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("evaluaciones.aspx?codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

    Protected Sub CmdRegNotas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdRegNotas.Click
        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("listaevaluaciones.aspx?codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

    Protected Sub CmdAsistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAsistencia.Click
        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("listaactividades.aspx?codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

    Protected Sub CmdProgramacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdProgramacion.Click
        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("reporteasistencia.aspx?codigo_sem=" & Me.DDLCiclo.SelectedValue.ToString & "&codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

    Protected Sub CmdEvaluacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdEvaluacion.Click
        Dim codigo_cac As Integer = Me.DDLCiclo.SelectedValue
        Dim codigo_cup As Integer = Me.txtelegido.Value.Substring(4)
        Response.Redirect("reportenotas.aspx?codigo_sem=" & Me.DDLCiclo.SelectedValue.ToString & "&codigo_cac=" & codigo_cac & "&codigo_cup=" & codigo_cup & "&codigo_per=" & Request.QueryString("id") & "&nombre_cur=" & Me.txtcurso.Value & "&nombre_per=" & Me.txtprofesor.Value)
    End Sub

End Class
