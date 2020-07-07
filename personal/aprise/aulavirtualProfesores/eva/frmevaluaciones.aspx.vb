
Partial Class frmevaluaciones
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If IsPostBack = False Then
            Me.cmdEliminar.Visible = False
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            Dim Tbl As New Data.DataTable
            Session("codigo_usu2") = Request.QueryString("idusuario")
            Session("idvisita2") = Request.QueryString("idvisita")
            Session("idcursovirtual2") = Request.QueryString("idcursovirtual")

            Tbl = obj.TraerDataTable("ConsultarCursoVirtual", 17, Session("codigo_usu2"), Session("idcursovirtual2"), 0)

            If Tbl.Rows(0).Item("LlenarNotas_cv") = 1 Then Me.cmdAgregar.Visible = True
            If Tbl.Rows(0).Item("ModificarNotas_cv") = 1 Then Me.cmdModificar.Visible = True
            ' If Tbl.Rows(0).Item("EliminarNotas_cv") = 1 Then Me.cmdEliminar.Visible = True

            ClsFunciones.LlenarListas(Me.dtcodigo_aev, obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 1, "N", Session("idcursovirtual2"), ""), "codigo_aev", "descripcion_aev", "--Seleccione el item de evaluación--")

        End If
    End Sub

    Protected Sub dtcodigo_aev_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtcodigo_aev.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
        Dim datos As Data.DataTable
        If dtcodigo_aev.SelectedValue > -1 Then
            datos = obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 6, Me.dtcodigo_aev.SelectedValue, "", "")
            Me.hdtipoval_aev.Value = datos.Rows(0).Item("tipoval_aev").ToString
            Me.hdlimiteval_aev.Value = datos.Rows(0).Item("limiteval_aev").ToString
            datos = Nothing
            datos = obj.TraerDataTable("DI_ConsultarEvaluacionParticipante", 2, Session("idcursovirtual2"), Me.dtcodigo_aev.SelectedValue, "")
            Me.GridView1.DataSource = datos
            Me.GridView1.DataBind()
            datos = Nothing
            Me.GridView1.Visible = True
            Me.cmdGuardar.Visible = (Me.cmdModificar.Visible = True) ' And Me.cmdEliminar.Visible = True)
        Else
            Me.cmdGuardar.Visible = False
            Me.cmdModificar.Visible = False
            Me.cmdEliminar.Visible = False
            Me.GridView1.Visible = False
        End If
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim fila As Data.DataRowView
            Dim CajaTexto As New TextBox
            'fila = e.Row.DataItem

            CajaTexto.ID = "Calificacion_eva"

            If Me.hdtipoval_aev.Value.Trim = "C" Then
                CType(CajaTexto, TextBox).Attributes.Add("onkeypress", "validarnumero()")
                CType(CajaTexto, TextBox).Attributes.Add("onkeyup", "validarnota(this)")
                CType(CajaTexto, TextBox).MaxLength = 2
                CType(CajaTexto, TextBox).Width = 30
            Else
                CType(CajaTexto, TextBox).Width = 60
            End If
            CajaTexto.Text = e.Row.Cells(4).Text
            e.Row.Cells(4).Controls.Add(CajaTexto)
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim CajaTexto As New TextBox

            e.Row.Attributes.Add("onMouseOver", "Resaltar(0,this)")
            e.Row.Attributes.Add("onMouseOut", "Resaltar(1,this)")
            CajaTexto.ID = "Calificacion_eva"
            If Me.hdtipoval_aev.Value.Trim = "C" Then
                CType(CajaTexto, TextBox).Attributes.Add("onkeypress", "validarnumero()")
                CType(CajaTexto, TextBox).Attributes.Add("onkeyup", "validarnota(this)")
                CType(CajaTexto, TextBox).MaxLength = 2
                CType(CajaTexto, TextBox).Width = 30
            Else
                CType(CajaTexto, TextBox).Width = 60
            End If
            CajaTexto.Text = e.Row.Cells(4).Text 'Trim(Server.HtmlDecode(e.Row.Cells(14).Text))
            e.Row.Cells(4).Controls.Add(CajaTexto)
        End If
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim operador As String
        operador = Session("codigo_usu2")

        If Me.GridView1.Rows.Count > 0 Then
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXCMUSAT").ConnectionString)
            Try
                obj.IniciarTransaccion()

                For j As Integer = 0 To Me.GridView1.Rows.Count
                    For i As Integer = 0 To GridView1.Controls(0).Controls(j).Controls(4).Controls.Count - 1
                        Dim ControlTexto As TextBox
                        Dim celda As TableCell
                        celda = GridView1.Controls(0).Controls(j).Controls(1)
                        'Response.Write(celda.Text)

                        ControlTexto = GridView1.Controls(0).Controls(j).Controls(4).Controls(i)

                        ' El numero de la solicitud es celda.text
                        obj.Ejecutar("DI_AgregarEvaluacionParticipante", celda.Text, Me.dtcodigo_aev.SelectedValue, ControlTexto.Text, operador)
                        'Response.Write(celda.Text & "--" & Me.dtcodigo_aev.SelectedValue & "--" & ControlTexto.Text & "--" & operador)
                    Next
                Next
                obj.TerminarTransaccion()
                obj = Nothing
            Catch ex As Exception
                obj.AbortarTransaccion()
                obj = Nothing
                Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos')</SCRIPT>")
                'Response.Write(ex.Message)
            End Try
            Response.Write("<SCRIPT>alert('Se actualizaron los datos correctamente'); </SCRIPT>")
        Else
            Response.Write("<script>alert('No hay registros para guardar')</script>")
        End If
    End Sub

    Protected Sub cmdAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAgregar.Click
        Response.Redirect("frmactvevaluacion.aspx?tiporeg_aev=N&accion=A&codigo_aev=0")
    End Sub

    Protected Sub cmdModificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdModificar.Click
        Response.Redirect("frmactvevaluacion.aspx?tiporeg_aev=N&accion=M&codigo_aev=" & Me.dtcodigo_aev.SelectedValue)
    End Sub

    Protected Sub cmdReporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReporte.Click
        Response.Redirect("rpteevaluacionparticipante.aspx")
    End Sub

    Protected Sub cmdEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdEliminar.Click

    End Sub
End Class
