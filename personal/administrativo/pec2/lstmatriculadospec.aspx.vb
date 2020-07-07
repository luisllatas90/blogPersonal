
Partial Class pec_lstmatriculadospec
    Inherits System.Web.UI.Page

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwParticipantes.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwParticipantes)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=matriculados_pecs" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Me.lbldescripcion_cco.Text = tblpec.Rows(0).Item("descripcion_cco")
            Me.lblcoordinador.Text = tblpec.Rows(0).Item("coordinador")

            Me.grwParticipantes.DataSource = obj.TraerDataTable("PEC_ConsultarMatriculadosProgramaEC", 1, Request.QueryString("id"), 0, 1)
            Me.grwParticipantes.DataBind()
        End If
        tblpec.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub grwParticipantes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwParticipantes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
End Class
