
Partial Class frmTipoActividad
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                CargaPeridoLaboral()
                CargaDptoAcad()
                CargarDatos()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaPeridoLaboral()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.CargaPeridoLaboral
            If dts.Rows.Count > 0 Then
                Me.ddlPeriodoLaboral.DataSource = dts
                Me.ddlPeriodoLaboral.DataTextField = "descripcion_Pel"
                Me.ddlPeriodoLaboral.DataValueField = "codigo_Pel"
                Me.ddlPeriodoLaboral.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargaDptoAcad()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            dts = obj.CargaDptoAcad
            If dts.Rows.Count > 0 Then
                Me.ddlDptAcad.DataSource = dts
                Me.ddlDptAcad.DataTextField = "nombre_Dac"
                Me.ddlDptAcad.DataValueField = "codigo_dac"
                Me.ddlDptAcad.DataBind()
                Me.ddlDptAcad.SelectedValue = "%"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub CargarDatos()
        Try
            Dim obj As New clsPersonal
            Dim dts As New Data.DataTable
            'dts = obj.ListaTipoActividad(20, 48, 0, "HD")
            'dts = obj.ListaTipoActividad(ddlPeriodoLaboral.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), IIf(chkFomrato.Checked = True, "HD", "HM"))
            'Modificado x yperez 06.05.15 nuevo parametro de filtro @area
            dts = obj.ListaTipoActividad_v1(ddlPeriodoLaboral.SelectedValue, Request.QueryString("id"), Request.QueryString("ctf"), IIf(chkFomrato.Checked = True, "HD", "HM"), Me.ddlDptAcad.SelectedItem.Text)
            If dts.Rows.Count > 0 Then
                gvTipoActividad.DataSource = dts
                gvTipoActividad.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvTipoActividad_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTipoActividad.RowDataBound
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

                If e.Row.Cells(2).Text <> "TOTAL" Then
                    e.Row.Cells(1).Text = e.Row.RowIndex + 1
                End If

                If e.Row.Cells(2).Text = "TOTAL" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#003399")
                    e.Row.ForeColor = Drawing.Color.White
                End If

                'Select Case e.Row.Cells(3).Text
                '    Case "TC"
                '        e.Row.Cells(3).ToolTip = "TIEMPO COMPLETO"
                '    Case "MT"
                '        e.Row.Cells(3).ToolTip = "MEDIO TIEMPO"
                '    Case "TP"
                '        e.Row.Cells(3).ToolTip = "TIEMPO PARCIAL < 20 HRS"
                '    Case "EX"
                '        e.Row.Cells(3).ToolTip = "EXTERNOS"
                '    Case "ST"
                '        e.Row.Cells(3).ToolTip = "SERVICIOS TEMPORALES"
                '    Case "DE"
                '        e.Row.Cells(3).ToolTip = "DEDICACION EXCLUSIVA"
                '    Case "T2"
                '        e.Row.Cells(3).ToolTip = "TIEMPO PARCIAL > 20 HRS"
                'End Select


            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub chkFomrato_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFomrato.CheckedChanged
        Try
            CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Try
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()
            Me.gvTipoActividad.EnableViewState = False
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.gvTipoActividad)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=TipoActividad" & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlPeriodoLaboral_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodoLaboral.SelectedIndexChanged
        Try
            CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlDptAcad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDptAcad.SelectedIndexChanged
        Try
            CargarDatos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
