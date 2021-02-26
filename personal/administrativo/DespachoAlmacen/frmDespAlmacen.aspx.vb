
Partial Class administrativo_DespachoAlmacen_frmDespAlmacen
    Inherits System.Web.UI.Page

     Dim md_Funciones As New d_Funciones

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatosListado()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub lknBtnDespachar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lknBtnDespachar.Click
        Try
            mt_CargarDatosDespacho()
            mt_FlujoTabs("DespachoPedido")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub chkSelecPedido_Change(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkBox As CheckBox
        Dim i As Integer
        Dim j As Integer
        i = -1
        j = -1
        chkBox = sender
        Try

            For Each row As GridViewRow In grwDetalle.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelecPedido"), CheckBox)
                    i = i + 1
                    If chkRow.Checked Then
                        'Dim cantPed As Integer = CInt(row.Cells(2).Text)
                        'Dim cantAte As Integer = CInt(row.Cells(3).Text)
                        Exit For
                    End If

                End If
            Next

            For Each row As GridViewRow In grwDetalle.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkRow As CheckBox = TryCast(row.Cells(0).FindControl("chkSelecPedido"), CheckBox)
                    j = j + 1
                    If j <> i Then

                        chkRow.Checked = False
                    End If

                End If
            Next



            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoContHab", "flujoContHab('detalle-tab-hab');", True)



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatosDespacho()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("codDespacho", GetType(Integer))
            dt.Columns.Add("item", GetType(String))
            dt.Columns.Add("unidad", GetType(String))
            dt.Columns.Add("cantAte", GetType(Integer))
            dt.Columns.Add("cantComp", GetType(Integer))

            dt.Rows.Add(1213, "BOLSAS ECOLÓGICAS USAT", "UNIDAD", 2, 0)
            dt.Rows.Add(1214, "DISCO DVD", "UNIDAD", 0, 1)
            'obj.CerrarConexion()
            Me.gvAtenderPedido.DataSource = dt
            Me.gvAtenderPedido.DataBind()
            Call md_Funciones.AgregarHearders(gvAtenderPedido)
            Call mt_UpdatePanel("DespachoPedido")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarDatosListado()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()
            dt.Columns.Add("codPedido", GetType(Integer))
            dt.Columns.Add("nroPedido", GetType(Integer))
            dt.Columns.Add("fechPedido", GetType(String))
            dt.Columns.Add("nombSolic", GetType(String))
            dt.Columns.Add("centCosto", GetType(String))
            dt.Columns.Add("codCentCosto", GetType(Integer))

            dt.Rows.Add(1, 2, "15/02/2021", "Alberto Jimenez", "JFC", 622)
            dt.Rows.Add(1, 3, "15/02/2021", "Roberto Gonzales", "JFD", 623)
            'obj.CerrarConexion()
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("ListadoPedido")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"

                Case "ListadoPedido"

                    Me.udpLista.Update()                     
                     ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", true)

                Case "DetallePedido"

                    Me.udpDetalle.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpDetalleUpdate", "", True)

                Case "DespachoPedido"

                    Me.udpDespachoPedido.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpDespachoPedido", "", True)


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
                Case "DetallePedido"

                    'If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub
                    'If Not mt_CargarFormularioRegistro(CInt(Session("frmNotificaciones-codigo_not"))) Then Exit Sub
                    Call mt_CargarDatosDetallePedido()
                    Call mt_FlujoTabs("DetallePedido")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

        Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "ListadoPedido"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)
                Case "DetallePedido"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('detalle-tab');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoCont", "flujoCont('detalle-tab');", True)
                Case "DespachoPedido"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('despacho-tab');", True)
                Case "Despacho->Detalle"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('despacho->detalle-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosDetallePedido()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim i As Integer
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'obj.AbrirConexion()

            dt.Columns.Add("codDetPedido", GetType(String))
            dt.Columns.Add("item", GetType(String))
            dt.Columns.Add("unidad", GetType(String))
            dt.Columns.Add("cantPed", GetType(Integer))
            dt.Columns.Add("cantAte", GetType(Integer))
            dt.Columns.Add("cantPorAte", GetType(Integer))
            dt.Columns.Add("stoActual", GetType(Integer))
            dt.Columns.Add("otroStock", GetType(Integer))
            dt.Columns.Add("cantFalt", GetType(Integer))
            dt.Columns.Add("cantDesp", GetType(Integer))
            dt.Columns.Add("cantComp", GetType(Integer))
            dt.Columns.Add("fecDesea", GetType(String))
            dt.Columns.Add("estado", GetType(String))
            dt.Columns.Add("centCostos", GetType(String))
            dt.Columns.Add("precUnit", GetType(Decimal))
            dt.Columns.Add("observ", GetType(String))
            dt.Columns.Add("despA", GetType(String))

            dt.Rows.Add(1213, "BOLSAS ECOLÓGICAS USAT", "UNIDAD", 100, 10, 10, 20, 10, 10, 30, 20, "20/02/2020", "Por Despachar", "PROGRAMA GESTIÓN DEL TALENTO HUMANO", 2.5, "Sin observaciones", "JOSE QUIÑOS")
            dt.Rows.Add(1214, "DISCO DVD", "UNIDAD", 100, 10, 10, 20, 10, 10, 30, 20, "20/02/2020", "Por Despachar", "PROGRAMA GESTIÓN DEL TALENTO HUMANO", 1.57, "Sin observaciones", "JOSE QUIÑOS")
            dt.Rows.Add(1215, "LAPICERO PROMOCIONAL USAT", "UNIDAD", 100, 10, 10, 20, 10, 10, 30, 20, "20/02/2020", "Atendido", "PROGRAMA GESTIÓN DEL TALENTO HUMANO", 0.75, "Sin observaciones", "JOSE QUIÑOS")
            dt.Rows.Add(1216, "SOBRE MANILA", "UNIDAD", 100, 10, 10, 20, 10, 10, 30, 20, "20/02/2020", "En compra", "PROGRAMA GESTIÓN DEL TALENTO HUMANO", 2.5, "Sin observaciones", "JOSE QUIÑOS")
            dt.Rows.Add(1217, "RESALTADORES", "UNIDAD", 100, 10, 10, 20, 10, 10, 30, 20, "20/02/2020", "Por Almacén", "PROGRAMA GESTIÓN DEL TALENTO HUMANO", 1.5, "Sin observaciones", "JOSE QUIÑOS")

            'obj.CerrarConexion()
            Me.grwDetalle.DataSource = dt
            Me.grwDetalle.DataBind()

            For i = 0 To Me.grwDetalle.Rows.Count - 1
                If (Me.grwDetalle.Rows(i).Cells(12).Text) = "Por Despachar" Then
                    Me.grwDetalle.Rows(i).BackColor = Drawing.Color.GreenYellow
                    Me.grwDetalle.Rows(i).Cells(12).Font.Bold = True
                End If

            Next

            Call md_Funciones.AgregarHearders(grwDetalle)
            Call mt_UpdatePanel("DetallePedido")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkBtnSalirDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSalirDetalle.Click
        Try
            Call btnListar_Click(Nothing, Nothing)
            Call mt_FlujoTabs("ListadoPedido")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lnkBtnSalirDespacho_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSalirDespacho.Click
        Try
            Call mt_CargarDatosDetallePedido()
            Call mt_FlujoTabs("Despacho->Detalle")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub






End Class
