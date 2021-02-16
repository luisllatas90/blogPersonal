Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsGenerarEvaluacion
Imports ClsProcesamientoResultados
Imports ClsBancoPreguntas
Imports System.Collections.Generic

Partial Class ProcesamientoResultados_frmRevisionResultados
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oeEvalDetalle As e_EvaluacionDetalle, odEvalDetalle As d_EvaluacionDetalle
    Private oeEvalAlumno As e_EvaluacionAlumno, odEvalAlumno As d_EvaluacionAlumno
    Private oeAlternativas As e_AlternativaEvaluacion, odAlternativas As d_AlternativaEvaluacion
    Private odGeneral As d_VacantesEvento
    Private mo_RepoAdmision As New ClsAdmision

    Public cod_user As Integer = 684
    Private orden_col As Integer = 3

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
                mt_CargarCicloAcademico()
                'mt_CargarCentroCosto()
            Else
                mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            'mt_CargarComboFiltroCentroCosto()
            mt_CargarCentroCosto()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroCicloAcademico.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Semestre Académico !", MessageType.Warning) : Exit Sub
            If Me.cmbFiltroCentroCostos.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvPostulante_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim codigo_alu As Integer = -1, codigo_prv As Integer = -1
        Dim lb_conforme As Boolean = True
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dt As Data.DataTable
            dt = CType(Session("adm_dtPreguntas"), Data.DataTable)
            codigo_alu = Me.grvPostulante.DataKeys(e.Row.RowIndex).Values("codigo_alu")
            For i As Integer = 0 To dt.Rows.Count - 1
                codigo_prv = dt.Rows(i).Item("codigo_prv").ToString
                e.Row.Cells(orden_col + i).Text = fc_ObetenerRespuesta(codigo_alu, i)
                If lb_conforme AndAlso e.Row.Cells(orden_col + i).Text = "-" Then
                    lb_conforme = False
                End If
            Next
            'e.Row.Cells(orden_col + dt.Rows.Count).Text = "radio"
            Dim rbl As New RadioButtonList()
            rbl.ID = "rblConforme"
            rbl.Items.Add("SI")
            rbl.Items.Add("NO")
            If lb_conforme Then
                rbl.Items(0).Selected = True
            Else
                rbl.Items(1).Selected = True
            End If
            'rbl.Visible = False
            e.Row.Cells(orden_col + dt.Rows.Count).Controls.Add(rbl)
            Dim lblProm As New Label()
            lblProm.ID = "lblCalificacion"
            e.Row.Cells(orden_col + dt.Rows.Count).Controls.Add(lblProm)
            'Dim rb As RadioButton
            'rb = New RadioButton
            'rb.ID = "rbConforme"
            'rb.Text = "SI"
            ''rbl.CssClass = "custom-control custom-checkbox custom-control-inline"
            'e.Row.Cells(orden_col + dt.Rows.Count).Controls.Add(rb)
        End If
    End Sub

    Protected Sub grvEvaluaciones_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvEvaluaciones.RowCommand
        Dim index As Integer = -1, codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim nombre_evl As String = ""
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_evl = Me.grvEvaluaciones.DataKeys(index).Values("codigo_evl")
                nombre_evl = Me.grvEvaluaciones.DataKeys(index).Values("nombre_evl")
                codigo_tev = Me.grvEvaluaciones.DataKeys(index).Values("codigo_tev")
                Select Case e.CommandName
                    Case "Editar"
                        Session("adm_codigo_evl") = codigo_evl
                        Session("adm_codigo_tev") = codigo_tev
                        mt_CargarRespuestas()
                        mt_MostrarTabs(1)
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mt_MostrarTabs(0)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As Data.DataTable
        Dim codigo_elu As Integer = -1
        Dim loEvalAlumn As New List(Of e_EvaluacionAlumno)
        Try
            dt = CType(Session("adm_dtPreguntas"), Data.DataTable)
            odEvalAlumno = New d_EvaluacionAlumno
            For x As Integer = 0 To Me.grvPostulante.Rows.Count - 1
                codigo_elu = Me.grvPostulante.DataKeys(x).Values("codigo_elu")
                oeEvalAlumno = New e_EvaluacionAlumno
                oeEvalAlumno.codigoElu = codigo_elu
                If Me.grvPostulante.Rows(x).FindControl("lblCalificacion") IsNot Nothing Then
                    Dim lbl As Label = CType(Me.grvPostulante.Rows(x).FindControl("lblCalificacion"), Label)
                    Dim xd As String = lbl.Text
                End If
                If Me.grvPostulante.Rows(x).FindControl("rblConforme") IsNot Nothing Then
                    Dim rbl As RadioButtonList = CType(Me.grvPostulante.Rows(x).FindControl("rblConforme"), RadioButtonList)
                    If rbl.Items(0).Selected Or rbl.Items(1).Selected Then
                        oeEvalAlumno.estadoVerificacionElu = "O"
                        oeEvalAlumno.observacionElu = "Ejemplo Test"
                        If rbl.Items(0).Selected Then
                            oeEvalAlumno.estadoVerificacionElu = "C"
                            oeEvalAlumno.observacionElu = ""
                        End If
                    Else
                        oeEvalAlumno.estadoVerificacionElu = "P"
                        oeEvalAlumno.observacionElu = ""
                    End If
                Else
                    oeEvalAlumno.estadoVerificacionElu = "P"
                    oeEvalAlumno.observacionElu = ""
                End If
                oeEvalAlumno.codUsuario = cod_user
                loEvalAlumn.Add(oeEvalAlumno)
            Next
            If loEvalAlumn.Count > 0 Then
                dt = odEvalAlumno.fc_ConfirmarEvaluaciones(loEvalAlumn)
                If dt.Rows.Count > 0 Then
                    mt_MostrarTabs(0)
                    btnListar_Click(Nothing, Nothing)
                    mt_ShowMessage("¡ Se confirmaron los datos correctamente !", MessageType.Success)
                Else
                    mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Error)
                End If
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

    Private Sub mt_CargarCicloAcademico()
        'Try
        Dim dt As Data.DataTable = fc_ListarCicloAcademico()
        mt_LlenarListas(cmbFiltroCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
        cmbFiltroCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarCentroCosto()
        'odGeneral = New d_VacantesEvento
        'mt_LlenarListas(Me.cmbFiltroCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        'mt_LlenarListas(Me.cmbCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbFiltroCentroCostos, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        With oeEvaluacion
            ._tipoOpe = "4" : ._codigo_cco = Me.cmbFiltroCentroCostos.SelectedValue
        End With
        dt = odEvaluacion.fc_Listar(oeEvaluacion)
        Me.grvEvaluaciones.DataSource = dt
        Me.grvEvaluaciones.DataBind()
        If Me.grvEvaluaciones.Rows.Count > 0 Then mt_AgruparFilas(Me.grvEvaluaciones.Rows, 0, 2)
    End Sub

    Private Sub mt_CargarRespuestas()
        Dim codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim dtAlu, dtPreg, dtAlt As Data.DataTable
        codigo_evl = Session("adm_codigo_evl")
        codigo_tev = Session("adm_codigo_tev")
        oeEvalAlumno = New e_EvaluacionAlumno : odEvalAlumno = New d_EvaluacionAlumno
        oeEvalAlumno.tipoConsulta = "EV" : oeEvalAlumno.codigoEvl = codigo_evl
        dtAlu = odEvalAlumno.fc_Listar(oeEvalAlumno)
        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
        oeEvalDetalle._tipoOpe = "1" : oeEvalDetalle._orden_evd = codigo_tev
        oeEvalDetalle._codigo_evl = codigo_evl
        dtPreg = odEvalDetalle.fc_Listar(oeEvalDetalle)
        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
        With oeAlternativas
            .tipoConsulta = "EVL" : .codigoPrv = codigo_evl
        End With
        dtAlt = odAlternativas.fc_Listar(oeAlternativas)
        Session("adm_dtAlternativas") = dtAlt
        Session("adm_dtPreguntas") = dtPreg
        Session("adm_dtRespuestas") = dtAlu
        mt_CrearColumns()
        Me.grvPostulante.DataSource = dtAlu
        Me.grvPostulante.DataBind()
    End Sub

    Private Sub mt_CrearColumns()
        Dim dtCol As Data.DataTable
        Dim dvCol As Data.DataView
        Dim tfield As New TemplateField()
        If Me.grvPostulante.Columns.Count > orden_col Then
            'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
            For i As Integer = (orden_col + 1) To Me.grvPostulante.Columns.Count
                Me.grvPostulante.Columns.RemoveAt(Me.grvPostulante.Columns.Count - 1)
            Next
        End If
        dtCol = CType(Session("adm_dtPreguntas"), Data.DataTable)
        dvCol = New Data.DataView(dtCol, "", "nro_item", Data.DataViewRowState.CurrentRows)
        dtCol = dvCol.ToTable
        If dtCol.Rows.Count = 0 Then
            tfield = New TemplateField()
            tfield.HeaderText = ""
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.grvPostulante.Columns.Add(tfield)
        Else
            For x As Integer = 0 To dtCol.Rows.Count - 1
                tfield = New TemplateField()
                tfield.HeaderText = dtCol.Rows(x).Item("nro_item").ToString.ToUpper
                'tfield.HeaderText = (x + 1)
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.grvPostulante.Columns.Add(tfield)
            Next
        End If
        ' Crear Column para operaciones
        tfield = New TemplateField()
        tfield.HeaderText = "¿CONFORME?"
        tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
        Me.grvPostulante.Columns.Add(tfield)
    End Sub

    Private Sub mt_RefreshGrid()
        For Each _Row As GridViewRow In Me.grvPostulante.Rows
            grvPostulante_OnRowDataBound(Me.grvPostulante, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    'Private Sub mt_Resultados()
    '    Dim dt As New Data.DataTable
    '    dt.Columns.Add("nro", GetType(Integer))
    '    dt.Columns.Add("codigo", GetType(String))
    '    dt.Columns.Add("dni", GetType(String))
    '    dt.Columns.Add("nombrecompleto", GetType(String))
    '    dt.Columns.Add("r1", GetType(String))
    '    dt.Columns.Add("r2", GetType(String))
    '    dt.Columns.Add("r3", GetType(String))
    '    dt.Columns.Add("r4", GetType(String))
    '    dt.Columns.Add("r5", GetType(String))
    '    dt.Columns.Add("r6", GetType(String))
    '    dt.Columns.Add("r7", GetType(String))
    '    dt.Columns.Add("r8", GetType(String))
    '    dt.Columns.Add("r9", GetType(String))
    '    dt.Columns.Add("r10", GetType(String))
    '    dt.Columns.Add("r11", GetType(String))
    '    dt.Columns.Add("r12", GetType(String))
    '    dt.Columns.Add("r13", GetType(String))
    '    dt.Columns.Add("r14", GetType(String))
    '    dt.Columns.Add("r15", GetType(String))
    '    dt.Columns.Add("r16", GetType(String))
    '    dt.Columns.Add("r17", GetType(String))
    '    dt.Columns.Add("r18", GetType(String))
    '    dt.Columns.Add("r19", GetType(String))
    '    dt.Columns.Add("r20", GetType(String))
    '    dt.Rows.Add(1, "2020001", "78909001", "POSTULANTE A", "A", "E", "A", "B", "A", "B", "A", "B", "C", "B", "C", "B", "C", "D", "B", "C", "D", "E", "D", "E")
    '    dt.Rows.Add(2, "2020002", "78909002", "POSTULANTE B", "A", "E", "A", "B", "A", "B", "A", "B", "C", "B", "C", "B", "C", "D", "B", "C", "D", "E", "D", "E")
    '    dt.Rows.Add(3, "2020003", "78909003", "POSTULANTE C", "A", "E", "A", "B", "A", "B", "A", "B", "C", "B", "C", "B", "C", "D", "B", "C", "D", "E", "D", "E")
    '    dt.Rows.Add(4, "2020004", "78909004", "POSTULANTE D", "A", "E", "A", "B", "A", "B", "A", "B", "C", "B", "C", "B", "C", "D", "B", "C", "D", "E", "D", "E")
    '    dt.Rows.Add(5, "2020005", "78909005", "POSTULANTE E", "A", "E", "A", "B", "A", "B", "A", "B", "C", "B", "C", "B", "C", "D", "B", "C", "D", "E", "D", "E")
    '    Me.grvPostulante.DataSource = dt
    '    Me.grvPostulante.DataBind()
    'End Sub

    'Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
    '    If totalColumns = 0 Then Return
    '    Dim i As Integer, count As Integer = 1
    '    Dim lst As ArrayList = New ArrayList()
    '    lst.Add(gridViewRows(0))
    '    Dim ctrl As TableCell
    '    ctrl = gridViewRows(0).Cells(startIndex)
    '    For i = 1 To gridViewRows.Count - 1
    '        Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
    '        If ctrl.Text = nextTbCell.Text Then
    '            count += 1
    '            nextTbCell.Visible = False
    '            lst.Add(gridViewRows(i))
    '        Else
    '            If count > 1 Then
    '                ctrl.RowSpan = count
    '                ctrl.VerticalAlign = VerticalAlign.Middle
    '                mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
    '            End If
    '            count = 1
    '            lst.Clear()
    '            ctrl = gridViewRows(i).Cells(startIndex)
    '            lst.Add(gridViewRows(i))
    '        End If
    '    Next
    '    If count > 1 Then
    '        ctrl.RowSpan = count
    '        ctrl.VerticalAlign = VerticalAlign.Middle
    '        mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
    '    End If
    '    count = 1
    '    lst.Clear()
    'End Sub

#End Region

#Region "Funciones"

    Private Function fc_ObetenerRespuesta(ByVal codigo_alu As Integer, ByVal item As Integer) As String
        Dim rpta As String = "-"
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Dim rpta_aux As String()
        dt = CType(Session("adm_dtRespuestas"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_alu= " & codigo_alu, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                rpta_aux = dt.Rows(0).Item("respuesta_elu").ToString.Split("|")
                If rpta_aux.Length > item Then
                    rpta = rpta_aux(item)
                End If
            End If
        End If
        Return rpta
    End Function

#End Region

End Class
