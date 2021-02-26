
Partial Class administrativo_activofijo_L_Interfaces_frmRegistraActivoFijoFaltante
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
            dt.Columns.Add("respons", GetType(String))

            dt.Rows.Add(1, "Laptop core i5", "Juanito Flores", "Equipo 1", "22/02/2021", "NO")
            dt.Rows.Add(2, "Silla", "Luis Llatas", "Equipo 1", "22/02/2021", "NO")
            dt.Rows.Add(3, "ventilador", "Jorge Perez", "Equipo 2", "23/02/2021", "NO")

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
            dt.Columns.Add("respons1", GetType(String))

            dt.Rows.Add(1, "Laptop core i5", "Juanito Flores", "Equipo 1", "22/02/2021", "SI")
            dt.Rows.Add(2, "Silla", "Luis Llatas", "Equipo 1", "22/02/2021", "SI")
            dt.Rows.Add(3, "ventilador", "Jorge Perez", "Equipo 2", "23/02/2021", "SI")

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
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaConRespUpdate", "", True)


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
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "ConResponsable"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('conResp-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


End Class
