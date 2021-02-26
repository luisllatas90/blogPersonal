
Partial Class administrativo_activofijo_L_Interfaces_frmImporLibroActivoFijo
    Inherits System.Web.UI.Page

    Dim md_Funciones As New d_Funciones

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum


    Protected Sub btnListarLibroContable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarLibroContable.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub btnNuevoRegistro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoRegistro.Click
        Try
            Call mt_FlujoTabs("Registrar")
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
            dt.Columns.Add("cod_LibroActFijo", GetType(Integer))
            dt.Columns.Add("cod_cuent_contable", GetType(String))
            dt.Columns.Add("cuenta_contable", GetType(String))
            dt.Columns.Add("fecha_adquis", GetType(String))
            dt.Columns.Add("num_comprobPago", GetType(String))
            dt.Columns.Add("ruc_proveedor", GetType(String))
            dt.Columns.Add("desc_proveedor", GetType(String))
            dt.Columns.Add("modelo", GetType(String))
            dt.Columns.Add("marca", GetType(String))
            dt.Columns.Add("serie", GetType(String))
            dt.Columns.Add("cantidad", GetType(String))
            dt.Columns.Add("costoUnitario", GetType(String))
            dt.Columns.Add("costoAdquisicion", GetType(String))
            dt.Columns.Add("porcDepreciacion", GetType(String))
            dt.Columns.Add("valorNeto", GetType(String))

            dt.Rows.Add(1, "33611001", "Equipos de cómputo", "27/02/2020", "E001-1357", "10462765111", "INVERSIONES CH COMPUTER S.R.L.", "", "", "", "1", "342.87", "342.87", "25%", " 271.44")
            dt.Rows.Add(2, "33611001", "Equipos de cómputo", "27/02/2020", "E001-1357", "10462765111", "INVERSIONES CH COMPUTER S.R.L.", "", "", "", "1", "342.87", "342.87", "25%", " 271.44")
            dt.Rows.Add(3, "33691001", "Otros Equipos", "27/02/2020", "E001-1357", "10462765111", "INDELAB SOCIEDAD ANONIMA CERRADA - INDELAB S.A.C.", "", "", "", "1", "342.87", "342.87", "25%", " 271.44")
            dt.Rows.Add(4, "33691001", "Otros Equipos", "27/02/2020", "E001-1357", "10462765111", "CIMATEC S.A.C.", "", "", "", "1", "342.87", "342.87", "25%", " 271.44")

            'obj.CerrarConexion()
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CalcularResumenFinal()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_ResFinal", GetType(Integer))
            dt.Columns.Add("num_CuenContable", GetType(String))
            dt.Columns.Add("sum_CostoAdquis", GetType(String))
            dt.Columns.Add("dep_acumulada_2019", GetType(String))
            dt.Columns.Add("dep_ejerc_2020", GetType(String))
            dt.Columns.Add("sum_valor_neto", GetType(String))

            dt.Rows.Add(1, "33111001", "3224695.00", "500504.107", "292830.8975", "3224695")
            dt.Rows.Add(1, "33111001", "5424695.00", "500504.107", "292830.8975", "5424695.00")
            dt.Rows.Add(1, "33211002", "3792042.944", "500504.107", "292830.8975", "5424695.00")
            'obj.CerrarConexion()
            Me.grwResumenFinal.DataSource = dt
            Me.grwResumenFinal.DataBind()
            Call md_Funciones.AgregarHearders(grwResumenFinal)
            Call mt_UpdatePanel("ResumenFinal")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CalcularTotalGeneral()

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("cod_ResFinalTotalGeneral", GetType(Integer))
            dt.Columns.Add("sum_CostoAdquisTot", GetType(String))
            dt.Columns.Add("dep_acumulada_2019Tot", GetType(String))
            dt.Columns.Add("dep_ejerc_2020Tot", GetType(String))
            dt.Columns.Add("sum_valor_netoTot", GetType(String))

            dt.Rows.Add(1, "7224695.00", "500504.107", "292830.8975", "3224695")

            'obj.CerrarConexion()
            Me.grwResFinalTotales.DataSource = dt
            Me.grwResFinalTotales.DataBind()
            Call md_Funciones.AgregarHearders(grwResFinalTotales)
            Call mt_UpdatePanel("ResumenTotalGeneral")

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

                Case "ResumenFinal"
                    Me.udpResumenFinal.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpResumenFinalUpdate", "", True)

                Case "ResumenTotalGeneral"
                    Me.udpResFinalTotalGeneral.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpResumenFinalUpdate", "", True)

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
                Case "Editar"
                    Call mt_FlujoTabs("Editar")

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

                Case "Registrar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registrar-tab');", True)

                Case "Editar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('editar-tab');", True)

                Case "Registrar->Listar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registrar->listar');", True)

                Case "Editar->Listar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('editar->listar');", True)

                Case "Listar->Importar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listar->importar');", True)

                Case "Importar->Listar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('importar->listar');", True)

                Case "Listar->ResumenFinal"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listar->resumenfinal');", True)

                Case "ResumenFinal->Listar"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('resumenfinal->listar');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirResFinal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirResFinal.Click
        Try

            Call mt_CargarDatos()
            Call mt_FlujoTabs("ResumenFinal->Listar")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirRegDatosContables_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirRegDatosContables.Click
        Try

            Call mt_CargarDatos()
            Call mt_FlujoTabs("Registrar->Listar")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Protected Sub btnSalirDetDatosContables_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirDetDatosContables.Click
        Try

            Call mt_CargarDatos()
            Call mt_FlujoTabs("Editar->Listar")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar.Click
        Try

            Call mt_FlujoTabs("Listar->Importar")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Protected Sub btnResumenFinal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResumenFinal.Click
        Try

            Call mt_FlujoTabs("Listar->ResumenFinal")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnCalcularResFinal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcularResFinal.Click
        Try

            Call mt_CalcularResumenFinal()
            Call mt_CalcularTotalGeneral()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirImportar.Click
        Try

            Call mt_FlujoTabs("Importar->Listar")


        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub





End Class