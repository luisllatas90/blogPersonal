
Partial Class librerianet_academico_totalmatriculados
    Inherits System.Web.UI.Page
    Dim Pre As Integer
    Dim Mat As Integer
    Dim Ret As Integer
    Dim Tot As Integer
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        CargarResultados()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "TO", 0), "codigo_cac", "descripcion_cac")
            ClsFunciones.LlenarListas(Me.dpCicloIng_alu, obj.TraerDataTable("ConsultarResumenMatriculados", 0, Me.dpCodigo_cac.SelectedValue, 0, 0, 0), "cicloing_alu", "cicloing_alu", "--Todos--")

            obj = Nothing

            Me.dpCodigo_cac.SelectedValue = Request.QueryString("cac")
	    Me.dpCicloIng_alu.SelectedValue = -1

            CargarResultados()
        End If
    End Sub

    Private Sub CargarResultados()
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
	Dim obj As New ClsAccesoDatos

        Try
	
            obj.AbrirConexion()

            If right(Me.dpCodigo_cac.selecteditem.text.trim, 1) = "0" Then
                Me.GridView1.DataSource = obj.TraerDataTable("ConsultarResumenMatriculadosVerano", 3, Me.dpCodigo_cac.SelectedValue, Me.dpCicloIng_alu.SelectedValue, 0, 0)
            Else
                Me.GridView1.DataSource = obj.TraerDataTable("ConsultarResumenMatriculados", 3, Me.dpCodigo_cac.SelectedValue, Me.dpCicloIng_alu.SelectedValue, 0, 0)
            End If

            Me.GridView1.DataBind()

            obj.cerrarConexion()

            Me.GridView1.Visible = True
            Me.lblMensaje.Visible = True
            Me.lblMensaje.Text = "(*) En este reporte no se toman en cuenta las devoluciones de dinero por concepto de matrícula."

        Catch ex As Exception
            Me.lblMensaje.Text = "Este proceso está demorando. Intente denuevo."
            Me.lblMensaje.Visible = True

            Me.GridView1.Visible = False
        End Try
        obj = Nothing

    End Sub

    Protected Sub dpCodigo_cac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpCodigo_cac.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

        ClsFunciones.LlenarListas(Me.dpCicloIng_alu, obj.TraerDataTable("ConsultarResumenMatriculados", 0, Me.dpCodigo_cac.SelectedValue, 0, 0, 0), "cicloing_alu", "cicloing_alu", "--Todos--")

        obj = Nothing

    End Sub
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            CType(e.Row.FindControl("lnkMatriculados"), LinkButton).Text = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MatEscuela")) - Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RetEscuela"))

            Pre += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PreEscuela"))
            Mat += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MatEscuela"))
            Ret += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RetEscuela"))

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(0).Text = "TOTAL:"
            e.Row.Cells(1).Text = Pre.ToString
            e.Row.Cells(2).Text = Mat.ToString
            e.Row.Cells(3).Text = Ret.ToString
            e.Row.Cells(4).Text = Mat - Ret
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
        End If
    End Sub
    Protected Sub GridView2_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(6).Text = IIf(fila.Row.Item("estadoDeuda_Alu") = False, "No", "Si")
        End If
    End Sub
    Protected Sub cmdExportarLista_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportarLista.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.GridView2.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.GridView2)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=listaestudiantes_Grupo_" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub lnkPreMatriculados_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim id As Integer = Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Values("codigo_cpf"))

        Me.CargarListadoEstudiantes(id, Me.GridView1.Rows(gvr.RowIndex).Cells(0).Text, "P")
        Me.mpeFicha.Show()
    End Sub

    Protected Sub lnkMatriculados_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim id As Integer = Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Values("codigo_cpf"))

        Me.CargarListadoEstudiantes(id, Me.GridView1.Rows(gvr.RowIndex).Cells(0).Text, "M")
        Me.mpeFicha.Show()
    End Sub
    Protected Sub lnkRetirados_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmd As LinkButton = DirectCast(sender, LinkButton)
        Dim gvr As GridViewRow = DirectCast(cmd.NamingContainer, GridViewRow)
        Dim id As Integer = Convert.ToInt32(Me.GridView1.DataKeys(gvr.RowIndex).Values("codigo_cpf"))

        Me.CargarListadoEstudiantes(id, Me.GridView1.Rows(gvr.RowIndex).Cells(0).Text, "A")
        Me.mpeFicha.Show()
    End Sub

    Private Sub CargarListadoEstudiantes(ByVal id As Integer, ByVal escuela As String, ByVal estado As String)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)

        Me.GridView2.DataSource = obj.TraerDataTable("ConsultarResumenMatriculados", 4, Me.dpCodigo_cac.SelectedValue, id, estado, Me.dpCicloIng_alu.SelectedValue)
        Me.GridView2.DataBind()
        obj = Nothing
        Me.lblEscuela.Text = escuela & "(" & Me.GridView2.Rows.Count.ToString & ")"

    End Sub
End Class
