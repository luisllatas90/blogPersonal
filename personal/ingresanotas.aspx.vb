
Partial Class medicina_ingresanotas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim alumnos, datos As New Data.DataTable, dtsBotones As New data.datatable
        Dim ConsideraNota As String

        Me.form1.Attributes.Add("OnSubmit", "return validaenvio();")

        If IsPostBack = False Then

            Me.CmdRegresar.Attributes.Add("OnClick", "javascript:location.href='" & "registraractividades.aspx?codigo_cup=" & Request.QueryString("codigo_cup") & "&codigo_syl=" & Request.QueryString("codigo_syl") & "&pag=guardar'" & "; return false;")

            If Request.QueryString("codigo_act") <> "" Then
                Dim HoraIni As String
                Obj.AbrirConexion()
                datos = Obj.TraerDataTable("ConsultarCursoProgramado", 10, Request.QueryString("codigo_cup"), 0, 0, 0)
                Me.LblCurso.Text = datos.Rows(0).Item("nombre_cur") & " - Grupo (" & datos.Rows(0).Item("grupohor_cup") & ")"
                datos.Dispose()
                datos = Obj.TraerDataTable("MED_ConsultarActividadDescripcion", Request.QueryString("codigo_act"))
                Obj.CerrarConexion()
                Me.LblActividad.Text = datos.Rows(0).Item("nombre").ToString
                Me.LblFechaIniTer.Text = CDate(datos.Rows(0).Item("fechaini_act")).ToShortDateString & "  - Inicio : " & CDate(datos.Rows(0).Item("fechaini_act")).ToShortTimeString & " Término : " & CDate(datos.Rows(0).Item("fechafin_act")).ToShortTimeString
                ConsideraNota = datos.Rows(0).Item("considerarnota_act").ToString
                HoraIni = CDate(datos.Rows(0).Item("fechaini_act")).ToShortTimeString
                HddConsideraAsistencia.Value = IIf(datos.Rows(0).Item("ConsiderarAsistencia_act") = True, 1, 0)

                Me.HddFecha.Value = CDate(datos.Rows(0).Item("fechaini_act")).ToShortDateString

                If HoraIni.Substring(6, 4) = "p.m." Then
                    If CInt(HoraIni.Substring(0, 2)) < 12 Then
                        Me.HddHoraIni.Value = (CInt(HoraIni.Substring(0, 2)) + 12).ToString.PadLeft(2, "0")
                        Me.HddMinIni.Value = HoraIni.Substring(3, 2)
                    Else
                        Me.HddHoraIni.Value = (CInt(HoraIni.Substring(0, 2))).ToString.PadLeft(2, "0")
                        Me.HddMinIni.Value = HoraIni.Substring(3, 2)
                    End If
                Else
                    Me.HddHoraIni.Value = (CInt(HoraIni.Substring(0, 2))).ToString.PadLeft(2, "0")
                    Me.HddMinIni.Value = HoraIni.Substring(3, 2)
                End If
            End If
            '----------------------------------------------------------------
            Obj.AbrirConexion()
            dtsBotones = Obj.TraerDataTable("ACAD_RegistrarInicioFinClase", "C", Request.QueryString("codigo_act"))
            Obj.CerrarConexion()

            If CDate(datos.Rows(0).Item("fechaini_act")).ToShortDateString <> CDate(now()).ToShortDateString Then

                If dtsBotones.rows(0).Item("inicio_Act") = "01/01/1900 12:00:00 a.m." Then
                    cmdInicio.Visible = True
                Else
                    Response.Write("Inicio: " & dtsBotones.rows(0).Item("inicio_Act"))
                    cmdInicio.Visible = False
                End If
                If dtsBotones.rows(0).Item("fin_Act") = "01/01/1900 12:00:00 a.m." Then
                    cmdFin.Visible = True
                Else
                    Response.Write("<br>Fin: " & dtsBotones.rows(0).Item("fin_Act"))
                    cmdFin.Visible = False
                End If
            Else
                cmdInicio.Visible = False
                cmdFin.Visible = False
            End If

            'validar que sólo se presente para profesores por horas
            Obj.AbrirConexion()
            dtsBotones = Obj.TraerDataTable("ACAD_RegistrarInicioFinClase", "D", Request.QueryString("codigo_act"))
            Obj.CerrarConexion()
            ' Response.Write(dtsBotones.Rows(0).Item("codigo_Ded"))
            If dtsBotones.Rows(0).Item("codigo_Ded") <> 3 Then
                cmdInicio.visible = False
                cmdFin.visible = False
            End If
            '----------------------------------------------------------------
        End If

        If Request.QueryString("codigo_act") <> "" Then
            Dim Codigo_alu As Integer
            Dim Contador As Integer

            Codigo_alu = 0
            Contador = 1

            If HddConsideraAsistencia.Value = 1 Then
                ChkTodos.Enabled = True
            Else
                ChkTodos.Enabled = False
            End If
            Obj.AbrirConexion()
            alumnos = Obj.TraerDataTable("MED_ConsultarAlumnosCurso", Request.QueryString("codigo_act"), Request.QueryString("codigo_cup"))
            Obj.CerrarConexion()
            Me.ChkTodos.Attributes.Add("OnClick", "marcartodos(this);")
            If alumnos.Rows.Count > 0 Then

                For i As Int16 = 0 To alumnos.Rows.Count - 1
                    If Codigo_alu <> CInt(alumnos.Rows(i).Item("codigo_Alu").ToString) Then
                        Codigo_alu = CInt(alumnos.Rows(i).Item("codigo_Alu").ToString)
                        Dim c As Control = LoadControl("controles/CtrlAsistencia.ascx")

                        If ConsideraNota = "NO" Then
                            CType(c, CtrlAsistencia).ActivarNota = False
                        Else
                            CType(c, CtrlAsistencia).ActivarNota = True
                        End If

                        CType(c, CtrlAsistencia).CodigoDMA = CInt(alumnos.Rows(i).Item("codigo_dma").ToString)
                        CType(c, CtrlAsistencia).Numero = Contador
                        If alumnos.Rows(i).Item("estadoDeuda_Alu") = 1 Then
                            CType(c, CtrlAsistencia).Nombres = alumnos.Rows(i).Item("codigoUniver_Alu").ToString + " - " + alumnos.Rows(i).Item("alumno").ToString + " <font color='red'>(Tiene deuda)</font>"
                        Else
                            CType(c, CtrlAsistencia).Nombres = alumnos.Rows(i).Item("codigoUniver_Alu").ToString + " - " + alumnos.Rows(i).Item("alumno").ToString
                        End If
                        CType(c, CtrlAsistencia).ID = "Ctrl" & Contador.ToString
                        CType(c, CtrlAsistencia).Observaciones = alumnos.Rows(i).Item("observacion_dact").ToString

                        If alumnos.Rows(i).Item("asistio").ToString = "S" Then
                            CType(c, CtrlAsistencia).ActivarIngreso = True
                            CType(c, CtrlAsistencia).Ingreso_Hora = CDate(alumnos.Rows(i).Item("horaingreso_dact")).Hour.ToString
                            CType(c, CtrlAsistencia).Ingreso_Min = CDate(alumnos.Rows(i).Item("horaingreso_dact")).Minute.ToString
                            If alumnos.Rows(i).Item("califnum_dact") IsNot System.DBNull.Value Then
                                CType(c, CtrlAsistencia).Notas = FormatNumber(alumnos.Rows(i).Item("califnum_dact").ToString, 2, TriState.False)
                            End If
                            CType(c, CtrlAsistencia).Estado = Mid(alumnos.Rows(i).Item("tipoasistencia_act").ToString, 1, 1)
                            CType(c, CtrlAsistencia).Asistio = True
                        Else
                            If HddConsideraAsistencia.Value = 1 Then
                                CType(c, CtrlAsistencia).BloqueoDatos = False
                                If alumnos.Rows(i).Item("califnum_dact") IsNot System.DBNull.Value Then
                                    CType(c, CtrlAsistencia).Notas = FormatNumber(alumnos.Rows(i).Item("califnum_dact").ToString, 2, TriState.False)
                                End If
                            Else
                                CType(c, CtrlAsistencia).BloqueoDatosAsistencia = False
                                If alumnos.Rows(i).Item("califnum_dact") IsNot System.DBNull.Value Then
                                    CType(c, CtrlAsistencia).Notas = FormatNumber(alumnos.Rows(i).Item("califnum_dact").ToString, 2, TriState.False)
                                End If
                            End If
                        End If
                        CType(c, CtrlAsistencia).Ingreso_Hora = Me.HddHoraIni.Value
                        CType(c, CtrlAsistencia).Ingreso_Min = Me.HddMinIni.Value


                        If Contador Mod 2 = 0 Then
                            CType(c, CtrlAsistencia).ColorFila = "#FEFFE1"
                        Else
                            CType(c, CtrlAsistencia).ColorFila = "#F9F5F2"
                        End If
                        Contador = Contador + 1

                        Me.TblCelda.Controls.Add(c)

                    End If

                Next
            End If
            Me.HidenAlumnos.Value = Contador - 1
        End If

    End Sub

    Protected Sub cmdGuardarArriba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarArriba.Click, cmdGuardarAbajo.Click
        Dim ObjAlu As New ClsConectarDatos
        ObjAlu.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim HoraIngreso, fecha As DateTime
        Dim Nota As String

        Try
            ObjAlu.IniciarTransaccion()

            If ChkTodos.Enabled = False Then
                ObjAlu.Ejecutar("MED_ActualizaMarcado", "EV", CInt(Request.QueryString("codigo_act")))
            Else
                ObjAlu.Ejecutar("MED_ActualizaMarcado", "IN", CInt(Request.QueryString("codigo_act")))
            End If

            For i As Int16 = 0 To Me.TblCelda.Controls.Count - 1
                Dim c As Control
                c = Me.TblCelda.Controls.Item(i)
                If (CType(c, CtrlAsistencia).ActivarIngreso = True And CType(c, CtrlAsistencia).Asistio = True) Or (ChkTodos.Enabled = False) Then
                    fecha = CDate(Me.HddFecha.Value)
                    HoraIngreso = CDate(fecha.Day.ToString & "/" & fecha.Month.ToString & "/" & fecha.Year.ToString & " " & CType(c, CtrlAsistencia).Ingreso_Hora.ToString & ":" & CType(c, CtrlAsistencia).Ingreso_Min.ToString)

                    Nota = "-1"
                    If CType(c, CtrlAsistencia).Notas.ToString <> "" Then
                        Nota = CType(c, CtrlAsistencia).Notas
                    End If

                    If ChkTodos.Enabled = False Then
                        ObjAlu.Ejecutar("MED_InsertaAsistenciaNota", CInt(Request.QueryString("codigo_act")), CInt(CType(c, CtrlAsistencia).CodigoDMA), DBNull.Value, CType(c, CtrlAsistencia).Estado, IIf(Nota = "-1", System.DBNull.Value, CDbl(Nota)), CInt(Request.QueryString("codigo_per")), CType(c, CtrlAsistencia).Observaciones)
                    Else
                        ObjAlu.Ejecutar("MED_InsertaAsistenciaNota", CInt(Request.QueryString("codigo_act")), CInt(CType(c, CtrlAsistencia).CodigoDMA), HoraIngreso, CType(c, CtrlAsistencia).Estado, IIf(Nota = "-1", System.DBNull.Value, CDbl(Nota)), CInt(Request.QueryString("codigo_per")), CType(c, CtrlAsistencia).Observaciones)
                    End If

                End If
            Next
            ObjAlu.TerminarTransaccion()

            'Response.Write("<script>alert('Se guardaron los datos correctamente.')</script>")

        Catch ex As Exception
            ObjAlu.AbortarTransaccion()
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
        ObjAlu = Nothing
    End Sub

  
    Protected Sub cmdInicio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdInicio.Click
        Dim ObjAlu As New ClsConectarDatos
        ObjAlu.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            ObjAlu.IniciarTransaccion()
            ObjAlu.Ejecutar("ACAD_RegistrarInicioFinClase", "I", CInt(Request.QueryString("codigo_act")))
            ObjAlu.TerminarTransaccion()
            cmdInicio.Visible = False
        Catch ex As Exception
            ObjAlu.AbortarTransaccion()
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
        ObjAlu = Nothing
    End Sub

    Protected Sub cmdFin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdFin.Click
        Dim ObjAlu As New ClsConectarDatos
        ObjAlu.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            ObjAlu.IniciarTransaccion()
            ObjAlu.Ejecutar("ACAD_RegistrarInicioFinClase", "F", CInt(Request.QueryString("codigo_act")))
            ObjAlu.TerminarTransaccion()
            cmdFin.Visible = False
        Catch ex As Exception
            ObjAlu.AbortarTransaccion()
            Response.Write(ex.Message)
            Response.Write("<script>alert('Ocurrio un error al procesar los datos, intentelo nuevamente')</script>")
        End Try
        ObjAlu = Nothing
    End Sub
End Class
