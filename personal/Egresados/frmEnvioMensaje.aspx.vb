Partial Class Egresado_frmEnvioMensaje
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("pso") <> Nothing) Then
                Dim Enc As New EncriptaCodigos.clsEncripta
                Me.HdCodigo_pso.Value = Enc.Decodifica(Request.QueryString("pso")).Substring(3)

                If (Request.QueryString("envio") = True) Then
                    chkEnvio.Checked = True
                Else
                    chkEnvio.Checked = False
                End If

                Dim cls As New clsEnvioMensajeAlumni
                Dim dtDestino As New Data.DataTable
                dtDestino = cls.RetornaDestinatario(Me.HdCodigo_pso.Value)
                If dtDestino.Rows.Count > 0 Then
                    Me.lblDestino.Text = dtDestino.Rows(0).Item("NombreCompleto") & "  [" & dtDestino.Rows(0).Item("Correo") & "]"
                End If
            Else
                Response.Redirect("frmListaEgresados.aspx")
            End If
        End If
    End Sub

    Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If (ValidaIngreso() = True) Then
            Dim envio As New clsEnvioMensajeAlumni
            envio.EnviarUno(Me.txtTitulo.Text, Me.txtDescripcion.Text, _
                            Request.QueryString("id"), Me.HdCodigo_pso.Value, Me.chkEnvio.Checked)

            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("ALUMNI_ObservarEgresado", Me.HdCodigo_pso.Value)
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

    Private Function ValidaIngreso() As Boolean
        If (Me.txtTitulo.Text.Trim = "") Then
            Me.lblMensaje.Text = "Debe ingresar el título del mensaje"
            Return False
        End If

        If (Me.txtDescripcion.Text.Trim = "") Then
            Me.lblMensaje.Text = "Debe ingresar el la descripción del mensaje"
            Return False
        End If

        Me.lblMensaje.Text = ""
        Return True
    End Function

    Protected Sub chkObservado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkObservado.CheckedChanged
        Me.chkEnvio.Checked = Me.chkObservado.Checked
        Me.txtTitulo.Enabled = Not Me.chkObservado.Checked
        Me.chkEnvio.Enabled = Not Me.chkObservado.Checked

        If (Me.chkObservado.Checked = True) Then
            Me.txtTitulo.Text = "Su perfil de Egresado fue OBSERVADO"
            Me.txtDescripcion.Focus()
        Else
            Me.txtTitulo.Text = ""
        End If
    End Sub

End Class
