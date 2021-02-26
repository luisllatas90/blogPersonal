
Partial Class administrativo_activofijo_L_Interfaces_frmInvestActivoFijoFaltante
    Inherits System.Web.UI.Page

    Dim md_Funciones As New d_Funciones

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

    Protected Sub btnFiltrarSinResp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltrarSinResp.Click
        Try
            If cmbTipoFiltroSinResp.SelectedValue.CompareTo("SINRESP") = 0 Then
                Call mt_CargarDatosSinResponsable()

            End If

            If cmbTipoFiltroSinResp.SelectedValue.CompareTo("CONRESP") = 0 Then
                Call mt_CargarDatosConResponsable()
                Call mt_FlujoTabs("ConResponsable")
            End If

            If cmbTipoFiltroSinResp.SelectedValue.CompareTo("TODOS") = 0 Then
                Call mt_CargarDatosConResponsable()
                Call mt_FlujoTabs("ConResponsable")
            End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnFiltrarConResp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFiltrarConResp.Click
        Try
            If cmbTipoFiltroConResp.SelectedValue.CompareTo("SINRESP") = 0 Then
                Call mt_CargarDatosSinResponsable()
                Call mt_FlujoTabs("conresponsable->sinresponsable")


            End If

            If cmbTipoFiltroSinResp.SelectedValue.CompareTo("CONRESP") = 0 Then
                Call mt_CargarDatosConResponsable()
            End If

            If cmbTipoFiltroSinResp.SelectedValue.CompareTo("TODOS") = 0 Then
                Call mt_CargarDatosConResponsable()
                Call mt_FlujoTabs("ConResponsable")
            End If


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Private Sub mt_CargarDatosSinResponsable()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_detInvest", GetType(Integer))
            dt.Columns.Add("desc_actFijo", GetType(String))
            dt.Columns.Add("resp_bien", GetType(String))
            dt.Columns.Add("comis_Inves", GetType(String))
            dt.Columns.Add("fech_progInves", GetType(String))

            dt.Rows.Add(1, "Laptop core i5", "Juanito Flores", "Equipo 1", "22/02/2021")
            dt.Rows.Add(2, "Silla", "Luis Llatas", "Equipo 1", "22/02/2021")
            dt.Rows.Add(3, "ventilador", "Jorge Perez", "Equipo 2", "23/02/2021")

            'obj.CerrarConexion()
            Me.grwSinResp.DataSource = dt
            Me.grwSinResp.DataBind()
            Call md_Funciones.AgregarHearders(Me.grwSinResp)
            Call mt_UpdatePanel("udpSinResp")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarDatosConResponsable()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_detInvest1", GetType(Integer))
            dt.Columns.Add("desc_actFijo1", GetType(String))
            dt.Columns.Add("resp_bien1", GetType(String))
            dt.Columns.Add("comis_Inves1", GetType(String))
            dt.Columns.Add("fech_progInves1", GetType(String))

            dt.Rows.Add(1, "Laptop core i5", "Juanito Flores", "Equipo 1", "22/02/2021")
            dt.Rows.Add(2, "Silla", "Luis Llatas", "Equipo 1", "22/02/2021")
            dt.Rows.Add(3, "ventilador", "Jorge Perez", "Equipo 2", "23/02/2021")

            'obj.CerrarConexion()
            Me.grwConResp.DataSource = dt
            Me.grwConResp.DataBind()
            Call md_Funciones.AgregarHearders(Me.grwConResp)
            Call mt_UpdatePanel("udpConResp")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "udpSinResp"
                    Me.udpSinResp.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaSinRespUpdate", "udpListaSinRespUpdate();", True)

                Case "udpConResp"
                    Me.udpConResp.Update()
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaConRespUpdate", "", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab

                Case "SinResponsable"
                    Me.udpScripts.Update()
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "ConResponsable"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('conresp-tab');", True)

                Case "DetalleConResp"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('conresp->detallConResp');", True)

                Case "detalle->conresponsable"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('detalle->conresponsable');", True)

                Case "conresponsable->sinresponsable"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('conresponsable->sinresponsable');", True)

                Case "SinRespo->AdjFichBaja"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('SinRespo->AdjFichBaja');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwConResp_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwConResp.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString
            'Call mt_ShowMessage("HOLA", MessageType.error)
            Select Case e.CommandName
                Case "detalleConResp"
                    mt_FlujoTabs("DetalleConResp")


            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwSinResp_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwSinResp.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString
            'Call mt_ShowMessage("HOLA", MessageType.error)
            Select Case e.CommandName
                Case "AdjFormBaja"
                    mt_FlujoTabs("SinRespo->AdjFichBaja")


            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirDetalle.Click
        Try
            Call mt_CargarDatosConResponsable()
            Call mt_FlujoTabs("detalle->conresponsable")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

End Class
