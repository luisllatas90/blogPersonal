﻿
Partial Class GestionCurricular_frmAlineacionCompetenciasAsignatura
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Dim cod_user As Integer
    Dim cod_ctf As Integer

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
            cod_ctf = Request.QueryString("ctf")
            If IsPostBack = False Then
                Session("gc_dtAsignatura02") = Nothing
                mt_CargarCarreraProfesional()
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Me.lblCompetencia.Visible = False
                Me.cboCompetencias.Visible = False
                Me.btnAgregar.Visible = False
                Me.btnQuitar.Visible = False
                Me.chkVerCursos.Visible = False
                Me.lblBuscar.Visible = False
                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarProf.SelectedIndexChanged
        Try
            mt_CargarPlanCurricular(Me.cboCarProf.SelectedValue)
            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarCurso(0)
            If Me.gvPerfilEgresoCurso.Rows.Count > 0 Then mt_CargarCompetenciaCurso(0, 0)
            If Me.cboPlanEst.Items.Count > 0 Then Me.cboPlanEst.SelectedIndex = 0
            If Me.cboCarProf.SelectedValue <> -1 Then cboPlanCurr_SelectedIndexChanged(Nothing, Nothing)
            If Me.cboCarProf.SelectedValue = -1 Then
                Me.chkVerCursos.Visible = False : Me.lblBuscar.Visible = False
                Me.chkVerCursos.Checked = False
                mt_ShowMessage("¡ Seleccione una Carrera profesional !", MessageType.Warning)
                Me.cboCarProf.Focus()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboPlanCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanCurr.SelectedIndexChanged
        Try
            mt_CargarPlanEstudio(Me.cboPlanCurr.SelectedValue)
            mt_CargarCompetencias(Me.cboPlanCurr.SelectedValue)
            If Me.gvAsignatura.Rows.Count > 0 Then mt_CargarCurso(0)
            If Me.gvPerfilEgresoCurso.Rows.Count > 0 Then mt_CargarCompetenciaCurso(0, 0)
            Me.chkVerCursos.Visible = False : Me.lblBuscar.Visible = False
            Me.chkVerCursos.Checked = False
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCompetencias_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompetencias.SelectedIndexChanged
        Try
            mt_CargarCompetenciaCurso(cboPlanCurr.SelectedValue, IIf(cboCompetencias.SelectedValue = -1, 0, cboCompetencias.SelectedValue))
            Me.chkVerCursos.Visible = IIf(cboCompetencias.SelectedValue = -1, False, True)
            Me.lblBuscar.Visible = IIf(cboCompetencias.SelectedValue = -1, False, True)
            Me.chkVerCursos.Checked = False
            mt_CargarCurso(Me.cboPlanEst.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim obj As New ClsConectarDatos
        Dim x, y, cont As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            cont = 0
            If Me.gvAsignatura.Rows.Count = 0 Then
                Page.RegisterStartupScript("alert", "<script>alert('¡ No hay Asignaturas disponibles !');</script>")
                Exit Sub
            End If
            For y = 0 To Me.gvAsignatura.Rows.Count - 1
                Dim chk As CheckBox = Me.gvAsignatura.Rows(y).FindControl("chkSelect")
                If chk.Checked Then
                    cont = cont + 1
                End If
            Next
            If cont = 0 Then
                Page.RegisterStartupScript("alert", "<script>alert('¡ Seleccione al menos una Asignatura para realizar esta operación !');</script>")
                Exit Sub
            End If
            obj.IniciarTransaccion()
            For x = 0 To Me.gvAsignatura.Rows.Count - 1
                Dim chk As CheckBox = Me.gvAsignatura.Rows(x).FindControl("chkSelect")
                If chk.Checked Then
                    Dim codigo_pes, codigo_cur As Integer
                    codigo_pes = CInt(Me.gvAsignatura.DataKeys(x).Values("codigo_pes"))
                    codigo_cur = CInt(Me.gvAsignatura.DataKeys(x).Values("codigo_Cur"))
                    obj.Ejecutar("PerfilEgresoCurso_insertar", Me.cboCompetencias.SelectedValue, codigo_pes, codigo_cur, 1, 0, 1, cod_user)
                    'mt_ShowMessage(Me.cboCompetencias.SelectedValue & " - " & codigo_pes & " - " & codigo_cur, MessageType.Success)
                End If
            Next
            obj.TerminarTransaccion()
            mt_ShowMessage("¡ Se han asignado " & cont & " asignatura(s) a la competencia " & Me.cboCompetencias.SelectedItem.Text & " !", MessageType.Success)
            mt_CargarCompetenciaCurso(Me.cboPlanCurr.SelectedValue, Me.cboCompetencias.SelectedValue)
            mt_CargarCurso(Me.cboPlanEst.SelectedValue)
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitar.Click
        Dim obj As New ClsConectarDatos
        Dim x, y, cont As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            For y = 0 To Me.gvPerfilEgresoCurso.Rows.Count - 1
                Dim chk As CheckBox = Me.gvPerfilEgresoCurso.Rows(y).FindControl("chkSelect")
                If chk.Checked Then
                    cont = +1
                End If
            Next
            If cont = 0 Then
                Page.RegisterStartupScript("alert", "<script>alert('¡ Seleccione al menos una Asignatura para realizar esta operación !');</script>")
                Exit Sub
            End If
            obj.IniciarTransaccion()
            For x = 0 To Me.gvPerfilEgresoCurso.Rows.Count - 1
                Dim chk As CheckBox = Me.gvPerfilEgresoCurso.Rows(x).FindControl("chkSelect")
                If chk.Checked Then
                    Dim codigo_pEgrCur As Integer
                    codigo_pEgrCur = CInt(Me.gvPerfilEgresoCurso.DataKeys(x).Values("codigo_pEgrCur"))
                    obj.Ejecutar("PerfilEgresoCurso_actualizar", codigo_pEgrCur, Me.cboCompetencias.SelectedValue, 0, 0, cod_user)
                End If
            Next
            obj.TerminarTransaccion()
            mt_ShowMessage("¡ Se han quitado " & cont & " asignatura(s) de la competencia " & Me.cboCompetencias.SelectedItem.Text & " !", MessageType.Success)
            mt_CargarCompetenciaCurso(Me.cboPlanCurr.SelectedValue, Me.cboCompetencias.SelectedValue)
            mt_CargarCurso(Me.cboPlanEst.SelectedValue)
        Catch ex As Exception
            obj.AbortarTransaccion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub rdClave_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos
        Dim nombre_Cur As String
        Dim x, codigo_pEgrCur As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            mt_DesmarcarRadioButton()
            Dim rb As RadioButton = sender
            rb.Checked = True
            For x = 0 To Me.gvPerfilEgresoCurso.Rows.Count - 1
                Dim chk As RadioButton = Me.gvPerfilEgresoCurso.Rows(x).FindControl("rdClave")
                If chk.Checked Then
                    codigo_pEgrCur = CInt(Me.gvPerfilEgresoCurso.DataKeys(x).Values("codigo_pEgrCur"))
                    nombre_Cur = Me.gvPerfilEgresoCurso.DataKeys(x).Values("nombre_Cur")
                    obj.AbrirConexion()
                    obj.Ejecutar("PerfilEgresoCurso_actualizar", codigo_pEgrCur, Me.cboCompetencias.SelectedValue, 1, 1, cod_user)
                    obj.CerrarConexion()
                    mt_ShowMessage("¡ La asignatura " & nombre_Cur & " se ha asignado como clave !", MessageType.Success)
                    mt_CargarCurso(Me.cboPlanEst.SelectedValue)
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim dt, dtAux As New Data.DataTable
            Dim dv As Data.DataView
            Dim strBuscar As String = ""
            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_dtAsignatura02"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_dtAsignatura02"), Data.DataTable)
            End If
            Me.gvAsignatura.DataSource = dt
            Me.gvAsignatura.DataBind()
            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim x, y, codigo_cur As Integer
        Dim chk As CheckBox
        Dim dt As New Data.DataTable
        dt = CType(Session("gc_dtAsignatura02"), Data.DataTable)
        For x = 0 To Me.gvAsignatura.Rows.Count - 1
            chk = Me.gvAsignatura.Rows(x).FindControl("chkSelect")
            codigo_cur = Me.gvAsignatura.DataKeys(x).Values("codigo_Cur")
            'If chk.Checked Then
            '    Me.gvAsignatura.Caption = "<label>" & codigo_cur & " " & dt.Rows(0).Item(2).ToString + " " & dt.Rows(0).Item(4).ToString & " " & dt.Rows(0).Item(5).ToString & " " & dt.Rows(0).Item(6).ToString & "</label>"
            'End If
            For y = 0 To dt.Rows.Count - 1
                'Me.gvAsignatura.Caption = "<label>" + dt.Rows(y).Item(0).ToString + " " + dt.Rows(y).Item(4).ToString + " " + dt.Rows(y).Item(5).ToString + " " + dt.Rows(y).Item(6).ToString + "</label>"
                If codigo_cur = CInt(dt.Rows(y).Item(2).ToString) Then
                    dt.Rows(y).Item(6) = IIf(chk.Checked, 1, 0)
                    'Me.gvAsignatura.Caption = "<label>" & codigo_cur & " " & dt.Rows(y).Item(1).ToString & "</label>"
                    Exit For
                End If
            Next
        Next
        Session("gc_dtAsignatura02") = dt
        'Me.panModal.Update()
    End Sub

    Protected Sub cboPlanEst_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanEst.SelectedIndexChanged
        Try
            mt_CargarCurso(Me.cboPlanEst.SelectedValue)
            mt_CargarCompetencias(Me.cboPlanCurr.SelectedValue)
            If Me.gvPerfilEgresoCurso.Rows.Count > 0 Then mt_CargarCompetenciaCurso(0, 0)
            Me.chkVerCursos.Visible = False : Me.lblBuscar.Visible = False
            Me.chkVerCursos.Checked = False
            Me.lblCompetencia.Visible = IIf(Me.cboPlanEst.SelectedValue = -1, False, True)
            Me.cboCompetencias.Visible = IIf(Me.cboPlanEst.SelectedValue = -1, False, True)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ClsConectarDatos
        Dim codigo_pEgrCur As Integer
        Dim nombre_Cur, nombre_niv As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            Dim cbo As DropDownList = sender
            Dim row As GridViewRow = CType(cbo.Parent.Parent, GridViewRow)
            codigo_pEgrCur = CInt(Me.gvPerfilEgresoCurso.DataKeys(row.RowIndex).Values("codigo_pEgrCur"))
            nombre_Cur = Me.gvPerfilEgresoCurso.DataKeys(row.RowIndex).Values("nombre_Cur")
            nombre_niv = cbo.SelectedItem.Text
            obj.AbrirConexion()
            obj.Ejecutar("PerfilEgresoCurso_actualizar2", codigo_pEgrCur, cbo.SelectedValue, cod_user)
            obj.CerrarConexion()
            mt_ShowMessage("¡ La asignatura " & nombre_Cur & " se ha cambiado de nivel a " & nombre_niv & " !", MessageType.Success)
            mt_CargarCurso(Me.cboPlanEst.SelectedValue)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkVerCursos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If chkVerCursos.Checked Then
                mt_CargarCurso(Me.cboPlanEst.SelectedValue, Me.cboCompetencias.SelectedValue)
            Else
                mt_CargarCurso(Me.cboPlanEst.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarCarreraProfesional()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'cod_user = 648
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            End If
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanCurricular(ByVal codigo_cpf As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarPlanCurricular", codigo_cpf, -1)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanCurr, dt, "codigo_pcur", "nombre_pcur")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarPlanEstudio(ByVal codigo_pcur As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarPlanEstudio", "PC", 2, codigo_pcur)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboPlanEst, dt, "codigo_Pes", "descripcion_Pes")
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

    Private Sub mt_CargarCurso(ByVal codigo_pes As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCurso", "PC", codigo_pes, -1)
            obj.CerrarConexion()
            Session("gc_dtAsignatura02") = dt
            Me.gvAsignatura.DataSource = CType(Session("gc_dtAsignatura02"), Data.DataTable)
            Me.gvAsignatura.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.txtBuscar.Text = String.Empty
            'Me.lblCompetencia.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            'Me.cboCompetencias.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.lblCompetencia.Visible = True
            Me.cboCompetencias.Visible = True
            Me.btnAgregar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCurso(ByVal codigo_pes As Integer, ByVal codigo_com As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCurso", "PX", codigo_pes, codigo_com)
            obj.CerrarConexion()
            Session("gc_dtAsignatura02") = dt
            Me.gvAsignatura.DataSource = CType(Session("gc_dtAsignatura02"), Data.DataTable)
            Me.gvAsignatura.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.txtBuscar.Text = String.Empty
            'Me.lblCompetencia.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            'Me.cboCompetencias.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
            Me.btnAgregar.Visible = IIf(Me.gvAsignatura.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCompetencias(ByVal codigo_pcur As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("PerfilEgreso_Listar", "PC", -1, -1, codigo_pcur, "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCompetencias, dt, "codigo_pEgr", "nombre_com_aux")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCompetenciaCurso(ByVal codigo_pcur As Integer, ByVal codigo_pEgr As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("PerfilEgresoCurso_Listar", -1, codigo_pEgr, Me.cboPlanEst.SelectedValue, -1, codigo_pcur)
            obj.CerrarConexion()
            Me.gvPerfilEgresoCurso.DataSource = dt
            Me.gvPerfilEgresoCurso.DataBind()
            Me.btnQuitar.Visible = IIf(Me.gvPerfilEgresoCurso.Rows.Count > 0, True, False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_DesmarcarRadioButton()
        Dim gvr As GridViewRow
        Dim i As Integer
        For Each gvr In gvPerfilEgresoCurso.Rows
            Dim rb As RadioButton
            rb = CType(gvPerfilEgresoCurso.Rows(i).FindControl("rdClave"), RadioButton)
            rb.Checked = False
            i += 1
        Next
    End Sub

#End Region

End Class
