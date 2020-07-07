
Partial Class GestionCurricular_frmCalificadorCurso_Modular
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private nro_col As Integer = 2

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
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            cod_ctf = Session("cod_ctf")

            If Not IsPostBack Then
                Session("isTextValid") = 1 'Cambiar a "cero" para proceder al registro por SMS
                Session("isValidated") = 0
                Session("gc_msjeEnlace") = Nothing
                Session("gc_msjeNoEnlace") = Nothing

                If Session("gc_enviar") Then mt_CargarCondiciones()

                mt_CargarCortes(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_CargarGrupos(Session("gc_codigo_cup"), Session("gc_enviar"), Session("gc_codigo_cor"))

                Me.lblCorte.InnerText = "Calificaciones de la " & fc_getDescripcionCorte(Session("gc_codigo_cor"))
                Me.lblCurso.InnerText = "Asignatura (Modular): " & Session("gc_nombre_cur")

                'If cod_ctf = 218 Then
                Me.btnEnviar.Visible = False
                Me.btnGuardar.Visible = False
                'Else
                'Me.btnEnviar.Visible = Not (Session("gc_enviar"))
                'Me.btnGuardar.Visible = Session("gc_enviar")
                'End If

                divResumen.Style.Item("display") = "none"
                divEnviar.Style.Item("display") = "none"

                Call mt_ObtenerInfoPersonal()

            Else

                If Not String.IsNullOrEmpty(navigate.Value) Then
                    Session("isValidated") = navigate.Value
                End If

                If Session("isTextValid") IsNot Nothing AndAlso Session("isTextValid") = 1 Then
                    divEnviar.Style.Item("display") = "none"
                End If

                Call mt_RefreshGrid()
            End If

            If Session("gc_enviar") IsNot Nothing AndAlso Session("gc_enviar") Then Call mt_MostrarLeyenda()

        Catch ex As Exception
            If ex.Message.Contains("objeto") Then
                mt_ShowMessage("La sesión del usuario <b>ha caducado</b>. Regrese a la pantalla anterior y vuelva a intentar.", MessageType.Error)
            Else
                mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            End If
        End Try
    End Sub

    Protected Sub gvNotas_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As New Data.DataTable
        Dim dtA As New Data.DataTable
        Dim _codigo_dma, _codigo_eva, _codigo_emd, _tipo_prom, _cant_ins, _cant_alu, _cant_ins_aux As Integer
        Dim _codigo_pso, _codigo_ins, _codigo_cup As Integer '20190918-ENEVADO
        Dim _codigo_ind, _tipo_prom_ind, _codigo_res, _cant_ind, _cant_rs As Integer '20191009-ENEVADO
        Dim _descripcion_eva, _descripcion_ins, _descripcion_ind, _descripcion_uni As String
        Dim _pendiente As Boolean
        Dim _calificacion, _sumatoria, _sum_ind, _peso_ins, _peso_ind As Double

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)

                _cant_alu = (dtA.Rows.Count - 1)
                _codigo_cup = Session("gc_codigo_cup") '20190919-ENEVADO

                If dt.Rows.Count > 0 Then
                    _codigo_dma = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_Dma"))
                    _codigo_pso = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_pso")) '20190918-ENEVADO
                    _calificacion = 0 : _sumatoria = 0 : _sum_ind = 0 : _cant_ind = 0 : _cant_ins_aux = 0
                    _codigo_res = -1 : _codigo_ind = -1 : _cant_rs = 0

                    For i As Integer = 0 To dt.Rows.Count - 1
                        _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
                        _codigo_ins = CInt(dt.Rows(i).Item("codigo_ins")) '20190918-ENEVADO
                        _descripcion_eva = dt.Rows(i).Item("descripcion_eva")
                        _codigo_emd = dt.Rows(i).Item("codigo_emd")
                        '_cant_ins = dt.Rows(i).Item("cant_ins")

                        '--> 25/09/2019 | JQuepuy
                        _descripcion_ins = dt.Rows(i).Item("descripcion_ins")
                        _descripcion_ind = dt.Rows(i).Item("descripcion_ind")
                        _descripcion_uni = dt.Rows(i).Item("numero_uni")

                        If _codigo_dma <> -1 Then

                            '20191009-ENEVADO --------------------------------------------------\
                            If _codigo_ind <> CInt(dt.Rows(i).Item("codigo_ind")) Then
                                _codigo_ind = CInt(dt.Rows(i).Item("codigo_ind"))
                                _cant_ind += 1
                                If i > 0 Then
                                    If _tipo_prom = 2 Then
                                        If _tipo_prom_ind = 2 Then
                                            _sumatoria += (_sum_ind * _peso_ind)
                                        Else
                                            _sumatoria += (_sum_ind / _cant_ins_aux) * _peso_ind
                                        End If
                                    Else
                                        If _tipo_prom_ind = 2 Then
                                            _sumatoria += _sum_ind
                                        Else
                                            _sumatoria += (_sum_ind / _cant_ins_aux)
                                        End If
                                    End If
                                    _sum_ind = 0
                                    _cant_ins_aux = 0
                                End If

                                _tipo_prom_ind = CInt(dt.Rows(i).Item("promedio_instrumento"))
                                _peso_ind = CDbl(dt.Rows(i).Item("peso_ind"))
                                '_sum_ind = 0
                            End If
                            _cant_ins_aux += 1

                            _peso_ins = CDbl(dt.Rows(i).Item("peso_ins"))
                            If _codigo_res <> CInt(dt.Rows(i).Item("codigo_res")) Then
                                _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                                _tipo_prom = CInt(dt.Rows(i).Item("promedio_indicador"))
                                _cant_ins += CInt(dt.Rows(i).Item("cant_ins"))
                                _sumatoria = 0
                                '_sum_ind = 0
                                _cant_ind = 0
                                _cant_rs += 1
                            End If
                            ' -------------------------------------------------------------------/

                            If Not (Session("gc_enviar")) Then

                                If _codigo_emd = -1 Then
                                    Dim lblNota As New Label()
                                    lblNota.ID = "lblNota" & (i + 1)
                                    lblNota.Style.Add("text-align", "right")
                                    lblNota.Text = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) '20190919-ENEVADO
                                    lblNota.Enabled = False
                                    If lblNota.Text.Trim <> "" Then lblNota.ForeColor = IIf(CDbl(lblNota.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                                    e.Row.Cells(nro_col + i).Controls.Add(lblNota)
                                Else
                                    ' 20190919-ENEVADO ----------------------------------------------------------------------\
                                    Dim lblNotaMdl As New Label()
                                    lblNotaMdl.ID = "lblNota" & (i + 1)

                                    Dim _nota_aux As String
                                    '_nota_aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins) 'Comentado por Luis Q.T| 13DIC2019
                                    _nota_aux = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup)

                                    If String.IsNullOrEmpty(_nota_aux) Then
                                        _nota_aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                    End If

                                    If _nota_aux.Trim <> "" Then
                                        lblNotaMdl.Text = CDbl(_nota_aux).ToString("00.00")
                                    End If
                                    If lblNotaMdl.Text.Trim <> "" Then lblNotaMdl.ForeColor = IIf(CDbl(lblNotaMdl.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                                    e.Row.Cells(nro_col + i).Controls.Add(lblNotaMdl)
                                    ' ---------------------------------------------------------------------------------------/
                                End If

                            Else

                                Dim lblNotaReg As New Label()
                                Dim _nota_aux As String
                                lblNotaReg.ID = "lblNota" & (i + 1)
                                'lbl.Text = fc_getNota(_codigo_dma, _codigo_eva)
                                _nota_aux = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup)
                                lblNotaReg.Text = CDbl(_nota_aux).ToString("00.00")
                                lblNotaReg.ForeColor = IIf(CDbl(lblNotaReg.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                                e.Row.Cells(nro_col + i).Controls.Add(lblNotaReg)

                                'Dim _nota_aux As String
                                _nota_aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                '_nota_aux = "18.00"
                                If _nota_aux.Trim <> "" Then
                                    If lblNotaReg.Text <> CDbl(_nota_aux).ToString("00.00") Then
                                        e.Row.Cells(nro_col + i).BackColor = Drawing.Color.LightGray
                                        e.Row.Cells(nro_col + i).ToolTip = "Aviso: La nota de esta evaluación ha sido modificado por: " & CDbl(_nota_aux).ToString("00.00")
                                    End If
                                End If

                                If _tipo_prom_ind = 2 Then
                                    _sum_ind += (fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) * _peso_ins)
                                Else
                                    _sum_ind += fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) '20191009-ENEVADO
                                End If

                                ' 20191015-ENEVADO ----------------------------------------------------------------------\
                                Dim _codigo_ece As Integer
                                Dim _observacion_ece, _nota_new, _nota As String
                                _codigo_ece = fc_getCodEdicion(_codigo_dma, _codigo_ins)
                                If _codigo_ece <> -1 Then

                                    _observacion_ece = fc_getObsEdicion(_codigo_dma, _codigo_ins)
                                    _nota_new = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                    '_nota_new = "16.00"
                                    _nota = lblNotaReg.Text

                                    lblNotaReg = New Label()
                                    lblNotaReg.ID = "lblStepNop" & (i + 1)
                                    lblNotaReg.Text = " "
                                    e.Row.Cells(nro_col + i).Controls.Add(lblNotaReg)

                                    Dim btnEdit As New LinkButton()
                                    btnEdit.ID = "btnEditarNota" & (i + 1)
                                    btnEdit.Text = "<i class='fa fa-pen'></i>"
                                    btnEdit.ToolTip = "Actualizar Nota"
                                    btnEdit.CommandArgument = e.Row.RowIndex
                                    btnEdit.Attributes.Add("codigo_nop", fc_getCodNota(_codigo_dma, _codigo_ins))
                                    btnEdit.Attributes.Add("codigo_ece", _codigo_ece)
                                    btnEdit.Attributes.Add("codigo_pso", _codigo_pso)
                                    btnEdit.Attributes.Add("codigo_ins", _codigo_ins)
                                    btnEdit.CommandName = "EditarNotaMoodle"
                                    If _nota <> CDbl(_nota_aux).ToString("00.00") Then
                                        btnEdit.OnClientClick = "return confirm('La Nota actual del estudiante es: " & _nota & ", se cambiará por: " & CDbl(_nota_aux).ToString("00.00") & "  ¿ Desea Continuar ?');"
                                    Else
                                        btnEdit.OnClientClick = "alert('Advertencia: La nota actual del estudiante aún no ha sido modificada en el aula virtual.'); return false; "
                                    End If

                                    btnEdit.CssClass = "btn btn-warning btn-sm"
                                    'btnObs.Visible = IIf(cbo.SelectedItem.Text = "OTROS", True, False)
                                    AddHandler btnEdit.Click, AddressOf btnEdit_Click
                                    e.Row.Cells(nro_col + i).Controls.Add(btnEdit)
                                    e.Row.Cells(nro_col + i).ToolTip = "Modificación de Nota Pendiente: " & _observacion_ece

                                End If
                                ' ---------------------------------------------------------------------------------------/

                            End If
                        Else
                            If Not (Session("gc_enviar")) Then
                                '_pendiente = fc_ValidarCorte(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                                '_edicion = fc_validarEdicionBtn(_codigo_eva)
                                If _codigo_emd = -1 Then
                                    Dim lblSinEnlazar As New Label()
                                    lblSinEnlazar.ID = "lblMoodle" & (i + 1)

                                    If _cant_alu <= fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "0") Then
                                        lblSinEnlazar.ToolTip = "Éstas calificaciones ya fueron enviadas"
                                        lblSinEnlazar.Text = "- <span style='color: black'><i class='fa fa-lock'></i></span> -"
                                    Else
                                        lblSinEnlazar.Text = "<b style='color: red'>No enlazado con aula virtual</b><br/>" & fc_getResumenNotas(False, _codigo_ins, _descripcion_ins, _descripcion_ind, _descripcion_uni)
                                    End If

                                    e.Row.Cells(nro_col + i).Controls.Add(lblSinEnlazar)
                                Else
                                    Dim lbl As New Label()
                                    lbl.ID = "lblMoodle" & (i + 1)

                                    If _cant_alu <= fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "0") Then
                                        lbl.ToolTip = "Éstas calificaciones ya fueron enviadas"
                                        lbl.Text = "- <span style='color: black'><i class='fa fa-lock'></i></span> -"
                                    Else
                                        lbl.Text = "<b style='color: green'>Enlazado con aula virtual</b><br/>" & fc_getResumenNotas(True, _codigo_ins, _descripcion_ins, _descripcion_ind, _descripcion_uni)
                                    End If

                                    e.Row.Cells(nro_col + i).Controls.Add(lbl)
                                End If
                            End If
                        End If
                    Next

                    If _codigo_dma <> -1 AndAlso (Session("gc_enviar")) Then
                        Dim lblProm As New Label()
                        lblProm.ID = "lblCalificacion"
                        'If i > 0 Then
                        If _tipo_prom = 2 Then
                            If _tipo_prom_ind = 2 Then
                                _sumatoria += (_sum_ind * _peso_ind)
                            Else
                                _sumatoria += (_sum_ind / _cant_ins_aux) * _peso_ind
                            End If
                        Else
                            If _tipo_prom_ind = 2 Then
                                _sumatoria += _sum_ind
                            Else
                                _sumatoria += (_sum_ind / _cant_ins_aux)
                            End If
                        End If

                        'If _cant_rs > 1 Then
                        _cant_ind += 1
                        'End If

                        'End If
                        If _cant_ins = dt.Rows.Count Then
                            If _tipo_prom = 2 Then
                                _calificacion = _sumatoria
                                '_calificacion = _cant_ins
                            Else
                                _calificacion = Math.Round(_sumatoria / _cant_ind, 2)
                            End If
                        Else
                            '_calificacion = fc_getNota(_codigo_dma, _codigo_eva)
                            _calificacion = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) '20190919-ENEVADO
                        End If
                        lblProm.Text = _calificacion.ToString("00.00")
                        lblProm.ForeColor = IIf(CDbl(lblProm.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                        'lbl.Style.Add("background-color", fc_getColorNivelLogro(CDbl(lbl.Text.Trim)))
                        e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lblProm)
                        lblProm = New Label()
                        lblProm.ID = "lblStep2"
                        lblProm.Text = " "
                        e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lblProm)
                        lblProm = New Label()
                        lblProm.ID = "lblColor"
                        lblProm.ForeColor = Drawing.Color.Transparent
                        lblProm.Text = "<br/>____"
                        lblProm.Style.Add("background-color", fc_getColorNivelLogro(_calificacion))
                        e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lblProm)
                        'e.Row.Cells(nro_col + dt.Rows.Count).Style.Add("background-color", fc_getColorNivelLogro(CDbl(lbl.Text.Trim)))
                        If _calificacion < CDbl("13.5") Then
                            Dim cbo As New DropDownList()
                            cbo.ID = "cboInterno"
                            cbo.CssClass = "form-control input-sm"
                            mt_CargarCombo(cbo, CType(Session("gc_dtCondInt"), Data.DataTable), "codigo_con", "descripcion_con")
                            cbo.SelectedValue = fc_getCodigoCon(_codigo_dma, 1)
                            cbo.AutoPostBack = True
                            AddHandler cbo.SelectedIndexChanged, AddressOf cboInterno_SelectedIndexChanged
                            e.Row.Cells(nro_col + dt.Rows.Count + 1).Controls.Add(cbo)

                            ' 20191001-ENEVADO ----------------------------------------------------------------------\
                            'Dim btnObs As New LinkButton()
                            'btnObs.ID = "btnObsInterno"
                            'btnObs.Text = "<i class='fa fa-pen'></i>"
                            'btnObs.ToolTip = "Ingresar condición interna"
                            'btnObs.CommandArgument = e.Row.RowIndex
                            'btnObs.Attributes.Add("obsInt", fc_getNombreCond(_codigo_dma, 1))
                            'btnObs.CommandName = "CondInterna"
                            ''If (_pendiente Or _edicion) Then btn.OnClientClick = "return confirm('¿ Desea guardar las calificaciones de la evaluación: " & _descripcion_eva & "?');"
                            'btnObs.CssClass = "btn btn-primary btn-sm"
                            'btnObs.Visible = IIf(cbo.SelectedItem.Text = "OTROS", True, False)
                            'AddHandler btnObs.Click, AddressOf btnObsInterno_Click
                            'e.Row.Cells(nro_col + dt.Rows.Count + 1).Controls.Add(btnObs)
                            ' ---------------------------------------------------------------------------------------/
                            Dim txtObs As TextBox

                            txtObs = New TextBox
                            txtObs.ID = "txtObsInterno"
                            txtObs.Text = fc_getNombreCond(_codigo_dma, 1)
                            txtObs.CssClass = "form-control input-sm"
                            txtObs.TextMode = TextBoxMode.MultiLine
                            txtObs.Attributes.Add("obsInt", txtObs.Text)
                            txtObs.Rows = 2
                            txtObs.Columns = 18
                            txtObs.Style.Add("overflow", "hidden")

                            Dim btnObs As New LinkButton()
                            btnObs.ID = "btnObsInterno"
                            btnObs.Text = "<i class='fa fa-times'></i>"
                            btnObs.ToolTip = "Cancelar edición de condición otros"
                            btnObs.CommandArgument = e.Row.RowIndex
                            btnObs.CommandName = "CondInterna"
                            btnObs.CssClass = "btn btn-danger btn-sm"

                            btnObs.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)
                            txtObs.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)
                            cbo.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), False, True)
                            AddHandler btnObs.Click, AddressOf btnObsInterno_Click

                            e.Row.Cells(nro_col + dt.Rows.Count + 1).Controls.Add(txtObs)
                            e.Row.Cells(nro_col + dt.Rows.Count + 1).Controls.Add(btnObs)
                            ' ---------------------------------------------------------------------------------------/

                            cbo = New DropDownList
                            cbo.ID = "cboExterno"
                            cbo.CssClass = "form-control input-sm"
                            mt_CargarCombo(cbo, CType(Session("gc_dtCondExt"), Data.DataTable), "codigo_con", "descripcion_con")
                            cbo.SelectedValue = fc_getCodigoCon(_codigo_dma, 2)
                            cbo.AutoPostBack = True
                            AddHandler cbo.SelectedIndexChanged, AddressOf cboExterno_SelectedIndexChanged
                            e.Row.Cells(nro_col + dt.Rows.Count + 2).Controls.Add(cbo)

                            ' 20191001-ENEVADO ----------------------------------------------------------------------\
                            'btnObs = New LinkButton()
                            'btnObs.ID = "btnObsExterno"
                            'btnObs.Text = "<i class='fa fa-pen'></i>"
                            'btnObs.ToolTip = "Ingresar condición externa"
                            'btnObs.CommandArgument = e.Row.RowIndex
                            'btnObs.Attributes.Add("obsExt", fc_getNombreCond(_codigo_dma, 2))
                            'btnObs.CommandName = "CondExterna"
                            ''If (_pendiente Or _edicion) Then btn.OnClientClick = "return confirm('¿ Desea guardar las calificaciones de la evaluación: " & _descripcion_eva & "?');"
                            'btnObs.CssClass = "btn btn-primary btn-sm"
                            'btnObs.Visible = IIf(cbo.SelectedItem.Text = "OTROS", True, False)
                            'AddHandler btnObs.Click, AddressOf btnObsExterno_Click
                            'e.Row.Cells(nro_col + dt.Rows.Count + 2).Controls.Add(btnObs)

                            txtObs = New TextBox
                            txtObs.ID = "txtObsExterno"
                            txtObs.Text = fc_getNombreCond(_codigo_dma, 2)
                            txtObs.CssClass = "form-control input-sm"
                            txtObs.TextMode = TextBoxMode.MultiLine
                            txtObs.Attributes.Add("obsExt", txtObs.Text)
                            txtObs.Rows = 2
                            txtObs.Columns = 18
                            txtObs.Style.Add("overflow", "hidden")

                            btnObs = New LinkButton()
                            btnObs.ID = "btnObsExterno"
                            btnObs.Text = "<i class='fa fa-times'></i>"
                            btnObs.ToolTip = "Cancelar edición de condición otros"
                            btnObs.CommandArgument = e.Row.RowIndex
                            btnObs.CommandName = "CondExterna"
                            btnObs.CssClass = "btn btn-danger btn-sm"

                            btnObs.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)
                            txtObs.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)
                            cbo.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), False, True)
                            AddHandler btnObs.Click, AddressOf btnObsExterno_Click

                            e.Row.Cells(nro_col + dt.Rows.Count + 2).Controls.Add(txtObs)
                            e.Row.Cells(nro_col + dt.Rows.Count + 2).Controls.Add(btnObs)
                            ' ---------------------------------------------------------------------------------------/

                        End If
                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                For i As Integer = 0 To dt.Rows.Count - 1
                    e.Row.Cells(nro_col + i).ToolTip = "Fecha: " & dt.Rows(i).Item("fecha_gru") & Environment.NewLine & _
                    "Evidencia: " & dt.Rows(i).Item("descripcion_evi") & Environment.NewLine & _
                    "Instrumento: " & dt.Rows(i).Item("descripcion_ins")
                Next
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvNotas.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _inhabilitado As Integer = Me.gvNotas.DataKeys(e.Row.RowIndex).Values("inhabilitado_dma")

                If _inhabilitado = 1 Then
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " [INH]"
                    e.Row.Cells(1).ForeColor = Drawing.Color.DarkOrange
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As Data.DataTable
        Dim _codigo_uni, _nro_eva, _codigo_res, _nro_res, _codigo_ind, _nro_ind As Integer
        Dim _cant_ra, _cant_ind As Integer
        Dim _nombre_ind, _tooltip_ind, _peso_ind, _tipo_prom As String
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objgridviewrow2 As GridViewRow = New GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objgridviewrow3 As GridViewRow = New GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                If dt.Rows.Count > 0 Then
                    _codigo_uni = -1 : _codigo_res = -1
                    _nombre_ind = String.Empty : _tooltip_ind = String.Empty
                    mt_AgregarCabecera(objgridviewrow, objtablecell, 2, 3, "Datos del Estudiante")
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If _codigo_uni <> CInt(dt.Rows(i).Item("codigo_uni")) Then
                            If i <> 0 Then
                                mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva, 1, "UNIDAD " & dt.Rows(i - 1).Item("numero_uni") & " : " & dt.Rows(i - 1).Item("descripcion_uni"))
                            End If
                            _codigo_uni = CInt(dt.Rows(i).Item("codigo_uni"))
                            _nro_eva = 0
                        End If
                        _nro_eva += 1
                        If _codigo_res <> CInt(dt.Rows(i).Item("codigo_res")) Then
                            If i <> 0 Then
                                _cant_ra += 1
                                mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res, 1, "RA " & _cant_ra & ": " & dt.Rows(i - 1).Item("descripcion_res"))
                            End If
                            _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                            _nro_res = 0
                        End If
                        _nro_res += 1
                        If _codigo_ind <> CInt(dt.Rows(i).Item("codigo_ind")) Then
                            If i <> 0 Then
                                _cant_ind += 1
                                mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "IND " & _cant_ind & ": " & _nombre_ind, _tooltip_ind)
                            End If
                            _codigo_ind = CInt(dt.Rows(i).Item("codigo_ind"))
                            _nombre_ind = dt.Rows(i).Item("descripcion_ind")
                            _peso_ind = CDbl(dt.Rows(i).Item("peso_ind").ToString) * 100
                            _tipo_prom = dt.Rows(i).Item("promedio_indicador").ToString

                            If _nombre_ind.Trim.Length > 45 Then
                                _tooltip_ind = _nombre_ind
                                _nombre_ind = _nombre_ind.Substring(0, 45) & "...<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ind).ToString("00.00") & "%") & "</span>"
                            Else
                                _tooltip_ind = String.Empty
                                _nombre_ind = _nombre_ind & "<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ind).ToString("00.00") & "%") & "</span>"
                            End If
                            _nro_ind = 0
                        End If
                        _nro_ind += 1
                    Next
                    mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva, 1, "UNIDAD " & dt.Rows(dt.Rows.Count - 1).Item("numero_uni") & " : " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_uni"))
                    _cant_ra += 1
                    mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res, 1, "RA " & _cant_ra & ": " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_res"))
                    _cant_ind += 1
                    mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "IND " & _cant_ind & ": " & _nombre_ind, _tooltip_ind)
                    If Session("gc_enviar") Then
                        mt_AgregarCabecera(objgridviewrow, objtablecell, 3, 3, "Condiciones", "En el caso que el corte se realize antes de la finalizar un resultado de aprendizaje, se tomara la última calificación como promedio")
                    End If
                    objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
                    objGridView.Controls(0).Controls.AddAt(1, objgridviewrow2)
                    objGridView.Controls(0).Controls.AddAt(2, objgridviewrow3)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    '    Dim txt As TextBox
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As Data.DataTable
    '    Dim _codigo_Dma, _codigo_eva, _codigo_nop As Integer
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        If fc_ValidarNotas(4) Then
    '            dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                obj.IniciarTransaccion()
    '                _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
    '                For j As Integer = 0 To Me.gvNotas.Rows.Count - 1
    '                    txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
    '                    _codigo_Dma = CInt(Me.gvNotas.DataKeys(j).Values("codigo_Dma"))
    '                    _codigo_nop = fc_traerCod(_codigo_Dma, _codigo_eva)
    '                    If _codigo_nop = -1 Then
    '                        obj.Ejecutar("DEA_NotasParciales_insertar", _codigo_Dma, _codigo_eva, CDbl(txt.Text.Trim), cod_user)
    '                    Else
    '                        If txt.Enabled Then
    '                            obj.Ejecutar("DEA_NotasParciales_actualizar", _codigo_nop, CDbl(txt.Text.Trim), cod_user)
    '                        End If
    '                    End If
    '                Next
    '                obj.TerminarTransaccion()
    '                Exit For
    '            Next
    '            mt_ShowMessage("¡ Se registraron Correctamente las calificaciones !", MessageType.Success)
    '        End If
    '    Catch ex As Exception
    '        obj.AbortarTransaccion()
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmResumenCalificador.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub gvNotas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvNotas.RowCommand
    '    Dim index As Integer
    '    Try
    '        index = CInt(e.CommandArgument)
    '        Select Case e.CommandName
    '            Case "CondInterna"
    '                Session("dea_row_index") = index
    '                Session("dea_tipo_cond") = 1
    '                Page.RegisterStartupScript("Pop", "<script>openModal2();</script>")
    '            Case "CondExterna"
    '                Session("dea_row_index") = index
    '                Session("dea_tipo_cond") = 2
    '                Page.RegisterStartupScript("Pop", "<script>openModal2();</script>")
    '        End Select
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim dt As New Data.DataTable
        Dim _codigo_emd As Integer '20190919-ENEVADO
        Dim msje As String = "", yesNo As String = ""
        Dim isV As String = Session("isValidated")

        Try
            If Session("isValidated") IsNot Nothing AndAlso Session("isValidated") = 2 Then
                If Session("gc_msjeNoEnlace") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("gc_msjeNoEnlace")) Then
                    yesNo = "no"
                    msje = String.Format("El proceso no puede continuar porque usted <b>no</b> ha enlazado las siguientes evaluaciones:<br/>{0}", Session("gc_msjeNoEnlace"))
                Else
                    If Session("gc_msjeEnlace") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("gc_msjeEnlace")) Then
                        yesNo = "sí"
                        msje = String.Format("Se encontraron calificaciones pendientes en:<br/>{0}.<br/>Si desea continuar, éstas se registrarán con <b>valor cero</b>", Session("gc_msjeEnlace"))
                    End If
                End If

                Session("isValidated") = 0
                If Session("gc_msjeNoEnlace") IsNot Nothing Or Session("gc_msjeEnlace") IsNot Nothing Then
                    Session("isValidated") = 2
                    ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & yesNo & "', '" & msje & "', 'danger');</script>")
                End If
            End If

            If Session("isValidated") IsNot Nothing AndAlso Session("isValidated") <> 2 Then
                If fc_ValidarCorteGrupo(Session("gc_codigo_cor"), Session("gc_codigo_cup"), Me.cboGrupo.SelectedValue) Then
                    dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)

                    For x As Integer = 0 To dt.Rows.Count - 1
                        _codigo_emd = dt.Rows(x).Item("codigo_emd") '20190919-ENEVADO
                        If _codigo_emd = -1 Then '20190919-ENEVADO
                            'If Not fc_ValidarNotas(dt.Rows(x).Item("codigo_eva")) Then
                            If Not fc_ValidarNotas(dt.Rows(x).Item("codigo_ins")) Then
                                Session("isValidated") = 0
                                Exit Sub
                            End If
                        End If
                    Next

                    If dt.Rows.Count > 0 Then
                        Session("isValidated") = 1
                    ElseIf dt.Rows.Count = 0 Then
                        mt_ShowMessage("No se ha encontrado ninguna calificación para Confirmar", MessageType.Warning)
                    End If

                    If Session("isValidated") IsNot Nothing AndAlso Session("isValidated") = 1 Then
                        'Comentar para proceder al registro por SMS
                        Call btnAceptar_Click(sender, e)

                        'Descomentar para proceder al registro por SMS
                        'divEnviar.Style.Item("display") = "block"
                    End If
                Else
                    Session("isValidated") = 0
                    'mt_ShowMessage("Ocurrió un problema en el envío o las calificaciones ya han sido enviadas anteriormente", MessageType.Warning)
                    mt_ShowMessage("Las calificaciones ya han sido confirmadas y enviadas anteriormente.", MessageType.Warning)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
    '    Dim txt As TextBox
    '    Dim obj As New ClsConectarDatos
    '    Dim dt, dtX As New Data.DataTable
    '    Dim _codigo_Dma, _codigo_eva, _codigo_nop, _codigo_coa, _codigo_niv As Integer
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        If fc_ValidarCorte(Session("gc_codigo_cor"), Session("gc_codigo_cup")) Then
    '            dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
    '            For x As Integer = 0 To dt.Rows.Count - 1
    '                If Not fc_ValidarNotas(dt.Rows(x).Item("codigo_eva")) Then
    '                    Exit Sub
    '                End If
    '            Next
    '            obj.IniciarTransaccion()
    '            dtX = obj.TraerDataTable("DEA_CortesCurso_insertar", Session("gc_codigo_cor"), Session("gc_codigo_cup"), cod_user)
    '            If dtX.Rows.Count > 0 Then
    '                _codigo_coa = CInt(dtX.Rows(0).Item(0))
    '                '_codigo_coa = 1
    '                For i As Integer = 0 To dt.Rows.Count - 1
    '                    _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
    '                    For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
    '                        txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
    '                        _codigo_Dma = CInt(Me.gvNotas.DataKeys(j).Values("codigo_Dma"))
    '                        _codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_eva)
    '                        _codigo_niv = fc_getCodNivelLogro(fc_getNota(_codigo_Dma, _codigo_eva))
    '                        'If _codigo_nop <> -1 Then
    '                        obj.Ejecutar("DEA_DetalleCortesCurso_insertar", _codigo_coa, _codigo_nop, _codigo_niv, cod_user)
    '                        'Else
    '                        'Throw New Exception("¡ No se encontro nota !" & _codigo_coa & " - " & _codigo_nop & " - " & cod_user & " - " & i & " - " & j)
    '                        'End If
    '                    Next
    '                Next
    '                mt_CargarNotas(Session("gc_codigo_cup"))
    '                mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
    '                mt_ShowMessage("¡ Se enviaron Correctamente las calificaciones !", MessageType.Success)
    '            End If
    '            obj.TerminarTransaccion()
    '        Else
    '            mt_ShowMessage("¡Ocurrio un problema en el envio!. Las calificaciones ya han sido enviadas", MessageType.Warning)
    '        End If
    '    Catch ex As Exception
    '        'obj.AbortarTransaccion()
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    ' 20191001-ENevado
    Protected Sub cboInterno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)

            Dim txtObsInt As TextBox = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("txtObsInterno"), TextBox)
            txtObsInt.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)

            Dim btnObsInt As LinkButton = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("btnObsInterno"), LinkButton)
            btnObsInt.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)

            If cbo.SelectedItem.Text.Trim.Equals("OTROS") Then txtObsInt.Focus()
            If cbo.SelectedItem.Text.Trim.Equals("OTROS") Then cbo.Visible = False : txtObsInt.Focus()

            Me.updNotas.Update()
            If Not cbo.Visible Then txtObsInt.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    ' 20191001-ENevado
    Protected Sub cboExterno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)

            Dim txtObsExt As TextBox = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("txtObsExterno"), TextBox)
            txtObsExt.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)

            Dim btnObsExt As LinkButton = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("btnObsExterno"), LinkButton)
            btnObsExt.Visible = IIf(cbo.SelectedItem.Text.Trim.Equals("OTROS"), True, False)

            If cbo.SelectedItem.Text.Trim.Equals("OTROS") Then txtObsExt.Focus()
            If cbo.SelectedItem.Text.Trim.Equals("OTROS") Then cbo.Visible = False : txtObsExt.Focus()

            Me.updNotas.Update()
            If Not cbo.Visible Then txtObsExt.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim _codigo_dma, _codigo_ccd, _codigo_coa, _codigo_con_int, _codigo_con_ext, _codigo_niv As Integer
        Dim _observacion_int, _observacion_ext As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If fc_ValidarCondicion() Then
                obj.IniciarTransaccion()
                For x As Integer = 1 To Me.gvNotas.Rows.Count - 1
                    Dim lbl As Label = CType(Me.gvNotas.Rows(x).FindControl("lblCalificacion"), Label)
                    _codigo_dma = CInt(Me.gvNotas.DataKeys(x).Values("codigo_Dma"))
                    _codigo_ccd = fc_getCodigoCcd(_codigo_dma)
                    _codigo_coa = fc_getCodigoCoa(Session("gc_codigo_cor"))
                    _codigo_niv = fc_getCodNivelLogro(CDbl(lbl.Text.Trim))
                    _codigo_con_int = -1 : _codigo_con_ext = -1
                    _observacion_int = "" : _observacion_ext = ""

                    If CDbl(lbl.Text.Trim) < CDbl("13.5") Then
                        Dim cboInt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboInterno"), DropDownList)
                        Dim cboExt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboExterno"), DropDownList)

                        _codigo_con_int = cboInt.SelectedValue
                        _codigo_con_ext = cboExt.SelectedValue

                        If Not cboInt.Visible Or cboInt.SelectedItem.Text.Trim.Equals("OTROS") Then _observacion_int = CType(Me.gvNotas.Rows(x).FindControl("txtObsInterno"), TextBox).Text
                        If Not cboExt.Visible Or cboExt.SelectedItem.Text.Trim.Equals("OTROS") Then _observacion_ext = CType(Me.gvNotas.Rows(x).FindControl("txtObsExterno"), TextBox).Text

                        'If cboInt.SelectedItem.Text = "OTROS" Then
                        '    Dim btnObsInt As LinkButton = CType(Me.gvNotas.Rows(x).FindControl("btnObsInterno"), LinkButton)
                        '    _observacion_int = btnObsInt.Attributes("obsInt")
                        'End If

                        'If cboExt.SelectedItem.Text = "OTROS" Then
                        '    Dim btnObsExt As LinkButton = CType(Me.gvNotas.Rows(x).FindControl("btnObsExterno"), LinkButton)
                        '    _observacion_ext = btnObsExt.Attributes("obsExt")
                        'End If

                        'Throw New Exception("OK: " & _codigo_ccd & " : " & _codigo_coa & " : " & _codigo_dma & " : " & _codigo_con_int & " : " & _codigo_con_ext & " : " & CDbl(lbl.Text.Trim) & " : " & _codigo_niv & " : " & cod_user)
                    End If

                    If _codigo_ccd = -1 Then
                        'obj.Ejecutar("DEA_CortesCurso_Condicion_insertar", _codigo_coa, _codigo_dma, _codigo_con_int, _codigo_con_ext, CDbl(lbl.Text.Trim), _observacion_int, _observacion_ext, _codigo_niv, cod_user)
                        obj.Ejecutar("DEA_CortesCurso_Condicion_insertar2", _codigo_coa, _codigo_dma, _codigo_con_int, _codigo_con_ext, CDbl(lbl.Text.Trim), _observacion_int, _observacion_ext, _codigo_niv, cod_user, Me.cboGrupo.SelectedValue)
                    Else
                        obj.Ejecutar("DEA_CortesCurso_Condicion_actualizar", _codigo_ccd, _codigo_con_int, _codigo_con_ext, _observacion_int, _observacion_ext, _codigo_niv, cod_user, CDbl(lbl.Text.Trim))
                    End If
                Next

                obj.TerminarTransaccion()
                mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_ShowMessage("¡ Se registró correctamente la información !", MessageType.Success)
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If Session("isTextValid") = 1 Then
            Dim txt As TextBox
            Dim lbl As Label
            Dim obj As New ClsConectarDatos
            Dim dt, dtX As New Data.DataTable
            Dim _codigo_Dma, _codigo_nop, _codigo_coa, _codigo_niv As Integer
            Dim _codigo_emd, _codigo_ins, _codigo_pso, _codigo_cup As Integer '20190919-ENEVADO
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            Try
                If fc_ValidarCorteGrupo(Session("gc_codigo_cor"), Session("gc_codigo_cup"), Me.cboGrupo.SelectedValue) Then
                    dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                    If dt.Rows.Count = 0 Then
                        mt_ShowMessage("¡ No hay evaluaciones configuradas para este corte !", MessageType.Warning)
                        Exit Sub
                    End If
                    _codigo_cup = Session("gc_codigo_cup")
                    For x As Integer = 0 To dt.Rows.Count - 1
                        _codigo_emd = dt.Rows(x).Item("codigo_emd") '20190919-ENEVADO
                        If Not fc_ValidarNotasMoodle(dt.Rows(x).Item("codigo_ins")) Then
                            Exit Sub
                        End If
                    Next
                    obj.IniciarTransaccion()
                    dtX = obj.TraerDataTable("DEA_CortesCurso_insertar", Session("gc_codigo_cor"), Session("gc_codigo_cup"), cod_user)
                    If dtX.Rows.Count > 0 Then
                        _codigo_coa = CInt(dtX.Rows(0).Item(0))
                        '_codigo_coa = 1
                        For i As Integer = 0 To dt.Rows.Count - 1
                            _codigo_emd = dt.Rows(i).Item("codigo_emd") '20190919-ENEVADO
                            If _codigo_emd = -1 Then '20190919-ENEVADO
                                _codigo_ins = CInt(dt.Rows(i).Item("codigo_ins"))
                                For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                                    txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
                                    _codigo_Dma = CInt(Me.gvNotas.DataKeys(j).Values("codigo_Dma"))
                                    _codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_ins)
                                    _codigo_niv = fc_getCodNivelLogro(fc_getNota(_codigo_Dma, _codigo_ins, _codigo_cup)) '20190919-ENEVADO
                                    obj.Ejecutar("DEA_DetalleCortesCurso_insertar", _codigo_coa, _codigo_nop, _codigo_niv, cod_user)
                                Next
                            Else
                                '20190919-ENEVADO -----------------------------------------------------------------------------------------------------------------\
                                Dim dtNotaAux As Data.DataTable
                                _codigo_ins = CInt(dt.Rows(i).Item("codigo_ins"))
                                For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                                    Dim _nota_aux As Double = 0
                                    Dim _sin_calificar As Integer = 0
                                    Dim _nota_mdl, _nota_cor_ant As String
                                    lbl = Me.gvNotas.Rows(j).FindControl("lblNota" & (i + 1))
                                    _codigo_Dma = CInt(Me.gvNotas.DataKeys(j).Values("codigo_Dma"))
                                    _codigo_pso = CInt(Me.gvNotas.DataKeys(j).Values("codigo_pso")) '20190919-ENEVADO
                                    '_nota_mdl = fc_getNotaMoodle(_codigo_pso, _codigo_ins) 'Comentado por Luis Q.T | 13DIC2019

                                    _nota_cor_ant = fc_getNota(_codigo_Dma, _codigo_ins, _codigo_cup)
                                    If _nota_cor_ant <> "" Then
                                        _nota_mdl = _nota_cor_ant
                                    Else
                                        _nota_mdl = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                    End If

                                    If _nota_mdl <> "" Then _nota_aux = Math.Round(CDbl(_nota_mdl), 2) : _sin_calificar = 1
                                    dtNotaAux = obj.TraerDataTable("DEA_NotasParciales_insertar", _codigo_Dma, -1, _nota_aux, _codigo_cup, _codigo_ins, _codigo_emd, cod_user, _sin_calificar)
                                    If dtNotaAux.Rows.Count > 0 Then
                                        _codigo_nop = dtNotaAux.Rows(0).Item(0)
                                        _codigo_niv = fc_getCodNivelLogro(_nota_aux)
                                        '_codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_ins)
                                        '_codigo_niv = fc_getCodNivelLogro(fc_getNota(_codigo_Dma, _codigo_ins, _codigo_cup)) '20190919-ENEVADO
                                        obj.Ejecutar("DEA_DetalleCortesCurso_insertar2", _codigo_coa, _codigo_nop, _codigo_niv, cod_user, Me.cboGrupo.SelectedValue)
                                    End If
                                Next
                                '-----------------------------------------------------------------------------------------------------------------------------------/
                            End If
                        Next

                        Call mt_CargarNotas(Session("gc_codigo_cup"))
                        Call mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"), Me.cboGrupo.SelectedValue)
                        Call mt_MostrarLeyendaNotas()
                        mt_ShowMessage("¡ Se enviaron Correctamente las calificaciones !", MessageType.Success)
                    End If
                    obj.TerminarTransaccion()
                Else
                    mt_ShowMessage("¡Ocurrió un problema en el envío!. Las calificaciones ya han sido enviadas", MessageType.Warning)
                End If
            Catch ex As Exception
                obj.AbortarTransaccion()
                mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            End Try
        End If
    End Sub

    ' 20191001-ENevado
    Protected Sub btnObsInterno_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btnObsInt As LinkButton = sender
            'Me.txtObservacion.Text = btnObsInt.Attributes("obsInt")

            Dim row As GridViewRow = CType(btnObsInt.Parent.Parent, GridViewRow)
            Dim cboInterno As DropDownList = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("cboInterno"), DropDownList)
            Dim txtObsInt As TextBox = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("txtObsInterno"), TextBox)

            cboInterno.SelectedIndex = 0
            cboInterno.Visible = True
            txtObsInt.Visible = False
            btnObsInt.Visible = False

            Me.updNotas.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    ' 20191001-ENevado
    Protected Sub btnObsExterno_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim btnObsExt As LinkButton = sender
            'Me.txtObservacion.Text = btnObsExt.Attributes("obsExt")

            Dim row As GridViewRow = CType(btnObsExt.Parent.Parent, GridViewRow)
            Dim cboExterno As DropDownList = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("cboExterno"), DropDownList)
            Dim txtObsExt As TextBox = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("txtObsExterno"), TextBox)

            cboExterno.SelectedIndex = 0
            cboExterno.Visible = True
            txtObsExt.Visible = False
            btnObsExt.Visible = False

            Me.updNotas.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    ' 20191002-ENevado
    'Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    '    Dim index As Integer
    '    Dim _tipo As Integer
    '    Try
    '        index = Session("dea_row_index")
    '        _tipo = CInt(Session("dea_tipo_cond"))
    '        If _tipo = 1 Then
    '            Dim btnObsInt As LinkButton = CType(Me.gvNotas.Rows(index).FindControl("btnObsInterno"), LinkButton)
    '            btnObsInt.Attributes("obsInt") = Me.txtObservacion.Text
    '        Else
    '            Dim btnObsExt As LinkButton = CType(Me.gvNotas.Rows(index).FindControl("btnObsExterno"), LinkButton)
    '            btnObsExt.Attributes("obsExt") = Me.txtObservacion.Text
    '        End If
    '        Me.txtObservacion.Text = String.Empty
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    ' 20191015-ENevado
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim _codigo_nop, _codigo_ece, _codigo_pso, _codigo_ins, _codigo_niv As Integer
        Dim _nota_aux As String
        Dim _nota As Double
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim btn As LinkButton = sender
            _codigo_nop = btn.Attributes("codigo_nop")
            _codigo_ece = btn.Attributes("codigo_ece")
            _codigo_pso = btn.Attributes("codigo_pso")
            _codigo_ins = btn.Attributes("codigo_ins")
            _nota_aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
            If _nota_aux <> "" Then
                _nota = Math.Round(CDbl(_nota_aux), 2)
            Else
                _nota = 0
            End If
            '_nota = 16
            _codigo_niv = fc_getCodNivelLogro(_nota)
            'Me.txtObservacion.Text = btnObsExt.Attributes("obsExt")
            obj.IniciarTransaccion()
            obj.Ejecutar("DEA_NotasParciales_actualizar", _codigo_nop, _nota, _codigo_niv, cod_user)
            obj.Ejecutar("DEA_EvaluacionCurso_Edicion_actualizar", _codigo_ece, cod_user)
            obj.TerminarTransaccion()
            mt_CargarNotas(Session("gc_codigo_cup"))
            mt_CargarEdiciones(Session("gc_codigo_cup"))
            mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"), Me.cboGrupo.SelectedValue)
            mt_MostrarLeyendaNotas()
            mt_ShowMessage("¡ Se actualizo la nota correctamente !", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        Try
            If Me.cboGrupo.SelectedValue = -1 Then
                btnEnviar.Visible = False
                mt_ShowMessage("¡ Seleccione una Grupo !", MessageType.Warning)
                mt_CrearColumns(Session("gc_codigo_cup"), Session("gc_fecha_ini"), Session("gc_fecha_fin"), 0)
                mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"), 0)
                Me.cboGrupo.Focus()
                Exit Sub
            End If
            mt_CargarNotas(Session("gc_codigo_cup"))
            mt_CargarEdiciones(Session("gc_codigo_cup"))
            If Session("gc_enviar") Then mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
            mt_CrearColumns(Session("gc_codigo_cup"), Session("gc_fecha_ini"), Session("gc_fecha_fin"), Me.cboGrupo.SelectedValue)
            mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"), Me.cboGrupo.SelectedValue)
            mt_MostrarLeyendaNotas()
            Me.btnGuardar.Visible = Session("gc_enviar")
            'If fc_CorteEnviado(Session("gc_codigo_cor"), Session("gc_codigo_cup"), cod_user, cod_ctf) Then
            '    Me.btnEnviar.Visible = False
            'End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Confirmar SMS"

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim dt As New Data.DataTable()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            If Session("isValidated") IsNot Nothing AndAlso Session("isValidated") = 1 Then
                If fc_ValidarCorte(Session("gc_codigo_cor"), Session("gc_codigo_cup")) Then
                    obj.AbrirConexion()
                    dt = obj.TraerDataTable("DEA_GenerarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA " & fc_getDescripcionSemana(Session("gc_codigo_cor")))
                    obj.CerrarConexion()

                    If dt.Rows.Count > 0 Then
                        Dim rpta As String = dt.Rows(0).Item(0).ToString
                        Dim msg As String = dt.Rows(0).Item(1).ToString

                        If rpta.Equals("1") Then
                            Me.divAlertModal.Visible = False
                            Me.lblMensaje.InnerText = ""
                            updMensaje.Update()

                            Page.RegisterStartupScript("Pop", "<script>StartCount(); openModal();</script>")
                            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtToken)
                        Else
                            If rpta.Equals("") Or msg.Equals("") Then
                                mt_ShowMessage("No se pudo procesar la petición. Verifique que el número esté operativo", MessageType.Info)
                            Else
                                If msg.Contains("msxml") Then
                                    If dt.Rows(0).Item("description") IsNot Nothing Then
                                        msg = dt.Rows(0).Item("description").ToString()
                                    End If
                                End If

                                If String.IsNullOrEmpty(msg) Then
                                    msg = "No se pudo enlazar con el servidor SMS. Por favor, intente nuevamente más tarde"
                                End If

                                mt_ShowMessage(msg, MessageType.Info)
                            End If
                        End If
                    Else
                        mt_ShowMessage("El número celular no existe", MessageType.Info)
                    End If

                    dt.Dispose()
                Else
                    mt_ShowMessage("Las calificaciones ya han sido confirmadas y enviadas anteriormente", MessageType.Warning)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidar.ServerClick
        Try
            Dim valid As Generic.Dictionary(Of String, String) = fc_Validar()

            If valid.Item("rpta") = 1 Then
                Me.divAlertModal.Visible = False
                Me.validar.Value = "1"
                updMensaje.Update()
            Else
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
                ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)

                Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub

    Private Sub mt_ObtenerCelular()
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ObtenerTelefonoPersonal", cod_user, "X")
            obj.CerrarConexion()
            'mt_ShowMessage("cod_user: " & cod_user, MessageType.Warning)
            If dt.Rows.Count > 0 Then
                lblTelefono.Text = dt.Rows(0).Item(0).ToString()
            End If

            If String.IsNullOrEmpty(lblTelefono.Text) Or lblTelefono.Text.Contains("configurado") Then
                btnSend.Visible = True
                btnGenerar.Visible = False
            Else
                btnSend.Visible = False
                btnGenerar.Visible = True
            End If

            dt.Dispose()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_ObtenerInfoPersonal()
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "ID", Session("gc_codigo_cac"), Session("gc_codigo_cor"), Session("gc_codigo_cup"))
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Dim sbMail As StringBuilder = New StringBuilder()
                Dim sbWsp As StringBuilder = New StringBuilder()
                Dim asunto As String
                Dim semana As String = dt.Rows(0).Item("numeroSemana_cor").ToString()
                Dim corte As String = dt.Rows(0).Item("descripcion_cor").ToString()
                Dim esCondicion As Boolean = False

                If Session("gc_enviar") IsNot Nothing Then
                    esCondicion = Session("gc_enviar")
                End If

                asunto = "Completar notas de la Semana " & semana & " del curso " & dt.Rows(0).Item("nombre_Cur").ToString() & "(" & dt.Rows(0).Item("grupoHor_Cup").ToString() & ") - " & dt.Rows(0).Item("abreviatura_Cpf").ToString()

                sbMail.Append("Estimado(a) docente,%0A")
                sbMail.Append(String.Format("Hemos comprobado que usted no ha completado las {0} del {1} correspondiente a la Semana {2}", IIf(esCondicion, "condiciones internas y externas", "notas"), corte, semana))
                sbMail.Append(" del curso " & dt.Rows(0).Item("nombre_Cur").ToString() & "(" & dt.Rows(0).Item("grupoHor_Cup").ToString() & "), " & dt.Rows(0).Item("abreviatura_Cpf").ToString() & ".%0A")
                sbMail.Append(String.Format("Por favor, ingrese a su campus y llene las {0} faltantes.%0A", IIf(esCondicion, "condiciones", "notas")))
                sbMail.Append("Gracias,%0A")
                sbMail.Append("Atte. Calidad Universitaria.")

                sbWsp.Append("%E2%9A%A0%20Estimado%28a%29%20docente%2C%0A")
                sbWsp.Append(String.Format("Hemos%20comprobado%20que%20usted%20no%20ha%20completado%20las%20{0}%20del%20{1}%20correspondiente%20a%20la%20Semana%20{2}%20", IIf(esCondicion, "condiciones%20internas%20y%20externas", "notas"), corte, semana))
                sbWsp.Append("del%20curso%20" & dt.Rows(0).Item("nombre_Cur").ToString().Replace(" ", "%20") & "(" & dt.Rows(0).Item("grupoHor_Cup").ToString().Replace(" ", "%20") & "),%20" & dt.Rows(0).Item("abreviatura_Cpf").ToString().Replace(" ", "%20") & ".%0A")
                sbWsp.Append(String.Format("Por%20favor%20ingrese%20a%20su%20campus%20y%20llene%20las%20{0}%20faltantes.%0A", IIf(esCondicion, "condiciones%20internas%20y%20externas", "notas"), corte, semana))
                sbWsp.Append("Gracias%2C%0A")
                sbWsp.Append("Atte.%20Calidad%20Universitaria.")


                lblDocente.InnerText = dt.Rows(0).Item("docente").ToString()

                If dt.Rows(0).Item("celular").ToString().Length > 8 Then
                    lblCelular.Text = dt.Rows(0).Item("celular").ToString().Substring(0, 9)

                    If cod_ctf = 13 Then
                        lblCelular.NavigateUrl = "#"
                        lblCelular.Enabled = False
                    Else
                        lblCelular.NavigateUrl = "https://api.whatsapp.com/send?phone=51" & dt.Rows(0).Item("celular").ToString().Substring(0, 9) & "&text=" & sbWsp.ToString()
                        lblCelular.Enabled = True
                    End If
                Else
                    lblCelular.NavigateUrl = "#"
                    lblCelular.Text = "No configurado"
                    lblCelular.Enabled = False
                End If

                lblEmail.Text = dt.Rows(0).Item("email").ToString()

                If cod_ctf = 13 Then
                    lblEmail.NavigateUrl = "#"
                    lblEmail.Enabled = False
                Else
                    lblEmail.NavigateUrl = "mailto:" & dt.Rows(0).Item("email").ToString() & "?subject=" & asunto & "&body=" & sbMail.ToString
                    lblEmail.Enabled = True
                End If

                panInfo.Update()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        Finally
            dt.Dispose()
        End Try
    End Sub

    Private Function fc_Validar() As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        Dim err As Boolean = False
        valid.Add("rpta", 1)
        valid.Add("msg", "")
        valid.Add("control", "")

        'Descomentar para proceder al registro por SMS
        'Try
        '    If Not err And String.IsNullOrEmpty(Request("txtToken")) Then
        '        If Not err Then
        '            valid.Item("rpta") = 0
        '            valid.Item("msg") = "Debe ingresar el código enviado por SMS"
        '            valid.Item("control") = "txtToken"
        '            err = True
        '            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtToken)
        '        End If
        '        txtToken.Attributes.Item("data-error") = "true"
        '    Else
        '        txtToken.Attributes.Item("data-error") = "false"
        '    End If

        '    If Not err Then
        '        Dim dt As New Data.DataTable
        '        Dim obj As New ClsConectarDatos
        '        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        '        obj.AbrirConexion()
        '        dt = obj.TraerDataTable("DEA_ConfirmarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA " & fc_getDescripcionSemana(Session("gc_codigo_cor")), txtToken.Text)
        '        obj.CerrarConexion()

        '        If dt.Rows.Count > 0 Then
        '            Dim rpta As String = dt.Rows(0).Item(0).ToString
        '            Dim msg As String = dt.Rows(0).Item(1).ToString

        '            Session("isTextValid") = rpta
        '            valid.Item("rpta") = rpta
        '            valid.Item("msg") = msg
        '        Else
        '            Session("isTextValid") = 0
        '            valid.Item("rpta") = 0
        '            valid.Item("msg") = "No se pudo confirmar el token, intente generar uno nuevo"
        '        End If

        '        valid.Item("control") = "txtToken"
        '        dt.Dispose()
        '    End If
        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        'End Try

        Return valid
    End Function

    Private Function fc_getDescripcionSemana(ByVal codigo_cor As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Dim des As String
        dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_cor = " & codigo_cor, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                des = dt.Rows(0).Item("descripcion").ToString()
                des = des.Split(" ")(0) & " " & des.Split(" ")(1)
                Return des
            End If
        End If

        Return ""
    End Function

    Private Function fc_getDescripcionCorte(ByVal codigo_cor As Integer) As String
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim des As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCorteSemestre", Session("gc_codigo_cac"), "P", codigo_cor)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                des = dt.Rows(0).Item("nombre_sem").ToString().ToUpper()
            End If
        Catch ex As Exception
            If ex.Source.Contains("Provider") Then
                mt_ShowMessage("La sesión del usuario <b>ha caducado</b>. Regrese a la pantalla anterior y vuelva a intentar.", MessageType.Error)
            Else
                mt_ShowMessage(ex.Message.Replace("'", " ") & " " & ex.Source, MessageType.Error)
            End If
        Finally
            dt.Dispose()
        End Try

        Return des
    End Function

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            Me.validar.Value = "0"
            updMensaje.Update()
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cup As Integer, ByVal codigo_gru As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "CM", codigo_cac, codigo_cup, codigo_gru)
            obj.CerrarConexion()
            Session("gc_dtAlumnos") = dt
            Me.gvNotas.DataSource = dt
            Me.gvNotas.DataBind()
            Me.updNotas.Update()
            'mt_ShowMessage(codigo_cac & " - " & codigo_cup & " - " & dt.Rows.Count, MessageType.Warning)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearColumns(ByVal codigo_cup As Integer, ByVal fecha_ini As Date, ByVal fecha_fin As Date, ByVal codigo_gru As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim _peso_ins, _tipo_prom As String

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarEvaluacionesCortes", "CM", codigo_cup, fecha_ini, fecha_fin, codigo_gru)
            obj.CerrarConexion()
            'Throw New Exception("codigo_cup: " & codigo_cup & " fecha_ini: " & fecha_ini & " fecha_fin: " & fecha_fin)
            Session("gc_dtEvaluacion") = dt
            Dim tfield As New TemplateField()
            If Me.gvNotas.Columns.Count > nro_col Then
                For i As Integer = 3 To Me.gvNotas.Columns.Count
                    Me.gvNotas.Columns.RemoveAt(Me.gvNotas.Columns.Count - 1)
                Next
            End If
            For x As Integer = 0 To dt.Rows.Count - 1
                _peso_ins = CDbl(dt.Rows(x).Item("peso_ins").ToString) * 100
                _tipo_prom = dt.Rows(x).Item("promedio_instrumento")

                tfield = New TemplateField()
                'tfield.HeaderText = dt.Rows(x).Item("fecha_gru")
                tfield.HeaderText = dt.Rows(x).Item("descripcion_eva") & "<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ins).ToString("00.00") & "%") & "</span>"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                'tfield.HeaderStyle.Width = (60 / dt.Rows.Count)
                Me.gvNotas.Columns.Add(tfield)
            Next
            If Session("gc_enviar") Then
                tfield = New TemplateField()
                tfield.HeaderText = "Calificación"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.gvNotas.Columns.Add(tfield)

                tfield = New TemplateField()
                tfield.HeaderText = "Internas"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                'tfield.HeaderStyle.Width = "130"
                tfield.HeaderStyle.Width = New Unit("130px")
                tfield.ItemStyle.Width = New Unit("130px")
                tfield.FooterStyle.Width = New Unit("130px")
                Me.gvNotas.Columns.Add(tfield)

                tfield = New TemplateField()
                tfield.HeaderText = "Externas"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                'tfield.HeaderStyle.Width = "130"
                tfield.HeaderStyle.Width = New Unit("130px")
                tfield.ItemStyle.Width = New Unit("130px")
                tfield.FooterStyle.Width = New Unit("130px")
                Me.gvNotas.Columns.Add(tfield)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal rowspan As Integer, ByVal celltext As String, Optional ByVal tooltip As String = "")
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.RowSpan = rowspan
        objtablecell.ToolTip = tooltip
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.VerticalAlign = VerticalAlign.Middle
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvNotas.Rows
                gvNotas_OnRowDataBound(Me.gvNotas, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarNotas(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt, dtNM As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim codigo_cor As String = Session("gc_codigo_cor")
            codigo_cor = IIf(String.IsNullOrEmpty(Session("gc_codigo_cor")), "-1", Session("gc_codigo_cor"))

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_NotasParciales_listar", "", -1, -1, "P", codigo_cup, codigo_cor) 'Por Luis Q.T | 10DIC2019: Obtener las notas del corte
            dtNM = obj.TraerDataTable("DEA_NotasParciales_listar", "CE", -1, -1, "P", codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtNotas") = dt
            Session("gc_dtNotasMoodle") = dtNM
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarEdiciones(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim codigo_cor As String = Session("gc_codigo_cor")
            codigo_cor = IIf(String.IsNullOrEmpty(Session("gc_codigo_cor")), "-1", Session("gc_codigo_cor"))

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_EvaluacionCurso_Edicion_listar", "", codigo_cup, -1, codigo_cor) 'Por Luis Q.T | 10DIC2019: Obtener las autorizaciones de notas del corte
            obj.CerrarConexion()
            Session("gc_dtNotasEditar") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCondiciones()
        Dim obj As New ClsConectarDatos
        Dim dtInt, dtExt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dtInt = obj.TraerDataTable("DEA_CondicionEstudiante_listar", "CE", -1, 1)
            dtExt = obj.TraerDataTable("DEA_CondicionEstudiante_listar", "CE", -1, 2)
            obj.CerrarConexion()
            Session("gc_dtCondInt") = dtInt
            Session("gc_dtCondExt") = dtExt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCondiciones(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_Condicion_listar", "", -1, codigo_cor, codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtCondiciones") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCortes(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "", -1, codigo_cor, codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtCorteCurso") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_MostrarLeyenda()
        Dim lbl As New Label()
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNivelLogro"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(dt, "", "rangoDesde_niv ASC", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                For i As Integer = 0 To dt.Rows.Count - 1
                    lbl = New Label()
                    lbl.Text = " " & dt.Rows(i).Item("nombre_niv") & " "
                    'lbl.CssClass = "col-md-1"
                    lbl.Font.Size = 8
                    lbl.Font.Bold = True
                    Me.divLeyenda.Controls.Add(lbl)
                    lbl = New Label()
                    lbl.ForeColor = Drawing.Color.Transparent
                    lbl.Style.Add("background-color", dt.Rows(i).Item("color_niv"))
                    lbl.Text = "____"
                    Me.divLeyenda.Controls.Add(lbl)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_MostrarLeyendaNotas()
        Dim dtN As New Data.DataTable
        Dim dtM As New Data.DataTable
        Dim dtI As New Data.DataTable
        Dim dtA As New Data.DataTable

        'Dim dvN As Data.DataView
        'Dim dvM As Data.DataView
        Dim dvEnl As Data.DataView
        Dim dvNoEnl As Data.DataView

        Try
            If Not Session("gc_enviar") Then
                Dim totEval As Integer = 0, totEnv As Integer = 0, totEnl As Integer = 0, totNoEnl As Integer = 0
                Dim totParc As Integer = 0, totSN As Integer = 0, totCN As Integer = 0
                Dim iconY As String = "far fa-check-circle", iconN As String = "far fa-times-circle", iconW As String = "fas fa-exclamation-triangle"
                Dim colorY As String = "color: green", colorN As String = "color: red", colorW As String = "color: darkorange"
                Dim flag As Boolean = True
                Dim totalNotas As Integer = 0

                dtN = CType(Session("gc_dtNotas"), Data.DataTable)
                dtM = CType(Session("gc_dtNotasMoodle"), Data.DataTable)
                dtI = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)
                totEval = dtI.Rows.Count

                ' If (dtN.Rows.Count + dtM.Rows.Count) > 0 And dtI.Rows.Count > 0 Then

                ' 2020-07-03 ENevado -----------------------------------------------------------------
                If dtA Is Nothing Then
                    Throw New Exception("¡ No hay alumnos asignados en el sub-grupo seleccionado !")
                End If
                ' -------------------------------------------------------------------------------------

                If dtI.Rows.Count > 0 Then
                    For i As Integer = 0 To dtI.Rows.Count - 1
                        Dim _codigo_ins As String = dtI.Rows(i).Item("codigo_ins").ToString

                        'Si tuviera notas en tabla NotasParciales
                        totalNotas = fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "0")
                        If totalNotas > 0 AndAlso totalNotas >= (dtA.Rows.Count - 1) Then
                            totEnv += 1
                        Else
                            'Si tuviera notas en tabla Moodle
                            totalNotas = fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "1")
                            If totalNotas > 0 AndAlso totalNotas >= (dtA.Rows.Count - 1) Then
                                totCN += 1 'Total con nota
                            Else
                                If totalNotas = 0 Then
                                    totSN += 1
                                Else
                                    If CInt(dtI.Rows(i).Item("codigo_emd").ToString) <> -1 Then
                                        totParc += 1
                                    End If
                                End If
                            End If
                        End If
                    Next

                    dvEnl = New Data.DataView(dtI, "codigo_emd <> -1", "", Data.DataViewRowState.CurrentRows)
                    'dvNoEnl = New Data.DataView(dtI, "codigo_emd = -1", "", Data.DataViewRowState.CurrentRows)

                    totEnl = dvEnl.ToTable.Rows.Count '+ totEnv
                    'totNoEnl = dvNoEnl.ToTable.Rows.Count - totEnv
                    'totNoEnl = IIf(totNoEnl < 0, 0, totNoEnl)
                End If

                totNoEnl = totEval - totEnl

                lblEval.InnerText = CStr(totEval)
                lblEnv.InnerText = CStr(totEnv)
                lblEnl.InnerText = CStr(totEnl)
                lblNoEnl.InnerText = CStr(totNoEnl)
                lblParc.InnerText = CStr(totParc)
                lblSN.InnerText = CStr(totSN)

                icoEval.Attributes.Item("class") = IIf(totEval = 0, iconN, iconY)
                icoEval.Attributes.Item("style") = IIf(totEval = 0, colorN, colorY)
                icoEnv.Attributes.Item("class") = IIf(totEnv = 0, iconN, iconY)
                icoEnv.Attributes.Item("style") = IIf(totEnv = 0, colorN, colorY)
                icoEnl.Attributes.Item("class") = IIf(totEnl = 0, iconN, iconY)
                icoEnl.Attributes.Item("style") = IIf(totEnl = 0, colorN, colorY)
                icoNoEnl.Attributes.Item("class") = IIf(totNoEnl <> 0, iconN, iconY)
                icoNoEnl.Attributes.Item("style") = IIf(totNoEnl <> 0, colorN, colorY)
                icoParc.Attributes.Item("class") = IIf(totParc <> 0, iconW, iconY)
                icoParc.Attributes.Item("style") = IIf(totParc <> 0, colorW, colorY)
                icoSN.Attributes.Item("class") = IIf(totSN <> 0, iconN, iconY)
                icoSN.Attributes.Item("style") = IIf(totSN <> 0, colorN, colorY)

                If flag And totEval = 0 Then flag = False
                'If flag And totEnv <> 0 Then flag = False
                If flag And totEnl = 0 Then flag = False
                If flag And totNoEnl <> 0 Then flag = False
                'If flag And totParc <> 0 Then flag = False --> Las notas parciales no restringe que las notas sean enviadas
                If flag And totSN <> 0 Then flag = False

                btnEnviar.Enabled = flag
                btnEnviar.Visible = flag

                If flag Then
                    If totEval - totEnv = 0 Then
                        btnEnviar.Enabled = Not flag
                        btnEnviar.Visible = Not flag
                    End If
                Else
                    Session("isValidated") = 2
                End If

                divResumen.Style.Item("display") = "block"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarGrupos(ByVal codigo_cup As Integer, ByVal enviar As Boolean, ByVal codigo_cor As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_subGrupoProgramado_listar", codigo_cup, IIf(enviar, codigo_cor, -1), -1, IIf(enviar, "GE", "CA"))
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboGrupo, dt, "codigo_sgr", "descripcion_sgr")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_ValidarNotas(ByVal codigo_ins As Integer) As Boolean
        Dim txt As Label 'TextBox
        Dim _nombre_alu As String
        Dim _codigo_emd As Integer '20190919-ENEVADO
        Dim dt As Data.DataTable
        dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)

        For i As Integer = 0 To dt.Rows.Count - 1
            If codigo_ins = CInt(dt.Rows(i).Item("codigo_ins")) Then '20190919-ENEVADO
                _codigo_emd = CInt(dt.Rows(i).Item("codigo_emd")) '20190919-ENEVADO
                For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                    If _codigo_emd = -1 Then '20190919-ENEVADO
                        txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
                        _nombre_alu = Me.gvNotas.DataKeys(j).Values("nombre_alu")
                        If String.IsNullOrEmpty(txt.Text.Trim) Then
                            Session("isValidated") = 0
                            mt_ShowMessage("¡ Ingrese Calificación al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                            txt.Focus()
                            Me.updNotas.Update()
                            Return False
                        End If
                        If CDbl(txt.Text.Trim) < 0 Or CDbl(txt.Text.Trim) > 20 Then
                            Session("isValidated") = 0
                            mt_ShowMessage("¡ Ingrese Calificación al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                            txt.Focus()
                            Me.updNotas.Update()
                            Return False
                        End If
                    End If
                Next
                Exit For
            End If
        Next

        Session("isValidated") = 1
        Return True
    End Function

    Private Function fc_getNota(ByVal codigo_dma As Integer, ByVal codigo_ins As Integer, ByVal codigo_cup As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNotas"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma & " AND codigo_ins = " & codigo_ins & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("nota_nop").ToString
            End If
        End If
        Return String.Empty
    End Function

    Private Function fc_getCodNota(ByVal codigo_dma As Integer, ByVal codigo_ins As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNotas"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma & " AND codigo_ins = " & codigo_ins, "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("codigo_nop")
                End If
            End If
            Return -1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_tieneNota(ByVal codigo_cup As Integer, ByVal codigo_ins As Integer, ByVal isMoodle As String) As Integer
        Dim dtA As New Data.DataTable
        Dim dtN As New Data.DataTable
        Dim dtM As New Data.DataTable
        Dim dvN As Data.DataView
        Dim dvM As Data.DataView
        Dim _codigo_pso As String = ""
        Dim _codigo_dma As String = ""

        Try
            dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)

            'isMoodle: 1:Sí, 0:No, 2:Ambos
            If isMoodle.Equals("0") Or isMoodle.Equals("2") Then
                For i As Integer = 1 To dtA.Rows.Count - 1
                    _codigo_dma += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_dma").ToString()
                Next
                dtN = CType(Session("gc_dtNotas"), Data.DataTable)
                dvN = New Data.DataView(dtN, "codigo_cup = " & codigo_cup & " AND codigo_ins = " & codigo_ins & " AND estado_nop = 'E'", "", Data.DataViewRowState.CurrentRows)
                dtN = dvN.ToTable
            End If

            If isMoodle.Equals("1") Or isMoodle.Equals("2") Then
                For i As Integer = 1 To dtA.Rows.Count - 1
                    _codigo_pso += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_pso").ToString()
                Next

                dtM = CType(Session("gc_dtNotasMoodle"), Data.DataTable)
                'dtM = CType(Session("gc_dtNotas"), Data.DataTable) 'Comentado por Luis Q.T. | 13DIC2019
                dvM = New Data.DataView(dtM, "codigo_ins = " & codigo_ins & " AND username IN ( " & _codigo_pso & " ) AND NOT nota_mdl IS NULL", "", Data.DataViewRowState.CurrentRows)
                'dvM = New Data.DataView(dtM, "codigo_cup = " & codigo_cup & " AND codigo_ins = " & codigo_ins & " AND estado_nop <> 'X'", "", Data.DataViewRowState.CurrentRows) ' dtM = CType(Session("gc_dtNotas"), Data.DataTable) 'Comentado por Luis Q.T. | 13DIC2019
                dtM = dvM.ToTable
            End If

            Return (dtN.Rows.Count + dtM.Rows.Count)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_ValidarCorte(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_cor = " & codigo_cor & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return False
            End If
        End If
        Return True
    End Function

    'Private Function fc_validarEdicionTxt(ByVal codigo_dma As Integer, ByVal codigo_ins As Integer) As Boolean
    '    Dim dv As Data.DataView
    '    Dim dt As New Data.DataTable
    '    dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
    '    If dt.Rows.Count > 0 Then
    '        dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma & " AND codigo_ins = " & codigo_ins, "", Data.DataViewRowState.CurrentRows)
    '        dt = dv.ToTable
    '        If dt.Rows.Count > 0 Then
    '            Return True
    '        End If
    '    End If
    '    Return False
    'End Function

    'Private Function fc_validarEdicionBtn(ByVal codigo_eva As Integer) As Boolean
    '    Dim dv As Data.DataView
    '    Dim dt As New Data.DataTable
    '    dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
    '    If dt.Rows.Count > 0 Then
    '        dv = New Data.DataView(dt, "codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
    '        dt = dv.ToTable
    '        If dt.Rows.Count > 0 Then
    '            Return True
    '        End If
    '    End If
    '    Return False
    'End Function

    Private Function fc_getCodEdicion(ByVal codigo_dma As Integer, ByVal codigo_ins As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma & " AND codigo_ins = " & codigo_ins, "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("codigo_ece")
                End If
            End If
            Return -1
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_getCodigoCcd(ByVal codigo_dma As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCondiciones"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("codigo_ccd")
            End If
        End If
        Return -1
    End Function

    Private Function fc_getCodigoCoa(ByVal codigo_cor As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_cor = " & codigo_cor, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("codigo_coa")
            End If
        End If
        Return -1
    End Function

    Private Function fc_getCodigoCon(ByVal codigo_dma As Integer, ByVal tipo As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCondiciones"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                If tipo = 1 Then
                    Return dt.Rows(0).Item("codigo_con_int")
                Else
                    Return dt.Rows(0).Item("codigo_con_ext")
                End If
            End If
        End If
        Return -1
    End Function

    Private Function fc_getCodNivelLogro(ByVal nota As Double) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNivelLogro"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "", "rangoDesde_niv DESC", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            For i As Integer = 0 To dt.Rows.Count - 1
                If CDbl(dt.Rows(i).Item("rangoDesde_niv")) <= nota Then
                    Return CInt(dt.Rows(i).Item("codigo_niv"))
                End If
            Next
        End If
        Return -1
    End Function

    Private Function fc_ValidarCondicion() As Boolean
        Dim lbl As Label
        Dim _nombre_alu As String
        Dim calif As Integer = 0
        Dim flag As Boolean = True

        For x As Integer = 1 To Me.gvNotas.Rows.Count - 1
            If Me.gvNotas.Rows(x).FindControl("lblCalificacion") IsNot Nothing Then
                calif += 1
                lbl = CType(Me.gvNotas.Rows(x).FindControl("lblCalificacion"), Label)
                _nombre_alu = Me.gvNotas.DataKeys(x).Values("nombre_alu")
                If CDbl(lbl.Text.Trim) < CDbl("13.5") Then
                    Dim cboInt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboInterno"), DropDownList)
                    Dim cboExt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboExterno"), DropDownList)

                    If cboInt.SelectedValue <> -1 Or cboExt.SelectedValue <> -1 Then
                        flag = False
                    End If

                    If flag And cboInt.SelectedValue = -1 Then
                        mt_ShowMessage("¡ Seleccione Condición Interna o Externa al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                        cboInt.Focus()
                        Me.updNotas.Update()
                        Return False
                    End If
                    If flag And cboExt.SelectedValue = -1 Then
                        mt_ShowMessage("¡ Seleccione Condición Interna o Externa al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                        cboExt.Focus()
                        Me.updNotas.Update()
                        Return False
                    End If

                    'If cboInt.SelectedItem.Text = "OTROS" Then
                    '    Dim btnObsInt As LinkButton = CType(Me.gvNotas.Rows(x).FindControl("btnObsInterno"), LinkButton)
                    '    If String.IsNullOrEmpty(btnObsInt.Attributes("obsInt")) Then
                    '        mt_ShowMessage("¡ Ingrese una descripción para la Condición Interna OTROS del Estudiante: " & _nombre_alu & "!", MessageType.Warning)
                    '        cboInt.Focus()
                    '        Me.updNotas.Update()
                    '        Return False
                    '    End If
                    'End If
                    'If cboExt.SelectedItem.Text = "OTROS" Then
                    '    Dim btnObsExt As LinkButton = CType(Me.gvNotas.Rows(x).FindControl("btnObsExterno"), LinkButton)
                    '    If String.IsNullOrEmpty(btnObsExt.Attributes("obsExt")) Then
                    '        mt_ShowMessage("¡ Ingrese una descripción para la Condición Externa OTROS del Estudiante: " & _nombre_alu & "!", MessageType.Warning)
                    '        cboExt.Focus()
                    '        Me.updNotas.Update()
                    '        Return False
                    '    End If
                    'End If

                    If Not cboInt.Visible Or cboInt.SelectedItem.Text.Trim.Equals("OTROS") Then
                        Dim txtObsInt As TextBox = CType(Me.gvNotas.Rows(x).FindControl("txtObsInterno"), TextBox)

                        If String.IsNullOrEmpty(txtObsInt.Text) Then
                            Call mt_ShowMessage("¡ Ingrese una descripción para la Condición Interna OTROS del Estudiante: " & _nombre_alu & "!", MessageType.Warning)
                            txtObsInt.Focus()
                            Me.updNotas.Update()
                            Return False
                        End If
                    End If
                    If Not cboExt.Visible Or cboExt.SelectedItem.Text.Trim.Equals("OTROS") Then
                        Dim txtObsExt As TextBox = CType(Me.gvNotas.Rows(x).FindControl("txtObsExterno"), TextBox)

                        If String.IsNullOrEmpty(txtObsExt.Text) Then
                            Call mt_ShowMessage("¡ Ingrese una descripción para la Condición Externa OTROS del Estudiante: " & _nombre_alu & "!", MessageType.Warning)
                            txtObsExt.Focus()
                            Me.updNotas.Update()
                            Return False
                        End If
                    End If
                End If

                flag = True
            End If
        Next

        If calif = 0 Then
            mt_ShowMessage("No se ha encontrado ninguna calificación para guardar", MessageType.Warning)
            Return False
        End If

        Return True
    End Function

    Private Function fc_getColorNivelLogro(ByVal nota As Double) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNivelLogro"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "", "rangoDesde_niv DESC", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            For i As Integer = 0 To dt.Rows.Count - 1
                If CDbl(dt.Rows(i).Item("rangoDesde_niv")) <= nota Then
                    Return dt.Rows(i).Item("color_niv").ToString
                End If
            Next
        End If
        Return ""
    End Function

    ' 20190918-ENEVADO
    Private Function fc_getNotaMoodle(ByVal codigo_pso As Integer, ByVal codigo_ins As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNotasMoodle"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_ins = " & codigo_ins & " AND username = " & codigo_pso, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("nota_mdl").ToString
            End If
        End If
        Return String.Empty
    End Function

    ' 20190927-ENEVADO
    Private Function fc_ValidarNotasMoodle(ByVal codigo_ins As Integer) As Boolean
        Dim txt As Label
        Dim _nombre_alu As String
        Dim _codigo_emd As Integer
        Dim _cont_sin_nota As Integer
        Dim dt As Data.DataTable
        dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            If codigo_ins = CInt(dt.Rows(i).Item("codigo_ins")) Then
                _cont_sin_nota = 0
                _codigo_emd = CInt(dt.Rows(i).Item("codigo_emd"))
                For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                    If _codigo_emd <> -1 Then
                        txt = Me.gvNotas.Rows(j).FindControl("lblNota" & (i + 1))
                        _nombre_alu = Me.gvNotas.DataKeys(j).Values("nombre_alu")
                        If String.IsNullOrEmpty(txt.Text.Trim) Then
                            _cont_sin_nota += 1
                        End If
                    Else
                        mt_ShowMessage("¡ No se ha alineado el Instrumento de Evaluación:  " & dt.Rows(i).Item("descripcion_eva").ToString & "!", MessageType.Warning)
                        Return False
                    End If
                Next
                If _cont_sin_nota = (Me.gvNotas.Rows.Count - 1) Then
                    mt_ShowMessage("¡ No se ha registrado notas en la Evaluación:  " & dt.Rows(i).Item("descripcion_eva").ToString & "!", MessageType.Warning)
                    Return False
                End If
                Exit For
            End If
        Next
        Return True
    End Function

    ' 20190927-ENEVADO
    Private Function fc_CorteEnviado(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer, ByVal user As Integer, ByVal ctf As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            If ctf = 1 Or ctf = 232 Then user = -1
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "CE", user, -1, -1)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(CType(Session("gc_dtCorteEnviados"), Data.DataTable), "codigo_cor = " & codigo_cor & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                If dt.Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ' 20190926-JQuepuy
    Private Function fc_getResumenNotas(ByVal isMoodle As Boolean, ByVal codigo_ins As Integer, ByVal desIns As String, ByVal desInd As String, ByVal desUni As String) As String
        Dim sup As String = "0"
        Dim inf As String = "0"
        Dim color As String = ""
        Dim msje As String = ""
        Dim msjeEnlace As String = ""
        Dim msjeNoEnlace As String = ""

        Dim dvM As Data.DataView
        Dim dvN As Data.DataView
        Dim dvA As Data.DataView
        Dim dtM As New Data.DataTable
        Dim dtN As New Data.DataTable
        Dim dtA As New Data.DataTable
        Dim _codigo_pso As String = "0"
        Dim _codigo_dma As String = "0"

        dtN = CType(Session("gc_dtNotas"), Data.DataTable)
        dtM = CType(Session("gc_dtNotasMoodle"), Data.DataTable)
        dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)

        dvA = New Data.DataView(dtA, "inhabilitado_dma = 0", "", Data.DataViewRowState.CurrentRows)
        inf = dvA.ToTable.Rows.Count - 1
        'inf = CStr(dtA.Rows.Count - 1)

        desIns = desIns.Replace(vbCr, " ").Replace(vbLf, "")
        desInd = desInd.Replace(vbCr, " ").Replace(vbLf, "")
        desUni = desUni.Replace(vbCr, " ").Replace(vbLf, "")

        '--> Contar los alumnos que cuentan con calificación
        If (dtN.Rows.Count + dtM.Rows.Count) > 0 Then
            For i As Integer = 1 To dtA.Rows.Count - 1
                If dtA.Rows(i).Item("inhabilitado_dma") = 0 Then
                    _codigo_pso += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_pso").ToString()
                    _codigo_dma += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_dma").ToString()
                End If
            Next

            dvN = New Data.DataView(dtN, "codigo_ins = " & codigo_ins & " AND codigo_dma NOT IN ( " & _codigo_dma & ")", "", Data.DataViewRowState.CurrentRows)
            dtN = dvN.ToTable

            dvM = New Data.DataView(dtM, "codigo_ins = " & codigo_ins & " AND username IN ( " & _codigo_pso & " ) AND NOT nota_mdl IS NULL", "", Data.DataViewRowState.CurrentRows)
            dtM = dvM.ToTable

            If (dtN.Rows.Count + dtM.Rows.Count) > 0 Then
                sup = CStr(dtN.Rows.Count + dtM.Rows.Count)
            End If
        End If
        dtN.Dispose()
        dtM.Dispose()

        '--> Definir el color del mensaje
        If sup.Equals("0") Or inf.Equals("0") Then
            color = "red"
        ElseIf sup.Equals(inf) Then
            color = "green"
        Else
            color = "darkorange"
        End If

        '--> Almacenar mensajes
        msjeEnlace = IIf(Session("gc_msjeEnlace") Is Nothing, "", Session("gc_msjeEnlace"))
        msjeNoEnlace = IIf(Session("gc_msjeNoEnlace") Is Nothing, "", Session("gc_msjeNoEnlace"))

        If Not sup.Equals(inf) Then
            msje = "el instrumento: " & desIns & ", del indicador: " & desInd & " de la unidad " & desUni

            If isMoodle Then
                msjeEnlace = IIf(String.IsNullOrEmpty(msjeEnlace), "", msjeEnlace & ";<br/>") & msje
            Else
                msjeNoEnlace = IIf(String.IsNullOrEmpty(msjeNoEnlace), "", msjeNoEnlace & ";<br/>") & msje
            End If
        End If

        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(msjeEnlace) Or Not String.IsNullOrEmpty(msjeNoEnlace) Then
                Session("isValidated") = 2
            End If

            Session("gc_msjeEnlace") = msjeEnlace
            Session("gc_msjeNoEnlace") = msjeNoEnlace
        End If

        Return String.Format("<b style='color: {0}'>Calificados: {1}/{2}</b>", color, sup, inf)
    End Function

    ' 20191001-ENevado
    Private Function fc_getNombreCond(ByVal codigo_dma As Integer, ByVal tipo As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCondiciones"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                If tipo = 1 Then
                    Return dt.Rows(0).Item("observacion_int").ToString
                Else
                    Return dt.Rows(0).Item("observacion_ext").ToString
                End If
            End If
        End If
        Return ""
    End Function

    Private Function fc_getObsEdicion(ByVal codigo_dma As Integer, ByVal codigo_ins As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(dt, "codigo_Dma = " & codigo_dma & " AND codigo_ins = " & codigo_ins, "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("observacion_ece")
                End If
            End If
            Return String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_ValidarCorteGrupo(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer, ByVal codigo_sgr As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dv As Data.DataView
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "CG", codigo_sgr, codigo_cor, codigo_cup)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                'dv = New Data.DataView(dt, "codigo_cor = " & codigo_cor & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
                'dt = dv.ToTable
                'If dt.Rows.Count > 0 Then
                Return False
                'End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        'dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)
    End Function

#End Region

End Class
