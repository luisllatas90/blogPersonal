
Partial Class medicina_listaevaluaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.LinkRegresar.NavigateUrl = "sylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")

        If IsPostBack = False Then
            CargarTree()
        End If

    End Sub

    Private Sub CargarMenu(ByVal GridTabla As Table, ByVal codigo_act As Integer, ByVal padreanterior As Integer)
        ' El Valor "padreanterior" me va a sevir para asignarle el codigo correspondiente a la fila
        Dim DatosTabla As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        DatosTabla = ObjDatos.TraerDataTable("MED_COnsultarActividades", codigo_act, Request.QueryString("codigo_syl"), "E")

        Dim Colum1 As New TableCell
        Dim Colum2 As New TableCell
        Dim Fila1 As New TableRow
        Dim GridTabla2 As New Table     'Tabla Maestro Anterior
        Dim EstadoHijos As Boolean      'Variable que me verifica si la tabla ya no tiene mas subItems
        EstadoHijos = False

        For i As Int32 = 0 To DatosTabla.Rows.Count - 1
            Dim col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Fila2 As New TableRow

            col1.Text = ""
            col1.Width = 20
            Col2.Text = DatosTabla.Rows(i).Item("descripcion_act")

            Fila2.ID = padreanterior.ToString & "_" & i.ToString   'En esta seccion indico el nombre de la fila, es crucial
            'para identificar la fila que utilizo y me muestre el desplegable 

            If DatosTabla.Rows(i).Item("Hijos") > 0 Then
                EstadoHijos = True                                      'Indico que la tabla si tiene subitems.
                col1.Text = "<img src='../../../../images/mas.gif'>"
                Fila2.Attributes.Add("OnClick", "cambiarDisplay('fila_" & padreanterior.ToString & "_" & DatosTabla.Rows(i).Item("codigo_act").ToString & "',this)")
            Else
                Dim Col3 As New TableCell
                Dim Col4 As New TableCell

                Col3.Text = DatosTabla.Rows(i).Item("EstadoNota").ToString
                Col4.Text = CDate(DatosTabla.Rows(i).Item("fechaini_act")).ToShortDateString
                Col3.HorizontalAlign = HorizontalAlign.Center
                Col4.HorizontalAlign = HorizontalAlign.Center

                Fila2.Cells.Add(Col3)
                Fila2.Cells.Add(Col4)

                'Como la fila no tiene hijos entonces agrego la referencia o link que va a llamr el registro.
                Fila2.Style.Add("cursor", "hand")
                Fila2.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                Fila2.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                Fila2.Attributes.Add("OnClick", "javascript:location.href='registranotas.aspx?est=S&codigo_Act=" & DatosTabla.Rows(i).Item("codigo_Act") & "&" & Page.ClientQueryString & "'")

                ' Esta seccion me sirve para colocar la cabecera, tenga en cuenta
                ' que la cabecera la agrego al final, despues de habera gregado todas la filas 
                ' que contiene los datos respectivos y que ademas no tiene hijos. 
                If EstadoHijos = False And i = DatosTabla.Rows.Count - 1 Then
                    Dim CabeceraCol1 As New TableCell
                    Dim CabeceraCol2 As New TableCell
                    Dim CabeceraCol3 As New TableCell
                    Dim CabeceraCol4 As New TableCell

                    Dim Fila3 As New TableRow
                    CabeceraCol1.Text = ""
                    CabeceraCol2.Text = "Evaluacion"
                    CabeceraCol3.Text = "Estado Notas"
                    CabeceraCol4.Text = "Fecha Evaluación"

                    CabeceraCol1.HorizontalAlign = HorizontalAlign.Center
                    CabeceraCol2.HorizontalAlign = HorizontalAlign.Center
                    CabeceraCol3.HorizontalAlign = HorizontalAlign.Center
                    CabeceraCol4.HorizontalAlign = HorizontalAlign.Center

                    CabeceraCol1.Width = 20
                    CabeceraCol2.Width = System.Web.UI.WebControls.Unit.Percentage(50)
                    CabeceraCol3.Width = System.Web.UI.WebControls.Unit.Percentage(25)
                    CabeceraCol4.Width = System.Web.UI.WebControls.Unit.Percentage(25)

                    Fila3.Cells.Add(CabeceraCol1)
                    Fila3.Cells.Add(CabeceraCol2)
                    Fila3.Cells.Add(CabeceraCol3)
                    Fila3.Cells.Add(CabeceraCol4)
                    Fila3.BackColor = Drawing.Color.FromArgb(248, 241, 129)
                    Fila3.Font.Bold = True
                    Fila3.Height = 20
                    GridTabla2.Rows.AddAt(0, Fila3)   'Agrego la cabecera en la posicion cero(0)
                    GridTabla2.GridLines = GridLines.Horizontal
                End If
            End If
            Fila2.Cells.AddAt(0, col1)  ' Las columnsa tb las agrego en la psicion cero(0) y uno(1)
            Fila2.Cells.AddAt(1, Col2)
            Fila2.Height = 20

            GridTabla2.Rows.Add(Fila2)

            CargarMenu(GridTabla2, DatosTabla.Rows(i).Item("codigo_act"), padreanterior)
            GridTabla2.CellPadding = 1
            GridTabla2.CellSpacing = 0
            GridTabla2.Width = System.Web.UI.WebControls.Unit.Percentage(100)
            Colum2.Controls.Add(GridTabla2)
        Next
        Colum1.Text = ""
        Colum1.Width = 10
        If DatosTabla.Rows.Count > 0 Then
            Fila1.Cells.Add(Colum1)
            Fila1.Cells.Add(Colum2)
            Fila1.Style.Add("display", "none")
            Fila1.ID = "fila_" & padreanterior.ToString & "_" & codigo_act.ToString
            GridTabla.Rows.Add(Fila1)
        End If

        DatosTabla.Dispose()
        DatosTabla = Nothing
        ObjDatos = Nothing
    End Sub

    Private Sub CargarTree()
        Me.TblActividades.Rows.Clear()
        Dim datos As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        datos = ObjDatos.TraerDataTable("MED_COnsultarActividades", 0, Request.QueryString("codigo_syl"), "E")
        For i As Int32 = 0 To datos.Rows.Count - 1
            Dim Col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Fila1 As New TableRow
            Col1.Text = ""
            Col1.Width = 10
            Col2.Text = datos.Rows(i).Item("descripcion_act")

            Fila1.ID = i

            If datos.Rows(i).Item("hijos") > 0 Then
                Col1.Text = "<img src='../../../../images/mas.gif'>"
                Fila1.Attributes.Add("OnClick", "cambiarDisplay('fila_0_" & datos.Rows(i).Item("codigo_act").ToString & "',this)")
            Else
                Fila1.Style.Add("cursor", "hand")
                Fila1.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                Fila1.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                Fila1.Attributes.Add("OnClick", "javascript:location.href='registranotas.aspx?est=S&codigo_Act=" & datos.Rows(i).Item("codigo_Act") & "&" & Page.ClientQueryString & "'")
            End If
            Fila1.Cells.Add(Col1)
            Fila1.Cells.Add(Col2)
            Me.TblActividades.Rows.Add(Fila1)
            CargarMenu(Me.TblActividades, datos.Rows(i).Item("codigo_act"), 0)
        Next
        datos.Dispose()
        datos = Nothing
        ObjDatos = Nothing
    End Sub


End Class
