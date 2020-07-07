'treyes
Partial Class presupuesto_areas_GestionarPresupuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            If Session("idPto") = "" Then
                Session("idPto") = Request.QueryString("id")
            End If
            If Session("ctfPto") = "" Then
                Session("ctfPto") = Request.QueryString("ctf")
            End If

            datos = objpre.ObtenerPeriodoPresupuestal(Session("ctfPto"))

            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            hddCodigo_Per.Value = Session("idPto")

            If Session("tipoPto") = "" Then
                hddTipo.Value = cboEstado.SelectedIndex
            Else
                hddTipo.Value = Session("tipoPto")
                cboEstado.SelectedIndex = Session("tipoPto")
            End If

            cboPeriodoPresu_SelectedIndexChanged(cboPeriodoPresu, New System.EventArgs())

            Call CargaPoas(cboPeriodoPresu.SelectedValue, hddCodigo_Per.Value)

            HddPoa.Value = ddlPoa.SelectedValue.ToString
        End If
    End Sub

    Sub CargaPoas(ByVal codigo_ejp As Integer, ByVal codigo_per As Integer)
        Dim objPre As New ClsPresupuesto
        Dim dtt As New Data.DataTable

        dtt = objPre.PRESU_ListarPoas(codigo_ejp, codigo_per)

        Me.ddlPoa.DataSource = dtt
        Me.ddlPoa.DataTextField = "descripcion"
        Me.ddlPoa.DataValueField = "codigo"
        Me.ddlPoa.DataBind()
        dtt.Dispose()
        objPre = Nothing
    End Sub

    Protected Sub gvPresupuesto_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvPresupuesto.RowEditing
        Session("codigoPto") = gvPresupuesto.DataKeys.Item(e.NewEditIndex).Values(0)
        Session("cecoPto") = gvPresupuesto.Rows(e.NewEditIndex).Cells(1).Text
        Session("instanciaPto") = gvPresupuesto.Rows(e.NewEditIndex).Cells(6).Text
        Session("estadoPto") = gvPresupuesto.Rows(e.NewEditIndex).Cells(7).Text
        Session("actividadPto") = gvPresupuesto.DataKeys.Item(e.NewEditIndex).Values(3)
        Session("nombre_poa") = gvPresupuesto.DataKeys.Item(e.NewEditIndex).Values(4)

        Response.Redirect("ModificarPresupuesto_V3.aspx?op=gesPto")
    End Sub
    Protected Sub cboPeriodoPresu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodoPresu.SelectedIndexChanged
        Call CargaPoas(cboPeriodoPresu.SelectedValue, hddCodigo_Per.Value)
        gvPresupuesto.DataBind()
    End Sub
    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("RegistrarPresupuestoDetalle_V2.aspx?op=gesPto")
    End Sub
    Protected Sub gvPresupuesto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPresupuesto.SelectedIndexChanged
        Dim objpre As New ClsPresupuesto
        Dim datos As New Data.DataTable
        Dim actividades As String = ""

        If gvPresupuesto.SelectedIndex <> -1 Then
            datos = objpre.ValidarEnvioPto(cboPeriodoPresu.SelectedValue, Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(1))
            If datos.Rows.Count() > 0 Then ' Actividades sin Pto asignado
                For i As Int16 = 0 To datos.Rows.Count() - 1
                    actividades += datos.Rows(i).Item("actividad") & " ,"
                Next
                actividades = actividades.TrimEnd(",")
                ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Presupuesto no puede ser enviado a Dirección de Área para su evaluación, las siguientes actividades aún no tienen asignado presupuesto: " & actividades & "');", True)
                gvPresupuesto.DataBind()
            Else
                datos = objpre.ObtenerTopeIngPresupuestal(Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(1), cboPeriodoPresu.SelectedValue)
                If datos.Rows.Count() > 0 Then
                    If Decimal.Parse(gvPresupuesto.Rows(gvPresupuesto.SelectedIndex).Cells(3).Text) < datos.Rows(0).Item("Ingresos") Then
                        ClientScript.RegisterStartupScript(Me.GetType, "error", "alert('Los ingresos son inferiores al tope presupuestal asignado');", True)
                    End If
                    datos = objpre.EnviarPresupuesto(Me.gvPresupuesto.DataKeys.Item(Me.gvPresupuesto.SelectedIndex).Values(1), cboPeriodoPresu.SelectedValue)
                    gvPresupuesto.DataBind()
                    ClientScript.RegisterStartupScript(Me.GetType, "mensaje", "alert('El Presupuesto fue enviado a evaluación: " & gvPresupuesto.Rows(gvPresupuesto.SelectedIndex).Cells(6).Text & "');", True)

                End If
            End If
        End If
    End Sub
    Protected Sub gvPresupuesto_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPresupuesto.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If (e.Row.Cells(7).Text <> "REGISTRO PRESUPUESTO" And e.Row.Cells(7).Text <> "OBSERVACION DIR. AREA") Or (cboEstado.SelectedValue = 2 And gvPresupuesto.DataKeys(e.Row.RowIndex).Values(2) = 0) Then
                Dim img As ImageButton
                img = e.Row.Cells(9).FindControl("imgEnviar")
                img.Style.Add("display", "none")
            End If

            If cboEstado.SelectedValue = 0 Then
                e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = False
                e.Row.BackColor = System.Drawing.Color.FromName("#F7FFDD")
            End If

            If cboEstado.SelectedValue = 2 Then
                If gvPresupuesto.DataKeys(e.Row.RowIndex).Values(2) = 0 Then
                    Dim img2 As ImageButton
                    img2 = e.Row.Cells(8).Controls(0)
                    img2.Visible = False
                    e.Row.BackColor = System.Drawing.Color.FromName("#F7FFDD")
                End If
            End If

            If (e.Row.Cells(6).Text = "DIRECCION DE AREA" And e.Row.Cells(7).Text = "EVALUACION PRESUPUESTO") Then
                e.Row.BackColor = System.Drawing.Color.FromName("#A7CBD5")
            End If

            If (e.Row.Cells(6).Text = "RESPONSABLE" And e.Row.Cells(7).Text = "OBSERVACION DIR. AREA") Then
                e.Row.BackColor = System.Drawing.Color.FromName("#FDCBCB")
            End If

            If (e.Row.Cells(6).Text = "DIRECCION DE FINANZAS" And e.Row.Cells(7).Text = "EVALUACION PRESUPUESTO") Then
                e.Row.BackColor = System.Drawing.Color.FromName("#D5F0FF")
            End If

            If (e.Row.Cells(6).Text = "DIRECCION DE AREA" And e.Row.Cells(7).Text = "OBSERVACION FINAL") Then
                e.Row.BackColor = System.Drawing.Color.FromName("#EEBCA1")
            End If

            If (e.Row.Cells(6).Text = "DIRECCION DE FINANZAS" And e.Row.Cells(7).Text = "APROBADO") Then
                e.Row.BackColor = System.Drawing.Color.FromName("#D2EEDC")
            End If

            If Request.QueryString("op") = "modPto" Then
                If gvPresupuesto.DataKeys(e.Row.RowIndex).Values(0) = Session("codigoPto") Then
                    'e.Row.BackColor = System.Drawing.Color.FromName("#E3F9FF")
                    e.Row.Font.Bold = True
                End If
            End If

        ElseIf e.Row.RowType = DataControlRowType.Header Then
            If cboEstado.SelectedValue = 0 Then
                e.Row.Cells(8).Visible = False
                e.Row.Cells(9).Visible = False
            End If
        End If
    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged
        If cboEstado.SelectedIndex <> -1 Then
            hddTipo.Value = cboEstado.SelectedIndex
            Session("tipoPto") = hddTipo.Value
            gvPresupuesto.DataBind()
        End If
    End Sub

    Protected Sub ddlPoa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPoa.SelectedIndexChanged
        HddPoa.Value = ddlPoa.SelectedValue.ToString
        gvPresupuesto.DataBind()
    End Sub
End Class
