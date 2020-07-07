Imports System.Data
Partial Class DirectorInvestigacion_administrarAreasLineasTematicas
    Inherits System.Web.UI.Page
    Dim xID As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New clsInvestigacion
        xID = Request.QueryString("xid")
        If Request.QueryString("del") = "A" Then
            obj.AbrirTransaccionCnx()
            obj.EliminarArea(xID, "usureg")
            obj.CerrarTransaccionCnx()
        Else
            obj.AbrirTransaccionCnx()
            obj.EliminarLinea(xID, "usureg")
            obj.CerrarTransaccionCnx()
        End If


        If Not Page.IsPostBack Then
            obj = New clsInvestigacion
            Dim dtComite, dtArea, dtLinea As DataTable

            Dim divContent As String = ""
            Dim varC, varA, varL As String
            Dim href As String = "#"

            divContent = "<ul id='red' class='treeview' style='cursor:hand;'>"

            dtComite = obj.ConsultaComite(0, 0)

            If dtComite.Rows.Count Then

                For i As Int16 = 0 To dtComite.Rows.Count - 1
                    href = "frmAgregarEditarAreaLinea.aspx?act=A&typ=A&xid=" & dtComite.Rows(i).Item(0) & "&xna="
                    varC = "C" & i
                    divContent &= "<li>"

                    divContent &= "  <div dataid='" & dtComite.Rows(i).Item(0) & "'>"

                    divContent &= "   <div style='float:left;' >"

                    divContent &= dtComite.Rows(i).Item(1)
                    divContent &= "   </div>"
                   

                    divContent &= "  </div>"

                    dtArea = obj.ConsultaArea(0, dtComite.Rows(i).Item(0))
                    If dtArea.Rows.Count Then
                        divContent &= "<ul style='cursor:hand;'>"
                        For j As Int16 = 0 To dtArea.Rows.Count - 1
                            varA = "A" & j
                            'divContent &= "<li><a onmouseover='mostrar(2);' href='?xid=" & dtArea.Rows(j).Item(0) & "&xna=" & dtArea.Rows(j).Item(1) & "'>" & dtArea.Rows(j).Item(1) & "</a>"
                            divContent &= "<li>    "

                            divContent &= "  <div>"
                            divContent &= "   <div style='float:left; 'onmouseover='showdiv(1,""" & varA & """);'>"
                            divContent &= dtArea.Rows(j).Item(1)
                            divContent &= "   </div>"

                            divContent &= "   <div id='" & varA & "' style='float:left;display:none;padding-left:10px;border:1px solid;'>"
                            href = "frmAgregarEditarAreaLinea.aspx?act=A&typ=L&xid=" & dtArea.Rows(j).Item(0) & "&xna="
                            divContent &= "     <a id='enlace' class='ifancybox' href='" & href & "'>+ Agregar Línea</a> | "
                            href = "frmAgregarEditarAreaLinea.aspx?act=E&typ=A&xid=" & dtArea.Rows(j).Item(0) & "&xna=" & dtArea.Rows(j).Item(1)
                            divContent &= "     <a id='enlace' class='ifancybox' href='" & href & "'>(*) Mofidicar</a> | "
                            href = "?xid=" & dtArea.Rows(j).Item(0) & "&del=A"
                            divContent &= "     <a href='" & href & "'> - Eliminar</a> | "
                            divContent &= "   </div>"

                            divContent &= "  </div>"


                            dtLinea = obj.ConsultaLinea(0, dtArea.Rows(j).Item(0))

                            If dtLinea.Rows.Count Then
                                divContent &= "<ul>"
                                For k As Int16 = 0 To dtLinea.Rows.Count - 1
                                    varL = "L" & k
                                    divContent &= "<li>    "

                                    divContent &= "  <div>"
                                    divContent &= "   <div style='float:left; padding-left:10px;' onmouseover='showdiv(1,""" & varL & """);'>"
                                    divContent &= dtLinea.Rows(k).Item(1)
                                    divContent &= "   </div>"
                                    divContent &= "   <div id='" & varL & "' style='float:left;display:none;padding-left:10px;' class='OpcA'>"

                                    href = "frmAgregarEditarAreaLinea.aspx?act=E&typ=L&xid=" & dtLinea.Rows(k).Item(0) & "&xna=" & dtLinea.Rows(k).Item(1)
                                    divContent &= "     <a id='enlace' class='ifancybox' href='" & href & "'>(*) Mofidicar</a> | "

                                    href = "?xid=" & dtLinea.Rows(k).Item(0) & "&del=L"
                                    divContent &= "     <a href='" & href & "'> - Eliminar</a> | "
                                    divContent &= "   </div>"

                                    divContent &= "  </div>"

                                Next
                                divContent &= "</ul>"
                            End If
                            divContent &= "</li>"
                        Next
                        divContent &= "</ul>"
                    End If

                    divContent &= "</li>"
                Next
                divContent &= "</ul>"
            End If
            Me.DivContenedor.InnerHtml = divContent
            dtComite = Nothing
            dtArea = Nothing
            dtLinea = Nothing
            obj = Nothing
        End If
    End Sub

    'Private Sub AgregaItem(ByVal Nodo As TreeNode, ByVal codigo_are As Integer)
    '    Dim Tabla As New Data.DataTable
    '    Dim ObjArbol As New clsInvestigacion
    '    Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
    '    Tabla = ObjArbol.ConsultaLinea(0, codigo_are)
    '    For i As Int32 = 0 To Tabla.Rows.Count - 1
    '        Dim Nodo_X As New TreeNode
    '        Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
    '        'Nodo_X.ExpandAll()
    '        Nodo_X.Text = Tabla.Rows(i).Item("nombre")
    '        Nodo_X.Value = Tabla.Rows(i).Item("id")
    '        Nodo.ChildNodes.Add(Nodo_X)
    '        'AgregaItem(Nodo_X, Tabla.Rows(i).Item("id"))
    '        'Response.Write(Tabla.Rows(i).Item("nombre") & " - " & Tabla.Rows(i).Item("id"))
    '        Nodo_X = Nothing
    '    Next
    '    Tabla.Dispose()
    '    Tabla = Nothing
    '    ObjDatos = Nothing
    'End Sub

    'Private Sub LlenarArbol()
    '    Dim Tabla1 As DataTable
    '    Me.TreeUnidades.Nodes.Clear()

    '    Dim ObjArbol As New clsInvestigacion
    '    Tabla1 = ObjArbol.ConsultaComite(0, 0)

    '    For i As Int16 = 0 To Tabla1.Rows.Count - 1
    '        Dim Nodo As New TreeNode
    '        Nodo.SelectAction = TreeNodeSelectAction.Expand
    '        Nodo.Text = Tabla1.Rows(i).Item(1)
    '        Nodo.Value = Tabla1.Rows(i).Item(0)
    '        Me.TreeUnidades.Nodes.Add(Nodo)

    '        Dim datos As New Data.DataTable
    '        datos = ObjArbol.ConsultaArea(0, Tabla1.Rows(i).Item(0))

    '        For j As Int32 = 0 To datos.Rows.Count - 1
    '            Dim Nodo2 As New TreeNode
    '            Nodo2.SelectAction = TreeNodeSelectAction.SelectExpand
    '            Nodo2.PopulateOnDemand = False
    '            'Nodo2.NavigateUrl = "pagina.aspx?" & j.ToString
    '            'Nodo2.ExpandAll()
    '            Nodo2.Text = datos.Rows(j).Item("nombre")
    '            Nodo2.Value = datos.Rows(j).Item("id")
    '            Me.TreeUnidades.Nodes(i).ChildNodes.Add(Nodo2)
    '            AgregaItem(Nodo2, datos.Rows(j).Item("id"))
    '            Nodo2 = Nothing
    '        Next
    '    Next
    'End Sub
End Class

