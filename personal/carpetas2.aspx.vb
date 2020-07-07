
Partial Class carpetas2
    Inherits System.Web.UI.Page
    Dim obj As New clsaccesodatos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Try

                'Cargar solamente menús Padre
                Me.hdid.Value = Request.Form("id")
                Me.hdctf.Value = Request.Form("ctf")
                Me.hdcapl.Value = Request.Form("capl")

                obj.abrirconexion()
                'Cargar Menú Principal
                CargarSubMenus(Me.hdcapl.Value, Me.hdctf.Value, Nothing,0)
                obj.cerrarconexion()
                obj = Nothing

                Me.lblmensaje.Visible = trwsubMenus.nodes.Count = 0
            Catch ex As Exception
                obj.cerrarconexion()
            End Try
        End If
    End Sub
    Private Sub CargarSubMenus(ByVal codigo_apl As int16, ByVal codigo_tfu As int16, ByVal Nodo As TreeNode, ByVal refcodigo_men As Integer)
        Dim Tabla As Data.DataTable
        Tabla = obj.TraerDataTable("ConsultarAplicacionUsuario", 11, codigo_apl, codigo_tfu, refcodigo_men)
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            If Tabla.Rows(i).Item("enlace_men").ToString <> "" Then
                Nodo_X.PopulateOnDemand = False
                Nodo_X.Target = "fraPrincipal"

                If InStr(Tabla.Rows(i).Item("enlace_men"), "../rptusat/?/") > 0 Then
                    Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "&id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value
                Else
                    If InStr(Tabla.Rows(i).Item("enlace_men"), "?") > 0 Then 'Si no encuentra una referencia
                        Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "&id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value
                    Else
                        Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "?id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value
                    End If
                End If

                Nodo_X.Expanded = False
            Else
                Nodo_X.SelectAction = TreeNodeSelectAction.Expand
            End If

            Nodo_X.CollapseAll()
            Nodo_X.Text = "&nbsp;" & Tabla.Rows(i).Item("descripcion_men")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_men")

            If IsNothing(Nodo) Then
                Me.trwsubMenus.Nodes.Add(Nodo_X)
            Else
                Nodo.ChildNodes.Add(Nodo_X)
            End If

            If Tabla.Rows(i).Item("total_men") > 0 Then
                Nodo_X.ImageUrl = "../images/librocerrado.gif" 'Tabla.Rows(i).Item("icono_men")
                CargarSubMenus(codigo_apl, codigo_tfu, Nodo_X, Tabla.Rows(i).Item("codigo_men"))
            Else
                Nodo_X.ImageUrl = "../images/librohoja.gif" 'Tabla.Rows(i).Item("icono_men")
            End If
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
    End Sub
End Class