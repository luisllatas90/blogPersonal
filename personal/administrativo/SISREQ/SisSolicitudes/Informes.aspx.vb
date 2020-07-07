
Partial Class SisSolicitudes_Informes
    Inherits System.Web.UI.Page
    Private total As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim evaluacion, datos As Data.DataTable
            Dim nivelUlt As Int16
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)

            'Response.Write(Request.QueryString("numero_sol"))

            If Not (Request.QueryString("codigo_univ") Is Nothing And Request.QueryString("numero_sol") Is Nothing) Then
                Me.LblNroSolicitud.Text = Request.QueryString("numero_sol").ToString
                datos = Obj.TraerDataTable("ConsultarMovimientosAlumno", Request.QueryString("codigo_univ"), 0, 0, 0, 0, "P")
                Me.GvEstadoCue.DataSource = datos
                Me.GvEstadoCue.DataBind()
                datos.Dispose()
                datos = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 2, Request.QueryString("numero_sol").ToString, 0)

                Page.RegisterStartupScript("DivHidden", "<script>DivAnulada.style.visibility='hidden';</script>")
                If datos.Rows.Count > 0 Then
                    ':::::Solicitud Anulada:::::
                    If datos.Rows(0).Item("estado_sol") = "A" Then
                        Page.RegisterStartupScript("DivVisible", "<script>DivAnulada.style.visibility='visible';</script>")
                    Else
                        Page.RegisterStartupScript("DivHidden", "<script>DivAnulada.style.visibility='hidden';</script>")
                    End If
                    ':::::Solicitud para bachillerato::::::
                    If Request.QueryString("tas") = 18 Then
                        Me.lblNivel1.Text = "DIRECCIÓN ACADÉMICA: "
                        Me.lblNivel2.Text = "GRADOS Y TÍTULOS: "
                    Else
                        Me.lblNivel1.Text = "DIRECTOR DE ESCUELA: "
                        Me.lblNivel2.Text = "DIRECCIÓN ACADÉMICA: "
                    End If

                    nivelUlt = datos.Rows(0).Item("nivel_eva")
                    If datos.Rows(0).Item("nivel_eva") = 1 Then
                        Page.RegisterStartupScript("Habilitar1", "<script>tblDE.style.visibility='visible';</script>")
                        Page.RegisterStartupScript("Habilitar2", "<script>tblDA.style.visibility='hidden';</script>")
                        Page.RegisterStartupScript("Habilitar3", "<script>tblAG.style.visibility='hidden';</script>")
                    ElseIf datos.Rows(0).Item("nivel_eva") = 2 Then
                        If datos.Rows.Count = 1 Then
                            Page.RegisterStartupScript("Habilitar1", "<script>tblDE.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar2", "<script>tblDA.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar3", "<script>tblAG.style.visibility='hidden';</script>")
                        Else
                            Page.RegisterStartupScript("Habilitar1", "<script>tblDE.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar2", "<script>tblDA.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar3", "<script>tblAG.style.visibility='hidden';</script>")
                        End If
                    Else
                        If datos.Rows.Count = 3 Then
                            Page.RegisterStartupScript("Habilitar1", "<script>tblDE.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar2", "<script>tblDA.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar3", "<script>tblAG.style.visibility='visible';</script>")
                        Else
                            Page.RegisterStartupScript("Habilitar1", "<script>tblDE.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar2", "<script>tblDA.style.visibility='visible';</script>")
                            Page.RegisterStartupScript("Habilitar3", "<script>tblAG.style.visibility='visible';</script>")
                        End If
                    End If

                    'Response.Write(datos.Rows(0).Item("activa_eva"))
                    If datos.Rows(0).Item("activa_eva") = True Then
                        Me.CmdImprimir.Style.Remove("visibility")
                        Me.CmdImprimir.Style.Add("visibility", "visible")
                        'Page.RegisterStartupScript("Imprimir", "<script>CmdImprimir.style.visibility='visible';</script>")
                    Else
                        Me.CmdImprimir.Style.Remove("visibility")
                        Me.CmdImprimir.Style.Add("visibility", "hidden")
                        'Page.RegisterStartupScript("noImprimir", "<script>CmdImprimir.style.visibility='hidden';</script>")
                    End If
                End If
                'Muestra las evaluaciones realizadas
                evaluacion = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 1, Request.QueryString("numero_sol"), CInt(Request.QueryString("id")))
                    If evaluacion.Rows.Count > 0 And evaluacion.Columns.Count > 1 Then
                        For i As Int16 = 0 To evaluacion.Rows.Count - 1
                            Select Case evaluacion.Rows(i).Item("nivel_eva")
                            Case 1
                                'Response.Write(evaluacion.Rows(i).Item("nivel_eva").ToString)
                                Me.LblFechaDE.Text = CDate(evaluacion.Rows(i).Item("fecha_eva").ToString).ToShortDateString
                                Me.LblResultadoDE.Text = evaluacion.Rows(i).Item("descripcion_res").ToString
                                Me.TxtObservacionDE.Text = evaluacion.Rows(i).Item("observacion_eva").ToString
                                Me.LblDE.Text = evaluacion.Rows(i).Item("personal").ToString
                                If evaluacion.Rows(i).Item("test").ToString = 2 Then
                                    lblNivel1.Text = "DIRECTOR DE ESCUELA:"
                                ElseIf evaluacion.Rows(i).Item("test").ToString = 3 Then
                                    lblNivel1.Text = "COORDINADOR ACADÉMICO:"
                                Else '---- agreagr otros elseIf en caso se amplíe la opción a más tipos de estudio esaavedra 05.10.12
                                    lblNivel1.Text = "DIRECTOR DE ESCUELA:"
                                End If
                            Case 2
                                Me.LblFechaDA.Text = CDate(evaluacion.Rows(i).Item("fecha_eva").ToString).ToShortDateString
                                Me.LblResultadoDA.Text = evaluacion.Rows(i).Item("descripcion_res").ToString
                                Me.TxtObservacionDA.Text = evaluacion.Rows(i).Item("observacion_eva").ToString
                                Me.LblDA.Text = evaluacion.Rows(i).Item("personal").ToString
                            Case 3
                                Me.LblFechaAG.Text = CDate(evaluacion.Rows(i).Item("fecha_eva").ToString).ToShortDateString
                                Me.LblResultadoAG.Text = evaluacion.Rows(i).Item("descripcion_res").ToString
                                Me.TxtObservacionAG.Text = evaluacion.Rows(i).Item("observacion_eva").ToString
                                Me.LblAG.Text = evaluacion.Rows(i).Item("personal").ToString
                        End Select
                        Next
                    End If
                End If
            End If
    End Sub

    Protected Sub GvEstadoCue_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvEstadoCue.RowDataBound
        'Estado de cuenta del estudiante
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Cells(17).Text = FormatNumber(CDbl(fila.Row.Item("Saldo").ToString) + CDbl(fila.Row.Item("Mora_deu")), 2, TriState.False)
            total = total + CDbl(fila.Row.Item("Saldo").ToString) + CDbl(fila.Row.Item("Mora_deu").ToString)
        End If
        Me.LblTotal.Text = FormatNumber(total, 2, TriState.True)
    End Sub
End Class
