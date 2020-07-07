﻿
Partial Class administrativo_tramite_FrmListaDevolucionesPendientes
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Me.txt_persona.Attributes.Add("placeholder", "Introducir nombre")
            cargarBancos()
            'ddlBanco.Enabled = False
            'txtCodigoInterbancario.Enabled = False
            HDCodigo_ini.Value = 0
            btnBuscar_Click(sender, e)
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Me.gvDeudas.DataSource = Nothing
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()

            'Response.Write("TES_ActualizarDatosDevolucion " & txt_persona.Text & "," & IIf(ddlEstado.SelectedIndex = 0, "P", "E"))
            dt = obj.TraerDataTable("TES_ActualizarDatosDevolucion", txt_persona.Text, IIf(ddlEstado.SelectedIndex = 0, "P", "E"))
            obj.CerrarConexion()

            Me.gvDeudas.DataSource = dt
            Me.gvDeudas.DataBind()
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Public Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
    End Sub

    Public Sub cargarBancos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("sp_ListarBancos", "TO", "")
            obj.CerrarConexion()
            Me.ddlBanco.DataSource = dt
            Me.ddlBanco.DataTextField = "banco"
            Me.ddlBanco.DataValueField = "codigo_Ban"
            Me.ddlBanco.DataBind()
            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvDeudas_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvDeudas.RowUpdating
        Try
            If ddlEstado.SelectedValue = "P" Then
                txtDni.Text = Me.gvDeudas.Rows(e.RowIndex).Cells(3).Text

                txtTitular.Text = HttpUtility.HtmlDecode(Me.gvDeudas.Rows(e.RowIndex).Cells(2).Text)

                txtTitular.Text = Me.gvDeudas.Rows(e.RowIndex).Cells(2).Text
                lblDetalle_nc.Text = Me.gvDeudas.Rows(e.RowIndex).Cells(5).Text
                HDCodigo_Cin.Value = gvDeudas.DataKeys(e.RowIndex).Values(0).ToString()
            Else
                lblDetalle_nc.Text = Me.gvDeudas.Rows(e.RowIndex).Cells(5).Text
                HDCodigo_Cin.Value = gvDeudas.DataKeys(e.RowIndex).Values(0).ToString()

                ddlTipoDevolucion.SelectedValue = gvDeudas.DataKeys(e.RowIndex).Values(2).ToString()
                ddlFormaPago.SelectedValue = gvDeudas.DataKeys(e.RowIndex).Values(3).ToString()
                txtDni.Text = gvDeudas.DataKeys(e.RowIndex).Values(4).ToString()
                txtTitular.Text = gvDeudas.DataKeys(e.RowIndex).Values(5).ToString()

                ddlBanco.SelectedValue = gvDeudas.DataKeys(e.RowIndex).Values(6).ToString()
                txtCodigoInterbancario.Text = gvDeudas.DataKeys(e.RowIndex).Values(7).ToString()
                txtObservacion.Text = gvDeudas.DataKeys(e.RowIndex).Values(8).ToString()

                txtObservacion.Text = gvDeudas.DataKeys(e.RowIndex).Values(8).ToString()
                HDCodigo_ini.Value = gvDeudas.DataKeys(e.RowIndex).Values(9).ToString()
            End If
        Catch ex As Exception
            ' Me.txtTitular.Text = ex.Message
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Call LimpiarControles()
        Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim tipo As String = IIf(ddlEstado.SelectedValue = "P", "I", "E")
            obj.AbrirConexion()
            obj.Ejecutar("TES_RegistrarInformacionIncompleta", HDCodigo_ini.Value, ddlTipoDevolucion.SelectedValue, ddlFormaPago.SelectedValue, txtDni.Text, _
                         txtTitular.Text, CInt(Me.ddlBanco.SelectedValue), txtCodigoInterbancario.Text, txtObservacion.Text, HDCodigo_Cin.Value, _
                         Session("id_per"), tipo)

            obj.CerrarConexion()
            Call LimpiarControles()
            ShowMessage("Datos Actualizados", MessageType.Success)

            btnBuscar_Click(sender, e)
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Sub LimpiarControles()
        ddlTipoDevolucion.SelectedIndex = 0
        ddlFormaPago.SelectedIndex = 0
        txtDni.Text = ""
        txtTitular.Text = ""
        ddlBanco.SelectedIndex = 0
        txtCodigoInterbancario.Text = ""
        txtObservacion.Text = ""
    End Sub

End Class
