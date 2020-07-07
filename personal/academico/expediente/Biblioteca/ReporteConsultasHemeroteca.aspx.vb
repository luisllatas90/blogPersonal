
Partial Class personal_academico_expediente_Biblioteca_ReporteConsultasHemeroteca
    Inherits System.Web.UI.Page

    Protected Sub ChkFechas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkFechas.CheckedChanged
        If ChkFechas.Checked = True Then
            Me.TxtFechaIni.BackColor = Drawing.Color.LightYellow
            Me.TxtFechafin.BackColor = Drawing.Color.LightYellow
        Else
            Me.TxtFechaIni.BackColor = Drawing.Color.LightGray
            Me.TxtFechafin.BackColor = Drawing.Color.LightGray
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
        Dim DatTipoLec, DatTipoMat, DatTipoPres As New Data.DataTable
        If Not IsPostBack Then
            DatTipoLec = obj.TraerDataTable("ConsultarTipoLector")
            ClsFunciones.LlenarListas(Me.CboTipoLector, DatTipoLec, "IdTipoLector", "NombreTipo", "Todos")
            DatTipoMat = obj.TraerDataTable("ConsultarTipoMaterial", "1", "", "")
            ClsFunciones.LlenarListas(Me.CboTipoMaterial, DatTipoMat, "IdMaterial", "NombreMaterial", "Todos")
            DatTipoPres = obj.TraerDataTable("BI_ConsultarEstadoMaterial", "TO", "", "")
            ClsFunciones.LlenarListas(Me.CboTipoPrestamo, DatTipoPres, "IdEstado", "DescripcionEstado", "Todos")
        End If
    End Sub

    Protected Sub CmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdConsultar.Click
         ConsultarConsultasHemeroteca(DgvDatos)
    End Sub
    Private Sub ConsultarConsultasHemeroteca(ByRef grid As GridView)
        Dim Datos As New Data.DataTable
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBIBUSAT").ConnectionString)
        If Me.ChkFechas.Checked = False Then ' consultar sin fecha
            'Todos
            If Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "TO", 0, 0, 0)
                'Por tipo de lector
            ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTL", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                'Por tipo de material
            ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTM", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                'Por tipo de préstamo
            ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTP", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                'Por tipo lector - tipo préstamo
            ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTLTP", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                'Por tipo lector y tipo material
            ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTLTM", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                'Por tipo material y tipo prestamo
            ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemeroteca", "XTMTP", Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
            End If
        Else ' Consultar por fecha
            If ValidarFechas(Me.TxtFechaIni.Text, Me.TxtFechafin.Text) Then
                If Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "TO", TxtFechaIni.Text, TxtFechafin.Text, 0, 0, 0)
                    'Por tipo de lector
                ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTL", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                    'Por tipo de material
                ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTM", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                    'Por tipo de préstamo
                ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTP", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                    'Por tipo lector - tipo préstamo
                ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue = -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTLTP", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                    'Por tipo lector y tipo material
                ElseIf Me.CboTipoLector.SelectedValue <> -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue = -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTLTM", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                    'Por tipo material y tipo prestamo
                ElseIf Me.CboTipoLector.SelectedValue = -1 And Me.CboTipoMaterial.SelectedValue <> -1 And Me.CboTipoPrestamo.SelectedValue <> -1 Then
                    Datos = Obj.TraerDataTable("HEM_ConsultarLibrosConsultadosHemerotecaXFechas", "XTMTP", TxtFechaIni.Text, TxtFechafin.Text, Me.CboTipoLector.SelectedValue, Me.CboTipoMaterial.SelectedValue, Me.CboTipoPrestamo.SelectedValue)
                End If
            End If
        End If
        If Datos.Rows.Count > 0 Then
            Me.LblTotal.Text = Datos.Rows.Count & " registros"
            grid.DataSource = Datos
            grid.DataBind()
        Else
            Me.LblTotal.Text = 0 & " registros"
            grid.DataSource = Nothing
            grid.DataBind()
        End If
    End Sub

    Private Function ValidarFechas(ByVal fechaini As DateTime, ByVal fechafin As DateTime) As Boolean
        If (fechaini <= fechafin) Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub DgvDatos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvDatos.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            'e.Row.BackColor = Drawing.Color.Azure
        End If
    End Sub


    Protected Sub CmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdExportar.Click
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment;filename=reporte.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()
    End Sub

    Public Function HTML() As String
        Dim page1 As New Page
        Dim form1 As New HtmlForm
        Dim Builder1 As New System.Text.StringBuilder
        Dim writer1 As New System.IO.StringWriter(Builder1)
        Dim writer2 As New HtmlTextWriter(writer1)
        Dim grid As New GridView
        ConsultarConsultasHemeroteca(grid)

        DgvDatos.EnableViewState = False
        page1.EnableViewState = False
        page1.EnableEventValidation = False

        page1.Controls.Add(form1)
        If ChkFechas.Checked = True Then
            writer1.Write("<strong>" & Me.LblTitulo.Text & " DEL " & Me.TxtFechaIni.Text & " AL " & Me.TxtFechafin.Text & "</strong><br> Módulo Biblioteca - Campus Virtual - USAT")
        Else
            writer1.Write("<strong>" & Me.LblTitulo.Text & " A LA FECHA " & Date.Now.ToShortDateString & "</strong><br> Módulo Biblioteca - Campus Virtual - USAT")
        End If
        grid.HeaderStyle.BackColor = Drawing.Color.LightBlue
        form1.Controls.Add(grid)
        page1.RenderControl(writer2)
        writer2.Write("<font face='verdana' size='1' color='#121212'><br><br><strong>Sistema de Consultas Hemeroteca - Desarrollo de Sistemas <br>Actualizado al: " & Date.Now() & "<strong></font>")
        page1.Dispose()
        page1 = Nothing
        Return Builder1.ToString
    End Function
End Class
