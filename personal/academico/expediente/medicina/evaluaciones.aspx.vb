
Partial Class medicina_evaluaciones
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.LinkRegresar.NavigateUrl = "sylabus.aspx?codigo_cac=" & Request.QueryString("codigo_cac") & "&codigo_per=" & Request.QueryString("codigo_per") & "&codigo_cup=" & Request.QueryString("codigo_cup") & "&nombre_per=" & Request.QueryString("nombre_per") & "&nombre_cur=" & Request.QueryString("nombre_cur")
        If IsPostBack = False Then
            Me.LblAsignatura.Text = Request.QueryString("nombre_cur")
            Me.LblProfesor.Text = Request.QueryString("nombre_per")
        End If
        CargarTree()

        Me.TreeActividad.Nodes(0).Select()
        Me.CmdNuevo.Enabled = True
        Me.CmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('frmevaluaciones.aspx?e=n&pa=0&syl=" & Request.QueryString("codigo_syl") & "&act=0',330,650,0,0,0,'Actividades'); return false;")

        Me.DataList1.DataBind()

    End Sub

    Private Sub CargarMenu(ByVal Nodo As TreeNode, ByVal codigo_act As Integer, Optional ByVal codigo_ActInsertar As Integer = 0)
        Dim Tabla As New Data.DataTable

        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Tabla = ObjDatos.TraerDataTable("MED_COnsultarActividades", codigo_act, Request.QueryString("codigo_syl"), "E")
        For i As Int32 = 0 To Tabla.Rows.Count - 1
            Dim Nodo_X As New TreeNode
            Nodo_X.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo_X.ExpandAll()
            Nodo_X.Text = Tabla.Rows(i).Item("descripcion_act")
            Nodo_X.Value = Tabla.Rows(i).Item("codigo_act")
            Nodo.ChildNodes.Add(Nodo_X)

            CargarMenu(Nodo_X, Tabla.Rows(i).Item("codigo_act"))
            Nodo_X = Nothing
        Next
        Tabla.Dispose()
        Tabla = Nothing
        ObjDatos = Nothing
    End Sub

    Protected Sub TreeActividad_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeActividad.SelectedNodeChanged
        Me.CmdModificar.Enabled = True
        Me.CmdNuevo.Enabled = True
        Me.CmdElminar.Enabled = True

        If Me.TreeActividad.SelectedNode.Value <> 0 Then
            Me.CmdModificar.Attributes.Add("OnClick", "AbrirPopUp('frmevaluaciones.aspx?e=m&syl=" & Request.QueryString("codigo_syl") & "&act=" & Me.TreeActividad.SelectedNode.Value & "',330,650,0,0,0,'Actividades'); return false;")
            Me.CmdNuevo.Attributes.Add("OnClick", "AbrirPopUp('frmevaluaciones.aspx?e=n&pa=" & Me.TreeActividad.SelectedValue & "&syl=" & Request.QueryString("codigo_syl") & "&act=" & Me.TreeActividad.SelectedNode.Value & "',330,650,0,0,0,'Actividades'); return false;")
        Else
            Me.CmdModificar.Enabled = False
            Me.CmdElminar.Enabled = False
        End If
        Me.HidenTree.Value = Me.TreeActividad.SelectedValue
    End Sub

    Protected Sub CmdElminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdElminar.Click
        Dim objDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If objDatos.TraerDataTable("MED_ConsultarEstadoActividad", Me.HidenTree.Value).Rows(0).Item("estadorealizado_act").ToString = "S" Then
            Response.Write("<script>alert('No se puede eliminar una actividad ya realizada.')</script>")
            objDatos = Nothing
        Else
            Try
                objDatos.IniciarTransaccion()
                objDatos.Ejecutar("MED_EliminarActividad", Me.HidenTree.Value)
                objDatos.TerminarTransaccion()
                Response.Write("<script>alert('Se eliminó la actividad correctamente')</script>")
                CargarTree()
            Catch ex As Exception
                objDatos.AbortarTransaccion()
                Response.Write("<script>alert('Ocurrio un error al procesar los datos, inténtelo nuevamente')</script>")
            End Try
        End If
        Me.CmdModificar.Enabled = False
        Me.CmdNuevo.Enabled = False
        Me.CmdElminar.Enabled = False

    End Sub

    Private Sub CargarTree()
        Me.TreeActividad.Nodes(0).ChildNodes.Clear()
        Dim datos As New Data.DataTable
        Dim ObjDatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        datos = ObjDatos.TraerDataTable("MED_COnsultarActividades", 0, Request.QueryString("codigo_syl"), "E")
        For i As Int32 = 0 To datos.Rows.Count - 1
            Dim Nodo As New TreeNode
            Nodo.SelectAction = TreeNodeSelectAction.SelectExpand
            Nodo.PopulateOnDemand = False
            Nodo.ExpandAll()
            Nodo.Text = datos.Rows(i).Item("descripcion_act")
            Nodo.Value = datos.Rows(i).Item("codigo_Act")
            Me.TreeActividad.Nodes(0).ChildNodes.Add(Nodo)
            CargarMenu(Nodo, datos.Rows(i).Item("codigo_act"))
            Nodo = Nothing
        Next
        datos.Dispose()
        datos = Nothing
        ObjDatos = Nothing
    End Sub
End Class
