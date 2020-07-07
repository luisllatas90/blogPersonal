﻿
Partial Class GestionCurricular_frmGenerarPromedio_Exportar
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
                Session("isValidated") = 2

                Call mt_CargarNotas(Session("gc_codigo_cup"))

                Call mt_CrearColumns(Session("gc_codigo_cup"), Session("gc_fecha_ini"), Session("gc_fecha_fin"))
                Call mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))


            Else

                Call mt_RefreshGrid()
            End If

            mt_ExportToExcel()

        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt, dtA As New Data.DataTable
        Dim _codigo_dma, _codigo_eva, _codigo_ins, _codigo_emd, _tipo_prom, _cant_ins, _codigo_res, _cont_res, _cont_eva As Integer
        Dim _codigo_ind, _tipo_prom_ind, _cant_ind, _cant_rs, _cant_ins_aux, _codigo_cup As Integer '20191115-ENEVADO
        Dim _descripcion_eva, _nota As String
        Dim _sumatoria, _peso_res, _promedio_final As Double
        Dim _calificacion, _sum_ind, _peso_ins, _peso_ind As Double '20191115-ENEVADO
        Dim _codigo_pso, _cant_alu, _codigo_res_aux, _cont_res_aux As Integer
        Dim _inhabilitado As Integer
        Dim _cant_notas_cambios As Integer = 0 '20191212-ENEVADO

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)
                _cant_alu = (dtA.Rows.Count - 1)

                _codigo_dma = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_Dma"))
                _codigo_pso = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_pso"))

                _inhabilitado = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("inhabilitado_dma"))

                _sumatoria = 0 : _peso_res = 0 : _promedio_final = 0
                _codigo_res = -1 : _cont_eva = 0 : _cont_res = 0 : _codigo_res_aux = 0 : _cont_res_aux = -1

                _codigo_cup = Session("gc_codigo_cup") '20190919-ENEVADO

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
                        _codigo_ins = CInt(dt.Rows(i).Item("codigo_ins"))
                        _descripcion_eva = dt.Rows(i).Item("descripcion_eva")
                        _codigo_emd = dt.Rows(i).Item("codigo_emd")

                        'Por JQuepuy | 26NOV2019 | Solo si quiero visualizar notas, entonces también obtener datos de moodle
                        If Session("gc_visualizar") IsNot Nothing AndAlso Session("gc_visualizar") = "0" Then
                            _codigo_emd = -1
                        End If

                        '_tipo_prom = dt.Rows(i).Item("promedio_indicador")
                        '_cant_ins = dt.Rows(i).Item("cant_ins")

                        If _codigo_dma <> -1 Then

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

                                '_cant_ins += CInt(dt.Rows(i).Item("cant_ins"))

                                If i > 0 Then
                                    Dim _prom_und As Double = 0
                                    Dim lblProm As New Label()
                                    lblProm.ID = "lblProm" & (_cont_res + 1)

                                    If _tipo_prom = 2 Then
                                        'lblProm.Text = Math.Round(_sumatoria, 2, MidpointRounding.AwayFromZero).ToString("00.00")
                                        _prom_und = _sumatoria
                                    Else
                                        'lblProm.Text = Math.Round((_sumatoria / _cant_ind), 2, MidpointRounding.AwayFromZero).ToString("00.00")
                                        _prom_und = (_sumatoria / _cant_ind)
                                    End If
                                    lblProm.Text = Math.Round(_prom_und, 2, MidpointRounding.AwayFromZero).ToString("00.00")

                                    _peso_res = Math.Round(CDbl(dt.Rows(i - 1).Item("peso_res")), 2, MidpointRounding.AwayFromZero)
                                    '_promedio_final += (CDbl(lblProm.Text.Trim) * _peso_res)
                                    _promedio_final += (_prom_und * _peso_res)

                                    'lblProm.Text = "RA > 1"

                                    If lblProm.Text.Trim <> "" Then lblProm.ForeColor = IIf(CDbl(lblProm.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)

                                    e.Row.Cells(nro_col + i + _cont_res).Controls.Add(lblProm)
                                    e.Row.Cells(nro_col + i + _cont_res).BackColor = Drawing.Color.GreenYellow

                                    _cont_res += 1
                                End If

                                _tipo_prom = CInt(dt.Rows(i).Item("promedio_indicador"))

                                _sumatoria = 0
                                _cant_ind = 0
                                _cont_eva = 0
                                _cant_rs += 1
                            End If

                            Dim lbl As New Label()
                            lbl.ID = "lblNota" & (i + 1)


                            If _codigo_emd = -1 Then
                                Dim aux2 As String = "0"
                                aux2 = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup)
                                lbl.Text = CDbl(aux2).ToString("00.00")
                            Else
                                Dim aux As String = "0"
                                aux = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup)

                                If String.IsNullOrEmpty(aux) Or aux.Equals("0") Then
                                    aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                End If

                                If String.IsNullOrEmpty(aux) Or aux.Equals("0") Then
                                    'If _codigo_dma = 1868560 Then
                                    '    If _codigo_ins = 2990 Then
                                    '        aux = "14.00"
                                    '    ElseIf _codigo_ins = 2991 Then
                                    '        aux = "12.00"
                                    '    ElseIf _codigo_ins = 2993 Then
                                    '        aux = "16.00"
                                    '    ElseIf _codigo_ins = 2994 Then
                                    '        aux = "14.00"
                                    '    ElseIf _codigo_ins = 2996 Then
                                    '        aux = "17.00"
                                    '    ElseIf _codigo_ins = 2995 Then
                                    '        aux = "15.00"
                                    '    End If
                                    'End If
                                    lbl.Text = aux
                                Else
                                    lbl.Text = CDbl(aux).ToString("00.00")
                                End If
                            End If

                            If lbl.Text.Trim <> "" Then lbl.ForeColor = IIf(CDbl(lbl.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                            If lbl.Text.Length = 1 AndAlso lbl.Text = "0" Then lbl.ForeColor = Drawing.Color.Green : lbl.Text = "--"
                            If lbl.Text = "--" Then lbl.ToolTip = "Alumno no ha sido calificado en Moodle"
                            e.Row.Cells(nro_col + i + _cont_res).Controls.Add(lbl)

                            ' 20191212-ENEVADO ----------------------------------------------------------------------\
                            Dim _nota_aux As String
                            If lbl.Text <> "--" Then
                                _nota_aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                If _nota_aux.Trim <> "" Then
                                    If lbl.Text <> CDbl(_nota_aux).ToString("00.00") Then
                                        e.Row.Cells(nro_col + i + _cont_res).BackColor = Drawing.Color.LightGray
                                        e.Row.Cells(nro_col + i + _cont_res).ToolTip = "Aviso: La nota de esta evaluación ha sido modificado por: " & CDbl(_nota_aux).ToString("00.00")
                                        _cant_notas_cambios += 1
                                    End If
                                End If
                            End If
                            ' ---------------------------------------------------------------------------------------/

                            If _codigo_emd = -1 Then
                                If _tipo_prom_ind = 2 Then
                                    _sum_ind += (fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) * _peso_ins)
                                Else
                                    _sum_ind += fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup) '20191009-ENEVADO
                                End If
                            Else
                                Dim aux As String = "0"
                                aux = fc_getNota(_codigo_dma, _codigo_ins, _codigo_cup)

                                If String.IsNullOrEmpty(aux) Or aux.Equals("0") Then
                                    aux = fc_getNotaMoodle(_codigo_pso, _codigo_ins)
                                End If

                                If String.IsNullOrEmpty(aux) Or aux.Equals("0") Then
                                    aux = "0"
                                End If

                                If _tipo_prom_ind = 2 Then
                                    _sum_ind += (aux * _peso_ins)
                                Else
                                    _sum_ind += aux
                                End If
                            End If

                            _cont_eva += 1

                        Else
                            If Session("gc_visualizar") IsNot Nothing AndAlso Session("gc_visualizar") = "1" Then
                                If _codigo_res_aux <> CInt(dt.Rows(i).Item("codigo_res")) Then
                                    _codigo_res_aux = CInt(dt.Rows(i).Item("codigo_res"))
                                    _cont_res_aux += 1
                                End If

                                Dim lblSinEnlazar As New Label()
                                lblSinEnlazar.ID = "lblMoodle" & (i + 1)

                                If _codigo_emd = -1 Then
                                    If _cant_alu <= fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "0") Then
                                        lblSinEnlazar.ToolTip = "Éstas calificaciones ya fueron enviadas"
                                        lblSinEnlazar.Text = "- <span style='color: black'><i class='fa fa-lock'></i></span> -"
                                    Else
                                        lblSinEnlazar.Text = "<b style='color: red'>No enlazado con aula virtual</b><br/>" & fc_getResumenNotas(False, _codigo_ins)
                                    End If

                                    e.Row.Cells(nro_col + i + _cont_res_aux).Controls.Add(lblSinEnlazar)
                                Else
                                    If _cant_alu <= fc_tieneNota(Session("gc_codigo_cup"), _codigo_ins, "0") Then
                                        lblSinEnlazar.ToolTip = "Éstas calificaciones ya fueron enviadas"
                                        lblSinEnlazar.Text = "- <span style='color: black'><i class='fa fa-lock'></i></span> -"
                                    Else
                                        lblSinEnlazar.Text = "<b style='color: green'>Enlazado con aula virtual</b><br/>" & fc_getResumenNotas(True, _codigo_ins)
                                    End If

                                    e.Row.Cells(nro_col + i + _cont_res_aux).Controls.Add(lblSinEnlazar)
                                End If
                            End If
                        End If
                    Next

                    If _codigo_dma <> -1 Then
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

                        Dim _prom_und2 As Double = 0
                        Dim lblProm2 As New Label()
                        lblProm2.ID = "lblProm" & (_cont_res + 1)

                        If _tipo_prom = 2 Then
                            'lblProm2.Text = Math.Round(_sumatoria, 2, MidpointRounding.AwayFromZero).ToString("00.00")
                            _prom_und2 = _sumatoria
                        Else
                            'lblProm2.Text = Math.Round((_sumatoria / _cant_ind), 2, MidpointRounding.AwayFromZero).ToString("00.00")
                            _prom_und2 = (_sumatoria / _cant_ind)
                        End If
                        lblProm2.Text = Math.Round(_prom_und2, 2, MidpointRounding.AwayFromZero).ToString("00.00")

                        _peso_res = Math.Round(CDbl(dt.Rows(dt.Rows.Count - 1).Item("peso_res")), 2, MidpointRounding.AwayFromZero)
                        '_promedio_final += (CDbl(lblProm2.Text.Trim) * _peso_res)
                        _promedio_final += (_prom_und2 * _peso_res)

                        If lblProm2.Text.Trim <> "" Then lblProm2.ForeColor = IIf(CDbl(lblProm2.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res).Controls.Add(lblProm2)
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res).BackColor = Drawing.Color.GreenYellow

                        Dim lblPromFinal As New Label()
                        lblPromFinal.ID = "lblPromFinal"
                        lblPromFinal.Text = _promedio_final.ToString("00.00")
                        lblPromFinal.ToolTip = "P.F. = " & _promedio_final.ToString
                        lblPromFinal.Font.Bold = True
                        If lblPromFinal.Text.Trim <> "" Then lblPromFinal.ForeColor = IIf(CDbl(lblPromFinal.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).Controls.Add(lblPromFinal)
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).BackColor = Drawing.Color.LightSteelBlue
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).ID = "colProm"

                        Dim lblPromUP As New Label()
                        lblPromUP.ID = "lblPromUP"
                        lblPromUP.Font.Bold = True

                        If _inhabilitado = 1 Then
                            lblPromUP.Text = "INHABILITADO"
                        Else
                            lblPromUP.Text = Math.Round(_promedio_final, 0, MidpointRounding.AwayFromZero).ToString("00")
                            If lblPromUP.Text.Trim <> "" Then lblPromUP.ForeColor = IIf(CDbl(lblPromUP.Text.Trim) < CDbl("13.5"), Drawing.Color.Red, Drawing.Color.Blue)
                        End If

                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 2).Controls.Add(lblPromUP)
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 2).BackColor = Drawing.Color.LightYellow
                        e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 2).ID = "colFinal"

                    End If
                End If

            End If

            If e.Row.RowType = DataControlRowType.Header Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                _cont_eva = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    If e.Row.Cells(nro_col + i + _cont_eva).Text = "Promedio Parcial" Then
                        _cont_eva += 1
                    End If

                    e.Row.Cells(nro_col + i + _cont_eva).ToolTip = "Fecha: " & dt.Rows(i).Item("fecha_gru") & Environment.NewLine & _
                    "Evidencia: " & dt.Rows(i).Item("descripcion_evi") & Environment.NewLine & _
                    "Instrumento: " & dt.Rows(i).Item("descripcion_ins")
                Next
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As New Data.DataTable
        Dim _codigo_uni, _nro_eva, _codigo_res, _nro_res, _codigo_ind, _nro_ind As Integer
        Dim _cant_ra, _cant_ind, _cant_res As Integer
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
                    mt_AgregarCabecera(objgridviewrow, objtablecell, 2, 1, "UNIDADES")
                    For i As Integer = 0 To dt.Rows.Count - 1
                        If _codigo_uni <> CInt(dt.Rows(i).Item("codigo_uni")) Then
                            If i <> 0 Then
                                mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva + _cant_res, 1, dt.Rows(i - 1).Item("numero_uni") & " : " & dt.Rows(i - 1).Item("descripcion_uni"))
                            End If
                            _codigo_uni = CInt(dt.Rows(i).Item("codigo_uni"))
                            _nro_eva = 0
                            _cant_res = 0
                        End If
                        _nro_eva += 1
                        If _codigo_res <> CInt(dt.Rows(i).Item("codigo_res")) Then
                            If i <> 0 Then
                                _cant_ra += 1
                                mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res + 1, 1, "RA " & _cant_ra & ": " & dt.Rows(i - 1).Item("descripcion_res"))
                            Else
                                mt_AgregarCabecera(objgridviewrow2, objtablecell, 2, 1, "RESULTADOS DE APRENDIZAJES (RA)")
                            End If
                            _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                            _nro_res = 0
                            _cant_res += 1
                        End If
                        If _codigo_ind <> CInt(dt.Rows(i).Item("codigo_ind")) Then
                            If i <> 0 Then
                                _cant_ind += 1
                                mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "IND " & _cant_ind & ": " & _nombre_ind, _tooltip_ind)
                            Else
                                mt_AgregarCabecera(objgridviewrow3, objtablecell, 2, 1, "DATOS DEL ESTUDIANTE")
                            End If
                            _codigo_ind = CInt(dt.Rows(i).Item("codigo_ind"))
                            _nombre_ind = dt.Rows(i).Item("descripcion_ind")
                            _peso_ind = CDbl(dt.Rows(i).Item("peso_ind").ToString) * 100
                            _tipo_prom = dt.Rows(i).Item("promedio_indicador").ToString

                            If _nombre_ind.Trim.Length > 30 Then
                                _tooltip_ind = _nombre_ind
                                _nombre_ind = _nombre_ind.Substring(0, 30) & "...<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ind).ToString("00.00") & "%") & "</span>"
                            Else
                                _tooltip_ind = String.Empty
                                _nombre_ind = _nombre_ind & "<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ind).ToString("00.00") & "%") & "</span>"
                            End If
                            _nro_ind = 0
                        End If
                        If i <> 0 AndAlso _nro_res = 0 Then
                            mt_AgregarCabecera(objgridviewrow3, objtablecell, 1, 1, "Peso RA<br/>" & dt.Rows(i - 1).Item("peso_res") * 100 & " %")
                        End If
                        _nro_res += 1
                        _nro_ind += 1
                    Next

                    mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva + _cant_res, 1, dt.Rows(dt.Rows.Count - 1).Item("numero_uni") & " : " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_uni"))
                    mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 3, " ")
                    _cant_ra += 1
                    mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res + 1, 1, "RA " & _cant_ra & ": " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_res"))
                    _cant_ind += 1
                    mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "IND " & _cant_ind & ": " & _nombre_ind, _tooltip_ind)
                    mt_AgregarCabecera(objgridviewrow3, objtablecell, 1, 1, "Peso RA<br/>" & dt.Rows(dt.Rows.Count - 1).Item("peso_res") * 100 & " %")
                    objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
                    objGridView.Controls(0).Controls.AddAt(1, objgridviewrow2)
                    objGridView.Controls(0).Controls.AddAt(2, objgridviewrow3)
                Else
                    mt_ShowMessage("No se encontró ninguna evaluación para esta asignatura", MessageType.Warning)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

        Session("gc_codigo_cac") = Session("gc_codigo_cac")
        Session("gc_codigo_cup") = Session("gc_codigo_cup")
        Session("gc_codigo_mat") = gvNotas.DataKeys(row.RowIndex).Item("codigo_mat").ToString
        Session("gc_alumno_nom") = gvNotas.DataKeys(row.RowIndex).Item("nombre_alu").ToString

        Response.Redirect("~/GestionCurricular/FrmCalificadorEstudiante.aspx")
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

#End Region

#Region "Confirmar SMS"

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
        '        dt = obj.TraerDataTable("DEA_ConfirmarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA FINAL", txtToken.Text)
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

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
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

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "GP", codigo_cac, codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtAlumnos") = dt
            Me.gvNotas.DataSource = dt
            Me.gvNotas.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarNotas(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt, dtNM As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_NotasParciales_listar", "", -1, -1, "P", codigo_cup, 99) 'Por Luis QT|04DIC2019: Uso 99 para obtener notas del último corte
            dtNM = obj.TraerDataTable("DEA_NotasParciales_listar", "CE", -1, -1, "P", codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtNotas") = dt
            Session("gc_dtNotasMoodle") = dtNM
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearColumns(ByVal codigo_cup As Integer, ByVal fecha_ini As Date, ByVal fecha_fin As Date)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim _codigo_res As Integer = -1
        Dim _peso_ins, _tipo_prom As String

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarEvaluacionesCortes", "CE", codigo_cup, fecha_ini, fecha_fin)
            obj.CerrarConexion()
            Session("gc_dtEvaluacion") = dt

            If dt.Rows.Count > 0 Then
                _codigo_res = dt.Rows(0).Item("codigo_res")
                Dim tfield As New TemplateField()
                For x As Integer = 0 To dt.Rows.Count - 1
                    If _codigo_res <> CInt(dt.Rows(x).Item("codigo_res")) Then
                        _codigo_res = CInt(dt.Rows(x).Item("codigo_res"))
                        tfield = New TemplateField()
                        tfield.HeaderText = "Promedio Parcial"
                        tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                        Me.gvNotas.Columns.Add(tfield)
                    End If

                    _peso_ins = CDbl(dt.Rows(x).Item("peso_ins").ToString) * 100
                    _tipo_prom = dt.Rows(x).Item("promedio_instrumento")

                    tfield = New TemplateField()
                    tfield.HeaderText = dt.Rows(x).Item("descripcion_eva") & "<br/><span style='color: #ffe8a5;'>" & IIf(_tipo_prom.Equals("1"), "Prom. simple", "Peso " & CDbl(_peso_ins).ToString("00.00") & "%") & "</span>"
                    tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    Me.gvNotas.Columns.Add(tfield)
                Next

                tfield = New TemplateField()
                tfield.HeaderText = "Promedio Parcial"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.gvNotas.Columns.Add(tfield)

                tfield = New TemplateField()
                tfield.HeaderText = "Promedio Asignatura"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.gvNotas.Columns.Add(tfield)

                tfield = New TemplateField()
                tfield.HeaderText = "Calificación final"
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
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
        objtablecell.VerticalAlign = VerticalAlign.Middle
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub mt_ExportToExcel()
        Dim attachment As String = "attachment; filename=notas.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As io.StringWriter = New io.StringWriter()
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Me.gvnotas.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

#End Region

#Region "Funciones"

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

                'If isMoodle.Equals("2") Then
                '    dvN = New Data.DataView(dtN, "codigo_cup = " & codigo_cup & " AND codigo_ins = " & codigo_ins & " AND estado_nop = 'E'" & " AND codigo_dma NOT IN ( " & _codigo_dma & ")", "", Data.DataViewRowState.CurrentRows)
                'Else
                dvN = New Data.DataView(dtN, "codigo_cup = " & codigo_cup & " AND codigo_ins = " & codigo_ins & " AND estado_nop = 'E'", "", Data.DataViewRowState.CurrentRows)
                'End If

                dtN = dvN.ToTable
            End If

            If isMoodle.Equals("1") Or isMoodle.Equals("2") Then
                For i As Integer = 1 To dtA.Rows.Count - 1
                    _codigo_pso += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_pso").ToString()
                Next

                dtM = CType(Session("gc_dtNotasMoodle"), Data.DataTable)

                dvM = New Data.DataView(dtM, "codigo_ins = " & codigo_ins & " AND username IN ( " & _codigo_pso & " ) AND NOT nota_mdl IS NULL", "", Data.DataViewRowState.CurrentRows)
                dtM = dvM.ToTable
            End If

            'If (dtA.Rows.Count - 1) = (dtN.Rows.Count + dtM.Rows.Count) Then
            '    Return True
            'End If

            Return (dtN.Rows.Count + dtM.Rows.Count)
        Catch ex As Exception
            Throw ex
        End Try
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
                'Else
                '    If codigo_dma = 1868560 Then
                '        If codigo_ins = 2990 Then
                '            Return "14.00"
                '        ElseIf codigo_ins = 2991 Then
                '            Return "12.00"
                '        ElseIf codigo_ins = 2993 Then
                '            Return "16.00"
                '        ElseIf codigo_ins = 2994 Then
                '            Return "14.00"
                '        ElseIf codigo_ins = 2996 Then
                '            Return "17.00"
                '        ElseIf codigo_ins = 2995 Then
                '            Return "15.00"
                '        End If
                '    End If
            End If
        End If

        If Session("gc_visualizar") IsNot Nothing AndAlso Session("gc_visualizar") = "1" Then
            Return "0"
        End If

        Return String.Empty
    End Function

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

        If Session("gc_visualizar") IsNot Nothing AndAlso Session("gc_visualizar") = "1" Then
            Return "0"
        End If

        Return String.Empty
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

    Private Function fc_getResumenNotas(ByVal isMoodle As Boolean, ByVal codigo_ins As Integer) As String
        Dim sup As String = "0"
        Dim inf As String = "0"
        Dim color As String = ""
        Dim msje As String = ""

        Dim dvM As Data.DataView
        Dim dvN As Data.DataView
        Dim dtM As New Data.DataTable
        Dim dtN As New Data.DataTable
        Dim dtA As New Data.DataTable
        Dim _codigo_pso As String = ""
        Dim _codigo_dma As String = ""

        dtN = CType(Session("gc_dtNotas"), Data.DataTable)
        dtM = CType(Session("gc_dtNotasMoodle"), Data.DataTable)
        dtA = CType(Session("gc_dtAlumnos"), Data.DataTable)
        inf = CStr(dtA.Rows.Count - 1)

        '--> Contar los alumnos que cuentan con calificación
        If (dtN.Rows.Count + dtM.Rows.Count) > 0 Then
            For i As Integer = 1 To dtA.Rows.Count - 1
                _codigo_pso += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_pso").ToString()
                _codigo_dma += IIf(i = 1, "", ",") & dtA.Rows(i).Item("codigo_dma").ToString()
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

        Return String.Format("<b style='color: {0}'>Calificados: {1}/{2}</b>", color, sup, inf)
    End Function

    Private Function fc_PublicarNotas(ByVal codigo_cac As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "VF", codigo_cac, -1)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) = 1 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_VerificarPublicacion(ByVal codigo_cac As Integer, ByVal codigo_cup As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "VP", codigo_cac, codigo_cup)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 1 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_VerificarDiferenciaNotas(ByVal codigo_cac As Integer, ByVal codigo_cup As Integer) As Boolean
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "VN", codigo_cac, codigo_cup)
            obj.CerrarConexion()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item(0) > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
