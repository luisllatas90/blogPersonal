Imports System.Data

Partial Class DirectorDepartamento_administrarareaslineas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LlenarArbol()
    End Sub

    Protected Sub TreeUnidades_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeUnidades.SelectedNodeChanged
        ' #####################################
        ' Primero los Nodos Raiz o Nodos Padres
        ' #####################################
        For i As Int16 = 0 To Me.TreeUnidades.Nodes.Count - 1
            If Me.TreeUnidades.Nodes(i).Selected = True Then
                Me.PanDepartamento.Enabled = True
                Me.PanDepartamento.Visible = True
                Me.PanLinea.Visible = False
                Me.PanArea.Visible = False
                Me.PanTematica.Visible = False
                Me.CmdAgreArea.Attributes.Add("OnClick", "AbrirPopUp('frmarea.aspx?modo=n&codigo=" & Me.TreeUnidades.Nodes(i).Value & "','320','580'); return false")
                Session("codigo_elim") = Me.TreeUnidades.Nodes(i).Value
                Exit Sub
            Else
                ' #####################################
                ' Los nodos 2° lugar, --> nodos de area
                ' #####################################
                For j As Int16 = 0 To Me.TreeUnidades.Nodes(i).ChildNodes.Count - 1
                    If Me.TreeUnidades.Nodes(i).ChildNodes(j).Selected = True Then
                        Me.PanDepartamento.Visible = False
                        Me.PanArea.Visible = True
                        Me.PanLinea.Visible = False
                        Me.PanTematica.Visible = False
                        Me.CmdAgreLinea.Attributes.Add("OnClick", "AbrirPopUp('frmlinea.aspx?modo=n&codigo=" & Me.TreeUnidades.Nodes(i).ChildNodes(j).Value & "','250','580'); return false")
                        Me.CmdModArea.Attributes.Add("OnClick", "AbrirPopUp('frmarea.aspx?modo=m&codigo1=" & Me.TreeUnidades.Nodes(i).Value & "&codigo=" & Me.TreeUnidades.Nodes(i).ChildNodes(j).Value & "','320','580'); return false")
                        Session("codigo_elim") = Me.TreeUnidades.Nodes(i).ChildNodes(j).Value
                        Exit Sub
                    Else
                        ' #######################################
                        ' Los nodos 3er lugar, --> nodos de linea
                        ' #######################################
                        For k As Int16 = 0 To Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes.Count - 1
                            If Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).Selected = True Then
                                Me.PanDepartamento.Visible = False
                                Me.PanArea.Visible = False
                                Me.PanLinea.Visible = True
                                Me.PanTematica.Visible = False
                                Me.CmdAgreTema.Attributes.Add("OnClick", "AbrirPopUp('frmtematica.aspx?modo=n&codigo=" & Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).Value & "','250','580'); return false")
                                Me.CmdModiLinea.Attributes.Add("OnClick", "AbrirPopUp('frmlinea.aspx?modo=m&codigo=" & Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).Value & "','250','580'); return false")
                                Session("codigo_elim") = Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).Value
                                Exit Sub
                            Else
                                ' #########################################
                                ' Los nodos 4° lugar, --> nodos de tematica
                                ' #########################################
                                For m As Int16 = 0 To Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).ChildNodes.Count - 1
                                    If Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).ChildNodes(m).Selected = True Then
                                        Me.CmdModifTematica.Attributes.Add("OnClick", "AbrirPopUp('frmtematica.aspx?modo=m&codigo=" & Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).ChildNodes(m).Value & "','250','580'); return false")
                                        Me.PanDepartamento.Visible = False
                                        Me.PanArea.Visible = False
                                        Me.PanLinea.Visible = False
                                        Me.PanTematica.Visible = True
                                        Session("codigo_elim") = Me.TreeUnidades.Nodes(i).ChildNodes(j).ChildNodes(k).ChildNodes(m).Value
                                        Exit Sub
                                    Else
                                        Me.PanTematica.Visible = False
                                    End If
                                Next
                                Me.PanLinea.Visible = False
                            End If
                        Next
                        Me.PanArea.Visible = False
                    End If
                Next
                Me.PanDepartamento.Enabled = False
            End If
        Next
    End Sub

    Private Sub AgregaItem(ByVal Nodo As TreeNode, ByVal codigo_are As Integer, ByVal codigo_cco As Integer)
        Dim Tabla As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("INV_ConsultarUnidadesInvestigacion", codigo_are, codigo_cco)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo_X.ExpandAll()
            Nodo_X.Text = Tabla.Rows(i).Item("nombre_are")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_are")
            Nodo_X.ToolTip = Tabla.Rows(i).Item("proposito_are")
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
        Tabla1 = ObjArbol.ConsultarUnidadesInvestigacion("1D", Request.QueryString("id"))

        For i As Int16 = 0 To Tabla1.Rows.Count - 1
            Dim Nodo As New TreeNode
            Nodo.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo.PopulateOnDemand = False
            Nodo.ExpandAll()
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
                Nodo2.ExpandAll()
                Nodo2.Text = datos.Rows(j).Item("nombre_are")
                Nodo2.Value = datos.Rows(j).Item("codigo_are")
                Nodo2.ToolTip = datos.Rows(j).Item("proposito_are")
                Me.TreeUnidades.Nodes(i).ChildNodes.Add(Nodo2)
                AgregaItem(Nodo2, datos.Rows(j).Item("codigo_are"), Tabla1.Rows(i).Item(0))
                Nodo2 = Nothing
            Next
            Nodo = Nothing
        Next
    End Sub

    Protected Sub CmdElimArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdElimArea.Click
        Dim ObjInv As New Investigacion
        Try
            If ObjInv.EliminarArea(Session("codigo_elim")) = -1 Then
                Response.Write("<script>alert('Imposible eliminar un área que contiene lineas de investigacion.')</script>")
            Else
                LlenarArbol()
            End If
        Catch ex As Exception

        End Try
        ObjInv = Nothing
    End Sub

    Protected Sub CmdElimLinea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdElimLinea.Click
        Dim objinv As New Investigacion
        Try
            If objinv.EliminarLinea(Session("codigo_elim")) = -1 Then
                Response.Write("<script>alert('Imposible eliminar una linea de investigación que contenga temáticas o personal asignados a la misma.')</script>")
            Else
                LlenarArbol()
            End If
        Catch ex As Exception

        End Try
        objinv = Nothing
    End Sub

    Protected Sub CmdElimiTematica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdElimiTematica.Click
        Dim objinv As New Investigacion
        Try
            If objinv.EliminarTematica(Session("codigo_elim")) = -1 Then
                Response.Write("<script>alert('Imposible eliminar una temática que contiene investigaciones asignadas.')</script>")
            Else
                LlenarArbol()
            End If
        Catch ex As Exception

        End Try
        objinv = Nothing
    End Sub
End Class
