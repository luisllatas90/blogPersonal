
Partial Class GestionCurricular_frmAutorizarEdicionNota
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
            cod_user = Session("id_per")
            'cod_user = IIf(cod_user = 684, -1, cod_user) 
            'cod_user = 648
            If Not IsPostBack Then
                mt_CargarSemestre()
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                'Me.txtObservacion.Attributes.Add("nro_est", 0)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            If Me.cboSemestre.SelectedValue = -1 Then
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                mt_CargarCarreraProfesional(0, 2)
                Me.cboSemestre.Focus()
                Exit Sub
            End If
            mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, 2)
            mt_CargarEstudiantes(0, 0)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            If Me.cboCarrProf.SelectedValue = -1 Then
                mt_ShowMessage("¡ Seleccione Carrera Profesional !", MessageType.Warning)
                mt_CargarDatos(0, 0, 0)
                Me.cboCarrProf.Focus()
                Exit Sub
            End If
            mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, -1)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvAsignatura_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAsignatura.PageIndexChanging
        Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
        Me.gvAsignatura.DataBind()
        Me.gvAsignatura.PageIndex = e.NewPageIndex
        Me.gvAsignatura.DataBind()
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim dt, dtAux As New Data.DataTable
            Dim dv As Data.DataView
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

    Protected Sub gvAsignatura_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAsignatura.RowCommand
        Dim _codigo_cup As Integer
        Try
            Dim index As Integer
            index = CInt(e.CommandArgument)
            _codigo_cup = Me.gvAsignatura.DataKeys(index).Values("codigo_cup")
            Session("gc_codigo_cup") = _codigo_cup
            'Session("gc_nro_est") = 0
            Me.hdnro_est.Value = 0
            If e.CommandName = "Solicitar" Then
                mt_CargarCortes(Session("gc_codigo_cup"))
                mt_CargarEvaluaciones(0)
                mt_CargarEstudiantes(0, 0)
                Me.txtObservacion.Text = String.Empty
                'Me.txtObservacion.Attributes.Add("tag", 0)
                Page.RegisterStartupScript("Pop", "<script>openModal();</script>")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCorte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCorte.SelectedIndexChanged
        Try
            mt_CargarEvaluaciones(IIf(Me.cboCorte.SelectedValue = -1, 0, Me.cboCorte.SelectedValue))
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboEvaluacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEvaluacion.SelectedIndexChanged
        Try
            mt_CargarEstudiantes(IIf(Me.cboEvaluacion.SelectedValue = -1, 0, Me.cboEvaluacion.SelectedValue), Session("gc_codigo_cup"))
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim x, y, _codigo_nop, _nro_est As Integer
        Dim chk As CheckBox
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtEstudiante"), Data.DataTable)
        _nro_est = 0
        For x = 0 To Me.gvEstudiantes.Rows.Count - 1
            chk = Me.gvEstudiantes.Rows(x).FindControl("chkSelect")
            If chk.Checked Then _nro_est += 1
            _codigo_nop = Me.gvEstudiantes.DataKeys(x).Values("codigo_nop")
            'If chk.Checked Then
            '    Me.gvAsignatura.Caption = "<label>" & codigo_cur & " " & dt.Rows(0).Item(2).ToString + " " & dt.Rows(0).Item(4).ToString & " " & dt.Rows(0).Item(5).ToString & " " & dt.Rows(0).Item(6).ToString & "</label>"
            'End If
            For y = 0 To dt.Rows.Count - 1
                'Me.gvAsignatura.Caption = "<label>" + dt.Rows(y).Item(0).ToString + " " + dt.Rows(y).Item(4).ToString + " " + dt.Rows(y).Item(5).ToString + " " + dt.Rows(y).Item(6).ToString + "</label>"
                If _codigo_nop = CInt(dt.Rows(y).Item("codigo_nop").ToString) Then
                    dt.Rows(y).Item("selec") = IIf(chk.Checked, 1, 0)
                    'Me.gvAsignatura.Caption = "<label>" & codigo_cur & " " & dt.Rows(y).Item(1).ToString & "</label>"
                    'If chk.Checked Then _nro_est += 1
                    Exit For
                End If
            Next
        Next
        Session("gc_dtEstudiante") = dt
        'Session("gc_nro_est") = _nro_est
        'Me.txtObservacion.Attributes.Add("tag", _nro_est)
        Me.panModal.Update()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim obj As New ClsConectarDatos
        Dim chk As CheckBox
        Dim _codigo_dma, _codigo_eva As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.IniciarTransaccion()
            For x As Integer = 0 To Me.gvEstudiantes.Rows.Count - 1
                chk = Me.gvEstudiantes.Rows(x).FindControl("chkSelect")
                _codigo_dma = CInt(Me.gvEstudiantes.DataKeys(x).Values("codigo_Dma"))
                _codigo_eva = CInt(Me.gvEstudiantes.DataKeys(x).Values("codigo_eva"))
                If chk.Checked Then
                    obj.Ejecutar("DEA_EvaluacionCurso_Edicion_insertar", _codigo_dma, _codigo_eva, Me.txtObservacion.Text.Trim, 0, cod_user)
                End If
            Next
            obj.TerminarTransaccion()
            mt_ShowMessage("¡ Se registro la autorizacion correctamente !", MessageType.Success)
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

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

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV3", "PS", codigo_cac, codigo_test, cod_user)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar", "CC", -1, codigo_cpf, user, codigo_cac)
            obj.CerrarConexion()
            Session("gc_dtCursoProg") = dt
            Me.gvAsignatura.DataSource = CType(Session("gc_dtCursoProg"), Data.DataTable)
            Me.gvAsignatura.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCortes(ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "CC", -1, -1, codigo_cup)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCorte, dt, "codigo_coa", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarEvaluaciones(ByVal codigo_coa As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_CortesCurso_listar", "DC", codigo_coa, -1, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboEvaluacion, dt, "codigo_eva", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarEstudiantes(ByVal codigo_eva As Integer, ByVal codigo_cup As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("DEA_NotasParciales_listar", "DC", -1, codigo_eva, "", codigo_cup)
            obj.CerrarConexion()
            Session("gc_dtEstudiante") = dt
            Me.gvEstudiantes.DataSource = CType(Session("gc_dtEstudiante"), Data.DataTable)
            Me.gvEstudiantes.DataBind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
