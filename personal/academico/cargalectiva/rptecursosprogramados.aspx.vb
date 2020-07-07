
Partial Class rptecursosprogramados
    Inherits System.Web.UI.Page
    Dim codigo_cup As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwCursosProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            contador = contador + 1
            'Combinar celdas
            If codigo_cup = fila("codigo_cup") Then
                e.Row.Cells(0).Text = ""
                e.Row.Cells(1).Text = ""
                e.Row.Cells(2).Text = ""
                e.Row.Cells(3).Text = ""
                e.Row.Cells(5).Text = ""
                e.Row.Cells(6).Text = ""
                e.Row.Cells(7).Text = ""
                contador = contador - 1
            Else
                e.Row.Cells(0).CssClass = "bordesup"
                e.Row.Cells(1).CssClass = "bordesup"
                e.Row.Cells(2).CssClass = "bordesup"
                e.Row.Cells(3).CssClass = "bordesup"
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.Cells(5).CssClass = "bordesup"
                e.Row.Cells(6).CssClass = "bordesup"
                e.Row.Cells(7).CssClass = "bordesup"
                e.Row.VerticalAlign = VerticalAlign.Middle
                codigo_cup = fila("codigo_cup").ToString()
                PrimeraFila = e.Row.RowIndex

                'Me.lblmensaje.Text = contador
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")
            Dim modulo As Int16 = Request.QueryString("mod")
            tbl = obj.TraerDataTable("EVE_ConsultarCarreraProfesional", modulo, codigo_tfu, codigo_usu)
            ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
            If (Request.QueryString("id") = 5315) Then
                ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico_SUNE", "TO", 0), "codigo_cac", "descripcion_cac")
            Else
                ClsFunciones.LlenarListas(Me.dpCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            End If


            tbl.Dispose()
            obj = Nothing
            Me.dpCiclo.SelectedIndex = 1
        End If
    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Me.grwCursosProgramados.DataBind()
        If Me.dpEscuela.SelectedValue <> -2 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            Me.grwCursosProgramados.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 1, Me.dpEscuela.SelectedValue, Me.dpCiclo.SelectedValue, 0, 0)
            Me.grwCursosProgramados.DataBind()
            obj = Nothing
            Me.lblmensaje.Text = contador.ToString & " grupos horario programados."
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwCursosProgramados.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwCursosProgramados)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=RpteCursosProgramados" & Me.dpCiclo.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
