
Partial Class librerianet_presupuesto_consultas_ReporteEjecutado
    Inherits System.Web.UI.Page
    Dim TotalEjecutado As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objFun As New ClsFunciones
            Dim datos As New Data.DataTable
            Dim objPre As New ClsPresupuesto
            'Cargar datos de programa presuspuestal
            objFun.CargarListas(chklProgPresupuestal, objPre.ObtenerListaProgramaPresupuestal(True), "codigo_ppr", "descripcion_ppr")
            'Cargar Datos Centro Costos
            datos = objPre.ObtenerListaCentroCostos("E2", Request.QueryString("id"))
            objFun.CargarListas(chklCecos, datos, "codigo_Cco", "descripcion_Cco")
            Me.hddCC.Value = Me.chklCecos.Items.Count
            Me.hddPP.Value = Me.chklProgPresupuestal.Items.Count
            Me.chkTodosCC.Attributes.Add("onClick", "MarcarTodosCC()")
            Me.chkTodosPP.Attributes.Add("onClick", "MarcarTodosPP()")
            Me.chklCecos.Attributes.Add("onClick", "VerificarMarcadosCC()")
            Me.chklProgPresupuestal.Attributes.Add("onClick", "VerificarMarcadosPP()")
            Me.chkTodosCC.Checked = False
            If Request.QueryString("ctf") <> 1 Then
                Me.chkTodosCC.Visible = False
            Else
                Me.chkTodosCC.Visible = True
            End If
            Me.TxtFechaD.Text = "01012010"
            Me.TxtFechaH.Text = "30092014"
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim objPre As New ClsPresupuesto
        Dim listaCodigo_cco, listaCodigo_ppr, listaEstado As String
        If CDate(Me.TxtFechaH.Text) < CDate(Me.TxtFechaD.Text) Then
            ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('La fecha final debe ser mayor o igual que la fecha inicial que desea consultar');", True)
            Exit Sub
        End If
        listaEstado = ""
        listaCodigo_cco = ""
        listaCodigo_ppr = ""

        'ARMAR CADENA ESTADO
        If chklEstado.Items(0).Selected = False And chklEstado.Items(1).Selected = False Then
            ClientScript.RegisterStartupScript(Me.GetType, "Estado", "alert('Seleccione algún estado que desea consultar');", True)
            Exit Sub
        Else
            If chklEstado.Items(0).Selected = True Then
                listaEstado = "'" & chklEstado.Items(0).Value & "'"
            End If
            If chklEstado.Items(1).Selected = True Then
                If listaEstado = "" Then
                    listaEstado = "'" & chklEstado.Items(1).Value & "'"
                Else
                    listaEstado = listaEstado & ", '" & chklEstado.Items(1).Value & "'"
                End If
            End If
        End If

        'ARMAR CENTRO COSTOS
        If chkTodosCC.Checked = True Then
            listaCodigo_cco = ""
        Else
            For i As Int16 = 0 To Me.chklCecos.Items.Count - 1
                If chklCecos.Items(i).Selected = True Then
                    If listaCodigo_cco = "" Then
                        listaCodigo_cco = chklCecos.Items(i).Value
                    Else
                        listaCodigo_cco = listaCodigo_cco & ", " & chklCecos.Items(i).Value
                    End If
                End If
            Next
        End If


        'ARMAR PROGRAMA PRESUPUESTAL
        If chkTodosPP.Checked = True Then
            listaCodigo_ppr = ""
        Else
            For i As Int16 = 0 To Me.chklProgPresupuestal.Items.Count - 1
                If chklProgPresupuestal.Items(i).Selected = True Then
                    If listaCodigo_ppr = "" Then
                        listaCodigo_ppr = chklProgPresupuestal.Items(i).Value
                    Else
                        listaCodigo_ppr = listaCodigo_ppr & ", " & chklProgPresupuestal.Items(i).Value
                    End If
                End If
            Next
        End If

        If Me.chkTodosCC.Checked = False And listaCodigo_cco = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Todos CECOS", "alert('Seleccione algún centro de costos o marque la opción todos');", True)
            Exit Sub
        End If
        If Me.chkTodosPP.Checked = False And listaCodigo_ppr = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "Todos PPR", "alert('Seleccione algún programa presupuestal o marque la opción todos');", True)
            Exit Sub
        End If

        Dim datos As Data.DataTable
        datos = objPre.ConsultarEjecutadoConsolidado(Me.TxtFechaD.Text, Me.TxtFechaH.Text, CByte(Me.chkTodosCC.Checked), listaCodigo_cco, CByte(Me.chkTodosPP.Checked), listaCodigo_ppr, listaEstado)
        If datos.Rows.Count > 0 Then
            Me.gvEjecutadoConsolidado.DataSource = datos
            Me.gvEjecutadoConsolidado.DataBind()
            Me.lblTotalEjecutado.Text = "Total Ejecutado: " & TotalEjecutado
        Else
            Me.gvEjecutadoConsolidado.DataSource = Nothing
            Me.gvEjecutadoConsolidado.DataBind()
            Me.lblTotalEjecutado.Text = ""
        End If
    End Sub

    Protected Sub gvEjecutadoConsolidado_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvEjecutadoConsolidado.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(3).Text = CDate(e.Row.Cells(3).Text).ToShortDateString
            e.Row.Cells(9).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(10).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(11).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(12).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(20).Visible = False
            TotalEjecutado = TotalEjecutado + CDbl(e.Row.Cells(11).Text)
        End If

    End Sub


    Protected Sub gvEjecutadoConsolidado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvEjecutadoConsolidado.SelectedIndexChanged

    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        cmdConsultar_Click(sender, e)
        Axls("ReportePresupuesto" & Date.Now.Day.ToString & Date.Now.Month.ToString & Date.Now.Year.ToString, gvEjecutadoConsolidado, "REPORTE DE EJECUTADO CONSOLIDADO", "Sistema de Presupuesto - Campus Virtual USAT")
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        gvEjecutadoConsolidado.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        gvEjecutadoConsolidado.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(gvEjecutadoConsolidado, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub hddPP_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles hddPP.ValueChanged

    End Sub
End Class
