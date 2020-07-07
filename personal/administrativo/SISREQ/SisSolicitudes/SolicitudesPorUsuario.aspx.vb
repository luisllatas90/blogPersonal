
Partial Class SisSolicitudes_SolicitudesPorUsuario
    Inherits System.Web.UI.Page

    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        If Me.TxtFechaIni.Text <> "" And Me.TxtFechaFin.Text <> "" Then
            If Me.TxtFechaFin.Text >= Me.TxtFechaIni.Text Then
                Axls()
            Else
                Page.RegisterStartupScript("alert1", "<script>alert('La fecha final no puede ser menor a la fecha de inicio');</script>")
            End If
        Else
            Page.RegisterStartupScript("alert2", "<script>alert('Para exportar debe primero realizar una consultar');</script>")
        End If

    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-word"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteSolicitudes_.doc")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub

    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim grid As New Table
        Dim label As New Label

        Llenargrid(grid)

        grid.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False

        label.Text = "Solicitudes registradas <strong>del " + Me.TxtFechaIni.Text + " al " + Me.TxtFechaFin.Text + "</strong><br><br>"
        Form2.Controls.Add(label)
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

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        If Me.TxtFechaIni.Text <> "" And Me.TxtFechaFin.Text <> "" Then
            If Me.TxtFechaFin.Text >= Me.TxtFechaIni.Text Then
                Llenargrid(Me.TblSolicitudes)
            Else
                Page.RegisterStartupScript("FImayorFF", "<script>alert('La fecha final no puede ser menor a la fecha de inicio');</script>")
            End If
        Else
            Page.RegisterStartupScript("Sin fechas", "<script>alert('Usted debe seleccionar una fecha de inicio y una fecha final');</script>")
        End If
    End Sub


    Private Sub Llenargrid(ByVal TablaDatos As Table)
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Solicitudes As New Data.DataTable
        If Me.CboUsuario.SelectedValue > 0 Then
            Solicitudes = Obj.TraerDataTable("SOL_ConsultarSolicitudPorUsuario", 1, Me.CboUsuario.SelectedValue, Me.TxtFechaIni.Text, Me.TxtFechaFin.Text)
        Else
            Solicitudes = Obj.TraerDataTable("SOL_ConsultarSolicitudPorUsuario", 0, 0, Me.TxtFechaIni.Text, Me.TxtFechaFin.Text)
        End If
        Dim i As Int32 = 0
        Dim cont As Int32 = 0
        Dim strNum_Sol As String
        Dim Asunto As String = ""

        If Solicitudes.Rows.Count > 0 Then
            strNum_Sol = Solicitudes.Rows(0).Item("numero_sol").ToString
            Asunto = "<strong>»</strong> " + Solicitudes.Rows(0).Item("asunto").ToString

            ' DEFINICION DE LA CABECERA
            Dim FilaSol1 As New TableRow
            Dim ColumSol1 As New TableCell
            Dim ColumSol2 As New TableCell
            Dim ColumSol3 As New TableCell
            Dim ColumSol4 As New TableCell
            Dim ColumSol5 As New TableCell
            Dim ColumSol6 As New TableCell

            ColumSol1.Text = "N°"
            ColumSol2.Text = "Solicitud"
            ColumSol3.Text = "Fecha"
            ColumSol4.Text = "Apellidos y Nombres"
            ColumSol5.Text = "Carrera Profesional"
            ColumSol6.Text = "Asunto"

            ColumSol1.Width = 20
            ColumSol1.BorderColor = Drawing.Color.Black
            ColumSol1.BorderStyle = BorderStyle.Solid
            ColumSol1.BorderWidth = 1
            ColumSol1.HorizontalAlign = HorizontalAlign.Center
            ColumSol1.BackColor = Drawing.Color.FromName("#81A7E8") '#CCCCCC")

            ColumSol2.Width = 70
            ColumSol2.BorderColor = Drawing.Color.Black
            ColumSol2.BorderStyle = BorderStyle.Solid
            ColumSol2.BorderWidth = 1
            ColumSol2.HorizontalAlign = HorizontalAlign.Center
            ColumSol2.BackColor = Drawing.Color.FromName("#81A7E8")

            ColumSol3.Width = 80
            ColumSol3.BorderColor = Drawing.Color.Black
            ColumSol3.BorderStyle = BorderStyle.Solid
            ColumSol3.BorderWidth = 1
            ColumSol3.HorizontalAlign = HorizontalAlign.Center
            ColumSol3.BackColor = Drawing.Color.FromName("#81A7E8")

            ColumSol4.HorizontalAlign = HorizontalAlign.Center
            ColumSol4.BorderColor = Drawing.Color.Black
            ColumSol4.BorderStyle = BorderStyle.Solid
            ColumSol4.BorderWidth = 1
            ColumSol4.BackColor = Drawing.Color.FromName("#81A7E8")

            ColumSol5.Width = 250
            ColumSol5.BorderColor = Drawing.Color.Black
            ColumSol5.BorderStyle = BorderStyle.Solid
            ColumSol5.BorderWidth = 1
            ColumSol5.HorizontalAlign = HorizontalAlign.Center
            ColumSol5.BackColor = Drawing.Color.FromName("#81A7E8")

            ColumSol6.HorizontalAlign = HorizontalAlign.Center
            ColumSol6.BorderColor = Drawing.Color.Black
            ColumSol6.BorderStyle = BorderStyle.Solid
            ColumSol6.BorderWidth = 1
            ColumSol6.BackColor = Drawing.Color.FromName("#81A7E8")

            FilaSol1.Cells.Add(ColumSol1)
            FilaSol1.Cells.Add(ColumSol2)
            FilaSol1.Cells.Add(ColumSol3)
            FilaSol1.Cells.Add(ColumSol4)
            FilaSol1.Cells.Add(ColumSol5)
            FilaSol1.Cells.Add(ColumSol6)

            FilaSol1.Height = 28
            FilaSol1.Font.Name = "Verdana"
            FilaSol1.Font.Size = 9
            FilaSol1.Font.Bold = True


            TablaDatos.Rows.Add(FilaSol1)

            For i = 0 To Solicitudes.Rows.Count - 2

                If Solicitudes.Rows(i + 1).Item("numero_sol").ToString <> strNum_Sol Then
                    ' Si es igual, --> Concatenar

                    cont += 1
                    'Asignar los nuevos parametros 
                    FilaSol1 = New TableRow
                    ColumSol1 = New TableCell
                    ColumSol2 = New TableCell
                    ColumSol3 = New TableCell
                    ColumSol4 = New TableCell
                    ColumSol5 = New TableCell
                    ColumSol6 = New TableCell

                    ColumSol1.Text = cont
                    ColumSol2.Text = Solicitudes.Rows(i).Item("numero_sol")
                    ColumSol3.Text = Solicitudes.Rows(i).Item("fecha_sol")
                    ColumSol4.Text = Solicitudes.Rows(i).Item("alumno")
                    ColumSol5.Text = Solicitudes.Rows(i).Item("nombre_cpf")
                    ColumSol6.Text = Asunto

                    ColumSol1.BorderColor = Drawing.Color.Black
                    ColumSol1.BorderStyle = BorderStyle.Solid
                    ColumSol1.BorderWidth = 1
                    ColumSol1.HorizontalAlign = HorizontalAlign.Center

                    ColumSol2.BorderColor = Drawing.Color.Black
                    ColumSol2.BorderStyle = BorderStyle.Solid
                    ColumSol2.BorderWidth = 1
                    ColumSol2.HorizontalAlign = HorizontalAlign.Center

                    ColumSol3.BorderColor = Drawing.Color.Black
                    ColumSol3.BorderStyle = BorderStyle.Solid
                    ColumSol3.BorderWidth = 1
                    ColumSol3.HorizontalAlign = HorizontalAlign.Center

                    ColumSol4.BorderColor = Drawing.Color.Black
                    ColumSol4.BorderStyle = BorderStyle.Solid
                    ColumSol4.BorderWidth = 1

                    ColumSol5.BorderColor = Drawing.Color.Black
                    ColumSol5.BorderStyle = BorderStyle.Solid
                    ColumSol5.BorderWidth = 1
                    ColumSol5.HorizontalAlign = HorizontalAlign.Center

                    'ColumSol6.Width = 250
                    ColumSol6.BorderColor = Drawing.Color.Black
                    ColumSol6.BorderStyle = BorderStyle.Solid
                    ColumSol6.BorderWidth = 1


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

                    strNum_Sol = Solicitudes.Rows(i + 1).Item("numero_sol").ToString
                    Asunto = "<strong>» </strong> " + Solicitudes.Rows(i + 1).Item("asunto").ToString
                Else
                    Asunto = Asunto + " <br> <strong>» </strong>" + Solicitudes.Rows(i + 1).Item("asunto").ToString
                End If
            Next

            i = Solicitudes.Rows.Count - 1
            cont += 1
            'Asignar los nuevos parametros 
            FilaSol1 = New TableRow
            ColumSol1 = New TableCell
            ColumSol2 = New TableCell
            ColumSol3 = New TableCell
            ColumSol4 = New TableCell
            ColumSol5 = New TableCell
            ColumSol6 = New TableCell

            ColumSol1.Text = cont
            ColumSol2.Text = Solicitudes.Rows(i).Item("numero_sol")
            ColumSol3.Text = Solicitudes.Rows(i).Item("fecha_sol")
            ColumSol4.Text = Solicitudes.Rows(i).Item("alumno")
            ColumSol5.Text = Solicitudes.Rows(i).Item("nombre_cpf")
            ColumSol6.Text = Asunto

            ColumSol1.BorderColor = Drawing.Color.Black
            ColumSol1.BorderStyle = BorderStyle.Solid
            ColumSol1.BorderWidth = 1
            ColumSol1.HorizontalAlign = HorizontalAlign.Center

            ColumSol2.BorderColor = Drawing.Color.Black
            ColumSol2.BorderStyle = BorderStyle.Solid
            ColumSol2.BorderWidth = 1
            ColumSol2.HorizontalAlign = HorizontalAlign.Center

            ColumSol3.BorderColor = Drawing.Color.Black
            ColumSol3.BorderStyle = BorderStyle.Solid
            ColumSol3.BorderWidth = 1
            ColumSol3.HorizontalAlign = HorizontalAlign.Center

            ColumSol4.BorderColor = Drawing.Color.Black
            ColumSol4.BorderStyle = BorderStyle.Solid
            ColumSol4.BorderWidth = 1

            ColumSol5.BorderColor = Drawing.Color.Black
            ColumSol5.BorderStyle = BorderStyle.Solid
            ColumSol5.BorderWidth = 1
            ColumSol5.HorizontalAlign = HorizontalAlign.Center

            'ColumSol6.Width = 250
            ColumSol6.BorderColor = Drawing.Color.Black
            ColumSol6.BorderStyle = BorderStyle.Solid
            ColumSol6.BorderWidth = 1


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

        Else
            Dim fila As New TableRow
            Dim colum As New TableCell
            colum.Text = "No se encontraron registros"
            colum.ForeColor = Drawing.Color.Red
            fila.Cells.Add(colum)
            TablaDatos.Rows.Add(fila)
            TablaDatos.BorderWidth = 0
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ClsFunciones.LlenarListas(Me.CboUsuario, Obj.TraerDataTable("ConsultarPersonalCentroCostos", "1", 165), "codigo_per", "datos_per", "Todos")
        End If
    End Sub

End Class
