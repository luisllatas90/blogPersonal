
Partial Class BecaEstudio_frmBecaxCiclo
    Inherits System.Web.UI.Page

    Protected Sub gvBecaxCiclo_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvBecaxCiclo.RowUpdating
        If Request.QueryString("id") <> "" Then
            Session("Beca_codigo_cac") = Me.ddlCiclo.SelectedValue
            Dim objcnx As New ClsConectarDatos
            Dim valor As Integer
            valor = e.NewValues(0)
            objcnx.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            objcnx.AbrirConexion()
            objcnx.Ejecutar("Beca_RegistrarBecaxCiclo", Me.gvBecaxCiclo.DataKeys.Item(e.RowIndex).Values(0).ToString, valor, CInt(Request.QueryString("id")))
            objcnx.CerrarConexion()
            e.Cancel = True
            Response.Redirect("frmBecaxCiclo.aspx?id=" & Page.Request.QueryString("id") & "&ctf=" & Page.Request.QueryString("ctf"))
        Else
            Response.Write("Debe iniciar sesión")
        End If
    End Sub
    Sub CargarCombos()
        Dim obj As New ClsConectarDatos
        Dim tb As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        tb = obj.TraerDataTable("Beca_ConsultarCicloAcademico")
        Me.ddlCiclo.DataSource = tb
        Me.ddlCiclo.DataTextField = "descripcion_cac"
        Me.ddlCiclo.DataValueField = "codigo_cac"
        Me.ddlCiclo.DataBind()
        If Session("Beca_codigo_cac") IsNot Nothing Then
            Me.ddlCiclo.SelectedValue = Session("codigo_cac")
        End If

        obj.CerrarConexion()
        obj = Nothing

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCombos()
        End If
    End Sub

    Sub VerificarRequisitos()

        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        obj.Ejecutar("Beca_GenerarValoresBeca", Me.ddlCiclo.SelectedValue)
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Try
            VerificarRequisitos()
        Catch ex As Exception
        End Try

    End Sub
End Class
