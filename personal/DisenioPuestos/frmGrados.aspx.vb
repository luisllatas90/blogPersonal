Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class DisenioPuestos_frmGrados
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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmGrados-codigo_gra") = 0

            Call mt_LimpiarControles("Registro")

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmGrados-codigo_gra") = Me.grwLista.DataKeys(index).Values("codigo_gra").ToString

            Select Case e.CommandName
                Case "Editar"
                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    'If Not mt_CargarFormularioRegistro(CInt(Session("frmGrados-codigo_gra"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "Eliminar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_EliminarNotificacion(CInt(Session("frmGrados-codigo_gra"))) Then Exit Sub

                    Call btnListar_Click(Nothing, Nothing)

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

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarNotificacion(CInt(Session("frmGrados-codigo_gra"))) Then
                Call btnListar_Click(Nothing, Nothing)
                Call mt_FlujoTabs("Listado")
                Exit Sub
            End If
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
            'Session("frmGrados-codigo_gra") = Nothing
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

            dt.Columns.Add("codigo_gra")
            dt.Columns.Add("grado")
            dt.Columns.Add("nombre")
            dt.Columns.Add("descripcion")

            Dim fila As Data.DataRow

            fila = dt.NewRow()
            fila("codigo_gra") = 1
            fila("grado") = "A"
            fila("nombre") = "Entendimiento"
            fila("descripcion") = "Conoce y puede describir los elementos más importantes de la competencia."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_gra") = 2
            fila("grado") = "B"
            fila("nombre") = "Conocimiento"
            fila("descripcion") = "Es capaz de interpretar, debatir y evaluar información en el área de competencia."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_gra") = 3
            fila("grado") = "C"
            fila("nombre") = "Habilidad"
            fila("descripcion") = "Es capaz de llevar a cabo actividades de manera consistente; resuelve problemas operativos comunes, guía y asesora a otros en aspectos técnicos/ operativos."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_gra") = 4
            fila("grado") = "D"
            fila("nombre") = "Dominio"
            fila("descripcion") = "Es capaz de diagnosticar y resolver problemas inusuales significativos y adaptar aspectos de la competencia de manera exitosa."
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_gra") = 5
            fila("grado") = "E"
            fila("nombre") = "Desarrollo/Innova"
            fila("descripcion") = "Es capaz de desarrollar, reestructurar y dirigir nuevos y significativos métodos y acciones estratégicas para una competencia."
            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_gra As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_gra)

            'If me_Notificacion.codigo_gra = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

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
            'If Session("frmGrados-codigo_gra") Is Nothing OrElse String.IsNullOrEmpty(Session("frmGrados-codigo_gra")) Then mt_ShowMessage("El código de notificación no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarNotificacion(ByVal codigo_gra As Integer) As Boolean
        Try
            'If Not fu_ValidarRegistrarNotificacion() Then Return False

            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_gra)

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

            'If dt.Rows.Count > 0 AndAlso CInt(Session("frmGrados-codigo_gra")) = 0 Then mt_ShowMessage("Existe un registro con este tipo, clasificación, abreviatura y versión.", MessageType.warning) : Return False

            'For Each fila As DataRow In dt.Rows
            '    If CInt(Session("frmGrados-codigo_gra")) <> CInt(fila("codigo_gra")) Then mt_ShowMessage("Existe un registro con este tipo, clasificación, abreviatura y versión.", MessageType.warning) : Return False
            'Next

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_EliminarNotificacion(ByVal codigo_gra As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_gra)

            'With me_Notificacion
            '    .operacion = "D"
            '    .codigo_gra = codigo_gra
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

#End Region

End Class
