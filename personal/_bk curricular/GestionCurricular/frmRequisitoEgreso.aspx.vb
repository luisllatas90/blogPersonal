
Partial Class GestionCurricular_frmRequisitoEgreso
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer

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
                Response.Redirect("../../../sinacceso.html")
            End If
            cod_user = Session("id_per") 'Request.QueryString("id")
            cod_ctf = Request.QueryString("ctf")
            If IsPostBack = False Then
                mt_CargarCarreraProfesional()
                mt_CargarTipoRequisito()
                Me.btnAgregar.Visible = False
            End If
            'btnEditar.Enabled = False
            'btnGrabar.Enabled = False
            'btnCancelar.Enabled = False
            'Me.btnAgregar.Enabled = False
            'If Me.txtNombre.Visible Then Me.txtNombre.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
    '    Try
    '        mt_CargarDatos(Me.cboPlanCurr.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    '    Dim obj As New ClsConectarDatos
    '    Dim x As Integer
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        For x = 0 To Me.gvRequisitoEgreso.Rows.Count - 1
    '            Dim chk As CheckBox = Me.gvRequisitoEgreso.Rows(x).FindControl("chkSelect")
    '            Dim txt As TextBox = Me.gvRequisitoEgreso.Rows(x).FindControl("txtCantidad")
    '            Dim codigo_req, codigo_tip, codigo_pcur, cantidad As Integer
    '            codigo_req = CInt(Me.gvRequisitoEgreso.DataKeys(x).Values("codigo_req"))
    '            codigo_tip = CInt(Me.gvRequisitoEgreso.DataKeys(x).Values("codigo_tip"))
    '            codigo_pcur = CInt(Me.gvRequisitoEgreso.DataKeys(x).Values("codigo_pcur"))
    '            cantidad = CInt(txt.Text)
    '            obj.AbrirConexion()
    '            If chk.Checked Then
    '                If codigo_req = -1 Then
    '                    obj.Ejecutar("RequisitoEgreso_insertar", codigo_tip, codigo_pcur, cantidad, cod_user)
    '                Else
    '                    obj.Ejecutar("RequisitoEgreso_actualizar", codigo_req, cantidad, 1, cod_user)
    '                End If
    '            Else
    '                If codigo_req <> -1 Then
    '                    obj.Ejecutar("RequisitoEgreso_actualizar", codigo_req, cantidad, 0, cod_user)
    '                End If
    '            End If
    '            obj.CerrarConexion()
    '            'btnGrabar.Enabled = False
    '            'btnCancelar.Enabled = False
    '            'btnListar.Enabled = True
    '        Next
    '        mt_ShowMessage("¡ Se ha guardado correctamente la información !", MessageType.Success)
    '        mt_CargarDatos(Me.cboPlanCurr.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub cboCarProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarProf.SelectedIndexChanged
        Try
            mt_CargarPlanCurricular(Me.cboCarProf.SelectedValue)
            If Me.gvRequisitoEgreso.Rows.Count > 0 Then Me.gvRequisitoEgreso.DataSource = Nothing : Me.gvRequisitoEgreso.DataBind()
            If Me.cboPlanCurr.SelectedIndex > -1 Then mt_CargarDatos(IIf(Me.cboPlanCurr.SelectedValue = -1, 0, Me.cboPlanCurr.SelectedValue))
            Me.btnAgregar.Visible = IIf(Me.cboPlanCurr.SelectedIndex > -1, True, False)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanCurr.SelectedIndexChanged
        Try
            mt_CargarDatos(IIf(Me.cboPlanCurr.SelectedValue = -1, 0, Me.cboPlanCurr.SelectedValue))
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditar.Click
    '    Try
    '        Dim x As Integer
    '        For x = 0 To Me.gvRequisitoEgreso.Rows.Count - 1
    '            Dim chk As CheckBox = Me.gvRequisitoEgreso.Rows(x).FindControl("chkSelect")
    '            Dim txt As TextBox = Me.gvRequisitoEgreso.Rows(x).FindControl("txtCantidad")
    '            chk.Enabled = True
    '            txt.Enabled = True
    '        Next
    '        ' Me.btnListar.Enabled = False
    '        Me.btnEditar.Enabled = False
    '        Me.btnGrabar.Enabled = True
    '        Me.btnCancelar.Enabled = True
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '    Try
    '        btnGrabar.Enabled = False
    '        btnCancelar.Enabled = False
    '        'btnListar.Enabled = True
    '        mt_CargarDatos(Me.cboPlanCurr.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If Me.cboCarProf.SelectedValue = "-1" Then
            Call mt_ShowMessage("Primero seleccione la carrera profesional", MessageType.Info)
        ElseIf Me.cboPlanCurr.SelectedValue = "-1" Then
            Call mt_ShowMessage("Primero seleccione el plan curricular", MessageType.Info)
        Else
            Me.chkTipoRequisito.Checked = False
            Me.chkTipoRequisito.Text = " Agregar Requisito de Egreso"
            Me.cboTipoRequisito.Visible = True
            Me.cboTipoRequisito.Enabled = True
            'Me.txtNombre.Visible = False
            Me.txtNombre.Text = String.Empty
            'Me.cboEstado.Enabled = False
            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
            Session("gc_codigo_req") = ""
            Session("gc_codigo_tip") = ""
            Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
            Me.cboTipoRequisito.Focus()
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim codigo_req, codigo_tip, x, codigo_aux As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.IniciarTransaccion()
            If String.IsNullOrEmpty(Session("gc_codigo_req")) Then
                If Me.chkTipoRequisito.Checked Then
                    dt = obj.TraerDataTable("TipoRequisitoEgreso_insertar", Me.txtNombre.Text, 1, cod_user)
                    If dt.Rows.Count = 0 Then Throw New Exception("¡ No se puedo Registrar el Tipo de Requisito !")
                    codigo_tip = dt.Rows(0).Item(0)
                    mt_CargarTipoRequisito()
                Else
                    codigo_tip = Me.cboTipoRequisito.SelectedValue
                    For x = 0 To Me.gvRequisitoEgreso.Rows.Count - 1
                        codigo_aux = Me.gvRequisitoEgreso.DataKeys(x).Values("codigo_tip")
                        If codigo_aux = codigo_tip Then
                            Me.divAlertModal.Visible = True
                            Me.lblMensaje.InnerText = "¡ El requisito ya esta registrado !"
                            updMensaje.Update()
                            Page.RegisterStartupScript("Pop", "<script>openModal('" & "Agregar" & "');</script>")
                            Me.cboTipoRequisito.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                obj.Ejecutar("RequisitoEgreso_insertar", codigo_tip, Me.cboPlanCurr.SelectedValue, Me.txtCantidad.Text, cod_user)
                mt_ShowMessage("¡ Los datos han sido registrados correctamente !" & " " & Me.txtNombre.Text, MessageType.Success)
            Else
                codigo_tip = Session("gc_codigo_tip")
                If Me.chkTipoRequisito.Checked Then
                    obj.Ejecutar("TipoRequisitoEgreso_actualizar", codigo_tip, Request("txtNombre").ToString, 1, cod_user)
                    mt_CargarTipoRequisito()
                Else
                    codigo_tip = Me.cboTipoRequisito.SelectedValue
                End If
                codigo_req = Session("gc_codigo_req")
                obj.Ejecutar("RequisitoEgreso_actualizar", codigo_req, Me.txtCantidad.Text, 1, cod_user)
                mt_ShowMessage("¡ Los datos han sido editados correctamente !" & " " & Request("txtNombre").ToString, MessageType.Success)
            End If
            'obj.Ejecutar("TipoRequisitoEgreso_insertar", Me.txtNombre.Text, IIf(Me.cboEstado.SelectedValue = "1", 1, 0), cod_user)
            obj.TerminarTransaccion()
            mt_CargarDatos(Me.cboPlanCurr.SelectedValue)
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvRequisitoEgreso_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvRequisitoEgreso.RowCommand
        Dim obj As New ClsConectarDatos
        Dim index, codigo_req, codigo_tip, cantidad As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            index = CInt(e.CommandArgument)
            codigo_req = Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_req")
            codigo_tip = Me.gvRequisitoEgreso.DataKeys(index).Values("codigo_tip")
            cantidad = Me.gvRequisitoEgreso.DataKeys(index).Values("cantidad")
            Me.cboTipoRequisito.Enabled = True
            Select Case e.CommandName
                Case "Editar"
                    Me.txtNombre.Text = String.Empty
                    Me.chkTipoRequisito.Checked = False
                    Me.chkTipoRequisito.Text = " Editar Requisito de Egreso"
                    'Me.txtNombre.Visible = False
                    Session("gc_codigo_tip") = codigo_tip
                    Session("gc_codigo_req") = codigo_req
                    Me.cboTipoRequisito.SelectedValue = codigo_tip
                    Me.cboTipoRequisito.Visible = True
                    Me.cboTipoRequisito.Enabled = False
                    Me.txtCantidad.Text = cantidad
                    'Me.cboEstado.Enabled = True
                    Session("gc_nombre_tip") = Me.gvRequisitoEgreso.DataKeys(index).Values("nombre")
                    Me.txtNombre.Text = Session("gc_nombre_tip")
                    Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
                Case "Eliminar"
                    obj.AbrirConexion()
                    obj.Ejecutar("RequisitoEgreso_actualizar", codigo_req, cantidad, 0, cod_user)
                    obj.CerrarConexion()
                    mt_ShowMessage("¡ Se ha eliminado correctamente !", MessageType.Success)
                    mt_CargarDatos(Me.cboPlanCurr.SelectedValue)
            End Select
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkTipoRequisito_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTipoRequisito.CheckedChanged
        'Me.txtNombre.Visible = Me.chkTipoRequisito.Checked
        Me.cboTipoRequisito.Visible = Not Me.chkTipoRequisito.Checked
        'Me.cboEstado.Enabled = IIf(chkTipoRequisito.Text = " Agregar", False, True)
        If chkTipoRequisito.Text = " Editar" Then Me.txtNombre.Text = Session("gc_nombre_tip")
        'If Me.chkTipoRequisito.Checked Then

        'End If
        Me.txtNombre.Focus()
        'Me.panModal.Update()
        'If Me.chkTipoRequisito.Checked Then Me.txtNombre.Focus()
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            'cod_user = 3133
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            End If

            obj.CerrarConexion()

            Call mt_CargarCombo(Me.cboCarProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanCurricular(ByVal codigo_cpf As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPlanCurricular", codigo_cpf, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanCurr, dt, "codigo_pcur", "nombre_pcur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_pec As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("RequisitoEgreso_Listar", "RE", -1, -1, codigo_pec)
            obj.CerrarConexion()
            Me.gvRequisitoEgreso.DataSource = dt
            Me.gvRequisitoEgreso.DataBind()
            'If Me.gvRequisitoEgreso.Rows.Count > 0 Then
            'btnEditar.Enabled = True
            'btnAgregar.Enabled = True
            'Else
            'btnEditar.Enabled = False
            'btnAgregar.Enabled = False
            'End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarTipoRequisito()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("TipoRequisitoEgreso_Listar", -1, "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboTipoRequisito, dt, "codigo_tip", "nombre")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
