Imports System.Data

Partial Class administrativo_gestion_educativa_frmGrupoAdmisionVirtual
    Inherits System.Web.UI.Page

#Region "Variables"

    Private cnx As ClsConectarDatos
    Private cod_user As Integer, cod_ctf As Integer, cod_test As Integer
    Private mo_RepoAdmision As New ClsAdmision
    Private oeGrupo As e_GrupoAdmisionVirtual, odGrupo As d_GrupoAdmisionVirtual
    Private oeGruCco As e_GrupoAdmision_CentroCosto, odGruCco As d_GrupoAdmision_CentroCosto
    Private oeGruRes As e_GrupoAdmision_Responsable, odGruRes As d_GrupoAdmision_Responsable
    Dim oeAmbiente As e_Ambiente, odAmbiente As d_Ambiente

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Sub New()
        If cnx Is Nothing Then
            cnx = New ClsConectarDatos
            cnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../../sinacceso.html")
            End If
            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")
            cod_test = Request.QueryString("mod")
            If Not IsPostBack Then
                mt_CargarCentroCosto()
                Me.btnAgregar.visible = False
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'Protected Sub cboCentroCosto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCentroCosto.SelectedIndexChanged
    '    Try
    '        mt_CargarDatos(IIf(Me.cboCentroCosto.SelectedValue = -1, 0, Me.cboCentroCosto.SelectedValue))
    '        'Me.btnAgregar.visible = IIf(Me.cboCentroCosto.SelectedValue = -1, 0, Me.cboCentroCosto.SelectedValue)
    '    Catch ex As Exception
    '        mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
    '    End Try
    'End Sub

    Protected Sub cboCentroCosto2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCentroCosto2.SelectedIndexChanged
        Try
            Dim codigosCco As String = ""
            For Each _Item As ListItem In Me.cboCentroCosto2.Items
                If _Item.Selected AndAlso _Item.Value <> "-1" Then
                    If codigosCco.Length > 0 Then codigosCco &= ","
                    codigosCco &= _Item.Value
                End If
            Next
            Me.btnAgregar.visible = IIf(codigosCco.Length > 0, True, False)
            Session("adm_codigo_cco") = codigosCco
            mt_CargarDatos(codigosCco)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        mt_LimpiarControles()
        mt_CargarAmbiente(0)
        Page.RegisterStartupScript("Pop", "<script>openModal('" & "gru" & "','" & "Agregar" & "');</script>")

    End Sub

    'Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
    '    mt_LimpiarControles()
    'End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Try
            Dim _codigo_gru As Integer
            Dim dt As New System.Data.DataTable
            oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
            With oeGrupo
                .codigo_cco = Session("adm_codigo_cco") : .codigo = Me.txtcodigo.text.trim : .nombre = Me.txtnombre.text.trim
                .codigo_amb = Me.cboAmbiente.selectedvalue : .capacidad = CInt(Me.txtcapacidad.text)
                .aulaactiva = False : .estado = 1 : .codigo_per = cod_user
            End With
            If String.IsNullOrEmpty(Session("adm_codigo_gru")) Then
                dt = odGrupo.fc_RegistrarGrupoAdmisionVirtual(oeGrupo)
                If dt.Rows.Count > 0 Then
                    mt_ShowMessage("¡ Se registró correctamente el grupo !", MessageType.Success)
                    cboCentroCosto2_SelectedIndexChanged(Nothing, Nothing)
                    mt_LimpiarControles()
                Else
                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
                End If
            Else
                oeGrupo.codigo_gru = Session("adm_codigo_gru")
                dt = odGrupo.fc_ActualizarGrupoAdmisionVirtual(oeGrupo)
                If dt.Rows.Count > 0 Then
                    mt_ShowMessage("¡ Se actualizó correctamente el grupo !", MessageType.Success)
                    cboCentroCosto2_SelectedIndexChanged(Nothing, Nothing)
                    mt_LimpiarControles()
                Else
                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvGrupo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvGrupo.RowCommand
        Dim index As Integer, _codigo_gru, _virtual, _codigo_amb As Integer
        Dim dt As New System.Data.DataTable
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                _codigo_gru = Me.gvgrupo.datakeys(index).values("codigo_gru")
                If e.CommandName = "Editar" Then
                    Session("adm_codigo_gru") = _codigo_gru
                    Me.txtCodigo.text = Me.gvgrupo.datakeys(index).values("codigo")
                    Me.txtnombre.text = Me.gvgrupo.datakeys(index).values("nombre")
                    If Me.gvgrupo.datakeys(index).values("virtual_amb") Then
                        _virtual = 1
                        Me.rbTipo.selectedvalue = 1
                        Me.txtcapacidad.enabled = True
                    Else
                        _virtual = 0
                        Me.rbTipo.selectedvalue = 0
                        Me.txtcapacidad.enabled = False
                    End If
                    _codigo_amb = Me.gvgrupo.datakeys(index).values("codigo_amb")
                    Me.txtcapacidad.text = CInt(Me.gvgrupo.datakeys(index).values("capacidad"))
                    mt_CargarAmbiente(_virtual)
                    Me.cboAmbiente.selectedvalue = _codigo_amb
                    'mt_ShowMessage("virtual: " & Me.gvgrupo.datakeys(index).values("virtual_amb"), MessageType.Warning)
                    Page.RegisterStartupScript("Pop", "<script>openModal('" & "gru" & "','" & "Editar" & "');</script>")
                ElseIf e.CommandName = "Eliminar" Then
                    oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
                    oeGrupo.codigo_gru = _codigo_gru : oeGrupo.codigo_per = cod_user
                    dt = odGrupo.fc_EliminarGrupoAdmisionVirtual(oeGrupo)
                    If dt.Rows.Count > 0 Then
                        mt_ShowMessage("¡ Se eliminó correctamente el grupo !", MessageType.Success)
                        cboCentroCosto2_SelectedIndexChanged(Nothing, Nothing)
                        mt_LimpiarControles()
                    Else
                        mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
                    End If
                ElseIf e.CommandName = "CentroCosto" Then
                    Session("adm_codigo_gru") = _codigo_gru
                    Session("adm_codigo_gcc") = ""
                    mt_CargarCentroCco(_codigo_gru)
                    mt_CargarCco(_codigo_gru)
                    Page.RegisterStartupScript("Pop", "<script>openModal('" & "cco" & "','" & "Agregar" & "');</script>")
                ElseIf e.CommandName = "Responsable" Then
                    Session("adm_codigo_gru") = _codigo_gru
                    Session("adm_codigo_gre") = ""
                    mt_CargarPersonal(_codigo_gru)
                    mt_CargarResponsable(_codigo_gru)
                    Page.RegisterStartupScript("Pop", "<script>openModal('" & "res" & "','" & "Agregar" & "');</script>")
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub rbTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            mt_CargarAmbiente(Me.rbTipo.selectedvalue)
            Me.txtcapacidad.enabled = Me.rbTipo.selectedvalue
			Me.txtcapacidad.text = ""
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboAmbiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAmbiente.SelectedIndexChanged
        Try
            Dim dt As New System.Data.DataTable
            oeAmbiente = New e_Ambiente : odAmbiente = New d_Ambiente
            oeAmbiente.operacion = "VIR" : oeAmbiente.codigo_amb = Me.cboAmbiente.selectedvalue : oeAmbiente.codigo_cup = "-1"
            dt = odAmbiente.ListarAmbiente(oeAmbiente)
            If dt.Rows.Count > 0 Then
                Me.txtcapacidad.text = dt.Rows(0).Item(2)
            End If
            Me.panModal.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.cboResponsable.selectedvalue > -1 Then
                Dim dtResp As DataTable = CType(Session("adm_dtRes"), Data.DataTable)
                dtResp.Rows.Add(-1, Session("adm_codigo_gru"), Me.cboResponsable.selectedvalue, True, "(Agregando ...)", Me.cboResponsable.items(Me.cboResponsable.selectedindex).text)
                Session("adm_dtRes") = dtResp
                mt_ResponsableBindGrid()
                mt_ActualizarPersonal(Me.cboResponsable.selectedvalue)
            End If
            Me.panResponsable.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar3.Click
        Dim dt As New System.Data.DataTable
        Dim _codigo_per As String = "", _codigo_aux As String = ""
        Dim _codigo_gre As Integer = -1
        Try
            For i As Integer = 0 To Me.gvresponsable.Rows.Count - 1
                _codigo_gre = CInt(Me.gvresponsable.datakeys(i).values("codigo_gre"))
                If _codigo_gre = -1 Then
                    If _codigo_per.Length > 0 Then _codigo_per &= ","
                    _codigo_per &= CStr(Me.gvresponsable.datakeys(i).values("codigo_per"))
                End If
            Next
            _codigo_aux = Session("adm_codigo_gre")
            If _codigo_per.Length > 0 Or _codigo_aux.Length > 0 Then
                oeGruRes = New e_GrupoAdmision_Responsable : odGruRes = New d_GrupoAdmision_Responsable
                oeGruRes.codigo_gru = Session("adm_codigo_gru") : oeGruRes.codigo_aux = _codigo_aux
                oeGruRes.codigo_per = _codigo_per : oeGruRes.codigo_usu = cod_user
                dt = odGruRes.fc_RegistrarGrupoAdmision_Responsable(oeGruRes)
                If dt.Rows.Count > 0 Then
                    mt_ShowMessage("¡ Se agregaron correctamente los responsables !", MessageType.Success)
                    'cboGrupo_SelectedIndexChanged(Nothing, Nothing)
                Else
                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResponsable_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResponsable.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _codigo_gre As Integer = Me.gvResponsable.DataKeys(e.Row.RowIndex).Values("codigo_gre")
                Dim _estado_gre As Boolean = CBool(Me.gvResponsable.DataKeys(e.Row.RowIndex).Values("estado_gre"))
                If _codigo_gre = -1 Then
                    If _estado_gre Then
                        e.Row.Cells(1).ForeColor = Drawing.Color.Green
                    Else
                        e.Row.Cells(1).ForeColor = Drawing.Color.Red
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResponsable_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResponsable.RowCommand
        Dim index, _codigo_per, _codigo_gre As Integer
        Dim dt As New System.Data.DataTable
        Dim _Personal As String = "", _Codigo_aux As String = ""
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                If e.CommandName = "Quitar" Then
                    _codigo_gre = CInt(Me.gvResponsable.datakeys(index).values("codigo_gre"))
                    _codigo_per = Me.gvResponsable.datakeys(index).values("codigo_per")
                    _Personal = Me.gvResponsable.datakeys(index).values("Personal")
                    dt = CType(Session("adm_dtRes"), Data.DataTable)
                    Dim dv As Data.DataView
                    dv = New Data.DataView(dt, "codigo_per <> " & _codigo_per, "", Data.DataViewRowState.CurrentRows)
                    dt = dv.ToTable
                    Session("adm_dtRes") = dt
                    mt_ResponsableBindGrid()
                    mt_ActualizarPersonal(_codigo_per, _Personal)
                    If _codigo_gre <> -1 Then
                        _Codigo_aux = Session("adm_codigo_gre")
                        If _Codigo_aux.Length > 0 Then _codigo_gre &= ","
                        _Codigo_aux &= CStr(_codigo_gre)
                        Session("adm_codigo_gre") = _Codigo_aux
                    End If
                End If
            End If
            Me.panResponsable.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnAdd2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd2.Click
        Try
            If Me.cboCentro.selectedvalue > -1 Then
                Dim dtCco As DataTable = CType(Session("adm_dtCco"), Data.DataTable)
                dtCco.Rows.Add(-1, Session("adm_codigo_gru"), Me.cboCentro.selectedvalue, True, Me.cboCentro.items(Me.cboCentro.selectedindex).text, "(Agregando ...)", Date.Now.Date)
                Session("adm_dtCco") = dtCco
                mt_CentroCcoBindGrid()
                mt_ActualizarCentroCco(Me.cboCentro.selectedvalue)
            End If
            Me.panCentroCosto.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGrabar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar2.Click
        Dim dt As New System.Data.DataTable
        Dim _codigo_cco As String = "", _codigo_aux As String = ""
        Dim _codigo_gcc As Integer = -1
        Try
            For i As Integer = 0 To Me.gvCentroCosto.Rows.Count - 1
                _codigo_gcc = CInt(Me.gvCentroCosto.datakeys(i).values("codigo_gcc"))
                If _codigo_gcc = -1 Then
                    If _codigo_cco.Length > 0 Then _codigo_cco &= ","
                    _codigo_cco &= CStr(Me.gvCentroCosto.datakeys(i).values("codigo_cco"))
                End If
            Next
            _codigo_aux = Session("adm_codigo_gcc")
            If _codigo_cco.Length > 0 Or _codigo_aux.Length > 0 Then
                oeGruCco = New e_GrupoAdmision_CentroCosto : odGruCco = New d_GrupoAdmision_CentroCosto
                oeGruCco.codigo_gru = Session("adm_codigo_gru") : oeGruCco.codigo_aux = _codigo_aux
                oeGruCco.codigo_cco = _codigo_cco : oeGruCco.codigo_per = cod_user
                dt = odGruCco.fc_RegistrarGrupoAdmision_CentroCosto(oeGruCco)
                If dt.Rows.Count > 0 Then
                    mt_ShowMessage("¡ Se agregaron correctamente los centro de costos !", MessageType.Success)
                Else
                    mt_ShowMessage("¡ Ocurrio un error en el registro !", MessageType.Error)
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCentroCosto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCentroCosto.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _codigo_gcc As Integer = Me.gvCentroCosto.DataKeys(e.Row.RowIndex).Values("codigo_gcc")
                Dim _estado_gcc As Boolean = CBool(Me.gvCentroCosto.DataKeys(e.Row.RowIndex).Values("estado_gcc"))
                If _codigo_gcc = -1 Then
                    If _estado_gcc Then
                        e.Row.Cells(2).ForeColor = Drawing.Color.Green
                    Else
                        e.Row.Cells(2).ForeColor = Drawing.Color.Red
                    End If
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCentroCosto_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCentroCosto.RowCommand
        Dim index, _codigo_cco, _codigo_gcc As Integer
        Dim dt As New System.Data.DataTable
        Dim _CentroCco As String = "", _Codigo_aux As String = ""
        Dim _fecha As Date
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                If e.CommandName = "Quitar2" Then
                    _codigo_gcc = CInt(Me.gvCentroCosto.datakeys(index).values("codigo_gcc"))
                    _codigo_cco = CInt(Me.gvCentroCosto.datakeys(index).values("codigo_cco"))
                    _CentroCco = CStr(Me.gvCentroCosto.datakeys(index).values("descripcion_Cco"))
                    _fecha = CDate(Me.gvCentroCosto.datakeys(index).values("fechainiciopropuesta_dev"))
                    dt = CType(Session("adm_dtCco"), Data.DataTable)
                    Dim dv As Data.DataView
                    'dt = CType(Session("adm_dtCco"), Data.DataTable)
                    dv = New Data.DataView(dt, "codigo_cco <> " & _codigo_cco, "", Data.DataViewRowState.CurrentRows)
                    dt = dv.ToTable
                    Session("adm_dtCco") = dt
                    mt_CentroCcoBindGrid()
                    mt_ActualizarCentroCco(_codigo_cco, _CentroCco, _fecha)
                    If _codigo_gcc <> -1 Then
                        _Codigo_aux = Session("adm_codigo_gcc")
                        If _Codigo_aux.Length > 0 Then _codigo_gcc &= ","
                        _Codigo_aux &= CStr(_codigo_gcc)
                        Session("adm_codigo_gcc") = _Codigo_aux
                    End If
                End If
            End If
            Me.panCentroCosto.Update()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
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

    Private Sub mt_CargarCentroCosto()
        'ClsFunciones.LlenarListas(Me.cboCentroCosto, mo_RepoAdmision.ListarCentroCosto(cod_ctf, cod_user, cod_test), "codigo_Cco", "Nombre", "-- Seleccione --")
        ClsFunciones.LlenarListas(Me.cboCentroCosto2, mo_RepoAdmision.ListarCentroCosto(cod_ctf, cod_user, cod_test), "codigo_Cco", "Nombre")
    End Sub

    Private Sub mt_CargarDatos(ByVal _codigo_cco As String)
        Try
            Dim dt As New System.Data.DataTable
            oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
            oeGrupo.codigo_cco = _codigo_cco
            dt = odGrupo.fc_ListarGrupoAdmisionVirtual(oeGrupo)
            gvGrupo.datasource = dt
            gvGrupo.databind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarControles()
        Me.txtcodigo.text = ""
        Me.txtnombre.text = ""
        Me.rbTipo.selectedvalue = 0
        Me.cboAmbiente.selectedvalue = -1
        Me.txtcapacidad.text = ""
        Me.txtcapacidad.enabled = False
        Session("adm_codigo_gru") = ""
    End Sub

    Private Sub mt_CargarAmbiente(ByVal _virtual_amb As Integer)
        oeAmbiente = New e_Ambiente : odAmbiente = New d_Ambiente
        oeAmbiente.operacion = "VIR" : oeAmbiente.codigo_amb = -1 : oeAmbiente.codigo_cup = _virtual_amb
        ClsFunciones.LlenarListas(Me.cboAmbiente, odAmbiente.ListarAmbiente(oeAmbiente), "codigo_Amb", "descripcion", "-- Seleccione --")
    End Sub

    Private Sub mt_CargarPersonal(ByVal _codigo_gru As Integer)
        Dim dt As New System.Data.DataTable
        oeGruRes = New e_GrupoAdmision_Responsable : odGruRes = New d_GrupoAdmision_Responsable
        oeGruRes.codigo_gru = _codigo_gru : oeGruRes.tipoOperacion = "LT" : oeGruRes.codigo_per = "-1"
        dt = odGruRes.fc_ListarGrupoAdmision_Responsable(oeGruRes)
        Session("adm_dtPer") = dt
        ClsFunciones.LlenarListas(Me.cboResponsable, CType(Session("adm_dtPer"), Data.DataTable), "codigo_Per", "Personal", "-- Seleccione --")
    End Sub

    Private Sub mt_CargarResponsable(ByVal _codigo_gru As Integer)
        Dim dt As New System.Data.DataTable
        oeGruRes = New e_GrupoAdmision_Responsable : odGruRes = New d_GrupoAdmision_Responsable
        oeGruRes.codigo_gru = _codigo_gru : oeGruRes.codigo_per = "-1"
        dt = odGruRes.fc_ListarGrupoAdmision_Responsable(oeGruRes)
        Session("adm_dtRes") = dt
        mt_ResponsableBindGrid()
    End Sub

    Private Sub mt_ResponsableBindGrid()
        Me.gvresponsable.datasource = CType(Session("adm_dtRes"), Data.DataTable)
        Me.gvresponsable.databind()
    End Sub

    Private Sub mt_ActualizarPersonal(ByVal _codigo_per As Integer)
        Dim dt As New System.Data.DataTable
        Dim dv As Data.DataView
        dt = CType(Session("adm_dtPer"), Data.DataTable)
        dv = New Data.DataView(dt, "codigo_per <> " & _codigo_per, "", Data.DataViewRowState.CurrentRows)
        dt = dv.ToTable
        Session("adm_dtPer") = dt
        ClsFunciones.LlenarListas(Me.cboResponsable, CType(Session("adm_dtPer"), Data.DataTable), "codigo_Per", "Personal", "-- Seleccione --")
    End Sub

    Private Sub mt_ActualizarPersonal(ByVal _codigo_per As Integer, ByVal _Personal As String)
        Dim dt As New System.Data.DataTable
        Dim dv As Data.DataView
        Dim dtPer As DataTable = CType(Session("adm_dtPer"), Data.DataTable)
        dtPer.Rows.Add(_codigo_per, "", _Personal)
        'dt = CType(Session("adm_dtPer"), Data.DataTable)
        dv = New Data.DataView(dtPer, "", "Personal", Data.DataViewRowState.CurrentRows)
        dt = dv.ToTable
        Session("adm_dtPer") = dt
        ClsFunciones.LlenarListas(Me.cboResponsable, CType(Session("adm_dtPer"), Data.DataTable), "codigo_Per", "Personal", "-- Seleccione --")
    End Sub

    Private Sub mt_CargarCco(ByVal _codigo_gru As Integer)
        Dim dt, dtAux As New System.Data.DataTable
        Dim _codigo_cco As Integer
        dt = mo_RepoAdmision.ListarCentroCosto(cod_ctf, cod_user, cod_test)
        Session("adm_dtCbo") = dt

        For i As Integer = 0 To Me.gvCentroCosto.rows.count - 1
            Dim dv As Data.DataView
            _codigo_cco = Me.gvCentroCosto.datakeys(i).values("codigo_cco")
            dt = CType(Session("adm_dtCbo"), Data.DataTable)
            dv = New Data.DataView(dt, "codigo_cco <> " & _codigo_cco, "", Data.DataViewRowState.CurrentRows)
            dt = dv.ToTable
            Session("adm_dtCbo") = dt
        Next

        ClsFunciones.LlenarListas(Me.cboCentro, CType(Session("adm_dtCbo"), Data.DataTable), "codigo_Cco", "Nombre", "-- Seleccione --")
    End Sub

    Private Sub mt_CargarCentroCco(ByVal _codigo_gru As Integer)
        Dim dt As New System.Data.DataTable
        oeGruCco = New e_GrupoAdmision_CentroCosto : odGruCco = New d_GrupoAdmision_CentroCosto
        oeGruCco.codigo_gru = _codigo_gru
        dt = odGruCco.fc_ListarGrupoAdmision_CentroCosto(oeGruCco)
        Session("adm_dtCco") = dt
        mt_CentroCcoBindGrid()
    End Sub

    Private Sub mt_CentroCcoBindGrid()
        Me.gvCentroCosto.datasource = CType(Session("adm_dtCco"), Data.DataTable)
        Me.gvCentroCosto.databind()
    End Sub

    Private Sub mt_ActualizarCentroCco(ByVal _codigo_cco As Integer)
        Dim dt As New System.Data.DataTable
        Dim dv As Data.DataView
        dt = CType(Session("adm_dtCbo"), Data.DataTable)
        dv = New Data.DataView(dt, "codigo_cco <> " & _codigo_cco, "", Data.DataViewRowState.CurrentRows)
        dt = dv.ToTable
        Session("adm_dtCbo") = dt
        ClsFunciones.LlenarListas(Me.cboCentro, CType(Session("adm_dtCbo"), Data.DataTable), "codigo_Cco", "Nombre", "-- Seleccione --")
    End Sub

    Private Sub mt_ActualizarCentroCco(ByVal _codigo_cco As Integer, ByVal _Descripcion As String, ByVal _fecha As Date)
        Dim dt As New System.Data.DataTable
        Dim dv As Data.DataView
        Dim dtCco As DataTable = CType(Session("adm_dtCbo"), Data.DataTable)
        dtCco.Rows.Add(_codigo_cco, _Descripcion, _fecha)
        'dt = CType(Session("adm_dtPer"), Data.DataTable)
        dv = New Data.DataView(dtCco, "", "fechainiciopropuesta_dev DESC", Data.DataViewRowState.CurrentRows)
        dt = dv.ToTable
        Session("adm_dtCbo") = dt
        ClsFunciones.LlenarListas(Me.cboCentro, CType(Session("adm_dtCbo"), Data.DataTable), "codigo_Cco", "Nombre", "-- Seleccione --")
    End Sub

#End Region

End Class
