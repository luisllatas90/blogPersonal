Imports System.Data

Partial Class Egresados_rptPostulaciones
    Inherits System.Web.UI.Page

    Sub CargarData()
        Try
            Dim obj As New ClsConectarDatos
            Dim dt As Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            dt = obj.TraerDataTable("ALUMNI_ReportePostulacionesDetalle")
            'Me.gwData.DataSource = dt
            'Me.gwData.DataBind()
            'dt.Dispose()
            obj.CerrarConexion()
            obj = Nothing

            'creo mi dt
            Dim miDataTable As New DataTable
            miDataTable.Columns.Add("orden")
            miDataTable.Columns.Add("fechaReg")
            miDataTable.Columns.Add("nombrePro")
            miDataTable.Columns.Add("titulo_ofe")
            miDataTable.Columns.Add("fechaInicioAnuncio")
            miDataTable.Columns.Add("fechaFinAnuncio")
            miDataTable.Columns.Add("lugar_ofe")
            miDataTable.Columns.Add("egresado")
            miDataTable.Columns.Add("fechaPost")
            miDataTable.Columns.Add("correoProfesional_Ega")
            miDataTable.Columns.Add("url")


            For Each row As DataRow In dt.Rows

                Dim Renglon As DataRow = miDataTable.NewRow()

                Renglon("orden") = CStr(row("orden"))
                Renglon("fechaReg") = CStr(row("fechaReg"))
                Renglon("nombrePro") = CStr(row("nombrePro"))
                Renglon("titulo_ofe") = CStr(row("titulo_ofe"))
                Renglon("fechaInicioAnuncio") = CStr(row("fechaInicioAnuncio"))
                Renglon("fechaFinAnuncio") = CStr(row("fechaFinAnuncio"))
                Renglon("lugar_ofe") = CStr(row("lugar_ofe"))
                Renglon("egresado") = CStr(row("egresado"))
                Renglon("fechaPost") = CStr(row("fechaPost"))
                Renglon("correoProfesional_Ega") = CStr(row("correoProfesional_Ega"))
                Renglon("url") = "https://intranet.usat.edu.pe/campusestudiante/alumniegresadousat.aspx?xcod=" & CStr(encode(row("codigo_alu")))
                miDataTable.Rows.Add(Renglon)

                'Dim valor As String = CStr(row("NombreCampo"))

            Next

            Me.gwData.DataSource = miDataTable
            Me.gwData.DataBind()
            dt.Dispose()

        Catch ex As Exception
            Throw ex
        End Try
       
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gwData)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=CampusVirtualAlumni_ReportePostulacionesDetalle" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        CargarData()
    End Sub

    Protected Sub gwData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gwData.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If

    End Sub
    Function encode(ByVal str As String) As String
        Return (Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(str)))
    End Function
End Class
