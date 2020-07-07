
Partial Class indicadores_frmNivelAccesoVariablesPlanCeco
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarListaPlanes()
            CargarAniosPlan()   'Estos se cargan de acuerdo a los obj del plan.
            CargarCentroCostos()


            Me.pnlConf.Visible = True
            pnlConsulta.Visible = False
            pnlListaPersonal.Visible = False

            If rbtVistaGeneral.Checked = True Then
                vEstadoControlTreeview(False)
                vEstadoControlGridview(True)
            Else
                vEstadoControlTreeview(True)
                vEstadoControlGridview(False)
            End If

            'Al iniciar solo mostramos la grilla con datos.
            'pnlListaVariablesGridview.Visible = True
            'LLamamos al procedure que carga la grilla
            pnlOpcionesGirdview.Visible = False
            ListaVariablesGrid()
        End If
    End Sub

    Private Sub vEstadoControlGridview(ByVal vEstado As Boolean)
        Try
            pnlOpcionesGirdview.Visible = vEstado
            pnlListaVariablesGridview.Visible = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub vEstadoControlTreeview(ByVal vEstado As Boolean)
        Try
            pnlListaVariablesTreeview.Visible = vEstado
            pnlOpcionesTreeview.Visible = vEstado
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    Private Sub ListaVariablesGrid()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim estado As String = ""

            'Response.Write("plan: " & ddlPlan.SelectedValue)
            'Response.Write("<br/>")
            'Response.Write("ceco: " & Me.HiddenField1.Value)
            'Response.Write("<br/>")
            'Response.Write("anio: " & Me.ddlAnio.SelectedValue)



            'Definicion del estado.---------------------------------
            If rbtAsignados.Checked = True Then
                estado = "A"
            End If
            If rbtNoAsignados.Checked = True Then
                estado = "N"
            End If
            If rbtTodos.Checked = True Then
                estado = "%"
            End If
            '------------------------------------------------------


            dts = obj.ListaGeneralVariablesSegunPlan(Me.ddlPlan.SelectedValue, _
                                                     Me.HiddenField1.Value, _
                                                     Me.ddlAnio.SelectedValue, _
                                                     estado, _
                                                     Me.txtBusqueda.Text.Trim)

            'Response.Write("<br/>")
            'Response.Write("cantdad: " & dts.Rows.Count)

            If dts.Rows.Count > 0 Then
                gvListaVar.DataSource = dts
                gvListaVar.DataBind()
            Else
                gvListaVar.DataSource = Nothing
                gvListaVar.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCentroCostos()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ListarCentroCostosVariablesPlan(Me.txtBusceco.Text.Trim)
            gvCecos.DataSource = dts
            gvCecos.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarAniosPlan()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If Me.ddlPlan.SelectedValue <> -1 Then
                dts = obj.ListaAniosSegunPlan(Me.ddlPlan.SelectedValue)
                If dts.Rows.Count > 0 Then
                    Me.ddlAnio.DataSource = dts
                    Me.ddlAnio.DataTextField = "anioplan"
                    Me.ddlAnio.DataValueField = "anioplan"
                    Me.ddlAnio.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarListaPlanes()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New data.datatable

            dts = obj.ListaPlanUsuario(Me.Request.QueryString("id"))
            If dts.Rows.Count > 0 Then
                Me.ddlPlan.DataSource = dts
                Me.ddlPlan.DataTextField = "Periodo_pla"
                Me.ddlPlan.DataValueField = "codigo_pla"
                Me.ddlPlan.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        Try
            'Response.Write("Codigo_pla: " & ddlPlan.SelectedValue)
            CargarAniosPlan()


            If rbtVistaGeneral.Checked = True Then
                ListaVariablesGrid()
            Else
                treePrueba.CollapseAll()
                treePrueba2.CollapseAll()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscarCecos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarCecos.Click
        Try
            CargarCentroCostos()
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

    Sub PopulateDimension(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaDimensionTreeViewPorPlan(node.Value, Me.HiddenField1.Value, Me.ddlPlan.SelectedValue)

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

            dts = obj.CargarListaSubVariablesTreeViewPorPlan(node.Value, Me.HiddenField1.Value, Me.ddlPlan.SelectedValue)

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



    Sub PopulateVariables(ByVal node As TreeNode)

        'Response.Write("Entra a cargar Variables")
        'Response.Write("<br/>")

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaVariableTreeViewPorPlan(node.Value, Me.ddlPlan.SelectedValue, Me.HiddenField1.Value, Me.ddlAnio.SelectedValue)

            'Response.Write("Cantidad: " & dts.Rows.Count)

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

    Sub PopulateCategories(ByVal node As TreeNode)

        'Response.Write("Entra a cargar categorias")
        'Response.Write("<br/>")

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

    Protected Sub gvCecos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCecos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCecos','Select$" & e.Row.RowIndex & "');")
                e.Row.Style.Add("cursor", "hand")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        Try
            Me.HiddenField1.Value = gvCecos.SelectedRow.Cells(0).Text

            If rbtVistaGeneral.Checked = True Then
                vEstadoControlGridview(True)
                vEstadoControlTreeview(False)
                ListaVariablesGrid()
            Else
                vEstadoControlGridview(False)
                vEstadoControlTreeview(True)

                Me.pnlConf.Visible = True
                pnlConsulta.Visible = False
                pnlListaPersonal.Visible = False
                lnkConfiguracion.ForeColor = Drawing.Color.Red
                lnkConsulta.ForeColor = Drawing.Color.Blue
                lnkPersnalCeco.ForeColor = Drawing.Color.Blue

                'Para poder actualizar el treeviwe.
                treePrueba.CollapseAll()
                treePrueba2.CollapseAll()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_TreeNodeCollapsed(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodeCollapsed
        Try
            '========================================================================
            'Es metodo permite volver a cargar el treeview al contraer y expandir
            '========================================================================

            If e.Node.ChildNodes.Count > 0 Then
                e.Node.ChildNodes.Clear()
            End If

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
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateSubDimension(ByVal Node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubDimensionTreeViewPorPlan(Node.Value, Me.HiddenField1.Value, Me.ddlPlan.SelectedValue)
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


    Protected Sub ddlAnio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
        Try
            'Response.Write("Anio: " & Me.ddlPlan.SelectedValue)
            Me.HiddenField1.Value = gvCecos.SelectedRow.Cells(0).Text

            If rbtVistaGeneral.Checked = True Then
                ListaVariablesGrid()
            Else
                treePrueba.CollapseAll()
                treePrueba2.CollapseAll()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim contador As Integer = 0
            Dim contador2 As Integer = 0
            Dim contador3 As Integer = 0

            Dim ID_var As String

            Dim Fila As GridViewRow
            Dim sw As Byte = 0

            If HiddenField1.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo al que se asignaran las variables.');", True)
                Exit Sub
            End If



            If rbtVistaGeneral.Checked = True Then

                For i As Integer = 0 To Me.gvListaVar.Rows.Count - 1
                    'Capturamos las filas que estan activas
                    Fila = gvListaVar.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
                    If (valor = True) Then
                        contador3 = contador3 + 1
                    End If
                Next

                If contador3 = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Ud. debe seleccionar por lo menos una variable para poder asignar al Centro Costo.');", True)
                    Exit Sub
                End If

                For i As Integer = 0 To Me.gvListaVar.Rows.Count - 1
                    Fila = gvListaVar.Rows(i)
                    Dim valor As Boolean = CType(Fila.FindControl("chkSel"), CheckBox).Checked
                    If (valor = True) Then
                        sw = 1

                        ID_var = Convert.ToString(gvListaVar.DataKeys(Fila.RowIndex).Value)
                        obj.GuardarConfiguracionVariables(Me.HiddenField1.Value, ID_var, Me.Request.QueryString("id"))

                    End If
                Next
                ListaVariablesGrid()
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuracion ha sido guardada correctamente.');", True)
            End If


            '===============================================================================================
            'Proceso con el arbol:
            '===============================================================================================
            If rbtVistaArbol.Checked = True Then
                For Each tn As TreeNode In Me.treePrueba.CheckedNodes
                    If tn.Checked Then
                        contador = contador + 1
                    End If
                Next

                If contador = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Ud. debe seleccionar por lo menos una variable para poder asignar al Centro Costo.');", True)
                    Exit Sub
                End If

                For Each tn As TreeNode In Me.treePrueba.CheckedNodes
                    If tn.Checked Then
                        'Response.Write(tn.Text)
                        If Left(tn.Text, 1).Trim.ToString = "A" Then
                            contador2 = contador2 + 1
                        End If
                    End If
                Next

                If contador2 > 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Algunas Variables ya estan Asignadas, Verificar (A).');", True)
                    Exit Sub
                End If

                For Each tn As TreeNode In Me.treePrueba.CheckedNodes
                    If tn.Checked Then
                        'Response.Write(tn.Value)
                        'Response.Write(tn.Text)
                        'Response.Write("<br />")
                        obj.GuardarConfiguracionVariables(Me.HiddenField1.Value, tn.Value, Me.Request.QueryString("id"))
                    End If
                Next

                treePrueba.CollapseAll()
                treePrueba2.CollapseAll()

                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La configuracion ha sido guardada correctamente.');", True)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkConfiguracion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfiguracion.Click
        Try
            Me.pnlConf.Visible = True
            pnlConsulta.Visible = False
            pnlListaPersonal.Visible = False
            lnkConfiguracion.ForeColor = Drawing.Color.Red
            lnkConsulta.ForeColor = Drawing.Color.Blue
            lnkPersnalCeco.ForeColor = Drawing.Color.Blue

            Me.treePrueba.CollapseAll()
            Me.treePrueba2.CollapseAll()
            Me.chkTreeview.Checked = False
            Me.chkTreeview.Text = "Expandir"

            If rbtVistaGeneral.Checked = True Then
                ListaVariablesGrid()
                vEstadoControlGridview(True)
                vEstadoControlTreeview(False)
            End If

            If rbtVistaArbol.Checked = True Then
                treePrueba.CollapseAll()
                vEstadoControlGridview(False)
                vEstadoControlTreeview(True)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkConsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsulta.Click
        Try
            Me.pnlConf.Visible = False
            pnlConsulta.Visible = True
            pnlListaPersonal.Visible = False

            lnkConfiguracion.ForeColor = Drawing.Color.Blue
            lnkConsulta.ForeColor = Drawing.Color.Red
            lnkPersnalCeco.ForeColor = Drawing.Color.Blue

            Me.treePrueba.CollapseAll()
            Me.treePrueba2.CollapseAll()
            Me.chkTreeview2.Checked = False
            Me.chkTreeview2.Text = "Expandir"

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub lnkPersnalCeco_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPersnalCeco.Click
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            If HiddenField1.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo a Consultar.');", True)
                Exit Sub
            End If

            Me.pnlConf.Visible = False
            pnlConsulta.Visible = False
            pnlListaPersonal.Visible = True

            lnkConfiguracion.ForeColor = Drawing.Color.Blue
            lnkConsulta.ForeColor = Drawing.Color.Blue
            lnkPersnalCeco.ForeColor = Drawing.Color.Red

            'Limpiamos la data antes de mostrarla --------------------
            gvPersonal.DataSource = Nothing
            gvPersonal.DataBind()
            '---------------------------------------------------------

            dts = obj.ListaUsuariosConfigurados(HiddenField1.Value)

            If dts.Rows.Count > 0 Then
                gvPersonal.DataSource = dts
                gvPersonal.DataBind()
            End If

            Me.treePrueba.CollapseAll()
            Me.treePrueba2.CollapseAll()



            Me.chkTreeview.Checked = False
            Me.chkTreeview.Text = "Expandir"
            Me.chkTreeview2.Checked = False
            Me.chkTreeview2.Text = "Expandir"


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub

    '=========================================================================
    '  Procedimientos para el Treeview2: Muestra las variables configuradas  '
    '=========================================================================

    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Response.Write("cosigo_cco= " & hdfCodigo_Ceco.Value)
            'Response.Write("<br />")

            If HiddenField1.Value = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el Centro de Costo al que se asignaran las variables.');", True)
                Exit Sub
            End If

            For Each tn As TreeNode In Me.treePrueba2.CheckedNodes
                If tn.Checked Then
                    'Response.Write(tn.Value)
                    'Response.Write("<br />")
                    obj.EliminarConfiguracionVariables(HiddenField1.Value, tn.Value, Me.Request.QueryString("id"))
                End If
            Next

            treePrueba2.CollapseAll()


            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Se eliminaron Correctamente.');", True)
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


    Sub PopulateSubDimension2(ByVal Node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubDimension2(Node.Value, Me.HiddenField1.Value)

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


    Sub PopulateDimension2(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaDimension2(node.Value, Me.HiddenField1.Value)

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
            dts = obj.CargarListaSubVariables2(node.Value, Me.HiddenField1.Value)
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
            dts = obj.CargarListaVariable2(node.Value, Me.HiddenField1.Value)
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


    Sub PopulateCategories2(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            'Ok
            dts = obj.CargarListaCategorias2(HiddenField1.Value)
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


    Protected Sub chkTreeview2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTreeview2.CheckedChanged
        Try
            If chkTreeview2.Checked = True Then
                chkTreeview2.Text = "Contraer"
                treePrueba2.ExpandAll()
            Else
                chkTreeview2.Text = "Expandir"
                treePrueba2.CollapseAll()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub chkTreeview_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTreeview.CheckedChanged
        If chkTreeview.Checked = True Then
            chkTreeview.Text = "Contraer"
            treePrueba.ExpandAll()
        Else
            chkTreeview.Text = "Expandir"
            treePrueba.CollapseAll()
        End If
    End Sub

    Protected Sub gvListaVar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaVar.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")


                'Si la variable ya esta asignada, no se le muestra el check xdguevara
                If e.Row.Cells(3).Text = "A" Then
                    Dim Cb As CheckBox
                    Cb = e.Row.FindControl("chkSel")
                    Cb.Visible = False
                End If

                'Asignamos los iconos xdguevara
                Select Case e.Row.Cells(3).Text
                    Case "A"    'Asignado
                        e.Row.Cells(3).Text = "<center><img src='../images/checkCeco.png' alt='" & "Asignado" & "'  style='border: 0px'/></center>"
                    Case "N"  ' No asignado
                        e.Row.Cells(3).Text = "<center><img src='../images/exclamation.png' alt='" & "No Asignado" & "' style='border: 0px'/></center>"
                    Case "V"    'Por asignar.
                        e.Row.Cells(3).Text = "<center><img src='../images/help.gif'  alt='" & "Por definir." & "' style='border: 0px'/></center>"
                End Select

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    
    Protected Sub rbtVistaArbol_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVistaArbol.CheckedChanged
        Try
            If rbtVistaArbol.Checked = True Then
                vEstadoControlTreeview(True)
                treePrueba.CollapseAll()
                vEstadoControlGridview(False)
            Else
                vEstadoControlGridview(True)
                vEstadoControlTreeview(False)
                ListaVariablesGrid()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtTodos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtTodos.CheckedChanged
        Try
            ListaVariablesGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtAsignados_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtAsignados.CheckedChanged
        Try
            ListaVariablesGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtNoAsignados_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtNoAsignados.CheckedChanged
        Try
            ListaVariablesGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBuscarVariable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarVariable.Click
        Try
            ListaVariablesGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbtVistaGeneral_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVistaGeneral.CheckedChanged
        Try
            If rbtVistaGeneral.Checked = True Then
                vEstadoControlTreeview(False)
                vEstadoControlGridview(True)
                ListaVariablesGrid()

                If HiddenField1.Value = 0 Then
                    pnlOpcionesGirdview.Visible = False
                Else
                    pnlOpcionesGirdview.Visible = True
                End If
            Else
                vEstadoControlGridview(False)
                vEstadoControlTreeview(True)
                treePrueba.CollapseAll()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
