'Modificado por hreyes: 30/09/09
'@sistema = Campus Virtual
'@modulo = Registro de notas y asistencias
'@funcion = Reporte de asistencias

Partial Class medicina_administrador_reporteasistencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        Dim Codigo_cup As Integer
        Dim Codigo_syl as integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        codigo_syl = Obj.TraerDataTable("MED_ConsultarSylabus", "SI", Request.QueryString("codigo_cup")).Rows(0).Item("codigo_syl").ToString
        obj.CerrarConexion()
        Codigo_cup = Request.QueryString("codigo_cup")

        'If Not IsPostBack Then
        Dim tblCurso As Data.DataTable
        Dim Alumnos As New Data.DataTable
        Dim Asistencia As New Data.DataTable
        Dim Actividades As New Data.DataTable
        Dim Codigo_alu As Integer
        Dim Contador As Integer

        obj.AbrirConexion()
        tblCurso = obj.TraerDataTable("ConsultarCursoProgramado", 10, Codigo_cup, 0, 0, 0)
        obj.CerrarConexion()
        If tblCurso.Rows.Count > 0 Then
            Me.lblcurso.Text = tblCurso.Rows(0).Item("nombre_cur") & " - Grupo (" & tblCurso.Rows(0).Item("grupohor_cup") & ")"
            Me.lblInicio.Text = tblCurso.Rows(0).Item("fechainicio_cup")
            Me.lblFin.Text = tblCurso.Rows(0).Item("fechafin_cup")
        End If

        Codigo_alu = 0
        Contador = 1

        obj.AbrirConexion()
        Alumnos = obj.TraerDataTable("MED_ConsultarAlumnosCurso", 0, Codigo_cup)
        Actividades = obj.TraerDataTable("MED_COnsultarActividades", 1, Codigo_syl, "S")
        obj.CerrarConexion()

        If Alumnos.Rows.Count > 0 Then
            Dim C1 As New TableCell
            Dim C2 As New TableCell
            Dim C3 As New TableCell
            Dim F As New TableRow
            Dim Con As Integer = 0

            C1.BackColor = Drawing.Color.Beige
            C2.BackColor = Drawing.Color.Beige
            C3.BackColor = Drawing.Color.Beige

            C1.ForeColor = Drawing.Color.Black
            C2.ForeColor = Drawing.Color.Black
            C3.ForeColor = Drawing.Color.Black

            C1.HorizontalAlign = HorizontalAlign.Center
            C2.HorizontalAlign = HorizontalAlign.Center
            C3.HorizontalAlign = HorizontalAlign.Center

            C1.Text = "N°"
            C2.Text = "Cod. Univ."
            C3.Text = "Apellidos y Nombres"

            F.Cells.Add(C1)
            F.Cells.Add(C2)
            F.Cells.Add(C3)

            For j As Integer = Actividades.Rows.Count - 1 To 0 Step -1
                Dim c As New TableCell
                Dim c_ley01 As New TableCell
                Dim c_ley02 As New TableCell
                Dim c_ley03 As New TableCell
                Dim c_ley04 As New TableCell

                Dim FLeyenda As New TableRow

                Con = Con + 1
                c.BackColor = Drawing.Color.Beige
                c.ForeColor = Drawing.Color.Black
                c.Text = "A" & Con.ToString & "<br>" & CDate(Actividades.Rows(j).Item("FechaIni_act")).Day & "-" & MonthName(CDate(Actividades.Rows(j).Item("FechaIni_act")).Month, True)
                c.Width = 45
                c.HorizontalAlign = HorizontalAlign.Center
                F.Cells.Add(c)

                c_ley01.Text = "A" & Con.ToString
                c_ley01.HorizontalAlign = HorizontalAlign.Center
                c_ley02.Text = Actividades.Rows(j).Item("FechaIni_act")
                c_ley03.Text = Actividades.Rows(j).Item("descripcion_Act")

                FLeyenda.Cells.Add(c_ley01)
                FLeyenda.Cells.Add(c_ley02)
                FLeyenda.Cells.Add(c_ley03)
                Me.TblLeyenda.Rows.Add(FLeyenda)

            Next
            Me.TblAsistencia.Rows.Add(F)

            For i As Int16 = 0 To Alumnos.Rows.Count - 1
                Dim Col1 As New TableCell
                Dim Col2 As New TableCell
                Dim Col3 As New TableCell
                Dim Fila As New TableRow

                If Codigo_alu <> CInt(Alumnos.Rows(i).Item("codigo_Alu").ToString) Then
                    Codigo_alu = CInt(Alumnos.Rows(i).Item("codigo_Alu").ToString)

                    Col1.Text = Contador
                    Col1.HorizontalAlign = HorizontalAlign.Center
                    Col2.Text = "&nbsp;" & Alumnos.Rows(i).Item("codigoUniver_Alu").ToString
                    Col3.Text = Alumnos.Rows(i).Item("alumno").ToString

                    Fila.Cells.Add(Col1)
                    Fila.Cells.Add(Col2)
                    Fila.Cells.Add(Col3)
                    Me.TblAsistencia.Rows.Add(Fila)
                    obj.AbrirConexion()
                    Asistencia = obj.TraerDataTable("MED_ConsultarAsistenciaAlumno", "AS", codigo_syl, Alumnos.Rows(i).Item("codigo_dma"))
                    obj.CerrarConexion()
                    For j As Integer = Actividades.Rows.Count - 1 To 0 Step -1
                        Dim CelAsist As New TableCell
                        CelAsist.Text = "F"
                        CelAsist.ForeColor = Drawing.Color.Red
                        For k As Integer = 0 To Asistencia.Rows.Count - 1
                            If Actividades.Rows(j).Item("codigo_act").ToString = Asistencia.Rows(k).Item("codigo_act").ToString Then
                                CelAsist.Text = Asistencia.Rows(k).Item("tipoasistencia_act").ToString
                                CelAsist.ForeColor = Drawing.Color.Black
                                Exit For
                            End If
                        Next
                        CelAsist.HorizontalAlign = HorizontalAlign.Center
                        Fila.Cells.Add(CelAsist)
                    Next
                    Fila.Height = 20
                    Me.TblAsistencia.Rows.Add(Fila)
                    Contador = Contador + 1
                End If
            Next
        End If
        'End If

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

        Me.TblAsistencia.EnableViewState = False
        page1.EnableViewState = False

        page1.Controls.Add(form1)
        form1.Controls.Add(Me.lblcurso)
        form1.Controls.Add(Me.TblAsistencia)
        form1.Controls.Add(lbl)
        form1.Controls.Add(Me.TblLeyenda)

        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='2' color='#121212'>Reporte de Asistencias - USAT<br>Actualizado al :" & Date.Now() & "</font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function




End Class
