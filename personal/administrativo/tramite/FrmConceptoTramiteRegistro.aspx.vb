Imports System.IO
Imports System.Security.Cryptography
Imports System.Collections.Generic

Partial Class FrmConceptoTramiteRegistro
    Inherits System.Web.UI.Page

#Region "Variables"

    Private C As ClsConectarDatos
    Private nuevo As Boolean = False
    Private cod_user As Integer '= 684
    Private cod_ctf As Integer '= 1
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private idTabla As Integer = 17 ' Desarrollo




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
                Response.Redirect("../../../sinacceso.html")

            Else

                cod_user = Session("id_per")
                cod_ctf = Request.QueryString("ctf")

                If Not IsPostBack Then
                    '#ddl Grado y Titulo{
                    Me.ddlTieneDGyT.Items.Clear()
                    Me.ddlTieneDGyT.Items.Add("")
                    Me.ddlTieneDGyT.DataBind()
                    divGyT.Visible = False
                    '}#ddl Grado y Titulo

                    Call mt_CargarTipoTramite()
                    Call mt_CargarTipoEstudio()
                    Call mt_CargarActividadCronograma()
                    ' Call mt_ListarConceptoTramite()
                    Me.txtTramiteBsq.Focus()
                Else
                    'Call RefreshGrid()
                End If
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_LimpiarGrid(ByVal grid As String)

        If grid = "sco" Then
            grwServicio.DataSource = Nothing
            grwServicio.DataBind()
            Me.txtServicioBsq.Text = String.Empty
            Me.txtCodServicio.Text = String.Empty
        ElseIf grid = "cco" Then
            grwCentroCosto.DataSource = Nothing
            grwCentroCosto.DataBind()
            Me.txtCentroCostoBsq.Text = String.Empty
            Me.txtCodCentroCosto.Text = String.Empty
        End If

    End Sub

    Protected Sub grwCentroCosto_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwCentroCosto.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "SelCentroCosto") Then

                If grwCentroCosto.DataKeys(index).Values("ref") = True Then
                    Me.txtCentroCostosCodRef.Value = grwCentroCosto.DataKeys(index).Values("codigo_cco").ToString
                    Me.txtCentroCostosRef.Text = grwCentroCosto.DataKeys(index).Values("descripcion_cco").ToString
                Else
                    Me.txtCentroCostosCod.Value = grwCentroCosto.DataKeys(index).Values("codigo_cco").ToString
                    Me.txtCentroCostos.Text = grwCentroCosto.DataKeys(index).Values("descripcion_cco").ToString
                End If
                mt_LimpiarGrid("cco")

                Me.pnlBuscarCentroCosto.Visible = False
                Me.pnlRegistro.Visible = True
                Me.pnlTramiteVirtual.Visible = True
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try

    End Sub

    Protected Sub grwServicio_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwServicio.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "SelServicio") Then
                If grwServicio.DataKeys(index).Values("ref") = True Then
                    Me.txtServicioConceptoCodRef.Value = grwServicio.DataKeys(index).Values("codigo_Sco").ToString
                    Me.txtServicioConceptoRef.Text = grwServicio.DataKeys(index).Values("descripcion_Sco").ToString
                Else
                    Me.txtServicioConceptoCod.Value = grwServicio.DataKeys(index).Values("codigo_Sco").ToString
                    Me.txtServicioConcepto.Text = grwServicio.DataKeys(index).Values("descripcion_Sco").ToString
                End If

                mt_LimpiarGrid("sco")
                Me.pnlBuscarServicio.Visible = False
                Me.pnlRegistro.Visible = True
                Me.pnlTramiteVirtual.Visible = True

            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try

    End Sub

    Protected Sub grwResultado_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwResultado.RowCommand
        Try

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "Editar") Then
                Me.hdCtr.Value = Encriptar(grwResultado.DataKeys(index).Values("codigo_ctr").ToString)
                Me.hdPaso.Value = 1 'Registro concepto Tramite 

                mt_MostrarConceptoTramite()
                Me.pnlRegistro.Visible = True
                Me.pnlTramiteVirtual.Visible = True
                Me.pnlLista.Visible = False
                Me.hdUpd.Value = 1
                mt_mostrarPaso("1")
            ElseIf (e.CommandName = "Eliminar") Then
                Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
                Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
                Dim rpta As Boolean = False
                Dim nombreCtr As String = grwResultado.DataKeys(index).Values("codigo_ctr") & "-" & grwResultado.DataKeys(index).Values("descripcion_ctr")
                Dim clsTrl As New clsConceptoTramite
                With clsTrl
                    ._codigoCtr = grwResultado.DataKeys(index).Values("codigo_ctr")
                    ._codigo_per = CInt(Session("id_per"))
                    ._ip = ip
                    ._ordenador = host
                End With

                rpta = clsTrl.Eliminar()
                If rpta Then
                    ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                    Call mt_ShowMessage("Trámite " & nombreCtr.ToString & " eliminado con &eacute;xito", MessageType.Success)
                    Call mt_ListarConceptoTramite()
                Else
                    rpta = False
                    Call mt_ShowMessage("Error al eliminar concepto trámite", MessageType.Error)
                    'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
                End If


                clsTrl = Nothing
            Else

            End If

            'Response.Write(Me.hdCtr.Value)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    Protected Sub gvFlujoTramite_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFlujoTramite.RowCommand
        Try

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "Editar") Then
                Call mt_CargarFuncionesUsuario()
                Me.Hdftr.Value = Encriptar(gvFlujoTramite.DataKeys(index).Values("codigo_ftr").ToString)
                Call mt_MostrarFlujoTramite()
                Me.pnlFlujoTramiteRegistro.Visible = True
                Me.pnlFlujoTramiteLista.Visible = False

                Me.btnGuardarTramite.Enabled = False
                Me.btnCancelarTramite.Enabled = False
            Else

            End If

            'Response.Write(Me.hdCtr.Value)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub

    'Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
    'Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
    'End Sub


    Protected Sub grwResultado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwResultado.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim celda As TableCellCollection = e.Row.Cells

              

                If celda(5).Text = "True" Or celda(5).Text = "False" Then
                    If (CBool(celda(5).Text) = True) Then
                        celda(5).Text = "<i class='fa fa-check'></i>"
                    ElseIf (CBool(celda(5).Text) = False) Then
                        celda(5).Text = "<i class='fa fa-ban'></i>"
                    End If
                End If

                If celda(6).Text = "True" Or celda(6).Text = "False" Then
                    If (CBool(celda(6).Text) = True) Then
                        celda(6).Text = "<i class='fa fa-check'></i>"
                    ElseIf (CBool(celda(6).Text) = False) Then
                        celda(6).Text = "<i class='fa fa-ban'></i>"
                    End If
                End If
                ' e.Row.Attributes.Add("onclick", Me.ClientScript.GetPostBackEventReference(CType(sender, Control), "Select$" & e.Row.RowIndex))
                'e.Row.Attributes.Add("onclickserver", e.Row.RowIndex)

                Dim ddl_dat As DropDownList = CType(e.Row.FindControl("ddl_data"), DropDownList)

                'Dim _codigo_ctr As String = grwResultado.DataKeys(e.Row.RowIndex).Values(0).ToString() ' codigo_ctr
                Dim _rowIndex As String = e.Row.RowIndex
                Dim _tieneCarreraProfesionalAsociada As String = grwResultado.DataKeys(e.Row.RowIndex).Values("tieneCarreraProfesionalAsociada").ToString() ' tieneCarreraProfesionalAsociada
                Dim _tieneMensajeInformativo As String = grwResultado.DataKeys(e.Row.RowIndex).Values("tieneMensajeInformativo").ToString() ' tieneMensajeInformativo
                Dim _tieneReglas As String = grwResultado.DataKeys(e.Row.RowIndex).Values("tieneReglas").ToString() ' tieneReglas

                'Response.Write(_tieneReglas & "<br>")

                Dim dtOpcion As New Data.DataTable()
                dtOpcion.Columns.Add("cod")
                dtOpcion.Columns.Add("nombre")

                Dim fila As Data.DataRow = dtOpcion.NewRow()
                fila("cod") = "-"
                fila("nombre") = "-"
                dtOpcion.Rows.Add(fila)

                If CBool(_tieneCarreraProfesionalAsociada) Then

                    Dim fila1 As Data.DataRow = dtOpcion.NewRow()
                    fila1("cod") = _rowIndex.ToString & "-1"
                    fila1("nombre") = "Carreras Asociadas"
                    dtOpcion.Rows.Add(fila1)
                End If

                If CBool(_tieneMensajeInformativo) Then
                    Dim fila2 As Data.DataRow = dtOpcion.NewRow()
                    fila2("cod") = _rowIndex.ToString & "-2"
                    fila2("nombre") = "Mensaje Informativo"
                    dtOpcion.Rows.Add(fila2)
                End If

                If CBool(_tieneReglas) Then
                    Dim fila3 As Data.DataRow = dtOpcion.NewRow()
                    fila3("cod") = _rowIndex.ToString & "-3"
                    fila3("nombre") = "Tiene Reglas"
                    dtOpcion.Rows.Add(fila3)
                End If

                If dtOpcion.Rows.Count > 1 Then
                    ddl_dat.Visible = True
                Else
                    ddl_dat.Visible = False
                End If

                ddl_dat.CssClass = "form-control2"
                ddl_dat.DataSource = dtOpcion
                ddl_dat.DataValueField = "cod"
                ddl_dat.DataTextField = "nombre"
                ddl_dat.DataBind()

            Else
                'Dim lbl_data As Label = CType(e.Row.FindControl("lbl_data"), Label)
                'lbl_data.Visible = True
                'lbl_data.Text = ""
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub


    Protected Sub ddl_dat_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dropDownList As DropDownList = CType(sender, DropDownList)

            Dim opcion As String = dropDownList.SelectedValue
            Dim opcionStr As String() = opcion.Split("-"c)

            Dim _rowIndex As Integer = fnDevuelveNumEntero(opcionStr(0).ToString)
            Dim opcionIndex As Integer = fnDevuelveNumEntero(opcionStr(1).ToString)

            Select Case opcionIndex

                Case "1" '"Carreras Asociadas"
                    '0 Response.Write("Carreras Asociadas")
                    mt_mostrarPaso(3, _rowIndex)
                Case "2" '"Mensaje Informativo"
                    'Response.Write("Mensaje Informativo")
                    mt_mostrarPaso(4, _rowIndex)
                Case "3" '"Tiene Reglas"
                    'Response.Write("Tiene Reglas")
                    sptotalreglas.innerHtml = "0"
                    mt_mostrarPaso(5, _rowIndex)

            End Select


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub grwResultado_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub btnCrear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrear.Click
        Try
            Me.hdUpd.Value = 0
            Me.hdPaso.Value = 1
            Me.hdCtr.Value = ""
            Me.txtTramite.Focus()
            Me.pnlLista.Visible = False
            Me.pnlRegistro.Visible = True
            Me.pnlTramiteVirtual.Visible = True
            mt_mostrarPaso("1")
            Me.chkActivar.Checked = True
            Me.txtAprobadoPorPersonal.Text = Session("nombreper") & " (" & Session("perlogin") & ")"

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        mt_ListarConceptoTramite()
    End Sub



    Private Sub mt_LimpiarConceptoTramite()
        Me.hdCtr.Value = ""

        Me.txtServicioConceptoCod.Value = String.Empty
        Me.txtServicioConcepto.Text = String.Empty
        Me.txtPrecio.Text = String.Empty
        Me.ddlCantidad.SelectedIndex = 0
        Me.txtTramite.Text = String.Empty
        Me.txtUbicacion.Text = String.Empty
        Me.ddlTipoEstudio.SelectedIndex = 0
        Me.chkTieneRequisitos.Checked = False
        Me.ddlSolVirtual.SelectedValue = 1
        Me.chkActivar.Checked = False
        Me.ddlEgresado.SelectedValue = 1
        Me.ddlTipoTramite.SelectedIndex = 0
        Me.ddlActividad.SelectedIndex = 0
        Me.chkTieneReglas.Checked = False
        Me.chkTieneFlujo.Checked = False
        Me.txtPrecioScoRef.Text = String.Empty
        Me.txtServicioConceptoCodRef.Value = String.Empty
        Me.txtServicioConceptoRef.Text = String.Empty
        Me.chkTieneCarrerasAsociadas.Checked = False
        Me.chkEstadoActivo.Checked = False
        ' img
        Me.txtCentroCostosCod.Value = String.Empty
        Me.txtCentroCostos.Text = String.Empty
        Me.chkTieneArchivo.Checked = False
        Me.chkTieneEntrega.Checked = False
        Me.chkTieneNotaAbono.Checked = False
        Me.chkTieneMensajeInformativo.Checked = False
        Me.ddlTieneDGyT.DataSource = Nothing
        divGyT.Visible = False
        Me.ddlTieneDGyT.DataBind()
        Me.txtCentroCostosCodRef.Value = String.Empty
        Me.txtCentroCostosRef.Text = String.Empty

    End Sub



    Private Sub mt_CargarFuncionesUsuario()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_MANT_FuncionesUsuario_Listar", "1", 64)

            Me.ddlFuncion_ft.DataSource = dt
            Me.ddlFuncion_ft.DataValueField = "codigo_Tfu"
            Me.ddlFuncion_ft.DataTextField = "descripcion_Tfu"
            Me.ddlFuncion_ft.DataBind()

            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_ListarFlujoTramite()

        'Try
        '    Dim dt As New Data.DataTable("Data")
        '    Dim tipo As String = "1"
        '    Dim codigo_ftr As Integer = 0
        '    Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value.ToString))
        '    Dim estado As String = ""
        '    If chkMostrarFlujoTramiteActivo.Checked Then
        '        estado = "1"
        '    Else
        '        estado = ""
        '    End If
        '    C.AbrirConexion()
        '    dt = C.TraerDataTable("TRL_MANT_FlujotramiteListar", tipo, codigo_ftr, codigo_ctr, estado)
        '    Me.gvFlujoTramite.DataSource = dt
        '    Me.gvFlujoTramite.DataBind()
        '    dt.Dispose()

        '    C.CerrarConexion()

        'Catch ex As Exception
        '    Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        'End Try

        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"
            Dim codigo_mctr As Integer = 0
            Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value.ToString))
            Dim orden As Integer = 0
            Dim estado As String = ""

            Dim cls As New clsFLujoTramite
            cls.tipooperacion = "1"
            cls._codigo_ftr = 0
            cls._codigo_ctr = codigo_ctr
            cls._orden_ftr = orden
            If chkMostrarFlujoTramiteActivo.Checked Then
                estado = "1"
            Else
                estado = ""
            End If

            dt = cls.Listar(estado)

            'C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            'C.CerrarConexion()

            Me.gvFlujoTramite.DataSource = dt
            Me.gvFlujoTramite.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message & "-" & ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub


    Protected Sub btnConsultarFlujoTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultarFlujoTramite.Click

        mt_ListarFlujoTramite()

    End Sub
    Protected Sub btnConsultarMensajeInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultarMensajeInfo.Click

        mt_ListarMensajeInformativo()

    End Sub


    Protected Sub btnCancelarFlujoTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarFlujoTramite.Click
        Me.btnGuardarTramite.Enabled = True
        Me.btnCancelarTramite.Enabled = True

        Me.pnlFlujoTramiteRegistro.Visible = False
        Me.pnlFlujoTramiteLista.Visible = True
    End Sub

    Protected Sub btnGuardarFlujoTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarFlujoTramite.Click

        If mt_GuardarPaso2FlujoTramite() Then
            mt_ListarFlujoTramite()
            Me.btnGuardarTramite.Enabled = True
            Me.btnCancelarTramite.Enabled = True

            Me.pnlFlujoTramiteRegistro.Visible = False
            Me.pnlFlujoTramiteLista.Visible = True

            mt_LimpiarFlujoTramite()
        End If

    End Sub

    Protected Sub btnActualizarFuncionFT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizarFuncionFT.Click
        mt_CargarFuncionesUsuario()
    End Sub


    Protected Sub btnCrearFlujoTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrearFlujoTramite.Click

        Me.pnlFlujoTramiteRegistro.Visible = True
        Me.pnlFlujoTramiteLista.Visible = False

        Me.btnGuardarTramite.Enabled = False
        Me.btnCancelarTramite.Enabled = False

        mt_CargarFuncionesUsuario()
        mt_LimpiarFlujoTramite()



    End Sub


    Protected Sub chkProcesa_ft_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkProcesa_ft.CheckedChanged

        If Me.chkProcesa_ft.Checked = True Then
            Me.txtproceso_ft.Enabled = True
            Me.txtproceso_ft.BackColor = Drawing.Color.White
            Me.txtproceso_ft.Focus()
        Else
            Me.txtproceso_ft.Enabled = False
            Me.txtproceso_ft.BackColor = Drawing.Color.LightGray
            Me.txtproceso_ft.Text = String.Empty

        End If
    End Sub

    Protected Sub btnCancelaActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelaActualizar.Click
        If Me.hdPaso.Value = 1 Then
            mt_mostrarPaso("2")
            Me.pnlFlujoTramite.Visible = True

            mt_ListarFlujoTramite()
        End If
    End Sub

    
    Private Sub mt_MostrarFlujoTramite()
        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "2"

            Dim codigo_ftr As Integer = CInt(Desencriptar(Me.Hdftr.Value))
            Dim codigo_ctr As Integer = 0
            Dim cls As New clsFLujoTramite

            cls.tipooperacion = tipo
            cls._codigo_ftr = codigo_ftr
            cls._codigo_ctr = codigo_ctr

            dt = cls.Listar("")
            'C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_FlujotramiteListar", tipo, codigo_ftr, codigo_ctr, 0, "")
            'C.CerrarConexion()

            If dt.Rows.Count = 1 Then
                With dt.Rows(0)

                    'Me.txtTramite.Text = .Item("descripcion_ctr").ToString
                    'Me.txtUbicacion.Text = .Item("ubicacion_ctr").ToString
                    Me.ddlFuncion_ft.SelectedValue = .Item("codigo_tfu")
                    Me.ddlOrden_ft.SelectedValue = .Item("orden_ftr")


                    If .Item("accionUrl_ftr") = 1 Then
                        Me.chkProcesa_ft.Checked = True
                        Me.txtproceso_ft.Text = ""
                        Me.txtproceso_ft.Enabled = True
                        Me.txtproceso_ft.BackColor = Drawing.Color.White
                    Else
                        Me.chkProcesa_ft.Checked = False
                        Me.txtproceso_ft.Text = ""
                        Me.txtproceso_ft.Enabled = False
                        Me.txtproceso_ft.BackColor = Drawing.Color.LightGray
                    End If

                    If .Item("verDetAdm_ftr") = 1 Then
                        Me.chkVerDatosAdm_ft.Checked = True
                    Else
                        Me.chkVerDatosAdm_ft.Checked = False
                    End If

                    If .Item("verDetAcad_ftr") = 1 Then
                        Me.chkVerDatosAcad_ft.Checked = True
                    Else
                        Me.chkVerDatosAcad_ft.Checked = False
                    End If

                    If .Item("estado") = 1 Then
                        Me.chkEstado_ft.Checked = True
                    Else
                        Me.chkEstado_ft.Checked = False
                    End If

                    If .Item("tieneEmailAprobacion") = 1 Then
                        Me.chkEmailAprobacion_ft.Checked = True
                    Else
                        Me.chkEmailAprobacion_ft.Checked = False
                    End If

                    If .Item("tieneEmailRechazo") = 1 Then
                        Me.chkEmailRechazo_ft.Checked = True
                    Else
                        Me.chkEmailRechazo_ft.Checked = False
                    End If

                    If .Item("tieneEmailPlanEstudio") = 1 Then
                        Me.chkEmailPlanEstudio_ft.Checked = True
                    Else
                        Me.chkEmailPlanEstudio_ft.Checked = False
                    End If

                    If .Item("tieneEmailPrecioCredito") = 1 Then
                        Me.chkEmailPrecioCredito_ft.Checked = True
                    Else
                        Me.chkEmailPrecioCredito_ft.Checked = False
                    End If

                    If .Item("tieneEmailGyT") = 1 Then
                        Me.chkEmailGyt_ft.Checked = True
                    Else
                        Me.chkEmailGyt_ft.Checked = False
                    End If

                    If .Item("tieneMsjTextoRechazo") = 1 Then
                        Me.chkmsnrechazar_ft.Checked = True
                    Else
                        Me.chkmsnrechazar_ft.Checked = False
                    End If

                    If .Item("tieneMsjTextoAprobacion") = 1 Then
                        Me.chkmsnaprobacion_ft.Checked = True
                    Else
                        Me.chkmsnaprobacion_ft.Checked = False
                    End If

                End With
            End If


            Me.ddlFuncion_ft.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnCrearMensajeInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrearMensajeInfo.Click

        Me.pnlMensajeInformativoRegistro.Visible = True
        Me.pnlMensajeInformativoLista.Visible = False

        Me.btnGuardarTramite.Enabled = False
        Me.btnCancelarTramite.Enabled = False

        Me.txtDescripcion_msjinfo.Focus()

    End Sub

    Protected Sub btnCancelarMensajeInfo_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarMensajeInfo.Click
        Me.pnlMensajeInformativoRegistro.Visible = False
        Me.pnlMensajeInformativoLista.Visible = True
        Me.btnGuardarTramite.Enabled = True
        Me.btnCancelarTramite.Enabled = True
        mt_LimpiarMensajeInformativo()
    End Sub

    Private Sub btnGuardarMensajeInfo_click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarMensajeInfo.Click

        If mt_GuardarPaso4MensajeInformativo() Then
            Me.pnlMensajeInformativoRegistro.Visible = False
            Me.pnlMensajeInformativoLista.Visible = True
            Me.btnGuardarTramite.Enabled = True
            Me.btnCancelarTramite.Enabled = True
            mt_LimpiarMensajeInformativo()
            mt_ListarMensajeInformativo()
        End If

    End Sub

    Private Sub mt_ListarReglas()

        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"
            Dim codigo_rctr As Integer = 0
            Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value.ToString))


            Dim clsReglaCtr As New clsReglaConceptoTramite
            clsReglaCtr.tipooperacion = "2"
            clsReglaCtr._codigo_rctr = 0
            clsReglaCtr._codigo_ctr = codigo_ctr

            
            dt = clsReglaCtr.Listar()
            'C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            'C.CerrarConexion()
            Me.grwReglas.DataSource = dt
            Me.grwReglas.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message & "-" & ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Private Sub mt_ListarMensajeInformativo()

        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"
            Dim codigo_mctr As Integer = 0
            Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value.ToString))
            Dim estado As String = ""

            Dim clsMsjInfo As New clsMensajeInformativo
            clsMsjInfo.tipooperacion = "1"
            clsMsjInfo._codigo_mctr = 0
            clsMsjInfo._codigo_ctr = codigo_ctr

            If chkMostrarMensajeInformativoActivo.Checked Then
                estado = "1"
            Else
                estado = ""
            End If
            dt = clsMsjInfo.Listar(estado)
            'C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            'C.CerrarConexion()
            Me.grwMensajeInformativo.DataSource = dt
            Me.grwMensajeInformativo.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message & "-" & ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_MostrarMensajeInformativo()


        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"
            Dim codigo_mctr As Integer = CInt(Desencriptar(Me.Hdmctr.Value.ToString))
            Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value.ToString))
            Dim estado As String = ""

            Dim clsMsjInfo As New clsMensajeInformativo
            clsMsjInfo.tipooperacion = "1"
            clsMsjInfo._codigo_mctr = codigo_mctr


            dt = clsMsjInfo.Listar()

            If Not dt Is Nothing AndAlso dt.Rows.Count Then
                With dt.Rows(0)
                    Me.txtDescripcion_msjinfo.Text = .Item("descripcion")
                    Me.ddlOrden_msjinfo.SelectedValue = .Item("orden")

                    If .Item("estado") Then
                        Me.chkActivo_msjinfo.Checked = True
                    Else
                        Me.chkActivo_msjinfo.Checked = False
                    End If

                End With
            End If



            dt.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message & "-" & ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscarReglas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarReglas.click
        mt_ListarReglas()
    End Sub

    Protected Sub btnAceptaActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptaActualizar.Click


        If Me.hdPaso.Value = 1 Then

            If mt_GuardarPaso1ConceptoTramite() Then
                mt_ListarFlujoTramite()
                mt_mostrarPaso("2")
            End If

        End If
    End Sub

    Private Function fnValidarEdicionConceptoTramite() As Boolean
        Try

            Dim rpta As Boolean = True

            Dim dt As New Data.datatable

            'dt = CType(Session("lstTramiteEditar"), Data.datatable)

            dt = CType(Session("TramiteEditar"), data.datatable)

            'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim myObjectJson As String = javaScriptSerializer.Serialize(dt)
            'Response.Clear()
            'Response.ContentType = "application/json; charset=utf-8"
            'Response.Write(myObjectJson)
            'Response.[End]()


            'Response.Write(dt.rows.count)


            With dt.rows(0)
                If .Item("codigo_ctr") > 0 Then

                    If .Item("codigo_sco") <> fnDevuelveNumEntero(Me.txtServicioConceptoCod.Value) Then
                        'Response.Write(1)
                        Return False
                    End If

                    If .Item("precio_ctr") <> fnDevuelveNumDecimal(Me.txtPrecio.Text) Then
                        'Response.Write(2)
                        Return False
                    End If
                    If .Item("cantMax_ctr") <> Me.ddlCantidad.SelectedValue Then
                        'Response.Write(3)
                        Return False
                    End If
                    If .Item("descripcion_ctr") <> Me.txtTramite.Text Then
                        'Response.Write(4)
                        Return False
                    End If
                    If .Item("codigo_sco") <> fnDevuelveNumEntero(Me.txtServicioConceptoCod.Value) Then
                        'Response.Write(5)
                        Return False
                    End If
                    If .item("ubicacion_ctr") <> Me.txtUbicacion.Text Then
                        'Response.Write(6)
                        Return False
                    End If
                    If .Item("codigo_test") <> Me.ddlTipoEstudio.SelectedValue Then
                        'Response.Write(7)
                        Return False
                    End If
                    'Response.Write("tieneRequisito: " & .Item("tieneRequisito"))
                    If .Item("tieneRequisito") <> Me.chkTieneRequisitos.Checked Then
                        'Response.Write(8)
                        Return False
                    End If
                    If .Item("tieneSolicitudVirtual") <> Me.ddlSolVirtual.SelectedValue Then
                        'Response.Write(9)
                        Return False
                    End If
                    If .Item("mostrar") <> Me.chkActivar.Checked Then
                        'Response.Write(10)
                        Return False
                    End If
                    If .Item("esEgresado") <> Me.ddlEgresado.SelectedValue Then
                        'Response.Write(11)
                        Return False
                    End If
                    If .Item("codigo_tctr") <> Me.ddlTipoTramite.SelectedValue Then
                        'Response.Write(12)
                        Return False
                    End If
                    If .Item("codigo_act") <> Me.ddlActividad.SelectedValue Then
                        'Response.Write(13)
                        Return False
                    End If
                    If .Item("tieneReglas") <> Me.chkTieneReglas.Checked Then
                        'Response.Write(14)
                        Return False
                    End If
                    If .Item("tieneFlujo") <> Me.chkTieneFlujo.Checked Then
                        'Response.Write(15)
                        Return False
                    End If
                    If .Item("refprecio_ctr") <> fnDevuelveNumDecimal(Me.txtPrecioScoRef.Text) Then
                        'Response.Write(16)
                        Return False
                    End If
                    If .Item("refcodigo_sco") <> fnDevuelveNumEntero(Me.txtServicioConceptoCodRef.Value) Then
                        'Response.Write(17)
                        Return False
                    End If
                    'Response.Write("tieneCarreraProfesionalAsociada: " & .Item("tieneCarreraProfesionalAsociada"))
                    If .Item("tieneCarreraProfesionalAsociada") <> Me.chkTieneCarrerasAsociadas.Checked Then
                        'Response.Write(18)
                        Return False
                    End If

                    If .Item("mostrar") <> Me.chkActivar.Checked Then
                        'Response.Write(19)
                        Return False
                    End If

                    If .Item("codigo_cco") <> fnDevuelveNumEntero(Me.txtCentroCostosCod.Value) Then
                        'Response.Write(21)
                        Return False
                    End If

                    If .Item("tieneArchivo") <> Me.chkTieneArchivo.Checked Then
                        'Response.Write(22)
                        Return False
                    End If

                    If .Item("tieneEntrega") <> Me.chkTieneEntrega.Checked Then
                        'Response.Write(23)
                        Return False
                    End If

                    If .Item("tieneNotaAbonoAutomatica") <> Me.chkTieneNotaAbono.Checked Then
                        'Response.Write(24)
                        Return False
                    End If

                    If .Item("tieneMensajeInformativo") <> Me.chkTieneMensajeInformativo.Checked Then
                        'Response.Write(25)
                        Return False
                    End If


                    If Me.ddlTieneDGyT.Items.count > 0 Then
                        'Response.Write("SelectedValue: " & fnDevuelveNumEntero(.Item("codigo_tdg")))
                        If .Item("codigo_tdg") <> fnDevuelveNumEntero(Me.ddlTieneDGyT.SelectedValue) Then
                            'Response.Write(26)
                            Return False
                        End If
                    End If

                    If .Item("refcodigo_cco") <> fnDevuelveNumEntero(Me.txtCentroCostosCodRef.Value) Then
                        'Response.Write(27)
                        Return False
                    End If
                Else
                    Return True

                End If
            End With


            Return True

        Catch ex As Exception
            Response.Write(ex.message & "--" & ex.StackTrace)
            Return False
        End Try
    End Function

    Protected Sub btnGuardarTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarTramite.Click
        'Response.Write(Me.hdUpd.Value)
        'Response.Write("<br>")
        'Response.Write(Me.hdPaso.Value)
        'Response.Write("<br>")

        If Me.hdUpd.Value = 0 Then
            If Me.hdPaso.Value = 1 Then
                If mt_GuardarPaso1ConceptoTramite() Then
                    mt_mostrarPaso("2")
                End If
            ElseIf Me.hdPaso.Value = 2 Then
                'mt_mostrarPaso("3")
                Me.pnlLista.Visible = True
                Me.pnlRegistro.Visible = False
                Me.pnlTramiteVirtual.Visible = False
                mt_ListarConceptoTramite()
            ElseIf Me.hdPaso.Value = 3 Then
                ' guardar carreraprofesional
                If mt_GuardarPaso3CarreraProfesional() Then
                    Me.pnlLista.Visible = True
                    Me.pnlRegistro.Visible = False
                    Me.pnlTramiteVirtual.Visible = False
                    mt_ListarConceptoTramite()
                Else

                End If
            ElseIf Me.hdPaso.Value = 5 Then
                ' Response.Write("grabar regla")
                If mt_GuardarPaso5ReglaTramite() Then
                    Me.pnlLista.Visible = True
                    Me.pnlRegistro.Visible = False
                    Me.pnlTramiteVirtual.Visible = False
                    mt_ListarConceptoTramite()
                Else

                End If

            Else
                Me.pnlLista.Visible = True
                Me.pnlRegistro.Visible = False
                Me.pnlTramiteVirtual.Visible = False
                mt_ListarConceptoTramite()
            End If
        ElseIf Me.hdUpd.Value = 1 Then

            If Me.hdPaso.Value = 1 Then
                'Response.Write("hdUpd: " & Me.hdUpd.Value & "<br>")
                'Response.Write("hdPaso: " & Me.hdPaso.Value & "<br>")

                If fnValidarEdicionConceptoTramite() Then
                    'Me.pnlLista.Visible = True
                    'Me.pnlRegistro.Visible = False
                    'Me.pnlTramiteVirtual.Visible = False
                    'mt_ListarConceptoTramite()
                    mt_mostrarPaso(2)
                    mt_ListarFlujoTramite()
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>openModal('mdConfirmarActualizacion');</script>")
                End If

            ElseIf Me.hdPaso.Value = 2 Then
                'mt_mostrarPaso("3")
                Me.pnlLista.Visible = True
                Me.pnlRegistro.Visible = False
                Me.pnlTramiteVirtual.Visible = False
                mt_ListarConceptoTramite()
                'Else
                '    ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>openModal('mdConfirmarActualizacion');</script>")
            End If



        End If
    End Sub

    Protected Sub btnCancelarTramite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarTramite.Click
        If Me.hdPaso.Value = 1 Then
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
            Me.pnlTramiteVirtual.Visible = False
            mt_LimpiarConceptoTramite()
            'mt_MostrarFlujoTramite()
            'Response.Write(1)
        ElseIf Me.hdPaso.Value = 2 Then
            mt_mostrarPaso("1")
            'Response.Write(2)
            mt_MostrarConceptoTramite()
        ElseIf Me.hdPaso.Value = 3 Then
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
            Me.pnlTramiteVirtual.Visible = False
            mt_ListarConceptoTramite()
            'Response.Write(3)
        ElseIf Me.hdPaso.Value = 4 Then
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
            Me.pnlTramiteVirtual.Visible = False
            mt_ListarConceptoTramite()
        ElseIf Me.hdPaso.Value = 4 Then
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
            Me.pnlTramiteVirtual.Visible = False
            mt_ListarConceptoTramite()
        ElseIf Me.hdPaso.Value = 5 Then
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
            Me.pnlTramiteVirtual.Visible = False
            mt_ListarConceptoTramite()
        End If

    End Sub

    Private Sub mt_mostrarPaso(ByVal opc As Integer, Optional ByVal _rowIndex As Integer = -1)
        Try
            Dim titulo As String = ""
            Dim _codigo_ctr As String = ""
            Select Case opc

                Case 1 ' registro
                    Me.tituloPaso.InnerHtml = "Registrar Concepto Trámite"
                    Me.pnlRegistro.Visible = True
                    Me.pnlFlujoTramite.Visible = False
                    Me.pnlCarreraAsociada.Visible = False
                    Me.pnlMensajeInformativo.Visible = False
                    Me.hdPaso.Value = "1"

                    Me.pnlMensajeInformativo.Visible = False
                    Me.pnlMensajeInformativoLista.Visible = False
                    Me.pnlMensajeInformativoRegistro.Visible = False

                    Me.pnlReglas.Visible = False
                    Me.pnlReglasLista.Visible = False



                Case 2 ' flujo tramite
                    Me.tituloPaso.InnerHtml = Chr(39) & Chr(39) & Me.txtTramite.Text & Chr(39) & Chr(39) & " <br> <i class='fa fa-edit'></i> Registrar Flujo Trámite"
                    Me.pnlRegistro.Visible = False
                    Me.pnlFlujoTramite.Visible = True
                    Me.pnlFlujoTramiteLista.Visible = True
                    Me.pnlFlujoTramiteRegistro.Visible = False
                    Me.pnlCarreraAsociada.Visible = False
                    Me.pnlMensajeInformativo.Visible = False

                    Me.hdPaso.Value = "2"



                    Me.pnlMensajeInformativoLista.Visible = False
                    Me.pnlMensajeInformativoRegistro.Visible = False

                    Me.pnlReglas.Visible = False
                    Me.pnlReglasLista.Visible = False
                Case 3 ' Carreras Asociadas
                    Dim _codigo_test As Integer
                    Me.hdPaso.Value = "3"
                    Me.hdUpd.Value = "0"
                    _codigo_ctr = grwResultado.DataKeys(_rowIndex).Values(0).ToString() ' codigo_ctr
                    _codigo_test = grwResultado.DataKeys(_rowIndex).Values(8).ToString() ' codigo_test
                    Me.hdCtr.Value = Encriptar(_codigo_ctr.ToString)

                    titulo = grwResultado.DataKeys(_rowIndex).Values(1).ToString() ' descripcion_ctr

                    titulo = titulo & "<br>" & "<i class='fa fa-check-square'></i> Asociar Carreras Profesionales"
                    Me.tituloPaso.InnerHtml = titulo

                    Me.pnlLista.Visible = False
                    Me.pnlTramiteVirtual.Visible = True
                    Me.pnlFlujoTramite.Visible = False
                    If _codigo_test > 0 Then
                        Me.ddlTipoEstudioBsqmd.SelectedValue = _codigo_test
                        ' Me.ddltipoestudioBsqmd.enabled = True
                        'onfocus="this.defaultIndex=this.selectedIndex;" onchange="this.selectedIndex=this.defaultIndex;"
                        Me.ddlTipoEstudioBsqmd.BackColor = Drawing.Color.DarkGray
                        Me.ddlTipoEstudioBsqmd.Attributes.Item("onfocus") = "this.defaultIndex=this.selectedIndex;"
                        Me.ddlTipoEstudioBsqmd.Attributes.Item("onchange") = "this.selectedIndex=this.defaultIndex;"
                    Else
                        Me.ddlTipoEstudioBsqmd.BackColor = Drawing.Color.White
                        Me.ddlTipoEstudioBsqmd.Attributes.Item("onfocus") = ""
                        Me.ddlTipoEstudioBsqmd.Attributes.Item("onchange") = ""
                    End If

                    Me.pnlCarreraAsociada.Visible = True
                    Me.pnlMensajeInformativo.Visible = False


                    Me.pnlMensajeInformativoLista.Visible = False
                    Me.pnlMensajeInformativoRegistro.Visible = False

                    Me.pnlReglas.Visible = False
                    Me.pnlReglasLista.Visible = False

                    mt_ListarCarreraProfesional()

                Case 4

                    Me.tituloPaso.InnerHtml = Chr(39) & Chr(39) & grwResultado.DataKeys(_rowIndex).Values(1).ToString() & Chr(39) & Chr(39) & " <br> <i class='fa fa-edit'></i> Registrar Mensajes Informativos"
                    Me.pnlLista.Visible = False
                    Me.pnlTramiteVirtual.Visible = True
                    Me.pnlFlujoTramite.Visible = False
                    Me.pnlCarreraAsociada.Visible = False
                    Me.pnlMensajeInformativo.Visible = True
                    Me.pnlMensajeInformativoLista.Visible = True
                    Me.pnlMensajeInformativoRegistro.Visible = False



                    Me.pnlReglas.Visible = False
                    Me.pnlReglasLista.Visible = False

                    Me.hdUpd.Value = 0
                    Me.hdPaso.Value = "4"
                    _codigo_ctr = grwResultado.DataKeys(_rowIndex).Values(0).ToString() ' codigo_ctr
                    Me.hdCtr.Value = Encriptar(_codigo_ctr.ToString)
                    mt_ListarMensajeInformativo()

                Case 5

                    Me.tituloPaso.InnerHtml = Chr(39) & Chr(39) & grwResultado.DataKeys(_rowIndex).Values(1).ToString() & Chr(39) & Chr(39) & " <br> <i class='fa fa-list-ol'></i> Reglas"
                    Me.pnlLista.Visible = False
                    Me.pnlTramiteVirtual.Visible = True
                    Me.pnlFlujoTramite.Visible = False
                    Me.pnlCarreraAsociada.Visible = False
                    Me.pnlMensajeInformativo.Visible = False
                    Me.pnlMensajeInformativoLista.Visible = False
                    Me.pnlMensajeInformativoRegistro.Visible = False

                    Me.pnlReglas.Visible = True
                    Me.pnlReglasLista.Visible = True



                    Me.hdUpd.Value = 0
                    Me.hdPaso.Value = "5"
                    _codigo_ctr = grwResultado.DataKeys(_rowIndex).Values(0).ToString() ' codigo_ctr
                    Me.hdCtr.Value = Encriptar(_codigo_ctr.ToString)
                    mt_ListarReglas()

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub mt_LimpiarFlujoTramite()

        Me.Hdftr.Value = String.Empty
        Me.ddlFuncion_ft.SelectedIndex = 0
        Me.ddlOrden_ft.SelectedIndex = 0
        Me.chkVerDatosAcad_ft.Checked = False
        Me.chkVerDatosAdm_ft.Checked = False
        Me.chkProcesa_ft.Checked = False
        Me.txtproceso_ft.Text = String.Empty
        Me.chkEstado_ft.Checked = True
        Me.chkEmailRechazo_ft.Checked = False
        Me.chkEmailAprobacion_ft.Checked = False
        Me.chkEmailPlanEstudio_ft.Checked = False
        Me.chkEmailPrecioCredito_ft.Checked = False
        Me.chkEmailGyt_ft.Checked = False
        Me.chkmsnrechazar_ft.Checked = False
        Me.chkmsnaprobacion_ft.Checked = False
        Me.chkEvaluaotroPer_ft.Checked = False

    End Sub

    Private Sub mt_LimpiarMensajeInformativo()
        Me.Hdmctr.Value = String.Empty
        Me.txtDescripcion_msjinfo.Text = String.Empty
        Me.ddlOrden_msjinfo.SelectedIndex = 0
        Me.chkActivo_msjinfo.Checked = True

    End Sub

    Private Function mt_GuardarPaso4MensajeInformativo() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim tipoOperacion As String = ""
            Dim CLSMsjInfo As New clsMensajeInformativo

            With CLSMsjInfo
                'Response.Write("C ctr: " & Me.hdCtr.Value)
                If Me.Hdmctr.Value = "" Or Me.Hdmctr.Value = "0" Then
                    .tipooperacion = "I"
                    ._codigo_mctr = 0
                Else
                    .tipooperacion = "A"
                    ._codigo_mctr = fnDevuelveNumEntero(Desencriptar(Me.Hdmctr.Value))
                End If

                ._codigo_ctr = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value))
                ._descripcion = Me.txtDescripcion_msjinfo.Text
                ._orden = Me.ddlOrden_msjinfo.SelectedValue
                If Me.chkActivo_msjinfo.Checked Then
                    ._activo = 1
                Else
                    ._activo = 0
                End If

                'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'Dim myObjectJson As String = javaScriptSerializer.Serialize(CLSctr)
                'Response.Clear()
                'Response.ContentType = "application/json; charset=utf-8"
                'Response.Write(myObjectJson)
                'Response.[End]()

            End With

            'tipoOperacion = CLSMsjInfo.tipooperacion
            'CLSMsjInfo.tipooperacion = "2"

            If CLSMsjInfo.fnExisteOrden() Then
                '   CLSMsjInfo.tipooperacion = tipoOperacion
                rpta = CLSMsjInfo.Registrar()
                If rpta Then
                    ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                    Call mt_ShowMessage("Mensaje informativo registrado con &eacute;xito", MessageType.Success)
                Else
                    rpta = False
                    Call mt_ShowMessage("Error al registrar mensaje informativo", MessageType.Error)
                    'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
                End If
            Else
                Call mt_ShowMessage("Ya existe un mensaje en el orden " & CLSMsjInfo._orden, MessageType.Warning)
            End If

            ''Response.Write("C ctr: " & Me.hdCtr.Value)

            CLSMsjInfo = Nothing

            Return rpta
        Catch ex As Exception
            Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function mt_GuardarPaso2FlujoTramite() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName

            Dim CLSftr As New clsFLujoTramite

            With CLSftr
                'Response.Write("C ctr: " & Me.hdCtr.Value)
                If Me.Hdftr.Value = "" Or Me.Hdftr.Value = "0" Then
                    .tipooperacion = "I"
                    ._codigo_ftr = 0
                Else
                    .tipooperacion = "A"
                    ._codigo_ftr = fnDevuelveNumEntero(Desencriptar(Me.Hdftr.Value))
                End If

                ._codigo_ctr = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value))
                ._codigo_apl = 64
                ._codigo_tfu = Me.ddlFuncion_ft.SelectedValue
                ._orden_ftr = Me.ddlOrden_ft.SelectedValue
                ._verDetAcad = Me.chkVerDatosAcad_ft.Checked
                ._verDetAdm = Me.chkVerDatosAdm_ft.Checked
                If Me.chkProcesa_ft.Checked Then
                    ._accionUrl = 1
                Else
                    ._accionUrl = 0
                End If
                ._proceso = Me.txtproceso_ft.Text
                ._activo = Me.chkEstado_ft.Checked
                ._tieneEmailRechazo = Me.chkEmailRechazo_ft.Checked
                ._tieneEmailAprobacion = Me.chkEmailAprobacion_ft.Checked
                ._tieneEmailPlanEstudio = Me.chkEmailPlanEstudio_ft.Checked
                ._tieneEmailPrecioCredito = Me.chkEmailPrecioCredito_ft.Checked
                ._tieneEmailGyT = Me.chkEmailGyt_ft.Checked
                ._tieneMsnTextoRechazo = Me.chkmsnrechazar_ft.Checked
                ._tieneMsnTextoAprobacion = Me.chkmsnaprobacion_ft.Checked
                ._tieneEvaluacionPersonal = Me.chkEvaluaotroPer_ft.Checked

                ._codigo_per = CInt(Session("id_per"))
                ._ip = ip
                ._ordenador = host

                'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'Dim myObjectJson As String = javaScriptSerializer.Serialize(CLSctr)
                'Response.Clear()
                'Response.ContentType = "application/json; charset=utf-8"
                'Response.Write(myObjectJson)
                'Response.[End]()

            End With


            If CLSftr.fnExisteOrden() Then
                Me.Hdftr.Value = CLSftr.Registrar()
                ''Response.Write("C ctr: " & Me.hdCtr.Value)
                If fnDevuelveNumEntero(Me.Hdftr.Value) > 0 Then
                    rpta = True
                    Me.Hdftr.Value = Encriptar(Me.Hdftr.Value)
                    ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                    Call mt_ShowMessage("Flujo del Tr&aacute;mite registrado con &eacute;xito", MessageType.Success)
                Else
                    rpta = False
                    Call mt_ShowMessage("Error al registrar tr&aacute;mite", MessageType.Error)
                    'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
                End If
            Else
                Call mt_ShowMessage("Ya existe un flujo trámite en el orden " & CLSftr._orden_ftr, MessageType.Warning)
            End If



            CLSftr = Nothing

            Return rpta

        Catch ex As Exception
            Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function mt_GuardarPaso1ConceptoTramite() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
            Dim dt As New Data.DataTable
            Dim CLSctr As New clsConceptoTramite

            With CLSctr
                'Response.Write("C ctr: " & Me.hdCtr.Value)

                If Me.hdCtr.Value = "" Or Me.hdCtr.Value = "0" Then
                    .tipooperacion = "I"
                    ._codigoCtr = 0
                Else
                    .tipooperacion = "A"
                    ._codigoCtr = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value))
                End If

                ._codigoSco = fnDevuelveNumEntero(Me.txtServicioConceptoCod.Value)
                ._precioCtr = fnDevuelveNumDecimal(Me.txtPrecio.Text)
                ._cantMaxCtr = Me.ddlCantidad.SelectedValue
                ._descripcionCtr = Me.txtTramite.Text
                ._ubicacionCtr = Me.txtUbicacion.Text
                ._codigoTest = Me.ddlTipoEstudio.SelectedValue
                ._tieneRequisito = Me.chkTieneRequisitos.Checked
                ._tieneSolicitudVirtual = Me.ddlSolVirtual.SelectedValue
                ._mostrar = Me.chkActivar.Checked
                ._esEgresado = Me.ddlEgresado.SelectedValue
                ._codigo_tctr = Me.ddlTipoTramite.SelectedValue
                ._codigoAct = Me.ddlActividad.SelectedValue
                ._tieneReglas = Me.chkTieneReglas.Checked
                ._tieneFlujo = Me.chkTieneFlujo.Checked
                ._refprecio_ctr = fnDevuelveNumDecimal(Me.txtPrecioScoRef.Text)
                ._refcodigo_sco = fnDevuelveNumEntero(Me.txtServicioConceptoCodRef.Value)
                ._tieneCarreraProfesionalAsociada = Me.chkTieneCarrerasAsociadas.Checked
                ._estadoAlu = Me.chkEstadoActivo.Checked
                ._rutaImg = ""
                ._codigo_cco = fnDevuelveNumEntero(Me.txtCentroCostosCod.Value)
                ._tieneArchivo = Me.chkTieneArchivo.Checked
                ._tieneEntrega = Me.chkTieneEntrega.Checked
                ._tieneNotaAbonoAutomatica = Me.chkTieneNotaAbono.Checked
                ._tieneMensajeInformativo = Me.chkTieneMensajeInformativo.Checked
                ._codigo_tdg = fnDevuelveNumEntero(Me.ddlTieneDGyT.SelectedValue)
                ._refcodigo_cco = fnDevuelveNumEntero(Me.txtCentroCostosCodRef.Value)

                ._codigo_per = CInt(Session("id_per"))
                ._ip = ip
                ._ordenador = host


                'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
                'Dim myObjectJson As String = javaScriptSerializer.Serialize(CLSctr)
                'Response.Clear()
                'Response.ContentType = "application/json; charset=utf-8"
                'Response.Write(myObjectJson)
                'Response.[End]()

            End With



            dt = CLSctr.ValidarRegistro()
            Dim rptavalidacion As Boolean
            Dim msgvalidacion As String
            rptavalidacion = dt.Rows(0).Item("rpta")
            msgvalidacion = dt.Rows(0).Item("msg")

            If rptavalidacion Then

                Me.hdCtr.Value = CLSctr.Registrar()

                If fnDevuelveNumEntero(Me.hdCtr.Value) > 0 Then
                    rpta = True
                    Me.hdCtr.Value = Encriptar(Me.hdCtr.Value)

                    ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                    Call mt_ShowMessage("Tr&aacute;mite registrado con &eacute;xito", MessageType.Success)
                Else
                    rpta = False
                    Call mt_ShowMessage("Error al registrar tr&aacute;mite", MessageType.Error)
                    'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
                End If
            Else
                rpta = False
                Call mt_ShowMessage(msgvalidacion, MessageType.Error)
            End If

            CLSctr = Nothing

            Return rpta
        Catch ex As Exception

            'Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function




    Protected Sub btnEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ctr As String = Desencriptar(button.Attributes("ctr").ToString)


            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('editar','');</script>")
            'Call mt_LlenarFormulario()
            'Call mt_CargarDatos()

            'Call ddlCarreraProf_SelectedIndexChanged(sender, e)
            'Call mt_MostrarDetalle(cpf)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    'Protected Sub btnValidar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidar.ServerClick
    '    Try
    '        'Dim valid As Generic.Dictionary(Of String, String) = fc_Validar()

    '        'If valid.Item("rpta") = 1 Then
    '        '    Me.divAlertModal.Visible = False
    '        '    Me.validar.Value = "1"
    '        '    updMensaje.Update()
    '        'Else
    '        '    ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
    '        '    ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)

    '        '    Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
    '        'End If
    '    Catch ex As Exception
    '        Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
    '    End Try
    'End Sub

    'Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
    '    Try
    '        'Dim dt As New Data.DataTable
    '        'Dim flag As Boolean = False
    '        'Dim cod_cpf As Object = IIf(String.IsNullOrEmpty(Session("codigo_cpf")), "", Session("codigo_cpf"))
    '        'Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "", Session("codigo_com"))

    '        'C.IniciarTransaccion()


    '        'C.TerminarTransaccion()

    '        'If flag Then
    '        '    Call mt_MostrarDetalle(cod_cpf)

    '        '    If nuevo Then
    '        '        Call mt_ShowMessage("Registro satisfactorio del comité", MessageType.Success)
    '        '    Else
    '        '        Call mt_ShowMessage("Actualización satisfactoria del comité", MessageType.Success)
    '        '    End If
    '        'End If
    '    Catch ex As Exception
    '        C.AbortarTransaccion()
    '        Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
    '    End Try
    'End Sub



    'Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
    '    Try
    '        'Call mt_MostrarDetalle(Me.ddlCarreraProf.SelectedValue)
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub




    Protected Sub OnUpdate(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            'Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            'Dim ddlDocente As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlDocente"), DropDownList)
            'Dim ddlRol As DropDownList = CType(grwComite.Rows(row.RowIndex).FindControl("ddlRol"), DropDownList)

            'Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)

            'If valid.Item("rpta") = 1 Then
            '    dt.Rows(row.RowIndex)("codigo_per") = ddlDocente.SelectedValue
            '    dt.Rows(row.RowIndex)("nombre_per") = ddlDocente.SelectedItem.Text
            '    dt.Rows(row.RowIndex)("rol_mie") = ddlRol.SelectedItem.Text
            '    ViewState("dt") = dt
            '    grwComite.EditIndex = -1
            '    Call BindGrid()
            'Else
            '    Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            'End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        End Try
    End Sub

    Protected Sub OnCancel(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub OnDelete(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim valor As String = "1"
            'Dim rpta As String = ""
            'Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            'Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
            'Dim codigo_mie As String = grwComite.DataKeys(row.RowIndex).Item("codigo_mie").ToString

            'If Not String.IsNullOrEmpty(codigo_mie) AndAlso Not codigo_mie.Equals("0") Then
            '    Dim dtRpta As New Data.DataTable
            '    Dim codigo_com As Object
            '    codigo_com = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

            '    C.AbrirConexion()
            '    dtRpta = C.TraerDataTable("COM_VerificarComiteCurricularMiembros", codigo_com)
            '    C.CerrarConexion()

            '    valor = dtRpta.Rows(0).Item(0).ToString
            '    rpta = dtRpta.Rows(0).Item(1).ToString
            '    dtRpta.Dispose()

            '    If valor.Equals("1") Then
            '        Dim dtElim As Data.DataTable = TryCast(ViewState("dtElim"), Data.DataTable)
            '        dtElim.Rows.Add(codigo_mie, codigo_com)
            '        ViewState("dtElim") = dtElim
            '    Else
            '        Call mt_ShowMessage(rpta, MessageType.Info, True)
            '        Return
            '    End If
            'End If

            'If valor.Equals("1") Then
            '    dt.Rows.RemoveAt(row.RowIndex)
            '    ViewState("dt") = dt
            '    grwComite.EditIndex = -1
            '    Call BindGrid()
            'End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
        End Try
    End Sub

    Protected Sub OnNew(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)
            'Dim ddlDocente As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewDocente"), DropDownList)
            'Dim ddlRol As DropDownList = CType(grwComite.FooterRow.FindControl("ddlNewRol"), DropDownList)

            'Dim valid As Generic.Dictionary(Of String, String) = fc_ValidarMiembros(ddlRol.SelectedItem.Text, ddlDocente.SelectedItem.Text)
            'Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), 0, Session("codigo_com"))

            'If valid.Item("rpta") = 1 Then
            '    If String.IsNullOrEmpty(dt.Rows(0).Item(1).ToString) Then
            '        dt.Rows.RemoveAt(0)
            '    End If

            '    dt.Rows.Add(cod_com, ddlDocente.SelectedValue.ToString, ddlDocente.SelectedItem.Text, 0, ddlRol.SelectedItem.Text, 1)
            '    ViewState("dt") = dt
            '    grwComite.EditIndex = -1
            '    Call BindGrid()
            'Else
            '    Call mt_ShowMessage(valid.Item("msg"), MessageType.Info, True)
            'End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Info, True)
        End Try
    End Sub

#End Region

#Region "Métodos"

    Protected Sub ddlTipoTramiteBsq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoTramiteBsq.SelectedIndexChanged
        Call mt_ListarConceptoTramite()
    End Sub

    Protected Sub ddlTipoEstudioBsq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudioBsq.SelectedIndexChanged
        Call mt_ListarConceptoTramite()
    End Sub

    Protected Sub ddlEgresadoBsq_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEgresadoBsq.SelectedIndexChanged
        Call mt_ListarConceptoTramite()
    End Sub
    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)

        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")

    End Sub


    Protected Sub btnQuitarServicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitarServicio.Click

        Me.txtServicioConceptoCod.Value = ""
        Me.txtServicioConcepto.Text = ""
    End Sub

    Protected Sub btnQuitarServicioRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitarServicioRef.Click

        Me.txtServicioConceptoCodRef.Value = ""
        Me.txtServicioConceptoRef.Text = ""
    End Sub

    Protected Sub btnQuitarCentroCostos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitarCentroCostos.Click

        Me.txtCentroCostosCod.Value = ""
        Me.txtCentroCostos.Text = ""
    End Sub
    Protected Sub btnQuitarCentroCostosRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuitarCentroCostosRef.Click

        Me.txtCentroCostosCodRef.Value = ""
        Me.txtCentroCostosRef.Text = ""
    End Sub

    Protected Sub chkActivar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActivar.CheckedChanged
        Try

            If Me.chkActivar.Checked Then
                Me.txtAprobadoPorPersonal.Text = Session("nombreper") & " (" & Session("perlogin") & ")"
            Else
                Me.txtAprobadoPorPersonal.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub mt_CargarSemestre()
        Try
            'Dim dt As New Data.DataTable("data")

            'C.AbrirConexion()
            'dt = C.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            'C.CerrarConexion()

            'ddlSemestre.DataSource = dt
            'ddlSemestre.DataTextField = "descripcion_Cac"
            'ddlSemestre.DataValueField = "codigo_Cac"
            'ddlSemestre.DataBind()

            'dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)

        End Try
    End Sub

    Private Sub mt_CargarCarreras()
        Try
            'Dim dt As New Data.DataTable("Data")
            'C.AbrirConexion()

            'dt = C.TraerDataTable("COM_ListarCarreraProfesional", cod_user, cod_ctf)

            'Me.ddlCarreraProf.DataSource = dt
            'Me.ddlCarreraProf.DataValueField = "codigo_Cpf"
            'Me.ddlCarreraProf.DataTextField = "nombre_Cpf"
            'Me.ddlCarreraProf.DataBind()

            'dt.Dispose()
            'C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarTipoTramite()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_TipoTramite_Listar", "1", 0)

            Me.ddlTipoTramiteBsq.DataSource = dt
            Me.ddlTipoTramiteBsq.DataValueField = "codigo_tctr"
            Me.ddlTipoTramiteBsq.DataTextField = "nombre_tctr"
            Me.ddlTipoTramiteBsq.DataBind()

            Me.ddlTipoTramite.DataSource = dt
            Me.ddlTipoTramite.DataValueField = "codigo_tctr"
            Me.ddlTipoTramite.DataTextField = "nombre_tctr"
            Me.ddlTipoTramite.DataBind()





            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDenominacionGyT()
        Try

            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_ConsultarTipoDenominacion", "GYT", "")

            Me.ddlTieneDGyT.DataSource = dt
            Me.ddlTieneDGyT.DataValueField = "codigo"
            Me.ddlTieneDGyT.DataTextField = "nombre"
            Me.ddlTieneDGyT.DataBind()

            dt.Dispose()
            C.CerrarConexion()

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub


    Private Sub mt_CargarTipoEstudio()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_TipoEstudio_Listar", "1", 0)

            Me.ddlTipoEstudioBsq.DataSource = dt
            Me.ddlTipoEstudioBsq.DataValueField = "codigo_test"
            Me.ddlTipoEstudioBsq.DataTextField = "descripcion_test"
            Me.ddlTipoEstudioBsq.DataBind()

            'dt = C.TraerDataTable("TRL_TipoEstudio_Listar", "2", 0)


            Me.ddlTipoEstudio.DataSource = dt
            Me.ddlTipoEstudio.DataValueField = "codigo_test"
            Me.ddlTipoEstudio.DataTextField = "descripcion_test"
            Me.ddlTipoEstudio.DataBind()

            Me.ddlTipoEstudioBsqmd.DataSource = dt
            Me.ddlTipoEstudioBsqmd.DataValueField = "codigo_test"
            Me.ddlTipoEstudioBsqmd.DataTextField = "descripcion_test"
            Me.ddlTipoEstudioBsqmd.DataBind()

            dt.Dispose()
            C.CerrarConexion()

            Me.ddlTipoEstudioBsqmd.Items.RemoveAt(0)

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarActividadCronograma()
        Try
            Dim dt As New Data.DataTable("Data")
            C.AbrirConexion()

            dt = C.TraerDataTable("TRL_ActividadCronograma_Listar", "1", 0, "")


            Me.ddlActividad.DataSource = dt
            Me.ddlActividad.DataValueField = "codigo_Act"
            Me.ddlActividad.DataTextField = "descripcion_Act"
            Me.ddlActividad.DataBind()

            dt.Dispose()
            C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub


    Private Sub mt_MostrarDetalle(ByVal codigo_cpf As String)
        Try
            'Dim dt As New Data.DataTable("Data")
            'C.AbrirConexion()

            'codigo_cpf = IIf(String.IsNullOrEmpty(codigo_cpf), "", codigo_cpf)
            'Session("codigo_cpf") = codigo_cpf

            'dt = C.TraerDataTable("COM_ListarComiteCurricular", codigo_cpf)

            'Me.grwResultado.DataSource = dt
            'Me.grwResultado.DataBind()
            'dt.Dispose()
            'C.CerrarConexion()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Function mt_EstructuraDTconceptoTramite() As data.datatable

        Try
            Dim dtedit As New Data.DataTable
            dtedit.Columns.Add("tipooperacion")
            dtedit.Columns.Add("codigo_ctr")
            dtedit.Columns.Add("descripcion_ctr")
            dtedit.Columns.Add("ubicacion_ctr")
            dtedit.Columns.Add("codigo_tctr")
            dtedit.Columns.Add("codigo_test")
            dtedit.Columns.Add("esEgresado")
            dtedit.Columns.Add("tieneSolicitudVirtual")
            dtedit.Columns.Add("cantMax_ctr")
            dtedit.Columns.Add("precio_ctr")
            dtedit.Columns.Add("codigo_sco")
            dtedit.Columns.Add("codigo_cco")
            dtedit.Columns.Add("codigo_act")
            dtedit.Columns.Add("refcodigo_sco")
            dtedit.Columns.Add("refcodigo_cco")
            dtedit.Columns.Add("refprecio_ctr")
            dtedit.Columns.Add("codigo_tdg")
            dtedit.Columns.Add("estadoAlu")
            dtedit.Columns.Add("tieneReglas")
            dtedit.Columns.Add("tieneFlujo")
            dtedit.Columns.Add("tieneCarreraProfesionalAsociada")
            dtedit.Columns.Add("tieneArchivo")
            dtedit.Columns.Add("tieneEntrega")
            dtedit.Columns.Add("tieneNotaAbonoAutomatica")
            dtedit.Columns.Add("tieneMensajeInformativo")
            dtedit.Columns.Add("tieneRequisito")
            dtedit.Columns.Add("mostrar")


            Return dtedit
        Catch ex As Exception
            Response.Write(ex.message)
            Return Nothing
        End Try
    End Function

    Private Sub mt_MostrarConceptoTramite()

        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "2"

            Dim codigo_ctr As Integer = CInt(Desencriptar(Me.hdCtr.Value))
            Dim codigo_test As Integer = 0
            Dim esEgresado As String = ""
            Dim codigo_tctr As Integer = 0
            Dim descripcion_ctr As String = ""

            Dim dtedit As New Data.DataTable
            dtedit = mt_EstructuraDTconceptoTramite()
            Dim fila As Data.DataRow = dtedit.NewRow()



            Dim cls As New clsConceptoTramite

            With cls
                .tipooperacion = tipo
                ._codigoCtr = codigo_ctr
                ._codigoTest = codigo_test
                ._codigo_tctr = codigo_tctr
                ._descripcionCtr = descripcion_ctr
            End With

            dt = cls.Consultar()

            If dt.Rows.Count = 1 Then
                With dt.Rows(0)
                    'oeTramiteEditar._codigoCtr = codigo_ctr
                    fila("codigo_ctr") = codigo_ctr

                    Me.txtTramite.Text = .Item("descripcion_ctr")
                    'oeTramiteEditar._descripcionCtr = .Item("descripcion_ctr").ToString
                    fila("descripcion_ctr") = .Item("descripcion_ctr")

                    Me.txtUbicacion.Text = .Item("ubicacion_ctr").ToString
                    'oeTramiteEditar._ubicacionCtr = .Item("ubicacion_ctr").ToString
                    fila("ubicacion_ctr") = .Item("ubicacion_ctr")

                    Me.ddlTipoTramite.SelectedValue = .Item("codigo_tctr")
                    'oeTramiteEditar._codigo_tctr = .Item("codigo_tctr")
                    fila("codigo_tctr") = .Item("codigo_tctr")

                    Me.ddlTipoEstudio.SelectedValue = .Item("codigo_test")
                    'oeTramiteEditar._codigoTest = .Item("codigo_test")
                    fila("codigo_test") = .Item("codigo_test")


                    Me.ddlEgresado.SelectedValue = .Item("esEgresado")
                    'oeTramiteEditar._esEgresado = .Item("esEgresado")
                    fila("esEgresado") = .Item("esEgresado")

                    Me.ddlSolVirtual.SelectedValue = .Item("tieneSolicitudVirtual")
                    'oeTramiteEditar._tieneSolicitudVirtual = .Item("tieneSolicitudVirtual")
                    fila("tieneSolicitudVirtual") = .Item("tieneSolicitudVirtual")

                    Me.ddlCantidad.SelectedValue = .Item("cantMax_ctr")
                    'oeTramiteEditar._cantMaxCtr = .Item("cantMax_ctr")
                    fila("cantMax_ctr") = .Item("cantMax_ctr")

                    Me.txtPrecio.Text = .Item("precio_ctr")
                    'oeTramiteEditar._precioCtr = .Item("precio_ctr")
                    fila("precio_ctr") = .Item("precio_ctr")

                    Me.txtServicioConceptoCod.Value = .Item("codigo_sco")
                    'oeTramiteEditar._codigoSco = .Item("codigo_sco")
                    fila("codigo_sco") = .Item("codigo_sco")


                    Me.txtServicioConcepto.Text = .Item("descripcion_Sco")

                    Me.txtCentroCostosCod.Value = .Item("codigo_cco")
                    'oeTramiteEditar._codigo_cco = .Item("codigo_cco")
                    fila("codigo_cco") = .Item("codigo_cco")

                    Me.txtCentroCostos.Text = .Item("descripcion_cco")

                    Me.ddlActividad.SelectedValue = .Item("codigo_act")
                    'oeTramiteEditar._codigoAct = .Item("codigo_act")
                    fila("codigo_act") = .Item("codigo_act")

                    Me.txtServicioConceptoCodRef.Value = .Item("refcodigo_sco")
                    'oeTramiteEditar._refcodigo_sco = .Item("refcodigo_sco")
                    fila("refcodigo_sco") = .Item("refcodigo_sco")

                    Me.txtServicioConceptoRef.Text = .Item("descripcion_Scoref")

                    Me.txtCentroCostosCodRef.Value = .Item("refcodigo_cco")
                    'oeTramiteEditar._refcodigo_cco = .Item("refcodigo_cco")
                    fila("refcodigo_cco") = .Item("refcodigo_cco")

                    Me.txtCentroCostosRef.Text = .Item("descripcion_Ccoref")

                    Me.txtPrecioScoRef.Text = .Item("refprecio_ctr")
                    'oeTramiteEditar._precioCtr = .Item("refprecio_ctr")
                    fila("refprecio_ctr") = .Item("refprecio_ctr")

                    If .Item("codigo_tctr") = 3 Then
                        mt_CargarDenominacionGyT()
                        Me.ddlTieneDGyT.SelectedValue = .Item("codigo_tdg")
                        'oeTramiteEditar._codigo_tdg = .Item("codigo_tdg")
                        fila("codigo_tdg") = .Item("codigo_tdg")
                        divGyT.Visible = True
                    Else
                        '#ddl Grado y Titulo{
                        Me.ddlTieneDGyT.Items.Clear()
                        Me.ddlTieneDGyT.Items.Add("")
                        Me.ddlTieneDGyT.DataBind()
                        divGyT.Visible = False
                        '}#ddl Grado y Titulo
                        fila("codigo_tdg") = 0
                    End If

                    If .Item("estadoAlu") = 1 Then
                        Me.chkEstadoActivo.Checked = True
                        'oeTramiteEditar._estadoAlu = True
                        fila("estadoAlu") = True
                    Else
                        Me.chkEstadoActivo.Checked = False
                        'oeTramiteEditar._estadoAlu = False
                        fila("estadoAlu") = False
                    End If
                    'Response.Write(.Item("tieneReglas"))

                    If .Item("tieneReglas") = 1 Then
                        Me.chkTieneReglas.Checked = True
                        'oeTramiteEditar._tieneReglas = True
                        fila("tieneReglas") = True
                    Else
                        Me.chkTieneReglas.Checked = False
                        'oeTramiteEditar._tieneReglas = False
                        fila("tieneReglas") = False
                    End If

                    If .Item("tieneFlujo") = 1 Then
                        Me.chkTieneFlujo.Checked = True
                        'oeTramiteEditar._tieneFlujo = True
                        fila("tieneFlujo") = True
                    Else
                        Me.chkTieneFlujo.Checked = False
                        'oeTramiteEditar._tieneFlujo = False
                        fila("tieneFlujo") = False
                    End If

                    If .Item("tieneCarreraProfesionalAsociada") = 1 Then
                        Me.chkTieneCarrerasAsociadas.Checked = True
                        'oeTramiteEditar._tieneCarreraProfesionalAsociada = True
                        fila("tieneCarreraProfesionalAsociada") = True
                    Else
                        Me.chkTieneCarrerasAsociadas.Checked = False
                        'oeTramiteEditar._tieneCarreraProfesionalAsociada = False
                        fila("tieneCarreraProfesionalAsociada") = False
                    End If

                    If .Item("tieneArchivo") = 1 Then
                        Me.chkTieneArchivo.Checked = True
                        'oeTramiteEditar._tieneArchivo = True
                        fila("tieneArchivo") = True
                    Else
                        Me.chkTieneArchivo.Checked = False
                        'oeTramiteEditar._tieneArchivo = False
                        fila("tieneArchivo") = False
                    End If

                    If .Item("tieneEntrega") = 1 Then
                        Me.chkTieneEntrega.Checked = True
                        'oeTramiteEditar._tieneEntrega = True
                        fila("tieneEntrega") = True
                    Else
                        Me.chkTieneEntrega.Checked = False
                        'oeTramiteEditar._tieneEntrega = False
                        fila("tieneEntrega") = False
                    End If

                    If .Item("tieneNotaAbonoAutomatica") = 1 Then
                        Me.chkTieneNotaAbono.Checked = True
                        'oeTramiteEditar._tieneNotaAbonoAutomatica = True
                        fila("tieneNotaAbonoAutomatica") = True
                    Else
                        Me.chkTieneNotaAbono.Checked = False
                        'oeTramiteEditar._tieneNotaAbonoAutomatica = False
                        fila("tieneNotaAbonoAutomatica") = False
                    End If

                    If .item("tieneRequisito") = 1 Then
                        Me.chkTieneRequisitos.checked = True
                        fila("tieneRequisito") = True
                    Else
                        Me.chkTieneRequisitos.checked = False
                        fila("tieneRequisito") = False
                    End If

                    If .Item("tieneMensajeInformativo") = 1 Then
                        Me.chkTieneMensajeInformativo.Checked = True
                        'oeTramiteEditar._tieneMensajeInformativo = True
                        fila("tieneMensajeInformativo") = True
                    Else
                        Me.chkTieneMensajeInformativo.Checked = False
                        'oeTramiteEditar._tieneMensajeInformativo = False
                        fila("tieneMensajeInformativo") = False
                    End If

                    If .Item("mostrar") = 1 Then
                        Me.chkActivar.Checked = True
                        'oeTramiteEditar._mostrar = True
                        fila("mostrar") = True
                        Me.txtAprobadoPorPersonal.Text = .Item("personal")
                    Else
                        Me.chkActivar.Checked = False
                        'oeTramiteEditar._mostrar = False
                        fila("mostrar") = False
                    End If
                    dtedit.rows.add(fila)

                    Session("TramiteEditar") = dtedit




                End With
            End If

            cls = Nothing
            Me.txtTramiteBsq.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    Private Sub mt_ListarConceptoTramite()

        Try
            Dim cls As New clsConceptoTramite
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"
            Dim codigo_ctr As Integer = 0
            Dim codigo_test As Integer = ddlTipoEstudioBsq.SelectedValue
            Dim egresado As String = ""
            egresado = IIf(ddlEgresadoBsq.SelectedValue = "", "%", ddlEgresadoBsq.SelectedValue)
            Dim codigo_tctr As Integer = ddlTipoTramiteBsq.SelectedValue
            Dim descripcion_ctr As String = txtTramiteBsq.Text.Trim
            Dim estado As String = ""
            estado = IIf(Me.chkMostrarActivoCtr.Checked, "1", "0")


            With cls
                .tipooperacion = tipo
                ._codigoCtr = codigo_ctr
                ._codigoTest = codigo_test
                ._codigo_tctr = codigo_tctr
                ._descripcionCtr = descripcion_ctr
                .P_estado = estado
                .P_egresado = egresado
            End With

            dt = cls.Consultar()

            Me.grwResultado.DataSource = dt
            Me.grwResultado.DataBind()
            dt.Dispose()

            cls = Nothing
            Me.txtTramiteBsq.Focus()

        Catch ex As Exception
            Response.Write(ex.Message & "--" & ex.StackTrace)
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub
    'mt_ListarCentroCosto


    Private Sub mt_ListarServicoConcepto(ByVal ref As Boolean)


        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"

            Dim codigo_sco As Integer = 0
            Dim descripcion_Sco As String = txtServicioBsq.Text.Trim
            If txtCodServicio.Text.Trim <> "" Then
                codigo_sco = CInt(txtCodServicio.Text.Trim)
            End If


            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_ServicioConcepto_Listar", tipo, codigo_sco, descripcion_Sco, ref)
            Me.grwServicio.DataSource = dt
            Me.grwServicio.DataBind()
            dt.Dispose()

            C.CerrarConexion()


            Me.grwServicio.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try

    End Sub

    Private Sub mt_ListarCentroCosto(ByVal ref As Boolean)

        Try
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"

            Dim codigo_cco As Integer = 0
            Dim descripcion_cco As String = txtCentroCostoBsq.Text.Trim

            If Me.txtCodCentroCosto.Text.Trim <> "" Then
                codigo_cco = CInt(Me.txtCodCentroCosto.Text.Trim)
            End If

            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_CentroCostos_Listar", tipo, codigo_cco, descripcion_cco, ref)
            Me.grwCentroCosto.DataSource = dt
            Me.grwCentroCosto.DataBind()
            dt.Dispose()

            C.CerrarConexion()

            Me.grwCentroCosto.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Dim dt As New Data.DataTable("data")
        Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), DBNull.Value, Session("codigo_com"))

        Try
            C.AbrirConexion()
            dt = C.TraerDataTable("COM_ListarMiembrosComiteCurricular", cod_com, cod_user)
            C.CerrarConexion()

            ViewState("dt") = dt
            Call BindGrid()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub BindGrid()
        Try
            'Dim dt As Data.DataTable = TryCast(ViewState("dt"), Data.DataTable)

            'If dt.Rows.Count > 0 Then
            '    grwComite.DataSource = dt
            '    grwComite.DataBind()
            'Else
            '    dt.Rows.Add(dt.NewRow())
            '    grwComite.DataSource = dt
            '    grwComite.DataBind()

            '    grwComite.Rows(0).Cells.Clear()

            '    'Dim totalColumns As Integer = grwComite.Rows(0).Cells.Count
            '    'grwComite.Rows(0).Cells.Clear()
            '    'grwComite.Rows(0).Cells.Add(New TableCell())
            '    'grwComite.Rows(0).Cells(0).ColumnSpan = totalColumns
            '    'grwComite.Rows(0).Cells(0).Style.Add("text-align", "center")
            '    'grwComite.Rows(0).Cells(0).Text = "No se ha registrado participantes"
            'End If

            'udpComite.Update()

            'Me.divAlertModal.Visible = False
            'Me.lblMensaje.InnerText = ""
            'updMensaje.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error, True)
        End Try
    End Sub


    Private Sub mt_LlenarFormulario()
        Try
            'Dim dt As New Data.DataTable("data")
            'Dim cod_com As Object = IIf(String.IsNullOrEmpty(Session("codigo_com")), "-1", Session("codigo_com"))

            'C.AbrirConexion()
            'dt = C.TraerDataTable("COM_ObtenerDatosComiteCurricular", cod_com, idTabla)
            'C.CerrarConexion()

            'spnFile.InnerText = "No se eligió resolución"

            'If dt.Rows.Count > 0 Then
            '    txtNombre.Text = dt.Rows(0).Item("nombre_com").ToString.Trim
            '    txtIniAprobacion.Text = dt.Rows(0).Item("fechaAprob_com").ToString.Trim
            '    txtFinAprobacion.Text = dt.Rows(0).Item("fechaTermino_com").ToString.Trim
            '    ddlSemestre.SelectedValue = dt.Rows(0).Item("codigo_cac").ToString.Trim
            '    txtNroDecreto.Text = dt.Rows(0).Item("nroDecreto_com").ToString.Trim
            '    spnFile.InnerText = dt.Rows(0).Item("archivo").ToString.Trim

            '    If spnFile.InnerText.Trim().Equals("No se eligió resolución") Then
            '        hf.Value = "0"
            '    Else
            '        hf.Value = "1"
            '    End If
            'End If

            'dt.Dispose()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlTipoTramite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoTramite.SelectedIndexChanged
        If Me.ddlTipoTramite.SelectedValue = 3 Then
            mt_CargarDenominacionGyT()
            divGyT.Visible = True
        Else
            Me.ddlTieneDGyT.Items.Clear()
            Me.ddlTieneDGyT.Items.Add("")
            Me.ddlTieneDGyT.DataBind()
            divGyT.Visible = False
        End If
    End Sub


    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefreshGrid()

        For Each _Row As GridViewRow In grwResultado.Rows
            grwResultado_RowDataBound(grwResultado, New GridViewRowEventArgs(_Row))
        Next

    End Sub


    Protected Sub btnServicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServicio.Click
        Me.pnlBuscarServicio.Visible = True
        Me.pnlRegistro.Visible = False
        Me.pnlTramiteVirtual.Visible = False
        Me.btnBuscarServicio.Visible = True
        Me.btnBuscarServicioRef.Visible = False
        ' ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('modalServicio');</script>")
        Me.txtServicioBsq.Focus()
    End Sub

    Protected Sub btnServicioRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnServicioRef.Click
        Me.pnlBuscarServicio.Visible = True
        Me.pnlRegistro.Visible = False
        Me.pnlTramiteVirtual.Visible = False
        Me.btnBuscarServicio.Visible = False
        Me.btnBuscarServicioRef.Visible = True
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('modalServicio');</script>")
        Me.txtServicioBsq.Focus()
    End Sub

    Protected Sub btnCostosRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCostosRef.Click
        Me.pnlBuscarCentroCosto.Visible = True
        Me.pnlRegistro.Visible = False
        Me.pnlTramiteVirtual.Visible = False
        Me.btnBuscarCentroCostoRef.Visible = True
        Me.btnBuscarCentroCosto.Visible = False

        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('modalCosto');</script>")
        Me.txtCentroCostoBsq.Focus()
    End Sub

    Protected Sub btnCostos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCostos.Click
        Me.pnlBuscarCentroCosto.Visible = True
        Me.pnlRegistro.Visible = False
        Me.pnlTramiteVirtual.Visible = False
        Me.btnBuscarCentroCostoRef.Visible = False
        Me.btnBuscarCentroCosto.Visible = True
        'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script>openModal('modalCosto');</script>")
        Me.txtCentroCostoBsq.Focus()
    End Sub


    Protected Sub btnBuscarServicio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarServicio.Click
        Me.btnBuscarServicio.Visible = True
        Me.btnBuscarServicioRef.Visible = False
        mt_ListarServicoConcepto(False)
        'btnServicio_Click(sender, e)
    End Sub
    Protected Sub btnBuscarServicioRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarServicioRef.Click
        Me.btnBuscarServicio.Visible = False
        Me.btnBuscarServicioRef.Visible = True
        mt_ListarServicoConcepto(True)
        'btnServicio_Click(sender, e)
    End Sub

    Protected Sub btnBuscarCentroCosto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCentroCosto.Click
        Me.btnBuscarServicio.Visible = True
        Me.btnBuscarServicioRef.Visible = False
        Me.btnBuscarCentroCostoRef.Visible = False
        Me.btnBuscarCentroCosto.Visible = True
        mt_ListarCentroCosto(False)
        'btnCostos_Click(sender, e)
    End Sub
    Protected Sub btnCancelarServicioRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarServicioRef.Click
        Me.pnlBuscarServicio.Visible = False
        Me.pnlRegistro.Visible = True
        Me.pnlTramiteVirtual.Visible = True
        'btnCostos_Click(sender, e)
    End Sub

    Protected Sub btnCancelarCentroCosto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarCentroCosto.Click
        Me.pnlBuscarCentroCosto.Visible = False
        Me.pnlRegistro.Visible = True
        Me.pnlTramiteVirtual.Visible = True
        'btnCostos_Click(sender, e)
    End Sub
    Protected Sub btnBuscarCentroCostoRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCentroCostoRef.Click
        Me.btnBuscarServicio.Visible = False
        Me.btnBuscarServicioRef.Visible = True
        Me.btnBuscarCentroCostoRef.Visible = True
        Me.btnBuscarCentroCosto.Visible = False
        mt_ListarCentroCosto(True)
        'btnCostos_Click(sender, e)
    End Sub


#Region "tieneCarreraAsociada"


    Protected Sub btnBuscarCarreraProfesional_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCarreraProfesional.Click
        mt_ListarCarreraProfesional()

    End Sub

    Protected Sub ddlTipoEstudioBsqmd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudioBsqmd.SelectedIndexChanged
        mt_ListarCarreraProfesional()
    End Sub

    Private Sub mt_ListarCarreraProfesional()
        Try
            Me.spTotalCpfAsoc.InnerHtml = "0"
            Dim dt As New Data.DataTable("Data")
            Dim tipo As String = "1"

            Dim codigo_test As Integer = fnDevuelveNumEntero(Me.ddlTipoEstudioBsqmd.SelectedValue)
            Dim codigo_ctr As Integer = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value))

            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_CarreraProfesional_Listar", tipo, 0, codigo_test, 0, codigo_ctr)
            Me.grwCarreraProfesional.DataSource = dt
            Me.grwCarreraProfesional.DataBind()
            dt.Dispose()

            C.CerrarConexion()

            Me.grwCentroCosto.Focus()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub chckchanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim contador As Integer = 0
        Dim chckheader As CheckBox = CType(grwCarreraProfesional.HeaderRow.FindControl("chkall"), CheckBox)
        'contador = 0
        For Each row As GridViewRow In grwCarreraProfesional.Rows
            Dim chckrw As CheckBox = CType(row.FindControl("chkElegir"), CheckBox)
            If chckheader.Checked = True Then
                chckrw.Checked = True
            Else
                chckrw.Checked = False
            End If
            If chckrw.Checked = True Then
                contador = contador + 1
                '  row.ControlStyle.BackColor = Drawing.Color.AntiqueWhite
                row.ControlStyle.Font.Bold = True
            Else
                '  row.ControlStyle.BackColor = Drawing.Color.White
                row.ControlStyle.Font.Bold = False
            End If
        Next
        'Me.lblContadorSeleccionado.Text = contador.ToString
    End Sub
    Protected Sub grwReglas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwReglas.RowDataBound
        Try

            Dim index As Integer = 0

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim _sel As String = grwReglas.DataKeys(e.Row.RowIndex).Values("sel").ToString() ' sel
                Dim sel As Integer = fnDevuelveNumEntero(Me.sptotalreglas.InnerHtml)

                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegirRegla")
                ''Response.Write(e.Row.FindControl("sel"))
                'checkAcceso.Checked = True

                If _sel > 0 Then
                    checkAcceso.Checked = True
                    sel = sel + 1
                Else
                    checkAcceso.Checked = False

                End If

                Me.sptotalreglas.InnerHtml = sel.ToString
            End If
        Catch ex As Exception
            Response.Write("grwReglas_RowDataBound : " & ex.Message & "--" & ex.StackTrace)
        End Try
    End Sub
    Protected Sub grwCarreraProfesional_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCarreraProfesional.RowDataBound
        Try

            Dim index As Integer = 0
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _sel As String = grwCarreraProfesional.DataKeys(e.Row.RowIndex).Values(2).ToString() ' sel
                Dim sel As Integer = fnDevuelveNumEntero(Me.spTotalCpfAsoc.InnerHtml)
                index = e.Row.RowIndex
                Dim checkAcceso As CheckBox
                checkAcceso = e.Row.FindControl("chkElegir")
                ' Response.Write(e.Row.FindControl("sel"))
                'checkAcceso.Checked = True

                If _sel > 0 Then
                    checkAcceso.Checked = True
                    sel = sel + 1
                Else
                    checkAcceso.Checked = False

                End If

                Me.spTotalCpfAsoc.InnerHtml = sel.ToString
            End If
        Catch ex As Exception
            Response.Write("grwCarreraProfesional_RowDataBound : " & ex.Message & "--" & ex.StackTrace)
        End Try
    End Sub

    Private Function mt_GuardarPaso3CarreraProfesional() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
            Dim dt As New Data.DataTable
            Dim filas As Integer = grwCarreraProfesional.Rows.Count
            Dim i As Integer = 0
            Dim Fila As GridViewRow
            Dim CLSctrcpf As New clsCarreraProfesional
            Dim ID As Integer
            Dim CPF As Integer
            Dim CTR As Integer = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value.ToString))
            With CLSctrcpf
                For i = 0 To filas - 1
                    Fila = Me.grwCarreraProfesional.Rows(i)
                    Dim clsDetCpf As New clsCarreraProfesional
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegir"), CheckBox).Checked

                    ID = fnDevuelveNumEntero(Me.grwCarreraProfesional.DataKeys(i).Values(2).ToString)
                    CPF = fnDevuelveNumEntero(Me.grwCarreraProfesional.DataKeys(i).Values(0).ToString)

                    With clsDetCpf



                        If ID > 0 Then
                            .tipooperacion = "A"

                        Else
                            .tipooperacion = "I"

                        End If
                        ._codigo_ctrcpf = ID
                        ._codigo_cpf = CPF
                        ._codigo_ctr = CTR
                        If (valor = True) Then
                            ._acceso = True
                        Else
                            ._acceso = False
                        End If



                    End With
                    CLSctrcpf.cpfasignada.Add(clsDetCpf)

                Next


            End With


            'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim myObjectJson As String = javaScriptSerializer.Serialize(CLSctrcpf)
            'Response.Clear()
            'Response.ContentType = "application/json; charset=utf-8"
            'Response.Write(myObjectJson)
            'Response.[End]()



            rpta = CLSctrcpf.Asignar()
            'rpta = True

            If rpta Then


                ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                Call mt_ShowMessage("Se asociaron correctamente las carreras profesionales", MessageType.Success)
            Else
                rpta = False
                Call mt_ShowMessage("Error al asignar carreras profesionales", MessageType.Error)
                'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
            End If


            CLSctrcpf = Nothing





            Return rpta
        Catch ex As Exception

            'Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function

    Private Function mt_GuardarPaso5ReglaTramite() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
            Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
            Dim dt As New Data.DataTable
            Dim filas As Integer = grwReglas.Rows.Count
            Dim i As Integer = 0
            Dim Fila As GridViewRow

            Dim CLSrctr As New clsReglaConceptoTramite
            Dim IDREGLA As Integer = 0

            Dim CTR As Integer = fnDevuelveNumEntero(Desencriptar(Me.hdCtr.Value.ToString))
            With CLSrctr
                ._codigo_ctr = CTR
                .tipooperacion = "E"
                For i = 0 To filas - 1
                    Fila = Me.grwReglas.Rows(i)
                    Dim clsDetRegla As New clsReglaConceptoTramite
                    Dim valor As Boolean = CType(Fila.FindControl("chkElegirRegla"), CheckBox).Checked

                    IDREGLA = fnDevuelveNumEntero(Me.grwReglas.DataKeys(i).Values(0).ToString)
                    ' TRL = fnDevuelveNumEntero(Me.grwCarreraProfesional.DataKeys(i).Values(0).ToString)

                    With clsDetRegla
                        .tipooperacion = "I"
                        ._codigo_rctr = IDREGLA
                        ._codigo_ctr = CTR
                        If (valor = True) Then
                            ._acceso = True
                        Else
                            ._acceso = False
                        End If

                    End With

                    CLSrctr.reglaasignada.Add(clsDetRegla)

                Next

            End With


            'Dim javaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
            'Dim myObjectJson As String = javaScriptSerializer.Serialize(CLSrctr)
            'Response.Clear()
            'Response.ContentType = "application/json; charset=utf-8"
            'Response.Write(myObjectJson)
            'Response.[End]()

            rpta = CLSrctr.Asignar()
            'rpta = True

            If rpta Then

                ' ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Tr&aacute;mite registrado con &eacute;xito', 'success');</script>")
                Call mt_ShowMessage("Se asociaron correctamente las reglas", MessageType.Success)
            Else
                rpta = False
                Call mt_ShowMessage("Error al asignar reglas", MessageType.Error)
                'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('Error al registrar tr&aacute;mite', 'danger');</script>")
            End If

            CLSrctr = Nothing

            Return rpta
        Catch ex As Exception

            'Response.Write(ex.Message & "----" & ex.StackTrace)
            'ClientScript.RegisterStartupScript(Me.GetType, "Pop", "<script>ShowMessage('" & ex.Message.Replace("'", "") & "', 'danger');</script>")
            Call mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
            Return False
        End Try
    End Function
#End Region



#End Region

#Region "Funciones"

    Public Function fnDevuelveNumEntero(ByVal input As String) As Integer
        Dim r As Integer = 0

        If input = "" Then
            r = 0
        Else
            r = CInt(input)
        End If

        Return r
    End Function
    Public Function fnDevuelveNumDecimal(ByVal input As String) As Decimal
        Dim r As Decimal = 0.0

        If input = "" Then
            r = 0.0
        Else
            r = CDec(input)
        End If

        Return r
    End Function

    Private Function fc_GetDocentes() As Data.DataTable
        Dim dt As New Data.DataTable("data")

        C.AbrirConexion()
        dt = C.TraerDataTable("COM_ListarDocentes")
        C.CerrarConexion()

        Return dt
    End Function


    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))
    End Function

#End Region




    Protected Sub grwMensajeInformativo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwMensajeInformativo.RowCommand
        Try

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If (e.CommandName = "Editar") Then
                Call mt_CargarFuncionesUsuario()
                Me.Hdmctr.Value = Encriptar(grwMensajeInformativo.DataKeys(index).Values("codigo_mctr").ToString)
                Call mt_MostrarMensajeInformativo()
                Me.pnlMensajeInformativoRegistro.Visible = True
                Me.pnlMensajeInformativoLista.Visible = False

                Me.btnGuardarTramite.Enabled = False
                Me.btnCancelarTramite.Enabled = False
                Me.txtDescripcion_msjinfo.Focus()
            Else

            End If

            'Response.Write(Me.hdCtr.Value)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message, MessageType.Error)
        End Try
    End Sub
End Class

Public Class clsReglaConceptoTramite

    Private C As ClsConectarDatos
    Public tipooperacion As String

    Private codigo_rctr As Integer
    Private codigo_ctr As Integer

    Private acceso As Boolean
    Public reglaasignada As New List(Of clsReglaConceptoTramite)

    Public Property _codigo_ctr() As Integer
        Get
            Return codigo_ctr
        End Get
        Set(ByVal value As Integer)
            codigo_ctr = value
        End Set
    End Property

    Public Property _codigo_rctr() As Integer
        Get
            Return codigo_rctr
        End Get
        Set(ByVal value As Integer)
            codigo_rctr = value
        End Set
    End Property
    Public Property _acceso() As Boolean
        Get
            Return acceso
        End Get
        Set(ByVal value As Boolean)
            acceso = value
        End Set
    End Property

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
        _codigo_ctr = 0
        _codigo_rctr = 0

    End Sub

    Public Function Listar(Optional ByVal est As String = "%") As Data.DataTable
        Try
            Dim dt As Data.DataTable
            C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            dt = C.TraerDataTable("TRL_MANT_ReglasListar", tipooperacion, codigo_rctr, codigo_ctr)
            C.CerrarConexion()

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try


    End Function

    Public Function Asignar() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim id As Integer = 0
            C.IniciarTransaccion()

            rpta = C.Ejecutar("TRL_ReglaTramite_Eliminar", tipooperacion, codigo_ctr, codigo_rctr)


            For Each detalle As clsReglaConceptoTramite In reglaasignada
                With detalle
                    If .acceso Then
                        rpta = C.Ejecutar("TRL_ReglaTramite_Asignar", .tipooperacion, .codigo_ctr, .codigo_rctr, .acceso)

                        If rpta = False Then
                            C.AbortarTransaccion()
                            Return False
                        End If
                    End If
                End With
            Next



            C.TerminarTransaccion()
            Return rpta
        Catch ex As Exception
            C.TerminarTransaccion()
            Return False
        End Try
    End Function


End Class


Public Class clsMensajeInformativo
    Private C As ClsConectarDatos
    Public tipooperacion As String
    Private codigo_mctr As Integer
    Private codigo_ctr As Integer
    Private descripcion As String

    Private orden As Integer
    

    Private activo As Boolean

    Public Property _orden() As Integer
        Get
            Return orden
        End Get
        Set(ByVal value As Integer)
            orden = value
        End Set
    End Property

    Public Property _activo() As Boolean
        Get
            Return activo
        End Get
        Set(ByVal value As Boolean)
            activo = value
        End Set
    End Property

    Public Property _descripcion() As String
        Get
            Return descripcion
        End Get
        Set(ByVal value As String)
            descripcion = value
        End Set
    End Property

    Public Property _codigo_ctr() As Integer
        Get
            Return codigo_ctr
        End Get
        Set(ByVal value As Integer)
            codigo_ctr = value
        End Set
    End Property

    Public Property _codigo_mctr() As Integer
        Get
            Return codigo_mctr
        End Get
        Set(ByVal value As Integer)
            codigo_mctr = value
        End Set
    End Property

    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
        _codigo_ctr = 0
        _codigo_mctr = 0

    End Sub

    Public Function Registrar() As Boolean

        Try
            Dim vOrden As Boolean
            Dim rpta As Boolean = False
            Dim id As Integer = 0
            C.AbrirConexion()
            rpta = C.Ejecutar("TRL_MensajeInformativo_Registrar", tipooperacion, codigo_mctr, codigo_ctr, descripcion, orden, activo)
            C.CerrarConexion()
            Return rpta
        Catch ex As Exception

            Return False
        End Try

    End Function


    Public Function fnExisteOrden() As Boolean
        Try
            Dim dt As Data.DataTable
            Dim i As Integer = 0
            Dim sw As Boolean = True

            dt = Listar("1")

            For i = 0 To dt.rows.count - 1
                If orden = dt.rows(i).item("orden") Then
                    sw = False
                End If
            Next

            Return sw

        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function Listar(Optional ByVal est As String = "%") As Data.DataTable
        Try
            Dim dt As Data.DataTable
            C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipooperacion, codigo_mctr, codigo_ctr, orden, est)
            C.CerrarConexion()

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try


    End Function



End Class

Public Class clsCarreraProfesional
    Private C As ClsConectarDatos
    Public tipooperacion As String
    Private codigo_ctrcpf As Integer
    Private codigo_cpf As Integer
    Private codigo_ctr As Integer
    Private codigo_cpf_STR As String
    Private acceso As Boolean
    Public cpfasignada As New List(Of clsCarreraProfesional)


    Public Property _acceso() As Boolean
        Get
            Return acceso
        End Get
        Set(ByVal value As Boolean)
            acceso = value
        End Set
    End Property
    Public Property _codigo_cpf_STR() As String
        Get
            Return codigo_cpf_STR
        End Get
        Set(ByVal value As String)
            codigo_cpf_STR = value
        End Set
    End Property


    Public Property _codigo_ctr() As Integer
        Get
            Return codigo_ctr
        End Get
        Set(ByVal value As Integer)
            codigo_ctr = value
        End Set
    End Property


    Public Property _codigo_cpf() As Integer
        Get
            Return codigo_cpf
        End Get
        Set(ByVal value As Integer)
            codigo_cpf = value
        End Set
    End Property

    Public Property _codigo_ctrcpf() As Integer
        Get
            Return codigo_ctrcpf
        End Get
        Set(ByVal value As Integer)
            codigo_ctrcpf = value
        End Set
    End Property


    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If

    End Sub


    Public Function Asignar() As Boolean
        Try
            Dim rpta As Boolean = False
            Dim id As Integer = 0
            C.IniciarTransaccion()

            For Each detalle As clsCarreraProfesional In cpfasignada
                With detalle
                    rpta = C.Ejecutar("TRL_CarreraProfesional_Asignar", .tipooperacion, ._codigo_ctrcpf, ._codigo_ctr, ._codigo_cpf, ._acceso)

                    If rpta = False Then
                        C.AbortarTransaccion()
                        Return False
                    End If
                End With
            Next




            C.TerminarTransaccion()
            Return rpta
        Catch ex As Exception
            C.TerminarTransaccion()
            Return False
        End Try
    End Function

End Class

'Public Class clsReglaTramite
'    Private C As ClsConectarDatos
'    Public tipooperacion As String

'    Private codigo_rctrc As Integer
'    Private codigo_rct As Integer
'    Private codigo_ctr As Integer
'    Private codigo_ctr_STR As String
'    Private acceso As Boolean
'    Public reglaasignada As New List(Of clsReglaTramite)


'    Public Property _acceso() As Boolean
'        Get
'            Return acceso
'        End Get
'        Set(ByVal value As Boolean)
'            acceso = value
'        End Set
'    End Property
'    Public Property _codigo_ctr_STR() As String
'        Get
'            Return codigo_ctr_STR
'        End Get
'        Set(ByVal value As String)
'            codigo_ctr_STR = value
'        End Set
'    End Property
'    Public Property _codigo_ctr() As Integer
'        Get
'            Return codigo_ctr
'        End Get
'        Set(ByVal value As Integer)
'            codigo_ctr = value
'        End Set
'    End Property
'    Public Property _codigo_rct() As Integer
'        Get
'            Return codigo_rct
'        End Get
'        Set(ByVal value As Integer)
'            codigo_rct = value
'        End Set
'    End Property
'    Public Property _codigo_rctrc() As Integer
'        Get
'            Return codigo_rctrc
'        End Get
'        Set(ByVal value As Integer)
'            codigo_rctrc = value
'        End Set
'    End Property

'    Sub New()
'        If C Is Nothing Then
'            C = New ClsConectarDatos
'            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
'        End If

'    End Sub

'    Public Function Asignar() As Boolean
'        Try
'            Dim rpta As Boolean = False
'            Dim id As Integer = 0
'            C.IniciarTransaccion()

'            rpta = C.Ejecutar("TRL_ReglaTramite_Eliminar", "E", ._codigo_ctr, ._codigo_rct, ._acceso)

'            If rpta = True Then

'                For Each detalle As clsReglaTramite In reglaasignada
'                    With detalle
'                        If ._acceso Then
'                            rpta = C.Ejecutar("TRL_ReglaTramite_Asignar", .tipooperacion, ._codigo_ctr, ._codigo_rct, ._acceso)

'                            If rpta = False Then
'                                C.AbortarTransaccion()
'                                Return False
'                            End If
'                        End If
'                    End With
'                Next

'            Else
'                C.AbortarTransaccion()
'                Return False
'            End If



'                C.TerminarTransaccion()
'                Return rpta
'        Catch ex As Exception
'            C.TerminarTransaccion()
'            Return False
'        End Try
'    End Function

'End Class

Public Class clsConceptoTramite

    Private C As ClsConectarDatos
    Public tipooperacion As String
    Public P_estado As String
    Public P_egresado As String
    Private codigoCtr As Integer
    Private codigoSco As Integer
    Private precioCtr As Decimal
    Private cantMaxCtr As Integer
    Private descripcionCtr As String
    Private ubicacionCtr As String
    Private codigoTest As String
    Private tieneRequisito As Boolean
    Private tieneSolicitudVirtual As Boolean
    Private mostrar As Boolean
    Private esEgresado As Boolean
    Private codigo_tctr As Integer
    Private codigoAct As Integer
    Private tieneReglas As Boolean
    Private tieneFlujo As Boolean
    Private refprecio_ctr As Decimal
    Private refcodigo_sco As Decimal
    Private tieneCarreraProfesionalAsociada As Boolean
    Private estadoAlu As Boolean
    Private rutaImg As String
    Private codigo_cco As Integer
    Private tieneArchivo As Boolean
    Private tieneEntrega As Boolean
    Private tieneNotaAbonoAutomatica As Boolean
    Private tieneMensajeInformativo As Boolean
    Private codigo_tdg As Integer
    Private refcodigo_cco As Integer
    Private codigo_per As Integer
    Private ip As String
    Private ordenador As String



    Public Property _codigoCtr() As Integer
        Get
            Return codigoCtr
        End Get
        Set(ByVal value As Integer)
            codigoCtr = value
        End Set
    End Property

    Public Property _codigoSco() As Integer
        Get
            Return codigoSco
        End Get
        Set(ByVal value As Integer)
            codigoSco = value
        End Set
    End Property

    Public Property _precioCtr() As Decimal
        Get
            Return precioCtr
        End Get
        Set(ByVal value As Decimal)
            precioCtr = value
        End Set
    End Property

    Public Property _cantMaxCtr() As Integer
        Get
            Return cantMaxCtr
        End Get
        Set(ByVal value As Integer)
            cantMaxCtr = value
        End Set
    End Property

    Public Property _descripcionCtr() As String
        Get
            Return descripcionCtr
        End Get
        Set(ByVal value As String)
            descripcionCtr = value
        End Set
    End Property

    Public Property _ubicacionCtr() As String
        Get
            Return ubicacionCtr
        End Get
        Set(ByVal value As String)
            ubicacionCtr = value
        End Set
    End Property

    Public Property _codigoTest() As String
        Get
            Return codigoTest
        End Get
        Set(ByVal value As String)
            codigoTest = value
        End Set
    End Property

    Public Property _tieneRequisito() As Boolean
        Get
            Return tieneRequisito
        End Get
        Set(ByVal value As Boolean)
            tieneRequisito = value
        End Set
    End Property

    Public Property _tieneSolicitudVirtual() As Boolean
        Get
            Return tieneSolicitudVirtual
        End Get
        Set(ByVal value As Boolean)
            tieneSolicitudVirtual = value
        End Set
    End Property

    Public Property _mostrar() As Boolean
        Get
            Return mostrar
        End Get
        Set(ByVal value As Boolean)
            mostrar = value
        End Set
    End Property

    Public Property _esEgresado() As Boolean
        Get
            Return esEgresado
        End Get
        Set(ByVal value As Boolean)
            esEgresado = value
        End Set
    End Property

    Public Property _codigo_tctr() As Integer
        Get
            Return codigo_tctr
        End Get
        Set(ByVal value As Integer)
            codigo_tctr = value
        End Set
    End Property

    Public Property _codigoAct() As Integer
        Get
            Return codigoAct
        End Get
        Set(ByVal value As Integer)
            codigoAct = value
        End Set
    End Property

    Public Property _tieneReglas() As Boolean
        Get
            Return tieneReglas
        End Get
        Set(ByVal value As Boolean)
            tieneReglas = value
        End Set
    End Property

    Public Property _tieneFlujo() As Boolean
        Get
            Return tieneFlujo
        End Get
        Set(ByVal value As Boolean)
            tieneFlujo = value
        End Set
    End Property

    Public Property _refprecio_ctr() As Decimal
        Get
            Return refprecio_ctr
        End Get
        Set(ByVal value As Decimal)
            refprecio_ctr = value
        End Set
    End Property

    Public Property _refcodigo_sco() As Decimal
        Get
            Return refcodigo_sco
        End Get
        Set(ByVal value As Decimal)
            refcodigo_sco = value
        End Set
    End Property

    Public Property _tieneCarreraProfesionalAsociada() As Boolean
        Get
            Return tieneCarreraProfesionalAsociada
        End Get
        Set(ByVal value As Boolean)
            tieneCarreraProfesionalAsociada = value
        End Set
    End Property

    Public Property _estadoAlu() As Boolean
        Get
            Return estadoAlu
        End Get
        Set(ByVal value As Boolean)
            estadoAlu = value
        End Set
    End Property

    Public Property _rutaImg() As String
        Get
            Return rutaImg
        End Get
        Set(ByVal value As String)
            rutaImg = value
        End Set
    End Property

    Public Property _codigo_cco() As Integer
        Get
            Return codigo_cco
        End Get
        Set(ByVal value As Integer)
            codigo_cco = value
        End Set
    End Property

    Public Property _tieneArchivo() As Boolean
        Get
            Return tieneArchivo
        End Get
        Set(ByVal value As Boolean)
            tieneArchivo = value
        End Set
    End Property

    Public Property _tieneEntrega() As Boolean
        Get
            Return tieneEntrega
        End Get
        Set(ByVal value As Boolean)
            tieneEntrega = value
        End Set
    End Property

    Public Property _tieneNotaAbonoAutomatica() As Boolean
        Get
            Return tieneNotaAbonoAutomatica
        End Get
        Set(ByVal value As Boolean)
            tieneNotaAbonoAutomatica = value
        End Set
    End Property

    Public Property _tieneMensajeInformativo() As Boolean
        Get
            Return tieneMensajeInformativo
        End Get
        Set(ByVal value As Boolean)
            tieneMensajeInformativo = value
        End Set
    End Property

    Public Property _codigo_tdg() As Integer
        Get
            Return codigo_tdg
        End Get
        Set(ByVal value As Integer)
            codigo_tdg = value
        End Set
    End Property

    Public Property _refcodigo_cco() As Integer
        Get
            Return refcodigo_cco
        End Get
        Set(ByVal value As Integer)
            refcodigo_cco = value
        End Set
    End Property

    Public Property _codigo_per() As Integer
        Get
            Return codigo_per
        End Get
        Set(ByVal value As Integer)
            codigo_per = value
        End Set
    End Property

    Public Property _ip() As String
        Get
            Return ip
        End Get
        Set(ByVal value As String)
            ip = value
        End Set
    End Property

    Public Property _ordenador() As String
        Get
            Return ordenador
        End Get
        Set(ByVal value As String)
            ordenador = value
        End Set
    End Property



    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If
        P_egresado = "%"
        P_estado = "%"
    End Sub


    Public Function Consultar() As Data.DataTable

        Try
            Dim dt As New Data.DataTable("data")

            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_ConceptoTramite_Listar", _
                        tipooperacion, _
                        _codigoCtr, _
                        _codigoTest, _
                        P_egresado, _
                        _codigo_tctr, _
                        _descripcionCtr, _
                        P_estado)
            'dt = C.TraerDataTable("TRL_ConceptoTramite_Listar", tipo, codigo_ctr, codigo_test, esEgresado, codigo_tctr, descripcion_ctr, estado)
            C.CerrarConexion()

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try

    End Function




    Public Function Registrar() As Integer
        Try

            Dim dt As New Data.DataTable("data")
            Dim id As Integer = 0
            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_ConceptoTramite_Registrar", _
                        tipooperacion, _
                        _codigoCtr, _
                        _codigoSco, _
                        _precioCtr, _
                        _cantMaxCtr, _
                        _descripcionCtr, _
                        _ubicacionCtr, _
                        _codigoTest, _
                        _tieneRequisito, _
                        _tieneSolicitudVirtual, _
                        _mostrar, _
                        _esEgresado, _
                        _codigo_tctr, _
                        _codigoAct, _
                        _tieneReglas, _
                        _tieneFlujo, _
                        _refprecio_ctr, _
                        _refcodigo_sco, _
                        _tieneCarreraProfesionalAsociada, _
                        _estadoAlu, _
                        _rutaImg, _
                        _codigo_cco, _
                        _tieneArchivo, _
                        _tieneEntrega, _
                        _tieneNotaAbonoAutomatica, _
                        _tieneMensajeInformativo, _
                        _codigo_tdg, _
                        _refcodigo_cco, _
                        _codigo_per, _
                        _ip, _
                        _ordenador)
            C.CerrarConexion()
            If dt.Rows.Count = 1 Then
                id = dt.Rows(0).Item("ID")
            End If

            Return id
        Catch ex As Exception

            Return 0
        End Try

    End Function

    Public Function Eliminar() As Boolean
        Try

            Dim rpta As Boolean
            Dim id As Integer = 0
            C.AbrirConexion()
            rpta = C.Ejecutar("TRL_ConceptoTramite_Eliminar", _
                        _codigoCtr, _
                        _codigo_per, _
                        _ip, _
                        _ordenador)
            C.CerrarConexion()
            

            Return rpta
        Catch ex As Exception
            Return False
        End Try

    End Function


    Public Function ValidarRegistro() As Data.DataTable
        Dim dt As New Data.DataTable
        dt.Columns.Add("rpta")
        dt.Columns.Add("msg")
        Dim fila As Data.DataRow = dt.NewRow()


        Try

            If _descripcionCtr = "" Then
                fila("rpta") = False
                fila("msg") = "Ingrese descripción del tramite"
                dt.Rows.Add(fila)
                Return dt


            Else
                fila("rpta") = True
                fila("msg") = ""
                dt.Rows.Add(fila)
                Return dt
            End If


        Catch ex As Exception
            fila("rpta") = False
            fila("msg") = ex.Message & " -- " & ex.StackTrace
            dt.Rows.Add(fila)

            Return dt

        End Try
    End Function

End Class

Public Class clsFLujoTramite
    Private C As ClsConectarDatos
    Public tipooperacion As String

    Private codigo_ftr As Integer

    Private codigo_ctr As Integer

    Private codigo_apl As Integer

    Private codigo_tfu As Integer

    Private orden_ftr As Integer

    Private verDetAcad As Boolean

    Private verDetAdm As Boolean

    Private accionUrl As Integer

    Private proceso As String

    Private activo As Boolean

    Private tieneEmailAprobacion As Boolean

    Private tieneEmailRechazo As Boolean

    Private tieneEmailPlanEstudio As Boolean

    Private tieneEmailPrecioCredito As Boolean

    Private tieneEmailGyT As Boolean

    Private tieneMsnTextoAprobacion As Boolean

    Private tieneMsnTextoRechazo As Boolean

    Private tieneEvaluacionPersonal As Boolean

    Private codigo_per As Integer

    Private ip As String

    Private ordenador As String

    Public Property _ordenador() As String
        Get
            Return ordenador
        End Get
        Set(ByVal value As String)
            ordenador = value
        End Set
    End Property

    Public Property _ip() As String
        Get
            Return ip
        End Get
        Set(ByVal value As String)
            ip = value
        End Set
    End Property

    Public Property _codigo_per() As Integer
        Get
            Return codigo_per
        End Get
        Set(ByVal value As Integer)
            codigo_per = value
        End Set
    End Property



    Public Property _tieneEvaluacionPersonal() As Boolean
        Get
            Return tieneEvaluacionPersonal
        End Get
        Set(ByVal value As Boolean)
            tieneEvaluacionPersonal = value
        End Set
    End Property

    Public Property _tieneMsnTextoRechazo() As Boolean
        Get
            Return tieneMsnTextoRechazo
        End Get
        Set(ByVal value As Boolean)
            tieneMsnTextoRechazo = value
        End Set
    End Property


    Public Property _tieneMsnTextoAprobacion() As Boolean
        Get
            Return tieneMsnTextoAprobacion
        End Get
        Set(ByVal value As Boolean)
            tieneMsnTextoAprobacion = value
        End Set
    End Property


    Public Property _tieneEmailGyT() As Boolean
        Get
            Return tieneEmailGyT
        End Get
        Set(ByVal value As Boolean)
            tieneEmailGyT = value
        End Set
    End Property

    Public Property _tieneEmailPrecioCredito() As Boolean
        Get
            Return tieneEmailPrecioCredito
        End Get
        Set(ByVal value As Boolean)
            tieneEmailPrecioCredito = value
        End Set
    End Property

    Public Property _tieneEmailPlanEstudio() As Boolean
        Get
            Return tieneEmailPlanEstudio
        End Get
        Set(ByVal value As Boolean)
            tieneEmailPlanEstudio = value
        End Set
    End Property


    Public Property _tieneEmailRechazo() As Boolean
        Get
            Return tieneEmailRechazo
        End Get
        Set(ByVal value As Boolean)
            tieneEmailRechazo = value
        End Set
    End Property


    Public Property _activo() As Boolean
        Get
            Return activo
        End Get
        Set(ByVal value As Boolean)
            activo = value
        End Set
    End Property


    Public Property _proceso() As String
        Get
            Return proceso
        End Get
        Set(ByVal value As String)
            proceso = value
        End Set
    End Property


    Public Property _accionUrl() As Integer
        Get
            Return accionUrl
        End Get
        Set(ByVal value As Integer)
            accionUrl = value
        End Set
    End Property



    Public Property _verDetAdm() As Boolean
        Get
            Return verDetAdm
        End Get
        Set(ByVal value As Boolean)
            verDetAdm = value
        End Set
    End Property


    Public Property _verDetAcad() As Boolean
        Get
            Return verDetAcad
        End Get
        Set(ByVal value As Boolean)
            verDetAcad = value
        End Set
    End Property



    Public Property _tieneEmailAprobacion() As Boolean
        Get
            Return tieneEmailAprobacion
        End Get
        Set(ByVal value As Boolean)
            tieneEmailAprobacion = value
        End Set
    End Property


    Public Property _orden_ftr() As Integer
        Get
            Return orden_ftr
        End Get
        Set(ByVal value As Integer)
            orden_ftr = value
        End Set
    End Property

    Public Property _codigo_tfu() As Integer
        Get
            Return codigo_tfu
        End Get
        Set(ByVal value As Integer)
            codigo_tfu = value
        End Set
    End Property


    Public Property _codigo_apl() As Integer
        Get
            Return codigo_apl
        End Get
        Set(ByVal value As Integer)
            codigo_apl = value
        End Set
    End Property


    Public Property _codigo_ctr() As Integer
        Get
            Return codigo_ctr
        End Get
        Set(ByVal value As Integer)
            codigo_ctr = value
        End Set
    End Property


    Public Property _codigo_ftr() As Integer
        Get
            Return codigo_ftr
        End Get
        Set(ByVal value As Integer)
            codigo_ftr = value
        End Set
    End Property



    Sub New()
        If C Is Nothing Then
            C = New ClsConectarDatos
            C.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        End If

        _orden_ftr = 0
    End Sub


    Public Function Registrar() As Integer
        Try

            Dim dt As New Data.DataTable("data")
            Dim id As Integer = 0
            C.AbrirConexion()
            dt = C.TraerDataTable("TRL_FlujoTramite_Registrar", _
                        tipooperacion, _
                        _codigo_ftr, _
                        _codigo_ctr, _
                        _codigo_apl, _
                        _codigo_tfu, _
                        _orden_ftr, _
                        _verDetAcad, _
                        _verDetAdm, _
                        _accionUrl, _
                        _proceso, _
                        _activo, _
                        _tieneEmailRechazo, _
                        _tieneEmailAprobacion, _
                        _tieneEmailPlanEstudio, _
                        _tieneEmailPrecioCredito, _
                        _tieneEmailGyT, _
                        _tieneMsnTextoRechazo, _
                        _tieneMsnTextoAprobacion, _
                        _tieneEvaluacionPersonal, _
                        _codigo_per, _
                        _ip, _
                        _ordenador)
            C.CerrarConexion()
            If dt.Rows.Count = 1 Then
                id = dt.Rows(0).Item("ID")
            End If

            Return id
        Catch ex As Exception

            Return 0
        End Try

    End Function


    Public Function Listar(Optional ByVal est As String = "%") As Data.DataTable
        Try
            Dim dt As Data.DataTable
            C.AbrirConexion()
            'dt = C.TraerDataTable("TRL_MANT_MensajeInformativoListar", tipo, codigo_mctr, codigo_ctr, estado)
            dt = C.TraerDataTable("TRL_MANT_FlujotramiteListar", tipooperacion, codigo_ftr, codigo_ctr, orden_ftr, est)
            C.CerrarConexion()

            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function fnExisteOrden() As Boolean
        Try
            Dim dt As Data.DataTable
            Dim i As Integer = 0
            Dim sw As Boolean = True

            dt = Listar("1")

            For i = 0 To dt.rows.count - 1
                If orden_ftr = dt.rows(i).item("orden") Then
                    sw = False
                End If
            Next

            Return sw

        Catch ex As Exception
            Return False
        End Try
    End Function


End Class