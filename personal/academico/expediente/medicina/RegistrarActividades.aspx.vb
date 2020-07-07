
Partial Class medicina_RegistrarActividades
    Inherits System.Web.UI.Page
    Dim Horario As System.Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim Codigo_cup As Integer
        Codigo_cup = Request.QueryString("codigo_cup")

        Me.LblMensaje.Visible = False

        If Not IsPostBack Then
            Me.TxtHoraInicio.Attributes.Add("onkeyup", "javascript:mascara(this,':',patron2,true)")
            Me.TxtHoraFin.Attributes.Add("onkeyup", "javascript:mascara(this,':',patron2,true)")
            Me.HpLinkAsistencia.NavigateUrl = "reportes/reporteasistencia.aspx?codigo_cup=" & Codigo_cup & "&codigo_syl=" & Request.QueryString("codigo_syl")
            Me.HpLinkNotas.NavigateUrl = "reportes/reportenotas.aspx?codigo_cup=" & Codigo_cup & "&codigo_syl=" & Request.QueryString("codigo_syl")
            Me.HpLinkResumen.NavigateUrl = "reportes/reporteconsolidado.aspx?codigo_cup=" & Codigo_cup & "&codigo_syl=" & Request.QueryString("codigo_syl")
            Me.HpLinkAlumnos.NavigateUrl = "reportes/reportealumnos.aspx?codigo_cup=" & Codigo_cup & "&codigo_syl=" & Request.QueryString("codigo_syl")
            Me.CmdRegresar.Attributes.Add("OnClick", "javascript:location.href='../../notas/profesor/miscursos2.asp?codigo_cac=" & Request.QueryString("codigo_cac") & "'; return false;")

            Dim tblCurso As Data.DataTable
            obj.AbrirConexion()
            tblCurso = obj.TraerDataTable("ConsultarCursoProgramado", 10, Codigo_cup, 0, 0, 0)
            obj.CerrarConexion()
            If tblCurso.Rows.Count > 0 Then
                Me.lblcurso.Text = tblCurso.Rows(0).Item("nombre_cur") & " - Grupo (" & tblCurso.Rows(0).Item("grupohor_cup") & ")"
                Me.lblInicio.Text = tblCurso.Rows(0).Item("fechainicio_cup")
                Me.lblFin.Text = tblCurso.Rows(0).Item("fechafin_cup")
            End If
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.DDLTipoActividad, obj.TraerDataTable("MED_ConsultarTipoActividad", "A"), "codigo_tac", "descripcion_act")
            obj.CerrarConexion()
            Me.DDLTipoActividad.SelectedIndex = 1

            If Request.QueryString("pag") <> "" AndAlso Session("MED_Fecha") <> "" Then
                CargarActividades(Session("MED_Fecha"))
                Me.CalHorario.SelectedDate = Session("MED_Fecha")
            Else
                CargarActividades(Now)
                Me.CalHorario.SelectedDate = Now
            End If
        End If
        obj.AbrirConexion()
        Horario = obj.TraerDataTable("MED_ConsultarHorarioCurso", 0, Codigo_cup, 0)
        obj.CerrarConexion()
        For i As Integer = 0 To RbHoras.Items.Count - 1
            Me.RbHoras.Items(i).Attributes.Add("OnClick", "javascript:form1.CmdNuevo.disabled=false;")
        Next
    End Sub

    Protected Sub CalHorario_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles CalHorario.DayRender
        For i As Integer = 0 To Horario.Rows.Count - 1
            If DateDiff(DateInterval.Day, Horario.Rows(i).Item("fechainicio_cup"), e.Day.Date) >= 0 And _
               DateDiff(DateInterval.Day, Horario.Rows(i).Item("fechafin_cup"), e.Day.Date) <= 0 Then
                If e.Day.Date.DayOfWeek = Horario.Rows(i).Item("num_dia") Then
                    e.Cell.BorderColor = Drawing.Color.Red
                    e.Cell.BorderStyle = BorderStyle.Solid
                    e.Cell.BackColor = Drawing.Color.FromArgb(0, 255, 145, 145)
                    e.Cell.ForeColor = Drawing.Color.Black
                    e.Cell.BorderWidth = 2
                    If e.Day.Date.ToShortDateString = Me.CalHorario.SelectedDate.ToShortDateString Then
                        e.Cell.BackColor = Drawing.Color.Gray
                    End If
                ElseIf e.Day.Date.ToShortDateString = Me.CalHorario.SelectedDate.ToShortDateString Then
                    e.Cell.BackColor = Drawing.Color.Gray
                End If
            Else
                e.Cell.Text = ""
            End If
        Next
    End Sub

    Private Sub CargarActividades(ByVal fecha As Date)
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim RegDelDia As System.Data.DataTable
        Dim dia As String

        Session("MED_fecha") = fecha.ToShortDateString
        Select Case DatePart(DateInterval.Weekday, fecha) - 1
            Case 0 : dia = "DOMINGO"
            Case 1 : dia = "LUNES"
            Case 2 : dia = "MARTES"
            Case 3 : dia = "MIERCOLES"
            Case 4 : dia = "JUEVES"
            Case 5 : dia = "VIERNES"
            Case 6 : dia = "SABADO"
        End Select
        Me.LblDiaSeleccionado.Text = dia & " - " & fecha.ToShortDateString
        Me.HddFecha.Value = fecha.ToShortDateString
        Obj.AbrirConexion()
        ClsFunciones.LlenarListas(Me.RbHoras, Obj.TraerDataTable("MED_ConsultarActividadesSegunDia", Request.QueryString("codigo_cup"), dia, fecha), "codigo_hdo", "descripcionhora", "-- Otras actividades --")
        RegDelDia = Obj.TraerDataTable("MED_ConsultarActividadesHoy", fecha, Request.QueryString("codigo_syl"))
        Obj.CerrarConexion()
        For i As Integer = 0 To RbHoras.Items.Count - 1
            Me.RbHoras.Items(i).Attributes.Add("OnClick", "javascript:form1.CmdNuevo.disabled=false;")
        Next

        Me.DgvRegDelDia.DataSource = RegDelDia
        Me.DgvRegDelDia.DataBind()

    End Sub

    Protected Sub CalHorario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CalHorario.SelectionChanged
        Me.DDLEstadoAct.SelectedIndex = 0
        CargarActividades(Me.CalHorario.SelectedDate)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdNuevo.Click

        Me.HddCodigo_act.Value = 0
        Me.ChkHabilitar.Checked = False
        Me.ChkAsistencia.Checked = True
        If Me.RbHoras.SelectedValue <> "-1" Then
            Me.TxtActividad.Text = Me.RbHoras.SelectedItem.Text
            Me.TxtTemas.Text = ""
            Me.LblFecha.Text = Me.HddFecha.Value
            Me.TxtHoraInicio.Text = Me.RbHoras.SelectedItem.Text.Substring(9, 5)
            Me.TxtHoraFin.Text = Me.RbHoras.SelectedItem.Text.Substring(17, 5)
        Else
            Me.TxtActividad.Text = ""
            Me.TxtTemas.Text = ""
            Me.LblFecha.Text = Me.HddFecha.Value
            Me.TxtHoraInicio.Text = ""
            Me.TxtHoraFin.Text = ""
        End If
        Me.MPEActividad.show()

    End Sub

    Protected Sub DgvRegDelDia_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvRegDelDia.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(1).ToolTip = "TEMAS: " & fila.Row("temas_act").ToString

            If fila.Row("estadorealizado_act").ToString.ToUpper = "S" Then
                e.Row.Cells(6).Text = "<img src='../images/ok.gif' alt='Actividad Realizada'>"
            Else
                e.Row.Cells(6).Text = ""
            End If

            If fila.Row("considerarnota_act").ToString.ToUpper = "S" Then
                e.Row.Cells(5).Text = "SI"
            Else
                e.Row.Cells(5).Text = "NO"
            End If

            If fila.Row("fechaini_act") >= Date.Now Then
                e.Row.Cells(9).Enabled = False
            Else
                e.Row.Cells(9).Enabled = True
            End If
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim ObjGuardar As New ClsConectarDatos
        ObjGuardar.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim codigo_syl As Integer
        Dim fechaini, fechafin As DateTime

        If validarFechas() = True Then
            fechaini = CDate(Me.LblFecha.Text & " " & Me.TxtHoraInicio.Text)
            fechafin = CDate(Me.LblFecha.Text & " " & Me.TxtHoraFin.Text)

            If Request.QueryString("codigo_syl") <> "" Then
                codigo_syl = Request.QueryString("codigo_syl")
                ObjGuardar.AbrirConexion()
                If Me.HddCodigo_act.Value = 0 Then
                    ObjGuardar.Ejecutar("MED_InsertarActividadesNotas", codigo_syl, Me.TxtActividad.Text.Trim, Me.TxtTemas.Text.Trim, 0, 1, fechaini, fechafin, 1, Me.DDLTipoActividad.SelectedValue, IIf(Me.ChkHabilitar.Checked = True, "S", "N"), Me.DDLPonderacion.SelectedValue, IIf(Me.ChkAsistencia.Checked = True, 1, 0), 0)
                Else
                    ObjGuardar.Ejecutar("MEd_ActualizarActividadesNotas", Me.HddCodigo_act.Value, Me.TxtActividad.Text.Trim, Me.TxtTemas.Text.Trim, fechaini, fechafin, Me.DDLTipoActividad.SelectedValue, Me.DDLPonderacion.SelectedValue, IIf(Me.ChkHabilitar.Checked = True, "S", "N"), IIf(Me.ChkAsistencia.Checked = True, 1, 0))
                End If
                ObjGuardar.CerrarConexion()
                CargarActividades(Me.HddFecha.Value)
            End If
        End If        
    End Sub

    Private Function validarFechas() As Boolean
        Try
            Date.Parse(Me.LblFecha.Text & " " & Me.TxtHoraInicio.Text)
        Catch ex As Exception
            Me.TxtHoraInicio.Text = ""
            Me.lblAviso.Text = "El formato de hora de inicio es 08:00"
            Return False
        End Try
        Try
            Date.Parse(Me.LblFecha.Text & " " & Me.TxtHoraFin.Text)
        Catch ex As Exception
            Me.TxtHoraFin.Text = ""
            Me.lblAviso.Text = "El formato de hora de final es 08:00"
            Return False
        End Try
        Me.lblAviso.Text = ""
        Return True
    End Function

    Protected Sub DgvRegDelDia_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles DgvRegDelDia.RowDeleting
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        'Try
        Obj.AbrirConexion()
        If Obj.TraerDataTable("MED_ConsultarEstadoActividad", Me.DgvRegDelDia.DataKeys(e.RowIndex).Value).Rows(0).Item("estadorealizado_act").ToString = "S" Then
            Me.LblMensaje.Text = "No se puede eliminar una actividad ya realizada."
            Me.LblMensaje.Visible = True
        Else
            Obj.Ejecutar("MED_EliminarActividad", Me.DgvRegDelDia.DataKeys(e.RowIndex).Value)
            CargarActividades(Me.HddFecha.Value)
        End If
        Obj.CerrarConexion()
        'Catch ex As Exception
        'End Try
        e.Cancel = True
    End Sub

    Protected Sub DgvRegDelDia_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DgvRegDelDia.RowEditing

        Try
            Dim Obj As New ClsConectarDatos
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim Datos As New Data.DataTable
            Me.HddCodigo_act.Value = Me.DgvRegDelDia.DataKeys(e.NewEditIndex).Value
            Obj.AbrirConexion()
            Datos = Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Me.DgvRegDelDia.DataKeys(e.NewEditIndex).Value)
            Obj.CerrarConexion()
            With Datos.Rows(0)
                Me.TxtActividad.Text = .Item("nombre").ToString
                Me.TxtTemas.Text = .Item("temas_act").ToString
                Me.LblFecha.Text = CDate(.Item("fechaini_act").ToString).ToShortDateString
                Me.TxtHoraInicio.Text = Me.DgvRegDelDia.Rows(e.NewEditIndex).Cells(3).Text
                Me.TxtHoraFin.Text = Me.DgvRegDelDia.Rows(e.NewEditIndex).Cells(4).Text
                Me.DDLTipoActividad.SelectedValue = .Item("Codigo_tac")
                If .Item("peso_act") IsNot System.DBNull.Value Then
                    Me.DDLPonderacion.SelectedIndex = CInt(.Item("peso_act")) - 1
                Else
                    Me.DDLPonderacion.SelectedIndex = 0
                End If

                If .Item("considerarnota_act").ToString.ToUpper = "SI" Then
                    Me.ChkHabilitar.Checked = True
                Else
                    Me.ChkHabilitar.Checked = False
                End If

                Me.ChkAsistencia.Checked = .Item("ConsiderarAsistencia_act")
                
            End With
            Me.MPEActividad.show()

        Catch ex As Exception
        End Try
        e.Cancel = True

    End Sub

    Protected Sub DDLEstadoAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLEstadoAct.SelectedIndexChanged
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Obj.AbrirConexion()
        Me.DgvRegDelDia.DataSource = Obj.TraerDataTable("MED_COnsultarActividades", 0, Request.QueryString("codigo_syl"), Me.DDLEstadoAct.SelectedValue)
        Obj.CerrarConexion()
        Me.DgvRegDelDia.DataBind()
    End Sub

  
End Class
