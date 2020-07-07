
Partial Class presupuesto_areas_detallePresupuesto
    Inherits System.Web.UI.Page
    Dim totaling As Double
    Dim totalegr As Double
    Dim numing, numegr As Int16
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objPre As ClsPresupuesto
            Dim objFun As ClsFunciones
            objPre = New ClsPresupuesto
            objFun = New ClsFunciones

            objFun.CargarListas(cboProceso, objPre.ConsultarProcesoContable(), "codigo_ejp", "descripcion_ejp")
            'If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 6 Then

            If Me.cboProceso.SelectedValue < 8 Then
                objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id")), "codigo_cco", "descripcion_cco")
            Else
                objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos_v1("LPR", Request.QueryString("id"), Request.QueryString("ctf"), Me.cboProceso.SelectedValue), "codigo_cco", "descripcion_cco")
            End If
            'Else
            '    objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("CP", Request.QueryString("id")), "codigo_cco", "descripcion_cco")
            'End If
            numegr = 0
            numing = 0
            totalegr = 0
            totaling = 0
            OcultarDetalle(False)
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click

        If Me.cboCentroCostos.SelectedValue <> "" Then
            Dim objcon As ClsPresupuesto
            objcon = New ClsPresupuesto
            Dim DatosIng, datosEgr As New Data.DataTable
            Dim techoIng, techoEgr As Double
            If Me.cboProceso.SelectedValue < 8 Then
                DatosIng = objcon.ConsultarDetallePresupuesto(cboProceso.SelectedValue, cboCentroCostos.SelectedValue, "I")
                datosEgr = objcon.ConsultarDetallePresupuesto(cboProceso.SelectedValue, cboCentroCostos.SelectedValue, "E")
            Else
                DatosIng = objcon.ConsultarDetallePresupuesto_v1(cboProceso.SelectedValue, cboCentroCostos.SelectedValue, "I")
                datosEgr = objcon.ConsultarDetallePresupuesto_v1(cboProceso.SelectedValue, cboCentroCostos.SelectedValue, "E")
            End If

            With DatosIng
                If DatosIng.Rows.Count > 0 Then
                    gvDetalle.DataSource = DatosIng
                    lblEjercicio.Text = .Rows(0).Item("Periodo Presupuestal")
                    lblCecos.Text = cboCentroCostos.SelectedItem.Text
                    lblEstado.Text = .Rows(0).Item("estado")
                    techoIng = FormatNumber(.Rows(0).Item("techoingresos_pto"), 2)
                Else
                    gvDetalle.DataSource = Nothing
                End If
                gvDetalle.DataBind()
                lblTechoIng.Text = " Techo Ingreso: " & FormatNumber(techoIng, 2)
                lblTotalIng.Text = " Total Ingreso: " & FormatNumber(totaling, 2)
                lblDiferenciaIng.Text = " Diferencia: " & FormatNumber(techoIng - totaling, 2)
            End With

            With datosEgr
                If datosEgr.Rows.Count > 0 Then
                    techoEgr = FormatNumber(datosEgr.Rows(0).Item("techoegresos_pto"), 2)
                    gvEgresos.DataSource = datosEgr
                    lblEjercicio.Text = .Rows(0).Item("Periodo Presupuestal")
                    lblCecos.Text = cboCentroCostos.SelectedItem.Text
                    lblEstado.Text = .Rows(0).Item("estado")
                Else
                    gvEgresos.DataSource = Nothing
                End If
            End With
            gvEgresos.DataBind()

            lblTechoEgr.Text = " Techo Egreso: " & FormatNumber(techoEgr, 2)
            lblTotalEgr.Text = " Total Egreso: " & FormatNumber(totalegr, 2)
            lblDiferenciaEgr.Text = " Diferencia: " & FormatNumber(techoEgr - totalegr, 2)

            Me.lblDiferencia.Text = "Total de Ingresos - Total de Egresos: S/. " & (FormatNumber(totaling - totalegr, 2)).ToString
            Me.lblDiferencia.ForeColor = Drawing.Color.Blue
            OcultarDetalle(True)
        Else
            Response.Write("<script>alert('Debe Seleccionar un Centro de Costo.')</script>")
        End If
    End Sub


    Private Sub OcultarDetalle(ByVal valor As Boolean)
        lblIngresos.Visible = valor
        lblEgresos.Visible = valor
        lblDiferenciaEgr.Visible = valor
        lblDiferenciaIng.Visible = valor
        lblTotalEgr.Visible = valor
        lblTotalIng.Visible = valor
        lblTechoEgr.Visible = valor
        lblTechoIng.Visible = valor
        If valor = True Then
            ClientScript.RegisterStartupScript(Me.GetType, "Habilitar1", "tblDetalle.style.visibility='visible';", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType, "Habilitar1", "tblDetalle.style.visibility='hidden';", True)
        End If
    End Sub

    Private Sub OcultarCeldas(ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        e.Row.Cells(2).Visible = False
        e.Row.Cells(27).Visible = False
        e.Row.Cells(28).Visible = False
        e.Row.Cells(29).Visible = False
    End Sub

    Protected Sub gvDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalle.RowDataBound

        e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        e.Row.Attributes.Add("Class", "Sel")
        e.Row.Attributes.Add("Typ", "Sel")

        If e.Row.RowType = DataControlRowType.Header Then
            OcultarCeldas(e)
            e.Row.VerticalAlign = VerticalAlign.Top
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.VerticalAlign = VerticalAlign.Top
            If e.Row.Cells(0).Text = "I" Then
                totaling = totaling + CDbl(e.Row.Cells(14).Text)
            End If
            For i As Int16 = 12 To 26
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
            Next
            OcultarCeldas(e)
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(13).Text = "Total"
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(14).Text = FormatNumber(totaling, 2)
            e.Row.Cells(14).HorizontalAlign = HorizontalAlign.Left
            hddTotalIng.Value = 0
            OcultarCeldas(e)
        End If
    End Sub

    Protected Sub gvEgresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEgresos.RowDataBound

        e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        e.Row.Attributes.Add("Class", "Sel")
        e.Row.Attributes.Add("Typ", "Sel")

        If e.Row.RowType = DataControlRowType.Header Then
            OcultarCeldas(e)
            e.Row.VerticalAlign = VerticalAlign.Top
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.VerticalAlign = VerticalAlign.Top
            If e.Row.Cells(0).Text = "E" Then
                totalegr = totalegr + CDbl(e.Row.Cells(14).Text)
            End If
            For i As Int16 = 12 To 26
                e.Row.Cells(i).Text = FormatNumber(e.Row.Cells(i).Text, 2)
                e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Right
            Next
            OcultarCeldas(e)
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(13).Text = "Total"
            e.Row.Cells(13).HorizontalAlign = HorizontalAlign.Left
            e.Row.Cells(14).Text = FormatNumber(totalegr, 2)
            e.Row.Cells(14).HorizontalAlign = HorizontalAlign.Left
            hddTotalIng.Value = 0
            OcultarCeldas(e)
        End If
    End Sub



    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReportePresupuesto", gvDetalle, "Reporte de Presupuesto por Centro de Costos" & ". <br>Emitido el ", "Sistema de Presupuesto - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        IF gvDetalle.Rows.Count > 0 then
            gvDetalle.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
	    gvDetalle.HeaderRow.ForeColor = Drawing.Color.White
        END IF 
        Response.Write(HTML(titulo, piedepagina))
        Response.End()
    End Sub


    Public Function HTML(ByVal titulo As String, ByVal piepagina As String) As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim label As New Label
        Dim lblPiePag As New Label

        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False
        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        writer2.Write("<H4>REPORTE DE PRESUPUESTO POR CENTRO DE COSTOS</H4>")
        writer2.Write("<b><table style='width:100%'; background-color: '#FFFFEA;' border='0' cellpadding=3 cellspacing=0 id=tblDetalle>")
        writer2.Write("<tr><td colspan=3><b>Ejercicio contable: " & lblEjercicio.Text.ToString & "</b>")
        writer2.Write("</td><td>&nbsp;</td></tr>")
        writer2.Write("<tr><td  colspan=3 ><b>Centro de costos: " & lblCecos.Text.ToString & "</b>")
        writer2.Write("<tr><td  colspan=3 ><b>Estado: " & lblEstado.Text.ToString & "</b></td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
        writer2.Write("</table></b>")


        label.Text = titulo + Now.Date.ToShortDateString + "<br><br>"
        label.ForeColor = Drawing.Color.Black
        label.Font.Bold = True
        label.Font.Size = 12
        label.Font.Size = 10
        lblPiePag.Text = "<BR>" & piepagina
        label.Text = "<BR><BR>"
        lblPiePag.ForeColor = Drawing.Color.Gray
        Form2.Controls.Add(lblIngresos)
        Form2.Controls.Add(lblTechoIng)
        Form2.Controls.Add(label)
        Form2.Controls.Add(lblTotalIng)
        Form2.Controls.Add(label)
        Form2.Controls.Add(lblDiferenciaIng)
        Form2.Controls.Add(label)
        gvEgresos.HeaderRow.CssClass = "TituloTabla"
        Form2.Controls.Add(gvDetalle)

        Form2.Controls.Add(label)
        Form2.Controls.Add(lblEgresos)
        Form2.Controls.Add(lblTechoEgr)
        Form2.Controls.Add(lblTotalEgr)
        Form2.Controls.Add(lblDiferenciaEgr)
        gvEgresos.HeaderRow.CssClass = "TituloTabla"
        Form2.Controls.Add(gvEgresos)

        Form2.Controls.Add(lblDiferencia)
        Form2.Controls.Add(lblPiePag)
        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)

        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function

    Protected Sub cboProceso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProceso.SelectedIndexChanged
        Dim objPre As ClsPresupuesto
        Dim objFun As ClsFunciones
        objPre = New ClsPresupuesto
        objFun = New ClsFunciones

        If Me.cboProceso.SelectedValue < 8 Then
            objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos("1", 523, Request.QueryString("id")), "codigo_cco", "descripcion_cco")
        Else
            objFun.CargarListas(cboCentroCostos, objPre.ObtenerListaCentroCostos_v1("LPR", Request.QueryString("id"), Request.QueryString("ctf"), Me.cboProceso.SelectedValue), "codigo_cco", "descripcion_cco")
        End If

    End Sub
End Class

