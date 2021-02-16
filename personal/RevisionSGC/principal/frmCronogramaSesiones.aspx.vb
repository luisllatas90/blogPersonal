
Partial Class GestionDocumentaria_frmCronogramaSesiones
    Inherits System.Web.UI.Page

#Region "variables"
    Dim codigo_tfu As Integer
    Dim codigo_usu As Integer
    Dim tipoestudio As Integer

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")
        Try
            If IsPostBack = False Then
                mt_CargarComboConsejo()
                Me.ddlConsejo.SelectedValue = "CU" 'para iniciar el formulario
                Call mt_listarSesionesConsejo(Me.ddlConsejo.SelectedValue) 'para iniciar el formulario
                mt_CargarComboFacultad()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            If Me.ddlConsejo.SelectedValue <> "" Then
                If Me.ddlConsejo.SelectedValue = "CF" And Me.ddlFacultad.SelectedValue = "" Then
                    Call mt_ShowMessage("!.. Seleccione una facultad", MessageType.error)
                    Me.updFiltros.Update()
                    Exit Sub
                End If
                Me.divSesionActual.Visible = False
                'Me.divCmbCargoActual.Visible = True
                Me.txtCodigoScu.Text = "0" ' para insertar
                Call mt_CargaComboTipoSesion()
                Call mt_limpiarControles()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalSesiom", "openModalSesion();", True)
                Me.udpModalSesion.Update()
            Else
                Call mt_ShowMessage("!.. Seleccione un consejo", MessageType.error)
                Me.updFiltros.Update()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
  
    Protected Sub ddlConsejo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConsejo.SelectedIndexChanged
        Try
            Me.mt_limpiarGrilla()
            If Me.ddlConsejo.SelectedValue = "CF" Then ''--- Consejo Facultad
                Call mt_activaComboFacultad(True)
            ElseIf ddlConsejo.SelectedValue = "CU" Then
                Call mt_activaComboFacultad(False)
                Call mt_listarSesionesConsejo(Me.ddlConsejo.SelectedValue)
                Me.updFiltros.Update()
            ElseIf ddlConsejo.SelectedValue = "CA" Then
                Call mt_activaComboFacultad(False)
                Call mt_listarSesionesConsejo(Me.ddlConsejo.SelectedValue)
                Me.updFiltros.Update()
            Else
                Call mt_activaComboFacultad(False)
                Call mt_limpiarGrilla()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        'Call mt_ShowMessage(Me.ddlConsejo.SelectedValue.ToString, MessageType.error)
        Call mt_listarSesionesConsejo(Me.ddlConsejo.SelectedValue)

    End Sub

    Protected Sub gvListaSesiones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaSesiones.RowCommand
        Dim codigo_scu As String
        Dim descripcion_scu As String
        Dim fecha_scu As String
        Dim cod_tipoSesion As String

        Call mt_limpiarControles()
        Call mt_CargaComboTipoSesion()

        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_scu = Me.gvListaSesiones.DataKeys(index).Values("codigo_scu")
            descripcion_scu = Me.gvListaSesiones.DataKeys(index).Values("descripcion_scu")
            fecha_scu = Me.gvListaSesiones.DataKeys(index).Values("fecha_scu")
            cod_tipoSesion = Me.gvListaSesiones.DataKeys(index).Values("cod_tipoSesion")

            Select Case e.CommandName
                Case "Editar"
                    Me.txtCodigoScu.Text = codigo_scu
                    Me.divSesionActual.Visible = True
                    Me.txtSesionActual.Text = descripcion_scu
                    Me.txtFechaActual.Text = fecha_scu
                    Me.ddlTipoSesion.SelectedValue = cod_tipoSesion
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalSesion", "openModalSesion();", True)
                    Me.udpModalSesion.Update()

                Case "Eliminar"
                    Me.txtCodigoScu.Text = codigo_scu
                    Dim me_sesiones As New e_SesionConsejoUniv_GYT
                    Dim dt As New Data.DataTable
                    Dim md_sesiones As New d_SesionConsejoUniv_GYT
                    With me_sesiones
                        .codigo_scu = Me.txtCodigoScu.Text
                        .descripcion_scu = ""
                        .fecha_scu = ""
                        .estado_scu = "1"
                        .usuario_reg = Me.codigo_usu
                        .vigencia_scu = "0"
                        .abreviatura_con = ""
                        .tipo_sesion = ""
                        .codigo_fac = ""
                    End With

                    dt = md_sesiones.ActualizaSesionesConsejo(me_sesiones)

                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item("respuesta") = "1" Then
                            Call mt_ShowMessage("La sesión fué deshabilitada", MessageType.success)
                            Call mt_listarSesionesConsejo(ddlConsejo.SelectedValue)
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalSesion", "closeModalSesion();", True)
                            Me.updFiltros.Update()
                        Else
                            Call mt_ShowMessage("La sesión no se deshabilitó", MessageType.error)
                        End If
                    End If

            End Select

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)


        End Try
    End Sub

    Protected Sub gvListaSesiones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaSesiones.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub lbActualizarSesion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbActualizarSesion.Click
        Try
            If mt_validaFormulario() Then
                Dim me_sesiones As New e_SesionConsejoUniv_GYT
                Dim dt As New Data.DataTable
                Dim md_sesiones As New d_SesionConsejoUniv_GYT
                With me_sesiones
                    .codigo_scu = Me.txtCodigoScu.Text
                    .descripcion_scu = "SESION " & Format(CDate(Me.txtFecha.Text), "dd-MM-yyyy")
                    .fecha_scu = Me.txtFecha.Text
                    .estado_scu = "1"
                    .usuario_reg = Me.codigo_usu
                    .vigencia_scu = "1"
                    .abreviatura_con = Me.ddlConsejo.SelectedValue
                    .tipo_sesion = Me.ddlTipoSesion.SelectedValue
                    If Me.ddlConsejo.SelectedValue = "CF" Then
                        .codigo_fac = Me.ddlFacultad.SelectedValue
                    ElseIf Me.ddlConsejo.SelectedValue = "CU" Then
                        .codigo_fac = ""
                    End If
                End With

                dt = md_sesiones.ActualizaSesionesConsejo(me_sesiones)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("respuesta") = "1" Then
                        Call mt_ShowMessage("La sesión se registró con éxito", MessageType.success)
                        Call mt_limpiarControles()
                        Call mt_listarSesionesConsejo(ddlConsejo.SelectedValue)
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalSesion", "closeModalSesion();", True)
                        Me.updFiltros.Update()
                    Else
                        Call mt_ShowMessage("!.. La sesión no se ha registrado", MessageType.error)
                    End If


                End If


            End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "procedimientos y funciones"
   

    Private Sub mt_CargarComboConsejo()
        Try
            Dim md_Funciones As New d_Funciones
            Dim md_Consejo As New d_Consejo
            Dim me_Consejo As New e_Consejo

            With me_Consejo
                .operacion = "GYT"
            End With

            Dim dt As New Data.DataTable

            dt = md_Consejo.ListarConsejos(me_Consejo)

            Call md_Funciones.CargarCombo(Me.ddlConsejo, dt, "abreviatura_con", "nombre_con", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboFacultad()
        Try
            Dim md_Funciones As New d_Funciones
            Dim md_Facultad As New d_Facultad
            Dim me_Facultad As New e_Facultad

            With me_Facultad
                .operacion = "GEN"
            End With

            Dim dt As New Data.DataTable

            dt = md_Facultad.ListarFacultad(me_Facultad)

            Call md_Funciones.CargarCombo(Me.ddlFacultad, dt, "codigo_fac", "nombre_fac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_listarSesionesConsejo(ByVal abreviatura_con As String)
        Try
            Dim md_SesionesConsejo As New d_SesionConsejoUniv_GYT
            Dim me_SesionesConsejo As New e_SesionConsejoUniv_GYT
            If Me.ddlConsejo.SelectedValue = "CU" Or Me.ddlConsejo.SelectedValue = "CA" Then '-- consejo universitario / consejo administrativo
                With me_SesionesConsejo
                    .operacion = "CON"
                    .abreviatura_con = abreviatura_con
                End With
            ElseIf Me.ddlConsejo.SelectedValue = "CF" Then '-- consejo facultad
                With me_SesionesConsejo
                    .operacion = "CFC"
                    .abreviatura_con = abreviatura_con
                    .codigo_fac = Me.ddlFacultad.SelectedValue
                End With

            End If

            Dim dt As New Data.DataTable

            dt = md_SesionesConsejo.ListarSesionesConsejo(me_SesionesConsejo)

            If dt.Rows.Count > 0 Then
                Me.gvListaSesiones.DataSource = dt
                Me.gvListaSesiones.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)
            Else
                Call mt_ShowMessage("No se encontraron sesiones de consejo", MessageType.warning)
                Call mt_limpiarGrilla()
            End If
            Me.updFiltros.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_limpiarGrilla()
        Me.gvListaSesiones.DataSource = Nothing
        Me.gvListaSesiones.DataBind()
        Me.updFiltros.Update()
    End Sub

    Private Sub mt_activaComboFacultad(ByVal flat As Boolean)
        Me.lblComboFacultad.Visible = flat
        Me.ddlFacultad.Visible = flat
        If flat = False Then
            Me.ddlFacultad.SelectedValue = ""
        End If
        Me.updFiltros.Update()
    End Sub

    Private Sub mt_CargaComboTipoSesion()
        Me.ddlTipoSesion.Items.Clear()
        Me.ddlTipoSesion.Items.Add(New ListItem("[--SELECCIONE--]", ""))
        Me.ddlTipoSesion.Items.Add(New ListItem("ORDINARIA", "O"))
        Me.ddlTipoSesion.Items.Add(New ListItem("EXTRAORDINARIA", "E"))
    End Sub

    Private Sub mt_limpiarControles()
        'Call mt_CargaComboTipoSesion()
        Me.txtCodigoScu.Text = String.Empty
        Me.txtFecha.Text = String.Empty
        Me.txtSesionActual.Text = String.Empty
        Me.txtFechaActual.Text = String.Empty
    End Sub

    Private Function mt_validaFormulario() As Boolean
        If Me.txtFecha.Text = "" Then
            Call mt_ShowMessage("Debe ingresar una fecha valida", MessageType.warning)
            Return False
        ElseIf Me.ddlTipoSesion.SelectedValue = "" Then
            Call mt_ShowMessage("Seleccione un tipo de sesión", MessageType.warning)
            Return False
        End If
        Return True
    End Function

#End Region

    
    
    
End Class
