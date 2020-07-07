Imports System.Data
Partial Class ListadoConsultas
    Inherits System.Web.UI.Page

    Private Sub ListarTipoEstudio()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ACAD_ConsultarTipoEstudio", "MV", Request("ctf"))
        Me.ddlTipoEstudio.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlTipoEstudio.Items.Add(New ListItem(tb.Rows(i).Item("descripcion_test"), tb.Rows(i).Item("codigo_test")))
            Next
            Me.ddlTipoEstudio.Items.Add(New ListItem("TODOS", "%"))
            Me.ddlTipoEstudio.SelectedValue = "%"
        End If
        obj.CerrarConexion()
    End Sub

    Private Sub ListarCarreras()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("MV_ConsultaCarreraxAccesoRecurso", Me.ddlTipoEstudio.SelectedValue, Session("id_per"), Request("ctf"), "%")

        'Me.ddlCarrera.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If tb.Rows.Count > 0 Then
            For i As Integer = 0 To tb.Rows.Count - 1
                Me.ddlCarrera.Items.Add(New ListItem(tb.Rows(i).Item("nombre_cpf"), tb.Rows(i).Item("codigo_cpf")))
            Next
            'If Request("ctf") = 1 Or Request("ctf") = 181 Then ' ADMINISTRADOR, DIR. ACADEMICA
            'If Request("ctf") <> 9 And Request("ctf") <> 222 And Request("ctf") <> 143 And Request("ctf") <> 159 Then
            ' DIFERENTE A DIRECTOR DE ESCUELA / COORDINADOR DE POSTGRADO / COORDINADOR GENERAL DE PROFESIONALIZACION/GO / COORDINADOR DE EDUC. CONTINUA
            Me.ddlCarrera.Items.Add(New ListItem("TODOS", "%"))
            Me.ddlCarrera.SelectedValue = "%"
            'End If

        End If
        obj.CerrarConexion()
    End Sub


    Private Sub ListarInstancias(ByVal codigo_test As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("MV_ListarInstancias", "E", codigo_test)
        Me.ddlInstancia.Items.Clear()
        Me.ddlInstancia.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                Me.ddlInstancia.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_tfu"), dt.Rows(i).Item("codigo_iin")))
            Next
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                ListarTipoEstudio()
                ListarCarreras()
                If Request("ctf") = 1 Or Request("ctf") = 181 Then ' ADMINISTRADOR, DIR. ACADEMICA
                    Me.ddlEstado.Items.Add(New ListItem("PENDIENTES", "P"))
                    Me.ddlEstado.Items.Add(New ListItem("DERIVADOS", "D"))
                    Me.ddlEstado.Items.Add(New ListItem("FINALIZADOS", "F"))
                    Me.ddlEstado.Items.Add(New ListItem("TODOS", "%"))
                    Me.ddlEstado.SelectedValue = "P"
                    Me.DivColorDerivar.Visible = True
                Else
                    Me.ddlEstado.Items.Add(New ListItem("PENDIENTES", "D"))
                    Me.ddlEstado.Items.Add(New ListItem("FINALIZADOS", "F"))
                    Me.ddlEstado.Items.Add(New ListItem("TODOS", "%"))
                    Me.ddlEstado.SelectedValue = "D"
                    Me.DivColorDerivar.Visible = False
                End If


                ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)

            Else
                mt_RefreshGrid()
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ConsultarIncidencias(ByVal codigo_Test As String, ByVal codigo_cpf As String, ByVal codigo_per As Integer, ByVal estado As String)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("MV_ListarIncidencias", codigo_Test, codigo_cpf, estado, Session("id_per"), Request("ctf"), Me.txtBusqueda.Text.Trim)
        If tb.Rows.Count > 0 Then
            Me.gvIncidencias.DataSource = tb
        Else
            Me.gvIncidencias.DataSource = ""
        End If
        Me.gvIncidencias.DataBind()
        obj.CerrarConexion()
    End Sub

    Private Sub ConsultarDetalleIncidencias(ByVal codigo_inc As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("MV_ListarDetalleIncidencia", codigo_inc)
        If tb.Rows.Count > 0 Then
            Me.gvDetalle.DataSource = tb
        Else
            Me.gvDetalle.DataSource = ""
        End If
        Me.gvDetalle.DataBind()
        obj.CerrarConexion()
    End Sub

    Protected Sub gvIncidencias_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvIncidencias.RowCommand
        If e.CommandName = "Ver" Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular", "MostrarModal('mdEditar');", True)
            Me.DivResponder.Visible = False
            Me.btnDerivar.Visible = False
            Me.btnRespuesta.CssClass = "btn btn-primary"
            Me.btnRespuesta.Text = "Responder"
            Me.btnRespuesta.Attributes.Remove("onclick")
            Me.ddlInstancia.SelectedValue = ""
            ListarInstancias(Me.gvIncidencias.DataKeys(e.CommandArgument).Values("codigo_test"))
            Limpiar()
            If Me.gvIncidencias.DataKeys(e.CommandArgument).Values("estado_inc") = "F" Or (Me.gvIncidencias.DataKeys(e.CommandArgument).Values("codigo_tfu") <> Request("ctf") And Me.gvIncidencias.DataKeys(e.CommandArgument).Values("estado_inc") = "D") Then
                Me.btnRespuesta.Visible = False
            Else
                Me.btnRespuesta.Visible = True
            End If
            CargarDatos(Me.gvIncidencias.DataKeys(e.CommandArgument).Values("codigo_inc"))
            ConsultarDetalleIncidencias(Me.gvIncidencias.DataKeys(e.CommandArgument).Values("codigo_inc"))
        End If
    End Sub

    Private Sub CargarDatos(ByVal codigo As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("MV_VerIncidencia", codigo)
        If dt.Rows.Count > 0 Then
            Me.hdi.Value = codigo
            Me.txtCodigo.Text = dt.Rows(0).Item("glosacorrelativo_inc").ToString
            Me.txtDocIdent.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString
            Me.txtFechaRegistro.Text = dt.Rows(0).Item("fecha").ToString
            Me.txtcodigouniver.Text = dt.Rows(0).Item("codigoUniver_Alu").ToString
            Me.txtestudiante.Text = dt.Rows(0).Item("alumno").ToString
            Me.txtCarrera.Text = dt.Rows(0).Item("nombre_cpf").ToString
            Me.txtAsunto.Text = dt.Rows(0).Item("asunto_inc").ToString
            Me.txtDescripcion.Text = dt.Rows(0).Item("descripcion_inc").ToString
            Me.txttelefono.Text = dt.Rows(0).Item("telefono").ToString
            Me.txtemail.Text = dt.Rows(0).Item("correo").ToString
            If dt.Rows(0).Item("archivo").ToString <> "" Then
                lblArchivo.Text = "Descargar Adjunto"
                lblArchivo.Attributes.Add("onclick", "fnDescargar('" + dt.Rows(0).Item("archivo") + "'); return false")
                lblArchivo.ForeColor = Drawing.Color.Blue
            Else
                lblArchivo.Text = "No tiene archivo adjunto"
                lblArchivo.Attributes.Remove("onclick")
                lblArchivo.ForeColor = Drawing.Color.Red
            End If
        End If
        obj.CerrarConexion()
    End Sub


    Protected Sub btnRespuesta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRespuesta.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        mt_RefreshGrid()
        If Me.btnRespuesta.Text = "Responder" Then
            Me.btnRespuesta.CssClass = "btn btn-success"
            Me.btnRespuesta.Attributes.Add("onclick", "return confirm('¿Está seguro que desea guardar la respuesta?')")
            Me.DivResponder.Visible = True
            Me.btnRespuesta.Text = "Enviar Respuesta"
            If Request("ctf") = 1 Or Request("ctf") = 181 Then
                Me.btnDerivar.Visible = True
            Else
                Me.btnDerivar.Visible = False
            End If
        Else
            If Me.txtRespuesta.Text <> "" And Me.txtRespuesta.Text.Length <= 5000 Then
                GuardarRespuesta(Me.hdi.Value, Me.txtRespuesta.Text, "F", 0, Session("id_per"))
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese correctamente respuesta máximo 5000 caracteres')", True)
            End If
        End If
    End Sub

    Private Sub GuardarRespuesta(ByVal codigo_inc As Integer, ByVal descripcion As String, ByVal estado As String, ByVal codigo_iin As Integer, ByVal codigo_per As Integer)
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim dt As New Data.DataTable
        dt = obj.TraerDataTable("MV_GuardarRespuestaIncidencia", codigo_inc, descripcion, estado, codigo_iin, codigo_per)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("Respuesta") = "1" Then
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('success','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "CerrarModal('mdEditar')", True)
                If estado = "D" Then
                    Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert2", "CerrarModal('mdDerivar')", True)
                End If
                If Me.ddlTipoEstudio.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" And Me.ddlEstado.SelectedValue <> "" Then
                    ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)
                End If
                Limpiar()
            Else
                Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + dt.Rows(0).Item("Mensaje").ToString + "')", True)
            End If
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','No se pudo realizar la operación')", True)
        End If
        obj.CerrarConexion()
    End Sub

    Protected Sub btnDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDerivar.Click
        If Me.txtRespuesta.Text <> "" And Me.txtRespuesta.Text.Length <= 5000 Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "MostrarmodalVIncular", "MostrarModal('mdDerivar');", True)
            Me.ddlInstancia.SelectedValue = ""
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Ingrese correctamente respuesta máximo 5000 caracteres')", True)
        End If
    End Sub

    Protected Sub btnGuardarDerivar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarDerivar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If Me.ddlInstancia.SelectedValue <> "" Then
            GuardarRespuesta(Me.hdi.Value, Me.txtRespuesta.Text, "D", Me.ddlInstancia.SelectedValue, Session("id_per"))
        Else
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione la Instancia')", True)
        End If
    End Sub
    Private Sub Limpiar()
        Me.hdi.Value = 0
        Me.txtRespuesta.Text = ""
        lblArchivo.Text = ""
        lblArchivo.Attributes.Remove("onclick")
        Me.txtemail.Text = ""
        Me.txtCarrera.Text = ""
        Me.txtCodigo.Text = ""
        Me.txtRespuesta.Text = ""
        Me.txtcodigouniver.Text = ""
        Me.txtFechaRegistro.Text = ""
    End Sub

    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If

        Me.gvIncidencias.DataSource = Nothing
        Me.gvIncidencias.DataBind()
        Limpiar()
        Me.ddlCarrera.Items.Clear()
        Me.ddlCarrera.Items.Add(New ListItem("[-- SELECCIONE --]", ""))
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            ListarCarreras()
        End If
        Me.txtBusqueda.Text = ""
        If Me.ddlTipoEstudio.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" And Me.ddlEstado.SelectedValue <> "" Then
            ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.gvIncidencias.DataSource = Nothing
        Me.gvIncidencias.DataBind()
        Limpiar()
        Me.txtBusqueda.Text = ""
        If Me.ddlTipoEstudio.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" And Me.ddlEstado.SelectedValue <> "" Then
            ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.gvIncidencias.DataSource = Nothing
        Me.gvIncidencias.DataBind()
        Limpiar()
        Me.txtBusqueda.Text = ""
        If Me.ddlTipoEstudio.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" And Me.ddlEstado.SelectedValue <> "" Then
            ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.gvIncidencias.DataSource = Nothing
        Me.gvIncidencias.DataBind()
        Limpiar()
        If Me.ddlTipoEstudio.SelectedValue <> "" And Me.ddlCarrera.SelectedValue <> "" And Me.ddlEstado.SelectedValue <> "" Then
            ConsultarIncidencias(Me.ddlTipoEstudio.SelectedValue, Me.ddlCarrera.SelectedValue, Session("id_per"), Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Protected Sub gvIncidencias_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvIncidencias.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim estado As String = Me.gvIncidencias.DataKeys(e.Row.RowIndex).Values("estado_inc").ToString

            If estado = "P" Or (estado = "D" And Request("ctf") <> 1 And Request("ctf") <> 181) Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#f9d0d0")
            End If
            If estado = "D" And (Request("ctf") = 1 Or Request("ctf") = 181) Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#fcffd0")
            End If
            If estado = "F" Then
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#C8FFC8")
            End If
        End If
    End Sub
    Private Sub mt_RefreshGrid()
        Try
            For Each _Row As GridViewRow In Me.gvIncidencias.Rows
                gvIncidencias_RowDataBound(Me.gvIncidencias, New GridViewRowEventArgs(_Row))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

