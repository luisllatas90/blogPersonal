
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub frmsolicitud_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles frmsolicitud.Load
        'Session.Add("id", Request.QueryString("id"))
        If IsPostBack = False Then
            'Me.Form.Attributes.Add("OnSubmit", "return confirm('¿Desea guardar los datos?')")
            Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
            ClsFunciones.LlenarListas(Me.CboTipo, ObjCnx.TraerDataTable("paReq_ConsultarTipoSolicitud", "s"), "id_tsol", "descripcion_tsol", "-- Seleccione Tipo --")
            ClsFunciones.LlenarListas(Me.CboAplicacion, ObjCnx.TraerDataTable("paReq_consultaraplicacion"), "codigo_apl", "descripcion_apl", "-- Seleccione Aplicación --")
            ClsFunciones.LlenarListas(Me.CboArea, ObjCnx.TraerDataTable("paReq_ConsultarCentroCosto"), "codigo_cco", "descripcion_cco", "-- Seleccione Área --")
            ClsFunciones.LlenarListas(Me.CboPersona, ObjCnx.TraerDataTable("paReq_ConsultarPersonal", -1), "codigo_per", "personal", "-- Seleccione Persona --")
            ClsFunciones.LlenarListas(Me.CboEstado, ObjCnx.TraerDataTable("paReq_ConsultarEstado"), "id_est", "descripcion_est")
            Me.CboEstado.Enabled = False
            ObjCnx = Nothing
        End If


    End Sub

    Protected Sub CboArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboArea.SelectedIndexChanged

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        ClsFunciones.LlenarListas(Me.CboPersona, ObjCnx.TraerDataTable("paReq_ConsultarPersonal", Me.CboArea.SelectedValue), "codigo_per", "personal", "-- Seleccione Persona --")
        ObjCnx = Nothing

    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click

        Dim ObjCnx As New ClsSqlServer(ConfigurationManager.ConnectionStrings(1).ConnectionString)
        Try
            ObjCnx.IniciarTransaccion()
            If Me.txtobservacion.Text = "" Then
                ObjCnx.Ejecutar("paReq_InsertarSolicitud", Me.txtDescripcion.Text, Me.CboPrioridad.SelectedValue, Me.CboEstado.SelectedValue, Me.txtfecha.Text, Me.CboTipo.SelectedValue, Me.CboAplicacion.SelectedValue, Me.CboPersona.SelectedValue, Me.CboArea.SelectedValue, Me.txtobservacion.Text, 0)
            Else
                ObjCnx.Ejecutar("paReq_InsertarSolicitud", Me.txtDescripcion.Text, Me.CboPrioridad.SelectedValue, Me.CboEstado.SelectedValue, Me.txtfecha.Text, Me.CboTipo.SelectedValue, Me.CboAplicacion.SelectedValue, Me.CboPersona.SelectedValue, Me.CboArea.SelectedValue, Me.txtobservacion.Text, 1)
            End If
            ObjCnx.Ejecutar("paReq_UpdateBitacora_Codper", Request.QueryString("id"))
            ObjCnx.TerminarTransaccion()
            Response.Write("<script>alert('Se registro correctamente su solicitud')</script>")
            CmdLimpiar_Click(sender, e)
            'Response.Redirect("index.aspx")
        Catch ex As Exception
            ObjCnx.AbortarTransaccion()
            Response.Write("<SCRIPT>alert('Ocurrio un error al procesar los datos' " & ex.Message & " )</SCRIPT>")
        End Try
        ObjCnx = Nothing
    End Sub

    Protected Sub CboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboTipo.SelectedIndexChanged
        If UCase(Me.CboTipo.SelectedItem.ToString) Like UCase("nuevo sistema") Or Me.CboTipo.SelectedValue = 1 Then
            Me.CboAplicacion.SelectedIndex = 0
            Me.CboAplicacion.Enabled = False
            Me.CompareValidator3.EnableClientScript = False
            Me.CompareValidator3.Enabled = False
        Else
            Me.CboAplicacion.SelectedIndex = 0
            Me.CboAplicacion.Enabled = True
            Me.CompareValidator3.Enabled = True
        End If
    End Sub


    Protected Sub CmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdLimpiar.Click
        Me.txtDescripcion.Text = ""
        Me.txtfecha.Text = ""
        Me.txtobservacion.Text = ""
        Me.CboAplicacion.SelectedValue = -1
        Me.CboArea.SelectedValue = -1
        Me.CboPersona.SelectedValue = -1
        Me.CboPrioridad.SelectedValue = -1
        Me.CboTipo.SelectedValue = -1
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
