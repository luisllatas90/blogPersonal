
Partial Class academico_frmCursosFaltantes
    Inherits System.Web.UI.Page

    Dim NUM As Int16 = 1
    Dim Suma, aprobados, electivos As Int16

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Request.QueryString("id") IsNot Nothing) Then
                Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
                Dim dt As New Data.DataTable
                dt = obj.TraerDataTable("ACAD_DatosAlumno", Request.QueryString("id"))

                If (dt.Rows.Count > 0) Then
                    'Datos para Grid
                    Dim datos As New Data.DataTable
                    datos = obj.TraerDataTable("GyT_ConsultarPlanEstudioMatricula_V2", "PE", Request.QueryString("id"), dt.Rows(0).Item("codigo_Pes"))
                    Me.GvPlanMatricula.DataSource = datos
                    Me.GvPlanMatricula.DataBind()
                End If
            End If            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GvPlanMatricula_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvPlanMatricula.RowDataBound
        Dim celda As New TableCell
        Dim img, imagen As New Image
        Dim fila As Data.DataRowView

        fila = e.Row.DataItem ' Toma valores de la base

        With e.Row
            If .RowType = DataControlRowType.DataRow Then
                'Toma valores de la celda
                .Cells(2).HorizontalAlign = HorizontalAlign.Left
                .Cells(1).HorizontalAlign = HorizontalAlign.Center
                .Cells(0).Text = e.Row.RowIndex + 1 ' NUM
                If Val(fila.Item("ciclo_cur")) Mod 2 = 0 Then
                    .BackColor = Drawing.Color.Azure
                End If
                ''Verifica nombre si es complementario
                'If fila.Item("nombre_cur").ToString.Contains("(Comp.)") Then
                '    .Cells(2).ForeColor = Drawing.Color.Green
                '    'fila.Item("nombre_cur").ToString.Replace("(Comp.)", "CC")
                'End If

                'Verifica si es complementario
                If fila.Item("tipo_cur").ToString = "CC" Or fila.Item("tipo_cur").ToString = "CO" Then
                    .Cells(2).ForeColor = Drawing.Color.Green
                    .Cells(2).Text = fila.Item("nombre_cur").ToString + " - (Comp.)"
                Else
                    .Cells(7).ForeColor = Drawing.Color.Black
                End If

                '::::Cursos Convalidados:::
                If fila.Item("tipomatricula_dma").ToString = "C" Then
                    img.ImageUrl = "../images/bola_amar.gif"
                    Suma += fila.Item("creditos_cur").ToString  'Total de creditos aprobados
                    aprobados += 1
                ElseIf fila.Item("condicion_dma").ToString = "P" Then
                    '::::Cursos Matriculados:::
                    img.ImageUrl = "../images/bola_azul.gif"
                ElseIf fila.Item("falta_curso") = 0 Or fila.Item("falta_curso") = fila.Item("codigo_cur") Then
                    '::::Cursos Aprobados:::
                    If fila.Item("notafinal_dma").ToString <> 0 Then
                        img.ImageUrl = "../images/bola_verde.gif"
                        Suma += fila.Item("creditos_cur").ToString
                        aprobados += 1
                    Else
                        '::::Cursos electivos:::
                        If fila.Item("electivo_cur") = True Then
                            img.ImageUrl = "../images/bola_naranja.gif"
                        Else
                            img.ImageUrl = "../images/bola_roja.gif"
                        End If
                    End If
                Else
                    '::::Cursos Faltantes - Electivos:::
                    If fila.Item("electivo_cur") = True Then
                        img.ImageUrl = "../images/bola_naranja.gif"
                    Else
                        img.ImageUrl = "../images/bola_roja.gif"
                    End If
                End If
                If fila.Item("notafinal_dma") = 0 Then
                    .Cells(6).Text = "-"
                End If
                .Cells(7).Controls.Add(img)
                NUM += 1
                If fila.Item("electivo_cur") = True Then
                    imagen.ImageUrl = "../images/check_m.jpg"
                    .Cells(5).Controls.Add(imagen)
                    electivos += 1
                Else
                    .Cells(5).Text = ""
                End If

                If fila.Item("nombre_curE") <> "" And fila.Item("nombre_curE") <> fila.Item("nombre_cur") Then
                    .Cells(2).Text = .Cells(2).Text + " ≈"
                    .Cells(2).ToolTip = "Equivalente con: " + fila.Row("nombre_curE")
                    '.Cells(2).Text = "<a href='frmVerCargosAbonos.aspx?nombre_curE=" & fila.Row("nombre_curE") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=true' title='MostrarEquivalencia' class='thickbox'>&nbsp;<img src='../../../images/previo.gif' border=0 /><a/>"

                End If

            End If
        End With
    End Sub
End Class
