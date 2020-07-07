
Partial Class medicina_administrador_ReportedeNotas
    Inherits System.Web.UI.Page
    Dim codigo_sem As String
    Dim notaAprob As Int32 = 14

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Form.Attributes.Add("OnSubmit", "return confirm('¿Desea Guardar los cambios realizados?')")
            'Me.cmdGuardar.Attributes.Add("disabled", "disabled")
            codigo_sem = Request.QueryString("codigo_sem")
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.LblEvaluacion.Text = ObjDatos.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_Act")).Rows(0).Item("Evaluacion").ToString()
            ObjDatos = Nothing
        End If
        ConsultaNOtas()
    End Sub

    Protected Sub ConsultaNOtas()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Alumnos As New Data.DataTable
        Dim Evaluaciones As New Data.DataTable

        Alumnos = Obj.TraerDataTable("MED_ConsultarAlumnosCurso", Request.QueryString("codigo_act"), Request.QueryString("codigo_cup"))
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

            Nombre.Text = "<br><font color='#1C3393'>" & Alumnos.Rows(i).Item("codigoUniver_Alu").ToString + "</font><br>" + Alumnos.Rows(i).Item("alumno").ToString
            Nombre.Font.Bold = True

            Col1.Controls.Add(Imagen)
            Col1.Controls.Add(Nombre)
            Col1.Width = New System.Web.UI.WebControls.Unit(32, UnitType.Percentage)
            Col2.Width = New System.Web.UI.WebControls.Unit(72, UnitType.Percentage)
            Col1.HorizontalAlign = HorizontalAlign.Center

            Dim Notas As New Table
            Evaluaciones = Obj.TraerDataTable("MEd_ConsultarEvaluacionesIngNotas", Request.QueryString("codigo_Act"), Alumnos.Rows(i).Item("codigo_dma"), Request.QueryString("est"))

            For j As Integer = 0 To Evaluaciones.Rows.Count - 1
                Dim ColumNota1 As New TableCell
                Dim ColumNota2 As New TableCell
                Dim ColumNota3 As New TableCell
                Dim FilaNota1 As New TableRow
                Dim CajaTexto As New TextBox
                Dim Estado As New Label
                'Dim Validador As New CompareValidator
                'Validador.ErrorMessage = "Nota debe estar entre 0 y 20"
                'Validador.Operator = ValidationCompareOperator.LessThanEqual
                'Validador.Type = ValidationDataType.Double
                'Validador.ValueToCompare = 20
                'Validador.Text = "*"
                'Validador.SetFocusOnError = True

                'Dim Requerido As New RequiredFieldValidator
                'Requerido.ErrorMessage = "Debe ingresar una nota en todas las casillas"
                'Requerido.Text = "*"
                'Requerido.SetFocusOnError = True

                CajaTexto.Width = 50
                CajaTexto.Attributes.Add("disabled", "disabled")
                CajaTexto.Style.Add("text-align", "center")
                'Estado.Style.Add("text-align", "center")
                CajaTexto.MaxLength = 5
                CajaTexto.ID = "Txt_" & Alumnos.Rows(i).Item("codigo_dma").ToString & "_" & Evaluaciones.Rows(j).Item("codigo_act").ToString
                CajaTexto.Attributes.Add("OnChange", "activaguardar(this)")
                CajaTexto.Attributes.Add("OnKeyPress", "numeros();")
                If Evaluaciones.Rows(j).Item("califnum_dact") Is System.DBNull.Value Then
                    CajaTexto.Text = ""
                    CajaTexto.ToolTip = ""
                    CajaTexto.Attributes.Add("Tag", "0")
                Else
                    CajaTexto.Text = FormatNumber(Evaluaciones.Rows(j).Item("califnum_dact").ToString, 2, TriState.False)
                    CajaTexto.ToolTip = FormatNumber(Evaluaciones.Rows(j).Item("califnum_dact").ToString, 2, TriState.False)
                    CajaTexto.Attributes.Add("Tag", FormatNumber(Evaluaciones.Rows(j).Item("califnum_dact").ToString, 2, TriState.False))
                End If

                'Validador.ControlToValidate = CajaTexto.ID.ToString
                'Requerido.ControlToValidate = CajaTexto.ID.ToString

                ColumNota1.Text = Evaluaciones.Rows(j).Item("descripcion_act").ToString
                If CInt(CajaTexto.Attributes("Tag")) <> 0 Then
                    If CInt(CajaTexto.Attributes("Tag")) >= notaAprob Then
                        Estado.Text = "Aprobado"
                        Estado.ForeColor = Drawing.Color.Blue
                    Else
                        Estado.Text = "Desaprobado"
                        Estado.ForeColor = Drawing.Color.Red
                    End If
                End If
                ColumNota3.Controls.Add(Estado)
                ColumNota2.Controls.Add(CajaTexto)
                'ColumNota2.Controls.Add(Validador)
                'ColumNota2.Controls.Add(Requerido)
                ColumNota3.Width = New System.Web.UI.WebControls.Unit(30, UnitType.Percentage)
                ColumNota2.Width = New System.Web.UI.WebControls.Unit(10, UnitType.Percentage)

                FilaNota1.Cells.Add(ColumNota1)
                FilaNota1.Cells.Add(ColumNota3)
                FilaNota1.Cells.Add(ColumNota2)
                Notas.Rows.Add(FilaNota1)

            Next
            Evaluaciones = Nothing
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

        Me.LinkRegresar.NavigateUrl = "EvaluacionesRegistradas.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_sem=" & codigo_sem & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
    End Sub


End Class
