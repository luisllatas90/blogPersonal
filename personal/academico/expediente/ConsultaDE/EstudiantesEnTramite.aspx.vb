﻿
Partial Class EstudiantesEnTramite
    Inherits System.Web.UI.Page
    Dim NUM As Int16 = 0
    Dim Suma, aprobados, electivos As Int16

    Protected Sub CmbBuscarpor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmbBuscarpor.SelectedIndexChanged
        If CmbBuscarpor.SelectedValue = 1 Then
            Me.TxtBuscar.MaxLength = 100
        Else
            Me.TxtBuscar.MaxLength = 10
        End If
    End Sub

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.CmbBuscarpor.SelectedValue = 1 Then
            Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultarAlumno", "NOM", Me.TxtBuscar.Text.ToString.Replace(" ", "%"))
        Else
            Me.GvAlumnos.DataSource = obj.TraerDataTable("ConsultarAlumno", "COD", Me.TxtBuscar.Text.ToString.Trim.Replace(" ", "%"))
        End If

       



        Me.GvAlumnos.DataBind()
        Panel1.Visible = False
    End Sub

    Protected Sub GvAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvAlumnos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim Fila As Data.DataRowView
            Fila = e.Row.DataItem
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Attributes.Add("OnClick", "javascript:__doPostBack('GvAlumnos','Select$" & e.Row.RowIndex & "')")
            If e.Row.Cells(4).Text = 1 Then
                e.Row.Cells(4).Text = "Activo"
            Else
                e.Row.Cells(4).Text = "Inactivo"
            End If
        End If
    End Sub

    Protected Sub GvAlumnos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GvAlumnos.SelectedIndexChanged
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos As New Data.DataTable
        Dim codigo_alu As Int32, codigo_pes As Int16
        Dim Ruta As New EncriptaCodigos.clsEncripta
        'Consulta plan de estudio por codigo_univer
        codigo_alu = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(0).ToString()
        Session("codigo_alu") = codigo_alu
        codigo_pes = Me.GvAlumnos.DataKeys.Item(Me.GvAlumnos.SelectedIndex).Values(1).ToString()
        Suma = 0
        aprobados = 0
        electivos = 0
        datos = obj.TraerDataTable("GyT_ConsultarPlanEstudioMatricula_V2", "PE", codigo_alu, codigo_pes) 'Me.GvAlumnos.Rows(Me.GvAlumnos.SelectedIndex).Cells(1).Text
        With datos.Rows(0)
            If datos.Rows.Count > 0 Then
                Panel1.Visible = True
                'If .Item("planes") = 1 Then
                '    CmdVerHistorial.Visible = True
                'Else
                '    CmdVerHistorial.Visible = False
                'End If
                Me.LblCodigoUniv.Text = .Item("codigouniver_alu").ToString
                Me.LblCreditosTot.Text = .Item("Total_Creditos").ToString
                Me.LblNombres.Text = .Item("nombres").ToString
                Me.LblPlanEstudio.Text = .Item("descripcion_pes").ToString
                Me.LblCredObligatorios.Text = .Item("totalCreObl_Pes").ToString
                Me.lblCreditosEgresar.text = .Item("creditosEgresar_pes").ToString
                If .Item("foto_alu") = 1 Then
                    ImgFoto.ImageUrl = "http://www.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & .Item("codigouniver_alu").ToString)
                Else
                    ImgFoto.ImageUrl = Request.ApplicationPath & "/images/Sin_foto.jpg"
                End If
                'Session("codigo_alu") = datos.Rows(0).Item("codigo_alu").ToString
                Me.Panel1.Visible = True
                Me.GvPlanMatricula.DataSource = datos
                Me.GvPlanMatricula.DataBind()
                'Me.LblCreditosAprob.Text = Suma  Cambio 20.11.18
                Me.LblCreditosAprob.Text = datos.Rows(0).Item("xCreditosAprob").ToString
                Me.LblNroCursosAprobados.Text = aprobados
                Me.LblNroCursosPlan.Text = NUM
                Me.LblCredElectivos.Text = electivos  '.Item("totalCredElecObl_Pes").ToString
                'Me.GvPlanMatricula.DataSource = obj.TraerDataTable("GyT_ConsultarPlanEstudioMatricula", "CM", Session("codigo_alu"))
            End If
        End With
    End Sub

    Protected Sub GvPlanMatricula_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvPlanMatricula.RowDataBound
        Dim celda As New TableCell
        Dim img, imagen As New Image
        Dim fila As Data.DataRowView

        e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
        e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

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
                    .Cells(2).ToolTip = "Equivalente con: " + fila.Row("nombre_curE") + " (" + fila.Row("PlanEscuelaE") + " )"
                    '.Cells(2).Text = "<a href='frmVerCargosAbonos.aspx?nombre_curE=" & fila.Row("nombre_curE") & "&KeepThis=true&TB_iframe=true&height=400&width=700&modal=true' title='MostrarEquivalencia' class='thickbox'>&nbsp;<img src='../../../images/previo.gif' border=0 /><a/>"

                End If

            End If
        End With
       

    End Sub


    Protected Sub CmdVerHistorial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdVerHistorial.Click
        'response.redirect("http://www.usat.edu.pe/campusvirtual/librerianet/academico/historial.aspx?ctf=1&id=" & Session("codigo_alu"))
        ClientScript.RegisterStartupScript(Me.GetType, "Historial", "window.open('http://www.usat.edu.pe/campusvirtual/librerianet/academico/historial.aspx?ctf=1&id=" & Session("codigo_alu") & "')", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("id_per") = "" Then
            Response.Redirect("../../../../sinacceso.html")
        End If
    End Sub
End Class
