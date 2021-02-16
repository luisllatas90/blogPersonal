Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class GradosYTitulos_frmEstructuraTramaElectronica
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES    
    Dim me_SesionConsejoUniv_GYT As e_SesionConsejoUniv_GYT
    Dim me_TipoDenominacionGradoTitulo As e_TipoDenominacionGradoTitulo
    Dim me_EnvioDiplomasProveedor As e_EnvioDiplomasProveedor

    'DATOS    
    Dim md_SesionConsejoUniv_GYT As New d_SesionConsejoUniv_GYT
    Dim md_TipoDenominacionGradoTitulo As New d_TipoDenominacionGradoTitulo
    Dim md_EnvioDiplomasProveedor As New d_EnvioDiplomasProveedor
    Dim md_Funciones As New d_Funciones

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
                Call mt_CargarComboSesionConsejo()
                Call mt_CargarComboTipoDenominacion()                
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbSesionConsejoFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSesionConsejoFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoDenominacionFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoDenominacionFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub cmbTipoEmisionFiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEmisionFiltro.SelectedIndexChanged
        Try
            Call mt_LimpiarListas()
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

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Call mt_Exportar("N")            
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnExportarRegistrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportarRegistrar.Click
        Try
            Call mt_Exportar("R")            
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

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboSesionConsejo()
        Try
            Dim dt As New Data.DataTable : me_SesionConsejoUniv_GYT = New e_SesionConsejoUniv_GYT

            With me_SesionConsejoUniv_GYT
                '.operacion = "CON"
                '.abreviatura_con = "CU"
                .operacion = "L"
                .codigo_scu = "%"
            End With
            dt = md_SesionConsejoUniv_GYT.ListarSesionesConsejo(me_SesionConsejoUniv_GYT)

            Call md_Funciones.CargarCombo(Me.cmbSesionConsejoFiltro, dt, "codigo_scu", "descripcion_scu", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarComboTipoDenominacion()
        Try
            Dim dt As New Data.DataTable : me_TipoDenominacionGradoTitulo = New e_TipoDenominacionGradoTitulo

            With me_TipoDenominacionGradoTitulo
                .operacion = "GYT"
            End With
            dt = md_TipoDenominacionGradoTitulo.ConsultarTipoDenominacion(me_TipoDenominacionGradoTitulo)

            Call md_Funciones.CargarCombo(Me.cmbTipoDenominacionFiltro, dt, "codigo", "nombre", True, "[-- SELECCIONE --]", "")
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarListas()
        Try            
            Me.grwListaElectronico.DataSource = Nothing : Me.grwListaElectronico.DataBind()

            Call md_Funciones.AgregarHearders(grwListaElectronico)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            If Not fu_ValidarCargarDatos() Then Exit Sub

            Dim dt As New DataTable : me_EnvioDiplomasProveedor = New e_EnvioDiplomasProveedor

            If Me.grwListaElectronico.Rows.Count > 0 Then Me.grwListaElectronico.DataSource = Nothing : Me.grwListaElectronico.DataBind()

            With me_EnvioDiplomasProveedor
                .operacion = "REP"
                .codigo_scu = Me.cmbSesionConsejoFiltro.SelectedValue
                .codigo_tdg = Me.cmbTipoDenominacionFiltro.SelectedValue
                .tipo_emision = Me.cmbTipoEmisionFiltro.SelectedValue
            End With

            dt = md_EnvioDiplomasProveedor.EstructuraTramaElectronica(me_EnvioDiplomasProveedor)

            Me.grwListaElectronico.DataSource = dt

            If Me.cmbTipoDenominacionFiltro.SelectedValue = 3 OrElse _
                Me.cmbTipoDenominacionFiltro.SelectedValue = 4 Then
                Me.grwListaElectronico.Columns(30).Visible = False 'FACULTAD
                Me.grwListaElectronico.Columns(31).Visible = False 'ESCUELA                
            Else
                Me.grwListaElectronico.Columns(30).Visible = True 'FACULTAD
                Me.grwListaElectronico.Columns(31).Visible = True 'ESCUELA   
            End If

            If Me.cmbTipoEmisionFiltro.SelectedValue = "D" Then
                Me.grwListaElectronico.Columns(46).Visible = True 'FECHA_DUPLICADO
            Else
                Me.grwListaElectronico.Columns(46).Visible = False 'FECHA_DUPLICADO
            End If

            Me.grwListaElectronico.DataBind()

            Call md_Funciones.AgregarHearders(grwListaElectronico)

            Call mt_UpdatePanel("Lista")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function fu_ValidarCargarDatos() As Boolean
        Try            
            If String.IsNullOrEmpty(Me.cmbSesionConsejoFiltro.SelectedValue) OrElse Me.cmbSesionConsejoFiltro.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar una sesión de consejo.", MessageType.warning) : Me.cmbSesionConsejoFiltro.Focus() : Return False
            If String.IsNullOrEmpty(Me.cmbTipoDenominacionFiltro.SelectedValue) OrElse Me.cmbTipoDenominacionFiltro.SelectedValue = 0 Then mt_ShowMessage("Debe seleccionar una denominación.", MessageType.warning) : Me.cmbTipoDenominacionFiltro.Focus() : Return False                        
            If String.IsNullOrEmpty(Me.cmbTipoEmisionFiltro.SelectedValue) Then mt_ShowMessage("Debe seleccionar un tipo de emisión.", MessageType.warning) : Me.cmbTipoEmisionFiltro.Focus() : Return False

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Sub mt_Exportar(ByVal tipo_exportar As String)
        Try
            If fu_ValidarCargarDatos() Then
                If Me.grwListaElectronico.Rows.Count > 0 Then
                    Session("frmDescargarExcel.formulario") = "frmEstructuraTramaElectronica"
                    Session("frmDescargarExcel.nombre_archivo") = "Listado de Diplomas Electrónicos"
                    Session("frmDescargarExcel.param01") = Me.cmbSesionConsejoFiltro.SelectedValue
                    Session("frmDescargarExcel.param02") = Me.cmbTipoDenominacionFiltro.SelectedValue
                    Session("frmDescargarExcel.param03") = Me.cmbTipoEmisionFiltro.SelectedValue
                    Session("frmDescargarExcel.param04") = tipo_exportar
                    Session("frmDescargarExcel.param05") = cod_ctf

                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "openwindows", "window.open('../frmDescargarExcel.aspx');", True)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "presionarBotonListar", "presionarBotonListar();", True)
                Else
                    mt_ShowMessage("No existen registros en la lista a exportar.", MessageType.warning)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class
