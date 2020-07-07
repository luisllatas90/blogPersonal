
Partial Class administrativo_propuestas2_proponente_contenidoAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If (Request.QueryString("id") Is Nothing) Then
            Response.Redirect("../../../ErrorSistema.aspx")
        Else
            Me.HdUsuario.Value = Request.QueryString("id")
        End If

        MostrarInstancias()
        If ddlInstanciaRevision.SelectedValue <> "P" Then
            Me.cmdModificar.Enabled = False
        End If
        If Me.ddlInstanciaRevision.SelectedValue = "P" And Me.ddlInstanciaPropuesta.SelectedValue = "P" Then
            Me.cmdEnviar.Visible = True
        Else
            Me.cmdEnviar.Visible = False
        End If
    End Sub

    Private Sub MostrarInstancias()
        If Me.ddlInstanciaRevision.SelectedValue = "P" Then
            Me.lblEtiquetaInstancia.Visible = True
            Me.ddlInstanciaPropuesta.Visible = True
            Me.lblEtiquetaEstado.Visible = False
            Me.ddlEstadoRevision.Visible = False
        Else
            Me.lblEtiquetaInstancia.Visible = False
            Me.ddlInstanciaPropuesta.Visible = False
            Me.lblEtiquetaEstado.Visible = True
            Me.ddlEstadoRevision.Visible = True
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")

        Response.Redirect("nuevapropuesta.aspx?idUsu=" & idUsu)
    End Sub

    Protected Sub dgvPropuestas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgvPropuestas.PageIndexChanging

    End Sub

    Protected Sub dgvPropuestas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dgvPropuestas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_prp").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Dim idUsu As Integer
        idUsu = Request.QueryString("id")
        'Response.Write("id:" & Me.txtelegido.Value)
        Response.Redirect("nuevapropuesta.aspx?idUsu=" & idUsu & "&codigo_prp=" & Me.txtelegido.Value & "&accion=M&instancia=" & Me.ddlInstanciaPropuesta.SelectedValue)
    End Sub

    Protected Sub ddlInstanciaRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
        MostrarInstancias()
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        If Me.txtelegido.Value = "" Then
            '   Response.Write("<H3>SELECCIONE UNA PROPUESTA PARA ENVIAR</H3>")
        Else
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            ObjCnx.Ejecutar("PRP_CalificarPropuestas", Request.QueryString("id"), Me.txtelegido.Value, "P", "P", "P")
        End If
    End Sub

    Protected Sub ddlInstanciaPropuesta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInstanciaPropuesta.SelectedIndexChanged        
        Me.txtelegido.Value = ""
        'Response.Write(Me.ddlInstanciaPropuesta.SelectedValue())
    End Sub

    Protected Sub ddlEstadoRevision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstadoRevision.SelectedIndexChanged
        Me.txtelegido.Value = ""
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            '           ObjCnx.Ejecutar("PRP_ConsultarPropuestaAdmin", Me.ddlInstanciaRevision.SelectedValue
            ',@instanciaInvolucrado VARCHAR(1)
            ',@instanciaPropuesta VARCHAR(1)
            ',@estadoPrp VARCHAR(10)
            ',@descripcion VARCHAR(200))
            '       Catch ex As Exception
        Catch

        End Try
        'PRP_ConsultarPropuesta
        '<asp:ControlParameter ControlID="ddlInstanciaRevision" DefaultValue="P" 
        '                                    Name="tipo" PropertyName="SelectedValue" Type="String" />
        '                                <asp:QueryStringParameter DefaultValue="" Name="codigo_per" 
        '                                    QueryStringField="id" Type="Int32" />
        '                                <asp:Parameter DefaultValue="P" Name="instanciaInvolucrado" Type="String" />
        '                                <asp:ControlParameter ControlID="ddlInstanciaPropuesta" DefaultValue="" 
        '                                    Name="instanciaPropuesta" PropertyName="SelectedValue" Type="String" />
        '                                <asp:ControlParameter ControlID="ddlEstadoRevision" DefaultValue="P" 
        '                                    Name="estadoPrp" PropertyName="SelectedValue" Type="String" />
    End Sub

    Protected Sub dgvPropuestas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvPropuestas.SelectedIndexChanged

    End Sub
End Class
