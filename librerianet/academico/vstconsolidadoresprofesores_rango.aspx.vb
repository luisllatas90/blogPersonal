
Partial Class academico_vstconsolidadoresprofesores_rango
    Inherits System.Web.UI.Page

    Dim PersonalAnterior As Int32 = -1
    Dim Horas As Int32 = 0
    Dim PrimeraFila As Int32 = -1
    Dim Contador As Int32 = 0
    Dim total As Int32 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") Is Nothing Then
            Response.Redirect("../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
            Dim id As String

            id = Session("id_per")

            ClsFunciones.LlenarListas(Me.dpDpto, obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 0, id, 0, 0, 0), "codigo_dac", "nombre_dac", ">>Seleccione el Dpto. Académico<<")
            ClsFunciones.LlenarListas(Me.dpCicloFin, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            ClsFunciones.LlenarListas(Me.dpCicloIni, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            'Session("codigo_usu2") = Session("id_per")
            obj = Nothing
        End If
    End Sub

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.GridView2.DataSource = obj.TraerDataTable("ConsultarConsolidadoResponsabilidadesProfesores_Rango", 0, Me.dpCicloIni.SelectedValue, Me.dpCicloFin.SelectedValue, Me.dpDpto.SelectedValue)
        Me.GridView2.DataBind()

        Me.GridView1.DataSource = obj.TraerDataTable("ConsultarConsolidadoResponsabilidadesProfesores_Rango", 1, Me.dpCicloIni.SelectedValue, Me.dpCicloFin.SelectedValue, Me.dpDpto.SelectedValue)
        Me.GridView1.DataBind()
        obj = Nothing

        Me.cmdExportar.Visible = Me.GridView1.Rows.Count > 0
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            Contador = Contador + 1
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            'e.Row.Cells(10).Text = CInt(fila("horasdiarias_ded")) - CInt(fila("totalHoras_Car"))

            'Combinar celdas
            ''If PersonalAnterior = fila("codigo_per") Then
            ''    'Incrementar the fila combinada
            ''    'If Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan = 0 Then
            ''    '    Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan = 2
            ''    'Else
            ''    '    Me.GridView1.Rows(PrimeraFila).Cells(2).RowSpan += 1
            ''    '    'Quitar la celda
            ''    '    e.Row.Cells.RemoveAt(2)
            ''    'End If
            ''    e.Row.Cells(0).Text = ""
            ''    e.Row.Cells(1).Text = ""
            ''    e.Row.Cells(2).Text = ""
            ''    e.Row.Cells(3).Text = ""
            ''    e.Row.Cells(8).Text = ""
            ''    e.Row.Cells(9).Text = ""
            ''    e.Row.Cells(10).Text = ""
            ''    e.Row.Cells(11).Text = ""
            ''    e.Row.Cells(1).Attributes.Add("style", "cursor:default")
            ''    Contador = Contador - 1
            ''    'Acumular horas de carga académica
            ''    'Horas += fila("totalHoras_Car")
            ''Else
            ''    'Añadir evento al profesor
            ''    e.Row.Cells(1).Attributes.Add("onClick", "AbrirPopUp('../../personal/academico/expediente/consultas/personal.aspx?id=" & Me.GridView1.DataKeys(e.Row.RowIndex).Value & "','500','600','yes','yes','yes')")

            ''    'Obtener Horas admnistrativas - Horas acumuladas de clase (reiniciar acumulador de horas)
            ''    'Horas = 0
            ''    'Filas divisoras
            ''    e.Row.Cells(0).CssClass = "bordesup"
            ''    e.Row.Cells(1).CssClass = "bordesup"
            ''    e.Row.Cells(2).CssClass = "bordesup"
            ''    e.Row.Cells(3).CssClass = "bordesup"
            ''    e.Row.Cells(4).CssClass = "bordesup"
            ''    e.Row.Cells(5).CssClass = "bordesup"
            ''    e.Row.Cells(6).CssClass = "bordesup"
            ''    e.Row.Cells(7).CssClass = "bordesup"
            ''    e.Row.Cells(8).CssClass = "bordesup"
            ''    e.Row.Cells(9).CssClass = "bordesup"
            ''    e.Row.Cells(10).CssClass = "bordesup"
            ''    e.Row.Cells(11).CssClass = "bordesup"
            ''    e.Row.VerticalAlign = VerticalAlign.Middle
            ''    PersonalAnterior = fila("codigo_per").ToString()
            ''    PrimeraFila = e.Row.RowIndex

            e.Row.Cells(0).Text = Contador
            ''End If

            'If PersonalAnterior = fila("codigo_per") Then
            '    Horas += fila("totalhoras_car")
            'End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=ConsolidadoResponsabilidadesprofesores" & Me.dpCicloFin.SelectedItem.Text & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "total"))
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "TOTAL:"
            e.Row.Cells(1).Text = total.ToString
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub

 
End Class
