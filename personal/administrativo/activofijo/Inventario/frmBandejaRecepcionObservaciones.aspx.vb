
Partial Class administrativo_activofijo_Inventario_frmBandejaRecepcionObservaciones
    Inherits System.Web.UI.Page

    Dim md_Funciones As New d_Funciones

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

    Protected Sub btnListarObservaciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarObservaciones.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_obsEnc", GetType(Integer))
            dt.Columns.Add("equi_inv", GetType(String))
            dt.Columns.Add("resp_bien", GetType(String))
            dt.Columns.Add("act_fijo", GetType(String))
            dt.Columns.Add("observ", GetType(String))

            dt.Rows.Add(1, "Equipo1", "Juanito Flores", "laptop core i5", "presenta rayadura")
            dt.Rows.Add(1, "Equipo1", "Juanito Flores", "silla", "silla quebrada")
            dt.Rows.Add(1, "Equipo1", "Juanito Flores", "mouse", "malogrado")

            'obj.CerrarConexion()
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

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

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString
            'Call mt_ShowMessage("HOLA", MessageType.error)
            Select Case e.CommandName
                Case "AdjuntarEvidencia"

                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    'If Not mt_CargarFormularioRegistro(CInt(Session("frmNotificaciones-codigo_not"))) Then Exit Sub

                    Call mt_FlujoTabs("AdjuntarEvidencia")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "ListadoObservacion"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)


                Case "AdjuntarEvidencia"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Protected Sub lnkBtnSalirAdjEvid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSalirAdjEvid.Click
        Try
            Call btnListarObservaciones_Click(Nothing, Nothing)
            Call mt_FlujoTabs("ListadoObservacion")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub



End Class
