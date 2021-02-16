Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class DisenioPuestos_frmConformidadFicha
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES

    'DATOS
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
                Call mt_LimpiarVariablesSession()
                Call mt_CargarComboClasificacion()
                Call mt_CargarComboProfile()
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

    'Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
    '    Try
    '        Session("frmConformidadFicha-codigo_pue") = 0

    '        Call mt_LimpiarControles("Registro")

    '        Call mt_UpdatePanel("Registro")

    '        Call mt_FlujoTabs("Registro")

    '    Catch ex As Exception
    '        Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
    '    End Try
    'End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmConformidadFicha-codigo_pue") = Me.grwLista.DataKeys(index).Values("codigo_pue").ToString

            Select Case e.CommandName
                Case "GestionarFicha"
                    Call mt_CargarDatosTarea(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosT1(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosT2(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosT3(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosT4(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosCompetencia(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_CargarDatosRecomendacion(Session("frmConformidadFicha-codigo_pue"))
                    Call mt_FlujoTabs("Ficha")

                    'Case "Ver"
                    '    Call mt_FlujoModal("BuscarAlumno", "open")

                Case "Editar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_CargarFormularioRegistro(CInt(Session("frmConformidadFicha-codigo_pue"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "Eliminar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_EliminarNotificacion(CInt(Session("frmConformidadFicha-codigo_pue"))) Then Exit Sub

                    Call btnListar_Click(Nothing, Nothing)

                    Call mt_FlujoModal("Observacion", "open")

                Case "Observar"
                    Call mt_FlujoModal("Mensaje", "open")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    'Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
    '    Try
    '        Call btnListar_Click(Nothing, Nothing)

    '        Call mt_FlujoTabs("Listado")

    '    Catch ex As Exception
    '        Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
    '    End Try
    'End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarNotificacion(CInt(Session("frmConformidadFicha-codigo_pue"))) Then
                Call btnListar_Click(Nothing, Nothing)
                Call mt_FlujoTabs("Listado")
                Exit Sub
            End If
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

    Protected Sub btnSalirFicha_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirFicha.Click
        Try
            Call mt_FlujoTabs("Listado")
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

                    'Case "Registro"
                    '    Me.udpRegistro.Update()
                    '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "Tarea"
                    Me.udpTarea.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpTareaUpdate", "udpTareaUpdate();", True)

                Case "T1"
                    Me.udpT1.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpT1Update", "udpT1Update();", True)
                Case "T2"
                    Me.udpT2.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpT2Update", "udpT2Update();", True)
                Case "T3"
                    Me.udpT3.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpT3Update", "udpT3Update();", True)
                Case "T4"
                    Me.udpT4.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpT4Update", "udpT4Update();", True)

                Case "Competencia"
                    Me.udpCompetencia.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpCompetenciaUpdate", "udpCompetenciaUpdate();", True)

                Case "Recomendacion"
                    Me.udpRecomendacion.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRecomendacionUpdate", "udpRecomendacionUpdate();", True)

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

                Case "Ficha"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('ficha-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboClasificacion()
        Try
            'Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            'With me_Categoria
            '    .operacion = "GEN"
            '    .grupo_cat = "CLASIFICACION_NOTIFICACION"
            'End With
            'dt = md_Categoria.ListarCategoria(me_Categoria)

            'Call md_Funciones.CargarCombo(Me.cmbClasificacion, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            'Call md_Funciones.CargarCombo(Me.cmbClasificacionFiltro, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            'dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboProfile()
        Try
            'Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            'With me_Categoria
            '    .operacion = "GEN"
            '    .grupo_cat = "PROFILE_NAME"
            'End With
            'dt = md_Categoria.ListarCategoria(me_Categoria)

            'Call md_Funciones.CargarCombo(Me.cmbProfile, dt, "abreviatura_cat", "nombre_cat", True, "[-- SELECCIONE --]", "")
            'dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    'Me.cmbTipo.SelectedValue = String.Empty
                    'Me.cmbClasificacion.SelectedValue = String.Empty
                    'Me.txtNombre.Text = String.Empty
                    'Me.txtAbreviatura.Text = String.Empty
                    'Me.txtVersion.Text = String.Empty
                    'Me.txtAsunto.Text = String.Empty
                    'Me.txtPlantilla.Value = String.Empty
                    'Me.cmbProfile.SelectedValue = String.Empty
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            'Session("frmConformidadFicha-codigo_pue") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            'Dim dt As New DataTable : me_Notificacion = New e_Notificaciones

            'If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            'With me_Notificacion
            '    .operacion = "LIS"
            '    .tipo_not = cmbTipoFiltro.SelectedValue
            '    .clasificacion_not = cmbClasificacionFiltro.SelectedValue
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            'Me.grwLista.DataSource = dt
            'Me.grwLista.DataBind()

            'Call md_Funciones.AgregarHearders(grwLista)

            'Call mt_UpdatePanel("Lista")

            Dim dt As New DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            dt.Columns.Add("codigo_pue")
            dt.Columns.Add("puesto")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_pue") = 1
            fila("puesto") = "Analista Programador"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_pue") = 2
            fila("puesto") = "Analista de Sistemas"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_pue") = 3
            fila("puesto") = "Calidad"
            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_pue As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_pue)

            'If me_Notificacion.codigo_pue = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            'Call mt_LimpiarControles("Registro")

            'With me_Notificacion
            '    Me.txtNombre.Text = .nombre_not
            '    Me.cmbTipo.SelectedValue = .tipo_not
            '    Me.cmbClasificacion.SelectedValue = .clasificacion_not
            '    Me.txtAbreviatura.Text = .abreviatura_not
            '    Me.txtVersion.Text = .version_not
            '    Me.txtAsunto.Text = .asunto_not
            '    Me.cmbProfile.SelectedValue = .profile_name
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "setCodigoPlantilla", "setCodigoPlantilla('" & fu_EliminarSaltosLinea(.cuerpo_not, "") & "');", True)
            'End With

            'Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            'If Session("frmConformidadFicha-codigo_pue") Is Nothing OrElse String.IsNullOrEmpty(Session("frmConformidadFicha-codigo_pue")) Then mt_ShowMessage("El código de notificación no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarNotificacion(ByVal codigo_pue As Integer) As Boolean
        Try
            'If Not fu_ValidarRegistrarNotificacion() Then Return False

            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_pue)

            'With me_Notificacion
            '    .operacion = "I"
            '    .cod_user = cod_user
            '    .tipo_not = cmbTipo.SelectedValue
            '    .clasificacion_not = cmbClasificacion.SelectedValue
            '    .nombre_not = txtNombre.Text.Trim
            '    .abreviatura_not = txtAbreviatura.Text.Trim
            '    .version_not = txtVersion.Text.Trim
            '    .asunto_not = txtAsunto.Text.Trim
            '    .profile_name = cmbProfile.SelectedValue
            '    .cuerpo_not = HttpUtility.UrlDecode(Me.txtPlantilla.Value)
            'End With

            'md_Notificacion.RegistrarNotificacion(me_Notificacion)

            'Call mt_ShowMessage("¡La notificación se registro exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarNotificacion() As Boolean
        Try
            'Dim ls_Plantilla As String = HttpUtility.UrlDecode(Me.txtPlantilla.Value)

            'If String.IsNullOrEmpty(Me.cmbTipo.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un tipo.", MessageType.warning) : Me.cmbTipo.Focus() : Return False
            'If String.IsNullOrEmpty(Me.cmbClasificacion.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar una clasificación.", MessageType.warning) : Me.cmbClasificacion.Focus() : Return False
            'If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre.", MessageType.warning) : Me.txtNombre.Focus() : Return False
            'If String.IsNullOrEmpty(Me.txtAbreviatura.Text.Trim) Then mt_ShowMessage("Debe ingresar una abreviatura.", MessageType.warning) : Me.txtAbreviatura.Focus() : Return False
            'If String.IsNullOrEmpty(Me.txtVersion.Text.Trim) Then mt_ShowMessage("Debe ingresar una versión.", MessageType.warning) : Me.txtVersion.Focus() : Return False
            'If String.IsNullOrEmpty(Me.cmbProfile.SelectedValue.Trim) Then mt_ShowMessage("Debe seleccionar un profile de envío.", MessageType.warning) : Me.cmbProfile.Focus() : Return False
            'If String.IsNullOrEmpty(Me.txtAsunto.Text.Trim) Then mt_ShowMessage("Debe ingresar un asunto.", MessageType.warning) : Me.txtAsunto.Focus() : Return False
            'If String.IsNullOrEmpty(ls_Plantilla.Trim) OrElse ls_Plantilla.Trim = "<p>&nbsp;</p>" OrElse ls_Plantilla.Trim = "<p><br></p>" Then mt_ShowMessage("Debe ingresar un cuerpo.", MessageType.warning) : Return False

            ''Validar duplicados
            'me_Notificacion = New e_Notificaciones
            'Dim dt As New DataTable

            'With me_Notificacion
            '    .operacion = "GEN"
            '    .tipo_not = cmbTipo.SelectedValue
            '    .clasificacion_not = cmbClasificacion.SelectedValue
            '    .abreviatura_not = txtAbreviatura.Text.Trim
            '    .version_not = txtVersion.Text.Trim
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            'If dt.Rows.Count > 0 AndAlso CInt(Session("frmConformidadFicha-codigo_pue")) = 0 Then mt_ShowMessage("Existe un registro con este tipo, clasificación, abreviatura y versión.", MessageType.warning) : Return False

            'For Each fila As DataRow In dt.Rows
            '    If CInt(Session("frmConformidadFicha-codigo_pue")) <> CInt(fila("codigo_pue")) Then mt_ShowMessage("Existe un registro con este tipo, clasificación, abreviatura y versión.", MessageType.warning) : Return False
            'Next

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarNotificacion(ByVal codigo_pue As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_pue)

            'With me_Notificacion
            '    .operacion = "D"
            '    .codigo_pue = codigo_pue
            '    .cod_user = cod_user
            'End With

            'md_Notificacion.RegistrarNotificacion(me_Notificacion)

            'mt_ShowMessage("¡La notificación se elimino exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_EliminarSaltosLinea(ByVal texto As String, ByVal caracter_reemplazar As String) As String
        Try
            Return Replace(Replace(texto, Chr(10), caracter_reemplazar), Chr(13), caracter_reemplazar)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub mt_CargarDatosTarea(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwTarea.Rows.Count > 0 Then Me.grwTarea.DataSource = Nothing : Me.grwTarea.DataBind()

            dt.Columns.Add("codigo_tar")
            dt.Columns.Add("que")
            dt.Columns.Add("como")
            dt.Columns.Add("min")
            dt.Columns.Add("tiempo")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_tar") = 1
            fila("que") = "abc"
            fila("como") = "xxxxxxxxxx"
            fila("min") = "4"
            fila("tiempo") = "50%"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_tar") = 2
            fila("que") = "xyz"
            fila("como") = "xxxxxxxxxx"
            fila("min") = "2"
            fila("tiempo") = "50%"
            dt.Rows.Add(fila)

            Me.grwTarea.DataSource = dt
            Me.grwTarea.DataBind()

            Call md_Funciones.AgregarHearders(grwTarea)

            Call mt_UpdatePanel("Tarea")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosCompetencia(ByVal codigo_emp As Integer)
        Try

            Dim dt As New DataTable

            If Me.grwCompetencia.Rows.Count > 0 Then Me.grwCompetencia.DataSource = Nothing : Me.grwCompetencia.DataBind()

            dt.Columns.Add("codigo_com")
            dt.Columns.Add("tipo_com")
            dt.Columns.Add("grado_com")
            dt.Columns.Add("nombre_com")
            dt.Columns.Add("descripcion_com")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_com") = 1
            fila("tipo_com") = "General"
            fila("grado_com") = "A"
            fila("nombre_com") = "Trabajo en equipo"
            fila("descripcion_com") = "Capacidad para orientarse hacia las metas comunes, valorando las opiniones de otros y compartiendo información e ideas."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_com") = 2
            fila("tipo_com") = "Específico"
            fila("grado_com") = "B"
            fila("nombre_com") = "Conocimiento Bizagi"
            fila("descripcion_com") = "Para modelado de flujo de procesos."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_com") = 3
            fila("tipo_com") = "General"
            fila("grado_com") = "C"
            fila("nombre_com") = "Calidad de servicio"
            fila("descripcion_com") = "Realiza su labor de manera correcta y de acuerdo a lo esperado, garantizando la satisfacción del cliente."
            dt.Rows.Add(fila)

            Me.grwCompetencia.DataSource = dt
            Me.grwCompetencia.DataBind()

            Call md_Funciones.AgregarHearders(grwCompetencia)

            Call mt_UpdatePanel("Competencia")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosRecomendacion(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwRecomendacion.Rows.Count > 0 Then Me.grwRecomendacion.DataSource = Nothing : Me.grwRecomendacion.DataBind()

            dt.Columns.Add("codigo_rec")
            dt.Columns.Add("clasificacion")
            dt.Columns.Add("descripcion")
            dt.Columns.Add("recomendacion")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_rec") = 1
            fila("clasificacion") = "Alto"
            fila("descripcion") = "xxxxxxxxxx"
            fila("recomendacion") = "yyyyyyyyyyyyyyy"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_rec") = 2
            fila("clasificacion") = "Medio"
            fila("descripcion") = "xxxxxxxxxx"
            fila("recomendacion") = "zzzzzzzzzzzzzzz"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_rec") = 3
            fila("clasificacion") = "Bajo"
            fila("descripcion") = "xxxxxxxxxx"
            fila("recomendacion") = "zzzzzzzzzzzzzzz"
            dt.Rows.Add(fila)

            Me.grwRecomendacion.DataSource = dt
            Me.grwRecomendacion.DataBind()

            Call md_Funciones.AgregarHearders(grwRecomendacion)

            Call mt_UpdatePanel("Recomendacion")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Private Sub mt_CargarDatosT1(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwT1.Rows.Count > 0 Then Me.grwT1.DataSource = Nothing : Me.grwT1.DataBind()

            dt.Columns.Add("codigo_t1")
            dt.Columns.Add("t1")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_t1") = 1
            fila("t1") = "abc"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_t1") = 2
            fila("t1") = "xyz"
            dt.Rows.Add(fila)

            Me.grwT1.DataSource = dt
            Me.grwT1.DataBind()

            Call md_Funciones.AgregarHearders(grwT1)

            Call mt_UpdatePanel("T1")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosT2(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwT2.Rows.Count > 0 Then Me.grwT2.DataSource = Nothing : Me.grwT2.DataBind()

            dt.Columns.Add("codigo_t2")
            dt.Columns.Add("t2")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_t2") = 1
            fila("t2") = "abc"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_t2") = 2
            fila("t2") = "xyz"
            dt.Rows.Add(fila)

            Me.grwT2.DataSource = dt
            Me.grwT2.DataBind()

            Call md_Funciones.AgregarHearders(grwT2)

            Call mt_UpdatePanel("T2")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosT3(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwT3.Rows.Count > 0 Then Me.grwT3.DataSource = Nothing : Me.grwT3.DataBind()

            dt.Columns.Add("codigo_t3")
            dt.Columns.Add("t3")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_t3") = 1
            fila("t3") = "abc"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_t3") = 2
            fila("t3") = "xyz"
            dt.Rows.Add(fila)

            Me.grwT3.DataSource = dt
            Me.grwT3.DataBind()

            Call md_Funciones.AgregarHearders(grwT3)

            Call mt_UpdatePanel("T3")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosT4(ByVal codigo_emp As Integer)
        Try
            Dim dt As New DataTable

            If Me.grwT4.Rows.Count > 0 Then Me.grwT4.DataSource = Nothing : Me.grwT4.DataBind()

            dt.Columns.Add("codigo_t4")
            dt.Columns.Add("t4")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_t4") = 1
            fila("t4") = "abc"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_t4") = 2
            fila("t4") = "xyz"
            dt.Rows.Add(fila)

            Me.grwT4.DataSource = dt
            Me.grwT4.DataBind()

            Call md_Funciones.AgregarHearders(grwT4)

            Call mt_UpdatePanel("T4")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
