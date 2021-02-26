Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class administrativo_DespachoAlmacen_frmDespachoAlmacen
    Inherits System.Web.UI.Page

 #Region "Declaracion de Variables"
 Dim md_Funciones As New d_Funciones

  Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum
#End Region

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

            'obj.CerrarConexion()
            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()
            Call md_Funciones.AgregarHearders(grwLista)
            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarDatosDetallePedido()
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

            dt.Rows.Add(1,1, "15/02/2021", "Luis Llatas", "Jefatura de Contabilidad",622)
            dt.Rows.Add(1,2, "16/02/2021", "Miluska Nicho", "Jefatura de Logística",2530)

            'obj.CerrarConexion()
            Me.grwDetalle.DataSource = dt
            Me.grwDetalle.DataBind()
            Call md_Funciones.AgregarHearders(grwDetalle)
            Call mt_UpdatePanel("Detalle")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)  Handles btnListar.Click
        Try            
           Call mt_CargarDatosListado()
        Catch ex As Exception
            Throw ex
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
                  Call mt_CargarDatosDetallePedido()
                  Call mt_FlujoTabs("Detalle")
                 

            End Select
        Catch ex As Exception
           Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Detalle"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('detalle-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


   Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
               
                Case "Lista"
                
                     Me.udpLista.Update()                     
                     ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", true)
                    
                Case "Detalle"
                
                     Me.udpDetalle.Update()                     
                     ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpDetalleUpdate", "udpDetalleUpdate();", true)
           
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


End Class
