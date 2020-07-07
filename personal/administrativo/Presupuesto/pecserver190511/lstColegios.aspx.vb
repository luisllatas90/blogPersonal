
Partial Class administrativo_pec_lstColegios
    Inherits System.Web.UI.Page

    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.DgvLista.DataSource = obj.TraerDataTable("ConsultarColegio", cboDpto.SelectedValue, Trim(txtbusqueda.Text))
        Me.DgvLista.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub DgvLista_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DgvLista.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            e.Row.Attributes.Add("id", "" & fila.Row("codigo_col").ToString & "")
            e.Row.Attributes.Add("Class", "Sel")
            e.Row.Attributes.Add("Typ", "Sel")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(5).Text = "<a href='frmDatosColegio.aspx?accion=M&pk=" & fila.Row("codigo_col") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=350&width=400&modal=true' title='Modificar Registro' class='thickbox'>&nbsp;<img src='../../App_Themes/" & Page.Theme & "/img/pencil.png" & "' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1
        End If
    End Sub

    Protected Sub cmdNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNuevo.Click
        Response.Redirect("frmDatosColegio.aspx?box=" & Request.QueryString("box") & "&accion=A&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&dep=" & cboDpto.SelectedValue)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            'Cargar Dpto
            Dim obj As New ClsConectarDatos
            Dim objfun As New ClsFunciones

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()

            objfun.CargarListas(cboDpto, obj.TraerDataTable("sp_ver_departamento"), "codigo_Dep", "nombre_Dep")

            obj.CerrarConexion()
            obj = Nothing

            cboDpto.SelectedValue = 13
        End If
    End Sub
End Class
