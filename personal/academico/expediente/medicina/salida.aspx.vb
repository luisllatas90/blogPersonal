
Partial Class medicina_salida
    Inherits System.Web.UI.Page
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim alumnos, datos As New Data.DataTable

        Me.DDLActividades.Attributes.Add("OnChange", "enviacombo(this)")
        Me.LinkRegresar.NavigateUrl = "sylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        Me.cmdGuardar.Enabled = False
        Me.form1.Attributes.Add("OnSubmit", "return validaenvio();")

        If IsPostBack = False Then

            If Request.QueryString("codigo_act") = "" Then
                Me.TxtFecha.Text = Now.Date.ToShortDateString
                ClsFunciones.LlenarListas(Me.DDLActividades, Obj.TraerDataTable("MED_ConsultarActividadesHoy", Me.TxtFecha.Text, Request.QueryString("codigo_syl")), "codigo_Act", "descripcion_act", "-- Seleccione Actividad --")
                'Page.RegisterStartupScript("hidden", "<script>TblAlumnos.style.visibility ='hidden' </script>")
            Else
                If Request.QueryString("nombre_act") <> "" Then
                    Me.TxtFecha.Text = Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act")).Rows(0).Item("fechaini_act")
                End If
            End If
        End If

        If Request.QueryString("nombre_act") <> "" Then
            'Me.LblMensaje.Text = Request.QueryString("nombre_act")
            'Page.RegisterStartupScript("visible", "<script>TblAlumnos.style.visibility ='visible' </script>")

            datos = Obj.TraerDataTable("MED_ConsultarActividadesHoy", Me.TxtFecha.Text, Request.QueryString("codigo_syl"))
            ClsFunciones.LlenarListas(Me.DDLActividades, datos, "codigo_Act", "descripcion_act", "-- Seleccione Actividad --")

            Me.LblMensaje.Text = Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act")).Rows(0).Item("nombre") & " - " & Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act")).Rows(0).Item("fechaini_act")
            If datos.Rows.Count > 0 Then
                alumnos = Obj.TraerDataTable("MED_ConsultarAlumnosCurso", Request.QueryString("codigo_act"), Request.QueryString("codigo_cup"))
                Me.cmdGuardar.Enabled = True
            End If
            Dim Codigo_alu As Integer
            Dim Contador As Integer
            Codigo_alu = 0
            Contador = 1
            If alumnos.Rows.Count > 0 Then
                For i As Int16 = 0 To alumnos.Rows.Count - 1
                    If Codigo_alu <> CInt(alumnos.Rows(i).Item("codigo_Alu").ToString) Then
                        Codigo_alu = CInt(alumnos.Rows(i).Item("codigo_Alu").ToString)
                        Dim c As Control = LoadControl("salida.ascx")
                        CType(c, medicina_salidas).CodigoDMA = CInt(alumnos.Rows(i).Item("codigo_dma").ToString)
                        CType(c, medicina_salidas).Numero = Contador
                        CType(c, medicina_salidas).Nombres = alumnos.Rows(i).Item("codigoUniver_Alu").ToString + " - " + alumnos.Rows(i).Item("alumno").ToString
                        CType(c, medicina_salidas).ID = "Ctrl" & Contador.ToString
                        CType(c, medicina_salidas).observaciones = alumnos.Rows(i).Item("observacion_dact").ToString
                        If alumnos.Rows(i).Item("tipoasistencia_act").ToString = "A" Then
                            CType(c, medicina_salidas).HoraIngreso = CDate(alumnos.Rows(i).Item("horaingreso_dact")).ToShortTimeString  'Now.ToShortTimeString
                            CType(c, medicina_salidas).Asistio = True
                            CType(c, medicina_salidas).Condicion = "ASISTIO"
                            CType(c, medicina_salidas).CondicionColor = Drawing.Color.Green
                        ElseIf alumnos.Rows(i).Item("tipoasistencia_act").ToString = "T" Then
                            CType(c, medicina_salidas).HoraIngreso = CDate(alumnos.Rows(i).Item("horaingreso_dact")).ToShortTimeString  'Now.ToShortTimeString
                            CType(c, medicina_salidas).Asistio = True
                            CType(c, medicina_salidas).Condicion = "TARDANZA"
                            CType(c, medicina_salidas).CondicionColor = Drawing.Color.Goldenrod
                        Else
                            If alumnos.Rows(i).Item("horaingreso_dact") Is System.DBNull.Value Then
                                CType(c, medicina_salidas).HoraIngreso = "---"
                                CType(c, medicina_salidas).Condicion = "FALTO"
                                CType(c, medicina_salidas).CondicionColor = Drawing.Color.Red
                            Else
                                CType(c, medicina_salidas).HoraIngreso = CDate(alumnos.Rows(i).Item("horaingreso_dact")).ToShortTimeString
                                CType(c, medicina_salidas).Condicion = "FALTO"
                                CType(c, medicina_salidas).CondicionColor = Drawing.Color.Red
                            End If

                        End If
                        If Contador Mod 2 = 0 Then
                            CType(c, medicina_salidas).ColorFila = "#FEFFE1"
                        Else
                            CType(c, medicina_salidas).ColorFila = "#F9F5F2"
                        End If
                        Contador = Contador + 1
                        Me.form1.Controls.Add(c)
                    End If
                Next
            End If
            Me.HidenAlumnos.Value = Contador - 1
        End If

    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim ObjAlu As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            ObjAlu.IniciarTransaccion()
            'ObjAlu.Ejecutar("MED_ActualizaMarcado", "IN", CInt(Request.QueryString("codigo_Act")))
            For i As Int16 = (Me.form1.Controls.Count - Me.HidenAlumnos.Value) To Me.form1.Controls.Count - 1
                Dim c As Control
                c = Form.Controls.Item(i)
                If CType(c, medicina_salidas).observaciones.ToString <> "" Then
                    ObjAlu.Ejecutar("MED_RegistraObservaciones", CInt(Request.QueryString("codigo_act")), CInt(CType(c, medicina_salidas).CodigoDMA), "A", CInt(Request.QueryString("codigo_per")), CType(c, medicina_salidas).observaciones.ToString)
                End If
            Next
            ObjAlu.TerminarTransaccion()
            Response.Write("<script>alert('Se guardaron los datos correctamente.')</script>")
            Response.Redirect("salida.aspx?" & Page.ClientQueryString)
        Catch ex As Exception
            ObjAlu.AbortarTransaccion()
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
    End Sub

    Protected Sub ImgVer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgVer.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        ClsFunciones.LlenarListas(Me.DDLActividades, Obj.TraerDataTable("MED_ConsultarActividadesHoy", Me.TxtFecha.Text, Request.QueryString("codigo_syl")), "codigo_Act", "descripcion_act", "-- Seleccione Actividad --")
    End Sub

End Class
