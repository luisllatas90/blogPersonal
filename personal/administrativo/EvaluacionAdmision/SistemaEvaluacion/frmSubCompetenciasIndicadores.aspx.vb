Imports ClsGlobales
Imports ClsSistemaEvaluacion

Partial Class frmSubCompetenciasIndicadores
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeCompetencia As e_CompetenciaAprendizaje, odCompetencia As d_CompetenciaAprendizaje
    Private oeSubCompetencia As e_SubCompetencia, odSubCompetencia As d_SubCompetencia
    Private oeIndicador As e_Indicador, odIndicador As d_Indicador
    Public cod_user As Integer = 684

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_CargarCompetencia()
                mt_CargarSubCompetencia()
                'Me.divFiltroSubCompetencia.visible = False
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregarSubCompetencia_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_MostrarTabs(1)
        mt_MostrarDivs(0)
        Me.txtSubCompetencia.Focus()
    End Sub

    Protected Sub btnAgregarIndicador_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_MostrarTabs(1)
        mt_MostrarDivs(1)
        Me.txtIndicador.Focus()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New Data.DataTable
        Try
            oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
            If Me.cmbCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Exit Sub
            If Me.txtSubCompetencia.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Nombre de SubCompetencia !", MessageType.Warning) : Exit Sub
            With oeSubCompetencia
                ._codigo_com = Me.cmbCompetencia.SelectedValue : ._nombre_scom = Me.txtSubCompetencia.Text.Trim : ._codigo_per = cod_user
            End With
            Dim _refresh As Boolean = False
            If String.IsNullOrEmpty(Session("amd_codigo_scom")) Then
                dt = odSubCompetencia.fc_Insertar(oeSubCompetencia)
                _refresh = True
            Else
                oeSubCompetencia._codigo_scom = Session("amd_codigo_scom")
                dt = odSubCompetencia.fc_Actualizar(oeSubCompetencia)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    mt_CargarSubCompetencia()
                    'Me.cmbFiltroSubCompetencia.SelectedValue = dt.Rows(0).Item(0)
                    mt_CargarDatos()
                    If _refresh Then
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                Else
                    Me.txtSubCompetencia.Focus()
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAltCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnAltGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New Data.DataTable
        Try
            oeIndicador = New e_Indicador : odIndicador = New d_Indicador
            If Me.cmbAltCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Exit Sub
            If Me.cmbSubCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione SubCompetencia !", MessageType.Warning) : Exit Sub
            If Me.txtIndicador.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Nombre de Indicador !", MessageType.Warning) : Exit Sub
            If Me.txtDescripcion.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Descripción de Indicador !", MessageType.Warning) : Exit Sub
            With oeIndicador
                ._codigo_scom = Me.cmbSubCompetencia.SelectedValue : ._nombre_ind = Me.txtIndicador.Text.Trim : ._codigo_per = cod_user
                ._descripcion_ind = Me.txtDescripcion.Text.Trim
            End With
            Dim _refresh As Boolean = False
            If String.IsNullOrEmpty(Session("amd_codigo_ind")) Then
                dt = odIndicador.fc_Insertar(oeIndicador)
                _refresh = True
            Else
                oeIndicador._codigo_ind = Session("amd_codigo_ind")
                dt = odIndicador.fc_Actualizar(oeIndicador)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    mt_CargarDatos()
                    If _refresh Then
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                Else
                    Me.txtIndicador.Focus()
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvSubCompetenciaIndicador_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvSubCompetenciaIndicador.RowCommand
        Dim index As Integer = -1, codigo_com As Integer = -1, codigo_scom As Integer = -1, codigo_ind As Integer = -1
        Dim nombre_scom As String = "", nombre_ind As String = "", descripcion_ind As String = ""
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_com = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("codigo_com")
                codigo_scom = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("codigo_scom")
                nombre_scom = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("nombre_scom")
                codigo_ind = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("codigo_ind")
                nombre_ind = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("nombre_ind")
                descripcion_ind = Me.grvSubCompetenciaIndicador.DataKeys(index).Values("descripcion_ind")
                Select Case e.CommandName
                    Case "EditarSubCompetencia"
                        mt_LimpiarControles()
                        Session("amd_codigo_scom") = codigo_scom
                        Me.cmbCompetencia.SelectedValue = codigo_com
                        Me.txtSubCompetencia.Text = nombre_scom
                        mt_MostrarTabs(1)
                        mt_MostrarDivs(0)
                        Me.txtSubCompetencia.Focus()
                    Case "EliminarSubCompetencia"
                        oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
                        With oeSubCompetencia
                            ._codigo_scom = codigo_scom : ._codigo_per = cod_user
                        End With
                        dt = odSubCompetencia.fc_Eliminar(oeSubCompetencia)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_CargarDatos()
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            End If
                        End If
                    Case "EditarIndicador"
                        mt_LimpiarControles()
                        Session("amd_codigo_ind") = codigo_ind
                        Me.cmbAltCompetencia.SelectedValue = codigo_com
                        cmbAltCompetencia_SelectedIndexChanged(Nothing, Nothing)
                        Me.cmbSubCompetencia.SelectedValue = codigo_scom
                        Me.txtIndicador.Text = nombre_ind
                        Me.txtDescripcion.Text = descripcion_ind
                        mt_MostrarTabs(1)
                        mt_MostrarDivs(1)
                        Me.txtIndicador.Focus()
                    Case "EliminarIndicador"
                        oeIndicador = New e_Indicador : odIndicador = New d_Indicador
                        With oeIndicador
                            ._codigo_ind = codigo_ind : ._codigo_per = cod_user
                        End With
                        dt = odIndicador.fc_Eliminar(oeIndicador)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_CargarDatos()
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbAltCompetencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAltCompetencia.SelectedIndexChanged
        oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
        oeSubCompetencia._tipoOpe = "1" : oeSubCompetencia._codigo_com = Me.cmbAltCompetencia.SelectedValue
        mt_LlenarListas(Me.cmbSubCompetencia, odSubCompetencia.fc_Listar(oeSubCompetencia), "codigo_scom", "nombre_scom", "-- SELECCIONE --")
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Dim dvAlerta As New Literal
        Dim cssclss As String
        Select Case type
            Case MessageType.Success
                cssclss = "alert-success"
            Case MessageType.Error
                cssclss = "alert-danger"
            Case MessageType.Warning
                cssclss = "alert-warning"
            Case Else
                cssclss = "alert-info"
        End Select
        dvAlerta.Text = "<div id='alert_div' style='margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;' class='alert " + cssclss + "'>"
        dvAlerta.Text += "  <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>"
        dvAlerta.Text += "  <span>" + Message + "</span>"
        dvAlerta.Text += "</div>"
        Me.divMensaje.controls.add(dvAlerta)
    End Sub

    Private Sub mt_CargarCompetencia()
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        mt_LlenarListas(Me.cmbFiltroCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbAltCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarSubCompetencia()
        oeSubCompetencia = New e_SubCompetencia : odSubCompetencia = New d_SubCompetencia
        'mt_LlenarListas(Me.cmbFiltroSubCompetencia, odSubCompetencia.fc_Listar(oeSubCompetencia), "codigo_scom", "nombre_scom", "-- SELECCIONE --")
        'mt_LlenarListas(Me.cmbSubCompetencia, odSubCompetencia.fc_Listar(oeSubCompetencia), "codigo_scom", "nombre_scom", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeIndicador = New e_Indicador : odIndicador = New d_Indicador
        With oeIndicador
            ._tipoOpe = "1" : ._codigo_scom = Me.cmbFiltroCompetencia.SelectedValue ': ._codigo_com = Me.cmbFiltroCompetencia.SelectedValue
        End With
        dt = odIndicador.fc_Listar(oeIndicador)
        grvSubCompetenciaIndicador.DataSource = dt
        grvSubCompetenciaIndicador.DataBind()
        If Me.grvSubCompetenciaIndicador.Rows.Count > 0 Then mt_AgruparFilas(Me.grvSubCompetenciaIndicador.Rows, 0, 3)
    End Sub

    Private Sub mt_LimpiarControles()
        Session("amd_codigo_scom") = ""
        Session("amd_codigo_ind") = ""
        Me.cmbCompetencia.SelectedValue = "-1"
        Me.txtSubCompetencia.Text = String.Empty
        Me.cmbAltCompetencia.SelectedValue = "-1"
        Me.cmbSubCompetencia.SelectedValue = "-1"
        Me.txtIndicador.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
    End Sub

    Private Sub mt_MostrarTabs(ByVal idTab As Integer)
        Select Case idTab
            Case 0
                Me.navlistatab.Attributes("class") = "nav-item nav-link active"
                Me.navlistatab.Attributes("aria-selected") = "true"
                Me.navlista.Attributes("class") = "tab-pane fade show active"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link"
                Me.navmantenimientotab.Attributes("aria-selected") = "false"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade"
            Case 1
                Me.navlistatab.Attributes("class") = "nav-item nav-link"
                Me.navlistatab.Attributes("aria-selected") = "false"
                Me.navlista.Attributes("class") = "tab-pane fade"
                Me.navmantenimientotab.Attributes("class") = "nav-item nav-link active"
                Me.navmantenimientotab.Attributes("aria-selected") = "true"
                Me.navmantenimiento.Attributes("class") = "tab-pane fade show active"
        End Select
    End Sub

    Private Sub mt_MostrarDivs(ByVal idDiv As Integer)
        Select Case idDiv
            Case 0
                Me.divSubCompetencia.visible = True
                Me.divIndicador.visible = False
            Case 1
                Me.divSubCompetencia.visible = False
                Me.divIndicador.visible = True
        End Select
    End Sub

#End Region

   
End Class
