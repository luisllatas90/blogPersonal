
Partial Class medicina_datos_asistencias
    Inherits System.Web.UI.Page

    Dim faltas, asistencias, tardanzas As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjCiclos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.DDLCiclos, ObjCiclos.TraerDataTable("ConsultarMatricula", "29", Me.Request.QueryString("codigo_alu"), "", ""), "codigo_mat", "descripcion_cac", "-- Seleccione Ciclo Academico --")
            ClsFunciones.LlenarListas(Me.DDLCursos, ObjCiclos.TraerDataTable("ConsultarMatricula", "3", Me.DDLCiclos.SelectedValue, "", ""), "codigo_cup", "nombre_cur", "-- Seleccione Cursos --")
            Me.LblAsistencia.Text = 0
            Me.LblFaltas.Text = 0
            Me.LblTardanzas.Text = 0
        End If

    End Sub

    Protected Sub DDLCiclos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCiclos.SelectedIndexChanged
        Dim ObjCursos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.DDLCursos, ObjCursos.TraerDataTable("ConsultarMatricula", "3", Me.DDLCiclos.SelectedValue, "", ""), "codigo_cup", "nombre_cur", "-- Seleccione Cursos --")
        Me.LblAsistencia.Text = 0
        Me.LblFaltas.Text = 0
        Me.LblTardanzas.Text = 0
    End Sub

    Protected Sub DDLCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCursos.SelectedIndexChanged
        Dim ObjEvalua As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Dim Tabla As New Data.DataTable
        Dim tabla2 As Data.DataTable
        Dim codigo_syl As Integer
        Dim DatSylabus As Data.DataTable

        faltas = asistencias = tardanzas = 0

        DatSylabus = ObjEvalua.TraerDataTable("MED_ConsultarSylabus", Me.DDLCursos.SelectedValue)
        If DatSylabus.Rows.Count <> 0 Then
            codigo_syl = DatSylabus.Rows(0).Item("codigo_syl")

            Tabla = ObjEvalua.TraerDataTable("MED_COnsultarActividades", 0, codigo_syl, "A")

            For i As Integer = 0 To Tabla.Rows.Count - 1


                If Tabla.Rows(i).Item("hijos") > 0 Then
                    tabla2 = ObjEvalua.TraerDataTable("MED_COnsultarAsistencia", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"))

                    Dim fila As New TableRow
                    Dim celda As New TableCell
                    celda.Text = Tabla.Rows(i).Item("descripcion_act")
                    celda.CssClass = "columna1"
                    celda.ColumnSpan = 5

                    fila.Cells.Add(celda)

                    fila.Height = "13"
                    Me.TblNotas.Rows.Add(fila)

                    MuestraActividades(Tabla.Rows(i).Item("codigo_act"), codigo_syl, "")
                Else
                    tabla2 = ObjEvalua.TraerDataTable("MED_COnsultarAsistencia", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"))

                    Dim fila As New TableRow
                    Dim celda As New TableCell
                    Dim celda2 As New TableCell
                    Dim celda3 As New TableCell
                    Dim celda4 As New TableCell
                    Dim celda5 As New TableCell

                    celda.Text = Tabla.Rows(i).Item("descripcion_act")
                    celda.CssClass = "columna1"
                    celda2.Text = Tabla.Rows(i).Item("fechaini_act")
                    celda2.CssClass = "columna2"

                    If tabla2.Rows(0).Item("codigo_dact") = 0 Or tabla2.Rows(0).Item("horaingreso_dact") Is DBNull.Value Then
                        celda3.Text = " -- "
                        celda4.Text = "NO ASISTIO"
                        celda4.CssClass = "columna4"
                        celda4.ForeColor = Drawing.Color.Red
                        faltas += 1
                    ElseIf tabla2.Rows(0).Item("tipoasistencia_act") = "F" Then
                        celda3.Text = CDate(tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                        celda4.Text = "NO ASISTIO"
                        celda4.CssClass = "columna4"
                        celda4.ForeColor = Drawing.Color.Red
                        faltas += 1
                    ElseIf tabla2.Rows(0).Item("tipoasistencia_act") = "T" Then
                        celda3.Text = CDate(tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                        celda4.Text = "TARDANTE"
                        celda4.CssClass = "columna4"
                        celda4.ForeColor = Drawing.Color.Goldenrod
                        tardanzas += 1
                    ElseIf tabla2.Rows(0).Item("tipoasistencia_act") = "A" Then
                        celda3.Text = CDate(tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                        celda4.Text = "ASISTIO"
                        celda4.CssClass = "columna4"
                        celda4.ForeColor = Drawing.Color.Green
                        asistencias += 1
                    End If
                    celda3.CssClass = "columna3"

                    celda5.Text = tabla2.Rows(0).Item("observacion_dact").ToString
                    celda5.CssClass = "columna5"

                    fila.Cells.Add(celda)
                    fila.Cells.Add(celda2)
                    fila.Cells.Add(celda3)
                    fila.Cells.Add(celda4)
                    fila.Cells.Add(celda5)
                    fila.Height = "22"
                    Me.TblNotas.Rows.Add(fila)
                    MuestraActividades(Tabla.Rows(i).Item("codigo_act"), codigo_syl, "")

                End If
            Next
            Me.LblAsistencia.Text = asistencias.ToString
            Me.LblFaltas.Text = faltas.ToString
            Me.LblTardanzas.Text = tardanzas.ToString
        End If
    End Sub

    Private Sub MuestraActividades(ByVal valorpadre As Integer, ByVal codigo_syl As Integer, ByVal tds As String)
        Dim ObjEvalua As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tabla As New Data.DataTable
        Dim Tabla2 As New Data.DataTable

        tds = tds & "&nbsp;&nbsp;&nbsp;"

        Tabla = ObjEvalua.TraerDataTable("MED_COnsultarActividades", valorpadre, codigo_syl, "A")

        For i As Integer = 0 To Tabla.Rows.Count - 1
            If Tabla.Rows(i).Item("hijos") > 0 Then
                Tabla2 = ObjEvalua.TraerDataTable("MED_COnsultarAsistencia", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"))

                Dim fila As New TableRow
                Dim celda As New TableCell
                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda.CssClass = "columna1"
                celda.ColumnSpan = 5
              
                fila.Cells.Add(celda)
          
                fila.Height = "13"
                Me.TblNotas.Rows.Add(fila)

                MuestraActividades(Tabla.Rows(i).Item("codigo_act"), codigo_syl, tds)
            Else
                Tabla2 = ObjEvalua.TraerDataTable("MED_COnsultarAsistencia", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"))
                Dim fila As New TableRow
                Dim celda As New TableCell
                Dim celda2 As New TableCell
                Dim celda3 As New TableCell
                Dim celda4 As New TableCell
                Dim celda5 As New TableCell

                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda.CssClass = "columna1"
                celda2.Text = Tabla.Rows(i).Item("fechaini_act")
                celda2.CssClass = "columna2"

                If Tabla2.Rows(0).Item("codigo_dact") = 0 Then
                    celda3.Text = " -- "
                    celda4.Text = "NO ASISTIO"
                    celda4.CssClass = "columna4"
                    celda4.ForeColor = Drawing.Color.Red
                    faltas += 1
                ElseIf Tabla2.Rows(0).Item("tipoasistencia_act") = "F" Then
                    celda3.Text = CDate(Tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                    celda4.Text = "NO ASISTIO"
                    celda4.CssClass = "columna4"
                    celda4.ForeColor = Drawing.Color.Red
                    faltas += 1
                ElseIf Tabla2.Rows(0).Item("tipoasistencia_act") = "T" Then
                    celda3.Text = CDate(Tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                    celda4.Text = "TARDANTE"
                    celda4.CssClass = "columna4"
                    celda4.ForeColor = Drawing.Color.Goldenrod
                    tardanzas += 1
                ElseIf Tabla2.Rows(0).Item("tipoasistencia_act") = "A" Then
                    celda3.Text = CDate(Tabla2.Rows(0).Item("horaingreso_dact")).ToShortTimeString
                    celda4.Text = "ASISTIO"
                    celda4.CssClass = "columna4"
                    celda4.ForeColor = Drawing.Color.Green
                    asistencias += 1
                End If
                celda3.CssClass = "columna3"

                celda5.Text = Tabla2.Rows(0).Item("observacion_dact").ToString
                celda5.CssClass = "columna5"

                fila.Cells.Add(celda)
                fila.Cells.Add(celda2)
                fila.Cells.Add(celda3)
                fila.Cells.Add(celda4)
                fila.Cells.Add(celda5)
                fila.Height = "22"
                Me.TblNotas.Rows.Add(fila)

                MuestraActividades(Tabla.Rows(i).Item("codigo_act"), codigo_syl, tds)
            End If
        Next

        Tabla = Nothing

    End Sub


End Class
