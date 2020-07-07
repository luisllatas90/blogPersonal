
Partial Class GestionCurricular_frmGenerarPromedio
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer '= 2238
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

            If Not IsPostBack Then
                Session("isValidated") = 0

                Me.lblCurso.InnerText = "Asignatura: " & Session("gc_nombre_cur")
                mt_CargarNotas(Session("gc_codigo_cup"))
                'mt_CargarEdiciones(Session("gc_codigo_cup"))
                'If Session("gc_enviar") Then mt_CargarCondiciones() : mt_CargarCondiciones(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                'mt_CargarCortes(Session("gc_codigo_cor"), Session("gc_codigo_cup"))
                mt_CrearColumns(Session("gc_codigo_cup"), Session("gc_fecha_ini"), Session("gc_fecha_fin"))
                mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cup"))
                mt_ObtenerCelular()

                divEnviar.Style.Item("display") = "none"
                divTexto.Style.Item("display") = "none"
            Else
                divEnviar.Style.Item("display") = "block"
                divTexto.Style.Item("display") = "block"

                mt_RefreshGrid()
            End If
            mt_MostrarLeyenda()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As New Data.DataTable
        Dim _codigo_dma, _codigo_eva, _codigo_emd, _tipo_prom, _cant_ins, _codigo_res, _cont_res, _cont_eva As Integer
        Dim _descripcion_eva, _nota As String
        Dim _sumatoria, _peso_res, _promedio_final As Double
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                _codigo_dma = CInt(Me.gvNotas.DataKeys(e.Row.RowIndex).Values("codigo_Dma"))
                _sumatoria = 0 : _peso_res = 0 : _promedio_final = 0
                _codigo_res = -1 : _cont_eva = 0 : _cont_res = 0
                For i As Integer = 0 To dt.Rows.Count - 1
                    _codigo_eva = CInt(dt.Rows(i).Item("codigo_eva"))
                    _descripcion_eva = dt.Rows(i).Item("descripcion_eva")
                    _codigo_emd = dt.Rows(i).Item("codigo_emd")
                    _tipo_prom = dt.Rows(i).Item("promedio_indicador")
                    _cant_ins = dt.Rows(i).Item("cant_ins")
                    If _codigo_dma <> -1 Then
                        If _codigo_res <> CInt(dt.Rows(i).Item("codigo_res")) Then
                            _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                            If i <> 0 Then
                                Dim lblProm As New Label()
                                lblProm.ID = "lblProm" & (_cont_res + 1)
                                lblProm.Text = Math.Round((_sumatoria / _cont_eva), 2).ToString("00.00")
                                If lblProm.Text.Trim <> "" Then lblProm.ForeColor = IIf(CDbl(lblProm.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                                e.Row.Cells(nro_col + i + _cont_res).Controls.Add(lblProm)
                                e.Row.Cells(nro_col + i + _cont_res).BackColor = Drawing.Color.LightGray
                                _cont_res += 1
                                _peso_res = Math.Round(CDbl(dt.Rows(i - 1).Item("peso_res")), 2)
                                _promedio_final += (CDbl(lblProm.Text.Trim) * _peso_res)
                                '_promedio_final += _peso_res
                            End If
                            _sumatoria = 0
                            _cont_eva = 0
                        End If
                        Dim lbl As New Label()
                        lbl.ID = "lblNota" & (i + 1)
                        lbl.Text = fc_getNota(_codigo_dma, _codigo_eva)
                        If lbl.Text.Trim <> "" Then lbl.ForeColor = IIf(CDbl(lbl.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                        e.Row.Cells(nro_col + i + _cont_res).Controls.Add(lbl)
                        If _tipo_prom = 1 Then
                            _nota = fc_getNota(_codigo_dma, _codigo_eva)
                            If _nota.Trim <> "" Then _sumatoria += CDbl(_nota)
                        End If
                        _cont_eva += 1
                    End If
                Next
                If _codigo_dma <> -1 Then
                    Dim lblProm2 As New Label()
                    lblProm2.ID = "lblProm" & (_cont_res + 1)
                    lblProm2.Text = Math.Round((_sumatoria / _cont_eva), 2).ToString("00.00")
                    If lblProm2.Text.Trim <> "" Then lblProm2.ForeColor = IIf(CDbl(lblProm2.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res).Controls.Add(lblProm2)
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res).BackColor = Drawing.Color.LightGray
                    _peso_res = Math.Round(CDbl(dt.Rows(dt.Rows.Count - 1).Item("peso_res")), 2)
                    _promedio_final += (CDbl(lblProm2.Text.Trim) * _peso_res)
                    '_promedio_final += _peso_res
                    Dim lblPromFinal As New Label()
                    lblPromFinal.ID = "lblPromFinal"
                    lblPromFinal.Text = _promedio_final.ToString("00.00")
                    lblPromFinal.Font.Bold = True
                    If lblPromFinal.Text.Trim <> "" Then lblPromFinal.ForeColor = IIf(CDbl(lblPromFinal.Text.Trim) < 14, Drawing.Color.Red, Drawing.Color.Blue)
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).Controls.Add(lblPromFinal)
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).BackColor = Drawing.Color.LightSteelBlue
                    Dim lblStep As New Label()
                    lblStep.ID = "lblStep"
                    lblStep.Text = " "
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).Controls.Add(lblStep)
                    Dim lblNivel As New Label()
                    lblNivel.ID = "lblColor"
                    lblNivel.ForeColor = Drawing.Color.Transparent
                    lblNivel.Text = "<br/>_____"
                    lblNivel.Style.Add("background-color", fc_getColorNivelLogro(_promedio_final))
                    e.Row.Cells(nro_col + dt.Rows.Count + _cont_res + 1).Controls.Add(lblNivel)
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
                    'End If
                    ' e.Row.Cells(2 + i).Width = (60 / dt.Rows.Count)
                Next
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvNotas_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim dt As New Data.DataTable
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
                mt_AgregarCabecera(objgridviewrow, objtablecell, 2, 1, "UNIDADES")
                For i As Integer = 0 To dt.Rows.Count - 1
                    If _codigo_uni <> CInt(dt.Rows(i).Item("codigo_uni")) Then
                        If i <> 0 Then
                            mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva + 1, 1, dt.Rows(i - 1).Item("numero_uni") & " : " & dt.Rows(i - 1).Item("descripcion_uni"))
                        End If
                        _codigo_uni = CInt(dt.Rows(i).Item("codigo_uni"))
                        _nro_eva = 0
                    End If
                    _nro_eva += 1
                    If _codigo_res <> CInt(dt.Rows(i).Item("codigo_res")) Then
                        If i <> 0 Then
                            mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res + 1, 1, dt.Rows(i - 1).Item("descripcion_res"))
                        Else
                            mt_AgregarCabecera(objgridviewrow2, objtablecell, 2, 1, "RESULTADOS DE APRENDIZAJES (RA)")
                        End If
                        _codigo_res = CInt(dt.Rows(i).Item("codigo_res"))
                        _nro_res = 0
                    End If
                    If _codigo_ind <> CInt(dt.Rows(i).Item("codigo_ind")) Then
                        If i <> 0 Then
                            mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "Indicador : " & _nombre_ind, _tooltip_ind)
                        Else
                            mt_AgregarCabecera(objgridviewrow3, objtablecell, 2, 1, "DATOS DEL ESTUDIANTE")
                        End If
                        _codigo_ind = CInt(dt.Rows(i).Item("codigo_ind"))
                        _nombre_ind = dt.Rows(i).Item("descripcion_ind")
                        If _nombre_ind.Trim.Length > 30 Then
                            _tooltip_ind = _nombre_ind
                            _nombre_ind = _nombre_ind.Substring(0, 30) & " ..."
                        Else
                            _tooltip_ind = String.Empty
                        End If
                        _nro_ind = 0
                    End If
                    If i <> 0 AndAlso _nro_res = 0 Then
                        mt_AgregarCabecera(objgridviewrow3, objtablecell, 1, 1, "Peso<br/>" & dt.Rows(i - 1).Item("peso_res") * 100 & " %")
                    End If
                    _nro_res += 1
                    _nro_ind += 1
                Next
                mt_AgregarCabecera(objgridviewrow, objtablecell, _nro_eva + 1, 1, dt.Rows(dt.Rows.Count - 1).Item("numero_uni") & " : " & dt.Rows(dt.Rows.Count - 1).Item("descripcion_uni"))
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 3, " ")
                mt_AgregarCabecera(objgridviewrow2, objtablecell, _nro_res + 1, 1, dt.Rows(dt.Rows.Count - 1).Item("descripcion_res"))
                mt_AgregarCabecera(objgridviewrow3, objtablecell, _nro_ind, 1, "Indicador : " & _nombre_ind, _tooltip_ind)
                mt_AgregarCabecera(objgridviewrow3, objtablecell, 1, 1, "Peso<br/>" & dt.Rows(dt.Rows.Count - 1).Item("peso_res") * 100 & " %")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
                objGridView.Controls(0).Controls.AddAt(1, objgridviewrow2)
                objGridView.Controls(0).Controls.AddAt(2, objgridviewrow3)
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

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Response.Redirect("~/gestioncurricular/frmResumenCalificador.aspx")
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If Session("isValidated") = 1 Then
            Dim lblProm As Label
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            Dim _codigo_dma, _codigo_res, _codigo_niv, _cont_res As Integer
            Dim _peso_res, _promedio_final As Double
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            Try
                'If fc_validarCortes(Session("gc_codigo_cup")) Then
                dt = CType(Session("gc_dtEvaluacion"), Data.DataTable)
                _codigo_dma = -1 : _codigo_res = -1
                obj.IniciarTransaccion()
                For x As Integer = 1 To Me.gvNotas.Rows.Count - 1
                    _codigo_dma = CInt(Me.gvNotas.DataKeys(x).Values("codigo_Dma"))
                    _cont_res = 0 : _peso_res = 0 : _promedio_final = 0
                    For y As Integer = 0 To dt.Rows.Count - 1
                        If _codigo_res <> CInt(dt.Rows(y).Item("codigo_res")) Then
                            _codigo_res = CInt(dt.Rows(y).Item("codigo_res"))
                            _peso_res = dt.Rows(y).Item("peso_res")
                            _cont_res += 1
                            lblProm = Me.gvNotas.Rows(x).FindControl("lblProm" & _cont_res)
                            _codigo_niv = fc_getCodNivelLogro(CDbl(lblProm.Text.Trim))
                            _promedio_final += (CDbl(lblProm.Text.Trim) * _peso_res)
                            obj.Ejecutar("DEA_NotaResultadoAprendizaje_insertar", Session("gc_codigo_cup"), _codigo_dma, _codigo_res, CDbl(lblProm.Text.Trim), _codigo_niv, cod_user)
                        End If
                    Next
                    _codigo_niv = fc_getCodNivelLogro(_promedio_final)
                    obj.Ejecutar("DEA_DetalleMatricula_PromedioFinal", _codigo_dma, _promedio_final, _codigo_niv, cod_user)
                Next

                obj.TerminarTransaccion()
                mt_ShowMessage("¡ Se generó correctamente las Calificaciones !", MessageType.Success)
                Page.RegisterStartupScript("Pop", "<script>showDivs('hide');</script>")
                'End If
            Catch ex As Exception
                obj.AbortarTransaccion()
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
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_GenerarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA FINAL")
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
                dt = obj.TraerDataTable("DEA_ConfirmarTokenNotas", Session("gc_codigo_cup"), cod_user, "NOTA FINAL", txtToken.Text)
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

    Private Sub mt_RefreshGrid()
        For Each _Row As GridViewRow In Me.gvNotas.Rows
            gvNotas_OnRowDataBound(Me.gvNotas, New GridViewRowEventArgs(_Row))
        Next
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
        Catch ex As Exception
            Throw ex
        End Try
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

    Private Sub mt_CrearColumns(ByVal codigo_cup As Integer, ByVal fecha_ini As Date, ByVal fecha_fin As Date)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim _codigo_res As Integer = -1
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarEvaluacionesCortes", "", codigo_cup, fecha_ini, fecha_fin)
            obj.CerrarConexion()
            Session("gc_dtEvaluacion") = dt
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
                tfield = New TemplateField()
                tfield.HeaderText = dt.Rows(x).Item("descripcion_eva")
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.gvNotas.Columns.Add(tfield)
            Next
            tfield = New TemplateField()
            tfield.HeaderText = "Promedio Parcial"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvNotas.Columns.Add(tfield)
            tfield = New TemplateField()
            tfield.HeaderText = "Promedio Final"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvNotas.Columns.Add(tfield)
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
