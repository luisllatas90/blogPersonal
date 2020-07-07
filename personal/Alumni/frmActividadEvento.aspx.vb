﻿Partial Class Alumni_frmActividadEvento
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_CentroCostos As New d_CentroCostos
    Dim md_ActividadEvento As New d_ActividadEvento
    Dim md_Participacion As New d_Participacion
    Dim md_EnvioSMS As New d_EnvioSMS
    Dim md_ActividadProgramacion As New d_ActividadProgramacion
    Dim md_CicloAcademico As New d_CicloAcademico
    Dim md_TipoParticipante As New d_TipoParticipante

    'ENTIDADES
    Dim me_CentroCostos As e_CentroCostos
    Dim me_ActividadEvento As e_ActividadEvento
    Dim me_Participacion As e_Participacion
    Dim me_EnvioSMS As e_EnvioSMS
    Dim me_ActividadProgramacion As e_ActividadProgramacion    
    Dim me_TipoParticipante As New e_TipoParticipante

    'VARIABLES
    Dim cod_user As Integer = 0


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
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarComboTipo()
                Call mt_LimpiarControlesModal()
                Call mt_LimpiarControlesEnvioSMS()
                '--------------- olluen 28/02/2020 -----------------
                Call mt_CargarComboTipoParticipante()
                '---------------------------------------------------

            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmActividadEvento-codigo_cco") = Me.grwLista.DataKeys(index).Values("codigo_Cco")
            Session("frmActividadEvento-nombre_evento") = Me.grwLista.DataKeys(index).Values("descripcion_Cco")
            '------ Olluen 27/02/2020 -----------------------------------------
            Session("frmActividadEvento-token_dev") = Me.grwLista.DataKeys(index).Values("token_dev")
            '--------------------------------------------------------------------

            Select Case e.CommandName
                Case "Ver"
                    Call mt_ListarActividades(Session("frmActividadEvento-codigo_cco"))
                    Me.txtEvento.Text = Session("frmActividadEvento-nombre_evento")
                    Call mt_CargarComboServicioConcepto()
                    Call mt_FlujoTabs("Actividad")
                    '------ Olluen 27/02/2020 -----------------------------------------
                Case "Inscripcion"
                    Me.txtCodigo_cco.Text = Session("frmActividadEvento-codigo_cco")
                    Me.txtEventoInsc.Text = Session("frmActividadEvento-nombre_evento")
                    Me.hfCodigo_cco.Value = Session("frmActividadEvento-codigo_cco")
                    Me.hftoken_dev.Value = Session("frmActividadEvento-token_dev")

                    Call mt_FlujoTabs("Inscripcion")
                    Me.udpInscripcion.Update()

                Case "VerInscritos"
                    Me.txtCcoVerInsc.Text = Session("frmActividadEvento-codigo_cco")
                    Me.txtEventoVerInsc.Text = Session("frmActividadEvento-nombre_evento")
                    'Me.hfCodigo_cco.Value = Session("frmActividadEvento-codigo_cco")
                    'Me.hftoken_dev.Value = Session("frmActividadEvento-token_dev")

                    Call mt_ListarVerInscritos(Session("frmActividadEvento-codigo_cco"))


                    Call mt_FlujoTabs("VerInscritos")
                    Me.udpVerInscritos.Update()



                    '--------------------------------------------------------------------
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwActividad_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwActividad.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmActividadEvento-codigo_aev") = Me.grwActividad.DataKeys(index).Values("codigo_aev")

            me_ActividadEvento = New e_ActividadEvento            

            Select Case e.CommandName
                Case "Editar"
                    Call mt_LimpiarControlesModal()

                    me_ActividadEvento = md_ActividadEvento.GetActividadEvento(Session("frmActividadEvento-codigo_aev"))

                    If me_ActividadEvento.codigo_aev = 0 Then Call mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.error) : Exit Sub
                    If me_ActividadEvento.envioSMS = "SI" Then Call mt_ShowMessage("No puede modificar el registro seleccionado.", MessageType.warning) : Exit Sub                    

                    Me.txtActividad.Text = me_ActividadEvento.nombre_aev                    
                    Me.cmbTipo.SelectedValue = me_ActividadEvento.codigo_act
                    Me.cmbServicioConcepto.SelectedValue = me_ActividadEvento.codigo_sco
                    Me.txtCupos.Text = me_ActividadEvento.cupos_aev
                    Me.txtCosto.Text = me_ActividadEvento.costo_aev
                    Me.txtInscritos.Text = me_ActividadEvento.inscritos
                    Me.txtEncuesta.Text = me_ActividadEvento.urlEncuesta_aev

                    Session("frmActividadEvento-codigo_cco") = me_ActividadEvento.codigo_cco

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoModal("Actividad", "open")                                        

                Case "SMS"
                    Call mt_LimpiarControlesEnvioSMS()

                    me_ActividadEvento = md_ActividadEvento.GetActividadEvento(Session("frmActividadEvento-codigo_aev"))

                    If me_ActividadEvento.codigo_aev = 0 Then Call mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.error) : Exit Sub

                    If me_ActividadEvento.envioSMS = "SI" Then Call mt_ShowMessage("Ya se ha realizado un envío de mensajes para la actividad seleccionada.", MessageType.warning) : Exit Sub

                    Call mt_ListarParticipantes(Session("frmActividadEvento-codigo_aev"))

                    If Me.grwParticipantesSMS.Rows.Count = 0 Then Call mt_ShowMessage("La actividad no presenta participantes.", MessageType.warning) : Exit Sub

                    Me.txtEventoSMS.Text = Session("frmActividadEvento-nombre_evento")
                    Me.txtActividadSMS.Text = me_ActividadEvento.nombre_aev
                    Me.txtMensajeSMS.Text = String.Empty
                    Me.txtEncuestaSMS.Text = me_ActividadEvento.urlEncuesta_aev
                    Me.txtParticipantesSMS.Text = Me.grwParticipantesSMS.Rows.Count

                    'Obtener el total de personas que no cuentan con celular
                    Dim ln_SinCelular As Integer = 0

                    If Me.grwParticipantesSMS.Rows.Count > 0 Then
                        For Each Fila As GridViewRow In Me.grwParticipantesSMS.Rows
                            If Fila.Cells(1).Text.Trim = "NO PRESENTA" Then
                                ln_SinCelular = ln_SinCelular + 1
                            End If
                        Next
                    End If

                    Me.txtTotalSMS.Text = Me.grwParticipantesSMS.Rows.Count - ln_SinCelular

                    Call mt_UpdatePanel("EnvioSMS")

                    Call mt_FlujoTabs("EnvioSMS")

                Case "Asistencia"
                    If Not fu_ValidarActividadAsistencia() Then Exit Sub

                    Call mt_LimpiarControlesAsistencia()

                    me_ActividadEvento = md_ActividadEvento.GetActividadEvento(Session("frmActividadEvento-codigo_aev"))

                    If me_ActividadEvento.codigo_aev = 0 Then Call mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.error) : Exit Sub

                    Me.txtEventoAsistencia.Text = Session("frmActividadEvento-nombre_evento")
                    Me.txtActividadAsistencia.Text = me_ActividadEvento.nombre_aev

                    Call mt_UpdatePanel("Asistencia")                    

                    Call mt_FlujoTabs("Asistencia")                                        

                Case "Programacion"
                    Call mt_ListarProgramaciones(Session("frmActividadEvento-codigo_aev"))

                    Call mt_LimpiarControlesProgramacion()

                    Call mt_UpdatePanel("RegistroProgramacion")

                    Call mt_FlujoTabs("ListadoProgramacion")

                    Call mt_FlujoModal("Programacion", "open")
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try            
            If Not mt_RegistrarActividadEvento(Session("frmActividadEvento-codigo_aev")) Then Exit Sub

            Call mt_FlujoModal("Actividad", "close")            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call mt_FlujoTabs("Listado")

            Me.txtEvento.Text = String.Empty
            Me.grwActividad.DataSource = Nothing : Me.grwActividad.DataBind()

            Call mt_UpdatePanel("Actividad")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Call mt_LimpiarControlesModal()

            Session("frmActividadEvento-codigo_aev") = 0

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoModal("Actividad", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnEnviarSMS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarSMS.Click
        Try
            If Not mt_EnviarSMS() Then Exit Sub            

            Call mt_ListarActividades(Session("frmActividadEvento-codigo_cco"))

            Call mt_FlujoTabs("Actividad")            

            Call mt_LimpiarControlesEnvioSMS()

            Call mt_UpdatePanel("EnvioSMS")

            Me.grwParticipantesSMS.DataSource = Nothing : Me.grwParticipantesSMS.DataBind()

            Call mt_UpdatePanel("ParticipantesSMS")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirSMS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirSMS.Click
        Try            
            Call mt_FlujoTabs("Actividad")

            Call mt_LimpiarControlesEnvioSMS()

            Call mt_UpdatePanel("EnvioSMS")

            Me.grwParticipantesSMS.DataSource = Nothing : Me.grwParticipantesSMS.DataBind()

            Call mt_UpdatePanel("ParticipantesSMS")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirAsistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirAsistencia.Click
        Try            
            Call mt_FlujoTabs("Actividad")

            Call mt_LimpiarControlesAsistencia()

            Call mt_UpdatePanel("Asistencia")
            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarAsistencia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAsistencia.Click
        Try            
            If Not mt_RegistrarAsistencia() Then Exit Sub
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListarProgramacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarProgramacion.Click
        Try
            Call mt_ListarProgramaciones(Session("frmActividadEvento-codigo_aev"))
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevaProgramacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevaProgramacion.Click
        Try           
            If Me.grwListaProgramacion.Rows.Count > 0 Then
                mt_ShowMessage("La actividad solo puede tener una programación asignada.", MessageType.warning)
                Exit Sub
            Else
                Call mt_LimpiarControlesProgramacion()

                Session("frmActividadEvento-codigo_apr") = 0

                Call mt_UpdatePanel("RegistroProgramacion")

                Call mt_FlujoTabs("RegistroProgramacion")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirProgramacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirProgramacion.Click
        Try
            Call mt_FlujoTabs("ListadoProgramacion")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardarProgramacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarProgramacion.Click
        Try
            If Not mt_RegistrarProgramacion(Session.Item("frmActividadEvento-codigo_apr")) Then Exit Sub

            Call mt_FlujoTabs("ListadoProgramacion")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwListaProgramacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaProgramacion.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmActividadEvento-codigo_apr") = Me.grwListaProgramacion.DataKeys(index).Values("codigo_apr")

            Select Case e.CommandName
                Case "Editar"
                    Call mt_LimpiarControlesProgramacion()

                    Call mt_CargarFormularioProgramacion(Session("frmActividadEvento-codigo_apr"))

                    Call mt_UpdatePanel("RegistroProgramacion")

                    Call mt_FlujoTabs("RegistroProgramacion")

                Case "Eliminar"
                    Call mt_EliminarProgramacion(Session("frmActividadEvento-codigo_apr"))

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    '--------------- olluen 27/02/2020 -----------------
    Protected Sub lbBuscaPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBuscaPersona.Click
        Try
            If ddTipoPart.SelectedValue = 0 Then
                Call mt_ShowMessage("Debe seleccionar Tipo de participante", MessageType.warning)
                Me.ddTipoPart.Focus()
            Else
                mt_BuscarPersona("DNIE", Me.txtNroDoc.Text.Trim)
                Me.udpInscripcion.Update()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
        
    End Sub

    Protected Sub lbGuardaInsc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardaInsc.Click

        Dim obj As New ClsConectarDatos
        Dim dtParticipacion As New Data.DataTable
        Dim dtResp As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        'validaciones
        If Me.ddTipoPart.SelectedValue = 0 Then
            Call mt_ShowMessage("Debe seleccionar el tipo de participante", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.txtNroDoc.Text = "" Then
            Call mt_ShowMessage("Ingrese el nro del documento", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.txtApPat.Text = "" Then
            Call mt_ShowMessage("Ingrese el apellido Paterno", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.txtNombres.Text = "" Then
            Call mt_ShowMessage("Ingrese el nombre", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.txtNombres.Text = "" Then
            Call mt_ShowMessage("Ingrese el Nro. de celular", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.chkLabora.Checked = False And Me.txtEmpAlum.Text = "" Then
            Call mt_ShowMessage("Ingrese el nombre de la empresa", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.chkLabora.Checked = False And Me.txtCargAlum.Text = "" Then
            Call mt_ShowMessage("Ingrese el cargo que ocupa", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub
        ElseIf Me.chkLabora.Checked = False And Me.rbModLabora.SelectedValue = "" Then
            Call mt_ShowMessage("Selecciones el modo que ingresó a laborar", MessageType.warning)
            Me.ddTipoPart.Focus()
            Exit Sub

        End If

        ' fin de validaciones
        Try
            obj.AbrirConexion()
            dtResp = obj.TraerDataTable("ALUMNI_PreInscripcionEventoRapido", Me.hftoken_dev.Value, Me.hfCodigo_cco.Value, Me.dpTipoDoc.SelectedValue, Me.txtNroDoc.Text, Me.txtApPat.Text, Me.txtApMat.Text, Me.txtNombres.Text, "", "", "", Me.ddTipoPart.SelectedValue, Me.txtCel.Text, "0", IIf(Me.chkLabora.Checked, 1, 0), Me.txtEmpAlum.Text, Me.txtCargAlum.Text, Me.txtDirEmp.Text, Me.txtTelEmp.Text, Me.txtEmailEmp.Text, Me.rbModLabora.SelectedValue)
            If dtResp.Rows.Count > 0 Then
                lblmensaje0.Text = dtResp.Rows(0).Item("cod")
                If lblmensaje0.Text = "-2" Then
                    Call mt_ShowMessage("Ya se encuentra registrado en este evento", MessageType.warning)
                    Call mt_limpiaInscripcionAlumno(False)
                    Call mt_habilitaLabora(True)
                    Me.chkLabora.Checked = False
                    Me.lblmensaje0.Text = ""
                    'Me.hfCodigo_cco.Value = ""
                    'Me.hftoken_dev.Value = ""
                Else
                    Call mt_ShowMessage("La Inscripción se realizó con éxito", MessageType.success)
                    Call mt_limpiaInscripcionAlumno(False)
                    Me.txtNroDoc.Text = ""
                    Me.ddTipoPart.SelectedValue = 0
                    Call mt_habilitaLabora(True)
                    Me.chkLabora.Checked = False
                    Me.lblmensaje0.Text = ""
                    'Me.hfCodigo_cco.Value = ""
                    'Me.hftoken_dev.Value = ""
                End If
            End If
            Me.udpInscripcion.Update()
            obj.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Protected Sub ddTipoPart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddTipoPart.SelectedIndexChanged
        If Me.ddTipoPart.SelectedValue = 3 Or ddTipoPart.SelectedValue = 1 Then
            mt_controlesBusqueda(False)
        Else
            mt_controlesBusqueda(True)
        End If
        Call mt_limpiaInscripcionAlumno(False)
        Me.udpInscripcion.Update()
    End Sub

    Protected Sub chkLabora_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLabora.CheckedChanged
        If Me.chkLabora.Checked = False Then
            Call mt_habilitaLabora(True)
        Else
            Call mt_habilitaLabora(False)
        End If
        Me.udpInscripcion.Update()
    End Sub

    Protected Sub lbSalirInsc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSalirInsc.Click
        Try
            Call mt_FlujoTabs("Listado")
            mt_limpiaInscripcionAlumno(False)
            mt_habilitaLabora(False)
            Me.txtNroDoc.Text = String.Empty
            Me.ddTipoPart.SelectedValue = 0
            udpInscripcion.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbSalirVerInsc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSalirVerInsc.Click
        Try
            Call mt_FlujoTabs("Listado")

            Me.txtEventoVerInsc.Text = String.Empty
            Me.gvListaVerInscritos.DataSource = Nothing : Me.gvListaVerInscritos.DataBind()

            Call mt_UpdatePanel("Actividad")
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
                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "Actividad"
                    Me.udpActividad.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpActividadUpdate", "udpActividadUpdate();", True)

                Case "Asistencia"
                    Me.udpAsistencia.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpAsistenciaUpdate", "udpAsistenciaUpdate();", True)

                Case "EnvioSMS"
                    Me.udpEnvioSMS.Update()

                Case "ParticipantesSMS"
                    Me.udpParticipantesSMS.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpParticipantesSMSUpdate", "udpParticipantesSMSUpdate();", True)

                Case "ListaProgramacion"
                    Me.udpListaProgramacion.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaProgramacionUpdate", "udpListaProgramacionUpdate();", True)

                Case "RegistroProgramacion"
                    Me.udpRegistroProgramacion.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroProgramacionUpdate", "udpRegistroProgramacionUpdate();", True)

                Case "VerInscritos"
                    Me.udpVerInscritos.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpVerInscritosUpdate", "udpVerInscritosUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "Actividad"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('actividad-tab');", True)

                Case "EnvioSMS"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('enviosms-tab');", True)

                Case "Asistencia"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('asistencia-tab');", True)

                Case "ListadoProgramacion"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('listaProgramacion-tab');", True)

                Case "RegistroProgramacion"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabsModal", "flujoTabsModal('registroProgramacion-tab');", True)
                    '--------------- olluen 27/02/2020 -----------------
                Case "Inscripcion"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('inscripcion-tab');", True)

                Case "VerInscritos"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('verInscritos-tab');", True)

                    '---------------------------------------------------


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

    Private Sub mt_CargarComboTipo()
        Try
            Dim dt As New Data.DataTable
            dt = md_ActividadEvento.BuscarActividades()

            Call md_Funciones.CargarCombo(Me.cmbTipo, dt, "codigo_Act", "descripcion_Act", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    '--------------- olluen 27/02/2020 -----------------
    Private Sub mt_CargarComboTipoParticipante()
        Try
            Dim dt As New Data.DataTable : me_TipoParticipante = New e_TipoParticipante

            With me_TipoParticipante
                .operacion = "GEN"
                .codigo_tpar = "1, 3, 6"
            End With

            dt = md_TipoParticipante.ListarTipoParticipante(me_TipoParticipante)
            Call md_Funciones.CargarCombo(Me.ddTipoPart, dt, "codigo_tpar", "descripcion_tpar", True, "[---Seleccione---]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    '--------------- olluen 27/02/2020 -----------------
    Private Sub mt_BuscarPersona(ByVal tipo As String, ByVal valor As String, Optional ByVal mostrardni As Boolean = False)

        Dim obj As New ClsConectarDatos
        Dim ExistePersona As Boolean = False

        Try
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim tbl As Data.DataTable

            obj.AbrirConexion()
            tbl = obj.TraerDataTable("PERSON_ConsultarPersona", tipo, valor)
            If tbl.Rows.Count > 0 Then
                mt_controlesBusqueda(False)
                ExistePersona = True
            Else
                Call mt_ShowMessage("No se encontró ninguna coicidencia", MessageType.warning)
                mt_limpiaInscripcionAlumno(False)
                Me.txtNroDoc.Focus()
                Exit Sub
            End If
            obj.CerrarConexion()
            obj = Nothing

            If ExistePersona = True Then
                'olf Me.hdcodigo_pso.Value = tbl.Rows(0).Item("codigo_Pso")
                If mostrardni = True Then
                    If tbl.Rows(0).Item("numeroDocIdent_Pso").ToString <> "" Then
                        Me.txtNroDoc.Text = tbl.Rows(0).Item("numeroDocIdent_Pso").ToString
                        Me.txtNroDoc.Enabled = False
                    Else
                        Me.txtNroDoc.Enabled = True
                    End If
                End If

                Me.txtApPat.Text = tbl.Rows(0).Item("apellidoPaterno_Pso").ToString
                Me.txtApMat.Text = tbl.Rows(0).Item("apellidoMaterno_Pso").ToString
                Me.txtNombres.Text = tbl.Rows(0).Item("nombres_Pso").ToString
                Me.txtCel.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString


                If tbl.Rows(0).Item("fechanacimiento_pso").ToString <> "" Then

                    'olluen Me.txtFechaNac.Text = CDate(tbl.Rows(0).Item("fechaNacimiento_Pso")).ToShortDateString
                End If

                'olluen Me.dpSexo.SelectedValue = -1

                If (tbl.Rows(0).Item("sexo_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("sexo_Pso").ToString.Trim <> "") Then
                    'olluen Me.dpSexo.SelectedValue = tbl.Rows(0).Item("sexo_Pso").ToString.ToUpper
                End If

                If (tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "CE.") Then

                    Me.dpTipoDoc.SelectedIndex = 1
                Else
                    'Inicio Hcano 27-06-17
                    If tbl.Rows(0).Item("tipoDocIdent_Pso").ToString = "PASAPORTE" Then
                        Me.dpTipoDoc.SelectedValue = "PAS"
                    Else
                        Me.dpTipoDoc.SelectedValue = tbl.Rows(0).Item("tipoDocIdent_Pso").ToString
                    End If
                    'Fin Hcano 27-06-17

                End If
                ' ''Me.txtemail1.Text = tbl.Rows(0).Item("emailPrincipal_Pso").ToString
                ' ''Me.txtemail2.Text = tbl.Rows(0).Item("emailAlternativo_Pso").ToString
                ' ''Me.txtdireccion.Text = tbl.Rows(0).Item("direccion_Pso").ToString
                ' ''Me.txttelefono.Text = tbl.Rows(0).Item("telefonoFijo_Pso").ToString
                ' ''Me.txtcelular.Text = tbl.Rows(0).Item("telefonoCelular_Pso").ToString

                ' ''Me.dpEstadoCivil.SelectedValue = -1

                If (tbl.Rows(0).Item("estadoCivil_Pso") Is System.DBNull.Value = False) AndAlso (tbl.Rows(0).Item("estadoCivil_Pso").ToString.Trim <> "") Then
                    'olluen Me.dpEstadoCivil.SelectedValue = tbl.Rows(0).Item("estadoCivil_Pso").ToString.ToUpper
                End If

                'olluen Me.txtruc.Text = tbl.Rows(0).Item("nroRuc_Pso").ToString

                'Si hay pais extranjero registrado para la persona
                If tbl.Rows(0).Item("codigo_Pai").ToString <> "" Then
                    If tbl.Rows(0).Item("codigo_Pai").ToString <> 156 Then
                        'olluen Me.dppais.SelectedValue = tbl.Rows(0).Item("codigo_Pai")
                    End If
                End If

                'Si hay distrito registrado para la persona
                If tbl.Rows(0).Item("codigo_dep").ToString <> "" Then
                    If tbl.Rows(0).Item("codigo_dep") = 26 Or tbl.Rows(0).Item("codigo_dep").ToString = "" Then
                        'olluen Me.dpdepartamento.SelectedValue = -1
                    Else
                        'olluen Me.dpdepartamento.SelectedValue = tbl.Rows(0).Item("codigo_dep")
                    End If
                    If tbl.Rows(0).Item("codigo_pro") = 1 Or tbl.Rows(0).Item("codigo_pro").ToString = "" Then
                        'olluen Me.dpprovincia.SelectedValue = -1
                        ' ''ElseIf Me.dpprovincia.Items.Count > 0 Then
                        ' ''    Me.dpprovincia.SelectedValue = tbl.Rows(0).Item("codigo_pro")
                        ' ''End If

                        ' ''If tbl.Rows(0).Item("codigo_dis") = 1 Or tbl.Rows(0).Item("codigo_dis").ToString = "" Then
                        ' ''    'olluen Me.dpdistrito.SelectedValue = -1
                        ' ''ElseIf Me.dpdistrito.Items.Count > 0 Then
                        ' ''    'olluen Me.dpdistrito.SelectedValue = tbl.Rows(0).Item("codigo_dis")
                        ' ''End If
                    End If

                    'Buscar si tiene DEUDAS
                    '## Modificado por mvillavicencio 06/08/2012. 
                    'Se puede registrar si tiene 1 o 0 deudas

                    'olluen EvaluarDeuda()
                    'Bloque nombres cuando no es Administrador
                    'olluenDesbloquearNombres(False)
                    'olluenDesbloquearOtrosDatos(True)
                    'olluenlnkComprobarNombres.Visible = False
                ElseIf ValidarNroIdentidad() = True Then
                    'olluen Me.hdcodigo_pso.Value = 0
                    'olluen lnkComprobarNombres.Visible = True
                    'olluen DesbloquearNombres(True)
                    'olluenDesbloquearOtrosDatos(False)

                    If Me.txtApPat.Enabled = True Then
                        Me.txtApPat.Focus()
                    End If
                End If
                tbl.Dispose()
                tbl = Nothing
            End If
        Catch ex As Exception
            'olluen
            Me.lblmensaje0.Text = "Error: " & ex.Message
            Me.lblmensaje0.Visible = True
            'obj.CerrarConexion()
            'obj = Nothing
        End Try
    End Sub
    '--------------- olluen 27/02/2020 -----------------
    Private Function ValidarNroIdentidad(Optional ByVal IrAlFoco As Boolean = True) As Boolean
        'Limpiar txt
        Me.lblmensaje0.Text = ""
        Me.lblmensaje0.Visible = False
        'Validar DNI
        If Me.dpTipoDoc.SelectedIndex = 0 Then
            If Me.txtNroDoc.Text.Length <> 8 OrElse IsNumeric(Me.txtNroDoc.Text.Trim) = False OrElse Me.txtNroDoc.Text = "00000000" Then
                Me.lblmensaje0.Text = "El número de DNI es incorrecto. Mínimo 8 caracteres"
                Me.lblmensaje0.Visible = True
                ValidarNroIdentidad = False
                If IrAlFoco = True Then Me.txtNroDoc.Focus()
                'Response.Write(1)
            Else
                'Response.Write(2)
                ValidarNroIdentidad = True
            End If
        ElseIf Me.dpTipoDoc.SelectedIndex = 2 And Me.txtNroDoc.Text.Length < 9 Then
            Me.lblmensaje0.Text = "El número de pasaporte es incorrecto. Mínimo 9 caracteres"
            Me.lblmensaje0.Visible = True
            ValidarNroIdentidad = False
            If IrAlFoco = True Then Me.txtNroDoc.Focus()
            'Response.Write(3)
        Else
            'Response.Write(4)
            'Me.lblmensaje.Text = "OK -"
            ValidarNroIdentidad = True
        End If
    End Function

    Private Sub mt_CargarComboServicioConcepto()
        Try
            Dim dt As New Data.DataTable : me_ActividadEvento = New e_ActividadEvento
            me_ActividadEvento.codigo_cco = Session("frmActividadEvento-codigo_cco")

            dt = md_ActividadEvento.BuscarServicioConcepto(me_ActividadEvento)
            Call md_Funciones.CargarCombo(Me.cmbServicioConcepto, dt, "codigo_sco", "descripcion_sco", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesModal()
        Try
            Me.txtActividad.Text = String.Empty            
            Me.cmbTipo.SelectedValue = "0"
            Me.cmbServicioConcepto.SelectedValue = "0"
            Me.txtCosto.Text = "0.00"
            Me.txtCupos.Text = 0
            Me.txtInscritos.Text = 0
            Me.txtEncuesta.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesEnvioSMS()
        Try
            Me.txtEventoSMS.Text = String.Empty
            Me.txtActividadSMS.Text = String.Empty
            Me.txtEncuestaSMS.Text = String.Empty
            Me.txtParticipantesSMS.Text = String.Empty
            Me.txtTotalSMS.Text = String.Empty
            Me.txtMensajeSMS.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesAsistencia()
        Try
            Me.txtEventoAsistencia.Text = String.Empty
            Me.txtActividadAsistencia.Text = String.Empty
            Me.txtFechaAsistencia.Text = Day(Now).ToString & "/" & Month(Now).ToString & "/" & Year(Now).ToString
            Me.txtDocumentoAsistencia.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControlesProgramacion()
        Try
            Me.txtLugarProgramacion.Text = String.Empty
            Me.txtFechaProgramacion.Text = String.Empty
            Me.txtHoraInicio.Text = ""
            Me.txtHoraFin.Text = ""
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmActividadEvento-codigo_cco") = Nothing
            Session("frmActividadEvento-nombre_evento") = Nothing
            Session("frmActividadEvento-codigo_aev") = Nothing
            Session("frmActividadEvento-codigo_apr") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            me_CentroCostos = New e_CentroCostos
            Dim dt As New Data.DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_CentroCostos
                .anio = String.Empty
                .descripcion_cco = Me.txtDescripcionFiltro.Text.Trim                
            End With

            dt = md_CentroCostos.ListarEventosAlumni(me_CentroCostos)
            Me.grwLista.DataSource = dt

            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ListarActividades(ByVal codigo_cco As String)
        Try
            me_ActividadEvento = New e_ActividadEvento
            Dim dt As New Data.DataTable

            If Me.grwActividad.Rows.Count > 0 Then Me.grwActividad.DataSource = Nothing : Me.grwActividad.DataBind()

            With me_ActividadEvento
                .operacion = "GEN"
                .codigo_cco = codigo_cco
            End With            

            dt = md_ActividadEvento.ListarActividadEvento(me_ActividadEvento)
            Me.grwActividad.DataSource = dt

            Me.grwActividad.DataBind()

            Call md_Funciones.AgregarHearders(grwActividad)

            Call mt_UpdatePanel("Actividad")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarActividadEvento(ByVal codigo_aev As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarActividadEvento() Then Return False

            me_ActividadEvento = md_ActividadEvento.GetActividadEvento(codigo_aev)

            With me_ActividadEvento
                .operacion = "I"
                .codigo_aev = codigo_aev
                .codigo_cco = Session("frmActividadEvento-codigo_cco")
                .nombre_aev = Me.txtActividad.Text.Trim.ToUpper
                .codigo_act = Me.cmbTipo.SelectedValue
                .codigo_sco = Me.cmbServicioConcepto.SelectedValue
                .costo_aev = Me.txtCosto.Text.Trim
                .cupos_aev = Me.txtCupos.Text.Trim
                .urlEncuesta_aev = Me.txtEncuesta.Text.Trim
            End With

            md_ActividadEvento.RegistrarActividadEvento(me_ActividadEvento)

            Call mt_ShowMessage("¡Los datos han sido registrados correctamente!", MessageType.success)

            Call mt_ListarActividades(Session("frmActividadEvento-codigo_cco"))

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarActividadEvento() As Boolean
        Try
            If Session.Item("frmActividadEvento-codigo_cco") Is Nothing OrElse Session("frmActividadEvento-codigo_cco") = 0 Then mt_ShowMessage("El código de evento no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtActividad.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre de actividad.", MessageType.warning) : Me.txtActividad.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbTipo.SelectedValue) OrElse Me.cmbTipo.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un tipo.", MessageType.warning) : Me.cmbTipo.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtCosto.Text.Trim) Then mt_ShowMessage("Debe ingresar un costo válido.", MessageType.warning) : Me.txtCosto.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtCupos.Text.Trim) OrElse Me.txtCupos.Text.Trim <= 0 Then mt_ShowMessage("Debe ingresar una cantidad de cupos válida.", MessageType.warning) : Me.txtCupos.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_ListarParticipantes(ByVal codigo_aev As String)
        Try
            me_Participacion = New e_Participacion
            Dim dt As New Data.DataTable

            If Me.grwParticipantesSMS.Rows.Count > 0 Then Me.grwParticipantesSMS.DataSource = Nothing : Me.grwParticipantesSMS.DataBind()

            With me_Participacion
                .operacion = "LIS"
                .codigo_aev = codigo_aev
            End With            

            dt = md_Participacion.ListarParticipacion(me_Participacion)

            Me.grwParticipantesSMS.DataSource = dt

            Me.grwParticipantesSMS.DataBind()

            Call md_Funciones.AgregarHearders(grwParticipantesSMS)

            Call mt_UpdatePanel("ParticipantesSMS")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_EnviarSMS() As Boolean
        Try            
            If Not fu_ValidarEnviarSMS() Then Return False

            me_EnvioSMS = New e_EnvioSMS

            With me_EnvioSMS
                .operacion = "I"
                .cod_user = cod_user
                .codigo_env = "0"
                .tabla_env = "ActividadEvento"
                .codigoTabla_env = Session("frmActividadEvento-codigo_aev")
                .codigo_per = cod_user
                .tipo_env = "M" 'Manual
                .mensaje_env = Me.txtMensajeSMS.Text.Trim & " Enlace: " & Me.txtEncuestaSMS.Text.Trim
            End With

            If Me.grwParticipantesSMS.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwParticipantesSMS.Rows
                    If Fila.Cells(1).Text.Trim <> "NO PRESENTA" Then
                        Dim le_EnvioSMSDetalle As New e_EnvioSMSDetalle

                        With le_EnvioSMSDetalle
                            .codigo_pso = Me.grwParticipantesSMS.DataKeys(Fila.RowIndex).Item("codigo_Pso")
                            .nombre_end = Fila.Cells(0).Text.Trim
                            .celular_end = Fila.Cells(1).Text.Trim
                        End With                                                

                        me_EnvioSMS.detalles.Add(le_EnvioSMSDetalle)
                    End If
                Next
            End If

            md_EnvioSMS.EnviarSMSEventoAlumni(me_EnvioSMS)

            Call mt_ShowMessage("¡El envío de mensajes se realizo exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)        
        End Try
    End Function

    Private Function fu_ValidarEnviarSMS() As Boolean
        Try            
            Dim lb_ExisteCelular As Boolean = False

            If String.IsNullOrEmpty(Me.txtMensajeSMS.Text.Trim) Then mt_ShowMessage("Debe ingresar un mensaje.", MessageType.warning) : Me.txtMensajeSMS.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtEncuestaSMS.Text.Trim) Then mt_ShowMessage("Debe ingresar una URL de encuesta.", MessageType.warning) : Me.txtEncuestaSMS.Focus() : Return False

            If Session.Item("frmActividadEvento-codigo_aev") Is Nothing OrElse Session("frmActividadEvento-codigo_aev") = 0 Then mt_ShowMessage("El código de la actividad no ha sido encontrado.", MessageType.warning) : Return False

            If Me.grwParticipantesSMS.Rows.Count > 0 Then
                For Each Fila As GridViewRow In Me.grwParticipantesSMS.Rows
                    If Fila.Cells(1).Text.Trim <> "NO PRESENTA" Then
                        lb_ExisteCelular = True
                        Exit For
                    End If
                Next                
            End If
                                    
            If Not lb_ExisteCelular Then mt_ShowMessage("No hay números de celular para realizar el envío.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarAsistencia() As Boolean
        Try            
            If Not fu_ValidarRegistrarAsistencia() Then Return False

            Dim dt As Data.DataTable
            me_Participacion = New e_Participacion

            me_Participacion.numero_doc = txtDocumentoAsistencia.Text.Trim

            dt = md_Participacion.BuscarPersonaXDocumento(me_Participacion)
            If dt.Rows.Count = 0 Then mt_ShowMessage("El número de documento no se encuentra registrado.", MessageType.warning) : Me.txtDocumentoAsistencia.Focus() : Exit Function

            With me_Participacion
                .codigo_cco = Session("frmActividadEvento-codigo_cco")
                .codigo_aev = Session("frmActividadEvento-codigo_aev")
                .codigo_pso = dt.Rows(0).Item("codigo_Pso")
                .observacion_par = "Asistencia a Evento " & txtEventoAsistencia.Text.Trim
                .fechahora_par = Me.txtFechaAsistencia.Text.Trim & " " & Hour(Now) & " : " & Minute(Now) & " : " & Second(Now)
            End With

            md_Participacion.RegistrarParticipacion(me_Participacion)

            Call mt_ShowMessage("¡La asistencia se registro exitosamente!", MessageType.success)

            Me.txtDocumentoAsistencia.Text = String.Empty

            Call mt_UpdatePanel("Asistencia")            

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarActividadAsistencia() As Boolean
        Try
            me_ActividadEvento = New e_ActividadEvento
            Dim dt As New Data.DataTable

            me_ActividadEvento.codigo_cco = Session("frmActividadEvento-codigo_cco")

            dt = md_ActividadEvento.ConsultarConfiguracionAsistencia(me_ActividadEvento)

            If dt.Rows.Count = 0 Then mt_ShowMessage("El evento no ha sido configurado para registrar asistencia.", MessageType.warning) : Return False
            If Not dt.Rows(0).Item("asistencia_dev") = True Then mt_ShowMessage("El evento no ha sido configurado para registrar asistencia.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarAsistencia() As Boolean
        Try
            Dim dt As Data.DataTable
            Dim codigo_pso As String = String.Empty
            Dim ciclo_aca As String = String.Empty

            If Session.Item("frmActividadEvento-codigo_aev") Is Nothing OrElse Session("frmActividadEvento-codigo_aev") = 0 Then mt_ShowMessage("El código de la actividad no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtFechaAsistencia.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtDocumentoAsistencia.Text.Trim) Then mt_ShowMessage("Debe ingresar un número de documento.", MessageType.warning) : Me.txtDocumentoAsistencia.Focus() : Return False

            'Consultar si hay una programación para la fecha seleccionada
            dt = New Data.DataTable
            me_ActividadProgramacion = New e_ActividadProgramacion

            me_ActividadProgramacion.operacion = "GEN"
            me_ActividadProgramacion.codigo_aev = Session("frmActividadEvento-codigo_aev")
            me_ActividadProgramacion.fecha = Me.txtFechaAsistencia.Text.Trim

            dt = md_ActividadProgramacion.ListarActividadProgramacion(me_ActividadProgramacion)

            If dt.Rows.Count = 0 Then mt_ShowMessage("La actividad no presenta una programación para la fecha seleccionada.", MessageType.warning) : Return False

            'Consultar si la persona esta inscrito
            dt = New Data.DataTable
            me_Participacion = New e_Participacion

            me_Participacion.numero_doc = txtDocumentoAsistencia.Text.Trim

            dt = md_Participacion.BuscarPersonaXDocumento(me_Participacion)
            If dt.Rows.Count = 0 Then mt_ShowMessage("El número de documento no se encuentra registrado.", MessageType.warning) : Me.txtDocumentoAsistencia.Focus() : Return False
            codigo_pso = dt.Rows(0).Item("codigo_Pso")

            'Consultar ciclo academico
            dt = New Data.DataTable            

            dt = md_CicloAcademico.ObtenerCicloAcademicoActual()
            If dt.Rows.Count > 0 Then ciclo_aca = dt.Rows(0).Item("codigo_Cac")

            'Consultar si presenta deuda
            dt = New Data.DataTable
            me_Participacion = New e_Participacion

            With me_Participacion
                .codigo_pso = codigo_pso
                .ciclo_aca = ciclo_aca
                .codigo_cco = Session("frmActividadEvento-codigo_cco")
            End With

            dt = md_Participacion.BuscarDeuda(me_Participacion)
            If dt.Rows.Count = 0 Then mt_ShowMessage("La persona no se encuentra registrada en este evento.", MessageType.warning) : Return False

            'Verificamos si las lineas de deuda son 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If (Double.Parse(dt.Rows(i).Item("saldo_Deu").ToString) > 0) And (Double.Parse(dt.Rows(i).Item("montopagoweb").ToString) = 0) Then
                    mt_ShowMessage("La persona tiene deuda pendiente, debe cancelar en caja.", MessageType.warning)
                    Return False
                    Exit For
                End If
            Next

            'Buscamos cruce de horarios
            Dim valor_cruce As Integer
            me_Participacion = New e_Participacion

            With me_Participacion
                .codigo_cco = 0
                .codigo_pso = codigo_pso
                .codigo_aev = Session("frmActividadEvento-codigo_aev")
            End With

            valor_cruce = md_Participacion.BuscarCruceHorario(me_Participacion)

            If valor_cruce = 1 Then mt_ShowMessage("La persona ya está participando de esta actividad.", MessageType.warning) : Return False
            If valor_cruce = 2 Then mt_ShowMessage("La persona está participando en otra actividad durante estas horas.", MessageType.warning) : Return False
            If valor_cruce <> 0 Then mt_ShowMessage("Existe un cruce de tipo no definido.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_ListarProgramaciones(ByVal codigo_aev As Integer)
        Try
            me_ActividadProgramacion = New e_ActividadProgramacion
            Dim dt As New Data.DataTable

            If Me.grwListaProgramacion.Rows.Count > 0 Then Me.grwListaProgramacion.DataSource = Nothing : Me.grwListaProgramacion.DataBind()

            With me_ActividadProgramacion
                .operacion = "GEN"
                .codigo_aev = codigo_aev
                .fecha = "01/01/1901"
            End With

            dt = md_ActividadProgramacion.ListarActividadProgramacion(me_ActividadProgramacion)

            Me.grwListaProgramacion.DataSource = dt

            Me.grwListaProgramacion.DataBind()

            Call md_Funciones.AgregarHearders(grwListaProgramacion)

            Call mt_UpdatePanel("ListaProgramacion")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioProgramacion(ByVal codigo_apr As Integer) As Boolean
        Try
            If codigo_apr = 0 Then mt_ShowMessage("El registro de programación no ha sido encontrado.", MessageType.warning) : Return False

            me_ActividadProgramacion = md_ActividadProgramacion.GetActividadProgramacion(codigo_apr)

            With me_ActividadProgramacion
                Me.txtLugarProgramacion.Text = .lugar_apr
                Me.txtFechaProgramacion.Text = .fechahoraini_apr.Substring(0, 10)
                Me.txtHoraInicio.Text = .fechahoraini_apr.Substring(11, 5)
                Me.txtHoraFin.Text = .fechahorafin_apr.Substring(11, 5)
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarProgramacion(ByVal codigo_apr As String) As Boolean
        Try
            If Not fu_ValidarRegistrarProgramacion() Then Return False

            me_ActividadProgramacion = New e_ActividadProgramacion

            With me_ActividadProgramacion
                .operacion = "I"
                .codigo_apr = codigo_apr
                .codigo_aev = Session.Item("frmActividadEvento-codigo_aev")
                .fechahoraini_apr = Date.Parse(Me.txtFechaProgramacion.Text.Trim & " " & Me.txtHoraInicio.Text)
                .fechahorafin_apr = Date.Parse(Me.txtFechaProgramacion.Text.Trim & " " & Me.txtHoraFin.Text)
                .lugar_apr = Me.txtLugarProgramacion.Text.Trim
            End With

            md_ActividadProgramacion.RegistrarActividadProgramacion(me_ActividadProgramacion)

            Call mt_ShowMessage("¡Los datos han sido registrados correctamente!", MessageType.success)

            Call mt_ListarProgramaciones(Session.Item("frmActividadEvento-codigo_aev"))

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarProgramacion(ByVal codigo_apr As String) As Boolean
        Try
            me_ActividadProgramacion = md_ActividadProgramacion.GetActividadProgramacion(codigo_apr)

            With me_ActividadProgramacion
                .operacion = "D"
                .codigo_apr = codigo_apr
            End With

            md_ActividadProgramacion.RegistrarActividadProgramacion(me_ActividadProgramacion)

            Call mt_ShowMessage("¡La programación ha sido eliminada correctamente!", MessageType.success)

            Call mt_ListarProgramaciones(Session.Item("frmActividadEvento-codigo_aev"))

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarProgramacion() As Boolean
        Try
            If Session.Item("frmActividadEvento-codigo_aev") Is Nothing OrElse Session("frmActividadEvento-codigo_aev") = 0 Then mt_ShowMessage("El código de actividad no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtLugarProgramacion.Text.Trim) Then mt_ShowMessage("Debe ingresar un lugar.", MessageType.warning) : Me.txtLugarProgramacion.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtFechaProgramacion.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha válida.", MessageType.warning) : Return False

            Try
                Date.Parse(Me.txtFechaProgramacion.Text)
            Catch ex As Exception                
                mt_ShowMessage("Debe ingresar una fecha válida.", MessageType.warning) : Me.txtFechaProgramacion.Text = String.Empty : Return False                
            End Try

            If String.IsNullOrEmpty(Me.txtHoraInicio.Text.Trim) OrElse Me.txtHoraInicio.Text.Trim.Length <> 5 Then mt_ShowMessage("Debe ingresar una hora de inicio válida.", MessageType.warning) : Me.txtHoraInicio.Focus() : Return False

            Try
                Date.Parse(Me.txtFechaProgramacion.Text.Trim & " " & Me.txtHoraInicio.Text)
            Catch ex As Exception
                mt_ShowMessage("Debe ingresar una hora de inicio válida.", MessageType.warning) : Return False
            End Try

            If String.IsNullOrEmpty(Me.txtHoraFin.Text.Trim) OrElse Me.txtHoraFin.Text.Trim.Length <> 5 Then mt_ShowMessage("Debe ingresar una hora de fin válida.", MessageType.warning) : Me.txtHoraFin.Focus() : Return False

            Try
                Date.Parse(Me.txtFechaProgramacion.Text.Trim & " " & Me.txtHoraFin.Text)
            Catch ex As Exception
                mt_ShowMessage("Debe ingresar una hora de fin válida.", MessageType.warning) : Return False
            End Try

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_controlesBusqueda(ByVal estado As Boolean)

        Me.txtApPat.Enabled = estado
        Me.txtApMat.Enabled = estado
        Me.txtNombres.Enabled = estado

    End Sub

    Private Sub mt_habilitaLabora(ByVal estado As Boolean)
        Me.txtEmpAlum.Enabled = estado
        Me.txtCargAlum.Enabled = estado
        Me.txtDirEmp.Enabled = estado
        Me.txtTelEmp.Enabled = estado
        Me.txtEmailEmp.Enabled = estado
        Me.rbModLabora.Enabled = estado
        If estado = False Then
            Me.txtEmpAlum.Text = ""
            Me.txtCargAlum.Text = ""
            Me.txtDirEmp.Text = ""
            Me.txtTelEmp.Text = ""
            Me.txtEmailEmp.Text = ""
        End If
    End Sub

    Private Sub mt_limpiaInscripcionAlumno(ByVal estado As Boolean)
        If estado = False Then
            Me.txtApPat.Text = String.Empty
            Me.txtApMat.Text = String.Empty
            Me.txtNombres.Text = String.Empty
            Me.txtCel.Text = String.Empty
        End If
    End Sub

    Private Function mt_validaInscripcion() As Boolean
        If Me.txtApPat.Text = "" Then
            Return False
        End If
        Return True
    End Function

    Private Sub mt_ListarVerInscritos(ByVal codigo_cco As String)
        Try
            md_TipoParticipante = New d_TipoParticipante
            Dim dt As New Data.DataTable
            dt = md_TipoParticipante.ListarInscritosEvento(codigo_cco)

            'If Me.gvListaVerInscritos.Rows.Count > 0 Then Me.grwParticipantesSMS.DataSource = Nothing : Me.grwParticipantesSMS.DataBind()

            Me.gvListaVerInscritos.DataSource = dt

            Me.gvListaVerInscritos.DataBind()

            Call md_Funciones.AgregarHearders(gvListaVerInscritos)

            Call mt_UpdatePanel("VerInscritos")

            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
