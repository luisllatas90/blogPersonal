
Partial Class frmprogramapresupuestal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            CargarLista()
        End If
    End Sub
    Private Sub CargarLista()
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Me.grwLista.DataSource = Obj.TraerDataTable("PRESU_ConsultarProgramapresupuestal", 0, 0)
        Me.grwLista.DataBind()
        Obj = Nothing
    End Sub
    Protected Sub grwLista_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grwLista.RowCancelingEdit
        Me.grwLista.EditIndex = -1
        Me.CargarLista()
    End Sub
    Protected Sub grwLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem 'Los datos devueltos (cuando quiero saber la data que llega)
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(0).Text = e.Row.RowIndex + 1

            e.Row.Cells(3).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea eliminar el Programa Presupuestal?');")
        End If
    End Sub
    Protected Sub grwLista_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwLista.RowDeleting
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim mensaje As String
        mensaje = obj.Ejecutar("PRESU_EliminarProgramaPresupuestal", grwLista.DataKeys(e.RowIndex).Value, System.DBNull.Value.ToString)
        obj = Nothing
        e.Cancel = True
        Me.lblmensaje.Text = ""
        If mensaje.ToString <> "" Then
            Me.lblmensaje.Text = mensaje
        Else
            Me.CargarLista()
        End If
    End Sub
    Protected Sub grwLista_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grwLista.RowEditing
        Me.grwLista.EditIndex = e.NewEditIndex
        'Me.grwLista.EditIndex.ToString()
        Me.CargarLista()
    End Sub

    Protected Sub grwLista_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grwLista.RowUpdating
        
        Dim fila As GridViewRow = Me.grwLista.Rows(e.RowIndex)
        Dim lbl As Label = CType(fila.FindControl("lblcodigo_ppr"), Label)
        Dim txt As TextBox = CType(fila.FindControl("txtdescripcion_ppr"), TextBox)

        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Obj.Ejecutar("PRESU_ModificarProgramaPresupuestal", lbl.Text, txt.Text)
        Obj = Nothing
        Me.grwLista.EditIndex = -1
        Me.CargarLista()
    End Sub
    Protected Sub cmdGuardarNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardarNuevo.Click
        Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Obj.Ejecutar("PRESU_AgregarProgramaPresupuestal", Me.txtdescripcion_ppr.Text.Trim)
        Obj = Nothing
        Me.CargarLista()
    End Sub

    Protected Sub grwLista_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grwLista.SelectedIndexChanged

    End Sub
End Class
