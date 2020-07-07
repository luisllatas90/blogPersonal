Partial Class frminscripcionevento
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim objfun As New ClsFunciones
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(cboCecos, obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), "", Request.QueryString("mod")), "codigo_Cco", "Nombre", ">> Seleccione<<")
            obj.CerrarConexion()
            obj = Nothing
            objfun = Nothing

            'Cargar módulos

            Select Case Request.QueryString("mod")
                Case 0 'Otros
                    Me.lblTitulo.Text = "Inscripciones"
                Case 1 'Epre
                    Me.lblTitulo.Text = "Inscripción a Escuela PreUniversitaria"
                Case 2 'Pregrado
                    Me.lblTitulo.Text = "Inscripción a Escuelas de PreGrado"
                Case 3 'Profesionalización
                    Me.lblTitulo.Text = "Inscripción a Programas de Profesionalización"
                Case 4 'Complementarios
                    Me.lblTitulo.Text = "Inscripción a Centro de Idiomas y Complementarios"
                Case 5 'PostGrado
                    Me.lblTitulo.Text = "Inscripción a Escuela de PostGrado"
                Case 6 'Educación Contínua
                    Me.lblTitulo.Text = "Inscripción a Programas de Educación Contínua"
                Case Else
                    Me.lblTitulo.Text = "Inscripción a eventos registrados"
            End Select
        End If
    End Sub
    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        trResultados.Visible = False
        lnkBusquedaAvanzada.Text = "Búsqueda Avanzada"
        MostrarPanelesTab()
    End Sub
    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.cmdBuscar.Visible = valor
        Me.cboCecos.Visible = Not (valor)
        'trResultados.Visible = (valor)
    End Sub
    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        Me.tabs.Visible = False
        Me.lblMensaje.Visible = False
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
        Me.txtBuscaCecos.Focus()
    End Sub
    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        MostrarPanelesTab()
    End Sub
    Private Sub MostrarPanelesTab()
        Me.tabs.Visible = False
        Me.lblMensaje.Visible = False
        If cboCecos.SelectedValue <> -1 Then
            '
            Dim tipoconsulta As Byte
            If Request.QueryString("mod") = 1 Then
                tipoconsulta = 9
            Else
                tipoconsulta = 4
            End If

            'Cargar información del Evento
            Dim obj As New ClsConectarDatos

            Dim tbl As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            tbl = obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", tipoconsulta, Me.cboCecos.SelectedValue, 0, 0)
            obj.CerrarConexion()
            obj = Nothing
            'No encuentra datos del evento. Obliga a registrar.
            If tbl.Rows.Count = 0 Then
                Me.tabs.Visible = False
                Me.lblMensaje.Text = "No se han registrado Datos del Evento aún. Coordinar con la Oficina de Presupuestos, Organización y Métodos"
                Me.lblMensaje.Visible = True
                Exit Sub
            Else
                'Verificar planes si gestiona notas
                If tbl.Rows(0).Item("planes") = 0 And CBool(tbl.Rows(0).Item("gestionanotas_dev")) = True Then
                    Me.lblMensaje.Text = "El evento seleccionado NO TIENE registado un Plan de Estudio. El coordinador del evento debe realizar dicho registro."
                    Me.lblMensaje.Visible = True
                    Exit Sub
                End If

                'Verificar servicios
                If tbl.Rows(0).Item("servicios").ToString = "0" Then
                    Me.lblMensaje.Text = "El evento seleccionado NO TIENE items asociados al Centro de Costos. Realizar las coordinaciones con el área de Contabilidad para su creación."
                    Me.lblMensaje.Visible = True
                    Exit Sub
                End If
            End If

            'Mostrar Tabs para trabjar
            Me.lnkDatosEvento.Visible = True
            Me.lnkDatosEvento.Visible = True
            Me.tabs.Visible = True
            EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod"))
        End If
    End Sub
    Protected Sub lnkDatosEvento_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDatosEvento.Click
        EnviarAPagina("detalleevento.aspx?mod=" & Request.QueryString("mod"))
    End Sub
    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Me.cboCecos.SelectedValue
    End Sub
    Protected Sub lnkInscripciones_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkInscripciones.Click
        Select Case Request.QueryString("mod")
            Case 1
                EnviarAPagina("lstinscritoseventocargo.aspx?mod=1") 'En el caso se envie a otra página
            Case Else
                EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod"))
        End Select
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        trResultados.Visible = False
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        gvCecos.DataSource = obj.TraerDataTable("EVE_ConsultarCentroCostosXPermisos", Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text.Trim, Request.QueryString("mod"))
        gvCecos.DataBind()
        obj = Nothing

        If gvCecos.Rows.Count > 0 Then
            trResultados.Visible = True
        End If
    End Sub
End Class