
Partial Class Egresados_frmListaOfertasNeuvooDetalle
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (IsPostBack = False) Then
            If (Request.QueryString("ofe") <> "") Then
                Call AsignaDatos(Request.QueryString("ofe"))
            End If
        End If
    End Sub

    Private Sub AsignaDatos(ByVal oferta As String)
        Dim dtDatos As New Data.DataTable
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dtDatos = obj.TraerDataTable("ALUMNI_RetornaOfertaTmpAlumni", oferta)
        obj.CerrarConexion()
        If (dtDatos.Rows.Count > 0) Then
            '        Me.HdCodigo_ofe.Value = dtDatos.Rows(0).Item("codigo_ofe")
            Me.lblTitulo.Text = dtDatos.Rows(0).Item("jobtitle")
            Me.LnkEmpresa.Text = dtDatos.Rows(0).Item("company")
            Me.lblsource.Text = dtDatos.Rows(0).Item("source")
            Me.lblDescripcion.Text = dtDatos.Rows(0).Item("snippet")
            Me.lblLugar.Text = (dtDatos.Rows(0).Item("formattedLocation"))
            'Me.lblWeb1.Text = dtDatos.Rows(0).Item("url").ToString
            Me.hyper_web.Text = dtDatos.Rows(0).Item("url").ToString
            Me.hyper_web.NavigateUrl = dtDatos.Rows(0).Item("url").ToString
            Me.lblInicio.Text = dtDatos.Rows(0).Item("datetime").ToString
            Me.lblDpto.Text = dtDatos.Rows(0).Item("city").ToString

        End If

        dtDatos.Dispose()
        obj = Nothing
    End Sub

    'Protected Sub LnkEmpresa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkEmpresa.Click
    '    Dim Enc As New EncriptaCodigos.clsEncripta
    '    Response.Redirect("frmFichaEmpresa.aspx?pro=" & Enc.Codifica("069" & Me.HdCodigo_pro.Value) & "&ofe=" & Enc.Codifica("069" & Me.HdCodigo_ofe.Value))
    '    'Response.Redirect("frmFichaEmpresa.aspx?pro=" & Enc.Codifica("069" & Me.HdCodigo_pro.Value) & "&KeepThis=true&TB_iframe=true&height=200&width=200&modal=true'")        
    'End Sub
End Class
