Imports System.Data

Partial Class OrientacionCliente_FrmServicio
    Inherits System.Web.UI.Page

    Dim dtConfiguracion As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(true)", True)
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If IsPostBack = False Then
            Listar()
            ConsultarInstancias()
            ConsultarTipoFuncion()
            Dim tabla As New Data.DataTable
            tabla.Columns.Add("codigo_cso")
            tabla.Columns.Add("codigo_soc")
            tabla.Columns.Add("codigo_ioc")
            tabla.Columns.Add("nombre_ioc")
            tabla.Columns.Add("codigo_tfu")
            tabla.Columns.Add("descripcion_tfu")
            tabla.Columns.Add("diasatencion_cso")
            tabla.Columns.Add("orden_cso")
            ViewState("Grilla") = tabla
        End If
    End Sub

    Private Sub Listar()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarServicioOrientacion]")

        If dt.Rows.Count > 0 Then
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()
        Else
            Me.gvLista.DataSource = Nothing
            Me.gvLista.DataBind()
        End If

        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarInstancias()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("OAC_ListarInstanciaOrientacion")
        obj.CerrarConexion()

        Me.ddlInstancia.Items.Clear()
        Me.ddlInstancia.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlInstancia.Items.Add(New ListItem(dt.Rows(i).Item("nombre_ioc"), dt.Rows(i).Item("codigo_ioc")))
            Next
        End If
    End Sub

    Private Sub ConsultarTipoFuncion()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("DOC_ListaTipoFuncion", "GEN", 0)
        obj.CerrarConexion()

        Me.ddlResponsable.Items.Clear()
        Me.ddlResponsable.Items.Add(New ListItem("[ -- Seleccione --]", "0"))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlResponsable.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_tfu"), dt.Rows(i).Item("codigo_tfu")))
            Next
        End If
    End Sub


    Protected Sub lbNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNuevo.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Me.Lista.Visible = False
        Me.DivMantenimiento.Visible = True
        Limpiar()
        Me.txtOrden.Text = 1
        Me.hdc.Value = 0
        Dim dt As Data.DataTable = DirectCast(ViewState("Grilla"), Data.DataTable)
        dt.Rows.Clear()
        ViewState("Grilla") = dt
        Me.gvConfiguracion.DataSource = Nothing
        Me.gvConfiguracion.DataBind()
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub

    Private Sub Limpiar()
        
        Me.txtNombre.Text = ""
        Me.txtDescripcion.Text = ""
        Me.ddlInstancia.SelectedValue = 0
        Me.ddlResponsable.SelectedValue = 0
        Me.txtDias.Text = ""
    End Sub

    Protected Sub lbCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelar.Click
        Me.Lista.Visible = True
        Me.DivMantenimiento.Visible = False
        Me.txtDescripcion.Text = ""
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

    End Sub


    Protected Sub gvLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvLista.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Try
            If (e.CommandName = "Editar") Then
                Limpiar()
                Me.Lista.Visible = False
                Me.DivMantenimiento.Visible = True
                Me.hdc.Value = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_soc")
                Me.txtNombre.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("nombre_soc")
                Me.txtDescripcion.Text = Me.gvLista.DataKeys(e.CommandArgument).Values("descripcion_soc")
                ListarConfiguracion(Me.hdc.Value)
                Me.txtOrden.Text = Me.gvConfiguracion.Rows.Count + 1
            End If
            If (e.CommandName = "Eliminar") Then
                Dim codigo As Integer = Me.gvLista.DataKeys(e.CommandArgument).Values("codigo_soc")
                EliminarServicio(codigo, Session("id_per"))
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading", "fnLoading(false)", True)

        Catch ex As Exception

            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
    End Sub

    Private Sub Actualizar(ByVal codigo As Integer, ByVal nombre As String, ByVal descripcion As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ActualizarServicioOrientacion]", codigo, nombre, descripcion, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Dim codigos As String = ""
            Dim dtc As New Data.DataTable
            dtc = DirectCast(ViewState("Grilla"), Data.DataTable)
            'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert321", "fnMensaje('success','" + dtc.Rows.Count.ToString + "')", True)

            For i As Integer = 0 To dtc.Rows.Count - 1
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert431", "fnMensaje('success','" + dtc.Rows(i).Item("codigo_cso").ToString + "')", True)
                If dtc.Rows(i).Item("codigo_cso") > 0 Then
                    codigos += dtc.Rows(i).Item("codigo_cso").ToString + ","
                End If
            Next
            EliminarConfiguracion(dt.Rows(0).Item("cod"), codigos, Session("id_per"))


            For i As Integer = 0 To dtc.Rows.Count - 1
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert431", "fnMensaje('success','" + dtc.Rows(i).Item("codigo_cso").ToString + "')", True)
                If CInt(dtc.Rows(i).Item("codigo_cso")) = 0 Then
                    GuardarConfiguracion(dt.Rows(0).Item("cod"), dtc.Rows(i).Item("codigo_ioc"), dtc.Rows(i).Item("codigo_tfu"), dtc.Rows(i).Item("diasatencion_cso"), dtc.Rows(i).Item("orden_cso"), Session("id_per"))
                End If
            Next
            'Eliminar los que fueron quitados

            Me.DivMantenimiento.Visible = False
            Me.Lista.Visible = True
            Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert1", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading1", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If

    End Sub

    Private Function ValidarServicio() As Boolean
        If Me.txtNombre.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese nombre de Servicio')", True)
            Return False
        End If
        Return True
    End Function

    Protected Sub lbGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If ValidarServicio() = True Then
            Actualizar(Me.hdc.Value, Me.txtNombre.Text, Me.txtDescripcion.Text, Session("id_per"))
        End If
        Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading5", "fnLoading(false)", True)

    End Sub


    Private Sub EliminarServicio(ByVal codigo As Integer, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_EliminarServicio]", codigo, usuario)
        obj.CerrarConexion()
        If dt.Rows(0).Item("Respuesta") = 1 Then
            Listar()
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        End If
    End Sub

    Function ValidarConfiguracion() As Boolean
        If Me.ddlInstancia.SelectedValue = "0" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Debe seleccionar un nivel de atención')", True)
            Return False
        End If
        If Me.txtDias.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese el número de dias de atención')", True)
            Return False
        End If
        If Me.ddlResponsable.SelectedValue = "0" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Debe seleccionar un rol responsable')", True)
            Return False
        End If

        If Me.txtOrden.Text.Trim = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','Ingrese el orden del nivel de atención')", True)
            Return False
        End If

        Dim dt As New Data.DataTable
        dt = ViewState("Grilla")
        Dim bandera As Integer = 0

        For i As Integer = 0 To dt.Rows.Count - 1
            If Me.ddlInstancia.SelectedValue = dt.Rows(i).Item("codigo_ioc") Then
                bandera = bandera + 1
            End If
        Next
        If bandera > 0 Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No puede agregar 2 veces el mismo nivel de atención')", True)
            Return False
        End If
        bandera = 0
        For i As Integer = 0 To dt.Rows.Count - 1
            If Me.txtOrden.Text = dt.Rows(i).Item("orden_cso").ToString Then
                bandera = bandera + 1
            End If
        Next
        If bandera > 0 Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "fnMensaje('error','No puede asignar el mismo orden 2 veces')", True)
            Return False
        End If

        Return True
    End Function

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        If ValidarConfiguracion() = True Then
            Dim dt As Data.DataTable = DirectCast(ViewState("Grilla"), Data.DataTable)
            dt.Rows.Add(0, Me.hdc.Value, Me.ddlInstancia.SelectedValue, Me.ddlInstancia.SelectedItem.Text, Me.ddlResponsable.SelectedValue, Me.ddlResponsable.SelectedItem.Text, Me.txtDias.Text, Me.txtOrden.Text)

            ViewState("Grilla") = dt
            gvConfiguracion.DataSource = dt
            gvConfiguracion.DataBind()
            Me.ddlInstancia.SelectedValue = 0
            Me.ddlResponsable.SelectedValue = 0
            Me.txtDias.Text = ""
            Me.txtOrden.Text = dt.Rows.Count + 1
        End If
    End Sub

    Private Sub ListarConfiguracion(ByVal codigo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_ListarConfiguracionServicioOrientacion]", codigo)
        obj.CerrarConexion()
        ViewState("Grilla") = dt
        If dt.Rows.Count > 0 Then
            Me.gvConfiguracion.DataSource = dt
            Me.gvConfiguracion.DataBind()
        Else
            Me.gvConfiguracion.DataSource = Nothing
            Me.gvConfiguracion.DataBind()
        End If

    End Sub

    Protected Sub gvConfiguracion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvConfiguracion.RowCommand
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        Dim dt As New Data.DataTable
        dt = DirectCast(ViewState("Grilla"), Data.DataTable)
        Try

            If (e.CommandName = "Eliminar") Then
                'Dim codigo As Integer = Me.gvConfiguracion.DataKeys(e.CommandArgument).Values("codigo_cso")

                'If codigo = 0 Then
                dt.Rows.RemoveAt(e.CommandArgument)
                ViewState("Grilla") = dt
                Me.gvConfiguracion.DataSource = dt
                Me.gvConfiguracion.DataBind()
                Me.txtOrden.Text = dt.Rows.Count + 1
                'Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading14", "fnMensaje('success','" + dt.Rows.Count.ToString + "')", True)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading14", "fnMensaje('success','Configuración quitada correctamente, deberá guardar para aplicar los cambios')", True)
                'Else
                'EliminarConfiguracion(codigo, Session("id_per"))
                'End If
            End If
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading9", "fnLoading(false)", True)
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + " - " + e.CommandArgument.ToString + " - " + dt.Rows.Count.ToString + "')", True)
        End Try

    End Sub


    Private Sub EliminarConfiguracion(ByVal codigo_soc As Integer, ByVal codigos_NO_Eliminar As String, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_EliminarConfiguracionServicio]", codigo_soc, codigos_NO_Eliminar, usuario)
        obj.CerrarConexion()
        'If dt.Rows(0).Item("Respuesta") = 1 Then
        '    ListarConfiguracion(Me.hdc.Value)
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
        'Else
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        'End If
    End Sub


    Private Sub GuardarConfiguracion(ByVal codigo_soc As Integer, ByVal codigo_ioc As Integer, ByVal codigo_tfu As Integer, ByVal diasatencion As Integer, ByVal orden As Integer, ByVal usuario As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("[OAC_GuardarConfiguracionServicio]", codigo_soc, codigo_ioc, codigo_tfu, diasatencion, orden, usuario)
        obj.CerrarConexion()
        'If dt.Rows(0).Item("Respuesta") = 1 Then
        '    ListarConfiguracion(Me.hdc.Value)
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert3", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje") + "')", True)
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "Loading2", "fnLoading(false)", True)
        'Else
        '    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert4", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje") + "')", True)
        'End If
    End Sub



End Class

