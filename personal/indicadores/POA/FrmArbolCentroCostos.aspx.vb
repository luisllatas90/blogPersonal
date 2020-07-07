
Partial Class indicadores_POA_FrmArbolCentroCostos
    Inherits System.Web.UI.Page

    Protected Sub treePrueba_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        CentroCostosPrimerNivel(e.Node)
                    Case 1
                        CentroCostosPrimerNivel(e.Node)
                    Case 2
                        CentroCostosPrimerNivel(e.Node)
                End Select

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub CentroCostosPrimerNivel(ByVal node As TreeNode)
        Try
            Dim obj As New ClsPlanOperativoAnual
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.ArbolCentroCostos(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion_cco").ToString, row("Codigo_cco").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            Dim newNode As TreeNode = New TreeNode(Request.QueryString("descripcion"), Request.QueryString("codigo"))
            newNode.PopulateOnDemand = True
            newNode.SelectAction = TreeNodeSelectAction.Expand
            treePrueba.Nodes.Add(newNode)
        End If
    End Sub

    Protected Sub treePrueba_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treePrueba.SelectedNodeChanged
        Try
            'Dim Text As String = treePrueba.SelectedNode.Text

            Response.Write(treePrueba.SelectedNode.Value.ToString.Trim)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
