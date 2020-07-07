﻿
Partial Class GestionCurricular_frmConfiguracionCicloAcademico
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
            cod_user = Request.QueryString("id")
            If IsPostBack = False Then
                mt_CargarTipoEstudio(Me.cboTipoEstudio, "GC")
                mt_CargarTipoEstudio(Me.cboTipEst, "TO")
                mt_CargarSemestre()
            End If
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

    Protected Sub gvConfiguracion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConfiguracion.RowCommand
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            If e.CommandName = "Editar" Then
                Me.hdcodigo_conf.Value = Me.gvConfiguracion.DataKeys(index).Values("codigo_conf")
                Me.cboTipEst.SelectedValue = Me.gvConfiguracion.DataKeys(index).Values("codigo_test")
                Me.cboSemestre.SelectedValue = Me.gvConfiguracion.DataKeys(index).Values("codigo_cac")
                Me.txtNombre.Text = Me.gvConfiguracion.DataKeys(index).Values("nombre_conf")
                Me.txtValor.Text = Me.gvConfiguracion.DataKeys(index).Values("valor_conf")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If Me.hdcodigo_conf.Value.ToString() <> "" Then
                Dim cod As Integer
                cod = CInt(Me.hdcodigo_conf.Value)
                obj.AbrirConexion()
                obj.Ejecutar("CicloAcademicoConf_actualizar", cod, Me.cboSemestre.SelectedValue, Me.cboTipEst.SelectedValue, Me.txtNombre.Text, Me.txtValor.Text, cod_user)
                obj.CerrarConexion()
                mt_ShowMessage("¡ Los datos han sido actualizados correctamente !", MessageType.Success)
            Else
                obj.AbrirConexion()
                obj.Ejecutar("CicloAcademicoConf_insertar", Me.cboSemestre.SelectedValue, Me.cboTipEst.SelectedValue, Me.txtNombre.Text, Me.txtValor.Text, cod_user)
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

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarTipoEstudio(ByVal cbo As DropDownList, ByVal tip As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", tip, -1)
            obj.CerrarConexion()
            mt_CargarCombo(cbo, dt, "codigo_test", "descripcion_test")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CicloAcademicoConf_Listar", -1, -1, Me.cboTipoEstudio.SelectedValue, Me.txtBuscar.Text)
            obj.CerrarConexion()
            Me.gvConfiguracion.DataSource = dt
            Me.gvConfiguracion.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
