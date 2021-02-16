Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class InstrumentosEvaluacion_frmGestionEvaluacion
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES

    'DATOS
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0
    Dim codigo_tfu As Integer = 0

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing OrElse Session("perlogin") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")
            codigo_tfu = Request.QueryString("ctf")

            If IsPostBack = False Then
                Call mt_LimpiarVariablesSession()

                Call mt_CargarDatos()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Call mt_UpdatePanel("Registro")

            Call mt_FlujoTabs("Registro")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Try
            'Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwLista_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwLista.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Select Case e.CommandName
                Case "Pregunta"
                    Call mt_CargarDatosPreguntas()

                    Call mt_FlujoTabs("Pregunta")

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnSalirPregunta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalirPregunta.Click
        Try
            'Call btnListar_Click(Nothing, Nothing)

            Call mt_FlujoTabs("Listado")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnNuevoPregunta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoPregunta.Click
        Try
            Call mt_FlujoModal("RegistroPreguntas", "open")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            Me.udpScripts.Update()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_UpdatePanel(ByVal ls_panel As String)
        Try
            Select Case ls_panel
                Case "Filtros"
                    Me.udpFiltros.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpFiltrosUpdate", "udpFiltrosUpdate();", True)

                Case "Lista"
                    Me.udpLista.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaUpdate", "udpListaUpdate();", True)

                Case "Registro"
                    Me.udpRegistro.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroUpdate", "udpRegistroUpdate();", True)

                Case "ListaPreguntas"
                    Me.udpListaPreguntas.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpListaPreguntasUpdate", "udpListaPreguntasUpdate();", True)

                Case "RegistroPreguntas"
                    Me.udpRegistroPreguntas.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpRegistroPreguntasUpdate", "udpRegistroPreguntasUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoTabs(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('registro-tab');", True)

                Case "Listado"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('listado-tab');", True)

                Case "Pregunta"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "flujoTabs", "flujoTabs('pregunta-tab');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_FlujoModal(ByVal ls_modal As String, ByVal ls_accion As String)
        Try
            Select Case ls_accion
                Case "open"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModal", "openModal('" & ls_modal & "');", True)

                Case "close"
                    Me.udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModal", "closeModal('" & ls_modal & "');", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarControles(ByVal ls_tab As String)
        Try
            Select Case ls_tab
                Case "Registro"

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_LimpiarVariablesSession()
        Try

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatos()
        Try
            Dim dt As New DataTable

            If Me.grwLista.Rows.Count > 0 Then Me.grwLista.DataSource = Nothing : Me.grwLista.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("codigo_cev")
            dt.Columns.Add("descripcion_cev")
            dt.Columns.Add("tipo_cev")
            dt.Columns.Add("ciclo_cev")
            dt.Columns.Add("fecha_ini")
            dt.Columns.Add("fecha_fin")
            dt.Columns.Add("estado_cev")

            fila = dt.NewRow()
            fila("codigo_cev") = "1"
            fila("descripcion_cev") = "EVALUACIÓN DOCENTE - ESTUDIANTES 2021-I"
            fila("tipo_cev") = "EVALUACIÓN DOCENTE - ESTUDIANTES"
            fila("ciclo_cev") = "2021-I"
            fila("fecha_ini") = "01/02/2021"
            fila("fecha_fin") = "28/02/2021"
            fila("estado_cev") = "VIGENTE"

            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("codigo_cev") = "2"
            fila("descripcion_cev") = "EVALUACIÓN DOCENTE - DIRECTOR DE ESCUELA 2021-I"
            fila("tipo_cev") = "EVALUACIÓN DOCENTE - DIRECTOR DE ESCUELA"
            fila("ciclo_cev") = "2021-I"
            fila("fecha_ini") = "01/02/2021"
            fila("fecha_fin") = "28/02/2021"
            fila("estado_cev") = "VIGENTE"

            dt.Rows.Add(fila)

            Me.grwLista.DataSource = dt
            Me.grwLista.DataBind()

            Call md_Funciones.AgregarHearders(grwLista)

            Call mt_UpdatePanel("Lista")

        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarDatosPreguntas()
        Try
            Dim dt As New DataTable

            If Me.grwListaPreguntas.Rows.Count > 0 Then Me.grwListaPreguntas.DataSource = Nothing : Me.grwListaPreguntas.DataBind()

            Dim fila As Data.DataRow
            dt.Columns.Add("tipo_enunciado")
            dt.Columns.Add("numeracion")
            dt.Columns.Add("enunciado")
            dt.Columns.Add("orden")
            dt.Columns.Add("tipo_pregunta")
            dt.Columns.Add("escala")
            dt.Columns.Add("pregunta_sino")
            dt.Columns.Add("grupo")

            fila = dt.NewRow()
            fila("tipo_enunciado") = "GRUPO"
            fila("numeracion") = "I"
            fila("enunciado") = "Cumplimiento de obligaciones"
            fila("orden") = "1"
            fila("tipo_pregunta") = ""
            fila("escala") = ""
            fila("pregunta_sino") = True
            fila("grupo") = ""
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "1"
            fila("enunciado") = "Cumple con puntualidad el horario de clases (inicio, desarrollo y término)"
            fila("orden") = "2"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "I. Cumplimiento de obligaciones"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "2"
            fila("enunciado") = "Es fácil acceder al profesor para recibir de tutorías en el horario programado"
            fila("orden") = "3"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "I. Cumplimiento de obligaciones"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "3"
            fila("enunciado") = "He dado a conocer el sílabo, objetivos y otros contenidos al inicio de la asignutura"
            fila("orden") = "4"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "I. Cumplimiento de obligaciones"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "GRUPO"
            fila("numeracion") = "II"
            fila("enunciado") = "Nivel académico"
            fila("orden") = "5"
            fila("tipo_pregunta") = ""
            fila("escala") = ""
            fila("pregunta_sino") = False
            fila("grupo") = ""
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "4"
            fila("enunciado") = "Realiza seguimiento a trabajos/investigaciones que asigna en el desarrollo de la asignatura"
            fila("orden") = "6"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "II. Nivel académico"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "5"
            fila("enunciado") = "Demuestra dominio de los contenidos en el dictado de la asignatura"
            fila("orden") = "7"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "II. Nivel académico"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "6"
            fila("enunciado") = "Asocia el desarrollo de sus clases con la realidad y actualidad"
            fila("orden") = "8"
            fila("tipo_pregunta") = "CERRADA"
            fila("escala") = "EVALUACIÓN DE RESULTADOS (1 - 5)"
            fila("pregunta_sino") = False
            fila("grupo") = "II. Nivel académico"
            dt.Rows.Add(fila)

            fila = dt.NewRow()
            fila("tipo_enunciado") = "PREGUNTA"
            fila("numeracion") = "7"
            fila("enunciado") = "Escribe en el recuadro alguna observación o comentario que creas conveniente"
            fila("orden") = "9"
            fila("tipo_pregunta") = "ABIERTA"
            fila("escala") = ""
            fila("pregunta_sino") = False
            fila("grupo") = ""
            dt.Rows.Add(fila)

            Me.grwListaPreguntas.DataSource = dt
            Me.grwListaPreguntas.DataBind()

            Call md_Funciones.AgregarHearders(grwListaPreguntas)

            Call mt_UpdatePanel("ListaPreguntas")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

#End Region

End Class