﻿
Partial Class GestionDocumentaria_frmConfiguracionDocumentaria
    Inherits System.Web.UI.Page

#Region "Variables"
    Dim tipoestudio, codigo_tfu, codigo_usu As Integer

    Dim md_Funciones As d_Funciones
    Dim md_DocArea As d_DocArea
    Dim md_confDocArea As d_ConfigurarDocumentoArea
    Dim md_tipoDoc As d_TipoDocumenntacion
    Dim me_confDocArea As e_ConfigurarDocumentoArea
    Dim me_tipoDocumento As e_TipoDocumentacion
    Dim me_area As e_Area


    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("https://intranet.usat.edu.pe/campusvirtual/sinacceso.html")
        End If

        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")

        If IsPostBack = False Then
            Call mt_CargarComboEstado()
            Call mt_CargarComboArea()
            Call mt_CargarComboFuncion()
            Call mt_CargarConfiguracionDocumentos()
            Call mt_CargarComboTipoDocumento()

            Me.lbAgregar.Enabled = True
            Me.lbModificar.Enabled = False
            Me.ddlEstado_cda.Enabled = False
        End If
    End Sub

    Protected Sub lbAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAgregar.Click
        Try
            Dim codigo_tid As Integer = 0
            Dim dt As New Data.DataTable
            Dim codigo_cda As Integer = 0
            me_tipoDocumento = New e_TipoDocumentacion
            me_confDocArea = New e_ConfigurarDocumentoArea


            md_tipoDoc = New d_TipoDocumenntacion
            md_confDocArea = New d_ConfigurarDocumentoArea

            '************validaciones
            If (Me.ddlCodigo_are.SelectedValue = "") Then
                Call mt_ShowMessage("Debe Seleccionar un área", MessageType.warning)
                Me.ddlCodigo_are.Focus()
                Exit Sub
            ElseIf (Me.ddlCodigo_tfu.SelectedValue = "") Then
                Call mt_ShowMessage("Debe seleccionar una función", MessageType.warning)
                Me.ddlCodigo_tfu.Focus()
                Exit Sub
            ElseIf (Me.ddlCodigo_tid.SelectedValue = "") Then
                Call mt_ShowMessage("Ingrese una descripcion del documento", MessageType.warning)
                Me.ddlCodigo_tid.Focus()
                Exit Sub
                'ElseIf (Me.txtAbreviatura_tid.Text = "") Then
                '    Call mt_ShowMessage("Ingrese una abreviatura del documento", MessageType.warning)
                '    Me.txtAbreviatura_tid.Focus()
                '    Exit Sub
                'ElseIf Len(Me.txtAbreviatura_tid.Text.Trim) > 4 Then
                '    Call mt_ShowMessage("La abreviatura del documento debe contener 4 dígitos", MessageType.warning)
                '    Me.txtAbreviatura_tid.Focus()
                '    Exit Sub
            End If
            '************fin de validaciones

            'With me_tipoDocumento
            '    .codigo_tid = codigo_tid
            '    .descripcion_tid = Me.txtDescripcion_tid.Text
            '    .abreviatura_tid = Me.txtAbreviatura_tid.Text
            '    .usuarioReg = codigo_usu
            'End With

            'dt = md_tipoDoc.RegistrarActualizarTipoDocumentacion(me_tipoDocumento)
            'If dt.Rows.Count > 0 Then
            '    With dt.Rows(0)
            '        codigo_tid = .Item("codigo_tid")
            '    End With
            'End If

            'Me.txtCodigo_tid.Text = codigo_tid
            'Me.udpFiltros.Update()

            With me_confDocArea
                .codigo_are = Me.ddlCodigo_are.SelectedValue
                .codigo_cda = codigo_cda
                .codigo_tid = Me.ddlCodigo_tid.SelectedValue
                .codigo_tfu = Me.ddlCodigo_tfu.SelectedValue
                .estado_cda = Me.ddlEstado_cda.SelectedValue
                .usuarioReg = codigo_usu
            End With

            codigo_cda = md_confDocArea.RegistrarActualizarConfigurarDocumentoArea(me_confDocArea)
            Call mt_CargarConfiguracionDocumentos()
            Me.udpListadoConf.Update()

            Call mt_ShowMessage("Se registró con éxito la configuración del documento", MessageType.success)


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub gvListaConfiguraDocumnetos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaConfiguraDocumnetos.RowCommand
        Dim codigo_cda As Integer = 0
        Dim dt As Data.DataTable
        md_confDocArea = New d_ConfigurarDocumentoArea

        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_cda = Me.gvListaConfiguraDocumnetos.DataKeys(index).Values("codigo_cda")

            Select Case e.CommandName
                Case "editConfiDoc"

                    Me.lbAgregar.Enabled = False
                    Me.lbModificar.Enabled = True
                    Me.ddlEstado_cda.Enabled = True

                    dt = md_confDocArea.ListarConfigurarDocumentoArea("COD", codigo_cda, "")
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.ddlCodigo_are.SelectedValue = .Item("codigo_are")
                            Me.ddlCodigo_tfu.SelectedValue = .Item("codigo_tfu")
                            Me.ddlEstado_cda.SelectedValue = .Item("estado_cda")
                            Me.ddlCodigo_tid.SelectedValue = .Item("codigo_tid")
                            Me.txtCodigo_cda.Text = .Item("codigo_cda")
                            'Me.txtDescripcion_tid.Text = .Item("descripcion_tid")
                            'Me.txtAbreviatura_tid.Text = .Item("abreviatura_tid")
                        End With
                    End If
                    Me.udpFiltros.Update()
            End Select
            

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbModificar.Click
        Try
            'me_tipoDocumento = New e_TipoDocumentacion
            me_confDocArea = New e_ConfigurarDocumentoArea

            'md_tipoDoc = New d_TipoDocumenntacion
            md_confDocArea = New d_ConfigurarDocumentoArea

            'With me_tipoDocumento
            '    .codigo_tid = Me.txtCodigo_tid.Text
            '    .descripcion_tid = Me.txtDescripcion_tid.Text
            '    .abreviatura_tid = Me.txtAbreviatura_tid.Text
            '    .usuarioReg = codigo_usu
            'End With

            'md_tipoDoc.RegistrarActualizarTipoDocumentacion(me_tipoDocumento)

            With me_confDocArea
                .codigo_are = Me.ddlCodigo_are.SelectedValue
                .codigo_cda = Me.txtCodigo_cda.Text
                .codigo_tid = Me.ddlCodigo_tid.SelectedValue
                .codigo_tfu = Me.ddlCodigo_tfu.SelectedValue
                .estado_cda = Me.ddlEstado_cda.SelectedValue
                .usuarioReg = codigo_usu
            End With

            md_confDocArea.RegistrarActualizarConfigurarDocumentoArea(me_confDocArea)

            Call mt_CargarConfiguracionDocumentos()
            Call mt_limpiar()

            Call mt_ShowMessage("Registro Actualizado", MessageType.warning)

            Me.udpListadoConf.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbNuevaArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevaArea.Click
        Try
            Call mt_CargarAreas()
            Me.udpModalArea.Update()

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
        Catch ex As Exception
            Call mt_ShowMessage("No se encontraron registros", MessageType.warning)
        End Try


    End Sub

    Protected Sub lbNuevoTipDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevoTipDoc.Click
        Try
            Call mt_CargarTipoDocumento()
            Me.udpModalDoc.Update()

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalDoc", "openModalDoc();", True)
        Catch ex As Exception
            Call mt_ShowMessage("No se encontraron registros", MessageType.warning)
        End Try

    End Sub

    Protected Sub lbGuardarArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarArea.Click
        Try
            md_DocArea = New d_DocArea
            me_area = New e_Area

            Dim codigo_are As Integer = 0
            'Me.txtCodigo_are.Text = codigo_are

            '***** validaciones
            If Me.txtDescripcion_are.Text = "" Then
                Call mt_ShowMessage("Ingrese la descripción del área", MessageType.warning)
                Me.txtDescripcion_are.Focus()
                Exit Sub
            End If
            If Me.txtDescripcion_are.Text = "" Then
                Call mt_ShowMessage("Ingrese la abreviatura del área", MessageType.warning)
                Me.txtAbreviatura_are.Focus()
                Exit Sub
            End If
            If Len(Me.txtAbreviatura_are.Text) > 4 Then
                Call mt_ShowMessage("La abreviatura del área debe contener 4 dígitos", MessageType.warning)
                Me.txtAbreviatura_are.Focus()
                Exit Sub
            End If
            '*** fin validaciones


            With me_area
                If Me.txtCodigo_are.Text = "" Then
                    .codigo_are = "0"
                Else
                    .codigo_are = Me.txtCodigo_are.Text
                End If
                .descripcion_are = Me.txtDescripcion_are.Text
                .abreviatura_are = Me.txtAbreviatura_are.Text
                .usuarioReg = codigo_usu
            End With

            codigo_are = md_DocArea.RegistrarActualizarArea(me_area)

            Call mt_CargarComboArea()
            Me.udpFiltros.Update()

            Call mt_CargarAreas()

            Call mt_limpiarModArea()


            If Me.txtCodigo_are.Text = "0" Then
                Call mt_ShowMessage("Se registró el área con éxito", MessageType.success)
            Else
                Call mt_ShowMessage("Se actualizó el área con éxito", MessageType.success)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub gvAreas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAreas.RowCommand
        Try
            Dim codigo_are As Integer = 0
            Dim dt As New Data.DataTable
            md_DocArea = New d_DocArea

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_are = Me.gvAreas.DataKeys(index).Values("codigo_are")
            Me.txtCodigo_are.Text = codigo_are

            Select Case e.CommandName
                Case "editArea"

                    dt = md_DocArea.ListarArea("COD", codigo_are)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.txtDescripcion_are.Text = .Item("descripcion_are")
                            Me.txtAbreviatura_are.Text = .Item("abreviatura_are")
                        End With
                    End If
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbGuardarDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarDoc.Click
        Try
            md_tipoDoc = New d_TipoDocumenntacion
            me_tipoDocumento = New e_TipoDocumentacion

            Dim codigo_tid As Integer = 0
            'Me.txtCodigo_are.Text = codigo_are

            '***** validaciones
            If Me.txt_Descripcion_tid.Text = "" Then
                Call mt_ShowMessage("Ingrese la descripción del documento", MessageType.warning)
                Me.txtDescripcion_are.Focus()
                Exit Sub
            End If
            If Me.txt_Abreviatura_tid.Text = "" Then
                Call mt_ShowMessage("Ingrese la abreviatura del documento", MessageType.warning)
                Me.txtAbreviatura_are.Focus()
                Exit Sub
            End If
            If Len(Me.txt_Abreviatura_tid.Text) > 4 Then
                Call mt_ShowMessage("La abreviatura del documento debe contener 4 dígitos", MessageType.warning)
                Me.txtAbreviatura_are.Focus()
                Exit Sub
            End If
            '*** fin validaciones

            With me_tipoDocumento
                If Me.txt_codigo_tid.Text = "" Then
                    .codigo_tid = "0"
                Else
                    .codigo_tid = Me.txt_codigo_tid.Text
                End If
                .descripcion_tid = Me.txt_Descripcion_tid.Text
                .abreviatura_tid = Me.txt_Abreviatura_tid.Text
                .usuarioReg = codigo_usu
            End With

            codigo_tid = md_tipoDoc.RegistrarActualizarTipoDocumentacion(me_tipoDocumento)

            Call mt_CargarComboTipoDocumento()
            Me.udpFiltros.Update()

            Call mt_CargarTipoDocumento()

            Call mt_limpiarModDoc()

            If Me.txt_codigo_tid.Text = "0" Then
                Call mt_ShowMessage("Se registró el área con éxito", MessageType.success)
            Else
                Call mt_ShowMessage("Se actualizó el área con éxito", MessageType.success)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub gvListaDocumentos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaDocumentos.RowCommand
        Try
            Dim codigo_tid As Integer = 0
            Dim dt As New Data.DataTable
            md_tipoDoc = New d_TipoDocumenntacion

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_tid = Me.gvListaDocumentos.DataKeys(index).Values("codigo_tid")
            Me.txt_codigo_tid.Text = codigo_tid

            Select Case e.CommandName
                Case "editDoc"
                    dt = md_tipoDoc.ListarTipoDocumentacion("COD", codigo_tid)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.txt_Descripcion_tid.Text = .Item("descripcion_tid")
                            Me.txt_Abreviatura_tid.Text = .Item("abreviatura_tid")
                        End With
                    End If
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


#End Region

#Region "Métodos y Funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboEstado()
        Me.ddlEstado_cda.Items.Clear()
        Me.ddlEstado_cda.Items.Add(New ListItem("ACTIVO", "1"))
        Me.ddlEstado_cda.Items.Add(New ListItem("INACTIVO", "0"))
    End Sub

    Private Sub mt_CargarComboArea()
        Try
            md_Funciones = New d_Funciones
            md_DocArea = New d_DocArea

            Dim dt As New Data.DataTable

            dt = md_DocArea.ListarArea("GEN", 0)

            Call md_Funciones.CargarCombo(Me.ddlCodigo_are, dt, "codigo_are", "descripcion_are", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboFuncion()
        Try
            md_Funciones = New d_Funciones
            md_confDocArea = New d_ConfigurarDocumentoArea

            Dim dt As New Data.DataTable

            dt = md_confDocArea.ListarTipoFuncion("GEN", 0)

            Call md_Funciones.CargarCombo(Me.ddlCodigo_tfu, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    '----- Carga la grilla de documentos configurados

    Private Sub mt_CargarConfiguracionDocumentos()
        md_confDocArea = New d_ConfigurarDocumentoArea
        Dim dt As New Data.DataTable

        Try

            dt = md_confDocArea.ListarConfigurarDocumentoArea("GEN", 0, "")

            If dt.Rows.Count > 0 Then
                Me.gvListaConfiguraDocumnetos.DataSource = dt
                Me.gvListaConfiguraDocumnetos.DataBind()
                'Para ocultar las columnas y no pierda el valor
                'Call mt_ocultarColCursosAmbientes()

            Else
                Call mt_ShowMessage("No se encontraron documentos configurados", MessageType.warning)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Private Sub mt_CargarAreas()
        Try
            Dim codigo_are As Integer = 0
            md_DocArea = New d_DocArea

            Dim dt As New Data.DataTable
            dt = md_DocArea.ListarArea("GEN", 0)

            If dt.Rows.Count > 0 Then
                Me.gvAreas.DataSource = dt
                Me.gvAreas.DataBind()
                'Para ocultar las columnas y no pierda el valor
                'Call mt_ocultarColCursosAmbientes()
            End If

            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal();", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Sub mt_CargarTipoDocumento()
        Try
            Dim codigo_tid As Integer = 0
            md_tipoDoc = New d_TipoDocumenntacion

            Dim dt As New Data.DataTable

            dt = md_tipoDoc.ListarTipoDocumentacion("GEN", 0)

            If dt.Rows.Count > 0 Then
                Me.gvListaDocumentos.DataSource = dt
                Me.gvListaDocumentos.DataBind()
                'Para ocultar las columnas y no pierda el valor
                'Call mt_ocultarColCursosAmbientes()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Private Sub mt_limpiar()

        Me.ddlCodigo_are.SelectedValue = ""
        Me.ddlCodigo_tfu.SelectedValue = ""
        Me.ddlEstado_cda.SelectedValue = "1"
        'Me.txtAbreviatura_tid.Text = String.Empty
        Me.txtCodigo_cda.Text = String.Empty
        Me.ddlCodigo_tid.SelectedValue = ""
        'Me.txtDescripcion_tid.Text = String.Empty
        Me.lbModificar.Enabled = False
        Me.lbAgregar.Enabled = True
        Me.ddlEstado_cda.Enabled = False

    End Sub

    Private Sub mt_limpiarModArea()
        Me.txtCodigo_are.Text = String.Empty
        Me.txtDescripcion_are.Text = String.Empty
        Me.txtAbreviatura_are.Text = String.Empty
    End Sub
    Private Sub mt_limpiarModDoc()
        Me.txt_codigo_tid.Text = String.Empty
        Me.txt_Descripcion_tid.Text = String.Empty
        Me.txt_Abreviatura_tid.Text = String.Empty
    End Sub

    Private Sub mt_CargarComboTipoDocumento()
        Try
            md_Funciones = New d_Funciones
            md_tipoDoc = New d_TipoDocumenntacion

            Dim dt As New Data.DataTable

            dt = md_tipoDoc.ListarTipoDocumentacion("GEN", 0)

            Call md_Funciones.CargarCombo(Me.ddlCodigo_tid, dt, "codigo_tid", "descripcion_tid", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


#End Region




   
   
    
    
    
End Class
