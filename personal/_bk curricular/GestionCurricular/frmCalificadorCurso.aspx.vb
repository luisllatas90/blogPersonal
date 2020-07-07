
Partial Class GestionCurricular_frmCalificadorCurso
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim nro_col As Integer = 2

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
            cod_user = Session("id_per")
            'cod_user = IIf(cod_user = 684, -1, cod_user) '2238
            If Not IsPostBack Then
                Session("isValidated") = 0

                Me.lblCurso.InnerText = "Asignatura: " & Session("gc_nombre_cur")
                mt_CargarNotas(Session("gc_codigo_cup"))
                mt_CargarEdiciones(Session("gc_codigo_cup"))
                If Session("gc_enviar") Then mt_CargarCondiciones() : mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_CargarCortes(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_CrearColumns(Session("gc_codigo_cup"), Session("gc_fecha_ini"), Session("gc_fecha_fin"))
                mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
                Me.btnEnviar.Visible = Not (Session("gc_enviar"))
                Me.btnGuardar.Visible = Session("gc_enviar")

                mt_ObtenerCelular()

                divEnviar.Style.Item("display") = "none"
                divTexto.Style.Item("display") = "none"
            Else
                divEnviar.Style.Item("display") = "block"
                divTexto.Style.Item("display") = "block"

                mt_RefreshGrid()
            End If
            If Session("gc_enviar") Then mt_MostrarLeyenda()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As New Data.DataTable
        Dim _codigo_dma, _codigo_eva, _codigo_emd, _tipo_prom, _cant_ins As Integer
        Dim _descripcion_eva As String
        Dim _pendiente, _edicion As Boolean
        Dim _calificacion, _sumatoria As Double
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                _codigo_dma = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_Dma"))
                _calificacion = 0 : _sumatoria = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
                    _descripcion_eva = dt.Rows(i).Item("descripcion_eva")
                    _codigo_emd = dt.Rows(i).Item("codigo_emd")
                    _tipo_prom = dt.Rows(i).Item("promedio_indicador")
                    _cant_ins = dt.Rows(i).Item("cant_ins")
                    If _codigo_dma <> -1 Then
                        If Not (Session("gc_enviar")) Then
                            Dim txt As New TextBox()
                            txt.ID = "txtNota" & (i + 1)
                            txt.CssClass = "form-control input-sm"
                            txt.Style.Add("text-align", "right")
                            txt.Width = 100
                            txt.Attributes.Add("onKeyPress", "return soloNumeros(event,this)")
                            txt.Attributes.Add("onKeyUp", "soloCalificacion(this)")
                            txt.Text = fc_getNota(_codigo_dma, _codigo_eva)
                            txt.Enabled = IIf((txt.Text.Trim = "" And _codigo_emd = -1) Or fc_validarEdicionTxt(_codigo_dma, _codigo_eva), True, False)
                            txt.MaxLength = 2
                            If txt.Text.Trim <> "" Then txt.ForeColor = IIf(CDbl(txt.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                            e.Row.Cells(nro_col + i).Controls.Add(txt)
                        Else
                            Dim lbl As New Label()
                            lbl.ID = "lblNota" & (i + 1)
                            lbl.Text = fc_getNota(_codigo_dma, _codigo_eva)
                            lbl.ForeColor = IIf(CDbl(lbl.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                            e.Row.Cells(nro_col + i).Controls.Add(lbl)
                            If _cant_ins = dt.Rows.Count AndAlso _tipo_prom = 1 Then
                                _sumatoria += fc_getNota(_codigo_dma, _codigo_eva)
                            End If
                        End If
                    Else
                        If Not (Session("gc_enviar")) Then
                            _pendiente = fc_ValidarCorte(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                            _edicion = fc_validarEdicionBtn(_codigo_eva)
                            If _codigo_emd = -1 Then
                                Dim btn As New LinkButton()
                                btn.ID = "btnNotaAdd" & (i + 1)
                                btn.Text = "<i class='fa fa-save'></i>"
                                btn.CommandArgument = _codigo_eva
                                btn.CommandName = IIf(_pendiente Or _edicion, "", "_") & "addEvaluacion" & (i + 1)
                                If (_pendiente Or _edicion) Then btn.OnClientClick = "return confirm('¿ Desea guardar las calificaciones de la evaluación: " & _descripcion_eva & "?');"
                                btn.CssClass = "btn btn-primary btn-sm"
                                btn.Enabled = IIf(_pendiente Or _edicion, True, False)
                                e.Row.Cells(nro_col + i).Controls.Add(btn)
                                Dim lbl As New Label()
                                lbl.ID = "lblStep" & (i + 1)
                                lbl.Text = " "
                                e.Row.Cells(nro_col + i).Controls.Add(lbl)
                                btn = New LinkButton()
                                btn.ID = "btnNotaDelete" & (i + 1)
                                btn.Text = "<i class='fa fa-trash'></i>"
                                btn.CommandArgument = _codigo_eva
                                btn.CommandName = IIf(_pendiente, "", "_") & "deleteEvaluacion" & (i + 1)
                                If _pendiente Then btn.OnClientClick = "return confirm('¿ Desea eliminar las calificaciones de la evaluación: " & _descripcion_eva & "?');"
                                btn.CssClass = "btn btn-danger btn-sm"
                                btn.Enabled = _pendiente
                                e.Row.Cells(nro_col + i).Controls.Add(btn)
                            Else
                                Dim lbl As New Label()
                                lbl.ID = "lblMoodle" & (i + 1)
                                lbl.Text = "Enlazar con aula virtual"
                                e.Row.Cells(nro_col + i).Controls.Add(lbl)
                            End If
                        End If
                    End If
                Next
                If _codigo_dma <> -1 AndAlso (Session("gc_enviar")) Then
                    Dim lbl As New Label()
                    lbl.ID = "lblCalificacion"
                    If _cant_ins = dt.Rows.Count AndAlso _tipo_prom = 1 Then
                        _calificacion = Math.Round(_sumatoria / _cant_ins, 2)
                    Else
                        _calificacion = fc_getNota(_codigo_dma, _codigo_eva)
                    End If
                    lbl.Text = _calificacion.ToString("00.00")
                    lbl.ForeColor = IIf(CDbl(lbl.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                    'lbl.Style.Add("background-color", fc_getColorNivelLogro(CDbl(lbl.Text.Trim)))
                    e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lbl)
                    lbl = New Label()
                    lbl.ID = "lblStep2"
                    lbl.Text = " "
                    e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lbl)
                    lbl = New Label()
                    lbl.ID = "lblColor"
                    lbl.ForeColor = Drawing.Color.Transparent
                    lbl.Text = "<br/>_____"
                    lbl.Style.Add("background-color", fc_getColorNivelLogro(_calificacion))
                    e.Row.Cells(nro_col + dt.Rows.Count).Controls.Add(lbl)
                    'e.Row.Cells(nro_col + dt.Rows.Count).Style.Add("background-color", fc_getColorNivelLogro(CDbl(lbl.Text.Trim)))
                    If _calificacion < 14 Then
                        Dim cbo As New DropDownList()
                        cbo.ID = "cboInterno"
                        cbo.CssClass = "form-control input-sm"
                        mt_CargarCombo(cbo, CType(Session("gc_dtCondInt"), Data.DataTable), "codigo_con", "descripcion_con")
                        cbo.SelectedValue = fc_getCodigoCon(_codigo_dma, 1)
                        'cbo.AutoPostBack = True
                        'AddHandler cbo.SelectedIndexChanged, AddressOf cboInterno_SelectedIndexChanged
                        e.Row.Cells(nro_col + dt.Rows.Count + 1).Controls.Add(cbo)
                        cbo = New DropDownList
                        cbo.ID = "cboExterno"
                        cbo.CssClass = "form-control input-sm"
                        mt_CargarCombo(cbo, CType(Session("gc_dtCondExt"), Data.DataTable), "codigo_con", "descripcion_con")
                        cbo.SelectedValue = fc_getCodigoCon(_codigo_dma, 2)
                        'cbo.AutoPostBack = True
                        'AddHandler cbo.SelectedIndexChanged, AddressOf cboExterno_SelectedIndexChanged
                        e.Row.Cells(nro_col + dt.Rows.Count + 2).Controls.Add(cbo)
                    End If
                End If
            End If
            If e.Row.RowType = DataControlRowType.Header Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                For i As Integer = 0 To dt.Rows.Count - 1
                    e.Row.Cells(nro_col + i).ToolTip = "Fecha: " & dt.Rows(i).Item("fecha_gru") & Environment.NewLine & _
                                                "Evidencia: " & dt.Rows(i).Item("descripcion_evi") & Environment.NewLine & _
                                                "Instrumento: " & dt.Rows(i).Item("descripcion_ins")
                    ' e.Row.Cells(2 + i).Width = (60 / dt.Rows.Count)
                Next
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As Data.DataTable
        Dim _codigo_uni, _nro_eva, _codigo_res, _nro_res, _codigo_ind, _nro_ind As Integer
        Dim _nombre_ind, _tooltip_ind As String
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objgridviewrow2 As GridViewRow = New GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objgridviewrow3 As GridViewRow = New GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
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
                            mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res, 1, "RA : " & dt.Rows(i - 1).Item("descripcion_res"))
                        End If
                        _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                        _nro_res = 0
                    End If
                    _nro_res += 1
                    If _codigo_ind <> CInt(dt.Rows(i).Item("codigo_ind")) Then
                        If i <> 0 Then
                            mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "Indicador : " & _nombre_ind, _tooltip_ind)
                        End If
                        _codigo_ind = CInt(dt.Rows(i).Item("codigo_ind"))
                        _nombre_ind = dt.Rows(i).Item("descripcion_ind")
                        If _nombre_ind.Trim.Length > 45 Then
                            _tooltip_ind = _nombre_ind
                            _nombre_ind = _nombre_ind.Substring(0, 45) & " ..."
                        Else
                            _tooltip_ind = String.Empty
                        End If
                        _nro_ind = 0
                    End If
                    _nro_ind += 1
                Next
                mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva, 1, "UNIDAD " & dt.Rows(dt.Rows.Count - 1).Item("numero_uni") & " : " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_uni"))
                mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res, 1, "RA : " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_res"))
                mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "Indicador : " & _nombre_ind, _tooltip_ind)
                If Session("gc_enviar") Then
                    mt_AgregarCabecera(objgridviewrow, objtablecell, 3, 3, "Condiciones")
                End If
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
                objGridView.Controls(0).Controls.AddAt(1, objgridviewrow2)
                objGridView.Controls(0).Controls.AddAt(2, objgridviewrow3)
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

    Protected Sub gvNotas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvNotas.RowCommand
        Dim txt As TextBox
        Dim dt As New Data.DataTable
        Dim _codigo_Dma, _codigo_eva, _codigo_nop, _codigo_ece, _codigo_niv As Integer
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
            For x As Integer = 0 To dt.Rows.Count - 1
                If e.CommandName = "addEvaluacion" & (x + 1) Then
                    If fc_ValidarNotas(e.CommandArgument) Then
                        obj.IniciarTransaccion()
                        _codigo_eva = CInt(e.CommandArgument)
                        For y As Integer = 1 To Me.gvNotas.Rows.Count - 1
                            txt = Me.gvNotas.Rows(y).FindControl("txtNota" & (x + 1))
                            _codigo_Dma = CInt(Me.gvNotas.DataKeys(y).Values("codigo_Dma"))
                            _codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_eva)
                            _codigo_ece = fc_getCodEdicion(_codigo_Dma, _codigo_eva)
                            If _codigo_nop = -1 Then
                                obj.Ejecutar("DEA_NotasParciales_insertar", _codigo_Dma, _codigo_eva, CDbl(txt.Text.Trim), cod_user)
                            Else
                                If txt.Enabled Then
                                    _codigo_niv = fc_getCodNivelLogro(CDbl(txt.Text.Trim))
                                    obj.Ejecutar("DEA_NotasParciales_actualizar", _codigo_nop, CDbl(txt.Text.Trim), _codigo_niv, cod_user)
                                    If _codigo_ece <> -1 Then
                                        obj.Ejecutar("DEA_EvaluacionCurso_Edicion_actualizar", _codigo_ece, cod_user)
                                    End If
                                End If
                            End If
                        Next
                        obj.TerminarTransaccion()
                        mt_CargarNotas(Session("gc_codigo_cup"))
                        mt_CargarEdiciones(Session("gc_codigo_cup"))
                        mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
                        mt_ShowMessage("¡ Se registraron Correctamente las calificaciones !", MessageType.Success)
                        'Exit For
                    End If
                    Exit For
                End If
                If e.CommandName = "deleteEvaluacion" & (x + 1) Then
                    Dim _lb_delete As Boolean
                    obj.IniciarTransaccion()
                    _codigo_eva = CInt(e.CommandArgument)
                    For y As Integer = 1 To Me.gvNotas.Rows.Count - 1
                        txt = Me.gvNotas.Rows(y).FindControl("txtNota" & (x + 1))
                        _codigo_Dma = CInt(Me.gvNotas.DataKeys(y).Values("codigo_Dma"))
                        _codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_eva)
                        If _codigo_nop <> -1 Then
                            obj.Ejecutar("DEA_NotasParciales_eliminar", _codigo_nop, cod_user)
                            _lb_delete = True
                        Else
                            txt.Text = String.Empty
                        End If
                    Next
                    obj.TerminarTransaccion()
                    If _lb_delete Then
                        mt_CargarNotas(Session("gc_codigo_cup"))
                        mt_CargarEdiciones(Session("gc_codigo_cup"))
                        mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
                    Else
                        mt_ShowMessage("¡ No hay calificaciones para eliminar !", MessageType.Warning)
                    End If
                    mt_ShowMessage("¡ Se eliminaron Correctamente las calificaciones !", MessageType.Success)
                    Exit For
                End If
            Next
        Catch ex As Exception
            obj.AbortarTransaccion()
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

    Protected Sub cboInterno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos
        Dim _codigo_dma, _codigo_ccd, _codigo_coa, _codigo_con_int, _codigo_con_ext, _codigo_niv As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)
            Dim lbl As Label = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("lblCalificacion"), Label)
            Dim cboExt As DropDownList = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("cboExterno"), DropDownList)
            _codigo_dma = CInt(Me.gvNotas.DataKeys(row.RowIndex).Values("codigo_Dma"))
            _codigo_ccd = fc_getCodigoCcd(_codigo_dma)
            _codigo_coa = fc_getCodigoCoa(Session("gc_codigo_cor"))
            _codigo_con_int = cbo.SelectedValue
            _codigo_con_ext = cboExt.SelectedValue
            _codigo_niv = fc_getCodNivelLogro(CDbl(lbl.Text.Trim))
            'Throw New Exception("OK: " & _codigo_ccd & " : " & _codigo_coa & " : " & _codigo_dma & " : " & _codigo_con_int & " : " & CDbl(lbl.Text.Trim) & " : " & cod_user)
            obj.AbrirConexion()
            If _codigo_ccd = -1 Then
                obj.Ejecutar("DEA_CortesCurso_Condicion_insertar", _codigo_coa, _codigo_dma, _codigo_con_int, _codigo_con_ext, CDbl(lbl.Text.Trim), "", _codigo_niv, cod_user)
            Else
                obj.Ejecutar("DEA_CortesCurso_Condicion_actualizar", _codigo_ccd, _codigo_con_int, _codigo_con_ext, "", _codigo_niv, cod_user)
            End If
            obj.CerrarConexion()
            mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
            mt_ShowMessage("¡ Se registro correctamente la información !", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboExterno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos
        Dim _codigo_dma, _codigo_ccd, _codigo_coa, _codigo_con_int, _codigo_con_ext, _codigo_niv As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)
            Dim lbl As Label = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("lblCalificacion"), Label)
            Dim cboInt As DropDownList = CType(Me.gvNotas.Rows(row.RowIndex).FindControl("cboInterno"), DropDownList)
            _codigo_dma = CInt(Me.gvNotas.DataKeys(row.RowIndex).Values("codigo_Dma"))
            _codigo_ccd = fc_getCodigoCcd(_codigo_dma)
            _codigo_coa = fc_getCodigoCoa(Session("gc_codigo_cor"))
            _codigo_con_int = cboInt.SelectedValue
            _codigo_con_ext = cbo.SelectedValue
            _codigo_niv = fc_getCodNivelLogro(CDbl(lbl.Text.Trim))
            'Throw New Exception("OK: " & _codigo_ccd & " : " & _codigo_coa & " : " & _codigo_dma & " : " & _codigo_con_int & " : " & CDbl(lbl.Text.Trim) & " : " & cod_user)
            obj.AbrirConexion()
            If _codigo_ccd = -1 Then
                obj.Ejecutar("DEA_CortesCurso_Condicion_insertar", _codigo_coa, _codigo_dma, _codigo_con_int, _codigo_con_ext, CDbl(lbl.Text.Trim), "", _codigo_niv, cod_user)
            Else
                obj.Ejecutar("DEA_CortesCurso_Condicion_actualizar", _codigo_ccd, _codigo_con_int, _codigo_con_ext, "", _codigo_niv, cod_user)
            End If
            obj.CerrarConexion()
            mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
            mt_ShowMessage("¡ Se registro correctamente la información !", MessageType.Success)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim obj As New ClsConectarDatos
        Dim _codigo_dma, _codigo_ccd, _codigo_coa, _codigo_con_int, _codigo_con_ext, _codigo_niv As Integer
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
                    _codigo_con_int = -1
                    _codigo_con_ext = -1
                    If CDbl(lbl.Text.Trim) < 14.0 Then
                        Dim cboInt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboInterno"), DropDownList)
                        Dim cboExt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboExterno"), DropDownList)
                        _codigo_con_int = cboInt.SelectedValue
                        _codigo_con_ext = cboExt.SelectedValue
                        'Throw New Exception("OK: " & _codigo_ccd & " : " & _codigo_coa & " : " & _codigo_dma & " : " & _codigo_con_int & " : " & _codigo_con_ext & " : " & CDbl(lbl.Text.Trim) & " : " & _codigo_niv & " : " & cod_user)
                    End If
                    If _codigo_ccd = -1 Then
                        obj.Ejecutar("DEA_CortesCurso_Condicion_insertar", _codigo_coa, _codigo_dma, _codigo_con_int, _codigo_con_ext, CDbl(lbl.Text.Trim), "", _codigo_niv, cod_user)
                    Else
                        obj.Ejecutar("DEA_CortesCurso_Condicion_actualizar", _codigo_ccd, _codigo_con_int, _codigo_con_ext, "", _codigo_niv, cod_user)
                    End If
                Next
                obj.TerminarTransaccion()
                mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_ShowMessage("¡ Se registro correctamente la información !", MessageType.Success)
            End If
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage("btnGuardar_Click: " & ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If Session("isValidated") = 1 Then
            Dim txt As TextBox
            Dim obj As New ClsConectarDatos
            Dim dt, dtX As New Data.DataTable
            Dim _codigo_Dma, _codigo_eva, _codigo_nop, _codigo_coa, _codigo_niv As Integer
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
            Try
                If fc_ValidarCorte(Session("gc_codigo_cor"), Session("gc_codigo_cup")) Then
                    dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                    For x As Integer = 0 To dt.Rows.Count - 1
                        If Not fc_ValidarNotas(dt.Rows(x).Item("codigo_eva")) Then
                            Exit Sub
                        End If
                    Next
                    obj.IniciarTransaccion()
                    dtX = obj.TraerDataTable("DEA_CortesCurso_insertar", Session("gc_codigo_cor"), Session("gc_codigo_cup"), cod_user)
                    If dtX.Rows.Count > 0 Then
                        _codigo_coa = CInt(dtX.Rows(0).Item(0))
                        '_codigo_coa = 1
                        For i As Integer = 0 To dt.Rows.Count - 1
                            _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
                            For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                                txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
                                _codigo_Dma = CInt(Me.gvNotas.DataKeys(j).Values("codigo_Dma"))
                                _codigo_nop = fc_getCodNota(_codigo_Dma, _codigo_eva)
                                _codigo_niv = fc_getCodNivelLogro(fc_getNota(_codigo_Dma, _codigo_eva))
                                'If _codigo_nop <> -1 Then
                                obj.Ejecutar("DEA_DetalleCortesCurso_insertar", _codigo_coa, _codigo_nop, _codigo_niv, cod_user)
                                'Else
                                'Throw New Exception("¡ No se encontro nota !" & _codigo_coa & " - " & _codigo_nop & " - " & cod_user & " - " & i & " - " & j)
                                'End If
                            Next
                        Next
                        mt_CargarNotas(Session("gc_codigo_cup"))
                        mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
                        mt_ShowMessage("¡ Se enviaron Correctamente las calificaciones !", MessageType.Success)
                    End If
                    obj.TerminarTransaccion()
                Else
                    mt_ShowMessage("¡Ocurrió un problema en el envío!. Las calificaciones ya han sido enviadas", MessageType.Warning)
                End If
            Catch ex As Exception
                'obj.AbortarTransaccion()
                mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
            End Try
        End If
    End Sub

#End Region

#Region "Confirmar SMS"

    Protected Sub btnGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim dt As New Data.DataTable()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
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
            dt = obj.TraerDataTable("DEA_ObtenerTelefonoPersonal", cod_user, "O")
            obj.CerrarConexion()

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

    Private Function fc_Validar() As Generic.Dictionary(Of String, String)
        Dim valid As New Generic.Dictionary(Of String, String)
        Dim err As Boolean = False
        valid.Add("rpta", 1)
        valid.Add("msg", "")
        valid.Add("control", "")

        Try
            If Not err And String.IsNullOrEmpty(Request("txtToken")) Then
                If Not err Then
                    valid.Item("rpta") = 0
                    valid.Item("msg") = "Debe ingresar el código enviado por SMS"
                    valid.Item("control") = "txtToken"
                    err = True
                    ScriptManager.GetCurrent(Me.Page).SetFocus(Me.txtToken)
                End If
                txtToken.Attributes.Item("data-error") = "true"
            Else
                txtToken.Attributes.Item("data-error") = "false"
            End If

            If Not err Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

                obj.AbrirConexion()
                dt = obj.TraerDataTable("DEA_ConfirmarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA " & fc_getDescripcionSemana(Session("gc_codigo_cor")), txtToken.Text)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Dim rpta As String = dt.Rows(0).Item(0).ToString
                    Dim msg As String = dt.Rows(0).Item(1).ToString

                    Session("isValidated") = rpta
                    valid.Item("rpta") = rpta
                    valid.Item("msg") = msg
                Else
                    Session("isValidated") = 0
                    valid.Item("rpta") = 0
                    valid.Item("msg") = "No se pudo confirmar el token, intente generar uno nuevo"
                End If

                valid.Item("control") = "txtToken"
                dt.Dispose()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try

        Return valid
    End Function

    Private Function fc_getDescripcionSemana(ByVal codigo_cor As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Dim des As String
        dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtCorteCurso"), Data.DataTable), "codigo_cor = " & codigo_cor, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                des = dt.Rows(0).Item("descripcion").ToString()
                des = des.Split(" ")(0) & " " & des.Split(" ")(1)
                Return des
            End If
        End If

        Return ""
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

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_DetalleMatricula_Listar", "", codigo_cac, codigo_cup)
            obj.CerrarConexion()
            Me.gvNotas.DataSource = dt
            Me.gvNotas.DataBind()
            'mt_ShowMessage(codigo_cac & " - " & codigo_cup & " - " & dt.Rows.Count, MessageType.Warning)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearColumns(ByVal codigo_cup As Integer, ByVal fecha_ini As Date, ByVal fecha_fin As Date)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarEvaluacionesCortes", "", codigo_cup, fecha_ini, fecha_fin)
            obj.CerrarConexion()
            Session("gc_dtEvaluacion") = dt
            Dim tfield As New TemplateField()
            For x As Integer = 0 To dt.Rows.Count - 1
                tfield = New TemplateField()
                'tfield.HeaderText = dt.Rows(x).Item("fecha_gru")
                tfield.HeaderText = dt.Rows(x).Item("descripcion_eva")
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
                Me.gvNotas.Columns.Add(tfield)
                tfield = New TemplateField()
                tfield.HeaderText = "Externas"
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
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.VerticalAlign = VerticalAlign.Middle
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Private Sub mt_RefreshGrid()
        For Each _Row As GridViewRow In Me.gvNotas.Rows
            gvNotas_OnRowDataBound(Me.gvNotas, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Private Sub mt_CargarNotas(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_NotasParciales_listar", "", -1, -1, "P", codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtNotas") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarEdiciones(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_EvaluacionCurso_Edicion_listar", "", codigo_cup, -1)
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
                dv = New Data.DataView(CType(Session("gc_dtNivelLogro"), Data.DataTable), "", "rangoDesde_niv ASC", Data.DataViewRowState.CurrentRows)
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
                    lbl.Text = "_____"
                    Me.divLeyenda.Controls.Add(lbl)
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_ValidarNotas(ByVal codigo_eva As Integer) As Boolean
        Dim txt As TextBox
        Dim _nombre_alu As String
        Dim dt As Data.DataTable
        dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
        For i As Integer = 0 To dt.Rows.Count - 1
            If codigo_eva = CInt(dt.Rows(i).Item("codigo_eva")) Then
                For j As Integer = 1 To Me.gvNotas.Rows.Count - 1
                    txt = Me.gvNotas.Rows(j).FindControl("txtNota" & (i + 1))
                    _nombre_alu = Me.gvNotas.DataKeys(j).Values("nombre_alu")
                    If String.IsNullOrEmpty(txt.Text.Trim) Then
                        mt_ShowMessage("¡ Ingrese Calificación al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                        txt.Focus()
                        Return False
                    End If
                    If CDbl(txt.Text.Trim) < 0 Or CDbl(txt.Text.Trim) > 20 Then
                        mt_ShowMessage("¡ Ingrese Calificación al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                        txt.Focus()
                        Return False
                    End If
                Next
                Exit For
            End If
        Next
        Return True
    End Function

    Private Function fc_getNota(ByVal codigo_dma As Integer, ByVal codigo_eva As Integer) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNotas"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtNotas"), Data.DataTable), "codigo_Dma = " & codigo_dma & " AND codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return dt.Rows(0).Item("nota_nop").ToString
            End If
        End If
        Return String.Empty
    End Function

    Private Function fc_getCodNota(ByVal codigo_dma As Integer, ByVal codigo_eva As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNotas"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(CType(Session("gc_dtNotas"), Data.DataTable), "codigo_Dma = " & codigo_dma & " AND codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
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

    Private Function fc_ValidarCorte(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCorteCurso"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtCorteCurso"), Data.DataTable), "codigo_cor = " & codigo_cor & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Function fc_validarEdicionTxt(ByVal codigo_dma As Integer, ByVal codigo_eva As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtNotasEditar"), Data.DataTable), "codigo_Dma = " & codigo_dma & " AND codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function fc_validarEdicionBtn(ByVal codigo_eva As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtNotasEditar"), Data.DataTable), "codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function fc_getCodEdicion(ByVal codigo_dma As Integer, ByVal codigo_eva As Integer) As Integer
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        Try
            dt = CType(Session("gc_dtNotasEditar"), Data.DataTable)
            If dt.Rows.Count > 0 Then
                dv = New Data.DataView(CType(Session("gc_dtNotasEditar"), Data.DataTable), "codigo_Dma = " & codigo_dma & " AND codigo_eva = " & codigo_eva, "", Data.DataViewRowState.CurrentRows)
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
            dv = New Data.DataView(CType(Session("gc_dtCondiciones"), Data.DataTable), "codigo_Dma = " & codigo_dma, "", Data.DataViewRowState.CurrentRows)
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
            dv = New Data.DataView(CType(Session("gc_dtCorteCurso"), Data.DataTable), "codigo_cor = " & codigo_cor, "", Data.DataViewRowState.CurrentRows)
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
            dv = New Data.DataView(CType(Session("gc_dtCondiciones"), Data.DataTable), "codigo_Dma = " & codigo_dma, "", Data.DataViewRowState.CurrentRows)
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
            dv = New Data.DataView(CType(Session("gc_dtNivelLogro"), Data.DataTable), "", "rangoDesde_niv DESC", Data.DataViewRowState.CurrentRows)
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
        For x As Integer = 1 To Me.gvNotas.Rows.Count - 1
            lbl = CType(Me.gvNotas.Rows(x).FindControl("lblCalificacion"), Label)
            _nombre_alu = Me.gvNotas.DataKeys(x).Values("nombre_alu")
            If CDbl(lbl.Text.Trim) < 14.0 Then
                Dim cboInt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboInterno"), DropDownList)
                Dim cboExt As DropDownList = CType(Me.gvNotas.Rows(x).FindControl("cboExterno"), DropDownList)
                If cboInt.SelectedValue = -1 Then
                    mt_ShowMessage("¡ Seleccione Condición Interna al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                    cboInt.Focus()
                    Return False
                End If
                If cboExt.SelectedValue = -1 Then
                    mt_ShowMessage("¡ Seleccione Condición Externa al Estudiante:  " & _nombre_alu & "!", MessageType.Warning)
                    cboExt.Focus()
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Function fc_getColorNivelLogro(ByVal nota As Double) As String
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtNivelLogro"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtNivelLogro"), Data.DataTable), "", "rangoDesde_niv DESC", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            For i As Integer = 0 To dt.Rows.Count - 1
                If CDbl(dt.Rows(i).Item("rangoDesde_niv")) <= nota Then
                    Return dt.Rows(i).Item("color_niv").ToString
                End If
            Next
        End If
        Return ""
    End Function

#End Region

End Class
