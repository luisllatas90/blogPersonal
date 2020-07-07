﻿
Partial Class logistica_frmProcedimientoCEFOCurso
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
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rowDelete As Integer
        Try
            If Me.gvProcedimiento.Rows.Count = 0 Then
                Throw New Exception("¡ No hay registro para realizar esta operación !")
            End If
            For x As Integer = 0 To Me.gvProcedimiento.Rows.Count - 1
                Dim chk As CheckBox = Me.gvProcedimiento.Rows(x).FindControl("chkEliminar")
                If chk.Checked Then
                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    Dim cod As Integer
                    cod = CInt(Me.gvProcedimiento.DataKeys(x).Values("codigo_cup"))
                    obj.AbrirConexion()
                    obj.Ejecutar("ODO_eliminarCursoProcedimiento", cod, cod_user)
                    obj.CerrarConexion()
                    rowDelete = rowDelete + 1
                End If
            Next
            If rowDelete = 0 Then
                Throw New Exception("¡ Seleccione al menos un registro para realizar esta operación !")
            Else
                mt_ShowMessage("¡ Se han eliminado " & rowDelete & " registros!", MessageType.Info)
                mt_ListarCursoProcedimiento(Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            mt_ListarCursoProcedimiento(Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    End Sub

    Protected Sub cboTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoEstudio.SelectedIndexChanged
        Try
            mt_ComboCarreraProf(Me.cboTipoEstudio.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarPro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarPro.SelectedIndexChanged
        Try
            mt_ComboPlanEstudio(Me.cboTipoEstudio.SelectedValue, Me.cboCarPro.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlaEst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlaEst.SelectedIndexChanged
        Try
            mt_ComboCurso(Me.cboPlaEst.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If fc_BuscarProcedimiento(Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue, CInt(Me.hdCodPro.Value)) Then
                obj.AbrirConexion()
                obj.Ejecutar("ODO_insertarCursoProcedimiento", CInt(Me.hdCodPro.Value), Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue, Me.txtCant.Text.Trim, cod_user)
                obj.CerrarConexion()
            Else
                mt_ShowMessage("El " & Me.txtNombre.Text & " ya ha sido registrado en el Curso: " & Me.cboCurso.SelectedItem.Text, MessageType.Warning)
            End If
            mt_ListarCursoProcedimiento(Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvProcedimiento_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProcedimiento.RowCommand
        Try
            'Dim index As Integer
            'index = CInt(e.CommandArgument)
            'If e.CommandName = "Eliminar" Then
            '    Dim obj As New ClsConectarDatos
            '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            '    Dim cod As Integer
            '    cod = CInt(Me.gvProcedimiento.DataKeys(index).Values("codigo_cup"))
            '    obj.AbrirConexion()
            '    obj.Ejecutar("ODO_eliminarCursoProcedimiento", cod, cod_user)
            '    obj.CerrarConexion()
            '    mt_ListarCursoProcedimiento(Me.cboPlaEst.SelectedValue, Me.cboCurso.SelectedValue)
            'End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal Type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & Type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_ComboCarreraProf(ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CP", codigo_test, 815)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarPro, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ComboPlanEstudio(ByVal codigo_test As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPlanEstudio", "PE", codigo_test, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlaEst, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ComboCurso(ByVal codigo_pes As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCurso", "CU", codigo_pes, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCurso, dt, "codigo_Cur", "nombre_Cur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_ListarCursoProcedimiento(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_listarCursoProcedimiento", -1, -1, codigo_pes, codigo_cur)
            obj.CerrarConexion()
            Me.gvProcedimiento.DataSource = dt
            Me.gvProcedimiento.Caption = "<label>LISTA DE PROCEDIMIENTOS DE TIPO ESTUDIO:" & Me.cboTipoEstudio.SelectedItem.Text & _
                                            " - CARRERA PROFESIONAL: " & Me.cboCarPro.SelectedItem.Text & _
                                            " - PLAN ESTUDIO: " & Me.cboPlaEst.SelectedItem.Text & _
                                            " - CURSO: " & Me.cboCurso.SelectedItem.Text & "</label>"
            Me.gvProcedimiento.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_BuscarProcedimiento(ByVal codigo_pes As Integer, ByVal codigo_cur As Integer, ByVal codigo_pro As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ODO_listarCursoProcedimiento", -1, codigo_pro, codigo_pes, codigo_cur)
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
