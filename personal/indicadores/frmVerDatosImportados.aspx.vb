
Partial Class Indicadores_Formularios_frmVerDatosImportados
    Inherits System.Web.UI.Page

    Dim codigo_var As String
    Dim codigo_cac As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            codigo_var = Request.QueryString("codigo_var")
            'Response.Write(codigo_var)

            'Me.Button1.Attributes.Add("onclick", "history.back(); return false;")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulatePeriodicidadVar(e.Node)
                    Case 1
                        PopulatePeriodoVar(e.Node)
                    Case 2
                        PopulateVariablePeriodoVar(e.Node)
                    Case 3
                        PopulateSubVariablePeriodoVar(e.Node)

                    Case 4
                        PopulateDimensionPeriodoVar(e.Node)
                    Case 5
                        PopulateSubDimensionPeriodoVar(e.Node)
                End Select
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    '6 Cargamos las Subdimensiones de la variable
    Sub PopulateSubDimensionPeriodoVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubDimensionPeriodo(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    '5 Cargamos las dimensiones
    Sub PopulateDimensionPeriodoVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaDimensionPeriodo(node.Value)

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



    '[4]. Lista de Subvariables por periodo de la variable dada
    Sub PopulateSubVariablePeriodoVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            codigo_var = Request.QueryString("codigo_var")
            codigo_cac = node.Value
            dts = obj.CargarSubVariableXPeriodo(codigo_var, node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    '[3].Cargamos la Variable de un periodo dado
    Sub PopulateVariablePeriodoVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            codigo_var = Request.QueryString("codigo_var")
            dts = obj.CargarVariableXPeriodo(codigo_var, node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



    '[2].Cargamos la Periodo de la Variable
    Sub PopulatePeriodoVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            'codigo_var = Request.QueryString("codigo_var")
            'dts = obj.CargarPeriodoVariable(codigo_var)

            dts = obj.CargarPeriodoVariable(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    '[1].Cargamos la Periodicaid de la Variable
    Sub PopulatePeriodicidadVar(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            codigo_var = Request.QueryString("codigo_var")
            dts = obj.CargarPeriodicidadVariable(codigo_var)

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


End Class
