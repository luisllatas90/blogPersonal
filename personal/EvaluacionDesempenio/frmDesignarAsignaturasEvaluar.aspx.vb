Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic


Partial Class EvaluacionDesempenio_frmDesignarAsignaturasEvaluar
    Inherits System.Web.UI.Page

#Region "Variables"

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

#Region "Métodos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    '==================================== GENERALES ====================================

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"

            End Select
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
                
                case "ListaInstrumentos"
                    'me.udpListaInstrumentos.Update()
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaInstrumentosUpdate", "udpListaInstrumentosUpdate();", True)

            End Select
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

    Private Sub mt_LimpiarVariablesSession()
        Try

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarFormularioRegistro() As Boolean
        Try

            If Session("frmInstrumentosEvaluacionTrabajador-codigo_asg") Is Nothing OrElse String.IsNullOrEmpty(Session("frmInstrumentosEvaluacionTrabajador-codigo_asg")) Then mt_ShowMessage("El código del registro no ha sido encontrado.", MessageType.warning) : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    '==================================== DATOS ====================================

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("codigo_asg")
            dt.Columns.Add("nombre_asg")
            dt.Columns.Add("ciclo_asg")
            dt.Columns.Add("docente_asg")
            dt.Columns.Add("grupo_asg")
            dt.Columns.Add("comentario_asg")
            dt.Columns.Add("estado_asg")

            fila = dt.NewRow()
            fila("codigo_asg") = "C0554"
            fila("nombre_asg") = "REALIDAD NACIONAL"
            fila("ciclo_asg") = "I"
            fila("docente_asg") = "JUAN GONZALES LOPEZ"
            fila("grupo_asg") = "B"
            fila("comentario_asg") = "Comentarios negativos de alumnos..."
            fila("estado_asg") = "ACEPTADO"

            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Private Function mt_CargarFormularioRegistro(ByVal codigo_asg As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_asg)

            'If me_Notificacion.codigo_asg = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")
            'call mt_CargarDatosEvaluaciones(codigo_asg)
            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function
 

#End Region

#Region "Botones y Controles"

Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Session("frmInstrumentosEvaluacionTrabajador-codigo_ins") = 0

            Call mt_LimpiarControles("Registro")

            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            Session("frmInstrumentosEvaluacionTrabajador-codigo_ins") = Me.grwLista.DataKeys(index).Values("codigo_ins").ToString

            Select Case e.CommandName
                Case "Editar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    If Not mt_CargarFormularioRegistro(CInt(Session("frmInstrumentosEvaluacionTrabajador-codigo_ins"))) Then Exit Sub

                    Call mt_UpdatePanel("Registro")

                    Call mt_UpdatePanel("ListaInstrumentos")

                    Call mt_FlujoTabs("Registro")

                Case "Eliminar"
                    If Not fu_ValidarCargarFormularioRegistro() Then Exit Sub

                    'If Not mt_EliminarNotificacion(CInt(Session("frmInstrumentosEvaluacionTrabajador-codigo_ins"))) Then Exit Sub

                    'Call btnListar_Click(Nothing, Nothing)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            'Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
