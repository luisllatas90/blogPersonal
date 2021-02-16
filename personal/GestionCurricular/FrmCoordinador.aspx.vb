Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmCoordinador
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private oeCoordinador As e_CoordinadorAsignatura, odCoordinador As d_CoordinadorAsignatura ' 20200114-ENevado
    
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
            cod_user = Request.QueryString("id")
            cod_ctf = Request.QueryString("ctf")
            If IsPostBack = False Then
                Session("gc_CoordinadorAsignatura") = Nothing
                mt_CargarSemestre()
                Me.cboTipoCur.SelectedValue = IIf(cod_ctf = 41 Or cod_ctf = 138, 1, 2)
                Me.cboTipoCur.Enabled = IIf(cod_ctf = 41 Or cod_ctf = 138, False, True)
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
    '    Call mt_CargarDatos()
    'End Sub

    'Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
    '    Try
    '        Me.hdCodigoCoordinador.Value = ""

    '        Page.RegisterStartupScript("Pop", "<script>openModal('nuevo', '', '');</script>")
    '    Catch ex As Exception
    '        Call mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    'Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.gvCoordinador.Rows.Count = 0 Then
    '            Page.RegisterStartupScript("alerta", "<script>alert('No existe Coordinador para editar');</script>")
    '            Me.hdCodigoCoordinador.Value = ""

    '            'Me.btnListar.Focus()
    '        Else
    '            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
    '            Dim area, doc As String

    '            Me.hdCodigoCoordinador.Value = button.Attributes("codigo_coo")

    '            area = button.Attributes("codigo_are")
    '            doc = button.Attributes("codigo_Per")

    '            Page.RegisterStartupScript("Pop", "<script>openModal('editar', '" & area & "', '" & doc & "');</script>")
    '        End If

    '    Catch ex As Exception
    '        Call mt_ShowMessage("Error_CargaDatos(): " & ex.Message.Replace("'", " "), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        ' Dim obj As New ClsConectarDatos
        Dim codigo_coo, codigo_cur, codigo_pes As Integer
        Dim creditos_cur As Integer
        Dim dt As New Data.DataTable
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
            codigo_cur = Session("gc_codigo_cur")
            codigo_pes = Session("gc_codigo_pes")
            creditos_cur = Session("gc_creditos_cur")
            codigo_coo = 0
            'mt_ShowMessage("code: " & Session("gc_codigo_coo"), MessageType.Info)
            'obj.AbrirConexion()
            oeCoordinador = New e_CoordinadorAsignatura : odCoordinador = New d_CoordinadorAsignatura
            With oeCoordinador
                .codigo_per = Me.ddlDocente.SelectedValue : .codigo_cac = Me.cboSemestre.SelectedValue : .codigo_cur = codigo_cur
                .codigo_pes = IIf(Me.cboTipoCur.SelectedValue = 1, -2, codigo_pes) : .codigo_per_reg = cod_user : .indicador_coo = IIf(Me.chkDocente.Checked, 1, 0)
                .creditos_cur = creditos_cur '--> Por Luis Q.T. 16DIC2020
            End With
            If String.IsNullOrEmpty(Session("gc_codigo_coo")) Then
                'dt = obj.TraerDataTable("COM_RegistrarCoordinadorAsignatura", Me.ddlDocente.SelectedValue, Me.cboSemestre.SelectedValue, codigo_cur, IIf(Me.cboTipoCur.SelectedValue = 1, DBNull.Value, codigo_pes), cod_user, IIf(Me.chkDocente.Checked, 1, 0))
                dt = odCoordinador.fc_RegistrarCoordinadorAsignatura(oeCoordinador)
                If dt.Rows.Count = 0 Then Throw New Exception("¡ No se pudo procesar la información !")
                mt_ShowMessage("¡ Coordinador registrado con éxito !", MessageType.Success)
            Else
                codigo_coo = Session("gc_codigo_coo")
                'obj.Ejecutar("COM_ActualizarCoordinadorAsignatura", codigo_coo, Me.ddlDocente.SelectedValue, Me.cboSemestre.SelectedValue, codigo_cur, IIf(Me.cboTipoCur.SelectedValue = 1, DBNull.Value, codigo_pes), cod_user, IIf(Me.chkDocente.Checked, 1, 0))
                oeCoordinador.codigo_coo = codigo_coo
                odCoordinador.fc_ActualizarCoordinadorAsignatura(oeCoordinador)
                mt_ShowMessage("Coordinador actualizado con éxito", MessageType.Success)
            End If
            'obj.CerrarConexion()
            mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue, Me.cboCarrProf.SelectedValue, Me.cboEstado.SelectedValue)
            'Else
            'Throw New Exception("Inicie Sesión")
            'End If
        Catch ex As Exception
            'obj.CerrarConexion()
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSemestre.SelectedIndexChanged
        Try
            'If cboSemestre.SelectedValue = -1 Then
            '    mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_ctf, cod_user)
            '    mt_LimpiarGrilla()
            '    mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
            '    Me.cboSemestre.Focus()
            '    Exit Sub
            'End If
            If Me.cboSemestre.SelectedIndex > -1 Then
                Me.cboTipoCur.SelectedValue = IIf(cod_ctf = 41 Or cod_ctf = 138, 1, 2)
                Me.cboTipoCur.Enabled = IIf(cod_ctf = 41 Or cod_ctf = 138, False, True)
                mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, cod_ctf, cod_user)
                cboCarrProf_SelectedIndexChanged(Nothing, Nothing)
                'If Me.cboCarrProf.Items.Count > 0 Then Me.cboCarrProf.SelectedValue = -1
                'mt_LimpiarGrilla()
                If cod_ctf = 41 Or cod_ctf = 138 Then
                    cboTipoCur_SelectedIndexChanged(Nothing, Nothing) : cboEstado_SelectedIndexChanged(Nothing, Nothing)
                    'mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue, Me.cboCarrProf.SelectedValue, Me.cboEstado.SelectedValue)
                End If
                'Me.cboTipoCur.SelectedValue = "2"
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboTipoCur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoCur.SelectedIndexChanged
        Try
            If cboSemestre.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                Me.cboSemestre.Focus()
                Exit Sub
            End If
            If cod_ctf <> 41 And cod_ctf <> 138 And Me.cboCarrProf.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Carrera Profesional !", MessageType.Warning)
                Me.cboCarrProf.Focus()
                Exit Sub
            End If
            If cboTipoCur.SelectedIndex > -1 Then
                'mt_CargarCarreraProfesional(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue)
                'Me.cboCarrProf.Enabled = IIf(Me.cboTipoCur.SelectedValue = 0 Or Me.cboTipoCur.SelectedValue = 1, False, True)
                If cboTipoCur.SelectedValue = 1 Then
                    Me.cboEstado.SelectedValue = "0"
                    'cboEstado_SelectedIndexChanged(Nothing, Nothing)
                    'Else
                    '    If Me.gvCoordinador.Rows.Count > 0 Then
                    '        Me.gvCoordinador.DataSource = Nothing : Me.gvCoordinador.DataBind()
                    '        Me.txtBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
                    '        Me.btnBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
                    '    End If
                End If
                cboEstado_SelectedIndexChanged(Nothing, Nothing)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCarrProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCarrProf.SelectedIndexChanged
        Try
            If cboSemestre.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                Me.cboSemestre.Focus()
                Exit Sub
            End If
            If cod_ctf <> 41 And cod_ctf <> 138 And Me.cboCarrProf.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Carrera Profesional !", MessageType.Warning)
                Me.cboCarrProf.Focus()
                Exit Sub
            End If
            If cboCarrProf.SelectedIndex > -1 Then
                Me.cboEstado.SelectedValue = 0
                mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue, IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCoordinador_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCoordinador.PageIndexChanging
        Me.gvCoordinador.DataSource = CType(Session("gc_CoordinadorAsignatura"), Data.DataTable)
        Me.gvCoordinador.DataBind()
        Me.gvCoordinador.PageIndex = e.NewPageIndex
        Me.gvCoordinador.DataBind()
    End Sub

    Protected Sub gvCoordinador_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCoordinador.RowCommand
        Dim obj As New ClsConectarDatos
        Dim index, codigo_cur, codigo_pes, codigo_per, codigo_coo, creditos_Cur As Integer
        Dim indicador As Boolean = False
        Dim asignatura As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            index = CInt(e.CommandArgument)
            codigo_cur = Me.gvCoordinador.DataKeys(index).Values("codigo_cur")
            codigo_pes = Me.gvCoordinador.DataKeys(index).Values("codigo_pes")
            codigo_per = Me.gvCoordinador.DataKeys(index).Values("codigo_per")
            codigo_coo = Me.gvCoordinador.DataKeys(index).Values("codigo_coo")
            asignatura = Me.gvCoordinador.DataKeys(index).Values("nombre_Cur")
            indicador = CBool(Me.gvCoordinador.DataKeys(index).Values("indicador_coo"))
            creditos_Cur = Me.gvCoordinador.DataKeys(index).Values("creditos_Cur")

            If indicador Then
                If cod_ctf = 236 Then
                    mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, 3, "FS")
                    indicador = True
                Else
                    mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, -1, "CX")
                End If
            Else
                mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, codigo_cur, "CA")
            End If

            Session("gc_codigo_coo") = ""
            Session("gc_codigo_cur") = codigo_cur
            Session("gc_codigo_pes") = codigo_pes
            Session("gc_creditos_cur") = creditos_Cur
            Me.chkDocente.Checked = indicador
            'Me.chkDocente.Text = "Listar Docentes de la Carrera Profesional"
            Select Case e.CommandName
                Case "Agregar"
                    'Me.ddlDocente.SelectedIndex = -1
                    Page.RegisterStartupScript("Pop", "<script>openModal('agregar','','" & asignatura & "');</script>")
                Case "Editar"
                    Session("gc_codigo_coo") = codigo_coo
                    Page.RegisterStartupScript("Pop", "<script>openModal('editar','" & codigo_per & "','" & asignatura & "');</script>")
            End Select
            'Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            Dim dt, dtAux As New Data.DataTable
            Dim dv As Data.DataView
            If Me.txtBuscar.Text.Trim <> "" Then
                dv = New Data.DataView(CType(Session("gc_CoordinadorAsignatura"), Data.DataTable), "nombre_Cur like '%" & Me.txtBuscar.Text.Trim & "%'".Trim, "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_CoordinadorAsignatura"), Data.DataTable)
            End If
            Me.gvCoordinador.DataSource = dt
            Me.gvCoordinador.DataBind()
            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        Try
            If cboSemestre.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Semestre Académico !", MessageType.Warning)
                Me.cboSemestre.Focus()
                Exit Sub
            End If
            If cod_ctf <> 41 And cod_ctf <> 138 And Me.cboCarrProf.SelectedValue = -1 Then
                mt_LimpiarGrilla()
                mt_ShowMessage("¡ Seleccione Carrera Profesional !", MessageType.Warning)
                Me.cboCarrProf.Focus()
                Exit Sub
            End If
            If cboCarrProf.SelectedIndex > -1 Then
                If cod_ctf <> 41 And cod_ctf <> 138 Then
                    mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue, IIf(Me.cboCarrProf.SelectedValue = -1, 0, Me.cboCarrProf.SelectedValue), Me.cboEstado.SelectedValue)
                Else
                    mt_CargarDatos(Me.cboSemestre.SelectedValue, Me.cboTipoCur.SelectedValue, Me.cboCarrProf.SelectedValue, Me.cboEstado.SelectedValue)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub chkDocente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDocente.CheckedChanged
        Try
            If chkDocente.Checked Then
                If cod_ctf = 236 Then
                    mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, 3, "FS")
                Else
                    mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, -1, "CX")
                End If

                'Me.chkDocente.Text = "Listar Docentes del Curso: " & Me.txtAsignatura.Text
            Else
                mt_CargarDocente(Me.cboSemestre.SelectedValue, Me.cboCarrProf.SelectedValue, Session("gc_codigo_cur"), "CA")
                'Me.chkDocente.Text = "Listar Docentes de la Carrera Profesional"
            End If
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCoordinador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCoordinador.RowDataBound
        Dim btn As LinkButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                btn = New linkbutton
                btn = CType(e.Row.Cells(5).FindControl("btnAgregar"), LinkButton)
                If Me.cboTipoCur.selectedvalue = 1 Then
                    btn.enabled = iif(cod_ctf = 41, True, False)
                    If cod_ctf <> 41 Then btn.OnClientClick = ""
                End If
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

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DAN", "")
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboSemestre, dt, "codigo_Cac", "descripcion_Cac")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub mt_CargarArea()
    '    Dim obj As New ClsConectarDatos
    '    Dim dt As New Data.DataTable("Area")
    '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

    '    Try
    '        obj.AbrirConexion()
    '        dt = obj.TraerDataTable("COM_ListarAreaAsignatura")
    '        obj.CerrarConexion()

    '        Call mt_CargarCombo(Me.ddlArea, dt, "codigo_are", "nombre_are")
    '        dt.dispose()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub mt_CargarDocente(ByVal codigo_cac As Integer, ByVal codigo_cpf As Integer, ByVal codigo_cur As Integer, ByVal tipo As String)
        'Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("Docente")
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            'obj.AbrirConexion()
            'dt = obj.TraerDataTable("COM_ListarCoordinadorAsignatura", tipo, -1, codigo_cac, codigo_cur, -1, -1, codigo_cpf, -1)
            'obj.CerrarConexion()
            ' 20200114-ENevado ---------------------------------------------------------------------------------------------\
            oeCoordinador = New e_CoordinadorAsignatura : odCoordinador = New d_CoordinadorAsignatura
            With oeCoordinador
                .TipoOperacion = tipo : .codigo_cac = codigo_cac : .codigo_cur = codigo_cur : .codigo_cpf = codigo_cpf
            End With
            dt = odCoordinador.fc_ListarCoordinadorAsignatura(oeCoordinador)
            ' --------------------------------------------------------------------------------------------------------------/
            mt_CargarCombo(Me.ddlDocente, dt, "codigo_Per", "Coordinador")
            If dt.Rows.Count = 1 Then
                If dt.Rows(0).Item(0) = -2 Then
                    Me.ddlDocente.Enabled = False
                Else
                    Me.ddlDocente.Enabled = True
                End If
            Else
                Me.ddlDocente.Enabled = True
            End If
            'Me.panModal.Update()
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

    Private Sub mt_CargarDatos(ByVal codigo_cac As Integer, ByVal codigo_tc As Integer, ByVal codigo_cpf As Integer, ByVal estado As Integer)
        'Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("Datos")
        'obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            'obj.AbrirConexion()
            'dt = obj.TraerDataTable("COM_ListarCoordinadorAsignatura", "TC", estado, codigo_cac, -1, -1, -1, codigo_cpf, codigo_tc, cod_ctf)
            'obj.CerrarConexion()
            ' 20200114-ENevado ---------------------------------------------------------------------------------------------\
            oeCoordinador = New e_CoordinadorAsignatura : odCoordinador = New d_CoordinadorAsignatura
            With oeCoordinador
                .TipoOperacion = "TC" : .codigo_coo = estado : .codigo_cac = codigo_cac : .codigo_cpf = codigo_cpf : .codigo_tc = codigo_tc : .codigo_ctf = cod_ctf
            End With
            dt = odCoordinador.fc_ListarCoordinadorAsignatura(oeCoordinador)
            ' --------------------------------------------------------------------------------------------------------------/
            Session("gc_CoordinadorAsignatura") = dt
            Me.gvCoordinador.DataSource = CType(Session("gc_CoordinadorAsignatura"), Data.DataTable)
            Me.gvCoordinador.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
            ' 20200114-ENevado ---------------------------------------------------------------------------------------------\
            If Me.cboTipoCur.selectedvalue = 1 Then
                Me.lblAviso.visible = True
                Me.lblAviso.innertext = "Para los Estudios Generales (transversales) solo prodrán asignar coordinador el Vicerrectorado."
            Else
                Me.lblAviso.visible = False
                Me.lblAviso.innertext = ""
            End If
            ' --------------------------------------------------------------------------------------------------------------/
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProfesional(ByVal codigo_cac As Integer, ByVal codigo_ctf As Integer, ByVal codigo_user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            'cod_user = 648
            dt = obj.TraerDataTable("ConsultarCarreraProfesionalV3", "TC", codigo_cac, codigo_ctf, codigo_user)
            obj.CerrarConexion()
            mt_CargarCombo(Me.cboCarrProf, dt, "codigo_Cpf", "nombre_Cpf")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Protected Sub gvCoordinador_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCoordinador.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim celda As TableCellCollection = e.Row.Cells
    '        Dim codigo_coo As String = Me.gvCoordinador.DataKeys(e.Row.RowIndex).Values.Item("codigo_coo")
    '        Dim codigo_are As String = Me.gvCoordinador.DataKeys(e.Row.RowIndex).Values.Item("codigo_are")
    '        Dim codigo_Per As String = Me.gvCoordinador.DataKeys(e.Row.RowIndex).Values.Item("codigo_per")
    '        Dim idx As Integer = e.Row.RowIndex + 1

    '        Dim btnEditar As New HtmlButton
    '        With btnEditar
    '            .ID = "btnEditar" & idx
    '            .Attributes.Add("type", "button")
    '            .Attributes.Add("codigo_coo", codigo_coo)
    '            .Attributes.Add("codigo_are", codigo_are)
    '            .Attributes.Add("codigo_Per", codigo_Per)
    '            .Attributes.Add("class", "btn btn-primary btn-sm")
    '            .Attributes.Add("title", "Editar Coordinador")
    '            .InnerHtml = "<i class='fa fa-edit' title='Editar Coordinador'></i>"

    '            AddHandler .ServerClick, AddressOf btnEditar_Click
    '        End With
    '        celda(4).Controls.Add(btnEditar)

    '        gvCoordinador.HeaderRow.TableSection = TableRowSection.TableHeader
    '    End If
    'End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    'Private Sub RefreshGrid()
    '    For Each _Row As GridViewRow In gvCoordinador.Rows
    '        gvCoordinador_RowDataBound(gvCoordinador, New GridViewRowEventArgs(_Row))
    '    Next
    'End Sub

    Private Sub mt_LimpiarGrilla()
        If Me.gvCoordinador.Rows.Count > 0 Then
            Me.gvCoordinador.DataSource = Nothing : Me.gvCoordinador.DataBind()
            Me.txtBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvCoordinador.Rows.Count > 0, True, False)
        End If
    End Sub

#End Region

End Class
