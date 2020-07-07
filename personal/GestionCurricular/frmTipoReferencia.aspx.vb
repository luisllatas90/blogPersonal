﻿
Partial Class GestionCurricular_frmTipoReferencia
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
            End If
            cod_user = Session("id_per") 'Request.QueryString("id")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            mt_CargarDatos()
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

    Protected Sub gvTipoReferencia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTipoReferencia.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Me.cboEstado.Enabled = True
                Me.hdcodigo_tip.Value = Me.gvTipoReferencia.DataKeys(index).Values("codigo_tip")
                Me.txtNombre.Text = Me.gvTipoReferencia.DataKeys(index).Values("nombre")
                Me.cboEstado.SelectedValue = Me.gvTipoReferencia.DataKeys(index).Values("estado").ToString
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Me.hdcodigo_tip.Value.ToString() <> "" Then
                Dim cod As Integer
                cod = CInt(Me.hdcodigo_tip.Value)
                obj.AbrirConexion()
                obj.Ejecutar("TipoReferencia_actualizar", cod, Me.txtNombre.Text, IIf(Me.cboEstado.SelectedValue = "1", 1, 0), cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido actualizados correctamente !", MessageType.Success)
            Else
                obj.AbrirConexion()
                obj.Ejecutar("TipoReferencia_insertar", Me.txtNombre.Text, IIf(Me.cboEstado.SelectedValue = "1", 1, 0), cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido registrados correctamente !", MessageType.Success)
            End If
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TipoReferencia_Listar", -1, Me.txtBuscar.Text)
            obj.CerrarConexion()
            Me.gvTipoReferencia.DataSource = dt
            Me.gvTipoReferencia.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
