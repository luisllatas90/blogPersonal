Partial Class administrativo_pec_frmProgramarHorarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargarFechas()
            chkVarias_CheckedChanged(sender, e)
            ddlSelDia_SelectedIndexChanged(sender, e)
            Me.ddlSelMes.SelectedIndex = Today.Month - 1
            Me.ddlInicioMes.SelectedIndex = Today.Month - 1
            Me.ddlFinMes.SelectedIndex = Today.Month - 1
            Me.ddlInicioDia.SelectedIndex = Today.Day - 1
            Me.ddlFinDia.SelectedIndex = Today.Day + 1
            CargarFechadelMes()
            li1Asp.Attributes.Add("class", "active")
            If Not Me.chkVarias.Checked Then
                Me.tab1.Attributes.Remove("class")
                Me.tab1.Attributes.Add("class", "tab active")
            End If
        End If

    End Sub

    Sub CargarFechas()
        Dim item As String
        For i As Integer = 1 To 23
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinHora.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlInicioHora.SelectedIndex = 6

        For i As Integer = 0 To 59
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinMinuto.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlFinHora.SelectedIndex = 21

        For i As Integer = 1 To 31
            If i < 10 Then
                item = "0" & i.ToString
            Else
                item = i.ToString
            End If
            Me.ddlInicioDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinDia.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i

        For i As Integer = Today.Year To Today.Year + 5
            item = i.ToString
            Me.ddlInicioAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlFinAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
            Me.ddlSelAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i        
    End Sub

    Protected Sub btnRegistrarPre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPre.Click
        Dim día As String
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim codigo_cup As Integer = CInt(Session("h_codigo_cup"))
        Dim usu As Integer = CInt(Session("h_id"))
        Dim TbHorariosReg As New Data.DataTable
        Dim TbHorariosCru As New Data.DataTable
        día = Me.ddlSelDia.SelectedValue
        nombre_hor = Me.ddlHorario.SelectedItem.Text.Substring(0, 5)
        horaFin = Me.ddlHorario.SelectedItem.Text.Substring(10, 5)

        Dim fechaIni As New Date(CDate(ddlFecha.SelectedValue).Year, CDate(ddlFecha.SelectedValue).Month, CDate(ddlFecha.SelectedValue).Day, CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        Dim fechafin As New Date(CDate(ddlFecha.SelectedValue).Year, CDate(ddlFecha.SelectedValue).Month, CDate(ddlFecha.SelectedValue).Day, CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)



        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        obj.AbrirConexion()
        TbHorariosReg = obj.TraerDataTable("HorarioPE_Consultar", codigo_cup, "F", fechaIni)
        obj.CerrarConexion()

        Dim HayCru As Boolean = False
        If TbHorariosReg.Rows.Count Then
            For i As Integer = 0 To TbHorariosReg.Rows.Count - 1
                obj.AbrirConexion()
                TbHorariosCru = obj.TraerDataTable("HorarioPE_ConsultarCru", codigo_cup, fechaIni, nombre_hor, horaFin)
                obj.CerrarConexion()
                If TbHorariosCru.Rows.Count Then
                    HayCru = True
                    Exit For
                End If
            Next
        End If

        TbHorariosReg.Dispose()
        TbHorariosCru.Dispose()
        If Not HayCru Then
            Dim tb As New Data.DataTable
            obj.AbrirConexion()
            tb = obj.TraerDataTable("HorarioPE_Registrar", día, codigo_cup, nombre_hor, horaFin, usu, fechaIni, fechafin)
            obj.CerrarConexion()
            If tb.Rows.Count Then
                Me.lblMsj.Text = "Horario registrado"
            Else
                Me.lblMsj.Text = "Ocurrió un error"
            End If
            tb.Dispose()
        Else
            Me.lblMsj.Text = "No se puede registrar este horario, debido a un cruce."
        End If
        obj = Nothing
    End Sub
    Sub CargarFechadelMes()
        Me.ddlFecha.Items.Clear()
        Dim año As Integer = Me.ddlSelAño.SelectedValue
        Dim mes As Integer = Me.ddlSelMes.SelectedValue
        Dim fechaRef As New Date(año, mes, 1)
        Dim fechaFin As New Date()
        fechaFin = DateSerial(Year(fechaRef), Month(fechaRef) + 1, 0) 'ultimo dia del mes
        Do
            If WeekdayName(Weekday(fechaRef)) = Me.ddlSelDia.SelectedItem.Text.ToLower Then
                Me.ddlFecha.Items.Add(New ListItem(fechaRef.ToString.Substring(0, 10), fechaRef.ToString.Substring(0, 10)))
            End If
            fechaRef = fechaRef.AddDays(1)
        Loop While fechaRef <= fechaFin
    End Sub   
    Protected Sub chkVarias_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVarias.CheckedChanged
        Me.ddlFinDia.Enabled = chkVarias.Checked
        Me.ddlFinMes.Enabled = chkVarias.Checked
        Me.ddlFinAño.Enabled = chkVarias.Checked
        Me.ddlDiaSelPer.Enabled = chkVarias.Checked

        If chkVarias.Checked Then
            Me.tab1.Attributes.Remove("class")
            Me.tab1.Attributes.Add("class", "tab")
            li1Asp.Attributes.Remove("class")
            Me.tab2.Attributes.Remove("class")
            Me.tab2.Attributes.Add("class", "tab active")
            Me.li2Asp.Attributes.Add("class", "active")
        End If
    End Sub

    Protected Sub ddlSelDia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelDia.SelectedIndexChanged
        Me.ddlHorario.Items.Clear()
        If Me.ddlSelDia.SelectedValue <> "SA" Then
            Me.ddlHorario.Items.Add(New ListItem("19:30 h - 23:00 h", 0))
        Else
            Me.ddlHorario.Items.Add(New ListItem("15:00 h - 18:20 h", 1))
            Me.ddlHorario.Items.Add(New ListItem("18:40 h - 22:00 h", 2))
        End If
        CargarFechadelMes()
    End Sub

    Protected Sub ddlSelMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelMes.SelectedIndexChanged
        CargarFechadelMes()
    End Sub

    Protected Sub ddlSelAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelAño.SelectedIndexChanged
        CargarFechadelMes()
    End Sub

    Protected Sub btnRegistrarPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarPers.Click
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim HayCru As Boolean = False
        Dim día As String
        Dim nombre_hor As String = ""
        Dim horaFin As String = ""
        Dim codigo_cup As Integer = CInt(Session("h_codigo_cup"))
        Dim usu As Integer = CInt(Session("h_id"))
        Dim msjVariasSesSI As String = ""
        Dim msjVariasSesNO As String = ""
        Dim TbHorariosReg As New Data.DataTable
        Dim TbHorariosCru As New Data.DataTable

        día = Me.ddlDiaSelPer.SelectedValue
        nombre_hor = Me.ddlInicioHora.SelectedItem.Text & ":" & Me.ddlInicioMinuto.SelectedItem.Text
        horaFin = Me.ddlFinHora.SelectedItem.Text & ":" & Me.ddlFinMinuto.SelectedItem.Text

        Dim fechaInicio As New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(nombre_hor.Substring(0, 2)), CInt(nombre_hor.Substring(3, 2)), 0)
        Dim fechaFin As New Date(CInt(Me.ddlFinAño.SelectedValue), CInt(Me.ddlFinMes.SelectedValue), CInt(Me.ddlFinDia.SelectedValue), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)



        If chkVarias.Checked Then
            If fechaInicio < fechaFin Then
                Do
                    tb = New Data.DataTable
                    HayCru = False
                    If WeekdayName(Weekday(fechaInicio)) = Me.ddlDiaSelPer.SelectedItem.Text.ToLower Then

                        obj.AbrirConexion()
                        TbHorariosReg = obj.TraerDataTable("HorarioPE_Consultar", codigo_cup, "F", fechaInicio)
                        obj.CerrarConexion()

                        If TbHorariosReg.Rows.Count Then
                            For i As Integer = 0 To TbHorariosReg.Rows.Count - 1
                                obj.AbrirConexion()
                                TbHorariosCru = obj.TraerDataTable("HorarioPE_ConsultarCru", codigo_cup, fechaInicio, nombre_hor, horaFin)
                                obj.CerrarConexion()
                                If TbHorariosCru.Rows.Count Then
                                    HayCru = True
                                    Exit For
                                End If
                            Next
                        End If

                        If Not HayCru Then
                            obj.AbrirConexion()


                            Dim fechaInicioG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaInicio.Hour, fechaInicio.Minute, 0)
                            Dim fechaFinG As New Date(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day, fechaFin.Hour, fechaFin.Minute, 0)

                            tb = obj.TraerDataTable("HorarioPE_Registrar", día, codigo_cup, nombre_hor, horaFin, usu, fechaInicioG, fechaFinG)
                            obj.CerrarConexion()
                        Else
                            msjVariasSesNO = msjVariasSesNO & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & ", "
                        End If

                        If tb.Rows.Count Then
                            If tb.Rows(0).Item(0).ToString <> "0" Then
                                msjVariasSesSI = msjVariasSesSI & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin & ", "
                            End If
                        End If

                    End If

                    fechaInicio = fechaInicio.AddDays(1)

                Loop While fechaInicio <= fechaFin
            Else
                Response.Write("Fecha Fin debe ser mayor a Fecha Inicio")
                Exit Sub
            End If

            If msjVariasSesNO <> "" Then
                msjVariasSesNO = msjVariasSesNO.Substring(0, msjVariasSesNO.Length - 1)
            End If
            If msjVariasSesSI <> "" Then
                msjVariasSesSI = msjVariasSesSI.Substring(0, msjVariasSesSI.Length - 1)
            End If

            Me.lblMsj.Text = "Se registraron las sesiones: " & msjVariasSesSI & ". No se registraron por cruce: " & msjVariasSesNO



        Else
            obj.AbrirConexion()
            TbHorariosReg = obj.TraerDataTable("HorarioPE_Consultar", codigo_cup, "F", fechaInicio)
            obj.CerrarConexion()
            fechaFin = New Date(CInt(Me.ddlInicioAño.SelectedValue), CInt(Me.ddlInicioMes.SelectedValue), CInt(Me.ddlInicioDia.SelectedValue), CInt(horaFin.Substring(0, 2)), CInt(horaFin.Substring(3, 2)), 0)

            If TbHorariosReg.Rows.Count Then
                For i As Integer = 0 To TbHorariosReg.Rows.Count - 1
                    obj.AbrirConexion()
                    TbHorariosCru = obj.TraerDataTable("HorarioPE_ConsultarCru", codigo_cup, fechaInicio, nombre_hor, horaFin)
                    obj.CerrarConexion()
                    If TbHorariosCru.Rows.Count Then
                        HayCru = True
                        Exit For
                    End If
                Next
            End If
            TbHorariosReg.Dispose()
            TbHorariosCru.Dispose()

            If Not HayCru Then
                'fechaFin = fechaInicio
                'Response.Write(fechaInicio.ToString)
                'Response.Write(fechaFin.ToString)
                obj.AbrirConexion()
                Dim culture As New System.Globalization.CultureInfo("es-ES")
                día = (culture.DateTimeFormat.GetDayName(fechaInicio.DayOfWeek)).ToString.Substring(0, 2).ToUpper
                tb = obj.TraerDataTable("HorarioPE_Registrar", día, codigo_cup, nombre_hor, horaFin, usu, fechaInicio, fechaFin)
                obj.CerrarConexion()
            Else
                Me.lblMsj.Text = "No se registró por cruce: " & fechaInicio.ToString.Substring(0, 10) & " " & nombre_hor & " - " & horaFin
            End If


            If tb.Rows.Count Then
                Me.lblMsj.Text = "Se registró el horario: " & fechaInicio.ToString.Substring(0, 10) & " de " & nombre_hor & " a " & horaFin
                'Else
                '    Me.lblMsj.Text = "Ocurrió un error"
            End If

            End If
            tb.Dispose()
            obj = Nothing

       

    End Sub
End Class


