
Partial Class medicina_administrar_reporteasistenciaynotas
    Inherits System.Web.UI.Page
    Private NumAsis As Integer = 0
    Private NumFalt As Integer = 0
    Private NumTard As Integer = 0


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim consideraasistencia As Int16
            fila = e.Row.DataItem

            e.row.Cells(4).Attributes.Add("tooltip", "<table><tr><td width='300'><b>TEMAS:" & "</b><br>" & fila.Row("temas_act").ToString & "</td></tr></table>")
            e.Row.Cells(4).Attributes.Add("style", "cursor:hand")

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            '** Notas ***
            If fila.Row("considerarnota_act").ToString.ToUpper = "S" Then
                e.Row.Cells(6).Text = "SI"
            Else
                e.Row.Cells(6).Text = "NO"
                e.Row.Cells(9).Text = "<img src='../../images/bloquear.gif' alt='La actividad no considera Ingreso de Notas'>"
            End If

            '*** Asistencia ***
            consideraasistencia = IIf(fila.Row("ConsiderarAsistencia_act") Is DBNull.Value, 0, IIf(fila.Row("ConsiderarAsistencia_act") = True, 1, 0))

            If consideraasistencia = 1 Then
                e.Row.Cells(5).Text = "SI"
                e.Row.Cells(5).ForeColor = Drawing.Color.Black

                If fila.Row("CalifNum_dact") Is DBNull.Value Or fila.Row("CalifNum_dact").ToString.Trim = "" Then
                    e.Row.Cells(8).Text = "-"
                Else
                    e.Row.Cells(8).Text = fila.Row("CalifNum_dact")
                End If

                If fila.Row("tipoasistencia_act") Is System.DBNull.Value Then
                    e.Row.Cells(7).Text = "FALTÓ"
                    e.Row.Cells(7).ForeColor = Drawing.Color.Red
                    e.Row.Cells(8).Text = "---"
                    NumFalt = NumFalt + 1
                ElseIf fila.Row("tipoasistencia_act").ToString.ToUpper = "A" Then
                    e.Row.Cells(7).Text = "ASISTIÓ"
                    e.Row.Cells(7).ForeColor = Drawing.Color.Blue
                    NumAsis = NumAsis + 1
                Else
                    e.Row.Cells(7).Text = "TARDANTE"
                    e.Row.Cells(7).ForeColor = Drawing.Color.Orange
                    NumTard = NumTard + 1
                End If
            Else
                e.Row.Cells(5).Text = "NO"
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
                e.Row.Cells(7).Text = "-"
                If fila.Row("CalifNum_dact") Is DBNull.Value Then
                    e.Row.Cells(8).Text = "-"
                Else
                    e.Row.Cells(8).Text = fila.Row("CalifNum_dact").ToString
                End If
            End If
        End If
        Me.LblAsistencia.Text = NumAsis
        Me.LblTardanzas.Text = NumTard
        Me.LblFaltas.Text = NumFalt

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        Dim Codigo_cup As Integer
        Dim tblCurso As Data.DataTable
        Dim con As Integer = 0
        If Not IsPostBack Then
            Try
                Codigo_cup = Request.QueryString("codigo_cup")
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                tblCurso = obj.TraerDataTable("ConsultarCursoProgramado", 10, Codigo_cup, 0, 0, 0)
                Me.codigo_syl.Value = obj.TraerDataTable("MED_ConsultarSylabus", "SI", Request.QueryString("codigo_cup")).Rows(0).Item("codigo_syl").ToString
                obj.CerrarConexion()

                If tblCurso.Rows.Count > 0 Then
                    Me.lblcurso.Text = tblCurso.Rows(0).Item("nombre_cur") & " - Grupo (" & tblCurso.Rows(0).Item("grupohor_cup") & ")"
                    Me.lblInicio.Text = tblCurso.Rows(0).Item("fechainicio_cup")
                    Me.lblFin.Text = tblCurso.Rows(0).Item("fechafin_cup")
                End If
                Dim c As Control = LoadControl("../controles/CtrlFotoAlumno.ascx")
                CType(c, CtrlFotoAlumno).CodigoUniversitario = Request.QueryString("codu")
                CType(c, CtrlFotoAlumno).Nombre = Request.QueryString("nom")
                Me.TblCelFoto.Controls.Add(c)
                Me.GridView1.DataBind()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
