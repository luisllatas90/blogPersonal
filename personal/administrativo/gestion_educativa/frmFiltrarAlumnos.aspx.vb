Imports ClsPensiones
Imports System.Collections.Generic

Partial Class administrativo_gestion_educativa_frmFiltrarAlumnos
    Inherits System.Web.UI.Page

#Region "Propiedades"
    'ENTIDADES
    Private me_CicloAcacemico As New e_PenCicloAcademico

    'DATOS
    Private md_Pensiones As New d_Pensiones
    Private md_Cpc As New d_PenConfiguracionProgramacionCargo
    Private md_CicloAcademico As New d_PenCicloAcademico
    Private md_TipoEstudio As New d_PenTipoEstudio
    Private md_CarreraProfesional As New d_PenCarreraProfesional

    'OTRAS VARIABLES
    Dim lo_DivFiltros As New List(Of String)
#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            mt_Init()

            Dim tipoForm As String = Request.QueryString("tipo")
            If tipoForm <> "" Then
                hddTipoForm.Value = tipoForm
            End If

            Dim filtros As String = Request.QueryString("filtros")
            If filtros Is Nothing Then
                filtros = ""
            End If
            hddCadenaFiltros.Value = filtros
            mt_SeleccionarFiltros()

            Dim incluirControles As String = IIf(Not String.IsNullOrEmpty(Request.QueryString("incluirControles")), Request.QueryString("incluirControles"), "")
            Dim excluirControles As String = IIf(Not String.IsNullOrEmpty(Request.QueryString("excluirControles")), Request.QueryString("excluirControles"), "")

            If incluirControles <> "" Then
                mt_MostrarControles(incluirControles)
            ElseIf excluirControles <> "" Then
                mt_OcultarControles(excluirControles)
            End If

        Else
            mt_LimpiarMensajeServidor()
            mt_LimpiarParametrosMensaje()
        End If


    End Sub

    Protected Sub cmbTipoEstudio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoEstudio.SelectedIndexChanged
        mt_CargarFiltroCarreraProfesional()
    End Sub

    Protected Sub btnGenerarCadenaFiltros_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerarCadenaFiltros.ServerClick
        mt_GenerarCadenaFiltros()
    End Sub
#End Region

#Region "Métodos"
    Private Sub mt_Init()
        Try
            mt_CargarSelectores()

            lo_DivFiltros.Add("divTipoEstudio")
            lo_DivFiltros.Add("divSemestreInscripcion")
            lo_DivFiltros.Add("divCarreraProfesional")
            lo_DivFiltros.Add("divSemestreInscripcion")
            lo_DivFiltros.Add("divCarreraProfesional")
            lo_DivFiltros.Add("divExcluirCarreraProfesional")
            lo_DivFiltros.Add("divCondicion")
            lo_DivFiltros.Add("divAlcanzoVacante")
            lo_DivFiltros.Add("divTieneDeuda")
            lo_DivFiltros.Add("divCicloAcademico")
            lo_DivFiltros.Add("divContinuador")
            lo_DivFiltros.Add("divInscritoCicloActual")
            lo_DivFiltros.Add("divEgresado")
            lo_DivFiltros.Add("divMatriculaExcepcion")
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try

    End Sub

    Private Sub mt_CargarSelectores()
        Try
            Dim lo_Dt As New Data.DataTable

            Dim le_TipoEstudio As New e_PenTipoEstudio
            With le_TipoEstudio
                .operacion = "TO"
                .codigoTest = "0"
            End With
            lo_Dt = md_TipoEstudio.Listar(le_TipoEstudio)
            ClsFunciones.LlenarListas(cmbTipoEstudio, lo_Dt, "codigo_test", "descripcion_test", "-- SELECCIONE --")

            Dim le_CicloAcademico As New e_PenCicloAcademico
            With le_CicloAcademico
                .operacion = "GEN"
                .tipo_Cac = "N"
            End With
            lo_Dt = md_CicloAcademico.Listar(le_CicloAcademico)
            ClsFunciones.LlenarListas(cmbCicloAcademico, lo_Dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
            ClsFunciones.LlenarListas(cmbCicloIngreso, lo_Dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")

            lo_Dt.Dispose()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_CargarFiltroCarreraProfesional()
        Try
            Dim le_CarreraProfesional As New e_PenCarreraProfesional
            With le_CarreraProfesional
                .codigoCpf = "0"
                .codigoTest = cmbTipoEstudio.SelectedValue
            End With
            Dim lo_Dt As Data.DataTable = md_CarreraProfesional.Listar(le_CarreraProfesional)

            Dim ln_RemoveIndex As Integer = -1
            For i As Integer = 0 To lo_Dt.Rows.Count - 1
                ln_RemoveIndex += 1
                If lo_Dt.Rows(i).Item("codigo_cpf") = "0" Then
                    Exit For
                End If
            Next

            If ln_RemoveIndex > -1 Then
                lo_Dt.Rows.RemoveAt(ln_RemoveIndex)
            End If

            ClsFunciones.LlenarListas(cmbCarreraProfesional, lo_Dt, "codigo_cpf", "nombre_cpf")
            udpCarreraProfesional.Update()

            ClsFunciones.LlenarListas(cmbExcluirCarreraProfesional, lo_Dt, "codigo_cpf", "nombre_cpf")
            udpExcluirCarreraProfesional.Update()

            lo_Dt.Dispose()
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_SeleccionarFiltros()
        Try
            If hddCadenaFiltros.Value.Trim = "" Then
                Exit Sub
            End If

            Dim la_Filtros As String() = hddCadenaFiltros.Value.Split("|")
            For Each f As String In la_Filtros
                Dim la_filtro As String() = f.Split("=")
                Dim ls_Key As String = la_filtro(0)
                Dim ls_Value As String = la_filtro(1)

                If ls_Value <> "" Then
                    Select Case ls_Key
                        Case "codigo_test"
                            cmbTipoEstudio.SelectedValue = ls_Value : cmbTipoEstudio_SelectedIndexChanged(Nothing, Nothing)
                        Case "descrCicloIngreso"
                            Dim lo_Cac As New e_PenCicloAcademico
                            lo_Cac.descripcion_Cac = ls_Value
                            lo_Cac.operacion = "GEN"
                            Dim dt_Cac As Data.DataTable = md_CicloAcademico.Listar(lo_Cac)
                            If dt_Cac.Rows.Count = 0 Then
                                mt_GenerarMensajeServidor("Error", -1, "No se ha encontrado ciclo académico de nombre " & ls_Value)
                                Exit Sub
                            End If
                            cmbCicloIngreso.SelectedValue = dt_Cac.Rows(0).Item("codigo_cac")
                        Case "codigo_cpf"
                            cmbCarreraProfesional.SelectedValue = ls_Value
                        Case "excluirCodigo_cpf"
                            cmbExcluirCarreraProfesional.SelectedValue = ls_Value
                        Case "condicion_alu"
                            Dim la_Condicion As String() = ls_Value.Split(",")
                            For Each i As ListItem In cmbCondicion.Items
                                i.Selected = (Array.IndexOf(la_Condicion, i.Value) > -1)
                            Next
                        Case "descrCicloActual"
                            Dim lo_Cac As New e_PenCicloAcademico
                            lo_Cac.descripcion_Cac = ls_Value
                            lo_Cac.operacion = "GEN"
                            Dim dt_Cac As Data.DataTable = md_CicloAcademico.Listar(lo_Cac)
                            If dt_Cac.Rows.Count = 0 Then
                                mt_GenerarMensajeServidor("Error", -1, "No se ha encontrado ciclo académico de nombre " & ls_Value)
                                Exit Sub
                            End If
                            cmbCicloAcademico.SelectedValue = dt_Cac.Rows(0).Item("codigo_cac")
                        Case "alcanzoVacante_alu"
                            rbtSiAlcanzoVacante.Checked = (ls_Value = "1")
                            rbtNoAlcanzoVacante.Checked = (ls_Value = "0")
                            rbtAmbosAlcanzoVacante.Checked = False
                        Case "estadoDeuda_alu"
                            rbtSiTieneDeuda.Checked = (ls_Value = "1")
                            rbtNoTieneDeuda.Checked = (ls_Value = "0")
                            rbtAmbosTieneDeuda.Checked = False
                        Case "esContinuador"
                            rbtSiContinuador.Checked = (ls_Value = "1")
                            rbtNoContinuador.Checked = (ls_Value = "0")
                            rbtAmbosContinuador.Checked = False
                        Case "inscritoCicloActual"
                            rbtSiInscritoCicloActual.Checked = (ls_Value = "1")
                            rbtNoInscritoCicloActual.Checked = (ls_Value = "0")
                            rbtAmbosInscritoCicloActual.Checked = False
                        Case "esEgresado"
                            rbtSiEgresado.Checked = (ls_Value = "1")
                            rbtNoEgresado.Checked = (ls_Value = "0")
                            rbtAmbosEgresado.Checked = False
                        Case "matriculaExcepcion"
                            rbtSiMatriculaExcepcion.Checked = (ls_Value = "1")
                            rbtNoMatriculaExcepcion.Checked = (ls_Value = "0")
                            rbtAmbosMatriculaExcepcion.Checked = False
                    End Select
                Else
                    Select Case ls_Key
                        Case "alcanzoVacante_alu"
                            rbtAmbosAlcanzoVacante.Checked = True
                        Case "rbtAmbosTieneDeuda"
                            rbtAmbosTieneDeuda.Checked = True
                        Case "esContinuador"
                            rbtAmbosContinuador.Checked = True
                        Case "inscritoCicloActual"
                            rbtAmbosInscritoCicloActual.Checked = True
                        Case "esEgresado"
                            rbtAmbosEgresado.Checked = True
                        Case "matriculaExcepcion"
                            rbtAmbosMatriculaExcepcion.Checked = True
                    End Select
                End If
            Next

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Se muestran solo los controles que se envían en el parámetro incluirControles
    Private Sub mt_MostrarControles(ByVal ls_Filtros As String)
        Try
            Dim la_Filtros As String() = ls_Filtros.Split("|")
            For Each ls_ControlId As String In lo_DivFiltros
                Dim lo_Control As HtmlControl = Me.FindControl(ls_ControlId)
                If Array.IndexOf(la_Filtros, ls_ControlId.Replace("div", "")) = -1 Then
                    lo_Control.Attributes.Item("class") = lo_Control.Attributes.Item("class").Replace("d-flex", "d-none")
                End If
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    'Se ocultan solo los controles que se envían en el parámetro excluirControles
    Private Sub mt_OcultarControles(ByVal ls_Filtros As String)
        Try
            Dim la_Filtros As String() = ls_Filtros.Split("|")
            For Each ls_ControlId As String In lo_DivFiltros
                Dim lo_Control As HtmlControl = Me.FindControl(ls_ControlId)
                If Array.IndexOf(la_Filtros, ls_ControlId.Replace("div", "")) > -1 Then
                    lo_Control.Attributes.Item("class") = lo_Control.Attributes.Item("class").Replace("d-flex", "d-none")
                End If
            Next
        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub

    Private Sub mt_GenerarMensaje(ByVal rpta As Integer, ByVal msg As String, ByVal titulo As String, Optional ByVal control As String = "")
        If hddTipoForm.Value = "N" Then
            mt_GenerarMensajeServidor(titulo, rpta, msg)
        Else
            mt_GenerarParametrosMensaje(rpta, msg, control)
        End If
    End Sub

    Private Sub mt_GenerarParametrosMensaje(ByVal rpta As Integer, ByVal msg As String, ByVal control As String)
        Try
            hddRpta.Value = rpta
            hddMsg.Value = msg
            hddControl.Value = control
            udpParametrosMensaje.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarParametrosMensaje()
        Try
            hddRpta.Value = ""
            hddMsg.Value = ""
            udpParametrosMensaje.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_GenerarMensajeServidor(ByVal ls_Titulo As String, ByVal ln_Rpta As Integer, ByVal ls_Mensaje As String)
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "true"
            divMenServParametros.Attributes.Item("data-rpta") = ln_Rpta
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ls_Titulo
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ls_Mensaje
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_LimpiarMensajeServidor()
        Try
            divMenServParametros.Attributes.Item("data-mostrar") = "false"
            divMenServParametros.Attributes.Item("data-rpta") = ""
            udpMenServParametros.Update()

            spnMenServTitulo.InnerHtml = ""
            udpMenServHeader.Update()

            divMenServMensaje.InnerHtml = ""
            udpMenServBody.Update()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_GenerarCadenaFiltros()
        Try
            'VALIDACIONES
            Dim codigo_test As Integer = cmbTipoEstudio.SelectedValue
            Dim codigoCac As Integer = cmbCicloAcademico.SelectedValue

            If codigo_test = -1 Then
                mt_GenerarMensaje(0, "Debe seleccionar tipo de estudio", "Advertencia", "cmbTipoEstudio")
                Exit Sub
            End If

            If Not rbtAmbosContinuador.Checked AndAlso codigoCac = -1 Then
                'mt_GenerarMensajeServidor("Advertencia", 0, "Para filtrar por condición ""Continuador"" debe seleccionar un ciclo académico")
                mt_GenerarMensaje(0, "Para filtrar por condición ""Continuador"" debe seleccionar un ciclo académico", "Advertencia", "cmbCicloAcademico")
                Exit Sub
            End If

            If Not rbtAmbosInscritoCicloActual.Checked AndAlso codigoCac = -1 Then
                'mt_GenerarMensajeServidor("Advertencia", 0, "Para filtrar por condición ""Inscrito en Ciclo Actual"" debe seleccionar un ciclo académico")
                mt_GenerarMensaje(0, "Para filtrar por condición ""Inscrito en Ciclo Actual"" debe seleccionar un ciclo académico", "Advertencia", "cmbCicloAcademico")
                Exit Sub
            End If

            If Not rbtAmbosMatriculaExcepcion.Checked AndAlso codigoCac = -1 Then
                'mt_GenerarMensajeServidor("Advertencia", 0, "Para filtrar por condición ""Matrícula Excepción"" debe seleccionar un ciclo académico")
                mt_GenerarMensaje(0, "Para filtrar por condición ""Matrícula Excepción"" debe seleccionar un ciclo académico", "Advertencia", "cmbCicloAcademico")
                Exit Sub
            End If

            Dim lo_DtAlumnos As New Data.DataTable
            Dim filtrosAlumnoCpc As String = ""

            Dim descrCicloActual As String = IIf(codigoCac = -1, "", cmbCicloAcademico.SelectedItem.Text.Trim)
            Dim descrCicloIngreso As String = IIf(cmbCicloIngreso.SelectedValue = -1, "", cmbCicloIngreso.SelectedItem.Text.Trim)

            Dim codigoCpf As String = ""
            For Each _item As ListItem In cmbCarreraProfesional.Items
                If _item.Selected Then
                    If codigoCpf <> "" Then
                        codigoCpf &= ","
                    End If
                    codigoCpf &= _item.Value
                End If
            Next

            Dim excluirCodigoCpf As String = ""
            For Each _item As ListItem In cmbExcluirCarreraProfesional.Items
                If _item.Selected Then
                    If excluirCodigoCpf <> "" Then
                        excluirCodigoCpf &= ","
                    End If
                    excluirCodigoCpf &= _item.Value
                End If
            Next

            Dim condicion_alu As String = ""
            For Each _item As ListItem In cmbCondicion.Items
                If _item.Selected Then
                    If condicion_alu <> "" Then
                        condicion_alu &= ","
                    End If
                    condicion_alu &= _item.Value
                End If
            Next

            Dim alcanzoVacante_alu As String = ""
            If rbtSiAlcanzoVacante.Checked Then
                alcanzoVacante_alu = rbtSiAlcanzoVacante.Value
            ElseIf rbtNoAlcanzoVacante.Checked Then
                alcanzoVacante_alu = rbtNoAlcanzoVacante.Value
            End If

            Dim estadoDeuda_alu As String = ""
            If rbtSiTieneDeuda.Checked Then
                estadoDeuda_alu = rbtSiTieneDeuda.Value
            ElseIf rbtNoTieneDeuda.Checked Then
                estadoDeuda_alu = rbtNoTieneDeuda.Value
            End If

            Dim esContinuador As String = ""
            If rbtSiContinuador.Checked Then
                esContinuador = rbtSiContinuador.Value
            ElseIf rbtNoContinuador.Checked Then
                esContinuador = rbtNoContinuador.Value
            End If

            Dim inscritoCicloActual As String = ""
            If rbtSiInscritoCicloActual.Checked Then
                inscritoCicloActual = rbtSiInscritoCicloActual.Value
            ElseIf rbtNoInscritoCicloActual.Checked Then
                inscritoCicloActual = rbtNoInscritoCicloActual.Value
            End If

            Dim esEgresado As String = ""
            If rbtSiEgresado.Checked Then
                esEgresado = rbtSiEgresado.Value
            ElseIf rbtNoEgresado.Checked Then
                esEgresado = rbtNoEgresado.Value
            End If

            Dim matriculaExcepcion As String = ""
            If rbtSiMatriculaExcepcion.Checked Then
                matriculaExcepcion = rbtSiMatriculaExcepcion.Value
            ElseIf rbtNoMatriculaExcepcion.Checked Then
                matriculaExcepcion = rbtNoMatriculaExcepcion.Value
            End If

            filtrosAlumnoCpc = "codigo_test=" & codigo_test _
                            & "|descrCicloIngreso=" & descrCicloIngreso _
                            & "|codigo_cpf=" & codigoCpf _
                            & "|excluirCodigo_cpf=" & excluirCodigoCpf _
                            & "|condicion_alu=" & condicion_alu _
                            & "|descrCicloActual=" & descrCicloActual

            If Not rbtAmbosAlcanzoVacante.Checked Then
                filtrosAlumnoCpc &= "|alcanzoVacante_alu=" & alcanzoVacante_alu
            End If

            If Not rbtAmbosTieneDeuda.Checked Then
                filtrosAlumnoCpc &= "|estadoDeuda_alu=" & estadoDeuda_alu
            End If

            If Not rbtAmbosContinuador.Checked Then
                filtrosAlumnoCpc &= "|esContinuador=" & esContinuador
            End If

            If Not rbtAmbosInscritoCicloActual.Checked Then
                filtrosAlumnoCpc &= "|inscritoCicloActual=" & inscritoCicloActual
            End If

            If Not rbtAmbosEgresado.Checked Then
                filtrosAlumnoCpc &= "|esEgresado=" & esEgresado
            End If

            If Not rbtAmbosMatriculaExcepcion.Checked Then
                filtrosAlumnoCpc &= "|matriculaExcepcion=" & matriculaExcepcion
            End If

            hddCadenaFiltros.Value = filtrosAlumnoCpc
            udpControlesOcultos.Update()

        Catch ex As Exception
            mt_GenerarMensajeServidor("Error", -1, ex.Message)
        End Try
    End Sub
#End Region

End Class
