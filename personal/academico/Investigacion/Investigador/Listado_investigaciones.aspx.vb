
Partial Class Listado_investigaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim tipo, id, codigo_cco As Integer
            Dim objdatos As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            tipo = Request.QueryString("tipo")
            id = Request.QueryString("id")
            If id = 1357 Or id = 892 Then
                tipo = 2
            End If
            Select Case tipo
                Case 1
                    ClsFunciones.LlenarListas(Me.cboPersonal, objdatos.TraerDataTable("ConsultarPersonalCentroCostos", "PE", id), "codigo_per", "nombres")
                Case 2
                    codigo_cco = objdatos.TraerDataTable("ConsultarPersonalCentroCostos", "PE", id.ToString).Rows(0).Item("codigo_cco")

                    If codigo_cco > 0 Then
                        ClsFunciones.LlenarListas(Me.cboPersonal, objdatos.TraerDataTable("ConsultarPersonalCentroCostos", 1, codigo_cco), "codigo_per", "Datos_Per")
                    End If
                Case 3
                    ClsFunciones.LlenarListas(Me.cboPersonal, objdatos.TraerDataTable("ConsultarPersonal", "TO", "0"), "codigo_per", "personal")
            End Select
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvInvestigaciones.RowDataBound
        e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
        e.Row.Style.Add("cursor", "hand")
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls()
    End Sub

    Private Sub Axls()
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=ReporteInvestigaciones.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        Response.Write(HTML())
        Response.End()

    End Sub

    Private Function HTML() As String
        Dim Page1 As New Page()
        Dim Form2 As New HtmlForm()
        Dim label As New Label
        Dim lblPiePag As New Label

        Page1.EnableViewState = False
        Page1.Controls.Add(Form2)
        Page1.EnableEventValidation = False

        label.Text = "Listado de investigaciones de " + Me.cboPersonal.SelectedItem.Text + " al " + Now.Date.ToShortDateString + "<br><br>"
        label.ForeColor = Drawing.Color.Black
        label.Font.Bold = True
        label.Font.Size = 12
        lblPiePag.Text = "Sistema de investigaciones - Campus virtual - USAT"
        lblPiePag.ForeColor = Drawing.Color.Blue
        label.Font.Size = 10
        Form2.Controls.Add(label)
        Form2.Controls.Add(Me.gvInvestigaciones)
        Form2.Controls.Add(lblPiePag)

        Dim builder1 As New System.Text.StringBuilder()
        Dim writer1 As New System.IO.StringWriter(builder1)
        Dim writer2 As New HtmlTextWriter(writer1)

        Page1.DesignerInitialize()
        Page1.RenderControl(writer2)
        Page1.Dispose()
        Page1 = Nothing
        Return builder1.ToString()
    End Function
End Class
