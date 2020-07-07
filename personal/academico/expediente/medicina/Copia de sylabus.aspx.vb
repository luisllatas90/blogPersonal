﻿
Partial Class copia_de_medicina_sylabus
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            For i As Int16 = 17 To 1 Step -1
                Me.DDLDuracion.Items.Add(i)
            Next

            Me.DDLAccion.Items.Add("Registro de Asistencias")
            Me.DDLAccion.Items.Add("Registro de Observaciones")
            Me.DDLAccion.Items(1).Value = 1
            Me.DDLAccion.Items(2).Value = 2

            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            If Obj.TraerDataTable("ConsultarNotas", "SC", Request.QueryString("codigo_cac"), Request.QueryString("codigo_cup"), Request.QueryString("codigo_per")).Rows(0).Item("mensajeprofesor").ToString.Trim = "" Then
                ConsultarDatos()

                Me.DDLAccion.Items.Add("Registro de Notas")
                Me.DDLAccion.Items(3).Value = 3

                Me.DDLAccion.Items.Add("Consolidado de Notas")
                Me.DDLAccion.Items(4).Value = 4
            Else
                ConsultarDatos()
                Me.DDLAccion.Items.Add("Consolidado de Notas")
                Me.DDLAccion.Items(3).Value = 4
                Me.CmdImportar.Enabled = False
                Me.CmdEvaluaciones.Enabled = False
                Me.CmdActividades.Enabled = False
                Me.CmdGuardar.Enabled = False

                If Me.LblRegistro.Text.Contains("LLENE") = True Then
                    Me.LblRegistro.Text = "Un sylabus solo puede ser ingresado por un profesor principal"
                End If

            End If
        End If
        Me.Form.Attributes.Add("OnSubmit", "return validaenvio();")
        Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
        Me.LblProfesor.Text = Request.QueryString("nombre_per")
        Me.DDLAccion.Attributes.Add("OnChange", "enviacombo()")
        Me.CmdActividades.Attributes.Add("OnClick", "javascript:location.href='actividades.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
        Me.CmdEvaluaciones.Attributes.Add("OnClick", "javascript:location.href='evaluaciones.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
        Me.CmdImportar.Attributes.Add("onclick", "AbrirPopUp('importasylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "','420','380'); return false;")

    End Sub

    Protected Sub CmdActividades_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdActividades.Click
        Response.Redirect("actividades.aspx")

    End Sub

    Protected Sub ConsultarDatos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As New Data.DataTable
        datos = obj.TraerDataTable("MED_ConsultarSylabus", Request.QueryString("codigo_cup"))
        If datos.Rows.Count = 0 Then
            Me.DDLAccion.Enabled = False
            Me.CmdActividades.Enabled = False
            Me.CmdEvaluaciones.Enabled = False
            Me.LblRegistro.Text = "LLENE LOS DATOS QUE SE MUESTRAN A CONTINUACIÓN Y REGISTRE UN SYLABUS."
            Me.LblRegistro.ForeColor = Drawing.Color.Red
        Else
            Me.CmdImportar.Enabled = False
            Me.CmdActividades.Enabled = True
            Me.CmdEvaluaciones.Enabled = True
            Me.DDLAccion.Enabled = True
            Me.LblRegistro.ForeColor = Drawing.Color.Blue
            Me.LblRegistro.Text = "SYLABUS REGISTRADO EL : " & datos.Rows(0).Item("FECHAREGISTRO_SYL").ToString
            With datos.Rows(0)
                Me.HidenCodigoSyl.Value = .Item("codigo_syl").ToString
                Me.DDLDuracion.SelectedValue = CInt(.Item("duracion_syl").ToString)
                Me.TxtImportancia.Text = .Item("importancia_syl").ToString
                Me.TxtRelevancia.Text = .Item("relevancia_syl").ToString
                Me.TxtAplicabilidad.Text = .Item("aplicabilidad_syl").ToString
                Me.TxtContenido.Text = .Item("contenido_syl").ToString
                Me.TxtInvestigacion.Text = .Item("trabajosinv_syl").ToString
                Me.TxtMetodologia.Text = .Item("metodologia_syl").ToString
                Me.TxtEvaluacion.Text = .Item("evaluacion_syl").ToString
                Me.TxtBibliografia.Text = .Item("bibliografia_syl").ToString
            End With

        End If

    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim objdatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.HidenCodigoSyl.Value = "" Then
            Try
                Dim codigo_syl As Integer
                Dim codigo_act As Integer
                objdatos.IniciarTransaccion()
                codigo_syl = objdatos.Ejecutar("MED_RegistrarSylabus", Request.QueryString("codigo_cup"), Me.DDLDuracion.SelectedValue, Me.TxtImportancia.Text.Trim, _
                Me.TxtRelevancia.Text.Trim, Me.TxtAplicabilidad.Text.Trim, Me.TxtContenido.Text.Trim, Me.TxtInvestigacion.Text.Trim, Me.TxtMetodologia.Text.Trim, _
                Me.TxtEvaluacion.Text.Trim, Me.TxtBibliografia.Text.Trim, 0)

                codigo_act = objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Evaluaciones", 0, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)

                If Request.QueryString("codigo_cpf") = 24 Then
                    objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Cognitivo", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                    objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Actitudinal", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                    objdatos.Ejecutar("MED_InsertarEvaluaciones", codigo_syl, "Procedimental", codigo_act, 1, Now, Now, (CInt(True) * -1).ToString, 4, 1, 1, 0)
                End If

                objdatos.TerminarTransaccion()

                Me.HidenCodigoSyl.Value = codigo_syl

                Me.CmdActividades.Attributes.Add("OnClick", "javascript:location.href='actividades.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")
                Me.CmdEvaluaciones.Attributes.Add("OnClick", "javascript:location.href='evaluaciones.aspx?nombre_per=" & Request.QueryString("nombre_per") & "&codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_cur=" & Request.QueryString("nombre_cur") & "&codigo_syl=" & Me.HidenCodigoSyl.Value & "'; return false;")

                ConsultarDatos()
                Response.Write("<script>alert('Los datos se grabaron correctamente.')</script>")
            Catch ex As Exception
                objdatos.AbortarTransaccion()
                Response.Write("<script>alert('Ocurrio un error al procesar los datos, inténtelo nuevamente')</script>")
            End Try
        Else
            Try
                objdatos.IniciarTransaccion()
                objdatos.Ejecutar("MED_ActualizarSylabus", Me.HidenCodigoSyl.Value, Request.QueryString("codigo_cup"), Me.DDLDuracion.SelectedValue, Me.TxtImportancia.Text.Trim, _
                Me.TxtRelevancia.Text.Trim, Me.TxtAplicabilidad.Text.Trim, Me.TxtContenido.Text.Trim, Me.TxtInvestigacion.Text.Trim, Me.TxtMetodologia.Text.Trim, _
                Me.TxtEvaluacion.Text.Trim, Me.TxtBibliografia.Text.Trim)
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
