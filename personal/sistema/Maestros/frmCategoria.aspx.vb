Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class sistema_Maestros_frmCategoria
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_Categoria As e_Categoria

    'DATOS
    Dim md_Categoria As New d_Categoria
    Dim md_Funciones As New d_Funciones

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
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If

            cod_user = Session("id_per")            

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()
                Call mt_CargarComboGrupo()                
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
            Session("frmCategoria-codigo_cat") = 0

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
            Session("frmCategoria-codigo_cat") = Me.grwLista.DataKeys(index).Values("codigo_cat").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_CargarFormularioRegistro(CInt(Session("frmCategoria-codigo_cat"))) Then Exit Sub

                    Me.txtAbreviatura.ReadOnly = True

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

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If mt_RegistrarCategoria(CInt(Session("frmCategoria-codigo_cat"))) Then
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

    Private Sub mt_CargarComboGrupo()
        Try
            Dim dt As New Data.DataTable : Dim le_Categoria As New e_Categoria

            With le_Categoria
                .operacion = "GEN"                
            End With
            dt = md_Categoria.ListarGrupo(le_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbGrupo, dt, "grupo_cat", "grupo_cat", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbGrupoFiltro, dt, "grupo_cat", "grupo_cat", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.cmbGrupo.SelectedValue = String.Empty                    
                    Me.txtGrupo.Text = String.Empty
                    Me.txtNombre.Text = String.Empty
                    Me.txtAbreviatura.Text = String.Empty
                    Me.txtAbreviatura.ReadOnly = False
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmCategoria-codigo_cat") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_Categoria = New e_Categoria

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = cmbGrupoFiltro.SelectedValue                
            End With

            dt = md_Categoria.ListarCategoria(me_Categoria)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_cat As Integer) As Boolean
        Try
            me_Categoria = md_Categoria.GetCategoria(codigo_cat)

            If me_Categoria.codigo_cat = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")

            With me_Categoria
                Me.cmbGrupo.SelectedValue = .grupo_cat
                Me.txtGrupo.Text = String.Empty
                Me.txtNombre.Text = .nombre_cat
                Me.txtAbreviatura.Text = .abreviatura_cat                
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try
            If Session("frmCategoria-codigo_cat") Is Nothing OrElse String.IsNullOrEmpty(Session("frmCategoria-codigo_cat")) Then mt_ShowMessage("El código de categoría no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarCategoria(ByVal codigo_cat As Integer) As Boolean
        Try
            If Not fu_ValidarRegistrarCategoria() Then Return False

            me_Categoria = md_Categoria.GetCategoria(codigo_cat)

            With me_Categoria
                .operacion = "I"
                .cod_user = cod_user
                .grupo_cat = IIf(String.IsNullOrEmpty(cmbGrupo.SelectedValue), txtGrupo.Text.Trim.ToUpper, cmbGrupo.SelectedValue)
                .nombre_cat = txtNombre.Text.Trim.ToUpper
                .abreviatura_cat = txtAbreviatura.Text.Trim.ToUpper
            End With

            md_Categoria.RegistrarCategoria(me_Categoria)

            Call mt_ShowMessage("¡La categoría se registró exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarCategoria() As Boolean
        Try            
            If String.IsNullOrEmpty(Me.cmbGrupo.SelectedValue.Trim) AndAlso String.IsNullOrEmpty(Me.txtGrupo.Text.Trim) Then mt_ShowMessage("Debe seleccionar un grupo.", MessageType.warning) : Me.cmbGrupo.Focus() : Return False            
            If String.IsNullOrEmpty(Me.txtNombre.Text.Trim) Then mt_ShowMessage("Debe ingresar un nombre.", MessageType.warning) : Me.txtNombre.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtAbreviatura.Text.Trim) Then mt_ShowMessage("Debe ingresar una abreviatura.", MessageType.warning) : Me.txtAbreviatura.Focus() : Return False            

            'Validar duplicados
            Dim le_Categoria As New e_Categoria
            Dim dt As New DataTable

            With le_Categoria
                .operacion = "GEN"                
                .grupo_cat = IIf(String.IsNullOrEmpty(cmbGrupo.SelectedValue), txtGrupo.Text.Trim.ToUpper, cmbGrupo.SelectedValue)
                .abreviatura_cat = txtAbreviatura.Text.Trim.ToUpper
            End With

            dt = md_Categoria.ListarCategoria(le_Categoria)

            If dt.Rows.Count > 0 AndAlso CInt(Session("frmCategoria-codigo_cat")) = 0 Then mt_ShowMessage("Existe un registro de categoría en este grupo con esta abreviatura.", MessageType.warning) : Return False

            For Each fila As DataRow In dt.Rows
                If CInt(Session("frmCategoria-codigo_cat")) <> CInt(fila("codigo_cat")) Then mt_ShowMessage("Existe un registro de categoría en este grupo con esta abreviatura.", MessageType.warning) : Return False
            Next

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
