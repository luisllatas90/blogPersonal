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

    Private oeTipoGrupoEval As e_TipoGrupoEvaluacion, odTipoGrupoEval As d_TipoGrupoEvaluacion '20200914-ENevado

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
                Response.Redirect("https://intranet.usat.edu.pe/campusvirtual/sinacceso.html")
            End If
            cod_user = Session("id_per")
            cod_ctf = Request.QueryString("ctf")
            cod_test = Request.QueryString("mod")
            If Not IsPostBack Then
                mt_CargarCentroCosto()
                mt_CargarTipoGrupoEval() '20200914-ENevado
                Me.btnAgregar.visible = False
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Evento de Combo Tipo Grupo Evaluacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>20200914-ENevado</remarks>
    Protected Sub cboFiltroTipoEva_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFiltroTipoEva.SelectedIndexChanged
        Try
            cboCentroCosto2_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cboCentroCosto2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCentroCosto2.SelectedIndexChanged
        Dim codigosCco As String = ""
        Dim codigo_tge As Integer = -1 '20200914-ENevado
        Try
            '20200914-ENevado -----------------------------------------
            If Me.cboFiltroTipoEva.selectedvalue = "-1" Then
                codigo_tge = 0
            Else
                codigo_tge = Me.cboFiltroTipoEva.selectedvalue
            End If
            '-----------------------------------------------------------
            For Each _Item As ListItem In Me.cboCentroCosto2.Items
                If _Item.Selected AndAlso _Item.Value <> "-1" Then
                    If codigosCco.Length > 0 Then codigosCco &= ","
                    codigosCco &= _Item.Value
                End If
            Next
            Me.btnAgregar.visible = IIf(codigosCco.Length > 0, True, False)
            Session("adm_codigo_cco") = codigosCco
            mt_CargarDatos(codigosCco, codigo_tge) '20200914-ENevado
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

            Dim datosValidos As Boolean = True, mensajeError As String = ""

            Dim fechaHoraInicio As Object = DBNull.Value
            Dim fechaHoraFin As Object = DBNull.Value

            Dim fecha As Date, nFecha As Nullable(Of Date)
            If txtFecha.Value.Trim <> "" Then
                If Not Date.TryParse(txtFecha.Value.Trim, fecha) Then
                    mensajeError = "La fecha ingresada no es válida" : datosValidos = False
                End If
                nFecha = fecha
            End If

            Dim horaInicio As TimeSpan, nHoraInicio As Nullable(Of TimeSpan)
            If txtHoraInicio.Value.Trim <> "" Then
                If txtFecha.Value.Trim = "" Then
                    mensajeError = "Para registrar la hora de inicio debe ingresar también la fecha" : datosValidos = False
                End If
                If Not TimeSpan.TryParse(txtHoraInicio.Value.Trim, horaInicio) Then
                    mensajeError = "La hora de inicio ingresada no es válida" : datosValidos = False
                End If
                nHoraInicio = horaInicio
            End If

            Dim horaFin As TimeSpan, nHoraFin As Nullable(Of TimeSpan)
            If txtHoraFin.Value.Trim <> "" Then
                If txtFecha.Value.Trim = "" Then
                    mensajeError = "Para registrar la hora de térmito debe ingresar también la fecha" : datosValidos = False
                End If
                If Not TimeSpan.TryParse(txtHoraFin.Value.Trim, horaFin) Then
                    mensajeError = "La hora de término ingresada no es válida" : datosValidos = False
                End If
                nHoraFin = horaFin
            End If

            If nFecha.HasValue Then
                fechaHoraInicio = nFecha.Value

                If nHoraInicio.HasValue Then
                    fechaHoraInicio = nFecha.Value.Add(nHoraInicio)
                End If
                If nHoraFin.HasValue Then
                    fechaHoraFin = nFecha.Value.Add(nHoraFin)
                    If fechaHoraFin < fechaHoraInicio Then
                        mensajeError = "La hora de término no puede ser menor a la hora de inicio" : datosValidos = False
                    End If
                End If
            End If

            If Not datosValidos Then
                mt_ShowMessage(mensajeError, MessageType.Error)
                Page.RegisterStartupScript("Pop", "<script>openModal('" & "gru" & "','" & "Editar" & "', 750);</script>")
                Exit Sub
            End If

            With oeGrupo
                .codigo_cco = Session("adm_codigo_cco") : .codigo = Me.txtcodigo.text.trim : .nombre = Me.txtnombre.text.trim
                .fechaHoraInicio_gru = fechaHoraInicio : .fechaHoraFin_gru = fechaHoraFin
                .codigo_amb = Me.cboAmbiente.selectedvalue : .capacidad = CInt(Me.txtcapacidad.text)
                .aulaactiva = False : .estado = 1 : .codigo_per = cod_user
                .codigo_tge = Me.cboTipoGrupoEval.selectedvalue
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
        Dim index As Integer, _codigo_gru, _virtual, _codigo_amb, _codigo_tge As Integer
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
                    
                    Dim fecha As Object = Me.gvGrupo.DataKeys(index).Values("fechaHoraInicio_gru")
                    If fecha IsNot DBNull.Value Then fecha = Date.Parse(fecha).ToString("dd/MM/yyyy") Else fecha = ""

                    Dim horaInicio As Object = Me.gvGrupo.DataKeys(index).Values("fechaHoraInicio_gru")
                    If horaInicio IsNot DBNull.Value Then
                        If Date.Parse(horaInicio).Hour = 0 And Date.Parse(horaInicio).Minute = 0 Then
                            horaInicio = ""
                        Else
                            horaInicio = Date.Parse(horaInicio).ToString("hh:mm")
                        End If
                    Else
                        horaInicio = ""
                    End If

                    Dim horaFin As Object = Me.gvGrupo.DataKeys(index).Values("fechaHoraFin_gru")
                    If horaFin IsNot DBNull.Value Then horaFin = Date.Parse(horaFin).ToString("hh:mm") Else horaFin = ""

                    _codigo_amb = Me.gvgrupo.datakeys(index).values("codigo_amb")
                    _codigo_tge = Me.gvgrupo.datakeys(index).values("codigo_tge")
                    Me.txtcapacidad.text = CInt(Me.gvgrupo.datakeys(index).values("capacidad"))
                    mt_CargarAmbiente(_virtual)
                    Me.cboAmbiente.selectedvalue = _codigo_amb
                    Me.cboTipoGrupoEval.selectedvalue = _codigo_tge
                    Me.txtFecha.Value = fecha
                    Me.txtHoraInicio.Value = horaInicio
                    Me.txtHoraFin.Value = horaFin
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
        ' 20200205ENevado ---------------------- \
        Dim _ctf As Integer = cod_ctf
        If _ctf = 26 Or _ctf = 168 Then
            _ctf = 1
        End If
        '---------------------------------------/
        ClsFunciones.LlenarListas(Me.cboCentroCosto2, mo_RepoAdmision.ListarCentroCosto(_ctf, cod_user, cod_test), "codigo_Cco", "Nombre")
    End Sub

    Private Sub mt_CargarDatos(ByVal _codigo_cco As String, ByVal _codigo_tge As Integer)
        Dim dt As New System.Data.DataTable
        Try
            oeGrupo = New e_GrupoAdmisionVirtual : odGrupo = New d_GrupoAdmisionVirtual
            oeGrupo.codigo_cco = _codigo_cco : oeGrupo.codigo_tge = _codigo_tge '20200914-ENevado
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
        Me.cboTipoGrupoEval.selectedvalue = -1
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

    ''' <summary>
    ''' Metodo para llenar los combos de Tipo Grupo Evaluacion
    ''' </summary>
    ''' <remarks>20200914-ENevado</remarks>
    Private Sub mt_CargarTipoGrupoEval()
        oeTipoGrupoEval = New e_TipoGrupoEvaluacion : odTipoGrupoEval = New d_TipoGrupoEvaluacion
        ClsFunciones.LlenarListas(Me.cboFiltroTipoEva, odTipoGrupoEval.fc_Listar(oeTipoGrupoEval), "codigo_tge", "nombre_tge", "TODOS")
        ClsFunciones.LlenarListas(Me.cboTipoGrupoEval, odTipoGrupoEval.fc_Listar(oeTipoGrupoEval), "codigo_tge", "nombre_tge", "-- Seleccionar --")
    End Sub

#End Region

End Class
