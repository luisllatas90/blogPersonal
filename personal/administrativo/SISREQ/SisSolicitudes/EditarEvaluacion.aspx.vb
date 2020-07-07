
Partial Class SisSolicitudes_EditarEvaluacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Dat_Evaluador, Dat_Director, Dat_Aux, Dat_Observadas, Dat_Obs As New Data.DataTable
            Dim nivel As Int16

            'Llena combo para respuesta : Aprobado, Desaprobado
            ClsFunciones.LlenarListas(Me.CmbHaResuelto, Obj.TraerDataTable("SOL_ConsultarResultadoSolicitud", "1", 1), "codigo_res", "descripcion_res", "--Seleccione resultado--")
            Dat_Evaluador = Obj.TraerDataTable("SOL_ConsultarEvaluacionSolicitud", 3, Request.QueryString("codigo_sol"), Request.QueryString("id"))
            If Dat_Evaluador.Rows.Count > 0 Then
                Me.HddCodCco.Value = Dat_Evaluador.Rows(0).Item("codigo_cco")
                nivel = Dat_Evaluador.Rows(0).Item("nivel_Eva")
                Me.HddCodEva.Value = Dat_Evaluador.Rows(0).Item("codigo_eva")
            Else
                Dat_Aux = Obj.TraerDataTable("SOL_ConsultarEvaluadorAuxiliar", 2, CInt(Request.QueryString("id")), Request.QueryString("codigo_sol"))
                If Dat_Aux.Rows.Count > 0 Then
                    HddCodCco.Value = Dat_Aux.Rows(0).Item("codigo_cco")
                    HddCodEva.Value = Dat_Aux.Rows(0).Item("codigo_eva")
                    nivel = 1
                End If
            End If
            Dat_Observadas = Obj.TraerDataTable("SOL_ConsultarSolicitudesObservadas", 5, Request.QueryString("codigo_sol"), nivel)
            If Dat_Observadas.Rows.Count > 0 Then
                Me.CmdGuardar.Enabled = True
                Me.HddCodEob.Value = Dat_Observadas.Rows(0).Item("codigo_eob")
                Me.CmdObservar.Enabled = True
            Else
                Me.CmdGuardar.Enabled = False
                Me.CmdObservar.Enabled = False
            End If
            Me.LblFecha.Text = Date.Now
            Me.HddNivel.Value = nivel
            Dat_Obs = Obj.TraerDataTable("SOL_ConsultarSolicitudesObservadas", 1, Request.QueryString("codigo_sol"), nivel)
            If Dat_Obs.Rows.Count > 0 Then
                Me.TxtObservaciones.Text = Dat_Obs.Rows(0).Item("observacion_eva")
                Me.CmbHaResuelto.SelectedValue = CInt(Dat_Obs.Rows(0).Item("codigo_res"))
            End If
            CmdObservar.Attributes.Add("OnClick", "javascript:AbrirPopUp('ObservarSolicitud.aspx?id_sol=" & Request.QueryString("codigo_sol").ToString & "&id=" & Request.QueryString("id") & "&id_Eva=" & HddCodEva.Value & "&codigo_eob=" & HddCodEob.Value & "','230','350'); return false;")
        End If
    End Sub

    Protected Sub CmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdGuardar.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Try
            Dim ObjMail As New ClsEnviaMail

            Obj.Ejecutar("SOL_AgregarEvaluacionSolicitudCliente", 2, Request.QueryString("codigo_sol"), Me.CmbHaResuelto.SelectedValue, Me.HddCodCco.Value, 0, Me.TxtObservaciones.Text, CInt(Request.QueryString("id").ToString))
            'Envia Correo a las personas indicadas
            ObjMail.EnvioMailRespuestaModificada(CInt(Request.QueryString("codigo_sol")), HddNivel.Value)
            ObjMail.EnviarMailAnulacion(Request.QueryString("codigo_sol"))

            Obj.Ejecutar("SOL_ActualizarObservacion", HddCodEob.Value)
            Me.LblMensaje.ForeColor = Drawing.Color.Blue
            Me.LblMensaje.Text = "Se insertaron los datos correctamente"

            Me.TxtObservaciones.Text = ""
            Me.CmdObservar.Enabled = False
            'Response.Write(CInt(Request.QueryString("codigo_sol")).ToString & " -" & HddNivel.Value.ToString)
        Catch ex As Exception
            Obj.AbortarTransaccion()
            Me.LblMensaje.ForeColor = Drawing.Color.Red
            Me.LblMensaje.Text = ex.Message
            'Me.LblMensaje.Text = "Ocurrió un error al procesar los datos"
        End Try
    End Sub

    Protected Sub CmdObservar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdObservar.Click
        CmdGuardar.Enabled = False
    End Sub
End Class
