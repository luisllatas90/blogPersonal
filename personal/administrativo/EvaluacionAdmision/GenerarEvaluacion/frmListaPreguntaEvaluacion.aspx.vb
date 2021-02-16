Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsGenerarEvaluacion
Imports ClsBancoPreguntas
Imports System.Collections.Generic

Partial Class GenerarEvaluacion_frmListaPreguntaEvaluacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeEvaluacion As e_Evaluacion, odEvaluacion As d_Evaluacion
    Private oeEvalDetalle As e_EvaluacionDetalle, odEvalDetalle As d_EvaluacionDetalle
    Private oePreguntas As e_PreguntaEvaluacion, odPreguntas As d_PreguntaEvaluacion
    Private oeAlternativas As e_AlternativaEvaluacion, odAlternativas As d_AlternativaEvaluacion
    Private odGeneral As d_VacantesEvento
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
                mt_CargarCicloAcademico()
                'mt_CargarCentroCosto()
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
            If Me.cmbCco.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvEvaluacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvEvaluacion.RowCommand
        Dim index As Integer = -1, codigo_evl As Integer = -1, codigo_tev As Integer = -1
        Dim nombre_evl As String = ""
        Dim dt As Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_evl = Me.grvEvaluacion.DataKeys(index).Values("codigo_evl")
                nombre_evl = Me.grvEvaluacion.DataKeys(index).Values("nombre_evl")
                codigo_tev = Me.grvEvaluacion.DataKeys(index).Values("codigo_tev")
                Select Case e.CommandName
                    Case "Editar"
                        oePreguntas = New e_PreguntaEvaluacion : odPreguntas = New d_PreguntaEvaluacion
                        With oePreguntas
                            .tipoConsulta = "EVL" : .codigoInd = codigo_evl : .codigoNcp = 0 : .cantidadPrv = 0
                        End With
                        dt = odPreguntas.fc_Listar(oePreguntas)
                        Session("adm_dtPreguntas") = dt
                        oeAlternativas = New e_AlternativaEvaluacion : odAlternativas = New d_AlternativaEvaluacion
                        With oeAlternativas
                            .tipoConsulta = "EVL" : .codigoPrv = codigo_evl
                        End With
                        dt = odAlternativas.fc_Listar(oeAlternativas)
                        Session("adm_dtAlternativas") = dt
                        oeEvalDetalle = New e_EvaluacionDetalle : odEvalDetalle = New d_EvaluacionDetalle
                        With oeEvalDetalle
                            ._tipoOpe = "2" : ._codigo_evl = codigo_evl
                        End With
                        dt = odEvalDetalle.fc_Listar(oeEvalDetalle)
                        Session("adm_dtConformidades") = dt
                        Session("adm_item") = 1
                        Session("adm_codigo_evd") = -1
                        Session("adm_codigo_evl") = codigo_evl
                        Session("adm_nombre_evl") = nombre_evl
                        mt_MostrarPregunta()
                        mt_MostrarConformidad()
                        Me.txtSiguiente.InnerText = "Siguiente"
                        Me.txtItem.InnerText = "Revisión de Pregunta N°: 1"
                        Me.btnAnterior.Disabled = True
                        mt_MostrarTabs(1)
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSiguiente_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dtPreg As New Data.DataTable
        Dim item As Integer = -1
        item = Session("adm_item")
        dtPreg = CType(Session("adm_dtPreguntas"), Data.DataTable)
        If Me.rbConforme.Checked Or Me.rbNoConforme.Checked Then
            If Me.rbNoConforme.Checked And Me.txtObservacion.Text.Trim = "" Then
                Session("adm_item") = item
                mt_MostrarPregunta()
                mt_ShowMessage("¡ Ingrese Observacion de No Conformidad !", MessageType.Warning)
            Else
                mt_ActualizaConformidad()
                If item = dtPreg.Rows.Count Then
                    mt_Guardar()
                    Session("adm_item") = 0
                    mt_MostrarTabs(0)
                Else
                    Me.btnAnterior.Disabled = False
                    If item = (dtPreg.Rows.Count - 1) Then
                        Me.txtSiguiente.InnerText = "Finalizar"
                    End If
                    Session("adm_item") = item + 1
                    mt_MostrarPregunta()
                    Me.rbConforme.Checked = False
                    Me.rbNoConforme.Checked = False
                    Me.txtObservacion.Text = ""
                    mt_MostrarConformidad()
                End If
            End If
        Else
            Session("adm_item") = item
            mt_MostrarPregunta()
            mt_ShowMessage("¡ Indique la conformidad de la pregunta !", MessageType.Warning)
        End If
    End Sub

    Protected Sub btnAnterior_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim item As Integer = -1
        item = Session("adm_item")
        If item = 2 Then
            Me.btnAnterior.Disabled = True
        End If
        Me.txtSiguiente.InnerText = "Siguiente"
        Session("adm_item") = item - 1
        mt_MostrarPregunta()
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
        'mt_LlenarListas(Me.cmbCco, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbCco, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        With oeEvaluacion
            ._tipoOpe = "2" : ._codigo_cco = Me.cmbCco.SelectedValue
        End With
        dt = odEvaluacion.fc_Listar(oeEvaluacion)
        Me.grvEvaluacion.DataSource = dt
        Me.grvEvaluacion.DataBind()
        If Me.grvEvaluacion.Rows.Count > 0 Then mt_AgruparFilas(Me.grvEvaluacion.Rows, 0, 2)
    End Sub

    Private Sub mt_MostrarPregunta()
        Dim dtPreg, dtAlt As New Data.DataTable
        Dim dvPreg, dvAlt As Data.DataView
        Dim divOl As New Literal
        Dim item As Integer = 0
        dtPreg = CType(Session("adm_dtPreguntas"), Data.DataTable)
        If dtPreg.Rows.Count > 0 Then
            dvPreg = New Data.DataView(dtPreg, "", "nro_item", Data.DataViewRowState.CurrentRows)
            dtPreg = dvPreg.ToTable
            If dtPreg.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtPreg.Rows
                    item += 1
                    If item = Session("adm_item") Then
                        Me.txtItem.InnerText = "Revisión de Pregunta N°: " & Session("adm_item")
                        Session("adm_codigo_evd") = row.Item("codigo_evd")
                        divOl.Text += " <div class='panel panel-default'>"
                        divOl.Text += "     <div class='panel-heading'>"
                        divOl.Text += "         <h4 class='panel-title'>"
                        divOl.Text += "         " & ClsGlobales.fc_DesencriptaTexto64(row.Item("texto_prv"))
                        divOl.Text += "         </h4>"
                        divOl.Text += "     </div>"
                        dtAlt = CType(Session("adm_dtAlternativas"), Data.DataTable)
                        If dtAlt.Rows.Count > 0 Then
                            dvAlt = New Data.DataView(dtAlt, "codigo_prv = " & row.Item("codigo_prv"), "", Data.DataViewRowState.CurrentRows)
                            dtAlt = dvAlt.ToTable
                            If dtAlt.Rows.Count > 0 Then
                                divOl.Text += " <div class='panel-body'>"
                                divOl.Text += "     <ol class='list-group'>"
                                For Each fila As Data.DataRow In dtAlt.Rows
                                    If fila.Item("correcta_ale") = 1 Then
                                        divOl.Text += "     <li class='list-group-item list-group-item-success'>"
                                    Else
                                        divOl.Text += "     <li class='list-group-item'>"
                                    End If
                                    divOl.Text += "     " & ClsGlobales.fc_DesencriptaTexto64(fila.Item("texto_ale"))
                                    divOl.Text += "     </li>"
                                Next
                                divOl.Text += "     </ol>"
                                divOl.Text += " </div>"
                            End If
                        End If
                        divOl.Text += " </div>"
                        Me.divSelectPregunta.Controls.Add(divOl)
                        Exit For
                    End If
                Next
                'Dim row As Data.DataRow
                'row = dtPreg.Rows(0)
            End If
        End If
    End Sub

    Private Sub mt_ActualizaConformidad()
        Dim dtEvd As New Data.DataTable
        dtEvd = CType(Session("adm_dtConformidades"), Data.DataTable)
        If dtEvd.Rows.Count > 0 Then
            For Each fil As Data.DataRow In dtEvd.Rows
                If fil.Item("codigo_evd") = Session("adm_codigo_evd") Then
                    If Me.rbConforme.Checked Then
                        fil.Item("estadovalidacion_evd") = "C"
                    End If
                    If Me.rbNoConforme.Checked Then
                        fil.Item("estadovalidacion_evd") = "O"
                        fil.Item("descripcion_edo") = Me.txtObservacion.Text.Trim
                    End If
                    Exit For
                End If
            Next
        End If
        Session("adm_dtConformidades") = dtEvd
    End Sub

    Public Sub mt_Guardar()
        Dim dtEvd As New Data.DataTable
        Dim oeEdo As e_EvaluacionDetalle_Observacion
        Dim nro_conf As Integer = -1
        dtEvd = CType(Session("adm_dtConformidades"), Data.DataTable)
        oeEvaluacion = New e_Evaluacion : odEvaluacion = New d_Evaluacion
        If dtEvd.Rows.Count > 0 Then
            oeEvaluacion._codigo_evl = Session("adm_codigo_evl")
            oeEvaluacion._nombre_evl = Session("adm_nombre_evl")
            oeEvaluacion._codigo_per = cod_user
            oeEvaluacion._leEdit = New List(Of e_EvaluacionDetalle)
            For Each fila As Data.DataRow In dtEvd.Rows
                If fila.Item("estadovalidacion_evd") <> "P" Then
                    If fila.Item("estadovalidacion_evd") = "C" Then nro_conf += 1
                    'If fila.Item("codigo_edo") = -1 Then
                    oeEvalDetalle = New e_EvaluacionDetalle
                    oeEvalDetalle._codigo_evd = fila.Item("codigo_evd")
                    oeEvalDetalle._codigo_prv = fila.Item("codigo_prv")
                    oeEvalDetalle._estadovalidacion_evd = fila.Item("estadovalidacion_evd")
                    oeEvalDetalle._codigo_per = cod_user
                    If fila.Item("estadovalidacion_evd") = "O" Then
                        oeEvalDetalle._oeObservacion = New e_EvaluacionDetalle_Observacion
                        oeEdo = New e_EvaluacionDetalle_Observacion
                        oeEdo._codigo_edo = fila.Item("codigo_edo")
                        oeEdo._codigo_evd = fila.Item("codigo_evd")
                        oeEdo._descripcion_edo = fila.Item("descripcion_edo")
                        oeEdo._codigo_per = cod_user
                        oeEvalDetalle._oeObservacion = oeEdo
                    End If
                    oeEvaluacion._leEdit.Add(oeEvalDetalle)
                    'End If
                End If
            Next
            oeEvaluacion._estadovalidacion_evl = IIf(nro_conf = dtEvd.Rows.Count, "C", "O")
            odEvaluacion.fc_Actualizar(oeEvaluacion)
            mt_ShowMessage("¡ Se actualizaron los datos correctamente !", MessageType.Success)
            btnListar_Click(Nothing, Nothing)
        End If
    End Sub

    Public Sub mt_MostrarConformidad()
        Dim dtEvd As New Data.DataTable
        dtEvd = CType(Session("adm_dtConformidades"), Data.DataTable)
        If dtEvd.Rows.Count > 0 Then
            For Each fil As Data.DataRow In dtEvd.Rows
                If fil.Item("codigo_evd") = Session("adm_codigo_evd") Then
                    If fil.Item("estadovalidacion_evd") <> "P" Then
                        If fil.Item("estadovalidacion_evd") = "C" Then
                            Me.rbConforme.Checked = True
                        Else
                            Me.rbNoConforme.Checked = True
                            Me.txtObservacion.Text = fil.Item("descripcion_edo")
                        End If
                    End If
                    Exit For
                End If
            Next
        End If
    End Sub


#End Region

End Class
