Partial Class Egresado_frmListaOfertas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            CargaCarreras()
        End If
    End Sub

    Private Sub CargaCarreras()
        Dim obj As New ClsConectarDatos
        Dim dtCarrera As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '#Escuela
        dtCarrera = New Data.DataTable
        If Request.QueryString("ctf") = 145 Then
            dtCarrera = obj.TraerDataTable("ALUMNI_ListarCarreraProfesionalPersonal", 2, Request.QueryString("ID"), "%")
        Else
            dtCarrera = obj.TraerDataTable("ALUMNI_ListarCarreraProfesional")
        End If
        Me.dpCarrera.DataSource = dtCarrera
        Me.dpCarrera.DataTextField = "nombre"
        Me.dpCarrera.DataValueField = "codigo"
        Me.dpCarrera.DataBind()
        dtCarrera.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaGridCabecera()
        Me.fradetalle.Visible = False
    End Sub
    Private Sub CargaGridCabecera()
        Dim dtOfertas As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        If Request.QueryString("ctf") = 145 Then
            If dpCarrera.SelectedItem.ToString.Trim = "TODOS" Then
                '--Busqueda Cuando Selecciona Todos, pero la persona es Director de Escuela
                dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasxCarrera", Request.QueryString("ID"), Me.dpEstado.SelectedValue, "PERS")
            Else
                '--Busqueda Cuando Selecciona Una Carrera
                dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasxCarrera", Me.dpCarrera.SelectedValue, Me.dpEstado.SelectedValue, "CAPR")
            End If
        Else
            If dpCarrera.SelectedItem.ToString.Trim = "TODOS" Then
                '--Busqueda Cuando Selecciona Todos y es Administrador o Coordinador
                dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasxCarrera", "%", Me.dpEstado.SelectedValue, "TODO")
            Else
                '--Busqueda Cuando Selecciona Una Carrera
                dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasxCarrera", Me.dpCarrera.SelectedValue, Me.dpEstado.SelectedValue, "CAPR")
            End If
        End If

        ''dtOfertas = obj.TraerDataTable("ALUMNI_ListaOfertasxCarrera", Me.dpCarrera.SelectedValue, Me.dpEstado.SelectedValue)
        obj.CerrarConexion()
        Me.gvwOfertas.DataSource = dtOfertas
        Me.gvwOfertas.DataBind()
        dtOfertas.Dispose()
        obj = Nothing

        Call wf_ocultarColumnaGridView()
    End Sub

    Sub wf_ocultarColumnaGridView()
        If Request.QueryString("ctf") = 145 Then
            If Me.dpEstado.SelectedValue = "R" Then
                Me.gvwOfertas.Columns(6).Visible = False
                Me.gvwOfertas.Columns(7).Visible = True
            Else
                Me.gvwOfertas.Columns(6).Visible = False
                Me.gvwOfertas.Columns(7).Visible = False
            End If
        Else
            If Me.dpEstado.SelectedValue = "R" Then
                Me.gvwOfertas.Columns(6).Visible = True
            Else
                Me.gvwOfertas.Columns(6).Visible = False
            End If
            Me.gvwOfertas.Columns(7).Visible = True
        End If
    End Sub

    Private Sub EnviarAPagina(ByVal pagina As String)
        Me.fradetalle.Attributes("src") = pagina
    End Sub
    Protected Sub lnkDetalles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDetalles.Click
        If (Me.HdCodigo_Ofe.Value <> "") Then
            EnviarAPagina("DetOfertas.aspx?ofe=" & Me.HdCodigo_Ofe.Value)
        End If
    End Sub
    Protected Sub lnkAlumnos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAlumnos.Click
        EnviarAPagina("DetAlumnos.aspx?cpf=" & Me.dpCarrera.SelectedValue & "&ofe=" & Me.HdCodigo_Ofe.Value)
    End Sub
    Protected Sub gvwOfertas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvwOfertas.RowDeleting
        'If (MsgBox("¿Esta seguro que desea rechazar esta oferta?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "CAMPUS VIRTUAL") = MsgBoxResult.Yes) Then

        'OnClientClick="javascript:if(!confirm('¿Borrar seleccionado?'))return false"
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_actualizaEstadoOferta", Me.gvwOfertas.DataKeys.Item(e.RowIndex).Values(0), "E")
        obj.CerrarConexion()
        obj = Nothing

        CargaGridCabecera()
        'End If        
    End Sub


    Protected Sub gvwOfertas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvwOfertas.SelectedIndexChanged
        Me.HdCodigo_Ofe.Value = Me.gvwOfertas.DataKeys.Item(Me.gvwOfertas.SelectedIndex).Values(0)
        lnkDetalles_Click(sender, e)
        If (gvwOfertas.Rows.Count <> 0) Then
            Me.fradetalle.Visible = True
        End If

    End Sub


    Protected Sub gvwOfertas_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvwOfertas.RowEditing
        'If (MsgBox("¿Esta seguro que desea activar esta oferta?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "CAMPUS VIRTUAL") = MsgBoxResult.Yes) Then
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ALUMNI_actualizaEstadoOferta", Me.gvwOfertas.DataKeys.Item(e.NewEditIndex).Values(0), "A")
        obj.CerrarConexion()
        obj = Nothing

        CargaGridCabecera()
        'End If
    End Sub


    Protected Sub gvwOfertas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvwOfertas.PageIndexChanging
        gvwOfertas.PageIndex = e.NewPageIndex()
        CargaGridCabecera()
    End Sub


    Protected Sub gvwOfertas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvwOfertas.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then

        End If
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'Response.Redirect("frmOfertaLaboral.aspx?id=" & Request.QueryString("id"))
        Response.Redirect("frmOfertaLaboral.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub btnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModificar.Click
        If (Me.gvwOfertas.SelectedDataKey IsNot Nothing) Then
            'Response.Redirect("frmOfertaLaboral.aspx?of=" & Me.gvwOfertas.SelectedDataKey.Values(0))
            Response.Redirect("frmOfertaLaboral.aspx?of=" & Me.gvwOfertas.SelectedDataKey.Values(0) & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
        Else
            Response.Write("<script>alert('Debe seleccionar una oferta laboral')</script>")
        End If
    End Sub
End Class
