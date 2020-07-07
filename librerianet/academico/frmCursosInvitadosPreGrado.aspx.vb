
Partial Class academico_frmCursosInvitadosPreGrado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim ip As String = Request.ServerVariables("REMOTE_ADDR").ToString()
        Dim host As String = System.Net.Dns.GetHostEntry(Request.UserHostAddress).HostName
        HdPC.Value = ip & " | " & host

        If (Session("codigo_alu") Is Nothing) Then
            Response.Redirect("../ErrorSistema.aspx")
        End If

        If (Session("codigo_cac") Is Nothing) Then
            Response.Redirect("../ErrorSistema.aspx")
        End If

        If (IsPostBack = False) Then
            If (TieneDeudas() = True) Then
                Response.Redirect("frmBloqueoDeuda1.aspx")
            Else
                CargaCursos()
                CursosNoDisponibles()
            End If
        End If

    End Sub

    Private Function TieneDeudas() As Boolean
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_VerificaDeudasRecuperacion", Session("codigo_alu"), Session("codigo_cac"))
            If (dt.Rows.Count = 0) Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Return True
        End Try
    End Function

    Private Sub CursosNoDisponibles()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_ListaExamenesNoPermitidos", Session("codigo_alu"), Session("codigo_cac"))
            Me.gvNoDisponible.DataSource = dt
            Me.gvNoDisponible.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los cursos no disponibles: " & ex.Message
        End Try
    End Sub

    Private Sub CargaCursos()
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As Data.DataTable
        Dim dtLista As Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_ListaCursosRecuperacion", Session("codigo_alu"), Session("codigo_cac"))
            Me.gvLista.DataSource = dt
            Me.gvLista.DataBind()

            dtLista = obj.TraerDataTable("ACAD_ListaCursosExamen", Session("codigo_cac"), Session("codigo_alu"))
            Me.gvDetalle.DataSource = dtLista
            Me.gvDetalle.DataBind()
        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar los examenes de recuperación: " & ex.Message
        End Try
    End Sub

    Protected Sub gvLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvLista.RowDeleting
        'Response.Write(Me.gvLista.DataKeys(e.RowIndex).Value("codigo_dma").ToString)
    End Sub

    Protected Sub gvLista_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvLista.RowUpdating
        'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try            
            'obj.Ejecutar("ACAD_RechazaExamenRecuperacion", Me.gvLista.DataKeys(e.RowIndex).Value("codigo_dma").ToString, Me.HdPC.Value)
            'Response.Write("dma: " & Me.gvLista.DataKeys(e.RowIndex).Value & "<br/>")
            'Response.Write("dma1: " & gvLista.DataKeys.Item(e.RowIndex).Values("codigo_Dma"))
            'Me.lblMensaje.Text = "Curso rechazado"
            'CargaCursos()
        Catch ex As Exception
            'Me.lblMensaje.Text = "Error al rechazar el examen de recuperación: " & ex.Message
        End Try
    End Sub

    Protected Sub gvLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvLista.SelectedIndexChanged
        '     Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        '    Try
        ' Me.lblMensaje.Text = "Curso confirmado"
        'obj.Ejecutar("ACAD_ConfirmaExamenRecuperacion", Session("codigo_alu"), Session("codigo_cac"), Me.gvLista.SelectedDataKey.Values("codigo_dma"), Me.HdPC.Value)
        ' Response.Write(Me.gvLista.SelectedDataKey.Values("codigo_dma"))

        ' Catch ex As Exception
        ' Me.lblMensaje.Text = "Error al Confirmar el examen de recuperación: " & ex.Message
        ' End Try
    End Sub

    Protected Sub ibtnConfirma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            obj.Ejecutar("ACAD_ConfirmaExamenRecuperacion", Session("codigo_alu"), Session("codigo_cac"), gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma"), Me.HdPC.Value)
            Me.lblMensaje.Text = "Curso confirmado. Estarás apto para rendir el examen de recuperación del curso una vez realizado el pago correspondiente."
            CargaCursos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ibtnRechaza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Dim ibtnRechaza As ImageButton
            Dim row As GridViewRow
            ibtnRechaza = sender
            row = ibtnRechaza.NamingContainer

            obj.Ejecutar("ACAD_RechazaExamenRecuperacion", gvLista.DataKeys.Item(row.RowIndex).Values("codigo_Dma").ToString, Me.HdPC.Value)
            Me.lblMensaje.Text = "Curso rechazado"
            CargaCursos()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnActualiza_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualiza.Click
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim dt As Data.DataTable
        Try
            dt = obj.TraerDataTable("ACAD_VerificaAlumnoExRecuperacion", Session("codigo_alu"))
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    obj.Ejecutar("ACAD_InvitaExRecuperacionPreGrado", dt.Rows(i).Item("codigo_alu"), _
                                 dt.Rows(i).Item("codigo_pso"), dt.Rows(i).Item("codigo_mat"), _
                                 dt.Rows(i).Item("codigo_cup"), dt.Rows(i).Item("tipo_cur"), _
                                 dt.Rows(i).Item("creditocur_dma"), dt.Rows(i).Item("vecescurso_dma"))
                Next
            ElseIf (dt.Rows.Count = 0) Then
                lblMensaje.Text = "Ud. debe estar al día en sus pagos y tener nota desaprobatoria."
            End If

            CargaCursos()
            CursosNoDisponibles()
        Catch ex As Exception
            lblMensaje.Text = "Error al cargar los cursos: " & ex.Message
        End Try        
    End Sub
End Class
