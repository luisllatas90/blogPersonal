
Partial Class Investigador_unidadesdeinvestigacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim Tabla1 As Data.DataTable
            Dim tabla2 As Data.DataTable
            Dim TABLA3 As Data.DataTable
            Dim tabla4 As Data.DataTable

            Dim ObjArbol As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Tabla1 = ObjArbol.TraerDataTable("ConsultarUnidadesInvestigacion", "1D", Request.QueryString("id"))

            Me.TreeUnidades.Nodes.Clear()

            For i As Int16 = 0 To Tabla1.Rows.Count - 1
                Dim Nodo As New TreeNode
                Nodo.SelectAction = TreeNodeSelectAction.None
                Nodo.Text = Tabla1.Rows(i).Item(1)
                Nodo.Value = Tabla1.Rows(i).Item(0)
                Me.TreeUnidades.Nodes.Add(Nodo)

                tabla2 = ObjArbol.TraerDataTable("ConsultarUnidadesInvestigacion", "2", Tabla1.Rows(i).Item(0).ToString)
                For j As Int16 = 0 To tabla2.Rows.Count - 1
                    Dim Nodo2 As New TreeNode
                    Nodo2.SelectAction = TreeNodeSelectAction.SelectExpand
                    Nodo2.Collapse()
                    Nodo2.Text = tabla2.Rows(j).Item(1)
                    Nodo2.Value = tabla2.Rows(j).Item(0)
                    Nodo2.ToolTip = tabla2.Rows(j).Item(2)

                    Nodo.ChildNodes.Add(Nodo2)

                    TABLA3 = ObjArbol.TraerDataTable("ConsultarUnidadesInvestigacion", "3", tabla2.Rows(j).Item(0).ToString)
                    For k As Int16 = 0 To TABLA3.Rows.Count - 1
                        Dim Nodo3 As New TreeNode
                        Nodo3.SelectAction = TreeNodeSelectAction.SelectExpand
                        Nodo2.Collapse()
                        Nodo3.Text = TABLA3.Rows(k).Item(1)
                        Nodo3.Value = TABLA3.Rows(k).Item(0)
                        Nodo3.ToolTip = TABLA3.Rows(k).Item(2)
                        Nodo2.ChildNodes.Add(Nodo3)

                        tabla4 = ObjArbol.TraerDataTable("ConsultarUnidadesInvestigacion", "4", TABLA3.Rows(k).Item(0).ToString)
                        For m As Int16 = 0 To tabla4.Rows.Count - 1
                            Dim Nodo4 As New TreeNode
                            Nodo4.Text = tabla4.Rows(m).Item(1)
                            Nodo4.Collapse()
                            Nodo4.Value = tabla4.Rows(m).Item(0)
                            Nodo4.ToolTip = tabla4.Rows(m).Item(2)
                            Nodo3.ChildNodes.Add(Nodo4)
                            Nodo4 = Nothing
                        Next
                        Nodo3 = Nothing
                    Next
                    Nodo2 = Nothing
                Next
                Nodo = Nothing
            Next
        End If
    End Sub

    Protected Sub TreeUnidades_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeUnidades.SelectedNodeChanged
        Me.LblTitulo.Text = Me.TreeUnidades.SelectedNode.Text
        Me.LblProposito.Text = Me.TreeUnidades.SelectedNode.ToolTip
    End Sub
End Class
