
Partial Class administrativo_pec2_frmActividades
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            CargaCboTipoActividad()
            Me.cmdCancelar.Attributes.Add("onclick", "self.parent.tb_remove();")
        End If
    End Sub

    Protected Sub cmdAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAceptar.Click
        Dim obj As New ClsConectarDatos        
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            obj.AbrirConexion()
            obj.Ejecutar("EVE_AgregaActividades", Me.txtTitulo.Text, Me.txtContenido.Text, _
                         Me.txtRuta.Text, Me.cboActividad.SelectedValue)
            obj.CerrarConexion()
            LimpiarControles()
            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Registro Guardado')</SCRIPT>")            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaCboTipoActividad()
        Dim obj As New ClsConectarDatos
        Dim dtTipo As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtTipo = obj.TraerDataTable("EVE_BuscaTipoActividad", 0, "")
        obj.CerrarConexion()
        Me.cboActividad.DataSource = dtTipo
        Me.cboActividad.DataTextField = "descripcion_Tia"
        Me.cboActividad.DataValueField = "codigo_Tia"
        Me.cboActividad.DataBind()
        dtTipo.Dispose()
    End Sub

    Private Sub LimpiarControles()
        Me.txtContenido.Text = ""
        Me.txtRuta.Text = ""
        Me.txtTitulo.Text = ""
    End Sub

    Protected Sub cmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancelar.Click
        'Response.Redirect("frmActividadEvento.aspx?mod=" & Request.QueryString("mod") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&cco=" & Request.QueryString("cco"))
    End Sub
End Class
