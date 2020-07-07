﻿Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class academico_ProgramacionAcademica_frmProgramarCurso
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_CicloAcademico As New d_CicloAcademico
    Dim md_CarreraProfesional As New d_CarreraProfesional
    Dim md_PlanEstudio As New d_PlanEstudio
    Dim md_PlanCurso As New d_PlanCurso
    Dim md_CursoProgramado As New d_CursoProgramadoACAD
    Dim md_Ambiente As New d_Ambiente
    Dim md_PreCargaAcademica As New d_PreCargaAcademica
    Dim md_BloquesCursoProgramado As New d_BloquesCursoProgramado
    Dim md_Cronograma As New d_Cronograma

    'ENTIDADES
    Dim me_CicloAcademico As e_CicloAcademico
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_PlanEstudio As e_PlanEstudio
    Dim me_PlanCurso As e_PlanCurso
    Dim me_CursoProgramado As e_CursoProgramadoACAD
    Dim me_CursoAgrupado As e_CursoProgramadoACAD
    Dim me_Ambiente As e_Ambiente
    Dim me_PreCargaAcademica As e_PreCargaAcademica
    Dim me_BloquesCursoProgramado As e_BloquesCursoProgramado
    Dim me_PermisoAccionFuncion As e_PermisoAccionFuncion
    Dim me_Cronograma As e_Cronograma

    'VARIABLES    
    Dim cod_user As Integer = 0
    Dim codigo_apl As Integer = 0
    Dim codigo_tfu As Integer = 0
    Dim codigo_test As Integer = 0    
    Dim nombre_formulario As String = String.Empty

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_apl = Request.QueryString("apl")
            codigo_tfu = Request.QueryString("ctf")
            codigo_test = Request.QueryString("mod")
            nombre_formulario = Me.Form.Name

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarComboProgramacionAcademica()
                Call mt_CargarComboCarreraProfesional()
                Call mt_CargarComboPlanEstudio()
                Call mt_CargarComboCiclo()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        Try
            Call mt_CargarComboPlanEstudio()
            Call mt_CargarComboCiclo()

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            If Not fu_ValidarCargarDatos() Then Exit Sub

            Call mt_CargarDatos()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmProgramarCurso-codigo_cur") = Me.grwLista.DataKeys(index).Values("codigo_cur").ToString
            Session("frmProgramarCurso-codigo_pes") = Me.grwLista.DataKeys(index).Values("codigo_pes").ToString
            Session("frmProgramarCurso-codigo_cpf") = Me.grwLista.DataKeys(index).Values("codigo_Cpf").ToString

            Select Case e.CommandName
                Case "Programar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    If Not mt_CargarFormularioRegistro(Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur")) Then Exit Sub

                    If Not fu_ValidarCargarGruposHorario() Then Exit Sub
                    Call mt_CargarGruposHorario(Session("frmProgramarCurso-codigo_cac"), Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur"), Session("frmProgramarCurso-codigo_cpf"))

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirGrupo.Click
        Try
            Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevoGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoGrupo.Click
        Try
            If Not mt_VerificarPermiso("AGREGAR", False) Then Exit Sub

            Session("frmProgramarCurso-capacidad_amb") = 1000
            Session("frmProgramarCurso-matriculados") = 0

            If Not fu_ValidarCargarFormularioGrupoHorario("NUEVO") Then Exit Sub
            If Not mt_CargarFormularioGrupoHorario("NUEVO", Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur"), 0, Session("frmProgramarCurso-codigo_cac"), Session("frmProgramarCurso-codigo_cpf")) Then Exit Sub

            Call mt_UpdatePanel("RegistroCurso")

            Call mt_FlujoTabs("RegistroCurso")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaCurso_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaCurso.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmProgramarCurso-codigo_cup") = Me.grwListaCurso.DataKeys(index).Values("codigo_cup").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not mt_VerificarPermiso("EDITAR", False) Then Exit Sub

                    If Not fu_ValidarCargarFormularioGrupoHorario("EDITAR") Then Exit Sub
                    If Not mt_CargarFormularioGrupoHorario("EDITAR", Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur"), Session("frmProgramarCurso-codigo_cup"), Session("frmProgramarCurso-codigo_cac"), Session("frmProgramarCurso-codigo_cpf")) Then Exit Sub

                    Call mt_ObtenerDatosComplementarios(Session("frmProgramarCurso-codigo_cup"))

                    Call mt_UpdatePanel("RegistroCurso")

                    Call mt_FlujoTabs("RegistroCurso")

                Case "Eliminar"
                    If Not mt_VerificarPermiso("ELIMINAR", False) Then Exit Sub

                    If Not mt_EliminarCursoProgramado(Session("frmProgramarCurso-codigo_cup")) Then Exit Sub

                    Call mt_CargarGruposHorario(Session("frmProgramarCurso-codigo_cac"), Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur"), Session("frmProgramarCurso-codigo_cpf"))

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirCurso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirCurso.Click
        Try
            Call mt_FlujoTabs("ListadoCurso")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListarGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarGrupo.Click
        Try
            If Not fu_ValidarCargarGruposHorario() Then Exit Sub
            Call mt_CargarGruposHorario(Session("frmProgramarCurso-codigo_cac"), Session("frmProgramarCurso-codigo_pes"), Session("frmProgramarCurso-codigo_cur"), Session("frmProgramarCurso-codigo_cpf"))
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarCurso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarCurso.Click
        Try
            If mt_RegistrarCursoProgramado() Then
                Call btnListarGrupo_Click(Nothing, Nothing)
                Call mt_FlujoTabs("ListadoCurso")
                Exit Sub
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEditarBloque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditarBloque.Click
        Try
            If Not fu_ValidarEditarBloque() Then Exit Sub

            Call mt_LimpiarControles("RegistrarBloque")

            Call mt_UpdatePanel("RegistroBloque")

            Call mt_CargarBloques()            

            Call mt_FlujoModal("RegistrarBloque", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarBloque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarBloque.Click
        Try
            If mt_RegistrarBloque(CInt(Session("frmProgramarCurso-codigo_cup"))) Then
                Call mt_LimpiarControles("RegistrarBloque")

                Call mt_UpdatePanel("RegistroBloque")

                Call mt_CargarBloques()

                Exit Sub
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaBloque_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaBloque.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Dim codigo_bcup As Integer = CInt(Me.grwListaBloque.DataKeys(index).Values("codigo_bcup").ToString)

            Select Case e.CommandName
                Case "Eliminar"
                    If mt_EliminarBloque(codigo_bcup) Then
                        Call mt_LimpiarControles("RegistrarBloque")

                        Call mt_UpdatePanel("RegistroBloque")

                        Call mt_CargarBloques()
                    End If
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

                Case "Registro"
                    Me.udpRegistro.Update()

                Case "ListaCurso"
                    Me.udpListaCurso.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaCurso", "formatoGrillaCurso();", True)

                Case "RegistroCurso"
                    Me.udpRegistroCurso.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroCursoUpdate", "udpRegistroCursoUpdate();", True)

                Case "ListaCursoEquivalente"
                    Me.udpCursoEquivalente.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaCursoEquivalente", "formatoGrillaCursoEquivalente();", True)

                Case "ListaProfesorSugerido"
                    Me.udpProfesorSugerido.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaProfesorSugerido", "formatoGrillaProfesorSugerido();", True)

                Case "RegistroBloque"
                    Me.udpRegistroBloque.Update()

                Case "ListaBloque"
                    Me.udpListaBloque.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrillaBloque", "formatoGrillaBloque();", True)

                Case "ReestablecerFilas"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "reestablecerFilas", "reestablecerFilas();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)

                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "RegistroCurso"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro_curso-tab');", True)

                Case "ListadoCurso"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado_curso-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoModal(ByVal ls_modal As String, ByVal ls_accion As String)
        Try
            Select Case ls_accion
                Case "open"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & ls_modal & "');", True)

                Case "close"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal('" & ls_modal & "');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_VerificarPermiso(ByVal accion_permiso As String, Optional ByVal mostrar_permiso As Boolean = False) As Boolean
        Try
            Dim ls_mensaje As String = String.Empty : Dim dt As New DataTable
            Dim lb_VerificarRestriccion As Boolean = False

            Select Case accion_permiso
                Case "AGREGAR", "EDITAR", "ELIMINAR"
                    ls_mensaje = g_Seguridad.VerificarPermiso(codigo_apl, codigo_tfu, cod_user, nombre_formulario, _
                                                               accion_permiso, mostrar_permiso)

                    If String.IsNullOrEmpty(ls_mensaje) Then 'SI ls_mensaje ES VACÍO ENTONCES CUENTA CON EL PERMISO
                        lb_VerificarRestriccion = g_Seguridad.VerificarRestriccion(codigo_apl, codigo_tfu, cod_user, nombre_formulario, _
                                                               accion_permiso, mostrar_permiso)

                        If lb_VerificarRestriccion Then 'VERIFICAR OTRAS RESTRICCIONES PROPIAS DEL FORMULARIO
                            'VERIFICAR CRONOGRAMA

                            'Llenar objeto cronograma
                            me_Cronograma = New e_Cronograma

                            With me_Cronograma
                                .operacion = "CRO"
                                .codigo_act = "8"
                                .codigo_cac = CInt(Session("frmProgramarCurso-codigo_cac"))
                                .codigo_test = codigo_test
                            End With

                            dt = md_Cronograma.ListarCronograma(me_Cronograma)

                            If dt.Rows.Count = 0 Then ls_mensaje = "La fecha actual no se encuentra dentro del cronograma establecido para realizar la acción."
                        End If
                    End If
            End Select

            If Not String.IsNullOrEmpty(ls_mensaje) Then mt_ShowMessage(ls_mensaje, MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.txtCodigo.Text = String.Empty
                    Me.txtNombre.Text = String.Empty
                    Me.txtCreditos.Text = String.Empty
                    Me.txtTipo.Text = String.Empty
                    Me.txtDepAcademico.Text = String.Empty
                    Me.txtHorasTeoria.Text = String.Empty
                    Me.txtHorasPractica.Text = String.Empty
                    Me.txtHorasTotal.Text = String.Empty
                    Me.lblElectivo.Visible = False

                Case "RegistroCurso"
                    Me.txtGrupo.Text = String.Empty
                    Me.cmbEstado.SelectedValue = String.Empty
                    Me.txtNroGrupos.Text = String.Empty
                    Me.txtVacantes.Text = String.Empty
                    Me.txtFecha.Text = String.Empty
                    Me.cmbTurno.Text = String.Empty
                    Me.chkPrimerCiclo.Checked = False
                    Me.chkMultiple.Checked = False
                    Me.txtFechaInicio.Text = String.Empty
                    Me.txtFechaFin.Text = String.Empty
                    Me.txtFechaRetiro.Text = String.Empty
                    Me.txtRegistrado.Text = String.Empty
                    Me.txtBloques.Text = String.Empty
                    Me.lblBloques.Visible = False
                    Me.btnEditarBloque.Visible = False

                Case "RegistrarBloque"
                    Me.txtNombreAsignatura.Text = Me.txtNombre.Text.Trim
                    Me.txtHorasDisponible.Text = fu_ObtenerHorasDisponibles(CInt(Session("frmProgramarCurso-codigo_cup")))
                    Me.txtHorasBloque.Text = 0

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmProgramarCurso-codigo_pes") = Nothing
            Session("frmProgramarCurso-codigo_cur") = Nothing
            Session("frmProgramarCurso-codigo_cpf") = Nothing
            Session("frmProgramarCurso-codigo_cac") = Nothing
            Session("frmProgramarCurso-codigo_cup") = Nothing
            Session("frmProgramarCurso-codigo_dac") = Nothing

            Session("frmProgramarCurso-capacidad_amb") = Nothing
            Session("frmProgramarCurso-matriculados") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboProgramacionAcademica()
        Try
            Dim dt As New DataTable : me_CicloAcademico = New e_CicloAcademico

            With me_CicloAcademico
                .operacion = "GEN"
            End With

            dt = md_CicloAcademico.ListarCicloAcademico(me_CicloAcademico)

            Call md_Funciones.CargarCombo(Me.cmbProgramacionAcademica, dt, "codigo_Cac", "descripcion_Cac", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCarreraProfesional()
        Try
            Dim dt As New DataTable : me_CarreraProfesional = New e_CarreraProfesional

            With me_CarreraProfesional
                .codigo_test = codigo_test
                .codigo_tfu = codigo_tfu
                .codigo_per = cod_user
            End With

            dt = md_CarreraProfesional.ConsultarCarreraProfesional(me_CarreraProfesional)

            Call md_Funciones.CargarCombo(Me.cmbCarreraProfesional, dt, "codigo_cpf", "nombre_cpf", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboPlanEstudio()
        Try
            Dim dt As New DataTable : me_PlanEstudio = New e_PlanEstudio

            If Not String.IsNullOrEmpty(Me.cmbCarreraProfesional.SelectedValue) Then
                With me_PlanEstudio
                    If Me.cmbCarreraProfesional.SelectedValue <> 25 Then
                        .operacion = "GEN"
                        .codigo_cpf = Me.cmbCarreraProfesional.SelectedValue
                        If Me.cmbCarreraProfesional.SelectedValue <> g_VariablesGlobales.CarreraProfesionalEscuelaPre Then .vigencia_pes = 1
                    Else
                        .operacion = "LAR"
                        .codigo_cpf = Me.cmbCarreraProfesional.SelectedValue
                        .codigo_ctf = codigo_tfu
                        .codigo_test = 3
                        .vigencia_pes = 1
                        .operadoraut_acr = cod_user
                        .ind_ppcodigo_cpf = 1
                    End If
                End With
            Else
                dt.Columns.Add("codigo_Pes", Type.GetType("System.String"))
                dt.Columns.Add("descripcion_Pes", Type.GetType("System.String"))
            End If
            dt = md_PlanEstudio.ListarPlanEstudio(me_PlanEstudio)

            Call md_Funciones.CargarCombo(Me.cmbPlanEstudios, dt, "codigo_Pes", "descripcion_Pes", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCiclo()
        Try
            Dim dt As New DataTable : me_PlanCurso = New e_PlanCurso

            If Not String.IsNullOrEmpty(Me.cmbCarreraProfesional.SelectedValue) Then
                With me_PlanCurso
                    .operacion = "CIC"
                    .codigo_cpf = Me.cmbCarreraProfesional.SelectedValue
                End With
            Else
                dt.Columns.Add("ciclo_cur", Type.GetType("System.String"))
            End If
            dt = md_PlanCurso.ListarPlanCurso(me_PlanCurso)

            Call md_Funciones.CargarCombo(Me.cmbCiclo, dt, "ciclo_cur", "ciclo_cur", True, "[-- TODOS --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New Data.DataTable : me_PlanCurso = New e_PlanCurso

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_PlanCurso
                .operacion = "PPC"
                .codigo_cac = Me.cmbProgramacionAcademica.SelectedValue
                .codigo_pes = Me.cmbPlanEstudios.SelectedValue
                .ciclo_cur = Me.cmbCiclo.SelectedValue
            End With

            dt = md_PlanCurso.ListarPlanCurso(me_PlanCurso)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Session("frmProgramarCurso-codigo_cac") = Me.cmbProgramacionAcademica.SelectedValue

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDatos() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbProgramacionAcademica.SelectedValue) Then mt_ShowMessage("Debe seleccionar una programación acádemica.", MessageType.warning) : Me.cmbProgramacionAcademica.Focus() : Session("frmProgramarCurso-codigo_cac") = Nothing : Return False
            If String.IsNullOrEmpty(Me.cmbPlanEstudios.SelectedValue) Then mt_ShowMessage("Debe seleccionar un plan de estudios.", MessageType.warning) : Me.cmbPlanEstudios.Focus() : Session("frmProgramarCurso-codigo_cac") = Nothing : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioRegistro(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_PlanCurso = New e_PlanCurso
            Session("frmProgramarCurso-codigo_dac") = Nothing

            With me_PlanCurso
                .operacion = "VPC"
                .codigo_pes = codigo_pes
                .codigo_cur = codigo_cur
            End With

            dt = md_PlanCurso.ListarPlanCurso(me_PlanCurso)

            If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")
            Call mt_LimpiarControles("RegistroCurso")

            With dt.Rows(0)
                Me.txtCodigo.Text = .Item("identificador_cur").ToString
                Me.txtNombre.Text = .Item("nombre_cur").ToString
                Me.txtCreditos.Text = .Item("creditos_cur").ToString
                Me.txtTipo.Text = .Item("tipo_cur").ToString
                Me.txtDepAcademico.Text = .Item("nombre_dac").ToString
                Me.txtHorasTeoria.Text = .Item("horasTeo_Cur").ToString
                Me.txtHorasPractica.Text = .Item("horasPra_Cur").ToString
                Me.txtHorasTotal.Text = .Item("totalHoras_Cur").ToString
                Me.lblElectivo.Visible = .Item("electivo_Cur")

                Session("frmProgramarCurso-codigo_dac") = .Item("codigo_dac").ToString
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            If Session("frmProgramarCurso-codigo_pes") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_pes")) _
                OrElse Session("frmProgramarCurso-codigo_pes") = 0 Then mt_ShowMessage("El código de plan de estudios no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cur") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cur")) _
                OrElse Session("frmProgramarCurso-codigo_cur") = 0 Then mt_ShowMessage("El código de curso no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarGruposHorario(ByVal codigo_cac As Integer, ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_cpf As Integer)
        Try
            Dim dt As New Data.DataTable : me_CursoProgramado = New e_CursoProgramadoACAD

            If Me.grwListaCurso.Rows.Count > 0 Then Me.grwListaCurso.DataSource = Nothing : Me.grwListaCurso.DataBind()

            With me_CursoProgramado
                .operacion = "LIS"
                .codigo_cac = codigo_cac
                .codigo_pes = codigo_pes
                .codigo_cur = codigo_cur
                .codigo_cpf = codigo_cpf
            End With

            dt = md_CursoProgramado.ListarCursoProgramado(me_CursoProgramado)

            Me.grwListaCurso.DataSource = dt
            Me.grwListaCurso.DataBind()

            Call md_Funciones.AgregarHearders(grwListaCurso)

            Call mt_UpdatePanel("ListaCurso")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarGruposHorario() As Boolean
        Try
            If Session("frmProgramarCurso-codigo_cac") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cac")) _
                OrElse Session("frmProgramarCurso-codigo_cac") = 0 Then mt_ShowMessage("El código de ciclo académico no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_pes") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_pes")) _
                OrElse Session("frmProgramarCurso-codigo_pes") = 0 Then mt_ShowMessage("El código de plan de estudios no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cur") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cur")) _
                OrElse Session("frmProgramarCurso-codigo_cur") = 0 Then mt_ShowMessage("El código de curso no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cpf") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cpf")) _
                OrElse Session("frmProgramarCurso-codigo_cpf") = 0 Then mt_ShowMessage("El código de carrera profesional no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarFormularioGrupoHorario(ByVal tipo As String, ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_cup As Integer, ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_CursoProgramado = New e_CursoProgramadoACAD
            Dim operacion As String = String.Empty

            Dim ls_tipos As String = "NUEVO EDITAR"
            If String.IsNullOrEmpty(tipo) OrElse Not ls_tipos.Contains(tipo.Trim.ToUpper) Then mt_ShowMessage("El tipo de operación no esta definida.", MessageType.warning) : Return False

            If tipo.Equals("NUEVO") Then
                operacion = "CPN"
            ElseIf tipo.Equals("EDITAR") Then
                operacion = "CPE"
            End If

            With me_CursoProgramado
                .operacion = operacion
                .codigo_pes = codigo_pes
                .codigo_cur = codigo_cur
                .codigo_cup = codigo_cup
                .codigo_cac = codigo_cac
            End With

            dt = md_CursoProgramado.ListarCursoProgramado(me_CursoProgramado)

            If dt.Rows.Count = 0 Then mt_ShowMessage("No se ha encontrado información relacionada al grupo programado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("RegistroCurso")

            With dt.Rows(0)
                Me.txtGrupo.Text = .Item("grupohor_cup")
                Me.cmbEstado.SelectedValue = .Item("estado_cup")
                Me.txtNroGrupos.Text = 1
                Me.txtVacantes.Text = .Item("vacantes_cup")
                Me.txtFecha.Text = .Item("fechadoc_cup")
                Me.cmbTurno.SelectedValue = .Item("turno_cup")

                Me.txtFechaInicio.Text = .Item("fechainicio_cup")
                Me.txtFechaFin.Text = .Item("fechafin_cup")
                Me.txtFechaRetiro.Text = .Item("fecharetiro_cup")

                Me.txtRegistrado.Text = .Item("login_per")
                Me.txtBloques.Text = .Item("bloque_cup")

                If tipo.Equals("NUEVO") Then
                    Me.chkPrimerCiclo.Checked = False
                    Me.chkMultiple.Checked = False
                    Me.lblBloques.Visible = False
                    Me.btnEditarBloque.Visible = False

                    Me.txtNroGrupos.ReadOnly = False
                    Me.txtGrupo.ReadOnly = True
                    Me.cmbEstado.Enabled = False

                ElseIf tipo.Equals("EDITAR") Then
                    Me.chkPrimerCiclo.Checked = CBool(.Item("soloprimerciclo_cup"))
                    Me.chkMultiple.Checked = CBool(.Item("multiestcuela"))
                    Me.lblBloques.Visible = True
                    Me.btnEditarBloque.Visible = True

                    Me.txtNroGrupos.ReadOnly = True
                    Me.txtGrupo.ReadOnly = False
                    Me.cmbEstado.Enabled = True

                End If

                Session("frmProgramarCurso-codigo_cup") = codigo_cup
            End With

            If codigo_cpf = g_VariablesGlobales.CarreraProfesionalProgramaIntercambio Then Return True

            Call mt_CargarGrupoHorarioEquivalente(tipo, codigo_pes, codigo_cur, codigo_cup, codigo_cac)

            If codigo_cpf = g_VariablesGlobales.CarreraProfesionalCursosComplementarios Then Return True

            Call mt_CargarProfesoresSugeridos(codigo_pes, codigo_cur, Session("frmProgramarCurso-codigo_dac"), codigo_cac)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioGrupoHorario(ByVal tipo As String) As Boolean
        Try
            If Session("frmProgramarCurso-codigo_pes") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_pes")) _
                OrElse Session("frmProgramarCurso-codigo_pes") = 0 Then mt_ShowMessage("El código de plan de estudios no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cur") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cur")) _
                OrElse Session("frmProgramarCurso-codigo_cur") = 0 Then mt_ShowMessage("El código de curso no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cac") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cac")) _
                OrElse Session("frmProgramarCurso-codigo_cac") = 0 Then mt_ShowMessage("El código de ciclo académico no ha sido encontrado.", MessageType.warning) : Return False

            If Session("frmProgramarCurso-codigo_cpf") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cpf")) _
                OrElse Session("frmProgramarCurso-codigo_cpf") = 0 Then mt_ShowMessage("El código de carrera profesional no ha sido encontrado.", MessageType.warning) : Return False

            If tipo.Equals("EDITAR") Then
                If Session("frmProgramarCurso-codigo_cup") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cup")) _
                OrElse Session("frmProgramarCurso-codigo_cup") = 0 Then mt_ShowMessage("El código de curso programado no ha sido encontrado.", MessageType.warning) : Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarGrupoHorarioEquivalente(ByVal tipo As String, ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_cup As Integer, ByVal codigo_cac As Integer)
        Try
            Dim dt As New Data.DataTable : me_CursoProgramado = New e_CursoProgramadoACAD

            If Me.grwListaCursoEquivalente.Rows.Count > 0 Then Me.grwListaCursoEquivalente.DataSource = Nothing : Me.grwListaCursoEquivalente.DataBind()

            With me_CursoProgramado
                .codigo_pes = codigo_pes
                .codigo_cur = codigo_cur
                .codigo_cac = codigo_cac

                If tipo.Equals("NUEVO") Then
                    .operacion = "CEA"
                ElseIf tipo.Equals("EDITAR") Then
                    .operacion = "CEP"
                    .codigo_cupPadre = codigo_cup
                End If
            End With

            dt = md_CursoProgramado.ListarCursoProgramado(me_CursoProgramado)

            Me.grwListaCursoEquivalente.DataSource = dt
            Me.grwListaCursoEquivalente.DataBind()

            Call md_Funciones.AgregarHearders(grwListaCursoEquivalente)

            Call mt_UpdatePanel("ListaCursoEquivalente")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarProfesoresSugeridos(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_dac As Integer, ByVal codigo_cac As Integer)
        Try
            Dim dt As New Data.DataTable : me_CursoProgramado = New e_CursoProgramadoACAD

            If Me.grwListaProfesorSugerido.Rows.Count > 0 Then Me.grwListaProfesorSugerido.DataSource = Nothing : Me.grwListaProfesorSugerido.DataBind()

            With me_CursoProgramado
                .operacion = "GEN"
                .codigo_pes = codigo_pes
                .codigo_cur = codigo_cur
                .codigo_dac = codigo_dac
                .codigo_cac = codigo_cac
            End With

            dt = md_CursoProgramado.ListarProfesorAdscrito(me_CursoProgramado)

            Me.grwListaProfesorSugerido.DataSource = dt
            Me.grwListaProfesorSugerido.DataBind()

            Call md_Funciones.AgregarHearders(grwListaProfesorSugerido)

            Call mt_UpdatePanel("ListaProfesorSugerido")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ObtenerDatosComplementarios(ByVal codigo_cup As Integer)
        Try
            'Obtener Matriculados

            Dim dt As New DataTable : me_CursoProgramado = New e_CursoProgramadoACAD

            With me_CursoProgramado
                .operacion = "MAT"
                .codigo_cup = codigo_cup
            End With

            dt = md_CursoProgramado.ListarCursoProgramado(me_CursoProgramado)

            If dt.Rows.Count > 0 Then Session("frmProgramarCurso-matriculados") = dt.Rows(0).Item("matriculados") Else Session("frmProgramarCurso-matriculados") = 0

            'Obtener Capacidad Ambiente

            dt = New DataTable : me_Ambiente = New e_Ambiente

            With me_Ambiente
                .operacion = "CAP"
                .codigo_cup = codigo_cup
            End With

            dt = md_Ambiente.ListarAmbiente(me_Ambiente)

            If dt.Rows.Count > 0 Then Session("frmProgramarCurso-capacidad_amb") = dt.Rows(0).Item("capacidad_amb") Else Session("frmProgramarCurso-capacidad_amb") = 1000

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_EliminarCursoProgramado(ByVal codigo_cup As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_CursoProgramado = md_CursoProgramado.GetCursoProgramado(0)

            With me_CursoProgramado
                .operacion = "D"
                .codigo_cup = codigo_cup
                .cod_user = cod_user
            End With

            dt = md_CursoProgramado.RegistrarCursoProgramado(me_CursoProgramado)

            If dt.Rows.Count = 0 Then mt_ShowMessage("Se ha producido un error al tratar de eliminar el registro.", MessageType.error) : Return False

            If dt.Rows(0).Item("codigo_cup") = 0 Then mt_ShowMessage(dt.Rows(0).Item("mensaje"), MessageType.warning) : Return False

            mt_ShowMessage(dt.Rows(0).Item("mensaje"), MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarCursoProgramado() As Boolean
        Try
            If Not fu_ValidarRegistrarCursoProgramado() Then Call mt_UpdatePanel("ReestablecerFilas") : Return False

            Dim lst_CursoAgrupado As New List(Of e_CursoProgramadoACAD)
            Dim lst_PreCargaAcademica As New List(Of e_PreCargaAcademica)

            '=======================
            'Almacenar Equivalencias
            '=======================
            Dim ls_CursoEquivalente As String = String.Empty

            If CInt(Session("frmProgramarCurso-codigo_cup")) = 0 Then
                If Me.grwListaCursoEquivalente.Rows.Count > 0 Then
                    For Each Fila As GridViewRow In Me.grwListaCursoEquivalente.Rows
                        If CType(Fila.FindControl("chkElegirCurso"), CheckBox).Checked Then
                            ls_CursoEquivalente &= Me.grwListaCursoEquivalente.DataKeys.Item(Fila.RowIndex).Values("codigo_ceq") & "," & ls_CursoEquivalente
                        End If
                    Next
                End If
            Else
                '=========================================
                'Agregar la modificacion de curso agrupado
                '=========================================
                If Me.grwListaCursoEquivalente.Rows.Count > 0 Then
                    For Each Fila As GridViewRow In Me.grwListaCursoEquivalente.Rows
                        me_CursoAgrupado = md_CursoProgramado.GetCursoProgramado(0)

                        If CType(Fila.FindControl("chkElegirCurso"), CheckBox).Checked Then
                            With me_CursoAgrupado
                                .operacion = "U"
                                .codigo_cac = CInt(Session("frmProgramarCurso-codigo_cac"))
                                .codigo_pese = CInt(Me.grwListaCursoEquivalente.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE"))
                                .codigo_cure = CInt(Me.grwListaCursoEquivalente.DataKeys.Item(Fila.RowIndex).Values("codigo_CurE"))
                                .grupohor_cup = Me.txtGrupo.Text
                                .tipoact_cup = "F"
                                .vacantes_cup = CInt(Me.txtVacantes.Text)
                                .tipo_cup = "C"
                                .fechainicio_cup = CDate(Me.txtFechaInicio.Text)
                                .fechafin_cup = CDate(Me.txtFechaFin.Text)
                                .fecharetiro_cup = CDate(Me.txtFechaRetiro.Text)
                                .usuario_cup = cod_user
                                .obs_cup = String.Empty
                                .codigo_pes = CInt(Session("frmProgramarCurso-codigo_pes"))
                                .codigo_cur = CInt(Session("frmProgramarCurso-codigo_cur"))
                                .soloprimerciclo_cup = Me.chkPrimerCiclo.Checked
                                .multiescuela = Me.chkMultiple.Checked
                                .turno_cup = Me.cmbTurno.SelectedValue
                                .bloque_cup = CInt(Me.txtBloques.Text)
                            End With
                        Else
                            With me_CursoAgrupado
                                .operacion = "D"
                                .refcodigo_cup = CInt(Session("frmProgramarCurso-codigo_cup"))
                                .codigo_pese = CInt(Me.grwListaCursoEquivalente.DataKeys.Item(Fila.RowIndex).Values("codigo_PesE"))
                                .codigo_cure = CInt(Me.grwListaCursoEquivalente.DataKeys.Item(Fila.RowIndex).Values("codigo_CurE"))
                                .grupohor_cup = Me.txtGrupo.Text
                            End With
                        End If

                        lst_CursoAgrupado.Add(me_CursoAgrupado)
                    Next
                End If
            End If

            '======================================
            'Agregar/Reactivar Profesores sugeridos
            '======================================
            If Me.grwListaProfesorSugerido.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwListaProfesorSugerido.Rows
                    If CType(Fila.FindControl("chkElegirProfesor"), CheckBox).Checked Then
                        me_PreCargaAcademica = md_PreCargaAcademica.GetPreCargaAcademica(0)

                        With me_PreCargaAcademica
                            .operacion = "I"
                            .codigo_cur = CInt(Session("frmProgramarCurso-codigo_cur"))
                            .codigo_pes = CInt(Session("frmProgramarCurso-codigo_pes"))
                            .codigo_cac = CInt(Session("frmProgramarCurso-codigo_cac"))
                            .codigo_per = CInt(Me.grwListaProfesorSugerido.DataKeys.Item(Fila.RowIndex).Values("codigo_Per"))
                            .cod_user = cod_user
                        End With

                        lst_PreCargaAcademica.Add(me_PreCargaAcademica)
                    End If
                Next
            End If

            '===================
            'Grabar Programación
            '===================
            Dim dt As New Data.DataTable : me_CursoProgramado = md_CursoProgramado.GetCursoProgramado(0)

            If CInt(Session("frmProgramarCurso-codigo_cup")) = 0 Then
                With me_CursoProgramado
                    .operacion = "I"
                    .num_grupos = CInt(Me.txtNroGrupos.Text.Trim)
                    .codigo_cac = CInt(Session("frmProgramarCurso-codigo_cac"))
                    .codigo_pes = CInt(Session("frmProgramarCurso-codigo_pes"))
                    .codigo_cur = CInt(Session("frmProgramarCurso-codigo_cur"))
                    .tipoact_cup = "F"
                    .vacantes_cup = CInt(Me.txtVacantes.Text.Trim)
                    .tipo_cup = "C"
                    .fechainicio_cup = CDate(Me.txtFechaInicio.Text)
                    .fechafin_cup = CDate(Me.txtFechaFin.Text)
                    .fecharetiro_cup = CDate(Me.txtFechaRetiro.Text)
                    .codigo_ceq_cad = ls_CursoEquivalente
                    .cod_user = cod_user
                    .codigodac_cup = CInt(Session("frmProgramarCurso-codigo_dac"))
                    .soloprimerciclo_cup = Me.chkPrimerCiclo.Checked
                    .multiescuela = Me.chkMultiple.Checked
                End With
            Else
                With me_CursoProgramado
                    .operacion = "U"
                    .codigo_cac = CInt(Session("frmProgramarCurso-codigo_cac"))
                    .codigo_pes = CInt(Session("frmProgramarCurso-codigo_pes"))
                    .codigo_cur = CInt(Session("frmProgramarCurso-codigo_cur"))
                    .codigo_cup = CInt(Session("frmProgramarCurso-codigo_cup"))
                    .grupohor_cup = Me.txtGrupo.Text.Trim
                    .estado_cup = Me.cmbEstado.SelectedValue
                    .vacantes_cup = CInt(Me.txtVacantes.Text.Trim)
                    .fechainicio_cup = CDate(Me.txtFechaInicio.Text)
                    .fechafin_cup = CDate(Me.txtFechaFin.Text)
                    .fecharetiro_cup = CDate(Me.txtFechaRetiro.Text)
                    .cod_user = cod_user
                    .codigodac_cup = CInt(Session("frmProgramarCurso-codigo_dac"))
                    .soloprimerciclo_cup = Me.chkPrimerCiclo.Checked
                    .multiescuela = Me.chkMultiple.Checked
                End With
            End If

            me_CursoProgramado.lst_CursoAgrupado = lst_CursoAgrupado
            me_CursoProgramado.lst_PreCargaAcademica = lst_PreCargaAcademica

            dt = md_CursoProgramado.RegistrarProgramacionCurso(me_CursoProgramado)

            If dt.Rows.Count = 0 Then Call mt_UpdatePanel("ReestablecerFilas") : mt_ShowMessage("Se ha producido un error al tratar de realizar el registro.", MessageType.error) : Return False

            If dt.Rows(0).Item("codigo_cup") = 0 Then mt_ShowMessage(dt.Rows(0).Item("mensaje"), MessageType.warning) : Return False

            mt_ShowMessage(dt.Rows(0).Item("mensaje"), MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_UpdatePanel("ReestablecerFilas")
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarCursoProgramado() As Boolean
        Try            
            'Validar  variables de sesión
            If Session("frmProgramarCurso-codigo_cup") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cup")) Then mt_ShowMessage("El código de curso programado no ha sido encontrado.", MessageType.warning) : Return False
            If Session("frmProgramarCurso-codigo_cac") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cac")) Then mt_ShowMessage("El código de ciclo académico no ha sido encontrado.", MessageType.warning) : Return False
            If Session("frmProgramarCurso-codigo_pes") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_pes")) Then mt_ShowMessage("El código de plan de estudio no ha sido encontrado.", MessageType.warning) : Return False
            If Session("frmProgramarCurso-codigo_cur") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cur")) Then mt_ShowMessage("El código del curso no ha sido encontrado.", MessageType.warning) : Return False
            If Session("frmProgramarCurso-matriculados") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-matriculados")) Then mt_ShowMessage("El número de matriculados no ha sido encontrado.", MessageType.warning) : Return False
            If Session("frmProgramarCurso-capacidad_amb") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-capacidad_amb")) Then mt_ShowMessage("La capacidad del ambiente no ha sido encontrada.", MessageType.warning) : Return False

            'Validar Controles
            If CInt(Session("frmProgramarCurso-codigo_cup")) > 0 AndAlso String.IsNullOrEmpty(Me.txtGrupo.Text.Trim) Then mt_ShowMessage("Debe ingresar un grupo válido.", MessageType.warning) : Me.txtGrupo.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtNroGrupos.Text.Trim) OrElse CInt(Me.txtNroGrupos.Text.Trim) <= 0 Then mt_ShowMessage("Debe ingresar un número de grupos válido.", MessageType.warning) : Me.txtNroGrupos.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbTurno.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un turno válido.", MessageType.warning) : Me.cmbTurno.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtFechaInicio.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha de inicio válida.", MessageType.warning) : Me.txtFechaInicio.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtFechaFin.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha de fin válida.", MessageType.warning) : Me.txtFechaFin.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtFechaRetiro.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha de retiro válida.", MessageType.warning) : Me.txtFechaRetiro.Focus() : Return False
            If CDate(Me.txtFechaFin.Text.Trim) < CDate(Me.txtFechaInicio.Text.Trim) Then mt_ShowMessage("La fecha de fin debe ser mayor o igual a la fecha de inicio.", MessageType.warning) : Me.txtFechaFin.Focus() : Return False
            If CDate(Me.txtFechaFin.Text.Trim) < CDate(Me.txtFechaRetiro.Text.Trim) Then mt_ShowMessage("La fecha de retiro debe ser menor o igual a la fecha de fin.", MessageType.warning) : Me.txtFechaRetiro.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtVacantes.Text.Trim) OrElse CInt(Me.txtVacantes.Text.Trim) <= 0 Then mt_ShowMessage("Debe ingresar una cantidad de vacantes válida.", MessageType.warning) : Me.txtVacantes.Focus() : Return False
            If CInt(Me.txtVacantes.Text.Trim) > CInt(Session("frmProgramarCurso-capacidad_amb")) Then mt_ShowMessage("No puede asignar mas vacantes debido a que la capacidad máxima del aula asignada es " & Session("frmProgramarCurso-capacidad_amb") & ".", MessageType.warning) : Me.txtVacantes.Focus() : Return False
            If CInt(Me.txtVacantes.Text.Trim) < CInt(Session("frmProgramarCurso-matriculados")) Then mt_ShowMessage("No puede asignar una cantidad de vacantes menor al número de matriculados", MessageType.warning) : Me.txtVacantes.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarEditarBloque() As Boolean
        Try
            'Validar  variables de sesión
            If Session("frmProgramarCurso-codigo_cup") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cup")) OrElse CInt(Session("frmProgramarCurso-codigo_cup")) <= 0 Then mt_ShowMessage("El código de curso programado no ha sido encontrado.", MessageType.warning) : Return False

            'Validar Controles
            If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre de asignatura válido.", MessageType.warning) : Me.txtNombre.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtHorasTotal.Text.Trim) OrElse CInt(Me.txtHorasTotal.Text.Trim) <= 0 Then mt_ShowMessage("Debe ingresar un número de horas válido.", MessageType.warning) : Me.txtHorasTotal.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarBloques()
        Try
            Dim dt As New Data.DataTable : me_BloquesCursoProgramado = New e_BloquesCursoProgramado

            If Me.grwListaBloque.Rows.Count > 0 Then Me.grwListaBloque.DataSource = Nothing : Me.grwListaBloque.DataBind()

            With me_BloquesCursoProgramado
                .operacion = "GEN"
                .codigo_cup = CInt(Session("frmProgramarCurso-codigo_cup"))
            End With

            dt = md_BloquesCursoProgramado.ListarBloquesCursoProgramado(me_BloquesCursoProgramado)

            Me.grwListaBloque.DataSource = dt
            Me.grwListaBloque.DataBind()

            Call md_Funciones.AgregarHearders(grwListaBloque)

            Call mt_UpdatePanel("ListaBloque")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ObtenerHorasDisponibles(ByVal codigo_cup As Integer) As Integer
        Try
            Dim horas_disponibles As Integer = 0
            Dim horas_utilizadas As Integer = 0

            Dim dt As New Data.DataTable : me_BloquesCursoProgramado = New e_BloquesCursoProgramado

            With me_BloquesCursoProgramado
                .operacion = "NUH"
                .codigo_cup = codigo_cup
            End With

            dt = md_BloquesCursoProgramado.ListarBloquesCursoProgramado(me_BloquesCursoProgramado)

            If dt.Rows.Count > 0 Then horas_utilizadas = CInt(dt.Rows(0).Item("total_horas"))

            horas_disponibles = CInt(Me.txtHorasTotal.Text.Trim) - horas_utilizadas
            If horas_disponibles < 0 Then horas_disponibles = 0

            Return horas_disponibles
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarBloque(ByVal codigo_cup As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarBloque() Then Return False

            me_BloquesCursoProgramado = md_BloquesCursoProgramado.GetBloquesCursoProgramado(0)

            With me_BloquesCursoProgramado
                .operacion = "I"
                .codigo_cup = codigo_cup
                .numerohoras = CInt(Me.txtHorasBloque.Text.Trim)
                .cod_user = cod_user
            End With

            md_BloquesCursoProgramado.RegistrarBloquesCursoProgramado(me_BloquesCursoProgramado)

            mt_ShowMessage("¡El bloque se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarBloque() As Boolean
        Try
            'Validar  variables de sesión
            If Session("frmProgramarCurso-codigo_cup") Is Nothing OrElse String.IsNullOrEmpty(Session("frmProgramarCurso-codigo_cup")) Then mt_ShowMessage("El código de curso programado no ha sido encontrado.", MessageType.warning) : Return False

            'Validar Controles
            If String.IsNullOrEmpty(Me.txtHorasBloque.Text.Trim) OrElse CInt(Me.txtHorasBloque.Text.Trim) <= 0 Then mt_ShowMessage("Debe ingresar un número de horas válido.", MessageType.warning) : Me.txtHorasBloque.Focus() : Return False
            If CInt(Me.txtHorasBloque.Text.Trim) > fu_ObtenerHorasDisponibles(CInt(Session("frmProgramarCurso-codigo_cup"))) Then mt_ShowMessage("El número de horas asignadas sobrepasan a las horas disponibles.", MessageType.warning) : Me.txtHorasBloque.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarBloque(ByVal codigo_bcup As Integer) As Boolean
        Try
            If Not fu_ValidarEliminarBloque(codigo_bcup) Then Return False

            me_BloquesCursoProgramado = md_BloquesCursoProgramado.GetBloquesCursoProgramado(0)

            With me_BloquesCursoProgramado
                .operacion = "D"
                .codigo_bcup = codigo_bcup
            End With

            md_BloquesCursoProgramado.RegistrarBloquesCursoProgramado(me_BloquesCursoProgramado)

            mt_ShowMessage("¡El bloque se elimino exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarEliminarBloque(ByVal codigo_bcup As Integer) As Boolean
        Try
            If codigo_bcup <= 0 Then mt_ShowMessage("El código de bloque no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
