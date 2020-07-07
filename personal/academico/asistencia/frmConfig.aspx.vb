
Partial Class academico_asistencia_jquery_Config
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CargaCombos()
            BuscarTipoFuncion()
        End If
    End Sub
    Private Sub CargaCombos()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        objfun.CargarListas(Me.ddlTipoAsistencia, obj.TraerDataTable("Asistencia_ListarTipoAsistencia", 0, True), "codigo_tipo", "descripcion")
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Sub BuscarTipoFuncion()
        Dim obj As New ClsConectarDatos
        Dim objfun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.gvData.DataSource = Nothing
        Me.gvData.DataBind()
        Me.gvData.DataSource = obj.TraerDataTable("Asistencia_ListarTipoAcceso", Me.ddlTipoAsistencia.SelectedValue)
        Me.gvData.DataBind()
        Me.Label1.Text = Me.ddlTipoAsistencia.SelectedItem.Text
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub ddlTipoAsistencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTipoAsistencia.SelectedIndexChanged
        BuscarTipoFuncion()
    End Sub

 

    Protected Sub gvData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvData.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            CType(e.Row.FindControl("chkElegirA"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")
            CType(e.Row.FindControl("chkElegirR"), CheckBox).Attributes.Add("OnClick", "PintarFilaMarcada(this.parentNode.parentNode,this.checked)")

            Dim checkA As CheckBox, checkR As CheckBox
            checkA = e.Row.FindControl("chkElegirA")
            checkR = e.Row.FindControl("chkElegirR")

            If gvData.DataKeys(e.Row.RowIndex).Values("administrar") Then
                checkA.Checked = True
            Else
                checkA.Checked = False
            End If

            checkA.Checked = CBool(gvData.DataKeys(e.Row.RowIndex).Values("administrar"))
            checkR.Checked = CBool(gvData.DataKeys(e.Row.RowIndex).Values("registrar"))
            
        End If

    End Sub

    Protected Sub gvData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvData.RowCommand
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Dim Fila As GridViewRow

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Fila = Me.gvData.Rows(index)
        Dim codigo_tfu As Integer = gvData.DataKeys(index).Values("codigo_tfu")
        If (e.CommandName = "CmdAsignar") Then
            obj.TraerDataTable("Asistencia_RegistrarAcceso", codigo_tfu, Me.ddlTipoAsistencia.SelectedValue, CType(Fila.FindControl("chkElegirA"), CheckBox).Checked, CType(Fila.FindControl("chkElegirR"), CheckBox).Checked)
        End If

        obj.CerrarConexion()

        obj = Nothing
        BuscarTipoFuncion()

    End Sub
End Class
