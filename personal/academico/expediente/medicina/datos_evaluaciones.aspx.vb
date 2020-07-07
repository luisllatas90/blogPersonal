
Partial Class medicina_datos_evaluaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjCiclos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.DDLCiclos, ObjCiclos.TraerDataTable("ConsultarMatricula", "29", Me.Request.QueryString("codigo_alu"), "", ""), "codigo_mat", "descripcion_cac", "-- Seleccione Ciclo Academico --")
            ClsFunciones.LlenarListas(Me.DDLCursos, ObjCiclos.TraerDataTable("ConsultarMatricula", "3", Me.DDLCiclos.SelectedValue, "", ""), "codigo_cup", "nombre_cur", "-- Seleccione Cursos --")
        End If
        
    End Sub

    Protected Sub DDLCiclos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCiclos.SelectedIndexChanged
        Dim ObjCursos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.DDLCursos, ObjCursos.TraerDataTable("ConsultarMatricula", "3", Me.DDLCiclos.SelectedValue, "", ""), "codigo_cup", "nombre_cur", "-- Seleccione Cursos --")
    End Sub

    Protected Sub DDLCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLCursos.SelectedIndexChanged
        Dim ObjEvalua As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Dim Tabla As New Data.DataTable
        Dim codigo_syl As Integer
        Dim DatSylabus As Data.DataTable
        Dim ValorPromedio As Double

        DatSylabus = ObjEvalua.TraerDataTable("MED_ConsultarSylabus", Me.DDLCursos.SelectedValue)
        If DatSylabus.Rows.Count <> 0 Then
            codigo_syl = DatSylabus.Rows(0).Item("codigo_syl")
            Tabla = ObjEvalua.TraerDataTable("MED_COnsultarActividades", 0, codigo_syl, "E")
            For i As Integer = 0 To Tabla.Rows.Count - 1

                Dim fila As New TableRow
                Dim celda As New TableCell
                Dim celda2 As New TableCell

                ValorPromedio = ObjEvalua.TraerValor("MED_COnsultarEvaluacionesNOtas", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"), 0)
                If ValorPromedio = -1 Then
                    'celda.Width = "500"
                    'celda2.HorizontalAlign = HorizontalAlign.Right
                    'celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                    celda2.Text = ""
                    'fila.Cells.Add(celda)
                    'fila.Cells.Add(celda2)
                Else
                    'celda.Width = "500"
                    celda2.HorizontalAlign = HorizontalAlign.Right
                    'celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                    celda2.Text = FormatNumber(ValorPromedio, 2, TriState.False)
                    'fila.Cells.Add(celda)
                    'fila.Cells.Add(celda2)
                End If

                celda.CssClass = "fila1"
                celda2.CssClass = "fila1"

                celda.Text = Tabla.Rows(i).Item("descripcion_act")
                'celda2.Text = ""

                fila.Cells.Add(celda)
                fila.Cells.Add(celda2)

                Me.TblNotas.Rows.Add(fila)
                MuestraNotas(Tabla.Rows(i).Item("codigo_act"), codigo_syl)
            Next
        End If
    End Sub


    Private Sub MuestraNotas(ByVal valorpadre As Integer, ByVal codigo_syl As Integer)
        Dim ObjEvalua As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tabla As New Data.DataTable
        Dim valor As Double
        Dim tds As String
        Tabla = ObjEvalua.TraerDataTable("MED_COnsultarActividades", valorpadre, codigo_syl, "E")

        For i As Integer = 0 To Tabla.Rows.Count - 1
            valor = ObjEvalua.TraerValor("MED_COnsultarEvaluacionesNOtas", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"), 0)
            Dim fila As New TableRow
            Dim celda As New TableCell
            Dim celda2 As New TableCell
            fila.Height = 15


            Select Case Tabla.Rows(i).Item("nivelnota__act")
                Case 0
                    tds = "&nbsp;&nbsp;&nbsp;"
                    fila.CssClass = "fila1"
                Case 1
                    tds = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    fila.CssClass = "fila2"
                Case 2
                    tds = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    fila.CssClass = "fila3"
                    celda.CssClass = "fila3"
                    celda2.CssClass = "fila3"
                Case 3
                    tds = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    fila.CssClass = "fila4"

                Case 4
                    tds = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                    fila.CssClass = "fila5"

            End Select

            If valor = -1 Then
                celda.Width = "500"
                celda2.HorizontalAlign = HorizontalAlign.Right
                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda2.Text = ""
                fila.Cells.Add(celda)
                fila.Cells.Add(celda2)
            Else
                celda.Width = "500"
                celda2.HorizontalAlign = HorizontalAlign.Right
                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda2.Text = FormatNumber(valor, 2, TriState.False)
                fila.Cells.Add(celda)
                fila.Cells.Add(celda2)
            End If
            Me.TblNotas.Rows.Add(fila)
            MuestraNotas(Tabla.Rows(i).Item("codigo_act"), codigo_syl)
        Next
        Tabla = Nothing

    End Sub
End Class

