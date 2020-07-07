﻿
Partial Class planillaQuinta_frmRegistroPlanillaQuinta_v2


    Inherits System.Web.UI.Page

    Private Sub MostrarBusquedaPersonal(ByVal valor As Boolean)
        Me.txtBuscaPersonal.Visible = valor
        Me.ImgBuscarPersonal.Visible = valor
        Me.lblClicPersonal.Visible = valor
        Me.cboPersonal.Visible = Not (valor)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            '*** Cargar datos de Proceso presupuestal ***
            'datos = objpre.ObtenerListaProcesos()
            datos = objpre.ConsultarProcesoContable()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            '*** Cargar Datos Centro Costos ***
            datos = objPlla.ConsultarCentroCostosQuinta(Session("id_per"))
            objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Seleccione <<")


            '*** Cargar Datos Funcion ***
            datos = objPlla.ConsultarFuncionQuinta(Session("id_per"), Request.QueryString("ctf"))
            objfun.CargarListas(cboFuncion, datos, "codigo_Fqta", "descripcion_Fqta", ">> Seleccione <<")

            '*** Cargar Datos Planilla *** 
            datos = objPlla.ConsultarPlanilla(Request.QueryString("ctf"))
            objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")

            '**** Cargar Datos Personal **** 
            datos = objPlla.ConsultarPersonal("TO")
            objfun.CargarListas(cboPersonal, datos, "codigo_Per", "nombres_per", ">> Seleccione <<")
            MostrarBusquedaPersonal(False)

            Me.txtImporte.Attributes.Add("onKeyPress", "validarnumero()")
            Panel2.Visible = False
            Me.hddCodigo_Adplla.Value = 0

            If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 84 Then ' Administrador o responsable de registro (coordinador administrativo)
                cmdEnviar.Visible = True
            Else
                cmdEnviar.Visible = False
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
            If Session("id_per") = "" Then
                Response.Redirect("../../../sinacceso.html")
            End If
            codigo_Dplla = 0
            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta10", "alert('" & Me.TextBox1.Text & "');", True)

            rpta = objPlla.AgregarAnexoDetallePlanillaEjecutado(Me.hddCodigo_Adplla.Value, 1, Me.cboPlanilla.SelectedValue, cboPeriodoPresu.SelectedValue, cboPersonal.SelectedValue, cboCecos.SelectedValue, Me.txtImporte.Text, 19, 28, Me.cboFuncion.SelectedValue, Me.txtObservacion.Text, Session("id_per"), "B", codigo_Dplla)
            If rpta = -1 Then
                lblMsjGuardar.Text = "No se puede registrar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal"
            ElseIf rpta = -2 Then
                lblMsjGuardar.Text = "No se puede registrar porque la planilla para este centro de costos ya fue enviada a Revisión o Dirección de Personal"
            ElseIf rpta = -3 Then
                lblMsjGuardar.Text = "Ocurrió un error al guardar los datos"
            Else
                lblMsjGuardar.Text = "Se guardaron correctamente los datos"
            End If

            Me.TextBox1.Text = rpta
            Me.hddCodigo_Adplla.Value = 0
            gvDetalleQuinta.DataBind()
        Catch ex As Exception
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
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Style.Add("cursor", "hand")

            '### verifica si hay algún estado diferente de borrador, esto para bloquear la opción de envío ###
            If fila.Item("Estado").ToString.Trim <> "Borrador" Then
                hddSw.Value = hddSw.Value + 1
            End If
            '### Suma el total de los importes ### 
            hddTotal.Value = hddTotal.Value + CDbl(e.Row.Cells(3).Text)


            e.Row.Cells(13).Visible = False
            e.Row.Cells(14).Visible = False
            If Request.QueryString("ctf") = 1 Then
                e.Row.Cells(13).Visible = True
                e.Row.Cells(14).Visible = True
            Else
                If Session("id_per") = gvDetalleQuinta.DataKeys.Item(e.Row.RowIndex).Values(6) Then
                    If fila.Item("Estado").ToString.Trim = "Borrador" Then
                        e.Row.Cells(13).Visible = True
                        e.Row.Cells(14).Visible = True
                    End If
                Else
                    e.Row.BackColor = Drawing.Color.Yellow
                End If
            End If
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
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        Dim ObjPlla As New clsPlanilla
        Dim Rpta As Int32
        Rpta = ObjPlla.EliminarAnexoDetallePlanillaEjecutado(gvDetalleQuinta.DataKeys.Item(e.RowIndex).Values(0), Session("id_per"))
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
        Me.lblRespuesta.Text = ""
        gvDetalleQuinta.DataBind()
        TextBox1.Text = hddSw.Value
        If Request.QueryString("ctf") = 1 Then
            gvDetalleQuinta.Columns(13).Visible = True
            gvDetalleQuinta.Columns(14).Visible = True
            Me.cmdEnviar.Enabled = True
        Else
            If hddSw.Value > 0 Then
                gvDetalleQuinta.Columns(13).Visible = False
                gvDetalleQuinta.Columns(14).Visible = False
                lblRespuesta.Text = "ESTADO: Planilla enviada para revisión"
                Me.cmdEnviar.Enabled = False
            Else
                gvDetalleQuinta.Columns(13).Visible = True
                gvDetalleQuinta.Columns(14).Visible = True
                lblRespuesta.Text = ""
                Me.cmdEnviar.Enabled = True
            End If
        End If

        Me.lblMsjEnviado.Text = ""
    End Sub

    Protected Sub cmdEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEnviar.Click
        Dim ObjPlla As New clsPlanilla
        Dim Rpta As Int32
        Dim Mensaje As String
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Me.gvDetalleQuinta.Rows.Count > 0 And cboCecos.SelectedValue > 0 And cboPlanilla.SelectedValue > 0 Then
            Rpta = ObjPlla.EnviarDetallePlanillaQuintaARevisor(Me.cboPlanilla.SelectedValue, Me.cboCecos.SelectedValue, Session("id_per"))
            cboCecos_SelectedIndexChanged(sender, e)
            If Rpta = -1 Then
                lblMsjEnviado.Text = "No se puede enviar porque ha expirado la fecha límite para registrar las quintas especiales indicada por Dirección de Personal"
            ElseIf Rpta = -2 Then
                lblMsjEnviado.Text = "No se puede enviar porque la planilla para este centro de costos ya fue enviada a Dirección de Personal"
            ElseIf Rpta = -3 Then
                lblMsjEnviado.Text = "Ocurrió un error al procesar los datos"
            Else
                lblMsjEnviado.Text = "Enviado Correctamente"
                gvDetalleQuinta.DataBind()
                Mensaje = "Se ha registrado la planilla de quinta especial del <b>" & cboCecos.SelectedItem.Text & "</b> sirvase evaluar la información para ser procesada.<br>"
                ObjPlla.EnviarCorreo(Me.cboCecos.SelectedValue, 1, Mensaje)
            End If
            lblMsjEnviado.Visible = True
            Me.cmdEnviar.Enabled = False
        Else
            Me.lblRespuesta.Text = "No puede enviar una planilla de quinta sin registros, seleccione un centro de costos"
            Me.lblRespuesta.ForeColor = Drawing.Color.Red
        End If
    End Sub


    Protected Sub imgRefrescar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRefrescar.Click
        cboCecos_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub cboPlanilla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanilla.SelectedIndexChanged

    End Sub
End Class
