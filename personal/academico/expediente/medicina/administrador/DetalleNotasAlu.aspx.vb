
Partial Class medicina_DetalleNotasAlu
    Inherits System.Web.UI.Page
    Public ValorPromedio As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.LinkRegresar.NavigateUrl = "consolidadoalumnos.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")

        If IsPostBack = False Then
            Dim ObjEvalua As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Tabla As New Data.DataTable
            Dim codigo_syl As Integer
            Dim DatSylabus As Data.DataTable
            Dim DatosAlumno As Data.DataTable
            Dim ObjFoto As New EncriptaCodigos.clsEncripta

            DatosAlumno = ObjEvalua.TraerDataTable("ConsultarAlumno", "CO", Request.QueryString("codigo_alu"))
            ImgFoto.ImageUrl = "http://www.usat.edu.pe/imgestudiantes/" & ObjFoto.CodificaWeb("069" & DatosAlumno.Rows(0).Item("codigouniver_alu").ToString)
            Me.LblCodigo.Text = DatosAlumno.Rows(0).Item("codigouniver_alu").ToString
            Me.LblNombre.Text = DatosAlumno.Rows(0).Item("apellidopat_alu").ToString & " " & DatosAlumno.Rows(0).Item("apellidomat_alu").ToString & " " & DatosAlumno.Rows(0).Item("nombres_alu").ToString
            Me.LblFecha.Text = Now.ToShortDateString & " - " & Now.ToLongTimeString

            DatosAlumno = Nothing
            Me.LinkRegresar.NavigateUrl = "BusquedaAlumno.aspx?codigo_per=" & Request.QueryString("codigo_per") & "&alumno=" & Me.LblCodigo.Text & "&codigo_sem=" & Request.QueryString("codigo_sem")

            DatSylabus = ObjEvalua.TraerDataTable("MED_ConsultarSylabus", "SI", Me.Request.QueryString("codigo_cup"))
            If DatSylabus.Rows.Count <> 0 Then
                codigo_syl = DatSylabus.Rows(0).Item("codigo_syl")
                Tabla = ObjEvalua.TraerDataTable("MED_COnsultarActividades", 0, codigo_syl, "E")
                For i As Integer = 0 To Tabla.Rows.Count - 1
                    Dim fila As New TableRow
                    Dim celda As New TableCell
                    Dim celda2 As New TableCell

                    ValorPromedio = ObjEvalua.TraerValor("MED_COnsultarEvaluacionesNOtas", Tabla.Rows(i).Item("codigo_act"), Request.QueryString("codigo_alu"), 0)
                    If ValorPromedio = -1 Then
                        celda2.Text = ""
                    Else
                        celda2.HorizontalAlign = HorizontalAlign.Right
                        celda2.Text = FormatNumber(ValorPromedio, 2, TriState.False)
                    End If

                    celda.CssClass = "fila1"
                    celda2.CssClass = "fila1"

                    celda.Text = Tabla.Rows(i).Item("descripcion_act")

                    fila.Cells.Add(celda)
                    fila.Cells.Add(celda2)

                    Me.TblNotas.Rows.Add(fila)
                    MuestraNotas(Tabla.Rows(i).Item("codigo_act"), codigo_syl)
                Next
            End If
            Dim Filaresumen2 As New TableRow
            Dim CeldaRes1 As New TableCell
            Dim CeldaRes2 As New TableCell
            CeldaRes1.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Promedio"
            If ValorPromedio = -1 Then
                CeldaRes2.Text = ""
            Else
                CeldaRes2.Text = FormatNumber(ValorPromedio, 2, TriState.False)
            End If

            CeldaRes1.CssClass = "fila1"
            CeldaRes2.CssClass = "fila1"
            CeldaRes2.HorizontalAlign = HorizontalAlign.Right
            Filaresumen2.Cells.Add(CeldaRes1)
            Filaresumen2.Cells.Add(CeldaRes2)
            Me.TblResumen.Rows.Add(Filaresumen2)
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

            fila.Height = 15

            If valor = -1 Then
                celda.Width = "500"
                celda2.HorizontalAlign = HorizontalAlign.Right
                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda2.Text = ""
            Else
                celda.Width = "500"
                celda2.HorizontalAlign = HorizontalAlign.Right
                celda.Text = tds & Tabla.Rows(i).Item("descripcion_act")
                celda2.Text = FormatNumber(valor, 2, TriState.False)

            End If

            If CInt(Tabla.Rows(i).Item("nivelnota__act")) = 1 Then
                Dim FilaResumen As New TableRow
                Dim CeldaResumen1 As New TableCell
                Dim CeldaResumen2 As New TableCell
                CeldaResumen1.Text = celda.Text
                CeldaResumen2.Text = celda2.Text
                CeldaResumen2.HorizontalAlign = HorizontalAlign.Right
                CeldaResumen1.CssClass = "fila2"
                CeldaResumen2.CssClass = "fila2"
                FilaResumen.Cells.Add(CeldaResumen1)
                FilaResumen.Cells.Add(CeldaResumen2)
                Me.TblResumen.Rows.Add(FilaResumen)
            End If

            fila.Cells.Add(celda)
            fila.Cells.Add(celda2)
            Me.TblNotas.Rows.Add(fila)

            MuestraNotas(Tabla.Rows(i).Item("codigo_act"), codigo_syl)
        Next

        Tabla = Nothing


    End Sub

End Class
