Imports System.Collections.Generic

Imports ClsGlobales
Imports ClsSistemaEvaluacion
Imports ClsProcesamientoResultados
Imports ClsPublicacionResultados

Partial Class frmAccesitarios
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private odGeneral As d_VacantesEvento
    Private oeEvalAlumno As e_EvaluacionAlumno, odEvalAlumno As d_EvaluacionAlumno
    Public cod_user As Integer = 684

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                mt_CargarCicloAcademico()
                'mt_CargarCentroCosto()
                mt_CargarProgramaEstudio()
                mt_CargarModalidadIngreso()
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub cmbFiltroCicloAcademico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiltroCicloAcademico.SelectedIndexChanged
        Try
            mt_CargarComboFiltroCentroCosto()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.cmbFiltroCicloAcademico.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Semestre Académico !", MessageType.Warning) : Exit Sub
            If Me.cmbFiltroCentroCostos.SelectedValue = "-1" Then mt_ShowMessage("¡ Seleccione un Centro de Costo !", MessageType.Warning) : Exit Sub
            mt_CargarDatos()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnEnviarNoti_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oeNoti As New e_NotificaEstadoAdmision, odNoti As New d_NotificacionEstadoAdmision
        Try
            With oeNoti
                ._tipoOpe = "A" : ._codigo_apl = 32 : ._codigo_per = cod_user : ._codigo_alu = Session("adm_codigo_alu")
            End With
            Dim lo_Resultado As New Dictionary(Of String, String)
            lo_Resultado = odNoti.fc_GenerarNotificacion(oeNoti)
            If lo_Resultado.Item("rpta") = "-1" Then
                mt_ShowMessage("Ocurrio un Error en el Proceso", MessageType.Error)
            Else
                btnListar_Click(Nothing, Nothing)
                mt_ShowMessage(lo_Resultado.Item("msg"), MessageType.Success)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub

    Protected Sub grvAccesitarios_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grvAccesitarios.RowCommand
        Dim index As Integer = -1, codigo_alu As Integer = -1
        Try
            index = CInt(e.CommandArgument)
            If index >= 0 Then
                codigo_alu = Me.grvAccesitarios.DataKeys(index).Values("codigo_alu")
                Select Case e.CommandName
                    Case "Notificar"
                        Session("adm_codigo_alu") = codigo_alu
                        ClientScript.RegisterStartupScript(Me.GetType(), "Pop", "<script>openModal('notificar');</script>")
                End Select
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", ""), MessageType.Error)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Dim dvAlerta As New Literal
        Dim cssclss As String
        Select Case type
            Case MessageType.Success
                cssclss = "alert-success"
            Case MessageType.Error
                cssclss = "alert-danger"
            Case MessageType.Warning
                cssclss = "alert-warning"
            Case Else
                cssclss = "alert-info"
        End Select
        dvAlerta.Text = "<div id='alert_div' style='margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;' class='alert " + cssclss + "'>"
        dvAlerta.Text += "  <a href='#' class='close' data-dismiss='alert' aria-label='close'>&times;</a>"
        dvAlerta.Text += "  <span>" + Message + "</span>"
        dvAlerta.Text += "</div>"
        Me.divMensaje.Controls.Add(dvAlerta)
    End Sub

    Private Sub mt_CargarCicloAcademico()
        'Try
        Dim dt As Data.DataTable = fc_ListarCicloAcademico()
        mt_LlenarListas(cmbFiltroCicloAcademico, dt, "codigo_cac", "descripcion_cac", "-- SELECCIONE --")
        cmbFiltroCicloAcademico_SelectedIndexChanged(Nothing, Nothing)
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarComboFiltroCentroCosto()
        'Try
        Dim codigoCac As Integer = cmbFiltroCicloAcademico.SelectedValue
        'Dim codUsuario As Integer = Request.QueryString("id")
        Dim dt As Data.DataTable = ClsGlobales.fc_ListarCentroCostos("GEN", codigoCac, cod_user)
        mt_LlenarListas(cmbFiltroCentroCostos, dt, "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
        'udpFiltros.Update()
        'Catch ex As Exception
        '    mt_GenerarMensajeServidor("Error", -1, ex.Message)
        'End Try
    End Sub

    Private Sub mt_CargarProgramaEstudio()
        odGeneral = New d_VacantesEvento
        mt_LlenarListas(Me.cmbFiltroCarreraProfesional, odGeneral.fc_ListarCarreraProfesional, "codigo_cpf", "nombre_cpf", "-- SELECCIONE --")
    End Sub

    Private Sub mt_CargarModalidadIngreso()
        mt_LlenarListas(Me.cmbFiltroModalidadIngreso, ClsGlobales.fc_ListarModalidadIngreso(), "codigo_min", "nombre_min", "-- SELECCIONE --")
    End Sub


    'Private Sub mt_CargarCentroCosto()
    '    odGeneral = New d_VacantesEvento
    '    mt_LlenarListas(Me.cmbFiltroCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    '    'mt_LlenarListas(Me.cmbCentroCostos, odGeneral.fc_ListarCentroCostos("GEN", 0, cod_user), "codigo_cco", "descripcion_cco", "-- SELECCIONE --")
    'End Sub

    Public Sub mt_CargarDatos()
        'Dim dt As New Data.DataTable
        'With dt
        '    .Columns.Add("nro", GetType(Integer))
        '    .Columns.Add("centroCostos", GetType(String))
        '    .Columns.Add("carreraProfesional", GetType(String))
        '    .Columns.Add("modalidadIngreso", GetType(String))
        '    .Columns.Add("nroDocIdentidad", GetType(String))
        '    .Columns.Add("postulante", GetType(String))
        '    .Columns.Add("nota", GetType(Integer))
        '    .Columns.Add("notificaciones", GetType(Integer))
        '    .Columns.Add("cargoGenerado", GetType(String))

        '    .Rows.Add(1, "ADM - EXAMEN TEST DAHC 2020-II (18-JUL-20)", "ADMINISTRACIÓN", "TEST DAHC", "12345678", "DÍAZ DÍAZ DANIEL", 12, 1, "NO")
        '    .Rows.Add(2, "ADM - EXAMEN TEST DAHC 2020-II (18-JUL-20)", "ADMINISTRACIÓN", "TEST DAHC", "98765432", "PÉREZ PÉREZ PEDRO", 11, 2, "SI")
        '    .Rows.Add(3, "ADM - EXAMEN TEST DAHC 2020-II (18-JUL-20)", "CONTABILIDAD", "TEST DAHC", "65432198", "RÍOS RÍOS RODOLFO", 13, 5, "SI")
        'End With
        Dim dt As New Data.DataTable
        oeEvalAlumno = New e_EvaluacionAlumno : odEvalAlumno = New d_EvaluacionAlumno
        With oeEvalAlumno
            .tipoConsulta = "PA" : .codigoCco = Me.cmbFiltroCentroCostos.SelectedValue
        End With
        dt = odEvalAlumno.fc_Listar(oeEvalAlumno)
        Me.grvAccesitarios.DataSource = dt
        Me.grvAccesitarios.DataBind()

    End Sub

#End Region

    
End Class
