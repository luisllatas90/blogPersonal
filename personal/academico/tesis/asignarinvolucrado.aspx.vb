
Partial Class asignarinvolucrado
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.cboCodigo_dac, obj.TraerDataTable("ConsultarDepartamentoAcademico", "TO", 0), "codigo_dac", "abreviatura_Dac", "--Seleccione--")
            Me.grwInvolucrados.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 5, Request.QueryString("codigo_tes"), 0, 0)
            Me.grwInvolucrados.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            Me.txtFechaInicio.Text = Now.Date.ToShortDateString
        End If
    End Sub
    Protected Sub cboCodigo_dac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCodigo_dac.SelectedIndexChanged
        Me.cbocodigo_per.DataBind()
        If cboCodigo_dac.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.cbocodigo_per, obj.TraerDataTable("TES_ConsultarResponsableTesis", 7, Request.QueryString("codigo_tes"), Me.cboCodigo_dac.SelectedValue, 0), "codigo_per", "docente", "-Seleccione el Profesor-")
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Protected Sub cbocodigo_per_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbocodigo_per.SelectedIndexChanged
        Me.cboCodigo_tpi.DataBind()
        If cbocodigo_per.SelectedValue <> -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(Me.cboCodigo_tpi, obj.TraerDataTable("TES_ConsultarResponsableTesis", 8, Request.QueryString("codigo_tes"), Me.cbocodigo_per.SelectedValue, 0), "codigo_tpi", "descripcion_tpi", "-Seleccione-")
            obj.CerrarConexion()
            obj = Nothing
            If Me.cboCodigo_tpi.Items.Count > 1 Then
                cmdGuardar.Visible = True
            Else
                Me.lblMensaje.Text = "Ya no puede asignar a más profesores, porque ha cumpliado el proceso."
            End If
        End If
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        If Me.cboCodigo_tpi.SelectedValue = -1 Or Me.txtFechaInicio.Text.Trim = "" Then
            Page.RegisterStartupScript("error", "<script>alert('Debe completar todos los datos solicitados de Rol o Fecha de asignación')</script>")
            Exit Sub
        End If
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("TES_AgregarResponsableTesis", Me.cboCodigo_tpi.SelectedValue, Me.cbocodigo_per.SelectedValue, Me.Request.QueryString("codigo_tes"), Request.QueryString("id"), CDate(Me.txtFechaInicio.Text.Trim), Me.txtObs.Text.Trim)
        'Actualizar GridView
        Me.grwInvolucrados.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 5, Request.QueryString("codigo_tes"), 0, 0)
        Me.grwInvolucrados.DataBind()
        obj.CerrarConexion()
        LimpiarValores()
    End Sub
    Private Sub LimpiarValores()
        'Limpiar controles
        Me.cboCodigo_dac.SelectedValue = -1
        Me.cbocodigo_per.Items.Clear()
        Me.cboCodigo_tpi.Items.Clear()
        Me.txtFechaInicio.Text = ""
        Me.lblMensaje.Text = ""
        Me.txtObs.Text = ""
        Me.cmdGuardar.Visible = False
    End Sub
    Protected Sub grwInvolucrados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwInvolucrados.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(5).Attributes.Add("onclick", "return confirm('Acción Irreversible: ¿Esta seguro que desea desactivarlo?');")
        End If
    End Sub
    Protected Sub grwInvolucrados_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwInvolucrados.RowDeleting
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("TES_EliminarResponsableTesis", "M", grwInvolucrados.DataKeys(e.RowIndex).Values("codigo_Rtes").ToString, Request.QueryString("codigo_tes"), Request.QueryString("id"), "")
        Me.grwInvolucrados.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 5, Request.QueryString("codigo_tes"), 0, 0)
        Me.grwInvolucrados.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        LimpiarValores()
        e.Cancel = True
    End Sub
End Class
