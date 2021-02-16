
Partial Class DisenioPuestos_frmCargarMatrizIperc
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
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarGrilla()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Protected Sub grwListaPuestos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwListaPuestos.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString

            Select Case e.CommandName
              
                Case "AddRiesgos"
                    'mt_CargarComboProfesional()
                    'mt_CargarGrillaCarreras(codigo_ofe)
                    'Me.txtCodOfMod.Text = codigo_ofe
                    Call mt_CargarGrillaRiesgos()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "OpenModalRiesgos", "OpenModalRiesgos('');", True)
                    Me.udpModalComp.Update()

                


            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
#End Region

#Region "Metodos, funciones, procedimientos"
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

            Dim Renglon1 As Data.DataRow = dt.NewRow()
            Renglon1("Puesto") = "ASISTENTE DE LABORATORIO"
            Renglon1("Estado") = "Registrado"
            dt.Rows.Add(Renglon1)


            'Dim dt As New DataTable : me_Notificacion = New e_Notificaciones

            'If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            'With me_Notificacion
            '.operacion = "LIS"
            '.tipo_not = cmbTipoFiltro.SelectedValue
            '.clasificacion_not = cmbClasificacionFiltro.SelectedValue
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            Me.grwListaPuestos.DataSource = dt
            Me.grwListaPuestos.DataBind()

            Call md_Funciones.AgregarHearders(grwListaPuestos)

            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarGrillaRiesgos()
        Try
            Dim dt As New Data.DataTable
            dt.Columns.Add("Riesgo")

            Dim Renglon As Data.DataRow = dt.NewRow()
            Renglon("Riesgo") = "Evitar un lugar visible y de fácil acceso las fichas técnicas de equipos y sustancias químicas"
            dt.Rows.Add(Renglon)


            'Dim dt As New DataTable : me_Notificacion = New e_Notificaciones

            'If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            'With me_Notificacion
            '.operacion = "LIS"
            '.tipo_not = cmbTipoFiltro.SelectedValue
            '.clasificacion_not = cmbClasificacionFiltro.SelectedValue
            'End With

            'dt = md_Notificacion.ListarNotificacion(me_Notificacion)

            Me.gvRiesgo.DataSource = dt
            Me.gvRiesgo.DataBind()

            'Call md_Funciones.AgregarHearders(gvRiesgo)

            'Call mt_UpdatePanel("Lista")

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
#End Region

   
End Class
