
Partial Class personal_aulavirtual_lebir_eva_rpteevaluacionparticipante
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Dim datos As New Data.DataTable

        datos = obj.TraerDataTable("DI_ConsultarRpteEvaluacionParticipante", "C", Session("idcursovirtual2"), "")

        Me.GridView1.DataSource = datos
        Me.GridView1.DataBind()

    End Sub
    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=rpteevaluacionparticipante.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub
    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)

        Form2.Controls.Add(Me.Label1)
        Form2.Controls.Add(Me.GridView1)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls()
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Response.Redirect("frmevaluaciones.aspx?idusuario=" & Session("codigo_usu2") & "&idcursovirtual=" & Session("idcursovirtual2") & "&idvisita=" & Session("idvisita2"))
    End Sub
End Class
