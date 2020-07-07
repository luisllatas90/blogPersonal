
Partial Class Consultas_ConsultarNuevosSistemas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.GvNewModulo.DataSource = Obj.TraerDataTable("paReq_ConsultarSolicitudPorTipo", 1)
            Me.GvNewModulo.DataBind()
            'Me.GvNewModulo.HeaderRow.Cells(0).Text = "Código"
            'Me.GvNewModulo.HeaderRow.Cells(1).Text = "Descripción"
            'Me.GvNewModulo.HeaderRow.Cells(2).Text = "Fecha"
            'Me.GvNewModulo.HeaderRow.Cells(4).Text = "Área Solicitante"
            'Me.GvNewModulo.HeaderRow.Cells(5).Text = "Solicitante"
            'Me.GvNewModulo.HeaderRow.Cells(3).Visible = False
        End If
    End Sub
    Private Sub Llenargrid(ByVal TablaDatos As Table)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim DatReq As New Data.DataTable
        Dim i As Int32 = 0, cont As Int32 = 1, id_req As Int32, j As Int32
        DatReq = Obj.TraerDataTable("paReq_ConsultarCronogramaNuevoModulo", 1, Me.HddSolicitud.Value)
        If DatReq.Rows.Count > 0 Then
            Me.CmdExportar.Visible = True
            'Cabecera
            Dim FilaSol1 As New TableRow
            Dim ColumSol1 As New TableCell
            Dim ColumSol2 As New TableCell
            Dim ColumSol3 As New TableCell
            Dim ColumSol4 As New TableCell
            Dim ColumSol5 As New TableCell
            Dim ColumSol6 As New TableCell

            FilaSol1.ForeColor = Drawing.Color.FromArgb(0, 64, 128)


            ColumSol1.Text = "N°"
            ColumSol2.Text = "Tarea"
            ColumSol3.Text = "F. Inicio"
            ColumSol4.Text = "F. Fin"
            ColumSol5.Text = "Duración"
            ColumSol6.Text = "Responsable"

            'ColumSol1.Width = 70
            ColumSol1.BorderColor = Drawing.Color.Black
            ColumSol1.BorderStyle = BorderStyle.Solid
            ColumSol1.BorderWidth = 1
            ColumSol1.HorizontalAlign = HorizontalAlign.Center
            ColumSol1.BackColor = Drawing.Color.FromName("#f0f0f0") '#CCCCCC")

            ColumSol2.BorderColor = Drawing.Color.Black
            ColumSol2.BorderStyle = BorderStyle.Solid
            ColumSol2.BorderWidth = 1
            ColumSol2.HorizontalAlign = HorizontalAlign.Center
            ColumSol2.BackColor = Drawing.Color.FromName("#f0f0f0")

            'ColumSol3.Width = 20
            ColumSol3.BorderColor = Drawing.Color.Black
            ColumSol3.BorderStyle = BorderStyle.Solid
            ColumSol3.BorderWidth = 1
            ColumSol3.HorizontalAlign = HorizontalAlign.Center
            ColumSol3.BackColor = Drawing.Color.FromName("#f0f0f0")

            'ColumSol4.Width = 20
            ColumSol4.HorizontalAlign = HorizontalAlign.Center
            ColumSol4.BorderColor = Drawing.Color.Black
            ColumSol4.BorderStyle = BorderStyle.Solid
            ColumSol4.BorderWidth = 1
            ColumSol4.BackColor = Drawing.Color.FromName("#f0f0f0")

            'ColumSol5.Width = 70
            ColumSol5.BorderColor = Drawing.Color.Black
            ColumSol5.BorderStyle = BorderStyle.Solid
            ColumSol5.BorderWidth = 1
            ColumSol5.HorizontalAlign = HorizontalAlign.Center
            ColumSol5.BackColor = Drawing.Color.FromName("#f0f0f0")

            ColumSol6.HorizontalAlign = HorizontalAlign.Center
            ColumSol6.BorderColor = Drawing.Color.Black
            ColumSol6.BorderStyle = BorderStyle.Solid
            ColumSol6.BorderWidth = 1
            ColumSol6.BackColor = Drawing.Color.FromName("#f0f0f0")

            FilaSol1.Cells.Add(ColumSol1)
            FilaSol1.Cells.Add(ColumSol2)
            FilaSol1.Cells.Add(ColumSol3)
            FilaSol1.Cells.Add(ColumSol4)
            FilaSol1.Cells.Add(ColumSol5)
            FilaSol1.Cells.Add(ColumSol6)

            FilaSol1.Height = 20
            FilaSol1.Font.Name = "Verdana"
            FilaSol1.Font.Size = 8
            FilaSol1.Font.Bold = True

            TablaDatos.Rows.Add(FilaSol1)
            id_req = DatReq.Rows(0).Item("id_req")

            Dim fechaini As Object
            Dim fechafin As Object

            For i = 0 To DatReq.Rows.Count - 1
                fechaini = DatReq.Rows(i).Item("FechaIni_req")
                fechafin = DatReq.Rows(i).Item("FechaFin_req")

                If fechaini Is System.DBNull.Value Then
                    fechaini = ""
                Else
                    fechaini = CDate(fechaini).ToShortDateString
                End If

                If fechafin Is System.DBNull.Value Then
                    fechafin = ""
                Else
                    fechafin = CDate(fechafin).ToShortDateString
                End If

                AgregarFila_Estilo(TablaDatos, cont, DatReq.Rows(i).Item("descripcion_req").ToString, fechaini, fechafin, "", "", 1)
                cont += 1
                Dim DatAct As New Data.DataTable
                DatAct = Obj.TraerDataTable("paReq_ConsultarCronogramaNuevoModulo", 2, DatReq.Rows(i).Item("id_req").ToString)
                For j = 0 To DatAct.Rows.Count - 1
                    AgregarFila_Estilo(TablaDatos, cont, DatAct.Rows(j).Item("descripcion_act").ToString, _
                    IIf(DatAct.Rows(j).Item("fechaini_croa") Is System.DBNull.Value, "", CDate(DatAct.Rows(j).Item("fechaini_croa")).ToShortDateString), _
                    IIf(DatAct.Rows(j).Item("fechafin_croa") Is System.DBNull.Value, "", CDate(DatAct.Rows(j).Item("fechafin_croa")).ToShortDateString), _
                    DatAct.Rows(j).Item("duracion_croa").ToString, DatAct.Rows(i).Item("personal").ToString, 2)
                    cont += 1
                Next
            Next
        End If
    End Sub
    Private Sub AgregarFila_Estilo(ByVal TablaDatos As Table, ByVal col1 As String, ByVal col2 As String, _
                            ByVal col3 As String, ByVal col4 As String, ByVal col5 As String, ByVal col6 As String, ByVal tipo As Int16)
        Dim FilaSol1 As New TableRow
        Dim ColumSol1 As New TableCell
        Dim ColumSol2 As New TableCell
        Dim ColumSol3 As New TableCell
        Dim ColumSol4 As New TableCell
        Dim ColumSol5 As New TableCell
        Dim ColumSol6 As New TableCell

        FilaSol1 = New TableRow
        ColumSol1 = New TableCell
        ColumSol2 = New TableCell
        ColumSol3 = New TableCell
        ColumSol4 = New TableCell
        ColumSol5 = New TableCell
        ColumSol6 = New TableCell

        ColumSol1.Text = col1 'cont
        If tipo = 1 Then
            ColumSol2.Text = col2 'DatReq.Rows(i).Item("descripcion_req").ToString
            FilaSol1.Font.Bold = True
        ElseIf tipo = 2 Then
            ColumSol2.Text = "&nbsp;&nbsp;&nbsp;&nbsp;" & col2
        End If

        ColumSol3.Text = col3 'DatReq.Rows(i).Item("FechaIni_req").ToString
        ColumSol4.Text = col4 'DatReq.Rows(i).Item("FechaFin_req").ToString
        ColumSol5.Text = col5 '""
        ColumSol6.Text = col6 '""
        'System.DBNull.Value 

        ColumSol1.BorderColor = Drawing.Color.Black
        ColumSol1.BorderStyle = BorderStyle.Solid
        ColumSol1.BorderWidth = 1
        ColumSol1.HorizontalAlign = HorizontalAlign.Center

        ColumSol2.BorderColor = Drawing.Color.Black
        ColumSol2.BorderStyle = BorderStyle.Solid
        ColumSol2.BorderWidth = 1
        ColumSol2.HorizontalAlign = HorizontalAlign.Left

        ColumSol3.BorderColor = Drawing.Color.Black
        ColumSol3.BorderStyle = BorderStyle.Solid
        ColumSol3.BorderWidth = 1
        ColumSol3.HorizontalAlign = HorizontalAlign.Center

        ColumSol4.BorderColor = Drawing.Color.Black
        ColumSol4.BorderStyle = BorderStyle.Solid
        ColumSol4.BorderWidth = 1
        ColumSol4.HorizontalAlign = HorizontalAlign.Center

        ColumSol5.BorderColor = Drawing.Color.Black
        ColumSol5.BorderStyle = BorderStyle.Solid
        ColumSol5.BorderWidth = 1
        ColumSol5.HorizontalAlign = HorizontalAlign.Center

        ColumSol6.BorderColor = Drawing.Color.Black
        ColumSol6.BorderStyle = BorderStyle.Solid
        ColumSol6.BorderWidth = 1
        ColumSol6.HorizontalAlign = HorizontalAlign.Center

        FilaSol1.Cells.Add(ColumSol1)
        FilaSol1.Cells.Add(ColumSol2)
        FilaSol1.Cells.Add(ColumSol3)
        FilaSol1.Cells.Add(ColumSol4)
        FilaSol1.Cells.Add(ColumSol5)
        FilaSol1.Cells.Add(ColumSol6)

        FilaSol1.Height = 28
        FilaSol1.Font.Name = "Verdana"
        FilaSol1.Font.Size = 8

        TablaDatos.CellPadding = 0
        TablaDatos.CellSpacing = 0
        TablaDatos.Rows.Add(FilaSol1)

        ColumSol1.Dispose()
        ColumSol2.Dispose()
        ColumSol3.Dispose()
        ColumSol4.Dispose()
        ColumSol5.Dispose()
        ColumSol6.Dispose()
    End Sub
    Protected Sub GvNewModulo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvNewModulo.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvNewModulo','Select$" & e.Row.RowIndex & "'); SeleccionarFila(); ResaltarPestana('0','','');")

            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")

        End If
    End Sub

    Protected Sub GvNewModulo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvNewModulo.SelectedIndexChanged
        Me.HddSolicitud.Value = Me.GvNewModulo.DataKeys.Item(Me.GvNewModulo.SelectedIndex).Values(0).ToString
        Llenargrid(Me.TblCronograma)
    End Sub

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteSolicitudes_.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub

    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim grid As New Table
        Dim label, label2, label3 As New Label
        Dim espacio, esp As New Label

        Llenargrid(grid)

        grid.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False

        label.Text = Me.GvNewModulo.Rows(Me.GvNewModulo.SelectedIndex).Cells(1).Text.ToUpper
        label.Font.Bold = True
        label2.Text = "Solicitado por: " & Me.GvNewModulo.Rows(Me.GvNewModulo.SelectedIndex).Cells(5).Text.ToUpper

        espacio.Text = "<br>"
        esp.Text = "<hr>"
        label3.Text = "<br>Área: " & Me.GvNewModulo.Rows(Me.GvNewModulo.SelectedIndex).Cells(4).Text.ToUpper

        Form2.Controls.Add(label)
        Form2.Controls.Add(espacio)
        Form2.Controls.Add(label2)
        Form2.Controls.Add(label3)
        Form2.Controls.Add(esp)
        Form2.Controls.Add(grid)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function
End Class
