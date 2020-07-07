
Partial Class medicina_sylabus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Request.ServerVariables("LOGON_USER").ToString.ToUpper <> "USAT\HZELADA" Then
        '    Response.Write("<CENTER>EN ESTOS MOMENTOS EL MODULO SE ENCUENTRA EN MANTENIMIENTO <BR> ESPERAMOS SU COMPRENSIÓN</CENTER>")
        '    Me.Form.Controls.Clear()
        '    Exit Sub
        'End If


        If IsPostBack = False Then
            'For i As Int16 = 17 To 1 Step -1
            '    Me.DDLDuracion.Items.Add(i)
            'Next
            'Me.DDLAccion.Items.Add("Registro de Asistencias")
            'Me.DDLAccion.Items.Add("Registro de Observaciones")
            'Me.DDLAccion.Items(1).Value = 1
            'Me.DDLAccion.Items(2).Value = 2

            Dim Obj As New ClsConectarDatos
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Obj.AbrirConexion()
            If Obj.TraerDataTable("ConsultarNotas", "SC", Request.QueryString("codigo_cac"), Request.QueryString("codigo_cup"), Request.QueryString("codigo_per")).Rows(0).Item("mensajeprofesor").ToString.Trim = "" Then
                ConsultarDatos()
                'Me.DDLAccion.Items.Add("Registro de Notas")
                'Me.DDLAccion.Items(3).Value = 3
                'Me.DDLAccion.Items.Add("Consolidado de Notas")
                'Me.DDLAccion.Items(4).Value = 4
            Else
                ConsultarDatos()
                'Me.DDLAccion.Items.Add("Consolidado de Notas")
                'Me.DDLAccion.Items(3).Value = 4
                'Me.CmdImportar.Enabled = False
                'Me.CmdEvaluaciones.Enabled = False
                Me.CmdActividades.Enabled = False
                Me.CmdGuardar.Enabled = False

                If Me.LblRegistro.Text.Contains("LLENE") = True Then
                    Me.LblRegistro.Text = "Un sylabus solo puede ser ingresado por un profesor principal"
                End If

            End If
            Obj.CerrarConexion()
        End If
        Me.Form.Attributes.Add("OnSubmit", "return validaenvio();")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        'Me.DDLAccion.Attributes.Add("OnChange", "enviacombo()")
        Me.CmdActividades.Attributes.Add("OnClick", "javascript:location.href='registraractividades.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
        'Me.CmdEvaluaciones.Attributes.Add("OnClick", "javascript:location.href='evaluaciones.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
        'Me.CmdImportar.Attributes.Add("onclick", "AbrirPopUp('importasylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "','420','380'); return false;")

    End Sub

    Protected Sub CmdActividades_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdActividades.Click
        'Response.Redirect("actividades.aspx")
        Response.Redirect("registraractividades.aspx")

    End Sub

    Protected Sub ConsultarDatos()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Dim datos As New Data.DataTable
        obj.AbrirConexion()
        datos = obj.TraerDataTable("MED_ConsultarSylabus", "CU", Request.QueryString("codigo_cup"))
        obj.CerrarConexion()
        If datos.Rows.Count <> 0 Then
            With datos.Rows(0)
                Me.LblNombreCurso.Text = .Item("nombre_Cur").ToString
                Me.LblGrupoHorario.Text = .Item("grupoHor_Cup").ToString
                Me.LblUsuarioRegistro.Text = Request.QueryString("nombre_per") '.Item("apellidoPat_Per").ToString + " " + .Item("apellidoMat_Per").ToString + " " + .Item("Nombres_Per").ToString
                Me.LblArchivoSylabus.text = "<a href='../../../../silabos/" & .Item("descripcion_Cac") & "/" & Request.QueryString("codigo_cup") & ".zip'>Descargar</a>"

                If .Item("fechasilabo_Cup") Is System.DBNull.Value Then
                    'Me.DDLAccion.Enabled = False
                    Me.CmdActividades.Enabled = False
                    'Me.CmdEvaluaciones.Enabled = False
                    Me.CmdGuardar.Enabled = False
                    Me.LblRegistro.Text = "NO SE HA REGISTRADO UN SYLABUS, COMUNIQUESE CON SU DIRECTOR DE ESCUELA."
                    Me.LblRegistro.ForeColor = Drawing.Color.Red
                    Exit Sub
                Else
                    Me.LblFechaSylabus.Text = .Item("fechasilabo_Cup").ToString
                End If
            End With
        End If

        datos.Dispose()
        obj.AbrirConexion()
        datos = obj.TraerDataTable("MED_ConsultarSylabus", "SI", Request.QueryString("codigo_cup"))
        obj.CerrarConexion()

        If datos.Rows.Count = 0 Then
            'Me.DDLAccion.Enabled = False
            Me.CmdActividades.Enabled = False
            'Me.CmdEvaluaciones.Enabled = False
            Me.CmdGuardar.Enabled = True
            Me.LblRegistro.Text = "FALTA ACTIVAR EL CURSO PARA LLENADO DE ASISTENCIA Y NOTAS."
            Me.LblRegistro.ForeColor = Drawing.Color.Red
        Else
            'Me.CmdImportar.Enabled = False
            Me.CmdActividades.Enabled = True
            'Me.CmdEvaluaciones.Enabled = True
            'Me.DDLAccion.Enabled = True
            Me.CmdGuardar.Enabled = False
            Me.LblRegistro.ForeColor = Drawing.Color.Blue
            Me.LblRegistro.Text = "CURSO ACTIVADO EL " & datos.Rows(0).Item("FECHAREGISTRO_SYL").ToString
            With datos.Rows(0)
                Me.HidenCodigoSyl.Value = .Item("codigo_syl").ToString
            End With
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objdatos As New ClsConectarDatos
        objdatos.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If Me.HidenCodigoSyl.Value = "" Then
            Try
                Dim codigo_syl As Integer
                Dim codigo_act As Integer
                Dim ValoresDevueltos(1) As Integer

                objdatos.IniciarTransaccion()
                objdatos.Ejecutar("MED_RegistrarSylabus", Request.QueryString("codigo_cup"), "", "", "", "", "", "", "", "", "", 0).copyto(ValoresDevueltos, 0)
                codigo_syl = ValoresDevueltos(0)
                'codigo_act = objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Evaluaciones", 0, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                'If Request.QueryString("codigo_cpf") = 24 Then
                'objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Cognitivo", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                'objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Actitudinal", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                'objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Procedimental", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                'End If
                objdatos.TerminarTransaccion()

                Me.HidenCodigoSyl.Value = codigo_syl
                Me.CmdActividades.Attributes.Add("OnClick", "javascript:location.href='registraractividades.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
                'Me.CmdEvaluaciones.Attributes.Add("OnClick", "javascript:location.href='evaluaciones.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")

                ConsultarDatos()
                Response.Write("<script>alert('Los datos se grabaron correctamente.')</script>")
            Catch ex As Exception
                objdatos.AbortarTransaccion()
                Response.Write("<script>alert('Ocurrio un error al procesar los datos, inténtelo nuevamente')</script>")
            End Try
        Else
            Try
                objdatos.IniciarTransaccion()
                objdatos.Ejecutar("MED_ActualizarSylabus", Me.HidenCodigoSyl.Value, Request.QueryString("codigo_cup"), "", "", "", "", "", "", "", "", "")
                objdatos.TerminarTransaccion()
                ConsultarDatos()
                Response.Write("<script>alert('Los datos se actualizaron correctamente.')</script>")
            Catch ex As Exception
                objdatos.AbortarTransaccion()
                Response.Write("<script>alert('Ocurrio un error al actualizar los datos, inténtelo nuevamente')</script>")
            End Try
        End If

    End Sub

End Class
