
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            HdCodigo_poa.Value = Request.QueryString("codigo_poa")

            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable
            dtt = obj.POA_ConsultarNombrePOA(Request.QueryString("codigo_poa"))
            txt_nombre_poa.Text = dtt.Rows(0).Item(0).ToString

            txt_montoIngreso.Text = FormatNumber(Request.QueryString("limite_ingreso"), 2)
            txt_montoEgreso.Text = FormatNumber(Request.QueryString("limite_egreso"), 2)
            txt_utilidad.Text = FormatNumber(Request.QueryString("utilidad"), 2)

            Dim newNode As TreeNode = New TreeNode(Request.QueryString("Nombre_cco"), Request.QueryString("codigo_cco"))
            newNode.PopulateOnDemand = True
            newNode.SelectAction = TreeNodeSelectAction.Expand
            treePrueba.Nodes.Add(newNode)

            'Call wf_limpiarGridView()
            Call wf_cargarData(HdCodigo_poa.Value)
            Call wf_ListaTipoActividad()

        End If
    End Sub

    Sub wf_cargarData(ByVal as_codigoPOA As Integer)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable

            dgv_ceco.DataSource = Nothing
            dgv_ceco.DataSource = obj.ListarCeCoAsignadoLimitePresupuestal(as_codigoPOA)
            dgv_ceco.DataBind()
            obj = Nothing


            'dtt = obj.ListarCeCoAsignadoLimitePresupuestal(as_codigoPOA)
            'For i As Integer = 0 To dtt.Rows.Count - 1
            '    '    Dim objCeco As New ClsCentroCosto
            '    '    objCeco.AgregarItemDetalle(dtt.Rows(i).Item("codigo_cco").ToString, dtt.Rows(i).Item("descripcion_cco").ToString, dtt.Rows(i).Item("codigo_tac").ToString, dtt.Rows(i).Item("descripcion_tac").ToString, dtt.Rows(i).Item("codigo_asp").ToString)
            'Next
            ''Call wf_CargarDetalle()
            'obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub wf_ListaTipoActividad()
        Dim obj As New clsPlanOperativoAnual
        Dim dtTipoActividad As New Data.DataTable
        dtTipoActividad = obj.POA_lista_tipoActividad
        Me.ddl_tipoActividad.DataSource = dtTipoActividad
        Me.ddl_tipoActividad.DataTextField = "descripcion"
        Me.ddl_tipoActividad.DataValueField = "codigo"
        Me.ddl_tipoActividad.DataBind()
        dtTipoActividad.Dispose()
        obj = Nothing
    End Sub

    Function wf_validate() As Boolean
        txt_utilidad.Text = txt_montoIngreso.Text - txt_montoEgreso.Text

        If Me.txt_montoIngreso.Text = "" Then
            Me.txt_montoIngreso.Text = "0.00"
        End If

        If Me.txt_montoEgreso.Text = "" Then
            Me.txt_montoEgreso.Text = "0.00"
        End If

        ''Valida que importes no sean Mayores a Limite de Egreso
        Dim obj As New clsPlanOperativoAnual
        Dim dtt As New Data.DataTable
        Dim li_codigo_poa As Integer = HdCodigo_poa.Value

        dtt = obj.POA_ConsultaImportesIngresoEgreso(li_codigo_poa)
        If Me.txt_montoEgreso.Text < dtt.Rows(0).Item("egresos") Then
            Me.lblrpta.Text = "El Monto de Egreso: " & FormatNumber(Me.txt_montoEgreso.Text, 2) & " es menor al Monto asignado en los [Programas/Proyectos]: " & FormatNumber(dtt.Rows(0).Item("egresos").ToString, 2)
            Me.aviso.Attributes.Add("class", "mensajeError")
            Return False
        End If

        Return True
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            If wf_validate() = False Then
                Return
            End If

            ' ''Actualizar Datos
            Dim obj As New clsPlanOperativoAnual
            Dim li_codigo_poa As Integer = HdCodigo_poa.Value

            'obj.POA_Elimina_limitePresupuestal(li_codigo_poa, 0, Request.QueryString("id"), "0")
            'For i As Integer = 0 To dgv_ceco.Rows.Count - 1
            '    Dim li_codigo_asp As Integer = Me.dgv_ceco.DataKeys.Item(i).Values(2).ToString
            '    obj.POA_Elimina_limitePresupuestal(li_codigo_poa, li_codigo_asp, Request.QueryString("id"), "1")
            'Next

            'For i As Integer = 0 To dgv_ceco.Rows.Count - 1
            '    Dim li_codigo_cco As Integer = Me.dgv_ceco.DataKeys.Item(i).Values(0).ToString
            '    Dim li_codigo_tac As Integer = Me.dgv_ceco.DataKeys.Item(i).Values(1).ToString
            '    Dim li_codigo_asp As Integer = Me.dgv_ceco.DataKeys.Item(i).Values(2).ToString

            '    obj.ActualizarLimitePresupuestal(li_codigo_asp, li_codigo_poa, li_codigo_cco, li_codigo_tac, Request.QueryString("id"))
            'Next
            obj.ActualizarMetaPOA(li_codigo_poa, txt_montoIngreso.Text, txt_montoEgreso.Text, txt_utilidad.Text)

            Response.Redirect("FrmAsignaLimitePresupuestal.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & _
                            "&estado=" & Request.QueryString("estado") & "&UpDate=" & "1" & "&plan=" & Request.QueryString("plan") & "&ejercicio=" & Request.QueryString("ejercicio"))

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmAsignaLimitePresupuestal.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&estado=" & Request.QueryString("estado") & _
                          "&UpDate=" & "0" & "&plan=" & Request.QueryString("plan") & "&ejercicio=" & Request.QueryString("ejercicio"))

    End Sub

    Protected Sub btn_Agregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Agregar.Click
        Try
            'Dim objCeco As New ClsCentroCosto
            If ddl_tipoActividad.SelectedValue.ToString = 0 Then
                Me.lblrpta.Text = "Debe Seleccionar El tipo de Actividad (Programa/Proyecto)"
                Me.aviso.Attributes.Add("class", "mensajeError")
                Return
            End If
            'If objCeco.f_verificarCodigo(txt_codigo_cco.Text) = True Then
            '    Me.lblrpta.Text = "Se adicionó el Centro de Costo: " & txt_nombre_cco.Text.Trim
            '    Me.aviso.Attributes.Add("class", "mensajeExito")
            '    objCeco.AgregarItemDetalle(txt_codigo_cco.Text, txt_nombre_cco.Text, ddl_tipoActividad.SelectedValue, ddl_tipoActividad.SelectedItem.ToString, 0)
            'End If
            'Call wf_CargarDetalle()


            ''objCeco.f_verificarCodigo(txt_codigo_cco.Text)
            Dim obj As New clsPlanOperativoAnual
            Dim li_codigo_asp As Integer = 0
            Dim li_codigo_poa As Integer = HdCodigo_poa.Value
            Dim li_codigo_cco As Integer = txt_codigo_cco.Text
            Dim li_codigo_tac As Integer = ddl_tipoActividad.SelectedValue

            If obj.POA_VerificarLimitePresupuestalEliminado(li_codigo_poa, li_codigo_cco) = 0 Then
                obj.ActualizarLimitePresupuestal(li_codigo_asp, li_codigo_poa, li_codigo_cco, li_codigo_tac, Request.QueryString("id"))
                Me.lblrpta.Text = "Se adicionó el Centro de Costo: " & txt_nombre_cco.Text.Trim
                Me.aviso.Attributes.Add("class", "mensajeExito")
            Else
                Me.lblrpta.Text = "El Centro de Costo: " & txt_nombre_cco.Text.Trim & " Ya se encuentar asignado en la lista"
                Me.aviso.Attributes.Add("class", "mensajeEliminado")
            End If

            Call wf_cargarData(HdCodigo_poa.Value)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub wf_CargarDetalle()
        'dgv_ceco.DataSource = Nothing
        'Dim objCeco As New ClsCentroCosto
        'dgv_ceco.DataSource = objCeco.ConsultarDetalle()
        'dgv_ceco.DataBind()

        'objCeco = Nothing
    End Sub

    Sub wf_limpiarGridView()
        'Dim objCeco As New ClsCentroCosto
        'objCeco.wf_limpiarGridView()
    End Sub

    Protected Sub gv_ceco_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgv_ceco.RowDeleting
        Try
            '    Dim obj As New ClsCentroCosto
            '    '' Verificar que no se encuentre aignado un Programa o Proyecto para este Centro de Costo
            '    Dim objCls As New clsPlanOperativoAnual
            '    Dim dtt As New Data.DataTable
            '    dtt = objCls.POA_verificar_CECO_ActividadPoa(HdCodigo_poa.Value, Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(0))
            '    If dtt.Rows(0).Item("dato").ToString > 0 Then
            '        Me.lblrpta.Text = "El Centro de Costo de encuentra Asignado en las Actividades (Programas/Proyectos)"
            '        Me.aviso.Attributes.Add("class", "mensajeError")
            '    Else
            '        obj.wf_EliminarItem(Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(0))
            '        Me.lblrpta.Text = "El Centro de Costo de Elimino Correctamente"
            '        Me.aviso.Attributes.Add("class", "mensajeEliminado")
            '    End If
            '    Call wf_CargarDetalle()
            '    Dim obj As New ClsCentroCosto
            '    '' Verificar que no se encuentre aignado un Programa o Proyecto para este Centro de Costo

            Dim obj As New clsPlanOperativoAnual
            Dim dtt As New Data.DataTable
            dtt = obj.POA_verificar_CECO_ActividadPoa(HdCodigo_poa.Value, Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(0))
            If dtt.Rows(0).Item("dato").ToString > 0 Then
                Me.lblrpta.Text = "El Centro de Costo de encuentra Asignado en las Actividades (Programas/Proyectos)"
                Me.aviso.Attributes.Add("class", "mensajeError")
            Else
                Dim li_codigo_asp As Integer = Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(2).ToString
                Dim li_codigo_poa As Integer = HdCodigo_poa.Value
                Dim li_codigo_cco As Integer = Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(0).ToString
                Dim li_codigo_tac As Integer = Me.dgv_ceco.DataKeys.Item(e.RowIndex()).Values(1).ToString

                obj.ActualizarLimitePresupuestal(li_codigo_asp, li_codigo_poa, li_codigo_cco, li_codigo_tac, Request.QueryString("id"))
                Me.lblrpta.Text = "El Centro de Costo de Elimino Correctamente"
                Me.aviso.Attributes.Add("class", "mensajeEliminado")
            End If

            Call wf_cargarData(HdCodigo_poa.Value)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub treePrueba_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles treePrueba.TreeNodePopulate
        Try
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        CentroCostosPrimerNivel(e.Node)
                    Case Is > 0
                        CentroCostosNivelSuperior(e.Node)
                End Select
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Sub CentroCostosPrimerNivel(ByVal node As TreeNode)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dts As New Data.DataTable

            'Cargamos la data
            dts = obj.ArbolCentroCostos(node.Value)
            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion_cco").ToString, row("Codigo_cco").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub CentroCostosNivelSuperior(ByVal node As TreeNode)
        Try
            Dim obj As New clsPlanOperativoAnual
            Dim dts As New Data.DataTable

            'Cargamos la data de las categorias
            dts = obj.ArbolCentroCostos(node.Value)

            If dts.Rows.Count > 0 Then
                Dim row As Data.DataRow
                For Each row In dts.Rows
                    Dim NewNode As TreeNode = New TreeNode(row("Descripcion_cco").ToString, row("Codigo_cco").ToString)
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub treePrueba_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles treePrueba.SelectedNodeChanged
        Try

            txt_codigo_cco.Text = treePrueba.SelectedNode.Value.ToString.Trim
            Me.txt_nombre_cco.Text = treePrueba.SelectedNode.Text.ToString.Trim
            'Me.treePrueba.CollapseAll()
            Me.treePrueba.SelectedNode.Selected = False

            btn_Agregar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub txt_montoIngreso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_montoIngreso.TextChanged
        Try
            If IsNumeric(Me.txt_montoIngreso.Text) Then
                If Me.txt_montoIngreso.Text.Contains(".") Then
                Else
                    Me.txt_montoIngreso.Text = Me.txt_montoIngreso.Text & ".00"

                End If
                Me.txt_montoIngreso.Text = FormatNumber(Me.txt_montoIngreso.Text.ToString, 2)

                Me.txt_utilidad.Text = Convert.ToDecimal(Me.txt_montoIngreso.Text) - Convert.ToDecimal(Me.txt_montoEgreso.Text)
                Me.txt_utilidad.Text = FormatNumber(Me.txt_utilidad.Text, 2)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub txt_montoEgreso_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_montoEgreso.TextChanged
        Try
            If IsNumeric(Me.txt_montoEgreso.Text) Then
                If Me.txt_montoEgreso.Text.Contains(".") Then
                Else
                    Me.txt_montoEgreso.Text = Me.txt_montoEgreso.Text & ".00"

                End If
                Me.txt_montoEgreso.Text = FormatNumber(Me.txt_montoEgreso.Text.ToString, 2)

                Me.txt_utilidad.Text = Convert.ToDecimal(Me.txt_montoIngreso.Text) - Convert.ToDecimal(Me.txt_montoEgreso.Text)
                Me.txt_utilidad.Text = FormatNumber(Me.txt_utilidad.Text, 2)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
