
Partial Class logistica_frmCosultarPedidosRealizados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Panel1.Visible = True
        If IsPostBack = False Then
            Dim obj As New ClsPresupuesto
            Dim combo As New ClsFunciones
            combo.CargarListas(Me.dpCodigo_pct, obj.ConsultarProcesoContable, "codigo_ejp", "descripcion_ejp", "--Seleccionar--")
            'Hcano 19-01-17 : Comento
            'combo.CargarListas(Me.dpCodigo_cco, obj.ObtenerListaCentroCostos("1", 523, Request.QueryString("id")), "codigo_cco", "descripcion_cco", "--TODOS--")
            '
            'combo.CargarListas(Me.dpCodigo_cla, obj.ObtenerListaClase("DES", ""), "codigo_cla", "descripcion_cla", "--TODOS--")
            combo = Nothing
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Dim obj As New ClsPresupuesto
        Dim datos As New Data.DataTable
        Me.Panel1.Visible = True
        'Response.Write(CInt(Me.dpCodigo_pct.SelectedItem.Text))
        'Response.Write(" " & Me.dpCodigo_cco.SelectedValue)
        If Me.dpCodigo_cco.SelectedIndex <> -1 Then
            datos = obj.ConsultarPedidosRealizados(CInt(Me.dpCodigo_pct.SelectedItem.Text), Me.dpCodigo_cco.SelectedValue)
            'Response.Write(datos.Rows.Count)
            Me.dgvPedidos.DataSource = datos
            Me.dgvPedidos.DataBind()
        Else
            Me.dgvPedidos.DataSource = ""
            Me.dgvPedidos.DataBind()
        End If
        
        obj = Nothing
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        If Me.dgvPedidos.Rows.Count > 0 Then
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-xls"
            Response.AddHeader("Content-Disposition", "attachment; filename=InformePedidosRealizados" & Me.dpCodigo_pct.SelectedItem.Text & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = System.Text.Encoding.Default
            Me.dgvPedidos.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
            Me.dgvPedidos.HeaderRow.ForeColor = Drawing.Color.White
            Response.Write(ClsFunciones.HTML(dgvPedidos, "INFORME DE PEDIDOS REALIZADOS PERIODO: " & Me.dpCodigo_pct.SelectedItem.Text, "Reporte emitido por: Sistema de Logítica - Campus Virtual USAT"))
            Response.End()
        Else
            Response.Write("<script>alert('No Existen Items a Exportar')</script>")
        End If
        
    End Sub
    'Hcano 19-01-17
    Protected Sub dpCodigo_pct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_pct.SelectedIndexChanged
        Dim obj As New ClsPresupuesto
        Dim combo As New ClsFunciones
        Dim tb As New Data.DataTable
        tb = obj.ObtenerListaCentroCostos_v1("LPR", Request.QueryString("id"), Request.QueryString("ctf"), Me.dpCodigo_pct.SelectedValue)
        'LPR : LOGISTICA : CONSULTAS : LISTA DE PEDIDOS REGISTRADOS
        If tb.Rows.Count > 0 Then
            combo.CargarListas(Me.dpCodigo_cco, tb, "codigo_cco", "descripcion_cco", "--TODOS--")
        Else
            Me.dpCodigo_cco.Items.Clear()
        End If

    End Sub
    'Hcano 19-01-17
End Class

