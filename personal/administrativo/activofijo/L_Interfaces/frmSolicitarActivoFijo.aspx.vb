
Partial Class administrativo_activofijo_l_Interfaces_frmSolicitarActivoFijo
    Inherits System.Web.UI.Page

    Dim md_Funciones As New d_Funciones

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum


    Protected Sub btnListarSolicitud_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarSolicitud.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub btnNuevaSolic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevaSolic.Click
        Try
            Call mt_CargarDatosNuevaSol()
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
            dt.Columns.Add("cod_solReq", GetType(Integer))
            dt.Columns.Add("solic", GetType(String))
            dt.Columns.Add("are_solc", GetType(String))
            dt.Columns.Add("estado", GetType(String))

            dt.Rows.Add(123, "Luis Llatas", "Tecnología de Información", "Solicitado")
            dt.Rows.Add(124, "Juan Jiménez", "Tecnología de Información", "Por validar")


            'obj.CerrarConexion()
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("ListaSolicitud")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarDatosNuevaSol()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_nuevoReg", GetType(Integer))
            dt.Columns.Add("descrip", GetType(String))
            dt.Columns.Add("cantid", GetType(Integer))

            dt.Rows.Add(1, "Laptop core i5", 2)
            dt.Rows.Add(2, "Tecnología de Información", 1)


            'obj.CerrarConexion()
            Me.gvRegistrar.DataSource = dt
            Me.gvRegistrar.DataBind()
            Call md_Funciones.AgregarHearders(gvRegistrar)
            Call mt_UpdatePanel("RegistraSolicitud")
            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarDetalle()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_nuevoReg", GetType(Integer))
            dt.Columns.Add("descrip", GetType(String))
            dt.Columns.Add("cantid", GetType(Integer))

            dt.Rows.Add(1, "Laptop core i5", 2)
            dt.Rows.Add(2, "Tecnología de Información", 1)


            'obj.CerrarConexion()
            Me.gvDetalle.DataSource = dt
            Me.gvDetalle.DataBind()
            Call md_Funciones.AgregarHearders(gvDetalle)
            Call mt_UpdatePanel("DetalleSolicitud")


        Catch ex As Exception
            Response.Write(ex.Message)
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

                Case "Detalle"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('detalle-tab');", True)

                Case "Detalle->Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('Detalle->Listado');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "ListaSolicitud"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)
                Case "RegistraSolicitud"
                    Me.udpRegistrar.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "", True)
                Case "DetalleSolicitud"
                    Me.udpDetalleSolic.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpDetalleUpdate", "", True)

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

    Protected Sub btnSalirDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirDetalle.Click
        Try
            Call mt_CargarDatos()
            Call mt_FlujoTabs("Detalle->Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            Call mt_CargarDatos()
            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            'Session("frmNotificaciones-codigo_not") = Me.grwLista.DataKeys(index).Values("codigo_not").ToString
            'Call mt_ShowMessage("HOLA", MessageType.error)
            Select Case e.CommandName
                Case "VerDetalle"

                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    'If Not mt_CargarFormularioRegistro(CInt(Session("frmNotificaciones-codigo_not"))) Then Exit Sub
                    Call mt_CargarDetalle()
                    Call mt_FlujoTabs("Detalle")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub




End Class
