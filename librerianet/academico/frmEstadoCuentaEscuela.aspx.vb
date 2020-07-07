
Partial Class librerianet_academico_frmEstadoCuentaEscuela
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16
            Dim codigo_usu As Integer

            codigo_tfu = Request.QueryString("ctf")
            codigo_usu = Request.QueryString("id")
            '=================================
            'Permisos por Escuela
            '=================================
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
            Else
                tbl = obj.TraerDataTable("consultaracceso", "ESC", Request.QueryString("mod"), codigo_usu)
            End If
            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_cpf, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac")
            tbl.Dispose()
            obj = Nothing
        End If
    End Sub

    Private Sub Axls(ByVal nombrearchivo As String, ByRef grid As GridView, ByVal titulo As String, ByVal piedepagina As String)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-xls"
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombrearchivo & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = System.Text.Encoding.Default
        '  gvConsulta.HeaderRow.BackColor = Drawing.Color.FromName("#3366CC")
        ' gvConsulta.HeaderRow.ForeColor = Drawing.Color.White
        Response.Write(ClsFunciones.HTML(gvConsulta, titulo, piedepagina))
        Response.End()
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Axls("ReportePresupuesto", gvConsulta, "Reporte Escuela: " & dpCodigo_cpf.SelectedItem.Text & " Ciclo: " & dpCodigo_cac.SelectedItem.Text, "Campus Virtual USAT")
    End Sub
End Class
