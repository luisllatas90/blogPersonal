Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Collections.Generic

Partial Class aulavirtual_frmIngresarAulaVirtual
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"
    'ENTIDADES
    Dim me_AsistenciaDocente As e_AsistenciaDocente

    'DATOS
    Dim md_AsistenciaDocente As New d_AsistenciaDocente
    Dim md_Funciones As New d_Funciones

    'VARIABLES
    Dim cod_user As Integer = 0        
    Dim mensaje_observacion As String = String.Empty

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

            If IsPostBack = False Then
                Call mt_MensajePassword()
                Call mt_CargarHorario()
                Call mt_CargarHistorial()
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub grwHorario_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grwHorario.RowCommand
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)

            Dim le_AsistenciaDocente As New e_AsistenciaDocente

            With le_AsistenciaDocente
                .codigo_hdo = CInt(Me.grwHorario.DataKeys(index).Values("codigo_hdo").ToString)
                .codigo_cup = CInt(Me.grwHorario.DataKeys(index).Values("codigo_cup").ToString)                
                .codigo_lho = CInt(Me.grwHorario.DataKeys(index).Values("codigo_lho").ToString)
                .codigo_per = CInt(Me.grwHorario.DataKeys(index).Values("codigo_per").ToString)                
                .descripcionHorario_ado = Me.grwHorario.DataKeys(index).Values("descripcion_horario").ToString                
            End With

            Select Case e.CommandName
                Case "Iniciar"
                    If Not mt_RegistrarInicioFinClase(le_AsistenciaDocente, "I") Then Exit Sub

                    Call mt_CargarHorario()
                    Call mt_CargarHistorial()

                Case "Finalizar"
                    If Not mt_RegistrarInicioFinClase(le_AsistenciaDocente, "F") Then Exit Sub

                    Call mt_CargarHorario()
                    Call mt_CargarHistorial()

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnGestionarCursos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGestionarCursos.Click
        Try
            If fu_ValidarGestionarCursos() Then
                Call mt_AccesoMoodle("no", "N")
            Else
                Call mt_AccesoMoodle("no", "S")
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub btnCapacitacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCapacitacion.Click
        Try
            Call mt_AccesoMoodle("si", "N")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub grwHorario_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwHorario.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim tipo As String = e.Row.DataItem("tipo").ToString

                Dim btnIniciar As LinkButton
                btnIniciar = e.Row.Cells(2).FindControl("btnIniciar")

                Dim btnIniciarInactivo As LinkButton
                btnIniciarInactivo = e.Row.Cells(2).FindControl("btnIniciarInactivo")

                Dim btnFinalizar As LinkButton
                btnFinalizar = e.Row.Cells(2).FindControl("btnFinalizar")

                Dim btnFinalizarInactivo As LinkButton
                btnFinalizarInactivo = e.Row.Cells(2).FindControl("btnFinalizarInactivo")

                If tipo = "I" Then
                    btnIniciarInactivo.Style.Add("display", "none")
                    btnFinalizar.Style.Add("display", "none")
                ElseIf tipo = "F" Then
                    btnIniciar.Style.Add("display", "none")
                    btnFinalizarInactivo.Style.Add("display", "none")
                End If
            End If
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
                Case "Horario"
                    Me.udpHorario.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpHorarioUpdate", "udpHorarioUpdate();", True)

                Case "Historial"
                    Me.udpHistorial.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "udpHistorialUpdate", "udpHistorialUpdate();", True)

            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarHorario()
        Try
            Dim dt As New DataTable : me_AsistenciaDocente = New e_AsistenciaDocente

            If Me.grwHorario.Rows.Count > 0 Then Me.grwHorario.DataSource = Nothing : Me.grwHorario.DataBind()

            With me_AsistenciaDocente
                .operacion = "HOR"
                .codigo_per = cod_user
            End With

            dt = md_AsistenciaDocente.ListarAsistenciaDocente(me_AsistenciaDocente)

            Me.grwHorario.DataSource = dt
            Me.grwHorario.DataBind()

            Call md_Funciones.AgregarHearders(grwHorario)

            Call mt_UpdatePanel("Horario")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Sub mt_CargarHistorial()
        Try
            Dim dt As New DataTable : me_AsistenciaDocente = New e_AsistenciaDocente

            If Me.grwHistorial.Rows.Count > 0 Then Me.grwHistorial.DataSource = Nothing : Me.grwHistorial.DataBind()

            With me_AsistenciaDocente
                .operacion = "CIC"
                .codigo_per = cod_user
            End With

            dt = md_AsistenciaDocente.ListarAsistenciaDocente(me_AsistenciaDocente)

            Me.grwHistorial.DataSource = dt
            Me.grwHistorial.DataBind()

            Call md_Funciones.AgregarHearders(grwHistorial)

            Call mt_UpdatePanel("Historial")
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Private Function mt_RegistrarInicioFinClase(ByVal le_AsistenciaDocente As e_AsistenciaDocente, ByVal tipo As String) As Boolean
        Try
            If Not fu_ValidarRegistrarInicioFinClase(le_AsistenciaDocente, tipo) Then Return False

            me_AsistenciaDocente = md_AsistenciaDocente.GetAsistenciaDocente(0)

            With me_AsistenciaDocente
                .operacion = "I"
                .cod_user = cod_user
                .codigo_hdo = le_AsistenciaDocente.codigo_hdo
                .codigo_cup = le_AsistenciaDocente.codigo_cup                
                .codigo_lho = le_AsistenciaDocente.codigo_lho
                .codigo_per = le_AsistenciaDocente.codigo_per                
                .descripcionHorario_ado = le_AsistenciaDocente.descripcionHorario_ado                
                .tipo = tipo
                .observacion = IIf(String.IsNullOrEmpty(mensaje_observacion), "N", "S")
            End With

            md_AsistenciaDocente.RegistrarAsistenciaDocente(me_AsistenciaDocente)

            If Not String.IsNullOrEmpty(mensaje_observacion) Then
                Call mt_ShowMessage(mensaje_observacion, MessageType.warning)
            Else
                If tipo = "I" Then
                    Call mt_ShowMessage("¡El inicio de la clase se registró exitosamente!", MessageType.success)
                ElseIf tipo = "F" Then
                    Call mt_ShowMessage("¡El fin de la clase se registró exitosamente!", MessageType.success)
                End If
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarRegistrarInicioFinClase(ByVal le_AsistenciaDocente As e_AsistenciaDocente, ByVal tipo As String) As Boolean
        Try
            If String.IsNullOrEmpty(le_AsistenciaDocente.codigo_per) OrElse CInt(le_AsistenciaDocente.codigo_per) = 0 Then mt_ShowMessage("No es posible realizar la acción.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(le_AsistenciaDocente.codigo_hdo) OrElse CInt(le_AsistenciaDocente.codigo_hdo) = 0 Then mt_ShowMessage("No es posible realizar la acción.", MessageType.warning) : Return False
            If String.IsNullOrEmpty(le_AsistenciaDocente.codigo_cup) OrElse CInt(le_AsistenciaDocente.codigo_cup) = 0 Then mt_ShowMessage("No es posible realizar la acción.", MessageType.warning) : Return False            
            If String.IsNullOrEmpty(le_AsistenciaDocente.codigo_lho) OrElse CInt(le_AsistenciaDocente.codigo_lho) = 0 Then mt_ShowMessage("No es posible realizar la acción.", MessageType.warning) : Return False

            Dim dt As New DataTable : me_AsistenciaDocente = New e_AsistenciaDocente
            mensaje_observacion = String.Empty

            With me_AsistenciaDocente
                .operacion = "VAL"
                .codigo_per = le_AsistenciaDocente.codigo_per
                .codigo_hdo = le_AsistenciaDocente.codigo_hdo
                .codigo_cup = le_AsistenciaDocente.codigo_cup                                
                .codigo_lho = le_AsistenciaDocente.codigo_lho
                .tipo = tipo
            End With

            dt = md_AsistenciaDocente.ListarAsistenciaDocente(me_AsistenciaDocente)


            If dt.Rows.Count = 0 Then
                If tipo = "I" Then
                    mt_ShowMessage("Estimado docente, aún no se encuentra dentro del tiempo establecido para registrar su asistencia de ingreso a la clase.", MessageType.warning) : Return False
                ElseIf tipo = "F" Then
                    mt_ShowMessage("Estimado docente, se encuentra fuera del tiempo establecido para registrar su asistencia de salida a la clase.", MessageType.warning) : Return False
                End If
            Else
                If Not String.IsNullOrEmpty(dt.Rows(0).Item("mensaje").ToString) Then
                    If dt.Rows(0).Item("mensaje").ToString.Contains("iniciada") OrElse _
                        dt.Rows(0).Item("mensaje").ToString.Contains("finalizada") Then
                        mt_ShowMessage(dt.Rows(0).Item("mensaje").ToString, MessageType.warning) : Return False
                    Else
                        mensaje_observacion = dt.Rows(0).Item("mensaje").ToString : Return True
                    End If
                End If
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_AccesoMoodle(ByVal curso As String, ByVal advertencia As String) As Boolean
        Try
            Dim dt As New DataTable : Dim le_AsistenciaDocente As New e_AsistenciaDocente

            With le_AsistenciaDocente
                .operacion = "PE"
                .codigo_per = cod_user
            End With

            dt = md_AsistenciaDocente.ConsultarCodigoAccesoMoodle(le_AsistenciaDocente)


            If dt.Rows.Count > 0 Then
                Dim codigo As String = dt.Rows(0).Item("codigo_pso").ToString
                Dim clave As String = dt.Rows(0).Item("ClaveInterna_Pso").ToString

                udpScripts.Update()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "loginMoodle", "loginMoodle('','','','','','','" & codigo & "','" & clave & "','" & curso & "', '" & advertencia & "');", True)
            Else
                mt_ShowMessage("Acceso Denegado.", MessageType.warning) : Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function mt_MensajePassword() As Boolean
        Try
            Dim dt As New DataTable : Dim le_AsistenciaDocente As New e_AsistenciaDocente

            With le_AsistenciaDocente
                .operacion = "8"
                .codigo_per = cod_user
            End With

            dt = md_AsistenciaDocente.ConsultarAplicacionUsuario(le_AsistenciaDocente)

            If dt.Rows.Count > 0 Then
                Dim dias As String = dt.Rows(0).Item("dias").ToString()

                lblMensajePassword.Text = "Te quedan " & dias & " días para cambiar tu contraseña."

                If CInt(dias) < 15 Then
                    udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mensajePassword", "mensajePassword('danger');", True)
                ElseIf CInt(dias) >= 15 AndAlso CInt(dias) <= 90 Then
                    udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mensajePassword", "mensajePassword('info');", True)
                Else
                    udpScripts.Update()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "mensajePassword", "mensajePassword('success');", True)
                End If
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

    Private Function fu_ValidarGestionarCursos() As Boolean
        Try
            Dim dt As New DataTable : me_AsistenciaDocente = New e_AsistenciaDocente

            With me_AsistenciaDocente
                .operacion = "HOR"
                .codigo_per = cod_user
            End With

            dt = md_AsistenciaDocente.ListarAsistenciaDocente(me_AsistenciaDocente)

            If dt.Rows.Count > 0 Then
                For Each fila As DataRow In dt.Rows
                    If fila("tipo").ToString.Trim.Equals("I") Then                        
                        Call mt_ShowMessage("Estimado docente, se le hace recordar que deberá registrar su asistencia para el ingreso y salida a clases.", MessageType.warning)
                        Call mt_CargarHorario()
                        Return False
                    End If
                Next
            End If

            Return True
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Function

#End Region

End Class
