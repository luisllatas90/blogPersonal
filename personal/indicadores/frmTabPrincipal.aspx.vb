
Partial Class indicadores_frmTabPrincipal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            MostrarPanelesTab()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub MostrarPanelesTab()
        Me.tabs.Visible = True
        'Me.lblMensaje.Visible = False
        'If cboCecos.SelectedValue <> -1 Then
        '    '
        '    Dim tipoconsulta As Byte
        '    If Request.QueryString("mod") = 1 Then
        '        tipoconsulta = 9
        '    Else
        '        tipoconsulta = 4
        '    End If

        '    'Cargar información del Evento
        '    Dim obj As New ClsConectarDatos

        '    Dim tbl As Data.DataTable
        '    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        '    obj.AbrirConexion()

        '    tbl = obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", tipoconsulta, Me.cboCecos.SelectedValue, 0, 0)
        '    obj.CerrarConexion()
        '    obj = Nothing
        '    'No encuentra datos del evento. Obliga a registrar.
        '    If tbl.Rows.Count = 0 Then
        '        Me.tabs.Visible = False
        '        Me.lblMensaje.Text = "No se han registrado Datos del Evento aún. Coordinar con la Oficina de Presupuestos, Organización y Métodos"
        '        Me.lblMensaje.Visible = True
        '        Exit Sub
        '    Else
        '        'Verificar planes si gestiona notas
        '        If tbl.Rows(0).Item("planes") = 0 And CBool(tbl.Rows(0).Item("gestionanotas_dev")) = True Then
        '            Me.lblMensaje.Text = "El evento seleccionado NO TIENE registado un Plan de Estudio. El coordinador del evento debe realizar dicho registro."
        '            Me.lblMensaje.Visible = True
        '            Exit Sub
        '        End If

        '        'Verificar servicios
        '        If tbl.Rows(0).Item("servicios").ToString = "0" Then
        '            Me.lblMensaje.Text = "El evento seleccionado NO TIENE items asociados al Centro de Costos. Realizar las coordinaciones con el área de Contabilidad para su creación."
        '            Me.lblMensaje.Visible = True
        '            Exit Sub
        '        End If
        '    End If

        'Mostrar Tabs para trabjar
        '    Me.tabs.Visible = True
        '    'EnviarAPagina("lstinscritoseventocargo.aspx?mod=" & Request.QueryString("mod"))
        '    EnviarAPagina("frmListaPreInscritos.aspx?mod=" & Request.QueryString("mod"))
        'End If
    End Sub

End Class
