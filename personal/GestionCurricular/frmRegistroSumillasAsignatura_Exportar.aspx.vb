﻿
Partial Class GestionCurricular_frmRegistroSumillasAsignatura_Exportar
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer

    Dim seleccion As String
    Dim flag As Boolean = False
    Dim dtX As New Data.DataTable


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
            cod_user = Session("id_per") 'Request.QueryString("id")

            If Not String.IsNullOrEmpty(Session("cod_ctf")) Then
                cod_ctf = Session("cod_ctf")
            Else
                cod_ctf = Request.QueryString("ctf")
            End If

            'If IsPostBack = False Then
            '    Session("gc_dtCursos") = Nothing
            '    Session("gc_dtAsignatura") = Nothing
            '    mt_CargarCarreraProfesional()
            '    Me.txtBuscar.Visible = False
            '    Me.btnBuscar.Visible = False
            '    Me.btnExportar.Visible = False
            '    Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
            '    'Else
            '    '    mt_RefreshGrid()
            'End If

            Me.gvAsignatura.DataSource = CType(Session("gc_dtAsignatura"), Data.DataTable)
            Me.gvAsignatura.DataBind()

            mt_ExportToExcel()

        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub cboCarProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarProf.SelectedIndexChanged
    '    Try
    '        If Me.cboCarProf.SelectedValue <> -2 Then
    '            divPlanCurr.Visible = True
    '            divPlanEst.Visible = True

    '            If Me.cboCarProf.SelectedValue = -1 Then mt_ShowMessage("¡ Seleccione una Carrera profesional !", MessageType.Warning) : Me.cboCarProf.Focus()

    '            Call mt_CargarPlanCurricular(Me.cboCarProf.SelectedValue)
    '            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarDatos(0, Me.cboEstado.SelectedValue)
    '            If Me.cboPlanEst.Items.Count > 0 Then Me.cboPlanEst.SelectedIndex = 0
    '            Call mt_CargarPlanEstudio(Me.cboPlanCurr.SelectedValue)
    '        Else
    '            divPlanCurr.Visible = False
    '            divPlanEst.Visible = False

    '            Call mt_CargarDatos(-2, Me.cboEstado.SelectedValue)
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboPlanCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanCurr.SelectedIndexChanged
    '    Try
    '        mt_CargarPlanEstudio(Me.cboPlanCurr.SelectedValue)
    '        If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarDatos(0, Me.cboEstado.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Page.RegisterStartupScript("Pop", "<script>openModal('" & "Editar" & "');</script>")
    '    'ScriptManager.RegisterStartupScript(Me.Page, Me.panGrid.GetType, "Pop", "<script>openModal('" & "Editar" & "');</script>", False)
    '    'Me.panGrid.Update()
    'End Sub

    'Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
    '    'AllowPaging="True" PageSize="10"
    '    '<FooterStyle Font-Bold="True" ForeColor="White" />
    '    '<PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
    '    'mt_CargarDatos(Me.cboPlanEst.SelectedValue)
    '    Me.gvAsignatura.DataSource = CType(Session("gc_dtAsignatura"), Data.DataTable)
    '    Me.gvAsignatura.DataBind()
    '    Me.gvAsignatura.PageIndex = e.NewPageIndex
    '    Me.gvAsignatura.DataBind()
    'End Sub

    'Protected Sub gvAsignaturaGen_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignaturaGen.PageIndexChanging
    '    Try
    '        Me.gvAsignaturaGen.DataSource = CType(Session("gc_dtCursos"), Data.DataTable)
    '        Me.gvAsignaturaGen.DataBind()
    '        Me.gvAsignaturaGen.PageIndex = e.NewPageIndex
    '        Me.gvAsignaturaGen.DataBind()
    '        Me.panModal.Update()
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
    '    Dim obj As New ClsConectarDatos
    '    Dim dtEsp, dtGen As Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        Dim index As Integer
    '        index = CInt(e.CommandArgument)
    '        If e.CommandName = "Editar" Then
    '            Session("gc_codigo_sum") = Me.gvAsignatura.DataKeys(index).Values("codigo_sum")
    '            Session("gc_codigo_pes") = Me.gvAsignatura.DataKeys(index).Values("codigo_pes")
    '            Session("gc_codigo_cur") = Me.gvAsignatura.DataKeys(index).Values("codigo_Cur")
    '            Me.hdselec.Value = ""
    '            Me.txtSumilla.Text = Me.gvAsignatura.DataKeys(index).Values("descripcion_sum")
    '            Me.txtCompetencia.Text = Me.gvAsignatura.DataKeys(index).Values("competencia_sum")
    '            'codigo_cur = CInt(Me.gvAsignatura.DataKeys(index).Values("codigo_Cur"))
    '            'Me.txtSumilla.Text = CStr(Me.cboCarProf.SelectedValue) + ", " + CStr(Me.cboCarProf.SelectedValue) + ", " + CStr(codigo_cur)
    '            obj.AbrirConexion()
    '            dtEsp = obj.TraerDataTable("PlanCursoSumilla_Listar", "TE", Me.cboCarProf.SelectedValue, Session("gc_codigo_pes"), Session("gc_codigo_cur"))
    '            obj.CerrarConexion()
    '            Me.gvAsignaturaEsp.DataSource = dtEsp
    '            Me.gvAsignaturaEsp.DataBind()
    '            Me.gvAsignaturaEsp.Caption = "<label>Actualizar en planes de la misma carrera</label>"
    '            'Me.gvAsignaturaGen.DataSource = dtGen
    '            'Me.gvAsignaturaGen.DataBind()
    '            'Me.gvAsignaturaGen.Caption = "<label>Actualizar en otras Carreras Profesionales</label>"
    '            mt_CargarAsignaturasGen(Me.cboCarProf.SelectedValue, Session("gc_codigo_pes"), Session("gc_codigo_cur"))
    '            flag = True
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
    '    Dim obj As New ClsConectarDatos
    '    Dim msj As String = ""
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        If String.IsNullOrEmpty(Session("gc_codigo_sum")) And Session("gc_codigo_sum") <> "-1" Then
    '            Dim codigo_sum As Integer
    '            codigo_sum = CInt(Session("gc_codigo_sum"))
    '            obj.AbrirConexion()
    '            obj.Ejecutar("PlanCursoSumilla_actualizar", codigo_sum, Session("gc_codigo_pes"), Session("gc_codigo_cur"), Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text.Trim, 1, cod_user)
    '            obj.CerrarConexion()
    '            msj = "¡ Los datos han sido actualizados correctamente !"
    '        Else
    '            obj.AbrirConexion()
    '            obj.Ejecutar("PlanCursoSumilla_insertar", Session("gc_codigo_pes"), Session("gc_codigo_cur"), Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text.Trim, cod_user)
    '            obj.CerrarConexion()
    '            msj = "¡ Los datos han sido registrados correctamente !"
    '        End If

    '        Dim x, y, codigo_pes, codigo_cur, cont, cod_aux As Integer
    '        Dim chk As CheckBox
    '        For y = 0 To Me.gvAsignaturaEsp.Rows.Count - 1
    '            chk = Me.gvAsignaturaEsp.Rows(y).FindControl("chkSelect")
    '            If chk.Checked Then
    '                cont = +1
    '            End If
    '        Next
    '        If cont > 0 Then
    '            obj.IniciarTransaccion()
    '            For x = 0 To Me.gvAsignaturaEsp.Rows.Count - 1
    '                chk = Me.gvAsignaturaEsp.Rows(x).FindControl("chkSelect")
    '                codigo_pes = Me.gvAsignaturaEsp.DataKeys(x).Values("codigo_pes")
    '                codigo_cur = Me.gvAsignaturaEsp.DataKeys(x).Values("codigo_Cur")
    '                cod_aux = Me.gvAsignaturaEsp.DataKeys(x).Values("codigo_sum")
    '                If chk.Checked Then
    '                    If cod_aux = -1 Then
    '                        obj.Ejecutar("PlanCursoSumilla_insertar", codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, cod_user)
    '                    Else
    '                        obj.Ejecutar("PlanCursoSumilla_actualizar", cod_aux, codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, 1, cod_user)
    '                    End If
    '                Else
    '                    If cod_aux <> -1 Then
    '                        obj.Ejecutar("PlanCursoSumilla_actualizar", cod_aux, codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, 0, cod_user)
    '                    End If
    '                End If
    '            Next
    '            obj.TerminarTransaccion()
    '        End If
    '        cont = 0
    '        For y = 0 To Me.gvAsignaturaGen.Rows.Count - 1
    '            chk = Me.gvAsignaturaGen.Rows(y).FindControl("chkSelect")
    '            If chk.Checked Then
    '                cont = +1
    '            End If
    '        Next
    '        If cont > 0 Then
    '            obj.IniciarTransaccion()
    '            For x = 0 To Me.gvAsignaturaGen.Rows.Count - 1
    '                chk = Me.gvAsignaturaGen.Rows(x).FindControl("chkSelect")
    '                codigo_pes = Me.gvAsignaturaGen.DataKeys(x).Values("codigo_pes")
    '                codigo_cur = Me.gvAsignaturaGen.DataKeys(x).Values("codigo_Cur")
    '                cod_aux = Me.gvAsignaturaGen.DataKeys(x).Values("codigo_sum")
    '                If chk.Checked Then
    '                    If cod_aux = -1 Then
    '                        obj.Ejecutar("PlanCursoSumilla_insertar", codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, cod_user)
    '                    Else
    '                        obj.Ejecutar("PlanCursoSumilla_actualizar", cod_aux, codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, 1, cod_user)
    '                    End If
    '                Else
    '                    If cod_aux <> -1 Then
    '                        obj.Ejecutar("PlanCursoSumilla_actualizar", cod_aux, codigo_pes, codigo_cur, Me.txtSumilla.Text.Trim, Me.txtCompetencia.Text, 0, cod_user)
    '                    End If
    '                End If
    '            Next
    '            obj.TerminarTransaccion()
    '        End If
    '        mt_ShowMessage(msj, MessageType.Success)
    '        mt_CargarDatos(Me.cboPlanEst.SelectedValue, Me.cboEstado.SelectedValue)
    '    Catch ex As Exception
    '        obj.AbortarTransaccion()
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim x, y, codigo_pes As Integer
    '    Dim chk As CheckBox
    '    Dim dt As New Data.DataTable
    '    Me.hdselec.Value = ""
    '    dt = CType(Session("gc_dtCursos"), Data.DataTable)
    '    For x = 0 To Me.gvAsignaturaGen.Rows.Count - 1
    '        chk = Me.gvAsignaturaGen.Rows(x).FindControl("chkSelect")
    '        codigo_pes = Me.gvAsignaturaGen.DataKeys(x).Values("codigo_pes")
    '        For y = 0 To dt.Rows.Count - 1
    '            'Me.gvAsignaturaGen.Caption = "<label>" + dt.Rows(y).Item(0).ToString + " " + dt.Rows(y).Item(4).ToString + " " + dt.Rows(y).Item(5).ToString + " " + dt.Rows(y).Item(6).ToString + "</label>"
    '            If codigo_pes = CInt(dt.Rows(y).Item(0).ToString) Then
    '                dt.Rows(y).Item(6) = IIf(chk.Checked, 1, 0)
    '                'Me.gvAsignaturaGen.Caption = "<label>" + dt.Rows(y).Item(0).ToString + "</label>"
    '                Exit For
    '            End If
    '        Next
    '    Next
    '    Session("gc_dtCursos") = dt
    '    Me.panModal.Update()
    'End Sub

    'Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
    '    Try
    '        Dim dt, dtAux As New Data.DataTable
    '        Dim dv As Data.DataView
    '        Dim strBuscar As String = ""
    '        If Me.txtBuscar.Text.Trim <> "" Then
    '            strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
    '            dv = New Data.DataView(CType(Session("gc_dtAsignatura"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
    '            dt = dv.ToTable
    '        Else
    '            dt = CType(Session("gc_dtAsignatura"), Data.DataTable)
    '        End If
    '        Me.gvAsignatura.DataSource = dt
    '        Me.gvAsignatura.DataBind()
    '        Me.txtBuscar.Focus()
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboPlanEst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEst.SelectedIndexChanged
    '    Try
    '        mt_CargarDatos(IIf(Me.cboPlanEst.SelectedValue = -1, 0, Me.cboPlanEst.SelectedValue), Me.cboEstado.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
    '    Try
    '        If Me.cboCarProf.SelectedValue <> -2 Then
    '            If Me.cboCarProf.SelectedValue = -1 Then mt_ShowMessage("¡ Seleccione una Carrera profesional !", MessageType.Warning) : Me.cboCarProf.Focus()
    '            mt_CargarDatos(IIf(Me.cboPlanEst.SelectedValue = -1, 0, Me.cboPlanEst.SelectedValue), Me.cboEstado.SelectedValue)
    '        Else
    '            Call mt_CargarDatos(-2, Me.cboEstado.SelectedValue)
    '        End If
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
    '    Try
    '        Response.Redirect("frmRegistroSumillasAsignatura_Exportar.aspx")
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub


#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    'Private Sub mt_CargarCarreraProfesional()
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        'cod_user = 3133
    '        If cod_ctf = 1 Or cod_ctf = 232 Then
    '            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
    '        Else
    '            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
    '        End If
    '        obj.CerrarConexion()
    '        mt_CargarCombo(Me.cboCarProf, dt, "codigo_Cpf", "nombre_Cpf")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarPlanCurricular(ByVal codigo_cpf As String)
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("COM_ListarPlanCurricular", codigo_cpf, -1)
    '        obj.CerrarConexion()
    '        mt_CargarCombo(Me.cboPlanCurr, dt, "codigo_pcur", "nombre_pcur")
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
    '        dt = obj.TraerDataTable("ConsultarPlanEstudio", "PC", 2, codigo_pcur)
    '        obj.CerrarConexion()
    '        mt_CargarCombo(Me.cboPlanEst, dt, "codigo_Pes", "descripcion_Pes")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
    '    cbo.DataSource = dt
    '    cbo.DataTextField = datatext
    '    cbo.DataValueField = datavalue
    '    cbo.DataBind()
    'End Sub

    'Private Sub mt_CargarDatos(ByVal codigo_pes As Integer, ByVal cod_estado As Integer)
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("PlanCursoSumilla_Listar", "PX", cod_estado, codigo_pes, -1, cod_ctf)
    '        obj.CerrarConexion()
    '        'Throw New Exception(codigo_pes)
    '        Session("gc_dtAsignatura") = dt
    '        Me.gvAsignatura.DataSource = dt 'CType(Session("gc_dtAsignatura"), Data.DataTable)
    '        Me.gvAsignatura.DataBind()
    '        Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
    '        Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
    '        Me.btnExportar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_CargarAsignaturasGen(ByVal codigo_cpf As Integer, ByVal codigo_pes As Integer, ByVal codigo_cur As Integer)
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("PlanCursoSumilla_Listar", "TG", codigo_cpf, codigo_pes, codigo_cur)
    '        'mt_ShowMessage("codigo_cpf: " & codigo_cpf & " codigo_pes: " & codigo_pes & " codigo_cur: " & codigo_cur, MessageType.Warning)
    '        obj.CerrarConexion()
    '        Session("gc_dtCursos") = dt
    '        Me.gvAsignaturaGen.DataSource = CType(Session("gc_dtCursos"), Data.DataTable)
    '        Me.gvAsignaturaGen.DataBind()
    '        Me.gvAsignaturaGen.Caption = "<label>Actualizar en planes de otras carreras</label>"
    '        Me.panModal.Update()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Sub mt_SeleccionarPlanes(ByVal lista As String)
    '    Dim i, j, codigo_pes As Integer
    '    Dim codigo As String()
    '    Dim chk As CheckBox
    '    Try
    '        'lista = Session("gc_Cursos")
    '        If lista.Length > 0 Then
    '            lista = lista.TrimEnd(",")
    '            codigo = lista.Split(",")
    '            For i = 0 To codigo.Length - 1
    '                For j = 0 To Me.gvAsignaturaGen.Rows.Count - 1
    '                    codigo_pes = Me.gvAsignaturaGen.DataKeys(j).Values("codigo_pes")
    '                    Response.Write(codigo(i) + " " + CStr(codigo_pes) + Environment.NewLine)
    '                    If CInt(codigo(i)) = codigo_pes Then
    '                        chk = Me.gvAsignaturaGen.Rows(j).FindControl("chkSelect")
    '                        chk.Checked = True
    '                        Exit For
    '                    End If
    '                Next
    '            Next
    '        End If
    '        Me.panModal.Update()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Private Sub mt_ExportToExcel()
        Dim attachment As String = "attachment; filename=sumillas.xls"
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As io.StringWriter = New io.StringWriter()
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Me.gvAsignatura.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

#End Region

End Class
