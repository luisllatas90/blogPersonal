﻿Imports System.IO
Imports System.Security.Cryptography

Partial Class frmprogramarcursos
    Inherits System.Web.UI.Page
    Dim permisos As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable

            Dim codigo_tfu As Int16
            Dim codigo_usu As Integer
            Dim modulo As Integer

            codigo_tfu = Request.QueryString("ctf")
            codigo_usu = Session("id_per")
            modulo = Request.QueryString("mod")
            hdte.value = Encriptar(modulo)
            '=================================
            'Permisos por Carrera profesional
            '=================================
            tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, codigo_tfu, codigo_usu)

            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_cpf, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            tbl.Dispose()
            obj = Nothing

            Me.txtInicio.Attributes.Add("OnKeyDown", "return false")
            Me.txtFin.Attributes.Add("OnKeyDown", "return false")
            Me.txtRetiro.Attributes.Add("OnKeyDown", "return false")
            Me.txtGrupos.Attributes.Add("onKeyPress", "validarnumero()")
            Me.txtVacantes.Attributes.Add("onKeyPress", "validarnumero()")
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        'Try
        LimpiarDatos(True)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.grwCursosPlan.DataSource = obj.TraerDataTable("ConsultarCursoProgramado", 0, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, Me.dpCiclo_cur.SelectedValue, "")
        Me.grwCursosPlan.DataBind()
        obj = Nothing
        'Catch ex As Exception
        '    Page.RegisterStartupScript("Error", "<script>alert('Por el momento está demorando procesar lo solicitado.\n Por favor cierre su navegador de Internet e ingrese nuevamente')</script>")
        'End Try
    End Sub
    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cpf.SelectedIndexChanged
        LimpiarDatos(False)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
	if Me.dpCodigo_cpf.SelectedValue<>25 then
        ClsFunciones.LlenarListas(Me.dpCodigo_pes, obj.TraerDataTable("ConsultarPlanEstudio", "AC", Me.dpCodigo_cpf.SelectedValue, 0), "codigo_pes", "descripcion_pes")
	else
            ClsFunciones.LlenarListas(Me.dpCodigo_pes, obj.TraerDataTable("ConsultarDatosProgramaEspecial", 6, Request.QueryString("ctf"), Me.dpCodigo_cpf.SelectedValue, Session("id_per"), 0), "codigo_pes", "descripcion_pes")
	end if

        ClsFunciones.LlenarListas(Me.dpCiclo_cur, obj.TraerDataTable("ConsultarPlanEstudio", "CI", Me.dpCodigo_cpf.SelectedValue, 0), "ciclo_cur", "ciclo_cur", "--Todos--")
        obj = Nothing
        cmdBuscar.Enabled = dpCodigo_pes.Items.Count > 0
    End Sub
    Protected Sub grwCursosPlan_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCursosPlan.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this)")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this)")
        End If
    End Sub
    Protected Sub grwCursosPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwCursosPlan.SelectedIndexChanged

        Try

        
            Dim codigo_test As String = Desencriptar(Me.hdte.value)
            'Response.Write("codigo_test: " & codigo_test)
            'Asignar Codigo_cur
            Me.hdCodigo_Cur.Value = Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString

            '=================================
            'Mostrar datos del curso
            '=================================
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tblcurso As Data.DataTable

            tblcurso = obj.TraerDataTable("ConsultarCursoPlan", 10, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, 0)
            Me.lblcodigo.Text = tblcurso.Rows(0).Item("identificador_cur")
            Me.lblasignatura.Text = tblcurso.Rows(0).Item("nombre_cur")

            Me.lblCrd.Text = tblcurso.Rows(0).Item("creditos_cur")
            Me.lblDpto.Text = tblcurso.Rows(0).Item("nombre_dac")
            Me.hdcodigo_dac.Value = tblcurso.Rows(0).Item("codigo_dac")
            Me.lblTipo.Text = tblcurso.Rows(0).Item("tipo_cur")
            Me.lblHT.Text = tblcurso.Rows(0).Item("horasTeo_Cur")
            Me.lblHP.Text = tblcurso.Rows(0).Item("horasPra_Cur")
            Me.lblHL.Text = tblcurso.Rows(0).Item("horasLab_Cur")
            Me.lblHA.Text = tblcurso.Rows(0).Item("horasAse_Cur")
            Me.lblTotal.Text = tblcurso.Rows(0).Item("totalHoras_Cur")
            'Me.hdTotalHoras.Value = tblcurso.Rows(0).Item("totalHoras_Cur")
            Me.lblelectivo.Visible = tblcurso.Rows(0).Item("electivo_Cur")
            tblcurso.Dispose()

            '******************************************************
            'Validar los permisos para horarios
            '******************************************************
            Dim DataPermisos As Data.DataTable

            DataPermisos = obj.TraerDataTable("ValidarPermisoAccionesEnProcesoMatriculav2", "0", Me.dpCodigo_cac.SelectedValue, Session("id_per"), "cursoprogramado", codigo_test)
            If DataPermisos.Rows.Count > 0 Then
                hddAgregar.Value = CBool(DataPermisos.Rows(0).Item("agregar_acr"))
                hddModificar.Value = CBool(DataPermisos.Rows(0).Item("modificar_acr"))
                hddEliminar.Value = CBool(DataPermisos.Rows(0).Item("eliminar_acr"))
            Else
                'permisos = False
                hddAgregar.Value = False
                hddModificar.Value = False
                hddEliminar.Value = False
            End If
            Me.cmdAgregar.Enabled = Me.hddAgregar.Value
            'Me.cmdGuardar.Enabled = Me.hddModificar.Value
            Me.cmdGuardar.Enabled = True
            '=================================
            'Mostrar/Ocultar Frames
            '=================================
            Me.fraDetalleGrupoHorario.Visible = False
            Me.fraDetalleCurso.Visible = True
            Me.fraGruposProgramados.Visible = True
            Me.lblProfesores.Text = "Sugerir Profesores para el Curso, al Dpto. de " & Me.lblDpto.Text
            Me.lblMensajeGrupos.Visible = False
            '=================================
            'Cargar Grupos Programados
            '=================================
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", 2, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, Me.dpCodigo_cpf.SelectedValue)
            Me.grwGruposProgramados.DataBind()

            If Me.grwGruposProgramados.Rows.Count = 0 Then
                Me.lblGrupos.Text = "Programar Nuevos Grupos Horario"
                Me.cmdAgregar.Text = "Nuevo"

            Else
                Me.lblGrupos.Text = "Lista de Grupos Horario Programados"
                Me.cmdAgregar.Text = "Añadir"

            End If
            Me.Button1.Enabled = False
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
    End Sub
    Private Sub CargarDetalleGrupoHorario(ByVal tipo As String, ByVal ID As String)


        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tblcurso As Data.DataTable

        'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('CAR_ConsultarProcesoProgramacionAcademica " & ID & "');", True)

        'Dim a As String = Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString()

        'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('ID = " & ID & "');", True)
        'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('datakey = " & a & "');", True)

        'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('" & "CAR_ConsultarProcesoProgramacionAcademica " & tipo & ", " & Me.dpCodigo_pes.SelectedValue & ", " & ID & ", " & Me.dpCodigo_cac.SelectedValue & ", " & 0 & "');", True)
        tblcurso = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", tipo, Me.dpCodigo_pes.SelectedValue, ID, Me.dpCodigo_cac.SelectedValue, 0)



        Me.lblFecha.Text = tblcurso.Rows(0).Item("fechadoc_cup")
        Me.txtInicio.Text = tblcurso.Rows(0).Item("fechainicio_cup")

        '#INFO PARA BLOQUES
        Session("cpCurso") = Me.lblasignatura.Text
        Session("cpTotalHoras") = Me.lblTotal.Text

        
        Me.txtFin.Text = tblcurso.Rows(0).Item("fechafin_cup")
        Me.txtRetiro.Text = tblcurso.Rows(0).Item("fecharetiro_cup")
        Me.txtVacantes.Text = tblcurso.Rows(0).Item("vacantes_cup")
        Me.dpestado_cup.SelectedValue = tblcurso.Rows(0).Item("estado_cup")
        Me.lblOperador.Text = tblcurso.Rows(0).Item("login_per")
        Me.txtGrupoHor_cup.Text = tblcurso.Rows(0).Item("grupohor_cup")
        Me.cboTurno.SelectedValue = tblcurso.Rows(0).Item("turno_cup")
        Me.txtBloques.Text = tblcurso.Rows(0).Item("bloque_cup")


        Me.hddcodigo_cup.Value = ID
        Me.txtGrupos.Text = 1

        If tipo = "3" Then ' 3: modificacion de curso programado
            Me.ChkPrimerCiclo.Checked = tblcurso.Rows(0).Item("soloPrimerCiclo_cup")
            Me.chkMultiple.Checked = tblcurso.Rows(0).Item("multiEstcuela")
            Me.Button1.Enabled = True
        ElseIf tipo = "0" Then
            Me.ChkPrimerCiclo.Checked = 0
            Me.chkMultiple.Checked = 0
            Me.Button1.Enabled = False
        End If




        tblcurso.Dispose()
        Me.grwEquivalencias.DataSource = Nothing
        Me.grwEquivalencias.DataBind()                
        Me.grwEquivalencias.Dispose()

        Me.chkProfesor.Items.Clear()
        Me.fraEquivalencias.Visible = False
        Me.fraProfesores.Visible = False
        Me.fraDetalleGrupoHorario.Visible = True

        Me.cmdGuardar.Enabled = Me.hddModificar.Value   'True
        Me.cmdGuardar.Enabled = True 'D
        Me.txtGrupos.Enabled = tipo = 0
        Me.dpestado_cup.Enabled = tipo = 3
        Me.lblgrupohor_cup.Visible = tipo = 3
        Me.txtGrupoHor_cup.Visible = tipo = 3
        cmdGuardar.Text = IIf(tipo = 3, "Actualizar", "Guardar")

        'Mostrar Equivalencias para Escuelas Regulares y Complementarios: Modo AGREGAR
        If (Me.dpCodigo_cpf.SelectedValue <> 25 And Me.dpCodigo_cpf.SelectedValue <> 35) Then
            'If tipo = 0 Then
            '    Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursoEquivalenteAProgramar", Me.dpCodigo_pes.SelectedValue, Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString, Me.dpCodigo_cac.SelectedValue)
            '    Me.grwEquivalencias.DataBind()
            '    Me.fraEquivalencias.Visible = (Me.grwEquivalencias.Rows.Count > 0)
            '    Me.grwEquivalencias.Visible = fraEquivalencias.Visible
            'End If
            '**** Agregado por hreyes *****
            'Permite agregar y modificar cursos equivalente 
            '************************************************+
            If tipo = 0 Then
                'Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursoEquivalenteAProgramar", Me.dpCodigo_pes.SelectedValue, Integer.Parse(Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString), Me.dpCodigo_cac.SelectedValue)

                Dim dt As New Data.DataTable
                dt = obj.TraerDataTable("CAR_ConsultarCursoEquivalenteAProgramar", Me.dpCodigo_pes.SelectedValue, CInt(ID), Me.dpCodigo_cac.SelectedValue)

                If dt.Rows.Count > 0 Then
                    Me.grwEquivalencias.DataSource = dt
                    ' Response.Write("codigo_pes = " & Me.dpCodigo_pes.SelectedValue)
                    'Response.Write("codigo_cur = " & Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString)
                    Me.grwEquivalencias.DataBind()
                    Me.fraEquivalencias.Visible = (Me.grwEquivalencias.Rows.Count > 0)
                    Me.grwEquivalencias.Visible = fraEquivalencias.Visible
                End If
                dt = Nothing
            ElseIf tipo = 3 Then
                'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "AvisCUP", "alert('Mensaje: " & ID & "');", True)
                Me.grwEquivalencias.DataSource = obj.TraerDataTable("CAR_ConsultarCursoEquivalenteProgramados", Me.dpCodigo_pes.SelectedValue, Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString, Me.dpCodigo_cac.SelectedValue, ID)
                Me.grwEquivalencias.DataBind()
                Me.fraEquivalencias.Visible = (Me.grwEquivalencias.Rows.Count > 0)
                Me.grwEquivalencias.Visible = fraEquivalencias.Visible
            End If
            '====================================
            'Cargar Profesores adscritos
            '====================================
            If Me.dpCodigo_cpf.SelectedValue <> 19 Then
                Dim tblProfesor As Data.DataTable

                tblProfesor = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", 1, Me.dpCodigo_pes.SelectedValue, Me.grwCursosPlan.DataKeys.Item(Me.grwCursosPlan.SelectedIndex).Values("codigo_cur").ToString, Me.hdcodigo_dac.Value, Me.dpCodigo_cac.SelectedValue)
                For i As Integer = 0 To tblProfesor.Rows.Count - 1
                    Me.chkProfesor.Items.Add(tblProfesor.Rows(i).Item("profesor").ToString)
                    Me.chkProfesor.Items(i).Value = tblProfesor.Rows(i).Item("codigo_per").ToString

                    If tblProfesor.Rows(i).Item("marcado") = 1 Then
                        Me.chkProfesor.Items(i).Selected = True
                        Me.chkProfesor.Items(i).Attributes.Add("style", "background-color: #FFFF66")
                    End If
                Next
                tblProfesor.Dispose()
                Me.fraProfesores.Visible = True
                Me.lblProfesores.Visible = True
            End If
        End If

        obj = Nothing
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        If ValidaVacante.IsValid = False Then
            Exit Sub
        End If

        If Me.txtVacantes.Text = "" Then
            Me.txtVacantes.Text = 0
        End If

        If Me.hddMatriculados.Value = "" Then
            Me.hddMatriculados.Value = 0
        End If


        If CInt(Me.txtVacantes.Text) = 0 And CInt(Me.hddMatriculados.Value) = 0 Then

        Else
            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('Matriculados " & Me.hddMatriculados.Value.ToString & " - " & Me.txtVacantes.Text & "');", True)
            If CInt(Me.txtVacantes.Text) > CInt(Me.hddCapacidadMin.Value) Then
                ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('No puede asignar mas vacantes debido a la disponibilidad de aula asignada maxima es " & Me.hddCapacidadMin.Value.ToString & "');", True)
                Exit Sub
            End If

            If CInt(Me.txtVacantes.Text) < CInt(Me.hddMatriculados.Value) Then
                ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('No puede asignar vacantes menor al número de matriculados');", True)
                Exit Sub
            End If
        End If


                
        'If CInt(Me.txtVacantes.Text) < CInt(Me.hddMatriculados.Value) Then
        '    ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('No puede asignar vacantes menor al número de matriculados');", True)
        '    Exit Sub
        'End If
        ' Me.cmdGuardar.Enabled = False
        Me.cmdGuardar.Enabled = True        
        Dim Fila As GridViewRow
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim codigo_usu As Integer = Session("id_per")
        Dim strEquivalencias As String = ""
        Dim mensaje As String = ""

        Try
            '==================================
            'Almacenar Equivalencias
            '==================================

            If Me.cmdGuardar.Text = "Guardar" Then
                For I As Int16 = 0 To Me.grwEquivalencias.Rows.Count - 1
                    Fila = Me.grwEquivalencias.Rows(I)
                    If Fila.RowType = DataControlRowType.DataRow Then
                        If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then                            
                            strEquivalencias = Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_ceq") & "," & strEquivalencias
                        End If
                    End If
                Next
            Else
                '=================================
                'Agregar la modificacion de curso programado
                For I As Int16 = 0 To Me.grwEquivalencias.Rows.Count - 1
                    Fila = Me.grwEquivalencias.Rows(I)
                    If Fila.RowType = DataControlRowType.DataRow Then
                        If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                            obj.Ejecutar("ACAD_ActualizarCursosAgrupados_v1", Me.dpCodigo_cac.SelectedValue, _
                                         Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE"), _
                                         Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_curE"), _
                                         txtGrupoHor_cup.Text, "F", Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), _
                                         CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, "", _
                                         Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, ChkPrimerCiclo.Checked, _
                                         chkMultiple.Checked, Me.cboTurno.SelectedValue, Me.txtBloques.Text)
                            'mensaje = "Codigo_Cac: " & Me.dpCodigo_cac.SelectedValue & " Codigo_PesE: " & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE") & " Codigo_curE: " & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_curE") & " GrupoHor: " & txtGrupoHor_cup.Text & " Codigo_Pes: " & Me.dpCodigo_pes.SelectedValue & " Codigo_Cur: " & Me.hdCodigo_Cur.Value
                            ' mensaje = "Codigo_cac: " & Me.dpCodigo_cac.SelectedValue & " Codigo_PesE: " & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE") & " Codigo_CurE: " & Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_curE") & " grupoHor: " & txtGrupoHor_cup.Text & " TipoAct: " & "F" & " Vacantes: " & Me.txtVacantes.Text & " tipo_Cur: " & "C" & " inicio: " & CDate(Me.txtInicio.Text) & " Fin: " & CDate(Me.txtFin.Text) & " Retiro: " & CDate(Me.txtRetiro.Text) & " codigo_usu: " & codigo_usu & " Obs: " & "" & " codigo_pes: " & Me.dpCodigo_pes.SelectedValue & " codigo_cur: " & Me.hdCodigo_Cur.Value & " primerciclo: " & ChkPrimerCiclo.Checked & " Multiple: " & chkMultiple.Checked
                            'ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "Programar", "alert('" & mensaje & "');", True)
                        Else
                            obj.Ejecutar("ACAD_InactivaCursosAgrupados", Me.hddcodigo_cup.Value, _
                                        Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE"), _
                                        Me.grwEquivalencias.DataKeys.Item(Fila.RowIndex).Values("codigo_curE"), _
                                        txtGrupoHor_cup.Text)
                        End If
                    End If
                Next
            End If

            '==================================
            'Desactivar a todos los Profesores sugeridos
            '==================================                     
            obj.IniciarTransaccion()            
            obj.Ejecutar("CAR_EliminarPreCargaAcademica", "D", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, codigo_usu)

            '==================================
            'Agregar/Reactivar Profesores sugeridos
            '==================================            
            For j As Int16 = 0 To Me.chkProfesor.Items.Count - 1
                If Me.chkProfesor.Items(j).Selected Then
                    obj.Ejecutar("CAR_AgregarPreCargaAcademica", Me.hdCodigo_Cur.Value, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, Me.chkProfesor.Items(j).Value, codigo_usu, "", "")
                End If
            Next
            '==================================
            'Validar Controles
            '==================================
            If Me.txtGrupos.Text = "" Or Me.txtGrupoHor_cup.Text = "" Or Me.txtVacantes.Text = "" Then Exit Sub

            '==================================
            'Grabar Programación
            '==================================

            If Me.cmdGuardar.Text = "Guardar" Then
                mensaje = obj.Ejecutar("AgregarCursoProgramado_v1", Me.txtGrupos.Text, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, "F", Me.txtVacantes.Text, "C", CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), strEquivalencias.ToString, codigo_usu, "", Me.hdcodigo_dac.Value, ChkPrimerCiclo.Checked, Me.chkMultiple.Checked, 0)
            Else
                mensaje = obj.Ejecutar("ModificarCursoProgramado_v1", Me.hddcodigo_cup.Value, Me.txtGrupoHor_cup.Text.Trim, Me.dpestado_cup.SelectedValue, Me.txtVacantes.Text, CDate(Me.txtInicio.Text), CDate(Me.txtFin.Text), CDate(Me.txtRetiro.Text), codigo_usu, " ", Me.hdcodigo_dac.Value, ChkPrimerCiclo.Checked, Me.chkMultiple.Checked, 0)
            End If
            obj.TerminarTransaccion()
            '==================================
            'Actualizar Controles
            '==================================
            Me.grwCursosPlan.DataSource = obj.TraerDataTable("ConsultarCursoProgramado", 0, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, Me.dpCiclo_cur.SelectedValue, "")
            Me.grwCursosPlan.DataBind()
            Me.grwCursosPlan.PageIndex = Me.grwCursosPlan.PageCount
            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", 2, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, 0)
            Me.grwGruposProgramados.DataBind()
            obj = Nothing
            Me.fraDetalleGrupoHorario.Visible = False
            Me.fraGruposProgramados.Visible = True
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "AvisoOK", "alert('Mensaje: " & mensaje & "');", True)
        Catch ex As Exception
            obj.AbortarTransaccion()
            obj = Nothing            
            Me.cmdGuardar.Enabled = True
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, Me.GetType(), "ErrorGuardar", "alert('Ocurrió un error: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub LimpiarDatos(ByVal estado As Boolean)
        Me.fraDetalleGrupoHorario.Visible = False
        Me.fraDetalleCurso.Visible = False
        Me.fraGruposProgramados.Visible = False
        Me.grwCursosPlan.Visible = estado
        Me.grwCursosPlan.DataBind()
        Me.grwEquivalencias.DataBind()
        Me.hdCodigo_Cur.Value = 0
        Me.hdcodigo_dac.Value = 0
    End Sub
    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Me.cmdAgregar.Enabled = False
        Me.fraGruposProgramados.Visible = False
        CargarDetalleGrupoHorario("0", Me.hdCodigo_Cur.Value)
        Me.cmdAgregar.Enabled = True
        Me.hddCapacidadMin.Value = 1000
    End Sub

    Protected Sub grwGruposProgramados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwGruposProgramados.RowCommand
        hddcodigo_cup.Value = 0
        If (e.CommandName = "editar") Then

            Me.fraGruposProgramados.Visible = False
            Me.CargarDetalleGrupoHorario(3, Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value))
            Me.Button1.Enabled = True
            Session("XCodigoCup") = Convert.ToString(Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value)
            'hddMatriculados.Value = Me.grwGruposProgramados.DataKeys(Convert.ToInt32(e.CommandArgument)).Value
            ConsultarAmbienteAsignado()
        Else
            Me.Button1.Enabled = False
        End If
    End Sub

    Protected Sub ConsultarAmbienteAsignado()
        Dim objCnx As New ClsConectarDatos
        Dim Datos As New Data.DataTable
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        Datos = objCnx.TraerDataTable("ACAD_ConsultarAmbienteAsignadoCursoProgramado", hddcodigo_cup.Value)
        objCnx.CerrarConexion()
        If Datos.Rows.Count > 0 Then
            Me.hddCapacidadMin.Value = Datos.Rows(0).Item("capacidadMin")
        Else
            Me.hddCapacidadMin.Value = 1000
        End If

        Dim dt As New Data.DataTable
        objCnx.AbrirConexion()
        dt = objCnx.TraerDataTable("ACAD_RetornaMatriculadosCup", hddcodigo_cup.Value)
        objCnx.CerrarConexion()
        If dt.Rows.Count > 0 Then
            Me.hddMatriculados.Value = dt.Rows(0).Item("matriculados")
        Else
            Me.hddMatriculados.Value = 0
        End If
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        fraDetalleGrupoHorario.Visible = False
        fraGruposProgramados.Visible = True
    End Sub
    Protected Sub grwGruposProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwGruposProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Me.lblGrupos.Text = "Lista de Grupos Horario Programados (" & e.Row.RowIndex + 1 & ")"
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(6).Text = IIf(fila.Row("estado_cup") = False, IIf(fila.Row("ModalidadEstado_cup") = "A", "Cerrado (Escuela)", "Cerrado (Aut.)"), "Abierto")


            If fila.Row("codigoA_cpf") > 0 And fila.Row("codigoA_cpf") <> Me.dpCodigo_cpf.SelectedValue Then
                e.Row.Cells(7).Text = ""
                e.Row.Cells(8).Text = ""
            End If
        End If
    End Sub
    Protected Sub grwEquivalencias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwEquivalencias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "HabilitarEnvio(this)")

            Dim Fila As GridViewRow
            For I As Int16 = 0 To Me.grwEquivalencias.Rows.Count - 1
                Fila = Me.grwEquivalencias.Rows(I)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                        CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("enabled", "false")
                    Else
                        CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("enabled", "true")
                    End If
                End If
            Next
        End If
    End Sub
    Protected Sub grwGruposProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwGruposProgramados.RowDeleting
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim mensaje As String
        If hddEliminar.Value = True Then
            mensaje = obj.Ejecutar("EliminarCursoProgramado", grwGruposProgramados.DataKeys(e.RowIndex).Values("codigo_cup"), Request.QueryString("id"), 0)

            Me.grwGruposProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoProgramacionAcademica", 2, Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_pes.SelectedValue, Me.hdCodigo_Cur.Value, 0)
            Me.grwGruposProgramados.DataBind()
            Me.grwCursosPlan.DataSource = obj.TraerDataTable("ConsultarCursoProgramado", 0, Me.dpCodigo_pes.SelectedValue, Me.dpCodigo_cac.SelectedValue, Me.dpCiclo_cur.SelectedValue, "")
            Me.grwCursosPlan.DataBind()

            obj = Nothing
            e.Cancel = True
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarProgramacion", "alert('Eliminado: " & mensaje & "');", True)
        Else
            ScriptManager.RegisterStartupScript(Me.fraDetalleGrupoHorario, "string".GetType(), "EliminarProgramacion2", "alert('No puede eliminar el curso programado porque la fecha de programación académica ha concluido');", True)
        End If
        
    End Sub



    
    Protected Sub grwGruposProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwGruposProgramados.SelectedIndexChanged

    End Sub

    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
End Class
