
Partial Class OrientacionCliente_FrmConsultarTickets
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ListarOrigen()
            ListarTipo()
            ListarServicio()
            Listar()
        End If
    End Sub

    Private Sub ListarOrigen()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarOrigenOrientacion]")

        Me.ddlOrigen.Items.Clear()
        Me.ddlOrigen.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlOrigen.Items.Add(New ListItem(dt.Rows(i).Item("nombre_ooc"), dt.Rows(i).Item("codigo_ooc")))
            Next
        End If
    End Sub

    Private Sub ListarTipo()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarTipoOrientacion]")

        Me.ddlTipo.Items.Clear()
        Me.ddlTipo.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlTipo.Items.Add(New ListItem(dt.Rows(i).Item("nombre_tio"), dt.Rows(i).Item("codigo_tio")))
            Next
        End If
    End Sub

    Private Sub ListarServicio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarServicioOrientacion]")

        Me.ddlServicio.Items.Clear()
        Me.ddlServicioAtencion.Items.Clear()
        Me.ddlServicio.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        Me.ddlServicioAtencion.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlServicio.Items.Add(New ListItem(dt.Rows(i).Item("nombre_soc"), dt.Rows(i).Item("codigo_soc")))
                Me.ddlServicioAtencion.Items.Add(New ListItem(dt.Rows(i).Item("nombre_soc"), dt.Rows(i).Item("codigo_soc")))
            Next
        End If
    End Sub

    Private Sub Listar()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable

        Dim estados As String = ""

        If Me.CheckBox1.Checked = True Then
            estados += "1,"
        End If

        If Me.CheckBox2.Checked = True Then
            estados += "2,"
        End If

        If Me.CheckBox3.Checked = True Then
            estados += "3,"
        End If

        If Me.CheckBox4.Checked = True Then
            estados += "4,"
        End If

        If Me.CheckBox5.Checked = True Then
            estados += "5,"
        End If

        Dim seguimiento As String = "%"
        If Me.CheckBox6.Checked = True Then
            seguimiento = 1
        End If

        dt = obj.TraerDataTable("[OAC_Listartickets]", estados, Request("ctf"), seguimiento, Me.ddlServicioAtencion.SelectedValue, Me.txtNroTicket.Text)

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()
    End Sub


    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = False
        Me.Mantenimiento.Visible = True
        Limpiar()
        ActivarCampos()
        Me.DivSeguimiento.Visible = False
        Me.DivEstado.Visible = False
        Me.btnGuardar.Visible = True
        Me.ddlOrigen.Enabled = True
        Me.ddlTipo.Enabled = True
        Me.ddlServicio.Enabled = True
        Me.DivSeguimiento.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingN", "fnLoading(false)", True)

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = True
        Me.Mantenimiento.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingC", "fnLoading(false)", True)

    End Sub

    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If e.CommandName = "Atender" Then
            Limpiar()
            Me.Lista.Visible = False
            Me.Mantenimiento.Visible = True
            Me.DivSeguimiento.Visible = True
            Dim codigo_toc As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_toc")
            CargarDatos(codigo_toc)
            SeguimientoTicket(codigo_toc)
            ListarTramites(Me.hda.Value, codigo_toc)
        End If
        If e.CommandName = "Retornar" Then
            Me.Lista.Visible = False
            Me.DivDerivar.Visible = True
        End If
        If e.CommandName = "Derivar" Then
            Me.Lista.Visible = False
            Me.DivDerivar.Visible = True
            Me.hdprocedencia.Value = 0
            CargarDerivar(Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_toc"))
            Me.hdc.Value = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_toc")
        End If
        If e.CommandName = "Anular" Then
            Me.Lista.Visible = False
            Me.DivAnular.Visible = True
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingRD", "fnLoading(false)", True)

    End Sub

    Private Sub CargarDatos(ByVal codigo_toc As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_Verticket]", codigo_toc)
        If dt.Rows.Count > 0 Then
            Me.hdc.Value = dt.Rows(0).Item("codigo_toc").ToString
            Me.hda.Value = dt.Rows(0).Item("codigo_alu").ToString
            Me.txtEstudiante.Text = dt.Rows(0).Item("estudiante").ToString
            Me.txtcorreousat.Text = dt.Rows(0).Item("correousat").ToString
            Me.txtcelular.Text = dt.Rows(0).Item("telefono").ToString
            Me.txtemail.Text = dt.Rows(0).Item("correo").ToString
            Me.txtAsunto.Text = dt.Rows(0).Item("asunto_toc").ToString
            Me.txtDescripcion.Text = dt.Rows(0).Item("descripcion_toc").ToString
            Me.ddlOrigen.SelectedValue = dt.Rows(0).Item("codigo_ooc").ToString
            Me.ddlTipo.SelectedValue = dt.Rows(0).Item("codigo_tio").ToString
            Me.ddlServicio.SelectedValue = dt.Rows(0).Item("codigo_soc").ToString

            If dt.Rows(0).Item("codigo_soc").ToString <> "0" Then
                Me.txtEstado.Text = dt.Rows(0).Item("nombre_eoc").ToString
                Me.DivEstado.Visible = True
                Me.btnGuardar.Visible = False

                Me.ddlOrigen.Enabled = False
                Me.ddlTipo.Enabled = False
                Me.ddlServicio.Enabled = False
                Me.DivSeguimiento.Visible = True
            Else
                Me.DivEstado.Visible = False
                Me.btnGuardar.Visible = True
                Me.ddlOrigen.Enabled = True
                Me.ddlTipo.Enabled = True
                Me.ddlServicio.Enabled = True
                Me.DivSeguimiento.Visible = False
            End If

            InactivarCampos()
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub Limpiar()
        Me.hdc.Value = 0
        Me.hda.Value = 0
        Me.txtEstudiante.Text = ""
        Me.txtcorreousat.Text = ""
        Me.txtcelular.Text = ""
        Me.txtemail.Text = ""
        Me.txtAsunto.Text = ""
        Me.txtDescripcion.Text = ""
        Me.ddlOrigen.SelectedValue = 0
        Me.ddlServicio.SelectedValue = 0
        Me.ddlTipo.SelectedValue = 0
        Me.gvSeguimiento.DataSource = Nothing
        Me.gvSeguimiento.DataBind()
        Me.lblMensajeValidación.InnerHtml = ""
        Me.lblMensajeValidación.Attributes.Remove("class")
        Me.gvTramites.DataSource = Nothing
        Me.gvTramites.DataBind()
        Me.txtEstado.Text = ""
        Me.txtNivelAtencion.Text = ""
        Me.txtRol.Text = ""
    End Sub

    Private Sub InactivarCampos()
        Me.txtEstudiante.Enabled = False
        Me.btnBuscarEstudiante.Enabled = False
        Me.txtcorreousat.Enabled = False
        Me.txtcelular.Enabled = False
        Me.txtemail.Enabled = False
        Me.txtAsunto.Enabled = False
        Me.txtDescripcion.Enabled = False
        Me.archivo.Enabled = False
        Me.btnConfirmarDatos.Visible = False
    End Sub

    Private Sub ActivarCampos()
        Me.txtEstudiante.Enabled = True
        Me.btnBuscarEstudiante.Enabled = True
        Me.txtcorreousat.Enabled = True
        Me.txtcelular.Enabled = True
        Me.txtemail.Enabled = True
        Me.txtAsunto.Enabled = True
        Me.txtDescripcion.Enabled = True
        Me.archivo.Enabled = True
        Me.btnConfirmarDatos.Visible = True
    End Sub

    Private Sub SeguimientoTicket(ByVal codigo_toc As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_SeguimientoTicket]", codigo_toc)
        If dt.Rows.Count > 0 Then
            Me.gvSeguimiento.DataSource = dt
            Me.gvSeguimiento.DataBind()
        Else
            Me.gvSeguimiento.DataSource = Nothing
            Me.gvSeguimiento.DataBind()
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub btnCancelarAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarAnular.Click
        Me.Lista.Visible = True
        Me.DivAnular.Visible = False
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingCA", "fnLoading(false)", True)

    End Sub

    Protected Sub btnCancelarDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarDerivar.Click
        'si se dió clic al derivar del listado
        If Me.hdprocedencia.Value = 0 Then
            Me.Lista.Visible = True
            Me.DivDerivar.Visible = False
        End If
        If Me.hdprocedencia.Value = 1 Then ' si se dio clic al boton derivar del div mantenimiento
            Me.Mantenimiento.Visible = True
            Me.DivDerivar.Visible = False
        End If

        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingCD", "fnLoading(false)", True)

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Listar()
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingB", "fnLoading(false)", True)

    End Sub

    Protected Sub btnBuscarEstudiante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarEstudiante.Click

        Me.txtBusqueda.Text = ""
        Me.gvEstudiantes.DataSource = ""
        Me.gvEstudiantes.DataBind()
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "ModalMBE", "$('#mdBusquedaEstudiante').modal('show')", True)
    End Sub

    Protected Sub btnBusquedaEstudiante_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusquedaEstudiante.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.txtBusqueda.Text.Trim.Length < 3 Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese por lo menos 3 caracteres')", True)
        Else
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim dt As New Data.DataTable
            dt = obj.TraerDataTable("[OAC_BuscarEstudiantes]", Me.txtBusqueda.Text)
            If dt.Rows.Count > 0 Then
                Me.gvEstudiantes.DataSource = dt
                Me.gvEstudiantes.DataBind()
            Else
                Me.gvEstudiantes.DataSource = Nothing
                Me.gvEstudiantes.DataBind()
            End If

            obj.CerrarConexion()
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingBB", "fnLoading(false)", True)

    End Sub


    Protected Sub gvEstudiantes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvEstudiantes.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If e.CommandName = "Seleccionar" Then
            Me.hda.Value = Me.gvEstudiantes.DataKeys(e.CommandArgument).Values("codigo_alu")
            Me.txtEstudiante.Text = Me.gvEstudiantes.Rows(e.CommandArgument).Cells(1).Text()
            Me.txtcorreousat.Text = Me.gvEstudiantes.DataKeys(e.CommandArgument).Values("correo_usat")
            Me.txtcelular.Text = Me.gvEstudiantes.DataKeys(e.CommandArgument).Values("celular")
            Me.txtemail.Text = Me.gvEstudiantes.DataKeys(e.CommandArgument).Values("correo_personal")
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "ModalMBES", "$('#mdBusquedaEstudiante').modal('hide')", True)
            ListarTramites(Me.hda.Value, Me.hdc.Value)
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "LoadingRCE", "fnLoading(false)", True)

    End Sub

    Protected Sub gvSeguimiento_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvSeguimiento.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim btn As LinkButton = DirectCast(e.Row.Cells(5).FindControl("btnDescargar"), LinkButton)
            If Me.gvSeguimiento.DataKeys(e.Row.RowIndex).Values("IdArchivosCompartidos") <> "0" Then
                btn.OnClientClick = "fnDescargar('../../Descargar.aspx?Id=" + Me.gvSeguimiento.DataKeys(e.Row.RowIndex).Values("IdArchivosCompartidos").ToString + "');return false;"
                btn.ToolTip = "Descargar archivo"
            Else
                btn.Visible = False
            End If


        End If
    End Sub

    Public Function Validar() As Boolean
        Me.lblMensajeValidación.InnerHtml = ""
        Me.lblMensajeValidación.Attributes.Remove("class")
        If Me.txtEstudiante.Text.Trim = "" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Realice la búsqueda y seleccione un estudiante"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.txtAsunto.Text.Trim = "" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Ingrese el asunto del ticket"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.txtDescripcion.Text.Trim = "" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Ingrese la descripción del ticket"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.archivo.HasFile = True Then
            If Me.archivo.PostedFile.ContentLength > 20971520 Then
                Me.lblMensajeValidación.InnerText = "<i class='ion-alert-circled'></i>&nbsp;Solo puede adjuntar archivos de un tamaño máximo de 20MB"
                Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If

            Dim Extensiones As String() = {".jpg", ".jpeg", ".png", ".doc", ".docx", ".pdf", ".rar"}
            Dim validarArchivo As Integer = -1
            validarArchivo = Array.IndexOf(Extensiones, System.IO.Path.GetExtension(archivo.FileName).ToLower)
            If validarArchivo = -1 Then
                Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Solo puede adjuntar archivos en formato .jpg,.jpeg,.png,.doc,.docx,.pdf,.rar"
                Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
                Return False
            End If
        End If
        If Me.ddlOrigen.SelectedValue = "0" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Seleccione el canal de recepción"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.ddlTipo.SelectedValue = "0" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Seleccione la clasificación"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        If Me.ddlServicio.SelectedValue = "0" Then
            Me.lblMensajeValidación.InnerHtml = "<i class='ion-alert-circled'></i>&nbsp;Seleccione el servicio"
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
            Return False
        End If
        Return True

    End Function

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Validar() = True Then
            If Me.hdc.Value = 0 Then
                Me.Lista.Visible = True
                Me.Mantenimiento.Visible = False
            Else
                ActualizarTicket(Me.hdc.Value, Me.ddlOrigen.SelectedValue, Me.ddlTipo.SelectedValue, Me.ddlServicio.SelectedValue, Session("id_per"), Request("ctf"))
            End If

        End If
    End Sub

    Private Sub ActualizarTicket(ByVal codigo_toc As Integer, ByVal codigo_ooc As Integer, ByVal codigo_tio As Integer, ByVal codigo_soc As Integer, ByVal usuario As Integer, ByVal ctf As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ActualizarTicket]", codigo_toc, codigo_ooc, codigo_tio, codigo_soc, usuario, ctf)
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Me.lblMensajeValidación.InnerHtml = dt.Rows(0).Item("Mensaje").ToString
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-success")
            CargarDatos(codigo_toc)
            SeguimientoTicket(codigo_toc)
            ListarTramites(Me.hda.Value, codigo_toc)
            Listar()
        Else
            Me.lblMensajeValidación.InnerHtml = dt.Rows(0).Item("Mensaje").ToString
            Me.lblMensajeValidación.Attributes.Add("class", "alert alert-danger")
        End If

    End Sub

    Protected Sub txtAsunto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAsunto.TextChanged
        If Me.txtAsunto.Text.Length > 400 Then
            Me.txtAsunto.Text = Me.txtAsunto.Text.Substring(0, 400)
        End If
        Me.lblContadorAsunto.Text = Me.txtAsunto.Text.Length
    End Sub

    Protected Sub txtDescripcion_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDescripcion.TextChanged
        If Me.txtDescripcion.Text.Length > 4000 Then
            Me.txtDescripcion.Text = Me.txtDescripcion.Text.Substring(0, 4000)
        End If
        Me.lblcontador.Text = Me.txtDescripcion.Text.Length
    End Sub

    Private Sub ListarTramites(ByVal codigo_alu As Integer, ByVal codigo_toc As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarTramitesVirtualesxCodigo]", codigo_alu, codigo_toc)
        If dt.Rows.Count > 0 Then
            Me.gvTramites.DataSource = dt
            Me.gvTramites.DataBind()
        Else
            Me.gvTramites.DataSource = Nothing
            Me.gvTramites.DataBind()
        End If

    End Sub

    Protected Sub gvTramites_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTramites.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chk As CheckBox = DirectCast(e.Row.Cells(0).FindControl("chk"), CheckBox)
            If Me.hdc.Value <> "0" Then
                If Me.gvTramites.DataKeys(e.Row.RowIndex).Values("vinculado") <> "0" Then
                    chk.Checked = True

                    chk.BackColor = Drawing.Color.Blue

                End If
                chk.Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDerivar.Click
        Me.Mantenimiento.Visible = False
        Me.DivDerivar.Visible = True
        Me.hdprocedencia.Value = 1
        CargarDerivar(Me.hdc.Value)
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Request("ctf") = 256 Then
                Me.gvLista.Columns(14).Visible = False
            End If

            Dim btn As LinkButton = DirectCast(e.Row.Cells(12).FindControl("btnRetornar"), LinkButton)
            Dim btn2 As LinkButton = DirectCast(e.Row.Cells(13).FindControl("btnDerivar"), LinkButton)

            If Me.gvLista.DataKeys(e.Row.RowIndex).Values("codigo_soc") <> "0" Then
                btn2.Visible = True
                If Me.gvLista.DataKeys(e.Row.RowIndex).Values("orden_cso") = 0 Then
                    btn.Visible = False
                Else
                    btn.Visible = True
                End If

            Else
                btn.Visible = False
                btn2.Visible = False
            End If
        End If

       
    End Sub

    Private Sub CargarDerivar(ByVal codigo_toc As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_VerDerivar]", codigo_toc)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            Me.txtServicioD.Text = dt.Rows(0).Item("nombre_soc")
            Me.txtRolD.Text = dt.Rows(0).Item("descripcion_Tfu")
            Me.txtNivelD.Text = dt.Rows(0).Item("nombre_ioc")
            Me.btnDerivar.Visible = True
        Else
            Me.txtServicioD.Text = ""
            Me.txtRolD.Text = ""
            Me.txtNivelD.Text = ""
            Me.btnDerivar.Visible = False
        End If
    End Sub

    Private Sub GuardarDerivar(ByVal codigo_toc As Integer, ByVal motivo As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_DerivarTicket]", codigo_toc, motivo, usuario)
        obj.CerrarConexion()
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Respuesta") = 1 Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "msj2", "fnMensaje('success','Ticket derivado correctamente')", True)
                Me.DivDerivar.Visible = False
                Me.Lista.Visible = True
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "msj1", "fnMensaje('error','No se pudo realizar la derivación')", True)

            End If
            
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "msj3", "fnMensaje('error','No se pudo realizar la derivación')", True)

        End If
    End Sub

    Protected Sub btnGuardarDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarDerivar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.txtMotivoDerivar.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "msj3", "fnMensaje('error','debe ingresar el motivo de derivación')", True)
        Else
            GuardarDerivar(Me.hdc.Value, Me.txtMotivoDerivar.Text, Session("id_per"))
        End If
    End Sub
End Class

