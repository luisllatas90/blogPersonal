
Partial Class lsttesisasesoria
    Inherits System.Web.UI.Page
    Dim tblAsesorias As Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            If IsPostBack = False Then
                Dim tbl As Data.DataTable
                ClsFunciones.LlenarListas(Me.dpFase, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 0, 0, 0, 0), "codigo_Eti", "nombre_Eti")
                tbl = obj.TraerDataTable("TES_ConsultarResponsableTesis", 6, Request.QueryString("codigo_per"), 0, 0)

                Me.lblasesor.Text = tbl.Rows(0).Item("Docente")
                Me.lblcategoria.Text = tbl.Rows(0).Item("descripcion_tpe") & " - " & tbl.Rows(0).Item("descripcion_ded")
                Me.lblemail.Text = tbl.Rows(0).Item("email_per").ToString
                Me.foto.ImageUrl = tbl.Rows(0).Item("foto_per").ToString
                BuscarTesisRegistradas(Request.QueryString("codigo_eti"))

                Me.dpFase.SelectedValue = Request.QueryString("codigo_eti")
            End If

            'Pintar Calendario con asesorías
            tblAsesorias = obj.TraerDataTable("TES_ConsultarAsesoriaTesis", 4, Request.QueryString("codigo_per"), dpFase.SelectedValue, 0, 0)
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.message)
        End Try       
    End Sub
    Private Sub BuscarTesisRegistradas(ByVal codigo_eti As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        'Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis", 7, codigo_eti, 1, Request.QueryString("codigo_per"))
        'Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis", 13, codigo_eti, 1, Request.QueryString("codigo_per"))

        '###### Modificado por mvillavicencio 23/11/2011. Mostrar tesis del ciclo correspondiente ###

        Me.GridView1.DataSource = obj.TraerDataTable("TES_ConsultarTesis_v2", 14, codigo_eti, 1, Request.QueryString("codigo_per"), Request.QueryString("codigo_Cac"))

        '#################
        Me.GridView1.DataBind()
        obj = Nothing
    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_tes").ToString & "")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub
    Protected Sub dpFase_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpFase.SelectedIndexChanged
        BuscarTesisRegistradas(Me.dpFase.SelectedValue)
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdBuscar.Click
        BuscarTesisRegistradas(Me.dpFase.SelectedValue)
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        Response.Redirect("vsttesisasesor.aspx?id=" & Session("codigo_usu2"))
    End Sub

    Protected Sub Calendar1_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles Calendar1.DayRender
        Try
            For i As Integer = 0 To tblAsesorias.Rows.Count - 1
                'If e.Day.DayNumberText = Day(tblAsesorias.Rows(i).Item("fechareg_ates")) And _
                '    e.Day.Date.Month = Month(tblAsesorias.Rows(i).Item("fechareg_ates")) And _
                '    e.Day.Date.Year = Year(tblAsesorias.Rows(i).Item("fechareg_ates")) Then
                '    'e.Cell.Controls.Add(New LiteralControl("<p>Asesoría de Tesis</p>"))
                '    e.Cell.BorderColor = Drawing.Color.Red
                '    e.Cell.BorderStyle = BorderStyle.Solid
                '    e.Cell.BackColor = Drawing.Color.Blue
                '    e.Cell.ForeColor = Drawing.Color.White
                '    e.Cell.BorderWidth = 2
                '    e.Cell.ToolTip = "Asesoría"
                '    'e.Cell.Attributes.Add("onClick", "alert('**********ASESORÍA DE TESIS********** \nTema: Definir título de investigación \nLugar: Oficina\nHora: 03:00 p.m.');return(false)")
                'End If
            Next
        Catch ex As Exception
            Response.Write(ex.message)
        End Try
        'e.Cell.VerticalAlign = VerticalAlign.Top
    End Sub
End Class
