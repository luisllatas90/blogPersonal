
Partial Class FrmAdscripcionPersonal
    Inherits System.Web.UI.Page

    Private Sub ConsultarPeriodoLaborable()
        Dim obj As New ClsAdscripcionDocente
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPeriodoLaborable("T", 0)
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboPeriodo.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_pel"), dt.Rows(i).Item("codigo_pel")))
            Me.ddlPeriodo.Items.Add(New ListItem(dt.Rows(i).Item("descripcion_pel"), dt.Rows(i).Item("codigo_pel")))
        Next
    End Sub

    Private Sub ConsultarDepartamento()
        Dim obj As New ClsAdscripcionDocente
        Dim dt As New Data.DataTable
        dt = obj.ConsultarDepartamentoAcademico(1, "", "")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.cboDepartamento.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
            Me.ddlDepartamento.Items.Add(New ListItem(dt.Rows(i).Item("descripcion"), dt.Rows(i).Item("codigo")))
        Next
        Me.cboDepartamento.Items.Add(New ListItem("TODOS", "%"))
    End Sub

    Private Sub ConsultarPersonal()
        Dim obj As New ClsAdscripcionDocente
        Dim dt As New Data.DataTable
        dt = obj.ConsultarPersonal("CXT", "1")
        For i As Integer = 0 To dt.Rows.Count - 1
            Me.ddlPersonal.Items.Add(New ListItem(dt.Rows(i).Item("nombre"), dt.Rows(i).Item("codigo")))
        Next
    End Sub

    Private Sub ConsultarAdscriptos(ByVal periodo As Integer, ByVal codigo_dac As String)
        Dim obj As New ClsAdscripcionDocente
        Dim dt As New Data.DataTable
        dt = obj.ListarAdscriptosPersonal(periodo, codigo_dac)
        Me.gvAdscriptos.DataSource = dt
        Me.gvAdscriptos.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../sinacceso.html")
        End If
        Try
            If IsPostBack = False Then
                Me.DivNuevo.Visible = False
                ConsultarPeriodoLaborable()
                ConsultarPersonal()
                If Request.QueryString("ctf") = 1 Or Request.QueryString("ctf") = 137 Or Request.QueryString("ctf") = 141 Then
                    ConsultarDepartamento()
                Else
                    Me.btnNuevo.Visible = False
                End If

                Dim obj As New ClsAdscripcionDocente
                Dim dt As New Data.DataTable
                dt = obj.ObtenerDepartamentoAcademico("CXP", Session("id_per"), 0)

                If dt.Rows.Count > 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Me.cboDepartamento.Items.Add(New ListItem(dt.Rows(i).Item("nombre_dac").ToString, dt.Rows(i).Item("codigo_dac")))
                    Next
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('" + ex.Message.ToString + "')</script>")
        End Try
    End Sub

    Protected Sub btnConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        Try
            If Me.cboDepartamento.SelectedValue <> "0" And cboPeriodo.SelectedValue <> "0" Then
                ConsultarAdscriptos(Me.cboPeriodo.SelectedItem.Value, Me.cboDepartamento.SelectedValue)
                If Request.QueryString("ctf") <> 1 And Request.QueryString("ctf") <> 137 And Request.QueryString("ctf") <> 141 Then
                    Me.gvAdscriptos.Columns(5).Visible = False
                End If
            Else
                ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Debe seleccionar un semestre y un departamento acádemico')</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Me.DivNuevo.Visible = True
        Me.divGrilla.Visible = False
        Me.tbFiltros.Visible = False
        Limpiar()
    End Sub

    Private Sub Limpiar()
        Me.ddlPeriodo.SelectedValue = "0"
        Me.ddlDepartamento.SelectedValue = "0"
        Me.ddlPersonal.SelectedValue = "0"
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.DivNuevo.Visible = False
        Me.divGrilla.Visible = True
        Me.tbFiltros.Visible = True
    End Sub

    Protected Sub gvAdscriptos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvAdscriptos.RowCommand
        Try
            If e.CommandName = "Quitar" Then
                Dim obj As New ClsAdscripcionDocente
                Dim dt As New Data.DataTable

                dt = obj.EliminarAdscripcionPersonal(Me.gvAdscriptos.DataKeys(e.CommandArgument).Values("codigo_apl"), Request.QueryString("id"))

                If dt.Rows(0).Item("respuesta") = 1 Then
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Afiliación eliminada correctamente')</script>")
                    If Me.cboDepartamento.SelectedValue <> "0" And cboPeriodo.SelectedValue <> "0" Then
                        ConsultarAdscriptos(Me.cboPeriodo.SelectedItem.Value, Me.cboDepartamento.SelectedValue)
                    End If
                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('No se pudo eliminar afiliación')</script>")
                End If

            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('No se pudo eliminar afiliación')</script>")
        End Try

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            If validar() = True Then
                Dim obj As New ClsAdscripcionDocente
                Dim dt As New Data.DataTable
                dt = obj.AgregarAdscripcionPersonal(Me.ddlPeriodo.SelectedValue, Me.ddlDepartamento.SelectedValue, Me.ddlPersonal.SelectedValue, Request.QueryString("id"))
                If dt.Rows(0).Item("respuesta") = 1 Then
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Afiliación registrada con éxito')</script>")
                    If Me.cboDepartamento.SelectedValue <> "0" And cboPeriodo.SelectedValue <> "0" Then
                        ConsultarAdscriptos(Me.cboPeriodo.SelectedItem.Value, Me.cboDepartamento.SelectedValue)
                        Me.DivNuevo.Visible = False
                        Me.divGrilla.Visible = True
                        Me.tbFiltros.Visible = True
                    End If
                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('" + dt.Rows(0).Item("mensaje").ToString + "')</script>")
                End If
            End If
        Catch ex As Exception
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('No se pudo registrar Afiliación')</script>")
        End Try
    End Sub

    Function validar() As Boolean
        If Me.ddlPeriodo.SelectedValue = "0" Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Debe seleccionar un Periodo laborable')</script>")
            Return False
        End If
        If Me.ddlDepartamento.SelectedValue = "0" Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Debe seleccionar un Departamento académico')</script>")
            Return False
        End If
        If Me.ddlPersonal.SelectedValue = "0" Then
            ClientScript.RegisterClientScriptBlock(Me.GetType, "alert", "<script>alert('Debe seleccionar un Docente')</script>")
            Return False
        End If
        Return True
    End Function

End Class
