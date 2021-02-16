Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class personal_frmListaSolicitudEscolaridad
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_SolicitudEscolaridad As e_SolicitudEscolaridad
    Dim me_Anio As e_Anio

    'DATOS
    Dim md_SolicitudEscolaridad As New d_SolicitudEscolaridad
    Dim md_Funciones As New d_Funciones
    Dim md_ArchivoCompartido As New d_ArchivoCompartido
    Dim md_ArchivoCompartidoDetalle As New d_ArchivoCompartidoDetalle
    Dim md_Anio As New d_Anio

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim cod_ctf As Integer = 0

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
            cod_ctf = Request.QueryString("ctf")

            If IsPostBack = False Then                
                Call mt_CargarComboAnio()
                Me.cmbAnioFiltro.SelectedValue = 2021

                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Call mt_CargarDatos()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)            
            Dim IdArchivosCompartidosRecibo As Integer = CInt(Me.grwLista.DataKeys(index).Values("IdArchivosCompartidosRecibo").ToString)
            Dim IdArchivosCompartidosDNI As Integer = CInt(Me.grwLista.DataKeys(index).Values("IdArchivosCompartidosDNI").ToString)

            Select Case e.CommandName
                Case "Recibo"
                    Call mt_DescargarArchivo(IdArchivosCompartidosRecibo)

                Case "DNI"
                    Call mt_DescargarArchivo(IdArchivosCompartidosDNI)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim IdArchivosCompartidosRecibo As Integer = CInt(e.Row.DataItem("IdArchivosCompartidosRecibo").ToString)
                Dim IdArchivosCompartidosDNI As Integer = CInt(e.Row.DataItem("IdArchivosCompartidosDNI").ToString)

                Dim btnVerRecibo As LinkButton
                btnVerRecibo = e.Row.Cells(4).FindControl("btnVerRecibo")

                Dim btnVerDNI As LinkButton
                btnVerDNI = e.Row.Cells(4).FindControl("btnVerDNI")

                If IdArchivosCompartidosRecibo = 0 Then
                    btnVerRecibo.Style.Add("display", "none")
                End If

                If IdArchivosCompartidosDNI = 0 Then
                    btnVerDNI.Style.Add("display", "none")
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            If Me.grwLista.Rows.Count = 0 Then
                mt_ShowMessage("No existen registros en la lista a exportar.", MessageType.warning)
                Exit Sub
            Else
                Session("frmDescargarExcel.formulario") = "frmListaSolicitudEscolaridad"
                Session("frmDescargarExcel.nombre_archivo") = "Lista de Solicitudes de Escolaridad"
                Session("frmDescargarExcel.param01") = Me.cmbAnioFiltro.SelectedValue

                Me.udpScripts.Update()
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../frmDescargarExcel.aspx');", True)
            End If
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
                    If Me.grwLista.Rows.Count > 0 Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboAnio()
        Try
            Dim dt As New Data.DataTable : me_Anio = New e_Anio

            With me_Anio
                .operacion = "GEN"
            End With

            dt = md_Anio.ListarAnio(me_Anio)

            Call md_Funciones.CargarCombo(Me.cmbAnioFiltro, dt, "anio", "anio", False, "-- SELECCIONE --", "0")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable : me_SolicitudEscolaridad = New e_SolicitudEscolaridad

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            With me_SolicitudEscolaridad
                .operacion = "LIS"
                .anio_soe = Me.cmbAnioFiltro.SelectedValue
            End With

            dt = md_SolicitudEscolaridad.ListarSolicitudEscolaridad(me_SolicitudEscolaridad)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_DescargarArchivo(ByVal IdArchivosCompartidos As Integer)
        Try
            If IdArchivosCompartidos = 0 Then mt_ShowMessage("No presenta archivo asociado.", MessageType.warning) : Exit Sub

            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../frmDescargarArchivoCompartido.aspx?Id=" & IdArchivosCompartidos & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
