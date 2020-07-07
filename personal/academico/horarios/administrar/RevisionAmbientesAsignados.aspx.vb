﻿
Partial Class academico_horarios_administrar_RevisionAmbientesAsignados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim tbl As Data.DataTable
            Dim codigo_tfu As Int16 = Request.QueryString("ctf")
            Dim codigo_usu As Integer = Request.QueryString("id")

            '===================================
            'Permisos por Departamento académico
            '===================================
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            If codigo_tfu = 1 Or codigo_tfu = 7 Or codigo_tfu = 16 Then
                tbl = obj.TraerDataTable("ConsultarDepartamentoAcademico", "TO", 0)
            Else
                tbl = obj.TraerDataTable("consultaraccesorecurso", 11, codigo_usu, 0, 0)
            End If
            '=================================
            'Llenar combos
            '=================================
            ClsFunciones.LlenarListas(Me.dpCodigo_Dac, tbl, 0, 1, "--Seleccione el Dpto--")
            ClsFunciones.LlenarListas(Me.dpCodigo_cac, obj.TraerDataTable("ConsultarCicloAcademico", "UCI", 0), "codigo_cac", "descripcion_cac", "--Seleccione--")
            ClsFunciones.LlenarListas(Me.dpTipoAmbiente, obj.TraerDataTable("ACAD_ConsultarTipoAmbiente"), "codigo_tam", "descripcion_Tam")
            dpTipoAmbiente.SelectedValue = 3
            tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing

            Me.cboSigno.Visible = False
            Me.txtCompararCon.Visible = False
        End If
    End Sub

    Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
        ConsultarCursosYAmbientesAsignados()
        Me.dpMarcarPor.SelectedValue = -1
        Me.txtCompararCon.Text = ""
    End Sub

    Private Sub ConsultarCursosYAmbientesAsignados()
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        '=================================
        'Cargar Grupos Programados
        '=================================
        Me.gvCursosProgramados.DataSource = obj.TraerDataTable("ACAD_ConsultarCursosHorariosAsignadosxDpto", Me.dpCodigo_cac.SelectedValue, Me.dpCodigo_Dac.SelectedValue, Me.dpEstado.SelectedValue, Me.dpTipoAmbiente.SelectedValue)
        Me.gvCursosProgramados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub


    Protected Sub gvCursosProgramados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvCursosProgramados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")

            If e.Row.Cells(7).Text = "Abierto" Then
                e.Row.Cells(7).ForeColor = Drawing.Color.Blue
            Else
                e.Row.Cells(7).ForeColor = Drawing.Color.Red
            End If

            If e.Row.Cells(4).Text = True Then
                e.Row.Cells(4).Text = "Si"
            Else
                e.Row.Cells(4).Text = "No"
            End If

            Select Case e.Row.Cells(11).Text
                Case "A" : e.Row.Cells(11).ForeColor = Drawing.Color.Blue
                Case "R" : e.Row.Cells(11).ForeColor = Drawing.Color.Green
                Case "P" : e.Row.Cells(11).ForeColor = Drawing.Color.Red
                Case "E" : e.Row.Cells(11).ForeColor = Drawing.Color.Gray
            End Select

        End If
    End Sub

    Protected Sub gvCursosProgramados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCursosProgramados.RowDeleting
        Dim codigo_cup, codigo_lho As Integer
        Try
            codigo_cup = gvCursosProgramados.DataKeys.Item(e.RowIndex).Values(0)
            codigo_lho = gvCursosProgramados.DataKeys.Item(e.RowIndex).Values(1)
            ActualizarEstadoAmbiente(codigo_cup, codigo_lho, "E")

            ScriptManager.RegisterStartupScript(Me.Page, "string".GetType(), "Programar", "alert('Se liberó el ambiente " & Me.gvCursosProgramados.Rows(e.RowIndex).Cells(8).Text & " en el horario " & Me.gvCursosProgramados.Rows(e.RowIndex).Cells(10).Text & " correctamente');", True)
            ConsultarCursosYAmbientesAsignados()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Error", "alert('Ocurrió un error al procesar los datos');", True)
        End Try
    End Sub

    Private Sub ActualizarEstadoAmbiente(ByVal codigo_cup As Integer, ByVal codigo_lho As Integer, ByVal estado_lho As String)
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("ACAD_ActualizarEstadoAmbiente", codigo_cup, codigo_lho, estado_lho)
        obj.CerrarConexion()
    End Sub

    Protected Sub cmdAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAsignar.Click
        Dim i As Integer
        Dim codigo_cup, codigo_lho As Integer
        Dim Fila As GridViewRow

        For i = 0 To Me.gvCursosProgramados.Rows.Count - 1
            Fila = Me.gvCursosProgramados.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    codigo_cup = gvCursosProgramados.DataKeys.Item(i).Values(0)
                    codigo_lho = gvCursosProgramados.DataKeys.Item(i).Values(1)
                    ActualizarEstadoAmbiente(codigo_cup, codigo_lho, "A")
                End If
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, "string".GetType(), "Asignar", "alert('Se asignaron ambientes a los cursos seleccionados');", True)
        ConsultarCursosYAmbientesAsignados()
    End Sub

    Protected Sub cmdReasignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReasignar.Click
        Dim i As Integer
        Dim codigo_cup, codigo_lho As Integer
        Dim Fila As GridViewRow

        For i = 0 To Me.gvCursosProgramados.Rows.Count - 1
            Fila = Me.gvCursosProgramados.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    codigo_cup = gvCursosProgramados.DataKeys.Item(i).Values(0)
                    codigo_lho = gvCursosProgramados.DataKeys.Item(i).Values(1)
                    ActualizarEstadoAmbiente(codigo_cup, codigo_lho, "R")
                End If
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, "string".GetType(), "Reasignar", "alert('Se marcaron los ambientes por reasignar de los cursos seleccionados');", True)
        ConsultarCursosYAmbientesAsignados()
    End Sub


    Protected Sub cmdLiberar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLiberar.Click
        Dim i As Integer
        Dim codigo_cup, codigo_lho As Integer
        Dim Fila As GridViewRow

        For i = 0 To Me.gvCursosProgramados.Rows.Count - 1
            Fila = Me.gvCursosProgramados.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                If CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True Then
                    codigo_cup = gvCursosProgramados.DataKeys.Item(i).Values(0)
                    codigo_lho = gvCursosProgramados.DataKeys.Item(i).Values(1)
                    ActualizarEstadoAmbiente(codigo_cup, codigo_lho, "E")
                End If
            End If
        Next
        ScriptManager.RegisterStartupScript(Me.Page, "string".GetType(), "Eliminar", "alert('Se liberaron los ambientes asignados a los cursos marcados');", True)
        ConsultarCursosYAmbientesAsignados()
    End Sub

    Protected Sub cboMarcarPor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpMarcarPor.SelectedIndexChanged
        If dpMarcarPor.SelectedValue = 3 Or Me.dpMarcarPor.SelectedValue = -1 Or Me.dpMarcarPor.SelectedValue = 4 Then
            Me.cboSigno.Visible = False
            Me.txtCompararCon.Visible = False
        Else
            Me.cboSigno.Visible = True
            Me.txtCompararCon.Visible = True
        End If
    End Sub

    Protected Sub cmdMarcar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMarcar.Click
        DesmarcarCheck()
        If dpMarcarPor.SelectedValue >= 0 Then

            Dim i As Integer
            Dim codigo_cup, codigo_lho As Integer
            Dim Fila As GridViewRow
            Dim valorAComparar, valorAComparar1 As Object
            valorAComparar = 0
            valorAComparar1 = 0

            For i = 0 To Me.gvCursosProgramados.Rows.Count - 1
                Fila = Me.gvCursosProgramados.Rows(i)
                If Fila.RowType = DataControlRowType.DataRow Then
                    Select Case dpMarcarPor.SelectedValue
                        Case 0 : valorAComparar = Fila.Cells(6).Text
                        Case 1 : valorAComparar = Fila.Cells(5).Text / Fila.Cells(9).Text * 100
                        Case 2
                            valorAComparar = (Fila.Cells(5).Text / Fila.Cells(9).Text) * 100
                            valorAComparar1 = (Fila.Cells(6).Text / Fila.Cells(5).Text) * 100
                    End Select

                    Select Case dpMarcarPor.SelectedValue
                        Case 0, 1
                            If Me.cboSigno.SelectedValue = "=" Then
                                If valorAComparar = Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            ElseIf Me.cboSigno.SelectedValue = ">=" Then
                                If valorAComparar >= Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            ElseIf Me.cboSigno.SelectedValue = "<=" Then
                                If valorAComparar <= Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            End If

                        Case 2
                            If Me.cboSigno.SelectedValue = "=" Then
                                If valorAComparar = Me.txtCompararCon.Text And valorAComparar1 = Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            ElseIf Me.cboSigno.SelectedValue = ">=" Then
                                If valorAComparar >= Me.txtCompararCon.Text And valorAComparar1 >= Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            ElseIf Me.cboSigno.SelectedValue = "<=" Then
                                If valorAComparar <= Me.txtCompararCon.Text And valorAComparar1 <= Me.txtCompararCon.Text Then
                                    CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                                End If
                            End If

                        Case 3
                            If Fila.Cells(4).Text = "Si" Then
                                CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True
                            End If

                        Case 4
                            CType(Fila.FindControl("chkElegir"), CheckBox).Checked = True

                    End Select
                End If
            Next

        End If
    End Sub

    Private Sub DesmarcarCheck()
        Dim i As Integer
        Dim Fila As GridViewRow

        For i = 0 To Me.gvCursosProgramados.Rows.Count - 1
            Fila = Me.gvCursosProgramados.Rows(i)
            If Fila.RowType = DataControlRowType.DataRow Then
                CType(Fila.FindControl("chkElegir"), CheckBox).Checked = False
            End If
        Next
    End Sub
End Class
