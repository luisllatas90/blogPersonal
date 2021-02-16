Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class GestionEgresado_GraduacionYTitulacion_frmCompletarDatosSustentacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_EscalaCalificacionSustentacion As e_EscalaCalificacionSustentacion
    Dim me_CicloAcademico As e_CicloAcademico
    Dim me_AdscripcionDocente As e_AdscripcionDocente
    Dim me_CompletarDatosSustentacion As e_CompletarDatosSustentacion

    'DATOS
    Dim md_EscalaCalificacionSustentacion As New d_EscalaCalificacionSustentacion
    Dim md_CicloAcademico As New d_CicloAcademico
    Dim md_AdscripcionDocente As New d_AdscripcionDocente
    Dim md_CompletarDatosSustentacion As New d_CompletarDatosSustentacion
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim codigo_tfu As Integer = 0

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
            codigo_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                Call mt_CargarComboCalificativo()
                Call mt_CargarComboCiclo()
                Me.cmbTipoFiltro.SelectedValue = "N"
                'Call mt_LimpiarVariablesSession()
                'Call mt_CargarComboClasificacion()
                'Call mt_CargarComboProfile()
                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmCompletarDatosSustentacion-codigo_alu") = Me.grwLista.DataKeys(index).Values("codigo_alu").ToString
            Session("frmCompletarDatosSustentacion-codigoUniver_alu") = Me.grwLista.DataKeys(index).Values("codigoUniver_alu").ToString
            Session("frmCompletarDatosSustentacion-bachiller") = Me.grwLista.DataKeys(index).Values("bachiller").ToString
            Session("frmCompletarDatosSustentacion-codigo_tes") = Me.grwLista.DataKeys(index).Values("codigo_tes").ToString

            Select Case e.CommandName
                Case "Editar"
                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    Call mt_LimpiarControles("Registro")

                    If Not mt_CargarFormularioRegistro(CInt(Session("frmCompletarDatosSustentacion-codigo_alu"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnPresidente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPresidente.Click
        Try
            mt_MostrarBuscarDocente("P")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSecretario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSecretario.Click
        Try
            mt_MostrarBuscarDocente("S")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnVocal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVocal.Click
        Try
            mt_MostrarBuscarDocente("V")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListarDocente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarDocente.Click
        Try
            Call mt_CargarDatosDocente()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub grwListaDocente_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaDocente.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Seleccionar"
                    If Session("frmCompletarDatosSustentacion-modal") = "P" Then
                        Me.txtCodigoJuradoPresidente.Value = 0
                        Me.txtCodigoPresidente.Value = Me.grwListaDocente.DataKeys(index).Values("codigo_per")
                        Me.txtPresidente.Text = Me.grwListaDocente.DataKeys(index).Values("nombre_per")

                    ElseIf Session("frmCompletarDatosSustentacion-modal") = "S" Then
                        Me.txtCodigoJuradoSecretario.Value = 0
                        Me.txtCodigoSecretario.Value = Me.grwListaDocente.DataKeys(index).Values("codigo_per")
                        Me.txtSecretario.Text = Me.grwListaDocente.DataKeys(index).Values("nombre_per")

                    ElseIf Session("frmCompletarDatosSustentacion-modal") = "V" Then
                        Me.txtCodigoJuradoVocal.Value = 0
                        Me.txtCodigoVocal.Value = Me.grwListaDocente.DataKeys(index).Values("codigo_per")
                        Me.txtVocal.Text = Me.grwListaDocente.DataKeys(index).Values("nombre_per")

                    End If

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoModal("BuscarDocente", "close")
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
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "ListaDocente"
                    Me.udpListaDocente.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaDocenteUpdate", "udpListaDocenteUpdate();", True)

                Case "FiltrosDocente"
                    Me.udpFiltrosDocente.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosDocenteUpdate", "udpFiltrosDocenteUpdate();", True)

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

    Private Sub mt_CargarComboCalificativo()
        Try
            me_EscalaCalificacionSustentacion = New e_EscalaCalificacionSustentacion
            Dim dt As New DataTable

            With me_EscalaCalificacionSustentacion
                .operacion = "GEN"
                .codigo_ecs = 0
                .eliminado_ecs = 0
            End With

            dt = md_EscalaCalificacionSustentacion.ListarEscalaCalificacionSustentacion(me_EscalaCalificacionSustentacion)

            Call md_Funciones.CargarCombo(Me.cmbCalificativo, dt, "codigo_ecs", "descripcion_ecs", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCiclo()
        Try
            me_CicloAcademico = New e_CicloAcademico
            Dim dt As New DataTable

            With me_CicloAcademico
                .operacion = "GEN"                
            End With

            dt = md_CicloAcademico.ListarCicloAcademico(me_CicloAcademico)

            Call md_Funciones.CargarCombo(Me.cmbCicloFiltro, dt, "codigo_cac", "descripcion_cac", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmCompletarDatosSustentacion-modal") = Nothing
            Session("frmCompletarDatosSustentacion-codigo_alu") = Nothing
            Session("frmCompletarDatosSustentacion-codigoUniver_alu") = Nothing
            Session("frmCompletarDatosSustentacion-bachiller") = Nothing
            Session("frmCompletarDatosSustentacion-codigo_tes") = Nothing
            Session("frmCompletarDatosSustentacion-codigo_cds") = Nothing

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.txtCodigo.Text = String.Empty
                    Me.txtEgresado.Text = String.Empty
                    Me.txtNumeroResolucion.Text = String.Empty
                    Me.txtFechaResolucion.Text = String.Empty
                    Me.txtNumeroActa.Text = String.Empty
                    Me.txtFechaActa.Text = String.Empty
                    Me.txtNota.Text = String.Empty
                    Me.cmbCalificativo.SelectedValue = String.Empty

                    Me.txtCodigoJuradoPresidente.Value = 0
                    Me.txtCodigoPresidente.Value = 0
                    Me.txtPresidente.Text = String.Empty

                    Me.txtCodigoJuradoSecretario.Value = 0
                    Me.txtCodigoSecretario.Value = 0
                    Me.txtSecretario.Text = String.Empty

                    Me.txtCodigoJuradoVocal.Value = 0
                    Me.txtCodigoVocal.Value = 0
                    Me.txtVocal.Text = String.Empty

                Case "FiltrosDocente"
                    Me.cmbCicloFiltro.SelectedValue = String.Empty

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_CompletarDatosSustentacion = New e_CompletarDatosSustentacion

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_CompletarDatosSustentacion
                .operacion = "PEN"
                .completado = Me.cmbTipoFiltro.SelectedValue
            End With

            dt = md_CompletarDatosSustentacion.ListarCompletarDatosSustentacion(me_CompletarDatosSustentacion)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_MostrarBuscarDocente(ByVal tipo As String)
        Try
            Call mt_LimpiarControles("FiltrosDocente")
            Call mt_UpdatePanel("FiltrosDocente")

            Me.grwListaDocente.DataSource = Nothing : Me.grwListaDocente.DataBind()
            Call mt_UpdatePanel("ListaDocente")

            Session("frmCompletarDatosSustentacion-modal") = tipo
            Call mt_FlujoModal("BuscarDocente", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosDocente()
        Try
            Dim dt As New DataTable : me_AdscripcionDocente = New e_AdscripcionDocente

            If Me.grwListaDocente.Rows.Count > 0 Then Me.grwListaDocente.DataSource = Nothing : Me.grwListaDocente.DataBind()

            With me_AdscripcionDocente
                .operacion = "ADO"
                .opcion_todos = "N"
                .codigo_cac = Me.cmbCicloFiltro.SelectedValue
            End With

            dt = md_AdscripcionDocente.ListarPersonalDocente(me_AdscripcionDocente)

            Me.grwListaDocente.DataSource = dt
            Me.grwListaDocente.DataBind()

            Call md_Funciones.AgregarHearders(grwListaDocente)

            Call mt_UpdatePanel("ListaDocente")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_alu As Integer) As Boolean
        Try
            Dim dt As New DataTable : me_CompletarDatosSustentacion = New e_CompletarDatosSustentacion

            With me_CompletarDatosSustentacion
                .operacion = "PEN"                
                .codigo_alu = codigo_alu
            End With

            dt = md_CompletarDatosSustentacion.ListarCompletarDatosSustentacion(me_CompletarDatosSustentacion)

            If dt.Rows.Count = 0 Then Call mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.error) : Return False

            Session("frmCompletarDatosSustentacion-codigo_cds") = CInt(dt.Rows(0).Item("codigo_cds").ToString().Trim)
            Session("frmCompletarDatosSustentacion-codigo_alu") = CInt(dt.Rows(0).Item("codigo_alu").ToString().Trim)
            Session("frmCompletarDatosSustentacion-codigoUniver_alu") = dt.Rows(0).Item("codigoUniver_alu").ToString().Trim
            Session("frmCompletarDatosSustentacion-bachiller") = dt.Rows(0).Item("bachiller").ToString().Trim

            Me.txtCodigo.Text = Session("frmCompletarDatosSustentacion-codigoUniver_alu")
            Me.txtEgresado.Text = Session("frmCompletarDatosSustentacion-bachiller")

            If Session("frmCompletarDatosSustentacion-codigo_cds") > 0 Then
                dt = New DataTable

                With me_CompletarDatosSustentacion
                    .operacion = "LIS"
                    .codigo_cds = Session("frmCompletarDatosSustentacion-codigo_cds")
                End With

                dt = md_CompletarDatosSustentacion.ListarCompletarDatosSustentacion(me_CompletarDatosSustentacion)

                If dt.Rows.Count = 0 Then Call mt_ShowMessage("Los datos completados no han sido encontrados.", MessageType.error) : Return False

                Me.txtNumeroResolucion.Text = dt.Rows(0).Item("nroResolcion_cds").ToString().Trim
                Me.txtFechaResolucion.Text = dt.Rows(0).Item("fechaResolucion_cds").ToString().Trim
                Me.txtNumeroActa.Text = dt.Rows(0).Item("nroActa_cds").ToString().Trim
                Me.txtFechaActa.Text = dt.Rows(0).Item("fechaActa_cds").ToString().Trim
                Me.txtNota.Text = dt.Rows(0).Item("calificacion_cds").ToString().Trim
                Me.cmbCalificativo.SelectedValue = IIf(dt.Rows(0).Item("codigo_ecs").ToString().Trim > 0, dt.Rows(0).Item("codigo_ecs").ToString().Trim, String.Empty)

                Me.txtCodigoJuradoPresidente.Value = dt.Rows(0).Item("codigo_jur_presidente").ToString().Trim
                Me.txtCodigoPresidente.Value = dt.Rows(0).Item("codigo_per_presidente").ToString().Trim
                Me.txtPresidente.Text = dt.Rows(0).Item("presidente").ToString().Trim

                Me.txtCodigoJuradoSecretario.Value = dt.Rows(0).Item("codigo_jur_secretario").ToString().Trim
                Me.txtCodigoSecretario.Value = dt.Rows(0).Item("codigo_per_secretario").ToString().Trim
                Me.txtSecretario.Text = dt.Rows(0).Item("secretario").ToString().Trim

                Me.txtCodigoJuradoVocal.Value = dt.Rows(0).Item("codigo_jur_vocal").ToString().Trim
                Me.txtCodigoVocal.Value = dt.Rows(0).Item("codigo_per_vocal").ToString().Trim
                Me.txtVocal.Text = dt.Rows(0).Item("vocal").ToString().Trim            
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrar() As Boolean
        Try
            If Session("frmCompletarDatosSustentacion-codigo_alu") Is Nothing OrElse _
                String.IsNullOrEmpty(Session("frmCompletarDatosSustentacion-codigo_alu")) OrElse _
                CInt(Session("frmCompletarDatosSustentacion-codigo_alu")) = 0 Then mt_ShowMessage("El id de bachiller no ha sido encontrado.", MessageType.warning) : Return False

            If String.IsNullOrEmpty(Me.txtCodigo.Text) Then mt_ShowMessage("El código de bachiller no debe ser vacío.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtEgresado.Text) Then mt_ShowMessage("El nombre de bachiller no debe ser vacío.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNumeroResolucion.Text) Then mt_ShowMessage("Debe ingresar un número de resolución.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtFechaResolucion.Text) Then mt_ShowMessage("Debe ingresar una fecha de resolución.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNumeroActa.Text) Then mt_ShowMessage("Debe ingresar un número de acta.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtFechaActa.Text) Then mt_ShowMessage("Debe ingresar una fecha de acta.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtNota.Text) OrElse CInt(Me.txtNota.Text) <= 0 Then mt_ShowMessage("Debe ingresar una nota válida.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbCalificativo.SelectedValue) Then mt_ShowMessage("Debe seleccionar un calificativo.", MessageType.warning) : Return False
            If CInt(Me.txtCodigoPresidente.Value) <= 0 Then mt_ShowMessage("Debe seleccionar un presidente de jurado.", MessageType.warning) : Return False
            If CInt(Me.txtCodigoSecretario.Value) <= 0 Then mt_ShowMessage("Debe seleccionar un secretario de jurado.", MessageType.warning) : Return False
            If CInt(Me.txtCodigoVocal.Value) <= 0 Then mt_ShowMessage("Debe seleccionar un vocal de jurado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region
    
End Class