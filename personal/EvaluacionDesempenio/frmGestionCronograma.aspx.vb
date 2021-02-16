Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class EvaluacionDesempenio_frmGestionCronograma
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
                Me.cmbTipoTrabajadorFiltro.SelectedValue = "2"
                Me.cmbAnioFiltro.SelectedValue = "2021"
                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Call mt_UpdatePanel("Registro")

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
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

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
            dt.Columns.Add("fecha_desde")
            dt.Columns.Add("fecha_hasta")
            dt.Columns.Add("actividad")
            dt.Columns.Add("responsable")

            fila = dt.NewRow()
            fila("fecha_desde") = "01/01/2021"
            fila("fecha_hasta") = "30/01/2021"
            fila("actividad") = "ACUERDO DE METAS Y COMPETENCIAS"
            fila("responsable") = "TRABAJADOR(A)"

            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("fecha_desde") = "31/01/2021"
            fila("fecha_hasta") = "31/01/2021"
            fila("actividad") = "FECHA LÍMITE PARA ENTREGA DE ACUERDOS"
            fila("responsable") = "DIRECTOR(A) DE PERSONAL"

            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("fecha_desde") = "01/07/2021"
            fila("fecha_hasta") = "30/07/2021"
            fila("actividad") = "REVISIÓN DE AVANCE DE DESEMPEÑO"
            fila("responsable") = "JEFE INMEDIATO"

            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("fecha_desde") = "31/07/2021"
            fila("fecha_hasta") = "31/07/2021"
            fila("actividad") = "FECHA LÍMITE PARA REVISIÓN DE AVANCES"
            fila("responsable") = "DIRECTOR(A) DE PERSONAL"

            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("fecha_desde") = "15/11/2021"
            fila("fecha_hasta") = "15/12/2021"
            fila("actividad") = "REVISIÓN FINAL DE EVALUACIÓN DE DESEMPEÑO"
            fila("responsable") = "JEFE INMEDIATO"

            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
