
Partial Class indicadores_frmRegistroFormulaAgrupador
    Inherits System.Web.UI.Page

    Dim usuario As Integer

    Dim Formula As String = ""
    Dim FormulaCar As String = ""
    Dim Contador As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Lista Solo las perspectivas que pertenecen al plan viegente
                CargaComboConfiguracionPerspectivaPlan("A")
                Me.txtFormula.Enabled = False
                ConsultarGridRegistros("%")

                lblCodigo.Text = ""

            End If

            'Debe tomar del inicio de sesión
            usuario = Request.QueryString("id")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargaComboConfiguracionPerspectivaPlan(ByVal vParametro As String)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsIndicadores

            dts = obj.ConsultarConfiguracionPerspectivaPlan(vParametro)
            ddlPerspectivaplan.DataSource = dts
            ddlPerspectivaplan.DataTextField = "Descripcion"
            ddlPerspectivaplan.DataValueField = "Codigo"
            ddlPerspectivaplan.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPerspectivaplan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPerspectivaplan.SelectedIndexChanged
        Try
            Dim codigo_ppla As Integer
            If ddlPerspectivaplan.SelectedValue <> 0 Then
                codigo_ppla = ddlPerspectivaplan.SelectedValue
                CargarObjetivosSegunPlan(codigo_ppla)
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Seleccione un Plan de la lista."
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarObjetivosSegunPlan(ByVal codigo_ppla As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarObjetivosSegunPlan(codigo_ppla)
            ddlObjetivos.DataSource = dts
            ddlObjetivos.DataTextField = "Descripcion"
            ddlObjetivos.DataValueField = "Codigo"
            ddlObjetivos.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlObjetivos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlObjetivos.SelectedIndexChanged
        Try
            Dim Codigo_obj As Integer
            If ddlObjetivos.SelectedValue <> 0 Then
                Codigo_obj = ddlObjetivos.SelectedValue
                'Response.Write(Codigo_obj)
                CargarIndicadoresSegunObjetivo(Codigo_obj)
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Red
                lblMensaje.Text = "Seleccione un Objetivo de la Lista."
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarIndicadoresSegunObjetivo(ByVal Codigo_obj As Integer)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            If Codigo_obj <> 0 Then
                dts = obj.CargarIndicadoresSegunObjetivo(Codigo_obj)
                ddlIndicador.DataSource = dts
                ddlIndicador.DataTextField = "Descripcion"
                ddlIndicador.DataValueField = "Codigo"
                ddlIndicador.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treeInd_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treeInd.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulatePlan(e.Node)
                    Case 1
                        PopulateCentroCosto(e.Node)
                    Case 2
                        PopulatePerspectivas(e.Node)

                    Case 3
                        PopulateObjetivos(e.Node)
                    Case 4
                        PopulateIndicadores(e.Node)

                End Select

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulateCategories(e.Node)
                    Case 1
                        PopulateVariables(e.Node)
                    Case 2
                        PopulateSubVariables(e.Node)
                    Case 3
                        PopulateDimension(e.Node)
                    Case 4
                        PopulateSubDimension(e.Node)

                End Select
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulatePlan(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.CargarListaPlan()
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateCentroCosto(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaCentroCosto(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateCategories(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.CargarListaCategorias()

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Sub PopulatePerspectivas(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaPerspectivas(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateVariables(ByVal node As TreeNode)

        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaVariable(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateObjetivos(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaObjetivos(node.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Expand
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateSubVariables(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubVariables(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateIndicadores(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaIndicadores(node.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateDimension(ByVal node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaDimension(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub PopulateSubDimension(ByVal Node As TreeNode)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.CargarListaSubDimension(Node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion").ToString, row("Codigo").ToString)
                    NewNode.PopulateOnDemand = False
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    Node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub treeInd_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treeInd.SelectedNodeChanged
        Try
            Dim Value As String
            Dim Value1 As String
            Value = treeInd.SelectedNode.Value
            Value1 = treeInd.SelectedNode.Value.ToString.Trim + "~~"


            Formula = Formula.Trim + Value.ToString.Trim
            FormulaCar = Formula.Trim + Value1.ToString.Trim

            txtFormula.Text = txtFormula.Text.Trim + Value.ToString.Trim
            txtFormulaHide.Text = txtFormulaHide.Text.Trim + Value1.ToString.Trim

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treePrueba.SelectedNodeChanged
        Try
            Dim Value As String
            Dim Value1 As String
            'Dim Text As String = treePrueba.SelectedNode.Text

            Value = treePrueba.SelectedNode.Value.ToString.Trim
            Value1 = treePrueba.SelectedNode.Value.ToString.Trim + "~~"

            Formula = Formula.Trim + Value.ToString.Trim
            FormulaCar = Formula.Trim + Value1.ToString.Trim

            txtFormula.Text = txtFormula.Text.Trim + Value.ToString.Trim
            txtFormulaHide.Text = txtFormulaHide.Text.Trim + Value1.ToString.Trim

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            Formula = Formula + "7"
            FormulaCar = FormulaCar + "7" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton10_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton10.Click
        Try
            Formula = Formula + "0"
            FormulaCar = FormulaCar + "0" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton7_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton7.Click
        Try
            Formula = Formula + "1"
            FormulaCar = FormulaCar + "1" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton4.Click
        Try
            Formula = Formula + "4"
            FormulaCar = FormulaCar + "4" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Try
            Formula = Formula + "8"
            FormulaCar = FormulaCar + "8" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Try
            Formula = Formula + "9"
            FormulaCar = FormulaCar + "9" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton5_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        Try
            Formula = Formula + "5"
            FormulaCar = FormulaCar + "5" '  + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton6_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton6.Click
        Try
            Formula = Formula + "6"
            FormulaCar = FormulaCar + "6" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton8_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton8.Click
        Try
            Formula = Formula + "2"
            FormulaCar = FormulaCar + "2" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton9_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton9.Click
        Try
            Formula = Formula + "3"
            FormulaCar = FormulaCar + "3" '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton17_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton17.Click
        Try
            Formula = Formula + "."
            FormulaCar = FormulaCar + "." '+ "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton11_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton11.Click
        Try
            Formula = Formula + "/"
            FormulaCar = FormulaCar + "/" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton13_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton13.Click
        Try
            Formula = Formula + "("
            FormulaCar = FormulaCar + "(" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton14_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton14.Click
        Try
            Formula = Formula + ")"
            FormulaCar = FormulaCar + ")" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton12_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton12.Click
        Try
            Formula = Formula + "*"
            FormulaCar = FormulaCar + "*" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton22_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton22.Click
        Try
            Formula = Formula + "-"
            FormulaCar = FormulaCar + "-" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ImageButton23_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton23.Click
        Try
            Formula = Formula + "+"
            FormulaCar = FormulaCar + "+" + "~~"

            txtFormula.Text = txtFormula.Text + Formula
            txtFormulaHide.Text = txtFormulaHide.Text + FormulaCar
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Try
            txtFormula.Text = ""
            txtFormulaHide.Text = ""
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            If lblCodigo.Text = "" Then
                '--### Insertar una nueva Fórmula ---###
                dts = obj.InsertarModificarFormula(0, ddlIndicador.SelectedValue, txtFormulaHide.Text.Trim, txtFormula.Text.Trim, "1", usuario)
            Else
                'Edicion de registro
                dts = obj.InsertarModificarFormula(CType(lblCodigo.Text.trim, Integer), ddlIndicador.SelectedValue, txtFormulaHide.Text.Trim, txtFormula.Text.Trim, "1", usuario)
                'RestablecerEstado()
                LimpiarControles()
                ConsultarGridRegistros("%")
            End If

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If
            '--------------------------------------------------
            LimpiarControles()
            ConsultarGridRegistros("%")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub LimpiarControles()
        Try
            ddlIndicador.SelectedValue = 0
            ddlObjetivos.SelectedValue = 0
            ddlPerspectivaplan.SelectedValue = 0
            txtFormula.Text = ""
            txtFormulaHide.Text = ""
            lblCodigo.text = ""
            cmdGuardar.Text = "  Guardar"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvFormulas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvFormulas.PageIndexChanging
        Try
            gvFormulas.PageIndex = e.NewPageIndex
            ConsultarGridRegistros("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvFormulas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvFormulas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Select Case e.Row.Cells(8).Text
                Case "E"        ' Formula con Errores en la sintaxis
                    e.Row.Cells(8).Text = "<center><img src='../images/bola_roja.gif' style='border: 0px'/></center>"
                    'e.Row.Cells(8).ForeColor = Drawing.Color.Red
                    e.Row.BackColor = Drawing.Color.Tomato        'Pinta toda la fila
                Case "C"        ' Formula Correcta
                    e.Row.Cells(8).Text = "<center><img src='../images/bola_verde.gif' style='border: 0px'/></center>"
                    'e.Row.Cells(9).Text = ""
                    'e.Row.Cells(9).Controls.Remove(e.Row.Cells(9).Controls(0))

                    'e.Row.Cells(8).ForeColor = Drawing.Color.Blue
                    'e.Row.Cells[1].Controls.Remove(e.Row.Cells[5].Controls[0]);
                    'e.Row.Cells(9).Controls.Remove(e.Row.Cells(9).Controls(0))

                Case "N"        ' Formula sin procesar, por definir su sintaxis. Le asigna 'N', por defecto cuando se inserta la formula.
                    e.Row.Cells(8).Text = "<center><img src='../images/bola_naranja.gif' style='border: 0px'/></center>"
            End Select

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub gvFormulas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvFormulas.RowDeleting
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            Dim dtsv As New Data.DataTable

            'Validacion: Si la formula esta amarrada a un periodo laboral, no se puede eliminar.
            dtsv = obj.ValidarInclusionFormulaPeriodo(gvFormulas.DataKeys(e.RowIndex).Value)
            If dtsv.Rows(0).Item("Mensaje").ToString.Trim.Length > 0 Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dtsv.Rows(0).Item("Mensaje").ToString.Trim
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
                Exit Sub
            End If
            '--- fin validacion

            dts = obj.EliminaFormula(gvFormulas.DataKeys(e.RowIndex).Value.ToString())

            If dts.Rows(0).Item("rpt").ToString = "1" Then
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/accept.png")
                Me.avisos.Attributes.Add("class", "mensajeExito")
            Else
                lblMensaje.Visible = True
                lblMensaje.ForeColor = Drawing.Color.Black
                lblMensaje.Text = dts.Rows(0).Item("Mensaje").ToString
                Me.Image1.Attributes.Add("src", "../Images/exclamation.png")
                Me.avisos.Attributes.Add("class", "mensajeError")
            End If

            'Consultamos la lista de registros
            ConsultarGridRegistros("%")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ConsultarGridRegistros(ByVal vParametro As String)
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable
            dts = obj.ListaFormulasIndicadores(vParametro)

            If dts.Rows.Count > 0 Then
                gvFormulas.DataSource = dts
                gvFormulas.DataBind()
            Else
                gvFormulas.DataSource = Nothing
                gvFormulas.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub gvFormulas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFormulas.RowCommand
        Try
            If (e.CommandName.Equals("Select")) Then 'comprueba que sea el boton de seleccion   
                Dim seleccion As GridViewRow
                Dim codigofor_seleccion As Integer
                'lblCodigo.Visible = True            'Solo para pruebas, debe estar oculto 

                '1. Obtengo la linea del gridview que fue cliqueada
                'Response.Write(codigoobj_seleccion)
                seleccion = DirectCast(e.CommandSource, GridView).Rows(e.CommandArgument)
                codigofor_seleccion = Convert.ToInt32(gvFormulas.DataKeys(seleccion.RowIndex).Values("Codigo"))


                '2. Obtengo el datakey de la linea que donde está el boton que cliqueé
                'lblCodigo.Text = gvVariable.DataKeys(seleccion.RowIndex).Value.ToString
                lblCodigo.Text = codigofor_seleccion


                txtFormula.Text = HttpUtility.HtmlDecode(gvFormulas.Rows(seleccion.RowIndex).Cells.Item(3).Text)
                txtFormulaHide.Text = HttpUtility.HtmlDecode(gvFormulas.Rows(seleccion.RowIndex).Cells.Item(4).Text)
                txtFormulaHide.Text = Convert.ToString(gvFormulas.DataKeys(seleccion.RowIndex).Values("FormulaC"))

                'Como este cbox esta cargado por defecto al iniciar la web, solo le mandamos el codigo oculto del gridview
                ddlPerspectivaplan.SelectedValue = Convert.ToInt32(gvFormulas.DataKeys(seleccion.RowIndex).Values("codigo_ppla"))

                'Para mostrar el cbox del objetivo, primero cargamos la lista segun el codigo anterior, y apuntamos el codigo del gridview
                If ddlPerspectivaplan.SelectedValue <> 0 Then
                    CargarObjetivosSegunPlan(ddlPerspectivaplan.SelectedValue)
                    ddlObjetivos.SelectedValue = Convert.ToInt32(gvFormulas.DataKeys(seleccion.RowIndex).Values("codigo_obj"))
                End If

                'Para mostrar el cbox del indicadores, primero cargamos la lista segun el codigo anterior, y apuntamos el codigo del gridview
                If ddlObjetivos.SelectedValue <> 0 Then
                    CargarIndicadoresSegunObjetivo(ddlObjetivos.SelectedValue)
                    ddlIndicador.SelectedValue = Convert.ToInt32(gvFormulas.DataKeys(seleccion.RowIndex).Values("codigo_ind"))
                End If

                'Cambio nombre del boton Guardar por Modificar
                cmdGuardar.Text = "    Modificar"

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub cmdBuscarIndi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscarIndi.Click
        Try
            If Page.IsValid Then        'Si no ha ingresado cadenas inválidas (select, php, script)
                If txtBuscarIndicador.Text.Trim <> "" Then
                    ConsultarGridRegistros(txtBuscarIndicador.Text.Trim)
                Else
                    ConsultarGridRegistros("%")
                End If
            Else 'Limpia la cadena inválida de la caja de texto
                txtBuscar.Text = ""
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    'Para Validar Caja de Busqueda
    Protected Sub CustomValidator2_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator2.ServerValidate
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            dts = obj.ValidarPalabrasReservadas(args.Value.ToString.Trim)
            If dts.Rows(0).Item("Encontro") > 0 Then
                args.IsValid = False
            Else
                args.IsValid = True
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub



 
End Class
