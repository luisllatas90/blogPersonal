
Partial Class Egresado_frmFichaEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("pro") <> Nothing And Request.QueryString("ofe") <> Nothing) Then
                Dim Enc As New EncriptaCodigos.clsEncripta
                AsignaEmpresa(Enc.Decodifica(Request.QueryString("pro")).Substring(3))
                Me.HdCodigo_ofe.Value = Enc.Decodifica(Request.QueryString("ofe")).Substring(3)
            End If
        End If
    End Sub

    Private Sub AsignaEmpresa(ByVal codigo As String)
        Dim dtEmpresa As New Data.DataTable
        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtEmpresa = obj.TraerDataTable("ALUMNI_BuscaEmpresa", codigo, "", "")
        obj.CerrarConexion()

        If (dtEmpresa.Rows.Count > 0) Then
            Me.lblNombre.Text = dtEmpresa.Rows(0).Item("nombrePro")
            Me.lblDireccion.Text = dtEmpresa.Rows(0).Item("direccionPro")
            Me.lblRUC.Text = dtEmpresa.Rows(0).Item("rucPro")
            Me.lblTelefono.Text = dtEmpresa.Rows(0).Item("telefonoPro")
            Me.lblFax.Text = dtEmpresa.Rows(0).Item("faxPro")
            Me.lblCorreo.Text = dtEmpresa.Rows(0).Item("emailPro")
        End If

        dtEmpresa.Dispose()
        obj = Nothing
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("DetOfertas.aspx?ofe=" & Me.HdCodigo_ofe.Value)
    End Sub
End Class
