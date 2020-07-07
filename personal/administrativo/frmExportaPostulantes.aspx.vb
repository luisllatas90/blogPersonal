
Partial Class administrativo_frmExportaPostulantes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then

            If (Request.QueryString("pro") IsNot Nothing And Request.QueryString("ceco") IsNot Nothing _
                And Request.QueryString("min") IsNot Nothing And Request.QueryString("dni") IsNot Nothing _
                And Request.QueryString("coduni") IsNot Nothing And Request.QueryString("nombres") IsNot Nothing _
                And Request.QueryString("estpos") IsNot Nothing And Request.QueryString("mod") IsNot Nothing _
                And Request.QueryString("alu") IsNot Nothing And Request.QueryString("categor") IsNot Nothing _
                And Request.QueryString("impre") IsNot Nothing) Then
                CargarInscritosConCargo()
                Exportar()
            Else
                diverrores.InnerHtml = "No se ha realizado la consulta."
            End If
        End If
    End Sub

    Private Sub CargarInscritosConCargo()
        Try
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Me.grwListaPersonas.DataSource = obj.TraerDataTable("EPRE_ListarPostulantes", _
            Request.QueryString("pro"), Request.QueryString("ceco"), Request.QueryString("min"), _
            Request.QueryString("dni"), Request.QueryString("coduni"), Request.QueryString("nombres"), _
            Request.QueryString("estpos"), Request.QueryString("mod"), Request.QueryString("alu"), _
            Request.QueryString("categor"), Request.QueryString("impre"), 0, "")

            Me.grwListaPersonas.DataBind()
            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Private Sub Exportar()
        Try
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()

            Me.grwListaPersonas.EnableViewState = False
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.grwListaPersonas)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=inscritos_cargo" & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grwListaPersonas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub grwListaPersonas_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwListaPersonas.DataBound

        If Me.grwListaPersonas.Rows.Count Then

            For Each row As GridViewRow In grwListaPersonas.Rows
                If grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 1 Then
                    row.Cells(14).Text = "Impresa"
                ElseIf grwListaPersonas.DataKeys(row.RowIndex).Values("imprimiocartacat_Dal") = 0 Then
                    row.Cells(14).Text = "No Impresa"
                End If

                If Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "I" Then
                    row.Cells(11).Text = "Ingresante"
                ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "P" Then
                    row.Cells(11).Text = "Postulante"
                ElseIf Trim(grwListaPersonas.DataKeys(row.RowIndex).Values("EstadoPostulacion")) = "R" Then
                    row.Cells(11).Text = "Retirado"
                End If
            Next
        End If
    End Sub

End Class
