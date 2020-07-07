Partial Class _AsignarEvaluador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            ConsultarDepartamentoAcademico()
            ConsultarDepartamentoAcadem()

        End If

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

    Public Sub ConsultarDepartamentoAcadem()
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarDepartamentoAcademico", "SG", "")
            obj.CerrarConexion()
            Me.ddlDepartamentoAcadem.DataTextField = "nombre_Dac"
            Me.ddlDepartamentoAcadem.DataValueField = "codigo_Dac"
            Me.ddlDepartamentoAcadem.DataSource = dt
            Me.ddlDepartamentoAcadem.SelectedIndex = 0
            Me.ddlDepartamentoAcadem.DataBind()

        Catch ex As Exception
            Me.lblMensaje.Text = "Error al cargar el Departamento Académico"
        End Try
    End Sub

    Public Sub ConsultarDocentes()

        Try
            If Me.ddlDepartamentoAcadem.SelectedValue <> "0" Then
                Dim dt As New Data.DataTable
                Dim obj As New ClsConectarDatos
                obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
                obj.AbrirConexion()
                dt = obj.TraerDataTable("ACAD_DepartamentoAcademicoDocente", Me.ddlDepartamentoAcadem.SelectedItem.Text)
                obj.CerrarConexion()
                If dt.Rows.Count Then
                    Me.ddlDocente.DataSource = dt
                    Me.ddlDocente.DataTextField = "Docente"
                    Me.ddlDocente.DataValueField = "codigo_per"
                    Me.ddlDocente.DataBind()

                End If
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Public Sub ConsultarEvaluadores()

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
                dt = obj.TraerDataTable("ACAD_ConsultarEvaluadores", Me.ddlDepartamentoAcademico.SelectedValue)

                If dt.Rows.Count Then
                    Me.gvCarga.DataSource = dt
                    Me.gvCarga.DataBind()
                Else
                    Me.gvCarga.DataSource = Nothing
                    Me.gvCarga.DataBind()
                    Me.celdaGrid.Visible = True
                    Me.celdaGrid.InnerHtml = "No existen Evaluadores para el Departamento Académico seleccionado"
                End If
                obj.CerrarConexion()
            Else
                Me.gvCarga.DataSource = Nothing
                Me.gvCarga.DataBind()
                Me.celdaGrid.Visible = True
                Me.celdaGrid.InnerHtml = "No existen Evaluadores para el Departamento Académico seleccionado"
            End If

        Catch ex As Exception
            Me.lblMensaje.Text = ex.Message & " - " & ex.StackTrace '"Error al consultar.."
        End Try

    End Sub

    Protected Sub cboDepartamentoAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoAcademico.SelectedIndexChanged

        ConsultarEvaluadores()
        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub cboDepartamentoAcadem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartamentoAcadem.SelectedIndexChanged

        ConsultarDocentes()
        Me.lblMensaje0.Text = ""
    End Sub
    Protected Sub cboDocente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDocente.SelectedIndexChanged

        Me.lblMensaje0.Text = ""
    End Sub

    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignarEvaluador.Click
        Try
            Dim rpta As Integer
            Dim valoresdevueltos(1) As Integer

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            If Me.ddlDepartamentoAcademico.SelectedIndex = 0 Then
                Me.lblMensaje0.Text = "Atención: Debe seleccionar un Departamento Académico"
                Exit Sub
            End If
            If Me.ddlDepartamentoAcadem.SelectedIndex = 0 Then
                Me.lblMensaje0.Text = "Atención: Debe seleccionar el Departamento Académico del Evaluador"
            Else
                obj.Ejecutar("[ACAD_AgregarEvaluador]", Me.ddlDepartamentoAcademico.SelectedValue, Me.ddlDocente.SelectedValue, 1, 0).copyto(valoresdevueltos, 0)

                rpta = valoresdevueltos(0)
                If rpta = 1 Then
                    Me.lblMensaje0.Text = "Asignación Correcta.."
                Else
                    Me.lblMensaje0.Text = "Atención! El Docente ya se ha registrado en este Departamento Académico"
                End If
            End If
            obj.CerrarConexion()
            obj = Nothing

        Catch ex As Exception
            Response.Write(ex.Message & " - " & ex.StackTrace)
        End Try

        ConsultarEvaluadores()

    End Sub

    Protected Sub gvCarga_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCarga.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
          
            e.Row.Cells(1).Text = e.Row.RowIndex + 1

            e.Row.Cells(6).Text = IIf(fila.Row("estado_evl") = 0, "Evaluador Inactivo", "Evaluador Activo")

            If e.Row.Cells(6).Text = "Evaluador Activo" Then
                e.Row.Cells(6).Font.Bold = True
            Else
                e.Row.Cells(6).Font.Bold = False
                e.Row.Cells(6).ForeColor = System.Drawing.Color.Red
            End If

            CType(e.Row.FindControl("CheckBox1"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

        End If

    End Sub

    Protected Sub btnCambiarEstado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCambiarEstado.Click

        Dim Fila As GridViewRow
        Try
            Dim x As Integer
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            x = 0
            For i As Integer = 0 To Me.gvCarga.Rows.Count - 1

                Fila = Me.gvCarga.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    If CType(Fila.FindControl("CheckBox1"), CheckBox).Checked = True Then
                        obj.Ejecutar("ACAD_CambiarEstadoEvaluador", gvCarga.DataKeys.Item(Fila.RowIndex).Values("codigo_evl"))
                        Me.lblMensaje0.Text = "Cambio de Estado CORRECTO.."
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

        ConsultarEvaluadores()

    End Sub
End Class
