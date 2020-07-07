Imports System.Data

Partial Class frmGestionComite
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            CargarDropDowLists()
            CreaDatatableMiembros()
        End If
    End Sub

    Private Sub CreaDatatableMiembros()
        If Session("dtMiembros") Is Nothing Then
            Dim dtMiembros As New Data.DataTable
            dtMiembros.Columns.Add("personal_id", GetType(String))
            dtMiembros.Columns.Add("nombre", GetType(String))
            dtMiembros.Columns.Add("coordinador", GetType(String))
            Session("dtMiembros") = dtMiembros
        End If
    End Sub

    Private Sub CargarDropDowLists()
        Dim objFun As New ClsFunciones
        Dim objInv As New clsInvestigacion
        Dim dt As New DataTable
        dt = objInv.ConsultaCentroCostos(0)
        objFun.CargarListas(ddlListaCentroCosto, dt, "codigo_Cco", "descripcion_Cco", "<< Seleccione >>")
        objFun.CargarListas(ddlACentroCosto, dt, "codigo_Cco", "descripcion_Cco", "<< Seleccione >>")
        objFun.CargarListas(ddlAPersonal, objInv.ConsultaPersonal(0), "personal_id", "nombrecompleto", "<< Seleccione >>")
        objFun.CargarListas(ddlAInstancia, objInv.ConsultaInstancia(0), "id", "nombre", "<< Seleccione >>")
    End Sub

    'Protected Sub ibtnAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnAgregar.Click
    '    If hfid.Value = "" Then
    '        AgregarMiembro(ddlAPersonal.SelectedValue, ddlAPersonal.SelectedItem.ToString, 0)
    '        ddlAPersonal.SelectedIndex = 0
    '    Else
    '        For Each row As GridViewRow In gvMiembros.Rows
    '            If row.Cells.Item(1).Text = ddlAPersonal.SelectedValue Then
    '                lblMensaje.Text = ddlAPersonal.SelectedItem.ToString + " ya fue registrado."
    '                Exit Sub
    '            End If
    '        Next
    '        Dim objInv As New clsInvestigacion
    '        objInv.AbrirTransaccionCnx()
    '        'Me.Response.Write(" Agregar " & ddlAInstancia.SelectedValue)
    '        objInv.AgregaMiembroComite(ddlAPersonal.SelectedValue, False, hfid.Value, ddlAInstancia.SelectedValue)
    '        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('1');", True)
    '        objInv.CerrarTransaccionCnx()
    '        gvMiembros.DataSource = objInv.ConsultaComiteMiembros(hfid.Value)
    '        gvMiembros.DataBind()
    '    End If
    'End Sub

    Private Sub AgregarMiembro(ByVal codigo_per As String, ByVal nombre As String, ByVal coordinador As Integer)
        Dim dtMiembros As New Data.DataTable
        dtMiembros = Session("dtMiembros")
        For Each row As Data.DataRow In dtMiembros.Rows
            If row("personal_id") = codigo_per Then
                lblMensaje.Text = nombre + " ya fue registrado."
                Exit Sub
            End If
        Next
        lblMensaje.Text = ""
        Dim myrow As Data.DataRow
        myrow = dtMiembros.NewRow
        myrow("personal_id") = codigo_per
        myrow("nombre") = nombre
        myrow("coordinador") = coordinador
        dtMiembros.Rows.Add(myrow)
        gvMiembros.DataSource = dtMiembros
        gvMiembros.DataBind()
    End Sub

    Private Sub QuitarMiembro(ByVal codigo_per As String)
        Dim dtMiembros As New Data.DataTable
        dtMiembros = Session("dtMiembros")
        For Each row As Data.DataRow In dtMiembros.Rows
            If row("personal_id") = codigo_per Then
                dtMiembros.Rows.Remove(row)
                Session("dtMiembros") = dtMiembros
                gvMiembros.DataSource = dtMiembros
                gvMiembros.DataBind()
                Exit Sub
            End If
        Next
    End Sub

    Protected Sub ibtnEliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ibtnEliminar As ImageButton
        Dim row As GridViewRow
        ibtnEliminar = sender
        row = ibtnEliminar.NamingContainer
        If hfid.Value = "" Then
            QuitarMiembro(row.Cells.Item(1).Text)
        Else
            Dim objInv As New clsInvestigacion
            objInv.AbrirTransaccionCnx()
            objInv.EliminaMiembroComite(row.Cells.Item(1).Text, hfid.Value)
            objInv.CerrarTransaccionCnx()
            gvMiembros.DataSource = objInv.ConsultaComiteMiembros(hfid.Value)
            gvMiembros.DataBind()
        End If
    End Sub

    Protected Sub gvMiembros_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvMiembros.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim output As Literal = CType(e.Row.FindControl("rbtnMarkup"), Literal)
            Dim str As String
            Try
                str = DataBinder.Eval(e.Row.DataItem, "coordinador").ToString
            Catch ex As Exception
                str = "False"
            End Try

            If str = "True" Then
                output.Text = String.Format("<input type=radio name=Grupo id=RowSelector{0} checked='checked' />", e.Row.RowIndex)
            Else
                output.Text = String.Format("<input type=radio name=Grupo id=RowSelector{0} value={0} />", e.Row.RowIndex)
            End If
        End If
    End Sub

    'Protected Sub ibtnNuevoComite_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnNuevoComite.Click
    '    pnlModificacion.Visible = True
    '    pnlListado.Visible = False
    '    chkEstado.Enabled = False
    '    chkEstado.Checked = True
    '    lblTitulo.Text = "Nuevo Comité"
    '    Limpiar()
    'End Sub

    'Protected Sub ibtnRegresar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnRegresar.Click
    '    pnlListado.Visible = True
    '    pnlModificacion.Visible = False
    'End Sub

    'Protected Sub ibtnGrabar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnGrabar.Click
    '    Try
    '        If hfid.Value = "" Then
    '            Guardar()
    '        Else
    '            Modificar()
    '        End If
    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Private Sub Guardar()
        Try
            Dim objInv As New clsInvestigacion
            Dim personal_id As Integer
            Dim id As Integer
            id = Request.QueryString("id")
            personal_id = gvMiembros.Rows(Convert.ToInt32(Request.Form("Grupo"))).Cells(1).Text
            objInv.AbrirTransaccionCnx()
            hfid.Value = objInv.AgregaComite(txtAComite.Text, id, ddlACentroCosto.SelectedValue, ddlAInstancia.SelectedValue, "", True)
            Dim dt As New DataTable
            dt = Session("dtMiembros")
            For Each row As Data.DataRow In dt.Rows
                If row("personal_id") = personal_id Then
                    objInv.AgregaMiembroComite(row("personal_id"), True, hfid.Value, ddlAInstancia.SelectedValue)
                Else
                    objInv.AgregaMiembroComite(row("personal_id"), False, hfid.Value, ddlAInstancia.SelectedValue)
                End If
            Next
            objInv.CerrarTransaccionCnx()
            Limpiar()
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se guardaron los datos correctamente.');", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmGestionComite.aspx?id=" & Request.QueryString("id") & "';", True)
        Catch ex As Exception
            'Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub Modificar()
        Dim objInv As New clsInvestigacion
        Dim coordinador As Integer
        Try
            coordinador = Convert.ToInt32(gvMiembros.Rows(Convert.ToInt32(Request.Form("Grupo"))).Cells(1).Text)
        Catch ex As Exception
            coordinador = hfSel.Value
        End Try
        objInv.AbrirTransaccionCnx()
        objInv.ModificaComite(hfid.Value, txtAComite.Text, ddlACentroCosto.SelectedValue, ddlAInstancia.SelectedValue, chkEstado.Checked)
        objInv.AsignaCoordinadorComite(coordinador, hfid.Value)
        objInv.CerrarTransaccionCnx()
        ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se guardaron las modificaciones.');", True)
        ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmGestionComite.aspx?id=" & Request.QueryString("id") & "';", True)
    End Sub

    Private Sub Limpiar()
        Dim dt As New DataTable
        dt = Session("dtMiembros")
        dt.Rows.Clear()
        Session("dtMiembros") = dt
        gvMiembros.DataSource = dt
        gvMiembros.DataBind()
        ddlACentroCosto.SelectedIndex = 0
        ddlAInstancia.SelectedIndex = 0
        ddlAPersonal.SelectedIndex = 0
        txtAComite.Text = ""
        hfid.Value = ""
    End Sub

    Private Sub CargarComite()
        Dim objInv As New clsInvestigacion
        Dim dt As New DataTable
        dt = objInv.ConsultaComite(0, IIf(ddlListaCentroCosto.SelectedIndex = 0, 0, ddlListaCentroCosto.SelectedValue))
        gvListaComite.DataSource = dt
        gvListaComite.DataBind()
        If dt.Rows.Count > 0 Then
            pnlResultado.CssClass = ""
        Else
            pnlResultado.CssClass = "hidden"
        End If
    End Sub

    Private Sub CargarDatosComite(ByVal id As Integer)
        Try
            Dim dtComite As New DataTable
            Dim objInv As New clsInvestigacion

            dtComite = objInv.ConsultaComite(id, 0)


            ddlACentroCosto.SelectedValue = dtComite.Rows(0).Item("centrocosto_id")
            ddlAInstancia.SelectedValue = dtComite.Rows(0).Item("investigacion_instancia_id")
            txtAComite.Text = dtComite.Rows(0).Item("nombre")
            chkEstado.Checked = dtComite.Rows(0).Item("activo")
            chkEstado.Enabled = True
            dtComite = objInv.ConsultaComiteMiembros(id)


            For Each row As DataRow In dtComite.Rows
                If row("coordinador") = "True" Or row("coordinador") = 1 Then
                    hfSel.Value = row("personal_id")
                End If
            Next
            gvMiembros.DataSource = dtComite
            gvMiembros.DataBind()
        Catch ex As Exception
            'Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    'Protected Sub ibtnBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnBuscar.Click
    '    CargarComite()
    'End Sub



    Protected Sub gvListaComite_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaComite.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvListaComite','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            'Response.Write(ex.Message)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub CargarComiteMiembros()
        Dim comite_id As Integer
        Dim hf As HiddenField
        Dim objInv As New clsInvestigacion
        hf = gvListaComite.SelectedRow.FindControl("hfCodigo")
        comite_id = hf.Value
        gvComiteMiembros.DataSource = objInv.ConsultaComiteMiembros(hf.Value)
        gvComiteMiembros.DataBind()
    End Sub

    Protected Sub gvListaComite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaComite.SelectedIndexChanged
        CargarComiteMiembros()
    End Sub

    'Protected Sub ibtnModificar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    Protected Sub ibtnModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim ibtnModificar As ImageButton
        Dim ibtnModificar As LinkButton
        Dim row As GridViewRow
        Dim hf As HiddenField
        ibtnModificar = sender
        row = ibtnModificar.NamingContainer
        hf = row.FindControl("hfCodigo")
        hfid.Value = hf.Value
        CargarDatosComite(hfid.Value)
        pnlListado.Visible = False
        pnlModificacion.Visible = True
        chkEstado.Enabled = True
        lblTitulo.Text = "Modificar Comité"
    End Sub

    Protected Sub gvListaComite_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaComite.PageIndexChanging
        gvListaComite.PageIndex = e.NewPageIndex
        CargarComite()
    End Sub

    Protected Sub gvComiteMiembros_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvComiteMiembros.PageIndexChanging
        gvComiteMiembros.PageIndex = e.NewPageIndex
        CargarComiteMiembros()
    End Sub

    Protected Sub ibtnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnBuscar.Click
        CargarComite()
    End Sub

    Protected Sub ibtnNuevoComite_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnNuevoComite.Click
        pnlModificacion.Visible = True
        pnlListado.Visible = False
        chkEstado.Enabled = False
        chkEstado.Checked = True
        lblTitulo.Text = "Nuevo Comité"
        Limpiar()
    End Sub

    Protected Sub ibtnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnGrabar.Click
        Try
            If hfid.Value = "" Then
                Guardar()
            Else
                Modificar()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnAgregar.Click
        If hfid.Value = "" Then
            AgregarMiembro(ddlAPersonal.SelectedValue, ddlAPersonal.SelectedItem.ToString, 0)
            ddlAPersonal.SelectedIndex = 0
        Else
            For Each row As GridViewRow In gvMiembros.Rows
                If row.Cells.Item(1).Text = ddlAPersonal.SelectedValue Then
                    lblMensaje.Text = ddlAPersonal.SelectedItem.ToString + " ya fue registrado."
                    Exit Sub
                End If
            Next
            Dim objInv As New clsInvestigacion
            objInv.AbrirTransaccionCnx()
            'Me.Response.Write(" Agregar " & ddlAInstancia.SelectedValue)
            objInv.AgregaMiembroComite(ddlAPersonal.SelectedValue, False, hfid.Value, ddlAInstancia.SelectedValue)
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('1');", True)
            objInv.CerrarTransaccionCnx()
            gvMiembros.DataSource = objInv.ConsultaComiteMiembros(hfid.Value)
            gvMiembros.DataBind()
        End If
    End Sub

    Protected Sub ibtnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRegresar.Click
        pnlListado.Visible = True
        pnlModificacion.Visible = False
    End Sub
End Class
