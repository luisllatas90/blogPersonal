Imports System.Data

Partial Class DirectorDepartamento_administrarareaslineas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LlenarArbol()
    End Sub

    Protected Sub TreeUnidades_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeUnidades.SelectedNodeChanged
        Me.PanArea.Enabled = True
        Me.CmdAgreLinea.Attributes.Add("OnClick", "AbrirPopUp('frmlinea.aspx?modo=n&codigo=" & Me.TreeUnidades.SelectedValue & "','250','580'); return false")
        Me.CmdModArea.Attributes.Add("OnClick", "AbrirPopUp('frmlinea.aspx?modo=m&codigo=" & Me.TreeUnidades.SelectedValue & "','250','580'); return false")
        Dim valores(2) As String
        Me.TreeUnidades.SelectedNode.Expand()
        valores = Split(Me.TreeUnidades.SelectedValue, "|")
        Session("codigo_are") = valores(1)
    End Sub

    Private Sub AgregaItem(ByVal Nodo As TreeNode, ByVal codigo_are As Integer, ByVal codigo_cco As Integer)
        Dim Tabla As Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        Tabla = ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", codigo_are, codigo_cco)
        ObjDatos = Nothing

        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo_X.Expand()
            Nodo_X.Text = Tabla.Rows(i).Item("nombre_are")
            Nodo_X.Value = codigo_cco.ToString & "|" & Tabla.Rows(i).Item("codigo_are").ToString & "|" & Tabla.Rows(i).Item("nivel_are")
            Nodo_X.ToolTip = Tabla.Rows(i).Item("proposito_are").ToString
            Nodo.ChildNodes.Add(Nodo_X)
            AgregaItem(Nodo_X, Tabla.Rows(i).Item("codigo_are"), codigo_cco)
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing

    End Sub

    Private Sub LlenarArbol()
        Dim Tabla1 As DataTable
        Me.TreeUnidades.Nodes.Clear()
        Dim ObjArbol As New Investigacion

        Tabla1 = ObjArbol.ConsultarUnidadesInvestigacion("1D", Request.QueryString("id"))

        ObjArbol = Nothing
        For i As Int16 = 0 To Tabla1.Rows.Count - 1
            Dim Nodo As New TreeNode
            Nodo.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo.PopulateOnDemand = False
            Nodo.ExpandAll()
            Nodo.Text = Tabla1.Rows(i).Item(1)
            Nodo.Value = Tabla1.Rows(i).Item(0).ToString & "|0|0"

            Me.TreeUnidades.Nodes.Add(Nodo)

            Dim datos As New Data.DataTable
            Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            datos = ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", 0, Tabla1.Rows(i).Item(0))
            ObjDatos = Nothing
            datos.Select("where codigo_are" = "2")

            For j As Int32 = 0 To datos.Rows.Count - 1
                Dim Nodo2 As New TreeNode
                Nodo2.SelectAction = TreeNodeSelectAction.SelectExpand
                Nodo2.PopulateOnDemand = False
                Nodo2.ExpandAll()
                Nodo2.Text = datos.Rows(j).Item("nombre_are")
                Nodo2.Value = Tabla1.Rows(i).Item(0) & "|" & datos.Rows(j).Item("codigo_are").ToString & "|" & datos.Rows(j).Item("nivel_are")
                Nodo2.ToolTip = datos.Rows(j).Item("proposito_are").ToString
                Me.TreeUnidades.Nodes(i).ChildNodes.Add(Nodo2)
                AgregaItem(Nodo2, datos.Rows(j).Item("codigo_are"), Tabla1.Rows(i).Item(0))
                Nodo2 = Nothing
            Next
            Nodo = Nothing
        Next
    End Sub

    Protected Sub CmdElimArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdElimArea.Click
        Dim ObjInv As New Investigacion
        Dim valordevuelto As Integer
        Try
            valordevuelto = ObjInv.EliminarArea(CInt(Session("codigo_are")))
            If valordevuelto = -3 Then
                Response.Write("<script>alert('Imposible Eliminar El Item, existe dependencia con algun otro.')</script>")
            ElseIf valordevuelto = -2 Then
                Response.Write("<script>alert('Existe personal relacionado al Item seleccionado.')</script>")
            ElseIf valordevuelto = -1 Then
                Response.Write("<script>alert('Ocurrió un error al tratar de eliminar el Item, inténtelo nuevamente.')</script>")
            ElseIf valordevuelto = 1 Then
                LlenarArbol()
            End If
        Catch ex As Exception
        End Try
        ObjInv = Nothing
    End Sub
End Class
