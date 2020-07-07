﻿'treyes
Partial Class presupuesto_areas_ModificarPresupuesto_V3
    Inherits System.Web.UI.Page
    Dim TotalIngresos As Decimal
    Dim TotalEgresos As Decimal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If Not IsPostBack Then

            lblTitulo.Text = "Presupuesto: " & Session("actividadPto")
            lblCeco.Text = ": " & Session("cecoPto")
            lblInstancia.Text = ": " & Session("instanciaPto")
            lblEstado.Text = ": " & Session("estadoPto")
            hddCodigo_pto.Value = Session("codigoPto")

            ObtenerObservaciones(hddCodigo_pto.value)
            If hddCantidadObs.Value = 0 Then
                pnlObservaciones.Visible = False
            End If

            If Request.QueryString("op") = "regPto" Then
                hddCodigo_dap.Value = Session("dapPto")
                hddCodigo_ejp.Value = Session("ejpPto")
                hddHabilitado_pto.Value = Session("habilitadoPto")

                gvCabecera.DataBind()
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='visible'", True)
                gvDetalle.DataBind()
                pnlObservar.Visible = False
                btnObservar.Visible = False
                btnAProbar.Visible = False
            End If

            If Request.QueryString("op") = "gesPto" Then
                gvCabecera.DataBind()
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden'", True)
                pnlObservar.Visible = False
                btnObservar.Visible = False
                btnAProbar.Visible = False
            End If

            If Request.QueryString("op") = "evaPOA" Then
                'Hcano
                Session("Ident_EvaPoa") = "evaPoa"
                Session("cb1_evapto") = Request.QueryString("cb1")
                Session("cb2_evapto") = Request.QueryString("cb2")
                Session("cb3_evapto") = Request.QueryString("cb3")
                Session("cb4_evapto") = Request.QueryString("cb4")
                'Hcano
                gvCabecera.DataBind()
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden'", True)
                pnlObservar.Visible = False
                Me.cmdVisibilidad.Visible = False
            End If
        End If
    End Sub
    Private Sub ObtenerObservaciones(ByVal codigo_pto As Integer)
        Dim objPre = New ClsPresupuesto
        Dim datos As New Data.DataTable
        Dim datosU As New Data.DataTable

        datos = objPre.ObtenerObservaciones(codigo_pto)
        blObservaciones.DataSource = datos
        blObservaciones.DataBind()
        hddCantidadObs.Value = datos.Rows.Count

        If datos.Rows.Count > 0 Then
            For i As int16 = 0 To datos.Rows.Count - 1
                If datos.Rows(i).Item("resuelto_obs") = 1 Then
                    blObservaciones.Items(i).Attributes("Style") = "Color:Green"
                End If
            Next

            If Request.QueryString("op") = "evaPOA" Then

                datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))
                If datos.Rows(datos.Rows.Count - 1).Item("resuelto_obs") = 1 Then
                    If datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "RESPONSABLE - OBS. DIR. AREA" And (datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS") Then
                        btnObservar.Enabled = 1
                        btnAprobar.Enabled = 1
                    Else
                        btnObservar.Enabled = 0
                        btnAprobar.Enabled = 0
                    End If
                Else
                    If (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "RESPONSABLE - OBS. DIR. AREA" And datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA") Or (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "DIR. AREA - OBS. FINAL" And datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS") Then
                        btnObservar.Enabled = 1
                        btnAprobar.Enabled = 1
                    Else
                        btnObservar.Enabled = 0
                        btnAprobar.Enabled = 0
                    End If
                End If
            End If
        End If

    End Sub
    Protected Sub gvCabecera_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCabecera.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            
            If Request.QueryString("op") = "regPto" Then
                If gvCabecera.DataKeys(e.Row.RowIndex).Values(0) = hddCodigo_dap.Value Then
                    e.Row.BackColor = System.Drawing.Color.FromName("#D8E5FF")
                End If
            End If
            TotalIngresos += CDec(e.Row.Cells(1).Text)
            TotalEgresos += CDec(e.Row.Cells(2).Text)
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Totales"
            e.Row.Cells(1).Text = String.Format("{0:N2}", TotalIngresos)
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).Text = String.Format("{0:N2}", TotalEgresos)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
        End If

    End Sub
    Protected Sub gvCabecera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabecera.SelectedIndexChanged
        Dim estado As String
        Dim instancia As String

        hddCodigo_dap.Value = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        hddCodigo_ejp.Value = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        hddCodigo_iep.Value = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(2)
        hddHabilitado_pto.Value = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(3)

        Session("dapPto") = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        Session("ejpPto") = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        Session("habilitadoPto") = Me.gvCabecera.Datakeys.Item(Me.gvCabecera.SelectedIndex).Values(3)

        If Request.QueryString("op") = "evaPOA" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
        Else
            If (Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
            (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
            (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0) Then

                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='visible'", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
            End If
        End If
        ObtenerObservaciones(hddCodigo_pto.value)
        gvDetalle.Visible = True
        gvDetalle.DataBind()
        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 156 Then
            Me.cmdVisibilidad.Visible = True
        Else
            Me.cmdVisibilidad.Visible = False
        End If

    End Sub
    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound
        Dim estado As String
        Dim instancia As String
        Dim objPre As New ClsPresupuesto
        Dim datos As New Data.DataTable

        If e.Row.RowType = DataControlRowType.DataRow Then
            datos = objPre.ObtenerMesesActividadPOA(hddCodigo_dap.Value)

            For i As Int16 = 9 To 20
                If i < datos.Rows(0).Item("MesIni") + 8 Or i > datos.Rows(0).Item("MesFin") + 8 Then
                    e.Row.Cells(i).Text = "-"
                End If
            Next

            e.Row.Cells(8).Font.Bold = True
            If CDec(e.Row.Cells(9).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(9).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(10).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(10).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(11).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(11).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(12).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(12).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(13).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(13).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(14).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(14).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(15).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(15).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(16).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(16).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(17).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(17).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(18).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(18).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(19).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(19).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If
            If CDec(e.Row.Cells(20).Text.Replace("-", "0")) > 0 Then
                e.Row.Cells(20).BackColor = System.Drawing.Color.FromName("#CEF6D8")
            End If

        End If

        'hcano
        If Not ((Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
            (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
            (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0)) Or _
            Request.QueryString("op") = "evaPOA" Then
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
        End If

        'If Not ((Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
        '    (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
        '    (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0) Or _
        '    (Session("estadoPto") = "EVALUACION PRESUPUESTO" And Session("instanciaPto") = "DIRECCION DE FINANZAS" And (Session("ctfPto") = 156 Or Session("ctfPto") = 1))) Then

        '    e.Row.Cells(26).Visible = False
        '    e.Row.Cells(27).Visible = False
        'End If

        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 156 Then
            e.Row.Cells(28).Visible = True
        Else
            e.Row.Cells(28).Visible = False
        End If
        'hcano

    End Sub
    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting
        EliminarItem(gvDetalle.DataKeys.Item(e.RowIndex).Value, sender, e)
        e.Cancel = True
    End Sub
    Private Sub EliminarItem(ByRef codigo_dpr As Int64, ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
        Dim objPre As ClsPresupuesto
        Dim rpta As String
        Try
            objPre = New ClsPresupuesto
            objPre.AbrirTransaccionCnx()
            rpta = objPre.EliminarDetallePresupuesto_V2(codigo_dpr, Session("idPto"))
            objPre.CerrarTransaccionCnx()

            If rpta = "0" Then
                ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo eliminar, debido a que el proceso no está habilitado');", True)
            ElseIf rpta = "1" Then
                gvCabecera.DataBind()
                gvDetalle.DataBind()
                If gvCabecera.Rows.Count = 0 Then
                    ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden';", True)
                Else
                    If gvDetalle.Rows.Count = 0 Then
                        gvCabecera.SelectedIndex = 0
                        gvCabecera_SelectedIndexChanged(gvCabecera, New System.EventArgs())
                    End If
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo eliminar porque ocurrió un error al procesar los datos');", True)
            End If

            objPre = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('ocurrió un error al procesar los datos')", True)
        End Try
    End Sub
    Protected Sub gvDetalle_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetalle.RowEditing
        Session("codigoDprPto") = gvDetalle.DataKeys.Item(e.NewEditIndex).Value
        'Hcano
        Response.Redirect("RegistrarPresupuestoDetalle_V2.aspx?tipo=E&op=modPto")
        'Response.Redirect("RegistrarPresupuestoDetalle_V2-HC.aspx?tipo=E&op=modPto")
        'Hcano
        e.Cancel = False
    End Sub
    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        If Request.QueryString("op") = "evaPOA" Then
            Response.Redirect("../../../personal/indicadores/POA/FrmListaEvaluarPresupuesto.aspx?back=pto" & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cb1=" & Request.QueryString("cb1") & "&cb2=" & Request.QueryString("cb2") & "&cb3=" & Request.QueryString("cb3") & "&cb4=" & Request.QueryString("cb4"))
        Else
            Response.Redirect("GestionarPresupuesto.aspx?op=modPto")
        End If

    End Sub
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Session("codigoDprPto") = gvDetalle.DataKeys.Item(0).Value
        Response.Redirect("RegistrarPresupuestoDetalle_V2.aspx?tipo=N&op=modPto")
    End Sub

    Protected Sub btnObservar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObservar.Click
        ObtenerObservaciones(hddCodigo_pto.value)
        pnlObservar.Visible = True
        pnlPresupuesto.Visible = False
    End Sub

    Protected Sub cmdVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVolver.Click
        pnlObservar.Visible = False
        pnlPresupuesto.Visible = True
        If hddCantidadObs.Value > 0 Then
            ObtenerObservaciones(hddCodigo_pto.value)
            pnlObservaciones.Visible = True
        Else
            pnlObservaciones.Visible = False
        End If

        If gvCabecera.SelectedIndex >= 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden';", True)
        End If

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim objPre As New ClsPresupuesto
        Dim rpta As Integer
        Dim datosU As New Data.DataTable

        objPre.AbrirTransaccionCnx()
        rpta = objPre.ObservarPto(hddCodigo_pto.Value, txtObservacion.Text, Request.QueryString("id"), Request.QueryString("ctf"))
        objPre.CerrarTransaccionCnx()

        If rpta = 1 Then
            ObtenerObservaciones(hddCodigo_pto.Value)
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('Se guardó correctamente los datos');", True)

            datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))

            If datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA" Then
                lblInstancia.Text = ": RESPONSABLE"
                lblEstado.Text = ": OBSERVACION DIR. AREA"

            ElseIf datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Then
                lblInstancia.Text = ": DIRECCION DE AREA"
                lblEstado.Text = ": OBSERVACION FINAL"
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo guardar porque ocurrió un error al procesar los datos');", True)
        End If
        txtObservacion.Text = ""

    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click

        Dim objPre As New ClsPresupuesto
        Dim rpta As Integer
        Dim datosU As New Data.DataTable

        objPre.AbrirTransaccionCnx()
        rpta = objPre.AprobarPto(hddCodigo_pto.Value, Request.QueryString("id"), Request.QueryString("ctf"))
        objPre.CerrarTransaccionCnx()

        If rpta = 1 Then
            ObtenerObservaciones(hddCodigo_pto.value)

            datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))

            If datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA" Then
                lblInstancia.Text = ": DIRECCION DE FINANZAS"
                lblEstado.Text = ": EVALUACION PRESUPUESTO"

            ElseIf datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Then
                lblInstancia.Text = ": DIRECCION DE FINANZAS"
                lblEstado.Text = ": APROBADO"
            End If


            If gvCabecera.SelectedIndex >= 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden';", True)
            End If

        Else
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo aprobar porque ocurrió un error al procesar los datos');", True)
        End If

    End Sub
    
    Protected Sub cmdVisibilidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVisibilidad.Click
        Dim objpresu As New ClsPresupuesto
        Dim dt As New Data.DataTable
        For i As Integer = 0 To gvDetalle.Rows.Count - 1
            Dim chk As CheckBox = DirectCast(gvDetalle.Rows(i).Cells(28).FindControl("checkbox1"), CheckBox)
            If chk.Checked Then
                dt = objpresu.VisiblidadDetallePresupuesto(Me.gvDetalle.DataKeys.Item(i).Value, 1)
            Else
                dt = objpresu.VisiblidadDetallePresupuesto(Me.gvDetalle.DataKeys.Item(i).Value, 0)
            End If
        Next
        Me.cmdNuevo.Visible = False
    End Sub
End Class
