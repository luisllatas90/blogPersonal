﻿
Partial Class SisSolicitudes_Evaluacion
    Inherits System.Web.UI.Page

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos, datos_motivo, datos_asunto, datos_Aux, corresponde As New Data.DataTable
        Dim asunto, motivo As String
        Dim DirectorEscuela, Dat_Director As Data.DataTable
        datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 2, Me.TxtCodSol.Text.Trim)
        'Response.Write(datos.Rows.Count)
        If datos.Rows.Count > 0 Then
            'verifica si la solicitud corresponde al evaluador
            'corresponde = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.TxtCodSol.Text, Me.HddCodigoCco.Value)
            'If corresponde.Rows.Count = 0 Then
            '    Page.RegisterStartupScript("No corresponde", "<script>alert('La solicitud no corresponde a ser evaluada por esta instancia')</script>")
            'End If
            Page.RegisterStartupScript("Contenido", "<script>TablaContenido.style.visibility ='visible' </script>")
            FvDatos.DataSource = datos
            FvDatos.DataBind()
            Session("codigo_alu") = datos.Rows(0).Item("codigo_Alu")
            Dim Ruta As New EncriptaCodigos.clsEncripta
            Me.ImgFoto.Visible = True
            Me.FvDatos.Visible = True
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------

            Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & datos.Rows(0).Item("codigoUniver_alu").ToString)
            Session("foto") = Me.ImgFoto.ImageUrl
            ImgFoto.Width = 80
            ImgFoto.BorderColor = Drawing.Color.Black
            ImgFoto.BorderWidth = 1

            IndicarEstadoSolicitud(datos.Rows(0).Item("Estado_sol"))

            Me.LblEstado.Font.Bold = True
            Me.LblFechaSol.Text = datos.Rows(0).Item("fecha_sol")
            Me.LblFechaReg.Text = datos.Rows(0).Item("fecharegistro_sol")
            Me.HddCodigoSol.Value = datos.Rows(0).Item("codigo_Sol").ToString
            Me.LblObservaciones.Text = datos.Rows(0).Item("observaciones_Sol").ToString
            datos.Dispose()

            'Consulta los motivos de la solicitud
            datos_motivo = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 3, Me.HddCodigoSol.Value.Trim)
            asunto = "» "
            motivo = "» "
            If datos_motivo.Rows.Count > 0 Then
                For i As Int16 = 0 To datos_motivo.Rows.Count - 1
                    motivo = motivo & datos_motivo.Rows(i).Item("motivo").ToString & "<br> » "
                Next
            End If

            'Consulta los asuntos de la solicitud
            datos_asunto = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 4, Me.HddCodigoSol.Value.Trim)
            If datos_asunto.Rows.Count > 0 Then
                For i As Int16 = 0 To datos_asunto.Rows.Count - 1
                    asunto = asunto & datos_asunto.Rows(i).Item("asunto").ToString & "<br> » "
                Next
                Me.LblResponsable.Text = datos_asunto.Rows(0).Item("responsable_sol").ToString
            End If

            Me.LblAsunto.Text = Left(asunto, asunto.Length - 2)
            Me.LblMotivo.Text = Left(motivo, motivo.Length - 2)
            Me.LblMensaje.Text = ""
            Page.RegisterStartupScript("frame", "<script>frameInforme.document.location.href='informes.aspx?codigo_univ=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&numero_sol=" & Me.TxtCodSol.Text & "&id=" & Request.QueryString("id") & "&tas=" & CInt(datos_asunto.Rows(0).Item("codigo_tas")) & "'</script>")
            Page.RegisterStartupScript("frame2", "<script>frameHistorial.document.location.href='clsbuscaralumno.asp?codigouniver_alu=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&pagina=historial.asp'</script>")
            hddCodigo_tas.Value = CInt(datos_asunto.Rows(0).Item("codigo_tas"))
            If (CInt(datos_asunto.Rows(0).Item("codigo_tas")) < 3 Or CInt(datos_asunto.Rows(0).Item("codigo_tas")) = 15) Then
                Me.lblEtiquetaFechaHasta.Visible = True
                Me.lblEtiquetaFormatFecha.Visible = True
                Me.txtFechaUltimaAsistencia.Visible = True
                Me.RequiredFieldValidator2.Enabled = True
            Else
                Me.lblEtiquetaFechaHasta.Visible = False
                Me.lblEtiquetaFormatFecha.Visible = False
                Me.txtFechaUltimaAsistencia.Visible = False
                Me.RequiredFieldValidator2.Enabled = False
            End If


            'Agregado Yperez 07/01/2013 para asuntos de solicitud con anulación de deuda
            If datos_asunto.rows.count = 2 Then
                If (CInt(datos_asunto.Rows(1).Item("codigo_tas")) < 3 Or CInt(datos_asunto.Rows(1).Item("codigo_tas")) = 15) Then
                    Me.lblEtiquetaFechaHasta.Visible = True
                    Me.lblEtiquetaFormatFecha.Visible = True
                    Me.txtFechaUltimaAsistencia.Visible = True
                    Me.RequiredFieldValidator2.Enabled = True
                End If
            End If


            If hddCodigo_tas.Value = 17 Then
                ClsFunciones.LlenarListas(CblSeleccionar, Obj.TraerDataTable("ConsultarPlanEstudio", "SO", Session("codigo_alu"), "NC"), "codigo_cur", "Descripcion")
                Me.HddTotalSel.Value = CblSeleccionar.Items.Count
                Me.pnlCursos.Visible = True
            Else
                Me.HddTotalSel.Value = 0
                Me.pnlCursos.Visible = False
            End If

            'Verifica si quien registra es el director de escuela y asigna el centro de costo para saber si tiene un asistente para evaluar
            HddEsDirector.Value = 0
            DirectorEscuela = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.HddCodigoSol.Value, CInt(Request.QueryString("id")))
            If DirectorEscuela.Rows.Count > 0 Then
                HddEsDirector.Value = 1
                Me.HddCodigoCco.Value = DirectorEscuela.Rows(0).Item("codigo_cco").ToString
            Else
                Dat_Director = Obj.TraerDataTable("SOL_VerificaDirectorEscuela", CInt(Request.QueryString("id")))
                If Dat_Director.Rows.Count > 0 Then
                    Me.HddCodigoCco.Value = Dat_Director.Rows(0).Item("codigo_cco").ToString
                    DirectorEscuela = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.HddCodigoSol.Value, CInt(Request.QueryString("id")))
                    If DirectorEscuela.Rows.Count > 0 Then
                        HddEsDirector.Value = 1
                    End If
                End If
            End If

            If LblEstado.Text <> "Anulada" Then
                If HddEsDirector.Value = 1 Then
                    If DirectorEscuela.Rows(0).Item("codigo_res") = 0 Then
                        Me.CmdGuardar.Enabled = True
                    Else
                        Me.CmdGuardar.Enabled = False
                    End If
                Else
                    datos_Aux = Obj.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 2, CInt(Request.QueryString("id")), HddCodigoSol.Value)
                    If datos_Aux.Rows.Count > 0 Then
                        HddCodigoCco.Value = datos_Aux.Rows(0).Item("codigo_cco").ToString
                        If CInt(datos_Aux.Rows(0).Item("codigo_res")) > 0 Then
                            CmdGuardar.Enabled = False
                        Else
                            CmdGuardar.Enabled = True
                        End If
                    Else
                        CmdGuardar.Enabled = False
                    End If
                End If
            Else
                CmdGuardar.Enabled = False
            End If


        Else
            Page.RegisterStartupScript("Solicitud no encontrada", "<script>alert('La solicitud que busca no se ha registrado')</script>")
        End If

    End Sub

    Private Sub IndicarEstadoSolicitud(ByVal estado_sol As String)
        Select Case estado_sol
            Case "P"
                Me.LblEstado.Text = "Pendiente"
                Me.LblEstado.ForeColor = Drawing.Color.Red
                Me.LblEstado.Font.Strikeout = False
            Case "T"
                Me.LblEstado.Text = "Finalizada"
                Me.LblEstado.ForeColor = Drawing.Color.Green
                Me.LblEstado.Font.Strikeout = False
            Case "A"
                Me.LblEstado.Text = "Anulada"
                Me.LblEstado.ForeColor = Drawing.Color.Red
                Me.LblEstado.Font.Strikeout = True
        End Select
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.LblFecha.Text = Date.Now.ToShortDateString
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim datos As Data.DataTable

            datos = Obj.TraerDataTable("ConsultarPersonalCentroCostos", "PE", Request.QueryString("id").ToString)
            Me.LblArea.Text = datos.Rows(0).Item("DESCRIPCION_CCO").ToString
            Me.LblUsuario.Text = datos.Rows(0).Item("nombres").ToString
            'Llena combo para respuesta : Aprobado, Desaprobado
            ClsFunciones.LlenarListas(Me.DdlHaResuelto, Obj.TraerDataTable("SOL_ConsultarResultadoSolicitud", "1", 1), "codigo_res", "descripcion_res", "--Seleccione resultado--")
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim observacion As String
        Try
            If hddCodigo_tas.Value < 3 Or hddCodigo_tas.Value = 15 Then
                observacion = Me.TxtObservaciones.Text & " | Fecha última asistencia: " & Me.txtFechaUltimaAsistencia.Text
            ElseIf hddCodigo_tas.Value = 17 Then
                observacion = Me.TxtObservaciones.Text & ". Cursos aprobados:"
                For i As Int16 = 0 To Me.CblSeleccionar.Items.Count - 1
                    If Me.CblSeleccionar.Items(i).Selected = True Then
                        observacion = observacion & " * " & Me.CblSeleccionar.Items(i).Text
                    End If
                Next
            Else
                observacion = Me.TxtObservaciones.Text
            End If

            Obj.IniciarTransaccion()
            Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 2, Me.HddCodigoSol.Value, Me.DdlHaResuelto.SelectedValue, Me.HddCodigoCco.Value, 0, observacion, CInt(Request.QueryString("id").ToString))
            Obj.TerminarTransaccion()

            'Obj.Ejecutar("SOL_ActualizarEstadoSolicitud", Me.HddCodigoSol.Value)
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = "Se insertaron los datos correctamente"

            'Page.RegisterStartupScript("Guardar", "<script>alert('Se insertaron los datos correctamente')</script>")
            Me.TxtObservaciones.Text = ""
            Me.txtFechaUltimaAsistencia.Text = ""
            Dim ObjMail As New ClsEnviaMail
            Me.LblMensaje.Text = Me.LblMensaje.Text
            ObjMail.ConsultarEnvioMail(Me.HddCodigoSol.Value, Session("foto"))
        Catch ex As Exception
            Obj.AbortarTransaccion()
            Me.LblMensaje.Text = ex.Message
            'Me.LblMensaje.Text = "Ocurrió un error al procesar los datos"
        End Try
    End Sub

End Class
