Imports ClsGlobales
Imports ClsSistemaEvaluacion

Partial Class frmComponentesCompetencias
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeComponente As e_Componente, odComponente As d_Componente
    Private oeCompetencia As e_CompetenciaAprendizaje, odCompetencia As d_CompetenciaAprendizaje
    Private oeCompCom As e_Componente_CompetenciaAprendizaje, odCompCom As d_Componente_CompetenciaAprendizaje
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

                mt_CargarComponente()
                mt_CargarCompetencia()
                'Me.divFiltroCompetencia.Visible = False
                Me.divEditCompetencia.Visible = False
                Me.btnAgregarCompetencia.visible = False
                'Else
                '    mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvComponenteCompetencia_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim objGridView As GridView = CType(sender, GridView)
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim objtablecell As TableCell = New TableCell()
            mt_AgregarCabecera(objgridviewrow, objtablecell, 2, "COMPONENTES", "#FFFFFF", True)
            mt_AgregarCabecera(objgridviewrow, objtablecell, 3, "COMPETENCIAS", "#FFFFFF", True)
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Protected Sub cmbCompetencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCompetencia.SelectedIndexChanged
        Dim codigosCom As String = ""
        For Each _Item As ListItem In Me.cmbCompetencia.Items
            If _Item.Selected AndAlso _Item.Value <> "-1" Then
                If codigosCom.Length > 0 Then codigosCom &= ","
                codigosCom &= _Item.Value
            End If
        Next
        Session("adm_list_cod_com") = codigosCom
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroComponente.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Componente !", MessageType.Warning) : Exit Sub
            'If Me.cmbFiltroCompetencia.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregarComponente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_MostrarTabs(1)
        mt_MostrarDivs(0)
        Me.txtComponente.Focus()
    End Sub

    Protected Sub btnAgregarCompetencia_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_MostrarTabs(1)
        mt_MostrarDivs(1)
        Me.txtCompetencia.Focus()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnAltCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim list_insert As String = "", list_delete As String = ""
        Dim dt, dtAux As New Data.DataTable
        Dim list_cod_com() As String
        Try
            oeComponente = New e_Componente : odComponente = New d_Componente
            If Me.txtComponente.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Nombre de Componente !", MessageType.Warning) : Me.txtCompetencia.Focus() : Exit Sub
            If String.IsNullOrEmpty(Session("adm_list_cod_com")) Then mt_ShowMessage("¡ Seleccione Competencia !", MessageType.Warning) : Me.cmbCompetencia.Focus() : Exit Sub
            With oeComponente
                ._nombre_cmp = Me.txtComponente.Text.Trim : ._codigo_per = cod_user
                If Not String.IsNullOrEmpty(Session("amd_codigo_cmp")) Then
                    dtAux = CType(Session("amd_dt_detalle"), Data.DataTable)
                End If
                list_cod_com = Session("adm_list_cod_com").ToString.Split(",")
                Dim _insert As Boolean = False
                For i As Integer = 0 To list_cod_com.Length - 1
                    _insert = True
                    For j As Integer = 0 To dtAux.Rows.Count - 1
                        If list_cod_com(i) = dtAux.Rows(j).Item(3) Then
                            _insert = False
                            Exit For
                        End If
                    Next
                    If _insert Then
                        If list_insert.Length > 0 Then list_insert &= ","
                        list_insert &= list_cod_com(i)
                    End If
                Next
                Dim _delete As Boolean = False
                For x As Integer = 0 To dtAux.Rows.Count - 1
                    _delete = True
                    For y As Integer = 0 To list_cod_com.Length - 1
                        If dtAux.Rows(x).Item(3) = list_cod_com(y) Then
                            _delete = False
                            Exit For
                        End If
                    Next
                    If _delete Then
                        If list_delete.Length > 0 Then list_delete &= ","
                        list_delete &= dtAux.Rows(x).Item(0)
                    End If
                Next
                ._codigo_com = list_insert : ._codigo_cca = list_delete
            End With
            Dim _refresh As Boolean = False
            If String.IsNullOrEmpty(Session("amd_codigo_cmp")) Then
                dt = odComponente.fc_Insertar(oeComponente)
                _refresh = True
            Else
                oeComponente._codigo_cmp = Session("amd_codigo_cmp")
                dt = odComponente.fc_Actualizar(oeComponente)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    mt_CargarComponente()
                    Me.cmbFiltroComponente.SelectedValue = dt.Rows(0).Item(0)
                    'mt_CargarDatos()
                    If _refresh Then
                        'mt_CargarDatos()
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_CargarDatos()
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                    'mt_CargarComponente()
                    'mt_CargarDatos()
                Else
                    Me.txtComponente.Focus()
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAltGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As New Data.DataTable
        Try
            If Me.txtCompetencia.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Nombre de Competencia !", MessageType.Warning) : Exit Sub
            If Me.txtAbrevCompetencia.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Abreviatura de Competencia !", MessageType.Warning) : Exit Sub
            oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
            With oeCompetencia
                ._nombre_com = Me.txtCompetencia.Text : ._abreviatura_com = Me.txtAbrevCompetencia.Text.Trim : ._codigo_per = cod_user
            End With
            Dim refresh As Boolean = False
            If String.IsNullOrEmpty(Session("amd_codigo_com")) Then
                dt = odCompetencia.fc_Insertar(oeCompetencia)
                refresh = True
            Else
                oeCompetencia._codigo_com = Session("amd_codigo_com")
                dt = odCompetencia.fc_Actualizar(oeCompetencia)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    mt_CargarCompetencia()
                    mt_CargarDatos()
                    If refresh Then
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                Else
                    mt_ShowMessage("¡ Ocurrió un error en el proceso !", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvComponenteCompetencia_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvComponenteCompetencia.RowCommand
        Dim index As Integer = -1, codigo_cca As Integer = -1, codigo_cmp As Integer = -1, codigo_com As Integer = -1
        Dim nombre_cmp As String = "", nombre_com As String = ""
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_cca = Me.grvComponenteCompetencia.DataKeys(index).Values("codigo_cca")
                codigo_cmp = Me.grvComponenteCompetencia.DataKeys(index).Values("codigo_cmp")
                nombre_cmp = Me.grvComponenteCompetencia.DataKeys(index).Values("nombre_cmp")
                codigo_com = Me.grvComponenteCompetencia.DataKeys(index).Values("codigo_com")
                nombre_com = Me.grvComponenteCompetencia.DataKeys(index).Values("nombre_com")
                Select Case e.CommandName
                    Case "EditarComponente"
                        mt_LimpiarControles()
                        Session("amd_codigo_cmp") = codigo_cmp
                        Me.txtComponente.Text = nombre_cmp
                        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
                        With oeCompetencia
                            ._tipoOpe = "2" : ._codigo_cmp = codigo_cmp
                        End With
                        dt = odCompetencia.fc_Listar(oeCompetencia)
                        Session("amd_dt_detalle") = dt
                        Dim lst_comp As String = ""
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For Each _Item As ListItem In Me.cmbCompetencia.Items
                                If _Item.Value = dt.Rows(i).Item(3) Then
                                    _Item.Selected = True
                                    If lst_comp.Length > 0 Then lst_comp &= ","
                                    lst_comp &= _Item.Value
                                    Exit For
                                End If
                            Next
                        Next
                        Session("adm_list_cod_com") = lst_comp
                        mt_MostrarTabs(1)
                        mt_MostrarDivs(0)
                    Case "EliminarComponente"
                        oeComponente = New e_Componente : odComponente = New d_Componente
                        oeComponente._codigo_cmp = codigo_cmp : oeComponente._codigo_per = cod_user
                        dt = odComponente.fc_Eliminar(oeComponente)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_CargarDatos()
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            Else
                                mt_ShowMessage("¡ Ocurrió un error en el proceso !", MessageType.Error)
                            End If
                        End If
                    Case "EditarCompetencia"
                        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
                        oeCompetencia._tipoOpe = "3" : oeCompetencia._codigo_com = codigo_com
                        dt = odCompetencia.fc_Listar(oeCompetencia)
                        If dt.Rows.Count > 0 Then
                            Me.cmbCompetenciaEditar.SelectedValue = dt.Rows(0).Item(0)
                            Me.txtCompetencia.Text = dt.Rows(0).Item(2)
                            Me.txtAbrevCompetencia.Text = dt.Rows(0).Item(1)
                        End If
                        Session("amd_codigo_com") = codigo_com
                        mt_MostrarTabs(1)
                        mt_MostrarDivs(1)
                    Case "EliminarCompetencia"
                        oeCompCom = New e_Componente_CompetenciaAprendizaje : odCompCom = New d_Componente_CompetenciaAprendizaje
                        oeCompCom._codigo_cca = codigo_cca : oeCompCom._codigo_per = cod_user
                        dt = odCompCom.fc_Eliminar(oeCompCom)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_CargarDatos()
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            Else
                                mt_ShowMessage("¡ Ocurrió un error en el proceso !", MessageType.Error)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
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

    Private Sub mt_CargarComponente()
        oeComponente = New e_Componente : odComponente = New d_Componente
        mt_LlenarListas(Me.cmbFiltroComponente, odComponente.fc_Listar(oeComponente), "codigo_cmp", "nombre_cmp", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarCompetencia()
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        'mt_LlenarListas(Me.cmbFiltroCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
        mt_LlenarListas(Me.cmbCompetencia, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com")
        mt_LlenarListas(Me.cmbCompetenciaEditar, odCompetencia.fc_Listar(oeCompetencia), "codigo_com", "nombre_com", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        With oeCompetencia
            ._tipoOpe = "2" : ._codigo_cmp = Me.cmbFiltroComponente.SelectedValue ': ._codigo_com = Me.cmbFiltroCompetencia.SelectedValue
        End With
        dt = odCompetencia.fc_Listar(oeCompetencia)
        grvComponenteCompetencia.DataSource = dt
        grvComponenteCompetencia.DataBind()
        If Me.grvComponenteCompetencia.Rows.Count > 0 Then mt_AgruparFilas(Me.grvComponenteCompetencia.Rows, 0, 2)
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

    Private Sub mt_LimpiarControles()
        Session("amd_codigo_cmp") = ""
        Session("amd_codigo_com") = ""
        Session("adm_list_cod_com") = ""
        Session("amd_dt_detalle") = Nothing
        Me.txtComponente.Text = ""
        For Each _Item As ListItem In Me.cmbCompetencia.Items
            If _Item.Selected Then
                _Item.Selected = False
            End If
        Next
        Me.txtCompetencia.Text = ""
        Me.txtAbrevCompetencia.Text = ""
    End Sub

    Private Sub mt_MostrarDivs(ByVal idDiv As Integer)
        Select Case idDiv
            Case 0
                Me.divComponente.Visible = True
                Me.divCompetencia.Visible = False
            Case 1
                Me.divComponente.Visible = False
                Me.divCompetencia.Visible = True
        End Select
    End Sub

#End Region

#Region "Funciones"

#End Region

End Class
