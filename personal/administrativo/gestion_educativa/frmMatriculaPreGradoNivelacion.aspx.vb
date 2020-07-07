Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmMatriculaPreGradoNivelacion
    Inherits System.Web.UI.Page

#Region "Variables"
    Private mo_Repo As New ClsMatriculaPregradoNivelacion
    Private mn_CodigoPer As Integer = 0
    Private mn_CodigoTfu As Integer = 0
    Private mn_CodigoTest As Integer = 0
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mn_CodigoPer = Request.QueryString("id")
        mn_CodigoTfu = Request.QueryString("ctf")
        mn_CodigoTest = Request.QueryString("mod")

        ViewState("nroVacantes") = IIf(ViewState("nroVacantes") = Nothing, 0, ViewState("nroVacantes"))

        If Not IsPostBack Then
            CargarCombos()
        Else
            RefrescarGrillaAlumnos()
        End If

        EstadoBotonera()
        LimpiarMensajeServidor()
    End Sub

    Protected Sub cmbPlanEstudios_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlanEstudios.SelectedIndexChanged
        CargarComboGrupoHorario()
        CargarComboCursosProgramados()
        CargarGrillaAlumnos()
    End Sub

    Protected Sub cmbCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCicloAcademico.SelectedIndexChanged
        CargarComboGrupoHorario()
        CargarComboCursosProgramados()
        CargarGrillaAlumnos()
    End Sub

    Protected Sub cmbCarreraProfesional_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCarreraProfesional.SelectedIndexChanged
        CargarGrillaAlumnos()
    End Sub

    Protected Sub cmbGrupoHorario_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGrupoHorario.SelectedIndexChanged
        CargarComboCursosProgramados()
        cmbCursosProgramados_SelectedIndexChanged(Nothing, Nothing)

        ObtenerVacantes()
    End Sub

    Protected Sub cmbCursosProgramados_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCursosProgramados.SelectedIndexChanged
        CargarGrillaAlumnos()
        ObtenerVacantes()
    End Sub

    Protected Sub cmbEstadoPagoMatricula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEstadoPagoMatricula.SelectedIndexChanged
        CargarGrillaAlumnos()
    End Sub

    Protected Sub btnListar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.ServerClick
        CargarGrillaAlumnos()
    End Sub

    Protected Sub grwAlumnos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwAlumnos.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim _cellsRow As TableCellCollection = e.Row.Cells
                Dim index As Integer = e.Row.RowIndex + 1
                Dim columnas As Integer = grwAlumnos.Columns.Count
                Dim matriculado As String = grwAlumnos.DataKeys(e.Row.RowIndex).Values.Item("matriculado").ToString.Trim
                Dim estadoActualAlu As String = grwAlumnos.DataKeys(e.Row.RowIndex).Values.Item("estadoActual_alu").ToString.Trim
                Dim pagoMatricula As String = grwAlumnos.DataKeys(e.Row.RowIndex).Values.Item("pagoMatricula").ToString.Trim
                Dim ln_codigoAlu As Integer = grwAlumnos.DataKeys(e.Row.RowIndex).Values.Item("codigo_alu")

                If matriculado = "Si" Then
                    e.Row.CssClass = "table-info matriculado"
                End If

                If matriculado = "Si" OrElse estadoActualAlu = "0" OrElse pagoMatricula = "No" Then
                    Dim chkElegir As HtmlInputCheckBox = e.Row.FindControl("chkElegir")
                    chkElegir.Visible = False

                    Dim lblHeader As Label = e.Row.FindControl("lblHeader")
                    lblHeader.Visible = False
                End If

                _cellsRow(1).Text = index

                'Retirar Matricula 
                If matriculado = "Si" Then
                    Dim lo_btnRetirar As New HtmlButton
                    With lo_btnRetirar
                        .ID = "btnRetirar" & index
                        .Attributes.Add("data-alu", ln_codigoAlu)
                        .Attributes.Add("class", "btn btn-danger btn-sm")
                        .Attributes.Add("type", "button")
                        .InnerHtml = "<i class='fa fa-minus-circle'></i>"
                        AddHandler .ServerClick, AddressOf btnRetirar_Click
                    End With
                    _cellsRow(columnas - 1).Controls.Add(lo_btnRetirar)
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Este método me permite llamar manualmente al evento RowDataBound que vuelve a reenderizar los botones de acción
    Private Sub RefrescarGrillaAlumnos()
        For Each _Row As GridViewRow In grwAlumnos.Rows
            grwAlumnos_RowDataBound(grwAlumnos, New GridViewRowEventArgs(_Row))
        Next
    End Sub

    Protected Sub btnMatricular_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMatricular.ServerClick
        Matricular()
    End Sub

    '20200306-ENevado
    ''' <summary>
    ''' Método para Retirar Curso Matricular
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRetirarMatricula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRetirarMatricula.ServerClick
        Try
            Dim dtRpta As New System.Data.DataTable
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoAlu As String = button.Attributes("data-alu")
            Dim ls_CodigosCup As String = button.Attributes("data-cup")
            Dim usuario As String = Session("perlogin").ToString
            'GenerarMensajeServidor("Respuesta", "1", ls_CodigosCup)
            dtRpta = mo_Repo.RetirarMatriculaCurso(ls_CodigoAlu, ls_CodigosCup, usuario)
            If dtRpta.Rows.Count > 0 Then
                If dtRpta.Rows(0).Item(0) = 1 Then
                    GenerarMensajeServidor("Respuesta", "1", "Se realizó la operación correctamente")
                    CargarComboCursosProgramados(True)
                    CargarGrillaAlumnos()
                    ObtenerVacantes()
                Else
                    GenerarMensajeServidor("Respuesta", "0", dtRpta.Rows(0).Item(1))
                End If
            Else
                GenerarMensajeServidor("Error", "0", "Ocurrió un error en el proceso")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '20200305-ENevado
    ''' <summary>
    ''' Método para Mostar un Modal con los Cursos Matriculados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRetirar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim button As HtmlButton = DirectCast(sender, HtmlButton)
            Dim ls_CodigoAlu As String = button.Attributes("data-alu")
            Dim codigoPes As Integer = cmbPlanEstudios.SelectedValue
            Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
            Dim codigosCup As String = ""
            Dim arrVD As String = ""
            Dim contCodigosCup As Integer = 0
            Dim dtResultado As New System.Data.DataTable, dtFinal As New System.Data.DataTable
            For Each _Item As ListItem In cmbCursosProgramados.Items
                If _Item.Selected AndAlso _Item.Value <> "-1" Then
                    Dim codigoCur As Integer = _Item.Value
                    dtResultado = mo_Repo.BuscarCursoProgramadoMatriculado(codigoPes, codigoCac, codigoCur, ls_CodigoAlu)
                    If dtResultado.Rows.Count > 0 Then
                        codigosCup &= dtResultado.Rows(0).Item("codigo_cup") & ","
                        arrVD &= "0,"
                        contCodigosCup += 1
                    End If
                    If dtFinal.Columns.Count = 0 Then
                        For Each _col As System.Data.DataColumn In dtResultado.Columns
                            dtFinal.Columns.Add(_col.ColumnName, _col.DataType)
                        Next
                    End If
                    For Each _row As System.Data.DataRow In dtResultado.rows
                        dtFinal.ImportRow(_row)
                    Next
                End If
            Next
            CargarCursosMatriculados(ls_CodigoAlu, codigosCup, dtFinal)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Métodos"

    Private Sub LimpiarCombo(ByVal combo As DropDownList)
        Try
            With combo
                combo.Items.Clear()
                combo.Items.Add("-- Seleccione --")
                combo.Items(0).Value = "-1"
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LimpiarCombo(ByVal combo As ListBox)
        Try
            With combo
                combo.Items.Clear()
                combo.Items.Add("-- Seleccione --")
                combo.Items(0).Value = "-1"
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarCombos()
        Try
            CargarComboPlanEstudios()
            CargarComboCicloAcademico()
            CargarComboCarreraProfesional()
            CargarComboGrupoHorario()
            CargarComboCursosProgramados()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboPlanEstudios()
        Try
            Dim tipo As String = "CV"
            Dim param1 As String = "1" 'Vigente
            Dim param2 As String = "36" 'Carrera: ESCUELA PRE
            ClsFunciones.LlenarListas(cmbPlanEstudios, mo_Repo.ListarPlanEstudio(tipo, param1, param2), "codigo_pes", "descripcion_pes", "-- Seleccione --")
            udpPlanEstudios.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboCicloAcademico()
        Try
            Dim tipo As String = "TO"
            Dim param1 As String = "0"
            ClsFunciones.LlenarListas(cmbCicloAcademico, mo_Repo.ListarCicloAcademico(tipo, param1), "codigo_cac", "descripcion_cac", "-- Seleccione --")
            For Each _Item As ListItem In cmbCicloAcademico.Items
                If _Item.Value <> "-1" Then
                    '_Item.Selected = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboCarreraProfesional()
        Try
            'Tipo de estudio: PREGRADO
            Dim codigoTest As Integer = 2
            Dim codigoStest As Integer = 1
            ClsFunciones.LlenarListas(cmbCarreraProfesional, mo_Repo.ListarCarreraProfesional(codigoTest, codigoStest), "codigo_Cpf", "nombre_Cpf", "-- TODAS --")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboGrupoHorario()
        Try
            Dim codigoPes As String = cmbPlanEstudios.SelectedValue
            Dim codigoCac As String = cmbCicloAcademico.SelectedValue

            If codigoPes <> "-1" AndAlso codigoCac <> "-1" Then
                ClsFunciones.LlenarListas(cmbGrupoHorario, mo_Repo.ListarGrupoHorario(codigoPes, codigoCac), "grupoHor_Cup", "grupoHor_Cup", "-- Seleccione --")
            Else
                LimpiarCombo(cmbGrupoHorario)
            End If
            udpGrupoHorario.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CargarComboCursosProgramados(Optional ByVal mantenerValores As Boolean = False)
        Try
            Dim codigoPes As String = cmbPlanEstudios.SelectedValue
            Dim codigoCac As String = cmbCicloAcademico.SelectedValue
            Dim valGrupoHorario As String = cmbGrupoHorario.SelectedValue

            If codigoPes <> "-1" AndAlso codigoCac <> "-1" AndAlso valGrupoHorario <> "-1" Then
                Dim cursosSeleccionados As String = ""

                If mantenerValores Then
                    For Each _Item As ListItem In cmbCursosProgramados.Items
                        If _Item.Selected Then
                            If cursosSeleccionados.Length > 0 Then cursosSeleccionados &= ","
                            cursosSeleccionados &= _Item.Value
                        End If
                    Next
                End If

                ClsFunciones.LlenarListas(cmbCursosProgramados, mo_Repo.ListarCursoProgramado(codigoPes, codigoCac, valGrupoHorario), "codigo_cur", "nombre_cur")

                If mantenerValores Then
                    For Each _Item As ListItem In cmbCursosProgramados.Items
                        Dim seleccionados As String() = cursosSeleccionados.Split(",")
                        For i As Integer = 0 To seleccionados.Length - 1
                            If _Item.Value.Trim = seleccionados(i).Trim Then
                                _Item.Selected = True
                            End If
                        Next
                    Next
                End If

                For Each _Item As ListItem In cmbCursosProgramados.Items
                    Dim nombreGrupoHorario As String = cmbGrupoHorario.SelectedItem.Text

                    Dim codigoCur As Integer = _Item.Value
                    If codigoCur <> "-1" Then
                        Dim dtResultado As Data.DataTable = mo_Repo.BuscarCursoProgramadoNivelacion(codigoPes, codigoCac, codigoCur, nombreGrupoHorario)
                        If dtResultado.Rows.Count > 0 Then
                            Dim vacantesRestantes As Integer = dtResultado.Rows(0).Item("vacantes_restantes")
                            Dim cssClass As String = IIf(vacantesRestantes > 0, "good", "bad")
                            _Item.Attributes.Item("data-subtext") = "<span class=" & cssClass & ">| Vacantes: " & vacantesRestantes & "</span>"
                        End If
                    End If
                Next
            Else
                LimpiarCombo(cmbCursosProgramados)
            End If
            udpCursosProgramados.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub EstadoBotonera()
        Dim codigoPes As Integer = cmbPlanEstudios.SelectedValue
        Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
        Dim codigoCpf As Integer = cmbCarreraProfesional.SelectedValue
        Dim codigosCur As String = ""
        For Each _Item As ListItem In cmbCursosProgramados.Items
            If _Item.Selected AndAlso _Item.Value <> "-1" Then
                If codigosCur.Length > 0 Then codigosCur &= ","
                codigosCur &= _Item.Value
            End If
        Next
        Dim valueGrupoHorario As String = cmbGrupoHorario.SelectedValue

        Dim validacion As Boolean = codigoPes <> -1 AndAlso codigoCac <> -1 AndAlso codigosCur <> "" AndAlso valueGrupoHorario <> "-1"
        btnListar.Disabled = Not validacion
        btnMatricular.Disabled = Not validacion

        udpBotonera.Update()
    End Sub

    Private Sub CargarGrillaAlumnos()
        Try
            Dim codigoPes As Integer = cmbPlanEstudios.SelectedValue
            Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
            Dim codigoCpf As Integer = cmbCarreraProfesional.SelectedValue
            Dim codigosCur As String = ""
            For Each _Item As ListItem In cmbCursosProgramados.Items
                If _Item.Selected AndAlso _Item.Value <> "-1" Then
                    If codigosCur.Length > 0 Then codigosCur &= ","
                    codigosCur &= _Item.Value
                End If
            Next
            Dim valueGrupoHorario As String = cmbGrupoHorario.SelectedValue
            Dim pagoMatricula As Integer = cmbEstadoPagoMatricula.SelectedValue

            Dim dtAlumnos As New Data.DataTable
            If codigoPes <> -1 AndAlso codigoCac <> -1 AndAlso codigosCur <> "" AndAlso valueGrupoHorario <> "-1" Then
                Dim nombreGrupoHorario As String = cmbGrupoHorario.SelectedItem.Text
                dtAlumnos = mo_Repo.ListarAlumnosParaCursoProgramado(codigoPes, codigoCac, codigoCpf, codigosCur, nombreGrupoHorario, pagoMatricula)
            End If
            grwAlumnos.DataSource = dtAlumnos
            grwAlumnos.DataBind()
            udpAlumnos.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ObtenerVacantes()
        Try
            Dim codigoPes As Integer = cmbPlanEstudios.SelectedValue
            Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
            Dim valGrupoHorario As String = cmbGrupoHorario.SelectedValue

            If Not (codigoPes <> -1 AndAlso codigoCac <> -1 And valGrupoHorario <> "-1") Then
                Exit Sub
            End If

            Dim vacantesCup As Nullable(Of Integer)
            For Each _Item As ListItem In cmbCursosProgramados.Items
                If _Item.Selected AndAlso _Item.Value <> "-1" Then
                    Dim codigoCur As Integer = _Item.Value
                    Dim nombreGrupoHorario As String = cmbGrupoHorario.SelectedValue
                    Dim dtResultado As Data.DataTable = mo_Repo.BuscarCursoProgramadoNivelacion(codigoPes, codigoCac, codigoCur, nombreGrupoHorario)
                    If dtResultado.Rows.Count > 0 Then
                        If Not vacantesCup.HasValue Then vacantesCup = dtResultado.Rows(0).Item("vacantes_restantes")
                        Dim newVacantesCup As Integer = dtResultado.Rows(0).Item("vacantes_restantes")
                        vacantesCup = Math.Min(Integer.Parse(vacantesCup), newVacantesCup)
                        ViewState("nroVacantes") = vacantesCup
                    End If
                End If
            Next
            If Not vacantesCup.HasValue Then vacantesCup = 0

            spnVacantes.InnerHtml = "Nro máximo de matrículas permitidas: " & Integer.Parse(vacantesCup)
            spnVacantes.Attributes.Item("data-vacantes") = vacantesCup
            udpSpnVacantes.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function ValidarMatricula() As Dictionary(Of String, Object)
        Dim dctRespuesta As New Dictionary(Of String, Object)
        dctRespuesta.Item("rpta") = 1

        'Valido que haya al menos un alumno seleccionado
        Dim cantAlumnos As Integer = 0
        For Each _Row As GridViewRow In grwAlumnos.Rows
            cantAlumnos += IIf(DirectCast(_Row.FindControl("chkElegir"), HtmlInputCheckBox).Checked, 1, 0)
        Next
        If cantAlumnos = 0 Then
            dctRespuesta.Item("rpta") = 0
            dctRespuesta.Item("msg") = "No ha seleccionado ningún alumno"
        End If

        Return dctRespuesta
    End Function

    Private Sub Matricular()
        Try
            Dim dctValidacion As Dictionary(Of String, Object) = ValidarMatricula()
            If dctValidacion.Item("rpta") <> 1 Then
                GenerarMensajeServidor("Error", dctValidacion.Item("rpta"), dctValidacion.Item("msg"))
                Exit Sub
            End If

            For Each _row As GridViewRow In grwAlumnos.Rows
                If DirectCast(_row.FindControl("chkElegir"), HtmlInputCheckBox).Checked Then
                    Dim codigoAlu As Integer = grwAlumnos.DataKeys.Item(_row.RowIndex).Values("codigo_alu")
                    Dim codigoPes As Integer = cmbPlanEstudios.SelectedValue
                    Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue
                    Dim codigosCup As String = ""
                    Dim arrVD As String = ""
                    Dim contCodigosCup As Integer = 0
                    For Each _Item As ListItem In cmbCursosProgramados.Items
                        If _Item.Selected AndAlso _Item.Value <> "-1" Then
                            Dim codigoCur As Integer = _Item.Value
                            Dim nombreGrupoHorario As String = cmbGrupoHorario.SelectedValue
                            Dim dtResultado As Data.DataTable = mo_Repo.BuscarCursoProgramadoNivelacion(codigoPes, codigoCac, codigoCur, nombreGrupoHorario)
                            If dtResultado.Rows.Count > 0 Then
                                codigosCup &= dtResultado.Rows(0).Item("codigo_cup") & ","
                                arrVD &= "0,"
                                contCodigosCup += 1
                            End If
                        End If
                    Next

                    Dim usuario As String = Session("perlogin").ToString
                    mo_Repo.AgregarMatriculaWeb(codigoAlu, codigoPes, codigoCac, codigosCup, arrVD, usuario)
                End If
            Next
            GenerarMensajeServidor("Respuesta", "1", "Se realizó la operación correctamente")
            CargarComboCursosProgramados(True)
            CargarGrillaAlumnos()
            ObtenerVacantes()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ls_Rpta As String, ByVal ls_Msg As String, Optional ByVal ls_Control As String = "")
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "true"
        udpMensajeServidorParametros.Update()

        spnMensajeServidorTitulo.InnerHtml = ls_Titulo
        udpMensajeServidorHeader.Update()

        With divRespuestaPostback.Attributes
            .Item("data-rpta") = ls_Rpta
            .Item("data-msg") = ls_Msg
            .Item("data-control") = ls_Control
        End With
        udpMensajeServidorBody.Update()
    End Sub

    Private Sub LimpiarMensajeServidor()
        divMdlMenServParametros.Attributes.Item("data-mostrar") = "false"
        udpMensajeServidorParametros.Update()
    End Sub

    '20200306-ENevado
    ''' <summary>
    ''' Método para Mostar Cursos Matriculados para Retirar.
    ''' </summary>
    ''' <param name="ls_CodigoAlu">Codigo de Alumno</param>
    ''' <param name="ls_codigosCup">Arreglo de Codigos de Curso Programado</param>
    ''' <param name="lo_dtCurso">Lisatdo de Cursos Programados</param>
    ''' <remarks></remarks>
    Private Sub CargarCursosMatriculados(ByVal ls_CodigoAlu As String, ByVal ls_codigosCup As String, ByVal lo_dtCurso As Data.DataTable)
        grwCursosMatriculados.DataSource = lo_dtCurso
        grwCursosMatriculados.DataBind()
        Me.btnRetirarMatricula.Attributes.Add("data-alu", ls_CodigoAlu)
        Me.btnRetirarMatricula.Attributes.Add("data-cup", ls_codigosCup)
        divSinCursos.Visible = (lo_dtCurso.Rows.Count = 0)
        udpSinCursos.Update()
    End Sub

#End Region

End Class
