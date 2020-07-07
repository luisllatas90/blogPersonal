
Partial Class administrativo_pec_lstCarrerasProfesionales
    Inherits System.Web.UI.Page

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.DgvCarreras.DataSource = obj.TraerDataTable("EVE_ConsultarInformacionParaEvento", 5, Me.txtbusqueda.Text.Trim, "", "")
        Me.DgvCarreras.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub DgvCarreras_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvCarreras.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_cpf").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(5).Text = "<a href='frmRegistraCarreras.aspx?accion=M&codcpf=" & fila.Row("codigo_cpf") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=350&width=400&modal=true' title='Modificar Registro' class='thickbox'>&nbsp;<img src='../../App_Themes/" & Page.Theme & "/img/pencil.png" & "' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("frmRegistraCarreras.aspx?accion=A&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id"))
    End Sub
End Class
