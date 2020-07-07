﻿
Partial Class academico_frmCicloAcademicoTipoEstudio
    Inherits System.Web.UI.Page

#Region "Variables"

    Private C As ClsConectarDatos
    Private cod_user As Integer, cod_ctf As Integer
    Private oeCATest As e_CicloAcademico_TipoEstudio, odCATest As d_CicloAcademico_TipoEstudio
    Private oeSemestre As e_CicloAcademico, odSemestre As d_CicloAcademico

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If
            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")

            If Not IsPostBack Then
                mt_CargarSemestre()
                cbotipoestudio.enabled = False
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim _codigo_cac As Integer
            Dim dt As New System.Data.DataTable
            If fecinicioaca.text.trim = "" Or Not isdate(fecinicioaca.text.trim) Then mt_ShowMessage("¡ Ingrese Fecha Inicio Académico [dd/MM/yyyy] !", MessageType.Warning) : fecinicioaca.focus() : Exit Sub
            If fecfinaca.text.trim = "" Or Not isdate(fecfinaca.text.trim) Then mt_ShowMessage("¡ Ingrese Fecha Fin Académico [dd/MM/yyyy] !", MessageType.Warning) : fecfinaca.focus() : Exit Sub
            If CDate(fecinicioaca.text.trim) >= CDate(fecfinaca.text.trim) Then mt_ShowMessage("¡ La Fecha de Fin debe ser mayor a la Fecha de Inicio !", MessageType.Warning) : fecfinaca.focus() : Exit Sub
            If fecinicioadm.text.trim = "" Or Not isdate(fecinicioadm.text.trim) Then mt_ShowMessage("¡ Ingrese Fecha Inicio Administrativo [dd/MM/yyyy] !", MessageType.Warning) : fecinicioadm.focus() : Exit Sub
            If fecfinadm.text.trim = "" Or Not isdate(fecfinadm.text.trim) Then mt_ShowMessage("¡ Ingrese Fecha Fin Administrativo [dd/MM/yyyy] !", MessageType.Warning) : fecfinadm.focus() : Exit Sub
            If CDate(fecinicioadm.text.trim) >= CDate(fecfinadm.text.trim) Then mt_ShowMessage("¡ La Fecha de Fin debe ser mayor a la Fecha de Inicio !", MessageType.Warning) : fecfinadm.focus() : Exit Sub
            If String.IsNullOrEmpty(Session("dea_codigo_ctest")) Then
                If cbosemestre.selectedvalue = -1 Then
                    oeSemestre = New e_CicloAcademico : odSemestre = New d_CicloAcademico
                    If txtdescripcion.text.trim = "" Then mt_ShowMessage("¡ Ingrese Descripción !", MessageType.Warning) : txtdescripcion.focus() : Exit Sub
                    If txtdescripcion.text.trim.length < 6 Then mt_ShowMessage("¡ Ingrese una descripción correcta [yyyy-N] !", MessageType.Warning) : txtdescripcion.focus() : Exit Sub
                    Dim _stipo As String() = txtdescripcion.text.trim.ToUpper.split("-")
                    If (_stipo(1) <> "0" And _stipo(1) <> "I" And _stipo(1) <> "II") Then
                        mt_ShowMessage("¡ Ingrese una descripción correcta. Para ciclo de verano usar [0] y para ciclo regular [I, II] !", MessageType.Warning) : txtdescripcion.focus() : Exit Sub
                    End If
                    With oeSemestre
                        .descripcion_cac = txtdescripcion.text.trim.toupper() : .fechaIni_Cro = CDate(fecinicioaca.text.trim) : .fechaFin_Cro = CDate(fecfinaca.text.trim)
                        .fechaIniAdm_cro = CDate(fecinicioadm.text.trim) : .fechafinAdm_cro = CDate(fecfinadm.text.trim) : .tipocac = ""
                    End With
                    dt = odSemestre.fc_RegistrarCicloAcademico(oeSemestre)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0) = 1 Then
                            mt_ShowMessage(dt.Rows(0).Item(1), MessageType.Success)
                            mt_CargarSemestre()
                            mt_LimpiarControles()
                        Else
                            mt_ShowMessage(dt.Rows(0).Item(1), MessageType.Error)
                        End If
                    End If
                Else
                    _codigo_cac = cbosemestre.selectedvalue
                    If cbotipoestudio.selectedvalue = -1 Then mt_ShowMessage("¡ Seleccione Tipo de Estudio !", MessageType.Warning) : cbotipoestudio.focus() : Exit Sub
                    oeCATest = New e_CicloAcademico_TipoEstudio : odCATest = New d_CicloAcademico_TipoEstudio
                    With oeCATest
                        .codigo_cac = _codigo_cac : .codigo_test = cbotipoestudio.selectedvalue : .codigo_per = cod_user
                        .fechainicio_cte = CDate(fecinicioaca.text.trim) : .fechafin_cte = CDate(fecfinaca.text.trim)
                        .fecini_adm_cte = CDate(fecinicioadm.text.trim) : .fecfin_adm_cte = CDate(fecfinadm.text.trim)
                    End With
                    dt = odCATest.fc_RegistrarCicloAcademico_TipoEstudio(oeCATest)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0) = 1 Then
                            mt_ShowMessage(dt.Rows(0).Item(1), MessageType.Success)
                            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
                        Else
                            mt_ShowMessage(dt.Rows(0).Item(1), MessageType.Error)
                        End If
                    End If
                End If
            Else
                oeCATest = New e_CicloAcademico_TipoEstudio : odCATest = New d_CicloAcademico_TipoEstudio
                With oeCATest
                    .codigo_ctest = Session("dea_codigo_ctest") : .codigo_per = cod_user
                    .fechainicio_cte = CDate(fecinicioaca.text.trim) : .fechafin_cte = CDate(fecfinaca.text.trim)
                    .fecini_adm_cte = CDate(fecinicioadm.text.trim) : .fecfin_adm_cte = CDate(fecfinadm.text.trim)
                End With
                If odCATest.fc_ActualizarCicloAcademico_TipoEstudio(oeCATest) Then
                    mt_ShowMessage("¡ Se actualizó correctamente !", MessageType.Success)
                    'mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
                    Me.cbotipoestudio.enabled = True : Me.cboSemestre.enabled = True
                    cboSemestre_SelectedIndexChanged(Nothing, Nothing)
                    Session("dea_codigo_ctest") = ""
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            Dim dt As New System.Data.DataTable
            mt_CargarTipoEstudio(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
            txtdescripcion.visible = IIf(Me.cboSemestre.SelectedValue = -1, True, False)
            'fecinicioaca.enabled = IIf(Me.cboSemestre.SelectedValue = -1, True, False)
            'fecfinaca.enabled = IIf(Me.cboSemestre.SelectedValue = -1, True, False)
            'fecinicioadm.enabled = IIf(Me.cboSemestre.SelectedValue = -1, True, False)
            'fecfinadm.enabled = IIf(Me.cboSemestre.SelectedValue = -1, True, False)
            cbotipoestudio.enabled = IIf(Me.cboSemestre.SelectedValue = -1, False, True)
            If cbosemestre.selectedvalue = -1 Then
                mt_LimpiarControles()
                txtdescripcion.focus()
            Else
                oeSemestre = New e_CicloAcademico : odSemestre = New d_CicloAcademico
                With oeSemestre
                    .codigo_cac = cbosemestre.selectedvalue : .tipooperacion = "" : .tipocac = ""
                End With
                dt = odSemestre.fc_ListarCicloAcademico(oeSemestre)
                If dt.Rows.Count > 0 Then
                    fecinicioaca.text = dt.Rows(0).Item(3)
                    fecfinaca.text = dt.Rows(0).Item(4)
                    fecinicioadm.text = dt.Rows(0).Item(9)
                    fecfinadm.text = dt.Rows(0).Item(10)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCATest_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCATest.RowCommand
        Dim index As Integer, _codigo_ctest As Integer, _codigo_test As Integer
        Dim dt As New System.Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                _codigo_ctest = Me.gvcatest.datakeys(index).values("codigo_ctest")
                _codigo_test = Me.gvcatest.datakeys(index).values("codigo_test")
                oeCATest = New e_CicloAcademico_TipoEstudio : odCATest = New d_CicloAcademico_TipoEstudio
                If e.CommandName = "Habilitar" Then
                    With oeCATest
                        .tipo = "V" : .codigo_ctest = _codigo_ctest : .codigo_test = _codigo_test : .codigo_per = cod_user
                    End With
                    dt = odCATest.fc_HabilitarCicloAcademico_TipoEstudio(oeCATest)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0) = 1 Then
                            mt_ShowMessage("¡ Se habilitó la vigencia correctamente !", MessageType.Success)
                            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
                        Else
                            mt_ShowMessage("¡ Se ocurrió un error en el proceso !", MessageType.Warning)
                        End If
                    End If
                ElseIf e.CommandName = "Admision" Then
                    With oeCATest
                        .tipo = "A" : .codigo_ctest = _codigo_ctest : .codigo_test = _codigo_test : .codigo_per = cod_user
                    End With
                    dt = odCATest.fc_HabilitarCicloAcademico_TipoEstudio(oeCATest)
                    If dt.Rows.Count > 0 Then
                        If dt.Rows(0).Item(0) = 1 Then
                            mt_ShowMessage("¡ Se habilitó la admisión correctamente !", MessageType.Success)
                            mt_CargarDatos(IIf(Me.cboSemestre.SelectedValue = -1, 0, Me.cboSemestre.SelectedValue))
                        Else
                            mt_ShowMessage("¡ Se ocurrió un error en el proceso !", MessageType.Warning)
                        End If
                    End If
                ElseIf e.CommandName = "Editar" Then
                    Session("dea_codigo_ctest") = _codigo_ctest
                    Me.cbotipoestudio.selectedvalue = _codigo_test
                    Me.cbotipoestudio.enabled = False : Me.cboSemestre.enabled = False
                    Me.fecinicioaca.text = Me.gvcatest.datakeys(index).values("fechaInicio_cte")
                    Me.fecfinaca.text = Me.gvcatest.datakeys(index).values("fechaFin_cte")
                    If String.IsNullOrEmpty(Me.gvcatest.datakeys(index).values("fechaIniAdm_cte").tostring) Then
                        Me.fecinicioadm.text = ""
                    Else
                        Me.fecinicioadm.text = Me.gvcatest.datakeys(index).values("fechaIniAdm_cte")
                    End If
                    If String.IsNullOrEmpty(Me.gvcatest.datakeys(index).values("fechaFinAdm_cte").tostring) Then
                        Me.fecfinadm.text = ""
                    Else
                        Me.fecfinadm.text = Me.gvcatest.datakeys(index).values("fechaFinAdm_cte")
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCATest_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCATest.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lb_vigente As Boolean = Me.gvCATest.DataKeys(e.Row.RowIndex).Values.Item("vigente_cte")
                Dim lb_admision As Boolean = Me.gvCATest.DataKeys(e.Row.RowIndex).Values.Item("admision_cte")
                Dim _btn As LinkButton = CType(e.Row.Cells(e.Row.RowIndex).FindControl("btnHabilitar"), LinkButton)
                Dim _btnAdm As LinkButton = CType(e.Row.Cells(e.Row.RowIndex).FindControl("btnAdmision"), LinkButton)

                If lb_vigente Then
                    _btn.Visible = False
                Else
                    If cod_ctf = 1 Or cod_ctf = 182 Then
                        _btn.Visible = True
                    End If
                End If
                If lb_admision Then
                    _btnAdm.Visible = False
                Else
                    If cod_ctf = 1 Or cod_ctf = 182 Then
                        _btnAdm.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub gvCATest_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            'Dim dt As Data.DataTable
            'dt = CType(Session("gc_dtCorte"), Data.DataTable)
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 3, "Tipo de Estudio", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 4, "Semestre Académico", "#D9534F")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 4, "Semestre Administrativo", "#D9534F")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        'If modal Then
        '    Me.divAlertModal.Visible = True
        '    Me.lblMensaje.InnerText = Message
        '    Me.validar.Value = "0"
        '    updMensaje.Update()
        'Else
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        'End If
    End Sub

    Private Sub mt_CargarSemestre()
        Try
            Dim dt As New Data.DataTable("data")

            C.AbrirConexion()
            dt = C.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            C.CerrarConexion()

            If dt.rows.count > 0 Then
                dt.rows(0).item(1) = "Crear Nuevo"
            End If

            cboSemestre.DataSource = dt
            cboSemestre.DataTextField = "descripcion_Cac"
            cboSemestre.DataValueField = "codigo_Cac"
            cboSemestre.DataBind()

            dt.Dispose()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
        End Try
    End Sub

    Private Sub mt_CargarTipoEstudio(ByVal _codigo_cac As Integer)
        Try
            Dim dt As New Data.DataTable("data")

            C.AbrirConexion()
            dt = C.TraerDataTable("ACAD_ConsultarTipoEstudio", "PH", _codigo_cac)
            C.CerrarConexion()

            If dt.rows.count > 0 Then
                dt.rows(0).item(1) = "Elija Tipo de Estudio"
            End If

            cbotipoestudio.DataSource = dt
            cbotipoestudio.DataTextField = "descripcion_test"
            cbotipoestudio.DataValueField = "codigo_test"
            cbotipoestudio.DataBind()

            dt.Dispose()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>mostrarMensaje('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
        End Try
    End Sub

    Private Sub mt_CargarDatos(ByVal _codigo_cac As Integer)
        Try
            Dim dt As New System.Data.DataTable
            oeCATest = New e_CicloAcademico_TipoEstudio : odCATest = New d_CicloAcademico_TipoEstudio
            oeCATest.codigo_cac = _codigo_cac
            dt = odCATest.fc_ListarCicloAcademico_TipoEstudio(oeCATest)
            gvcatest.datasource = dt
            gvcatest.databind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        txtdescripcion.text = String.Empty
        fecinicioaca.text = String.Empty
        fecfinaca.text = String.Empty
        fecinicioadm.text = String.Empty
        fecfinadm.text = String.Empty
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

#End Region

End Class
