
Partial Class planillaQuinta_frmRegistroPlanillaQuinta


    Inherits System.Web.UI.Page

    Protected Sub ImgBuscarCecos_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarCecos.Click
        BuscarCeCos()
    End Sub

    Private Sub BuscarCeCos()
        Dim objPre As ClsPresupuesto
        objPre = New ClsPresupuesto
        gvCecos.DataSource = objPre.ConsultaCentroCostosConPermisos(Request.QueryString("ctf"), Request.QueryString("id"), txtBuscaCecos.Text)
        gvCecos.DataBind()
        objPre = Nothing
        Panel3.Visible = True
    End Sub


    Protected Sub lnkBusquedaAvanzada_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzada.Click
        If lnkBusquedaAvanzada.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaCeCos(False)
            lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaCeCos(True)
            lnkBusquedaAvanzada.Text = "Busqueda Simple"
        End If
    End Sub

    Private Sub MostrarBusquedaCeCos(ByVal valor As Boolean)
        Me.txtBuscaCecos.Visible = valor
        Me.ImgBuscarCecos.Visible = valor
        Me.lblTextBusqueda.Visible = valor
        Me.cboCecos.Visible = Not (valor)
    End Sub

    Private Sub MostrarBusquedaPersonal(ByVal valor As Boolean)
        Me.txtBuscaPersonal.Visible = valor
        Me.ImgBuscarPersonal.Visible = valor
        Me.lblClicPersonal.Visible = valor
        Me.cboPersonal.Visible = Not (valor)
    End Sub

    Protected Sub gvCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCecos.SelectedIndexChanged
        cboCecos.SelectedValue = Me.gvCecos.DataKeys.Item(Me.gvCecos.SelectedIndex).Values(0)
        MostrarBusquedaCeCos(False)
        Panel3.Visible = False
        lnkBusquedaAvanzada.Text = "Busqueda Avanzada"
        cboCecos_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable


            'Cargar datos de Proceso presupuestal
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            'Cargar Datos Centro Costos
            datos = objpre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")
            MostrarBusquedaCeCos(False)

            'Cargar Datos Funcion
            datos = objPlla.ConsultarFuncionQuinta()
            objfun.CargarListas(cboFuncion, datos, "codigo_Fqta", "descripcion_Fqta", ">> Seleccione <<")

            'Cargar Datos Planilla
            datos = objPlla.ConsultarPlanilla(Request.QueryString("ctf"))
            objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")

            'Cargar Datos Personal
            datos = objPlla.ConsultarPersonal("TO")
            objfun.CargarListas(cboPersonal, datos, "codigo_Per", "nombres_per", ">> Seleccione <<")
            MostrarBusquedaPersonal(False)

            Me.txtImporte.Attributes.Add("onKeyPress", "validarnumero()")
            Panel3.Visible = False
            Panel2.Visible = False
            Me.hddCodigo_Adplla.Value = 0

	    if request.querystring("Tipo")="1" then
	        cmdEnviar.text ="Enviar para revisión"
	    else
		cmdEnviar.text ="Aprobar y Enviar a Personal"
	    end if


            If Request.QueryString("ctf") = 1 Then
                cmdEnviar.Visible = True

            Else
                If Request.QueryString("Tipo") = 1 Then
                    cmdEnviar.Visible = False
                Else
                    cmdEnviar.Visible = True
                End If
            End If
            

        End If
    End Sub

    Protected Sub lnkBusquedaAvanzadaPer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBusquedaAvanzadaPer.Click
        If lnkBusquedaAvanzadaPer.Text.Trim = "Busqueda Simple" Then
            MostrarBusquedaPersonal(False)
            lnkBusquedaAvanzadaPer.Text = "Busqueda Avanzada"
        Else
            MostrarBusquedaPersonal(True)
            lnkBusquedaAvanzadaPer.Text = "Busqueda Simple"
        End If
    End Sub

    Protected Sub ImgBuscarPersonal_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBuscarPersonal.Click
        BuscarPersonal()
    End Sub

    Private Sub BuscarPersonal()
        Dim objPlla As clsPlanilla
        objPlla = New clsPlanilla
        gvPersonal.DataSource = objPlla.ConsultarPersonal(Me.txtBuscaPersonal.Text) ' Busqueda Simple
        gvPersonal.DataBind()
        objPlla = Nothing
        Panel2.Visible = True
    End Sub

    Protected Sub gvPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPersonal.SelectedIndexChanged
        cboPersonal.SelectedValue = Me.gvPersonal.DataKeys.Item(Me.gvPersonal.SelectedIndex).Values(0)
        MostrarBusquedaPersonal(False)
        Panel2.Visible = False
        lnkBusquedaAvanzadaPer.Text = "Busqueda Avanzada"
    End Sub

    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        Me.cboCecos.SelectedValue = -1
        Me.cboPersonal.SelectedValue = -1
        Me.cboFuncion.SelectedValue = -1
        Me.txtBuscaCecos.Text = ""
        Me.txtBuscaPersonal.Text = ""
        Me.txtImporte.Text = ""
        Me.txtObservacion.Text = ""
        Me.hddSw.Value = 0
        Me.lblMsjGuardar.Text = ""
        Me.TextBox1.Text = ""

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objPlla As New clsPlanilla
        Dim codigo_Dplla As Int16
        Dim rpta As Int32
        Try
            codigo_Dplla = 0
            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta10", "alert('" & Me.TextBox1.Text & "');", True)

            rpta = objPlla.AgregarAnexoDetallePlanillaEjecutado(Me.hddCodigo_Adplla.Value, 1, Me.cboPlanilla.SelectedValue, cboPeriodoPresu.SelectedValue, cboPersonal.SelectedValue, cboCecos.SelectedValue, Me.txtImporte.Text, 19, 28, Me.cboFuncion.SelectedValue, Me.txtObservacion.Text, Request.QueryString("id"), "R", codigo_Dplla)
            If rpta = -1 Then
                'Page.RegisterStartupScript("fechaExpirada", "<script>alert('No se puede registrar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal');</script>")
                lblMsjGuardar.Text = "No se puede registrar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal"
            ElseIf rpta = -2 Then
                'ClientScript.RegisterStartupScript(gvCecos.GetType, "Respuesta2", "alert('No se puede registrar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal');", True)
                'Page.RegisterStartupScript("PlanillaEnviada", "<script>alert('No se puede registrar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal');</script>")
                lblMsjGuardar.Text = "No se puede registrar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
            ElseIf rpta = -3 Then
                'ClientScript.RegisterStartupScript(gvDetalleQuinta.GetType, "Alerta3", "alert('Ocurrió un error al guardar los datos');", True)
                'Page.RegisterStartupScript("Fallo", "<script>alert('Ocurrió un error al guardar los datos');</script>")
                lblMsjGuardar.Text = "Ocurrió un error al guardar los datos"
            Else
                'ClientScript.RegisterStartupScript(Me.GetType, "Correcto", "alert('Se guardaron correctamente los datos');", True)
                'Page.RegisterStartupScript("Correcto", "<script>alert('Se guardaron correctamente los datos');</script>")
                lblMsjGuardar.Text = "Se guardaron correctamente los datos"
            End If

            Me.TextBox1.Text = rpta
            Me.hddCodigo_Adplla.Value = 0
            gvDetalleQuinta.DataBind()
        Catch ex As Exception
            'ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('Ocurrió un error al guardar los datos');", True)
            Page.RegisterStartupScript("Error", "<script>alert('Ocurrió un error al guardar los datos');</script>")
        End Try
    End Sub



    Protected Sub gvDetalleQuinta_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleQuinta.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            If e.Row.RowIndex = 0 Then
                hddSw.Value = 0
                hddTotal.Value = 0
            End If
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvDetalleQuinta','Select$" & e.Row.RowIndex & "');")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")
            If fila.Item("Estado").ToString.Trim <> "Registrado" Then
                hddSw.Value = hddSw.Value + 1
            End If
            hddTotal.Value = hddTotal.Value + CDbl(e.Row.Cells(3).Text)
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.BackColor = Drawing.Color.LightGray
            e.Row.Cells(3).Text = FormatNumber(hddTotal.Value, 2)
            e.Row.Cells(3).Font.Bold = True
            e.Row.Cells(2).Text = "Total"
            e.Row.Cells(2).Font.Bold = True
        End If

    End Sub


    Protected Sub gvDetalleQuinta_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalleQuinta.RowDeleting
        Dim ObjPlla As New clsPlanilla
        Dim Rpta As Int32
        Rpta = ObjPlla.EliminarAnexoDetallePlanillaEjecutado(gvDetalleQuinta.DataKeys.Item(e.RowIndex).Values(0), request.querystring("id"))
        If Rpta = -1 Then
            lblMsjGuardar.Text = "No se puede eliminar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal"
        ElseIf Rpta = -2 Then
            lblMsjGuardar.Text = "No se puede eliminar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
        ElseIf Rpta = -3 Then
            lblMsjGuardar.Text = "Ocurrió un error al guardar los datos"
        Else
            lblMsjGuardar.Text = "Se eliminaron correctamente los datos"
        End If

        gvDetalleQuinta.DataBind()
        e.Cancel = True
    End Sub


    Protected Sub gvDetalleQuinta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalleQuinta.SelectedIndexChanged
        Me.lblMsjGuardar.Text = ""
        Me.hddCodigo_Adplla.Value = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(0)
        Me.TextBox1.Text = hddCodigo_Adplla.Value
        Me.cboPeriodoPresu.SelectedValue = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(1)
        Me.cboPlanilla.SelectedValue = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(2)
        Me.cboPersonal.SelectedValue = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(3)
        Me.cboCecos.SelectedValue = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(4)
        Me.cboFuncion.SelectedValue = gvDetalleQuinta.DataKeys.Item(gvDetalleQuinta.SelectedIndex).Values(5)
        Me.txtImporte.Text = gvDetalleQuinta.SelectedRow.Cells(3).Text
        Me.txtObservacion.Text = HttpUtility.HtmlDecode(gvDetalleQuinta.SelectedRow.Cells(4).Text)
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        Me.lblMsjGuardar.Text = ""
        'hddSw.Value = 0
        gvDetalleQuinta.DataBind()
        TextBox1.Text = hddSw.Value
        If Request.QueryString("ctf") = 1 Then
            gvDetalleQuinta.Columns(12).Visible = True
            gvDetalleQuinta.Columns(13).Visible = True
            Me.cmdEnviar.Enabled = True
        Else
            If hddSw.Value > 0 Then
                gvDetalleQuinta.Columns(12).Visible = False
                gvDetalleQuinta.Columns(13).Visible = False
                lblRespuesta.Text = "ESTADO: Planilla Enviada a Personal"
                Me.cmdEnviar.Enabled = False
            Else
                gvDetalleQuinta.Columns(12).Visible = True
                gvDetalleQuinta.Columns(13).Visible = True
                lblRespuesta.Text = ""
                Me.cmdEnviar.Enabled = True
            End If
        End If

        Me.lblMsjEnviado.Text = ""
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim ObjPlla As New clsPlanilla
        Dim Rpta As Int32
        Rpta = ObjPlla.EnviarDetallePlanillaQuinta(Me.cboPlanilla.SelectedValue, Me.cboCecos.SelectedValue, request.querystring("ctf"))

        cboCecos_SelectedIndexChanged(sender, e)
        If Rpta = -1 Then
            lblMsjEnviado.Text = "No se puede enviar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal"

        ElseIf Rpta = -2 Then
            lblMsjEnviado.Text = "No se puede enviar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
        ElseIf Rpta = -3 Then
            lblMsjEnviado.Text = "Ocurrió un error al procesar los datos"
        Else
            lblMsjEnviado.Text = "Enviado Correctamente"
        End If
        lblMsjEnviado.visible = True

        gvDetalleQuinta.DataBind()

        Me.cmdEnviar.Enabled = False

    End Sub
End Class
