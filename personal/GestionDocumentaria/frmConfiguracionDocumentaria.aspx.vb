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
    Dim me_documento As e_Documento
    Dim md_documento As d_Documento
    Dim me_area As e_Area
    Dim me_configuraFirma As e_Firma
    Dim md_configuraFirma As d_configuraFirma


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
            Response.Redirect("../../sinacceso.html")
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
            Call mt_CargarComboDocumento()

            Me.lbAgregar.Enabled = True
            Me.lbModificar.Enabled = False
            Me.ddlEstado_cda.Enabled = False

            Me.imgIndicacion.ImageUrl = "../GestionDocumentaria/img/indicacion.png"
        Else
            mt_CargarConfiguracionDocumentos()
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
                Call mt_ShowMessage("Seleccione tipo de documento", MessageType.warning)
                Me.ddlCodigo_tid.Focus()
                Exit Sub
            ElseIf (Me.ddlDocumento.SelectedValue = "") Then
                Call mt_ShowMessage("Seleccione documento ", MessageType.warning)
                Me.ddlDocumento.Focus()
                Exit Sub
               
            End If
           
            With me_confDocArea
                .codigo_are = Me.ddlCodigo_are.SelectedValue
                .codigo_cda = codigo_cda
                .codigo_tid = Me.ddlCodigo_tid.SelectedValue
                .codigo_tfu = Me.ddlCodigo_tfu.SelectedValue
                .estado_cda = Me.ddlEstado_cda.SelectedValue
                .codigo_doc = Me.ddlDocumento.SelectedValue
                .indFirma = Me.chkIndFirma.Checked
                .usuarioReg = codigo_usu
            End With


            codigo_cda = md_confDocArea.RegistrarActualizarConfigurarDocumentoArea(me_confDocArea)
            Call mt_CargarConfiguracionDocumentos()
            Me.udpListadoConf.Update()

            Me.mt_limpiar()
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

                    dt = md_confDocArea.ListarConfigurarDocumentoArea("COD", codigo_cda, "", 0, 0, 0, 0)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.ddlCodigo_are.SelectedValue = .Item("codigo_are")
                            Me.ddlCodigo_tfu.SelectedValue = .Item("codigo_tfu")
                            Me.ddlEstado_cda.SelectedValue = .Item("estado_cda")
                            Me.ddlCodigo_tid.SelectedValue = .Item("codigo_tid")
                            Me.ddlDocumento.SelectedValue = .Item("codigo_doc")
                            Me.txtCodigo_cda.Text = .Item("codigo_cda")
                            Me.chkIndFirma.Checked = .Item("indFirma")
                        End With
                    End If
                    Me.udpFiltros.Update()

                Case "editFirmas"

                    Me.txtCodigo_cda_modFma.Text = codigo_cda
                    dt = md_confDocArea.ListarConfigurarDocumentoArea("COD", codigo_cda, "", 0, 0, 0, 0)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.txtDescripcion_doc_mod.Text = .Item("descripcion_doc")
                            Me.txtCodigo_tfu_modFma.Text = .Item("codigo_tfu")
                        End With
                    End If
                    'Call mt_CargarComboUsuarioFirma(Me.txtCodigo_tfu_modFma.Text)
                    Call mt_CargarComboFuncionFirma()
                    Call mt_CargarComboAlcance()
                    Call mt_CargarGrillaConfiguraFirma()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalFirmas", "openModalFirmas();", True)
                    Me.udpFirmas.Update()

            End Select
            

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub

    Protected Sub gvListaConfiguraDocumnetos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaConfiguraDocumnetos.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.Header Then
                e.Row.TableSection = TableRowSection.TableHeader
            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lsDataKeyValue As String = gvListaConfiguraDocumnetos.DataKeys(e.Row.RowIndex).Values("indFirma").ToString()
                Dim lnk As LinkButton = (CType(e.Row.Cells(9).FindControl("btnFirmar"), LinkButton))

                If lsDataKeyValue Then
                    lnk.Visible = True
                Else
                    lnk.Visible = False
                End If


            End If
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

            With me_confDocArea
                .codigo_are = Me.ddlCodigo_are.SelectedValue
                .codigo_cda = Me.txtCodigo_cda.Text
                .codigo_tid = Me.ddlCodigo_tid.SelectedValue
                .codigo_tfu = Me.ddlCodigo_tfu.SelectedValue
                .estado_cda = Me.ddlEstado_cda.SelectedValue
                .codigo_doc = Me.ddlDocumento.SelectedValue
                .usuarioReg = codigo_usu
                .indFirma = Me.chkIndFirma.Checked
            End With

            md_confDocArea.RegistrarActualizarConfigurarDocumentoArea(me_confDocArea)

            Call mt_CargarConfiguracionDocumentos()
            Call mt_limpiar()

            Call mt_ShowMessage("Registro Actualizado", MessageType.success)

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

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalTipoDoc", "openModalTipoDoc();", True)

        Catch ex As Exception
            Call mt_ShowMessage("No se encontraron registros", MessageType.warning)
        End Try

    End Sub

    Protected Sub lbNuevoDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevoDoc.Click
        Try
            Call mt_CargarDocumento()
            Me.udpModalDocumento.Update()


            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalDocumento", "openModalDocumento();", True)
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
                    dt = md_DocArea.ListarArea("COD", codigo_are, 0)
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
                Call mt_ShowMessage("Se registró el Tipo de documento con éxito", MessageType.success)
            Else
                Call mt_ShowMessage("Se actualizó el Tipo de documento con éxito", MessageType.success)
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbGuardarDocumento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarDocumento.Click
        Try
            md_documento = New d_Documento
            me_documento = New e_Documento

            Dim codigo_doc As Integer = 0
            'Me.txtCodigo_are.Text = codigo_are

            '***** validaciones
            If Me.txtDescripcion_doc.Text = "" Then
                Call mt_ShowMessage("Ingrese la descripción del documento", MessageType.warning)
                Me.txtDescripcion_are.Focus()
                Exit Sub
            End If
            If Me.txtAbreviatura_doc.Text = "" Then
                Call mt_ShowMessage("Ingrese la abreviatura del documento", MessageType.warning)
                Me.txtAbreviatura_doc.Focus()
                Exit Sub
            End If
            If Len(Me.txtAbreviatura_doc.Text) > 4 Then
                Call mt_ShowMessage("La abreviatura del documento debe contener 4 dígitos", MessageType.warning)
                Me.txtAbreviatura_doc.Focus()
                Exit Sub
            End If
            '*** fin validaciones

            With me_documento
                If Me.txtcodigo_doc.Text = "" Then
                    .codigo_doc = "0"
                Else
                    .codigo_doc = Me.txtCodigo_doc.Text
                End If
                .descripcion_doc = Me.txtDescripcion_doc.Text
                .abreviatura_doc = Me.txtAbreviatura_doc.Text
                .usuarioReg = codigo_usu
            End With

            codigo_doc = md_documento.RegistrarActualizarDocumento(me_documento)

            Call mt_CargarComboDocumento()
            Me.udpFiltros.Update()

            Call mt_CargarDocumento()

            Call mt_limpiarModDocumento()

            If Me.txt_codigo_tid.Text = "0" Then
                Call mt_ShowMessage("Se registró el documento con éxito", MessageType.success)
            Else
                Call mt_ShowMessage("Se actualizó el documento con éxito", MessageType.success)
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

    Protected Sub gvListaDoc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaDoc.RowCommand
        Try
            Dim codigo_doc As Integer = 0
            Dim dt As New Data.DataTable
            md_documento = New d_Documento

            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_doc = Me.gvListaDoc.DataKeys(index).Values("codigo_doc")
            Me.txtCodigo_doc.Text = codigo_doc

            Select Case e.CommandName
                Case "editDocumento"
                    dt = md_documento.ListarDocumento("COD", codigo_doc, 0)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            Me.txtDescripcion_doc.Text = .Item("descripcion_doc")
                            Me.txtAbreviatura_doc.Text = .Item("abreviatura_doc")
                        End With
                    End If
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbAddFirma_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddFirma.Click
        Try
            Dim codigo_fma As Integer = 0
            me_configuraFirma = New e_Firma
            md_configuraFirma = New d_configuraFirma
            If Me.ddlCodigo_tfu_modFma.SelectedValue = "" Then
                Call mt_ShowMessage("¡ Debe Seleccionar un tipo de funcion !", MessageType.warning)
                'Me.ddlUsuarioFirma.Focus()
                Exit Sub
            End If

            If Me.ddlAlcance_modFma.SelectedValue = "" Then
                Call mt_ShowMessage("¡ Complete los datos!", MessageType.warning)
                'Me.ddlUsuarioFirma.Focus()
                Exit Sub
            End If

            If mt_validaExisteConfiguraFirma(Me.txtCodigo_cda_modFma.Text, Me.ddlCodigo_tfu_modFma.SelectedValue, Me.ddlAlcance_modFma.SelectedValue) Then
                Call mt_ShowMessage("¡ la configuración de la firma para este documento ya existe !", MessageType.warning)
                Exit Sub
            End If

            With me_configuraFirma
                .codigo_fma = IIf(Me.txtCodigo_fma.Text = "", "0", Me.txtCodigo_fma.Text)
                .codigo_cda = Me.txtCodigo_cda_modFma.Text
                .codigo_per = 0
                .codigo_tfu = Me.ddlCodigo_tfu_modFma.SelectedValue
                .cod_alcance = Me.ddlAlcance_modFma.SelectedValue
                .estado_fma = "1"
                .orden_fma = IIf(Me.txtOrden_fma.Text = "", "0", Me.txtOrden_fma.Text)
                .fechaReg = Now()
                .usarioReg = codigo_usu
            End With

            codigo_fma = md_configuraFirma.RegistraActualizarConfiguraFirma(me_configuraFirma)
            Call mt_limpiarModFirma()
            Call mt_CargarGrillaConfiguraFirma()
            Me.udpFirmas.Update()
            Call mt_ShowMessage("¡ Se configuró la Firma !", MessageType.success)

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

            dt = md_DocArea.ListarArea("GEN", 0, 0)

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

    Private Sub mt_CargarConfiguracionDocumentos()
        md_confDocArea = New d_ConfigurarDocumentoArea
        Dim dt As New Data.DataTable

        Try

            dt = md_confDocArea.ListarConfigurarDocumentoArea("GEN", 0, "", 0, 0, 0, 0)

            If dt.Rows.Count > 0 Then
                Me.gvListaConfiguraDocumnetos.DataSource = dt
                Me.gvListaConfiguraDocumnetos.DataBind()
                'Para ocultar las columnas y no pierda el valor
                'Call mt_ocultarColCursosAmbientes()

            Else
                Call mt_ShowMessage("No se encontraron documentos configurados", MessageType.warning)
            End If

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            Me.udpListadoConf.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try


    End Sub

    Private Sub mt_CargarAreas()
        Try
            Dim codigo_are As Integer = 0
            md_DocArea = New d_DocArea

            Dim dt As New Data.DataTable
            dt = md_DocArea.ListarArea("GEN", 0, 0)

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

    Private Sub mt_CargarDocumento()
        Try
            Dim codigo_cod As Integer = 0
            md_documento = New d_Documento

            Dim dt As New Data.DataTable

            dt = md_documento.ListarDocumento("GEN", 0, 0)

            If dt.Rows.Count > 0 Then
                Me.gvListaDoc.DataSource = dt
                Me.gvListaDoc.DataBind()
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
        Me.ddlDocumento.SelectedValue = ""
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

    Private Sub mt_limpiarModDocumento()
        Me.txtCodigo_doc.Text = String.Empty
        Me.txtDescripcion_doc.Text = String.Empty
        Me.txtAbreviatura_doc.Text = String.Empty
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

    Private Sub mt_CargarComboDocumento()
        Try
            md_Funciones = New d_Funciones
            md_documento = New d_Documento

            Dim dt As New Data.DataTable

            dt = md_documento.ListarDocumento("GEN", 0, 0)

            Call md_Funciones.CargarCombo(Me.ddlDocumento, dt, "codigo_doc", "descripcion_doc", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    'Private Sub mt_CargarComboUsuarioFirma(ByVal codigo_tfu As Integer)
    '    Try
    '        md_Funciones = New d_Funciones
    '        md_confDocArea = New d_ConfigurarDocumentoArea

    '        Dim dt As New Data.DataTable

    '        dt = md_confDocArea.ListarUsuarioFirma(codigo_tfu)

    '        Call md_Funciones.CargarCombo(Me.ddlUsuarioFirma, dt, "codigo_per", "NombreUsuario", True, "[-- SELECCIONE --]", "")

    '    Catch ex As Exception
    '        Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
    '    End Try
    'End Sub

    Private Sub mt_limpiarModFirma()
        Me.txtCodigo_fma.Text = String.Empty
        'Me.ddlUsuarioFirma.SelectedValue = ""
        Me.ddlCodigo_tfu_modFma.SelectedValue = ""
        Me.ddlAlcance_modFma.SelectedValue = ""
    End Sub

    Private Function mt_validaExisteConfiguraFirma(ByVal codigo_cda As Integer, ByVal codigo_tfu As Integer, ByVal cod_Alcance As String) As Boolean
        Try
            md_configuraFirma = New d_configuraFirma
            Dim dt As New Data.DataTable
            dt = md_configuraFirma.ListarConfiguraFirma("EXS", 0, codigo_cda, codigo_tfu, 0, 0, cod_Alcance)
            If dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_CargarGrillaConfiguraFirma()
        Try
            md_configuraFirma = New d_configuraFirma

            Dim dt As New Data.DataTable

            dt = md_configuraFirma.ListarConfiguraFirma("CDA", 0, CInt(Me.txtCodigo_cda_modFma.Text), 0, 0, 0, "")

            If dt.Rows.Count > 0 Then
                Me.gvConfiguraFirma.DataSource = dt
                Me.gvConfiguraFirma.DataBind()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboFuncionFirma()
        Try
            md_Funciones = New d_Funciones
            md_confDocArea = New d_ConfigurarDocumentoArea

            Dim dt As New Data.DataTable

            dt = md_confDocArea.ListarTipoFuncion("GEN", 0)

            Call md_Funciones.CargarCombo(Me.ddlCodigo_tfu_modFma, dt, "codigo_tfu", "descripcion_tfu", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboAlcance()
        '***** Tio de Reporte
        Me.ddlAlcance_modFma.Items.Clear()
        'Me.ddlTipoReporte.Items.Add("[--SELECCIONE--]")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE MATRICULA")
        'Me.ddlTipoReporte.Items.Add("CONSTANCIA DE NOTAS")

        Me.ddlAlcance_modFma.Items.Add(New System.Web.UI.WebControls.ListItem("[--SELECCIONE--]", ""))
        Me.ddlAlcance_modFma.Items.Add(New System.Web.UI.WebControls.ListItem("ADMINISTRATIVO", "A"))
        Me.ddlAlcance_modFma.Items.Add(New System.Web.UI.WebControls.ListItem("DEPARTAMENTO ACADEMICO", "D"))
        Me.ddlAlcance_modFma.Items.Add(New System.Web.UI.WebControls.ListItem("ESCUELA", "E"))
        Me.ddlAlcance_modFma.Items.Add(New System.Web.UI.WebControls.ListItem("FACULTAD", "F"))

        'Me.ddlTipoReporte.SelectedIndex = tipoPrint
    End Sub

#End Region














End Class
