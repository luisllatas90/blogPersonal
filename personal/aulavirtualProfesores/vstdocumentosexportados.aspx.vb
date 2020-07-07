
Partial Class vstdocumentosexportados
    Inherits System.Web.UI.Page
    Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            If Request.QueryString("id") <> "" Then
                Me.trw.Nodes.Clear()
                CargarDocumentos("DI_RecursosCursoVirtual_vN2", Request.QueryString("id"), Nothing, 0)
            End If
        End If
    End Sub
    Private Sub CargarDocumentos(ByVal sp As String, ByVal idcursovirtual As Integer, ByVal Nodo As TreeNode, ByVal refcodigo_ccv As Integer)
        Dim Tabla As Data.DataTable
        Tabla = Obj.TraerDataTable(sp, idcursovirtual, refcodigo_ccv)
        For i As Int32 = 0 To Tabla.Rows.Count - 1

            Dim Nodo_X As New TreeNode

            Nodo_X.ExpandAll()
            Nodo_X.ImageUrl = Tabla.Rows(i).Item("icono_tre")
            Nodo_X.Text = "&nbsp;" & Tabla.Rows(i).Item("titulo_ccv")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_ccv")
            If Tabla.Rows(i).Item("codigo_tre") = "A" Then
                Nodo_X.PopulateOnDemand = False
                Nodo_X.Target = "_blank"

                If Tabla.Rows(i).Item("nombrearchivo") <> "" Then
                    Nodo_X.NavigateUrl = "documentos/" & Tabla.Rows(i).Item("nombrearchivo")
                End If
            End If

            If IsNothing(Nodo) Then
                Me.trw.Nodes.Add(Nodo_X)
            Else
                Nodo.ChildNodes.Add(Nodo_X)
            End If

            If Tabla.Rows(i).Item("total_ccv") > 0 Then
                Nodo_X.SelectAction = TreeNodeSelectAction.None
                CargarDocumentos(sp, idcursovirtual, Nodo_X, Tabla.Rows(i).Item("codigo_ccv"))
            End If
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
    End Sub
End Class
