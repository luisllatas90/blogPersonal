﻿
Partial Class SisSolicitudes_EvaluacionFinal
    Inherits System.Web.UI.Page

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos, datos_motivo, datos_asunto, corresponde As New Data.DataTable
        Dim asunto, motivo As String

        datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 2, Me.TxtCodSol.Text)
        If datos.Rows.Count > 0 And TxtCodSol.Text.Length = 6 Then
            'verifica si la solicitud corresponde al evaluador
            'corresponde = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Me.TxtCodSol.Text, CInt(Request.QueryString("id")))
            'If corresponde.Rows.Count = 0 Then
            '    Page.RegisterStartupScript("No corresponde", "<script>alert('La solicitud no corresponde a ser evaluada por esta instancia')</script>")
            'End If
            Page.RegisterStartupScript("Contenido", "<script>TablaContenido.style.visibility ='visible' </script>")
            FvDatos.DataSource = datos
            FvDatos.DataBind()
            Dim Ruta As New EncriptaCodigos.clsEncripta
            Me.ImgFoto.Visible = True
            Me.FvDatos.Visible = True
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------
            Me.ImgFoto.ImageUrl = "//intranet.usat.edu.pe/imgestudiantes/" & Ruta.CodificaWeb("069" & datos.Rows(0).Item("codigoUniver_alu").ToString)
            ImgFoto.Width = 80
            ImgFoto.BorderColor = Drawing.Color.Black
            ImgFoto.BorderWidth = 1
            Select Case datos.Rows(0).Item("Estado_sol")
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
            Me.LblEstado.Font.Bold = True
            Me.LblFechaSol.Text = datos.Rows(0).Item("fecha_sol")
            Me.LblFechaReg.Text = datos.Rows(0).Item("fecharegistro_sol")
            Me.HddCodigoSol.Value = datos.Rows(0).Item("codigo_Sol").ToString
            datos.Dispose()
            'Consulta los motivos de la solicitud
            datos_motivo = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 3, Me.HddCodigoSol.Value.Trim)
            asunto = ""
            motivo = ""
            If datos_motivo.Rows.Count > 0 Then
                For i As Int16 = 0 To datos_motivo.Rows.Count - 1
                    motivo = datos_motivo.Rows(i).Item("motivo").ToString & "<br>" & motivo
                Next
            End If
            'Consulta los asuntos de la solicitud
            datos_asunto = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 4, Me.HddCodigoSol.Value.Trim)
            If datos_asunto.Rows.Count > 0 Then
                For i As Int16 = 0 To datos_asunto.Rows.Count - 1
                    asunto = datos_asunto.Rows(i).Item("asunto").ToString & "<br>" & asunto
                Next
                Me.LblResponsable.Text = datos_asunto.Rows(0).Item("responsable_sol").ToString
            End If
            Me.LblAsunto.Text = asunto
            Me.LblMotivo.Text = motivo
            Page.RegisterStartupScript("frame", "<script>frameInforme.document.location.href='ConsultarInformes.aspx?codigo_univ=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&numero_sol=" & Me.TxtCodSol.Text & "&id=" & Request.QueryString("id") & "&tas=" & CInt(datos_asunto.Rows(0).Item("codigo_tas")) & "'</script>")
            Page.RegisterStartupScript("frame2", "<script>frameHistorial.document.location.href='clsbuscaralumno.asp?codigouniver_alu=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&pagina=historial.asp'</script>")

            'Habilitar opciones a Pensiones: anular solicitud, quitar anulación, agregar asunto anulación de deuda
            If Request.QueryString("ctf") = "19" Or Request.QueryString("ctf") = "20" Or Request.QueryString("ctf") = "1" Then
                lnkAgregarAnulacion.Visible = True
                lnkAnularSolicitud.Visible = True
                lnkQuitarAnulacion.Visible = True
                txtSemestre.Visible = True
                txtSemestre.Text = ""
                Page.RegisterStartupScript("Habilitar1", "<script>tdOpciones.style.visibility='visible';</script>")
            Else
                lnkAgregarAnulacion.Visible = False
                lnkAnularSolicitud.Visible = False
                lnkQuitarAnulacion.Visible = False
                txtSemestre.Visible = False
                'Page.RegisterStartupScript("DesHabilitar1", "<script>tdOpciones.style.visibility='hidden';</script>")
            End If
        Else
            Page.RegisterStartupScript("Solicitud no encontrada", "<script>alert('La solicitud que busca no se ha registrado')</script>")
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim datos As Data.DataTable
            datos = Obj.TraerDataTable("ConsultarPersonalCentroCostos", "PE", Request.QueryString("id").ToString)
            Me.HddCodigoCco.Value = datos.Rows(0).Item("codigo_cco").ToString
            Me.LblArea.Text = datos.Rows(0).Item("DESCRIPCION_CCO").ToString
            Me.LblUsuario.Text = datos.Rows(0).Item("nombres").ToString
        End If
    End Sub

    Protected Sub lnkAgregarAnulacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAgregarAnulacion.Click
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        If Me.txtSemestre.Text.Length >= 4 Then
            objCnx.AbrirConexion()
            objCnx.Ejecutar("SOL_AgregarAsuntoSolicitudCliente", HddCodigoSol.Value, 5, txtSemestre.Text)
            objCnx.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 1, HddCodigoSol.Value, DBNull.Value, 616, 3, "", 0)
            objCnx.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 1, HddCodigoSol.Value, DBNull.Value, 1653, 0, "", 0)
            objCnx.Ejecutar("SOL_ActualizarEstadoSolicitud", HddCodigoSol.Value)
            objCnx.CerrarConexion()
            '********* Envia mail a la instancia de evaluación **********
            Dim ObjMail As New ClsEnviaMail
            ObjMail.ConsultarEnvioMail(HddCodigoSol.Value)
            ClientScript.RegisterStartupScript(Me.GetType, "Exito", "alert('Se agregó correctamente anulación de deuda, dar clic en buscar para actualizar la página');", True)
        End If
    End Sub

    Protected Sub lnkAnularSolicitud_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAnularSolicitud.Click
        AnularSolicitud("A")
        ClientScript.RegisterStartupScript(Me.GetType, "anulado", "alert('Se anuló la solicitud correctamente');", True)
    End Sub

    Sub AnularSolicitud(ByVal estado As String)
        Dim objCnx As New ClsConectarDatos
        objCnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        objCnx.AbrirConexion()
        objCnx.Ejecutar("SOL_AnularSolicitudCliente", HddCodigoSol.Value, estado)
        If estado = "P" Then
            objCnx.Ejecutar("SOL_ActualizarEstadoSolicitud", HddCodigoSol.Value)
        End If
        objCnx.CerrarConexion()
    End Sub

    Protected Sub lnkQuitarAnulacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkQuitarAnulacion.Click
        AnularSolicitud("P")
        ClientScript.RegisterStartupScript(Me.GetType, "restaurado", "alert('Se restauró la solicitud anulada');", True)
    End Sub

    Protected Sub txtSemestre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSemestre.TextChanged

    End Sub
End Class
