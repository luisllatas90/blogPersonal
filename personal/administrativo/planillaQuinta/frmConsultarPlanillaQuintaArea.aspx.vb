Partial Class planillaQuinta_frmConsultarPlanillaQuintaArea
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objpre As New ClsPresupuesto
            Dim objPlla As New clsPlanilla
            Dim objfun As New ClsFunciones
            Dim datos As New Data.DataTable

            'Dim codigo_per As String = ""
            'Dim clsF As New ClsCRM

            'If Request.Form("PersonaId") <> "" Then
            '    codigo_per = clsF.DesencriptaTexto(Request.Form("PersonaId"))
            'End If ' Session("id_per")

            '*** Cargar datos de Proceso presupuestal ***
            datos = objpre.ObtenerListaProcesos()
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPeriodoPresu, datos, "codigo_ejp", "descripcion_ejp")
            End If

            '*** Cargar Datos Centro Costos ***
            datos = objPlla.ConsultarCentroCostosQuinta(Request.QueryString("id"))
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboCecos, datos, "codigo_Cco", "descripcion_Cco", ">> Todos <<")
            End If

            '*** Cargar Datos Planilla ***
            datos = objPlla.ConsultarPlanilla(Request.QueryString("ctf"))
            If datos.Rows.Count > 0 Then
                objfun.CargarListas(cboPlanilla, datos, "codigo_plla", "PLanilla")
            End If

            Me.hddCodigo_Adplla.Value = 0
        End If
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
        End If

        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.BackColor = Drawing.Color.LightGray
            e.Row.Cells(3).Text = FormatNumber(hddTotal.Value, 2)
            e.Row.Cells(3).Font.Bold = True
            e.Row.Cells(2).Text = "Total"
            e.Row.Cells(2).Font.Bold = True
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowIndex = 0 Then
                hddAntes.Value = 0
                hddActual.Value = 0
            End If
            hddAntes.Value = hddAntes.Value + CDbl(e.Row.Cells(1).Text)
            hddActual.Value = hddActual.Value + CDbl(e.Row.Cells(2).Text)
        End If
        If e.Row.RowType = DataControlRowType.Footer Then
            e.Row.BackColor = Drawing.Color.LightGray
            e.Row.Cells(1).Text = FormatNumber(hddAntes.Value, 2)
            e.Row.Cells(1).Font.Bold = True
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right

            e.Row.Cells(2).Text = FormatNumber(hddActual.Value, 2)
            e.Row.Cells(2).Font.Bold = True
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Text = "Total"
            e.Row.Cells(0).Font.Bold = True
        End If
    End Sub

    Protected Sub cboPlanilla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPlanilla.SelectedIndexChanged
        Dim cls As New clsaccesodatos
        Dim dt As New Data.DataTable
        Try
            cls.abrirconexion()
            dt = cls.consultar("PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorAreayCco", cboPlanilla.SelectedValue, cboCecos.SelectedValue, Session("id_per")).Tables(0)
            cls.cerrarconexion()

            Me.gvDetalleQuinta.DataSource = dt
            Me.gvDetalleQuinta.DataBind()

        Catch ex As Exception
            Response.Write("Mensaje Usuario:" + ex.Message)
        End Try
        'gvDetalleQuinta.DataSource = Nothing
        'gvDetalleQuinta.DataSource = ObjectDataSource1
        'gvDetalleQuinta.DataBind()
    End Sub

    Protected Sub cboCecos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCecos.SelectedIndexChanged
        Dim cls As New clsaccesodatos
        Dim dt As New Data.DataTable
        Try
            cls.abrirconexion()
            dt = cls.consultar("PRESU_ConsultarAnexoDetallePlanillaEjecutadoPorAreayCco", cboPlanilla.SelectedValue, cboCecos.SelectedValue, Session("id_per")).Tables(0)
            cls.cerrarconexion()

            Me.gvDetalleQuinta.DataSource = dt
            Me.gvDetalleQuinta.DataBind()

        Catch ex As Exception
            Response.Write("Mensaje Usuario:" + ex.Message)
        End Try
        'gvDetalleQuinta.DataSource = Nothing
        'gvDetalleQuinta.DataSource = ObjectDataSource1
        'gvDetalleQuinta.DataBind()
    End Sub

    Protected Sub gvDetalleQuinta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDetalleQuinta.SelectedIndexChanged

    End Sub
End Class
