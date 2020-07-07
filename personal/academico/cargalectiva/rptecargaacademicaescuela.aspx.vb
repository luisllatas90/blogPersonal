
Partial Class rptecargaacademicaescuela
    Inherits System.Web.UI.Page
    Dim codigo_cup As Int32 = -1
    Dim contador As Int16 = 0
    Dim PrimeraFila As Int16 = -1

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this)")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this)")

            contador = contador + 1
            'Combinar celdas
            If codigo_cup = fila("codigo_cup") Then
                e.Row.Cells(0).Text = ""
                e.Row.Cells(1).Text = ""
                e.Row.Cells(2).Text = ""
                e.Row.Cells(3).Text = ""
                e.Row.Cells(4).Text = ""
                contador = contador - 1
            Else
                e.Row.Cells(0).CssClass = "bordesup"
                e.Row.Cells(1).CssClass = "bordesup"
                e.Row.Cells(2).CssClass = "bordesup"
                e.Row.Cells(3).CssClass = "bordesup"
                e.Row.Cells(4).CssClass = "bordesup"
                e.Row.VerticalAlign = VerticalAlign.Middle
                codigo_cup = fila("codigo_cup").ToString()
                PrimeraFila = e.Row.RowIndex

                'e.Row.Cells(0).Text = contador
            End If
            'Asignar linea separadora
            ' e.Row.Cells(4).CssClass = "bordesup" 'Autor
            e.Row.Cells(5).CssClass = "bordesup" 'Estado
            e.Row.Cells(6).CssClass = "bordesup"
            e.Row.Cells(7).CssClass = "bordesup"
            e.Row.Cells(8).CssClass = "bordesup"
            e.Row.Cells(9).CssClass = "bordesup"
            e.Row.Cells(10).CssClass = "bordesup"
            e.Row.Cells(11).CssClass = "bordesup"

            '=================================================================
            'Sólo se asigna horas, cuando se haya asignado la CARGA ACADEMICA
            '=================================================================
            If fila.Row("totalHorasAula").ToString <> "" Then
                e.Row.Cells(5).CssClass = "lineaprofesor"
                e.Row.Cells(6).CssClass = "lineaprofesor"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim tbl As Data.DataTable
        Try
            If IsPostBack = False Then
                Dim codigo_tfu As Int16 = Request.QueryString("ctf")
                Dim codigo_usu As Integer = Request.QueryString("id")                
                If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                    If Request.QueryString("mod") = 10 Then '
                        tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "GO", 0) '
                    ElseIf Request.QueryString("mod") = 3 Then '
                        tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "PP", 0)
                    Else
                        tbl = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
                    End If '

                Else
                    tbl = obj.TraerDataTable("consultaracceso", "ESC", Request.QueryString("mod"), codigo_usu)
                End If
                tbl.Dispose()

                ClsFunciones.LlenarListas(Me.dpEscuela, tbl, "codigo_cpf", "nombre_cpf", "--Seleccione--")
                ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "to", 0), "codigo_cac", "descripcion_cac")
                obj = Nothing
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        If Me.dpEscuela.SelectedValue <> -1 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Me.GridView1.DataSource = obj.TraerDataTable("CAR_ConsultarProcesoCargaAcademica", 9, Me.dpCodigo_cac.SelectedValue, Me.dpEscuela.SelectedValue, Me.dpFiltro.SelectedValue, 0)
            Me.GridView1.DataBind()
            obj = Nothing
            Me.GridView1.Visible = True
            Me.lblmensaje.Text = contador & " cursos programados."
        Else
            Me.lblmensaje.Text = "0 cursos programados."
            Me.GridView1.Visible = False
        End If
    End Sub

    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView1.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView1)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=InformeDeCargaAcademicaEnProceso" & Me.dpCodigo_cac.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub


End Class
