
Partial Class GestionInvestigacion_FrmEvaluarAsignacionJuradoSustentacion
    Inherits System.Web.UI.Page
    Dim contador As Integer = 0
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        If IsPostBack = False Then
            ConsultarTipoEstudio()
            'ListarDocentes()
            If Request("ctf") <> 1 Then
                Dim obj As New ClsGestionInvestigacion
                'Me.ddlDocente.SelectedValue = obj.EncrytedString64(Session("id_per"))
            End If
        End If
    End Sub

    Private Sub ConsultarTipoEstudio()
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        dt = obj.ConsultarTipoEstudio("GT", 0)
        Me.ddlTipoEstudio.Items.Clear()
        Me.ddlTipoEstudio.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlTipoEstudio.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_test"), dt.Rows(i).Item("codigo_test")))
        Next
    End Sub

    Private Sub ConsultarCarreras(ByVal codigo_test As Integer, ByVal id As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Dim codigo_apl As Integer = 72 'GESTION INVESTIGACIÓN
        Me.ddlCarrera.Items.Clear()
        Me.ddlCarrera.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        dt = obj.ConsultarCarrerasxTipoEstudio(codigo_test, id, ctf, codigo_apl)
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlCarrera.Items.Add(New ListItem(dt.Rows(i).Item("nombre_Cpf"), dt.Rows(i).Item("codigo_cpf")))
        Next
    End Sub

    Private Sub ListarJuradosEvaluacion(ByVal textobusqueda As String, ByVal codigo_cpf As Integer, ByVal codigo_per As Integer, ByVal ctf As Integer)
        Dim obj As New ClsGestionInvestigacion
        Dim dt As New Data.DataTable
        Me.gvJurados.DataSource = ""
        dt = obj.ListarEvaluarJuradoSustentacion(textobusqueda, codigo_cpf, codigo_per, ctf)
        If dt.Rows.Count > 0 Then
            Me.gvJurados.DataSource = dt
        End If
        Me.gvJurados.DataBind()
    End Sub


    Dim dtRol As New Data.DataTable
    Private Sub ListarRol()
        Dim obj As New ClsGestionInvestigacion
        dtRol = obj.ConsultarTipoParticipante("3", "J")
        Dim dataview As New Data.DataView(dtRol)
        dtRol = dataview.ToTable
    End Sub

    Dim dtPersonal As New Data.DataTable
    Private Sub ListarPersonal()
        Dim obj As New ClsGestionInvestigacion
        dtPersonal = obj.ConsultarPersonalxDepartamentoAcademico("%")
    End Sub


    Function validarBusqueda() As Boolean
        If Me.ddlTipoEstudio.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione un tipo de estudio')", True)
            Return False
        End If
        If Me.ddlCarrera.SelectedValue = "" Then
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','Seleccione una Carrera Profesional')", True)
            Return False
        End If
        Return True
    End Function


    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            If validarBusqueda() = True Then
                Me.lblmensaje.InnerText = ""
                Me.lblmensaje.Attributes.Remove("class")
                If Me.ddlCarrera.SelectedValue <> "" Then
                    ListarPersonal()
                    ListarRol()

                    ListarJuradosEvaluacion(Me.txtAlumno.Text, Me.ddlCarrera.SelectedValue, Session("id_per"), Request("ctf"))

                Else
                    Me.gvJurados.DataSource = ""
                    Me.gvJurados.DataBind()
                End If
            End If
        Catch ex As Exception
            Me.ScriptManager1.RegisterStartupScript(Me.Page, Me.GetType(), "alert", "fnMensaje('error','" + ex.Message.ToString + "')", True)

        End Try
        
    End Sub

    Protected Sub ddlCarrera_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarrera.SelectedIndexChanged
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Me.lblmensaje.InnerText = ""
        Me.lblmensaje.Attributes.Remove("class")
        Me.gvJurados.DataSource = ""
        Me.gvJurados.DataBind()
    End Sub

    Dim codigo_Tesant As Integer = 0
    Dim color As System.Drawing.Color = Drawing.Color.AliceBlue
    Dim ultimotitulo As String = ""
    Dim ultimoalumno As String = ""
    Dim CurrentRow As Integer = -1

    Protected Sub gvJurados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvJurados.RowCommand
        If e.CommandName = "Actualizar" Then
            Dim obj As New ClsGestionInvestigacion
            Dim dt As New Data.DataTable
            Dim codigo_per As Integer
            Dim codigo_tpi As Integer

            Dim seleccion As GridViewRow = gvJurados.Rows(e.CommandArgument)

            Dim ddlDocente As DropDownList = seleccion.FindControl("ddlDocente")
            codigo_per = ddlDocente.SelectedValue

            Dim ddlRol As DropDownList = seleccion.FindControl("ddlRol")
            codigo_tpi = ddlRol.SelectedValue

            Me.lblmensaje.InnerText = ""
            Me.lblmensaje.Attributes.Remove("class")
            If codigo_per <> 0 And codigo_tpi <> 0 Then

                dt = obj.ActualizarRolJurado(Me.gvJurados.DataKeys(seleccion.RowIndex).Values("codigo_Tes"), Me.gvJurados.DataKeys(seleccion.RowIndex).Values("codigo_jur"), codigo_per, codigo_tpi)

                If dt.Rows(0).Item("Respuesta") = 1 Then
                    Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                    ListarPersonal()
                    ListarRol()
                    ListarJuradosEvaluacion(Me.txtAlumno.Text, Me.ddlCarrera.SelectedValue, Session("id_per"), Request("ctf"))
                Else
                    Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                End If
            Else
                Me.lblmensaje.InnerText = "Debe seleccionar el docente y Rol para actualizar Jurado"
                Me.lblmensaje.Attributes.Add("class", "alert alert-danger")

            End If

        End If

    End Sub
    Protected Sub gvJurados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvJurados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'PARA CARGAR EL COMBO Y SELECCION DEL  ROL
            Dim ddlDocente As DropDownList = e.Row.FindControl("ddlDocente")
            For i As Integer = 0 To dtPersonal.Rows.Count - 1
                ddlDocente.Items.Add(New ListItem(dtPersonal.Rows(i).Item("descripcion"), dtPersonal.Rows(i).Item("codigo")))
            Next
            ddlDocente.SelectedValue = DataBinder.Eval(e.Row.DataItem, "codigo_per")
            ddlDocente.DataBind()

            'PARA CARGAR EL COMBO Y SELECCION DEL  ROL
            Dim ddlRol As DropDownList = e.Row.FindControl("ddlRol")
            For i As Integer = 0 To dtRol.Rows.Count - 1
                ddlRol.Items.Add(New ListItem(dtRol.Rows(i).Item("descripcion"), dtRol.Rows(i).Item("codigo")))
            Next
            ddlRol.SelectedValue = DataBinder.Eval(e.Row.DataItem, "codigo_tpi")
            ddlRol.DataBind()

            'Pintar Filas por titulo
            If DataBinder.Eval(e.Row.DataItem, "codigo_Tes") = codigo_Tesant Or codigo_Tesant = 0 Then
                e.Row.BackColor = color
            Else
                If color = Drawing.Color.AliceBlue Then
                    color = Drawing.Color.LightSkyBlue
                Else
                    color = Drawing.Color.AliceBlue
                End If
                e.Row.BackColor = color
            End If
            codigo_Tesant = DataBinder.Eval(e.Row.DataItem, "codigo_Tes")

            'Combinar Celdas
            Dim row As Data.DataRowView = CType(e.Row.DataItem, Data.DataRowView)
            If ultimotitulo = row("titulo_tes") Then

                If (gvJurados.Rows(CurrentRow).Cells(0).RowSpan = 0) Then
                    gvJurados.Rows(CurrentRow).Cells(0).RowSpan = 2
                Else
                    gvJurados.Rows(CurrentRow).Cells(0).RowSpan += 1
                End If
                e.Row.Cells(0).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                ultimotitulo = row("titulo_tes").ToString()
                CurrentRow = e.Row.RowIndex
            End If
            '

            'Combinar Celdas
            If ultimoalumno = row("Responsables") Then
                If (gvJurados.Rows(CurrentRow).Cells(1).RowSpan = 0) Then
                    gvJurados.Rows(CurrentRow).Cells(1).RowSpan = 2
                Else
                    gvJurados.Rows(CurrentRow).Cells(1).RowSpan += 1
                End If
                e.Row.Cells(1).Visible = False
            Else
                e.Row.VerticalAlign = VerticalAlign.Middle
                ultimoalumno = row("Responsables").ToString()
                CurrentRow = e.Row.RowIndex
            End If
            '


            If DataBinder.Eval(e.Row.DataItem, "apruebadirector_jur") = 1 Then
                e.Row.Cells(2).Text = ""
                e.Row.Cells(5).Text = "APROBADO"
                e.Row.Cells(5).ForeColor = Drawing.Color.Green
                e.Row.Cells(5).Font.Bold = True
                e.Row.Cells(3).Enabled = False
                e.Row.Cells(4).Enabled = False
            End If
        End If
    End Sub

    Protected Sub btnAprobar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAprobar.Click
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../sinacceso.html")
        End If
        Try
            Dim obj As New ClsGestionInvestigacion
            Dim contador As Integer = 0
            Dim codigos As String = ""
            For i As Integer = 0 To gvJurados.Rows.Count - 1
                Dim chkAutoriza As CheckBox = (CType(gvJurados.Rows(i).FindControl("chkAutorizar"), CheckBox))
                If chkAutoriza.Checked = True Then
                    contador = contador + 1
                    codigos = codigos + gvJurados.DataKeys(i).Values("codigo_jur").ToString + ","
                End If
            Next
            If contador > 0 Then
                Dim dt As New Data.DataTable
                dt = obj.AprobarJurado(codigos, Session("id_per"))

                If dt.Rows(0).Item("Respuesta") = 1 Then
                    Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblmensaje.Attributes.Add("class", "alert alert-success")
                    ListarPersonal()
                    ListarRol()
                    ListarJuradosEvaluacion(Me.txtAlumno.Text, Me.ddlCarrera.SelectedValue, Session("id_per"), Request("ctf"))

                Else
                    Me.lblmensaje.InnerText = dt.Rows(0).Item("Mensaje").ToString
                    Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
                End If
            Else
                Me.lblmensaje.InnerText = "Debe Seleccionar al menos un jurado"
                Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
            End If
            
        Catch ex As Exception
            Me.lblmensaje.InnerText = "No se pudo realizar operación"
            Me.lblmensaje.Attributes.Add("class", "alert alert-danger")
        End Try
    End Sub


    Protected Sub ddlTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoEstudio.SelectedIndexChanged
        If Me.ddlTipoEstudio.SelectedValue <> "" Then
            ConsultarCarreras(Me.ddlTipoEstudio.SelectedValue, Request("id"), Request("ctf"))
        Else
            Me.ddlCarrera.Items.Clear()
            Me.ddlCarrera.Items.Add(New ListItem("[ -- Seleccione -- ]", ""))
        End If
    End Sub
End Class
