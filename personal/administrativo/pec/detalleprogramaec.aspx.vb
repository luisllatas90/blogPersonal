
Partial Class librerianet_pec_detalleprogramaec
    Inherits System.Web.UI.Page
    Dim crd, ht, hi, ha, hep, th As Integer
    Dim ap, dp As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim tblpec As Data.DataTable
            tblpec = obj.TraerDataTable("PEC_ConsultarProgramaEC", 6, Request.QueryString("id"), 0, 1)

            If tblpec.Rows.Count > 0 Then
                Me.lbldescripcion_tpec.Text = tblpec.Rows(0).Item("descripcion_tpec")
                Me.lblDescripcion_pes.Text = tblpec.Rows(0).Item("descripcion_pes")
                Me.lblversion_pec.Text = tblpec.Rows(0).Item("version_pec")
                Me.lbldescripcion_epec.Text = tblpec.Rows(0).Item("descripcion_epec")
                Me.lblfechainicio_pec.Text = tblpec.Rows(0).Item("fechainicio_pec")
                Me.lblfechafin_pec.Text = tblpec.Rows(0).Item("fechafin_pec")
                Me.lblnroresolucion_pec.Text = tblpec.Rows(0).Item("nroresolucion_pec")
                Me.lblhorarios.Text = tblpec.Rows(0).Item("horarios_pec")
                Me.lblhorastotales.Text = tblpec.Rows(0).Item("totalHoras_Pes")
                Me.lbldescripcion_cco.Text = tblpec.Rows(0).Item("descripcion_cco")
                Me.lblcoordinador.Text = tblpec.Rows(0).Item("coordinador")
                Me.lblOperador.Text = tblpec.Rows(0).Item("operador")

                Me.grwModulosPEC.DataSource = obj.TraerDataTable("PEC_ConsultarEvaluacionPEC", 0, Request.QueryString("id"), 0, 0)
                Me.grwModulosPEC.DataBind()
            End If
            tblpec.Dispose()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Protected Sub grwModulosPEC_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwModulosPEC.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(10).Text = CDbl(fila("aprobados") + fila("desaprobados"))

            crd += fila("creditos_cur")
            ht += fila("horasteo_cur")
            hi += fila("horaspra_cur")
            ha += fila("horasase_cur")
            hep += fila("horaslab_cur")
            th += fila("totalhoras_cur")
            ap += fila("aprobados")
            dp += fila("desaprobados")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(1).Text = "TOTAL"
            e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(2).Text = crd
            e.Row.Cells(3).Text = ht
            e.Row.Cells(4).Text = hi
            e.Row.Cells(5).Text = ha
            e.Row.Cells(6).Text = hep
            e.Row.Cells(7).Text = th
            e.Row.Cells(8).Text = ap
            e.Row.Cells(9).Text = dp
        End If
    End Sub
End Class
