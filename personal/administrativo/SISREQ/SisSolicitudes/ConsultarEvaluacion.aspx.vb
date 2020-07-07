﻿
Partial Class SisSolicitudes_ConsultarEvaluacion
    Inherits System.Web.UI.Page

    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdBuscar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim datos, corresponde As New Data.DataTable
        Dim asunto, motivo As String

        datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 2, Me.TxtCodSol.Text)
        If datos.Rows.Count > 0 Then
            'verifica si la solicitud corresponde al evaluador
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

            IndicarEstadoSolicitud(datos.Rows(0).Item("Estado_sol"))

            Me.LblEstado.Font.Bold = True
            Me.LblFechaSol.Text = datos.Rows(0).Item("fecha_sol")
            Me.LblFechaReg.Text = datos.Rows(0).Item("fecharegistro_sol")
            Me.HddCodigoSol.Value = datos.Rows(0).Item("codigo_Sol").ToString
            Me.LblObservaciones.Text = datos.Rows(0).Item("observaciones_Sol").ToString

            Page.RegisterStartupScript("frame", "<script>frameInforme.document.location.href='ConsultarInformes.aspx?codigo_univ=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&numero_sol=" & Me.TxtCodSol.Text & "&id=" & Request.QueryString("id") & "'</script>")
            Page.RegisterStartupScript("frame2", "<script>frameHistorial.document.location.href='clsbuscaralumno.asp?codigouniver_alu=" & datos.Rows(0).Item("codigoUniver_alu").ToString & "&pagina=historial.asp'</script>")
            'Consulta los motivos de la solicitud

            datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 3, Me.HddCodigoSol.Value.Trim)
            asunto = "» "
            motivo = "» "
            For i As Int16 = 0 To datos.Rows.Count - 1
                motivo = motivo & datos.Rows(i).Item("motivo").ToString & "<br> » "
            Next
            'Consulta los asuntos de la solicitud
            datos = Obj.TraerDataTable("SOL_ConsultarSolicitudesPendientes", 4, Me.HddCodigoSol.Value.Trim)
            For i As Int16 = 0 To datos.Rows.Count - 1
                asunto = asunto & datos.Rows(i).Item("asunto").ToString & "<br> » "
            Next
            Me.LblResponsable.Text = datos.Rows(0).Item("responsable_sol").ToString
            Me.LblAsunto.Text = Left(asunto, asunto.Length - 2)
            Me.LblMotivo.Text = Left(motivo, motivo.Length - 2)
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
        If Not IsPostBack Then
            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim datos As Data.DataTable
            datos = Obj.TraerDataTable("ConsultarPersonalCentroCostos", "PE", Request.QueryString("id").ToString)
            Me.HddCodigoCco.Value = datos.Rows(0).Item("codigo_cco").ToString
            Me.LblArea.Text = datos.Rows(0).Item("DESCRIPCION_CCO").ToString
            Me.LblUsuario.Text = datos.Rows(0).Item("nombres").ToString
        End If
    End Sub


End Class
