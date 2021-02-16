Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class EvaluacionDesempenio_frmInstrumentosEvaluacionTrabajador
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
                    me.udpListaInstrumentos.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaInstrumentosUpdate", "udpListaInstrumentosUpdate();", True)

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

            If Session("frmInstrumentosEvaluacionTrabajador-codigo_ins") Is Nothing OrElse String.IsNullOrEmpty(Session("frmInstrumentosEvaluacionTrabajador-codigo_ins")) Then mt_ShowMessage("El código del registro no ha sido encontrado.", MessageType.warning) : Return False

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
            dt.Columns.Add("codigo_ins")
            dt.Columns.Add("dependencia_ins")
            dt.Columns.Add("trabajador_ins")
            dt.Columns.Add("compromiso_ins")
            dt.Columns.Add("compgenerales_ins")
            dt.Columns.Add("compespecific_ins")

            fila = dt.NewRow()
            fila("codigo_ins") = "1"
            fila("dependencia_ins") = "DEPENDENCIA 1"
            fila("trabajador_ins") = "Juan Sanchez"
            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub


    Private Function mt_CargarFormularioRegistro(ByVal codigo_ins As Integer) As Boolean
        Try
            'me_Notificacion = md_Notificacion.GetNotificacion(codigo_ins)

            'If me_Notificacion.codigo_ins = 0 Then mt_ShowMessage("El registro seleccionado no ha sido encontrado.", MessageType.warning) : Return False

            Call mt_LimpiarControles("Registro")
            call mt_CargarDatosEvaluaciones(codigo_ins)
            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_CargarDatosEvaluaciones(ByVal codigo_ins as Integer) as Boolean
        Try
            Dim dt As New DataTable

            If Me.grwListaInstrumentos.Rows.Count > 0 Then Me.grwListaInstrumentos.DataSource = Nothing : Me.grwListaInstrumentos.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("codigo_eva")
            dt.Columns.Add("fecha_eva")
            dt.Columns.Add("descripcion_eva")

            fila = dt.NewRow()
            fila("codigo_eva") = "1"
            fila("fecha_eva") = "20/12/2020"
            fila("descripcion_eva") = "Evaluación Compromiso 2020 - 12"
            dt.Rows.Add(fila)

            Me.grwListaInstrumentos.DataSource = dt
            Me.grwListaInstrumentos.DataBind()

            Call md_Funciones.AgregarHearders(grwListaInstrumentos)

            Call mt_UpdatePanel("ListaInstrumentos")
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