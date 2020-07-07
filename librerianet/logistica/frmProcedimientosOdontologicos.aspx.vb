﻿
Partial Class logistica_frmProcedimientosOdontologicos
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            Else
                If (Request.QueryString("id") <> "") Then
                    cod_user = CInt(Request.QueryString("id"))
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
    End Sub

    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub gvProcedimiento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProcedimiento.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Me.cboEstado.Enabled = True
                Me.txtNombre.Text = Me.gvProcedimiento.DataKeys(index).Values("nombre_pro")
                Me.txtDescripcion.Text = Me.gvProcedimiento.DataKeys(index).Values("descripcion_pro")
                Me.cboTipoEstudio.SelectedValue = Me.gvProcedimiento.DataKeys(index).Values("codigo_test")
                Me.hdCodPro.Value = Me.gvProcedimiento.DataKeys(index).Values("codigo_pro")
                If Me.gvProcedimiento.DataKeys(index).Values("estado_pro").ToString = "ACTIVO" Then
                    Me.cboEstado.SelectedValue = 1
                Else
                    Me.cboEstado.SelectedValue = 0
                End If
            Else
                Dim obj As New ClsConectarDatos
                Dim cod As Integer
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                cod = CInt(Me.gvProcedimiento.DataKeys(index).Values("codigo_pro"))
                obj.AbrirConexion()
                obj.Ejecutar("ODO_eliminarProcedimiento", cod, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("se eliminó correctamente la información", MessageType.Success)
                mt_CargarDatos()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            mt_CargarDatos()
            'mt_ShowMessage("La búsqueda se realizo correctamente", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Me.hdCodPro.Value.ToString() <> "" Then
                Dim cod As Integer
                cod = CInt(Me.hdCodPro.Value)
                obj.AbrirConexion()
                obj.Ejecutar("ODO_editarProcedimiento", cod, Me.cboTipoEstudio.SelectedValue, Me.txtNombre.Text, Me.txtDescripcion.Text, Me.cboEstado.SelectedValue, cod_user)
                obj.CerrarConexion()
            Else
                obj.AbrirConexion()
                obj.Ejecutar("ODO_insertarProcedimiento", Me.cboTipoEstudio.SelectedValue, Me.txtNombre.Text, Me.txtDescripcion.Text, cod_user)
                obj.CerrarConexion()
            End If
            Me.hdCodPro.Value = ""
            mt_CargarDatos()
            mt_ShowMessage("se grabó correctamente la información", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    If Me.hdCodPro.Value.ToString() <> "" Then
        '        Dim cod As Integer
        '        cod = CInt(Me.hdCodPro.Value)
        '        obj.AbrirConexion()
        '        obj.Ejecutar("ODO_eliminarProcedimiento", cod)
        '        obj.CerrarConexion()
        '        mt_ShowMessage("se eliminó correctamente la información", MessageType.Success)
        '    End If
        'Catch ex As Exception
        '    mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        'End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal Type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & Type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_ListarProcedimiento", -1, Me.cboTipoEstBus.SelectedValue, -1, Me.txtBuscar.Text)
            obj.CerrarConexion()
            Me.gvProcedimiento.DataSource = dt
            Me.gvProcedimiento.Caption = "<label>LISTA DE PROCEDIMIENTOS DE TIPO ESTUDIO: " & Me.cboTipoEstBus.SelectedItem.Text & "</label>"
            Me.gvProcedimiento.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
