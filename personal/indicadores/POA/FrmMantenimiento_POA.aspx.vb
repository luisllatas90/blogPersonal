
Partial Class indicadores_POA_PROTOTIPOS_Registrar_POA
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Carga_PEIS()
            CargaEjercicio()
            If Request.QueryString("cp") <> "" Then
                Dim codigo_poa As Integer
                Dim obj As New clsPlanOperativoAnual
                Dim dt As New Data.DataTable
                codigo_poa = Request.QueryString("cp")
                Me.hdcodigopoa.Value = codigo_poa
                dt = obj.ListaPoas(codigo_poa)
                Me.ddlEjercicio.SelectedValue = dt.Rows(0).Item("codigo_ejp")
                Me.ddlPlan.SelectedValue = dt.Rows(0).Item("codigo_pla")
                ddlPlan_SelectedIndexChanged(sender, e)
                Me.txtNombrePoa.Text = dt.Rows(0).Item("nombre_poa")
                Me.hdcodarea.Value = dt.Rows(0).Item("codigo_cco")
                Me.txtarea.Text = dt.Rows(0).Item("descripcion_cco")
                CargaResponsables()

                Me.ddlResponsable.SelectedValue = dt.Rows(0).Item("responsable_poa")
                If dt.Rows(0).Item("vigencia_poa") = 0 Then
                    Me.chkVigencia.Checked = False
                End If
            Else
                Dim codigo_ejp, codigo_pei As Integer
                codigo_pei = Request.QueryString("cb1")
                codigo_ejp = Request.QueryString("cb2")

                If codigo_ejp > 0 Then
                    Me.ddlEjercicio.SelectedValue = codigo_ejp
                End If

                If codigo_pei > 0 Then
                    Me.ddlPlan.SelectedValue = codigo_pei
                    ddlPlan_SelectedIndexChanged(sender, e)
                End If

            End If
        End If
    End Sub

    Sub Carga_PEIS()
        Dim obj As New clsPlanOperativoAnual
        Dim dtPEI As New Data.DataTable
        dtPEI = obj.ListaPeis
        Me.ddlPlan.DataSource = dtPEI
        Me.ddlPlan.DataTextField = "descripcion"
        Me.ddlPlan.DataValueField = "codigo"
        Me.ddlPlan.DataBind()
        dtPEI.Dispose()
        obj = Nothing
    End Sub

    Sub CargaEjercicio()
        Dim obj As New clsPlanOperativoAnual
        Dim dtEjercicioPresupuestal As New Data.DataTable
        dtEjercicioPresupuestal = obj.ListaEjercicio
        Me.ddlEjercicio.DataSource = dtEjercicioPresupuestal
        Me.ddlEjercicio.DataTextField = "descripcion"
        Me.ddlEjercicio.DataValueField = "codigo"
        Me.ddlEjercicio.DataBind()

        dtEjercicioPresupuestal.Dispose()
        obj = Nothing
    End Sub

    Protected Sub cmdGuardarPoa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarPoa.Click
        Try
            Dim dt As New Data.DataTable
            Dim obj As New clsPlanOperativoAnual
            Dim mensaje As String
            'lblMensajePers.Visible = True
            If Page.IsValid Then        'Si la pagina no tiene errores, ingresa.

                'Nuevo registro
                Dim vigencia As Integer
                If Me.chkVigencia.Checked = True Then
                    vigencia = 1
                Else
                    vigencia = 0
                End If

                If Me.hdcodigopoa.Value = "0" Then
                    mensaje = obj.AtualizarPoa(Me.ddlEjercicio.SelectedValue, Me.ddlPlan.SelectedValue, Me.hdcodarea.Value, Me.txtNombrePoa.Text.Trim.ToUpper.ToString, Me.ddlResponsable.SelectedValue, Request.QueryString("id"), vigencia, Me.hdcodigopoa.Value)
                    If mensaje = "1" Then
                        Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Request.QueryString("cb3") & "&msj=R")
                        'Me.lblmensaje.Text = "Datos Registrados Correctamente"
                        'Me.aviso.Attributes.Add("class", "mensajeExito")
                        'Limpiar()
                    ElseIf mensaje = "0" Then
                        Me.lblmensaje.Text = "No se pudo Registrar, Error al Registrar"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    ElseIf mensaje = "2" Then
                        Me.lblmensaje.Text = "No se Pudo Registrar, Existe un Plan Creado Para El Area y Ejercicio Seleccionados"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    ElseIf mensaje = "3" Then
                        Me.lblmensaje.Text = "No se Pudo Actualizar, Plan Operativo Cuenta con Centros de Costo Asignados"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    End If
                Else
                    mensaje = obj.AtualizarPoa(Me.ddlEjercicio.SelectedValue, Me.ddlPlan.SelectedValue, Me.hdcodarea.Value, Me.txtNombrePoa.Text.Trim.ToUpper.ToString, Me.ddlResponsable.SelectedValue, Request.QueryString("id"), vigencia, Me.hdcodigopoa.Value)
                    If mensaje = "1" Then
                        Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Me.ddlPlan.SelectedValue & "&cb2=" & Me.ddlEjercicio.SelectedValue & "&cb3=" & Request.QueryString("cb3") & "&msj=M")
                        'Me.lblmensaje.Text = "Datos Actualizados Correctamente"
                        'Me.aviso.Attributes.Add("class", "mensajeExito")
                        'Limpiar()
                    ElseIf mensaje = "0" Then
                        Me.lblmensaje.Text = "No se Pudo Modificar, Error al Modificar"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    ElseIf mensaje = "2" Then
                        Me.lblmensaje.Text = "No se Pudo Modificar, Existe un Plan Creado Para El Area y Ejercicio Seleccionados"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    ElseIf mensaje = "3" Then
                        Me.lblmensaje.Text = "No se Pudo Actualizar, Plan Operativo Cuenta con Centros de Costo Asignados"
                        Me.aviso.Attributes.Add("class", "mensajeError")
                    End If
                End If
                Me.treePrueba.CollapseAll()
            Else 'Limpia la cadena inválida de la caja de texto
                'txtDescripcionPers.Text = ""
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        'Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf"))
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        Response.Redirect("FrmListaPlanOperativoAnual.aspx?id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&msj=C")
    End Sub

    Protected Sub ddlPlan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPlan.SelectedIndexChanged
        CargaDireccion()
        Me.txtNombrePoa.Text = ""
        Me.txtarea.Text = ""
        Me.hdcodarea.Value = ""
        Me.ddlResponsable.Items.Clear()
        Me.treePrueba.Nodes.Clear()
        If Me.ddlDireccion.Items.Count > 0 Then
            Dim newNode As TreeNode = New TreeNode(Me.ddlDireccion.SelectedItem.ToString, Me.ddlDireccion.SelectedValue.ToString)
            newNode.PopulateOnDemand = True
            newNode.SelectAction = TreeNodeSelectAction.Expand
            treePrueba.Nodes.Add(newNode)
        End If
        Me.lblmensaje.Text = ""
    End Sub

    Sub CargaDireccion()
        Me.ddlDireccion.Items.Clear()
        Dim obj As New clsPlanOperativoAnual
        Dim dtDir As New Data.DataTable
        dtDir = obj.ConsultaDireccionxPlan(Me.ddlPlan.SelectedValue)
        If dtDir.Rows.Count > 0 Then
            Me.ddlDireccion.DataSource = dtDir
            Me.ddlDireccion.DataTextField = "descripcion"
            Me.ddlDireccion.DataValueField = "codigo"
            Me.ddlDireccion.DataBind()
        End If
        dtDir.Dispose()
        obj = Nothing
    End Sub

    Sub CargaResponsables()
        Me.ddlResponsable.Items.Clear()
        If Me.hdcodarea.Value <> "" Then
            Dim obj As New clsPlanOperativoAnual
            Dim dt As New Data.DataTable
            dt = obj.ConsultaResponsablexArea(Me.hdcodarea.Value)
            If dt.Rows.Count > 0 Then
                Me.ddlResponsable.DataSource = dt
                Me.ddlResponsable.DataTextField = "descripcion"
                Me.ddlResponsable.DataValueField = "codigo"
                Me.ddlResponsable.DataBind()
            End If
            dt.Dispose()
            obj = Nothing
        End If
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
            'Dim Text As String = treePrueba.SelectedNode.Text
            Me.hdcodarea.Value = treePrueba.SelectedNode.Value.ToString.Trim
            Me.txtarea.Text = treePrueba.SelectedNode.Text.ToString.Trim
            Me.treePrueba.CollapseAll()
            CargaResponsables()
            If Me.ddlEjercicio.SelectedValue <> 0 Then
                Me.txtNombrePoa.Text = "POA - " + Me.txtarea.Text + " - " + Me.ddlEjercicio.SelectedItem.ToString
            Else
                Me.txtNombrePoa.Text = "POA - " + Me.txtarea.Text
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        Me.lblmensaje.Text = ""
    End Sub

    Sub Limpiar()
        Me.ddlEjercicio.SelectedValue = 0
        Me.ddlPlan.SelectedValue = 0
        Me.ddlDireccion.Items.Clear()
        Me.ddlResponsable.Items.Clear()
        Me.txtNombrePoa.Text = ""
        Me.treePrueba.Nodes.Clear()
        Me.hdcodigopoa.Value = 0
        Me.hdcodarea.Value = ""
        Me.txtarea.Text = ""
    End Sub

    Protected Sub ddlEjercicio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEjercicio.SelectedIndexChanged
        Me.lblmensaje.Text = ""
        Me.aviso.Attributes.Clear()

    End Sub

    Protected Sub ddlResponsable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlResponsable.SelectedIndexChanged
        Me.lblmensaje.Text = ""
        Me.aviso.Attributes.Clear()
    End Sub
End Class
