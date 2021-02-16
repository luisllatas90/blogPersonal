
Partial Class DisenioPuestos_frmPuestoTrabajo
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim codigo_tfu As Integer = 0
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
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                'Call mt_LimpiarVariablesSession()
                'Call mt_CargarComboClasificacion()
                'Call mt_CargarComboProfile()
                'Call mt_CargarGrilla()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarGrilla()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmNotificaciones-codigo_not") = 0

            'Call mt_LimpiarControles("Registro")

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString

            Select Case e.CommandName
                Case "Editar"
                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    'If Not mt_CargarFormularioRegistro(CInt(Session("frmNotificaciones-codigo_not"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_FlujoTabs("Registro")

                Case "Eliminar"
                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    'If Not mt_EliminarNotificacion(CInt(Session("frmNotificaciones-codigo_not"))) Then Exit Sub

                    Call btnListar_Click(Nothing, Nothing)

                Case "AddComp"
                    'mt_CargarComboProfesional()
                    'mt_CargarGrillaCarreras(codigo_ofe)
                    'Me.txtCodOfMod.Text = codigo_ofe
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModalComp", "OpenModalComp('');", True)
                    Me.udpModalComp.Update()

                Case "AddFuncioPt"
                    'mt_CargarComboProfesional()
                    'mt_CargarGrillaCarreras(codigo_ofe)
                    'Me.txtCodOfMod.Text = codigo_ofe
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModalTF", "OpenModalTF('');", True)
                    Me.udpModalTF.Update()

                Case "btnAddFuncPt"
                    Call mt_CargarGrillaFuncionesPt()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "modalAddFuncionPt", "modalAddFuncionPt('');", True)
                    Me.udpFuncionesPt.Update()

            End Select
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

    Private Sub mt_CargarGrilla()
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("Puesto")
            dt.Columns.Add("Estado")
            
            Dim Renglon As Data.DataRow = dt.NewRow()
            Renglon("Puesto") = "DIRECTOR DE ESCUELA"
            Renglon("Estado") = "Registrado"
            dt.Rows.Add(Renglon)

            'Dim dt As New DataTable : me_Notificacion = New e_Notificaciones

            'If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            'With me_Notificacion
            '.operacion = "LIS"
            '.tipo_not = cmbTipoFiltro.SelectedValue
            '.clasificacion_not = cmbClasificacionFiltro.SelectedValue
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarGrillaFuncionesPt()
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("funcion")

            Dim Renglon As Data.DataRow = dt.NewRow()
            Renglon("funcion") = "Participar en la elaboración  de los presupuestos de cada laboratorio."
            dt.Rows.Add(Renglon)

            Dim Renglon1 As Data.DataRow = dt.NewRow()
            Renglon1("funcion") = "Participar en la elaboración  del Plan Anual de Desechos y el Plan Anual de Mantenimiento de los laboratorios y otros planes o normativas según se requiera."
            dt.Rows.Add(Renglon1)


            'Dim dt As New DataTable : me_Notificacion = New e_Notificaciones

            'If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            'With me_Notificacion
            '.operacion = "LIS"
            '.tipo_not = cmbTipoFiltro.SelectedValue
            '.clasificacion_not = cmbClasificacionFiltro.SelectedValue
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            Me.gvFuncionPt.DataSource = dt
            Me.gvFuncionPt.DataBind()

            'Call md_Funciones.AgregarHearders(gvRiesgo)

            'Call mt_UpdatePanel("Lista")

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


#End Region

    
    
    
    
End Class
