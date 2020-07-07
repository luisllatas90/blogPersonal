
Partial Class medicina_administrador_Asistencias
    Inherits System.Web.UI.Page
    Private codigo_sem As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim alumnos As New Data.DataTable
        codigo_sem = Request.QueryString("codigo_sem")
        Me.LinkRegresar.NavigateUrl = "programacion.aspx?codigo_sem=" & codigo_sem & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        Me.cmdGuardar.Enabled = False
        Me.form1.Attributes.Add("OnSubmit", "return validaenvio();")

        If Request.QueryString("codigo_Act") <> "" Then
            Me.LblMensaje.Text = Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act")).Rows(0).Item("nombre") & " - " & Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act")).Rows(0).Item("fechaini_act")

            alumnos = Obj.TraerDataTable("MED_ConsultarAlumnosCurso", Request.QueryString("codigo_act"), Request.QueryString("codigo_cup"))
            Me.cmdGuardar.Enabled = True
            ' Me.ChkTodos.Attributes.Add("OnClick", "marcartodos(this);")

            Dim Codigo_alu As Integer
            Dim Contador As Integer
            Codigo_alu = 0
            Contador = 1

            For i As Int16 = 0 To alumnos.Rows.Count - 1
                If Codigo_alu <> CInt(alumnos.Rows(i).Item("codigo_Alu").ToString) Then
                    Codigo_alu = CInt(alumnos.Rows(i).Item("codigo_Alu").ToString)
                    Dim c As Control = LoadControl("../ingreso.ascx")
                    CType(c, medicina_ingreso).CodigoDMA = CInt(alumnos.Rows(i).Item("codigo_dma").ToString)
                    CType(c, medicina_ingreso).Numero = Contador
                    CType(c, medicina_ingreso).Nombres = alumnos.Rows(i).Item("codigoUniver_Alu").ToString + " - " + alumnos.Rows(i).Item("alumno").ToString
                    CType(c, medicina_ingreso).ID = "Ctrl" & Contador.ToString
                    CType(c, medicina_ingreso).Ingreso_Asistencia = alumnos.Rows(i).Item("tipoasistencia_act").ToString
                    If alumnos.Rows(i).Item("tipoasistencia_act").ToString = "Asistió" Then
                        CType(c, medicina_ingreso).Color_Asistencia = Drawing.Color.DarkGreen
                    ElseIf alumnos.Rows(i).Item("tipoasistencia_act").ToString = "Faltante" Then
                        CType(c, medicina_ingreso).Color_Asistencia = Drawing.Color.Red
                    Else
                        CType(c, medicina_ingreso).Color_Asistencia = Drawing.Color.DarkOrange
                    End If
                    If alumnos.Rows(i).Item("asistio").ToString = "S" Then
                        CType(c, medicina_ingreso).ActivarIngreso = False
                        CType(c, medicina_ingreso).Ingreso_Hora = CDate(alumnos.Rows(i).Item("horaingreso_dact")).Hour.ToString
                        CType(c, medicina_ingreso).Ingreso_Min = CDate(alumnos.Rows(i).Item("horaingreso_dact")).Minute.ToString
                        'CType(c, medicina_ingreso).observaciones = alumnos.Rows(i).Item("observacion_dact").ToString
                        'CType(c, medicina_ingreso).Asistio = True
                    Else
                        CType(c, medicina_ingreso).Ingreso_Hora = Hour(Now)
                        CType(c, medicina_ingreso).Ingreso_Min = Minute(Now)
                    End If
                    CType(c, medicina_ingreso).CheckVisible = False
                    If Contador Mod 2 = 0 Then
                        CType(c, medicina_ingreso).ColorFila = "#FEFFE1"
                    Else
                        CType(c, medicina_ingreso).ColorFila = "#F9F5F2"
                    End If
                    Contador = Contador + 1
                    Me.form1.Controls.Add(c)
                End If
            Next
            Me.HidenAlumnos.Value = Contador - 1
        End If

    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjAlu As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim HoraIngreso As DateTime
        Try
            ObjAlu.IniciarTransaccion()
            ObjAlu.Ejecutar("MED_ActualizaMarcado", "IN", CInt(Request.QueryString("codigo_Act")))
            For i As Int16 = (Me.form1.Controls.Count - Me.HidenAlumnos.Value) To Me.form1.Controls.Count - 1
                Dim c As Control
                c = Form.Controls.Item(i)
                If CType(c, medicina_ingreso).ActivarIngreso = True And CType(c, medicina_ingreso).Asistio = True Then
                    HoraIngreso = CDate(Now.Day.ToString & "/" & Now.Month.ToString & "/" & Now.Year.ToString & " " & CType(c, medicina_ingreso).Ingreso_Hora.ToString & ":" & CType(c, medicina_ingreso).Ingreso_Min.ToString)
                    ObjAlu.Ejecutar("MED_InsertaAsistencia", CInt(Request.QueryString("codigo_act")), CInt(CType(c, medicina_ingreso).CodigoDMA), "A", HoraIngreso, CInt(Request.QueryString("codigo_per")), "")
                End If
            Next
            ObjAlu.TerminarTransaccion()
            Response.Write("<script>alert('Se guardaron los datos correctamente.')</script>")
            Response.Redirect("evaluacion.aspx?" & Page.ClientQueryString)
        Catch ex As Exception
            ObjAlu.AbortarTransaccion()
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

End Class
