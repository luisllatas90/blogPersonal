
Partial Class administrativo_frmConsultarIngresosEgresosPersonal
    Inherits System.Web.UI.Page
    '
    Public periodo_anterior As String = Year(DateTime.Now.AddMonths(-1)) ' Year(Today).ToString 'planilla de Referencia
    Public periodo_actual As String = Year(Today).ToString


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         Dim fechaReferencia As DateTime = DateTime.Now.AddMonths(-1)
        'If (Month(Today) - 1).ToString.Length < 2 Then
        If (Month(fechaReferencia)).ToString.Length < 2 Then
            periodo_anterior = periodo_anterior & "0" & (Month(fechaReferencia)).ToString
        Else
            'periodo_anterior = periodo_anterior & (Month(Today) - 1).ToString
            periodo_anterior = periodo_anterior & (Month(fechaReferencia)).ToString
        End If
        If (Month(Today)).ToString.Length < 2 Then
            periodo_actual = periodo_actual & "0" & (Month(Today)).ToString
        Else
            periodo_actual = periodo_actual & (Month(Today)).ToString
        End If
        If Not Page.IsPostBack Then
            CargarPersonal()
            CargarAños()
        End If
        lblPlanilla.Text = "Planilla Ordinaria " & MonthName(Month(fechaReferencia)).ToUpper & " " & Year(fechaReferencia)
    End Sub
    Sub CargarPersonal()
        Dim tabla As New Data.DataTable
        Dim objcx As New ClsConectarDatos
        objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objcx.AbrirConexion()
        tabla = objcx.TraerDataTable("deuweb_ListarPersonal", periodo_anterior)
        If tabla.Rows.Count Then
            Me.ddlPersonal.DataSource = tabla
            Me.ddlPersonal.DataTextField = "personal"
            Me.ddlPersonal.DataValueField = "codigo_Per"
            Me.ddlPersonal.DataBind()
        End If
        objcx.CerrarConexion()
        objcx = Nothing
        Me.ddlPersonal.SelectedValue = 0
        Me.pnlResumen.Visible = False
        Me.panelDetalle.Visible = False
    End Sub
    Sub CargarAños()
        Dim item As String
        For i As Integer = Today.Year - 1 To Today.Year + 5
            item = i.ToString
            Me.ddlAño.Items.Add(New ListItem(item.ToString(), item.ToString()))
        Next i
        Me.ddlAño.SelectedValue = Year(Today)
        Me.ddlMes.SelectedValue = Month(Today)
    End Sub
    Sub Consultar()
        If ddlPersonal.SelectedValue > 0 Then
            Dim tabla As New Data.DataTable
            Dim objcx As New ClsConectarDatos
            Dim ingresos As Decimal = 0
            Dim egresos As Decimal = 0
            Dim descuentos As Decimal = 0
            Dim neto As Decimal = 0
            Dim ingresosext As Decimal = 0
            objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcx.AbrirConexion()
            tabla = objcx.TraerDataTable("deuweb_AcumuladasPorTrabajador", Me.ddlPersonal.SelectedValue, periodo_anterior, Me.ddlAño.SelectedValue, Me.ddlMes.SelectedValue, periodo_actual)
            If tabla.Rows.Count Then
                Me.gridIngresosEgresos.DataSource = tabla
                Me.pnlResumen.Visible = True
                For i As Integer = 0 To tabla.Rows.Count - 1
                    Select Case tabla.Rows(i).Item("TIPO")
                        Case "INGRESOS"
                            ingresos += tabla.Rows(i).Item("MONTO_I")
                        Case "EGRESOS"
                            egresos += tabla.Rows(i).Item("MONTO_E")
                        Case "DESCUENTOS"
                            descuentos += tabla.Rows(i).Item("MONTO_E")
                        Case "INGRESOS EXTRAORD."
                            ingresosext += tabla.Rows(i).Item("MONTO_I")
                    End Select
                Next
                neto = ingresos - (egresos + descuentos)
                lblIngresos.Text = FormatCurrency(ingresos, 2)
                lblEgresos.Text = FormatCurrency(egresos, 2)
                lblDescuentos.Text = FormatCurrency(descuentos, 2)
                lblNeto.Text = FormatCurrency(neto, 2)
                lblIngresosExt.Text = FormatCurrency(ingresosext, 2)
                lblNetoyExt.Text = FormatCurrency(neto + ingresosext, 2)

                If neto >= 750 Then
                    Me.mensaje.Attributes("class") = "success"                    
                    Me.mensaje.InnerText = "El trabajador no está endeudado."
                    Me.HdEstado.Value = "A" 'Apto
                Else
                    Me.mensaje.Attributes("class") = "warning"
                    Me.mensaje.InnerText = "El trabajador está endeudado."
                    Me.HdEstado.Value = "E" 'Endeudado
                End If
            Else
                Me.gridIngresosEgresos.DataSource = Nothing
                Me.pnlResumen.Visible = False
                Me.panelDetalle.Visible = False
                Me.mensaje.InnerText = ""
                Me.mensaje.Attributes("class") = ""

            End If
            Me.gridIngresosEgresos.DataBind()
            objcx.CerrarConexion()
            objcx = Nothing
            Me.panelDetalle.Visible = False
        End If
    End Sub
    Protected Sub ddlPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPersonal.SelectedIndexChanged
        Consultar()
    End Sub

    Protected Sub gridIngresosEgresos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridIngresosEgresos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If (e.Row.Cells(0).Text) = "INGRESOS" Or (e.Row.Cells(0).Text) = "INGRESOS EXTRAORD." Then
                e.Row.Cells(3).Text = ""
            Else
                e.Row.Cells(2).Text = ""
            End If
            If (e.Row.Cells(0).Text) = "DESCUENTOS" Then
                e.Row.Cells(3).ForeColor = Drawing.Color.Green
            End If
            If (e.Row.Cells(0).Text) = "INGRESOS EXTRAORD." Then
                e.Row.Cells(2).ForeColor = Drawing.Color.Purple
            End If
            If Not (e.Row.Cells(0).Text) = "DESCUENTOS" Then
                e.Row.Cells(4).Text = ""
            End If
        End If
    End Sub

    Protected Sub ddlMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        Consultar()
    End Sub

    Protected Sub ddlAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAño.SelectedIndexChanged
        Consultar()
    End Sub

    Protected Sub gridIngresosEgresos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridIngresosEgresos.RowCommand
        Me.panelDetalle.Visible = True
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim codigo_dpc As Integer = Me.gridIngresosEgresos.DataKeys.Item(index).Values(0)
        Dim tb As New Data.DataTable
        Session("codigo_ddc") = Me.gridIngresosEgresos.DataKeys.Item(index).Values(1)
        If (e.CommandName = "VerDetalle") Then
            Dim objcx As New ClsConectarDatos

            objcx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcx.AbrirConexion()
            tb = objcx.TraerDataTable("deuweb_ConsultarDetalleDeuda", codigo_dpc, "C")
            If tb.Rows.Count Then
                Me.lblFecha.Text = tb.Rows(0).Item("fechagen_dpc")
                Me.lblRubro.Text = tb.Rows(0).Item("descripcion_rub")                
                Me.lblImporte.Text = FormatCurrency(tb.Rows(0).Item("importe_dpc"), 2)
                Me.lblPagado.Text = FormatCurrency(tb.Rows(0).Item("importe_pagado"), 2)
                Me.lblPendiente.Text = FormatCurrency(tb.Rows(0).Item("importe_pendiente"), 2)
                tb = objcx.TraerDataTable("deuweb_ConsultarDetalleDeuda", codigo_dpc, "D")
                Me.gridDetalle.DataSource = tb
            Else
                Me.panelDetalle.Visible = False
            End If
            Me.gridDetalle.DataBind()
        End If
        Consultar()
        If tb.Rows.Count Then
            Me.panelDetalle.Visible = True
        Else
            Me.panelDetalle.Visible = False
        End If
    End Sub

    Protected Sub gridDetalle_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridDetalle.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If Session("codigo_ddc") = gridDetalle.DataKeys(e.Row.RowIndex).Values("codigo_ddc") Then
                e.Row.Cells(0).ForeColor = Drawing.Color.Green
                e.Row.Cells(1).ForeColor = Drawing.Color.Green
                e.Row.Cells(2).ForeColor = Drawing.Color.Green
            End If
        End If

    End Sub

    Protected Sub btnRedireccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedireccionar.Click
        If (Me.ddlPersonal.SelectedValue <> 0) Then
            Response.Redirect("FrmPrestamoPersonal.aspx?id=" & Me.ddlPersonal.SelectedValue & "&estado=" & Me.HdEstado.Value)
        End If
    End Sub
End Class
