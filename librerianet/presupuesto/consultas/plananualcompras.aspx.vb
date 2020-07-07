
Partial Class presupuesto_consultas_plananualcompras
    Inherits System.Web.UI.Page
    Dim PrecioUnitario As Double
    Dim Cantidad As Double
    Dim SubTotal, Total, Disponible As Double
    Dim ene, feb, mar, abr, may, jun, jul, ago, sep, oct, nov, dic As Double
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load        
        If IsPostBack = False Then
            Dim obj As New ClsPresupuesto
            Dim combo As New ClsFunciones
            combo.CargarListas(Me.dpCodigo_pct, obj.ConsultarProcesoContable, "codigo_ejp", "descripcion_ejp", "--Seleccionar--")
            'combo.CargarListas(Me.dpCodigo_cco, obj.ObtenerListaCentroCostos("1", 523, Request.QueryString("id")), "codigo_cco", "descripcion_cco", "--TODOS--")
            combo.CargarListas(Me.dpCodigo_cla, obj.ObtenerListaClase("DES", ""), "codigo_cla", "descripcion_cla", "--TODOS--")
            combo = Nothing
            obj = Nothing

        End If

        ScriptManager.RegisterStartupScript(up, Me.GetType(), "MyAction", "abrir();", True)
    End Sub
    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-xls"
            Response.AddHeader("Content-Disposition", "attachment; filename=InformePlanAnualCompras" & Me.dpCodigo_pct.SelectedItem.Text & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = System.Text.Encoding.Default
            grwPlanAnualCompras.HeaderRow.BackColor = Drawing.Color.FromName("#E33439")
            grwPlanAnualCompras.HeaderRow.ForeColor = Drawing.Color.White
            grwPlanAnualCompras.FooterRow.BackColor = Drawing.Color.FromName("#E33439")
            grwPlanAnualCompras.FooterRow.ForeColor = Drawing.Color.White
            Response.Write(ClsFunciones.HTML(grwPlanAnualCompras, "PLAN ANUAL DE COMPRAS " & Me.dpCodigo_pct.SelectedItem.Text, "Reporte emitido por: Sistema de Presupuesto - Campus Virtual USAT"))

            'Response.Write(ClsFunciones.HTML(GridView1, "PLAN ANUAL DE COMPRAS " & Me.dpCodigo_pct.SelectedItem.Text, "Reporte emitido por: Sistema de Presupuesto - Campus Virtual USAT"))
            Response.End()
            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)

        Catch ex As Exception
            'Response.Write(ex)
        End Try

    End Sub
    Sub CommandBtn_Click(ByVal sender As Object, ByVal e As CommandEventArgs)

            Dim commandArgs() As String
            Dim DesEstandar As String
            Dim PresAcum As String
            Dim SalEjeAcum As String
            Dim Pedido As String
            Dim SalPreEje As String
            Dim MesIni As String
            Dim MesFin As String
            Dim PresAnt As String
            Dim transferido_dpr As String
            Dim Disponible As String

            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se calificó el pedido satisfactoriamente');", True)

            commandArgs = e.CommandArgument.ToString().Split(New Char() {","})
            DesEstandar = commandArgs(0)
            PresAcum = commandArgs(1)
            SalEjeAcum = commandArgs(2)
            Pedido = commandArgs(3)
            SalPreEje = commandArgs(4)
            MesIni = commandArgs(5)
            MesFin = commandArgs(6)
            PresAnt = commandArgs(7)
            transferido_dpr = commandArgs(8)
            Disponible = commandArgs(9)

            Me.thDesEstandar.InnerText = DesEstandar
            Me.tdPresAcum.InnerText = " " & PresAcum
            Me.tdSalEjeAcum.InnerText = " " & SalEjeAcum & " (- Pedido: " & Pedido & ")"
            Me.tdSalPresEje.InnerText = " " & SalPreEje & " (- Fechas:  " & MesIni & " - " & MesFin & ": " & PresAnt & ")"
            Me.tdTransferido.InnerText = " " & transferido_dpr
            Me.tdDisponible.InnerText = " " & Disponible

            ClientScript.RegisterStartupScript(Me.GetType, "ABrir", "abrir();", True)

            Me.pnlVerDetalle1.Style("display") = "block"
            Me.pnlVerDetalle1.Style("top") = "100px"
            Me.pnlVerDetalle1.Style("left") = "200px"
            Me.divBackground.Style("display") = "block"

            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "ScrollPage", "ResetScrollPosition();", True)

    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim obj As New ClsPresupuesto

        'Hcano : 04-07-17
        'dt = obj.ConsultarPlanAnualCompras(Me.dpCodigo_pct.SelectedValue, Me.dpCodigo_cco.SelectedValue, Me.dpCodigo_cla.SelectedValue)
        dt = obj.ConsultarPlanAnualComprasV1(Me.dpCodigo_pct.SelectedValue, Me.dpCodigo_cco.SelectedValue, Me.dpCodigo_cla.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"))
        'Hcano : 04-07-17

        Me.grwPlanAnualCompras.DataSource = dt
        Me.grwPlanAnualCompras.DataBind()
        'Me.GridView1.DataSource = dt
        'Me.GridView1.DataBind()
        obj = Nothing

    End Sub

    Protected Sub grwPlanAnualCompras_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPlanAnualCompras.RowDataBound

        'Protected Sub grwPlanAnualCompras_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwPlanAnualCompras.RowDataBound, GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            If Me.dpCodigo_cco.SelectedValue = -1 Then
                e.Row.Cells(10).Enabled = False
            End If

            Dim fila As Data.DataRowView
            Dim chk As CheckBox = CType(e.Row.FindControl("chkElegir"), CheckBox)
            fila = e.Row.DataItem
            ''Cambiar Texto
            'e.Row.Cells(10).Text = IIf(fila("indicoCantidades") = True, "Sí", "No")
            'Diferencia de Techo ingresos
            e.Row.Cells(23).Text = FormatNumber(CDbl(fila("ene")) + _
                                                CDbl(fila("feb")) + _
                                                CDbl(fila("mar")) + _
                                                CDbl(fila("abr")) + _
                                                CDbl(fila("may")) + _
                                                CDbl(fila("jun")) + _
                                                CDbl(fila("jul")) + _
                                                CDbl(fila("ago")) + _
                                                CDbl(fila("sep")) + _
                                                CDbl(fila("oct")) + _
                                                CDbl(fila("nov")) + _
                                                CDbl(fila("dic")), 2)
            Me.PrecioUnitario += CDbl(fila.Item("preUnitario"))
            Me.Cantidad += CDbl(fila.Item("Cantidad"))
            Me.SubTotal += CDbl(fila.Item("subTotal"))
            Me.Disponible += CDbl(fila.Item("Disponible"))
            Me.ene += CDbl(fila.Item("ene"))
            Me.feb += CDbl(fila.Item("feb"))
            Me.mar += CDbl(fila.Item("mar"))
            Me.abr += CDbl(fila.Item("abr"))
            Me.may += CDbl(fila.Item("may"))
            Me.jun += CDbl(fila.Item("jun"))
            Me.jul += CDbl(fila.Item("jul"))
            Me.ago += CDbl(fila.Item("ago"))
            Me.sep += CDbl(fila.Item("sep"))
            Me.oct += CDbl(fila.Item("oct"))
            Me.nov += CDbl(fila.Item("nov"))
            Me.dic += CDbl(fila.Item("dic"))
            Total += CDbl(e.Row.Cells(23).Text)

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "TOTAL"
            e.Row.Cells(7).Text = FormatNumber(PrecioUnitario, 2)
            e.Row.Cells(8).Text = FormatNumber(Cantidad, 2)
            e.Row.Cells(9).Text = FormatNumber(SubTotal, 2)
            e.Row.Cells(10).Text = FormatNumber(Disponible, 2)
            e.Row.Cells(11).Text = FormatNumber(ene, 2)
            e.Row.Cells(12).Text = FormatNumber(feb, 2)
            e.Row.Cells(13).Text = FormatNumber(mar, 2)
            e.Row.Cells(14).Text = FormatNumber(abr, 2)
            e.Row.Cells(15).Text = FormatNumber(may, 2)
            e.Row.Cells(16).Text = FormatNumber(jun, 2)
            e.Row.Cells(17).Text = FormatNumber(jul, 2)
            e.Row.Cells(18).Text = FormatNumber(ago, 2)
            e.Row.Cells(19).Text = FormatNumber(sep, 2)
            e.Row.Cells(20).Text = FormatNumber(oct, 2)
            e.Row.Cells(21).Text = FormatNumber(nov, 2)
            e.Row.Cells(22).Text = FormatNumber(dic, 2)
            e.Row.Cells(23).Text = FormatNumber(Total, 2)

            Me.PrecioUnitario = 0
            Me.Cantidad = 0
            Me.SubTotal = 0
            Me.Disponible = 0
            Me.ene = 0
            Me.feb = 0
            Me.mar = 0
            Me.abr = 0
            Me.may = 0
            Me.jun = 0
            Me.jul = 0
            Me.ago = 0
            Me.sep = 0
            Me.oct = 0
            Me.nov = 0
            Me.dic = 0
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.pnlVerDetalle1.Style("display") = "none"
        Me.divBackground.Style("display") = "none"
    End Sub

    Protected Sub dpCodigo_pct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_pct.SelectedIndexChanged
        Dim obj As New ClsPresupuesto
        Dim combo As New ClsFunciones
        'LPR : LOGISTICA: CONSULTAS : LISTA DE PEDIDOS REGISTRADOS, SE UTLIZA EL MISMO PROCEDIMIENTO 
        'SE ENVIADIRECTAMENTE COMO CTF = 1 : ADMINISTRDOR PARA QUE CARGUE TODOS LOS CECOS.
        combo.CargarListas(Me.dpCodigo_cco, obj.ObtenerListaCentroCostos_v2("LPR", Request.QueryString("id"), Request.QueryString("ctf"), Me.dpCodigo_pct.SelectedValue, 0), "codigo_cco", "descripcion_cco", "--TODOS--")
    End Sub
End Class
