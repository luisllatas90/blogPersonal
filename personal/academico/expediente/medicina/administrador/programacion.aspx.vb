Partial Class medicina_administrador_programacion
    Inherits System.Web.UI.Page
    Private codigo_sem As String

    Private Sub CargarMenu(ByVal GridTabla As Table, ByVal codigo_act As Integer, ByVal padreanterior As Integer)
        Dim DatosTabla As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        DatosTabla = ObjDatos.TraerDataTable("MED_COnsultarActividades", codigo_act, Me.HddCodSyl.Value, "A")

        Dim Colum1 As New TableCell
        Dim Colum2 As New TableCell
        Dim Fila1 As New TableRow
        Dim GridTabla2 As New Table
        Dim EstadoHijos As Boolean
        EstadoHijos = False
        For i As Int32 = 0 To DatosTabla.Rows.Count - 1
            Dim col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Fila2 As New TableRow

            col1.Text = ""
            col1.Width = 20
            Col2.Text = DatosTabla.Rows(i).Item("descripcion_act")
            Fila2.ID = padreanterior.ToString & "_" & i.ToString
            If DatosTabla.Rows(i).Item("Hijos") > 0 Then

                col1.Style.Add("border-top", "black 1px solid")
                col1.Style.Add("border-bottom", "black 1px solid")

                Col2.Style.Add("border-top", "black 1px solid")
                Col2.Style.Add("border-bottom", "black 1px solid")

                EstadoHijos = True
                col1.Text = "<img src='../../../../../images/mas.gif'>"
                Fila2.Font.Bold = True
                Fila2.BackColor = Drawing.Color.FromArgb(248, 241, 129)
                Fila2.Attributes.Add("OnClick", "cambiarDisplay('fila_" & padreanterior.ToString & "_" & DatosTabla.Rows(i).Item("codigo_act").ToString & "',this)")
                Col2.ColumnSpan = 3
            Else
                Dim Col3 As New TableCell
                Dim Col4 As New TableCell

                Col3.Text = DatosTabla.Rows(i).Item("Asistencia").ToString
                Col4.Text = CDate(DatosTabla.Rows(i).Item("fechaini_act")).ToShortDateString
                Col3.HorizontalAlign = HorizontalAlign.Center
                Col4.HorizontalAlign = HorizontalAlign.Center
                Fila2.Cells.Add(Col3)
                Fila2.Cells.Add(Col4)
                Fila2.Style.Add("cursor", "hand")
                Fila2.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                Fila2.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                Fila2.Attributes.Add("OnClick", "javascript:location.href='Asistencias.aspx?est=S&codigo_Act=" & DatosTabla.Rows(i).Item("codigo_Act") & "&" & Page.ClientQueryString & "'")
            End If
            Fila2.Cells.AddAt(0, col1)
            Fila2.Cells.AddAt(1, Col2)
            Fila2.Height = 20
            GridTabla2.Rows.Add(Fila2)
            CargarMenu(GridTabla2, DatosTabla.Rows(i).Item("codigo_act"), padreanterior)
            GridTabla2.CellPadding = 1
            GridTabla2.CellSpacing = 0
            GridTabla2.Width = System.Web.UI.WebControls.Unit.Percentage(100)
            Colum2.Controls.Add(GridTabla2)
            Colum2.ColumnSpan = 3
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
        datos = ObjDatos.TraerDataTable("MED_COnsultarActividades", 0, Me.HddCodSyl.Value, "A")
        For i As Int32 = 0 To datos.Rows.Count - 1
            Dim Col1 As New TableCell
            Dim Col2 As New TableCell
            Dim Fila1 As New TableRow
            Col1.Text = ""
            Col1.Width = 10
            Col2.Text = datos.Rows(i).Item("descripcion_act")
            Fila1.ID = i

            If datos.Rows(i).Item("hijos") > 0 Then
                Col1.Text = "<img src='../../../../../images/mas.gif'>"
                Col1.Style.Add("border-top", "black 1px solid")
                Col1.Style.Add("border-bottom", "black 1px solid")

                Col2.Style.Add("border-top", "black 1px solid")
                Col2.Style.Add("border-bottom", "black 1px solid")

                Col2.ColumnSpan = 3
                Col2.HorizontalAlign = HorizontalAlign.Left
                Fila1.Font.Bold = True
                Fila1.Attributes.Add("OnClick", "cambiarDisplay('fila_0_" & datos.Rows(i).Item("codigo_act").ToString & "',this)")
                Fila1.BackColor = Drawing.Color.FromArgb(248, 241, 129)
            Else
                Dim Col3 As New TableCell
                Dim Col4 As New TableCell

                Col3.Text = datos.Rows(i).Item("Asistencia").ToString
                Col4.Text = CDate(datos.Rows(i).Item("fechaini_act")).ToShortDateString
                Col3.HorizontalAlign = HorizontalAlign.Center
                Col4.HorizontalAlign = HorizontalAlign.Center
                Fila1.Cells.Add(Col3)
                Fila1.Cells.Add(Col4)

                Fila1.Style.Add("cursor", "hand")
                Fila1.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                Fila1.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                Fila1.Attributes.Add("OnClick", "javascript:location.href='Asistencias.aspx?est=S&codigo_Act=" & datos.Rows(i).Item("codigo_Act") & "&" & Page.ClientQueryString & "'")
            End If


            Fila1.Cells.AddAt(0, Col1)
            Fila1.Cells.AddAt(1, Col2)
            Fila1.Height = 20

            Me.TblActividades.Rows.Add(Fila1)
            CargarMenu(Me.TblActividades, datos.Rows(i).Item("codigo_act"), 0)
        Next
        datos.Dispose()
        datos = Nothing
        ObjDatos = Nothing
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Datos As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        codigo_sem = Request.QueryString("codigo_sem")

        Me.LinkRegresar.NavigateUrl = "administrar.aspx?codigo_sem=" & codigo_sem & "&codigo_per=" & Request.QueryString("codigo_per")

        Datos = ObjDatos.TraerDataTable("MED_ConsultarSylabus", "SI", Request.QueryString("codigo_cup"))
        If Datos.Rows.Count = 0 Then
            Me.LblMensaje.Text = "El Profesor Principal del Curso no ha registrado un Sylabus"
            Exit Sub
        End If
        Me.HddCodSyl.Value = Datos.Rows(0).Item("codigo_syl").ToString
        CargarTree()
    End Sub

End Class
