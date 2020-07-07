
Partial Class rpteinscritoseventoxescuelacicloing
    Inherits System.Web.UI.Page
    Protected Sub grwListaPersonas_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaPersonas1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                CargarListas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Private Sub CargarParticipantes()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwListaPersonas1.DataSource = obj.TraerDataTable("EVE_ConsultarInscritosXEscuelaCicloIngreso", Me.ddlCicloIngreso.SelectedValue, CInt(Me.ddlEscuela.SelectedValue))
        Me.grwListaPersonas1.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub CargarListas()
        Dim objfun As New ClsFunciones
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("EVE_ConsultarInscritosXEscuelaCicloIngreso_ListaEscuelas"), "codigo_cpf", "nombre_cpf", ">> Seleccione<<")
        objfun.CargarListas(Me.ddlCicloIngreso, obj.TraerDataTable("EVE_ConsultarInscritosXEscuelaCicloIngreso_ListaCicloIng"), "descripcion_cac", "descripcion_cac", ">> Seleccione<<")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwListaPersonas1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwListaPersonas1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=reporte_participantes_xEscuelaXCicloIngreso" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        CargarParticipantes()
    End Sub
End Class
