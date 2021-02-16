Imports System.Collections.Generic

Partial Class GestionEgresado_GradosYTitulos_frmGenerarUrlTesis
    Inherits System.Web.UI.Page
#Region "Variables"
    Dim codigo_tfu As Integer
    Dim tipoestudio As Integer
    Dim codigo_usu As Integer

    Public Enum MessageType
        success
        [error]
        info
        warning
    End Enum

#End Region

#Region "Eventos"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If
        codigo_tfu = Request.QueryString("ctf")
        'tipoestudio = Request.QueryString("mod")
        tipoestudio = "2"
        codigo_usu = Request.QueryString("id")

        If IsPostBack = False Then
            Call mt_CargarComboEstado()
            Call mt_ListarTramitesTitulosUrl("TTI", codigo_tfu, Me.ddlEstado.SelectedValue)
        End If
    End Sub

    Protected Sub gvListaTramTitulosUrl_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvListaTramTitulosUrl.RowCommand
        Dim codigo_tes As Integer
        Dim descripcion_tes As String
        Dim urlTesis As String
        Dim codigo_dta As String
        Dim urlVerifica As String
        Dim estadoAprobacion As String
        'Dim codigo_tfu_finaliza As String
        Try
            Dim index As Integer = 0 : index = CInt(e.CommandArgument)
            codigo_tes = Me.gvListaTramTitulosUrl.DataKeys(index).Values("codigo_tes")
            descripcion_tes = Me.gvListaTramTitulosUrl.DataKeys(index).Values("titulo_tes")
            urlTesis = Me.gvListaTramTitulosUrl.DataKeys(index).Values("url_Tes")
            codigo_dta = Me.gvListaTramTitulosUrl.DataKeys(index).Values("codigo_dta")
            urlVerifica = Me.gvListaTramTitulosUrl.DataKeys(index).Values("url_Tes")
            estadoAprobacion = Me.gvListaTramTitulosUrl.DataKeys(index).Values("estadoAprobacion")
            'codigo_tfu_finaliza = Me.gvListaTramTitulosUrl.DataKeys(index).Values("codigo_tfu")
            Select Case e.CommandName
                Case "editUrl"
                    Call mt_LimpiaControles()
                    Me.txtCodigoTesis.Text = codigo_tes
                    Me.txtDescTesis.Text = descripcion_tes
                    Me.txtUrl.Text = urlTesis
                    Me.txtCodigoDta.Text = codigo_dta
                    Me.txtUrlTesis.Text = urlVerifica
                    Me.txtCodigoTfuFinal.Text = codigo_tfu
                    Me.txtEstadoAprobacion.Text = estadoAprobacion

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "openModalUrl", "openModalUrl();", True)
                    Me.udpModalUrl.Update()
            End Select
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub

    Protected Sub lbGuardarURL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbGuardarURL.Click
        Try
            Dim dt As New Data.DataTable
            Dim me_tesis As New e_Tesis
            Dim md_Tesis As New d_Tesis

            If mt_validaFormulario() Then
               
                Dim codigo_dta As String
                Dim urlTesis As String
                Dim actualizaEtapa As String
                Dim codigo_tfu_final As String


                With me_tesis
                    .codigo_Tes = Me.txtCodigoTesis.Text
                    .Titulo_Tes = Me.txtDescTesis.Text
                    .url_Tes = Me.txtUrl.Text
                End With

                dt = md_Tesis.ActualizarUrlTesis(me_tesis)

                If dt.Rows.Count > 0 Then

                    If dt.Rows(0).Item("Respuesta") = "1" Then

                        codigo_dta = Me.txtCodigoDta.Text
                        urlTesis = Me.txtUrlTesis.Text
                        '-- If urlTesis = "" Then '------- si es por primera ves que se va actualizar la url.... finaliza etapa y envia correo
                        If Me.txtEstadoAprobacion.Text = "P" Then
                            'Call mt_ShowMessage("entre", MessageType.success)
                            '-------------Actualizar la etapa del trámite------------------------------------------------------------------------------------------------
                            codigo_tfu_final = Me.txtCodigoTfuFinal.Text
                            actualizaEtapa = mt_EnviaActualizarEtapaTramite(codigo_dta, codigo_tfu)
                            If actualizaEtapa <> "" Then
                                Call mt_ShowMessage(actualizaEtapa, MessageType.error)
                                Exit Sub
                            End If

                            '--------------------------------------------------------------------------------------------------------------------------------------------
                            '----------------- Envia correo--------------------------------------------------------------------------------------------------------------
                            'traigo los datos para el correo


                            Dim dtEmail As New Data.DataTable

                            With me_tesis
                                .operacion = "E"
                                .codigo_Tes = Me.txtCodigoTesis.Text
                            End With
                            dtEmail = md_Tesis.ConsultarTesis(me_tesis)

                            If dtEmail.Rows.Count > 0 Then
                                Dim emailAlumno As String
                                If ConfigurationManager.AppSettings("CorreoUsatActivo") = "1" Then
                                    emailAlumno = dtEmail.Rows(0).Item("eMail_Alu")
                                Else
                                    emailAlumno = "olluen@usat.edu.pe"
                                End If
                                Dim codigoEnvio As Integer
                                codigoEnvio = ClsComunicacionInstitucional.ObtenerCodigoEnvio(codigo_usu, codigo_tfu, 47)
                                ClsComunicacionInstitucional.EnviarNotificacionEmail(codigoEnvio, "TITU", "TFF1", "1", codigo_usu, "codigo_alu", dtEmail.Rows(0).Item("codigo_Alu"), "47", emailAlumno, "", "", "", _
                                                                     dtEmail.Rows(0).Item("Responsable"), dtEmail.Rows(0).Item("Titulo_Tes"), dtEmail.Rows(0).Item("url_Tes"))

                            End If

                            '-----------------------------------------------------------------------------------------------------------------------------------------------

                            Call mt_ShowMessage("Se actualizó URL, Se actualizó etapa del trámite y se envió notificación", MessageType.success)

                        Else
                            Call mt_ShowMessage("URL Actualizada", MessageType.success)
                        End If
                        Call mt_LimpiaControles()
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalUrl", "closeModalUrl();", True)
                        Call mt_ListarTramitesTitulosUrl("TTI", codigo_tfu, Me.ddlEstado.SelectedValue)
                        Me.udpListadoConf.Update()
                    Else
                        Call mt_ShowMessage("No se actualizó la URL", MessageType.error)
                        Exit Sub
                    End If
                End If

            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)

        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Call mt_ListarTramitesTitulosUrl("TTI", codigo_tfu, Me.ddlEstado.SelectedValue)
    End Sub

    Protected Sub lbCerrarMod_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCerrarMod.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "closeModalUrl", "closeModalUrl();", True)
        Call mt_ListarTramitesTitulosUrl("TTI", codigo_tfu, Me.ddlEstado.SelectedValue)
        Me.udpListadoConf.Update()
    End Sub

#End Region

#Region "Métodos y funciones"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "showMessage", "showMessage('" & Message & "','" & type.ToString & "');", True)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try
    End Sub
    Private Sub mt_CargarComboEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("PENDIENTES", "P"))
        Me.ddlEstado.Items.Add(New ListItem("APROBADOS", "F"))
        'Me.ddlEstado.Items.Add(New ListItem("PENDIENTES Y APROBADOS", "N"))
    End Sub
    Private Sub mt_ListarTramitesTitulosUrl(ByVal operacion As String, ByVal codigo_tfu As Integer, ByVal estado_dft As String)

        Dim md_TramiteAlumno As New d_TramiteAlumno
        Dim me_TramiteAlumno As New e_TramiteAlumno
        Dim dt As New Data.DataTable
        With me_TramiteAlumno
            .operacion = "TTI"
            .codigo_tfu = codigo_tfu
            .estado_dft = Me.ddlEstado.SelectedValue
            If Me.ddlEstado.SelectedValue = "P" Then
                .estadoAprobacion = ""
            ElseIf Me.ddlEstado.SelectedValue = "F" Then
                .estadoAprobacion = "A"
            End If

        End With
        Try
            dt = md_TramiteAlumno.ListarTramitesTitulosUrl(me_TramiteAlumno)

            If dt.Rows.Count > 0 Then
                Me.gvListaTramTitulosUrl.DataSource = dt
                Me.gvListaTramTitulosUrl.DataBind()
            Else
                Call mt_ShowMessage("No se encontraron trámites aprobados", MessageType.warning)
                Call mt_LimpiaControles()
            End If
            Me.udpListadoConf.Update()
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.error)
        End Try

    End Sub
    Private Function mt_validaFormulario() As Boolean
        If Me.txtCodigoTesis.Text = "" Then
            Call mt_ShowMessage("Tesis no ubicada", MessageType.warning)
            Return False
        End If
        If Me.txtDescTesis.Text = "" Then
            Call mt_ShowMessage("Ingrese el título de la tesis", MessageType.warning)
            Return False
        End If
        If Me.txtUrl.Text = "" Then
            Call mt_ShowMessage("Ingrese URL de la tesis", MessageType.warning)
            Return False
        End If
        If Me.hfValidaUrl.Value = "false" Then
            Call mt_ShowMessage("Ingrese una URL válida", MessageType.warning)
            Return False
        End If

        Return True
    End Function
    Private Function mt_EnviaActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal codigo_tfu As Integer) As String
        Dim mensaje As String = ""
        If codigo_dta <> "0" Then
            Dim dtTramite As New Data.DataTable
            dtTramite = mt_ActualizarEtapaTramite(codigo_dta, "1", "A", codigo_tfu)
            If dtTramite.Rows.Count > 0 Then ' SI HAY RESPUESTA DEL CAMBIO DE ETAPA
                If dtTramite.Rows(0).Item("revision") = True Then ' SI SE HIZO LA REVISIÓN                   
                    mensaje = ""
                End If
            Else
                mensaje = "Se actualizó url, pero no se pudo realizar la actualización de la etapa del trámite"
                Return mensaje
                Exit Function
            End If
        Else
            mensaje = "Se actualizó la url pero el código del trámite es invalido"
            Return mensaje
            Exit Function
        End If
        Return mensaje
    End Function
    Private Function mt_ActualizarEtapaTramite(ByVal codigo_dta As Integer, ByVal tipooperacion As String, ByVal estadoaprobacion As String, ByVal codigo_tfu As Integer) As Data.DataTable
        Try
            Dim cmp As New clsComponenteTramiteVirtualCVE
            Dim objcmp As New List(Of Dictionary(Of String, Object))()
            cmp._codigo_dta = codigo_dta
            'cmp.tipoOperacion = "1"
            cmp.tipoOperacion = tipooperacion
            cmp._codigo_per = Session("id_per")
            cmp._codigo_tfu = codigo_tfu ' tipo funcion director de escuela
            If estadoaprobacion = "A" Then
                cmp._estadoAprobacion = "A" ' DA CONFORMIDAD OSEA APRUEBA
                cmp._observacionEvaluacion = "Tu tesis ya cuenta con un URL y se encuentra registrada en el Repositorio USAT"
            Else
                cmp._estadoAprobacion = "R"
                cmp._observacionEvaluacion = "Rechazar componente"
            End If
            objcmp = cmp.mt_EvaluarTramite()
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")
            For Each fila As Dictionary(Of String, Object) In objcmp
                dt.Rows.Add(fila.Item("evaluacion"), fila.Item("registos evaluados").ToString, fila.Item("email"))
            Next
            Return dt
        Catch ex As Exception
            Dim dt As New Data.DataTable
            dt.Columns.Add("revision")
            dt.Columns.Add("registros")
            dt.Columns.Add("email")

            dt.Rows.Add(False, "", False)

            Return dt
        End Try
    End Function
    Private Sub mt_LimpiaControles()
        Me.gvListaTramTitulosUrl.DataSource = Nothing
        Me.gvListaTramTitulosUrl.DataBind()
        Me.txtCodigoDta.Text = String.Empty
        Me.txtCodigoTesis.Text = String.Empty
        Me.txtUrlTesis.Text = String.Empty
        Me.txtDescTesis.Text = String.Empty
        Me.txtUrl.Text = String.Empty
        Me.txtCodigoTfuFinal.Text = String.Empty
        Me.hfValidaUrl.Value = String.Empty
        Me.txtEstadoAprobacion.Text = String.Empty
    End Sub

#End Region





    
    
  
End Class
