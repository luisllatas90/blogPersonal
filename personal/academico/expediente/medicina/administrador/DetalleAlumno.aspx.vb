
Partial Class medicina_administrador_DetalleAlumno
    Inherits System.Web.UI.Page
    Dim cod_alu As Int16
    Dim CodUniv As String
    Private NumAsis As Integer = 0
    Private NumFalt As Integer = 0
    Private NumTard As Integer = 0

    Protected Sub gvConsolidado_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvConsolidado.DataBinding
       
    End Sub


    Protected Sub gvConsolidado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvConsolidado.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            Dim consideraasistencia As Int16
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            e.Row.Cells(5).Attributes.Add("tooltip", "<table><tr><td width='300'><b>TEMAS:" & "</b><br>" & fila.Row("temas_act").ToString & "</td></tr></table>")
            e.Row.Cells(5).Attributes.Add("style", "cursor:hand")

            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            '** Notas ***
            If fila.Row("considerarnota_act").ToString.ToUpper = "S" Then
                e.Row.Cells(7).Text = "SI"
            Else
                e.Row.Cells(7).Text = "NO"
                e.Row.Cells(10).Text = "<img src='../../images/bloquear.gif' alt='La actividad no considera Ingreso de Notas'>"
            End If

            '*** Asistencia ***
            consideraasistencia = IIf(fila.Row("ConsiderarAsistencia_act") Is DBNull.Value, 0, IIf(fila.Row("ConsiderarAsistencia_act") = True, 1, 0))

            If consideraasistencia = 1 Then
                e.Row.Cells(6).Text = "SI"
                e.Row.Cells(6).ForeColor = Drawing.Color.Black

                If fila.Row("CalifNum_dact") Is DBNull.Value Or fila.Row("CalifNum_dact").ToString.Trim = "" Then
                    e.Row.Cells(9).Text = "-"
                Else
                    e.Row.Cells(9).Text = fila.Row("CalifNum_dact")
                End If

                If fila.Row("tipoasistencia_act") Is System.DBNull.Value Then
                    e.Row.Cells(8).Text = "FALTÓ"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Red
                    e.Row.Cells(9).Text = "---"
                    NumFalt = NumFalt + 1
                ElseIf fila.Row("tipoasistencia_act").ToString.ToUpper = "A" Then
                    e.Row.Cells(8).Text = "ASISTIÓ"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Blue
                    NumAsis = NumAsis + 1
                Else
                    e.Row.Cells(8).Text = "TARDANTE"
                    e.Row.Cells(8).ForeColor = Drawing.Color.Orange
                    NumTard = NumTard + 1
                End If
            Else
                e.Row.Cells(6).Text = "NO"
                e.Row.Cells(6).ForeColor = Drawing.Color.Red
                e.Row.Cells(8).Text = "-"
                If fila.Row("CalifNum_dact") Is DBNull.Value Then
                    e.Row.Cells(9).Text = "-"
                Else
                    e.Row.Cells(9).Text = fila.Row("CalifNum_dact").ToString
                End If
            End If

        End If
        Me.LblAsistencia.Text = NumAsis
        Me.LblTardanzas.Text = NumTard
        Me.LblFaltas.Text = NumFalt

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim c As Control = LoadControl("../controles/CtrlFotoAlumno.ascx")
            CType(c, CtrlFotoAlumno).CodigoUniversitario = Request.QueryString("codu")
            CType(c, CtrlFotoAlumno).Nombre = Request.QueryString("nom")
            Me.TblCelFoto.Controls.Add(c)
            Me.lblAlumno.Text = Request.QueryString("nom")
            Dim objAlu As New ClsConectarDatos
            Dim datosAlu As New Data.DataTable
            objAlu.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objAlu.AbrirConexion()
            Me.lblAlumno.Text = "<b>ALUMNO: </b>" & objAlu.TraerDataTable("consultarAlumno", "COD", Request.QueryString("codu")).Rows(0).Item("nombres")
            Me.lblCicloAcad.Text = "<b>| CICLO ACADÉMICO: </b>" & objAlu.TraerDataTable("ConsultarCicloAcademico", "CO", Request.QueryString("cac")).Rows(0).Item("descripcion_cac")
            objAlu.CerrarConexion()

        End If
    End Sub


    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
      
        Response.Write(ClsFunciones.HTML(Me.gvConsolidado, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReportePresupuesto", gvConsolidado, "Reporte de Asistencia y notas " & Me.lblCicloAcad.Text, "Sistema de Notas y Asistencias - Campus Virtual USAT")
    End Sub
End Class
