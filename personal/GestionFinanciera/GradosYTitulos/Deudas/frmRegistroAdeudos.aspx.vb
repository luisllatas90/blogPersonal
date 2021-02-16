Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class GraduacionTitulacion_Deudas_frmRegistroAdeudos
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_Categoria As e_Categoria
    Dim me_Facultad As e_Facultad
    Dim me_CarreraProfesional As e_CarreraProfesional
    Dim me_Adeudos As e_Adeudos
    Dim me_Alumno As e_Alumno    

    'DATOS
    Dim md_Categoria As New d_Categoria
    Dim md_Facultad As New d_Facultad
    Dim md_CarreraProfesional As New d_CarreraProfesional
    Dim md_Adeudos As New d_Adeudos
    Dim md_Funciones As New d_Funciones
    Dim md_Alumno As New d_Alumno
    Dim md_ConfiguracionInstanciasAdeudos As New d_ConfiguracionInstanciasAdeudos    

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
                Response.Redirect("../../../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()
                Call mt_CargarComboArea()
                Call mt_CargarComboEstado()
                Call mt_CargarComboNivel()
                Call mt_CargarComboFacultad()
                Call mt_CargarComboCarrera()

                Me.cmbEstadoFiltro.SelectedValue = "P"
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbNivelFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNivelFiltro.SelectedIndexChanged
        Try
            Call mt_CargarComboCarrera()

            Call mt_UpdatePanel("FiltrosAlumno")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbFacultadFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFacultadFiltro.SelectedIndexChanged
        Try
            Call mt_CargarComboCarrera()

            Call mt_UpdatePanel("FiltrosAlumno")
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmRegistroAdeudos-codigo_ade") = 0
            Session("frmRegistroAdeudos-operacion") = "Nuevo"

            Call mt_LimpiarControles("Registro")

            Me.cmbEstado.SelectedValue = "P"
            Me.btnBuscar.Enabled = True
            Me.cmbArea.Enabled = True
            Me.txtMotivo.ReadOnly = False
            Me.txtFechaAdeudo.ReadOnly = False
            Me.txtMonto.ReadOnly = False
            Me.txtFechaDevuelto.ReadOnly = True
            Me.txtComentario.ReadOnly = True

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

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

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Call mt_LimpiarControles("FiltrosAlumno")
            Call mt_UpdatePanel("FiltrosAlumno")

            Me.grwListaAlumno.DataSource = Nothing : Me.grwListaAlumno.DataBind()
            Call mt_UpdatePanel("ListaAlumno")

            Call mt_FlujoModal("BuscarAlumno", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListarAlumno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarAlumno.Click
        Try
            Call mt_CargarDatosAlumno()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub grwListaAlumno_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaAlumno.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Seleccionar"
                    Me.txtCodigoAlu.Value = Me.grwListaAlumno.DataKeys(index).Values("codigo_alu")
                    Me.txtCodigoUniversitario.Text = Me.grwListaAlumno.DataKeys(index).Values("codigoUniver_Alu")
                    Me.txtAlumno.Text = Me.grwListaAlumno.DataKeys(index).Values("nombre_completo")

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoModal("BuscarAlumno", "close")
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarAdeudo(CInt(Session("frmRegistroAdeudos-codigo_ade"))) Then
                Call btnListar_Click(Nothing, Nothing)
                Call mt_FlujoTabs("Listado")
                Exit Sub
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmRegistroAdeudos-codigo_ade") = Me.grwLista.DataKeys(index).Values("codigo_ade").ToString

            Select Case e.CommandName
                Case "Editar"
                    Session("frmRegistroAdeudos-operacion") = "Editar"

                    If Not mt_RealizarOperacion(Session("frmRegistroAdeudos-operacion"), CInt(Session("frmRegistroAdeudos-codigo_ade"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "GenerarDeuda"
                    Session("frmRegistroAdeudos-operacion") = "GenerarDeuda"
                    Call mt_RealizarOperacion(Session("frmRegistroAdeudos-operacion"), CInt(Session("frmRegistroAdeudos-codigo_ade")))

                Case "Devolver"
                    Session("frmRegistroAdeudos-operacion") = "Devolver"

                    If Not mt_RealizarOperacion(Session("frmRegistroAdeudos-operacion"), CInt(Session("frmRegistroAdeudos-codigo_ade"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "Eliminar"
                    Session("frmRegistroAdeudos-operacion") = "Eliminar"
                    Call mt_RealizarOperacion(Session("frmRegistroAdeudos-operacion"), CInt(Session("frmRegistroAdeudos-codigo_ade")))

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

                Case "FiltrosAlumno"
                    Me.udpFiltrosAlumno.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosAlumnoUpdate", "udpFiltrosAlumnoUpdate();", True)

                Case "ListaAlumno"
                    Me.udpListaAlumno.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaAlumnoUpdate", "udpListaAlumnoUpdate();", True)

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

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.txtCodigoAlu.Value = String.Empty
                    Me.txtCodigoUniversitario.Text = String.Empty
                    Me.txtAlumno.Text = String.Empty

                    Me.cmbArea.SelectedValue = "0"
                    Me.txtMotivo.Text = String.Empty
                    Me.txtFechaAdeudo.Text = String.Empty
                    Me.txtMonto.Text = String.Empty
                    Me.cmbEstado.SelectedValue = String.Empty                    
                    Me.chkGeneroDeuda.Checked = False
                    Me.chkGeneroDeuda.Visible = False
                    Me.lblGeneroDeuda.Visible = False

                    Me.txtFechaDevuelto.Text = String.Empty
                    Me.txtComentario.Text = String.Empty

                Case "FiltrosAlumno"
                    Me.cmbNivelFiltro.SelectedValue = String.Empty
                    Me.cmbFacultadFiltro.SelectedValue = String.Empty
                    Me.cmbCarreraFiltro.SelectedValue = String.Empty
                    Me.txtNombreFiltro.Text = String.Empty

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmRegistroAdeudos-codigo_ade") = Nothing
            Session("frmRegistroAdeudos-operacion") = Nothing
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

    Private Sub mt_CargarComboArea()
        Try
            Dim dt As New Data.DataTable : Dim le_ConfiguracionInstanciasAdeudos As New e_ConfiguracionInstanciasAdeudos

            With le_ConfiguracionInstanciasAdeudos
                .operacion = "CCO"
                .codigo_tfu = codigo_tfu
            End With
            dt = md_ConfiguracionInstanciasAdeudos.ListarConfiguracionInstanciasAdeudos(le_ConfiguracionInstanciasAdeudos)

            Call md_Funciones.CargarCombo(Me.cmbAreaFiltro, dt, "codigoArea_cco", "descripcionArea_cco", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbArea, dt, "codigoArea_cco", "descripcionArea_cco", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboEstado()
        Try
            Dim dt As New Data.DataTable : md_Funciones = New d_Funciones
            dt = md_Funciones.ObtenerDataTable("ESTADO_ADEUDOS")

            Call md_Funciones.CargarCombo(Me.cmbEstado, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbEstadoFiltro, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboNivel()
        Try
            me_Categoria = New e_Categoria
            Dim dt As New Data.DataTable

            me_Categoria.operacion = "GEN"
            me_Categoria.grupo_cat = "NIVEL_ESTUDIO"
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbNivelFiltro, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboFacultad()
        Try
            me_Facultad = New e_Facultad
            Dim dt As New Data.DataTable

            me_Facultad.operacion = "GEN"
            dt = md_Facultad.ListarFacultad(me_Facultad)

            Call md_Funciones.CargarCombo(Me.cmbFacultadFiltro, dt, "codigo_Fac", "nombre_Fac", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboCarrera()
        Try
            me_CarreraProfesional = New e_CarreraProfesional
            Dim dt As New Data.DataTable

            me_CarreraProfesional.operacion = "GEN"
            me_CarreraProfesional.codigo_Fac = cmbFacultadFiltro.SelectedValue
            me_CarreraProfesional.vigencia_Cpf = 1
            me_CarreraProfesional.eliminado_cpf = 0

            If Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPreGrado Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPreGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostGrado Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPostGrado
            ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostTitulo Then
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPostTitulo
            Else
                me_CarreraProfesional.codigo_test = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                                    g_VariablesGlobales.CodigoTestPostTitulo
            End If

            dt = md_CarreraProfesional.ListarCarreraProfesional(me_CarreraProfesional)

            Call md_Funciones.CargarCombo(Me.cmbCarreraFiltro, dt, "codigo_Cpf", "nombre_Cpf", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            If Not fu_ValidarCargarDatos() Then Exit Sub

            Dim dt As New DataTable : me_Adeudos = New e_Adeudos

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_Adeudos
                .operacion = "LIS"
                .codigoArea_cco = Me.cmbAreaFiltro.SelectedValue
                .estado_ade = cmbEstadoFiltro.SelectedValue
            End With

            dt = md_Adeudos.ListarAdeudos(me_Adeudos)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDatos() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbAreaFiltro.SelectedValue) Then mt_ShowMessage("Debe seleccionar un área.", MessageType.warning) : Me.cmbAreaFiltro.Focus() : Return False

            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Sub mt_CargarDatosAlumno()
        Try
            Dim dt As New DataTable : me_Alumno = New e_Alumno

            If Me.grwListaAlumno.Rows.Count > 0 Then Me.grwListaAlumno.DataSource = Nothing : Me.grwListaAlumno.DataBind()

            With me_Alumno
                .operacion = "BUS"

                If Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPreGrado Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado
                ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostGrado Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPostGrado
                ElseIf Me.cmbNivelFiltro.SelectedValue.ToString = g_VariablesGlobales.NivelEstudioPostTitulo Then
                    .nivel_ega = g_VariablesGlobales.CodigoTestPostTitulo
                Else
                    .nivel_ega = g_VariablesGlobales.CodigoTestPreGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostGrado & "," & _
                                g_VariablesGlobales.CodigoTestPostTitulo
                End If

                .codigo_Fac = Me.cmbFacultadFiltro.SelectedValue
                .tempcodigo_cpf = Me.cmbCarreraFiltro.SelectedValue
                .alumno = Me.txtNombreFiltro.Text.Trim
                .tiene_diploma = "S"
            End With

            dt = md_Alumno.ListarAlumno(me_Alumno)

            Me.grwListaAlumno.DataSource = dt
            Me.grwListaAlumno.DataBind()

            Call md_Funciones.AgregarHearders(grwListaAlumno)

            Call mt_UpdatePanel("ListaAlumno")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarAdeudo(ByVal codigo_ade As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarAdeudo() Then Return False

            me_Adeudos = md_Adeudos.GetAdeudos(codigo_ade)

            If Session("frmRegistroAdeudos-operacion") = "Nuevo" OrElse Session("frmRegistroAdeudos-operacion") = "Editar" Then
                With me_Adeudos
                    .operacion = "I"
                    .cod_user = cod_user
                    .codigo_alu = Me.txtCodigoAlu.Value
                    .codigoArea_cco = Me.cmbArea.SelectedValue
                    .codigo_tfu = codigo_tfu
                    .codigo_sco = 0
                    .codigo_cco = 0
                    .motivo_ade = Me.txtMotivo.Text.Trim.ToUpper
                    .fechaDeuda_ade = IIf(String.IsNullOrEmpty(Me.txtFechaAdeudo.Text.Trim), "01/01/1901", Me.txtFechaAdeudo.Text.Trim)
                    .monto_ade = Me.txtMonto.Text.Trim
                    .fechaCancelado_ade = IIf(String.IsNullOrEmpty(Me.txtFechaDevuelto.Text.Trim), "01/01/1901", Me.txtFechaDevuelto.Text.Trim)
                    .comentario_ade = Me.txtComentario.Text.Trim.ToUpper
                    .estado_ade = Me.cmbEstado.SelectedValue
                End With

                md_Adeudos.RegistrarAdeudos(me_Adeudos)

                Call mt_ShowMessage("¡El adeudo se registró exitosamente!", MessageType.success)

            ElseIf Session("frmRegistroAdeudos-operacion") = "Devolver" Then
                With me_Adeudos
                    .operacion = "U"
                    .cod_user = cod_user
                    .fechaCancelado_ade = IIf(String.IsNullOrEmpty(Me.txtFechaDevuelto.Text.Trim), "01/01/1901", Me.txtFechaDevuelto.Text.Trim)
                    .comentario_ade = Me.txtComentario.Text.Trim.ToUpper
                    .estado_ade = "D"
                End With

                md_Adeudos.RegistrarAdeudos(me_Adeudos)

                Call mt_ShowMessage("¡El adeudo se devolvió exitosamente!", MessageType.success)
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarAdeudo() As Boolean
        Try
            Dim le_Adeudos As e_Adeudos

            If Session("frmRegistroAdeudos-codigo_ade") Is Nothing OrElse String.IsNullOrEmpty(Session("frmRegistroAdeudos-codigo_ade")) Then mt_ShowMessage("El código de adeudo no ha sido encontrado.", MessageType.warning) : Return False

            If String.IsNullOrEmpty(Me.txtCodigoAlu.Value) OrElse Me.txtCodigoAlu.Value = 0 _
                OrElse String.IsNullOrEmpty(Me.txtCodigoUniversitario.Text.Trim) _
                OrElse String.IsNullOrEmpty(Me.txtAlumno.Text.Trim) Then
                mt_ShowMessage("Debe seleccionar un estudiante.", MessageType.warning) : Me.btnBuscar.Focus() : Return False
            End If

            If String.IsNullOrEmpty(Me.cmbArea.SelectedValue) OrElse Me.cmbArea.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar un área.", MessageType.warning) : Me.cmbArea.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtMotivo.Text.Trim) Then mt_ShowMessage("Debe ingresar un motivo.", MessageType.warning) : Me.txtMotivo.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtFechaAdeudo.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha de adeudo.", MessageType.warning) : Me.txtFechaAdeudo.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtMonto.Text.Trim) Then mt_ShowMessage("Debe ingresar un monto referencial.", MessageType.warning) : Me.txtMonto.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbEstado.SelectedValue) Then mt_ShowMessage("Debe seleccionar un estado.", MessageType.warning) : Me.cmbEstado.Focus() : Return False

            If Session("frmRegistroAdeudos-operacion") = "Editar" Then                
                le_Adeudos = md_Adeudos.GetAdeudos(CInt(Session("frmRegistroAdeudos-codigo_ade")))
                If le_Adeudos.estado_ade <> "P" Then mt_ShowMessage("Solo puede modificar adeudos en estado PENDIENTE.", MessageType.warning) : Return False
                If le_Adeudos.codigo_deu <> 0 Then mt_ShowMessage("No puede modificar el adeudo debido a que presenta una deuda generada.", MessageType.warning) : Return False

            ElseIf Session("frmRegistroAdeudos-operacion") = "Devolver" Then                                
                le_Adeudos = md_Adeudos.GetAdeudos(CInt(Session("frmRegistroAdeudos-codigo_ade")))
                If le_Adeudos.estado_ade <> "P" Then mt_ShowMessage("Solo puede devolver adeudos en estado PENDIENTE.", MessageType.warning) : Return False
                If le_Adeudos.codigo_deu <> 0 Then mt_ShowMessage("No puede devolver el adeudo debidó a que presenta una deuda generada.", MessageType.warning) : Return False

                If String.IsNullOrEmpty(Me.txtFechaDevuelto.Text.Trim) Then mt_ShowMessage("Debe ingresar una fecha de devolución.", MessageType.warning) : Me.txtFechaDevuelto.Focus() : Return False
                If String.IsNullOrEmpty(Me.txtComentario.Text.Trim) Then mt_ShowMessage("Debe ingresar un comentario.", MessageType.warning) : Me.txtComentario.Focus() : Return False

            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RealizarOperacion(ByVal operacion As String, ByVal codigo_ade As Integer) As Boolean
        Try
            me_Adeudos = md_Adeudos.GetAdeudos(codigo_ade)

            If me_Adeudos.codigo_ade = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False
            Call mt_LimpiarControles("Registro")

            Select Case operacion
                Case "Editar"
                    With me_Adeudos
                        Me.txtCodigoAlu.Value = .codigo_alu
                        Me.txtCodigoUniversitario.Text = .codigoUniver_alu
                        Me.txtAlumno.Text = .nombre_alu
                        Me.btnBuscar.Enabled = True
                        Me.cmbArea.SelectedValue = .codigoArea_cco
                        Me.cmbArea.Enabled = True
                        Me.txtMotivo.Text = .motivo_ade
                        Me.txtMotivo.ReadOnly = False
                        Me.txtFechaAdeudo.Text = .fechaDeuda_ade
                        Me.txtFechaAdeudo.ReadOnly = False
                        Me.txtMonto.Text = .monto_ade
                        Me.txtMonto.ReadOnly = False
                        Me.cmbEstado.SelectedValue = .estado_ade
                        Me.chkGeneroDeuda.Checked = IIf(.codigo_deu = 0, False, True)
                        Me.chkGeneroDeuda.Visible = True
                        Me.lblGeneroDeuda.Visible = True
                        Me.txtFechaDevuelto.Text = .fechaCancelado_ade
                        Me.txtFechaDevuelto.ReadOnly = True
                        Me.txtComentario.Text = .comentario_ade
                        Me.txtComentario.ReadOnly = True
                    End With

                Case "Devolver"
                    If me_Adeudos.estado_ade <> "P" Then mt_ShowMessage("El adeudo debe encontrarse en estado PENDIENTE.", MessageType.warning) : Return False
                    If me_Adeudos.codigo_deu <> 0 Then mt_ShowMessage("El adeudo presenta una deuda generada.", MessageType.warning) : Return False

                    With me_Adeudos
                        Me.txtCodigoAlu.Value = .codigo_alu
                        Me.txtCodigoUniversitario.Text = .codigoUniver_alu
                        Me.txtAlumno.Text = .nombre_alu
                        Me.btnBuscar.Enabled = False
                        Me.cmbArea.SelectedValue = .codigoArea_cco
                        Me.cmbArea.Enabled = False
                        Me.txtMotivo.Text = .motivo_ade
                        Me.txtMotivo.ReadOnly = True
                        Me.txtFechaAdeudo.Text = .fechaDeuda_ade
                        Me.txtFechaAdeudo.ReadOnly = True
                        Me.txtMonto.Text = .monto_ade
                        Me.txtMonto.ReadOnly = True
                        Me.cmbEstado.SelectedValue = .estado_ade
                        Me.chkGeneroDeuda.Checked = IIf(.codigo_deu = 0, False, True)
                        Me.chkGeneroDeuda.Visible = True
                        Me.lblGeneroDeuda.Visible = True
                        Me.txtFechaDevuelto.Text = String.Empty
                        Me.txtFechaDevuelto.ReadOnly = False
                        Me.txtComentario.Text = String.Empty
                        Me.txtComentario.ReadOnly = False
                    End With

                Case "GenerarDeuda"
                    If me_Adeudos.estado_ade <> "P" Then mt_ShowMessage("El adeudo debe encontrarse en estado PENDIENTE.", MessageType.warning) : Return False
                    If me_Adeudos.codigo_deu <> 0 Then mt_ShowMessage("El adeudo presenta una deuda generada.", MessageType.warning) : Return False
                    If me_Adeudos.monto_ade <= 0 Then mt_ShowMessage("Debe ingresar un monto de adeudo.", MessageType.warning) : Return False

                    'Obtener datos de servicio concepto y centro de costo
                    Dim dt As New DataTable
                    Dim le_ConfiguracionInstanciasAdeudos As New e_ConfiguracionInstanciasAdeudos

                    With le_ConfiguracionInstanciasAdeudos
                        .operacion = "GEN"
                        .codigoArea_cco = me_Adeudos.codigoArea_cco
                        .codigo_tfu = codigo_tfu
                        .estado_cia = "A"
                    End With

                    dt = md_ConfiguracionInstanciasAdeudos.ListarConfiguracionInstanciasAdeudos(le_ConfiguracionInstanciasAdeudos)
                    If dt.Rows.Count = 0 Then mt_ShowMessage("Debe presentar una configuración de adeudo activa para su cargo.", MessageType.warning) : Return False
                    If CInt(dt.Rows(0).Item("codigo_sco").ToString) = 0 OrElse CInt(dt.Rows(0).Item("codigo_cco").ToString) = 0 Then mt_ShowMessage("Debe seleccionar un concepto y un centro de costo en la configuración de adeudo de su cargo.", MessageType.warning) : Return False

                    With me_Adeudos
                        .operacion = "G"
                        .cod_user = cod_user
                        .codigo_tfu = codigo_tfu
                        .codigo_sco = CInt(dt.Rows(0).Item("codigo_sco").ToString)
                        .codigo_cco = CInt(dt.Rows(0).Item("codigo_cco").ToString)
                    End With                    

                    md_Adeudos.RegistrarAdeudos(me_Adeudos)

                    Call mt_ShowMessage("¡La deuda se generó exitosamente!", MessageType.success)
                    Call mt_CargarDatos()

                Case "Eliminar"
                    If me_Adeudos.estado_ade <> "P" Then mt_ShowMessage("El adeudo debe encontrarse en estado PENDIENTE.", MessageType.warning) : Return False
                    If me_Adeudos.codigo_deu <> 0 Then mt_ShowMessage("El adeudo presenta una deuda generada.", MessageType.warning) : Return False

                    me_Adeudos.operacion = "D"
                    me_Adeudos.cod_user = cod_user
                    md_Adeudos.RegistrarAdeudos(me_Adeudos)

                    Call mt_ShowMessage("¡El adeudo se eliminó exitosamente!", MessageType.success)
                    Call mt_CargarDatos()

                Case Else
                    Return False

            End Select

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
