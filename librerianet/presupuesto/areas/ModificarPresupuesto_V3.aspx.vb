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
            lblpoa.Text = ": " & Session("nombre_poa")
            lblCeco.Text = ": " & Session("cecoPto")
            lblInstancia.Text = ": " & Session("instanciaPto")
            lblEstado.Text = ": " & Session("estadoPto")
            hddCodigo_pto.Value = Session("codigoPto")

            ObtenerObservaciones(hddCodigo_pto.Value)
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
                Me.divOrden.Visible = True
                Me.cboOrdenar.SelectedValue = "F"
                pnlObservar.Visible = False
                btnObservar.Visible = False
                btnAprobar.Visible = False
            End If

            If Request.QueryString("op") = "gesPto" Then
                gvCabecera.DataBind()
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden'", True)
                pnlObservar.Visible = False
                btnObservar.Visible = False
                btnAprobar.Visible = False
            End If

            If Request.QueryString("op") = "evaPOA" Then
                VerificaAutorizacion(Session("codigoPto"))
                gvCabecera.DataBind()
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden'", True)
                pnlObservar.Visible = False
            End If
            Me.cmdVisibilidad.Visible = False
            VisibleMoverActividad()
        End If
    End Sub

    Private Sub VerificaAutorizacion(ByVal codigo_pto As Integer)
        Dim obj As New ClsPresupuesto
        Dim dt As New Data.DataTable
        dt = obj.VerificaAutorizacionFinanzas_POA(codigo_pto)
        If dt.Rows.Count > 0 Then
            Me.hddautorizaFinanzas.Value = dt.Rows(0).Item("autorizadirfin").ToString
        Else
            Me.hddautorizaFinanzas.Value = 0
        End If

    End Sub

    Private Sub VisibleMoverActividad()
        Dim obj As New ClsPresupuesto
        If obj.POA_VisibleMoverActividadPOA(hddCodigo_pto.Value) > 0 And Session("instanciaPto") = "RESPONSABLE" And Session("estadoPto") = "REGISTRO PRESUPUESTO" Then
            Me.gvCabecera.Columns(7).Visible = True
        Else
            Me.gvCabecera.Columns(7).Visible = False
        End If
        If Request.QueryString("op") = "evaPOA" And (Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158) Then
            Me.gvCabecera.Columns(7).Visible = True
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
            For i As Int16 = 0 To datos.Rows.Count - 1
                If datos.Rows(i).Item("resuelto_obs") = 1 Then
                    blObservaciones.Items(i).Attributes("Style") = "Color:Green"
                End If
            Next

            If Request.QueryString("op") = "evaPOA" Then
                datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))
                If datos.Rows(datos.Rows.Count - 1).Item("resuelto_obs") = 1 Then
                    If (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "RESPONSABLE - OBS. DIR. AREA" And (datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "COORDINADOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA")) Or (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "DIR. AREA - OBS. FINAL" And (datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "COORDINADOR DE FINANZAS")) Then
                        btnObservar.Enabled = 1
                        btnAprobar.Enabled = 1
                    Else
                        btnObservar.Enabled = 0
                        btnAprobar.Enabled = 0
                    End If
                Else

                    If (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "RESPONSABLE - OBS. DIR. AREA" And datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA") Or (datos.Rows(datos.Rows.Count - 1).Item("nombre_iep") = "DIR. AREA - OBS. FINAL" And (datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "COORDINADOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA")) Then
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
            e.Row.Cells(0).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(1).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(2).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(3).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(5).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(6).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")
            e.Row.Cells(8).Attributes.Add("OnClick", "javascript:__doPostBack('gvCabecera','Select$" & e.Row.RowIndex & "');")

            If Request.QueryString("op") = "regPto" Then
                If gvCabecera.DataKeys(e.Row.RowIndex).Values(0) = hddCodigo_dap.Value Then
                    e.Row.BackColor = System.Drawing.Color.FromName("#D8E5FF")
                End If
            End If

            TotalIngresos += CDec(e.Row.Cells(1).Text)
            TotalEgresos += CDec(e.Row.Cells(2).Text)

            Dim ddl As DropDownList = e.Row.FindControl("cboMesIni")
            ddl.SelectedValue = e.Row.DataItem("mes_ini")

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "Totales"
            e.Row.Cells(1).Text = String.Format("{0:N2}", TotalIngresos)
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).Text = String.Format("{0:N2}", TotalEgresos)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Font.Bold = True
            TotalIngresos = 0
            TotalEgresos = 0
        End If
    End Sub
    Protected Sub gvCabecera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCabecera.SelectedIndexChanged
        Dim estado As String
        Dim instancia As String

        hddCodigo_dap.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        hddCodigo_ejp.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        hddCodigo_iep.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(2)
        hddHabilitado_pto.Value = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(3)

        Session("dapPto") = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(0)
        Session("ejpPto") = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(1)
        Session("habilitadoPto") = Me.gvCabecera.DataKeys.Item(Me.gvCabecera.SelectedIndex).Values(3)

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
        ObtenerObservaciones(hddCodigo_pto.Value)
        'gvDetalle.AllowSorting = True
        Me.divOrden.Visible = True
        Me.cboOrdenar.SelectedValue = "F"
        gvDetalle.Visible = True
        gvDetalle.DataBind()

        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158 Then
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

        If Not ((Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
            (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
            (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0)) Or _
            Request.QueryString("op") = "evaPOA" Then
            e.Row.Cells(26).Visible = False
            e.Row.Cells(27).Visible = False
        End If

        If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 156 Or Request.QueryString("ctf") = 158 Then

            If Me.hddautorizaFinanzas.Value = True Then
                e.Row.Cells(26).Visible = True
                e.Row.Cells(27).Visible = True
            Else
                e.Row.Cells(26).Visible = False
                e.Row.Cells(27).Visible = False
            End If
            e.Row.Cells(28).Visible = True
            e.Row.Cells(29).Visible = True

        Else
            e.Row.Cells(28).Visible = False
            e.Row.Cells(29).Visible = False
        End If

        If e.Row.Cells(0).Text = "I" Then
            e.Row.Cells(29).Visible = False

        End If



    End Sub
    Protected Sub gvDetalle_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvDetalle.RowDeleting
        EliminarItem(gvDetalle.DataKeys.Item(e.RowIndex).Value, sender, e)
        e.Cancel = True
    End Sub

    Protected Sub ibtnMover_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If (Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
                (Session("estadoPto") = "OBSERVACION DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
                (Session("estadoPto") = "OBSERVACION FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0) Then
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='visible'", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='hidden';", True)
            End If
            Dim obj As New ClsPresupuesto
            Dim codigo_dap, codigo_ejp, codigo_cco As Integer
            Dim combo As DropDownList
            Dim row As GridViewRow
            Dim ibtnMover As ImageButton
            ibtnMover = sender
            row = ibtnMover.NamingContainer
            combo = DirectCast(Me.gvCabecera.Rows(row.RowIndex).FindControl("cboMesIni"), DropDownList)
            If combo.SelectedIndex <> -1 Then

                If combo.SelectedValue = CInt(Me.gvCabecera.DataKeys(row.RowIndex).Item("fecini_dap").Substring(3, 2)) Then
                    ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se Pudo mover Actividad, Mes De Inicio es El Mismo');", True)
                Else
                    codigo_dap = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_Dap")
                    codigo_ejp = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_Ejp")
                    codigo_cco = Me.gvCabecera.DataKeys(row.RowIndex).Item("codigo_cco")

                    Dim mensajerpta As String = obj.POA_MoverActividadPOA(codigo_dap, codigo_ejp, codigo_cco, combo.SelectedValue)

                    If mensajerpta = "0" Then
                        Me.gvDetalle.DataBind()
                        ClientScript.RegisterStartupScript(Me.GetType, "Mensaje", "alert('No se Pudo Mover Actividad, Contactarse con Área de Desarrollo');", True)

                    Else
                        gvCabecera.DataBind()
                        gvDetalle.DataBind()
                        ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('" & mensajerpta & "');", True)
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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
        If Request.QueryString("op") = "evaPOA" Then
            Session("RegresaEvaPoa") = 1
            Session("id_eva") = Request.QueryString("id")
            Session("ctf_eva") = Request.QueryString("ctf")
            Session("cb1_eva") = Request.QueryString("cb1")
            Session("cb2_eva") = Request.QueryString("cb2")
            Session("cb3_eva") = Request.QueryString("cb3")
            Session("cb4_eva") = Request.QueryString("cb4")
        End If
        Response.Redirect("RegistrarPresupuestoDetalle_V2.aspx?tipo=E&op=modPto")
        e.Cancel = False
    End Sub
    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        If Request.QueryString("op") = "evaPOA" Or Session("RegresaEvaPoa") = 1 Then
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
        ObtenerObservaciones(hddCodigo_pto.Value)
        pnlObservar.Visible = True
        pnlPresupuesto.Visible = False
    End Sub

    Protected Sub cmdVolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVolver.Click
        pnlObservar.Visible = False
        pnlPresupuesto.Visible = True
        If hddCantidadObs.Value > 0 Then
            ObtenerObservaciones(hddCodigo_pto.Value)
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
        If hddCodigo_pto.Value = 0 Then
            rpta = objPre.ObservarPtoxCodigoACP(Request.QueryString("acp"), txtObservacion.Text, Request.QueryString("id"), Request.QueryString("ctf"))
        Else
            rpta = objPre.ObservarPto(hddCodigo_pto.Value, txtObservacion.Text, Request.QueryString("id"), Request.QueryString("ctf"))
        End If

        objPre.CerrarTransaccionCnx()

        If rpta = 1 Then
            ObtenerObservaciones(hddCodigo_pto.Value)
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('Se guardó correctamente los datos');", True)

            datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))

            If datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA" Then
                lblInstancia.Text = ": RESPONSABLE"
                lblEstado.Text = ": OBSERVACION DIR. AREA"

            ElseIf datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "COORDINADOR DE FINANZAS" Then
                lblInstancia.Text = ": DIRECCION DE AREA"
                lblEstado.Text = ": OBSERVACION FINAL"
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo guardar porque ocurrió un error al procesar los datos');", True)
        End If
        txtObservacion.Text = ""
        btnObservar.Visible = False
        btnAprobar.Visible = False
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        Dim objPre As New ClsPresupuesto
        Dim rpta As Integer
        Dim datosU As New Data.DataTable

        objPre.AbrirTransaccionCnx()
        If hddCodigo_pto.Value = 0 Then
            rpta = objPre.AprobarPtoxCodigoACP(Request.QueryString("acp"), Request.QueryString("id"), Request.QueryString("ctf"))
        Else
            rpta = objPre.AprobarPto(hddCodigo_pto.Value, Request.QueryString("id"), Request.QueryString("ctf"))
        End If

        objPre.CerrarTransaccionCnx()
        If rpta = 1 Then
            ObtenerObservaciones(hddCodigo_pto.Value)
            datosU = objPre.ObtenerFuncionUsuarioPOA(Request.QueryString("id"), Request.QueryString("ctf"))

            If datosU.Rows(0).Item("descripcion_Tfu") = "RESPONSABLE DE POA" Then
                lblInstancia.Text = ": DIRECCIÓN DE FINANZAS"
                lblEstado.Text = ": EVALUACIÓN PRESUPUESTO"

            ElseIf datosU.Rows(0).Item("descripcion_Tfu") = "DIRECTOR DE FINANZAS" Or datosU.Rows(0).Item("descripcion_Tfu") = "COORDINADOR DE FINANZAS" Then
                lblInstancia.Text = ": DIRECCIÓN DE FINANZAS"
                lblEstado.Text = ": APROBADO"
            End If

            If gvCabecera.SelectedIndex >= 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='visible';TdNuevo.style.visibility='hidden';", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdDetalle.style.visibility='hidden';TdNuevo.style.visibility='hidden';", True)
            End If

            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('El Presupuesto fue correctamente aprobado Ha sido enviado a DIRECCIÓN DE FINANZAS para su Evaluación.');", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('No se pudo aprobar porque ocurrió un error al procesar los datos');", True)
        End If
        btnObservar.Visible = False
        btnAprobar.Visible = False
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
        ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='hidden'", True)
    End Sub

    Protected Sub cboOrdenar_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOrdenar.SelectedIndexChanged
        Me.gvDetalle.DataBind()

        If Request.QueryString("op") = "evaPOA" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='hidden';", True)
        Else
            If (Session("estadoPto") = "REGISTRO PRESUPUESTO" And Session("instanciaPto") = "RESPONSABLE") Or _
            (Session("estadoPto") = "OBSERVACIÓN DIR. AREA" And Session("instanciaPto") = "RESPONSABLE" And hddEvaPto.Value = 0) Or _
            (Session("estadoPto") = "OBSERVACIÓN FINAL" And Session("instanciaPto") = "DIRECCION DE AREA" And hddEvaPto.Value = 0) Then
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='visible'", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "TdNuevo.style.visibility='hidden';", True)
            End If
        End If
    End Sub



    Protected Sub ibtnTransferir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim row As GridViewRow
            Dim ibtnTransferir As ImageButton
            ibtnTransferir = sender
            row = ibtnTransferir.NamingContainer

            Dim url As String = "TransferirPresupuesto.aspx?id=" + Me.gvDetalle.DataKeys(row.RowIndex).Item("codigo_dpr").ToString

            ClientScript.RegisterStartupScript(Me.GetType, "Mostrar", "openPopup('" + url + "');", True)
            gvDetalle.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
