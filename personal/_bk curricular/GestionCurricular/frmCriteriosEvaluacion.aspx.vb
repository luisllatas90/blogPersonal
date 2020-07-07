﻿
Partial Class GestionCurricular_frmCriteriosEvaluacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer

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
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            'cod_user = Request.QueryString("id")
            cod_user = Session("id_per")
            Session("gc_peso_res") = 0
            Session("gc_peso_ind") = 0
            If IsPostBack = False Then
                Session("gc_dtAsignatura03") = Nothing
                mt_CargarSemestre()
                'Session("gc_peso_res") = 0
                If Not String.IsNullOrEmpty(Session("gc_codigo_dis")) Then
                    If Me.cboSemestre.Items.Count > 0 Then Me.cboSemestre.SelectedValue = Session("gc_codigo_cac") : mt_CargarCarreraProfesional(Session("gc_codigo_cac"))
                    If Me.cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = Session("gc_codigo_cpf") : mt_CargarPlanEstudio(Session("gc_codigo_cac"), Session("gc_codigo_cpf"))
                    If Me.cboPlanEstudio.Items.Count > 0 Then Me.cboPlanEstudio.SelectedValue = Session("gc_codigo_pes") : mt_CargarDiseñoAsignatura(Session("gc_codigo_cac"), Session("gc_codigo_cpf"), Session("gc_codigo_pes"))
                    If Me.cboDisApr.Items.Count > 0 Then Me.cboDisApr.SelectedValue = Session("gc_codigo_dis") : mt_CargarDatos(IIf(Session("gc_codigo_dis") = -1, 0, Session("gc_codigo_dis")))
                    Me.cboSemestre.Enabled = False
                    Me.cboCarrProf.Enabled = False
                    Me.cboPlanEstudio.Enabled = False
                    Me.cboDisApr.Enabled = False
                    Me.btnBack.Visible = True
                    Me.btnSeguir.Visible = True
                    Me.lblCurso.InnerText = "Configurar Criterios de Evaluación de Asignatura: " & Session("gc_nombre_cur")
                Else
                    Me.cboSemestre.Enabled = True
                    Me.cboCarrProf.Enabled = True
                    Me.cboPlanEstudio.Enabled = True
                    Me.cboDisApr.Enabled = True
                    Me.btnBack.Visible = False
                    Me.btnSeguir.Visible = False
                End If

            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            mt_CargarCarreraProfesional(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            If Me.gvResultados.Rows.Count > 0 Then Me.gvResultados.DataSource = Nothing : Me.gvResultados.DataBind()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue))
            If Me.gvResultados.Rows.Count > 0 Then Me.gvResultados.DataSource = Nothing : Me.gvResultados.DataBind()
            If Me.cboDisApr.Items.Count > 0 Then Me.cboDisApr.SelectedIndex = 0
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEstudio.SelectedIndexChanged
        Try
            mt_CargarDiseñoAsignatura(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue), IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), IIf(Me.cboPlanEstudio.SelectedValue = -1, 0, Me.cboPlanEstudio.SelectedValue))
            If Me.gvResultados.Rows.Count > 0 Then Me.gvResultados.DataSource = Nothing : Me.gvResultados.DataBind()
        Catch ex As Exception
            mt_ShowMessage (ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboDisApr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDisApr.SelectedIndexChanged
        Try
            'If Me.gvResultados.Rows.Count > 0 Then Me.gvResultados.DataSource = Nothing : Me.gvResultados.DataBind()
            mt_CargarDatos(IIf(Me.cboDisApr.SelectedValue = -1, 0, Me.cboDisApr.SelectedValue))
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
    '    Try
    '        mt_CargarDatos(Me.cboDisApr.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub btnEditResultado_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Resultado" & "','" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnEditIndicador_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Indicador" & "','" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnAddIndicador_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'mt_ShowMessage(Session("gc_peso_ind"), MessageType.Warning)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Indicador" & "','" & "Agregar" & "');</script>")
    End Sub

    Protected Sub btnEditEvidencia_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Evidencia" & "','" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnAddEvidencia_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Evidencia" & "','" & "Agregar" & "');</script>")
    End Sub

    Protected Sub btnEditInstrumento_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Instrumento" & "','" & "Editar" & "');</script>")
    End Sub

    Protected Sub btnAddInstrumento_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Instrumento" & "','" & "Agregar" & "');</script>")
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Dim obj As New ClsConectarDatos
        Dim index, tipo_prom, tipo_prom2, codigo_eviIns, codigo_indEvi, codigo_ind, x As Integer
        Dim codigox, codigox_aux, codigoy, codigoy_aux As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            index = CInt(e.CommandArgument)
            Session("gc_codigo_uni") = Me.gvResultados.DataKeys(index).Values("codigo_uni")
            Session("gc_codigo_res") = Me.gvResultados.DataKeys(index).Values("codigo_res")
            tipo_prom = CInt(Me.gvResultados.DataKeys(index).Values("tipo_prom").ToString)
            tipo_prom2 = CInt(Me.gvResultados.DataKeys(index).Values("tipo_prom2").ToString)
            Select Case e.CommandName
                Case "EditResultado"
                    Session("gc_peso_res") = 0
                    codigox = Me.gvResultados.DataKeys(index).Values("codigo_res")
                    codigox_aux = 0
                    For x = 0 To Me.gvResultados.Rows.Count - 1
                        If codigox_aux <> CInt(Me.gvResultados.DataKeys(x).Values("codigo_res")) Then
                            codigox_aux = Me.gvResultados.DataKeys(x).Values("codigo_res")
                            If codigox <> codigox_aux Then Session("gc_peso_res") = CDbl(Session("gc_peso_res")) + CDbl(Me.gvResultados.DataKeys(x).Values("peso_res") * 100)
                        End If
                    Next
                    Me.txtResultado.Text = Me.gvResultados.DataKeys(index).Values("descripcion_res")
                    Me.txtPorcenRes.Text = (Me.gvResultados.DataKeys(index).Values("peso_res") * 100)
                    Me.cboTipoProm.SelectedValue = IIf(Me.gvResultados.DataKeys(index).Values("tipo_prom") <> 2, 1, Me.gvResultados.DataKeys(index).Values("tipo_prom"))
                Case "AddIndicador"
                    Session("gc_peso_ind") = 0
                    codigox = Me.gvResultados.DataKeys(index).Values("codigo_res")
                    codigoy = -1
                    codigoy_aux = 0 : codigox_aux = 0
                    For x = 0 To Me.gvResultados.Rows.Count - 1
                        If codigox = CInt(Me.gvResultados.DataKeys(x).Values("codigo_res")) Then
                            If codigoy_aux <> CInt(Me.gvResultados.DataKeys(x).Values("codigo_ind")) Then
                                codigoy_aux = Me.gvResultados.DataKeys(x).Values("codigo_ind")
                                Session("gc_peso_ind") = CDbl(Session("gc_peso_ind")) + CDbl(Me.gvResultados.DataKeys(x).Values("peso_ind") * 100)
                            End If
                        End If
                    Next
                    Session("gc_codigo_ind") = ""
                    Me.txtPorcentaje.Enabled = IIf(tipo_prom = 2, True, False)
                    Me.cboTipoProm2.SelectedValue = 1 ' IIf(Me.gvResultados.DataKeys(index).Values("tipo_prom2") <> 2, 1, Me.gvResultados.DataKeys(index).Values("tipo_prom2"))
                    'mt_ShowMessage(Session("gc_peso_ind"), MessageType.Warning)
                Case "EditIndicador"
                    Session("gc_peso_ind") = 0
                    codigox = Me.gvResultados.DataKeys(index).Values("codigo_res")
                    codigoy = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    codigoy_aux = 0 : codigox_aux = 0
                    For x = 0 To Me.gvResultados.Rows.Count - 1
                        codigox_aux = Me.gvResultados.DataKeys(x).Values("codigo_res")
                        If codigoy_aux <> CInt(Me.gvResultados.DataKeys(x).Values("codigo_ind")) Then
                            codigoy_aux = Me.gvResultados.DataKeys(x).Values("codigo_ind")
                            If codigox = codigox_aux And codigoy <> codigoy_aux Then Session("gc_peso_ind") = CDbl(Session("gc_peso_ind")) + CDbl(Me.gvResultados.DataKeys(x).Values("peso_ind") * 100)
                        End If
                    Next
                    Session("gc_codigo_ind") = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    Me.txtIndicador.Text = Me.gvResultados.DataKeys(index).Values("descripcion_ind")
                    Me.txtPorcentaje.Text = (Me.gvResultados.DataKeys(index).Values("peso_ind") * 100)
                    Me.txtPorcentaje.Enabled = IIf(tipo_prom = 2, True, False)
                    Me.cboTipoProm2.SelectedValue = Me.gvResultados.DataKeys(index).Values("tipo_prom2")
                Case "AddEvidencia"
                    Session("gc_codigo_indEvi") = ""
                    Session("gc_codigo_evi") = ""
                    Session("gc_codigo_ind") = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                Case "EditEvidencia"
                    Session("gc_codigo_indEvi") = Me.gvResultados.DataKeys(index).Values("codigo_indEvi")
                    Session("gc_codigo_evi") = Me.gvResultados.DataKeys(index).Values("codigo_evi")
                    Session("gc_codigo_ind") = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    Me.txtEvidencia.Text = Me.gvResultados.DataKeys(index).Values("descripcion_evi")
                Case "AddInstrumento"
                    Session("gc_codigo_eviIns") = ""
                    Session("gc_codigo_ins") = ""
                    Session("codigo_indEvi") = Me.gvResultados.DataKeys(index).Values("codigo_indEvi")
                    Session("gc_codigo_evi") = Me.gvResultados.DataKeys(index).Values("codigo_evi")
                    Session("gc_codigo_ind") = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    Session("gc_peso_ins") = 0
                    codigox = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    codigoy = -1
                    codigoy_aux = 0 : codigox_aux = 0
                    For x = 0 To Me.gvResultados.Rows.Count - 1
                        If codigox = CInt(Me.gvResultados.DataKeys(x).Values("codigo_ind")) Then
                            If codigoy_aux <> CInt(Me.gvResultados.DataKeys(x).Values("codigo_ins")) Then
                                codigoy_aux = Me.gvResultados.DataKeys(x).Values("codigo_ins")
                                Session("gc_peso_ins") = CDbl(Session("gc_peso_ins")) + CDbl(Me.gvResultados.DataKeys(x).Values("peso_ins") * 100)
                            End If
                        End If
                    Next
                    Me.txtPorcenIns.Enabled = IIf(tipo_prom2 = 2, True, False)
                Case "EditInstrumento"
                    Session("gc_codigo_eviIns") = Me.gvResultados.DataKeys(index).Values("codigo_eviIns")
                    Session("gc_codigo_ins") = Me.gvResultados.DataKeys(index).Values("codigo_ins")
                    Session("gc_codigo_indEvi") = Me.gvResultados.DataKeys(index).Values("codigo_indEvi")
                    Session("gc_codigo_evi") = Me.gvResultados.DataKeys(index).Values("codigo_evi")
                    Session("gc_codigo_ind") = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    Session("gc_peso_ins") = 0
                    codigox = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    codigoy = Me.gvResultados.DataKeys(index).Values("codigo_ins")
                    codigoy_aux = 0 : codigox_aux = 0
                    For x = 0 To Me.gvResultados.Rows.Count - 1
                        codigox_aux = Me.gvResultados.DataKeys(x).Values("codigo_ind")
                        If codigoy_aux <> CInt(Me.gvResultados.DataKeys(x).Values("codigo_ins")) Then
                            codigoy_aux = Me.gvResultados.DataKeys(x).Values("codigo_ins")
                            If codigox = codigox_aux And codigoy <> codigoy_aux Then Session("gc_peso_ins") = CDbl(Session("gc_peso_ins")) + CDbl(Me.gvResultados.DataKeys(x).Values("peso_ins") * 100)
                        End If
                    Next
                    Me.txtInstrumento.Text = Me.gvResultados.DataKeys(index).Values("descripcion_ins")
                    Me.txtPorcenIns.Text = (Me.gvResultados.DataKeys(index).Values("peso_ins") * 100)
                    Me.txtPorcenIns.Enabled = IIf(tipo_prom2 = 2, True, False)
                Case "DeleteInstrumento"
                    codigo_eviIns = Me.gvResultados.DataKeys(index).Values("codigo_eviIns")
                    obj.AbrirConexion()
                    obj.Ejecutar("EvidenciaInstrumento_eliminar", codigo_eviIns, cod_user)
                    obj.CerrarConexion()
                    mt_ShowMessage("¡ Se eliminó el instrumento correctamente !", MessageType.Success)
                    mt_CargarDatos(cboDisApr.SelectedValue)
                Case "DeleteEvidencia"
                    codigo_indEvi = Me.gvResultados.DataKeys(index).Values("codigo_indEvi")
                    obj.AbrirConexion()
                    obj.Ejecutar("IndicadorEvidencia_eliminar", codigo_indEvi, cod_user)
                    obj.CerrarConexion()
                    mt_ShowMessage("¡ Se eliminó la evidencia correctamente !", MessageType.Success)
                    mt_CargarDatos(cboDisApr.SelectedValue)
                Case "DeleteIndicador"
                    codigo_ind = Me.gvResultados.DataKeys(index).Values("codigo_ind")
                    obj.AbrirConexion()
                    obj.Ejecutar("IndicadorAprendizaje_eliminar", codigo_ind, cod_user)
                    obj.CerrarConexion()
                    mt_ShowMessage("¡ Se eliminó el indicador correctamente !", MessageType.Success)
                    mt_CargarDatos(cboDisApr.SelectedValue)
            End Select
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabarInd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarInd.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            If String.IsNullOrEmpty(Session("gc_codigo_ind")) Then
                obj.Ejecutar("IndicadorAprendizaje_insertar", Session("gc_codigo_res"), Me.txtIndicador.Text.Trim, CDbl(IIf(Me.txtPorcentaje.Text.Trim = "", 0, Me.txtPorcentaje.Text)) / 100, Me.cboTipoProm2.SelectedValue, cod_user)
                mt_ShowMessage("¡ Se guardó el indicador correctamente !", MessageType.Success)
            Else
                obj.Ejecutar("IndicadorAprendizaje_actualizar", Session("gc_codigo_ind"), Session("gc_codigo_res"), Me.txtIndicador.Text.Trim, CDbl(IIf(Me.txtPorcentaje.Text.Trim = "", 0, Me.txtPorcentaje.Text)) / 100, Me.cboTipoProm2.SelectedValue, cod_user)
                mt_ShowMessage("¡ Se editó el indicador correctamente !", MessageType.Success)
            End If
            obj.CerrarConexion()
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabarEvi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarEvi.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            If String.IsNullOrEmpty(Session("gc_codigo_indEvi")) Then
                obj.Ejecutar("IndicadorEvidencia_insertar", Session("gc_codigo_ind"), -1, Me.txtEvidencia.Text.Trim, cod_user)
                mt_ShowMessage("¡ Se guardó la evidencia correctamente !", MessageType.Success)
            Else
                obj.Ejecutar("IndicadorEvidencia_actualizar", Session("gc_codigo_indEvi"), Session("gc_codigo_ind"), Session("gc_codigo_evi"), Me.txtEvidencia.Text.Trim, cod_user)
                mt_ShowMessage("¡ Se editó la evidencia correctamente !", MessageType.Success)
            End If
            obj.CerrarConexion()
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabarIns_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarIns.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            If String.IsNullOrEmpty(Session("gc_codigo_eviIns")) Then
                obj.Ejecutar("EvidenciaInstrumento_insertar", Session("gc_codigo_evi"), -1, Me.txtInstrumento.Text.Trim, CDbl(IIf(Me.txtPorcenIns.Text.Trim = "", 0, Me.txtPorcenIns.Text)) / 100, cod_user)
                mt_ShowMessage("¡ Se guardó el instrumento correctamente !", MessageType.Success)
            Else
                obj.Ejecutar("EvidenciaInstrumento_actualizar", Session("gc_codigo_eviIns"), Session("gc_codigo_evi"), Session("gc_codigo_ins"), Me.txtInstrumento.Text.Trim, CDbl(IIf(Me.txtPorcenIns.Text.Trim = "", 0, Me.txtPorcenIns.Text)) / 100, cod_user)
                mt_ShowMessage("¡ Se editó el instrumento correctamente !", MessageType.Success)
            End If
            obj.CerrarConexion()
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabarRes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarRes.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("COM_RegistrarResultados", Session("gc_codigo_res"), Session("gc_codigo_uni"), Me.txtResultado.Text.Trim, (CDbl(Me.txtPorcenRes.Text) / 100), Me.cboTipoProm.SelectedValue, cod_user)
            mt_ShowMessage("¡ Se editó el resultado de la unidad correctamente !", MessageType.Success)
            obj.CerrarConexion()
            mt_CargarDatos(Me.cboDisApr.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub gvResultados_OnDataBound(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
    '    Dim cell As New TableHeaderCell()
    '    cell.Text = "Resultados de Aprendizaje"
    '    cell.ColumnSpan = 2
    '    row.Controls.Add(cell)

    '    cell = New TableHeaderCell()
    '    cell.ColumnSpan = 2
    '    cell.Text = "Indicadores de Aprendizaje"
    '    row.Controls.Add(cell)

    '    cell = New TableHeaderCell()
    '    cell.ColumnSpan = 2
    '    cell.Text = "Evidencias de Aprendizaje"
    '    row.Controls.Add(cell)

    '    cell = New TableHeaderCell()
    '    cell.ColumnSpan = 2
    '    cell.Text = "Instrumentos de Aprendizaje"
    '    row.Controls.Add(cell)

    '    'row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
    '    Me.gvResultados.HeaderRow.Parent.Controls.AddAt(0, row)
    'End Sub

    Protected Sub gvResultados_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                AddMergedCells(objgridviewrow, objtablecell, 1, "Unidad", "#D9534F")
                AddMergedCells(objgridviewrow, objtablecell, 3, "Resultado de Aprendizaje", "#D9534F")
                AddMergedCells(objgridviewrow, objtablecell, 4, "Indicadores de Aprendizaje", "#D9534F")
                AddMergedCells(objgridviewrow, objtablecell, 3, "Evidencia de Evaluación", "#D9534F")
                AddMergedCells(objgridviewrow, objtablecell, 4, "Instrumento de Evaluación", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim cell As TableCell = New TableCell()
                'cell.Style.Add("background-color", System.Drawing.Color.LightSkyBlue.Name)
                'If e.Row.RowIndex = 0 Then
                '    e.Row.BackColor = Drawing.Color.AliceBlue
                'End If
                'If e.Row.Cells("peso_ind").Text = "1.00" Then
                '    e.Row.ForeColor = Drawing.Color.Red
                'End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultados.RowDataBound
        Try
            Dim tipo_prom, tipo_prom2 As Integer
            Dim peso_res, peso_ind, peso_ins As Double
            Dim codigo_ind, codigo_ins As Integer
            'Dim dv As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            tipo_prom = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("tipo_prom")
            tipo_prom2 = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("tipo_prom2")
            peso_res = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_res")
            peso_ind = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_ind")
            peso_ins = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("peso_ins")
            codigo_ind = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("codigo_ind")
            codigo_ins = Me.gvResultados.DataKeys(e.Row.RowIndex).Values("codigo_ins")
            If e.Row.RowType = DataControlRowType.DataRow Then
                If tipo_prom = 2 Then
                    e.Row.Cells(6).Text = CStr(peso_ind * 100) & " %"
                Else
                    e.Row.Cells(6).Text = IIf(codigo_ind = -1, "", "Promedio Simple")
                End If
                If tipo_prom2 = 2 Then
                    e.Row.Cells(13).Text = CStr(peso_ins * 100) & " %"
                Else
                    e.Row.Cells(13).Text = IIf(codigo_ins = -1, "", "Promedio Simple")
                End If
                e.Row.Cells(2).Text = CStr(peso_res * 100) & " %"
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnImportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportar.Click
    '    Dim obj As New ClsConectarDatos
    '    Dim dt, dtAux As New Data.DataTable
    '    Dim codigo_cpf, codigo_pes, codigo_cur As Integer
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "", Me.cboDisApr.SelectedValue, -1, -1, -1, -1)
    '        obj.CerrarConexion()
    '        If dt.Rows.Count > 0 Then
    '            codigo_pes = CInt(dt.Rows(0).Item(1))
    '            codigo_cur = CInt(dt.Rows(0).Item(3))
    '            codigo_cpf = CInt(dt.Rows(0).Item(6))
    '            obj.AbrirConexion()
    '            dtAux = obj.TraerDataTable("PlanCursoSumilla_Listar", "TG", codigo_cpf, codigo_pes, codigo_cur)
    '            obj.CerrarConexion()
    '            Session("gc_dtAsignatura03") = dtAux
    '            Me.gvImportar.DataSource = CType(Session("gc_dtAsignatura03"), Data.DataTable)
    '        Else
    '            Me.gvImportar.DataSource = Nothing
    '        End If
    '        'Me.gvImportar.Caption = "<label>" & codigo_cpf & " " & codigo_pes & " " & codigo_cur & "</label>"
    '        Me.gvImportar.DataBind()
    '        Page.RegisterStartupScript("Pop", "<script>openModal('" & "Importar" & "','" & "Agregar" & "');</script>")

    '        'Dim clsPdf As New clsGenerarPDF
    '        'Dim codigo_cup As Integer
    '        'Dim memory As New System.IO.MemoryStream
    '        'codigo_cup = 563661
    '        ''mt_GenerarSilabo(codigo_cup)
    '        'clsPdf.mt_GenerarSilabo(codigo_cup, Server.MapPath(".") & "/logo_usat.png", memory)
    '        'Dim bytes() As Byte = memory.ToArray
    '        'memory.Close()
    '        'Response.Clear()
    '        'Response.ContentType = "application/pdf"
    '        'Response.AddHeader("content-length", bytes.Length.ToString())
    '        'Response.BinaryWrite(bytes)

    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim x, y, codigo_pes As Integer
        Dim chk As CheckBox
        Dim dt As New Data.DataTable
        Me.hdselec.Value = ""
        dt = CType(Session("gc_dtAsignatura03"), Data.DataTable)
        For x = 0 To Me.gvImportar.Rows.Count - 1
            chk = Me.gvImportar.Rows(x).FindControl("chkSelect")
            codigo_pes = Me.gvImportar.DataKeys(x).Values("codigo_pes")
            For y = 0 To dt.Rows.Count - 1
                'Me.gvAsignaturaGen.Caption = "<label>" + dt.Rows(y).Item(0).ToString + " " + dt.Rows(y).Item(4).ToString + " " + dt.Rows(y).Item(5).ToString + " " + dt.Rows(y).Item(6).ToString + "</label>"
                If codigo_pes = CInt(dt.Rows(y).Item(0).ToString) Then
                    dt.Rows(y).Item(6) = IIf(chk.Checked, 1, 0)
                    'Me.gvAsignaturaGen.Caption = "<label>" + dt.Rows(y).Item(0).ToString + "</label>"
                    Exit For
                End If
            Next
        Next
        Session("gc_dtAsignatura03") = dt
        Me.panModal.Update()
    End Sub

    Protected Sub gvImportar_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvImportar.PageIndexChanging
        Me.gvImportar.DataSource = CType(Session("gc_dtAsignatura03"), Data.DataTable)
        Me.gvImportar.DataBind()
        Me.gvImportar.PageIndex = e.NewPageIndex
        Me.gvImportar.DataBind()
        Me.panModal.Update()
    End Sub

    Protected Sub btnGrabarImp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabarImp.Click
        Dim obj As New ClsConectarDatos
        Dim x, codigo_pes, codigo_cur As Integer
        Dim chk As CheckBox
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            For x = 0 To Me.gvImportar.Rows.Count - 1
                chk = Me.gvImportar.Rows(x).FindControl("chkSelect")
                If chk.Checked Then
                    codigo_pes = Me.gvImportar.DataKeys(x).Values("codigo_pes")
                    codigo_cur = Me.gvImportar.DataKeys(x).Values("codigo_Cur")
                    obj.AbrirConexion()
                    obj.Ejecutar("DiseñoAsignatura_clonar", Me.cboDisApr.SelectedValue, Me.cboSemestre.SelectedValue, codigo_pes, codigo_cur, cod_user)
                    obj.CerrarConexion()
                End If
            Next
            mt_ShowMessage("Se importo correctamente", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/FrmResultadoAprendizaje.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSeguir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSeguir.Click
        Dim i, codigo_res, codigo_ind, codigo_ins, tipo_prom, tipo_prom2 As Integer
        Dim peso_res, peso_ind, peso_ins As Double
        Dim nombre_res, nombre_ind, nombre_evi, nombre_ins As String
        Try
            codigo_res = -1 : codigo_ind = -1 : peso_res = 0
            nombre_res = String.Empty : nombre_ind = String.Empty : nombre_evi = String.Empty : nombre_ins = String.Empty
            For i = 0 To Me.gvResultados.Rows.Count - 1
                If codigo_res <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_res")) Then
                    If i > 0 And tipo_prom = 2 And Math.Round(peso_ind, 2) <> 1 Then
                        Throw New Exception("¡ El Resultado de Aprendizaje: " & nombre_res & ", la suma de los pesos de sus indicadores no es 100% !")
                    End If
                    codigo_res = CInt(Me.gvResultados.DataKeys(i).Values("codigo_res"))
                    nombre_res = Me.gvResultados.DataKeys(i).Values("descripcion_res")
                    tipo_prom = CInt(Me.gvResultados.DataKeys(i).Values("tipo_prom"))
                    If CDbl(Me.gvResultados.DataKeys(i).Values("peso_res")) = 0.0 Then
                        Throw New Exception("¡ El Resultado de Aprendizaje: " & nombre_res & ", no tiene peso !")
                    End If
                    peso_res += CDbl(Me.gvResultados.DataKeys(i).Values("peso_res"))
                    peso_ind = 0
                End If
                If CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind")) = -1 Then
                    Throw New Exception("¡ El Resultado de Aprendizaje: " & nombre_res & ", no se ha registrado un indicador !")
                End If
                'nombre_ind = Me.gvResultados.DataKeys(i).Values("descripcion_ind")
                If codigo_ind <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind")) Then
                    If i > 0 And tipo_prom2 = 2 And Math.Round(peso_ins, 2) <> 1 Then
                        Throw New Exception("¡ El Indicador de Aprendizaje: " & nombre_ind & ", la suma de los pesos de sus instrumentos no es 100% !")
                    End If
                    codigo_ind = CInt(Me.gvResultados.DataKeys(i).Values("codigo_ind"))
                    nombre_ind = Me.gvResultados.DataKeys(i).Values("descripcion_ind")
                    tipo_prom2 = CInt(Me.gvResultados.DataKeys(i).Values("tipo_prom2"))
                    peso_ins = 0
                    If tipo_prom = 2 Then
                        If CDbl(Me.gvResultados.DataKeys(i).Values("peso_ind")) = 0.0 Then
                            Throw New Exception("¡ El Indicador de Aprendizaje: " & nombre_ind & ", no tiene peso !")
                        End If
                        peso_ind += CDbl(Me.gvResultados.DataKeys(i).Values("peso_ind"))
                    End If
                End If
                If CInt(Me.gvResultados.DataKeys(i).Values("codigo_indEvi")) = -1 Then
                    Throw New Exception("¡ El Indicador de Aprendizaje: " & nombre_ind & ", no se ha registrado una evidencia !")
                End If
                nombre_evi = Me.gvResultados.DataKeys(i).Values("descripcion_evi")
                If CInt(Me.gvResultados.DataKeys(i).Values("codigo_eviIns")) = -1 Then
                    Throw New Exception("¡ la Evidencia de Aprendizaje: " & nombre_evi & ", no se ha registrado un instrumento !")
                End If
                ' nombre_ins = Me.gvResultados.DataKeys(i).Values("descripcion_ins")
                If codigo_ins <> CInt(Me.gvResultados.DataKeys(i).Values("codigo_ins")) Then
                    codigo_ins = CInt(Me.gvResultados.DataKeys(i).Values("codigo_ins"))
                    nombre_ins = Me.gvResultados.DataKeys(i).Values("descripcion_ins")
                    If tipo_prom2 = 2 Then
                        If CDbl(Me.gvResultados.DataKeys(i).Values("peso_ins")) = 0.0 Then
                            Throw New Exception("¡ El Instrumento de Evaluación: " & nombre_ins & ", no tiene peso !")
                        End If
                        peso_ins += CDbl(Me.gvResultados.DataKeys(i).Values("peso_ins"))
                    End If
                End If
            Next
            If Math.Round(peso_res, 2) <> 1 Then Throw New Exception("¡ La suma de los pesos de los Resultados de Aprendizaje no es 100% ! ")
            If tipo_prom = 2 And Math.Round(peso_ind, 2) <> 1 Then
                Throw New Exception("¡ El Resultado de Aprendizaje: " & nombre_res & ", la suma de los pesos de sus indicadores no es 100% !")
            End If
            If tipo_prom2 = 2 And Math.Round(peso_ins, 2) <> 1 Then
                Throw New Exception("¡ El Indicador de Aprendizaje: " & nombre_ind & ", la suma de los pesos de sus instrumentos no es 100% !")
            End If
            Response.Redirect("~/gestioncurricular/frmEstrategiasDidactica.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    'Private Sub RefreshGrid()
    '    For Each _Row As GridViewRow In gvResultados.Rows
    '        gvResultados_OnDataBound(gvResultados, New GridViewRowEventArgs(_Row))
    '    Next
    'End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDiseñoAsignatura(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_pes As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "DA", -1, codigo_pes, -1, codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboDisApr, dt, "codigo_dis", "nombre_Cur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "CP", -1, -1, -1, codigo_cac, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DiseñoAsignatura_Listar", "PE", -1, -1, -1, codigo_cac, codigo_cpf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanEstudio, dt, "codigo_Pes", "descripcion_Pes")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_dis As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ResultadoAprendizaje_Listar", "", -1, codigo_dis, "")
            obj.CerrarConexion()
            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()
            'gvResultados_OnDataBound(Nothing, Nothing)
            If Me.gvResultados.Rows.Count > 0 Then mt_AgruparFilas(Me.gvResultados.Rows, 0, 11)
            If Me.gvResultados.Rows.Count > 0 Then mt_BackColorCell(Me.gvResultados, Drawing.Color.LightYellow)
            'Me.btnImportar.Enabled = IIf(Me.gvResultados.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        lst.Add(gridViewRows(0))
        Dim ctrl As TableCell
        ctrl = gridViewRows(0).Cells(startIndex)
        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)
            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    ctrl.VerticalAlign = VerticalAlign.Middle
                    mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            ctrl.VerticalAlign = VerticalAlign.Middle
            mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Private Sub mt_BackColorCell(ByVal grid As GridView, ByVal backcolor As Drawing.Color)
        Dim i As Integer = 0
        Dim text As String = ""
        Dim band As Boolean
        text = grid.Rows(0).Cells(0).Text
        For Each fil As GridViewRow In grid.Rows
            If fil.Cells(0).Text <> text Then
                text = fil.Cells(0).Text
                band = Not band
            End If
            If band Then
                fil.Cells(3).BackColor = backcolor
                fil.Cells(4).BackColor = backcolor
                fil.Cells(5).BackColor = backcolor
                fil.Cells(6).BackColor = backcolor
                fil.Cells(10).BackColor = backcolor
                fil.Cells(11).BackColor = backcolor
                fil.Cells(12).BackColor = backcolor
                fil.Cells(13).BackColor = backcolor
                fil.Cells(14).BackColor = backcolor
                fil.Cells(0).BackColor = backcolor
                fil.Cells(1).BackColor = backcolor
                fil.Cells(2).BackColor = backcolor
                fil.Cells(7).BackColor = backcolor
                fil.Cells(8).BackColor = backcolor
                fil.Cells(9).BackColor = backcolor
            End If
        Next
    End Sub

    Protected Sub AddMergedCells(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    'Private Sub mt_GenerarSilabo(ByVal codigo_cup As Integer)
    '    Dim obj As New ClsConectarDatos
    '    Dim x, y, z, i, j As Integer
    '    Dim dtDis, dtCup As New Data.DataTable
    '    Dim codigo_dis, codigo_pes, codigo_cur, codigo_cac As Integer
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        Dim xUni As Integer
    '        xUni = 4

    '        obj.AbrirConexion()
    '        dtCup = obj.TraerDataTable("CursoProgramado_Listar2", codigo_cup)
    '        If dtCup.Rows.Count > 0 Then
    '            codigo_cac = CInt(dtCup.Rows(0).Item("codigo_Cac"))
    '            codigo_pes = CInt(dtCup.Rows(0).Item("codigo_Pes"))
    '            codigo_cur = CInt(dtCup.Rows(0).Item("codigo_Cur"))
    '        End If
    '        dtDis = obj.TraerDataTable("DiseñoAsignatura_Listar", "", -1, codigo_pes, codigo_cur, codigo_cac, -1)
    '        obj.CerrarConexion()

    '        If dtDis.Rows.Count > 0 Then
    '            codigo_dis = CInt(dtDis.Rows(0).Item("codigo_dis"))
    '        Else
    '            Throw New Exception("¡ No existe Diseño de Asignatura para este Curso !")
    '        End If

    '        Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
    '        ' Dim memory As New System.IO.MemoryStream
    '        'Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)
    '        Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, New System.IO.FileStream("D:\\" & codigo_cup & ".pdf", IO.FileMode.Create))

    '        pdfDoc.Open()

    '        Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(1)
    '        pdfTable.WidthPercentage = 98.0F
    '        pdfTable.DefaultCell.Border = 0

    '        ' 0: Cabecera de Silabo ----------------------------------------------------------------------------------

    '        Dim srcIcon As String = Server.MapPath(".") & "/logo_usat.png"
    '        Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
    '        usatIcon.ScalePercent(60.0F)
    '        usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

    '        Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
    '        cellIcon.HorizontalAlignment = 0
    '        cellIcon.VerticalAlignment = 2
    '        cellIcon.Border = 0
    '        'cellIcon.Rowspan = 2
    '        pdfTable.AddCell(cellIcon)

    '        pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '        pdfTable.AddCell(fc_CeldaTexto("FACULTAD DE " & dtDis.Rows(0).Item("nombre_Fac").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
    '        pdfTable.AddCell(fc_CeldaTexto("PROGRAMA DE ESTUDIOS DE " & dtDis.Rows(0).Item("nombreOficial_cpf").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))
    '        pdfTable.AddCell(fc_CeldaTexto("SÍLABO DE " & dtDis.Rows(0).Item("nombre_Cur").ToString.ToUpper, 10.0F, 1, 0, 1, 1, 1))

    '        pdfTable.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '        ' 1: Datos Informativos ----------------------------------------------------------------------------------

    '        Dim cellTable1 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
    '        cellTable1.WidthPercentage = 100.0F
    '        cellTable1.SetWidths(New Single() {6.0F, 94.0F})
    '        cellTable1.DefaultCell.Border = 0

    '        'pdfTable.AddCell(fc_CeldaTexto("I.	DATOS INFORMATIVOS", 10.0F, 1, 0, 1, 1, 0))
    '        cellTable1.AddCell(fc_CeldaTexto("I. ", 10.0F, 1, 0, 1, 1, 0))
    '        cellTable1.AddCell(fc_CeldaTexto("DATOS INFORMATIVOS", 10.0F, 1, 0, 1, 1, 0))
    '        cellTable1.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '        Dim cellTable11 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
    '        cellTable11.WidthPercentage = 100.0F
    '        cellTable11.SetWidths(New Single() {35.0F, 7.5F, 25.0F, 7.5F, 25.0F})

    '        cellTable11.AddCell(fc_CeldaTexto("1.1 Asignatura:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("nombre_Cur").ToString, 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.2 Código:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("identificador_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.3 Ciclo del plan de estudios:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("ciclo_Cur").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.4 Créditos:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("creditos_Cur"), 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(" 1.5 Tipo de asignatura:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "( X )", "(   )"), 10.0F, 0, 15, 1, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto("Obligatorio", 10.0F, 0, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(IIf(dtCup.Rows(0).Item("electivo_Cur") = 0, "(   )", "( X )"), 10.0F, 0, 15, 1, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto("Electivo", 10.0F, 0, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.6 Prerrequisito:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("prerequisito").ToString.ToUpper, 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.7 Número de horas semanales:", 10.0F, 1, 15, 1, 3, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("N° de horas teóricas:", 10.0F, 0, 15, 3, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasTeo_Cur"), 10.0F, 0, 15, 1, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto("N° de horas prácticas:", 10.0F, 0, 15, 3, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("horasPra_Cur"), 10.0F, 0, 15, 1, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto("N° de horas totales:", 10.0F, 0, 15, 3, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("totalHoras_Cur"), 10.0F, 0, 15, 1, 1, 1))
    '        cellTable11.AddCell(fc_CeldaTexto("1.8 Duración:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("Del (dd/mm) al (dd/mm/año)", 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.9 Semestre académico:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("descripcion_Cac"), 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.10 Grupo Horario:", 10.0F, 1, 15, 1, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtCup.Rows(0).Item("grupoHor_Cup"), 10.0F, 0, 15, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto("1.11 Docente coordinador:", 10.0F, 1, 15, 1, 2, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtDis.Rows(0).Item("Coordinador").ToString, 10.0F, 0, 8, 4, 1, 0))
    '        cellTable11.AddCell(fc_CeldaTexto(dtDis.Rows(0).Item("email").ToString, 10.0F, 4, 8, 4, 1, 0, iTextSharp.text.BaseColor.BLUE))

    '        Dim dtDoc As Data.DataTable
    '        obj.AbrirConexion()
    '        dtDoc = obj.TraerDataTable("DocenteCursoProgramado_Listar", codigo_cup)
    '        obj.CerrarConexion()

    '        If dtDoc.Rows.Count > 0 Then
    '            cellTable11.AddCell(fc_CeldaTexto("1.12 Docente(s):", 10.0F, 1, 15, 1, (dtDoc.Rows.Count * 2), 0))
    '            For x = 0 To dtDoc.Rows.Count - 1
    '                cellTable11.AddCell(fc_CeldaTexto(dtDoc.Rows(0).Item("docente").ToString, 10.0F, 0, 9, 4, 1, 0))
    '                cellTable11.AddCell(fc_CeldaTexto(dtDoc.Rows(0).Item("email_Per").ToString, 10.0F, 4, 10, 4, 1, 0, iTextSharp.text.BaseColor.BLUE))
    '            Next
    '        End If

    '        cellTable1.AddCell(cellTable11)

    '        pdfTable.AddCell(cellTable1)

    '        pdfDoc.Add(pdfTable)

    '        pdfDoc.NewPage()

    '        Dim pdfTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(2)
    '        pdfTable2.WidthPercentage = 98.0F
    '        pdfTable2.SetWidths(New Single() {6.0F, 94.0F})
    '        pdfTable2.DefaultCell.Border = 0

    '        ' 2: Sumilla ------------------------------------------------------------------------------------

    '        Dim dtSum As Data.DataTable
    '        obj.AbrirConexion()
    '        dtSum = obj.TraerDataTable("PlanCursoSumilla_Listar", "PC", -1, codigo_pes, codigo_cur)
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("II. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("SUMILLA", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto(dtSum.Rows(0).Item("descripcion_sum").ToString, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 3: Competencias -------------------------------------------------------------------------------------------------------

    '        Dim dtCom As Data.DataTable
    '        obj.AbrirConexion()
    '        dtCom = obj.TraerDataTable("PerfilEgresoCurso_Listar", -1, -1, codigo_pes, codigo_cur, -1)
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("III. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("COMPETENCIA(S)", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 4, 1))
    '        pdfTable2.AddCell(fc_CeldaTexto("3.1 Competencia(s) de perfil de egreso", 10.0F, 1, 15, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("La asignatura " & dtDis.Rows(0).Item("nombre_Cur").ToString & _
    '                                        ", que corresponde al área de estudios " & dtCom.Rows(0).Item("nombre_cat").ToString & _
    '                                        ", contribuye al logro del perfil de egreso, específicamente a la competencia " & dtCom.Rows(0).Item("nombre_com").ToString & ".", 10.0F, 0, 15, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("3.2 Competencia de la asignatura", 10.0F, 1, 15, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto(dtCom.Rows(0).Item("descripcion_pEgr").ToString, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 4: Unidades didactidas -------------------------------------------------------------------------------------------------

    '        Dim dtUni, dtRes, dtInd, dtEvi, dtIns, dtCon As Data.DataTable
    '        obj.AbrirConexion()
    '        dtUni = obj.TraerDataTable("UnidadAsignatura_Listar", "", -1, codigo_dis, "")
    '        dtRes = obj.TraerDataTable("ResultadoAprendizaje_Listar", "RA", -1, codigo_dis, "")
    '        dtInd = obj.TraerDataTable("IndicadorAprendizaje_listar", "IA", -1, codigo_dis, "")
    '        dtEvi = obj.TraerDataTable("IndicadorEvidencia_Listar", "IE", -1, codigo_dis, -1)
    '        dtIns = obj.TraerDataTable("EvidenciaInstrumento_Listar", "IE", -1, codigo_dis, -1)
    '        dtCon = obj.TraerDataTable("ContenidoAsignatura_Listar", "", -1, codigo_dis)
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("IV. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("UNIDADES DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '        Dim cellTable2 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(5)
    '        cellTable2.WidthPercentage = 100.0F
    '        cellTable2.SetWidths(New Single() {20.0F, 20.0F, 20.0F, 20.0F, 20.0F})
    '        cellTable2.DefaultCell.Border = 0

    '        cellTable2.AddCell(fc_CeldaTexto("Unidad Didáctica", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable2.AddCell(fc_CeldaTexto("Indicadores", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable2.AddCell(fc_CeldaTexto("Evidencia", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable2.AddCell(fc_CeldaTexto("Instrumento de evaluación", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable2.AddCell(fc_CeldaTexto("Contenidos", 10.0F, 1, 15, 1, 1, 1))

    '        Dim rowsInd As Integer = 0
    '        Dim strInd, strEvi, strIns, strCon As String

    '        For x = 0 To (dtUni.Rows.Count - 1)

    '            cellTable2.AddCell(fc_CeldaTexto("Unidad didáctica N° " & (x + 1) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 10.0F, 1, 15, 1, 1, 1))

    '            Dim dtResx, dtIndx, dtEvix, dtInsx, dtConx As Data.DataTable
    '            dtResx = New Data.DataView(dtRes, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable
    '            dtConx = New Data.DataView(dtCon, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

    '            rowsInd = 0
    '            strInd = "" : strEvi = "" : strIns = "" : strCon = ""

    '            For z = 0 To (dtConx.Rows.Count - 1)
    '                strCon = strCon & "• " & dtConx.Rows(z).Item("descripcion").ToString & Environment.NewLine
    '            Next

    '            If dtUni.Rows(x).Item("cant_res") = 1 Then
    '                dtIndx = New Data.DataView(dtInd, "codigo_res = " & dtResx.Rows(0).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                dtEvix = New Data.DataView(dtEvi, "codigo_res = " & dtResx.Rows(0).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                dtInsx = New Data.DataView(dtIns, "codigo_res = " & dtResx.Rows(0).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                For z = 0 To (dtIndx.Rows.Count - 1)
    '                    strInd = strInd & "• " & dtIndx.Rows(z).Item("descripcion_ind").ToString & Environment.NewLine
    '                Next
    '                For z = 0 To (dtEvix.Rows.Count - 1)
    '                    strEvi = strEvi & "• " & dtEvix.Rows(z).Item("descripcion_evi").ToString & Environment.NewLine
    '                Next
    '                For z = 0 To (dtInsx.Rows.Count - 1)
    '                    strIns = strIns & "• " & dtInsx.Rows(z).Item("descripcion_ins").ToString & Environment.NewLine
    '                Next
    '                cellTable2.AddCell(fc_CeldaTexto(strInd, 9.0F, 0, 15, 1, 2, 0))
    '                cellTable2.AddCell(fc_CeldaTexto(strEvi, 9.0F, 0, 15, 1, 2, 0))
    '                cellTable2.AddCell(fc_CeldaTexto(strIns, 9.0F, 0, 15, 1, 2, 0))
    '                cellTable2.AddCell(fc_CeldaTexto(strCon, 10.0F, 0, 15, 1, 2, 0)) '
    '                cellTable2.AddCell(fc_CeldaTexto(dtResx.Rows(0).Item("descripcion_res").ToString, 10.0F, 0, 15, 1, 1, 1))
    '            Else
    '                For y = 0 To dtResx.Rows.Count - 1
    '                    strInd = "" : strEvi = "" : strIns = ""
    '                    dtIndx = New Data.DataView(dtInd, "codigo_res = " & dtResx.Rows(y).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                    dtEvix = New Data.DataView(dtEvi, "codigo_res = " & dtResx.Rows(y).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                    dtInsx = New Data.DataView(dtIns, "codigo_res = " & dtResx.Rows(y).Item("codigo_res"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                    For z = 0 To (dtIndx.Rows.Count - 1)
    '                        strInd = strInd & "• " & dtIndx.Rows(z).Item("descripcion_ind").ToString & Environment.NewLine
    '                    Next
    '                    For z = 0 To (dtEvix.Rows.Count - 1)
    '                        strEvi = strEvi & "• " & dtEvix.Rows(z).Item("descripcion_evi").ToString & Environment.NewLine
    '                    Next
    '                    For z = 0 To (dtInsx.Rows.Count - 1)
    '                        strIns = strIns & "• " & dtInsx.Rows(z).Item("descripcion_ins").ToString & Environment.NewLine
    '                    Next
    '                    If y = 0 Then
    '                        rowsInd = dtUni.Rows(x).Item("cant_res") + 1
    '                        cellTable2.AddCell(fc_CeldaTexto(strInd, 9.0F, 0, 15, 1, 2, 0))
    '                        cellTable2.AddCell(fc_CeldaTexto(strEvi, 9.0F, 0, 15, 1, 2, 0))
    '                        cellTable2.AddCell(fc_CeldaTexto(strIns, 9.0F, 0, 15, 1, 2, 0))
    '                        cellTable2.AddCell(fc_CeldaTexto(strCon, 10.0F, 0, 15, 1, rowsInd, 0)) '
    '                        cellTable2.AddCell(fc_CeldaTexto(dtResx.Rows(0).Item("descripcion_res").ToString, 10.0F, 0, 15, 1, 1, 1))
    '                    Else
    '                        cellTable2.AddCell(fc_CeldaTexto(dtResx.Rows(y).Item("descripcion_res").ToString, 10.0F, 0, 15, 1, 1, 1))
    '                        cellTable2.AddCell(fc_CeldaTexto(strInd, 9.0F, 0, 15, 1, 1, 0))
    '                        cellTable2.AddCell(fc_CeldaTexto(strEvi, 9.0F, 0, 15, 1, 1, 0))
    '                        cellTable2.AddCell(fc_CeldaTexto(strIns, 9.0F, 0, 15, 1, 1, 0))
    '                    End If
    '                Next
    '            End If

    '        Next

    '        pdfTable2.AddCell(cellTable2)

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 5: Estrategias Didacticas -------------------------------------------------------------------------------------

    '        Dim dtEst As Data.DataTable
    '        obj.AbrirConexion()
    '        dtEst = obj.TraerDataTable("EstrategiaDidactica_Listar", -1, codigo_dis, "")
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("V. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("ESTRATEGIAS DIDÁCTICAS", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 0))

    '        Dim strEst As String
    '        strEst = "Para el desarrollo de la asignatura se emplearán las siguientes estrategias didácticas:" & Environment.NewLine
    '        For x = 0 To (dtEst.Rows.Count - 1)
    '            strEst = strEst & "• " & dtEst.Rows(x).Item("descripcion_dis").ToString & Environment.NewLine
    '        Next

    '        pdfTable2.AddCell(fc_CeldaTexto(strEst, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 6: Evaluacion ---------------------------------------------------------------------------------------------------

    '        pdfTable2.AddCell(fc_CeldaTexto("VI. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("EVALUACIÓN", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '        Dim cellTable3 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
    '        cellTable3.WidthPercentage = 100.0F
    '        cellTable3.SetWidths(New Single() {35.0F, 20.0F, 15.0F, 20.0F})
    '        cellTable3.DefaultCell.Border = 0

    '        cellTable3.AddCell(fc_CeldaTexto("6.1 Criterios de evaluación", 10.0F, 1, 15, 4, 1, 0))
    '        cellTable3.AddCell(fc_CeldaTexto("La evaluación será formativa y sumativa, se aplicará evaluaciones de entrada y de salida, considerando las evidencias " & _
    '                                         "(por ejemplo informes, exposiciones sobre textos académicos) e instrumentos que se emplearán para la evaluación " & _
    '                                         "de cada una de ellas. Por ejemplo: listas de cotejo, escalas estimativas, rúbricas, pruebas de ensayo etc.", 10.0F, 0, 15, 4, 1, 0))
    '        cellTable3.AddCell(fc_CeldaTexto("Nota: El  límite de inasistencias acumuladas es de 30%", 10.0F, 0, 15, 4, 1, 0))
    '        cellTable3.AddCell(fc_CeldaTexto("6.2 Sistema de calificación", 10.0F, 1, 15, 4, 1, 0))
    '        cellTable3.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota de resultado de aprendizaje (RA)", 10.0F, 1, 15, 4, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("RA = promedio (Calificaciones obtenidas en sus indicadores)", 10.0F, 0, 15, 4, 1, 1))

    '        cellTable3.AddCell(fc_CeldaTexto("Evaluación", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("Unidad(es) en la(s) que se trabaja", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("Peso", 10.0F, 1, 15, 1, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("N° de evaluaciones", 10.0F, 1, 15, 1, 1, 1))

    '        Dim strRes As String
    '        strRes = "NF ="
    '        For x = 0 To (dtRes.Rows.Count - 1)
    '            cellTable3.AddCell(fc_CeldaTexto("Resultado de aprendizaje N° " & (x + 1) & " (RA" & (x + 1) & ")", 10.0F, 0, 15, 1, 1, 0))
    '            cellTable3.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("unidad_res").ToString, 10.0F, 0, 15, 1, 1, 1))
    '            cellTable3.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("peso_res"), 10.0F, 0, 15, 1, 1, 1))
    '            cellTable3.AddCell(fc_CeldaTexto(dtRes.Rows(x).Item("cant_eva"), 10.0F, 0, 15, 1, 1, 1))
    '            strRes = strRes & " RA" & (x + 1) & "(" & dtRes.Rows(x).Item("peso_res") & ") +"
    '        Next
    '        strRes = strRes.TrimEnd("+")

    '        cellTable3.AddCell(fc_CeldaTexto("Total de evaluaciones programadas", 10.0F, 1, 15, 3, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 15, 1, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto("Fórmula para la obtención de la nota final de la asignatura (NF)", 10.0F, 1, 15, 4, 1, 1))
    '        cellTable3.AddCell(fc_CeldaTexto(strRes, 10.0F, 0, 15, 4, 1, 1))

    '        pdfTable2.AddCell(cellTable3)

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 7: Referencias --------------------------------------------------------------------------------------------------------------

    '        Dim dtRef As Data.DataTable
    '        obj.AbrirConexion()
    '        dtRef = obj.TraerDataTable("ReferenciaBibliografica_Listar", -1, codigo_dis, -1, "")
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("VII. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("REFERENCIAS", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 6, 1))

    '        Dim dtRef1, dtRef2, dtRef3 As Data.DataTable
    '        Dim strRef1, strRef2, strRef3 As String

    '        dtRef1 = New Data.DataView(dtRef, "codigo_tip = " & "5", "", Data.DataViewRowState.CurrentRows).ToTable
    '        dtRef2 = New Data.DataView(dtRef, "codigo_tip = " & "6", "", Data.DataViewRowState.CurrentRows).ToTable
    '        dtRef3 = New Data.DataView(dtRef, "codigo_tip = " & "7", "", Data.DataViewRowState.CurrentRows).ToTable

    '        strRef1 = "" : strRef2 = "" : strRef3 = ""

    '        For x = 0 To (dtRef1.Rows.Count - 1)
    '            strRef1 = strRef1 & "• " & dtRef1.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
    '        Next
    '        For x = 0 To (dtRef2.Rows.Count - 1)
    '            strRef2 = strRef2 & "• " & dtRef2.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
    '        Next
    '        For x = 0 To (dtRef3.Rows.Count - 1)
    '            strRef3 = strRef3 & "• " & dtRef3.Rows(x).Item("descripcion_ref").ToString & Environment.NewLine
    '        Next

    '        pdfTable2.AddCell(fc_CeldaTexto("7.1 Referencias USAT", 10.0F, 1, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto(strRef1, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("7.2 Referencias complementarias", 10.0F, 1, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto(strRef2, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("7.3 Investigaciones de docentes", 10.0F, 1, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto(strRef3, 10.0F, 0, 15, 1, 1, 0))

    '        pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 2, 1, 0))

    '        ' 8: Programacion de Actividades -------------------------------------------------------------------------------

    '        Dim dtGru, dtSes, dtAct, dtEva As Data.DataTable
    '        obj.AbrirConexion()
    '        dtGru = obj.TraerDataTable("GrupoContenidoAsignatura_Listar", "GC", -1, codigo_dis)
    '        dtAct = obj.TraerDataTable("ActividadAsignatura_Listar", "AA", -1, codigo_dis)
    '        dtSes = obj.TraerDataTable("SesionAsignatura_Listar", "SA", -1, codigo_dis, "")
    '        dtEva = obj.TraerDataTable("EvaluacionCurso_Listar", "EC", -1, codigo_cup, -1, -1)
    '        obj.CerrarConexion()

    '        pdfTable2.AddCell(fc_CeldaTexto("VIII. ", 10.0F, 1, 0, 1, 1, 0))
    '        pdfTable2.AddCell(fc_CeldaTexto("PROGRAMACIÓN DE ACTIVIDADES", 10.0F, 1, 0, 1, 1, 0))

    '        For x = 0 To (dtUni.Rows.Count - 1)

    '            pdfTable2.AddCell(fc_CeldaTexto("" & Environment.NewLine, 10.0F, 1, 0, 1, 1, 1))

    '            Dim cellTable4 As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(4)
    '            cellTable4.WidthPercentage = 100.0F
    '            cellTable4.SetWidths(New Single() {25.0F, 25.0F, 25.0F, 25.0F})
    '            cellTable4.DefaultCell.Border = 0

    '            cellTable4.AddCell(fc_CeldaTexto("Unidad didáctica N° " & (x + 1) & ": " & dtUni.Rows(x).Item("descripcion_uni").ToString, 10.0F, 1, 15, 4, 1, 1))
    '            cellTable4.AddCell(fc_CeldaTexto("Sesión (N° / dd-mm)", 10.0F, 1, 15, 1, 1, 1))
    '            cellTable4.AddCell(fc_CeldaTexto("Contenidos", 10.0F, 1, 15, 1, 1, 1))
    '            cellTable4.AddCell(fc_CeldaTexto("Actividades", 10.0F, 1, 15, 1, 1, 1))
    '            cellTable4.AddCell(fc_CeldaTexto("Evaluaciones", 10.0F, 1, 15, 1, 1, 1))

    '            Dim dtGrux, dtCony, dtActx, dtSesx, dtEvax As Data.DataTable
    '            Dim strCony, strActx, strFecx, strMes, strEvax As String
    '            Dim strFec As String()
    '            dtGrux = New Data.DataView(dtGru, "codigo_uni = " & dtUni.Rows(x).Item("codigo_uni"), "", Data.DataViewRowState.CurrentRows).ToTable

    '            For y = 0 To (dtGrux.Rows.Count - 1)

    '                dtCony = New Data.DataView(dtCon, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                dtActx = New Data.DataView(dtAct, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                dtSesx = New Data.DataView(dtSes, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                dtEvax = New Data.DataView(dtEva, "codigo_gru = " & dtGrux.Rows(y).Item("codigo_gru"), "", Data.DataViewRowState.CurrentRows).ToTable
    '                strCony = "" : strActx = "" : strFecx = "" : strMes = "" : strEvax = ""
    '                strFec = Nothing
    '                For z = 0 To (dtCony.Rows.Count - 1)
    '                    strCony = strCony & "• " & dtCony.Rows(z).Item("descripcion").ToString & Environment.NewLine
    '                Next
    '                For z = 0 To (dtActx.Rows.Count - 1)
    '                    strActx = strActx & "• " & dtActx.Rows(z).Item("descripcion").ToString & Environment.NewLine
    '                Next
    '                For z = 0 To (dtEvax.Rows.Count - 1)
    '                    strEvax = strEvax & "• " & dtEvax.Rows(z).Item("descripcion_ins").ToString & Environment.NewLine
    '                Next
    '                If dtSesx.Rows.Count = 1 Then
    '                    If dtSesx.Rows(0).Item("fecha_ses").ToString = "-" Then
    '                        strFecx = "-"
    '                    Else
    '                        strFec = dtSesx.Rows(0).Item("fecha_ses").ToString.Split(",")
    '                        For j = 0 To strFec.Length - 1
    '                            strMes = CDate(strFec(j)).Month
    '                            strFecx = strFecx & CDate(strFec(j)).Day & " -"
    '                        Next
    '                        strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
    '                    End If
    '                    cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(0).Item("orden_ses").ToString & " / " & strFecx, 10.0F, 0, 15, 1, 1, 0))
    '                    cellTable4.AddCell(fc_CeldaTexto(strCony, 10.0F, 0, 15, 1, 1, 0))
    '                    cellTable4.AddCell(fc_CeldaTexto(strActx, 10.0F, 0, 15, 1, 1, 0))
    '                    cellTable4.AddCell(fc_CeldaTexto(strEvax, 10.0F, 0, 15, 1, 1, 0))
    '                Else
    '                    strFecx = ""
    '                    For i = 0 To (dtSesx.Rows.Count - 1)
    '                        If dtSesx.Rows(i).Item("fecha_ses").ToString = "-" Then
    '                            strFecx = "-"
    '                        Else
    '                            strFec = dtSesx.Rows(i).Item("fecha_ses").ToString.Split(",")
    '                            For j = 0 To strFec.Length - 1
    '                                strMes = CDate(strFec(j)).Month
    '                                strFecx = strFecx & CDate(strFec(j)).Day & " -"
    '                            Next
    '                            strFecx = strFecx.TrimEnd("-") & " de " & MonthName(strMes)
    '                        End If
    '                        If i = 0 Then
    '                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("orden_ses").ToString & " / " & strFecx, 10.0F, 0, 15, 1, 1, 0))
    '                            cellTable4.AddCell(fc_CeldaTexto(strCony, 10.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
    '                            cellTable4.AddCell(fc_CeldaTexto(strActx, 10.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
    '                            cellTable4.AddCell(fc_CeldaTexto(strEvax, 10.0F, 0, 15, 1, dtSesx.Rows.Count, 0))
    '                        Else
    '                            cellTable4.AddCell(fc_CeldaTexto(dtSesx.Rows(i).Item("orden_ses").ToString & " / " & strFecx, 10.0F, 0, 15, 1, 1, 0))
    '                        End If
    '                    Next
    '                End If
    '            Next

    '            pdfTable2.AddCell(cellTable4)

    '        Next

    '        pdfDoc.Add(pdfTable2)

    '        pdfDoc.Close()

    '        'Data.Add("PDF", "OK")

    '        'Dim bytes() As Byte = memory.ToArray
    '        'memory.Close()

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarCarreraProfesional()
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CA", "2", cod_user)
    '        obj.CerrarConexion()
    '        mt_CargarCombo(Me.cboCarProf, dt, "codigo_Cpf", "nombre_Cpf")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarPlanEstudio(ByVal codigo_pcur As String)
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("ConsultarPlanEstudio", "CA", codigo_pcur, cod_user)
    '        obj.CerrarConexion()
    '        mt_CargarCombo(Me.cboPlanEst, dt, "codigo_Pes", "descripcion_Pes")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función para crear una celta tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.Padding = 6
        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <param name="_fontcolor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                 ByVal _fontcolor As iTextSharp.text.BaseColor) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style, _fontcolor)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.Padding = 6
        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto2(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC2 As iTextSharp.text.pdf.PdfPCell
        Dim fontITC2 As iTextSharp.text.Font
        fontITC2 = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC2))
        celdaITC2.HorizontalAlignment = _haligment
        Return celdaITC2
    End Function

#End Region

End Class
