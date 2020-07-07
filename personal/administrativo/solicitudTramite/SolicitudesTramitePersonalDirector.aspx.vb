Imports System.IO
Imports System.Security.Cryptography

Partial Class _SolicitudesTramitePersonalDirector
    Inherits System.Web.UI.Page
    Dim ctf As Integer
    Dim mes As Integer
    Dim anio, estado, prioridad, tipo_tram, trabajador As String
    Dim j As Integer
    Dim respuesta As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Session("id_per") Is Nothing) Then

            Response.Redirect("../../../sinacceso.html")

        End If

        Session("codigo_ctf") = Request.QueryString("ctf")

        If Not IsPostBack Then

            If Request.QueryString("anio") <> "" Then
                Me.ddlAño.SelectedValue = Request.QueryString("anio")
            End If
            If Request.QueryString("mes") <> "" Then
                Me.ddlMes.SelectedValue = Request.QueryString("mes")
            End If
            If Request.QueryString("estado") <> "" Then
                Me.ddlEstado.SelectedValue = Request.QueryString("estado")
            End If
            If Request.QueryString("prioridad") <> "" Then
                Me.ddlPrioridad.SelectedValue = Request.QueryString("prioridad")
            End If

            ConsultarTipoSolicitud()

            If Request.QueryString("tipo_tram") <> "" Then
                Me.ddlTipoSolicitud.SelectedValue = Request.QueryString("tipo_tram")
            End If

            If Request.QueryString("trabajador") <> "" Then
                Me.txtTrabajador.Text = Request.QueryString("trabajador")
            End If

            If Request.QueryString("respuesta") <> "" Then
                respuesta = Request.QueryString("respuesta")
            End If

            lblMensaje.Visible = False 'Se añadió

            ConsultarAño() '02/01/2020

            ConsultarListaSolicitudes()

        End If
    End Sub

    Public Sub ConsultarTipoSolicitud()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ListaTipoSolicitudTramite", "T")
            obj.CerrarConexion()

            Me.ddlTipoSolicitud.DataTextField = "nombre_TST"
            Me.ddlTipoSolicitud.DataValueField = "codigo_TST"
            Me.ddlTipoSolicitud.DataSource = dt
            Me.ddlTipoSolicitud.DataBind()

            'Me.ddlTipoSolicitud.Items.Add("TODOS")            
            'Me.ddlTipoSolicitud.SelectedIndex = 3 'Permisos

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los Tipos de Solicitud Trámite"
        End Try
    End Sub

    Public Sub ConsultarListaSolicitudes()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""
        limpiar_mensajes()

        registrar_filtros()

        j = 0

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ListaSolicitudTramiteColaboradores", anio, mes, CInt(Session("id_per")), estado, prioridad, tipo_tram, Trim(Me.txtTrabajador.Text))

            If dt.Rows.Count > 0 Then
                Me.gvCarga.DataSource = dt
                Me.gvCarga.DataBind()

                btnExporta.enabled = True 'Se añadió

            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                'Me.celdaGrid.Visible = True
                'Me.celdaGrid.InnerHtml = "** AVISO :  No Existen Solicitudes con los parámetros seleccionados"
                lblMensaje.Visible = True 'Se añadió
                Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"

                btnExporta.enabled = False 'Se añadió

            End If
            obj.CerrarConexion()

            'Me.gvCarga.DataSource = Nothing
            'Me.gvCarga.DataBind()
            'Me.celdaGrid.Visible = True

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Public Sub ConsultarAño()

        Dim i As Integer

        Try
            For i = 2018 To year(System.DateTime.Today)

                Me.ddlAño.Items.Add(i)
                Me.ddlAño.text = i

            Next

        Catch ex As Exception
            Me.lblMensaje0.Text = "Error al cargar los años"
        End Try

    End Sub

    Private Sub registrar_filtros()

        anio = Me.ddlAño.SelectedValue
        mes = Me.ddlMes.SelectedValue
        estado = Me.ddlEstado.SelectedValue
        prioridad = Me.ddlPrioridad.SelectedValue
        tipo_tram = Me.ddlTipoSolicitud.SelectedValue

        trabajador = Trim(Me.txtTrabajador.Text)

    End Sub

    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub cboPrioridad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPrioridad.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub ddlTipoSolicitud_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoSolicitud.SelectedIndexChanged
        ConsultarListaSolicitudes()
    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'Dim chk As CheckBox = CType(e.Row.FindControl("CheckBox1"), CheckBox)
            'chk.Visible = False
            'e.Row.Cells(1).Text = e.Row.RowIndex + 1

            Dim cod, codi As Integer
            cod = fila.Row("codigo_ST")
            codi = fila.Row("codigo_EST")

            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            If e.Row.Cells(3).Text = "Urgente" Then
                e.Row.Cells(3).Font.Bold = False
                e.Row.Cells(3).ForeColor = System.Drawing.Color.Blue
                'e.Row.Cells(3).Attributes.Add("HyperLink", "../SolicitudTramite.aspx")
            End If

            e.Row.Cells(13).ForeColor = IIf(fila.Row("Estado") = "Pendiente", Drawing.Color.Red, Drawing.Color.Blue)
            e.Row.Cells(13).Font.Underline = True
            Dim pagina As String = "<a href='SolicitudTramitePersonal.aspx?cod=" & cod & "&codi=" & codi & "&anio=" & anio & "&mes=" & mes & "&estado=" & estado & "&prioridad=" & prioridad & "&tipo_tram=" & tipo_tram & "&trabajador=" & trabajador & "'>" & e.Row.Cells(13).Text & "</a>"
            'e.Row.Cells(11).Text = IIf(fila.Row("Estado") <> "Generado", "<a href='SolicitudTramite.aspx?cod=" + cod + "'>" + e.Row.Cells(11).Text + "</a>", e.Row.Cells(11).Text)
            e.Row.Cells(13).Text = pagina

            If fila.Row("Estado") <> "Pendiente" Then
                e.Row.Cells(0).Text = ""
            Else
                j = j + 1
            End If

        End If
    End Sub

    Private Sub CargaMeses()
        Me.ddlMes.Items.Add("TODOS")
        Me.ddlMes.Items.Add("Enero")
        Me.ddlMes.Items.Add("Febrero")
        Me.ddlMes.Items.Add("Marzo")
        Me.ddlMes.Items.Add("Abril")
        Me.ddlMes.Items.Add("Mayo")
        Me.ddlMes.Items.Add("Junio")
        Me.ddlMes.Items.Add("Julio")
        Me.ddlMes.Items.Add("Agosto")
        Me.ddlMes.Items.Add("Setiembre")
        Me.ddlMes.Items.Add("Octubre")
        Me.ddlMes.Items.Add("Noviembre")
        Me.ddlMes.Items.Add("Diciembre")
    End Sub

    Private Sub limpiar_mensajes()
        lblMensaje0.text = ""
        lblMensaje.text = ""
    End Sub

    Protected Sub Unnamed1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBuscar.Click, btnBuscar.Click

        Busqueda()

    End Sub

    Private Sub Busqueda()

        Me.lblMensaje0.Text = ""

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        'Me.celdaGrid.Visible = True
        'Me.celdaGrid.InnerHtml = ""

        If Trim(Me.txtTrabajador.Text) = "" Then
            ConsultarListaSolicitudes()
            Me.lblMensaje0.Text = "** Aviso: No ha ingresado el APELLIDO o NOMBRE del trabajador"
            Exit Sub
        End If

        'Response.Write(Trim(Me.txtTrabajador.Text))
        registrar_filtros()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("ListaSolicitudTramiteColaboradores", anio, mes, CInt(Session("id_per")), estado, prioridad, tipo_tram, Trim(Me.txtTrabajador.Text))

        If tb.Rows.Count Then
            Me.gvCarga.DataSource = tb
            Me.lblMensaje.Text = "" 'Se añadió

            btnExporta.enabled = True 'Se añadió

        Else
            Me.gvCarga.DataSource = Nothing
            Me.gvCarga.DataBind()
            lblMensaje.Visible = True 'Se añadió
            Me.lblMensaje.Text = "** AVISO :  No Existen Solicitudes con los Parámetros seleccionados"

            btnExporta.enabled = False 'Se añadió
        End If

        Me.gvCarga.DataBind()
        obj.CerrarConexion()
        obj = Nothing

    End Sub

    Protected Sub btnExporta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExporta.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvCarga.EnableViewState = False
        gvCarga.Visible = True
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvCarga)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"

        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        gvCarga.Visible = False
    End Sub

End Class
