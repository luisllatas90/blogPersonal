
Partial Class lsteventos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'Cargar combos.

        End If
    End Sub
    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.grwListaEventos.DataSource = obj.TraerDataTable("EVE_ConsultarEventos", 1, Me.txtbusqueda.Text.Trim, 0)
        Me.grwListaEventos.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub grwListaEventos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaEventos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_cco").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(10).Text = "<a href='frmregistrarevento.aspx?accion=M&cco=" & fila.Row("codigo_cco") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=650&width=800&modal=true' title='Cambiar estado' class='thickbox'>&nbsp;<img src='../../App_Themes/" & Page.Theme & "/img/pencil.png" & "' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("frmregistrarevento.aspx?accion=A&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub

   
End Class
