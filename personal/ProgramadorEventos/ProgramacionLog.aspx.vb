
Partial Class ProgramacionLog
    Inherits System.Web.UI.Page

#Region "Variables globales"
    Private objCRM As New ClsCRM
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ln_CodigoPro As Integer = Request.QueryString.Item("cod")

        If Not IsPostBack Then
            CargarGrillaProgramacionLog(ln_CodigoPro)
        End If
    End Sub
#End Region

#Region "Métodos"
    Private Sub CargarGrillaProgramacionLog(ByVal ln_CodigoPro As Integer)
        Dim dtDatos As Data.DataTable = objCRM.ListarHistorialProgramacion(ln_CodigoPro)
        grwProgramacionLog.DataSource = dtDatos
        grwProgramacionLog.DataBind()
    End Sub
#End Region

    Protected Sub grwProgramacionLog_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwProgramacionLog.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim _cellsRow As TableCellCollection = e.Row.Cells
            Dim ln_Index As Integer = e.Row.RowIndex + 1
            _cellsRow(0).Text = ln_Index
        End If
    End Sub
End Class
