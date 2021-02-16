
Partial Class GestionEgresado_GradosYTitulos_frmMantMiembrosConsejo
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
                Me.ddlConsejo.SelectedValue = "7" 'para iniciar el formulario
                Call mt_listarPersonalConsejo("CAD") 'para iniciar el formulario
                mt_CargarComboFacultad()
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Me.divPersonalActual.Visible = False
            Me.divCmbCargoActual.Visible = True
            Me.txtCodigoCjf.Text = "0" ' para insertar
            Me.txtEstadoCjf.Text = "A"
            Call mt_CargaComboCargoConsejoFacultad()
            Call mt_CargarComboPersonalConsejo()
            'Call mt_CargarComboCargoPersonalConsejo()

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalMiembro", "openModalMiembro();", True)
            Me.udpModalMiembro.Update()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
      
    End Sub
    Protected Sub ddlConsejo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlConsejo.SelectedIndexChanged
        Try
            Me.mt_limpiarGrilla()
            If Me.ddlConsejo.SelectedValue = "8" Then ''--- Consejo Facultad
                Call mt_activaComboFacultad(True)
            ElseIf ddlConsejo.SelectedValue = "7" Then
                Call mt_activaComboFacultad(False)
                Call mt_listarPersonalConsejo("CAD")
                Me.updFiltros.Update()
            End If

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
       
    End Sub
    Protected Sub ddlFacultad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFacultad.SelectedIndexChanged
        Call mt_listarPersonalConsejo("CFC")
    End Sub
    Protected Sub ddlPersResp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersResp.SelectedIndexChanged
        If Me.ddlPersResp.SelectedValue <> "" Then
            Call mt_CargarComboCargoPersonalConsejo()
        End If
    End Sub
    Protected Sub lbActualizarMiembro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbActualizarMiembro.Click
        Try
            Dim dt As New Data.DataTable
            Dim md_Consejo As New d_ConsejoFacultad
            Dim me_consejo As New e_ConsejoFacultad
            With me_consejo
                .codigo_con = Me.ddlConsejo.SelectedValue
                .codigo_pcc = Me.ddlPersResp.SelectedValue
                .estado_cjf = Me.txtEstadoCjf.Text
                .cargo_cjf = Me.ddlCargoCF.SelectedValue
                .codigo_cgo = Me.ddlCargoPers.SelectedValue
                .codigo_cjf = Me.txtCodigoCjf.Text
                If Me.ddlConsejo.SelectedValue = "8" Then 'consejo facultad
                    .codigo_fac = Me.ddlFacultad.SelectedValue
                ElseIf Me.ddlConsejo.SelectedValue = "7" Then ' consejo administrativo
                    .codigo_fac = 0
                End If

                .usuario = codigo_usu
            End With
            If mt_validarModalMiembro() Then
                dt = md_Consejo.ActualizaConsejo(me_consejo)
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("Respuesta") = "1" Then
                        Call mt_ShowMessage("Se actualizó el miembro del consejo", MessageType.success)
                        Call mt_limpiarControles()
                        If Me.ddlConsejo.SelectedValue = "8" Then '' consejo facultad
                            Call mt_listarPersonalConsejo("CFC")
                        ElseIf Me.ddlConsejo.SelectedValue = "7" Then '' consejo administrativo
                            Call mt_listarPersonalConsejo("CAD")
                        End If
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalMiembro", "closeModalMiembro();", True)
                        Me.updFiltros.Update()
                    Else
                        Call mt_ShowMessage("No se pudo actualizar el miembro el consejo", MessageType.error)
                    End If
                End If

            End If
           


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub gvListaConsejo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaConsejo.RowCommand
        'Call mt_ShowMessage("aqui", MessageType.error)
        Dim codigo_cjf As String
        Dim responsable_cco As String
        Dim cargo_cjf As String
        Dim cod_cargo_ctf As String
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Call mt_CargarComboPersonalConsejo()
            Call mt_CargaComboCargoConsejoFacultad()
            Call mt_CargaComboCargoConsejoFacultad()

            codigo_cjf = Me.gvListaConsejo.DataKeys(index).Values("codigo_cjf")
            responsable_cco = Me.gvListaConsejo.DataKeys(index).Values("responsable_cco")
            cargo_cjf = Me.gvListaConsejo.DataKeys(index).Values("cargo_cjf")
            cod_cargo_ctf = Me.gvListaConsejo.DataKeys(index).Values("cod_cargo_cjf")

            Select Case e.CommandName
                Case "Editar"
                    Me.ddlCargoPers.Items.Clear()
                    'Me.DropDownList.Items.Clear()
                    Me.divPersonalActual.Visible = True
                    Me.divCmbCargoActual.Visible = False
                    Me.txtCodigoCjf.Text = codigo_cjf
                    Me.ddlCargoCF.SelectedValue = cod_cargo_ctf
                    Me.txtEstadoCjf.Text = "C"
                    Me.txtRespActual.Text = responsable_cco
                    Me.txtCargoActual.Text = cargo_cjf
                    Me.udpModalMiembro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalMiembro", "openModalMiembro();", True)
                    Me.udpModalMiembro.Update()
                Case "Eliminar"
                    Dim dt As New Data.DataTable
                    Dim md_Consejo As New d_ConsejoFacultad
                    Dim me_consejo As New e_ConsejoFacultad
                    With me_consejo
                        .codigo_con = "0"
                        .codigo_pcc = "0"
                        .estado_cjf = "I"
                        .cargo_cjf = "0"
                        .codigo_cgo = "0"
                        .codigo_cjf = codigo_cjf
                        .codigo_fac = "0"
                        .usuario = codigo_usu
                    End With
                    dt = md_Consejo.ActualizaConsejo(me_consejo)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item("Respuesta") = "1" Then
                            Call mt_ShowMessage("Se deshabilitó el miembro del consejo", MessageType.success)
                            If Me.ddlConsejo.SelectedValue = "8" Then '' consejo facultad
                                Call mt_listarPersonalConsejo("CFC")
                            ElseIf Me.ddlConsejo.SelectedValue = "7" Then '' consejo administrativo
                                Call mt_listarPersonalConsejo("CAD")
                            End If
                            Me.updFiltros.Update()
                        Else
                            Call mt_ShowMessage("No se pudo deshabilitar el miembro el consejo", MessageType.error)
                        End If
                    End If
            End Select
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

            Call md_Funciones.CargarCombo(Me.ddlConsejo, dt, "codigo_con", "nombre_con", True, "[-- SELECCIONE --]", "")
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
                .operacion = "CFA"
            End With

            Dim dt As New Data.DataTable

            dt = md_Facultad.ListarFacultad(me_Facultad)

            Call md_Funciones.CargarCombo(Me.ddlFacultad, dt, "codigo_fac", "nombre_fac", True, "[-- SELECCIONE --]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboPersonalConsejo()
        Try
            Dim md_Funciones As New d_Funciones
            Dim md_Consejo As New d_ConsejoFacultad
            Dim me_consejo As New e_ConsejoFacultad

            With me_consejo
                If Me.ddlConsejo.SelectedValue = "8" Then '' consejo de facultad - consejo nuevo
                    .operacion = "G"
                    '.codigo_con = "1"
                    .codigo_fac = Me.ddlFacultad.SelectedValue
                ElseIf Me.ddlConsejo.SelectedValue = "7" Then 'consejoa administrativo y ademas 6 rectorado
                    .operacion = "T"
                    .codigo_fac = 0
                End If
                .codigo_con = Me.ddlConsejo.SelectedValue

            End With

            Dim dt As New Data.DataTable

            dt = md_Consejo.ListarPersonalConsejo(me_consejo)

            Call md_Funciones.CargarCombo(Me.ddlPersResp, dt, "codigo_pcc", "responsable_cco", True, "[--SELECCIONE--]", "")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboCargoPersonalConsejo()
        Try
            Dim md_Funciones As New d_Funciones
            Dim md_Consejo As New d_ConsejoFacultad
            Dim me_personal As New e_Personal

            With me_personal
                .codigo_per = Me.ddlPersResp.SelectedValue
            End With

            Dim dt As New Data.DataTable

            dt = md_Consejo.ListarCargosPersonalConsejo(me_personal)

            Call md_Funciones.CargarCombo(Me.ddlCargoPers, dt, "codigo_cgo", "descripcion_cgo", True, "[--SELECCIONE--]", "")

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
    Private Sub mt_listarPersonalConsejo(ByVal operacion As String)
        Try
            Dim md_ConsejoFacultad As New d_ConsejoFacultad
            Dim me_ConsejoFacultad As New e_ConsejoFacultad
            With me_ConsejoFacultad
                .operacion = operacion
                If Me.ddlConsejo.SelectedValue = "8" Then '' consejo facultad
                    .codigo_fac = Me.ddlFacultad.SelectedValue
                ElseIf Me.ddlConsejo.SelectedValue = "7" Then '' consejo administrativo
                    .codigo_con = Me.ddlConsejo.SelectedValue
                End If

            End With

            Dim dt As New Data.DataTable

            dt = md_ConsejoFacultad.ListarConsejoFacultad(me_ConsejoFacultad)

            If dt.Rows.Count > 0 Then
                Me.gvListaConsejo.DataSource = dt
                Me.gvListaConsejo.DataBind()
            Else
                Call mt_ShowMessage("No se encontraron miembros del consejo", MessageType.warning)
            End If
            Me.updFiltros.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_activaComboFacultad(ByVal flat As Boolean)
        Me.lblComboFacultad.Visible = flat
        Me.ddlFacultad.Visible = flat
        If flat = False Then
            Me.ddlFacultad.SelectedValue = ""
        End If
        Me.updFiltros.Update()
    End Sub
    Private Sub mt_CargaComboCargoConsejoFacultad()
        Me.ddlCargoCF.Items.Clear()
        Me.ddlCargoCF.Items.Add(New ListItem("[--SELECCIONE--]", ""))
        Me.ddlCargoCF.Items.Add(New ListItem("MIEMBRO DEL CONSEJO DE FACULTAD", "F"))
        Me.ddlCargoCF.Items.Add(New ListItem("SECRETARIO DE FACULTAD", "T"))
        Me.ddlCargoCF.Items.Add(New ListItem("COORDINADOR DE EDUCACIÓN CONTINUA", "C"))
    End Sub
    Private Sub mt_limpiarControles()
        Call mt_CargarComboPersonalConsejo()
        Call mt_CargaComboCargoConsejoFacultad()
        Call mt_CargarComboCargoPersonalConsejo()
        Me.txtCodigoCjf.Text = String.Empty
        Me.txtRespActual.Text = String.Empty
        Me.txtCargoActual.Text = String.Empty
        Me.txtEstadoCjf.Text = String.Empty
    End Sub
    Private Function mt_validarModalMiembro() As Boolean
        If Me.ddlPersResp.SelectedValue = "" Then
            Call mt_ShowMessage("Seleccione un personal responsable", MessageType.warning)
            Return False
        End If
        If Me.ddlCargoCF.Text = "" Then
            Call mt_ShowMessage("Seleccione cargo", MessageType.warning)
            Return False
        End If
        If Me.ddlCargoPers.SelectedValue = "" Then
            Call mt_ShowMessage("Seleccione cargo del personal", MessageType.warning)
            Return False
        End If
        Return True
    End Function
    Private Sub mt_limpiarGrilla()
        Me.gvListaConsejo.DataSource = Nothing
        Me.gvListaConsejo.DataBind()
        Me.updFiltros.Update()
    End Sub
#End Region


   
    
    
    
End Class
