Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports System.Collections.Generic

Partial Class frmTiposEvaluacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeCompetencia As e_CompetenciaAprendizaje, odCompetencia As d_CompetenciaAprendizaje
    Private oeTipoEvaluacion As e_TipoEvaluacion, odTipoEvaluacion As d_TipoEvaluacion
    Private oeTipoEvaluacion_Indicador As e_TipoEvaluacion_Indicador, odTipoEvaluacion_Indicador As d_TipoEvaluacion_Indicador
    Public cod_user As Integer = 684
    Private orden_col As Integer = 2

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
            Else
                mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvTipoEvaluacion_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As Data.DataTable
        dt = CType(Session("adm_dtCompetencia"), Data.DataTable)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim objGridView As GridView = CType(sender, GridView)
            Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
            Dim objtablecell As TableCell = New TableCell()
            mt_AgregarCabecera(objgridviewrow, objtablecell, orden_col, "TIPO EVALUACION", "#FFFFFF", True)
            mt_AgregarCabecera(objgridviewrow, objtablecell, dt.Rows.Count, "COMPETENCIAS", "#FFFFFF", True)
            objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
        End If
    End Sub

    Protected Sub grvTipoEvaluacion_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim btn As New LinkButton()
        Dim lblStep As New Label(), lblTotal As New Label()
        Dim codigo_tev As Integer = -1, codigo_com As Integer = -1, cant As Integer = -1, total As Integer = -1
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As Data.DataTable
            dt = CType(Session("adm_dtCompetencia"), Data.DataTable)
            codigo_tev = Me.grvTipoEvaluacion.DataKeys(e.Row.RowIndex).Values("codigo_tev")
            total = 0 : cant = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_com = dt.Rows(i).Item(0)
                cant = fc_ObtenerTotal(codigo_tev, codigo_com)
                e.Row.Cells(orden_col + i).Text = cant
                total += cant
            Next

            lblTotal = New Label()
            lblTotal.ID = "lblTotal"
            lblTotal.Text = total
            e.Row.Cells(orden_col + dt.Rows.Count).Controls.Add(lblTotal)

            btn = New LinkButton()
            btn.ID = "btnEditar"
            btn.Text = "<i class='fa fa-edit'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "Editar"
            btn.ToolTip = "Editar"
            btn.CssClass = "btn btn-sm btn-accion btn-primary"
            e.Row.Cells(orden_col + dt.Rows.Count + 1).Controls.Add(btn)

            lblStep = New Label()
            lblStep.ID = "lblStep1"
            lblStep.Text = " "
            e.Row.Cells(orden_col + dt.Rows.Count + 1).Controls.Add(lblStep)

            'btn = New LinkButton()
            'btn.ID = "btnAdjuntarPortada"
            'btn.Text = "<i class='fa fa-file-upload'></i>"
            'btn.CommandArgument = e.Row.RowIndex
            'btn.CommandName = "Adjuntar"
            'btn.CssClass = "btn btn-sm btn-accion btn-secondary"
            'e.Row.Cells(orden_col + dt.Rows.Count + 1).Controls.Add(btn)

            'lblStep = New Label()
            'lblStep.ID = "lblStep2"
            'lblStep.Text = " "
            'e.Row.Cells(orden_col + dt.Rows.Count + 1).Controls.Add(lblStep)

            btn = New LinkButton()
            btn.ID = "btnEliminar"
            btn.Text = "<i class='fa fa-trash-alt'></i>"
            btn.CommandArgument = e.Row.RowIndex
            btn.CommandName = "Eliminar"
            btn.ToolTip = "Eliminar"
            btn.CssClass = "btn btn-sm btn-accion btn-danger"
            e.Row.Cells(orden_col + dt.Rows.Count + 1).Controls.Add(btn)
        End If
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            mt_CrearColumnComp(CType(Session("adm_dtCompetencia"), Data.DataTable))
            mt_CargarTotales()
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_LimpiarControles()
        mt_CargarDetalle(-1)
        mt_MostrarTabs(1)
        Me.txtDescripcion.Focus()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim codigo_tei As Integer = -1, codigo_ind As Integer = -1
        Dim cant As Integer = -1, cant_ant As Integer = -1
        Dim dt As New Data.DataTable
        Try
            If Me.txtDescripcion.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Descripcion !", MessageType.Warning) : Exit Sub
            If Me.txtBasico.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Peso de Preguntas Basicas !", MessageType.Warning) : Exit Sub
            If Me.txtIntermedio.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Peso de Preguntas Intermedias !", MessageType.Warning) : Exit Sub
            If Me.txtAvanzado.Text.Trim = "" Then mt_ShowMessage("¡ Ingrese Peso de Preguntas Avanzadas !", MessageType.Warning) : Exit Sub
            oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
            With oeTipoEvaluacion
                ._nombre_tev = Me.txtDescripcion.Text : ._peso_basica_tev = Me.txtBasico.Text.Trim : ._peso_intermedia_tev = Me.txtIntermedio.Text
                ._peso_avanzada_tev = Me.txtAvanzado.Text : ._codigo_per = cod_user
                ._virtual_tev = Me.chkVirtual.checked '20201123-ENevado
                ._leInsert = New List(Of e_TipoEvaluacion_Indicador) : ._leEdit = New List(Of e_TipoEvaluacion_Indicador) : ._leDelet = New List(Of e_TipoEvaluacion_Indicador)
                For i As Integer = 0 To Me.grvConfigPreguntas.Rows.Count - 1
                    codigo_tei = Me.grvConfigPreguntas.DataKeys(i).Values("codigo_tei")
                    codigo_ind = Me.grvConfigPreguntas.DataKeys(i).Values("codigo_ind")
                    cant_ant = Me.grvConfigPreguntas.DataKeys(i).Values("cantidad_preguntas_tei")
                    Dim txtCant As TextBox = CType(Me.grvConfigPreguntas.Rows(i).FindControl("txtCantidad"), TextBox)
                    cant = txtCant.Text
                    oeTipoEvaluacion_Indicador = New e_TipoEvaluacion_Indicador
                    If codigo_tei = -1 Then
                        If cant > 0 Then
                            With oeTipoEvaluacion_Indicador
                                ._codigo_ind = codigo_ind : ._cantidad_preguntas_tev = cant : ._codigo_per = cod_user
                            End With
                            ._leInsert.Add(oeTipoEvaluacion_Indicador)
                        End If
                    Else
                        oeTipoEvaluacion_Indicador._codigo_tei = codigo_tei : oeTipoEvaluacion_Indicador._codigo_per = cod_user
                        If cant = 0 Then
                            ._leDelet.Add(oeTipoEvaluacion_Indicador)
                        Else
                            If cant_ant <> cant Then
                                oeTipoEvaluacion_Indicador._cantidad_preguntas_tev = cant
                                ._leEdit.Add(oeTipoEvaluacion_Indicador)
                            End If
                        End If
                    End If
                Next
            End With
            Dim _refresh As Boolean = False
            If String.IsNullOrEmpty(Session("adm_codigo_tev")) Then
                dt = odTipoEvaluacion.fc_Insertar(oeTipoEvaluacion)
                _refresh = True
            Else
                oeTipoEvaluacion._codigo_tev = Session("adm_codigo_tev")
                dt = odTipoEvaluacion.fc_Actualizar(oeTipoEvaluacion)
            End If
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) <> -1 Then
                    mt_MostrarTabs(0)
                    btnListar_Click(Nothing, Nothing)
                    If _refresh Then
                        mt_ShowMessage("¡ Los Datos fueron registrados exitosamente !", MessageType.Success)
                    Else
                        mt_ShowMessage("¡ Los Datos fueron actualizados exitosamente !", MessageType.Success)
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvTipoEvaluacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvTipoEvaluacion.RowCommand
        Dim index As Integer = -1, codigo_tev As Integer = -1
        Dim dt As New Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_tev = Me.grvTipoEvaluacion.DataKeys(index).Values("codigo_tev")
                Select Case e.CommandName
                    Case "Editar"
                        mt_LimpiarControles()
                        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
                        oeTipoEvaluacion._tipoOpe = "1" : oeTipoEvaluacion._codigo_tev = codigo_tev
                        dt = odTipoEvaluacion.fc_Listar(oeTipoEvaluacion)
                        If dt.Rows.Count > 0 Then
                            Me.txtDescripcion.Text = dt.Rows(0).Item(1)
                            Me.txtBasico.Text = dt.Rows(0).Item(2)
                            Me.txtIntermedio.Text = dt.Rows(0).Item(3)
                            Me.txtAvanzado.Text = dt.Rows(0).Item(4)
                            Me.chkvirtual.checked = dt.Rows(0).Item("virtual_tev") '20201123-ENevado
                        End If
                        mt_CargarDetalle(codigo_tev)
                        Session("adm_codigo_tev") = codigo_tev
                        mt_MostrarTabs(1)
                        chkVirtual_ChekedChanged(Nothing, Nothing)
                        Me.txtDescripcion.Focus()
                    Case "Eliminar"
                        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
                        oeTipoEvaluacion._codigo_tev = codigo_tev : oeTipoEvaluacion._codigo_per = cod_user
                        dt = odTipoEvaluacion.fc_Eliminar(oeTipoEvaluacion)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0).Item(0) <> -1 Then
                                mt_MostrarTabs(0)
                                btnListar_Click(Nothing, Nothing)
                                mt_ShowMessage("¡ Los Datos fueron eliminados exitosamente !", MessageType.Success)
                            End If
                        End If
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkVirtual_ChekedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To Me.grvConfigPreguntas.Rows.Count - 1
            Dim txtCant As TextBox = CType(Me.grvConfigPreguntas.Rows(i).FindControl("txtCantidad"), TextBox)
            txtCant.enabled = Not chkvirtual.checked
        Next
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
        Me.divMensaje.Controls.Add(dvAlerta)
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
        Session("adm_codigo_tev") = ""
        Me.txtDescripcion.Text = ""
        Me.txtBasico.Text = ""
        Me.txtIntermedio.Text = ""
        Me.txtAvanzado.Text = ""
    End Sub

    Private Sub mt_CargarCompetencia()
        oeCompetencia = New e_CompetenciaAprendizaje : odCompetencia = New d_CompetenciaAprendizaje
        Session("adm_dtCompetencia") = odCompetencia.fc_Listar(oeCompetencia)
    End Sub

    Private Sub mt_CrearColumnComp(ByVal dtColumn As Data.DataTable)
        Dim tfield As New TemplateField()
        If Me.grvTipoEvaluacion.Columns.Count > orden_col Then
            'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
            For i As Integer = (orden_col + 1) To Me.grvTipoEvaluacion.Columns.Count
                Me.grvTipoEvaluacion.Columns.RemoveAt(Me.grvTipoEvaluacion.Columns.Count - 1)
            Next
        End If
        If dtColumn.Rows.Count = 0 Then
            tfield = New TemplateField()
            tfield.HeaderText = ""
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.grvTipoEvaluacion.Columns.Add(tfield)
        Else
            For x As Integer = 0 To dtColumn.Rows.Count - 1
                tfield = New TemplateField()
                tfield.HeaderText = dtColumn.Rows(x).Item("nombre_corto_com").ToString.ToUpper
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.grvTipoEvaluacion.Columns.Add(tfield)
            Next
        End If
        ' Crear Column para Totales
        tfield = New TemplateField()
        tfield.HeaderText = "TOTALES"
        tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        Me.grvTipoEvaluacion.Columns.Add(tfield)
        ' Crear Column para operaciones
        tfield = New TemplateField()
        tfield.HeaderText = "OPERACIONES"
        tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        Me.grvTipoEvaluacion.Columns.Add(tfield)
    End Sub

    Private Sub mt_CargarTotales()
        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
        oeTipoEvaluacion._tipoOpe = "2"
        Session("adm_dtTotales") = odTipoEvaluacion.fc_Listar(oeTipoEvaluacion)
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        Dim dv As Data.DataView
        oeTipoEvaluacion = New e_TipoEvaluacion : odTipoEvaluacion = New d_TipoEvaluacion
        dt = odTipoEvaluacion.fc_Listar(oeTipoEvaluacion)
        If Me.txtFiltroTipoEvaluacion.text.trim <> "" Then
            dv = New Data.DataView(dt, "nombre_tev like '%" & Me.txtFiltroTipoEvaluacion.text.trim & "%'", "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
        End If
        Me.grvTipoEvaluacion.DataSource = dt
        Me.grvTipoEvaluacion.DataBind()
        'If Me.grvPesos.Rows.Count > 0 Then mt_AgruparFilas(Me.grvPesos.Rows, 0, 2)
    End Sub

    Private Sub mt_RefreshGrid()
        For Each _Row As GridViewRow In Me.grvTipoEvaluacion.Rows
            grvTipoEvaluacion_OnRowDataBound(Me.grvTipoEvaluacion, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub mt_CargarDetalle(ByVal codigo_tev As Integer)
        Dim dt As New Data.DataTable
        oeTipoEvaluacion_Indicador = New e_TipoEvaluacion_Indicador : odTipoEvaluacion_Indicador = New d_TipoEvaluacion_Indicador
        oeTipoEvaluacion_Indicador._tipoOpe = "3" : oeTipoEvaluacion_Indicador._codigo_tev = codigo_tev
        dt = odTipoEvaluacion_Indicador.fc_Listar(oeTipoEvaluacion_Indicador)
        Me.grvConfigPreguntas.DataSource = dt
        Me.grvConfigPreguntas.DataBind()
        If Me.grvConfigPreguntas.Rows.Count > 0 Then mt_AgruparFilas(Me.grvConfigPreguntas.Rows, 0, 2)
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_ObtenerTotal(ByVal codigo_tev As Integer, ByVal codigo_com As Integer) As Integer
        Dim _return As Integer = 0
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("adm_dtTotales"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_tev = " & codigo_tev & " AND codigo_com = " & codigo_com, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                _return = dt.Rows(0).Item(2)
            End If
        End If
        Return _return
    End Function


#End Region

End Class
