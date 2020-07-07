
Partial Class aulavirtual_exportarcurso
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim idcursovirtual As Integer = Request.QueryString("idcursovirtual")
        Dim Tabla1 As Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        'Aplicar configuración Querystring
        If Request.QueryString("titulo") = "" Then Me.lbltitulo.Visible = False
        If Request.QueryString("color") <> "" Then Me.form1.Style.Item("background-color") = Request.QueryString("color")

        Tabla1 = ObjDatos.TraerDataTable("ConsultarCursoVirtual", 3, idcursovirtual, 0, 0)
        Me.lbltitulo.Text = Tabla1.Rows(0).Item("titulocursovirtual")
        Me.Title = Tabla1.Rows(0).Item("titulocursovirtual")
        Tabla1 = Nothing
        Me.trw.Nodes.Clear()

        Tabla1 = ObjDatos.TraerDataTable("CMI_ExportarCursoVirtual", idcursovirtual, 0)

        'Llenar arbol con los items PADRE
        For i As Int16 = 0 To Tabla1.Rows.Count - 1
            Dim Nodo As New TreeNode
            Nodo.SelectAction = TreeNodeSelectAction.Expand
            Nodo.ImageUrl = Tabla1.Rows(i).Item("icono_tre")
            Nodo.Text = "&nbsp;" & Tabla1.Rows(i).Item("titulo_ccv")
            Nodo.Value = Tabla1.Rows(i).Item("codigo_ccv")
            Me.trw.Nodes.Add(Nodo)

            'If Tabla1.Rows(i).Item("total_ccv") > 0 Then
            AgregaItem(Nodo, idcursovirtual, Tabla1.Rows(i).Item("codigo_ccv"))
            'End If
        Next
    End Sub

    Private Sub AgregaItem(ByVal Nodo As TreeNode, ByVal idcursovirtual As Integer, ByVal refcodigo_ccv As Integer)
        Dim Tabla As Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)

        Tabla = ObjDatos.TraerDataTable("CMI_ExportarCursoVirtual", idcursovirtual, refcodigo_ccv)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
            'Nodo_X.ExpandAll()
            If Tabla.Rows(i).Item("codigo_tre") = "A" Then
                Nodo_X.PopulateOnDemand = False
                Nodo_X.Target = "_blank"
                Nodo_X.ImageUrl = Tabla.Rows(i).Item("icono_tre")
                Nodo_X.NavigateUrl = Tabla.Rows(i).Item("nombrearchivo")
            End If
            Nodo_X.Text = "&nbsp;" & Tabla.Rows(i).Item("titulo_ccv")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_ccv")
            Nodo.ChildNodes.Add(Nodo_X)
            AgregaItem(Nodo_X, idcursovirtual, Tabla.Rows(i).Item("codigo_ccv"))
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
        ObjDatos = Nothing
    End Sub
End Class
