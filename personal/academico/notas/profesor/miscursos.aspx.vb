
Partial Class academico_notas_profesor_miscursos
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../ErrorSistema.aspx")
        End If
        If IsPostBack = False Then
            Session("ctf_notas") = Request.QueryString("ctf")
            CargaSemestre()
            CargaDocente()
            CicloActual()
               End If
    End Sub

    Private Sub CicloActual()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("EVE_CicloAcademicoActual")
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then
                Me.cboCiclo.SelectedValue = dt.Rows(0).Item("codigo_Cac")
            End If

            dt.Dispose()
            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub CargaSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaCicloAcademico", 0, "")
            obj.CerrarConexion()

            Me.cboCiclo.DataSource = dt
            Me.cboCiclo.DataTextField = "descripcion_cac"
            Me.cboCiclo.DataValueField = "codigo_Cac"
            Me.cboCiclo.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Private Sub CargaDocente()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_BuscaDocenteAula", Session("id_per"), Request.QueryString("ctf"))
            obj.CerrarConexion()

            Me.cboDocente.DataSource = dt
            Me.cboDocente.DataTextField = "NombreUsuario"
            Me.cboDocente.DataValueField = "codigo_Uap"
            Me.cboDocente.DataBind()

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("NOT_ConsultarRegistroNotas", "3", Me.cboCiclo.SelectedValue, Me.cboDocente.SelectedValue)
            obj.CerrarConexion()

            Me.gvCursos.DataSource = dt
            Me.gvCursos.DataBind()
            ShowMessage("Para cursos de pregrado regular, los promedios finales se publican a través del módulo de Desarrollo y Evaluación del Aprendizaje > Desarrollo de Asignatura > Calificar Asignatura.", MessageType.Info)

            obj = Nothing
        Catch ex As Exception
            ShowMessage("Error: " & ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvCursos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursos.RowDataBound
        Dim codigo_aut As Integer = 0, codigo_test As Integer = 0, modular_pcu As Boolean = 0, codigo_cpf As Integer = 0, refrecuperacion_cup As Integer = 0

        If (e.Row.RowType.ToString = "DataRow") Then

            '<a href="../administrarconsultar/lstalumnosmatriculados.asp?codigo_cac=<%=codigo_cac%>&codigo_cup=<%=rsCarga("codigo_cup")%>&nivel=<%=rsCarga("codigo_aut")%>"><u><%=rsCarga("nombre_cur")%></u></a>
            '/administrarconsultar/lstalumnosmatriculados.asp?codigo_cac=69&codigo_cup=544761&nivel=0

            'e.Row.Cells(1).Text = "<a href='" & strRuta & "?cup=" & gvCursos.DataKeys(e.Row.RowIndex).Values(0).ToString() & "'>" & e.Row.Cells(1).Text & "</a>"

            'If e.Row.Cells(7).Text = "P" Then
            '    e.Row.Cells(7).Text = "PENDIENTE"
            'ElseIf e.Row.Cells(7).Text = "R" Then
            '    e.Row.Cells(7).Text = "REGISTRADO"
            'Else
            '    e.Row.Cells(7).Text = "-"
            'End If

            'Yperez
            codigo_aut = gvCursos.DataKeys(e.Row.RowIndex).Values("codigo_aut").ToString()
            codigo_test = gvCursos.DataKeys(e.Row.RowIndex).Values("codigo_test").ToString()
            modular_pcu = gvCursos.DataKeys(e.Row.RowIndex).Values("modular_pcu").ToString()
            codigo_cpf = gvCursos.DataKeys(e.Row.RowIndex).Values("codigo_cpf").ToString()
            refrecuperacion_cup = gvCursos.DataKeys(e.Row.RowIndex).Values("refrecuperacion_cup").ToString()

            If codigo_aut = 0 And (e.Row.Cells(7).Text) <> "P" Then
                e.Row.Cells(7).Text = "Realizado"
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Blue
            ElseIf codigo_aut <> 0 And (e.Row.Cells(7).Text) <> "P" Then
                e.Row.Cells(7).Text = "Pendiente con autorización"
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Red
            ElseIf CInt(e.Row.Cells(5).Text) = 0 Then
                e.Row.Cells(7).Text = "-"
            Else
                e.Row.Cells(7).Text = "Pendiente"
                e.Row.Cells(7).ForeColor = System.Drawing.Color.Green
            End If

            ' If modular_pcu = True Then
            'e.Row.Enabled = True
            'Else
            'If codigo_cpf = 23 Then
            '    e.Row.Enabled = True
            'ElseIf codigo_cpf = 24 Then
            '    e.Row.Enabled = True
            'Else
            If (codigo_test = 2 Or codigo_test = 4) And refrecuperacion_cup = 0 Then
                e.Row.Enabled = False
            Else
                e.Row.Enabled = True
                'End If
            End If

            'End If


        End If
    End Sub

    Protected Sub gvCursos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvCursos.SelectedIndexChanged
        Session("cup") = gvCursos.DataKeys(gvCursos.SelectedRow.RowIndex).Values(0).ToString()
        Session("id_perNota") = Me.cboDocente.SelectedValue
        Response.Redirect("../administrarconsultar/lstalumnosmatriculados.aspx")
    End Sub
End Class
