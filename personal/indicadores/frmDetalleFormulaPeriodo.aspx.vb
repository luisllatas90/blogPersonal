
Partial Class indicadores_frmDetalleFormulaPeriodo
    Inherits System.Web.UI.Page
    Dim Codigo_fp As Integer
    Dim codigo_pdo As String
    Dim Total As Decimal = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Codigo_fp = Request.QueryString("Codigo_fp")
                codigo_pdo = Request.QueryString("Codigo_pdo")
                CargaInformacion()  'Cargamos la informacion 
                CargarDetalle()     'Carga el detalle de la fórmula
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarDetalle()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Codigo_fp = Request.QueryString("Codigo_fp")
            codigo_pdo = Request.QueryString("Codigo_pdo")

            dts = obj.ListaDetalleFormula(Codigo_fp, codigo_pdo)

            If dts.Rows.Count Then
                gvDetalleFormula.DataSource = dts
                gvDetalleFormula.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaInformacion()
        Try
            Dim obj As New clsIndicadores
            Dim dts As New Data.DataTable

            Codigo_fp = Request.QueryString("Codigo_fp")
            codigo_pdo = Request.QueryString("Codigo_pdo")


            dts = obj.ListaInformacionFormulaPeriodo(Codigo_fp, codigo_pdo)

            If dts.Rows.Count > 0 Then
                txtNombrePlan.Text = dts.Rows(0).Item("Periodo_pla").ToString
                txtNombreCeco.Text = dts.Rows(0).Item("descripcion_Cco").ToString
                txtNombrePerspectiva.Text = dts.Rows(0).Item("descripcion_pers").ToString
                txtNombreObjetivo.Text = dts.Rows(0).Item("nombre_obj").ToString
                txtNombreIndicador.Text = dts.Rows(0).Item("descripcion_ind").ToString
                txtFormula.Text = dts.Rows(0).Item("descripcionc_for").ToString
                txtValor.Text = dts.Rows(0).Item("ValorFormula_vf").ToString
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub gvDetalleFormula_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvDetalleFormula.RowDataBound
        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                'Total = Total + Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "tmp_Valor"))
                'Linea para crear el correlativo del registro.
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If

            'If e.Row.RowType = DataControlRowType.Footer Then
            '    e.Row.Cells(4).Text = "Total"
            '    e.Row.Cells(5).Text = Total.ToString
            '    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
            '    e.Row.Font.Bold = True
            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
