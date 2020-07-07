
Partial Class frmConsolidadoAlumnosProfesionalizacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                'Carga los programas de profesionalizacion segun el coordinador, para sistemas carga todos
                CargarCombo()
                'Consulta la vista
                ListaProgramas()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargarCombo()
        Try
            Dim obj As New clsProfesionalizacion
            Dim dts As New Data.DataTable
            dts = obj.ConsultarProgramasConfigurados(Me.Request.QueryString("ctf"), 3, Me.Request.QueryString("id"))

            If dts.Rows.Count > 0 Then
                ddlProgramas.DataSource = dts
                ddlProgramas.DataTextField = "nombre_cpf"
                ddlProgramas.DataValueField = "codigo_cpf"  '
                'ddlProgramas.DataValueField = "codigo_Cco"  'Funcionaba bien pero modificaron los cecos... y malograron x eso se cambio.
                ddlProgramas.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub ListaProgramas()
        Try
            'If Request.QueryString("Accion") = "C" Then
            '    'Response.Write("</ br>")
            '    'Response.Write("Accion: " & "C")
            '    'Response.Write("</ br>")
            '    'ddlProgramas.SelectedValue = Request.QueryString("Ceco")
            '    CargaDatos(ddlProgramas.SelectedValue)
            'Else
            '    'Response.Write("</ br>")
            '    'Response.Write("Accion: " & "diferente de C")
            '    'Response.Write("</ br>")
            '    ''Carga los datos en el consilidado segun el item de la lista desplegable.
            '    'Response.Write("</ br>")
            '    'Response.Write("ddlProgramas.SelectedValue: " & ddlProgramas.SelectedValue)
            '    CargaDatos(ddlProgramas.SelectedValue)
            'End If
            CargaDatos(ddlProgramas.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub chkActivos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkActivos.CheckedChanged
        Try
            CargaDatos(ddlProgramas.SelectedValue)
            'CargaDatos(1831)
        Catch ex As Exception
            Response.Write(ex.StackTrace & " " & ex.Message)
        End Try
    End Sub

    'Procedimiento para cargar los datos al grid,
    Private Sub CargaDatos(ByVal codigo_cpf As Integer)
        Try
            Dim obj As New clsProfesionalizacion
            Dim dts As New Data.DataTable

            dts = obj.ConsultarDatosConsolidado(Request.QueryString("id"), Me.Request.QueryString("ctf"), codigo_cpf, IIf(chkActivos.Checked, 1, 0))
            If dts.Rows.Count > 0 Then
                gvLista.DataSource = dts
                gvLista.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.StackTrace & " " & ex.Message & " " & ex.Message)
        End Try
    End Sub

    Protected Sub gvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvLista.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim fila As Data.DataRowView
                fila = e.Row.DataItem

                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
                e.Row.Height = 25

                e.Row.Cells(0).Font.Bold = True
                e.Row.Cells(0).ForeColor = System.Drawing.ColorTranslator.FromHtml("#3366CC")

                If (e.Row.Cells.Count - 1) > 3 Then
                    For i As Integer = 1 To (e.Row.Cells.Count - 2)
                        If e.Row.Cells(i).Text <> 0 Then
                            'Asignamos las sessiones:
                            'Session("s_id") = Me.Request.QueryString("id")
                            'Session("s_codigo_cur") = e.Row.Cells(0).Text.Trim
                            'Session("s_programa") = gvLista.HeaderRow.Cells(i).Text
                            'Session("s_tipo") = "U"
                            'Session("s_ceco") = Me.ddlProgramas.SelectedValue
                            'Session("s_ctf") = Request.QueryString("ctf")
                            'Session("s_accion") = "C"
                            'Session("s_estado") = IIf(chkActivos.Checked, 1, 0)

                            e.Row.Cells(i).Attributes.Add("OnClick", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?codigo_per=" & Me.Request.QueryString("id") & "&codigo_cur=" + e.Row.Cells(0).Text.Trim & "&Programa=" & gvLista.HeaderRow.Cells(i).Text & "&Tipo=" & "U" & "&Ceco=" & Me.ddlProgramas.SelectedValue & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&Accion=" & "C" & "&estado=" & IIf(chkActivos.Checked, 1, 0) & "'")
                            e.Row.Cells(i).ToolTip = "Ver Detalle"
                            e.Row.Cells(i).Font.Bold = True
                        End If
                        e.Row.Cells(i).HorizontalAlign = HorizontalAlign.Center
                    Next

                    If gvLista.HeaderRow.Cells(e.Row.Cells.Count - 1).Text = "TOTAL" Then
                        e.Row.Cells(e.Row.Cells.Count - 1).Attributes.Add("OnClick", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?codigo_per=" & Me.Request.QueryString("id") & "&codigo_cur=" + e.Row.Cells(0).Text.Trim & "&Ceco=" & Me.ddlProgramas.SelectedValue & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&Tipo=" & "T" & "&Accion=" & "C" & "&estado=" & IIf(chkActivos.Checked, 1, 0) & "'")
                        e.Row.Cells(0).Attributes.Add("OnClick", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?codigo_per=" & Me.Request.QueryString("id") & "&codigo_cur=" + e.Row.Cells(0).Text.Trim & "&Ceco=" & Me.ddlProgramas.SelectedValue & "&Tipo=" & "T" & "&Accion=" & "C" & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&estado=" & IIf(chkActivos.Checked, 1, 0) & "'")

                        e.Row.Cells(e.Row.Cells.Count - 1).ToolTip = "Motrar Detalle"
                        e.Row.Cells(0).ToolTip = "Motrar Detalle"
                    End If
                End If

                '------------------------------------------------------------------------
                If e.Row.Cells(0).Text = "TOTAL" Then
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#3366CC")
                    e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
                    e.Row.Font.Bold = True
                    e.Row.Height = 30
                Else
                    '[Ejemplos: ]
                    'e.Row.Cells(7).Attributes.Add("onclick", "location.href='frmmodificarpostulante.aspx?id=" & Request.QueryString("id") & "&codigo_pot=" + Me.GridView1.DataKeys(e.Row.RowIndex).Value.ToString & "'")
                    'e.Row.Cells(2).Text = "<a href='detalleprogramaec.aspx?id=" & fila.Row("codigo_pec") & "&cc=" & fila.Row("codigo_cco") & "'>" & fila.Row("descripcion_pes") & "</a>"
                    'Llamanos a la pagina Detalle
                    '[Esta linea esta funcionando bien]----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    'e.Row.Cells(1).Attributes.Add("OnClick", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?codigo_per=" & 2771 & "&codigo_cur=" + gvLista.DataKeys(e.Row.RowIndex).Value.ToString & "'")
                    '--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    '[pruebas]
                    'e.Row.Cells(1).Attributes.Add("OnClick", "javascript:location.href='frmListaAlumnosProfesionalizacion.aspx?codigo_per=" & 2771 & "&codigo_cur=" + gvLista.DataKeys(e.Row.RowIndex).Values(0).ToString & "'")
                    'Color de las columnas
                    'e.Row.Cells(1).BackColor = System.Drawing.ColorTranslator.FromHtml("#e8eef7")
                    'e.Row.Cells(1).Font.Bold = True
                    'e.Row.Cells(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#e8eef7")
                    'e.Row.Cells(1).ToolTip = "Ver Detalle"
                    e.Row.Cells(0).Width = 350
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.StackTrace & " " & ex.Message)
        End Try
    End Sub


    Protected Sub ddlProgramas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProgramas.SelectedIndexChanged
        Try
            'Response.Write("SelectedValue: " & ddlProgramas.SelectedValue.ToString)

            CargaDatos(ddlProgramas.SelectedValue)
        Catch ex As Exception
            Response.Write(ex.StackTrace & " " & ex.Message)
        End Try
    End Sub

   
End Class
