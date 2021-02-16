Imports System.Security.Cryptography
Partial Class carpetas
    Inherits System.Web.UI.Page
    Dim obj As New clsaccesodatos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Try
            ' Try ' try 'try
            'Cargar solamente menús Padre
            Me.hdid.Value = Request.Form("id")
            Me.hdctf.Value = Request.Form("ctf")
            Me.hdcapl.Value = Request.Form("capl")

            obj.abrirconexion()
            Dim tbl As Data.DataTable
            'Cargar Menú Principal
            tbl = obj.TraerDataTable("ConsultarAplicacionUsuario", 0, Me.hdcapl.Value, Me.hdctf.Value, 0)
            Me.dlMenuPadre.DataSource = obj.TraerDataTable("ConsultarAplicacionUsuario", 11, Me.hdcapl.Value, Me.hdctf.Value, 0)
            Me.dlMenuPadre.DataBind()

            obj.cerrarconexion()
            obj = Nothing

            If tbl.Rows.Count > 0 Then
                ' Me.lblModulo.Text = StrConv(tbl.Rows(0).Item("descripcion_apl").ToString, 3)
                Me.lblModulo.Text = tbl.Rows(0).Item("descripcion_apl").ToString
            Else
                Me.lblmensaje.Visible = True
            End If
            tbl.Dispose()
            'Catch ex As Exception
            'obj.cerrarconexion()
            'End Try
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
                    Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "&id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value '& "&capl=" & EncriptarMenu(Me.hdcapl.value)
                Else

                    'If InStr(Tabla.Rows(i).Item("enlace_men"), "?p=") > 0 Then 'enviar ruta querystring por POST
                    'Me.hdRutas.Value = Tabla.Rows(i).Item("enlace_men")
                    'Nodo_X.NavigateUrl = "pase_campus.asp?id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value

                    If InStr(Tabla.Rows(i).Item("enlace_men"), "/reportServer/") > 0 Then
                        Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "&id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value  '& "&capl=" & EncriptarMenu(Me.hdcapl.value)
                    Else
                        If InStr(Tabla.Rows(i).Item("enlace_men"), "?") > 0 Then 'Si no encuentra una referencia
                            Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "&id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value & "&apl=" & codigo_apl  '& "&capl=" & EncriptarMenu(Me.hdcapl.value)
                        Else
                            If (Tabla.Rows(i).Item("codigo_men") = 1484) Then
                                Me.trwsubMenus.Attributes.Add("onclick", "OcultarFrame()")
                            End If
                            Nodo_X.NavigateUrl = "../personal/" & Tabla.Rows(i).Item("enlace_men") & "?id=" & Me.hdid.Value & "&ctf=" & Me.hdctf.Value & "&apl=" & codigo_apl  '& "&capl=" & EncriptarMenu(Me.hdcapl.value)
                        End If
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
    Protected Sub dlMenuPadre_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlMenuPadre.ItemCommand
        'Cargar SubMenús
        obj.abrirconexion()
        Me.trwsubMenus.Nodes.Clear()
        CargarSubMenus(Me.hdcapl.Value, Me.hdctf.Value, Nothing, CInt(dlMenuPadre.DataKeys(e.Item.ItemIndex)))
        obj.cerrarconexion()
        obj = Nothing
        e.Item.CssClass = "mnuPadreElegido"
    End Sub

    Public Function EncriptarMenu(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

  

End Class