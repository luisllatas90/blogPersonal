﻿
Partial Class logistica_frmProcedimientoCEFOPaquete
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
                    cod_user = Request.QueryString("id")
                End If
                'If IsPostBack = False Then
                '    mt_ComboProcedimiento()
                'End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            mt_ListarPaquetes(Me.cboProcedimiento.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If fc_BuscarPaquete(Me.cboProcedimiento.SelectedValue, CInt(Me.hdCodPaq.Value)) Then
                obj.AbrirConexion()
                obj.Ejecutar("ODO_insertarProcedimientoPaquete", Me.cboProcedimiento.SelectedValue, CInt(Me.hdCodPaq.Value), Me.txtCant.Text, cod_user)
                obj.CerrarConexion()
            Else
                mt_ShowMessage("El tratamiento: " & Me.txtNombre.Text & " ya esta registrado para " & Me.cboProcedimiento.SelectedItem.Text, MessageType.Warning)
            End If
            mt_ListarPaquetes(Me.cboProcedimiento.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowDelete As Integer
        Try
            If Me.gvTratamiento.Rows.Count = 0 Then
                Throw New Exception("¡ No hay registros para realizar esta operación !")
            End If
            For x As Integer = 0 To Me.gvTratamiento.Rows.Count - 1
                Dim chk As CheckBox = Me.gvTratamiento.Rows(x).FindControl("chkEliminar")
                If chk.Checked Then
                    Dim obj As New ClsConectarDatos
                    Dim cod As Integer
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    cod = CInt(Me.gvTratamiento.DataKeys(x).Values("codigo_prp"))
                    obj.AbrirConexion()
                    obj.Ejecutar("ODO_eliminarProcedimientoPaquete", cod, cod_user)
                    obj.CerrarConexion()
                    rowDelete = rowDelete + 1
                End If
            Next
            If rowDelete = 0 Then
                Throw New Exception("¡ Seleccione al menos un registro para realizar esta operación !")
            Else
                mt_ShowMessage("¡ Se han eliminado " & rowDelete & " registros !", MessageType.Info)
                mt_ListarPaquetes(Me.cboProcedimiento.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvTratamiento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTratamiento.RowCommand
        Try
            'Dim index As Integer
            'index = CInt(e.CommandArgument)
            'If e.CommandName = "Eliminar" Then
            '    Dim obj As New ClsConectarDatos
            '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            '    Dim cod As Integer
            '    cod = CInt(Me.gvTratamiento.DataKeys(index).Values("codigo_prp"))
            '    obj.AbrirConexion()
            '    obj.Ejecutar("ODO_eliminarProcedimientoPaquete", cod, cod_user)
            '    obj.CerrarConexion()
            '    mt_ListarPaquetes(Me.cboProcedimiento.SelectedValue)
            'End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged
        Try
            mt_ComboProcedimiento(Me.cboTipoEstudio.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal Type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & Type.ToString & "');</script>")
    End Sub

    Private Sub mt_ComboProcedimiento(ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_ListarProcedimiento", -1, codigo_test, 1, "")
            obj.CerrarConexion()
            Me.cboProcedimiento.DataSource = dt
            Me.cboProcedimiento.DataTextField = "nombre_pro"
            Me.cboProcedimiento.DataValueField = "codigo_pro"
            Me.cboProcedimiento.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ListarPaquetes(ByVal codigo_pro As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_listarProcedimientoPaquete", -1, codigo_pro, -1)
            obj.CerrarConexion()
            Me.gvTratamiento.DataSource = dt
            Me.gvTratamiento.Caption = "<label>LISTA DE TRATAMIENTOS DE TIPO ESTUDIOS: " & Me.cboTipoEstudio.SelectedItem.Text & _
                                        " | PROCEDIMIENTO: " & Me.cboProcedimiento.SelectedItem.Text & "</label>"
            Me.gvTratamiento.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_BuscarPaquete(ByVal codigo_pro As Integer, ByVal codigo_paq As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_listarProcedimientoPaquete", -1, codigo_pro, codigo_paq)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
