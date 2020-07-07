
Partial Class administrativo_propuestas2_proponente_TipoPropuestaLista
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            wf_CargarListas()
            cmdConsultar_Click(sender, e)
        End If
    End Sub

    Sub wf_CargarListas()
        Dim obj As New ClsConectarDatos
        Dim dt As Data.DataTable
        Me.hdcodigo_tpc.Value = 0

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        '#Tipo de Propuesta
        dt = New Data.DataTable
        dt = obj.TraerDataTable("PRP_listaTipoPropuesta")
        Me.ddlTipoPropuesta.DataSource = dt
        Me.ddlTipoPropuesta.DataTextField = "descripcion"
        Me.ddlTipoPropuesta.DataValueField = "codigo"
        Me.ddlTipoPropuesta.DataBind()
        'Me.ddlPersonal.SelectedValue = 0
        dt.Dispose()

        ''#Centro de Costos
        dt = New Data.DataTable
        dt = obj.TraerDataTable("PRP_listaCentroCostos")
        Me.ddlCentroCostos.DataSource = dt
        Me.ddlCentroCostos.DataTextField = "descripcion"
        Me.ddlCentroCostos.DataValueField = "codigo"
        Me.ddlCentroCostos.DataBind()
        dt.Dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdConsultar.Click
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            Dim ls_TipoPropuesta As String = IIf(ddlTipoPropuesta.SelectedItem.ToString.Trim = "<<TODOS>>", "%", ddlTipoPropuesta.SelectedValue)
            Dim ls_CentroCosto As String = IIf(ddlCentroCostos.SelectedItem.ToString.Trim = "<<TODOS>>", "%", ddlCentroCostos.SelectedValue)
            Dim ls_estado As String = IIf(ddlEstado.SelectedIndex = 0, "1", "0")

            dtConsultar = obj.TraerDataTable("PRP_ConsultaTipoPropuestaCentroCostos", ls_TipoPropuesta, ls_CentroCosto, ls_estado)
            obj.CerrarConexion()

            dgvPropuestas.DataSource = dtConsultar
            dgvPropuestas.DataBind()
            dgvPropuestas.Dispose()
            obj = Nothing

            'Dim nContador As Integer = 0
            'For i As Integer = 0 To dgv_Personal.Rows.Count - 1
            '    'Response.Write("<script>alert('" & Me.dgv_Personal.Rows(0).Cells(3).Text & "')</script>")
            '    If dgv_Personal.Rows(i).Cells(3).Text = "ACTIVO" Then
            '        nContador = nContador + 1
            '    End If
            'Next

            'Me.lbl_Mensaje.Text = nContador.ToString & " REGISTROS ACTIVOS  DE " & Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            'obj.TraerValor("PRP_RegistarTipoPropuestaCentroCostos", Me.hdcodigo_tpc.Value, ddlTipoPropuesta.SelectedValue, ddlCentroCostos.SelectedValue, "1")
            obj.Ejecutar("PRP_RegistarTipoPropuestaCentroCostos", Me.hdcodigo_tpc.Value, ddlTipoPropuesta.SelectedValue, ddlCentroCostos.SelectedValue, "1")
            Response.Write("<script>alert('Los Datos se Guardaron con Exito')</script>")

            cmdConsultar_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuestas.RowDeleting
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_tpc As Integer = dgvPropuestas.DataKeys(e.RowIndex()).Values("codigo_tpc").ToString
            obj.Ejecutar("PRP_RegistarTipoPropuestaCentroCostos", codigo_tpc, "", "", "2")

            cmdConsultar_Click(sender, e)

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
