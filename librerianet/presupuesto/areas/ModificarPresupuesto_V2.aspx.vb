
Partial Class presupuesto_areas_ModificarPresupuesto_V2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            'Cargar datos de Proceso presupuestal

            If Request.QueryString("ctf") = 1 Then  'Si es administrador puede ver todos los procesos
                datos = objPre.ConsultarProcesoContable()
            Else
                datos = objpre.ObtenerListaProcesos() 'Solo ve el proceso activo actualmente
                cboPeriodoPresu.Enabled = False
            End If



            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            'Cargar Datos Centro Costos = Area presupuestal
            Session("datosCecos") = objpre.ObtenerListaCentroCostosNuevoPresupuesto("1", 523, Request.QueryString("id"))
            objfun.CargarListas(cboAreaPresu, Session("datosCecos"), "codigo_Cco", "descripcion_Cco")
            Dim keys(1) As Data.DataColumn
            keys(0) = Session("datosCecos").Columns("codigo_Cco")
            'Session("datosCecos").PrimaryKey = keys
            ' Dim fila As Data.DataRow = Session("datosCecos").Rows.Find(cboAreaPresu.SelectedValue)
            datos.Dispose()

            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden'", True)

            VerificarValoresDeRetorno()
        End If
    End Sub

    Private Sub VerificarValoresDeRetorno()
        If Request.QueryString("cecos") <> "" Then
            cboAreaPresu.SelectedValue = Request.QueryString("cecos")
        End If
        If Request.QueryString("ppr") <> "" Then
            hddCodigo_ppr.Value = Request.QueryString("ppr")
        End If
        If Request.QueryString("pto") <> "" Then
            hddCodigo_pto.Value = Request.QueryString("pto")
        End If
        If Request.QueryString("habilitado") <> "" Then
            hddHabilitado.Value = Request.QueryString("habilitado")
        End If
        ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible'", True)
    End Sub

    Protected Sub gvCabecera_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabecera.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")

            e.Row.Style.Add("cursor", "hand")
        End If
    End Sub

    Protected Sub gvCabecera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabecera.SelectedIndexChanged
        hddCodigo_pto.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        hddCodigo_ppr.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        hddHabilitado.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(2)
        ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible'", True)
        gvDetalle.DataBind()
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ctrlEliminar As ImageButton = CType(e.Row.Cells(28).Controls(0), ImageButton)
            Dim ctrlEditar As ImageButton = CType(e.Row.Cells(27).Controls(0), ImageButton)
            Dim ctrl As ImageButton = CType(e.Row.Cells(29).Controls(1), ImageButton)
            e.Row.Cells(27).Enabled = hddHabilitado.Value
            e.Row.Cells(28).Enabled = hddHabilitado.Value
            e.Row.Cells(29).Enabled = hddHabilitado.Value

            If hddHabilitado.Value = True Then
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar.gif"
                e.Row.Cells(27).Visible = True
                e.Row.Cells(28).Visible = False
                e.Row.Cells(29).Visible = True
            Else
                ctrlEditar.ImageUrl = "../../images/presupuesto/editar_d.gif"
                ctrlEliminar.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                ctrl.ImageUrl = "../../images/presupuesto/eliminar_d.gif"
                e.Row.Cells(27).Visible = True
                e.Row.Cells(28).Visible = True
                e.Row.Cells(29).Visible = False
            End If
        End If
    End Sub

    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting
        EliminarItem(gvDetalle.DataKeys.Item(e.RowIndex).Value, True, sender, e)
        gvDetalle.DataBind()
        e.Cancel = True
    End Sub


    Private Sub EliminarItem(ByRef codigo_dpr As Int64, ByVal mostrarmsj As Boolean, ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim objPre As ClsPresupuesto
        Dim rpta As String
        Try
            objPre = New ClsPresupuesto
            objPre.AbrirTransaccionCnx()
            rpta = objPre.EliminarDetallePresupuesto(codigo_dpr, Request.QueryString("id"))
            objPre.CerrarTransaccionCnx()
            If mostrarmsj = True Then
                If rpta = "0" Then
                    ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo eliminar, debido a que el proceso no está habilitado');", True)
                ElseIf rpta = "1" Then

                    ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('Se eliminaron correctamente los datos');", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo eliminar porque ocurrió un error al procesar los datos');", True)
                End If
            End If
            objPre = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        End Try
    End Sub

    Protected Sub gvDetalle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetalle.RowEditing
        If Request.QueryString("ctf") = 1 Then
            Response.Redirect("RegistrarPresupuestoDetalleAdmin.aspx?field=" & gvDetalle.DataKeys.Item(e.NewEditIndex).Value & "&tipo=E&id=" & Request.QueryString("id") & "&cecos=" & cboAreaPresu.SelectedValue & "&ppr=" & hddCodigo_ppr.Value & "&pto=" & hddCodigo_pto.Value & "&habilitado=" & hddHabilitado.Value & "&ctf=" & Request.QueryString("ctf"))
        Else
            Response.Redirect("RegistrarPresupuestoDetalle.aspx?field=" & gvDetalle.DataKeys.Item(e.NewEditIndex).Value & "&tipo=E&id=" & Request.QueryString("id") & "&cecos=" & cboAreaPresu.SelectedValue & "&ppr=" & hddCodigo_ppr.Value & "&pto=" & hddCodigo_pto.Value & "&habilitado=" & hddHabilitado.Value & "&ctf=" & Request.QueryString("ctf"))
        End If

        e.Cancel = False
    End Sub


    Protected Sub cboAreaPresu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAreaPresu.SelectedIndexChanged
        gvCabecera.DataBind()
    End Sub

    Protected Sub gvDetalle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalle.SelectedIndexChanged

    End Sub

    Protected Sub cboPeriodoPresu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodoPresu.SelectedIndexChanged

    End Sub
End Class
