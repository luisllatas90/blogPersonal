
Partial Class academico_horarios_administrar_frmRptAmbientesxFecha
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlAmbiente.DataSource = obj.TraerDataTable("Ambiente_ConsultarAmbiente", "T")
            Me.ddlAmbiente.DataTextField = "Ambiente"
            Me.ddlAmbiente.DataValueField = "codigo_amb"
            Me.ddlAmbiente.DataBind()

            Me.ddlCco.DataSource = obj.TraerDataTable("HorariosPE_ConsultarCcoSol", "T")
            Me.ddlCco.DataTextField = "descripcion_Cco"
            Me.ddlCco.DataValueField = "codigo_cco"
            Me.ddlCco.DataBind()

            ClsFunciones.LlenarListas(Me.ddlTipoEstudio, obj.TraerDataTable("AsignarAmbiente_ListarTipoEstudio", 2), "codigo_test", "descripcion_test")
            Me.ddlTipoEstudio.SelectedValue = -1
            obj.CerrarConexion()
            obj = Nothing
            Me.txtDesde.Value = DateSerial(Now.Date.Year, Now.Month, 1)
            Me.txtHasta.Value = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)
            
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("HorarioPE_ConsultarHorario", Me.ddlAmbiente.SelectedValue, Me.txtDesde.Value, Me.txtHasta.Value, Me.ddlCco.SelectedValue, Me.ddlEstado.SelectedValue, Me.ddlTipoEstudio.SelectedValue, CInt(Me.checkPreferencial.Checked) * -1)
        Me.gridAmbientes.DataSource = tb
        Me.gridAmbientesExportar.DataSource = tb

        'Response.Write(Me.ddlAmbiente.SelectedValue & "," & Me.txtDesde.Value & "," & Me.txtHasta.Value & "," & Me.ddlCco.SelectedValue & "," & Me.ddlEstado.SelectedValue & "," & Me.ddlTipoEstudio.SelectedValue & "," & CInt(Me.checkPreferencial.Checked))

        Me.gridAmbientes.DataBind()
        Me.gridAmbientesExportar.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridAmbientes.PageIndexChanging
        Me.gridAmbientes.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound



        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = "1" Then
                e.Row.Cells(0).Text = "<img src='images/star.png' title='Ambiente preferencial'>"
            Else
                e.Row.Cells(0).Text = "<img src='images/door.png' title=''>"
            End If


            For i As Integer = 2 To Me.gridAmbientes.Columns.Count
                With e.Row.Cells(i - 1)
                    If e.Row.Cells(11).Text = "ASIGNADO" Then
                        Select Case gridAmbientes.DataKeys(e.Row.RowIndex).Values("codigo_test")
                            Case 0
                                .BackColor = Drawing.Color.FromArgb(146, 208, 80)
                            Case 1
                                .BackColor = Drawing.Color.FromArgb(217, 149, 148)
                            Case 2
                                .BackColor = Drawing.Color.FromArgb(255, 192, 0)
                            Case 3
                                .BackColor = Drawing.Color.FromArgb(178, 161, 199)
                            Case 4
                                .BackColor = Drawing.Color.FromArgb(255, 192, 0)
                            Case 5
                                .BackColor = Drawing.Color.FromArgb(146, 205, 220)
                            Case 6
                                .BackColor = Drawing.Color.FromArgb(148, 138, 84)
                            Case 7
                                .BackColor = Drawing.Color.FromArgb(84, 141, 212)
                            Case 8
                                .BackColor = Drawing.Color.FromArgb(227, 108, 10)
                        End Select
                    Else
                        .BackColor = Drawing.Color.FromArgb(232, 232, 232)
                    End If


                End With
            Next

        End If


    End Sub

  
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Me.gridAmbientesExportar.Visible = True
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gridAmbientesExportar)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Consulta_de_Ambientes_por_Fecha" & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
        Me.gridAmbientesExportar.Visible = False
    End Sub

    Protected Sub gridAmbientesExportar_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientesExportar.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = "1" Then
                e.Row.Cells(0).Text = "Preferencial"
            Else
                e.Row.Cells(0).Text = "No Preferencial"
            End If

            For i As Integer = 2 To Me.gridAmbientesExportar.Columns.Count
                With e.Row.Cells(i - 1)
                    If e.Row.Cells(9).Text = "ASIGNADO" Then
                        Select Case gridAmbientesExportar.DataKeys(e.Row.RowIndex).Values("codigo_test")
                            Case 0
                                .BackColor = Drawing.Color.FromArgb(146, 208, 80)
                            Case 1
                                .BackColor = Drawing.Color.FromArgb(217, 149, 148)
                            Case 2 Or 4
                                .BackColor = Drawing.Color.FromArgb(255, 192, 0)
                            Case 3
                                .BackColor = Drawing.Color.FromArgb(178, 161, 199)
                            Case 5
                                .BackColor = Drawing.Color.FromArgb(146, 205, 220)
                            Case 6
                                .BackColor = Drawing.Color.FromArgb(148, 138, 84)
                            Case 7
                                .BackColor = Drawing.Color.FromArgb(84, 141, 212)
                            Case 8
                                .BackColor = Drawing.Color.FromArgb(227, 108, 10)
                        End Select
                    Else
                        .BackColor = Drawing.Color.FromArgb(232, 232, 232)
                    End If


                End With
            Next


        End If


    End Sub
End Class
