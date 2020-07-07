﻿
Partial Class academico_cargalectiva_consultapublica_estimarvacantesgw
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If

        If Not Page.IsPostBack Then
            fnLoading(False)
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones

            objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ACAD_BuscaCarreraProfesionalv2", "1", 0, "", 0), "codigo_Cpf", "nombre_Cpf")
            objFun.CargarListas(Me.ddlCiclo, obj.TraerDataTable("ConsultarCicloAcademico", "TO", ""), "codigo_cac", "descripcion_cac")
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
            obj.CerrarConexion()
            obj = Nothing
            fnLoading(True)
            CargaPlan()


        End If
    End Sub

    Protected Sub Tab1_Click(ByVal sender As Object, ByVal e As EventArgs)

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',3000);", True)
    End Sub
    Protected Sub Tab2_Click(ByVal sender As Object, ByVal e As EventArgs)

        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',7000);", True)
    End Sub

    Private Sub fnLoading(ByVal sw As Boolean)
        If sw Then
            ' Response.Write(1 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "hidden")
        Else
            ' Response.Write(0 & "<br>")
            Me.loading.Attributes.Remove("class")
            Me.loading.Attributes.Add("class", "")
            ' Me.loading.Attributes.Add("class", "show")
        End If
    End Sub

    Sub CargaPlan()
        Try
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Dim objFun As New ClsFunciones

            objFun.CargarListas(Me.ddlPlan, obj.TraerDataTable("ConsultarPlanEstudio", "AC", Me.ddlEscuela.SelectedValue.ToString, "2"), "codigo_pes", "descripcion_pes")

            obj.CerrarConexion()
            obj = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub

    Protected Sub ddlEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEscuela.SelectedIndexChanged
        CargaPlan()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        fnLoading(False)
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        Dim Fila As Integer
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        ' tb = obj.TraerDataTable("acad_consolidadoxescuela_padres", "P", Me.ddlCiclo.selectedvalue, Me.ddlPlan.SelectedValue)
        If Me.ddlEscuela.Items(Me.ddlEscuela.SelectedIndex).ToString().Contains("DEPARTAMENTO") Then
            tb = obj.TraerDataTable("cursosestimarvacantes", Me.ddlPlan.SelectedValue, Me.ddlCiclo.SelectedValue, "TEO")
        Else
            tb = obj.TraerDataTable("cursosestimarvacantes", Me.ddlPlan.SelectedValue, Me.ddlCiclo.SelectedValue, Me.ddlEscuela.SelectedValue)
        End If

        If tb.Rows.Count > 0 Then
            btnExportar.Visible = True
        Else
            btnExportar.Visible = False
        End If


        Me.gData.DataSource = tb

        Me.gData.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        CargaElectivos()
        fnLoading(True)
    End Sub


    Sub CargaElectivos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("cursosestimarvacanteselectivos", "", Me.ddlPlan.SelectedValue, 0, Me.ddlCiclo.SelectedValue)

        gdElectivo.DataSource = tb
        gdElectivo.DataBind()

        obj.CerrarConexion()
        tb = Nothing
        obj = Nothing

        gdElectivo.HeaderStyle.Font.Bold = True

    End Sub

    Protected Sub gdElectivo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gdElectivo.RowCommand
        Try

        
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Select Case e.CommandName.ToLower()
                Case "ver"
                    ' Button1.Attributes.Add("onclick", "alert('hello, world')")
                    liReporte.Attributes.Remove("class")
                    liElectivo.Attributes.Add("class", "active")
                    reporte.Attributes.Remove("class")
                    electivo.Attributes.Remove("class")
                    reporte.Attributes.Add("class", "tab-pane")
                    electivo.Attributes.Add("class", "tab-pane active")


                    hTituloElectivoDet.InnerHtml = "CICLO : " & gdElectivo.DataKeys(index).Values("CICLO").ToString
                    'Response.Write(gdElectivo.DataKeys(index).Values("codigo_pes").ToString)
                    Dim obj As New ClsConectarDatos
                    Dim tb As New Data.DataTable

                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                    obj.AbrirConexion()

                    tb = obj.TraerDataTable("cursosestimarvacanteselectivosdetalle", "", gdElectivo.DataKeys(index).Values("codigo_pes"), 0, 0, gdElectivo.DataKeys(index).Values("CICLO"))

                    Dim cols As Integer = tb.Columns.Count
                    Dim rows As Integer = tb.Rows.Count
                    gdDetalleElectivo.DataSource = tb
                    gdDetalleElectivo.DataBind()

                    CalcularTotalesElectivos(cols, rows, tb)
                    obj.CerrarConexion()
                    tb = Nothing
                    obj = Nothing

                    gdDetalleElectivo.HeaderStyle.Font.Bold = True

                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ModalElectivo", "openModalElectivo();", True)
                    Exit Select
                Case Else
                    Exit Select
            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

   
    Sub CalcularTotalesElectivos(ByVal col As Integer, ByVal row As Integer, ByVal tb As Data.DataTable)
        Try

            'Response.Write(gdDetalleElectivo.Columns.Count)
            'Dim col As Integer = 0
            'col = gdDetalleElectivo.Columns.Count
            'Dim row As Integer = 0
            'row = gdDetalleElectivo.Rows.Count
            Dim total As Integer = 0
            Dim i As Integer = 0
            Dim j As Integer = 0
            ' Dim imageName As String = "accept.png"
            With tb

                For i = 0 To row - 1
                    For j = 1 To col - 1

                        ' Response.Write(.Rows(i).Cells(j).Text)
                        If IsDBNull(.Rows(i).Item(j)) Then
                            total = total + 0
                        Else
                            total = total + CInt(.Rows(i).Item(j))
                            '                            gdDetalleElectivo.Rows(i).Cells(j).Attributes.Add("Style", (Convert.ToString("background-image: url('images/") & imageName) + "');")

                            gdDetalleElectivo.Rows(i).Cells(j).Attributes.Add("Style", "text-align:center;FONT-WEIGHT: bold")

                            gdDetalleElectivo.Rows(i).Cells(j).Text = "X"
                        End If
                        ' lbl2.Text = lbl2.Text & " " & CInt(.Rows(i).Item(j))
                        ' total = total + CInt(.Rows(i).Cells(j).Text)
                        '   total = total + CDec(.Rows(i).Cells(j).Text)
                    Next
                    'lbl2.Text = total.ToString & "<br>"
                    ' lbl2.Text = lbl2.Text & "<br>"
                    '.Rows(i).Cells(col - 1).Text = total
                    gdDetalleElectivo.Rows(i).Cells(col - 1).Text = total
                    gdDetalleElectivo.Rows(i).Cells(col - 1).Attributes.Add("Style", "text-align:center;FONT-WEIGHT: bold")
                   '.Rows(0).Item(col - 1).Text = "0"
                    total = 0

                Next
                '.Rows(0).Cells(col - 1).Text = "0"
            End With

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
  

    Protected Sub gdElectivo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdElectivo.RowDataBound

        'e.Row.Cells(1).Width = 0
        e.Row.Cells(1).Visible = False
        'If e.Row.DataItem IsNot Nothing Then
        '    Dim lb As New LinkButton()
        '    lb.CommandArgument = e.Row.RowIndex
        '    lb.CommandName = "numclick"
        '    lb.Text = "Ver"
        '    lb.CssClass = "btn btn-primary"
        '    lb.OnClientClick = "fnDivLoad('report', 7000);"
        '    e.Row.Cells(0).Controls.Add(DirectCast(lb, Control))
        'End If


    End Sub



    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.gData.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.gData)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Estimacion_de_Vacantes.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default        
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(sb.ToString())
        Response.End()
    End Sub


    

    Protected Sub gData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gData.RowCommand

        Try

            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim st0 As New StringBuilder
            Dim st As New StringBuilder
            Dim st2 As New StringBuilder
            Dim st22 As New StringBuilder
            Dim st3 As New StringBuilder

            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "DivLoad", "fnDivLoad('report',1000);", True)
            If (e.CommandName = "Ver") Then
                fnLoading(False)
                Me.hTituloDet.InnerHtml = "Curso: " & gData.DataKeys(index).Values("nombre_cur").ToString

                'Response.Write(CInt(gData.DataKeys(index).Values("codigo_pes").ToString))
                'Response.Write(CInt(gData.DataKeys(index).Values("codigo_cpf").ToString))
                'Response.Write(CInt(gData.DataKeys(index).Values("codigo_cur").ToString))
                'Response.Write(CInt(gData.DataKeys(index).Values("codigo_cac").ToString))

                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "Pop", "openModal();", True)

                Dim obj As New ClsConectarDatos

                Dim tb0 As New Data.DataTable

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                tb0 = obj.TraerDataTable("EstimacionVacantesDetalle", "4", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))
                st0.Append("")
  



         


                For i As Integer = 0 To tb0.Rows.Count - 1


                    'stpes.Append("<tr>")
                    'stpes.Append("<td>")
                    'stpes.Append(pes.ToString)
                    'stpes.Append("</td>")
                    'stpes.Append("<td>")
                    'stpes.Append(cpes)
                    'stpes.Append("</td>")
                    'stpes.Append("</tr>")
                    'pes = tb0.Rows(i).Item("descripcion_Pes").ToString


                    st0.Append("<tr>")
                    st0.Append("<td>")
                    st0.Append(tb0.Rows(i).Item("codigoUniver_Alu").ToString)
                    st0.Append("</td>")
                    st0.Append("<td>")
                    st0.Append(tb0.Rows(i).Item("Estudiante").ToString)
                    st0.Append("</td>")
                    st0.Append("<td>")
                    st0.Append(tb0.Rows(i).Item("cicloIng_Alu").ToString)
                    st0.Append("</td>")
                    st0.Append("<td>")
                    st0.Append(tb0.Rows(i).Item("descripcion_Pes").ToString)
                    st0.Append("</td>")
                    st0.Append("</tr>")
                Next

                Dim stpes As New StringBuilder
                Dim tbPes As New Data.DataTable
                tbPes = obj.TraerDataTable("EstimacionVacantesDetalle", "5", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))

                For i As Integer = 0 To tbPes.Rows.Count - 1
                    stpes.Append("<tr>")
                    stpes.Append("<td>")
                    stpes.Append(tbPes.Rows(i).Item("descripcion_Pes").ToString)
                    stpes.Append("</td>")
                    stpes.Append("<td>")
                    stpes.Append(tbPes.Rows(i).Item("cantidad").ToString)
                    stpes.Append("</td>")
                    stpes.Append("</tr>")


                Next
                Me.tbdMatriculadoPE.InnerHtml = stpes.ToString



                Me.tbdMatriculado.InnerHtml = st0.ToString
                Me.lblmat.InnerHtml = tb0.Rows.Count.ToString




                tb0 = Nothing

                'Detalle cantidad alumno por plan de estudio Cursos Requsitos

                Dim stcrpes As New StringBuilder
                Dim tbcrPes As New Data.DataTable
                tbcrPes = obj.TraerDataTable("EstimacionVacantesDetalle", "6", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))

                For i As Integer = 0 To tbcrPes.Rows.Count - 1
                    stcrpes.Append("<tr>")
                    stcrpes.Append("<td>")
                    stcrpes.Append(tbcrPes.Rows(i).Item("descripcion_Pes").ToString)
                    stcrpes.Append("</td>")
                    stcrpes.Append("<td>")
                    stcrpes.Append(tbcrPes.Rows(i).Item("cantidad").ToString)
                    stcrpes.Append("</td>")
                    stcrpes.Append("</tr>")
                Next
                Me.tbdMatriculadoCRPE.InnerHtml = stcrpes.ToString


                ' fin





                Dim tb As New Data.DataTable

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()

                tb = obj.TraerDataTable("EstimacionVacantesDetalle", "1", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))
                st.Append("")
                For i As Integer = 0 To tb.Rows.Count - 1
                    st.Append("<tr>")
                    st.Append("<td>")
                    st.Append(tb.Rows(i).Item("codigoUniver_Alu").ToString)
                    st.Append("</td>")
                    st.Append("<td>")
                    st.Append(tb.Rows(i).Item("Estudiante").ToString)
                    st.Append("</td>")
                    st.Append("<td>")
                    st.Append(tb.Rows(i).Item("cicloIng_Alu").ToString)
                    st.Append("</td>")
                    st.Append("</tr>")
                Next

                Me.tbdAprobado.InnerHtml = st.ToString
                Me.lblap.InnerHtml = tb.Rows.Count.ToString

                tb = Nothing

                Dim tb2 As New Data.DataTable

                tb2 = obj.TraerDataTable("EstimacionVacantesDetalle", "2", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))
                st2.Append("")
                For i As Integer = 0 To tb2.Rows.Count - 1
                    st2.Append("<tr>")
                    st2.Append("<td>")
                    st2.Append(tb2.Rows(i).Item("codigoUniver_Alu").ToString)
                    st2.Append("</td>")
                    st2.Append("<td>")
                    st2.Append(tb2.Rows(i).Item("Estudiante").ToString)
                    st2.Append("</td>")
                    st2.Append("<td>")
                    st2.Append(tb2.Rows(i).Item("cicloIng_Alu").ToString)
                    st2.Append("</td>")
                    st2.Append("<td>")
                    st2.Append(tb2.Rows(i).Item("descripcion_Pes").ToString)
                    st2.Append("</td>")
                    st2.Append("</tr>")
                Next

                Me.tbdAprobadoReq.InnerHtml = st2.ToString
                Me.lblrap.InnerHtml = tb2.Rows.Count.ToString

                tb2 = Nothing

                Dim tb22 As New Data.DataTable

                tb22 = obj.TraerDataTable("EstimacionVacantesDetalle", "7", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))
                st22.Append("")
                For i As Integer = 0 To tb22.Rows.Count - 1
                    st22.Append("<tr>")
                    st22.Append("<td>")
                    st22.Append(tb22.Rows(i).Item("codigoUniver_Alu").ToString)
                    st22.Append("</td>")
                    st22.Append("<td>")
                    st22.Append(tb22.Rows(i).Item("Estudiante").ToString)
                    st22.Append("</td>")
                    st22.Append("<td>")
                    st22.Append(tb22.Rows(i).Item("cicloIng_Alu").ToString)
                    st22.Append("</td>")
                    st22.Append("<td>")
                    st22.Append(tb22.Rows(i).Item("descripcion_Pes").ToString)
                    st22.Append("</td>")
                    st22.Append("</tr>")
                Next

                Me.tbdAprobadoReq2.InnerHtml = st22.ToString
                Me.lblrap2.InnerHtml = tb22.Rows.Count.ToString

                tb22 = Nothing
















                Dim tb3 As New Data.DataTable


                tb3 = obj.TraerDataTable("EstimacionVacantesDetalle", "3", _
                                        CInt(gData.DataKeys(index).Values("codigo_pes").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cpf").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cur").ToString), _
                                        CInt(gData.DataKeys(index).Values("codigo_cac").ToString))

                st3.Append("")
                For i As Integer = 0 To tb3.Rows.Count - 1
                    st3.Append("<tr>")
                    st3.Append("<td>")
                    st3.Append(tb3.Rows(i).Item("codigoUniver_Alu").ToString)
                    st3.Append("</td>")
                    st3.Append("<td>")
                    st3.Append(tb3.Rows(i).Item("Estudiante").ToString)
                    st3.Append("</td>")
                    st3.Append("<td>")
                    st3.Append(tb3.Rows(i).Item("cicloIng_Alu").ToString)
                    st3.Append("</td>")
                    st3.Append("</tr>")
                Next
                Me.tbdAptos.InnerHtml = st3.ToString
                Me.lblapto.InnerHtml = tb3.Rows.Count.ToString

                obj.CerrarConexion()
                obj = Nothing

                fnLoading(True)


            End If
        Catch ex As Exception

        End Try



    End Sub


    Protected Sub gData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gData.RowDataBound
        Try

            e.Row.Cells(16).Visible = False 'e.Row.Cells(15).Visible = False
            e.Row.Cells(16).Width = 0 'e.Row.Cells(15).Width = 0
            If e.Row.RowIndex >= 0 Then

                ' Response.Write(e.Row.Cells(1).Text & " = " & e.Row.Cells(7).Text & "<br>")
                e.Row.Cells(6).Text = CDbl(e.Row.Cells(3).Text) * CDbl(e.Row.Cells(5).Text)
                'e.Row.Cells(7).Attributes.Add("data-toggle", "modal")
                'e.Row.Cells(7).Attributes.Add("href", "#EditModal")

                e.Row.Cells(10).Text = CDec(e.Row.Cells(8).Text) * CDec(e.Row.Cells(9).Text) 'e.Row.Cells(9).Text = CDec(e.Row.Cells(7).Text) * CDec(e.Row.Cells(8).Text)
                e.Row.Cells(11).Text = CDec(e.Row.Cells(6).Text) + CDec(e.Row.Cells(10).Text) 'e.Row.Cells(10).Text = CDec(e.Row.Cells(6).Text) + CDec(e.Row.Cells(9).Text)

                'e.Row.Cells(11).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + (CDec(e.Row.Cells(7).Text) - (CDec(e.Row.Cells(7).Text) * CDec(e.Row.Cells(8).Text))) + CDec(e.Row.Cells(10).Text)
                e.Row.Cells(13).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + ((CDec(e.Row.Cells(8).Text) * CDec(e.Row.Cells(9).Text))) + CDec(e.Row.Cells(12).Text) 'e.Row.Cells(12).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + ((CDec(e.Row.Cells(7).Text) * CDec(e.Row.Cells(8).Text))) + CDec(e.Row.Cells(11).Text)

                'e.Row.Cells(13).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + (CDec(e.Row.Cells(7).Text) - (CDec(e.Row.Cells(7).Text) * CDec(e.Row.Cells(8).Text))) + CDec(e.Row.Cells(10).Text) + CDec(e.Row.Cells(12).Text)

                e.Row.Cells(15).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + ((CDec(e.Row.Cells(8).Text) * CDec(e.Row.Cells(9).Text))) + CDec(e.Row.Cells(12).Text) + CDec(e.Row.Cells(14).Text) 'e.Row.Cells(14).Text = CDec(e.Row.Cells(3).Text) * CDec(e.Row.Cells(5).Text) + ((CDec(e.Row.Cells(7).Text) * CDec(e.Row.Cells(8).Text))) + CDec(e.Row.Cells(11).Text) + CDec(e.Row.Cells(13).Text)


                e.Row.Cells(11).BackColor = Drawing.Color.FromArgb(164, 218, 252) 'e.Row.Cells(10).BackColor = Drawing.Color.FromArgb(164, 218, 252)
                e.Row.Cells(13).BackColor = Drawing.Color.MediumAquamarine 'e.Row.Cells(12).BackColor = Drawing.Color.MediumAquamarine
                e.Row.Cells(13).ForeColor = Drawing.Color.White 'e.Row.Cells(12).ForeColor = Drawing.Color.White
                e.Row.Cells(15).BackColor = Drawing.Color.White 'e.Row.Cells(14).BackColor = Drawing.Color.White

                e.Row.Cells(11).Font.Bold = True 'e.Row.Cells(10).Font.Bold = True
                e.Row.Cells(13).Font.Bold = True 'e.Row.Cells(12).Font.Bold = True

                If CBool(e.Row.Cells(16).Text) Then 'If CBool(e.Row.Cells(15).Text) Then
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " (<b>Electivo</b>)"
                End If

                e.Row.Cells(6).Text = Math.Round(CDec(e.Row.Cells(6).Text), 2) 'e.Row.Cells(5).Text = Math.Round(CDec(e.Row.Cells(5).Text), 2)
                e.Row.Cells(7).Text = Math.Round(CDec(e.Row.Cells(7).Text), 2) 'e.Row.Cells(6).Text = Math.Round(CDec(e.Row.Cells(6).Text), 2)
                e.Row.Cells(9).Text = Math.Round(CDec(e.Row.Cells(9).Text), 2) 'e.Row.Cells(8).Text = Math.Round(CDec(e.Row.Cells(8).Text), 2)
                e.Row.Cells(10).Text = Math.Round(CDec(e.Row.Cells(10).Text), 2) 'e.Row.Cells(9).Text = Math.Round(CDec(e.Row.Cells(9).Text), 2)
                e.Row.Cells(13).Text = Math.Round(CDec(e.Row.Cells(13).Text)) 'e.Row.Cells(12).Text = Math.Round(CDec(e.Row.Cells(12).Text))
                e.Row.Cells(15).Text = Math.Round(CDec(e.Row.Cells(15).Text)) 'e.Row.Cells(14).Text = Math.Round(CDec(e.Row.Cells(14).Text))
                e.Row.Cells(11).Text = Math.Round(CDec(e.Row.Cells(11).Text), 0) 'e.Row.Cells(10).Text = Math.Round(CDec(e.Row.Cells(10).Text), 0)

                If e.Row.Cells(7).Text = e.Row.Cells(8).Text Then

                    e.Row.Cells(7).Text = "-"
                End If

            End If
        
        Catch ex As Exception

        End Try

    End Sub

    'var neca = parseFloat(data[i].necca) * parseFloat(data[i].tdca) +
    '               (
    '               parseFloat(data[i].neccapr) - (parseFloat(data[i].neccapr) * parseFloat(data[i].tdcapr))
    '               )
    '               +
    '               parseFloat(data[i].nenmap)
    '               var _neca = neca.toFixed(4);

    '               var necaMax = parseFloat(data[i].necca) * parseFloat(data[i].tdca) +
    '               (
    '               parseFloat(data[i].neccapr) - (parseFloat(data[i].neccapr) * parseFloat(data[i].tdcapr))
    '               )
    '               + parseFloat(data[i].nenmap)
    '               + parseFloat(data[i].nenmin)
    '               var _necaMax = necaMax.toFixed(4);

    Protected Sub gData_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gData.PreRender
        If gData.Rows.Count > 0 Then
            gData.UseAccessibleHeader = True
            gData.HeaderRow.TableSection = TableRowSection.TableHeader
        End If




    End Sub



    'Protected Sub itemsRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs)
    '    ScriptManager.RegisterAsyncPostBackControl(TryCast(e.Item.FindControl("whatever"), TextBox))
    'End Sub


 
    Protected Sub gdDetalleElectivo_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gdDetalleElectivo.PreRender
        If gdDetalleElectivo.Rows.Count > 0 Then
            gdDetalleElectivo.UseAccessibleHeader = True
            gdDetalleElectivo.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

End Class
