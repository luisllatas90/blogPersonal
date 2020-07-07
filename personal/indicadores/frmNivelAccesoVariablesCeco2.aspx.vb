
Partial Class indicadores_frmNivelAccesoVariablesCeco2
    Inherits System.Web.UI.Page

    '===================================================================
    'Fuente:
    '    http://forums.asp.net/t/1141731.aspx/1
    'Desarrollado:
    'xDguevaara 07.12.12
    '===================================================================

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Me.pnlConsulta.Visible = False
                pnlListaPersonal.Visible = False

                Me.pnlConf.Visible = True
                CargarListaUnidadNegocio()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaUnidadNegocio()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListaUnidadNegocio_cnf
            If dts.Rows.Count > 0 Then
                ddlUnidadNegocio.DataSource = dts
                ddlUnidadNegocio.DataTextField = "Descripcion"
                ddlUnidadNegocio.DataValueField = "Codigo"
                ddlUnidadNegocio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlUnidadNegocio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidadNegocio.SelectedIndexChanged
        Try
            'Response.Write("valor: " & ddlUnidadNegocio.SelectedValue)
            If ddlUnidadNegocio.SelectedValue <> 0 Then
                CargarListaSubUnidadNegocio(ddlUnidadNegocio.SelectedValue)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarListaSubUnidadNegocio(ByVal codigo_uneg As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaSubUnidadNegocio_cnf(codigo_uneg)
            If dts.Rows.Count > 0 Then
                ddlSubunidadNegocio.DataSource = dts
                ddlSubunidadNegocio.DataTextField = "Descripcion"
                ddlSubunidadNegocio.DataValueField = "Codigo"
                ddlSubunidadNegocio.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaCentroCostoParametros()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If txtBusceco.Text.Trim <> "" Then
                Busqueda()
            Else
                If ddlUnidadNegocio.SelectedValue <> 0 And ddlSubunidadNegocio.SelectedValue <> 0 Then
                    dts = obj.ListaCecos_cnf(ddlUnidadNegocio.SelectedValue, ddlSubunidadNegocio.SelectedValue)
                    If dts.Rows.Count > 0 Then
                        gvListaCecos.DataSource = dts
                        gvListaCecos.DataBind()
                    Else
                        gvListaCecos.DataSource = Nothing
                        gvListaCecos.DataBind()
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Busqueda()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Response.Write(txtBusceco.Text.Trim)

            If ddlUnidadNegocio.SelectedValue <> 0 Then
                dts = obj.BurcarCeco(txtBusceco.Text.Trim, CType(ddlSubunidadNegocio.SelectedValue, Integer), CType(ddlUnidadNegocio.SelectedValue, Integer))
            Else
                dts = obj.BurcarCeco(txtBusceco.Text.Trim, 0, 0)
            End If

            If dts.Rows.Count > 0 Then
                gvListaCecos.DataSource = dts
                gvListaCecos.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    
    Protected Sub ddlSubunidadNegocio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSubunidadNegocio.SelectedIndexChanged
        Try
            'Response.Write("valor: " & ddlSubunidadNegocio.SelectedValue)
            ListaCentroCostoParametros()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvListaCecos.PageIndexChanging
        Try
            'Response.Write("pageIndexChanging: " & ddlSubunidadNegocio.SelectedValue)
            gvListaCecos.PageIndex = e.NewPageIndex
            Busqueda()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaCecos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                'e.Row.Cells(0).Text = e.Row.RowIndex + 1

                '================================================================================================================
                'Marcar toda la fila del gridview
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvListaCecos','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
                '================================================================================================================
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


    Protected Sub btnBuscarCecos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCecos.Click
        Try
            Busqueda()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulateCategories(e.Node)
                    Case 1
                        PopulateVariables(e.Node)
                    Case 2
                        PopulateSubVariables(e.Node)
                    Case 3
                        PopulateDimension(e.Node)
                    Case 4
                        PopulateSubDimension(e.Node)

                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateCategories(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.CargarListaCategorias()
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub


    Sub PopulateCategories2(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            'Ok
            dts = obj.CargarListaCategorias2(Me.hdfCodigo_Ceco.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub PopulateVariables(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaVariable(node.Value, Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub PopulateVariables2(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Ok
            dts = obj.CargarListaVariable2(node.Value, Me.hdfCodigo_Ceco.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub PopulateSubVariables(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubVariables(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateSubVariables2(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'OK
            dts = obj.CargarListaSubVariables2(node.Value, Me.hdfCodigo_Ceco.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateDimension(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaDimension(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateDimension2(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'ok
            dts = obj.CargarListaDimension2(node.Value, Me.hdfCodigo_Ceco.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateSubDimension(ByVal Node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubDimension(Node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = False
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    Node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Sub PopulateSubDimension2(ByVal Node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'ok
            dts = obj.CargarListaSubDimension2(Node.Value, hdfCodigo_Ceco.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = False
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    Node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treePrueba.SelectedNodeChanged
        Try
            'Response.Write(treePrueba.SelectedNode.Value)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Response.Write("cosigo_cco= " & hdfCodigo_Ceco.Value)
            'Response.Write("<br />")

            If hdfCodigo_Ceco.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo al que se asignaran las variables.');", True)
                Exit Sub
            End If

            For Each tn As TreeNode In Me.treePrueba.CheckedNodes
                If tn.Checked Then
                    'Response.Write(tn.Value)
                    'Response.Write("<br />")
                    obj.GuardarConfiguracionVariables(Me.hdfCodigo_Ceco.Value, tn.Value, Me.Request.QueryString("id"))
                End If
            Next

            treePrueba2.CollapseAll()
            treePrueba2.ExpandAll()

            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuracion ha sido guardada correctamente.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvListaCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvListaCecos.SelectedIndexChanged
        Try
            'hfCodInvestigacion.Value = gvInvestigacion.SelectedRow.Cells(0).Text
            'Dim row As GridViewRow = CustomersGridView.SelectedRow

            Me.hdfCodigo_Ceco.Value = gvListaCecos.SelectedRow.Cells(1).Text
            'Response.Write(gvListaCecos.SelectedRow.Cells(1).Text)
            'Response.Write("<br />")
            'Response.Write(Me.hdfCodigo_Ceco.Value)

            pnlConf.Visible = True
            pnlConsulta.Visible = False
            pnlListaPersonal.Visible = False





        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub lnkConfiguracion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfiguracion.Click
        Try
            Me.pnlConf.Visible = True
            Me.pnlConsulta.Visible = False
            pnlListaPersonal.Visible = False

            treePrueba2.CollapseAll()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub lnkConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsulta.Click
        Try
            If Me.hdfCodigo_Ceco.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo a Consultar.');", True)
                Exit Sub
            End If

            treePrueba2.CollapseAll()
            treePrueba2.ExpandAll()

            Me.pnlConf.Visible = False
            Me.pnlConsulta.Visible = True
            Me.pnlListaPersonal.Visible = False
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub treePrueba2_TreeNodeCollapsed(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba2.TreeNodeCollapsed
        Try
            '========================================================================
            'Es metodo permite volver a cargar el treeview al contraer y expandir
            '========================================================================

            If e.Node.ChildNodes.Count > 0 Then
                e.Node.ChildNodes.Clear()
            End If

            Select Case e.Node.Depth
                Case 0
                    PopulateCategories2(e.Node)
                Case 1
                    PopulateVariables2(e.Node)
                Case 2
                    PopulateSubVariables2(e.Node)
                Case 3
                    PopulateDimension2(e.Node)
                Case 4
                    PopulateSubDimension2(e.Node)
            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub treePrueba2_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba2.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulateCategories2(e.Node)
                    Case 1
                        PopulateVariables2(e.Node)
                    Case 2
                        PopulateSubVariables2(e.Node)
                    Case 3
                        PopulateDimension2(e.Node)
                    Case 4
                        PopulateSubDimension2(e.Node)

                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Response.Write("cosigo_cco= " & hdfCodigo_Ceco.Value)
            'Response.Write("<br />")

            If hdfCodigo_Ceco.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo al que se asignaran las variables.');", True)
                Exit Sub
            End If

            For Each tn As TreeNode In Me.treePrueba2.CheckedNodes
                If tn.Checked Then
                    'Response.Write(tn.Value)
                    'Response.Write("<br />")
                    obj.EliminarConfiguracionVariables(Me.hdfCodigo_Ceco.Value, tn.Value, Me.Request.QueryString("id"))
                End If
            Next

            treePrueba2.CollapseAll()
            treePrueba2.ExpandAll()

            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Se eliminaron Correctamente.');", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPersnalCeco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPersnalCeco.Click
        Try
            If Me.hdfCodigo_Ceco.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo a Consultar.');", True)
                Exit Sub
            End If

            Me.pnlConf.Visible = False
            Me.pnlConsulta.Visible = False
            Me.pnlListaPersonal.Visible = True

            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            'Limpiamos la data antes de mostrarla --------------------
            gvPersonal.DataSource = Nothing
            gvPersonal.DataBind()
            '---------------------------------------------------------

            dts = obj.ListaUsuariosConfigurados(Me.hdfCodigo_Ceco.Value)
            If dts.Rows.Count > 0 Then
                gvPersonal.DataSource = dts
                gvPersonal.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvPersonal_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPersonal.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem
                e.Row.Cells(1).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
