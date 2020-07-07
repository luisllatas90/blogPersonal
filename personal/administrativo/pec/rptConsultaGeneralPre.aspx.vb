
Partial Class aulavirtual_adminaula_rptEstadisticaUso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        If Not IsPostBack Then
            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            objfun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("RptEscuelaPre_cicloIngreso"), "codigo_cac", "descripcion_cac", ">> Seleccione<<")
            obj.CerrarConexion()
        End If
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If Not Me.ddlCiclo.SelectedValue = -1 Then
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("RptEscuelaPre_General", Me.ddlCiclo.SelectedItem.Text)
            obj.CerrarConexion()
            If dt.Rows.Count Then
                Me.GridView1.DataSource = dt
            Else
                Response.Write("No se encontraron registros")
            End If
            Me.GridView1.DataBind()
        Else
            Response.Write("Seleccione el ciclo de ingreso")
        End If
       
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        If Me.GridView1.Rows.Count Then
            Dim sb As StringBuilder = New StringBuilder()
            Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
            Dim Page As Page = New Page()
            Dim form As HtmlForm = New HtmlForm()
            Page.EnableEventValidation = False
            Page.DesignerInitialize()
            Page.Controls.Add(form)
            form.Controls.Add(Me.GridView1)
            Page.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "attachment;filename=ConsultaGeneralPre-" & Me.ddlCiclo.SelectedItem.Text & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Else
            Response.Write("Para exportar el contenido, debe consultar.")
        End If
       
    End Sub
End Class
