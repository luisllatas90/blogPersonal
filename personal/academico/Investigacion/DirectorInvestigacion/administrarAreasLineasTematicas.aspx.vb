Imports System.Data
Partial Class DirectorInvestigacion_administrarAreasLineasTematicas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LlenarArbol()
    End Sub

    Private Sub AgregaItem(ByVal Nodo As TreeNode, ByVal codigo_are As Integer, ByVal codigo_cco As Integer)
        Dim Tabla As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", codigo_are, codigo_cco)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
            'Nodo_X.ExpandAll()
            Nodo_X.Text = Tabla.Rows(i).Item("nombre_are")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_are")
            Nodo.ChildNodes.Add(Nodo_X)
            AgregaItem(Nodo_X, Tabla.Rows(i).Item("codigo_are"), codigo_cco)
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
        ObjDatos = Nothing
    End Sub

    Private Sub LlenarArbol()
        Dim Tabla1 As DataTable
        Me.TreeUnidades.Nodes.Clear()

        Dim ObjArbol As New Investigacion
        Tabla1 = ObjArbol.ConsultarUnidadesInvestigacion("1", "")

        For i As Int16 = 0 To Tabla1.Rows.Count - 1
            Dim Nodo As New TreeNode
            Nodo.SelectAction = TreeNodeSelectAction.Expand
            Nodo.Text = Tabla1.Rows(i).Item(1)
            Nodo.Value = Tabla1.Rows(i).Item(0)
            Me.TreeUnidades.Nodes.Add(Nodo)

            Dim datos As New Data.DataTable
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            datos = ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", 0, Tabla1.Rows(i).Item(0))

            For j As Int32 = 0 To datos.Rows.Count - 1
                Dim Nodo2 As New TreeNode
                Nodo2.SelectAction = TreeNodeSelectAction.SelectExpand
                Nodo2.PopulateOnDemand = False
                Nodo2.NavigateUrl = "pagina.aspx?" & j.ToString
                'Nodo2.ExpandAll()
                Nodo2.Text = datos.Rows(j).Item("nombre_are")
                Nodo2.Value = datos.Rows(j).Item("codigo_are")
                Me.TreeUnidades.Nodes(i).ChildNodes.Add(Nodo2)
                AgregaItem(Nodo2, datos.Rows(j).Item("codigo_are"), Tabla1.Rows(i).Item(0))
                Nodo2 = Nothing
            Next
        Next
    End Sub
End Class
