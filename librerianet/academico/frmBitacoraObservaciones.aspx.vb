
Partial Class academico_frmBitacoraObservaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then

            If (Request.QueryString("codigouniv") IsNot Nothing) Then
                CargarBitacoraObservaciones()
            Else
                diverrores.InnerHtml = "No se ha realizado la consulta."
            End If
        End If
    End Sub

    Private Sub CargarBitacoraObservaciones()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("EPRE_ListarBitacoraObservaciones", Request.QueryString("codigouniv"))
            If dt.Rows.Count() > 0 Then
                Me.grwListaObservaciones.DataSource = dt
            Else
                diverrores.InnerHtml = "No se han registrado observaciones para el estudiante."
            End If
            Me.grwListaObservaciones.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub grwListaObservaciones_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaObservaciones.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
