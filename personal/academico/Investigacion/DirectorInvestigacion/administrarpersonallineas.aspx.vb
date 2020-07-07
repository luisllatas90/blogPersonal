
Partial Class DirectorInvestigacion_administrarpersonallineas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim ObjArbol As New Investigacion
            ClsFunciones.LlenarListas(Me.DDLUnidad, ObjArbol.ConsultarUnidadesInvestigacion("1", Request.QueryString("id")), 0, 1, "---- Seleccione Unidad de Investigacion ----")
            ObjArbol = Nothing
        End If
    End Sub

    Protected Sub DDLUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLUnidad.SelectedIndexChanged
        Dim ObjArbol As New Investigacion
        Me.HddCodigoCco.Value = Me.DDLUnidad.SelectedValue
        ClsFunciones.LlenarListas(Me.DDLArea, ObjArbol.ConsultarUnidadesInvestigacion_New(0, Me.DDLUnidad.SelectedValue), 0, 1, "---- Seleccione Area de Investigación ----")
        Me.DDLTematica.Visible = False
        Me.LblTematica.Visible = False
        ObjArbol = Nothing
    End Sub

    Protected Sub DDLArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLArea.SelectedIndexChanged
        Dim ObjArbol As New Investigacion
        ClsFunciones.LlenarListas(Me.DDLLinea, ObjArbol.ConsultarUnidadesInvestigacion_New(Me.DDLArea.SelectedValue, Me.HddCodigoCco.Value), 0, 1, "---- Seleccione Linea de Investigación ----")
        Me.DDLTematica.Visible = False
        Me.LblTematica.Visible = False
        ObjArbol = Nothing
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
                e.Row.Cells(0).Text = e.Row.RowIndex + 1
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DDLLinea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLLinea.SelectedIndexChanged
        Dim ObjInv As New Investigacion
        Dim Tematicas As New Data.DataTable
        Tematicas = ObjInv.ConsultarUnidadesInvestigacion_New(Me.DDLLinea.SelectedValue, Me.HddCodigoCco.Value)
        If Tematicas.Rows.Count > 0 Then
            Me.LblTematica.Visible = True
            Me.DDLTematica.Visible = True
            ClsFunciones.LlenarListas(DDLTematica, Tematicas, 0, 1, "---- Seleccione Tematica ----")
        Else
            Me.LblTematica.Visible = False
            Me.DDLTematica.Visible = False
            Me.GridView1.DataSource = ObjInv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Me.DDLLinea.SelectedValue, 1)
            Me.GridView1.DataBind()
        End If
        ObjInv = Nothing
    End Sub

    Protected Sub DDLTematica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLTematica.SelectedIndexChanged
        Dim ObjInv As New Investigacion
        Me.GridView1.DataSource = ObjInv.ConsultarPersonaldeLineaInvestigacion(Me.HddCodigoCco.Value, Me.DDLTematica.SelectedValue, 1)
        ObjInv = Nothing
        Me.GridView1.DataBind()
    End Sub
End Class
