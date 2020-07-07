
Partial Class Equipo_ReprogramarSolicitudes
    Inherits System.Web.UI.Page
    Private cod_per As Int32

    Protected Sub lbIniciadas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbIniciadas.Click
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.lbIniciadas.BackColor = Drawing.Color.FromArgb(207, 225, 252)
        Me.lbNoIniciadas.BackColor = Drawing.Color.FromArgb(253, 254, 214)
        Me.lbVencidas.BackColor = Drawing.Color.FromArgb(253, 254, 214)

        Me.lbIniciadas.Style.Add("BORDER-RIGHT", "#5D5D5D 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-TOP", "#5D5D5D 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-LEFT", "#5D5D5D 1px solid")

        Me.lbVencidas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbVencidas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbVencidas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbNoIniciadas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbIniciadas.Font.Bold = True
        Me.lbNoIniciadas.Font.Bold = False
        Me.lbVencidas.Font.Bold = False

        Session("tipo") = 1
        Me.GvSolicitudes.DataBind()
        LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
    End Sub

    Protected Sub lbNoIniciadas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNoIniciadas.Click
        Me.lbNoIniciadas.BackColor = Drawing.Color.FromArgb(207, 225, 252)
        Me.lbIniciadas.BackColor = Drawing.Color.FromArgb(253, 254, 214)
        Me.lbVencidas.BackColor = Drawing.Color.FromArgb(253, 254, 214)

        Me.lbNoIniciadas.Style.Add("BORDER-RIGHT", "#5D5D5D 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-TOP", "#5D5D5D 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-LEFT", "#5D5D5D 1px solid")

        Me.lbIniciadas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbVencidas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbVencidas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbVencidas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbIniciadas.Font.Bold = False
        Me.lbNoIniciadas.Font.Bold = True
        Me.lbVencidas.Font.Bold = False

        Session("tipo") = 2
        Me.GvSolicitudes.DataBind()
        LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
    End Sub

    Protected Sub lbVencidas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbVencidas.Click
        Me.lbVencidas.BackColor = Drawing.Color.FromArgb(207, 225, 252)
        Me.lbNoIniciadas.BackColor = Drawing.Color.FromArgb(253, 254, 214)
        Me.lbIniciadas.BackColor = Drawing.Color.FromArgb(253, 254, 214)

        Me.lbVencidas.Style.Add("BORDER-RIGHT", "#5D5D5D 1px solid")
        Me.lbVencidas.Style.Add("BORDER-TOP", "#5D5D5D 1px solid")
        Me.lbVencidas.Style.Add("BORDER-LEFT", "#5D5D5D 1px solid")

        Me.lbIniciadas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbIniciadas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbNoIniciadas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        Me.lbNoIniciadas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        Me.lbIniciadas.Font.Bold = False
        Me.lbNoIniciadas.Font.Bold = False
        Me.lbVencidas.Font.Bold = True

        Session("tipo") = 3
        Me.GvSolicitudes.DataBind()
        LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        cod_per = Request.QueryString("id")
        'Session("tipo") = 1
        'If Not (IsPostBack) Then
        '    'Resaltar pestaña seleccionada
        '    Me.lbIniciadas.BackColor = Drawing.Color.FromArgb(207, 225, 252)
        '    Me.lbNoIniciadas.BackColor = Drawing.Color.FromArgb(253, 254, 214)
        '    Me.lbVencidas.BackColor = Drawing.Color.FromArgb(253, 254, 214)

        '    Me.lbIniciadas.Style.Add("BORDER-RIGHT", "#5D5D5D 1px solid")
        '    Me.lbIniciadas.Style.Add("BORDER-TOP", "#5D5D5D 1px solid")
        '    Me.lbIniciadas.Style.Add("BORDER-LEFT", "#5D5D5D 1px solid")

        '    Me.lbNoIniciadas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        '    Me.lbNoIniciadas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        '    Me.lbNoIniciadas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        '    Me.lbVencidas.Style.Add("BORDER-RIGHT", "#FFFF80 1px solid")
        '    Me.lbVencidas.Style.Add("BORDER-TOP", "#FFFF80 1px solid")
        '    Me.lbVencidas.Style.Add("BORDER-LEFT", "#FFFF80 1px solid")

        '    Me.lbIniciadas.Font.Bold = True
        '    LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
        '    Session("tipo") = 1
        '    Page.RegisterStartupScript("Visible", "<script>CelDatos.style.visibility='hidden'</script>")
        'Else
        '    If Me.lbIniciadas.Font.Bold = True Then
        '        Session("tipo") = 1
        '    ElseIf Me.lbIniciadas.Font.Bold = True Then
        '        Session("tipo") = 2
        '    ElseIf Me.lbIniciadas.Font.Bold = True Then
        '        Session("tipo") = 3
        '    End If
        'End If

    End Sub

    Protected Sub GvSolicitudes_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowCreated
        LblRegistros.Text = "Total de registros: " & Me.GvSolicitudes.Rows.Count
    End Sub

    Protected Sub GvSolicitudes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GvSolicitudes.RowDataBound
        Dim cadena As String
        Dim fila As Data.DataRowView
        Dim id_act As Int16

        'If e.Row.RowType = DataControlRowType.Header Then
        '    Select Case Session("tipo").ToString
        '        Case 1
        '            e.Row.Cells(14).Visible = True
        '            e.Row.Cells(15).Visible = True
        '        Case 2
        '            e.Row.Cells(14).Visible = False
        '            e.Row.Cells(15).Visible = True
        '        Case 3
        '            e.Row.Cells(14).Visible = True
        '            e.Row.Cells(15).Visible = False
        '    End Select
        'End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            fila = e.Row.DataItem
            cadena = "'DatosSolicitud.aspx?cod_sol=" & fila.Row("id_sol").ToString & "'"
            e.Row.Attributes.Add("onMouseOver", "pintarcelda(this)")
            e.Row.Attributes.Add("onMouseOut", "despintarcelda(this)")
            e.Row.Attributes.Add("onClick", "javascript:ifSolicitud.document.location.href=" & cadena)
            Page.RegisterStartupScript("Visible", "<script>CelDatos.style.visibility='visible'</script>")
            If CInt(fila.Row("id_act").ToString) = 0 Then
                id_act = CInt(fila.Row("id_sol").ToString)
            Else
                id_act = CInt(fila.Row("id_act").ToString)
            End If
            'Select Case Session("tipo")
            Select Case Me.RblVer.SelectedValue
                Case 1
                    e.Row.Cells(14).Text = "<div style='cursor:hand' onclick=AbrirPopUp('planificar.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & id_act & "&tipo=" & fila.Row("tipo").ToString & "&Pag=R','350','550')><img border=0 src='../images/vcalendar.gif'></div>"
                    e.Row.Cells(15).Text = "<div style='cursor:hand' onclick=AbrirPopUp('Adminsolicitud.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & id_act & "&tipo=" & fila.Row("tipo").ToString & "','350','450')><img border=0 src='../images/kappfinder.gif'></div>"
                Case 2
                    e.Row.Cells(14).Text = "-"
                    e.Row.Cells(15).Text = "<div style='cursor:hand' onclick=AbrirPopUp('Adminsolicitud.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & id_act & "&tipo=" & fila.Row("tipo").ToString & "','350','450')><img border=0 src='../images/kappfinder.gif'></div>"
                    'e.Row.Cells(15).Visible = False

                Case 3
                    e.Row.Cells(14).Text = "<div style='cursor:hand' onclick=AbrirPopUp('planificar.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & id_act & "&tipo=" & fila.Row("tipo").ToString & "&Pag=R','350','550')><img border=0 src='../images/vcalendar.gif'></div>"
                    'e.Row.Cells(15).Visible = False
                    e.Row.Cells(15).Text = "-"
                    e.Row.Cells(15).Text = "<div style='cursor:hand' onclick=AbrirPopUp('Adminsolicitud.aspx?id_sol=" & fila.Row("id_sol").ToString & "&id=" & cod_per.ToString & "&id_act=" & id_act & "&tipo=" & fila.Row("tipo").ToString & "','350','450')><img border=0 src='../images/kappfinder.gif'></div>"
            End Select
            'If fila.Row("tipo").ToString = "s" Then
            '    e.Row.ForeColor = Drawing.Color.DarkBlue
            'Else
            '    e.Row.ForeColor = Drawing.Color.FromArgb(0, 128, 192)
            'End If
            If fila.Row.Item("tipo").ToString = "s" Then
                e.Row.ForeColor = Drawing.Color.FromArgb(21, 117, 149) '61, 132, 211)
            Else
                e.Row.ForeColor = Drawing.Color.FromArgb(26, 132, 26)
            End If
        End If

    End Sub

    Protected Sub CboCampo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboCampo.SelectedIndexChanged
        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        If Me.CboCampo.SelectedValue > 0 Then
            Me.cboValor.Visible = True
            If Me.CboCampo.SelectedValue = 1 Then
                ClsFunciones.LlenarListas(Me.cboValor, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco")
            ElseIf Me.CboCampo.SelectedValue = 2 Then
                ClsFunciones.LlenarListas(Me.cboValor, ObjCnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "s"), "id_tsol", "descripcion_tsol")
            End If
        Else
            Me.CboCampo.AutoPostBack = False
        End If
    End Sub

End Class
