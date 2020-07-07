﻿Partial Class Alumni_frmCategoria
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    'DATOS
    Dim md_Funciones As d_Funciones
    Dim md_Categoria As d_Categoria

    'ENTIDADES
    Dim me_Categoria As e_Categoria

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
                Call mt_CargarComboGrupo()

                Call mt_LimpiarControles()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnRegistrarCategoria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegistrarCategoria.Click
        Try
            If String.IsNullOrEmpty(Me.cmbGrupoFiltro.SelectedValue.Trim) Then
                Me.cmbGrupoFiltro.Focus()
                Call mt_ShowMessage("Debe seleccionar un grupo", MessageType.warning)
            Else
                Session("codigo_cat") = 0
                Call mt_LimpiarControles()
                Me.txtGrupo.Text = cmbGrupoFiltro.SelectedValue.Trim

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & "Nuevo" & "');", True)
                Me.txtNombre.Focus()
                Me.udpContenidoModal.Update()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Call mt_RegistrarCategoria()
        Catch ex As Exception            
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("codigo_cat") = Me.grwLista.DataKeys(index).Values("codigo_cat")

            Select Case e.CommandName
                Case "Editar"
                    mt_LimpiarControles()

                    Me.txtGrupo.Text = Me.grwLista.DataKeys(index).Values("grupo_cat")
                    Me.txtNombre.Text = Me.grwLista.DataKeys(index).Values("nombre_cat")

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & "Editar" & "');", True)
                    Me.udpContenidoModal.Update()

                Case "Eliminar"
                    Call mt_EliminarCategoria()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try            
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)            
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try                        
            Me.txtGrupo.Text = String.Empty
            Me.txtNombre.Text = String.Empty
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboGrupo()
        Try            
            md_Funciones = New d_Funciones : md_Categoria = New d_Categoria : me_Categoria = New e_Categoria
            Dim dt As New Data.DataTable

            me_Categoria.operacion = "GEN"
            dt = md_Categoria.ListarGrupo(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbGrupoFiltro, dt, "grupo_cat", "grupo_cat", True, "[-- Seleccione un Grupo --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            md_Funciones = New d_Funciones : md_Categoria = New d_Categoria : me_Categoria = New e_Categoria
            Dim dt As New Data.DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            me_Categoria.operacion = "GEN"
            me_Categoria.grupo_cat = Me.cmbGrupoFiltro.SelectedValue.Trim
            me_Categoria.nombre_cat = Me.txtNombreFiltro.Text.Trim
            dt = md_Categoria.ListarCategoria(me_Categoria)
            Me.grwLista.DataSource = dt


            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

            Me.udpLista.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_RegistrarCategoria()
        Try
            me_Categoria = New e_Categoria : md_Categoria = New d_Categoria

            me_Categoria.operacion = "I"
            me_Categoria.cod_user = cod_user
            me_Categoria.codigo_cat = Session("codigo_cat")
            me_Categoria.nombre_cat = Me.txtNombre.Text.Trim
            me_Categoria.grupo_cat = Me.txtGrupo.Text.Trim

            md_Categoria.RegistrarCategoria(me_Categoria)

            Call mt_ShowMessage("¡Los datos han sido registrados correctamente!", MessageType.success)

            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_EliminarCategoria()
        Try
            me_Categoria = New e_Categoria : md_Categoria = New d_Categoria

            me_Categoria.operacion = "D"
            me_Categoria.cod_user = cod_user
            me_Categoria.codigo_cat = Session("codigo_cat")

            md_Categoria.RegistrarCategoria(me_Categoria)

            Call mt_ShowMessage("¡Se ha eliminado correctamente!", MessageType.success)
            mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
