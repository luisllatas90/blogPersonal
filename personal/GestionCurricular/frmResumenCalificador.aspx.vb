﻿
Partial Class GestionCurricular_frmResumenCalificador
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private orden_col As Integer = 4

    Private uni_aux As String = ""
    Private tot_uni As Integer = 1

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

            If Not String.IsNullOrEmpty(Session("cod_ctf")) Then
                cod_ctf = Session("cod_ctf")
            Else
                cod_ctf = Request.QueryString("ctf")
            End If

            If Not IsPostBack Then
                Session("gc_codigo_sgr") = Nothing

                Dim dtDesarrollo As Data.DataTable = New Data.DataTable()
                Dim dtDesarrolloElim As Data.DataTable = New Data.DataTable()
                Dim dtDificultad As Data.DataTable = New Data.DataTable()
                Dim dtDificultadElim As Data.DataTable = New Data.DataTable()

                dtDesarrollo.Columns.Add("numero_uni")
                dtDesarrollo.Columns.Add("codigo_uni")
                dtDesarrollo.Columns.Add("codigo_uad")
                dtDesarrollo.Columns.Add("comentario_uad")
                ViewState("dtDesarrollo") = dtDesarrollo

                dtDesarrolloElim.Columns.Add("codigo_uad")
                ViewState("dtDesarrolloElim") = dtDesarrolloElim

                dtDificultad.Columns.Add("codigo_udf")
                dtDificultad.Columns.Add("codigo_uae")
                dtDificultad.Columns.Add("descripcion_uae")
                dtDificultad.Columns.Add("comentario_udf")
                ViewState("dtDificultad") = dtDificultad

                dtDificultadElim.Columns.Add("codigo_udf")
                ViewState("dtDificultadElim") = dtDificultadElim

                Session("gc_dtCursoProg") = Nothing
                Call mt_CargarSemestre()
                Call mt_CargarNivelLogro()

                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False

                If Not String.IsNullOrEmpty(Session("gc_codigo_cac")) Then
                    If cboSemestre.Items.Count > 0 Then Me.cboSemestre.SelectedValue = Session("gc_codigo_cac")
                    If cboSemestre.Items.Count > 0 Then Call mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user, cod_ctf)
                    If cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = Session("gc_codigo_cpf")
                    mt_CrearColumns(CType(Session("gc_dtCorte"), Data.DataTable))
                    mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cpf"), cod_user, cod_ctf)
                    Session("gc_codigo_cac") = "" : Session("gc_codigo_cpf") = ""
                End If
            Else
                mt_RefreshGrid()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            If Me.cboSemestre.SelectedValue = -1 Then
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                mt_CargarCarreraProfesional(0, cod_user, cod_ctf)
                Me.cboSemestre.Focus()
                Exit Sub
            End If
            mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_user, cod_ctf)
            'mt_ShowMessage(cod_user, MessageType.Success)
            mt_CargarCierres(Me.cboSemestre.SelectedValue)
            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarDatos(0, 0, 0, 0)
            'cboCarrProf_SelectedIndexChanged(Nothing, Nothing)
            'mt_CrearColumns(CType(Session("gc_dtCorte"), Data.DataTable))
            'mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, cod_user)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            If Me.cboCarrProf.SelectedValue = -1 Then
                mt_ShowMessage("¡ Seleccione Carrera Profesional !", MessageType.Warning)
                mt_CargarDatos(0, 0, 0, 0)
                Me.cboCarrProf.Focus()
                Exit Sub
            End If
            mt_CrearColumns(CType(Session("gc_dtCorte"), Data.DataTable))
            mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, cod_user, cod_ctf)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSubGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubGrupo.SelectedIndexChanged
        Try
            If ddlSubGrupo.Items.Count > 0 Then
                Session("gc_codigo_sgr") = ddlSubGrupo.SelectedValue.ToString
            Else
                Session("gc_codigo_sgr") = "-1"
            End If

            Call mt_CargarContenido()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Session("gc_dtCursoProg") Is Nothing Then
                mt_CargarDatos(Session("gc_codigo_cac"), Session("gc_codigo_cpf"), cod_user, cod_ctf)
            End If

            Dim dt As New Data.DataTable
            Dim dv As New Data.DataView
            Dim strBuscar As String = ""

            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_dtCursoProg"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_dtCursoProg"), Data.DataTable)
            End If

            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()

            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        If Session("gc_dtCursoProg") IsNot Nothing Then
            Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
            Me.gvAsignatura.DataBind()
        End If

        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub gvAsignatura_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            Dim dt As Data.DataTable
            dt = CType(Session("gc_dtCorte"), Data.DataTable)
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, orden_col, "Mis Cursos", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Enlazar", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Avance", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, dt.Rows.Count, "Seguimiento Evaluación", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Observaciones", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, "Visualizar", "#D9534F") 'Here
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, " ", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
        Dim index As Integer
        Dim dt, dtCiclo As New Data.DataTable
        Dim _modular As Boolean = False
        Dim ciclo, grupo As String
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                _modular = CBool(Me.gvAsignatura.DataKeys(index).Values("modular_pcu"))

                Session("gc_codigo_cac") = Me.cboSemestre.SelectedValue
                Session("gc_codigo_cpf") = Me.cboCarrProf.SelectedValue
                Session("gc_nombre_cur") = Me.gvAsignatura.DataKeys(index).Values("nombre_Cur").ToString & " - " & Me.gvAsignatura.DataKeys(index).Values("grupoHor_Cup").ToString
                Session("gc_codigo_cup") = Me.gvAsignatura.DataKeys(index).Values("codigo_cup").ToString
                Session("cod_ctf") = cod_ctf
                ciclo = Me.gvAsignatura.DataKeys(index).Values("cicloRom").ToString
                grupo = Me.gvAsignatura.DataKeys(index).Values("grupoHor_Cup").ToString
                dtCiclo = CType(Session("gc_dtSemestre"), Data.DataTable)
                For x As Integer = 0 To dtCiclo.Rows.Count - 1
                    If Me.cboSemestre.SelectedValue = dtCiclo.Rows(x).Item("codigo_cac") Then
                        Session("gc_fecha_ini") = dtCiclo.Rows(x).Item("FechaIni_cac")
                    End If
                Next
                If e.CommandName = "Moodle" Then
                    Response.Redirect("~/gestioncurricular/frmEnlazarEvaluacionMoodle.aspx")
                ElseIf e.CommandName = "Visualizar" Then
                    dt = CType(Session("gc_dtCorte"), Data.DataTable)

                    If dt.Rows.Count > 0 Then
                        Session("gc_fecha_fin") = dt.Rows(dt.Rows.Count - 1).Item("fecha")
                        Session("gc_visualizar") = "1"
                        Response.Redirect("~/gestioncurricular/frmGenerarPromedio.aspx")
                    Else
                        mt_ShowMessage("Aún no se han establecido las fechas de corte", MessageType.Info)
                    End If
                ElseIf e.CommandName = "Generar" Then
                    dt = CType(Session("gc_dtCorte"), Data.DataTable)

                    If dt.Rows.Count > 0 Then
                        Session("gc_fecha_fin") = dt.Rows(dt.Rows.Count - 1).Item("fecha")
                        Session("gc_visualizar") = "0"
                        Response.Redirect("~/gestioncurricular/frmGenerarPromedio.aspx")
                    Else
                        mt_ShowMessage("Aún no se han establecido las fechas de corte", MessageType.Info)
                    End If
                ElseIf e.CommandName = "Observacion" Then
                    Me.lblTitulo.InnerText = Session("gc_nombre_cur") & " [Ciclo: " & ciclo & ", Grupo: " & grupo & "]"
                    Call mt_CargarDesarrollo()
                    Call mt_CargarDificultad()
                    Page.RegisterStartupScript("Pop", "<script>openModal('');</script>")
                ElseIf e.CommandName = "Contenido" Then
                    Me.lblTituloContenido.InnerText = Session("gc_nombre_cur") & " [Ciclo: " & ciclo & ", Grupo: " & grupo & "]"

                    If _modular Then
                        divSubGrupo.Visible = True
                        Call mt_CargarSubGrupos(Session("gc_codigo_cup"))
                    Else
                        divSubGrupo.Visible = False
                    End If

                    Call mt_CargarContenido()
                    Page.RegisterStartupScript("Pop", "<script>openModal('contenido');</script>")
                ElseIf e.CommandName = "AulaVirtual" Then
                    Dim obj As New ClsConectarDatos
                    Dim dtUsr As New Data.DataTable
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
                    obj.AbrirConexion()
                    dtUsr = obj.TraerDataTable("MOODLE_ConsultarCodigoAcceso", "PE", cod_user)
                    obj.CerrarConexion()
                    If dtUsr.Rows.Count > 0 Then
                        Dim _url = "//intranet.usat.edu.pe/aulavirtual/login/index.php"
                        Dim _user = dtUsr.Rows(0).Item("codigo_pso").ToString
                        Dim _pass = dtUsr.Rows(0).Item("ClaveInterna_Pso").ToString
                        Dim _curso = "grades"
                        Dim _idcurso = Me.gvAsignatura.DataKeys(index).Values("idcurso_mdl")
                        If _idcurso <> "-1" Then
                            Response.Clear()
                            Dim sb = New System.Text.StringBuilder()
                            sb.Append("<html>")
                            sb.AppendFormat("<body onload='document.forms[0].submit()'>")
                            sb.AppendFormat("<form action='{0}' method='post' target='_blank'>", _url)
                            sb.AppendFormat("<input type='hidden' name='avm7' value='{0}'>", _user) ' Response.write (pso) - cifrado de siuasivo2 y disuasivo3
                            sb.AppendFormat("<input type='hidden' name='avm8' value='{0}'>", _pass) ' Response.write (PassPer) - cifrado de siuasivo2 y disuasivo3
                            sb.AppendFormat("<input type='hidden' name='curso' value='{0}'>", _curso) ' no
                            sb.AppendFormat("<input type='hidden' name='idcurso' value='{0}'>", _idcurso) ' no
                            sb.Append("</form>")
                            sb.Append("</body>")
                            sb.Append("</html>")
                            Response.Write(sb.ToString())
                        Else
                            mt_ShowMessage("¡ No se puede acceder al Aula Virtual porque aún no ha sido creada !", MessageType.Info)
                        End If
                    Else
                        mt_ShowMessage("¡ Ocurrió un Error para iniciar sesión en el Aula Virtual !", MessageType.Warning)
                    End If
                Else
                    Dim _nombre_form As String
                    _nombre_form = IIf(_modular, "frmCalificadorCurso_Modular.aspx", "frmCalificadorCurso.aspx")

                    dt = CType(Session("gc_dtCorte"), Data.DataTable)
                    For i As Integer = 0 To dt.Rows.Count - 1
                        'If i = 0 Then
                        '    'Session("gc_fecha_ini") = CDate("16/03/2017")
                        'Else
                        '    Session("gc_fecha_ini") = CDate(dt.Rows(i - 1).Item("fecha").ToString)
                        'End If
                        Session("gc_fecha_fin") = dt.Rows(i).Item("fecha")
                        Session("gc_codigo_cor") = dt.Rows(i).Item("codigo_cor")
                        Session("gc_enviar") = False
                        If e.CommandName = "Corte" & (i + 1) Then
                            Response.Redirect("~/gestioncurricular/" & _nombre_form)
                        End If
                        If e.CommandName = "Enviar" & (i + 1) Then
                            Session("gc_enviar") = True
                            Response.Redirect("~/gestioncurricular/" & _nombre_form)
                        End If
                    Next
                End If
                'mt_ShowMessage(e.CommandName, MessageType.Warning)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " ").Replace(vbCr, " ").Replace(vbLf, ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Dim btn As New LinkButton()
        Dim _codigo_cor, _codigo_cup, _nro_col, _codigo_dis, _codigo_cor_ant As Integer
        Dim _nro_cortes, _nrosemana_cor, _nrosemana_cor_ant As Integer
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim dt As Data.DataTable
                dt = CType(Session("gc_dtCorte"), Data.DataTable)
                _codigo_cor = -1
                _codigo_cor_ant = -1
                _codigo_cup = CInt(Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("codigo_cup"))
                _codigo_dis = CInt(Me.gvAsignatura.DataKeys(e.Row.RowIndex).Values("codigo_dis"))
                _nro_col = 0
                _nro_cortes = 0
                _nrosemana_cor = 0 : _nrosemana_cor_ant = 0

                '----------------------------------esaavedra 03.05.2020
                btn = New LinkButton()
                btn.ID = "btnIrAula"
                btn.Text = "<i class='fa fa-share-square'></i>"
                btn.CommandArgument = e.Row.RowIndex
                btn.CommandName = "AulaVirtual"
                btn.ToolTip = "Ir a Aula Virtual"
                btn.OnClientClick = "return confirm('¿Desea ir al aula virtual?');"
                btn.CssClass = "btn btn-default btn-sm"
                e.Row.Cells(orden_col).Controls.Add(btn)
                '----------------------------------

                If _codigo_dis <> -1 Then
                    ' Crear button Para Enlazar Moodle
                    If cod_ctf <> 13 Then
                        btn = New LinkButton()
                        btn.ID = "btnMoodle"
                        btn.Text = "<i class='fa fa-link'></i>"
                        btn.CommandArgument = e.Row.RowIndex
                        btn.CommandName = "Moodle"
                        btn.ToolTip = "Alinear Instrumentos de Evaluación con Tareas de Aula Virtual"
                        btn.OnClientClick = "return confirm('¿Desea ingresar información?');"
                        btn.CssClass = "btn btn-warning btn-sm"
                        e.Row.Cells(orden_col).Controls.Add(btn)
                    End If

                    Dim lbl2 As New Label()
                    lbl2.ID = "lblStep2" & _nro_col
                    lbl2.Text = " "
                    e.Row.Cells(orden_col).Controls.Add(lbl2)

                    '----------------------------------esaavedra 03.05.2020
                    'btn = New LinkButton()
                    'btn.ID = "btnIrAula"
                    'btn.Text = "<i class='fa fa-share-square'></i>"
                    'btn.CommandArgument = e.Row.RowIndex
                    'btn.CommandName = "AulaVirtual"
                    'btn.ToolTip = "Ir a Aula Virtual"
                    'btn.OnClientClick = "return confirm('¿Desea ir al aula virtual?');"
                    'btn.CssClass = "btn btn-default btn-sm"
                    'e.Row.Cells(orden_col).Controls.Add(btn)

                    ' Crear button Para Contenidos
                    btn = New LinkButton()
                    btn.ID = "btnContenido"
                    btn.Text = "<i class='fa fa-check-double'></i>"
                    btn.CommandArgument = e.Row.RowIndex
                    btn.CommandName = "Contenido"
                    btn.ToolTip = "Confirmar avance del contenido"
                    btn.OnClientClick = "return confirm('¿Desea ingresar contenido?');"
                    btn.CssClass = "btn btn-success btn-sm"
                    e.Row.Cells(orden_col + 1).Controls.Add(btn)

                    ' Crear buttons Para Momentos
                    If dt.Rows.Count = 0 Then
                        _nro_col = _nro_col + 1
                    End If

                    For i As Integer = 0 To dt.Rows.Count - 1
                        _nro_col = (i + 1)

                        _codigo_cor = CInt(dt.Rows(i).Item("codigo_cor"))
                        _nrosemana_cor = CInt(dt.Rows(i).Item("numeroSemana_cor"))

                        If fc_CorteEnviado(_codigo_cor, _codigo_cup) Then


                            btn = New LinkButton()
                            btn.ID = "Enviar" & _nro_col
                            btn.Text = "<i class='fa fa-check-circle'></i>"
                            btn.CommandArgument = e.Row.RowIndex
                            btn.CommandName = "Enviar" & _nro_col
                            btn.ToolTip = "Ingresar condiciones internas y externas"
                            'btn.OnClientClick = "return confirm('¿Desea enviar información de la " & dt.Rows(i).Item("nombre_sem") & "?');"
                            btn.CssClass = "btn btn-success btn-sm"
                            e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(btn)

                            _nro_cortes += 1

                        Else

                            If i > 0 Then

                                _codigo_cor_ant = CInt(dt.Rows(i - 1).Item("codigo_cor"))
                                _nrosemana_cor_ant = CInt(dt.Rows(i - 1).Item("numeroSemana_cor"))

                                If Not fc_CorteEvaluaciones(_codigo_cup, _nrosemana_cor_ant) Then

                                    If _nro_cortes = i Then

                                        If fc_CorteEvaluaciones(_codigo_cup, _nrosemana_cor) Then

                                            btn = New LinkButton()
                                            btn.ID = "Corte" & _nro_col
                                            btn.Text = "<i class='fas fa-cut'></i>"
                                            btn.CommandArgument = e.Row.RowIndex
                                            btn.CommandName = "Corte" & (_nro_col)
                                            btn.ToolTip = "Realizar corte de evaluaciones"
                                            'btn.OnClientClick = "return confirm('¿Desea ingresar información de la " & dt.Rows(i).Item("nombre_sem") & "?');"
                                            btn.CssClass = "btn btn-info btn-sm"
                                            e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(btn)

                                        Else

                                            Dim lblSinEva As New Label()
                                            lblSinEva.ID = "lblSinCorte" & _nro_col
                                            lblSinEva.ToolTip = "No hay evaluaciones programadas para este corte"
                                            lblSinEva.Font.Size = 16
                                            lblSinEva.Text = "<span style='color: orange'><i class='fa fa-ban'></i></span>"

                                            e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(lblSinEva)

                                            _nro_cortes += 1

                                        End If

                                    Else

                                        Dim lblPendiente As New Label()
                                        lblPendiente.ID = "lblPendiente" & _nro_col
                                        lblPendiente.ToolTip = "No se ha confirmado el corte anterior"
                                        lblPendiente.Font.Size = 16
                                        lblPendiente.Text = "<span style='color: gray'><i class='fa fa-exclamation-triangle'></i></span>"

                                        e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(lblPendiente)

                                    End If

                                Else

                                    If fc_CorteEnviado(_codigo_cor_ant, _codigo_cup) Then

                                        If fc_CorteEvaluaciones(_codigo_cup, _nrosemana_cor) Then

                                            btn = New LinkButton()
                                            btn.ID = "Corte" & _nro_col
                                            btn.Text = "<i class='fas fa-cut'></i>"
                                            btn.CommandArgument = e.Row.RowIndex
                                            btn.CommandName = "Corte" & (_nro_col)
                                            btn.ToolTip = "Realizar corte de evaluaciones"
                                            'btn.OnClientClick = "return confirm('¿Desea ingresar información de la " & dt.Rows(i).Item("nombre_sem") & "?');"
                                            btn.CssClass = "btn btn-info btn-sm"
                                            e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(btn)

                                        Else

                                            Dim lblSinEva As New Label()
                                            lblSinEva.ID = "lblSinCorte" & _nro_col
                                            lblSinEva.ToolTip = "No hay evaluaciones programadas para este corte"
                                            lblSinEva.Font.Size = 16
                                            lblSinEva.Text = "<span style='color: orange'><i class='fa fa-ban'></i></span>"

                                            e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(lblSinEva)

                                            _nro_cortes += 1

                                        End If

                                    Else

                                        Dim lblPendiente As New Label()
                                        lblPendiente.ID = "lblPendiente" & _nro_col
                                        lblPendiente.ToolTip = "No se ha confirmado el corte anterior"
                                        lblPendiente.Font.Size = 16
                                        lblPendiente.Text = "<span style='color: gray'><i class='fa fa-exclamation-triangle'></i></span>"

                                        e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(lblPendiente)

                                    End If

                                End If

                            Else

                                If fc_CorteEvaluaciones(_codigo_cup, _nrosemana_cor) Then

                                    btn = New LinkButton()
                                    btn.ID = "Corte" & _nro_col
                                    btn.Text = "<i class='fas fa-cut'></i>"
                                    btn.CommandArgument = e.Row.RowIndex
                                    btn.CommandName = "Corte" & (_nro_col)
                                    btn.ToolTip = "Realizar corte de evaluaciones"
                                    'btn.OnClientClick = "return confirm('¿Desea ingresar información de la " & dt.Rows(i).Item("nombre_sem") & "?');"
                                    btn.CssClass = "btn btn-info btn-sm"
                                    e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(btn)
                                Else

                                    Dim lblSinEva As New Label()
                                    lblSinEva.ID = "lblSinCorte" & _nro_col
                                    lblSinEva.ToolTip = "No hay evaluaciones programadas para este corte"
                                    lblSinEva.Font.Size = 16
                                    lblSinEva.Text = "<span style='color: orange'><i class='fa fa-ban'></i></span>"

                                    e.Row.Cells(orden_col + 1 + _nro_col).Controls.Add(lblSinEva)

                                    _nro_cortes += 1

                                End If

                            End If

                        End If
                    Next

                    ' Crear button Para Observaciones
                    If cod_ctf <> 218 Then
                        btn = New LinkButton()
                        btn.ID = "btnObservacion"
                        btn.Text = "<i class='fa fa-eye'></i>"
                        btn.CommandArgument = e.Row.RowIndex
                        btn.CommandName = "Observacion"
                        btn.OnClientClick = "return confirm('¿Desea ingresar observaciones?');"
                        btn.CssClass = "btn btn-primary btn-sm"
                        e.Row.Cells(orden_col + 1 + _nro_col + 1).Controls.Add(btn)
                    End If

                    ' Crear button Para Visualizar el registro de notas
                    'If _nro_cortes = dt.Rows.Count Then
                    btn = New LinkButton()
                    btn.ID = "btnVisualizar"
                    btn.Text = "<i class='fas fa-book'></i>"
                    btn.CommandArgument = e.Row.RowIndex
                    btn.CommandName = "Visualizar"
                    btn.CssClass = "btn btn-danger btn-sm"
                    e.Row.Cells(orden_col + 1 + _nro_col + 2).Controls.Add(btn)
                    'End If

                    ' Crear button Para Generar Promedio
                    If _nro_cortes = dt.Rows.Count And cod_ctf = 13 Then

                        btn = New LinkButton()
                        btn.ID = "btnGenerar"
                        btn.Text = "<i class='fa fa-arrow-alt-circle-right'></i>"
                        btn.CommandArgument = e.Row.RowIndex
                        btn.CommandName = "Generar"
                        'btn.OnClientClick = "return confirm('¿Desea generar el promedio?');"
                        btn.CssClass = "btn btn-primary btn-sm"
                        e.Row.Cells(orden_col + 1 + _nro_col + 3).Controls.Add(btn) 'Here

                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvContenido_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvContenido.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim numero_uni As String = gvContenido.DataKeys(e.Row.RowIndex).Item("numero_uni").ToString
                Dim unidad_des As String = gvContenido.DataKeys(e.Row.RowIndex).Item("descripcion_uni").ToString

                If Not numero_uni.Equals(uni_aux) Then
                    uni_aux = numero_uni
                    Dim objGridView As GridView = CType(sender, GridView)
                    Dim objGridViewRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                    Dim objTableCell As TableCell = New TableCell()
                    Dim fila As Integer = objGridView.Rows.Count

                    mt_AgregarCabecera(objGridViewRow, objTableCell, gvContenido.Columns.Count, uni_aux & ": " & unidad_des, "#E3DFDF", True)
                    objGridView.Controls(0).Controls.AddAt(fila + tot_uni, objGridViewRow)

                    tot_uni += 1
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            updMensaje.Update()
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
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
            Session("gc_dtSemestre") = dt
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal user As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            If ctf = 1 Or ctf = 232 Or ctf = 41 Then user = -1
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV3", "CC", codigo_cac, ctf, user, ctf)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As String, ByVal codigo_cpf As String, ByVal user As String, ByVal ctf As String)
        Dim obj As New ClsConectarDatos
        Dim dt, dtAux, dtEC As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            codigo_cac = IIf(String.IsNullOrEmpty(codigo_cac), "0", codigo_cac)
            codigo_cpf = IIf(String.IsNullOrEmpty(codigo_cpf), "0", codigo_cpf)
            user = IIf(String.IsNullOrEmpty(user), "0", user)
            ctf = IIf(String.IsNullOrEmpty(ctf), "0", ctf)
            
            If ctf = 1 Or ctf = 232 Or ctf = 9 Or ctf = 41 Then user = -1

            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar", "CE", ctf, codigo_cpf, user, codigo_cac)
            dtAux = obj.TraerDataTable("DEA_CortesCurso_listar", "CE", IIf(ctf = 13, user, -1), -1, -1)
            dtEC = obj.TraerDataTable("DEA_CursoEvaluacionCorte", codigo_cac)
            obj.CerrarConexion()

            Session("gc_dtCursoProg") = dt
            Session("gc_dtCorteEnviados") = dtAux
            Session("gc_dtCursoEvaCorte") = dtEC

            Me.gvAsignatura.DataSource = dt 'CType(Session("gc_dtCursoProg"), Data.DataTable)
            Me.gvAsignatura.DataBind()

            dt.Dispose()
            dtAux.Dispose()
            dtEC.Dispose()

            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String, Optional ByVal paint As Boolean = False)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan

        If paint Then
            objtablecell.Style.Add("background-color", backcolor)
            objtablecell.Style.Add("font-weight", "600")
        End If

        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub


    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvAsignatura.Rows
                gvAsignatura_OnRowDataBound(Me.gvAsignatura, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCierres(ByVal codigo_cac As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarCorteSemestre", codigo_cac, "P")
            obj.CerrarConexion()
            Session("gc_dtCorte") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CrearColumns(ByVal dtColumns As Data.DataTable)
        Dim tfield As New TemplateField()
        Try
            If Me.gvAsignatura.Columns.Count > orden_col Then
                'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
                For i As Integer = 5 To Me.gvAsignatura.Columns.Count
                    Me.gvAsignatura.Columns.RemoveAt(Me.gvAsignatura.Columns.Count - 1)
                Next
            End If
            ' Crear Column para enlazar con Moodle
            tfield = New TemplateField()
            tfield.HeaderText = "Aula Virtual"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvAsignatura.Columns.Add(tfield)
            ' Crear Column para Avance de Contenido
            tfield = New TemplateField()
            tfield.HeaderText = "del Contenido"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvAsignatura.Columns.Add(tfield)
            ' Crear Columns para Momentos
            If dtColumns.Rows.Count = 0 Then
                tfield = New TemplateField()
                tfield.HeaderText = ""
                tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                Me.gvAsignatura.Columns.Add(tfield)
            Else
                For x As Integer = 0 To dtColumns.Rows.Count - 1
                    tfield = New TemplateField()
                    tfield.HeaderText = dtColumns.Rows(x).Item("nombre_sem")
                    tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    Me.gvAsignatura.Columns.Add(tfield)
                Next
            End If
            ' Crear Column para ingresar observacion
            tfield = New TemplateField()
            tfield.HeaderText = "del Curso"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvAsignatura.Columns.Add(tfield)
            ' Crear Column para Registro de Notas
            tfield = New TemplateField()
            tfield.HeaderText = "Registro Notas"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvAsignatura.Columns.Add(tfield) 'Here
            ' Crear Column para public Promedio Final
            tfield = New TemplateField()
            tfield.HeaderText = "Promedios"
            tfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center
            Me.gvAsignatura.Columns.Add(tfield)
            'mt_ShowMessage("mt_CrearColumns: " & dtColumns.Rows.Count & " | " & Me.gvAsignatura.Columns.Count, MessageType.Warning)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarNivelLogro()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarNivelLogroAprendizaje")
            obj.CerrarConexion()
            Session("gc_dtNivelLogro") = dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarSubGrupos(ByVal codigo_cup As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_cup = IIf(String.IsNullOrEmpty(codigo_cup), "-1", codigo_cup)

            dt = obj.TraerDataTable("DEA_subGrupoProgramado_listar", codigo_cup, -1, -1, "CG")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlSubGrupo, dt, "codigo_sgr", "descripcion_sgr")

            If ddlSubGrupo.Items.Count > 0 Then
                Session("gc_codigo_sgr") = ddlSubGrupo.SelectedValue.ToString
            Else
                Session("gc_codigo_sgr") = "-1"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_CorteEnviado(ByVal codigo_cor As Integer, ByVal codigo_cup As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCorteEnviados"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtCorteEnviados"), Data.DataTable), "codigo_cor = " & codigo_cor & " AND codigo_cup = " & codigo_cup, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function fc_CorteEvaluaciones(ByVal codigo_cup As Integer, ByVal semana_cor As Integer) As Boolean
        Dim dv As Data.DataView
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtCursoEvaCorte"), Data.DataTable)
        If dt.Rows.Count > 0 Then
            dv = New Data.DataView(CType(Session("gc_dtCursoEvaCorte"), Data.DataTable), "codigo_cup = " & codigo_cup & " AND semana = " & semana_cor, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            If dt.Rows.Count > 0 Then
                Return True
            End If
        End If
        Return False
    End Function

#End Region

#Region "Desarrollo"

    Protected Sub BindGridDesarrollo()
        Dim dt As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvDesarrollo.DataSource = dt
            gvDesarrollo.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvDesarrollo.DataSource = dt
            gvDesarrollo.DataBind()

            gvDesarrollo.Rows(0).Cells.Clear()
        End If

        Me.udpDesarrollo.Update()

        Me.divAlertModal.Visible = False
        Me.lblMensaje.InnerText = ""
        updMensaje.Update()
    End Sub

    Private Sub mt_CargarDesarrollo()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim codigo_cup As String
            codigo_cup = IIf(String.IsNullOrEmpty(Session("gc_codigo_cup")), "-1", Session("gc_codigo_cup"))

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarUnidadAsignaturaDesarrollo", codigo_cup)
            obj.CerrarConexion()

            ViewState("dtDesarrollo") = dt
            Call BindGridDesarrollo()
            dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub gvDesarrollo_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvDesarrollo.EditIndex = e.NewEditIndex
        Call BindGridDesarrollo()
    End Sub

    Protected Sub OnUpdateDesarrollo(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

            Dim txtComentario As TextBox = CType(gvDesarrollo.Rows(row.RowIndex).FindControl("txtComentario"), TextBox)

            If Not String.IsNullOrEmpty(txtComentario.Text) Then
                dt.Rows(row.RowIndex)("comentario_uad") = txtComentario.Text
                ViewState("dtDesarrollo") = dt
                gvDesarrollo.EditIndex = -1
                Call BindGridDesarrollo()
            Else
                Call mt_ShowMessage("Ingrese un comentario para el desarrollo de la Unidad", MessageType.Warning, True)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning, True)
        End Try
    End Sub

    Protected Sub OnCancelDesarrollo(ByVal sender As Object, ByVal e As EventArgs)
        gvDesarrollo.EditIndex = -1
        Call BindGridDesarrollo()
    End Sub

    Protected Sub OnDeleteDesarrollo(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_uad As String = gvDesarrollo.DataKeys(row.RowIndex).Item("codigo_uad").ToString

        If Not String.IsNullOrEmpty(codigo_uad) And Not codigo_uad.Equals("0") Then
            Dim dtElim As Data.DataTable = TryCast(ViewState("dtDesarrolloElim"), Data.DataTable)
            dtElim.Rows.Add(codigo_uad)
            ViewState("dtDesarrolloElim") = dtElim
        End If

        dt.Rows(row.RowIndex)("comentario_uad") = ""
        ViewState("dtDesarrollo") = dt
        gvDesarrollo.EditIndex = -1
        Call BindGridDesarrollo()
    End Sub

#End Region

#Region "Dificultad"

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            obj.IniciarTransaccion()

            Dim dt As New Data.DataTable

            Dim dtDesarrollo As Data.DataTable = TryCast(ViewState("dtDesarrollo"), Data.DataTable)
            Dim dtDesarrolloElim As Data.DataTable = TryCast(ViewState("dtDesarrolloElim"), Data.DataTable)

            Dim codigo_cup As String
            codigo_cup = IIf(String.IsNullOrEmpty(Session("gc_codigo_cup")), "-1", Session("gc_codigo_cup"))

            For i As Integer = 0 To dtDesarrolloElim.Rows.Count - 1
                Dim codigo_uad As Object
                codigo_uad = dtDesarrolloElim.Rows(i).Item(0).ToString
                obj.Ejecutar("DEA_ActualizarUnidadAsignaturaDesarrollo", codigo_uad, "", cod_user)
            Next

            For i As Integer = 0 To dtDesarrollo.Rows.Count - 1
                Dim codigo_uad, codigo_uni, comentario As Object

                codigo_uni = dtDesarrollo.Rows(i).Item(1).ToString
                codigo_uad = dtDesarrollo.Rows(i).Item(2).ToString
                comentario = dtDesarrollo.Rows(i).Item(3).ToString

                If codigo_uad Is Nothing Or codigo_uad.Equals("0") Then
                    dt = obj.TraerDataTable("DEA_RegistrarUnidadAsignaturaDesarrollo", codigo_cup, codigo_uni, comentario, cod_user)

                    If Not CInt(dt.Rows(0).Item(0).ToString()) > 0 Then
                        obj.AbortarTransaccion()
                        Return
                        Exit For
                    End If
                Else
                    dt = obj.TraerDataTable("DEA_ActualizarUnidadAsignaturaDesarrollo", codigo_uad, comentario, cod_user)
                End If
            Next


            dt = New Data.DataTable
            Dim dtDificultad As Data.DataTable = TryCast(ViewState("dtDificultad"), Data.DataTable)
            Dim dtDificultadElim As Data.DataTable = TryCast(ViewState("dtDificultadElim"), Data.DataTable)

            For i As Integer = 0 To dtDificultadElim.Rows.Count - 1
                Dim codigo_udf As Object
                codigo_udf = dtDificultadElim.Rows(i).Item(0).ToString
                obj.Ejecutar("DEA_EliminarUnidadAsignaturaDificultad", codigo_udf, cod_user)
            Next

            For i As Integer = 0 To dtDificultad.Rows.Count - 1
                Dim codigo_udf, codigo_uae, comentario As Object

                codigo_udf = dtDificultad.Rows(i).Item(0).ToString
                codigo_uae = dtDificultad.Rows(i).Item(1).ToString
                comentario = dtDificultad.Rows(i).Item(3).ToString
                codigo_udf = IIf(String.IsNullOrEmpty(codigo_udf), DBNull.Value, codigo_udf)

                If codigo_udf Is Nothing Or codigo_udf.Equals("0") Then
                    dt = obj.TraerDataTable("DEA_RegistrarUnidadAsignaturaDificultad", codigo_uae, codigo_cup, comentario, cod_user)

                    If Not CInt(dt.Rows(0).Item(0).ToString()) > 0 Then
                        obj.AbortarTransaccion()
                        Return
                        Exit For
                    End If
                Else
                    If Not String.IsNullOrEmpty(codigo_uae) Then
                        dt = obj.TraerDataTable("DEA_ActualizarUnidadAsignaturaDificultad", codigo_udf, codigo_uae, comentario, cod_user)
                    End If
                End If
            Next

            dt = New Data.DataTable
            dt = obj.TraerDataTable("DEA_RegistrarUnidadAsignaturaSugerencia", codigo_cup, txtSugerencia.Text, cod_user)

            If Not CInt(dt.Rows(0).Item(0).ToString()) > 0 Then
                obj.AbortarTransaccion()
                Return
            End If

            obj.TerminarTransaccion()
            dt.Dispose()
            txtSugerencia.Text = ""

            Call mt_CargarDesarrollo()
            Call mt_CargarDificultad()
        Catch ex As Exception
            obj.AbortarTransaccion()
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub BindGridDificultad()
        Dim dt As Data.DataTable = TryCast(ViewState("dtDificultad"), Data.DataTable)

        If dt.Rows.Count > 0 Then
            gvDificultad.DataSource = dt
            gvDificultad.DataBind()
        Else
            dt.Rows.Add(dt.NewRow())
            gvDificultad.DataSource = dt
            gvDificultad.DataBind()

            gvDificultad.Rows(0).Cells.Clear()
        End If

        Me.udpDificultad.Update()

        Me.divAlertModal.Visible = False
        Me.lblMensaje.InnerText = ""
        updMensaje.Update()
    End Sub

    Private Sub mt_CargarDificultad()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dtSugerencia As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim codigo_cup As String
            codigo_cup = IIf(String.IsNullOrEmpty(Session("gc_codigo_cup")), "-1", Session("gc_codigo_cup"))

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarUnidadAsignaturaDificultad", codigo_cup)
            dtSugerencia = obj.TraerDataTable("DEA_ObtenerAsignaturaSugerencia", codigo_cup)
            obj.CerrarConexion()

            If dtSugerencia.Rows.Count > 0 Then
                Dim sugerencia As Object = dtSugerencia.Rows(0).Item(0)
                sugerencia = IIf(String.IsNullOrEmpty(sugerencia), "", sugerencia)
                txtSugerencia.Text = sugerencia
            End If

            ViewState("dtDificultad") = dt
            Call BindGridDificultad()
            dt.Dispose()
            dtSugerencia.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub gvDificultad_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvDificultad.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlElemento"), DropDownList)

            If Not ddl Is Nothing Then
                ddl.DataSource = fc_GetElementos()
                ddl.DataValueField = "codigo_uae"
                ddl.DataTextField = "descripcion_uae"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione tipo --]", "-1"))
                ddl.SelectedValue = gvDificultad.DataKeys(e.Row.RowIndex).Item("codigo_uae").ToString()
            End If
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            Dim ddl As DropDownList = CType(e.Row.FindControl("ddlNewElemento"), DropDownList)

            If ddl IsNot Nothing Then
                ddl.DataSource = fc_GetElementos()
                ddl.DataValueField = "codigo_uae"
                ddl.DataTextField = "descripcion_uae"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione tipo --]", "-1"))
            End If
        End If
    End Sub

    Protected Sub gvDificultad_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvDificultad.EditIndex = e.NewEditIndex
        Call BindGridDificultad()
    End Sub

    Protected Sub OnNewDificultad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dtDificultad"), Data.DataTable)

            Dim ddlElemento As DropDownList = CType(gvDificultad.FooterRow.FindControl("ddlNewElemento"), DropDownList)
            Dim txtComentario As TextBox = CType(gvDificultad.FooterRow.FindControl("txtNewComentario"), TextBox)

            If fc_ValidarDificultad(ddlElemento, txtComentario) Then
                If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
                    dt.Rows.RemoveAt(0)
                End If

                dt.Rows.Add(0, ddlElemento.SelectedValue, ddlElemento.SelectedItem.Text, txtComentario.Text)
                ViewState("dtDificultad") = dt
                gvDificultad.EditIndex = -1
                Call BindGridDificultad()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning, True)
        End Try
    End Sub

    Protected Sub OnUpdateDificultad(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dt As Data.DataTable = TryCast(ViewState("dtDificultad"), Data.DataTable)
            Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)

            Dim ddlElemento As DropDownList = CType(gvDificultad.Rows(row.RowIndex).FindControl("ddlElemento"), DropDownList)
            Dim txtComentario As TextBox = CType(gvDificultad.Rows(row.RowIndex).FindControl("txtComentario"), TextBox)

            If fc_ValidarDificultad(ddlElemento, txtComentario) Then
                dt.Rows(row.RowIndex)("codigo_uae") = ddlElemento.SelectedValue
                dt.Rows(row.RowIndex)("comentario_udf") = txtComentario.Text
                ViewState("dtDificultad") = dt
                gvDificultad.EditIndex = -1
                Call BindGridDificultad()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Warning, True)
        End Try
    End Sub

    Protected Sub OnCancelDificultad(ByVal sender As Object, ByVal e As EventArgs)
        gvDificultad.EditIndex = -1
        Call BindGridDificultad()
    End Sub

    Protected Sub OnDeleteDificultad(ByVal sender As Object, ByVal e As EventArgs)
        Dim dt As Data.DataTable = TryCast(ViewState("dtDificultad"), Data.DataTable)

        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_udf As String = gvDificultad.DataKeys(row.RowIndex).Item("codigo_udf").ToString

        If Not String.IsNullOrEmpty(codigo_udf) And Not codigo_udf.Equals("0") Then
            Dim dtElim As Data.DataTable = TryCast(ViewState("dtDificultadElim"), Data.DataTable)
            dtElim.Rows.Add(codigo_udf)
            ViewState("dtDificultadElim") = dtElim
        End If

        dt.Rows.RemoveAt(row.RowIndex)
        ViewState("dtDificultad") = dt
        gvDificultad.EditIndex = -1
        Call BindGridDificultad()
    End Sub

    Private Function fc_GetElementos() As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        obj.AbrirConexion()
        dt = obj.TraerDataTable("DEA_ListarUnidadAsignaturaElemento")
        obj.CerrarConexion()

        Return dt
    End Function

    Private Function fc_ValidarDificultad(ByVal ddlElemento As DropDownList, ByVal txtComentario As TextBox) As Boolean
        Dim isOk As Boolean = False

        If ddlElemento.SelectedValue = "-1" Then
            Call mt_ShowMessage("Seleccione un Tipo de Elemento", MessageType.Warning, True)
        Else
            If String.IsNullOrEmpty(txtComentario.Text) Then
                Call mt_ShowMessage("Ingrese un comentario", MessageType.Warning, True)
            Else
                isOk = True
            End If
        End If

        Return isOk
    End Function

#End Region

#Region "Contenido"

    Private Sub mt_CargarContenido()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim dtSugerencia As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            uni_aux = ""
            tot_uni = 1

            Dim codigo_cac, codigo_cup, codigo_sgr As String
            codigo_cac = IIf(String.IsNullOrEmpty(Session("gc_codigo_cac")), "-1", Session("gc_codigo_cac"))
            codigo_cup = IIf(String.IsNullOrEmpty(Session("gc_codigo_cup")), "-1", Session("gc_codigo_cup"))
            codigo_sgr = IIf(String.IsNullOrEmpty(Session("gc_codigo_sgr")), "-1", Session("gc_codigo_sgr"))

            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_ListarContenidoDesarrollado", codigo_cac, codigo_cup, codigo_sgr)
            obj.CerrarConexion()

            gvContenido.DataSource = dt
            gvContenido.DataBind()

            Call mt_AgruparFilas(gvContenido.Rows, 0, 4)
            dt.Dispose()
            udpContenido.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " ").Replace(vbCr, " ").Replace(vbLf, ""), MessageType.Error, True)
        End Try
    End Sub

    Protected Sub OnCheck(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, CheckBox)).NamingContainer, GridViewRow)
        Dim chkRealizado As CheckBox = CType(gvContenido.Rows(row.RowIndex).FindControl("chkRealizado"), CheckBox)

        If chkRealizado.Enabled Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

            Dim codigo_cac, codigo_cup, codigo_sgr As String
            codigo_cac = IIf(String.IsNullOrEmpty(Session("gc_codigo_cac")), "-1", Session("gc_codigo_cac"))
            codigo_cup = IIf(String.IsNullOrEmpty(Session("gc_codigo_cup")), "-1", Session("gc_codigo_cup"))
            codigo_sgr = IIf(String.IsNullOrEmpty(Session("gc_codigo_sgr")), "-1", Session("gc_codigo_sgr"))

            Dim codigo_con As String = gvContenido.DataKeys(row.RowIndex).Item("codigo_con").ToString
            Dim codigo_cdes As String = gvContenido.DataKeys(row.RowIndex).Item("codigo_cdes").ToString
            Dim chk As Int16 = IIf(chkRealizado.Checked, 1, 0)

            Try
                obj.AbrirConexion()
                obj.Ejecutar("DEA_RegistrarContenidoDesarrollado", codigo_cdes, codigo_cup, codigo_con, chk, cod_user, codigo_sgr)
                Call mt_CargarContenido()
            Catch ex As Exception
                Call mt_ShowMessage(ex.Message, MessageType.Info, True)
            Finally
                obj.CerrarConexion()
            End Try
        End If
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
                    Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

#End Region

End Class
