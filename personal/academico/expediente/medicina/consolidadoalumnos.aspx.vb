
Partial Class medicina_consolidadoalumnos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.LinkRegresar.NavigateUrl = "sylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        ConsultaNOtas()
    End Sub

    Protected Sub ConsultaNOtas()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Alumnos As New Data.DataTable
        Dim Promedio As String

        Alumnos = Obj.TraerDataTable("MED_ConsultarAlumnosCurso", 0, Request.QueryString("codigo_cup"))
        Dim Ruta As New EncriptaCodigos.clsEncripta

        For i As Integer = 0 To Alumnos.Rows.Count - 1
            Dim Col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Fila As New TableRow
            Dim Imagen As New Image
            Dim Nombre As New Label

            Imagen.ImageUrl = "http://www.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & Alumnos.Rows(i).Item("codigoUniver_Alu").ToString.Trim)
            Imagen.Width = 80
            Imagen.BorderColor = Drawing.Color.Black
            Imagen.BorderWidth = 1

            Nombre.Text = "<br><font color='#1C3393'>" & Alumnos.Rows(i).Item("codigoUniver_Alu").ToString + "</font><br>" '+ Alumnos.Rows(i).Item("alumno").ToString
            Nombre.Font.Bold = True

            Col1.Controls.Add(Imagen)
            Col1.Controls.Add(Nombre)
            Col1.Width = New System.Web.UI.WebControls.Unit(32, UnitType.Percentage)
            Col2.Width = New System.Web.UI.WebControls.Unit(72, UnitType.Percentage)
            Col1.HorizontalAlign = HorizontalAlign.Center

            Dim Notas As New Table
            Dim ColumNota1 As New TableCell
            Dim ColumNota2 As New TableCell
            Dim columNota3 As New TableCell

            Dim FilaNota1 As New TableRow
            Dim FilaNota2 As New TableRow
            Dim FilaNOta3 As New TableRow

            ColumNota1.Text = "<br>" & Alumnos.Rows(i).Item("alumno").ToString
            ColumNota1.HorizontalAlign = HorizontalAlign.Center
            ColumNota1.Font.Bold = True
            ColumNota1.Font.Size = 10
            FilaNota1.Cells.Add(ColumNota1)


            Promedio = Alumnos.Rows(i).Item("califnum_dact").ToString
            If Promedio = "" Then
                ColumNota2.Text = "<br>PROMEDIO : --"
            ElseIf CDbl(Promedio) < 14 Then
                ColumNota2.Text = "<br>PROMEDIO : <b><font color='#FF0000'>" & FormatNumber(Promedio).ToString & "</font>"
            Else
                ColumNota2.Text = "<br>PROMEDIO : <b><font color='#0000FF'>" & FormatNumber(Promedio).ToString & "</font>"
            End If

            ColumNota2.Font.Size = 9
            FilaNota2.Cells.Add(ColumNota2)


            columNota3.Text = "<a href='notasalumno.aspx?codigo_alu=" & Alumnos.Rows(i).Item("codigo_alu").ToString & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "'><font color='#2E3A92'>Ver Detalle Notas</font></a>" & _
            "<br><br><a href='actividadesalumno.aspx?codigo_alu=" & Alumnos.Rows(i).Item("codigo_alu").ToString & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "'><font color='#2E3A92'>Ver Detalle Asistencias</font></a>"
            'columNota3
            columNota3.HorizontalAlign = HorizontalAlign.Right
            FilaNOta3.Cells.Add(columNota3)

            Notas.Rows.Add(FilaNota1)
            Notas.Rows.Add(FilaNota2)
            Notas.Rows.Add(FilaNOta3)

            Col2.VerticalAlign = VerticalAlign.Top
            Col2.Controls.Add(Notas)
            Notas.Width = New System.Web.UI.WebControls.Unit(90, UnitType.Percentage)
            Fila.BackColor = Drawing.Color.WhiteSmoke
            Fila.Cells.Add(Col1)
            Fila.Cells.Add(Col2)
            Me.TblAlumnos.BorderColor = Drawing.Color.LightGray
            Me.TblAlumnos.BorderWidth = 1
            Me.TblAlumnos.CellSpacing = 10
            Me.TblAlumnos.Rows.Add(Fila)
        Next
        Alumnos = Nothing
        Ruta = Nothing

        
    End Sub

End Class
