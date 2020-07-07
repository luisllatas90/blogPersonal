Imports System.Net
Imports System.IO
Imports System.Drawing

Partial Class Alumni_frmSincronizarDiagramasER
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'DATOS
    Dim md_Funciones As New d_Funciones
    Dim md_DiagramaER As New d_DiagramaER

    'ENTIDADES
    Dim me_DiagramaER As e_DiagramaER

    'VARIABLES
    Dim cod_user As Integer = 0

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

            If IsPostBack = False Then
                Call mt_CargarComboSRVFiltro()
                Call mt_CargarComboBDFiltro()
                Call mt_LimpiarControles()                
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            If Not fu_ValidarCargarDiagramas() Then Exit Sub

            Call mt_CargarDiagramas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim sincronizar As String = Me.grwLista.DataKeys(e.Row.RowIndex).Values("sincronizar").ToString

                If sincronizar.Trim.Equals("NO") Then
                    TryCast(e.Row.FindControl("btnSincronizarDiagrama"), LinkButton).Visible = False
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try        
    End Sub

    Protected Sub btnSincronizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSincronizar.Click
        Try
            If Not fu_ValidarCargarDiagramas() Then Exit Sub

            If mt_SincronizarDiagramas() Then Call btnListar_Click(Nothing, Nothing)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Dim server_name_origen As String = Me.grwLista.DataKeys(index).Values("server_name_origen").ToString
            Dim server_name_destino As String = Me.grwLista.DataKeys(index).Values("server_name_destino").ToString
            Dim database_name_origen As String = Me.grwLista.DataKeys(index).Values("database_name_origen").ToString
            Dim database_name_destino As String = Me.grwLista.DataKeys(index).Values("database_name_destino").ToString
            Dim diagram_name As String = Me.grwLista.DataKeys(index).Values("diagram_name_origen").ToString

            Select Case e.CommandName
                Case "SincronizarDiagrama"
                    me_DiagramaER = New e_DiagramaER

                    With me_DiagramaER
                        .operacion = "IND"                        
                        .server_name_origen = server_name_origen
                        .server_name_destino = server_name_destino
                        .database_name_origen = database_name_origen
                        .database_name_destino = database_name_destino
                        .diagram_name = diagram_name
                    End With

                    If Not fu_ValidarSincronizarDiagramas(me_DiagramaER) Then Exit Sub

                    md_DiagramaER.SincronizarDiagramaER(me_DiagramaER)

                    Call mt_ShowMessage("¡El diagrama se sincronizo exitosamente!", MessageType.success)

                    Call btnListar_Click(Nothing, Nothing)
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbSRVOrigen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSRVOrigen.SelectedIndexChanged
        Try
            Call mt_CargarComboBDOrigenFiltro()

            Call mt_UpdatePanel("Filtros")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbSRVDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSRVDestino.SelectedIndexChanged
        Try
            Call mt_CargarComboBDDestinoFiltro()

            Call mt_UpdatePanel("Filtros")
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

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "formatoGrilla", "formatoGrilla();", True)

                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Try
            Me.cmbSRVOrigen.SelectedValue = ""
            Me.cmbSRVDestino.SelectedValue = ""
            Me.cmbBDOrigen.SelectedValue = ""
            Me.cmbBDDestino.SelectedValue = ""
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSRVFiltro()
        Try
            Dim dt As New Data.DataTable

            dt = md_DiagramaER.ListarServidorVinculado()

            Call md_Funciones.CargarCombo(Me.cmbSRVOrigen, dt, "SRV_NAME", "SRV_NAME", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbSRVDestino, dt, "SRV_NAME", "SRV_NAME", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboBDOrigenFiltro()
        Try
            Dim dt As New Data.DataTable : me_DiagramaER = New e_DiagramaER

            If Not String.IsNullOrEmpty(Me.cmbSRVOrigen.SelectedValue) Then
                With me_DiagramaER
                    .operacion = "LBD"
                    .server_name = Me.cmbSRVOrigen.SelectedValue
                End With
                dt = md_DiagramaER.ListarDiagramaER(me_DiagramaER)
            Else                
                dt.Columns.Add("database_name", Type.GetType("System.String"))
            End If

            Call md_Funciones.CargarCombo(Me.cmbBDOrigen, dt, "database_name", "database_name", True, "[-- SELECCIONE --]", "")            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboBDDestinoFiltro()
        Try
            Dim dt As New Data.DataTable : me_DiagramaER = New e_DiagramaER

            If Not String.IsNullOrEmpty(Me.cmbSRVDestino.SelectedValue) Then
                With me_DiagramaER
                    .operacion = "LBD"
                    .server_name = Me.cmbSRVDestino.SelectedValue
                End With
                dt = md_DiagramaER.ListarDiagramaER(me_DiagramaER)
            Else                
                dt.Columns.Add("database_name", Type.GetType("System.String"))
            End If

            Call md_Funciones.CargarCombo(Me.cmbBDDestino, dt, "database_name", "database_name", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboBDFiltro()
        Try
            Dim dt As New Data.DataTable            
            dt.Columns.Add("database_name", Type.GetType("System.String"))

            Call md_Funciones.CargarCombo(Me.cmbBDOrigen, dt, "database_name", "database_name", True, "[-- SELECCIONE --]", "")
            Call md_Funciones.CargarCombo(Me.cmbBDDestino, dt, "database_name", "database_name", True, "[-- SELECCIONE --]", "")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDiagramas()
        Try
            Dim dt As New Data.DataTable : me_DiagramaER = New e_DiagramaER

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_DiagramaER
                .operacion = "LOD"
                .server_name_origen = Me.cmbSRVOrigen.Text.Trim.ToUpper
                .server_name_destino = Me.cmbSRVDestino.Text.Trim.ToUpper
                .database_name_origen = Me.cmbBDOrigen.Text.Trim.ToUpper
                .database_name_destino = Me.cmbBDDestino.Text.Trim.ToUpper
            End With

            dt = md_DiagramaER.ListarDiagramaER(me_DiagramaER)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDiagramas() As Boolean
        Try
            If String.IsNullOrEmpty(Me.cmbSRVOrigen.SelectedValue) Then mt_ShowMessage("Debe seleccionar un servidor de origen.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbSRVDestino.SelectedValue) Then mt_ShowMessage("Debe seleccionar un servidor de destino.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbBDOrigen.SelectedValue) Then mt_ShowMessage("Debe seleccionar una base de datos de origen.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(Me.cmbBDDestino.SelectedValue) Then mt_ShowMessage("Debe seleccionar una base de datos de destino.", MessageType.warning) : Return False
            If Me.cmbSRVOrigen.SelectedValue = Me.cmbSRVDestino.SelectedValue Then mt_ShowMessage("El servidor de destino debe ser diferente al servidor de origen.", MessageType.warning) : Me.cmbSRVDestino.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarSincronizarDiagramas(ByVal le_DiagramaER As e_DiagramaER) As Boolean
        Try
            If String.IsNullOrEmpty(le_DiagramaER.operacion.Trim) Then mt_ShowMessage("Debe especificar el tipo de sincronización.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(le_DiagramaER.server_name_origen.Trim) Then mt_ShowMessage("Debe especificar el servidor de origen.", MessageType.warning) : Me.cmbSRVOrigen.Focus() : Return False
            If String.IsNullOrEmpty(le_DiagramaER.server_name_destino.Trim) Then mt_ShowMessage("Debe especificar el servidor de destino.", MessageType.warning) : Me.cmbSRVDestino.Focus() : Return False
            If String.IsNullOrEmpty(le_DiagramaER.database_name_origen.Trim) Then mt_ShowMessage("Debe especificar la base de datos de origen.", MessageType.warning) : Me.cmbBDOrigen.Focus() : Return False
            If String.IsNullOrEmpty(le_DiagramaER.database_name_destino.Trim) Then mt_ShowMessage("Debe especificar la base de datos de destino.", MessageType.warning) : Me.cmbBDDestino.Focus() : Return False
            If le_DiagramaER.server_name_destino.Trim.Equals(le_DiagramaER.server_name_origen.Trim) Then mt_ShowMessage("El servidor de destino debe ser diferente al servidor de origen.", MessageType.warning) : Me.cmbSRVDestino.Focus() : Return False
            If le_DiagramaER.operacion.Trim.Equals("IND") AndAlso String.IsNullOrEmpty(le_DiagramaER.diagram_name.Trim) Then mt_ShowMessage("Debe especificar el diagrama a sincronizar.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_SincronizarDiagramas() As Boolean
        Try
            me_DiagramaER = New e_DiagramaER

            With me_DiagramaER
                .operacion = "ALL"
                .server_name_origen = Me.cmbSRVOrigen.SelectedValue
                .server_name_destino = Me.cmbSRVDestino.SelectedValue
                .database_name_origen = Me.cmbBDOrigen.SelectedValue
                .database_name_destino = Me.cmbBDDestino.SelectedValue                
            End With

            md_DiagramaER.SincronizarDiagramaER(me_DiagramaER)

            Call mt_ShowMessage("¡La sincronización se realizo exitosamente!", MessageType.success)

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
