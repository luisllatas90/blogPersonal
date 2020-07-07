
Partial Class academico_horarios_administrar_frmsolicitudatender
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Me.ddlAmbiente.DataSource = obj.TraerDataTable("Ambiente_ConsultarAmbiente", "T")
            Me.ddlAmbiente.DataTextField = "Ambiente"
            Me.ddlAmbiente.DataValueField = "codigo_amb"
            Me.ddlAmbiente.DataBind()
            Me.ddlCco.DataSource = obj.TraerDataTable("HorariosPE_ConsultarCcoSol", "T")
            Me.ddlCco.DataTextField = "descripcion_Cco"
            Me.ddlCco.DataValueField = "codigo_cco"
            Me.ddlCco.DataBind()
            obj.CerrarConexion()
            obj = Nothing
            'Me.txtDesde.Value = DateSeriAl(Now.Date.Year, Now.Month, 1)
            Me.txtDesde.Value = Today
            Me.txtHasta.Value = DateSerial(Year(Now.Date), Month(Now.Date) + 1, 0)
            btnBuscar_Click(sender, e)
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim tb As New Data.DataTable
        tb = obj.TraerDataTable("[HorarioPE_ConsultarHorarioAtendido]", Me.ddlAmbiente.SelectedValue, Me.txtDesde.Value, Me.txtHasta.Value, Me.ddlCco.SelectedValue, CInt(Me.checkPreferencial.Checked) * -1, Me.ddlEstado3.SelectedValue)
        Me.gridAmbientes.DataSource = tb      
        Me.gridAmbientes.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub gridAmbientes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridAmbientes.PageIndexChanging
        Me.gridAmbientes.PageIndex = e.NewPageIndex
    End Sub

    Protected Sub gridAmbientes_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridAmbientes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(0).Text = "1" Then
                e.Row.Cells(0).Text = "<img src='images/star.png' title='Ambiente preferencial'>"
            Else
                e.Row.Cells(0).Text = "<img src='images/door.png' title=''>"
            End If
            Dim txt As New TextBox
            txt = e.Row.FindControl("txtObs")
            txt.Text = gridAmbientes.DataKeys(e.Row.RowIndex).Values("obs_lho").ToString

            If e.Row.Cells(16).Text = "A" Then
                e.Row.Cells(13).Text = ""
            End If
            If e.Row.Cells(16).Text = "N" Then
                e.Row.Cells(14).Text = ""
            End If
        End If
    End Sub

    Protected Sub gridAmbientes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridAmbientes.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim txt As New TextBox
        Dim row As GridViewRow = Me.gridAmbientes.Rows(index)
        txt = row.FindControl("txtObs")
        If (e.CommandName = "Atendido") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_ActualizarEstado3", gridAmbientes.DataKeys(index).Values("codigo_lho"), "A", txt.Text.Trim)
            btnBuscar_Click(sender, e)
            obj.CerrarConexion()
            obj = Nothing
        End If
        If (e.CommandName = "NoAtendido") Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("HorarioPE_ActualizarEstado3", gridAmbientes.DataKeys(index).Values("codigo_lho"), "N", txt.Text.Trim)
            btnBuscar_Click(sender, e)
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub

End Class
