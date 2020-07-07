﻿
Partial Class GestionCurricular_frmSuspensionPorHoras
    Inherits System.Web.UI.Page

#Region "Variables"

    Private cod_user As Integer
    Private oeSPH As e_SuspensionPorHoras, odSPH As d_SuspensionPorHoras

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
            cod_user = Session("id_per")
            If Not IsPostBack Then
                mt_CargarAños()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAnio.SelectedIndexChanged
        Try
            mt_CargarDatos(IIf(Me.cboAnio.selectedvalue = -1, 0, Me.cboAnio.selectedvalue))
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Session("dea_codigo_sph") = ""
            Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            oeSPH = New e_SuspensionPorHoras : odSPH = New d_SuspensionPorHoras
            With oeSPH
                .descripcion_sph = Me.txtdescripcion.text.trim : .fecha_sph = CDate(Me.fecRegistro.text.trim)
                .horaInicio_sph = Me.txtHoraIni.text.trim : .horaFin_sph = Me.txthorafin.text.trim : .codigo_per = cod_user
            End With
            If String.IsNullOrEmpty(Session("dea_codigo_sph")) Then
                If odSPH.fc_RegistrarSuspensionPorHoras(oeSPH) Then
                    mt_ShowMessage("¡ Se registró correctamente la suspensión por horas !", MessageType.Success)
                    mt_CargarDatos(IIf(Me.cboAnio.selectedvalue = -1, 0, Me.cboAnio.selectedvalue))
                End If
            Else
                oeSPH.codigo_sph = Session("dea_codigo_sph")
                If odSPH.fc_ActualizarSuspensionPorHoras(oeSPH) Then
                    mt_ShowMessage("¡ Se actualizó correctamente la suspensión por horas !", MessageType.Success)
                    mt_CargarDatos(IIf(Me.cboAnio.selectedvalue = -1, 0, Me.cboAnio.selectedvalue))
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvSPH_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSPH.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Session("dea_codigo_sph") = Me.gvSPH.DataKeys(index).Values("codigo_sph")
                Me.txtdescripcion.text = Me.gvSPH.DataKeys(index).Values("descripcion_sph")
                Me.fecRegistro.text = Me.gvSPH.DataKeys(index).Values("fecha_sph")
                Me.txtHoraIni.text = Me.gvSPH.DataKeys(index).Values("horaInicio_sph")
                Me.txthorafin.text = Me.gvSPH.DataKeys(index).Values("horaFin_sph")
                Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
            End If
            If e.CommandName = "Eliminar" Then
                oeSPH = New e_SuspensionPorHoras : odSPH = New d_SuspensionPorHoras
                oeSPH.codigo_sph = Me.gvSPH.DataKeys(index).Values("codigo_sph") : oeSPH.codigo_per = cod_user
                If odSPH.fc_EliminarSuspensionPorHoras(oeSPH) Then
                    mt_ShowMessage("¡ Se eliminó correctamente la suspensión por horas !", MessageType.Success)
                    mt_CargarDatos(IIf(Me.cboAnio.selectedvalue = -1, 0, Me.cboAnio.selectedvalue))
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        'If modal Then
        '    Me.divAlertModal.Visible = True
        '    Me.lblMensaje.InnerText = Message
        '    Me.validar.Value = "0"
        '    updMensaje.Update()
        'Else
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        'End If
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarAños()
        Dim dt As New System.Data.DataTable
        Try
            oeSPH = New e_SuspensionPorHoras : odSPH = New d_SuspensionPorHoras
            oeSPH.tipo = "AF"
            dt = odSPH.fc_ListarSuspensionPorHoras(oeSPH)
            mt_CargarCombo(Me.cboAnio, dt, "codigo", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal _año As Integer)
        Dim dt As New System.Data.DataTable
        Try
            oeSPH = New e_SuspensionPorHoras : odSPH = New d_SuspensionPorHoras
            oeSPH.año = _año
            dt = odSPH.fc_ListarSuspensionPorHoras(oeSPH)
            Me.gvSPH.DataSource = dt
            Me.gvSPH.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
