
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
        Me.hdcodigo_atp.Value = 0

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
        dt = obj.TraerDataTable("PRP_AmbitoTipoPropuesta")
        Me.ddlAmbito.DataSource = dt
        Me.ddlAmbito.DataTextField = "descripcion"
        Me.ddlAmbito.DataValueField = "codigo"
        Me.ddlAmbito.DataBind()
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

            Dim ls_ambito As String = IIf(ddlAmbito.SelectedItem.ToString.Trim = "<<TODOS>>", "%", ddlAmbito.SelectedValue)
            Dim ls_TipoPropuesta As String = IIf(ddlTipoPropuesta.SelectedItem.ToString.Trim = "<<TODOS>>", "%", ddlTipoPropuesta.SelectedValue)
            Dim ls_rectorado As String = IIf(ddlRectorado.SelectedIndex = 0, "%", IIf(ddlRectorado.SelectedIndex = 1, "1", "0"))
            dtConsultar = obj.TraerDataTable("PRP_ListadoAmbitoTipoPropuesta", "0", ls_ambito, ls_TipoPropuesta, ls_rectorado, 1)
            obj.CerrarConexion()

            dgvPropuestas.DataSource = dtConsultar
            dgvPropuestas.DataBind()
            dgvPropuestas.Dispose()
            obj = Nothing

            'Me.lbl_Mensaje.Text = nContador.ToString & " REGISTROS ACTIVOS  DE " & Me.dgv_Personal.Rows.Count.ToString & " REGISTROS ENCONTRADOS"
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Dim dtConsultar As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            'Verificar que todos los filtros sean diferente de TODOS
            If ddlAmbito.SelectedIndex = 0 Then
                Response.Write("<script>alert('Seleccione un Ámbito')</script>")
                ddlAmbito.Focus()
                Return
            End If

            If ddlTipoPropuesta.SelectedIndex = 0 Then
                Response.Write("<script>alert('Seleccione un Tipo de Propuesta')</script>")
                ddlTipoPropuesta.Focus()
                Return
            End If

            If ddlRectorado.SelectedIndex = 0 Then
                Response.Write("<script>alert('Seleccione Rectorado')</script>")
                ddlRectorado.Focus()
                Return
            End If

            'Verificar que no este registrado
            dtConsultar = obj.TraerDataTable("PRP_ListadoAmbitoTipoPropuesta", "0", ddlAmbito.SelectedValue, ddlTipoPropuesta.SelectedValue, "", 2)
            If dtConsultar.Rows(0).Item("dato").ToString = 1 Then
                Response.Write("<script>alert('Tipo de Propuesta para este Ambito ya se encuentra Registrado')</script>")
                Return
            End If

            ''Insetar
            Dim rectorado As Integer = 0
            rectorado = IIf(ddlRectorado.SelectedIndex = 1, 1, 0)
            obj.Ejecutar("PRP_ListadoAmbitoTipoPropuesta", "0", ddlAmbito.SelectedValue, ddlTipoPropuesta.SelectedValue, rectorado, 3)
            Response.Write("<script>alert('Los Datos se Guardaron con Exito')</script>")

            cmdConsultar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvPropuestas_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles dgvPropuestas.RowDeleting
        Try
            Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim codigo_atp As Integer = dgvPropuestas.DataKeys(e.RowIndex()).Values("codigo_atp").ToString
            obj.Ejecutar("PRP_ListadoAmbitoTipoPropuesta", codigo_atp, ddlAmbito.SelectedValue, ddlTipoPropuesta.SelectedValue, ddlRectorado.SelectedIndex, 4)
            cmdConsultar_Click(sender, e)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
