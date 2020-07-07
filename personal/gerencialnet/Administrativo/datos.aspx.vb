'Clase del Formulario datos correspondiente al Modulo de Reportes Academicos
'Elaborado por  : Wilfredo Aljobin Cumpa
'Fecha          : 24/10/2006
'Observaciones  : Se encuentra el codigo para realizar todas las consultas al CUBO
'del analysis services, se han utilizado funciones dentro de la misma
'clase que ayudan a reducir un tanto el codigo fuente
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Partial Class Academico_datos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            MuestraCabecera("Total Recaudado de Ingresos segun Tipo de Documento, Escuelas profesionales y Servicios")
            LlenaCarrera()
            LlenaServicios()
            LlenaTiempo()
            LlenaCiclo()
            MostrarReporte()
        End If


    End Sub

    ' Codigo especialmente de diseño de la cabecera de la pagina, no se coloco directaemnte en la pagina
    ' porque le metodo responsewrite se ejecuta primero que el metodo LOAD
    Protected Sub MuestraCabecera(ByVal nombre As String)
        Dim HTML As String
        HTML = "<table width='100%'  align='center' border='0' cellspacing='0' cellpadding='0'>"
        HTML = HTML & "<table width='100%' border='0' cellspacing='0'><tr>"
        HTML = HTML & "<td width='65' height='60' align='right'><img class='NoImprimir' src='../images/cubo_.jpg' width='40' height='40'></td>"
        HTML = HTML & "<td width='651'><strong><font size='3' face='Arial'>" & nombre & "</font></strong></td><td width='263' align='right'>"
        'HTML = HTML & "<a href='javascript:abrir();'><img class='NoImprimir' border='0' alt='Reportes Personalizados' src='../images/softwareD.gif' width='31' height='31'></a>"
        'HTML = HTML & "<a href='main.html' target='_self'><img class='NoImprimir' border='0' alt='Pulsa aquí para regresar a la página inicial' src='../images/home.jpg' width='31' height='29'></a>&nbsp;"
        HTML = HTML & "<a href='javascript:print()'><img class='NoImprimir' border='0' alt='Pulsa aquí para imprimir esta página' src='../images/printer.jpg' width='39' height='31'></a>"
        HTML = HTML & "<a href='graCubModalidadIngreso.htm'></a>"
        'HTML = HTML & "<a href='javascript:history.back()'><img class='NoImprimir' border='0' alt='Regresar' src='../images/back.jpg' width='32' height='32'></a></td>"
        HTML = HTML & "</tr></table><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr bgcolor='#990000'>"
        HTML = HTML & "<td  height='2' width='10'></td>"
        HTML = HTML & "<td></td></tr><tr align='center'><td colspan='2'></td></tr></table></td></tr><tr><td align='center'></td></tr></table>"
        Response.Write(HTML)
    End Sub

    ' Muestro la cabecere que me va a permitir realizar la funcion abrir el PopPup pata personalizacion de datos

    Protected Sub LlenaCarrera()
        Dim adapter1 As SqlDataAdapter
        Dim adapter2 As SqlDataAdapter
        Dim Tabla1 As New DataTable
        Dim Tabla2 As New DataTable
        Try
            Me.Arbol1.ToolTip = "Carreras Profesionales y Otros programas de estudios."
            Dim TreeDim As New TreeNode
            adapter1 = New SqlDataAdapter("pa_facultad_mostrar", ConfigurationManager.ConnectionStrings(1).ConnectionString)
            adapter1.SelectCommand.CommandType = CommandType.StoredProcedure
            adapter1.Fill(Tabla1)
            TreeDim.Text = "Carreraprofesional"
            TreeDim.Value = "Carreraprofesional"
            TreeDim.PopulateOnDemand = False
            'TreeDim.SelectAction = TreeNodeSelectAction.None
            TreeDim.SelectAction = TreeNodeSelectAction.SelectExpand
            Arbol1.Nodes.Add(TreeDim)
            For i As Integer = 0 To Tabla1.Rows.Count - 1
                Dim treelevel As New TreeNode
                treelevel.Text = Tabla1.Rows(i).Item(0)
                treelevel.Value = Tabla1.Rows(i).Item(0)
                treelevel.PopulateOnDemand = False
                'treelevel.ShowCheckBox = True
                TreeDim.ChildNodes.Add(treelevel)
                adapter2 = New SqlDataAdapter("pa_carrera_mostrar", ConfigurationManager.ConnectionStrings(1).ConnectionString)
                adapter2.SelectCommand.CommandType = CommandType.StoredProcedure
                adapter2.SelectCommand.Parameters.Add("@facultad", SqlDbType.VarChar, 60).Value = Tabla1.Rows(i).Item(0)
                adapter2.Fill(Tabla2)
                For j As Integer = 0 To Tabla2.Rows.Count - 1
                    Dim Miembro As New TreeNode
                    Miembro.Text = Tabla2.Rows(j).Item(0)
                    Miembro.Value = Tabla2.Rows(j).Item(0)
                    Miembro.PopulateOnDemand = True
                    Miembro.ShowCheckBox = True
                    Miembro.SelectAction = TreeNodeSelectAction.None
                    treelevel.ChildNodes.Add(Miembro)
                    Miembro = Nothing
                Next
                Tabla2.Clear()
                treelevel = Nothing
            Next
            Arbol1.ExpandDepth = 1
        Catch ex As Exception

        End Try
        adapter1 = Nothing
        adapter2 = Nothing
    End Sub

    Protected Sub LlenaServicios()
        Dim adapter1 As SqlDataAdapter
        Dim adapter2 As SqlDataAdapter
        Dim Tabla1 As New DataTable
        Dim Tabla2 As New DataTable
        Try
            Me.Arbol2.ToolTip = "Servicios que la universidad presta."
            Dim TreeDim As New TreeNode
            adapter1 = New SqlDataAdapter("pa_servicio_grupos", ConfigurationManager.ConnectionStrings(1).ConnectionString)
            adapter1.SelectCommand.CommandType = CommandType.StoredProcedure
            adapter1.Fill(Tabla1)
            TreeDim.Text = "Servicio"
            TreeDim.Value = "Servicio"
            TreeDim.PopulateOnDemand = False
            'TreeDim.SelectAction = TreeNodeSelectAction.None
            TreeDim.SelectAction = TreeNodeSelectAction.SelectExpand
            Arbol2.Nodes.Add(TreeDim)
            For i As Integer = 0 To Tabla1.Rows.Count - 1
                Dim treelevel As New TreeNode
                treelevel.Text = Tabla1.Rows(i).Item(0)
                treelevel.Value = Tabla1.Rows(i).Item(0)
                treelevel.PopulateOnDemand = False
                'treelevel.ShowCheckBox = True
                TreeDim.ChildNodes.Add(treelevel)
                adapter2 = New SqlDataAdapter("pa_servicio_lista", ConfigurationManager.ConnectionStrings(1).ConnectionString)
                adapter2.SelectCommand.CommandType = CommandType.StoredProcedure
                adapter2.SelectCommand.Parameters.Add("@grupo", SqlDbType.VarChar, 60).Value = Tabla1.Rows(i).Item(0)
                adapter2.Fill(Tabla2)
                For j As Integer = 0 To Tabla2.Rows.Count - 1
                    Dim Miembro As New TreeNode
                    Miembro.Text = Tabla2.Rows(j).Item(0)
                    Miembro.Value = Tabla2.Rows(j).Item(0)
                    Miembro.PopulateOnDemand = True
                    Miembro.ShowCheckBox = True
                    Miembro.SelectAction = TreeNodeSelectAction.None
                    treelevel.ChildNodes.Add(Miembro)
                    Miembro = Nothing
                Next
                Tabla2.Clear()
                treelevel = Nothing
            Next
            Arbol2.ExpandDepth = 1
        Catch ex As Exception

        End Try
        adapter1 = Nothing
        adapter2 = Nothing
    End Sub

    Protected Sub LlenaCiclos()
        Dim adapter1 As SqlDataAdapter
        Dim adapter2 As SqlDataAdapter
        Dim Tabla1 As New DataTable
        Dim Tabla2 As New DataTable
        Try

            Dim TreeDim As New TreeNode
            adapter1 = New SqlDataAdapter("pa_cicloacademico_grupos", ConfigurationManager.ConnectionStrings(1).ConnectionString)
            adapter1.SelectCommand.CommandType = CommandType.StoredProcedure
            adapter1.Fill(Tabla1)
            TreeDim.Text = "Ciclo Academico"
            TreeDim.Value = "Ciclo Academico"
            TreeDim.PopulateOnDemand = False
            'TreeDim.SelectAction = TreeNodeSelectAction.None
            TreeDim.SelectAction = TreeNodeSelectAction.SelectExpand
            Arbol3.Nodes.Add(TreeDim)
            For i As Integer = 0 To Tabla1.Rows.Count - 1
                Dim treelevel As New TreeNode
                treelevel.Text = Tabla1.Rows(i).Item(0)
                treelevel.Value = Tabla1.Rows(i).Item(0)
                treelevel.PopulateOnDemand = False
                ' treelevel.ShowCheckBox = True
                TreeDim.ChildNodes.Add(treelevel)
                adapter2 = New SqlDataAdapter("pa_cicloacademico_ciclos", ConfigurationManager.ConnectionStrings(1).ConnectionString)
                adapter2.SelectCommand.CommandType = CommandType.StoredProcedure
                adapter2.SelectCommand.Parameters.Add("@grupo", SqlDbType.VarChar, 60).Value = Tabla1.Rows(i).Item(0)
                adapter2.Fill(Tabla2)
                For j As Integer = 0 To Tabla2.Rows.Count - 1
                    Dim Miembro As New TreeNode
                    Miembro.Text = Tabla2.Rows(j).Item(0)
                    Miembro.Value = Tabla2.Rows(j).Item(0)
                    Miembro.PopulateOnDemand = True
                    Miembro.ShowCheckBox = True
                    Miembro.SelectAction = TreeNodeSelectAction.None
                    treelevel.ChildNodes.Add(Miembro)
                    Miembro = Nothing
                Next
                Tabla2.Clear()
                treelevel = Nothing
            Next
            Arbol3.ExpandDepth = 1
        Catch ex As Exception

        End Try
        adapter1 = Nothing
        adapter2 = Nothing
    End Sub

    Protected Sub LlenaTiempo()
        Dim adapter1 As SqlDataAdapter
        Dim adapter2 As SqlDataAdapter
        Dim Tabla1 As New DataTable
        Dim Tabla2 As New DataTable
        Try
            Me.Arbol3.ToolTip = "Tiempo en que ha sido registrado el pago en la universidad."
            Dim TreeDim As New TreeNode
            adapter1 = New SqlDataAdapter("pa_tiempo_año", ConfigurationManager.ConnectionStrings(1).ConnectionString)
            adapter1.SelectCommand.CommandType = CommandType.StoredProcedure
            adapter1.Fill(Tabla1)
            TreeDim.Text = "Tiempo"
            TreeDim.Value = "Tiempo"
            TreeDim.PopulateOnDemand = False
            'TreeDim.SelectAction = TreeNodeSelectAction.None
            TreeDim.SelectAction = TreeNodeSelectAction.SelectExpand
            Arbol3.Nodes.Add(TreeDim)
            For i As Integer = 0 To Tabla1.Rows.Count - 1
                Dim treelevel As New TreeNode
                treelevel.Text = Tabla1.Rows(i).Item(0)
                treelevel.Value = Tabla1.Rows(i).Item(0)
                treelevel.PopulateOnDemand = False
                'treelevel.ShowCheckBox = True
                TreeDim.ChildNodes.Add(treelevel)
                adapter2 = New SqlDataAdapter("pa_tiempo_mes", ConfigurationManager.ConnectionStrings(1).ConnectionString)
                adapter2.SelectCommand.CommandType = CommandType.StoredProcedure
                adapter2.SelectCommand.Parameters.Add("@año", SqlDbType.VarChar, 60).Value = Tabla1.Rows(i).Item(0)
                adapter2.Fill(Tabla2)
                For j As Integer = 0 To Tabla2.Rows.Count - 1
                    Dim Miembro As New TreeNode
                    Miembro.Text = Tabla2.Rows(j).Item(0)
                    Miembro.Value = Tabla2.Rows(j).Item(0)
                    Miembro.PopulateOnDemand = True
                    Miembro.ShowCheckBox = True
                    Miembro.SelectAction = TreeNodeSelectAction.None
                    treelevel.ChildNodes.Add(Miembro)
                    Miembro = Nothing
                Next
                Tabla2.Clear()
                treelevel = Nothing
            Next
            Arbol3.ExpandDepth = 1
        Catch ex As Exception

        End Try
        adapter1 = Nothing
        adapter2 = Nothing
    End Sub

    Public Function LeerArbol(ByVal arbol As TreeView) As String
        Dim StrSQL As String = Nothing

        For i As Integer = 0 To arbol.Nodes(0).ChildNodes.Count - 1
            For j As Integer = 0 To arbol.Nodes(0).ChildNodes(i).ChildNodes.Count - 1
                If (arbol.Nodes(0).ChildNodes(i).ChildNodes(j).Checked = True) Then
                    StrSQL = StrSQL & ", [" & arbol.Nodes(0).Value & "].[" & arbol.Nodes(0).ChildNodes(i).Value & "].[" & arbol.Nodes(0).ChildNodes(i).ChildNodes(j).Value & "]"
                End If
            Next
        Next

        If arbol.SelectedNode Is Nothing And StrSQL Is Nothing Then
            StrSQL = "[" & arbol.Nodes(0).Value & "].children"
            LeerArbol = StrSQL
            Exit Function
        End If

        If arbol.SelectedNode IsNot Nothing And StrSQL Is Nothing Then
            If arbol.SelectedValue = arbol.Nodes(0).Value Then
                StrSQL = "[" & arbol.SelectedValue & "]." & "children"
            Else
                StrSQL = "[" & arbol.Nodes(0).Value & "].[" & arbol.SelectedValue & "]"
            End If
            LeerArbol = StrSQL
            Exit Function
        End If

        If StrSQL Is Nothing Then
            StrSQL = "[[" & arbol.Nodes(0).Value & "].[" & arbol.SelectedNode.Value & "].children"
        End If
        LeerArbol = Mid(StrSQL, 2)
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        MuestraCabecera("Total Recaudado de Ingresos segun Tipo de Documento, Escuelas profesionales y Servicios")
        MostrarReporte()
    End Sub

    Public Sub MarcarArbol(ByVal arbol As TreeView)
        Try
            For i As Integer = 0 To arbol.Nodes(0).ChildNodes.Count - 1
                If arbol.Nodes(0).ChildNodes(i).Text = arbol.SelectedNode.Text Then
                    For j As Integer = 0 To arbol.Nodes(0).ChildNodes(i).ChildNodes.Count - 1
                        arbol.Nodes(0).ChildNodes(i).ChildNodes(j).Checked = True
                        arbol.Nodes(0).ChildNodes(i).Expanded = True
                    Next
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub

    Public Sub QuitarArbol(ByVal arbol As TreeView)
        Try
            For i As Integer = 0 To arbol.Nodes(0).ChildNodes.Count - 1
                If arbol.Nodes(0).ChildNodes(i).Text = arbol.SelectedNode.Text Then
                    For j As Integer = 0 To arbol.Nodes(0).ChildNodes(i).ChildNodes.Count - 1
                        arbol.Nodes(0).ChildNodes(i).ChildNodes(j).Checked = False
                        arbol.Nodes(0).ChildNodes(i).Expanded = False
                    Next
                End If
            Next
        Catch ex As Exception

        End Try
        
    End Sub

    Public Function ObtenerConsulta(ByVal Param As String, ByVal miembro As String, ByVal arbol1 As TreeView, ByVal arbol2 As TreeView, ByVal arbol3 As TreeView, _
        ByVal pos1 As Integer, ByVal pos2 As Integer, ByVal pos3 As Integer, ByVal valores As Integer, ByVal filtro As String, ByVal cubo As String) As String
        Dim StrSQL As String
        StrSQL = Param & " SELECT NON EMPTY {" & LeerArbol(arbol1) & "} on axis(" & CStr(pos1) & ")"
        StrSQL = StrSQL & ", NON EMPTY {" & LeerArbol(arbol2) & "} on axis(" & CStr(pos2) & ")"
        StrSQL = StrSQL & ", NON EMPTY {" & LeerArbol(arbol3) & "} on axis(" & CStr(pos3) & ")"
        StrSQL = StrSQL & ", NON EMPTY {" & miembro & "} on axis(" & CStr(valores) & ")"
        StrSQL = StrSQL & ", NON EMPTY {documento.[RECIBO DE INGRESO],documento.[BOLETA PROVISIONAL],documento.[FACTURA PROVISIONAL], documento.[RECIBO PRE]} on axis(4) "
        StrSQL = StrSQL & " from " & cubo & ""
        StrSQL = StrSQL & filtro
        Return StrSQL
    End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'MostrarReporte()
        MuestraCabecera("Total Recaudado de Ingresos segun Tipo de Documento, Escuelas profesionales y Servicios")
        Panel.Height = 500
    End Sub

    Protected Sub LlenaCiclo()
        Dim cnx As New SqlConnection
        Dim cmd As SqlCommand
        Dim reader As SqlDataReader
        Try
            cnx.ConnectionString = ConfigurationManager.ConnectionStrings(1).ConnectionString
            cnx.Open()
            cmd = New SqlCommand("pa_consultar_ciclo", cnx)
            cmd.CommandType = CommandType.StoredProcedure
            reader = cmd.ExecuteReader
            Me.DDLCiclo.Items.Add("-- Todos -- ")
            Me.DDLCiclo.Items(0).Value = "0"
            While reader.Read
                Me.DDLCiclo.Items.Add(reader.Item(0))
            End While
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub MostrarReporte()
        Panel.Height = 40
        Dim objcubo As New cuboAdm
        objcubo.Cubo = "cubcajaingreso"
        Dim x, y As Int16
        Dim strWhere As String
        x = Me.DDLModo.SelectedValue
        If x = 1 Then
            y = 2
        Else
            y = 1
        End If
        'strWhere = " WHERE (estado.ingreso, documento.[RECIBO DE INGRESO]"
        strWhere = " WHERE (estado.ingreso"
        If Me.DDLCiclo.SelectedValue <> "0" Then
            strWhere = strWhere & ", cicloacademico.[" & Me.DDLCiclo.SelectedValue & "])"
        Else
            strWhere = strWhere & ")"
        End If
        objcubo._strConsulta = ObtenerConsulta("", "[measures].recaudado", Me.Arbol1, Me.Arbol2, Me.Arbol3, y, x, 0, 3, strWhere, "cubcajaingreso")

        If objcubo.MostrarDatos() IsNot Nothing Then
            Response.Write("<br><br>&nbsp;&nbsp;&nbsp;&nbsp;<font face='arial' color='blue' size='1'>Actualizado al : " & objcubo.MuestraFecha() & "</font>")
            Response.Write(objcubo.MostrarDatos())
        Else
            Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡No se encontraron coincidencias de las opciones seleccionadas!!!<br>Intentelo Nuevamente</center>")
        End If
        objcubo = Nothing
    End Sub

    
    Protected Sub CmdMarcar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdMarcar1.Click
        MarcarArbol(Me.Arbol1)
    End Sub

    Protected Sub CmdQuitar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdQuitar1.Click
        QuitarArbol(Arbol1)
    End Sub

    Protected Sub CmdMarcar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdMarcar2.Click
        MarcarArbol(Arbol2)
    End Sub

    Protected Sub CmdQuitar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdQuitar2.Click
        QuitarArbol(Arbol2)
    End Sub

    Protected Sub CmdMarcar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdMarcar3.Click
        MarcarArbol(Arbol3)
    End Sub

    Protected Sub CmdQuitar3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdQuitar3.Click
        QuitarArbol(Arbol3)
    End Sub
End Class
