﻿Imports System.Data

Partial Class Alumni_frmRegReqEgresados
    Inherits System.Web.UI.Page



#Region "Variables de clase"
    Dim codigo_tfu, codigo_usu, codigo_test As String
    Dim md_Funciones As d_Funciones
    Dim md_CarreraProfesional As d_CarreraProfesional
    Dim md_PlanEstudio As d_PlanEstudio
    Dim md_Alumno As d_Alumno
    Dim md_RequisitoEgreso As d_RequisitoEgreso
    Dim md_cicloAcademico As d_CicloAcademico
    Dim md_alumnoRequisito As d_AlumnoRequisito

    'Endtidades
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_planEstudio As e_PlanEstudio
    Dim me_alumno As e_Alumno
    Dim me_RequisitoEgreso As e_RequisitoEgreso
    Dim me_cicloAcademico As e_CicloAcademico
    Dim me_alumnoRequisito As e_AlumnoRequisito

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub lbBusca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBusca.Click
        If ddlCarrrera.SelectedValue = "" Or ddlPlanEst.SelectedValue = "" Then
            Call mt_ShowMessage("Debe completar los datos", MessageType.warning)
            Me.udpListado.Update()
        End If
        Me.udpScripts.Update()
        mt_CargarGrillaAlumnos()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            codigo_tfu = Request.QueryString("ctf") '1
            codigo_usu = Request.QueryString("id") 'Session("id_per") -- 684'
            codigo_test = "2" 'Request.QueryString("mod")

            'codigo_tfu = "1" '1
            'codigo_usu = "684"  '----684 '
            'codigo_test = "2" 'Request.QueryString("mod")

            If IsPostBack = False Then
                Call mt_CargarComboProfesional()
                Call mt_CargarComboCicloAcademico()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub ddlCarrrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrrera.SelectedIndexChanged
        Try
            'Me.txtPrueba.Text = ddlCarrrera.SelectedValue
            'Me.udpListado.Update()
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "alert", "alert('Hola');", True)
            Call mt_CargarComboPlan()
            'mt_CargarPlanCurricular()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If Me.gvRequisitos.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.gvRequisitos.Rows
                    md_alumnoRequisito = New d_AlumnoRequisito : me_alumnoRequisito = New e_AlumnoRequisito

                    me_alumnoRequisito.codigo_alu = hdCodigo_alu.Value 'Me.gvAlumnosReq.DataKeys(Fila.RowIndex).Item("codigo_alu").ToString
                    me_alumnoRequisito.codigo_pcur = Me.gvRequisitos.DataKeys(Fila.RowIndex).Item("codigo_pcur").ToString
                    me_alumnoRequisito.codigo_req = Me.gvRequisitos.DataKeys(Fila.RowIndex).Item("codigo_req").ToString
                    me_alumnoRequisito.codigo_tip = Me.gvRequisitos.DataKeys(Fila.RowIndex).Item("codigo_tip").ToString
                    me_alumnoRequisito.codigo_are = Me.gvRequisitos.DataKeys(Fila.RowIndex).Item("codigo_are").ToString
                    me_alumnoRequisito.fecha_reg = DateTime.Now()
                    me_alumnoRequisito.usuario_reg = codigo_usu

                    Dim dt As New Data.DataTable

                    If CType(Fila.FindControl("chkCumplir"), CheckBox).Checked Then
                        me_alumnoRequisito.observacion = "Cumplió"
                        me_alumnoRequisito.estado = "1"
                        me_alumnoRequisito.fecha_mod = DateTime.Now()
                        me_alumnoRequisito.usuario_mod = codigo_usu
                        dt = md_alumnoRequisito.InsertarAlumnoRequisito(me_alumnoRequisito)
                    Else
                        If me_alumnoRequisito.codigo_are <> "0" Then
                            me_alumnoRequisito.observacion = ""
                            me_alumnoRequisito.estado = "0"
                            me_alumnoRequisito.fecha_mod = DateTime.Now()
                            me_alumnoRequisito.usuario_mod = codigo_usu
                            dt = md_alumnoRequisito.InsertarAlumnoRequisito(me_alumnoRequisito)
                        End If
                    End If
                Next
                udpScripts.Update()
                Call mt_ShowMessage("¡Se actualizó el estado de los requisitos!", MessageType.success)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal();", True)
                'mt_CargarGrillaAlumnos()
                'Me.udpListado.Update()
                'mt_cargarFormAlumno(hdCodigo_alu.Value.ToString)
                'udpModal.Update()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub gvAlumnosReq_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAlumnosReq.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(4).Text = IIf(fila.Row("estadoactual_alu") = 0, "Inactivo", "Activo")

            If e.Row.Cells(4).Text = "Activo" Then
                e.Row.Cells(4).Font.Bold = True
                '---- ****** para desactivar el check cuando el alumno no cumple con todos los requisitos
                'md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso

                'Dim dtReqAct As New Data.DataTable
                'me_alumno.codigo_alu = fila.Row("codigo_alu")
                'dtReqAct = md_RequisitoEgreso.ListarRequisitosByAlumno(me_alumno)

                'Dim cuentaReq As Integer = 0
                'For Each row As DataRow In dtReqAct.Rows
                '    If (row("cumplio").ToString() = "false") Then
                '        cuentaReq = cuentaReq + 1
                '    End If
                'Next
                'If cuentaReq > 0 Then
                '    e.Row.Cells(0).Enabled = False
                'End If
                '---- ****** fin
            Else
                'e.Row.Cells(0).Enabled = False
                e.Row.Cells(10).Enabled = False
                'e.Row.Cells(0).Visible = False
                e.Row.Cells(4).Font.Bold = False
                e.Row.Cells(4).ForeColor = System.Drawing.Color.Red
            End If
            'CType(e.Row.FindControl("chkElegir"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

        End If


    End Sub
    Protected Sub gvAlumnosReq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAlumnosReq.RowCommand
        Try

            'Call mt_ShowMessage("¡Se actualizó el estado de los requisitos!", MessageType.success)

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Session("codigo_alu") = Me.gvAlumnosReq.DataKeys(index).Values("codigo_alu")

            Select Case e.CommandName
                Case "Editar"
                    If mt_cargarFormAlumno(Session("codigo_alu")) <> "" Then
                        Dim mensaje As String
                        mensaje = mt_cargarFormAlumno(Session("codigo_alu"))
                        Me.udpScripts.Update()
                        mt_ShowMessage(mensaje, MessageType.warning)
                        Exit Sub
                    End If
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)
                    'Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
                    Me.udpModal.Update()
            End Select


        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Procedimeintos"

    Private Sub mt_CargarComboProfesional()
        Try
            md_Funciones = New d_Funciones : md_CarreraProfesional = New d_CarreraProfesional : me_CarreraProfesional = New e_CarreraProfesional
            Dim dtCarreraProfesional As New Data.DataTable
            me_CarreraProfesional.codigo_per = codigo_usu
            me_CarreraProfesional.codigo_tfu = codigo_tfu
            me_CarreraProfesional.codigo_test = codigo_test
            dtCarreraProfesional = md_CarreraProfesional.ListarCarreraProfesionalByAcceso(me_CarreraProfesional)
            Call md_Funciones.CargarCombo(Me.ddlCarrrera, dtCarreraProfesional, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")

            dtCarreraProfesional.Dispose()
            'udpFiltro.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboPlan()
        Try
            md_Funciones = New d_Funciones : md_PlanEstudio = New d_PlanEstudio : me_planEstudio = New e_PlanEstudio
            Dim dtPlanEstudio As New Data.DataTable
            me_planEstudio.codigo_cpf = Me.ddlCarrrera.SelectedValue
            'Me.txtPrueba.Text = me_planEstudio.codigo_cpf
            dtPlanEstudio = md_PlanEstudio.ListarPlanEstudioByCarrera(me_planEstudio)
            Call md_Funciones.CargarCombo(Me.ddlPlanEst, dtPlanEstudio, "codigo_pcur", "nombre_pcur", True, "[-- SELECCIONE --]", "")
            Me.udpListado.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub mt_CargarGrillaAlumnos()
        me_alumno = New e_Alumno : md_Alumno = New d_Alumno : md_Funciones = New d_Funciones
        Try
            me_alumno.codigo_pes = "0"
            me_alumno.tempcodigo_cpf = Me.ddlCarrrera.SelectedValue
            me_alumno.nombres = Me.txtAlumno.Text
            me_alumno.codigo_pcur = Me.ddlPlanEst.SelectedValue

            Dim dtAlumo As New Data.DataTable
            dtAlumo = md_Alumno.ListarAlumnoReqEgreByPEstudioCprofe(me_alumno)
            Me.gvAlumnosReq.DataSource = dtAlumo
            Me.gvAlumnosReq.DataBind()
            'dtAlumo.Dispose()

            'Call md_Funciones.AgregarHearders(gvAlumnosReq)
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            Me.udpListado.Update()


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Function mt_cargarFormAlumno(ByVal codigo_alu As String) As String
        Dim condicion As Integer
        Dim mensaje As String
        Dim condicionAlu As String
        Try
            mensaje = ""
            md_Alumno = New d_Alumno : me_alumno = New e_Alumno : md_RequisitoEgreso = New d_RequisitoEgreso : me_RequisitoEgreso = New e_RequisitoEgreso
            With me_alumno
                .codigo_alu = codigo_alu
                .accion = "CRE"
            End With
            Dim dtAlumno As New Data.DataTable
            Dim dtRequisito As New Data.DataTable

            'Para ver si el plan curricular tiene requisitos
            Dim dtReqPlanCur As New Data.DataTable
            dtReqPlanCur = md_Alumno.ListarReqEgreDePlanCurByCodAlum(me_alumno)

            If dtReqPlanCur.Rows.Count = 0 Then
                mensaje = "El plan de estudio seleccionado no contiene requisitos"
                Return mensaje
                'si tiene requisitos
            Else
                dtAlumno = md_Alumno.ListarAlumnoReq(me_alumno)

                If dtAlumno.Rows.Count = 0 Then
                    mensaje = "No se encontró registro para el código de este alumno"

                Else
                    With dtAlumno.Rows(0)
                        Me.txtCodUniAlum.Text = .Item("codigoUniver_Alu").ToString
                        Me.txtNombres.Text = .Item("nombres").ToString
                        Me.hdCodigo_alu.Value = me_alumno.codigo_alu.ToString
                        condicionAlu = .Item("condicion_Alu").ToString
                    End With

                    If condicionAlu <> "I" Then
                        mensaje = "El alumno no tiene condición de ingresante por lo tanto no tiene requisitos de egresado"
                        Return mensaje
                    Else
                        dtRequisito = md_RequisitoEgreso.ListarRequisitosByAlumno(me_alumno)
                        'If dtRequisito.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Exit Function
                        If dtRequisito.Rows.Count = 0 Then
                            mensaje = mensaje & " No se encontró requisitos para el alumno, configure requisitos"
                        End If

                        'validar que haya cumplido todos los requisitos
                        condicion = 0
                        For Each row As DataRow In dtRequisito.Rows
                            If (row("cumplio").ToString() = "false") Then
                                condicion = condicion + 1
                            End If
                        Next
                        Me.HdCondicion.Value = condicion.ToString

                        If condicion > 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "validaReq", "validaReq(" & condicion & ");", True)
                            'Me.btnGuardar.Enabled = False
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "NoValidaReq", "NoValidaReq();", True)
                            'Me.btnGuardar.Enabled = True
                        End If

                        '-------------------------
                        Me.gvRequisitos.DataSource = dtRequisito
                        Me.gvRequisitos.DataBind()
                        'gvRequisitos.Dispose()
                        Return mensaje
                    End If
                End If
            End If
        Catch ex As Exception
            condicion = 0
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
            'Response.Write(ex)
            mensaje = ex.Message
            Return mensaje

        End Try
    End Function
    Private Sub mt_CargarComboCicloAcademico()
        Try
            md_Funciones = New d_Funciones : md_cicloAcademico = New d_CicloAcademico : me_cicloAcademico = New e_CicloAcademico
            Dim dt As New Data.DataTable
            dt = md_cicloAcademico.ObtenerCicloAcademico

            ' Call md_Funciones.CargarCombo(Me.ddlSemestre, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            'Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    
#End Region
    
    


End Class
