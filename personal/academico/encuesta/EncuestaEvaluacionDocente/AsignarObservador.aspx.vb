Partial Class _AsignarObservador

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'Response.Write(Request.QueryString("id")

            ConsultarDepartamentoAcademico()

            If Request.QueryString("ctf") = 15 Then 'DIRECTOR DE DEPARTAMENTO ACADEMICO

                DepartamentoPersonal()
                Me.ddlDepartamentoAcademico.SelectedValue = Me.ddlDepAcad.SelectedItem.Text
                ConsultarEvaluadores()
                FiltroEvaluadores()

                ConsultarObservadorDocente()
                'Me.ddlDepartamentoAcademico.Enabled = False
                Me.ddlObservador.Visible = False
                Me.txtEvaluador.Visible = False

            End If

        End If

    End Sub

    Public Sub DepartamentoPersonal()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ACAD_ConsultaDepartamentoDirector", Request.QueryString("id"))
            obj.CerrarConexion()

            Me.ddlDepAcad.DataTextField = "codigo_Dac"
            Me.ddlDepAcad.DataValueField = "codigo_Dac"
            Me.ddlDepAcad.DataSource = dt
            Me.ddlDepAcad.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el Departamento Académico"
        End Try

    End Sub

    Public Sub ConsultarDepartamentoAcademico()

        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarDepartamentoAcademico", "SG", "")
            obj.CerrarConexion()
            Me.ddlDepartamentoAcademico.DataTextField = "nombre_Dac"
            Me.ddlDepartamentoAcademico.DataValueField = "codigo_Dac"
            Me.ddlDepartamentoAcademico.DataSource = dt

            Me.ddlDepartamentoAcademico.SelectedIndex = 0

            Me.ddlDepartamentoAcademico.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el Departamento Académico"
        End Try

    End Sub

    Public Sub ConsultarEvaluadores()

        Me.ddlEvaluador.Items.Clear()

        Try
            If Me.ddlDepartamentoAcademico.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_ConsultarEvaluadorActivo", Me.ddlDepartamentoAcademico.SelectedValue)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Me.ddlEvaluador.DataSource = dt
                    Me.ddlEvaluador.DataTextField = "Personal"
                    Me.ddlEvaluador.DataValueField = "codigo_evl"

                ElseIf dt.Rows.Count = 0 Then

                    Me.lblMensaje0.Text = "Nota: No existen Evaluadores asignados al Departamento Académico"
                    Me.ddlEvaluador.DataSource = Nothing

                End If

                Me.ddlEvaluador.DataBind()
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Public Sub FiltroEvaluadores()

        Me.ddlObservador.Items.Clear()

        Try
            If Me.ddlDepartamentoAcademico.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_ConsultarEvaluadorActivo", Me.ddlDepartamentoAcademico.SelectedValue)
                obj.CerrarConexion()

                If dt.Rows.Count > 0 Then
                    Me.ddlObservador.DataSource = dt
                    Me.ddlObservador.DataTextField = "Personal"
                    Me.ddlObservador.DataValueField = "codigo_evl"

                ElseIf dt.Rows.Count = 0 Then

                    Me.ddlObservador.DataSource = Nothing

                End If
                Me.ddlObservador.DataBind()
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Public Sub ConsultarObservadorDocente()

        Me.gvCarga.DataSource = Nothing
        Me.gvCarga.DataBind()
        Me.celdaGrid.Visible = True
        Me.celdaGrid.InnerHtml = ""

        Try
            If Me.ddlDepartamentoAcademico.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos

                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_ConsultarObservadorDocente", Me.ddlDepartamentoAcademico.SelectedValue, Me.ddlEstado.SelectedValue, Me.ddlObservador.SelectedValue)

                If dt.Rows.Count Then
                    Me.gvCarga.DataSource = dt
                    Me.gvCarga.DataBind()
                Else
                    Me.gvCarga.DataSource = Nothing
                    Me.gvCarga.DataBind()
                    Me.celdaGrid.Visible = True
                    Me.celdaGrid.InnerHtml = "No se ha asignado el Evaluador a ningún docente en el Departamento Académico"
                End If
                obj.CerrarConexion()
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Me.celdaGrid.Visible = True
                Me.celdaGrid.InnerHtml = "*Nota: No ha seleccionado el Departamento Académico"
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Protected Sub cboDepartamentoAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoAcademico.SelectedIndexChanged

        ConsultarEvaluadores()
        FiltroEvaluadores()

        ConsultarObservadorDocente()

        Me.lblMensaje0.Text = ""

    End Sub

    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged

        If Me.ddlEstado.SelectedValue = "T" Or Me.ddlEstado.SelectedValue = "P" Then
            Me.txtEvaluador.Visible = False
            Me.ddlObservador.Visible = False
        End If

        If Me.ddlEstado.SelectedValue = "A" Then
            Me.txtEvaluador.Visible = True
            Me.ddlObservador.Visible = True
        End If

        ConsultarObservadorDocente()
        Me.lblMensaje0.Text = ""

    End Sub

    Protected Sub cboEvaluador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEvaluador.SelectedIndexChanged

        Me.lblMensaje0.Text = ""

    End Sub

    Protected Sub cboObservador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlObservador.SelectedIndexChanged

        ConsultarObservadorDocente()
        Me.lblMensaje0.Text = ""

    End Sub

    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignar.Click

        Dim Fila As GridViewRow
        Try
            Dim x As Integer
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            x = 0
            If Me.ddlDepartamentoAcademico.SelectedIndex = 0 Then
                Me.lblMensaje0.Text = "Atención: Debe seleccionar un Departamento Académico"
                Exit Sub
            End If
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1
                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        obj.Ejecutar("ACAD_AsignarObservadorDocente", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_Per"), Me.ddlDepartamentoAcademico.SelectedValue, Me.ddlEvaluador.SelectedValue)
                        Me.lblMensaje0.Text = "Actualización Correcta.."
                    Else
                        x = x + 1
                    End If
                End If
            Next
            If x = Me.gvCarga.Rows.Count Then
                Me.lblMensaje0.Text = "Nota: Debe seleccionar un registro de la lista"
            End If
            obj.CerrarConexion()

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarObservadorDocente()

    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            e.Row.Cells(1).Text = e.Row.RowIndex + 1

            'e.Row.Cells(6).Text = IIf(fila.Row("estado_obs") = 0, "Pendiente", "Confirmado")

            'If e.Row.Cells(6).Text = "Confirmado" Then
            '    e.Row.Cells(6).Font.Bold = True
            'Else
            '    e.Row.Cells(6).Font.Bold = False
            '    e.Row.Cells(6).ForeColor = System.Drawing.Color.Red
            'End If

            CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

        End If

    End Sub

End Class
