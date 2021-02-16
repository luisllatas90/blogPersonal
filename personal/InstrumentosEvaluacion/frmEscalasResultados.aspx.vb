Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class InstrumentosEvaluacion_frmEscalasResultados
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES

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

#Region "Eventos"

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

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Call mt_UpdatePanel("Registro")

            Call mt_CargarDatosEscalas()

            Call mt_FlujoTabs("Registro")

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

    Protected Sub btnNuevoEscala_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoEscala.Click
        Try
            Call mt_FlujoModal("RegistroEscalas", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Escala"
                    Call mt_CargarDatosEscalas()

                    Call mt_FlujoTabs("Escala")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirEscala_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirEscala.Click
        Try
            'Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

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
                Case "Filtros"
                    Me.udpFiltros.Update()

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()

                Case "ListaEscalas"
                    Me.udpListaEscalas.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaEscalasUpdate", "udpListaEscalasUpdate();", True)

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

                Case "Escala"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('escala-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoModal(ByVal ls_modal As String, ByVal ls_accion As String)
        Try
            Select Case ls_accion
                Case "open"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & ls_modal & "');", True)

                Case "close"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal('" & ls_modal & "');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
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

    Private Sub mt_LimpiarVariablesSession()
        Try

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("codigo_esc")
            dt.Columns.Add("nombre_esc")
            dt.Columns.Add("descripcion_esc")

            fila = dt.NewRow()
            fila("codigo_esc") = "1"
            fila("nombre_esc") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("descripcion_esc") = "Los resultados pueden ser comparados en los niveles de medición del 1 al 5."
            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosEscalas()
        Try
            Dim dt As New DataTable

            If Me.grwListaEscalas.Rows.Count > 0 Then Me.grwListaEscalas.DataSource = Nothing : Me.grwListaEscalas.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("codigo_esd")
            dt.Columns.Add("descripcion_esd")
            dt.Columns.Add("orden_esd")
            dt.Columns.Add("rango_esd")

            fila = dt.NewRow()
            fila("codigo_esd") = "1"
            fila("descripcion_esd") = "INSUFICIENTE"            
            fila("orden_esd") = "1"
            fila("rango_esd") = "<.. - 2>"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_esd") = "1"
            fila("descripcion_esd") = "REGULAR"
            fila("orden_esd") = "2"
            fila("rango_esd") = "[2 - 3>"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_esd") = "3"
            fila("descripcion_esd") = "BUENO"
            fila("orden_esd") = "3"
            fila("rango_esd") = "[3 - 4>"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_esd") = "4"
            fila("descripcion_esd") = "MUY BUENO"
            fila("orden_esd") = "4"
            fila("rango_esd") = "[4 - 5>"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_esd") = "5"
            fila("descripcion_esd") = "SOBRESALIENTE"
            fila("orden_esd") = "5"
            fila("rango_esd") = "[5 - ...>"
            dt.Rows.Add(fila)

            Me.grwListaEscalas.DataSource = dt
            Me.grwListaEscalas.DataBind()

            Call md_Funciones.AgregarHearders(grwListaEscalas)

            Call mt_UpdatePanel("ListaEscalas")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
