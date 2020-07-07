﻿Partial Class Alumni_frmEmpresaAprobar
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_Categoria As New d_Categoria
    Dim md_Departamento As New d_Departamento
    Dim md_Provincia As New d_Provincia
    Dim md_Distrito As New d_Distrito
    Dim md_Empresa As New d_Empresa
    Dim md_RevisionEmpresa As New d_RevisionEmpresa
    Dim md_Sector As New d_Sector

    'ENTIDADES
    Dim me_Categoria As e_Categoria
    Dim me_Departamento As e_Departamento
    Dim me_Provincia As e_Provincia
    Dim me_Distrito As e_Distrito
    Dim me_Empresa As e_Empresa
    Dim me_RevisionEmpresa As e_RevisionEmpresa

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
                Call mt_LimpiarVariablesSession()

                Session("frmEmpresaAprobar-cod_ctf") = Request.QueryString("ctf")
                Call mt_CargarComboDepartamento()
                Call mt_CargarComboTipo()
                Call mt_CargarComboSector()
                Call mt_CargarComboTelefono()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call mt_FlujoTabs("Listado")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbDepartamento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDepartamento.SelectedIndexChanged
        Try
            Call mt_CargarComboProvincia()            

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbProvincia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvincia.SelectedIndexChanged
        Try
            Call mt_CargarComboDistrito()

            Call mt_UpdatePanel("Registro")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmEmpresaAprobar-codigo_emp") = Me.grwLista.DataKeys(index).Values("codigo_emp")
            Dim ruc_emp As String = Me.grwLista.DataKeys(index).Values("ruc_emp")

            Select Case e.CommandName
                Case "Aprobar"
                    If Not fu_ValidarSeleccionEmpresa(ruc_emp) Then Exit Sub

                    If Not mt_CargarFormularioRegistro(Session("frmEmpresaAprobar-codigo_emp")) Then Exit Sub

                    Session("frmEmpresaAprobar-codigoEstado_cat") = g_VariablesGlobales.EstadoEmpresaAprobado

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "Rechazar"
                    If Not fu_ValidarSeleccionEmpresa(ruc_emp) Then Exit Sub

                    If Not mt_CargarFormularioRegistro(Session("frmEmpresaAprobar-codigo_emp")) Then Exit Sub

                    Session("frmEmpresaAprobar-codigoEstado_cat") = g_VariablesGlobales.EstadoEmpresaRechazado

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If Not mt_RegistrarRevision() Then Exit Sub

            Call mt_CargarDatos()

            Call mt_FlujoTabs("Listado")
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
                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

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

    Private Sub mt_LimpiarControles()
        Try
            Me.cmbDepartamento.SelectedValue = "0"
            Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
            Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
            Me.txtRuc.Text = String.Empty
            Me.txtRazonSocial.Text = String.Empty
            Me.txtNombreComercial.Text = String.Empty
            Me.txtAbreviatura.Text = String.Empty
            Me.cmbTipoEmpresa.SelectedValue = "0"
            Me.cmbSector.SelectedValue = "0"
            Me.txtDireccion.Text = String.Empty
            Me.txtDireccionWeb.Text = String.Empty
            Me.txtCorreo.Text = String.Empty
            Me.cmbTelefono.SelectedValue = String.Empty
            Me.txtTelefono.Text = String.Empty
            Me.txtCelular.Text = String.Empty
            Me.txtComentario.Text = String.Empty
            Me.chkAccesoCampus.Checked = False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try
            Session("frmEmpresaAprobar-cod_ctf") = Nothing
            Session("frmEmpresaAprobar-codigo_emp") = Nothing
            Session("frmEmpresaAprobar-codigoEstado_cat") = Nothing
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipo()
        Try
            Dim dt As New Data.DataTable : me_Categoria = New e_Categoria

            With me_Categoria
                .operacion = "GEN"
                .grupo_cat = "TIPO_EMPRESA"
            End With
            dt = md_Categoria.ListarCategoria(me_Categoria)

            Call md_Funciones.CargarCombo(Me.cmbTipoEmpresa, dt, "codigo_cat", "nombre_cat", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSector()
        Try
            Dim dt As New Data.DataTable

            dt = md_Sector.BuscaSector()

            Call md_Funciones.CargarCombo(Me.cmbSector, dt, "codigo_sec", "nombre_sec", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDepartamento()
        Try
            Dim dt As New Data.DataTable : me_Departamento = New e_Departamento

            With me_Departamento
                .operacion = "GEN"
                .codigo_pai = "156"
            End With
            dt = md_Departamento.ListarDepartamento(me_Departamento)

            Call md_Funciones.CargarCombo(Me.cmbDepartamento, dt, "codigo_Dep", "nombre_Dep", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboProvincia()
        Try
            Dim dt As New Data.DataTable : me_Provincia = New e_Provincia

            With me_Provincia
                .operacion = "GEN"
                If Not String.IsNullOrEmpty(Me.cmbDepartamento.SelectedValue) Then .codigo_dep = Me.cmbDepartamento.SelectedValue
            End With
            dt = md_Provincia.ListarProvincia(me_Provincia)

            Call md_Funciones.CargarCombo(Me.cmbProvincia, dt, "codigo_Pro", "nombre_Pro", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboDistrito()
        Try
            Dim dt As New Data.DataTable : me_Distrito = New e_Distrito

            With me_Distrito
                .operacion = "GEN"
                If Not String.IsNullOrEmpty(Me.cmbProvincia.SelectedValue) Then .codigo_pro = Me.cmbProvincia.SelectedValue
            End With
            dt = md_Distrito.ListarDistrito(me_Distrito)

            Call md_Funciones.CargarCombo(Me.cmbDistrito, dt, "codigo_Dis", "nombre_Dis", True, "[-- SELECCIONE --]", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTelefono()
        Try
            Dim dt As Data.DataTable = md_Funciones.ObtenerDataTable("CODIGO_TELEFONICO")

            Call md_Funciones.CargarCombo(Me.cmbTelefono, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")            
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New Data.DataTable : me_Empresa = New e_Empresa

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_Empresa
                .operacion = "GEN"
                .nombreComercial_emp = Me.txtNombreFiltro.Text.Trim
                .codigoEstado_cat = g_VariablesGlobales.EstadoEmpresaGenerado
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_CargarFormularioRegistro(ByVal codigo_emp As Integer) As Boolean
        Try
            Dim dt As New Data.DataTable : me_Empresa = New e_Empresa

            With me_Empresa
                .operacion = "GEN"
                .codigo_emp = codigo_emp
            End With

            dt = md_Empresa.ListarEmpresa(me_Empresa)
            If dt.Rows.Count = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles()

            With dt.Rows(0)
                Me.txtRuc.Text = .Item("ruc_emp").ToString                
                Me.txtNombreComercial.Text = .Item("nombreComercial_emp").ToString
                Me.txtRazonSocial.Text = .Item("razonSocial_emp").ToString
                Me.txtAbreviatura.Text = .Item("abreviatura_emp").ToString
                Me.cmbTipoEmpresa.SelectedValue = .Item("codigoTipo_cat").ToString
                Me.cmbSector.SelectedValue = .Item("codigo_sec").ToString
                Me.cmbDepartamento.SelectedValue = .Item("codigo_dep").ToString
                Call cmbDepartamento_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbProvincia.SelectedValue = .Item("codigo_pro").ToString
                Call cmbProvincia_SelectedIndexChanged(Nothing, Nothing)
                Me.cmbDistrito.SelectedValue = .Item("codigo_dis").ToString
                Me.txtDireccion.Text = .Item("direccion_emp").ToString
                Me.txtDireccionWeb.Text = .Item("direccionWeb_emp").ToString
                Me.txtCorreo.Text = .Item("correo_emp").ToString
                Me.cmbTelefono.SelectedValue = .Item("prefijoTel_emp").ToString
                Me.txtTelefono.Text = .Item("telefono_emp").ToString
                Me.txtCelular.Text = .Item("celular_emp").ToString
                If .Item("accesoCampus_emp").ToString.Trim = "S" Then chkAccesoCampus.Checked = True Else chkAccesoCampus.Checked = False
            End With

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_RegistrarRevision() As Boolean
        Try
            If Not fu_ValidarRegistrarRevision() Then Return False

            me_RevisionEmpresa = New e_RevisionEmpresa

            With me_RevisionEmpresa
                .operacion = "I"
                .cod_user = cod_user
                .codigo_rem = 0
                .codigo_emp = Session("frmEmpresaAprobar-codigo_emp")
                .codigoRevisor_pso = cod_user
                .codigoEstado_cat = Session("frmEmpresaAprobar-codigoEstado_cat")
                .vigente_rem = "S"
                .comentario_rem = Me.txtComentario.Text.Trim
            End With

            md_RevisionEmpresa.RegistrarRevisionEmpresa(me_RevisionEmpresa)

            If Session("frmEmpresaAprobar-codigoEstado_cat") = g_VariablesGlobales.EstadoEmpresaAprobado Then
                Call mt_ShowMessage("¡La empresa ha sido aprobada exitosamente!", MessageType.success)
            Else
                Call mt_ShowMessage("¡La empresa ha sido rechazada exitosamente!", MessageType.success)
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarRevision() As Boolean
        Try
            If Session("frmEmpresaAprobar-codigo_emp") Is Nothing OrElse Session("frmEmpresaAprobar-codigo_emp") = 0 Then mt_ShowMessage("El código de la empresa no ha sido encontrado.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.txtRuc.Text.Trim) Then mt_ShowMessage("Debe completar la información de la empresa ingresada por registro rápido.", MessageType.warning) : Me.txtRuc.Focus() : Return False
            If String.IsNullOrEmpty(Me.txtComentario.Text.Trim) Then mt_ShowMessage("Debe ingresar un comentario.", MessageType.warning) : Me.txtComentario.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarSeleccionEmpresa(ByVal ruc_emp As String) As Boolean
        Try
            If String.IsNullOrEmpty(ruc_emp) Then mt_ShowMessage("Debe completar la información de la empresa ingresada por registro rápido.", MessageType.warning) : Me.txtRuc.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
