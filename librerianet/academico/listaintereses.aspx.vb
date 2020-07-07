
Partial Class librerianet_academico_listaintereses
    Inherits System.Web.UI.Page
    Dim TotalInteres As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.grwListaIntereses.datasource = obj.traerDataTable("CAJ_ConsultarInteresProgramaEspecial", Request.QueryString("id"), Request.QueryString("s"))
            Me.grwListaIntereses.databind()

            obj.cerrarconexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub grwListaIntereses_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaIntereses.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            'e.Row.Cells(7).Text = FormatNumber(CDbl(fila.Row("saldo")) + CDbl(fila.Row("mora_deu")), 2)
            TotalInteres += fila.Row("interes")
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(2).Text = "TOTAL:"
            e.Row.Cells(4).Text = FormatNumber(TotalInteres, 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub
End Class
