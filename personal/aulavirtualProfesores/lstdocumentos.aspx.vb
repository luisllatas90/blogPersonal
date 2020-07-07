
Partial Class librerianet_aulavirtual_lstdocumentos
    Inherits System.Web.UI.Page
    Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Me.hdidcursovirtual.Value = 40 'Request.QueryString("idcursovirtual")
            Me.hdIdUsuario.Value = "USAT\gchunga"
            Me.hdcodigo_tfu.Value = 1

            Me.trw.Nodes.Clear()

            CargarCarpetas(Nothing, Me.hdidcursovirtual.Value, 0)
        End If
    End Sub

    Private Sub CargarCarpetas(ByVal NodoPadre As TreeNode, ByVal idcursovirtual As Integer, ByVal refcodigo_ccv As Integer)
        Dim Tabla As Data.DataTable

        Tabla = ObjDatos.TraerDataTable("ConsultarDocumento", "1a", Me.hdIdUsuario.Value, refcodigo_ccv, Me.hdidcursovirtual, 0)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim NodoHijo As New TreeNode
            NodoHijo.SelectAction = TreeNodeSelectAction.SelectExpand
            NodoHijo.PopulateOnDemand = False
            NodoHijo.Target = "_selft"
            NodoHijo.ImageUrl = "../../images/libroabierto.gif"

            NodoHijo.Text = "&nbsp;" & Tabla.Rows(i).Item("titulodocumento")
            NodoHijo.Value = Tabla.Rows(i).Item("iddocumento")

            If NodoPadre Is Nothing Then
                Me.trw.Nodes.Add(NodoHijo)
            End If
            If Tabla.Rows(i).Item("Nodos") > 0 Then
                Me.trw.Nodes.Add(NodoHijo)
                'Cargar nodos
                CargarCarpetas(NodoHijo, idcursovirtual, Tabla.Rows(i).Item("iddocumento"))
            Else
                NodoPadre.ChildNodes.Add(NodoHijo)
            End If
            NodoHijo = Nothing
        Next
        Tabla.Dispose()
    End Sub
End Class
