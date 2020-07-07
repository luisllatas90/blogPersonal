
Partial Class aulavirtual_adminaula_rptEstadisticaUso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("ctf") = 1 Or Request.QueryString("id") = "471" Or Request.QueryString("id") = "48" Then
        Else
            Me.tabla.Visible = False
            Response.Write("Usted no tiene permiso para ver este reporte. Comunicarse con desarrollosistemas@usat.edu.pe")
            Exit Sub

        End If
    End Sub

  


    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click

        Dim dt As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("BecaDocente_Marcaciones", Me.txtDesde.Value)
        obj.CerrarConexion()

        If dt.Rows.Count Then
            Me.GridView1.DataSource = dt
        Else
            Response.Write("No se encontraron registros")
        End If
        Me.GridView1.DataBind()
     
      
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
            Response.AddHeader("Content-Disposition", "attachment;filename=MarcacionesBecaDocente-" & Me.txtDesde.Value.ToString & ".xls")
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        Else
            Response.Write("Para exportar el contenido, debe consultar.")
        End If
       
    End Sub
End Class
